/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_rpt_quyet_toan_thu_mua_bhyt_than_nhan_nld]    Script Date: 9/29/2023 3:10:53 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qttm_rpt_quyet_toan_thu_mua_bhyt_than_nhan_nld]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qttm_rpt_quyet_toan_thu_mua_bhyt_than_nhan_nld]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_rpt_quyet_toan_thu_mua_bhyt_than_nhan]    Script Date: 9/29/2023 3:10:53 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qttm_rpt_quyet_toan_thu_mua_bhyt_than_nhan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qttm_rpt_quyet_toan_thu_mua_bhyt_than_nhan]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_rpt_quyet_toan_thu_mua_bhyt_hssv]    Script Date: 9/29/2023 3:10:53 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qttm_rpt_quyet_toan_thu_mua_bhyt_hssv]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qttm_rpt_quyet_toan_thu_mua_bhyt_hssv]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_bhyt]    Script Date: 9/29/2023 3:10:53 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_bhyt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_bhyt]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_bhxh_bhtn]    Script Date: 9/29/2023 3:10:53 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_bhxh_bhtn]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_bhxh_bhtn]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_bhxh_bhtn]    Script Date: 9/29/2023 3:10:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_bhxh_bhtn]
	@NamLamViec int,
	@LstSelectedUnit ntext,
	@KhoiDuToan nvarchar(50),
	@KhoiHachToan nvarchar(50),
	@Dvt int,
	@IsTongHop bit
