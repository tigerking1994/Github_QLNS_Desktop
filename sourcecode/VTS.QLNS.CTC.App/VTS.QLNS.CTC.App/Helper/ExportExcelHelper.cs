using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Helper
{
    public static class ExportExcelHelper<T>
    {
        public static List<int> HideColumn(string chiTietToi)
        {
            List<int> hideColumns = new List<int>();
            int columnIndexMax = DynamicMLNS.GetColumnIndexByChiTietToi(chiTietToi);
            List<string> mlnsColumns = new List<string> { "lns", "l", "k", "m", "tm", "ttm", "ng", "tng", "tng1", "tng2", "tng3" };
            foreach (var prop in typeof(T).GetProperties())
            {
                if (prop.GetCustomAttributes(true).Length > 0)
                {
                    ColumnAttribute attribute = prop.GetCustomAttributes(true).First() as ColumnAttribute;
                    if (attribute != null)
                    {
                        string propName = prop.Name.ToLower();
                        if ((mlnsColumns.Contains(propName) || mlnsColumns.Contains(attribute.ColumnName.ToLower())) && (int)attribute.MlnsFiled > columnIndexMax)
                            hideColumns.Add(attribute.ColumnIndex);
                    }
                }
            }
            return hideColumns;
        }

        public static List<int> HideColumnDivision(DivisionColumnVisibility columnVisibility)
        {
            List<int> hideColumns = new List<int>();
            foreach (var prop in typeof(T).GetProperties())
            {
                if (prop.GetCustomAttributes(true).Length > 0)
                {
                    ColumnAttribute attribute = prop.GetCustomAttributes(true).First() as ColumnAttribute;
                    if (attribute != null)
                    {
                        string attributeName = attribute.ColumnName;
                        switch (attributeName)
                        {
                            case EstimateColumn.FTUCHI:
                            case EstimateColumn.FRUTKBNN:
                            case EstimateColumn.FCAPBANGTIEN:
                                if (!columnVisibility.IsDisplayTuChi)
                                    hideColumns.Add(attribute.ColumnIndex);
                                break;
                            case EstimateColumn.FHIENVAT:
                                if (!columnVisibility.IsDisplayHienVat)
                                    hideColumns.Add(attribute.ColumnIndex);
                                break;
                            case EstimateColumn.FPHANCAP:
                                if (!columnVisibility.IsDisplayPhanCap)
                                    hideColumns.Add(attribute.ColumnIndex);
                                break;
                            case EstimateColumn.FHANGNHAP:
                                if (!columnVisibility.IsDisplayHangNhap)
                                    hideColumns.Add(attribute.ColumnIndex);
                                break;
                            case EstimateColumn.FHANGMUA:
                                if (!columnVisibility.IsDisplayHangMua)
                                    hideColumns.Add(attribute.ColumnIndex);
                                break;
                            case EstimateColumn.FDUPHONG:
                                if (!columnVisibility.IsDisplayDuPhong)
                                    hideColumns.Add(attribute.ColumnIndex);
                                break;
                            case EstimateColumn.FTONKHO:
                                if (!columnVisibility.IsDisplayTonKho)
                                    hideColumns.Add(attribute.ColumnIndex);
                                break;
                        }
                    }
                }
            }
            return hideColumns;
        }

        public static List<int> HideColumnFullNameMLNS(string chiTietToi)
        {
            List<int> hideColumns = new List<int>();
            int columnIndexMax = DynamicMLNS.GetColumnIndexByFullNameMLNS(chiTietToi);
            List<string> mlnsColumns = new List<string> { "lns", "Loại", "Khoản", "Mục", "Tiểu mục", "Tiết mục", "Ngành", "Tiểu ngành", "Tiểu ngành 1", "Tiểu ngành 2", "Tiểu ngành 3" };
            foreach (var prop in typeof(T).GetProperties())
            {
                if (prop.GetCustomAttributes(true).Length > 0)
                {
                    ColumnAttribute attribute = prop.GetCustomAttributes(true).First() as ColumnAttribute;
                    if (attribute != null)
                    {
                        string propName = DynamicMLNS.FormatLevelAllocation(prop.Name.ToUpper());
                        if ((mlnsColumns.Contains(propName) || mlnsColumns.Contains(attribute.ColumnName)) && (int)attribute.MlnsFiled > columnIndexMax)
                            hideColumns.Add(attribute.ColumnIndex + 1);
                    }
                }
            }
            return hideColumns;
        }
    }
}
