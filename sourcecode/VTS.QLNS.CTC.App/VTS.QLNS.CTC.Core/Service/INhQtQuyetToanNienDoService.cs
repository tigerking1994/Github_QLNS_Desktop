using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhQtQuyetToanNienDoService
    {
        void Add(NhQtQuyetToanNienDo entity);
        void Update(NhQtQuyetToanNienDo entity);
        void Delete(NhQtQuyetToanNienDo entity);
        void LockOrUnlock(Guid id, bool status);
        

        NhQtQuyetToanNienDo FindById(Guid id);
        IEnumerable<NhQtQuyetToanNienDoQuery> FindIndex();
        IEnumerable<NhQtQuyetToanNienDoQuery> FindTongHopIndex();
        IEnumerable<NhQtQuyetToanNienDo> FindAll();
        IEnumerable<NhQtQuyetToanNienDo> FindAll(Expression<Func<NhQtQuyetToanNienDo, bool>> predicate);
        IEnumerable<ReportNhQtQuyetToanNienDoNamQuery> ReportNam(Guid quyetToanNienDoId);
        IEnumerable<ReportNhQtQuyetToanNienDoQuyQuery> ReportQuy(Guid quyetToanNienDoId);
        public bool CheckDuplicateSoQD(string soQuyetDinh, Guid id);
        public bool CheckDuplicateQTND(Guid? IIdDonViId, int? INamKeHoach, Guid id);
    }
}
