using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository.Impl;
using VTS.QLNS.CTC.Utility;


namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlDmCapBacNq104Service : IService<TlDmCapBacNq104>, ITlDmCapBacNq104Service
    {
        private ITlDmCapBacNq104Repository _TlDmCapBacNq104Repository;
        public TlDmCapBacNq104Service(ITlDmCapBacNq104Repository TlDmCapBacNq104Repository)
        {
            _TlDmCapBacNq104Repository = TlDmCapBacNq104Repository;
        }

        public override void AddOrUpdateRange(IEnumerable<TlDmCapBacNq104> listEntities, AuthenticationInfo authenticationInfo)
        {
            var lstPhuCapChange = new List<string>();

            if (listEntities != null && listEntities.Any(n => n.IsDeleted))
            {
                _TlDmCapBacNq104Repository.UpdateCanBoPhuCapWhenChangeCapBac(authenticationInfo.Month, authenticationInfo.YearOfWork, listEntities.Where(n => n.IsDeleted).Select(n => n.Id).ToList(), true, string.Join(",", lstPhuCapChange));
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

                        var oldCapBac = _TlDmCapBacNq104Repository.FindByMaCapBac(item.MaCb);
                        var capBacChange = GetChangedProperties<TlDmCapBacNq104>(item, oldCapBac);
                        capBacChange.ForAll(n =>
                        {
                            if (mapPhuCap.ContainsKey(n))
                            {
                                lstPhuCapChange.AddRange(mapPhuCap[n]);
                            };
                        });
                    }

                    _TlDmCapBacNq104Repository.AddOrUpdateRange(listEntities);
                    foreach (var item in listModified)
                    {
                        _TlDmCapBacNq104Repository.UpdateCanBoPhuCapWhenChangeCapBac(authenticationInfo.Month, authenticationInfo.YearOfWork, new List<Guid> { item.Id }, false, string.Join(",", lstPhuCapChange));
                    }
                }
                else
                {
                    _TlDmCapBacNq104Repository.AddOrUpdateRange(listEntities);
                }
            }
        }

        public int AddOrUpdateRange(IEnumerable<TlDmCapBacNq104> entities)
        {
            return _TlDmCapBacNq104Repository.AddOrUpdateRange(entities);
        }
        public IEnumerable<TlDmCapBacNq104> FindAll()
        {
            return _TlDmCapBacNq104Repository.FindAll();
        }

        public override IEnumerable<TlDmCapBacNq104> FindAll(AuthenticationInfo authenticationInfo)
        {
            return _TlDmCapBacNq104Repository.FindAll().OrderBy(x => x.XauNoiMa).ToList();
        }

        public TlDmCapBacNq104 FindById(Guid id)
        {
            return _TlDmCapBacNq104Repository.Find(id);
        }

        public TlDmCapBacNq104 FindByMaCapBac(string maCapBac)
        {
            return _TlDmCapBacNq104Repository.FindByMaCapBac(maCapBac);
        }

        public IEnumerable<TlDmCapBacNq104> FindByNote()
        {
            return _TlDmCapBacNq104Repository.FindByNote().OrderBy(x => x.XauNoiMa);
        }

        public IEnumerable<TlDmCapBacNq104> FindParent()
        {
            return _TlDmCapBacNq104Repository.FindParent();
        }

        public IEnumerable<TlDmCapBacNq104> FindAll(Expression<Func<TlDmCapBacNq104, bool>> predicate)
        {
            return _TlDmCapBacNq104Repository.FindAll(predicate);
        }

        public void UpdateCanBoPhuCapWhenChangeCapBac(int iThang, int iNam, List<Guid> lstIdCapBac, bool bIsDelete, string sMaPhuCapChange)
        {
            _TlDmCapBacNq104Repository.UpdateCanBoPhuCapWhenChangeCapBac(iThang, iNam, lstIdCapBac, bIsDelete, sMaPhuCapChange);
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

        public IEnumerable<RptGiayGTTaiChinhLoaiNhomQuery> FindByTenLoaiAndTenNhom(int nam, int thang, string maCanBo,string maDonVi)
        {
            return _TlDmCapBacNq104Repository.FindByTenLoaiAndTenNhom(nam, thang, maCanBo, maDonVi);
        }
    }
}
