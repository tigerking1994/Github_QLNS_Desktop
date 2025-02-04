using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class DmChuKyService : IService<DmChuKy>, IDmChuKyService
    {
        private readonly IDmChuKyRepository _dmChuKyRepository;
        private readonly IDanhMucRepository _danhMucRepository;

        public DmChuKyService(IDmChuKyRepository dmChuKyRepository, IDanhMucRepository danhMucRepository)
        {
            _dmChuKyRepository = dmChuKyRepository;
            _danhMucRepository = danhMucRepository;
        }

        public override void AddOrUpdateRange(IEnumerable<DmChuKy> listEntities, AuthenticationInfo authenticationInfo)
        {
            _dmChuKyRepository.AddOrUpdateRange(listEntities);
        }

        public override IEnumerable<DmChuKy> FindAll(AuthenticationInfo authenticationInfo)
        {
            return _dmChuKyRepository.FindAll();
        }

        public IEnumerable<DanhMuc> FindChuKyChucDanh()
        {
            return _danhMucRepository.FindChuKyChucDanh();
        }

        public IEnumerable<DanhMuc> FindChuKyTen()
        {
            return _danhMucRepository.FindChuKyTen();
        }

        public IEnumerable<DanhMuc> FindChuKyTieuDe1()
        {
            return _danhMucRepository.FindChuKyTieuDe1();
        }

        public IEnumerable<DanhMuc> FindChuKyTieuDe2()
        {
            return _danhMucRepository.FindChuKyTieuDe2();
        }

        public IEnumerable<DanhMuc> FindNhomChuKy(int year)
        {
            return _danhMucRepository.FindNhomChuKy(year);
        }

        public void Add(DmChuKy dmChuKy)
        {
            _dmChuKyRepository.Add(dmChuKy);
        }

        public void UpdateRange(IEnumerable<DmChuKy> dmChuKy)
        {
            _dmChuKyRepository.UpdateRange(dmChuKy);
        }

        public void Save(DmChuKy dmChuKy)
        {
            _dmChuKyRepository.SaveChuKy(dmChuKy);
        }

        public DmChuKy FindById(Guid id)
        {
            return _dmChuKyRepository.SingleOrDefault(t => t.Id.Equals(id));
        }

        public IEnumerable<DmChuKy> FindByCondition(Expression<Func<DmChuKy, bool>> predicate)
        {
            return _dmChuKyRepository.FindAll(predicate);
        }

        public void GetConfigSign(string sTypeChuKy, ref Dictionary<string, object> data)
        {
            DmChuKy _dmChuKy = new DmChuKy();
            var objChuKy = FindByCondition(n => n.IdType == sTypeChuKy && n.ITrangThai == 1);
            if (objChuKy != null && objChuKy.Count() != 0)
                _dmChuKy = objChuKy.FirstOrDefault();
            else
                _dmChuKy = new DmChuKy();
            data.Add("ThuaLenh1", _dmChuKy.ThuaLenh1MoTa);
            data.Add("ThuaLenh2", _dmChuKy.ThuaLenh2MoTa);
            data.Add("ThuaLenh3", _dmChuKy.ThuaLenh3MoTa);
            data.Add("ThuaLenh4", _dmChuKy.ThuaLenh4MoTa);
            data.Add("ThuaLenh5", _dmChuKy.ThuaLenh5MoTa);
            data.Add("ThuaLenh6", _dmChuKy.ThuaLenh6MoTa);

            data.Add("ChucDanh1", _dmChuKy.ChucDanh1MoTa);
            data.Add("ChucDanh2", _dmChuKy.ChucDanh2MoTa);
            data.Add("ChucDanh3", _dmChuKy.ChucDanh3MoTa);
            data.Add("ChucDanh4", _dmChuKy.ChucDanh4MoTa);
            data.Add("ChucDanh5", _dmChuKy.ChucDanh5MoTa);
            data.Add("ChucDanh6", _dmChuKy.ChucDanh6MoTa);

            data.Add("Ten1", _dmChuKy.Ten1MoTa);
            data.Add("Ten2", _dmChuKy.Ten2MoTa);
            data.Add("Ten3", _dmChuKy.Ten3MoTa);
            data.Add("Ten4", _dmChuKy.Ten4MoTa);
            data.Add("Ten5", _dmChuKy.Ten5MoTa);
            data.Add("Ten6", _dmChuKy.Ten6MoTa);
        }

        public IEnumerable<DanhMuc> FindNhomChuKyTen()
        {
            return _danhMucRepository.FindNhomChuKyTen();
        }

        public IEnumerable<DanhMuc> FindNhomChuKyChucDanh()
        {
            return _danhMucRepository.FindNhomChuKyChucDanh();
        }
    }
}
