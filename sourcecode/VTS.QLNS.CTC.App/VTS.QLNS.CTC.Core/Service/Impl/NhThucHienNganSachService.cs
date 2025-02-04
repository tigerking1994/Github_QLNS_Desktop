using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhTtThucHienNganSachService : INhTtThucHienNganSachService
    {
        private INhTtThucHienNganSachRepository _repository;
        public NhTtThucHienNganSachService(INhTtThucHienNganSachRepository repository)
        {
            _repository = repository;
        }
        public IEnumerable<NhTtThucHienNganSachQuery> FindAllData(int? tabTable, int? iQuyList, int? iNam, int? iTuNam, int? iDenNam, Guid? iDonVi)
        {
            return _repository.FindAllData(tabTable, iQuyList, iNam, iTuNam, iDenNam, iDonVi);
        }
        public IEnumerable<NhTtThucHienNganSachGiaiDoanQuery> GetGiaiDoan()
        {
            return _repository.GetGiaiDoan();
        }

        public string GetSTTLAMA(int STT)
        {
            return _repository.GetSTTLAMA(STT);
        }

        public NhTtThucHienNganSach FindById(Guid id)
        {
            return _repository.Find(id);
        }

        public IEnumerable<NhTtThucHienNganSachQuery> ReportThucHienNganSach(int? tabindex, int? iQuyPrint, int? iNamPrint, int? iTuNamPrint, int? iDenNamPrint, Guid? iDonVi)
        {
            return _repository.ReportThucHienNganSach(tabindex, iQuyPrint, iNamPrint, iTuNamPrint, iDenNamPrint, iDonVi);
        }
    }
}
