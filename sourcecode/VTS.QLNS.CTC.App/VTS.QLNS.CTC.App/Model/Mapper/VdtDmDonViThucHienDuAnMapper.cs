using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class VdtDmDonViThucHienDuAnMapper : Profile
    {
        public VdtDmDonViThucHienDuAnMapper()
        {
            CreateMap<VdtDmDonViThucHienDuAn, VdtDmDonViThucHienDuAnModel>();
            CreateMap<VdtDmDonViThucHienDuAnModel, VdtDmDonViThucHienDuAn>();
            CreateMap<VdtDmDonViThucHienDuAn, CheckBoxItem>()
                .ForMember(n => n.DisplayItem, m => m.MapFrom(n => n.STenDonVi))
                .ForMember(n => n.ValueItem, m => m.MapFrom(n => n.IIdMaDonVi));
            CreateMap<VdtDmDonViThucHienDuAn, ComboboxItem>()
                .ForMember(n => n.DisplayItem, m => m.MapFrom(n => string.Format("{0} - {1}", n.IIdMaDonVi, n.STenDonVi)))
                .ForMember(n => n.ValueItem, m => m.MapFrom(n => n.IIdMaDonVi));
        }
    }
}
