using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class ImpTnDtThuNopMapper : Profile
    {
        public ImpTnDtThuNopMapper()
        {
            CreateMap<RevenueExpenditurePlanDetailImportModel, ImpTnDtThuNop>()
                .ForMember(x => x.MlnsId, y => y.MapFrom(z => Guid.Parse(z.MlnsId)))
                .ForMember(x => x.MlnsIdParent, y => y.MapFrom(z => Guid.Parse(z.MlnsIdParent)))
                .ForMember(x => x.Lns, y => y.MapFrom(z => z.LNS))
                .ForMember(x => x.L, y => y.MapFrom(z => z.L))
                .ForMember(x => x.K, y => y.MapFrom(z => z.K))
                .ForMember(x => x.M, y => y.MapFrom(z => z.M))
                .ForMember(x => x.Tm, y => y.MapFrom(z => z.TM))
                .ForMember(x => x.Ttm, y => y.MapFrom(z => z.TTM))
                .ForMember(x => x.Ng, y => y.MapFrom(z => z.NG))
                .ForMember(x => x.Tng, y => y.MapFrom(z => z.TNG))
                .ForMember(x => x.XauNoiMa, y => y.MapFrom(z => z.ConcatenateCode))
                .ForMember(x => x.NoiDung, y => y.MapFrom(z => z.Description))
                .ForMember(x => x.GhiChu, y => y.MapFrom(z => z.Note))
                .ForMember(x => x.TuChi, y => y.MapFrom(z => string.IsNullOrEmpty(z.SelfPaymentSettlement) ? 0 : double.Parse(z.SelfPaymentSettlement)));
        }
    }
}
