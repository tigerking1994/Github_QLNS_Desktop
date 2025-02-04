/****** Object:  StoredProcedure [dbo].[sp_qtc_rpt_get_donvi_nKPQL_lns]    Script Date: 12/28/2023 9:36:50 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_rpt_get_donvi_nKPQL_lns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_rpt_get_donvi_nKPQL_lns]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_quykinhphi_khac_chungtu_chi_tiet]    Script Date: 12/28/2023 9:36:50 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_quykinhphi_khac_chungtu_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_quykinhphi_khac_chungtu_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_kcbqy_slns]    Script Date: 12/28/2023 9:36:50 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_kcbqy_slns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_kcbqy_slns]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_kcbqy_slns]    Script Date: 12/28/2023 9:36:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_kcbqy_slns]
@iDNdtctg uniqueidentifier,
@sLns nvarchar(max),
@NamLamViec int,
@IIDDonVi nvarchar(50)

AS
BEGIN

SELECT 
	A.sLNS,
	A.sTM,
	A.sTTM,
	A.sM,
	A.sNG,
	A.sMoTa AS sNoiDung,
	A.sXauNoiMa,
	A.iID_MLNS,
	A.iID_MLNS_Cha,
	A.bHangCha,
	A.bHangCha as isHangCha,
	B.iID_DTC_DuToanChiTrenGiao,
	A.iID_MLNS AS iID_MucLucNganSach,
	B.fTienKeHoachThucHienNamNay as fTienTuChi,
	A.iNamlamViec,
	@IIDDonVi as iID_MaDonVi
	FROM BH_DM_MucLucNganSach AS A
	LEFT JOIN (select	
		@iDNdtctg as iID_DTC_DuToanChiTrenGiao
		, ctct.iID_MucLucNganSach
		, ctct.fTienKeHoachThucHienNamNay
		from BH_KHC_KCB_ChiTiet ctct
left join BH_KHC_KCB ct on ctct.iID_KHC_KCB=ct.iID_BH_KHC_KCB
where (ct.iLoaiTongHop=2 ) and ct.iID_MaDonVi=@IIDDonVi
		and ct.iNamLamViec=@NamLamViec
		and ct.bIsKhoa=1) AS B
	ON B.iID_MucLucNganSach = A.iID_MLNS
	WHERE   A.sLNS IN (SELECT * FROM f_split(@sLns))
		AND A.iNamlamViec=@NamLamViec
	order by A.sXauNoiMa
END


GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_quykinhphi_khac_chungtu_chi_tiet]    Script Date: 12/28/2023 9:36:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Doantc
-- Create date: 20/09/2023
-- Description:	Lấy danh sách hiển thị chứng từ quyết toán chi quy kinh phí khác chi tiết

-- =============================================
CREATE PROCEDURE [dbo].[sp_qtc_quykinhphi_khac_chungtu_chi_tiet]
	@VoucherId uniqueidentifier,
	@LNS nvarchar(max),
	@YearOfWork int,
	@AgencyId nvarchar(500),
	@VoucherDate datetime,
	@iID_LoaiDanhMucChi uniqueidentifier,
	@iQuyChungTu int
AS
BEGIN
	DECLARE @iD uniqueidentifier='00000000-0000-0000-0000-000000000000';
	DECLARE @CountIndex INT;
	SELECT * into #tempLNS from f_split(@LNS);
	SELECT * into #tempAgency from  f_split(@AgencyId);
	SELECT @CountIndex = COUNT(*) FROM 
									BH_QTC_Quy_KPK_ChiTiet 
									WHERE iID_QTC_Quy_KPK =@VoucherId

	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha
			into #tblNsMlns
	FROM BH_DM_MucLucNganSach 
	where iNamLamViec = @YearOfWork
	and iTrangThai=1;

		-- lấy ra dữ liệu dự toán
	SELECT 
		  SUM(fTienTuChi) AS fTien_DuToanGiaoNamNay,
          0 AS fTien_DuToanNamTruocChuyenSang,
          0 AS fTien_TongDuToanDuocGiao,
		  0 AS fTienThucChi,
		  0 AS fTienQuyetToanDaDuyet,
		  0 AS fTienDeNghiQuyetToanQuyNay,
		  0 AS fTienXacNhanQuyetToanQuyNay,
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
	BEGIN
	-- lấy dữ liệu đã cấp
	SELECT  
		0 AS fTien_DuToanGiaoNamNay,
		SUM(fTien_DuToanNamTruocChuyenSang) AS fTien_DuToanNamTruocChuyenSang,
		SUM(fTien_TongDuToanDuocGiao) AS fTien_TongDuToanDuocGiao,
		SUM(fTienThucChi) AS fTienThucChi,
		SUM(fTienQuyetToanDaDuyet) AS fTienQuyetToanDaDuyet,
		SUM(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay,
		SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay,
		CT.iID_MaDonVi,
	    CTCT.iID_MucLucNganSach
		into #tblDataDaCap
	FROM
	BH_QTC_Quy_KPK CT
	INNER JOIN BH_QTC_Quy_KPK_ChiTiet CTCT
	ON CT.ID_QTC_Quy_KPK=CTCT.iID_QTC_Quy_KPK
	WHERE CT.iNamChungTu = @YearOfWork
		  --AND i = @iID_LoaiDanhMucChi
          AND CAST(CT.dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
		  AND CT.ID_QTC_Quy_KPK=@VoucherId
		  AND CT.iID_MaDonVi IN (SELECT * FROM f_split(@AgencyId))
		  AND CT.iQuyChungTu<@iQuyChungTu

	GROUP BY  CT.iID_MaDonVi,CTCT.iID_MucLucNganSach

	--SELECT * into #tempAgency from  f_split(N'001'); 


	--SELECT 0 AS fTienDuToanDuocGiao,
	--	  SUM(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay,
	--	  SUM(fTienQuyetToanDaDuyet) AS fTienQuyetToanDaDuyet,
	--	  SUM(fTienThucChi) AS fTienThucChi,
	--	  SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay
 --         --iID_MaDonVi,
 --         --iID_MucLucNganSach
	--	  --into #tblDataDaCap
	--FROM BH_QTC_Quy_KinhPhiQuanLy_chiTiet
	--WHERE iID_QTC_Quy_KinhPhiQuanLy IN
 --      (
	--	SELECT iID_QTC_Quy_KinhPhiQuanLy
 --       FROM BH_QTC_Quy_KinhPhiQuanLy
 --       WHERE iNamChungTu = 2023
	--	  --AND i = @iID_LoaiDanhMucChi
 --         AND CAST(dNgayChungTu AS DATE) <= CAST('09-20-2023' AS DATE)
	--	  AND EXISTS(SELECT * FROM #tempAgency INTERSECT SELECT * FROM f_split(N'001'))
	--	  AND iID_QTC_Quy_KinhPhiQuanLy='4033EB9C-ECDE-4D24-A41C-FB5B303B5883'
	--	  AND iID_MaDonVi IN (SELECT * FROM  f_split(N'001'))
	--	)
		
	--GROUP BY iID_MucLucNganSach
	SELECT * into #tblDataDuToan FROM #tblPhanBoDuToan

	SELECT sum(fTien_DuToanGiaoNamNay) AS fTien_DuToanGiaoNamNay
	, sum(fTien_DuToanNamTruocChuyenSang) AS fTien_DuToanNamTruocChuyenSang
	, sum(fTien_TongDuToanDuocGiao) fTien_TongDuToanDuocGiao
	, SUM(fTienThucChi) AS fTienThucChi
	,SUM(fTienQuyetToanDaDuyet) AS fTienQuyetToanDaDuyet
	,SUM(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay
	, SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay
	,iID_MaDonVi
	, iID_MucLucNganSach into #tblDataDaCapDuToan FROM (
		SELECT * FROM #tblDataDaCap
		UNION ALL
		SELECT * FROM #tblDataDuToan
		) data
	GROUP BY iID_MaDonVi, iID_MucLucNganSach;
	--select * from  #tblMlnsRoot
	SELECT mlns.iID_MLNS,
		   mlns.iID_MLNS_Cha,
		   mlns.sXauNoiMa,
		   fTien_DuToanGiaoNamNay,
		   fTien_DuToanNamTruocChuyenSang,
		   fTien_TongDuToanDuocGiao,
		   fTienThucChi,
		   fTienQuyetToanDaDuyet,
		   fTienDeNghiQuyetToanQuyNay,
		   fTienXacNhanQuyetToanQuyNay,
		   isnull(mlns.iID_MaDonVi, daCapDuToan.iID_MaDonVi) as iID_MaDonVi
		   INTO #tblDaCapDuToan
	FROM #tblMlnsRoot mlns
	LEFT JOIN
	  #tblDataDaCapDuToan daCapDuToan
	ON mlns.iID_MLNS = daCapDuToan.iID_MucLucNganSach and ((mlns.iID_MaDonVi is not null and mlns.iID_MaDonVi = daCapDuToan.iID_MaDonVi) or mlns.iID_MaDonVi is null)

	SELECT iID_MLNS, iID_MLNS_Cha, iID_MaDonVi, sum(fTien_DuToanGiaoNamNay) AS fTien_DuToanGiaoNamNay, sum(fTien_DuToanNamTruocChuyenSang) AS fTien_DuToanNamTruocChuyenSang, SUM(fTien_TongDuToanDuocGiao) as fTien_TongDuToanDuocGiao,SUM(fTienThucChi) as fTienThucChi,SUM(fTienQuyetToanDaDuyet) AS fTienQuyetToanDaDuyet,SUM(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay,SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay INTO #tblDaCapDuToanResult FROM (
		SELECT distinct T.iID_MLNS,  
			   T.iID_MLNS_Cha,
			   --isnull(T.iID_MaDonVi, T.iID_MaDonVi) as iID_MaDonVi,
			   T.iID_MaDonVi,
			   T.fTien_DuToanGiaoNamNay,
			   T.fTien_DuToanNamTruocChuyenSang,
			   T.fTien_TongDuToanDuocGiao,
			   T.fTienThucChi,
			   T.fTienQuyetToanDaDuyet,
			   T.fTienDeNghiQuyetToanQuyNay,
			   T.fTienXacNhanQuyetToanQuyNay
		FROM #tblDaCapDuToan T 
		WHERE T.fTien_DuToanGiaoNamNay <> 0 OR  T.fTien_DuToanNamTruocChuyenSang <> 0 OR  T.fTien_TongDuToanDuocGiao <> 0 OR  T.fTienThucChi <> 0 OR  T.fTienQuyetToanDaDuyet <> 0 OR  T.fTienDeNghiQuyetToanQuyNay <> 0 OR  T.fTienXacNhanQuyetToanQuyNay <> 0) data
	GROUP BY iID_MLNS, iID_MLNS_Cha, iID_MaDonVi
	OPTION (maxrecursion 0)

	-- Get data
	SELECT
		isnull(ctct.ID_QTC_Quy_KPK_ChiTiet, @iD) AS ID_QTC_Quy_KPK_ChiTiet,
		@VoucherId AS iID_QTC_Quy_KPK,
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
		isnull(daCapDuToan.fTien_DuToanGiaoNamNay, 0) as fTien_DuToanGiaoNamNay,
		isnull(daCapDuToan.fTien_DuToanNamTruocChuyenSang, 0) as fTien_DuToanNamTruocChuyenSang,
		isnull(daCapDuToan.fTien_TongDuToanDuocGiao, 0) as fTien_TongDuToanDuocGiao,
		isnull(daCapDuToan.fTienThucChi, 0) as fTienThucChi,
		isnull(daCapDuToan.fTienQuyetToanDaDuyet, 0) as fTienQuyetToanDaDuyet,
		isnull(daCapDuToan.fTienDeNghiQuyetToanQuyNay, 0) as fTienDeNghiQuyetToanQuyNay,
		isnull(daCapDuToan.fTienXacNhanQuyetToanQuyNay, 0) as fTienXacNhanQuyetToanQuyNay,

		isnull(ctct.dNgayTao, getdate()) AS DNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS DNgaySua,
		ctct.sNguoiTao AS SNguoiTao,
		ctct.sNguoiSua AS SNguoiSua
	FROM #tblMlnsRoot AS mlnsPhanBo
	LEFT JOIN
		(SELECT *
			FROM 
				BH_QTC_Quy_KPK_ChiTiet
			WHERE 
		 		iID_QTC_Quy_KPK = @VoucherId
		) ctct
	ON mlnsPhanBo.iID_MLNS = ctct.iID_MucLucNganSach --and mlnsPhanBo.iID_MaDonVi = ctct.iID_MaDonVi
	LEFT JOIN BH_QTC_Quy_KPK ct ON ctct.iID_QTC_Quy_KPK = ct.ID_QTC_Quy_KPK
	LEFT JOIN #tblDaCapDuToanResult daCapDuToan
	ON mlnsPhanBo.iID_MLNS = daCapDuToan.iID_MLNS and daCapDuToan.iID_MaDonVi LIKE '%' + mlnsPhanBo.iID_MaDonVi + '%' 
	WHERE mlnsPhanBo.sLNS IN (SELECT * FROM #tempLNS)
	ORDER BY mlnsPhanBo.sXauNoiMa, mlnsPhanBo.iID_MaDonVi;
	END
	ELSE 
		BEGIN
	-- lấy dữ liệu đã cấp
	SELECT  
		SUM(fTien_DuToanGiaoNamNay) AS fTien_DuToanGiaoNamNay,
		SUM(fTien_DuToanNamTruocChuyenSang) AS fTien_DuToanNamTruocChuyenSang,
		SUM(fTien_TongDuToanDuocGiao) AS fTien_TongDuToanDuocGiao,
		SUM(fTienThucChi) AS fTienThucChi,
		SUM(fTienQuyetToanDaDuyet) AS fTienQuyetToanDaDuyet,
		SUM(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay,
		SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay,
		CT.iID_MaDonVi,
		CTCT.iID_MucLucNganSach
		into #tblDataDaCapExist
	FROM
	BH_QTC_Quy_KPK CT
	INNER JOIN BH_QTC_Quy_KPK_ChiTiet CTCT
	ON CT.ID_QTC_Quy_KPK=CTCT.iID_QTC_Quy_KPK
	WHERE CT.iNamChungTu = @YearOfWork
		  --AND i = @iID_LoaiDanhMucChi
          AND CAST(CT.dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
		  AND CT.ID_QTC_Quy_KPK=@VoucherId
		  AND CT.iID_MaDonVi IN (SELECT * FROM f_split(@AgencyId))
		  AND CT.iQuyChungTu=@iQuyChungTu

	GROUP BY  CT.iID_MaDonVi,CTCT.iID_MucLucNganSach


	SELECT sum(fTien_DuToanGiaoNamNay) AS fTien_DuToanGiaoNamNay
	, sum(fTien_DuToanNamTruocChuyenSang) AS fTien_DuToanNamTruocChuyenSang
	, SUM(fTien_TongDuToanDuocGiao) AS fTien_TongDuToanDuocGiao
	,SUM(fTienThucChi) AS fTienThucChi
	,SUM(fTienQuyetToanDaDuyet) AS fTienQuyetToanDaDuyet
	,SUM(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay
	,SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay
	, iID_MaDonVi, iID_MucLucNganSach into #tblDataDaCapDuToanExist FROM (
		SELECT * FROM #tblDataDaCapExist
		) data
	GROUP BY iID_MaDonVi, iID_MucLucNganSach;
	--select * from  #tblMlnsRoot
	SELECT mlns.iID_MLNS,
		   mlns.iID_MLNS_Cha,
		   mlns.sXauNoiMa,
		   fTien_DuToanGiaoNamNay,
		   fTien_DuToanNamTruocChuyenSang,
		   fTien_TongDuToanDuocGiao,
		   fTienThucChi,
		   fTienQuyetToanDaDuyet,
		   fTienDeNghiQuyetToanQuyNay,
		   fTienXacNhanQuyetToanQuyNay,
		   isnull(mlns.iID_MaDonVi, daCapDuToan.iID_MaDonVi) as iID_MaDonVi
		   INTO #tblDaCapDuToanExist
	FROM #tblMlnsRoot mlns
	LEFT JOIN
	  #tblDataDaCapDuToanExist daCapDuToan
	ON mlns.iID_MLNS = daCapDuToan.iID_MucLucNganSach and ((mlns.iID_MaDonVi is not null and mlns.iID_MaDonVi = daCapDuToan.iID_MaDonVi) or mlns.iID_MaDonVi is null)

	SELECT iID_MLNS, iID_MLNS_Cha, iID_MaDonVi
	, sum(fTien_DuToanGiaoNamNay) AS fTien_DuToanGiaoNamNay
	, sum(fTien_DuToanNamTruocChuyenSang) AS fTien_DuToanNamTruocChuyenSang
	, SUM(fTien_TongDuToanDuocGiao) as fTien_TongDuToanDuocGiao
	, SUM(fTienThucChi) AS fTienThucChi
	, SUM(fTienQuyetToanDaDuyet) AS fTienQuyetToanDaDuyet
	, SUM(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay
	, SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay INTO #tblDaCapDuToanResultExist FROM (
		SELECT distinct T.iID_MLNS,  
			   T.iID_MLNS_Cha,
			   --isnull(T.iID_MaDonVi, T.iID_MaDonVi) as iID_MaDonVi,
			   T.iID_MaDonVi,
			   T.fTien_DuToanGiaoNamNay,
			   T.fTien_DuToanNamTruocChuyenSang,
			   T.fTien_TongDuToanDuocGiao,
			   T.fTienThucChi,
			   T.fTienQuyetToanDaDuyet,
			   T.fTienDeNghiQuyetToanQuyNay,
			   T.fTienXacNhanQuyetToanQuyNay
		FROM #tblDaCapDuToanExist T 
		WHERE  T.fTien_DuToanGiaoNamNay <> 0 OR T.fTien_DuToanNamTruocChuyenSang<>0 
				OR   T.fTien_TongDuToanDuocGiao<>0 OR T.fTienThucChi <>0 
				OR T.fTienQuyetToanDaDuyet <>0 OR T.fTienDeNghiQuyetToanQuyNay <>0 
				OR T.fTienXacNhanQuyetToanQuyNay<>0) data
	GROUP BY iID_MLNS, iID_MLNS_Cha, iID_MaDonVi
	OPTION (maxrecursion 0)

	-- Get data
	SELECT
		isnull(ctct.ID_QTC_Quy_KPK_ChiTiet, @iD) AS ID_QTC_Quy_KPK_ChiTiet,
		@VoucherId AS iID_QTC_Quy_KPK,
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
		isnull(daCapDuToan.fTien_DuToanGiaoNamNay, 0) as fTien_DuToanGiaoNamNay,
		isnull(daCapDuToan.fTien_DuToanNamTruocChuyenSang, 0) as fTien_DuToanNamTruocChuyenSang,
		isnull(daCapDuToan.fTien_TongDuToanDuocGiao, 0) as fTien_TongDuToanDuocGiao,
		isnull(daCapDuToan.fTienThucChi, 0) as fTienThucChi,
		isnull(daCapDuToan.fTienQuyetToanDaDuyet, 0) as fTienQuyetToanDaDuyet,
		isnull(daCapDuToan.fTienDeNghiQuyetToanQuyNay, 0) as fTienDeNghiQuyetToanQuyNay,
		isnull(daCapDuToan.fTienXacNhanQuyetToanQuyNay, 0) as fTienXacNhanQuyetToanQuyNay,
		isnull(ctct.dNgayTao, getdate()) AS DNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS DNgaySua,
		ctct.sNguoiTao AS SNguoiTao,
		ctct.sNguoiSua AS SNguoiSua
	FROM #tblMlnsRoot AS mlnsPhanBo
	LEFT JOIN
		(SELECT *
			FROM 
				BH_QTC_Quy_KPK_chiTiet
			WHERE 
		 		iID_QTC_Quy_KPK = @VoucherId
		) ctct
	ON mlnsPhanBo.iID_MLNS = ctct.iID_MucLucNganSach --and mlnsPhanBo.iID_MaDonVi = ctct.iID_MaDonVi
	LEFT JOIN BH_QTC_Quy_KPK ct ON ctct.iID_QTC_Quy_KPK = ct.ID_QTC_Quy_KPK
	LEFT JOIN #tblDaCapDuToanResultExist daCapDuToan
	ON mlnsPhanBo.iID_MLNS = daCapDuToan.iID_MLNS and daCapDuToan.iID_MaDonVi LIKE '%' + mlnsPhanBo.iID_MaDonVi + '%' 
	WHERE mlnsPhanBo.sLNS IN (SELECT * FROM #tempLNS)
	ORDER BY mlnsPhanBo.sXauNoiMa, mlnsPhanBo.iID_MaDonVi;
	END
END
;
;
;
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_rpt_get_donvi_nKPQL_lns]    Script Date: 12/28/2023 9:36:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_qtc_rpt_get_donvi_nKPQL_lns]
@NamLamViec int,
@LoaiChungTu int
AS BEGIN
SET NOCOUNT ON;

SELECT 
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

FROM BH_QTC_Nam_KinhPhiQuanLy chungtu
LEFT JOIN DonVi donvi ON chungtu.iID_MaDonVi = donvi.iID_MaDonVi
WHERE chungtu.iNamLamViec = @NamLamViec
  AND chungtu.iID_MaDonVi <> ''
  AND chungtu.iID_MaDonVi IS NOT NULL
  AND donvi.iNamLamViec = @NamLamViec
 -- AND chungtu.iLoaiTongHop=@LoaiChungTu
ORDER BY cast(chungtu.dNgayChungTu AS date) END
;
;

GO
