﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NhNhuCauChiQuyBaoCaoQuery
    {
        public virtual Guid ID { get; set; }
        public virtual Guid? iID_NhuCauChiQuyID { get; set; }
        public virtual Guid? iID_KHCTBQP_NhiemVuChiID { get; set; }
        public virtual Guid? iID_HopDongID { get; set; }
        public virtual string sNoiDung { get; set; }
        public virtual double? fChiNgoaiTeUSD { get; set; }
        public virtual double? fChiNgoaiTeVND { get; set; }
        public virtual double? fChiTrongNuocUSD { get; set; }
        public virtual double? fChiTrongNuocVND { get; set; }
        public virtual Guid? iID_ParentID { get; set; }
        public virtual Guid? iID_DuAnID { get; set; }
        public int IDDs { get; set; }
        public virtual Guid iID_DonViID { get; set; }
        public virtual Guid ID_NhiemVuChi { get; set; }
        public string sTenHopDong { get; set; }
        public string sTenDonvi { get; set; }
        public string sTenNhiemVuChi { get; set; }
        public bool isDelete { get; set; }
        public string sChiNgoaiTeUSD { get; set; }
        public string sChiNgoaiTeVND { get; set; }
        public string sChiTrongNuocUSD { get; set; }
        public string sChiTrongNuocVND { get; set; }
        public string depth { get; set; }
        public virtual double? GiaTriHopDongUSD { get; set; }
        public virtual double? fChiNgoaiTeUSDTyGia { get; set; }
        public virtual double? fChiTrongNuocVNDTyGia { get; set; }
        public virtual double? GiaTriHopDongVND { get; set; }
        public virtual double? GiaTriTongTheUSD { get; set; }
        public virtual double? GiaTriGaiDoanUSD { get; set; }
        public virtual double? GiaTriBQPUSD { get; set; }
        public virtual double? KinhPhiUSD { get; set; }
        public virtual double? KinhPhiVND { get; set; }
        public virtual double? KinhPhiDaChiUSD { get; set; }
        public virtual double? KinhPhiDaChiVND { get; set; }
        public virtual double? KinhPhiToYUSD { get; set; }
        public virtual double? KinhPhiToYVND { get; set; }
        public virtual double? KinhPhiDaChiToYUSD { get; set; }
        public virtual double? KinhPhiDaChiToYVND { get; set; }
        public string Noidungchi { get; set; }
    }
}
