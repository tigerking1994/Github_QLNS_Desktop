using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    class VdtDaNguonVonMapper : Profile
    {
        public VdtDaNguonVonMapper()
        {
            CreateMap<VdtDaNguonVonModel, VdtDaNguonVon>();
            CreateMap<VdtDaNguonVon, VdtDaNguonVon>();
            CreateMap<DuAnKeHoachTrungHanModel, VdtDaNguonVonModel>()
                .ForMember(entity => entity.FThanhTien, model => model.MapFrom(z => z.FHanMucDauTu))
                .ForMember(entity => entity.IIdDuAn, model => model.MapFrom(z => z.IIdDuAnId))
                .ForMember(entity => entity.IIdNguonVonId, model => model.MapFrom(z => z.IIdNguonVonId));

            CreateMap<VdtDaNguonVonModel, DuAnKeHoachTrungHanModel>()
                .ForMember(entity => entity.FHanMucDauTu, model => model.MapFrom(z => z.FThanhTien))
                .ForMember(entity => entity.IIdDuAnId, model => model.MapFrom(z => z.IIdDuAn))
                .ForMember(entity => entity.IIdNguonVonId, model => model.MapFrom(z => z.IIdNguonVonId));

            CreateMap<VdtDaDuAn, VdtDaNguonVonModel>()
                .ForMember(entity => entity.IIdDuAn, model => model.MapFrom(z => z.Id))
                .ForMember(entity => entity.IIdNguonVonId, model => model.MapFrom(z => z.IIdNguonVonId))
                .ForMember(entity => entity.FThanhTien, model => model.MapFrom(z => z.FHanMucDauTu));

            CreateMap<VdtDaDuAn, VdtDaDuAnHangMuc>()
                .ForMember(entity => entity.IIdDuAnId, model => model.MapFrom(x => x.Id))
                .ForMember(entity => entity.IdLoaiCongTrinh, model => model.MapFrom(x => x.IIdLoaiCongTrinhId))
                .ForMember(entity => entity.iID_NguonVonID, model => model.MapFrom(x => x.IIdNguonVonId))
                .ForMember(entity => entity.fHanMucDauTu, model => model.MapFrom(x => x.FHanMucDauTu))
                .ForMember(entity => entity.SMaDuAn, model => model.MapFrom(x => x.SMaDuAn))
                .ForMember(entity => entity.STenHangMuc, model => model.MapFrom(x => x.STenHangMuc));

            CreateMap<VDTDaNguonVonQuery, VdtDaNguonVonModel>();
        }
    }
}
