/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chi_tiet_cssk_hssv_nld]    Script Date: 2/26/2024 2:08:10 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_chungtu_chi_tiet_cssk_hssv_nld]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_chungtu_chi_tiet_cssk_hssv_nld]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chi_tiet_chedo_bhxh]    Script Date: 2/26/2024 2:08:10 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_chungtu_chi_tiet_chedo_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_chungtu_chi_tiet_chedo_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chi_tiet]    Script Date: 2/26/2024 2:08:10 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_chungtu_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_chungtu_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dtc_dieuchinh_tonghop_chitiet_donvi]    Script Date: 2/27/2024 5:27:53 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dtc_dieuchinh_tonghop_chitiet_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dtc_dieuchinh_tonghop_chitiet_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dtc_dieuchinh_tonghop__KQPL_KCBQY_KCBTS_KPK_chitiet_donvi]    Script Date: 2/27/2024 5:27:53 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dtc_dieuchinh_tonghop__KQPL_KCBQY_KCBTS_KPK_chitiet_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dtc_dieuchinh_tonghop__KQPL_KCBQY_KCBTS_KPK_chitiet_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dtc_dieuchinh_chitiet_donvi]    Script Date: 2/27/2024 5:27:53 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dtc_dieuchinh_chitiet_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dtc_dieuchinh_chitiet_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_dtc_rpt_get_donvi]    Script Date: 2/27/2024 5:27:53 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dtc_rpt_get_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dtc_rpt_get_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chi_tiet]    Script Date: 2/26/2024 2:08:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_cp_chungtu_chi_tiet]
	@VoucherId uniqueidentifier ,
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

	DECLARE
		@quy INT;
	SELECT
		@quy = iQuy 
	FROM
		BH_CP_ChungTu 
	WHERE
		iID_CP_ChungTu = @VoucherId;

	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha,sCPChiTietToi
			into #tblNsMlns
	FROM BH_DM_MucLucNganSach 
	where iNamLamViec = @YearOfWork 



		-- lấy ra dữ liệu dự toán
	SELECT 
		  SUM(fTienTuChi) AS fTienDuToan,
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
		WHERE sLNS in (select * from splitstring(@LNS)) and  bHangCha = 1
			and( sL <>'' or sL is not null) and (sCPChiTietToi <>'' or sCPChiTietToi is not null)
		UNION ALL
		SELECT *, null AS iID_MaDonVi, null AS sTenDonVi  
		FROM #tblNsMlns 
		WHERE sLNS in (select * from splitstring(@LNS)) and  bHangCha = 1
			and( sL ='' or sL is null) 
			and (sCPChiTietToi ='' or sCPChiTietToi is null)
	) mlns

	SELECT 
	SUM(ctct.fTienKeHoachCap) fTienDaCap,
	SUM(ctct.fTienKeHoachCap) fTienKeHoachCap,
	ctct.iID_MaDonVi,
	ct.iNamChungTu,
	ctct.sXauNoiMa
	into #tempDuToanCapQuyTruoc
	FROM BH_CP_ChungTu_ChiTiet ctct
	INNER JOIN BH_CP_ChungTu ct ON ctct.iID_CP_ChungTu=ct.iID_CP_ChungTu
	where ct.iQuy<@quy 
	and ct.iID_LoaiCap=@iID_LoaiDanhMucChi
	and CAST(dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
	and ctct.iID_MaDonVi in (select * from splitstring(@AgencyId)) 
	and ct.iNamChungTu=@YearOfWork
	GROUP BY
			ctct.iID_MaDonVi,
			ct.iNamChungTu,
			ctct.sXauNoiMa
	

	---- lấy dữ liệu đã cấp
	--SELECT SUM(fTienDuToan) AS fTienDuToan,
	--	  SUM(fTienKeHoachCap) AS fTienKeHoachCap,
	--	  SUM(fTienDaCap) AS fTienDaCap,
 --         iID_MaDonVi,
 --         iID_MucLucNganSach
	--	  into #tblDataDaCapExist
	--FROM BH_CP_ChungTu_ChiTiet
	--WHERE iID_CP_ChungTu IN
 --      (
	--	SELECT iID_CP_ChungTu
 --       FROM BH_CP_ChungTu
 --       WHERE iNamChungTu = @YearOfWork
	--	  AND iID_LoaiCap = @iID_LoaiDanhMucChi
 --         AND CAST(dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
	--	  AND EXISTS(SELECT * FROM #tempAgency INTERSECT SELECT * FROM f_split(@AgencyId))
	--	  AND iID_CP_ChungTu=@VoucherId
	--	)
	--	AND iID_MaDonVi IN (SELECT * FROM #tempAgency)
	--GROUP BY iID_MaDonVi, iID_MucLucNganSach

	--SELECT sum(fTienDuToan) AS fTienDuToan, sum(fTienKeHoachCap) AS fTienKeHoachCap, sum(fTienDaCap) fTienDaCap, iID_MaDonVi, iID_MucLucNganSach into #tblDataDaCapDuToanExist FROM (
	--	SELECT * FROM #tblDataDaCapExist
	--	) data
	--GROUP BY iID_MaDonVi, iID_MucLucNganSach;
	----select * from  #tblMlnsRoot
	--SELECT mlns.iID_MLNS,
	--	   mlns.iID_MLNS_Cha,
	--	   mlns.sXauNoiMa,
	--	   fTienDuToan,
	--	   fTienDaCap,
	--	   fTienKeHoachCap,
	--	   isnull(mlns.iID_MaDonVi, daCapDuToan.iID_MaDonVi) as iID_MaDonVi
	--	   INTO #tblDaCapDuToanExist
	--FROM #tblMlnsRoot mlns
	--LEFT JOIN
	--  #tblDataDaCapDuToanExist daCapDuToan
	--ON mlns.iID_MLNS = daCapDuToan.iID_MucLucNganSach and ((mlns.iID_MaDonVi is not null and mlns.iID_MaDonVi = daCapDuToan.iID_MaDonVi) or mlns.iID_MaDonVi is null)

	--SELECT iID_MLNS, iID_MLNS_Cha, iID_MaDonVi, sum(fTienDuToan) AS fTienDuToan, sum(fTienDaCap) AS fTienDaCap, SUM(fTienKeHoachCap) as fTienKeHoachCap INTO #tblDaCapDuToanResultExist FROM (
	--	SELECT distinct T.iID_MLNS,  
	--		   T.iID_MLNS_Cha,
	--		   --isnull(T.iID_MaDonVi, T.iID_MaDonVi) as iID_MaDonVi,
	--		   T.iID_MaDonVi,
	--		   T.fTienDuToan,
	--		   T.fTienDaCap,
	--		   T.fTienKeHoachCap
	--	FROM #tblDaCapDuToanExist T 
	--	WHERE T.fTienDaCap <> 0 OR T.fTienDuToan <> 0 OR t.fTienKeHoachCap<>0) data
	--GROUP BY iID_MLNS, iID_MLNS_Cha, iID_MaDonVi
	--option (maxrecursion 0)



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
		mlnsPhanBo.sCPChiTietToi sDuToanChiTietToi,
		@YearOfWork AS INamLamViec,
		mlnsPhanBo.iID_MaDonVi AS IID_MaDonVi,
		mlnsPhanBo.sTenDonVi AS STenDonVi,
		isnull(daCaCap.fTienDaCap, 0) as FTienDaCaQuyTruoc,
		isnull(daCaCap.fTienKeHoachCap, 0) as FTienKeHoachCapQuyTruoc,
		isnull(daCapDuToan.fTienDuToan, 0) as FTienDuToan,
		isnull(ctct.fTienKeHoachCap, 0) as FTienKeHoachCap,
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
	LEFT JOIN #tblPhanBoDuToan daCapDuToan
	on mlnsPhanBo.iID_MLNS = daCapDuToan.iID_MucLucNganSach and daCapDuToan.iID_MaDonVi = mlnsPhanBo.iID_MaDonVi 
	LEFT JOIN #tempDuToanCapQuyTruoc daCaCap on mlnsPhanBo.iID_MaDonVi=daCaCap.iID_MaDonVi and mlnsPhanBo.sXauNoiMa=daCaCap.sXauNoiMa
	where mlnsPhanBo.sLNS IN (SELECT * FROM #tempLNS)
	order by mlnsPhanBo.sXauNoiMa, mlnsPhanBo.iID_MaDonVi;
	
END
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chi_tiet_chedo_bhxh]    Script Date: 2/26/2024 2:08:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_cp_chungtu_chi_tiet_chedo_bhxh]
	@VoucherId uniqueidentifier ,
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
	DECLARE
		@quy INT;
	SELECT
		@quy = iQuy 
	FROM
		BH_CP_ChungTu 
	WHERE
		iID_CP_ChungTu = @VoucherId;
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha,sCPChiTietToi
			into #tblNsMlns
	FROM BH_DM_MucLucNganSach 
	where iNamLamViec = @YearOfWork 
	and iTrangThai=1
	and sLNS in (select * from splitstring(@LNS))
	--and  bHangCha = 1 AND ( (IsNull(sM, '') = '' or sM = '')) AND ( (IsNull(sTM, '') = '' or sTM = '')) OR ((ISNULL(sTM,'')='' OR sTM='')) OR sTM='0'

	-- lấy ra dữ liệu dự toán
	SELECT 
		  SUM(fTienTuChi) AS fTienDuToan,
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

   SELECT 
	SUM(ctct.fTienKeHoachCap) fTienDaCap,
	SUM(ctct.fTienKeHoachCap)  fTienKeHoachCap,
	ctct.iID_MaDonVi,
	ct.iNamChungTu,
	ctct.sXauNoiMa
	into #tempDuToanCapQuyTruoc
	FROM BH_CP_ChungTu_ChiTiet ctct
	INNER JOIN BH_CP_ChungTu ct ON ctct.iID_CP_ChungTu=ct.iID_CP_ChungTu
	where ct.iQuy<@quy 
	and ct.iID_LoaiCap=@iID_LoaiDanhMucChi
	and CAST(dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
	and ctct.iID_MaDonVi in (select * from splitstring(@AgencyId)) 
	and ct.iNamChungTu=@YearOfWork
	GROUP BY
			ctct.iID_MaDonVi,
			ct.iNamChungTu,
			ctct.sXauNoiMa
	
	-- lấy dữ liệu đã cấp
	--SELECT ISNULL(SUM(fTienDuToan),0) AS fTienDuToan,
	--	  ISNULL(SUM(fTienKeHoachCap),0)  AS fTienKeHoachCap,
	--	  ISNULL(SUM(fTienDaCap),0)  AS fTienDaCap,
 --         iID_MaDonVi,
 --         iID_MucLucNganSach
	--	  into #tblDataDaCap
	--FROM BH_CP_ChungTu_ChiTiet
	--WHERE iID_CP_ChungTu IN
 --      (
	--	SELECT iID_CP_ChungTu
 --       FROM BH_CP_ChungTu
 --       WHERE iNamChungTu = @YearOfWork
	--	  AND iID_LoaiCap = @iID_LoaiDanhMucChi
 --         AND CAST(dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
	--	  AND EXISTS(SELECT * FROM #tempAgency INTERSECT SELECT * FROM f_split(@AgencyId))
	--	  AND iID_CP_ChungTu=@VoucherId
	--	)
	--	AND iID_MaDonVi IN (SELECT * FROM #tempAgency)
	--GROUP BY iID_MaDonVi, iID_MucLucNganSach

	-- tao bang tam chua don vi
	SELECT iID_MaDonVi, iID_MaDonVi + ' - ' + sTenDonVi as sTenDonVi into #tblDonVi
	FROM DonVi 
	WHERE 
		INamLamViec = @YearOfWork 
		AND iID_MaDonVi IN (SELECT * FROM f_split(@AgencyId))
	
	
	-- Tao bang tam luu chu mlns cha co don vi
	SELECT * INTO #tblMlnsExistDonVi
	FROM #tblNsMlns ,#tblDonVi dv
		WHERE bHangCha = 1 AND ( (IsNull(sM, '') = '' or sM = '')) AND ( (IsNull(sTM, '') = '' or sTM = '')) OR ((ISNULL(sTM,'')='' OR sTM='')) OR sTM='0'
	
	-- Tao bang tam luu chu mlns va tien du toan chi che do bhxh
	SELECT ml.sXauNoiMa,
		SUM(pb.fTienDuToan) fTienDuToan ,
		ml.iID_MaDonVi 
		INTO #tempMlnsbhxh 
		FROM #tblMlnsExistDonVi ml
   LEFT JOIN #tblPhanBoDuToan pb ON pb.iID_MucLucNganSach=ml.iID_MLNS and ml.iID_MaDonVi=pb.iID_MaDonVi
   GROUP BY ml.sXauNoiMa,ml.iID_MaDonVi
		ORDER BY ml.sXauNoiMa

	SELECT SUBSTRING(A.sXauNoiMa,1,3) sXauNoiMa,
		A.iID_MaDonVi, 
		SUM(A.fTienDuToan) fTienDuToan
		INTO #tblDaCapDuToanResult
		FROM #tempMlnsbhxh  A
		GROUP BY SUBSTRING(A.sXauNoiMa,1,3),A.iID_MaDonVi
		ORDER BY SUBSTRING(A.sXauNoiMa,1,3),A.iID_MaDonVi

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
		mlnsPhanBo.sCPChiTietToi sDuToanChiTietToi,
		@YearOfWork AS INamLamViec,
		mlnsPhanBo.iID_MaDonVi AS IID_MaDonVi,
		mlnsPhanBo.sTenDonVi AS STenDonVi,
		isnull(daCaCap.fTienDaCap, 0) as FTienDaCaQuyTruoc,
		isnull(daCaCap.fTienKeHoachCap, 0) as FTienKeHoachCapQuyTruoc,
		isnull(daCapDuToan.fTienDuToan, 0) as FTienDuToan,
		isnull(ctct.fTienKeHoachCap, 0) as FTienKeHoachCap,
		ctct.sGhiChu AS SGhiChu,
		isnull(ctct.dNgayTao, getdate()) AS DNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS DNgaySua,
		ctct.sNguoiTao AS SNguoiTao,
		ctct.sNguoiSua AS SNguoiSua
	FROM #tblMlnsExistDonVi AS mlnsPhanBo
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
	on mlnsPhanBo.sXauNoiMa = daCapDuToan.sXauNoiMa and daCapDuToan.iID_MaDonVi= mlnsPhanBo.iID_MaDonVi
	LEFT JOIN #tempDuToanCapQuyTruoc daCaCap on mlnsPhanBo.iID_MaDonVi=daCaCap.iID_MaDonVi and mlnsPhanBo.sXauNoiMa=daCaCap.sXauNoiMa
	where mlnsPhanBo.sLNS ='901'
	order by mlnsPhanBo.sXauNoiMa, mlnsPhanBo.iID_MaDonVi;

END
;
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chi_tiet_cssk_hssv_nld]    Script Date: 2/26/2024 2:08:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_cp_chungtu_chi_tiet_cssk_hssv_nld]
	@VoucherId uniqueidentifier ,
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
	DECLARE
		@quy INT;
	SELECT
		@quy = iQuy 
	FROM
		BH_CP_ChungTu 
	WHERE
		iID_CP_ChungTu = @VoucherId;
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha,sCPChiTietToi
			into #tblNsMlns
	FROM BH_DM_MucLucNganSach 
	where iNamLamViec = @YearOfWork 
	and iTrangThai=1
	and sLNS in (select * from splitstring(@LNS))
	--and  bHangCha = 1 AND ( (IsNull(sM, '') = '' or sM = '')) AND ( (IsNull(sTM, '') = '' or sTM = '')) OR ((ISNULL(sTM,'')='' OR sTM='')) OR sTM='0'

	-- lấy ra dữ liệu dự toán
	SELECT 
		  SUM(fTienTuChi) AS fTienDuToan,
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

	-- lấy dữ liệu đã cấp
	   SELECT 
	SUM(ctct.fTienKeHoachCap) fTienDaCap,
	SUM(ctct.fTienKeHoachCap) fTienKeHoachCap,
	ctct.iID_MaDonVi,
	ct.iNamChungTu,
	ctct.sXauNoiMa
	into #tempDuToanCapQuyTruoc
	FROM BH_CP_ChungTu_ChiTiet ctct
	INNER JOIN BH_CP_ChungTu ct ON ctct.iID_CP_ChungTu=ct.iID_CP_ChungTu
	where ct.iQuy<@quy 
	and ct.iID_LoaiCap=@iID_LoaiDanhMucChi
	and CAST(dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
	and ctct.iID_MaDonVi in (select * from splitstring(@AgencyId)) 
	and ct.iNamChungTu=@YearOfWork
	GROUP BY
			ctct.iID_MaDonVi,
			ct.iNamChungTu,
			ctct.sXauNoiMa
	--SELECT ISNULL(SUM(fTienDuToan),0) AS fTienDuToan,
	--	  ISNULL(SUM(fTienKeHoachCap),0)  AS fTienKeHoachCap,
	--	  ISNULL(SUM(fTienDaCap),0)  AS fTienDaCap,
 --         iID_MaDonVi,
 --         iID_MucLucNganSach
	--	  into #tblDataDaCap
	--FROM BH_CP_ChungTu_ChiTiet
	--WHERE iID_CP_ChungTu IN
 --      (
	--	SELECT iID_CP_ChungTu
 --       FROM BH_CP_ChungTu
 --       WHERE iNamChungTu = @YearOfWork
	--	  AND iID_LoaiCap = @iID_LoaiDanhMucChi
 --         AND CAST(dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
	--	  AND EXISTS(SELECT * FROM #tempAgency INTERSECT SELECT * FROM f_split(@AgencyId))
	--	  AND iID_CP_ChungTu=@VoucherId
	--	)
	--	AND iID_MaDonVi IN (SELECT * FROM #tempAgency)
	--GROUP BY iID_MaDonVi, iID_MucLucNganSach

	-- tao bang tam chua don vi
	SELECT iID_MaDonVi, iID_MaDonVi + ' - ' + sTenDonVi as sTenDonVi into #tblDonVi
	FROM DonVi 
	WHERE 
		INamLamViec = @YearOfWork 
		AND iID_MaDonVi IN (SELECT * FROM f_split(@AgencyId))
	
	
	
	-- Tao bang tam luu chu mlns cha co don vi
	SELECT *,'' AS iID_MaDonVi, '' AS sTenDonVi   INTO #tblMlnsByPhanCapNotDonVi
	FROM #tblNsMlns 
	WHERE bHangCha = 1 AND ( (IsNull(sM, '') = '' or sM = '')) AND ( (IsNull(sTM, '') = '' or sTM = '')) 
	
		-- Tao bang tam luu chu mlns cha co don vi
	SELECT * INTO #tblMlnsByPhanCapExistDonVi
	FROM #tblNsMlns ,#tblDonVi dv
		WHERE  bHangCha=0

		-- map bảng mlnsPhanCap và đơn vị
	SELECT * INTO #tblMLNS FROM (
		SELECT *
		FROM #tblMlnsByPhanCapExistDonVi tbl
		UNION ALL
		SELECT *
		FROM #tblMlnsByPhanCapNotDonVi 
	) mlns

	
	---- Tao bang tam luu chu mlns va tien du toan chi che do bhxh
	--SELECT ml.sXauNoiMa,
	--	SUM(pb.fTienDuToan) fTienDuToan ,
	--	ml.iID_MaDonVi 
	--	INTO #tempMlnsbhxh 
	--	FROM #tblMlnsExistDonVi ml
 --  LEFT JOIN #tblPhanBoDuToan pb ON pb.iID_MucLucNganSach=ml.iID_MLNS and ml.iID_MaDonVi=pb.iID_MaDonVi
 --  GROUP BY ml.sXauNoiMa,ml.iID_MaDonVi
	--	ORDER BY ml.sXauNoiMa

	--SELECT SUBSTRING(A.sXauNoiMa,1,3) sXauNoiMa,
	--	A.iID_MaDonVi, 
	--	SUM(A.fTienDuToan) fTienDuToan
	--	INTO #tblDaCapDuToanResult
	--	FROM #tempMlnsbhxh  A
	--	GROUP BY SUBSTRING(A.sXauNoiMa,1,3),A.iID_MaDonVi
	--	ORDER BY SUBSTRING(A.sXauNoiMa,1,3),A.iID_MaDonVi

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
		mlnsPhanBo.sCPChiTietToi sDuToanChiTietToi,
		@YearOfWork AS INamLamViec,
		mlnsPhanBo.iID_MaDonVi AS IID_MaDonVi,
		mlnsPhanBo.sTenDonVi AS STenDonVi,
		isnull(daCaCap.fTienDaCap, 0) as FTienDaCaQuyTruoc,
		isnull(daCaCap.fTienKeHoachCap, 0) as FTienKeHoachCapQuyTruoc,
		0 as FTienDuToan,
		isnull(ctct.fTienKeHoachCap, 0) as FTienKeHoachCap,
		ctct.sGhiChu AS SGhiChu,
		isnull(ctct.dNgayTao, getdate()) AS DNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS DNgaySua,
		ctct.sNguoiTao AS SNguoiTao,
		ctct.sNguoiSua AS SNguoiSua
	FROM #tblMLNS AS mlnsPhanBo
	LEFT JOIN
		(SELECT *
			FROM 
				BH_CP_ChungTu_ChiTiet
			WHERE 
		 		iID_CP_ChungTu = @VoucherId
				
		) ctct
	ON mlnsPhanBo.iID_MLNS = ctct.iID_MucLucNganSach and mlnsPhanBo.iID_MaDonVi = ctct.iID_MaDonVi
	LEFT JOIN BH_CP_ChungTu ct ON ctct.iID_CP_ChungTu = ct.iID_CP_ChungTu
	LEFT JOIN #tempDuToanCapQuyTruoc daCaCap on mlnsPhanBo.iID_MaDonVi=daCaCap.iID_MaDonVi and mlnsPhanBo.sXauNoiMa=daCaCap.sXauNoiMa
	--LEFT JOIN #tempMlnsbhxh daCapDuToan
	--on mlnsPhanBo.sXauNoiMa = daCapDuToan.sXauNoiMa and daCapDuToan.iID_MaDonVi= mlnsPhanBo.iID_MaDonVi
	
	order by mlnsPhanBo.sXauNoiMa ,mlnsPhanBo.iID_MaDonVi;

END
;
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dtc_rpt_get_donvi]    Script Date: 2/27/2024 5:27:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dtc_rpt_get_donvi]
@NamLamViec int,
@IDLoaiChi uniqueidentifier
AS BEGIN
SET NOCOUNT ON;

SELECT
	DISTINCT
	donvi.iID_DonVi AS Id,
	donvi.iID_MaDonVi as IIDMaDonVi,
	donvi.sTenDonVi as TenDonVi,
	donvi.sKyHieu as KyHieu,
	donvi.sMoTa as MoTa,
	donvi.iLoai as Loai,
	donvi.iNamLamViec as NamLamViec,
	donvi.iTrangThai as iTrangThai,
	donvi.dNgayTao as DNgayTao,
	donvi.sNguoiTao as SNguoiTao,
	donvi.dNgaySua as DNgaySua,
	donvi.dNgaySua as SNguoiSua,
	donvi.*

FROM BH_DTC_DieuChinhDuToanChi chungtu
LEFT JOIN DonVi donvi ON chungtu.iID_MaDonVi = donvi.iID_MaDonVi
WHERE chungtu.iNamLamViec = @NamLamViec
  AND chungtu.iID_MaDonVi <> ''
  AND chungtu.iID_MaDonVi IS NOT NULL
  AND donvi.iNamLamViec = @NamLamViec
  AND chungtu.iID_LoaiCap=@IDLoaiChi
  --AND chungtu.iLoaiTongHop=@LoaiChungTu
 -- ORDER BY 
	--donvi.iID_DonVi,
	--donvi.iID_MaDonVi ,
	--donvi.sTenDonVi ,
	--donvi.sKyHieu ,
	--donvi.sMoTa ,
	--donvi.iLoai ,
	--donvi.iNamLamViec,
	--donvi.iTrangThai,
	--donvi.dNgayTao ,
	--donvi.sNguoiTao ,
	--donvi.dNgaySua ,
	--donvi.dNgaySua ,
  
--ORDER BY cast(chungtu.dNgayChungTu AS date)
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dtc_dieuchinh_chitiet_donvi]    Script Date: 2/27/2024 5:27:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dtc_dieuchinh_chitiet_donvi]
	@NamLamViec int,
	@IdDonVi nvarchar(max),
	@IDLoaiChi uniqueidentifier,
	@SLNS nvarchar(max),
	@ngayChungTu date, 
	@Dvt int,
	@Loai int
AS
BEGIN

	SELECT ML.*
			into #tblml
	FROM BH_DM_MucLucNganSach ML
	WHERE ML.sLNS IN  (SELECT * FROM splitstring(@SLNS))
			AND ML.iNamLamViec=@NamLamViec
			AND ML.bHangCha IS NOT NULL
	ORDER BY sXauNoiMa
	--- Chung tu thuong
	IF (@Loai=0)
	SELECT 
		ml.iID_MLNS as iID_MucLucNganSach,
		ml.sM,
		ml.SLNS,
		ml.sTM,
		ml.sTTM,
		ml.sMoTa as sNoiDung,
		ml.sXauNoiMa,
		ml.iID_MLNS_Cha as IdParent,
		ml.bHangChaDuToanDieuChinh IsHangCha,
		ml.sDuToanChiTietToi,
		ct.iNamLamViec,
		ISNULL(SUM(dt.fTienTuChi),0)/ @Dvt fTienDuToanDuocGiao,
		ISNULL(SUM(ct.fTienThucHien06ThangDauNam),0)/ @Dvt fTienThucHien06ThangDauNam,
		ISNULL(SUM(ct.fTienUocThucHien06ThangCuoiNam),0)/ @Dvt fTienUocThucHien06ThangCuoiNam,
		ISNULL(SUM(ct.fTienUocThucHienCaNam),0) /@Dvt fTienUocThucHienCaNam,
		ISNULL(SUM(ct.fTienSoSanhTang),0)/ @Dvt fTienSoSanhTang,
		ISNULL(SUM(dt.fTienTuChi) - (SUM(ct.fTienUocThucHienCaNam)),0)/ @Dvt fTienSoSanhGiam

	FROM
	#tblml ml 
	LEFT JOIN
		(
			SELECT
				--bh.iID_MaDonVi,
				bh.sMoTa,
				ddv.sTenDonVi,
				bhct.*
			FROM 
				BH_DTC_DieuChinhDuToanChi bh
			JOIN 
				BH_DTC_DieuChinhDuToanChi_ChiTiet bhct ON bh.iID_BH_DTC = bhct.iID_BH_DTC 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bh.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bh.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
				AND	bh.iNamLamViec=@NamLamViec
				AND BH.iID_LoaiCap=@IDLoaiChi
				--AND (@BKhoa = 3 OR bh.bIsKhoa = @BKhoa) 
		) ct
		ON ct.iID_MucLucNganSach=ml.iID_MLNS
		LEFT JOIN (
			SELECT ctct.sXauNoiMa
					, SUM(ctct.fTienTuChi)  fTienTuChi
			
			FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct 
			JOIN BH_DTC_PhanBoDuToanChi ct ON ctct.iID_DTC_PhanBoDuToanChi = ct.ID 
				WHERE ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
				AND BIsKhoa = 1
				AND ct.iNamChungTu = @NamLamViec
				GROUP BY ctct.sXauNoiMa) dt ON dt.sXauNoiMa=ml.sXauNoiMa 
		WHERE ml.iNamLamViec=@NamLamViec
		GROUP BY 
		ml.iID_MLNS,
		ml.iID_MLNS_Cha,
		ml.bHangChaDuToanDieuChinh,
		ml.SLNS,
		ml.sM,
		ml.sTM,
		ml.sTTM,
		ml.sMoTa,
		ml.sXauNoiMa,
		ct.iNamLamViec,
		ml.sDuToanChiTietToi
		ORDER BY sXauNoiMa
		--- Chung tu tong hop
		ELSE 
		SELECT 
		ml.iID_MLNS as iID_MucLucNganSach,
		ml.sM,
		ml.SLNS,
		ml.sTM,
		ml.sTTM,
		ml.sMoTa as sNoiDung,
		ml.sXauNoiMa,
		ml.iID_MLNS_Cha as IdParent,
		ml.bHangChaDuToanDieuChinh IsHangCha,
		ml.sDuToanChiTietToi,
		ct.iNamLamViec,
		ISNULL(SUM(dt.fTienTuChi),0)/ @Dvt fTienDuToanDuocGiao,
		ISNULL(SUM(ct.fTienThucHien06ThangDauNam),0)/ @Dvt fTienThucHien06ThangDauNam,
		ISNULL(SUM(ct.fTienUocThucHien06ThangCuoiNam),0)/ @Dvt fTienUocThucHien06ThangCuoiNam,
		ISNULL(SUM(ct.fTienUocThucHienCaNam),0) /@Dvt fTienUocThucHienCaNam,
		ISNULL(SUM(ct.fTienSoSanhTang),0)/ @Dvt fTienSoSanhTang,
		ISNULL(SUM(dt.fTienTuChi) - (SUM(ct.fTienUocThucHienCaNam)),0)/ @Dvt fTienSoSanhGiam

	FROM
	#tblml ml 
	LEFT JOIN
		(
			SELECT
				--bh.iID_MaDonVi,
				bh.sMoTa,
				ddv.sTenDonVi,
				bhct.*
			FROM 
				BH_DTC_DieuChinhDuToanChi bh
			JOIN 
				BH_DTC_DieuChinhDuToanChi_ChiTiet bhct ON bh.iID_BH_DTC = bhct.iID_BH_DTC 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bh.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bh.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
				AND	bh.iNamLamViec=@NamLamViec
				AND BH.iID_LoaiCap=@IDLoaiChi
				--AND (@BKhoa = 3 OR bh.bIsKhoa = @BKhoa) 
		) ct
		ON ct.iID_MucLucNganSach=ml.iID_MLNS
		LEFT JOIN (
					SELECT ctct.sXauNoiMa,
							SUM(ctct.fTienTuChi) fTienTuChi
							FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct 
							JOIN BH_DTC_DuToanChiTrenGiao ct ON ctct.iID_DTC_DuToanChiTrenGiao = ct.ID 
							WHERE ct.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
							AND BIsKhoa = 1
							AND ct.iID_LoaiDanhMucChi=@IDLoaiChi
							AND ct.iNamLamViec = @NamLamViec
							GROUP BY ctct.sXauNoiMa) dt ON dt.sXauNoiMa = ml.sXauNoiMa 
		WHERE ml.iNamLamViec=@NamLamViec
		GROUP BY 
		ml.iID_MLNS,
		ml.iID_MLNS_Cha,
		ml.bHangChaDuToanDieuChinh,
		ml.SLNS,
		ml.sM,
		ml.sTM,
		ml.sTTM,
		ml.sMoTa,
		ml.sXauNoiMa,
		ct.iNamLamViec,
		ml.sDuToanChiTietToi
		ORDER BY sXauNoiMa;

		DROP TABLE #tblml

END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dtc_dieuchinh_tonghop__KQPL_KCBQY_KCBTS_KPK_chitiet_donvi]    Script Date: 2/27/2024 5:27:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dtc_dieuchinh_tonghop__KQPL_KCBQY_KCBTS_KPK_chitiet_donvi]
	@NamLamViec int,
	@IdDonVi nvarchar(200),
	@IDLoaiChi uniqueidentifier,
	@SLNS nvarchar(max),
	@ngayChungTu date, 
	@Dvt int
