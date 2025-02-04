using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class QDDauTuChiPhiNguonVonDetailMapper : Profile
    {
        public QDDauTuChiPhiNguonVonDetailMapper()
        {
            CreateMap<Core.Domain.Query.QDDauTuChiPhiNguonVonDetailQuery, QDDauTuChiPhiNguonVonDetailModel>();
        }
    }
}
