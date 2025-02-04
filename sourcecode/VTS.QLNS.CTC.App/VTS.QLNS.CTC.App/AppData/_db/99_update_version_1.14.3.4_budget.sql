/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_dutoan_donvi_clone]    Script Date: 4/16/2024 12:49:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_dutoan_donvi_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_dutoan_donvi_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_chungtu_tonghop]    Script Date: 4/16/2024 12:49:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_chungtu_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_chungtu_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_chungtu_chitiet_tao_tonghop]    Script Date: 4/16/2024 12:49:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_chungtu_chitiet_tao_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_chungtu_chitiet_tao_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_chungtu_chitiet]    Script Date: 4/16/2024 12:49:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_chungtu_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_chungtu_chitiet]
GO
/****** Object:  UserDefinedFunction [dbo].[f_quyettoan_tonghop_dieuchinh_hientai_quy]    Script Date: 4/16/2024 12:49:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_quyettoan_tonghop_dieuchinh_hientai_quy]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_quyettoan_tonghop_dieuchinh_hientai_quy]
GO
/****** Object:  UserDefinedFunction [dbo].[f_quyettoan_tonghop_dieuchinh_hientai]    Script Date: 4/16/2024 12:49:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_quyettoan_tonghop_dieuchinh_hientai]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_quyettoan_tonghop_dieuchinh_hientai]
GO
/****** Object:  UserDefinedFunction [dbo].[f_quyettoan_tonghop_dieuchinh]    Script Date: 4/16/2024 12:49:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_quyettoan_tonghop_dieuchinh]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_quyettoan_tonghop_dieuchinh]
GO
/****** Object:  UserDefinedFunction [dbo].[f_get_lns_by_id]    Script Date: 4/16/2024 12:49:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_get_lns_by_id]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_get_lns_by_id]
GO
/****** Object:  UserDefinedFunction [dbo].[f_get_lns_by_id]    Script Date: 4/16/2024 12:49:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[f_get_lns_by_id]
(    
	  @NamLamViec int,
	  @id nvarchar(max)
)
RETURNS nvarchar(max)
AS
BEGIN
	RETURN (
		SELECT TOP 1 sLNS FROM NS_MucLucNganSach 
		WHERE 
			iNamLamViec = @NamLamViec
			AND iID_MLNS = @id
	)
END
;
;
GO
/****** Object:  UserDefinedFunction [dbo].[f_quyettoan_tonghop_dieuchinh]    Script Date: 4/16/2024 12:49:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[f_quyettoan_tonghop_dieuchinh]
(    
      @NamLamViec int,
	  @NamNganSach nvarchar(20),
	  @NguonNganSach nvarchar(20),
	  @ithangquy nvarchar(100),
	  @ithangquyhientai nvarchar(100),
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
	  AND (@NamNganSach IS NULL OR iNamNganSach = @NamNganSach)
	  AND (@NguonNganSach IS NULL OR iID_MaNguonNganSach = @NguonNganSach)
	  AND (@IdDonVi = ''
		   OR iID_MaDonVi in (SELECT * FROM f_split(@IdDonVi)))
	  AND (@lns IS NULL
		   OR sLNS in (SELECT * FROM f_split(@lns)))
	  AND iID_QTChungTu IN 
		(
			SELECT iID_QTChungTu 
			FROM NS_QT_ChungTu
			where 
			(iThangQuy in (select * from f_split(@ithangquy))) 
			OR (iThangQuy in (select * from f_split(@ithangquyhientai)) AND ISNULL(iLoaiChungTu, 1) = 2)
		)
	GROUP BY iID_MLNS, iID_MLNS_Cha, iID_MaDonVi
	HAVING sum(fTuChi_PheDuyet) <> 0;
;
;
;
;
;
GO
/****** Object:  UserDefinedFunction [dbo].[f_quyettoan_tonghop_dieuchinh_hientai]    Script Date: 4/16/2024 12:49:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[f_quyettoan_tonghop_dieuchinh_hientai]
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
	  AND (@NamNganSach IS NULL OR iNamNganSach = @NamNganSach)
	  AND (@NguonNganSach IS NULL OR iID_MaNguonNganSach = @NguonNganSach)
	  AND (@IdDonVi = ''
		   OR iID_MaDonVi in (SELECT * FROM f_split(@IdDonVi)))
	  AND (@lns IS NULL
		   OR sLNS in (SELECT * FROM f_split(@lns)))
	  AND iID_QTChungTu IN 
		(
			SELECT iID_QTChungTu 
			FROM NS_QT_ChungTu
			where iThangQuy in (select * from f_split(@ithangquy)) AND ISNULL(iLoaiChungTu, 1) <> 2
		)
	GROUP BY iID_MLNS, iID_MLNS_Cha, iID_MaDonVi
	HAVING sum(fTuChi_PheDuyet) <> 0;
;
;
;
;
;
GO
/****** Object:  UserDefinedFunction [dbo].[f_quyettoan_tonghop_dieuchinh_hientai_quy]    Script Date: 4/16/2024 12:49:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[f_quyettoan_tonghop_dieuchinh_hientai_quy]
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
	  AND (@NamNganSach IS NULL OR iNamNganSach = @NamNganSach)
	  AND (@NguonNganSach IS NULL OR iID_MaNguonNganSach = @NguonNganSach)
	  AND (@IdDonVi = ''
		   OR iID_MaDonVi in (SELECT * FROM f_split(@IdDonVi)))
	  AND (@lns IS NULL
		   OR sLNS in (SELECT * FROM f_split(@lns)))
	  AND iID_QTChungTu IN 
		(
			SELECT iID_QTChungTu 
			FROM NS_QT_ChungTu
			where iThangQuy in (select * from f_split(@ithangquy)) AND ISNULL(iLoaiChungTu, 1) = 2
		)
	GROUP BY iID_MLNS, iID_MLNS_Cha, iID_MaDonVi
	HAVING sum(fTuChi_PheDuyet) <> 0;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_chungtu_chitiet]    Script Date: 4/16/2024 12:49:59 PM ******/
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
	@UserName nvarchar(100),
	@QuarterMonth nvarchar(100)
