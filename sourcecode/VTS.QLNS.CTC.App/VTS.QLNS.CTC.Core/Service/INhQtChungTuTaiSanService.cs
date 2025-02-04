using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhQtChungTuTaiSanService
    {
        IEnumerable<NhQtChungTuTaiSan> FindAll();
        NhQtChungTuTaiSan FindById(Guid? id);
        void Update(NhQtChungTuTaiSan nhQtChungTuTaiSan);
        void Add(NhQtChungTuTaiSan nhQtChungTuTaiSan);
        void Delete(Guid id);
    }
}
