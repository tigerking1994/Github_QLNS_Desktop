using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Extensions;

namespace VTS.QLNS.CTC.App.Model.Import
{
    public class BaseImportModel : BindableBase
    {
        private bool _importStatus;
        [DisplayDetailInfo("Ok", true)]
        public bool ImportStatus
        {
            get => _importStatus;
            set => SetProperty(ref _importStatus, value);
        }

        [DisplayDetailInfo("Chi tiết lỗi", true)]
        public string ErrorDetail { get; set; }

        private bool _isImported;
        [DisplayDetailInfo("Lấy dữ liệu", false)]
        public bool IsImported
        {
            get => _isImported;
            set => SetProperty(ref _isImported, value);
        }
    }
}
