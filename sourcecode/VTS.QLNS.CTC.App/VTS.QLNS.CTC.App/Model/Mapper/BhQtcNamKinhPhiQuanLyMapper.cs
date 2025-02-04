using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class BhQtcNamKinhPhiQuanLyMapper : Profile
    {
        public BhQtcNamKinhPhiQuanLyMapper()
        {
            CreateMap<BhQtcNamKinhPhiQuanLyModel, BhQtcNamKinhPhiQuanLy>().ReverseMap();
            CreateMap<BhQtcNamKinhPhiQuanLyModel, BhQtcNamKinhPhiQuanLyQuery>().ReverseMap();
            CreateMap<Core.Domain.Query.BhQtcNamKinhPhiQuanLyQuery, ComboboxItem>()
           .ForMember(entity => entity.DisplayItem, model => model.MapFrom(item => string.Format("{0} Ngày: {1}", item.SSoChungTu, item.DNgayChungTu.HasValue ? item.DNgayChungTu.Value.ToString("dd/MM/yyyy") : string.Empty)))
           .ForMember(entity => entity.HiddenValue, model => model.MapFrom(item => item.DNgayChungTu.HasValue ? item.DNgayChungTu.Value.ToString("dd/MM/yyyy") : string.Empty))
           .ForMember(entity => entity.ValueItem, model => model.MapFrom(item => item.SSoChungTu));
        }
    }
}
