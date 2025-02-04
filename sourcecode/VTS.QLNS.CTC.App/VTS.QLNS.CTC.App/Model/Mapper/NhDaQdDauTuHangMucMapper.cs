using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NhDaQdDauTuHangMucMapper : Profile
    {
        public NhDaQdDauTuHangMucMapper()
        {
            CreateMap<NhDaChuTruongDauTuHangMuc, NhDaQdDauTuHangMucModel>();
            CreateMap<NhDaChuTruongDauTuHangMucModel, NhDaQdDauTuHangMucModel>();
            CreateMap<NhDaQdDauTuHangMuc, NhDaQdDauTuHangMucModel>();
            CreateMap<NhDaQdDauTuHangMucModel, NhDaQdDauTuHangMuc>();
        }
    }
}
