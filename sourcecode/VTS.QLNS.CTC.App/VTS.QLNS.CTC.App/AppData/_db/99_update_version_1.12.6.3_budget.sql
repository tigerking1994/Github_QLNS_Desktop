/****** Object:  StoredProcedure [dbo].[sp_qs_capnhat_chitiet_year_begin]    Script Date: 24/02/2023 5:54:37 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qs_capnhat_chitiet_year_begin]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qs_capnhat_chitiet_year_begin]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chitiet_all]    Script Date: 24/02/2023 5:54:37 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_chungtu_chitiet_all]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_chungtu_chitiet_all]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chitiet_all]    Script Date: 24/02/2023 5:54:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

Create PROCEDURE [dbo].[sp_cp_chungtu_chitiet_all]
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
	-- Khai báo chỉ mục cấp phát
	DECLARE @CapPhatIndex int;
	SELECT @CapPhatIndex = iSoChungTuIndex
	FROM NS_CP_ChungTu
	WHERE iID_CTCapPhat = @VoucherId

	-- Cắt chuỗi LNS và ID đơn vị
	SELECT * INTO #TempLNS FROM f_split(@LNS);
	SELECT * INTO #TempAgency FROM f_split(@AgencyId);

	-- Mục lục ngân sách đầy đủ theo năm
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha into #tblNsMlns	
	FROM NS_MucLucNganSach 
	WHERE iNamLamViec = @YearOfWork 

	-- Lấy ra mlns theo phân cấp
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa,
			CASE WHEN (sNG IS NULL OR sNG = '') THEN CAST(1 as bit) ELSE CAST(0 as bit) END AS bHangCha	
			INTO #tblMlnsByPhanCap
	FROM #tblNsMlns 
	WHERE sLNS IN (
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
				AND sLNS IN (SELECT * FROM #TempLNS)
		) LNS
		UNPIVOT
		(
			value
			FOR col in (LNS1, LNS3, LNS5, LNS)
		) un) 
	
	--;WITH C AS  
	--(  
	--  SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa
	--  FROM #tblNsMlns
	--  UNION ALL 
	--  SELECT T.iID_MLNS, T.iID_MLNS_Cha, T.sXauNoiMa, T.sLNS, T.sL, T.sK, T.sM, T.sTM, T.sTTM, T.sNG, T.sTNG, T.sTNG1, T.sTNG2, T.sTNG3, T.sMoTa
	--  FROM #tblNsMlns T
	--  INNER JOIN C
	--  ON T.iID_MLNS = C.iID_MLNS_Cha
	--)

	--SELECT DISTINCT * into #mlnsPhanBo from C;
	
	
	-- Lấy ra đơn vị từ đơn vị của chứng từ
	SELECT iID_MaDonVi, iID_MaDonVi + ' - ' + sTenDonVi as sTenDonVi into #tblDonVi
	FROM DonVi 
	WHERE INamLamViec = @YearOfWork AND iID_MaDonVi IN (SELECT * FROM #TempAgency)

	-- Map bảng mlnsPhanCap và đơn vị
	SELECT * INTO #tblMlnsDonVi FROM (
		SELECT * FROM 
			(SELECT * FROM #tblMlnsByPhanCap
			 WHERE bHangCha = 0 OR (bHangCha = 1 AND @PhanCap = 'M' AND ISNULL(sM, '') <> '') 
								OR (bHangCha = 1 AND @PhanCap = 'TM' AND IsNull(sTM, '') <> '')) a
	    INNER JOIN #tblDonVi b ON 1 = 1
		UNION ALL
		SELECT *, NULL AS iID_MaDonVi, NULL AS sTenDonVi  
		FROM #tblMlnsByPhanCap 
		WHERE bHangCha = 1 AND (@PhanCap <> 'M' OR ISNULL(sM, '') = '') 
						   AND (@PhanCap <> 'TM' OR ISNULL(sTM, '') = '')
	) mlns

	
	-- Map bảng mlns và đơn vị
	/*
	SELECT * INTO #tblMlnsRoot FROM (
		SELECT * FROM #tblNsMlns, #tblDonVi 
		WHERE bHangCha = 0
		UNION ALL
		SELECT *, null AS iID_MaDonVi, null AS sTenDonVi  
		FROM #tblNsMlns 
		WHERE bHangCha = 1
	) mlns
	*/

	-- Lấy dữ liệu đã cấp
	SELECT SUM(fTuChi) AS DaCap,
		   0 AS DuToan,
           iID_MaDonVi,
           iID_MLNS,
		   sLNS
		   INTO #tblDataDaCap
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
		  AND EXISTS(SELECT * FROM #TempAgency INTERSECT SELECT * FROM f_split(sDSID_MaDonVi))
		)
		AND iID_MaDonVi IN (SELECT * FROM #tempAgency)
	GROUP BY iID_MaDonVi, iID_MLNS, sLNS;

	-- Lấy ra dữ liệu dự toán
	SELECT SUM(fTuChi) AS TuChi,
           SUM(fHangNhap) AS HangNhap,
           SUM(fHangMua) AS HangMua,
           iID_MaDonVi,
           iID_MLNS,
		   iID_MLNS_Cha,
           sXauNoiMa,
           sLNS AS LNS
		   INTO #tblPhanBoDuToan
	FROM NS_DT_ChungTuChiTiet
	WHERE iID_DTChungTu IN
       (SELECT iID_DTChungTu
        FROM NS_DT_ChungTu
        WHERE sSoQuyetDinh <> ''
          AND sSoQuyetDinh IS NOT NULL
          AND iLoai = 1 
          AND iNamLamViec = @YearOfWork
          AND ((@YearOfBudget = 2 AND (iNamNganSach = 2 OR iNamNganSach = 4))
            OR (@YearOfBudget <> 2 AND iNamNganSach = @YearOfBudget))
          AND iID_MaNguonNganSach = @BudgetSource
          AND CAST(dNgayQuyetDinh AS DATE) <= CAST(@VoucherDate AS DATE))
    AND iID_MaDonVi IN (SELECT * FROM #TempAgency)
	GROUP BY iID_MaDonVi, iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS

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
	FROM (SELECT * FROM NS_MucLucNganSach WHERE iNamLamViec = @YearOfWork) mlns
	INNER JOIN (SELECT * FROM #tblPhanBoDuToan WHERE LNS <> '1040100') dtctct
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
	FROM (SELECT * FROM NS_MucLucNganSach WHERE iNamLamViec = @YearOfWork) mlns
	INNER JOIN (SELECT * FROM #tblPhanBoDuToan WHERE LNS = '1040100') dtctct
	ON  REPLACE(dtctct.sXauNoiMa, '1040100', '1040200') = mlns.sXauNoiMa
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
	FROM (SELECT * FROM NS_MucLucNganSach WHERE iNamLamViec = @YearOfWork) mlns
	INNER JOIN(SELECT * FROM #tblPhanBoDuToan WHERE LNS = '1040100') dtctct
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
		   ISNULL(mlns.iID_MaDonVi, daCapDuToan.iID_MaDonVi) as iID_MaDonVi
		   INTO #tblDaCapDuToan
	FROM #tblMlnsDonVi mlns
	LEFT JOIN
	  #tblDataDaCapDuToan daCapDuToan
	ON mlns.iID_MLNS = daCapDuToan.iID_MLNS 
	AND ((mlns.iID_MaDonVi IS NOT NULL AND mlns.iID_MaDonVi = daCapDuToan.iID_MaDonVi) 
	OR mlns.iID_MaDonVi IS NULL)	

	SELECT iID_MLNS, iID_MLNS_Cha, iID_MaDonVi, sum(DaCap) AS DaCap, sum(DuToan) AS DuToan INTO #tblDaCapDuToanResult FROM (
		SELECT distinct T.iID_MLNS,  
			   T.iID_MLNS_Cha,
			   T.iID_MaDonVi,
			   T.DaCap,
			   T.DuToan
		FROM #tblDaCapDuToan T 
		WHERE T.DaCap <> 0 OR T.DuToan <> 0) data
	GROUP BY iID_MLNS, iID_MLNS_Cha, iID_MaDonVi
	-- OPTION (MAXRECURSION 0)

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
	FROM #tblMlnsDonVi AS mlnsPhanBo
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
	ON mlnsPhanBo.iID_MLNS = daCapDuToan.iID_MLNS and daCapDuToan.iID_MaDonVi LIKE '%' + mlnsPhanBo.iID_MaDonVi + '%' 
	ORDER BY mlnsPhanBo.sXauNoiMa, mlnsPhanBo.iID_MaDonVi;

END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_capnhat_chitiet_year_begin]    Script Date: 24/02/2023 5:54:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qs_capnhat_chitiet_year_begin]
	@YearOfWork int,
	@IdMaDonVi nvarchar(50)
AS
BEGIN
	update ct1 set 
		ct1.fSoThieuUy  = ct2.fSoThieuUy ,
		ct1.fSoTrungUy  = ct2.fSoTrungUy ,
		ct1.fSoThuongUy  = ct2.fSoThuongUy ,
		ct1.fSoDaiUy  = ct2.fSoDaiUy ,
		ct1.fSoThieuTa  = ct2.fSoThieuTa ,
		ct1.fSoTrungTa  = ct2.fSoTrungTa ,
		ct1.fSoThuongTa  = ct2.fSoThuongTa ,
		ct1.fSoDaiTa  = ct2.fSoDaiTa ,
		ct1.fSoTuong = ct2.fSoTuong,
		ct1.fSoTSQ  = ct2.fSoTSQ ,
		ct1.fSoBinhNhi  = ct2.fSoBinhNhi ,
		ct1.fSoBinhNhat  = ct2.fSoBinhNhat ,
		ct1.fSoHaSi  = ct2.fSoHaSi ,
		ct1.fSoTrungSi  = ct2.fSoTrungSi ,
		ct1.fSoThuongSi  = ct2.fSoThuongSi ,
		ct1.fSoQNCN  = ct2.fSoQNCN ,
		ct1.fSoCNVQP  = ct2.fSoCNVQP ,
		ct1.fSoLDHD  = ct2.fSoLDHD ,
		ct1.fSoCNVQPCT  = ct2.fSoCNVQPCT ,
		ct1.fSoQNVQPHD  = ct2.fSoQNVQPHD ,
		ct1.fTongSo  = ct2.fTongSo ,
		ct1.fSoSQ_KH  = ct2.fSoSQ_KH ,
		ct1.fSoHSQBS_KH  = ct2.fSoHSQBS_KH ,
		ct1.fSoCNVQP_KH  = ct2.fSoCNVQP_KH ,
		ct1.fSoLDHD_KH  = ct2.fSoLDHD_KH ,
		ct1.fSoQNCN_KH  = ct2.fSoQNCN_KH ,
		ct1.fSoVCQP  = ct2.fSoVCQP ,
		ct1.fSoCY_H  = ct2.fSoCY_H ,
		ct1.fSoCY_KT = ct2.fSoCY_KT
	from 
	NS_QS_ChungTuChiTiet as ct1
	INNER JOIN (
		select * from NS_QS_ChungTuChiTiet 
		where iThangQuy = 12 and iNamLamViec = @YearOfWork - 1
			and ((@IdMaDonVi <> '' and iID_MaDonVi = @IdMaDonVi) or @IdMaDonVi = '')
	) as ct2 on ct1.iID_MaDonVi = ct2.iID_MaDonVi and ct1.sKyHieu = ct2.sKyHieu
	where ct1.iThangQuy = 0;

END
;
;
GO
