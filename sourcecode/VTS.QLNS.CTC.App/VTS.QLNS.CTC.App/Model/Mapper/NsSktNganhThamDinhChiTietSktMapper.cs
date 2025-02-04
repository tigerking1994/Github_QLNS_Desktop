using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NsSktNganhThamDinhChiTietSktMapper : Profile
    {
        public NsSktNganhThamDinhChiTietSktMapper()
        {
            CreateMap<NsSktNganhThamDinhChiTietSkt, NsSktNganhThamDinhChiTietSktModel>().ReverseMap();
            CreateMap<NsSktNganhThamDinhChiTiet, NsSktNganhThamDinhChiTietSktModel>(); 
            CreateMap<ExpertiseImportModel, ExpertiseNTDImportModel>().ReverseMap();
            CreateMap<JsonNsSktNganhThamDinhChiTietSktQuery, NsSktNganhThamDinhChiTietSkt>();
            CreateMap<NsSktNganhThamDinhChiTietSkt, JsonNsSktNganhThamDinhChiTietSktQuery>();
        }
    }
}
