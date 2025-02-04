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
    public class TlDmKinhPhiService : IService<NsMucLucNganSach>, ITlDmKinhPhiService
    {
        private IMucLucNganSachRepository _mucLucNganSachRepository;

        public TlDmKinhPhiService(IMucLucNganSachRepository mucLucNganSachRepository)
        {
            _mucLucNganSachRepository = mucLucNganSachRepository;
        }

        public override void AddOrUpdateRange(IEnumerable<NsMucLucNganSach> listEntities, AuthenticationInfo authenticationInfo)
        {
            var time = DateTime.Now;
            IEnumerable<Guid> excludeIds = listEntities.Select(i => i.Id).ToList();
            IEnumerable<NsMucLucNganSach> modifiedItems = listEntities.Where(i => !i.IsDeleted).ToList();
            List<Guid> lstIdParentDelete = new List<Guid>();
            foreach (var item in listEntities)
            {
                item.NamLamViec = authenticationInfo.YearOfWork;
                item.XauNoiMa = GetXNM(item);
                if (item.IsDeleted && item.MlnsIdParent.HasValue)
                {
                    lstIdParentDelete.Add(item.MlnsIdParent.Value);
                }
                if (item.IsModified)
                {
                    var predicate = PredicateBuilder.True<NsMucLucNganSach>();
                    predicate = predicate.And(x => x.NamLamViec == item.NamLamViec);
                    predicate = predicate.And(x => x.Lns == item.Lns);
                    predicate = predicate.And(x => x.L == item.L);
                    predicate = predicate.And(x => x.K == item.K);
                    predicate = predicate.And(x => x.M == item.M);
                    predicate = predicate.And(x => x.Tm == item.Tm);
                    predicate = predicate.And(x => x.Ttm == item.Ttm);
                    predicate = predicate.And(x => x.Ng == item.Ng);
                    predicate = predicate.And(x => x.Tng == item.Tng);
                    predicate = predicate.And(x => x.Tng1 == item.Tng1);
                    predicate = predicate.And(x => x.Tng2 == item.Tng2);
                    predicate = predicate.And(x => x.Tng3 == item.Tng3);
                    int countDuplicateIdCodes = modifiedItems.Where(predicate.Compile()).Count();
                    if (countDuplicateIdCodes > 1)
                    {
                        throw new ArgumentException("Xâu nối mã " + item.XauNoiMa + " bị lặp, vui lòng thử lại");
                    }
                    // Nếu mã tồn tại và mã không thuộc danh sách bản ghi bị xóa thfi sẽ throw exception
                    if (CheckExistXNM(predicate, excludeIds))
                    {
                        throw new ArgumentException("Xâu nối mã " + item.XauNoiMa + " đã tồn tại, vui lòng thử lại");
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
            _mucLucNganSachRepository.AddOrUpdateRange(listEntities);
            UpdateDataDelete(lstIdParentDelete, authenticationInfo.YearOfWork);
        }

        private bool CheckExistXNM(Expression<Func<NsMucLucNganSach, bool>> predicate, IEnumerable<Guid> excludeIds)
        {
            // tìm bản ghi có cùng mã, cùng năm làm việc, cùng loại, không bao gồm các bản ghi sẽ được cập nhật
            predicate = predicate.And(t => !excludeIds.Contains(t.Id));
            IEnumerable<NsMucLucNganSach> danhMuc = _mucLucNganSachRepository.FindAll(predicate).ToList();
            return danhMuc.Count() != 0;
        }

        public int countMLNS(int namLamViec)
        {
            return _mucLucNganSachRepository.countMLNS(namLamViec);
        }

        public override IEnumerable<NsMucLucNganSach> FindAll(AuthenticationInfo authenticationInfo)
        {
            if (authenticationInfo.OptionalParam != null && authenticationInfo.OptionalParam.Length > 0 && authenticationInfo.OptionalParam[0] is DialogType dialogType)
            {
                if (DialogType.LoadMLNSOfSktMucLuc.Equals(dialogType))
                {
                    string sktKyHieu = authenticationInfo.OptionalParam[1].ToString();
                    return _mucLucNganSachRepository.FindBySktMucLucNotIn(sktKyHieu, authenticationInfo.YearOfWork);
                }
                else if (DialogType.LoadMLNSOfNsDonVi.Equals(dialogType))
                {
                    IEnumerable<string> excludeMLNS = authenticationInfo.OptionalParam[1] as IEnumerable<string>;
                    return _mucLucNganSachRepository.FindLNSStartWith2ByNsDonVi(excludeMLNS, authenticationInfo.YearOfWork);
                }
            }
            var predicate = PredicateBuilder.True<NsMucLucNganSach>();
            predicate = predicate.And(x => authenticationInfo.YearOfWork == x.NamLamViec);
            predicate = predicate.And(x => x.Lns == "1010000");
            return _mucLucNganSachRepository.FindAll(predicate).OrderBy(p => p.XauNoiMa);
        }

        public List<NsMucLucNganSach> FindByLNS(string lns)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NsMucLucNganSach> GetLoaiNganSachByNamLamViec(int iNamLamViec)
        {
            throw new NotImplementedException();
        }

        public string GetXNM(NsMucLucNganSach nsMucLucNganSach)
        {
            StringBuilder xnm = new StringBuilder();
            if (string.IsNullOrEmpty(nsMucLucNganSach.Lns))
            {
                return xnm.ToString();
            }
            xnm.Append(nsMucLucNganSach.Lns);
            if (string.IsNullOrEmpty(nsMucLucNganSach.L))
            {
                return xnm.ToString();
            }
            xnm.Append("-").Append(nsMucLucNganSach.L);
            if (string.IsNullOrEmpty(nsMucLucNganSach.K))
            {
                return xnm.ToString();
            }
            xnm.Append("-").Append(nsMucLucNganSach.K);
            if (string.IsNullOrEmpty(nsMucLucNganSach.M))
            {
                return xnm.ToString();
            }
            xnm.Append("-").Append(nsMucLucNganSach.M);
            if (string.IsNullOrEmpty(nsMucLucNganSach.Tm))
            {
                return xnm.ToString();
            }
            xnm.Append("-").Append(nsMucLucNganSach.Tm);
            if (string.IsNullOrEmpty(nsMucLucNganSach.Ttm))
            {
                return xnm.ToString();
            }
            xnm.Append("-").Append(nsMucLucNganSach.Ttm);
            if (string.IsNullOrEmpty(nsMucLucNganSach.Ng))
            {
                return xnm.ToString();
            }
            xnm.Append("-").Append(nsMucLucNganSach.Ng);
            if (string.IsNullOrEmpty(nsMucLucNganSach.Tng))
            {
                return xnm.ToString();
            }
            xnm.Append("-").Append(nsMucLucNganSach.Tng);
            if (string.IsNullOrEmpty(nsMucLucNganSach.Tng1))
            {
                return xnm.ToString();
            }
            xnm.Append("-").Append(nsMucLucNganSach.Tng1);
            if (string.IsNullOrEmpty(nsMucLucNganSach.Tng2))
            {
                return xnm.ToString();
            }
            xnm.Append("-").Append(nsMucLucNganSach.Tng2);
            if (string.IsNullOrEmpty(nsMucLucNganSach.Tng3))
            {
                return xnm.ToString();
            }
            xnm.Append("-").Append(nsMucLucNganSach.Tng3);
            return xnm.ToString();
        }

        private void UpdateDataDelete(List<Guid> lstParentId, int iNamLamViec)
        {
            _mucLucNganSachRepository.UpdateIsHangChaMucLucNganSach(lstParentId, iNamLamViec);
        }
    }
}
