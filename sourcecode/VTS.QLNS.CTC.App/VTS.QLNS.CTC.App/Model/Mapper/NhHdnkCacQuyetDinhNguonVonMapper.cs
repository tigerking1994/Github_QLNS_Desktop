using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NhHdnkCacQuyetDinhNguonVonMapper : Profile
    {
        public NhHdnkCacQuyetDinhNguonVonMapper()
        {
            CreateMap<NhHdnkCacQuyetDinhNguonVon, NhHdnkCacQuyetDinhNguonVonModel>();
            CreateMap<NhHdnkCacQuyetDinhNguonVonModel, NhHdnkCacQuyetDinhNguonVon>();
            CreateMap<NhThongTinNGuonVonQuery, NhHdnkCacQuyetDinhNguonVonModel>();
            CreateMap<NhHdnkCacQuyetDinhNguonVonModel, NhThongTinNGuonVonQuery>();
        }
    }
}
