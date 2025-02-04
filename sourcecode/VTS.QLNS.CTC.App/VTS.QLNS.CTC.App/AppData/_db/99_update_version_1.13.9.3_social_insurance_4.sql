/****** Object:  StoredProcedure [dbo].[sp_qtc_quykinhphi_khac_chungtu_chitiet_tao_tonghop]    Script Date: 1/25/2024 2:59:55 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_quykinhphi_khac_chungtu_chitiet_tao_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_quykinhphi_khac_chungtu_chitiet_tao_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_quykinhphi_khac_chungtu_chi_tiet]    Script Date: 1/25/2024 2:59:55 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_quykinhphi_khac_chungtu_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_quykinhphi_khac_chungtu_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_quykinhphi_quanly_chungtu_chitiet_tao_tonghop]    Script Date: 1/25/2024 3:09:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_quykinhphi_quanly_chungtu_chitiet_tao_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_quykinhphi_quanly_chungtu_chitiet_tao_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqKPQL_create_datafor_quaterbefore]    Script Date: 1/25/2024 3:09:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtcqKPQL_create_datafor_quaterbefore]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtcqKPQL_create_datafor_quaterbefore]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_quykinhphi_khac_chungtu_chi_tiet]    Script Date: 1/25/2024 2:59:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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

	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha,sDuToanChiTietToi
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

	--IF @CountIndex=0
	--BEGIN
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

	SELECT iID_MLNS, iID_MLNS_Cha
	, iID_MaDonVi
	, sum(fTien_DuToanGiaoNamNay) AS fTien_DuToanGiaoNamNay
	, sum(fTien_DuToanNamTruocChuyenSang) AS fTien_DuToanNamTruocChuyenSang
	, SUM(fTien_TongDuToanDuocGiao) as fTien_TongDuToanDuocGiao
	,SUM(fTienThucChi) as fTienThucChi
	,SUM(fTienQuyetToanDaDuyet) AS fTienQuyetToanDaDuyet
	,SUM(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay
	,SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay 
	INTO #tblDaCapDuToanResult FROM (
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

	SELECT CTCT.* 
			INTO #TempChungTuQuyTruoc
			FROM 
				BH_QTC_Quy_KPK_ChiTiet CTCT
			WHERE CTCT.iID_QTC_Quy_KPK IN
					(
						SELECT ID_QTC_Quy_KPK FROM  BH_QTC_Quy_KPK 
						WHERE iID_MaDonVi IN ( SELECT * FROM  f_split(@AgencyId))
									AND iNamChungTu=@YearOfWork
									AND iQuyChungTu=1
					)

		
	if @iQuyChungTu>1
	begin 
			select * into #tblQuyTruoc from #TempChungTuQuyTruoc where ( fTien_DuToanGiaoNamNay =0 or fTien_DuToanGiaoNamNay is null)
			update #tblQuyTruoc set iID_QTC_Quy_KPK=@VoucherId
			
				insert into BH_QTC_Quy_KPK_ChiTiet (ID_QTC_Quy_KPK_ChiTiet
				,iID_QTC_Quy_KPK
				,fTien_DuToanNamTruocChuyenSang
				,fTien_TongDuToanDuocGiao
				,iID_MucLucNganSach
				,sNoiDung
				,iNamLamViec
				,iIDMaDonVi
				,sXauNoiMa)
				select NEWID() as BH_QTC_Quy_KPK_ChiTiet,
						t1.iID_QTC_Quy_KPK,
						t1.fTien_DuToanNamTruocChuyenSang,
						t1.fTien_DuToanNamTruocChuyenSang as fTien_TongDuToanDuocGiao,
						t1.iID_MucLucNganSach,
						t1.sNoiDung,
						t1.iNamLamViec,
						t1.iIDMaDonVi,
						t1.sXauNoiMa
						from #tblQuyTruoc t1 
						where not exists (select 1
												from BH_QTC_Quy_KPK_ChiTiet t2 
												where t2.iID_QTC_Quy_KPK=t1.iID_QTC_Quy_KPK
												and t2.iID_MucLucNganSach=t1.iID_MucLucNganSach
												and t2.iNamLamViec=t1.iNamLamViec
												and t2.iIDMaDonVi=t1.iIDMaDonVi
												)
			
			UPDATE  t2
				SET
				t2.fTien_DuToanNamTruocChuyenSang=t1.fTien_DuToanNamTruocChuyenSang,
				t2.fTien_TongDuToanDuocGiao= t1.fTien_TongDuToanDuocGiao
				FROM #tblQuyTruoc t1
				JOIN 
				BH_QTC_Quy_KPK_ChiTiet t2
				ON t2.iID_QTC_Quy_KPK=t1.iID_QTC_Quy_KPK
					and t2.iID_MucLucNganSach=t1.iID_MucLucNganSach
					and t2.iNamLamViec=t1.iNamLamViec
					and t2.iIDMaDonVi=t1.iIDMaDonVi
	end
					
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
		mlnsPhanBo.sDuToanChiTietToi,
		@YearOfWork AS INamLamViec,
		mlnsPhanBo.iID_MaDonVi AS IID_MaDonVi,
		mlnsPhanBo.iID_MaDonVi AS iIMaDonVi,
		mlnsPhanBo.sTenDonVi AS STenDonVi,
		isnull(daCapDuToan.fTien_DuToanGiaoNamNay, 0) as fTien_DuToanGiaoNamNay,
		isnull(ctquytruoc.fTien_DuToanNamTruocChuyenSang, 0) as fTien_DuToanNamTruocChuyenSang,
		isnull(ctct.fTien_TongDuToanDuocGiao, 0) as fTien_TongDuToanDuocGiao,
		isnull(ctct.fTienThucChi, 0) as fTienThucChi,
		isnull(ctct.fTienQuyetToanDaDuyet, 0) as fTienQuyetToanDaDuyet,
		isnull(ctct.fTienDeNghiQuyetToanQuyNay, 0) as fTienDeNghiQuyetToanQuyNay,
		isnull(ctct.fTienXacNhanQuyetToanQuyNay, 0) as fTienXacNhanQuyetToanQuyNay,

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
	ON mlnsPhanBo.iID_MLNS = daCapDuToan.iID_MLNS -- and daCapDuToan.iID_MaDonVi LIKE '%' + mlnsPhanBo.iID_MaDonVi + '%' 
	LEFT JOIN #TempChungTuQuyTruoc ctquytruoc on ctct.iID_MucLucNganSach=ctquytruoc.iID_MucLucNganSach
	WHERE mlnsPhanBo.sLNS IN (SELECT * FROM #tempLNS)
	ORDER BY mlnsPhanBo.sXauNoiMa, mlnsPhanBo.iID_MaDonVi;
	--END
	--ELSE 
	--	BEGIN
	---- lấy dữ liệu đã cấp
	--SELECT  
	--	SUM(fTien_DuToanGiaoNamNay) AS fTien_DuToanGiaoNamNay,
	--	SUM(fTien_DuToanNamTruocChuyenSang) AS fTien_DuToanNamTruocChuyenSang,
	--	SUM(fTien_TongDuToanDuocGiao) AS fTien_TongDuToanDuocGiao,
	--	SUM(fTienThucChi) AS fTienThucChi,
	--	SUM(fTienQuyetToanDaDuyet) AS fTienQuyetToanDaDuyet,
	--	SUM(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay,
	--	SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay,
	--	CT.iID_MaDonVi,
	--	CTCT.iID_MucLucNganSach
	--	into #tblDataDaCapExist
	--FROM
	--BH_QTC_Quy_KPK CT
	--INNER JOIN BH_QTC_Quy_KPK_ChiTiet CTCT
	--ON CT.ID_QTC_Quy_KPK=CTCT.iID_QTC_Quy_KPK
	--WHERE CT.iNamChungTu = @YearOfWork
	--	  --AND i = @iID_LoaiDanhMucChi
 --         AND CAST(CT.dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
	--	  AND CT.ID_QTC_Quy_KPK=@VoucherId
	--	  AND CT.iID_MaDonVi IN (SELECT * FROM f_split(@AgencyId))
	--	  AND CT.iQuyChungTu=@iQuyChungTu

	--GROUP BY  CT.iID_MaDonVi,CTCT.iID_MucLucNganSach


	--SELECT sum(fTien_DuToanGiaoNamNay) AS fTien_DuToanGiaoNamNay
	--, sum(fTien_DuToanNamTruocChuyenSang) AS fTien_DuToanNamTruocChuyenSang
	--, SUM(fTien_TongDuToanDuocGiao) AS fTien_TongDuToanDuocGiao
	--,SUM(fTienThucChi) AS fTienThucChi
	--,SUM(fTienQuyetToanDaDuyet) AS fTienQuyetToanDaDuyet
	--,SUM(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay
	--,SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay
	--, iID_MaDonVi, iID_MucLucNganSach into #tblDataDaCapDuToanExist FROM (
	--	SELECT * FROM #tblDataDaCapExist
	--	) data
	--GROUP BY iID_MaDonVi, iID_MucLucNganSach;
	----select * from  #tblMlnsRoot
	--SELECT mlns.iID_MLNS,
	--	   mlns.iID_MLNS_Cha,
	--	   mlns.sXauNoiMa,
	--	   fTien_DuToanGiaoNamNay,
	--	   fTien_DuToanNamTruocChuyenSang,
	--	   fTien_TongDuToanDuocGiao,
	--	   fTienThucChi,
	--	   fTienQuyetToanDaDuyet,
	--	   fTienDeNghiQuyetToanQuyNay,
	--	   fTienXacNhanQuyetToanQuyNay,
	--	   isnull(mlns.iID_MaDonVi, daCapDuToan.iID_MaDonVi) as iID_MaDonVi
	--	   INTO #tblDaCapDuToanExist
	--FROM #tblMlnsRoot mlns
	--LEFT JOIN
	--  #tblDataDaCapDuToanExist daCapDuToan
	--ON mlns.iID_MLNS = daCapDuToan.iID_MucLucNganSach and ((mlns.iID_MaDonVi is not null and mlns.iID_MaDonVi = daCapDuToan.iID_MaDonVi) or mlns.iID_MaDonVi is null)

	--SELECT iID_MLNS, iID_MLNS_Cha, iID_MaDonVi
	--, sum(fTien_DuToanGiaoNamNay) AS fTien_DuToanGiaoNamNay
	--, sum(fTien_DuToanNamTruocChuyenSang) AS fTien_DuToanNamTruocChuyenSang
	--, SUM(fTien_TongDuToanDuocGiao) as fTien_TongDuToanDuocGiao
	--, SUM(fTienThucChi) AS fTienThucChi
	--, SUM(fTienQuyetToanDaDuyet) AS fTienQuyetToanDaDuyet
	--, SUM(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay
	--, SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay INTO #tblDaCapDuToanResultExist FROM (
	--	SELECT distinct T.iID_MLNS,  
	--		   T.iID_MLNS_Cha,
	--		   --isnull(T.iID_MaDonVi, T.iID_MaDonVi) as iID_MaDonVi,
	--		   T.iID_MaDonVi,
	--		   T.fTien_DuToanGiaoNamNay,
	--		   T.fTien_DuToanNamTruocChuyenSang,
	--		   T.fTien_TongDuToanDuocGiao,
	--		   T.fTienThucChi,
	--		   T.fTienQuyetToanDaDuyet,
	--		   T.fTienDeNghiQuyetToanQuyNay,
	--		   T.fTienXacNhanQuyetToanQuyNay
	--	FROM #tblDaCapDuToanExist T 
	--	WHERE  T.fTien_DuToanGiaoNamNay <> 0 OR T.fTien_DuToanNamTruocChuyenSang<>0 
	--			OR   T.fTien_TongDuToanDuocGiao<>0 OR T.fTienThucChi <>0 
	--			OR T.fTienQuyetToanDaDuyet <>0 OR T.fTienDeNghiQuyetToanQuyNay <>0 
	--			OR T.fTienXacNhanQuyetToanQuyNay<>0) data
	--GROUP BY iID_MLNS, iID_MLNS_Cha, iID_MaDonVi
	--OPTION (maxrecursion 0)

	---- Get data
	--SELECT
	--	isnull(ctct.ID_QTC_Quy_KPK_ChiTiet, @iD) AS ID_QTC_Quy_KPK_ChiTiet,
	--	@VoucherId AS iID_QTC_Quy_KPK,
	--	mlnsPhanBo.iID_MLNS AS iID_MucLucNganSach,
	--	mlnsPhanBo.iID_MLNS_Cha AS IdParent,
	--	mlnsPhanBo.sXauNoiMa AS sXauNoiMa,
	--	mlnsPhanBo.sLNS AS SLNS,
	--	mlnsPhanBo.sL AS SL,
	--	mlnsPhanBo.sK AS SK,
	--	mlnsPhanBo.sM AS SM,
	--	mlnsPhanBo.sTM AS STM,
	--	mlnsPhanBo.sTTM AS STTM,
	--	mlnsPhanBo.sNG AS SNG,
	--	mlnsPhanBo.sTNG AS STNG,
	--	mlnsPhanBo.sTNG1 AS STNG1,
	--	mlnsPhanBo.sTNG2 AS STNG2,
	--	mlnsPhanBo.sTNG3 AS STNG3,
	--	mlnsPhanBo.sMoTa AS SNoiDung,
	--	mlnsPhanBo.bHangCha As IsHangCha,
	--	mlnsPhanBo.sDuToanChiTietToi,
	--	@YearOfWork AS INamLamViec,
	--	mlnsPhanBo.iID_MaDonVi AS IID_MaDonVi,
	--	mlnsPhanBo.sTenDonVi AS STenDonVi,
	--	isnull(daCapDuToan.fTien_DuToanGiaoNamNay, 0) as fTien_DuToanGiaoNamNay,
	--	isnull(daCapDuToan.fTien_DuToanNamTruocChuyenSang, 0) as fTien_DuToanNamTruocChuyenSang,
	--	isnull(daCapDuToan.fTien_TongDuToanDuocGiao, 0) as fTien_TongDuToanDuocGiao,
	--	isnull(daCapDuToan.fTienThucChi, 0) as fTienThucChi,
	--	isnull(daCapDuToan.fTienQuyetToanDaDuyet, 0) as fTienQuyetToanDaDuyet,
	--	isnull(daCapDuToan.fTienDeNghiQuyetToanQuyNay, 0) as fTienDeNghiQuyetToanQuyNay,
	--	isnull(daCapDuToan.fTienXacNhanQuyetToanQuyNay, 0) as fTienXacNhanQuyetToanQuyNay,
	--	isnull(ctct.dNgayTao, getdate()) AS DNgayTao,
	--	isnull(ctct.dNgaySua, getdate()) AS DNgaySua,
	--	ctct.sNguoiTao AS SNguoiTao,
	--	ctct.sNguoiSua AS SNguoiSua
	--FROM #tblMlnsRoot AS mlnsPhanBo
	--LEFT JOIN
	--	(SELECT *
	--		FROM 
	--			BH_QTC_Quy_KPK_chiTiet
	--		WHERE 
	--	 		iID_QTC_Quy_KPK = @VoucherId
	--	) ctct
	--ON mlnsPhanBo.iID_MLNS = ctct.iID_MucLucNganSach --and mlnsPhanBo.iID_MaDonVi = ctct.iID_MaDonVi
	--LEFT JOIN BH_QTC_Quy_KPK ct ON ctct.iID_QTC_Quy_KPK = ct.ID_QTC_Quy_KPK
	--LEFT JOIN #tblDaCapDuToanResultExist daCapDuToan
	--ON mlnsPhanBo.iID_MLNS = daCapDuToan.iID_MLNS --and daCapDuToan.iID_MaDonVi LIKE '%' + mlnsPhanBo.iID_MaDonVi + '%' 
	--WHERE mlnsPhanBo.sLNS IN (SELECT * FROM #tempLNS)
	--ORDER BY mlnsPhanBo.sXauNoiMa, mlnsPhanBo.iID_MaDonVi;
	--END
END
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_quykinhphi_khac_chungtu_chitiet_tao_tonghop]    Script Date: 1/25/2024 2:59:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  CREATE PROCEDURE [dbo].[sp_qtc_quykinhphi_khac_chungtu_chitiet_tao_tonghop] 
  @ListIdChungTuTongHop ntext, 
  @Nguoitao nvarchar(50),
  @IdChungTu nvarchar(100), 
  @NamLamViec int,
  @SMaDonVi nvarchar(50)
  AS BEGIN 
  INSERT INTO [dbo].BH_QTC_Quy_KPK_ChiTiet (
    ID_QTC_Quy_KPK_ChiTiet,
	iID_QTC_Quy_KPK, 
    iID_MucLucNganSach, 
    sNoiDung, 
    fTien_DuToanNamTruocChuyenSang,
	fTien_DuToanGiaoNamNay, 
    fTien_TongDuToanDuocGiao,
	fTienThucChi, 
    fTienQuyetToanDaDuyet,
	fTienDeNghiQuyetToanQuyNay,
	fTienXacNhanQuyetToanQuyNay,
	dNgaySua,
	dNgayTao, 
    sNguoiSua,
	sNguoiTao,
	sXauNoiMa,
	iNamLamViec,
	iIDMaDonVi
  ) 
SELECT 
DISTINCT
  NEWID(), 
  @idChungTu, 
  iID_MucLucNganSach, 
  sNoiDung, 
  sum(fTien_DuToanNamTruocChuyenSang), 
  sum(fTien_DuToanGiaoNamNay), 
  sum(fTien_TongDuToanDuocGiao), 
  sum(fTienThucChi), 
  sum(fTienQuyetToanDaDuyet), 
  sum(fTienDeNghiQuyetToanQuyNay), 
  sum(fTienXacNhanQuyetToanQuyNay), 
  Null, 
  GETDATE(), 
  Null, 
  @Nguoitao,
  sXauNoiMa,
  iNamLamViec,
  @SMaDonVi
FROM 
  BH_QTC_Quy_KPK_ChiTiet 
WHERE 
  iID_QTC_Quy_KPK in (
    SELECT 
      * 
    FROM 
      f_split(@ListIdChungTuTongHop)
  ) 
GROUP BY 
  iID_MucLucNganSach, 
  sNoiDung,
  sXauNoiMa,
  iNamLamViec;
--danh dau chung tu da tong hop
update 
  BH_QTC_Quy_KPK 
set 
  iLoaiTongHop = 2 
where 
  ID_QTC_Quy_KPK in (
    SELECT 
      * 
    FROM 
      f_split(@ListIdChungTuTongHop)
  ) 
  
END;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqKPQL_create_datafor_quaterbefore]    Script Date: 1/25/2024 3:09:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtcqKPQL_create_datafor_quaterbefore]
@IdChungTu nvarchar(MAX),
@Username nvarchar(500),
@NamLamViec int,
@Quy int,
@MaDonVi nvarchar(MAX)
AS
BEGIN
SET NOCOUNT ON;

	INSERT INTO BH_QTC_Quy_KinhPhiQuanLy_ChiTiet
	( 
		ID_QTC_Quy_KinhPhiQuanLy_ChiTiet,
		iID_QTC_Quy_KinhPhiQuanLy,
		iID_MucLucNganSach,
		sNoiDung,
		dNgayTao,
		sNguoiTao,
		fTienQuyetToanDaDuyet,
		sXauNoiMa,
		iNamLamViec,
		iIDMaDonVi
	)
	SELECT 
		NEWID(),
		@IdChungTu,
		qtcn_ct.iID_MucLucNganSach,
		qtcn_ct.sNoiDung,
		GETDATE(),
		@Username,
		SUM(qtcn_ct.fTienXacNhanQuyetToanQuyNay),
		qtcn_ct.sXauNoiMa,
		qtcn_ct.iNamLamViec,
		qtcn_ct.iIDMaDonVi
	FROM BH_QTC_Quy_KinhPhiQuanLy_ChiTiet qtcn_ct
		inner join BH_QTC_Quy_KinhPhiQuanLy  as qtcn on qtcn_ct.iID_QTC_Quy_KinhPhiQuanLy = qtcn.ID_QTC_Quy_KinhPhiQuanLy
	WHERE qtcn.iQuyChungTu < @Quy
			AND qtcn.iID_MaDonVi = @MaDonVi
			AND qtcn.iNamChungTu = @NamLamViec
			--AND qtcn.bIsKhoa=1
		GROUP BY qtcn_ct.iID_MucLucNganSach, qtcn_ct.sNoiDung,qtcn_ct.sXauNoiMa,
		qtcn_ct.iNamLamViec,
		qtcn_ct.iIDMaDonVi
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_quykinhphi_quanly_chungtu_chitiet_tao_tonghop]    Script Date: 1/25/2024 3:09:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_qtc_quykinhphi_quanly_chungtu_chitiet_tao_tonghop] 
  @ListIdChungTuTongHop ntext, 
  @Nguoitao nvarchar(50),
  @IdChungTu nvarchar(100), 
  @NamLamViec int,
  @SMaDonVi  nvarchar(50)
  AS BEGIN 
  INSERT INTO [dbo].[BH_QTC_Quy_KinhPhiQuanLy_ChiTiet] (
    ID_QTC_Quy_KinhPhiQuanLy_ChiTiet,
	iID_QTC_Quy_KinhPhiQuanLy, 
    iID_MucLucNganSach, sM, 
    sTM, sNoiDung, 
    fTienDuToanDuocGiao,
	fTienThucChi, 
    fTienQuyetToanDaDuyet,
	fTienDeNghiQuyetToanQuyNay, 
    fTienXacNhanQuyetToanQuyNay,
	dNgaySua,
	dNgayTao, 
    sNguoiSua,
	sNguoiTao,
	sXauNoiMa,
	iNamLamViec,
	iIDMaDonVi
  ) 
SELECT 
DISTINCT
  NEWID(), 
  @idChungTu, 
  iID_MucLucNganSach, 
  sM,
  sTM,
  sNoiDung, 
  sum(fTienDuToanDuocGiao), 
  sum(fTienThucChi), 
  sum(fTienQuyetToanDaDuyet), 
  sum(fTienDeNghiQuyetToanQuyNay), 
  sum(fTienXacNhanQuyetToanQuyNay), 
  Null, 
  GETDATE(), 
  Null, 
  @Nguoitao,
  sXauNoiMa,
  iNamLamViec,
  @SMaDonVi
FROM 
  BH_QTC_Quy_KinhPhiQuanLy_ChiTiet 
WHERE 
  iID_QTC_Quy_KinhPhiQuanLy in (
    SELECT 
      * 
    FROM 
      f_split(@ListIdChungTuTongHop)
  ) 
GROUP BY 
  iID_MucLucNganSach, 
  sM,
  sTM,
  sNoiDung,sXauNoiMa,
  iNamLamViec
  ;
--danh dau chung tu da tong hop
update 
  BH_QTC_Quy_KinhPhiQuanLy 
set 
  iLoaiTongHop = 2 
where 
  ID_QTC_Quy_KinhPhiQuanLy in (
    SELECT 
      * 
    FROM 
      f_split(@ListIdChungTuTongHop)
  ) 
  
END;
;
GO
