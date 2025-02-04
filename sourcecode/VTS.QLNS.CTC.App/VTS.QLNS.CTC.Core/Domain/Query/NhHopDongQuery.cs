using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NhHopDongQuery
    {
        public Guid Id { get; set; }

        public string SSoHopDong { get; set; }
        public string STenHopDong { get; set; }

        [Column("iID_TiGiaUSD_VNDID")]
        public Guid? IIdTiGiaUsdVndId { get; set; }

        [Column("iID_TiGiaUSD_NgoaiTeKhacID")]
        public Guid? IIdTiGiaUsdNgoaiTeKhacID { get; set; }

        public double? FGiaTriNgoaiTeKhac { get; set; }

        public double? FGiaTriUSD { get; set; }

        public double? FGiaTriVND { get; set; }

        public bool? BHieuLuc { get; set; }

        // detail tỉ giá 
        public string SMaTienTe1UsdVnd { get; set; }
        public string SMaTienTe2UsdVnd { get; set; }
        public double? FTiGia1 { get; set; }

        public string SMaTienTe1NgoaiTeKhacUsd { get; set; }
        public string SMaTienTe2NgoaiTeKhacUsd { get; set; }

        public double? FTiGia2 { get; set; }

    }
}
