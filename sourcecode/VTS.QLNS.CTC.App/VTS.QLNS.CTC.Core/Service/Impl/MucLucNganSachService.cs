using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class MucLucNganSachService : IService<NsMucLucNganSach>, IMucLucNganSachService
    {
        private readonly IMucLucNganSachRepository _mucLucNganSachRepository;
        public IDanhMucRepository _danhMucRepository;

        public MucLucNganSachService(IMucLucNganSachRepository mucLucNganSachRepository, IDanhMucRepository danhMucRepository)
        {
            _mucLucNganSachRepository = mucLucNganSachRepository;
            _danhMucRepository = danhMucRepository;
        }

        public override void AddOrUpdateRange(IEnumerable<NsMucLucNganSach> listEntities, AuthenticationInfo authenticationInfo)
        {
            var time = DateTime.Now;
            IEnumerable<Guid> excludeIds = listEntities.Select(i => i.Id).ToList();
            IEnumerable<NsMucLucNganSach> modifiedItems = listEntities.Where(i => !i.IsDeleted).ToList();
            foreach (var item in listEntities)
            {
                item.NamLamViec = authenticationInfo.YearOfWork;
                item.XauNoiMa = GetXNM(item);
                item.ILoai = getTypeOfMlns(item);
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
                    var countDuplicateIdCodes = modifiedItems.Where(predicate.Compile()).Count();
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
            _mucLucNganSachRepository.AddOrUpdateRange(listEntities, authenticationInfo.YearOfWork);
        }

        public override IEnumerable<NsMucLucNganSach> FindAll(AuthenticationInfo authenticationInfo)
        {
            if (authenticationInfo.OptionalParam != null && authenticationInfo.OptionalParam.Length > 0 && authenticationInfo.OptionalParam[0] is DialogType dialogType)
            {
                if (DialogType.LoadMLNSOfSktMucLuc.Equals(dialogType))
                {
                    var sktKyHieu = authenticationInfo.OptionalParam[1].ToString();
                    var rs = _mucLucNganSachRepository.FindBySktMucLucNotIn(sktKyHieu, authenticationInfo.YearOfWork);
                    rs = rs.Where(p => p.BHangChaDuToan.HasValue).OrderBy(p => p.XauNoiMa);
                    return rs.ToList();
                }
                else if (DialogType.LoadMLNSOfBHXH.Equals(dialogType))
                {
                    var mlnsLoai = authenticationInfo.OptionalParam[1].ToString();
                    var mlnsBHXH = authenticationInfo.OptionalParam[2].ToString();
                    var mlnsChosen = authenticationInfo.OptionalParam[3].ToString();
                    var rs = _mucLucNganSachRepository.FindByBHXHMucLucNotIn(authenticationInfo.YearOfWork, mlnsLoai, mlnsBHXH, mlnsChosen);
                    //rs = rs.Where(p => p.BHangChaDuToan.HasValue).OrderBy(p => p.XauNoiMa);
                    return rs.OrderBy(p => p.XauNoiMa).ToList();
                }
                else if (DialogType.LoadMLNSOfNsDonVi.Equals(dialogType))
                {
                    var excludeMLNS = authenticationInfo.OptionalParam[1] as IEnumerable<string>;
                    return _mucLucNganSachRepository.FindLNSStartWith2ByNsDonVi(excludeMLNS, authenticationInfo.YearOfWork).OrderBy(p => p.XauNoiMa);
                }
                else if (DialogType.LoadMLNSOfMLQTNam.Equals(dialogType))
                {
                    if (authenticationInfo.OptionalParam[2] is List<string> listMapExist)
                    {
                        var listMucLuc =  _mucLucNganSachRepository.FindAll(n =>
                            n.NamLamViec == authenticationInfo.YearOfWork
                            && !listMapExist.Contains(n.XauNoiMa)
                            ).OrderBy(p => p.XauNoiMa);
                        var dictParent = listMucLuc.Where(x => x.MlnsIdParent != null).GroupBy(x => x.MlnsIdParent).ToDictionary(x => x.Key);
                        return listMucLuc.Where(x =>
                        {
                            if (x.BHangCha && !dictParent.ContainsKey(x.MlnsId))
                            {
                                return false;
                            }
                            return true;
                        });
                    }
                    return _mucLucNganSachRepository.FindAll(n => n.NamLamViec == authenticationInfo.YearOfWork).OrderBy(p => p.XauNoiMa);
                }
            }

            var data = _mucLucNganSachRepository.FindByMLNSNamLamViec(authenticationInfo.YearOfWork).ToList();
            return data;
        }

        public override IEnumerable<NsMucLucNganSach> FindAllNew(IEnumerable<NsMucLucNganSach> listEntities, AuthenticationInfo authenticationInfo)
        {
            IEnumerable<NsMucLucNganSach> DbMlns = _mucLucNganSachRepository.FindAll().Where(r => r.NamLamViec == authenticationInfo.YearOfWork).ToList();
            var rs = listEntities.Where(r => !DbMlns.Any(t => t.SXauNoiMa == r.SXauNoiMa)).ToList();
            rs.ForEach(rs => rs.NamLamViec = authenticationInfo.YearOfWork);
            return rs;
        }

        public NsMucLucNganSach FindById(string Id)
        {
            var rs = _mucLucNganSachRepository.FindAll().Where(r => r.Id == new Guid(Id)).First();
            return rs;
        }

        public override string FindByBHXHMucLucIn(int namLamViec, string mlnsLoai, string mlnsBhxh)
        {
            return _mucLucNganSachRepository.FindByBHXHMucLucIn(namLamViec, mlnsLoai, mlnsBhxh);
        }

        public override IEnumerable<NsMucLucNganSach> FindAll(AuthenticationInfo authenticationInfo, bool isPopup, List<string> notIns)
        {
            var data = _mucLucNganSachRepository.FindAllNotIn(notIns, authenticationInfo.YearOfWork);
            return data;
        }

        public override void ImportDataExcel(IEnumerable<NsMucLucNganSach> listEntities, AuthenticationInfo authenticationInfo, int importMode)
        {
            var time = DateTime.Now;
            IEnumerable<NsMucLucNganSach> DbMlns = _mucLucNganSachRepository.FindAll().Where(r => r.NamLamViec == authenticationInfo.YearOfWork).ToList();
            var modifiedIds = listEntities.Select(i => i.Id);
            var entities = listEntities.ToList();
            for (var i = 0; i < entities.Count(); i++)
            {
                var item = entities[i];
                var track = DbMlns.FirstOrDefault(i => i.Id == item.Id);
                // bản ghi mới
                if (track == null)
                {
                    item.MlnsId = Guid.NewGuid();
                    item.DNgayTao = time;
                    item.SNguoiTao = authenticationInfo.Principal;
                    item.DNgaySua = null;
                    item.SNguoiSua = null;
                }
                else
                {
                    track.Lns = item.Lns;
                    track.K = item.K;
                    track.L = item.L;
                    track.M = item.M;
                    track.Tm = item.Tm;
                    track.Ttm = item.Ttm;
                    track.Ng = item.Ng;
                    track.Tng = item.Tng;
                    track.MoTa = item.MoTa;
                    track.NamLamViec = item.NamLamViec;
                    track.DNgaySua = time;
                    track.SNguoiSua = authenticationInfo.Principal;
                    item = ObjectCopier.Clone(track);
                }
                item.XauNoiMa = StringUtils.Join(StringUtils.DIVISION, item.Lns, item.L, item.K, item.M, item.Tm, item.Ttm, item.Ng, item.Tng);
                item.IsModified = true;
                entities[i] = item;
            }
            for (var i = 0; i < entities.Count(); i++)
            {
                var item = entities[i];
                if (Guid.Empty.Equals(item.Id))
                {
                    var parent = FindParentByXNM(item.XauNoiMa, entities, DbMlns, modifiedIds, item.NamLamViec.Value);
                    if (parent != null)
                    {
                        item.MlnsIdParent = parent.MlnsId;
                    }
                    else
                    {
                        item.MlnsIdParent = Guid.Empty;
                    }
                    item.BHangCha = IsHangCha(item, entities, DbMlns);
                    if (string.IsNullOrEmpty(item.Tng) && !string.IsNullOrEmpty(item.Ng))
                    {
                        item.BHangChaDuToan = false;
                        item.BHangChaQuyetToan = false;
                        item.SDuToanChiTietToi = "NG";
                        item.SQuyetToanChiTietToi = "NG";
                    }
                    else if (!string.IsNullOrEmpty(item.Tng))
                    {
                        item.SDuToanChiTietToi = "";
                        item.SQuyetToanChiTietToi = "";
                    }
                }
            }
            _mucLucNganSachRepository.AddOrUpdateRange(entities);
        }

        private bool IsHangCha(NsMucLucNganSach nsMucLucNganSach, IEnumerable<NsMucLucNganSach> listEntities, IEnumerable<NsMucLucNganSach> DbMlns)
        {
            if (!string.IsNullOrEmpty(nsMucLucNganSach.SXauNoiMa) && nsMucLucNganSach.SXauNoiMa.Length < 6) return IsParentRoot(nsMucLucNganSach, listEntities, DbMlns, nsMucLucNganSach.NamLamViec ?? 0);
            return listEntities.Any(i => i.XauNoiMa.StartsWith(nsMucLucNganSach.XauNoiMa + "-") && nsMucLucNganSach.NamLamViec == i.NamLamViec)
                || DbMlns.Any(i => i.XauNoiMa.StartsWith(nsMucLucNganSach.XauNoiMa + "-") && nsMucLucNganSach.NamLamViec == i.NamLamViec);
        }

        public override bool ValidateDataExcel(IEnumerable<NsMucLucNganSach> listEntities, AuthenticationInfo authenticationInfo, int importMode)
        {
            IEnumerable<Guid> excludeIds = listEntities.Select(i => i.Id).ToList();
            IEnumerable<NsMucLucNganSach> DbMlns = _mucLucNganSachRepository.FindAll().Where(r => r.NamLamViec == authenticationInfo.YearOfWork).ToList();
            foreach (var item in listEntities)
            {
                var predicate = PredicateBuilder.True<NsMucLucNganSach>();
                item.XauNoiMa = StringUtils.Join(StringUtils.DIVISION, item.Lns, item.L, item.K, item.M, item.Tm, item.Ttm, item.Ng, item.Tng);
                predicate = predicate.And(x => x.NamLamViec == item.NamLamViec);
                predicate = predicate.And(x => x.Lns == item.Lns);
                predicate = predicate.And(x => x.L == item.L);
                predicate = predicate.And(x => x.K == item.K);
                predicate = predicate.And(x => x.M == item.M);
                predicate = predicate.And(x => x.Tm == item.Tm);
                predicate = predicate.And(x => x.Ttm == item.Ttm);
                predicate = predicate.And(x => x.Ng == item.Ng);
                predicate = predicate.And(x => x.Tng == item.Tng);
                var countDuplicateIdCodes = listEntities.Where(predicate.Compile()).Count();
                if (countDuplicateIdCodes > 1)
                {
                    throw new ArgumentException("Xâu nối mã " + item.XauNoiMa + " bị lặp, vui lòng thử lại");
                }
                //// Nếu mã tồn tại và mã không thuộc danh sách bản ghi bị xóa thi sẽ throw exception
                //if (CheckExistXNM(predicate, excludeIds, DbMlns))
                //{
                //    throw new ArgumentException("Xâu nối mã " + item.XauNoiMa + " đã tồn tại, vui lòng thử lại");
                //}
            }
            return true;
        }

        private NsMucLucNganSach FindParentByXNM(string xnm,
            IEnumerable<NsMucLucNganSach> modifiedMlns, IEnumerable<NsMucLucNganSach> DbMlns, IEnumerable<Guid> excludeIds, int namLamViec)
        {
            IEnumerable<NsMucLucNganSach> ancestors = modifiedMlns.Where(i => xnm.StartsWith(i.XauNoiMa + "-") && namLamViec == i.NamLamViec)
                .OrderByDescending(i => i.XauNoiMa.Length);
            var parentInModifiedList = ancestors.FirstOrDefault();

            IEnumerable<NsMucLucNganSach> DbAncestors = DbMlns.Where(i => !excludeIds.Contains(i.Id) && namLamViec == i.NamLamViec &&
                xnm.StartsWith(i.XauNoiMa + "-")).OrderByDescending(i => i.XauNoiMa.Length);
            var parentInDbList = DbAncestors.FirstOrDefault();

            if ((parentInModifiedList is null || parentInDbList is null) && xnm.Length < 8)
            {
                parentInModifiedList = GetParentRoot(xnm, modifiedMlns, namLamViec);
                parentInDbList = GetParentRoot(xnm, DbAncestors, namLamViec);
            }

            if (parentInModifiedList != null && (parentInDbList == null || parentInModifiedList.XauNoiMa.Length > parentInDbList.XauNoiMa.Length))
            {
                return parentInModifiedList;
            }
            return parentInDbList;
        }

        private NsMucLucNganSach GetParentRoot(string xnm, IEnumerable<NsMucLucNganSach> modifiedMlns, int namLamViec)
        {
            return xnm.Length switch
            {
                NSConstants.MLNS_LENGTH_3 => modifiedMlns.FirstOrDefault(x => !string.IsNullOrEmpty(x.SXauNoiMa) && NSConstants.MLNS_LENGTH_1.Equals(x.SXauNoiMa.Length) && x.SXauNoiMa.Equals(xnm.Substring(0, 1)) && namLamViec.Equals(x.NamLamViec)),

                NSConstants.MLNS_LENGTH_5 => modifiedMlns.FirstOrDefault(x => !string.IsNullOrEmpty(x.SXauNoiMa) && NSConstants.MLNS_LENGTH_3.Equals(x.SXauNoiMa.Length) && x.SXauNoiMa.Equals(xnm.Substring(0, 3)) && namLamViec.Equals(x.NamLamViec)),

                NSConstants.MLNS_LENGTH_7 => modifiedMlns.Any(x => !string.IsNullOrEmpty(x.SXauNoiMa) && NSConstants.MLNS_LENGTH_5.Equals(x.SXauNoiMa.Length) && x.SXauNoiMa.Equals(xnm.Substring(0, 5)) && namLamViec.Equals(x.NamLamViec)) ? modifiedMlns.FirstOrDefault(x => !string.IsNullOrEmpty(x.SXauNoiMa) && NSConstants.MLNS_LENGTH_5.Equals(x.SXauNoiMa.Length) && x.SXauNoiMa.Equals(xnm.Substring(0, 5)) && namLamViec.Equals(x.NamLamViec)) :
                 modifiedMlns.FirstOrDefault(x => !string.IsNullOrEmpty(x.SXauNoiMa) && NSConstants.MLNS_LENGTH_3.Equals(x.SXauNoiMa.Length) && x.SXauNoiMa.Equals(xnm.Substring(0, 3)) && namLamViec.Equals(x.NamLamViec)),
                _ => null,
            };
        }
        private bool IsParentRoot(NsMucLucNganSach item, IEnumerable<NsMucLucNganSach> modifiedMlns, IEnumerable<NsMucLucNganSach> dbMlns, int namLamViec)
        {
            return item.SXauNoiMa.Length switch
            {
                NSConstants.MLNS_LENGTH_1 => modifiedMlns.Any(x => !string.IsNullOrEmpty(x.SXauNoiMa) && NSConstants.MLNS_LENGTH_3.Equals(x.SXauNoiMa.Length) && x.SXauNoiMa.StartsWith(item.SXauNoiMa) && namLamViec.Equals(x.NamLamViec)),

                NSConstants.MLNS_LENGTH_3 => modifiedMlns.Any(x => !string.IsNullOrEmpty(x.SXauNoiMa) && NSConstants.MLNS_LENGTH_5.Equals(x.SXauNoiMa.Length) && x.SXauNoiMa.StartsWith(item.SXauNoiMa) && namLamViec.Equals(x.NamLamViec)) ? modifiedMlns.Any(x => !string.IsNullOrEmpty(x.SXauNoiMa) && NSConstants.MLNS_LENGTH_5.Equals(x.SXauNoiMa.Length) && x.SXauNoiMa.StartsWith(item.SXauNoiMa) && namLamViec.Equals(x.NamLamViec)) :
                modifiedMlns.Any(x => !string.IsNullOrEmpty(x.SXauNoiMa) && NSConstants.MLNS_LENGTH_7.Equals(x.SXauNoiMa.Length) && x.SXauNoiMa.StartsWith(item.SXauNoiMa) && namLamViec.Equals(x.NamLamViec))
                ,
                NSConstants.MLNS_LENGTH_5 => modifiedMlns.Any(x => !string.IsNullOrEmpty(x.SXauNoiMa) && NSConstants.MLNS_LENGTH_7.Equals(x.SXauNoiMa.Length) && x.SXauNoiMa.StartsWith(item.SXauNoiMa) && namLamViec.Equals(x.NamLamViec)),
                _ => false,
            };
        }
        /// <summary>
        /// Lấy danh sách mục lục ngân sách theo LNS
        /// </summary>
        /// <param name="lns"></param>
        /// <returns></returns>
        public List<NsMucLucNganSach> FindByLNS(string lns)
        {
            return _mucLucNganSachRepository.FindByLNS(lns);
        }

        public IEnumerable<NsMucLucNganSach> GetLoaiNganSachByNamLamViec(int iNamLamViec)
        {
            return _mucLucNganSachRepository.GetLoaiNganSachByNamLamViec(iNamLamViec);
        }

        public IEnumerable<NsMucLucNganSach> FindByIsHangCha(int year, bool isHangCha)
        {
            var predicate = PredicateBuilder.True<NsMucLucNganSach>();
            predicate = predicate.And(x => x.NamLamViec == year && x.BHangCha == isHangCha);
            return _mucLucNganSachRepository.FindAll(predicate).OrderBy(p => p.XauNoiMa);
        }

        public IEnumerable<ReportMLNSQuery> FindReportMLNSQuery(int year, Guid guid)
        {
            return _mucLucNganSachRepository.ReportMLNS(year, guid);
        }

        public virtual bool CheckExistXNM(Expression<Func<NsMucLucNganSach, bool>> predicate, IEnumerable<Guid> excludeIds)
        {
            // tìm bản ghi có cùng mã, cùng năm làm việc, cùng loại, không bao gồm các bản ghi sẽ được cập nhật
            predicate = predicate.And(t => !excludeIds.Contains(t.Id));
            IEnumerable<NsMucLucNganSach> danhMuc = _mucLucNganSachRepository.FindAll(predicate).ToList();
            return danhMuc.Count() != 0;
        }

        private bool CheckExistXNM(Expression<Func<NsMucLucNganSach, bool>> predicate, IEnumerable<Guid> excludeIds, IEnumerable<NsMucLucNganSach> DbMlns)
        {
            // tìm bản ghi có cùng mã, cùng năm làm việc, cùng loại, không bao gồm các bản ghi sẽ được cập nhật
            predicate = predicate.And(t => !excludeIds.Contains(t.Id));
            IEnumerable<NsMucLucNganSach> danhMuc = DbMlns.Where(predicate.Compile()).ToList();
            return danhMuc.Count() != 0;
        }

        public string GetXNM(NsMucLucNganSach nsMucLucNganSach)
        {
            var xnm = new StringBuilder();
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

        public override IEnumerable<NsMucLucNganSach> FindDataToExportTemplate(AuthenticationInfo authenticationInfo)
        {
            return _mucLucNganSachRepository.FindAll(i => i.NamLamViec == authenticationInfo.YearOfWork).OrderBy(p => p.XauNoiMa).ToList();
        }

        public int countMLNS(int namLamViec)
        {
            return _mucLucNganSachRepository.countMLNS(namLamViec);
        }

        public bool IsUsedMLNS(Guid mlnsId, int namLamViec)
        {
            return _mucLucNganSachRepository.IsUsedMLNS(mlnsId, namLamViec);
        }


        private readonly List<string> mlnsType = new List<string>
        {
            "Tng3", "Tng2", "Tng1", "Tng", "Ng", "Ttm", "Tm", "M", "K", "L", "Lns"
        };

        private string getTypeOfMlns(NsMucLucNganSach entity)
        {
            foreach (var type in mlnsType)
            {
                var propertyInfo = typeof(NsMucLucNganSach).GetProperty(type);
                var val = propertyInfo.GetValue(entity, null);
                if (val != null && !string.IsNullOrWhiteSpace(val.ToString()))
                {
                    return type;
                }
            }
            return "";
        }

        public DanhMuc FindMLNSChiTietToi(int namLamViec)
        {
            return _danhMucRepository.FindByCode("MLNS_CHITIET_TOI", namLamViec);
        }

        public IEnumerable<NsMucLucNganSach> GetAll()
        {
            return _mucLucNganSachRepository.FindAll();
        }

        public IEnumerable<MucLucNganSachCheckDataQuery> FindHasDataMLNS(int yearOfWork, string sXauNoiMa, int loai)
        {
            return _mucLucNganSachRepository.FindHasDataMLNS(yearOfWork, sXauNoiMa, loai);
        }
        public void DeleteHasDataMLNS(string sXauNoiMa, string loai, Guid uniqueidentifier)
        {
            _mucLucNganSachRepository.DeleteHasDataMLNS(sXauNoiMa, loai, uniqueidentifier);
        }

        public IEnumerable<NsMucLucNganSach> FinByCondition(Expression<Func<NsMucLucNganSach, bool>> predicate)
        {
            return _mucLucNganSachRepository.FindAll(predicate);
        }
        public NsMucLucNganSach FindByMLNS(string sXauNoiMa, int yearOfWork)
        {
            return _mucLucNganSachRepository.FindByMLNS(sXauNoiMa, yearOfWork);
        }
    }
}
