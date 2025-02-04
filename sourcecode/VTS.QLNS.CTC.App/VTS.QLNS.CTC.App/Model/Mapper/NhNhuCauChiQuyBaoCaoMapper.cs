using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NhNhuCauChiQuyBaoCaoMapper : Profile
    {
        public NhNhuCauChiQuyBaoCaoMapper ()
        {
            CreateMap<NhNhuCauChiQuyBaoCaoQuery, NhNhuCauChiQuyBaoCaoModel>();
            CreateMap<NhNhuCauChiQuyBaoCaoModel, NhNhuCauChiQuyBaoCaoQuery>();
        }
    }
}
