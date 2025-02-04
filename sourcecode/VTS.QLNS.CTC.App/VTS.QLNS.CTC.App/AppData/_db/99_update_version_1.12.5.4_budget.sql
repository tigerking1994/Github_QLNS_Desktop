/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_thongtri_donvi_1]    Script Date: 01/02/2023 6:09:30 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_thongtri_donvi_1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_thongtri_donvi_1]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_thongtri_donvi]    Script Date: 01/02/2023 6:09:30 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_thongtri_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_thongtri_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_quyettoan_giaithichbangso]    Script Date: 01/02/2023 6:09:30 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_rpt_quyettoan_giaithichbangso]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_rpt_quyettoan_giaithichbangso]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chitiet]    Script Date: 01/02/2023 6:09:30 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_chungtu_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_chungtu_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chitiet]    Script Date: 01/02/2023 6:09:30 PM ******/
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
		(
			--(@PhanCap = 'M' AND (sTM IS NULL OR sTM = ''))
			--OR (@PhanCap = 'TM' AND (sTTM IS NULL OR sTTM = ''))
			--OR (@PhanCap = 'NG' AND (sTNG IS NULL OR sTNG = ''))
			sTNG IS NULL OR sTNG = ''
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
		isnull(ctct.fTuChi, 0) AS TuChi,
		isnuLL(ctct.fDeNghiDonVi, 0) AS DeNghiDonVi,
		isnull(ctct.fHienVat, 0) AS HienVat,
		isnull(daCapDuToan.DaCap, 0) as DaCap,
		isnull(daCapDuToan.DuToan, 0) as DuToan,
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
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_quyettoan_giaithichbangso]    Script Date: 01/02/2023 6:09:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_ns_rpt_quyettoan_giaithichbangso]
@sLoai nvarchar(50),
@iID_MaDonVi nvarchar(50),
@iThang int,
@iQuy int,
@iNamLamViec int,
@iNamNganSach int,
@iID_NguonNganSach int,
@isTongHop int,
@explainId nvarchar(50)
as 
begin
	select 
				   --Tiền lương tháng này
				   sum(fLuong_SiQuan) as FLuongSiQuan, sum(fLuong_QNCN) as FLuongQncn, sum(fLuong_CNVQP) as FLuongCnvqp, sum(fLuong_HD) as FLuongHd,
				   sum(fPhuCap_SiQuan) as FPhuCapSiQuan, sum(fPhuCap_QNCN) as FPhuCapQncn, sum(fPhuCap_CNVQP) as FPhuCapCnvqp, sum(fPhuCap_HD) as FPhuCapHd,

				   --Tiền lương của những ngày nghỉ không lương
				   sum(fLuong_SiQuan_Tru) as FLuongSiQuanTru, sum(fLuong_QNCN_Tru) as FLuongQncnTru, sum(fLuong_CNVQP_Tru) as FLuongCnvqpTru, sum(fLuong_HD_Tru) as FLuongHdTru,
				   sum(fPhuCap_SiQuan_Tru) as FPhuCapSiQuanTru, sum(fPhuCap_QNCN_Tru) as FPhuCapQncnTru, sum(fPhuCap_CNVQP_Tru) as FPhuCapCnvqpTru, sum(fPhuCap_HD_Tru) as FPhuCapHdTru,

				   --Tiền lương của những ngày hưởng trợ cấp BHXH
				   sum(fLuongBHXH_SiQuan_Tru) as FLuongBhxhSiQuanTru, sum(fLuongBHXH_QNCN_Tru) as FLuongBhxhQncnTru, sum(fLuongBHXH_CNVQP_Tru) as FLuongBhxhCnvqpTru, sum(fLuongBHXH_HD_Tru) as FLuongBhxhHdTru,
				   sum(fPhuCapBHXH_SiQuan_Tru) as FPhuCapBhxhSiQuanTru, sum(fPhuCapBHXH_QNCN_Tru) as FPhuCapBhxhQncnTru, sum(fPhuCapBHXH_CNVQP_Tru) as FPhuCapBhxhCnvqpTru, sum(fPhuCapBHXH_HD_Tru) as FPhuCapBhxhHdTru,

				   --Tiền lương quyết toán tháng này
				   sum(fLuong_SiQuan_QT) as FLuongSiQuanQt, sum(fLuong_QNCN_QT) as FLuongQncnQt, sum(fLuong_CNVQP_QT) as FLuongCnvqpQt, sum(fLuong_HD_QT) as FLuongHdQt,
				   sum(fPhuCap_SiQuan_QT) as FPhuCapSiQuanQt, sum(fPhuCap_QNCN_QT) as FPhuCapQncnQt, sum(fPhuCap_CNVQP_QT) as FPhuCapCnvqpQt, sum(fLuong_HD_QT) as FPhuCapHdQt,

				   --Kinh phí không thực hiện tự chủ
				   sum(fKinhPhi_LuongPC_Khac) as FKinhPhiLuongPcKhac,
				   sum(fKinhPhi_PhuCap_HSQBS) as FKinhPhiPhuCapHsqbs,
				   sum(fKinhPhi_An) as FKinhPhiAn,

				   --Quân số cung cấp tiền ăn
				   sum(fNgayAn) as FNgayAn,
				   sum(fNgayAn_Cong) as FNgayAnCong,
				   sum(fNgayAn_Tru) as FNgayAnTru,
				   sum(fNgayAn_QT) as FNgayAnQt,

				   --Ra quân trong tháng
				   sum(fRaQuan_SiQuan_Nguoi_XuatNgu) as FRaQuanSiQuanNguoiXuatNgu, sum(fRaQuan_SiQuan_Tien_XuatNgu) as FRaQuanSiQuanTienXuatNgu, sum(fRaQuan_QNCN_Nguoi_XuatNgu) as FRaQuanQncnNguoiXuatNgu, 
				   sum(fRaQuan_QNCN_Tien_XuatNgu) as FRaQuanQncnTienXuatNgu, sum(fRaQuan_CNVQP_Nguoi_XuatNgu) as FRaQuanCnvqpNguoiXuatNgu, sum(fRaQuan_CNVQP_Tien_XuatNgu) as FRaQuanCnvqpTienXuatNgu,
				   sum(fRaQuan_HSQCS_Nguoi_XuatNgu) as FRaQuanHsqcsNguoiXuatNgu , sum(fRaQuan_HSQCS_Tien_XuatNgu) as FRaQuanHsqcsTienXuatNgu,

				   sum(fRaQuan_SiQuan_Nguoi_Huu) as FRaQuanSiQuanNguoiHuu, sum(fRaQuan_SiQuan_Tien_Huu) as FRaQuanSiQuanTienHuu, sum(fRaQuan_QNCN_Nguoi_Huu) as FRaQuanQncnNguoiHuu, 
				   sum(fRaQuan_QNCN_Tien_Huu) as FRaQuanQncnTienHuu, sum(fRaQuan_CNVQP_Nguoi_Huu) as FRaQuanCnvqpNguoiHuu, sum(fRaQuan_CNVQP_Tien_Huu) as FRaQuanCnvqpTienHuu,
				   sum(fRaQuan_HSQCS_Nguoi_Huu) as FRaQuanHsqcsNguoiHuu , sum(fRaQuan_HSQCS_Tien_Huu) as FRaQuanHsqcsTienHuu,
		   
				   sum(fRaQuan_SiQuan_Nguoi_ThoiViec) as FRaQuanSiQuanNguoiThoiViec, sum(fRaQuan_SiQuan_Tien_ThoiViec) as FRaQuanSiQuanTienThoiViec, sum(fRaQuan_QNCN_Nguoi_ThoiViec) as FRaQuanQncnNguoiThoiViec, 
				   sum(fRaQuan_QNCN_Tien_ThoiViec) as FRaQuanQncnTienThoiViec, sum(fRaQuan_HSQCS_Nguoi_ThoiViec) as FRaQuanCnvqpNguoiThoiViec, sum(fRaQuan_CNVQP_Tien_ThoiViec) as FRaQuanCnvqpTienThoiViec,
				   sum(fRaQuan_HSQCS_Nguoi_ThoiViec) as FRaQuanHsqcsNguoiThoiViec , sum(fRaQuan_HSQCS_Tien_ThoiViec) as FRaQuanHsqcsTienThoiViec


			from  NS_QT_ChungTuChiTiet_GiaiThich ctgt
			inner join  NS_QT_ChungTu ct on ctgt.iID_QTChungTu = ct.iID_QTChungTu
			where ctgt.iNamLamViec = @iNamLamViec
			and ct.iNamNganSach = @iNamNganSach
			and ct.iID_MaNguonNganSach = @iID_NguonNganSach
			and ( (@iQuy = 0 and ct.iThangQuy = @iThang) or
				  (@iQuy = 1 and ((@iThang = 3 and ct.iThangQuy in (1,2,3)) or (@iThang = 6 and ct.iThangQuy in (4,5,6)) or (@iThang = 9 and ct.iThangQuy in (7,8,9)) or (@iThang = 12 and ct.iThangQuy in (10,11,12))))
				)
			and ctgt.iID_QTChungTu in ( select iID_QTChungTu from NS_QT_ChungTu where iID_MaDonVi = @iID_MaDonVi and sLoai = @sLoai)  
			group by ctgt.iID_MaDonVi
	
