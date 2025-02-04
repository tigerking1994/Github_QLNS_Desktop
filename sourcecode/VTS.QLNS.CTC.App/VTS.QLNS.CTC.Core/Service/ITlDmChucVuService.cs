using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlDmChucVuService
    {
        IEnumerable<TlDmChucVu> FindAll();
        TlDmChucVu FindByMaChucVu(string maChucVu);
        TlDmChucVu FindByHeSoChucVu(decimal? heSoCv);
    }
}
