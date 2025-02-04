using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class ImpTnQtThuNopMapper : Profile
    {
        public ImpTnQtThuNopMapper()
        {
            CreateMap<RealRevenueExpenditureDetailImportModel, ImpTnQtThuNop>()
                .ForMember(x => x.IdMaLoaiHinh, y => y.MapFrom(z => Guid.Parse(z.IdMaLoaiHinh)))
                .ForMember(x => x.IdMaLoaiHinhCha, y => y.MapFrom(z => Guid.Parse(z.IdMaLoaiHinhCha)))
                .ForMember(x => x.Lns, y => y.MapFrom(z => z.LNS))
                .ForMember(x => x.GhiChu, y => y.MapFrom(z => z.GhiChu))
                .ForMember(x => x.Noidung, y => y.MapFrom(z => z.Description))
                .ForMember(x => x.TongSoThu, y => y.MapFrom(z => string.IsNullOrEmpty(z.TongSoThu) ? 0 : double.Parse(z.TongSoThu)))
                .ForMember(x => x.TongSoChiPhi, y => y.MapFrom(z => string.IsNullOrEmpty(z.TongSoChiPhi) ? 0 : double.Parse(z.TongSoChiPhi)))
                .ForMember(x => x.QtTongSoQtns, y => y.MapFrom(z => string.IsNullOrEmpty(z.TongSoQTNS) ? 0 : double.Parse(z.TongSoQTNS)))
                .ForMember(x => x.QtKhauHaoTscđ, y => y.MapFrom(z => string.IsNullOrEmpty(z.KhauHaoTSTD) ? 0 : double.Parse(z.KhauHaoTSTD)))
                .ForMember(x => x.QtTienLuong, y => y.MapFrom(z => string.IsNullOrEmpty(z.TienLuong) ? 0 : double.Parse(z.TienLuong)))
                .ForMember(x => x.QtQtnskhac, y => y.MapFrom(z => string.IsNullOrEmpty(z.QtnsKhac) ? 0 : double.Parse(z.QtnsKhac)))
                .ForMember(x => x.ChiPhiKhac, y => y.MapFrom(z => string.IsNullOrEmpty(z.ChiPhiKhac) ? 0 : double.Parse(z.ChiPhiKhac)))
                .ForMember(x => x.TongnopNsnn, y => y.MapFrom(z => string.IsNullOrEmpty(z.TongNopNSNN) ? 0 : double.Parse(z.TongNopNSNN)))
                .ForMember(x => x.ThueGtgt, y => y.MapFrom(z => string.IsNullOrEmpty(z.ThueGTGT) ? 0 : double.Parse(z.ThueGTGT)))
                .ForMember(x => x.ThueTndn, y => y.MapFrom(z => string.IsNullOrEmpty(z.ThueTNDN) ? 0 : double.Parse(z.ThueTNDN)))
                .ForMember(x => x.ThueTndnBqp, y => y.MapFrom(z => string.IsNullOrEmpty(z.ThueTndnNopQuaBQP) ? 0 : double.Parse(z.ThueTndnNopQuaBQP)))
                .ForMember(x => x.PhiLePhi, y => y.MapFrom(z => string.IsNullOrEmpty(z.PhiLePhi) ? 0 : double.Parse(z.PhiLePhi)))
                .ForMember(x => x.NsnnKhac, y => y.MapFrom(z => string.IsNullOrEmpty(z.NsnnKhac) ? 0 : double.Parse(z.NsnnKhac)))
                .ForMember(x => x.NsnnKhacBqp, y => y.MapFrom(z => string.IsNullOrEmpty(z.NsnnKhacNopQuaBQP) ? 0 : double.Parse(z.NsnnKhacNopQuaBQP)))
                .ForMember(x => x.ChenhLech, y => y.MapFrom(z => string.IsNullOrEmpty(z.ChenhLech) ? 0 : double.Parse(z.ChenhLech)))
                .ForMember(x => x.PpNopNsqp, y => y.MapFrom(z => string.IsNullOrEmpty(z.NopNSQP) ? 0 : double.Parse(z.NopNSQP)))
                .ForMember(x => x.PpBoSungKinhPhi, y => y.MapFrom(z => string.IsNullOrEmpty(z.BoSungKinhPhi) ? 0 : double.Parse(z.BoSungKinhPhi)))
                .ForMember(x => x.PpTrichCacQuy, y => y.MapFrom(z => string.IsNullOrEmpty(z.PpTrichCacQuy) ? 0 : double.Parse(z.PpTrichCacQuy)))
                .ForMember(x => x.PpSoChuaPhanPhoi, y => y.MapFrom(z => string.IsNullOrEmpty(z.ChuaPhanPhoi) ? 0 : double.Parse(z.ChuaPhanPhoi)));
        }
    }
}
