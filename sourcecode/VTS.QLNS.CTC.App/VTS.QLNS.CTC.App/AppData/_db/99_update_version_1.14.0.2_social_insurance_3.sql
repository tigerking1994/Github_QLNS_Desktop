/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chi_tiet_cssk_hssv_nld]    Script Date: 2/23/2024 10:08:16 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_chungtu_chi_tiet_cssk_hssv_nld]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_chungtu_chi_tiet_cssk_hssv_nld]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chi_tiet_chedo_bhxh]    Script Date: 2/23/2024 10:08:16 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_chungtu_chi_tiet_chedo_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_chungtu_chi_tiet_chedo_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chi_tiet]    Script Date: 2/23/2024 10:08:16 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_chungtu_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_chungtu_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]    Script Date: 2/23/2024 10:08:16 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_bhxh]    Script Date: 2/23/2024 4:26:43 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]    Script Date: 2/23/2024 10:08:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet] @IdChungTu uniqueidentifier,
	@SLNS nvarchar (MAX),
	@INamLamViec INT,
	@MaDonVi nvarchar (100),
	@Loai BIT
	AS BEGIN
	DECLARE
		@quy INT;
	SELECT
		@quy = iquychungtu 
	FROM
		BH_QTC_Quy_CheDoBHXH 
	WHERE
		ID_QTC_Quy_CheDoBHXH = @IdChungTu;
---Lấy danh sách mục lục ngân sách
	SELECT
		danhmuc.iID_MLNS AS iID_MLNS,
		danhmuc.iID_MLNS_Cha,
		danhmuc.sLNS,
		danhmuc.sL,
		danhmuc.sK,
		danhmuc.sM,
		danhmuc.sTM,
		danhmuc.sTTM,
		danhmuc.sNG,
		danhmuc.sTNG,
		danhmuc.sXauNoiMa,
		danhmuc.sMoTa,
		danhmuc.bHangCha,
		danhmuc.sDuToanChiTietToi INTO #tblMucLucNganSach 
	FROM
		BH_DM_MucLucNganSach AS danhmuc 
	WHERE
		danhmuc.iNamLamViec = @INamLamViec 
		AND danhmuc.sLNs IN (SELECT * FROM f_split (@SLNS)) ---Lấy thông tin chi tiết chứng từ
	SELECT
		qtcn_ct.ID_QTC_Quy_CheDoBHXH_ChiTiet,
		qtcn_ct.iID_MucLucNganSach,
		qtcn_ct.sLoaiTroCap,
		qtcn_ct.fTienDuToanDuyet,
		(
			isnull(qtcn_ct_truoc.fTienCNVCQP_DeNghi, 0) + isnull(qtcn_ct_truoc.fTienHSQBS_DeNghi, 0) + isnull(qtcn_ct_truoc.fTienLDHD_DeNghi, 0) + isnull(qtcn_ct_truoc.fTienQNCN_DeNghi, 0) + isnull(qtcn_ct_truoc.fTienSQ_DeNghi, 0) 
		) fTienLuyKeCuoiQuyTruoc,
		(
			isnull(qtcn_ct_truoc.iSoCNVCQP_DeNghi, 0) + isnull(qtcn_ct_truoc.iSoHSQBS_DeNghi, 0) + isnull(qtcn_ct_truoc.iSoLDHD_DeNghi, 0) + isnull(qtcn_ct_truoc.iSoQNCN_DeNghi, 0) + isnull(qtcn_ct_truoc.iSoSQ_DeNghi, 0) 
		) iSoLuyKeCuoiQuyTruoc,
		qtcn_ct.iSoSQ_DeNghi,
		qtcn_ct.fTienSQ_DeNghi,
		qtcn_ct.iSoQNCN_DeNghi,
		qtcn_ct.fTienQNCN_DeNghi,
		qtcn_ct.iSoCNVCQP_DeNghi,
		qtcn_ct.fTienCNVCQP_DeNghi,
		qtcn_ct.iSoHSQBS_DeNghi,
		qtcn_ct.fTienHSQBS_DeNghi,