end
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_thongtri_donvi]    Script Date: 01/02/2023 6:09:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qt_rpt_thongtri_donvi]
	@YearOfWork int,
	@BudgetSource int,
	@AgencyId nvarchar(max),
	@QuarterMonth nvarchar(100),
	@LNS nvarchar(max),
	@Dvt int
AS
BEGIN
	SELECT *
	FROM
	  (SELECT iID_MaDonVi,
			  TuChi = SUM(fTuChi_PheDuyet) / @Dvt,
			  cast(0 AS float) AS HienVat,
			  SoNguoi = SUM(fSoNguoi),
			  SoNgay = SUM(fSoNgay),
			  SoLuot = SUM(fSoLuot)
	   FROM NS_QT_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
		 AND iID_MaNguonNganSach = @BudgetSource
		 AND (@AgencyId IS NULL OR iID_MaDonVi in (SELECT * FROM f_split(@AgencyId)))
		 AND (@QuarterMonth IS NULL OR iThangQuy in (SELECT * FROM f_split(@QuarterMonth)))
		 AND (@LNS IS NULL  OR sLNS in (SELECT *  FROM f_split(@LNS)))
	   GROUP BY iID_MaDonVi)AS ct 
	-- lay ten don vi
	LEFT JOIN
	  (SELECT sTenDonVi,
			  iID_MaDonVi AS dv_id
	   FROM DonVi
	   WHERE iTrangThai = 1
		 AND iNamLamViec = @YearOfWork) AS dv ON dv.dv_id = ct.iID_MaDonVi
	ORDER BY iID_MaDonVi
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_thongtri_donvi_1]    Script Date: 01/02/2023 6:09:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qt_rpt_thongtri_donvi_1]
	@YearOfWork int,
	@BudgetSource int,
	@AgencyId nvarchar(max),
	@QuarterMonth nvarchar(100),
	@LNS nvarchar(max),
	@Dvt int,
	@IsInTongHop bit, 
	@IKhoi int
