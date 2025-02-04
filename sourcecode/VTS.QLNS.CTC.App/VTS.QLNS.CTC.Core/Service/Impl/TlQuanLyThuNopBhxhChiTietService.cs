using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Repository.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlQuanLyThuNopBhxhChiTietService : ITlQuanLyThuNopBhxhChiTietService
    {
        private ITlQuanLyThuNopBhxhChiTietRepository _tlQuanLyThuNopBhxhChiTietRepository;
        private ITlQuanLyThuNopBhxhRepository _tlQuanLyThuNopBhxhRepository;
        public TlQuanLyThuNopBhxhChiTietService
        (
            ITlQuanLyThuNopBhxhChiTietRepository tlQuanLyThuNopBhxhChiTietRepository,
            ITlQuanLyThuNopBhxhRepository tlQuanLyThuNopBhxhRepository
         )
        {
            _tlQuanLyThuNopBhxhChiTietRepository = tlQuanLyThuNopBhxhChiTietRepository;
            _tlQuanLyThuNopBhxhRepository = tlQuanLyThuNopBhxhRepository;
        }

        public int Add(TlQuanLyThuNopBhxhChiTiet entity)
        {
            return _tlQuanLyThuNopBhxhChiTietRepository.Add(entity);
        }

        public void BulkInsert(List<TlQuanLyThuNopBhxhChiTiet> dataDetails)
        {
            _tlQuanLyThuNopBhxhChiTietRepository.BulkInsert(dataDetails);
        }

        public void CreateSummaryDetails(Guid iIdParent, string lstidChungTus, string idMaDonVi, int iNamLamViec, int iThang)
        {
            _tlQuanLyThuNopBhxhChiTietRepository.CreateSummaryDetails(iIdParent, lstidChungTus, idMaDonVi, iNamLamViec, iThang);
        }

        public int Delete(TlQuanLyThuNopBhxhChiTiet entity)
        {
            return _tlQuanLyThuNopBhxhChiTietRepository.Delete(entity);
        }

        public IEnumerable<TlQuanLyThuNopBhxhChiTiet> FindByCondition(Expression<Func<TlQuanLyThuNopBhxhChiTiet, bool>> predicate)
        {
            return _tlQuanLyThuNopBhxhChiTietRepository.FindAll(predicate);
        }

        public DataTable GetDataQlThuNopBhxhDetails(Guid id)
        {
            return _tlQuanLyThuNopBhxhChiTietRepository.GetDataQlThuNopBhxhDetails(id);
        }

        public DataTable ReportThuNopBhxhCalculate(DataTable data, int donViTinh, Dictionary<string, string> dsNgach)
        {
            DataTable rs = new DataTable();
            if (data != null && data.Rows.Count > 0)
            {
                data.Columns.Add(ExportColumnHeader.SUM_TL_BHXH, typeof(decimal));
                data.Columns.Add(ExportColumnHeader.SUM_BHTN, typeof(decimal));
                data.Columns.Add(ExportColumnHeader.SUM_BHYT, typeof(decimal));
                data.Columns.Add(ExportColumnHeader.SUM_BHXH, typeof(decimal));
                data.Columns.Add(ExportColumnHeader.SUM_ROW, typeof(decimal));
                // Summary
                // Add mutil column
                rs = data.Clone();
                rs.Columns.Add(ExportColumnHeader.STT, typeof(int));
                rs.Columns.Add(ExportColumnHeader.IS_PARENT, typeof(bool));
                rs.Columns.Add(ExportColumnHeader.SO_CAN_BO, typeof(int));
                rs.Columns.Add(ExportColumnHeader.THANH_TIEN_TAI_DV, typeof(decimal));
                rs.Columns.Add(ExportColumnHeader.THANH_TIEN_QUA_TK, typeof(decimal));


                var columns = data.Columns;
                foreach (var item in dsNgach)
                {
                    int index = 1;
                    string countExpression = string.Format("Count({0})", ExportColumnHeader.MA_CAN_BO);
                    string filter = string.Format("{0} LIKE '{1}%'", ExportColumnHeader.XAU_NOI_MA, item.Key);
                    int soCanBo = Convert.ToInt32(data.Compute(countExpression, filter));
                    if (soCanBo > 0)
                    {
                        // Parent row
                        DataRow rowParent = rs.NewRow();
                        rowParent[ExportColumnHeader.IS_PARENT] = true;
                        rowParent[ExportColumnHeader.MA_CAN_BO] = string.Empty;
                        rowParent[ExportColumnHeader.TEN_CAN_BO] = item.Value;
                        rowParent[ExportColumnHeader.MA_CAP_BAC] = 0;
                        rowParent[ExportColumnHeader.CAP_BAC] = string.Empty;
                        rowParent[ExportColumnHeader.STK] = string.Empty;
                        rowParent[ExportColumnHeader.NGAY_NHAP_NGU] = string.Empty;
                        rowParent[ExportColumnHeader.NGAY_XUAT_NGU] = string.Empty;
                        rowParent[ExportColumnHeader.NGAY_TAI_NGU] = string.Empty;
                        rowParent[ExportColumnHeader.TNN] = 0;
                        rowParent[PhuCap.NTN] = 0;
                        rowParent[ExportColumnHeader.SUM_TL_BHXH] = 0;
                        rowParent[ExportColumnHeader.SUM_BHTN] = 0;
                        rowParent[ExportColumnHeader.SUM_BHXH] = 0;
                        rowParent[ExportColumnHeader.SUM_BHYT] = 0;
                        rowParent[ExportColumnHeader.SUM_ROW] = 0;
                        rowParent[ExportColumnHeader.SO_CAN_BO] = soCanBo;
                        rs.Rows.Add(rowParent);

                        // Add details
                        for (int i = 0; i < data.Rows.Count; i++)
                        {
                            var rowData = data.Rows[i];
                            var rowDetail = rs.NewRow();

                            object sXauNoiMa = rowData[ExportColumnHeader.XAU_NOI_MA];
                            if (sXauNoiMa != null && sXauNoiMa.ToString().StartsWith(item.Key))
                            {
                                rowDetail[ExportColumnHeader.STT] = index++;
                                foreach (DataColumn column in columns)
                                {
                                    string columnName = column.ColumnName;
                                    if (ExportColumnHeader.SUM_TL_BHXH.Equals(columnName))
                                    {
                                        rowData[columnName] = ((decimal)rowData[PhuCap.LHT_TT] + (decimal)rowData[PhuCap.PCCV_TT] + (decimal)rowData[PhuCap.PCTN_TT] + (decimal)rowData[PhuCap.PCTNVK_TT] + (decimal)rowData[PhuCap.HSBL_TT]);

                                    }
                                    else if (ExportColumnHeader.SUM_BHXH.Equals(columnName))
                                    {
                                        rowData[columnName] = ((decimal)rowData[PhuCap.BHXHCN_TT] + (decimal)rowData[PhuCap.BHXHDV_TT]);
                                    }
                                    else if (ExportColumnHeader.SUM_BHYT.Equals(columnName))
                                    {
                                        rowData[columnName] = ((decimal)rowData[PhuCap.BHYTCN_TT] + (decimal)rowData[PhuCap.BHYTDV_TT]);

                                    }
                                    else if (ExportColumnHeader.SUM_BHTN.Equals(columnName))
                                    {
                                        rowData[columnName] = ((decimal)rowData[PhuCap.BHTNCN_TT] + (decimal)rowData[PhuCap.BHTNDV_TT]);

                                    }else if (ExportColumnHeader.SUM_ROW.Equals(columnName))
                                    {
                                        rowData[columnName] = (decimal)rowData[ExportColumnHeader.SUM_TL_BHXH] + (decimal)rowData[ExportColumnHeader.SUM_BHXH] + (decimal)rowData[ExportColumnHeader.SUM_BHTN] + (decimal)rowData[ExportColumnHeader.SUM_BHYT];

                                    }
                                    else if (column.DataType == typeof(decimal))
                                    {
                                        var value = rowData[columnName];
                                        if (value != DBNull.Value)
                                        {
                                            decimal val = decimal.Parse(value.ToString());
                                            rowData[columnName] = Math.Ceiling(val) / donViTinh;
                                        }
                                        else
                                        {
                                            rowData[columnName] = 0;

                                        }
                                    }
                                    rowDetail[columnName] = rowData[columnName];
                                }
                                rs.Rows.Add(rowDetail);
                            }
                        }

                        // Recalculate data parent
                        foreach (DataColumn column in columns)
                        {
                            string columnName = column.ColumnName;
                            if (column.DataType == typeof(decimal))
                            {
                                string sumExpression = string.Format("SUM({0})", columnName);
                                var value = data.Compute(sumExpression, filter);
                                if (value != DBNull.Value)
                                {
                                    decimal val = decimal.Parse(value.ToString());
                                    rowParent[columnName] = Math.Ceiling(val) / donViTinh;
                                }
                            }
                        }
                    }
                }
            }
            return rs;
        }

        public DataTable ReportThuNopBhxh(TlDmDonVi donVi, DataTable data, int donViTinh, bool isOrderChucVu, Dictionary<string, string> dsNgach)
        {
            var subData = data.Select(string.Format("MaDonVi='{0}'", donVi.MaDonVi));
            if (subData.Any())
            {
                return ReportThuNopBhxhCalculate(subData.CopyToDataTable(), donViTinh, dsNgach);
            }
            return ReportThuNopBhxhCalculate(data.Clone(), donViTinh, dsNgach);
        }

        public int Update(TlQuanLyThuNopBhxhChiTiet entity)
        {
            return _tlQuanLyThuNopBhxhChiTietRepository.Update(entity);
        }

        public DataTable ReportThuNopBhxhTongHopTheoDonVi(List<TlDmDonVi> listDonVi, int thang, int nam, int donViTinh, bool isOrderChucVu, string sMaDonViRoot, Dictionary<string, string> dsNgach, bool isCheckedMaHuongLuong)
        {
            var data = ReportThuNopBhxhTongHopTheoDonVi(listDonVi, thang, nam, isOrderChucVu, sMaDonViRoot, isCheckedMaHuongLuong);
            return ReportThuNopBhxhTheoDonViCalculate(listDonVi, data, donViTinh, dsNgach);
        }


        public DataTable ReportThuNopBhxhTongHopTheoDonVi(List<TlDmDonVi> listDonVi, int thang, int nam, bool isOrderChucVu, string sMaDonViRoot, bool isCheckedMaHuongLuong)
        {
            //string maDonVi = string.Join(",", listDonVi.Select(x => x.MaDonVi));
            string maDonVi = sMaDonViRoot;
            return _tlQuanLyThuNopBhxhChiTietRepository.ReportThuNopBhxhTongHopTheoDonVi(maDonVi, thang, nam, isOrderChucVu, false, isCheckedMaHuongLuong);
        }

        public DataTable ReportThuNopBhxhTheoDonViCalculate(List<TlDmDonVi> listDonVi, DataTable data, int donViTinh, Dictionary<string, string> dsNgach)
        {
            DataTable rs = new DataTable();
            if (data != null && data.Rows.Count > 0)
            {
                //ADD column Calculate summary 
                data.Columns.Add(ExportColumnHeader.SUM_TL_BHXH, typeof(decimal));
                data.Columns.Add(ExportColumnHeader.SUM_BHTN, typeof(decimal));
                data.Columns.Add(ExportColumnHeader.SUM_BHYT, typeof(decimal));
                data.Columns.Add(ExportColumnHeader.SUM_BHXH, typeof(decimal));
                data.Columns.Add(ExportColumnHeader.SUM_ROW, typeof(decimal));
                // Summary
                // Add mutil column
                rs = data.Clone();
                rs.Columns.Add(ExportColumnHeader.STT, typeof(int));
                rs.Columns.Add(ExportColumnHeader.IS_PARENT, typeof(bool));
                rs.Columns.Add(ExportColumnHeader.SO_CAN_BO, typeof(int));

                var columns = data.Columns;

                foreach (var donvi in listDonVi)
                {
                    // Parent row
                    string countExpressionDonVi = string.Format("Count({0})", ExportColumnHeader.MA_CAN_BO);
                    string filterDonVi = string.Format("{0} LIKE '{1}' AND ({2} LIKE '1%' OR {3} LIKE '2%' OR {4} LIKE '3%' OR {5} LIKE '4%')"
                        , ExportColumnHeader.MADONVI, donvi.MaDonVi, ExportColumnHeader.XAU_NOI_MA, ExportColumnHeader.XAU_NOI_MA, ExportColumnHeader.XAU_NOI_MA, ExportColumnHeader.XAU_NOI_MA);
                    int soCanBoDonVi = Convert.ToInt32(data.Compute(countExpressionDonVi, filterDonVi));
                    if (soCanBoDonVi == 0) continue;

                    DataRow rowParentDonVi = rs.NewRow();
                    rowParentDonVi[ExportColumnHeader.IS_PARENT] = true;
                    rowParentDonVi[ExportColumnHeader.MA_CAN_BO] = string.Empty;
                    rowParentDonVi[ExportColumnHeader.TEN_CAN_BO] = donvi.TenDonVi;
                    rowParentDonVi[ExportColumnHeader.MA_CAP_BAC] = 0;
                    rowParentDonVi[ExportColumnHeader.CAP_BAC] = string.Empty;
                    rowParentDonVi[ExportColumnHeader.STK] = string.Empty;
                    rowParentDonVi[ExportColumnHeader.NGAY_NHAP_NGU] = string.Empty;
                    rowParentDonVi[ExportColumnHeader.NGAY_XUAT_NGU] = string.Empty;
                    rowParentDonVi[ExportColumnHeader.NGAY_TAI_NGU] = string.Empty;
                    rowParentDonVi[ExportColumnHeader.TNN] = 0;
                    rowParentDonVi[ExportColumnHeader.SUM_TL_BHXH] = 0;
                    rowParentDonVi[ExportColumnHeader.SUM_BHTN] = 0;
                    rowParentDonVi[ExportColumnHeader.SUM_BHXH] = 0;
                    rowParentDonVi[ExportColumnHeader.SUM_BHYT] = 0;
                    rowParentDonVi[ExportColumnHeader.SUM_ROW] = 0;
                    rowParentDonVi[ExportColumnHeader.SO_CAN_BO] = soCanBoDonVi;
                    rs.Rows.Add(rowParentDonVi);
                    foreach (var item in dsNgach)
                    {
                        int index = 1;
                        string countExpression = string.Format("Count({0})", ExportColumnHeader.MA_CAN_BO);
                        string filter = string.Format("{0} LIKE '{1}%' AND {2} LIKE '{3}' ", ExportColumnHeader.XAU_NOI_MA, item.Key, ExportColumnHeader.MADONVI, donvi.MaDonVi);
                        int soCanBo = Convert.ToInt32(data.Compute(countExpression, filter));
                        if (soCanBo > 0)
                        {
                            // Parent row
                            DataRow rowParent = rs.NewRow();
                            rowParent[ExportColumnHeader.IS_PARENT] = true;
                            rowParent[ExportColumnHeader.MA_CAN_BO] = string.Empty;
                            rowParent[ExportColumnHeader.TEN_CAN_BO] = item.Value;
                            rowParent[ExportColumnHeader.MA_CAP_BAC] = 0;
                            rowParent[ExportColumnHeader.CAP_BAC] = string.Empty;
                            rowParent[ExportColumnHeader.STK] = string.Empty;
                            rowParent[ExportColumnHeader.NGAY_NHAP_NGU] = string.Empty;
                            rowParent[ExportColumnHeader.NGAY_XUAT_NGU] = string.Empty;
                            rowParent[ExportColumnHeader.NGAY_TAI_NGU] = string.Empty;
                            rowParent[ExportColumnHeader.TNN] = 0;
                            rowParent[ExportColumnHeader.SUM_TL_BHXH] = 0;
                            rowParent[ExportColumnHeader.SUM_BHTN] = 0;
                            rowParent[ExportColumnHeader.SUM_BHXH] = 0;
                            rowParent[ExportColumnHeader.SUM_BHYT] = 0;
                            rowParent[ExportColumnHeader.SUM_ROW] = 0;
                            rowParent[ExportColumnHeader.SO_CAN_BO] = soCanBo;
                            rs.Rows.Add(rowParent);

                            // Add details
                            for (int i = 0; i < data.Rows.Count; i++)
                            {
                                var rowData = data.Rows[i];
                                var rowDetail = rs.NewRow();

                                object sXauNoiMa = rowData[ExportColumnHeader.XAU_NOI_MA];
                                object sMaDonVi = rowData[ExportColumnHeader.MADONVI];
                                if (sXauNoiMa != null && sXauNoiMa.ToString().StartsWith(item.Key) && sMaDonVi != null && sMaDonVi.ToString().Equals(donvi.MaDonVi))
                                {
                                    rowDetail[ExportColumnHeader.STT] = index++;
                                    foreach (DataColumn column in columns)
                                    {
                                        string columnName = column.ColumnName;
                                        if (PhuCap.LHT_HS.Equals(columnName) || PhuCap.PCTNVK_HS.Equals(columnName) || PhuCap.HSBL_HS.Equals(columnName))
                                        {
                                            var value = rowData[columnName];
                                            if (value != DBNull.Value)
                                            {
                                                decimal val = decimal.Parse(value.ToString());
                                                if (val != 0)
                                                {
                                                    rowData[columnName] = Math.Round(val, 2);
                                                }
                                                else
                                                {
                                                    rowData[columnName] = DBNull.Value;
                                                }
                                            }
                                        }
                                        else if (ExportColumnHeader.SUM_TL_BHXH.Equals(columnName))
                                        {
                                            rowData[columnName] = ((decimal)rowData[PhuCap.LHT_TT] + (decimal)rowData[PhuCap.PCCV_TT] + (decimal)rowData[PhuCap.PCTN_TT] + (decimal)rowData[PhuCap.PCTNVK_TT] + (decimal)rowData[PhuCap.HSBL_TT]);

                                        }
                                        else if (ExportColumnHeader.SUM_BHXH.Equals(columnName))
                                        {
                                            rowData[columnName] = ((decimal)rowData[PhuCap.BHXHCN_TT] + (decimal)rowData[PhuCap.BHXHDV_TT]);
                                        }
                                        else if (ExportColumnHeader.SUM_BHYT.Equals(columnName))
                                        {
                                            rowData[columnName] = ((decimal)rowData[PhuCap.BHYTCN_TT] + (decimal)rowData[PhuCap.BHYTDV_TT]);

                                        }
                                        else if (ExportColumnHeader.SUM_BHTN.Equals(columnName))
                                        {
                                            rowData[columnName] = ((decimal)rowData[PhuCap.BHTNCN_TT] + (decimal)rowData[PhuCap.BHTNDV_TT]);

                                        }
                                        else if (ExportColumnHeader.SUM_ROW.Equals(columnName))
                                        {
                                            rowData[columnName] = (decimal)rowData[ExportColumnHeader.SUM_TL_BHXH] + (decimal)rowData[ExportColumnHeader.SUM_BHXH] + (decimal)rowData[ExportColumnHeader.SUM_BHTN] + (decimal)rowData[ExportColumnHeader.SUM_BHYT];

                                        }
                                        else if (column.DataType == typeof(decimal))
                                        {
                                            var value = rowData[columnName];
                                            if (value != DBNull.Value)
                                            {
                                                decimal val = decimal.Parse(value.ToString());
                                                rowData[columnName] = Math.Ceiling(val) / donViTinh;
                                            }
                                            else
                                            {
                                                rowData[columnName] = 0;

                                            }
                                        }
                                        rowDetail[columnName] = rowData[columnName];
                                    }
                                    rs.Rows.Add(rowDetail);
                                }
                            }

                            // Recalculate data parent
                            // Summary total
                            foreach (DataColumn column in columns)
                            {
                                string columnName = column.ColumnName;
                                if (column.DataType == typeof(decimal))
                                {
                                    string sumExpression = string.Format("SUM({0})", columnName);
                                    var value = data.Compute(sumExpression, filter);
                                    if (value != DBNull.Value)
                                    {
                                        decimal val = decimal.Parse(value.ToString());
                                        rowParent[columnName] = Math.Ceiling(val) / donViTinh;
                                    }
                                }
                            }
                        }
                    }

                    // Recalculate data Agencies
                    // Summary total
                    foreach (DataColumn column in columns)
                    {
                        string columnName = column.ColumnName;
                        if (column.DataType == typeof(decimal))
                        {
                            string sumExpression = string.Format("SUM({0})", columnName);
                            var value = data.Compute(sumExpression, filterDonVi);
                            if (value != DBNull.Value)
                            {
                                decimal val = decimal.Parse(value.ToString());
                                rowParentDonVi[columnName] = Math.Ceiling(val) / donViTinh;
                            }
                        }
                    }
                }
            }
            return rs;
        }

        public DataTable GetDataReportThuNopBhxh(List<TlDmDonVi> listDonVi, int thang, int nam, bool isOrderChucVu, bool isTongHop, string sMaDonViRoot, bool isCheckedMaHuongLuong, bool isInCanBoMoi)
        {
            string maDonVi = string.Join(",", listDonVi.Select(x => x.MaDonVi));
            if (isTongHop) maDonVi = sMaDonViRoot;
            var results = _tlQuanLyThuNopBhxhChiTietRepository.GetDataReportThuNopBhxh(maDonVi, thang, nam, isOrderChucVu, isTongHop, isCheckedMaHuongLuong, isInCanBoMoi);
            //Fillter by listDonvi
            results = results.AsEnumerable()
                       .Where(row => listDonVi.Select(x => x.MaDonVi).Contains(row.Field<string>("MaDonVi")))
                       .CopyToDataTable();
            return results;
        }

        public DataTable ReportThuNopBhxh(TlDmDonViNq104 donVi, DataTable data, int donViTinh, bool isOrderChucVu, Dictionary<string, string> dsNgach)
        {
            var subData = data.Select(string.Format("MaDonVi='{0}'", donVi.MaDonVi));
            if (subData.Any())
            {
                return ReportThuNopBhxhCalculate(subData.CopyToDataTable(), donViTinh, dsNgach);
            }
            return ReportThuNopBhxhCalculate(data.Clone(), donViTinh, dsNgach);
        }

        public DataTable ReportThuNopBhxhTongHopTheoDonVi(List<TlDmDonViNq104> listDonVi, int thang, int nam, int donViTinh, bool isOrderChucVu, string sMaDonViRoot, Dictionary<string, string> dsNgach, bool isCheckedMaHuongLuong)
        {
            var data = ReportThuNopBhxhTongHopTheoDonVi(listDonVi, thang, nam, isOrderChucVu, sMaDonViRoot, isCheckedMaHuongLuong);
            return ReportThuNopBhxhTheoDonViCalculate(listDonVi, data, donViTinh, dsNgach);
        }

        public DataTable ReportThuNopBhxhTheoDonViCalculate(List<TlDmDonViNq104> listDonVi, DataTable data, int donViTinh, Dictionary<string, string> dsNgach)
        {
            DataTable rs = new DataTable();
            if (data != null && data.Rows.Count > 0)
            {
                //ADD column Calculate summary 
                data.Columns.Add(ExportColumnHeader.SUM_TL_BHXH, typeof(decimal));
                data.Columns.Add(ExportColumnHeader.SUM_BHTN, typeof(decimal));
                data.Columns.Add(ExportColumnHeader.SUM_BHYT, typeof(decimal));
                data.Columns.Add(ExportColumnHeader.SUM_BHXH, typeof(decimal));
                data.Columns.Add(ExportColumnHeader.SUM_ROW, typeof(decimal));
                // Summary
                // Add mutil column
                rs = data.Clone();
                rs.Columns.Add(ExportColumnHeader.STT, typeof(int));
                rs.Columns.Add(ExportColumnHeader.IS_PARENT, typeof(bool));
                rs.Columns.Add(ExportColumnHeader.SO_CAN_BO, typeof(int));

                var columns = data.Columns;

                foreach (var donvi in listDonVi)
                {
                    // Parent row
                    string countExpressionDonVi = string.Format("Count({0})", ExportColumnHeader.MA_CAN_BO);
                    string filterDonVi = string.Format("{0} LIKE '{1}' AND ({2} LIKE '1%' OR {3} LIKE '2%' OR {4} LIKE '3%' OR {5} LIKE '4%')"
                        , ExportColumnHeader.MADONVI, donvi.MaDonVi, ExportColumnHeader.XAU_NOI_MA, ExportColumnHeader.XAU_NOI_MA, ExportColumnHeader.XAU_NOI_MA, ExportColumnHeader.XAU_NOI_MA);
                    int soCanBoDonVi = Convert.ToInt32(data.Compute(countExpressionDonVi, filterDonVi));
                    if (soCanBoDonVi == 0) continue;

                    DataRow rowParentDonVi = rs.NewRow();
                    rowParentDonVi[ExportColumnHeader.IS_PARENT] = true;
                    rowParentDonVi[ExportColumnHeader.MA_CAN_BO] = string.Empty;
                    rowParentDonVi[ExportColumnHeader.TEN_CAN_BO] = donvi.TenDonVi;
                    rowParentDonVi[ExportColumnHeader.MA_CAP_BAC] = 0;
                    rowParentDonVi[ExportColumnHeader.CAP_BAC] = string.Empty;
                    rowParentDonVi[ExportColumnHeader.STK] = string.Empty;
                    rowParentDonVi[ExportColumnHeader.NGAY_NHAP_NGU] = string.Empty;
                    rowParentDonVi[ExportColumnHeader.NGAY_XUAT_NGU] = string.Empty;
                    rowParentDonVi[ExportColumnHeader.NGAY_TAI_NGU] = string.Empty;
                    rowParentDonVi[ExportColumnHeader.TNN] = 0;
                    rowParentDonVi[ExportColumnHeader.SUM_TL_BHXH] = 0;
                    rowParentDonVi[ExportColumnHeader.SUM_BHTN] = 0;
                    rowParentDonVi[ExportColumnHeader.SUM_BHXH] = 0;
                    rowParentDonVi[ExportColumnHeader.SUM_BHYT] = 0;
                    rowParentDonVi[ExportColumnHeader.SUM_ROW] = 0;
                    rowParentDonVi[ExportColumnHeader.SO_CAN_BO] = soCanBoDonVi;
                    rs.Rows.Add(rowParentDonVi);
                    foreach (var item in dsNgach)
                    {
                        int index = 1;
                        string countExpression = string.Format("Count({0})", ExportColumnHeader.MA_CAN_BO);
                        string filter = string.Format("{0} LIKE '{1}%' AND {2} LIKE '{3}' ", ExportColumnHeader.XAU_NOI_MA, item.Key, ExportColumnHeader.MADONVI, donvi.MaDonVi);
                        int soCanBo = Convert.ToInt32(data.Compute(countExpression, filter));
                        if (soCanBo > 0)
                        {
                            // Parent row
                            DataRow rowParent = rs.NewRow();
                            rowParent[ExportColumnHeader.IS_PARENT] = true;
                            rowParent[ExportColumnHeader.MA_CAN_BO] = string.Empty;
                            rowParent[ExportColumnHeader.TEN_CAN_BO] = item.Value;
                            rowParent[ExportColumnHeader.MA_CAP_BAC] = 0;
                            rowParent[ExportColumnHeader.CAP_BAC] = string.Empty;
                            rowParent[ExportColumnHeader.STK] = string.Empty;
                            rowParent[ExportColumnHeader.NGAY_NHAP_NGU] = string.Empty;
                            rowParent[ExportColumnHeader.NGAY_XUAT_NGU] = string.Empty;
                            rowParent[ExportColumnHeader.NGAY_TAI_NGU] = string.Empty;
                            rowParent[ExportColumnHeader.TNN] = 0;
                            rowParent[ExportColumnHeader.SUM_TL_BHXH] = 0;
                            rowParent[ExportColumnHeader.SUM_BHTN] = 0;
                            rowParent[ExportColumnHeader.SUM_BHXH] = 0;
                            rowParent[ExportColumnHeader.SUM_BHYT] = 0;
                            rowParent[ExportColumnHeader.SUM_ROW] = 0;
                            rowParent[ExportColumnHeader.SO_CAN_BO] = soCanBo;
                            rs.Rows.Add(rowParent);

                            // Add details
                            for (int i = 0; i < data.Rows.Count; i++)
                            {
                                var rowData = data.Rows[i];
                                var rowDetail = rs.NewRow();

                                object sXauNoiMa = rowData[ExportColumnHeader.XAU_NOI_MA];
                                object sMaDonVi = rowData[ExportColumnHeader.MADONVI];
                                if (sXauNoiMa != null && sXauNoiMa.ToString().StartsWith(item.Key) && sMaDonVi != null && sMaDonVi.ToString().Equals(donvi.MaDonVi))
                                {
                                    rowDetail[ExportColumnHeader.STT] = index++;
                                    foreach (DataColumn column in columns)
                                    {
                                        string columnName = column.ColumnName;
                                        if (PhuCap.LHT_HS.Equals(columnName) || PhuCap.PCTNVK_HS.Equals(columnName) || PhuCap.HSBL_HS.Equals(columnName))
                                        {
                                            var value = rowData[columnName];
                                            if (value != DBNull.Value)
                                            {
                                                decimal val = decimal.Parse(value.ToString());
                                                if (val != 0)
                                                {
                                                    rowData[columnName] = Math.Round(val, 2);
                                                }
                                                else
                                                {
                                                    rowData[columnName] = DBNull.Value;
                                                }
                                            }
                                        }
                                        else if (ExportColumnHeader.SUM_TL_BHXH.Equals(columnName))
                                        {
                                            rowData[columnName] = ((decimal)rowData[PhuCap.LHT_TT] + (decimal)rowData[PhuCap.PCCV_TT] + (decimal)rowData[PhuCap.PCTN_TT] + (decimal)rowData[PhuCap.PCTNVK_TT] + (decimal)rowData[PhuCap.HSBL_TT]);

                                        }
                                        else if (ExportColumnHeader.SUM_BHXH.Equals(columnName))
                                        {
                                            rowData[columnName] = ((decimal)rowData[PhuCap.BHXHCN_TT] + (decimal)rowData[PhuCap.BHXHDV_TT]);
                                        }
                                        else if (ExportColumnHeader.SUM_BHYT.Equals(columnName))
                                        {
                                            rowData[columnName] = ((decimal)rowData[PhuCap.BHYTCN_TT] + (decimal)rowData[PhuCap.BHYTDV_TT]);

                                        }
                                        else if (ExportColumnHeader.SUM_BHTN.Equals(columnName))
                                        {
                                            rowData[columnName] = ((decimal)rowData[PhuCap.BHTNCN_TT] + (decimal)rowData[PhuCap.BHTNDV_TT]);

                                        }
                                        else if (ExportColumnHeader.SUM_ROW.Equals(columnName))
                                        {
                                            rowData[columnName] = (decimal)rowData[ExportColumnHeader.SUM_TL_BHXH] + (decimal)rowData[ExportColumnHeader.SUM_BHXH] + (decimal)rowData[ExportColumnHeader.SUM_BHTN] + (decimal)rowData[ExportColumnHeader.SUM_BHYT];

                                        }
                                        else if (column.DataType == typeof(decimal))
                                        {
                                            var value = rowData[columnName];
                                            if (value != DBNull.Value)
                                            {
                                                decimal val = decimal.Parse(value.ToString());
                                                rowData[columnName] = Math.Ceiling(val) / donViTinh;
                                            }
                                            else
                                            {
                                                rowData[columnName] = 0;

                                            }
                                        }
                                        rowDetail[columnName] = rowData[columnName];
                                    }
                                    rs.Rows.Add(rowDetail);
                                }
                            }

                            // Recalculate data parent
                            // Summary total
                            foreach (DataColumn column in columns)
                            {
                                string columnName = column.ColumnName;
                                if (column.DataType == typeof(decimal))
                                {
                                    string sumExpression = string.Format("SUM({0})", columnName);
                                    var value = data.Compute(sumExpression, filter);
                                    if (value != DBNull.Value)
                                    {
                                        decimal val = decimal.Parse(value.ToString());
                                        rowParent[columnName] = Math.Ceiling(val) / donViTinh;
                                    }
                                }
                            }
                        }
                    }

                    // Recalculate data Agencies
                    // Summary total
                    foreach (DataColumn column in columns)
                    {
                        string columnName = column.ColumnName;
                        if (column.DataType == typeof(decimal))
                        {
                            string sumExpression = string.Format("SUM({0})", columnName);
                            var value = data.Compute(sumExpression, filterDonVi);
                            if (value != DBNull.Value)
                            {
                                decimal val = decimal.Parse(value.ToString());
                                rowParentDonVi[columnName] = Math.Ceiling(val) / donViTinh;
                            }
                        }
                    }
                }
            }
            return rs;
        }

        public DataTable GetDataReportThuNopBhxh(List<TlDmDonViNq104> listDonVi, int thang, int nam, bool isOrderChucVu, bool isTongHop, string sMaDonViRoot, bool isCheckedMaHuongLuong, bool isInCanBoMoi)
        {
            string maDonVi = string.Join(",", listDonVi.Select(x => x.MaDonVi));
            if (isTongHop) maDonVi = sMaDonViRoot;
            var results = _tlQuanLyThuNopBhxhChiTietRepository.GetDataReportThuNopBhxh(maDonVi, thang, nam, isOrderChucVu, isTongHop, isCheckedMaHuongLuong, isInCanBoMoi);
            //Fillter by listDonvi
            results = results.AsEnumerable()
                       .Where(row => listDonVi.Select(x => x.MaDonVi).Contains(row.Field<string>("MaDonVi")))
                       .CopyToDataTable();
            return results;
        }

        public DataTable ReportThuNopBhxhTongHopTheoDonVi(List<TlDmDonViNq104> listDonVi, int thang, int nam, bool isOrderChucVu, string sMaDonViRoot, bool isCheckedMaHuongLuong)
        {
            //string maDonVi = string.Join(",", listDonVi.Select(x => x.MaDonVi));
            string maDonVi = sMaDonViRoot;
            return _tlQuanLyThuNopBhxhChiTietRepository.ReportThuNopBhxhTongHopTheoDonVi(maDonVi, thang, nam, isOrderChucVu, false, isCheckedMaHuongLuong);
        }
    }
}
