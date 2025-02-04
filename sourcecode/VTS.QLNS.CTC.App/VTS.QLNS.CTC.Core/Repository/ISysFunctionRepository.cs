using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ISysFunctionRepository : IRepository<HtChucNang>
    {
        public IEnumerable<HtChucNang> FindAllWithAuthorties();
        HtChucNang FindOneWithAuthorities(Guid id);
    }
}
