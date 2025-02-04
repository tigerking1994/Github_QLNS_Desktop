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
    public class NhDaQuyetDinhKhacMapper : Profile
    {
        public NhDaQuyetDinhKhacMapper()
        {
            CreateMap<NhDaQuyetDinhKhac, NhDaQuyetDinhKhacModel>().ReverseMap();
            CreateMap<NhDaQuyetDinhKhacQuery, NhDaQuyetDinhKhacModel>().ReverseMap();
            CreateMap<NhDaQuyetDinhKhacChiPhi, NhDaQuyetDinhKhacChiPhiModel>().ReverseMap();
            CreateMap<NhDaQuyetDinhKhacImport, NhDaQuyetDinhKhac>()
                .ForMember(entity => entity.Id, model => model.MapFrom(y => y.IIdQuyetDinhKhacId))
                .ForMember(entity => entity.SSoQuyetDinh, model => model.MapFrom(y => y.SSoQuyetDinh))
                .ForMember(entity => entity.STenQuyetDinh, model => model.MapFrom(y => y.STenQuyetDinh))
                .ForMember(entity => entity.DNgayQuyetDinh, model => model.MapFrom(y => y.DNgayQuyetDinh))
                .ForMember(entity => entity.IThuocMenu, model => model.MapFrom(y => y.IThuocMenu));
        }
    }
}
