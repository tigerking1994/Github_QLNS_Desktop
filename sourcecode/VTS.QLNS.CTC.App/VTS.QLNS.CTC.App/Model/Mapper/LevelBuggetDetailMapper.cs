using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class LevelBuggetDetailMapper : Profile
    {
        public LevelBuggetDetailMapper()
        {
            CreateMap<LbChungTuChiTietQuery, LevelBuggetDetailModel>()
                .ForMember(entity => entity.IsHangCha, model => model.MapFrom(item => item.BHangCha));
            CreateMap<LevelBuggetDetailModel, Core.Domain.NsNganhChungTuChiTiet>()
               .ForMember(entity => entity.Id, model => model.MapFrom(item => item.Id))
               .ForMember(entity => entity.IIdCtnganh, model => model.MapFrom(item => item.IdChungTu))
               .ForMember(entity => entity.IIdMlns, model => model.MapFrom(item => item.MlnsId))
               .ForMember(entity => entity.IIdParentMlns, model => model.MapFrom(item => item.MlnsIdParent))
               .ForMember(entity => entity.SXauNoiMa, model => model.MapFrom(item => item.XauNoiMa))
               .ForMember(entity => entity.SLns, model => model.MapFrom(item => item.Lns))
               .ForMember(entity => entity.SL, model => model.MapFrom(item => item.L))
               .ForMember(entity => entity.SK, model => model.MapFrom(item => item.K))
               .ForMember(entity => entity.SM, model => model.MapFrom(item => item.M))
               .ForMember(entity => entity.STm, model => model.MapFrom(item => item.Tm))
               .ForMember(entity => entity.SNg, model => model.MapFrom(item => item.Ng))
               .ForMember(entity => entity.STng, model => model.MapFrom(item => item.Tng))
               .ForMember(entity => entity.SMoTa, model => model.MapFrom(item => item.MoTa))
               .ForMember(entity => entity.SChuong, model => model.MapFrom(item => item.Chuong))
               .ForMember(entity => entity.INamLamViec, model => model.MapFrom(item => item.NamLamViec))
               .ForMember(entity => entity.IIdMaDonVi, model => model.MapFrom(item => item.IdDonVi))
               .ForMember(entity => entity.STenDonVi, model => model.MapFrom(item => item.TenDonVi))
               .ForMember(entity => entity.FTuChi, model => model.MapFrom(item => item.TuChi))
               .ForMember(entity => entity.FPhanCap, model => model.MapFrom(item => item.PhanCap))
               .ForMember(entity => entity.SGhiChu, model => model.MapFrom(item => item.GhiChu))
               .ForMember(entity => entity.DNgayTao, model => model.MapFrom(item => item.DateCreated))
               .ForMember(entity => entity.SNguoiTao, model => model.MapFrom(item => item.UserCreator))
               .ForMember(entity => entity.DNgaySua, model => model.MapFrom(item => item.DateModified))
               .ForMember(entity => entity.SNguoiSua, model => model.MapFrom(item => item.UserModifier))
               .ForMember(entity => entity.DNgaySua, model => model.MapFrom(item => item.DateModified))
               .ForMember(entity => entity.IIdMaNguonNganSach, model => model.MapFrom(item => item.NguonNganSach))
               .ForMember(entity => entity.FChuaPhanCap, model => model.MapFrom(item => item.ChuaPhanCap))
               .ForMember(entity => entity.FHangNhap, model => model.MapFrom(item => item.HangNhap))
               .ForMember(entity => entity.FHangMua, model => model.MapFrom(item => item.HangMua))
               .ForMember(entity => entity.INamNganSach, model => model.MapFrom(item => item.NamNganSach))
               .ForMember(entity => entity.BHangCha, model => model.MapFrom(item => item.IsHangCha));
        }
    }
}
