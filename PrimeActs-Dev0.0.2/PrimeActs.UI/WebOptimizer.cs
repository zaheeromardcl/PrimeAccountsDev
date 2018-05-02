#region Using

using System;
using System.IO.Compression;
using System.Text.RegularExpressions;
using System.Web;
using System.Net;
using System.Text;

#endregion

/// <summary>
/// Contains various methods usable by web projects
/// </summary>
public static class WebOptimizer
{
	#region Private variables

	/// <summary>
	/// The name of the GZIP encoding.
	/// </summary>
	private const string GZIP = "gzip";

	/// <summary>
	/// The name of the Deflate encoding.
	/// </summary>
	private const string DEFLATE = "deflate";

	/// <summary>
	/// A regular expression to localize all whitespace preceeding HTML tag endings.
	/// </summary>
	private static readonly Regex RegexBetweenTags = new Regex(@">\s+", RegexOptions.Compiled);

	/// <summary>
	/// A regular expression to localize all whitespace preceeding a line break.
	/// </summary>
	private static readonly Regex RegexLineBreaks = new Regex(@"\n\s+", RegexOptions.Compiled);

	#endregion

	/// <summary>
	/// Writes the Last-Modified header and sets the conditional get headers.
	/// </summary>
	/// <param name="lastModified">The date of the last modification.</param>
	public static void SetConditionalGetHeaders(DateTime lastModified)
	{
		HttpResponse response = HttpContext.Current.Response;
		HttpRequest request = HttpContext.Current.Request;

		string incomingDate = request.Headers["If-Modified-Since"];

		response.Cache.SetLastModified(lastModified);

		DateTime testDate = DateTime.MinValue;

		if (DateTime.TryParse(incomingDate, out testDate) && testDate == lastModified)
		{
			response.ClearContent();
			response.StatusCode = (int)HttpStatusCode.NotModified;
			response.End();
		}
	}

	/// <summary>
	/// Removes whitespace from the specified string of HTML.
	/// </summary>
	/// <param name="html">The HTML string to remove white space from.</param>
	/// <returns>The specified HTML string stripped from all whitespace.</returns>
	public static string RemoveWhitespaceFromHtml(string html)
	{
		html = RegexBetweenTags.Replace(html, ">");
		html = RegexLineBreaks.Replace(html, string.Empty);

		return html;
	}

	/// <summary>
	/// Strips the whitespace from any .css file.
	/// </summary>
	public static string RemoveWhitespaceFromCss(string body)
	{
		body = body.Replace("  ", " ");
		body = body.Replace(Environment.NewLine, String.Empty);
		body = body.Replace("\t", string.Empty);
		body = body.Replace(" {", "{");
		body = body.Replace(" :", ":");
		body = body.Replace(": ", ":");
		body = body.Replace(", ", ",");
		body = body.Replace("; ", ";");
		body = body.Replace(";}", "}");

		// sometimes found when retrieving CSS remotely
		body = body.Replace(@"?", string.Empty);

		//body = Regex.Replace(body, @"/\*[^\*]*\*+([^/\*]*\*+)*/", "$1");
		body = Regex.Replace(body, @"(?<=[>])\s{2,}(?=[<])|(?<=[>])\s{2,}(?=&nbsp;)|(?<=&ndsp;)\s{2,}(?=[<])", String.Empty);

		//Remove comments from CSS
		body = Regex.Replace(body, @"/\*[\d\D]*?\*/", string.Empty);

		return body;
	}

	/// <summary>
	/// Strips the whitespace from any .js file.
	/// </summary>
	public static string RemoveWhitespaceFromJavaScript(string body)
	{
		string[] lines = body.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
		StringBuilder emptyLines = new StringBuilder();
		foreach (string line in lines)
		{
			string s = line.Trim();
			if (s.Length > 0 && !s.StartsWith("//"))
				emptyLines.AppendLine(s.Trim());
		}

		body = emptyLines.ToString();

		// remove C styles comments
		body = Regex.Replace(body, "/\\*.*?\\*/", String.Empty, RegexOptions.Compiled | RegexOptions.Singleline);
		//// trim left
		body = Regex.Replace(body, "^\\s*", String.Empty, RegexOptions.Compiled | RegexOptions.Multiline);
		//// trim right
		body = Regex.Replace(body, "\\s*[\\r\\n]", "\r\n", RegexOptions.Compiled | RegexOptions.ECMAScript);
		// remove whitespace beside of left curly braced
		body = Regex.Replace(body, "\\s*{\\s*", "{", RegexOptions.Compiled | RegexOptions.ECMAScript);
		// remove whitespace beside of coma
		body = Regex.Replace(body, "\\s*,\\s*", ",", RegexOptions.Compiled | RegexOptions.ECMAScript);
		// remove whitespace beside of semicolon
		body = Regex.Replace(body, "\\s*;\\s*", ";", RegexOptions.Compiled | RegexOptions.ECMAScript);
		// remove newline after keywords
		body = Regex.Replace(body, "\\r\\n(?<=\\b(abstract|boolean|break|byte|case|catch|char|class|const|continue|default|delete|do|double|else|extends|false|final|finally|float|for|function|goto|if|implements|import|in|instanceof|int|interface|long|native|new|null|package|private|protected|public|return|short|static|super|switch|synchronized|this|throw|throws|transient|true|try|typeof|var|void|while|with)\\r\\n)", " ", RegexOptions.Compiled | RegexOptions.ECMAScript);

		return body;
	}

	#region HTTP compression

	/// <summary>
	/// Compresses the HttpContext's output stream using either Deflate or GZip.
	/// </summary>
	/// <param name="context">The current HTTP context to compress.</param>
	public static void Compress(HttpContext context)
	{
		if (context != null)
		{
			if (IsEncodingAccepted(DEFLATE))
			{
				context.Response.Filter = new DeflateStream(context.Response.Filter, CompressionMode.Compress);
				SetEncoding(DEFLATE);
			}
			else if (IsEncodingAccepted(GZIP))
			{
				context.Response.Filter = new GZipStream(context.Response.Filter, CompressionMode.Compress);
				SetEncoding(GZIP);
			}
		}
	}

	/// <summary>
	/// Checks the request headers to see if the specified
	/// encoding is accepted by the client.
	/// </summary>
	/// <param name="encoding">The name of the encoding to check for.</param>
	/// <returns>True if the client supports the specified encoding; otherwise false.</returns>
	private static bool IsEncodingAccepted(string encoding)
	{
		return HttpContext.Current.Request.Headers["Accept-encoding"] != null && HttpContext.Current.Request.Headers["Accept-encoding"].Contains(encoding);
	}

	/// <summary>
	/// Adds the specified encoding to the response headers.
	/// </summary>
	/// <param name="encoding">The encoding to sent to the Accept-encoding HTTP header of the response.</param>
	private static void SetEncoding(string encoding)
	{
		HttpContext.Current.Response.AppendHeader("Content-encoding", encoding);
		HttpContext.Current.Response.Cache.VaryByHeaders["Accept-encoding"] = true;
	}

	#endregion
}