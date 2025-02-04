using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Import;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class QSMucLucMapper : Profile
    {
        public QSMucLucMapper()
        {
            CreateMap<Core.Domain.NsQsMucLuc, QsMucLucModel>();

            CreateMap<QsMucLucModel, Core.Domain.NsQsMucLuc>();
            CreateMap<DanhMucMLQSImportModel, Core.Domain.NsQsMucLuc>()
                //.ForMember(x => x.Id, y => y.MapFrom(z => string.IsNullOrEmpty(z.Id) ? Guid.Empty : Guid.Parse(z.Id)))
                .ForMember(x => x.Id, y => y.MapFrom(z => Guid.NewGuid()))
                .ForMember(x => x.INamLamViec, y => y.MapFrom(z => int.Parse(z.NamLamViec)))
                .ForMember(x => x.SM, y => y.MapFrom(z => z.M))
                .ForMember(x => x.STm, y => y.MapFrom(z => z.TM))
                .ForMember(x => x.SKyHieu, y => y.MapFrom(z => z.XNM))
                .ForMember(x => x.SMoTa, y => y.MapFrom(z => z.MoTa))
                .ForMember(x => x.SHienThi, y => y.MapFrom(z => z.HienThi));
        }
    }
}
