using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Repository.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhDaDuAnService : IService<NhDaDuAn>, INhDaDuAnService
    {
        private readonly INhDaDuAnRepository _repository;
        private readonly INhDaDuAnNguonVonRepository _nhDaDuAnNguonVonRepository;
        private readonly INhDaDuAnHangMucRepository _nhDaDuAnHangMucRepository;

        public NhDaDuAnService(
            INhDaDuAnRepository repository,
            INhDaDuAnNguonVonRepository nhDaDuAnNguonVonRepository,
            INhDaDuAnHangMucRepository nhDaDuAnHangMucRepository)
        {
            _repository = repository;
            _nhDaDuAnNguonVonRepository = nhDaDuAnNguonVonRepository;
            _nhDaDuAnHangMucRepository = nhDaDuAnHangMucRepository;
        }

        public void AddRange(IEnumerable<NhDaDuAn> data)
        {
            _repository.AddRange(data);
        }
        public void Add(NhDaDuAn entity)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _repository.Add(entity);
                _nhDaDuAnNguonVonRepository.AddOrUpdate(entity.Id, entity.DuAnNguonVons);
                _nhDaDuAnHangMucRepository.AddOrUpdate(entity.Id, entity.DuAnHangMucs);

                transactionScope.Complete();
            }
        }

        public void Update(NhDaDuAn entity)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _repository.Update(entity);
                _nhDaDuAnNguonVonRepository.AddOrUpdate(entity.Id, entity.DuAnNguonVons);
                _nhDaDuAnHangMucRepository.AddOrUpdate(entity.Id, entity.DuAnHangMucs);

                transactionScope.Complete();
            }
        }

        public void Delete(NhDaDuAn entity)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _repository.Delete(entity);
                _nhDaDuAnNguonVonRepository.DeleteByDuAnId(entity.Id);
                _nhDaDuAnHangMucRepository.DeleteByDuAnId(entity.Id);

                transactionScope.Complete();
            }
        }
		
		public NhDaDuAn FindById(Guid Id)
        {
            return _repository.Find(Id);
        }

        public IEnumerable<NhDaDuAn> FindAll()
        {
            return _repository.FindAll();
        }

        public IEnumerable<NhDaDuAn> FindAll(Expression<Func<NhDaDuAn, bool>> predicate)
        {
            return _repository.FindAll(predicate);
        }

        public IEnumerable<NhDaDuAnQuery> FindIndex(int iLoai)
        {
            return _repository.FindIndex(iLoai);
        }

        public IEnumerable<NhDaDuAn> FindByDonVi(Guid idDonVi)
        {
            return _repository.FindAll(x => x.IIdDonViQuanLyId == idDonVi);
        }

        public IEnumerable<NhDaDuAnQuery> FindFromChuTruongDauTu(int yearOfWork, string maDonVi, Guid? chuTruongDauTuId = null)
        {
            return _repository.FindFromChuTruongDauTu(yearOfWork, maDonVi, chuTruongDauTuId);
        }

        public IEnumerable<NhDaDuAnQuery> FindFromQdDauTu(int yearOfWork, string maDonVi, int iLoai, Guid? qdDauTuId = null)
        {
            return _repository.FindFromQdDauTu(yearOfWork, maDonVi, iLoai, qdDauTuId);
        }

        public IEnumerable<NhDaDuAnQuery> FindFromDuToan(int yearOfWork, string maDonVi, Guid? qdDauTuId = null)
        {
            return _repository.FindFromDuToan(yearOfWork, maDonVi, qdDauTuId);
        }

        public IEnumerable<NhDaDuAnTrongNuocQuery> FindDuAnTrongNuoc()
        {
            return _repository.FindDuAnTrongNuoc();
        }

        public IEnumerable<NhDaDuAnTinhHinhDuAnQuery> GetInfoDuAnTinhHinhDuAnReport(int yearOfWork, string maDonVi)
        {
            return _repository.GetInfoDuAnTinhHinhDuAnReport(yearOfWork, maDonVi);
        }

        public IEnumerable<NganSachNhDuAnInfoQuery> FindNganSachNgoaiHoiDuAnInfoByIdDuAn(string idDuAn)
        {
            return _repository.FindNganSachNgoaiHoiDuAnInfoByIdDuAn(idDuAn);
        }

        public IEnumerable<NhBaoCaoTinhHinhThucHienDuAnQuery> GetDataReportTinhHinhThucHienDuAn(string idDuAn,
            DateTime? ngayBatDau, DateTime? ngayKetThuc, string idHopDong, string idKhTongThe)
        {
            return _repository.GetDataReportTinhHinhThucHienDuAn(idDuAn, ngayBatDau, ngayKetThuc, idHopDong, idKhTongThe);
        }

        public IEnumerable<ReportNHTongHopThongTinDuAnQuery> FindByAggregateProjectInformationReport(AggregateProjectInformationReportCriteria searchCondition)
        {
            return _repository.FindByAggregateProjectInformationReport(searchCondition);
        }
        
        public IEnumerable<NhDaDuAn> FindAllDuAnByQDDT()
        {
            return _repository.FindAllDuAnByQDDT();
        }
        public override IEnumerable<NhDaDuAn> FindAll(AuthenticationInfo authenticationInfo)
        {
            return _repository.FindAll(authenticationInfo);
        }
        public override void AddOrUpdateRange(IEnumerable<NhDaDuAn> listEntities, AuthenticationInfo authenticationInfo)
        {
            _repository.AddOrUpdateRange(listEntities, authenticationInfo);
        }
        public IEnumerable<NhDaDuAnExportCTCQuery> GetDuAnExportCTC(int iLoai)
        {
            return _repository.GetDuAnExportCTC(iLoai);
        }
    }
}
