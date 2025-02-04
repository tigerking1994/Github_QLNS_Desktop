using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhDaQdDauTuService
    {
        void Add(NhDaQdDauTu entity);
        void AddRange(IEnumerable<NhDaQdDauTu> data);

        void Update(NhDaQdDauTu entity);
        void Adjust(NhDaQdDauTu entity);
        void Delete(NhDaQdDauTu entity);
        void LockOrUnlock(Guid id, bool status);
        void SaveQdDauTuNguonVon(Guid qdDauTuId, IEnumerable<NhDaQdDauTuNguonVon> items);
        void SaveQdDauTuChiPhi(Guid IdNguonVon, Guid IdQdDauTuId, IEnumerable<NhDaQdDauTuChiPhi> items);
        NhDaQdDauTu FindById(Guid id);
        NhDaQdDauTu FindByDuAnId(Guid duAnId);
        IEnumerable<NhDaQdDauTuQuery> FindIndex(int yearOfWork, int iLoai);
        IEnumerable<NhDaQdDauTu> FindAll();
        IEnumerable<NhDaQdDauTu> FindAll(Expression<Func<NhDaQdDauTu, bool>> predicate);
        IEnumerable<NhDaDetailNguonVonQuery> GetNguonVonByQdDauTuId(Guid iIdQdDauTuId);
        IEnumerable<NhDaDetailChiPhiQuery> GetChiPhiByQdDauTuId(Guid iIdQdDauTuId);
        IEnumerable<NhDaDetailHangMucQuery> GetHangMucByQdDauTuId(Guid iIdQdDauTuId);
        List<NhDaQdDauTu> FindListByDuAnId(Guid duAnId);
        bool CheckDuplicateSoQD(string soQuyetDinh, Guid id);
    } 
}
