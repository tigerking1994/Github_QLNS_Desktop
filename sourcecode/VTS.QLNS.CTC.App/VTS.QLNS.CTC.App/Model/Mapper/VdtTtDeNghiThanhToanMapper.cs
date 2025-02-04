using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class VdtTtDeNghiThanhToanMapper : Profile
    {
        public VdtTtDeNghiThanhToanMapper()
        {
            CreateMap<VdtTtDeNghiThanhToanModel, VdtTtDeNghiThanhToanQuery>();
            CreateMap<VdtTtDeNghiThanhToanQuery, VdtTtDeNghiThanhToanModel>()
                 .ForMember(n => n.BKhoa, m => m.MapFrom(n => n.bKhoa));
            CreateMap<DuAnDenghiThanhToanModel, DuAnDeNghiThanhToanQuery>();
            CreateMap<DuAnDeNghiThanhToanQuery, DuAnDenghiThanhToanModel>();
            CreateMap<VdtTtDeNghiThanhToanChiTietModel, DuAnDeNghiThanhToanQuery>()
                .ForMember(n => n.fLuyKeThanhToanKL, m => m.MapFrom(n => n.fLuyKeThanhToanKLHT))
                .ForMember(n => n.fThanhToanTrongNam, m => m.MapFrom(n => n.fGiaTriDaThanhToanTrongNam));
            CreateMap<DuAnDeNghiThanhToanQuery, VdtTtDeNghiThanhToanChiTietModel>()
                .ForMember(n => n.fLuyKeThanhToanKLHT, m => m.MapFrom(n => n.fLuyKeThanhToanKL))
                .ForMember(n => n.fGiaTriDaThanhToanTrongNam, m => m.MapFrom(n => n.fThanhToanTrongNam));
            CreateMap<VdtTtDeNghiThanhToanChiTietQuery, VdtTtDeNghiThanhToanChiTietModel>();
            CreateMap<VdtTtDeNghiThanhToanChiTietModel, VdtTtDeNghiThanhToanChiTietQuery>();
            CreateMap<VdtTtDeNghiThanhToan, VdtTtDeNghiThanhToanModel>()
                .ForMember(n => n.sSoDeNghi, m => m.MapFrom(n => n.SSoDeNghi))
                .ForMember(n => n.iID_DonViQuanLyID, m => m.MapFrom(n => n.IIdDonViQuanLyId))
                .ForMember(n => n.iID_MaDonViQuanLy, m => m.MapFrom(n => n.IIdMaDonViQuanLy))
                .ForMember(n => n.iID_NhomQuanLyID, m => m.MapFrom(n => n.IIdNhomQuanLyId))
                .ForMember(n => n.sNguoiLap, m => m.MapFrom(n => n.SNguoiLap))
                .ForMember(n => n.fGiaTriThanhToanTN, m => m.MapFrom(n => n.FGiaTriThanhToanTN))
                .ForMember(n => n.fGiaTriThanhToanNN, m => m.MapFrom(n => n.FGiaTriThanhToanNN))
                .ForMember(n => n.fGiaTriThuHoiTN, m => m.MapFrom(n => n.FGiaTriThuHoiTN))
                .ForMember(n => n.fGiaTriThuHoiNN, m => m.MapFrom(n => n.FGiaTriThuHoiNN))
                .ForMember(n => n.sGhiChu, m => m.MapFrom(n => n.SGhiChu))
                .ForMember(n => n.iLoaiThanhToan, m => m.MapFrom(n => n.ILoaiThanhToan))
                .ForMember(n => n.iID_DuAnId, m => m.MapFrom(n => n.IIdDuAnId))
                .ForMember(n => n.iID_HopDongId, m => m.MapFrom(n => n.IIdHopDongId))
                .ForMember(n => n.iID_NhaThauId, m => m.MapFrom(n => n.IIdNhaThauId))
                .ForMember(n => n.iID_PhanBoVonID, m => m.MapFrom(n => n.IIdPhanBoVonID))
                .ForMember(n => n.dNgayDeNghi, m => m.MapFrom(n => n.DNgayDeNghi))
                .ForMember(n => n.dNgayPheDuyet, m => m.MapFrom(n => n.DNgayPheDuyet))
                .ForMember(n => n.iID_LoaiNguonVonID, m => m.MapFrom(n => n.IIdLoaiNguonVonId))
                .ForMember(n => n.iID_NguonVonID, m => m.MapFrom(n => n.IIdNguonVonId))
                .ForMember(n => n.iID_NhomQuanLyID, m => m.MapFrom(n => n.IIdNhomQuanLyId))
                .ForMember(n => n.iNamKeHoach, m => m.MapFrom(n => n.INamKeHoach));
            CreateMap<VdtTtDeNghiThanhToanModel, VdtTtDeNghiThanhToan>()
                .ForMember(n => n.SSoDeNghi, m => m.MapFrom(n => n.sSoDeNghi))
                .ForMember(n => n.IIdDonViQuanLyId, m => m.MapFrom(n => n.iID_DonViQuanLyID))
                .ForMember(n => n.IIdMaDonViQuanLy, m => m.MapFrom(n => n.iID_MaDonViQuanLy))
                .ForMember(n => n.IIdNhomQuanLyId, m => m.MapFrom(n => n.iID_NhomQuanLyID))
                .ForMember(n => n.SNguoiLap, m => m.MapFrom(n => n.sNguoiLap))
                .ForMember(n => n.FGiaTriThanhToanTN, m => m.MapFrom(n => n.fGiaTriThanhToanTN))
                .ForMember(n => n.FGiaTriThanhToanNN, m => m.MapFrom(n => n.fGiaTriThanhToanNN))
                .ForMember(n => n.FGiaTriThuHoiTN, m => m.MapFrom(n => n.fGiaTriThuHoiTN))
                .ForMember(n => n.FGiaTriThuHoiNN, m => m.MapFrom(n => n.fGiaTriThuHoiNN))
                .ForMember(n => n.SGhiChu, m => m.MapFrom(n => n.sGhiChu))
                .ForMember(n => n.ILoaiThanhToan, m => m.MapFrom(n => n.iLoaiThanhToan))
                .ForMember(n => n.IIdDuAnId, m => m.MapFrom(n => n.iID_DuAnId))
                .ForMember(n => n.IIdHopDongId, m => m.MapFrom(n => n.iID_HopDongId))
                .ForMember(n => n.IIdNhaThauId, m => m.MapFrom(n => n.iID_NhaThauId))
                .ForMember(n => n.IIdPhanBoVonID, m => m.MapFrom(n => n.iID_PhanBoVonID))
                .ForMember(n => n.DNgayDeNghi, m => m.MapFrom(n => n.dNgayDeNghi))
                .ForMember(n => n.DNgayPheDuyet, m => m.MapFrom(n => n.dNgayPheDuyet))
                .ForMember(n => n.IIdLoaiNguonVonId, m => m.MapFrom(n => n.iID_LoaiNguonVonID))
                .ForMember(n => n.IIdNguonVonId, m => m.MapFrom(n => n.iID_NguonVonID))
                .ForMember(n => n.IIdNhomQuanLyId, m => m.MapFrom(n => n.iID_NhomQuanLyID))
                .ForMember(n => n.INamKeHoach, m => m.MapFrom(n => n.iNamKeHoach));

            CreateMap<VdtTtDeNghiThanhToanChiPhiModel, VdtTtDeNghiThanhToanChiPhiQuery>();
            CreateMap<VdtTtDeNghiThanhToanChiPhiQuery, VdtTtDeNghiThanhToanChiPhiModel>();
            CreateMap<VdtTtThongTinCanCu, VdtTtThongTinCanCuModel>();
        }
    }
}
