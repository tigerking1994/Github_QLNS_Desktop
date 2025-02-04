using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class CloneDataYearOfWorkService : ICloneDataYearOfWork
    {
        private readonly ApplicationDbContextFactory _contextFactory;
        private readonly IMucLucNganSachRepository _mucLucNganSachRepository;
        private readonly INsDonViRepository _donViRepository;
        private readonly INsPhongBanRepository _phongBanRepository;
        private readonly INsQsMucLucRepository _mlQuanSuRepository;
        private readonly ISktMucLucRepository _sktMucLucRepository;
        private readonly IDanhMucRepository _danhMucRepository;
        private readonly ISktMucLucMapRepository _mlsktnsRepository;
        private readonly ICpDanhMucRepository _cpDanhMucRepository;
        private readonly ITlPhuCapMlnsRepository _phuCapMlnsRepository;
        private readonly ITlDmCapBacKeHoachRepository _capBacKeHoachRepository;

        public CloneDataYearOfWorkService(ApplicationDbContextFactory contextFactory,
            IMucLucNganSachRepository lucNganSachRepository,
            INsDonViRepository donViRepository,
            INsPhongBanRepository phongBanRepository,
            INsQsMucLucRepository mlQuanSuRepository,
            ISktMucLucRepository sktMucLucRepository,
            IDanhMucRepository danhMucRepository,
            ISktMucLucMapRepository mlsktnsRepository,
            ICpDanhMucRepository cpDanhMucRepository,
            ITlPhuCapMlnsRepository phuCapMlnsRepository,
            ITlDmCapBacKeHoachRepository capBacKeHoachRepository
            )
        {
            _contextFactory = contextFactory;
            _mucLucNganSachRepository = lucNganSachRepository;
            _donViRepository = donViRepository;
            _phongBanRepository = phongBanRepository;
            _mlQuanSuRepository = mlQuanSuRepository;
            _sktMucLucRepository = sktMucLucRepository;
            _danhMucRepository = danhMucRepository;
            _mlsktnsRepository = mlsktnsRepository;
            _cpDanhMucRepository = cpDanhMucRepository;
            _phuCapMlnsRepository = phuCapMlnsRepository;
            _capBacKeHoachRepository = capBacKeHoachRepository;
        }

        public void CloneData(int sourceYear, int destinationYear, AuthenticationInfo authenticationInfo,
            int isUpdatedMLNS, int isUpdatedNSDV, int isUpdatedBQuanly, int isUpdateMLQS, int isUpdateDanhMucChuyenNganh, int isUpdateDanhMucNganh,
            int isUpdateMuclucSkt, int isUpdateDanhMucCapPhat, int isUpdatePhuCapMLNS, int isUpdateDanhMucCapBacKH,
            int isUpdateNSSKT, int isUpdateCauHinhHeThong, int isUpdateDanhMucDonViTinh, int isUpdateDanhMucCanCu, int isUpdateDanhMucCKTC,
            int isUpdateDanhMucBHXH, int isUpdateMucLucCacLoaiChi, int isUpdateDanhMucCoSoYTe, int isUpdateDanhMucThamDinhQuyetToan, int isUpdateDanhMucCauHinhThamSoBHXH,
            int isUpdateDanhMucNgayNghi, int isUpdateMucLucQuyetToanNam
            //, int isUpdateDanhMucChuDauTu, int isUpdateDanhMucDonviQuanLyDuAn, int isUpdateDanhMucNhaThau
            )
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter userCreator = new SqlParameter("userCreator", authenticationInfo.Principal);
                SqlParameter SourceYear = new SqlParameter("sourceYear", sourceYear);
                SqlParameter DestinationYear = new SqlParameter("destinationYear", destinationYear);
                SqlParameter IsUpdatedMLNS = new SqlParameter("isUpdatedMLNS", isUpdatedMLNS);
                SqlParameter IsUpdatedNSDV = new SqlParameter("isUpdatedNSDV", isUpdatedNSDV);
                SqlParameter IsUpdatedBQuanly = new SqlParameter("isUpdatedBQuanLy", isUpdatedBQuanly);
                SqlParameter IsUpdateMLQS = new SqlParameter("isUpdateMLQS", isUpdateMLQS);
                SqlParameter IsUpdateDanhMucChuyenNganh = new SqlParameter("isUpdateDanhMucChuyenNganh", isUpdateDanhMucChuyenNganh);
                SqlParameter IsUpdateDanhMucNganh = new SqlParameter("isUpdateDanhMucNganh", isUpdateDanhMucNganh);
                SqlParameter IsUpdateMuclucSkt = new SqlParameter("isUpdateMuclucSkt", isUpdateMuclucSkt);
                SqlParameter IsUpdateDanhMucCapPhat = new SqlParameter("isUpdateDanhMucCapPhat", isUpdateDanhMucCapPhat);
                SqlParameter IsUpdateCauHinhChiTieuLuongMLNS = new SqlParameter("isUpdateCauHinhChiTieuLuongMLNS", isUpdatePhuCapMLNS);
                SqlParameter IsUpdateDmCapBacKh = new SqlParameter("isUpdateDmCapBacKh", isUpdateDanhMucCapBacKH);
                SqlParameter IsUpdateNSSKT = new SqlParameter("isUpdateNSSKT", isUpdateNSSKT);
                SqlParameter IsUpdateCauHinhHeThong = new SqlParameter("isUpdateCauHinhHeThong", isUpdateCauHinhHeThong);
                SqlParameter IsUpdateDanhMucDonViTinh = new SqlParameter("isUpdateDanhMucDonViTinh", isUpdateDanhMucDonViTinh);
                SqlParameter IsUpdateDanhMucCanCu = new SqlParameter("isUpdateDanhMucCanCu", isUpdateDanhMucCanCu);
                SqlParameter IsUpdateDanhMucCKTC = new SqlParameter("isUpdateDanhMucCKTC", isUpdateDanhMucCKTC);
                SqlParameter IsUpdateDanhMucBHXH = new SqlParameter("isUpdateDanhMucBHXH", isUpdateDanhMucBHXH);
                SqlParameter IsUpdateMucLucCacLoaiChi = new SqlParameter("isUpdateMucLucCacLoaiChi", isUpdateMucLucCacLoaiChi);
                SqlParameter IsUpdateDanhMucCoSoYTe = new SqlParameter("isUpdateDanhMucCoSoYTe", isUpdateDanhMucCoSoYTe);
                SqlParameter IsUpdateDanhMucTDQT = new SqlParameter("isUpdateDanhMucTDQT", isUpdateDanhMucThamDinhQuyetToan);
                SqlParameter IsUpdateDanhMucCHTMoBHXH = new SqlParameter("isUpdateDanhMucCHTSBHXH", isUpdateDanhMucCauHinhThamSoBHXH);
                SqlParameter IsUpdateDanhMucNgayNghi = new SqlParameter("isUpdateDanhMucNgayNghi", isUpdateDanhMucNgayNghi);
                SqlParameter IsUpdateMucLucQuyetToanNam = new SqlParameter("isUpdateMucLucQuyetToanNam", isUpdateMucLucQuyetToanNam);

                ctx.Database.ExecuteSqlCommand("EXEC sp_clone_data_update_new" +
                    " @userCreator, @sourceYear, @destinationYear, @isUpdatedMLNS, " +
                    "@isUpdatedNSDV, @isUpdatedBQuanLy, @isUpdateMLQS, @isUpdateDanhMucChuyenNganh, @isUpdateDanhMucNganh, @isUpdateMuclucSkt, @isUpdateDanhMucCapPhat, @isUpdateCauHinhChiTieuLuongMLNS, @isUpdateDmCapBacKh, @isUpdateNSSKT, @isUpdateCauHinhHeThong, @isUpdateDanhMucDonViTinh, @isUpdateDanhMucCanCu, @isUpdateDanhMucCKTC, @isUpdateDanhMucBHXH, @isUpdateMucLucCacLoaiChi, @isUpdateDanhMucCoSoYTe, @isUpdateDanhMucTDQT, @isUpdateDanhMucCHTSBHXH, @isUpdateDanhMucNgayNghi, @isUpdateMucLucQuyetToanNam",
                    userCreator, SourceYear, DestinationYear, IsUpdatedMLNS, IsUpdatedNSDV, IsUpdatedBQuanly, IsUpdateMLQS, IsUpdateDanhMucChuyenNganh,
                    IsUpdateDanhMucNganh, IsUpdateMuclucSkt, IsUpdateDanhMucCapPhat, IsUpdateCauHinhChiTieuLuongMLNS, IsUpdateDmCapBacKh,
                    IsUpdateNSSKT, IsUpdateCauHinhHeThong, IsUpdateDanhMucDonViTinh, IsUpdateDanhMucCanCu, IsUpdateDanhMucCKTC, IsUpdateDanhMucBHXH,
                    IsUpdateMucLucCacLoaiChi, IsUpdateDanhMucCoSoYTe, IsUpdateDanhMucTDQT, IsUpdateDanhMucCHTMoBHXH, IsUpdateDanhMucNgayNghi, IsUpdateMucLucQuyetToanNam
                    );
            }
        }

        public void CloneDataExistData(int sourceYear, int destinationYear, string tableName, AuthenticationInfo authenticationInfo, bool isCloneUnit)
        {
            switch (tableName)
            {
                case "MucLucNganSach":
                    CopyMlns(sourceYear, destinationYear, authenticationInfo);
                    break;
                case "DanhMucDonVi":
                    CopyDonVi(sourceYear, destinationYear, authenticationInfo, isCloneUnit);
                    break;
                case "DanhMucBQuanLy":
                    CopyBQuanLy(sourceYear, destinationYear, authenticationInfo);
                    break;
                case "DanhMucQuanSo":
                    CopyMucLucQs(sourceYear, destinationYear, authenticationInfo);
                    break;
                case "MucLucSoKiemTra":
                    if (destinationYear == 2024 || destinationYear == 2025) break;
                    CopyMucLucSkt(sourceYear, destinationYear, authenticationInfo);
                    break;
                case "DanhMucCauHinhHeThong":
                    CopyDanhMucCauHinh(sourceYear, destinationYear, authenticationInfo);
                    break;
                case "MucLucSoKiemTraNganSach":
                    if (destinationYear == 2024 || destinationYear == 2025) break;
                    CopyMlnsMlskt(sourceYear, destinationYear, authenticationInfo);
                    break;
                case "DanhMucChuyenNganh":
                    CopyDanhMucChuyenNganh(sourceYear, destinationYear, authenticationInfo);
                    break;
                case "DanhMucNganh":
                    CopyDanhMucNganh(sourceYear, destinationYear, authenticationInfo);
                    break;
                case "DanhMucCapPhat":
                    //CopyDmCapPhat(sourceYear, destinationYear, authenticationInfo);
                    break;
                case "CauHinhChiTieuLuong":
                    CopyPhuCapChiTieuLuong(sourceYear, destinationYear, authenticationInfo);
                    break;
                case "DanhMucCapBac":
                    //CopyDanhMucCapBac(sourceYear, destinationYear, authenticationInfo);
                    break;
                case "MucLucNganhChuyenNganh":
                    CopyDanhMucNganhChuyenNganh(sourceYear, destinationYear, authenticationInfo);
                    break;
                case "DanhMucDonViTinh":
                    CopyDanhMucDonViTinh(sourceYear, destinationYear, authenticationInfo);
                    break;
                case "DanhMucCanCu":
                    CopyDanhMucCanCu(sourceYear, destinationYear, authenticationInfo);
                    break;
                case "DanhMucCongKhaiTaiChinh":
                    CopyDanhMucCKTC(sourceYear, destinationYear, authenticationInfo);
                    break;
                case "DanhMucMLNSBHXH":
                    CopyMlnsBHXH(sourceYear, destinationYear, authenticationInfo);
                    break;
                case "DanhMucCacLoaiChi":
                    CopyMucLucCacLoaiChi(sourceYear, destinationYear, authenticationInfo);
                    break;
                case "DanhMucCoSoYTe":
                    CopyDanhMucCSYT(sourceYear, destinationYear, authenticationInfo);
                    break;
                case "DanhMucThamDinhQuyetToan":
                    CopyDanhMucTDQT(sourceYear, destinationYear, authenticationInfo);
                    break;
                case "DanhMucCauHinhThamSoBHXH":
                    CopyDanhMucCHTSBHXH(sourceYear, destinationYear, authenticationInfo);
                    break;
                case "DanhMucCauHinhNgayNghi":
                    CopyDanhMucNgayNghi(sourceYear, destinationYear, authenticationInfo);
                    break;
                case "MucLucQuyetToanNam":
                    CopyMucLucQuyetToanNam(sourceYear, destinationYear, authenticationInfo);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Mục lục ngân sách
        /// </summary>
        /// <param name="sourceYear"></param>
        /// <param name="destYear"></param>
        private void CopyMlns(int sourceYear, int destYear, AuthenticationInfo user)
        {

            using (var ctx = _contextFactory.CreateDbContext())
            {
                ctx.Database.SetCommandTimeout(300);
                SqlParameter source = new SqlParameter("source", sourceYear);
                SqlParameter dest = new SqlParameter("dest", destYear);
                SqlParameter userCreate = new SqlParameter("userCreate", user.Principal);
                ctx.Database.ExecuteSqlCommand("EXEC sp_clone_year_mlns @source, @dest, @userCreate", source, dest, userCreate);
            }
        }

        /// <summary>
        /// Clone đơn vị
        /// </summary>
        /// <param name="sourceYear"></param>
        /// <param name="destYear"></param>
        private void CopyDonVi(int sourceYear, int destYear, AuthenticationInfo user, bool isCloneUnit)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter source = new SqlParameter("source", sourceYear);
                SqlParameter dest = new SqlParameter("dest", destYear);
                SqlParameter userCreate = new SqlParameter("userCreate", user.Principal);
                //SqlParameter isCopyUnitUsed = new SqlParameter("isCopyDonViSuDung", isCloneUnit);
                ctx.Database.ExecuteSqlCommand("EXEC sp_clone_year_donVi @source, @dest, @userCreate", source, dest, userCreate);
            }
        }

        /// <summary>
        /// Clone BQuanLy
        /// </summary>
        /// <param name="sourceYear"></param>
        /// <param name="destYear"></param>
        /// <param name="user"></param>

        private void CopyBQuanLy(int sourceYear, int destYear, AuthenticationInfo user)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter source = new SqlParameter("source", sourceYear);
                SqlParameter dest = new SqlParameter("dest", destYear);
                SqlParameter userCreate = new SqlParameter("userCreate", user.Principal);
                ctx.Database.ExecuteSqlCommand("EXEC sp_clone_year_BQuanLy @source, @dest, @userCreate", source, dest, userCreate);
            }
        }

        /// <summary>
        /// Clone mục lục quân số
        /// </summary>
        /// <param name="sourceYear"></param>
        /// <param name="destYear"></param>
        /// <param name="user"></param>
        private void CopyMucLucQs(int sourceYear, int destYear, AuthenticationInfo user)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter source = new SqlParameter("source", sourceYear);
                SqlParameter dest = new SqlParameter("dest", destYear);
                SqlParameter userCreate = new SqlParameter("userCreate", user.Principal);
                ctx.Database.ExecuteSqlCommand("EXEC sp_clone_year_mlqs @source, @dest, @userCreate", source, dest, userCreate);
            }
        }

        /// <summary>
        /// Clone mục lục số kiểm tra
        /// </summary>
        /// <param name="sourceYear"></param>
        /// <param name="destYear"></param>
        /// <param name="user"></param>

        private void CopyMucLucSkt(int sourceYear, int destYear, AuthenticationInfo user)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter source = new SqlParameter("source", sourceYear);
                SqlParameter dest = new SqlParameter("dest", destYear);
                SqlParameter userCreate = new SqlParameter("userCreate", user.Principal);
                ctx.Database.ExecuteSqlCommand("EXEC sp_clone_year_mlskt @source, @dest, @userCreate", source, dest, userCreate);
            }

        }

        /// <summary>
        /// Clone mục lục cấu hình 
        /// </summary>
        /// <param name="sourceYear"></param>
        /// <param name="destYear"></param>
        /// <param name="user"></param>

        private void CopyDanhMucCauHinh(int sourceYear, int destYear, AuthenticationInfo user)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter source = new SqlParameter("source", sourceYear);
                SqlParameter dest = new SqlParameter("dest", destYear);
                SqlParameter userCreate = new SqlParameter("userCreate", user.Principal);
                SqlParameter type = new SqlParameter("type", "DM_CauHinh");
                int isCopy = 1;
                SqlParameter isGiaTri = new SqlParameter("isCopyGiaTri", isCopy);
                ctx.Database.ExecuteSqlCommand("EXEC sp_clone_year_muclucDanhMuc @source, @dest, @userCreate, @type, @isCopyGiaTri", source, dest, userCreate, type, isGiaTri);
            }

        }

        /// <summary>
        /// Clone danh mục ngành
        /// </summary>
        /// <param name="sourceYear"></param>
        /// <param name="destYear"></param>
        /// <param name="user"></param>
        private void CopyDanhMucNganh(int sourceYear, int destYear, AuthenticationInfo user)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter source = new SqlParameter("source", sourceYear);
                SqlParameter dest = new SqlParameter("dest", destYear);
                SqlParameter userCreate = new SqlParameter("userCreate", user.Principal);
                SqlParameter type = new SqlParameter("type", "NS_Nganh_Nganh");
                int icopy = 0;
                SqlParameter isGiaTri = new SqlParameter("isCopyGiaTri", icopy);
                ctx.Database.ExecuteSqlCommand("EXEC sp_clone_year_muclucDanhMuc @source, @dest, @userCreate, @type, @isCopyGiaTri", source, dest, userCreate, type, isGiaTri);
            }
        }

        /// <summary>
        /// Clone danh mục chuyên ngành
        /// </summary>
        /// <param name="sourceYear"></param>
        /// <param name="destYear"></param>
        /// <param name="user"></param>
        private void CopyDanhMucChuyenNganh(int sourceYear, int destYear, AuthenticationInfo user)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter source = new SqlParameter("source", sourceYear);
                SqlParameter dest = new SqlParameter("dest", destYear);
                SqlParameter userCreate = new SqlParameter("userCreate", user.Principal);
                SqlParameter type = new SqlParameter("type", "NS_Nganh");
                int copy = 0;
                SqlParameter isGiaTri = new SqlParameter("isCopyGiaTri", copy);
                ctx.Database.ExecuteSqlCommand("EXEC sp_clone_year_muclucDanhMuc @source, @dest, @userCreate, @type, @isCopyGiaTri", source, dest, userCreate, type, isGiaTri);
            }
        }

        /// <summary>
        /// Clone bảng nối Mục lục ngân sách và mục lục số kiểm tra
        /// </summary>
        /// <param name="sourceYear"></param>
        /// <param name="destYear"></param>
        /// <param name="user"></param>
        private void CopyMlnsMlskt(int sourceYear, int destYear, AuthenticationInfo user)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter source = new SqlParameter("source", sourceYear);
                SqlParameter dest = new SqlParameter("dest", destYear);
                SqlParameter userCreate = new SqlParameter("userCreate", user.Principal);
                ctx.Database.ExecuteSqlCommand("EXEC sp_clone_year_mlnsmlskt @source, @dest, @userCreate", source, dest, userCreate);
            }

        }

        /// <summary>
        /// Clone quan hệ ngành - chuyên ngành
        /// </summary>
        /// <param name="sourceYear"></param>
        /// <param name="destYear"></param>
        /// <param name="user"></param>
        private void CopyDanhMucNganhChuyenNganh(int sourceYear, int destYear, AuthenticationInfo user)
        {
            var items = _danhMucRepository.FindAll(c => (c.INamLamViec == sourceYear || c.INamLamViec == destYear) && c.SType.Equals("NS_Nganh_Nganh"));

            var nganhSources = items.Where(c => c.INamLamViec == sourceYear && c.SType.Equals("NS_Nganh_Nganh"));
            var nganhDests = items.Where(c => c.INamLamViec == destYear && c.SType.Equals("NS_Nganh_Nganh")).ToList();

            foreach (var item in nganhDests)
            {
                string child = nganhSources.FirstOrDefault(c => c.IIDMaDanhMuc.Equals(item.IIDMaDanhMuc))?.SGiaTri;
                if (!string.IsNullOrEmpty(child))
                {
                    var childs = child.Split(",").ToList();
                    foreach (var c in childs.ToList())
                    {
                        if (nganhDests.Any(d => d.SGiaTri.Contains(c)))
                        {
                            childs.Remove(c);
                        }
                    }

                    item.SGiaTri = childs.Join(",");
                }
                item.IsModified = true;
            }

            _danhMucRepository.AddOrUpdateRange(nganhDests);
        }

        private void CopyDanhMucDonViTinh(int sourceYear, int destYear, AuthenticationInfo user)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter source = new SqlParameter("source", sourceYear);
                SqlParameter dest = new SqlParameter("dest", destYear);
                SqlParameter userCreate = new SqlParameter("userCreate", user.Principal);
                SqlParameter type = new SqlParameter("type", "DM_DonViTinh");
                int copy = 1;
                SqlParameter isGiaTri = new SqlParameter("isCopyGiaTri", copy);
                ctx.Database.ExecuteSqlCommand("EXEC sp_clone_year_muclucDanhMuc @source, @dest, @userCreate, @type, @isCopyGiaTri", source, dest, userCreate, type, isGiaTri);
            }
        }

        private void CopyDanhMucCKTC(int sourceYear, int destYear, AuthenticationInfo user)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter source = new SqlParameter("source", sourceYear);
                SqlParameter dest = new SqlParameter("dest", destYear);
                SqlParameter userCreate = new SqlParameter("userCreate", user.Principal);
                ctx.Database.ExecuteSqlCommand("EXEC sp_clone_year_danhmuccktc @source, @dest, @userCreate", source, dest, userCreate);
            }
        }

        private void CopyDanhMucCanCu(int sourceYear, int destYear, AuthenticationInfo user)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter source = new SqlParameter("source", sourceYear);
                SqlParameter dest = new SqlParameter("dest", destYear);
                SqlParameter userCreate = new SqlParameter("userCreate", user.Principal);
                ctx.Database.ExecuteSqlCommand("EXEC sp_clone_year_danhmuccancu @source, @dest, @userCreate", source, dest, userCreate);
            }
        }

        /// <summary>
        /// Clone danh mục cấp phát (compare Ma)
        /// </summary>
        /// <param name="sourceYear"></param>
        /// <param name="destYear"></param>
        /// <param name="user"></param>
        /// <param name="tableName"></param>

        private void CopyDmCapPhat(int sourceYear, int destYear, AuthenticationInfo user)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter source = new SqlParameter("source", sourceYear);
                SqlParameter dest = new SqlParameter("dest", destYear);
                SqlParameter userCreate = new SqlParameter("userCreate", user.Principal);

                ctx.Database.ExecuteSqlCommand("EXEC sp_clone_year_dmcp @source, @dest, @userCreate", source, dest, userCreate);
            }
        }

        /// <summary>
        /// Clone phụ cấp chỉ tiêu lương - mục lục ngân sách
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        private void CopyPhuCapChiTieuLuong(int sourceYear, int destYear, AuthenticationInfo user)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter source = new SqlParameter("source", sourceYear);
                SqlParameter dest = new SqlParameter("dest", destYear);
                SqlParameter userCreate = new SqlParameter("userCreate", user.Principal);

                ctx.Database.ExecuteSqlCommand("EXEC sp_clone_year_phuCapChiLuong @source, @dest, @userCreate", source, dest, userCreate);
            }
        }

        /// <summary>
        /// Clone danh mục cấp bậc - kế hoạch
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        private void CopyDanhMucCapBac(int sourceYear, int destYear, AuthenticationInfo user)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter source = new SqlParameter("source", sourceYear);
                SqlParameter dest = new SqlParameter("dest", destYear);
                SqlParameter userCreate = new SqlParameter("userCreate", user.Principal);

                ctx.Database.ExecuteSqlCommand("EXEC sp_clone_year_dmcapkehoach @source, @dest, @userCreate", source, dest, userCreate);
            }
        }

        /// <summary>
        /// Mục lục ngân sách BHXH
        /// </summary>
        /// <param name="sourceYear"></param>
        /// <param name="destYear"></param>
        private void CopyMlnsBHXH(int sourceYear, int destYear, AuthenticationInfo user)
        {

            using (var ctx = _contextFactory.CreateDbContext())
            {
                ctx.Database.SetCommandTimeout(300);
                SqlParameter source = new SqlParameter("source", sourceYear);
                SqlParameter dest = new SqlParameter("dest", destYear);
                SqlParameter userCreate = new SqlParameter("userCreate", user.Principal);
                ctx.Database.ExecuteSqlCommand("EXEC sp_clone_year_mlns_bhxh @source, @dest, @userCreate", source, dest, userCreate);
            }
        }

        private void CopyMucLucCacLoaiChi(int sourceYear, int destYear, AuthenticationInfo user)
        {

            using (var ctx = _contextFactory.CreateDbContext())
            {
                ctx.Database.SetCommandTimeout(300);
                SqlParameter source = new SqlParameter("source", sourceYear);
                SqlParameter dest = new SqlParameter("dest", destYear);
                SqlParameter userCreate = new SqlParameter("userCreate", user.Principal);
                ctx.Database.ExecuteSqlCommand("EXEC sp_clone_year_muc_cac_loai_chi @source, @dest, @userCreate", source, dest, userCreate);
            }
        }

        private void CopyDanhMucCSYT(int sourceYear, int destYear, AuthenticationInfo user)
        {

            using (var ctx = _contextFactory.CreateDbContext())
            {
                ctx.Database.SetCommandTimeout(300);
                SqlParameter source = new SqlParameter("source", sourceYear);
                SqlParameter dest = new SqlParameter("dest", destYear);
                SqlParameter userCreate = new SqlParameter("userCreate", user.Principal);
                ctx.Database.ExecuteSqlCommand("EXEC sp_clone_year_danh_muc_csyt @source, @dest, @userCreate", source, dest, userCreate);
            }
        }

        private void CopyDanhMucTDQT(int sourceYear, int destYear, AuthenticationInfo user)
        {

            using (var ctx = _contextFactory.CreateDbContext())
            {
                ctx.Database.SetCommandTimeout(300);
                SqlParameter source = new SqlParameter("source", sourceYear);
                SqlParameter dest = new SqlParameter("dest", destYear);
                SqlParameter userCreate = new SqlParameter("userCreate", user.Principal);
                ctx.Database.ExecuteSqlCommand("EXEC sp_clone_year_danh_muc_tdqt @source, @dest, @userCreate", source, dest, userCreate);
            }
        }

        private void CopyDanhMucCHTSBHXH(int sourceYear, int destYear, AuthenticationInfo user)
        {

            using (var ctx = _contextFactory.CreateDbContext())
            {
                ctx.Database.SetCommandTimeout(300);
                SqlParameter source = new SqlParameter("source", sourceYear);
                SqlParameter dest = new SqlParameter("dest", destYear);
                SqlParameter userCreate = new SqlParameter("userCreate", user.Principal);
                ctx.Database.ExecuteSqlCommand("EXEC sp_clone_year_danh_muc_chts_bhxh @source, @dest, @userCreate", source, dest, userCreate);
            }
        }

        private void CopyDanhMucNgayNghi(int sourceYear, int destYear, AuthenticationInfo user)
        {

            using (var ctx = _contextFactory.CreateDbContext())
            {
                ctx.Database.SetCommandTimeout(300);
                SqlParameter source = new SqlParameter("source", sourceYear);
                SqlParameter dest = new SqlParameter("dest", destYear);
                ctx.Database.ExecuteSqlCommand("EXEC sp_clone_year_danh_muc_ngay_nghi @source, @dest", source, dest);
            }
        }

        private void CopyMucLucQuyetToanNam(int sourceYear, int destYear, AuthenticationInfo user)
        {

            using (var ctx = _contextFactory.CreateDbContext())
            {
                ctx.Database.SetCommandTimeout(300);
                SqlParameter source = new SqlParameter("source", sourceYear);
                SqlParameter dest = new SqlParameter("dest", destYear);
                SqlParameter userCreate = new SqlParameter("userCreate", user.Principal);
                ctx.Database.ExecuteSqlCommand("EXEC sp_clone_year_muc_luc_quyet_toan_nam @source, @dest, @userCreate", source, dest, userCreate);
            }
        }

        /// <summary>
        /// Kiểm tra đơn vị của năm mới và năm cũ có cùng tồn tại iLoai = 0
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        /// <returns></returns>
        public bool IsDuplicateUnit(int source, int dest)
        {
            Func<DonVi, bool> isLoai = unit => unit.Loai.Equals("0");
            var items = _donViRepository.FindAll(unit => unit.NamLamViec == source || unit.NamLamViec == dest);
            var sources = items.Where(c => c.NamLamViec == source).ToList();
            var dests = items.Where(c => c.NamLamViec == dest).ToList();

            return sources.Any(isLoai) && dests.Any(isLoai);
        }

        private Guid GetParentMlns(string child, List<NsMucLucNganSach> sources)
        {
            var parent = sources.Where(c => child.StartsWith(c.XauNoiMa)).OrderByDescending(c => c.XauNoiMa.Length).FirstOrDefault();
            if (parent is null)
            {
                return default;
            }

            return parent.MlnsId;
        }

    }
}
