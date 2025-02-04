using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IImpSoKiemTraService
    {
        void AddRange(List<ImpSoKiemTra> duToans);
    }
}
