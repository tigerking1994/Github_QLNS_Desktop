using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Repository.Impl;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class BhQtcqCtctGtTroCapService : IBhQtcqCtctGtTroCapService
    {
        private readonly IBhQtcqCtctGtTroCapRepository _repository;
        public BhQtcqCtctGtTroCapService(IBhQtcqCtctGtTroCapRepository repository)
        {
            _repository = repository;
        }

        public void AddRange(List<BhQtcqCtctGtTroCap> listGiaiThichLuongTrus)
        {
            _repository.AddRange(listGiaiThichLuongTrus);
        }

        public void Delete(Guid id)
        {
            BhQtcqCtctGtTroCap bhQtcqCtctGtTroCap = _repository.Find(id);
            if (bhQtcqCtctGtTroCap != null)
                _repository.Delete(id);
        }

        public int DeleteDupItem(Guid voucherID)
        {
            return _repository.DeleteDupItem(voucherID);
        }

        public BhQtcqCtctGtTroCap FindById(Guid id)
        {
            return _repository.Find(id);
        }

        public IEnumerable<BhQtcqCtctGtTroCap> FindByVoucherID(Guid voucherID)
        {
            return _repository.FindByVoucherID(voucherID);
        }

        public IEnumerable<BhSalaryDataQuery> FindSalaryDataByXauNoiMaAnQuarte(string sXauNoiMa, string sQuarte, string sMaDonVi, int namLamViec, string sLNS)
        {
            return _repository.FindSalaryDataByXauNoiMaAnQuarte(sXauNoiMa, sQuarte, sMaDonVi, namLamViec, sLNS);
        }

        public List<BhQtcqCtctGtTroCapQuery> GetDataExplainSubtracts(int yearWork, string idQTCQuyCheDoBHXH, string sXauNoiMa)
        {
            return _repository.GetDataExplainSubtracts(yearWork, idQTCQuyCheDoBHXH, sXauNoiMa);
        }

        public bool IsExistExplain(Guid voucherQTCQID)
        {
            return _repository.IsExistExplain(voucherQTCQID);
        }

        public int RemoveRange(IEnumerable<BhQtcqCtctGtTroCap> items)
        {
            return _repository.RemoveRange(items);
        }

        public void Update(BhQtcqCtctGtTroCap giaiThichLuongTru)
        {
            _repository.Update(giaiThichLuongTru);
        }

        public IEnumerable<BhQtcqBHXHChiTiet> FindDataBhxhByIdQtcqAndXauNoiMa(Guid voucherID, string sXauNoiMa, int iNamLamViec)
        {
            return _repository.FindDataBhxhByIdQtcqAndXauNoiMa(voucherID, sXauNoiMa, iNamLamViec);
        }

        public List<BhQtcqCtctGtTroCap> FindByForIdChungTu(Guid id)
        {
            return _repository.FindByForIdChungTu(id);
        }

        public List<BhQtcqCtctGtTroCap> FindExplainSubtracts(int iNamLamViec)
        {
            return _repository.FindExplainSubtracts(iNamLamViec);
        }
    }
}
