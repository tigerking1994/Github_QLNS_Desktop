using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Report;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class VdtQtDeNghiQuyetToanNienDoMapper : Profile
    {
        public VdtQtDeNghiQuyetToanNienDoMapper()
        {
            CreateMap<VdtQtBcQuyetToanNienDoPhanTichQuery, VdtQtBcQuyetToanNienDoPhanTichModel>();
            CreateMap<VdtQtBcQuyetToanNienDoPhanTichModel, VdtQtBcQuyetToanNienDoPhanTichQuery>();

            CreateMap<VdtQtBcQuyetToanNienDoPhanTichQuery, RptVdtQtBcQuyetToanNienDoPhanTichModel>();
            CreateMap<RptVdtQtBcQuyetToanNienDoPhanTichModel, VdtQtBcQuyetToanNienDoPhanTichQuery>();

            CreateMap<VdtQtDenghiQuyetToanNienDoQuery, VdtQtDenghiQuyetToanNienDoModel>();
            CreateMap<VdtQtDenghiQuyetToanNienDoModel, VdtQtDenghiQuyetToanNienDoQuery>();

            CreateMap<VdtQtDeNghiQuyetToanNienDo, VdtQtDenghiQuyetToanNienDoModel>()
                .ForMember(n => n.dDateCreate, m => m.MapFrom(n => n.DDateCreate))
                .ForMember(n => n.dDateDelete, m => m.MapFrom(n => n.DDateDelete))
                .ForMember(n => n.dDateUpdate, m => m.MapFrom(n => n.DDateUpdate))
                .ForMember(n => n.dNgayDeNghi, m => m.MapFrom(n => n.DNgayDeNghi))
                .ForMember(n => n.iID_DonViDeNghiID, m => m.MapFrom(n => n.IIdDonViDeNghiId))
                .ForMember(n => n.iID_MaDonViDeNghi, m => m.MapFrom(n => n.IIDMaDonViDeNghi))
                .ForMember(n => n.iID_LoaiNguonVonID, m => m.MapFrom(n => n.IIdLoaiNguonVonId))
                .ForMember(n => n.iID_NguonVonID, m => m.MapFrom(n => n.IIdNguonVonId))
                .ForMember(n => n.iNamKeHoach, m => m.MapFrom(n => n.INamKeHoach))
                .ForMember(n => n.sNguoiDeNghi, m => m.MapFrom(n => n.SNguoiDeNghi))
                .ForMember(n => n.sSoDeNghi, m => m.MapFrom(n => n.SSoDeNghi))
                .ForMember(n => n.sUserCreate, m => m.MapFrom(n => n.SUserCreate))
                .ForMember(n => n.sUserDelete, m => m.MapFrom(n => n.SUserDelete))
                .ForMember(n => n.sUserUpdate, m => m.MapFrom(n => n.SUserUpdate));

            CreateMap<VdtQtDenghiQuyetToanNienDoModel, VdtQtDeNghiQuyetToanNienDo>()
                .ForMember(n => n.DDateDelete, m => m.MapFrom(n => n.dDateDelete))
                .ForMember(n => n.DDateUpdate, m => m.MapFrom(n => n.dDateUpdate))
                .ForMember(n => n.DNgayDeNghi, m => m.MapFrom(n => n.dNgayDeNghi))
                .ForMember(n => n.IIdDonViDeNghiId, m => m.MapFrom(n => n.iID_DonViDeNghiID))
                .ForMember(n => n.IIDMaDonViDeNghi, m => m.MapFrom(n => n.iID_MaDonViDeNghi))
                .ForMember(n => n.IIdLoaiNguonVonId, m => m.MapFrom(n => n.iID_LoaiNguonVonID))
                .ForMember(n => n.IIdNguonVonId, m => m.MapFrom(n => n.iID_NguonVonID))
                .ForMember(n => n.INamKeHoach, m => m.MapFrom(n => n.iNamKeHoach))
                .ForMember(n => n.SNguoiDeNghi, m => m.MapFrom(n => n.sNguoiDeNghi))
                .ForMember(n => n.SSoDeNghi, m => m.MapFrom(n => n.sSoDeNghi))
                .ForMember(n => n.SUserCreate, m => m.MapFrom(n => n.sUserCreate))
                .ForMember(n => n.SUserDelete, m => m.MapFrom(n => n.sUserDelete))
                .ForMember(n => n.SUserUpdate, m => m.MapFrom(n => n.sUserUpdate));

            CreateMap<VdtQtDenghiQuyetToanNienDoChiTietModel, VdtQtDenghiQuyetToanNienDoChiTietQuery>();
            CreateMap<VdtQtDenghiQuyetToanNienDoChiTietQuery, VdtQtDenghiQuyetToanNienDoChiTietModel>();

            CreateMap<VdtQtDenghiQuyetToanNienDoChiTietModel, VdtQtDeNghiQuyetToanNienDoChiTiet>()
                .ForMember(n => n.FGiaTriQuyetToanNamNay, m => m.MapFrom(n => n.fGiaTriQuyetToanNamNay))
                .ForMember(n => n.FGiaTriQuyetToanNamNayDonVi, m => m.MapFrom(n => n.fGiaTriQuyetToanNamNayDonVi))
                .ForMember(n => n.FGiaTriQuyetToanNamTruoc, m => m.MapFrom(n => n.fGiaTriQuyetToanNamTruoc))
                .ForMember(n => n.FGiaTriQuyetToanNamTruocDonVi, m => m.MapFrom(n => n.fGiaTriQuyetToanNamTruocDonVi))
                .ForMember(n => n.IIdDeNghiQuyetToanNienDoId, m => m.MapFrom(n => n.iID_DeNghiQuyetToanNienDoId))
                .ForMember(n => n.IIdDonViTienTeId, m => m.MapFrom(n => n.iId_DonViTienTeId))
                .ForMember(n => n.IIdDuAnId, m => m.MapFrom(n => n.iID_DuAnId))
                .ForMember(n => n.IIdMucId, m => m.MapFrom(n => n.iId_MucId))
                .ForMember(n => n.IIdNganhId, m => m.MapFrom(n => n.iId_NganhId))
                .ForMember(n => n.IIdTienTeId, m => m.MapFrom(n => n.iId_TienTeId))
                .ForMember(n => n.IIdTietMucId, m => m.MapFrom(n => n.iId_TietMucId))
                .ForMember(n => n.IIdTieuMucId, m => m.MapFrom(n => n.iId_TieuMucId))
                .ForMember(n => n.MTiGia, m => m.MapFrom(n => n.mTiGia))
                .ForMember(n => n.MTiGiaDonVi, m => m.MapFrom(n => n.mTiGiaDonVi));

            CreateMap<VdtQtDeNghiQuyetToanNienDoChiTiet, VdtQtDenghiQuyetToanNienDoChiTietModel>()
                .ForMember(n => n.fGiaTriQuyetToanNamNay, m => m.MapFrom(n => n.FGiaTriQuyetToanNamNay))
                .ForMember(n => n.fGiaTriQuyetToanNamNayDonVi, m => m.MapFrom(n => n.FGiaTriQuyetToanNamNayDonVi))
                .ForMember(n => n.fGiaTriQuyetToanNamTruoc, m => m.MapFrom(n => n.FGiaTriQuyetToanNamTruoc))
                .ForMember(n => n.fGiaTriQuyetToanNamTruocDonVi, m => m.MapFrom(n => n.FGiaTriQuyetToanNamTruocDonVi))
                .ForMember(n => n.iID_DeNghiQuyetToanNienDoId, m => m.MapFrom(n => n.IIdDeNghiQuyetToanNienDoId))
                .ForMember(n => n.iId_DonViTienTeId, m => m.MapFrom(n => n.IIdDonViTienTeId))
                .ForMember(n => n.iID_DuAnId, m => m.MapFrom(n => n.IIdDuAnId))
                .ForMember(n => n.iId_MucId, m => m.MapFrom(n => n.IIdMucId))
                .ForMember(n => n.iId_NganhId, m => m.MapFrom(n => n.IIdNganhId))
                .ForMember(n => n.iId_TienTeId, m => m.MapFrom(n => n.IIdTienTeId))
                .ForMember(n => n.iId_TietMucId, m => m.MapFrom(n => n.IIdTietMucId))
                .ForMember(n => n.iId_TieuMucId, m => m.MapFrom(n => n.IIdTieuMucId))
                .ForMember(n => n.mTiGia, m => m.MapFrom(n => n.MTiGia))
                .ForMember(n => n.mTiGiaDonVi, m => m.MapFrom(n => n.MTiGiaDonVi));

            CreateMap<VdtQtDenghiQuyetToanNienDoChiTietModel, VdtQtXuLySoLieu>()
                .ForMember(n => n.IIdDuAnId, m => m.MapFrom(n => n.iID_DuAnId))
                .ForMember(n => n.FBuTruThuaThieu, m => m.MapFrom(n => n.fBuTruThuaThieu))
                .ForMember(n => n.FThuLaiKeHoachNamNay, m => m.MapFrom(n => n.fThuLaiKeHoachNamNay))
                .ForMember(n => n.FThuUng, m => m.MapFrom(n => n.fThuUng))
                .ForMember(n => n.FThuThanhKhoan, m => m.MapFrom(n => n.fThuThanhKhoan))
                .ForMember(n => n.FThuLaiKeHoachNamTruoc, m => m.MapFrom(n => n.fThuLaiKeHoachNamTruoc))
                .ForMember(n => n.FThuThanhKhoanNamTruoc, m => m.MapFrom(n => n.fThuThanhKhoanNamTruoc))
                .ForMember(n => n.IIdMucId, m => m.MapFrom(n => n.iId_MucId))
                .ForMember(n => n.IIdTieuMucId, m => m.MapFrom(n => n.iId_TieuMucId))
                .ForMember(n => n.IIdTietMucId, m => m.MapFrom(n => n.iId_TietMucId))
                .ForMember(n => n.IIdNganhId, m => m.MapFrom(n => n.iId_NganhId))
                .ForMember(n => n.IIdDonViTienTeId, m => m.MapFrom(n => n.iId_DonViTienTeId))
                .ForMember(n => n.IIdTienTeId, m => m.MapFrom(n => n.iId_TienTeId))
                .ForMember(n => n.FTiGia, m => m.MapFrom(n => n.mTiGia))
                .ForMember(n => n.FTiGiaDonVi, m => m.MapFrom(n => n.mTiGiaDonVi));

            CreateMap<BaoCaoKetQuaGiaiNganChiKPDTModel, VdtBaoCaoKetQuaGiaiNganChiKPDTQuery>();
            CreateMap<VdtBaoCaoKetQuaGiaiNganChiKPDTQuery, BaoCaoKetQuaGiaiNganChiKPDTModel>();
        }
    }
}
