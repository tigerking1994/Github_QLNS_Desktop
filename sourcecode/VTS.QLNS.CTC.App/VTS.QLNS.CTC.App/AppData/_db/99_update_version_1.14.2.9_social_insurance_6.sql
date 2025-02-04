/****** Object:  StoredProcedure [dbo].[sp_rpt_khtm_du_toan_thu_bhyt_than_nhan]    Script Date: 4/5/2024 4:54:54 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_khtm_du_toan_thu_bhyt_than_nhan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_khtm_du_toan_thu_bhyt_than_nhan]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khtm_du_toan_thu_bhyt_hssv_hvqs]    Script Date: 4/5/2024 4:54:54 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_khtm_du_toan_thu_bhyt_hssv_hvqs]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_khtm_du_toan_thu_bhyt_hssv_hvqs]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khtm_du_toan_thu_bhyt_hssv_hvqs]    Script Date: 4/5/2024 4:54:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_rpt_khtm_du_toan_thu_bhyt_hssv_hvqs]
	@namLamViec int,
	@lstSelectedUnit ntext,
	@hSSV nvarchar(50),
	@luuHS nvarchar(50),
	@hVSQ nvarchar(50),
	@sQDuBi nvarchar(50),
	@soQuyetDinh nvarchar(500),
	@ngayQuyetDinh nvarchar(500),
	@dvt int
AS
BEGIN
	declare @tbl_HSSV table (IdDonVi nvarchar(50),TenDonVI nvarchar(200), ThanhTien_HSSV float);
	declare @tbl_LuuHS table (IdDonVi nvarchar(50),TenDonVI nvarchar(200), ThanhTien_LuuHS float);
	declare @tbl_HVQS table (IdDonVi nvarchar(50),TenDonVI nvarchar(200), ThanhTien_HVQS float);
	declare @tbl_SQDuBi table (IdDonVi nvarchar(50),TenDonVI nvarchar(200), ThanhTien_SQDuBi float);

	INSERT INTO @tbl_HSSV (IdDonVi, TenDonVI, ThanhTien_HSSV)
	SELECT 
		dt_dv.id,
		dt_dv.sTenDonVi,
	   SUM(IsNull(A.ThanhTien, 0)) ThanhTien
		FROM
		  (SELECT
				   ml.sMoTa,
				   ctct.iID_MaDonVi,
				   IsNull(ctct.fDuToan, 0) ThanhTien,
				   ml.sLNS
		   FROM BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct
		   JOIN BH_DTTM_BHYT_ThanNhan_PhanBo ct ON ct.iID_DTTM_BHYT_ThanNhan_PhanBo = ctct.iID_DTTM_BHYT_ThanNhan_PhanBo
		   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
		   AND ml.iNamLamViec = @namLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS = @hSSV
		   WHERE ct.iNamLamViec = @namLamViec
			AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@soQuyetDinh))
			AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@ngayQuyetDinh))
		   ) AS A 
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

	INSERT INTO @tbl_LuuHS (IdDonVi, TenDonVI, ThanhTien_LuuHS)
	SELECT 
	   dt_dv.id,
		dt_dv.sTenDonVi,
	   SUM(IsNull(A.ThanhTien, 0)) ThanhTien
		FROM
		  (SELECT
				   ml.sMoTa,
				   ctct.iID_MaDonVi,
				   IsNull(ctct.fDuToan, 0) ThanhTien,
				   ml.sLNS
		   FROM BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct
		   JOIN BH_DTTM_BHYT_ThanNhan_PhanBo ct ON ct.iID_DTTM_BHYT_ThanNhan_PhanBo = ctct.iID_DTTM_BHYT_ThanNhan_PhanBo
		   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
		   AND ml.iNamLamViec = @namLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS = @luuHS
		   WHERE ct.iNamLamViec = @namLamViec
			AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@soQuyetDinh))
			AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@ngayQuyetDinh))
		   ) AS A 
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

	INSERT INTO @tbl_HVQS (IdDonVi, TenDonVI, ThanhTien_HVQS)
	SELECT 
	   dt_dv.id,
		dt_dv.sTenDonVi,
	   SUM(IsNull(A.ThanhTien, 0)) ThanhTien
		FROM
		  (SELECT
				   ml.sMoTa,
				   ctct.iID_MaDonVi,
				   IsNull(ctct.fDuToan, 0) ThanhTien,
				   ml.sLNS
		   FROM BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct
		   JOIN BH_DTTM_BHYT_ThanNhan_PhanBo ct ON ct.iID_DTTM_BHYT_ThanNhan_PhanBo = ctct.iID_DTTM_BHYT_ThanNhan_PhanBo
		   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
		   AND ml.iNamLamViec = @namLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS = @hVSQ
		   WHERE ct.iNamLamViec = @namLamViec
			AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@soQuyetDinh))
			AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@ngayQuyetDinh))
		   ) AS A 
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

	INSERT INTO @tbl_SQDuBi (IdDonVi, TenDonVI, ThanhTien_SQDuBi)
	SELECT 
	   dt_dv.id,
		dt_dv.sTenDonVi,
	   SUM(IsNull(A.ThanhTien, 0)) ThanhTien
		FROM
		  (SELECT
				   ml.sMoTa,
				   ctct.iID_MaDonVi,
				   IsNull(ctct.fDuToan, 0) ThanhTien,
				   ml.sLNS
		   FROM BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct
		   LEFT JOIN BH_DTTM_BHYT_ThanNhan_PhanBo ct ON ct.iID_DTTM_BHYT_ThanNhan_PhanBo = ctct.iID_DTTM_BHYT_ThanNhan_PhanBo
		   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
		   AND ml.iNamLamViec = @namLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS = @sQDuBi
		   WHERE ct.iNamLamViec = @namLamViec
			AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@soQuyetDinh))
			AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@ngayQuyetDinh))
		   ) AS A 
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
		result.STenDonVi, 
		result.HSSV/@dvt HSSV, 
		result.LuuHS/@dvt LuuHS,
		result.TongHSSV/@dvt TongHSSV,
		result.HVQS/@dvt HVQS,
		result.SQDuBi/@dvt SQDuBi,
		(result.TongHSSV + result.HVQS + result.SQDuBi)/@dvt TongCongHSSV
		FROM
		(SELECT donvi.iID_MaDonVi idDonVi, 
		donvi.STenDonVi,
		IsNull(hssv.ThanhTien_HSSV, 0) HSSV,
		IsNull(luuhs.ThanhTien_LuuHS, 0) LuuHS,
		(IsNull(hssv.ThanhTien_HSSV + luuhs.ThanhTien_LuuHS, 0)) TongHSSV,
		IsNull(hvsq.ThanhTien_HVQS, 0) HVQS,
		IsNull(sqdb.ThanhTien_SQDuBi, 0) SQDuBi
		FROM DonVi donvi
		LEFT JOIN @tbl_HSSV hssv ON donvi.iID_MaDonVi = hssv.idDonVi
		LEFT JOIN @tbl_LuuHS luuhs ON donvi.iID_MaDonVi = luuhs.idDonVi
		LEFT JOIN @tbl_HVQS hvsq ON donvi.iID_MaDonVi = hvsq.idDonVi
		LEFT JOIN @tbl_SQDuBi sqdb ON donvi.iID_MaDonVi = sqdb.idDonVi
		WHERE donvi.iTrangThai = 1
		   AND donvi.iNamLamViec = @NamLamViec
		   AND donvi.iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))) result
		
END



GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khtm_du_toan_thu_bhyt_than_nhan]    Script Date: 4/5/2024 4:54:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_khtm_du_toan_thu_bhyt_than_nhan]
	@namLamViec int,
	@lstSelectedUnit ntext,
	@thanNhanQuanNhan nvarchar(50),
	@thanNhanCNVQP nvarchar(50),
	@smDuToan nvarchar(50),
	@smHachToan nvarchar(50),
	@soQuyetDinh nvarchar(500),
	@ngayQuyetDinh nvarchar(500),
	@dvt int
AS
BEGIN
	declare @TNQN_DuToan table (IdDonVi nvarchar(50),TenDonVI nvarchar(200), ThanhTien_TNQN_DuToan float);
	declare @TN_CNVQP_DuToan table (IdDonVi nvarchar(50),TenDonVI nvarchar(200), ThanhTien_TN_CNVQP_DuToan float);
	declare @TNQN_HachToan table (IdDonVi nvarchar(50),TenDonVI nvarchar(200), ThanhTien_TNQN_HachToan float);
	declare @TN_CNVQP_HachToan table (IdDonVi nvarchar(50),TenDonVI nvarchar(200), ThanhTien_TN_CNVQP_HachToan float);

	INSERT INTO @TNQN_DuToan (IdDonVi, TenDonVI, ThanhTien_TNQN_DuToan)
	SELECT
		dt_dv.id,
		dt_dv.sTenDonVi,
	   SUM(IsNull(A.ThanhTien, 0)) ThanhTien
		FROM
		  (SELECT
				   ml.sMoTa,
				   ctct.iID_MaDonVi,
				   IsNull(ctct.fDuToan, 0) ThanhTien,
				   ml.sLNS
		   FROM BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct
		   JOIN BH_DTTM_BHYT_ThanNhan_PhanBo ct ON ct.iID_DTTM_BHYT_ThanNhan_PhanBo = ctct.iID_DTTM_BHYT_ThanNhan_PhanBo
		   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
		   AND ml.iNamLamViec = @namLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS = @thanNhanQuanNhan
		   AND ml.sM = '0001'
		   WHERE ct.iNamLamViec = @namLamViec
		   AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@soQuyetDinh))
		   AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@ngayQuyetDinh))
		   ) AS A 
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

	INSERT INTO @TN_CNVQP_DuToan (IdDonVi, TenDonVI, ThanhTien_TN_CNVQP_DuToan)
	SELECT 
	   dt_dv.id,
		dt_dv.sTenDonVi,
	   SUM(IsNull(A.ThanhTien, 0)) ThanhTien
		FROM
		  (SELECT
				   ml.sMoTa,
				   ctct.iID_MaDonVi,
				   IsNull(ctct.fDuToan, 0) ThanhTien,
				   ml.sLNS
		   FROM BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct
		   JOIN BH_DTTM_BHYT_ThanNhan_PhanBo ct ON ct.iID_DTTM_BHYT_ThanNhan_PhanBo = ctct.iID_DTTM_BHYT_ThanNhan_PhanBo
		   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
		   AND ml.iNamLamViec = @namLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS = @thanNhanCNVQP
		   AND ml.sM = '0000'
		   WHERE ct.iNamLamViec = @namLamViec
		   AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@soQuyetDinh))
		   AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@ngayQuyetDinh))
		   ) AS A 
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

	INSERT INTO @TNQN_HachToan (IdDonVi, TenDonVI, ThanhTien_TNQN_HachToan)
	SELECT 
			dt_dv.id,
			dt_dv.sTenDonVi,
		  SUM(IsNull(A.ThanhTien, 0)) ThanhTien
		FROM
			 (SELECT
					  ml.sMoTa,
					  ctct.iID_MaDonVi,
					  IsNull(ctct.fDuToan, 0) ThanhTien,
					  ml.sLNS
			  FROM BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct
			  JOIN BH_DTTM_BHYT_ThanNhan_PhanBo ct ON ct.iID_DTTM_BHYT_ThanNhan_PhanBo = ctct.iID_DTTM_BHYT_ThanNhan_PhanBo
			  RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
			  AND ml.iNamLamViec = @namLamViec
			  AND ml.iTrangThai = 1
			  AND ml.sLNS = @thanNhanQuanNhan
			  AND ml.sM = '0002'
			  WHERE ct.iNamLamViec = @namLamViec
				AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@soQuyetDinh))
				AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@ngayQuyetDinh))
			  ) AS A 
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

	INSERT INTO @TN_CNVQP_HachToan (IdDonVi, TenDonVI, ThanhTien_TN_CNVQP_HachToan)
	SELECT 
		dt_dv.id,
		dt_dv.sTenDonVi,
		SUM(IsNull(A.ThanhTien, 0)) ThanhTien
		FROM
		(SELECT
					ml.sMoTa,
					ctct.iID_MaDonVi,
					IsNull(ctct.fDuToan, 0) ThanhTien,
					ml.sLNS
			FROM BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct
			JOIN BH_DTTM_BHYT_ThanNhan_PhanBo ct ON ct.iID_DTTM_BHYT_ThanNhan_PhanBo = ctct.iID_DTTM_BHYT_ThanNhan_PhanBo
			RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
			AND ml.iNamLamViec = @namLamViec
			AND ml.iTrangThai = 1
			AND ml.sLNS = @thanNhanCNVQP
			AND ml.sM = '0001'
			WHERE ct.iNamLamViec = @namLamViec
			AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@soQuyetDinh))
			AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@ngayQuyetDinh))
			) AS A 
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
		result.STenDonVi, 
		result.TN_QN_DT/@dvt TNQNDuToan, 
		result.TN_CNVQP_DT/@dvt TNCNVQPDuToan,
		result.TongDuToan/@dvt TongDuToan,
		result.TN_QN_HT/@dvt TNQNHachToan,
		result.TN_CNVQP_HT/@dvt TNCNVQPHachToan,
		result.TongHachToan/@dvt TongHachToan,
		(result.TongDuToan + result.TongHachToan)/@dvt TongCongThanNhan
		FROM
		(SELECT donvi.iID_MaDonVi idDonVi, 
		donvi.sTenDonVi,
		IsNull(tnqn_dt.ThanhTien_TNQN_DuToan, 0) TN_QN_DT,
		IsNull(tncn_dt.ThanhTien_TN_CNVQP_DuToan, 0) TN_CNVQP_DT,
		(IsNull(tnqn_dt.ThanhTien_TNQN_DuToan, 0) + IsNull(tncn_dt.ThanhTien_TN_CNVQP_DuToan, 0)) TongDuToan,
		IsNull(tnqn_ht.ThanhTien_TNQN_HachToan, 0) TN_QN_HT,
		IsNull(tncn_ht.ThanhTien_TN_CNVQP_HachToan, 0) TN_CNVQP_HT,
		(IsNull(tnqn_ht.ThanhTien_TNQN_HachToan, 0) + IsNull(tncn_ht.ThanhTien_TN_CNVQP_HachToan, 0)) TongHachToan
		FROM
		DonVi donvi
		LEFT JOIN @TNQN_DuToan tnqn_dt ON donvi.iID_MaDonVi = tnqn_dt.idDonVi
		LEFT JOIN @TN_CNVQP_DuToan tncn_dt ON donvi.iID_MaDonVi = tncn_dt.idDonVi
		LEFT JOIN @TNQN_HachToan tnqn_ht ON donvi.iID_MaDonVi = tnqn_ht.idDonVi
		LEFT JOIN @TN_CNVQP_HachToan tncn_ht ON donvi.iID_MaDonVi = tncn_ht.idDonVi
		WHERE donvi.iTrangThai = 1
		   AND donvi.iNamLamViec = @NamLamViec
		   AND donvi.iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))) result
END
;
;
;
;



GO
