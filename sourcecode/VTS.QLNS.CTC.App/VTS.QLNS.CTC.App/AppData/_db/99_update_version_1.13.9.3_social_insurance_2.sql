/****** Object:  StoredProcedure [dbo].[sp_qtc_quykinhphi_quanly_chungtu_chitiet_tao_tonghop]    Script Date: 1/25/2024 9:37:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_quykinhphi_quanly_chungtu_chitiet_tao_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_quykinhphi_quanly_chungtu_chitiet_tao_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_quykinhphi_quanly_chungtu_chi_tiet]    Script Date: 1/25/2024 9:37:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_quykinhphi_quanly_chungtu_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_quykinhphi_quanly_chungtu_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chiquyKCB_chitiet_clone]    Script Date: 1/25/2024 9:37:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_chiquyKCB_chitiet_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_chiquyKCB_chitiet_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqKPk_create_datafor_quaterbefore]    Script Date: 1/25/2024 9:37:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtcqKPk_create_datafor_quaterbefore]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtcqKPk_create_datafor_quaterbefore]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqKCB_create_datafor_quaterbefore]    Script Date: 1/25/2024 9:37:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtcqKCB_create_datafor_quaterbefore]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtcqKCB_create_datafor_quaterbefore]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqKCB_create_data_summary]    Script Date: 1/25/2024 9:37:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtcqKCB_create_data_summary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtcqKCB_create_data_summary]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqKCB_create_data_summary]    Script Date: 1/25/2024 9:37:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtcqKCB_create_data_summary]
@IdChungTu nvarchar(MAX),
@NguoiTao nvarchar(500),
@YearOfWork int,
@IdChungTuSummary nvarchar(MAX),
@MaDonVi nvarchar(500)
AS
BEGIN
SET NOCOUNT ON;

INSERT INTO BH_QTC_Quy_KCB_ChiTiet(
		ID_QTC_Quy_KCB_ChiTiet,
		iID_QTC_Quy_KCB,
		iID_MucLucNganSach,
		sNoiDung,
		dNgaySua,
		dNgayTao,
		sNguoiSua,
		sNguoiTao,
		fTien_DuToanNamTruocChuyenSang,
		fTien_DuToanGiaoNamNay,
		fTien_TongDuToanDuocGiao,
		fTienThucChi,
		fTienQuyetToanDaDuyet,
		fTienDeNghiQuyetToanQuyNay,
		fTienXacNhanQuyetToanQuyNay,
		sXauNoiMa,
		iNamLamViec,
		iID_MaDonVi
	)
SELECT 
	   NEWID(),
	   @IdChungTuSummary,
       iID_MucLucNganSach,
       sNoiDung,
	   GETDATE(),
	   GETDATE(),
	   @NguoiTao,
	   '',
	   SUM(fTien_DuToanNamTruocChuyenSang),
	   SUM(fTien_DuToanGiaoNamNay),
	   SUM(fTien_TongDuToanDuocGiao),
	   SUM(fTienThucChi),
	   SUM(fTienQuyetToanDaDuyet),
	   SUM(fTienDeNghiQuyetToanQuyNay),
	   SUM(fTienXacNhanQuyetToanQuyNay),
	   sXauNoiMa,
	   iNamLamViec,
	   @MaDonVi
FROM BH_QTC_Quy_KCB_ChiTiet
WHERE  iID_QTC_Quy_KCB IN
    (SELECT *
     FROM f_split(@IdChungTu))
group by iID_MucLucNganSach, sNoiDung, sXauNoiMa,iNamLamViec

UPDATE BH_QTC_Quy_KCB SET bDaTongHop = 1, iLoaiTongHop = 2 WHERE ID_QTC_Quy_KCB IN (SELECT * FROM f_split(@IdChungTu));
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqKCB_create_datafor_quaterbefore]    Script Date: 1/25/2024 9:37:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtcqKCB_create_datafor_quaterbefore]
@IdChungTu nvarchar(MAX),
@Username nvarchar(500),
@NamLamViec int,
@Quy int,
@MaDonVi nvarchar(MAX)
AS
BEGIN
SET NOCOUNT ON;

	INSERT INTO BH_QTC_Quy_KCB_ChiTiet
	( 
		ID_QTC_Quy_KCB_ChiTiet,
		iID_QTC_Quy_KCB,
		iID_MucLucNganSach,
		sNoiDung,
		dNgayTao,
		sNguoiTao,
		--fTien_DuToanGiaoNamNay,
		fTienQuyetToanDaDuyet,
		sXauNoiMa,
		iNamLamViec,
		iID_MaDonVi
	)
	SELECT 
		NEWID(),
		@IdChungTu,
		qtcn_ct.iID_MucLucNganSach,
		qtcn_ct.sNoiDung,
		GETDATE(),
		@Username,
		--Sum(qtcn_ct.fTien_DuToanGiaoNamNay),
		SUM(qtcn_ct.fTienXacNhanQuyetToanQuyNay),
		qtcn_ct.sXauNoiMa,
		qtcn_ct.iNamLamViec,
		qtcn_ct.iID_MaDonVi
	FROM BH_QTC_Quy_KCB_ChiTiet qtcn_ct
		inner join BH_QTC_Quy_KCB  as qtcn on qtcn_ct.iID_QTC_Quy_KCB = qtcn.ID_QTC_Quy_KCB
	WHERE qtcn.iQuyChungTu < @Quy
			AND qtcn.iID_MaDonVi = @MaDonVi
			AND qtcn.iNamChungTu = @NamLamViec
			--AND qtcn.bIsKhoa=1
		GROUP BY qtcn_ct.iID_MucLucNganSach, qtcn_ct.sNoiDung,qtcn_ct.sXauNoiMa,
		qtcn_ct.iNamLamViec,
		qtcn_ct.iID_MaDonVi
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqKPk_create_datafor_quaterbefore]    Script Date: 1/25/2024 9:37:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtcqKPk_create_datafor_quaterbefore]
@IdChungTu nvarchar(MAX),
@Username nvarchar(500),
@NamLamViec int,
@Quy int,
@LoaiChi uniqueidentifier,
@MaDonVi nvarchar(MAX)
AS
BEGIN
SET NOCOUNT ON;

	INSERT INTO BH_QTC_Quy_KPK_ChiTiet
	( 
		ID_QTC_Quy_KPK_ChiTiet,
		iID_QTC_Quy_KPK,
		iID_MucLucNganSach,
		sNoiDung,
		dNgayTao,
		sNguoiTao,
		fTienQuyetToanDaDuyet,
		iNamLamViec,
		iIDMaDonVi,
		sXauNoiMa
	)
	SELECT 
		NEWID(),
		@IdChungTu,
		qtcn_ct.iID_MucLucNganSach,
		qtcn_ct.sNoiDung,
		GETDATE(),
		@Username,
		SUM(qtcn_ct.fTienXacNhanQuyetToanQuyNay),
		qtcn_ct.iNamLamViec,
		qtcn_ct.iIDMaDonVi,
		qtcn_ct.sXauNoiMa
	FROM BH_QTC_Quy_KPK_ChiTiet qtcn_ct
		inner join BH_QTC_Quy_KPK  as qtcn on qtcn_ct.iID_QTC_Quy_KPK = qtcn.ID_QTC_Quy_KPK
	WHERE qtcn.iQuyChungTu < @Quy
			AND qtcn.iID_MaDonVi = @MaDonVi
			AND qtcn.iNamChungTu = @NamLamViec
			--AND qtcn.bIsKhoa=1
			AND iID_LoaiChi=@LoaiChi
		GROUP BY qtcn_ct.iID_MucLucNganSach, qtcn_ct.sNoiDung,qtcn_ct.iNamLamViec,qtcn_ct.iIDMaDonVi,qtcn_ct.sXauNoiMa
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chiquyKCB_chitiet_clone]    Script Date: 1/25/2024 9:37:43 AM ******/
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
	@INamLamViec int
