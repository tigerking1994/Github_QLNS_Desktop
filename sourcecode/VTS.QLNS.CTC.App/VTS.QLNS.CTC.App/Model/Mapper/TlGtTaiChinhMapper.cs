using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TlGtTaiChinhMapper : Profile
    {
        public TlGtTaiChinhMapper()
        {
            CreateMap<TlGtTaiChinhModel, TlGtTaiChinh>();
            CreateMap<TlGtTaiChinh, TlGtTaiChinhModel>();
            CreateMap<CadresModel, TlGtTaiChinhModel>();
            CreateMap<CadresModel, RptGiayGtTaiChinhModel>()
                .ForMember(entity => entity.sMaCanBo, model => model.MapFrom(item => item.MaCanBo))
                .ForMember(entity => entity.sTenCanBo, model => model.MapFrom(item => item.TenCanBo))
                .ForMember(entity => entity.iThang, model => model.MapFrom(item => item.Thang))
                .ForMember(entity => entity.iNam, model => model.MapFrom(item => item.Nam))
                .ForMember(entity => entity.sTenCv, model => model.MapFrom(item => item.ChucVu))
                .ForMember(entity => entity.sTenCapBac, model => model.MapFrom(item => item.CapBac))
                .ForMember(entity => entity.sNgayNn, model => model.MapFrom(item => item.NgayNn))
                .ForMember(entity => entity.sNgayNn, model => model.MapFrom(item => item.NgayXn))
                .ForMember(entity => entity.sNgayNn, model => model.MapFrom(item => item.NgayTn))
                .ForMember(entity => entity.sSoSoLuong, model => model.MapFrom(item => item.SoSoLuong))
                .ForMember(entity => entity.sNoiChuyenDen, model => model.MapFrom(item => item.NoiCongTac))
                .ForMember(entity => entity.sSoTaiKhoan, model => model.MapFrom(item => item.SoTaiKhoan))
                .ForMember(entity => entity.sNganHang, model => model.MapFrom(item => item.TenKhoBac)); ;
            CreateMap<TlGtTaiChinhModel, RptGiayGtTaiChinhModel>()
                .ForMember(entity => entity.sMaCanBo, model => model.MapFrom(item => item.MaCanBo))
                .ForMember(entity => entity.sTenCanBo, model => model.MapFrom(item => item.TenCanBo))
                .ForMember(entity => entity.iThang, model => model.MapFrom(item => item.Thang))
                .ForMember(entity => entity.iNam, model => model.MapFrom(item => item.Nam))
                .ForMember(entity => entity.sTenCv, model => model.MapFrom(item => item.TenCv))
                .ForMember(entity => entity.sTenCapBac, model => model.MapFrom(item => item.TenCapBac))
                .ForMember(entity => entity.sNgayNn, model => model.MapFrom(item => item.NgayNn))
                .ForMember(entity => entity.sNgayNn, model => model.MapFrom(item => item.NgayXn))
                .ForMember(entity => entity.sNgayNn, model => model.MapFrom(item => item.NgayTn))
                .ForMember(entity => entity.sSoSoLuong, model => model.MapFrom(item => item.SoSoLuong))
                .ForMember(entity => entity.sNoiChuyenDen, model => model.MapFrom(item => item.NoiChuyenDen))
                .ForMember(entity => entity.sSoQd, model => model.MapFrom(item => item.SoQd))
                .ForMember(entity => entity.sNgayKyQd, model => model.MapFrom(item => item.NgayKyQd))
                .ForMember(entity => entity.iCapPhatTiepThang, model => model.MapFrom(item => item.CapPhatTiepThang))
                .ForMember(entity => entity.iCapPhatTiepNam, model => model.MapFrom(item => item.CapPhatTiepNam))
                .ForMember(entity => entity.fLoPhiDuocCap, model => model.MapFrom(item => item.LoPhiDuocCap))
                .ForMember(entity => entity.fLoPhiThanhToan, model => model.MapFrom(item => item.LoPhiThanhToan))
                .ForMember(entity => entity.sNgayKy, model => model.MapFrom(item => item.NgayKy))
                .ForMember(entity => entity.sSoTaiKhoan, model => model.MapFrom(item => item.SoTaiKhoan))
                .ForMember(entity => entity.sNganHang, model => model.MapFrom(item => item.NganHang));
            CreateMap<CadresNq104Model, RptGiayGtTaiChinhNq104Model>()
               .ForMember(entity => entity.MaCanBo, model => model.MapFrom(item => item.MaCanBo))
               .ForMember(entity => entity.TenCanBo, model => model.MapFrom(item => item.TenCanBo))
               .ForMember(entity => entity.Thang, model => model.MapFrom(item => item.Thang))
               .ForMember(entity => entity.Nam, model => model.MapFrom(item => item.Nam))
               .ForMember(entity => entity.TenCv, model => model.MapFrom(item => item.ChucVu))
               .ForMember(entity => entity.TenCapBac, model => model.MapFrom(item => item.CapBac))
               .ForMember(entity => entity.NgayNn, model => model.MapFrom(item => item.NgayNn))
               .ForMember(entity => entity.NgayNn, model => model.MapFrom(item => item.NgayXn))
               .ForMember(entity => entity.NgayNn, model => model.MapFrom(item => item.NgayTn))
               .ForMember(entity => entity.SoSoLuong, model => model.MapFrom(item => item.SoSoLuong))
               .ForMember(entity => entity.NoiChuyenDen, model => model.MapFrom(item => item.NoiCongTac))
               .ForMember(entity => entity.SoTaiKhoan, model => model.MapFrom(item => item.SoTaiKhoan))
               .ForMember(entity => entity.NganHang, model => model.MapFrom(item => item.TenKhoBac)); ;

        }
    }
}