AS
BEGIN

	SELECT 
			B.iID_MLNS as iID_MucLucNganSach,
			B.iID_MLNS_Cha as IdParent,
			B.bHangCha as IsHangCha,
			B.sLNS,
			B.sL,
			B.sM,
			B.sTM,
			b.sMoTa sNoiDung,
			B.sXauNoiMa,
			B.sDuToanChiTietToi,
			A.fTienDuToanDuocGiao / @Dvt fTienDuToanDuocGiao,
			A.fTienSoSanhGiam/ @Dvt fTienSoSanhGiam,
			A.fTienSoSanhTang/ @Dvt fTienSoSanhTang,
			A.fTienThucHien06ThangDauNam/ @Dvt fTienThucHien06ThangDauNam,
			A.fTienUocThucHien06ThangCuoiNam / @Dvt fTienUocThucHien06ThangCuoiNam,
			A.fTienUocThucHienCaNam / @Dvt fTienUocThucHienCaNam,
			A.iID_MaDonVi,
			1 TYPE
			into #temp1
		FROM BH_DM_MucLucNganSach B
		LEFT JOIN (
			SELECT * FROM BH_DTC_DieuChinhDuToanChi_ChiTiet
			WHERE iID_BH_DTC IN 
								(SELECT iID_BH_DTC
									FROM BH_DTC_DieuChinhDuToanChi 
									WHERE iID_MaDonVi in (select * from splitstring(@IdDonVi))
									AND  iID_LoaiCap=@IDLoaiChi
									AND iNamLamViec=@NamLamViec
										)
			) A ON A.iID_MucLucNganSach=B.iID_MLNS
			
			LEFT JOIN (
			SELECT ctct.sXauNoiMa
					, SUM(ctct.fTienTuChi)  fTienTuChi
					, ctct.iID_MaDonVi
			FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct 
			JOIN BH_DTC_PhanBoDuToanChi ct ON ctct.iID_DTC_PhanBoDuToanChi = ct.ID 
				WHERE ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
				AND BIsKhoa = 1
				AND ct.iNamChungTu = @NamLamViec
				GROUP BY ctct.sXauNoiMa,ctct.iID_MaDonVi) dt ON dt.sXauNoiMa=A.sXauNoiMa  and dt.iID_MaDonVi=A.iID_MaDonVi
			WHERE B.iNamLamViec=@NamLamViec
			AND (A.fTienDuToanDuocGiao<>0 
			OR A.fTienSoSanhGiam<>0 
			OR A.fTienSoSanhTang<>0 
			OR A.fTienThucHien06ThangDauNam<>0
			OR A.fTienUocThucHien06ThangCuoiNam<>0
			OR A.fTienUocThucHienCaNam<>0)

	SELECT 
		    tbl1.iID_MucLucNganSach,
			tbl1.IdParent,
			tbl1.IsHangCha ,
			tbl1.sLNS,
			tbl1.sL,
			tbl1.sM,
			tbl1.sTM,
			tbl1.sDuToanChiTietToi,
			'          '+ dv.sTenDonVi sNoiDung,
			tbl1.sXauNoiMa,
			tbl1.fTienDuToanDuocGiao,
			tbl1.fTienSoSanhGiam ,
			tbl1.fTienSoSanhTang ,
			tbl1.fTienThucHien06ThangDauNam ,
			tbl1.fTienUocThucHien06ThangCuoiNam ,
			tbl1.fTienUocThucHienCaNam ,
			tbl1.iID_MaDonVi,
			tbl1.TYPE
			INTO #tempChillMlns
		FROM #temp1 tbl1
		LEFT JOIN  DonVi dv ON tbl1.iID_MaDonVi=dv.iID_MaDonVi
		WHERE dv.iNamLamViec=2024
		AND tbl1.sDuToanChiTietToi is null
		order by sXauNoiMa

		SELECT 
			B.iID_MLNS as iID_MucLucNganSach,
			B.iID_MLNS_Cha as IdParent,
			B.bHangCha as IsHangCha,
			B.sLNS,
			B.sL,
			B.sM,
			B.sTM,
			B.sDuToanChiTietToi,
			b.sMoTa sNoiDung,
			B.sXauNoiMa,
			dt.fTienTuChi / @Dvt fTienDuToanDuocGiao,
			Sum(A.fTienSoSanhGiam) /@Dvt fTienSoSanhGiam,
			Sum(A.fTienSoSanhTang) / @Dvt fTienSoSanhTang,
			Sum(A.fTienThucHien06ThangDauNam) / @Dvt fTienThucHien06ThangDauNam,
			Sum(A.fTienUocThucHien06ThangCuoiNam) / @Dvt fTienUocThucHien06ThangCuoiNam,
			Sum(fTienUocThucHienCaNam) / @Dvt fTienUocThucHienCaNam,
			null iID_MaDonVi,
			0 TYPE
			into #tblBhMlns
		FROM BH_DM_MucLucNganSach B
		LEFT JOIN (
			SELECT * FROM BH_DTC_DieuChinhDuToanChi_ChiTiet
			WHERE iID_BH_DTC IN 
								(SELECT iID_BH_DTC
									FROM BH_DTC_DieuChinhDuToanChi 
									WHERE iID_MaDonVi in (select * from splitstring(@IdDonVi))
									AND  iID_LoaiCap=@IDLoaiChi
									AND iNamLamViec=@NamLamViec
										)
			) A ON A.iID_MucLucNganSach=B.iID_MLNS
			LEFT JOIN (
			SELECT ctct.sXauNoiMa
					, SUM(ctct.fTienTuChi)  fTienTuChi
			FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct 
			JOIN BH_DTC_PhanBoDuToanChi ct ON ctct.iID_DTC_PhanBoDuToanChi = ct.ID 
				WHERE ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
				AND BIsKhoa = 1
				AND ct.iNamChungTu = @NamLamViec
				GROUP BY ctct.sXauNoiMa) dt ON dt.sXauNoiMa=B.sXauNoiMa 
			WHERE B.iNamLamViec=@NamLamViec
			AND B.sLNS IN (SELECT * FROM splitstring(@SLNS))
			group by B.iID_MLNS,
				B.sLNS,
				B.sMoTa,
				B.iID_MLNS_Cha,
				B.sL,
				B.sM,
				B.sTM,
				B.sDuToanChiTietToi,
				B.sXauNoiMa,
				B.bHangCha,
				dt.fTienTuChi
		order by sXauNoiMa

	-- map bảng 
		SELECT * INTO #tblMLNS FROM (
			SELECT *
			FROM #tblBhMlns tbl
			UNION ALL
			SELECT *
			FROM #tempChillMlns 
		) mlns

		select * from #tblMLNS
		order by sXauNoiMa, TYPE 


		DROP TABLE #tblMLNS	
		DROP TABLE #tblBhMlns	
		DROP TABLE #tempChillMlns	
		DROP TABLE #temp1
