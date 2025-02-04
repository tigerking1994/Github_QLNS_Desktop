using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [Sheet(4, "3. DS Dự án", 4, 0)]

    public class NhDaDuAnImport : BindableBase
    {
        public Guid IIdDuAnId { get; set; } = Guid.NewGuid();
        private bool _importStatus;
        public bool ImportStatus
        {
            get => _importStatus;
            set => SetProperty(ref _importStatus, value);
        }

        private bool _isErrorMLNS;
        public bool IsErrorMLNS
        {
            get => _isErrorMLNS;
            set => SetProperty(ref _isErrorMLNS, value);
        }

        private bool _isWarning;
        public bool IsWarning
        {
            get => _isWarning;
            set => SetProperty(ref _isWarning, value);
        }
        private string _sSoQuyetDinh;
        [Column("Số quyết định đầu tư", 1, ValidateType.IsString, true)]
        public string SSoQuyetDinh
        {
            get => _sSoQuyetDinh;
            set => SetProperty(ref _sSoQuyetDinh, value);
        }
        private string _sTenDuAn;
        [Column("Tên dự án", 2, ValidateType.IsString, true)]
        public string STenDuAn
        {
            get => _sTenDuAn;
            set => SetProperty(ref _sTenDuAn, value);
        }
        private string _ngayPheDuyet;
        [Column("Ngày phê duyệt quyết định đầu tư", 3)]
        public string NgayPheDuyet
        {
            get => _ngayPheDuyet;
            set => SetProperty(ref _ngayPheDuyet, value);
        }

        public DateTime? DNgayPheDuyet
        {
            get
            {
                return DateUtils.CheckDateFormatAndConverter(this.NgayPheDuyet);
            }
        }

        private string _sThuocMenu;
        [Column("Thuộc menu ", 4)]
        public string SThuocMenu
        {
            get => _sThuocMenu;
            set => SetProperty(ref _sThuocMenu, value);
        }

        public int IThuocMenu
        {
            get
            {
                if (this.SThuocMenu.ToLower().Trim().Equals(NhTongHopConstants.STHUOC_MENU_DUAN.ToLower().Trim()))
                {
                    return NHConstants.ITHUOC_MENU_DUAN;
                }
                else if (this.SThuocMenu.ToLower().Trim().Equals(NhTongHopConstants.STHUOC_MENU_MUASAM.ToLower().Trim()))
                {
                    return NHConstants.ITHUOC_MENU_MUASAM;
                }
                else
                {
                    return NHConstants.ZERO;
                }
            }
        }
    }
}