AS
BEGIN
	SET NOCOUNT ON;
	Declare @YearOfBudgetCurrent int = 2,
		@YearOfBudgetAfter int = 4;
	Declare @voucherIndex int,
	@IsLocked bit,
	@STongHop nvarchar(500),
	@ILanDieuChinh int,
	@ILoaiChungTu int;
	SELECT @voucherIndex = iSoChungTuIndex, @IsLocked = bKhoa, @STongHop = sTongHop, @ILoaiChungTu = iLoaiChungTu, @ILanDieuChinh = iLanDieuChinh FROM NS_QT_ChungTu WHERE iID_QTChungTu = @VoucherId;

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
		isnull(ctct.iID_MaDonVi, dtctct.iID_MaDonVi) as iID_MaDonVi,
		--ctct.iID_MaDonVi,
		sTenDonVi = case when @ILoaiChungTu = 2 then dv.sTenDonVi + N' (Điều chỉnh)' else dv.sTenDonVi end,
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
		fDuToan = case when @ILoaiChungTu = 2 then 0 else dtctct.DuToan end,
		fDaQuyetToan = case when @ILoaiChungTu = 2 then 0 else ctctdqt.DaQuyetToan end,
		mlns.sChiTietToi,
		mlns.bHangChaQuyetToan,
		mlns.sMaCB,
		ctct.fDeNghi_ChuyenNamSau
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
	ON mlns.sXauNoiMa = ctct.sXauNoiMa 
	LEFT JOIN 
		(select 
			(case sLNS1
				when '1040200' then (SUM(fHangNhap) + SUM(fTuChi))
				when '1040300' then (SUM(fHangMua) + SUM(fTuChi))
				else SUM(fTuChi)
			end) as DuToan,
			iID_MLNS1 as iID_MLNS,
			@AgencyId as iID_MaDonVi,
			sXauNoiMa
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
								((ISNULL(@STongHop, '') = '' AND sDSID_MaDonVi like '%' + @AgencyId + '%') OR (ISNULL(@STongHop, '') <> '' AND sDSID_MaDonVi = @AgencyId))
								AND (iLoai = 1 or iLoai = 0)
								AND iNamLamViec = @YearOfWork
								AND iNamNganSach = @YearOfBudget
								AND iID_MaNguonNganSach = @BudgetSource
								AND bKhoa = 1
								AND cast(dNgayQuyetDinh as date) <= cast(@VoucherDate as date)
								AND sSoQuyetDinh is not null AND sSoQuyetDinh <> ''
						)
						and iID_MaDonVi = @AgencyId
						and IDuLieuNhan = 0) ctct
					left join 
						(select '1040100' as sLNS1, * from NS_MucLucNganSach where sLNS in ('1040100', '1040200', '1040300') and iNamLamViec = @YearOfWork) mlns
					on mlns.sLNS1 = ctct.sLNS and mlns.sL = ctct.sL and mlns.sK = ctct.sK and mlns.sM = ctct.sM and mlns.sTM = ctct.sTM and mlns.sNG = ctct.sNG and mlns.sTTM = ctct.sTTM
				) dt
				GROUP BY iID_MLNS1, sLNS1, sXauNoiMa
			) dtctct
	on mlns.sXauNoiMa = dtctct.sXauNoiMa
	LEFT JOIN 
		(SELECT
			SUM(fTuChi_PheDuyet) AS DaQuyetToan,
			sXauNoiMa
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
					AND ((iThangQuy < @QuarterMonth) OR
					((iThangQuy = @QuarterMonth AND @ILoaiChungTu = 2 AND ISNULL(iLanDieuChinh, 0) < @ILanDieuChinh) 
					OR (iThangQuy = @QuarterMonth AND ISNULL(@ILoaiChungTu, 1) = 1 AND ISNULL(iLoaiChungTu, 1) = 1)))
					--AND cast(dNgayChungTu as date) <= cast(@voucherDate as Date)
					AND iID_QTChungTu <> @VoucherId
			)
		 GROUP BY sXauNoiMa
		) ctctdqt
	ON mlns.sXauNoiMa = ctctdqt.sXauNoiMa
	LEFT JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1) dv
	ON dv.iID_MaDonVi = ctct.iID_MaDonVi OR dv.iID_MaDonVi = dtctct.iID_MaDonVi
	WHERE
		mlns.iNamLamViec = @YearOfWork
		AND mlns.iTrangThai = 1
	Order by mlns.sLNS, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.sTNG, mlns.sTNG1, mlns.sTNG2, mlns.sTNG3
