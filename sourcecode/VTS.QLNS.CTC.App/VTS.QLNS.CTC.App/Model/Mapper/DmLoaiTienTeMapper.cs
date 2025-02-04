using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class DmLoaiTienTeMapper : Profile
    {
        public DmLoaiTienTeMapper()
        {
            CreateMap<NhDmLoaiTienTe, DmLoaiTienTeModel>();
            CreateMap<DmLoaiTienTeModel, NhDmLoaiTienTe>();
        }
    }
}
