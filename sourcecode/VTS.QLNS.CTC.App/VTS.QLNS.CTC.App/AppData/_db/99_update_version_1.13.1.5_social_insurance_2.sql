/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_get_lns]    Script Date: 9/11/2023 3:16:34 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_dieuchinh_get_lns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_dieuchinh_get_lns]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_thongtri_tonghop]    Script Date: 9/11/2023 3:16:34 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_rpt_thongtri_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_rpt_thongtri_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_kehoach_bh]    Script Date: 9/11/2023 3:16:34 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_rpt_kehoach_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_rpt_kehoach_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_lns_rpt_thongtri_lns_bh]    Script Date: 9/11/2023 3:16:34 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_lns_rpt_thongtri_lns_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_lns_rpt_thongtri_lns_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_get_donvi_bh]    Script Date: 9/11/2023 3:16:34 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_get_donvi_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_get_donvi_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_index_bh]    Script Date: 9/11/2023 3:16:34 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_chungtu_index_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_chungtu_index_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chi_tiet_tonghop]    Script Date: 9/11/2023 3:16:34 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_chungtu_chi_tiet_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_chungtu_chi_tiet_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chi_tiet]    Script Date: 9/11/2023 3:16:34 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_chungtu_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_chungtu_chi_tiet]
GO

/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chi_tiet]    Script Date: 9/11/2023 3:16:34 PM ******/
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
           SUM(fTienTuChi) AS fTienKeHoachCap,
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
		  SUM(fTienKeHoachCap) AS fTienKeHoachCap,
		  SUM(fTienDaCap) AS fTienDaCap,
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
		  AND iID_CP_ChungTu=@VoucherId
		)
		AND iID_MaDonVi IN (SELECT * FROM #tempAgency)
	GROUP BY iID_MaDonVi, iID_MucLucNganSach
	SELECT * into #tblDataDuToan FROM #tblPhanBoDuToan

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
		ctct.sGhiChu AS GhiChu,
		isnull(ctct.dNgayTao, getdate()) AS DateCreated,
		isnull(ctct.dNgaySua, getdate()) AS DateModified,
		ctct.sNguoiTao AS UserCreator,
		ctct.sNguoiSua AS UserModifier
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
		ctct.sGhiChu AS GhiChu,
		isnull(ctct.dNgayTao, getdate()) AS DateCreated,
		isnull(ctct.dNgaySua, getdate()) AS DateModified,
		ctct.sNguoiTao AS UserCreator,
		ctct.sNguoiSua AS UserModifier
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
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chi_tiet_tonghop]    Script Date: 9/11/2023 3:16:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  -- Create date: <Create Date,,>
  -- Description:  <Description,,> Tao chi tiet chung tu tong hop
  -- =============================================
  Create PROCEDURE [dbo].[sp_cp_chungtu_chi_tiet_tonghop] 
  @ListIdChungTuTongHop ntext, 
  @Nguoitao nvarchar(50), 
  @IdChungTu nvarchar(100), 
  @NamLamViec int 
 AS 
 BEGIN 
 INSERT INTO [dbo].BH_CP_ChungTu_ChiTiet (
    iID_CP_ChungTu_ChiTiet,
	iID_CP_ChungTu, 
    iID_MucLucNganSach, 
	sM,
	sTM,
	sNoiDung, 
    fTienDuToan,
	fTienKeHoachCap, 
    fTienDaCap,
	iID_MaDonVi,
	dNgaySua, 
	dNgayTao, 
    sNguoiSua,
	sNguoiTao
  ) 
SELECT 
  DISTINCT 
  NEWID(), 
  @idChungTu, 
  iID_MucLucNganSach, 
  sM, 
  sTM, 
  sNoiDung, 
  sum(fTienDuToan), 
  sum(fTienKeHoachCap), 
  sum(fTienDaCap), 
  iID_MaDonVi, 
  Null, 
  GETDATE(), 
  Null, 
  @Nguoitao 
FROM 
  BH_CP_ChungTu_ChiTiet 
WHERE 
  iID_CP_ChungTu in (
    SELECT 
      * 
    FROM 
      f_split(@ListIdChungTuTongHop)
  ) 
GROUP BY 
  sM, 
  sTM, 
  iID_MucLucNganSach, 
  sNoiDung,
  iID_MaDonVi;
--danh dau chung tu da tong hop
update 
  BH_CP_ChungTu 
set 
  iLoaiTongHop = 2 
where 
  iID_CP_ChungTu in (
    SELECT 
      * 
    FROM 
      f_split(@ListIdChungTuTongHop)
  ) END;
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_index_bh]    Script Date: 9/11/2023 3:16:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Doantc
-- Create date: 13/06/2023
-- Description:	Lấy danh sách hiển thị chứng từ cấp phát BHXH

