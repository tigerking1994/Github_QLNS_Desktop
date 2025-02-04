/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_sosanh_lns]    Script Date: 3/25/2024 11:10:02 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_rpt_sosanh_lns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_rpt_sosanh_lns]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_loaicap_tong_donvi]    Script Date: 3/25/2024 11:10:02 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_rpt_loaicap_tong_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_rpt_loaicap_tong_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_loaicap_lns]    Script Date: 3/25/2024 11:10:02 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_rpt_loaicap_lns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_rpt_loaicap_lns]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_donvi]    Script Date: 3/25/2024 11:10:02 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_rpt_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_rpt_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_lns_rpt_thongtri_lns]    Script Date: 3/25/2024 11:10:02 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_lns_rpt_thongtri_lns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_lns_rpt_thongtri_lns]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_lns_rpt_thongtri_donvi]    Script Date: 3/25/2024 11:10:02 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_lns_rpt_thongtri_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_lns_rpt_thongtri_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chitiet]    Script Date: 3/25/2024 11:10:02 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_chungtu_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_chungtu_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chitiet]    Script Date: 3/25/2024 11:10:02 AM ******/
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
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_lns_rpt_thongtri_donvi]    Script Date: 3/25/2024 11:10:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_cp_lns_rpt_thongtri_donvi] 
	@NamLamViec int,
    @CapPhatId nvarchar(100),
	@DonViId nvarchar(max),
	@Dvt int,
	@ILoaiNganSach int
AS
BEGIN
SET NOCOUNT ON;
	SELECT ctct.iID_MaDonVi AS MaDonVi,
		dv.sTenDonVi AS TenDonVi,
		ROUND((SUM(fTuChi) / @Dvt), 0) AS CapPhat
	FROM NS_CP_ChungTuChiTiet ctct
	LEFT JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec) dv
	ON dv.iID_MaDonVi = ctct.iID_MaDonVi
	INNER JOIN 
		(SELECT * FROM NS_MucLucNganSach WHERE iNamLamViec = @NamLamViec) ns 
	ON ctct.iID_MLNS = ns.iID_MLNS
	WHERE iID_CTCapPhat = @CapPhatId 
		AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@DonViId))
		AND (@ILoaiNganSach = -1 OR ns.iLoaiNganSach = @ILoaiNganSach)
	GROUP BY ctct.iID_MaDonVi, dv.sTenDonVi
	ORDER BY ctct.iID_MaDonVi
END;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_lns_rpt_thongtri_lns]    Script Date: 3/25/2024 11:10:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_cp_lns_rpt_thongtri_lns]
	 @NamLamViec int,
	 @CapPhatId nvarchar(100),
	 @IdDonVi nvarchar(max),
	 @PhanCap nvarchar(10),
	 @LNS nvarchar(max),
	 @UserName nvarchar(100),
	 @Dvt int
