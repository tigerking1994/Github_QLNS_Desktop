using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.App.Model.ConvertGenericData;
using System.Windows;
using System.Linq;


namespace VTS.QLNS.CTC.App.Model
{
    public class NhDmNhiemVuChiModel : ModelBase
    {
        private string _sMaNhiemVuChi;
        [DisplayName("Mã chương trình")]
        [DisplayDetailInfo("Mã chương trình")]
        [ColumnIndex(0)]
        [Validate("Mã chương trình", Utility.Enum.DATA_TYPE.String, 100, true)]
        public string SMaNhiemVuChi
        {
            get => _sMaNhiemVuChi;
            set => SetProperty(ref _sMaNhiemVuChi, value);
        }

        private string _sTenNhiemVuChi;
        [DisplayName("Tên chương trình")]
        [DisplayDetailInfo("Tên chương trình")]
        [Validate("Tên chương trình", Utility.Enum.DATA_TYPE.String, 255, true)]
        public string STenNhiemVuChi
        {
            get => _sTenNhiemVuChi;
            set => SetProperty(ref _sTenNhiemVuChi, value);
        }

        private string _sMoTaChiTiet;
        [DisplayName("Mô tả chi tiết")]
        [DisplayDetailInfo("Mô tả chi tiết")]
        public string SMoTaChiTiet
        {
            get => _sMoTaChiTiet;
            set => SetProperty(ref _sMoTaChiTiet, value);
        }

        private int? _iLoaiNhiemVuChi;
        /*[DisplayName("Loại nhiệm vụ chi")]
        [ColumnType(ColumnType.Combobox)]*/
        public int? ILoaiNhiemVuChi
        {
            get => _iLoaiNhiemVuChi;
            set => SetProperty(ref _iLoaiNhiemVuChi, value);
        }

        // Another properties
        public override bool IsEditable => !IsDeleted;
        public Guid? IIdKHTTNhiemVuChiId { get; set; }
        public Guid? IID_KHTT_NhiemVuChiID { get; set; }
        public Guid? IIdParentId { get; set; }
        public string SMaOrder { get; set; }
    }
}
