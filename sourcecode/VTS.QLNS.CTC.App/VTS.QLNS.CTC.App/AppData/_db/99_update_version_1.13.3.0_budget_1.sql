/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_trinh_ky]    Script Date: 17/10/2023 7:24:56 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_trinh_ky]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_trinh_ky]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_phan_bo_so_kiem_tra_dv]    Script Date: 17/10/2023 4:17:15 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_phan_bo_so_kiem_tra_dv]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_phan_bo_so_kiem_tra_dv]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_phan_bo_so_kiem_tra_dv]    Script Date: 17/10/2023 4:17:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_skt_phan_bo_so_kiem_tra_dv]
	@idDV varchar(20),
	@idChungTu varchar(200),
	@LoaiChungTu int,
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@dvt int,
	@iLoai int,
	@iLoaiNNS int
AS
BEGIN
SELECT ml.sM AS M ,
       ml.sKyHieu AS KyHieu ,
       ml.sMoTa AS MoTa ,
       ml.iID_MLSKT AS IdMucLuc ,
       ml.iID_MLSKTCha AS IdParent ,
       ml.sSTT AS STT ,
       ml.bHangCha ,
	   ct.sGhiChu,
       TuChi =ISNULL(SUM(ct.fTuChi), 0)/@dvt ,
       HuyDong =ISNULL(SUM(ct.fHuyDongTonKho), 0)/@dvt ,
       PhanCap =ISNULL(SUM(ct.fPhanCap), 0)/@dvt ,
       MuaHangHienVat =ISNULL(SUM(ct.fMuaHangCapHienVat), 0)/@dvt ,
       DacThu =ISNULL(SUM(ct.fPhanCap), 0)/@dvt ,
       TongCongNSSD = ISNULL(SUM(ct.fTuChi), 0)/@dvt ,
       TongCongNSBD = ISNULL(SUM(ct.fTuChi), 0)/@dvt
FROM NS_SKT_MucLuc ml
LEFT JOIN NS_SKT_ChungTuChiTiet ct ON ml.sKyHieu = ct.sKyHieu AND ml.iNamLamViec = ct.iNamLamViec
AND ml.iTrangThai=1
AND ml.iNamLamViec = @NamLamViec
AND ct.iNamLamViec = @NamLamViec
AND ct.iLoai = @iLoai
AND ct.iLoaiChungTu = @LoaiChungTu
AND ct.iID_MaDonVi = @idDV
--AND ct.iID_CTSoKiemTra = @idChungTu
AND ct.iNamNganSach = @NamNganSach
AND ct.iID_MaNguonNganSach = @NguonNganSach
LEFT JOIN NS_SKT_ChungTu ctc 
ON ct.iID_CTSoKiemTra = ctc.iID_CTSoKiemTra 
AND ctc.iNamLamViec = ct.iNamLamViec
AND ctc.iNamLamViec = @NamLamViec
AND (@iLoaiNNS = 0 OR ctc.iLoaiNguonNganSach = @iLoaiNNS)

GROUP BY ml.sM ,
         ml.sKyHieu ,
         ml.sMoTa ,
         ml.iID_MLSKT ,
         ml.iID_MLSKTCha ,
         ml.sSTT ,
         ml.bHangCha,
		 ct.sGhiChu
ORDER BY ml.sKyHieu;
END
;
;
;
;
;


GO


/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_trinh_ky]    Script Date: 17/10/2023 7:24:56 PM ******/
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
	@dvt int,
	@iLoaiNNS int
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
   JOIN NS_SKT_ChungTu ct ON ctc.iID_CTSoKiemTra = ct.iID_CTSoKiemTra AND ct.iNamLamViec = ctc.iNamLamViec
   AND (@iLoaiNNS = 0 OR ct.iLoaiNguonNganSach = @iLoaiNNS)
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
   JOIN NS_SKT_ChungTu ct ON ctc.iID_CTSoKiemTra = ct.iID_CTSoKiemTra AND ct.iNamLamViec = ctc.iNamLamViec
   AND (@iLoaiNNS = 0 OR ct.iLoaiNguonNganSach = @iLoaiNNS)

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
   AND (@iLoaiNNS = 0 OR ct.iLoaiNguonNganSach = @iLoaiNNS)

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
;
GO
