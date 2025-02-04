using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class SktMucLucService : IService<NsSktMucLuc>, ISktMucLucService
    {
        private readonly ISktMucLucRepository _sktMucLucRepository;
        private readonly ISktMucLucMapRepository _sktMucLucMapRepository;

        public SktMucLucService(ISktMucLucRepository sktMucLucRepository, ISktMucLucMapRepository sktMucLucMapRepository)
        {
            _sktMucLucRepository = sktMucLucRepository;
            _sktMucLucMapRepository = sktMucLucMapRepository;
        }

        public virtual bool CheckExistSKyHieu(Expression<Func<NsSktMucLuc, bool>> predicate, IEnumerable<Guid> excludeIds)
        {
            // tìm bản ghi có cùng mã, cùng năm làm việc, cùng loại, không bao gồm các bản ghi sẽ được cập nhật
            predicate = predicate.And(t => !excludeIds.Contains(t.Id));
            IEnumerable<NsSktMucLuc> danhMuc = _sktMucLucRepository.FindAll(predicate).ToList();
            return danhMuc.Count() != 0;
        }

        public override void AddOrUpdateRange(IEnumerable<NsSktMucLuc> listEntities, AuthenticationInfo authenticationInfo)
        {
            var time = DateTime.Now;
            IEnumerable<Guid> excludeIds = listEntities.Select(i => i.Id).ToList();
            IEnumerable<NsSktMucLuc> modifiedItems = listEntities.Where(i => !i.IsDeleted).ToList();
            foreach (var item in listEntities)
            {
                item.INamLamViec = authenticationInfo.YearOfWork;
                var predicate = PredicateBuilder.True<NsSktMucLuc>();
                predicate = predicate.And(x => x.INamLamViec == item.INamLamViec);
                predicate = predicate.And(x => x.SKyHieu == item.SKyHieu);
                int countDuplicateIdCodes = modifiedItems.Where(predicate.Compile()).Count();
                if (countDuplicateIdCodes > 1)
                {
                    throw new ArgumentException("Số ký hiệu " + item.SKyHieu + " bị lặp, vui lòng thử lại");
                }
                // Nếu mã tồn tại và mã không thuộc danh sách bản ghi bị xóa thì sẽ throw exception
                if (CheckExistSKyHieu(predicate, excludeIds))
                {
                    throw new ArgumentException("Số ký hiệu " + item.SKyHieu + " đã tồn tại, vui lòng thử lại");
                }
                _sktMucLucMapRepository.DeleteBySktMucLucKyHieu(item.SKyHieu, item.INamLamViec);
                if (item.IsModified)
                {

                    if (Guid.Empty.Equals(item.Id))
                    {
                        item.DNgayTao = time;
                        item.DNguoiTao = authenticationInfo.Principal;
                        item.DNgaySua = null;
                        item.DNguoiSua = null;
                    }
                    else
                    {
                        item.DNgaySua = time;
                        item.DNguoiSua = authenticationInfo.Principal;
                    }
                    foreach (NsMlsktMlns map in item.SktMucLucMaps)
                    {
                        map.Id = Guid.NewGuid();
                        map.SSktKyHieu = item.SKyHieu;
                        map.DNgayTao = time;
                        map.SNguoiTao = authenticationInfo.Principal;
                        map.ITrangThai = 1;
                        map.INamLamViec = item.INamLamViec;
                    }
                    if (item.SktMucLucMaps.Count != 0)
                    {
                        _sktMucLucMapRepository.AddRange(item.SktMucLucMaps);
                    }
                }
            }
            _sktMucLucRepository.AddOrUpdateRange(listEntities);

        }

        public override void ImportDataExcel(IEnumerable<NsSktMucLuc> listEntities, AuthenticationInfo authenticationInfo, int importMode)
        {
            IEnumerable<NsSktMucLuc> DbSktMucluc = _sktMucLucRepository.FindAll().ToList();
            var time = DateTime.Now;
            IEnumerable<Guid> modifiedIds = listEntities.Select(i => i.Id);
            List<NsSktMucLuc> entities = listEntities.ToList();
            for (int i = 0; i < entities.Count(); i++)
            {
                NsSktMucLuc item = entities[i];
                NsSktMucLuc track = _sktMucLucRepository.FirstOrDefault(i => i.Id == item.Id);
                if (track == null)
                {
                    var id = Guid.NewGuid();
                    item.IIDMLSKT = id;
                }
                else
                {
                    track.SM = item.SM;
                    track.SNGCha = item.SNGCha;
                    track.SNg = item.SNg;
                    track.SSTT = item.SSTT;
                    track.SSttBC = item.SSttBC;
                    track.SKyHieu = item.SKyHieu;
                    track.INamLamViec = item.INamLamViec;
                    item = ObjectCopier.Clone(track);
                }
                item.IsModified = true;
                entities[i] = item;
            }
            foreach (NsSktMucLuc item in entities)
            {
                NsSktMucLuc parent = FindParent(item, entities, modifiedIds, DbSktMucluc);
                if (parent != null)
                {
                    item.IIDMLSKTCha = parent.IIDMLSKT;
                }
                item.BHangCha = IsHangCha(item, entities);
            }
            _sktMucLucRepository.AddOrUpdateRange(listEntities);
        }

        private bool IsHangCha(NsSktMucLuc sktMucLuc, List<NsSktMucLuc> entities)
        {
            return entities.Any(i => i.IIDMLSKTCha == sktMucLuc.IIDMLSKT);
        }

        private NsSktMucLuc FindParent(NsSktMucLuc child, IEnumerable<NsSktMucLuc> listEntities, IEnumerable<Guid> excludeIds, IEnumerable<NsSktMucLuc> DbSktMucluc)
        {
            NsSktMucLuc parent = listEntities.FirstOrDefault(i => child.INamLamViec == i.INamLamViec
                && child.IIDMLSKTCha == i.IIDMLSKT);
            if (parent != null)
            {
                return parent;
            }
            parent = DbSktMucluc.FirstOrDefault(i => child.INamLamViec == i.INamLamViec && !excludeIds.Contains(i.Id)
                && child.IIDMLSKTCha == i.IIDMLSKT);
            return parent;
        }

        public override bool ValidateDataExcel(IEnumerable<NsSktMucLuc> listEntities, AuthenticationInfo authenticationInfo, int importMode)
        {
            IEnumerable<NsSktMucLuc> DbSktMucluc = _sktMucLucRepository.FindAll().ToList();
            IEnumerable<Guid> excludeIds = listEntities.Select(i => i.Id).ToList();
            foreach (var item in listEntities)
            {
                var predicate = PredicateBuilder.True<NsSktMucLuc>();
                predicate = predicate.And(x => x.INamLamViec == item.INamLamViec);
                predicate = predicate.And(x => x.SKyHieu == item.SKyHieu);
                int countDuplicateIdCodes = listEntities.Where(predicate.Compile()).Count();
                if (countDuplicateIdCodes > 1)
                {
                    throw new ArgumentException("Mã " + item.SKyHieu + " bị lặp!");
                }
                if (CheckExistKyHieu(predicate, excludeIds, DbSktMucluc))
                {
                    throw new ArgumentException("Mã " + item.SKyHieu + " đã tồn tại!");
                }
            }
            return true;
        }

        private bool CheckExistKyHieu(Expression<Func<NsSktMucLuc, bool>> predicate, IEnumerable<Guid> excludeIds, IEnumerable<NsSktMucLuc> dbData)
        {
            predicate = predicate.And(t => !excludeIds.Contains(t.Id));
            IEnumerable<NsSktMucLuc> danhMuc = dbData.Where(predicate.Compile()).ToList();
            return danhMuc.Count() != 0;
        }

        public override IEnumerable<NsSktMucLuc> FindAll(AuthenticationInfo authenticationInfo)
        {
            var SktMucLucs = _sktMucLucRepository.FindAll(authenticationInfo).ToList();
            var joinToGetParent = from skt in SktMucLucs
                                  join parent in SktMucLucs on skt.IIDMLSKTCha equals parent.IIDMLSKT into p
                                  from parent in p.DefaultIfEmpty()
                                  select new { skt, parent };
            var result = joinToGetParent.Select(t =>
            {
                var skt = t.skt;
                skt.KyHieuCha = t.parent == null ? string.Empty : t.parent.SKyHieu;
                return skt;
            }).OrderBy(t => t.SKyHieu).ToList();

            return result;
        }

        public void RevertAllMLSKT(int namLamViec)
        {
            _sktMucLucRepository.RevertAllMLSKT(namLamViec);
        }

        public IEnumerable<NsSktMucLuc> FindAllOld(AuthenticationInfo authenticationInfo)
        {
            var SktMucLucs = _sktMucLucRepository.FindAllOld(authenticationInfo).ToList();
            var joinToGetParent = from skt in SktMucLucs
                                  join parent in SktMucLucs on skt.IIDMLSKTCha equals parent.IIDMLSKT into p
                                  from parent in p.DefaultIfEmpty()
                                  select new { skt, parent };
            var result = joinToGetParent.Select(t =>
            {
                var skt = t.skt;
                skt.KyHieuCha = t.parent == null ? string.Empty : t.parent.SKyHieu;
                return skt;
            }).OrderBy(t => t.SKyHieu).ToList();

            return result;
        }

        public IEnumerable<NsSktMucLuc> FindAllNew(AuthenticationInfo authenticationInfo)
        {
            var SktMucLucs = _sktMucLucRepository.FindAllNew(authenticationInfo).ToList();
            var joinToGetParent = from skt in SktMucLucs
                                  join parent in SktMucLucs on skt.IIDMLSKTCha equals parent.IIDMLSKT into p
                                  from parent in p.DefaultIfEmpty()
                                  select new { skt, parent };
            var result = joinToGetParent.Select(t =>
            {
                var skt = t.skt;
                skt.KyHieuCha = t.parent == null ? string.Empty : t.parent.SKyHieu;
                return skt;
            }).OrderBy(t => t.SKyHieu).ToList();

            return result;
        }

        public IEnumerable<NsSktMucLuc> FindByCondition(Expression<Func<NsSktMucLuc, bool>> predicate)
        {
            return _sktMucLucRepository.FindAll(predicate);
        }

        public IEnumerable<SktMucLucQuery> FindByCondition(int namLamViec, int loai, string idDonVi, int loaiChungTu)
        {
            return _sktMucLucRepository.FindByCondition(namLamViec, loai, idDonVi, loaiChungTu);
        }

        public override IEnumerable<NsSktMucLuc> FindDataToExportTemplate(AuthenticationInfo authenticationInfo)
        {
            return _sktMucLucRepository.FindAll(i => i.INamLamViec == authenticationInfo.YearOfWork).OrderBy(t => t.SKyHieu).ToList();
        }

        public IEnumerable<NsSktMucLuc> FindByKyHieu(int namLamViec, List<string> kyHieu)
        {
            return _sktMucLucRepository.FindByKyHieu(namLamViec, kyHieu);
        }

        public IEnumerable<NsSktMucLuc> FindByNganh(int namLamViec, List<string> nganh)
        {
            return _sktMucLucRepository.FindByNganh(namLamViec, nganh);
        }

        public int CountSktMucLuc(int namLamViec)
        {
            return _sktMucLucRepository.CountSktMucLuc(namLamViec);
        }

        public int AddRange(IEnumerable<NsSktMucLuc> sktMucLucs)
        {
            return _sktMucLucRepository.AddRange(sktMucLucs);
        }

        public bool IsUsedMLSKT(Guid iidMlskt, int namLamViec)
        {
            return _sktMucLucRepository.IsUsedMLSKT(iidMlskt, namLamViec);
        }

        public IEnumerable<SktMucLucDtQuery> FindByCondition(int namLamViec, int namNganSach, int nguonNganSach, int loai, string idDonVi, int loaiChungTu, string chungTuId)
        {
            return _sktMucLucRepository.FindByCondition(namLamViec, namNganSach, nguonNganSach, loai, idDonVi, loaiChungTu, chungTuId);
        }
        public IEnumerable<SktMucLucDtQuery> FindByConditionBVTC(int namLamViec, int namNganSach, int nguonNganSach, string loai, string idDonVi, int loaiChungTu, int? iLoaiNNS, string chungTuId)
        {
            return _sktMucLucRepository.FindByConditionBVTC(namLamViec, namNganSach, nguonNganSach, loai, idDonVi, loaiChungTu, iLoaiNNS, chungTuId);
        }

        public IEnumerable<NsMlsktMlns> FindAllMapMlsktMlns(int namLamViec)
        {
            return _sktMucLucRepository.FindAllMapMlsktMlns(namLamViec);
        }
        public IEnumerable<NsMlsktMlns> FindByConditionMlsktMlns(Expression<Func<NsMlsktMlns, bool>> predicate)
        {
            return _sktMucLucMapRepository.FindAll(predicate);
        }

        public int CountNsMlsktMlns(int namLamViec)
        {
            return _sktMucLucRepository.CountNsMlsktMlns(namLamViec);
        }

        public int UpdateSKTML(NsSktMucLuc item)
        {
            return _sktMucLucRepository.Update(item);
        }

        public void UpdateNSMlsktMlnsMapping()
        {
            _sktMucLucRepository.UpdateNSMlsktMlnsMapping();
        }

        public void UpdateSKTChungTuChiTiet(Guid voucherID)
        {
            _sktMucLucRepository.UpdateSKTChungTuChiTiet(voucherID);
        }
    }
}