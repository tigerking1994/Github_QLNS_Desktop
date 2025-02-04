using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhNhuCauChiQuyService
    {
        IEnumerable<NhNhuCauChiQuyQuery> GetAll();
        void Add(NhNhuCauChiQuy entity);
        void Update(NhNhuCauChiQuy entity);
        void Delete(NhNhuCauChiQuy entity);
        string UpdateChilrenGeneral(Guid? Id);
        NhNhuCauChiQuy FindById(Guid Id);
        void LockOrUnLock(NhNhuCauChiQuy entity);
        void UpDateRange(List<NhNhuCauChiQuy> entities);
        IEnumerable<NhNhuCauChiQuyBaoCaoQuery> ReportNhuCauChiQuy(Guid idChiQuy);
        string GetSTTLAMA(int STT);
    }
}
