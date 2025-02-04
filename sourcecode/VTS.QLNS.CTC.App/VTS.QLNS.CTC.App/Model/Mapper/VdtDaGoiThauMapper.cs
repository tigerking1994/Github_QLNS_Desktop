using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class VdtDaGoiThauMapper : Profile
    {
        public VdtDaGoiThauMapper()
        {
            CreateMap<Core.Domain.VdtDaGoiThau, VdtDaGoiThauModel>();
            CreateMap<VdtKhlcNhaThauGoiThauDetailQuery, VdtKhlcNhaThauGoiThauDetailModel>();
            CreateMap<VdtKhlcNhaThauGoiThauDetailModel, VdtKhlcNhaThauGoiThauDetailQuery>();
            CreateMap<Core.Domain.Query.VdtDaGoiThauQuery, VdtDaGoiThauModel>();
            CreateMap<VdtDaGoiThauModel, Core.Domain.VdtDaGoiThau>();
            CreateMap<Core.Domain.Query.GoiThauNguonVonQuery, GoiThauNguonVonModel>();
            CreateMap<Core.Domain.Query.GoiThauChiPhiQuery, GoiThauChiPhiModel>();
            CreateMap<Core.Domain.Query.GoiThauHangMucQuery, GoiThauHangMucModel>();
            CreateMap<GoiThauModel, GoiThauQuery>();
            CreateMap<GoiThauQuery, GoiThauModel>();
            CreateMap<HopDongHangMucQuery, HopDongHangMucModel>();
            CreateMap<HopDongHangMucModel, HopDongHangMucQuery>();
            CreateMap<VdtKhlcntChiPhiNguonVonCanCuQuery, VdtKhlcntChiPhiNguonVonCanCuModel>();
            CreateMap<VdtKhlcntChiPhiNguonVonCanCuModel, VdtKhlcntChiPhiNguonVonCanCuQuery>();
            CreateMap<Core.Domain.Query.NhaThauHopDongQuery, NhaThauHopDongModel>();

            CreateMap<Core.Domain.VdtDaGoiThau, ComboboxItem>()
                .ForMember(x => x.DisplayItem, y => y.MapFrom(z => z.STenGoiThau))
                .ForMember(x => x.ValueItem, y => y.MapFrom(z => z.Id.ToString()))
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id));
        }
    }
}
