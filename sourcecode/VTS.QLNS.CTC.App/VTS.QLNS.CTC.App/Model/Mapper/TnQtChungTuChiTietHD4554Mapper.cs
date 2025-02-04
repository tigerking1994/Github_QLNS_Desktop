using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TnQtChungTuChiTietHD4554Mapper : Profile
    {
        public TnQtChungTuChiTietHD4554Mapper()
        {
            CreateMap<TnQtChungTuChiTietHD4554, TnQtChungTuChiTietHD4554Model>();
            CreateMap<TnQtChungTuChiTietHD4554Model, TnQtChungTuChiTietHD4554>();
            CreateMap<TnQtChungTuChiTietHD4554Query, TnQtChungTuChiTietHD4554Model>();
            CreateMap<TnQtChungTuChiTietHD4554Model, TnQtChungTuChiTietHD4554Query>();
            CreateMap<RealRevenueExpenditureDetailImportHD4554Model, TnQtChungTuChiTietHD4554>()
                .ForMember(x => x.SLNS, y => y.MapFrom(z => z.LNS))
                .ForMember(x => x.SGhiChu, y => y.MapFrom(z => z.Note))
                .ForMember(x => x.FSoTien, y => y.MapFrom(z => string.IsNullOrEmpty(z.SelfPaymentSettlement) ? 0 : double.Parse(z.SelfPaymentSettlement)))
                .ForMember(x => x.FSoTienDeNghi, y => y.MapFrom(z => string.IsNullOrEmpty(z.SoTienDeNghi) ? 0 : double.Parse(z.SoTienDeNghi)))
                .ForMember(x => x.SL, y => y.MapFrom(z => z.L))
                .ForMember(x => x.SK, y => y.MapFrom(z => z.K))
                .ForMember(x => x.SM, y => y.MapFrom(z => z.M))
                .ForMember(x => x.SNG, y => y.MapFrom(z => z.NG))
                .ForMember(x => x.STM, y => y.MapFrom(z => z.TM))
                .ForMember(x => x.STNG, y => y.MapFrom(z => z.TNG))
                .ForMember(x => x.STNG1, y => y.MapFrom(z => z.TNG1))
                .ForMember(x => x.STNG2, y => y.MapFrom(z => z.TNG2))
                .ForMember(x => x.STNG3, y => y.MapFrom(z => z.TNG3))
                .ForMember(x => x.STTM, y => y.MapFrom(z => z.TTM))
                .ForMember(x => x.SXauNoiMa, y => y.MapFrom(z => z.ConcatenateCode))
                .ForMember(x => x.BHangCha, y => y.MapFrom(z => z.BHangCha));
        }
    }
}