AS
BEGIN 
	SET NOCOUNT ON;

	SELECT DISTINCT VALUE
	INTO #tblLNS
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
			AND INamLamViec = @NamLamViec
			AND sLNS IN (SELECT * FROM f_split(@LNS))
	) LNS
	UNPIVOT
	(
		value
		FOR col in (LNS1, LNS3, LNS5, LNS)
	) un


	-- lấy ra mlns theo phân cấp
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa,
			CASE
				WHEN @PhanCap = 'M' and (sM is null OR sM = '') THEN cast(1 as bit)
				WHEN @PhanCap = 'TM' and (sTM is null OR sTM = '') THEN cast(1 as bit)
				WHEN @PhanCap = 'NG' and (sNG is null OR sNG = '') THEN cast(1 as bit)
			ELSE cast(0 as bit)
			END AS bHangCha	
			INTO #tblMlnsByPhanCap
	FROM NS_MucLucNganSach 
	WHERE 
		iNamLamViec = @NamLamViec 
		AND iTrangThai = 1
		AND (
			(@PhanCap = 'M' AND (sTM IS NULL OR sTM = ''))
			OR (@PhanCap = 'TM' AND (sTTM IS NULL OR sTTM = ''))
			OR (@PhanCap = 'NG' AND (sTNG IS NULL OR sTNG = ''))
			)
		AND sLNS IN (SELECT * FROM #tblLNS) 

	SELECT 
		mlns.iID_MLNS as IdMlns,
		mlns.iID_MLNS_Cha as IdMlnsCha,
		mlns.sXauNoiMa as XauNoiMa,
		mlns.sLNS as LNS,
		mlns.sL as L,
		mlns.sK as K,
		mlns.sM as M,
		mlns.sTM as TM,
		mlns.sTTM as TTM,
		mlns.sNG as NG,
		mlns.sTNG as TNG,
		mlns.sMoTa as MoTa,
		mlns.bHangCha as IsHangCha,
		ROUND((ISNULL(ctct.fTuChi, 0) / @Dvt), 0) as TuChi 
	FROM 
		#tblMlnsByPhanCap mlns
	LEFT JOIN 
		(SELECT * FROM NS_CP_ChungTuChiTiet WHERE iID_CTCapPhat = @CapPhatId AND iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))) ctct
	ON mlns.iID_MLNS = ctct.iID_MLNS 
	Order by mlns.sXauNoiMa
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_donvi]    Script Date: 3/25/2024 11:10:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_cp_rpt_donvi]
@NamLamViec int,
@NamNganSach int,
@NguonNganSach int,
@IdDonvi nvarchar(2000),
@ctID nvarchar(max),
@NgayChungTu DateTime,
@UserName nvarchar(50),
@Dvt int
AS
BEGIN
	select ROUND((isnull(sum(ctct.fTuChi),0) / @Dvt), 0) as fCapPhat, ctct.iID_MaDonVi, ctct.iID_MLNS
	into #temp
	from NS_CP_ChungTuChiTiet as ctct
	inner join NS_CP_ChungTu as ct on ctct.iID_CTCapPhat = ct.iID_CTCapPhat
	where ct.iNamLamViec = @NamLamViec
	and ct.iNamNganSach = @NamNganSach
	and (@NguonNganSach = 0 or ct.iID_MaNguonNganSach = @NguonNganSach)
	and ctct.iID_MaDonVi  IN (SELECT * FROM f_split(@IdDonVi))
	and ct.iID_CTCapPhat in (select * from f_split(@ctID))	
	group by ctct.iID_MaDonVi, ctct.iID_MLNS;

	select ns.iID_MLNS, ns.iID_MLNS_Cha, ns.sLNS, ns.sL, ns.sK, ns.sM, ns.sTTM, ns.sNG, ns.bHangCha, tm.iID_MaDonVi, ns.sMoTa, tm.fCapPhat, ns.sXauNoiMa
	from  #temp as tm
	inner join NS_MucLucNganSach as ns on tm.iID_MLNS = ns.iID_MLNS
	where ns.iNamLamViec = @NamLamViec

end
;
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_loaicap_lns]    Script Date: 3/25/2024 11:10:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_cp_rpt_loaicap_lns]
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@IdDonVi nvarchar(max),
	@CapPhatIds nvarchar(max),
	@Dvt int
