using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    class QuyetToanChiPhiMapper : Profile
    {
        public QuyetToanChiPhiMapper()
        {
            CreateMap<PheDuyetQuyetToanDetailModel, VdtQtQuyetToanChiPhi>()
                .ForMember(entity => entity.IIdChiPhiId, model => model.MapFrom(item => item.IdChiPhi))
                .ForMember(entity => entity.FTienPheDuyet, model => model.MapFrom(item => item.GtQuyetToan));
        }
    }
}
