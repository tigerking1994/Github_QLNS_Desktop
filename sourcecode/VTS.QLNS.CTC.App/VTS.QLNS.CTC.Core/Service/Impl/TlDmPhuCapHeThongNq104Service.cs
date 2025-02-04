using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlDmPhuCapHeThongNq104Service : IService<TlDmPhuCapNq104>, ITlDmPhuCapHeThongService
    {
        private readonly ITlDmPhuCapNq104Repository _tlDmPhuCapNq104Repository;
        private readonly ITlDmCanBoService _tlDmCanBoService;
        private readonly ITlCanBoPhuCapNq104Repository _tlCanBoPhuCapNq104Repository;
        private readonly ILog _logger;
        public TlDmPhuCapHeThongNq104Service(ILog logger,
            ITlDmPhuCapNq104Repository tlDmPhuCapNq104Repository,
            ITlDmCanBoService tlDmCanBoService,
            ITlCanBoPhuCapNq104Repository tlCanBoPhuCapNq104Repository)
        {
            _logger = logger;
            _tlDmPhuCapNq104Repository = tlDmPhuCapNq104Repository;
            _tlDmCanBoService = tlDmCanBoService;
            _tlCanBoPhuCapNq104Repository = tlCanBoPhuCapNq104Repository;
        }

        public override void AddOrUpdateRange(IEnumerable<TlDmPhuCapNq104> listEntities, AuthenticationInfo authenticationInfo)
        {
            var listCanBo = _tlDmCanBoService.FindAll(x => x.Nam > authenticationInfo.YearOfWork
                            || (x.Thang >= authenticationInfo.Month && x.Nam == authenticationInfo.YearOfWork)).ToList();

            var newList = new BlockingCollection<AllowencePhuCapNq104Criteria>();
            var listPhuCapCanBo = _tlCanBoPhuCapNq104Repository.FindAll(x => listCanBo.Select(y => y.MaCanBo).Contains(x.MaCbo));
            var listPccb = listPhuCapCanBo.AsParallel();

            var tenNganHang = listEntities.FirstOrDefault(x => x.MaPhuCap == "TENNGANHANG");
            if (tenNganHang != null)
            {
                foreach (var entity in listCanBo)
                {
                    entity.TenKhoBac = tenNganHang.TenNganHang;
                }
            }
            try
            {
                Parallel.ForEach(listPccb, data =>
                {
                    var res = JsonConvert.DeserializeObject<AllowenceCanBoNq104Criteria>(CompressExtension.DecompressFromBase64(data.Data)).X.AsParallel();
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
                _tlDmCanBoService.UpdateRange(listCanBo);
                _tlCanBoPhuCapNq104Repository.UpdateRange(listPhuCapCanBo);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public override IEnumerable<TlDmPhuCapNq104> FindAll(AuthenticationInfo authenticationInfo)
        {
            return _tlDmPhuCapNq104Repository.FindAll().Where(x => x.MaPhuCap == "HETHONG" || x.Parent == "HETHONG").OrderBy(x => x.XauNoiMa).ToList();
        }

        public override IEnumerable<TlDmPhuCapNq104> FindAllPhuCapHeThong(AuthenticationInfo authenticationInfo)
        {
            return _tlDmPhuCapNq104Repository.FindAllPhuCapHeThong();
        }
    }
}
