using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ISysAuditLogService
    {
        void WriteLog(
            string sApplicationName,
            string sServiceCode,
            int iActionName,
            DateTime dStartTime,
            bool bTransactionStatus,
            string sUserLogin,
            int? iErrorCode = null,
            string sErrorDecription = null
            );

        IEnumerable<HtNhatKyCapNhatDuLieu> FindAll(Expression<Func<HtNhatKyCapNhatDuLieu, bool>> predicate);

        void Delete(Guid id);

        HtNhatKyCapNhatDuLieu GetLogCategory(string sApplicationName,
            string sServiceCode,
            int iActionName,
            DateTime dStartTime,
            bool bTransactionStatus,
            string sUserLogin,
            int? iErrorCode = null,
            string sErrorDecription = null);

        void AddAllLogs(IEnumerable<HtNhatKyCapNhatDuLieu> sysAuditLogs);
    }
}