AS
BEGIN
declare @strChungTu nvarchar (500)
set @strChungTu=  (select sTongHop + ',' as 'data()' from NS_QT_ChungTu where  iID_MaDonVi in ( SELECT * FROM f_split(@AgencyId))  FOR XML PATH(''));
	if (@IsInTongHop = 0 OR @strChungTu = '')
		SELECT *
		FROM
		  (SELECT iID_MaDonVi,
				  TuChi = SUM(fTuChi_PheDuyet) / @Dvt,
				  cast(0 AS float) AS HienVat,
				  SoNguoi = SUM(fSoNguoi),
				  SoNgay = SUM(fSoNgay),
				  SoLuot = SUM(fSoLuot)
		   FROM NS_QT_ChungTuChiTiet
		   WHERE iNamLamViec = @YearOfWork
			 AND iID_MaNguonNganSach = @BudgetSource
			 AND (@AgencyId IS NULL OR iID_MaDonVi in (SELECT * FROM f_split(@AgencyId)))
			 AND (@QuarterMonth IS NULL OR iThangQuy in (SELECT * FROM f_split(@QuarterMonth)))
			 AND (@LNS IS NULL  OR sLNS in (SELECT *  FROM f_split(@LNS)))
		   GROUP BY iID_MaDonVi)AS ct 
		-- lay ten don vi
		LEFT JOIN
		  (SELECT sTenDonVi,
				  iID_MaDonVi AS dv_id
		   FROM DonVi
		   WHERE iTrangThai = 1
			 AND iNamLamViec = @YearOfWork) AS dv ON dv.dv_id = ct.iID_MaDonVi
		ORDER BY iID_MaDonVi
	else 

	SELECT *
		FROM
		  (SELECT ctct.iID_MaDonVi,
				  TuChi = SUM(fTuChi_PheDuyet) / @Dvt,
				  cast(0 AS float) AS HienVat,
				  SoNguoi = SUM(fSoNguoi),
				  SoNgay = SUM(fSoNgay),
				  SoLuot = SUM(fSoLuot)
		   FROM NS_QT_ChungTuChiTiet ctct inner join ns_qt_Chungtu ct on  ctct.iID_QTChungTu =  ct.iID_QTChungTu 
		   WHERE ctct.iNamLamViec = @YearOfWork
			 AND ctct.iID_MaNguonNganSach = @BudgetSource
			 --AND (@AgencyId IS NULL OR ctct.iID_MaDonVi in  (select DonVi.iID_MaDonVi from DonVi  where DonVi.iID_Parent in ( SELECT * FROM f_split(@AgencyId))))
			 AND ct.bdatonghop = 1
			 AND (@QuarterMonth IS NULL OR ctct.iThangQuy in (SELECT * FROM f_split(@QuarterMonth)))
			 AND (@LNS IS NULL  OR sLNS in (SELECT *  FROM f_split(@LNS)))
			 AND ct.sSoChungTu in (select * from f_split(Replace(@strChungTu, ' ', '')))
		   GROUP BY ctct.iID_MaDonVi)AS ct 
		-- lay ten don vi
		--LEFT JOIN
		INNER JOIN
		  (SELECT sTenDonVi,
				  iID_MaDonVi AS dv_id
		   FROM DonVi
		   WHERE iTrangThai = 1
		     AND (@IKhoi = -1 OR iKhoi = @IKhoi)
			 AND iNamLamViec = @YearOfWork) AS dv ON dv.dv_id = ct.iID_MaDonVi
		ORDER BY iID_MaDonVi
END
;
;


GO
