using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class RptBangLuongTruyLinhModel : BindableBase
    {
        public int iStt { get; set; }
        public string sTenCbo { get; set; }
        public string sMaCb { get; set; }
        public decimal? iNtn { get; set; }
        public decimal? fLhtHs { get; set; }
        public decimal? fLhtHsCu { get; set; }
        public string sHeSoChenhLech => (fLhtHs - fLhtHsCu).ToString();
        public decimal? iTtl { get; set; }
        public decimal? fLhtTt { get; set; }
        public decimal? fLhtTtCu { get; set; }
        public decimal? fPctnvkTt { get; set; }
        public decimal? fLuongCoBan => fLhtTt - fLhtTtCu + fPctnvkTt;
        public decimal? fPccvTt { get; set; }
        public decimal? fPccvTtCu { get; set; }
        public decimal? fPhuCapChucvu => fPccvTt - fPccvTtCu;
        public decimal? fPccovTt { get; set; }
        public decimal? fPctnTt { get; set; }
        public decimal? fLuongThangSum { get; set; }
        public decimal? fPhaiTruSum { get; set; }
        public decimal? fThanhTien { get; set; }
        public decimal? fKPQP { get; set; }
        public decimal? fKPNN { get; set; }
        public decimal? fKPBH { get; set; }
    }
}
