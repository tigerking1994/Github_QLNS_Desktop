using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Service.Impl;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class ExportResultMapper : Profile
    {
        public ExportResultMapper()
        {
            CreateMap<ExportResult, PdfFileModel>()
                .ForMember(entity => entity.FilePath, model => model.MapFrom(item => item.FileName));
        }
    }
}
