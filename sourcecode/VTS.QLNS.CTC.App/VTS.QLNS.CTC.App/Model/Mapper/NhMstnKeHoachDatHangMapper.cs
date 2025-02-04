using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NhMstnKeHoachDatHangMapper : Profile
    {
        public NhMstnKeHoachDatHangMapper()
        {
            //CreateMap<NhDaDetailNguonVonQuery, NhDaGoiThauDetailNguonVonModel>();
            //CreateMap<NhDaGoiThauDetailNguonVonModel, NhDaDetailNguonVonQuery>();

            //CreateMap<NhDaDetailChiPhiQuery, NhDaGoiThauDetailChiPhiModel>();
            //CreateMap<NhDaGoiThauDetailChiPhiModel, NhDaDetailChiPhiQuery>();

            //CreateMap<NhDaDetailHangMucQuery, NhDaGoiThauDetailHangMucModel>();
            //CreateMap<NhDaGoiThauDetailHangMucModel, NhDaDetailHangMucQuery>();

            CreateMap<NhMSTNKeHoachDatHang, NhMstnKeHoachDatHangModel>();
            CreateMap<NhMstnKeHoachDatHangModel, NhMSTNKeHoachDatHang>();

            CreateMap<NhMstnKeHoachDatHangModel, NhMstnKeHoachDatHangQuery>();
            CreateMap<NhMstnKeHoachDatHangQuery, NhMstnKeHoachDatHangModel>();
        }
    }
}
