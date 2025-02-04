using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NsNguoiDungLnsMapper : Profile
    {
        public NsNguoiDungLnsMapper()
        {
            CreateMap<NsNguoiDungLns, NsNguoiDungLnsModel>();
            CreateMap<NsNguoiDungLnsModel, NsNguoiDungLns>();
        }
    }
}
