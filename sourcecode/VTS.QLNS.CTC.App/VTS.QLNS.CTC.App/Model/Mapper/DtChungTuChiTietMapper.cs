using AutoMapper;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class DtChungTuChiTietMapper : Profile
    {
        public DtChungTuChiTietMapper()
        {
            CreateMap<NsDtChungTuChiTiet, DtChungTuChiTietModel>()
                .ForMember(entity => entity.IsHangCha, model => model.MapFrom(z => z.BHangCha));
            CreateMap<DtChungTuChiTietModel, NsDtChungTuChiTiet>()
                .ForMember(entity => entity.BHangCha, model => model.MapFrom(z => z.IsHangCha));
            CreateMap<DuToanDonViQuery, DtChungTuChiTietModel>()
                .ForMember(x => x.FTuChi, y => y.MapFrom(z => z.TuChi))
                .ForMember(x => x.FHienVat, y => y.MapFrom(z => z.HienVat));
            CreateMap<DivisionDetailImportModel, NsDtChungTuChiTiet>()
                .ForMember(x => x.SLns, y => y.MapFrom(z => z.LNS))
                .ForMember(x => x.SL, y => y.MapFrom(z => z.L))
                .ForMember(x => x.SK, y => y.MapFrom(z => z.K))
                .ForMember(x => x.SM, y => y.MapFrom(z => z.M))
                .ForMember(x => x.STm, y => y.MapFrom(z => z.TM))
                .ForMember(x => x.STtm, y => y.MapFrom(z => z.TTM))
                .ForMember(x => x.SNg, y => y.MapFrom(z => z.NG))
                .ForMember(x => x.STng, y => y.MapFrom(z => z.TNG))
                .ForMember(x => x.STng1, y => y.MapFrom(z => z.TNG1))
                .ForMember(x => x.STng2, y => y.MapFrom(z => z.TNG2))
                .ForMember(x => x.STng3, y => y.MapFrom(z => z.TNG3))
                .ForMember(x => x.SXauNoiMa, y => y.MapFrom(z => z.XauNoiMa))
                .ForMember(x => x.FTuChi, y => y.MapFrom(z => double.Parse(z.TuChi)))
                .ForMember(x => x.FHienVat, y => y.MapFrom(z => double.Parse(z.HienVat)))
                .ForMember(x => x.FHangNhap, y => y.MapFrom(z => double.Parse(z.HangNhap)))
                .ForMember(x => x.FHangMua, y => y.MapFrom(z => double.Parse(z.HangMua)))
                .ForMember(x => x.FPhanCap, y => y.MapFrom(z => double.Parse(z.PhanCap)))
                .ForMember(x => x.FDuPhong, y => y.MapFrom(z => double.Parse(z.DuPhong)))
                .ForMember(x => x.FRutKBNN, y => y.MapFrom(z => double.Parse(z.RutKBNN)))
                .ForMember(x => x.FTonKho, y => y.MapFrom(z => double.Parse(z.TonKho)));
            CreateMap<DivisionDetailImportModel, ImpDuToan>()
                .ForMember(x => x.Lns, y => y.MapFrom(z => z.LNS))
                .ForMember(x => x.L, y => y.MapFrom(z => z.L))
                .ForMember(x => x.K, y => y.MapFrom(z => z.K))
                .ForMember(x => x.M, y => y.MapFrom(z => z.M))
                .ForMember(x => x.Tm, y => y.MapFrom(z => z.TM))
                .ForMember(x => x.Ttm, y => y.MapFrom(z => z.TTM))
                .ForMember(x => x.Ng, y => y.MapFrom(z => z.NG))
                .ForMember(x => x.Tng, y => y.MapFrom(z => z.TNG))
                .ForMember(x => x.XauNoiMa, y => y.MapFrom(z => z.XauNoiMa))
                .ForMember(x => x.MoTa, y => y.MapFrom(z => z.MoTa))
                .ForMember(x => x.TuChi, y => y.MapFrom(z => double.Parse(z.TuChi)))
                .ForMember(x => x.HienVat, y => y.MapFrom(z => double.Parse(z.HienVat)))
                .ForMember(x => x.HangNhap, y => y.MapFrom(z => double.Parse(z.HangNhap)))
                .ForMember(x => x.HangMua, y => y.MapFrom(z => double.Parse(z.HangMua)))
                .ForMember(x => x.RutKBNN, y => y.MapFrom(z => double.Parse(z.RutKBNN)))
                .ForMember(x => x.PhanCap, y => y.MapFrom(z => double.Parse(z.PhanCap)));
            CreateMap<NsDtChungTuChiTietQuery, DtChungTuChiTietModel>()
                .ForMember(entity => entity.IsHangCha, model => model.MapFrom(z => z.BHangChaDuToan));
            CreateMap<NsDuToanChungTuChiTietQuery, NsDtChungTuChiTiet>();
            CreateMap<NsDtChungTuChiTiet, NsDuToanChungTuChiTietQuery>();
            // theo chuẩn thì dữ liệu mục cha sẽ cấu hình theo BHangChaDuToan dựa vào
            // cấu hình dự toán chi tiết tới
            // tuy nhiên CTC yêu cầu mở ra cho nhập với MLNS con (BHangCha = 0 và BHangChaDuToan = 1)
            // ducbq1. Không chắc chắn các lỗi phát sinh, thân ái!
            CreateMap<NsDuToanChungTuChiTietQuery, DtChungTuChiTietModel>()
                .ForMember(entity => entity.IsHangCha, model => model.MapFrom(z => z.BHangChaDuToan & z.BHangCha));
            CreateMap<NsDuToanChungTuChiTietDieuChinhQuery, DtChungTuChiTietModel>()
                .ForMember(entity => entity.IsHangCha, model => model.MapFrom(z => z.BHangCha));
            CreateMap<ReportChiTieuDuToanQuery, DtChungTuChiTietModel>()
                .ForMember(x => x.SLns, y => y.MapFrom(z => z.LNS))
                .ForMember(x => x.SL, y => y.MapFrom(z => z.L))
                .ForMember(x => x.SK, y => y.MapFrom(z => z.K))
                .ForMember(x => x.SM, y => y.MapFrom(z => z.M))
                .ForMember(x => x.STm, y => y.MapFrom(z => z.TM))
                .ForMember(x => x.STtm, y => y.MapFrom(z => z.TTM))
                .ForMember(x => x.SNg, y => y.MapFrom(z => z.NG))
                .ForMember(x => x.STng, y => y.MapFrom(z => z.TNG))
                .ForMember(x => x.STng1, y => y.MapFrom(z => z.TNG1))
                .ForMember(x => x.STng2, y => y.MapFrom(z => z.TNG2))
                .ForMember(x => x.STng3, y => y.MapFrom(z => z.TNG3))
                .ForMember(x => x.SXauNoiMa, y => y.MapFrom(z => z.XauNoiMa))
                .ForMember(x => x.SMoTa, y => y.MapFrom(z => z.MoTa))
                .ForMember(x => x.IIdMaDonVi, y => y.MapFrom(z => z.MaDonVi))
                .ForMember(x => x.STenDonVi, y => y.MapFrom(z => z.TenDonVi))
                .ForMember(x => x.FTuChi, y => y.MapFrom(z => z.TuChi.GetValueOrDefault(0)))
                .ForMember(x => x.FRutKBNN, y => y.MapFrom(z => z.RutKBNN.GetValueOrDefault(0)))
                .ForMember(x => x.FHienVat, y => y.MapFrom(z => z.HienVat.GetValueOrDefault(0)))
                .ForMember(x => x.FDuPhong, y => y.MapFrom(z => z.DuPhong.GetValueOrDefault(0)))
                .ForMember(x => x.FHangNhap, y => y.MapFrom(z => z.HangNhap.GetValueOrDefault(0)))
                .ForMember(x => x.FHangMua, y => y.MapFrom(z => z.HangMua.GetValueOrDefault(0)))
                .ForMember(x => x.FPhanCap, y => y.MapFrom(z => z.PhanCap.GetValueOrDefault(0)))
                .ForMember(x => x.IIdMlns, y => y.MapFrom(z => z.MlnsId))
                .ForMember(x => x.IIdMlnsCha, y => y.MapFrom(z => z.MlnsIdParent))
                .ForMember(x => x.IsHangCha, y => y.MapFrom(z => z.IsHangCha));
            CreateMap<ReportDuToanNhanPhanBoTheoDotQuery, DtChungTuChiTietModel>()
                .ForMember(entity => entity.IsHangCha, model => model.MapFrom(z => z.BHangChaDuToan));
            CreateMap<TnDtDuToanReportQuery, TnDtDuToanReportModel>();
            CreateMap<TnDtDuToanReportQuery, TnDtDuToanReport2Model>();
        }
    }
}