--qtcn_ct.iTongSo_DeNghi,
--qtcn_ct.fTongTien_DeNghi,
		qtcn_ct.fTongTien_PheDuyet,
		qtcn_ct.iSoLDHD_DeNghi,
		qtcn_ct.fTienLDHD_DeNghi,
		qtcn.iID_MaDonVi iIDMaDonVi,
		qtcn.iNamChungTu iNamLamViec INTO #tblQuyetToanQuyChiTiet 
	FROM
		BH_QTC_Quy_CheDoBHXH_ChiTiet AS qtcn_ct
		INNER JOIN BH_QTC_Quy_CheDoBHXH AS qtcn ON qtcn_ct.iID_QTC_Quy_CheDoBHXH = qtcn.ID_QTC_Quy_CheDoBHXH
		LEFT JOIN (
		SELECT SUM
			(isnull(fTienCNVCQP_DeNghi, 0)) fTienCNVCQP_DeNghi,
			SUM (isnull(fTienHSQBS_DeNghi, 0)) fTienHSQBS_DeNghi,
			SUM (isnull(fTienLDHD_DeNghi, 0)) fTienLDHD_DeNghi,
			SUM (isnull(fTienQNCN_DeNghi, 0)) fTienQNCN_DeNghi,
			SUM (isnull(fTienSQ_DeNghi, 0)) fTienSQ_DeNghi,
			SUM (isnull(iSoCNVCQP_DeNghi, 0)) iSoCNVCQP_DeNghi,
			SUM (isnull(ctct.fTongTien_PheDuyet, 0)) fTongTien_PheDuyet,
			SUM (isnull(iSoHSQBS_DeNghi, 0)) iSoHSQBS_DeNghi,
			SUM (isnull(iSoLDHD_DeNghi, 0)) iSoLDHD_DeNghi,
			SUM (isnull(iSoQNCN_DeNghi, 0)) iSoQNCN_DeNghi,
			SUM (isnull(iSoSQ_DeNghi, 0)) iSoSQ_DeNghi,
			ct.iID_MaDonVi,
			ct.iNamChungTu 
		FROM
			BH_QTC_Quy_CheDoBHXH_ChiTiet ctct
			INNER JOIN BH_QTC_Quy_CheDoBHXH ct ON ctct.iID_QTC_Quy_CheDoBHXH = ct.ID_QTC_Quy_CheDoBHXH 
		WHERE
			ct.iQuyChungTu < @quy 
		GROUP BY
			ct.iID_MaDonVi,
			ct.iNamChungTu 
		) qtcn_ct_truoc ON qtcn.iID_MaDonVi = qtcn_ct_truoc.iID_MaDonVi 
		AND qtcn.iNamChungTu = qtcn_ct_truoc.iNamChungTu 
	WHERE
		qtcn_ct.iID_QTC_Quy_CheDoBHXH = @IdChungTu 
		AND qtcn.iNamChungTu=@INamLamViec;
---Kết quả hiển thị trả về
	IF
		(@Loai = 1) SELECT
		mucluc.iID_MLNS,
		mucluc.iID_MLNS_Cha,
		mucluc.sLNS,
		mucluc.sL,
		mucluc.sK,
		mucluc.sM,
		mucluc.sTM,
		mucluc.sTTM,
		mucluc.sNG,
		mucluc.sTNG,
		mucluc.sXauNoiMa,
		mucluc.bHangCha,
		mucluc.sDuToanChiTietToi,
		chi_tiet.ID_QTC_Quy_CheDoBHXH_ChiTiet,
		mucluc.iID_MLNS AS iID_MucLucNganSach,
		mucluc.sMoTa AS sLoaiTroCap,
		(isnull(dt.fTienTuChi, 0)) fTienDuToanDuyet,
		chi_tiet.iSoLuyKeCuoiQuyTruoc,
		chi_tiet.fTienLuyKeCuoiQuyTruoc,
		chi_tiet.iSoSQ_DeNghi,
		chi_tiet.fTienSQ_DeNghi,
		chi_tiet.iSoQNCN_DeNghi,
		chi_tiet.fTienQNCN_DeNghi,
		chi_tiet.iSoCNVCQP_DeNghi,
		chi_tiet.fTienCNVCQP_DeNghi,
		chi_tiet.iSoHSQBS_DeNghi,
		chi_tiet.fTienHSQBS_DeNghi,
