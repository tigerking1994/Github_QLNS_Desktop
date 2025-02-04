/****** Object:  StoredProcedure [dbo].[sp_rpt_dtc_dieuchinh_chitiet_donvi]    Script Date: 12/11/2024 11:18:50 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dtc_dieuchinh_chitiet_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dtc_dieuchinh_chitiet_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dtc_dieuchinh_chitiet_donvi]    Script Date: 12/11/2024 11:18:50 AM ******/
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
	WHERE ML.sLNS IN  (SELECT * FROM f_split(@SLNS))
			AND ML.iNamLamViec=@NamLamViec
			AND ML.bHangCha IS NOT NULL
			AND Ml.iTrangThai=1
	ORDER BY sXauNoiMa

				
	SELECT 
		distinct
		ct.dNgayChungTu
		, ct.iID_MaDonVi as  iID_MaDonVi
		into #tempTblNgayChungTuDonVi
	FROM BH_DTC_DieuChinhDuToanChi ct
	left join  BH_DTC_DieuChinhDuToanChi_ChiTiet ctct on ct.iID_BH_DTC=ctct.iID_BH_DTC
	where ctct.iNamLamViec=@NamLamViec
		and ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
		and ct.iID_LoaiCap=@IDLoaiChi

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
		ISNULL((dt.fTienTuChi),0)/ @Dvt fTienDuToanDuocGiao,
		ISNULL(SUM(ct.fTienThucHien06ThangDauNam),0)/ @Dvt fTienThucHien06ThangDauNam,
		ISNULL(SUM(ct.fTienUocThucHien06ThangCuoiNam),0)/ @Dvt fTienUocThucHien06ThangCuoiNam,
		ISNULL(SUM(ct.fTienUocThucHienCaNam),0) /@Dvt fTienUocThucHienCaNam,
		ISNULL(SUM(ct.fTienSoSanhTang),0)/ @Dvt fTienSoSanhTang,
		ISNULL(SUM(dt.fTienTuChi) - (SUM(ct.fTienUocThucHienCaNam)),0)/ @Dvt fTienSoSanhGiam,
		ml.bHangChaDuToan,
		ml.bHangChaDuToanDieuChinh

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
				bh.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
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
			LEFT JOIN #tempTblNgayChungTuDonVi ngayChungTuDonVi ON  ctct.iID_MaDonVi=ngayChungTuDonVi.iID_MaDonVi
				WHERE ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
				AND BIsKhoa = 1
				AND ct.iNamChungTu = @NamLamViec
				And cast(ct.DngayChungTu as date) <= cast(ngayChungTuDonVi.dNgayChungTu as date)
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
		ml.sDuToanChiTietToi,
		dt.fTienTuChi,
		ml.bHangChaDuToan,
		ml.bHangChaDuToanDieuChinh
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
		ISNULL((dt.fTienTuChi),0)/ @Dvt fTienDuToanDuocGiao,
		ISNULL(SUM(ct.fTienThucHien06ThangDauNam),0)/ @Dvt fTienThucHien06ThangDauNam,
		ISNULL(SUM(ct.fTienUocThucHien06ThangCuoiNam),0)/ @Dvt fTienUocThucHien06ThangCuoiNam,
		ISNULL(SUM(ct.fTienUocThucHienCaNam),0) /@Dvt fTienUocThucHienCaNam,
		ISNULL(SUM(ct.fTienSoSanhTang),0)/ @Dvt fTienSoSanhTang,
		ISNULL(SUM(dt.fTienTuChi) - (SUM(ct.fTienUocThucHienCaNam)),0)/ @Dvt fTienSoSanhGiam,
		ml.bHangChaDuToan,
		ml.bHangChaDuToanDieuChinh

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
				bh.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
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
							LEFT JOIN #tempTblNgayChungTuDonVi ngayChungTuDonVi ON  ctct.iID_MaDonVi=ngayChungTuDonVi.iID_MaDonVi
							WHERE ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
							AND BIsKhoa = 1
							And cast(ct.DngayChungTu as date) <= cast(ngayChungTuDonVi.dNgayChungTu as date)
							--AND ct.iID_LoaiDanhMucChi=@IDLoaiChi
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
		ml.sDuToanChiTietToi,
		dt.fTienTuChi,
		ml.bHangChaDuToan,
		ml.bHangChaDuToanDieuChinh
		ORDER BY sXauNoiMa;

		DROP TABLE #tblml
		drop table #tempTblNgayChungTuDonVi

END
;
;
;
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkcb_tonghopchi]    Script Date: 12/12/2024 5:20:32 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qtc_qkcb_tonghopchi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qtc_qkcb_tonghopchi]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_get_donvi_qkcb]    Script Date: 12/12/2024 5:20:32 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_get_donvi_qkcb]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_get_donvi_qkcb]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chiquyKCB_chitiet_clone]    Script Date: 12/12/2024 5:20:32 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_chiquyKCB_chitiet_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_chiquyKCB_chitiet_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chiquyKCB_chitiet_clone]    Script Date: 12/12/2024 5:20:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_quyet_toan_chiquyKCB_chitiet_clone]
	@IdChungTu uniqueidentifier,
	@IDLoaiChi uniqueidentifier,
	@SLNS nvarchar(max),
	@SMaLoaiChi nvarchar(50),
	@IIdMaDonVi nvarchar(500),
	@DNgayChungTu datetime,
	@iQuyChungTu int,
	@INamLamViec int,
	@Loai int
