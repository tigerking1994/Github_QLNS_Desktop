using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    class ExportTongHopLuongNgachCbMapper : Profile
    {
        public ExportTongHopLuongNgachCbMapper()
        {
            CreateMap<ExportNgachCanBoModel, Core.Domain.Query.ExportLuongNgachCanBoQuery>();
            CreateMap<Core.Domain.Query.ExportLuongNgachCanBoQuery, ExportNgachCanBoModel>();
            CreateMap<ExportNgachCanBoModel, Core.Domain.Query.ExportLuongNgachCanBoNq104Query>();
            CreateMap<Core.Domain.Query.ExportLuongNgachCanBoNq104Query, ExportNgachCanBoModel>();
        }
    }
}