END
;
;
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_chungtu_chitiet_tao_tonghop]    Script Date: 4/16/2024 12:49:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qt_chungtu_chitiet_tao_tonghop]
	@VoucherIds ntext,
	@VoucherId nvarchar(100),
	@YearOfBudget int,
	@BudgetSource int,
	@YearOfWork int,
	@Type nvarchar(10),
	@QuarterMonthType int,
	@QuarterMonth int,
	@AgencyId nvarchar(10),
	@UserName nvarchar(100)
AS
BEGIN
	INSERT INTO [dbo].[NS_QT_ChungTuChiTiet]
           ([iID_QTChungTu]
           ,[iID_MLNS]
           ,[iID_MLNS_Cha]
           ,[sXauNoiMa]
           ,[sLNS]
           ,[sL]
           ,[sK]
           ,[sM]
           ,[sTM]
           ,[sTTM]
           ,[sNG]
           ,[sTNG]
		   ,[sTNG1]
		   ,[sTNG2]
		   ,[sTNG3]
           ,[bHangCha]
           ,[iNamNganSach]
           ,[iID_MaNguonNganSach]
           ,[iNamLamViec]
           ,[iThangQuyLoai]
           ,[iThangQuy]
           ,[iID_MaDonVi]
           ,[fSoNguoi]
           ,[fSoNgay]
           ,[fSoLuot]
		   ,[fTuChi_DeNghi]
		   ,[fTuChi_PheDuyet]
		   ,[fDeNghi_ChuyenNamSau]
           ,[sGhiChu]
           ,[dNgayTao]
           ,[sNguoiTao]
           ,[dNgaySua]
           ,[sNguoiSua])
    SELECT 
			@VoucherId
           ,mlns.iID_MLNS
           ,mlns.iID_MLNS_Cha
           ,mlns.sXauNoiMa
           ,mlns.sLNS
           ,mlns.sL
           ,mlns.sK
           ,mlns.sM
           ,mlns.sTM
           ,mlns.sTTM
           ,mlns.sNG
           ,mlns.sTNG
		   ,mlns.sTNG1
		   ,mlns.sTNG2
		   ,mlns.sTNG3
           ,mlns.bHangCha
           ,@YearOfBudget
           ,@BudgetSource
           ,@YearOfWork
           ,@QuarterMonthType
           ,@QuarterMonth
           ,@AgencyId
           ,Sum(fSoNguoi)
           ,Sum(fSoNgay)
           ,Sum(fSoLuot)
		   ,Sum(fTuChi_DeNghi)
		   ,Sum(fTuChi_PheDuyet)
		   ,Sum(fDeNghi_ChuyenNamSau)
           ,null
           ,GetDate()
           ,@UserName
           ,null
           ,null
	FROM NS_QT_ChungTuChiTiet ctct
	INNER JOIN NS_MucLucNganSach mlns ON ctct.sXauNoiMa = mlns.sXauNoiMa AND mlns.iNamLamViec = ctct.iNamLamViec
	INNER JOIN NS_QT_ChungTu ct ON ctct.iID_QTChungTu = ct.iID_QTChungTu
	WHERE ct.iID_QTChungTu IN (SELECT * FROM f_split(@VoucherIds)) 
	GROUP BY mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.sXauNoiMa, mlns.sLNS, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.sTNG, mlns.sTNG1, mlns.sTNG2, mlns.sTNG3, mlns.bHangCha;

	-- Danh dau chung tu da tong hop
	UPDATE NS_QT_ChungTu SET bDaTongHop = 1 
	WHERE iID_QTChungTu in
		(SELECT *
		 FROM f_split(@VoucherIds))
