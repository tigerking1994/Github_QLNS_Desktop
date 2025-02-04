using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    class PheDuyetQuyetToanDetailMapper : Profile
    {
        public PheDuyetQuyetToanDetailMapper()
        {
            CreateMap<PheDuyetQuyetToanDetailQuery, PheDuyetQuyetToanDetailModel>()
                .ForMember(entity => entity.Id, model => model.MapFrom(item => item.Id.HasValue ? item.Id.Value : Guid.Empty));
        }
    }
}
