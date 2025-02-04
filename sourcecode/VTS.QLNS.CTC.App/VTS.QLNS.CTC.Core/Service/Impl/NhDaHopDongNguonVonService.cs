using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhDaHopDongNguonVonService : INhDaHopDongNguonVonService
    {
        private readonly INhDaHopDongNguonVonRepository _nhDaHopDongNguonVonRepository;

        public NhDaHopDongNguonVonService(INhDaHopDongNguonVonRepository nhDaHopDongNguonVonRepository)
        {
            _nhDaHopDongNguonVonRepository = nhDaHopDongNguonVonRepository;
        }

        public int UpdateRange(IEnumerable<NhDaHopDongNguonVon> entities)
        {
           return _nhDaHopDongNguonVonRepository.UpdateRange(entities);
        }
        public void Add(NhDaHopDongNguonVon nhHopDongNguonVon)
        {
            _nhDaHopDongNguonVonRepository.Add(nhHopDongNguonVon);
        }
        public void Delete(NhDaHopDongNguonVon nhDaHopDongNguonVon)
        {
            _nhDaHopDongNguonVonRepository.Delete(nhDaHopDongNguonVon.Id);
        }
        public IEnumerable<NhDaHopDongNguonVon> FindByIdHopDong(Guid idHopDong)
        {
                return _nhDaHopDongNguonVonRepository.FindByIdHopDong(idHopDong);
        }
        public IEnumerable<NhDaHopDongNguonVon> FindByHopDongIdQuyetDinhId(Guid? idHopDong, Guid? idQuyetDinh)
        {
            return _nhDaHopDongNguonVonRepository.FindByHopDongIdQuyetDinhId(idHopDong, idQuyetDinh);
        }
        public IEnumerable<NhDaHopDongNguonVon> FindByHopDongIdGoiThauNguonVonId(Guid? idHopDong, Guid? idGoiThauNguonVon)
        {
            return _nhDaHopDongNguonVonRepository.FindByHopDongIdGoiThauNguonVonId(idHopDong, idGoiThauNguonVon);
        }

        public IEnumerable<NhDaHopDongNguonVon> FindAll(Expression<Func<NhDaHopDongNguonVon, bool>> predicate)
        {
            return _nhDaHopDongNguonVonRepository.FindAll(predicate);
        }
    }
}
