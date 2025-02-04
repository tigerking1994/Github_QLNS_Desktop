using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NhTtThucHienNganSachMapper : Profile
    {
        public NhTtThucHienNganSachMapper()
        {
            CreateMap<NhTtThucHienNganSachModel, NhTtThucHienNganSach>().ReverseMap();
            CreateMap<NhTtThucHienNganSach, NhTtThucHienNganSachModel>().ReverseMap();
            CreateMap<NhTtThucHienNganSachQuery, NhTtThucHienNganSachModel>().ReverseMap();
            CreateMap<NhTtThucHienNganSachGiaiDoanModel, NhTtThucHienNganSachGiaiDoan>().ReverseMap();
            CreateMap<NhTtThucHienNganSachGiaiDoan, NhTtThucHienNganSachGiaiDoanModel>().ReverseMap();
            CreateMap<NhTtThucHienNganSachGiaiDoanQuery, NhTtThucHienNganSachGiaiDoanModel>().ReverseMap();
        }
    }
}
