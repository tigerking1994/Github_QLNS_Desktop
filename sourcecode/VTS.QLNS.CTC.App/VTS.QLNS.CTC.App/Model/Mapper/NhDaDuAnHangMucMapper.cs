using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NhDaDuAnHangMucMapper : Profile
    {
        public NhDaDuAnHangMucMapper()
        {
            CreateMap<NhDaDuAnHangMuc, NhDaDuAnHangMucModel>();
            CreateMap<NhDaDuAnHangMucModel, NhDaDuAnHangMuc>();
            CreateMap<NhDaDuAnHangMuc, NhDaChuTruongDauTuHangMucModel>();
            CreateMap<NhDaDuAnHangMucImportModel, NhDaDuAnHangMucModel>().ReverseMap();

        }
    }
}
