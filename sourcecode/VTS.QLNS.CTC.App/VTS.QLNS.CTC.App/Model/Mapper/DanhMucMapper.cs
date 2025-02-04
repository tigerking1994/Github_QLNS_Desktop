using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class DanhMucMapper : Profile
    {
        public DanhMucMapper()
        {
            CreateMap<Core.Domain.DanhMuc, CheckBoxItem>()
                .ForMember(entity => entity.DisplayItem, model => model.MapFrom(item => string.Join(" - ", item.IIDMaDanhMuc, item.STen)))
                .ForMember(entity => entity.ValueItem, model => model.MapFrom(item => item.SType == EstimationReport.DU_TOAN_THEO_NGANH ? item.SGiaTri : item.IIDMaDanhMuc))
                .ForMember(entity => entity.NameItem, model => model.MapFrom(item => item.IIDMaDanhMuc)); ;
            CreateMap<Core.Domain.DanhMuc, ComboboxItem>()
               .ForMember(entity => entity.DisplayItem, model => model.MapFrom(item => item.STen))
               .ForMember(entity => entity.ValueItem, model => model.MapFrom(item => item.SGiaTri));

            CreateMap<Core.Domain.DanhMuc, DanhMucNganhModel>();

            CreateMap<DanhMucNganhModel, Core.Domain.DanhMuc>();

            CreateMap<Core.Domain.DanhMuc, DanhMucNhomNganhModel>();

            CreateMap<DanhMucNhomNganhModel, Core.Domain.DanhMuc>();

            CreateMap<Core.Domain.DanhMuc, DanhMucCauHinhHeThongModel>();

            CreateMap<DanhMucCauHinhHeThongModel, Core.Domain.DanhMuc>();
            CreateMap<DmNhomChuKyModel, Core.Domain.DanhMuc>();
            CreateMap<Core.Domain.DanhMuc, DmNhomChuKyModel>();

            CreateMap<DmCapBacModel, Core.Domain.DanhMuc>();
            CreateMap<Core.Domain.DanhMuc, DmCapBacModel>();

            CreateMap<DanhMucNguonNganSachModel, Core.Domain.DanhMuc>();
            CreateMap<Core.Domain.DanhMuc, DanhMucNguonNganSachModel>();

            CreateMap<DmCapBacNhomChuKyModel, Core.Domain.DanhMuc>().ReverseMap();
        }
    }
}
