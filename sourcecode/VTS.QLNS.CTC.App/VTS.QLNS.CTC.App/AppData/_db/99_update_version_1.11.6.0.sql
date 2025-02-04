/****** Object:  StoredProcedure [dbo].[sp_vdt_denghithanhtoan_by_thongtri]    Script Date: 15/08/2022 8:34:51 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_denghithanhtoan_by_thongtri]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_denghithanhtoan_by_thongtri]
GO
/****** Object:  StoredProcedure [dbo].[sp_get_all_du_an_kehoachtrunghan_dexuat_chuyentiep_dieuchinh]    Script Date: 15/08/2022 8:34:51 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_get_all_du_an_kehoachtrunghan_dexuat_chuyentiep_dieuchinh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_get_all_du_an_kehoachtrunghan_dexuat_chuyentiep_dieuchinh]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_get_donvi_2]    Script Date: 15/08/2022 8:34:51 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_get_donvi_2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_get_donvi_2]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chitiet]    Script Date: 15/08/2022 8:34:51 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_chungtu_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_chungtu_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chitiet]    Script Date: 15/08/2022 8:34:51 AM ******/
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
	SELECT @CapPhatIndex = iSoChungTuIndex
	FROM NS_CP_ChungTu
	WHERE iID_CTCapPhat = @VoucherId

	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha
			into #tblNsMlns
	FROM NS_MucLucNganSach 
	where iNamLamViec = @YearOfWork 

	-- lấy ra mlns theo phân cấp
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa,
			CASE
				WHEN @PhanCap = 'M' and (sM is null OR sM = '') THEN cast(1 as bit)
				WHEN @PhanCap = 'TM' and (sTM is null OR sTM = '') THEN cast(1 as bit)
				WHEN @PhanCap = 'NG' and (sNG is null OR sNG = '') THEN cast(1 as bit)
			ELSE cast(0 as bit)
			END AS bHangCha	
			into #tblMlnsByPhanCap
	FROM #tblNsMlns 
	WHERE 
		(
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
							AND INamLamViec = @YearOfWork
							AND sLNS IN (SELECT * FROM f_split(@LNS))
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
		AND iID_MaDonVi IN (SELECT * FROM f_split(@AgencyId))

	-- map bảng mlnsPhanCap và đơn vị
	SELECT * INTO #tblMLNS FROM (
		SELECT * FROM #tblMlnsByPhanCap, #tblDonVi 
		WHERE bHangCha = 0
		UNION ALL
		SELECT *, null AS iID_MaDonVi, null AS sTenDonVi  
		FROM #tblMlnsByPhanCap 
		WHERE bHangCha = 1
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
		  AND EXISTS(SELECT * FROM f_split(@AgencyId) INTERSECT SELECT * FROM f_split(sDSID_MaDonVi))
		)
		AND iID_MaDonVi IN (SELECT * FROM f_split(@AgencyId))
	GROUP BY iID_MaDonVi, iID_MLNS, sLNS;

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
          AND iNamLamViec = @YearOfWork
		  -- sua lay theo dung nam ngan sách--
		  AND iNamNganSach = @YearOfBudget
          --AND ((@YearOfBudget = 2
          --      AND (iNamNganSach = 2
          --           OR iNamNganSach = 4))
          --     OR (@YearOfBudget <> 2
          --         AND iNamNganSach = @YearOfBudget))
          AND iID_MaNguonNganSach = @BudgetSource
          AND cast(dNgayQuyetDinh AS date) <= cast(@VoucherDate AS date))
     AND iID_MaDonVi in  (SELECT * FROM f_split(@AgencyId))
   GROUP BY iID_MaDonVi, iID_MLNS, sXauNoiMa, sLNS

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

	;WITH C AS  
	(  
	  SELECT T.iID_MLNS,  
			 T.DaCap, 
			 T.DuToan, 
			 T.iID_MLNS AS RootID,
			 T.iID_MaDonVi
	  FROM #tblDaCapDuToan T
	  UNION ALL 
	  SELECT T.iID_MLNS,  
			 T.DaCap,
			  T.DuToan, 
			 C.RootID,
			 T.iID_MaDonVi
	  FROM #tblDaCapDuToan T
		INNER JOIN C
		  on T.iID_MLNS_Cha = C.iID_MLNS 
	)

	SELECT iID_MLNS, iID_MLNS_Cha, iID_MaDonVi, sum(DaCap) AS DaCap, sum(DuToan) AS DuToan INTO #tblDaCapDuToanResult FROM (
		SELECT distinct T.iID_MLNS,  
			   T.iID_MLNS_Cha,
			   isnull(T.iID_MaDonVi, total.iID_MaDonVi) as iID_MaDonVi,
			   total.DaCap,
			   total.DuToan
		FROM #tblDaCapDuToan T  
		LEFT JOIN (  
					 SELECT RootID, iID_MaDonVi, DaCap,  DuToan  
					 FROM C
					 ) AS total 
		ON T.iID_MLNS = total.RootID AND (T.iID_MaDonVi = total.iID_MaDonVi OR T.iID_MaDonVi IS NULL)
		WHERE total.DaCap <> 0 OR total.DuToan <> 0) data
	GROUP BY iID_MLNS, iID_MLNS_Cha, iID_MaDonVi
	option (maxrecursion 0)


	SELECT 
		isnull(ctct.iID_CTCapPhatChiTiet, NEWID()) AS Id,
		ctct.iID_CTCapPhat AS IdChungTu,
		mlns.iID_MLNS AS MlnsId,
		mlns.iID_MLNS_Cha AS MlnsIdParent,
		mlns.sXauNoiMa AS XauNoiMa,
		mlns.sLNS AS LNS,
		mlns.sL AS L,
		mlns.sK AS K,
		mlns.sM AS M,
		mlns.sTM AS TM,
		mlns.sTTM AS TTM,
		mlns.sNG AS NG,
		mlns.sTNG AS TNG,
		mlns.sTNG1 AS TNG1,
		mlns.sTNG2 AS TNG2,
		mlns.sTNG3 AS TNG3,
		mlns.sMoTa AS MoTa,
		'' Chuong, 
		mlns.bHangCha,
		@YearOfWork AS NamLamViec,
		 @YearOfBudget AS NamNganSach,
		 @BudgetSource AS NguonNganSach,
		ctct.iLoai,
		 mlns.iID_MaDonVi AS IdDonVi,
		mlns.sTenDonVi AS TenDonVi,
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
	FROM #tblMlns AS mlns
	left JOIN
		(SELECT *
			FROM 
				NS_CP_ChungTuChiTiet
			WHERE 
		 		iID_CTCapPhat = @VoucherId
		 		AND INamLamViec = @YearOfWork
		 		AND iID_MaNguonNganSach = @BudgetSource
				AND iNamNganSach = @YearOfBudget
		) ctct
	on mlns.iID_MLNS = ctct.iID_MLNS and mlns.iID_MaDonVi = ctct.iID_MaDonVi
	left JOIN #tblDaCapDuToanResult daCapDuToan
	on mlns.iID_MLNS = daCapDuToan.iID_MLNS and daCapDuToan.iID_MaDonVi LIKE '%' + mlns.iID_MaDonVi + '%' 
	order by mlns.sXauNoiMa, mlns.iID_MaDonVi;
	

	drop table #tblMlnsByPhanCap;
	drop table #tblDonVi;
	drop table #tblMLNS;
	drop table #tblDataDaCap;
	drop table #tblPhanBoDuToan;
	drop table #tblDataDuToan;
	drop table #tblDataDaCapDuToan;
	drop table #tblDaCapDuToan;
	drop table #tblDaCapDuToanResult;
	drop table #tblMlnsRoot;
	drop table #tblNsMlns;
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_get_donvi_2]    Script Date: 15/08/2022 8:34:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_cp_get_donvi_2]
	@YearOfWork int,
	@CapPhatIds nvarchar(max),
	@ILoaiNganSach int
