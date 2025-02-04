using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class PhanBoVonChiTietMapper : Profile
    {
        public PhanBoVonChiTietMapper()
        {
            CreateMap<PhanBoVonDuocDuyetChiTietQuery, PhanBoVonChiTietQuery>();
            CreateMap<PhanBoVonChiTietQuery, YearPlanManagerExportModel>();
            CreateMap<PhanBoVonChiTietQuery, PhanBoVonChiTietModel>()
                .ForMember(n => n.fVonConLai, m => m.MapFrom(n => n.fGiaTriDauTu - n.fVonDaBoTri));

            CreateMap<PhanBoVonChiTietModel, PhanBoVonChiTietQuery>();
            CreateMap<RptDieuChinhKeHoachModel, RptDieuChinhKeHoachQuery>();
            CreateMap<RptDieuChinhKeHoachQuery, RptDieuChinhKeHoachModel>();
            CreateMap<PhanBoVonImportModel, PhanBoVon_ChuyenTiepImportModel>();
            CreateMap<PhanBoVon_ChuyenTiepImportModel, PhanBoVonImportModel>();

            CreateMap<PhanBoVonImportModel, PhanBoVon_MoMoiImportModel>();
            CreateMap<PhanBoVon_MoMoiImportModel, PhanBoVonImportModel>();

            CreateMap<PhanBoVonImportModel, PhanBoVonChiTietInsertQuery>()
                .ForMember(n => n.sK, m => m.MapFrom(n => n.sK))
                .ForMember(n => n.sL, m => m.MapFrom(n => n.sL))
                .ForMember(n => n.sM, m => m.MapFrom(n => n.sM))
                .ForMember(n => n.sNG, m => m.MapFrom(n => n.sNg))
                .ForMember(n => n.sTM, m => m.MapFrom(n => n.sTm))
                .ForMember(n => n.sTTM, m => m.MapFrom(n => n.sTtm));

            CreateMap<ComboboxItem, SelectedItemModel>()
                .ForMember(n => n.Value, m => m.MapFrom(n => n.ValueItem))
                .ForMember(n => n.DisplayName, m => m.MapFrom(n => n.DisplayItem));

            CreateMap<SelectedItemModel, ComboboxItem>()
                .ForMember(n => n.ValueItem, m => m.MapFrom(n => n.Value))
                .ForMember(n => n.DisplayItem, m => m.MapFrom(n => n.DisplayName));

            CreateMap<KeHoachNamDuocDuyetDetailModel, KeHoachNamDuocDuyetDetailQuery>();
            CreateMap<KeHoachNamDuocDuyetDetailQuery, KeHoachNamDuocDuyetDetailModel>();

            CreateMap<PhanBoVonImportModel, VdtKhvPhanBoVonChiTiet>()
                .ForMember(n => n.IIdPhanBoVonId, m => m.MapFrom(n => n.IdChungTu));
        }
    }
}
