/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_trinh_ky]    Script Date: 07/11/2022 6:18:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_trinh_ky]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_trinh_ky]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc]    Script Date: 07/11/2022 6:18:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc]    Script Date: 07/11/2022 6:18:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc]
	@idDV varchar(max),
	@LoaiChungTu int,
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@dvt int
AS
BEGIN

SELECT ml.iID_MLSKTCha IIdMlsktCha,
       ml.iID_MLSKT IIdMlskt,
       ml.sM,
       ml.sKyHieu,
       ml.sSTT,
       ml.sMoTa,
       ml.bHangCha ,
       IsNull(sum(ct.sumToTal), 0)/@dvt TongTuChi ,
       IsNull(sum(ct.sumTotalMuaHangHienVat), 0)/@dvt TongMuaHangHienVat ,
       IsNull(sum(ct.sumTotalDacThu), 0)/@dvt TongDacThu ,
       IsNull(sum(ct.sumDonVi), 0)/@dvt TongTuChiPB ,
       IsNull(sum(ct.sumDonViMuaHangHienVat), 0)/@dvt TongMuaHangHienVatPB ,
       IsNull(sum(ct.sumDonViDacThu), 0)/@dvt TongDacThuPB
FROM NS_SKT_MucLuc ml
LEFT JOIN
  (SELECT ml.iID_MLSKT,
          ml.sKyHieu,
          ml.sSTT,
          ml.sMoTa ,
          sum(ISNull(ctc.fTuChi, 0)) sumTotal ,
          sum(ISNull(ctc.fMuaHangCapHienVat, 0)) sumTotalMuaHangHienVat ,
          sum(ISNull(ctc.fPhanCap, 0)) sumTotalDacThu ,
          sumDonVi = 0 ,
          sumDonViMuaHangHienVat = 0 ,
          sumDonViDacThu = 0
   FROM NS_SKT_MucLuc ml
   LEFT JOIN (SELECT ctct.* FROM NS_SKT_ChungTuChiTiet ctct inner join NS_SKT_ChungTu ct on ctct.iID_CTSoKiemTra = ct.iID_CTSoKiemTra 
   WHERE (ctct.iLoai = 3 OR (ctct.iLoai = 2 and ct.iLoai = 2))
     AND ctct.iLoaiChungTu = @LoaiChungTu) ctc
    ON ml.iID_MLSKT = ctc.iID_MLSKT 
   AND ml.iNamLamViec = @NamLamViec
   AND ctc.iNamLamViec = @NamLamViec
   AND ctc.iNamNganSach = @NamNganSach
   AND ctc.iID_MaNguonNganSach = @NguonNganSach
   
   GROUP BY ml.iID_MLSKT,
            ml.sKyHieu,
            ml.sSTT,
            ml.sMoTa
   UNION ALL SELECT ml.iID_MLSKT,
                    ml.sKyHieu,
                    ml.sSTT,
                    ml.sMoTa ,
                    sumTotal = 0 ,
                    sumTotalMuaHangHienVat = 0 ,
                    sumTotalDacThu = 0 ,
                    sum(ISNull(ctc.fTuChi, 0)) sumDonVi ,
                    sum(ISNull(ctc.fMuaHangCapHienVat, 0)) sumDonViMuaHangHienVat ,
                    sum(ISNull(ctc.fPhanCap, 0)) sumDonViDacThu
   FROM NS_SKT_MucLuc ml
   LEFT JOIN NS_SKT_ChungTuChiTiet ctc ON ml.iID_MLSKT = ctc.iID_MLSKT
   AND ml.iNamLamViec = @NamLamViec
   AND ctc.iNamLamViec = @NamLamViec
   AND ctc.iNamNganSach = @NamNganSach
   AND ctc.iID_MaNguonNganSach = @NguonNganSach
   WHERE (ctc.iLoai = 4  or ctc.iLoai =2) --WHERE ctc.iLoai = 4 
     AND ctc.iLoaiChungTu = @LoaiChungTu
     AND iID_MaDonVi in
       (SELECT *
        FROM f_split(@idDV))
     AND exists
       (SELECT iID_CTSoKiemTra
        FROM NS_SKT_ChungTu ct
        WHERE ct.iID_CTSoKiemTra = ctc.iID_CTSoKiemTra)
   GROUP BY ml.iID_MLSKT,
            ml.sKyHieu,
            ml.sSTT,
            ml.sMoTa) AS ct ON ml.iID_MLSKT = ct.iID_MLSKT
