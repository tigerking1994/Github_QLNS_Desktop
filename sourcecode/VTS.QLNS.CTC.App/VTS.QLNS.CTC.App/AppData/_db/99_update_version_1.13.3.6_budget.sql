/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt_1]    Script Date: 25/10/2023 2:20:24 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt_1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt_1]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_nhan_phan_bo_du_toan_chi_tiet_so_quyet_dinh_clone]    Script Date: 25/10/2023 2:20:24 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_nhan_phan_bo_du_toan_chi_tiet_so_quyet_dinh_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_nhan_phan_bo_du_toan_chi_tiet_so_quyet_dinh_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_danhsach_chungtu_chitiet]    Script Date: 25/10/2023 2:20:24 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_danhsach_chungtu_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_danhsach_chungtu_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_danhsach_chungtu_chitiet]    Script Date: 25/10/2023 2:20:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_danhsach_chungtu_chitiet]
	@VoucherId NVARCHAR(255),
	@LNS NVARCHAR(MAX),
	@AgencyId NVARCHAR(MAX),
	@YearOfWork INT,
	@BudgetOfYear INT,
	@SourceOfBudget INT,
	@UserName NVARCHAR(100),
	@IsGetAll BIT
AS
BEGIN
	DECLARE @sVoucherId NVARCHAR(255) = @VoucherId;
	DECLARE @sLNS NVARCHAR(MAX) = @LNS;
	DECLARE @sAgencyId NVARCHAR(MAX) = @AgencyId;
	DECLARE @iYearOfWork INT = @YearOfWork;
	DECLARE @iBudgetOfYear INT = @BudgetOfYear;
	DECLARE @iSourceOfBudget INT = @SourceOfBudget;
	DECLARE @sUserName NVARCHAR(100) = @UserName;
	DECLARE @CountRoot INT; 
	DECLARE @isRoot INT;
	SELECT @CountRoot = COUNT(*)
	FROM
	  (SELECT *
	   FROM NguoiDung_DonVi
	   WHERE iID_MaNguoiDung = @sUserName
		 AND iNamLamViec = @iYearOfWork
		 AND iTrangThai = 1) nddv
	INNER JOIN
	  (SELECT *
	   FROM DonVi
	   WHERE iNamLamViec = @iYearOfWork
		 AND iTrangThai = 1
		 AND iLoai = 0) dv ON dv.iID_MaDonVi = nddv.iID_MaDonVi;

	DECLARE @dNgayQuyetDinh AS DATETIME;
	DECLARE @dNgayChungTu AS DATETIME;
	DECLARE @dNgayPhanBo AS DATETIME;
	DECLARE @iSoChungTuIndex AS INT;

	SELECT 
		@dNgayQuyetDinh = CAST(dNgayQuyetDinh AS DATE), 
		@dNgayChungTu = CAST(dNgayChungTu AS DATE),
		@iSoChungTuIndex = iSoChungTuIndex
	FROM NS_DT_ChungTu 
	WHERE iID_DTChungTu = @sVoucherId;

	SELECT @isRoot = COUNT(*) FROM DonVi 
  JOIN NguoiDung_DonVi ON DonVi.iID_MaDonVi = NguoiDung_DonVi.iID_MaDonVi  
	WHERE iLoai = 0 AND NguoiDung_DonVi.iID_MaNguoiDung = @sUserName AND NguoiDung_DonVi.iNamLamViec = @iYearOfWork AND DonVi.iNamLamViec= @iYearOfWork;
	IF @isRoot > 0
		SET @isRoot = 1
	ELSE
		SET @isRoot = 0 

	IF @dNgayQuyetDinh IS NULL
		SELECT @dNgayPhanBo = @dNgayChungTu;
	ELSE 
		SELECT @dNgayPhanBo = @dNgayQuyetDinh;

	SELECT DISTINCT sLNS INTO #tblLns FROM NS_DT_ChungTuChiTiet 
	WHERE iID_DTChungTu IN (SELECT iID_CTDuToan_Nhan FROM NS_DT_Nhan_PhanBo_Map WHERE iID_CTDuToan_PhanBo = @sVoucherId)


	SELECT iID_MLNS INTO #tblIdMlns 
	FROM (
	SELECT * FROM NS_DT_ChungTuChiTiet WHERE iID_DTChungTu IN (SELECT iID_CTDuToan_Nhan FROM NS_DT_Nhan_PhanBo_Map WHERE iID_CTDuToan_PhanBo = @sVoucherId)
	UNION ALL 
	SELECT * FROM NS_DT_ChungTuChiTiet WHERE iID_DTChungTu = @sVoucherId) tblNhan
	GROUP BY iID_MLNS;

	SELECT * INTO #tblMlns
	FROM NS_MucLucNganSach
	WHERE iNamLamViec = @iYearOfWork
		AND iTrangThai = 1
		AND bHangChaDuToan IS NOT NULL
		AND ((@CountRoot <> 0
			AND sLNS IN
				(SELECT DISTINCT VALUE
					FROM
					(SELECT CAST(LEFT(Item, 1) AS NVARCHAR(10)) LNS1,
							CAST(LEFT(Item, 3) AS NVARCHAR(10)) LNS3,
							CAST(LEFT(Item, 5) AS NVARCHAR(10)) LNS5,
							CAST(Item AS NVARCHAR(10)) LNS
					FROM f_split(@LNS) ) LNS UNPIVOT (value FOR col in (LNS1, LNS3, LNS5, LNS)) un))
			OR (@CountRoot = 0
				AND sLNS IN
				(SELECT DISTINCT VALUE
					FROM
					(SELECT CAST(LEFT(sLNS, 1) AS NVARCHAR(10)) LNS1,
							CAST(LEFT(sLNS, 3) AS NVARCHAR(10)) LNS3,
							CAST(LEFT(sLNS, 5) AS NVARCHAR(10)) LNS5,
							CAST(sLNS AS NVARCHAR(10)) LNS
					FROM NS_NguoiDung_LNS
					WHERE sMaNguoiDung = @sUserName
						AND iNamLamViec = @iYearOfWork
						AND sLNS IN (SELECT * FROM f_split(@sLNS))) LNS UNPIVOT (VALUE FOR col in (LNS1, LNS3, LNS5, LNS)) un)))

	-- lấy dữ liệu seting nhập từ bảng MLNS
	SELECT sLNS, bTuChi, bHienVat, bHangNhap, bHangMua, bPhanCap, bDuPhong, bTonKho INTO #tblMlnsseting FROM #tblMlns WHERE sL = ''
	
	-- lấy dữ liệu MLNS cha
	SELECT * INTO #tblMlnsParent FROM #tblMlns WHERE bHangChaDuToan = 1

	-- lấy dữ liệu MLNS con
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, mlns.sLns, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha, bHangChaDuToan,
		seting.bTuChi, seting.bHienVat, seting.bHangNhap, seting.bHangMua, seting.bPhanCap, seting.bDuPhong, seting.bTonKho
	INTO #tblMlnsChild
	FROM #tblMlns mlns
	LEFT JOIN #tblMlnsseting seting
	ON seting.sLNS = mlns.sLNS
	WHERE bHangChaDuToan = 0 
	AND (@IsGetAll = 1 OR NOT EXISTS(SELECT * FROM #tblLns) OR ((EXISTS(SELECT * FROM #tblLns) AND iID_MLNS in (SELECT * FROM #tblIdMlns))))

	-- lấy ra đơn vị được chọn để phân bổ theo iID_ChungTu phân bổ
	SELECT iID_MaDonVi AS MaDonVi, sTenDonVi
	INTO #tblDonViPhanBo
	FROM DonVi
	WHERE iNamLamViec = @iYearOfWork
		AND iID_MaDonVi IN (SELECT * FROM f_split(@sAgencyId))
	-- lấy ra số quyết định nhận được chọn để phân bổ theo iID_ChungTu phân bổ
	SELECT 
		sSoQuyetDinh, 
		iID_DTChungTu AS idSoQuyetDinh 
	INTO #tblSoQuyetDinhNhan
	FROM NS_DT_ChungTu 
	WHERE iID_DTChungTu IN (
		SELECT iID_CTDuToan_Nhan 
		FROM NS_DT_Nhan_PhanBo_Map 
		WHERE iID_CTDuToan_PhanBo = @sVoucherId
	)
	-- lấy dữ liệu nhận phân bổ theo so quyet dinh va don vi
		SELECT vctpb.iID_MLNS, 
		vctpb.sSoQuyetDinh, 
		vctpb.idSoQuyetDinh,
		SUM(fTuChi) AS fTuChi, 
		SUM(fHienVat) AS fHienVat,
		SUM(fDuPhong) AS fDuPhong,
		SUM(fHangMua) AS fHangMua,
		SUM(fHangNhap) AS fHangNhap,
		SUM(fPhanCap) AS fPhanCap,
		SUM(fTonKho) AS fTonKho 
		INTO #tblChungTuNhanPhanBo
		FROM  (
			SELECT 
				ctct.iID_MLNS, 
				(CASE WHEN @isRoot = 1 then dt.sSoQuyetDinh ELSE '' end) AS sSoQuyetDinh,
				(CASE WHEN @isRoot = 1 then dt.iID_DTChungTu ELSE CAST(NULL AS UNIQUEIDENTIFIER) end) AS idSoQuyetDinh,
				fTuChi, 
				fHienVat,
				fDuPhong,
				fHangMua,
				fHangNhap,
				fPhanCap,
				fTonKho
			FROM NS_DT_ChungTuChiTiet ctct
			INNER JOIN NS_DT_ChungTu dt
			ON dt.iID_DTChungTu = ctct.iID_DTChungTu AND ctct.iDuLieuNhan = 0
			WHERE dt.iID_DTChungTu IN ( SELECT iID_CTDuToan_Nhan FROM NS_DT_Nhan_PhanBo_Map WHERE iID_CTDuToan_PhanBo = @sVoucherId) 
	) vctpb
	GROUP BY vctpb.iID_MLNS, vctpb.sSoQuyetDinh, vctpb.idSoQuyetDinh

	SELECT vdaphanbo.iID_MLNS, 
		vdaphanbo.sSoQuyetDinh, 
		vdaphanbo.idSoQuyetDinh,
		0 - SUM(fTuChi) AS fTuChi, 
		0 - SUM(fHienVat) AS fHienVat,
		0 - SUM(fDuPhong) AS fDuPhong,
		0 - SUM(fHangMua) AS fHangMua,
		0 - SUM(fHangNhap) AS fHangNhap,
		0 - SUM(fPhanCap) AS fPhanCap,
		0 - SUM(fTonKho) AS fTonKho
	INTO #tblDaPhanBo
	FROM (
			SELECT 
				ctct.iID_MLNS, 
				(CASE WHEN @isRoot = 1 then ct.sSoQuyetDinh ELSE '' end) AS sSoQuyetDinh,
				(CASE WHEN @isRoot = 1 then ct.iID_DTChungTu ELSE CAST(NULL AS UNIQUEIDENTIFIER) end) AS idSoQuyetDinh,
				fTuChi, 
				fHienVat,
				fDuPhong,
				fHangMua,
				fHangNhap,
				fPhanCap,
				fTonKho
			FROM NS_DT_ChungTuChiTiet ctct
			LEFT JOIN NS_DT_ChungTu ct
			ON ctct.iID_CTDuToan_Nhan = ct.iID_DTChungTu
			LEFT JOIN NS_DT_ChungTu dtct
			ON ctct.iID_DTChungTu = dtct.iID_DTChungTu
			WHERE (iID_CTDuToan_Nhan IS NOT NULL AND iID_CTDuToan_Nhan IN (SELECT idSoQuyetDinh FROM #tblSoQuyetDinhNhan))
			AND (
				(dtct.dNgayQuyetDinh IS NOT NULL 
				AND ((CAST(dtct.dNgayQuyetDinh AS DATE) = CAST(@dNgayPhanBo AS DATE) AND dtct.iSoChungTuIndex < @iSoChungTuIndex)
					OR 
					(CAST(dtct.dNgayQuyetDinh AS DATE) < CAST(@dNgayPhanBo AS DATE))))
				OR (dtct.dNgayQuyetDinh IS NULL AND 
					(CAST(dtct.dNgayChungTu AS DATE) <= CAST(@dNgayPhanBo AS DATE) AND dtct.iSoChungTuIndex < @iSoChungTuIndex)
					OR 
					(CAST(dtct.dNgayChungTu AS DATE) < CAST(@dNgayPhanBo AS DATE))))
			AND ctct.iID_DTChungTu <> @sVoucherId
	) vdaphanbo	
	GROUP BY vdaphanbo.iID_MLNS, vdaphanbo.sSoQuyetDinh, vdaphanbo.idSoQuyetDinh

	SELECT iID_MLNS, sSoQuyetDinh, idSoQuyetDinh,
		SUM(fTuChi) AS fTuChi, 
		SUM(fHienVat) AS fHienVat,
		SUM(fDuPhong) AS fDuPhong,
		SUM(fHangMua) AS fHangMua,
		SUM(fHangNhap) AS fHangNhap,
		SUM(fPhanCap) AS fPhanCap,
		SUM(fTonKho) AS fTonKho
		INTO #tblNhanPhanBo
		FROM (
			SELECT 
				iID_MLNS,
				(CASE WHEN @isRoot = 1 then sSoQuyetDinh ELSE '' end) AS sSoQuyetDinh,
				(CASE WHEN @isRoot = 1 then idSoQuyetDinh ELSE CAST(NULL AS UNIQUEIDENTIFIER) END) AS idSoQuyetDinh,
				fTuChi, 
				fHienVat,
				fDuPhong,
				fHangMua,
				fHangNhap,
				fPhanCap,
				fTonKho
			FROM (
			SELECT * FROM #tblChungTuNhanPhanBo
			UNION ALL
			SELECT * FROM #tblDaPhanBo
			) npb
		) vnhanPhanBo
	GROUP BY iID_MLNS, sSoQuyetDinh, idSoQuyetDinh

	-- lấy ra dữ liệu nhận phân bổ (iRowType = 1)
	SELECT * INTO #tblMlnsChildAndSqd FROM #tblMlnsChild, #tblSoQuyetDinhNhan

	SELECT 
		NEWID() AS iID_DTCTChiTiet,
		CAST(0x0 AS uniqueidentIFier) AS iID_DTChungTu,
		c.iID_MLNS,
		c.iID_MLNS_Cha,
		c.sXauNoiMa,
		c.sLNS,
		c.sL,
		c.sK,
		c.sM,
		c.sTM,
		c.sTTM,
		c.sNG,
		c.sTNG,
		c.sTNG1,
		c.sTNG2,
		c.sTNG3,
		c.sMoTa,
		'1' AS bHangCha,
		'' AS iID_MaDonVi,
		'' AS sTenDonVi,
		(CASE WHEN @isRoot = 1 THEN c.sSoQuyetDinh ELSE '' end)AS  sSoQuyetDinh , 
		(CASE WHEN @isRoot = 1 THEN c.idSoQuyetDinh ELSE  CAST(NULL AS UNIQUEIDENTIFIER) END) AS idSoQuyetDinh,
		ISNULL(p.fTonKho, 0) AS fTonKho,
		ISNULL(p.fTuChi, 0) AS fTuChi,
		ISNULL(p.fHienVat, 0) AS fHienVat,
		ISNULL(p.fHangNhap, 0) AS fHangNhap,
		ISNULL(p.fHangMua, 0) AS fHangMua,
		ISNULL(p.fPhanCap, 0) AS fPhanCap,
		ISNULL(p.fDuPhong, 0) AS fDuPhong,
		c.idSoQuyetDinh AS iID_CTDuToan_Nhan,
		'' AS sGhiChu,
		1 AS iRowType,
		CAST(1 AS BIT) AS bHangChaDuToan,
		CAST(0 AS BIT) AS IsEditTuChi,
		CAST(0 AS BIT) AS IsEditHienVat,
		CAST(0 AS BIT) AS IsEditHangNhap,
		CAST(0 AS BIT) AS IsEditHangMua,
		CAST(0 AS BIT) AS IsEditPhanCap,
		CAST(0 AS BIT) AS IsEditDuPhong

	INTO #tblRowNhanPhanBo
	FROM #tblMlnsChildAndSqd c
	LEFT JOIN #tblNhanPhanBo p
	ON c.iID_MLNS = p.iID_MLNS AND c.idSoQuyetDinh = p.idSoQuyetDinh

	-- lấy ra dòng cha từ bảng tmpMlnsParent iRowType = 0
	SELECT 
		NEWID() AS iID_DTCTChiTiet,
		CAST(0x0 AS uniqueidentIFier) AS iID_DTChungTu,
		iID_MLNS,
		iID_MLNS_Cha,
		sXauNoiMa,
		sLNS,
		sL,
		sK,
		sM,
		sTM,
		sTTM,
		sNG,
		sTNG,
		sTNG1,
		sTNG2,
		sTNG3,
		sMoTa,
		bHangCha,
		'' AS iID_MaDonVi,
		'' AS sTenDonVi,
		'' AS sSoQuyetDinh,
		CAST(null AS uniqueidentIFier) AS idSoQuyetDinh,
		0 AS fTonKho,
		0 AS fTuChi,
		0 AS fHienVat,
		0 AS fHangNhap,
		0 AS fHangMua,
		0 AS fPhanCap,
		0 AS fDuPhong,
		cast(0x0 as uniqueidentifier) AS iID_CTDuToan_Nhan,
		'' AS sGhiChu,
		0 AS iRowType,
		bHangChaDuToan,
		CAST(0 AS BIT) AS IsEditTuChi,
		CAST(0 AS BIT) AS IsEditHienVat,
		CAST(0 AS BIT) AS IsEditHangNhap,
		CAST(0 AS BIT) AS IsEditHangMua,
		CAST(0 AS BIT) AS IsEditPhanCap,
		CAST(0 AS BIT) AS IsEditDuPhong
	INTO #tblRowCha
	FROM #tblMlnsParent

	-- lấy ra dòng cON từ bảng tmpMlnsChild và bảng NS_DT_ChungTuChiTiet iRowType = 3
	SELECT * INTO #tblMlnsChildAndDvSqd FROM #tblMlnsChild, #tblDonViPhanBo, #tblSoQuyetDinhNhan

	SELECT 
		ISNULL(ctct.iID_DTCTChiTiet, NEWID()) AS iID_DTCTChiTiet,
		ctct.iID_DTChungTu,
		mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
		mlns.sXauNoiMa,
		mlns.sLNS,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		mlns.sTTM,
		mlns.sNG,
		mlns.sTNG,
		mlns.sTNG1,
		mlns.sTNG2,
		mlns.sTNG3,
		mlns.sMoTa,
		mlns.bHangCha,
		mlns.MaDonVi AS iID_MaDonVi,
		mlns.MaDonVi + ' - ' + mlns.sTenDonVi AS sTenDonVi,
		(CASE WHEN @isRoot = 1 then mlns.sSoQuyetDinh ELSE '' end) AS sSoQuyetDinh ,
		(CASE WHEN @isRoot = 1 then mlns.idSoQuyetDinh ELSE  CAST(NULL AS UNIQUEIDENTIFIER) end) AS idSoQuyetDinh ,
		ISNULL(ctct.fTonKho, 0) AS fTonKho,
		ISNULL(ctct.fTuChi, 0) AS fTuChi,
		ISNULL(ctct.fHienVat, 0) AS fHienVat,
		ISNULL(ctct.fHangNhap, 0) AS fHangNhap,
		ISNULL(ctct.fHangMua, 0) AS fHangMua,
		ISNULL(ctct.fPhanCap, 0) AS fPhanCap,
		ISNULL(ctct.fDuPhong, 0) AS fDuPhong,
		mlns.idSoQuyetDinh AS iID_CTDuToan_Nhan,
		ISNULL(ctct.sGhiChu, '') AS sGhiChu,
		3 AS iRowType,
		mlns.bHangChaDuToan,
		ISNULL(mlns.bTuChi, CAST(0 AS BIT)) AS IsEditTuChi,
		ISNULL(mlns.bHienVat, CAST(0 AS BIT)) AS IsEditHienVat,
		ISNULL(mlns.bHangNhap, CAST(0 AS BIT)) AS IsEditHangNhap,
		ISNULL(mlns.bHangMua, CAST(0 AS BIT)) AS IsEditHangMua,
		ISNULL(mlns.bPhanCap, CAST(0 AS BIT)) AS IsEditPhanCap,
		ISNULL(mlns.bDuPhong, CAST(0 AS BIT)) AS IsEditDuPhong
	INTO #tblRowChiTiet
	FROM #tblMlnsChildAndDvSqd mlns
	LEFT JOIN
	(SELECT *
		FROM NS_DT_ChungTuChiTiet
		WHERE iID_DTChungTu = @sVoucherId) ctct 
	ON mlns.iID_MLNS = ctct.iID_MLNS AND mlns.MaDonVi = ctct.iID_MaDonVi 
	AND (mlns.idSoQuyetDinh = ctct.iID_CTDuToan_Nhan AND ctct.iID_CTDuToan_Nhan IS NOT NULL)

	-- tính dữ liệu đã phân bổ
	SELECT 
		iID_MLNS, sSoQuyetDinh, idSoQuyetDinh,
		SUM(fTonKho) AS fTonKho,
		SUM(fTuChi) AS fTuChi,
		SUM(fHienVat) AS fHienVat,
		SUM(fHangNhap) AS fHangNhap,
		SUM(fHangMua) AS fHangMua,
		SUM(fPhanCap) AS fPhanCap,
		SUM(fDuPhong) AS fDuPhong
	INTO #tblTongRowChiTiet
	FROM (SELECT * FROM #tblRowChiTiet WHERE fTonKho <> 0 OR fTuChi <> 0 OR fHienVat <> 0 OR fHangNhap <> 0 OR fHangMua <> 0 OR fPhanCap <> 0 OR fDuPhong <> 0) ct
	GROUP BY iID_MLNS, sSoQuyetDinh, idSoQuyetDinh
	
	SELECT 
		NEWID() AS iID_DTCTChiTiet,
		CAST(0x0 AS uniqueidentIFier) AS iID_DTChungTu,
		npb.iID_MLNS,
		npb.iID_MLNS_Cha,
		npb.sXauNoiMa,
		npb.sLNS,
		npb.sL,
		npb.sK,
		npb.sM,
		npb.sTM,
		npb.sTTM,
		npb.sNG,
		npb.sTNG,
		npb.sTNG1,
		npb.sTNG2,
		npb.sTNG3,
		N'Số chưa phân bổ ' AS sMoTa,
		'1' AS bHangCha,
		'' AS iID_MaDonVi,
		'' AS sTenDonVi,
		--npb.sSoQuyetDinh,
		(CASE WHEN @isRoot=1 then npb.sSoQuyetDinh ELSE '' end)AS  sSoQuyetDinh,
		(CASE WHEN @isRoot=1 then npb.idSoQuyetDinh ELSE  CAST(NULL AS UNIQUEIDENTIFIER) end)AS  idSoQuyetDinh,
		npb.fTonKho - ISNULL(rct.fTonKho, 0) AS fTonKho,
		npb.fTuChi - ISNULL(rct.fTuChi, 0) AS fTuChi,
		npb.fHienVat - ISNULL(rct.fHienVat, 0) AS fHienVat,
		npb.fHangNhap - ISNULL(rct.fHangNhap, 0) AS fHangNhap,
		npb.fHangMua - ISNULL(rct.fHangMua, 0) AS fHangMua,
		npb.fPhanCap - ISNULL(rct.fPhanCap, 0) AS fPhanCap,
		npb.fDuPhong - ISNULL(rct.fDuPhong, 0) AS fDuPhong,
		npb.iID_CTDuToan_Nhan AS iID_CTDuToan_Nhan,
		'' AS sGhiChu,
		2 AS iRowType,
		CAST(1 AS BIT) AS bHangChaDuToan,
		CAST(0 AS BIT) AS IsEditTuChi,
		CAST(0 AS BIT) AS IsEditHienVat,
		CAST(0 AS BIT) AS IsEditHangNhap,
		CAST(0 AS BIT) AS IsEditHangMua,
		CAST(0 AS BIT) AS IsEditPhanCap,
		CAST(0 AS BIT) AS IsEditDuPhong
	INTO #tblRowConLai
	FROM #tblRowNhanPhanBo npb
	LEFT JOIN #tblTongRowChiTiet rct
	ON npb.iID_MLNS = rct.iID_MLNS 
	AND npb.idSoQuyetDinh = rct.idSoQuyetDinh
	WHERE @isRoot = 1

	--SELECT * FROM #tblRowNhanPhanBo; 
	--SELECT * FROM  tblDonViPhanBo; 
	--SELECT * FROM  tblSoQuyetDinhNhan;
	--SELECT * FROM tblMlnsChildAndDvSqd
	--SELECT * FROM tblTongRowChiTieT;
	--SELECT * FROM tblRowNhanPhanBo;

	SELECT * FROM #tblRowCha
	UNION ALL 
	SELECT * FROM #tblRowNhanPhanBo
	UNION ALL 
	SELECT * FROM #tblRowConLai
	UNION ALL 
	SELECT * FROM #tblRowChiTiet 
	ORDER BY sXauNoiMa
	
END

;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_nhan_phan_bo_du_toan_chi_tiet_so_quyet_dinh_clone]    Script Date: 25/10/2023 2:20:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_nhan_phan_bo_du_toan_chi_tiet_so_quyet_dinh_clone]
	@ChungTuId nvarchar(max),
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@VoucherDate datetime,
	@VoucherIndex int,
	@IsLuyKe bit
AS
BEGIN
	DECLARE @tblDuToan table (iID_MLNS uniqueidentifier, fTuChi float, fHienVat float, fDuPhong float, fHangMua float, fHangNhap float, fPhanCap float, fTonKho float)
	-- số liệu dự toán
	IF @IsLuyKe = 0
		INSERT INTO @tblDuToan
		SELECT 
			iID_MLNS,
			SUM(fTuChi) AS fTuChi, 
			SUM(fHienVat) AS fHienVat,
			SUM(fDuPhong) AS fDuPhong,
			SUM(fHangMua) AS fHangMua,
			SUM(fHangNhap) AS fHangNhap,
			SUM(fPhanCap) AS fPhanCap,
			SUM(fTonKho) AS fTonKho
		FROM (
			-- nhận dự toán
			SELECT iID_MLNS,
				SUM(fTuChi) AS fTuChi, 
				SUM(fHienVat) AS fHienVat,
				SUM(fDuPhong) AS fDuPhong,
				SUM(fHangMua) AS fHangMua,
				SUM(fHangNhap) AS fHangNhap,
				SUM(fPhanCap) AS fPhanCap,
				SUM(fTonKho) AS fTonKho
			FROM NS_DT_ChungTuChiTiet
			WHERE iNamLamViec = @YearOfWork
				AND iNamNganSach = @YearOfBudget
				AND iID_MaNguonNganSach = @BudgetSource
				AND iID_DTChungTu in
					(SELECT DISTINCT iID_CTDuToan_Nhan
					FROM NS_DT_Nhan_PhanBo_Map
					WHERE iID_CTDuToan_PhanBo in (select * from f_split(@ChungTuId)))
				AND iDuLieuNhan = 0 
			group by iID_MLNS
			--UNION  
			---- phân bổ
			--SELECT 
			--	ctct.iID_MLNS, 
			--	0 - SUM(fTuChi) AS fTuChi, 
			--	0 - SUM(fHienVat) AS fHienVat,
			--	0 - SUM(fDuPhong) AS fDuPhong,
			--	0 - SUM(fHangMua) AS fHangMua,
			--	0 - SUM(fHangNhap) AS fHangNhap,
			--	0 - SUM(fPhanCap) AS fPhanCap,
			--	0 - SUM(fTonKho) AS fTonKho
			--FROM NS_DT_ChungTuChiTiet ctct
			--WHERE iID_DTChungTu IN 
			--	(SELECT iID_DTChungTu
			--	FROM NS_DT_ChungTu
			--	WHERE iNamLamViec = @YearOfWork
			--		and iNamNganSach = @YearOfBudget
			--		and iID_MaNguonNganSach = @BudgetSource
			--		and iLoai = 1
			--		AND 
			--		(
			--			(dNgayQuyetDinh IS NOT NULL AND (CAST(dNgayQuyetDinh AS DATE) <= cast(@VoucherDate AS DATE) AND iSoChungTuIndex < @VoucherIndex))
			--			OR 
			--			(dNgayQuyetDinh IS NULL AND (CAST(dNgayChungTu AS DATE) <= cast(@VoucherDate AS DATE) AND iSoChungTuIndex < @VoucherIndex))
			--		)
			--	)
			--GROUP BY ctct.iID_MLNS
		) dt
		GROUP BY iID_MLNS
	ELSE
		INSERT INTO @tblDuToan
		SELECT iID_MLNS,
			SUM(fTuChi) AS fTuChi, 
			SUM(fHienVat) AS fHienVat,
			SUM(fDuPhong) AS fDuPhong,
			SUM(fHangMua) AS fHangMua,
			SUM(fHangNhap) AS fHangNhap,
			SUM(fPhanCap) AS fPhanCap,
			SUM(fTonKho) AS fTonKho
		FROM NS_DT_ChungTuChiTiet
		WHERE 
			iID_DTChungTu IN 
			(
				SELECT iID_DTChungTu FROM NS_DT_ChungTu
				WHERE iNamLamViec = @YearOfWork
				AND iNamNganSach = @YearOfBudget
				AND iID_MaNguonNganSach = @BudgetSource
				AND iLoai = 0
				AND ((dNgayQuyetDinh IS NOT NULL AND CAST(dNgayQuyetDinh AS DATE) <= CAST(@VoucherDate AS DATE))
					OR
					(dNgayQuyetDinh IS NULL AND CAST(dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)))
			)
			AND iDuLieuNhan = 0 
		GROUP BY iID_MLNS

	SELECT 
       mlns.iID_MLNS,
       mlns.iID_MLNS_Cha,
       mlns.sXauNoiMa,
       mlns.sLNS,
       mlns.sL,
       mlns.sK,
       mlns.sM,
       mlns.sTM,
       mlns.sTTM,
       mlns.sNG,
       mlns.sTNG,
       mlns.sTNG1,
       mlns.sTNG2,
       mlns.sTNG3,
       mlns.sMoTa,
       mlns.bHangCha,
       isnull(ctct.fHangMua, 0) AS fHangMua,
       isnull(ctct.fHangNhap, 0) AS fHangNhap,
       isnull(ctct.fDuPhong, 0) AS fDuPhong,
       isnull(ctct.fPhanCap, 0) AS fPhanCap,
       isnull(ctct.fTuChi, 0) AS fTuChi,
       isnull(ctct.fHienVat, 0) AS fHienVat,
	   mlns.sChiTietToi,
	   mlns.bHangChaDuToan
	FROM 
	(select * from NS_MucLucNganSach 
		where iNamLamViec = @YearOfWork and iTrangThai = 1 and bHangChaDuToan is not null and 
		sLNS in 
				(
					SELECT 
						DISTINCT VALUE
					FROM 
					(
						SELECT 
							CAST(LEFT(Item, 1) AS nvarchar(10)) sLNS1, 
							CAST(LEFT(Item, 3) AS nvarchar(10)) sLNS3, 
							CAST(LEFT(Item, 5) AS nvarchar(10)) sLNS5, 
							CAST(Item AS nvarchar(10)) sLNS 
						FROM f_split(@LNS)
					) sLNS
					UNPIVOT
					(
					  value
					  FOR col in (sLNS1, sLNS3, sLNS5, sLNS)
					) un
				)
	) mlns
	LEFT JOIN @tblDuToan ctct 
		ON mlns.iID_MLNS = ctct.iID_MLNS
		AND (isnull(ctct.fTuChi, 0) > 0
		   OR isnull(ctct.fHienVat, 0) > 0
		   OR isnull(ctct.fPhanCap, 0) > 0
		   OR isnull(ctct.fHangNhap, 0) > 0
		   OR isnull(ctct.fHangMua, 0) > 0)


	ORDER BY mlns.sXauNoiMa;
END
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_baocaodquyettoanniendo1]    Script Date: 08/12/2021 2:57:30 PM ******/
SET ANSI_NULLS ON
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt_1]    Script Date: 25/10/2023 2:20:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt_1] 
@Loai varchar(MAX),
@IdDonVi varchar(MAX),
@NamLamViec int, 
@Dvt int,
@LoaiChungTu int 
AS 
BEGIN
SELECT DISTINCT mucluc.iID AS Id,
                mucluc.iID_MLSKT AS IDMucLuc,
                mucluc.sSTT AS STT,
                mucluc.sMoTa AS MoTa,
                mucluc.iID_MLSKTCha AS IdParent,
                mucluc.sSTTBC AS STTBC,
                mucluc.sM AS M,
                CAST(0 AS BIT) AS IsHangCha,
                chitiet.*
