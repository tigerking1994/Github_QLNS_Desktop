using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service
{
    public abstract class IService<T> where T : EntityBase
    {
        public virtual IEnumerable<T> FindAll(AuthenticationInfo authenticationInfo)
        {
            return new List<T>();
        }

        public virtual IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate)
        {
            return new List<T>();
        }

        public virtual IEnumerable<T> FindAllNew(IEnumerable<T> listEntities, AuthenticationInfo authenticationInfo)
        {
            return new List<T>();
        }

        public virtual void AddOrUpdateRange(IEnumerable<T> listEntities, AuthenticationInfo authenticationInfo)
        {
        }

        public virtual void AddOrUpdateRange(IEnumerable<T> listEntities, AuthenticationInfo authenticationInfo, Action<int> func)
        {
        }

        public virtual void ImportDataExcel(IEnumerable<T> listEntities, AuthenticationInfo authenticationInfo, int importMode)
        {
        }

        public virtual bool ValidateDataExcel(IEnumerable<T> listEntities, AuthenticationInfo authenticationInfo, int import)
        {
            return true;
        }        

        public virtual IEnumerable<T> FindDataToExportTemplate(AuthenticationInfo authenticationInfo)
        {
            return new List<T>();
        }

        public virtual int GetNextValueOfCode(string propName, string formatVal, AuthenticationInfo authenticationInfo)
        {
            return 0;
        }

        public virtual IEnumerable<T> FindAll(AuthenticationInfo authenticationInfo, bool isPopup, List<string> notIns)
        {
            return new List<T>();
        }

        public virtual IEnumerable<T> FindAllMlnsByLnsAndNamLmaViec(List<string> lns, AuthenticationInfo authenticationInfo)
        {
            return new List<T>();
        }

        public virtual bool CheckPhuCapExist(string maPhuCap, Guid? iId)
        {
            return false;
        }

        public virtual IEnumerable<T> FindAllPhuCapHeThong(AuthenticationInfo authenticationInfo)
        {
            return new List<T>();
        }

        public virtual IEnumerable<T> FindListChucVu(bool isLoaiChucVu, int Nam)
        {
            return new List<T>();
        }

        public virtual IEnumerable<T> FindAllThu(AuthenticationInfo authenticationInfo)
        {
            return new List<T>();
        }
        public virtual IEnumerable<T> FindAllTTPhuCapLuong(AuthenticationInfo authenticationInfo)
        {
            return new List<T>();
        }
        public virtual string FindByBHXHMucLucIn(int namLamViec, string mlnsLoai, string mlnsBhxh)
        {
            return string.Empty;
        }

        public virtual string FindByBHXHPhuCapIn(int namLamViec, string pcLoai, string pcBhxh)
        {
            return string.Empty;
        }

        public virtual IEnumerable<T> FindDmPhuCapNq104(int Nam)
        {
            return new List<T>();
        }
        public virtual IEnumerable<T> FindFixNguonNganSach(Guid id)
        {
            return new List<T>();
        }

        public virtual void UpdateCadres()
        {

        }

        public virtual IEnumerable<T> FindAllNhomChuKy(AuthenticationInfo authenticationInfo)
        {
            return new List<T>();
        }

        public virtual void AddOrUpdateRangeSignatureGroup(IEnumerable<T> listEntities, AuthenticationInfo authenticationInfo)
        {
        }
    }
}
