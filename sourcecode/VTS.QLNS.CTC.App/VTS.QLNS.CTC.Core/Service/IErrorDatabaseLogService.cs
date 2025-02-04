using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IErrorDatabaseLogService
    {
        IEnumerable<ErrorDatabaseLog> FindAllByToday();
        void RemoveByToday();
        void AddRange(IEnumerable<ErrorDatabaseLog> entities);
        void Add(ErrorDatabaseLog entity);
        ErrorDatabaseLog FindById(Guid id);
    }
}
