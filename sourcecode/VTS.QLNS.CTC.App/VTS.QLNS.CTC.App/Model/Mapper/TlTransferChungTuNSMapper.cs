using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TlTransferChungTuNSMapper : Profile
    {
        public TlTransferChungTuNSMapper()
        {
            CreateMap<TlQsChungTuModel, NsQsChungTu>()
                .ForMember(x => x.SSoChungTu, y => y.MapFrom(z => z.SoChungTu))
                .ForMember(x => x.ISoChungTuIndex, y => y.MapFrom(z => int.Parse(z.SoChungTu.Substring(3, z.SoChungTu.Length - 3))))
                .ForMember(x => x.DNgayTao, y => y.MapFrom(z => z.NgayTao))
                .ForMember(x => x.DNgayQuyetDinh, y => y.MapFrom(z => z.NgayTao))
                .ForMember(x => x.SMoTa, y => y.MapFrom(z => z.MoTa))
                .ForMember(x => x.IIdMaDonVi, y => y.MapFrom(z => z.MaDonVi))
                .ForMember(x => x.INamLamViec, y => y.MapFrom(z => z.Nam))
                .ForMember(x => x.SNguoiTao, y => y.MapFrom(z => z.UserCreated))
                .ForMember(x => x.IThangQuy, y => y.MapFrom(z => z.Thang))
                .ForMember(x => x.BKhoa, y => y.MapFrom(z => z.IsLock));
        }
    }
}
