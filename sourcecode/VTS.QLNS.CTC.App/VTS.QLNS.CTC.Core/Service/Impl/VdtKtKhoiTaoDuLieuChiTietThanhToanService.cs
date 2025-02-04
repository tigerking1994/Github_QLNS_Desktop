using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class VdtKtKhoiTaoDuLieuChiTietThanhToanService : IVdtKtKhoiTaoDuLieuChiTietThanhToanService
    {
        private readonly IVdtKtKhoiTaoDuLieuChiTietThanhToanRepository _repository;

        public VdtKtKhoiTaoDuLieuChiTietThanhToanService(IVdtKtKhoiTaoDuLieuChiTietThanhToanRepository repository)
        {
            _repository = repository;
        }

        public void AddRange(IEnumerable<VdtKtKhoiTaoDuLieuChiTietThanhToan> datas)
        {
            _repository.AddRange(datas);
        }

        public void DeleteByKhoiTaoDuLieuId(Guid iId)
        {
            _repository.DeleteByKhoiTaoDuLieuId(iId);
        }

        public IEnumerable<VdtKtKhoiTaoDuLieuChiTietThanhToanQuery> GetDetailByKTDLId(Guid iId)
        {
            return _repository.GetDetailByKTDLId(iId);
        }
    }
}
