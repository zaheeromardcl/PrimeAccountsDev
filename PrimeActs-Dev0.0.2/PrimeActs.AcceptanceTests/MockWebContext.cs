﻿#region

using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;

#endregion

namespace PrimeActs.AcceptanceTests
{
    public class MockWebContext
    {
        public MockWebContext()
        {
            RoutingRequestContext = new Mock<RequestContext>(MockBehavior.Loose);
            ActionExecuting = new Mock<ActionExecutingContext>(MockBehavior.Loose);
            Http = new Mock<HttpContextBase>(MockBehavior.Loose);
            Server = new Mock<HttpServerUtilityBase>(MockBehavior.Loose);
            Response = new Mock<HttpResponseBase>(MockBehavior.Loose);
            Request = new Mock<HttpRequestBase>(MockBehavior.Loose);
            Session = new Mock<HttpSessionStateBase>(MockBehavior.Loose);
            Cookies = new HttpCookieCollection();

            RoutingRequestContext.SetupProperty(c => c.HttpContext);
            ActionExecuting.SetupGet(c => c.HttpContext).Returns(Http.Object);
            Http.SetupGet(c => c.Request).Returns(Request.Object);
            Http.SetupGet(c => c.Response).Returns(Response.Object);
            Http.SetupGet(c => c.Server).Returns(Server.Object);
            Http.SetupGet(c => c.Session).Returns(Session.Object);
            Request.Setup(c => c.Cookies).Returns(Cookies);
            Response.Setup(c => c.Cookies).Returns(Cookies);
        }

        public MockWebContext(string user, string[] groups, bool isAuthenticated)
            : this()
        {
            Http.SetupGet(c => c.User.Identity.Name).Returns(user);
            Http.SetupGet(c => c.User.Identity.IsAuthenticated).Returns(isAuthenticated);
            foreach (var group in groups)
            {
                Http.Setup(c => c.User.IsInRole(group)).Returns(isAuthenticated);
            }
        }

        public Mock<RequestContext> RoutingRequestContext { get; private set; }
        public Mock<HttpContextBase> Http { get; private set; }
        public Mock<HttpServerUtilityBase> Server { get; private set; }
        public Mock<HttpResponseBase> Response { get; private set; }
        public Mock<HttpRequestBase> Request { get; private set; }
        public Mock<HttpSessionStateBase> Session { get; private set; }
        public Mock<ActionExecutingContext> ActionExecuting { get; private set; }
        public HttpCookieCollection Cookies { get; private set; }

        public static ControllerContext BasicContext()
        {
            return new ControllerContext
            {
                HttpContext = new MockWebContext().Http.Object
            };
        }

        public static ControllerContext AuthenticatedContext(string user, string[] groups, bool authenticated)
        {
            return new ControllerContext
            {
                HttpContext = new MockWebContext(user, groups, authenticated).Http.Object
            };
        }
    }
}