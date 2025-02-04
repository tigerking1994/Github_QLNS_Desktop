using AutoMapper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TnDtChungTuMapper : Profile
    {
        public TnDtChungTuMapper()
        {
            CreateMap<TnDtChungTu, CheckBoxItem>()
               .ForMember(entity => entity.DisplayItem, model => model.MapFrom(item => item.SoChungTu + " - " + item.MoTaChiTiet))
               .ForMember(entity => entity.ValueItem, model => model.MapFrom(item => item.SoChungTu))
               .ForMember(entity => entity.NameItem, model => model.MapFrom(item => item.Id));
            CreateMap<TnDtChungTu, ComboboxItem>()
              .ForMember(entity => entity.DisplayItem, model => model.MapFrom(item => item.SoChungTu + " - " + item.MoTaChiTiet))
              .ForMember(entity => entity.ValueItem, model => model.MapFrom(item => item.SoChungTu));
            CreateMap<TnDtChungTu, TnDtChungTuModel>();
            CreateMap<TnDtChungTuModel, TnDtChungTu>();
            CreateMap<RevenueExpenditurePlanImportModel, TnDtChungTu>()
                .ForMember(x => x.NgayChungTu, y => y.MapFrom(z => DateTime.Parse(z.NgayChungTu, CultureInfo.CreateSpecificCulture("vi-VN"))))
                .ForMember(x => x.TuChiSum, y => y.MapFrom(z => double.Parse(z.TuChiSum)))
                .ForMember(x => x.SoQuyetDinh, y => y.MapFrom(z => z.SoQuyetDinh))
                .ForMember(x => x.Lns, y => y.MapFrom(z => z.Lns))
                .ForMember(x => x.NgayQuyetDinh, y => y.MapFrom(z => DateTime.Parse(z.NgayQuyetDinh, CultureInfo.CreateSpecificCulture("vi-VN"))))
                .ForMember(x => x.MoTaChiTiet, y => y.MapFrom(z => z.MoTaChiTiet));
            CreateMap<RevenueExpenditureDivisionImportModel, TnDtChungTu>()
                .ForMember(x => x.NgayChungTu, y => y.MapFrom(z => DateTime.Parse(z.NgayChungTu, CultureInfo.CreateSpecificCulture("vi-VN"))))
                .ForMember(x => x.TuChiSum, y => y.MapFrom(z => double.Parse(z.TuChiSum)))
                .ForMember(x => x.SoQuyetDinh, y => y.MapFrom(z => z.SoQuyetDinh))
                .ForMember(x => x.Lns, y => y.MapFrom(z => z.Lns))
                .ForMember(x => x.NgayQuyetDinh, y => y.MapFrom(z => DateTime.Parse(z.NgayQuyetDinh, CultureInfo.CreateSpecificCulture("vi-VN"))))
                .ForMember(x => x.MoTaChiTiet, y => y.MapFrom(z => z.MoTaChiTiet))
                .ForMember(x => x.IdDotNhan, y => y.MapFrom(z => z.DotNhan));
            CreateMap<TnDtChungTuModel, ComboboxItem>()
                .ForMember(entity => entity.DisplayItem, model => model.MapFrom(item => $"{item.SoQuyetDinh} - {item.NgayQuyetDinh.Value:dd/MM/yyyy}"))
                .ForMember(entity => entity.HiddenValue, model => model.MapFrom(item => item.Id.ToString()))
                .ForMember(entity => entity.ValueItem, model => model.MapFrom(item => item.IdDotNhan))
                .ForMember(entity => entity.HiddenValueOption2, model => model.MapFrom(item => $"{item.NgayQuyetDinh.Value:dd/MM/yyyy}"));
        }
    }
}
