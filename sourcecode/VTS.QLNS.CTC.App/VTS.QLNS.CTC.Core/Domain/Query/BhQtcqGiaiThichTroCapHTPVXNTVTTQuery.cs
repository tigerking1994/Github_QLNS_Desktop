using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhQtcqGiaiThichTroCapHTPVXNTVTTQuery
    {
        public bool BHangCha { get; set; }
        public string STT { get; set; }
        public string STenCanBo { get; set; }
        public string SMaCapBac { get; set; }
        public string SSoQuyetDinh { get; set; }
        public string STenPhanHo {  get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public double? FTienTroCap1Lan { get; set; }
        public double? FTienTroCapKV { get; set; }
        public double? FTienTroCapMT { get; set; }
        public double? FTienTroCap1LanTL { get; set; }
        public double? FTienTroCapKVTL { get; set; }
        public double? FTienTroCapMTTL { get; set; }
        public bool BHasData { get; set; }
        public bool IsNotData => FTienTroCap1Lan.GetValueOrDefault(0) != 0 || FTienTroCapKV.GetValueOrDefault(0) != 0 || FTienTroCapMT.GetValueOrDefault(0) != 0 || FTienTroCap1LanTL.GetValueOrDefault(0) != 0 || FTienTroCapKVTL.GetValueOrDefault(0) != 0 || FTienTroCapMTTL.GetValueOrDefault(0) != 0;
        public double? FTongTien => FTienTroCap1Lan.GetValueOrDefault(0) + FTienTroCapKV.GetValueOrDefault(0) + FTienTroCapMT.GetValueOrDefault(0);
        public double? FTongTienTL => FTienTroCap1LanTL.GetValueOrDefault(0) + FTienTroCapKVTL.GetValueOrDefault(0) + FTienTroCapMTTL.GetValueOrDefault(0);
        public double? FTongTienAll => FTongTien.GetValueOrDefault(0) + FTongTienTL.GetValueOrDefault(0);
        public string SNgayQuyetDinh => DNgayQuyetDinh?.ToString("dd/MM/yyyy");
        public bool IsHangCha => BHangCha;
        public int Type { get; set; }
        public bool? IsParent { get; set; }
    }
}
