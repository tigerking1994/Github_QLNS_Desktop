using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlCanBoPhuCapBridgeNq104Service : ITlCanBoPhuCapBridgeNq104Service
    {
        private readonly ITlCanBoPhuCapBridgeNq104Repository _tlCanBoPhuCapBridgeRepository;
        private readonly ITlCanBoPhuCapNq104Repository _tlCanBoPhuCapRepository;

        public TlCanBoPhuCapBridgeNq104Service(
            ITlCanBoPhuCapBridgeNq104Repository tlCanBoPhuCapBridgeRepository,
            ITlCanBoPhuCapNq104Repository tlCanBoPhuCapRepository)
        {
            _tlCanBoPhuCapRepository = tlCanBoPhuCapRepository;
            _tlCanBoPhuCapBridgeRepository = tlCanBoPhuCapBridgeRepository;
        }

        public void BulkInsert(IEnumerable<TlCanBoPhuCapBridgeNq104> lstData)
        {
            _tlCanBoPhuCapBridgeRepository.BulkInsert(lstData);
        }

        public void DeleteAll()
        {
            _tlCanBoPhuCapBridgeRepository.DeleteAll();
        }

        public void DataPreprocess(int? thang = null, int? nam = null)
        {
            try
            {
                List<TlCanBoPhuCapNq104> listData = default;
                var predicate = PredicateBuilder.True<TlCanBoPhuCapNq104>();
                if (thang != null && nam != null)
                {
                    predicate = predicate.And(x => x.MaCbo.StartsWith($"{nam:D2}{thang:D2}"));
                }

                listData = _tlCanBoPhuCapRepository.FindAll(predicate).Where(z => !string.IsNullOrEmpty(z.Data)).ToList();
                var canBoPhuCap = listData.AsParallel();

                var newList = new BlockingCollection<TlCanBoPhuCapBridgeNq104>();
                Parallel.ForEach(canBoPhuCap, data =>
                {
                    var phuCap = JsonConvert.DeserializeObject<AllowenceCanBoNq104Criteria>(CompressExtension.DecompressFromBase64(data.Data)).X.AsParallel();
                    Parallel.ForEach(phuCap, datum =>
                    {
                        newList.Add(new TlCanBoPhuCapBridgeNq104()
                        {
                            MaCanBo = data.MaCbo,
                            MaPhuCap = datum.A,
                            GiaTri = datum.B,
                            NgayHuongPhuCap = datum.C
                        });
                    });
                });

                _tlCanBoPhuCapBridgeRepository.DeleteAll();
                _tlCanBoPhuCapBridgeRepository.BulkInsert(newList);
            } catch(Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<TlCanBoPhuCapBridgeNq104> FindByMaCanBo(string maCanBo)
        {
            maCanBo ??= string.Empty;
            var predicate = PredicateBuilder.True<TlCanBoPhuCapBridgeNq104>();
            predicate = predicate.And(x => maCanBo.Equals(x.MaCanBo));
            return _tlCanBoPhuCapBridgeRepository.FindAll(predicate);
        }

        public int UpdateRang(List<TlCanBoPhuCapBridgeNq104> entities)
        {
            return _tlCanBoPhuCapBridgeRepository.UpdateRange(entities);
        }
        public IEnumerable<TlCanBoPhuCapBridgeNq104> FindAll(Expression<Func<TlCanBoPhuCapBridgeNq104, bool>> predicate)
        {
            return _tlCanBoPhuCapBridgeRepository.FindAll(predicate);
        }

    }
}
