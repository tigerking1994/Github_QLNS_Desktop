using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace VTS.QLNS.CTC.Utility
{
    public class EvalExtensions
    {
        public static object Execute(string code, object parameters)
        {
            if (parameters is Dictionary<string, object> param)
            {
                DataTable table = new DataTable();
                foreach (var item in param)
                {
                    DataColumn column = new DataColumn();
                    column.ColumnName = item.Key;
                    column.DataType = System.Type.GetType("System.Decimal");
                    if (item.Value != null)
                    {
                        if (item.Value is string expression)
                        {
                            try
                            {
                                decimal value = Decimal.Parse(expression);
                                column.DefaultValue = value;
                            }
                            catch (Exception)
                            {
                                column.Expression = expression;
                            }
                        }
                        else
                        {
                            column.DefaultValue = item.Value;
                        }
                    }
                    table.Columns.Add(column);
                }

                DataColumn resultColumn = new DataColumn();
                resultColumn.ColumnName = "result";
                resultColumn.Expression = code;
                table.Columns.Add(resultColumn);

                DataRow row = table.NewRow();
                table.Rows.Add(row);
                return row[resultColumn.ColumnName];
            }
            return null;
        }
    }
}
