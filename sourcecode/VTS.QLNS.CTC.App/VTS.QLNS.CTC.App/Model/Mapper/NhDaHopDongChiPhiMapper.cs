using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NhDaHopDongChiPhiMapper : Profile 
    {
        public NhDaHopDongChiPhiMapper()
        {
            CreateMap<NhDaHopDongNguonVon, NhDaHopDongNguonVonModel>();
            CreateMap<NhDaHopDongNguonVonModel, NhDaHopDongNguonVon>();
            CreateMap<NhDaHopDongChiPhi, NhDaHopDongChiPhiModel>();
            CreateMap<NhDaHopDongChiPhiModel, NhDaHopDongChiPhi>();

            CreateMap<NhDaHopDongTrongNuocChiPhiGoiThauModel, NhDaHopDongChiPhi>();
            CreateMap<NhDaHopDongChiPhi, NhDaHopDongTrongNuocChiPhiGoiThauModel>();
        }
    }
}
