using AutoMapper;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TlBangLuongThangMapper : Profile
    {
        public TlBangLuongThangMapper()
        {
            CreateMap<TlBangLuongThangQuery, TlBangLuongThang>();
            CreateMap<TlBangLuongThang, TlBangLuongThangModel>();
            CreateMap<TlBangLuongThangModel, TlBangLuongThang>();
            CreateMap<TlBangLuongThangQuery, TlBangLuongThangTruyThu>().ReverseMap();
            CreateMap<TlBangLuongThang, TlBangLuongThangTruyThu>().ReverseMap();
            CreateMap<RptQuyetToanNamThueTNCNQuery, RptQuyetToanNamThueTncnModel>()
                .ForMember(entity => entity.sTenCbo, model => model.MapFrom(item => item.TenCbo))
                .ForMember(entity => entity.fLuong, model => model.MapFrom(item => item.LhtTt + item.PcctTt))
                .ForMember(entity => entity.fThuongTt, model => model.MapFrom(item => item.ThuongTt))
                .ForMember(entity => entity.fThuNhapKhacTt, model => model.MapFrom(item => item.ThuNhapKhacTt))
                .ForMember(entity => entity.fTongCong, model => model.MapFrom(item => item.LhtTt + item.PcctTt + item.ThuongTt + item.ThuNhapKhacTt))
                .ForMember(entity => entity.fBhcnTt, model => model.MapFrom(item => item.BhcnTt))
                .ForMember(entity => entity.fGtnn, model => model.MapFrom(item => item.Gtnn))
                .ForMember(entity => entity.fNguoiPhuThuoc, model => model.MapFrom(item => item.NguoiPhuThuoc))
                .ForMember(entity => entity.fGtkhacTt, model => model.MapFrom(item => item.GtkhacTt))
                .ForMember(entity => entity.fLuongThueTt, model => model.MapFrom(item => item.LuongthueTt))
                .ForMember(entity => entity.fThueTncnTt, model => model.MapFrom(item => item.ThuetncnTt))
                .ForMember(entity => entity.fGiamThueTt, model => model.MapFrom(item => item.GiamThueTt))
                .ForMember(entity => entity.fPhaiNopThue, model => model.MapFrom(item => item.ThuetncnTt - item.GiamThueTt))
                .ForMember(entity => entity.fThueDaNopTt, model => model.MapFrom(item => item.ThueDaNopTt))
                .ForMember(entity => entity.fThueConPhaiNop, model => model.MapFrom(item => item.ThuetncnTt - item.GiamThueTt - item.ThueDaNopTt));
            CreateMap<TlRptTruyLinhChuyenCheDoModel, TlRptTruyLinhChuyenCheDoQuery>();
            CreateMap<TlRptTruyLinhChuyenCheDoQuery, TlRptTruyLinhChuyenCheDoModel>();
            CreateMap<TlBangLuongThangQuery, TlQuanLyThuNopBhxhChiTiet>()
                     .ForMember(entity => entity.IIdMaDonVi, model => model.MapFrom(item => item.MaDonVi))
                     .ForMember(entity => entity.SMaHieuCanBo, model => model.MapFrom(item => item.MaHieuCanBo))
                     .ForMember(entity => entity.SMaCb, model => model.MapFrom(item => item.MaCb))
                     .ForMember(entity => entity.SMaCbo, model => model.MapFrom(item => item.MaCbo))
                     .ForMember(entity => entity.SMaPhuCap, model => model.MapFrom(item => item.MaPhuCap))
                     .ForMember(entity => entity.SMaCachTl, model => model.MapFrom(item => item.MaCachTl))
                     .ForMember(entity => entity.STenCachTl, model => model.MapFrom(item => item.TenCachTl))
                     .ForMember(entity => entity.DNgayHt, model => model.MapFrom(item => item.NgayHt))
                     .ForMember(entity => entity.INam, model => model.MapFrom(item => item.Nam))
                     .ForMember(entity => entity.IThang, model => model.MapFrom(item => item.Thang))
                     .ForMember(entity => entity.ILoai, model => model.MapFrom(item => item.LoaiBl))
                     .ForMember(entity => entity.IIdParentId, model => model.MapFrom(item => item.Parent))
                     .ForMember(entity => entity.ISoTt, model => model.MapFrom(item => item.SoTt))
                     .ForMember(entity => entity.SUserName, model => model.MapFrom(item => item.UserName))
                     .ForMember(entity => entity.STenCbo, model => model.MapFrom(item => item.TenCbo))
;

        }
    }
}
