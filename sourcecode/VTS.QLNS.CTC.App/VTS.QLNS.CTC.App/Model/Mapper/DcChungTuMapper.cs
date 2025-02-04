using AutoMapper;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class DcChungTuMapper : Profile
    {
        public DcChungTuMapper()
        {
            CreateMap<DcChungTuModel, NsDcChungTu>();
            CreateMap<NsDcChungTu, DcChungTuModel>();
            CreateMap<NsDcChungTuQuery, DcChungTuModel>();
            CreateMap<NsDcChungTuChiTietQuery, DcChungTuChiTietModel>()
                .ForMember(entity => entity.IsHangCha, model => model.MapFrom(z => z.BHangChaDuToan)).ReverseMap();
            CreateMap<DcChungTuChiTietModel, NsDcChungTuChiTiet>().ReverseMap();
            CreateMap<NsDcChungTuChiTietQuery, NsDcChungTuChiTiet>()
                .ForMember(entity => entity.BHangCha, model => model.MapFrom(z => z.BHangChaDuToan));
            CreateMap<NsDcChungTuQuery, ComboboxManyItem>()
                .ForMember(entity => entity.DisplayItem, model => model.MapFrom(item => item.SSoChungTu))
                .ForMember(entity => entity.DisplayItem1, model => model.MapFrom(item => item.DNgayChungTu.ToString("dd/MM/yyyy")))
                .ForMember(entity => entity.IndexItem, model => model.MapFrom(item => item.ISoChungTuIndex))
                .ForMember(entity => entity.ValueItem, model => model.MapFrom(item => item.Id));
            CreateMap<AdjustedEstimateDetailImportModel, NsDcChungTuChiTiet>()
                .ForMember(x => x.SLns, y => y.MapFrom(z => z.LNS))
                .ForMember(x => x.SL, y => y.MapFrom(z => z.L))
                .ForMember(x => x.SK, y => y.MapFrom(z => z.K))
                .ForMember(x => x.SM, y => y.MapFrom(z => z.M))
                .ForMember(x => x.STm, y => y.MapFrom(z => z.TM))
                .ForMember(x => x.STtm, y => y.MapFrom(z => z.TTM))
                .ForMember(x => x.SNg, y => y.MapFrom(z => z.NG))
                .ForMember(x => x.STng, y => y.MapFrom(z => z.TNG))
                .ForMember(x => x.SXauNoiMa, y => y.MapFrom(z => z.ConcatenateCode))
                .ForMember(x => x.SMoTa, y => y.MapFrom(z => z.Description))
                .ForMember(x => x.FDuToanNganSachNam, y => y.MapFrom(z => string.IsNullOrEmpty(z.DuToanNganSachNam) ? 0 : double.Parse(z.DuToanNganSachNam)))
                .ForMember(x => x.FDuToanChuyenNamSau, y => y.MapFrom(z => string.IsNullOrEmpty(z.DuToanChuyenNamSau) ? 0 : double.Parse(z.DuToanChuyenNamSau)))
                .ForMember(x => x.FDuKienQtDauNam, y => y.MapFrom(z => string.IsNullOrEmpty(z.DuKienQtDauNam) ? 0 : double.Parse(z.DuKienQtDauNam)))
                .ForMember(x => x.FDuKienQtCuoiNam, y => y.MapFrom(z => string.IsNullOrEmpty(z.DuKienQtCuoiNam) ? 0 : double.Parse(z.DuKienQtCuoiNam)));
        }
    }
}
