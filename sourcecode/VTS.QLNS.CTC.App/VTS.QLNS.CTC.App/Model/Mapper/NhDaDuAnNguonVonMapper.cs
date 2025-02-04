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
    public class NhDaDuAnNguonVonMapper : Profile
    {
        public NhDaDuAnNguonVonMapper()
        {
            CreateMap<NhDaDuAnNguonVon, NhDaDuAnNguonVonModel>();
            CreateMap<NhDaDuAnNguonVonModel, NhDaDuAnNguonVon>();
            CreateMap<NhDaDuAnNguonVonImportModel, NhDaDuAnNguonVonModel>()
                .ForMember(entity => entity.IIdNguonVonId, model => model.MapFrom(item => Convert.ToInt32(item.IIdNguonVonId)))
                .ForMember(entity => entity.FGiaTriUsd, model => model.MapFrom(item => Convert.ToDouble(item.FGiaTriUsd.Replace(',', '.'))))
                .ForMember(entity => entity.FGiaTriVnd, model => model.MapFrom(item => Convert.ToDouble(item.FGiaTriVnd.Replace(',', '.'))))
                .ReverseMap();
        }
    }
}
