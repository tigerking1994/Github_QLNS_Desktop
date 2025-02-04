using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class ExportNgachCanBoMapper : Profile
    {
        public ExportNgachCanBoMapper()
        {
            CreateMap<ExportNgachCanBoModel, ExportGiaiThichChiTietBangLuongThangQuery>();
            CreateMap<ExportGiaiThichChiTietBangLuongThangQuery, ExportNgachCanBoModel>();
        }
    }
}