FROM NS_SKT_MucLuc mucluc
LEFT JOIN
  (SELECT KyHieu,
          QuyetToan =sum(QuyetToan) / @Dvt,
          DuToan =sum(DuToan) / @Dvt,
          TuChi =sum(fTuChi) / @Dvt,
          TuChi2 =sum(TuChi2) / @Dvt
   FROM
     (SELECT mucluc.sKyHieu AS KyHieu,
             QuyetToan =0,
             DuToan =0,
             fTuChi,
             TuChi2 =0
      FROM NS_SKT_ChungTuChiTiet chitiet
      LEFT JOIN NS_SKT_MucLuc mucluc ON chitiet.sKyHieu = mucluc.sKyHieu AND mucluc.iNamLamViec = @NamLamViec
      WHERE chitiet.iNamLamViec = @NamLamViec
        AND chitiet.iLoai in (select * from f_split(@Loai))
        AND chitiet.iLoaiChungTu = @LoaiChungTu
        AND (@IdDonVi IS NULL
             OR chitiet.iID_MaDonVi in
               (SELECT *
                FROM f_split(@IdDonVi)))
     

      UNION ALL SELECT dbo.f_get_ky_hieu_by_xaunoima(XauNoiMa,@NamLamViec) AS KyHieu,
                       QuyetToan =0,
                       DuToan = 0,
                       TuChi = 0,
                       TuChi2 = TuChi
      FROM f_skt_dutoan(@NamLamViec, @IdDonVi, CAST(@LoaiChungTu AS nvarchar(MAX))) AS a
      WHERE dbo.f_get_ky_hieu_by_xaunoima(XauNoiMa,@NamLamViec) IS NOT NULL ) AS a
   GROUP BY KyHieu --order by Tuchi
 ) chitiet ON mucluc.sKyHieu = chitiet.KyHieu
WHERE chitiet.KyHieu IS NOT NULL
  AND mucluc.iNamLamViec = @NamLamViec
ORDER BY sSTTBC END
;
;
;
;
;
;
GO
