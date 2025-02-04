using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class VdtKhvKeHoach5NamChiTietMapper : Profile
    {
        public VdtKhvKeHoach5NamChiTietMapper()
        {
            //CreateMap<VdtKhvKeHoachKhthChiTietApprovedQuery, VdtKhvKeHoach5NamChiTietModel>();

            CreateMap<VdtKhvKeHoach5NamChiTietQuery, VdtKhvKeHoach5NamChiTietModel>();

            CreateMap<PlanManageApprovedImportModel, VdtKhvKeHoach5NamChiTietModel>()
                .ForMember(entity => entity.STen, model => model.MapFrom(x => x.STenDuAn))
                .ForMember(entity => entity.IIdLoaiCongTrinhId, model => model.MapFrom(x => !string.IsNullOrEmpty(x.IIdLoaiCongTrinhId) ? x.IIdLoaiCongTrinhId : null))
                .ForMember(entity => entity.SDiaDiem, model => model.MapFrom(x => x.SDiaDiemThucHien))
                .ForMember(entity => entity.IIdNguonVonId, model => model.MapFrom(x => !string.IsNullOrEmpty(x.IMaNguonVon) ? int.Parse(x.IMaNguonVon) : 0))
                .ForMember(entity => entity.FHanMucDauTu, model => model.MapFrom(x => string.IsNullOrEmpty(x.FHanMucDuToan) ? 0 : double.Parse(x.FHanMucDuToan)))
                .ForMember(entity => entity.FVonDaGiao, model => model.MapFrom(x => string.IsNullOrEmpty(x.FVonDaGiao) ? 0 : double.Parse(x.FVonDaGiao)))
                .ForMember(entity => entity.FVonBoTriTuNamDenNam, model => model.MapFrom(x => string.IsNullOrEmpty(x.FVonBoTriGiaiDoan) ? 0 : double.Parse(x.FVonBoTriGiaiDoan)))
                .ForMember(entity => entity.FGiaTriSau5Nam, model => model.MapFrom(x => string.IsNullOrEmpty(x.FVonBoTriSauNam) ? 0 : double.Parse(x.FVonBoTriSauNam)))
                .ForMember(entity => entity.Status, model => model.MapFrom(x => x.ImportStatus))
                .ForMember(entity => entity.ThoiGianDuocDuyet, model => model.MapFrom(x => x.ThoiGianThucHien))
                .ForMember(entity => entity.Error, model => model.MapFrom(x => x.IsError))
                .ForMember(entity => entity.IIdDuAnId, model => model.MapFrom(x => !string.IsNullOrEmpty(x.IIdDuAnId) ? Guid.Parse(x.IIdDuAnId) : Guid.Empty))
                .ForMember(entity => entity.Id, model => model.MapFrom(x => !string.IsNullOrEmpty(x.Id) ? Guid.Parse(x.Id) : Guid.Empty))
                .ForMember(entity => entity.IIdParentId, model => model.MapFrom(x => !string.IsNullOrEmpty(x.IIdParentId) ? Guid.Parse(x.IIdParentId) : Guid.Empty))
                .ForMember(entity => entity.IIdKeHoach5NamId, model => model.MapFrom(x => x.IdChungTu))
                .ForMember(entity => entity.IIdDonViId, model => model.MapFrom(x => !string.IsNullOrEmpty(x.IIdDonViThucHienDuAn) ? x.IIdDonViThucHienDuAn : null))
                .ForMember(entity => entity.IIdMaDonVi, model => model.MapFrom(x => x.IIdMaDonViThucHienDuAn));

            CreateMap<VdtKhvKeHoach5NamChiTiet, VdtKhvKeHoach5NamChiTietModel>()
                .ForMember(entity => entity.FGiaTriSau5Nam, model => model.MapFrom(x => x.FGiaTriBoTri))
                .ForMember(entity => entity.IIdDonViId, model => model.MapFrom(x => x.IIdDonViQuanLyId));

            CreateMap<VdtKhvKeHoach5NamChiTietModel, VdtKhvKeHoach5NamChiTiet>()
                .ForMember(entity => entity.FGiaTriBoTri, model => model.MapFrom(x => x.FGiaTriSau5Nam))
                .ForMember(entity => entity.IIdDonViQuanLyId, model => model.MapFrom(x => x.IIdDonViId));
            
            CreateMap<DuAnKeHoachTrungHanModel, DuAnKeHoachTrungHanQuery>();
            CreateMap<DuAnKeHoachTrungHanQuery, DuAnKeHoachTrungHanModel>();
            CreateMap<DuAnKeHoachTrungHanModel, VdtKhvKeHoach5NamChiTietModel>()
                .ForMember(entity => entity.Id, model => model.MapFrom(x => x.IIdKeHoach5NamChiTietId))
                .ForMember(entity => entity.STrangThai, model => model.MapFrom(x => x.ILoaiDuAn.ToString()));
                //.ForMember(entity => entity.IIdMaDonVi, model => model.MapFrom(x => x.IIdMaDonVi));
            CreateMap<VdtKhvKeHoach5NamChiTietModel, DuAnKeHoachTrungHanModel>();

            CreateMap<DuAnKeHoachTrungHanModel, VdtDaDuAn>()
                .ForMember(n => n.Id, m => m.MapFrom(n => n.IIdDuAnId))
                .ForMember(n => n.IIdMaDonViQuanLy, m => m.MapFrom(n => n.IIdMaDonVi))
                .ForMember(n => n.IIdDonViQuanLyId, m => m.MapFrom(n => n.IIdDonViId))
                .ForMember(n => n.IIdMaChuDauTuId, m => m.MapFrom(n => n.IIdMaChuDauTu))
                .ForMember(n => n.IIdChuDauTuId, m => m.MapFrom(n => n.IIdChuDauTuId));

            CreateMap<VdtDaDuAn, DuAnKeHoachTrungHanModel>()
                .ForMember(n => n.IIdDuAnId, m => m.MapFrom(n => n.Id));

            CreateMap<VdtKhvKeHoach5NamExportQuery, VdtKeHoachTrungHanExportModel>();
            //CreateMap<ReportKeHoach5NamModel, ReportKeHoach5NamQuery>();
            //CreateMap<ReportKeHoach5NamQuery, ReportKeHoach5NamModel>();
            CreateMap<VdtKhvKeHoach5NamDeXuatChiTietModel, VdtKhvKeHoach5NamChiTietModel>()
                .ForMember(n => n.STenDuAn, m => m.MapFrom(n => n.STen));
        }
    }
}
