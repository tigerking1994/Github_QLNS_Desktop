using AutoMapper;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class DtChungTuMapMapper : Profile
    {
       public DtChungTuMapMapper()
       {
           CreateMap<NsDtNhanPhanBoMap, DtNhanPhanBoMap>();
           CreateMap<DtChungTuModel, ComboboxItem>()
                .ForMember(entity => entity.DisplayItem, model => model.MapFrom(item => $"{item.SSoQuyetDinh} - {item.DNgayQuyetDinh.Value:dd/MM/yyyy}"))
                .ForMember(entity => entity.HiddenValue, model => model.MapFrom(item => item.Id.ToString()))
                .ForMember(entity => entity.ValueItem, model => model.MapFrom(item => item.IIdDotNhan))
                .ForMember(entity => entity.HiddenValueOption2, model => model.MapFrom(item => $"{item.DNgayQuyetDinh.Value:dd/MM/yyyy}"));
        }
    }
}
