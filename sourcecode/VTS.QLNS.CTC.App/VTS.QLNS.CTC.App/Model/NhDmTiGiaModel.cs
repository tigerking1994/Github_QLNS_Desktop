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
using System.Collections.ObjectModel;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhDmTiGiaModel : ModelBase
    {
        private string _sMaTiGia;
        [DisplayName("Mã tỉ giá")]
        [DisplayDetailInfo("Mã tỉ giá")]
        [ColumnIndex(0)]
        [Validate("Mã tỉ giá", Utility.Enum.DATA_TYPE.String, true)]
        public string SMaTiGia
        {
            get => _sMaTiGia;
            set => SetProperty(ref _sMaTiGia, value);
        }

        private string _sTenTiGia;
        [DisplayName("Tên tỉ giá")]
        [DisplayDetailInfo("Tên tỉ giá")]
        [Validate("Tên tỉ giá", Utility.Enum.DATA_TYPE.String, true)]
        public string STenTiGia
        {
            get => _sTenTiGia;
            set => SetProperty(ref _sTenTiGia, value);
        }

        private string _sMoTaTiGia;
        [DisplayName("Mô tả tỉ giá")]
        [DisplayDetailInfo("Mô tả tỉ giá")]
        public string SMoTaTiGia
        {
            get => _sMoTaTiGia;
            set => SetProperty(ref _sMoTaTiGia, value);
        }

        private Guid? _iIdTienTeGocId;
        [DisplayName("Mã tiền tệ gốc")]
        [ColumnType(ColumnType.Combobox)]
        [Validate("Mã tiền tệ gốc", Utility.Enum.DATA_TYPE.Guid, true)]
        public Guid? IIdTienTeGocId
        {
            get => _iIdTienTeGocId;
            set => SetProperty(ref _iIdTienTeGocId, value);
        }
        [Validate("Mã tiền tệ gốc", Utility.Enum.DATA_TYPE.String, true)]
        public string SMaTienTeGoc { get; set; }

        private DateTime? _dNgayTao;
        [DisplayName("Ngày lập")]
        [DisplayDetailInfo("Ngày Lập")]
        [Validate("Ngày lập", Utility.Enum.DATA_TYPE.Date, true)]
        public DateTime? DNgayTao
        {
            get => _dNgayTao;
            set => SetProperty(ref _dNgayTao, value);
        }

        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }

        private bool _selected;
        public bool Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }

        public override bool IsEditable => !IsDeleted;

        public string SMaTienTe1 { get; set; }
        public string SMaTienTe2 { get; set; }
        public double FTiGiaHoiDoai { get; set; }
        public DateTime? DThangLapTiGia { get; set; }
        public ObservableCollection<NhDmTiGiaChiTietModel> TiGiaChiTiets { get; set; }
        public string SSoThongBaoKBNN { get; set; }
        public int? IThangTiGia { get; set; }
        public int? INamTiGia { get; set; }

        [DisplayName("Ngày ban hành thông báo KBNN")]
        [DisplayDetailInfo("Ngày ban hành thông báo KBNN")]
        [Validate("Ngày ban hành thông báo KBNN", Utility.Enum.DATA_TYPE.Date, true)]
        public DateTime? DNgayBanHanhKBNN { get; set; }

        public string STenTiGiaFormat
        {
            get
            {
                return INamTiGia != null && IThangTiGia != null ? $"Tỉ giá tháng {IThangTiGia} năm {INamTiGia}" : string.Empty;
            }
        }
    }
}