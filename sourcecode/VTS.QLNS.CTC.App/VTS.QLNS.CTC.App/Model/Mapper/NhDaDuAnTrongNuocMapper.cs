using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NhDaDuAnTrongNuocMapper : Profile
    {
        public NhDaDuAnTrongNuocMapper()
        {
            CreateMap<NhDaDuAnTrongNuocModel, NhDaDuAnTrongNuocQuery>();
            CreateMap<NhDaDuAnTrongNuocQuery, NhDaDuAnTrongNuocModel>();
        }
    }
}
