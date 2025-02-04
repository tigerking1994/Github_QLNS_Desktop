using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly INsNguoiDungDonViRepository _nsNguoiDungDonViRepository;
        private readonly ITlNguoiDungPhanHoRepository _tlNguoiDungPhanHoRepository;
        private readonly INsNguoiDungLnsRepository _nsNguoiDungLnsRepository;

        public UserService(IUserRepository userRepository, 
            INsNguoiDungDonViRepository nsNguoiDungDonViRepository,
            ITlNguoiDungPhanHoRepository tlNguoiDungPhanHoRepository,
            INsNguoiDungLnsRepository nsNguoiDungLnsRepository)
        {
            _userRepository = userRepository;
            _nsNguoiDungDonViRepository = nsNguoiDungDonViRepository;
            _tlNguoiDungPhanHoRepository = tlNguoiDungPhanHoRepository;
            _nsNguoiDungLnsRepository = nsNguoiDungLnsRepository;
        }

        public HtNguoiDung Add(HtNguoiDung entity, int namLamViec)
        {
            _userRepository.Add(entity);
            IEnumerable<NguoiDungDonVi> addedList = entity.NsNguoiDungDonVis.Select(t => new NguoiDungDonVi
            {
                Id = 0,
                IIDMaNguoiDung = entity.STaiKhoan,
                IIdMaDonVi = t.IIdMaDonVi,
                INamLamViec = namLamViec,
                IStt = 1,
                ITrangThai = 1,
                BPublic = false,
                DNgayTao = DateTime.Now,
                ISoLanSua = 1,
                STenDonVi = t.STenDonVi
            });
            IEnumerable<NguoiDungPhanHo> addedListPhanHo = entity.TlNguoiDungPhanHos.Select(t => new NguoiDungPhanHo
            {
                Id = 0,
                IIDMaNguoiDung = entity.STaiKhoan,
                IIdMaDonVi = t.IIdMaDonVi,
                INamLamViec = namLamViec,
                IStt = 1,
                ITrangThai = 1,
                BPublic = false,
                DNgayTao = DateTime.Now,
                ISoLanSua = 1,
                STenDonVi = t.STenDonVi
            });

            _nsNguoiDungDonViRepository.AddRange(addedList);
            _tlNguoiDungPhanHoRepository.AddRange(addedListPhanHo);
            _nsNguoiDungLnsRepository.AddRange(entity.NguoiDungLns);
            return entity;
        }

        public HtNguoiDung FindByLogin(string login)
        {
            return _userRepository.SingleOrDefault(u => u.STaiKhoan.ToLower().Equals(login));
        }

        public string CalculateHash(string clearTextPassword, string salt)
        {
            // Convert the salted password to a byte array
            byte[] saltedHashBytes = Encoding.UTF8.GetBytes(clearTextPassword + salt);
            // Use the hash algorithm to calculate the hash
            HashAlgorithm algorithm = new SHA256Managed();
            byte[] hash = algorithm.ComputeHash(saltedHashBytes);
            // Return the hash as a base64 encoded string to be compared to the stored password
            return Convert.ToBase64String(hash);
        }

        public void Update(HtNguoiDung entity, int namLamViec)
        {
            _userRepository.UpdateNguoiDung(entity);
            
            IEnumerable<NguoiDungDonVi> addedList = entity.NsNguoiDungDonVis.Select(t => new NguoiDungDonVi
            {
                Id = 0,
                IIDMaNguoiDung = entity.STaiKhoan,
                IIdMaDonVi = t.IIdMaDonVi,
                INamLamViec = namLamViec,
                IStt = 1,
                ITrangThai = 1,
                BPublic = false,
                DNgayTao = DateTime.Now,
                ISoLanSua = t.ISoLanSua != null ? t.ISoLanSua + 1 : 1,
                STenDonVi = t.STenDonVi
            });
            IEnumerable<NguoiDungPhanHo> addedListPhanHo = entity.TlNguoiDungPhanHos.Select(t => new NguoiDungPhanHo
            {
                Id = 0,
                IIDMaNguoiDung = entity.STaiKhoan,
                IIdMaDonVi = t.IIdMaDonVi,
                INamLamViec = namLamViec,
                IStt = 1,
                ITrangThai = 1,
                BPublic = false,
                DNgayTao = DateTime.Now,
                ISoLanSua = t.ISoLanSua != null ? t.ISoLanSua + 1 : 1,
                STenDonVi = t.STenDonVi
            });

            // update nguoi dung don vi
            List<NguoiDungDonVi> deletedListNguoiDungDonvi = _nsNguoiDungDonViRepository.FindAll(t => t.IIDMaNguoiDung.Equals(entity.STaiKhoan) && t.INamLamViec == namLamViec).ToList();
            List<NguoiDungPhanHo> deletedListNguoiDungPhanHo = _tlNguoiDungPhanHoRepository.FindAll(t => t.IIDMaNguoiDung.Equals(entity.STaiKhoan) && t.INamLamViec == namLamViec).ToList();
            _nsNguoiDungDonViRepository.RemoveListNguoiDungDonvi(deletedListNguoiDungDonvi);
            _tlNguoiDungPhanHoRepository.RemoveListNguoiDungPhanHo(deletedListNguoiDungPhanHo);

            _nsNguoiDungDonViRepository.AddRange(addedList);
            _tlNguoiDungPhanHoRepository.AddRange(addedListPhanHo);

            // update nguoi dung lns
            List<NsNguoiDungLns> deletedListNguoiDungLns = _nsNguoiDungLnsRepository.FindAll(t => t.SMaNguoiDung.Equals(entity.STaiKhoan) && t.INamLamViec == namLamViec).ToList();
            _nsNguoiDungLnsRepository.RemoveListNguoiDungLns(deletedListNguoiDungLns);
            _nsNguoiDungLnsRepository.AddRange(entity.NguoiDungLns);
        }

        public void RemoveRange(IEnumerable<HtNguoiDung> entities)
        {
            _userRepository.RemoveRange(entities);
        }

        public IEnumerable<HtNguoiDung> FindAll(Expression<Func<HtNguoiDung, bool>> predicate, int namLamViec)
        {
            return _userRepository.FindAllUsers(predicate, namLamViec);
        }

        public HtNguoiDung FindByPredicate(Expression<Func<HtNguoiDung, bool>> predicate)
        {
            return _userRepository.FindByPredicate(predicate);
        }

        public HtNguoiDung FindById(Guid id)
        {
            Expression<Func<HtNguoiDung, bool>> predicate = c => c.Id.Equals(id);
            return _userRepository.FindByPredicate(predicate);
        }

        public IEnumerable<HtNguoiDung> FindAll(int namLamViec)
        {
            return _userRepository.FindAllUsers(u => true, namLamViec);
        }
        public IEnumerable<HtNguoiDung> FindAll()
        {
            return _userRepository.FindAll();
        }


        public void UpdateRange(IEnumerable<HtNguoiDung> entities)
        {
            _userRepository.UpdateRange(entities);
        }

        public void Delete(Guid id)
        {
            _userRepository.DeleteUser(id);
        }

        public void LockOrUnLock(Guid id, bool isActivated)
        {
            HtNguoiDung sysUser = FindById(id);
            sysUser.BKichHoat = isActivated;
            _userRepository.Update(sysUser);
        }

        public void Update(HtNguoiDung entity)
        {
            _userRepository.Update(entity);
        }

        public HtNguoiDung FindById(Guid id, int namLamViec)
        {
            Expression<Func<HtNguoiDung, bool>> predicate = c => c.Id.Equals(id);
            return _userRepository.FindByPredicate(predicate, namLamViec);
        }

        public IEnumerable<HtNguoiDung> FindAllUsersWithLns(Expression<Func<HtNguoiDung, bool>> predicate, int namLamViec)
        {
            return _userRepository.FindAllUsersWithLns(predicate, namLamViec);
        }

        public void SaveUserLns(string userLogin, List<NsMucLucNganSach> lns, int namLamViec)
        {
            List<NsNguoiDungLns> deletedList = _nsNguoiDungLnsRepository.FindAll(t => t.SMaNguoiDung.Equals(userLogin) && t.INamLamViec == namLamViec).ToList();
            _nsNguoiDungLnsRepository.RemoveListNguoiDungLns(deletedList);
            List<NsNguoiDungLns> addedList = lns.Select(t => new NsNguoiDungLns
            {
                SMaNguoiDung = userLogin,
                SLns = t.Lns,
                INamLamViec = namLamViec
            }).ToList();
            _nsNguoiDungLnsRepository.AddRange(addedList);
        }

        public HtNguoiDung FindByPredicate(Expression<Func<HtNguoiDung, bool>> predicate, int namLamViec)
        {
            return _userRepository.FindByPredicate(predicate, namLamViec);
        }

        public HtNguoiDung FindUserWithGroupAndLns(Guid userId, int namLamViec)
        {
            return _userRepository.FindUserWithGroupAndLns(userId, namLamViec);
        }

        public void ResetPassword(string userName)
        {
            string pwd = CalculateHash("111111", userName);
            _userRepository.ResetPassword(userName, pwd);
        }
    }
}
