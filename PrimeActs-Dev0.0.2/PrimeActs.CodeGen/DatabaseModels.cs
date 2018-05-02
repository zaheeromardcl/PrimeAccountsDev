using Microsoft.SqlServer.Dac.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeActs.CodeGen
{
    public class DatabaseModel
    {
        public List<TableModel> Tables { get; set; }

        public DatabaseModel()
        {
            Tables = new List<TableModel>();
        }
    }

    public class TableModel
    {
        private static List<string> AUDIT_COLUMNS = new List<string> { "UpdatedBy", "UpdatedDate", "CreatedBy", "CreatedDate" };

        public string Name { get; set; }
        public List<ColumnModel> Columns { get; set; }
        public List<Reference> References { get; set; }

        public string ClassName
        {
            get { return Name.ToClassName(); }
        }

        public string PropertyName
        {
            get { return Name.ToPropertyName(); }
        }

        public string PrimaryKeyName
        {
            get
            {
                var primaryKeyColumn = Columns.FirstOrDefault(x => x.IsPrimarykey);
                if (primaryKeyColumn != null)
                {
                    return primaryKeyColumn.Name;
                }
                return "";
            }
        }

        public bool IsAuditable
        {
            get { return Columns.Any(x => AUDIT_COLUMNS.Contains(x.Name)); }
        }

        public TableModel()
        {
            Columns = new List<ColumnModel>();
            References = new List<Reference>();
        }
    }

    public class ColumnModel
    {
        public string Name { get; set; }
        public SqlDataType SqlDbType { get; set; }
        public bool Nullable { get; set; }
        public int Length { get; set; }
        public bool IsPrimarykey { get; set; }

        public string PropertyName
        {
            get { return Name.ToClassName(); }
        }

        public string CollectionName
        {
            get { return Name.ToPropertyName(); }
        }

        public string CSharpType
        {
            get { return SqlDbType.CSharpType(Nullable); }
        }

        public ColumnModel()
        {
        }
    }

    public class Reference
    {
        public ReferenceType ReferenceType { get; set; }
        public string SourceTableName { get; set; }
        public string SourceColumnName { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }

        public string LinkTableName { get; set; }
        public string LeftColumnName { get; set; }
        public string RightColumnName { get; set; }
    }

    public enum ReferenceType
    {
        Undefined = 0,
        OneToOne = 1,
        OneToMany = 2,
        ManyToOne = 3,
        ManyToMany = 4
    }
}
