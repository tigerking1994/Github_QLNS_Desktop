using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NhKhChiTietHopDongMapper : Profile
    {
        public NhKhChiTietHopDongMapper()
        {
            CreateMap<NhKhChiTietHopDong, NhKhChiTietHopDongModel>();
            CreateMap<NhKhChiTietHopDongModel, NhKhChiTietHopDong>();
        }
    }
}
