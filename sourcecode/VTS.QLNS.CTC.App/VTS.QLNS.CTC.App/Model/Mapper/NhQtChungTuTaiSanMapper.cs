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
    public class NhQtChungTuTaiSanMapper:Profile
    {
        public NhQtChungTuTaiSanMapper()
        {
            CreateMap<NhQtChungTuTaiSan, NhQtChungTuTaiSanModel>();
            CreateMap<NhQtChungTuTaiSanModel, NhQtChungTuTaiSanQuery>();
            CreateMap<NhQtChungTuTaiSanQuery, NhQtChungTuTaiSan>();
        }
    }
}
