using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Repository.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class BhDmMucLucNganSachService : IService<BhDmMucLucNganSach>, IBhDmMucLucNganSachService
    {
        private readonly IBhDmMucLucNganSachRepository _bhDmMucLucNganSachRepository;
        public IDanhMucRepository _danhMucRepository;
        public BhDmMucLucNganSachService(IBhDmMucLucNganSachRepository bhDmMucLucNganSachRepository, IDanhMucRepository danhMucRepository)
        {
            _bhDmMucLucNganSachRepository = bhDmMucLucNganSachRepository;
            _danhMucRepository = danhMucRepository;
        }
        public IEnumerable<BhDmMucLucNganSach> FindByCondition(Expression<Func<BhDmMucLucNganSach, bool>> predicate)
        {
            return _bhDmMucLucNganSachRepository.FindAll(predicate);
        }

        public List<BhDmMucLucNganSach> GetListBhMucLucNs(int namLamViec, string khoiDuToanBHXH, string khoiHachToanBHXH)
        {
            return _bhDmMucLucNganSachRepository.GetListBhMucLucNs(namLamViec, khoiDuToanBHXH, khoiHachToanBHXH);
        }

        public List<BhDmMucLucNganSach> GetListBhMucLucNsForQLKP(int inamLamViec, string khoi)
        {
            return _bhDmMucLucNganSachRepository.GetListBhMucLucNsForQLKP(inamLamViec, khoi);
        }

        public List<BhDmMucLucNganSach> GetListBhytMucLucNs(int namLamViec, string loaiNS)
        {
            return _bhDmMucLucNganSachRepository.GetListBhytMucLucNs(namLamViec, loaiNS);
        }

        public IEnumerable<BhDmMucLucNganSach> FindByListLnsDonVi(string lns, int namLamViec)
        {
            return _bhDmMucLucNganSachRepository.FindByListLnsDonVi(lns, namLamViec);
        }
        public DanhMuc FindMLNSChiTietToi(int namLamViec)
        {
            return _danhMucRepository.FindByCode("MLNS_CHITIET_TOI", namLamViec);
        }
        public override IEnumerable<BhDmMucLucNganSach> FindAll(AuthenticationInfo authenticationInfo)
        {
            return _bhDmMucLucNganSachRepository.FindByMLNSNamLamViec(authenticationInfo.YearOfWork).ToList();
        }
        public override void AddOrUpdateRange(IEnumerable<BhDmMucLucNganSach> listEntities, AuthenticationInfo authenticationInfo)
        {
            var time = DateTime.Now;
            IEnumerable<Guid> excludeIds = listEntities.Select(i => i.Id).ToList();
            IEnumerable<BhDmMucLucNganSach> modifiedItems = listEntities.Where(i => !i.IsDeleted).ToList();
            foreach (var item in listEntities)
            {
                item.INamLamViec = authenticationInfo.YearOfWork;
                item.SXauNoiMa = GetXNM(item);
                item.ILoai = getTypeOfMlns(item);
                if (item.IsModified)
                {
                    var predicate = PredicateBuilder.True<BhDmMucLucNganSach>();
                    predicate = predicate.And(x => x.INamLamViec == item.INamLamViec);
                    predicate = predicate.And(x => x.SLNS.ToLower() == item.SLNS.ToLower());
                    predicate = predicate.And(x => x.SL.ToLower() == item.SL.ToLower());
                    predicate = predicate.And(x => x.SK.ToLower() == item.SK.ToLower());
                    predicate = predicate.And(x => x.SM.ToLower() == item.SM.ToLower());
                    predicate = predicate.And(x => x.STM.ToLower() == item.STM.ToLower());
                    predicate = predicate.And(x => x.STTM.ToLower() == item.STTM.ToLower());
                    predicate = predicate.And(x => x.SNG.ToLower() == item.SNG.ToLower());
                    predicate = predicate.And(x => x.STNG.ToLower() == item.STNG.ToLower());
                    predicate = predicate.And(x => x.STNG1.ToLower() == item.STNG1.ToLower());
                    predicate = predicate.And(x => x.STNG2.ToLower() == item.STNG2.ToLower());
                    predicate = predicate.And(x => x.STNG3.ToLower() == item.STNG3.ToLower());
                    int countDuplicateIdCodes = modifiedItems.Where(predicate.Compile()).Count();
                    if (countDuplicateIdCodes > 1)
                    {
                        throw new ArgumentException("Xâu nối mã " + item.SXauNoiMa + " bị lặp, vui lòng thử lại");
                    }
                    // Nếu mã tồn tại và mã không thuộc danh sách bản ghi bị xóa thfi sẽ throw exception
                    if (CheckExistXNM(predicate, excludeIds))
                    {
                        throw new ArgumentException("Xâu nối mã " + item.SXauNoiMa + " đã tồn tại, vui lòng thử lại");
                    }
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
                }
            }
            _bhDmMucLucNganSachRepository.AddOrUpdateRange(listEntities, authenticationInfo.YearOfWork);
        }
        private string getTypeOfMlns(BhDmMucLucNganSach entity)
        {
            foreach (string type in mlnsType)
            {
                PropertyInfo propertyInfo = typeof(BhDmMucLucNganSach).GetProperty(type);
                object val = propertyInfo.GetValue(entity, null);
                if (val != null && !string.IsNullOrWhiteSpace(val.ToString()))
                {
                    return type;
                }
            }
            return "";
        }
        private List<string> mlnsType = new List<string>
        {
            "STNG3", "STNG2", "STNG1", "STNG", "SNG", "STTM", "STM", "SM", "SK", "SL", "SLNS"
        };
        public virtual bool CheckExistXNM(Expression<Func<BhDmMucLucNganSach, bool>> predicate, IEnumerable<Guid> excludeIds)
        {
            // tìm bản ghi có cùng mã, cùng năm làm việc, cùng loại, không bao gồm các bản ghi sẽ được cập nhật
            predicate = predicate.And(t => !excludeIds.Contains(t.Id));
            IEnumerable<BhDmMucLucNganSach> danhMuc = _bhDmMucLucNganSachRepository.FindAll(predicate).ToList();
            return danhMuc.Count() != 0;
        }
        private bool CheckExistXNM(Expression<Func<BhDmMucLucNganSach, bool>> predicate, IEnumerable<Guid> excludeIds, IEnumerable<BhDmMucLucNganSach> DbMlns)
        {
            // tìm bản ghi có cùng mã, cùng năm làm việc, cùng loại, không bao gồm các bản ghi sẽ được cập nhật
            predicate = predicate.And(t => !excludeIds.Contains(t.Id));
            IEnumerable<BhDmMucLucNganSach> danhMuc = DbMlns.Where(predicate.Compile()).ToList();
            return danhMuc.Count() != 0;
        }
        public string GetXNM(BhDmMucLucNganSach bhMucLucNganSach)
        {
            StringBuilder xnm = new StringBuilder();
            if (string.IsNullOrEmpty(bhMucLucNganSach.SLNS))
            {
                return xnm.ToString();
            }
            xnm.Append(bhMucLucNganSach.SLNS);
            if (string.IsNullOrEmpty(bhMucLucNganSach.SL))
            {
                return xnm.ToString();
            }
            xnm.Append("-").Append(bhMucLucNganSach.SL);
            if (string.IsNullOrEmpty(bhMucLucNganSach.SK))
            {
                return xnm.ToString();
            }
            xnm.Append("-").Append(bhMucLucNganSach.SK);
            if (string.IsNullOrEmpty(bhMucLucNganSach.SM))
            {
                return xnm.ToString();
            }
            xnm.Append("-").Append(bhMucLucNganSach.SM);
            if (string.IsNullOrEmpty(bhMucLucNganSach.STM))
            {
                return xnm.ToString();
            }
            xnm.Append("-").Append(bhMucLucNganSach.STM);
            if (string.IsNullOrEmpty(bhMucLucNganSach.STTM))
            {
                return xnm.ToString();
            }
            xnm.Append("-").Append(bhMucLucNganSach.STTM);
            if (string.IsNullOrEmpty(bhMucLucNganSach.SNG))
            {
                return xnm.ToString();
            }
            xnm.Append("-").Append(bhMucLucNganSach.SNG);
            if (string.IsNullOrEmpty(bhMucLucNganSach.STNG))
            {
                return xnm.ToString();
            }
            xnm.Append("-").Append(bhMucLucNganSach.STNG);
            if (string.IsNullOrEmpty(bhMucLucNganSach.STNG1))
            {
                return xnm.ToString();
            }
            xnm.Append("-").Append(bhMucLucNganSach.STNG1);
            if (string.IsNullOrEmpty(bhMucLucNganSach.STNG2))
            {
                return xnm.ToString();
            }
            xnm.Append("-").Append(bhMucLucNganSach.STNG2);
            if (string.IsNullOrEmpty(bhMucLucNganSach.STNG3))
            {
                return xnm.ToString();
            }
            xnm.Append("-").Append(bhMucLucNganSach.STNG3);
            return xnm.ToString();
        }

        public bool IsUsedMLNS(Guid mlnsId, int namLamViec)
        {
            return _bhDmMucLucNganSachRepository.IsUsedMLNS(mlnsId, namLamViec);
        }

        public IEnumerable<BhDmMucLucNganSach> FindByIsHangCha(int year, bool isHangCha)
        {
            var predicate = PredicateBuilder.True<BhDmMucLucNganSach>();
            predicate = predicate.And(x => x.INamLamViec == year && x.BHangCha == isHangCha);
            return _bhDmMucLucNganSachRepository.FindAll(predicate).OrderBy(p => p.SXauNoiMa);
        }

        public IEnumerable<ReportMLNSQuery> FindReportMLNSQuery(int year, Guid guid)
        {
            return _bhDmMucLucNganSachRepository.ReportMLNS(year, guid);
        }

        public override IEnumerable<BhDmMucLucNganSach> FindDataToExportTemplate(AuthenticationInfo authenticationInfo)
        {
            return _bhDmMucLucNganSachRepository.FindAll(i => i.INamLamViec == authenticationInfo.YearOfWork).OrderBy(p => p.SXauNoiMa).ToList();
        }

        public override bool ValidateDataExcel(IEnumerable<BhDmMucLucNganSach> listEntities, AuthenticationInfo authenticationInfo, int importMode)
        {
            IEnumerable<Guid> excludeIds = listEntities.Select(i => i.Id).ToList();
            IEnumerable<BhDmMucLucNganSach> DbMlns = _bhDmMucLucNganSachRepository.FindAll().ToList();
            foreach (var item in listEntities)
            {
                var predicate = PredicateBuilder.True<BhDmMucLucNganSach>();
                item.SXauNoiMa = StringUtils.Join(StringUtils.DIVISION, item.SLNS, item.SL, item.SK, item.SM, item.STM, item.STTM, item.SNG, item.STNG);
                predicate = predicate.And(x => x.INamLamViec == item.INamLamViec);
                predicate = predicate.And(x => x.SLNS == item.SLNS);
                predicate = predicate.And(x => x.SL == item.SL);
                predicate = predicate.And(x => x.SK == item.SK);
                predicate = predicate.And(x => x.SM == item.SM);
                predicate = predicate.And(x => x.STM == item.STM);
                predicate = predicate.And(x => x.STTM == item.STTM);
                predicate = predicate.And(x => x.SNG == item.SNG);
                predicate = predicate.And(x => x.STNG == item.STNG);
                int countDuplicateIdCodes = listEntities.Where(predicate.Compile()).Count();
                if (countDuplicateIdCodes > 1)
                {
                    throw new ArgumentException("Xâu nối mã " + item.SXauNoiMa + " bị lặp, vui lòng thử lại");
                }
                // Nếu mã tồn tại và mã không thuộc danh sách bản ghi bị xóa thfi sẽ throw exception
                if (CheckExistXNM(predicate, excludeIds, DbMlns))
                {
                    throw new ArgumentException("Xâu nối mã " + item.SXauNoiMa + " đã tồn tại, vui lòng thử lại");
                }
            }
            return true;
        }

        public override void ImportDataExcel(IEnumerable<BhDmMucLucNganSach> listEntities, AuthenticationInfo authenticationInfo, int importMode)
        {
            var time = DateTime.Now;
            IEnumerable<BhDmMucLucNganSach> DbMlns = _bhDmMucLucNganSachRepository.FindAll().ToList();
            IEnumerable<Guid> modifiedIds = listEntities.Select(i => i.Id);
            List<BhDmMucLucNganSach> entities = listEntities.ToList();
            for (int i = 0; i < entities.Count(); i++)
            {
                BhDmMucLucNganSach item = entities[i];
                BhDmMucLucNganSach track = DbMlns.FirstOrDefault(i => i.Id == item.Id);
                // bản ghi mới
                if (track == null)
                {
                    item.IIDMLNS = Guid.NewGuid();
                    item.DNgayTao = time;
                    item.SNguoiTao = authenticationInfo.Principal;
                    item.DNgaySua = null;
                    item.SNguoiSua = null;
                }
                else
                {
                    track.SLNS = item.SLNS;
                    track.SK = item.SK;
                    track.SL = item.SL;
                    track.SM = item.SM;
                    track.STM = item.STM;
                    track.STTM = item.STTM;
                    track.SNG = item.SNG;
                    track.STNG = item.STNG;
                    track.SMoTa = item.SMoTa;
                    track.INamLamViec = item.INamLamViec;
                    track.DNgaySua = time;
                    track.SNguoiSua = authenticationInfo.Principal;
                    item = ObjectCopier.Clone(track);
                }
                item.SXauNoiMa = StringUtils.Join(StringUtils.DIVISION, item.SLNS, item.SL, item.SK, item.SM, item.STM, item.STTM, item.SNG, item.STNG);
                item.IsModified = true;
                entities[i] = item;
            }
            for (int i = 0; i < entities.Count(); i++)
            {
                BhDmMucLucNganSach item = entities[i];
                if (Guid.Empty.Equals(item.Id))
                {
                    BhDmMucLucNganSach parent = FindParentByXNM(item.SXauNoiMa, entities, DbMlns, modifiedIds, item.INamLamViec.Value);
                    if (parent != null)
                    {
                        item.IIDMLNSCha = parent.IIDMLNS;
                    }
                    item.BHangCha = IsHangCha(item, entities, DbMlns);
                }
            }
            _bhDmMucLucNganSachRepository.AddOrUpdateRange(entities);
        }

        private BhDmMucLucNganSach FindParentByXNM(string xnm,
            IEnumerable<BhDmMucLucNganSach> modifiedMlns, IEnumerable<BhDmMucLucNganSach> DbMlns, IEnumerable<Guid> excludeIds, int namLamViec)
        {
            IEnumerable<BhDmMucLucNganSach> ancestors = modifiedMlns.Where(i => xnm.StartsWith(i.SXauNoiMa + "-") && namLamViec == i.INamLamViec)
                .OrderByDescending(i => i.SXauNoiMa.Length);
            BhDmMucLucNganSach parentInModifiedList = ancestors.FirstOrDefault();

            IEnumerable<BhDmMucLucNganSach> DbAncestors = DbMlns.Where(i => !excludeIds.Contains(i.Id) && namLamViec == i.INamLamViec &&
                xnm.StartsWith(i.SXauNoiMa + "-")).OrderByDescending(i => i.SXauNoiMa.Length);
            BhDmMucLucNganSach parentInDbList = DbAncestors.FirstOrDefault();

            if (parentInModifiedList != null && (parentInDbList == null || parentInModifiedList.SXauNoiMa.Length > parentInDbList.SXauNoiMa.Length))
            {
                return parentInModifiedList;
            }
            return parentInDbList;
        }

        private bool IsHangCha(BhDmMucLucNganSach bhMucLucNganSach, IEnumerable<BhDmMucLucNganSach> listEntities, IEnumerable<BhDmMucLucNganSach> DbMlns)
        {
            return listEntities.Any(i => i.SXauNoiMa.StartsWith(bhMucLucNganSach.SXauNoiMa + "-") && bhMucLucNganSach.INamLamViec == i.INamLamViec)
                || DbMlns.Any(i => i.SXauNoiMa.StartsWith(bhMucLucNganSach.SXauNoiMa + "-") && bhMucLucNganSach.INamLamViec == i.INamLamViec);
        }

        public int Update(BhDmMucLucNganSach item)
        {
            return _bhDmMucLucNganSachRepository.Update(item);
        }

        public IEnumerable<BhDmMucLucNganSach> FindByMLNS(int namLamViec, int status)
        {
            var predicate = createPredicateAllNull();
            predicate = predicate.And(x => x.INamLamViec == namLamViec);
            predicate = predicate.And(x => x.ITrangThai == status);
            return _bhDmMucLucNganSachRepository.FindAll(predicate).OrderBy(n => n.SLNS);
        }

        public Expression<Func<BhDmMucLucNganSach, bool>> createPredicateAllNull()
        {
            var predicate = PredicateBuilder.True<BhDmMucLucNganSach>();
            predicate = predicate.And(x => string.IsNullOrEmpty(x.SL));
            predicate = predicate.And(x => string.IsNullOrEmpty(x.SK));
            predicate = predicate.And(x => string.IsNullOrEmpty(x.SM));
            predicate = predicate.And(x => string.IsNullOrEmpty(x.STM));
            predicate = predicate.And(x => string.IsNullOrEmpty(x.STTM));
            predicate = predicate.And(x => string.IsNullOrEmpty(x.SNG));
            predicate = predicate.And(x => string.IsNullOrEmpty(x.STNG));
            return predicate;
        }

        public IEnumerable<BhDmMucLucNganSach> FindForDieuChinh(int namLamViec, string donVi, Guid loaiDanhMucCapChi, DateTime ngayChungTu, string userName)
        {
            return _bhDmMucLucNganSachRepository.FindForDieuChinh(namLamViec, donVi, loaiDanhMucCapChi, ngayChungTu, userName);
        }

        public IEnumerable<BhDmMucLucNganSach> FindAllByYear(int namLamViec)
        {
            var predicate = PredicateBuilder.True<BhDmMucLucNganSach>();
            predicate = predicate.And(x => x.INamLamViec == namLamViec && x.ITrangThai == 1);
            return _bhDmMucLucNganSachRepository.FindAll(predicate).OrderBy(x => x.SXauNoiMa);
        }

        public IEnumerable<BhDmMucLucNganSach> FindForDieuChinhDTT(int namLamViec, string donVi, DateTime ngayChungTu, string userName)
        {
            return _bhDmMucLucNganSachRepository.FindForDieuChinhDTT(namLamViec, donVi, ngayChungTu, userName);
        }
        public List<BhDmMucLucNganSach> GetListMucLucForDanhMucLoaiChi(int namLamViec, string sLNS)
        {
            return _bhDmMucLucNganSachRepository.GetListMucLucForDanhMucLoaiChi(namLamViec, sLNS);
        }

        public List<BhDmMucLucNganSach> FindByUser(string userName, int yearOfWork, string LNSExcept)
        {
            return _bhDmMucLucNganSachRepository.FindByUser(userName, yearOfWork, LNSExcept).ToList();
        }

        public IEnumerable<BhDmMucLucNganSach> FindAll(int namLamViec)
        {
            var predicate = PredicateBuilder.True<BhDmMucLucNganSach>();
            predicate = predicate.And(x => x.INamLamViec == namLamViec && x.ITrangThai == 1);
            return _bhDmMucLucNganSachRepository.FindAll(predicate).OrderBy(x => x.SXauNoiMa);
        }
        public IEnumerable<BhDmMucLucNganSach> FindSLNSForQTCQKPQL(int namLamViec, string donVi, int iQuy, DateTime ngayChungTu, string userName)
        {
            return _bhDmMucLucNganSachRepository.FindSLNSForQTCQKPQL(namLamViec, donVi, iQuy, ngayChungTu, userName);
        }

        public IEnumerable<BhDmMucLucNganSach> FindSLNSForQTCQKPK(int namLamViec, string donVi, int iQuy, DateTime ngayChungTu, string userName, Guid IdLoaiChi)
        {
            return _bhDmMucLucNganSachRepository.FindSLNSForQTCQKPK(namLamViec, donVi, iQuy, ngayChungTu, userName, IdLoaiChi);
        }

        public IEnumerable<BhDmMucLucNganSach> FindSLNSForQTCQBHXH(int namLamViec, string donVi, int iQuy, DateTime ngayChungTu, string userName)
        {
            return _bhDmMucLucNganSachRepository.FindSLNSForQTCQBHXH(namLamViec, donVi, iQuy, ngayChungTu, userName);
        }

        public IEnumerable<BhDmMucLucNganSach> FindSLNSForQTCQKCB(int yearOfWork, string agencyIds, int iQuy, DateTime dt, string principal)
        {
            return _bhDmMucLucNganSachRepository.FindSLNSForQTCQKCB(yearOfWork, agencyIds, iQuy, dt, principal);
        }

        public List<BhDmMucLucNganSach> FindSLNSForQTCNBHXH(int yearOfWork, string agencyIds, DateTime dtime, string principal)
        {
            return _bhDmMucLucNganSachRepository.FindSLNSForQTCNBHXH(yearOfWork, agencyIds, dtime, principal);
        }

        public List<BhDmMucLucNganSach> FindSLNSForQTCNKCB(int yearOfWork, string agencyIds, DateTime dtime, string principal)
        {
            return _bhDmMucLucNganSachRepository.FindSLNSForQTCNKCB(yearOfWork, agencyIds, dtime, principal);
        }

        public List<BhDmMucLucNganSach> FindSLNSForQTCNKPQL(int yearOfWork, string agencyIds, DateTime dtime, string principal)
        {
            return _bhDmMucLucNganSachRepository.FindSLNSForQTCNKPQL(yearOfWork, agencyIds, dtime, principal);
        }

        public List<BhDmMucLucNganSach> FindSLNSForQTCNKPK(int yearOfWork, string agencyIds, DateTime dt, string principal, Guid idLoaiChi)
        {
            return _bhDmMucLucNganSachRepository.FindSLNSForQTCNKPK(yearOfWork, agencyIds, dt, principal, idLoaiChi);
        }

        public List<BhDmMucLucNganSachQuery> GetMLNSCheDoBHXH(int yearOfWork)
        {
            return _bhDmMucLucNganSachRepository.GetMLNSCheDoBHXH(yearOfWork);
        }

        public List<BhDmMucLucNganSach> GetByLnsDieuChinhDuToan(int yearOfWork, string sLNS)
        {
            return _bhDmMucLucNganSachRepository.GetByLnsDieuChinhDuToan(yearOfWork, sLNS);
        }
    }
}
