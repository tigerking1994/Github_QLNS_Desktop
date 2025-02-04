/****** Object:  StoredProcedure [dbo].[sp_rpt_kht_du_toan_thu_bhyt]    Script Date: 26/04/2024 6:18:29 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_kht_du_toan_thu_bhyt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_kht_du_toan_thu_bhyt]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_kht_du_toan_thu_bhyt]    Script Date: 26/04/2024 6:18:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_kht_du_toan_thu_bhyt] 
	@NamLamViec int,
	@LstSelectedUnit ntext,
	@KhoiDuToan nvarchar(50),
	@KhoiHachToan nvarchar(50),
	@SM nvarchar(50),
	@SoQuyetDinh nvarchar(500),
	@NgayQuyetDinh nvarchar(500),
	@DVT int,
	@IsMillionRound bit
AS
BEGIN
	declare @BhytDuToan table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), sm nvarchar(50),BhytNSDDongDuToan float, BhytNLDDongDuToan float, TongBhytDuToan float);
	declare @BhytHachToan table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), sm nvarchar(50), BhytNSDDongHachToan float,BhytNLDDongHachToan float, TongBhytHachToan float);

	INSERT INTO @BhytDuToan (sTenDonVI, idDonVi, sm, BhytNSDDongDuToan, BhytNLDDongDuToan, TongBhytDuToan)
		SELECT 
		   dt_dv.sTenDonVi,
		   dt_dv.id idDonVi,
		   A.sm,
		   SUM(IsNull(A.ThuBHYTNSDDongDuToan, 0)) BhytNSDDongDuToan,
		   SUM(IsNull(A.ThuBHYTNLDDongDuToan, 0)) BhytNLDDongDuToan,
		   SUM(IsNull(A.TongBhytDuToan, 0)) TongBhytDuToan

	FROM
	  (SELECT ml.sm,
			   ml.sMoTa,
			   ctct.iID_MaDonVi,
			   IsNull(ctct.fBHYT_NSD, 0) ThuBHYTNSDDongDuToan,
			   IsNull(ctct.fBHYT_NLD, 0) ThuBHYTNLDDongDuToan,
			   IsNull(ctct.fThuBHYT, 0) TongBhytDuToan
	   FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
	   LEFT JOIN BH_DTT_BHXH_PhanBo_ChungTu ct ON ct.iID_DTT_BHXH_PhanBo_ChungTu = ctct.iID_DTT_BHXH_ChungTu
	   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
	   AND ml.iNamLamViec = @NamLamViec
	   AND ml.iTrangThai = 1
	   AND ml.sLNS = @KhoiDuToan
	   AND ml.sM = @SM
	   WHERE ct.iNamLamViec = @NamLamViec
	   AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh))
		   AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))) AS A 
	   JOIN
	  (SELECT iID_MaDonVi AS id,
			  sTenDonVi, iLoai
	   FROM DonVi
	   WHERE iTrangThai = 1
	   AND iNamLamViec = @namLamViec
	   AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   
	GROUP BY dt_dv.sTenDonVi,
			dt_dv.id,
			A.sm;

	INSERT INTO @BhytHachToan (idDonVi, sTenDonVI, sm, BhytNSDDongHachToan, BhytNLDDongHachToan, TongBhytHachToan)
		SELECT
			dt_dv.id idDonVi,
		   dt_dv.sTenDonVi,
		   A.sm,
		   SUM(IsNull(A.ThuBHYTNSDDongHachToan, 0)) BhytNSDDongHachToan,
		   SUM(IsNull(A.ThuBHYTNLDDongHachToan, 0)) BhytNLDDongHachToan,
		   SUM(IsNull(A.TongBhytHachToan, 0)) TongBhytHachToan

	FROM
	  (SELECT ml.sm,
			   ml.sMoTa,
			   ctct.iID_MaDonVi,
			   IsNull(ctct.fBHYT_NSD, 0) ThuBHYTNSDDongHachToan,
			   IsNull(ctct.fBHYT_NLD, 0) ThuBHYTNLDDongHachToan,
			   IsNull(ctct.fThuBHYT, 0) TongBhytHachToan
	   FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
	   LEFT JOIN BH_DTT_BHXH_PhanBo_ChungTu ct ON ct.iID_DTT_BHXH_PhanBo_ChungTu = ctct.iID_DTT_BHXH_ChungTu
	   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
	   AND ml.iNamLamViec = @NamLamViec
	   AND ml.iTrangThai = 1
	   AND ml.sLNS = @KhoiHachToan
	   AND ml.sM = @SM
	   WHERE ct.iNamLamViec = @namLamViec
	   AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh))
	   AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))) AS A 
	   JOIN
	  (SELECT iID_MaDonVi AS id,
			  sTenDonVi, iLoai
	   FROM DonVi
	   WHERE iTrangThai = 1
	   AND iNamLamViec = @namLamViec
	   AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   
	GROUP BY dt_dv.sTenDonVi,
			dt_dv.id,
			A.sm;

	if (@IsMillionRound = 1)
	SELECT donvi.iID_MaDonVi idDonVi, 
	donvi.sTenDonVI, 
	round(IsNull(dt.BhytNLDDongDuToan, 0) / 1000000, 0) * 1000000 /@DVT BhytNldDongDuToan, 
	round(IsNull(dt.BhytNSDDongDuToan, 0) / 1000000, 0) * 1000000 /@DVT BhytNsddDongDuToan, 
	round(IsNull(ht.BhytNLDDongHachToan, 0) / 1000000, 0) * 1000000 /@DVT BhytNldDongHachToan, 
	round(IsNull(ht.BhytNSDDongHachToan, 0) / 1000000, 0) * 1000000 /@DVT BhytNsddDongHachToan, 
	round(IsNull(dt.TongBhytDuToan, 0) / 1000000, 0) * 1000000 /@DVT BHYTTongCongDuToan, 
	round(IsNull(ht.TongBhytHachToan, 0) / 1000000, 0) * 1000000 /@DVT BHYTTongCongHachToan
	FROM
	DonVi donvi
	LEFT JOIN @BhytDuToan dt ON donvi.iID_MaDonVi = dt.idDonVi
	LEFT JOIN @BhytHachToan ht ON donvi.iID_MaDonVi = ht.idDonVi
	WHERE donvi.iTrangThai = 1
		   AND donvi.iNamLamViec = @NamLamViec
		   AND donvi.iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
	else
	SELECT donvi.iID_MaDonVi idDonVi, 
	donvi.sTenDonVI, 
	IsNull(dt.BhytNLDDongDuToan, 0)/@DVT BhytNldDongDuToan, 
	IsNull(dt.BhytNSDDongDuToan, 0)/@DVT BhytNsddDongDuToan, 
	IsNull(ht.BhytNLDDongHachToan, 0)/@DVT BhytNldDongHachToan, 
	IsNull(ht.BhytNSDDongHachToan, 0)/@DVT BhytNsddDongHachToan,
	IsNull(dt.TongBhytDuToan, 0)/@DVT BHYTTongCongDuToan,
	IsNull(ht.TongBhytHachToan, 0)/@DVT BHYTTongCongHachToan
	FROM
	DonVi donvi
	LEFT JOIN @BhytDuToan dt ON donvi.iID_MaDonVi = dt.idDonVi
	LEFT JOIN @BhytHachToan ht ON donvi.iID_MaDonVi = ht.idDonVi
	WHERE donvi.iTrangThai = 1
		   AND donvi.iNamLamViec = @NamLamViec
		   AND donvi.iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
END
;
;
;
;
;
GO
