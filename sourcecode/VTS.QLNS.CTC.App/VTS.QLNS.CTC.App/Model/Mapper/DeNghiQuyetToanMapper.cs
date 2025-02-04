using AutoMapper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class DeNghiQuyetToanMapper : Profile
    {
        public DeNghiQuyetToanMapper()
        {
            CreateMap<DeNghiQuyetToanQuery, DeNghiQuyetToanModel>();

            CreateMap<VdtQtDeNghiQuyetToan, DeNghiQuyetToanModel>()
                .ForMember(entity => entity.SoBaoCao, model => model.MapFrom(item => item.SSoBaoCao))
                .ForMember(entity => entity.ThoiGianBaoCao, model => model.MapFrom(item => item.DThoiGianLapBaoCao));

            CreateMap<DeNghiQuyetToanModel, VdtQtDeNghiQuyetToan>()
               .ForMember(entity => entity.SSoBaoCao, model => model.MapFrom(item => item.SoBaoCao))
               .ForMember(entity => entity.DThoiGianLapBaoCao, model => model.MapFrom(item => item.ThoiGianBaoCao));

            CreateMap<RequestSettlementDialogModel, VdtQtDeNghiQuyetToan>()
                .ForMember(entity => entity.SSoBaoCao, model => model.MapFrom(item => item.SoBaoCao))
                .ForMember(entity => entity.DThoiGianLapBaoCao, model => model.MapFrom(item => item.NgayDuyet))
                .ForMember(entity => entity.SNguoiLap, model => model.MapFrom(item => item.NguoiDuyet))
                .ForMember(entity => entity.DThoiGianNhanBaoCao, model => model.MapFrom(item => item.NgayNhan))
                .ForMember(entity => entity.SNguoiNhan, model => model.MapFrom(item => item.NguoiNhan))
                .ForMember(entity => entity.DThoiGianKhoiCong, model => model.MapFrom(item => item.ThoiGianKhoiCong))
                .ForMember(entity => entity.DThoiGianHoanThanh, model => model.MapFrom(item => item.ThoiGianHoanThanh))
                .ForMember(entity => entity.FGiaTriDeNghiQuyetToan, model => model.MapFrom(item => item.GiaTriQuyetToan))
                .ForMember(entity => entity.FChiPhiThietHai, model => model.MapFrom(item => item.ChiPhiThietHai))
                .ForMember(entity => entity.FChiPhiKhongTaoNenTaiSan, model => model.MapFrom(item => item.ChiPhiKhongTaoTaiSan))
                .ForMember(entity => entity.FTaiSanDaiHanThuocCDTQuanLy, model => model.MapFrom(item => item.DaiHanThuocQuanLy))
                .ForMember(entity => entity.FTaiSanDaiHanDonViKhacQuanLy, model => model.MapFrom(item => item.DaiHanDonViKhacQuanLy))
                .ForMember(entity => entity.FTaiSanNganHanThuocCDTQuanLy, model => model.MapFrom(item => item.NganHanThuocQuanLy))
                .ForMember(entity => entity.FTaiSanNganHanDonViKhacQuanLy, model => model.MapFrom(item => item.NganHanDonViKhacQuanLy))
                .ForMember(entity => entity.SMoTa, model => model.MapFrom(item => item.GhiChu));

            CreateMap<VdtQtBcQuyetToanNienDoQuery, VdtQtBcquyetToanNienDoModel>();
            CreateMap<VdtQtBcquyetToanNienDoModel, VdtQtBcQuyetToanNienDoQuery>();

            CreateMap<VdtQtBcQuyetToanNienDo, VdtQtBcquyetToanNienDoModel>()
                .ForMember(n => n.DNgayDeNghi, m => m.MapFrom(n => n.DNgayDeNghi))
                .ForMember(n => n.IIDDonViQuanLyID, m => m.MapFrom(n => n.IIdDonViQuanLyId))
                .ForMember(n => n.IIDMaDonViQuanLy, m => m.MapFrom(n => n.IIdMaDonViQuanLy))
                .ForMember(n => n.IIDNguonVonID, m => m.MapFrom(n => n.IIdNguonVonId))
                .ForMember(n => n.ICoQuanThanhToan, m => m.MapFrom(n => n.ICoQuanThanhToan))
                .ForMember(n => n.ILoaiThanhToan, m => m.MapFrom(n => n.ILoaiThanhToan))
                .ForMember(n => n.INamKeHoach, m => m.MapFrom(n => n.INamKeHoach))
                .ForMember(n => n.SSoDeNghi, m => m.MapFrom(n => n.SSoDeNghi));
            CreateMap<VdtQtBcquyetToanNienDoModel, VdtQtBcQuyetToanNienDo>()
                .ForMember(n => n.DNgayDeNghi, m => m.MapFrom(n => n.DNgayDeNghi))
                .ForMember(n => n.IIdDonViQuanLyId, m => m.MapFrom(n => n.IIDDonViQuanLyID))
                .ForMember(n => n.IIdMaDonViQuanLy, m => m.MapFrom(n => n.IIDMaDonViQuanLy))
                .ForMember(n => n.IIdNguonVonId, m => m.MapFrom(n => n.IIDNguonVonID))
                .ForMember(n => n.ICoQuanThanhToan, m => m.MapFrom(n => n.ICoQuanThanhToan))
                .ForMember(n => n.ILoaiThanhToan, m => m.MapFrom(n => n.ILoaiThanhToan))
                .ForMember(n => n.INamKeHoach, m => m.MapFrom(n => n.INamKeHoach))
                .ForMember(n => n.SSoDeNghi, m => m.MapFrom(n => n.SSoDeNghi));

            CreateMap<VdtQtBcquyetToanNienDoChiTiet1Model, VdtQtBcquyetToanNienDoChiTiet1Query>();
            CreateMap<VdtQtBcquyetToanNienDoChiTiet1Query, VdtQtBcquyetToanNienDoChiTiet1Model>();

            CreateMap<BcquyetToanNienDoVonUngChiTietModel, BcquyetToanNienDoVonUngChiTietQuery>();
            CreateMap<BcquyetToanNienDoVonUngChiTietQuery, BcquyetToanNienDoVonUngChiTietModel>()
                .ForMember(n => n.FKeHoachVonDuocKeoDai, m => m.MapFrom(n => n.FUngTruocChuaThuHoiNamTruoc - n.FLuyKeThanhToanNamTruoc))
                .ForMember(n => n.FVonKeoDaiDaThanhToanNamNay, m => m.MapFrom(n => (n.FThanhToanKLHTNamTruocChuyenSang + n.FThanhToanUngNamTruocChuyenSang)
                    - (n.FThuHoiTamUngNamNayVonNamTruoc + n.FThuHoiTamUngNamTruocVonNamTruoc)))
                .ForMember(n => n.FVonDaThanhToanNamNay, m => m.MapFrom(n => (n.FThanhToanKLHTTamUngNamNay + n.FThanhToanUngNamNay)
                    - (n.FThuHoiTamUngNamNay + n.FThuHoiTamUngNamTruoc)))
                .ForMember(n => n.FKHVUChuaThuHoiChuyenNamSau, m => m.MapFrom(n => (n.FUngTruocChuaThuHoiNamTruoc - n.FThuHoiVonNamNay + n.FKHVUNamNay)))
                .ForMember(n => n.FTongSoVonDaThanhToanThuHoi, m => m.MapFrom(n => n.FLuyKeThanhToanNamTruoc
                + ((n.FThanhToanKLHTNamTruocChuyenSang + n.FThanhToanUngNamTruocChuyenSang) - (n.FThuHoiTamUngNamNayVonNamTruoc + n.FThuHoiTamUngNamTruocVonNamTruoc))
                + ((n.FThanhToanKLHTTamUngNamNay + n.FThanhToanUngNamNay) - (n.FThuHoiTamUngNamNay + n.FThuHoiTamUngNamTruoc))));

            CreateMap<VdtDaDuToanChiPhiDataQuery, DeNghiQuyetToanChiTietModel>();

            CreateMap<DuToanDetailQuery, DeNghiQuyetToanChiTietModel>()
                 .ForMember(n => n.HangMucId, m => m.MapFrom(n => n.IdDuAnHangMuc))
                 .ForMember(n => n.TenChiPhi, m => m.MapFrom(n => n.TenHangMuc))
                 .ForMember(n => n.IsHangCha, m => m.MapFrom(n => n.IsHangCha))
                 .ForMember(n => n.GiaTriPheDuyet, m => m.MapFrom(n => n.GiaTriPheDuyet))
                 .ForMember(n => n.IdHangMucParent, m => m.MapFrom(n => n.HangMucParentId))
                 .ForMember(n => n.MaOrderDb, m => m.MapFrom(n => n.MaOrDer));

            CreateMap<BcquyetToanNienDoVonUngChiTietModel, ExportBcquyetToanNienDoVonUngChiTietModel>();

            CreateMap<VdtQtBcquyetToanNienDoChiTiet1Model, ExportVdtQtBcquyetToanNienDoChiTiet1Model>();
        }
    }
}