--chi_tiet.iTongSo_DeNghi,
--chi_tiet.fTongTien_DeNghi,
		chi_tiet.fTongTien_PheDuyet,
		chi_tiet.iSoLDHD_DeNghi,
		chi_tiet.fTienLDHD_DeNghi,
		chi_tiet.iIDMaDonVi,
		chi_tiet.iNamLamViec 
	FROM
		#tblMucLucNganSach AS mucluc
		LEFT JOIN (
			SELECT ctct.sXauNoiMa, SUM(ctct.fTienTuChi)  fTienTuChi
			FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct 
			JOIN BH_DTC_PhanBoDuToanChi ct ON ctct.iID_DTC_PhanBoDuToanChi = ct.ID 
				WHERE ctct.iID_MaDonVi = @MaDonVi 
				AND BIsKhoa = 1
				AND ct.iNamChungTu = @INamLamViec
				GROUP BY ctct.sXauNoiMa) dt ON dt.sXauNoiMa = mucluc.sXauNoiMa
		LEFT JOIN #tblQuyetToanQuyChiTiet AS chi_tiet ON mucluc.iID_MLNS = chi_tiet.iID_MucLucNganSach 
	ORDER BY
		mucluc.sXauNoiMa ELSE SELECT
		mucluc.iID_MLNS,
		mucluc.iID_MLNS_Cha,
		mucluc.sLNS,
		mucluc.sL,
		mucluc.sK,
		mucluc.sM,
		mucluc.sTM,
		mucluc.sTTM,
		mucluc.sNG,
		mucluc.sTNG,
		mucluc.sXauNoiMa,
		mucluc.bHangCha,
		mucluc.sDuToanChiTietToi,
		chi_tiet.ID_QTC_Quy_CheDoBHXH_ChiTiet,
		mucluc.iID_MLNS AS iID_MucLucNganSach,
		mucluc.sMoTa AS sLoaiTroCap,
		(isnull(dt.fTienTuChi, 0) ) fTienDuToanDuyet,
		chi_tiet.iSoLuyKeCuoiQuyTruoc,
		chi_tiet.fTienLuyKeCuoiQuyTruoc,
		chi_tiet.iSoSQ_DeNghi,
		chi_tiet.fTienSQ_DeNghi,
		chi_tiet.iSoQNCN_DeNghi,
		chi_tiet.fTienQNCN_DeNghi,
		chi_tiet.iSoCNVCQP_DeNghi,
		chi_tiet.fTienCNVCQP_DeNghi,
		chi_tiet.iSoHSQBS_DeNghi,
		chi_tiet.fTienHSQBS_DeNghi,
--chi_tiet.iTongSo_DeNghi,
--chi_tiet.fTongTien_DeNghi,
		chi_tiet.fTongTien_PheDuyet,
		chi_tiet.iSoLDHD_DeNghi,
		chi_tiet.fTienLDHD_DeNghi,
		chi_tiet.iIDMaDonVi,
		chi_tiet.iNamLamViec 
	FROM
		#tblMucLucNganSach AS mucluc
		LEFT JOIN (
					SELECT ctct.sXauNoiMa,
							SUM(ctct.fTienTuChi) fTienTuChi
							FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct 
							JOIN BH_DTC_DuToanChiTrenGiao ct ON ctct.iID_DTC_DuToanChiTrenGiao = ct.ID 
		WHERE ct.iID_MaDonVi = @MaDonVi
		AND BIsKhoa = 1
		AND ct.iNamLamViec = @INamLamViec
		GROUP BY ctct.sXauNoiMa) dt ON dt.sXauNoiMa = mucluc.sXauNoiMa
		LEFT JOIN #tblQuyetToanQuyChiTiet AS chi_tiet ON mucluc.iID_MLNS = chi_tiet.iID_MucLucNganSach 
	ORDER BY
		mucluc.sXauNoiMa DROP TABLE #tblMucLucNganSach;
	DROP TABLE #tblQuyetToanQuyChiTiet;
	
