using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhDaHopDongCacQuyetDinhService : INhDaHopDongCacQuyetDinhService
    {
        private readonly INhDaHopDongCacQuyetDinhRepository _nhDaHopDongCacQuyetDinhRepository;

        public NhDaHopDongCacQuyetDinhService(INhDaHopDongCacQuyetDinhRepository nhDaHopDongCacQuyetDinhRepository)
        {
            _nhDaHopDongCacQuyetDinhRepository = nhDaHopDongCacQuyetDinhRepository;
        }
        public void Add(NhDaHopDongCacQuyetDinh nhHopDongCacQuyetDinh)
        {
            _nhDaHopDongCacQuyetDinhRepository.Add(nhHopDongCacQuyetDinh);
        }
        public void DeleteQuyetDinh(Guid? idHopDong, Guid? idQuyetDinh)
        {
            _nhDaHopDongCacQuyetDinhRepository.DeleteQuyetDinh(idHopDong, idQuyetDinh);
        }
        public IEnumerable<NhDaHopDongCacQuyetDinh> FindByHopDongIdQuyetDinhId(Guid? idHopDong, Guid? idQuyetDinh)
        {
            return _nhDaHopDongCacQuyetDinhRepository.FindByHopDongIdQuyetDinhId(idHopDong, idQuyetDinh);
        }
        public IEnumerable<NhDaHopDongCacQuyetDinh> FindByIdHopDong(Guid? idHopDong)
        {
            return _nhDaHopDongCacQuyetDinhRepository.FindByIdHopDong(idHopDong);
        }
    }
}