AS
BEGIN
	WITH tblCapPhat AS (
		SELECT 
				iID_MLNS,
				sLNS,
				iID_MaDonVi,
				sum(fTuChi) as fTuChi,
				ct.iID_MaDMCapPhat
			FROM 
				(SELECT * 
				FROM NS_CP_ChungTuChiTiet
				WHERE iID_CTCapPhat IN (SELECT * FROM f_split(@CapPhatIds))
					AND (@IdDonvi IS NULL OR iID_MaDonVi IN (SELECT * FROM f_split(@IdDonvi)))
				) ctct
				LEFT JOIN 
					(SELECT * 
					FROM NS_CP_ChungTu
					WHERE iNamLamViec = @NamLamViec
						AND iNamNganSach = @NamNganSach
						AND (iID_MaNguonNganSach = @NguonNganSach OR @NguonNganSach = 0)
					) ct
				ON ct.iID_CTCapPhat = ctct.iID_CTCapPhat
			GROUP BY iID_MaDonVi, sLNS, iID_MLNS, ct.iID_MaDMCapPhat, ctct.iID_CTCapPhat
	),	
	tblData AS (
		SELECT iID_MLNS,
			sLNS,
			capPhat.iID_MaDonVi as MaDonVi,
			dv.sTenDonVi as TenDonVi,
			SUM(CapUng) / @Dvt as CapUng,
			SUM(CapThanhKhoan) / @Dvt as CapThanhKhoan,
			SUM(CapHopThuc) / @Dvt as CapHopThuc,
			SUM(CapThu) / @Dvt as CapThu
		FROM 
		(
			SELECT 
				iID_MLNS,
				sLNS,
				iID_MaDonVi,
				SUM(fTuChi) as CapUng,
				0 as CapHopThuc,
				0 as CapThanhKhoan,
				0 as CapThu
			FROM tblCapPhat
			WHERE iID_MaDMCapPhat = 1
			GROUP BY iID_MaDonVi, sLNS, iID_MLNS
			UNION
			SELECT iID_MLNS,
				sLNS,
				iID_MaDonVi,
				0 as CapUng,
				SUM(fTuChi) as CapHopThuc,
				0 as CapThanhKhoan,
				0 as CapThu
			FROM tblCapPhat
			WHERE iID_MaDMCapPhat = 2
			GROUP BY iID_MaDonVi, sLNS, iID_MLNS
			UNION
			SELECT iID_MLNS,
				sLNS,
				iID_MaDonVi,
				0 as CapUng,
				0 as CapHopThuc,
				SUM(fTuChi) as CapThanhKhoan,
				0 as CapThu
			FROM tblCapPhat
			WHERE iID_MaDMCapPhat = 3
			GROUP BY iID_MaDonVi, sLNS, iID_MLNS
			UNION 
			SELECT iID_MLNS,
				sLNS,
				iID_MaDonVi,
				0 as CapUng,
				0 as CapHopThuc,
				0 as CapThanhKhoan,
				SUM(fTuChi) as CapThu
			FROM tblCapPhat
			WHERE iID_MaDMCapPhat = 4
			GROUP BY iID_MaDonVi, sLNS, iID_MLNS
		) capPhat
		LEFT JOIN 
			(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec) dv
		ON dv.iID_MaDonVi = capPhat.iID_MaDonVi
		GROUP BY capPhat.iID_MaDonVi, dv.sTenDonVi, iID_MLNS, sLNS
	),
	tblLNS AS (
		SELECT DISTINCT VALUE
		FROM 
		(
			SELECT 
				CAST(LEFT(sLNS, 1) AS nvarchar(10)) LNS1, 
				CAST(LEFT(sLNS, 3) AS nvarchar(10)) LNS3, 
				CAST(LEFT(sLNS, 5) AS nvarchar(10)) LNS5, 
				CAST(sLNS AS nvarchar(10)) LNS 
			FROM
				tblData
		) LNS
		UNPIVOT
		(
			value
			FOR col in (LNS1, LNS3, LNS5, LNS)
		) un
	)
	select 
		mlns.iID_MLNS as MlnsId,
		mlns.iID_MLNS_Cha as MlnsIdCha,
		mlns.sLNS as LNS,
		mlns.sL as L,
		mlns.sK as K,
		mlns.sM as M,
		mlns.sTM as TM,
		mlns.sTTM as TTM,
		mlns.sNG as NG,
		mlns.sMoTa as MoTa,
		mlns.sXauNoiMa as XauNoiMa,
		case 
			when len(mlns.sLNS) <= 3 THEN cast(1 as bit)
		else 
			cast (0 as bit)
		end as BHangChaLns,
		case 
			when dt.MaDonVi is null THEN cast(1 as bit)
		else 
			cast (0 as bit)
		end as BHangChaDonVi,
		dt.MaDonVi,
		dt.TenDonVi,
		ROUND((isnull(sum(dt.CapUng), 0)), 0) as CapUng,
		ROUND((isnull(sum(dt.CapThanhKhoan), 0)), 0) as CapThanhKhoan,
		ROUND((isnull(sum(dt.CapHopThuc), 0)), 0) as CapHopThuc,
		ROUND((isnull(sum(dt.CapThu), 0)), 0) as CapThu
	from
		(select * from NS_MucLucNganSach where iNamLamViec = @NamLamViec and sLNS in (select * from tblLNS)) mlns
	left join 
		tblData dt
	on dt.iID_MLNS = mlns.iID_MLNS
	where bHangCha = 1 or (CapUng is not null or CapThanhKhoan is not null or CapHopThuc is not null or capThu is not null)
	group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.sLNS, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.sMoTa, mlns.bHangCha, dt.MaDonVi, dt.TenDonVi, mlns.sXauNoiMa
	order by sXauNoiMa
	
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_loaicap_tong_donvi]    Script Date: 3/25/2024 11:10:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_cp_rpt_loaicap_tong_donvi]
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@IdDonVi nvarchar(max),
	@CapPhatIds nvarchar(max),
	@Dvt int
