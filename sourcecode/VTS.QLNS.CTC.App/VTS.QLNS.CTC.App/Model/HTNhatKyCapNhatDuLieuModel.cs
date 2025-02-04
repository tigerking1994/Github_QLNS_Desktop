using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Extensions;

namespace VTS.QLNS.CTC.App.Model
{
    public class HTNhatKyCapNhatDuLieuModel : ModelBase
    {
        public string ApplicationCode { get; set; }
        [DisplayDetailInfo("Danh mục")]
        public string ServiceCode { get; set; }
        public string SessionId { get; set; }
        public string IpPortParentNode { get; set; }
        public string IpPortCurrentNode { get; set; }
        public string RequestContent { get; set; }
        public string ResponseContent { get; set; }

        private DateTime? _startTime;
        [DisplayDetailInfo("Thời gian bắt đầu")]
        public DateTime? StartTime 
        {
            get => _startTime;
            set => SetProperty(ref _startTime, value);
        }

        private DateTime? _endTime;
        [DisplayDetailInfo("Thời gian kết thúc")]
        public DateTime? EndTime 
        {
            get => _endTime;
            set => SetProperty(ref _endTime, value);
        }

        [DisplayDetailInfo("Thời gian thực hiện")]
        public int? Duration { get; set; }
        [DisplayDetailInfo("Mã lỗi")]
        public decimal? ErrorCode { get; set; }
        [DisplayDetailInfo("Chi tiết lỗi")]
        public string ErrorDescription { get; set; }
        public bool? TransactionStatus { get; set; }
        [DisplayDetailInfo("Hành động")]
        public string ActionName { get; set; }
        [DisplayDetailInfo("Người thực hiện")]
        public string UserName { get; set; }

        private string _account;
        public string Account 
        { 
            get => _account;
            set => SetProperty(ref _account, value);
        }

        public string StartTimeToString
        {
            get => StartTime.HasValue ? StartTime.Value.ToString("dd/MM/yyyy hh:mm:ss tt") : "";
        }

        public string EndTimeToString
        {
            get => EndTime.HasValue ? EndTime.Value.ToString("dd/MM/yyyy hh:mm:ss tt") : "";
        }

        [DisplayDetailInfo("Kết quả")]
        public string TransactionStatusToString
        {
            get => !TransactionStatus.HasValue ? "" : TransactionStatus.Value ? "Success": "Error";
        }
            
    }
}
