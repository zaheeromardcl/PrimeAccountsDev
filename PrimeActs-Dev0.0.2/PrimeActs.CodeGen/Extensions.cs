using Microsoft.SqlServer.Dac.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Humanizer;

namespace PrimeActs.CodeGen
{
    public static class Extensions
    {
        public static string RemovePrefixes(this string str)
        {
            return str.Replace("tbl", "").Replace("tlkp", "").Replace("tgen", "");
        }

        public static string ToClassName(this string str)
        {
            return str.RemovePrefixes().Singularize(false);
        }

        public static string ToPropertyName(this string str)
        {
            return str.RemovePrefixes().Pluralize();
        }

        public static string CSharpType(this SqlDataType sqlDbType, bool nullable)
        {
            switch (sqlDbType)
            {
                case SqlDataType.BigInt:
                    return "long" + (nullable ? "?" : "");

                case SqlDataType.Binary:
                case SqlDataType.Image:
                case SqlDataType.Timestamp:
                case SqlDataType.VarBinary:
                    return "byte[]";

                case SqlDataType.Bit:
                    return "bool" + (nullable ? "?" : "");

                case SqlDataType.Char:
                case SqlDataType.NChar:
                case SqlDataType.NText:
                case SqlDataType.NVarChar:
                case SqlDataType.Text:
                case SqlDataType.VarChar:
                case SqlDataType.Xml:
                    return "string";

                case SqlDataType.DateTime:
                case SqlDataType.SmallDateTime:
                case SqlDataType.Date:
                case SqlDataType.Time:
                case SqlDataType.DateTime2:
                    return "DateTime" + (nullable ? "?" : "");

                case SqlDataType.Decimal:
                case SqlDataType.Money:
                case SqlDataType.SmallMoney:
                case SqlDataType.Numeric:
                    return "decimal" + (nullable ? "?" : "");

                case SqlDataType.Float:
                    return "double" + (nullable ? "?" : "");

                case SqlDataType.Int:
                    return "int" + (nullable ? "?" : "");

                case SqlDataType.Real:
                    return "float" + (nullable ? "?" : "");

                case SqlDataType.UniqueIdentifier:
                    return "Guid" + (nullable ? "?" : "");

                case SqlDataType.SmallInt:
                    return "short" + (nullable ? "?" : "");

                case SqlDataType.TinyInt:
                    return "byte" + (nullable ? "?" : "");

                case SqlDataType.Variant:
                case SqlDataType.Table:
                case SqlDataType.DateTimeOffset:
                default:
                    return "";
            }
        }
    }
}
