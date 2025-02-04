using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Repository.Impl;
using VTS.QLNS.CTC.Utility;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class BhCpBsChungTuChiTietService : IBhCpBsChungTuChiTietService
    {
        private readonly IBhCpBsChungTuChiTietRepository _repository;

        public BhCpBsChungTuChiTietService(IBhCpBsChungTuChiTietRepository chungTuCTRepository)
        {
            _repository = chungTuCTRepository;
        }

        public int AddRange(IEnumerable<BhCpBsChungTuChiTiet> chungTuChiTiets)
        {
            return _repository.AddRange(chungTuChiTiets);
        }

        public bool ExistVoucherDetail(Guid chungTuId)
        {
            return _repository.ExistVoucherDetail(chungTuId);
        }

        public IEnumerable<BhCpBsChungTuChiTietQuery> ExportKeHoachCapPhatBoSungKCBBHYT(string sIdCsYTe, int iQuy, int iNamLamViec, string userName, int donViTinh, string sXauNoiMa)
        {
            return _repository.ExportKeHoachCapPhatBoSungKCBBHYT(sIdCsYTe, iQuy, iNamLamViec, userName, donViTinh, sXauNoiMa);
        }

        public IEnumerable<BhCpBsChungTuChiTietQuery> ExportTheoCoSoYTe(Guid voucherID, string maCSYT, int iNamLamViec)
        {
            return _repository.ExportTheoCoSoYTe(voucherID, maCSYT, iNamLamViec);
        }

        public IEnumerable<BhCpBsChungTuChiTietQuery> ExportThongTriCapPhatBoSungKCBBHYT(string sIdCsYTe, int iQuy, int iNamLamViec, int donViTinh, string sXauNoiMa)
        {
            return _repository.ExportThongTriCapPhatBoSungKCBBHYT(sIdCsYTe, iQuy, iNamLamViec, donViTinh, sXauNoiMa);
        }

        public IEnumerable<BhCpBsChungTuChiTietQuery> ExportTongHopCapPhatBoSungKCBBHYT(string sIdCsYTe, int iQuy, int iNamLamViec, string userName, int donViTinh, string sXauNoiMa)
        {
            return _repository.ExportTongHopCapPhatBoSungKCBBHYT(sIdCsYTe, iQuy, iNamLamViec, userName, donViTinh, sXauNoiMa);
        }

        public IEnumerable<BhCpBsChungTuChiTiet> FindAllVouchers()
        {
            return _repository.FindAll();
        }

        public IEnumerable<BhCpBsChungTuChiTiet> FindByCondition(Expression<Func<BhCpBsChungTuChiTiet, bool>> predicate)
        {
            return _repository.FindAll(predicate);
        }

        public BhCpBsChungTuChiTiet FindById(Guid id)
        {
            return _repository.FindById(id);
        }

        public IEnumerable<BhCpBsChungTuChiTiet> FindChungTuChiTietByChungTuId(BhCpBsChungTuChiTietCriteria searchModel)
        {
            return _repository.FindChungTuChiTietByChungTuId(searchModel);
        }

        public IEnumerable<BhCpBsChungTuChiTiet> FindVoucherDetailByCondition(BhCpBsChungTuChiTietCriteria searchModel)
        {
            return _repository.FindVoucherDetailByCondition(searchModel);
        }

        public IEnumerable<string> GetMaCoSoYTeDetailByCondition(int iQuy, int iNamLamViec, string sXauNoiMa, bool isTongHop, AllocationFunction functionType, bool isShowAllCoSoYTe)
        {
            return _repository.GetMaCoSoYTeDetailByCondition(iQuy, iNamLamViec, sXauNoiMa, isTongHop, functionType, isShowAllCoSoYTe);
        }

        public int RemoveRange(IEnumerable<BhCpBsChungTuChiTiet> chungTuChiTiets)
        {
            return _repository.RemoveRange(chungTuChiTiets);
        }

        public int Update(BhCpBsChungTuChiTiet item)
        {
            return _repository.Update(item);
        }
    }
}