AS
BEGIN
	declare @DataDuToanQTT table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), BhxhNldDongDuToan float, BhxhNsddDongDuToan float, BHXHTongCongDuToan float, BhtnNldDongDuToan float, BhtnNsddDongDuToan float, BHTNTongCongDuToan float);
	declare @DataHachToanQTT table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), BhxhNldDongHachToan float, BhxhNsddDongHachToan float, BHXHTongCongHachToan float, BhtnNldDongHachToan float, BhtnNsddDongHachToan float, BHTNTongCongHachToan float);

	INSERT INTO @DataDuToanQTT (sTenDonVI, idDonVi, BhxhNldDongDuToan, BhxhNsddDongDuToan, BHXHTongCongDuToan, BhtnNldDongDuToan, BhtnNsddDongDuToan, BHTNTongCongDuToan)
		SELECT 
		   dt_dv.sTenDonVi,
		   dt_dv.id idDonVi,
		   SUM(IsNull(A.ThuBHXHNLDDong,0)),
		   SUM(IsNull(A.ThuBHXHNSDDong,0)),
		   SUM(IsNull(A.ThuBHXHNLDDong,0)) + SUM(IsNull(A.ThuBHXHNSDDong,0)),
		   SUM(IsNull(A.ThuBHTNNLDDong,0)),
		   SUM(IsNull(A.ThuBHTNNSDDong,0)),
		   SUM(IsNull(A.ThuBHTNNLDDong,0)) + SUM(IsNull(A.ThuBHTNNSDDong,0))
		FROM
		  (SELECT ml.iID_MLNS ,
				ml.iID_MLNS_Cha,
				ml.sNG,
				ml.sMoTa,
				ml.bHangCha,
				ml.sXauNoiMa,
				ct.iID_MaDonVi,
				IsNull(ctct.fThu_BHXH_NLD, 0) ThuBHXHNLDDong,
				IsNull(ctct.fThu_BHXH_NSD, 0) ThuBHXHNSDDong,
				IsNull(ctct.fThu_BHTN_NLD, 0) ThuBHTNNLDDong,
				IsNull(ctct.fThu_BHTN_NSD, 0) ThuBHTNNSDDong
		   FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
		   LEFT JOIN BH_QTT_BHXH_ChungTu ct ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
		   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
		   AND ml.iNamLamViec = @NamLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS = @KhoiDuToan
		   WHERE ct.iNamLamViec = @NamLamViec
				AND ct.iLoaiTongHop = case when @IsTongHop = 1 then 2 else 1 end
				AND ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end) AS A 
		   JOIN
		  (SELECT iID_MaDonVi AS id,
				  sTenDonVi, iLoai
		   FROM DonVi
		   WHERE iTrangThai = 1
		   AND iNamLamViec = @NamLamViec
		   AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   
		GROUP BY dt_dv.sTenDonVi,
				dt_dv.id;

	INSERT INTO @DataHachToanQTT (sTenDonVI, idDonVi, BhxhNldDongHachToan, BhxhNsddDongHachToan, BHXHTongCongHachToan, BhtnNldDongHachToan, BhtnNsddDongHachToan, BHTNTongCongHachToan)
		SELECT 
		   dt_dv.sTenDonVi,
		   dt_dv.id idDonVi,
		   SUM(IsNull(A.ThuBHXHNLDDong,0)),
		   SUM(IsNull(A.ThuBHXHNSDDong,0)),
		   SUM(IsNull(A.ThuBHXHNLDDong,0)) + SUM(IsNull(A.ThuBHXHNSDDong,0)),
		   SUM(IsNull(A.ThuBHTNNLDDong,0)),
		   SUM(IsNull(A.ThuBHTNNSDDong,0)),
		   SUM(IsNull(A.ThuBHTNNLDDong,0)) + SUM(IsNull(A.ThuBHTNNSDDong,0))

		FROM
		  (SELECT ml.iID_MLNS ,
						ml.iID_MLNS_Cha,
						ml.sNG,
						ml.sMoTa,
						ml.bHangCha,
						ml.sXauNoiMa,
						ct.iID_MaDonVi,
						IsNull(ctct.fThu_BHXH_NLD, 0) ThuBHXHNLDDong,
						IsNull(ctct.fThu_BHXH_NSD, 0) ThuBHXHNSDDong,
						IsNull(ctct.fThu_BHTN_NLD, 0) ThuBHTNNLDDong,
						IsNull(ctct.fThu_BHTN_NSD, 0) ThuBHTNNSDDong
		   FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
		   LEFT JOIN BH_QTT_BHXH_ChungTu ct ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
		   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
		   AND ml.iNamLamViec = @NamLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS = @KhoiHachToan
		   WHERE ct.iNamLamViec = @NamLamViec
				AND ct.iLoaiTongHop = case when @IsTongHop = 1 then 2 else 1 end
				AND ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end) AS A 
		   JOIN
		  (SELECT iID_MaDonVi AS id,
				  sTenDonVi, iLoai
		   FROM DonVi
		   WHERE iTrangThai = 1
		   AND iNamLamViec = @NamLamViec
		   AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   
		GROUP BY dt_dv.sTenDonVi,
				dt_dv.id;

	SELECT dt.idDonVi, 
	dt.sTenDonVI, 
	IsNull(dt.BhxhNldDongDuToan, 0)/@Dvt fBhxhNldDongDuToan, 
	IsNull(dt.BhxhNsddDongDuToan, 0)/@Dvt fBhxhNsddDongDuToan, 
	IsNull(ht.BhxhNldDongHachToan, 0)/@Dvt fBhxhNldDongHachToan, 
	IsNull(ht.BhxhNsddDongHachToan, 0)/@Dvt fBhxhNsddDongHachToan,
	IsNull(dt.BHXHTongCongDuToan, 0)/@Dvt fBHXHTongCongDuToan,
	IsNull(ht.BHXHTongCongHachToan, 0)/@Dvt fBHXHTongCongHachToan,
	IsNull(dt.BhtnNldDongDuToan, 0)/@Dvt fBhtnNldDongDuToan, 
	IsNull(dt.BhtnNsddDongDuToan, 0)/@Dvt fBhtnNsddDongDuToan,
	IsNull(ht.BhtnNldDongHachToan, 0)/@Dvt fBhtnNldDongHachToan, 
	IsNull(ht.BhtnNsddDongHachToan, 0)/@Dvt fBhtnNsddDongHachToan,
	IsNull(dt.BHTNTongCongDuToan, 0)/@Dvt fBHTNTongCongDuToan,
	IsNull(ht.BHTNTongCongHachToan, 0)/@Dvt fBHTNTongCongHachToan
	FROM @DataDuToanQTT dt
	LEFT JOIN @DataHachToanQTT ht ON dt.idDonVi = ht.idDonVi;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_bhyt]    Script Date: 9/29/2023 3:10:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_bhyt] 
	@NamLamViec int,
	@LstSelectedUnit ntext,
	@KhoiDuToan nvarchar(50),
	@KhoiHachToan nvarchar(50),
	@Dvt int,
	@IsTongHop bit,
	@SM nvarchar(50)
