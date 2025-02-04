using AutoMapper;
using System;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TlTransferChungTuChiTietNSMapper : Profile
    {
        public TlTransferChungTuChiTietNSMapper()
        {
            CreateMap<TlQsChungTuChiTietModel, NsQsChungTuChiTiet>()
                .ForMember(x => x.IIdQschungTu, y => y.MapFrom(z => new Guid(z.IdChungTu)))
                .ForMember(x => x.IIdMlns, y => y.MapFrom(z => new Guid(z.MlnsId)))
                .ForMember(x => x.IIdMlnsCha,
                    y => y.MapFrom(z => string.IsNullOrEmpty(z.MlnsIdParent) ? Guid.Empty : new Guid(z.MlnsIdParent)))
                .ForMember(x => x.SKyHieu, y => y.MapFrom(z => z.XauNoiMa))
                .ForMember(x => x.SMoTa, y => y.MapFrom(z => z.MoTa))
                .ForMember(x => x.IIdMaDonVi, y => y.MapFrom(z => z.IdDonVi))
                .ForMember(x => x.INamLamViec, y => y.MapFrom(z => z.NamLamViec))
                .ForMember(x => x.SNguoiTao, y => y.MapFrom(z => z.UserCreator))
                .ForMember(x => x.FSoThieuUy, y => y.MapFrom(z => z.ThieuUy))
                .ForMember(x => x.FSoTrungUy, y => y.MapFrom(z => z.TrungUy))
                .ForMember(x => x.FSoThuongUy, y => y.MapFrom(z => z.ThuongUy))
                .ForMember(x => x.FSoDaiUy, y => y.MapFrom(z => z.DaiUy))
                .ForMember(x => x.FSoThieuTa, y => y.MapFrom(z => z.ThieuTa))
                .ForMember(x => x.FSoTrungTa, y => y.MapFrom(z => z.TrungTa))
                .ForMember(x => x.FSoThuongTa, y => y.MapFrom(z => z.ThuongTa))
                .ForMember(x => x.FSoDaiTa, y => y.MapFrom(z => z.DaiTa))
                .ForMember(x => x.FSoTuong, y => y.MapFrom(z => z.Tuong))
                .ForMember(x => x.FSoBinhNhi, y => y.MapFrom(z => z.BinhNhi))
                .ForMember(x => x.FSoBinhNhat, y => y.MapFrom(z => z.BinhNhat))
                .ForMember(x => x.FSoHaSi, y => y.MapFrom(z => z.HaSi))
                .ForMember(x => x.FSoTrungSi, y => y.MapFrom(z => z.TrungSi))
                .ForMember(x => x.FSoThuongSi, y => y.MapFrom(z => z.ThuongSi))
                .ForMember(x => x.FSoTsq, y => y.MapFrom(z => z.TongSiQuan))
                .ForMember(x => x.FSoThuongTaQNCN, y => y.MapFrom(z => z.Qncn))
                .ForMember(x => x.FSoCnvqp, y => y.MapFrom(z => z.Cnqp))
                .ForMember(x => x.FSoVcqp, y => y.MapFrom(z => z.Vcqp))
                .ForMember(x => x.FSoLdhd, y => y.MapFrom(z => z.Ldhd + z.Cnqp))
                .ForMember(x => x.IThangQuy, y => y.MapFrom(z => z.Thang))
                .ForMember(x => x.SNgaySua, y => y.MapFrom(z => z.DateCreated));
        }
    }
}