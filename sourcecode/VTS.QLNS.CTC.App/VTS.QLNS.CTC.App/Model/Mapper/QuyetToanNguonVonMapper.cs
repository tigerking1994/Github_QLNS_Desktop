using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class QuyetToanNguonVonMapper : Profile
    {
        public QuyetToanNguonVonMapper()
        {
            CreateMap<PheDuyetQuyetToanDetailModel, VdtQtQuyetToanNguonvon>()
                .ForMember(entity => entity.IIdNguonVonId, model => model.MapFrom(item => item.IdNguonVon))
                .ForMember(entity => entity.FTienPheDuyet, model => model.MapFrom(item => item.GtQuyetToan));
        }
    }
}
