using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhDmHinhThucChonNhaThauService
    {
        IEnumerable<NhDmHinhThucChonNhaThau> FindAll();
        NhDmHinhThucChonNhaThau FindByMaHinhThuc(string sMaHinhThuc);
        bool IsDuplicateHinhThucChonNhaThau(string sMaHinhThuc, Guid Id);
    }
}