AS
BEGIN
	declare @BhytDuToanQTT table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), sm nvarchar(50),BhytNSDDongDuToan float, BhytNLDDongDuToan float, TongBhytDuToan float);
	declare @BhytHachToanQTT table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), sm nvarchar(50), BhytNSDDongHachToan float,BhytNLDDongHachToan float, TongBhytHachToan float);

	INSERT INTO @BhytDuToanQTT (sTenDonVI, idDonVi, sm, BhytNSDDongDuToan, BhytNLDDongDuToan, TongBhytDuToan)
		SELECT 
		   dt_dv.sTenDonVi,
		   dt_dv.id idDonVi,
		   A.sm,
		   SUM(IsNull(A.ThuBHYTNSDDongDuToan, 0)),
		   SUM(IsNull(A.ThuBHYTNLDDongDuToan, 0)),
		   SUM(IsNull(A.ThuBHYTNLDDongDuToan, 0)) +  SUM(IsNull(A.ThuBHYTNSDDongDuToan, 0))
		FROM
		  (SELECT ml.sm,
				   ml.sMoTa,
				   ct.iID_MaDonVi,
				   IsNull(ctct.fThu_BHYT_NSD, 0) ThuBHYTNSDDongDuToan,
				   IsNull(ctct.fThu_BHYT_NLD, 0) ThuBHYTNLDDongDuToan
		   FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
		   LEFT JOIN BH_QTT_BHXH_ChungTu ct ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
		   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
		   AND ml.iNamLamViec = @namLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS = @khoiDuToan
		   AND ml.sM = @sm
		   WHERE ct.iNamLamViec = @namLamViec
				AND ct.iLoaiTongHop = case when @IsTongHop = 1 then 2 else 1 end
				AND ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end) AS A 
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

	INSERT INTO @BhytHachToanQTT (idDonVi, sTenDonVI, sm, BhytNSDDongHachToan, BhytNLDDongHachToan, TongBhytHachToan)
		SELECT
			dt_dv.id idDonVi,
		   dt_dv.sTenDonVi,
		   A.sm,
		   SUM(IsNull(A.ThuBHYTNSDDongHachToan, 0)),
		   SUM(IsNull(A.ThuBHYTNLDDongHachToan, 0)),
		   SUM(IsNull(A.ThuBHYTNLDDongHachToan, 0)) + SUM(IsNull(A.ThuBHYTNSDDongHachToan, 0))

		FROM
		  (SELECT ml.sm,
				   ml.sMoTa,
				   ct.iID_MaDonVi,
				   IsNull(ctct.fThu_BHYT_NSD, 0) ThuBHYTNSDDongHachToan,
				   IsNull(ctct.fThu_BHYT_NLD, 0) ThuBHYTNLDDongHachToan
		   FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
		   LEFT JOIN BH_QTT_BHXH_ChungTu ct ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
		   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
		   AND ml.iNamLamViec = @namLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS = @khoiHachToan
		   AND ml.sM = @sm
		   WHERE ct.iNamLamViec = @namLamViec
				AND ct.iLoaiTongHop = case when @IsTongHop = 1 then 2 else 1 end
				AND ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end) AS A 
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

	SELECT dt.idDonVi, 
	dt.sTenDonVI,
	IsNull(dt.BhytNLDDongDuToan, 0)/@dvt FBhytNldDongDuToan, 
	IsNull(dt.BhytNSDDongDuToan, 0)/@dvt FBhytNsddDongDuToan, 
	IsNull(ht.BhytNLDDongHachToan, 0)/@dvt FBhytNldDongHachToan, 
	IsNull(ht.BhytNSDDongHachToan, 0)/@dvt FBhytNsddDongHachToan,
	IsNull(dt.TongBhytDuToan, 0)/@dvt FBHYTTongCongDuToan,
	IsNull(ht.TongBhytHachToan, 0)/@dvt FBHYTTongCongHachToan
	FROM @BhytDuToanQTT dt
	LEFT JOIN @BhytHachToanQTT ht ON dt.idDonVi = ht.idDonVi;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_rpt_quyet_toan_thu_mua_bhyt_hssv]    Script Date: 9/29/2023 3:10:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qttm_rpt_quyet_toan_thu_mua_bhyt_hssv]
	@NamLamViec int,
	@IsTongHop bit,
	@LstSelectedUnit ntext,
	@HSSV nvarchar(50),
	@LuuHS nvarchar(50),
	@HVSQ nvarchar(50),
	@SQDuBi nvarchar(50),
	@Dvt int
