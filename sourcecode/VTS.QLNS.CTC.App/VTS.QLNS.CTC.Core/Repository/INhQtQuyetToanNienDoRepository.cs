using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INhQtQuyetToanNienDoRepository : IRepository<NhQtQuyetToanNienDo>
    {
        IEnumerable<NhQtQuyetToanNienDoQuery> FindIndex();
        IEnumerable<NhQtQuyetToanNienDoQuery> FindTongHopIndex();
        IEnumerable<ReportNhQtQuyetToanNienDoNamQuery> ReportNam(Guid quyetToanNienDoId);
        IEnumerable<ReportNhQtQuyetToanNienDoQuyQuery> ReportQuy(Guid quyetToanNienDoId);
        public bool CheckDuplicateSoQD(string soQuyetDinh, Guid id);
        public bool CheckDuplicateQTND(Guid? IIdDonViId, int? INamKeHoach, Guid id);
    }
}
