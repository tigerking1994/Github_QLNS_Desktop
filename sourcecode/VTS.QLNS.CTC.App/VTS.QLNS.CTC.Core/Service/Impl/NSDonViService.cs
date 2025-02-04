using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NsDonViService : IService<DonVi>, INsDonViService
    {
        private readonly INsDonViRepository _repository;
        private readonly ISktChungTuRepository _sktChungTuRepository;
        private readonly IDanhMucRepository _danhMucRepository;
        private readonly IMucLucNganSachRepository _mucLucNganSachRepository;
        private readonly IVdtDmDonViThucHienDuAnRepository _vdtDmDonViThucHienDuAnRepository;
        private const string PARENT_TYPE = "0";

        public NsDonViService(INsDonViRepository nsDonViRepository,
            ISktChungTuRepository sktChungTuRepository,
            IDanhMucRepository danhMucRepository,
            IMucLucNganSachRepository mucLucNganSachRepository,
            IVdtDmDonViThucHienDuAnRepository vdtDmDonViThucHienDuAnRepository)
        {
            _repository = nsDonViRepository;
            _sktChungTuRepository = sktChungTuRepository;
            _danhMucRepository = danhMucRepository;
            _mucLucNganSachRepository = mucLucNganSachRepository;
            _vdtDmDonViThucHienDuAnRepository = vdtDmDonViThucHienDuAnRepository;
        }

        public override IEnumerable<DonVi> FindAll(AuthenticationInfo authenticationInfo)
        {
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(d => authenticationInfo.YearOfWork == d.NamLamViec);
            if (authenticationInfo.OptionalParam != null && authenticationInfo.OptionalParam.Length > 0 && authenticationInfo.OptionalParam[0] is DialogType param)
            {
                if (param.Equals(DialogType.LoadDonViOfDanhMucNganh))
                    predicate = predicate.And(d => "1".Equals(d.Loai) && d.BCoNSNganh);
            }
            IEnumerable<DanhMuc> dmChuyenNganhs = _danhMucRepository.FindAll(t => t.INamLamViec == authenticationInfo.YearOfWork && t.SType == "NS_Nganh").ToList();
            IEnumerable<NsMucLucNganSach> lns = _mucLucNganSachRepository.FindAllLnsStartWith2(authenticationInfo).ToList();
            IEnumerable<DonVi> result = _repository.FindAll(predicate).OrderBy(d => d.Loai).ThenBy(d => d.IIDMaDonVi).ToList();
            foreach (DonVi n in result)
            {
                if (n.IdParent.HasValue)
                {
                    DonVi parent = result.SingleOrDefault(p => p.Id.Equals(n.IdParent.Value));
                    n.ParentName = parent == null ? null : parent.TenDonVi;
                }
                n.DanhMucChuyenNganh = dmChuyenNganhs.Where(d => d.SGiaTri != null && d.SGiaTri.Split(",").Contains(n.IIDMaDonVi)).ToList();
                n.TenDanhMuc = string.Join("; ", n.DanhMucChuyenNganh.Select(t => t.STen));
                n.LNS = lns.Where(t => n.IIDMaDonVi.Equals(t.IdMaDonVi)).ToList();
                n.TenLNS = string.Join("; ", n.LNS.Select(t => t.Lns));
            }
            return result;
        }

        public DonVi FindByIdDonVi(string idDonVi, int namLamViec)
        {
            return _repository.FindByIdDonVi(idDonVi, namLamViec);
        }

        public IEnumerable<DonVi> FindByNamLamViec(int namLamViec)
        {
            return _repository.FindByNamLamViec(namLamViec);
        }

        public IEnumerable<DonVi> FindByAllDataDonVi()
        {
            return _repository.FindAllDataDonVi();
        }

        public IEnumerable<DonVi> FindByCondition(Expression<Func<DonVi, bool>> predicate)
        {
            return _repository.FindAll(predicate).OrderBy(x => x.IIDMaDonVi);
        }

        public IEnumerable<DonVi> FindByListIdDonVi(string listIdDonVi)
        {
            return _repository.FindByListIdDonVi(listIdDonVi);
        }

        public IEnumerable<DonVi> FindByListIdDonVi(IEnumerable<string> listIdDonVi, int namLamViec)
        {
            return _repository.FindByListIdDonVi(listIdDonVi, namLamViec);
        }

        public IEnumerable<DonVi> FindByListIdDonVi(string idsDonVi, int namLamViec)
        {
            return _repository.FindByListIdDonVi(idsDonVi, namLamViec);
        }

        public override void AddOrUpdateRange(IEnumerable<DonVi> listEntities, AuthenticationInfo authenticationInfo)
        {
            List<Guid> deletedDonVis = listEntities.Where(i => i.IsDeleted).Select(i => i.Id).ToList();
            var time = DateTime.Now;
            // Đếm số bản ghi có loại đơn vị = 0 được cập nhật
            int CountRootDonVi = listEntities.Where(item => item.IsModified && !item.IsDeleted && (string.IsNullOrEmpty(item.Loai) || PARENT_TYPE.Equals(item.Loai))).Count();
            if (CountRootDonVi > 1)
            {
                throw new ArgumentException("Chỉ cho phép có 1 đơn vị có loại là 0");
            }
            // Lấy bản ghi có loại đơn vị = 0 được cập nhật, không tính bản ghi xóa
            DonVi UpdatedDonViRoot = listEntities.FirstOrDefault(t => t.IsModified && !t.IsDeleted && (string.IsNullOrEmpty(t.Loai) || PARENT_TYPE.Equals(t.Loai)));
            // Lấy bản ghi có cùng năm làm việc và loại đơn vị = 0 trong csdl, ko bao gồm bản ghi bị xóa
            DonVi DonViRoot = _repository.FirstOrDefault(t => !deletedDonVis.Contains(t.Id) && (string.IsNullOrEmpty(t.Loai) || PARENT_TYPE.Equals(t.Loai)) && authenticationInfo.YearOfWork == t.NamLamViec);
            if (DonViRoot != null && UpdatedDonViRoot != null && !DonViRoot.Id.Equals(UpdatedDonViRoot.Id))
            {
                throw new ArgumentException("Chỉ cho phép có 1 đơn vị có loại là 0");
            }
            ValidateMaDonVi(listEntities, authenticationInfo);
            foreach (var item in listEntities)
            {
                item.NamLamViec = authenticationInfo.YearOfWork;
                IEnumerable<DanhMuc> oldDanhMucs = _danhMucRepository.FindAll(d => "NS_Nganh".Equals(d.SType) && d.INamLamViec == item.NamLamViec && !string.IsNullOrEmpty(d.SGiaTri)).ToList();
                oldDanhMucs = oldDanhMucs.Where(d => d.SGiaTri.Split(",").Contains(item.IIDMaDonVi)).ToList();
                _danhMucRepository.RemoveDonViOfDanhMuc(oldDanhMucs, item.IIDMaDonVi);
                IEnumerable<NsMucLucNganSach> oldLns = _mucLucNganSachRepository.FindAll(d => d.NamLamViec == item.NamLamViec && item.IIDMaDonVi.Equals(d.IdMaDonVi)).ToList();
                _mucLucNganSachRepository.UpdateDonViOfMLNS(oldLns, null);
                if (item.IsModified)
                {
                    if (Guid.Empty.Equals(item.Id))
                    {
                        item.DNgayTao = time;
                        item.SNguoiTao = authenticationInfo.Principal;
                        item.DNgaySua = null;
                        item.SNguoiSua = null;
                    }
                    else
                    {
                        item.DNgaySua = time;
                        item.SNguoiSua = authenticationInfo.Principal;
                    }

                    // update danh muc chuyen nganh donvi
                    IEnumerable<string> idCodes = item.DanhMucChuyenNganh.Select(i => i.IIDMaDanhMuc);
                    IEnumerable<DanhMuc> danhMucs = _danhMucRepository.FindAll(d => "NS_Nganh".Equals(d.SType) && d.INamLamViec == item.NamLamViec && idCodes.Contains(d.IIDMaDanhMuc)).ToList();
                    _danhMucRepository.AddDonViOfDanhMuc(danhMucs, item.IIDMaDonVi);
                    // update lns cua donvi
                    IEnumerable<string> lns = item.LNS.Select(i => i.Lns);
                    IEnumerable<NsMucLucNganSach> updatedMLNS = _mucLucNganSachRepository.FindAllLnsStartWith2(authenticationInfo).Where(item => lns.Contains(item.Lns)).ToList();
                    _mucLucNganSachRepository.UpdateDonViOfMLNS(updatedMLNS, item.IIDMaDonVi);
                }
            }
            _repository.AddOrUpdateRange(listEntities);
        }

        public override void ImportDataExcel(IEnumerable<DonVi> listEntities, AuthenticationInfo authenticationInfo, int importMode)
        {
            ValidateDataExcel(listEntities, authenticationInfo, importMode);
            var time = DateTime.Now;
            IEnumerable<DonVi> DbMlns = _repository.FindAll().ToList();
            List<DonVi> entities = listEntities.ToList();
            for (int i = 0; i < entities.Count(); i++)
            {
                DonVi item = entities[i];
                DonVi track = DbMlns.FirstOrDefault(i => i.Id == item.Id);
                if (track == null)
                {
                    item.DNgayTao = time;
                    item.SNguoiTao = authenticationInfo.Principal;
                    item.DNgaySua = null;
                    item.SNguoiSua = null;
                }
                else
                {
                    track.MoTa = item.MoTa;
                    track.IIDMaDonVi = item.IIDMaDonVi;
                    track.TenDonVi = item.TenDonVi;
                    track.KyHieu = item.KyHieu;
                    track.MoTa = item.MoTa;
                    track.Loai = item.Loai;
                    track.NamLamViec = item.NamLamViec;
                    track.DNgaySua = time;
                    track.SNguoiSua = authenticationInfo.Principal;
                    track.Parent = null;
                    track.Children = null;
                    item = ObjectCopier.Clone(track);
                }
                item.IsModified = true;
                entities[i] = item;
            }
            _repository.AddOrUpdateRange(entities);
        }

        public override bool ValidateDataExcel(IEnumerable<DonVi> listEntities, AuthenticationInfo authenticationInfo, int importMode)
        {
            IEnumerable<Guid> modifiedIds = listEntities.Select(i => i.Id).ToList();
            foreach (DonVi donvi in listEntities)
            {
                if (string.IsNullOrEmpty(donvi.Loai) || PARENT_TYPE.Equals(donvi.Loai))
                {
                    // count số đơn vị có cùng loại là 0 và cùng năm làm việc
                    int countDonViParent = listEntities.Where(i => !i.IsDeleted && i.NamLamViec == donvi.NamLamViec
                        && (string.IsNullOrEmpty(i.Loai) || PARENT_TYPE.Equals(i.Loai))).Count();
                    if (countDonViParent > 1)
                    {
                        throw new ArgumentException("Chỉ cho phép có 1 đơn vị có loại là 0 trong năm " + donvi.NamLamViec);
                    }
                    if (IsExistDonViParent(donvi.NamLamViec.Value, donvi.Id))
                    {
                        throw new ArgumentException("Đã tồn tại 1 đơn vị có loại là 0 trong năm " + donvi.NamLamViec + " trong CSDL");
                    }
                }

                int countDuplicateIdDonVi = listEntities.Where(i => !i.IsDeleted && i.NamLamViec == donvi.NamLamViec &&
                    i.IIDMaDonVi == donvi.IIDMaDonVi).Count();
                if (countDuplicateIdDonVi > 1)
                {
                    throw new ArgumentException("Chỉ cho phép có mã là " + donvi.IIDMaDonVi + " trong năm " + donvi.NamLamViec);
                }
                if (IsExistIdDonVi(donvi.NamLamViec.Value, donvi.IIDMaDonVi, modifiedIds))
                {
                    throw new ArgumentException("Đã tồn tại 1 đơn vị có mã là " + donvi.IIDMaDonVi + " trong năm " + donvi.NamLamViec);
                }

            }
            return true;
        }

        public bool IsExistDonViParent(int namLamViec, Guid id)
        {
            int count = _repository.FindAll(i => i.Id != id && namLamViec == i.NamLamViec
                && (string.IsNullOrEmpty(i.Loai) || PARENT_TYPE.Equals(i.Loai))).ToList().Count();
            return count > 0;
        }

        public bool IsExistIdDonVi(int namLamViec, string idDonVi, IEnumerable<Guid> excludeIds)
        {
            int count = _repository.FindAll(i => !excludeIds.Contains(i.Id) && namLamViec == i.NamLamViec && idDonVi == i.IIDMaDonVi).ToList().Count();
            return count > 0;
        }

        public IEnumerable<DonVi> FindAll()
        {
            return _repository.FindAll();
        }

        public IEnumerable<DonViPlanBeginYearQuery> FindPlanBeginYearByConditon(int namLamViec, int namNganSach, int nguonNganSach, string loai, int loaiNNS, string userName)
        {
            return _repository.FindPlanBeginYearByConditon(namLamViec, namNganSach, nguonNganSach, loai, loaiNNS, userName);
        }

        public IEnumerable<DonViNgayChungTuQuery> FindByNamLamViecHasCapPhatChiTiet(int namLamViec)
        {
            return _repository.FindByNamLamViecHasCapPhatChiTiet(namLamViec);
        }

        public IEnumerable<DonVi> FindAllChildByIdDonVi(string idDonVi, int namLamViec)
        {
            return _repository.FindAllChildByIdDonVi(idDonVi, namLamViec);
        }
        public IEnumerable<DonViQuery> FindAllHopDongByDonViId(Guid idHopDong)
        {
            return _repository.FindAllHopDongByDonViId(idHopDong);
        }
        public IEnumerable<DonViQuery> FindAllDuAnByDonViId(Guid idDuAn)
        {
            return _repository.FindAllDuAnByDonViId(idDuAn);
        }
        public DonVi FindByLoai(string loai, int namLamViec)
        {
            return _repository.FindByLoai(loai, namLamViec);
        }

        public bool IsDonViCha(string maDonVi, int namLamViec)
        {
            return _repository.IsDonViCha(maDonVi, namLamViec);
        }

        public IEnumerable<string> FindChildNsDonVi(string idDonVi, int yearOfWork, int status)
        {
            return _repository.FindChildNsDonVi(idDonVi, yearOfWork, status);
        }

        public IEnumerable<DonVi> FindByCondition(int estimateDivision, int status, int namLamViec)
        {
            return _repository.FindByCondition(estimateDivision, status, namLamViec);
        }

        public IEnumerable<DonVi> FindBySettlementMonth(int yearOfWork, int yearOfBudget, int budgetSource, string quarterMonth, int quarterMonthType, string loaiQuyetToan)
        {
            return _repository.FindBySettlementMonth(yearOfWork, yearOfBudget, budgetSource, quarterMonth, quarterMonthType, loaiQuyetToan);
        }

        public IEnumerable<DonVi> FindDonViHasDataSktSoLieuChiTiet(int namLamViec, int namNganSach, int nguonNganSach, string loaiChungTu, int loaiNNS)
        {
            return _repository.FindDonViHasDataSktSoLieuChiTiet(namLamViec, namNganSach, nguonNganSach, loaiChungTu, loaiNNS);
        }

        public IEnumerable<DonViNgayChungTuQuery> FindByNgayChungTu(int namLamViec, DateTime ngayChungTu, bool isLuyKe)
        {
            return _repository.FindByNgayChungTu(namLamViec, ngayChungTu, isLuyKe);
        }

        public IEnumerable<DonVi> FindByEstimateSettlement(int yearOfWork, int yearOfBudget, int budgetSource, DateTime voucherDate, int quarterMonth, int quarterMonthType)
        {
            return _repository.FindByEstimateSettlement(yearOfWork, yearOfBudget, budgetSource, voucherDate, quarterMonth, quarterMonthType);
        }

        public IEnumerable<DonVi> FindByLoai(int namLamViec, string loai)
        {
            return _repository.FindByLoai(namLamViec, loai);
        }

        public IEnumerable<DonVi> FindForReceiveSettlementReport(int yearOfWork, string yearOfBudget, int budgetSource, string lns)
        {
            return _repository.FindForReceiveSettlementReport(yearOfWork, yearOfBudget, budgetSource, lns);
        }

        public IEnumerable<DonVi> FindBySettlement(int yearOfWork, int budgetSource, int dataType, string lns)
        {
            return _repository.FindBySettlement(yearOfWork, budgetSource, dataType, lns);
        }

        public IEnumerable<DonVi> FindBySettlement(int yearOfWork, int budgetSource, int dataType, string lns, string type)
        {
            return _repository.FindBySettlement(yearOfWork, budgetSource, dataType, lns);
        }

        public List<DonVi> GetDanhSachDonViByNguoiDung(string sMaNguoiDung, int iNamLamViec)
        {
            return _repository.GetDanhSachDonViByNguoiDung(sMaNguoiDung, iNamLamViec);
        }

        public DonVi Find(Guid id)
        {
            return _repository.Find(id);
        }

        public IEnumerable<DonVi> FindBySummaryVoucherList(int yearOfWork, int quarterMonth, string lns)
        {
            return _repository.FindBySummaryVoucherList(yearOfWork, quarterMonth, lns);
        }

        public DonVi FindById(Guid idDonVi)
        {
            return _repository.FindById(idDonVi);
        }

        public override IEnumerable<DonVi> FindDataToExportTemplate(AuthenticationInfo authenticationInfo)
        {
            return _repository.FindAll(i => i.NamLamViec == authenticationInfo.YearOfWork).ToList();
        }

        public IEnumerable<DonViQuery> FindAllDonViNotDuplicate()
        {
            return _repository.FindAllDonViNotDuplicate();
        }

        public int countNsDonViByNamLamViec(int namLamViec)
        {
            return _repository.countNsDonViByNamLamViec(namLamViec);
        }

        public IEnumerable<NSDonViExportQuery> GetDonViExportByNamLamViec(int iNamLamViec)
        {
            return _repository.GetDonViExportByNamLamViec(iNamLamViec);
        }

        public List<DonVi> FindByUser(string userName, int namLamViec, string type)
        {
            return _repository.FindByUser(userName, namLamViec, type).ToList();
        }

        public List<DonVi> FindByUserCreateVoucher(string userName, int namLamViec, string type)
        {
            return _repository.FindByUserCreateVoucher(userName, namLamViec, type).ToList();
        }

        private bool ValidateMaDonVi(IEnumerable<DonVi> listEntities, AuthenticationInfo authenticationInfo)
        {
            IEnumerable<DonVi> updatedDonvis = listEntities.Where(d => !d.IsDeleted);
            IEnumerable<DonVi> deletedDonvis = listEntities.Where(d => d.IsDeleted);
            var dupes = updatedDonvis.GroupBy(x => new { x.IIDMaDonVi, x.NamLamViec })
                   .Where(x => x.Count() > 1).Any();
            if (dupes)
            {
                throw new ArgumentException("Mã đơn vị không được trùng");
            }
            IEnumerable<DonVi> dataDonvis = _repository.FindAll(d => updatedDonvis.Select(t => t.IIDMaDonVi).Contains(d.IIDMaDonVi)
                && authenticationInfo.YearOfWork == d.NamLamViec
                && !listEntities.Select(t => t.Id).Contains(d.Id)).ToList();
            if (dataDonvis.Any())
            {
                throw new ArgumentException("Mã đơn vị không được trùng");
            }
            return true;
        }

        public IEnumerable<DonVi> FindInternalByNamLamViec(int namLamViec)
        {
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == namLamViec);
            predicate = predicate.And(x => LoaiDonVi.ROOT.Equals(x.Loai) || LoaiDonVi.NOI_BO.Equals(x.Loai));
            return FindByCondition(predicate);
        }

        public IEnumerable<DonVi> FindForEstimateDivisionReport(int namLamViec, int namNganSach, int nguonNganSach, int loaiChungTu, string idChungTu, bool isLuyKe)
        {
            return _repository.FindForEstimateDivisionReport(namLamViec, namNganSach, nguonNganSach, loaiChungTu, idChungTu, isLuyKe);
        }
        public IEnumerable<DonVi> FindForRevenueExpenditureDivisionReport(int namLamViec, int namNganSach, int nguonNganSach, string idChungTu, bool isLuyKe)
        {
            return _repository.FindForRevenueExpenditureDivisionReport(namLamViec, namNganSach, nguonNganSach, idChungTu, isLuyKe);
        }

        public IEnumerable<DonVi> FindForSocialInsuranceEstimateDivisionReport(int namLamViec, string idChungTu)
        {
            return _repository.FindForSocialInsuranceEstimateDivisionReport(namLamViec, idChungTu);
        }

        public void SaveDonViSuDung(DonVi donVi, int namLamViec)
        {
            _repository.SaveDonViSuDung(donVi, namLamViec);
        }

        public DonVi FindByMaDonViAndNamLamViec(string maDonVi, int namLamViec)
        {
            return _repository.FindByMaDonViAndNamLamViec(maDonVi, namLamViec);
        }

        public DonVi FindCurrentDonViSuDungByNamLamViec(int namLamViec)
        {
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == namLamViec);
            predicate = predicate.And(x => LoaiDonVi.ROOT.Equals(x.Loai));
            return _repository.FirstOrDefault(predicate);
        }

        public IEnumerable<DonVi> FindDonViConByNamLamViec(int namLamViec)
        {
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == namLamViec);
            predicate = predicate.And(x => LoaiDonVi.NOI_BO.Equals(x.Loai) || LoaiDonVi.TOAN_QUAN.Equals(x.Loai));
            return FindByCondition(predicate);
        }

        public void SaveAllDonViCon(IEnumerable<DonVi> items, int yearOfWork)
        {
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork);
            predicate = predicate.And(x => LoaiDonVi.ROOT.Equals(x.Loai));
            DonVi parent = _repository.FirstOrDefault(predicate);
            foreach (DonVi dv in items)
            {
                if (parent != null)
                {
                    dv.NamLamViec = parent.NamLamViec;
                    dv.IdParent = parent.Id;
                    dv.iCapDonVi = parent.iCapDonVi + 1;
                }
            }
            _repository.AddOrUpdateRange(items);
        }

        public IEnumerable<DonViPlanBeginYearQuery> FindPlanBeginYearAgencyByConditon(int namLamViec, int namNganSach, int nguonNganSach, string loai, string userName)
        {
            return _repository.FindPlanBeginYearAgencyByConditon(namLamViec, namNganSach, nguonNganSach, loai, userName);
        }

        public void CopyDataToDonViThucHienDuAn(int namLamViec)
        {
            List<VdtDmDonViThucHienDuAn> existedVdtDmDonViThucHienDuAns = _vdtDmDonViThucHienDuAnRepository.FindAll().ToList();
            List<string> MaDonVis = existedVdtDmDonViThucHienDuAns.Select(t => t.IIdMaDonVi).ToList();
            List<VdtDmDonViThucHienDuAn> vdtDmDonViThucHienDuAns = new List<VdtDmDonViThucHienDuAn>();
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == namLamViec);
            IEnumerable<DonVi> donVis = _repository.FindAll(predicate);
            foreach (var donvi in donVis)
            {
                if (MaDonVis.Contains(donvi.IIDMaDonVi))
                {
                    continue;
                }
                VdtDmDonViThucHienDuAn vdtDmDonViThucHienDuAn = new VdtDmDonViThucHienDuAn
                {
                    IIdDonVi = donvi.Id,
                    IIdMaDonVi = donvi.IIDMaDonVi,
                    STenDonVi = donvi.TenDonVi,
                    IIdDonViCha = donvi.IdParent,
                    ICapDonVi = donvi.iCapDonVi,
                    BHangCha = donVis.Any(d => d.IdParent != null && donvi.Id == d.IdParent),
                    IIDMaDonViNS = donvi.IIDMaDonVi,
                };
                vdtDmDonViThucHienDuAns.Add(vdtDmDonViThucHienDuAn);
            }
            _vdtDmDonViThucHienDuAnRepository.AddRange(vdtDmDonViThucHienDuAns);
        }

        public int CountDonVi()
        {
            return _repository.CountDonVi();
        }

        public IEnumerable<DonVi> FindByQtTongHopQuy(int yearOfWork, string yearOfBudget, int budgetSource, string quarterMonth, string khoi, string loaiQuyetToan, string userName)
        {
            return _repository.FindByQtTongHopQuy(yearOfWork, yearOfBudget, budgetSource, quarterMonth, khoi, loaiQuyetToan, userName);
        }

        public IEnumerable<DonVi> FindByCapPhatId(int yearOfWork, string listCapPhatId)
        {
            return _repository.FindByCapPhatId(yearOfWork, listCapPhatId);
        }
        public IEnumerable<DonVi> FindByCapPhatIdForBH(int yearOfWork, string listCapPhatId)
        {
            return _repository.FindByCapPhatIdForBH(yearOfWork, listCapPhatId);
        }
        public IEnumerable<DonVi> FindByDonViOfAllocationTongHopForBH(int yearOfWork, int quy, Guid idLoaiChi)
        {
            return _repository.FindByDonViOfAllocationTongHopForBH(yearOfWork, quy, idLoaiChi);
        }
        public IEnumerable<DonVi> FindByDonViOfAllocationPlanForBH(int yearOfWork, string listMaDonVi, int iQuy)
        {
            return _repository.FindByDonViOfAllocationPlanForBH(yearOfWork, listMaDonVi, iQuy);
        }
        public IEnumerable<DonVi> FindByCapPhatId2(int yearOfWork, string listCapPhatId, int loaiNganSach)
        {
            return _repository.FindByCapPhatId2(yearOfWork, listCapPhatId, loaiNganSach);
        }
        public IEnumerable<DonVi> FindByQuanSo(int yearOfWork, string months)
        {
            return _repository.FindByQuanSo(yearOfWork, months);
        }

        public IEnumerable<DonVi> FindBySummaryAgencySettlement(int yearOfWork, int yearOfBudget, int budgetSouce, string lns, string quarterMonth, DateTime voucherDate, bool hasDuToan)
        {
            return _repository.FindBySummaryAgencySettlement(yearOfWork, yearOfBudget, budgetSouce, lns, quarterMonth, voucherDate, hasDuToan);
        }

        public IEnumerable<DonVi> FindHospitalTargetAgencyReportDonVi(int namLamViec, string idChungTu, int loaiChungTu)
        {
            return _repository.FindHospitalTargetAgencyReportDonVi(namLamViec, idChungTu, loaiChungTu);
        }

        public IEnumerable<DonVi> FindForAdjustmentEstimateReport(int yearOfWork, int yearOfBudget, int budgetSouce, int dot)
        {
            return _repository.FindForAdjustmentEstimateReport(yearOfWork, yearOfBudget, budgetSouce, dot);
        }

        public int CountILoaiByNamLamViec(int year, string type)
        {
            return _repository.CountILoaiByNamLamViec(year, type);
        }

        public IEnumerable<DonVi> FindByYearAndNhiemVuChi(int namLamViec, bool HasNhiemVuChi = true)
        {
            return _repository.FindByYearAndNhiemVuChi(namLamViec, HasNhiemVuChi);
        }

        public IEnumerable<DonVi> FindAll(Expression<Func<DonVi, bool>> predicate)
        {
            return _repository.FindAll(predicate);
        }

        public IEnumerable<DonVi> FindInTongHopSKTBenhVienTuChu(int yearOfWork, int loaiNNS)
        {
            return _repository.FindInTongHopSKTBenhVienTuChu(yearOfWork, loaiNNS);
        }
        public IEnumerable<DonVi> FindByYearAndIDNhiemVuChi(int namLamViec, Guid? IDNhiemVuChi, string sLoaiSoCu = null)
        {
            return _repository.FindByYearAndIDNhiemVuChi(namLamViec, IDNhiemVuChi, sLoaiSoCu);
        }

        public IEnumerable<DonVi> FindForSocialInsuranceEstimateReport(int yearOfWork, Guid iIDLoaiCap)
        {
            return _repository.FindForSocialInsuranceEstimateReport(yearOfWork, iIDLoaiCap);
        }

        public IEnumerable<DonVi> FindByListDonViCap2KhacCha(int namLamViec)
        {
            return _repository.FindByListDonViCap2KhacCha(namLamViec);
        }

        public IEnumerable<DonVi> FindDonViCoDataSktSoLieuChiTietAllLoai(int namLamViec, int namNganSach, int nguonNganSach, int loaiNNS, int loaiChungtu)
        {
            return _repository.FindDonViCoDataSktSoLieuChiTietAllLoai(namLamViec, namNganSach, nguonNganSach, loaiNNS, loaiChungtu);
        }
        public IEnumerable<DonVi> FindAllDataDonViCurrent(int namLamViec)
        {
            return _repository.FindAllDataDonViCurrent(namLamViec);
        }
        public IEnumerable<string> FindAllDonViByBaoCaoThamDinhBH(int namLamViec)
        {
            return _repository.FindAllDonViByBaoCaoThamDinhBH(namLamViec);
        }
    }
}
