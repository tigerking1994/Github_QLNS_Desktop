namespace VTS.QLNS.CTC.Utility.Enum
{
    public static class ConstantUrlPathPhanHe
    {
        // export file -> local đến server : receive
        // import từ server về -> local sử dụng : send


        public const string UrlFolderFile = @"AppData\FtpSave";
        public const string UrlFolderTemp = @"AppData\Temp";

        public const string UrlKhthdxWinformReceive = "VDT/KeHoachTrungHanDeXuat/send"; //Gửi lên server
        public const string UrlKhthdxWinformSend = "VDT/KeHoachTrungHanDeXuat/send";//nhận từ server

        public const string UrlKhchddWinformReceive = "VDT/KeHoachTrungHanDuocDuyet/receive";
        public const string UrlKhchddWinformSend = "VDT/KeHoachTrungHanDuocDuyet/receive";

        public const string UrlKhvndxWinformReceive = "VDT/KeHoachVonNamDeXuat/send";
        public const string UrlKhvndxWinformSend = "VDT/KeHoachVonNamDeXuat/send";

        public const string UrlKhcqWinformReceive = "VDT/QLKeHoachChiQuy/send";
        public const string UrlKhcqWinformSend = "VDT/QLKeHoachChiQuy/send";

        public const string UrlKhnddWinformReceive = "VDT/KeHoachVonNamDuocDuyet/receive";
        public const string UrlKhnddWinformSend = "VDT/KeHoachVonNamDuocDuyet/receive";
        //
        public const string UrlBcqtcnvdtWinformReceiveTt = "VDT/BcQuyetToanNienDo/ThanhToan/send";
        public const string UrlBcqtcnvdtWinformReceiveTu = "VDT/BcQuyetToanNienDo/ThanhToanTamUng/send";
        public const string UrlBcqtcnvdtWinformSendTt = "VDT/BcQuyetToanNienDo/ThanhToan/send";
        public const string UrlBcqtcnvdtWinformSendTu = "VDT/BcQuyetToanNienDo/ThanhToanTamUng/send";
        //Quản lý ngân sách

        public const string UrlQlnsNcdvformReceive = "VNS/NhuCauDonVi/send"; //Gửi lên server
        public const string UrlQlnsNcdvformSend = "VNS/NhuCauDonVi/send";//nhận từ server

        public const string UrlQlnsSktformReceive = "VNS/SoKiemTra/send";
        public const string UrlQlnsSktformSend = "VNS/SoKiemTra/send";

        public const string UrlQlnsDtDnformReceive = "VNS/DuToanDauNam/send";
        public const string UrlQlnsDtDnformSend = "VNS/DuToanDauNam/send";


        public const string UrlQlnsCpformReceive = "VNS/ChungTuCapPhat/send";
        public const string UrlQlnsCpformSend = "VNS/ChungTuCapPhat/send";

        public const string UrlQtnsWinformSend = "VNS/Qtns/send";
        public const string UrlQtnsWinformReceive = "VNS/Qtns/send";

        public const string UrlPbdtWinformReceive = "VNS/DuToan/send";
        public const string UrlPbdtWinformSend = "VNS/DuToan/send";
    }
}
