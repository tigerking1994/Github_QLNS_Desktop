/****** Object:  StoredProcedure [dbo].[sp_qtc_quykinhphi_khac_chungtu_chi_tiet]    Script Date: 4/1/2024 2:18:24 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_quykinhphi_khac_chungtu_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_quykinhphi_khac_chungtu_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_quykinhphi_khac_chungtu_chi_tiet]    Script Date: 4/1/2024 2:18:24 PM ******/
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
	@iQuyChungTu int,
	@loai int
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
	and sLNS IN (SELECT * FROM #tempLNS)
	and iTrangThai=1;

		-- lấy ra dữ liệu dự toán
	SELECT 
		  SUM(fTienTuChi) AS fTien_DuToanGiaoNamNay,
          iID_MaDonVi,
          sXauNoiMa,
		  iID_MucLucNganSach
		  into #tblPhanBoDuToan
   FROM BH_DTC_PhanBoDuToanChi_ChiTiet
   WHERE iID_DTC_PhanBoDuToanChi IN
       (SELECT ID
        FROM BH_DTC_PhanBoDuToanChi
        WHERE sSoQuyetDinh <> ''
          AND sSoQuyetDinh IS NOT NULL
          AND iNamChungTu = @YearOfWork
          --AND iID_LoaiDanhMucChi = @iID_LoaiDanhMucChi
		  AND bIsKhoa=1
          AND cast(dNgayQuyetDinh AS date) <= cast(@VoucherDate AS date))
     AND iID_MaDonVi in  (SELECT * FROM f_split(@AgencyId))-- donvi
   GROUP BY iID_MaDonVi, 
   sXauNoiMa,
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
		--WHERE bHangCha = 0
		--UNION ALL
		--SELECT *, null AS iID_MaDonVi, null AS sTenDonVi  
		--FROM #tblNsMlns 
		--WHERE bHangCha = 1
	) mlns

	-- lay du toan tren giao
	SELECT 
		  SUM(fTienTuChi) AS fTien_DuToanGiaoNamNay,
          iID_MaDonVi,
          iID_MucLucNganSach,
		  sXauNoiMa
		  into #tblNhanPhanBoTrenGiao
   FROM BH_DTC_DuToanChiTrenGiao_ChiTiet
   WHERE iID_DTC_DuToanChiTrenGiao IN
       (SELECT ID
        FROM BH_DTC_DuToanChiTrenGiao
        WHERE sSoQuyetDinh <> ''
          AND sSoQuyetDinh IS NOT NULL
          AND iNamLamViec = @YearOfWork
		  AND bIsKhoa=1
		  )
     AND iID_MaDonVi in  (SELECT * FROM f_split(@AgencyId))-- donvi
   GROUP BY iID_MaDonVi, 
   iID_MucLucNganSach,
   sXauNoiMa
   -- lay du lieu  quy 1
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

	-- lay du lieu da quyet toan 
	SELECT  
		SUM(fTienXacNhanQuyetToanQuyNay) AS fTienQuyetToanDaDuyet,
		CT.iID_MaDonVi,
		CTCT.sXauNoiMa
		into #TemptblTienDaDuyet
	FROM
	BH_QTC_Quy_KPK CT
	INNER JOIN BH_QTC_Quy_KPK_ChiTiet CTCT
	ON CT.ID_QTC_Quy_KPK=CTCT.iID_QTC_Quy_KPK
	WHERE CT.iNamChungTu = @YearOfWork
		  AND CAST(CT.dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
		  AND CT.iID_MaDonVi IN (SELECT * FROM f_split(@AgencyId))
		  --AND CT.bIsKhoa=1
		  AND CT.iQuyChungTu<@iQuyChungTu
	GROUP BY  CT.iID_MaDonVi,CTCT.sXauNoiMa
		

	IF @loai=1				
	-- Get data chi tiet chung tu thuong
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
		mlnsPhanBo.iID_MaDonVi AS iIMaDonVi,
		mlnsPhanBo.sTenDonVi,
		isnull(daCapDuToan.fTien_DuToanGiaoNamNay, 0) as fTien_DuToanGiaoNamNay,
		isnull(ctquytruoc.fTien_DuToanNamTruocChuyenSang, 0) as fTien_DuToanNamTruocChuyenSang,
		isnull(ctct.fTien_TongDuToanDuocGiao, 0) as fTien_TongDuToanDuocGiao,
		isnull(ctct.fTienThucChi, 0) as fTienThucChi,
		isnull(tblDaDuyet.fTienQuyetToanDaDuyet, 0) as fTienQuyetToanDaDuyet,
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
	LEFT JOIN #tblPhanBoDuToan daCapDuToan
	ON mlnsPhanBo.sXauNoiMa = daCapDuToan.sXauNoiMa -- and daCapDuToan.iID_MaDonVi LIKE '%' + mlnsPhanBo.iID_MaDonVi + '%' 
	LEFT JOIN #TempChungTuQuyTruoc ctquytruoc on mlnsPhanBo.sXauNoiMa=ctquytruoc.sXauNoiMa
	LEFT JOIN #TemptblTienDaDuyet tblDaDuyet on mlnsPhanBo.sXauNoiMa=tblDaDuyet.sXauNoiMa
	WHERE mlnsPhanBo.sLNS IN (SELECT * FROM #tempLNS)
	ORDER BY mlnsPhanBo.sXauNoiMa
	else
	-- Get data chi tiet chung tu tong hop
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
		mlnsPhanBo.iID_MaDonVi AS iIMaDonVi,
		mlnsPhanBo.sTenDonVi,
		isnull(daCapDuToan.fTien_DuToanGiaoNamNay, 0) as fTien_DuToanGiaoNamNay,
		isnull(ctquytruoc.fTien_DuToanNamTruocChuyenSang, 0) as fTien_DuToanNamTruocChuyenSang,
		isnull(ctct.fTien_TongDuToanDuocGiao, 0) as fTien_TongDuToanDuocGiao,
		isnull(ctct.fTienThucChi, 0) as fTienThucChi,
		isnull(tblDaDuyet.fTienQuyetToanDaDuyet, 0) as fTienQuyetToanDaDuyet,
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
	LEFT JOIN #tblNhanPhanBoTrenGiao daCapDuToan
	ON mlnsPhanBo.sXauNoiMa = daCapDuToan.sXauNoiMa -- and daCapDuToan.iID_MaDonVi LIKE '%' + mlnsPhanBo.iID_MaDonVi + '%' 
	LEFT JOIN #TempChungTuQuyTruoc ctquytruoc on mlnsPhanBo.sXauNoiMa=ctquytruoc.sXauNoiMa
	LEFT JOIN #TemptblTienDaDuyet tblDaDuyet on mlnsPhanBo.sXauNoiMa=tblDaDuyet.sXauNoiMa
	WHERE mlnsPhanBo.sLNS IN (SELECT * FROM #tempLNS)
	ORDER BY mlnsPhanBo.sXauNoiMa

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