AS
BEGIN
	DECLARE @iD uniqueidentifier='00000000-0000-0000-0000-000000000000';
	DECLARE @CountIndex INT;
	SELECT * into #tempLNS from f_split(@SLNS);
	SELECT * into #tempAgency from  f_split(@IIdMaDonVi);
	SELECT @CountIndex = COUNT(*) FROM 
									BH_QTC_QUY_KCB_Chitiet 
									WHERE iID_QTC_Quy_KCB =@IdChungTu

	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha,sDuToanChiTietToi
			into #tblNsMlns
	FROM BH_DM_MucLucNganSach 
	where iNamLamViec = @INamLamViec 

		-- lấy ra dữ liệu dự toán
	SELECT 
		  0 AS fTien_DuToanNamTruocChuyenSang,
		  SUM(fTienTuChi) AS fTien_DuToanGiaoNamNay,
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
          AND iNamChungTu = @INamLamViec
          AND iID_LoaiDanhMucChi = @IDLoaiChi
		  AND sMaLoaiChi=@SMaLoaiChi
		  AND bIsKhoa=1
          AND cast(dNgayQuyetDinh AS date) <= cast(@DNgayChungTu AS date))
     AND iID_MaDonVi in  (SELECT * FROM f_split(@IIdMaDonVi))-- donvi
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
		0 AS fTien_DuToanNamTruocChuyenSang,
		0 AS fTien_DuToanGiaoNamNay,
		SUM(fTien_TongDuToanDuocGiao) AS fTien_TongDuToanDuocGiao,
		SUM(fTienThucChi) AS fTienThucChi,
		SUM(fTienQuyetToanDaDuyet) AS fTienQuyetToanDaDuyet,
		SUM(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay,
		SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay,
		CT.iID_MaDonVi,
	    CTCT.iID_MucLucNganSach
		into #tblDataDaCap
	FROM
	BH_QTC_QUY_KCB CT
	INNER JOIN BH_QTC_QUY_KCB_Chitiet CTCT
	ON CT.ID_QTC_Quy_KCB=CTCT.iID_QTC_Quy_KCB
	WHERE CT.iNamChungTu = @INamLamViec
		  --AND i = @IDLoaiChi
          AND CAST(CT.dNgayChungTu AS DATE) <= CAST(@DNgayChungTu AS DATE)
		  AND CT.ID_QTC_Quy_KCB=@IdChungTu
		  AND CT.iID_MaDonVi IN (SELECT * FROM f_split(@IIdMaDonVi))
		  AND CT.iQuyChungTu<@iQuyChungTu

	GROUP BY  CT.iID_MaDonVi,CTCT.iID_MucLucNganSach

	SELECT * into #tblDataDuToan FROM #tblPhanBoDuToan

	SELECT sum(fTien_DuToanNamTruocChuyenSang) AS fTien_DuToanNamTruocChuyenSang
	, sum(fTien_DuToanGiaoNamNay) AS fTien_DuToanGiaoNamNay
	, sum(fTien_TongDuToanDuocGiao) fTien_TongDuToanDuocGiao
	, SUM(fTienThucChi) AS fTienThucChi
	, SUM(fTienQuyetToanDaDuyet) AS fTienQuyetToanDaDuyet
	, SUM(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay
	, SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay
	, iID_MaDonVi, iID_MucLucNganSach into #tblDataDaCapDuToan FROM (
		SELECT * FROM #tblDataDaCap
		UNION ALL
		SELECT * FROM #tblDataDuToan
		) data
	GROUP BY iID_MaDonVi, iID_MucLucNganSach;
	--select * from  #tblMlnsRoot
	SELECT mlns.iID_MLNS,
		   mlns.iID_MLNS_Cha,
		   mlns.sXauNoiMa,
		   fTien_DuToanNamTruocChuyenSang,
		   fTien_DuToanGiaoNamNay,
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

	SELECT iID_MLNS, iID_MLNS_Cha, iID_MaDonVi, sum(fTien_DuToanNamTruocChuyenSang) AS fTien_DuToanNamTruocChuyenSang
	, SUM(fTien_DuToanGiaoNamNay) AS fTien_DuToanGiaoNamNay
	, SUM(fTien_TongDuToanDuocGiao) as fTien_TongDuToanDuocGiao
	, SUM(fTienThucChi) as fTienThucChi
	, SUM(fTienQuyetToanDaDuyet) as fTienQuyetToanDaDuyet
	, SUM(fTienDeNghiQuyetToanQuyNay) as fTienDeNghiQuyetToanQuyNay
	, SUM(fTienXacNhanQuyetToanQuyNay) as fTienXacNhanQuyetToanQuyNay INTO #tblDaCapDuToanResult FROM (
		SELECT distinct T.iID_MLNS,  
			   T.iID_MLNS_Cha,
			   --isnull(T.iID_MaDonVi, T.iID_MaDonVi) as iID_MaDonVi,
			   T.iID_MaDonVi,
			   T.fTien_DuToanNamTruocChuyenSang,
			   T.fTien_DuToanGiaoNamNay,
			   T.fTien_TongDuToanDuocGiao,
			   T.fTienThucChi,
			   T.fTienQuyetToanDaDuyet,
			   T.fTienDeNghiQuyetToanQuyNay,
			   T.fTienXacNhanQuyetToanQuyNay

		FROM #tblDaCapDuToan T 
		WHERE T.fTien_DuToanGiaoNamNay <> 0) data
	GROUP BY iID_MLNS, iID_MLNS_Cha, iID_MaDonVi
	OPTION (maxrecursion 0)

		SELECT CTCT.* 
			INTO #TempChungTuQuyTruoc
			FROM 
				BH_QTC_QUY_KCB_Chitiet CTCT
			WHERE CTCT.iID_QTC_Quy_KCB IN
					(
						SELECT ID_QTC_Quy_KCB FROM  BH_QTC_QUY_KCB 
						WHERE iID_MaDonVi IN ( SELECT * FROM  f_split(@IIdMaDonVi))
									AND iNamChungTu=@INamLamViec
									AND iQuyChungTu=1
					)


	if @iQuyChungTu>1
	begin 
			select * into #tblQuyTruoc from #TempChungTuQuyTruoc where ( fTien_DuToanGiaoNamNay =0 or fTien_DuToanGiaoNamNay is null)
			update #tblQuyTruoc set iID_QTC_Quy_KCB=@IdChungTu
			
				insert into BH_QTC_QUY_KCB_Chitiet (ID_QTC_Quy_KCB_ChiTiet
				,iID_QTC_Quy_KCB
				,fTien_DuToanNamTruocChuyenSang
				,fTien_TongDuToanDuocGiao
				,iID_MucLucNganSach
				,sNoiDung
				,iNamLamViec
				,iID_MaDonVi
				,sXauNoiMa)
				select NEWID() as ID_QTC_Quy_KCB_ChiTiet,
						t1.iID_QTC_Quy_KCB,
						t1.fTien_DuToanNamTruocChuyenSang,
						t1.fTien_DuToanNamTruocChuyenSang as fTien_TongDuToanDuocGiao,
						t1.iID_MucLucNganSach,
						t1.sNoiDung,
						t1.iNamLamViec,
						t1.iID_MaDonVi,
						t1.sXauNoiMa
						from #tblQuyTruoc t1 
						where not exists (select 1
												from BH_QTC_QUY_KCB_Chitiet t2 
												where t2.iID_QTC_Quy_KCB=t1.iID_QTC_Quy_KCB
												and t2.iID_MucLucNganSach=t1.iID_MucLucNganSach
												and t2.iNamLamViec=t1.iNamLamViec
												and t2.iID_MaDonVi=t1.iID_MaDonVi
												)
			
			UPDATE  t2
				SET
				t2.fTien_DuToanNamTruocChuyenSang=t1.fTien_DuToanNamTruocChuyenSang,
				t2.fTien_TongDuToanDuocGiao= t1.fTien_TongDuToanDuocGiao
				FROM #tblQuyTruoc t1
				JOIN 
				BH_QTC_QUY_KCB_Chitiet t2
				ON t2.iID_QTC_Quy_KCB=t1.iID_QTC_Quy_KCB
					and t2.iID_MucLucNganSach=t1.iID_MucLucNganSach
					and t2.iNamLamViec=t1.iNamLamViec
					and t2.iID_MaDonVi=t1.iID_MaDonVi
	end
			
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
		isnull(tblQuyTruoc.fTien_DuToanNamTruocChuyenSang, 0) as FTienDuToanNamTruocChuyenSang,
		isnull(daCapDuToan.fTien_DuToanGiaoNamNay, 0) as FTienDuToanGiaoNamNay,
		isnull(ctct.fTien_TongDuToanDuocGiao, 0) as FTienTongDuToanDuocGiao,
		isnull(ctct.fTienThucChi, 0) as FTienThucChi,
		isnull(ctct.fTienQuyetToanDaDuyet, 0) as FTienQuyetToanDaDuyet,
		isnull(ctct.fTienDeNghiQuyetToanQuyNay, 0) as FTienDeNghiQuyetToanQuyNay,
		isnull(ctct.fTienXacNhanQuyetToanQuyNay, 0) as FTienXacNhanQuyetToanQuyNay,
		isnull(ctct.dNgayTao, getdate()) AS DNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS DNgaySua,
		ctct.sNguoiTao AS SNguoiTao,
		ctct.sNguoiSua AS SNguoiSua
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
	LEFT JOIN #tblDaCapDuToanResult daCapDuToan
	ON mlnsPhanBo.iID_MLNS = daCapDuToan.iID_MLNS
	LEFT JOIN #TempChungTuQuyTruoc tblQuyTruoc on ctct.iID_MucLucNganSach=tblQuyTruoc.iID_MucLucNganSach
	WHERE mlnsPhanBo.sLNS IN (SELECT * FROM #tempLNS)
	ORDER BY mlnsPhanBo.sXauNoiMa, mlnsPhanBo.iID_MaDonVi;
	--END
	------ Chứng từ chưa tồn tại
	--ELSE 
	--	BEGIN
	---- lấy dữ liệu đã cấp
	--SELECT  
	--	SUM(fTien_DuToanNamTruocChuyenSang) AS fTien_DuToanNamTruocChuyenSang,
	--	SUM(fTien_DuToanGiaoNamNay) AS fTien_DuToanGiaoNamNay,
	--	SUM(fTien_TongDuToanDuocGiao) AS fTien_TongDuToanDuocGiao,
	--	SUM(fTienThucChi) AS fTienThucChi,
	--	SUM(fTienQuyetToanDaDuyet) AS fTienQuyetToanDaDuyet,
	--	SUM(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay,
	--	SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay,
	--	CT.iID_MaDonVi,
	--	CTCT.iID_MucLucNganSach
	--	into #tblDataDaCapExist
	--FROM
	--BH_QTC_QUY_KCB CT
	--INNER JOIN BH_QTC_QUY_KCB_Chitiet CTCT
	--ON CT.ID_QTC_QUY_KCB=CTCT.iID_QTC_QUY_KCB
	--WHERE CT.iNamChungTu = @INamLamViec
	--	  --AND i = @IDLoaiChi
 --         AND CAST(CT.dNgayChungTu AS DATE) <= CAST(@DNgayChungTu AS DATE)
	--	  AND CT.ID_QTC_QUY_KCB=@IdChungTu
	--	  AND CT.iID_MaDonVi IN (SELECT * FROM f_split(@IIdMaDonVi))
	--	  AND CT.iQuyChungTu=@IQuyChungTu

	--GROUP BY  CT.iID_MaDonVi,CTCT.iID_MucLucNganSach


	--SELECT sum(fTien_DuToanNamTruocChuyenSang) AS fTien_DuToanNamTruocChuyenSang
	--, sum(fTien_DuToanGiaoNamNay) AS fTien_DuToanGiaoNamNay
	--, SUM(fTien_TongDuToanDuocGiao) AS fTien_TongDuToanDuocGiao
	--, SUM(fTienThucChi) AS fTienThucChi
	--, SUM(fTienQuyetToanDaDuyet) AS fTienQuyetToanDaDuyet
	--, SUM(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay
	--, SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay
	--, iID_MaDonVi, iID_MucLucNganSach into #tblDataDaCapDuToanExist FROM (
	--	SELECT * FROM #tblDataDaCapExist
	--	) data
	--GROUP BY iID_MaDonVi, iID_MucLucNganSach;
	----select * from  #tblMlnsRoot
	--SELECT mlns.iID_MLNS,
	--	   mlns.iID_MLNS_Cha,
	--	   mlns.sXauNoiMa,
	--	   fTien_DuToanNamTruocChuyenSang,
	--	   fTien_DuToanGiaoNamNay,
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
	--	, sum(fTien_DuToanNamTruocChuyenSang) AS fTien_DuToanNamTruocChuyenSang,
	--	sum(fTien_DuToanGiaoNamNay) AS fTien_DuToanGiaoNamNay
	--	, SUM(fTien_TongDuToanDuocGiao) as fTien_TongDuToanDuocGiao
	--	,SUM(fTienThucChi) AS fTienThucChi
	--	, SUM(fTienQuyetToanDaDuyet) AS fTienQuyetToanDaDuyet,
	--	 SUM(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay,
	--	SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay
	--	INTO #tblDaCapDuToanResultExist FROM (
	--	SELECT distinct T.iID_MLNS,  
	--		   T.iID_MLNS_Cha,
	--		   --isnull(T.iID_MaDonVi, T.iID_MaDonVi) as iID_MaDonVi,
	--		   T.iID_MaDonVi,
	--		   T.fTien_DuToanNamTruocChuyenSang,
	--		   T.fTien_DuToanGiaoNamNay,
	--		   T.fTien_TongDuToanDuocGiao,
	--		   T.fTienThucChi,
	--		   T.fTienQuyetToanDaDuyet,
	--		   T.fTienDeNghiQuyetToanQuyNay,
	--		   T.fTienXacNhanQuyetToanQuyNay
	--	FROM #tblDaCapDuToanExist T 
	--	WHERE  T.fTien_DuToanNamTruocChuyenSang <> 0 OR T.fTien_DuToanGiaoNamNay<>0 
	--		OR   T.fTien_TongDuToanDuocGiao<>0 OR T.fTienThucChi <>0 
	--		OR T.fTienQuyetToanDaDuyet<>0
	--		OR T.fTienDeNghiQuyetToanQuyNay<>0
	--		OR T.fTienXacNhanQuyetToanQuyNay<>0) data
	--GROUP BY iID_MLNS, iID_MLNS_Cha, iID_MaDonVi
	--OPTION (maxrecursion 0)

	---- Get data
	--SELECT
	--	isnull(ctct.ID_QTC_QUY_KCB_Chitiet, @iD) AS ID_QTC_QUY_KCB_Chitiet,
	--	@IdChungTu AS iID_QTC_QUY_KCB,
	--	mlnsPhanBo.iID_MLNS ,
	--	mlnsPhanBo.iID_MLNS_Cha ,
	--	mlnsPhanBo.iID_MLNS as iID_MucLucNganSach,
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
	--	mlnsPhanBo.bHangCha ,
	--	mlnsPhanBo.sDuToanChiTietToi,
	--	@INamLamViec AS INamLamViec,
	--	mlnsPhanBo.iID_MaDonVi AS IID_MaDonVi,
	--	mlnsPhanBo.sTenDonVi AS STenDonVi,
	--	isnull(daCapDuToan.fTien_DuToanNamTruocChuyenSang, 0) as FTienDuToanNamTruocChuyenSang,
	--	isnull(daCapDuToan.fTien_DuToanGiaoNamNay, 0) as FTienDuToanGiaoNamNay,
	--	isnull(daCapDuToan.fTien_TongDuToanDuocGiao, 0) as FTienTongDuToanDuocGiao,
	--	isnull(daCapDuToan.fTienThucChi, 0) as FTienThucChi,
	--	isnull(daCapDuToan.fTienQuyetToanDaDuyet, 0) as FTienQuyetToanDaDuyet,
	--	isnull(daCapDuToan.fTienDeNghiQuyetToanQuyNay, 0) as FTienDeNghiQuyetToanQuyNay,
	--	isnull(daCapDuToan.fTienXacNhanQuyetToanQuyNay, 0) as FTienXacNhanQuyetToanQuyNay,
	--	isnull(ctct.dNgayTao, getdate()) AS DNgayTao,
	--	isnull(ctct.dNgaySua, getdate()) AS DNgaySua,
	--	ctct.sNguoiTao AS SNguoiTao,
	--	ctct.sNguoiSua AS SNguoiSua
	--FROM #tblMlnsRoot AS mlnsPhanBo
	--LEFT JOIN
	--	(SELECT *
	--		FROM 
	--			BH_QTC_QUY_KCB_Chitiet
	--		WHERE 
	--	 		iID_QTC_QUY_KCB = @IdChungTu
	--	) ctct
	--ON mlnsPhanBo.iID_MLNS = ctct.iID_MucLucNganSach --and mlnsPhanBo.iID_MaDonVi = ctct.iID_MaDonVi
	--LEFT JOIN BH_QTC_QUY_KCB ct ON ctct.iID_QTC_QUY_KCB = ct.ID_QTC_QUY_KCB
	--LEFT JOIN #tblDaCapDuToanResultExist daCapDuToan
	--ON mlnsPhanBo.iID_MLNS = daCapDuToan.iID_MLNS 
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
/****** Object:  StoredProcedure [dbo].[sp_qtc_quykinhphi_quanly_chungtu_chi_tiet]    Script Date: 1/25/2024 9:37:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_qtc_quykinhphi_quanly_chungtu_chi_tiet]
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
									BH_QTC_Quy_KinhPhiQuanLy_ChiTiet 
									WHERE iID_QTC_Quy_KinhPhiQuanLy =@VoucherId

	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha,sDuToanChiTietToi
			into #tblNsMlns
	FROM BH_DM_MucLucNganSach 
	where iNamLamViec = @YearOfWork 

		-- lấy ra dữ liệu dự toán
	SELECT 
		  SUM(fTienTuChi) AS fTienDuToanDuocGiao,
          0 AS fTienDeNghiQuyetToanQuyNay,
          0 AS fTienQuyetToanDaDuyet,
		  0 AS fTienThucChi,
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
		0 AS fTienDuToanDuocGiao,
		SUM(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay,
		SUM(fTienQuyetToanDaDuyet) AS fTienQuyetToanDaDuyet,
		SUM(fTienThucChi) AS fTienThucChi,
		SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay,
		CT.iID_MaDonVi,
	    CTCT.iID_MucLucNganSach
		into #tblDataDaCap
	FROM
	BH_QTC_Quy_KinhPhiQuanLy CT
	INNER JOIN BH_QTC_Quy_KinhPhiQuanLy_chiTiet CTCT
	ON CT.ID_QTC_Quy_KinhPhiQuanLy=CTCT.iID_QTC_Quy_KinhPhiQuanLy
	WHERE CT.iNamChungTu = @YearOfWork
		  --AND i = @iID_LoaiDanhMucChi
          AND CAST(CT.dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
		  AND CT.ID_QTC_Quy_KinhPhiQuanLy=@VoucherId
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

	SELECT sum(fTienDuToanDuocGiao) AS fTienDuToanDuocGiao, sum(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay, sum(fTienQuyetToanDaDuyet) fTienQuyetToanDaDuyet, SUM(fTienThucChi) AS fTienThucChi,SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay, iID_MaDonVi, iID_MucLucNganSach into #tblDataDaCapDuToan FROM (
		SELECT * FROM #tblDataDaCap
		UNION ALL
		SELECT * FROM #tblDataDuToan
		) data
	GROUP BY iID_MaDonVi, iID_MucLucNganSach;
	--select * from  #tblMlnsRoot
	SELECT mlns.iID_MLNS,
		   mlns.iID_MLNS_Cha,
		   mlns.sXauNoiMa,
		   fTienDuToanDuocGiao,
		   fTienDeNghiQuyetToanQuyNay,
		   fTienQuyetToanDaDuyet,
		   fTienThucChi,
		   fTienXacNhanQuyetToanQuyNay,
		   isnull(mlns.iID_MaDonVi, daCapDuToan.iID_MaDonVi) as iID_MaDonVi
		   INTO #tblDaCapDuToan
	FROM #tblMlnsRoot mlns
	LEFT JOIN
	  #tblDataDaCapDuToan daCapDuToan
	ON mlns.iID_MLNS = daCapDuToan.iID_MucLucNganSach and ((mlns.iID_MaDonVi is not null and mlns.iID_MaDonVi = daCapDuToan.iID_MaDonVi) or mlns.iID_MaDonVi is null)

	SELECT iID_MLNS, iID_MLNS_Cha, iID_MaDonVi, sum(fTienDuToanDuocGiao) AS fTienDuToanDuocGiao, sum(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay, SUM(fTienQuyetToanDaDuyet) as fTienQuyetToanDaDuyet,SUM(fTienThucChi) as fTienThucChi,SUM(fTienXacNhanQuyetToanQuyNay) as fTienXacNhanQuyetToanQuyNay INTO #tblDaCapDuToanResult FROM (
		SELECT distinct T.iID_MLNS,  
			   T.iID_MLNS_Cha,
			   --isnull(T.iID_MaDonVi, T.iID_MaDonVi) as iID_MaDonVi,
			   T.iID_MaDonVi,
			   T.fTienDuToanDuocGiao,
			   T.fTienDeNghiQuyetToanQuyNay,
			   T.fTienQuyetToanDaDuyet,
			   T.fTienThucChi,
			   T.fTienXacNhanQuyetToanQuyNay
		FROM #tblDaCapDuToan T 
		WHERE T.fTienDuToanDuocGiao <> 0) data
	GROUP BY iID_MLNS, iID_MLNS_Cha, iID_MaDonVi
	OPTION (maxrecursion 0)

	-- Get data
	SELECT
		isnull(ctct.ID_QTC_Quy_KinhPhiQuanLy_ChiTiet, @iD) AS ID_QTC_Quy_KinhPhiQuanLy_ChiTiet,
		@VoucherId AS iID_QTC_Quy_KinhPhiQuanLy,
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
		mlnsPhanBo.sTenDonVi AS STenDonVi,
		isnull(daCapDuToan.fTienDeNghiQuyetToanQuyNay, 0) as FTienDeNghiQuyetToanQuyNay,
		isnull(daCapDuToan.fTienDuToanDuocGiao, 0) as FTienDuToanDuocGiao,
		isnull(daCapDuToan.fTienQuyetToanDaDuyet, 0) as FTienQuyetToanDaDuyet,
		isnull(daCapDuToan.fTienThucChi, 0) as FTienThucChi,
		isnull(daCapDuToan.fTienXacNhanQuyetToanQuyNay, 0) as FTienXacNhanQuyetToanQuyNay,
		ctct.sGhiChu AS SGhiChu,
		isnull(ctct.dNgayTao, getdate()) AS DNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS DNgaySua,
		ctct.sNguoiTao AS SNguoiTao,
		ctct.sNguoiSua AS SNguoiSua
	FROM #tblMlnsRoot AS mlnsPhanBo
	LEFT JOIN
		(SELECT *
			FROM 
				BH_QTC_Quy_KinhPhiQuanLy_chiTiet
			WHERE 
		 		iID_QTC_Quy_KinhPhiQuanLy = @VoucherId
		) ctct
	ON mlnsPhanBo.iID_MLNS = ctct.iID_MucLucNganSach --and mlnsPhanBo.iID_MaDonVi = ctct.iID_MaDonVi
	LEFT JOIN BH_QTC_Quy_KinhPhiQuanLy ct ON ctct.iID_QTC_Quy_KinhPhiQuanLy = ct.ID_QTC_Quy_KinhPhiQuanLy
	LEFT JOIN #tblDaCapDuToanResult daCapDuToan
	ON mlnsPhanBo.iID_MLNS = daCapDuToan.iID_MLNS and daCapDuToan.iID_MaDonVi LIKE '%' + mlnsPhanBo.iID_MaDonVi + '%' 
	WHERE mlnsPhanBo.sLNS IN (SELECT * FROM #tempLNS)
	ORDER BY mlnsPhanBo.sXauNoiMa, mlnsPhanBo.iID_MaDonVi;
	END
	ELSE 
		BEGIN
	-- lấy dữ liệu đã cấp
	SELECT  
		SUM(fTienDuToanDuocGiao) AS fTienDuToanDuocGiao,
		SUM(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay,
		SUM(fTienQuyetToanDaDuyet) AS fTienQuyetToanDaDuyet,
		SUM(fTienThucChi) AS fTienThucChi,
		SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay,
		CT.iID_MaDonVi,
		CTCT.iID_MucLucNganSach
		into #tblDataDaCapExist
	FROM
	BH_QTC_Quy_KinhPhiQuanLy CT
	INNER JOIN BH_QTC_Quy_KinhPhiQuanLy_chiTiet CTCT
	ON CT.ID_QTC_Quy_KinhPhiQuanLy=CTCT.iID_QTC_Quy_KinhPhiQuanLy
	WHERE CT.iNamChungTu = @YearOfWork
		  --AND i = @iID_LoaiDanhMucChi
          AND CAST(CT.dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
		  AND CT.ID_QTC_Quy_KinhPhiQuanLy=@VoucherId
		  AND CT.iID_MaDonVi IN (SELECT * FROM f_split(@AgencyId))
		  AND CT.iQuyChungTu=@iQuyChungTu

	GROUP BY  CT.iID_MaDonVi,CTCT.iID_MucLucNganSach


	SELECT sum(fTienDuToanDuocGiao) AS fTienDuToanDuocGiao, sum(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay, SUM(fTienQuyetToanDaDuyet) AS fTienQuyetToanDaDuyet,SUM(fTienThucChi) AS fTienThucChi,SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay, iID_MaDonVi, iID_MucLucNganSach into #tblDataDaCapDuToanExist FROM (
		SELECT * FROM #tblDataDaCapExist
		) data
	GROUP BY iID_MaDonVi, iID_MucLucNganSach;
	--select * from  #tblMlnsRoot
	SELECT mlns.iID_MLNS,
		   mlns.iID_MLNS_Cha,
		   mlns.sXauNoiMa,
		   fTienDuToanDuocGiao,
		   fTienDeNghiQuyetToanQuyNay,
		   fTienQuyetToanDaDuyet,
		   fTienThucChi,
		   fTienXacNhanQuyetToanQuyNay,
		   isnull(mlns.iID_MaDonVi, daCapDuToan.iID_MaDonVi) as iID_MaDonVi
		   INTO #tblDaCapDuToanExist
	FROM #tblMlnsRoot mlns
	LEFT JOIN
	  #tblDataDaCapDuToanExist daCapDuToan
	ON mlns.iID_MLNS = daCapDuToan.iID_MucLucNganSach and ((mlns.iID_MaDonVi is not null and mlns.iID_MaDonVi = daCapDuToan.iID_MaDonVi) or mlns.iID_MaDonVi is null)

	SELECT iID_MLNS, iID_MLNS_Cha, iID_MaDonVi, sum(fTienDuToanDuocGiao) AS fTienDuToanDuocGiao, sum(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay, SUM(fTienQuyetToanDaDuyet) as fTienQuyetToanDaDuyet,SUM(fTienThucChi) AS fTienThucChi, SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay INTO #tblDaCapDuToanResultExist FROM (
		SELECT distinct T.iID_MLNS,  
			   T.iID_MLNS_Cha,
			   --isnull(T.iID_MaDonVi, T.iID_MaDonVi) as iID_MaDonVi,
			   T.iID_MaDonVi,
			   T.fTienDuToanDuocGiao,
			   T.fTienDeNghiQuyetToanQuyNay,
			   T.fTienQuyetToanDaDuyet,
			   T.fTienThucChi,
			   T.fTienXacNhanQuyetToanQuyNay
		FROM #tblDaCapDuToanExist T 
		WHERE  T.fTienDuToanDuocGiao <> 0 OR T.fTienDeNghiQuyetToanQuyNay<>0 OR   T.fTienQuyetToanDaDuyet<>0 OR T.fTienThucChi <>0 OR T.fTienXacNhanQuyetToanQuyNay<>0) data
	GROUP BY iID_MLNS, iID_MLNS_Cha, iID_MaDonVi
	OPTION (maxrecursion 0)

	-- Get data
	SELECT
		isnull(ctct.ID_QTC_Quy_KinhPhiQuanLy_ChiTiet, @iD) AS ID_QTC_Quy_KinhPhiQuanLy_ChiTiet,
		@VoucherId AS iID_QTC_Quy_KinhPhiQuanLy,
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
		mlnsPhanBo.sTenDonVi AS STenDonVi,
		isnull(daCapDuToan.fTienDeNghiQuyetToanQuyNay, 0) as fTienDeNghiQuyetToanQuyNay,
		isnull(daCapDuToan.fTienDuToanDuocGiao, 0) as fTienDuToanDuocGiao,
		isnull(daCapDuToan.fTienQuyetToanDaDuyet, 0) as fTienQuyetToanDaDuyet,
		isnull(daCapDuToan.fTienThucChi, 0) as fTienThucChi,
		isnull(daCapDuToan.fTienXacNhanQuyetToanQuyNay, 0) as fTienXacNhanQuyetToanQuyNay,
		ctct.sGhiChu AS SGhiChu,
		isnull(ctct.dNgayTao, getdate()) AS DNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS DNgaySua,
		ctct.sNguoiTao AS SNguoiTao,
		ctct.sNguoiSua AS SNguoiSua
	FROM #tblMlnsRoot AS mlnsPhanBo
	LEFT JOIN
		(SELECT *
			FROM 
				BH_QTC_Quy_KinhPhiQuanLy_chiTiet
			WHERE 
		 		iID_QTC_Quy_KinhPhiQuanLy = @VoucherId
		) ctct
	ON mlnsPhanBo.iID_MLNS = ctct.iID_MucLucNganSach --and mlnsPhanBo.iID_MaDonVi = ctct.iID_MaDonVi
	LEFT JOIN BH_QTC_Quy_KinhPhiQuanLy ct ON ctct.iID_QTC_Quy_KinhPhiQuanLy = ct.ID_QTC_Quy_KinhPhiQuanLy
	LEFT JOIN #tblDaCapDuToanResultExist daCapDuToan
	ON mlnsPhanBo.iID_MLNS = daCapDuToan.iID_MLNS 
	WHERE mlnsPhanBo.sLNS IN (SELECT * FROM #tempLNS)
	ORDER BY mlnsPhanBo.sXauNoiMa, mlnsPhanBo.iID_MaDonVi;
	END
END
;
;
;
;
;
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_quykinhphi_quanly_chungtu_chitiet_tao_tonghop]    Script Date: 1/25/2024 9:37:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Author:    <Author,,Name>
  -- Create date: <Create Date,,>
  -- Description:  <Description,,>
  -- =============================================
  CREATE PROCEDURE [dbo].[sp_qtc_quykinhphi_quanly_chungtu_chitiet_tao_tonghop] 
  @ListIdChungTuTongHop ntext, 
  @Nguoitao nvarchar(50),
  @IdChungTu nvarchar(100), 
  @NamLamViec int
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
  sum(fTienDuToanDuocGiao), 
  sum(fTienThucChi), 
  sum(fTienQuyetToanDaDuyet), 
  sum(fTienDeNghiQuyetToanQuyNay), 
  sum(fTienXacNhanQuyetToanQuyNay), 
  Null, 
  GETDATE(), 
  Null, 
  @Nguoitao 
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
  sNoiDung;
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