AS
BEGIN
	SELECT dv.* 
	FROM 
	(
		SELECT DISTINCT ct.iID_MaDonVi 
		FROM NS_CP_ChungTuChiTiet ct
		INNER JOIN 
		(SELECT * FROM NS_MucLucNganSach WHERE iNamLamViec = 2022) ns 
		ON ct.iID_MLNS = ns.iID_MLNS
		WHERE
			iID_CTCapPhat IN (SELECT * FROM f_split(@CapPhatIds))
			AND (@ILoaiNganSach = -1 OR ns.iLoaiNganSach = @ILoaiNganSach) 
	) ctct
	LEFT JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork) dv
	on dv.iID_MaDonVi = ctct.iID_MaDonVi
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_get_all_du_an_kehoachtrunghan_dexuat_chuyentiep_dieuchinh]    Script Date: 15/08/2022 8:34:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_get_all_du_an_kehoachtrunghan_dexuat_chuyentiep_dieuchinh]
	@IdDonVi nvarchar(max)
AS
BEGIN
	SELECT DISTINCT dt.iID_DuAnID INTO #tmpDuAnKHTH
	FROM VDT_KHV_KeHoach5Nam as tbl
	INNER JOIN VDT_KHV_KeHoach5Nam_ChiTiet as dt on tbl.iID_KeHoach5NamID = dt.iID_KeHoach5NamID
	WHERE tbl.bActive = 1

	SELECT
			duan.iID_DuAnID AS IDDuAnID,
			duan.sMaDuAn as SMaDuAn,
			duan.sTenDuAn AS STenDuAn,
			duan.sDiaDiem AS SDiaDiem,
			CAST(duan.sKhoiCong AS int) AS IGiaiDoanTu,
			CAST(duan.sKetThuc AS int) AS IGiaiDoanDen,
			duan.fHanMucDauTu AS FHanMucDauTu,
			donvi.iID_DonVi AS IIdDonViId,
			donvi.iID_MaDonVi AS IIDMaDonVi,
			donvi.sTenDonVi AS STenDonVi,
			null AS IIDLoaiCongTrinhID,
			'' AS STenLoaiCongTrinh,
			null AS IIDNguonVonID,
			'' AS STenNguonVon
		FROM VDT_DA_DuAn duan
		INNER JOIN #tmpDuAnKHTH as khth on duan.iID_DuAnID = khth.iID_DuAnID
		LEFT JOIN VDT_DM_DonViThucHienDuAn donvi
			ON duan.iID_DonViThucHienDuAnID  = donvi.iID_DonVi
		WHERE
			1=1
			--AND duan.sTrangThaiDuAn = 'THUC_HIEN'
			AND duan.bIsKetThuc IS NULL
			AND iID_MaDonViThucHienDuAnID = @IdDonVi
		ORDER BY duan.dDateCreate			--them ngay 13/8
	DROP TABLE #tmpDuAnKHTH
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_denghithanhtoan_by_thongtri]    Script Date: 15/08/2022 8:34:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_vdt_denghithanhtoan_by_thongtri]
	@YearOfWork int,
	@UserName nvarchar(100),
	@thongTriId uniqueidentifier
