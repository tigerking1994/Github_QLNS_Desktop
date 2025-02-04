/****** Object:  StoredProcedure [dbo].[sp_dt_danhsach_chungtu_chitiet]    Script Date: 30/12/2022 5:46:30 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_danhsach_chungtu_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_danhsach_chungtu_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_chungtu_chitiet]    Script Date: 30/12/2022 3:39:19 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_chungtu_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_chungtu_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_rpt_get_lns1]    Script Date: 30/12/2022 3:39:19 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_rpt_get_lns1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_rpt_get_lns1]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_rpt_get_lns1]    Script Date: 30/12/2022 3:39:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_rpt_get_lns1]
	 @NamLamViec int,
	 @ChungTuId nvarchar(MAX),
	 @NgayQuyetDinh datetime,
	 @Type int
AS
BEGIN 
	SET NOCOUNT ON;
	IF (@Type = 1)
	BEGIN
		SELECT * FROM NS_MucLucNganSach
		WHERE iID_MLNS IN (
			SELECT mlns.iID_MLNS FROM NS_DT_ChungTuChiTiet AS chitiet
			INNER JOIN NS_MucLucNganSach AS mlns 
			ON chitiet.sLNS = mlns.sLNS AND mlns.iNamLamViec = @NamLamViec and mlns.sL = '' 
			INNER JOIN (SELECT * FROM NS_DT_ChungTu 
				WHERE iID_DTChungTu IN (SELECT * FROM f_split(@ChungTuId))
				AND dNgayQuyetDinh <= DATEADD(dd, -1, (DATEADD(mm, 1, @NgayQuyetDinh)))
				AND sDSLNS <> ''
			) chungtu
			ON chitiet.iID_DTChungTu = chungtu.iID_DTChungTu
			AND (chitiet.fTuChi <> 0 OR chitiet.fHienVat <> 0 OR chitiet.fDuPhong <> 0 OR chitiet.fHangMua <> 0 OR chitiet.fHangNhap <> 0 OR chitiet.fPhanCap <> 0)
		)
		AND iNamLamViec = @NamLamViec
	END
	ELSE IF(@Type = 4)
	BEGIN
		SELECT * FROM NS_MucLucNganSach
		WHERE iID_MLNS IN (
			SELECT mlns.iID_MLNS FROM NS_DT_ChungTuChiTiet AS chitiet
			INNER JOIN NS_MucLucNganSach AS mlns 
			ON chitiet.sLNS = mlns.sLNS AND mlns.iNamLamViec = @NamLamViec and mlns.sL = '' 
			INNER JOIN (SELECT * FROM NS_DT_ChungTu 
				WHERE iID_DTChungTu IN (SELECT * FROM f_split(@ChungTuId))
				AND dNgayQuyetDinh <= DATEADD(dd, -1, (DATEADD(mm, 1, @NgayQuyetDinh)))
				AND sDSLNS <> ''
			) chungtu
			ON chitiet.iID_DTChungTu = chungtu.iID_DTChungTu
			AND (chitiet.fTuChi <> 0)
		)
		AND iNamLamViec = @NamLamViec
	END
	ELSE IF(@Type = 5)
	BEGIN
		SELECT * FROM NS_MucLucNganSach
		WHERE iID_MLNS IN (
			SELECT mlns.iID_MLNS FROM NS_DT_ChungTuChiTiet AS chitiet
			INNER JOIN NS_MucLucNganSach AS mlns 
			ON chitiet.sLNS = mlns.sLNS AND mlns.iNamLamViec = @NamLamViec and mlns.sL = '' 
			INNER JOIN (SELECT * FROM NS_DT_ChungTu 
				WHERE iID_DTChungTu IN (SELECT * FROM f_split(@ChungTuId))
				AND dNgayQuyetDinh <= DATEADD(dd, -1, (DATEADD(mm, 1, @NgayQuyetDinh)))
				AND sDSLNS <> ''
			) chungtu
			ON chitiet.iID_DTChungTu = chungtu.iID_DTChungTu
			AND (chitiet.fHienVat <> 0)
		)
		AND iNamLamViec = @NamLamViec
	END
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_chungtu_chitiet]    Script Date: 30/12/2022 3:39:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_qt_chungtu_chitiet]
	-- Add the parameters for the stored procedure here
	@VoucherId nvarchar(255),
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@Type nvarchar(10),
	@AgencyId nvarchar(100),
	@VoucherDate datetime,
	@UserName nvarchar(100)
AS
BEGIN
	SET NOCOUNT ON;
	Declare @YearOfBudgetCurrent int = 2,
		@YearOfBudgetAfter int = 4;
	Declare @voucherIndex int,
	@IsLocked bit,
	@STongHop nvarchar(500);
	SELECT @voucherIndex = iSoChungTuIndex, @IsLocked = bKhoa, @STongHop = sTongHop FROM NS_QT_ChungTu WHERE iID_QTChungTu = @VoucherId;

	DECLARE @CountDonViCha int;
	SELECT 
		@CountDonViCha = count(*) FROM (SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName AND iNamLamViec = @YearOfWork AND iTrangThai = 1) nddv
	INNER JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1 AND iLoai = 0) dv
	ON dv.iID_MaDonVi = nddv.iID_MaDonVi;

	SELECT 
		isnull(ctct.iID_QTCTChiTiet, NEWID()) AS iID_QTCTChiTiet,
		ctct.iID_QTChungTu,
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
		isnull(ctct.iNamNganSach, @YearOfBudget) as iNamNganSach,
		isnull(ctct.iID_MaNguonNganSach, @BudgetSource) as iID_MaNguonNganSach,
		isnull(ctct.iNamLamViec, @YearOfWork) as iNamLamViec,
		isnull(ctct.iThangQuyLoai, 0) as iThangQuyLoai,
		isnull(ctct.iThangQuy, 0) as iThangQuy,
		ctct.iID_MaDonVi,
		dv.sTenDonVi,
		isnull(ctct.fSoNguoi, 0) as fSoNguoi,
		isnull(ctct.fSoNgay, 0) as fSoNgay,
		isNull(ctct.fSoLuot, 0) as fSoLuot,
		isnull(ctct.fTuChi_DeNghi, 0) as fTuChi_DeNghi,
		isnull(ctct.fTuChi_PheDuyet, 0) as fTuChi_PheDuyet,
		ctct.sGhiChu,
		ctct.dNgayTao,
		ctct.dNgaySua,
		ctct.sNguoiTao,
		ctct.sNguoiSua,
		dtctct.DuToan as fDuToan,
		ctctdqt.DaQuyetToan as fDaQuyetToan,
		mlns.sChiTietToi,
		mlns.bHangChaQuyetToan
	FROM 
	(
		select * from NS_MucLucNganSach where iNamLamViec = @YearOfWork and iTrangThai = 1 and bHangChaQuyetToan is not null and
		(
			(@CountDonViCha > 0 and (@IsLocked = 1 or @STongHop is not null) and sLNS in (select * from f_split(@LNS)))
			or
			(
				sLNS in 
				(
					SELECT 
						DISTINCT VALUE
					FROM 
					(
						SELECT 
							CAST(LEFT(sLNS, 1) AS nvarchar(10)) sLNS1, 
							CAST(LEFT(sLNS, 3) AS nvarchar(10)) sLNS3, 
							CAST(LEFT(sLNS, 5) AS nvarchar(10)) sLNS5, 
							CAST(sLNS AS nvarchar(10)) sLNS 
						FROM
							NS_NguoiDung_LNS 
						WHERE 
							sMaNguoiDung = @UserName 
							AND iNamLamViec = @YearOfWork
							AND sLNS IN (SELECT * FROM f_split(@LNS))
					) sLNS
					UNPIVOT
					(
					  value
					  FOR col in (sLNS1, sLNS3, sLNS5, sLNS)
					) un
				)
			)
		)
	) mlns
	LEFT JOIN
		(SELECT 
			* 
		 FROM 
			NS_QT_ChungTuChiTiet 
		 WHERE 
			iID_QTChungTu = @VoucherId
			AND iNamLamViec = @YearOfWork
			AND (@YearOfBudget = @YearOfBudgetCurrent and (iNamNganSach = @YearOfBudgetCurrent or iNamNganSach = @YearOfBudgetAfter) or (iNamNganSach = @YearOfBudget))
			AND iID_MaNguonNganSach = @BudgetSource
		) ctct
	ON mlns.iID_MLNS = ctct.iID_MLNS 
	LEFT JOIN 
		(select 
			(case sLNS1
				when '1040200' then SUM(fHangNhap)
				when '1040300' then SUM(fHangMua)
				else SUM(fTuChi)
			end) as DuToan,
			iID_MLNS1 as iID_MLNS 
			from 
			(
				select 
					case 
						when ctct.sLNS = '1040100' then mlns.sLNS
						else ctct.sLNS
						end 
					as sLNS1,
					case 
						when ctct.sLNS = '1040100' then mlns.iID_MLNS
						else ctct.iID_MLNS
						end
					as iID_MLNS1,
					ctct.*
				from 
				(
					SELECT
					*
					FROM 
						NS_DT_ChungTuChiTiet
					 WHERE
						iID_DTChungTu 
						IN (SELECT iID_DTChungTu FROM NS_DT_ChungTu 
							where 
								((@STongHop IS NULL AND iID_MaDonVi like '%' + @AgencyId + '%') OR (@STongHop IS NOT NULL AND iID_MaDonVi = @AgencyId))
								AND (iLoai = 1 or iLoai = 0)
								AND iNamLamViec = @YearOfWork
								AND iNamNganSach = @YearOfBudget
								AND iID_MaNguonNganSach = @BudgetSource
								AND bKhoa = 1
								AND cast(dNgayQuyetDinh as date) <= cast(@VoucherDate as date)
								AND sSoQuyetDinh is not null AND sSoQuyetDinh <> ''
						)
						and ((@STongHop IS NULL AND iID_MaDonVi = @AgencyId) OR (@STongHop IS NOT NULL AND iID_MaDonVi = @AgencyId))
						and IDuLieuNhan = 0) ctct
					left join 
						(select '1040100' as sLNS1, * from NS_MucLucNganSach where sLNS in ('1040100', '1040200', '1040300') and iNamLamViec = @YearOfWork) mlns
					on mlns.sLNS1 = ctct.sLNS and mlns.sL = ctct.sL and mlns.sK = ctct.sK and mlns.sM = ctct.sM and mlns.sTM = ctct.sTM and mlns.sNG = ctct.sNG and mlns.sTTM = ctct.sTTM
				) dt
				GROUP BY iID_MLNS1, sLNS1
			) dtctct
	on mlns.iID_MLNS = dtctct.iID_MLNS
	LEFT JOIN 
		(SELECT
			SUM(fTuChi_PheDuyet) AS DaQuyetToan,
			iID_MLNS
		 FROM 
			NS_QT_ChungTuChiTiet
		 WHERE
			iID_QTChungTu 
			IN (SELECT iID_QTChungTu FROM NS_QT_ChungTu 
				where 
					iID_MaDonVi = @AgencyId
					AND sLoai = @Type
					AND iNamLamViec = @YearOfWork
					AND (@YearOfBudget = @YearOfBudgetCurrent and (iNamNganSach = @YearOfBudgetCurrent or iNamNganSach = @YearOfBudgetAfter) or (iNamNganSach = @YearOfBudget))
					AND iID_MaNguonNganSach = @BudgetSource
					AND cast(dNgayChungTu as date) <= cast(@voucherDate as Date)
					AND iID_QTChungTu <> @VoucherId
			)
		 GROUP BY iID_MLNS
		) ctctdqt
	ON mlns.iID_MLNS = ctctdqt.iID_MLNS
	LEFT JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1) dv
	ON dv.iID_MaDonVi = ctct.iID_MaDonVi
	WHERE
		mlns.iNamLamViec = @YearOfWork
		AND mlns.iTrangThai = 1
	Order by mlns.sLNS, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.sTNG, mlns.sTNG1, mlns.sTNG2, mlns.sTNG3
END
;
;
;
;
GO

/****** Object:  StoredProcedure [dbo].[sp_dt_danhsach_chungtu_chitiet]    Script Date: 30/12/2022 5:46:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_danhsach_chungtu_chitiet]
	@ChungTuId NVARCHAR(255),
	@LNS NVARCHAR(MAX),
	@IdDonVi NVARCHAR(MAX),
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@UserName NVARCHAR(100)
AS
BEGIN
	DECLARE @sChungTuId NVARCHAR(255) = @ChungTuId;
	DECLARE @sLNS NVARCHAR(MAX) = @LNS;
	DECLARE @sIdDonVi NVARCHAR(MAX) = @IdDonVi;
	DECLARE @iNamLamViec int = @NamLamViec;
	DECLARE @iNamNganSach int = @NamNganSach;
	DECLARE @iNguonNganSach int = @NguonNganSach;
	DECLARE @sUserName NVARCHAR(100) = @UserName;
	DECLARE @CountDonViCha int; 
	DECLARE @isQuanly int;
	SELECT @CountDonViCha = count(*)
	FROM
	  (SELECT *
	   FROM NguoiDung_DonVi
	   WHERE iID_MaNguoiDung = @sUserName
		 AND iNamLamViec = @iNamLamViec
		 AND iTrangThai = 1) nddv
	INNER JOIN
	  (SELECT *
	   FROM DonVi
	   WHERE iNamLamViec = @iNamLamViec
		 AND iTrangThai = 1
		 AND iLoai = 0) dv ON dv.iID_MaDonVi = nddv.iID_MaDonVi;

	DECLARE @dNgayQuyetDinh AS DATETIME;
	DECLARE @dNgayChungTu AS DATETIME;
	DECLARE @dNgayPhanBo AS DATETIME;
	DECLARE @iSoChungTuIndex AS int;
	SELECT 
		@dNgayQuyetDinh = CAST(dNgayQuyetDinh AS date), 
		@dNgayChungTu = CAST(dNgayChungTu AS date),
		@iSoChungTuIndex = iSoChungTuIndex
	FROM NS_DT_ChungTu 
	WHERE iID_DTChungTu = @sChungTuId;

	SELECT  @isQuanly =  COUNT(*) FROM  DonVi inner join  NguoiDung_DonVi  ON  DonVi.iID_MaDonVi =  NguoiDung_DonVi.iID_MaDonVi  
	WHERE  iLoai = 0 and NguoiDung_DonVi.iID_MaNguoiDung = @sUserName and NguoiDung_DonVi.inamlamviec = @iNamLamViec  and DonVi.iNamLamViec= @iNamLamViec;
	IF @isQuanly >0
		SET @isQuanly=1
	ELSE
		SET @isQuanly=0 

	IF @dNgayQuyetDinh IS NULL
		SELECT @dNgayPhanBo = @dNgayChungTu;
	ELSE 
		SELECT @dNgayPhanBo = @dNgayQuyetDinh;

	SELECT DISTINCT sLNS INTO #tblLns FROM NS_DT_ChungTuChiTiet 
	WHERE iID_DTChungTu in (SELECT iID_CTDuToan_Nhan FROM NS_DT_Nhan_PhanBo_Map WHERE iID_CTDuToan_PhanBo = @sChungTuId)

	SELECT iID_MLNS INTO #tblIdMlns FROM NS_DT_ChungTuChiTiet 
	WHERE iID_DTChungTu in (SELECT iID_CTDuToan_Nhan FROM NS_DT_Nhan_PhanBo_Map WHERE iID_CTDuToan_PhanBo = @sChungTuId)


	SELECT * INTO #tblMlns
	FROM NS_MucLucNganSach
	WHERE iNamLamViec = @iNamLamViec
		AND iTrangThai = 1
		AND bHangChaDuToan is not null
		AND ((@CountDonViCha <> 0
			AND sLNS IN
				(SELECT DISTINCT VALUE
					FROM
					(SELECT CAST(LEFT(Item, 1) AS NVARCHAR(10)) LNS1,
							CAST(LEFT(Item, 3) AS NVARCHAR(10)) LNS3,
							CAST(LEFT(Item, 5) AS NVARCHAR(10)) LNS5,
							CAST(Item AS NVARCHAR(10)) LNS
					FROM f_split(@LNS) ) LNS UNPIVOT (value
															FOR col in (LNS1, LNS3, LNS5, LNS)) un))
			OR (@CountDonViCha = 0
				AND sLNS IN
				(SELECT DISTINCT VALUE
					FROM
					(SELECT CAST(LEFT(sLNS, 1) AS NVARCHAR(10)) LNS1,
							CAST(LEFT(sLNS, 3) AS NVARCHAR(10)) LNS3,
							CAST(LEFT(sLNS, 5) AS NVARCHAR(10)) LNS5,
							CAST(sLNS AS NVARCHAR(10)) LNS
					FROM NS_NguoiDung_LNS
					WHERE sMaNguoiDung = @sUserName
						AND iNamLamViec = @iNamLamViec
						AND sLNS IN
						(SELECT *
							FROM f_split(@sLNS)) ) LNS UNPIVOT (value
															FOR col in (LNS1, LNS3, LNS5, LNS)) un)))

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
	WHERE bHangChaDuToan = 0 and iID_MLNS in (SELECT * FROM #tblIdMlns)

	-- lấy ra đơn vị được chọn để phân bổ theo iID_ChungTu phân bổ
	SELECT iID_MaDonVi AS MaDonVi, sTenDonVi
	INTO #tblDonViPhanBo
	FROM DonVi
	WHERE iNamLamViec = @iNamLamViec
		AND iID_MaDonVi IN (SELECT * FROM f_split(@sIdDonVi))
	-- lấy ra số quyết định nhận được chọn để phân bổ theo iID_ChungTu phân bổ
	SELECT 
		sSoQuyetDinh, 
		iID_DTChungTu AS idSoQuyetDinh 
	INTO #tblSoQuyetDinhNhan
	FROM NS_DT_ChungTu 
	WHERE iID_DTChungTu IN (
		SELECT iID_CTDuToan_Nhan 
		FROM NS_DT_Nhan_PhanBo_Map 
		WHERE iID_CTDuToan_PhanBo = @sChungTuId
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
				(CASE WHEN @isQuanly=1 then dt.sSoQuyetDinh ELSE '' end)AS  sSoQuyetDinh ,  --dt.sSoQuyetDinh, 
				(CASE WHEN @isQuanly=1 then dt.iID_DTChungTu ELSE CAST(NULL AS UNIQUEIDENTIFIER) end)AS  idSoQuyetDinh,  --dt.iID_DTChungTu AS idSoQuyetDinh,
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
			WHERE dt.iID_DTChungTu IN ( SELECT iID_CTDuToan_Nhan FROM NS_DT_Nhan_PhanBo_Map WHERE iID_CTDuToan_PhanBo = @sChungTuId) 
	) vctpb
	GROUP BY vctpb.iID_MLNS,vctpb.sSoQuyetDinh, vctpb.idSoQuyetDinh

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
				(CASE WHEN @isQuanly=1 then ct.sSoQuyetDinh ELSE '' end)AS  sSoQuyetDinh ,  --ct.sSoQuyetDinh, 
				(CASE WHEN @isQuanly=1 then ct.iID_DTChungTu ELSE CAST(NULL AS UNIQUEIDENTIFIER) end)AS  idSoQuyetDinh,  --ct.iID_DTChungTu AS idSoQuyetDinh,
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
			WHERE (iID_CTDuToan_Nhan <> '' and iID_CTDuToan_Nhan IN (SELECT idSoQuyetDinh FROM #tblSoQuyetDinhNhan))
			AND (
				(dtct.dNgayQuyetDinh IS NOT NULL 
				AND 
				(
					(CAST(dtct.dNgayQuyetDinh AS DATE) = CAST(@dNgayPhanBo AS DATE) AND dtct.iSoChungTuIndex < @iSoChungTuIndex)
					OR 
					(CAST(dtct.dNgayQuyetDinh AS DATE) < CAST(@dNgayPhanBo AS DATE))
				))
				OR 
				(dtct.dNgayQuyetDinh IS NULL 
				AND 
				(
					CAST(dtct.dNgayChungTu AS DATE) <= CAST(@dNgayPhanBo AS DATE) AND dtct.iSoChungTuIndex < @iSoChungTuIndex)
					OR 
					(CAST(dtct.dNgayChungTu AS DATE) < CAST(@dNgayPhanBo AS DATE))
				)
			)
			AND ctct.iID_DTChungTu <> @sChungTuId
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
				iID_MLNS, --sSoQuyetDinh, idSoQuyetDinh,
					
				(CASE WHEN @isQuanly=1 then sSoQuyetDinh ELSE '' end)AS  sSoQuyetDinh ,  --ct.sSoQuyetDinh, 
				(CASE WHEN @isQuanly=1 then idSoQuyetDinh ELSE CAST(NULL AS UNIQUEIDENTIFIER) END) AS  idSoQuyetDinh,  --ct.iID_DTChungTu AS idSoQuyetDinh,
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
		--c.sSoQuyetDinh,
		(CASE WHEN @isQuanly=1 then c.sSoQuyetDinh ELSE '' end)AS  sSoQuyetDinh ,  --c.sSoQuyetDinh, 
		(CASE WHEN @isQuanly=1 then c.idSoQuyetDinh ELSE  CAST(NULL AS UNIQUEIDENTIFIER) end)AS  idSoQuyetDinh ,  --c.sSoQuyetDinh, 
		--idSoQuyetDinh

		ISNULL(p.fTonKho, 0) AS fTonKho,
		ISNULL(p.fTuChi, 0) AS fTuChi,
		ISNULL(p.fHienVat, 0) AS fHienVat,
		ISNULL(p.fHangNhap, 0) AS fHangNhap,
		ISNULL(p.fHangMua, 0) AS fHangMua,
		ISNULL(p.fPhanCap, 0) AS fPhanCap,
		ISNULL(p.fDuPhong, 0) AS fDuPhong,
		CAST(c.idSoQuyetDinh AS NVARCHAR(MAX)) AS iID_CTDuToan_Nhan,
		'' AS sGhiChu,
		1 AS iRowType,
		CAST(1 AS bit) AS bHangChaDuToan,
		CAST(0 AS bit) AS IsEditTuChi,
		CAST(0 AS bit) AS IsEditHienVat,
		CAST(0 AS bit) AS IsEditHangNhap,
		CAST(0 AS bit) AS IsEditHangMua,
		CAST(0 AS bit) AS IsEditPhanCap,
		CAST(0 AS bit) AS IsEditDuPhong
	INTO #tblRowNhanPhanBo
	FROM #tblMlnsChildAndSqd c
	LEFT JOIN #tblNhanPhanBo p
	ON c.iID_MLNS = p.iID_MLNS and c.idSoQuyetDinh = p.idSoQuyetDinh

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
		'' AS iID_CTDuToan_Nhan,
		'' AS sGhiChu,
		0 AS iRowType,
		bHangChaDuToan,
		CAST(0 AS bit) AS IsEditTuChi,
		CAST(0 AS bit) AS IsEditHienVat,
		CAST(0 AS bit) AS IsEditHangNhap,
		CAST(0 AS bit) AS IsEditHangMua,
		CAST(0 AS bit) AS IsEditPhanCap,
		CAST(0 AS bit) AS IsEditDuPhong
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
		--mlns.sSoQuyetDinh,
		(CASE WHEN @isQuanly=1 then mlns.sSoQuyetDinh ELSE '' end)AS  sSoQuyetDinh ,
		(CASE WHEN @isQuanly=1 then mlns.idSoQuyetDinh ELSE  CAST(NULL AS UNIQUEIDENTIFIER) end)AS  idSoQuyetDinh ,
			
		ISNULL(ctct.fTonKho, 0) AS fTonKho,
		ISNULL(ctct.fTuChi, 0) AS fTuChi,
		ISNULL(ctct.fHienVat, 0) AS fHienVat,
		ISNULL(ctct.fHangNhap, 0) AS fHangNhap,
		ISNULL(ctct.fHangMua, 0) AS fHangMua,
		ISNULL(ctct.fPhanCap, 0) AS fPhanCap,
		ISNULL(ctct.fDuPhong, 0) AS fDuPhong,
		CAST(mlns.idSoQuyetDinh AS NVARCHAR(MAX)) AS iID_CTDuToan_Nhan,
		ISNULL(ctct.sGhiChu, '') AS sGhiChu,
		3 AS iRowType,
		mlns.bHangChaDuToan,
		ISNULL(mlns.bTuChi, CAST(0 AS bit)) AS IsEditTuChi,
		ISNULL(mlns.bHienVat, CAST(0 AS bit)) AS IsEditHienVat,
		ISNULL(mlns.bHangNhap, CAST(0 AS bit)) AS IsEditHangNhap,
		ISNULL(mlns.bHangMua, CAST(0 AS bit)) AS IsEditHangMua,
		ISNULL(mlns.bPhanCap, CAST(0 AS bit)) AS IsEditPhanCap,
		ISNULL(mlns.bDuPhong, CAST(0 AS bit)) AS IsEditDuPhong
	INTO #tblRowChiTiet
	FROM #tblMlnsChildAndDvSqd mlns
	LEFT JOIN
	(SELECT *
		FROM NS_DT_ChungTuChiTiet
		WHERE iID_DTChungTu = @sChungTuId) ctct 
	ON mlns.iID_MLNS = ctct.iID_MLNS and mlns.MaDonVi = ctct.iID_MaDonVi AND (mlns.idSoQuyetDinh = ctct.iID_CTDuToan_Nhan and ctct.iID_CTDuToan_Nhan <> '')

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
	FROM (SELECT * FROM #tblRowChiTiet WHERE fTonKho <> 0 or fTuChi <> 0 or fHienVat <> 0 or fHangNhap <> 0 or fHangMua <> 0 or fPhanCap <> 0 or fDuPhong <> 0) ct
	group by iID_MLNS, sSoQuyetDinh, idSoQuyetDinh
	
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
		(CASE WHEN @isQuanly=1 then npb.sSoQuyetDinh ELSE '' end)AS  sSoQuyetDinh,
		(CASE WHEN @isQuanly=1 then npb.idSoQuyetDinh ELSE  CAST(NULL AS UNIQUEIDENTIFIER) end)AS  idSoQuyetDinh,
		npb.fTonKho - ISNULL(rct.fTonKho, 0) AS fTonKho,
		npb.fTuChi - ISNULL(rct.fTuChi, 0) AS fTuChi,
		npb.fHienVat - ISNULL(rct.fHienVat, 0) AS fHienVat,
		npb.fHangNhap - ISNULL(rct.fHangNhap, 0) AS fHangNhap,
		npb.fHangMua - ISNULL(rct.fHangMua, 0) AS fHangMua,
		npb.fPhanCap - ISNULL(rct.fPhanCap, 0) AS fPhanCap,
		npb.fDuPhong - ISNULL(rct.fDuPhong, 0) AS fDuPhong,
		CAST(npb.iID_CTDuToan_Nhan AS NVARCHAR(MAX)) AS iID_CTDuToan_Nhan,
		'' AS sGhiChu,
		2 AS iRowType,
		CAST(1 AS bit) AS bHangChaDuToan,
		CAST(0 AS bit) AS IsEditTuChi,
		CAST(0 AS bit) AS IsEditHienVat,
		CAST(0 AS bit) AS IsEditHangNhap,
		CAST(0 AS bit) AS IsEditHangMua,
		CAST(0 AS bit) AS IsEditPhanCap,
		CAST(0 AS bit) AS IsEditDuPhong
	INTO #tblRowConLai
	FROM #tblRowNhanPhanBo npb
	LEFT JOIN #tblTongRowChiTiet rct
	ON npb.iID_MLNS = rct.iID_MLNS 
	and npb.idSoQuyetDinh = rct.idSoQuyetDinh
		
	WHERE 1 = @isQuanly

	--SELECT * FROM tblMlnsChild; 
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
/****** Object:  StoredProcedure [dbo].[sp_dt_danhsach_chungtu_chitiet_dieuchinh]    Script Date: 17/12/2021 8:08:45 AM ******/
SET ANSI_NULLS ON
;
;
GO
