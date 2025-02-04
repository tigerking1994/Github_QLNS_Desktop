using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Repository.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class BhBaoCaoGhiChuService : IService<BhCauHinhBaoCao>, IBhBaoCaoGhiChuService
    {
        private readonly IBhBaoCaoGhiChuRepository _repository;

        public BhBaoCaoGhiChuService(IBhBaoCaoGhiChuRepository repository)
        {
            _repository = repository;
        }
        public void Add(BhCauHinhBaoCao dmGhiChu)
        {
            _repository.Add(dmGhiChu);
        }

        public IEnumerable<BhCauHinhBaoCao> FindByCondition(Expression<Func<BhCauHinhBaoCao, bool>> predicate)
        {
            return _repository.FindAll(predicate);
        }

        public BhCauHinhBaoCao FindById(Guid id)
        {
            return _repository.SingleOrDefault(t => t.Id.Equals(id));
        }

        public void Save(BhCauHinhBaoCao dmGhiChu)
        {
            throw new NotImplementedException();
        }

        public void UpdateRange(IEnumerable<BhCauHinhBaoCao> dmGhiChus)
        {
            _repository.UpdateRange(dmGhiChus);
        }

        public void AddOrUpdateRange(IEnumerable<BhCauHinhBaoCao> listEntities)
        {
            _repository.AddOrUpdateRange(listEntities);
        }

        public override IEnumerable<BhCauHinhBaoCao> FindAll(AuthenticationInfo authenticationInfo)
        {
            return _repository.FindAll();
        }

        public void AddReportConfig(Dictionary<string, object> data, string idType, int iNamLamViec, string idMaDonVi = null)
        {
            BhCauHinhBaoCao bhGhiChu;
            if (string.IsNullOrEmpty(idMaDonVi))
            {
                bhGhiChu = FindByCondition(x => x.SMaBaoCao.Equals(idType) && x.INamLamViec == iNamLamViec && x.ILoaiBaoCao.Equals((int)NoteTypeBhxh.AgencySummary)).FirstOrDefault();
            }
            else
            {
                bhGhiChu = FindByCondition(x => x.SMaBaoCao.Equals(idType) && x.INamLamViec == iNamLamViec && x.ILoaiBaoCao.Equals((int)NoteTypeBhxh.AgencyDetail) && x.IIdMaDonVi.Equals(idMaDonVi)).FirstOrDefault();
            }
            if (!data.ContainsKey("GhiChu")) data.Add("GhiChu", bhGhiChu == null ? string.Empty : bhGhiChu.SGhiChu);
            if (!data.ContainsKey("CanCu1")) data.Add("CanCu1", bhGhiChu == null ? string.Empty : bhGhiChu.SCanCu1);
            if (!data.ContainsKey("CanCu2")) data.Add("CanCu2", bhGhiChu == null ? string.Empty : bhGhiChu.SCanCu2);
            if (bhGhiChu != null && !string.IsNullOrEmpty(bhGhiChu.SGhiChu))
            {
                data.Add("ShowNote", true);
            }
        }
    }
}
