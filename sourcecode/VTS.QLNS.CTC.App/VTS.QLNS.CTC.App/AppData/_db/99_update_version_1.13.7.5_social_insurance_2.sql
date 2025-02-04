/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_intheolyke_donvi]    Script Date: 12/28/2023 5:36:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_pbdtc_intheolyke_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_pbdtc_intheolyke_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_dtc_phanbo_getfor_dotnhan_donvi]    Script Date: 12/28/2023 5:36:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dtc_phanbo_getfor_dotnhan_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dtc_phanbo_getfor_dotnhan_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_dtc_phanbo_get_donvi]    Script Date: 12/28/2023 5:36:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dtc_phanbo_get_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dtc_phanbo_get_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chiquyKCB_chitiet_clone]    Script Date: 12/28/2023 5:36:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_chiquyKCB_chitiet_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_chiquyKCB_chitiet_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chinamKCB_chitiet]    Script Date: 12/28/2023 5:36:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_chinamKCB_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_chinamKCB_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_kcb]    Script Date: 12/28/2023 5:36:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_kcb]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_kcb]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_kcb]    Script Date: 12/28/2023 5:36:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_kcb]
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
			danhmuc.bHangCha
		into #tblMucLucNganSach
		from BH_DM_MucLucNganSach as danhmuc
		where danhmuc.iNamLamViec = @INamLamViec and danhmuc.sLNs  in (select * from dbo.splitstring(@SLN)) 
		
		
		
	---Lấy thông tin chi tiết chứng từ
		select 
			qtcn_ct.iID_MucLucNganSach,
			qtcn_ct.sNoiDung,
			Sum(qtcn_ct.fTien_DuToanNamTruocChuyenSang) as FTienDuToanNamTruocChuyenSang,
			Sum(qtcn_ct.fTien_DuToanGiaoNamNay) as FTienDuToanGiaoNamNay,
			Sum(qtcn_ct.fTien_TongDuToanDuocGiao) as FTienTongDuToanDuocGiao,
			sum(qtcn_ct.fTienThucChi) as FTienThucChi ,
			sum(qtcn_ct.fTienQuyetToanDaDuyet) as FTienQuyetToanDaDuyet ,
			sum(qtcn_ct.fTienDeNghiQuyetToanQuyNay) as FTienDeNghiQuyetToanQuyNay,
			sum(qtcn_ct.fTienXacNhanQuyetToanQuyNay) as FTienXacNhanQuyetToanQuyNay
		into #tblQuyetToanQuyChiTiet
		from BH_QTC_Quy_KCB_ChiTiet as qtcn_ct
		inner join BH_QTC_Quy_KCB  as qtcn on qtcn_ct.iID_QTC_Quy_KCB = qtcn.ID_QTC_Quy_KCB
		where qtcn.iID_MaDonVi in (select * from dbo.splitstring(@IdMaDonVi)) 
		and qtcn.iNamChungTu = @INamLamViec
		--and ((@IsTongHop = 0 and qtcn.bDaTongHop = 0 and qtcn.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  qtcn.sDSSoChungTuTongHop is not null))
		and qtcn.iQuyChungTu = @IQuy
		group by qtcn_ct.iID_MucLucNganSach,qtcn_ct.sNoiDung

		
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
			mucluc.bHangCha,
			mucluc.iID_MLNS as iID_MucLucNganSach,
			mucluc.sMoTa as sNoiDung,
			chi_tiet.FTienDuToanNamTruocChuyenSang,
			chi_tiet.FTienDuToanGiaoNamNay,
			chi_tiet.FTienTongDuToanDuocGiao,
			chi_tiet.fTienThucChi,
			chi_tiet.fTienQuyetToanDaDuyet,
			chi_tiet.fTienDeNghiQuyetToanQuyNay,
			chi_tiet.fTienXacNhanQuyetToanQuyNay
		from #tblMucLucNganSach as mucluc
		left join #tblQuyetToanQuyChiTiet as chi_tiet on mucluc.iID_MLNS = chi_tiet.iID_MucLucNganSach
		order by mucluc.sXauNoiMa
	
	drop table #tblMucLucNganSach;
	drop table #tblQuyetToanQuyChiTiet;
end


GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chinamKCB_chitiet]    Script Date: 12/28/2023 5:36:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_chinamKCB_chitiet]
@IdChungTu uniqueidentifier,
@Lns nvarchar(max),
@INamLamViec int,
@IsTongHop4Quy bit
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
			danhmuc.bHangCha
		into #tblMucLucNganSach
		from BH_DM_MucLucNganSach as danhmuc
		where danhmuc.iNamLamViec = @INamLamViec and danhmuc.sLNs in (select * from splitstring(@Lns))
				and danhmuc.iTrangThai=1
		
		
		
	---Lấy thông tin chi tiết chứng từ
		select 
			qtcn_ct.ID_QTC_Nam_KCB_QuanYDonVi_ChiTiet,
			qtcn_ct.iID_MucLucNganSach,
			qtcn_ct.sNoiDung,
			qtcn_ct.sXauNoiMa,
			qtcn_ct.iNamLamViec,
			qtcn_ct.iID_MaDonVi,
			qtcn_ct.fTien_DuToanNamTruocChuyenSang,
			qtcn_ct.fTien_DuToanGiaoNamNay,
			qtcn_ct.fTien_TongDuToanDuocGiao,
			qtcn_ct.fTien_ThucChi,
			(CASE  WHEN isnull(qtcn_ct.fTien_TongDuToanDuocGiao,0) > isnull(qtcn_ct.fTien_ThucChi,0) THEN isnull(qtcn_ct.fTien_TongDuToanDuocGiao,0) - isnull(qtcn_ct.fTien_ThucChi,0) ELSE 0 END ) as fTienThua,
			(CASE  WHEN isnull(qtcn_ct.fTien_ThucChi,0) > isnull(qtcn_ct.fTien_TongDuToanDuocGiao,0) THEN isnull(qtcn_ct.fTien_ThucChi,0) - isnull(qtcn_ct.fTien_TongDuToanDuocGiao,0) ELSE 0 END ) as fTienThieu
		into #tblQuyetToanNamChiTiet
		from BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet as qtcn_ct
		inner join BH_QTC_Nam_KCB_QuanYDonVi  as qtcn on qtcn_ct.iID_QTC_Nam_KCB_QuanYDonVi = qtcn.ID_QTC_Nam_KCB_QuanYDonVi
		where qtcn_ct.iID_QTC_Nam_KCB_QuanYDonVi = @IdChungTu;

		
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
		mucluc.bHangCha,
		chi_tiet.ID_QTC_Nam_KCB_QuanYDonVi_ChiTiet,
		mucluc.iID_MLNS as iID_MucLucNganSach,
		mucluc.sMoTa as sNoiDung,
		chi_tiet.iID_MaDonVi,
		chi_tiet.iNamLamViec,
		chi_tiet.fTien_DuToanNamTruocChuyenSang, 
		chi_tiet.fTien_DuToanGiaoNamNay,
		chi_tiet.fTien_TongDuToanDuocGiao,
		chi_tiet.fTien_ThucChi,
		chi_tiet.fTienThua,
		chi_tiet.fTienThieu
	from #tblMucLucNganSach as mucluc
	left join #tblQuyetToanNamChiTiet as chi_tiet on mucluc.iID_MLNS = chi_tiet.iID_MucLucNganSach
	order by mucluc.sXauNoiMa
	
	drop table #tblMucLucNganSach;
	drop table #tblQuyetToanNamChiTiet;
end
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chiquyKCB_chitiet_clone]    Script Date: 12/28/2023 5:36:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Doantc
-- Create date: 20/09/2023
-- Description:	Lấy danh sách hiển thị chứng từ quyết toán chi quy kinh phí khám chua ben quan y

