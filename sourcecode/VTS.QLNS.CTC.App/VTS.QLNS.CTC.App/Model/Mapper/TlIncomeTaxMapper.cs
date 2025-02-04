using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TlIncomeTaxMapper : Profile
    {
        public TlIncomeTaxMapper()
        {
            CreateMap<TlDmCanBo, IncomeTaxModel>()
                .ForMember(entity => entity.MaCb, model => model.MapFrom(item => item.MaCanBo))
                .ForMember(entity => entity.Thang, model => model.MapFrom(item => item.Thang))
                .ForMember(entity => entity.SoSoLuong, model => model.MapFrom(item => item.SoSoLuong))
                .ForMember(entity => entity.TenCb, model => model.MapFrom(item => item.TenCanBo))
                .ForMember(entity => entity.Nam, model => model.MapFrom(item => item.Nam))
                .ForMember(entity => entity.MaDonVi, model => model.MapFrom(item => item.Parent));
            CreateMap<SalaryMonthTncnImportModel, IncomeTaxModel>()
                .ForMember(entity => entity.TienThuong, model => model.MapFrom(item => !string.IsNullOrEmpty(item.TienThuong) ? decimal.Parse(item.TienThuong) : 0))
                .ForMember(entity => entity.LoiIchKhac, model => model.MapFrom(item => !string.IsNullOrEmpty(item.LoiIchKhac) ? decimal.Parse(item.LoiIchKhac) : 0))
                .ForMember(entity => entity.TienThueDuocGiam, model => model.MapFrom(item => !string.IsNullOrEmpty(item.TienThueDuocGiam) ? decimal.Parse(item.TienThueDuocGiam) : 0))
                .ForMember(entity => entity.ThueTNCNDaNop, model => model.MapFrom(item => !string.IsNullOrEmpty(item.ThueTNCNDaNop) ? decimal.Parse(item.ThueTNCNDaNop) : 0));
        }
    }
}
