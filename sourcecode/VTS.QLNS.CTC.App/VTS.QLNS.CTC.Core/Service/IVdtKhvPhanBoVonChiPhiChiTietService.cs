using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IVdtKhvPhanBoVonChiPhiChiTietService
    {
        IEnumerable<VdtKhvPhanBoVonChiPhiChiTiet> FindAll();
        void Add(VdtKhvPhanBoVonChiPhiChiTiet entity);
        void Update(VdtKhvPhanBoVonChiPhiChiTiet entity);
        void Delete(VdtKhvPhanBoVonChiPhiChiTiet entity);
        IEnumerable<VdtKhvPhanBoVonChiPhiChiTietQuery> FindByIdChiPhi(Guid idChiPhi);
    }
}