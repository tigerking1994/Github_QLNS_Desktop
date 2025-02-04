/****** Object:  StoredProcedure [dbo].[sp_bh_rpt_thamdinh_quyet_toan_thu_bhyt_thannhan]    Script Date: 1/31/2024 7:35:51 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_rpt_thamdinh_quyet_toan_thu_bhyt_thannhan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_rpt_thamdinh_quyet_toan_thu_bhyt_thannhan]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_rpt_thamdinh_quyet_toan_thu_bhyt_hssv_hvqs_sqdb]    Script Date: 1/31/2024 7:35:51 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_rpt_thamdinh_quyet_toan_thu_bhyt_hssv_hvqs_sqdb]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_rpt_thamdinh_quyet_toan_thu_bhyt_hssv_hvqs_sqdb]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_rpt_thamdinh_quyet_toan_thu_bhyt_hssv_hvqs_sqdb]    Script Date: 1/31/2024 7:35:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_rpt_thamdinh_quyet_toan_thu_bhyt_hssv_hvqs_sqdb]
	@NamLamViec int,
	@LstSelectedUnit ntext,
	@Dvt int
AS
BEGIN
		declare @BHYTHSSV table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), fHSSV float);
		declare @BHYTLUUHS table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), fLuuHS float);
		declare @BHYTHVQS table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), fHVQS float);
		declare @BHYTSQDB table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), fSQDuBi float);
		
		INSERT INTO @BHYTHSSV (sTenDonVI, idDonVi, fHSSV)
			SELECT dt_dv.sTenDonVi, dt_dv.id idDonVi, SUM(IsNull(A.ThuBHXHNLDDong, 0))
			FROM
			  (
				SELECT ct.iID_MaDonVi, IsNull(ctct.fSoThamDinh, 0) ThuBHXHNLDDong
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN BH_ThamDinhQuyetToan_ChungTu ct ON ct.iID_BH_TDQT_ChungTu = ctct.iID_BH_TDQT_ChungTu AND ctct.iNamLamViec = @NamLamViec
				WHERE ct.iNamLamViec = @NamLamViec AND ctct.iMa = 163
			   ) AS A 
			   JOIN (
						SELECT iID_MaDonVi AS id, sTenDonVi, iLoai
						FROM DonVi
						WHERE iTrangThai = 1
						AND iNamLamViec = @NamLamViec
						AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
				) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
			GROUP BY dt_dv.sTenDonVi, dt_dv.id;

		INSERT INTO @BHYTLUUHS (sTenDonVI, idDonVi, fLuuHS)
			SELECT dt_dv.sTenDonVi, dt_dv.id idDonVi, SUM(IsNull(A.ThuBHXHNLDDong, 0))
			FROM
			  (
				SELECT ct.iID_MaDonVi, IsNull(ctct.fSoThamDinh, 0) ThuBHXHNLDDong
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN BH_ThamDinhQuyetToan_ChungTu ct ON ct.iID_BH_TDQT_ChungTu = ctct.iID_BH_TDQT_ChungTu AND ctct.iNamLamViec = @NamLamViec
				WHERE ct.iNamLamViec = @NamLamViec AND ctct.iMa = 167
			   ) AS A 
			   JOIN (
						SELECT iID_MaDonVi AS id, sTenDonVi, iLoai
						FROM DonVi
						WHERE iTrangThai = 1
						AND iNamLamViec = @NamLamViec
						AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
				) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
			GROUP BY dt_dv.sTenDonVi, dt_dv.id;

		INSERT INTO @BHYTHVQS (sTenDonVI, idDonVi, fHVQS)
			SELECT dt_dv.sTenDonVi, dt_dv.id idDonVi, SUM(IsNull(A.ThuBHXHNLDDong, 0))
			FROM
			  (
				SELECT ct.iID_MaDonVi, IsNull(ctct.fSoThamDinh, 0) ThuBHXHNLDDong
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN BH_ThamDinhQuyetToan_ChungTu ct ON ct.iID_BH_TDQT_ChungTu = ctct.iID_BH_TDQT_ChungTu AND ctct.iNamLamViec = @NamLamViec
				WHERE ct.iNamLamViec = @NamLamViec AND ctct.iMa = 159
			   ) AS A 
			   JOIN (
						SELECT iID_MaDonVi AS id, sTenDonVi, iLoai
						FROM DonVi
						WHERE iTrangThai = 1
						AND iNamLamViec = @NamLamViec
						AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
				) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
			GROUP BY dt_dv.sTenDonVi, dt_dv.id;

		INSERT INTO @BHYTSQDB (sTenDonVI, idDonVi, fSQDuBi)
			SELECT dt_dv.sTenDonVi, dt_dv.id idDonVi, SUM(IsNull(A.ThuBHXHNLDDong, 0))
			FROM
			  (
				SELECT ct.iID_MaDonVi, IsNull(ctct.fSoThamDinh, 0) ThuBHXHNLDDong
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN BH_ThamDinhQuyetToan_ChungTu ct ON ct.iID_BH_TDQT_ChungTu = ctct.iID_BH_TDQT_ChungTu AND ctct.iNamLamViec = @NamLamViec
				WHERE ct.iNamLamViec = @NamLamViec AND ctct.iMa = 171
			   ) AS A 
			   JOIN (
						SELECT iID_MaDonVi AS id, sTenDonVi, iLoai
						FROM DonVi
						WHERE iTrangThai = 1
						AND iNamLamViec = @NamLamViec
						AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
				) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
			GROUP BY dt_dv.sTenDonVi, dt_dv.id;

		SELECT hssv.idDonVi, hssv.sTenDonVI, 
		IsNull(hssv.fHSSV, 0)/@Dvt fHSSV,
		IsNull(luuhs.fLuuHS, 0)/@Dvt fLuuHS, 
		IsNull(hssv.fHSSV, 0)/@Dvt + IsNull(luuhs.fLuuHS, 0)/@Dvt fTongHSSV,
		IsNull(hvqs.fHVQS, 0)/@Dvt fHVQS, 
		IsNull(sqdb.fSQDuBi, 0)/@Dvt fSQDuBi,
		IsNull(hssv.fHSSV, 0)/@Dvt + IsNull(luuhs.fLuuHS, 0)/@Dvt + IsNull(hvqs.fHVQS, 0)/@Dvt + IsNull(sqdb.fSQDuBi, 0)/@Dvt fTongCongHSSV
		FROM @BHYTHSSV hssv
		LEFT JOIN @BHYTLUUHS luuhs ON hssv.idDonVi = luuhs.idDonVi
		LEFT JOIN @BHYTHVQS hvqs ON hssv.idDonVi = hvqs.idDonVi
		LEFT JOIN @BHYTSQDB sqdb ON hssv.idDonVi = sqdb.idDonVi
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_rpt_thamdinh_quyet_toan_thu_bhyt_thannhan]    Script Date: 1/31/2024 7:35:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_rpt_thamdinh_quyet_toan_thu_bhyt_thannhan]
	@NamLamViec int,
	@LstSelectedUnit ntext,
	@Dvt int
AS
BEGIN
		declare @BHYTTNQN table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), fSoPhaiThuTNQN float);
		declare @BHYTTNCNVCQP table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), fSoPhaiThuTNCNVQP float);
		
		INSERT INTO @BHYTTNQN (sTenDonVI, idDonVi, fSoPhaiThuTNQN)
			SELECT dt_dv.sTenDonVi, dt_dv.id idDonVi, SUM(IsNull(A.ThuBHXHNLDDong, 0))
			FROM
			  (
				SELECT ct.iID_MaDonVi, IsNull(ctct.fSoThamDinh, 0) ThuBHXHNLDDong
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN BH_ThamDinhQuyetToan_ChungTu ct ON ct.iID_BH_TDQT_ChungTu = ctct.iID_BH_TDQT_ChungTu AND ctct.iNamLamViec = @NamLamViec
				WHERE ct.iNamLamViec = @NamLamViec AND ctct.iMa = 151
			   ) AS A 
			   JOIN (
						SELECT iID_MaDonVi AS id, sTenDonVi, iLoai
						FROM DonVi
						WHERE iTrangThai = 1
						AND iNamLamViec = @NamLamViec
						AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
				) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
			GROUP BY dt_dv.sTenDonVi, dt_dv.id;

		INSERT INTO @BHYTTNCNVCQP (sTenDonVI, idDonVi, fSoPhaiThuTNCNVQP)
			SELECT dt_dv.sTenDonVi, dt_dv.id idDonVi, SUM(IsNull(A.ThuBHXHNLDDong, 0))
			FROM
			  (
				SELECT ct.iID_MaDonVi, IsNull(ctct.fSoThamDinh, 0) ThuBHXHNLDDong
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN BH_ThamDinhQuyetToan_ChungTu ct ON ct.iID_BH_TDQT_ChungTu = ctct.iID_BH_TDQT_ChungTu AND ctct.iNamLamViec = @NamLamViec
				WHERE ct.iNamLamViec = @NamLamViec AND ctct.iMa = 155
			   ) AS A 
			   JOIN (
						SELECT iID_MaDonVi AS id, sTenDonVi, iLoai
						FROM DonVi
						WHERE iTrangThai = 1
						AND iNamLamViec = @NamLamViec
						AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
				) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
			GROUP BY dt_dv.sTenDonVi, dt_dv.id;

		SELECT tnqh.idDonVi, tnqh.sTenDonVI, 
		IsNull(tnqh.fSoPhaiThuTNQN, 0)/@Dvt fSoPhaiThuTNQN,
		IsNull(cnvcqp.fSoPhaiThuTNCNVQP, 0)/@Dvt fSoPhaiThuTNCNVQP, 
		IsNull(tnqh.fSoPhaiThuTNQN, 0)/@Dvt + IsNull(cnvcqp.fSoPhaiThuTNCNVQP, 0)/@Dvt fTongCong
		FROM @BHYTTNQN tnqh
		LEFT JOIN @BHYTTNCNVCQP cnvcqp ON tnqh.idDonVi = cnvcqp.idDonVi
END
;
GO
