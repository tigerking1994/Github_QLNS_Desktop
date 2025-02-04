using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class VdtKhvPhanBoVonDonViMapper : Profile
    {
        public VdtKhvPhanBoVonDonViMapper()
        {
            CreateMap<PhanBoVonDonViQuery, VdtKhvPhanBoVonDonViChiTiet>()
                    .ForMember(entity => entity.IIdDuAnId, model => model.MapFrom(x => x.IIdDuAn))
                    .ForMember(entity => entity.IIdLoaiCongTrinhId, model => model.MapFrom(x => x.IdLoaiCongTrinh))
                    .ForMember(entity => entity.IIdParentId, model => model.MapFrom(x => x.IdParent))
                    .ForMember(entity => entity.IID_DuAn_HangMucID, model => model.MapFrom(x => x.iID_DuAn_HangMucID));

            CreateMap<ExportVonNamDonViQuery, PhanBoVonDonViChiTietModel>()
                .ForMember(n => n.sMaDonViQuanLy, m => m.MapFrom(n => n.iID_MaDonViQuanLy));
            CreateMap<PhanBoVonDonViChiTietModel, ExportVonNamDonViQuery>()
                .ForMember(n => n.iID_MaDonViQuanLy, m => m.MapFrom(n => n.sMaDonViQuanLy));

            CreateMap<VdtKhvPhanBoVonDonViChiTietQuery, DuAnKeHoachVonNamDeXuatModel>()
                .ForMember(n => n.IIdDuAnId, m => m.MapFrom(m => m.iID_DuAnID))
                .ForMember(n => n.STenDuAn, m => m.MapFrom(m => m.sTenDuAn))
                .ForMember(n => n.SMaDuAn, m => m.MapFrom(m => m.sMaDuAn))
                .ForMember(n => n.ILoaiDuAn, m => m.MapFrom(m => m.ILoaiDuAn));

            CreateMap<VdtKhvPhanBoVonDonViChiTietDieuChinhQuery, VdtKhvPhanBoVonDonViChiTietQuery>();
            CreateMap<VdtKhvPhanBoVonDonViChiTietQuery, PhanBoVonDonViChiTietModel>();
            CreateMap<PhanBoVonDonViChiTietModel, VdtKhvPhanBoVonDonViChiTietQuery>();
            CreateMap<VdtKhvPhanBoVonDonViQuery, PhanBoVonDonViModel>();
            CreateMap<PhanBoVonDonViModel, VdtKhvPhanBoVonDonViQuery>();
            CreateMap<VdtKhvPhanBoVonDonViChiTietQuery, VdtKhvPhanBoVonDonViChiTiet>()
                .ForMember(n => n.IIdDuAnId, m => m.MapFrom(n => n.iID_DuAnID))
                .ForMember(n => n.IIdLoaiCongTrinhId, m => m.MapFrom(n => n.iID_LoaiCongTrinhID))
                .ForMember(n => n.FTongMucDauTuDuocDuyet, m => m.MapFrom(n => n.fTongMucDauTuDuocDuyet))
                .ForMember(n => n.FLuyKeVonNamTruoc, m => m.MapFrom(n => n.fLuyKeVonNamTruoc))
                .ForMember(n => n.FKeHoachVonDuocDuyetNamNay, m => m.MapFrom(n => n.fKeHoachVonDuocDuyetNamNay))
                .ForMember(n => n.FVonKeoDaiCacNamTruoc, m => m.MapFrom(n => n.fVonKeoDaiCacNamTruoc))
                .ForMember(n => n.FUocThucHien, m => m.MapFrom(n => n.fUocThucHien))
                .ForMember(n => n.FThuHoiVonUngTruoc, m => m.MapFrom(n => n.fThuHoiVonUngTruoc))
                .ForMember(n => n.FThanhToan, m => m.MapFrom(n => n.fThanhToan))
                .ForMember(n => n.ILoaiDuAn, m => m.MapFrom(n => n.ILoaiDuAn));

            CreateMap<PhanBoVonDonViChiTietModel, PhanBoVonDonViImportModel>();
            CreateMap<PhanBoVonDonViImportModel, PhanBoVonDonViChiTietModel>()
                .ForMember(n => n.fTongMucDauTuDuocDuyet, m => m.MapFrom(n => string.IsNullOrEmpty(n.fTongMucDauTuDuocDuyet) ? 0 : double.Parse(n.fTongMucDauTuDuocDuyet)))
                .ForMember(n => n.fVonKeoDaiCacNamTruoc, m => m.MapFrom(n => string.IsNullOrEmpty(n.fVonKeoDaiCacNamTruoc) ? 0 : double.Parse(n.fVonKeoDaiCacNamTruoc)))
                 .ForMember(n => n.fThanhToan, m => m.MapFrom(n => string.IsNullOrEmpty(n.fThanhToan) ? 0 : double.Parse(n.fThanhToan)))
                 .ForMember(n => n.fUocThucHien, m => m.MapFrom(n => string.IsNullOrEmpty(n.fUocThucHien) ? 0 : double.Parse(n.fUocThucHien)))
                 .ForMember(n => n.fThuHoiVonUngTruoc, m => m.MapFrom(n => string.IsNullOrEmpty(n.fThuHoiVonUngTruoc) ? 0 : double.Parse(n.fThuHoiVonUngTruoc)))
                 .ForMember(n => n.IdChungTuParent, m => m.MapFrom(n => !string.IsNullOrEmpty(n.IdChungTuParent) ? Guid.Parse(n.IdChungTuParent) : Guid.Empty))
                 .ForMember(n => n.iID_LoaiCongTrinhID, m => m.MapFrom(n => !string.IsNullOrEmpty(n.IIdLoaiCongTrinh) ? Guid.Parse(n.IIdLoaiCongTrinh) : Guid.Empty));

            CreateMap<VdtKhvPhanBoVonDonViChiTiet, PhanBoVonDonViChiTietModel>()
                .ForMember(n => n.fKeHoachVonDuocDuyetNamNay, m => m.MapFrom(n => n.FKeHoachVonDuocDuyetNamNay))
                .ForMember(n => n.fLuyKeVonNamTruoc, m => m.MapFrom(n => n.FLuyKeVonNamTruoc))
                .ForMember(n => n.fThanhToan, m => m.MapFrom(n => n.FThanhToan))
                .ForMember(n => n.fThuHoiVonUngTruoc, m => m.MapFrom(n => n.FThuHoiVonUngTruoc))
                .ForMember(n => n.fTiGia, m => m.MapFrom(n => n.FTiGia))
                .ForMember(n => n.fTiGiaDonVi, m => m.MapFrom(n => n.FTiGiaDonVi))
                .ForMember(n => n.fTongMucDauTuDuocDuyet, m => m.MapFrom(n => n.FTongMucDauTuDuocDuyet))
                .ForMember(n => n.fUocThucHien, m => m.MapFrom(n => n.FUocThucHien))
                .ForMember(n => n.fVonKeoDaiCacNamTruoc, m => m.MapFrom(n => n.FVonKeoDaiCacNamTruoc))
                .ForMember(n => n.iID_DonViTienTeID, m => m.MapFrom(n => n.IIdDonViTienTeId))
                .ForMember(n => n.iID_DuAnID, m => m.MapFrom(n => n.IIdDuAnId))
                .ForMember(n => n.iID_TienTeID, m => m.MapFrom(n => n.IIdTienTeId))
                .ForMember(n => n.sMaDuAn, m => m.MapFrom(n => n.SMaDuAn))
                .ForMember(n => n.FUocThucHienSauDc, m => m.MapFrom(n => n.FUocThucHienDC))
                .ForMember(n => n.FThuHoiVonUngTruocSauDc, m => m.MapFrom(n => n.FThuHoiVonUngTruocDC))
                .ForMember(n => n.FThanhToanSauDc, m => m.MapFrom(n => n.FThanhToanDC))
                .ForMember(n => n.sTrangThaiDuAnDangKy, m => m.MapFrom(n => n.STrangThaiDuAnDangKy))
                .ForMember(n => n.iID_LoaiCongTrinhID, m => m.MapFrom(n => n.IIdLoaiCongTrinhId));

            CreateMap<PhanBoVonDonViChiTietModel, VdtKhvPhanBoVonDonViChiTiet>()
                .ForMember(n => n.FKeHoachVonDuocDuyetNamNay, m => m.MapFrom(n => n.fKeHoachVonDuocDuyetNamNay))
                .ForMember(n => n.FLuyKeVonNamTruoc, m => m.MapFrom(n => n.fLuyKeVonNamTruoc))
                .ForMember(n => n.FThanhToan, m => m.MapFrom(n => n.fThanhToan))
                .ForMember(n => n.FThuHoiVonUngTruoc, m => m.MapFrom(n => n.fThuHoiVonUngTruoc))
                .ForMember(n => n.FTiGia, m => m.MapFrom(n => n.fTiGia))
                .ForMember(n => n.FTiGiaDonVi, m => m.MapFrom(n => n.fTiGiaDonVi))
                .ForMember(n => n.FTongMucDauTuDuocDuyet, m => m.MapFrom(n => n.fTongMucDauTuDuocDuyet))
                .ForMember(n => n.FUocThucHien, m => m.MapFrom(n => n.fUocThucHien))
                .ForMember(n => n.FVonKeoDaiCacNamTruoc, m => m.MapFrom(n => n.fVonKeoDaiCacNamTruoc))
                .ForMember(n => n.IIdDonViTienTeId, m => m.MapFrom(n => n.iID_DonViTienTeID))
                .ForMember(n => n.IIdDuAnId, m => m.MapFrom(n => n.iID_DuAnID))
                .ForMember(n => n.IIdTienTeId, m => m.MapFrom(n => n.iID_TienTeID))
                .ForMember(n => n.SMaDuAn, m => m.MapFrom(n => n.sMaDuAn))
                .ForMember(n => n.FUocThucHienDC, m => m.MapFrom(n => n.FUocThucHienSauDc))
                .ForMember(n => n.FThuHoiVonUngTruocDC, m => m.MapFrom(n => n.FThuHoiVonUngTruocSauDc))
                .ForMember(n => n.FThanhToanDC, m => m.MapFrom(n => n.FThanhToanSauDc))
                .ForMember(n => n.STrangThaiDuAnDangKy, m => m.MapFrom(n => n.sTrangThaiDuAnDangKy))
                .ForMember(n => n.IIdPhanBoVonDonVi, m => m.MapFrom(n => n.IdChungTu))
                .ForMember(n => n.IIdLoaiCongTrinhId, m => m.MapFrom(n => n.iID_LoaiCongTrinhID));

            CreateMap<VdtKhvPhanBoVonDonVi, PhanBoVonDonViModel>()
                .ForMember(n => n.bIsCanBoDuyet, m => m.MapFrom(n => n.BIsCanBoDuyet))
                .ForMember(n => n.bIsDuyet, m => m.MapFrom(n => n.BIsDuyet))
                .ForMember(n => n.dNgayQuyetDinh, m => m.MapFrom(n => n.DNgayQuyetDinh))
                .ForMember(n => n.fThanhToan, m => m.MapFrom(n => n.FThanhToan))
                .ForMember(n => n.fThuHoiVonUngTruoc, m => m.MapFrom(n => n.FThuHoiVonUngTruoc))
                .ForMember(n => n.iID_DonViQuanLyID, m => m.MapFrom(n => n.IIdDonViQuanLyId))
                .ForMember(n => n.iID_MaDonViQuanLy, m => m.MapFrom(n => n.IIdMaDonViQuanLy))
                .ForMember(n => n.iId_NguonVonId, m => m.MapFrom(n => n.IIdNguonVonId))
                .ForMember(n => n.iID_ParentId, m => m.MapFrom(n => n.IIdParentId))
                .ForMember(n => n.iNamKeHoach, m => m.MapFrom(n => n.INamKeHoach))
                .ForMember(n => n.sNguoiLap, m => m.MapFrom(n => n.SNguoiLap))
                .ForMember(n => n.sSoQuyetDinh, m => m.MapFrom(n => n.SSoQuyetDinh))
                .ForMember(n => n.sTruongPhong, m => m.MapFrom(n => n.STruongPhong));

            CreateMap<PhanBoVonDonViModel, VdtKhvPhanBoVonDonVi>()
                .ForMember(n => n.BIsCanBoDuyet, m => m.MapFrom(n => n.bIsCanBoDuyet))
                .ForMember(n => n.BIsDuyet, m => m.MapFrom(n => n.bIsDuyet))
                .ForMember(n => n.DNgayQuyetDinh, m => m.MapFrom(n => n.dNgayQuyetDinh))
                .ForMember(n => n.FThanhToan, m => m.MapFrom(n => n.fThanhToan))
                .ForMember(n => n.FThuHoiVonUngTruoc, m => m.MapFrom(n => n.fThuHoiVonUngTruoc))
                .ForMember(n => n.IIdDonViQuanLyId, m => m.MapFrom(n => n.iID_DonViQuanLyID))
                .ForMember(n => n.IIdMaDonViQuanLy, m => m.MapFrom(n => n.iID_MaDonViQuanLy))
                .ForMember(n => n.IIdNguonVonId, m => m.MapFrom(n => n.iId_NguonVonId))
                .ForMember(n => n.IIdParentId, m => m.MapFrom(n => n.iID_ParentId))
                .ForMember(n => n.INamKeHoach, m => m.MapFrom(n => n.iNamKeHoach))
                .ForMember(n => n.SNguoiLap, m => m.MapFrom(n => n.sNguoiLap))
                .ForMember(n => n.SSoQuyetDinh, m => m.MapFrom(n => n.sSoQuyetDinh))
                .ForMember(n => n.STruongPhong, m => m.MapFrom(n => n.sTruongPhong));

            CreateMap<VdtKhvPhanBoVonDonVi, ComboboxItem>()
                .ForMember(n => n.DisplayItem, m => m.MapFrom(n => string.Format("{0}-{1}", n.SSoQuyetDinh, n.INamKeHoach)))
                .ForMember(n => n.ValueItem, m => m.MapFrom(n => n.Id));
        }
    }
}
