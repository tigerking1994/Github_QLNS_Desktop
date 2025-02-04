using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security;
using VTS.QLNS.CTC.App.Properties;

namespace VTS.QLNS.CTC.App.Model
{
    public class HTNguoiDungModel : BindableBase, IDataErrorInfo
    {
        public HTNguoiDungModel()
        {
            SysGroupModels = new HashSet<HTNhomModel>();
            NsNguoiDungLnsModels = new HashSet<NsNguoiDungLnsModel>();
            BKichHoat = true;
        }

        public string STaiKhoan { get; set; }
        public string SMatKhau { get; set; }
        public string SHo { get; set; }
        public string FullName => SHo + " " + STen;
        public string STen { get; set; }
        public bool BKichHoat { get; set; }

        private string _sDuongDanAnh;
        public string SDuongDanAnh 
        {
            get => _sDuongDanAnh;
            set => SetProperty(ref _sDuongDanAnh, value);
        }

        public string SEmail { get; set; }
        public string LangKey { get; set; }
        public string ActivationKey { get; set; }
        public string ResetKey { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgayTao { get; set; }
        public DateTime? DNgayCaiLai { get; set; }
        public string SNguoiSua { get; set; }
        public DateTime? DNgaySua { get; set; }
        public Guid Id { get; set; }
        public virtual ICollection<HTNhomModel> SysGroupModels { get; set; }
        public virtual ICollection<NsNguoiDungDonViModel> NsNguoiDungDonViModels { get; set; }
        public virtual ICollection<NsNguoiDungLnsModel> NsNguoiDungLnsModels { get; set; }
        public string IdDonVi { get; set; }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        public string DanhSachDonVi => NsNguoiDungDonViModels.Count == 0 ? string.Empty : string.Join(";", NsNguoiDungDonViModels.Select(n => n.STenDonVi).Where(name => !string.IsNullOrEmpty(name)));
        public string DanhSachLns => NsNguoiDungLnsModels.Count == 0 ? string.Empty : string.Join(";", NsNguoiDungLnsModels.Select(n => n.SLns).Where(lns => !string.IsNullOrEmpty(lns)));

        public string Groups
        {
            get
            {
                if (SysGroupModels.Count != 0)
                {
                    ICollection<String> result = SysGroupModels.Select(p => p.STenNhom).ToList();
                    return string.Join(";", result);
                }
                return String.Empty;
            }
        }

        private SecureString _password;
        public SecureString Password
        {
            get => _password;
            set
            {
                _password = value.Copy();
                _password.MakeReadOnly();
                OnPropertyChanged("Password");
            }
        }

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                string result = null;

                switch(columnName)
                {
                    case "STaiKhoan":
                        if (String.IsNullOrEmpty(STaiKhoan))
                        {
                            return Resources.ErrorUserNameEmpty;
                        }
                        if (STaiKhoan.Length < 5)
                        {
                            return Resources.ErrorUserNameLength;
                        }
                        return null;
                    case "Password":
                        if (!Id.Equals(Guid.Empty))
                        {
                            return null;
                        }
                        if (Password == null)
                        {
                            return Resources.ErrorPasswordEmpty;
                        }
                        else if (String.IsNullOrEmpty(Password.ToString()))
                        {
                            return Resources.ErrorPasswordEmpty;
                        }
                        else if (Password.Length < 6)
                        {
                            return Resources.ErrorPasswordLength;
                        }
                        return null;
                    case "SEmail":
                        var emailAttr = new EmailAddressAttribute();
                        if (!string.IsNullOrEmpty(SEmail) && !emailAttr.IsValid(SEmail))
                        {
                            return "Email không đúng định dạng";
                        }
                        return null;
                }
                return result;
            }
        }
    }
}
