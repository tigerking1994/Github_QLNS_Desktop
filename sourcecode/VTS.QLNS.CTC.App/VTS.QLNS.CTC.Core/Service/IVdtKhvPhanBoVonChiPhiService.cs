using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IVdtKhvPhanBoVonChiPhiService
    {
        List<VdtKhvPhanBoVonChiPhi> GetVdtKhvPhanBoVonChiPhiInThanhToanChiPhiDialog(string sMaDonVi, Guid iIdDuAnId, int iNamKeHoach);
        IEnumerable<VdtKhvPhanBoVonChiPhi> FindAll();
        VdtKhvPhanBoVonChiPhi FindById(Guid idChiPhi);
        void Delete(VdtKhvPhanBoVonChiPhi chiphi);
        IEnumerable<VdtKhvPhanBoVonChiPhiQuery> FindGiaoDuToanIndex();
        void Add(VdtKhvPhanBoVonChiPhi chiphi);
        void Update(VdtKhvPhanBoVonChiPhi chiphi);
        void Adjust(VdtKhvPhanBoVonChiPhi entity);
        bool LogItem(Guid iId, string sUserLogin);

        public bool IsExistSoQuyetDinh(string soQuyetDinh, Guid id); 
    }
}