END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_chungtu_tonghop]    Script Date: 4/16/2024 12:49:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_qt_rpt_chungtu_tonghop]
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@EstimateAgencyId nvarchar(max),
	@AgencyId nvarchar(max),
	@VoucherDate datetime,
	@QuarterMonth nvarchar(100),
	@QuarterMonthBefore nvarchar(100),
	@Dvt int
AS
BEGIN

	declare @AdjustMonth nvarchar(100)
	declare @CurrentMonth nvarchar(100)
	if (@QuarterMonth like '%,%')
	begin
		set @AdjustMonth = (select top(1) Item from f_split(@QuarterMonth))
		set @CurrentMonth = REPLACE(@QuarterMonth,@AdjustMonth + ',', '')
	end
	else 
	begin
		set @AdjustMonth = @QuarterMonth
		set @CurrentMonth = ''
	end
			

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
	   FROM f_dutoan_tonghop(@YearOfWork, @YearOfBudget, @BudgetSource, @VoucherDate, @EstimateAgencyId, @lns) 

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
	   FROM f_quyettoan_tonghop_dieuchinh(@YearOfWork, @YearOfBudget, @BudgetSource, @QuarterMonthBefore, @AdjustMonth, @AgencyId, @lns) 
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
	   FROM f_quyettoan_tonghop_dieuchinh_hientai(@YearOfWork, @YearOfBudget, @BudgetSource, @QuarterMonth, @AgencyId, @lns)

	   UNION ALL 
	   SELECT iID_MLNS,
			ChiTieu = 0,
			TuChi2 = 0,
			TuChi,
			HienVat,
			SoNguoi,
			SoNgay,
			SoLuot
	   FROM f_quyettoan_tonghop_dieuchinh_hientai_quy(@YearOfWork, @YearOfBudget, @BudgetSource, @CurrentMonth, @AgencyId, @lns)

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

	select @AdjustMonth
	select @CurrentMonth
END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_dutoan_donvi_clone]    Script Date: 4/16/2024 12:49:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qt_rpt_dutoan_donvi_clone]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@EstimateAgencyId nvarchar(max),
	@AgencyId nvarchar(max),
	@LNS nvarchar(max),
	@QuarterMonth nvarchar(100),
	@QuarterMonthBefore nvarchar(100),
	@VoucherDate datetime,
	@Dvt int
