using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class SysAuditLogService : ISysAuditLogService
    {
        private readonly ISysAuditLogRepository _iLogRepository;

        public SysAuditLogService(ISysAuditLogRepository logRepository)
        {
            _iLogRepository = logRepository;
        }

        public void AddAllLogs(IEnumerable<HtNhatKyCapNhatDuLieu> sysAuditLogs)
        {
            _iLogRepository.AddRange(sysAuditLogs);
        }

        public void Delete(Guid id)
        {
            _iLogRepository.Delete(id);
        }

        public IEnumerable<HtNhatKyCapNhatDuLieu> FindAll(Expression<Func<HtNhatKyCapNhatDuLieu, bool>> predicate)
        {
            return _iLogRepository.FindAll(predicate).ToList();
        }

        public HtNhatKyCapNhatDuLieu GetLogCategory(string sApplicationName,
            string sServiceCode,
            int iActionName,
            DateTime dStartTime,
            bool bTransactionStatus,
            string sUserLogin,
            int? iErrorCode = null,
            string sErrorDecription = null)
        {
            HtNhatKyCapNhatDuLieu log = new HtNhatKyCapNhatDuLieu();
            try
            {
                log.Id = Guid.NewGuid();
                log.ApplicationCode = sApplicationName;
                log.ServiceCode = sServiceCode;
                switch (iActionName)
                {
                    case (int)TypeExecute.Insert:
                        log.ActionName = TypeExecuteName.INSERT;
                        break;
                    case (int)TypeExecute.Update:
                        log.ActionName = TypeExecuteName.UPDATE;
                        break;
                    case (int)TypeExecute.Adjust:
                        log.ActionName = TypeExecuteName.ADJUST;
                        break;
                    case (int)TypeExecute.Delete:
                        log.ActionName = TypeExecuteName.DELETE;
                        break;
                }
                log.StartTime = dStartTime;
                log.EndTime = DateTime.Now;
                log.Duration = (int)(log.EndTime.Value).Subtract(dStartTime).TotalMilliseconds;
                log.Account = log.UserName = sUserLogin;
                log.TransactionStatus = bTransactionStatus;
                log.ErrorCode = iErrorCode;
                log.ErrorDescription = sErrorDecription;
                return log;
            }
            catch (Exception ex)
            {
                return log;
            }
        }

        public void WriteLog(
            string sApplicationName,
            string sServiceCode,
            int iActionName,
            DateTime dStartTime,
            bool bTransactionStatus,
            string sUserLogin,
            int? iErrorCode = null,
            string sErrorDecription = null)
        {
            try
            {
                HtNhatKyCapNhatDuLieu log = new HtNhatKyCapNhatDuLieu();
                log.Id = Guid.NewGuid();
                log.ApplicationCode = sApplicationName;
                log.ServiceCode = sServiceCode;
                switch (iActionName)
                {
                    case (int)TypeExecute.Insert:
                        log.ActionName = TypeExecuteName.INSERT;
                        break;
                    case (int)TypeExecute.Update:
                        log.ActionName = TypeExecuteName.UPDATE;
                        break;
                    case (int)TypeExecute.Adjust:
                        log.ActionName = TypeExecuteName.ADJUST;
                        break;
                    case (int)TypeExecute.Delete:
                        log.ActionName = TypeExecuteName.DELETE;
                        break;
                }
                log.StartTime = dStartTime;
                log.EndTime = DateTime.Now;
                log.Duration = (int)(log.EndTime.Value).Subtract(dStartTime).TotalMilliseconds;
                log.Account = log.UserName = sUserLogin;
                log.TransactionStatus = bTransactionStatus;
                log.ErrorCode = iErrorCode;
                log.ErrorDescription = sErrorDecription;
                _iLogRepository.Add(log);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