AS
BEGIN
	WITH tblCapPhat AS (
		SELECT 
			iID_MaDonVi,
			sum(fTuChi) as fTuChi,
			ct.iID_MaDMCapPhat
		FROM 
			(SELECT * 
			FROM NS_CP_ChungTuChiTiet
			WHERE iID_CTCapPhat IN (SELECT * FROM f_split(@CapPhatIds))
				AND (@IdDonvi IS NULL OR iID_MaDonVi IN (SELECT * FROM f_split(@IdDonvi)))
			) ctct
			LEFT JOIN 
				(SELECT * 
				FROM NS_CP_ChungTu
				WHERE iNamLamViec = @NamLamViec
					AND iNamNganSach = @NamNganSach
					AND (iID_MaNguonNganSach = @NguonNganSach OR @NguonNganSach = 0)
				) ct
			ON ct.iID_CTCapPhat = ctct.iID_CTCapPhat
		GROUP BY iID_MaDonVi, sLNS, iID_MLNS, ct.iID_MaDMCapPhat, ctct.iID_CTCapPhat
	)
	SELECT capPhat.iID_MaDonVi as MaDonVi,
		dv.sTenDonVi as TenDonVi,
		ROUND((SUM(CapUng) / @Dvt), 0) as CapUng,
		ROUND((SUM(CapThanhKhoan) / @Dvt), 0) as CapThanhKhoan,
		ROUND((SUM(CapHopThuc) / @Dvt), 0) as CapHopThuc,
		ROUND((SUM(CapThu) / @Dvt), 0) as CapThu
	FROM 
	(
		SELECT iID_MaDonVi,
			SUM(fTuChi) as CapUng,
			0 as CapHopThuc,
			0 as CapThanhKhoan,
			0 as CapThu
		FROM tblCapPhat
		WHERE iID_MaDMCapPhat = 1
		GROUP BY iID_MaDonVi
		UNION
		SELECT iID_MaDonVi,
			0 as CapUng,
			SUM(fTuChi) as CapHopThuc,
			0 as CapThanhKhoan,
			0 as CapThu
		FROM tblCapPhat
		WHERE iID_MaDMCapPhat = 2
		GROUP BY iID_MaDonVi
		UNION
		SELECT iID_MaDonVi,
			0 as CapUng,
			0 as CapHopThuc,
			SUM(fTuChi) as CapThanhKhoan,
			0 as CapThu
		FROM tblCapPhat
		WHERE iID_MaDMCapPhat = 3
		GROUP BY iID_MaDonVi
		UNION 
		SELECT iID_MaDonVi,
			0 as CapUng,
			0 as CapHopThuc,
			0 as CapThanhKhoan,
			SUM(fTuChi) as CapThu
		FROM tblCapPhat
		WHERE iID_MaDMCapPhat = 4
		GROUP BY iID_MaDonVi
	) capPhat
	LEFT JOIN 
		(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec) dv
	ON dv.iID_MaDonVi = capPhat.iID_MaDonVi
	GROUP BY capPhat.iID_MaDonVi, dv.sTenDonVi
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_sosanh_lns]    Script Date: 3/25/2024 11:10:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_cp_rpt_sosanh_lns]
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@IdDonvi nvarchar(2000),
	@CapPhatIds nvarchar(max), 
	@NgayChungTu datetime,
	@PhanCap nvarchar(10),
	@LNS nvarchar(max),
	@UserName nvarchar(100),
	@Dvt int
