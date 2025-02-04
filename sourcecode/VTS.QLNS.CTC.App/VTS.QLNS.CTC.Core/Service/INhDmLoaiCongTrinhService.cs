using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhDmLoaiCongTrinhService
    {
        IEnumerable<NhDmLoaiCongTrinh> FindAll();
    }
}
