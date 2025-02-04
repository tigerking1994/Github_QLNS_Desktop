using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NsNguoiDungDonViMapper : Profile
    {
        public NsNguoiDungDonViMapper()
        {
            CreateMap<NguoiDungDonVi, NsNguoiDungDonViModel>();
            CreateMap<NsNguoiDungDonViModel, NguoiDungDonVi>();
        }
    }
}
