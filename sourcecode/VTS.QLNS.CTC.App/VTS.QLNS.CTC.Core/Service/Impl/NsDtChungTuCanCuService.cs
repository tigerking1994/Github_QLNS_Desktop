using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NsDtChungTuCanCuService : INsDtChungTuCanCuService
    {
        private readonly INsDtChungTuCanCuRepository _repository;

        public NsDtChungTuCanCuService(INsDtChungTuCanCuRepository repository)
        {
            _repository = repository;
        }

        public int DeleteByIdChungTuDuToan(Guid id)
        {
            var predicate = PredicateBuilder.True<NsDtChungTuCanCu>();
            predicate = predicate.And(x => x.IIdCtduToan == id);
            List<NsDtChungTuCanCu> entities = _repository.FindAll(predicate).ToList();
            return _repository.RemoveRange(entities);
        }

        public int Update(Guid idChungTu, List<Guid> idsChungTuPhanBo, string userModified)
        {
            // Delete all record of DuToan
            var predicate = PredicateBuilder.True<NsDtChungTuCanCu>();
            predicate = predicate.And(x => x.IIdCtduToan == idChungTu);
            List<NsDtChungTuCanCu> entities = _repository.FindAll(predicate).ToList();
            _repository.RemoveRange(entities);

            // Update
            entities = new List<NsDtChungTuCanCu>();
            foreach (var item in idsChungTuPhanBo)
            {
                entities.Add(new NsDtChungTuCanCu { IIdCtduToan = idChungTu, IIdCtnsnganh = item, SNguoiTao = userModified, DNgayTao = DateTime.Now });
            }
            return _repository.AddRange(entities);
        }

        public IEnumerable<NsDtChungTuCanCu> FindAll(Expression<Func<NsDtChungTuCanCu, bool>> predicate)
        {
            return _repository.FindAll(predicate); 
        }
    }
}
