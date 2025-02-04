using AutoMapper;
using System;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.Model.Report;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NsMucLucNganSachMapper : Profile
    {
        public NsMucLucNganSachMapper()
        {
            CreateMap<NsMucLucNganSach, NsMuclucNgansachModel>();
            CreateMap<NsMucLucNganSachQuery, NsMuclucNgansachModel>();
            CreateMap<NsMuclucNgansachModel, NsMucLucNganSach>();
            CreateMap<NsMucLucNganSach, ComboboxItem>()
                .ForMember(entity => entity.DisplayItem, model => model.MapFrom(z => z.MoTa))
                .ForMember(entity => entity.ValueItem, model => model.MapFrom(z => z.Id));
            CreateMap<NsMuclucNgansachModel, ComboboxItem>()
                .ForMember(entity => entity.DisplayItem, model => model.MapFrom(z => z.MoTa))
                .ForMember(entity => entity.ValueItem, model => model.MapFrom(z => z.Id))
                .ForMember(entity => entity.HiddenValue, model => model.MapFrom(z => z.XauNoiMa));
            CreateMap<NsMucLucNganSach, CheckBoxItem>()
                .ForMember(entity => entity.DisplayItem, model => model.MapFrom(z => string.Format("{0} - {1}", z.Lns, z.MoTa)))
                .ForMember(entity => entity.ValueItem, model => model.MapFrom(z => z.Lns));
            CreateMap<NsMucLucNganSach, ComboboxItem>()
                .ForMember(entity => entity.ValueItem, model => model.MapFrom(z => z.Id.ToString()))
                .ForMember(entity => entity.DisplayItem, model => model.MapFrom(z => z.MoTa));
            CreateMap<NsMucLucNganSach, CheckBoxTreeItem>()
                .ForMember(entity => entity.DisplayItem, model => model.MapFrom(z => string.Format("{0} - {1}", z.Lns, z.MoTa)))
                .ForMember(entity => entity.Id, model => model.MapFrom(z => z.MlnsId))
                .ForMember(entity => entity.ParentId, model => model.MapFrom(z => z.MlnsIdParent))
                .ForMember(entity => entity.ValueItem, model => model.MapFrom(z => z.Lns));
            CreateMap<MLNSImportModel, NsMucLucNganSach>()
                //.ForMember(x => x.Id, y => y.MapFrom(z => string.IsNullOrEmpty(z.Id) ? Guid.Empty : Guid.Parse(z.Id)))
                .ForMember(x => x.Id, y => y.MapFrom(z => Guid.NewGuid()))
                .ForMember(x => x.NamLamViec, y => y.MapFrom(z => int.Parse(z.NamLamViec)));
            CreateMap<NsMucLucNganSach, MLNSImportModel>();
            CreateMap<NsMucLucNganSach, CauHinhMLNSModel>();
            CreateMap<CauHinhMLNSModel, NsMucLucNganSach>();
            CreateMap<NsMucLucNganSach, CauHinhUserMLNSModel>();
            CreateMap<CauHinhUserMLNSModel, NsMucLucNganSach>();
            CreateMap<NsMucLucNganSach, TLDmKinhPhiModel>();
            CreateMap<TLDmKinhPhiModel, NsMucLucNganSach>();
            CreateMap<NsMucLucNganSach, CauHinhMLNSChiTieuLuongModel>();
            CreateMap<CauHinhMLNSChiTieuLuongModel, NsMucLucNganSach>();

            CreateMap<NsMucLucNganSach, NsMucLucNganSachCha>();
            CreateMap<SettlementVoucherDetailImportModel, NsMucLucNganSach>();
            CreateMap<SettlementVoucherDetailImportModel, NsMuclucNgansachModel>();
            CreateMap<SoKyHieuMucLucNganSachModel, SoKyHieuMucLucNganSachQuery>();
            CreateMap<SoKyHieuMucLucNganSachQuery, SoKyHieuMucLucNganSachModel>();

            CreateMap<NsMucLucNganSach, NsMuclucNganSachChildModel>();
            CreateMap<NsMuclucNganSachChildModel, NsMucLucNganSach>();
            CreateMap<NsMuclucNganSachChildModel, NsMucLucNganSachQuery>();
            CreateMap<NsMucLucNganSachQuery, NsMuclucNganSachChildModel>()
                .ForMember(dest => dest.MoTa, source => source.MapFrom(c => c.MoTa));
            CreateMap<NsMuclucNgansachModel, NsMuclucNganSachChildModel>();
            CreateMap<NsMuclucNganSachChildModel, NsMuclucNgansachModel>();
            CreateMap<NsMucLucNganSach, NhQtChuyenQuyetToanChiTietModel>()
                .ForMember(entity => entity.iID_MaMucLucNganSach, model => model.MapFrom(z => z.MlnsId))
                .ForMember(entity => entity.iID_MaMucLucNganSach_Cha, model => model.MapFrom(z => z.MlnsIdParent))
                .ForMember(entity => entity.BHangCha, model => model.MapFrom(z => z.BHangCha))
                .ForMember(entity => entity.sXauNoiMa, model => model.MapFrom(z => z.XauNoiMa))
                .ForMember(entity => entity.sLNS, model => model.MapFrom(z => z.Lns))
                .ForMember(entity => entity.sL, model => model.MapFrom(z => z.L))
                .ForMember(entity => entity.sK, model => model.MapFrom(z => z.K))
                .ForMember(entity => entity.sM, model => model.MapFrom(z => z.M))
                .ForMember(entity => entity.sTM, model => model.MapFrom(z => z.Tm))
                .ForMember(entity => entity.sTTM, model => model.MapFrom(z => z.Ttm))
                .ForMember(entity => entity.sNG, model => model.MapFrom(z => z.Ng))
                .ForMember(entity => entity.sTNG, model => model.MapFrom(z => z.Tng))
                .ForMember(entity => entity.sMoTa, model => model.MapFrom(z => z.MoTa))
                .ForMember(entity => entity.iNamLamViec, model => model.MapFrom(z => z.NamLamViec)).ReverseMap();

            CreateMap<MucLucNganSachCheckDataModel, MucLucNganSachCheckDataQuery>().ReverseMap();
            CreateMap<NsMucLucNgansachMapModel, NsMucLucNganSach>().ReverseMap();
        }
    }
}
