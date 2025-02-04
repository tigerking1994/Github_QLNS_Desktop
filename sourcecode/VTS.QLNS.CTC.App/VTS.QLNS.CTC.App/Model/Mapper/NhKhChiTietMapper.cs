using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NhKhChiTietMapper : Profile
    {
        public NhKhChiTietMapper()
        {
            CreateMap<NhKhChiTiet, NhKhChiTietModel>();
            CreateMap<NhKhChiTietModel, NhKhChiTiet>();
            CreateMap<NhKhChiTietModel, NhKhChiTietQuery>();

            CreateMap<NhKhChiTietQuery, NhKhChiTietModel>().ForMember(entity => entity.SSoLanDc, model => model.MapFrom(x => string.Format("({0})", x.ILanDieuChinh)));
        }
    }
}
