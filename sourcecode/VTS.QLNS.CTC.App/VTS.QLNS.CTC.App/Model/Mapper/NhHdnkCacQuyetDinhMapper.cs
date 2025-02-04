using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NhHdnkCacQuyetDinhMapper : Profile
    {
        public NhHdnkCacQuyetDinhMapper()
        {
            CreateMap<NhHdnkCacQuyetDinh, NhQuyetDinhDamPhamModel>();
            CreateMap<NhQuyetDinhDamPhamModel, NhHdnkCacQuyetDinh>();

            CreateMap<NhQuyetDinhDamPhamQuery, NhQuyetDinhDamPhamModel>()
                .ForMember(entity => entity.SLoaiQuyetDinhText , model => model.MapFrom(item => DisplayQuyetDinhText(item)))
                .ForMember(entity => entity.SLoaiNhiemVuChiText , model => model.MapFrom(item => DisplayNhiemVuChiText(item)));
            CreateMap<NhQuyetDinhDamPhamModel, NhQuyetDinhDamPhamQuery>();
        }

        private string DisplayQuyetDinhText(NhQuyetDinhDamPhamQuery item)
        {
            string result = "";
            switch (item.ILoaiQuyetDinh)
            {
                case 1:
                    result = "Phê duyệt kết quả đàm phán";
                    break;    
                case 2:       
                    result = "Phê duyệt chi trong nước";
                    break;    
                case 3:       
                    result = "Phê duyệt chi đoàn ra";
                    break;
            }
            return result;
        }
        private string DisplayNhiemVuChiText(NhQuyetDinhDamPhamQuery item)
        {
            string result = "";
            switch (item.ILoai)
            {
                case 1:
                    result = "Nhiệm vụ chi mua sắm";
                    break;
                case 2:
                    result = "Nhiệm vụ chi dự án";
                    break;
            }
            return result;
        }
    }
}