-- =============================================
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

	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha
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

	IF @CountIndex=0
	BEGIN
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
		@INamLamViec AS INamLamViec,
		mlnsPhanBo.iID_MaDonVi AS IID_MaDonVi,
		mlnsPhanBo.sTenDonVi AS STenDonVi,
		isnull(daCapDuToan.fTien_DuToanNamTruocChuyenSang, 0) as FTienDuToanNamTruocChuyenSang,
		isnull(daCapDuToan.fTien_DuToanGiaoNamNay, 0) as FTienDuToanGiaoNamNay,
		isnull(daCapDuToan.fTien_TongDuToanDuocGiao, 0) as FTienTongDuToanDuocGiao,
		isnull(daCapDuToan.fTienThucChi, 0) as FTienThucChi,
		isnull(daCapDuToan.fTienQuyetToanDaDuyet, 0) as FTienQuyetToanDaDuyet,
		isnull(daCapDuToan.fTienDeNghiQuyetToanQuyNay, 0) as FTienDeNghiQuyetToanQuyNay,
		isnull(daCapDuToan.fTienXacNhanQuyetToanQuyNay, 0) as FTienXacNhanQuyetToanQuyNay,
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
	ON mlnsPhanBo.iID_MLNS = daCapDuToan.iID_MLNS and daCapDuToan.iID_MaDonVi LIKE '%' + mlnsPhanBo.iID_MaDonVi + '%' 
	WHERE mlnsPhanBo.sLNS IN (SELECT * FROM #tempLNS)
	ORDER BY mlnsPhanBo.sXauNoiMa, mlnsPhanBo.iID_MaDonVi;
	END
	---- Chứng từ chưa tồn tại
	ELSE 
		BEGIN
	-- lấy dữ liệu đã cấp
	SELECT  
		SUM(fTien_DuToanNamTruocChuyenSang) AS fTien_DuToanNamTruocChuyenSang,
		SUM(fTien_DuToanGiaoNamNay) AS fTien_DuToanGiaoNamNay,
		SUM(fTien_TongDuToanDuocGiao) AS fTien_TongDuToanDuocGiao,
		SUM(fTienThucChi) AS fTienThucChi,
		SUM(fTienQuyetToanDaDuyet) AS fTienQuyetToanDaDuyet,
		SUM(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay,
		SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay,
		CT.iID_MaDonVi,
		CTCT.iID_MucLucNganSach
		into #tblDataDaCapExist
	FROM
	BH_QTC_QUY_KCB CT
	INNER JOIN BH_QTC_QUY_KCB_Chitiet CTCT
	ON CT.ID_QTC_QUY_KCB=CTCT.iID_QTC_QUY_KCB
	WHERE CT.iNamChungTu = @INamLamViec
		  --AND i = @IDLoaiChi
          AND CAST(CT.dNgayChungTu AS DATE) <= CAST(@DNgayChungTu AS DATE)
		  AND CT.ID_QTC_QUY_KCB=@IdChungTu
		  AND CT.iID_MaDonVi IN (SELECT * FROM f_split(@IIdMaDonVi))
		  AND CT.iQuyChungTu=@IQuyChungTu

	GROUP BY  CT.iID_MaDonVi,CTCT.iID_MucLucNganSach


	SELECT sum(fTien_DuToanNamTruocChuyenSang) AS fTien_DuToanNamTruocChuyenSang
	, sum(fTien_DuToanGiaoNamNay) AS fTien_DuToanGiaoNamNay
	, SUM(fTien_TongDuToanDuocGiao) AS fTien_TongDuToanDuocGiao
	, SUM(fTienThucChi) AS fTienThucChi
	, SUM(fTienQuyetToanDaDuyet) AS fTienQuyetToanDaDuyet
	, SUM(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay
	, SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay
	, iID_MaDonVi, iID_MucLucNganSach into #tblDataDaCapDuToanExist FROM (
		SELECT * FROM #tblDataDaCapExist
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
		   INTO #tblDaCapDuToanExist
	FROM #tblMlnsRoot mlns
	LEFT JOIN
	  #tblDataDaCapDuToanExist daCapDuToan
	ON mlns.iID_MLNS = daCapDuToan.iID_MucLucNganSach and ((mlns.iID_MaDonVi is not null and mlns.iID_MaDonVi = daCapDuToan.iID_MaDonVi) or mlns.iID_MaDonVi is null)

	SELECT iID_MLNS, iID_MLNS_Cha, iID_MaDonVi
		, sum(fTien_DuToanNamTruocChuyenSang) AS fTien_DuToanNamTruocChuyenSang,
		sum(fTien_DuToanGiaoNamNay) AS fTien_DuToanGiaoNamNay
		, SUM(fTien_TongDuToanDuocGiao) as fTien_TongDuToanDuocGiao
		,SUM(fTienThucChi) AS fTienThucChi
		, SUM(fTienQuyetToanDaDuyet) AS fTienQuyetToanDaDuyet,
		 SUM(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay,
		SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay
		INTO #tblDaCapDuToanResultExist FROM (
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
		FROM #tblDaCapDuToanExist T 
		WHERE  T.fTien_DuToanNamTruocChuyenSang <> 0 OR T.fTien_DuToanGiaoNamNay<>0 
			OR   T.fTien_TongDuToanDuocGiao<>0 OR T.fTienThucChi <>0 
			OR T.fTienQuyetToanDaDuyet<>0
			OR T.fTienDeNghiQuyetToanQuyNay<>0
			OR T.fTienXacNhanQuyetToanQuyNay<>0) data
	GROUP BY iID_MLNS, iID_MLNS_Cha, iID_MaDonVi
	OPTION (maxrecursion 0)

	-- Get data
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
		@INamLamViec AS INamLamViec,
		mlnsPhanBo.iID_MaDonVi AS IID_MaDonVi,
		mlnsPhanBo.sTenDonVi AS STenDonVi,
		isnull(daCapDuToan.fTien_DuToanNamTruocChuyenSang, 0) as FTienDuToanNamTruocChuyenSang,
		isnull(daCapDuToan.fTien_DuToanGiaoNamNay, 0) as FTienDuToanGiaoNamNay,
		isnull(daCapDuToan.fTien_TongDuToanDuocGiao, 0) as FTienTongDuToanDuocGiao,
		isnull(daCapDuToan.fTienThucChi, 0) as FTienThucChi,
		isnull(daCapDuToan.fTienQuyetToanDaDuyet, 0) as FTienQuyetToanDaDuyet,
		isnull(daCapDuToan.fTienDeNghiQuyetToanQuyNay, 0) as FTienDeNghiQuyetToanQuyNay,
		isnull(daCapDuToan.fTienXacNhanQuyetToanQuyNay, 0) as FTienXacNhanQuyetToanQuyNay,
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
/****** Object:  StoredProcedure [dbo].[sp_dtc_phanbo_get_donvi]    Script Date: 12/28/2023 5:36:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dtc_phanbo_get_donvi]
@NamLamViec int,
@IDLoaiChi uniqueidentifier,
@MaLoaichi nvarchar(50),
@SoQuyetDinh nvarchar(50),
@DNgayQuyetDinh nvarchar(50)
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

FROM BH_DTC_PhanBoDuToanChi chungtu
LEFT JOIN DonVi donvi ON donvi.iID_MaDonVi in (SELECT * FROM splitstring(chungtu.sID_MaDonVi))
WHERE chungtu.iNamChungTu = @NamLamViec
  AND chungtu.sID_MaDonVi <> ''
  AND chungtu.sID_MaDonVi IS NOT NULL
  AND donvi.iNamLamViec = @NamLamViec
  AND chungtu.sSoQuyetDinh=@SoQuyetDinh
   AND CONVERT(nvarchar ,chungtu.dNgayQuyetDinh,103) = @DNgayQuyetDinh 
  --AND chungtu.iID_LoaiDanhMucChi=@IDLoaiChi
  AND chungtu.sMaLoaiChi=@MaLoaichi
  --AND chungtu.iLoaiTongHop=@LoaiChungTu
ORDER BY cast(chungtu.dNgayChungTu AS date) END
;
;


GO
/****** Object:  StoredProcedure [dbo].[sp_dtc_phanbo_getfor_dotnhan_donvi]    Script Date: 12/28/2023 5:36:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dtc_phanbo_getfor_dotnhan_donvi]
@NamLamViec int,
@IDLoaiChi uniqueidentifier,
@MaLoaichi nvarchar(50),
@SoQuyetDinh nvarchar(50),
@DNgayQuyetDinh nvarchar(50)
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

FROM BH_DTC_PhanBoDuToanChi chungtu
LEFT JOIN DonVi donvi ON donvi.iID_MaDonVi in (SELECT * FROM splitstring(chungtu.sID_MaDonVi))
WHERE chungtu.iNamChungTu = @NamLamViec
  AND chungtu.sID_MaDonVi <> ''
  AND chungtu.sID_MaDonVi IS NOT NULL
  AND donvi.iNamLamViec = @NamLamViec
  AND chungtu.sSoQuyetDinh=@SoQuyetDinh
  AND CONVERT(nvarchar ,chungtu.dNgayQuyetDinh,103) <= @DNgayQuyetDinh 
  --AND chungtu.iID_LoaiDanhMucChi=@IDLoaiChi
  AND chungtu.sMaLoaiChi=@MaLoaichi
  --AND chungtu.iLoaiTongHop=@LoaiChungTu
ORDER BY cast(chungtu.dNgayChungTu AS date) END
;
;



GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_intheolyke_donvi]    Script Date: 12/28/2023 5:36:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_rpt_dt_pbdtc_intheolyke_donvi]	
	@INamLamViec int ,
	@IdMaDonVi nvarchar(MAX) ,
	@IDLoaiChi nvarchar(MAX),
	@MaLoaiChi nvarchar(MAX) ,
	@SNgayQuyetDinh nvarchar(MAX),
	@SoQuyetdinh nvarchar(MAX),
	@Donvitinh int 
AS
BEGIN

select ctct.* into #tempall from BH_DTC_PhanBoDuToanChi ct
right join BH_DTC_PhanBoDuToanChi_ChiTiet ctct on ct.ID=ctct.iID_DTC_PhanBoDuToanChi
where ct.iNamChungTu=2023
and ct.iID_LoaiDanhMucChi=@IDLoaiChi
and ct.sMaLoaiChi=@MaLoaiChi
and cast(ct.dNgayQuyetDinh as date) <= @SNgayQuyetDinh
and ct.sSoQuyetDinh=@SoQuyetdinh
and ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi))

select 
	 CAST(ROW_NUMBER() OVER (ORDER BY dv.sTenDonVi) AS VARCHAR(6)) STT
	, dv.sTenDonVi
	, dv.iID_MaDonVi
	, 0 fTienTroCapOmDau
	, 0 fTienTroCapThaiSan
	, 0 fTienTroCapTaiNan
	, 0 fTienTroCapHuuTri
	, 0 fTienTroCapPhucVien
	, 0 fTienTroCapXuatNgu
	, 0 fTienTroCapThoiViec
	, 0 fTienTroCapTuTuat
	, 1 IsHangCha
	, 0 RowNumber
	into #temp1
from 
#tempall ctct
left join DonVi dv on ctct.iID_MaDonVi= dv.iID_MaDonVi
where ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi))
and
dv.iNamLamViec=@INamLamViec
and
ctct.iNamLamViec=@INamLamViec
group by  dv.iID_MaDonVi, dv.sTenDonVi

select
	null as STT
	, N'Khối dự toán' as sTenDonVi
	, ctct.iID_MaDonVi
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0001%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapOmDau
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0002%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapThaiSan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0003%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapTaiNan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0004%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapHuuTri
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0005%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapPhucVien
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0006%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapXuatNgu
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0007%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapThoiViec
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0008%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapTuTuat
	, 0 IsHangCha
	, 1 RowNumber
	into #temp2
from #tempall ctct
where ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi)) and ctct.iNamLamViec=@INamLamViec
AND(
ctct.sXauNoiMa like '9010001-010-011-0002%'
OR ctct.sXauNoiMa like '9010001-010-011-0001%'
OR ctct.sXauNoiMa like '9010001-010-011-0003%'
OR ctct.sXauNoiMa like '9010001-010-011-0004%'
OR ctct.sXauNoiMa like '9010001-010-011-0005%'
OR ctct.sXauNoiMa like '9010001-010-011-0006%'
OR ctct.sXauNoiMa like '9010001-010-011-0007%'
OR ctct.sXauNoiMa like '9010001-010-011-0008%'
)
group by ctct.iID_DonVi, ctct.iID_MaDonVi 

select
	null as STT
	, N'Khối hạch toán' as sTenDonVi
	, ctct.iID_MaDonVi
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0001%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapOmDau
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0002%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapThaiSan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0003%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapTaiNan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0004%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapHuuTri
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0005%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapPhucVien
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0006%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapXuatNgu
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0007%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapThoiViec
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0008%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapTuTuat
	, 0 IsHangCha
	, 2 RowNumber
	into #temp3
from #tempall ctct
where ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi)) and ctct.iNamLamViec=@INamLamViec
AND (

ctct.sXauNoiMa like '9010002-010-011-0001%'
OR ctct.sXauNoiMa like '9010002-010-011-0002%'
OR ctct.sXauNoiMa like '9010002-010-011-0003%'
OR ctct.sXauNoiMa like '9010002-010-011-0004%'
OR ctct.sXauNoiMa like '9010002-010-011-0005%'
OR ctct.sXauNoiMa like '9010002-010-011-0006%'
OR ctct.sXauNoiMa like '9010002-010-011-0007%'
OR ctct.sXauNoiMa like '9010002-010-011-0008%'
)
group by ctct.iID_DonVi, ctct.iID_MaDonVi

SELECT  NEWID() as Id,* INTO #tempRESULT
from
(
	SELECT * FROM #temp1
	UNION ALL 
	SELECT * FROM #temp2
	UNION ALL 
	SELECT * FROM #temp3
) tempRESULT;

With #tree(id,sTenDonVi,iID_MaDonVi, STT, IsHangCha,fTienTroCapOmDau,fTienTroCapThaiSan,fTienTroCapTaiNan,fTienTroCapHuuTri,fTienTroCapPhucVien,fTienTroCapXuatNgu, fTienTroCapThoiViec, fTienTroCapTuTuat , RowNumber,position, iIdParent)
as
(
	SELECT  id,sTenDonVi,iID_MaDonVi, STT,IsHangCha,fTienTroCapOmDau,fTienTroCapThaiSan,fTienTroCapTaiNan,fTienTroCapHuuTri,fTienTroCapPhucVien,fTienTroCapXuatNgu, fTienTroCapThoiViec, fTienTroCapTuTuat,RowNumber,
						CAST(ROW_NUMBER() OVER(ORDER BY STT) AS NVARCHAR(MAX)) AS position, NEWID()
	FROM #tempRESULT
	WHERE  IsHangCha = 1 
	UNION ALL 
	SELECT chil.Id, chil.sTenDonVi, chil.iID_MaDonVi,   chil.STT, chil.IsHangCha,chil.fTienTroCapOmDau,chil.fTienTroCapThaiSan,chil.fTienTroCapTaiNan,chil.fTienTroCapHuuTri,chil.fTienTroCapPhucVien,chil.fTienTroCapXuatNgu, chil.fTienTroCapThoiViec, chil.fTienTroCapTuTuat,chil.RowNumber,
	CONCAT(pr.position,'.',CAST(ROW_NUMBER() OVER(ORDER BY chil.iID_MaDonVi) AS NVARCHAR(MAX))) AS position, pr.id
	FROM #tempRESULT chil
	INNER JOIN #tree pr ON pr.iID_MaDonVi = chil.iID_MaDonVi AND  pr.IsHangCha = 1
	WHERE chil.STT IS NULL AND chil.IsHangCha = 0 

	
)
select *, 
		cast('/' + replace(position, '.', '/') + '/' as hierarchyid) AS sort
		INTO #result 
from #tree 
--temp table sum chill
SELECT  iIdParent
		,SUM(fTienTroCapOmDau) fTienTroCapOmDau
		,SUM(fTienTroCapThaiSan) fTienTroCapThaiSan
		,SUM(fTienTroCapTaiNan) fTienTroCapTaiNan
		,SUM(fTienTroCapHuuTri) fTienTroCapHuuTri
		,SUM(fTienTroCapPhucVien) fTienTroCapPhucVien
		,SUM(fTienTroCapXuatNgu) fTienTroCapXuatNgu
		,SUM(fTienTroCapThoiViec) fTienTroCapThoiViec
		,SUM(fTienTroCapTuTuat) fTienTroCapTuTuat
		INTO #tempSumParent
FROM #result
WHERE IsHangCha = 0
Group by iIdParent

Update #result
SET iIdParent = null
where IsHangCha = 1;

select id,sTenDonVi,iID_MaDonVi, STT,IsHangCha,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapOmDau ELSE rs.fTienTroCapOmDau END) fTienTroCapOmDau,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapThaiSan ELSE rs.fTienTroCapThaiSan END) fTienTroCapThaiSan,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapTaiNan ELSE rs.fTienTroCapTaiNan END) fTienTroCapTaiNan,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapHuuTri ELSE rs.fTienTroCapHuuTri END) fTienTroCapHuuTri,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapPhucVien ELSE rs.fTienTroCapPhucVien END) fTienTroCapPhucVien,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapXuatNgu ELSE rs.fTienTroCapXuatNgu END) fTienTroCapXuatNgu,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapThoiViec ELSE rs.fTienTroCapThoiViec END) fTienTroCapThoiViec,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapTuTuat ELSE rs.fTienTroCapTuTuat END) fTienTroCapTuTuat,
			RowNumber,
			rs.position
from #result rs
LEFT JOIN #tempSumParent su ON rs.id = su.iIdParent
order by sort;

DROP TABLE #tempall
DROP TABLE #temp1
DROP TABLE #temp2
DROP TABLE #temp3
DROP TABLE #tempRESULT
DROP TABLE #result
DROP TABLE #tempSumParent

END
;
GO
