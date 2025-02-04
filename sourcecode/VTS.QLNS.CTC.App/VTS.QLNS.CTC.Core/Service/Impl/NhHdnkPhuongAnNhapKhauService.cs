using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Repository.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhHdnkPhuongAnNhapKhauService : INhHdnkPhuongAnNhapKhauService
    {
        private readonly INhHdnkPhuongAnNhapKhauRepository _repository;
        private readonly INhDaGoiThauRepository _goiThauRepository;
        private readonly INhDagoiThauNguonVonRepository _nguonVonRepository;
        private readonly INhDaGoiThauChiPhiRepository _chiPhiRepository;
        private readonly INhDaGoiThauHangMucRepository _hangMucRepository;

        public NhHdnkPhuongAnNhapKhauService
        (
            INhHdnkPhuongAnNhapKhauRepository repository,
            INhDaGoiThauRepository goiThauRepository,
            INhDagoiThauNguonVonRepository nguonVonRepository,
            INhDaGoiThauChiPhiRepository chiPhiRepository,
            INhDaGoiThauHangMucRepository hangMucRepository
        )
        {
            _repository = repository;
            _goiThauRepository = goiThauRepository;
            _nguonVonRepository = nguonVonRepository;
            _chiPhiRepository = chiPhiRepository;
            _hangMucRepository = hangMucRepository;
        }

        public void Add(NhHdnkPhuongAnNhapKhau entity)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _repository.Add(entity);
                SaveGoiThau(entity.Id, entity.NhDaGoiThaus);
                transactionScope.Complete();
            }
        }

        public void Update(NhHdnkPhuongAnNhapKhau entity)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _repository.Update(entity);
                SaveGoiThau(entity.Id, entity.NhDaGoiThaus);
                transactionScope.Complete();
            }
        }

        public void Adjust(NhHdnkPhuongAnNhapKhau entity)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                //_repository.Update(entity);
                _repository.Add(entity);
                entity.BIsGoc = false;
                _repository.Update(entity);
                // Update BIsActive = false của bản ghi gốc
                var parentEntity = _repository.Find(entity.IIdParentId);
                if (parentEntity != null)
                {
                    parentEntity.BIsActive = false;
                    _repository.Update(parentEntity);
                }

                SaveGoiThau(entity.Id, entity.NhDaGoiThaus);
                transactionScope.Complete();
            }
        }

        public void Delete(NhHdnkPhuongAnNhapKhau entity)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                var pank = _repository.Find(entity.Id);
                if (pank != null)
                {
                    // Nếu là xóa bản ghi điều chỉnh thì bản ghi gốc sẽ được update bactive = 1
                    if (pank.IIdParentId.HasValue)
                    {
                        var pankParent = _repository.Find(pank.IIdParentId.Value);
                        if (pankParent != null)
                        {
                            pankParent.BIsGoc = true;
                            pankParent.BIsActive = true;
                            _repository.Update(pankParent);
                        }
                    }

                    _repository.Delete(pank);

                    // Xóa chi tiết
                    var listGoiThau = _goiThauRepository.FindAll(s => s.IIdPhuongAnNhapKhauId == pank.Id);
                    if (!listGoiThau.IsEmpty())
                    {
                        DeleteGoiThau(listGoiThau);
                    }

                    transactionScope.Complete();
                }
            }
        }

        private void SaveGoiThau(Guid pankId, IEnumerable<NhDaGoiThau> goiThaus)
        {
            if (goiThaus.IsEmpty()) return;

            var listAdded = goiThaus.Where(s => s.IsAdded && !s.IsDeleted).ToList();
            if (!listAdded.IsEmpty())
            {
                foreach (var item in listAdded)
                {
                    item.IIdPhuongAnNhapKhauId = pankId;
                    SaveNguonVon(item.Id, item.GoiThauNguonVons);
                }
                _goiThauRepository.AddRange(listAdded);
            }

            var listModified = goiThaus.Where(s => s.IsModified && !s.IsAdded && !s.IsDeleted).ToList();
            if (!listModified.IsEmpty())
            {
                foreach (var item in listModified)
                {
                    item.IIdPhuongAnNhapKhauId = pankId;
                    SaveNguonVon(item.Id, item.GoiThauNguonVons);
                }
                _goiThauRepository.UpdateRange(listModified);
            }

            var listDeleted = goiThaus.Where(s => s.IsDeleted).ToList();
            var tempList = _goiThauRepository.FindAll();
            List<NhDaGoiThau> listDel = new List<NhDaGoiThau>();
            foreach (var item in listDeleted)
            {
                if(tempList.Any(x => x.Id == item.Id))
                {
                    listDel.Add(item);
                }
            }
            if (!listDel.IsEmpty())
            {
                DeleteGoiThau(listDel);
            }
        }

        private void SaveNguonVon(Guid goiThauId, IEnumerable<NhDaGoiThauNguonVon> nguonVons)
        {
            if (nguonVons.IsEmpty()) return;

            var listAdded = nguonVons.Where(s => s.IsAdded && !s.IsDeleted).ToList();
            if (!listAdded.IsEmpty())
            {
                foreach (var item in listAdded)
                {
                    item.IIdGoiThauId = goiThauId;
                    SaveChiPhiHangMuc(item.Id, item.GoiThauChiPhis);
                }
                _nguonVonRepository.AddRange(listAdded);
            }

            var listModified = nguonVons.Where(s => s.IsModified && !s.IsAdded && !s.IsDeleted).ToList();
            if (!listModified.IsEmpty())
            {
                foreach (var item in listModified)
                {
                    item.IIdGoiThauId = goiThauId;
                    SaveChiPhiHangMuc(item.Id, item.GoiThauChiPhis);
                }
                _nguonVonRepository.UpdateRange(listModified);
            }

            var listDeleted = nguonVons.Where(s => s.IsDeleted).ToList();
            if (!listDeleted.IsEmpty())
            {
                foreach (var nv in listDeleted)
                {
                    var listDeletedChiPhi = _chiPhiRepository.FindAll(s => s.IIdGoiThauNguonVonId == nv.Id);
                    if (listDeletedChiPhi.Any())
                    {
                        foreach (var cp in listDeletedChiPhi)
                        {
                            var listDeletedHangMuc = _hangMucRepository.FindAll(s => s.IIdGoiThauChiPhiId == cp.Id);

                            _hangMucRepository.RemoveRange(listDeletedHangMuc);
                        }
                        _chiPhiRepository.RemoveRange(listDeletedChiPhi);
                    }
                }
                _nguonVonRepository.RemoveRange(listDeleted);
            }
        }

        private void SaveChiPhiHangMuc(Guid nguonVonId, IEnumerable<NhDaGoiThauChiPhi> chiPhis)
        {
            if (chiPhis.IsEmpty()) return;
            var litChiPhiOld = _chiPhiRepository.FindAll(s => s.IIdGoiThauNguonVonId == nguonVonId);
            if (litChiPhiOld.Any())
            {
                foreach (var item in litChiPhiOld)
                {
                    _chiPhiRepository.Delete(item);
                }              
            }
            _chiPhiRepository.AddRange(chiPhis.Where(c => c.IsDeleted == false));
            IEnumerable<NhDaGoiThauHangMuc> lstHangMucOld;
            foreach (var cp in chiPhis)
            {
                lstHangMucOld = _hangMucRepository.FindAll(s => s.IIdGoiThauChiPhiId == cp.Id);
                if (lstHangMucOld.Any())
                {
                    foreach(var hm in lstHangMucOld)
                    {
                        _hangMucRepository.Delete(hm);
                    }
                }
                _hangMucRepository.AddRange(cp.GoiThauHangMucs.Where(h => h.IsDeleted == false));
            }

            //var listAdded = chiPhis.Where(s => s.IsAdded && !s.IsDeleted).ToList();
            //if (!listAdded.IsEmpty())
            //{
            //    foreach (var item in listAdded)
            //    {
            //        item.IIdGoiThauNguonVonId = nguonVonId;
            //        _hangMucRepository.AddOrUpdate(item.Id, item.GoiThauHangMucs);
            //    }
            //    _chiPhiRepository.AddRange(listAdded);
            //}

                //var listModified = chiPhis.Where(s => s.IsModified && !s.IsAdded && !s.IsDeleted).ToList();
                //if (!listModified.IsEmpty())
                //{
                //    foreach (var item in listModified)
                //    {
                //        item.IIdGoiThauId = nguonVonId;
                //        _hangMucRepository.AddOrUpdate(item.Id, item.GoiThauHangMucs);
                //    }
                //    _chiPhiRepository.UpdateRange(listModified);
                //}

                //var listDeleted = chiPhis.Where(s => s.IsDeleted).ToList();
                //if (!listDeleted.IsEmpty())
                //{
                //    foreach (var cp in listDeleted)
                //    {
                //        var listDeletedHangMuc = _hangMucRepository.FindAll(s => s.IIdGoiThauChiPhiId == cp.Id);
                //        _hangMucRepository.RemoveRange(listDeletedHangMuc);
                //    }
                //    _chiPhiRepository.RemoveRange(listDeleted);
                //}
        }

        private void DeleteGoiThau(IEnumerable<NhDaGoiThau> goiThaus)
        {
            if (goiThaus.Any())
            {
                foreach (var item in goiThaus)
                {
                    var listDeletedNguonVon = _nguonVonRepository.FindAll(s => s.IIdGoiThauId == item.Id);
                    if (listDeletedNguonVon.Any())
                    {
                        foreach (var nv in listDeletedNguonVon)
                        {
                            var listDeletedChiPhi = _chiPhiRepository.FindAll(s => s.IIdGoiThauNguonVonId == nv.Id);
                            if (listDeletedChiPhi.Any())
                            {
                                foreach (var cp in listDeletedChiPhi)
                                {
                                    var listDeletedHangMuc = _hangMucRepository.FindAll(s => s.IIdGoiThauChiPhiId == cp.Id);
                                    _hangMucRepository.RemoveRange(listDeletedHangMuc);
                                }
                                _chiPhiRepository.RemoveRange(listDeletedChiPhi);
                            }
                        }
                        _nguonVonRepository.RemoveRange(listDeletedNguonVon);
                    }
                }
                _goiThauRepository.RemoveRange(goiThaus);
            }
        }

        public IEnumerable<NhHdnkPhuongAnNhapKhau> FindAll()
        {
            return _repository.FindAll();
        }

        public IEnumerable<NhHdnkPhuongAnNhapKhau> FindAll(Expression<Func<NhHdnkPhuongAnNhapKhau, bool>> predicate)
        {
            return _repository.FindAll(predicate);
        }

        public NhHdnkPhuongAnNhapKhau FindById(Guid id)
        {
            return _repository.Find(id);
        }

        public IEnumerable<NhHdnkPhuongAnNhapKhau> FindIndex(int? iLoai = null)
        {
            return _repository.FindIndex(iLoai).OrderByDescending(a => a.DNgayTao);
        }

        public void LockOrUnlock(Guid id, bool status)
        {
            NhHdnkPhuongAnNhapKhau entity = _repository.Find(id);
            if (entity != null)
            {
                entity.BIsKhoa = status;
                _repository.Update(entity);
            }
        }
    }
}
