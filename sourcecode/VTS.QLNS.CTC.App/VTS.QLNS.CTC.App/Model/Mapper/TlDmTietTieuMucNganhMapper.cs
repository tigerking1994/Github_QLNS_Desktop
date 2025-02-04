using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TlDmTietTieuMucNganhMapper : Profile
    {
        public TlDmTietTieuMucNganhMapper()
        {
            CreateMap<Core.Domain.TlDmTietTieuMucNganh, TlDmTietTieuMucNganhModel>();
            CreateMap<TlDmTietTieuMucNganhModel, Core.Domain.TlDmTietTieuMucNganh>();
        }
    }
}
