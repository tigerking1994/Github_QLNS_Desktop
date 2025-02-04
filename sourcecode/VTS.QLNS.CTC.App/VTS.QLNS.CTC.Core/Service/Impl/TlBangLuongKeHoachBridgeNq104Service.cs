using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlBangLuongKeHoachBridgeNq104Service : ITlBangLuongKeHoachBridgeNq104Service
    {
        private readonly ITlBangLuongKeHoachBridgeNq104Repository _tlBangLuongKeHoachBridgeRepository;
        private readonly ITlBangLuongKeHoachNq104Repository _tlBangLuongKeHoachRepository;

        public TlBangLuongKeHoachBridgeNq104Service(
            ITlBangLuongKeHoachBridgeNq104Repository tlBangLuongKeHoachBridgeRepository,
            ITlBangLuongKeHoachNq104Repository tlBangLuongKeHoachRepository)
        {
            _tlBangLuongKeHoachBridgeRepository = tlBangLuongKeHoachBridgeRepository;
            _tlBangLuongKeHoachRepository = tlBangLuongKeHoachRepository;
        }

        public void BulkInsert(List<TlBangLuongKeHoachBridgeNq104> lstData)
        {
            _tlBangLuongKeHoachBridgeRepository.BulkInsert(lstData);
        }

        public void DeleteAll()
        {
            _tlBangLuongKeHoachBridgeRepository.DeleteAll();
        }

        public void DataPreprocess(int? nam = null, string donVi = "", string maCachTl = "")
        {
            try
            {
                var listAll = new List<TlBangLuongKeHoachNq104>();
                var predicate = PredicateBuilder.True<TlBangLuongKeHoachNq104>();
               
                if (nam != null)
                {
                    predicate = predicate.And(x => x.Nam == nam);
                }
                if (!string.IsNullOrEmpty(donVi))
                {
                    predicate = predicate.And(x => donVi.Split(',').Contains(x.MaDonVi));
                }
                if (!string.IsNullOrEmpty(maCachTl))
                {
                    predicate = predicate.And(x => maCachTl.Split(',').Contains(x.MaCachTl));
                }

                listAll = _tlBangLuongKeHoachRepository.FindAll(predicate).ToList();
                var newData = new BlockingCollection<TlBangLuongKeHoachBridgeNq104>();
                var listParellel = listAll.AsParallel().ToList();

                Parallel.ForEach(listParellel, data =>
                {
                    JObject rss = JObject.Parse(CompressExtension.DecompressFromBase64(data.Data));
                    var res = ((JToken)rss).AsParallel().OfType<JProperty>();
                    Parallel.ForEach(res, x =>
                    {
                        newData.Add(new TlBangLuongKeHoachBridgeNq104()
                        {
                            Id = Guid.NewGuid(),
                            Parent = data.Parent,
                            MaDonVi = data.MaDonVi,
                            MaHieuCanBo = data.MaHieuCanBo,
                            MaCanBo = data.MaCanBo,
                            MaPhuCap = x.Name,
                            GiaTri = (decimal?)x.Value,
                            MaCB = data.MaCb,
                            Thang = data.Thang,
                            Nam = data.Nam,
                        });
                    });
                });

                _tlBangLuongKeHoachBridgeRepository.DeleteAll();
                _tlBangLuongKeHoachBridgeRepository.BulkInsert(newData);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<TlBangLuongKeHoachBridgeNq104> FindAll()
        {
            return _tlBangLuongKeHoachBridgeRepository.FindAll();
        }
    }
}
