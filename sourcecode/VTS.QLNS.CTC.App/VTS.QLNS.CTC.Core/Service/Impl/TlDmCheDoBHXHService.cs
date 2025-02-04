using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Repository.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using static Dapper.SqlMapper;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlDmCheDoBHXHService : IService<TlDmCheDoBHXH>, ITlDmCheDoBHXHService
    {
        private ITlDmCheDoBHXHRepository _repository;
        public TlDmCheDoBHXHService(ITlDmCheDoBHXHRepository iTlDmCheDoBHXHRepository)
        {
            _repository = iTlDmCheDoBHXHRepository;
        }

        public override IEnumerable<TlDmCheDoBHXH> FindAll(AuthenticationInfo authenticationInfo)
        {
            return _repository.FindAll().OrderBy(x => x.SXauNoiMa).ToList();
        }

        public override void AddOrUpdateRange(IEnumerable<TlDmCheDoBHXH> listEntities, AuthenticationInfo authenticationInfo)
        {
            foreach (var item in listEntities)
            {
                if (!string.IsNullOrEmpty(item.SMaCheDoCha))
                {
                    var parentItem = _repository.GetCheDoBHXHByMaCheDo(item.SMaCheDoCha);
                    item.SXauNoiMa = parentItem.SXauNoiMa + "-" + item.SMaCheDo;
                }
                else
                {
                    item.SXauNoiMa = item.SMaCheDo;
                }
            }
            _repository.AddOrUpdateRange(listEntities);
        }

        public override bool CheckPhuCapExist(string maCheDo, Guid? iId)
        {
            return _repository.CheckPhuCapExist(maCheDo, iId ?? Guid.Empty);
        }

        public override IEnumerable<TlDmCheDoBHXH> FindDataToExportTemplate(AuthenticationInfo authenticationInfo)
        {
            return _repository.FindAll().OrderBy(p => p.ILoaiCheDo).ThenBy(p => p.SXauNoiMa).ToList();
        }

        public override void ImportDataExcel(IEnumerable<TlDmCheDoBHXH> listEntities, AuthenticationInfo authenticationInfo, int importMode)
        {
            var time = DateTime.Now;
            IEnumerable<TlDmCheDoBHXH> DbCheDo = _repository.FindAll().ToList();
            IEnumerable<Guid> modifiedIds = listEntities.Select(i => i.Id);
            List<TlDmCheDoBHXH> entities = listEntities.ToList();
            for (int i = 0; i < entities.Count(); i++)
            {
                TlDmCheDoBHXH item = entities[i];
                TlDmCheDoBHXH track = DbCheDo.FirstOrDefault(i => i.SMaCheDo == item.SMaCheDo);
                // bản ghi mới
                if (track == null)
                {
                    //item.Id = Guid.NewGuid();
                }
                else
                {
                    track.SMaCheDo = item.SMaCheDo;
                    track.STenCheDo = item.STenCheDo;
                    track.SMaCheDoCha = item.SMaCheDoCha;
                    track.SMoTa = item.SMoTa;
                    item = ObjectCopier.Clone(track);
                }
                if (!string.IsNullOrEmpty(item.SMaCheDoCha))
                {
                    item.SXauNoiMa = item.SMaCheDoCha + "-" + item.SXauNoiMa;
                }
                else
                {
                    item.SXauNoiMa = item.SMaCheDoCha;
                }
                item.IsModified = true;
                entities[i] = item;
            }
            
            _repository.AddOrUpdateRange(entities);
        }

        public override bool ValidateDataExcel(IEnumerable<TlDmCheDoBHXH> listEntities, AuthenticationInfo authenticationInfo, int importMode)
        {
            IEnumerable<Guid> excludeIds = listEntities.Select(i => i.Id).ToList();
            IEnumerable<TlDmCheDoBHXH> DbMlns = _repository.FindAll().ToList();
            foreach (var item in listEntities)
            {
                if (item.Id == null || item.Id ==Guid.Empty)
                {
                    var checkExist = _repository.CheckPhuCapExist(item.SMaCheDo, item.Id);
                    if (checkExist && !item.IsDeleted)
                    {
                        throw new ArgumentException("Mã chế độ " + item.SMaCheDo + " đã tồn tại.");
                    }
                    if (string.IsNullOrEmpty(item.SMaCheDo))
                    {
                        throw new ArgumentException("Vui lòng nhập Mã chế độ.");
                    }
                }
            }
            return true;
        }

        public IEnumerable<TlDmCheDoBHXH> FindAll()
        {
            return _repository.FindAll().OrderBy(x => x.SXauNoiMa).ToList();
        }

        public TlDmCheDoBHXH GetCheDoBHXHByMaCheDo(string maCheDo)
        {
            return _repository.GetCheDoBHXHByMaCheDo(maCheDo);
        }

        public IEnumerable<TlDmCheDoBHXH> GetAllCheDoBHXH()
        {
            return _repository.GetAllCheDoBHXH();
        }

        public int UpdateCheDoBHXH(TlDmCheDoBHXH entity)
        {
            return _repository.Update(entity);
        }

        public IEnumerable<TlDmCheDoBHXHQuery> GetCheDoBHXHMapping()
        {
            return _repository.GetCheDoBHXHMapping();
        }

        public IEnumerable<TlDmCheDoBHXH> GetCheDoByParent(string maCheDoCha)
        {
            return _repository.GetCheDoByParent(maCheDoCha);
        }

        public TlDmCachTinhLuongBaoHiem GetCachTinhLuong(string congThuc)
        {
            return _repository.GetCachTinhLuong(congThuc);
        }

        public TlDmCachTinhLuongBaoHiemNq104 GetCachTinhLuongNq104(string congThuc)
        {
            return _repository.GetCachTinhLuongNq104(congThuc);
        }
    }
}
