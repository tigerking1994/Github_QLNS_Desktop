using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TlCanBoPhuCapKeHoachBridgeNq104Mapper : Profile
    {
        public TlCanBoPhuCapKeHoachBridgeNq104Mapper()
        {
            CreateMap<TlCanBoPhuCapKeHoachBridgeNq104, AllowencePhuCapNq104Criteria>();
        }
    }
}
