using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class BhBaoCaoGhiChuMapper : Profile
    {
        public BhBaoCaoGhiChuMapper()
        {
            CreateMap<BhCauHinhBaoCao, BhCauHinhBaoCaoModel>().ReverseMap();
            CreateMap<DonVi, BhCauHinhBaoCaoModel>()
                .ForMember(model => model.IIdMaDonVi, entity => entity.MapFrom(item => item.IIDMaDonVi))
                .ForMember(model => model.STenDonVi, entity => entity.MapFrom(item => item.TenDonVi));

        }
    }
}
