/****** Object:  StoredProcedure [dbo].[sp_dt_dutoan_dotnhan_phanbo_find_all]    Script Date: 27/12/2022 5:40:37 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_dutoan_dotnhan_phanbo_find_all]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_dutoan_dotnhan_phanbo_find_all]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_danhsach_chungtu_chitiet]    Script Date: 27/12/2022 5:40:37 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_danhsach_chungtu_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_danhsach_chungtu_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chitiet]    Script Date: 27/12/2022 5:40:37 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_chungtu_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_chungtu_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_check_data_used_mlns]    Script Date: 27/12/2022 5:40:37 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_check_data_used_mlns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_check_data_used_mlns]
GO
/****** Object:  StoredProcedure [dbo].[sp_check_data_used_mlns]    Script Date: 27/12/2022 5:40:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_check_data_used_mlns] 
	@YearOfWork int,
	@CodeChain nvarchar(max),
	@Type bit

AS
BEGIN

	SET NOCOUNT ON;

	IF (@Type = 0)

	SELECT
		dulieu.ID,
		dulieu.ID_Parent,
		dulieu.LoaiChungTu,
		dulieu.Loai,
		dulieu.SoChungTu,
		dulieu.NgayChungTu,
		dulieu.SoQuyetDinh,
		dulieu.NgayQuyetDinh,
		dulieu.MaDonVi,
		donvi.sTenDonVi AS TenDonVi,
		dulieu.MoTa,
		dulieu.SoTien
	FROM (
	SELECT 
	t1.iID_CTDTDauNamChiTiet AS ID,
	t3.iID_CTDTDauNam AS ID_Parent,
	N'DTDN' AS LoaiChungTu,
	'DU_TOAN_DAU_NAM' AS Loai,
	t3.sSoChungTu AS SoChungTu,
	t3.dNgayChungTu AS NgayChungTu,
	'' AS SoQuyetDinh,
	NULL AS NgayQuyetDinh,
	t1.iID_MaDonVi AS MaDonVi,
	t3.sMoTa AS MoTa,
	(t1.fTuChi + t1.fHienVat + t1.fHangNhap + t1.fHangMua + t1.fPhanCap + ISNULL(t2.fTuChi, 0)) AS SoTien
	FROM NS_DTDauNam_ChungTuChiTiet t1
	LEFT JOIN NS_DTDauNam_PhanCap t2 ON t1.iID_CTDTDauNamChiTiet = t2.iID_CTDTDauNamChiTiet
	JOIN NS_DTDauNam_ChungTu t3 ON t1.iID_CTDTDauNam = t3.iID_CTDTDauNam
	WHERE t1.iNamLamViec = @YearOfWork AND t1.sXauNoiMa IN (SELECT * FROM f_split(@CodeChain))

	UNION

	SELECT
	t1.iID_DTCTChiTiet AS ID,
	t2.iID_DTChungTu AS ID_Parent,
	N'Nhận DT' AS LoaiChungTu,
	'NHAN_DU_TOAN' AS Loai,
	t2.sSoChungTu AS SoChungTu,
	t2.dNgayChungTu AS NgayChungTu,
	t2.sSoQuyetDinh AS SoQuyetDinh,
	t2.dNgayQuyetDinh AS NgayQuyetDinh,
	t1.iID_MaDonVi AS MaDonVi,
	t2.sMoTa AS MoTa,
	(t1.fTuChi + t1.fHienVat + t1.fHangNhap + t1.fHangMua + t1.fPhanCap) AS SoTien
	FROM NS_DT_ChungTuChiTiet t1
	JOIN NS_DT_ChungTu t2 ON t1.iID_DTChungTu = t2.iID_DTChungTu AND t2.iLoai = 0 AND t1.iDuLieuNhan = 0
	WHERE t1.iNamLamViec = @YearOfWork AND t1.sXauNoiMa IN (SELECT * FROM f_split(@CodeChain))

	UNION

	SELECT
	t1.iID_DTCTChiTiet AS ID,
	t2.iID_DTChungTu AS ID_Parent,
	N'Phân bổ DT' AS LoaiChungTu,
	'PHAN_BO_DU_TOAN' AS Loai,
	t2.sSoChungTu AS SoChungTu,
	t2.dNgayChungTu AS NgayChungTu,
	t2.sSoQuyetDinh AS SoQuyetDinh,
	t2.dNgayQuyetDinh AS NgayQuyetDinh,
	t1.iID_MaDonVi AS MaDonVi,
	t2.sMoTa AS MoTa,
	(t1.fTuChi + t1.fHienVat + t1.fHangNhap + t1.fHangMua + t1.fPhanCap) AS SoTien
	FROM NS_DT_ChungTuChiTiet t1
	JOIN NS_DT_ChungTu t2 ON t1.iID_DTChungTu = t2.iID_DTChungTu AND t2.iLoai = 1
	WHERE t1.iNamLamViec = @YearOfWork AND t1.sXauNoiMa IN (SELECT * FROM f_split(@CodeChain))

	UNION

	SELECT
	t1.iID_CTNganhChiTiet AS ID,
	t3.iID_CTNganh AS ID_Parent, 
	N'Phân cấp NS ngành' AS LoaiChungTu,
	'PHAN_CAP_NGAN_SACH_NGANH' AS Loai,
	t3.sSoChungTu AS SoChungTu,
	t3.dNgayChungTu AS NgayChungTu,
	t3.sSoCongVan AS SoQuyetDinh,
	t3.dNgayQuyetDinh AS NgayQuyetDinh,
	t1.iiD_MaDonVi AS MaDonVi,
	t3.sMoTa AS MoTa,
	(t1.fTuChi + t1.fHangNhap + t1.fHangMua + t1.fPhanCap + t2.fHienVat + t2.fPhanCap) AS SoTien
	FROM NS_Nganh_ChungTuChiTiet t1
	LEFT JOIN NS_Nganh_ChungTuChiTiet_PhanCap t2 ON t1.iID_CTNganhChiTiet = t2.iID_CTNganhChiTiet
	JOIN NS_Nganh_ChungTu t3 ON t1.iID_CTNganh = t3.iID_CTNganh
	WHERE t1.iNamLamViec = @YearOfWork AND t1.sXauNoiMa IN (SELECT * FROM f_split(@CodeChain))

	) dulieu
	LEFT JOIN DonVi donvi ON dulieu.MaDonVi = donvi.iID_MaDonVi AND donvi.iNamLamViec = @YearOfWork
	ORDER BY LoaiChungTu, NgayChungTu, SoChungTu
	
	ELSE

	SELECT
		dulieu.ID,
		dulieu.ID_Parent,
		dulieu.SoChungTu,
		dulieu.NgayChungTu,
		dulieu.MaDonVi,
		donvi.sTenDonVi AS TenDonVi,
		dulieu.ThangQuy,
		dulieu.Loai,
		dulieu.LoaiQuyetToan,
		dulieu.MoTa,
		dulieu.SoTien
	FROM (
	SELECT
	t1.iID_QTCTChiTiet AS ID,
	t2.iID_QTChungTu AS ID_Parent,
	t2.sSoChungTu AS SoChungTu,
	t2.dNgayChungTu AS NgayChungTu,
	t2.iID_MaDonVi AS MaDonVi,
	t2.iThangQuy AS ThangQuy,
	'QUYET_TOAN' AS Loai,
	CASE
		WHEN t2.sLoai = '101' THEN N'Thường xuyên'
		WHEN t2.sLoai = '1' THEN N'Quốc phòng'
		WHEN t2.sLoai = '2' THEN N'Nhà nước'
		WHEN t2.sLoai = '3' THEN N'Ngoại hối'
		WHEN t2.sLoai = '4' THEN N'Kinh phí khác'
		ELSE ''
	END AS LoaiQuyetToan,
	t2.sMoTa AS MoTa,
	t1.fTuChi_PheDuyet AS SoTien
	FROM NS_QT_ChungTuChiTiet t1
	JOIN NS_QT_ChungTu t2 ON t1.iID_QTChungTu = t2.iID_QTChungTu
	WHERE t1.iNamLamViec = @YearOfWork AND t1.sXauNoiMa IN (SELECT * FROM f_split(@CodeChain))
	) dulieu
	LEFT JOIN DonVi donvi ON dulieu.MaDonVi = donvi.iID_MaDonVi AND donvi.iNamLamViec = @YearOfWork
	ORDER BY LoaiQuyetToan, NgayChungTu, SoChungTu
END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chitiet]    Script Date: 27/12/2022 5:40:37 PM ******/
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
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_danhsach_chungtu_chitiet]    Script Date: 27/12/2022 5:40:37 PM ******/
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
			ON dt.iID_DTChungTu = ctct.iID_DTChungTu
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
/****** Object:  StoredProcedure [dbo].[sp_dt_dutoan_dotnhan_phanbo_find_all]    Script Date: 27/12/2022 5:40:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_dutoan_dotnhan_phanbo_find_all]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@Type int,
	@VoucherType int,
	@Date datetime,
	@Index int
AS
BEGIN
	DECLARE @DieuChinh int = 4;
	WITH tblNhanPhanBo AS (
		SELECT *, 
			   fTongTuChi + fTongHienVat + fTongPhanCap + fTongHangNhap + fTongHangMua + fTongDuPhong + fTongTonKho AS SoPhanBo
		FROM NS_DT_ChungTu 
		WHERE iNamLamViec = @YearOfWork 
			AND iNamNganSach = @YearOfBudget 
			AND iID_MaNguonNganSach = @BudgetSource
			AND iLoai = @Type 
			AND iLoaiChungTu = @VoucherType
			AND (fTongTuChi <> 0 OR fTongHienVat <> 0 OR fTongPhanCap <> 0 OR fTongHangNhap <> 0 OR fTongHangMua <> 0 OR fTongDuPhong <> 0 OR fTongTonKho <> 0)
			AND (
				(dNgayQuyetDinh IS NOT NULL AND CAST(dNgayQuyetDinh AS DATE) <= CAST(@Date AS DATE)) 
				OR 
				(dNgayQuyetDinh IS NULL AND dNgayChungTu IS NOT NULL AND CAST(dNgayChungTu AS DATE) <= CAST(@Date AS DATE)))
	),
	tblChungTuNhanPhanBoMap AS (
		SELECT * 
		FROM NS_DT_Nhan_PhanBo_Map 
		WHERE iID_CTDuToan_Nhan IN (SELECT iID_DTChungTu FROM tblNhanPhanBo)
	),
	tblDaPhanBo AS (
		SELECT ctpb.iID_CTDuToan_Nhan, 
			   SUM(fTuChi) + SUM(fHienVat) + SUM(fPhanCap) + SUM(fHangNhap) + SUM(fHangMua) + SUM(fDuPhong) + SUM(fTonKho) AS DaPhanBo 
		FROM tblChungTuNhanPhanBoMap ctpb
		LEFT JOIN 
			(SELECT dtctct.*, dtct.iLoaiDuToan, dtct.iID_DotNhan FROM NS_DT_ChungTuChiTiet dtctct
			 LEFT JOIN NS_DT_ChungTu dtct
			 ON dtctct.iID_DTChungTu = dtct.iID_DTChungTu
			 WHERE dtctct.iNamLamViec = @YearOfWork 
				   AND dtct.iLoai = 1
				   AND dtct.iLoaiChungTu = 1
				   AND (dNgayQuyetDinh IS NOT NULL AND ((CAST(dNgayQuyetDinh AS DATE) < CAST(@Date AS DATE) AND (@Index <> iSoChungTuIndex)) OR (CAST(dNgayQuyetDinh AS DATE) = CAST(@Date AS DATE) AND @Index > iSoChungTuIndex))) 
				   OR (dNgayQuyetDinh IS NULL AND dNgayChungTu IS NOT NULL AND ((CAST(dNgayChungTu AS DATE) < CAST(@Date AS DATE) AND (@Index <> iSoChungTuIndex)) OR (CAST(dNgayChungTu AS DATE) = CAST(@Date AS DATE) AND @Index > iSoChungTuIndex)))
				   AND dtctct.iNamNganSach = @YearOfBudget 
				   AND dtctct.iID_MaNguonNganSach = @BudgetSource) ctct
		ON ctpb.iID_CTDuToan_Nhan = ctct.iID_CTDuToan_Nhan
		WHERE ctct.iID_DTChungTu IS NOT NULL
		and 
		(
			(ctpb.iID_CTDuToan_PhanBo = ctct.iID_DTChungTu AND ctct.iLoaiDuToan <> @DieuChinh)
			OR
			(ctct.iLoaiDuToan = @DieuChinh AND ctct.iID_DotNhan = ctpb.iID_CTDuToan_PhanBo)
		)
		GROUP BY ctpb.iID_CTDuToan_Nhan
	)

	SELECT 
		   npb.iID_DTChungTu AS Id,
		   npb.sSoChungTu AS SSoChungTu,
		   npb.dNgayChungTu AS DNgayChungTu,
		   npb.sDSLNS AS SDslns,
		   npb.sDSID_MaDonVi AS SDsidMaDonVi,
		   npb.sSoQuyetDinh AS SSoQuyetDinh,
		   npb.dNgayQuyetDinh AS DNgayQuyetDinh,
		   ISNULL(npb.SoPhanBo, 0) AS SoPhanBo,
		   ISNULL(DaPhanBo, 0) AS DaPhanBo,
		   ISNULL(npb.SoPhanBo, 0) - ISNULL(DaPhanBo, 0) AS SoChuaPhanBo 
	FROM tblNhanPhanBo npb
	LEFT JOIN tblDaPhanBo dpb
	ON npb.iID_DTChungTu = dpb.iID_CTDuToan_Nhan
	WHERE ISNULL(npb.SoPhanBo, 0) <> ISNULL(DaPhanBo, 0) OR ISNULL(npb.SoPhanBo, 0) = 0
	ORDER BY npb.sSoChungTu

END
;
;
;
;
;
;
;
GO
