using Microsoft.SqlServer.Dac.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeActs.CodeGen
{
    public static class DatabaseModelHelper
    {
        public static DatabaseModel LoadDatabaseModel(TSqlModel model)
        {
            var databaseModel = new DatabaseModel();
            var tables = model.GetObjects(DacQueryScopes.All, ModelSchema.Table);
            foreach (var table in tables)
            {
                var tableModel = new TableModel
                {
                    Name = table.Name.Parts[1]
                };

                var columns = table.GetReferenced(Table.Columns, DacQueryScopes.All);
                foreach (var column in columns)
                {
                    var dataType = column.GetReferenced(Column.DataType).First();
                    var columnModel = new ColumnModel
                    {
                        Name = column.Name.Parts[2],
                        SqlDbType = dataType.GetProperty<SqlDataType>(DataType.SqlDataType),
                        Nullable = column.GetProperty<bool>(Column.Nullable),
                        Length = column.GetProperty<int>(Column.Length)
                    };

                    tableModel.Columns.Add(columnModel);
                }

                var primaryKeyConstraints = table.GetReferencing(PrimaryKeyConstraint.Host, DacQueryScopes.UserDefined);
                foreach (var primaryKeyConstraint in primaryKeyConstraints)
                {
                    var primaryKeyColumn = primaryKeyConstraint.GetReferenced(PrimaryKeyConstraint.Columns).FirstOrDefault();
                    if (primaryKeyColumn != null)
                    {
                        tableModel.Columns.First(x => x.Name == primaryKeyColumn.Name.Parts[2]).IsPrimarykey = true;
                    }
                }

                var foreignKeyConstraints = table.GetReferencing(ForeignKeyConstraint.Host, DacQueryScopes.UserDefined);
                foreach (var foreignKeyConstraint in foreignKeyConstraints)
                {
                    var sourceTable = foreignKeyConstraint.GetReferenced(ForeignKeyConstraint.Host).FirstOrDefault();
                    var sourceColumn = foreignKeyConstraint.GetReferenced(ForeignKeyConstraint.Columns).FirstOrDefault();
                    var referencedTable = foreignKeyConstraint.GetReferenced(ForeignKeyConstraint.ForeignTable).FirstOrDefault();
                    var referencedColumn = foreignKeyConstraint.GetReferenced(ForeignKeyConstraint.ForeignColumns).FirstOrDefault();
                    if (referencedTable != null && referencedColumn != null)
                    {
                        var reference = new Reference
                        {
                            SourceTableName = sourceTable.Name.Parts.Last(),
                            SourceColumnName = sourceColumn.Name.Parts.Last(),
                            TableName = referencedTable.Name.Parts.Last(),
                            ColumnName = referencedColumn.Name.Parts.Last()
                        };
                        tableModel.References.Add(reference);
                    }

                    var t = foreignKeyConstraint.GetReferenced(ForeignKeyConstraint.Host);
                }

                databaseModel.Tables.Add(tableModel);
            }
            ProcessReferences(databaseModel);
            return databaseModel;
        }

        private static void ProcessReferences(DatabaseModel model)
        {
            foreach (var table in model.Tables)
            {
                foreach (var reference in table.References)
                {
                    if (reference.ReferenceType == ReferenceType.Undefined)
                    {
                        var referencedTable = model.Tables.FirstOrDefault(x => x.Name == reference.TableName);
                        // referenced table has a reference to current table
                        var reverseReference = referencedTable.References.FirstOrDefault(x => x.TableName == table.Name);
                        if (reverseReference != null)
                        {
                            reference.ReferenceType = ReferenceType.OneToOne;
                            reverseReference.ReferenceType = ReferenceType.OneToOne;
                        }
                        else
                        {
                            reference.ReferenceType = ReferenceType.ManyToOne;
                            referencedTable.References.Add(
                                new Reference
                                {
                                    ReferenceType = ReferenceType.OneToMany,
                                    TableName = table.Name,
                                    ColumnName = table.PrimaryKeyName
                                });
                        }
                    }
                }
            }

            // Many to Many
            for (var i = model.Tables.Count - 1; i >= 0; i--)
            {
                var table = model.Tables[i];
                if ((table.Columns.Count == 2 || table.Columns.Count == 3) &&
                    table.References.Count == 2 &&
                    table.References[0].ReferenceType == ReferenceType.ManyToOne &&
                    table.References[1].ReferenceType == ReferenceType.ManyToOne)
                {
                    var leftReference = table.References[0];
                    var rightReference = table.References[1];

                    // Add right reference to left table
                    var leftTable = model.Tables.First(x => x.Name == leftReference.TableName);
                    leftTable.References.Remove(leftTable.References.First(x => x.TableName == table.Name));
                    leftTable.References.Add(
                        new Reference
                        {
                            ReferenceType = ReferenceType.ManyToMany,
                            TableName = rightReference.TableName,
                            ColumnName = rightReference.ColumnName,
                            LinkTableName = table.Name,
                            LeftColumnName = leftReference.SourceColumnName,
                            RightColumnName = rightReference.SourceColumnName
                        });

                    // Add left reference
                    var rightTable = model.Tables.First(x => x.Name == rightReference.TableName);
                    rightTable.References.Remove(rightTable.References.First(x => x.TableName == table.Name));
                    rightTable.References.Add(
                        new Reference
                        {
                            ReferenceType = ReferenceType.ManyToMany,
                            TableName = leftReference.TableName,
                            ColumnName = leftReference.ColumnName
                        });

                    model.Tables.RemoveAt(i);
                }
            }
        }
    }
}
