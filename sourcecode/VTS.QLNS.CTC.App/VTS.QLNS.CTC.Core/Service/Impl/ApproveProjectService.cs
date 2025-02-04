using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Transactions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class ApproveProjectService : IApproveProjectService
    {
        private readonly IApproveProjectRepository _repository;
        private readonly IVDTDuAnRepository _vdtDuAnRepository;
        private readonly IVdtQddtChiPhiRepository _vdtQddtChiPhiRepository;
        private readonly IVdtQddtNguonVonRepository _vdtQddtNguonVonRepository;
        private readonly IVdtQddtHangMucRepository _vdtQddtHangMucRepository;
        private readonly IVdtDaHangMucRepository _vdtDaHangMucRepository;
        private readonly IVdtDaQDDauTuDMHangMucRepository _vdtDaQDDauTuDMHangMucRepository;
        private readonly IVdtDmDuAnChiPhiRepository _vdtDmDuAnChiPhiRepository;

        public ApproveProjectService(
            IApproveProjectRepository repository,
            IVDTDuAnRepository vDTDuAnRepository,
            IVdtQddtChiPhiRepository vdtQddtChiPhiRepository,
            IVdtQddtNguonVonRepository vdtQddtNguonVonRepository,
            IVdtQddtHangMucRepository vdtQddtHangMucRepository,
            IVdtDaHangMucRepository vdtDaHangMucRepository,
            IVdtDaQDDauTuDMHangMucRepository vdtDaQDDauTuDMHangMucRepository,
            IVdtDmDuAnChiPhiRepository vdtDmDuAnChiPhiRepository)
        {
            _repository = repository;
            _vdtDuAnRepository = vDTDuAnRepository;
            _vdtQddtChiPhiRepository = vdtQddtChiPhiRepository;
            _vdtQddtNguonVonRepository = vdtQddtNguonVonRepository;
            _vdtQddtHangMucRepository = vdtQddtHangMucRepository;
            _vdtDaHangMucRepository = vdtDaHangMucRepository;
            _vdtDaQDDauTuDMHangMucRepository = vdtDaQDDauTuDMHangMucRepository;
            _vdtDmDuAnChiPhiRepository = vdtDmDuAnChiPhiRepository;
        }

        public IEnumerable<ApproveProjectQuery> FindByCondition(int namLamViec, int nguonNganSach, string donviUserId)
        {
            return _repository.FindByCondition(namLamViec, nguonNganSach, donviUserId);
        }

        public VdtDaQddauTu FindById(Guid id)
        {
            return _repository.Find(id);
        }

        public VdtDaQddauTu FindQDDaTuDieuChinhByDuAn(Guid id, Guid duAnId)
        {
            return _repository.FindQDDaTuDieuChinhByDuAn(id, duAnId);
        }

        public IEnumerable<VdtDaDuAn> FindDuAnByDonVi(string donviQLId)
        {
            return _repository.FindDuAnByDonVi(donviQLId);
        }

        public IEnumerable<VdtDmNhomDuAn> GetAllNhomDuAn()
        {
            return _repository.GetAllNhomDuAn();
        }

        public IEnumerable<VdtDmHinhThucQuanLy> GetAllHinhThucQuanLy()
        {
            return _repository.GetAllHinhThucQuanLy();
        }

        public int SaveDataQDDauTu(VdtDaQddauTu entity, List<VdtDaQddauTuNguonVon> nguonVons, List<VdtDaQddauTuChiPhi> chiphis, List<VdtDaQddauTuHangMuc> hangmucs)
        {
            using (var transactionScope = new TransactionScope(
                 TransactionScopeOption.Required,
                 new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
            {
                try
                {
                    // 1. Add thong tin phe duyet du an
                    _repository.Add(entity);

                    // 2. Add detail: Chi phí, hạng mục, nguồn vốn
                    // Nguon von
                    List<VdtDaQddauTuNguonVon> listNguonVonAdd = nguonVons.Where(x => (x.Id == null || x.Id == Guid.Empty) && !x.IsDeleted).ToList();
                    List<VdtDaQddauTuNguonVon> listNguonVonEdit = nguonVons.Where(x => x.IsModified && x.Id != null && x.Id != Guid.Empty && !x.IsDeleted).ToList();
                    List<VdtDaQddauTuNguonVon> listNguonVonDelete = nguonVons.Where(x => x.IsDeleted && x.Id != null && x.Id != Guid.Empty).ToList();

                    if (listNguonVonAdd.Count > 0)
                    {
                        foreach (var item in listNguonVonAdd)
                        {
                            item.IIdQddauTuId = entity.Id;
                        }

                        AddRangeNguonVon(listNguonVonAdd);
                    }

                    if (listNguonVonEdit.Count > 0)
                    {
                        foreach (var item in listNguonVonEdit)
                        {
                            VdtDaQddauTuNguonVon quyetDinhNV = FindNguonVon(item.Id);
                            if (quyetDinhNV != null)
                            {
                                UpdateNguonVon(quyetDinhNV);
                            }
                        }
                    }

                    if (listNguonVonDelete.Count > 0)
                    {
                        foreach (var item in listNguonVonDelete)
                        {
                            DeleteNguonVon(item.Id);
                        }
                    }

                    // Chi phi

                    //var lstDelete = DataQDDauTuChiPhi.Where(n => n.IsDeleted);
                    var listChiPhiAdd = chiphis.Where(x => (x.Id == null || x.Id == Guid.Empty) && !x.IsDeleted && x.FTienPheDuyet > 0).ToList();
                    var listChiPhiEdit = chiphis.Where(x => x.IsModified && x.IdQDChiPhi != null && x.IdQDChiPhi != Guid.Empty && !x.IsDeleted && x.FTienPheDuyet > 0).ToList();
                    List<VdtDaQddauTuChiPhi> listChiPhiDelete = chiphis.Where(x => x.IsDeleted && x.IdQDChiPhi != null && x.IdQDChiPhi != Guid.Empty).ToList();


                    if (listChiPhiAdd.Count > 0)
                    {
                        List<VdtDmDuAnChiPhi> listDuAnChiPhiAdd = new List<VdtDmDuAnChiPhi>();
                        foreach (var item in listChiPhiAdd)
                        {
                            item.IIdQddauTuId = entity.Id;
                            listDuAnChiPhiAdd.Add(new VdtDmDuAnChiPhi
                            {
                                Id = item.IIdDuAnChiPhi.Value,
                                IIdChiPhi = item.IIdChiPhiId,
                                IIdChiPhiParent = item.IIdChiPhiParent,
                                STenChiPhi = item.STenChiPhi,
                                SMaChiPhi = item.SMaChiPhi,
                                IThuTu = item.IThuTu
                            });
                        }
                        // add vào bảng Vdt_Dm_DuAn_ChiPhi

                        AddRangeDMDuAnChiPhi(listDuAnChiPhiAdd);

                        #region add vào bảng VDt_DA_QDDauTu_Chi phi
                        //List<VdtDaQddtChiPhiModel> listQDChiPhiAdd = listAdd.Where(x => x.IsEditHangMuc).ToList();
                        if (listChiPhiAdd != null && listChiPhiAdd.Count > 0)
                        {
                            AddRangeChiPhi(listChiPhiAdd);
                        }

                        if (listChiPhiDelete.Count > 0)
                        {
                            foreach (var item in listChiPhiDelete)
                            {
                                DeleteChiPhi(item.IdQDChiPhi.Value);
                            }
                        }

                        #endregion
                    }

                    //update
                    if (listChiPhiEdit.Count > 0)
                    {
                        foreach (var item in listChiPhiEdit)
                        {
                            // sửa VDT_DM_Duan_Chiphi
                            VdtDmDuAnChiPhi duAnChiPhi = FindDMDuAnChiPhi(item.IIdDuAnChiPhi);
                            if (duAnChiPhi != null)
                            {
                                duAnChiPhi.STenChiPhi = item.STenChiPhi;
                                duAnChiPhi.IIdChiPhiParent = item.IIdChiPhiParent;
                                UpdateVdtDmDuAnChiPhi(duAnChiPhi);
                            }

                            VdtDaQddauTuChiPhi qDChiPhi = FindChiPhi(item.IdQDChiPhi);
                            if (qDChiPhi != null)
                            {
                                qDChiPhi.FTienPheDuyet = item.FTienPheDuyet;
                                UpdateChiPhi(qDChiPhi);
                            }
                        }
                    }

                    //delete QDDauTuChiPhi
                    if (listChiPhiDelete.Count > 0)
                    {
                        foreach (var item in listChiPhiDelete)
                        {
                            DeleteChiPhi(item.IdQDChiPhi.Value);
                        }
                    }

                    // Hang muc
                    List<VdtDaQddauTuHangMuc> listDMHangMucAdd = hangmucs.Where(x => !x.IsDeleted && (x.Id == Guid.Empty || x.Id == null) && x.FTienPheDuyet > 0).ToList();
                    List<VdtDaQddauTuHangMuc> listQDHangMucAdd = hangmucs.Where(x => x.IsModified && !x.IsDeleted && (x.Id == Guid.Empty || x.Id == null) && x.FTienPheDuyet > 0).ToList();
                    List<VdtDaQddauTuHangMuc> listDMHangMucEdit = hangmucs.Where(x => x.IsModified && !x.IsDeleted && x.Id != Guid.Empty && x.Id != null).ToList();
                    List<VdtDaQddauTuHangMuc> listQDHangMucEdit = hangmucs.Where(x => x.IsModified && !x.IsDeleted && x.Id != Guid.Empty && x.Id != null).ToList();
                    List<VdtDaQddauTuHangMuc> listDetailDelete = hangmucs.Where(x => x.IsDeleted && x.Id != Guid.Empty && x.Id != null).ToList();

                    transactionScope.Complete();
                }
                catch (Exception)
                {
                    return 0;
                }

            }
            return 1;
        }

        public VdtDaDuAn FindDuAnById(Guid idDuAn)
        {
            return _repository.FindDuAnById(idDuAn);
        }

        public IEnumerable<VdtDmChiPhi> GetAllDmChiPhi()
        {
            return _repository.GetAllDmChiPhi();
        }

        public IEnumerable<NsNguonNganSach> GetAllNguonNS()
        {
            return _repository.GetAllNguonNS();
        }

        public IEnumerable<ApproveProjectDetailQuery> FindListDetail(Guid quyetDinhDauTuId, Guid duAnId, Guid? duAnChiPhiId)
        {
            return _repository.FindListDetail(quyetDinhDauTuId, duAnId, duAnChiPhiId);
        }

        public int AddRangeChiPhi(IEnumerable<VdtDaQddauTuChiPhi> entities)
        {
            return _vdtQddtChiPhiRepository.AddRange(entities);
        }

        public int AddRangeNguonVon(IEnumerable<VdtDaQddauTuNguonVon> entities)
        {
            return _vdtQddtNguonVonRepository.AddRange(entities);
        }

        public int Add(VdtDaQddauTuNguonVon entities)
        {
            return _vdtQddtNguonVonRepository.Add(entities);
        }

        public int AddRangeHangMuc(IEnumerable<VdtDaQddauTuHangMuc> entities)
        {
            return _vdtQddtHangMucRepository.AddRange(entities);
        }

        public int AddRangeDuAnHangMuc(IEnumerable<VdtDaDuAnHangMuc> entities)
        {
            return _vdtDaHangMucRepository.AddRange(entities);
        }

        public VdtDaQddauTuChiPhi FindChiPhi(params object[] keyValues)
        {
            return _vdtQddtChiPhiRepository.Find(keyValues);
        }

        public VdtDaQddauTuNguonVon FindNguonVon(params object[] keyValues)
        {
            return _vdtQddtNguonVonRepository.Find(keyValues);
        }

        public VdtDaDuAnHangMuc FindDuAnHangMuc(params object[] keyValues)
        {
            return _vdtDaHangMucRepository.Find(keyValues);
        }

        public VdtDaQddauTuHangMuc FindQDDTHangMuc(params object[] keyValues)
        {
            return _vdtQddtHangMucRepository.Find(keyValues);
        }

        public int UpdateChiPhi(VdtDaQddauTuChiPhi entity)
        {
            return _vdtQddtChiPhiRepository.Update(entity);
        }

        public int UpdateNguonVon(VdtDaQddauTuNguonVon entity)
        {
            return _vdtQddtNguonVonRepository.Update(entity);
        }

        public int UpdateHangMuc(VdtDaDuAnHangMuc entity)
        {
            return _vdtDaHangMucRepository.Update(entity);
        }

        public int UpdateQDDTHangMuc(VdtDaQddauTuHangMuc entity)
        {
            return _vdtQddtHangMucRepository.Update(entity);
        }

        public int UpdateVdtDuAn(VdtDaDuAn entity)
        {
            return _vdtDuAnRepository.Update(entity);
        }

        public int Update(VdtDaQddauTu entity)
        {
            return _repository.Update(entity);
        }

        public int DeleteChiPhi(Guid id)
        {
            VdtDaQddauTuChiPhi entity = FindChiPhi(id);
            if (entity != null)
            {
                return _vdtQddtChiPhiRepository.Delete(entity);
            }
            return 0;
        }

        public int DeleteNguonVon(Guid id)
        {
            VdtDaQddauTuNguonVon entity = FindNguonVon(id);
            if (entity != null)
            {
                return _vdtQddtNguonVonRepository.Delete(entity);
            }
            return 0;
        }

        public int DeleteQDDTHangMuc(Guid id)
        {
            VdtDaQddauTuHangMuc qdHangMuc = FindQDDTHangMuc(id);
            if (qdHangMuc != null)
            {
                return _vdtQddtHangMucRepository.Delete(qdHangMuc);
            }
            return 0;
        }

        public void DeleteQDDauTuChiTiet(Guid id, Guid? parentId)
        {
            _repository.DeleteQDDauTuChiTiet(id, parentId);
        }

        public bool CheckDuplicateSoQD(string soQuyetDinh, Guid id)
        {
            return _repository.CheckDuplicateSoQD(soQuyetDinh, id);
        }

        public VdtDaQddauTu FindByDuAnId(Guid duAnId)
        {
            return _repository.FindByDuAnId(duAnId);
        }

        public int DeleteDuAnHangMucByDuAnId(Guid id)
        {
            return _vdtDaHangMucRepository.DeleteDuAnHangMucById(id);
        }

        public int AddRangeQdDauTuDMHangMuc(IEnumerable<VdtDaQddauTuDmHangMuc> entities)
        {
            return _vdtDaQDDauTuDMHangMucRepository.AddRange(entities);
        }

        public IEnumerable<VdtDaQDNguonVonQuery> FindListQDDauTuNguonVon(Guid qdDauTuId)
        {
            return _repository.FindListQDDauTuNguonVon(qdDauTuId);
        }

        public IEnumerable<VdtDaQddtChiPhiQuery> FindListQDDauTuChiPhi(Guid qdDauTuId)
        {
            return _repository.FindListQDDauTuChiPhi(qdDauTuId);
        }

        public IEnumerable<VdtDaQDNguonVonQuery> FindListQDDauTuNguonVonByDuAn(Guid duAnId)
        {
            return _repository.FindListQDDauTuNguonVonByDuAn(duAnId);
        }

        public int AddRangeDMDuAnChiPhi(IEnumerable<VdtDmDuAnChiPhi> entities)
        {
            return _vdtDmDuAnChiPhiRepository.AddRange(entities);
        }

        public VdtDmDuAnChiPhi FindDMDuAnChiPhi(params object[] keyValues)
        {
            return _vdtDmDuAnChiPhiRepository.Find(keyValues);
        }

        public int UpdateVdtDmDuAnChiPhi(VdtDmDuAnChiPhi entity)
        {
            return _vdtDmDuAnChiPhiRepository.Update(entity);
        }

        public IEnumerable<VdtDaQddtChiPhiQuery> FindListAllLoaiChiPhi()
        {
            return _repository.FindListAllLoaiChiPhi();
        }

        public bool CheckExistInQDHangMuc(Guid qdDauTuId, Guid danhMucDuAnChiPhiId)
        {
            return _repository.CheckExistInQDHangMuc(qdDauTuId, danhMucDuAnChiPhiId);
        }


        public IEnumerable<ApproveProjectDetailQuery> FindListDetailBeforeSave(Guid duAnId, Guid quyetDinhDauTuId)
        {
            return _repository.FindListDetailBeforeSave(duAnId, quyetDinhDauTuId);
        }

        public VdtDaQddauTuDmHangMuc FindDanhMucHangMuc(params object[] keyValues)
        {
            return _vdtDaQDDauTuDMHangMucRepository.Find(keyValues);
        }

        public int UpdateVDTDanhMucHangMuc(VdtDaQddauTuDmHangMuc entity)
        {
            return _vdtDaQDDauTuDMHangMucRepository.Update(entity);
        }

        public IEnumerable<VdtDaQDNguonVonQuery> FindListQDDauTuNguonVonDieuChinh(Guid qdDauTuId)
        {
            return _repository.FindListQDDauTuNguonVonDieuChinh(qdDauTuId);
        }

        public IEnumerable<VdtDaQddtChiPhiQuery> FindListQDDauTuChiPhiDieuChinhDefault(Guid qdDauTuId)
        {
            return _repository.FindListQDDauTuChiPhiDieuChinhDefault(qdDauTuId);
        }

        public IEnumerable<ApproveProjectDetailQuery> FindListAllHangMucByQDDauTu(Guid quyetDinhDauTuId)
        {
            return _repository.FindListAllHangMucByQDDauTu(quyetDinhDauTuId);
        }

        public IEnumerable<ApproveProjectQuery> FindListQDDauTuByDuAnId(Guid duAnId)
        {
            return _repository.FindListQDDauTuByDuAnId(duAnId);
        }

        public IEnumerable<VdtDaQDNguonVonQuery> FindListQDDauTuNguonVonDieuChinhUpdate(Guid qdDauTuId)
        {
            return _repository.FindListQDDauTuNguonVonDieuChinhUpdate(qdDauTuId);
        }

        public IEnumerable<VdtDaQddtChiPhiQuery> FindListQDDauTuChiPhiDieuChinhUpdate(Guid qdDauTuId)
        {
            return _repository.FindListQDDauTuChiPhiDieuChinhUpdate(qdDauTuId);
        }

        public IEnumerable<ApproveProjectDetailQuery> FindListHangMucDieuChinhAdd(Guid quyetDinhDauTuId, Guid duAnChiPhiId)
        {
            return _repository.FindListHangMucDieuChinhAdd(quyetDinhDauTuId, duAnChiPhiId);
        }

        public bool CheckChiPhiCoHangMuc(Guid chiPhiId)
        {
            return _repository.CheckChiPhiCoHangMuc(chiPhiId);
        }

        public VdtDmDuAnChiPhi FindByNameDuAnChiPhi(string name)
        {
            return _vdtDmDuAnChiPhiRepository.FindByName(name);
        }

        public VdtDmDuAnChiPhi FindByMaDuAnChiPhi(string ma)
        {
            return _vdtDmDuAnChiPhiRepository.FindByMaChiPhi(ma);
        }

        public int Add(VdtDaQddauTu entity)
        {
            return _repository.Add(entity);
        }

        public IEnumerable<ApproveProjectDetailQuery> FindListHangMucByQDDauTu(Guid quyetDinhDauTuId)
        {
            return _repository.FindListHangMucByQDDauTu(quyetDinhDauTuId);
        }

        public IEnumerable<ApproveProjectDetailQuery> FindListHangMucDieuChinhByQDDauTuAdd(Guid quyetDinhDauTuId)
        {
            return _repository.FindListHangMucDieuChinhByQDDauTuAdd(quyetDinhDauTuId);
        }

        public IEnumerable<ApproveProjectDetailQuery> FindListHangMucDieuChinhByQDDauTuUpdate(Guid quyetDinhDauTuId)
        {
            return _repository.FindListHangMucDieuChinhByQDDauTuUpdate(quyetDinhDauTuId);
        }

        public IEnumerable<VdtDaDuAn> FindDuAnByMaDonVi(string sMaDonVi)
        {
            return _repository.FindDuAnByMaDonVi(sMaDonVi);
        }

        public void LockOrUnlock(Guid id, bool isLock)
        {
            VdtDaQddauTu chungTu = _repository.Find(id);
            chungTu.BKhoa = isLock;
            _repository.Update(chungTu);
        }

        public IEnumerable<VdtDaQddauTu> FindByCondition(Expression<Func<VdtDaQddauTu, bool>> predicate)
        {
            return _repository.FindAll(predicate);
        }

        public IEnumerable<VdtDaQddauTuNguonVon> FindNguonVonByCondition(Expression<Func<VdtDaQddauTuNguonVon, bool>> predicate)
        {
            return _vdtQddtNguonVonRepository.FindAll(predicate);
        }
    }
}
