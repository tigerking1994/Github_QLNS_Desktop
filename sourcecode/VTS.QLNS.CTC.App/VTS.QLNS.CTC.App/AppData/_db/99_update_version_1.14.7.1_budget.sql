UPDATE NS_CP_ChungTu SET iLoai = 1
GO
INSERT [dbo].[HT_ChucNang] ([iID_MaChucNang], [iID_ChucNang], [iID_ChucNangCha], [sSTT], [ITrangThai], [sTenChucNang], [BHangCha]) VALUES (N'BUDGET_ALLOCATION_RECEIVE', N'a12be807-14e8-42ca-b007-41998ec68937', N'c95d24a0-b1c5-440f-90ef-86afbdf1f72d', N'03-03-00-00-00', 1, N'NHẬN CẤP PHÁT', 1)
GO
INSERT [dbo].[HT_Quyen_ChucNang] ([iID_MaChucNang], [iID_MaQuyen]) VALUES (N'BUDGET_ALLOCATION_RECEIVE', N'ADMIN')
GO
INSERT [dbo].[HT_Quyen_ChucNang] ([iID_MaChucNang], [iID_MaQuyen]) VALUES (N'BUDGET_ALLOCATION_RECEIVE', N'CP')
GO

/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_theo_donvi]    Script Date: 8/6/2024 2:10:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_chungtu_theo_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_chungtu_theo_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_tatca_donvi]    Script Date: 8/6/2024 2:10:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_chungtu_tatca_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_chungtu_tatca_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chitiet_nhan]    Script Date: 8/6/2024 2:10:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_chungtu_chitiet_nhan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_chungtu_chitiet_nhan]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chitiet]    Script Date: 8/6/2024 2:10:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_chungtu_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_chungtu_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chitiet]    Script Date: 8/6/2024 2:10:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE [dbo].[sp_cp_chungtu_chitiet]
	@VoucherId nvarchar(255),
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@AgencyId nvarchar(500),
	@VoucherDate datetime,
	@UserName nvarchar(100),
	@PhanCap nvarchar(10),
	@IsCapPhatToanDonVi bit
