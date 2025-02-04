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
    public class DmLoaiTienTeModel : ModelBase
    {
        private string _sMaTienTe;
        [DisplayName("Mã tiền tệ")]
        [DisplayDetailInfo("Mã tiền tệ")]
        [ColumnIndex(0)]
        [Validate("Mã tiền tệ", Utility.Enum.DATA_TYPE.String, 10, true)]
        public string SMaTienTe
        {
            get => _sMaTienTe;
            set => SetProperty(ref _sMaTienTe, value);
        }

        private string _sTenTienTe;
        [DisplayName("Tên tiền tệ")]
        [DisplayDetailInfo("Tên tiền tệ")]
        [Validate("Tên tiền tệ", Utility.Enum.DATA_TYPE.String, 255, true)]
        public string STenTienTe
        {
            get => _sTenTienTe;
            set => SetProperty(ref _sTenTienTe, value);
        }

        private string _sMoTaChiTiet;
        [DisplayName("Mô tả")]
        [DisplayDetailInfo("Mô tả")]
        [Validate("Mô tả")]
        public string SMoTaChiTiet
        {
            get => _sMoTaChiTiet;
            set => SetProperty(ref _sMoTaChiTiet, value);
        }

        public override bool IsEditable => !IsDeleted;
    }
}