using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NhDaHopDongHangMucMapper : Profile 
    {
        public NhDaHopDongHangMucMapper()
        {
            CreateMap<NhDaGoiThauHangMucQuery, NhDaHopDongTrongNuocHangMucGoiThauModel>()
                .ForMember(entity => entity.IIdGoiThauHangMucId, model => model.MapFrom(item => item.Id))
                .ForMember(entity => entity.STenHangMuc, model => model.MapFrom(item => item.STenHangMucDT != null ? item.STenHangMucDT : item.STenHangMucQDDT))
                .ForMember(entity => entity.FTienGoiThauUSD, model => model.MapFrom(item => item.FTienGoiThauUsd))
                .ForMember(entity => entity.FTienGoiThauVND, model => model.MapFrom(item => item.FTienGoiThauVnd))
                .ForMember(entity => entity.FTienGoiThauEUR, model => model.MapFrom(item => item.FTienGoiThauEur))
                .ForMember(entity => entity.FTienGoiThauNgoaiTeKhac, model => model.MapFrom(item => item.FTienGoiThauNgoaiTeKhac));

            CreateMap<NhDaHopDongTrongNuocHangMucGoiThauModel, NhDaHopDongHangMuc>();

            CreateMap<NhMSTNKeHoachDatHangDanhMuc, NhDaHopDongHangMuc>()
                .ForMember(entity => entity.STenHangMuc, model => model.MapFrom(item => item.STenDanhMuc))
                .ForMember(entity => entity.SMaOrder, model => model.MapFrom(item => item.SMaOrder))
                .ForMember(entity => entity.SMaHangMuc, model => model.MapFrom(item => StringUtils.ConvertMaOrder(item.SMaOrder)))
                .ForMember(entity => entity.IIdKeHoachDatHangDanhMucId, model => model.MapFrom(item => item.Id))
                .ForMember(entity => entity.IIdHopDongNhaThauId, model => model.MapFrom(item => item.IID_NhaThauID))
                .ForMember(entity => entity.SDonViTinh, model => model.MapFrom(item => item.SDonViTinh))
                .ForMember(entity => entity.ISoLuong, model => model.MapFrom(item => item.ISoLuong))
                .ForMember(entity => entity.FDonGia, model => model.MapFrom(item => item.FDonGia_VND))
                .ForMember(entity => entity.FGiaTriUsd, model => model.MapFrom(item => item.FGiaTriUsd))
                .ForMember(entity => entity.FGiaTriVnd, model => model.MapFrom(item => item.FGiaTriVnd))
                .ForMember(entity => entity.FGiaTriEur, model => model.MapFrom(item => item.FGiaTriEur))
                .ForMember(entity => entity.FGiaTriNgoaiTeKhac, model => model.MapFrom(item => item.FGiaTriNgoaiTeKhac))
                .ForMember(entity => entity.SGhiChu, model => model.MapFrom(item => item.SGhiChu));
            CreateMap<NhDaGoiThauChiPhiHangMucQuery, NhDaHopDongTrongNuocHangMucGoiThauModel>()
                .ForMember(entity => entity.STenHangMuc, model => model.MapFrom(item => item.STenHangMucDT != null ? item.STenHangMucDT : item.STenHangMucQDDT))
                .ForMember(entity => entity.FTienGoiThauUSD, model => model.MapFrom(item => item.FTienGoiThauUsd))
                .ForMember(entity => entity.FTienGoiThauVND, model => model.MapFrom(item => item.FTienGoiThauVnd))
                .ForMember(entity => entity.FTienGoiThauEUR, model => model.MapFrom(item => item.FTienGoiThauEur))
                .ForMember(entity => entity.FTienGoiThauNgoaiTeKhac, model => model.MapFrom(item => item.FTienGoiThauNgoaiTeKhac))
                .ForMember(entity => entity.STenChiPhiDT, model => model.MapFrom(item => item.STenChiPhi));
        }
    }
}