AND ml.iNamLamViec = @NamLamViec
AND ml.iTrangThai = 1
GROUP BY ml.iID_MLSKTCha,
         ml.iID_MLSKT,
         ml.sM,
         ml.sKyHieu,
         ml.sSTT,
         ml.sMoTa,
         ml.bHangCha
ORDER BY ml.sKyHieu;
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_trinh_ky]    Script Date: 07/11/2022 6:18:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_trinh_ky]
	@idDV varchar(max),
	@LoaiChungTu int,
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@ChungTuSnc varchar(max),
	@dvt int
AS
BEGIN
SELECT rs.sTenDonVi,
	   rs.iID_MaDonVi MaDonVi,
       IsNull(sum(rs.sumSnc), 0)/@dvt TongSncTuChi ,
       IsNull(sum(rs.sumSncMuaHangHienVat), 0)/@dvt TongSncMuaHangHienVat ,
       IsNull(sum(rs.sumSncDacThu), 0)/@dvt TongSncDacThu ,
       IsNull(sum(rs.sumSkt), 0)/@dvt TongSktTuChi ,
       IsNull(sum(rs.sumSktMuaHangHienVat), 0)/@dvt TongSktMuaHangHienVat ,
       IsNull(sum(rs.sumSktDacThu), 0)/@dvt TongSktDacThu,
	   IsNull(sum(rs.sumSktNamTruoc), 0)/@dvt TongSktTuChiNamTruoc ,
       IsNull(sum(rs.sumSktMuaHangHienVatNamTruoc), 0)/@dvt TongSktMuaHangHienVatNamTruoc ,
       IsNull(sum(rs.sumSktDacThuNamTruoc), 0)/@dvt TongSktDacThuNamTruoc