END
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dtc_dieuchinh_tonghop_chitiet_donvi]    Script Date: 2/27/2024 5:27:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dtc_dieuchinh_tonghop_chitiet_donvi]
	@NamLamViec int,
	@IdDonVi nvarchar(100),
	@IDLoaiChi uniqueidentifier,
	@SLNS nvarchar(max),
	@ngayChungTu date, 
	@Dvt int
AS
BEGIN

	SELECT ML.*
			into #tblml
	FROM BH_DM_MucLucNganSach ML
	WHERE ML.sLNS IN  (SELECT * FROM splitstring(@SLNS))
			AND ML.iNamLamViec=@NamLamViec
			ANd ML.bHangChaDuToanDieuChinh is not null
	ORDER BY sXauNoiMa

	SELECT 
						chitiet.fTienDuToanDuocGiao, 
						chitiet.fTienSoSanhGiam, chitiet.fTienSoSanhTang, 
						dtct_by_donvi.fTienThucHien06ThangDauNam as fTienThucHien06ThangDauNam, 
						dtct_by_donvi.fTienUocThucHien06ThangCuoiNam as fTienUocThucHien06ThangCuoiNam, 
						dtct_by_donvi.fTienUocThucHienCaNam as fTienUocThucHienCaNam,
						chitiet.iID_BH_DTC,
						chitiet.iID_MucLucNganSach,
						dtct_by_donvi.sNoiMa,
						chitiet.sXauNoiMa,
						chitiet.iNamLamViec
					into #bh_dtc_dieuchinh_chitiet
					FROM BH_DTC_DieuChinhDuToanChi_ChiTiet chitiet
					LEFT JOIN (
						SELECT sTM, 
							iID_MaDonVi, 
							sNoiMa,
							sM,
							SUM(fTienThucHien06ThangDauNam) as fTienThucHien06ThangDauNam, 
							SUM(fTienUocThucHien06ThangCuoiNam) as fTienUocThucHien06ThangCuoiNam,
							SUM(fTienUocThucHienCaNam) as fTienUocThucHienCaNam 
						FROM (
								SELECT *, 
									CASE WHEN sXauNoiMa LIKE '9010001%' THEN '9010001' 
									WHEN sXauNoiMa LIKE '9010002%' THEN '9010002' END as sNoiMa
								FROM BH_DTC_DieuChinhDuToanChi_ChiTiet 
							) BH_DTC_DieuChinhDuToanChi_ChiTiet
						WHERE iNamLamViec=@NamLamViec
						AND iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
						AND sTM != ''
						GROUP BY sTM, iID_MaDonVi, sNoiMa, sM
					) dtct_by_donvi ON chitiet.sTM = '' 
						AND chitiet.sM = dtct_by_donvi.sM 
						AND chitiet.iID_MaDonVi = dtct_by_donvi.iID_MaDonVi
						AND chitiet.sXauNoiMa LIKE (dtct_by_donvi.sNoiMa + '%')

					ORDER BY chitiet.sXauNoiMa

	SELECT 
		--ct.iID_MaDonVi,
		ct.sTenDonVi as STenDonVi,
		ml.iID_MLNS as iID_MucLucNganSach,
		ml.sM,
		ml.SLNS,
		ml.sTM,
		ml.sTTM,
		ml.sMoTa as sNoiDung,
		ml.sXauNoiMa,
		ml.iID_MLNS_Cha as IdParent,
		ml.bHangChaDuToanDieuChinh bHangCha,
		--ml.bHangChaDuToanDieuChinh IsHangCha,
		ct.iNamLamViec,
		ISNULL((dt.fTienTuChi),0)/ @Dvt fTienDuToanDuocGiao,
		ISNULL(SUM(ct.fTienThucHien06ThangDauNam),0)/ @Dvt fTienThucHien06ThangDauNam,
		ISNULL(SUM(ct.fTienUocThucHien06ThangCuoiNam),0)/ @Dvt fTienUocThucHien06ThangCuoiNam,
		ISNULL(SUM(ct.fTienUocThucHienCaNam),0) /@Dvt fTienUocThucHienCaNam,
		ISNULL(SUM(ct.fTienSoSanhTang),0)/ @Dvt fTienSoSanhTang,
		ISNULL(SUM(ct.fTienSoSanhGiam),0)/ @Dvt fTienSoSanhGiam

	FROM
	#tblml ml 
	LEFT JOIN
		(
			SELECT
				bh.sMoTa,
				CASE 
					WHEN (SELECT COUNT(*) FROM splitstring(@IdDonVi)) > 1 THEN ''
					ELSE ddv.sTenDonVi
				END as sTenDonVi,
				--ddv.sTenDonVi,
				bhct.*
			FROM 
				BH_DTC_DieuChinhDuToanChi bh
			JOIN 
				#bh_dtc_dieuchinh_chitiet bhct ON bh.iID_BH_DTC = bhct.iID_BH_DTC 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bh.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bh.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
				AND	bh.iNamLamViec=@NamLamViec
				AND BH.iID_LoaiCap=@IDLoaiChi
				--AND (@BKhoa = 3 OR bh.bIsKhoa = @BKhoa) 
		) ct
		ON ct.iID_MucLucNganSach=ml.iID_MLNS
		LEFT JOIN (
			SELECT ctct.sXauNoiMa
					, SUM(ctct.fTienTuChi)  fTienTuChi
			
			FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct 
			JOIN BH_DTC_PhanBoDuToanChi ct ON ctct.iID_DTC_PhanBoDuToanChi = ct.ID 
				WHERE ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
				AND BIsKhoa = 1
				AND ct.iNamChungTu = @NamLamViec
				GROUP BY ctct.sXauNoiMa) dt ON dt.sXauNoiMa=ml.sXauNoiMa 
		WHERE ml.iNamLamViec=@NamLamViec
		--and ml.bHangChaDuToanDieuChinh = 1
		and ml.sTM = ''
		GROUP BY 
		ml.iID_MLNS,
		ml.iID_MLNS_Cha,
		ml.bHangChaDuToanDieuChinh,
		ml.SLNS,
		ml.sM,
		ml.sTM,
		ml.sTTM,
		ml.sMoTa,
		ml.sXauNoiMa,
		ct.iNamLamViec,
		ct.sTenDonVi
		--ct.iID_MaDonVi,
		ORDER BY sXauNoiMa;
		
		DROP TABLE #tblml		
		DROP TABLE #bh_dtc_dieuchinh_chitiet
END
;
;
;
;
;
;
GO