-- =============================================
CREATE PROCEDURE [dbo].[sp_cp_chungtu_index_bh]
	@YearOfWork int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
	DISTINCT 
		 ct.iID_CP_ChungTu 
		, ct.sSoChungTu
		, ct.dNgayChungTu
		, ct.sSoQuyetDinh
		, ct.dNgayQuyetDinh
		, ct.iNamChungTu
		, ct.sID_MaDonVi
		, ct.sMoTa
		, ct.sLNS
		, ct.iID_LoaiCap
		, ct.fTienDaCap
		, ct.fTienKeHoachCap
		, ct.fTienDuToan
		, ct.sTongHop
		, ct.iID_TongHop
		, ct.iLoaiTongHop
		, ct.dNgaySua
		, ct.dNgayTao
		, ct.sNguoiSua
		, ct.sNguoiTao
		, ct.dNgayTao
		, ct.bIsKhoa
		, ct.iQuy
		, lc.sTenDanhMucLoaiChi
		-- Tong dự toán todo
	FROM BH_CP_ChungTu ct
	LEFT JOIN BH_DM_LoaiChi lc on ct.iID_LoaiCap=lc.iID and ct.iNamChungTu=lc.iNamLamViec 
	WHERE ct.iNamChungTu=@YearOfWork
END


