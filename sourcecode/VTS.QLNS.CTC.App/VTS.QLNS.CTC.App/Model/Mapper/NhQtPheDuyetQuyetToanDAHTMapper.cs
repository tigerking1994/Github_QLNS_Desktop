using AutoMapper;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NhQtPheDuyetQuyetToanDAHTMapper : Profile
    {
        public NhQtPheDuyetQuyetToanDAHTMapper()
        {
            CreateMap<NhQtPheDuyetQuyetToanDAHTModel, NhQtPheDuyetQuyetToanDAHT>().ReverseMap();
            CreateMap<NhQtPheDuyetQuyetToanDAHTModel, NhQtPheDuyetQuyetToanDAHTQuery>().ReverseMap();
            CreateMap<NhQtPheDuyetQuyetToanDAHTChiTietModel, NhQtPheDuyetQuyetToanDAHTChiTietQuery>().ReverseMap();
            CreateMap<NhQtPheDuyetQuyetToanDAHTChiTietModel, NhQtPheDuyetQuyetToanDAHTChiTiet>().ReverseMap();
        }
    }
}
