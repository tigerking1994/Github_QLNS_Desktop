using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class VdtHopDongMapper : Profile
    {
        public VdtHopDongMapper()
        {
            CreateMap<HopDongDialogModel, VdtDaTtHopDong>()
                    .ForMember(entity => entity.SSoHopDong, model => model.MapFrom(item => item.SoHopDong))
                    .ForMember(entity => entity.IThoiGianThucHien, model => model.MapFrom(item => item.TGThucHienNgay.HasValue ? item.TGThucHienNgay : 0))
                    .ForMember(entity => entity.DNgayHopDong, model => model.MapFrom(item => item.NgayLap.Value))
                    .ForMember(entity => entity.DKhoiCongDuKien, model => model.MapFrom(item => item.NgayKhoiCongDuKien))
                    .ForMember(entity => entity.DKetThucDuKien, model => model.MapFrom(item => item.NgayKetThucDuKien))
                    .ForMember(entity => entity.SSoTaiKhoan, model => model.MapFrom(item => item.SoTaiKhoan))
                    .ForMember(entity => entity.SNganHang, model => model.MapFrom(item => item.TenNganHang))
                    .ForMember(entity => entity.SHinhThucHopDong, model => model.MapFrom(item => item.HTHopDong))
                    .ForMember(entity => entity.FTienHopDong, model => model.MapFrom(item => item.GiaTriHopDong));
            CreateMap<VdtDaTtHopDong, HopDongDialogModel>()
                    .ForMember(entity => entity.SoHopDong, model => model.MapFrom(item => item.SSoHopDong))
                    .ForMember(entity => entity.TGThucHienNgay, model => model.MapFrom(item => item.IThoiGianThucHien))
                    .ForMember(entity => entity.NgayLap, model => model.MapFrom(item => item.DNgayHopDong))
                    .ForMember(entity => entity.NgayKhoiCongDuKien, model => model.MapFrom(item => item.DKhoiCongDuKien))
                    .ForMember(entity => entity.NgayKetThucDuKien, model => model.MapFrom(item => item.DKetThucDuKien))
                    .ForMember(entity => entity.SoTaiKhoan, model => model.MapFrom(item => item.SSoTaiKhoan))
                    .ForMember(entity => entity.TenNganHang, model => model.MapFrom(item => item.SNganHang))
                    .ForMember(entity => entity.HTHopDong, model => model.MapFrom(item => item.SHinhThucHopDong))
                    .ForMember(entity => entity.GiaTriHopDong, model => model.MapFrom(item => item.FTienHopDong));
        }
    }
}
