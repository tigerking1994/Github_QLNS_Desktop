using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class VdtDaDuAnHangMucMapper : Profile
    {
        public VdtDaDuAnHangMucMapper()
        {
            CreateMap<VdtDaDuAnHangMuc, DuAnKeHoachTrungHanModel>()
                .ForMember(entity => entity.IIdDuAnId, model => model.MapFrom(z => z.IIdDuAnId))
                .ForMember(entity => entity.IIdNguonVonId, model => model.MapFrom(z => z.iID_NguonVonID))
                .ForMember(entity => entity.STenDuAn, model => model.MapFrom(z => z.STenHangMuc))
                .ForMember(entity => entity.SMaHangMuc, model => model.MapFrom(z => z.SMaHangMuc))
                .ForMember(entity => entity.sMaOrder, model => model.MapFrom(z => z.MaOrDer))
                .ForMember(entity => entity.FHanMucDauTu, model => model.MapFrom(z => z.fHanMucDauTu))
                .ForMember(entity => entity.IIdParentHangMuc, model => model.MapFrom(z => z.IIdParentId));

            CreateMap<DuAnKeHoachTrungHanModel, VdtDaDuAnHangMuc>()
                .ForMember(entity => entity.IIdDuAnId, model => model.MapFrom(z => z.IIdDuAnId))
                .ForMember(entity => entity.iID_NguonVonID, model => model.MapFrom(z => z.IIdNguonVonId))
                .ForMember(entity => entity.STenHangMuc, model => model.MapFrom(z => z.STenDuAn))
                .ForMember(entity => entity.SMaHangMuc, model => model.MapFrom(z => z.SMaHangMuc))
                .ForMember(entity => entity.MaOrDer, model => model.MapFrom(z => z.sMaOrder))
                .ForMember(entity => entity.fHanMucDauTu, model => model.MapFrom(z => z.FHanMucDauTu))
                .ForMember(entity => entity.indexMaHangMuc, model => model.MapFrom(z => z.IndexHangMuc))
                .ForMember(entity => entity.IIdParentId, model => model.MapFrom(z => z.IIdParentHangMuc));
        }
    }
}
