using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlDmPhuCapNq104Service : IService<TlDmPhuCapNq104>, ITlDmPhuCapNq104Service
    {
        private readonly ITlDmPhuCapNq104Repository _tlDmPhuCapNq104Repository;
        private readonly ITlDmCanBoRepository _tlDmCanBoRepository;
        private readonly ITlCanBoPhuCapNq104Repository _tlCanBoPhuCapNq104Repository;
        private readonly ILog _logger;

        public TlDmPhuCapNq104Service(
            ILog logger,
            ITlDmPhuCapNq104Repository tlDmPhuCapNq104Repository,
            ITlDmCanBoRepository tlDmCanBoRepository,
            ITlCanBoPhuCapNq104Repository tlCanBoPhuCapNq104Repository)
        {
            _logger = logger;
            _tlDmPhuCapNq104Repository = tlDmPhuCapNq104Repository;
            _tlDmCanBoRepository = tlDmCanBoRepository;
            _tlCanBoPhuCapNq104Repository = tlCanBoPhuCapNq104Repository;
        }

        public override void AddOrUpdateRange(IEnumerable<TlDmPhuCapNq104> listEntities, AuthenticationInfo authenticationInfo, Action<int> func)
        {
            foreach (var item in listEntities)
            {
                item.XauNoiMa = !string.IsNullOrEmpty(item.Parent) ? item.Parent + "-" + item.MaPhuCap : item.MaPhuCap;
            }

            var listCanBo = _tlDmCanBoRepository.FindAll(x => x.Nam > authenticationInfo.YearOfWork
                || (x.Thang >= authenticationInfo.Month && x.Nam == authenticationInfo.YearOfWork)).ToList();
            var canBo = listCanBo.Select(x => x.MaCanBo).ToHashSet();

            var listPhuCapCanBo = _tlCanBoPhuCapNq104Repository.FindAll(x => canBo.Contains(x.MaCbo));
            var listPccb = listPhuCapCanBo.AsParallel();
            int count1 = listCanBo.Count();
            int count2 = listPccb.Count();
            int progress = 0;
            var tenNganHang = listEntities.FirstOrDefault(x => x.MaPhuCap == "TENNGANHANG");
            if (tenNganHang != null)
            {
                foreach (var entity in listCanBo)
                {
                    func(progress++ * 50 / count1);
                    entity.TenKhoBac = tenNganHang.TenNganHang;
                }
                _tlDmCanBoRepository.BulkUpdate(listCanBo);
            }
            try
            {
                Parallel.ForEach(listPccb, data =>
                {
                    func(Interlocked.Increment(ref progress) * 50 / count2 + 50);
                    var res = JsonConvert.DeserializeObject<AllowenceCanBoNq104Criteria>(CompressExtension.DecompressFromBase64(data.Data)).X.AsParallel();
                    var newList = new BlockingCollection<AllowencePhuCapNq104Criteria>();
                    Parallel.ForEach(res, datum =>
                    {
                        var phuCap = listEntities.FirstOrDefault(x => x.MaPhuCap == datum.A);
                        if (phuCap is null)
                        {
                            newList.Add(datum);
                        }
                        else if (!phuCap.IsDeleted)
                        {
                            newList.Add(new AllowencePhuCapNq104Criteria()
                            {
                                A = datum.A,
                                B = phuCap.GiaTri,
                                C = phuCap.HuongPCSN
                            });
                        }
                    });
                    data.Data = CompressExtension.CompressToBase64(JsonConvert.SerializeObject(new AllowenceCanBoNq104Criteria()
                    {
                        X = newList
                    }));
                    data.IsModified = true;
                });
                _tlDmPhuCapNq104Repository.AddOrUpdateRange(listEntities);
                _tlCanBoPhuCapNq104Repository.BulkUpdate(listPhuCapCanBo.ToList());
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public override IEnumerable<TlDmPhuCapNq104> FindAll(AuthenticationInfo authenticationInfo)
        {
            return _tlDmPhuCapNq104Repository.FindAll().Where(x => x.MaPhuCap != "HETHONG" || x.Parent != "HETHONG").OrderBy(x => x.XauNoiMa).ToList();
        }

        public TlDmPhuCapNq104 FindById(string id)
        {
            return _tlDmPhuCapNq104Repository.Find(id);
        }

        public IEnumerable<TlDmPhuCapNq104> FindAll()
        {
            return _tlDmPhuCapNq104Repository.FindAll();
        }

        public IEnumerable<TlDmPhuCapNq104> FindByCondition()
        {
            return _tlDmPhuCapNq104Repository.FindByCondition();
        }

        public IEnumerable<TlDmPhuCapNq104> FindByCondition(Expression<Func<TlDmPhuCapNq104, bool>> predicate)
        {
            return _tlDmPhuCapNq104Repository.FindAll(predicate);
        }

        public IEnumerable<TlDmPhuCapNq104> FindByHeThong()
        {
            return _tlDmPhuCapNq104Repository.FindByHeThong();
        }

        public TlDmPhuCapNq104 FindByMaPhuCap(string maPhuCap)
        {
            return _tlDmPhuCapNq104Repository.FindByMaPhuCap(maPhuCap);
        }

        public IEnumerable<TlDmPhuCapNq104> GetDmPhuCapInDcTapTheCanBo()
        {
            return _tlDmPhuCapNq104Repository.GetDmPhuCapInDcTapTheCanBo();
        }

        public IEnumerable<TlDmPhuCapNq104> FindAll(Expression<Func<TlDmPhuCapNq104, bool>> predicate)
        {
            return _tlDmPhuCapNq104Repository.FindAll(predicate);
        }

        public IEnumerable<TlDmPhuCapNq104> FindHasDataBangLuong(int nam, int thang, string maCachTl)
        {
            return _tlDmPhuCapNq104Repository.FindHasDataBangLuong(nam, thang, maCachTl);
        }

        public override bool CheckPhuCapExist(string maPhuCap, Guid? iId)
        {
            return _tlDmPhuCapNq104Repository.CheckPhuCapExist(maPhuCap, iId ?? Guid.Empty);
        }

        public void UpdateCanBoPhuCapWhenChangePhuCap(int iThang, int iNam, List<Guid> lstIdPhuCap, bool bIsDelete)
        {
            _tlDmPhuCapNq104Repository.UpdateCanBoPhuCapWhenChangePhuCap(iThang, iNam, lstIdPhuCap, bIsDelete);
        }

        public IEnumerable<TlPhuCapNq104Query> FindAllPhuCapVaCheDoBHXH()
        {
            return _tlDmPhuCapNq104Repository.FindAllPhuCapVaCheDoBHXH();
        }
        public IEnumerable<TlDmPhuCapNq104> FindByIdThuNopBhxh(Guid id)
        {
            return _tlDmPhuCapNq104Repository.FindByIdThuNopBhxh(id);
        }
        public override IEnumerable<TlDmPhuCapNq104> FindDmPhuCapNq104(int namLamViec)
        {
            return _tlDmPhuCapNq104Repository.FindAll(x => x.Nam == namLamViec).OrderBy(x => x.XauNoiMa);
        }
    }
}