AS
BEGIN
	declare @Tbl_HSSV_QTTM table (IdDonVi nvarchar(50),TenDonVI nvarchar(200), ThanhTien_HSSV float);
	declare @Tbl_LuuHS_QTTM table (IdDonVi nvarchar(50),TenDonVI nvarchar(200), ThanhTien_LuuHS float);
	declare @Tbl_HVQS_QTTM table (IdDonVi nvarchar(50),TenDonVI nvarchar(200), ThanhTien_HVQS float);
	declare @Tbl_SQDuBi_QTTM table (IdDonVi nvarchar(50),TenDonVI nvarchar(200), ThanhTien_SQDuBi float);

	INSERT INTO @Tbl_HSSV_QTTM (IdDonVi, TenDonVI, ThanhTien_HSSV)
	SELECT 
		dt_dv.id,
		dt_dv.sTenDonVi,
	   SUM(IsNull(A.ThanhTien, 0)) ThanhTien
		FROM
		  (SELECT
				   ml.sMoTa,
				   ct.iID_MaDonVi,
				   IsNull(ctct.fSoPhaiThu, 0) ThanhTien,
				   ml.sLNS
		   FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
		   LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
		   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
		   AND ml.iNamLamViec = @namLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS = @hSSV
		   WHERE ct.iNamLamViec = @namLamViec
					AND ct.iQuyNam = @namLamViec
					AND ct.iLoaiTongHop = case when @IsTongHop = 1 then 2 else 1 end
					AND ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end) AS A 
		   JOIN
		  (SELECT iID_MaDonVi AS id,
				  sTenDonVi, iLoai
		   FROM DonVi
		   WHERE iTrangThai = 1
		   AND iNamLamViec = @namLamViec
		   AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi = dt_dv.id
		GROUP BY
		dt_dv.sTenDonVi,
		dt_dv.id;

	INSERT INTO @Tbl_LuuHS_QTTM (IdDonVi, TenDonVI, ThanhTien_LuuHS)
	SELECT 
	   dt_dv.id,
		dt_dv.sTenDonVi,
	   SUM(IsNull(A.ThanhTien, 0)) ThanhTien
		FROM
		  (SELECT
				   ml.sMoTa,
				   ct.iID_MaDonVi,
				   IsNull(ctct.fSoPhaiThu, 0) ThanhTien,
				   ml.sLNS
		   FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
		   LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
		   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
		   AND ml.iNamLamViec = @namLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS = @luuHS
		   WHERE ct.iNamLamViec = @namLamViec
					AND ct.iQuyNam = @namLamViec
					AND ct.iLoaiTongHop = case when @IsTongHop = 1 then 2 else 1 end
					AND ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end) AS A 
		   JOIN
		  (SELECT iID_MaDonVi AS id,
				  sTenDonVi, iLoai
		   FROM DonVi
		   WHERE iTrangThai = 1
		   AND iNamLamViec = @namLamViec
		   AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi = dt_dv.id
		GROUP BY
		dt_dv.sTenDonVi,
		dt_dv.id;

	INSERT INTO @Tbl_HVQS_QTTM (IdDonVi, TenDonVI, ThanhTien_HVQS)
	SELECT 
	   dt_dv.id,
		dt_dv.sTenDonVi,
	   SUM(IsNull(A.ThanhTien, 0)) ThanhTien
		FROM
		  (SELECT
				   ml.sMoTa,
				   ct.iID_MaDonVi,
				   IsNull(ctct.fSoPhaiThu, 0) ThanhTien,
				   ml.sLNS
		   FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
		   LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
		   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
		   AND ml.iNamLamViec = @namLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS = @hVSQ
		   WHERE ct.iNamLamViec = @namLamViec
					AND ct.iQuyNam = @namLamViec
					AND ct.iLoaiTongHop = case when @IsTongHop = 1 then 2 else 1 end
					AND ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end) AS A 
		   JOIN
		  (SELECT iID_MaDonVi AS id,
				  sTenDonVi, iLoai
		   FROM DonVi
		   WHERE iTrangThai = 1
		   AND iNamLamViec = @namLamViec
		   AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi = dt_dv.id
		GROUP BY
		dt_dv.sTenDonVi,
		dt_dv.id;

	INSERT INTO @Tbl_SQDuBi_QTTM (IdDonVi, TenDonVI, ThanhTien_SQDuBi)
	SELECT 
	   dt_dv.id,
		dt_dv.sTenDonVi,
	   SUM(IsNull(A.ThanhTien, 0)) ThanhTien
		FROM
		  (SELECT
				   ml.sMoTa,
				   ct.iID_MaDonVi,
				   IsNull(ctct.fSoPhaiThu, 0) ThanhTien,
				   ml.sLNS
		   FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
		   LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
		   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
		   AND ml.iNamLamViec = @namLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS = @sQDuBi
		   WHERE ct.iNamLamViec = @namLamViec
					AND ct.iQuyNam = @namLamViec
					AND ct.iLoaiTongHop = case when @IsTongHop = 1 then 2 else 1 end
					AND ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end) AS A 
		   JOIN
		  (SELECT iID_MaDonVi AS id,
				  sTenDonVi, iLoai
		   FROM DonVi
		   WHERE iTrangThai = 1
		   AND iNamLamViec = @namLamViec
		   AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi = dt_dv.id
		GROUP BY
		dt_dv.sTenDonVi,
		dt_dv.id;

	SELECT result.idDonVi, 
		result.TenDonVI STenDonVi, 
		result.HSSV/@dvt fHSSV, 
		result.LuuHS/@dvt fLuuHS,
		result.TongHSSV/@dvt fTongHSSV,
		result.HVQS/@dvt fHVQS,
		result.SQDuBi/@dvt fSQDuBi,
		(result.TongHSSV + result.HVQS + result.SQDuBi)/@dvt fTongCongHSSV
		FROM
		(SELECT hssv.idDonVi, 
		hssv.TenDonVI,
		IsNull(hssv.ThanhTien_HSSV, 0) HSSV,
		IsNull(luuhs.ThanhTien_LuuHS, 0) LuuHS,
		(IsNull(hssv.ThanhTien_HSSV + luuhs.ThanhTien_LuuHS, 0)) TongHSSV,
		IsNull(hvsq.ThanhTien_HVQS, 0) HVQS,
		IsNull(sqdb.ThanhTien_SQDuBi, 0) SQDuBi
		FROM @Tbl_HSSV_QTTM hssv
		LEFT JOIN @Tbl_LuuHS_QTTM luuhs ON hssv.idDonVi = luuhs.idDonVi
		LEFT JOIN @Tbl_HVQS_QTTM hvsq ON hssv.idDonVi = hvsq.idDonVi
		LEFT JOIN @Tbl_SQDuBi_QTTM sqdb ON hssv.idDonVi = sqdb.idDonVi) result
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_rpt_quyet_toan_thu_mua_bhyt_than_nhan]    Script Date: 9/29/2023 3:10:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qttm_rpt_quyet_toan_thu_mua_bhyt_than_nhan]
	@NamLamViec int,
	@IsTongHop bit,
	@LstSelectedUnit ntext,
	@ThanNhanQuanNhan nvarchar(50),
	@ThanNhanCNVQP nvarchar(50),
	@Dvt int
AS
BEGIN
	declare @TNQN_TMP table (IdDonVi nvarchar(50),TenDonVI nvarchar(200), fSoPhaiThu float);
	declare @TN_CNVQP_TMP table (IdDonVi nvarchar(50),TenDonVI nvarchar(200), fSoPhaiThu float);

	-- Lấy dữ liệu số phải thu thân nhân quân nhân theo đơn vị
	INSERT INTO @TNQN_TMP (IdDonVi, TenDonVI, fSoPhaiThu)
		SELECT
			dt_dv.id,
			dt_dv.sTenDonVi,
		   SUM(IsNull(A.fSoPhaiThu, 0)) ThanhTien
			FROM
			  (SELECT
					   ml.sMoTa,
					   ct.iID_MaDonVi,
					   IsNull(ctct.fSoPhaiThu, 0) fSoPhaiThu,
					   ml.sLNS
			   FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
			   LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
			   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
			   AND ml.iNamLamViec = @NamLamViec
			   AND ml.iTrangThai = 1
			   AND ml.sLNS = @ThanNhanQuanNhan
			   WHERE ct.iNamLamViec = @NamLamViec
					AND ct.iQuyNam = @NamLamViec
					AND ct.iLoaiTongHop = case when @IsTongHop = 1 then 2 else 1 end
					AND ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end) AS A 
			   JOIN
			  (SELECT iID_MaDonVi AS id,
					  sTenDonVi, iLoai
			   FROM DonVi
			   WHERE iTrangThai = 1
			   AND iNamLamViec = @NamLamViec
			   AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi = dt_dv.id
			GROUP BY
			dt_dv.sTenDonVi,
			dt_dv.id;

		-- Lấy dữ liệu số phải thu thân nhân CNVCQP theo đơn vị
		INSERT INTO @TN_CNVQP_TMP (IdDonVi, TenDonVI, fSoPhaiThu)
			SELECT
				dt_dv.id,
				dt_dv.sTenDonVi,
			   SUM(IsNull(A.fSoPhaiThu, 0)) ThanhTien
				FROM
				  (SELECT
						   ml.sMoTa,
						   ct.iID_MaDonVi,
						   IsNull(ctct.fSoPhaiThu, 0) fSoPhaiThu,
						   ml.sLNS
				   FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
				   LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
				   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
				   AND ml.iNamLamViec = @NamLamViec
				   AND ml.iTrangThai = 1
				   AND ml.sLNS = @ThanNhanCNVQP
				   WHERE ct.iNamLamViec = @NamLamViec
						AND ct.iQuyNam = @NamLamViec
						AND ct.iLoaiTongHop = case when @IsTongHop = 1 then 2 else 1 end
						AND ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end) AS A 
				   JOIN
				  (SELECT iID_MaDonVi AS id,
						  sTenDonVi, iLoai
				   FROM DonVi
				   WHERE iTrangThai = 1
				   AND iNamLamViec = @NamLamViec
				   AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi = dt_dv.id
				GROUP BY
				dt_dv.sTenDonVi,
				dt_dv.id;

		-- Kết quả
		SELECT result.idDonVi, 
			result.TenDonVI sTenDonVi, 
			result.fSoPhaiThuTNQN/@dvt fSoPhaiThuTNQN, 
			result.fSoPhaiThuTNCNVQP/@dvt fSoPhaiThuTNCNVQP,
			result.fTongCong/@dvt fTongCong
		FROM
			(SELECT tnqn.idDonVi, 
				tnqn.TenDonVI,
				IsNull(tnqn.fSoPhaiThu, 0) fSoPhaiThuTNQN,
				IsNull(tncnvqp.fSoPhaiThu, 0) fSoPhaiThuTNCNVQP,
				IsNull(tnqn.fSoPhaiThu, 0) + IsNull(tncnvqp.fSoPhaiThu, 0) fTongCong
				FROM @TNQN_TMP tnqn
				LEFT JOIN @TN_CNVQP_TMP tncnvqp ON tnqn.idDonVi = tncnvqp.idDonVi) result
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_rpt_quyet_toan_thu_mua_bhyt_than_nhan_nld]    Script Date: 9/29/2023 3:10:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qttm_rpt_quyet_toan_thu_mua_bhyt_than_nhan_nld]
	@NamLamViec int,
	@IdDonVi nvarchar(50),
	@Donvitinh int,
	@IsTongHop bit,
	@IQuy int
AS
BEGIN
	select
		chungtudonvi.iID_QTTM_BHYT_ChungTu_ChiTiet,
		mlns.iID_MLNS,
		mlns.sMoTa,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		mlns.sLNS,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		mlns.sTTM,
		mlns.sNG,
		mlns.sTNG,
		mlns.sXauNoiMa,
		mlns.sMoTa,
		@NamLamViec iNamLamViec,
		chungtudonvi.fDuToan/@Donvitinh fDuToan,
		chungtudonvi.fDaQuyetToan/@Donvitinh fDaQuyetToan,
		chungtudonvi.fConLai/@Donvitinh fConLai,
		chungtudonvi.fSoPhaiThu/@Donvitinh fSoPhaiThu
		from
		(select
			BH_DM_MucLucNganSach.iID_MLNS,
			BH_DM_MucLucNganSach.iID_MLNS_Cha,
			BH_DM_MucLucNganSach.bHangCha,
			BH_DM_MucLucNganSach.sLNS,
			BH_DM_MucLucNganSach.sL,
			BH_DM_MucLucNganSach.sK,
			BH_DM_MucLucNganSach.sM,
			BH_DM_MucLucNganSach.sTM,
			BH_DM_MucLucNganSach.sTTM,
			BH_DM_MucLucNganSach.sNG,
			BH_DM_MucLucNganSach.sTNG,
			BH_DM_MucLucNganSach.sXauNoiMa,
			BH_DM_MucLucNganSach.sMoTa
		from BH_DM_MucLucNganSach 
		where sLNS like '903%'
		and iNamLamViec = @NamLamViec) mlns
		left join
		(select
			ct.iID_MaDonVi,
			ct.iNamLamViec,
			ctct.*
			from
			BH_QTTM_BHYT_Chung_Tu ct
			join
			BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct on ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
			where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam = @IQuy
			and ct.iID_MaDonVi = @IdDonVi
			and ct.iLoaiTongHop = case when @IsTongHop = 1 then 2 else 1 end
			and ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end
				) chungtudonvi 
			on mlns.iID_MLNS = chungtudonvi.iID_MLNS
		
		order by mlns.sXauNoiMa
END
GO
