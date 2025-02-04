using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class KhoiTaoMapper : Profile
    {
        public KhoiTaoMapper()
        {
            CreateMap<KhoiTaoQuery, InitializationProjectModel>();
            CreateMap<KhoiTaoDuLieuQuery, InitializationProcessModel>()
                .ForMember(model => model.IIdMaDonVi, member => member.MapFrom(entity => entity.DonViId));

            CreateMap<VdtKtKhoiTao, InitializationProjectModel>()
                .ForMember(model => model.DuAnId, member => member.MapFrom(entity => entity.IIdDuAnId))
                .ForMember(model => model.NamKhoiTao, member => member.MapFrom(entity => entity.INamKhoiTao))
                .ForMember(model => model.DonViId, member => member.MapFrom(entity => entity.IIdDonViId));

            CreateMap<VdtKtKhoiTaoDuLieu, InitializationProcessModel>()
               .ForMember(model => model.NamKhoiTao, member => member.MapFrom(entity => entity.INamKhoiTao))
               .ForMember(model => model.NgayKhoiTao, member => member.MapFrom(entity => entity.DNgayKhoiTao))
               .ForMember(model => model.IIdMaDonVi, member => member.MapFrom(entity => entity.IIdMaDonVi)).ReverseMap();
             
            CreateMap<KhoiTaoChiTietQuery, InitializationProjectDetailModel>();

            CreateMap<InitializationProjectDetailModel, VdtKtKhoiTaoChiTiet>()
                .ForMember(model => model.FKhvonHetNamTruoc, member => member.MapFrom(entity => entity.KHVonHetNamTruoc))
                .ForMember(model => model.FLuyKeThanhToanKlht, member => member.MapFrom(entity => entity.LuyKeThanhToanKLHT))
                .ForMember(model => model.FLuyKeThanhToanTamUng, member => member.MapFrom(entity => entity.LuyKeThanhToanTamUng))
                .ForMember(model => model.FThanhToanQuaKb, member => member.MapFrom(entity => entity.ThanhToanQuaKB))
                .ForMember(model => model.FTamUngQuaKb, member => member.MapFrom(entity => entity.TamUngQuaKB))
                .ForMember(model => model.FSoChuyenChiTieuDaCap, member => member.MapFrom(entity => entity.SoChuyenChiTieuDaCap))
                .ForMember(model => model.IIdKhoiTaoId, member => member.MapFrom(entity => entity.IdKhoiTaoID))
                .ForMember(model => model.IIdNguonVonId, member => member.MapFrom(entity => entity.MaNguonNganSach))
                .ForMember(model => model.IIdLoaiNguonVonId, member => member.MapFrom(entity => entity.IdLoaiNguonVonID))
                .ForMember(model => model.IIdMucId, member => member.MapFrom(entity => entity.M))
                .ForMember(model => model.IIdTieuMucId, member => member.MapFrom(entity => entity.TM))
                .ForMember(model => model.IIdTietMucId, member => member.MapFrom(entity => entity.TTM))
                .ForMember(model => model.IIdNganhId, member => member.MapFrom(entity => entity.NG))
                .ForMember(model => model.FSoChuyenChiTieuChuaCap, member => member.MapFrom(entity => entity.SoChuyenChiTieuChuaCap));

            CreateMap<KhoiTaoDuLieuChiTietQuery, InitializationProcessDetailModel>();

            CreateMap<InitializationProcessDetailModel, VdtKtKhoiTaoDuLieuChiTiet>()
                .ForMember(model => model.IIdDuAnId, member => member.MapFrom(entity => entity.IID_DuAnID))
                .ForMember(model => model.IIdKhoiTaoDuLieuId, member => member.MapFrom(entity => entity.IID_KhoiTaoDuLieuID))
                .ForMember(model => model.FKhvnVonBoTriHetNamTruoc, member => member.MapFrom(entity => entity.FKHVN_VonBoTriHetNamTruoc))
                .ForMember(model => model.FKhvnLkvonDaThanhToanTuKhoiCongDenHetNamTruoc, member => member.MapFrom(entity => entity.FKHVN_LKVonDaThanhToanTuKhoiCongDenHetNamTruoc))
                .ForMember(model => model.FKhvnTrongDoVonTamUngTheoCheDoChuaThuHoi, member => member.MapFrom(entity => entity.FKHVN_TrongDoVonTamUngTheoCheDoChuaThuHoi))
                .ForMember(model => model.FKhvnLkvonTamUngTheoCheDoChuaThuHoiNopDieuChinhGiamDenHetNamTruoc, member => member.MapFrom(entity => entity.FKHVN_LKVonTamUngTheoCheDoChuaThuHoiNopDieuChinhGiamDenHetNamTruoc))
                .ForMember(model => model.FKhvnKeHoachVonKeoDaiSangNam, member => member.MapFrom(entity => entity.FKHVN_KeHoachVonKeoDaiSangNam))
                .ForMember(model => model.FKhutVonBoTriHetNamTruoc, member => member.MapFrom(entity => entity.FKHUT_VonBoTriHetNamTruoc))
                .ForMember(model => model.FKhutLkvonDaThanhToanTuKhoiCongDenHetNamTruoc, member => member.MapFrom(entity => entity.FKHUT_LKVonDaThanhToanTuKhoiCongDenHetNamTruoc))
                .ForMember(model => model.FKhutTrongDoVonTamUngTheoCheDoChuaThuHoi, member => member.MapFrom(entity => entity.FKHUT_TrongDoVonTamUngTheoCheDoChuaThuHoi))
                .ForMember(model => model.FKhutKeHoachUngTruocKeoDaiSangNam, member => member.MapFrom(entity => entity.FKHUT_KeHoachUngTruocKeoDaiSangNam))
                .ForMember(model => model.FKhutKeHoachUngTruocChuaThuHoi, member => member.MapFrom(entity => entity.FKHUT_KeHoachUngTruocChuaThuHoi))
                .ForMember(model => model.lstContract, member => member.MapFrom(entity => entity.LstDetail));


            CreateMap<KhoiTaoImportModel, VdtKtKhoiTaoDuLieuChiTiet>()
               .ForMember(model => model.IIdDuAnId, member => member.MapFrom(entity => entity.DuAnId))
               .ForMember(model => model.SMaDuAn, member => member.MapFrom(entity => entity.MaDuAn))
               .ForMember(model => model.FKhvnVonBoTriHetNamTruoc, member => member.MapFrom(entity => entity.FKHVN_VonBoTriHetNamTruocValue))
               .ForMember(model => model.FKhvnLkvonDaThanhToanTuKhoiCongDenHetNamTruoc, member => member.MapFrom(entity => entity.FKHVN_LKVonDaThanhToanTuKhoiCongDenHetNamTruocValue))
               .ForMember(model => model.FKhvnTrongDoVonTamUngTheoCheDoChuaThuHoi, member => member.MapFrom(entity => entity.FKHVN_TrongDoVonTamUngTheoCheDoChuaThuHoiValue))
               .ForMember(model => model.FKhvnLkvonTamUngTheoCheDoChuaThuHoiNopDieuChinhGiamDenHetNamTruoc, member => member.MapFrom(entity => entity.FKHVN_LKVonTamUngTheoCheDoChuaThuHoiNopDieuChinhGiamDenHetNamTruocValue))
               .ForMember(model => model.FKhvnKeHoachVonKeoDaiSangNam, member => member.MapFrom(entity => entity.FKHVN_KeHoachVonKeoDaiSangNamValue))
               .ForMember(model => model.FKhutVonBoTriHetNamTruoc, member => member.MapFrom(entity => entity.FKHUT_VonBoTriHetNamTruocValue))
               .ForMember(model => model.FKhutLkvonDaThanhToanTuKhoiCongDenHetNamTruoc, member => member.MapFrom(entity => entity.FKHUT_LKVonDaThanhToanTuKhoiCongDenHetNamTruocValue))
               .ForMember(model => model.FKhutTrongDoVonTamUngTheoCheDoChuaThuHoi, member => member.MapFrom(entity => entity.FKHUT_TrongDoVonTamUngTheoCheDoChuaThuHoiValue))
               .ForMember(model => model.FKhutKeHoachUngTruocKeoDaiSangNam, member => member.MapFrom(entity => entity.FKHUT_KeHoachUngTruocKeoDaiSangNamValue))
               .ForMember(model => model.FKhutKeHoachUngTruocChuaThuHoi, member => member.MapFrom(entity => entity.FKHUT_KeHoachUngTruocChuaThuHoiValue));

            CreateMap<InitializationProjectDialogModel, VdtKtKhoiTao>();
            CreateMap<VdtKtKhoiTao, InitializationProjectDialogModel>();

            CreateMap<VdtKtKhoiTaoDuLieuChiTietThanhToanQuery, VdtKtKhoiTaoDuLieuChiTietThanhToanModel>();
            CreateMap<VdtKtKhoiTaoDuLieuChiTietThanhToanModel, VdtKtKhoiTaoDuLieuChiTietThanhToanQuery>();

            CreateMap<VdtKtKhoiTaoDuLieuChiTietThanhToanModel, VdtKtKhoiTaoDuLieuChiTietThanhToan>();
            CreateMap<VdtKtKhoiTaoDuLieuChiTietThanhToan, VdtKtKhoiTaoDuLieuChiTietThanhToanModel>();
        }
    }
}