AS
BEGIN

	DECLARE @CountDonViCha int;
	SELECT 
		@CountDonViCha = count(*) FROM (SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName AND iNamLamViec = @YearOfWork AND iTrangThai = 1) nddv
	INNER JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1 AND iLoai = 0) dv
	ON dv.iID_MaDonVi = nddv.iID_MaDonVi;

	SELECT DISTINCT iID_DeNghiThanhToanID, tbl.iLoai,
		(CASE WHEN khvn.Id IS NOT NULL THEN khvn.sSoQuyetDinh
			WHEN khvu.Id IS NOT NULL THEN khvu.sSoQuyetDinh
			WHEN qt.Id IS NOT NULL THEN qt.sSoDeNghi
		END) as sSoQuyetDinh INTO #tmp
	FROM VDT_TT_DeNghiThanhToan_KHV as tbl
	LEFT JOIN VDT_KHV_PhanBoVon as khvn on tbl.iID_KeHoachVonID = khvn.Id AND tbl.iLoai = 1
	LEFT JOIN VDT_KHV_KeHoachVonUng as khvu on tbl.iID_KeHoachVonID = khvu.Id AND tbl.iLoai = 2
	LEFT JOIN VDT_QT_BCQuyetToanNienDo as qt on tbl.iID_KeHoachVonID = qt.Id AND tbl.iLoai in (3,4)


	SELECT 
	  iID_DeNghiThanhToanID,
	  STUFF((
		SELECT '; ' + sSoQuyetDinh
		FROM #tmp 
		WHERE (iID_DeNghiThanhToanID = Results.iID_DeNghiThanhToanID) 
		FOR XML PATH(''),TYPE).value('(./text())[1]','VARCHAR(MAX)')
	  ,1,2,'') AS sKeHoachVon ,
	  STUFF((
		SELECT '; ' + CAST(iLoai as nvarchar(5))
		FROM #tmp 
		WHERE (iID_DeNghiThanhToanID = Results.iID_DeNghiThanhToanID) 
		FOR XML PATH(''),TYPE).value('(./text())[1]','VARCHAR(MAX)')
	  ,1,2,'') AS sLoaiKeHoachVon
	  INTO #tmpKhv
	FROM #tmp Results
	GROUP BY iID_DeNghiThanhToanID


	SELECT tbl.*, ns.sTen as sNguonVon, lnv.sMoTa as sLoaiNguonVon, dv.sTenDonVi as sTenDonVi, 
		da.sTenDuAn, hd.sSoHopDong, hd.dNgayHopDong, hd.fTienHopDong as fGiaTriHopDong, nt.sMaNhaThau, da.sMaDuAn, khv.sKeHoachVon, khv.sLoaiKeHoachVon, tbl.iID_ChiPhiID as IIdChiPhiId,
		hd.sTenHopDong
	FROM VDT_TT_DeNghiThanhToan as tbl
	LEFT JOIN NguonNganSach as ns on tbl.iID_NguonVonID = ns.iID_MaNguonNganSach
	LEFT JOIN NS_MucLucNganSach as lnv on tbl.iID_LoaiNguonVonID = lnv.iID_MLNS
	LEFT JOIN DonVi as dv on tbl.iID_DonViQuanLyID = dv.iID_DonVi
	LEFT JOIN VDT_DA_DuAn as da on tbl.iID_DuAnId = da.iID_DuAnID
	LEFT JOIN VDT_DA_TT_HopDong as hd on tbl.iID_HopDongId = hd.Id
	LEFT JOIN VDT_DM_NhaThau as nt on tbl.iID_NhaThauId = nt.Id
	LEFT JOIN #tmpKhv as khv on tbl.Id = khv.iID_DeNghiThanhToanID
	WHERE 
	(
		( EXISTS (SELECT * from f_split(tbl.iID_MaDonViQuanLy) INTERSECT SELECT iID_MaDonVi FROM NguoiDung_DonVi  WHERE iID_MaNguoiDung = @UserName and iNamLamViec = @YearOfWork)
			AND (@CountDonViCha = 0)
		)
		OR (@CountDonViCha <> 0 AND tbl.bKhoa = 1)
		OR 
		(   EXISTS (SELECT * from f_split(tbl.iID_MaDonViQuanLy) INTERSECT SELECT iID_MaDonVi FROM NguoiDung_DonVi  WHERE iID_MaNguoiDung = @UserName and iNamLamViec = @YearOfWork)
			AND (@CountDonViCha <> 0)
		)
	) and (tbl.bTongHop is null or tbl.bTongHop != 1)
	and tbl.iID_ThongTriThanhToanID = @thongTriId

	ORDER BY tbl.dDateCreate DESC

	DROP TABLE #tmp
END
;
GO
