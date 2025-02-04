using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class VdtKhvKeHoach5NamDeXuatChiTietMapper : Profile
    {
        public VdtKhvKeHoach5NamDeXuatChiTietMapper()
        {
            CreateMap<DuAnTrungHanDeXuatQuery, DuAnKeHoachTrungHanDeXuatModel>()
                .ForMember(entity => entity.IIdDuAnId, model => model.MapFrom(x => x.IDDuAnID));

            CreateMap<VdtKhvKeHoach5NamDeXuatChiTietQuery, VdtKhvKeHoach5NamDeXuatChiTiet>()
                .ForMember(n => n.IIdDonViQuanLyId, m => m.MapFrom(n => n.IIdDonViId));
            //CreateMap<VdtKhvKeHoachTrungHanDeXuatChiTietModifiedQuery, VdtKhvKeHoach5NamDeXuatChiTietModel>();

            CreateMap<VdtKhvKeHoach5NamDeXuatChiTietModel, VdtDaDuAn>()
                .ForMember(entity => entity.IdDuAnKhthDeXuat, model => model.MapFrom(x => x.Id));

            CreateMap<VdtKhvKeHoach5NamDeXuatChiTietQuery, VdtKhvKeHoach5NamDeXuatChiTietModel>();
            CreateMap<VdtKhvKeHoach5NamDeXuatChiTiet, VdtKhvKeHoach5NamDeXuatChiTietModel>()
                .ForMember(n => n.IIdDonViId, model => model.MapFrom(n => n.IIdDonViQuanLyId));
            CreateMap<VdtKhvKeHoach5NamDeXuatChiTietModel, VdtKhvKeHoach5NamDeXuatChiTiet>()
                .ForMember(n => n.IIdDonViQuanLyId, model => model.MapFrom(n => n.IIdDonViId));

            CreateMap<KeHoach5NamDeXuatImportModel, VdtKhvKeHoach5NamDeXuatChiTietModel>()
               .ForMember(n => n.FGiaTriBoTri, m => m.MapFrom(n => double.Parse((string.IsNullOrEmpty(n.FGiaTriBoTri) ? "0" : n.FGiaTriBoTri))))
               .ForMember(n => n.FGiaTriNamThuBa, m => m.MapFrom(n => double.Parse((string.IsNullOrEmpty(n.FGiaTriNamThuBa) ? "0" : n.FGiaTriNamThuBa))))
               .ForMember(n => n.FGiaTriNamThuHai, m => m.MapFrom(n => double.Parse((string.IsNullOrEmpty(n.FGiaTriNamThuHai) ? "0" : n.FGiaTriNamThuHai))))
               .ForMember(n => n.FGiaTriNamThuNam, m => m.MapFrom(n => double.Parse((string.IsNullOrEmpty(n.FGiaTriNamThuNam) ? "0" : n.FGiaTriNamThuNam))))
               .ForMember(n => n.FGiaTriNamThuNhat, m => m.MapFrom(n => double.Parse((string.IsNullOrEmpty(n.FGiaTriNamThuNhat) ? "0" : n.FGiaTriNamThuNhat))))
               .ForMember(n => n.FGiaTriNamThuTu, m => m.MapFrom(n => double.Parse((string.IsNullOrEmpty(n.FGiaTriNamThuTu) ? "0" : n.FGiaTriNamThuTu))))
               .ForMember(n => n.FHanMucDauTu, m => m.MapFrom(n => double.Parse((string.IsNullOrEmpty(n.FHanMucDauTu) ? "0" : n.FHanMucDauTu))))
               .ForMember(n => n.IIdNguonVonId, m => m.MapFrom(n => !string.IsNullOrEmpty(n.IIdNguonVonId) ? int.Parse(n.IIdNguonVonId) : 0))
               .ForMember(n => n.IdReference, m => m.MapFrom(n => string.IsNullOrEmpty(n.IdReference) ? null : n.IdReference))
               .ForMember(n => n.Id, m => m.MapFrom(n => Guid.Parse(n.IdOld)))
               .ForMember(n => n.IdParent, m => m.MapFrom(n => string.IsNullOrEmpty(n.IdParentOld) ? null : n.IdParentOld))
               .ForMember(n => n.IndexCode, m => m.MapFrom(n => string.IsNullOrEmpty(n.IndexCode) ? 0 : Int32.Parse(n.IndexCode)))
               .ForMember(n => n.IsStatus, m => m.MapFrom(n => string.IsNullOrEmpty(n.IsStatus) ? 0 : Int32.Parse(n.IsStatus)))
               .ForMember(n => n.Level, m => m.MapFrom(n => string.IsNullOrEmpty(n.Level) ? 0 : Int32.Parse(n.Level)))
               .ForMember(n => n.IIdLoaiCongTrinhId, m => m.MapFrom(n => string.IsNullOrEmpty(n.IIdLoaiCongTrinhId) ? null : n.IIdLoaiCongTrinhId))
               .ForMember(n => n.IIdDonViId, m => m.MapFrom(n => string.IsNullOrEmpty(n.IIdDonViId) ? null : n.IIdDonViId))
               .ForMember(n => n.IIdDuAnId, m => m.MapFrom(n => string.IsNullOrEmpty(n.IIdDuAnId) ? null : n.IIdDuAnId))
               .ForMember(n => n.IdParentModified, m => m.MapFrom(n => string.IsNullOrEmpty(n.IdParentModified) ? null : n.IdParentModified))
               .ForMember(n => n.IIdKeHoach5NamId, m => m.MapFrom(n => n.IdChungTu));
        }
    }
}
