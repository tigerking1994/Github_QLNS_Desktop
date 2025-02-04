using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlCanBoPhuCapKeHoachBridgeNq104Service : ITlCanBoPhuCapKeHoachBridgeNq104Service
    {
        private readonly ITlCanBoPhuCapKeHoachBridgeNq104Repository _tlCanBoPhuCapBridgeRepository;
        private readonly ITlCanBoPhuCapKeHoachNq104Repository _tlCanBoPhuCapRepository;

        public TlCanBoPhuCapKeHoachBridgeNq104Service(
            ITlCanBoPhuCapKeHoachBridgeNq104Repository tlCanBoPhuCapBridgeRepository,
            ITlCanBoPhuCapKeHoachNq104Repository tlCanBoPhuCapRepository)
        {
            _tlCanBoPhuCapRepository = tlCanBoPhuCapRepository;
            _tlCanBoPhuCapBridgeRepository = tlCanBoPhuCapBridgeRepository;
        }

        public void BulkInsert(IEnumerable<TlCanBoPhuCapKeHoachBridgeNq104> lstData)
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
                List<TlCanBoPhuCapKeHoachNq104> listData = default;
                var predicate = PredicateBuilder.True<TlCanBoPhuCapKeHoachNq104>();
                if (thang != null && nam != null)
                {
                    predicate = predicate.And(x => x.MaCanBo.StartsWith($"{nam:D2}{thang:D2}"));
                }

                listData = _tlCanBoPhuCapRepository.FindAll(predicate).Where(z => !string.IsNullOrEmpty(z.Data)).ToList();
                var canBoPhuCap = listData.AsParallel();

                var newList = new BlockingCollection<TlCanBoPhuCapKeHoachBridgeNq104>();
                Parallel.ForEach(canBoPhuCap, data =>
                {
                    var phuCap = JsonConvert.DeserializeObject<AllowenceCanBoNq104Criteria>(CompressExtension.DecompressFromBase64(data.Data)).X.AsParallel();
                    Parallel.ForEach(phuCap, datum =>
                    {
                        newList.Add(new TlCanBoPhuCapKeHoachBridgeNq104()
                        {
                            MaCanBo = data.MaCanBo,
                            MaPhuCap = datum.A,
                            GiaTri = datum.B,
                            NgayHuongPhuCap = datum.C
                        });
                    });
                });

                _tlCanBoPhuCapBridgeRepository.DeleteAll();
                _tlCanBoPhuCapBridgeRepository.BulkInsert(newList);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<TlCanBoPhuCapKeHoachBridgeNq104> FindByMaCanBo(string maCanBo)
        {
            maCanBo ??= string.Empty;
            var predicate = PredicateBuilder.True<TlCanBoPhuCapKeHoachBridgeNq104>();
            predicate = predicate.And(x => maCanBo.Equals(x.MaCanBo));
            return _tlCanBoPhuCapBridgeRepository.FindAll(predicate);
        }

        public int UpdateRang(List<TlCanBoPhuCapKeHoachBridgeNq104> entities)
        {
            return _tlCanBoPhuCapBridgeRepository.UpdateRange(entities);
        }
        public IEnumerable<TlCanBoPhuCapKeHoachBridgeNq104> FindAll(Expression<Func<TlCanBoPhuCapKeHoachBridgeNq104, bool>> predicate)
        {
            return _tlCanBoPhuCapBridgeRepository.FindAll(predicate);
        }
    }
}
