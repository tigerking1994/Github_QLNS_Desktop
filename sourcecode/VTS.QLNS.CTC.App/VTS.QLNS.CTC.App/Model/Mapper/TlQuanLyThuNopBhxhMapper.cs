using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TlQuanLyThuNopBhxhMapper : Profile
    {
        public TlQuanLyThuNopBhxhMapper()
        {
            CreateMap<TlQuanLyThuNopBhxhModel, TlQuanLyThuNopBhxh>().ReverseMap();
            CreateMap<TlQuanLyThuNopBhxhQuery, TlQuanLyThuNopBhxhModel>().ReverseMap();
            CreateMap<TlQuanLyThuNopBhxhChiTiet, TlQuanLyThuNopBhxhChiTietModel>().ReverseMap();
            CreateMap<TlQuanLyThuNopBhxhChiTiet, TlQuanLyThuNopBhxhNq104ChiTietModel>().ReverseMap();
            CreateMap<FeeCollectionBhxhImportModel, TlQuanLyThuNopBhxhChiTiet>()
                .ForMember(entity => entity.GiaTri, model => model.MapFrom(item => (decimal)NumberUtils.ConvertTextToNumber(item.GiaTri)))
                .ForMember(entity => entity.SMaCbo, model => model.MapFrom(item => item.SMaCanBo))
                .ForMember(entity => entity.SMaPhuCap, model => model.MapFrom(item => item.SMaPhuCap))
                .ForMember(entity => entity.STenCbo, model => model.MapFrom(item => item.STenCanBo))
                .ForMember(entity => entity.SMaCb, model => model.MapFrom(item => item.SMaCapBac))
                .ForMember(entity => entity.SMaCachTl, model => model.MapFrom(item => item.SMaCachTinhLuong))
                .ForMember(entity => entity.SMaHieuCanBo, model => model.MapFrom(item => item.SMaHieuCanBo))
                .ForMember(entity => entity.IThang, model => model.MapFrom(item => int.Parse(item.IThang)))
                .ForMember(entity => entity.INam, model => model.MapFrom(item => int.Parse(item.INam)))
                .ReverseMap();
        }
    }
}
