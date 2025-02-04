using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TnDtdnChungTuChiTietMapper : Profile
    {
        public TnDtdnChungTuChiTietMapper()
        {
            CreateMap<TnDtdnChungTuChiTiet, TnDtdnChungTuChiTietModel>().ReverseMap();
            CreateMap<TnDtdnChungTuChiTietModel, TnDtdnChungTuChiTietQuery>().ReverseMap();
            CreateMap<TnDtdnChungTuChiTietImportModel, TnDtdnChungTuChiTiet>()
                .ForMember(s => s.FDuToanNamKeHoach, d => d.MapFrom(m => NumberUtils.ConvertTextToDouble(m.FDuToanNamKeHoach)))
                .ForMember(s => s.FDuToanNamNay, d => d.MapFrom(m => NumberUtils.ConvertTextToDouble(m.FDuToanNamNay)))
                .ForMember(s => s.FThucThuNamTruoc, d => d.MapFrom(m => NumberUtils.ConvertTextToDouble(m.FThucThuNamTruoc)))
                .ForMember(s => s.FUocThucHienNamNay, d => d.MapFrom(m => NumberUtils.ConvertTextToDouble(m.FUocThucHienNamNay)));

        }
    }
}
