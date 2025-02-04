using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Repository.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlBangLuongThangNq104Service : ITlBangLuongThangNq104Service
    {
        private readonly ITlBangLuongThangNq104Repository _tlBangLuongThangRepository;
        private readonly ITlDmCanBoNq104Repository _tlDmCanboRepository;
        private readonly ITlDmDonViRepository _tlDmDonViRepository;
        private readonly ITlDmCapBacService _tlDmCapBacService;
        private readonly ITlDmCapBacNq104Repository _tlDmCapBacRepository;
        private readonly ITlBaoCaoNq104Repository _tlBaoCaoRepository;
        private readonly ITlCanBoPhuCapNq104Repository _tlCanBoPhuCapRepository;
        private readonly ITlQsChungTuChiTietNq104Repository _tlQsChungTuChiTietRepository;
        private readonly ITlQsChungTuNq104Repository _tlQsChungTuRepository;
        private readonly ITlDmPhuCapNq104Repository _tlDmPhuCapRepository;
        private readonly ITlDmThueThuNhapCaNhanRepository _tlDmThueThuNhapCaNhanRepository;

        public TlBangLuongThangNq104Service(ITlBangLuongThangNq104Repository tlBangLuongThangRepository,
            ITlDmCanBoNq104Repository tlDmCanboRepository, ITlDmDonViRepository tlDmDonViRepository,
            ITlDmCapBacService tlDmCapBacService,
            ITlDmCapBacNq104Repository tlDmCapBacRepository,
            ITlBaoCaoNq104Repository tlBaoCaoRepository,
            ITlCanBoPhuCapNq104Repository tlCanBoPhuCapRepository,
            ITlQsChungTuChiTietNq104Repository tlQsChungTuChiTietRepository,
            ITlQsChungTuNq104Repository tlQsChungTuRepository,
            ITlDmPhuCapNq104Repository tlDmPhuCapRepository,
            ITlDmThueThuNhapCaNhanRepository tlDmThueThuNhapCaNhanRepository)
        {
            _tlBangLuongThangRepository = tlBangLuongThangRepository;
            _tlDmCanboRepository = tlDmCanboRepository;
            _tlDmDonViRepository = tlDmDonViRepository;
            _tlDmCapBacService = tlDmCapBacService;
            _tlDmCapBacRepository = tlDmCapBacRepository;
            _tlBaoCaoRepository = tlBaoCaoRepository;
            _tlCanBoPhuCapRepository = tlCanBoPhuCapRepository;
            _tlQsChungTuChiTietRepository = tlQsChungTuChiTietRepository;
            _tlQsChungTuRepository = tlQsChungTuRepository;
            _tlDmPhuCapRepository = tlDmPhuCapRepository;
            _tlDmThueThuNhapCaNhanRepository = tlDmThueThuNhapCaNhanRepository;
        }

        public int Add(IEnumerable<TlBangLuongThangNq104> entity)
        {
            return _tlBangLuongThangRepository.AddRange(entity);
        }

        public IEnumerable<TlDmCanBoNq104> FindCbLuong(decimal? thang, decimal? nam, string maDonVi)
        {
            return _tlBangLuongThangRepository.FindCb(thang, nam, maDonVi);
        }

        public IEnumerable<TlDmThueThuNhapCaNhanNq104> FindThue(bool bIsThueThang = true)
        {
            return _tlBangLuongThangRepository.FindThue(bIsThueThang);
        }

        public int AddRange(IEnumerable<TlBangLuongThangNq104> entities)
        {
            return _tlBangLuongThangRepository.AddRange(entities);
        }

        public int AddOrUpdateRange(IEnumerable<TlBangLuongThangNq104> entities)
        {
            return _tlBangLuongThangRepository.AddOrUpdateRange(entities);
        }

        public int UpdateRange(IEnumerable<TlBangLuongThangNq104> entities)
        {
            return _tlBangLuongThangRepository.UpdateRange(entities);
        }
        public int Delete(TlBangLuongThangNq104 entity)
        {
            return _tlBangLuongThangRepository.Delete(entity);
        }

        public int Update(TlBangLuongThangNq104 entity)
        {
            return _tlBangLuongThangRepository.Update(entity);
        }

        public int DeleteByParentId(Guid parentId)
        {
            return _tlBangLuongThangRepository.DeleteByParentId(parentId);
        }

        public IEnumerable<TlBangLuongThangNq104> FindByParentId(Guid parentId)
        {
            return _tlBangLuongThangRepository.FindByParentId(parentId);
        }

        public IEnumerable<TlBangLuongThangNq104> FindByCondition(Expression<Func<TlBangLuongThangNq104, bool>> predicate)
        {
            return _tlBangLuongThangRepository.FindAll(predicate);
        }

        public DataTable ReportBangKeTrichThueTNCN(int thang, int nam, string maCachTl, string maDonVi, int donViTinh, bool isExportAll, bool isOrderChucVu)
        {
            var result = _tlBangLuongThangRepository.ReportBangKeTrichThueTNCN(thang, nam, maCachTl, maDonVi, isExportAll, isOrderChucVu);
            result.Columns.Add(ExportColumnHeader.STT, typeof(int));

            DataTable dtresult = new DataTable();

            foreach (DataRow item in result.Rows)
            {
                if (result.Columns.IndexOf("TINHTHUE") != -1)
                {
                    decimal dc_thuttcn = TinhThueTN(item.Field<decimal>("TINHTHUE"));
                    item["THUETNCN_TT"] = dc_thuttcn;
                }
            }

            if (!isExportAll && result.Columns.IndexOf("THUETNCN_TT") != -1)
            {
                var data = result.AsEnumerable().Where(x => x.Field<decimal>("THUETNCN_TT") > 0);
                if (data.Any())
                    return data.CopyToDataTable();
            }
            return result;
        }

        public DataTable ReportBangKeTrichThueTNCNSummary(int thang, int nam, string maCachTl, List<TlDmDonViNq104> tlDmDonVis, int donViTinh, bool isExportAll, bool isOrderChucVu)
        {
            DataTable rs = new DataTable();
            string maDonVi = string.Join(",", tlDmDonVis.Select(x => x.MaDonVi));
            var results = _tlBangLuongThangRepository.ReportBangKeTrichThueTNCN(thang, nam, maCachTl, maDonVi, isExportAll, isOrderChucVu);
            results.Columns.Add(ExportColumnHeader.STT, typeof(int));
            results.Columns.Add(ExportColumnHeader.IS_PARENT, typeof(bool));
            var dicDonVi = tlDmDonVis.ToDictionary(x => x.MaDonVi, x => x.TenDonVi);
            var columns = results.Columns;
            if (results != null && results.Rows.Count > 0)
            {
                rs = results.Clone();
                foreach (var item in dicDonVi)
                {
                    string filter = string.Format("{0} = '{1}'", ExportColumnHeader.MA_DONVI, item.Key);

                    var rowParent = rs.NewRow();
                    rowParent[ExportColumnHeader.TEN_CAN_BO] = item.Value;
                    rowParent[ExportColumnHeader.IS_PARENT] = true;

                    var index = 1;
                    var rowDetails = results.Select(string.Format("MaDonVi='{0}'", item.Key));
                    if (rowDetails.Any())
                    {
                        rs.Rows.Add(rowParent);
                        foreach (DataRow dataRow in rowDetails)
                        {
                            dataRow[ExportColumnHeader.STT] = index++;
                            dataRow[ExportColumnHeader.IS_PARENT] = false;
                            dataRow["THUETNCN_TT"] = TinhThueTN(dataRow.Field<decimal>("TINHTHUE"));
                            rs.Rows.Add(dataRow.ItemArray);
                        }
                    }

                    foreach (DataColumn column in columns)
                    {
                        string columnName = column.ColumnName;
                        if (column.DataType == typeof(decimal))
                        {
                            string sumExpression = string.Format("SUM({0})", columnName);
                            var value = results.Compute(sumExpression, filter);
                            if (value != DBNull.Value)
                            {
                                decimal val = decimal.Parse(value.ToString());
                                rowParent[columnName] = Math.Round(val) / donViTinh;
                            }
                        }
                    }

                }
            }

            return rs;
        }

        public DataTable ReportBangLuongTruyLinhDongPhuCap(List<TlDmDonViNq104> lstDonVi, List<TlDmPhuCapNq104> lstPhuCap, int nam, int thang, string maCachTl, bool isTruyLinh, int donViTinh, bool isOrderChucVu)
        {
            string maDonVi = string.Join(",", lstDonVi.Select(x => x.MaDonVi));
            string[] listPhuCapStatic = new string[]
            {
                PhuCap.NTN,
                PhuCap.THANHTIEN,
                PhuCap.TTL,
                PhuCap.LHT_TT,
                PhuCap.PCCV_TT,
                PhuCap.PCCOV_TT,
                PhuCap.PCTN_TT,
                PhuCap.PHAITRU_SUM,
                PhuCap.PCTNVK_TT
            };
            string[] listPhuCapDynamic = lstPhuCap.Select(x => x.MaPhuCap).Distinct().ToArray();
            string maPhuCap = string.Join(",", listPhuCapStatic.Union(listPhuCapDynamic));
            string condition = string.Format(" {0} < 0 ", PhuCap.THANHTIEN);
            if (isTruyLinh)
                condition = string.Format(" {0} > 0 ", PhuCap.THANHTIEN);
            return _tlBangLuongThangRepository.ReportBangLuongTruyLinhDongPhuCap(maDonVi, maPhuCap, nam, thang, maCachTl, condition, donViTinh, isOrderChucVu);
        }

        public Dictionary<string, List<ReportBangLuongTruyLinhDongPhuCapNq104Query>> ReportBangLuongTruyLinhDongPhuCap(DataTable data, TlDmDonViNq104 donVi, List<TlDmPhuCapNq104> lstPhuCap, bool isTruyLinh)
        {
            var dicResult = new Dictionary<string, List<ReportBangLuongTruyLinhDongPhuCapNq104Query>>();
            List<ReportBangLuongTruyLinhDongPhuCapNq104Query> results = new List<ReportBangLuongTruyLinhDongPhuCapNq104Query>();
            List<ReportBangLuongTruyLinhDongPhuCapNq104Query> itemTotal = new List<ReportBangLuongTruyLinhDongPhuCapNq104Query>();

            var subRows = data.Select(string.Format("MaDonVi='{0}'", donVi.MaDonVi));
            if (subRows.Any())
            {
                var subData = subRows.CopyToDataTable();
                int index = 1;
                int prefixValue = isTruyLinh ? 1 : -1;
                foreach (DataRow dataRow in subRows)
                {
                    ReportBangLuongTruyLinhDongPhuCapNq104Query itemDetail = new ReportBangLuongTruyLinhDongPhuCapNq104Query();
                    itemDetail.Stt = index++;
                    itemDetail.TenCanBo = dataRow[ExportColumnHeader.TEN_CAN_BO].ToString();
                    itemDetail.SoTaiKhoan = dataRow[ExportColumnHeader.SO_TAI_KHOAN].ToString() + " " + dataRow[ExportColumnHeader.MA_CAN_BO].ToString();
                    itemDetail.NgayNhapNgu = dataRow[ExportColumnHeader.NGAY_NHAP_NGU].ToString();
                    itemDetail.NgayXuatNgu = dataRow[ExportColumnHeader.NGAY_XUAT_NGU].ToString();
                    itemDetail.NgayTaiNgu = dataRow[ExportColumnHeader.NGAY_TAI_NGU].ToString();
                    itemDetail.CapBac = dataRow[ExportColumnHeader.MA_CAP_BAC].ToString();
                    itemDetail.NamThamNien = decimal.Parse(dataRow[PhuCap.NTN].ToString());
                    itemDetail.SoThangTruyLinh = string.IsNullOrEmpty(dataRow[PhuCap.TTL].ToString()) ? 0 : decimal.Parse(dataRow[PhuCap.TTL].ToString());
                    itemDetail.LuongCoBan = prefixValue * decimal.Parse(dataRow[PhuCap.LHT_TT].ToString()) + decimal.Parse(dataRow[PhuCap.PCTNVK_TT].ToString());
                    itemDetail.PhuCapChucVu = prefixValue * decimal.Parse(dataRow[PhuCap.PCCV_TT].ToString());
                    itemDetail.PhuCapCongVu = prefixValue * decimal.Parse(dataRow[PhuCap.PCCOV_TT].ToString());
                    itemDetail.PhuCapThamNien = prefixValue * decimal.Parse(dataRow[PhuCap.PCTN_TT].ToString());
                    itemDetail.TongCong = itemDetail.LuongCoBan + itemDetail.PhuCapChucVu + itemDetail.PhuCapCongVu + itemDetail.PhuCapThamNien;
                    itemDetail.TruBHXH = prefixValue * decimal.Parse(dataRow[PhuCap.PHAITRU_SUM].ToString());
                    itemDetail.SoTienDuocNhan = prefixValue * decimal.Parse(dataRow[PhuCap.THANHTIEN].ToString());
                    itemDetail.LstGiaTri = new List<GiaTriTruyLinhDongNq104>();

                    GiaTriTruyLinhDongNq104 dynamicItemDetail;
                    foreach (var phuCap in lstPhuCap)
                    {
                        dynamicItemDetail = new GiaTriTruyLinhDongNq104();
                        dynamicItemDetail.MaPhuCap = phuCap.MaPhuCap;
                        dynamicItemDetail.GiaTri = prefixValue * decimal.Parse(dataRow[phuCap.MaPhuCap].ToString());
                        itemDetail.LstGiaTri.Add(dynamicItemDetail);
                        itemDetail.TongCong += dynamicItemDetail.GiaTri;
                    }
                    results.Add(itemDetail);
                }


                ReportBangLuongTruyLinhDongPhuCapNq104Query itTotal = new ReportBangLuongTruyLinhDongPhuCapNq104Query();
                itTotal.LstGiaTriTotal = new List<GiaTriTruyLinhDongNq104>();
                GiaTriTruyLinhDongNq104 gtTotal;
                foreach (var phuCap in lstPhuCap)
                {
                    gtTotal = new GiaTriTruyLinhDongNq104();
                    gtTotal.MaPhuCap = phuCap.MaPhuCap;
                    gtTotal.GiaTri = prefixValue * decimal.Parse(subData.Compute(string.Format("SUM({0})", phuCap.MaPhuCap), string.Empty).ToString());
                    itTotal.LstGiaTriTotal.Add(gtTotal);
                }
                itemTotal.Add(itTotal);
            }

            dicResult.Add("Items", results);
            dicResult.Add("ItemsTotal", itemTotal);
            return dicResult;
        }

        public Dictionary<string, List<ReportBangLuongTruyLinhDongPhuCapNq104Query>> ReportBangLuongTruyLinhDongPhuCapSummary(DataTable data, List<TlDmPhuCapNq104> lstPhuCap, bool isTruyLinh)
        {
            var dicResult = new Dictionary<string, List<ReportBangLuongTruyLinhDongPhuCapNq104Query>>();
            List<ReportBangLuongTruyLinhDongPhuCapNq104Query> results = new List<ReportBangLuongTruyLinhDongPhuCapNq104Query>();
            List<ReportBangLuongTruyLinhDongPhuCapNq104Query> itemTotal = new List<ReportBangLuongTruyLinhDongPhuCapNq104Query>();

            var subRows = data.AsEnumerable();
            if (subRows.Any())
            {
                var subData = subRows.CopyToDataTable();
                int index = 1;
                int prefixValue = isTruyLinh ? 1 : -1;
                foreach (DataRow dataRow in subRows)
                {
                    ReportBangLuongTruyLinhDongPhuCapNq104Query itemDetail = new ReportBangLuongTruyLinhDongPhuCapNq104Query();
                    itemDetail.Stt = index++;
                    itemDetail.TenCanBo = dataRow[ExportColumnHeader.TEN_CAN_BO].ToString();
                    itemDetail.SoTaiKhoan = dataRow[ExportColumnHeader.SO_TAI_KHOAN].ToString() + " " + dataRow[ExportColumnHeader.MA_CAN_BO].ToString();
                    itemDetail.NgayNhapNgu = dataRow[ExportColumnHeader.NGAY_NHAP_NGU].ToString();
                    itemDetail.NgayXuatNgu = dataRow[ExportColumnHeader.NGAY_XUAT_NGU].ToString();
                    itemDetail.NgayTaiNgu = dataRow[ExportColumnHeader.NGAY_TAI_NGU].ToString();
                    itemDetail.CapBac = dataRow[ExportColumnHeader.MA_CAP_BAC].ToString();
                    itemDetail.NamThamNien = decimal.Parse(dataRow[PhuCap.NTN].ToString());
                    itemDetail.SoThangTruyLinh = string.IsNullOrEmpty(dataRow[PhuCap.TTL].ToString()) ? 0 : decimal.Parse(dataRow[PhuCap.TTL].ToString());
                    itemDetail.LuongCoBan = prefixValue * decimal.Parse(dataRow[PhuCap.LHT_TT].ToString()) + decimal.Parse(dataRow[PhuCap.PCTNVK_TT].ToString());
                    itemDetail.PhuCapChucVu = prefixValue * decimal.Parse(dataRow[PhuCap.PCCV_TT].ToString());
                    itemDetail.PhuCapCongVu = prefixValue * decimal.Parse(dataRow[PhuCap.PCCOV_TT].ToString());
                    itemDetail.PhuCapThamNien = prefixValue * decimal.Parse(dataRow[PhuCap.PCTN_TT].ToString());
                    itemDetail.TongCong = itemDetail.LuongCoBan + itemDetail.PhuCapChucVu + itemDetail.PhuCapCongVu + itemDetail.PhuCapThamNien;
                    itemDetail.TruBHXH = prefixValue * decimal.Parse(dataRow[PhuCap.PHAITRU_SUM].ToString());
                    itemDetail.SoTienDuocNhan = prefixValue * decimal.Parse(dataRow[PhuCap.THANHTIEN].ToString());
                    itemDetail.LstGiaTri = new List<GiaTriTruyLinhDongNq104>();

                    GiaTriTruyLinhDongNq104 dynamicItemDetail;
                    foreach (var phuCap in lstPhuCap)
                    {
                        dynamicItemDetail = new GiaTriTruyLinhDongNq104();
                        dynamicItemDetail.MaPhuCap = phuCap.MaPhuCap;
                        dynamicItemDetail.GiaTri = prefixValue * decimal.Parse(dataRow[phuCap.MaPhuCap].ToString());
                        itemDetail.LstGiaTri.Add(dynamicItemDetail);
                        itemDetail.TongCong += dynamicItemDetail.GiaTri;
                    }
                    results.Add(itemDetail);
                }


                ReportBangLuongTruyLinhDongPhuCapNq104Query itTotal = new ReportBangLuongTruyLinhDongPhuCapNq104Query();
                itTotal.LstGiaTriTotal = new List<GiaTriTruyLinhDongNq104>();
                GiaTriTruyLinhDongNq104 gtTotal;
                foreach (var phuCap in lstPhuCap)
                {
                    gtTotal = new GiaTriTruyLinhDongNq104();
                    gtTotal.MaPhuCap = phuCap.MaPhuCap;
                    gtTotal.GiaTri = prefixValue * decimal.Parse(subData.Compute(string.Format("SUM({0})", phuCap.MaPhuCap), string.Empty).ToString());
                    itTotal.LstGiaTriTotal.Add(gtTotal);
                }
                itemTotal.Add(itTotal);
            }

            dicResult.Add("Items", results);
            dicResult.Add("ItemsTotal", itemTotal);
            return dicResult;
        }

        public DataTable ReportGiaiThichChiTietPhuCapHsqCs(List<TlDmDonViNq104> lstDonVi, int nam, int thang, int donViTinh)
        {
            string maDonVi = string.Join(",", lstDonVi.Select(x => x.MaDonVi));
            return _tlBangLuongThangRepository.GetDataReportGiaiThichChiTietHsqCs(maDonVi, nam, thang, donViTinh);
        }

        public DataTable GetDataReportDanhSachChiTraNganHang(List<TlDmDonViNq104> lstDonVi, int nam, int thang)
        {
            string maDonVi = string.Join(",", lstDonVi.Select(x => x.MaDonVi));
            return _tlBangLuongThangRepository.GetDataReportDanhSachChiTraNganHang(maDonVi, nam, thang);
        }

        public DataTable ReportDanhSachChiTraNganHang(int nam, int thang, int donViTinh, string sNoiDung, params TlDmDonViNq104[] tlDmDonVis)
        {
            var data = _tlBangLuongThangRepository.RptDSChiTraCaNhanNganHang(nam, thang, tlDmDonVis.ToList());
            int index = 1;
            data.Columns.Add(ExportColumnHeader.STT);
            data.Columns.Add(ExportColumnHeader.NOI_DUNG);
            data.Columns.Add(ExportColumnHeader.IS_PARENT, typeof(bool));
            foreach (DataRow row in data.Rows)
            {
                row[ExportColumnHeader.STT] = index++;
                row[ExportColumnHeader.IS_PARENT] = false;
                row[ExportColumnHeader.NOI_DUNG] = sNoiDung;
                if (row[ExportColumnHeader.THANH_TIEN] != DBNull.Value)
                {
                    var thanhTien = Math.Ceiling(Convert.ToDecimal(row[ExportColumnHeader.THANH_TIEN])) / donViTinh;
                    row[ExportColumnHeader.THANH_TIEN] = thanhTien;
                }
            }
            return data;
        }

        public DataTable ReportDanhSachChiTraNganHangTongHopTheoDonVi(int nam, int thang, int donViTinh, string sNoiDung, List<TlDmDonViNq104> tlDmDonVis)
        {
            var data = _tlBangLuongThangRepository.RptDSChiTraCaNhanNganHang(nam, thang, tlDmDonVis);
            if (data != null && data.Rows.Count > 0)
            {
                data.Columns.Add(ExportColumnHeader.STT);
                data.Columns.Add(ExportColumnHeader.IS_PARENT, typeof(bool));
                data.Columns.Add(ExportColumnHeader.NOIDUNG);
                foreach (var donvi in tlDmDonVis)
                {
                    DataRow row = data.Select(string.Format("{0} = '{1}'", ExportColumnHeader.MA_DONVI, donvi.MaDonVi)).FirstOrDefault();
                    if (row != null)
                    {

                        DataRow parentRow = data.NewRow();
                        parentRow[ExportColumnHeader.TEN_CAN_BO] = donvi.TenDonVi;
                        parentRow[ExportColumnHeader.IS_PARENT] = true;
                        parentRow[ExportColumnHeader.NOIDUNG] = string.Empty;

                        string countExpression = string.Format("SUM({0})", ExportColumnHeader.THANH_TIEN);
                        string filter = string.Format("{0} = '{1}'", ExportColumnHeader.MA_DONVI, donvi.MaDonVi);
                        decimal tongTien = Math.Round(Convert.ToDecimal(data.Compute(countExpression, filter))) / donViTinh;
                        parentRow[ExportColumnHeader.THANH_TIEN] = tongTien;

                        int rowIndex = data.Rows.IndexOf(row);
                        data.Rows.InsertAt(parentRow, rowIndex);
                    }
                }

                int index = 1;
                foreach (DataRow row in data.Rows)
                {
                    row[ExportColumnHeader.STT] = index++;
                    if (row[ExportColumnHeader.THANH_TIEN] != DBNull.Value)
                    {
                        var thanhtien = Math.Ceiling(Convert.ToDecimal(row[ExportColumnHeader.THANH_TIEN])) / donViTinh;
                        row[ExportColumnHeader.THANH_TIEN] = thanhtien;
                    }
                    if (row[ExportColumnHeader.IS_PARENT] != DBNull.Value && (bool)row[ExportColumnHeader.IS_PARENT])
                    {
                        index = 1;
                        row[ExportColumnHeader.STT] = "";
                        row[ExportColumnHeader.IS_PARENT] = true;
                    }
                    else
                    {
                        row[ExportColumnHeader.NOIDUNG] = sNoiDung;
                        row[ExportColumnHeader.IS_PARENT] = false;
                    }
                }
            }
            return data;
        }

        public DataTable ReportDanhSachChiTraNganHang(TlDmDonViNq104 donVi, DataTable data, int donViTinh, bool isSummary, bool isShowName)
        {
            var lstDonVi = new List<TlDmDonViNq104>() { donVi };
            return ReportDanhSachChiTraNganHang(lstDonVi, data, donViTinh, isSummary, isShowName);
        }

        public DataTable ReportDanhSachChiTraNganHang(List<TlDmDonViNq104> lstDonVi, DataTable data, int donViTinh, bool isSummary, bool isShowName)
        {
            // Create table results
            DataTable rs = new DataTable();
            rs.Columns.Add(ExportColumnHeader.IS_PARENT, typeof(bool));
            rs.Columns.Add(ExportColumnHeader.STT);
            rs.Columns.Add(ExportColumnHeader.TEN_CAN_BO);
            rs.Columns.Add(ExportColumnHeader.SO_TAI_KHOAN);
            rs.Columns.Add(ExportColumnHeader.MA_DONVI);
            //rs.Columns.Add(ExportColumnHeader.THANH_TIEN, typeof(decimal));
            rs.Columns.Add(ExportColumnHeader.THANH_TIEN);
            // Cột ẩn danh
            rs.Columns.Add(ExportColumnHeader.STT1);
            rs.Columns.Add(ExportColumnHeader.STK1);
            //rs.Columns.Add(ExportColumnHeader.THANH_TIEN1, typeof(decimal));
            rs.Columns.Add(ExportColumnHeader.THANH_TIEN1);
            rs.Columns.Add(ExportColumnHeader.STT2);
            rs.Columns.Add(ExportColumnHeader.STK2);
            //rs.Columns.Add(ExportColumnHeader.THANH_TIEN2, typeof(decimal));
            rs.Columns.Add(ExportColumnHeader.THANH_TIEN2);
            foreach (var item in lstDonVi)
            {
                string filter = string.Format("{0}='{1}'", ExportColumnHeader.MA_DONVI, item.MaDonVi);
                var subData = new DataTable();
                try
                {
                    if (data.Select(filter).Any())
                        subData = data.Select(filter).CopyToDataTable();
                }
                catch
                {
                    subData = null;
                }
                if (subData != null && subData.Rows.Count > 0)
                {
                    if (isSummary)
                    {
                        decimal thanhTien = 0;
                        string sumExpression = string.Format("SUM({0})", ExportColumnHeader.GIA_TRI);
                        var val = subData.Compute(sumExpression, string.Empty);
                        if (val != DBNull.Value)
                        {
                            thanhTien = Convert.ToDecimal(val) / donViTinh;
                        }

                        DataRow parentRow = rs.NewRow();
                        parentRow[ExportColumnHeader.IS_PARENT] = true;
                        parentRow[ExportColumnHeader.TEN_CAN_BO] = item.TenDonVi;
                        parentRow[ExportColumnHeader.THANH_TIEN] = thanhTien.ToString();
                        rs.Rows.Add(parentRow);
                    }

                    for (int i = 0; i < subData.Rows.Count; i++)
                    {
                        var detailRow = rs.NewRow();
                        var currentRow = subData.Rows[i];

                        if (isShowName)
                        {
                            detailRow[ExportColumnHeader.IS_PARENT] = false;
                            detailRow[ExportColumnHeader.STT] = i + 1;
                            detailRow[ExportColumnHeader.TEN_CAN_BO] = currentRow[ExportColumnHeader.TEN_CAN_BO];
                            detailRow[ExportColumnHeader.SO_TAI_KHOAN] = currentRow[ExportColumnHeader.SO_TAI_KHOAN];
                            detailRow[ExportColumnHeader.MA_DONVI] = currentRow[ExportColumnHeader.MA_DONVI];
                            detailRow[ExportColumnHeader.THANH_TIEN] = currentRow[ExportColumnHeader.GIA_TRI];
                        }
                        else
                        {
                            if (i % 2 != 0)
                                continue; // Bước nhảy 2
                            detailRow[ExportColumnHeader.STT1] = i + 1;
                            detailRow[ExportColumnHeader.STK1] = currentRow[ExportColumnHeader.SO_TAI_KHOAN];
                            detailRow[ExportColumnHeader.THANH_TIEN1] = currentRow[ExportColumnHeader.GIA_TRI];
                            if (i + 1 < subData.Rows.Count)
                            {
                                var nextRow = subData.Rows[i + 1];
                                detailRow[ExportColumnHeader.STT2] = i + 2;
                                detailRow[ExportColumnHeader.STK2] = nextRow[ExportColumnHeader.SO_TAI_KHOAN];
                                detailRow[ExportColumnHeader.THANH_TIEN2] = nextRow[ExportColumnHeader.GIA_TRI];
                            }
                        }

                        rs.Rows.Add(detailRow);
                    }
                }
            }
            return rs;
        }

        public DataTable ReportDanhSachChiTraNganHangCaNhan(List<TlDmDonViNq104> lstDonVi, DataTable data, int donViTinh, bool isSummary, bool isShowName)
        {
            // Create table results
            DataTable rs = new DataTable();
            rs.Columns.Add(ExportColumnHeader.IS_PARENT, typeof(bool));
            rs.Columns.Add(ExportColumnHeader.STT);
            rs.Columns.Add(ExportColumnHeader.TEN_CAN_BO);
            rs.Columns.Add(ExportColumnHeader.SO_TAI_KHOAN);
            rs.Columns.Add(ExportColumnHeader.MA_DONVI);
            rs.Columns.Add(ExportColumnHeader.THANH_TIEN);
            // Cột ẩn danh
            rs.Columns.Add(ExportColumnHeader.STT1);
            rs.Columns.Add(ExportColumnHeader.STK1);
            rs.Columns.Add(ExportColumnHeader.THANH_TIEN1);
            rs.Columns.Add(ExportColumnHeader.STT2);
            rs.Columns.Add(ExportColumnHeader.STK2);
            rs.Columns.Add(ExportColumnHeader.THANH_TIEN2);
            if (data != null && data.Rows.Count > 0)
            {
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    var detailRow = rs.NewRow();
                    var currentRow = data.Rows[i];

                    if (isShowName)
                    {
                        detailRow[ExportColumnHeader.IS_PARENT] = false;
                        detailRow[ExportColumnHeader.STT] = i + 1;
                        detailRow[ExportColumnHeader.TEN_CAN_BO] = currentRow[ExportColumnHeader.TEN_CAN_BO];
                        detailRow[ExportColumnHeader.SO_TAI_KHOAN] = currentRow[ExportColumnHeader.SO_TAI_KHOAN];
                        detailRow[ExportColumnHeader.MA_DONVI] = currentRow[ExportColumnHeader.MA_DONVI];
                        detailRow[ExportColumnHeader.THANH_TIEN] = currentRow[ExportColumnHeader.GIA_TRI];
                    }
                    else
                    {
                        if (i % 2 != 0)
                            continue; // Bước nhảy 2
                        detailRow[ExportColumnHeader.STT1] = i + 1;
                        detailRow[ExportColumnHeader.STK1] = currentRow[ExportColumnHeader.SO_TAI_KHOAN];
                        detailRow[ExportColumnHeader.THANH_TIEN1] = currentRow[ExportColumnHeader.GIA_TRI];
                        if (i + 1 < data.Rows.Count)
                        {
                            var nextRow = data.Rows[i + 1];
                            detailRow[ExportColumnHeader.STT2] = i + 2;
                            detailRow[ExportColumnHeader.STK2] = nextRow[ExportColumnHeader.SO_TAI_KHOAN];
                            detailRow[ExportColumnHeader.THANH_TIEN2] = nextRow[ExportColumnHeader.GIA_TRI];
                        }
                    }

                    rs.Rows.Add(detailRow);
                }
            }
            return rs;
        }

        public DataTable ReportThueThuNhapCaNhanNam(string maDonVi, int donViTinh, bool printAll, int Nam, bool isOrderChucVu)
        {
            var results = _tlBangLuongThangRepository.GetDataReportThueTncnNam(maDonVi, Nam, isOrderChucVu);
            results.Columns.Add(PhuCap.THUETNCN_NAM, typeof(decimal));
            results.Columns.Add(ExportColumnHeader.STT, typeof(int));
            results.Columns.Add(ExportColumnHeader.IS_PARENT, typeof(bool));
            DataTable rs = new DataTable();

            if (results != null && results.Rows.Count > 0)
            {
                rs = results.Clone();
                foreach (DataRow dataRow in results.Rows)
                {
                    if (dataRow[ExportColumnHeader.TINHTHUE] != DBNull.Value && dataRow.Field<decimal>("TINHTHUE") <= 0)
                    {
                        dataRow[ExportColumnHeader.TINHTHUE] = 0;
                    }

                    dataRow[PhuCap.THUETNCN_NAM] = Math.Ceiling(TinhThueTN(dataRow.Field<decimal>("TINHTHUE"), false));

                    if (!printAll)
                    {
                        if (dataRow.Field<decimal>(PhuCap.THUETNCN_NAM) == 0 || dataRow[PhuCap.THUETNCN_NAM] == DBNull.Value)
                        {
                            continue;
                        }
                    }

                    rs.Rows.Add(dataRow.ItemArray);
                }
            }

            return rs;
        }

        public DataTable ReportThueThuNhapCaNhanNamSummary(List<TlDmDonViNq104> lstDonVi, int donViTinh, bool printAll, int Nam, bool isOrderChucVu)
        {
            var lstDonViStr = lstDonVi.Select(x => x.MaDonVi).ToList();
            var results = _tlBangLuongThangRepository.GetDataReportThueTncnNam(string.Join(",", lstDonViStr), Nam, isOrderChucVu);
            results.Columns.Add(PhuCap.THUETNCN_NAM, typeof(decimal));
            results.Columns.Add(ExportColumnHeader.STT, typeof(int));
            results.Columns.Add(ExportColumnHeader.IS_PARENT, typeof(bool));
            DataTable rs = new DataTable();

            var dicDonVi = lstDonVi.ToDictionary(x => x.MaDonVi, x => x.TenDonVi);
            var columns = results.Columns;
            if (results != null && results.Rows.Count > 0)
            {
                rs = results.Clone();
                foreach (var item in dicDonVi)
                {
                    string filter = string.Format("{0} = '{1}'", ExportColumnHeader.MA_DONVI, item.Key);

                    var rowParent = rs.NewRow();
                    rowParent[ExportColumnHeader.TEN_CAN_BO] = item.Value;
                    rowParent[ExportColumnHeader.IS_PARENT] = true;

                    var index = 1;
                    var rowsDetail = results.Select(string.Format("MaDonVi='{0}'", item.Key));
                    if (rowsDetail.Any())
                    {
                        rs.Rows.Add(rowParent);
                        foreach (DataRow dataRow in rowsDetail)
                        {
                            dataRow[ExportColumnHeader.STT] = index++;
                            if (dataRow[ExportColumnHeader.TINHTHUE] != DBNull.Value && dataRow.Field<decimal>("TINHTHUE") <= 0)
                            {
                                dataRow[ExportColumnHeader.TINHTHUE] = 0;
                            }

                            dataRow[PhuCap.THUETNCN_NAM] = Math.Ceiling(TinhThueTN(dataRow.Field<decimal>("TINHTHUE"), false));

                            if (!printAll)
                            {
                                if (dataRow.Field<decimal>(PhuCap.THUETNCN_NAM) == 0 || dataRow[PhuCap.THUETNCN_NAM] == DBNull.Value)
                                {
                                    index--;
                                    continue;
                                }
                            }

                            rs.Rows.Add(dataRow.ItemArray);
                        }

                        if (index == 1)
                        {
                            rs.Rows.Remove(rowParent);
                            continue;
                        }
                    }

                    foreach (DataColumn column in columns)
                    {
                        string columnName = column.ColumnName;
                        if (column.DataType == typeof(decimal))
                        {
                            string sumExpression = string.Format("SUM({0})", columnName);
                            var value = results.Compute(sumExpression, filter);
                            if (value != DBNull.Value)
                            {
                                decimal val = decimal.Parse(value.ToString());
                                rowParent[columnName] = Math.Round(val) / donViTinh;
                            }
                        }
                    }
                }
            }

            return rs;
        }

        public DataTable ReportGiaiThichChiTietPhuCapTNVKTHD(List<TlDmDonViNq104> lstDonVi, int nam, int thang, string maCachTl, int donViTinh)
        {
            DataTable rs = new DataTable();
            string maDonVi = string.Join(",", lstDonVi.Select(x => x.MaDonVi));
            var data = _tlBangLuongThangRepository.ReportGiaiThichChiTietPhuCapTNVKTHD(maDonVi, nam, thang, maCachTl, donViTinh);
            if (data != null && data.Rows.Count > 0)
            {
                var columns = data.Columns;
                rs = data.Clone();
                rs.Columns.Add(ExportColumnHeader.IS_PARENT, typeof(bool));

                foreach (var item in lstDonVi)
                {
                    string filter = string.Format("MaDonVi='{0}'", item.MaDonVi);
                    var subData = data.Select(filter);
                    if (subData.Any())
                    {
                        // Header row
                        DataRow rowParent = rs.NewRow();
                        rowParent[ExportColumnHeader.IS_PARENT] = true;
                        rowParent[ExportColumnHeader.DOI_TUONG] = item.TenDonVi;
                        rowParent[ExportColumnHeader.SO_NGUOI] = 0;

                        foreach (DataColumn column in columns)
                        {
                            string columnName = column.ColumnName;
                            if (column.DataType == typeof(decimal) || column.DataType == typeof(int))
                            {
                                string sumExpression = string.Format("SUM({0})", columnName);
                                var value = data.Compute(sumExpression, filter);
                                if (value != DBNull.Value)
                                {
                                    rowParent[columnName] = decimal.Parse(value.ToString());
                                }
                            }
                        }
                        rs.Rows.Add(rowParent);

                        // Detail rows
                        for (int i = 0; i < subData.Length; i++)
                        {
                            rs.Rows.Add(subData[i].ItemArray);
                        }
                    }
                }
            }
            return rs;
        }

        public DataTable GetDataBangLuongThang(List<TlDmDonViNq104> listDonVi, int thang, int nam, bool isOrderChucVu, bool isGiaTriAm, bool isCheckedMaHuongLuong, bool isInCanBoMoi = false, decimal tyLeHuong = 0)
        {
            string maDonVi = string.Join(",", listDonVi.Select(x => x.MaDonVi));
            return _tlBangLuongThangRepository.ReportBangLuongThang(maDonVi, thang, nam, isOrderChucVu, isGiaTriAm, isCheckedMaHuongLuong, isInCanBoMoi, tyLeHuong);
        }

        public DataTable ReportBangLuongThangDong(List<TlDmDonViNq104> listDonVi, int thang, int nam, bool isOrderChucVu, List<string> lstColumnInclude)
        {
            string maDonVi = string.Join(",", listDonVi.Select(x => x.MaDonVi));
            return _tlBangLuongThangRepository.ReportBangLuongThangDong(maDonVi, thang, nam, isOrderChucVu, lstColumnInclude);
        }

        public DataTable GetDataBangLuongThangTheoDonVi(List<TlDmDonViNq104> listDonVi, int thang, int nam, bool isOrderChucVu, bool isGiaTriAm, bool isCheckedMaHuongLuong, bool isInCanBoMoi = false, decimal tyLeHuong = 0)
        {
            string maDonVi = string.Join(",", listDonVi.Select(x => x.MaDonVi));
            return _tlBangLuongThangRepository.ReportBangLuongThangTheoDonVi(maDonVi, thang, nam, isOrderChucVu, isGiaTriAm, isCheckedMaHuongLuong, isInCanBoMoi, tyLeHuong);
        }

        public DataTable GetDataDanhSachCapPhat(List<TlDmDonViNq104> listDonVi, int thang, int nam)
        {
            string maDonVi = string.Join(",", listDonVi.Select(x => x.MaDonVi));
            return _tlBangLuongThangRepository.ReportDanhSachCapPhatPhuCap(maDonVi, thang, nam);
        }

        public DataTable GetDataBangLuongTruyLinh(List<TlDmDonViNq104> listDonVi, int thang, int nam, bool isTruyLinh, bool isOrderChucVu)
        {
            string maDonVi = string.Join(",", listDonVi.Select(x => x.MaDonVi));
            return _tlBangLuongThangRepository.ReportBangLuongTruyLinh(maDonVi, thang, nam, isTruyLinh, isOrderChucVu);
        }

        public DataTable ReportBangLuongThang(TlDmDonViNq104 donVi, DataTable data, int donViTinh, bool isOrderChucVu, Dictionary<string, string> dsNgach)
        {
            var subData = data.Select(string.Format("MaDonVi='{0}'", donVi.MaDonVi));
            if (subData.Any())
            {
                return ReportBangLuongThang(subData.CopyToDataTable(), donViTinh, dsNgach);
            }
            return ReportBangLuongThang(data.Clone(), donViTinh, dsNgach);
        }
        public DataTable ReportBangLuongThangDoc(TlDmDonViNq104 donVi, DataTable data)
        {
            var subData = data.Select(string.Format("MaDonVi='{0}'", donVi.MaDonVi));
            if (subData.Any())
            {
                return ReportBangLuongThangDoc(subData.CopyToDataTable());
            }
            return ReportBangLuongThangDoc(data.Clone());
        }

        public DataTable ReportBangLuongTruyLinh(TlDmDonViNq104 donVi, DataTable data, int donViTinh)
        {
            var subData = data.Select(string.Format("MaDonVi='{0}'", donVi.MaDonVi));
            if (subData.Any())
            {
                return ReportBangLuongTruyLinh(subData.CopyToDataTable(), donViTinh);
            }
            return ReportBangLuongTruyLinh(data.Clone(), donViTinh);
        }

        public DataTable ReportBangLuongTruyLinhSummary(DataTable data, int donViTinh)
        {
            var subData = data.AsEnumerable();
            if (subData.Any())
            {
                return ReportBangLuongTruyLinh(subData.CopyToDataTable(), donViTinh);
            }
            return ReportBangLuongTruyLinh(data.Clone(), donViTinh);
        }

        public DataTable ReportBangLuongThang(List<TlDmDonViNq104> listDonVi, int thang, int nam, int donViTinh, bool isOrderChucVu, bool isGiaTriAm, Dictionary<string, string> dsNgach, bool IsCheckedMaHuongLuong, bool isReduceBHXH, decimal tyLeHuong = 0)
        {
            DataTable data = new DataTable();
            if (isReduceBHXH)
            {
                data = GetDataBangLuongThangTruBHXH(listDonVi, thang, nam, isOrderChucVu, isGiaTriAm, IsCheckedMaHuongLuong, false);
            }
            else
            {
                data = GetDataBangLuongThang(listDonVi, thang, nam, isOrderChucVu, isGiaTriAm, IsCheckedMaHuongLuong, isReduceBHXH, tyLeHuong);
            }
            //return ReportBangLuongThangCoTenDonVi(listDonVi, data, donViTinh, dsNgach);
            return ReportBangLuongThang(data, donViTinh, dsNgach);
        }

        public DataTable ReportBangLuongThangTheoDonVi(List<TlDmDonViNq104> listDonVi, int thang, int nam, int donViTinh, bool isOrderChucVu, bool isGiaTriAm, Dictionary<string, string> dsNgach, bool IsCheckedMaHuongLuong, bool isReduceBHXH, decimal tyLeHuong = 0)
        {
            DataTable data = new DataTable();
            if (isReduceBHXH)
            {
                data = ReportBangLuongThangTheoDonViTruBHXH(listDonVi, thang, nam, isOrderChucVu, isGiaTriAm, IsCheckedMaHuongLuong);
            }
            else
            {
                data = GetDataBangLuongThangTheoDonVi(listDonVi, thang, nam, isOrderChucVu, isGiaTriAm, IsCheckedMaHuongLuong, isReduceBHXH, tyLeHuong);
            }
            return ReportBangLuongThangTheoDonVi(listDonVi, data, donViTinh, dsNgach);
        }

        public DataTable ReportBangLuongThang(DataTable data, int donViTinh, Dictionary<string, string> dsNgach)
        {
            DataTable rs = new DataTable();
            if (data != null && data.Rows.Count > 0)
            {
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
                        rowParent[PhuCap.LHT_HS] = 0;
                        rowParent[PhuCap.HSBL_HS] = 0;
                        rowParent[PhuCap.PCTNVK_HS] = 0;
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
                                    else if (column.DataType == typeof(decimal))
                                    {
                                        var value = rowData[columnName];
                                        if (value != DBNull.Value)
                                        {
                                            decimal val = decimal.Parse(value.ToString());
                                            rowData[columnName] = Math.Round(val / donViTinh);
                                        }
                                    }
                                    rowDetail[columnName] = rowData[columnName];
                                }
                                rs.Rows.Add(rowDetail);
                            }
                        }

                        // Recalculate data parent
                        // Sum thành tiền tại đơn vị
                        string sumThanhTienTaiDVExpression = string.Format("SUM({0})", ExportColumnHeader.THANH_TIEN);
                        string thanhTienTaiDVFilter = string.Format("{0} LIKE '{1}%' AND {2}=1", ExportColumnHeader.XAU_NOI_MA, item.Key, PhuCap.TM);
                        var valueTaiDV = data.Compute(sumThanhTienTaiDVExpression, thanhTienTaiDVFilter);
                        if (valueTaiDV != DBNull.Value)
                        {
                            rowParent[ExportColumnHeader.THANH_TIEN_TAI_DV] = Math.Round((decimal)valueTaiDV);
                        }

                        // Sum thành tiền qua tài khoản
                        string sumThanhTienQuaNHExpression = string.Format("SUM({0})", ExportColumnHeader.THANH_TIEN);
                        string thanhTienQuaNHFilter = string.Format("{0} LIKE '{1}%' AND {2}=0", ExportColumnHeader.XAU_NOI_MA, item.Key, PhuCap.TM);
                        var valueQuaTK = data.Compute(sumThanhTienQuaNHExpression, thanhTienQuaNHFilter);
                        if (valueQuaTK != DBNull.Value)
                        {
                            rowParent[ExportColumnHeader.THANH_TIEN_QUA_TK] = Math.Round((decimal)valueQuaTK);
                        }

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
                                    rowParent[columnName] = Math.Round(val);
                                }
                            }
                        }
                    }
                }
            }
            return rs;
        }

        public DataTable ReportBangLuongThangTheoDonVi(List<TlDmDonViNq104> listDonVi, DataTable data, int donViTinh, Dictionary<string, string> dsNgach)
        {
            DataTable rs = new DataTable();
            if (data != null && data.Rows.Count > 0)
            {
                // Summary
                // Add mutil column
                rs = data.Clone();
                rs.Columns.Add(ExportColumnHeader.STT, typeof(int));
                rs.Columns.Add(ExportColumnHeader.IS_PARENT, typeof(bool));
                rs.Columns.Add(ExportColumnHeader.SO_CAN_BO, typeof(int));
                rs.Columns.Add(ExportColumnHeader.THANH_TIEN_TAI_DV, typeof(decimal));
                rs.Columns.Add(ExportColumnHeader.THANH_TIEN_QUA_TK, typeof(decimal));

                var columns = data.Columns;

                foreach (var donvi in listDonVi)
                {
                    // Parent row
                    string countExpressionDonVi = string.Format("Count({0})", ExportColumnHeader.MA_CAN_BO);
                    string filterDonVi = string.Format("{0} LIKE '{1}' AND ({2} LIKE '1%' OR {3} LIKE '2%' OR {4} LIKE '3%' OR {5} LIKE '4%' OR {6} LIKE '5%' OR {7} LIKE '6%')"
                        , ExportColumnHeader.MADONVI, donvi.MaDonVi, ExportColumnHeader.XAU_NOI_MA, ExportColumnHeader.XAU_NOI_MA, ExportColumnHeader.XAU_NOI_MA, ExportColumnHeader.XAU_NOI_MA, ExportColumnHeader.XAU_NOI_MA, ExportColumnHeader.XAU_NOI_MA);
                    int soCanBoDonVi = Convert.ToInt32(data.Compute(countExpressionDonVi, filterDonVi));
                    if (soCanBoDonVi == 0)
                        continue;

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
                    rowParentDonVi[PhuCap.NTN] = 0;
                    rowParentDonVi[PhuCap.LHT_HS] = 0;
                    rowParentDonVi[PhuCap.HSBL_HS] = 0;
                    rowParentDonVi[PhuCap.PCTNVK_HS] = 0;
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
                            rowParent[PhuCap.NTN] = 0;
                            rowParent[PhuCap.LHT_HS] = 0;
                            rowParent[PhuCap.HSBL_HS] = 0;
                            rowParent[PhuCap.PCTNVK_HS] = 0;
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
                                        else if (column.DataType == typeof(decimal))
                                        {
                                            var value = rowData[columnName];
                                            if (value != DBNull.Value)
                                            {
                                                decimal val = decimal.Parse(value.ToString());
                                                rowData[columnName] = Math.Round(val / donViTinh);
                                                //rowData[columnName] = Math.Ceiling(val) / donViTinh;
                                            }
                                        }
                                        rowDetail[columnName] = rowData[columnName];
                                    }
                                    rs.Rows.Add(rowDetail);
                                }
                            }

                            // Recalculate data parent
                            // Sum thành tiền tại đơn vị
                            string sumThanhTienTaiDVExpression = string.Format("SUM({0})", ExportColumnHeader.THANH_TIEN);
                            string thanhTienTaiDVFilter = string.Format("{0} LIKE '{1}%' AND {2}=1", ExportColumnHeader.XAU_NOI_MA, item.Key, PhuCap.TM);
                            var valueTaiDV = data.Compute(sumThanhTienTaiDVExpression, thanhTienTaiDVFilter);
                            if (valueTaiDV != DBNull.Value)
                            {
                                rowParent[ExportColumnHeader.THANH_TIEN_TAI_DV] = Math.Round((decimal)valueTaiDV);
                            }

                            // Sum thành tiền qua tài khoản
                            string sumThanhTienQuaNHExpression = string.Format("SUM({0})", ExportColumnHeader.THANH_TIEN);
                            string thanhTienQuaNHFilter = string.Format("{0} LIKE '{1}%' AND {2}=0", ExportColumnHeader.XAU_NOI_MA, item.Key, PhuCap.TM);
                            var valueQuaTK = data.Compute(sumThanhTienQuaNHExpression, thanhTienQuaNHFilter);
                            if (valueQuaTK != DBNull.Value)
                            {
                                rowParent[ExportColumnHeader.THANH_TIEN_QUA_TK] = Math.Round((decimal)valueQuaTK);
                            }

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
                                        //rowParent[columnName] = Math.Ceiling(val) / donViTinh;
                                        //rowParent[columnName] = Math.Round(val / donViTinh);
                                        rowParent[columnName] = Math.Round(val);
                                    }
                                }
                            }
                        }
                    }

                    // Recalculate data parent
                    // Sum thành tiền tại đơn vị
                    string sumThanhTienTaiDVTongDonViExpression = string.Format("SUM({0})", ExportColumnHeader.THANH_TIEN);
                    string thanhTienTaiDVTongDonViFilter = string.Format("{0} LIKE '{1}' AND {2}=1 AND ({3} LIKE '1%' OR {4} LIKE '2%' OR {5} LIKE '3%')",
                        ExportColumnHeader.MADONVI, donvi.MaDonVi, PhuCap.TM, ExportColumnHeader.XAU_NOI_MA, ExportColumnHeader.XAU_NOI_MA, ExportColumnHeader.XAU_NOI_MA);
                    var valueTaiDVTongDonVi = data.Compute(sumThanhTienTaiDVTongDonViExpression, thanhTienTaiDVTongDonViFilter);
                    if (valueTaiDVTongDonVi != DBNull.Value)
                    {
                        rowParentDonVi[ExportColumnHeader.THANH_TIEN_TAI_DV] = Math.Round((decimal)valueTaiDVTongDonVi);
                    }

                    // Sum thành tiền qua tài khoản
                    string sumThanhTienQuaNHTongDonViExpression = string.Format("SUM({0})", ExportColumnHeader.THANH_TIEN);
                    string thanhTienQuaNHTongDonViFilter = string.Format("{0} LIKE '{1}%' AND {2}=0 AND ({3} LIKE '1%' OR {4} LIKE '2%' OR {5} LIKE '3%')",
                        ExportColumnHeader.MADONVI, donvi.MaDonVi, PhuCap.TM, ExportColumnHeader.XAU_NOI_MA, ExportColumnHeader.XAU_NOI_MA, ExportColumnHeader.XAU_NOI_MA);
                    var valueQuaTKTongDonVi = data.Compute(sumThanhTienQuaNHTongDonViExpression, thanhTienQuaNHTongDonViFilter);
                    if (valueQuaTKTongDonVi != DBNull.Value)
                    {
                        rowParentDonVi[ExportColumnHeader.THANH_TIEN_QUA_TK] = Math.Round((decimal)valueQuaTKTongDonVi);
                    }

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
                                //rowParentDonVi[columnName] = Math.Ceiling(val) / donViTinh;
                                rowParentDonVi[columnName] = Math.Round(val);
                            }
                        }
                    }
                }
            }
            return rs;
        }

        public DataTable ReportBangLuongThangDoc(DataTable data)
        {
            DataTable rs = new DataTable();
            if (data != null && data.Rows.Count > 0)
            {
                // Summary
                // Add mutil column
                rs = data.Clone();

                var columns = data.Columns;
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    var rowData = data.Rows[i];
                    var rowDetail = rs.NewRow();

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
                        else if (column.DataType == typeof(decimal))
                        {
                            var value = rowData[columnName];
                            if (value != DBNull.Value)
                            {
                                decimal val = decimal.Parse(value.ToString());
                                rowData[columnName] = Math.Round(val);
                            }
                        }
                        rowDetail[columnName] = rowData[columnName];
                    }
                    rs.Rows.Add(rowDetail);
                }
            }
            return rs;
        }

        public DataTable ReportBangLuongTruyLinh(DataTable data, int donViTinh)
        {
            DataTable rs = new DataTable();
            if (data != null && data.Rows.Count > 0)
            {
                var dsNgach = new Dictionary<string, string>()
                {
                    { "1", "Sĩ quan"},
                    { "2", "QNCN"},
                    { "4", "CNVQP"},
                    { "0", "HSQ - CS"}
                };

                // Summary
                // Add mutil column
                rs = data.Clone();
                rs.Columns.Add(ExportColumnHeader.STT, typeof(int));
                rs.Columns.Add(ExportColumnHeader.IS_PARENT, typeof(bool));
                rs.Columns.Add(ExportColumnHeader.SO_CAN_BO, typeof(int));

                var columns = data.Columns;
                foreach (var item in dsNgach)
                {
                    int index = 1;
                    string countExpression = string.Format("Count({0})", ExportColumnHeader.MA_CAN_BO);
                    string filter = string.Format("{0} LIKE '{1}%'", ExportColumnHeader.MA_CAP_BAC, item.Key);
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
                        rowParent[ExportColumnHeader.SO_CAN_BO] = soCanBo;
                        rs.Rows.Add(rowParent);

                        // Add details
                        for (int i = 0; i < data.Rows.Count; i++)
                        {
                            var rowData = data.Rows[i];
                            var rowDetail = rs.NewRow();

                            object maCapBac = rowData[ExportColumnHeader.MA_CAP_BAC];
                            if (maCapBac != null && maCapBac.ToString().StartsWith(item.Key))
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
                                                rowData[columnName] = Math.Round(val);
                                                //rowData[columnName] = Math.Round(val, 2);
                                            }
                                            else
                                            {
                                                rowData[columnName] = DBNull.Value;
                                            }
                                        }
                                    }
                                    else if (PhuCap.TTL.Equals(columnName) || PhuCap.TTL_LHT.Equals(columnName) || PhuCap.TTL_PCCV.Equals(columnName) || PhuCap.TTL_PCCOV.Equals(columnName))
                                    {
                                        var value = rowData[columnName];
                                        if (value != DBNull.Value)
                                        {
                                            decimal val = decimal.Parse(value.ToString());
                                            if (val != 0)
                                            {
                                                rowData[columnName] = Math.Round(decimal.Parse(val.ToString("G29")));
                                            }
                                            else
                                            {
                                                rowData[columnName] = DBNull.Value;
                                            }
                                        }
                                    }
                                    else if (column.DataType == typeof(decimal))
                                    {
                                        var value = rowData[columnName];
                                        if (value != DBNull.Value)
                                        {
                                            decimal val = decimal.Parse(value.ToString());
                                            rowData[columnName] = Math.Round(val / donViTinh);
                                        }
                                    }
                                    rowDetail[columnName] = rowData[columnName];
                                }
                                rs.Rows.Add(rowDetail);
                            }
                        }

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
                                    //rowParent[columnName] = Math.Ceiling(val) / donViTinh;
                                    rowParent[columnName] = Math.Round(val);
                                }
                            }
                        }
                    }
                }
            }
            return rs;
        }

        public DataTable ReportDienBienLuong(string maHieuCanBo, DateTime tuNgay, DateTime denNgay)
        {
            var data = _tlBangLuongThangRepository.ReportDienBienLuong(maHieuCanBo, tuNgay.ToShortDateString(), denNgay.ToShortDateString());
            data.Columns.Add(ExportColumnHeader.STT, typeof(int));
            data.Columns.Add(ExportColumnHeader.THANH_TIEN_TAI_DV, typeof(decimal));
            data.Columns.Add(ExportColumnHeader.THANH_TIEN_QUA_TK, typeof(decimal));

            int index = 1;

            foreach (DataRow item in data.Rows)
            {
                item[ExportColumnHeader.STT] = index++;

                var lhtValue = item[PhuCap.LHT_HS];
                if (lhtValue != DBNull.Value)
                {
                    decimal val = decimal.Parse(lhtValue.ToString());
                    if (val != 0)
                    {
                        item[PhuCap.LHT_HS] = Math.Round(val, 2);
                    }
                    else
                    {
                        item[PhuCap.LHT_HS] = DBNull.Value;
                    }
                }

                var pctnvkValue = item[PhuCap.PCTNVK_HS];
                if (pctnvkValue != DBNull.Value)
                {
                    decimal val = decimal.Parse(pctnvkValue.ToString());
                    if (val != 0)
                    {
                        item[PhuCap.PCTNVK_HS] = Math.Round(val, 2);
                    }
                    else
                    {
                        item[PhuCap.PCTNVK_HS] = DBNull.Value;
                    }
                }

                var hsblValue = item[PhuCap.HSBL_HS];
                if (hsblValue != DBNull.Value)
                {
                    decimal val = decimal.Parse(hsblValue.ToString());
                    if (val != 0)
                    {
                        item[PhuCap.HSBL_HS] = Math.Round(val, 2);
                    }
                    else
                    {
                        item[PhuCap.HSBL_HS] = DBNull.Value;
                    }
                }
            }

            return data;
        }

        public DataTable GetDataReportGiaiThichLuongChiTiet(List<TlDmDonViNq104> lstDonVi, int thang, int nam, string maCachTl, int donViTinh, bool isSummary)
        {
            string maDonVi = string.Join(",", lstDonVi.Select(x => x.MaDonVi));
            List<string> lstPhuCap = new List<string>()
            {
                "LBLCVCDHT_TT",
                "NLCVCDHT_TT",
                "BLCVCDHT_TT",
                "LCVCDHT_TT",
                "CVCDHT_TT",
                "BLCBHT_TT",
                "LBLCBHT_TT",
                "NLCBHT_TT",
                "LCBHT_TT",
                "TLCB_TT",
                "TNLCB_TT",
                "TLBLCB_TT",
                "TLCV_CD_TT",
                "TNLCV_CD_TT",
                "TLBLCV_CD_TT",
                "PCTNVK_TT",
                "PCTN_TT",
                "PCKV_TT",
                "PCKHAC_SUM",
                "TRUYLINHKHAC_SUM",
                "LUONGTHANG_SUM",
                "BHXHCN_TT",
                "BHYTCN_TT",
                "BHTNCN_TT",
                "THUETNCN_TT",
                "TA_TT",
                "GTKHAC_TT",
                "PHAITRU_SUM",
                "THANHTIEN",
            };
            string maPhuCap = string.Join(",", lstPhuCap);
            string maPhuCapCount = string.Join(",", lstPhuCap.Select(x => string.Format("COUNT_{0}", x)));
            return _tlBangLuongThangRepository.ReportGiaiThichLuongChiTiet(maDonVi, thang, nam, maCachTl, maPhuCap, maPhuCapCount, donViTinh, isSummary);
        }

        public DataTable ReportGiaiThichLuongChiTiet(TlDmDonViNq104 donVi, DataTable data, string maCachTl)
        {
            var subData = data.Select(string.Format("MaDonVi='{0}'", donVi.MaDonVi));
            if (subData.Any())
            {
                return ReportGiaiThichLuongChiTiet(subData.CopyToDataTable(), maCachTl);
            }
            return ReportGiaiThichLuongChiTiet(data.Clone(), maCachTl);
        }

        public DataTable ReportGiaiThichLuongChiTiet(DataTable data, string maCachTl)
        {
            DataTable rs = new DataTable();
            if (data != null && data.Rows.Count > 0)
            {
                var dsNgach = new Dictionary<string, string>()
                {
                    { "1", "Sĩ quan"},
                    { "2", "QNCN"},
                    { "3", "Công nhân viên chức quốc phòng"},
                    { "4", "HSQ - CS"}
                };

                rs = data.Clone();
                rs.Columns.Add(ExportColumnHeader.IS_PARENT, typeof(bool));
                rs.Columns.Add(ExportColumnHeader.IS_HEADER, typeof(bool));
                rs.Columns.Add(ExportColumnHeader.IS_TOTAL, typeof(bool));
                rs.Columns.Add(ExportColumnHeader.DOI_TUONG);

                DataRow rowParent = rs.NewRow();
                rowParent[ExportColumnHeader.IS_PARENT] = true;
                rowParent[ExportColumnHeader.IS_HEADER] = true;
                rowParent[ExportColumnHeader.IS_TOTAL] = false;
                rowParent[ExportColumnHeader.DOI_TUONG] = CachTinhLuong.CACH0.Equals(maCachTl) ? "I. Tổng hợp tiền lương tháng" : "II. Tổng hợp tiền lương truy lĩnh";
                rs.Rows.Add(rowParent);

                var columns = data.Columns;
                foreach (var item in dsNgach)
                {
                    string filter = string.Format("{0} LIKE '{1}%'", ExportColumnHeader.MA_CAP_BAC_PARENT, item.Key);
                    var subData = data.Select(filter);
                    if (subData.Any())
                    {
                        // Header row
                        rowParent = rs.NewRow();
                        rowParent[ExportColumnHeader.IS_PARENT] = true;
                        rowParent[ExportColumnHeader.IS_HEADER] = true;
                        rowParent[ExportColumnHeader.IS_TOTAL] = false;
                        rowParent[ExportColumnHeader.DOI_TUONG] = item.Value;
                        rs.Rows.Add(rowParent);

                        // Detail rows
                        for (int i = 0; i < subData.Length; i++)
                        {
                            var rowData = subData[i];
                            var rowDetail = rs.NewRow();

                            string maCapBacParent = rowData[ExportColumnHeader.MA_CAP_BAC_PARENT].ToString();
                            foreach (DataColumn column in columns)
                            {
                                var columnName = column.ColumnName;
                                if (columnName.Equals(ExportColumnHeader.MA_CAP_BAC) && (maCapBacParent.StartsWith("2") || maCapBacParent.StartsWith("4")))
                                {
                                    rowDetail[columnName] = string.Empty;
                                }
                                else
                                {
                                    rowDetail[columnName] = rowData[columnName];
                                }
                            }
                            rs.Rows.Add(rowDetail);
                        }

                        // Total row
                        DataRow rowTotal = rs.NewRow();
                        rowTotal[ExportColumnHeader.IS_PARENT] = true;
                        rowTotal[ExportColumnHeader.IS_HEADER] = false;
                        rowTotal[ExportColumnHeader.IS_TOTAL] = true;
                        rowTotal[ExportColumnHeader.DOI_TUONG] = "+";
                        rowTotal["CachTl"] = maCachTl;

                        foreach (DataColumn column in columns)
                        {
                            string columnName = column.ColumnName;
                            if (column.DataType == typeof(decimal) || column.DataType == typeof(int))
                            {
                                string sumExpression = string.Format("SUM({0})", columnName);
                                var value = data.Compute(sumExpression, filter);
                                if (value != DBNull.Value)
                                {
                                    rowTotal[columnName] = decimal.Parse(value.ToString());
                                }
                            }
                        }
                        rs.Rows.Add(rowTotal);
                    }
                }
            }
            return rs;
        }

        public DataTable ReportInKiem(int nam, int thang, string maDonVi, int donViTinh)
        {
            string thangNam = nam.ToString() + thang.ToString("D2");
            var listData = _tlBaoCaoRepository.FindCanBoPhuCapInKiem(thangNam, maDonVi);
            List<string> listMaCbo = listData.Select(item => item.MaCbo).Distinct().ToList();
            List<TlCanBoPhuCapNq104> listDataPhuCap = new List<TlCanBoPhuCapNq104>();

            var predicate = PredicateBuilder.True<TlDmCanBoNq104>();
            predicate = predicate.And(x => x.Thang == thang);
            predicate = predicate.And(x => x.Nam == nam);
            predicate = predicate.And(x => maDonVi.Equals(x.Parent));
            predicate = predicate.And(x => x.ITrangThai == 1 || x.ITrangThai == 3);
            var listCanBoDelete = _tlDmCanboRepository.FindAll(predicate).OrderBy(x => x.ITrangThai).ToList();

            if (listMaCbo.Count > 0)
            {
                listDataPhuCap = _tlCanBoPhuCapRepository.FindByLstMaCanBo(listMaCbo).ToList();
            }
            else if (listMaCbo.Count == 0 && listCanBoDelete.Count > 0)
            {
                listDataPhuCap = _tlCanBoPhuCapRepository.FindByLstMaCanBo(listCanBoDelete.Select(x => x.MaCanBo).Distinct().ToList()).ToList();
            }

            var listPc = listDataPhuCap.Select(x => x.MaPhuCap).Distinct().ToList();

            DataTable reslut = new DataTable();
            reslut.Columns.Add(ExportColumnHeader.STT);
            reslut.Columns.Add(ExportColumnHeader.STK);
            reslut.Columns.Add(ExportColumnHeader.TEN_CAN_BO);
            reslut.Columns.Add(ExportColumnHeader.CAP_BAC);
            reslut.Columns.Add(ExportColumnHeader.MA_CAP_BAC);
            reslut.Columns.Add(ExportColumnHeader.NGAY_NHAP_NGU);
            reslut.Columns.Add(ExportColumnHeader.NGAY_XUAT_NGU);
            reslut.Columns.Add(ExportColumnHeader.NGAY_TAI_NGU);
            reslut.Columns.Add(ExportColumnHeader.PARENT);
            reslut.Columns.Add(ExportColumnHeader.LUONG_CO_BAN);
            reslut.Columns.Add(ExportColumnHeader.NOTE);
            reslut.Columns[ExportColumnHeader.LUONG_CO_BAN].DataType = typeof(Decimal);

            if (listPc != null && listPc.Count > 0)
            {
                foreach (var pc in listPc)
                {
                    reslut.Columns.Add(pc);
                    reslut.Columns[pc].DataType = typeof(Decimal);
                }
            }
            else
            {
                reslut.Columns.Add(PhuCap.NTN, typeof(decimal));
            }
            var index = 1;
            foreach (var item in listMaCbo)
            {
                var cbo = _tlDmCanboRepository.FindByMaCanbo(item);
                if (cbo != null)
                {
                    DataRow dataRow = reslut.NewRow();
                    dataRow[ExportColumnHeader.STT] = index++;
                    dataRow[ExportColumnHeader.TEN_CAN_BO] = cbo.TenCanBo;
                    dataRow[ExportColumnHeader.MA_CAP_BAC] = cbo.MaCb;
                    dataRow[ExportColumnHeader.STK] = cbo.SoTaiKhoan + "  " + cbo.MaCanBo;
                    dataRow[ExportColumnHeader.NGAY_NHAP_NGU] = cbo.NgayNn.HasValue ? cbo.NgayNn.Value.ToString("MM/yy") : string.Empty;
                    dataRow[ExportColumnHeader.NGAY_XUAT_NGU] = cbo.NgayXn.HasValue ? cbo.NgayXn.Value.ToString("MM/yy") : string.Empty;
                    dataRow[ExportColumnHeader.NGAY_TAI_NGU] = cbo.NgayTn.HasValue ? cbo.NgayTn.Value.ToString("MM/yy") : string.Empty;
                    dataRow[ExportColumnHeader.PARENT] = cbo.Parent;
                    dataRow[ExportColumnHeader.NOTE] = "Sửa cán bộ";

                    var capBac = _tlDmCapBacRepository.FindByMaCapBac(cbo.MaCb);
                    if (capBac != null)
                    {
                        dataRow[ExportColumnHeader.CAP_BAC] = capBac.Note;
                    }
                    foreach (var pc in listPc)
                    {
                        var data = listDataPhuCap.FirstOrDefault(x => x.MaCbo.Equals(item) && x.MaPhuCap.Equals(pc));
                        if (data != null)
                        {
                            if (data.MaPhuCap == "NTN")
                            {
                                dataRow[pc] = data.GiaTri;
                            }
                            else if (data.MaPhuCap == "LHT_HS")
                            {
                                //if ((bool)data.Flag)

                                dataRow[ExportColumnHeader.LUONG_CO_BAN] = data.GiaTri * listDataPhuCap.FirstOrDefault(x => x.MaCbo == cbo.MaCanBo && x.MaPhuCap == "LCS").GiaTri;
                                dataRow[pc] = data.GiaTri;

                                //else
                                //{
                                //    dataRow[ExportColumnHeader.LUONG_CO_BAN] = 0;
                                //    dataRow[pc] = 0;
                                //}
                            }
                            else if (data.MaPhuCap == "LCS")
                            {
                                //if ((bool)data.Flag)
                                //{
                                dataRow[ExportColumnHeader.LUONG_CO_BAN] = data.GiaTri * listDataPhuCap.FirstOrDefault(x => x.MaCbo == cbo.MaCanBo && x.MaPhuCap == "LHT_HS").GiaTri;
                                dataRow[pc] = data.GiaTri;
                                //}
                                //else
                                //{
                                //    dataRow[ExportColumnHeader.LUONG_CO_BAN] = 0;
                                //    dataRow[pc] = 0;
                                //}
                            }
                            else
                            {
                                //dataRow[pc] = ((bool)data.Flag) ? data.GiaTri : 0;
                            }
                        }
                    }
                    reslut.Rows.Add(dataRow);
                }
            }

            foreach (var cbo in listCanBoDelete)
            {
                DataRow dataRow = reslut.NewRow();
                dataRow[ExportColumnHeader.STT] = index++;
                dataRow[ExportColumnHeader.TEN_CAN_BO] = cbo.TenCanBo;
                dataRow[ExportColumnHeader.MA_CAP_BAC] = cbo.MaCb;
                dataRow[ExportColumnHeader.STK] = cbo.SoTaiKhoan + "  " + cbo.MaCanBo;
                dataRow[ExportColumnHeader.NGAY_NHAP_NGU] = cbo.NgayNn.HasValue ? cbo.NgayNn.Value.ToString("MM/yy") : string.Empty;
                dataRow[ExportColumnHeader.NGAY_XUAT_NGU] = cbo.NgayXn.HasValue ? cbo.NgayXn.Value.ToString("MM/yy") : string.Empty;
                dataRow[ExportColumnHeader.NGAY_TAI_NGU] = cbo.NgayTn.HasValue ? cbo.NgayTn.Value.ToString("MM/yy") : string.Empty;
                dataRow[ExportColumnHeader.PARENT] = cbo.Parent;
                var predicateDelete = PredicateBuilder.True<TlCanBoPhuCapNq104>();
                predicateDelete = predicateDelete.And(x => x.MaCbo.Equals(cbo.MaCanBo));
                //predicateDelete = predicateDelete.And(x => x.MaPhuCap.Equals(PhuCap.NTN));
                var lstData = _tlCanBoPhuCapRepository.FindAll(predicateDelete);
                var NTN = lstData.FirstOrDefault(x => PhuCap.NTN.Equals(x.MaPhuCap)).GiaTri;
                if (cbo.ITrangThai == 3)
                {
                    dataRow[ExportColumnHeader.NOTE] = "Xóa cán bộ";
                }
                else
                {
                    decimal LHT_HS = 0;
                    decimal LCS = 0;
                    dataRow[ExportColumnHeader.NOTE] = "Thêm cán bộ";
                    foreach (var pc in listPc)
                    {
                        var data = lstData.FirstOrDefault(x => x.MaCbo.Equals(cbo.MaCanBo) && x.MaPhuCap.Equals(pc));
                        if (data != null)
                        {
                            dataRow[pc] = data.GiaTri;
                            if (data.MaPhuCap.Equals(PhuCap.LHT_HS))
                            {
                                LHT_HS = (decimal)data.GiaTri;
                                dataRow[pc] = LHT_HS;
                            }
                            else if (data.MaPhuCap.Equals(PhuCap.LCS))
                            {
                                LCS = (decimal)data.GiaTri;
                                dataRow[pc] = LCS;
                            }
                            else if (data.MaPhuCap.Contains("_HS"))
                            {
                                dataRow[pc] = data.GiaTri;
                            }
                        }
                    }
                    dataRow[ExportColumnHeader.LUONG_CO_BAN] = Math.Ceiling(LHT_HS * LCS);
                }
                if (NTN == null)
                {
                    dataRow[PhuCap.NTN] = 0;
                }
                else
                {
                    dataRow[PhuCap.NTN] = (decimal)NTN;
                }
                reslut.Rows.Add(dataRow);
            }
            return reslut;
        }

        public DataTable GetDataLuongThang(Guid id)
        {
            return _tlBangLuongThangRepository.GetDataLuongThang(id);
        }

        public IEnumerable<TlBangLuongThangNq104> FindAll()
        {
            return _tlBangLuongThangRepository.FindAll();
        }

        public DataTable ReportQtQuanSo(Expression<Func<TlQsChungTuChiTietNq104, bool>> predicate, List<TlDmDonViNq104> list)
        {
            var listData = _tlQsChungTuChiTietRepository.FindAll(predicate);
            var listCapBac = _tlDmCapBacRepository.FindAll().Where(x => x.Parent != null).ToList();
            DataTable result = new DataTable();
            result.Columns.Add(ExportColumnHeader.DON_VI);
            result.Columns.Add(TEN_CAP_BAC.THIEU_UY);
            result.Columns[TEN_CAP_BAC.THIEU_UY].DataType = typeof(Decimal);
            result.Columns.Add(TEN_CAP_BAC.TRUNG_UY);
            result.Columns[TEN_CAP_BAC.TRUNG_UY].DataType = typeof(Decimal);
            result.Columns.Add(TEN_CAP_BAC.THUONG_UY);
            result.Columns[TEN_CAP_BAC.THUONG_UY].DataType = typeof(Decimal);
            result.Columns.Add(TEN_CAP_BAC.DAI_UY);
            result.Columns[TEN_CAP_BAC.DAI_UY].DataType = typeof(Decimal);
            result.Columns.Add(TEN_CAP_BAC.THIEU_TA);
            result.Columns[TEN_CAP_BAC.THIEU_TA].DataType = typeof(Decimal);
            result.Columns.Add(TEN_CAP_BAC.TRUNG_TA);
            result.Columns[TEN_CAP_BAC.TRUNG_TA].DataType = typeof(Decimal);
            result.Columns.Add(TEN_CAP_BAC.THUONG_TA);
            result.Columns[TEN_CAP_BAC.THUONG_TA].DataType = typeof(Decimal);
            result.Columns.Add(TEN_CAP_BAC.DAI_TA);
            result.Columns[TEN_CAP_BAC.DAI_TA].DataType = typeof(Decimal);
            result.Columns.Add(TEN_CAP_BAC.TUONG);
            result.Columns[TEN_CAP_BAC.TUONG].DataType = typeof(Decimal);
            result.Columns.Add(TEN_CAP_BAC.BINH_NHI);
            result.Columns[TEN_CAP_BAC.BINH_NHI].DataType = typeof(Decimal);
            result.Columns.Add(TEN_CAP_BAC.BINH_NHAT);
            result.Columns[TEN_CAP_BAC.BINH_NHAT].DataType = typeof(Decimal);
            result.Columns.Add(TEN_CAP_BAC.HA_SI);
            result.Columns[TEN_CAP_BAC.HA_SI].DataType = typeof(Decimal);
            result.Columns.Add(TEN_CAP_BAC.TRUNG_SI);
            result.Columns[TEN_CAP_BAC.TRUNG_SI].DataType = typeof(Decimal);
            result.Columns.Add(TEN_CAP_BAC.THUONG_SI);
            result.Columns[TEN_CAP_BAC.THUONG_SI].DataType = typeof(Decimal);
            result.Columns.Add(TEN_CAP_BAC.QNCN1);
            result.Columns[TEN_CAP_BAC.QNCN1].DataType = typeof(Decimal);
            result.Columns.Add(TEN_CAP_BAC.VCQP);
            result.Columns[TEN_CAP_BAC.VCQP].DataType = typeof(Decimal);
            result.Columns.Add(TEN_CAP_BAC.CCQP);
            result.Columns[TEN_CAP_BAC.CCQP].DataType = typeof(Decimal);
            result.Columns.Add(TEN_CAP_BAC.CNQP);
            result.Columns[TEN_CAP_BAC.CNQP].DataType = typeof(Decimal);
            result.Columns.Add(TEN_CAP_BAC.LDHD);
            result.Columns[TEN_CAP_BAC.LDHD].DataType = typeof(Decimal);
            result.Columns.Add(TEN_CAP_BAC.THIEU_UY_CN);
            result.Columns[TEN_CAP_BAC.THIEU_UY_CN].DataType = typeof(Decimal);
            result.Columns.Add(TEN_CAP_BAC.TRUNG_UY_CN);
            result.Columns[TEN_CAP_BAC.TRUNG_UY_CN].DataType = typeof(Decimal);
            result.Columns.Add(TEN_CAP_BAC.DAI_UY_CN);
            result.Columns[TEN_CAP_BAC.DAI_UY_CN].DataType = typeof(Decimal);
            result.Columns.Add(TEN_CAP_BAC.THIEU_TA_CN);
            result.Columns[TEN_CAP_BAC.THIEU_TA_CN].DataType = typeof(Decimal);
            result.Columns.Add(TEN_CAP_BAC.TRUNG_TA_CN);
            result.Columns[TEN_CAP_BAC.TRUNG_TA_CN].DataType = typeof(Decimal);
            result.Columns.Add(TEN_CAP_BAC.THUONG_TA_CN);
            result.Columns[TEN_CAP_BAC.THUONG_TA_CN].DataType = typeof(Decimal);

            foreach (var donvi in list)
            {
                var listDataDonVi = listData.Where(x => x.IdDonVi == donvi.MaDonVi).FirstOrDefault();
                if (listDataDonVi == null)
                    continue;
                DataRow dataRow = result.NewRow();
                dataRow[ExportColumnHeader.DON_VI] = donvi.TenDonVi;
                dataRow[TEN_CAP_BAC.THIEU_UY] = listDataDonVi.ThieuUy;
                dataRow[TEN_CAP_BAC.TRUNG_UY] = listDataDonVi.TrungUy;
                dataRow[TEN_CAP_BAC.THUONG_UY] = listDataDonVi.ThuongUy;
                dataRow[TEN_CAP_BAC.DAI_UY] = listDataDonVi.DaiUy;
                dataRow[TEN_CAP_BAC.THIEU_TA] = listDataDonVi.ThieuTa;
                dataRow[TEN_CAP_BAC.TRUNG_TA] = listDataDonVi.TrungTa;
                dataRow[TEN_CAP_BAC.THUONG_TA] = listDataDonVi.ThuongTa;
                dataRow[TEN_CAP_BAC.DAI_TA] = listDataDonVi.DaiTa;
                dataRow[TEN_CAP_BAC.TUONG] = listDataDonVi.Tuong;
                dataRow[TEN_CAP_BAC.BINH_NHI] = listDataDonVi.BinhNhi;
                dataRow[TEN_CAP_BAC.BINH_NHAT] = listDataDonVi.BinhNhat;
                dataRow[TEN_CAP_BAC.HA_SI] = listDataDonVi.HaSi;
                dataRow[TEN_CAP_BAC.TRUNG_SI] = listDataDonVi.TrungSi;
                dataRow[TEN_CAP_BAC.THUONG_SI] = listDataDonVi.ThuongSi;
                dataRow[TEN_CAP_BAC.QNCN1] = listDataDonVi.Qncn;
                dataRow[TEN_CAP_BAC.VCQP] = listDataDonVi.Vcqp;
                dataRow[TEN_CAP_BAC.CNQP] = listDataDonVi.Cnqp;
                dataRow[TEN_CAP_BAC.CCQP] = listDataDonVi.Ccqp ?? 0;
                dataRow[TEN_CAP_BAC.LDHD] = listDataDonVi.Ldhd;
                dataRow[TEN_CAP_BAC.THIEU_UY_CN] = listDataDonVi.ThieuUyCn;
                dataRow[TEN_CAP_BAC.TRUNG_UY_CN] = listDataDonVi.TrungUyCn;
                dataRow[TEN_CAP_BAC.THUONG_UY_CN] = listDataDonVi.ThuongUyCn;
                dataRow[TEN_CAP_BAC.DAI_UY_CN] = listDataDonVi.DaiUyCn;
                dataRow[TEN_CAP_BAC.THIEU_TA_CN] = listDataDonVi.DaiUyCn;
                dataRow[TEN_CAP_BAC.TRUNG_TA_CN] = listDataDonVi.TrungTaCn;
                dataRow[TEN_CAP_BAC.THUONG_TA_CN] = listDataDonVi.ThuongTaCn;
                result.Rows.Add(dataRow);
            }
            return result;
        }

        public List<TlQsChungTuChiTietNq104> FindChungTuChiTiet(Expression<Func<TlQsChungTuNq104, bool>> predicate)
        {
            var data = _tlQsChungTuRepository.FindAll(predicate).FirstOrDefault();
            List<TlQsChungTuChiTietNq104> list = new List<TlQsChungTuChiTietNq104>();
            if (data != null)
            {
                list = _tlQsChungTuChiTietRepository.FindAll(x => data.Id.ToString().Equals(x.IdChungTu)).ToList();
            }
            return list;
        }

        public IEnumerable<TlQsChungTuChiTietNq104> FindQuyetToanQuanSo(string idDonVi, string thang, int nam, string thangTruoc, int namTruoc)
        {
            return _tlQsChungTuChiTietRepository.FindQuyetToanQuanSo(idDonVi, thang, nam, thangTruoc, namTruoc);
        }

        public List<TlQsChungTuChiTietNq104> FindChungTuChiTietSummary(Expression<Func<TlQsChungTuNq104, bool>> predicate, List<String> listDonVi)
        {
            var data = _tlQsChungTuRepository.FindAll(predicate).Where(x => listDonVi.Contains(x.MaDonVi));
            List<TlQsChungTuChiTietNq104> chungTuChiTiet = new List<TlQsChungTuChiTietNq104>();
            if (data != null && data.Count() > 0)
            {
                foreach (var item in data)
                {
                    var list = _tlQsChungTuChiTietRepository.FindAll(x => item.Id.ToString().Equals(x.IdChungTu)).ToList();
                    chungTuChiTiet.AddRange(list);
                }
            }

            var listMucLuc = chungTuChiTiet.Select(x => x.XauNoiMa).Distinct();

            List<TlQsChungTuChiTietNq104> chungTuChiTietTongHop = new List<TlQsChungTuChiTietNq104>();
            foreach (var mucLuc in listMucLuc)
            {
                TlQsChungTuChiTietNq104 model = new TlQsChungTuChiTietNq104();
                model.XauNoiMa = mucLuc;
                model.MoTa = chungTuChiTiet.Where(x => x.XauNoiMa.Equals(mucLuc)).FirstOrDefault().MoTa;
                model.MlnsIdParent = chungTuChiTiet.Where(x => x.XauNoiMa.Equals(mucLuc)).FirstOrDefault().MlnsIdParent;
                var ChungTuChiTietMucLuc = chungTuChiTiet.Where(x => mucLuc.Equals(x.XauNoiMa));

                model.ThieuUy = ChungTuChiTietMucLuc.Where(x => mucLuc.Equals(x.XauNoiMa)).Sum(x => x.ThieuUy);
                model.TrungUy = ChungTuChiTietMucLuc.Where(x => mucLuc.Equals(x.XauNoiMa)).Sum(x => x.TrungUy);
                model.ThuongUy = ChungTuChiTietMucLuc.Where(x => mucLuc.Equals(x.XauNoiMa)).Sum(x => x.ThuongUy);
                model.DaiUy = ChungTuChiTietMucLuc.Where(x => mucLuc.Equals(x.XauNoiMa)).Sum(x => x.DaiUy);
                model.ThieuTa = ChungTuChiTietMucLuc.Where(x => mucLuc.Equals(x.XauNoiMa)).Sum(x => x.ThieuTa);
                model.TrungTa = ChungTuChiTietMucLuc.Where(x => mucLuc.Equals(x.XauNoiMa)).Sum(x => x.TrungTa);
                model.ThuongTa = ChungTuChiTietMucLuc.Where(x => mucLuc.Equals(x.XauNoiMa)).Sum(x => x.ThuongTa);
                model.DaiTa = ChungTuChiTietMucLuc.Where(x => mucLuc.Equals(x.XauNoiMa)).Sum(x => x.DaiTa);
                model.BinhNhi = ChungTuChiTietMucLuc.Where(x => mucLuc.Equals(x.XauNoiMa)).Sum(x => x.BinhNhi);
                model.BinhNhat = ChungTuChiTietMucLuc.Where(x => mucLuc.Equals(x.XauNoiMa)).Sum(x => x.BinhNhat);
                model.HaSi = ChungTuChiTietMucLuc.Where(x => mucLuc.Equals(x.XauNoiMa)).Sum(x => x.HaSi);
                model.TrungSi = ChungTuChiTietMucLuc.Where(x => mucLuc.Equals(x.XauNoiMa)).Sum(x => x.TrungSi);
                model.ThuongSi = ChungTuChiTietMucLuc.Where(x => mucLuc.Equals(x.XauNoiMa)).Sum(x => x.ThuongSi);
                model.Tuong = ChungTuChiTietMucLuc.Where(x => mucLuc.Equals(x.XauNoiMa)).Sum(x => x.Tuong);
                model.ThieuUyCn = ChungTuChiTietMucLuc.Where(x => mucLuc.Equals(x.XauNoiMa)).Sum(x => x.ThieuUyCn);
                model.TrungUyCn = ChungTuChiTietMucLuc.Where(x => mucLuc.Equals(x.XauNoiMa)).Sum(x => x.TrungUyCn);
                model.ThuongUyCn = ChungTuChiTietMucLuc.Where(x => mucLuc.Equals(x.XauNoiMa)).Sum(x => x.ThuongUyCn);
                model.DaiUyCn = ChungTuChiTietMucLuc.Where(x => mucLuc.Equals(x.XauNoiMa)).Sum(x => x.DaiUyCn);
                model.ThieuTaCn = ChungTuChiTietMucLuc.Where(x => mucLuc.Equals(x.XauNoiMa)).Sum(x => x.ThieuTaCn);
                model.TrungTaCn = ChungTuChiTietMucLuc.Where(x => mucLuc.Equals(x.XauNoiMa)).Sum(x => x.TrungTaCn);
                model.ThuongTaCn = ChungTuChiTietMucLuc.Where(x => mucLuc.Equals(x.XauNoiMa)).Sum(x => x.ThuongTaCn);
                model.Qncn = ChungTuChiTietMucLuc.Where(x => mucLuc.Equals(x.XauNoiMa)).Sum(x => x.Qncn);
                model.Vcqp = ChungTuChiTietMucLuc.Where(x => mucLuc.Equals(x.XauNoiMa)).Sum(x => x.Vcqp);
                model.Ldhd = ChungTuChiTietMucLuc.Where(x => mucLuc.Equals(x.XauNoiMa)).Sum(x => x.Ldhd);
                model.Cnqp = ChungTuChiTietMucLuc.Where(x => mucLuc.Equals(x.XauNoiMa)).Sum(x => x.Cnqp);
                ;
                model.TongSo = ChungTuChiTietMucLuc.Where(x => mucLuc.Equals(x.XauNoiMa)).Sum(x => x.TongSo);

                chungTuChiTietTongHop.Add(model);
            }

            return chungTuChiTietTongHop;
        }

        public DataTable ExportTongHopLuongTheoNgach(int thang, int nam, List<TlDmDonViNq104> listDonVi, string maCachTl, int donViTinh, bool isSummary)
        {
            var lstDonViStr = listDonVi.Select(x => x.MaDonVi);
            var data = _tlBangLuongThangRepository.FindLuongNgachCanBo(thang, nam, string.Join(",", lstDonViStr), maCachTl, donViTinh, isSummary);
            data.Columns.Add(ExportColumnHeader.IS_PARENT, typeof(bool));
            data.Columns.Add("IsHeader", typeof(bool));

            DataRow firstRow = data.NewRow();
            firstRow["TenNgach"] = maCachTl == CachTinhLuong.CACH0 ? "I. Tổng hợp tiền lương tháng" : "II. Tổng hợp tiền lương truy lĩnh";
            firstRow[ExportColumnHeader.IS_PARENT] = true;
            firstRow["IsHeader"] = true;
            data.Rows.InsertAt(firstRow, 0);
            return data;
        }

        public DataTable ExportTongHopLuongTheoDonVi(int thang, int nam, List<TlDmDonViNq104> listDonVi, string maCachTl, int donViTinh)
        {
            var lstDonViStr = listDonVi.Select(x => x.MaDonVi);
            var data = _tlBangLuongThangRepository.FindLuongDonViCanBo(thang, nam, string.Join(",", lstDonViStr), maCachTl, donViTinh);
            data.Columns.Add(ExportColumnHeader.IS_PARENT, typeof(bool));
            data.Columns.Add("IsHeader", typeof(bool));
            DataRow firstRow = data.NewRow();
            firstRow["DoiTuong"] = maCachTl == CachTinhLuong.CACH0 ? "I. Tổng hợp tiền lương tháng" : "II. Tổng hợp tiền lương truy lĩnh";
            firstRow[ExportColumnHeader.IS_PARENT] = true;
            firstRow["IsHeader"] = true;
            data.Rows.InsertAt(firstRow, 0);
            return data;
        }

        public DataTable ExportTongHopLuongTheoNgachDonVi(int thang, int nam, List<TlDmDonViNq104> listDonVi, string maCachTl, int donViTinh)
        {
            var lstDonViStr = listDonVi.Select(x => x.MaDonVi);
            var data = _tlBangLuongThangRepository.FindLuongNgachDonViCanBo(thang, nam, string.Join(",", lstDonViStr), maCachTl, donViTinh);
            var dsNgach = new Dictionary<string, string>()
            {
                { "1", "Sĩ quan"},
                { "2", "QNCN"},
                { "3", "Công nhân viên chức quốc phòng"},
                { "4", "HSQ - CS"}
            };

            data.Columns.Add("IsHeader", typeof(bool));

            DataTable items = data.Clone();

            if (data.Rows.Count != 0)
            {
                var firstRow = items.NewRow();
                firstRow["DoiTuong"] = maCachTl == CachTinhLuong.CACH0 ? "I. Tổng hợp tiền lương tháng" : "II. Tổng hợp tiền lương truy lĩnh";
                firstRow[ExportColumnHeader.IS_PARENT] = true;
                firstRow["IsHeader"] = true;
                items.Rows.Add(firstRow);
            }

            foreach (var item in dsNgach)
            {

                DataRow dataRow = items.NewRow();
                dataRow["DoiTuong"] = item.Value;
                dataRow[ExportColumnHeader.IS_PARENT] = true;

                var rowNgach = data.AsEnumerable().Where(x => x.Field<string>("Ngach").Equals(item.Key));

                dataRow[ExportColumnHeader.SO_NGUOI] = rowNgach.Sum(x => x.Field<int>(ExportColumnHeader.SO_NGUOI));
                dataRow[PhuCap.LHT_TT] = rowNgach.Sum(x => x.Field<decimal?>(PhuCap.LHT_TT));
                dataRow[PhuCap.HSBL_TT] = rowNgach.Sum(x => x.Field<decimal?>(PhuCap.HSBL_TT));
                dataRow[PhuCap.PCTNVK_TT] = rowNgach.Sum(x => x.Field<decimal?>(PhuCap.PCTNVK_TT));
                dataRow[PhuCap.PCCV_TT] = rowNgach.Sum(x => x.Field<decimal?>(PhuCap.PCCV_TT));
                dataRow[PhuCap.PCTN_TT] = rowNgach.Sum(x => x.Field<decimal?>(PhuCap.PCTN_TT));
                dataRow[PhuCap.PCKV_TT] = rowNgach.Sum(x => x.Field<decimal?>(PhuCap.PCKV_TT));
                dataRow[PhuCap.PCCOV_TT] = rowNgach.Sum(x => x.Field<decimal?>(PhuCap.PCCOV_TT));
                dataRow[PhuCap.PCDACTHU_SUM] = rowNgach.Sum(x => x.Field<decimal?>(PhuCap.PCDACTHU_SUM));
                dataRow[PhuCap.PCTRA_SUM] = rowNgach.Sum(x => x.Field<decimal?>(PhuCap.PCTRA_SUM));
                dataRow[PhuCap.PCKHAC_SUM] = rowNgach.Sum(x => x.Field<decimal?>(PhuCap.PCKHAC_SUM));
                dataRow[PhuCap.LUONGTHANG_SUM] = rowNgach.Sum(x => x.Field<decimal?>(PhuCap.LUONGTHANG_SUM));
                dataRow[PhuCap.BHXHCN_TT] = rowNgach.Sum(x => x.Field<decimal?>(PhuCap.BHXHCN_TT));
                dataRow[PhuCap.BHYTCN_TT] = rowNgach.Sum(x => x.Field<decimal?>(PhuCap.BHYTCN_TT));
                dataRow[PhuCap.BHTNCN_TT] = rowNgach.Sum(x => x.Field<decimal?>(PhuCap.BHTNCN_TT));
                dataRow[PhuCap.TA_TONG] = rowNgach.Sum(x => x.Field<decimal?>(PhuCap.TA_TONG));
                dataRow[PhuCap.THUETNCN_TT] = rowNgach.Sum(x => x.Field<decimal?>(PhuCap.THUETNCN_TT));
                dataRow[PhuCap.TRICHLUONG_TT] = rowNgach.Sum(x => x.Field<decimal?>(PhuCap.TRICHLUONG_TT));
                dataRow[PhuCap.GTKHAC_TT] = rowNgach.Sum(x => x.Field<decimal?>(PhuCap.GTKHAC_TT));
                dataRow[PhuCap.PHAITRU_SUM] = rowNgach.Sum(x => x.Field<decimal?>(PhuCap.PHAITRU_SUM));
                dataRow[PhuCap.THANHTIEN] = rowNgach.Sum(x => x.Field<decimal?>(PhuCap.THANHTIEN));
                dataRow[PhuCap.THANHTIEN_NH] = rowNgach.Sum(x => x.Field<decimal?>(PhuCap.THANHTIEN_NH));

                if (rowNgach.Any())
                {
                    items.Rows.Add(dataRow);
                    foreach (DataRow item1 in rowNgach)
                    {
                        items.Rows.Add(item1.ItemArray);
                    }
                }
            }

            return items;
        }

        public DataTable ExportGiaiThichPhaiTru(string maCanBo, string maPhuCap)
        {
            return _tlBangLuongThangRepository.ExportGiaiThichPhaiTru(maCanBo, maPhuCap);
        }

        public IEnumerable<TlRptDienBienLuongNq104Query> GetDataBangLuong()
        {
            return _tlBangLuongThangRepository.GetDataBangLuong();
        }

        public void BulkInsert(IEnumerable<TlBangLuongThangNq104> entities)
        {
            _tlBangLuongThangRepository.BulkInsert(entities);
        }

        public List<TlRptTruyLinhChuyenCheDoNq104Query> ExportTruyLinhChuyenCheDo(Expression<Func<TlDmCanBoNq104, bool>> predicate, string maDonVi, int thang, int nam, bool isOrderChucVu)
        {
            var listCanBo = _tlDmCanboRepository.FindAll(predicate).ToList();
            string maHieuCanBo = string.Join(",", listCanBo.Select(x => x.MaHieuCanBo).Distinct());
            int thangTruoc = 0;
            int namTruoc = 0;
            if (thang == 1)
            {
                thangTruoc = 12;
                namTruoc = nam - 1;
            }
            else
            {
                thangTruoc = thang - 1;
                namTruoc = nam;
            }
            return _tlBangLuongThangRepository.ReportTruyLinhChuyenCheDo(maDonVi, thangTruoc, namTruoc, thang, nam, maHieuCanBo, isOrderChucVu).ToList();
        }

        public DataTable ExportBaoCaoTienAn(Expression<Func<TlBangLuongThangNq104, bool>> predicate, List<TlDmDonViNq104> listDonVi, int donViTinh)
        {
            var listData = _tlBangLuongThangRepository.FindAll(predicate);
            List<string> listPcTienAn = new List<string> { PhuCap.TA_BB_DG, PhuCap.TA_DOCHAI_DG, PhuCap.TA_TRUCQY_DG1, PhuCap.TA_TRUCQY_DG2,
                PhuCap.TA_TRUCQY_DG3, PhuCap.TA_TRUCQY_DG4, PhuCap.TA_OM_DG, PhuCap.TA_TRUCTRAI_DG };
            var listDataTa = listData.Where(x => listPcTienAn.Contains(x.MaPhuCap) && (x.GiaTri != null || x.GiaTri != 0));
            var listMaCanBo = listData.Select(x => x.MaCbo).Distinct().ToList();
            string maCanBo = string.Join(",", listMaCanBo);
            string maPhuCap = string.Join(",", listPcTienAn);
            var data = _tlBaoCaoRepository.ReportBaoCaoTienAn(maCanBo, maPhuCap).ToList();

            DataTable reslut = new DataTable();
            reslut.Columns.Add(ExportColumnHeader.TIEN_AN);
            reslut.Columns.Add(ExportColumnHeader.DINH_MUC, typeof(decimal));
            reslut.Columns.Add(ExportColumnHeader.SO_NGUOI, typeof(decimal));
            reslut.Columns.Add(ExportColumnHeader.SO_NGAY, typeof(decimal));
            reslut.Columns.Add(ExportColumnHeader.DV_TINH);
            reslut.Columns.Add(ExportColumnHeader.THANH_TIEN, typeof(decimal));
            reslut.Columns.Add(ExportColumnHeader.IS_PARENT, typeof(bool));
            reslut.Columns.Add(ExportColumnHeader.NHAN);
            reslut.Columns.Add(ExportColumnHeader.BANG);
            Dictionary<string, string> tieuDePc = new Dictionary<string, string>()
            {
                {PhuCap.TA_BB_DG, "TIỀN ĂN BỘ BINH"},
                {PhuCap.TA_TRUCTRAI_DG, "TIỀN ĂN TRỰC ĐÊM DOANH TRẠI"},
                {PhuCap.TA_DOCHAI_DG, "TIỀN ĂN BỒI DƯỠNG ĐỘC HẠI"},
                {PhuCap.TA_OM_DG, "TIỀN ĂN ỐM TẠI TRẠI"},
                {PhuCap.TA_TRUCQY, "TIỀN TRỰC QUÂN Y"}
            };

            foreach (var pc in tieuDePc)
            {
                int tongThanhTien = 0;
                int tongSoNgay = 0;
                var dataPc = data.Where(x => x.MaPhuCap.StartsWith(pc.Key));
                DataRow dataRow = reslut.NewRow();
                var tongCong = dataPc.Count();
                if (tongCong > 0)
                {
                    dataRow[ExportColumnHeader.TIEN_AN] = pc.Value;
                    dataRow[ExportColumnHeader.IS_PARENT] = true;
                    reslut.Rows.Add(dataRow);
                    foreach (var donVi in listDonVi)
                    {
                        DataRow dataRowDvCon = reslut.NewRow();
                        if (pc.Key.Equals(PhuCap.TA_BB_DG) || pc.Key.Equals(PhuCap.TA_TRUCTRAI_DG) || pc.Key.Equals(PhuCap.TA_OM_DG) || pc.Key.Equals(PhuCap.TA_DOCHAI_DG))
                        {
                            if (dataPc.Count(x => x.MaDonVi.Equals(donVi.MaDonVi)) > 0)
                            {
                                dataRowDvCon[ExportColumnHeader.TIEN_AN] = donVi.TenDonVi;
                                dataRowDvCon[ExportColumnHeader.DINH_MUC] = dataPc.FirstOrDefault().GiaTri;
                                dataRowDvCon[ExportColumnHeader.SO_NGUOI] = dataPc.Count(x => x.MaDonVi.Equals(donVi.MaDonVi));
                                if (pc.Key.Equals(PhuCap.TA_DOCHAI_DG) || pc.Key.Equals(PhuCap.TA_OM_DG))
                                {
                                    decimal soNgayOm = (decimal)dataPc.Where(x => x.MaDonVi.Equals(donVi.MaDonVi)).Sum(x => x.SoNgayHuong);
                                    dataRowDvCon[ExportColumnHeader.SO_NGAY] = soNgayOm;
                                    var t = soNgayOm * dataPc.FirstOrDefault().GiaTri;
                                    dataRowDvCon[ExportColumnHeader.THANH_TIEN] = t;
                                    tongThanhTien += (int)t;
                                }
                                else
                                {
                                    var soNgay = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                                    tongSoNgay += soNgay;
                                    dataRowDvCon[ExportColumnHeader.SO_NGAY] = soNgay;
                                    var t = soNgay * dataPc.Count(x => x.MaDonVi.Equals(donVi.MaDonVi)) * dataPc.FirstOrDefault().GiaTri;
                                    tongThanhTien += (int)t;
                                    dataRowDvCon[ExportColumnHeader.THANH_TIEN] = t;
                                }
                                dataRowDvCon[ExportColumnHeader.DV_TINH] = "ngày";
                                dataRowDvCon[ExportColumnHeader.NHAN] = "x";
                                dataRowDvCon[ExportColumnHeader.BANG] = "=";
                                dataRowDvCon[ExportColumnHeader.IS_PARENT] = false;
                                reslut.Rows.Add(dataRowDvCon);

                            }
                        }
                        else
                        {
                            if (dataPc.Count(x => x.MaDonVi.Equals(donVi.MaDonVi)) > 0)
                            {
                                var dinhmuc = dataPc.OrderBy(x => x.GiaTri).Select(x => x.GiaTri).Distinct();
                                int thanhTien = 0;
                                foreach (var item in dinhmuc)
                                {
                                    thanhTien += (int)(dataPc.Where(x => x.MaDonVi.Equals(donVi.MaDonVi) && x.GiaTri == item).Sum(x => x.SoNgayHuong) * item);
                                }
                                dataRowDvCon[ExportColumnHeader.TIEN_AN] = donVi.TenDonVi;
                                dataRowDvCon[ExportColumnHeader.SO_NGUOI] = dataPc.Count(x => x.MaDonVi.Equals(donVi.MaDonVi));
                                dataRowDvCon[ExportColumnHeader.SO_NGAY] = dataPc.Where(x => x.MaDonVi.Equals(donVi.MaDonVi)).Sum(x => x.SoNgayHuong);
                                dataRowDvCon[ExportColumnHeader.THANH_TIEN] = thanhTien;
                                tongThanhTien += thanhTien;
                                dataRowDvCon[ExportColumnHeader.IS_PARENT] = true;
                                reslut.Rows.Add(dataRowDvCon);
                                foreach (var item in dinhmuc)
                                {
                                    DataRow dataRowDvDinhMuc = reslut.NewRow();
                                    if (pc.Key.Equals(PhuCap.TA_TRUCQY))
                                    {
                                        dataRowDvDinhMuc[ExportColumnHeader.TIEN_AN] = dataPc.FirstOrDefault(x => x.GiaTri == item).TenPhuCap;
                                    }
                                    dataRowDvDinhMuc[ExportColumnHeader.DINH_MUC] = item;
                                    dataRowDvDinhMuc[ExportColumnHeader.SO_NGUOI] = dataPc.Count(x => x.MaDonVi.Equals(donVi.MaDonVi) && x.GiaTri == item);
                                    dataRowDvDinhMuc[ExportColumnHeader.SO_NGAY] = dataPc.Where(x => x.MaDonVi.Equals(donVi.MaDonVi) && x.GiaTri == item).Sum(x => x.SoNgayHuong);
                                    dataRowDvDinhMuc[ExportColumnHeader.DV_TINH] = "ngày";
                                    dataRowDvDinhMuc[ExportColumnHeader.NHAN] = "x";
                                    dataRowDvDinhMuc[ExportColumnHeader.BANG] = "=";
                                    dataRowDvDinhMuc[ExportColumnHeader.THANH_TIEN] = dataPc.Where(x => x.MaDonVi.Equals(donVi.MaDonVi) && x.GiaTri == item).Sum(x => x.SoNgayHuong) * item;
                                    dataRowDvDinhMuc[ExportColumnHeader.IS_PARENT] = false;
                                    reslut.Rows.Add(dataRowDvDinhMuc);
                                }
                            }
                        }
                    }
                    DataRow dataRowSum = reslut.NewRow();
                    dataRowSum[ExportColumnHeader.TIEN_AN] = "Tổng cộng";
                    dataRowSum[ExportColumnHeader.SO_NGUOI] = tongCong;
                    if (pc.Key.Equals(PhuCap.TA_BB_DG) || pc.Key.Equals(PhuCap.TA_TRUCTRAI_DG))
                    {
                        dataRowSum[ExportColumnHeader.SO_NGAY] = tongSoNgay;
                    }
                    else
                    {
                        dataRowSum[ExportColumnHeader.SO_NGAY] = dataPc.Sum(x => x.SoNgayHuong);
                    }
                    dataRowSum[ExportColumnHeader.IS_PARENT] = true;
                    dataRowSum[ExportColumnHeader.THANH_TIEN] = tongThanhTien;
                    reslut.Rows.Add(dataRowSum);
                }
            }
            return reslut;
        }

        public DataTable GetDataTienAn(int thang, int nam, string maDonVi)
        {
            return _tlBangLuongThangRepository.ReportTienAn(thang, nam, maDonVi, DateTime.DaysInMonth(nam, thang));
        }

        public DataTable GetDataTienAn(DataTable data, string maDonVi, List<TlDmPhuCapNq104> lstPhuCap)
        {
            var subData = data.Select(string.Format("MaDonVi='{0}'", maDonVi));
            if (subData.Any())
            {
                return ExportBaoCaoTienAn(subData.CopyToDataTable(), lstPhuCap);
            }
            return ExportBaoCaoTienAn(data.Clone(), lstPhuCap);
        }

        public DataTable GetDataBangLuongPhuCapTongHopBienPhong(List<TlDmDonViNq104> lstDonVi, int thang, int nam)
        {
            string maDonVi = lstDonVi.Select(n => n.MaDonVi).Join(",");
            return _tlBangLuongThangRepository.GetDataBangLuongPhuCapTongHopBienPhong(maDonVi, thang, nam);
        }

        public DataTable ExportBaoCaoTienAn(DataTable data, List<TlDmPhuCapNq104> lstPhuCap)
        {
            data.Columns.Add(ExportColumnHeader.IS_PARENT, typeof(bool));
            DataTable rs = new DataTable();
            if (data != null && data.Rows.Count > 0)
            {
                rs = data.Clone();
                Dictionary<string, string> tieuDePc = new Dictionary<string, string>();
                //{
                //    {PhuCap.TA_BB_DG, "TIỀN ĂN BỘ BINH"},
                //    {PhuCap.TA_TRUCTRAI_DG, "TIỀN ĂN TRỰC ĐÊM DOANH TRẠI"},
                //    {PhuCap.TA_DOCHAI_DG, "TIỀN ĂN BỒI DƯỠNG ĐỘC HẠI"},
                //    {PhuCap.TA_OM_DG, "TIỀN ĂN ỐM TẠI TRẠI"},
                //    {PhuCap.TA_TRUCQY, "TIỀN TRỰC QUÂN Y"}
                //};

                var dataMaPhuCap = data.AsEnumerable().Select(x => x.Field<string>("MaPhuCap")).Distinct();
                foreach (var maPhuCap in dataMaPhuCap)
                {
                    var tenPhuCap = lstPhuCap.Where(x => x.MaPhuCap.Equals(maPhuCap)).Select(x => x.TenPhuCap).First().ToUpper();
                    tieuDePc.Add(maPhuCap, tenPhuCap);
                }

                foreach (var item in tieuDePc)
                {
                    var rowParent = rs.NewRow();
                    rowParent["TienAn"] = item.Value;
                    rowParent[ExportColumnHeader.IS_PARENT] = true;
                    if (item.Key != PhuCap.TA_TRUCQY)
                    {
                        var dataRows = data.AsEnumerable().Where(x => x.Field<string>("MaPhuCap").StartsWith(item.Key));
                        if (dataRows.Any())
                        {
                            rs.Rows.Add(rowParent);
                            foreach (DataRow item1 in dataRows)
                            {
                                item1["TienAn"] = item1["TenDonVi"];
                                rs.Rows.Add(item1.ItemArray);
                            }

                            var rowTong = rs.NewRow();
                            rowTong["TienAn"] = "Tổng cộng";
                            rowTong[ExportColumnHeader.IS_PARENT] = true;
                            rowTong["SoNguoi"] = dataRows.AsEnumerable().Sum(x => x.Field<decimal>("SoNguoi"));
                            rowTong["SoNgay"] = dataRows.AsEnumerable().Sum(x => x.Field<decimal>("SoNgay"));
                            rowTong["THANHTIEN"] = dataRows.AsEnumerable().Sum(x => x.Field<decimal>("THANHTIEN"));
                            rs.Rows.Add(rowTong);
                        }
                    }
                    else
                    {
                        var dataRows = data.AsEnumerable().Where(x => x.Field<string>("MaPhuCap").StartsWith(item.Key)).OrderBy(x => x.Field<string>("MaPhuCap"));
                        if (dataRows.Any())
                        {
                            rs.Rows.Add(rowParent);

                            var lstDonVi = dataRows.GroupBy(x => x.Field<string>("MaDonVi")).Select(x => x.First()).ToDictionary(x => x.Field<string>("MaDonVi"), x => x.Field<string>("TenDonVi"));
                            foreach (var item2 in lstDonVi)
                            {
                                var dataRowsDonVi = dataRows.Where(x => x.Field<string>("MaDonVi").Equals(item2.Key)).OrderBy(x => x.Field<string>("MaPhuCap"));
                                var rowDonVi = rs.NewRow();
                                rowDonVi["TienAn"] = item2.Value;
                                rowDonVi[ExportColumnHeader.IS_PARENT] = true;
                                if (dataRowsDonVi.Any())
                                {
                                    rs.Rows.Add(rowDonVi);
                                    foreach (DataRow item3 in dataRowsDonVi)
                                    {
                                        rs.Rows.Add(item3.ItemArray);
                                    }
                                }
                            }

                            var rowTong = rs.NewRow();
                            rowTong["TienAn"] = "Tổng cộng";
                            rowTong[ExportColumnHeader.IS_PARENT] = true;
                            rowTong["SoNguoi"] = dataRows.AsEnumerable().Sum(x => x.Field<decimal>("SoNguoi"));
                            rowTong["SoNgay"] = dataRows.AsEnumerable().Sum(x => x.Field<decimal>("SoNgay"));
                            rowTong["THANHTIEN"] = dataRows.AsEnumerable().Sum(x => x.Field<decimal>("THANHTIEN"));
                            rs.Rows.Add(rowTong);
                        }
                    }
                }
            }

            return rs;
        }

        public IEnumerable<TlBangLuongThangDongNq104Query> ReportBangLuongThangDong(string maDonVi, string ngach, string maPhuCap, int thang, int nam)
        {
            return _tlBangLuongThangRepository.ReportBangLuongThangDong(maDonVi, ngach, maPhuCap, thang, nam);
        }

        public DataTable ReportBangLuongThangDoc(string madonvi, string ngach, string maphucap, int thang, int nam)
        {
            return _tlBangLuongThangRepository.ReportBangLuongThangDoc(madonvi, ngach, maphucap, thang, nam);
        }

        public Dictionary<string, object> ReportBangLuongThangDongData(List<TlBangLuongThangDongNq104Query> data, List<TlBangLuongThangDongNq104Query> lstHslCb, List<TlDmDonViNq104> lstDonVi, int page, int totalPage)
        {
            Dictionary<string, object> rs = new Dictionary<string, object>();
            List<TlRptBangLuongThangDong> items = new List<TlRptBangLuongThangDong>();
            string[] tenDonViArr = new string[5];
            int limit = 5;
            var lstDonViSub = lstDonVi.Skip((page - 1) * limit - 1).Take(limit).ToList();
            foreach (var row in lstHslCb)
            {
                var itemArr = new TlRptBangLuongThangDongItem[5];
                int index = 0;
                foreach (var column in lstDonViSub)
                {
                    var obj = data.FirstOrDefault(x => row.MaCb.Equals(x.MaCb) && row.HeSoLuong == x.HeSoLuong && column.MaDonVi.Equals(x.MaDonVi));
                    if (obj != null)
                    {
                        itemArr[index] = new TlRptBangLuongThangDongItem() { QuanSo = obj.QuanSo, Tien = obj.Tien };
                    }
                    index++;
                }

                TlRptBangLuongThangDong item = new TlRptBangLuongThangDong();
                item.IsSummary = false;
                item.CapBac = row.MaCb;
                item.HeSoLuong = row.HeSoLuong;
                item.Item1 = itemArr[0] ?? item.Item1;
                item.Item2 = itemArr[1] ?? item.Item2;
                item.Item3 = itemArr[2] ?? item.Item3;
                item.Item4 = itemArr[3] ?? item.Item4;
                item.Item5 = itemArr[4] ?? item.Item5;

                // Nếu là trang đầu => Tính tổng
                if (page == 1)
                {
                    item.ItemSummary.QuanSo = data.Where(x => x.MaCb.Equals(row.MaCb) && x.HeSoLuong.Equals(row.HeSoLuong)).Sum(x => x.QuanSo);
                    item.ItemSummary.Tien = data.Where(x => x.MaCb.Equals(row.MaCb) && x.HeSoLuong.Equals(row.HeSoLuong)).Sum(x => x.Tien);
                }
                items.Add(item);
            }

            // Add summary item
            TlRptBangLuongThangDong itemSummary = new TlRptBangLuongThangDong();
            itemSummary.IsSummary = true;
            itemSummary.Item1.QuanSo = items.Where(x => x.Item1 != null && x.Item1.QuanSo != null).Sum(x => x.Item1.QuanSo);
            itemSummary.Item2.QuanSo = items.Where(x => x.Item2 != null && x.Item2.QuanSo != null).Sum(x => x.Item2.QuanSo);
            itemSummary.Item3.QuanSo = items.Where(x => x.Item3 != null && x.Item3.QuanSo != null).Sum(x => x.Item3.QuanSo);
            itemSummary.Item4.QuanSo = items.Where(x => x.Item4 != null && x.Item4.QuanSo != null).Sum(x => x.Item4.QuanSo);
            itemSummary.Item5.QuanSo = items.Where(x => x.Item5 != null && x.Item5.QuanSo != null).Sum(x => x.Item5.QuanSo);
            itemSummary.Item1.Tien = items.Where(x => x.Item1 != null && x.Item1.Tien != null).Sum(x => x.Item1.Tien);
            itemSummary.Item2.Tien = items.Where(x => x.Item2 != null && x.Item2.Tien != null).Sum(x => x.Item2.Tien);
            itemSummary.Item3.Tien = items.Where(x => x.Item3 != null && x.Item3.Tien != null).Sum(x => x.Item3.Tien);
            itemSummary.Item4.Tien = items.Where(x => x.Item4 != null && x.Item4.Tien != null).Sum(x => x.Item4.Tien);
            itemSummary.Item5.Tien = items.Where(x => x.Item5 != null && x.Item5.Tien != null).Sum(x => x.Item5.Tien);
            itemSummary.ItemSummary.Tien = items.Sum(x => x.ItemSummary.Tien);
            itemSummary.ItemSummary.QuanSo = items.Sum(x => x.ItemSummary.QuanSo);
            items.Add(itemSummary);

            rs.Add("Items", items);
            rs.Add("DonVi1", lstDonViSub.Count > 0 ? lstDonViSub[0].TenDonVi : string.Empty);
            rs.Add("DonVi2", lstDonViSub.Count > 1 ? lstDonViSub[1].TenDonVi : string.Empty);
            rs.Add("DonVi3", lstDonViSub.Count > 2 ? lstDonViSub[2].TenDonVi : string.Empty);
            rs.Add("DonVi4", lstDonViSub.Count > 3 ? lstDonViSub[3].TenDonVi : string.Empty);
            rs.Add("DonVi5", lstDonViSub.Count > 4 ? lstDonViSub[4].TenDonVi : string.Empty);
            return rs;
        }

        private decimal ThueTN(decimal luongThuThue)
        {
            decimal t = 0;
            if (luongThuThue <= 0)
            {
                return 0;
            }
            else if (luongThuThue > 0 && luongThuThue <= 60000000)
            {
                t = luongThuThue / 20;
            }
            else if (luongThuThue > 60000000 && luongThuThue <= 120000000)
            {
                t = luongThuThue / 10 - 3000000;
            }
            else if (luongThuThue > 120000000 && luongThuThue <= 216000000)
            {
                t = luongThuThue * 15 / 100 - 9000000;

            }
            else if (luongThuThue > 216000000 && luongThuThue <= 384000000)
            {
                t = luongThuThue * 20 / 100 - 23400000;

            }
            else if (luongThuThue > 384000000 && luongThuThue <= 624000000)
            {
                t = luongThuThue * 25 / 100 - 57000000;

            }
            else if (luongThuThue > 624000000 && luongThuThue <= 960000000)
            {
                t = luongThuThue * 30 / 100 - 117000000;

            }
            else if (luongThuThue > 960000000)
            {
                t = luongThuThue * 35 / 100 - 217800000;

            }
            return t;
        }

        public IEnumerable<TlBangLuongThangNq104Query> GetDataInsert(int thang, int nam, string maDonVi, string maCachTl, int soNgay)
        {
            return _tlBangLuongThangRepository.GetDataInsert(thang, nam, maDonVi, maCachTl, soNgay);
        }
        public IEnumerable<TlBangLuongThangNq104Query> GetDataInsertBhxh(int thang, int nam, string maDonVi, string maCachTl, int soNgay)
        {
            return _tlBangLuongThangRepository.GetDataInsertBhxh(thang, nam, maDonVi, maCachTl, soNgay);
        }

        public DataTable ReportGiaiThichChiTietPhuCapKhac(List<TlDmDonViNq104> lstDonVi, List<TlDmPhuCapNq104> listPhuCap, int nam, int thang, string maCachTl, int donViTinh, bool isOrderChucVu)
        {
            string maDonVi = string.Join(",", lstDonVi.Select(x => x.MaDonVi));
            string maPhuCap = string.Join(",", listPhuCap.Select(x => x.MaPhuCap).Distinct());
            return _tlBangLuongThangRepository.ReportGiaiThichChiTietPhuCapKhac(maDonVi, maPhuCap, nam, thang, maCachTl, donViTinh, isOrderChucVu);
        }

        public DataTable ReportGiaiThichChiTietPhuCapTruyLinhKhac(List<TlDmDonViNq104> lstDonVi, List<TlDmPhuCapNq104> listPhuCap, int nam, int thang, string maCachTl, int donViTinh, bool isOrderChucVu)
        {
            string maDonVi = string.Join(",", lstDonVi.Select(x => x.MaDonVi));
            string maPhuCap = string.Join(",", listPhuCap.Select(x => x.MaPhuCap).Distinct());
            return _tlBangLuongThangRepository.ReportGiaiThichChiTietPhuCapKhac(maDonVi, maPhuCap, nam, thang, maCachTl, donViTinh, isOrderChucVu);
        }

        public DataTable ReportGiaiThichPhuCapTheoNgay(List<TlDmDonViNq104> lstDonVi, List<TlDmPhuCapNq104> listPhuCap, int nam, int thang, string maCachTl, int donViTinh, bool isOrderChucVu)
        {
            string maDonVi = string.Join(",", lstDonVi.Select(x => x.MaDonVi));
            string maPhuCap = string.Join(",", listPhuCap.Select(x => x.MaPhuCap).Distinct());
            return _tlBangLuongThangRepository.ReportGiaiThichPhuCapTheoNgay(maDonVi, maPhuCap, nam, thang, maCachTl, donViTinh, isOrderChucVu);
        }

        public DataTable ReportGiaiThichSoNgayPhuCapTheoNgay(List<TlDmDonViNq104> lstDonVi, List<TlDmPhuCapNq104> listPhuCap, int nam, int thang, string maCachTl, int donViTinh, bool isOrderChucVu)
        {
            string maDonVi = string.Join(",", lstDonVi.Select(x => x.MaDonVi));
            string maPhuCap = string.Join(",", listPhuCap.Select(x => x.MaPhuCap).Distinct());
            return _tlBangLuongThangRepository.ReportGiaiThichSoNgayPhuCapTheoNgay(maDonVi, maPhuCap, nam, thang, maCachTl, donViTinh, isOrderChucVu);
        }

        public DataTable GetDataRaQuanXuatNgu(List<TlDmDonViNq104> listDonVi, int thang, int nam, int donViTinh)
        {
            string maDonVi = string.Join(",", listDonVi.Select(x => x.MaDonVi));
            var data = _tlBangLuongThangRepository.ReportGiaiThichRaQuanXuatNgu(maDonVi, nam, thang, donViTinh);
            data.Columns.Add(ExportColumnHeader.STT, typeof(int));
            data.Columns.Add("ThangTn", typeof(int));
            foreach (DataRow item in data.Rows)
            {
                var ngayNn = item.Field<DateTime?>("NhapNgu");
                var ngayXn = item.Field<DateTime?>("XuatNgu");
                if (ngayNn == null)
                {
                    item["ThangTn"] = 0;
                }
                else if (ngayNn != null && ngayXn == null)
                {
                    var ngayNhapNgu = (DateTime)ngayNn;
                    item["ThangTn"] = (nam - ngayNhapNgu.Year) * 12 + thang - ngayNhapNgu.Month + 1;
                }
                else
                {
                    var ngayNhapNgu = (DateTime)ngayNn;
                    var ngayXuatNgu = (DateTime)ngayXn;
                    item["ThangTn"] = (ngayXuatNgu.Year - ngayNhapNgu.Year) * 12 + ngayXuatNgu.Month - ngayNhapNgu.Month + 1;
                }
            }
            return data;
        }

        public DataTable ReportChiTraNganHangThuNhapKhac(List<TlDmDonViNq104> listDonVi, int thang, int nam, bool isSummary, bool isOrderChucVu)
        {
            string maDonVi = string.Join(",", listDonVi.Select(x => x.MaDonVi));
            return _tlBangLuongThangRepository.ReportChiTraNganHangThuNhapKhac(maDonVi, thang, nam, isOrderChucVu);
        }

        public DataTable ReportGiaiThichBienPhong(string maDonVi, int nam, int thang, int donViTinh, string maPhuCap)
        {
            var data = _tlBangLuongThangRepository.ReportGiaiThichBienPhong(maDonVi, nam, thang, donViTinh, maPhuCap);
            data.Columns.Add("STT", typeof(int));
            var index = 1;
            foreach (DataRow item in data.Rows)
            {
                item["STT"] = index++;
            }
            return data;
        }

        public DataTable ReportGiaiThichBienPhongTheoHeSo(string maDonVi, int nam, int thang, int donViTinh, string maPhuCap, string maPhuCapTien)
        {
            return _tlBangLuongThangRepository.ReportGiaiThichBienPhongTheoHeSo(maDonVi, nam, thang, donViTinh, maPhuCap, maPhuCapTien);
        }

        private decimal TinhThueTN(decimal luongThuThue, bool bIsThueThang = true)
        {
            var dsThuThue = FindThue(bIsThueThang).ToList();
            decimal tienThue = 0;
            decimal t = luongThuThue.Clone();
            if (luongThuThue <= 0)
            {
                return 0;
            }
            else
            {
                foreach (var item in dsThuThue)
                {
                    if (luongThuThue >= (decimal)item.ThuNhapDen && (int)item.ThuNhapDen != 0)
                    {
                        tienThue += ((decimal)item.ThuNhapDen - (decimal)item.ThuNhapTu) * ((decimal)item.ThueXuat / 100);
                        t = t - ((decimal)item.ThuNhapDen - (decimal)item.ThuNhapTu);
                    }
                    else if ((int)item.ThuNhapDen == 0)
                    {
                        tienThue += (luongThuThue - (decimal)item.ThuNhapTu) * ((decimal)item.ThueXuat / 100);
                    }
                    else if (luongThuThue < (decimal)item.ThuNhapDen)
                    {
                        decimal tien = t * ((decimal)item.ThueXuat / 100);
                        tienThue += tien;
                        return Math.Round(tienThue);
                    }
                }
                return Math.Round(tienThue);
            }
        }

        public TlBangLuongThangNq104 GetMonthlySalary(string maCanBo, string maPhuCap, int? thang, int? nam)
        {
            return _tlBangLuongThangRepository.GetMonthlySalary(maCanBo, maPhuCap, thang, nam);
        }

        public TlBangLuongThangNq104 GetLatestSalary(string maCanBo, int? thang, int? nam)
        {
            return _tlBangLuongThangRepository.GetLatestSalary(maCanBo, thang, nam);
        }

        public DataTable GetDataBangLuongThangTruBHXH(List<TlDmDonViNq104> listDonVi, int thang, int nam, bool isOrderChucVu, bool isGiaTriAm, bool isCheckedMaHuongLuong, bool isInCanBoMoi, decimal tyLeHuong = 0)
        {
            string maDonVi = string.Join(",", listDonVi.Select(x => x.MaDonVi));
            return _tlBangLuongThangRepository.GetDataBangLuongThangTruBHXH(maDonVi, thang, nam, isOrderChucVu, isGiaTriAm, isCheckedMaHuongLuong, isInCanBoMoi, tyLeHuong);
        }

        public DataTable ReportBangLuongThangTheoDonViTruBHXH(List<TlDmDonViNq104> listDonVi, int thang, int nam, bool isOrderChucVu, bool isGiaTriAm, bool isCheckedMaHuongLuong)
        {
            string maDonVi = string.Join(",", listDonVi.Select(x => x.MaDonVi));
            return _tlBangLuongThangRepository.ReportBangLuongThangTheoDonViTruBHXH(maDonVi, thang, nam, isOrderChucVu, isGiaTriAm, isCheckedMaHuongLuong);
        }

        public IEnumerable<TlBangLuongThangNq104> FindBangLuongThangByCondition(string maDonVi, int? thang, int? nam, string maCachTL, string maHieuCanBo)
        {
            return _tlBangLuongThangRepository.FindBangLuongThangByCondition(maDonVi, thang, nam, maCachTL, maHieuCanBo);
        }

        public List<TlBangLuongThangNq104> FindLuongThangCanBo(int? thang, int? nam, string maDonVi, Guid id, string maCach)
        {
            return _tlBangLuongThangRepository.FindLuongThangCanBo(thang, nam, maDonVi, id, maCach);
        }
    }
}
