/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_kpql_chitiet]    Script Date: 12/27/2023 6:23:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_khc_kpql_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_khc_kpql_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_bhxh_chitiet]    Script Date: 12/27/2023 6:23:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_khc_bhxh_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_khc_bhxh_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chi_tiet]    Script Date: 12/27/2023 6:23:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_chungtu_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_chungtu_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_phanbodutoanchi_index]    Script Date: 12/27/2023 6:23:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_phanbodutoanchi_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_phanbodutoanchi_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiao_index]    Script Date: 12/27/2023 6:23:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_nhandutoanchitrengiao_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_nhandutoanchitrengiao_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiao_index]    Script Date: 12/27/2023 6:23:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE  [dbo].[sp_bhxh_nhandutoanchitrengiao_index]
	@YearOfWork int
AS
BEGIN
	SELECT
	DTC.ID,
	DTC.sSoChungTu,
	DTC.iID_MaDonVi,
	DV.sTenDonVi AS sTenDonVi,
	DTC.dNgayChungTu,
	DTC.sSoQuyetDinh,
	DTC.dNgayQuyetDinh,
	DTC.sLNS,
	DTC.fTongTienTuChi,
	DTC.fTongTienHienVat,
	DTC.fTongTien,
	DTC.iLoaiDotNhanPhanBo,
	DTC.sMoTa,
	DTC.iNamLamViec,
	DTC.sNguoiTao,
	DTC.bIsKhoa,
	BH_DM_LoaiChi.sTenDanhMucLoaiChi AS sLoaiChi,
	DTC.sMaLoaiChi,
	DTC.iID_LoaiDanhMucChi
	FROM BH_DTC_DuToanChiTrenGiao DTC
	LEFT JOIN DonVi DV ON DTC.iID_MaDonVi = DV.iID_MaDonVi
	INNER JOIN BH_DM_LoaiChi ON DTC.iID_LoaiDanhMucChi = BH_DM_LoaiChi.iID
	WHERE DV.iNamLamViec = @YearOfWork and DTC.iNamLamViec = @YearOfWork
	ORDER BY DTC.dNgayQuyetDinh DESC
END;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_phanbodutoanchi_index]    Script Date: 12/27/2023 6:23:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE  [dbo].[sp_bhxh_phanbodutoanchi_index]
	@YearOfWork int
AS
BEGIN
	SELECT
	PBDTC.ID,
	PBDTC.sSoChungTu,
	PBDTC.dNgayChungTu,
	PBDTC.sSoQuyetDinh,
	PBDTC.dNgayQuyetDinh,
	PBDTC.iNamChungTu,
	PBDTC.iLoaiDotNhanPhanBo,
	PBDTC.sMoTa,
	PBDTC.sLNS,
	PBDTC.fTongTien,
	PBDTC.fTongTienTuChi,
	PBDTC.fTongTienHienVat,
	PBDTC.bIsKhoa,
	PBDTC.sNguoiTao,
	PBDTC.sNguoiSua,
	PBDTC.dNgayTao,
	PBDTC.bIsKhoa,
	PBDTC.sDotNhan,
	PBDTC.sID_MaDonVi,
	PBDTC.iLoaiChungTu,
	PBDTC.iID_DotNhan,
	BH_DM_LoaiChi.sTenDanhMucLoaiChi AS sLoaiChi,
	PBDTC.sMaLoaiChi,
	PBDTC.iID_LoaiDanhMucChi
	FROM BH_DTC_PhanBoDuToanChi PBDTC
	INNER JOIN BH_DM_LoaiChi ON PBDTC.iID_LoaiDanhMucChi = BH_DM_LoaiChi.iID
	AND PBDTC.iNamChungTu = @YearOfWork
	ORDER BY PBDTC.dNgayQuyetDinh DESC
END;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chi_tiet]    Script Date: 12/27/2023 6:23:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_cp_chungtu_chi_tiet]
	@VoucherId uniqueidentifier,
	@LNS nvarchar(max),
	@YearOfWork int,
	@AgencyId nvarchar(500),
	@VoucherDate datetime,
	@iID_LoaiDanhMucChi nvarchar(255)
