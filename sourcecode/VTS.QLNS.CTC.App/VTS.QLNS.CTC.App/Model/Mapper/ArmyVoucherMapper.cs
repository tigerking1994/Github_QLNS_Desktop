using AutoMapper;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class ArmyVoucherMapper : Profile
    {
        public ArmyVoucherMapper()
        {
            CreateMap<NsQsChungTu, ArmyVoucherModel>();
            CreateMap<ArmyVoucherModel, NsQsChungTu>();
            CreateMap<QsChungTuChiTietQuery, ArmyVoucherDetailModel>()
                .ForMember(x => x.BHangCha, y => y.MapFrom(z => z.BHangCha));
            CreateMap<ArmyVoucherDetailModel, NsQsChungTuChiTiet>()
                .ForMember(x => x.BHangCha, y => y.MapFrom(z => z.BHangCha));
            CreateMap<ArmyDetailImportModel, NsQsChungTuChiTiet>()
                .ForMember(x => x.SKyHieu, y => y.MapFrom(z => z.XauNoiMa))
                .ForMember(x => x.SMoTa, y => y.MapFrom(z => z.MoTa))
                .ForMember(x => x.FSoThieuUy,
                    y => y.MapFrom(z => string.IsNullOrEmpty(z.RThieuUy) ? 0 : double.Parse(z.RThieuUy)))
                .ForMember(x => x.FSoTrungUy,
                    y => y.MapFrom(z => string.IsNullOrEmpty(z.RTrungUy) ? 0 : double.Parse(z.RTrungUy)))
                .ForMember(x => x.FSoThuongUy,
                    y => y.MapFrom(z => string.IsNullOrEmpty(z.RThuongUy) ? 0 : double.Parse(z.RThuongUy)))
                .ForMember(x => x.FSoDaiUy,
                    y => y.MapFrom(z => string.IsNullOrEmpty(z.RDaiUy) ? 0 : double.Parse(z.RDaiUy)))
                .ForMember(x => x.FSoThieuTa,
                    y => y.MapFrom(z => string.IsNullOrEmpty(z.RThieuTa) ? 0 : double.Parse(z.RThieuTa)))
                .ForMember(x => x.FSoTrungTa,
                    y => y.MapFrom(z => string.IsNullOrEmpty(z.RTrungTa) ? 0 : double.Parse(z.RTrungTa)))
                .ForMember(x => x.FSoThuongTa,
                    y => y.MapFrom(z => string.IsNullOrEmpty(z.RThuongTa) ? 0 : double.Parse(z.RThuongTa)))
                .ForMember(x => x.FSoDaiTa,
                    y => y.MapFrom(z => string.IsNullOrEmpty(z.RDaiTa) ? 0 : double.Parse(z.RDaiTa)))
                .ForMember(x => x.FSoTuong,
                    y => y.MapFrom(z => string.IsNullOrEmpty(z.RTuong) ? 0 : double.Parse(z.RTuong)))
                .ForMember(x => x.FSoBinhNhi,
                    y => y.MapFrom(z => string.IsNullOrEmpty(z.RBinhNhi) ? 0 : double.Parse(z.RBinhNhi)))
                .ForMember(x => x.FSoBinhNhat,
                    y => y.MapFrom(z => string.IsNullOrEmpty(z.RBinhNhat) ? 0 : double.Parse(z.RBinhNhat)))
                .ForMember(x => x.FSoHaSi,
                    y => y.MapFrom(z => string.IsNullOrEmpty(z.RHaSi) ? 0 : double.Parse(z.RHaSi)))
                .ForMember(x => x.FSoTrungSi,
                    y => y.MapFrom(z => string.IsNullOrEmpty(z.RTrungSi) ? 0 : double.Parse(z.RTrungSi)))
                .ForMember(x => x.FSoThuongSi,
                    y => y.MapFrom(z => string.IsNullOrEmpty(z.RThuongSi) ? 0 : double.Parse(z.RThuongSi)))
                .ForMember(x => x.FSoDaiUyQNCN,
                    y => y.MapFrom(z => string.IsNullOrEmpty(z.RQncn) ? 0 : double.Parse(z.RQncn)))
                .ForMember(x => x.FSoVcqp,
                    y => y.MapFrom(z => string.IsNullOrEmpty(z.RVcqp) ? 0 : double.Parse(z.RVcqp)))
                .ForMember(x => x.FSoCnvqp,
                    y => y.MapFrom(z => string.IsNullOrEmpty(z.RCnvqp) ? 0 : double.Parse(z.RCnvqp)))
                .ForMember(x => x.FSoLdhd,
                    y => y.MapFrom(z => string.IsNullOrEmpty(z.RLdhd) ? 0 : double.Parse(z.RLdhd)))
                .ForMember(x => x.FSoThuongTaQNCN,
                    y => y.MapFrom(z => string.IsNullOrEmpty(z.SoThuongTaQNCN) ? 0 : double.Parse(z.SoThuongTaQNCN)))
                .ForMember(x => x.FSoTrungTaQNCN,
                    y => y.MapFrom(z => string.IsNullOrEmpty(z.SoTrungTaQNCN) ? 0 : double.Parse(z.SoTrungTaQNCN)))
                .ForMember(x => x.FSoThieuTaQNCN,
                    y => y.MapFrom(z => string.IsNullOrEmpty(z.SoThieuTaQNCN) ? 0 : double.Parse(z.SoThieuTaQNCN)))
                .ForMember(x => x.FSoDaiUyQNCN,
                    y => y.MapFrom(z => string.IsNullOrEmpty(z.SoDaiUyQNCN) ? 0 : double.Parse(z.SoDaiUyQNCN)))
                .ForMember(x => x.FSoThuongUyQNCN,
                    y => y.MapFrom(z => string.IsNullOrEmpty(z.SoThuongUyQNCN) ? 0 : double.Parse(z.SoThuongUyQNCN)))
                .ForMember(x => x.FSoTrungUyQNCN,
                    y => y.MapFrom(z => string.IsNullOrEmpty(z.SoTrungUyQNCN) ? 0 : double.Parse(z.SoTrungUyQNCN)))
                .ForMember(x => x.FSoThieuUyQNCN,
                    y => y.MapFrom(z => string.IsNullOrEmpty(z.SoThieuUyQNCN) ? 0 : double.Parse(z.SoThieuUyQNCN)))
                .ForMember(x => x.FSoCcqp,
                    y => y.MapFrom(z => string.IsNullOrEmpty(z.RCcqp) ? 0 : double.Parse(z.RCcqp)));

        }
    }
}