END;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chi_tiet]    Script Date: 2/23/2024 10:08:16 AM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chi_tiet_chedo_bhxh]    Script Date: 2/23/2024 10:08:16 AM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chi_tiet_cssk_hssv_nld]    Script Date: 2/23/2024 10:08:16 AM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_bhxh]    Script Date: 2/23/2024 4:26:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_bhxh]
@INamLamViec int,
@IdMaDonVi nvarchar(max),
@SLN nvarchar(max),
@IsTongHop bit,
@IQuy int,
@Donvitinh int
as
begin
	---Lấy danh sách mục lục ngân sách
		select 
			danhmuc.iID_MLNS as iID_MLNS,
			danhmuc.iID_MLNS_Cha,
			danhmuc.sLNS,
			danhmuc.sL,
			danhmuc.sK,
			danhmuc.sM,
			danhmuc.sTM,
			danhmuc.sTTM,
			danhmuc.sNG,
			danhmuc.sTNG,
			danhmuc.sXauNoiMa,
			danhmuc.sMoTa,
			danhmuc.sDuToanChiTietToi,
			danhmuc.bHangCha
		into #tblMucLucNganSach
		from BH_DM_MucLucNganSach as danhmuc
		where danhmuc.iNamLamViec = @INamLamViec and danhmuc.sLNs  in (select * from dbo.splitstring(@SLN)) 
		
		
		
	---Lấy thông tin chi tiết chứng từ
		select 
			qtcn_ct.iID_MucLucNganSach,
			qtcn_ct.sXauNoiMa,
			Sum(qtcn_ct.fTienDuToanDuyet) as fTienDuToanDuyet,
			Sum(qtcn_ct.iSoLuyKeCuoiQuyNay) as iSoLuyKeCuoiQuyNay,
			Sum(qtcn_ct.fTienLuyKeCuoiQuyNay) as fTienLuyKeCuoiQuyNay,
			sum(qtcn_ct.iSoSQ_DeNghi) as iSoSQ_DeNghi ,
			sum(qtcn_ct.fTienSQ_DeNghi) as fTienSQ_DeNghi ,
			sum(qtcn_ct.iSoQNCN_DeNghi) as iSoQNCN_DeNghi,
			sum(qtcn_ct.fTienQNCN_DeNghi) as fTienQNCN_DeNghi ,
			sum(qtcn_ct.iSoCNVCQP_DeNghi) as iSoCNVCQP_DeNghi ,
			sum(qtcn_ct.fTienCNVCQP_DeNghi) as fTienCNVCQP_DeNghi,
			sum(qtcn_ct.iSoHSQBS_DeNghi) as iSoHSQBS_DeNghi,
			sum(qtcn_ct.fTienHSQBS_DeNghi) as fTienHSQBS_DeNghi,
			sum(qtcn_ct.iTongSo_DeNghi) as iTongSo_DeNghi,
			sum(qtcn_ct.fTongTien_DeNghi) as fTongTien_DeNghi,
			sum(qtcn_ct.fTongTien_PheDuyet) as fTongTien_PheDuyet,
			sum(qtcn_ct.iSoLDHD_DeNghi) as iSoLDHD_DeNghi,
			sum(qtcn_ct.fTienLDHD_DeNghi) as fTienLDHD_DeNghi
		into #tblQuyetToanQuyChiTiet
		from BH_QTC_Quy_CheDoBHXH_ChiTiet as qtcn_ct
		inner join BH_QTC_Quy_CheDoBHXH  as qtcn on qtcn_ct.iID_QTC_Quy_CheDoBHXH = qtcn.ID_QTC_Quy_CheDoBHXH
		where qtcn.iID_MaDonVi in (select * from dbo.splitstring(@IdMaDonVi)) 
		and qtcn.iNamChungTu = @INamLamViec
		--and ((@IsTongHop = 0 and qtcn.bDaTongHop = 0 and qtcn.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  qtcn.sDSSoChungTuTongHop is not null))
		and qtcn.iQuyChungTu = @IQuy
		group by qtcn_ct.iID_MucLucNganSach,qtcn_ct.sXauNoiMa

		--- Get tien du toan 
		SELECT ctct.sXauNoiMa,
			SUM(ctct.fTienTuChi)  fTienDuToanDuyet
			into #tempDuToan
			FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct 
			JOIN BH_DTC_PhanBoDuToanChi ct ON ctct.iID_DTC_PhanBoDuToanChi = ct.ID 
				WHERE ctct.iID_MaDonVi in (select * from dbo.splitstring(@IdMaDonVi)) 
				AND BIsKhoa = 1
				AND ct.iNamChungTu = @INamLamViec
				GROUP BY ctct.sXauNoiMa
	--- lay luy kê quy truoc
	SELECT SUM
			(isnull(fTienCNVCQP_DeNghi, 0)) fTienCNVCQP_DeNghi,
			SUM (isnull(fTienHSQBS_DeNghi, 0)) fTienHSQBS_DeNghi,
			SUM (isnull(fTienLDHD_DeNghi, 0)) fTienLDHD_DeNghi,
			SUM (isnull(fTienQNCN_DeNghi, 0)) fTienQNCN_DeNghi,
			SUM (isnull(fTienSQ_DeNghi, 0)) fTienSQ_DeNghi,
			SUM (isnull(iSoCNVCQP_DeNghi, 0)) iSoCNVCQP_DeNghi,
			SUM (isnull(ctct.fTongTien_PheDuyet, 0)) fTongTien_PheDuyet,
			SUM (isnull(iSoHSQBS_DeNghi, 0)) iSoHSQBS_DeNghi,
			SUM (isnull(iSoLDHD_DeNghi, 0)) iSoLDHD_DeNghi,
			SUM (isnull(iSoQNCN_DeNghi, 0)) iSoQNCN_DeNghi,
			SUM (isnull(iSoSQ_DeNghi, 0)) iSoSQ_DeNghi,
			ct.iID_MaDonVi,
			ct.iNamChungTu,
			ctct.sXauNoiMa
			into #tempDuLieuQuyTruoc
		FROM
			BH_QTC_Quy_CheDoBHXH_ChiTiet ctct
			INNER JOIN BH_QTC_Quy_CheDoBHXH ct ON ctct.iID_QTC_Quy_CheDoBHXH = ct.ID_QTC_Quy_CheDoBHXH 
		WHERE
			ct.iQuyChungTu < @IQuy 
			and ct.iID_MaDonVi in (select * from dbo.splitstring(@IdMaDonVi)) 
			and ct.iNamChungTu=@INamLamViec
		GROUP BY
			ct.iID_MaDonVi,
			ct.iNamChungTu,
			ctct.sXauNoiMa

	---Kết quả hiển thị trả về
		select 
			mucluc.iID_MLNS,
			mucluc.iID_MLNS_Cha,
			mucluc.sLNS,
			mucluc.sL,
			mucluc.sK,
			mucluc.sM,
			mucluc.sTM,
			mucluc.sTTM,
			mucluc.sNG,
			mucluc.sTNG,
			mucluc.sXauNoiMa,
			mucluc.sDuToanChiTietToi,
			mucluc.bHangCha,
			mucluc.iID_MLNS as iID_MucLucNganSach,
			mucluc.sMoTa as sLoaiTroCap,
			duToan.fTienDuToanDuyet,
			(
			isnull(duLieuQuyTruoc.fTienCNVCQP_DeNghi, 0) + isnull(duLieuQuyTruoc.fTienHSQBS_DeNghi, 0) + isnull(duLieuQuyTruoc.fTienLDHD_DeNghi, 0) + isnull(duLieuQuyTruoc.fTienQNCN_DeNghi, 0) + isnull(duLieuQuyTruoc.fTienSQ_DeNghi, 0) 
			) fTienLuyKeCuoiQuyTruoc,
			(
				isnull(duLieuQuyTruoc.iSoCNVCQP_DeNghi, 0) + isnull(duLieuQuyTruoc.iSoHSQBS_DeNghi, 0) + isnull(duLieuQuyTruoc.iSoLDHD_DeNghi, 0) + isnull(duLieuQuyTruoc.iSoQNCN_DeNghi, 0) + isnull(duLieuQuyTruoc.iSoSQ_DeNghi, 0) 
			) iSoLuyKeCuoiQuyTruoc,
			chi_tiet.iSoSQ_DeNghi,
			chi_tiet.fTienSQ_DeNghi,
			chi_tiet.iSoQNCN_DeNghi,
			chi_tiet.fTienQNCN_DeNghi,
			chi_tiet.iSoCNVCQP_DeNghi,
			chi_tiet.fTienCNVCQP_DeNghi,
			chi_tiet.iSoHSQBS_DeNghi,
			chi_tiet.fTienHSQBS_DeNghi,
			chi_tiet.iTongSo_DeNghi,
			chi_tiet.fTongTien_DeNghi,
			chi_tiet.fTongTien_PheDuyet,
			chi_tiet.iSoLDHD_DeNghi,
			chi_tiet.fTienLDHD_DeNghi

		from #tblMucLucNganSach as mucluc
		left join #tblQuyetToanQuyChiTiet as chi_tiet on mucluc.sXauNoiMa = chi_tiet.sXauNoiMa
		left join #tempDuToan duToan on chi_tiet.sXauNoiMa=duToan.sXauNoiMa
		left join #tempDuLieuQuyTruoc duLieuQuyTruoc on chi_tiet.sXauNoiMa=duLieuQuyTruoc.sXauNoiMa
		order by mucluc.sXauNoiMa
	
	drop table #tblMucLucNganSach;
	drop table #tblQuyetToanQuyChiTiet;
	drop table #tempDuToan;
	drop table #tempDuLieuQuyTruoc;

end
;
GO
