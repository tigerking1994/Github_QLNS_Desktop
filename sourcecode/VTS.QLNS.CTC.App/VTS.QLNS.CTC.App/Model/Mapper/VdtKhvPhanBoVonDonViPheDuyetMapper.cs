using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class VdtKhvPhanBoVonDonViPheDuyetMapper : Profile
    {
        public VdtKhvPhanBoVonDonViPheDuyetMapper()
        {
            CreateMap<PhanBoVonDonViPheDuyetQuery, VdtKhvPhanBoVonDonViPheDuyetModel>();
            CreateMap<VdtKhvPhanBoVonDonViPheDuyet, VdtKhvPhanBoVonDonViPheDuyetModel>()
                .ForMember(n => n.bActive, m => m.MapFrom(m => m.BActive))
                .ForMember(n => n.bIsGoc, m => m.MapFrom(m => m.BIsGoc))
                .ForMember(n => n.bLaThayThe, m => m.MapFrom(m => m.BLaThayThe))
                .ForMember(n => n.dDateCreate, m => m.MapFrom(m => m.DDateCreate))
                .ForMember(n => n.dDateDelete, m => m.MapFrom(m => m.DDateDelete))
                .ForMember(n => n.dDateUpdate, m => m.MapFrom(m => m.DDateUpdate))
                .ForMember(n => n.dNgayQuyetDinh, m => m.MapFrom(m => m.DNgayQuyetDinh))
                .ForMember(n => n.fGiaTriThuHoi, m => m.MapFrom(m => m.FGiaTriThuHoi))
                .ForMember(n => n.fGiaTrPhanBo, m => m.MapFrom(m => m.FGiaTrPhanBo))
                .ForMember(n => n.fTiGia, m => m.MapFrom(m => m.FTiGia))
                .ForMember(n => n.fTiGiaDonVi, m => m.MapFrom(m => m.FTiGiaDonVi))
                .ForMember(n => n.iId_DonViQuanLyId, m => m.MapFrom(m => m.IIdDonViQuanLyId))
                .ForMember(n => n.iID_MaDonViQuanLy, m => m.MapFrom(m => m.IIdMaDonViQuanLy))
                .ForMember(n => n.iId_DonViTienTeId, m => m.MapFrom(m => m.IIdDonViTienTeId))
                .ForMember(n => n.iId_LoaiNguonVonId, m => m.MapFrom(m => m.IIdLoaiNguonVonId))
                .ForMember(n => n.iId_NguonVonId, m => m.MapFrom(m => m.IIdNguonVonId))
                .ForMember(n => n.iId_ParentId, m => m.MapFrom(m => m.IIdParentId))
                .ForMember(n => n.iId_TienTeId, m => m.MapFrom(m => m.IIdTienTeId))
                .ForMember(n => n.iNamKeHoach, m => m.MapFrom(m => m.INamKeHoach))
                .ForMember(n => n.sLoaiDieuChinh, m => m.MapFrom(m => m.SLoaiDieuChinh))
                .ForMember(n => n.sSoQuyetDinh, m => m.MapFrom(m => m.SSoQuyetDinh))
                .ForMember(n => n.sUserCreate, m => m.MapFrom(m => m.SUserCreate))
                .ForMember(n => n.sUserDelete, m => m.MapFrom(m => m.SUserDelete))
                .ForMember(n => n.sUserUpdate, m => m.MapFrom(m => m.SUserUpdate));

            CreateMap<VdtKhvPhanBoVonDonViPheDuyetModel, VdtKhvPhanBoVonDonViPheDuyet>()
                .ForMember(n => n.BActive, m => m.MapFrom(m => m.bActive))
                .ForMember(n => n.BIsGoc, m => m.MapFrom(m => m.bIsGoc))
                .ForMember(n => n.BLaThayThe, m => m.MapFrom(m => m.bLaThayThe))
                .ForMember(n => n.DDateCreate, m => m.MapFrom(m => m.dDateCreate))
                .ForMember(n => n.DDateDelete, m => m.MapFrom(m => m.dDateDelete))
                .ForMember(n => n.DDateUpdate, m => m.MapFrom(m => m.dDateUpdate))
                .ForMember(n => n.DNgayQuyetDinh, m => m.MapFrom(m => m.dNgayQuyetDinh))
                .ForMember(n => n.FGiaTriThuHoi, m => m.MapFrom(m => m.fGiaTriThuHoi))
                .ForMember(n => n.FGiaTrPhanBo, m => m.MapFrom(m => m.fGiaTrPhanBo))
                .ForMember(n => n.FTiGia, m => m.MapFrom(m => m.fTiGia))
                .ForMember(n => n.FTiGiaDonVi, m => m.MapFrom(m => m.fTiGiaDonVi))
                .ForMember(n => n.IIdDonViQuanLyId, m => m.MapFrom(m => m.iId_DonViQuanLyId))
                .ForMember(n => n.IIdMaDonViQuanLy, m => m.MapFrom(m => m.iID_MaDonViQuanLy))
                .ForMember(n => n.IIdDonViTienTeId, m => m.MapFrom(m => m.iId_DonViTienTeId))
                .ForMember(n => n.IIdLoaiNguonVonId, m => m.MapFrom(m => m.iId_LoaiNguonVonId))
                .ForMember(n => n.IIdNguonVonId, m => m.MapFrom(m => m.iId_NguonVonId))
                .ForMember(n => n.IIdParentId, m => m.MapFrom(m => m.iId_ParentId))
                .ForMember(n => n.IIdTienTeId, m => m.MapFrom(m => m.iId_TienTeId))
                .ForMember(n => n.INamKeHoach, m => m.MapFrom(m => m.iNamKeHoach))
                .ForMember(n => n.SLoaiDieuChinh, m => m.MapFrom(m => m.sLoaiDieuChinh))
                .ForMember(n => n.SSoQuyetDinh, m => m.MapFrom(m => m.sSoQuyetDinh))
                .ForMember(n => n.SUserCreate, m => m.MapFrom(m => m.sUserCreate))
                .ForMember(n => n.SUserDelete, m => m.MapFrom(m => m.sUserDelete))
                .ForMember(n => n.SUserUpdate, m => m.MapFrom(m => m.sUserUpdate));

            CreateMap<VdtKhvPhanBoVonDonViPheDuyet, ComboboxItem>()
                .ForMember(n => n.DisplayItem, m => m.MapFrom(n => string.Format("{0}-{1}", n.SSoQuyetDinh, n.INamKeHoach)))
                .ForMember(n => n.ValueItem, m => m.MapFrom(n => n.Id));
        }
    }
}