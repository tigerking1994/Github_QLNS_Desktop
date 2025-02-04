using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NhDaDuToanMapper : Profile
    {
        public NhDaDuToanMapper()
        {
            CreateMap<Core.Domain.NhDaDuToan, NhDaDuToanModel>();
            CreateMap<Core.Domain.Query.NhDaDuToanQuery, NhDaDuToanModel>();
            CreateMap<Core.Domain.Query.NhDaDuToanQuery, Core.Domain.NhDaDuToan>();
            CreateMap<NhDaDuToanModel, Core.Domain.NhDaDuToan>();
            CreateMap<Core.Domain.Query.NhDaDuToanChiPhiQuery, NhDaDuToanChiPhiModel>();
            CreateMap<NhDaDuToanChiPhiModel, Core.Domain.NhDaDuToanChiPhi>();
            CreateMap<Core.Domain.Query.NhDaDuToanHangMucQuery, NhDaDuToanHangMucModel>();
            CreateMap<NhDaDuToanHangMucModel, Core.Domain.NhDaDuToanHangMuc>();
            //CreateMap<Core.Domain.Query.NhDaDuToanNguonVonQuery, NhDaDuToanNguonVonModel>();
            //CreateMap<NhDaDuToanNguonVonModel, Core.Domain.NhDaDuToanNguonVon>();
        }
    }
}
