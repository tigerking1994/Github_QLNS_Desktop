using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class VoucherListMapper : Profile
    {
        public VoucherListMapper()
        {
            CreateMap<NsBkChungTu, VoucherListModel>();
            CreateMap<VoucherListModel, NsBkChungTu>();

            CreateMap<NsBkChungTuChiTiet, VoucherListDetailModel>()
                .ForMember(x => x.IsModified, y => y.Ignore());
            CreateMap<VoucherListDetailModel, NsBkChungTuChiTiet>()
                .ForMember(x => x.IsModified, y => y.Ignore()); ;
            CreateMap<NsBkChungTuChiTietQuery, VoucherListDetailModel>().ReverseMap();
        }
    }
}
