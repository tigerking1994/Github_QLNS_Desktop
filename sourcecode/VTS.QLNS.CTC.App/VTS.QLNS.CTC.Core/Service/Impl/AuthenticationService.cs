using System.Collections.Generic;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Exceptions;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly IGroupService _groupService;
        private readonly ITlNguoiDungPhanHoRepository _iTlNguoiDungPhanHoRepository;
        private readonly ISysFunctionAuthorityService _sysFunctionAuthorityService;

        public AuthenticationService(IUserService userService,
            IGroupService groupService,
            ITlNguoiDungPhanHoRepository iTlNguoiDungPhanHoRepository,
            ISysFunctionAuthorityService sysFunctionAuthorityService)
        {
            _userService = userService;
            _groupService = groupService;
            _iTlNguoiDungPhanHoRepository = iTlNguoiDungPhanHoRepository;
            _sysFunctionAuthorityService = sysFunctionAuthorityService;
        }

        public HtNguoiDung Login(string userName, string password)
        {
            var effort = int.Parse(GlobalVariables.GetItemsByTag("EffortCount"));
            // Kiểm tra trên bảng hệ thống người dùng không có tài khoản nào đang kích hoạt hoặc đăng nhập sai quá 5 lần thì sẽ đăng nhập bằng super admin
            List<HtNguoiDung> htNguoiDung = _userService.FindAll().ToList();

            if (!htNguoiDung.Any(n => n.BKichHoat))
            {
                HtNguoiDung superAdmin = new HtNguoiDung();

                if (userName.ToUpper().Equals(NSConstants.SUPER_ADMIN) && password.Equals(NSConstants.SUPER_ADMIN_PWD))
                {
                    var functionAuthoritiesSuperAdmin = _sysFunctionAuthorityService.FindAll();
                    superAdmin.Authorities = new List<string>() { NSConstants.ADMIN };
                    superAdmin.FuncAuthorities = functionAuthoritiesSuperAdmin.Where(n => n.IIDMaChucNang.StartsWith(NSConstants.SYSTEM)).GroupBy(x => x.IIDMaChucNang).ToDictionary(y => y.Key, y => y.Select(item => item.IIDMaQuyen).ToList());
                    return superAdmin;
                }
                else
                {
                    throw new NoUserException(userName);
                }

            }
            else if (effort > 5)
            {
                HtNguoiDung superAdmin = new HtNguoiDung();

                if (userName.ToUpper().Equals(NSConstants.SUPER_ADMIN) && password.Equals(NSConstants.SUPER_ADMIN_PWD))
                {
                    var functionAuthoritiesSuperAdmin = _sysFunctionAuthorityService.FindAll();
                    superAdmin.Authorities = new List<string>() { NSConstants.ADMIN };
                    superAdmin.FuncAuthorities = functionAuthoritiesSuperAdmin.Where(n => n.IIDMaChucNang.StartsWith(NSConstants.SYSTEM)).GroupBy(x => x.IIDMaChucNang).ToDictionary(y => y.Key, y => y.Select(item => item.IIDMaQuyen).ToList());
                    return superAdmin;
                }
                else
                {
                    throw new InputPasswordExceedLimitException(userName);
                }

            }
            else
            {
                HtNguoiDung sysUser = _userService.FindByPredicate(u => userName.Equals(u.STaiKhoan));

                if (sysUser == null || !sysUser.SMatKhau.Equals(_userService.CalculateHash(password, sysUser.STaiKhoan)))
                {
                    GlobalVariables.AddItemsByTag("EffortCount", (effort + 1).ToString());
                    effort++;
                    throw new InvalidPasswordException(userName, password);
                }
                if (!sysUser.BKichHoat && !sysUser.SysGroupUsers.Select(n => n.HTNhom.STenNhom).Contains(NSConstants.ADMIN_GROUP))
                {
                    throw new UserLockedException(userName);
                }

                // Authorities
                ICollection<HtNhom> sysGroups = sysUser.SysGroupUsers.Select(u => u.HTNhom).ToList().Where(g => g.BKichHoat).ToList();
                List<string> authorities = new List<string>();
                foreach (HtNhom sysGroup in sysGroups)
                {
                    HtNhom group = _groupService.FindById(sysGroup.Id);
                    ICollection<string> sysAuthorities = group.SysGroupAuthorities.Select(t => t.IIDMaQuyen).ToList();
                    authorities.AddRange(sysAuthorities.Except(authorities));
                }
                // authorities.AddRange(sysUser.SysUserAuthorities.Select(auth => auth.AuthorityName).Except(authorities));
                sysUser.Authorities = authorities;

                // Function authorities
                var functionAuthorities = _sysFunctionAuthorityService.FindAll();
                sysUser.FuncAuthorities = functionAuthorities.GroupBy(x => x.IIDMaChucNang).ToDictionary(y => y.Key, y => y.Select(item => item.IIDMaQuyen).ToList());

                sysUser.TlNguoiDungPhanHos = _iTlNguoiDungPhanHoRepository.FindAll().Where(n => n.IIDMaNguoiDung == sysUser.STaiKhoan).ToArray();

                return sysUser;
            }


        }
    }
}
