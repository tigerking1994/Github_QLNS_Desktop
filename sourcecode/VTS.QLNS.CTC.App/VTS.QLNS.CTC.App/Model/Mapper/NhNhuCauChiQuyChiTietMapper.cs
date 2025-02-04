using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NhNhuCauChiQuyChiTietMapper : Profile
    {
        public NhNhuCauChiQuyChiTietMapper()
        {
            CreateMap<NhNhuCauChiQuyChiTietModel, NhNhuCauChiQuyChiTiet>();
            CreateMap<NhNhuCauChiQuyChiTiet, NhNhuCauChiQuyChiTietModel>();
            CreateMap<NhuCauChiQuyNhiemVuChiQuery, ComboboxItem>()
                .ForMember(x => x.DisplayItem, y => y.MapFrom(z => z.STenNhiemVuChi))
                .ForMember(x => x.ValueItem, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.HiddenValue, y => y.MapFrom(z => z.IIdNhiemVuChiId));
        }
    }
}
