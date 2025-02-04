using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NhDmPhuongThucChonNhaThauMapper : Profile
    {
        public NhDmPhuongThucChonNhaThauMapper()
        {
            CreateMap<NhDmPhuongThucChonNhaThau, NhDmPhuongThucChonNhaThauModel>();
            CreateMap<NhDmPhuongThucChonNhaThauModel, NhDmPhuongThucChonNhaThau>();
        }
    }
}
