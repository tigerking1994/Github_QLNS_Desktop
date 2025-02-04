using System;

namespace VTS.QLNS.CTC.Core.Domain
{
    public class NhDmTaiKhoan : EntityBase
    {
        public override Guid Id { get; set; }
        public string SNhomTaiKhoan { get; set; }
        public string SMaTaiKhoan { get; set; }
        public string STenTaiKhoan { get; set; }
    }
}