AS
BEGIN
	DECLARE @iD uniqueidentifier='00000000-0000-0000-0000-000000000000';
	DECLARE @CountIndex INT;
	SELECT * into #tempLNS from f_split(@SLNS);
	SELECT * into #tempAgency from  f_split(@IIdMaDonVi);
	SELECT @CountIndex = COUNT(*) FROM 
									BH_QTC_QUY_KCB_Chitiet 
									WHERE iID_QTC_Quy_KCB =@IdChungTu
	DECLARE @fSoThamDinh INT;
	SELECT @fSoThamDinh = fSoThamDinh
	FROM BH_ThamDinhQuyetToan_ChungTuChiTiet 
	WHERE iNamLamViec = @INamLamViec - 1 and iMa = 225  and iID_MaDonVi IN (SELECT * FROM f_split(@IIdMaDonVi))

	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha,sDuToanChiTietToi,bHangChaDuToan
			into #tblNsMlns
	FROM BH_DM_MucLucNganSach 
	where iNamLamViec = @INamLamViec 
	and iTrangThai=1
		-- lấy ra dữ liệu dự toán
	SELECT 
		  SUM(fTienTuChi) AS fTien_DuToanGiaoNamNay,
          iID_MaDonVi,
          iID_MucLucNganSach,
		  sXauNoiMa
		  into #tblPhanBoDuToan
   FROM BH_DTC_PhanBoDuToanChi_ChiTiet
   WHERE iID_DTC_PhanBoDuToanChi IN
       (SELECT ID
        FROM BH_DTC_PhanBoDuToanChi
        WHERE sSoQuyetDinh <> ''
          AND sSoQuyetDinh IS NOT NULL
          AND iNamChungTu = @INamLamViec
 
		  AND bIsKhoa=1
          AND cast(dNgayQuyetDinh AS date) <= cast(@DNgayChungTu AS date))
     AND iID_MaDonVi in  (SELECT * FROM f_split(@IIdMaDonVi))-- donvi
   GROUP BY iID_MaDonVi, 
   iID_MucLucNganSach,
   sXauNoiMa

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
          AND iNamLamViec = @INamLamViec
		  AND bIsKhoa=1
		  )
     AND iID_MaDonVi in  (SELECT * FROM f_split(@IIdMaDonVi))-- donvi
   GROUP BY iID_MaDonVi, 
   iID_MucLucNganSach,
   sXauNoiMa


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
		INamLamViec = @INamLamViec 
		AND iID_MaDonVi IN (SELECT * FROM #tempAgency)

	-- Tao bang tam luu chu mlng con
	SELECT * INTO #tblMlnsByPhanCapExistDonVi
	FROM #tblMlnsByPhanCap tbl, #tblDonVi dv
	WHERE  tbl.bHangCha = 0 OR (tbl.bHangCha = 1 and (IsNull(tbl.sM, '') != '' OR tbl.sM <> '' OR IsNull(tbl.sTM, '') != '')  )
	AND tbl.sTM !=''  


	-- Tao bang tam luu chu mlng cha
	SELECT *, '' AS iID_MaDonVi, '' AS sTenDonVi  INTO #tblMlnsByPhanCapNotDonVi
	FROM #tblMlnsByPhanCap 
		WHERE bHangCha = 1 AND ( (IsNull(sM, '') = '' or sM = '')) AND ( (IsNull(sTM, '') = '' or sTM = ''))
		OR ((ISNULL(sTM,'')='' OR sTM='')) OR sTM='0'

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

	SELECT CTCT.* 
		INTO #TempChungTuQuyTruoc
	FROM BH_QTC_QUY_KCB_Chitiet CTCT
		WHERE CTCT.iID_QTC_Quy_KCB IN
	(
		SELECT ID_QTC_Quy_KCB FROM  BH_QTC_QUY_KCB 
		WHERE iID_MaDonVi IN ( SELECT * FROM  f_split(@IIdMaDonVi))
					AND iNamChungTu=@INamLamViec
					AND iQuyChungTu=1
	)

	-- lay du lieu da quyet toan 
	SELECT  
		SUM(FTienDeNghiQuyetToanQuyNay) AS fTienQuyetToanDaDuyet,
		CT.iID_MaDonVi,
		CTCT.sXauNoiMa
		into #TemptblTienDaDuyet
	FROM
	BH_QTC_QUY_KCB CT
	INNER JOIN BH_QTC_QUY_KCB_Chitiet CTCT
	ON CT.ID_QTC_Quy_KCB=CTCT.iID_QTC_Quy_KCB
	WHERE CT.iNamChungTu = @INamLamViec
		  --AND CAST(CT.dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
		  AND CT.iID_MaDonVi IN (SELECT * FROM f_split(@IIdMaDonVi))
		  --AND CT.bIsKhoa=1
		  AND CT.iQuyChungTu<@iQuyChungTu
	GROUP BY  CT.iID_MaDonVi,CTCT.sXauNoiMa

	--- chung tu thuong
	if 	@Loai=1	
	-- Get data
	SELECT
		isnull(ctct.ID_QTC_QUY_KCB_Chitiet, @iD) AS ID_QTC_QUY_KCB_Chitiet,
		@IdChungTu AS iID_QTC_QUY_KCB,
		mlnsPhanBo.iID_MLNS,
		mlnsPhanBo.iID_MLNS_Cha,
		mlnsPhanBo.iID_MLNS as iID_MucLucNganSach,
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
		mlnsPhanBo.bHangCha ,
		mlnsPhanBo.sDuToanChiTietToi,
		@INamLamViec AS INamLamViec,
		mlnsPhanBo.iID_MaDonVi AS IID_MaDonVi,
		mlnsPhanBo.sTenDonVi AS STenDonVi,
		mlnsPhanBo.bHangChaDuToan,
		CASE WHEN mlnsPhanBo.sXauNoiMa = @SLNS THEN @fSoThamDinh ELSE 0 END as FTienDuToanNamTruocChuyenSang,
		isnull(daCapDuToan.fTien_DuToanGiaoNamNay, 0) as FTienDuToanGiaoNamNay,
		isnull(ctct.fTien_TongDuToanDuocGiao, 0) as FTienTongDuToanDuocGiao,
		isnull(ctct.fTienThucChi, 0) as FTienThucChi,
		isnull(tblDaDuyet.fTienQuyetToanDaDuyet, 0) as FTienQuyetToanDaDuyet,
		isnull(ctct.fTienDeNghiQuyetToanQuyNay, 0) as FTienDeNghiQuyetToanQuyNay,
		isnull(ctct.fTienXacNhanQuyetToanQuyNay, 0) as FTienXacNhanQuyetToanQuyNay,
		isnull(ctct.dNgayTao, getdate()) AS DNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS DNgaySua,
		ctct.sNguoiTao AS SNguoiTao,
		ctct.sNguoiSua AS SNguoiSua,
		ctct.sGhiChu
	FROM #tblMlnsRoot AS mlnsPhanBo
	LEFT JOIN
		(SELECT *
			FROM 
				BH_QTC_QUY_KCB_Chitiet
			WHERE 
		 		iID_QTC_QUY_KCB = @IdChungTu
		) ctct
	ON mlnsPhanBo.iID_MLNS = ctct.iID_MucLucNganSach --and mlnsPhanBo.iID_MaDonVi = ctct.iID_MaDonVi
	LEFT JOIN BH_QTC_QUY_KCB ct ON ctct.iID_QTC_QUY_KCB = ct.ID_QTC_QUY_KCB
	LEFT JOIN #tblPhanBoDuToan daCapDuToan
	ON mlnsPhanBo.sXauNoiMa = daCapDuToan.sXauNoiMa
	LEFT JOIN #TempChungTuQuyTruoc tblQuyTruoc on mlnsPhanBo.sXauNoiMa=tblQuyTruoc.sXauNoiMa
	LEFT JOIN #TemptblTienDaDuyet tblDaDuyet on mlnsPhanBo.sXauNoiMa=tblDaDuyet.sXauNoiMa
	WHERE mlnsPhanBo.sLNS IN (SELECT * FROM #tempLNS)
	ORDER BY mlnsPhanBo.sXauNoiMa, mlnsPhanBo.iID_MaDonVi;
	------ Chứng từ tong hop
	ELSE 
	---- Get data
	SELECT
		isnull(ctct.ID_QTC_QUY_KCB_Chitiet, @iD) AS ID_QTC_QUY_KCB_Chitiet,
		@IdChungTu AS iID_QTC_QUY_KCB,
		mlnsPhanBo.iID_MLNS ,
		mlnsPhanBo.iID_MLNS_Cha ,
		mlnsPhanBo.iID_MLNS as iID_MucLucNganSach,
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
		mlnsPhanBo.bHangCha ,
		mlnsPhanBo.sDuToanChiTietToi,
		@INamLamViec AS INamLamViec,
		mlnsPhanBo.iID_MaDonVi AS IID_MaDonVi,
		mlnsPhanBo.sTenDonVi AS STenDonVi,
		mlnsPhanBo.bHangChaDuToan,
		CASE WHEN mlnsPhanBo.sXauNoiMa = @SLNS THEN @fSoThamDinh ELSE 0 END as FTienDuToanNamTruocChuyenSang,
		isnull(daCapDuToan.fTien_DuToanGiaoNamNay, 0) as FTienDuToanGiaoNamNay,
		isnull(ctct.fTien_TongDuToanDuocGiao, 0) as FTienTongDuToanDuocGiao,
		isnull(ctct.fTienThucChi, 0) as FTienThucChi,
		isnull(tblDaDuyet.fTienQuyetToanDaDuyet, 0) as FTienQuyetToanDaDuyet,
		isnull(ctct.fTienDeNghiQuyetToanQuyNay, 0) as FTienDeNghiQuyetToanQuyNay,
		isnull(ctct.fTienXacNhanQuyetToanQuyNay, 0) as FTienXacNhanQuyetToanQuyNay,
		isnull(ctct.dNgayTao, getdate()) AS DNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS DNgaySua,
		ctct.sNguoiTao AS SNguoiTao,
		ctct.sNguoiSua AS SNguoiSua,
		ctct.sGhiChu
	FROM #tblMlnsRoot AS mlnsPhanBo
	LEFT JOIN
		(SELECT *
			FROM 
				BH_QTC_QUY_KCB_Chitiet
			WHERE 
		 		iID_QTC_QUY_KCB = @IdChungTu
		) ctct
	ON mlnsPhanBo.iID_MLNS = ctct.iID_MucLucNganSach --and mlnsPhanBo.iID_MaDonVi = ctct.iID_MaDonVi
	LEFT JOIN BH_QTC_QUY_KCB ct ON ctct.iID_QTC_QUY_KCB = ct.ID_QTC_QUY_KCB
	LEFT JOIN #tblNhanPhanBoTrenGiao daCapDuToan
	ON mlnsPhanBo.sXauNoiMa = daCapDuToan.sXauNoiMa 
	LEFT JOIN #TempChungTuQuyTruoc tblQuyTruoc on mlnsPhanBo.sXauNoiMa=tblQuyTruoc.sXauNoiMa
	LEFT JOIN #TemptblTienDaDuyet tblDaDuyet on mlnsPhanBo.sXauNoiMa=tblDaDuyet.sXauNoiMa
	WHERE mlnsPhanBo.sLNS IN (SELECT * FROM #tempLNS)
	ORDER BY mlnsPhanBo.sXauNoiMa, mlnsPhanBo.iID_MaDonVi;
	--END
END
;
;
;
;
;
;
;
;
;;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_get_donvi_qkcb]    Script Date: 12/12/2024 5:20:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[sp_qt_rpt_get_donvi_qkcb]
@NamLamViec int,
@Quy int

AS BEGIN
SET NOCOUNT ON;

Select distinct  
	dv.iID_DonVi AS Id,
	dv.iID_MaDonVi as IIDMaDonVi,
	dv.sTenDonVi as TenDonVi,
	dv.sKyHieu as KyHieu,
	dv.sMoTa as MoTa,
	dv.iLoai as Loai,
	dv.iNamLamViec as NamLamViec,
	dv.iTrangThai as iTrangThai,
	dv.dNgayTao as DNgayTao,
	dv.sNguoiTao as SNguoiTao,
	dv.dNgaySua as DNgaySua,
	dv.dNgaySua as SNguoiSua,
	dv.*
from DonVi dv
left join BH_QTC_Quy_KCB_ChiTiet ctct on dv.iID_MaDonVi=ctct.iID_MaDonVi
where dv.iNamLamViec=@NamLamViec
and dv.iTrangThai=1

and ctct.iID_QTC_Quy_KCB in (select ID_QTC_Quy_KCB from BH_QTC_Quy_KCB
							where iNamChungTu=@NamLamViec
								and iQuyChungTu=@Quy)
and ctct.FTienDeNghiQuyetToanQuyNay >0
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkcb_tonghopchi]    Script Date: 12/12/2024 5:20:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_qtc_qkcb_tonghopchi]
@YearOfWork int,
@DonViTinh int,
@AgencyId nvarchar(max),
@Quy int

AS BEGIN
SET NOCOUNT ON;
		Select 
		 sum(case when ctct.sXauNoiMa like '9010004-010-011-0001-0000' or 
		 ctct.sXauNoiMa like '9010004-010-011-0002-0000'
		 then (ctct.FTienDeNghiQuyetToanQuyNay) else 0 end) thuoc
		 , sum(case when ctct.sXauNoiMa like '9010004-010-011-0001-0001' or 
		 ctct.sXauNoiMa like '9010004-010-011-0002-0001'
		 then (ctct.FTienDeNghiQuyetToanQuyNay) else 0 end)  vtyt
		 , sum(case when ctct.sXauNoiMa like '9010004-010-011-0001-0002' or 
		 ctct.sXauNoiMa like '9010004-010-011-0002-0002'
		 then (ctct.FTienDeNghiQuyetToanQuyNay) else 0 end)  DVKT
		 , sum(case when ctct.sXauNoiMa like '9010004-010-011-0001-0003' or 
		 ctct.sXauNoiMa like '9010004-010-011-0002-0003'
		 then (ctct.FTienDeNghiQuyetToanQuyNay) else 0 end)  dcyt
		 ,(ml.sLNS +'-' + ml.sL+'-'+ml.sK+'-'+ml.sM+'-'+ml.sTM) sXauNoiMa
		 , dv.sTenDonVi
		 , dv.iID_MaDonVi
		 into #tempData
		from BH_QTC_Quy_KCB_ChiTiet ctct 
		left join DonVi dv on dv.iID_MaDonVi=ctct.iID_MaDonVi
		left join BH_DM_MucLucNganSach ml on ctct.sXauNoiMa=ml.sXauNoiMa
		where dv.iNamLamViec=@YearOfWork
		and dv.iTrangThai=1

		and ctct.iID_QTC_Quy_KCB in (select ID_QTC_Quy_KCB from BH_QTC_Quy_KCB
									where iNamChungTu=@YearOfWork
										and iQuyChungTu=@Quy)
		and ctct.FTienDeNghiQuyetToanQuyNay >0

		and ml.iNamLamViec=@YearOfWork
		and ml.iTrangThai=1
		group by ctct.sXauNoiMa,ml.sLNS,ml.sL,ml.sK,ml.sM,ml.sTM,dv.iID_MaDonVi,dv.sTenDonVi

		-- Get ghi chu
		select 
		( (case when ctct.sXauNoiMa = '9010004-010-011-0001-0000' then ctct.sGhiChu 
		 else ' ' end ) + 
		(case when ctct.sXauNoiMa = '9010004-010-011-0001-0001' then ctct.sGhiChu 
		else ' ' end)  + 
		(case when ctct.sXauNoiMa = '9010004-010-011-0001-0002' then ctct.sGhiChu 
		else ' ' end) +
		(case when ctct.sXauNoiMa = '9010004-010-011-0001-0003' then ctct.sGhiChu 
		else '' end ) ) a
		,((case when ctct.sXauNoiMa = '9010004-010-011-0002-0000' then ctct.sGhiChu 
		else ' ' end) 
		+(case when ctct.sXauNoiMa = '9010004-010-011-0002-0001' then ctct.sGhiChu 
		else  ' ' end) 
		+(case when ctct.sXauNoiMa = '9010004-010-011-0002-0002' then ctct.sGhiChu 
		else ' ' end) 
		+(case when ctct.sXauNoiMa = '9010004-010-011-0002-0003' then ctct.sGhiChu 
		else ' ' end) )b
		 , dv.sTenDonVi
		 , dv.iID_MaDonVi
		 --, case when ctct.sXauNoiMa = '9010004-010-011-0001-0000' or  ctct.sXauNoiMa = '9010004-010-011-0002-0000' then 1 else 0 end isThuoc
		 --, case when ctct.sXauNoiMa = '9010004-010-011-0001-0001' or  ctct.sXauNoiMa = '9010004-010-011-0002-0001' then 2 else 0 end isVTYT
		 --, case when ctct.sXauNoiMa = '9010004-010-011-0001-0002' or  ctct.sXauNoiMa = '9010004-010-011-0002-0002' then 3 else 0 end isDVKT
		 --, case when ctct.sXauNoiMa = '9010004-010-011-0001-0003' or  ctct.sXauNoiMa = '9010004-010-011-0002-0003' then 4 else 0 end isDCYT
		 --, 0 isUpdate
		 into #tempGhiChu
		 from BH_QTC_Quy_KCB_ChiTiet ctct 
		left join DonVi dv on dv.iID_MaDonVi=ctct.iID_MaDonVi
		left join BH_DM_MucLucNganSach ml on ctct.sXauNoiMa=ml.sXauNoiMa
		where dv.iNamLamViec=@YearOfWork
		and dv.iTrangThai=1

		and ctct.iID_QTC_Quy_KCB in (select ID_QTC_Quy_KCB from BH_QTC_Quy_KCB
									where iNamChungTu=@YearOfWork
										and iQuyChungTu=@Quy)
		and ctct.FTienDeNghiQuyetToanQuyNay >0

		and ml.iNamLamViec=@YearOfWork
		and ml.iTrangThai=1

		group by ctct.sXauNoiMa,ctct.sGhiChu,dv.iID_MaDonVi,dv.sTenDonVi


		SELECT 
		sTenDonVi,
		iID_MaDonVi,
		Replace(STUFF((
			SELECT ', ' + a
			FROM #tempGhiChu T2
			WHERE T2.sTenDonVi = T1.sTenDonVi
			  AND T2.iID_MaDonVi = T1.iID_MaDonVi
			  AND (a IS NOT NULL or a <> ' ')
			FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, ''),'    ,','') AS SGhiChu1,
		Replace(STUFF((
			SELECT ', ' + b
			FROM #tempGhiChu T2
			WHERE T2.sTenDonVi = T1.sTenDonVi
			  AND T2.iID_MaDonVi = T1.iID_MaDonVi
			  AND (b IS NOT NULL or b <> ' ')
			FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, ''),'    ,','') AS SGhiChu2
			into #tempDataGhiChu
		FROM #tempGhiChu T1 
		GROUP BY sTenDonVi, iID_MaDonVi;

		select 
		 Round(sum(case when sXauNoiMa like '9010004-010-011-0001-0000' or 
		 sXauNoiMa like '9010004-010-011-0002-0000'
		 then (thuoc) else 0 end)/ @DonViTinh,0) fTienThuoc
		 , Round(sum(case when sXauNoiMa like '9010004-010-011-0001-0001' or 
		 sXauNoiMa like '9010004-010-011-0002-0001'
		 then (vtyt) else 0 end)/ @DonViTinh,0) fTienVTYT
		 , Round(sum(case when sXauNoiMa like '9010004-010-011-0001-0002' or 
		 sXauNoiMa like '9010004-010-011-0002-0002'
		 then (DVKT) else 0 end)/ @DonViTinh,0) fTienDVKT
		 , Round(sum(case when sXauNoiMa like '9010004-010-011-0001-0003' or 
		 sXauNoiMa like '9010004-010-011-0002-0003'
		 then (dcyt) else 0 end)/ @DonViTinh,0) fTienDCYT
		 , Round((sum(case when sXauNoiMa like '9010004-010-011-0001-0000' or 
		 sXauNoiMa like '9010004-010-011-0002-0000'
		 then (thuoc) else 0 end) 
		 + sum(case when sXauNoiMa like '9010004-010-011-0001-0001' or 
		 sXauNoiMa like '9010004-010-011-0002-0001'
		 then (vtyt) else 0 end) 
		 + sum(case when sXauNoiMa like '9010004-010-011-0001-0002' or 
		 sXauNoiMa like '9010004-010-011-0002-0002'
		 then (DVKT) else 0 end) 
		 + sum(case when sXauNoiMa like '9010004-010-011-0001-0003' or 
		 sXauNoiMa like '9010004-010-011-0002-0003'
		 then (dcyt) else 0 end) )/@DonViTinh,0) fTienTongCong
		 , iID_MaDonVi as IIDMaDonVi
		 , sTenDonVi
		 into #tempReulst
		from #tempData
		where iID_MaDonVi in (select * from f_split(@AgencyId))
		group by iID_MaDonVi, sTenDonVi
		order by iID_MaDonVi

		select 
		ROW_NUMBER() OVER(ORDER BY T1.IIDMaDonVi ASC) AS STT
		,T1.*
		,LTRIM(RTRIM(T2.SGhiChu1)) SGhiChu1
		,LTRIM(RTRIM(T2.SGhiChu2)) SGhiChu2 from #tempReulst T1
		left join #tempDataGhiChu T2 on T1.IIDMaDonVi=t2.iID_MaDonVi
		order by iID_MaDonVi

		drop table #tempGhiChu
		drop table #tempReulst
		drop table #tempData
		drop table #tempDataGhiChu
END
;
;
;
GO

INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) VALUES (newid(), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rpt_BH_QuyetToan_Qkcb_TongHopChi', NULL, N'rpt_BH_QuyetToan_Qkcb_TongHopChi', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', N'BẢNG KÊ TỔNG HỢP', NULL, N'CHI PHÍ KHÁM BỆNH, CHỮA BỆNH TẠI QUÂN Y ĐƠN VỊ', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

/****** Object:  StoredProcedure [dbo].[sp_bhxh_ndt_ctg_get_khc_slns]    Script Date: 12/13/2024 10:04:48 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_ndt_ctg_get_khc_slns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_ndt_ctg_get_khc_slns]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_get_data_khc_clone]    Script Date: 12/13/2024 10:04:48 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_get_data_khc_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_get_data_khc_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_get_data_khc_clone]    Script Date: 12/13/2024 10:04:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_get_data_khc_clone]
	@NamLamViec int,
	@SoChungTu nvarchar(500),
	@SLNS nvarchar(500)
AS
BEGIN
	select
		ct.iID_MaDonVi IIdMaDonVi,
		mlns.sXauNoiMa,
		mlns.iID_MLNS IID_MucLucNganSach,
		mlns.sLNS,
		ctct.fTienKeHoachThucHienNamNay
		into #temp
	from
	BH_DM_MucLucNganSach mlns
	left join
	BH_KHC_CheDoBHXH_ChiTiet ctct on ctct.iID_MucLucNganSach = mlns.iID_MLNS
	join
	(select * from BH_KHC_CheDoBHXH
		where iNamLamViec = @NamLamViec and bDaTongHop=1
		and sSoChungTu in ((SELECT * FROM f_split(@SoChungTu)))) ct on ctct.iID_KHC_CheDoBHXH = ct.id
	--join BH_DM_MucLucNganSach mlns on ctct.iID_MucLucNganSach = mlns.iID_MLNS
	----------------
	union
	select
		ct.iID_MaDonVi IIdMaDonVi,
		mlns.sXauNoiMa,
		mlns.iID_MLNS IID_MucLucNganSach,
		mlns.sLNS,
		ctct.fTienKeHoachThucHienNamNay
	from
	 BH_DM_MucLucNganSach mlns  
	 left join 
	BH_KHC_KinhPhiQuanLy_ChiTiet ctct on ctct.iID_MucLucNganSach = mlns.iID_MLNS
	join
	(select * from BH_KHC_KinhPhiQuanLy
		where iNamLamViec = @NamLamViec and bDaTongHop=1
		and sSoChungTu in ((SELECT * FROM f_split(@SoChungTu)))) ct on ctct.iID_KHC_KinhPhiQuanLy = ct.iID_BH_KHC_KinhPhiQuanLy
	--join BH_DM_MucLucNganSach mlns on ctct.iID_MucLucNganSach = mlns.iID_MLNS
	------------------
	union
	select
		ct.iID_MaDonVi IIdMaDonVi,
		mlns.sXauNoiMa,
		mlns.iID_MLNS IID_MucLucNganSach,
		mlns.sLNS,
		ctct.fTienKeHoachThucHienNamNay
	from
	 BH_DM_MucLucNganSach mlns 
	left join
	BH_KHC_KCB_ChiTiet ctct on ctct.iID_MucLucNganSach = mlns.iID_MLNS
	join
	(select * from BH_KHC_KCB
		where iNamLamViec = @NamLamViec and bDaTongHop=1
		and sSoChungTu in ((SELECT * FROM f_split(@SoChungTu)))) ct on ctct.iID_KHC_KCB = ct.iID_BH_KHC_KCB
	union
	select
		ct.iID_MaDonVi IIdMaDonVi,
		mlns.sXauNoiMa,
		mlns.iID_MLNS IID_MucLucNganSach,
		mlns.sLNS,
		ctct.fTienKeHoachThucHienNamNay
	from
	 BH_DM_MucLucNganSach mlns 
	left join BH_KHC_K_ChiTiet ctct on ctct.iID_MucLucNganSach = mlns.iID_MLNS
	join
	(select * from BH_KHC_K
		where iNamLamViec = @NamLamViec
		and sSoChungTu in ((SELECT * FROM f_split(@SoChungTu)))) ct on ctct.iID_KHC_K = ct.iID_BH_KHC_K

		-- 	--round(IsNull(dt.BhxhNldDongDuToan, 0) / 1000000, 0) * 1000000
	--- Get data
	select * INTO #result from
	(
		--- che do bao hiem
		select SUBSTRING(A.sXauNoiMa,1,20) sXauNoiMa ,A.IIdMaDonVi, 
		round(SUM(isnull(A.fTienKeHoachThucHienNamNay,0)) / 1000000,0) * 1000000 fTienKeHoachThucHienNamNay
		from #temp A
		where A.sLNS in (select * from splitstring('9010001,9010002'))
		Group by SUBSTRING(A.sXauNoiMa,1,20),A.IIdMaDonVi
		--- Cssk hssv va nld
		UNION ALL
		select SUBSTRING(A.sXauNoiMa,1,3) sXauNoiMa ,A.IIdMaDonVi, 
		round(SUM(isnull(A.fTienKeHoachThucHienNamNay,0)) / 1000000,0) * 1000000 fTienKeHoachThucHienNamNay
		from #temp A
		where A.sLNS in (select * from splitstring('905,9050001,9050002'))
		Group by SUBSTRING(A.sXauNoiMa,1,3),A.IIdMaDonVi
		--- KPQL, KCB quan y, KCB truong sa,  KCB BHYT , TTB Y Te, BHTN
		UNION ALL
		select SUBSTRING(A.sXauNoiMa,1,7) sXauNoiMa ,A.IIdMaDonVi, 
		round(SUM(isnull(A.fTienKeHoachThucHienNamNay,0)) / 1000000,0) * 1000000 fTienKeHoachThucHienNamNay
		from #temp A
		where A.sLNS in (select * from splitstring('9010004,9010003,9010006,9010008,9010009,9010010'))
		Group by SUBSTRING(A.sXauNoiMa,1,7),A.IIdMaDonVi

		) as test

	select * from #result
	drop table #temp
	drop table #result

END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_ndt_ctg_get_khc_slns]    Script Date: 12/13/2024 10:04:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bhxh_ndt_ctg_get_khc_slns]
@iDNdtctg uniqueidentifier,
@sLns nvarchar(max),
@NamLamViec int,
@IIDDonVi nvarchar(50)

AS
BEGIN

	DECLARE @fTienDauNam FLOAT;
	SELECT @fTienDauNam = SUM(0.1*(ctct.fThu_BHYT_NLD + ctct.fThu_BHYT_NSD))
	FROM BH_DTT_BHXH_ChungTu_ChiTiet as ctct
	JOIN BH_DTT_BHXH_ChungTu as ct
	ON ctct.iID_DTT_BHXH = ct.iID_DTT_BHXH
	WHERE
	(ctct.sXauNoiMa like '9020001-010-011-0001%' or ctct.sXauNoiMa like '9020002-010-011-0001%')
	AND ctct.iNamLamViec = @NamLamViec
	AND ct.iLoaiDuToan = 1

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
	a.bHangChaDuToan,
	A.bHangCha as isHangCha,
	B.iID_DTC_DuToanChiTrenGiao,
	A.iID_MLNS AS iID_MucLucNganSach,
	round(isnull(CASE WHEN A.sXauNoiMa = '9010004' THEN @fTienDauNam ELSE B.fTienKeHoachThucHienNamNay END,0) /1000000 ,0) * 1000000 as fTienTuChi,
	A.iNamlamViec,
	A.sCPChiTietToi,
	A.sDuToanChiTietToi,
	@IIDDonVi as iID_MaDonVi
	FROM BH_DM_MucLucNganSach AS A
		LEFT JOIN (
					select	
					@iDNdtctg as iID_DTC_DuToanChiTrenGiao
					, ctct.sXauNoiMa
					, ctct.fTienKeHoachThucHienNamNay
					from BH_KHC_CheDoBHXH_ChiTiet ctct
					left join BH_KHC_CheDoBHXH ct on ctct.iID_KHC_CheDoBHXH=ct.ID
					where (ct.iLoaiTongHop=2 ) and ct.iID_MaDonVi=@IIDDonVi
					and ct.iNamLamViec=@NamLamViec
					and ct.bIsKhoa=1
					UNION All

					select	
					@iDNdtctg as iID_DTC_DuToanChiTrenGiao
					, ctct.sXauNoiMa
					, ctct.fTienKeHoachThucHienNamNay
					from BH_KHC_KinhPhiQuanLy_ChiTiet ctct
					left join BH_KHC_KinhPhiQuanLy ct on ctct.iID_KHC_KinhPhiQuanLy=ct.iID_BH_KHC_KinhPhiQuanLy
					where (ct.iLoaiTongHop=2 ) and ct.iID_MaDonVi=@IIDDonVi
					and ct.iNamLamViec=@NamLamViec
					and ct.bIsKhoa=1

					UNION ALL
					select
					@iDNdtctg as iID_DTC_DuToanChiTrenGiao
					, ctct.sXauNoiMa
					, ctct.fTienKeHoachThucHienNamNay
					from BH_KHC_KCB_ChiTiet ctct
					left join BH_KHC_KCB ct on ctct.iID_KHC_KCB=ct.iID_BH_KHC_KCB
					where (ct.iLoaiTongHop=2 ) and ct.iID_MaDonVi=@IIDDonVi
					and ct.iNamLamViec=@NamLamViec
					and ct.bIsKhoa=1

					UNION ALL
					select	
					@iDNdtctg as iID_DTC_DuToanChiTrenGiao
					, ctct.sXauNoiMa
					, ctct.fTienKeHoachThucHienNamNay
					from BH_KHC_K_ChiTiet ctct
					left join BH_KHC_K ct on ctct.iID_KHC_K=ct.iID_BH_KHC_K
					where (ct.iLoaiTongHop=2 ) and ct.iID_MaDonVi=@IIDDonVi
					and ct.iNamLamViec=@NamLamViec
					and ct.bIsKhoa=1
			
			) AS B
		ON A.sXauNoiMa = B.sXauNoiMa
	WHERE   A.sLNS IN (SELECT * FROM f_split(@sLns))
		AND A.iNamlamViec=@NamLamViec
		AND A.iTrangThai=1
		--AND a.bHangChaDuToan IS NOT NULL
	order by A.sXauNoiMa
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt_slns_clone]    Script Date: 12/13/2024 10:52:04 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt_slns_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt_slns_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet]    Script Date: 12/13/2024 10:52:04 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_dt_danhsach_pbdtc_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet]    Script Date: 12/13/2024 10:52:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet]
	@ChungTuId NVARCHAR(255),
	@LNS NVARCHAR(MAX),
	@IdDonVi NVARCHAR(MAX),
	@NamLamViec int,
	@UserName NVARCHAR(100)
As

begin
	-- Lấy danh mục Phân bổ thu BHXH
	SELECT round(SUM(0.1*(pbctct.fBHYT_NLD+ pbctct.fBHYT_NSD)) / 1000000,0) * 1000000 as fTienPhanBo, pbctct.iID_MaDonVi INTO #temPBThuBHXH
	FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet as pbctct
	JOIN BH_DTT_BHXH_PhanBo_ChungTu as pbct
	ON pbctct.iID_DTT_BHXH_ChungTu = pbct.iID_DTT_BHXH_PhanBo_ChungTu
	WHERE (pbctct.sXauNoiMa like '9020001-010-011-0001%' or pbctct.sXauNoiMa like '9020002-010-011-0001%')
	AND pbctct.iNamLamViec = @NamLamViec
	AND pbctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND pbct.iLoaiDuToan = 1
	GROUP BY pbctct.iID_MaDonVi

	---Lấy danh sách mục lục ngân sách theo điều kiện @LNS
	select 
	NEWID() as iID_DTC_DuToanChiTrenGiao,
	CAST(NULL AS VARCHAR(50)) as iID_DTC_PhanBoDuToanChiTiet,
	BH_DM_MucLucNganSach.iID_MLNS as iID_MLNS,
	BH_DM_MucLucNganSach.iID_MLNS_Cha,
	BH_DM_MucLucNganSach.sLNS,
	BH_DM_MucLucNganSach.sL,
	BH_DM_MucLucNganSach.sK,
	BH_DM_MucLucNganSach.sM,
	BH_DM_MucLucNganSach.sTM,
	BH_DM_MucLucNganSach.sTTM,
	BH_DM_MucLucNganSach.sNG,
	BH_DM_MucLucNganSach.sTNG,
	BH_DM_MucLucNganSach.sXauNoiMa,
	BH_DM_MucLucNganSach.sMoTa as sNoiDung,
	0 as fTienTuChi,
	--0 as fTienTuChiTruocDieuChinh,
	--0 as fTienHienVat,
	--0 as fTienHienVatTruocDieuChinh,
	BH_DM_MucLucNganSach.sCPChiTietToi,
	BH_DM_MucLucNganSach.sDuToanChiTietToi,
	1 as Type,
	'' as iID_MaDonVi,
	'' as sTenDonVi,
	'' as sSoQuyetDinh,
	BH_DM_MucLucNganSach.bHangCha as bHangCha,
	BH_DM_MucLucNganSach.bHangChaDuToan,
	0 as IsRemainRow
	into #tblMucLucNganSach
	from BH_DM_MucLucNganSach 
	--where sLNS  IN (SELECT * FROM f_split(@LNS)) and iNamLamViec = @NamLamViec  and sLNS LIKE '901%'
	where sLNS  IN (SELECT * FROM f_split(@LNS)) and iNamLamViec = @NamLamViec  
	and bHangChaDuToan is not null
	and iTrangThai=1
	---Lấy danh sách đơn vị được phân bổ
	select * 
	into  #tblDonVi
	from DonVi where iNamLamViec = @NamLamViec and iID_MaDonVi  IN (SELECT * FROM f_split(@IdDonVi)) ;

	--lấy danh sách dự toán nhận phân bổ
	select *
	into #tblChungTuNhanPhanBo
	from BH_DTC_Nhan_PhanBo_Map
	where iID_BHDTC_PhanBo = @ChungTuId

	
	---Lấy danh sách chi tiết dự toán toán nhận phân bổ
	select 
			nhanpb_chitiet.ID as iIDNhan_ChiTiet,
			nhanpb_chitiet.iID_DTC_DuToanChiTrenGiao as iID_DTC_DuToanChiTrenGiao,
			'' as iID_MaDonVi,
			nhanpb_chitiet.iID_MucLucNganSach,
			nhanpb_chitiet.fTienTuChi as fTuChi,
			--nhanpb_chitiet.fTienHienVat as fHienVat,
			nhanpb.sSoQuyetDinh
	into #tblChiTietDuToanNhan
	from BH_DTC_DuToanChiTrenGiao_ChiTiet as nhanpb_chitiet
	inner join BH_DTC_DuToanChiTrenGiao as nhanpb on nhanpb.ID = nhanpb_chitiet.iID_DTC_DuToanChiTrenGiao
	where iID_DTC_DuToanChiTrenGiao in (select iID_BHDTC_NhanPhanBo from #tblChungTuNhanPhanBo)

	

	---Lấy  danh sách chi tiết nhận phân bổ có thông tin mục lục ngân sách
	select 
		#tblChiTietDuToanNhan.iID_DTC_DuToanChiTrenGiao,
		#tblChiTietDuToanNhan.iID_MucLucNganSach as iID_MLNS,
		#tblMucLucNganSach.iID_MLNS_Cha,
		#tblMucLucNganSach.sLNS,
		#tblMucLucNganSach.sL,
		#tblMucLucNganSach.sK, 
		#tblMucLucNganSach.sM,
		#tblMucLucNganSach.sTM,
		#tblMucLucNganSach.sTTM,
		#tblMucLucNganSach.sNG,
		#tblMucLucNganSach.sTNG,
		#tblMucLucNganSach.sXauNoiMa,
		#tblMucLucNganSach.sNoiDung,
		#tblChiTietDuToanNhan.sSoQuyetDinh,
		#tblChiTietDuToanNhan.fTuChi as fTienTuChi ,
		--#tblChiTietDuToanNhan.fHienVat as fTienHienVat,
		#tblMucLucNganSach.sCPChiTietToi,
		#tblMucLucNganSach.sDuToanChiTietToi,
		3 as Type
	into #tbl_tblChiTietDuToanNhan_MucLuc
	from #tblMucLucNganSach
	inner join #tblChiTietDuToanNhan on #tblMucLucNganSach.iID_MLNS = #tblChiTietDuToanNhan.iID_MucLucNganSach

	


	---Hiển thị danh sách chi tiết nhận phân bổ theo đơn vị được chọn phân bổ
	select  #tbl_tblChiTietDuToanNhan_MucLuc.*,#tblDonVi.iID_MaDonVi, concat(#tblDonVi.iID_MaDonVi, '-', #tblDonVi.sTenDonVi) as sTenDonVi, 0 as bHangCha
	into #temp
	from #tbl_tblChiTietDuToanNhan_MucLuc cross join #tblDonVi 


	---Map với bảng BH_DTC_PhanBoDuToanChi_ChiTiet để lấy thông tin fTuChi đã được phân bổ
	select 
		#temp.iID_DTC_DuToanChiTrenGiao, 
		chitiet_phanbo.ID as iID_DTC_PhanBoDuToanChiTiet,
		#temp.iID_MLNS,
		#temp.iID_MLNS_Cha,
		#temp.sLNS,
		#temp.sL,
		#temp.sK,
		#temp.sM,
		#temp.sTM,
		#temp.sTTM,
		#temp.sNG,
		#temp.sTNG,
		#temp.sXauNoiMa,
		#temp.sNoiDung as sNoiDung,
		chitiet_phanbo.fTienTuChi as fTienTuChi,
		#temp.fTienTuChi as fTienTuChiTruocDieuChinh,
		--chitiet_phanbo.fTienHienVat as fTienHienVat,
		--#temp.fTienHienVat as fTienHienVatTruocDieuChinh,
		#temp.sCPChiTietToi,
		#temp.sDuToanChiTietToi,
		3 as Type,
		#temp.iID_MaDonVi,
		#temp.sTenDonVi,
		#temp.sSoQuyetDinh,
		0 as bHangCha,
		0 as bHangChaDuToan,
		0 as IsRemainRow
	into #temp1
	from #temp
	left join 
		(
			select * 
			from BH_DTC_PhanBoDuToanChi_ChiTiet 
			where iID_DTC_PhanBoDuToanChi = @ChungTuId
		) as chitiet_phanbo
		on chitiet_phanbo.iID_DTC_DuToanChiTrenGiao = #temp.iID_DTC_DuToanChiTrenGiao and chitiet_phanbo.iID_MaDonVi = #temp.iID_MaDonVi and chitiet_phanbo.iID_MucLucNganSach = #temp.iID_MLNS



	-----Lấy danh sách số chưa phân bổ
	select 
	npb.ID as iID_DTC_DuToanChiTrenGiao,
	CAST(NULL AS VARCHAR(50)) as iID_DTC_PhanBoDuToanChiTiet,
	muluc_ngansach.iID_MLNS as iID_MLNS,
	muluc_ngansach.iID_MLNS_Cha,
	muluc_ngansach.sLNS,
	muluc_ngansach.sL,
	muluc_ngansach.sK,
	muluc_ngansach.sM,
	muluc_ngansach.sTM,
	muluc_ngansach.sTTM,
	muluc_ngansach.sNG,
	muluc_ngansach.sTNG,
	muluc_ngansach.sXauNoiMa,
	N'Số chưa phân bổ' as sNoiDung,
	chitiet_chuaphanbo.fTuChi as fTienTuChi,
	chitiet_chuaphanbo.fTuChi as fTienTuChiTruocDieuChinh,
	--chitiet_chuaphanbo.fHienVat as fTienHienVat,
	--chitiet_chuaphanbo.fHienVat as fTienHienVatTruocDieuChinh,
	muluc_ngansach.sCPChiTietToi,
	muluc_ngansach.sDuToanChiTietToi,
	2 as Type,
	'' as iID_MaDonVi,
	'' as sTenDonVi,
	npb.sSoQuyetDinh as sSoQuyetDinh,
	1 as bHangCha,
	1 as bHangChaDuToan,
	1 as IsRemainRow
	into #tblSoChuaPhanBo
	from #tblMucLucNganSach as muluc_ngansach
	inner join 
	(
		select (
		
		ISNULL(ct_npb.fTienTuChi,0) -  ISNULL(ct_pb_t.fTuChi,0) ) as fTuChi ,
		ct_npb.iID_MucLucNganSach, ct_npb.iID_DTC_DuToanChiTrenGiao 
		--select (ISNULL(ct_npb.fTienTuChi,0) - ISNULL(ct_pb_t.fTuChi,0)) as fTuChi , (ISNULL(ct_npb.fTienHienVat,0) - ISNULL(ct_pb_t.fHienVat,0)) as fHienVat, ct_npb.iID_MucLucNganSach, ct_npb.iID_DTC_DuToanChiTrenGiao 
		from BH_DTC_DuToanChiTrenGiao_ChiTiet as ct_npb
		left join
			(
				select  sum(  fTienTuChi) as fTuChi , iID_MucLucNganSach, iID_DTC_DuToanChiTrenGiao
				from BH_DTC_PhanBoDuToanChi_ChiTiet as ct_pb
				where iID_DTC_DuToanChiTrenGiao in (select iID_BHDTC_NhanPhanBo from #tblChungTuNhanPhanBo)
				group by  iID_MucLucNganSach, iID_DTC_DuToanChiTrenGiao
			)as ct_pb_t  

		on ct_pb_t.iID_MucLucNganSach = ct_npb.iID_MucLucNganSach and ct_npb.iID_DTC_DuToanChiTrenGiao = ct_pb_t.iID_DTC_DuToanChiTrenGiao) as chitiet_chuaphanbo
		on chitiet_chuaphanbo.iID_MucLucNganSach = muluc_ngansach.iID_MLNS 
	inner join BH_DTC_DuToanChiTrenGiao as npb on npb.ID = chitiet_chuaphanbo.iID_DTC_DuToanChiTrenGiao
	where npb.ID in ( select iID_BHDTC_NhanPhanBo from #tblChungTuNhanPhanBo);
	---- Lấy mục lục ngân sách có dupliate các mục lục ngân sách con
	select 
	#tblSoChuaPhanBo.iID_DTC_DuToanChiTrenGiao as iID_DTC_DuToanChiTrenGiao,
	CAST(NULL AS VARCHAR(50)) as iID_DTC_PhanBoDuToanChiTiet,
	#tblMucLucNganSach.iID_MLNS as iID_MLNS,
	#tblMucLucNganSach.iID_MLNS_Cha,
	#tblMucLucNganSach.sLNS,
	#tblMucLucNganSach.sL,
	#tblMucLucNganSach.sK,
	#tblMucLucNganSach.sM,
	#tblMucLucNganSach.sTM,
	#tblMucLucNganSach.sTTM,
	#tblMucLucNganSach.sNG,
	#tblMucLucNganSach.sTNG,
	#tblMucLucNganSach.sXauNoiMa,
	#tblMucLucNganSach.sNoiDung as sNoiDung,
	#tblSoChuaPhanBo.fTienTuChi as fTienTuChi,
	#tblSoChuaPhanBo.fTienTuChiTruocDieuChinh as fTienTuChiTruocDieuChinh,
	--#tblSoChuaPhanBo.fTienHienVat as fTienHienVat,
	--#tblSoChuaPhanBo.fTienHienVatTruocDieuChinh as fTienHienVatTruocDieuChinh,
	#tblMucLucNganSach.sCPChiTietToi,
	#tblMucLucNganSach.sDuToanChiTietToi,
	case when #tblSoChuaPhanBo.Type = 2 then 2 else #tblMucLucNganSach.Type end Type,
	'' as iID_MaDonVi,
	'' as sTenDonVi,
	#tblSoChuaPhanBo.sSoQuyetDinh as sSoQuyetDinh,
	case when #tblSoChuaPhanBo.Type = 2 then 1 else #tblMucLucNganSach.bHangCha end bHangCha,
	case when #tblSoChuaPhanBo.Type = 2 then 1 else #tblMucLucNganSach.bHangCha end bHangChaDuToan,
	0 as IsRemainRow
	into #tblMucLucNganSach_duplicate
	from #tblMucLucNganSach
	left join #tblSoChuaPhanBo on #tblMucLucNganSach.iID_MLNS = #tblSoChuaPhanBo.iID_MLNS
	order by sXauNoiMa
	
	--select * from tblMucLucNganSach_duplicate
	--select * from tblSoChuaPhanBo
	--select * from #temp1
	---Tính lại dự toán, số đã phân bổ
	-- Dữ liệu nhận phân bổ
	declare @iiDotNhan nvarchar(500) =( select  iID_DotNhan from  BH_DTC_PhanBoDuToanChi where ID = @ChungTuId);
	select iID_MucLucNganSach,iID_DTC_DuToanChiTrenGiao, fTienTuChi INTO #tmpNhanDuToan from BH_DTC_DuToanChiTrenGiao_ChiTiet ct
	INNER JOIN BH_DTC_DuToanChiTrenGiao dt on dt.ID = ct.iID_DTC_DuToanChiTrenGiao
	where dt.ID IN (select * from splitstring( @iiDotNhan))


	-- Dữ liệu đã phân bổ
	declare @dNgayQuyetDinh Datetime = (select dNgayQuyetDinh from BH_DTC_PhanBoDuToanChi where ID = @ChungTuId);
	select iID_MucLucNganSach,iID_DTC_DuToanChiTrenGiao, 0 - SUM(ISNULL(fTienTuChi,0)) fTuChi   INTO #tmpDaPhanBo from BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	INNER JOIN BH_DTC_PhanBoDuToanChi ct ON ctct.iID_DTC_PhanBoDuToanChi = ct.ID
	where 
	ct.iNamChungTu = @NamLamViec
	AND ct.dNgayQuyetDinh < @dNgayQuyetDinh
	group by iID_MucLucNganSach,iID_DTC_DuToanChiTrenGiao;

	--Hiển thị kết quả trả về
	select * INTO #result from
	(
		SELECT * from #tblMucLucNganSach_duplicate
		UNION ALL
		SELECT * from #tblSoChuaPhanBo
		UNION ALL
		SELECT * from #temp1
	) as test
	--order by test.sXauNoiMa, test.sSoQuyetDinh, test.iID_MaDonVi,test.Type,test.IsRemainRow

	----============
	SELECT
	rs.iID_DTC_DuToanChiTrenGiao,
	rs.iID_DTC_PhanBoDuToanChiTiet,
	rs.iID_MLNS,
	rs.iID_MLNS_Cha,
	rs.sLNS,
	rs.sL,
	rs.sK,
	rs.sM,
	rs.sTM,
	rs.sTTM,
	rs.sNG,
	rs.sTNG,
	rs.sXauNoiMa,
	rs.sNoiDung as sNoiDung,

	CASE WHEN rs.sXauNoiMa = '9010004' THEN pbbhxh.fTienPhanBo
	ELSE (
		CASE WHEN rs.IsRemainRow = 1 THEN ISNULL(dt.fTienTuChi,0) + (ISNULL(dpb.fTuChi, 0))
		WHEN rs.IsRemainRow = 0 AND rs.Type = 2 THEN ISNULL(dt.fTienTuChi,0) + (ISNULL(dpb.fTuChi, 0))
		ELSE rs.fTienTuChi
		END
	)
	END as fTienTuChi,
	CASE WHEN rs.IsRemainRow = 1 THEN ISNULL(dt.fTienTuChi,0) + (ISNULL(dpb.fTuChi, 0))
		WHEN rs.IsRemainRow = 0 AND rs.Type = 2 THEN ISNULL(dt.fTienTuChi,0) + (ISNULL(dpb.fTuChi, 0))
		ELSE rs.fTienTuChi
	END as fTienTuChiTruocDieuChinh,
	--#tblSoChuaPhanBo.fTienHienVat as fTienHienVat,
	--#tblSoChuaPhanBo.fTienHienVatTruocDieuChinh as fTienHienVatTruocDieuChinh,
	rs.sCPChiTietToi,
	rs.sDuToanChiTietToi,
	rs.Type,
	rs.iID_MaDonVi,
	rs.sTenDonVi,
	rs.sSoQuyetDinh,
	rs.bHangCha,
	rs.bHangChaDuToan,
	rs.IsRemainRow
	FROM #result rs
	LEFT JOIN #tmpNhanDuToan dt ON rs.iID_MLNS = dt.iID_MucLucNganSach 
	LEFT JOIN #tmpDaPhanBo dpb ON dpb.iID_MucLucNganSach = rs.iID_MLNS and dpb.iID_DTC_DuToanChiTrenGiao = rs.iID_DTC_DuToanChiTrenGiao
	LEFT JOIN (
	SELECT SUM(fTienTuChi) fTuChi, iID_MucLucNganSach FROM BH_DTC_PhanBoDuToanChi_ChiTiet WHERE iID_DTC_PhanBoDuToanChi = @ChungTuId GROUP BY iID_MucLucNganSach

	) ct ON ct.iID_MucLucNganSach = rs.iID_MLNS
	LEFT JOIN #temPBThuBHXH pbbhxh ON rs.iID_MaDonVi = pbbhxh.iID_MaDonVi
	order by rs.sXauNoiMa, rs.sSoQuyetDinh, rs.iID_MaDonVi,rs.Type,rs.IsRemainRow
	--SELECT * from #tblSoChuaPhanBo


drop table #tblMucLucNganSach;
drop table #tblDonVi;
drop table #tblChungTuNhanPhanBo;
drop table #tblChiTietDuToanNhan;
drop table #tbl_tblChiTietDuToanNhan_MucLuc;
drop table #temp;
drop table #temp1;
drop table #tblSoChuaPhanBo;
drop table #tblMucLucNganSach_duplicate;

end
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt_slns_clone]    Script Date: 12/13/2024 10:52:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt_slns_clone]
@iDNdtctg uniqueidentifier,
@sLns nvarchar(max),
@NamLamViec int,
@IIDDonVi nvarchar(50)

AS
BEGIN

	DECLARE @fTienDauNam FLOAT;
	SELECT @fTienDauNam = Round(SUM(0.1*(ctct.fThu_BHYT_NLD + ctct.fThu_BHYT_NSD)) /1000000,0) * 1000000
	FROM BH_DTT_BHXH_ChungTu_ChiTiet as ctct
	JOIN BH_DTT_BHXH_ChungTu as ct
	ON ctct.iID_DTT_BHXH = ct.iID_DTT_BHXH
	WHERE
	(ctct.sXauNoiMa like '9020001-010-011-0001%' or ctct.sXauNoiMa like '9020002-010-011-0001%')
	AND ctct.iNamLamViec = @NamLamViec
	AND ct.iLoaiDuToan = 1


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
	A.bHangChaDuToan bHangCha,
	A.bHangChaDuToan as isHangCha,
	B.ID,
	B.iID_DTC_DuToanChiTrenGiao,
	A.iID_MLNS AS iID_MucLucNganSach,
	B.fTongTien,
	B.fTienHienVat,
	CASE WHEN A.sXauNoiMa = '9010004' THEN @fTienDauNam ELSE B.fTienTuChi END as fTienTuChi,
	A.sCPChiTietToi,
	A.sDuToanChiTietToi,
	A.iNamlamViec,
	@IIDDonVi as iID_MaDonVi,
	 B.dNgaySua,
	 B.dNgayTao,
	 B.sNguoiSua,
	 B.sNguoiTao
	FROM BH_DM_MucLucNganSach AS A
	LEFT JOIN ( SELECT ctct.*, CT.iLoaiDotNhanPhanBo
					FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct
				LEFT JOIN BH_DTC_DuToanChiTrenGiao CT ON ctct.iID_DTC_DuToanChiTrenGiao=CT.ID 
				WHERE ctct.iID_DTC_DuToanChiTrenGiao = @iDNdtctg
					 and ct.iID_MaDonVi=@IIDDonVi 
					 And CT.iNamLamViec=@NamLamViec) AS B
	ON B.iID_MucLucNganSach = A.iID_MLNS
	WHERE   A.sLNS IN (SELECT * FROM f_split( @sLns))
		AND A.iNamlamViec=@NamLamViec
		AND A.bHangChaDuToan IS NOT NULL
		AND A.iTrangThai=1
	order by A.sXauNoiMa
END
;
;
;
;
;
GO