AS
BEGIN
	DECLARE @iD uniqueidentifier='00000000-0000-0000-0000-000000000000';
	DECLARE @CountIndex INT;
	SELECT * into #tempLNS from f_split(@LNS);
	SELECT * into #tempAgency from  f_split(@AgencyId);
	SELECT @CountIndex = COUNT(*) 
	FROM BH_CP_ChungTu_ChiTiet
	WHERE iID_CP_ChungTu = @VoucherId

	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha
			into #tblNsMlns
	FROM BH_DM_MucLucNganSach 
	where iNamLamViec = @YearOfWork 

		-- lấy ra dữ liệu dự toán
	SELECT 
		  SUM(fTongTien) AS fTienDuToan,
		  --fTongTien AS fTienDuToan,
           0 AS fTienKeHoachCap,
          0 AS fTienDaCap,
          iID_MaDonVi,
          iID_MucLucNganSach
		  into #tblPhanBoDuToan
   FROM BH_DTC_PhanBoDuToanChi_ChiTiet
   WHERE iID_DTC_PhanBoDuToanChi IN
       (SELECT ID
        FROM BH_DTC_PhanBoDuToanChi
        WHERE sSoQuyetDinh <> ''
          AND sSoQuyetDinh IS NOT NULL
          AND iNamChungTu = @YearOfWork
          AND iID_LoaiDanhMucChi = @iID_LoaiDanhMucChi
		  AND bIsKhoa=1
          AND cast(dNgayQuyetDinh AS date) <= cast(@VoucherDate AS date))
     AND iID_MaDonVi in  (SELECT * FROM f_split(@AgencyId))-- donvi
   GROUP BY iID_MaDonVi, 
   iID_MucLucNganSach

	SELECT DISTINCT iID_MucLucNganSach
	--, iID_MLNS_Cha 
	into #tblPhanBoDuToanGroupbyiID_MLNS from #tblPhanBoDuToan

	SELECT T.* INTO #tblNsMlns1
	FROM #tblNsMlns T
	INNER JOIN #tblPhanBoDuToanGroupbyiID_MLNS E ON T.iID_MLNS = E.iID_MucLucNganSach 


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
				WHEN (sNG is null OR sNG = '') THEN cast(1 as bit)
			ELSE cast(0 as bit)
			END AS bHangCha	
			into #tblMlnsByPhanCap
	FROM #mlnsPhanBo 
	WHERE 
		sLNS IN (SELECT * FROM #tempLNS)

	-- lấy ra đơn vị từ đơn vị của chứng từ
	SELECT iID_MaDonVi, iID_MaDonVi + ' - ' + sTenDonVi as sTenDonVi into #tblDonVi
	FROM DonVi 
	WHERE 
		INamLamViec = @YearOfWork 
		AND iID_MaDonVi IN (SELECT * FROM #tempAgency)

	-- Tao bang tam luu chu mlng con
	SELECT * INTO #tblMlnsByPhanCapExistDonVi
	FROM #tblMlnsByPhanCap tbl, #tblDonVi dv
	WHERE  tbl.bHangCha = 0 OR (tbl.bHangCha = 1 and (IsNull(tbl.sM, '') != '' OR tbl.sM <> '' OR IsNull(tbl.sTM, '') != '')  )
	AND tbl.sTM !=''  


	-- Tao bang tam luu chu mlng cha
	SELECT *, '' AS iID_MaDonVi, '' AS sTenDonVi  INTO #tblMlnsByPhanCapNotDonVi
	FROM #tblMlnsByPhanCap 
		WHERE bHangCha = 1 AND ( (IsNull(sM, '') = '' or sM = '')) AND ( (IsNull(sTM, '') = '' or sTM = '')) OR ((ISNULL(sTM,'')='' OR sTM='')) OR sTM='0'

	-- map bảng mlnsPhanCap và đơn vị
	SELECT * INTO #tblMLNS FROM (
		SELECT *
		FROM #tblMlnsByPhanCapExistDonVi tbl
		WHERE sTM !='0'
		UNION ALL
		SELECT *
		FROM #tblMlnsByPhanCapNotDonVi 
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

	IF @CountIndex=0
	begin

	-- lấy dữ liệu đã cấp
	SELECT 0 AS fTienDuToan,
		  0 AS fTienKeHoachCap,
		  ISNULL(SUM(fTienKeHoachCap),0)  AS fTienDaCap,
          iID_MaDonVi,
          iID_MucLucNganSach
		  into #tblDataDaCap
	FROM BH_CP_ChungTu_ChiTiet
	WHERE iID_CP_ChungTu IN
       (
		SELECT iID_CP_ChungTu
        FROM BH_CP_ChungTu
        WHERE iNamChungTu = @YearOfWork
		  AND iID_LoaiCap = @iID_LoaiDanhMucChi
          AND CAST(dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
		  AND EXISTS(SELECT * FROM #tempAgency INTERSECT SELECT * FROM f_split(@AgencyId))
		  --AND iID_CP_ChungTu=@VoucherId
		)
		AND iID_MaDonVi IN (SELECT * FROM #tempAgency)
	GROUP BY iID_MaDonVi, iID_MucLucNganSach
	SELECT * into #tblDataDuToan FROM #tblPhanBoDuToan

	--select * from #tblDataDaCap

	--select * from #tblDataDaCap

	SELECT sum(fTienDuToan) AS fTienDuToan, sum(fTienKeHoachCap) AS fTienKeHoachCap, sum(fTienDaCap) fTienDaCap, iID_MaDonVi, iID_MucLucNganSach into #tblDataDaCapDuToan FROM (
		SELECT * FROM #tblDataDaCap
		UNION ALL
		SELECT * FROM #tblDataDuToan
		) data
	GROUP BY iID_MaDonVi, iID_MucLucNganSach;



	--select * from  #tblMlnsRoot
	SELECT mlns.iID_MLNS,
		   mlns.iID_MLNS_Cha,
		   mlns.sXauNoiMa,
		   fTienDuToan,
		   fTienDaCap,
		   fTienKeHoachCap,
		   isnull(mlns.iID_MaDonVi, daCapDuToan.iID_MaDonVi) as iID_MaDonVi
		   INTO #tblDaCapDuToan
	FROM #tblMlnsRoot mlns
	LEFT JOIN
	  #tblDataDaCapDuToan daCapDuToan
	ON mlns.iID_MLNS = daCapDuToan.iID_MucLucNganSach and ((mlns.iID_MaDonVi is not null and mlns.iID_MaDonVi = daCapDuToan.iID_MaDonVi) or mlns.iID_MaDonVi is null)

	SELECT iID_MLNS, iID_MLNS_Cha, iID_MaDonVi, sum(fTienDuToan) AS fTienDuToan, sum(fTienDaCap) AS fTienDaCap, SUM(fTienKeHoachCap) as fTienKeHoachCap INTO #tblDaCapDuToanResult FROM (
		SELECT distinct T.iID_MLNS,  
			   T.iID_MLNS_Cha,
			   --isnull(T.iID_MaDonVi, T.iID_MaDonVi) as iID_MaDonVi,
			   T.iID_MaDonVi,
			   T.fTienDuToan,
			   T.fTienDaCap,
			   T.fTienKeHoachCap
		FROM #tblDaCapDuToan T 
		WHERE T.fTienDaCap <> 0 OR T.fTienDuToan <> 0 OR t.fTienKeHoachCap<>0) data
	GROUP BY iID_MLNS, iID_MLNS_Cha, iID_MaDonVi
	option (maxrecursion 0)

	-- Get data
	SELECT
		isnull(ctct.iID_CP_ChungTu_ChiTiet, @iD) AS iID_CP_ChungTu_ChiTiet,
		@VoucherId AS iID_CP_ChungTu,
		mlnsPhanBo.iID_MLNS AS iID_MucLucNganSach,
		mlnsPhanBo.iID_MLNS_Cha AS IdParent,
		mlnsPhanBo.sXauNoiMa AS sXauNoiMa,
		mlnsPhanBo.sLNS AS SLNS,
		mlnsPhanBo.sL AS SL,
		mlnsPhanBo.sK AS SK,
		mlnsPhanBo.sM AS SM,
		mlnsPhanBo.sTM AS STM,
		mlnsPhanBo.sTTM AS STTM,
		mlnsPhanBo.sNG AS SNG,
		mlnsPhanBo.sTNG AS STNG,
		mlnsPhanBo.sTNG1 AS STNG1,
		mlnsPhanBo.sTNG2 AS STNG2,
		mlnsPhanBo.sTNG3 AS STNG3,
		mlnsPhanBo.sMoTa AS SNoiDung,
		mlnsPhanBo.bHangCha As IsHangCha,
		@YearOfWork AS INamLamViec,
		mlnsPhanBo.iID_MaDonVi AS IID_MaDonVi,
		mlnsPhanBo.sTenDonVi AS STenDonVi,
		isnull(daCapDuToan.fTienDaCap, 0) as FTienDaCap,
		isnull(daCapDuToan.fTienDuToan, 0) as FTienDuToan,
		isnull(daCapDuToan.fTienKeHoachCap, 0) as FTienKeHoachCap,
		ctct.sGhiChu AS SGhiChu,
		isnull(ctct.dNgayTao, getdate()) AS DNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS DNgaySua,
		ctct.sNguoiTao AS SNguoiTao,
		ctct.sNguoiSua AS SNguoiSua
	FROM #tblMlnsRoot AS mlnsPhanBo
	LEFT JOIN
		(SELECT *
			FROM 
				BH_CP_ChungTu_ChiTiet
			WHERE 
		 		iID_CP_ChungTu = @VoucherId
		) ctct
	ON mlnsPhanBo.iID_MLNS = ctct.iID_MucLucNganSach and mlnsPhanBo.iID_MaDonVi = ctct.iID_MaDonVi
	LEFT JOIN BH_CP_ChungTu ct ON ctct.iID_CP_ChungTu = ct.iID_CP_ChungTu
	LEFT JOIN #tblDaCapDuToanResult daCapDuToan
	on mlnsPhanBo.iID_MLNS = daCapDuToan.iID_MLNS and daCapDuToan.iID_MaDonVi LIKE '%' + mlnsPhanBo.iID_MaDonVi + '%' 
	where mlnsPhanBo.sLNS IN (SELECT * FROM #tempLNS)
	order by mlnsPhanBo.sXauNoiMa, mlnsPhanBo.iID_MaDonVi;
	end
	ELSE 
		begin
	-- lấy dữ liệu đã cấp
	SELECT SUM(fTienDuToan) AS fTienDuToan,
		  SUM(fTienKeHoachCap) AS fTienKeHoachCap,
		  SUM(fTienDaCap) AS fTienDaCap,
          iID_MaDonVi,
          iID_MucLucNganSach
		  into #tblDataDaCapExist
	FROM BH_CP_ChungTu_ChiTiet
	WHERE iID_CP_ChungTu IN
       (
		SELECT iID_CP_ChungTu
        FROM BH_CP_ChungTu
        WHERE iNamChungTu = @YearOfWork
		  AND iID_LoaiCap = @iID_LoaiDanhMucChi
          AND CAST(dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
		  AND EXISTS(SELECT * FROM #tempAgency INTERSECT SELECT * FROM f_split(@AgencyId))
		  AND iID_CP_ChungTu=@VoucherId
		)
		AND iID_MaDonVi IN (SELECT * FROM #tempAgency)
	GROUP BY iID_MaDonVi, iID_MucLucNganSach

	SELECT sum(fTienDuToan) AS fTienDuToan, sum(fTienKeHoachCap) AS fTienKeHoachCap, sum(fTienDaCap) fTienDaCap, iID_MaDonVi, iID_MucLucNganSach into #tblDataDaCapDuToanExist FROM (
		SELECT * FROM #tblDataDaCapExist
		) data
	GROUP BY iID_MaDonVi, iID_MucLucNganSach;
	--select * from  #tblMlnsRoot
	SELECT mlns.iID_MLNS,
		   mlns.iID_MLNS_Cha,
		   mlns.sXauNoiMa,
		   fTienDuToan,
		   fTienDaCap,
		   fTienKeHoachCap,
		   isnull(mlns.iID_MaDonVi, daCapDuToan.iID_MaDonVi) as iID_MaDonVi
		   INTO #tblDaCapDuToanExist
	FROM #tblMlnsRoot mlns
	LEFT JOIN
	  #tblDataDaCapDuToanExist daCapDuToan
	ON mlns.iID_MLNS = daCapDuToan.iID_MucLucNganSach and ((mlns.iID_MaDonVi is not null and mlns.iID_MaDonVi = daCapDuToan.iID_MaDonVi) or mlns.iID_MaDonVi is null)

	SELECT iID_MLNS, iID_MLNS_Cha, iID_MaDonVi, sum(fTienDuToan) AS fTienDuToan, sum(fTienDaCap) AS fTienDaCap, SUM(fTienKeHoachCap) as fTienKeHoachCap INTO #tblDaCapDuToanResultExist FROM (
		SELECT distinct T.iID_MLNS,  
			   T.iID_MLNS_Cha,
			   --isnull(T.iID_MaDonVi, T.iID_MaDonVi) as iID_MaDonVi,
			   T.iID_MaDonVi,
			   T.fTienDuToan,
			   T.fTienDaCap,
			   T.fTienKeHoachCap
		FROM #tblDaCapDuToanExist T 
		WHERE T.fTienDaCap <> 0 OR T.fTienDuToan <> 0 OR t.fTienKeHoachCap<>0) data
	GROUP BY iID_MLNS, iID_MLNS_Cha, iID_MaDonVi
	option (maxrecursion 0)



	-- Get data
	SELECT
		isnull(ctct.iID_CP_ChungTu_ChiTiet, @iD) AS iID_CP_ChungTu_ChiTiet,
		@VoucherId AS iID_CP_ChungTu,
		mlnsPhanBo.iID_MLNS AS iID_MucLucNganSach,
		mlnsPhanBo.iID_MLNS_Cha AS IdParent,
		mlnsPhanBo.sXauNoiMa AS sXauNoiMa,
		mlnsPhanBo.sLNS AS SLNS,
		mlnsPhanBo.sL AS SL,
		mlnsPhanBo.sK AS SK,
		mlnsPhanBo.sM AS SM,
		mlnsPhanBo.sTM AS STM,
		mlnsPhanBo.sTTM AS STTM,
		mlnsPhanBo.sNG AS SNG,
		mlnsPhanBo.sTNG AS STNG,
		mlnsPhanBo.sTNG1 AS STNG1,
		mlnsPhanBo.sTNG2 AS STNG2,
		mlnsPhanBo.sTNG3 AS STNG3,
		mlnsPhanBo.sMoTa AS SNoiDung,
		mlnsPhanBo.bHangCha As IsHangCha,
		@YearOfWork AS INamLamViec,
		mlnsPhanBo.iID_MaDonVi AS IID_MaDonVi,
		mlnsPhanBo.sTenDonVi AS STenDonVi,
		isnull(daCapDuToan.fTienDaCap, 0) as FTienDaCap,
		isnull(daCapDuToan.fTienDuToan, 0) as FTienDuToan,
		isnull(daCapDuToan.fTienKeHoachCap, 0) as FTienKeHoachCap,
		ctct.sGhiChu AS SGhiChu,
		isnull(ctct.dNgayTao, getdate()) AS DNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS DNgaySua,
		ctct.sNguoiTao AS SNguoiTao,
		ctct.sNguoiSua AS SNguoiSua
	FROM #tblMlnsRoot AS mlnsPhanBo
	LEFT JOIN
		(SELECT *
			FROM 
				BH_CP_ChungTu_ChiTiet
			WHERE 
		 		iID_CP_ChungTu = @VoucherId
		) ctct
	ON mlnsPhanBo.iID_MLNS = ctct.iID_MucLucNganSach and mlnsPhanBo.iID_MaDonVi = ctct.iID_MaDonVi
	LEFT JOIN BH_CP_ChungTu ct ON ctct.iID_CP_ChungTu = ct.iID_CP_ChungTu
	LEFT JOIN #tblDaCapDuToanResultExist daCapDuToan
	on mlnsPhanBo.iID_MLNS = daCapDuToan.iID_MLNS and daCapDuToan.iID_MaDonVi LIKE '%' + mlnsPhanBo.iID_MaDonVi + '%' 
	where mlnsPhanBo.sLNS IN (SELECT * FROM #tempLNS)
	order by mlnsPhanBo.sXauNoiMa, mlnsPhanBo.iID_MaDonVi;
	end
END
;
;
;
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_bhxh_chitiet]    Script Date: 12/27/2023 6:23:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay chung tu theo don vi và theo lns
CREATE PROCEDURE [dbo].[sp_rpt_khc_bhxh_chitiet]
	 @NamLamViec int,
	 @IdDonVi nvarchar(max),
	 @Dvt int
AS
BEGIN 
	SET NOCOUNT ON;
	-- lấy ra mlns theo phân cấp
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha
			into #tblMlnsByPhanCap
		FROM BH_DM_MucLucNganSach 
		WHERE 
		iNamLamViec = 2023 
		AND iTrangThai = 1
		AND (sLNS ='9010001' OR sLNS='9010002')

		SELECT 
		tblml.iID_MLNS as IID_MucLucNganSach,
		tblml.iID_MLNS_Cha as IdParent,
		tblml.sXauNoiMa  ,
		tblml.sLNS,
		tblml.sL,
		tblml.sK,
		tblml.sM,
		tblml.sTM,
		tblml.sTTM,
		tblml.sNG,
		tblml.sTNG,
		tblml.sTNG1,
		tblml.sTNG2,
		tblml.sTNG3,
		tblml.sMoTa,
		tblml.bHangCha,
		SUM(ISNULL(chitiet.fTienCNVQP, 0))/@Dvt fTienCNVQP,
		SUM(ISNULL(chitiet.fTienDaThucHienNamTruoc, 0))/@Dvt fTienDaThucHienNamTruoc,
		SUM(ISNULL(chitiet.fTienHSQBS, 0))/@Dvt fTienHSQBS,
		SUM(ISNULL(chitiet.fTienKeHoachThucHienNamNay, 0))/@Dvt fTienKeHoachThucHienNamNay,
		SUM(ISNULL(chitiet.fTienLDHD, 0))/@Dvt fTienLDHD,
		SUM(ISNULL(chitiet.fTienQNCN, 0))/@Dvt fTienQNCN,
		SUM(ISNULL(chitiet.fTienSQ, 0))/@Dvt fTienSQ,
		SUM(ISNULL(chitiet.fTienUocThucHienNamTruoc, 0))/@Dvt fTienUocThucHienNamTruoc,
		chitiet.iSoCNVQP,
		chitiet.iSoDaThucHienNamTruoc,
		chitiet.iSoHSQBS,
		chitiet.iSoKeHoachThucHienNamNay,
		chitiet.iSoLDHD,
		chitiet.iSoQNCN,
		chitiet.iSoSQ,
		chitiet.iSoUocThucHienNamTruoc,
		chitiet.sGhiChu
		
		FROM #tblMlnsByPhanCap tblml
		LEFT JOIN
		(SELECT CTCT.* FROM
			BH_KHC_CheDoBHXH_ChiTiet CTCT
			WHERE  CTCT.iID_KHC_CheDoBHXH IN
			(
				SELECT CT.ID
						FROM BH_KHC_CheDoBHXH CT
						WHERE CT.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVi))
							AND CT.sSoChungTu <> ''
							AND CT.sSoChungTu IS NOT NULL
							AND CT.iNamLamViec=@NamLamViec
			)) chitiet ON 

			chitiet.iID_MucLucNganSach=tblml.iID_MLNS

		GROUP BY tblml.iID_MLNS, tblml.iID_MLNS_Cha, tblml.sXauNoiMa, tblml.sLNS, tblml.sL, tblml.sK, tblml.sM, tblml.sTM, tblml.sTTM, tblml.sNG, tblml.sTNG, tblml.sTNG1
		, tblml.sTNG2, tblml.sTNG3, tblml.sMoTa, tblml.bHangCha, chitiet.iSoCNVQP,chitiet.iSoDaThucHienNamTruoc,  chitiet.iSoHSQBS, chitiet.iSoKeHoachThucHienNamNay, chitiet.iSoLDHD,
		chitiet.iSoQNCN, chitiet.iSoSQ, chitiet.iSoUocThucHienNamTruoc, chitiet.sGhiChu

		ORDER BY tblml.sXauNoiMa

		drop table #tblMlnsByPhanCap
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_kpql_chitiet]    Script Date: 12/27/2023 6:23:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay chung tu theo don vi và theo lns
CREATE PROCEDURE [dbo].[sp_rpt_khc_kpql_chitiet]
	 @NamLamViec int,
	 @IdDonVi nvarchar(max),
	 @Dvt int
AS
BEGIN 
	SET NOCOUNT ON;
	-- lấy ra mlns theo phân cấp
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha
			into #tblMlnsByPhanCap
		FROM BH_DM_MucLucNganSach 
		WHERE 
		iNamLamViec = @NamLamViec 
		AND iTrangThai = 1
		AND sLNS ='9010003'

		SELECT 
		tblml.iID_MLNS as IID_MucLucNganSach,
		tblml.iID_MLNS_Cha as IdParent,
		tblml.sXauNoiMa  ,
		tblml.sLNS,
		tblml.sL,
		tblml.sK,
		tblml.sM,
		tblml.sTM,
		tblml.sTTM,
		tblml.sNG,
		tblml.sTNG,
		tblml.sTNG1,
		tblml.sTNG2,
		tblml.sTNG3,
		tblml.sMoTa as SNoiDung,
		tblml.bHangCha,
		SUM(ISNULL(chitiet.fTienDaThucHienNamTruoc, 0))/@Dvt FTienDaThucHienNamTruoc,
		SUM(ISNULL(chitiet.fTienUocThucHienNamTruoc, 0))/@Dvt FTienUocThucHienNamTruoc,
		SUM(ISNULL(chitiet.fTienKeHoachThucHienNamNay, 0))/@Dvt FTienKeHoachThucHienNamNay,
		SUM(ISNULL(chitiet.fTienCanBo, 0))/@Dvt FTienCanBo,
		SUM(ISNULL(chitiet.fTienQuanLuc, 0))/@Dvt FTienQuanLuc,
		SUM(ISNULL(chitiet.fTienTaiChinh, 0))/@Dvt FTienTaiChinh,
		SUM(ISNULL(chitiet.fTienQuanY, 0))/@Dvt FTienQuanY,
		chitiet.sGhiChu
		
		FROM #tblMlnsByPhanCap tblml
		LEFT JOIN
		(SELECT CTCT.* FROM
			BH_KHC_KinhPhiQuanLy_ChiTiet CTCT
			WHERE  CTCT.iID_KHC_KinhPhiQuanLy IN
			(
				SELECT CT.iID_BH_KHC_KinhPhiQuanLy
						FROM BH_KHC_KinhPhiQuanLy CT
						WHERE CT.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVi))
							AND CT.sSoChungTu <> ''
							AND CT.sSoChungTu IS NOT NULL
							AND CT.iNamLamViec=@NamLamViec
			)) chitiet ON 

			chitiet.iID_MucLucNganSach=tblml.iID_MLNS

		GROUP BY tblml.iID_MLNS, tblml.iID_MLNS_Cha,tblml.sXauNoiMa,  tblml.sLNS, tblml.sLNS, tblml.sL, tblml.sK, tblml.sM, tblml.sTM, tblml.sTTM,tblml.sNG,  tblml.sTNG, 
		tblml.sTNG1,tblml.sTNG2,tblml.sTNG3,  tblml.sMoTa, tblml.bHangCha, chitiet.sGhiChu 

		ORDER BY tblml.sXauNoiMa

		drop table #tblMlnsByPhanCap
END
;
;
;
GO
