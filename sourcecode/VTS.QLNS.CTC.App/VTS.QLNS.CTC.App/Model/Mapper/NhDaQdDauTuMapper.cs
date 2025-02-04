using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NhDaQdDauTuMapper : Profile
    {
        public NhDaQdDauTuMapper()
        {
            CreateMap<NhDaQdDauTu, NhDaQdDauTuModel>();
            CreateMap<NhDaQdDauTuQuery, NhDaQdDauTuModel>();
            CreateMap<NhDaQdDauTuModel, NhDaQdDauTu>();
            CreateMap<NHDAQDDauTuChiPhiHangMuc, NHDAQDDauTuChiPhiHangMucModel>();
            CreateMap<NhDaDuAnImport, NhDaQdDauTu>()
               .ForMember(entity => entity.ILoai, model => model.MapFrom(item => item.IThuocMenu))
               .ForMember(entity => entity.SSoQuyetDinh, model => model.MapFrom(item => item.SSoQuyetDinh))
               .ForMember(entity => entity.DNgayQuyetDinh, model => model.MapFrom(item => item.DNgayPheDuyet))
               .ForMember(entity => entity.SMoTa, model => model.MapFrom(item => item.STenDuAn))
               .ForMember(entity => entity.IIdDuAnId, model => model.MapFrom(item => item.IIdDuAnId));
        }
    }
}
