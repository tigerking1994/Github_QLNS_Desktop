using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NhKtKhoiTaoCapPhatChiTietMapper : Profile
    {
        public NhKtKhoiTaoCapPhatChiTietMapper()
        {
            CreateMap<NhKtKhoiTaoCapPhatChiTietModel, NhKtKhoiTaoCapPhatChiTiet>().ReverseMap();
            CreateMap<NhKtKhoiTaoCapPhatChiTietModel, NhKtKhoiTaoCapPhatChiTietQuery>().ReverseMap();
            CreateMap<NhKtKhoiTaoCapPhatChiTietModel, NhKtKhoiTaoCapPhatImportModel>().ReverseMap();
            CreateMap<NhKtKhoiTaoCapPhatChiTietImport, NhKtKhoiTaoCapPhatChiTiet>()
                .ForMember(entity => entity.ILoaiNoiDung, model => model.MapFrom(item => item.ILoai))
                .ForMember(entity => entity.SMaNoiDung, model => model.MapFrom(item => item.STT))
                .ForMember(entity => entity.STenNoiDung, model => model.MapFrom(item => item.NoiDung))
                .ForMember(entity => entity.FQTKinhPhiDuyetCacNamTruocUSD, model => model.MapFrom(item => NumberUtils.ConvertTextToDouble(item.FQTKinhPhiDuyetCacNamTruocUSD)))
                .ForMember(entity => entity.FQTKinhPhiDuyetCacNamTruocVND, model => model.MapFrom(item => NumberUtils.ConvertTextToDouble(item.FQTKinhPhiDuyetCacNamTruocVND)))
                .ForMember(entity => entity.FDeNghiQTNamNayUSD, model => model.MapFrom(item => NumberUtils.ConvertTextToDouble(item.FDeNghiQTNamNayUSD)))
                .ForMember(entity => entity.FDeNghiQTNamNayVND, model => model.MapFrom(item => NumberUtils.ConvertTextToDouble(item.FDeNghiQTNamNayVND)))
                .ForMember(entity => entity.FDeNghiChuyenNamSauUsd, model => model.MapFrom(item => NumberUtils.ConvertTextToDouble(item.FDeNghiChuyenNamSauUSD)))
                .ForMember(entity => entity.FDeNghiChuyenNamSauVnd, model => model.MapFrom(item => NumberUtils.ConvertTextToDouble(item.FDeNghiChuyenNamSauVND)))
                .ForMember(entity => entity.FLuyKeKinhPhiDuocCapUSD, model => model.MapFrom(item => NumberUtils.ConvertTextToDouble(item.FLuyKeKinhPhiDuocCapUSD)))
                .ForMember(entity => entity.FLuyKeKinhPhiDuocCapVND, model => model.MapFrom(item => NumberUtils.ConvertTextToDouble(item.FLuyKeKinhPhiDuocCapVND)))
                .ForMember(entity => entity.FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd, model => model.MapFrom(item => NumberUtils.ConvertTextToDouble(item.FLuyKeKinhPhiDaGiaiNganTrongNamNayUSD)))
                .ForMember(entity => entity.FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd, model => model.MapFrom(item => NumberUtils.ConvertTextToDouble(item.FLuyKeKinhPhiDaGiaiNganTrongNamNayVND)))
                .ForMember(entity => entity.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd, model => model.MapFrom(item => NumberUtils.ConvertTextToDouble(item.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUSD)))
                .ForMember(entity => entity.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd, model => model.MapFrom(item => NumberUtils.ConvertTextToDouble(item.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVND)))
                .ForMember(entity => entity.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd, model => model.MapFrom(item => NumberUtils.ConvertTextToDouble(item.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUSD)))
                .ForMember(entity => entity.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd, model => model.MapFrom(item => NumberUtils.ConvertTextToDouble(item.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVND)))

                .ForMember(entity => entity.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd, model => model.MapFrom(item => NumberUtils.ConvertTextToDouble(item.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUSD)))
                .ForMember(entity => entity.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd, model => model.MapFrom(item => NumberUtils.ConvertTextToDouble(item.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVND)))
                .ForMember(entity => entity.FKinhPhiThuaNopNsnnUSD, model => model.MapFrom(item => NumberUtils.ConvertTextToDouble(item.FKinhPhiThuaNopNsnnUSD)))
                .ForMember(entity => entity.FKinhPhiThuaNopNsnnVND, model => model.MapFrom(item => NumberUtils.ConvertTextToDouble(item.FKinhPhiThuaNopNsnnVND)))
                .ForMember(entity => entity.FConLaiChuaGiaiNganUSD, model => model.MapFrom(item => NumberUtils.ConvertTextToDouble(item.FConLaiChuaGiaiNganUSD)))
                .ForMember(entity => entity.FConLaiChuaGiaiNganVND, model => model.MapFrom(item => NumberUtils.ConvertTextToDouble(item.FConLaiChuaGiaiNganVND)));

            CreateMap<NhKtKhoiTaoCapPhatChiTietImport, NhKtKhoiTaoTheoQuyetDinhKhacChiTietImportModel>()
                .ForMember(entity => entity.ILoaiNoiDung, model => model.MapFrom(item => item.ILoai))
                .ForMember(entity => entity.SMaNoiDung, model => model.MapFrom(item => item.STT))
                .ForMember(entity => entity.STenNoiDung, model => model.MapFrom(item => item.NoiDung))
                .ForMember(entity => entity.FQTKinhPhiDuyetCacNamTruocUSD, model => model.MapFrom(item => NumberUtils.ConvertTextToDouble(item.FQTKinhPhiDuyetCacNamTruocUSD)))
                .ForMember(entity => entity.FQTKinhPhiDuyetCacNamTruocVND, model => model.MapFrom(item => NumberUtils.ConvertTextToDouble(item.FQTKinhPhiDuyetCacNamTruocVND)))
                .ForMember(entity => entity.FDeNghiQTNamNayUSD, model => model.MapFrom(item => NumberUtils.ConvertTextToDouble(item.FDeNghiQTNamNayUSD)))
                .ForMember(entity => entity.FDeNghiQTNamNayVND, model => model.MapFrom(item => NumberUtils.ConvertTextToDouble(item.FDeNghiQTNamNayVND)))
                .ForMember(entity => entity.FDeNghiChuyenNamSauUsd, model => model.MapFrom(item => NumberUtils.ConvertTextToDouble(item.FDeNghiChuyenNamSauUSD)))
                .ForMember(entity => entity.FDeNghiChuyenNamSauVnd, model => model.MapFrom(item => NumberUtils.ConvertTextToDouble(item.FDeNghiChuyenNamSauVND)))
                .ForMember(entity => entity.FLuyKeKinhPhiDuocCapUSD, model => model.MapFrom(item => NumberUtils.ConvertTextToDouble(item.FLuyKeKinhPhiDuocCapUSD)))
                .ForMember(entity => entity.FLuyKeKinhPhiDuocCapVND, model => model.MapFrom(item => NumberUtils.ConvertTextToDouble(item.FLuyKeKinhPhiDuocCapVND)))
                .ForMember(entity => entity.FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd, model => model.MapFrom(item => NumberUtils.ConvertTextToDouble(item.FLuyKeKinhPhiDaGiaiNganTrongNamNayUSD)))
                .ForMember(entity => entity.FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd, model => model.MapFrom(item => NumberUtils.ConvertTextToDouble(item.FLuyKeKinhPhiDaGiaiNganTrongNamNayVND)))
                .ForMember(entity => entity.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd, model => model.MapFrom(item => NumberUtils.ConvertTextToDouble(item.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUSD)))
                .ForMember(entity => entity.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd, model => model.MapFrom(item => NumberUtils.ConvertTextToDouble(item.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVND)))
                .ForMember(entity => entity.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd, model => model.MapFrom(item => NumberUtils.ConvertTextToDouble(item.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUSD)))
                .ForMember(entity => entity.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd, model => model.MapFrom(item => NumberUtils.ConvertTextToDouble(item.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVND)))

                .ForMember(entity => entity.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd, model => model.MapFrom(item => NumberUtils.ConvertTextToDouble(item.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUSD)))
                .ForMember(entity => entity.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd, model => model.MapFrom(item => NumberUtils.ConvertTextToDouble(item.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVND)))
                .ForMember(entity => entity.FKinhPhiThuaNopNsnnUSD, model => model.MapFrom(item => NumberUtils.ConvertTextToDouble(item.FKinhPhiThuaNopNsnnUSD)))
                .ForMember(entity => entity.FKinhPhiThuaNopNsnnVND, model => model.MapFrom(item => NumberUtils.ConvertTextToDouble(item.FKinhPhiThuaNopNsnnVND)))
                .ForMember(entity => entity.FConLaiChuaGiaiNganUSD, model => model.MapFrom(item => NumberUtils.ConvertTextToDouble(item.FConLaiChuaGiaiNganUSD)))
                .ForMember(entity => entity.FConLaiChuaGiaiNganVND, model => model.MapFrom(item => NumberUtils.ConvertTextToDouble(item.FConLaiChuaGiaiNganVND)))
                .ForMember(entity => entity.FGiaTriNoiDungVND, model => model.MapFrom(item => NumberUtils.ConvertTextToDouble(item.FGiaTriNoiDungVND)))
                .ForMember(entity => entity.FGiaTriNoiDungUSD, model => model.MapFrom(item => NumberUtils.ConvertTextToDouble(item.FGiaTriNoiDungUSD)));
            CreateMap<NhKtKhoiTaoTheoQuyetDinhKhacChiTietImportModel, NhKtKhoiTaoCapPhatChiTiet>().ReverseMap();
            CreateMap<NhKtKhoiTaoTheoQuyetDinhKhacChiTietImportModel, NhKtKhoiTaoCapPhatChiTietQuery>().ReverseMap();
            CreateMap<NhKtKhoiTaoTheoQuyetDinhKhacChiTietImportModel, NhDaHopDongChiPhi>()
                .ForMember(entity => entity.IIdHopDongId, model => model.MapFrom(item => item.IIdHopDongID))
                .ForMember(entity => entity.IIdChiPhiId, model => model.MapFrom(item => item.IIdChiPhiID))
                .ForMember(entity => entity.FGiaTriUsd, model => model.MapFrom(item => item.FGiaTriNoiDungUSD))
                .ForMember(entity => entity.FGiaTriVnd, model => model.MapFrom(item => item.FGiaTriNoiDungVND));
            CreateMap<NhKtKhoiTaoTheoQuyetDinhKhacChiTietImportModel, NhDaQuyetDinhKhacChiPhi>()
                .ForMember(entity => entity.IIdQuyetDinhKhacId, model => model.MapFrom(item => item.IIdQuyetDinhKhacID))
                .ForMember(entity => entity.IIdDmChiPhiId, model => model.MapFrom(item => item.IIdChiPhiID))
                .ForMember(entity => entity.FGiaTriUsd, model => model.MapFrom(item => item.FGiaTriNoiDungUSD))
                .ForMember(entity => entity.FGiaTriVnd, model => model.MapFrom(item => item.FGiaTriNoiDungVND));


        }
    }
}