AS
BEGIN
	declare @AdjustMonth nvarchar(100)
	declare @CurrentMonth nvarchar(100)
	if (@QuarterMonth like '%,%')
	begin
		set @AdjustMonth = (select top(1) Item from f_split(@QuarterMonth))
		set @CurrentMonth = REPLACE(@QuarterMonth,@AdjustMonth + ',', '')
	end
	else 
	begin
		set @AdjustMonth = @QuarterMonth
		set @CurrentMonth = ''
	end
	DECLARE @IsDonViCha bit;
	IF @EstimateAgencyId = @AgencyId
		SET @IsDonViCha = 0
	ELSE
		SET @IsDonViCha = 1
	SELECT sLNS,
		   iID_MLNS,
		   TenDonVi,
		   ChiTieu = SUM(ChiTieu) / @Dvt,
		   TuChi2 = SUM(TuChi2) / @Dvt,
		   TuChi = SUM(TuChi) / @Dvt 
		   INTO #tblEstimateSettlement
	FROM
	  (--chi tieu theo don vi
	  SELECT sLNS,
			iID_MLNS,
			iID_MaDonVi,
			ChiTieu = fTuChi + fHangNhap + fHangMua,
			TuChi2 = 0,
			TuChi = 0
	   FROM NS_DT_ChungTuChiTiet
	   WHERE iID_DTChungTu in 
		(
			SELECT iID_DTChungTu 
			FROM NS_DT_ChungTu 
			WHERE iNamLamViec = @YearOfWork
				AND iNamNganSach = @YearOfBudget
				AND iID_MaNguonNganSach = @BudgetSource
				AND (sSoQuyetDinh is not null or sSoQuyetDinh <> '')
				AND (cast(dNgayQuyetDinh as DATE) <= cast(@VoucherDate as date)
				OR (YEAR(CAST(dNgayQuyetDinh AS DATE)) - YEAR(CAST(@VoucherDate AS DATE)) = 1
				AND MONTH(CAST(dNgayQuyetDinh AS DATE)) IN (1,2,3) AND MONTH(CAST(@VoucherDate AS DATE)) = 12))
		)
		 AND (@AgencyId IS NULL OR iID_MaDonVi in (SELECT * FROM f_split(@EstimateAgencyId)))
		 AND (@LNS IS NULL OR sLNS in (SELECT * FROM dbo.splitstring(@LNS)))
		 AND IDuLieuNhan = 0		 
	   --số đã quyết toán
	   UNION ALL 
	   SELECT sLNS=dbo.f_get_lns_by_id(@YearOfWork,iID_MLNS),
			iID_MLNS,
			CASE
			    WHEN @IsDonViCha = 1 THEN @EstimateAgencyId
		    ELSE 
				iID_MaDonVi
		    END AS iID_MaDonVi,
			ChiTieu =0,
			TuChi2 = TuChi,
			TuChi = 0
		from f_quyettoan_tonghop_dieuchinh(@YearOfWork, @YearOfBudget,@BudgetSource,@QuarterMonthBefore,@AdjustMonth,@AgencyId,@LNS)
	   --FROM f_quyettoan_tonghop_dieuchinh(@YearOfWork, @YearOfBudget, @BudgetSource, @QuarterMonthBefore, @AgencyId, @LNS)
		 
	   --quyết toán đợt này
	   UNION ALL 
	   SELECT sLNS=dbo.f_get_lns_by_id(@YearOfWork,iID_MLNS),
			iID_MLNS,
			CASE
			    WHEN @IsDonViCha = 1 THEN @EstimateAgencyId
		    ELSE 
				iID_MaDonVi
		    END AS iID_MaDonVi,
			ChiTieu = 0,
			TuChi2 = 0,
			TuChi
	   FROM f_quyettoan_tonghop_dieuchinh_hientai(@YearOfWork, @YearOfBudget, @BudgetSource, @QuarterMonth, @AgencyId, @lns)
	   
	   UNION ALL 
	   SELECT sLNS=dbo.f_get_lns_by_id(@YearOfWork,iID_MLNS),
			iID_MLNS,
			CASE
			    WHEN @IsDonViCha = 1 THEN @EstimateAgencyId
		    ELSE 
				iID_MaDonVi
		    END AS iID_MaDonVi,
			ChiTieu = 0,
			TuChi2 = 0,
			TuChi
	   FROM f_quyettoan_tonghop_dieuchinh_hientai_quy(@YearOfWork, @YearOfBudget, @BudgetSource, @CurrentMonth, @AgencyId, @lns)) AS ct
	LEFT JOIN
	  (SELECT iID_MaDonVi,
			  TenDonVi = iID_MaDonVi + ' - ' + sTenDonVi
	   FROM DonVi
	   WHERE iTrangThai=1
		 AND iNamLamViec = @YearOfWork) AS dv ON dv.iID_MaDonVi = ct.iID_MaDonVi
	GROUP BY 
			 sLNS,
			 TenDonVi,
			 iID_MLNS
	HAVING SUM(TuChi) <> 0
	OR SUM(TuChi2) <> 0
	OR SUM(ChiTieu) <> 0;

	select distinct sLNS into #tblLNS from #tblEstimateSettlement;
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
		TenDonVi
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
					FROM #tblLNS) LNS UNPIVOT (value FOR col in (LNS1, LNS3, LNS5, LNS)) un)) mlns
	LEFT JOIN #tblEstimateSettlement dt
	ON dt.iID_MLNS = mlns.iID_MLNS
	WHERE bHangCha = 1 OR (bHangCha = 0 AND (dt.TuChi <> 0 OR dt.ChiTieu <> 0 OR dt.TuChi2 <> 0))
	ORDER BY mlns.sXauNoiMa
	DROP TABLE #tblEstimateSettlement, #tblLNS;
END
;
;
;
;
;
;
;
GO