AS
BEGIN
	DECLARE @CapPhatIndex int;
	select * into #tempLNS from f_split(@LNS);
	select * into #tempAgency from  f_split(@AgencyId);
	SELECT @CapPhatIndex = iSoChungTuIndex
	FROM NS_CP_ChungTu
	WHERE iID_CTCapPhat = @VoucherId

	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha
			into #tblNsMlns
	FROM NS_MucLucNganSach 
	where iNamLamViec = @YearOfWork 

	
		-- lấy ra dữ liệu dự toán
	SELECT SUM(fTuChi) AS TuChi,
          SUM(fHangNhap) AS HangNhap,
          SUM(fHangMua) AS HangMua,
          iID_MaDonVi,
          iID_MLNS,
		  iID_MLNS_Cha,
          sXauNoiMa,
          sLNS AS LNS
		  into #tblPhanBoDuToan
   FROM NS_DT_ChungTuChiTiet
   WHERE iID_DTChungTu IN
       (SELECT iID_DTChungTu
        FROM NS_DT_ChungTu
        WHERE sSoQuyetDinh <> ''
          AND sSoQuyetDinh IS NOT NULL
          AND iLoai = 1 
          AND iNamLamViec = @YearOfWork
          AND ((@YearOfBudget = 2
                AND (iNamNganSach = 2
                     OR iNamNganSach = 4))
               OR (@YearOfBudget <> 2
                   AND iNamNganSach = @YearOfBudget))
          AND iID_MaNguonNganSach = @BudgetSource
          AND cast(dNgayQuyetDinh AS date) <= cast(@VoucherDate AS date))
     AND iID_MaDonVi in  (SELECT * FROM f_split(@AgencyId))
   GROUP BY iID_MaDonVi, iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS


	SELECT DISTINCT iID_MLNS, iID_MLNS_Cha into #tblPhanBoDuToanGroupbyiID_MLNS from #tblPhanBoDuToan

	SELECT T.* INTO #tblNsMlns1
	FROM #tblNsMlns T
	INNER JOIN #tblPhanBoDuToanGroupbyiID_MLNS E ON T.iID_MLNS = E.iID_MLNS 

	-- ;WITH C AS  
	--(  
	--  SELECT T.*
	--  FROM #tblNsMlns T
	--  INNER JOIN #tblPhanBoDuToanGroupbyiID_MLNS E ON T.iID_MLNS = E.iID_MLNS 
	--  UNION ALL 
	--  SELECT T.*
	--  FROM #tblNsMlns T
	--  INNER JOIN C
	--  ON T.iID_MLNS = C.iID_MLNS_Cha
	--)

	;WITH C AS  
	(  
	  SELECT *
	  FROM #tblNsMlns1
	  UNION ALL 
	  SELECT T.*
	  FROM #tblNsMlns T
	  INNER JOIN C
	  ON T.iID_MLNS = C.iID_MLNS_Cha
	)

	SELECT DISTINCT * into #mlnsPhanBo from C;

	
	--select a.* into #tblMlns_LNS_L_K_M_TM_TTM_NG from #tblNsMlns a inner join #tblPhanBoDuToanGroupbyiID_MLNS b on  a.iID_MLNS =  b.iID_MLNS;

 --   select * into #tblMlns_LNS_L_K_M_TM_TTM from  #tblNsMlns  where  sXauNoiMa in 
 --     (select CONCAT  (sLNS,'-',sL,'-',sK,'-',sM,'-',sTM,'-',sTTM ) as sXaunoima from #tblMlns_LNS_L_K_M_TM_TTM_NG);
 

 --   select * into #tblMlns_LNS_L_K_M_TM from  #tblNsMlns    where  sXauNoiMa in 
 --     (select CONCAT  (sLNS,'-',sL,'-',sK,'-',sM,'-',sTM ) as sXaunoima from #tblMlns_LNS_L_K_M_TM_TTM_NG );

 --   select * into #tblMlns_LNS_L_K_M from  #tblNsMlns    where  sXauNoiMa in 
 --     (select CONCAT  (sLNS,'-',sL,'-',sK,'-',sM ) as sXaunoima from #tblMlns_LNS_L_K_M_TM_TTM_NG) ;
  
 --   select * into #tblMlns_LNS_L_K from  #tblNsMlns    where  sXauNoiMa in 
 --     (select CONCAT  (sLNS,'-',sL,'-',sK) as sXaunoima from #tblMlns_LNS_L_K_M_TM_TTM_NG);

 --   select * into #tblMlns_LNS_L from  #tblNsMlns    where  sXauNoiMa in 
 --     (select CONCAT  (sLNS,'-',sL) as sXaunoima from #tblMlns_LNS_L_K_M_TM_TTM_NG );

 --   select * into #tblMlns_LNS from  #tblNsMlns    where  sXauNoiMa in 
 --     (select sLNS as sXaunoima from #tblMlns_LNS_L_K_M_TM_TTM_NG );

	--select * into #tblMlns_LN from  #tblNsMlns    where  sXauNoiMa in 
 --     (select SUBSTRING(sLNS,0,4) as sXaunoima from #tblMlns_LNS_L_K_M_TM_TTM_NG );

	--select * into #tblMlns_L from  #tblNsMlns    where  sXauNoiMa in 
 --     (select SUBSTRING(sLNS,0,2) as sXaunoima from #tblMlns_LNS_L_K_M_TM_TTM_NG );

 --   select * into #mlnsPhanBo from #tblMlns_LNS_L_K_M_TM_TTM_NG union all 
 --   select * from #tblMlns_LNS_L_K_M_TM_TTM union all 
 --   select * from #tblMlns_LNS_L_K_M_TM union all 
 --   select * from #tblMlns_LNS_L_K_M union all 
 --   select * from #tblMlns_LNS_L_K union all 
 --   select * from #tblMlns_LNS_L union all 
 --   select * from #tblMlns_LNS union all
	--select * from #tblMlns_LN union all 
	--select * from #tblMlns_L  
	

	-- lấy ra mlns theo phân cấp
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa,
			CASE
				--WHEN @PhanCap = 'M' and (sM is null OR sM = '') THEN cast(1 as bit)
				--WHEN @PhanCap = 'TM' and (sTM is null OR sTM = '') THEN cast(1 as bit)
				--WHEN @PhanCap = 'NG' and (sNG is null OR sNG = '') THEN cast(1 as bit)
				WHEN (sNG is null OR sNG = '') THEN cast(1 as bit)
			ELSE cast(0 as bit)
			END AS bHangCha	
			into #tblMlnsByPhanCap
	FROM #mlnsPhanBo 
	WHERE 
			--(
			--(@PhanCap = 'M' AND (sTM IS NULL OR sTM = ''))
			--OR (@PhanCap = 'TM' AND (sTTM IS NULL OR sTTM = ''))
			--OR (@PhanCap = 'NG' AND (sTNG IS NULL OR sTNG = ''))
			--sTNG IS NULL OR sTNG = ''
			--)
		sLNS IN (
					SELECT DISTINCT VALUE
					FROM 
					(
						SELECT 
							CAST(LEFT(sLNS, 1) AS nvarchar(10)) LNS1, 
							CAST(LEFT(sLNS, 3) AS nvarchar(10)) LNS3, 
							CAST(LEFT(sLNS, 5) AS nvarchar(10)) LNS5, 
							CAST(sLNS AS nvarchar(10)) LNS 
						FROM
							NS_NguoiDung_LNS 
						WHERE 
							sMaNguoiDung = @UserName
							AND INamLamViec = @YearOfWork
							AND sLNS IN (SELECT * FROM #tempLNS)
					) LNS
					UNPIVOT
					(
						value
						FOR col in (LNS1, LNS3, LNS5, LNS)
					) un) 

	-- lấy ra đơn vị từ đơn vị của chứng từ
	SELECT iID_MaDonVi, iID_MaDonVi + ' - ' + sTenDonVi as sTenDonVi into #tblDonVi
	FROM DonVi 
	WHERE 
		INamLamViec = @YearOfWork 
		AND iID_MaDonVi IN (SELECT * FROM #tempAgency)



	-- map bảng mlnsPhanCap và đơn vị
	SELECT * INTO #tblMLNS FROM (
		SELECT * FROM #tblMlnsByPhanCap, #tblDonVi 
		WHERE bHangCha = 0 OR (bHangCha = 1 AND @PhanCap = 'M' and (IsNull(sM, '') != '' or sM <> '')) OR (bHangCha = 1 AND @PhanCap = 'TM' and (IsNull(sTM, '') != '' or sTM <> ''))
		UNION ALL
		SELECT *, null AS iID_MaDonVi, null AS sTenDonVi  
		FROM #tblMlnsByPhanCap 
		WHERE bHangCha = 1 AND (@PhanCap <> 'M' OR  (IsNull(sM, '') = '' or sM = '')) AND (@PhanCap <> 'TM' OR  (IsNull(sTM, '') = '' or sTM = ''))
	) mlns

	-- map bảng mlns ns và đơn vị
	SELECT * INTO #tblMlnsRoot FROM (
		SELECT * FROM #tblNsMlns, #tblDonVi 
		WHERE bHangCha = 0
		UNION ALL
		SELECT *, null AS iID_MaDonVi, null AS sTenDonVi  
		FROM #tblNsMlns 
		WHERE bHangCha = 1
	) mlns


	-- lấy dữ liệu đã cấp
	SELECT SUM(fTuChi) AS DaCap,
		  0 AS DuToan,
          iID_MaDonVi,
          iID_MLNS,
		  sLNS
		  into #tblDataDaCap
	FROM NS_CP_ChungTuChiTiet
	WHERE iID_CTCapPhat IN
       (
		SELECT iID_CTCapPhat
        FROM NS_CP_ChungTu
        WHERE iNamLamViec = @YearOfWork
		  AND iID_MaNguonNganSach = @BudgetSource
		  AND iNamNganSach = @YearOfBudget
          AND iID_CTCapPhat <> @VoucherId
		  AND bKhoa = 1
		  AND iLoai = 1
		  AND iSoChungTuIndex < @CapPhatIndex
          AND CAST(dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
		  AND EXISTS(SELECT * FROM #tempAgency INTERSECT SELECT * FROM f_split(sDSID_MaDonVi))
		)
		AND iID_MaDonVi IN (SELECT * FROM #tempAgency)
	GROUP BY iID_MaDonVi, iID_MLNS, sLNS;

	

   SELECT * into #tblDataDuToan FROM (
   SELECT 0 AS DaCap,
		ISNULL(CASE
			WHEN mlns.sLNS = '1040200' THEN (dtctct.TuChi + dtctct.HangNhap)
			WHEN mlns.sLNS = '1040300' THEN (dtctct.TuChi + dtctct.HangMua)
			ELSE dtctct.TuChi
		END, 0)  AS DuToan,
		isnull(dtctct.iID_MaDonVi, @AgencyId) AS iID_MaDonVi,
		mlns.iID_MLNS,
		mlns.sLNS
	FROM (SELECT * FROM NS_MucLucNganSach where iNamLamViec = @YearOfWork) mlns
	INNER JOIN (SELECT * FROM #tblPhanBoDuToan where LNS <> '1040100') dtctct
	ON mlns.iID_MLNS = dtctct.iID_MLNS
	UNION ALL
	SELECT 0 AS DaCap,
		ISNULL(CASE
			WHEN mlns.sLNS = '1040200' THEN (dtctct.TuChi + dtctct.HangNhap)
			WHEN mlns.sLNS = '1040300' THEN (dtctct.TuChi + dtctct.HangMua)
			ELSE dtctct.TuChi
		END, 0)  AS DuToan,
		isnull(dtctct.iID_MaDonVi, @AgencyId) AS iID_MaDonVi,
		mlns.iID_MLNS,
		mlns.sLNS
	FROM (SELECT * FROM NS_MucLucNganSach where iNamLamViec = @YearOfWork) mlns
	INNER JOIN (SELECT * FROM #tblPhanBoDuToan where LNS = '1040100') dtctct
	on  REPLACE(dtctct.sXauNoiMa, '1040100', '1040200') = mlns.sXauNoiMa
	UNION ALL
	SELECT 0 AS DaCap,
		ISNULL(CASE
			WHEN mlns.sLNS = '1040200' THEN (dtctct.TuChi + dtctct.HangNhap)
			WHEN mlns.sLNS = '1040300' THEN (dtctct.TuChi + dtctct.HangMua)
			ELSE dtctct.TuChi
		END, 0)  AS DuToan,
		isnull(dtctct.iID_MaDonVi, @AgencyId) AS iID_MaDonVi,
		mlns.iID_MLNS,
		mlns.sLNS
	FROM (SELECT * FROM NS_MucLucNganSach where iNamLamViec = @YearOfWork) mlns
	INNER JOIN(SELECT * FROM #tblPhanBoDuToan where LNS = '1040100') dtctct
	on REPLACE(dtctct.sXauNoiMa, '1040100', '1040300') = mlns.sXauNoiMa
	) dt

	SELECT sum(DaCap) AS DaCap, sum(DuToan) AS DuToan, iID_MaDonVi, iID_MLNS, sLNS into #tblDataDaCapDuToan FROM (
		SELECT * FROM #tblDataDaCap
		UNION ALL
		SELECT * FROM #tblDataDuToan
		) data
	GROUP BY iID_MaDonVi, iID_MLNS, sLNS

	SELECT mlns.iID_MLNS,
		   mlns.iID_MLNS_Cha,
		   mlns.sXauNoiMa,
		   DaCap,
		   DuToan,
		   isnull(mlns.iID_MaDonVi, daCapDuToan.iID_MaDonVi) as iID_MaDonVi
		   INTO #tblDaCapDuToan
	FROM #tblMlnsRoot mlns
	LEFT JOIN
	  #tblDataDaCapDuToan daCapDuToan
	ON mlns.iID_MLNS = daCapDuToan.iID_MLNS and ((mlns.iID_MaDonVi is not null and mlns.iID_MaDonVi = daCapDuToan.iID_MaDonVi) or mlns.iID_MaDonVi is null)

	--;WITH C AS  
	--(  
	--  SELECT T.iID_MLNS,  
	--		 T.DaCap, 
	--		 T.DuToan, 
	--		 T.iID_MLNS AS RootID,
	--		 T.iID_MaDonVi
	--  FROM #tblDaCapDuToan T
	--  WHERE DaCap <> 0 OR DuToan <> 0
	--  UNION ALL 
	--  SELECT T.iID_MLNS,  
	--		 T.DaCap,
	--		  T.DuToan, 
	--		 C.RootID,
	--		 T.iID_MaDonVi
	--  FROM #tblDaCapDuToan T
	--	INNER JOIN C
	--	  on T.iID_MLNS_Cha = C.iID_MLNS 
	--)

	--SELECT iID_MLNS, iID_MLNS_Cha, iID_MaDonVi, sum(DaCap) AS DaCap, sum(DuToan) AS DuToan INTO #tblDaCapDuToanResult FROM (
	--	SELECT distinct T.iID_MLNS,  
	--		   T.iID_MLNS_Cha,
	--		   isnull(T.iID_MaDonVi, total.iID_MaDonVi) as iID_MaDonVi,
	--		   total.DaCap,
	--		   total.DuToan
	--	FROM #tblDaCapDuToan T  
	--	LEFT JOIN (  
	--				 SELECT RootID, iID_MaDonVi, DaCap,  DuToan  
	--				 FROM C
	--				 ) AS total 
	--	ON T.iID_MLNS = total.RootID AND (T.iID_MaDonVi = total.iID_MaDonVi OR T.iID_MaDonVi IS NULL)
	--	WHERE total.DaCap <> 0 OR total.DuToan <> 0) data
	--GROUP BY iID_MLNS, iID_MLNS_Cha, iID_MaDonVi
	--option (maxrecursion 0)

	SELECT iID_MLNS, iID_MLNS_Cha, iID_MaDonVi, sum(DaCap) AS DaCap, sum(DuToan) AS DuToan INTO #tblDaCapDuToanResult FROM (
		SELECT distinct T.iID_MLNS,  
			   T.iID_MLNS_Cha,
			   --isnull(T.iID_MaDonVi, T.iID_MaDonVi) as iID_MaDonVi,
			   T.iID_MaDonVi,
			   T.DaCap,
			   T.DuToan
		FROM #tblDaCapDuToan T 
		WHERE T.DaCap <> 0 OR T.DuToan <> 0) data
	GROUP BY iID_MLNS, iID_MLNS_Cha, iID_MaDonVi
	option (maxrecursion 0)

	SELECT
		isnull(ctct.iID_CTCapPhatChiTiet, NEWID()) AS Id,
		ctct.iID_CTCapPhat AS IdChungTu,
		mlnsPhanBo.iID_MLNS AS MlnsId,
		mlnsPhanBo.iID_MLNS_Cha AS MlnsIdParent,
		mlnsPhanBo.sXauNoiMa AS XauNoiMa,
		mlnsPhanBo.sLNS AS LNS,
		mlnsPhanBo.sL AS L,
		mlnsPhanBo.sK AS K,
		mlnsPhanBo.sM AS M,
		mlnsPhanBo.sTM AS TM,
		mlnsPhanBo.sTTM AS TTM,
		mlnsPhanBo.sNG AS NG,
		mlnsPhanBo.sTNG AS TNG,
		mlnsPhanBo.sTNG1 AS TNG1,
		mlnsPhanBo.sTNG2 AS TNG2,
		mlnsPhanBo.sTNG3 AS TNG3,
		mlnsPhanBo.sMoTa AS MoTa,
		'' Chuong, 
		ct.sSoChungTu AS SoChungTu,
		mlnsPhanBo.bHangCha,
		@YearOfWork AS NamLamViec,
		@YearOfBudget AS NamNganSach,
		@BudgetSource AS NguonNganSach,
		ctct.iLoai,
		mlnsPhanBo.iID_MaDonVi AS IdDonVi,
		mlnsPhanBo.sTenDonVi AS TenDonVi,
		ROUND((isnull(ctct.fTuChi, 0)), 0) AS TuChi,
		ROUND((isnuLL(ctct.fDeNghiDonVi, 0)), 0) AS DeNghiDonVi,
		ROUND((isnull(ctct.fHienVat, 0)), 0) AS HienVat,
		ROUND((isnull(daCapDuToan.DaCap, 0)), 0) as DaCap,
		ROUND((isnull(daCapDuToan.DuToan, 0)), 0) as DuToan,
		ctct.sGhiChu AS GhiChu,
		isnull(ctct.dNgayTao, getdate()) AS DateCreated,
		isnull(ctct.dNgaySua, getdate()) AS DateModified,
		ctct.sNguoiTao AS UserCreator,
		ctct.sNguoiSua AS UserModifier
	FROM #tblMLNS AS mlnsPhanBo
	LEFT JOIN
		(SELECT *
			FROM 
				NS_CP_ChungTuChiTiet
			WHERE 
		 		iID_CTCapPhat = @VoucherId
		 		AND INamLamViec = @YearOfWork
		 		AND iID_MaNguonNganSach = @BudgetSource
				AND iNamNganSach = @YearOfBudget
		) ctct
	ON mlnsPhanBo.iID_MLNS = ctct.iID_MLNS and mlnsPhanBo.iID_MaDonVi = ctct.iID_MaDonVi
	LEFT JOIN NS_CP_ChungTu ct ON ctct.iID_CTCapPhat = ct.iID_CTCapPhat
	LEFT JOIN #tblDaCapDuToanResult daCapDuToan
	on mlnsPhanBo.iID_MLNS = daCapDuToan.iID_MLNS and daCapDuToan.iID_MaDonVi LIKE '%' + mlnsPhanBo.iID_MaDonVi + '%' 
	order by mlnsPhanBo.sXauNoiMa, mlnsPhanBo.iID_MaDonVi;
	
END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chitiet_nhan]    Script Date: 8/6/2024 2:10:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE [dbo].[sp_cp_chungtu_chitiet_nhan]
	@VoucherId nvarchar(255),
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@AgencyId nvarchar(500),
	@VoucherDate datetime,
	@UserName nvarchar(100),
	@PhanCap nvarchar(10),
	@IsCapPhatToanDonVi bit
AS
BEGIN
	DECLARE @CapPhatIndex int;
	select * into #tempLNS from f_split(@LNS);
	SELECT @CapPhatIndex = iSoChungTuIndex
	FROM NS_CP_ChungTu
	WHERE iID_CTCapPhat = @VoucherId

	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha
			into #tblNsMlns
	FROM NS_MucLucNganSach 
	where iNamLamViec = @YearOfWork 

	
		-- lấy ra dữ liệu dự toán
	SELECT SUM(fTuChi) AS TuChi,
          SUM(fHangNhap) AS HangNhap,
          SUM(fHangMua) AS HangMua,
          iID_MaDonVi,
          iID_MLNS,
		  iID_MLNS_Cha,
          sXauNoiMa,
          sLNS AS LNS
		  into #tblPhanBoDuToan
   FROM NS_DT_ChungTuChiTiet
   WHERE iID_DTChungTu IN
       (SELECT iID_DTChungTu
        FROM NS_DT_ChungTu
        WHERE sSoQuyetDinh <> ''
          AND sSoQuyetDinh IS NOT NULL
          AND iLoai = 0
          AND iNamLamViec = @YearOfWork
          AND ((@YearOfBudget = 2
                AND (iNamNganSach = 2
                     OR iNamNganSach = 4))
               OR (@YearOfBudget <> 2
                   AND iNamNganSach = @YearOfBudget))
          AND iID_MaNguonNganSach = @BudgetSource
          AND cast(dNgayQuyetDinh AS date) <= cast(@VoucherDate AS date))
     AND iID_MaDonVi in  (SELECT * FROM f_split(@AgencyId))
   GROUP BY iID_MaDonVi, iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS


	SELECT DISTINCT iID_MLNS, iID_MLNS_Cha into #tblPhanBoDuToanGroupbyiID_MLNS from #tblPhanBoDuToan

	SELECT T.* INTO #tblNsMlns1
	FROM #tblNsMlns T
	INNER JOIN #tblPhanBoDuToanGroupbyiID_MLNS E ON T.iID_MLNS = E.iID_MLNS 

	-- ;WITH C AS  
	--(  
	--  SELECT T.*
	--  FROM #tblNsMlns T
	--  INNER JOIN #tblPhanBoDuToanGroupbyiID_MLNS E ON T.iID_MLNS = E.iID_MLNS 
	--  UNION ALL 
	--  SELECT T.*
	--  FROM #tblNsMlns T
	--  INNER JOIN C
	--  ON T.iID_MLNS = C.iID_MLNS_Cha
	--)

	;WITH C AS  
	(  
	  SELECT *
	  FROM #tblNsMlns1
	  UNION ALL 
	  SELECT T.*
	  FROM #tblNsMlns T
	  INNER JOIN C
	  ON T.iID_MLNS = C.iID_MLNS_Cha
	)

	SELECT DISTINCT * into #mlnsPhanBo from C;

	

	-- lấy ra mlns theo phân cấp
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa,
			CASE
				--WHEN @PhanCap = 'M' and (sM is null OR sM = '') THEN cast(1 as bit)
				--WHEN @PhanCap = 'TM' and (sTM is null OR sTM = '') THEN cast(1 as bit)
				--WHEN @PhanCap = 'NG' and (sNG is null OR sNG = '') THEN cast(1 as bit)
				WHEN (sNG is null OR sNG = '') THEN cast(1 as bit)
			ELSE cast(0 as bit)
			END AS bHangCha	
			into #tblMlnsByPhanCap
	FROM #mlnsPhanBo 
	WHERE 
		sLNS IN (
					SELECT DISTINCT VALUE
					FROM 
					(
						SELECT 
							CAST(LEFT(sLNS, 1) AS nvarchar(10)) LNS1, 
							CAST(LEFT(sLNS, 3) AS nvarchar(10)) LNS3, 
							CAST(LEFT(sLNS, 5) AS nvarchar(10)) LNS5, 
							CAST(sLNS AS nvarchar(10)) LNS 
						FROM
							NS_NguoiDung_LNS 
						WHERE 
							sMaNguoiDung = @UserName
							AND INamLamViec = @YearOfWork
							AND sLNS IN (SELECT * FROM #tempLNS)
					) LNS
					UNPIVOT
					(
						value
						FOR col in (LNS1, LNS3, LNS5, LNS)
					) un) 



	-- lấy dữ liệu đã cấp
	SELECT SUM(fTuChi) AS DaCap,
		  0 AS DuToan,
          iID_MaDonVi,
          iID_MLNS,
		  sLNS
		  into #tblDataDaCap
	FROM NS_CP_ChungTuChiTiet
	WHERE iID_CTCapPhat IN
       (
		SELECT iID_CTCapPhat
        FROM NS_CP_ChungTu
        WHERE iNamLamViec = @YearOfWork
		  AND iID_MaNguonNganSach = @BudgetSource
		  AND iNamNganSach = @YearOfBudget
          AND iID_CTCapPhat <> @VoucherId
		  AND bKhoa = 1
		  AND iLoai = 0
		  AND iSoChungTuIndex < @CapPhatIndex
          AND CAST(dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE))
	GROUP BY iID_MaDonVi, iID_MLNS, sLNS;

   SELECT * into #tblDataDuToan FROM (
   SELECT 0 AS DaCap,
		ISNULL(CASE
			WHEN mlns.sLNS = '1040200' THEN (dtctct.TuChi + dtctct.HangNhap)
			WHEN mlns.sLNS = '1040300' THEN (dtctct.TuChi + dtctct.HangMua)
			ELSE dtctct.TuChi
		END, 0)  AS DuToan,
		isnull(dtctct.iID_MaDonVi, @AgencyId) AS iID_MaDonVi,
		mlns.iID_MLNS,
		mlns.sLNS
	FROM (SELECT * FROM NS_MucLucNganSach where iNamLamViec = @YearOfWork) mlns
	INNER JOIN (SELECT * FROM #tblPhanBoDuToan where LNS <> '1040100') dtctct
	ON mlns.iID_MLNS = dtctct.iID_MLNS
	UNION ALL
	SELECT 0 AS DaCap,
		ISNULL(CASE
			WHEN mlns.sLNS = '1040200' THEN (dtctct.TuChi + dtctct.HangNhap)
			WHEN mlns.sLNS = '1040300' THEN (dtctct.TuChi + dtctct.HangMua)
			ELSE dtctct.TuChi
		END, 0)  AS DuToan,
		isnull(dtctct.iID_MaDonVi, @AgencyId) AS iID_MaDonVi,
		mlns.iID_MLNS,
		mlns.sLNS
	FROM (SELECT * FROM NS_MucLucNganSach where iNamLamViec = @YearOfWork) mlns
	INNER JOIN (SELECT * FROM #tblPhanBoDuToan where LNS = '1040100') dtctct
	on  REPLACE(dtctct.sXauNoiMa, '1040100', '1040200') = mlns.sXauNoiMa
	UNION ALL
	SELECT 0 AS DaCap,
		ISNULL(CASE
			WHEN mlns.sLNS = '1040200' THEN (dtctct.TuChi + dtctct.HangNhap)
			WHEN mlns.sLNS = '1040300' THEN (dtctct.TuChi + dtctct.HangMua)
			ELSE dtctct.TuChi
		END, 0)  AS DuToan,
		isnull(dtctct.iID_MaDonVi, @AgencyId) AS iID_MaDonVi,
		mlns.iID_MLNS,
		mlns.sLNS
	FROM (SELECT * FROM NS_MucLucNganSach where iNamLamViec = @YearOfWork) mlns
	INNER JOIN(SELECT * FROM #tblPhanBoDuToan where LNS = '1040100') dtctct
	on REPLACE(dtctct.sXauNoiMa, '1040100', '1040300') = mlns.sXauNoiMa
	) dt

	SELECT sum(DaCap) AS DaCap, sum(DuToan) AS DuToan, iID_MaDonVi, iID_MLNS, sLNS into #tblDataDaCapDuToan FROM (
		SELECT * FROM #tblDataDaCap
		UNION ALL
		SELECT * FROM #tblDataDuToan
		) data
	GROUP BY iID_MaDonVi, iID_MLNS, sLNS

	SELECT mlns.iID_MLNS,
		   mlns.iID_MLNS_Cha,
		   mlns.sXauNoiMa,
		   DaCap,
		   DuToan
		   INTO #tblDaCapDuToan
	FROM #tblNsMlns mlns
	LEFT JOIN
	  #tblDataDaCapDuToan daCapDuToan
	ON mlns.iID_MLNS = daCapDuToan.iID_MLNS

	

	SELECT iID_MLNS, iID_MLNS_Cha, sum(DaCap) AS DaCap, sum(DuToan) AS DuToan INTO #tblDaCapDuToanResult FROM (
		SELECT distinct T.iID_MLNS,  
			   T.iID_MLNS_Cha,
			   --isnull(T.iID_MaDonVi, T.iID_MaDonVi) as iID_MaDonVi,
			   T.DaCap,
			   T.DuToan
		FROM #tblDaCapDuToan T 
		WHERE T.DaCap <> 0 OR T.DuToan <> 0) data
	GROUP BY iID_MLNS, iID_MLNS_Cha
	option (maxrecursion 0)

	SELECT
		isnull(ctct.iID_CTCapPhatChiTiet, NEWID()) AS Id,
		ctct.iID_CTCapPhat AS IdChungTu,
		mlnsPhanBo.iID_MLNS AS MlnsId,
		mlnsPhanBo.iID_MLNS_Cha AS MlnsIdParent,
		mlnsPhanBo.sXauNoiMa AS XauNoiMa,
		mlnsPhanBo.sLNS AS LNS,
		mlnsPhanBo.sL AS L,
		mlnsPhanBo.sK AS K,
		mlnsPhanBo.sM AS M,
		mlnsPhanBo.sTM AS TM,
		mlnsPhanBo.sTTM AS TTM,
		mlnsPhanBo.sNG AS NG,
		mlnsPhanBo.sTNG AS TNG,
		mlnsPhanBo.sTNG1 AS TNG1,
		mlnsPhanBo.sTNG2 AS TNG2,
		mlnsPhanBo.sTNG3 AS TNG3,
		mlnsPhanBo.sMoTa AS MoTa,
		'' Chuong, 
		ct.sSoChungTu AS SoChungTu,
		mlnsPhanBo.bHangCha,
		@YearOfWork AS NamLamViec,
		@YearOfBudget AS NamNganSach,
		@BudgetSource AS NguonNganSach,
		ctct.iLoai,
		ROUND((isnull(ctct.fTuChi, 0)), 0) AS TuChi,
		ROUND((isnuLL(ctct.fDeNghiDonVi, 0)), 0) AS DeNghiDonVi,
		ROUND((isnull(ctct.fHienVat, 0)), 0) AS HienVat,
		ROUND((isnull(daCapDuToan.DaCap, 0)), 0) as DaCap,
		ROUND((isnull(daCapDuToan.DuToan, 0)), 0) as DuToan,
		ctct.sGhiChu AS GhiChu,
		isnull(ctct.dNgayTao, getdate()) AS DateCreated,
		isnull(ctct.dNgaySua, getdate()) AS DateModified,
		ctct.sNguoiTao AS UserCreator,
		ctct.sNguoiSua AS UserModifier
	FROM #tblMlnsByPhanCap AS mlnsPhanBo
	LEFT JOIN
		(SELECT *
			FROM 
				NS_CP_ChungTuChiTiet
			WHERE 
		 		iID_CTCapPhat = @VoucherId
		 		AND INamLamViec = @YearOfWork
		 		AND iID_MaNguonNganSach = @BudgetSource
				AND iNamNganSach = @YearOfBudget
		) ctct
	ON mlnsPhanBo.iID_MLNS = ctct.iID_MLNS
	LEFT JOIN NS_CP_ChungTu ct ON ctct.iID_CTCapPhat = ct.iID_CTCapPhat
	LEFT JOIN #tblDaCapDuToanResult daCapDuToan
	on mlnsPhanBo.iID_MLNS = daCapDuToan.iID_MLNS
	order by mlnsPhanBo.sXauNoiMa
	
END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_tatca_donvi]    Script Date: 8/6/2024 2:10:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_cp_chungtu_tatca_donvi]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@ILoai int
AS
BEGIN
	SELECT chungtu.iID_CTCapPhat as Id,
		chungtu.sSoChungTu as SoChungTu,
		chungtu.iSoChungTuIndex as SoChungTuIndex,
		chungtu.dNgayChungTu as NgayChungTu,
		chungtu.sSoQuyetDinh as SoQuyetDinh,
		chungtu.dNgayQuyetDinh as NgayQuyetDinh,
		chungtu.sMoTa as MoTa,
		chungtu.sDSID_MaDonVi as IdDonVi,
		'' as TenDonVi,
		chungtu.sDSLNS as Lns,
		chungtu.nChiTietToi as ChiTietToi,
		chungtu.iType_MoTa as ITypeMoTa,
		chungtu.iLoai,
		chungtu.iNamLamViec as NamLamViec,
		chungtu.dNgayTao as DateCreated,
		chungtu.sNguoiTao as UserCreator,
		chungtu.dNgaySua as DateModified,
		chungtu.sNguoiSua as UserModifier,
		chungtu.bKhoa as IsLocked,
		chungtu.iID_MaNguonNganSach as NguonNganSach,
		chungtu.iNamNganSach as NamNganSach,
		chungtu.iID_MaDMCapPhat as LoaiCap,
		chungtu.fTongTuChi as SoTuChi,
		danhmuc.sTen as TenLoai,
		chungtu.sDSSoChungTuTongHop AS DSSoChungTuTongHop,
		chungTu.bDaTongHop
		FROM NS_CP_ChungTu chungtu
	LEFT JOIN 
		(SELECT * FROM CP_DanhMuc WHERE iNamLamViec = @YearOfWork) danhmuc 
	ON danhmuc.iID_MaDMCapPhat = chungtu.iID_MaDMCapPhat
	WHERE 
		chungtu.iID_MaNguonNganSach = @BudgetSource 
		AND chungtu.iNamLamViec = @YearOfWork 
		AND chungtu.iNamNganSach = @YearOfBudget
		AND chungtu.iLoai = @ILoai
		AND chungTu.sDSSoChungTuTongHop IS NULL
		OR chungTu.sDSSoChungTuTongHop = ''
	order by chungtu.sSoChungTu asc;
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_theo_donvi]    Script Date: 8/6/2024 2:10:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_cp_chungtu_theo_donvi]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@UserName nvarchar(100),
	@ILoai int
as
begin

	DECLARE @CountDonViCha int;
	SELECT 
		@CountDonViCha = count(*) 
	FROM 
		(SELECT * 
		FROM NguoiDung_DonVi 
		WHERE iID_MaNguoiDung = @UserName 
			AND iNamLamViec = @YearOfWork 
			AND iTrangThai = 1
		) nddv
	INNER JOIN
		(SELECT * 
		FROM DonVi 
		WHERE iNamLamViec = @YearOfWork 
			AND iTrangThai = 1 
			AND iLoai = 0
		) dv
	ON dv.iID_MaDonVi = nddv.iID_MaDonVi;

	SELECT chungtu.iID_CTCapPhat as Id,
		chungtu.sSoChungTu as SoChungTu,
		chungtu.iSoChungTuIndex as SoChungTuIndex,
		chungtu.dNgayChungTu as NgayChungTu,
		chungtu.sSoQuyetDinh as SoQuyetDinh,
		chungtu.dNgayQuyetDinh as NgayQuyetDinh,
		chungtu.sMoTa as MoTa,
		chungtu.sDSID_MaDonVi as IdDonVi,
		'' as TenDonVi,
		chungtu.sDSLNS as Lns,
		chungtu.nChiTietToi as ChiTietToi,
		chungtu.iType_MoTa as ITypeMoTa,
		chungtu.iLoai,
		chungtu.iNamLamViec as NamLamViec,
		chungtu.dNgayTao as DateCreated,
		chungtu.sNguoiTao as UserCreator,
		chungtu.dNgaySua as DateModified,
		chungtu.sNguoiSua as UserModifier,
		chungtu.bKhoa as IsLocked,
		chungtu.iID_MaNguonNganSach as NguonNganSach,
		chungtu.iNamNganSach as NamNganSach,
		chungtu.iID_MaDMCapPhat as LoaiCap,
		chungtu.fTongTuChi as SoTuChi,
		danhmuc.sTen as TenLoai,
		chungtu.sDSSoChungTuTongHop AS DSSoChungTuTongHop,
		chungTu.bDaTongHop
		FROM NS_CP_ChungTu chungtu
		LEFT JOIN
			(SELECT * FROM CP_DanhMuc WHERE iNamLamViec = @YearOfWork) danhmuc 
		ON danhmuc.iID_MaDMCapPhat = chungtu.iID_MaDMCapPhat
		WHERE chungtu.iID_MaNguonNganSach = @BudgetSource 
			AND chungtu.iNamLamViec = @YearOfWork 
			AND chungtu.iNamNganSach = @YearOfBudget
			AND chungtu.iLoai = @ILoai
			AND 
			(
				(
					(
						EXISTS (SELECT * from f_split(chungtu.sDSLNS) INTERSECT SELECT sLNS FROM NS_NguoiDung_LNS WHERE sMaNguoiDung = @UserName and iNamLamViec = @YearOfWork)
						or chungtu.sDSLNS = '' or chungtu.sDSLNS is null 
					)
					AND EXISTS (SELECT * from f_split(chungtu.sDSID_MaDonVi) INTERSECT SELECT iID_MaDonVi FROM NguoiDung_DonVi  WHERE iID_MaNguoiDung = @UserName and iNamLamViec = @YearOfWork)
					AND (@CountDonViCha = 0)
				)
				OR (@CountDonViCha <> 0 AND chungtu.bKhoa = 1)
				OR 
				(
					(
						EXISTS (SELECT * from f_split(chungtu.sDSLNS) INTERSECT SELECT sLNS FROM NS_NguoiDung_LNS WHERE sMaNguoiDung = @UserName and iNamLamViec = @YearOfWork)
						or chungtu.sDSLNS = '' or chungtu.sDSLNS is null 
					)
					AND EXISTS (SELECT * from f_split(chungtu.sDSID_MaDonVi) INTERSECT SELECT iID_MaDonVi FROM NguoiDung_DonVi  WHERE iID_MaNguoiDung = @UserName and iNamLamViec = @YearOfWork)
					AND (@CountDonViCha <> 0)
				)
			)
			order by chungtu.sSoChungTu asc;
end;
;
;
GO
