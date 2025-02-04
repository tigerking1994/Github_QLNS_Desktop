using AutoMapper;
using System;
using System.Globalization;
using System.Linq;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class DtChungTuMapper : Profile
    {
       public DtChungTuMapper()
       {
            CreateMap<NsDtChungTu, ComboboxManyItem>()
                    .ForMember(entity => entity.DisplayItem, model => model.MapFrom(item => item.SSoChungTu))
                    .ForMember(entity => entity.DisplayItem1, model => model.MapFrom(item => item.SSoQuyetDinh))
                    .ForMember(entity => entity.DisplayItem2, model => model.MapFrom(item => item.DNgayChungTu.HasValue ? item.DNgayChungTu.Value.ToString("dd/MM/yyyy") : string.Empty))
                    .ForMember(entity => entity.DisplayItem3, model => model.MapFrom(item => item.DNgayQuyetDinh.HasValue ? item.DNgayQuyetDinh.Value.ToString("dd/MM/yyyy") : string.Empty))
                    .ForMember(entity => entity.DisplayItem4, model => model.MapFrom(item => item.SMoTa))
                    .ForMember(entity => entity.IndexItem, model => model.MapFrom(item => item.ISoChungTuIndex))
                    .ForMember(entity => entity.ValueItem, model => model.MapFrom(item => item.SSoChungTu));
            CreateMap<DtChungTuModel, ComboboxManyItem>()
                .ForMember(entity => entity.DisplayItem, model => model.MapFrom(item => item.SSoChungTu))
                .ForMember(entity => entity.DisplayItem1, model => model.MapFrom(item => item.SSoQuyetDinh))
                .ForMember(entity => entity.DisplayItem2, model => model.MapFrom(item => item.DNgayChungTu.HasValue ? item.DNgayChungTu.Value.ToString("dd/MM/yyyy") : string.Empty))
                .ForMember(entity => entity.DisplayItem3, model => model.MapFrom(item => item.DNgayQuyetDinh.HasValue ? item.DNgayQuyetDinh.Value.ToString("dd/MM/yyyy") : string.Empty))
                .ForMember(entity => entity.DisplayItem4, model => model.MapFrom(item => item.SMoTa))
                .ForMember(entity => entity.IndexItem, model => model.MapFrom(item => item.ISoChungTuIndex))
                .ForMember(entity => entity.ValueItem, model => model.MapFrom(item => item.SSoChungTu));
            CreateMap<NsDtChungTu, CheckBoxItem>()
               .ForMember(entity => entity.DisplayItem, model => model.MapFrom(item => item.SSoChungTu + " - " + item.SMoTa))
               .ForMember(entity => entity.ValueItem, model => model.MapFrom(item => item.SSoChungTu));
            CreateMap<NsDtChungTu, ComboboxItem>()
                .ForMember(entity => entity.DisplayItem, model => model.MapFrom(item => string.Format("{0} {1} {2} {3}",
                                                                        item.SSoChungTu, item.SSoQuyetDinh, item.DNgayChungTu, item.SMoTa)))
                .ForMember(entity => entity.ValueItem, model => model.MapFrom(item => item.SSoChungTu));
            CreateMap<NsDtChungTu, DtChungTuModel>();
                
            CreateMap<DtChungTuModel, NsDtChungTu>();

            CreateMap<DivisionImportModel, NsDtChungTu>()
                .ForMember(x => x.DNgayChungTu, y => y.MapFrom(z => DateTime.Parse(z.NgayChungTu, CultureInfo.CreateSpecificCulture("vi-VN"))))
                .ForMember(x => x.DNgayQuyetDinh, y => y.MapFrom(z => DateTime.Parse(z.NgayQuyetDinh, CultureInfo.CreateSpecificCulture("vi-VN"))))
                .ForMember(x => x.DNgayTao, y => y.MapFrom(z => DateTime.Parse(z.DateCreated, CultureInfo.CreateSpecificCulture("vi-VN"))));
            ;

            CreateMap<NsDtChungTu, ChungTuCanCuModel>()
                .ForMember(model => model.SoChungTu, member => member.MapFrom(entity => entity.SSoChungTu))
                .ForMember(model => model.NgayChungTu, member => member.MapFrom(entity => entity.DNgayChungTu))
                .ForMember(model => model.SoQuyetDinh, member => member.MapFrom(entity => entity.SSoQuyetDinh))
                .ForMember(model => model.NgayQuyetDinh, member => member.MapFrom(entity => entity.DNgayQuyetDinh))
                .ForMember(model => model.LoaiDuToan, member => member.MapFrom(entity => entity.ILoaiDuToan));

            CreateMap<NsDtChungTuDotNhanQuery, DtChungTuModel>();
            CreateMap<NsDtChungTuQuery, DtChungTuModel>();
        }
    }
}