GO
/****** Object:  StoredProcedure [dbo].[sp_cp_get_donvi_bh]    Script Date: 9/11/2023 3:16:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay don vi cua chung tu cap phat chi tiet theo id
-- =============================================
Create PROCEDURE [dbo].[sp_cp_get_donvi_bh]
	@YearOfWork int,
	@CapPhatIds nvarchar(max)
AS
BEGIN
	SELECT dv.* 
	FROM 
	(
		SELECT DISTINCT iID_MaDonVi 
		FROM BH_CP_ChungTu_ChiTiet 
		WHERE
			iID_CP_ChungTu IN (SELECT * FROM f_split(@CapPhatIds))
	) ctct
	LEFT JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork) dv
	on dv.iID_MaDonVi = ctct.iID_MaDonVi
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_lns_rpt_thongtri_lns_bh]    Script Date: 9/11/2023 3:16:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay chung tu theo don vi và theo lns
CREATE PROCEDURE [dbo].[sp_cp_lns_rpt_thongtri_lns_bh]
	 @NamLamViec int,
	 @CapPhatId nvarchar(100),
	 @IdDonVi nvarchar(max),
	 @LNS nvarchar(max),
	 @UserName nvarchar(100),
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
		--AND (
		--	(@PhanCap = 'M' AND (sTM IS NULL OR sTM = ''))
		--	OR (@PhanCap = 'TM' AND (sTTM IS NULL OR sTTM = ''))
		--	OR (@PhanCap = 'NG' AND (sTNG IS NULL OR sTNG = ''))
		--	)
		AND sLNS IN (SELECT * FROM f_split(@LNS))
		--(
		--			SELECT DISTINCT VALUE
		--			FROM 
		--			(
		--				SELECT 
		--					CAST(LEFT(sLNS, 1) AS nvarchar(10)) LNS1, 
		--					CAST(LEFT(sLNS, 3) AS nvarchar(10)) LNS3, 
		--					CAST(LEFT(sLNS, 5) AS nvarchar(10)) LNS5, 
		--					CAST(sLNS AS nvarchar(10)) LNS 
		--				FROM
		--					NS_NguoiDung_LNS 
		--				WHERE 
		--					sMaNguoiDung = @UserName
		--					AND INamLamViec = @NamLamViec
		--					AND sLNS IN (SELECT * FROM f_split(@LNS))
		--			) LNS
		--			UNPIVOT
		--			(
		--				value
		--				FOR col in (LNS1, LNS3, LNS5, LNS)
		--			) un) 

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
		ISNULL(ctct.fTienKeHoachCap, 0) / @Dvt as FTienKeHoach 
	FROM 
		#tblMlnsByPhanCap mlns
	LEFT JOIN 
		(select * from BH_CP_ChungTu_ChiTiet where iID_CP_ChungTu = @CapPhatId and iID_MaDonVi in (select * from f_split(@IdDonVi))) ctct
	on mlns.iID_MLNS = ctct.iID_MucLucNganSach 
	Order by mlns.sXauNoiMa
END
;
;


select * from BH_CP_ChungTu_ChiTiet
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_kehoach_bh]    Script Date: 9/11/2023 3:16:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_cp_rpt_kehoach_bh]
	@IdMaDonVi NVARCHAR(MAX),
	@IQuy int,
	@NamLamViec int,
	@UserName NVARCHAR(100),
	@Donvitinh int,
	@iIdLoaiCap uniqueidentifier
AS
BEGIN
		select 
			row_number() over (order by ctct.iID_MaDonVi) as STT,
			sum(ctct.fTienDuToan)/@Donvitinh as FTienDuToan,
			sum(ctct.fTienDaCap)/@Donvitinh as FTienDaCap, 
			sum(ctct.fTienKeHoachCap)/@Donvitinh as FTienKeHoachCap,
			ctct.iID_MaDonVi
			into #tblkehoach
		from BH_CP_ChungTu_ChiTiet as ctct
		left join BH_CP_ChungTu as ct on ctct.iID_CP_ChungTu = ct.iID_CP_ChungTu
		where ctct.iID_MaDonVi In (SELECT * FROM f_split(@IdMaDonVi))
			and ct.iNamChungTu = @NamLamViec
			--and ct.iLoaiTongHop <> 2
			and ct.iQuy = @IQuy
			and ct.sNguoiTao=@UserName
			and ct.iID_LoaiCap=@iIdLoaiCap
			group by ctct.iID_MaDonVi

		select kh.*,dv.iID_MaDonVi,dv.sTenDonVi from #tblkehoach kh
		left join DonVi dv on kh.iID_MaDonVi=dv.iID_MaDonVi
		where dv.iNamLamViec=@NamLamViec
		order by STT

		drop table #tblkehoach
	
END


GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_thongtri_tonghop]    Script Date: 9/11/2023 3:16:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_cp_rpt_thongtri_tonghop]
	@NamLamViec int,
	@VoucherId uniqueidentifier
AS
BEGIN
SELECT 
		ct.iID_CP_ChungTu_ChiTiet,
		ct.iID_CP_ChungTu  as IID_CP_ChungTu,
		ct.iID_MaDonVi as IID_MaDonVi,
		ct.iID_MucLucNganSach as IID_MucLucNganSach,
		ct.sM as SM,
		ct.sTM as STM,
		ct.sNoiDung as SNoiDung,
		ct.fTienDaCap as FTienDaCap,
		ct.fTienDuToan as FTienDuToan,
		ct.fTienKeHoachCap as FTienKeHoachCap,
		ct.sGhiChu as SGhiChu ,
		ct.dNgaySua as DNgaySua,
		ct.dNgayTao as DNgayTao,
		ct.sNguoiSua as SNguoiSua,
		ct.sNguoiTao as SNguoiTao,
		ct.sTenDonVi as STenDonVi

	FROM
		(
			SELECT
				--bhct.iID_MaDonVi,
				bh.sMoTa,
				ddv.sTenDonVi,
				bhct.*
			FROM 
				BH_CP_ChungTu bh
			JOIN 
				BH_CP_ChungTu_ChiTiet bhct ON bh.iID_CP_ChungTu = bhct.iID_CP_ChungTu 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bhct.iID_MaDonVi = ddv.iID_MaDonVi
			--WHERE
			--	bh.iID_MaDonVi = @IdDonVi
				--AND (@BKhoa = 3 OR bh.bIsKhoa = @BKhoa) 
		) ct
		where ct.iID_CP_ChungTu=@VoucherId
		;

END
;
;


GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_get_lns]    Script Date: 9/11/2023 3:16:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_dt_dieuchinh_get_lns]
	@namLamViec int,
	@donVi nvarchar(max),
	@loaiDanhMucCapChi nvarchar(100),
	@ngayChungTu datetime,
	@userName nvarchar(100)
AS
BEGIN
	WITH tblLNS AS (
		SELECT DISTINCT sLNS
		FROM BH_DTC_PhanBoDuToanChi_ChiTiet
		WHERE iID_MaDonVi in (select * from f_split(@donVi))
			AND iID_DTC_PhanBoDuToanChi in 
				(SELECT ID 
				FROM BH_DTC_PhanBoDuToanChi 
				WHERE iNamChungTu = @namLamViec 
					AND sSoQuyetDinh IS NOT NULL
					AND sSoQuyetDinh <> ''
					AND bIsKhoa = 1
					AND iID_LoaiDanhMucChi = @loaiDanhMucCapChi
					AND bIsKhoa = 1
					AND cast(dNgayQuyetDinh as datetime) <= cast(@ngayChungTu as datetime))
	), 
	LNSTree AS (
		SELECT *
		FROM BH_DM_MucLucNganSach
		WHERE 
			sL = ''
			AND iNamLamViec = @namLamViec
			AND sLNS in (SELECT * FROM tblLNS)
		UNION ALL
		SELECT mlnsParent.*
		FROM BH_DM_MucLucNganSach mlnsParent
		INNER JOIN LNSTree
			ON mlnsParent.iID_MLNS = lnstree.iID_MLNS_Cha
		WHERE 
			mlnsParent.sL = '' 
			AND mlnsParent.iNamLamViec = @namLamViec
	)
	SELECT mlns.* 
	FROM (SELECT DISTINCT * FROM LNSTree) mlns
	WHERE mlns.iNamlamviec=@namLamViec
	ORDER BY sXauNoiMa
END
;
;
GO
