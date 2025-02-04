using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITlDmTangGiamNq104Repository : IRepository<TlDmTangGiamNq104>
    {
        TlDmTangGiamNq104 FindByMaTangGiam(string maTangGiam);
    }
}
