using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhQtPheDuyetQuyetToanDAHTService 
    {
        void Add(NhQtPheDuyetQuyetToanDAHT entity);
        void Update(NhQtPheDuyetQuyetToanDAHT entity);
        void Delete(NhQtPheDuyetQuyetToanDAHT entity);
        NhQtPheDuyetQuyetToanDAHT FindById(Guid id);
        IEnumerable<NhQtPheDuyetQuyetToanDAHTQuery> FindIndex(int yearOfWork);
        IEnumerable<NhQtPheDuyetQuyetToanDAHT> FindAll();

        IEnumerable<NhQtPheDuyetQuyetToanDAHTChiTietQuery> GetDataCreate(int iNamBaoCaoTu, int iNamBaoCaoDen, Guid? iID_DonViID,int? slbDonViUSD, int? slbDonViVND);
        IEnumerable<NhQtPheDuyetQuyetToanDAHTChiTietQuery> GetDataDetail(Guid? iIDDonVi, int? devideDonViUSD, int? devideDonViVND, Guid? iDPheDuyetQuyetToan);

        IEnumerable<NhQtPheDuyetQuyetToanDAHTChiTietQuery> GetDataBaoCaoKetLuanDetail(Guid? iIDDonVi, int? devideDonViUSD, int? devideDonViVND, DateTime? dNgayPheDuyetTu, DateTime? dNgayPheDuyetDen);

        void AddOrUpdate(IEnumerable<NhQtPheDuyetQuyetToanDAHTChiTiet> entities);

        IEnumerable<NhTtThucHienNganSachGiaiDoanQuery> GetGiaiDoan();

    }
}
