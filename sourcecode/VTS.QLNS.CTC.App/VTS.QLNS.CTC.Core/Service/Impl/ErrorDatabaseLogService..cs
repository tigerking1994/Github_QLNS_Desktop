using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class ErrorDatabaseLogService : IErrorDatabaseLogService
    {
        private readonly IErrorDatabaseLogRepository _reposirory;

        public ErrorDatabaseLogService(IErrorDatabaseLogRepository reposirory)
        {
            _reposirory = reposirory;
        }

        public void AddRange(IEnumerable<ErrorDatabaseLog> entities)
        {
            _reposirory.AddRange(entities);
        }

        public void Add(ErrorDatabaseLog entity)
        {
            _reposirory.Add(entity);
        }

        public ErrorDatabaseLog FindById(Guid id)
        {
            return _reposirory.Find(id);
        }

        public IEnumerable<ErrorDatabaseLog> FindAllByToday()
        {
            return _reposirory.FindAll(x => x.CreatedAt.Date == DateTime.Now.Date);
        }
        public void RemoveByToday()
        {
            var data = FindAllByToday();
            _reposirory.RemoveRange(data);
        }

        public void RemoveErrorDatabaseLogs(IEnumerable<ErrorDatabaseLog> ErrorDatabaseLogs)
        {
            _reposirory.RemoveRange(ErrorDatabaseLogs);
        }

    }
}
