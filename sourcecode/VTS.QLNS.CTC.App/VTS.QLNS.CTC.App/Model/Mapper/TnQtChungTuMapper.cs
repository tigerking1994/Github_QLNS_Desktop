using AutoMapper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TnQtChungTuMapper : Profile
    {
        public TnQtChungTuMapper()
        {
            CreateMap<TnQtChungTu, TnQtChungTuModel>();
            CreateMap<TnQtChungTuModel, TnQtChungTu>();
            CreateMap<RealRevenueExpenditureImportModel, TnQtChungTu>()
                .ForMember(x => x.NgayChungTu, y => y.MapFrom(z => DateTime.Parse(z.NgayChungTu, CultureInfo.CreateSpecificCulture("vi-VN"))))
                .ForMember(x => x.TongSoThuSum, y => y.MapFrom(z => double.Parse(z.TongSoThu)))
                .ForMember(x => x.TongSoChiPhiSum, y => y.MapFrom(z => double.Parse(z.TongSoChiPhi)))
                .ForMember(x => x.NgayQuyetDinh, y => y.MapFrom(z => DateTime.Parse(z.NgayQuyetDinh, CultureInfo.CreateSpecificCulture("vi-VN"))))
                .ForMember(x => x.MoTaChiTiet, y => y.MapFrom(z => z.MoTaChiTiet))
                .ForMember(x => x.Lns, y => y.MapFrom(z => z.Lns));
        }
    }
}
