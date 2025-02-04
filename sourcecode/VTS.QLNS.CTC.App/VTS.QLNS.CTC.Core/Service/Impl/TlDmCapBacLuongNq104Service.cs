using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository.Impl;
using VTS.QLNS.CTC.Utility;


namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlDmCapBacLuongNq104Service : IService<TlDmCapBacLuongNq104>, ITlDmCapBacLuongNq104Service
    {
        private ITlDmCapBacLuongNq104Repository _tlDmCapBacLuongNq104Repository;
        public TlDmCapBacLuongNq104Service(ITlDmCapBacLuongNq104Repository tlDmCapBacLuongNq104Repository)
        {
            _tlDmCapBacLuongNq104Repository = tlDmCapBacLuongNq104Repository;
        }

        public int AddOrUpdateRange(IEnumerable<TlDmCapBacLuongNq104> entities)
        {
            return _tlDmCapBacLuongNq104Repository.AddOrUpdateRange(entities);
        }
        public override void AddOrUpdateRange(IEnumerable<TlDmCapBacLuongNq104> listEntities, AuthenticationInfo authenticationInfo)
        {
            var lstPhuCapChange = new List<string>();

            if (listEntities != null && listEntities.Any(n => n.IsDeleted))
            {
                _tlDmCapBacLuongNq104Repository.UpdateCanBoPhuCapWhenChangeCapBac(authenticationInfo.Month, authenticationInfo.YearOfWork, listEntities.Where(n => n.IsDeleted).Select(n => n.Id).ToList(), true, string.Join(",", lstPhuCapChange));
            }
            if (listEntities != null)
            {
                if (listEntities.Any(n => !n.IsDeleted))
                {
                    var listModified = listEntities.Where(n => !n.IsDeleted && n.Id != Guid.Empty);
                    foreach (var item in listModified)
                    {
                        var mapPhuCap = new Dictionary<string, List<string>>() {
                        { "BhxhCq", new List<string>(){ "BHXHDV_HS", "BHXHDVCS_HS" } },
                        { "HsBhxh", new List<string>(){ "BHXHCN_HS" } },
                        { "BhytCq", new List<string>(){ "BHYTDV_HS", "BHYTDVCS_HS" } },
                        { "HsBhyt", new List<string>(){ "BHYTCN_HS" } },
                        { "BhtnCq", new List<string>(){ "BHTNDV_HS" } },
                        { "HsBhtn", new List<string>(){ "BHTNCN_HS" } },
                        { "LhtHs",  new List<string>(){ "LHT_HS" } },
                        { "TiLeHuong", new List<string>(){ "TILE_HUONG" } },
                    };

                        var oldCapBac = _tlDmCapBacLuongNq104Repository.FindByMaCapBac(item.MaDm, item.Nam);
                        var capBacChange = GetChangedProperties<TlDmCapBacNq104>(item, oldCapBac);
                        capBacChange.ForAll(n =>
                        {
                            if (mapPhuCap.ContainsKey(n))
                            {
                                lstPhuCapChange.AddRange(mapPhuCap[n]);
                            };
                        });
                    }

                    _tlDmCapBacLuongNq104Repository.AddOrUpdateRange(listEntities);
                    foreach (var item in listModified)
                    {
                        _tlDmCapBacLuongNq104Repository.UpdateCanBoPhuCapWhenChangeCapBac(authenticationInfo.Month, authenticationInfo.YearOfWork, new List<Guid> { item.Id }, false, string.Join(",", lstPhuCapChange));
                    }
                }
                else
                {
                    _tlDmCapBacLuongNq104Repository.AddOrUpdateRange(listEntities);
                }
            }
        }

        public IEnumerable<TlDmCapBacLuongNq104> FindAll()
        {
            return _tlDmCapBacLuongNq104Repository.FindAll();
        }

        public override IEnumerable<TlDmCapBacLuongNq104> FindAll(AuthenticationInfo authenticationInfo)
        {
            return _tlDmCapBacLuongNq104Repository.FindAll().ToList();
        }

        public TlDmCapBacLuongNq104 FindById(Guid id)
        {
            return _tlDmCapBacLuongNq104Repository.Find(id);
        }

        public TlDmCapBacLuongNq104 FindByMaCapBac(string maCapBac, int? nam)
        {
            return _tlDmCapBacLuongNq104Repository.FindByMaCapBac(maCapBac, nam);
        }

        public TlDmCapBacLuongNq104 FindByXauNoiMa(string xauNoiMa, int? nam)
        {
            return _tlDmCapBacLuongNq104Repository.FindByXauNoiMa(xauNoiMa, nam);
        }

        public IEnumerable<TlDmCapBacLuongNq104> FindByNote()
        {
            return _tlDmCapBacLuongNq104Repository.FindByNote();
        }

        public IEnumerable<TlDmCapBacLuongNq104> FindParent()
        {
            return _tlDmCapBacLuongNq104Repository.FindParent();
        }

        public IEnumerable<TlDmCapBacLuongNq104> FindAll(Expression<Func<TlDmCapBacLuongNq104, bool>> predicate)
        {
            return _tlDmCapBacLuongNq104Repository.FindAll(predicate);
        }

        public void UpdateCanBoPhuCapWhenChangeCapBac(int iThang, int iNam, List<Guid> lstIdCapBac, bool bIsDelete, string sMaPhuCapChange)
        {
            _tlDmCapBacLuongNq104Repository.UpdateCanBoPhuCapWhenChangeCapBac(iThang, iNam, lstIdCapBac, bIsDelete, sMaPhuCapChange);
        }

        private List<string> GetChangedProperties<T>(object A, object B)
        {
            if (A != null && B != null)
            {
                var type = typeof(T);
                var allProperties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                var unequalProperties =
                       from pi in allProperties
                       let AValue = type.GetProperty(pi.Name).GetValue(A, null)
                       let BValue = type.GetProperty(pi.Name).GetValue(B, null)
                       where AValue != BValue && (AValue == null || !AValue.Equals(BValue))
                       select pi.Name;
                return unequalProperties.ToList();
            }
            else
            {
                throw new ArgumentNullException("Thiếu tham số so sánh");
            }
        }

        public IEnumerable<TlDmCapBacLuongNq104> FindAllByXauNoiMa(string xauNoiMa, int nam)
        {
            return _tlDmCapBacLuongNq104Repository.FindAllByXauNoiMa(xauNoiMa, nam);
        }
    }
}
