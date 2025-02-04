using AutoMapper;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class Nc3YChungTuChiTietMapper : Profile
    {
        public Nc3YChungTuChiTietMapper()
        {
            CreateMap<NsNc3YChungTuChiTiet, NsNc3YChungTuChiTietModel>().ReverseMap();            
        }
    }
}