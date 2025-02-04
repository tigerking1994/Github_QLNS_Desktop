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
    public class TnDtChungTuChiTietMapper : Profile
    {
        public TnDtChungTuChiTietMapper()
        {
            CreateMap<TnDtChungTuChiTiet, CheckBoxItem>()
                                        .ForMember(entity => entity.ValueItem, model => model.MapFrom(z => z.IdDonVi))
                                        .ForMember(entity => entity.DisplayItem, model => model.MapFrom(z => z.IdDonVi + "-" + z.TenDonVi));
            CreateMap<TnDtChungTuChiTietQuery, TnDtChungTuChiTietModel>()
                .ForMember(entity => entity.IsHangCha, model => model.MapFrom(z => z.BHangCha));
            CreateMap<TnDtChungTuChiTiet, TnDtChungTuChiTietModel>().ForMember(entity => entity.IsHangCha, model => model.MapFrom(z => z.BHangCha));
            CreateMap<TnDtChungTuChiTietModel, TnDtChungTuChiTiet>().ForMember(entity => entity.BHangCha, model => model.MapFrom(z => z.IsHangCha));
            CreateMap<RevenueExpenditurePlanDetailImportModel, TnDtChungTuChiTiet>()
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

            CreateMap<DivisionDetailImportModel, TnDtChungTuChiTiet>()
                .ForMember(x => x.Lns, y => y.MapFrom(z => z.LNS))
                .ForMember(x => x.L, y => y.MapFrom(z => z.L))
                .ForMember(x => x.K, y => y.MapFrom(z => z.K))
                .ForMember(x => x.M, y => y.MapFrom(z => z.M))
                .ForMember(x => x.Tm, y => y.MapFrom(z => z.TM))
                .ForMember(x => x.Ttm, y => y.MapFrom(z => z.TTM))
                .ForMember(x => x.Ng, y => y.MapFrom(z => z.NG))
                .ForMember(x => x.Tng, y => y.MapFrom(z => z.TNG))
                .ForMember(x => x.Tng1, y => y.MapFrom(z => z.TNG1))
                .ForMember(x => x.Tng2, y => y.MapFrom(z => z.TNG2))
                .ForMember(x => x.Tng3, y => y.MapFrom(z => z.TNG3))
                .ForMember(x => x.XauNoiMa, y => y.MapFrom(z => z.XauNoiMa))
                .ForMember(x => x.NoiDung, y => y.MapFrom(z => z.MoTa))
                .ForMember(x => x.TuChi, y => y.MapFrom(z => double.Parse(z.TuChi)));

        }
    }
}
