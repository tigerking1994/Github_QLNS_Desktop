using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtDaHopDongGoiThauNhaThauRepository : IRepository<VdtDaHopDongGoiThauNhaThau>
    {
        void DeleteHopDongDetail(Guid iIdHopDongId);
        void DeleteHopDongGoiThauNhaThau(List<Guid> listGoiThauNhaThauId);
        IEnumerable<VdtDaHopDongGoiThauNhaThau> ListGoiThauNhaThauByGoiThauId(Guid goiThauId);
        double CalculateTotalUsedValueOfGoiThau(Guid GoiThauIds, Guid hopDongId);
        double CalculateTotalUsedValueOfChiPhi(Guid chiphiId, Guid hopDongId);
        void SaveHopDong(VdtDaTtHopDong vdtDaTtHopDong);
        void SaveHopDongDC(VdtDaTtHopDong vdtDaTtHopDongDC, VdtDaTtHopDong vdtDaTtHopDongGoc);
    }
}
