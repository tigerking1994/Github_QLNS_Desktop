using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INhQtPheDuyetQuyetToanDAHTRepository : IRepository<NhQtPheDuyetQuyetToanDAHT>
    {
        IEnumerable<NhQtPheDuyetQuyetToanDAHTQuery> FindIndex(int yearOfWork);
        IEnumerable<NhQtPheDuyetQuyetToanDAHTChiTietQuery> GetDataCreate(int iNamBaoCaoTu, int iNamBaoCaoDen, Guid? iID_DonViID, int? slbDonViUSD, int? slbDonViVND);
        IEnumerable<NhQtPheDuyetQuyetToanDAHTChiTietQuery> GetDataDetail(Guid? iIDDonVi, int? devideDonViUSD, int? devideDonViVND, Guid? iDPheDuyetQuyetToan);

        IEnumerable<NhQtPheDuyetQuyetToanDAHTChiTietQuery> GetDataBaoCaoKetLuanDetail(Guid? iIDDonVi, int? devideDonViUSD, int? devideDonViVND, DateTime? dNgayPheDuyetTu, DateTime? dNgayPheDuyetDen);
        IEnumerable<NhTtThucHienNganSachGiaiDoanQuery> GetGiaiDoan();

    }
}