FROM (SELECT dv.sTenDonVi,
		  dv.iID_MaDonVi,
		  sum(ISNull(ctc.fTuChi, 0)) sumSnc ,
          sum(ISNull(ctc.fMuaHangCapHienVat, 0)) sumSncMuaHangHienVat ,
          sum(ISNull(ctc.fPhanCap, 0)) sumSncDacThu ,
          sumSkt = 0 ,
          sumSktMuaHangHienVat = 0 ,
          sumSktDacThu = 0,
		  sumSktNamTruoc = 0 ,
          sumSktMuaHangHienVatNamTruoc = 0 ,
          sumSktDacThuNamTruoc = 0
   FROM DonVi dv
   JOIN NS_SKT_ChungTuChiTiet ctc ON dv.iID_MaDonVi = ctc.iID_MaDonVi
   AND dv.iNamLamViec = @NamLamViec
   AND ctc.iNamLamViec = @NamLamViec
   AND ctc.iNamNganSach = @NamNganSach
   AND ctc.iID_MaNguonNganSach = @NguonNganSach
   WHERE ctc.iLoai = 0
     AND ctc.iLoaiChungTu = @LoaiChungTu
	 AND ctc.iID_MaDonVi in
       (SELECT *
        FROM f_split(@idDV))
     AND exists
       (SELECT iID_CTSoKiemTra
        FROM NS_SKT_ChungTu ct
        WHERE ct.iID_CTSoKiemTra = ctc.iID_CTSoKiemTra and ct.iID_CTSoKiemTra in (select * from f_split(@ChungTuSnc)))
   GROUP BY dv.sTenDonVi,
		   dv.iID_MaDonVi
   UNION ALL SELECT dv.sTenDonVi,
		  dv.iID_MaDonVi,
		  sumSnc = 0,
          sumSncMuaHangHienVat = 0,
          sumSncDacThu = 0,
          sum(ISNull(ctc.fTuChi, 0)) sumSkt ,
          sum(ISNull(ctc.fMuaHangCapHienVat, 0)) sumSktMuaHangHienVat ,
          sum(ISNull(ctc.fPhanCap, 0)) sumSktDacThu,
		  sumSktNamTruoc = 0 ,
          sumSktMuaHangHienVatNamTruoc = 0 ,
          sumSktDacThuNamTruoc = 0
   FROM DonVi dv
   JOIN NS_SKT_ChungTuChiTiet ctc ON dv.iID_MaDonVi = ctc.iID_MaDonVi
   AND dv.iNamLamViec = @NamLamViec
   AND ctc.iNamLamViec = @NamLamViec
   AND ctc.iNamNganSach = @NamNganSach
   AND ctc.iID_MaNguonNganSach = @NguonNganSach
	WHERE (ctc.iLoai = 4 or ctc.iLoai = 2) -- WHERE (ctc.iLoai = 4)
     AND ctc.iLoaiChungTu = @LoaiChungTu
	 AND ctc.iID_MaDonVi in
       (SELECT *
        FROM f_split(@idDV))
     AND exists
       (SELECT iID_CTSoKiemTra
        FROM NS_SKT_ChungTu ct
        WHERE ct.iID_CTSoKiemTra = ctc.iID_CTSoKiemTra)
   GROUP BY dv.sTenDonVi,
		   dv.iID_MaDonVi
UNION ALL SELECT dv.sTenDonVi,
		  dv.iID_MaDonVi,
		  sumSnc = 0 ,
          sumSncMuaHangHienVat = 0,
          sumSncDacThu = 0,
          sumSkt = 0 ,
          sumSktMuaHangHienVat = 0 ,
          sumSktDacThu = 0,
		  sum(ISNull(ctc.fTuChi, 0)) sumSktNamTruoc,
          sum(ISNull(ctc.fMuaHangCapHienVat, 0)) sumSktMuaHangHienVatNamTruoc,
          sum(ISNull(ctc.fPhanCap, 0)) sumSktDacThuNamTruoc
   FROM DonVi dv
   JOIN NS_SKT_ChungTuChiTiet ctc ON dv.iID_MaDonVi = ctc.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
   JOIN NS_SKT_ChungTu ct ON ctc.iID_CTSoKiemTra = ct.iID_CTSoKiemTra AND ct.bKhoa = 1 AND ct.iNamLamViec = @NamLamViec - 1
   WHERE ctc.iNamNganSach = @NamNganSach
   AND ctc.iID_MaNguonNganSach = @NguonNganSach
   AND (ctc.iLoai = 4 or ctc.iLoai = 2) -- WHERE (ctc.iLoai = 4)
     AND ctc.iLoaiChungTu = @LoaiChungTu 
	 AND ctc.iID_MaDonVi in
       (SELECT *
        FROM f_split(@idDV))
   GROUP BY dv.sTenDonVi,
		   dv.iID_MaDonVi) as rs
		   Where rs.sumSnc != 0 
		   or rs.sumSncMuaHangHienVat != 0 
		   or rs.sumSncDacThu != 0 
		   or rs.sumSkt != 0 
		   or rs.sumSktMuaHangHienVat != 0 
		   or rs.sumSktDacThu != 0 
		   or rs.sumSktNamTruoc != 0 
		   or rs.sumSktMuaHangHienVatNamTruoc != 0 
		   or rs.sumSktDacThuNamTruoc != 0 
		   GROUP BY rs.sTenDonVi,
					rs.iID_MaDonVi;
END
;
;
;
GO
