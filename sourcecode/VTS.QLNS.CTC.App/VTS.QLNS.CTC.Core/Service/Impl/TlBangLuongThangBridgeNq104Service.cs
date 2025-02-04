using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlBangLuongThangBridgeNq104Service : ITlBangLuongThangBridgeNq104Service
    {
        private readonly ITlBangLuongThangBridgeNq104Repository _tlBangLuongThangBridgeRepository;
        private readonly ITlBangLuongThangNq104Repository _tlBangLuongThangRepository;

        public TlBangLuongThangBridgeNq104Service(
            ITlBangLuongThangBridgeNq104Repository tlBangLuongThangBridgeRepository,
            ITlBangLuongThangNq104Repository tlBangLuongThangRepository)
        {
            _tlBangLuongThangBridgeRepository = tlBangLuongThangBridgeRepository;
            _tlBangLuongThangRepository = tlBangLuongThangRepository;
        }

        public void BulkInsert(List<TlBangLuongThangBridgeNq104> lstData)
        {
            _tlBangLuongThangBridgeRepository.BulkInsert(lstData);
        }

        public void DeleteAll()
        {
            _tlBangLuongThangBridgeRepository.DeleteAll();
        }

        public void DataPreprocess(int? thang = null, int? nam = null, string donVi = "", string maCachTl = "")
        {
            try
            {
                var listAll = new List<TlBangLuongThangNq104>();
                var predicate = PredicateBuilder.True<TlBangLuongThangNq104>();
                if (thang != null)
                {
                    predicate = predicate.And(x => x.Thang == thang);
                }
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
                    //predicate = predicate.And(x => x.MaCachTl == maCachTl);
                    predicate = predicate.And(x => maCachTl.Split(',').Contains(x.MaCachTl));
                }

                listAll = _tlBangLuongThangRepository.FindAll(predicate).ToList();
                var newData = new BlockingCollection<TlBangLuongThangBridgeNq104>();
                var listParellel = listAll.AsParallel().ToList();

                Parallel.ForEach(listParellel, data =>
                {
                    JObject rss = JObject.Parse(CompressExtension.DecompressFromBase64(data.Data));
                    var res = ((JToken)rss).AsParallel().OfType<JProperty>();
                    Parallel.ForEach(res, x =>
                    {
                        newData.Add(new TlBangLuongThangBridgeNq104()
                        {
                            Parent = data.Parent,
                            MaDonVi = data.MaDonVi,
                            MaHieuCanBo = data.MaHieuCanBo,
                            MaCanBo = data.MaCbo,
                            MaPhuCap = x.Name,
                            GiaTri = (decimal?)x.Value
                        });
                    });
                });

                _tlBangLuongThangBridgeRepository.DeleteAll();
                _tlBangLuongThangBridgeRepository.BulkInsert(newData);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<TlBangLuongThangBridgeNq104> FindAll()
        {
            return _tlBangLuongThangBridgeRepository.FindAll();
        }
    }
}
