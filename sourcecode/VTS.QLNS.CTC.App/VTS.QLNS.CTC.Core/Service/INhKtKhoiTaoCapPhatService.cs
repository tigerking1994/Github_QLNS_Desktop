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
    public interface INhKtKhoiTaoCapPhatService
    {
        void Add(NhKtKhoiTaoCapPhat entity);
        void Update(NhKtKhoiTaoCapPhat entity);
        void Delete(NhKtKhoiTaoCapPhat entity);
        void DeleteKhoiTaoTheoQuyetDinh(Guid idKhoiTao, int type);
        NhKtKhoiTaoCapPhat FindById(Guid id);
        IEnumerable<NhKtKhoiTaoCapPhatQuery> GetAll(int iNamLamViec);
        IEnumerable<NhKtKhoiTaoCapPhat> FindAll();
    }
}
