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
    public class BhQtcQuyKinhPhiQuanLyChiTietMapper : Profile
    {
        public BhQtcQuyKinhPhiQuanLyChiTietMapper()
        {
            CreateMap<BhQtcQuyKinhPhiQuanLyChiTietModel, BhQtcQuyKinhPhiQuanLyChiTiet>().ReverseMap();
            CreateMap<BhQtcQuyKinhPhiQuanLyChiTietModel, BhQtcQuyKinhPhiQuanLyChiTietQuery>().ReverseMap();
            CreateMap<BhQtcQuyKinhPhiQuanLyChiTietModel, ReportBHQTCQKPQuanLyThongTriQuery>().ReverseMap();
            CreateMap<ReportBHQTCQKPQuanLyThongTriQuery, BhQtcQuyKinhPhiQuanLyChiTietModel>().ReverseMap();
        }
    }
}
