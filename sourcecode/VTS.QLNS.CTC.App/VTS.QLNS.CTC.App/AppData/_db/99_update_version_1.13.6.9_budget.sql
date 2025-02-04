UPDATE NS_QS_ChungTuChiTiet
SET fSoLDHD = fSoLDHD + fSoCNVQP
GO

INSERT [dbo].[DanhMuc] ([iID_DanhMuc], [dNgaySua], [dNgayTao], [iID_MaDanhMuc], [iNamLamViec], [iThuTu], [iTrangThai], [Log], [NganSachNganh], [sGiaTri], [sMoTa], [sNguoiSua], [sNguoiTao], [sTen], [sType], [Tag]) VALUES (NEWID(), NULL, NULL, N'3', NULL, 999, 1, NULL, NULL, NULL, NULL, NULL, NULL, N'3 - Năm trước chuyển sang', N'NS_NamNganSach', NULL)
GO

/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_tonghop_quy_donvi]    Script Date: 19/12/2023 5:12:30 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_tonghop_quy_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_tonghop_quy_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_chungtu_tonghop_2_year]    Script Date: 19/12/2023 5:03:51 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_chungtu_tonghop_2_year]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_chungtu_tonghop_2_year]
GO
/****** Object:  UserDefinedFunction [dbo].[f_quyettoan_tonghop_contain]    Script Date: 19/12/2023 5:03:51 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_quyettoan_tonghop_contain]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_quyettoan_tonghop_contain]
GO
/****** Object:  UserDefinedFunction [dbo].[f_dutoan_tonghop_contain]    Script Date: 19/12/2023 5:03:51 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_dutoan_tonghop_contain]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_dutoan_tonghop_contain]
GO
/****** Object:  UserDefinedFunction [dbo].[f_dutoan_tonghop_contain]    Script Date: 19/12/2023 5:03:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[f_dutoan_tonghop_contain]
(    
      @NamLamViec int,
	  @NamNganSach nvarchar(100),
	  @NguonNganSach nvarchar(20),
	  @NgayChungTu datetime,
	  @IdDonVi nvarchar(max),
	  @lns nvarchar(max)
)
RETURNS TABLE
AS RETURN

	SELECT iID_MLNS,
		   iID_MLNS_Cha,
		   iID_MaDonVi,
		   TuChi =sum(fTuChi),
		   HienVat =sum(fHienVat)
	FROM NS_DT_ChungTuChiTiet
	WHERE iNamLamViec = @NamLamViec
	  AND iDuLieuNhan = 0
	  AND (@NamNganSach IS NULL OR iNamNganSach in (select * from f_split(@NamNganSach)))
	  AND (@NguonNganSach IS NULL OR iID_MaNguonNganSach = @NguonNganSach)
	  AND (@IdDonVi IS NULL
		   OR iID_MaDonVi in (SELECT * FROM f_split(@IdDonVi)))
	  AND (@lns IS NULL
		   OR sLNS in (SELECT * FROM f_split(@lns)))
	  AND iID_DTChungTu in
		(SELECT iID_DTChungTu
		 FROM NS_DT_ChungTu
		 WHERE iNamLamViec = @NamLamViec
		   AND (@NamNganSach IS NULL OR iNamNganSach in (select * from f_split(@NamNganSach)))
		   AND (@NguonNganSach IS NULL OR iID_MaNguonNganSach = @NguonNganSach)
		   AND CAST(dNgayQuyetDinh AS DATE) <= @NgayChungTu)
	GROUP BY iID_MLNS, iID_MLNS_Cha, iID_MaDonVi;
;
;
;
GO
/****** Object:  UserDefinedFunction [dbo].[f_quyettoan_tonghop_contain]    Script Date: 19/12/2023 5:03:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[f_quyettoan_tonghop_contain]
(    
      @NamLamViec int,
	  @NamNganSach nvarchar(20),
	  @NguonNganSach nvarchar(20),
	  @ithangquy nvarchar(100),
	  @IdDonVi nvarchar(max),
	  @lns nvarchar(max)
	  )
RETURNS TABLE
AS RETURN

	SELECT iID_MLNS,
		   iID_MLNS_Cha,
		   iID_MaDonVi,
		   TuChi = Sum(fTuChi_PheDuyet),
		   cast(0 AS float) AS HienVat,
		   SoNguoi = Sum(fSoNguoi),
		   SoNgay = Sum(fSoNgay),
		   SoLuot = Sum(fSoLuot)
	FROM NS_QT_ChungTuChiTiet
	WHERE iNamLamViec=@NamLamViec
	  AND (@NamNganSach IS NULL OR iNamNganSach in (select * from f_split(@NamNganSach)))
	  AND (@NguonNganSach IS NULL OR iID_MaNguonNganSach = @NguonNganSach)
	  AND (@IdDonVi = ''
		   OR iID_MaDonVi in (SELECT * FROM f_split(@IdDonVi)))
	  AND (@lns IS NULL
		   OR sLNS in (SELECT * FROM f_split(@lns)))
	  AND iID_QTChungTu IN 
		(
			SELECT iID_QTChungTu 
			FROM NS_QT_ChungTu
			where iThangQuy in (select * from f_split(@ithangquy))
		)
	GROUP BY iID_MLNS, iID_MLNS_Cha, iID_MaDonVi
	HAVING sum(fTuChi_PheDuyet) <> 0;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_chungtu_tonghop_2_year]    Script Date: 19/12/2023 5:03:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_qt_rpt_chungtu_tonghop_2_year]
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget nvarchar(100),
	@BudgetSource int,
	@EstimateAgencyId nvarchar(max),
	@AgencyId nvarchar(max),
	@VoucherDate datetime,
	@QuarterMonth nvarchar(100),
	@QuarterMonthBefore nvarchar(100),
	@Dvt int
AS
BEGIN
	SELECT iID_MLNS,
		   ChiTieu = SUM(ChiTieu) / @Dvt,
		   TuChi2 = SUM(TuChi2) / @Dvt,
		   TuChi = SUM(TuChi) / @Dvt,
		   HienVat = SUM(HienVat) / @Dvt,
		   SoNguoi = SUM(SoNguoi),
		   SoNgay = SUM(SoNgay),
		   SoLuot = SUM(SoLuot)
		   into #tblData
	FROM
	  (-- dự toán
	  SELECT iID_MLNS,
			ChiTieu = TuChi,
			TuChi2 = 0,
			TuChi = 0,
			HienVat = 0,
			SoNguoi = 0,
			SoNgay = 0,
			SoLuot = 0
	   FROM f_dutoan_tonghop_contain(@YearOfWork, @YearOfBudget, @BudgetSource, @VoucherDate, @EstimateAgencyId, @lns) 

	   --so da quyết toán
	   UNION ALL 
	   SELECT iID_MLNS,
			ChiTieu = 0,
			TuChi2 = TuChi,
			TuChi = 0,
			HienVat = 0,
			SoNguoi = 0,
			SoNgay = 0,
			SoLuot = 0
	   FROM f_quyettoan_tonghop_contain(@YearOfWork, @YearOfBudget, @BudgetSource, @QuarterMonthBefore, @AgencyId, @lns) 
	   -- quyet toan dot nay
	   UNION ALL 
	   SELECT iID_MLNS,
			ChiTieu = 0,
			TuChi2 = 0,
			TuChi,
			HienVat,
			SoNguoi,
			SoNgay,
			SoLuot
	   FROM f_quyettoan_tonghop_contain(@YearOfWork, @YearOfBudget, @BudgetSource, @QuarterMonth, @AgencyId, @lns)
	) AS ct
	GROUP BY iID_MLNS;

	SELECT mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
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
		mlns.sXauNoiMa,
		mlns.sMoTa,
		mlns.bHangCha as IsHangCha,
		isnull(dt.ChiTieu, 0) as ChiTieu,
		isnull(dt.TuChi2, 0) as TuChi2,
		isnull(dt.TuChi, 0) as TuChi,
		isnull(dt.TuChi2, 0) + isnull(dt.TuChi, 0) as ThucChi,
		isnull(dt.HienVat, 0) as HienVat,
		isnull(dt.SoNguoi, 0) as SoNguoi,
		isnull(dt.SoNgay, 0) as SoNgay,
		isnull(dt.SoLuot, 0) as SoLuot
	FROM (
		SELECT *
		   FROM NS_MucLucNganSach
		   WHERE iTrangThai=1
			 AND iNamLamViec = @YearOfWork
			 AND sLNS in
				(SELECT DISTINCT VALUE
					FROM
					(SELECT CAST(LEFT(sLNS, 1) AS nvarchar(10)) LNS1,
							CAST(LEFT(sLNS, 3) AS nvarchar(10)) LNS3,
							CAST(LEFT(sLNS, 5) AS nvarchar(10)) LNS5,
							CAST(sLNS AS nvarchar(10)) LNS
					FROM f_split(@LNS)) LNS UNPIVOT (value FOR col in (LNS1, LNS3, LNS5, LNS)) un)) mlns
	LEFT JOIN #tblData dt
	ON dt.iID_MLNS = mlns.iID_MLNS
	WHERE bHangCha = 1 OR (bHangCha = 0 AND (dt.TuChi <> 0 OR dt.ChiTieu <> 0 OR dt.TuChi2 <> 0 OR dt.HienVat <> 0 OR dt.SoNgay <> 0 OR dt.SoNguoi <> 0 OR dt.SoLuot <> 0))
	ORDER BY mlns.sXauNoiMa
	DROP TABLE #tblData;
END
;
;
;
;
;
GO

/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_tonghop_quy_donvi]    Script Date: 19/12/2023 5:12:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_qt_rpt_tonghop_quy_donvi]
	@YearOfWork int,
	@YearOfBudget nvarchar(100),
	@BudgetSource int,
	@QuarterMonth nvarchar(100),
	@Khoi nvarchar(10),
	@LoaiQuyetToan nvarchar(100),
	@UserName nvarchar(100)
AS
BEGIN
	DECLARE @CountDonViCha int;
	DECLARE @tblIdChungTuQT table (iID_QTChungTu uniqueidentifier)
	DECLARE @tblIdChungTuDT table (iID_DTChungTu uniqueidentifier)
	SELECT 
		@CountDonViCha = count(*) FROM (SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName AND iNamLamViec = @YearOfWork AND iTrangThai = 1) nddv
	INNER JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1 AND iLoai = 0) dv
	ON dv.iID_MaDonVi = nddv.iID_MaDonVi;

	IF @CountDonViCha = 0
		INSERT INTO @tblIdChungTuQT (iID_QTChungTu)
		SELECT ct.iID_QTChungTu
		FROM 
			(SELECT 
				* 
			FROM 
				NS_QT_ChungTu 
			WHERE 
				sLoai in (select * from f_split(@LoaiQuyetToan))
				AND iNamNganSach in (select * from f_split(@YearOfBudget))
				AND iNamLamViec = @YearOfWork
				AND iID_MaNguonNganSach = @BudgetSource
				AND (@QuarterMonth IS NULL OR iThangQuy in (SELECT * FROM f_split(@QuarterMonth)))
				AND EXISTS (SELECT * from f_split(sDSLNS) INTERSECT SELECT sLNS FROM NS_NguoiDung_LNS WHERE sMaNguoiDung = @UserName and iNamLamViec = @YearOfWork)
			) ct
		INNER JOIN 
			(SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName and iNamLamViec = @YearOfWork and iTrangThai = 1) nddv
		ON ct.iID_MaDonVi = nddv.iID_MaDonVi;
	ELSE
		INSERT INTO @tblIdChungTuQT (iID_QTChungTu)
		SELECT ct.iID_QTChungTu
		FROM 
			(SELECT 
				* 
			FROM 
				NS_QT_ChungTu 
			WHERE 
				sLoai in (select * from f_split(@LoaiQuyetToan))
				AND iNamNganSach in (select * from f_split(@YearOfBudget))
				AND iNamLamViec = @YearOfWork
				AND iID_MaNguonNganSach = @BudgetSource
				AND (@QuarterMonth IS NULL OR iThangQuy in (SELECT * FROM f_split(@QuarterMonth)))
				AND EXISTS (SELECT * from f_split(sDSLNS) INTERSECT SELECT sLNS FROM NS_NguoiDung_LNS WHERE sMaNguoiDung = @UserName and iNamLamViec = @YearOfWork)
			) ct
		LEFT JOIN 
			(SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName and iNamLamViec = @YearOfWork and iTrangThai = 1) nddv
		ON ct.iID_MaDonVi = nddv.iID_MaDonVi
		WHERE ((ct.iID_MaDonVi IS NULL OR ct.sNguoiTao <> @UserName) AND ct.bKhoa = 1) OR (ct.sNguoiTao = @UserName);

	INSERT INTO @tblIdChungTuDT (iID_DTChungTu)
	SELECT iID_DTChungTu
	FROM NS_DT_ChungTu
	WHERE iLoai in (0, 1, 2)
	  AND iNamNganSach in (select * from f_split(@YearOfBudget))
	  AND iNamLamViec = @YearOfWork
	  AND iID_MaNguonNganSach = @BudgetSource
	  AND ((@CountDonViCha = 0
			AND EXISTS
			  (SELECT *
			   FROM f_split(sDSLNS) INTERSECT SELECT sLNS
			   FROM NS_NguoiDung_LNS
			   WHERE sMaNguoiDung = @UserName
				 AND iNamLamViec = @YearOfWork)
			OR (@CountDonViCha <> 0)))
	  AND ((@CountDonViCha = 0
			AND EXISTS
			  (SELECT *
			   FROM f_split(sDSID_MaDonVi) INTERSECT SELECT iID_MaDonVi
			   FROM NguoiDung_DonVi
			   WHERE iID_MaNguoiDung = @UserName
				 AND iNamLamViec = @YearOfWork
				 AND iTrangThai = 1)
			OR (@CountDonViCha <> 0)))
	  AND ((@CountDonViCha = 0
			AND bKhoa = 1)
		   OR (@CountDonViCha <> 0))
	ORDER BY sSoChungTu

	SELECT distinct dv.* 
	FROM 
		(SELECT * 
		FROM DonVi 
		WHERE iNamLamViec = @YearOfWork 
			AND ((((@Khoi <> '' AND iKhoi IN (SELECT * FROM f_split(@Khoi))) OR @Khoi = '')
				AND iLoai = 1)
			Or iLoai = 0)) dv
	INNER JOIN 
		(SELECT distinct iID_MaDonVi 
		FROM NS_QT_ChungTuChiTiet
		WHERE
			(fTuChi_DeNghi <> 0 OR fTuChi_PheDuyet <> 0)
			AND iID_QTChungTu IN (SELECT * FROM @tblIdChungTuQT)
		UNION 
		SELECT distinct iID_MaDonVi 
		FROM NS_DT_ChungTuChiTiet
		WHERE
			fTuChi <> 0
			AND iID_DTChungTu IN (SELECT * FROM @tblIdChungTuDT)
			AND (
					(@LoaiQuyetToan = '101' AND sLNS like '101%')
					OR (@LoaiQuyetToan = '1' AND sLNS like '1%' and sLNS not like '101%')
					OR (@LoaiQuyetToan = '2' AND sLNS like '2%')
					OR (@LoaiQuyetToan = '3' AND sLNS like '3%')
					OR (@LoaiQuyetToan = '4' AND sLNS like '4%')
					OR (@LoaiQuyetToan = '101,1,2,3,4' AND (sLNS like '1%' OR sLNS like '2%' OR sLNS like '3%' OR sLNS like '4%'))
				)
			) ctct
	ON ctct.iID_MaDonVi = dv.iID_MaDonVi
	ORDER BY dv.iID_MaDonVi
END
;
;
;
GO

