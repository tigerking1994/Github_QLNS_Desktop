using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class VdtKhvPhanBoVonMapper : Profile
    {
        public VdtKhvPhanBoVonMapper()
        {
            CreateMap<PhanBoVonQuery, VdtKhvPhanBoVon>()
            .ForMember(entity => entity.BActive, model => model.MapFrom(n => n.bActive))
            .ForMember(entity => entity.BIsCanBoDuyet, model => model.MapFrom(n => n.bIsCanBoDuyet))
            .ForMember(entity => entity.BIsDuyet, model => model.MapFrom(n => n.bIsDuyet))
            .ForMember(entity => entity.BIsGoc, model => model.MapFrom(n => n.bIsGoc))
            .ForMember(entity => entity.BLaThayThe, model => model.MapFrom(n => n.bLaThayThe))
            .ForMember(entity => entity.DDateCreate, model => model.MapFrom(n => n.dDateCreate))
            .ForMember(entity => entity.DDateDelete, model => model.MapFrom(n => n.dDateDelete))
            .ForMember(entity => entity.DDateUpdate, model => model.MapFrom(n => n.dDateUpdate))
            .ForMember(entity => entity.DNgayQuyetDinh, model => model.MapFrom(n => n.dNgayQuyetDinh))
            .ForMember(entity => entity.FGiaTrDeNghi, model => model.MapFrom(n => n.fGiaTrDeNghi))
            .ForMember(entity => entity.FGiaTriThuHoi, model => model.MapFrom(n => n.fGiaTriThuHoi))
            .ForMember(entity => entity.FGiaTrPhanBo, model => model.MapFrom(n => n.fGiaTrPhanBo))
            .ForMember(entity => entity.FTiGia, model => model.MapFrom(n => n.fTiGia))
            .ForMember(entity => entity.FTiGiaDonVi, model => model.MapFrom(n => n.fTiGiaDonVi))
            .ForMember(entity => entity.IIdDonViQuanLyId, model => model.MapFrom(n => n.iId_DonViQuanLyId))
            .ForMember(entity => entity.IIDMaDonViQuanLy, model => model.MapFrom(n => n.iID_MaDonViQuanLy))
            .ForMember(entity => entity.IIdDonViTienTeId, model => model.MapFrom(n => n.iId_DonViTienTeId))
            .ForMember(entity => entity.IIdLoaiNguonVonId, model => model.MapFrom(n => n.iId_LoaiNguonVonId))
            .ForMember(entity => entity.IIdNguonVonId, model => model.MapFrom(n => n.iId_NguonVonId))
            .ForMember(entity => entity.IIdNhomQuanLyId, model => model.MapFrom(n => n.iId_NhomQuanLyId))
            .ForMember(entity => entity.IIdParentId, model => model.MapFrom(n => n.iId_ParentId))
            .ForMember(entity => entity.IIdTienTeId, model => model.MapFrom(n => n.iId_TienTeId))
            .ForMember(entity => entity.INamKeHoach, model => model.MapFrom(n => n.iNamKeHoach))
            .ForMember(entity => entity.SLoaiDieuChinh, model => model.MapFrom(n => n.sLoaiDieuChinh))
            .ForMember(entity => entity.SSoQuyetDinh, model => model.MapFrom(n => n.sSoQuyetDinh))
            .ForMember(entity => entity.SUserCreate, model => model.MapFrom(n => n.sUserCreate))
            .ForMember(entity => entity.SUserDelete, model => model.MapFrom(n => n.sUserDelete))
            .ForMember(entity => entity.SUserUpdate, model => model.MapFrom(n => n.sUserUpdate));

            CreateMap<PhanBoVonQuery, PhanBoVonModel>();

            CreateMap<VdtKhvPhanBoVon, PhanBoVonModel>()
                .ForMember(n => n.bActive, m => m.MapFrom(m => m.BActive))
                .ForMember(n => n.bIsCanBoDuyet, m => m.MapFrom(m => m.BIsCanBoDuyet))
                .ForMember(n => n.bIsDuyet, m => m.MapFrom(m => m.BIsDuyet))
                .ForMember(n => n.bIsGoc, m => m.MapFrom(m => m.BIsGoc))
                .ForMember(n => n.bLaThayThe, m => m.MapFrom(m => m.BLaThayThe))
                .ForMember(n => n.dDateCreate, m => m.MapFrom(m => m.DDateCreate))
                .ForMember(n => n.dDateDelete, m => m.MapFrom(m => m.DDateDelete))
                .ForMember(n => n.dDateUpdate, m => m.MapFrom(m => m.DDateUpdate))
                .ForMember(n => n.dNgayQuyetDinh, m => m.MapFrom(m => m.DNgayQuyetDinh))
                .ForMember(n => n.fGiaTrDeNghi, m => m.MapFrom(m => m.FGiaTrDeNghi))
                .ForMember(n => n.fGiaTriThuHoi, m => m.MapFrom(m => m.FGiaTriThuHoi))
                .ForMember(n => n.fGiaTrPhanBo, m => m.MapFrom(m => m.FGiaTrPhanBo))
                .ForMember(n => n.fTiGia, m => m.MapFrom(m => m.FTiGia))
                .ForMember(n => n.fTiGiaDonVi, m => m.MapFrom(m => m.FTiGiaDonVi))
                .ForMember(n => n.iId_DonViQuanLyId, m => m.MapFrom(m => m.IIdDonViQuanLyId))
                .ForMember(n => n.iID_MaDonViQuanLy, m => m.MapFrom(m => m.IIDMaDonViQuanLy))
                .ForMember(n => n.iId_DonViTienTeId, m => m.MapFrom(m => m.IIdDonViTienTeId))
                .ForMember(n => n.iId_KhoanNganSachId, m => m.MapFrom(m => m.IIdKhoanNganSachId))
                .ForMember(n => n.iId_LoaiNganSachId, m => m.MapFrom(m => m.IIdLoaiNganSachId))
                .ForMember(n => n.iId_LoaiNguonVonId, m => m.MapFrom(m => m.IIdLoaiNguonVonId))
                .ForMember(n => n.iId_NguonVonId, m => m.MapFrom(m => m.IIdNguonVonId))
                .ForMember(n => n.iId_NhomQuanLyId, m => m.MapFrom(m => m.IIdNhomQuanLyId))
                .ForMember(n => n.iId_ParentId, m => m.MapFrom(m => m.IIdParentId))
                .ForMember(n => n.iId_TienTeId, m => m.MapFrom(m => m.IIdTienTeId))
                .ForMember(n => n.iNamKeHoach, m => m.MapFrom(m => m.INamKeHoach))
                .ForMember(n => n.sLoaiDieuChinh, m => m.MapFrom(m => m.SLoaiDieuChinh))
                .ForMember(n => n.sSoQuyetDinh, m => m.MapFrom(m => m.SSoQuyetDinh))
                .ForMember(n => n.sUserCreate, m => m.MapFrom(m => m.SUserCreate))
                .ForMember(n => n.sUserDelete, m => m.MapFrom(m => m.SUserDelete))
                .ForMember(n => n.sUserUpdate, m => m.MapFrom(m => m.SUserUpdate));

            CreateMap<PhanBoVonModel, VdtKhvPhanBoVon>()
                   .ForMember(n => n.BActive, m => m.MapFrom(m => m.bActive))
                   .ForMember(n => n.BIsCanBoDuyet, m => m.MapFrom(m => m.bIsCanBoDuyet))
                   .ForMember(n => n.BIsDuyet, m => m.MapFrom(m => m.bIsDuyet))
                   .ForMember(n => n.BIsGoc, m => m.MapFrom(m => m.bIsGoc))
                   .ForMember(n => n.BLaThayThe, m => m.MapFrom(m => m.bLaThayThe))
                   .ForMember(n => n.DDateCreate, m => m.MapFrom(m => m.dDateCreate))
                   .ForMember(n => n.DDateDelete, m => m.MapFrom(m => m.dDateDelete))
                   .ForMember(n => n.DDateUpdate, m => m.MapFrom(m => m.dDateUpdate))
                   .ForMember(n => n.DNgayQuyetDinh, m => m.MapFrom(m => m.dNgayQuyetDinh))
                   .ForMember(n => n.FGiaTrDeNghi, m => m.MapFrom(m => m.fGiaTrDeNghi))
                   .ForMember(n => n.FGiaTriThuHoi, m => m.MapFrom(m => m.fGiaTriThuHoi))
                   .ForMember(n => n.FGiaTrPhanBo, m => m.MapFrom(m => m.fGiaTrPhanBo))
                   .ForMember(n => n.FTiGia, m => m.MapFrom(m => m.fTiGia))
                   .ForMember(n => n.FTiGiaDonVi, m => m.MapFrom(m => m.fTiGiaDonVi))
                   .ForMember(n => n.IIdDonViQuanLyId, m => m.MapFrom(m => m.iId_DonViQuanLyId))
                   .ForMember(n => n.IIDMaDonViQuanLy, m => m.MapFrom(m => m.iID_MaDonViQuanLy))
                   .ForMember(n => n.IIdDonViTienTeId, m => m.MapFrom(m => m.iId_DonViTienTeId))
                   .ForMember(n => n.IIdKhoanNganSachId, m => m.MapFrom(m => m.iId_KhoanNganSachId))
                   .ForMember(n => n.IIdLoaiNganSachId, m => m.MapFrom(m => m.iId_LoaiNganSachId))
                   .ForMember(n => n.IIdLoaiNguonVonId, m => m.MapFrom(m => m.iId_LoaiNguonVonId))
                   .ForMember(n => n.IIdNguonVonId, m => m.MapFrom(m => m.iId_NguonVonId))
                   .ForMember(n => n.IIdNhomQuanLyId, m => m.MapFrom(m => m.iId_NhomQuanLyId))
                   .ForMember(n => n.IIdParentId, m => m.MapFrom(m => m.iId_ParentId))
                   .ForMember(n => n.IIdTienTeId, m => m.MapFrom(m => m.iId_TienTeId))
                   .ForMember(n => n.INamKeHoach, m => m.MapFrom(m => m.iNamKeHoach))
                   .ForMember(n => n.SLoaiDieuChinh, m => m.MapFrom(m => m.sLoaiDieuChinh))
                   .ForMember(n => n.SSoQuyetDinh, m => m.MapFrom(m => m.sSoQuyetDinh))
                   .ForMember(n => n.SUserCreate, m => m.MapFrom(m => m.sUserCreate))
                   .ForMember(n => n.SUserDelete, m => m.MapFrom(m => m.sUserDelete))
                   .ForMember(n => n.SUserUpdate, m => m.MapFrom(m => m.sUserUpdate));

            CreateMap<RptAnnualBudgetAllocationQuery, RptAnnualBudgetAllocationModel>();
            CreateMap<RptAnnualBudgetAllocationModel, RptAnnualBudgetAllocationQuery>();
            CreateMap<PhanBoVonModel, YearPlanManagerExportCriteria>()
                .ForMember(n => n.Id, m => m.MapFrom(m => m.Id));
            CreateMap<VdtKhvPhanBoVon, ComboboxItem>()
                .ForMember(n => n.DisplayItem, m => m.MapFrom(n => string.Format("{0}-{1}", n.SSoQuyetDinh, n.INamKeHoach)))
                .ForMember(n => n.ValueItem, m => m.MapFrom(n => n.Id));
        }
    }
}
