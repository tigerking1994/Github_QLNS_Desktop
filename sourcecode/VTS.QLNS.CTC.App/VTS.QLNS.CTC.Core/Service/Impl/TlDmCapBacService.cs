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
    public class TlDmCapBacService : IService<TlDmCapBac>, ITlDmCapBacService
    {
        private ITlDmCapBacRepository _tlDmCapBacRepository;
        public TlDmCapBacService(ITlDmCapBacRepository tlDmCapBacRepository)
        {
            _tlDmCapBacRepository = tlDmCapBacRepository;
        }

        public override void AddOrUpdateRange(IEnumerable<TlDmCapBac> listEntities, AuthenticationInfo authenticationInfo)
        {
            var lstPhuCapChange = new List<string>();

            if (listEntities != null && listEntities.Any(n => n.IsDeleted))
            {
                _tlDmCapBacRepository.UpdateCanBoPhuCapWhenChangeCapBac(authenticationInfo.Month, authenticationInfo.YearOfWork, listEntities.Where(n => n.IsDeleted).Select(n => n.Id).ToList(), true, string.Join(",", lstPhuCapChange));
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

                        var oldCapBac = _tlDmCapBacRepository.FindByMaCapBac(item.MaCb);
                        var capBacChange = GetChangedProperties<TlDmCapBac>(item, oldCapBac);
                        capBacChange.ForAll(n =>
                        {
                            if (mapPhuCap.ContainsKey(n))
                            {
                                lstPhuCapChange.AddRange(mapPhuCap[n]);
                            };
                        });
                    }

                    _tlDmCapBacRepository.AddOrUpdateRange(listEntities);
                    foreach (var item in listModified)
                    {
                        _tlDmCapBacRepository.UpdateCanBoPhuCapWhenChangeCapBac(authenticationInfo.Month, authenticationInfo.YearOfWork, new List<Guid> { item.Id }, false, string.Join(",", lstPhuCapChange));
                    }
                }
                else
                {
                    _tlDmCapBacRepository.AddOrUpdateRange(listEntities);
                }
            }
        }

        public IEnumerable<TlDmCapBac> FindAll()
        {
            return _tlDmCapBacRepository.FindAll();
        }

        public override IEnumerable<TlDmCapBac> FindAll(AuthenticationInfo authenticationInfo)
        {
            return _tlDmCapBacRepository.FindAll().OrderBy(x => x.XauNoiMa).ToList();
        }

        public TlDmCapBac FindById(Guid id)
        {
            return _tlDmCapBacRepository.Find(id);
        }

        public TlDmCapBac FindByMaCapBac(string maCapBac)
        {
            return _tlDmCapBacRepository.FindByMaCapBac(maCapBac);
        }

        public IEnumerable<TlDmCapBac> FindByNote()
        {
            return _tlDmCapBacRepository.FindByNote().OrderBy(x => x.XauNoiMa);
        }

        public IEnumerable<TlDmCapBac> FindParent()
        {
            return _tlDmCapBacRepository.FindParent();
        }

        public IEnumerable<TlDmCapBac> FindAll(Expression<Func<TlDmCapBac, bool>> predicate)
        {
            return _tlDmCapBacRepository.FindAll(predicate);
        }

        public void UpdateCanBoPhuCapWhenChangeCapBac(int iThang, int iNam, List<Guid> lstIdCapBac, bool bIsDelete, string sMaPhuCapChange)
        {
            _tlDmCapBacRepository.UpdateCanBoPhuCapWhenChangeCapBac(iThang, iNam, lstIdCapBac, bIsDelete, sMaPhuCapChange);
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
    }
}
