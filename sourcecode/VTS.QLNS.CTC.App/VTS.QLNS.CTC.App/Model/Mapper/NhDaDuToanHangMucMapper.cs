using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NhDaDuToanHangMucMapper : Profile
    {
        public NhDaDuToanHangMucMapper()
        {
            CreateMap<Core.Domain.NhDaQdDauTuHangMuc, NhDaDuToanHangMucModel>();
            CreateMap<NhDaQdDauTuHangMucModel, NhDaDuToanHangMucModel>();
            CreateMap<Core.Domain.NhDaDuToanHangMuc, NhDaDuToanHangMucModel>();
            CreateMap<NhDaDuToanHangMucModel, Core.Domain.NhDaDuToanHangMuc>();
        }
    }
}
