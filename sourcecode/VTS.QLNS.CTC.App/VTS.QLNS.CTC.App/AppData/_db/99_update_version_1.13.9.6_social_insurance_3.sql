/****** Object:  StoredProcedure [dbo].[sp_bh_rpt_thamdinh_quyet_toan_thu_bhyt_quannhan]    Script Date: 1/31/2024 5:28:03 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_rpt_thamdinh_quyet_toan_thu_bhyt_quannhan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_rpt_thamdinh_quyet_toan_thu_bhyt_quannhan]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_rpt_thamdinh_quyet_toan_thu_bhyt_nld]    Script Date: 1/31/2024 5:28:03 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_rpt_thamdinh_quyet_toan_thu_bhyt_nld]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_rpt_thamdinh_quyet_toan_thu_bhyt_nld]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_rpt_thamdinh_quyet_toan_thu_bhxh_bhtn]    Script Date: 1/31/2024 5:28:03 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_rpt_thamdinh_quyet_toan_thu_bhxh_bhtn]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_rpt_thamdinh_quyet_toan_thu_bhxh_bhtn]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_rpt_thamdinh_quyet_toan_thu_bhxh_bhtn]    Script Date: 1/31/2024 7:33:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_rpt_thamdinh_quyet_toan_thu_bhxh_bhtn]
	@NamLamViec int,
	@LstSelectedUnit ntext,
	@Dvt int,
	@IsBHXH bit
AS
BEGIN
		declare @DTBHXHNLDDONG table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), BhxhNldDongDuToan float);
		declare @DTBHXHNLDLDDONG table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), BhxhNldLdDongDuToan float);
		declare @HTBHXHNLDDONG table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), BhxhNldDongHachToan float);
		declare @HTBHXHNLDLDDONG table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), BhxhNldLdDongHachToan float);
		
		INSERT INTO @DTBHXHNLDDONG (sTenDonVI, idDonVi, BhxhNldDongDuToan)
			SELECT dt_dv.sTenDonVi, dt_dv.id idDonVi, SUM(IsNull(A.ThuBHXHNLDDong, 0))
			FROM
			  (
				SELECT ct.iID_MaDonVi, IsNull(ctct.fSoThamDinh, 0) ThuBHXHNLDDong
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN BH_ThamDinhQuyetToan_ChungTu ct ON ct.iID_BH_TDQT_ChungTu = ctct.iID_BH_TDQT_ChungTu AND ctct.iNamLamViec = @NamLamViec
				WHERE ct.iNamLamViec = @NamLamViec AND
				((@IsBHXH = 1 AND ctct.iMa IN (7,8,9,10,12,13,14,15)) OR (@IsBHXH = 0 AND ctct.iMa IN (70,71)))
			   ) AS A 
			   JOIN (
						SELECT iID_MaDonVi AS id, sTenDonVi, iLoai
						FROM DonVi
						WHERE iTrangThai = 1
						AND iNamLamViec = @NamLamViec
						AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
				) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
			GROUP BY dt_dv.sTenDonVi, dt_dv.id;

		INSERT INTO @DTBHXHNLDLDDONG (sTenDonVI, idDonVi, BhxhNldLdDongDuToan)
			SELECT dt_dv.sTenDonVi, dt_dv.id idDonVi, SUM(IsNull(A.ThuBHXHNLDDong, 0))
			FROM
			  (
				SELECT ct.iID_MaDonVi, IsNull(ctct.fSoThamDinh, 0) ThuBHXHNLDDong
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN BH_ThamDinhQuyetToan_ChungTu ct ON ct.iID_BH_TDQT_ChungTu = ctct.iID_BH_TDQT_ChungTu AND ctct.iNamLamViec = @NamLamViec
				WHERE ct.iNamLamViec = @NamLamViec AND 
				((@IsBHXH = 1 AND ctct.iMa IN (19,20,21,22,24,25,26,27,29,30)) OR (@IsBHXH = 0 AND ctct.iMa IN (7,73,74)))
			   ) AS A 
			   JOIN (
						SELECT iID_MaDonVi AS id, sTenDonVi, iLoai
						FROM DonVi
						WHERE iTrangThai = 1
						AND iNamLamViec = @NamLamViec
						AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
				) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
			GROUP BY dt_dv.sTenDonVi, dt_dv.id;

		INSERT INTO @HTBHXHNLDDONG (sTenDonVI, idDonVi, BhxhNldDongHachToan)
			SELECT dt_dv.sTenDonVi, dt_dv.id idDonVi, SUM(IsNull(A.ThuBHXHNLDDong, 0))
			FROM
			  (
				SELECT ct.iID_MaDonVi, IsNull(ctct.fSoThamDinh, 0) ThuBHXHNLDDong
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN BH_ThamDinhQuyetToan_ChungTu ct ON ct.iID_BH_TDQT_ChungTu = ctct.iID_BH_TDQT_ChungTu AND ctct.iNamLamViec = @NamLamViec
				WHERE ct.iNamLamViec = @NamLamViec AND
				((@IsBHXH = 1 AND  ctct.iMa IN (34,35,36,37,39,40,41,42)) OR (@IsBHXH = 0 AND ctct.iMa IN (77,78)))
			   ) AS A 
			   JOIN (
						SELECT iID_MaDonVi AS id, sTenDonVi, iLoai
						FROM DonVi
						WHERE iTrangThai = 1
						AND iNamLamViec = @NamLamViec
						AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
				) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
			GROUP BY dt_dv.sTenDonVi, dt_dv.id;

		INSERT INTO @HTBHXHNLDLDDONG (sTenDonVI, idDonVi, BhxhNldLdDongHachToan)
			SELECT dt_dv.sTenDonVi, dt_dv.id idDonVi, SUM(IsNull(A.ThuBHXHNLDDong, 0))
			FROM
			  (
				SELECT ct.iID_MaDonVi, IsNull(ctct.fSoThamDinh, 0) ThuBHXHNLDDong
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN BH_ThamDinhQuyetToan_ChungTu ct ON ct.iID_BH_TDQT_ChungTu = ctct.iID_BH_TDQT_ChungTu AND ctct.iNamLamViec = @NamLamViec
				WHERE ct.iNamLamViec = @NamLamViec AND 
				((@IsBHXH = 1 AND ctct.iMa IN (46,47,48,49,51,52,53,54,56,57)) OR (@IsBHXH = 0 AND ctct.iMa IN (80,81)))
			   ) AS A 
			   JOIN (
						SELECT iID_MaDonVi AS id, sTenDonVi, iLoai
						FROM DonVi
						WHERE iTrangThai = 1
						AND iNamLamViec = @NamLamViec
						AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
				) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
			GROUP BY dt_dv.sTenDonVi, dt_dv.id;

		SELECT dt.idDonVi, dt.sTenDonVI, 
		Case when @IsBHXH = 1 then IsNull(dt.BhxhNldDongDuToan, 0)/@Dvt end as fBhxhNldDongDuToan,
		Case when @IsBHXH = 0 then IsNull(dt.BhxhNldDongDuToan, 0)/@Dvt end as fBhtnNldDongDuToan,
		Case when @IsBHXH = 1 then IsNull(dtld.BhxhNldLdDongDuToan, 0)/@Dvt end as fBhxhNsddDongDuToan, 
		Case when @IsBHXH = 0 then IsNull(dtld.BhxhNldLdDongDuToan, 0)/@Dvt end as fBhtnNsddDongDuToan,
		Case when @IsBHXH = 1 then (IsNull(dt.BhxhNldDongDuToan, 0)/@Dvt + IsNull(dtld.BhxhNldLdDongDuToan, 0)/@Dvt) end as fBHXHTongCongDuToan,
		Case when @IsBHXH = 0 then (IsNull(dt.BhxhNldDongDuToan, 0)/@Dvt + IsNull(dtld.BhxhNldLdDongDuToan, 0)/@Dvt) end as fBHTNTongCongDuToan,
		Case when @IsBHXH = 1 then IsNull(ht.BhxhNldDongHachToan, 0)/@Dvt end as fBhxhNldDongHachToan,
		Case when @IsBHXH = 0 then IsNull(ht.BhxhNldDongHachToan, 0)/@Dvt end as fBhtnNldDongHachToan, 
		Case when @IsBHXH = 1 then IsNull(htld.BhxhNldLdDongHachToan, 0)/@Dvt end as fBhxhNsddDongHachToan,
		Case when @IsBHXH = 0 then IsNull(htld.BhxhNldLdDongHachToan, 0)/@Dvt end as fBhtnNsddDongHachToan,
		Case when @IsBHXH = 1 then (IsNull(ht.BhxhNldDongHachToan, 0)/@Dvt + IsNull(htld.BhxhNldLdDongHachToan, 0)/@Dvt) end as fBHXHTongCongHachToan,
		Case when @IsBHXH = 0 then (IsNull(ht.BhxhNldDongHachToan, 0)/@Dvt + IsNull(htld.BhxhNldLdDongHachToan, 0)/@Dvt) end as fBHTNTongCongHachToan
		FROM @DTBHXHNLDDONG dt
		LEFT JOIN @DTBHXHNLDLDDONG dtld ON dt.idDonVi = dtld.idDonVi
		LEFT JOIN @HTBHXHNLDDONG ht ON dt.idDonVi = ht.idDonVi
		LEFT JOIN @HTBHXHNLDLDDONG htld ON dt.idDonVi = htld.idDonVi
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_rpt_thamdinh_quyet_toan_thu_bhyt_nld]    Script Date: 1/31/2024 5:28:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_rpt_thamdinh_quyet_toan_thu_bhyt_nld]
	@NamLamViec int,
	@LstSelectedUnit ntext,
	@Dvt int
AS
BEGIN
		declare @DTBHYTNLDDONG table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), BhytNldDongDuToan float);
		declare @DTBHYTNLDLDDONG table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), BhytNldLdDongDuToan float);
		declare @HTBHYTNLDDONG table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), BhytNldDongHachToan float);
		declare @HTBHYTLDLDDONG table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), BhytNldLdDongHachToan float);
		
		INSERT INTO @DTBHYTNLDDONG (sTenDonVI, idDonVi, BhytNldDongDuToan)
			SELECT dt_dv.sTenDonVi, dt_dv.id idDonVi, SUM(IsNull(A.ThuBHXHNLDDong, 0))
			FROM
			  (
				SELECT ct.iID_MaDonVi, IsNull(ctct.fSoThamDinh, 0) ThuBHXHNLDDong
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN BH_ThamDinhQuyetToan_ChungTu ct ON ct.iID_BH_TDQT_ChungTu = ctct.iID_BH_TDQT_ChungTu AND ctct.iNamLamViec = @NamLamViec
				WHERE ct.iNamLamViec = @NamLamViec AND ctct.iMa IN (95, 96, 98, 99)
			   ) AS A 
			   JOIN (
						SELECT iID_MaDonVi AS id, sTenDonVi, iLoai
						FROM DonVi
						WHERE iTrangThai = 1
						AND iNamLamViec = @NamLamViec
						AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
				) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
			GROUP BY dt_dv.sTenDonVi, dt_dv.id;

		INSERT INTO @DTBHYTNLDLDDONG (sTenDonVI, idDonVi, BhytNldLdDongDuToan)
			SELECT dt_dv.sTenDonVi, dt_dv.id idDonVi, SUM(IsNull(A.ThuBHXHNLDDong, 0))
			FROM
			  (
				SELECT ct.iID_MaDonVi, IsNull(ctct.fSoThamDinh, 0) ThuBHXHNLDDong
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN BH_ThamDinhQuyetToan_ChungTu ct ON ct.iID_BH_TDQT_ChungTu = ctct.iID_BH_TDQT_ChungTu AND ctct.iNamLamViec = @NamLamViec
				WHERE ct.iNamLamViec = @NamLamViec AND ctct.iMa IN (102, 103, 105, 106)
			   ) AS A 
			   JOIN (
						SELECT iID_MaDonVi AS id, sTenDonVi, iLoai
						FROM DonVi
						WHERE iTrangThai = 1
						AND iNamLamViec = @NamLamViec
						AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
				) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
			GROUP BY dt_dv.sTenDonVi, dt_dv.id;

		INSERT INTO @HTBHYTNLDDONG (sTenDonVI, idDonVi, BhytNldDongHachToan)
			SELECT dt_dv.sTenDonVi, dt_dv.id idDonVi, SUM(IsNull(A.ThuBHXHNLDDong, 0))
			FROM
			  (
				SELECT ct.iID_MaDonVi, IsNull(ctct.fSoThamDinh, 0) ThuBHXHNLDDong
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN BH_ThamDinhQuyetToan_ChungTu ct ON ct.iID_BH_TDQT_ChungTu = ctct.iID_BH_TDQT_ChungTu AND ctct.iNamLamViec = @NamLamViec
				WHERE ct.iNamLamViec = @NamLamViec AND ctct.iMa IN (110, 111, 113, 114)
			   ) AS A 
			   JOIN (
						SELECT iID_MaDonVi AS id, sTenDonVi, iLoai
						FROM DonVi
						WHERE iTrangThai = 1
						AND iNamLamViec = @NamLamViec
						AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
				) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
			GROUP BY dt_dv.sTenDonVi, dt_dv.id;

		INSERT INTO @HTBHYTLDLDDONG (sTenDonVI, idDonVi, BhytNldLdDongHachToan)
			SELECT dt_dv.sTenDonVi, dt_dv.id idDonVi, SUM(IsNull(A.ThuBHXHNLDDong, 0))
			FROM
			  (
				SELECT ct.iID_MaDonVi, IsNull(ctct.fSoThamDinh, 0) ThuBHXHNLDDong
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN BH_ThamDinhQuyetToan_ChungTu ct ON ct.iID_BH_TDQT_ChungTu = ctct.iID_BH_TDQT_ChungTu AND ctct.iNamLamViec = @NamLamViec
				WHERE ct.iNamLamViec = @NamLamViec AND ctct.iMa IN (117, 118, 120, 121)
			   ) AS A 
			   JOIN (
						SELECT iID_MaDonVi AS id, sTenDonVi, iLoai
						FROM DonVi
						WHERE iTrangThai = 1
						AND iNamLamViec = @NamLamViec
						AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
				) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
			GROUP BY dt_dv.sTenDonVi, dt_dv.id;

		SELECT dt.idDonVi, dt.sTenDonVI, 
		IsNull(dt.BhytNldDongDuToan, 0)/@Dvt fBhytNldDongDuToan,
		IsNull(dtld.BhytNldLdDongDuToan, 0)/@Dvt fBhytNsddDongDuToan, 
		IsNull(dt.BhytNldDongDuToan, 0)/@Dvt + IsNull(dtld.BhytNldLdDongDuToan, 0)/@Dvt fBHYTTongCongDuToan,
		IsNull(ht.BhytNldDongHachToan, 0)/@Dvt fBhytNldDongHachToan, 
		IsNull(htld.BhytNldLdDongHachToan, 0)/@Dvt fBhytNsddDongHachToan,
		IsNull(ht.BhytNldDongHachToan, 0)/@Dvt + IsNull(htld.BhytNldLdDongHachToan, 0)/@Dvt fBHYTTongCongHachToan
		FROM @DTBHYTNLDDONG dt
		LEFT JOIN @DTBHYTNLDLDDONG dtld ON dt.idDonVi = dtld.idDonVi
		LEFT JOIN @HTBHYTNLDDONG ht ON dt.idDonVi = ht.idDonVi
		LEFT JOIN @HTBHYTLDLDDONG htld ON dt.idDonVi = htld.idDonVi
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_rpt_thamdinh_quyet_toan_thu_bhyt_quannhan]    Script Date: 1/31/2024 5:28:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_rpt_thamdinh_quyet_toan_thu_bhyt_quannhan]
	@NamLamViec int,
	@LstSelectedUnit ntext,
	@Dvt int
AS
BEGIN
		declare @DTBHYTNLDDONG table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), BhytNldDongDuToan float);
		declare @HTBHYTNLDDONG table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), BhytNldDongHachToan float);
		
		INSERT INTO @DTBHYTNLDDONG (sTenDonVI, idDonVi, BhytNldDongDuToan)
			SELECT dt_dv.sTenDonVi, dt_dv.id idDonVi, SUM(IsNull(A.ThuBHXHNLDDong, 0))
			FROM
			  (
				SELECT ct.iID_MaDonVi, IsNull(ctct.fSoThamDinh, 0) ThuBHXHNLDDong
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN BH_ThamDinhQuyetToan_ChungTu ct ON ct.iID_BH_TDQT_ChungTu = ctct.iID_BH_TDQT_ChungTu AND ctct.iNamLamViec = @NamLamViec
				WHERE ct.iNamLamViec = @NamLamViec AND ctct.iMa IN (133, 134, 135, 136)
			   ) AS A 
			   JOIN (
						SELECT iID_MaDonVi AS id, sTenDonVi, iLoai
						FROM DonVi
						WHERE iTrangThai = 1
						AND iNamLamViec = @NamLamViec
						AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
				) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
			GROUP BY dt_dv.sTenDonVi, dt_dv.id;

		INSERT INTO @HTBHYTNLDDONG (sTenDonVI, idDonVi, BhytNldDongHachToan)
			SELECT dt_dv.sTenDonVi, dt_dv.id idDonVi, SUM(IsNull(A.ThuBHXHNLDDong, 0))
			FROM
			  (
				SELECT ct.iID_MaDonVi, IsNull(ctct.fSoThamDinh, 0) ThuBHXHNLDDong
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN BH_ThamDinhQuyetToan_ChungTu ct ON ct.iID_BH_TDQT_ChungTu = ctct.iID_BH_TDQT_ChungTu AND ctct.iNamLamViec = @NamLamViec
				WHERE ct.iNamLamViec = @NamLamViec AND ctct.iMa IN (138, 139, 140, 141)
			   ) AS A 
			   JOIN (
						SELECT iID_MaDonVi AS id, sTenDonVi, iLoai
						FROM DonVi
						WHERE iTrangThai = 1
						AND iNamLamViec = @NamLamViec
						AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
				) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
			GROUP BY dt_dv.sTenDonVi, dt_dv.id;

		SELECT dt.idDonVi, dt.sTenDonVI, 
		IsNull(dt.BhytNldDongDuToan, 0)/@Dvt fBHYTTongCongDuToan,
		IsNull(ht.BhytNldDongHachToan, 0)/@Dvt fBHYTTongCongHachToan
		FROM @DTBHYTNLDDONG dt
		LEFT JOIN @HTBHYTNLDDONG ht ON dt.idDonVi = ht.idDonVi
END
;
GO
