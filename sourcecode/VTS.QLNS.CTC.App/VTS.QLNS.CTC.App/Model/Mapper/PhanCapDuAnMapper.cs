using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class PhanCapDuAnMapper: Profile
    {
        public PhanCapDuAnMapper()
        {
            CreateMap<Core.Domain.VdtDmPhanCapDuAn, ComboboxItem>()
                .ForMember(x => x.DisplayItem, y => y.MapFrom(z => z.STen))
                .ForMember(x => x.ValueItem, y => y.MapFrom(z => z.IIdPhanCapId));
        }
    }
}