AS
BEGIN
	-- lấy dữ liệu cấp phát
	SELECT SUM(fTuChi) AS CapPhat,
		  0 AS DuToan,
          iID_MaDonVi,
          iID_MLNS,
		  sLNS
		  into #tblDataCapPhat
	FROM NS_CP_ChungTuChiTiet
	WHERE iID_CTCapPhat IN
       (select * from f_split(@CapPhatIds))
		AND iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	GROUP BY iID_MaDonVi, iID_MLNS, sLNS;

	-- lấy ra mlns từ dữ liệu đã cấp
	SELECT DISTINCT VALUE INTO #tblMlnsCapPhat
	FROM 
	(
		SELECT 
			CAST(LEFT(sLNS, 1) AS nvarchar(10)) LNS1, 
			CAST(LEFT(sLNS, 3) AS nvarchar(10)) LNS3, 
			CAST(LEFT(sLNS, 5) AS nvarchar(10)) LNS5, 
			CAST(sLNS AS nvarchar(10)) LNS 
		FROM
			#tblDataCapPhat
	) LNS
	UNPIVOT
	(
		value
		FOR col in (LNS1, LNS3, LNS5, LNS)
	) un

	-- lấy ra dữ liệu dự toán
	SELECT SUM(fTuChi) AS TuChi,
          SUM(fHangNhap) AS HangNhap,
          SUM(fHangMua) AS HangMua,
          iID_MaDonVi,
          iID_MLNS,
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
          AND iNamLamViec = @NamLamViec
          AND ((@NamNganSach = 2
                AND (iNamNganSach = 2
                     OR iNamNganSach = 4))
               OR (@NamNganSach <> 2
                   AND iNamNganSach = @NamNganSach))
          AND (iID_MaNguonNganSach = @NguonNganSach OR @NguonNganSach =0)
          AND cast(dNgayQuyetDinh AS date) <= cast(@NgayChungTu AS date))
     AND iID_MaDonVi in  (SELECT * FROM f_split(@IdDonVi))
   GROUP BY iID_MaDonVi, iID_MLNS, sXauNoiMa, sLNS

   SELECT * into #tblDataDuToan FROM (
   SELECT 0 AS CapPhat,
		ISNULL(CASE
			WHEN mlns.sLNS = '1040200' THEN (dtctct.TuChi + dtctct.HangNhap)
			WHEN mlns.sLNS = '1040300' THEN (dtctct.TuChi + dtctct.HangMua)
			ELSE dtctct.TuChi
		END, 0)  AS DuToan,
		isnull(dtctct.iID_MaDonVi, @IdDonVi) AS iID_MaDonVi,
		mlns.iID_MLNS,
		mlns.sLNS
	FROM (SELECT * FROM NS_MucLucNganSach where iNamLamViec = @NamLamViec) mlns
	INNER JOIN (SELECT * FROM #tblPhanBoDuToan where LNS <> '1040100') dtctct
	ON mlns.iID_MLNS = dtctct.iID_MLNS
	UNION ALL
	SELECT 0 AS CapPhat,
		ISNULL(CASE
			WHEN mlns.sLNS = '1040200' THEN (dtctct.TuChi + dtctct.HangNhap)
			WHEN mlns.sLNS = '1040300' THEN (dtctct.TuChi + dtctct.HangMua)
			ELSE dtctct.TuChi
		END, 0)  AS DuToan,
		isnull(dtctct.iID_MaDonVi, @IdDonVi) AS iID_MaDonVi,
		mlns.iID_MLNS,
		mlns.sLNS
	FROM (SELECT * FROM NS_MucLucNganSach where iNamLamViec = @NamLamViec) mlns
	INNER JOIN (SELECT * FROM #tblPhanBoDuToan where LNS = '1040100') dtctct
	on  REPLACE(dtctct.sXauNoiMa, '1040100', '1040200') = mlns.sXauNoiMa
	UNION ALL
	SELECT 0 AS CapPhat,
		ISNULL(CASE
			WHEN mlns.sLNS = '1040200' THEN (dtctct.TuChi + dtctct.HangNhap)
			WHEN mlns.sLNS = '1040300' THEN (dtctct.TuChi + dtctct.HangMua)
			ELSE dtctct.TuChi
		END, 0)  AS DuToan,
		isnull(dtctct.iID_MaDonVi, @IdDonVi) AS iID_MaDonVi,
		mlns.iID_MLNS,
		mlns.sLNS
	FROM (SELECT * FROM NS_MucLucNganSach where iNamLamViec = @NamLamViec) mlns
	INNER JOIN(SELECT * FROM #tblPhanBoDuToan where LNS = '1040100') dtctct
	on REPLACE(dtctct.sXauNoiMa, '1040100', '1040300') = mlns.sXauNoiMa
	) dt

	SELECT DISTINCT VALUE INTO #tblMlnsDuToan
	FROM 
	(
		SELECT 
			CAST(LEFT(sLNS, 1) AS nvarchar(10)) LNS1, 
			CAST(LEFT(sLNS, 3) AS nvarchar(10)) LNS3, 
			CAST(LEFT(sLNS, 5) AS nvarchar(10)) LNS5, 
			CAST(sLNS AS nvarchar(10)) LNS 
		FROM
			#tblDataDuToan
	) LNS
	UNPIVOT
	(
		value
		FOR col in (LNS1, LNS3, LNS5, LNS)
	) un

	SELECT distinct * into #tblMlnsCapPhatDuToan FROM (
		SELECT * FROM #tblMlnsCapPhat
		UNION ALL
		SELECT * FROM #tblMlnsDuToan
	) mlns

	SELECT sum(CapPhat) AS CapPhat, sum(DuToan) AS DuToan, iID_MaDonVi, iID_MLNS, sLNS into #tblDataCapPhatDuToan FROM (
		SELECT * FROM #tblDataCapPhat
		UNION ALL
		SELECT * FROM #tblDataDuToan
		) data
	GROUP BY iID_MaDonVi, iID_MLNS, sLNS

	SELECT mlns.iID_MLNS,
		   mlns.iID_MLNS_Cha,
		   mlns.sXauNoiMa,
		   CapPhat,
		   DuToan,
		   capPhatDuToan.iID_MaDonVi
		   INTO #tblCapPhatDuToan
	FROM (SELECT * FROM NS_MucLucNganSach where iNamLamViec = @NamLamViec and sLNS in (SELECT * FROM #tblMlnsCapPhatDuToan)) mlns
	LEFT JOIN
	  #tblDataCapPhatDuToan capPhatDuToan
	ON mlns.iID_MLNS = capPhatDuToan.iID_MLNS

	;WITH C AS  
	(  
	  SELECT T.iID_MLNS,  
			 T.CapPhat, 
			 T.DuToan, 
			 T.iID_MLNS AS RootID,
			 T.iID_MaDonVi
	  FROM #tblCapPhatDuToan T
	  UNION ALL 
	  SELECT T.iID_MLNS,  
			 T.CapPhat,
			 T.DuToan, 
			 C.RootID,
			 T.iID_MaDonVi
	  FROM #tblCapPhatDuToan T
		INNER JOIN C
		  on T.iID_MLNS_Cha = C.iID_MLNS
	)

	SELECT iID_MLNS, iID_MLNS_Cha, iID_MaDonVi, sum(CapPhat) AS CapPhat, sum(DuToan) AS DuToan INTO #tblCapPhatDuToanResult FROM (
		SELECT distinct T.iID_MLNS,  
			   T.iID_MLNS_Cha,
			   isnull(T.iID_MaDonVi, total.iID_MaDonVi) as iID_MaDonVi,
			   total.CapPhat,
			   total.DuToan
		FROM #tblCapPhatDuToan T  
		LEFT JOIN (  
					 SELECT RootID, iID_MaDonVi,  CapPhat,  DuToan  
					 FROM C
					 ) AS total 
		ON T.iID_MLNS = total.RootID AND (T.iID_MaDonVi = total.iID_MaDonVi OR T.iID_MaDonVi IS NULL)
		WHERE total.CapPhat <> 0 OR total.DuToan <> 0) data
	GROUP BY iID_MLNS, iID_MLNS_Cha, iID_MaDonVi
	option (maxrecursion 0)

	SELECT distinct sLNS into #tblLNS
	FROM NS_MucLucNganSach 
	WHERE iID_MLNS in (SELECT iID_MLNS FROM #tblCapPhatDuToanResult WHERE iNamLamViec = @NamLamViec)

	-- lấy ra mlns theo phân cấp
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa,
			CASE
				WHEN @PhanCap = 'M' and (sM is null OR sM = '') THEN cast(1 as bit)
				WHEN @PhanCap = 'TM' and (sTM is null OR sTM = '') THEN cast(1 as bit)
				WHEN @PhanCap = 'NG' and (sNG is null OR sNG = '') THEN cast(1 as bit)
			ELSE cast(0 as bit)
			END AS bHangCha	
			into #tblMlnsByPhanCap
	FROM NS_MucLucNganSach 
	WHERE 
		iNamLamViec = @NamLamViec 
		AND iTrangThai = 1
		AND (
			(@PhanCap = 'M' AND (sTM IS NULL OR sTM = ''))
			OR (@PhanCap = 'TM' AND (sTTM IS NULL OR sTTM = ''))
			OR (@PhanCap = 'NG' AND (sTNG IS NULL OR sTNG = ''))
			)
		AND sLNS IN (
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
							AND INamLamViec = @NamLamViec
							AND sLNS IN (SELECT * FROM #tblLNS)
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
		INamLamViec = @NamLamViec 
		AND iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))

	-- map bảng mlnsPhanCap và đơn vị
	SELECT * INTO #tblMLNS FROM (
		SELECT * FROM #tblMlnsByPhanCap, #tblDonVi 
		WHERE bHangCha = 0
		UNION ALL
		SELECT *, null AS iID_MaDonVi, null AS sTenDonVi  
		FROM #tblMlnsByPhanCap 
		WHERE bHangCha = 1
	) mlns

	SELECT 
		mlns.iID_MLNS AS MlnsId,
		mlns.iID_MLNS_Cha AS MlnsIdCha,
		mlns.sLNS AS LNS,
		mlns.sL AS L,
		mlns.sK AS K,
		mlns.sM AS M,
		mlns.sTM AS TM,
		mlns.sTTM AS TTM,
		mlns.sNG AS NG,
		mlns.sMoTa AS MoTa,
		mlns.sXauNoiMa AS XauNoiMa,
		mlns.bHangCha as BHangCha,
		mlns.iID_MaDonVi AS MaDonVi,
		mlns.sTenDonVi AS TenDonVi,
		ROUND((isnull(capPhatDuToan.CapPhat, 0) / @Dvt), 0) as CapPhat,
		ROUND((isnull(capPhatDuToan.DuToan, 0) / @Dvt), 0) as DuToan
	FROM #tblMlns AS mlns
	left JOIN #tblCapPhatDuToanResult capPhatDuToan
	on mlns.iID_MLNS = capPhatDuToan.iID_MLNS and capPhatDuToan.iID_MaDonVi LIKE '%' + mlns.iID_MaDonVi + '%' 
	WHERE bHangCha = 1 or (CapPhat <> 0 OR DuToan <> 0)
	order by mlns.sXauNoiMa, mlns.iID_MaDonVi;
	

	drop table #tblMlnsByPhanCap;
	drop table #tblDonVi;
	drop table #tblMLNS;
	drop table #tblDataCapPhat;
	drop table #tblPhanBoDuToan;
	drop table #tblDataDuToan;
	drop table #tblMlnsCapPhat;
	drop table #tblMlnsDuToan;
	drop table #tblMlnsCapPhatDuToan;
	drop table #tblDataCapPhatDuToan;
	drop table #tblCapPhatDuToan;
	drop table #tblCapPhatDuToanResult;
END
;
;
GO
