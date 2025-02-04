using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITTnDanhMucLoaiHinhService
    {
        IEnumerable<TnDanhMucLoaiHinh> FindByLoaiHinh(int yearOfWork, int iTrangThai);
        void AddRange(List<TnDanhMucLoaiHinh> listMlns);
    }
}
