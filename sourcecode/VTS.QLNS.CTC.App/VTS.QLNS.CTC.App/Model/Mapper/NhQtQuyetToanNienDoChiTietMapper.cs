using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NhQtQuyetToanNienDoChiTietMapper : Profile
    {
        public NhQtQuyetToanNienDoChiTietMapper()
        {
            CreateMap<NhQtQuyetToanNienDoChiTietModel, NhQtQuyetToanNienDoChiTiet>().ReverseMap();
            CreateMap<NhQtQuyetToanNienDoChiTietModel, NhQTQuyetToanNienDoChiTietQuery>().ReverseMap();
        }
    }
}
