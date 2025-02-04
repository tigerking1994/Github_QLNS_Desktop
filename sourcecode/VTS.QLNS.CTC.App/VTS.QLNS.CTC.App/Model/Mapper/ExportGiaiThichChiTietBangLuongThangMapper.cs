using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class ExportGiaiThichChiTietBangLuongThangMapper : Profile
    {
        public ExportGiaiThichChiTietBangLuongThangMapper()
        {
            CreateMap<ExportLuongCapBacGiaiThichChiTietLuongQuery, ExportGiaiThichChiTietBangLuongThangModel>();
            CreateMap<ExportGiaiThichChiTietBangLuongThangModel, ExportLuongCapBacGiaiThichChiTietLuongQuery>();
            CreateMap<ExportLuongCapBacGiaiThichChiTietLuongNq104Query, ExportGiaiThichChiTietBangLuongThangModel>();
            CreateMap<ExportGiaiThichChiTietBangLuongThangModel, ExportLuongCapBacGiaiThichChiTietLuongNq104Query>();
        }
    }
}
