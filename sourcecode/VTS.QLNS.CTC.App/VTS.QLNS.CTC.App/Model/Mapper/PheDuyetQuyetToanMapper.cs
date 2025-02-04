using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class PheDuyetQuyetToanMapper : Profile
    {
        public PheDuyetQuyetToanMapper()
        {
            CreateMap<VdtQtQuyetToanQuery, PheDuyetQuyetToanModel>()
                .ForMember(entity => entity.TongMucDauTuPheDuyet, model => model.MapFrom(item => item.TongMucDauTuPheDuyet.HasValue ? item.TongMucDauTuPheDuyet.Value : 0))
                .ForMember(entity => entity.TienQuyetToanPheDuyet, model => model.MapFrom(item => item.TienQuyetToanPheDuyet.HasValue ? item.TienQuyetToanPheDuyet.Value : 0));

            CreateMap<VdtQtQuyetToan, PheDuyetQuyetToanModel>()
                .ForMember(entity => entity.IdDuAn, model => model.MapFrom(item => item.IIdDuAnId));

            CreateMap<DeNghiQuyetToanChiTietModel, PheDuyetQuyetToanProcessModel>();
        }
    }
}
