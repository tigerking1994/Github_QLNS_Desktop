using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class KHLCNhaThauMapper : Profile
    {
        public KHLCNhaThauMapper()
        {
            CreateMap<KHLCNhaThauModel, KHLCNhaThauQuery>();
            CreateMap<KHLCNhaThauQuery, KHLCNhaThauModel>();

            CreateMap<KHLCNhaThauDetailQuery, KHLCNhaThauDetailModel>();
            CreateMap<KHLCNhaThauDetailModel, KHLCNhaThauDetailQuery>();

            CreateMap<VdtKhlcNhaThauCanCuQuery, VdtKhlcNhaThauCanCuModel>();
            CreateMap<VdtKhlcNhaThauCanCuModel, VdtKhlcNhaThauCanCuQuery>();

            CreateMap<ChiPhiHangMucQuery, ChiPhiHangMucModel>();
            CreateMap<ChiPhiHangMucModel, ChiPhiHangMucQuery>();

            CreateMap<KHLCNhaThauModel, VdtQddtKhlcnhaThau>();
            CreateMap<VdtQddtKhlcnhaThau, KHLCNhaThauModel>();
        }
    }
}
