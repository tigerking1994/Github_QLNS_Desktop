using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class DanhMucService : IService<DanhMuc>, IDanhMucService
    {
        private readonly IDanhMucRepository _danhMucRepository;

        public DanhMucService(IDanhMucRepository danhMucRepository)
        {
            _danhMucRepository = danhMucRepository;
        }

        public override void AddOrUpdateRange(IEnumerable<DanhMuc> listEntities, AuthenticationInfo authenticationInfo)
        {
            _danhMucRepository.AddOrUpdateRange(listEntities);
        }

        public override IEnumerable<DanhMuc> FindAll(AuthenticationInfo authenticationInfo)
        {
            return _danhMucRepository.FindAll();
        }

        public void Update(DanhMuc danhMuc)
        {
            DanhMuc entity = _danhMucRepository.FindByCode(danhMuc.IIDMaDanhMuc);
            entity.SGiaTri = danhMuc.SGiaTri;
            _danhMucRepository.Update(entity);
        }

        public virtual IEnumerable<DanhMuc> FindAll(string type, int year)
        {
            IEnumerable<DanhMuc> result = _danhMucRepository.FindAll(d => type.Equals(d.SType) && d.INamLamViec == year).ToList();
            return result;
        }

        public IEnumerable<DanhMuc> FindByNsDonvi(string idDonVi, int year)
        {
            return null;
        }

        public IEnumerable<DanhMuc> FindDanhMucTheoNganh(string idChungTu, int namLamViec, string type)
        {
            return _danhMucRepository.FindDanhMucTheoNganh(idChungTu, namLamViec, type);
        }
        
        public IEnumerable<DanhMuc> FindByType(string type, int namLamViec)
        {
            return _danhMucRepository.FindByType(type, namLamViec);
        }
        
        public IEnumerable<DanhMuc> FindByCondition(Expression<Func<DanhMuc, bool>> predicate)
        {
            return _danhMucRepository.FindAll(predicate);
        }

        public virtual bool CheckExistIdCode(string idCode, int namLamViec, string type, Guid id, IEnumerable<Guid> excludeIds)
        {
            // tìm bản ghi có cùng mã, cùng năm làm việc, cùng loại, không bao gồm các bản ghi sẽ được cập nhật
            IEnumerable<DanhMuc> danhMuc = _danhMucRepository.FindAll(t => idCode.Equals(t.IIDMaDanhMuc) &&  namLamViec == t.INamLamViec 
                                                && type.Equals(t.SType) && !id.Equals(t.Id) && !excludeIds.Contains(t.Id)).ToList();
            return danhMuc.Count() == 0 ? false : true;
        }

        public void Add(DanhMuc danhMuc)
        {
            _danhMucRepository.Add(danhMuc);
        }

        public DanhMuc FindByCode(string idCode, int? namLamViec = null)
        {
            return _danhMucRepository.FindByCode(idCode, namLamViec);
        }

        public DanhMuc FindByTypeAndCode(string type, string idCode)
        {
            return _danhMucRepository.FindAll(x => x.SType == type && x.IIDMaDanhMuc == idCode && x.ITrangThai == 1).FirstOrDefault();
        }

        public IEnumerable<DanhMuc> FindByType(string type)
        {
            return _danhMucRepository.FindByType(type);
        }

        public int countDanhMucByTypeAndNLV(string type, int namLamViec)
        {
            return _danhMucRepository.countDanhMucByTypeAndNLV(type, namLamViec);
        }

        public IEnumerable<DanhMuc> FindDmChuyenNganhByNsDonvi(IEnumerable<Guid> excludeIds, int year)
        {
            return _danhMucRepository.FindDmChuyenNganhByNsDonvi(excludeIds, year);
        }

        public string FindDonViQuanLy(int namLamViec)
        {
            return _danhMucRepository.FindDonViQuanLy(namLamViec);
        }

        public IEnumerable<DanhMuc> FindByCodes(List<string> codes)
        {
            return _danhMucRepository.FindAll(x => codes.Contains(x.IIDMaDanhMuc));
        }

        public int countDanhMucNganhChuyenNganh(int namLamViec)
        {
            return _danhMucRepository.CountDanhMucNganhChuyenNganh(namLamViec);
        }
    }
}
