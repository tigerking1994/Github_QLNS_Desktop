using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NhHdnkCacQuyetDinhChiPhiMapper: Profile
    {
        public NhHdnkCacQuyetDinhChiPhiMapper()
        {
            // chi phi
            CreateMap<NhHdnkCacQuyetDinhChiPhi, NhHdnkCacQuyetDinhChiPhiModel>();
            CreateMap<NhHdnkCacQuyetDinhChiPhiModel, NhHdnkCacQuyetDinhChiPhi>();
            CreateMap<NhHdnkCacQuyetDinhChiPhiQuery, NhHdnkCacQuyetDinhChiPhi>();
            // chi phi hang muc
            CreateMap<NhHdnkCacQuyetDinhChiPhiHangMuc, NhHdnkCacQuyetDinhChiPhiHangMucModel>();
            CreateMap<NhHdnkCacQuyetDinhChiPhiHangMucModel, NhHdnkCacQuyetDinhChiPhiHangMuc>();
            CreateMap<NhHdnkCacQuyetDinhChiPhiDmChiPhiQuery, NhHdnkCacQuyetDinhChiPhiModel>()
                .ForMember(entity => entity.STenChiPhi, model => model.MapFrom(item => item.STenChiPhi != null ? item.STenChiPhi : item.STenDanhMucChiPhi));
        }
    }
}
