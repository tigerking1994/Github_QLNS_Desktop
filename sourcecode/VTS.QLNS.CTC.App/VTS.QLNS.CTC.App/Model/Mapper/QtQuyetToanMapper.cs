using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class QtQuyetToanMapper : Profile
    {
        public QtQuyetToanMapper()
        {
            CreateMap<ApproveSettlementDoneDialogModel, VdtQtQuyetToan>()
                .ForMember(entity => entity.SSoQuyetDinh, model => model.MapFrom(item => item.SoQuyetDinh))
                .ForMember(entity => entity.SCoQuanPheDuyet, model => model.MapFrom(item => item.CoQuanPheDuyet))
                .ForMember(entity => entity.FChiPhiThietHai, model => model.MapFrom(item => item.ChiPhiThietHai))
                .ForMember(entity => entity.FChiPhiKhongTaoNenTaiSan, model => model.MapFrom(item => item.ChiPhiKhongTaoTaiSan))
                .ForMember(entity => entity.FTaiSanDaiHanThuocCDTQuanLy, model => model.MapFrom(item => item.DaiHanThuocQuanLy))
                .ForMember(entity => entity.FTaiSanDaiHanDonViKhacQuanLy, model => model.MapFrom(item => item.DaiHanDonViKhacQuanLy))
                .ForMember(entity => entity.FTaiSanNganHanThuocCDTQuanLy, model => model.MapFrom(item => item.NganHanThuocQuanLy))
                .ForMember(entity => entity.FTaiSanNganHanDonViKhacQuanLy, model => model.MapFrom(item => item.NganHanDonViKhacQuanLy))
                .ForMember(entity => entity.SNguoiKy, model => model.MapFrom(item => item.NguoiKy));

            CreateMap<Core.Domain.Query.NguonVonQuyetToanQuery, VdtQtQuyetToanNguonVonModel>();

            CreateMap<Core.Domain.Query.NguonVonQuyetToanQuery, VdtQtQuyetToanPheDuyetNguonVonModel>();

            CreateMap<NguonVonQuyetToanKeHoachQuery, VdtQtDeNghiQuyetToanNguonvon>();
            CreateMap<VdtQtDeNghiQuyetToanNguonvon, NguonVonQuyetToanKeHoachQuery>();

            CreateMap<NguonVonQuyetToanKeHoachQuery, VdtQtQuyetToanNguonVonModel>();
            CreateMap<VdtQtQuyetToanNguonVonModel, NguonVonQuyetToanKeHoachQuery>();
        }
    }
}
