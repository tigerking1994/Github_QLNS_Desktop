using AutoMapper;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class SktChungTuChiTietMapper : Profile
    {
        public SktChungTuChiTietMapper()
        {
            CreateMap<NsSktChungTuChiTiet, NsSktChungTuChiTietModel>().ReverseMap();
            CreateMap<DemandVoucherDetailImportModel, ImpSoKiemTra>()
                .ForMember(x => x.KyHieu, y => y.MapFrom(z => z.KyHieu))
                .ForMember(x => x.Stt, y => y.MapFrom(z => z.STT))
                .ForMember(x => x.MoTa, y => y.MapFrom(z => z.Description))
                .ForMember(x => x.TuChi, y => y.MapFrom(z => string.IsNullOrEmpty(z.TuChi) ? 0 : double.Parse(z.TuChi)))
                .ForMember(x => x.HuyDong,
                    y => y.MapFrom(z => string.IsNullOrEmpty(z.HuyDong) ? 0 : double.Parse(z.HuyDong)))
                .ForMember(x => x.MuaHangHienVat,
                    y => y.MapFrom(z => string.IsNullOrEmpty(z.MuaHangHienVat) ? 0 : double.Parse(z.MuaHangHienVat)))
                .ForMember(x => x.DacThu,
                    y => y.MapFrom(z => string.IsNullOrEmpty(z.DacThu) ? 0 : double.Parse(z.DacThu)));

            CreateMap<CanCuSoNhuCauQuery, CanCuDuToanQtCpSoNhuCauQuery>()
                .ForMember(x => x.KyHieu, y => y.MapFrom(z => z.KyHieu))
                .ForMember(x => x.TuChi, y => y.MapFrom(z => z.TuChi))
                .ForMember(x => x.HangNhap, y => y.MapFrom(z => z.HangNhap))
                .ForMember(x => x.HangMua, y => y.MapFrom(z => z.HangMua))
                .ForMember(x => x.PhanCap, y => y.MapFrom(z => z.PhanCap))
                .ForMember(x => x.MuaHangHienVat, y => y.MapFrom(z => z.MuaHangHienVat))
                .ForMember(x => x.DacThu, y => y.MapFrom(z => z.DacThu)).ReverseMap();
            CreateMap<DemandVoucherDetailImportModel, DemandVoucherDetailImportModelNSSD>();
            CreateMap<DemandVoucherDetailImportModelNSSD, DemandVoucherDetailImportModel>();
            CreateMap<DemandVoucherDetailImportModel, DemandVoucherDetailImportModelNSBD>();
            CreateMap<DemandVoucherDetailImportModelNSBD, DemandVoucherDetailImportModel>();
        }
    }
}