/****** Object:  StoredProcedure [dbo].[sp_rpt_kht_du_toan_thu_theokhoi_bhyt]    Script Date: 10/23/2024 2:54:41 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_kht_du_toan_thu_theokhoi_bhyt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_kht_du_toan_thu_theokhoi_bhyt]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_kht_du_toan_thu_theokhoi_bhxh]    Script Date: 10/23/2024 2:54:41 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_kht_du_toan_thu_theokhoi_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_kht_du_toan_thu_theokhoi_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_kht_du_toan_thu_theokhoi_bhxh]    Script Date: 10/23/2024 2:54:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_kht_du_toan_thu_theokhoi_bhxh] 
	@NamLamViec int,
	@LstSelectedUnit ntext,
	@KhoiDuToan nvarchar(50),
	@KhoiHachToan nvarchar(50),
	@SoQuyetDinh nvarchar(500),
	@NgayQuyetDinh nvarchar(500),
	@DVT int,
	@IsMillionRound bit
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

	SELECT '' idDonVi,N'A. Khối dự toán' sTenDonVI,0 BhxhNldDongDuToan, 0 BhxhNsddDongDuToan, 0 BhxhNldDongHachToan, 0 BhxhNsddDongHachToan, 0 BHXHTongCongDuToan, 0 BHXHTongCongHachToan,0 BhtnNldDongDuToan,0 BhtnNsddDongDuToan,0 BhtnNldDongHachToan,0 BhtnNsddDongHachToan,0 BHTNTongCongDuToan,0 BHTNTongCongHachToan into #tempDuToan
	SELECT '' idDonVi, N'B. Khối hạch toán' sTenDonVI,0 BhxhNldDongDuToan, 0 BhxhNsddDongDuToan, 0 BhxhNldDongHachToan, 0 BhxhNsddDongHachToan, 0 BHXHTongCongDuToan, 0 BHXHTongCongHachToan,0 BhtnNldDongDuToan ,0 BhtnNsddDongDuToan,0 BhtnNldDongHachToan,0 BhtnNsddDongHachToan,0 BHTNTongCongDuToan,0 BHTNTongCongHachToan into #tempHachToan
	if (@IsMillionRound = 1)
		begin 
	-- data du toan
	SELECT donvi.iID_MaDonVi idDonVi, 
	donvi.sTenDonVI, 
	round(IsNull(dt.BhxhNldDongDuToan, 0) / 1000000, 0) * 1000000 /  @DVT BhxhNldDongDuToan, 
	round(IsNull(dt.BhxhNsddDongDuToan, 0) / 1000000, 0) * 1000000 /  @DVT BhxhNsddDongDuToan, 
	round(IsNull(ht.BhxhNldDongHachToan, 0) / 1000000, 0) * 1000000 /  @DVT BhxhNldDongHachToan, 
	round(IsNull(ht.BhxhNsddDongHachToan, 0) / 1000000, 0) * 1000000 /  @DVT BhxhNsddDongHachToan, 
	round(IsNull(dt.BHXHTongCongDuToan, 0) / 1000000, 0) * 1000000 /  @DVT BHXHTongCongDuToan, 
	round(IsNull(ht.BHXHTongCongHachToan, 0) / 1000000, 0) * 1000000 /  @DVT BHXHTongCongHachToan, 
	round(IsNull(dt.BhtnNldDongDuToan, 0) / 1000000, 0) * 1000000 /  @DVT BhtnNldDongDuToan, 
	round(IsNull(dt.BhtnNsddDongDuToan, 0) / 1000000, 0) * 1000000 /  @DVT BhtnNsddDongDuToan, 
	round(IsNull(ht.BhtnNldDongHachToan, 0) / 1000000, 0) * 1000000 /  @DVT BhtnNldDongHachToan, 
	round(IsNull(ht.BhtnNsddDongHachToan, 0) / 1000000, 0) * 1000000 /  @DVT BhtnNsddDongHachToan, 
	round(IsNull(dt.BHTNTongCongDuToan, 0) / 1000000, 0) * 1000000 /  @DVT BHTNTongCongDuToan, 
	round(IsNull(ht.BHTNTongCongHachToan, 0) / 1000000, 0) * 1000000 /  @DVT BHTNTongCongHachToan
	into #tempDataDuToan
	FROM 
	DonVi donvi
	LEFT JOIN @DataDuToan dt ON donvi.iID_MaDonVi = dt.idDonVi
	LEFT JOIN @DataHachToan ht ON donvi.iID_MaDonVi = ht.idDonVi
	WHERE donvi.iTrangThai = 1
		   AND donvi.iNamLamViec = @NamLamViec
		   AND donvi.iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		   AND donvi.iKhoi=2
	order by donvi.iID_MaDonVi

	-- data hach toan
	SELECT donvi.iID_MaDonVi idDonVi, 
	donvi.sTenDonVI, 
	round(IsNull(dt.BhxhNldDongDuToan, 0) / 1000000, 0) * 1000000 /  @DVT BhxhNldDongDuToan, 
	round(IsNull(dt.BhxhNsddDongDuToan, 0) / 1000000, 0) * 1000000 /  @DVT BhxhNsddDongDuToan, 
	round(IsNull(ht.BhxhNldDongHachToan, 0) / 1000000, 0) * 1000000 /  @DVT BhxhNldDongHachToan, 
	round(IsNull(ht.BhxhNsddDongHachToan, 0) / 1000000, 0) * 1000000 /  @DVT BhxhNsddDongHachToan, 
	round(IsNull(dt.BHXHTongCongDuToan, 0) / 1000000, 0) * 1000000 /  @DVT BHXHTongCongDuToan, 
	round(IsNull(ht.BHXHTongCongHachToan, 0) / 1000000, 0) * 1000000 /  @DVT BHXHTongCongHachToan, 
	round(IsNull(dt.BhtnNldDongDuToan, 0) / 1000000, 0) * 1000000 /  @DVT BhtnNldDongDuToan, 
	round(IsNull(dt.BhtnNsddDongDuToan, 0) / 1000000, 0) * 1000000 /  @DVT BhtnNsddDongDuToan, 
	round(IsNull(ht.BhtnNldDongHachToan, 0) / 1000000, 0) * 1000000 /  @DVT BhtnNldDongHachToan, 
	round(IsNull(ht.BhtnNsddDongHachToan, 0) / 1000000, 0) * 1000000 /  @DVT BhtnNsddDongHachToan, 
	round(IsNull(dt.BHTNTongCongDuToan, 0) / 1000000, 0) * 1000000 /  @DVT BHTNTongCongDuToan, 
	round(IsNull(ht.BHTNTongCongHachToan, 0) / 1000000, 0) * 1000000 /  @DVT BHTNTongCongHachToan
	into #tempDataHachToan
	FROM 
	DonVi donvi
	LEFT JOIN @DataDuToan dt ON donvi.iID_MaDonVi = dt.idDonVi
	LEFT JOIN @DataHachToan ht ON donvi.iID_MaDonVi = ht.idDonVi
	WHERE donvi.iTrangThai = 1
		   AND donvi.iNamLamViec = @NamLamViec
		   AND donvi.iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		   AND donvi.iKhoi!=2
	order by donvi.iID_MaDonVi

	-- Gop vao du toan
	SELECT SUM(B.BhxhNldDongDuToan) BhxhNldDongDuToan  ,
	SUM(B.BhxhNsddDongDuToan) BhxhNsddDongDuToan,
	SUM(B.BhxhNldDongHachToan) BhxhNldDongHachToan,
	SUM(B.BhxhNsddDongHachToan) BhxhNsddDongHachToan,
	SUM(B.BHXHTongCongDuToan) BHXHTongCongDuToan,
	SUM(B.BHXHTongCongHachToan) BHXHTongCongHachToan,
	SUM(B.BhtnNldDongDuToan)BhtnNldDongDuToan, 
	SUM(B.BhtnNsddDongDuToan) BhtnNsddDongDuToan,
	SUM(B.BhtnNldDongHachToan) BhtnNldDongHachToan,
	SUM(B.BhtnNsddDongHachToan) BhtnNsddDongHachToan,
	SUM(B.BHTNTongCongDuToan) BHTNTongCongDuToan, 
	SUM(B.BHTNTongCongHachToan) BHTNTongCongHachToan
	INTO #TempSumDataDuToan
	FROM #tempDataDuToan B

	UPDATE A
	SET A.BhxhNldDongDuToan=(B.BhxhNldDongDuToan),
		A.BhxhNsddDongDuToan=(B.BhxhNsddDongDuToan),
		A.BhxhNldDongHachToan=(B.BhxhNldDongHachToan),
		A.BhxhNsddDongHachToan=(B.BhxhNsddDongHachToan),
		A.BHXHTongCongDuToan=(B.BHXHTongCongDuToan),
		A.BHXHTongCongHachToan=(B.BHXHTongCongHachToan),
		A.BhtnNldDongDuToan=(B.BhtnNldDongDuToan),
		A.BhtnNsddDongDuToan=(B.BhtnNsddDongDuToan),
		A.BhtnNldDongHachToan=(B.BhtnNldDongHachToan),
		A.BhtnNsddDongHachToan=(B.BhtnNsddDongHachToan),
		A.BHTNTongCongDuToan=(B.BHTNTongCongDuToan),
		A.BHTNTongCongHachToan=(B.BHTNTongCongHachToan)
	FROM #tempDuToan A, #TempSumDataDuToan B

	SELECT * INTO #TempResultDuToan FROM 
	( 
	SELECT * FROM #tempDuToan
	Union all
	SELECT * FROM #tempDataDuToan
	) DuToan

	SELECT SUM(B.BhxhNldDongDuToan) BhxhNldDongDuToan  ,
	SUM(B.BhxhNsddDongDuToan) BhxhNsddDongDuToan,
	SUM(B.BhxhNldDongHachToan) BhxhNldDongHachToan,
	SUM(B.BhxhNsddDongHachToan) BhxhNsddDongHachToan,
	SUM(B.BHXHTongCongDuToan) BHXHTongCongDuToan,
	SUM(B.BHXHTongCongHachToan) BHXHTongCongHachToan,
	SUM(B.BhtnNldDongDuToan)BhtnNldDongDuToan, 
	SUM(B.BhtnNsddDongDuToan) BhtnNsddDongDuToan,
	SUM(B.BhtnNldDongHachToan) BhtnNldDongHachToan,
	SUM(B.BhtnNsddDongHachToan) BhtnNsddDongHachToan,
	SUM(B.BHTNTongCongDuToan) BHTNTongCongDuToan, 
	SUM(B.BHTNTongCongHachToan) BHTNTongCongHachToan
	INTO #TempSumDataHachToan
	FROM #tempDataHachToan B

	UPDATE A
	SET A.BhxhNldDongDuToan=(B.BhxhNldDongDuToan),
		A.BhxhNsddDongDuToan=(B.BhxhNsddDongDuToan),
		A.BhxhNldDongHachToan=(B.BhxhNldDongHachToan),
		A.BhxhNsddDongHachToan=(B.BhxhNsddDongHachToan),
		A.BHXHTongCongDuToan=(B.BHXHTongCongDuToan),
		A.BHXHTongCongHachToan=(B.BHXHTongCongHachToan),
		A.BhtnNldDongDuToan=(B.BhtnNldDongDuToan),
		A.BhtnNsddDongDuToan=(B.BhtnNsddDongDuToan),
		A.BhtnNldDongHachToan=(B.BhtnNldDongHachToan),
		A.BhtnNsddDongHachToan=(B.BhtnNsddDongHachToan),
		A.BHTNTongCongDuToan=(B.BHTNTongCongDuToan),
		A.BHTNTongCongHachToan=(B.BHTNTongCongHachToan)
	FROM #tempHachToan A, #TempSumDataHachToan B

	-- Gop vao Hach Toan
	SELECT * INTO #TempResultHachToan FROM 
	(
	SELECT * FROM #tempHachToan
	Union all
	SELECT * FROM #tempDataHachToan
	) HachToan
	-- Tra ra kết quả Result
	SELECT * FROM 
	( 
	SELECT * FROM #TempResultDuToan
	UNION ALL
	SELECT * FROM #TempResultHachToan ) Result
	DROP TABLE #TempSumDataDuToan
	DROP TABLE #tempSumDataHachToan
	DROP TABLE #tempDataDuToan
	DROP TABLE #tempDataHachToan
	DROP TABLE #TempResultDuToan
	DROP TABLE #TempResultHachToan

	end
	else
	begin
	-- data du toan
	SELECT donvi.iID_MaDonVi idDonVi, 
	donvi.sTenDonVI, 
	IsNull(dt.BhxhNldDongDuToan, 0)/ @DVT BhxhNldDongDuToan, 
	IsNull(dt.BhxhNsddDongDuToan, 0)/ @DVT BhxhNsddDongDuToan, 
	IsNull(ht.BhxhNldDongHachToan, 0)/ @DVT BhxhNldDongHachToan, 
	IsNull(ht.BhxhNsddDongHachToan, 0)/ @DVT BhxhNsddDongHachToan,
	IsNull(dt.BHXHTongCongDuToan, 0)/ @DVT BHXHTongCongDuToan,
	IsNull(ht.BHXHTongCongHachToan, 0)/ @DVT BHXHTongCongHachToan,
	IsNull(dt.BhtnNldDongDuToan, 0)/ @DVT BhtnNldDongDuToan, 
	IsNull(dt.BhtnNsddDongDuToan, 0)/ @DVT BhtnNsddDongDuToan,
	IsNull(ht.BhtnNldDongHachToan, 0)/ @DVT BhtnNldDongHachToan, 
	IsNull(ht.BhtnNsddDongHachToan, 0)/ @DVT BhtnNsddDongHachToan,
	IsNull(dt.BHTNTongCongDuToan, 0)/ @DVT BHTNTongCongDuToan,
	IsNull(ht.BHTNTongCongHachToan, 0)/ @DVT BHTNTongCongHachToan
	into #tempDataDuToan1
	FROM 
	DonVi donvi
	LEFT JOIN @DataDuToan dt ON donvi.iID_MaDonVi = dt.idDonVi
	LEFT JOIN @DataHachToan ht ON donvi.iID_MaDonVi = ht.idDonVi
	WHERE donvi.iTrangThai = 1
		   AND donvi.iNamLamViec = @NamLamViec
		   AND donvi.iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		    AND donvi.iKhoi=2


	-- data hach toan
	SELECT donvi.iID_MaDonVi idDonVi, 
	donvi.sTenDonVI, 
	IsNull(dt.BhxhNldDongDuToan, 0)/ @DVT BhxhNldDongDuToan, 
	IsNull(dt.BhxhNsddDongDuToan, 0)/ @DVT BhxhNsddDongDuToan, 
	IsNull(ht.BhxhNldDongHachToan, 0)/ @DVT BhxhNldDongHachToan, 
	IsNull(ht.BhxhNsddDongHachToan, 0)/ @DVT BhxhNsddDongHachToan,
	IsNull(dt.BHXHTongCongDuToan, 0)/ @DVT BHXHTongCongDuToan,
	IsNull(ht.BHXHTongCongHachToan, 0)/ @DVT BHXHTongCongHachToan,
	IsNull(dt.BhtnNldDongDuToan, 0)/ @DVT BhtnNldDongDuToan, 
	IsNull(dt.BhtnNsddDongDuToan, 0)/ @DVT BhtnNsddDongDuToan,
	IsNull(ht.BhtnNldDongHachToan, 0)/ @DVT BhtnNldDongHachToan, 
	IsNull(ht.BhtnNsddDongHachToan, 0)/ @DVT BhtnNsddDongHachToan,
	IsNull(dt.BHTNTongCongDuToan, 0)/ @DVT BHTNTongCongDuToan,
	IsNull(ht.BHTNTongCongHachToan, 0)/ @DVT BHTNTongCongHachToan
	into #tempDataHachToan1
	FROM 
	DonVi donvi
	LEFT JOIN @DataDuToan dt ON donvi.iID_MaDonVi = dt.idDonVi
	LEFT JOIN @DataHachToan ht ON donvi.iID_MaDonVi = ht.idDonVi
	WHERE donvi.iTrangThai = 1
		   AND donvi.iNamLamViec = @NamLamViec
		   AND donvi.iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		   AND donvi.iKhoi!=2

	-- Gop vao du toan
	SELECT SUM(B.BhxhNldDongDuToan) BhxhNldDongDuToan  ,
	SUM(B.BhxhNsddDongDuToan) BhxhNsddDongDuToan,
	SUM(B.BhxhNldDongHachToan) BhxhNldDongHachToan,
	SUM(B.BhxhNsddDongHachToan) BhxhNsddDongHachToan,
	SUM(B.BHXHTongCongDuToan) BHXHTongCongDuToan,
	SUM(B.BHXHTongCongHachToan) BHXHTongCongHachToan,
	SUM(B.BhtnNldDongDuToan)BhtnNldDongDuToan, 
	SUM(B.BhtnNsddDongDuToan) BhtnNsddDongDuToan,
	SUM(B.BhtnNldDongHachToan) BhtnNldDongHachToan,
	SUM(B.BhtnNsddDongHachToan) BhtnNsddDongHachToan,
	SUM(B.BHTNTongCongDuToan) BHTNTongCongDuToan, 
	SUM(B.BHTNTongCongHachToan) BHTNTongCongHachToan
	INTO #TempSumDataDuToan1
	FROM #tempDataDuToan1 B

	UPDATE A
	SET A.BhxhNldDongDuToan=(B.BhxhNldDongDuToan),
		A.BhxhNsddDongDuToan=(B.BhxhNsddDongDuToan),
		A.BhxhNldDongHachToan=(B.BhxhNldDongHachToan),
		A.BhxhNsddDongHachToan=(B.BhxhNsddDongHachToan),
		A.BHXHTongCongDuToan=(B.BHXHTongCongDuToan),
		A.BHXHTongCongHachToan=(B.BHXHTongCongHachToan),
		A.BhtnNldDongDuToan=(B.BhtnNldDongDuToan),
		A.BhtnNsddDongDuToan=(B.BhtnNsddDongDuToan),
		A.BhtnNldDongHachToan=(B.BhtnNldDongHachToan),
		A.BhtnNsddDongHachToan=(B.BhtnNsddDongHachToan),
		A.BHTNTongCongDuToan=(B.BHTNTongCongDuToan),
		A.BHTNTongCongHachToan=(B.BHTNTongCongHachToan)
	FROM #tempDuToan A, #TempSumDataDuToan1 B

	SELECT * INTO #TempResultDuToan1 FROM 
	( 
	SELECT * FROM #tempDuToan
	Union all
	SELECT * FROM #tempDataDuToan1
	) DuToan

	SELECT SUM(B.BhxhNldDongDuToan) BhxhNldDongDuToan  ,
	SUM(B.BhxhNsddDongDuToan) BhxhNsddDongDuToan,
	SUM(B.BhxhNldDongHachToan) BhxhNldDongHachToan,
	SUM(B.BhxhNsddDongHachToan) BhxhNsddDongHachToan,
	SUM(B.BHXHTongCongDuToan) BHXHTongCongDuToan,
	SUM(B.BHXHTongCongHachToan) BHXHTongCongHachToan,
	SUM(B.BhtnNldDongDuToan)BhtnNldDongDuToan, 
	SUM(B.BhtnNsddDongDuToan) BhtnNsddDongDuToan,
	SUM(B.BhtnNldDongHachToan) BhtnNldDongHachToan,
	SUM(B.BhtnNsddDongHachToan) BhtnNsddDongHachToan,
	SUM(B.BHTNTongCongDuToan) BHTNTongCongDuToan, 
	SUM(B.BHTNTongCongHachToan) BHTNTongCongHachToan
	INTO #TempSumDataHachToan1
	FROM #tempDataHachToan1 B

	UPDATE A
	SET A.BhxhNldDongDuToan=(B.BhxhNldDongDuToan),
		A.BhxhNsddDongDuToan=(B.BhxhNsddDongDuToan),
		A.BhxhNldDongHachToan=(B.BhxhNldDongHachToan),
		A.BhxhNsddDongHachToan=(B.BhxhNsddDongHachToan),
		A.BHXHTongCongDuToan=(B.BHXHTongCongDuToan),
		A.BHXHTongCongHachToan=(B.BHXHTongCongHachToan),
		A.BhtnNldDongDuToan=(B.BhtnNldDongDuToan),
		A.BhtnNsddDongDuToan=(B.BhtnNsddDongDuToan),
		A.BhtnNldDongHachToan=(B.BhtnNldDongHachToan),
		A.BhtnNsddDongHachToan=(B.BhtnNsddDongHachToan),
		A.BHTNTongCongDuToan=(B.BHTNTongCongDuToan),
		A.BHTNTongCongHachToan=(B.BHTNTongCongHachToan)
	FROM #tempHachToan A, #TempSumDataHachToan1 B

	-- Gop vao Hach Toan
	SELECT * INTO #TempResultHachToan1 FROM 
	(
	SELECT * FROM #tempHachToan
	Union all
	SELECT * FROM #tempDataHachToan1
	) HachToan
	-- Tra ra kết quả Result
	SELECT * FROM 
	( 
	SELECT * FROM #TempResultDuToan1
	UNION ALL
	SELECT * FROM #TempResultHachToan1 ) Result

	DROP TABLE #TempSumDataDuToan1
	DROP TABLE #TempSumDataHachToan1
	DROP TABLE #tempDataDuToan1
	DROP TABLE #tempDataHachToan1
	DROP TABLE #TempResultDuToan1
	DROP TABLE #TempResultHachToan1
	end

	DROP TABLE #tempDuToan
	DROP TABLE #tempHachToan
END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_kht_du_toan_thu_theokhoi_bhyt]    Script Date: 10/23/2024 2:54:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[sp_rpt_kht_du_toan_thu_theokhoi_bhyt] 
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


	SELECT '' idDonVi,N'A. Khối dự toán' sTenDonVI,0 BhytNldDongDuToan,0 BhytNsddDongDuToan,0 BhytNldDongHachToan,0 BhytNsddDongHachToan,0 BHYTTongCongDuToan,0 BHYTTongCongHachToan into #tempDuToan
	SELECT '' idDonVi,N'B. Khối hạch toán' sTenDonVI,0 BhytNldDongDuToan,0 BhytNsddDongDuToan,0 BhytNldDongHachToan,0 BhytNsddDongHachToan,0 BHYTTongCongDuToan,0 BHYTTongCongHachToan into #tempHachToan
	if (@IsMillionRound = 1)
	begin 
	SELECT donvi.iID_MaDonVi idDonVi, 
	donvi.sTenDonVI, 
	round(IsNull(dt.BhytNLDDongDuToan, 0) / 1000000, 0) * 1000000 /@DVT BhytNldDongDuToan, 
	round(IsNull(dt.BhytNSDDongDuToan, 0) / 1000000, 0) * 1000000 /@DVT BhytNsddDongDuToan, 
	round(IsNull(ht.BhytNLDDongHachToan, 0) / 1000000, 0) * 1000000 /@DVT BhytNldDongHachToan, 
	round(IsNull(ht.BhytNSDDongHachToan, 0) / 1000000, 0) * 1000000 /@DVT BhytNsddDongHachToan, 
	round(IsNull(dt.TongBhytDuToan, 0) / 1000000, 0) * 1000000 /@DVT BHYTTongCongDuToan, 
	round(IsNull(ht.TongBhytHachToan, 0) / 1000000, 0) * 1000000 /@DVT BHYTTongCongHachToan
	into #tempDataDuToan
	FROM
	DonVi donvi
	LEFT JOIN @BhytDuToan dt ON donvi.iID_MaDonVi = dt.idDonVi
	LEFT JOIN @BhytHachToan ht ON donvi.iID_MaDonVi = ht.idDonVi
	WHERE donvi.iTrangThai = 1
		   AND donvi.iNamLamViec = @NamLamViec
		   AND donvi.iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		   AND donvi.iKhoi=2

	SELECT donvi.iID_MaDonVi idDonVi, 
	donvi.sTenDonVI, 
	round(IsNull(dt.BhytNLDDongDuToan, 0) / 1000000, 0) * 1000000 /@DVT BhytNldDongDuToan, 
	round(IsNull(dt.BhytNSDDongDuToan, 0) / 1000000, 0) * 1000000 /@DVT BhytNsddDongDuToan, 
	round(IsNull(ht.BhytNLDDongHachToan, 0) / 1000000, 0) * 1000000 /@DVT BhytNldDongHachToan, 
	round(IsNull(ht.BhytNSDDongHachToan, 0) / 1000000, 0) * 1000000 /@DVT BhytNsddDongHachToan, 
	round(IsNull(dt.TongBhytDuToan, 0) / 1000000, 0) * 1000000 /@DVT BHYTTongCongDuToan, 
	round(IsNull(ht.TongBhytHachToan, 0) / 1000000, 0) * 1000000 /@DVT BHYTTongCongHachToan
	into #tempDataHachToan
	FROM
	DonVi donvi
	LEFT JOIN @BhytDuToan dt ON donvi.iID_MaDonVi = dt.idDonVi
	LEFT JOIN @BhytHachToan ht ON donvi.iID_MaDonVi = ht.idDonVi
	WHERE donvi.iTrangThai = 1
		   AND donvi.iNamLamViec = @NamLamViec
		   AND donvi.iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		   AND donvi.iKhoi!=2
	-- Sum Du toan
	SELECT 
	SUM(BhytNldDongDuToan) BhytNldDongDuToan,
	SUM(BhytNsddDongDuToan) BhytNsddDongDuToan,
	SUM(BhytNldDongHachToan) BhytNldDongHachToan,
	SUM(BhytNsddDongHachToan) BhytNsddDongHachToan,
	SUM(BHYTTongCongDuToan) BHYTTongCongDuToan,
	SUM(BHYTTongCongHachToan) BHYTTongCongHachToan
	INTO #tempSumDataDuToan
	FROM #tempDataDuToan

	Update A
	SET A.BhytNldDongDuToan=B.BhytNldDongDuToan,
		A.BhytNsddDongDuToan=B.BhytNsddDongDuToan,
		A.BhytNldDongHachToan=B.BhytNldDongHachToan,
		A.BhytNsddDongHachToan=B.BhytNsddDongHachToan,
		A.BHYTTongCongDuToan=B.BHYTTongCongDuToan,
		A.BHYTTongCongHachToan=B.BHYTTongCongHachToan
	FROM #tempDuToan A , #tempSumDataDuToan B

	-- Sum Hach Toan
	SELECT 
	SUM(BhytNldDongDuToan) BhytNldDongDuToan,
	SUM(BhytNsddDongDuToan) BhytNsddDongDuToan,
	SUM(BhytNldDongHachToan) BhytNldDongHachToan,
	SUM(BhytNsddDongHachToan) BhytNsddDongHachToan,
	SUM(BHYTTongCongDuToan) BHYTTongCongDuToan,
	SUM(BHYTTongCongHachToan) BHYTTongCongHachToan
	INTO #tempSumDataHachToan
	FROM #tempDataHachToan

		Update A
	SET A.BhytNldDongDuToan=B.BhytNldDongDuToan,
		A.BhytNsddDongDuToan=B.BhytNsddDongDuToan,
		A.BhytNldDongHachToan=B.BhytNldDongHachToan,
		A.BhytNsddDongHachToan=B.BhytNsddDongHachToan,
		A.BHYTTongCongDuToan=B.BHYTTongCongDuToan,
		A.BHYTTongCongHachToan=B.BHYTTongCongHachToan
	FROM #tempHachToan A , #tempSumDataHachToan B

	-- Gop Du toan
	SELECT * INTO #TempResultDuToan FROM 
	( 
	SELECT * FROM #tempDuToan
	Union all
	SELECT * FROM #tempDataDuToan
	) DuToan

	-- Gop vao Hach Toan
	SELECT * INTO #TempResultHachToan FROM 
	(
	SELECT * FROM #tempHachToan
	Union all
	SELECT * FROM #tempDataHachToan
	) HachToan

	-- Tra ra kết quả Result
	SELECT * FROM 
	( 
	SELECT * FROM #TempResultDuToan
	UNION ALL
	SELECT * FROM #TempResultHachToan ) Result

	DROP TABLE #tempDataDuToan
	DROP TABLE #tempDataHachToan
	DROP TABLE #tempSumDataDuToan
	DROP TABLE #tempSumDataHachToan
	DROP TABLE #TempResultDuToan
	DROP TABLE #TempResultHachToan
	end
	else
	begin 
	SELECT donvi.iID_MaDonVi idDonVi, 
	donvi.sTenDonVI, 
	IsNull(dt.BhytNLDDongDuToan, 0)/@DVT BhytNldDongDuToan, 
	IsNull(dt.BhytNSDDongDuToan, 0)/@DVT BhytNsddDongDuToan, 
	IsNull(ht.BhytNLDDongHachToan, 0)/@DVT BhytNldDongHachToan, 
	IsNull(ht.BhytNSDDongHachToan, 0)/@DVT BhytNsddDongHachToan,
	IsNull(dt.TongBhytDuToan, 0)/@DVT BHYTTongCongDuToan,
	IsNull(ht.TongBhytHachToan, 0)/@DVT BHYTTongCongHachToan
	INTO #tempDataDuToan1
	FROM
	DonVi donvi
	LEFT JOIN @BhytDuToan dt ON donvi.iID_MaDonVi = dt.idDonVi
	LEFT JOIN @BhytHachToan ht ON donvi.iID_MaDonVi = ht.idDonVi
	WHERE donvi.iTrangThai = 1
		   AND donvi.iNamLamViec = @NamLamViec
		   AND donvi.iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		   AND donvi.iKhoi=2
	

	SELECT donvi.iID_MaDonVi idDonVi, 
	donvi.sTenDonVI, 
	IsNull(dt.BhytNLDDongDuToan, 0)/@DVT BhytNldDongDuToan, 
	IsNull(dt.BhytNSDDongDuToan, 0)/@DVT BhytNsddDongDuToan, 
	IsNull(ht.BhytNLDDongHachToan, 0)/@DVT BhytNldDongHachToan, 
	IsNull(ht.BhytNSDDongHachToan, 0)/@DVT BhytNsddDongHachToan,
	IsNull(dt.TongBhytDuToan, 0)/@DVT BHYTTongCongDuToan,
	IsNull(ht.TongBhytHachToan, 0)/@DVT BHYTTongCongHachToan
	INTO #tempDataHachToan1
	FROM
	DonVi donvi
	LEFT JOIN @BhytDuToan dt ON donvi.iID_MaDonVi = dt.idDonVi
	LEFT JOIN @BhytHachToan ht ON donvi.iID_MaDonVi = ht.idDonVi
	WHERE donvi.iTrangThai = 1
		   AND donvi.iNamLamViec = @NamLamViec
		   AND donvi.iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		   AND donvi.iKhoi!=2
		-- Sum Du toan
	SELECT 
	SUM(BhytNldDongDuToan) BhytNldDongDuToan,
	SUM(BhytNsddDongDuToan) BhytNsddDongDuToan,
	SUM(BhytNldDongHachToan) BhytNldDongHachToan,
	SUM(BhytNsddDongHachToan) BhytNsddDongHachToan,
	SUM(BHYTTongCongDuToan) BHYTTongCongDuToan,
	SUM(BHYTTongCongHachToan) BHYTTongCongHachToan
	INTO #tempSumDataDuToan1
	FROM #tempDataDuToan1

	Update A
	SET A.BhytNldDongDuToan=B.BhytNldDongDuToan,
		A.BhytNsddDongDuToan=B.BhytNsddDongDuToan,
		A.BhytNldDongHachToan=B.BhytNldDongHachToan,
		A.BhytNsddDongHachToan=B.BhytNsddDongHachToan,
		A.BHYTTongCongDuToan=B.BHYTTongCongDuToan,
		A.BHYTTongCongHachToan=B.BHYTTongCongHachToan
	FROM #tempDuToan A , #tempSumDataDuToan1 B

	-- Sum Hach Toan
	SELECT 
	SUM(BhytNldDongDuToan) BhytNldDongDuToan,
	SUM(BhytNsddDongDuToan) BhytNsddDongDuToan,
	SUM(BhytNldDongHachToan) BhytNldDongHachToan,
	SUM(BhytNsddDongHachToan) BhytNsddDongHachToan,
	SUM(BHYTTongCongDuToan) BHYTTongCongDuToan,
	SUM(BHYTTongCongHachToan) BHYTTongCongHachToan
	INTO #tempSumDataHachToan1
	FROM #tempDataHachToan1

		Update A
	SET A.BhytNldDongDuToan=B.BhytNldDongDuToan,
		A.BhytNsddDongDuToan=B.BhytNsddDongDuToan,
		A.BhytNldDongHachToan=B.BhytNldDongHachToan,
		A.BhytNsddDongHachToan=B.BhytNsddDongHachToan,
		A.BHYTTongCongDuToan=B.BHYTTongCongDuToan,
		A.BHYTTongCongHachToan=B.BHYTTongCongHachToan
	FROM #tempHachToan A , #tempSumDataHachToan1 B

	-- Gop Du toan
	SELECT * INTO #TempResultDuToan1 FROM 
	( 
	SELECT * FROM #tempDuToan
	Union all
	SELECT * FROM #tempDataDuToan1
	) DuToan

	-- Gop vao Hach Toan
	SELECT * INTO #TempResultHachToan1 FROM 
	(
	SELECT * FROM #tempHachToan
	Union all
	SELECT * FROM #tempDataHachToan1
	) HachToan

	-- Tra ra kết quả Result
	SELECT * FROM 
	( 
	SELECT * FROM #TempResultDuToan1
	UNION ALL
	SELECT * FROM #TempResultHachToan1 ) Result

	DROP TABLE #tempDataDuToan1
	DROP TABLE #tempDataHachToan1
	DROP TABLE #tempSumDataDuToan1
	DROP TABLE #tempSumDataHachToan1
	DROP TABLE #TempResultDuToan1
	DROP TABLE #TempResultHachToan1
	end
	DROP TABLE #tempDuToan
	DROP TABLE #tempHachToan
END
;
;
;
;
;
;
GO
