using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain;
using AutoMapper;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class BhDcDuToanThuChiTietMapper : Profile
    {
        public BhDcDuToanThuChiTietMapper()
        {
            CreateMap<BhDcDuToanThuChiTiet, BhDcDuToanThuChiTietModel>().ReverseMap();
            CreateMap<BhDcDuToanThuChiTiet, BhDcDuToanThuChiTietQuery>().ReverseMap();
            CreateMap<BhDcDuToanThuChiTietModel, BhDcDuToanThuChiTietQuery>().ReverseMap();
        }
    }
}
