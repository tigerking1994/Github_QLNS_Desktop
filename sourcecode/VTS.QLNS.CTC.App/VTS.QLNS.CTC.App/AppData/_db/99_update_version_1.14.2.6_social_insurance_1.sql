/****** Object:  StoredProcedure [dbo].[sp_rpt_kht_du_toan_thu_bhyt]    Script Date: 4/1/2024 5:14:57 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_kht_du_toan_thu_bhyt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_kht_du_toan_thu_bhyt]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_kht_du_toan_thu_bhxh]    Script Date: 4/1/2024 5:14:57 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_kht_du_toan_thu_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_kht_du_toan_thu_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_kht_du_toan_thu_bhxh]    Script Date: 4/1/2024 5:14:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_kht_du_toan_thu_bhxh] 
	@NamLamViec int,
	@LstSelectedUnit ntext,
	@KhoiDuToan nvarchar(50),
	@KhoiHachToan nvarchar(50),
	@SoQuyetDinh nvarchar(500),
	@NgayQuyetDinh nvarchar(500),
	@DVT int
AS
BEGIN
	declare @DataDuToan table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), BhxhNldDongDuToan float, BhxhNsddDongDuToan float, BHXHTongCongDuToan float, BhtnNldDongDuToan float, BhtnNsddDongDuToan float, BHTNTongCongDuToan float);
	declare @DataHachToan table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), BhxhNldDongHachToan float, BhxhNsddDongHachToan float, BHXHTongCongHachToan float, BhtnNldDongHachToan float, BhtnNsddDongHachToan float, BHTNTongCongHachToan float);

	INSERT INTO @DataDuToan (sTenDonVI, idDonVi, BhxhNldDongDuToan, BhxhNsddDongDuToan, BHXHTongCongDuToan, BhtnNldDongDuToan, BhtnNsddDongDuToan, BHTNTongCongDuToan)
		SELECT 
		   dt_dv.sTenDonVi,
		   dt_dv.id idDonVi,
		   BhxhNldDong = SUM(IsNull(A.ThuBHXHNLDDong,0)),
		   BhxhNsddDong = SUM(IsNull(A.ThuBHXHNSDDong,0)),
		   SUM(IsNull(A.ThuBHXHNLDDong,0)) + SUM(IsNull(A.ThuBHXHNSDDong,0)),
		   BhtnNldDong = SUM(IsNull(A.ThuBHTNNLDDong,0)),
		   BhtnNsddDong = SUM(IsNull(A.ThuBHTNNSDDong,0)),
		   SUM(IsNull(A.ThuBHTNNLDDong,0)) + SUM(IsNull(A.ThuBHTNNSDDong,0))
		FROM
		  (SELECT ml.iID_MLNS ,
				ml.iID_MLNS_Cha,
				ml.sNG,
				ml.sMoTa,
				ml.bHangCha,
				ml.sXauNoiMa,
				ctct.iID_MaDonVi,
				IsNull(ctct.fBHXH_NLD, 0) ThuBHXHNLDDong,
				IsNull(ctct.fBHXH_NSD, 0) ThuBHXHNSDDong,
				IsNull(ctct.fBHTN_NLD, 0) ThuBHTNNLDDong,
				IsNull(ctct.fBHTN_NSD, 0) ThuBHTNNSDDong
		   FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
		   LEFT JOIN BH_DTT_BHXH_PhanBo_ChungTu ct ON ct.iID_DTT_BHXH_PhanBo_ChungTu = ctct.iID_DTT_BHXH_ChungTu
		   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
		   AND ml.iNamLamViec = @NamLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS = @KhoiDuToan
		   WHERE ct.iNamLamViec = @NamLamViec
		   AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh))
		   AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))) AS A 
		   JOIN
		  (SELECT iID_MaDonVi AS id,
				  sTenDonVi, iLoai
		   FROM DonVi
		   WHERE iTrangThai = 1
		   AND iNamLamViec = @NamLamViec
		   AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   
		GROUP BY dt_dv.sTenDonVi,
				dt_dv.id;

	INSERT INTO @DataHachToan (sTenDonVI, idDonVi, BhxhNldDongHachToan, BhxhNsddDongHachToan, BHXHTongCongHachToan, BhtnNldDongHachToan, BhtnNsddDongHachToan, BHTNTongCongHachToan)
		SELECT 
		   dt_dv.sTenDonVi,
		   dt_dv.id idDonVi,
		   BhxhNldDong = SUM(IsNull(A.ThuBHXHNLDDong,0)),
		   BhxhNsddDong = SUM(IsNull(A.ThuBHXHNSDDong,0)),
		   SUM(IsNull(A.ThuBHXHNLDDong,0)) + SUM(IsNull(A.ThuBHXHNSDDong,0)),
		   BhtnNldDong = SUM(IsNull(A.ThuBHTNNLDDong,0)),
		   BhtnNsddDong = SUM(IsNull(A.ThuBHTNNSDDong,0)),
		   SUM(IsNull(A.ThuBHTNNLDDong,0)) + SUM(IsNull(A.ThuBHTNNSDDong,0))

		FROM
		  (SELECT ml.iID_MLNS ,
				ml.iID_MLNS_Cha,
				ml.sNG,
				ml.sMoTa,
				ml.bHangCha,
				ml.sXauNoiMa,
				ctct.iID_MaDonVi,
				IsNull(ctct.fBHXH_NLD, 0) ThuBHXHNLDDong,
				IsNull(ctct.fBHXH_NSD, 0) ThuBHXHNSDDong,
				IsNull(ctct.fBHTN_NLD, 0) ThuBHTNNLDDong,
				IsNull(ctct.fBHTN_NSD, 0) ThuBHTNNSDDong
		   FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
		   LEFT JOIN BH_DTT_BHXH_PhanBo_ChungTu ct ON ct.iID_DTT_BHXH_PhanBo_ChungTu = ctct.iID_DTT_BHXH_ChungTu
		   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
		   AND ml.iNamLamViec = @NamLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS = @KhoiHachToan
		   WHERE ct.iNamLamViec = @NamLamViec
		   AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh))
		   AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))) AS A 
		   JOIN
		  (SELECT iID_MaDonVi AS id,
				  sTenDonVi, iLoai
		   FROM DonVi
		   WHERE iTrangThai = 1
		   AND iNamLamViec = @NamLamViec
		   AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   
		GROUP BY dt_dv.sTenDonVi,
				dt_dv.id;

	SELECT donvi.iID_MaDonVi idDonVi, 
	donvi.sTenDonVI, 
	IsNull(dt.BhxhNldDongDuToan, 0)/@DVT BhxhNldDongDuToan, 
	IsNull(dt.BhxhNsddDongDuToan, 0)/@DVT BhxhNsddDongDuToan, 
	IsNull(ht.BhxhNldDongHachToan, 0)/@DVT BhxhNldDongHachToan, 
	IsNull(ht.BhxhNsddDongHachToan, 0)/@DVT BhxhNsddDongHachToan,
	IsNull(dt.BHXHTongCongDuToan, 0)/@DVT BHXHTongCongDuToan,
	IsNull(ht.BHXHTongCongHachToan, 0)/@DVT BHXHTongCongHachToan,
	IsNull(dt.BhtnNldDongDuToan, 0)/@DVT BhtnNldDongDuToan, 
	IsNull(dt.BhtnNsddDongDuToan, 0)/@DVT BhtnNsddDongDuToan,
	IsNull(ht.BhtnNldDongHachToan, 0)/@DVT BhtnNldDongHachToan, 
	IsNull(ht.BhtnNsddDongHachToan, 0)/@DVT BhtnNsddDongHachToan,
	IsNull(dt.BHTNTongCongDuToan, 0)/@DVT BHTNTongCongDuToan,
	IsNull(ht.BHTNTongCongHachToan, 0)/@DVT BHTNTongCongHachToan
	FROM 
	DonVi donvi
	LEFT JOIN @DataDuToan dt ON donvi.iID_MaDonVi = dt.idDonVi
	LEFT JOIN @DataHachToan ht ON donvi.iID_MaDonVi = ht.idDonVi
	WHERE donvi.iTrangThai = 1
		   AND donvi.iNamLamViec = @NamLamViec
		   AND donvi.iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))

END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_kht_du_toan_thu_bhyt]    Script Date: 4/1/2024 5:14:57 PM ******/
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
	@DVT int
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
GO
