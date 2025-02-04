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
    public class BhQtcqCtctGtTroCapMapper : Profile
    {
        public BhQtcqCtctGtTroCapMapper()
        {
            CreateMap<BhQtcqCtctGtTroCapModel, BhQtcqCtctGtTroCap>().ReverseMap();
            CreateMap<BhQtcqCtctGtTroCapQuery, BhQtcqCtctGtTroCapModel>().ReverseMap();
            CreateMap<BhQtcqCtctGtTroCapModel, BhSalaryDataQuery>().ReverseMap();
        }
    }
}
