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
    public class NhDaGoiThauTrongNuocMapper : Profile 
    {
        public NhDaGoiThauTrongNuocMapper()
        {
            CreateMap<NhDaGoiThauTrongNuocModel, NhDaGoiThau>();
            CreateMap<NhDaGoiThauTrongNuocQuery, NhDaGoiThauTrongNuocModel>()
                .ForMember(entity => entity.DNgayQuyetDinhString, model => model.MapFrom(item => item.DNgayQuyetDinh.HasValue ? item.DNgayQuyetDinh.Value.ToString("dd/MM/yyyy") : string.Empty));
        }
    }
}
