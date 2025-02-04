using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [Sheet(6, "5. DS Quyết định chi phí khác", 4, 0)]
    public class NhDaQuyetDinhKhacImport : BindableBase
    {
        public Guid IIdQuyetDinhKhacId { get; set; } = Guid.NewGuid();
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
        public string _sSoQuyetDinh;
        [Column("Số quyết định", 1, ValidateType.IsString, true)]
        public string SSoQuyetDinh
        {
            get => _sSoQuyetDinh;
            set => SetProperty(ref _sSoQuyetDinh, value);
        }
        private string _sTenQuyetDinh;
        [Column("Tên quyết định chi phí khác", 2, ValidateType.IsString, true)]
        public string STenQuyetDinh
        {
            get => _sTenQuyetDinh;
            set => SetProperty(ref _sTenQuyetDinh, value);
        }
        private string _dNgayQuyetDinh;
        [Column("Ngày phê duyệt", 3)]
        public string NgayQuyetDinh
        {
            get => _dNgayQuyetDinh;
            set => SetProperty(ref _dNgayQuyetDinh, value);
        }
        public DateTime? DNgayQuyetDinh
        {
            get
            {
                return DateUtils.CheckDateFormatAndConverter(this.NgayQuyetDinh);
            }
        }
        private string _sThuocMenu;
        [Column("Thuộc menu", 4)]
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
