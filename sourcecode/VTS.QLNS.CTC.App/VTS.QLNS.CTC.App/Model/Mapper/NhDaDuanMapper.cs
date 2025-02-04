using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.Model.Report;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NhDaDuAnMapper : Profile
    {
        public NhDaDuAnMapper()
        {
            CreateMap<NhDaDuAn, NhDaDuAnModel>();
            CreateMap<NhDaDuAnQuery, NhDaDuAnModel>();
            CreateMap<NhDaDuAnModel, NhDaDuAn>();
            CreateMap<NhBaoCaoTinhHinhThucHienDuAnQuery, NhBaoCaoTinhHinhThucHienDuAnModel>();
            CreateMap<ReportNHTongHopThongTinDuAnQuery, RptNHTongHopThongTinDuAn>();
            CreateMap<NhDaDuAnImportModel, NhDaDuAnModel>().ReverseMap();
            CreateMap<NhDaDuAnExportCTCQuery, NhDaThongTinDuAnCTCExportModel>();
            CreateMap<NhDaDuAnImport, NhDaDuAn>()
                .ForMember(entity => entity.ILoai, model => model.MapFrom(item => item.IThuocMenu))
                .ForMember(entity => entity.STenDuAn, model => model.MapFrom(item => item.STenDuAn))
                .ForMember(entity => entity.Id, model => model.MapFrom(item => item.IIdDuAnId));
        }
    }
}
