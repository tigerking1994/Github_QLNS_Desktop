/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_gttrocap_huutri_phucvien_xuatngu_thoiviec_tutuat]    Script Date: 7/12/2024 8:13:12 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_qtcq_gttrocap_huutri_phucvien_xuatngu_thoiviec_tutuat]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_qtcq_gttrocap_huutri_phucvien_xuatngu_thoiviec_tutuat]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Khoi_Detail]    Script Date: 7/12/2024 8:13:12 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Khoi_Detail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Khoi_Detail]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Khoi]    Script Date: 7/12/2024 8:13:12 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Khoi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Khoi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT]    Script Date: 7/12/2024 8:13:12 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_khoi]    Script Date: 7/12/2024 8:13:12 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_khoi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_khoi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_bhxh]    Script Date: 7/12/2024 8:13:12 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_khoi]    Script Date: 7/12/2024 8:13:12 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_khoi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_khoi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_bhxh]    Script Date: 7/12/2024 8:13:12 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_khoi]    Script Date: 7/12/2024 8:13:12 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_khoi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_khoi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_bhxh]    Script Date: 7/12/2024 8:13:12 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_kcb]    Script Date: 7/12/2024 8:13:12 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_kcb]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_kcb]
GO
/****** Object:  StoredProcedure [dbo].[bh_qtcq_ctct_gttrocap_index]    Script Date: 7/12/2024 8:13:12 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[bh_qtcq_ctct_gttrocap_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[bh_qtcq_ctct_gttrocap_index]
GO
/****** Object:  StoredProcedure [dbo].[bh_qtcq_ctct_gttrocap_index]    Script Date: 7/12/2024 8:13:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Doantc
-- Create date: 13/06/2023
-- Description:	Lấy danh sách hiển thị giai thich 
-- =============================================
CREATE PROCEDURE [dbo].[bh_qtcq_ctct_gttrocap_index]
@YearWork int,
@IdQTCQuyCheDoBHXH nvarchar(max),
@SXauNoiMa nvarchar(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
	DISTINCT 
		 gttc.iiD_QTC_Quy_CTCT_GiaiThichTroCap
		, gttc.iID_QTC_Quy_ChungTu as IID_QTC_Quy_ChungTu
		, gttc.iNamLamViec
		, gttc.iQuy
		, gttc.sNguoiSua
		, gttc.sNguoiTao
		, gttc.dNgaySua
		, gttc.dNgayTao
		, gttc.iSoNgayHuong
		, gttc.sMa_Hieu_Can_Bo AS SMaHieuCanBo
		, gttc.iiD_MaPhanHo AS  ID_MaPhanHo
		, gttc.sMaCapBac
		, gttc.sTenCapBac
		, gttc.fSoTien fSoTien
		, gttc.iiD_MaPhanHo
		, gttc.sSoQuyetDinh
		, gttc.sTenCanBo
		, gttc.sXauNoiMa
		, gttc.dNgayQuyetDinh
		, gttc.iiD_MaDonVi AS ID_MaDonVi
		, gttc.sSoSoBHXH
		, gttc.dTuNgay
		, gttc.dDenNgay
		, gttc.fTienLuongThangDongBHXH
		,gttc.sTenPhanHo
		,gttc.iSoNgayTruyLinh
		,gttc.fTienTruyLinh

		-- Tong dự toán todo
	FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gttc
	WHERE gttc.iNamLamViec=@YearWork
			AND gttc.sXauNoiMa=@SXauNoiMa
			AND gttc.iID_QTC_Quy_ChungTu in (select * from splitstring(@IdQTCQuyCheDoBHXH))
	
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_kcb]    Script Date: 7/12/2024 8:13:12 AM ******/
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
	DECLARE @fSoThamDinh INT;
	SELECT @fSoThamDinh = sum(fSoThamDinh)
	FROM BH_ThamDinhQuyetToan_ChungTuChiTiet 
	WHERE iNamLamViec = @INamLamViec - 1 and iMa = 225 and  iID_MaDonVi IN (SELECT * FROM f_split(@IdMaDonVi))
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
			danhmuc.bHangCha,
			danhmuc.bHangChaDuToan
		into #tblMucLucNganSach
		from BH_DM_MucLucNganSach as danhmuc
		where danhmuc.iNamLamViec = @INamLamViec and danhmuc.sLNs  in (select * from dbo.splitstring(@SLN)) 
		and danhmuc.iTrangThai = 1
		
		
		
	---Lấy thông tin chi tiết chứng từ
		select 
			qtcn_ct.sXauNoiMa,
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
		group by qtcn_ct.sXauNoiMa,qtcn_ct.sNoiDung

   		-- lấy ra dữ liệu dự toán
	SELECT 
		  ROUND(SUM(fTienTuChi),0) AS FTienDuToanGiaoNamNay,
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
          --AND cast(dNgayQuyetDinh AS date) <= cast(@DNgayChungTu AS date)) 
		  )
     AND iID_MaDonVi in  (SELECT * FROM f_split(@IdMaDonVi))-- donvi
   GROUP BY sXauNoiMa
   --- Get nhan phan bo tren giao
   	SELECT 
		  ROUND(SUM(fTienTuChi),0) AS FTienDuToanGiaoNamNay,
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
          --AND cast(dNgayQuyetDinh AS date) <= cast(@DNgayChungTu AS date)) 
		  )
     AND iID_MaDonVi in  (SELECT * FROM f_split(@IdMaDonVi))-- donvi
   GROUP BY sXauNoiMa

   SELECT SUM(CTCT.fTien_DuToanNamTruocChuyenSang)  fTien_DuToanNamTruocChuyenSang,
					CTCT.sXauNoiMa
		INTO #TempChungTuQuyTruoc
	FROM BH_QTC_QUY_KCB_Chitiet CTCT
		WHERE CTCT.iID_QTC_Quy_KCB IN
	(
		SELECT ID_QTC_Quy_KCB FROM  BH_QTC_QUY_KCB 
		WHERE iID_MaDonVi IN ( SELECT * FROM  f_split(@IdMaDonVi))
					AND iNamChungTu=@INamLamViec
					AND iQuyChungTu=1
	)
	group by CTCT.sXauNoiMa

	-- lay du lieu da quyet toan 
	SELECT  
		SUM(FTienDeNghiQuyetToanQuyNay) AS fTienQuyetToanDaDuyet,
		CTCT.sXauNoiMa
		into #TemptblTienDaDuyet
	FROM
	BH_QTC_QUY_KCB CT
	INNER JOIN BH_QTC_QUY_KCB_Chitiet CTCT
	ON CT.ID_QTC_Quy_KCB=CTCT.iID_QTC_Quy_KCB
	WHERE CT.iNamChungTu = @INamLamViec
		  --AND CAST(CT.dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
		  AND CT.iID_MaDonVi IN (SELECT * FROM f_split(@IdMaDonVi))
		  --AND CT.bIsKhoa=1
		  AND CT.iQuyChungTu<@IQuy
	GROUP BY  CTCT.sXauNoiMa

   -- chung tu thuong
		if @IsTongHop=1
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
			mucluc.bHangChaDuToan,
			mucluc.iID_MLNS as iID_MucLucNganSach,
			mucluc.sMoTa as sNoiDung,
			CASE WHEN mucluc.sXauNoiMa = @SLN THEN ROUND(@fSoThamDinh/@Donvitinh,0)  ELSE 0 END as FTienDuToanNamTruocChuyenSang,
			ROUND(dt.FTienDuToanGiaoNamNay /@Donvitinh,0) FTienDuToanGiaoNamNay,
			ROUND(chi_tiet.FTienTongDuToanDuocGiao /@Donvitinh,0) FTienTongDuToanDuocGiao,
			ROUND((isnull(chi_tiet.fTienThucChi,0) + isnull(tienDuyet.fTienQuyetToanDaDuyet,0)) /@Donvitinh,0) as fTienThucChi,
			ROUND(tienDuyet.fTienQuyetToanDaDuyet /@Donvitinh,0)  fTienQuyetToanDaDuyet,
			ROUND(chi_tiet.fTienDeNghiQuyetToanQuyNay/ @Donvitinh,0) fTienDeNghiQuyetToanQuyNay,
			ROUND(chi_tiet.fTienXacNhanQuyetToanQuyNay/ @Donvitinh,0) fTienXacNhanQuyetToanQuyNay
		from #tblMucLucNganSach as mucluc
		left join #tblQuyetToanQuyChiTiet as chi_tiet on mucluc.sXauNoiMa = chi_tiet.sXauNoiMa
		left join #tblPhanBoDuToan as dt on mucluc.sXauNoiMa=dt.sXauNoiMa
		left join #TempChungTuQuyTruoc as quy_truoc on mucluc.sXauNoiMa=quy_truoc.sXauNoiMa
		left join #TemptblTienDaDuyet tienDuyet on mucluc.sXauNoiMa=tienDuyet.sXauNoiMa
		order by mucluc.sXauNoiMa
	else
		---- chung tu tong hop
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
			mucluc.bHangChaDuToan,
			mucluc.iID_MLNS as iID_MucLucNganSach,
			mucluc.sMoTa as sNoiDung,
			CASE WHEN mucluc.sXauNoiMa = @SLN THEN ROUND(@fSoThamDinh/@Donvitinh,0)  ELSE 0 END as FTienDuToanNamTruocChuyenSang,
			--quy_truoc.fTien_DuToanNamTruocChuyenSang as FTienDuToanNamTruocChuyenSang,
			ROUND(dt.FTienDuToanGiaoNamNay/@Donvitinh,0) FTienDuToanGiaoNamNay,
			ROUND(chi_tiet.FTienTongDuToanDuocGiao /@Donvitinh,0) FTienTongDuToanDuocGiao,
			ROUND((isnull(chi_tiet.fTienThucChi,0) + isnull(tienDuyet.fTienQuyetToanDaDuyet,0))/@Donvitinh,0) fTienThucChi,
			ROUND(tienDuyet.fTienQuyetToanDaDuyet/@Donvitinh,0) fTienQuyetToanDaDuyet,
			ROUND(chi_tiet.fTienDeNghiQuyetToanQuyNay/@Donvitinh,0) fTienDeNghiQuyetToanQuyNay,
			ROUND(chi_tiet.fTienXacNhanQuyetToanQuyNay/@Donvitinh,0) fTienXacNhanQuyetToanQuyNay
		from #tblMucLucNganSach as mucluc
		left join #tblQuyetToanQuyChiTiet as chi_tiet on mucluc.sXauNoiMa = chi_tiet.sXauNoiMa
		left join #tblNhanPhanBoTrenGiao as dt on mucluc.sXauNoiMa=dt.sXauNoiMa
		left join #TempChungTuQuyTruoc as quy_truoc on mucluc.sXauNoiMa=quy_truoc.sXauNoiMa
		left join #TemptblTienDaDuyet tienDuyet on mucluc.sXauNoiMa=tienDuyet.sXauNoiMa
		order by mucluc.sXauNoiMa
end
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_bhxh]    Script Date: 7/12/2024 8:13:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_bhxh]
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

		---Lấy danh sách đơn vị được chọn
		select 
			iID_DonVi,
			iID_MaDonVi,
			sTenDonVi
		into #tblDonvi
		from DonVi where iNamLamViec = @INamLamViec and iID_MaDonVi  in (select * from dbo.splitstring(@IdMaDonVi)) 


		---Lấy thông tin chi tiết chứng từ
		select
			ROW_NUMBER() OVER(ORDER BY as_qtct_1.iID_MaDonVi ASC) AS sTT,
			as_qtct_1.iID_MaDonVi,
			dv.sTenDonVi,
			Sum(iSoNgayDuoi14BenhDaiNgay) as iSoNgayDuoi14BenhDaiNgay,
			Sum(fSoTienDuoi14BenhDaiNgay) as fSoTienDuoi14BenhDaiNgay,
			Sum(iSoNgayTren14BenhDaiNgay) as iSoNgayTren14BenhDaiNgay,
			Sum(fSoTienTren14BenhDaiNgay) as fSoTienTren14BenhDaiNgay,
			Sum(iSoNgayDuoi14OmKhac) as iSoNgayDuoi14OmKhac,
			Sum(fSoTienDuoi14OmKhac) as fSoTienDuoi14OmKhac,
			Sum(iSoNgayTren14OmKhac) as iSoNgayTren14OmKhac,
			Sum(fSoTienTren14OmKhac) as fSoTienTren14OmKhac,
			Sum(iSoNgayConOm) as iSoNgayConOm,
			Sum(fSoTienConOm) as fSoTienConOm,
			Sum(iSoNgayPHSK) as iSoNgayPHSK,
			Sum(fSoTienPHSK) as fSoTienPHSK,
			isnull(Sum(fSoTienDuoi14BenhDaiNgay),0) + isnull(Sum(fSoTienTren14BenhDaiNgay),0) + isnull(Sum(fSoTienDuoi14OmKhac),0) + isnull(Sum(fSoTienTren14OmKhac),0) + isnull(Sum(fSoTienConOm),0)
			+ isnull(Sum(fSoTienPHSK),0) as fTongTien
			
		from
			(
			select
				as_qtct.iID_MaDonVi,
				case when as_qtct.sXauNoiMa = '010-011-0001-0001-0001-01-01' then iTongSo_DeNghi else 0 end iSoNgayDuoi14BenhDaiNgay,
				case when as_qtct.sXauNoiMa = '010-011-0001-0001-0001-01-01' then fTongTien_DeNghi else 0 end fSoTienDuoi14BenhDaiNgay,
				case when as_qtct.sXauNoiMa = '010-011-0001-0001-0001-01-02' then iTongSo_DeNghi else 0 end iSoNgayTren14BenhDaiNgay,
				case when as_qtct.sXauNoiMa = '010-011-0001-0001-0001-01-02' then fTongTien_DeNghi else 0 end fSoTienTren14BenhDaiNgay,
				case when as_qtct.sXauNoiMa = '010-011-0001-0001-0001-02-01' then iTongSo_DeNghi else 0 end iSoNgayDuoi14OmKhac,
				case when as_qtct.sXauNoiMa = '010-011-0001-0001-0001-02-01' then fTongTien_DeNghi else 0 end fSoTienDuoi14OmKhac,
				case when as_qtct.sXauNoiMa = '010-011-0001-0001-0001-02-02' then iTongSo_DeNghi else 0 end iSoNgayTren14OmKhac,
				case when as_qtct.sXauNoiMa = '010-011-0001-0001-0001-02-02' then fTongTien_DeNghi else 0 end fSoTienTren14OmKhac,
				case when as_qtct.sXauNoiMa = '010-011-0001-0002' then iTongSo_DeNghi else 0 end iSoNgayConOm,
				case when as_qtct.sXauNoiMa = '010-011-0001-0002' then fTongTien_DeNghi else 0 end fSoTienConOm,
				case when as_qtct.sXauNoiMa = '010-011-0001-0003' then iTongSo_DeNghi else 0 end iSoNgayPHSK,
				case when as_qtct.sXauNoiMa = '010-011-0001-0003' then fTongTien_DeNghi else 0 end fSoTienPHSK
				--case when as_qtct.sXauNoiMa = '010-011-0001-0001-01-01-01' then iTongSo_DeNghi else 0 end iSoNgayDuoi14BenhDaiNgay,
				--case when as_qtct.sXauNoiMa = '010-011-0001-0001-01-01-01' then fTongTien_DeNghi else 0 end fSoTienDuoi14BenhDaiNgay,
				--case when as_qtct.sXauNoiMa = '010-011-0001-0001-01-01-02' then iTongSo_DeNghi else 0 end iSoNgayTren14BenhDaiNgay,
				--case when as_qtct.sXauNoiMa = '010-011-0001-0001-01-01-02' then fTongTien_DeNghi else 0 end fSoTienTren14BenhDaiNgay,
				--case when as_qtct.sXauNoiMa = '010-011-0001-0001-01-02-01' then iTongSo_DeNghi else 0 end iSoNgayDuoi14OmKhac,
				--case when as_qtct.sXauNoiMa = '010-011-0001-0001-01-02-01' then fTongTien_DeNghi else 0 end fSoTienDuoi14OmKhac,
				--case when as_qtct.sXauNoiMa = '010-011-0001-0001-01-02-02' then iTongSo_DeNghi else 0 end iSoNgayTren14OmKhac,
				--case when as_qtct.sXauNoiMa = '010-011-0001-0001-01-02-02' then fTongTien_DeNghi else 0 end fSoTienTren14OmKhac,
				--case when as_qtct.sXauNoiMa = '010-011-0001-0002-02-00' then iTongSo_DeNghi else 0 end iSoNgayConOm,
				--case when as_qtct.sXauNoiMa = '010-011-0001-0002-02-00' then fTongTien_DeNghi else 0 end fSoTienConOm,
				--case when as_qtct.sXauNoiMa = '010-011-0001-0003-03-00' then iTongSo_DeNghi else 0 end iSoNgayPHSK,
				--case when as_qtct.sXauNoiMa = '010-011-0001-0003-03-00' then fTongTien_DeNghi else 0 end fSoTienPHSK
			from
			(
				select 
					qtcn.iID_MaDonVi,
					case when ml.sLNS = '9010001' then REPLACE(ml.sXauNoiMa,'9010001-' , '') else REPLACE(ml.sXauNoiMa,'9010002-' , '') end sXauNoiMa,
					Sum(qtcn_ct.iTongSo_DeNghi) as iTongSo_DeNghi,
					Sum(qtcn_ct.fTongTien_DeNghi)  as fTongTien_DeNghi
				from BH_QTC_Quy_CheDoBHXH_ChiTiet as qtcn_ct
				inner join BH_QTC_Quy_CheDoBHXH  as qtcn on qtcn_ct.iID_QTC_Quy_CheDoBHXH = qtcn.ID_QTC_Quy_CheDoBHXH
				inner join #tblMucLucNganSach ml on ml.iID_MLNS = qtcn_ct.iID_MucLucNganSach
				--and ((@IsTongHop = 0 and qtcn.bDaTongHop = 0 and qtcn.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  qtcn.sDSSoChungTuTongHop is not null))
				and qtcn.iQuyChungTu = @IQuy
				where qtcn.iNamChungTu=@INamLamViec
				group by qtcn.iID_MaDonVi,case when ml.sLNS = '9010001' then REPLACE(ml.sXauNoiMa,'9010001-' , '') else REPLACE(ml.sXauNoiMa,'9010002-' , '') end

			) as as_qtct) as as_qtct_1

			inner join #tblDonvi  dv on dv.iID_MaDonVi = as_qtct_1.iID_MaDonVi
			group by as_qtct_1.iID_MaDonVi, dv.sTenDonVi
		
		

		drop table #tblMucLucNganSach;
		drop table #tblDonvi


end
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_khoi]    Script Date: 7/12/2024 8:13:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_khoi]
@INamLamViec int,
@IdMaDonVi nvarchar(max),
@SLN nvarchar(max),
@IsTongHop bit,
@IQuy int,
@Donvitinh int
as
begin

---Lấy thông tin chi tiết giai thich tro cap du toan
		SELECT gt.* ,dv.sTenDonVi
			into #TBL_TroCapOmDauDuToan 
						FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gt
						LEFT JOIN BH_QTC_Quy_CheDoBHXH ct ON gt.iID_QTC_Quy_ChungTu=ct.ID_QTC_Quy_CheDoBHXH
						LEFT JOIN DonVi dv on dv.iID_MaDonVi=ct.iID_MaDonVi
						WHERE (gt.sXauNoiMa like '9010001-010-011-0001%' or gt.sXauNoiMa like '9010002-010-011-0001%')
								AND gt.iNamLamViec = @INamLamViec and gt.iQuy = @IQuy 
								AND dv.iNamLamViec=@INamLamViec
								AND dv.iID_MaDonVi  IN (select * from dbo.splitstring(@IdMaDonVi)) 
								AND dv.iKhoi=2
								AND ct.iNamChungTu=@INamLamViec

	---Lấy thông tin chi tiết giai thich tro cap hach toan
		SELECT gt.*,dv.sTenDonVi 
			into #TBL_TroCapOmDauHachToan 
						FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gt
						LEFT JOIN BH_QTC_Quy_CheDoBHXH ct ON gt.iID_QTC_Quy_ChungTu=ct.ID_QTC_Quy_CheDoBHXH
						LEFT JOIN DonVi dv on dv.iID_MaDonVi=ct.iID_MaDonVi
						WHERE (gt.sXauNoiMa like '9010001-010-011-0001%' or gt.sXauNoiMa like '9010002-010-011-0001%')
								AND gt.iNamLamViec = @INamLamViec and gt.iQuy = @IQuy 
								AND dv.iNamLamViec=@INamLamViec
								AND dv.iID_MaDonVi  IN (select * from dbo.splitstring(@IdMaDonVi)) 
								AND ct.iNamChungTu=@INamLamViec
								AND dv.iKhoi=1

		select 
		N'I. Khối dự toán' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgayDuoi14BenhDaiNgay
		,0 fSoTienDuoi14BenhDaiNgay
		,0 iSoNgayTren14BenhDaiNgay
		,0 fSoTienTren14BenhDaiNgay
		,0 iSoNgayDuoi14OmKhac
		,0 fSoTienDuoi14OmKhac
		,0 iSoNgayTren14OmKhac
		,0 fSoTienTren14OmKhac
		,0 iSoNgayConOm
		,0 fSoTienConOm
		,0 iSoNgayPHSK
		,0 fSoTienPHSK
		,3 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempDuToan

		-- SQ Du Toan
		select 
		N'1. Sĩ quan' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgayDuoi14BenhDaiNgay
		,0 fSoTienDuoi14BenhDaiNgay
		,0 iSoNgayTren14BenhDaiNgay
		,0 fSoTienTren14BenhDaiNgay
		,0 iSoNgayDuoi14OmKhac
		,0 fSoTienDuoi14OmKhac
		,0 iSoNgayTren14OmKhac
		,0 fSoTienTren14OmKhac
		,0 iSoNgayConOm
		,0 fSoTienConOm
		,0 iSoNgayPHSK
		,0 fSoTienPHSK
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempSQDuToan

		select 
		N'2. QNCN' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgayDuoi14BenhDaiNgay
		,0 fSoTienDuoi14BenhDaiNgay
		,0 iSoNgayTren14BenhDaiNgay
		,0 fSoTienTren14BenhDaiNgay
		,0 iSoNgayDuoi14OmKhac
		,0 fSoTienDuoi14OmKhac
		,0 iSoNgayTren14OmKhac
		,0 fSoTienTren14OmKhac
		,0 iSoNgayConOm
		,0 fSoTienConOm
		,0 iSoNgayPHSK
		,0 fSoTienPHSK
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempQNCNDuToan

		select 
		N'3. CNVCQP' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgayDuoi14BenhDaiNgay
		,0 fSoTienDuoi14BenhDaiNgay
		,0 iSoNgayTren14BenhDaiNgay
		,0 fSoTienTren14BenhDaiNgay
		,0 iSoNgayDuoi14OmKhac
		,0 fSoTienDuoi14OmKhac
		,0 iSoNgayTren14OmKhac
		,0 fSoTienTren14OmKhac
		,0 iSoNgayConOm
		,0 fSoTienConOm
		,0 iSoNgayPHSK
		,0 fSoTienPHSK
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempCNVCQPDuToan

		select 
		N'4. HSQBS' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgayDuoi14BenhDaiNgay
		,0 fSoTienDuoi14BenhDaiNgay
		,0 iSoNgayTren14BenhDaiNgay
		,0 fSoTienTren14BenhDaiNgay
		,0 iSoNgayDuoi14OmKhac
		,0 fSoTienDuoi14OmKhac
		,0 iSoNgayTren14OmKhac
		,0 fSoTienTren14OmKhac
		,0 iSoNgayConOm
		,0 fSoTienConOm
		,0 iSoNgayPHSK
		,0 fSoTienPHSK
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempHSQBSDuToan

		select 
		N'5. LDHD' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgayDuoi14BenhDaiNgay
		,0 fSoTienDuoi14BenhDaiNgay
		,0 iSoNgayTren14BenhDaiNgay
		,0 fSoTienTren14BenhDaiNgay
		,0 iSoNgayDuoi14OmKhac
		,0 fSoTienDuoi14OmKhac
		,0 iSoNgayTren14OmKhac
		,0 fSoTienTren14OmKhac
		,0 iSoNgayConOm
		,0 fSoTienConOm
		,0 iSoNgayPHSK
		,0 fSoTienPHSK
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempLDHDDuToan

----- Hach Toan
		select 
		N'II. Khối hạch toán' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgayDuoi14BenhDaiNgay
		,0 fSoTienDuoi14BenhDaiNgay
		,0 iSoNgayTren14BenhDaiNgay
		,0 fSoTienTren14BenhDaiNgay
		,0 iSoNgayDuoi14OmKhac
		,0 fSoTienDuoi14OmKhac
		,0 iSoNgayTren14OmKhac
		,0 fSoTienTren14OmKhac
		,0 iSoNgayConOm
		,0 fSoTienConOm
		,0 iSoNgayPHSK
		,0 fSoTienPHSK
		,3 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempHachToan

		select 
		N'1. Sĩ quan' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgayDuoi14BenhDaiNgay
		,0 fSoTienDuoi14BenhDaiNgay
		,0 iSoNgayTren14BenhDaiNgay
		,0 fSoTienTren14BenhDaiNgay
		,0 iSoNgayDuoi14OmKhac
		,0 fSoTienDuoi14OmKhac
		,0 iSoNgayTren14OmKhac
		,0 fSoTienTren14OmKhac
		,0 iSoNgayConOm
		,0 fSoTienConOm
		,0 iSoNgayPHSK
		,0 fSoTienPHSK
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempSQHachToan

		select 
		N'2. QNCN' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgayDuoi14BenhDaiNgay
		,0 fSoTienDuoi14BenhDaiNgay
		,0 iSoNgayTren14BenhDaiNgay
		,0 fSoTienTren14BenhDaiNgay
		,0 iSoNgayDuoi14OmKhac
		,0 fSoTienDuoi14OmKhac
		,0 iSoNgayTren14OmKhac
		,0 fSoTienTren14OmKhac
		,0 iSoNgayConOm
		,0 fSoTienConOm
		,0 iSoNgayPHSK
		,0 fSoTienPHSK
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempQNCNHachToan

		select 
		N'3. CNVCQP' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgayDuoi14BenhDaiNgay
		,0 fSoTienDuoi14BenhDaiNgay
		,0 iSoNgayTren14BenhDaiNgay
		,0 fSoTienTren14BenhDaiNgay
		,0 iSoNgayDuoi14OmKhac
		,0 fSoTienDuoi14OmKhac
		,0 iSoNgayTren14OmKhac
		,0 fSoTienTren14OmKhac
		,0 iSoNgayConOm
		,0 fSoTienConOm
		,0 iSoNgayPHSK
		,0 fSoTienPHSK
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempCNVCQPHachToan

		select 
		N'4. HSQBS' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgayDuoi14BenhDaiNgay
		,0 fSoTienDuoi14BenhDaiNgay
		,0 iSoNgayTren14BenhDaiNgay
		,0 fSoTienTren14BenhDaiNgay
		,0 iSoNgayDuoi14OmKhac
		,0 fSoTienDuoi14OmKhac
		,0 iSoNgayTren14OmKhac
		,0 fSoTienTren14OmKhac
		,0 iSoNgayConOm
		,0 fSoTienConOm
		,0 iSoNgayPHSK
		,0 fSoTienPHSK
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempHSQBSHachToan

		select 
		N'5. LDHD' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgayDuoi14BenhDaiNgay
		,0 fSoTienDuoi14BenhDaiNgay
		,0 iSoNgayTren14BenhDaiNgay
		,0 fSoTienTren14BenhDaiNgay
		,0 iSoNgayDuoi14OmKhac
		,0 fSoTienDuoi14OmKhac
		,0 iSoNgayTren14OmKhac
		,0 fSoTienTren14OmKhac
		,0 iSoNgayConOm
		,0 fSoTienConOm
		,0 iSoNgayPHSK
		,0 fSoTienPHSK
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempLDHDHachToan

--- Lay thong tin giai thich theo khoi du toan
		---Du Toan SQ
		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY gt.iID_MaDonVi ASC))) AS sTT,
				gt.iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01' then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.fSoTien) else 0 end ) fSoTienDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.fSoTien) else 0 end ) fSoTienTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.fSoTien) else 0 end ) fSoTienDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayTren14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.fSoTien) else 0 end ) fSoTienTren14OmKhac,

				
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayConOm,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.fSoTien) else 0 end ) fSoTienConOm,


				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayPHSK,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.fSoTien) else 0 end ) fSoTienPHSK
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalSQDuToan 
			FROM #TBL_TroCapOmDauDuToan gt
			where gt.sMaCapBac like  '1%'
			Group by gt.iID_MaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

		---Du Toan QNCN
		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY gt.iID_MaDonVi ASC))) AS sTT,
				gt.iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01' then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.fSoTien) else 0 end ) fSoTienDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.fSoTien) else 0 end ) fSoTienTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.fSoTien) else 0 end ) fSoTienDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayTren14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.fSoTien) else 0 end ) fSoTienTren14OmKhac,

				
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayConOm,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.fSoTien) else 0 end ) fSoTienConOm,


				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayPHSK,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.fSoTien) else 0 end ) fSoTienPHSK
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalQNCNDuToan 
			FROM #TBL_TroCapOmDauDuToan gt
			where gt.sMaCapBac LIKE '2%'
			Group by gt.iID_MaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa


	---Du Toan CNVCQP
		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY gt.iID_MaDonVi ASC))) AS sTT,
				gt.iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01' then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.fSoTien) else 0 end ) fSoTienDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.fSoTien) else 0 end ) fSoTienTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.fSoTien) else 0 end ) fSoTienDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayTren14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.fSoTien) else 0 end ) fSoTienTren14OmKhac,

				
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayConOm,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.fSoTien) else 0 end ) fSoTienConOm,


				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayPHSK,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.fSoTien) else 0 end ) fSoTienPHSK
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalCNVCQPDuToan 
			FROM #TBL_TroCapOmDauDuToan gt
			where gt.sMaCapBac LIKE '3.1%' OR gt.sMaCapBac LIKE '3.2%' OR gt.sMaCapBac LIKE '3.3%'  OR gt.sMaCapBac = '413' OR gt.sMaCapBac = '415'
			Group by gt.iID_MaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

	---Du Toan HSQBS
		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY gt.iID_MaDonVi ASC))) AS sTT,
				gt.iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01' then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.fSoTien) else 0 end ) fSoTienDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.fSoTien) else 0 end ) fSoTienTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.fSoTien) else 0 end ) fSoTienDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayTren14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.fSoTien) else 0 end ) fSoTienTren14OmKhac,

				
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayConOm,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.fSoTien) else 0 end ) fSoTienConOm,


				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayPHSK,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.fSoTien) else 0 end ) fSoTienPHSK
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalHSQBSDuToan 
			FROM #TBL_TroCapOmDauDuToan gt
			where gt.sMaCapBac  LIKE '0%'
			Group by gt.iID_MaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

	---Du Toan LDHD
		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY gt.iID_MaDonVi ASC))) AS sTT,
				gt.iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01' then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.fSoTien) else 0 end ) fSoTienDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.fSoTien) else 0 end ) fSoTienTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.fSoTien) else 0 end ) fSoTienDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayTren14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.fSoTien) else 0 end ) fSoTienTren14OmKhac,

				
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayConOm,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.fSoTien) else 0 end ) fSoTienConOm,


				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayPHSK,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.fSoTien) else 0 end ) fSoTienPHSK
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalLDHDDuToan 
			FROM #TBL_TroCapOmDauDuToan gt
				where gt.sMaCapBac  = '423' OR gt.sMaCapBac = '425' OR gt.sMaCapBac = '43'
			Group by gt.iID_MaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa


--- Lay thong tin giai thich theo khoi hach toan
		---Hạch Toan SQ
		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY gt.iID_MaDonVi ASC))) AS sTT,
				gt.iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01' then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.fSoTien) else 0 end ) fSoTienDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.fSoTien) else 0 end ) fSoTienTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.fSoTien) else 0 end ) fSoTienDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayTren14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.fSoTien) else 0 end ) fSoTienTren14OmKhac,

				
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayConOm,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.fSoTien) else 0 end ) fSoTienConOm,


				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayPHSK,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.fSoTien) else 0 end ) fSoTienPHSK
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalSQHachToan
			FROM #TBL_TroCapOmDauHachToan gt
			where gt.sMaCapBac like  '1%'
			Group by gt.iID_MaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

		---Hạch Toan QNCN
		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY gt.iID_MaDonVi ASC))) AS sTT,
				gt.iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01' then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.fSoTien) else 0 end ) fSoTienDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.fSoTien) else 0 end ) fSoTienTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.fSoTien) else 0 end ) fSoTienDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayTren14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.fSoTien) else 0 end ) fSoTienTren14OmKhac,

				
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayConOm,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.fSoTien) else 0 end ) fSoTienConOm,


				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayPHSK,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.fSoTien) else 0 end ) fSoTienPHSK
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalQNCNHachToan
			FROM #TBL_TroCapOmDauHachToan gt
			where gt.sMaCapBac LIKE '2%'
			Group by gt.iID_MaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa


	---Hạch Toan CNVCQP
		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY gt.iID_MaDonVi ASC))) AS sTT,
				gt.iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01' then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.fSoTien) else 0 end ) fSoTienDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.fSoTien) else 0 end ) fSoTienTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.fSoTien) else 0 end ) fSoTienDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayTren14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.fSoTien) else 0 end ) fSoTienTren14OmKhac,

				
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayConOm,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.fSoTien) else 0 end ) fSoTienConOm,


				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayPHSK,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.fSoTien) else 0 end ) fSoTienPHSK
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalCNVCQPHachToan
			FROM #TBL_TroCapOmDauHachToan gt
			where gt.sMaCapBac LIKE '3.1%' OR gt.sMaCapBac LIKE '3.2%' OR gt.sMaCapBac LIKE '3.3%'  OR gt.sMaCapBac = '413' OR gt.sMaCapBac = '415'
			Group by gt.iID_MaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

	---Hạch Toan HSQBS
		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY gt.iID_MaDonVi ASC))) AS sTT,
				gt.iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01' then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.fSoTien) else 0 end ) fSoTienDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.fSoTien) else 0 end ) fSoTienTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.fSoTien) else 0 end ) fSoTienDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayTren14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.fSoTien) else 0 end ) fSoTienTren14OmKhac,

				
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayConOm,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.fSoTien) else 0 end ) fSoTienConOm,


				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayPHSK,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.fSoTien) else 0 end ) fSoTienPHSK
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalHSQBSHachToan
			FROM #TBL_TroCapOmDauHachToan gt
			where gt.sMaCapBac  LIKE '0%'
			Group by gt.iID_MaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

	---Hạch Toan LDHD
		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY gt.iID_MaDonVi ASC))) AS sTT,
				gt.iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01' then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.fSoTien) else 0 end ) fSoTienDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.fSoTien) else 0 end ) fSoTienTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.fSoTien) else 0 end ) fSoTienDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayTren14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.fSoTien) else 0 end ) fSoTienTren14OmKhac,

				
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayConOm,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.fSoTien) else 0 end ) fSoTienConOm,


				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayPHSK,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.fSoTien) else 0 end ) fSoTienPHSK
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalLDHDHachToan
			FROM #TBL_TroCapOmDauHachToan gt
				where gt.sMaCapBac  = '423' OR gt.sMaCapBac = '425' OR gt.sMaCapBac = '43'
			Group by gt.iID_MaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

		----- Update Du Toan : SQ,QNCN,CNVCQP,HSQBS,LDHD
			update  A 
			SET
			A.iSoNgayDuoi14BenhDaiNgay=ISNULL(Detail.iSoNgayDuoi14BenhDaiNgay,0),
			A.fSoTienDuoi14BenhDaiNgay=ISNULL(Detail.fSoTienDuoi14BenhDaiNgay,0),
			A.iSoNgayTren14BenhDaiNgay=ISNULL(Detail.iSoNgayTren14BenhDaiNgay,0),
			A.fSoTienTren14BenhDaiNgay=ISNULL(Detail.fSoTienTren14BenhDaiNgay,0),
			A.iSoNgayDuoi14OmKhac=ISNULL(Detail.iSoNgayDuoi14OmKhac,0),
			A.fSoTienDuoi14OmKhac=ISNULL(Detail.fSoTienDuoi14OmKhac,0),
			A.iSoNgayTren14OmKhac=ISNULL(Detail.iSoNgayTren14OmKhac,0),
			A.fSoTienTren14OmKhac=ISNULL(Detail.fSoTienTren14OmKhac,0),
			A.iSoNgayConOm=ISNULL(Detail.iSoNgayConOm,0),
			A.fSoTienConOm=ISNULL(Detail.fSoTienConOm,0),
			A.iSoNgayPHSK=ISNULL(Detail.iSoNgayPHSK,0),
			A.fSoTienPHSK=ISNULL(Detail.fSoTienPHSK,0)
			FROM #tempSQDuToan A,
			(SELECT 
			Sum(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay
			, Sum(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay
			, Sum(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay
			, Sum(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay
			, Sum(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac
			, Sum(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac
			, Sum(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac
			, Sum(fSoTienTren14OmKhac) fSoTienTren14OmKhac
			, Sum(iSoNgayConOm) iSoNgayConOm
			, Sum(fSoTienConOm) fSoTienConOm
			, Sum(iSoNgayPHSK) iSoNgayPHSK
			, Sum(fSoTienPHSK) fSoTienPHSK
			, 2 type
			FROM #tempDetalSQDuToan) Detail
			where A.type=Detail.type

			update  A 
			SET
			A.iSoNgayDuoi14BenhDaiNgay=ISNULL(Detail.iSoNgayDuoi14BenhDaiNgay,0),
			A.fSoTienDuoi14BenhDaiNgay=ISNULL(Detail.fSoTienDuoi14BenhDaiNgay,0),
			A.iSoNgayTren14BenhDaiNgay=ISNULL(Detail.iSoNgayTren14BenhDaiNgay,0),
			A.fSoTienTren14BenhDaiNgay=ISNULL(Detail.fSoTienTren14BenhDaiNgay,0),
			A.iSoNgayDuoi14OmKhac=ISNULL(Detail.iSoNgayDuoi14OmKhac,0),
			A.fSoTienDuoi14OmKhac=ISNULL(Detail.fSoTienDuoi14OmKhac,0),
			A.iSoNgayTren14OmKhac=ISNULL(Detail.iSoNgayTren14OmKhac,0),
			A.fSoTienTren14OmKhac=ISNULL(Detail.fSoTienTren14OmKhac,0),
			A.iSoNgayConOm=ISNULL(Detail.iSoNgayConOm,0),
			A.fSoTienConOm=ISNULL(Detail.fSoTienConOm,0),
			A.iSoNgayPHSK=ISNULL(Detail.iSoNgayPHSK,0),
			A.fSoTienPHSK=ISNULL(Detail.fSoTienPHSK,0)
			FROM #tempQNCNDuToan A,
			(SELECT 
			Sum(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay
			, Sum(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay
			, Sum(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay
			, Sum(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay
			, Sum(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac
			, Sum(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac
			, Sum(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac
			, Sum(fSoTienTren14OmKhac) fSoTienTren14OmKhac
			, Sum(iSoNgayConOm) iSoNgayConOm
			, Sum(fSoTienConOm) fSoTienConOm
			, Sum(iSoNgayPHSK) iSoNgayPHSK
			, Sum(fSoTienPHSK) fSoTienPHSK
			, 2 type
			FROM #tempDetalQNCNDuToan) Detail
			where A.type=Detail.type

	update  A 
			SET
			A.iSoNgayDuoi14BenhDaiNgay=ISNULL(Detail.iSoNgayDuoi14BenhDaiNgay,0),
			A.fSoTienDuoi14BenhDaiNgay=ISNULL(Detail.fSoTienDuoi14BenhDaiNgay,0),
			A.iSoNgayTren14BenhDaiNgay=ISNULL(Detail.iSoNgayTren14BenhDaiNgay,0),
			A.fSoTienTren14BenhDaiNgay=ISNULL(Detail.fSoTienTren14BenhDaiNgay,0),
			A.iSoNgayDuoi14OmKhac=ISNULL(Detail.iSoNgayDuoi14OmKhac,0),
			A.fSoTienDuoi14OmKhac=ISNULL(Detail.fSoTienDuoi14OmKhac,0),
			A.iSoNgayTren14OmKhac=ISNULL(Detail.iSoNgayTren14OmKhac,0),
			A.fSoTienTren14OmKhac=ISNULL(Detail.fSoTienTren14OmKhac,0),
			A.iSoNgayConOm=ISNULL(Detail.iSoNgayConOm,0),
			A.fSoTienConOm=ISNULL(Detail.fSoTienConOm,0),
			A.iSoNgayPHSK=ISNULL(Detail.iSoNgayPHSK,0),
			A.fSoTienPHSK=ISNULL(Detail.fSoTienPHSK,0)
			FROM #tempCNVCQPDuToan A,
			(SELECT 
			Sum(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay
			, Sum(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay
			, Sum(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay
			, Sum(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay
			, Sum(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac
			, Sum(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac
			, Sum(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac
			, Sum(fSoTienTren14OmKhac) fSoTienTren14OmKhac
			, Sum(iSoNgayConOm) iSoNgayConOm
			, Sum(fSoTienConOm) fSoTienConOm
			, Sum(iSoNgayPHSK) iSoNgayPHSK
			, Sum(fSoTienPHSK) fSoTienPHSK
			, 2 type
			FROM #tempDetalCNVCQPDuToan) Detail
			where A.type=Detail.type

		update  A 
			SET
			A.iSoNgayDuoi14BenhDaiNgay=ISNULL(Detail.iSoNgayDuoi14BenhDaiNgay,0),
			A.fSoTienDuoi14BenhDaiNgay=ISNULL(Detail.fSoTienDuoi14BenhDaiNgay,0),
			A.iSoNgayTren14BenhDaiNgay=ISNULL(Detail.iSoNgayTren14BenhDaiNgay,0),
			A.fSoTienTren14BenhDaiNgay=ISNULL(Detail.fSoTienTren14BenhDaiNgay,0),
			A.iSoNgayDuoi14OmKhac=ISNULL(Detail.iSoNgayDuoi14OmKhac,0),
			A.fSoTienDuoi14OmKhac=ISNULL(Detail.fSoTienDuoi14OmKhac,0),
			A.iSoNgayTren14OmKhac=ISNULL(Detail.iSoNgayTren14OmKhac,0),
			A.fSoTienTren14OmKhac=ISNULL(Detail.fSoTienTren14OmKhac,0),
			A.iSoNgayConOm=ISNULL(Detail.iSoNgayConOm,0),
			A.fSoTienConOm=ISNULL(Detail.fSoTienConOm,0),
			A.iSoNgayPHSK=ISNULL(Detail.iSoNgayPHSK,0),
			A.fSoTienPHSK=ISNULL(Detail.fSoTienPHSK,0)
			FROM #tempHSQBSDuToan A,
			(SELECT 
			Sum(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay
			, Sum(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay
			, Sum(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay
			, Sum(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay
			, Sum(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac
			, Sum(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac
			, Sum(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac
			, Sum(fSoTienTren14OmKhac) fSoTienTren14OmKhac
			, Sum(iSoNgayConOm) iSoNgayConOm
			, Sum(fSoTienConOm) fSoTienConOm
			, Sum(iSoNgayPHSK) iSoNgayPHSK
			, Sum(fSoTienPHSK) fSoTienPHSK
			, 2 type
			FROM #tempDetalHSQBSDuToan) Detail
			where A.type=Detail.type

		update  A 
			SET
			A.iSoNgayDuoi14BenhDaiNgay=ISNULL(Detail.iSoNgayDuoi14BenhDaiNgay,0),
			A.fSoTienDuoi14BenhDaiNgay=ISNULL(Detail.fSoTienDuoi14BenhDaiNgay,0),
			A.iSoNgayTren14BenhDaiNgay=ISNULL(Detail.iSoNgayTren14BenhDaiNgay,0),
			A.fSoTienTren14BenhDaiNgay=ISNULL(Detail.fSoTienTren14BenhDaiNgay,0),
			A.iSoNgayDuoi14OmKhac=ISNULL(Detail.iSoNgayDuoi14OmKhac,0),
			A.fSoTienDuoi14OmKhac=ISNULL(Detail.fSoTienDuoi14OmKhac,0),
			A.iSoNgayTren14OmKhac=ISNULL(Detail.iSoNgayTren14OmKhac,0),
			A.fSoTienTren14OmKhac=ISNULL(Detail.fSoTienTren14OmKhac,0),
			A.iSoNgayConOm=ISNULL(Detail.iSoNgayConOm,0),
			A.fSoTienConOm=ISNULL(Detail.fSoTienConOm,0),
			A.iSoNgayPHSK=ISNULL(Detail.iSoNgayPHSK,0),
			A.fSoTienPHSK=ISNULL(Detail.fSoTienPHSK,0)
			FROM #tempLDHDDuToan A,
			(SELECT 
			Sum(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay
			, Sum(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay
			, Sum(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay
			, Sum(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay
			, Sum(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac
			, Sum(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac
			, Sum(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac
			, Sum(fSoTienTren14OmKhac) fSoTienTren14OmKhac
			, Sum(iSoNgayConOm) iSoNgayConOm
			, Sum(fSoTienConOm) fSoTienConOm
			, Sum(iSoNgayPHSK) iSoNgayPHSK
			, Sum(fSoTienPHSK) fSoTienPHSK
			, 2 type
			FROM #tempDetalLDHDDuToan) Detail
			where A.type=Detail.type
		----- Update Hach Toan : SQ,QNCN,CNVCQP,HSQBS,LDHD
			update  A 
			SET
			A.iSoNgayDuoi14BenhDaiNgay=ISNULL(Detail.iSoNgayDuoi14BenhDaiNgay,0),
			A.fSoTienDuoi14BenhDaiNgay=ISNULL(Detail.fSoTienDuoi14BenhDaiNgay,0),
			A.iSoNgayTren14BenhDaiNgay=ISNULL(Detail.iSoNgayTren14BenhDaiNgay,0),
			A.fSoTienTren14BenhDaiNgay=ISNULL(Detail.fSoTienTren14BenhDaiNgay,0),
			A.iSoNgayDuoi14OmKhac=ISNULL(Detail.iSoNgayDuoi14OmKhac,0),
			A.fSoTienDuoi14OmKhac=ISNULL(Detail.fSoTienDuoi14OmKhac,0),
			A.iSoNgayTren14OmKhac=ISNULL(Detail.iSoNgayTren14OmKhac,0),
			A.fSoTienTren14OmKhac=ISNULL(Detail.fSoTienTren14OmKhac,0),
			A.iSoNgayConOm=ISNULL(Detail.iSoNgayConOm,0),
			A.fSoTienConOm=ISNULL(Detail.fSoTienConOm,0),
			A.iSoNgayPHSK=ISNULL(Detail.iSoNgayPHSK,0),
			A.fSoTienPHSK=ISNULL(Detail.fSoTienPHSK,0)
			FROM #tempSQHachToan A,
			(SELECT 
			Sum(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay
			, Sum(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay
			, Sum(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay
			, Sum(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay
			, Sum(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac
			, Sum(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac
			, Sum(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac
			, Sum(fSoTienTren14OmKhac) fSoTienTren14OmKhac
			, Sum(iSoNgayConOm) iSoNgayConOm
			, Sum(fSoTienConOm) fSoTienConOm
			, Sum(iSoNgayPHSK) iSoNgayPHSK
			, Sum(fSoTienPHSK) fSoTienPHSK
			, 2 type
			FROM #tempDetalSQHachToan) Detail
			where A.type=Detail.type

			update  A 
			SET
			A.iSoNgayDuoi14BenhDaiNgay=ISNULL(Detail.iSoNgayDuoi14BenhDaiNgay,0),
			A.fSoTienDuoi14BenhDaiNgay=ISNULL(Detail.fSoTienDuoi14BenhDaiNgay,0),
			A.iSoNgayTren14BenhDaiNgay=ISNULL(Detail.iSoNgayTren14BenhDaiNgay,0),
			A.fSoTienTren14BenhDaiNgay=ISNULL(Detail.fSoTienTren14BenhDaiNgay,0),
			A.iSoNgayDuoi14OmKhac=ISNULL(Detail.iSoNgayDuoi14OmKhac,0),
			A.fSoTienDuoi14OmKhac=ISNULL(Detail.fSoTienDuoi14OmKhac,0),
			A.iSoNgayTren14OmKhac=ISNULL(Detail.iSoNgayTren14OmKhac,0),
			A.fSoTienTren14OmKhac=ISNULL(Detail.fSoTienTren14OmKhac,0),
			A.iSoNgayConOm=ISNULL(Detail.iSoNgayConOm,0),
			A.fSoTienConOm=ISNULL(Detail.fSoTienConOm,0),
			A.iSoNgayPHSK=ISNULL(Detail.iSoNgayPHSK,0),
			A.fSoTienPHSK=ISNULL(Detail.fSoTienPHSK,0)
			FROM #tempQNCNHachToan A,
			(SELECT 
			Sum(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay
			, Sum(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay
			, Sum(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay
			, Sum(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay
			, Sum(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac
			, Sum(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac
			, Sum(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac
			, Sum(fSoTienTren14OmKhac) fSoTienTren14OmKhac
			, Sum(iSoNgayConOm) iSoNgayConOm
			, Sum(fSoTienConOm) fSoTienConOm
			, Sum(iSoNgayPHSK) iSoNgayPHSK
			, Sum(fSoTienPHSK) fSoTienPHSK
			, 2 type
			FROM #tempDetalQNCNHachToan) Detail
			where A.type=Detail.type

	update  A 
			SET
			A.iSoNgayDuoi14BenhDaiNgay=ISNULL(Detail.iSoNgayDuoi14BenhDaiNgay,0),
			A.fSoTienDuoi14BenhDaiNgay=ISNULL(Detail.fSoTienDuoi14BenhDaiNgay,0),
			A.iSoNgayTren14BenhDaiNgay=ISNULL(Detail.iSoNgayTren14BenhDaiNgay,0),
			A.fSoTienTren14BenhDaiNgay=ISNULL(Detail.fSoTienTren14BenhDaiNgay,0),
			A.iSoNgayDuoi14OmKhac=ISNULL(Detail.iSoNgayDuoi14OmKhac,0),
			A.fSoTienDuoi14OmKhac=ISNULL(Detail.fSoTienDuoi14OmKhac,0),
			A.iSoNgayTren14OmKhac=ISNULL(Detail.iSoNgayTren14OmKhac,0),
			A.fSoTienTren14OmKhac=ISNULL(Detail.fSoTienTren14OmKhac,0),
			A.iSoNgayConOm=ISNULL(Detail.iSoNgayConOm,0),
			A.fSoTienConOm=ISNULL(Detail.fSoTienConOm,0),
			A.iSoNgayPHSK=ISNULL(Detail.iSoNgayPHSK,0),
			A.fSoTienPHSK=ISNULL(Detail.fSoTienPHSK,0)
			FROM #tempCNVCQPHachToan A,
			(SELECT 
			Sum(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay
			, Sum(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay
			, Sum(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay
			, Sum(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay
			, Sum(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac
			, Sum(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac
			, Sum(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac
			, Sum(fSoTienTren14OmKhac) fSoTienTren14OmKhac
			, Sum(iSoNgayConOm) iSoNgayConOm
			, Sum(fSoTienConOm) fSoTienConOm
			, Sum(iSoNgayPHSK) iSoNgayPHSK
			, Sum(fSoTienPHSK) fSoTienPHSK
			, 2 type
			FROM #tempDetalCNVCQPHachToan) Detail
			where A.type=Detail.type

		update  A 
			SET
			A.iSoNgayDuoi14BenhDaiNgay=ISNULL(Detail.iSoNgayDuoi14BenhDaiNgay,0),
			A.fSoTienDuoi14BenhDaiNgay=ISNULL(Detail.fSoTienDuoi14BenhDaiNgay,0),
			A.iSoNgayTren14BenhDaiNgay=ISNULL(Detail.iSoNgayTren14BenhDaiNgay,0),
			A.fSoTienTren14BenhDaiNgay=ISNULL(Detail.fSoTienTren14BenhDaiNgay,0),
			A.iSoNgayDuoi14OmKhac=ISNULL(Detail.iSoNgayDuoi14OmKhac,0),
			A.fSoTienDuoi14OmKhac=ISNULL(Detail.fSoTienDuoi14OmKhac,0),
			A.iSoNgayTren14OmKhac=ISNULL(Detail.iSoNgayTren14OmKhac,0),
			A.fSoTienTren14OmKhac=ISNULL(Detail.fSoTienTren14OmKhac,0),
			A.iSoNgayConOm=ISNULL(Detail.iSoNgayConOm,0),
			A.fSoTienConOm=ISNULL(Detail.fSoTienConOm,0),
			A.iSoNgayPHSK=ISNULL(Detail.iSoNgayPHSK,0),
			A.fSoTienPHSK=ISNULL(Detail.fSoTienPHSK,0)
			FROM #tempHSQBSHachToan A,
			(SELECT 
			Sum(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay
			, Sum(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay
			, Sum(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay
			, Sum(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay
			, Sum(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac
			, Sum(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac
			, Sum(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac
			, Sum(fSoTienTren14OmKhac) fSoTienTren14OmKhac
			, Sum(iSoNgayConOm) iSoNgayConOm
			, Sum(fSoTienConOm) fSoTienConOm
			, Sum(iSoNgayPHSK) iSoNgayPHSK
			, Sum(fSoTienPHSK) fSoTienPHSK
			, 2 type
			FROM #tempDetalHSQBSHachToan) Detail
			where A.type=Detail.type

		update  A 
			SET
			A.iSoNgayDuoi14BenhDaiNgay=ISNULL(Detail.iSoNgayDuoi14BenhDaiNgay,0),
			A.fSoTienDuoi14BenhDaiNgay=ISNULL(Detail.fSoTienDuoi14BenhDaiNgay,0),
			A.iSoNgayTren14BenhDaiNgay=ISNULL(Detail.iSoNgayTren14BenhDaiNgay,0),
			A.fSoTienTren14BenhDaiNgay=ISNULL(Detail.fSoTienTren14BenhDaiNgay,0),
			A.iSoNgayDuoi14OmKhac=ISNULL(Detail.iSoNgayDuoi14OmKhac,0),
			A.fSoTienDuoi14OmKhac=ISNULL(Detail.fSoTienDuoi14OmKhac,0),
			A.iSoNgayTren14OmKhac=ISNULL(Detail.iSoNgayTren14OmKhac,0),
			A.fSoTienTren14OmKhac=ISNULL(Detail.fSoTienTren14OmKhac,0),
			A.iSoNgayConOm=ISNULL(Detail.iSoNgayConOm,0),
			A.fSoTienConOm=ISNULL(Detail.fSoTienConOm,0),
			A.iSoNgayPHSK=ISNULL(Detail.iSoNgayPHSK,0),
			A.fSoTienPHSK=ISNULL(Detail.fSoTienPHSK,0)
			FROM #tempLDHDHachToan A,
			(SELECT 
			Sum(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay
			, Sum(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay
			, Sum(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay
			, Sum(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay
			, Sum(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac
			, Sum(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac
			, Sum(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac
			, Sum(fSoTienTren14OmKhac) fSoTienTren14OmKhac
			, Sum(iSoNgayConOm) iSoNgayConOm
			, Sum(fSoTienConOm) fSoTienConOm
			, Sum(iSoNgayPHSK) iSoNgayPHSK
			, Sum(fSoTienPHSK) fSoTienPHSK
			, 2 type
			FROM #tempDetalLDHDHachToan) Detail
			where A.type=Detail.type

---Tra ve kqua DuToan
			SELECT * INTO #tempkqDuToan FROM
			(
					SELECT * FROM #tempDuToan
					UNION ALL
					SELECT * FROM  #tempSQDuToan
					UNION ALL
					SELECT * FROM  #tempDetalSQDuToan
					UNION ALL
					SELECT * FROM  #tempQNCNDuToan
					UNION ALL
					SELECT * FROM  #tempDetalQNCNDuToan
					UNION ALL
					SELECT * FROM  #tempCNVCQPDuToan
					UNION ALL
					SELECT * FROM  #tempDetalCNVCQPDuToan
					UNION ALL
					SELECT * FROM  #tempHSQBSDuToan
					UNION ALL
					SELECT * FROM  #tempDetalHSQBSDuToan
					UNION ALL
					SELECT * FROM  #tempLDHDDuToan
					UNION ALL
					SELECT * FROM  #tempDetalLDHDHachToan
			) TEMPDuToan

------ Update total khoi du toan
			SELECT 
			Sum(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay
			, Sum(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay
			, Sum(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay
			, Sum(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay
			, Sum(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac
			, Sum(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac
			, Sum(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac
			, Sum(fSoTienTren14OmKhac) fSoTienTren14OmKhac
			, Sum(iSoNgayConOm) iSoNgayConOm
			, Sum(fSoTienConOm) fSoTienConOm
			, Sum(iSoNgayPHSK) iSoNgayPHSK
			, Sum(fSoTienPHSK) fSoTienPHSK
			, 3 type
			INTO #tempTotalDuToan
			FROM #tempkqDuToan
			WHERE type=2

			update T1
			set T1.iSoNgayDuoi14BenhDaiNgay=T2.iSoNgayDuoi14BenhDaiNgay,
			T1.fSoTienDuoi14BenhDaiNgay=T2.fSoTienDuoi14BenhDaiNgay,
			T1.iSoNgayTren14BenhDaiNgay=T2.iSoNgayTren14BenhDaiNgay,
			T1.fSoTienTren14BenhDaiNgay=T2.fSoTienTren14BenhDaiNgay,
			T1.iSoNgayDuoi14OmKhac=T2.iSoNgayDuoi14OmKhac,
			T1.fSoTienDuoi14OmKhac=T2.fSoTienDuoi14OmKhac,
			T1.iSoNgayTren14OmKhac=T2.iSoNgayTren14OmKhac,
			T1.fSoTienTren14OmKhac=T2.fSoTienTren14OmKhac,
			T1.iSoNgayConOm=T2.iSoNgayConOm,
			T1.fSoTienConOm=T2.fSoTienConOm,
			T1.iSoNgayPHSK=T2.iSoNgayPHSK,
			T1.fSoTienPHSK=T2.fSoTienPHSK
			FROM #tempkqDuToan T1, #tempTotalDuToan T2
			WHERE T1.type=T2.type
			and T1.STT=N'I. Khối dự toán'

---------  Tra ve kqua HachToan
			SELECT * INTO #tempkqHachToan FROM
			(
					SELECT * FROM #tempHachToan
					UNION ALL
					SELECT * FROM  #tempSQHachToan
					UNION ALL
					SELECT * FROM  #tempDetalSQHachToan
					UNION ALL
					SELECT * FROM  #tempQNCNHachToan
					UNION ALL
					SELECT * FROM  #tempDetalQNCNHachToan
					UNION ALL
					SELECT * FROM  #tempCNVCQPHachToan
					UNION ALL
					SELECT * FROM  #tempDetalCNVCQPHachToan
					UNION ALL
					SELECT * FROM  #tempHSQBSHachToan
					UNION ALL
					SELECT * FROM  #tempDetalHSQBSHachToan
					UNION ALL
					SELECT * FROM  #tempLDHDHachToan
					UNION ALL
					SELECT * FROM  #tempDetalLDHDHachToan
			) TEMPHachToan

------ Update total khoi du toan
			SELECT 
			Sum(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay
			, Sum(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay
			, Sum(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay
			, Sum(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay
			, Sum(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac
			, Sum(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac
			, Sum(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac
			, Sum(fSoTienTren14OmKhac) fSoTienTren14OmKhac
			, Sum(iSoNgayConOm) iSoNgayConOm
			, Sum(fSoTienConOm) fSoTienConOm
			, Sum(iSoNgayPHSK) iSoNgayPHSK
			, Sum(fSoTienPHSK) fSoTienPHSK
			, 3 type
			INTO #tempTotalHachToan
			FROM #tempkqHachToan
			WHERE type=2

			update T1
			set T1.iSoNgayDuoi14BenhDaiNgay=T2.iSoNgayDuoi14BenhDaiNgay,
			T1.fSoTienDuoi14BenhDaiNgay=T2.fSoTienDuoi14BenhDaiNgay,
			T1.iSoNgayTren14BenhDaiNgay=T2.iSoNgayTren14BenhDaiNgay,
			T1.fSoTienTren14BenhDaiNgay=T2.fSoTienTren14BenhDaiNgay,
			T1.iSoNgayDuoi14OmKhac=T2.iSoNgayDuoi14OmKhac,
			T1.fSoTienDuoi14OmKhac=T2.fSoTienDuoi14OmKhac,
			T1.iSoNgayTren14OmKhac=T2.iSoNgayTren14OmKhac,
			T1.fSoTienTren14OmKhac=T2.fSoTienTren14OmKhac,
			T1.iSoNgayConOm=T2.iSoNgayConOm,
			T1.fSoTienConOm=T2.fSoTienConOm,
			T1.iSoNgayPHSK=T2.iSoNgayPHSK,
			T1.fSoTienPHSK=T2.fSoTienPHSK
			FROM #tempkqHachToan T1, #tempTotalHachToan T2
			WHERE T1.type=T2.type
			and T1.STT=N'II. Khối hạch toán'

			SELECT * INTO #tempKQAll FROM
			(
					SELECT * FROM #tempkqDuToan
					UNION ALL
					SELECT * FROM  #tempkqHachToan
					
			) TEMPKQAll

		select sTT
		, iID_MaDonVi
		,sTenDonVi
		,iSoNgayDuoi14BenhDaiNgay
		,fSoTienDuoi14BenhDaiNgay/@Donvitinh fSoTienDuoi14BenhDaiNgay
		,iSoNgayTren14BenhDaiNgay
		,fSoTienTren14BenhDaiNgay/@Donvitinh fSoTienTren14BenhDaiNgay
		,iSoNgayDuoi14OmKhac
		,fSoTienDuoi14OmKhac/@Donvitinh fSoTienDuoi14OmKhac
		,iSoNgayTren14OmKhac
		,fSoTienTren14OmKhac/@Donvitinh fSoTienTren14OmKhac

		,iSoNgayConOm
		,fSoTienConOm/@Donvitinh fSoTienConOm
		,iSoNgayPHSK
		,fSoTienPHSK/@Donvitinh fSoTienPHSK

		,(fSoTienDuoi14BenhDaiNgay+fSoTienTren14BenhDaiNgay+fSoTienDuoi14OmKhac+fSoTienTren14OmKhac+fSoTienConOm+fSoTienPHSK)/@Donvitinh as fTongTien
		,IsHangCha
		,BHangCha
		,type
		,IsParent
		from #tempKQAll

		drop table #TBL_TroCapOmDauDuToan;
		drop table #TBL_TroCapOmDauHachToan;
		drop table #tempDuToan
		drop table #tempSQDuToan
		drop table #tempQNCNDuToan
		drop table #tempCNVCQPDuToan
		drop table #tempHSQBSDuToan
		drop table #tempLDHDDuToan
		DROP TABLE #tempDetalSQDuToan
		DROP TABLE #tempDetalQNCNDuToan
		DROP TABLE #tempDetalCNVCQPDuToan
		DROP TABLE #tempDetalHSQBSDuToan
		DROP TABLE #tempDetalLDHDDuToan
		DROP TABLE #tempTotalDuToan

		drop table #tempHachToan
		drop table #tempSQHachToan
		drop table #tempQNCNHachToan
		drop table #tempCNVCQPHachToan
		drop table #tempHSQBSHachToan
		drop table #tempLDHDHachToan
		DROP TABLE #tempDetalSQHachToan
		DROP TABLE #tempDetalQNCNHachToan
		DROP TABLE #tempDetalCNVCQPHachToan
		DROP TABLE #tempDetalHSQBSHachToan
		DROP TABLE #tempDetalLDHDHachToan
		DROP TABLE #tempTotalHachToan

		DROP TABLE #tempkqDuToan
		DROP TABLE #tempkqHachToan
		DROP TABLE #tempKQAll

end
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_bhxh]    Script Date: 7/12/2024 8:13:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_bhxh]
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
		and iTrangThai = 1
		---Lấy danh sách đơn vị được chọn
		select 
			iID_DonVi,
			iID_MaDonVi,
			sTenDonVi
		into #tblDonVi
		from DonVi where iNamLamViec = @INamLamViec and iID_MaDonVi  in (select * from dbo.splitstring(@IdMaDonVi)) 

		---Lấy thông tin chi tiết chứng từ
		select
			ROW_NUMBER() OVER(ORDER BY as_qtct_1.iID_MaDonVi ASC) AS sTT,
			as_qtct_1.iID_MaDonVi,
			#tblDonVi.sTenDonVi,
			Sum(iSoNgaySinhConNNuoiCon) as iSoNgaySinhConNNuoiCon,
			Sum(fSoTienSinhConNNuoiCon) as fSoTienSinhConNNuoiCon,
			Sum(iSoNgaySinhTroCapSinhCon) as iSoNgaySinhTroCapSinhCon,
			Sum(fSoTienSinhTroCapSinhCon) as fSoTienSinhTroCapSinhCon,
			Sum(iSoNgayKhamThaiKHHGD) as ISoNgayKhamThaiKHHGD,
			Sum(fSoTienKhamThaiKHHGD) as fSoTienKhamThaiKHHGD,
			Sum(iSoNgayPHSKThaiSan) as iSoNgayPHSKThaiSan,
			Sum(fSoTienPHSKThaiSan) as fSoTienPHSKThaiSan,
			isnull(Sum(fSoTienSinhConNNuoiCon),0) + isnull(Sum(fSoTienSinhTroCapSinhCon),0) + isnull(Sum(fSoTienKhamThaiKHHGD),0) + isnull(Sum(fSoTienPHSKThaiSan),0)  as fTongTien
			,0 BHangCha
			,0 IsHangCha
			
		from
			(
			select
				as_qtct.iID_MaDonVi,
				case when as_qtct.sXauNoiMa = '010-011-0002-0001-0001-00-01' or as_qtct.sXauNoiMa = '010-011-0002-0001-0001-00-02' then iTongSo_DeNghi else 0 end iSoNgaySinhConNNuoiCon,
				case when as_qtct.sXauNoiMa = '010-011-0002-0001-0001-00-01' or as_qtct.sXauNoiMa = '010-011-0002-0001-0001-00-02' then fTongTien_DeNghi else 0 end fSoTienSinhConNNuoiCon,
				case when as_qtct.sXauNoiMa = '010-011-0002-0001-0002-00-01' or as_qtct.sXauNoiMa = '010-011-0002-0001-0002-00-02' then iTongSo_DeNghi else 0 end iSoNgaySinhTroCapSinhCon,
				case when as_qtct.sXauNoiMa = '010-011-0002-0001-0002-00-01' or as_qtct.sXauNoiMa = '010-011-0002-0001-0002-00-02' then fTongTien_DeNghi else 0 end fSoTienSinhTroCapSinhCon,
				case when as_qtct.sXauNoiMa = '010-011-0002-0001-0003-00-01' or as_qtct.sXauNoiMa = '010-011-0002-0001-0003-00-02' then iTongSo_DeNghi else 0 end iSoNgayKhamThaiKHHGD,
				case when as_qtct.sXauNoiMa = '010-011-0002-0001-0003-00-01' or as_qtct.sXauNoiMa = '010-011-0002-0001-0003-00-02' then fTongTien_DeNghi else 0 end fSoTienKhamThaiKHHGD,
				case when as_qtct.sXauNoiMa = '010-011-0002-0001-0004-00' then iTongSo_DeNghi else 0 end iSoNgayPHSKThaiSan,
				case when as_qtct.sXauNoiMa = '010-011-0002-0001-0004-00' then fTongTien_DeNghi else 0 end fSoTienPHSKThaiSan
				--case when as_qtct.sXauNoiMa = '010-011-0002-0001-01-00-01' or as_qtct.sXauNoiMa = '010-011-0002-0001-01-00-02' then iTongSo_DeNghi else 0 end iSoNgaySinhConNNuoiCon,
				--case when as_qtct.sXauNoiMa = '010-011-0002-0001-01-00-01' or as_qtct.sXauNoiMa = '010-011-0002-0001-01-00-02' then fTongTien_DeNghi else 0 end fSoTienSinhConNNuoiCon,
				--case when as_qtct.sXauNoiMa = '010-011-0002-0001-02-00-01' or as_qtct.sXauNoiMa = '010-011-0002-0001-02-00-02' then iTongSo_DeNghi else 0 end iSoNgaySinhTroCapSinhCon,
				--case when as_qtct.sXauNoiMa = '010-011-0002-0001-02-00-01' or as_qtct.sXauNoiMa = '010-011-0002-0001-02-00-02' then fTongTien_DeNghi else 0 end fSoTienSinhTroCapSinhCon,
				--case when as_qtct.sXauNoiMa = '010-011-0002-0001-03-00-01' or as_qtct.sXauNoiMa = '010-011-0002-0001-03-00-02' then iTongSo_DeNghi else 0 end iSoNgayKhamThaiKHHGD,
				--case when as_qtct.sXauNoiMa = '010-011-0002-0001-03-00-01' or as_qtct.sXauNoiMa = '010-011-0002-0001-03-00-02' then fTongTien_DeNghi else 0 end fSoTienKhamThaiKHHGD,
				--case when as_qtct.sXauNoiMa = '010-011-0002-0001-04-00' then iTongSo_DeNghi else 0 end iSoNgayPHSKThaiSan,
				--case when as_qtct.sXauNoiMa = '010-011-0002-0001-04-00' then fTongTien_DeNghi else 0 end fSoTienPHSKThaiSan
			from
			(
				select 
					qtcn.iID_MaDonVi,
					case when ml.sLNS = '9010001' then REPLACE(ml.sXauNoiMa,'9010001-' , '') else REPLACE(ml.sXauNoiMa,'9010002-' , '') end sXauNoiMa,
					Sum(qtcn_ct.iTongSo_DeNghi) as iTongSo_DeNghi,
					Sum(qtcn_ct.fTongTien_DeNghi)  as fTongTien_DeNghi
				from BH_QTC_Quy_CheDoBHXH_ChiTiet as qtcn_ct
				inner join BH_QTC_Quy_CheDoBHXH  as qtcn on qtcn_ct.iID_QTC_Quy_CheDoBHXH = qtcn.ID_QTC_Quy_CheDoBHXH
				inner join #tblMucLucNganSach ml on ml.iID_MLNS = qtcn_ct.iID_MucLucNganSach
			
				--and ((@IsTongHop = 0 and qtcn.bDaTongHop = 0 and qtcn.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  qtcn.sDSSoChungTuTongHop is not null))
				and qtcn.iQuyChungTu = @IQuy
				where 	qtcn_ct.iNamLamViec=@INamLamViec
				group by qtcn.iID_MaDonVi,case when ml.sLNS = '9010001' then REPLACE(ml.sXauNoiMa,'9010001-' , '') else REPLACE(ml.sXauNoiMa,'9010002-' , '') end

			) as as_qtct) as as_qtct_1

			inner join #tblDonVi on #tblDonVi.iID_MaDonVi = as_qtct_1.iID_MaDonVi
			group by as_qtct_1.iID_MaDonVi, #tblDonVi.sTenDonVi
		
		

		drop table #tblMucLucNganSach;
		drop table #tblDonVi


end
;
;


GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_khoi]    Script Date: 7/12/2024 8:13:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_khoi]
@INamLamViec int,
@IdMaDonVi nvarchar(max),
@IQuy int,
@Donvitinh int
as
begin
	---Lấy thông tin chi tiết giai thich tro cap du toan
		SELECT gt.* ,dv.sTenDonVi
			into #TBL_TroCapThaiSanDuToan 
						FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gt
						LEFT JOIN BH_QTC_Quy_CheDoBHXH ct ON gt.iID_QTC_Quy_ChungTu=ct.ID_QTC_Quy_CheDoBHXH
						LEFT JOIN DonVi dv on dv.iID_MaDonVi=ct.iID_MaDonVi
						WHERE (gt.sXauNoiMa like '9010001-010-011-0002%' or gt.sXauNoiMa like '9010002-010-011-0002%')
								AND gt.iNamLamViec = @INamLamViec and gt.iQuy = @IQuy 
								AND dv.iNamLamViec=@INamLamViec
								AND dv.iID_MaDonVi  IN (select * from dbo.splitstring(@IdMaDonVi)) 
								AND dv.iKhoi=2
								AND ct.iNamChungTu=@INamLamViec

	---Lấy thông tin chi tiết giai thich tro cap hach toan
		SELECT gt.*,dv.sTenDonVi 
			into #TBL_TroCapThaiSanHachToan 
						FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gt
						LEFT JOIN BH_QTC_Quy_CheDoBHXH ct ON gt.iID_QTC_Quy_ChungTu=ct.ID_QTC_Quy_CheDoBHXH
						LEFT JOIN DonVi dv on dv.iID_MaDonVi=ct.iID_MaDonVi
						WHERE (gt.sXauNoiMa like '9010001-010-011-0002%' or gt.sXauNoiMa like '9010002-010-011-0002%')
								AND gt.iNamLamViec = @INamLamViec and gt.iQuy = @IQuy 
								AND dv.iNamLamViec=@INamLamViec
								AND dv.iID_MaDonVi  IN (select * from dbo.splitstring(@IdMaDonVi)) 
								AND ct.iNamChungTu=@INamLamViec
								AND dv.iKhoi=1

		select 
		N'I. Khối dự toán' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgaySinhConNNuoiCon
		,0 fSoTienSinhConNNuoiCon
		,0 iSoNgaySinhTroCapSinhCon
		,0 fSoTienSinhTroCapSinhCon
		,0 ISoNgayKhamThaiKHHGD
		,0 fSoTienKhamThaiKHHGD
		,0 iSoNgayPHSKThaiSan
		,0 fSoTienPHSKThaiSan
		,3 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempDuToan

		-- SQ Du Toan
		select 
		N'1. Sĩ quan' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgaySinhConNNuoiCon
		,0 fSoTienSinhConNNuoiCon
		,0 iSoNgaySinhTroCapSinhCon
		,0 fSoTienSinhTroCapSinhCon
		,0 ISoNgayKhamThaiKHHGD
		,0 fSoTienKhamThaiKHHGD
		,0 iSoNgayPHSKThaiSan
		,0 fSoTienPHSKThaiSan
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempSQDuToan

		select 
		N'2. QNCN' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgaySinhConNNuoiCon
		,0 fSoTienSinhConNNuoiCon
		,0 iSoNgaySinhTroCapSinhCon
		,0 fSoTienSinhTroCapSinhCon
		,0 ISoNgayKhamThaiKHHGD
		,0 fSoTienKhamThaiKHHGD
		,0 iSoNgayPHSKThaiSan
		,0 fSoTienPHSKThaiSan
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempQNCNDuToan

		select 
		N'3. CNVCQP' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgaySinhConNNuoiCon
		,0 fSoTienSinhConNNuoiCon
		,0 iSoNgaySinhTroCapSinhCon
		,0 fSoTienSinhTroCapSinhCon
		,0 ISoNgayKhamThaiKHHGD
		,0 fSoTienKhamThaiKHHGD
		,0 iSoNgayPHSKThaiSan
		,0 fSoTienPHSKThaiSan
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempCNVCQPDuToan

		select 
		N'4. HSQBS' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgaySinhConNNuoiCon
		,0 fSoTienSinhConNNuoiCon
		,0 iSoNgaySinhTroCapSinhCon
		,0 fSoTienSinhTroCapSinhCon
		,0 ISoNgayKhamThaiKHHGD
		,0 fSoTienKhamThaiKHHGD
		,0 iSoNgayPHSKThaiSan
		,0 fSoTienPHSKThaiSan
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempHSQBSDuToan

		select 
		N'5. LDHD' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgaySinhConNNuoiCon
		,0 fSoTienSinhConNNuoiCon
		,0 iSoNgaySinhTroCapSinhCon
		,0 fSoTienSinhTroCapSinhCon
		,0 ISoNgayKhamThaiKHHGD
		,0 fSoTienKhamThaiKHHGD
		,0 iSoNgayPHSKThaiSan
		,0 fSoTienPHSKThaiSan
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempLDHDDuToan

----- Hach Toan
		select 
		N'II. Khối hạch toán' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgaySinhConNNuoiCon
		,0 fSoTienSinhConNNuoiCon
		,0 iSoNgaySinhTroCapSinhCon
		,0 fSoTienSinhTroCapSinhCon
		,0 ISoNgayKhamThaiKHHGD
		,0 fSoTienKhamThaiKHHGD
		,0 iSoNgayPHSKThaiSan
		,0 fSoTienPHSKThaiSan
		,3 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempHachToan

		select 
		N'1. Sĩ quan' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgaySinhConNNuoiCon
		,0 fSoTienSinhConNNuoiCon
		,0 iSoNgaySinhTroCapSinhCon
		,0 fSoTienSinhTroCapSinhCon
		,0 ISoNgayKhamThaiKHHGD
		,0 fSoTienKhamThaiKHHGD
		,0 iSoNgayPHSKThaiSan
		,0 fSoTienPHSKThaiSan
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempSQHachToan

		select 
		N'2. QNCN' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgaySinhConNNuoiCon
		,0 fSoTienSinhConNNuoiCon
		,0 iSoNgaySinhTroCapSinhCon
		,0 fSoTienSinhTroCapSinhCon
		,0 ISoNgayKhamThaiKHHGD
		,0 fSoTienKhamThaiKHHGD
		,0 iSoNgayPHSKThaiSan
		,0 fSoTienPHSKThaiSan
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempQNCNHachToan

		select 
		N'3. CNVCQP' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgaySinhConNNuoiCon
		,0 fSoTienSinhConNNuoiCon
		,0 iSoNgaySinhTroCapSinhCon
		,0 fSoTienSinhTroCapSinhCon
		,0 ISoNgayKhamThaiKHHGD
		,0 fSoTienKhamThaiKHHGD
		,0 iSoNgayPHSKThaiSan
		,0 fSoTienPHSKThaiSan
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempCNVCQPHachToan

		select 
		N'4. HSQBS' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgaySinhConNNuoiCon
		,0 fSoTienSinhConNNuoiCon
		,0 iSoNgaySinhTroCapSinhCon
		,0 fSoTienSinhTroCapSinhCon
		,0 ISoNgayKhamThaiKHHGD
		,0 fSoTienKhamThaiKHHGD
		,0 iSoNgayPHSKThaiSan
		,0 fSoTienPHSKThaiSan
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempHSQBSHachToan

		select 
		N'5. LDHD' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgaySinhConNNuoiCon
		,0 fSoTienSinhConNNuoiCon
		,0 iSoNgaySinhTroCapSinhCon
		,0 fSoTienSinhTroCapSinhCon
		,0 ISoNgayKhamThaiKHHGD
		,0 fSoTienKhamThaiKHHGD
		,0 iSoNgayPHSKThaiSan
		,0 fSoTienPHSKThaiSan
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempLDHDHachToan

		--- Lay thong tin giai thich theo khoi du toan
		---Du Toan SQ
		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY gt.iID_MaDonVi ASC))) AS sTT,
				gt.iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%' then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iSoNgayHuong) else 0 end )/30 iSoNgaySinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.fSoTien) else 0 end ) fSoTienSinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgaySinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.fSoTien) else 0 end ) fSoTienSinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayKhamThaiKHHGD,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fSoTien) else 0 end ) fSoTienKhamThaiKHHGD,

				
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayPHSKThaiSan,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fSoTien) else 0 end ) fSoTienPHSKThaiSan
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalSQDuToan 
			FROM #TBL_TroCapThaiSanDuToan gt
			where gt.sMaCapBac like  '1%'
			Group by gt.iID_MaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

		---Du Toan QNCN
		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY gt.iID_MaDonVi ASC))) AS sTT,
				gt.iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%' then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iSoNgayHuong) else 0 end )/30 iSoNgaySinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.fSoTien) else 0 end ) fSoTienSinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgaySinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.fSoTien) else 0 end ) fSoTienSinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayKhamThaiKHHGD,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fSoTien) else 0 end ) fSoTienKhamThaiKHHGD,

				
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayPHSKThaiSan,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fSoTien) else 0 end ) fSoTienPHSKThaiSan
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalQNCNDuToan 
			FROM #TBL_TroCapThaiSanDuToan gt
			where gt.sMaCapBac LIKE '2%'
			Group by gt.iID_MaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

		---Du Toan CNVCQP
		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY gt.iID_MaDonVi ASC))) AS sTT,
				gt.iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%' then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iSoNgayHuong) else 0 end )/30 iSoNgaySinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.fSoTien) else 0 end ) fSoTienSinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgaySinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.fSoTien) else 0 end ) fSoTienSinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayKhamThaiKHHGD,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fSoTien) else 0 end ) fSoTienKhamThaiKHHGD,

				
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayPHSKThaiSan,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fSoTien) else 0 end ) fSoTienPHSKThaiSan
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalCNVCQPDuToan 
			FROM #TBL_TroCapThaiSanDuToan gt
			where gt.sMaCapBac LIKE '3.1%' OR gt.sMaCapBac LIKE '3.2%' OR gt.sMaCapBac LIKE '3.3%'  OR gt.sMaCapBac = '413' OR gt.sMaCapBac = '415'
			Group by gt.iID_MaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

		-- Du Toan HSQBS
		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY gt.iID_MaDonVi ASC))) AS sTT,
				gt.iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%' then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iSoNgayHuong) else 0 end )/30 iSoNgaySinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.fSoTien) else 0 end ) fSoTienSinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgaySinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.fSoTien) else 0 end ) fSoTienSinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayKhamThaiKHHGD,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fSoTien) else 0 end ) fSoTienKhamThaiKHHGD,

				
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayPHSKThaiSan,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fSoTien) else 0 end ) fSoTienPHSKThaiSan
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalHSQBSDuToan 
			FROM #TBL_TroCapThaiSanDuToan gt
			where gt.sMaCapBac  LIKE '0%'
			Group by gt.iID_MaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

		-- Du Toan LDHD
		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY gt.iID_MaDonVi ASC))) AS sTT,
				gt.iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%' then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iSoNgayHuong) else 0 end )/30 iSoNgaySinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.fSoTien) else 0 end ) fSoTienSinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgaySinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.fSoTien) else 0 end ) fSoTienSinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayKhamThaiKHHGD,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fSoTien) else 0 end ) fSoTienKhamThaiKHHGD,

				
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayPHSKThaiSan,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fSoTien) else 0 end ) fSoTienPHSKThaiSan
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalLDHDDuToan 
			FROM #TBL_TroCapThaiSanDuToan gt
			where gt.sMaCapBac  = '423' OR gt.sMaCapBac = '425' OR gt.sMaCapBac = '43'
			Group by gt.iID_MaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

		--- Lay thong tin giai thich theo khoi Hach Toan
		---Hach Toan SQ
		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY gt.iID_MaDonVi ASC))) AS sTT,
				gt.iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%' then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iSoNgayHuong) else 0 end )/30 iSoNgaySinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.fSoTien) else 0 end ) fSoTienSinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgaySinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.fSoTien) else 0 end ) fSoTienSinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayKhamThaiKHHGD,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fSoTien) else 0 end ) fSoTienKhamThaiKHHGD,

				
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayPHSKThaiSan,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fSoTien) else 0 end ) fSoTienPHSKThaiSan
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalSQHachToan 
			FROM #TBL_TroCapThaiSanHachToan gt
			where gt.sMaCapBac like  '1%'
			Group by gt.iID_MaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

		---Hach Toan QNCN
		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY gt.iID_MaDonVi ASC))) AS sTT,
				gt.iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%' then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iSoNgayHuong) else 0 end )/30 iSoNgaySinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.fSoTien) else 0 end ) fSoTienSinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgaySinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.fSoTien) else 0 end ) fSoTienSinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayKhamThaiKHHGD,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fSoTien) else 0 end ) fSoTienKhamThaiKHHGD,

				
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayPHSKThaiSan,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fSoTien) else 0 end ) fSoTienPHSKThaiSan
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalQNCNHachToan 
			FROM #TBL_TroCapThaiSanHachToan gt
			where gt.sMaCapBac LIKE '2%'
			Group by gt.iID_MaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

		---Hach Toan CNVCQP
		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY gt.iID_MaDonVi ASC))) AS sTT,
				gt.iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%' then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iSoNgayHuong) else 0 end )/30 iSoNgaySinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.fSoTien) else 0 end ) fSoTienSinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgaySinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.fSoTien) else 0 end ) fSoTienSinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayKhamThaiKHHGD,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fSoTien) else 0 end ) fSoTienKhamThaiKHHGD,

				
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayPHSKThaiSan,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fSoTien) else 0 end ) fSoTienPHSKThaiSan
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalCNVCQPHachToan
			FROM #TBL_TroCapThaiSanHachToan gt
			where gt.sMaCapBac LIKE '3.1%' OR gt.sMaCapBac LIKE '3.2%' OR gt.sMaCapBac LIKE '3.3%'  OR gt.sMaCapBac = '413' OR gt.sMaCapBac = '415'
			Group by gt.iID_MaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

		-- Hach Toan HSQBS
		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY gt.iID_MaDonVi ASC))) AS sTT,
				gt.iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%' then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iSoNgayHuong) else 0 end )/30 iSoNgaySinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.fSoTien) else 0 end ) fSoTienSinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgaySinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.fSoTien) else 0 end ) fSoTienSinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayKhamThaiKHHGD,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fSoTien) else 0 end ) fSoTienKhamThaiKHHGD,

				
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayPHSKThaiSan,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fSoTien) else 0 end ) fSoTienPHSKThaiSan
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalHSQBSHachToan
			FROM #TBL_TroCapThaiSanHachToan gt
			where gt.sMaCapBac  LIKE '0%'
			Group by gt.iID_MaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa


		-- Hach Toan LDHD
		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY gt.iID_MaDonVi ASC))) AS sTT,
				gt.iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%' then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iSoNgayHuong) else 0 end )/30 iSoNgaySinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.fSoTien) else 0 end ) fSoTienSinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgaySinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.fSoTien) else 0 end ) fSoTienSinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayKhamThaiKHHGD,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fSoTien) else 0 end ) fSoTienKhamThaiKHHGD,

				
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.iSoNgayHuong) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.iSoNgayHuong) else 0 end ) iSoNgayPHSKThaiSan,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fSoTien) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fSoTien) else 0 end ) fSoTienPHSKThaiSan
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalLDHDHachToan
			FROM #TBL_TroCapThaiSanHachToan gt
			where gt.sMaCapBac  = '423' OR gt.sMaCapBac = '425' OR gt.sMaCapBac = '43'
			Group by gt.iID_MaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

		----- Update Du Toan : SQ,QNCN,CNVCQP,HSQBS,LDHD
					update SQ
			set SQ.iSoNgaySinhConNNuoiCon=ISNULL(DetailSQ.iSoNgaySinhConNNuoiCon,0),
			SQ.fSoTienSinhConNNuoiCon=ISNULL(DetailSQ.fSoTienSinhConNNuoiCon,0),
			SQ.iSoNgaySinhTroCapSinhCon=ISNULL(DetailSQ.iSoNgaySinhTroCapSinhCon,0),
			SQ.fSoTienSinhTroCapSinhCon=ISNULL(DetailSQ.fSoTienSinhTroCapSinhCon,0),
			SQ.iSoNgayKhamThaiKHHGD=ISNULL(DetailSQ.iSoNgayKhamThaiKHHGD,0),
			SQ.fSoTienKhamThaiKHHGD=ISNULL(DetailSQ.fSoTienKhamThaiKHHGD,0),
			SQ.iSoNgayPHSKThaiSan=ISNULL(DetailSQ.iSoNgayPHSKThaiSan,0),
			SQ.fSoTienPHSKThaiSan=ISNULL(DetailSQ.fSoTienPHSKThaiSan,0)
			FROM #tempSQDuToan SQ,
			(SELECT 
			Sum(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon
			, Sum(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon
			, Sum(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon
			, Sum(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon
			, Sum(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD
			, Sum(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD
			, Sum(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan
			, Sum(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
			, 2 type
			FROM #tempDetalSQDuToan) DetailSQ
			where DetailSQ.type=SQ.type

			update QNCN
			set QNCN.iSoNgaySinhConNNuoiCon=ISNULL(DetailQNCN.iSoNgaySinhConNNuoiCon,0),
			QNCN.fSoTienSinhConNNuoiCon=ISNULL(DetailQNCN.fSoTienSinhConNNuoiCon,0),
			QNCN.iSoNgaySinhTroCapSinhCon=ISNULL(DetailQNCN.iSoNgaySinhTroCapSinhCon,0),
			QNCN.fSoTienSinhTroCapSinhCon=ISNULL(DetailQNCN.fSoTienSinhTroCapSinhCon,0),
			QNCN.iSoNgayKhamThaiKHHGD=ISNULL(DetailQNCN.iSoNgayKhamThaiKHHGD,0),
			QNCN.fSoTienKhamThaiKHHGD=ISNULL(DetailQNCN.fSoTienKhamThaiKHHGD,0),
			QNCN.iSoNgayPHSKThaiSan=ISNULL(DetailQNCN.iSoNgayPHSKThaiSan,0),
			QNCN.fSoTienPHSKThaiSan=ISNULL(DetailQNCN.fSoTienPHSKThaiSan,0)
			FROM #tempQNCNDuToan QNCN,
			(SELECT 
			Sum(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon
			, Sum(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon
			, Sum(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon
			, Sum(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon
			, Sum(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD
			, Sum(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD
			, Sum(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan
			, Sum(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
			FROM #tempDetalQNCNDuToan) DetailQNCN

			update CNVCQP
			set CNVCQP.iSoNgaySinhConNNuoiCon=ISNULL(DetailCNVCQP.iSoNgaySinhConNNuoiCon,0),
			CNVCQP.fSoTienSinhConNNuoiCon=ISNULL(DetailCNVCQP.fSoTienSinhConNNuoiCon,0),
			CNVCQP.iSoNgaySinhTroCapSinhCon=ISNULL(DetailCNVCQP.iSoNgaySinhTroCapSinhCon,0),
			CNVCQP.fSoTienSinhTroCapSinhCon=ISNULL(DetailCNVCQP.fSoTienSinhTroCapSinhCon,0),
			CNVCQP.iSoNgayKhamThaiKHHGD=ISNULL(DetailCNVCQP.iSoNgayKhamThaiKHHGD,0),
			CNVCQP.fSoTienKhamThaiKHHGD=ISNULL(DetailCNVCQP.fSoTienKhamThaiKHHGD,0),
			CNVCQP.iSoNgayPHSKThaiSan=ISNULL(DetailCNVCQP.iSoNgayPHSKThaiSan,0),
			CNVCQP.fSoTienPHSKThaiSan=ISNULL(DetailCNVCQP.fSoTienPHSKThaiSan,0)
			FROM #tempCNVCQPDuToan CNVCQP,
			(SELECT 
			Sum(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon
			, Sum(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon
			, Sum(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon
			, Sum(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon
			, Sum(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD
			, Sum(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD
			, Sum(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan
			, Sum(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
			FROM #tempDetalCNVCQPDuToan) DetailCNVCQP

			update #tempHSQBSDuToan
			set iSoNgaySinhConNNuoiCon=ISNULL(DetailHSQBS.iSoNgaySinhConNNuoiCon,0),
			fSoTienSinhConNNuoiCon=ISNULL(DetailHSQBS.fSoTienSinhConNNuoiCon,0),
			iSoNgaySinhTroCapSinhCon=ISNULL(DetailHSQBS.iSoNgaySinhTroCapSinhCon,0),
			fSoTienSinhTroCapSinhCon=ISNULL(DetailHSQBS.fSoTienSinhTroCapSinhCon,0),
			iSoNgayKhamThaiKHHGD=ISNULL(DetailHSQBS.iSoNgayKhamThaiKHHGD,0),
			fSoTienKhamThaiKHHGD=ISNULL(DetailHSQBS.fSoTienKhamThaiKHHGD,0),
			iSoNgayPHSKThaiSan=ISNULL(DetailHSQBS.iSoNgayPHSKThaiSan,0),
			fSoTienPHSKThaiSan=ISNULL(DetailHSQBS.fSoTienPHSKThaiSan,0)
			FROM #tempHSQBSDuToan HSQBS,
			(SELECT 
			Sum(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon
			, Sum(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon
			, Sum(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon
			, Sum(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon
			, Sum(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD
			, Sum(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD
			, Sum(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan
			, Sum(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
			FROM #tempDetalHSQBSDuToan) DetailHSQBS

			update #tempLDHDDuToan
			set iSoNgaySinhConNNuoiCon=ISNULL(DetailLDHD.iSoNgaySinhConNNuoiCon,0),
			fSoTienSinhConNNuoiCon=ISNULL(DetailLDHD.fSoTienSinhConNNuoiCon,0),
			iSoNgaySinhTroCapSinhCon=ISNULL(DetailLDHD.iSoNgaySinhTroCapSinhCon,0),
			fSoTienSinhTroCapSinhCon=ISNULL(DetailLDHD.fSoTienSinhTroCapSinhCon,0),
			iSoNgayKhamThaiKHHGD=ISNULL(DetailLDHD.iSoNgayKhamThaiKHHGD,0),
			fSoTienKhamThaiKHHGD=ISNULL(DetailLDHD.fSoTienKhamThaiKHHGD,0),
			iSoNgayPHSKThaiSan=ISNULL(DetailLDHD.iSoNgayPHSKThaiSan,0),
			fSoTienPHSKThaiSan=ISNULL(DetailLDHD.fSoTienPHSKThaiSan,0)
			FROM #tempLDHDDuToan LDHD,
			(SELECT 
			Sum(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon
			, Sum(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon
			, Sum(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon
			, Sum(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon
			, Sum(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD
			, Sum(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD
			, Sum(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan
			, Sum(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
			FROM #tempDetalLDHDDuToan) DetailLDHD
		----- Update Du Toan : SQ,QNCN,CNVCQP,HSQBS,LDHD
					update SQ
			set SQ.iSoNgaySinhConNNuoiCon=ISNULL(DetailSQ.iSoNgaySinhConNNuoiCon,0),
			SQ.fSoTienSinhConNNuoiCon=ISNULL(DetailSQ.fSoTienSinhConNNuoiCon,0),
			SQ.iSoNgaySinhTroCapSinhCon=ISNULL(DetailSQ.iSoNgaySinhTroCapSinhCon,0),
			SQ.fSoTienSinhTroCapSinhCon=ISNULL(DetailSQ.fSoTienSinhTroCapSinhCon,0),
			SQ.iSoNgayKhamThaiKHHGD=ISNULL(DetailSQ.iSoNgayKhamThaiKHHGD,0),
			SQ.fSoTienKhamThaiKHHGD=ISNULL(DetailSQ.fSoTienKhamThaiKHHGD,0),
			SQ.iSoNgayPHSKThaiSan=ISNULL(DetailSQ.iSoNgayPHSKThaiSan,0),
			SQ.fSoTienPHSKThaiSan=ISNULL(DetailSQ.fSoTienPHSKThaiSan,0)
			FROM #tempSQHachToan SQ,
			(SELECT 
			Sum(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon
			, Sum(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon
			, Sum(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon
			, Sum(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon
			, Sum(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD
			, Sum(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD
			, Sum(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan
			, Sum(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
			, 2 type
			FROM #tempDetalSQHachToan) DetailSQ
			where DetailSQ.type=SQ.type

			update QNCN
			set QNCN.iSoNgaySinhConNNuoiCon=ISNULL(DetailQNCN.iSoNgaySinhConNNuoiCon,0),
			QNCN.fSoTienSinhConNNuoiCon=ISNULL(DetailQNCN.fSoTienSinhConNNuoiCon,0),
			QNCN.iSoNgaySinhTroCapSinhCon=ISNULL(DetailQNCN.iSoNgaySinhTroCapSinhCon,0),
			QNCN.fSoTienSinhTroCapSinhCon=ISNULL(DetailQNCN.fSoTienSinhTroCapSinhCon,0),
			QNCN.iSoNgayKhamThaiKHHGD=ISNULL(DetailQNCN.iSoNgayKhamThaiKHHGD,0),
			QNCN.fSoTienKhamThaiKHHGD=ISNULL(DetailQNCN.fSoTienKhamThaiKHHGD,0),
			QNCN.iSoNgayPHSKThaiSan=ISNULL(DetailQNCN.iSoNgayPHSKThaiSan,0),
			QNCN.fSoTienPHSKThaiSan=ISNULL(DetailQNCN.fSoTienPHSKThaiSan,0)
			FROM #tempQNCNHachToan QNCN,
			(SELECT 
			Sum(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon
			, Sum(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon
			, Sum(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon
			, Sum(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon
			, Sum(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD
			, Sum(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD
			, Sum(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan
			, Sum(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
			FROM #tempDetalQNCNHachToan) DetailQNCN

			update CNVCQP
			set CNVCQP.iSoNgaySinhConNNuoiCon=ISNULL(DetailCNVCQP.iSoNgaySinhConNNuoiCon,0),
			CNVCQP.fSoTienSinhConNNuoiCon=ISNULL(DetailCNVCQP.fSoTienSinhConNNuoiCon,0),
			CNVCQP.iSoNgaySinhTroCapSinhCon=ISNULL(DetailCNVCQP.iSoNgaySinhTroCapSinhCon,0),
			CNVCQP.fSoTienSinhTroCapSinhCon=ISNULL(DetailCNVCQP.fSoTienSinhTroCapSinhCon,0),
			CNVCQP.iSoNgayKhamThaiKHHGD=ISNULL(DetailCNVCQP.iSoNgayKhamThaiKHHGD,0),
			CNVCQP.fSoTienKhamThaiKHHGD=ISNULL(DetailCNVCQP.fSoTienKhamThaiKHHGD,0),
			CNVCQP.iSoNgayPHSKThaiSan=ISNULL(DetailCNVCQP.iSoNgayPHSKThaiSan,0),
			CNVCQP.fSoTienPHSKThaiSan=ISNULL(DetailCNVCQP.fSoTienPHSKThaiSan,0)
			FROM #tempCNVCQPHachToan CNVCQP,
			(SELECT 
			Sum(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon
			, Sum(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon
			, Sum(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon
			, Sum(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon
			, Sum(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD
			, Sum(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD
			, Sum(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan
			, Sum(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
			FROM #tempDetalCNVCQPHachToan) DetailCNVCQP

			update #tempHSQBSHachToan
			set iSoNgaySinhConNNuoiCon=ISNULL(DetailHSQBS.iSoNgaySinhConNNuoiCon,0),
			fSoTienSinhConNNuoiCon=ISNULL(DetailHSQBS.fSoTienSinhConNNuoiCon,0),
			iSoNgaySinhTroCapSinhCon=ISNULL(DetailHSQBS.iSoNgaySinhTroCapSinhCon,0),
			fSoTienSinhTroCapSinhCon=ISNULL(DetailHSQBS.fSoTienSinhTroCapSinhCon,0),
			iSoNgayKhamThaiKHHGD=ISNULL(DetailHSQBS.iSoNgayKhamThaiKHHGD,0),
			fSoTienKhamThaiKHHGD=ISNULL(DetailHSQBS.fSoTienKhamThaiKHHGD,0),
			iSoNgayPHSKThaiSan=ISNULL(DetailHSQBS.iSoNgayPHSKThaiSan,0),
			fSoTienPHSKThaiSan=ISNULL(DetailHSQBS.fSoTienPHSKThaiSan,0)
			FROM #tempHSQBSHachToan HSQBS,
			(SELECT 
			Sum(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon
			, Sum(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon
			, Sum(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon
			, Sum(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon
			, Sum(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD
			, Sum(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD
			, Sum(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan
			, Sum(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
			FROM #tempDetalHSQBSHachToan) DetailHSQBS

			update #tempLDHDHachToan
			set iSoNgaySinhConNNuoiCon=ISNULL(DetailLDHD.iSoNgaySinhConNNuoiCon,0),
			fSoTienSinhConNNuoiCon=ISNULL(DetailLDHD.fSoTienSinhConNNuoiCon,0),
			iSoNgaySinhTroCapSinhCon=ISNULL(DetailLDHD.iSoNgaySinhTroCapSinhCon,0),
			fSoTienSinhTroCapSinhCon=ISNULL(DetailLDHD.fSoTienSinhTroCapSinhCon,0),
			iSoNgayKhamThaiKHHGD=ISNULL(DetailLDHD.iSoNgayKhamThaiKHHGD,0),
			fSoTienKhamThaiKHHGD=ISNULL(DetailLDHD.fSoTienKhamThaiKHHGD,0),
			iSoNgayPHSKThaiSan=ISNULL(DetailLDHD.iSoNgayPHSKThaiSan,0),
			fSoTienPHSKThaiSan=ISNULL(DetailLDHD.fSoTienPHSKThaiSan,0)
			FROM #tempLDHDHachToan LDHD,
			(SELECT 
			Sum(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon
			, Sum(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon
			, Sum(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon
			, Sum(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon
			, Sum(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD
			, Sum(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD
			, Sum(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan
			, Sum(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
			FROM #tempDetalLDHDHachToan) DetailLDHD

---Tra ve kqua DuToan
			SELECT * INTO #tempkqDuToan FROM
			(
					SELECT * FROM #tempDuToan
					UNION ALL
					SELECT * FROM  #tempSQDuToan
					UNION ALL
					SELECT * FROM  #tempDetalSQDuToan
					UNION ALL
					SELECT * FROM  #tempQNCNDuToan
					UNION ALL
					SELECT * FROM  #tempDetalQNCNDuToan
					UNION ALL
					SELECT * FROM  #tempCNVCQPDuToan
					UNION ALL
					SELECT * FROM  #tempDetalCNVCQPDuToan
					UNION ALL
					SELECT * FROM  #tempHSQBSDuToan
					UNION ALL
					SELECT * FROM  #tempDetalHSQBSDuToan
					UNION ALL
					SELECT * FROM  #tempLDHDDuToan
					UNION ALL
					SELECT * FROM  #tempDetalLDHDHachToan
			) TEMPDuToan

------ Update total khoi du toan
			SELECT 
			Sum(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon
			, Sum(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon
			, Sum(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon
			, Sum(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon
			, Sum(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD
			, Sum(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD
			, Sum(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan
			, Sum(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
			, 3 type
			INTO #tempTotalDuToan
			FROM #tempkqDuToan
			WHERE type=2

			update T1
			set T1.iSoNgaySinhConNNuoiCon=T2.iSoNgaySinhConNNuoiCon,
			T1.fSoTienSinhConNNuoiCon=T2.fSoTienSinhConNNuoiCon,
			T1.iSoNgaySinhTroCapSinhCon=T2.iSoNgaySinhTroCapSinhCon,
			T1.fSoTienSinhTroCapSinhCon=T2.fSoTienSinhTroCapSinhCon,
			T1.iSoNgayKhamThaiKHHGD=T2.iSoNgayKhamThaiKHHGD,
			T1.fSoTienKhamThaiKHHGD=T2.fSoTienKhamThaiKHHGD,
			T1.iSoNgayPHSKThaiSan=T2.iSoNgayPHSKThaiSan,
			T1.fSoTienPHSKThaiSan=T2.fSoTienPHSKThaiSan
			FROM #tempkqDuToan T1, #tempTotalDuToan T2
			WHERE T1.type=T2.type
			and T1.STT=N'I. Khối dự toán'

---------  Tra ve kqua HachToan
			SELECT * INTO #tempkqHachToan FROM
			(
					SELECT * FROM #tempHachToan
					UNION ALL
					SELECT * FROM  #tempSQHachToan
					UNION ALL
					SELECT * FROM  #tempDetalSQHachToan
					UNION ALL
					SELECT * FROM  #tempQNCNHachToan
					UNION ALL
					SELECT * FROM  #tempDetalQNCNHachToan
					UNION ALL
					SELECT * FROM  #tempCNVCQPHachToan
					UNION ALL
					SELECT * FROM  #tempDetalCNVCQPHachToan
					UNION ALL
					SELECT * FROM  #tempHSQBSHachToan
					UNION ALL
					SELECT * FROM  #tempDetalHSQBSHachToan
					UNION ALL
					SELECT * FROM  #tempLDHDHachToan
					UNION ALL
					SELECT * FROM  #tempDetalLDHDHachToan
			) TEMPHachToan

			------ Update total khoi hach toan
			SELECT 
			Sum(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon
			, Sum(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon
			, Sum(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon
			, Sum(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon
			, Sum(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD
			, Sum(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD
			, Sum(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan
			, Sum(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
			, 3 type
			INTO #tempTotalHachToan
			FROM #tempkqHachToan
			WHERE type=2

			UPDATE  T1
			set T1.iSoNgaySinhConNNuoiCon=T2.iSoNgaySinhConNNuoiCon,
			T1.fSoTienSinhConNNuoiCon=T2.fSoTienSinhConNNuoiCon,
			T1.iSoNgaySinhTroCapSinhCon=T2.iSoNgaySinhTroCapSinhCon,
			T1.fSoTienSinhTroCapSinhCon=T2.fSoTienSinhTroCapSinhCon,
			T1.iSoNgayKhamThaiKHHGD=T2.iSoNgayKhamThaiKHHGD,
			T1.fSoTienKhamThaiKHHGD=T2.fSoTienKhamThaiKHHGD,
			T1.iSoNgayPHSKThaiSan=T2.iSoNgayPHSKThaiSan,
			T1.fSoTienPHSKThaiSan=T2.fSoTienPHSKThaiSan
			FROM #tempkqHachToan T1, #tempTotalHachToan T2
			WHERE T1.type=T2.type
			and T1.STT=N'II. Khối hạch toán'

		SELECT * INTO #tempKQAll FROM
			(
					SELECT * FROM #tempkqDuToan
					UNION ALL
					SELECT * FROM  #tempkqHachToan
					
			) TEMPKQAll

		select sTT
		, iID_MaDonVi
		,sTenDonVi
		,iSoNgaySinhConNNuoiCon
		,fSoTienSinhConNNuoiCon/@Donvitinh fSoTienSinhConNNuoiCon
		,iSoNgaySinhTroCapSinhCon
		,fSoTienSinhTroCapSinhCon/@Donvitinh fSoTienSinhTroCapSinhCon
		,iSoNgayKhamThaiKHHGD
		,fSoTienKhamThaiKHHGD/@Donvitinh fSoTienKhamThaiKHHGD
		,iSoNgayPHSKThaiSan
		,fSoTienPHSKThaiSan/@Donvitinh fSoTienPHSKThaiSan
		,(fSoTienSinhConNNuoiCon+fSoTienSinhTroCapSinhCon+fSoTienKhamThaiKHHGD+fSoTienPHSKThaiSan)/@Donvitinh as fTongTien
		,IsHangCha
		,BHangCha
		,type
		,IsParent
		from #tempKQAll

		drop table #TBL_TroCapThaiSanDuToan;
		drop table #TBL_TroCapThaiSanHachToan;
		drop table #tempDuToan
		drop table #tempSQDuToan
		drop table #tempQNCNDuToan
		drop table #tempCNVCQPDuToan
		drop table #tempHSQBSDuToan
		drop table #tempLDHDDuToan
		DROP TABLE #tempDetalSQDuToan
		DROP TABLE #tempDetalQNCNDuToan
		DROP TABLE #tempDetalCNVCQPDuToan
		DROP TABLE #tempDetalHSQBSDuToan
		DROP TABLE #tempDetalLDHDDuToan
		DROP TABLE #tempTotalDuToan

		drop table #tempHachToan
		drop table #tempSQHachToan
		drop table #tempQNCNHachToan
		drop table #tempCNVCQPHachToan
		drop table #tempHSQBSHachToan
		drop table #tempLDHDHachToan
		DROP TABLE #tempDetalSQHachToan
		DROP TABLE #tempDetalQNCNHachToan
		DROP TABLE #tempDetalCNVCQPHachToan
		DROP TABLE #tempDetalHSQBSHachToan
		DROP TABLE #tempDetalLDHDHachToan
		DROP TABLE #tempTotalHachToan

		DROP TABLE #tempkqDuToan
		DROP TABLE #tempkqHachToan
		DROP TABLE #tempKQAll

end
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_bhxh]    Script Date: 7/12/2024 8:13:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_bhxh]
@INamLamViec int,
@IdMaDonVi nvarchar(max),
@IQuy int,
@Donvitinh int
as
begin
	---Lấy thông tin chi tiết giai thich tro cap
	SELECT gt.* into #TBL_TroCapTaiNan 
			FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gt
			LEFT JOIN BH_QTC_Quy_CheDoBHXH ct ON gt.iID_QTC_Quy_ChungTu=ct.ID_QTC_Quy_CheDoBHXH
			LEFT JOIN DonVi dv on dv.iID_MaDonVi=ct.iID_MaDonVi
			WHERE (gt.sXauNoiMa like '9010001-010-011-0003%' or gt.sXauNoiMa like '9010002-010-011-0003%')
					AND gt.iNamLamViec = @INamLamViec and gt.iQuy = @IQuy 
					AND dv.iNamLamViec=@INamLamViec
					AND dv.iID_MaDonVi  IN (select * from dbo.splitstring(@IdMaDonVi)) 

		SELECT
			ROW_NUMBER() OVER(ORDER BY tbltctn.sTenCanBo ASC) AS STT
			,tbltctn.sTenCanBo
			,tbltctn.sTenPhanHo
			, tbltctn.sMaCapBac
			, tbltctn.sSoQuyetDinh
			, CONVERT(varchar, (tbltctn.dNgayQuyetDinh), 101) As SNgayQuyetDinh

			---- Chi giám định mức suy giảm KNLĐ (người)1
			, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) /@Donvitinh ELSE 0 END 
			+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END) FTienGiamDinh
			
			
			---- Chi giám định mức suy giảm KNLĐ (người)1 truy lĩnh
			, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) /@Donvitinh ELSE 0 END 
			+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienGiamDinhTL

			---- Trợ cấp 1 lần (người)2
			, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END) FTienTroCap1Lan

			---- Trợ cấp 1 lần (người)2 truy lĩnh
			, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTroCap1LanTL


			--- - Chi hỗ Trợ phòng người (người)
			-- - Chi h.trợ chuyển đổi n.nghiệp (người)
			, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END) FTienTCTP
			
			--- - Chi hỗ Trợ phòng người (người) truy linh
			-- - Chi h.trợ chuyển đổi n.nghiệp (người) truy linh
			, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTCTPTL
			
			---- Trợ cấp hàng tháng (người)
			, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end 
			+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end ) FTienTCHangThang
			
			---- Trợ cấp hàng tháng (người) truy linh
			, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end 
			+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end ) FTienTCHangThangTL
			
			--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người)
			,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPV

			--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người) truy linh
			,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPVTL

			--- - Trợ cấp chết do TNLD. BNN (người) 
			,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh  ELSE 0 END )FTienTCCDTNLD

			--- - Trợ cấp chết do TNLD. BNN (người) truy linh
			,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh  ELSE 0 END )FTienTCCDTNLDTL

			--- - DS, PHSK sau TNLĐ, BNN (người)
			,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END 
			+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ) ISoNgayDSPHSK

			--- - DS, PHSK sau TNLĐ, BNN (người) truy linh
			,( CASE WHEN tbltctn.sXauNoiMa like '9010001-010-011-0003%' THEN Sum(tbltctn.iSoNgayTruyLinh)  ELSE 0 END 
			+ CASE WHEN tbltctn.sXauNoiMa like '9010002-010-011-0003%' THEN Sum(tbltctn.iSoNgayTruyLinh)  ELSE 0 END ) ISoNgayDSPHSKTL

			,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END) FTienDSPHSK
			
			,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienDSPHSKTL,
			0 IsHangCha
		FROM #TBL_TroCapTaiNan tbltctn
		GROUP BY tbltctn.sTenCanBo,
			tbltctn.sTenPhanHo,
			tbltctn.sMaCapBac
			, tbltctn.sSoQuyetDinh
			, tbltctn.dNgayQuyetDinh
			,tbltctn.sXauNoiMa 
		

		DROP TABLE #TBL_TroCapTaiNan


end
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_khoi]    Script Date: 7/12/2024 8:13:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_khoi]
@INamLamViec int,
@IdMaDonVi nvarchar(max),
@IQuy int,
@Donvitinh int
as
begin
	---Lấy thông tin chi tiết giai thich tro cap du toan
	SELECT gt.* into #TBL_TroCapTaiNanDuToan 
			FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gt
			LEFT JOIN BH_QTC_Quy_CheDoBHXH ct ON gt.iID_QTC_Quy_ChungTu=ct.ID_QTC_Quy_CheDoBHXH
			LEFT JOIN DonVi dv on dv.iID_MaDonVi=ct.iID_MaDonVi
			WHERE (gt.sXauNoiMa like '9010001-010-011-0003%' or gt.sXauNoiMa like '9010002-010-011-0003%')
					AND gt.iNamLamViec = @INamLamViec and gt.iQuy = @IQuy 
					AND dv.iNamLamViec=@INamLamViec
					AND dv.iID_MaDonVi  IN (select * from dbo.splitstring(@IdMaDonVi)) 
					AND dv.iKhoi=2
					AND ct.iNamChungTu=@INamLamViec
	---Lấy thông tin chi tiết giai thich tro cap hach toan
	SELECT gt.* into #TBL_TroCapTaiNanHachToan 
			FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gt
			LEFT JOIN BH_QTC_Quy_CheDoBHXH ct ON gt.iID_QTC_Quy_ChungTu=ct.ID_QTC_Quy_CheDoBHXH
			LEFT JOIN DonVi dv on dv.iID_MaDonVi=ct.iID_MaDonVi
			WHERE (gt.sXauNoiMa like '9010001-010-011-0003%' or gt.sXauNoiMa like '9010002-010-011-0003%')
					AND gt.iNamLamViec = @INamLamViec and gt.iQuy = @IQuy 
					AND dv.iNamLamViec=@INamLamViec
					AND dv.iID_MaDonVi  IN (select * from dbo.splitstring(@IdMaDonVi)) 
					AND dv.iKhoi=1
					AND ct.iNamChungTu=@INamLamViec

	---I. Khối dự toán
		SELECT 
		N'I. Khối dự toán' As STT
		,'' As sTenCanBo
		,'' As sTenPhanHo
		,'' As sMaCapBac
		,'' As sSoQuyetDinh
		,'' As SNgayQuyetDinh
		,0 As FTienGiamDinh
		,0 As FTienTroCap1Lan
		,0 As FTienTCTP
		,0 As FTienTCHangThang
		,0 As FTienTCPHCNvPV
		,0 as FTienTCCDTNLD
		,0 as ISoNgayDSPHSK
		,0 as FTienDSPHSK
		,0 As FTienGiamDinhTL
		,0 As FTienTroCap1LanTL
		,0 As FTienTCTPTL
		,0 As FTienTCHangThangTL
		,0 As FTienTCPHCNvPVTL
		,0 as FTienTCCDTNLDTL
		,0 as ISoNgayDSPHSKTL
		,0 as FTienDSPHSKTL
		,3 as type
		,1 IsHangCha
		,1 IsParent
		into #tempDuToan

		--- Total SQ DuToan
		SELECT 
		N'1. Sĩ quan' As STT
		,'' As sTenCanBo
		,'' As sTenPhanHo
		,'' As sMaCapBac
		,'' As sSoQuyetDinh
		,'' As SNgayQuyetDinh
		,0 As FTienGiamDinh
		,0 As FTienTroCap1Lan
		,0 As FTienTCTP
		,0 As FTienTCHangThang
		,0 As FTienTCPHCNvPV
		,0 as FTienTCCDTNLD
		,0 as ISoNgayDSPHSK
		,0 as FTienDSPHSK
		,0 As FTienGiamDinhTL
		,0 As FTienTroCap1LanTL
		,0 As FTienTCTPTL
		,0 As FTienTCHangThangTL
		,0 As FTienTCPHCNvPVTL
		,0 as FTienTCCDTNLDTL
		,0 as ISoNgayDSPHSKTL
		,0 as FTienDSPHSKTL
		,2 as type
		,1 IsHangCha
		,1 IsParent
		into #tempSQDuToan

		--- Total QNCN DuToan
		SELECT 
		N'2. QNCN' As STT
		,'' As sTenCanBo
		,'' As sTenPhanHo
		,'' As sMaCapBac
		,'' As sSoQuyetDinh
		,'' As SNgayQuyetDinh
		,0 As FTienGiamDinh
		,0 As FTienTroCap1Lan
		,0 As FTienTCTP
		,0 As FTienTCHangThang
		,0 As FTienTCPHCNvPV
		,0 as FTienTCCDTNLD
		,0 as ISoNgayDSPHSK
		,0 as FTienDSPHSK
		,0 As FTienGiamDinhTL
		,0 As FTienTroCap1LanTL
		,0 As FTienTCTPTL
		,0 As FTienTCHangThangTL
		,0 As FTienTCPHCNvPVTL
		,0 as FTienTCCDTNLDTL
		,0 as ISoNgayDSPHSKTL
		,0 as FTienDSPHSKTL
		,2 as type
		,1 IsHangCha
		,1 IsParent
		into #tempQNCNDuToan

		--- Total CNVCQP DuToan
		SELECT 
		N'3. CNVCQP' As STT
		,'' As sTenCanBo
		,'' As sTenPhanHo
		,'' As sMaCapBac
		,'' As sSoQuyetDinh
		,'' As SNgayQuyetDinh
		,0 As FTienGiamDinh
		,0 As FTienTroCap1Lan
		,0 As FTienTCTP
		,0 As FTienTCHangThang
		,0 As FTienTCPHCNvPV
		,0 as FTienTCCDTNLD
		,0 as ISoNgayDSPHSK
		,0 as FTienDSPHSK
		,0 As FTienGiamDinhTL
		,0 As FTienTroCap1LanTL
		,0 As FTienTCTPTL
		,0 As FTienTCHangThangTL
		,0 As FTienTCPHCNvPVTL
		,0 as FTienTCCDTNLDTL
		,0 as ISoNgayDSPHSKTL
		,0 as FTienDSPHSKTL
		,2 as type
		,1 IsHangCha
		,1 IsParent
		into #tempCNVCQPDuToan

		--- Total HSQBS  DuToan
		SELECT 
		N'4. HSQBS' As STT
		,'' As sTenCanBo
		,'' As sTenPhanHo
		,'' As sMaCapBac
		,'' As sSoQuyetDinh
		,'' As SNgayQuyetDinh
		,0 As FTienGiamDinh
		,0 As FTienTroCap1Lan
		,0 As FTienTCTP
		,0 As FTienTCHangThang
		,0 As FTienTCPHCNvPV
		,0 as FTienTCCDTNLD
		,0 as ISoNgayDSPHSK
		,0 as FTienDSPHSK
		,0 As FTienGiamDinhTL
		,0 As FTienTroCap1LanTL
		,0 As FTienTCTPTL
		,0 As FTienTCHangThangTL
		,0 As FTienTCPHCNvPVTL
		,0 as FTienTCCDTNLDTL
		,0 as ISoNgayDSPHSKTL
		,0 as FTienDSPHSKTL
		,2 as type
		,1 IsHangCha
		,1 IsParent
		into #tempHSQBSDuToan

		---	Total LDHD DuToan
		SELECT 
		N'5. LDHD' As STT
		,'' As sTenCanBo
		,'' As sTenPhanHo
		,'' As sMaCapBac
		,'' As sSoQuyetDinh
		,'' As SNgayQuyetDinh
		,0 As FTienGiamDinh
		,0 As FTienTroCap1Lan
		,0 As FTienTCTP
		,0 As FTienTCHangThang
		,0 As FTienTCPHCNvPV
		,0 as FTienTCCDTNLD
		,0 as ISoNgayDSPHSK
		,0 as FTienDSPHSK
		,0 As FTienGiamDinhTL
		,0 As FTienTroCap1LanTL
		,0 As FTienTCTPTL
		,0 As FTienTCHangThangTL
		,0 As FTienTCPHCNvPVTL
		,0 as FTienTCCDTNLDTL
		,0 as ISoNgayDSPHSKTL
		,0 as FTienDSPHSKTL
		,2 as type
		,1 IsHangCha
		,1 IsParent
		into #tempLDHDDuToan		

	---II. Khối hạch toán
		SELECT 
		N'II. Khối hạch toán' As STT
		,'' As sTenCanBo
		,'' As sTenPhanHo
		,'' As sMaCapBac
		,'' As sSoQuyetDinh
		,'' As SNgayQuyetDinh
		,0 As FTienGiamDinh
		,0 As FTienTroCap1Lan
		,0 As FTienTCTP
		,0 As FTienTCHangThang
		,0 As FTienTCPHCNvPV
		,0 as FTienTCCDTNLD
		,0 as ISoNgayDSPHSK
		,0 as FTienDSPHSK
		,0 As FTienGiamDinhTL
		,0 As FTienTroCap1LanTL
		,0 As FTienTCTPTL
		,0 As FTienTCHangThangTL
		,0 As FTienTCPHCNvPVTL
		,0 as FTienTCCDTNLDTL
		,0 as ISoNgayDSPHSKTL
		,0 as FTienDSPHSKTL
		,3 as type
		,1 IsHangCha
		,1 IsParent
		into #tempHachToan
		--- Total SQ HachToan
		SELECT 
		N'1. Sĩ quan' As STT
		,'' As sTenCanBo
		,'' As sTenPhanHo
		,'' As sMaCapBac
		,'' As sSoQuyetDinh
		,'' As SNgayQuyetDinh
		,0 As FTienGiamDinh
		,0 As FTienTroCap1Lan
		,0 As FTienTCTP
		,0 As FTienTCHangThang
		,0 As FTienTCPHCNvPV
		,0 as FTienTCCDTNLD
		,0 as ISoNgayDSPHSK
		,0 as FTienDSPHSK
		,0 As FTienGiamDinhTL
		,0 As FTienTroCap1LanTL
		,0 As FTienTCTPTL
		,0 As FTienTCHangThangTL
		,0 As FTienTCPHCNvPVTL
		,0 as FTienTCCDTNLDTL
		,0 as ISoNgayDSPHSKTL
		,0 as FTienDSPHSKTL
		,2 as type
		,1 IsHangCha
		,1 IsParent
		into #tempSQHachToan

		--- Total QNCN HachToan
		SELECT 
		N'2. QNCN' As STT
		,'' As sTenCanBo
		,'' As sTenPhanHo
		,'' As sMaCapBac
		,'' As sSoQuyetDinh
		,'' As SNgayQuyetDinh
		,0 As FTienGiamDinh
		,0 As FTienTroCap1Lan
		,0 As FTienTCTP
		,0 As FTienTCHangThang
		,0 As FTienTCPHCNvPV
		,0 as FTienTCCDTNLD
		,0 as ISoNgayDSPHSK
		,0 as FTienDSPHSK
		,0 As FTienGiamDinhTL
		,0 As FTienTroCap1LanTL
		,0 As FTienTCTPTL
		,0 As FTienTCHangThangTL
		,0 As FTienTCPHCNvPVTL
		,0 as FTienTCCDTNLDTL
		,0 as ISoNgayDSPHSKTL
		,0 as FTienDSPHSKTL
		,2 as type
		,1 IsHangCha
		,1 IsParent
		into #tempQNCNHachToan

		--- Total CNVCQP HachToan
		SELECT 
		N'3. CNVCQP' As STT
		,'' As sTenCanBo
		,'' As sTenPhanHo
		,'' As sMaCapBac
		,'' As sSoQuyetDinh
		,'' As SNgayQuyetDinh
		,0 As FTienGiamDinh
		,0 As FTienTroCap1Lan
		,0 As FTienTCTP
		,0 As FTienTCHangThang
		,0 As FTienTCPHCNvPV
		,0 as FTienTCCDTNLD
		,0 as ISoNgayDSPHSK
		,0 as FTienDSPHSK
		,0 As FTienGiamDinhTL
		,0 As FTienTroCap1LanTL
		,0 As FTienTCTPTL
		,0 As FTienTCHangThangTL
		,0 As FTienTCPHCNvPVTL
		,0 as FTienTCCDTNLDTL
		,0 as ISoNgayDSPHSKTL
		,0 as FTienDSPHSKTL
		,2 as type
		,1 IsHangCha
		,1 IsParent
		into #tempCNVCQPHachToan

		--- Total HSQBS  HachToan
		SELECT 
		N'4. HSQBS' As STT
		,'' As sTenCanBo
		,'' As sTenPhanHo
		,'' As sMaCapBac
		,'' As sSoQuyetDinh
		,'' As SNgayQuyetDinh
		,0 As FTienGiamDinh
		,0 As FTienTroCap1Lan
		,0 As FTienTCTP
		,0 As FTienTCHangThang
		,0 As FTienTCPHCNvPV
		,0 as FTienTCCDTNLD
		,0 as ISoNgayDSPHSK
		,0 as FTienDSPHSK
		,0 As FTienGiamDinhTL
		,0 As FTienTroCap1LanTL
		,0 As FTienTCTPTL
		,0 As FTienTCHangThangTL
		,0 As FTienTCPHCNvPVTL
		,0 as FTienTCCDTNLDTL
		,0 as ISoNgayDSPHSKTL
		,0 as FTienDSPHSKTL
		,2 as type
		,1 IsHangCha
		,1 IsParent
		into #tempHSQBSHachToan

		--- Total LDHD HachToan
		SELECT 
		N'5. LDHD' As STT
		,'' As sTenCanBo
		,'' As sTenPhanHo
		,'' As sMaCapBac
		,'' As sSoQuyetDinh
		,'' As SNgayQuyetDinh
		,0 As FTienGiamDinh
		,0 As FTienTroCap1Lan
		,0 As FTienTCTP
		,0 As FTienTCHangThang
		,0 As FTienTCPHCNvPV
		,0 as FTienTCCDTNLD
		,0 as ISoNgayDSPHSK
		,0 as FTienDSPHSK
		,0 As FTienGiamDinhTL
		,0 As FTienTroCap1LanTL
		,0 As FTienTCTPTL
		,0 As FTienTCHangThangTL
		,0 As FTienTCPHCNvPVTL
		,0 as FTienTCCDTNLDTL
		,0 as ISoNgayDSPHSKTL
		,0 as FTienDSPHSKTL
		,2 as type
		,1 IsHangCha
		,1 IsParent
		into #tempLDHDHachToan	

	----- Lay ra  du toan
		-- Du Toan SQ
			SELECT
				 CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY tbltctn.sTenCanBo ASC))) AS STT
				,tbltctn.sTenCanBo
				,tbltctn.sTenPhanHo
				, tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, CONVERT(varchar, (tbltctn.dNgayQuyetDinh), 101) As SNgayQuyetDinh

				---- Chi giám định mức suy giảm KNLĐ (người)1
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) /@Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END) FTienGiamDinh
			
				---- Trợ cấp 1 lần (người)2
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END) FTienTroCap1Lan


				--- - Chi hỗ Trợ phòng người (người)
				-- - Chi h.trợ chuyển đổi n.nghiệp (người)
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END) FTienTCTP
			
				---- Trợ cấp hàng tháng (người)
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end )FTienTCHangThang
			
				--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPV
				--- - Trợ cấp chết do TNLD. BNN (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh  ELSE 0 END )FTienTCCDTNLD
				--- - DS, PHSK sau TNLĐ, BNN (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ) ISoNgayDSPHSK

				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END) FTienDSPHSK

				---- Chi giám định mức suy giảm KNLĐ (người)1 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) /@Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienGiamDinhTL
			
				---- Trợ cấp 1 lần (người)2 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTroCap1LanTL


				--- - Chi hỗ Trợ phòng người (người) truy linh
				-- - Chi h.trợ chuyển đổi n.nghiệp (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTCTPTL
			
				---- Trợ cấp hàng tháng (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end )FTienTCHangThangTL
			
				--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPVTL
				--- - Trợ cấp chết do TNLD. BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh  ELSE 0 END )FTienTCCDTNLDTL
				--- - DS, PHSK sau TNLĐ, BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa LIKE '9010001-010-011-0003%' THEN Sum(tbltctn.iSoNgayTruyLinh)  ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa LIKE '9010002-010-011-0003%' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ) ISoNgayDSPHSKTL

				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienDSPHSKTL
				,1 as type
				,0 IsHangCha
				,null IsParent
				into #tempDetailSQDuToan
			FROM #TBL_TroCapTaiNanDuToan tbltctn
			where tbltctn.sMaCapBac LIKE '1%'
			GROUP BY tbltctn.sTenCanBo,
				tbltctn.sTenPhanHo,
				tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.dNgayQuyetDinh
				,tbltctn.sXauNoiMa 

		-- Du Toan QNCN
			SELECT
				 CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY tbltctn.sTenCanBo ASC))) AS STT
				,tbltctn.sTenCanBo
				,tbltctn.sTenPhanHo
				, tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, CONVERT(varchar, (tbltctn.dNgayQuyetDinh), 101) As SNgayQuyetDinh

				---- Chi giám định mức suy giảm KNLĐ (người)1
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) /@Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END) FTienGiamDinh
			
				---- Trợ cấp 1 lần (người)2
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END) FTienTroCap1Lan


				--- - Chi hỗ Trợ phòng người (người)
				-- - Chi h.trợ chuyển đổi n.nghiệp (người)
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END) FTienTCTP
			
				---- Trợ cấp hàng tháng (người)
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end )FTienTCHangThang
			
				--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPV
				--- - Trợ cấp chết do TNLD. BNN (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh  ELSE 0 END )FTienTCCDTNLD
				--- - DS, PHSK sau TNLĐ, BNN (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ) ISoNgayDSPHSK

				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END) FTienDSPHSK

				---- Chi giám định mức suy giảm KNLĐ (người)1 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) /@Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienGiamDinhTL
			
				---- Trợ cấp 1 lần (người)2 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTroCap1LanTL

				--- - Chi hỗ Trợ phòng người (người) truy linh
				-- - Chi h.trợ chuyển đổi n.nghiệp (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTCTPTL
			
				---- Trợ cấp hàng tháng (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end )FTienTCHangThangTL
			
				--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPVTL
				--- - Trợ cấp chết do TNLD. BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh  ELSE 0 END )FTienTCCDTNLDTL
				--- - DS, PHSK sau TNLĐ, BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa LIKE '9010001-010-011-0003%' THEN Sum(tbltctn.iSoNgayTruyLinh)  ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa LIKE '9010002-010-011-0003%' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ) ISoNgayDSPHSKTL

				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienDSPHSKTL
				,1 as type
				,0 IsHangCha
				,null IsParent
				into  #tempDetailQNCNDuToan
			FROM #TBL_TroCapTaiNanDuToan tbltctn
			where tbltctn.sMaCapBac LIKE '2%'
			GROUP BY tbltctn.sTenCanBo,
				tbltctn.sTenPhanHo,
				tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.dNgayQuyetDinh
				,tbltctn.sXauNoiMa 

		-- Du Toan CNVCQP
			SELECT
				 CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY tbltctn.sTenCanBo ASC))) AS STT
				,tbltctn.sTenCanBo
				,tbltctn.sTenPhanHo
				, tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, CONVERT(varchar, (tbltctn.dNgayQuyetDinh), 101) As SNgayQuyetDinh

				---- Chi giám định mức suy giảm KNLĐ (người)1
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) /@Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END) FTienGiamDinh
			
				---- Trợ cấp 1 lần (người)2
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END) FTienTroCap1Lan


				--- - Chi hỗ Trợ phòng người (người)
				-- - Chi h.trợ chuyển đổi n.nghiệp (người)
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END) FTienTCTP
			
				---- Trợ cấp hàng tháng (người)
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end )FTienTCHangThang
			
				--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPV
				--- - Trợ cấp chết do TNLD. BNN (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh  ELSE 0 END )FTienTCCDTNLD
				--- - DS, PHSK sau TNLĐ, BNN (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ) ISoNgayDSPHSK

				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END) FTienDSPHSK

				---- Chi giám định mức suy giảm KNLĐ (người)1 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) /@Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienGiamDinhTL
			
				---- Trợ cấp 1 lần (người)2 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTroCap1LanTL


				--- - Chi hỗ Trợ phòng người (người) truy linh
				-- - Chi h.trợ chuyển đổi n.nghiệp (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTCTPTL
			
				---- Trợ cấp hàng tháng (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end )FTienTCHangThangTL
			
				--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPVTL
				--- - Trợ cấp chết do TNLD. BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh  ELSE 0 END )FTienTCCDTNLDTL
				--- - DS, PHSK sau TNLĐ, BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa LIKE '9010001-010-011-0003%' THEN Sum(tbltctn.iSoNgayTruyLinh)  ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa LIKE '9010002-010-011-0003%' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ) ISoNgayDSPHSKTL

				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienDSPHSKTL
				,1 as type
				,0 IsHangCha
				,null IsParent
				into #tempDetailCNVCQPDuToan
			FROM #TBL_TroCapTaiNanDuToan tbltctn
			where tbltctn.sMaCapBac LIKE '3.1%' OR tbltctn.sMaCapBac LIKE '3.2%' OR tbltctn.sMaCapBac LIKE '3.3%'  OR tbltctn.sMaCapBac = '413' OR tbltctn.sMaCapBac = '415'
			GROUP BY tbltctn.sTenCanBo,
				tbltctn.sTenPhanHo,
				tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.dNgayQuyetDinh
				,tbltctn.sXauNoiMa 

		-- Du Toan HSQBS
			SELECT
				 CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY tbltctn.sTenCanBo ASC))) AS STT
				,tbltctn.sTenCanBo
				,tbltctn.sTenPhanHo
				, tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, CONVERT(varchar, (tbltctn.dNgayQuyetDinh), 101) As SNgayQuyetDinh

				---- Chi giám định mức suy giảm KNLĐ (người)1
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) /@Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END) FTienGiamDinh
			
				---- Trợ cấp 1 lần (người)2
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END) FTienTroCap1Lan


				--- - Chi hỗ Trợ phòng người (người)
				-- - Chi h.trợ chuyển đổi n.nghiệp (người)
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END) FTienTCTP
			
				---- Trợ cấp hàng tháng (người)
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end )FTienTCHangThang
			
				--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPV
				--- - Trợ cấp chết do TNLD. BNN (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh  ELSE 0 END )FTienTCCDTNLD
				--- - DS, PHSK sau TNLĐ, BNN (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ) ISoNgayDSPHSK

				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END) FTienDSPHSK
				---- Chi giám định mức suy giảm KNLĐ (người)1 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) /@Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienGiamDinhTL
			
				---- Trợ cấp 1 lần (người)2 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTroCap1LanTL


				--- - Chi hỗ Trợ phòng người (người) truy linh
				-- - Chi h.trợ chuyển đổi n.nghiệp (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTCTPTL
			
				---- Trợ cấp hàng tháng (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end )FTienTCHangThangTL
			
				--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPVTL
				--- - Trợ cấp chết do TNLD. BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh  ELSE 0 END )FTienTCCDTNLDTL
				--- - DS, PHSK sau TNLĐ, BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa LIKE '9010001-010-011-0003%' THEN Sum(tbltctn.iSoNgayTruyLinh)  ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa LIKE '9010002-010-011-0003%' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ) ISoNgayDSPHSKTL

				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienDSPHSKTL
				,1 as type
				,0 IsHangCha
				,null IsParent
				 into #tempDetailHSQBSDuToan
			FROM #TBL_TroCapTaiNanDuToan tbltctn
			where tbltctn.sMaCapBac LIKE '0%'
			GROUP BY tbltctn.sTenCanBo,
				tbltctn.sTenPhanHo,
				tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.dNgayQuyetDinh
				,tbltctn.sXauNoiMa 

		-- Du Toan LDHD
			SELECT
				 CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY tbltctn.sTenCanBo ASC))) AS STT
				,tbltctn.sTenCanBo
				,tbltctn.sTenPhanHo
				, tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, CONVERT(varchar, (tbltctn.dNgayQuyetDinh), 101) As SNgayQuyetDinh

				---- Chi giám định mức suy giảm KNLĐ (người)1
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) /@Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END) FTienGiamDinh
			
				---- Trợ cấp 1 lần (người)2
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END) FTienTroCap1Lan


				--- - Chi hỗ Trợ phòng người (người)
				-- - Chi h.trợ chuyển đổi n.nghiệp (người)
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END) FTienTCTP
			
				---- Trợ cấp hàng tháng (người)
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end )FTienTCHangThang
			
				--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPV
				--- - Trợ cấp chết do TNLD. BNN (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh  ELSE 0 END )FTienTCCDTNLD
				--- - DS, PHSK sau TNLĐ, BNN (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ) ISoNgayDSPHSK

				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END) FTienDSPHSK
				
				---- Chi giám định mức suy giảm KNLĐ (người)1 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) /@Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienGiamDinhTL
				
				---- Trợ cấp 1 lần (người)2 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTroCap1LanTL

				--- - Chi hỗ Trợ phòng người (người) truy linh
				-- - Chi h.trợ chuyển đổi n.nghiệp (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTCTPTL
			
				---- Trợ cấp hàng tháng (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end )FTienTCHangThangTL
			
				--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPVTL
				--- - Trợ cấp chết do TNLD. BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh  ELSE 0 END )FTienTCCDTNLDTL
				--- - DS, PHSK sau TNLĐ, BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa LIKE '9010001-010-011-0003%' THEN Sum(tbltctn.iSoNgayTruyLinh)  ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa LIKE '9010002-010-011-0003%' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ) ISoNgayDSPHSKTL

				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienDSPHSKTL
				,1 as type
				,0 IsHangCha
				,null IsParent
				into #tempDetailLDHDDuToan
			FROM #TBL_TroCapTaiNanDuToan tbltctn
			where tbltctn.sMaCapBac = '423' OR tbltctn.sMaCapBac = '425' OR tbltctn.sMaCapBac = '43'
			GROUP BY tbltctn.sTenCanBo,
				tbltctn.sTenPhanHo,
				tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.dNgayQuyetDinh
				,tbltctn.sXauNoiMa 

	----- Update Du Toan : SQ,QNCN,CNVCQP,HSQBS,LDHD

			update SQ
			set SQ.FTienGiamDinh=ISNULL(DetailSQ.FTienGiamDinh,0),
			SQ.FTienTroCap1Lan=ISNULL(DetailSQ.FTienTroCap1Lan,0),
			SQ.FTienTCTP=ISNULL(DetailSQ.FTienTCTP,0),
			SQ.FTienTCHangThang=ISNULL(DetailSQ.FTienTCHangThang,0),
			SQ.FTienTCPHCNvPV=ISNULL(DetailSQ.FTienTCPHCNvPV,0),
			SQ.FTienTCCDTNLD=ISNULL(DetailSQ.FTienTCCDTNLD,0),
			SQ.ISoNgayDSPHSK=ISNULL(DetailSQ.ISoNgayDSPHSK,0),
			SQ.FTienDSPHSK=ISNULL(DetailSQ.FTienDSPHSK,0),
			SQ.FTienGiamDinhTL=ISNULL(DetailSQ.FTienGiamDinhTL,0),
			SQ.FTienTroCap1LanTL=ISNULL(DetailSQ.FTienTroCap1LanTL,0),
			SQ.FTienTCTPTL=ISNULL(DetailSQ.FTienTCTPTL,0),
			SQ.FTienTCHangThangTL=ISNULL(DetailSQ.FTienTCHangThangTL,0),
			SQ.FTienTCPHCNvPVTL=ISNULL(DetailSQ.FTienTCPHCNvPVTL,0),
			SQ.FTienTCCDTNLDTL=ISNULL(DetailSQ.FTienTCCDTNLDTL,0),
			SQ.ISoNgayDSPHSKTL=ISNULL(DetailSQ.ISoNgayDSPHSKTL,0),
			SQ.FTienDSPHSKTL=ISNULL(DetailSQ.FTienDSPHSKTL,0)
			FROM #tempSQDuToan SQ,
			(SELECT 
			Sum(FTienGiamDinh) FTienGiamDinh
			, Sum(FTienTroCap1Lan) FTienTroCap1Lan
			, Sum(FTienTCTP) FTienTCTP
			, Sum(FTienTCHangThang) FTienTCHangThang
			, Sum(FTienTCPHCNvPV) FTienTCPHCNvPV
			, Sum(FTienTCCDTNLD) FTienTCCDTNLD
			, Sum(ISoNgayDSPHSK) ISoNgayDSPHSK
			, Sum(FTienDSPHSK) FTienDSPHSK
			, Sum(FTienGiamDinhTL) FTienGiamDinhTL
			, Sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			, Sum(FTienTCTPTL) FTienTCTPTL
			, Sum(FTienTCHangThangTL) FTienTCHangThangTL
			, Sum(FTienTCPHCNvPVTL) FTienTCPHCNvPVTL
			, Sum(FTienTCCDTNLDTL) FTienTCCDTNLDTL
			, Sum(ISoNgayDSPHSKTL) ISoNgayDSPHSKTL
			, Sum(FTienDSPHSKTL) FTienDSPHSKTL
			, 2 type
			FROM #tempDetailSQDuToan) DetailSQ
			where DetailSQ.type=SQ.type

			update QNCN
			set QNCN.FTienGiamDinh=ISNULL(DetailQNCN.FTienGiamDinh,0),
			QNCN.FTienTroCap1Lan=ISNULL(DetailQNCN.FTienTroCap1Lan,0),
			QNCN.FTienTCTP=ISNULL(DetailQNCN.FTienTCTP,0),
			QNCN.FTienTCHangThang=ISNULL(DetailQNCN.FTienTCHangThang,0),
			QNCN.FTienTCPHCNvPV=ISNULL(DetailQNCN.FTienTCPHCNvPV,0),
			QNCN.FTienTCCDTNLD=ISNULL(DetailQNCN.FTienTCCDTNLD,0),
			QNCN.ISoNgayDSPHSK=ISNULL(DetailQNCN.ISoNgayDSPHSK,0),
			QNCN.FTienDSPHSK=ISNULL(DetailQNCN.FTienDSPHSK,0),
			QNCN.FTienGiamDinhTL=ISNULL(DetailQNCN.FTienGiamDinhTL,0),
			QNCN.FTienTroCap1LanTL=ISNULL(DetailQNCN.FTienTroCap1LanTL,0),
			QNCN.FTienTCTPTL=ISNULL(DetailQNCN.FTienTCTPTL,0),
			QNCN.FTienTCHangThangTL=ISNULL(DetailQNCN.FTienTCHangThangTL,0),
			QNCN.FTienTCPHCNvPVTL=ISNULL(DetailQNCN.FTienTCPHCNvPVTL,0),
			QNCN.FTienTCCDTNLDTL=ISNULL(DetailQNCN.FTienTCCDTNLDTL,0),
			QNCN.ISoNgayDSPHSKTL=ISNULL(DetailQNCN.ISoNgayDSPHSKTL,0),
			QNCN.FTienDSPHSKTL=ISNULL(DetailQNCN.FTienDSPHSKTL,0)
			FROM #tempQNCNDuToan QNCN,
			(SELECT 
			Sum(FTienGiamDinh) FTienGiamDinh
			, Sum(FTienTroCap1Lan) FTienTroCap1Lan
			, Sum(FTienTCTP) FTienTCTP
			, Sum(FTienTCHangThang) FTienTCHangThang
			, Sum(FTienTCPHCNvPV) FTienTCPHCNvPV
			, Sum(FTienTCCDTNLD) FTienTCCDTNLD
			, Sum(ISoNgayDSPHSK) ISoNgayDSPHSK
			, Sum(FTienDSPHSK) FTienDSPHSK
			, Sum(FTienGiamDinhTL) FTienGiamDinhTL
			, Sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			, Sum(FTienTCTPTL) FTienTCTPTL
			, Sum(FTienTCHangThangTL) FTienTCHangThangTL
			, Sum(FTienTCPHCNvPVTL) FTienTCPHCNvPVTL
			, Sum(FTienTCCDTNLDTL) FTienTCCDTNLDTL
			, Sum(ISoNgayDSPHSKTL) ISoNgayDSPHSKTL
			, Sum(FTienDSPHSKTL) FTienDSPHSKTL
			FROM #tempDetailQNCNDuToan) DetailQNCN


			update CNVCQP
			set CNVCQP.FTienGiamDinh=ISNULL(DetailCNVCQP.FTienGiamDinh,0),
			CNVCQP.FTienTroCap1Lan=ISNULL(DetailCNVCQP.FTienTroCap1Lan,0),
			CNVCQP.FTienTCTP=ISNULL(DetailCNVCQP.FTienTCTP,0),
			CNVCQP.FTienTCHangThang=ISNULL(DetailCNVCQP.FTienTCHangThang,0),
			CNVCQP.FTienTCPHCNvPV=ISNULL(DetailCNVCQP.FTienTCPHCNvPV,0),
			CNVCQP.FTienTCCDTNLD=ISNULL(DetailCNVCQP.FTienTCCDTNLD,0),
			CNVCQP.ISoNgayDSPHSK=ISNULL(DetailCNVCQP.ISoNgayDSPHSK,0),
			CNVCQP.FTienDSPHSK=ISNULL(DetailCNVCQP.FTienDSPHSK,0),
			CNVCQP.FTienGiamDinhTL=ISNULL(DetailCNVCQP.FTienGiamDinhTL,0),
			CNVCQP.FTienTroCap1LanTL=ISNULL(DetailCNVCQP.FTienTroCap1LanTL,0),
			CNVCQP.FTienTCTPTL=ISNULL(DetailCNVCQP.FTienTCTPTL,0),
			CNVCQP.FTienTCHangThangTL=ISNULL(DetailCNVCQP.FTienTCHangThangTL,0),
			CNVCQP.FTienTCPHCNvPVTL=ISNULL(DetailCNVCQP.FTienTCPHCNvPVTL,0),
			CNVCQP.FTienTCCDTNLDTL=ISNULL(DetailCNVCQP.FTienTCCDTNLDTL,0),
			CNVCQP.ISoNgayDSPHSKTL=ISNULL(DetailCNVCQP.ISoNgayDSPHSKTL,0),
			CNVCQP.FTienDSPHSKTL=ISNULL(DetailCNVCQP.FTienDSPHSKTL,0)
			FROM #tempCNVCQPDuToan CNVCQP,
			(SELECT 
			Sum(FTienGiamDinh) FTienGiamDinh
			, Sum(FTienTroCap1Lan) FTienTroCap1Lan
			, Sum(FTienTCTP) FTienTCTP
			, Sum(FTienTCHangThang) FTienTCHangThang
			, Sum(FTienTCPHCNvPV) FTienTCPHCNvPV
			, Sum(FTienTCCDTNLD) FTienTCCDTNLD
			, Sum(ISoNgayDSPHSK) ISoNgayDSPHSK
			, Sum(FTienDSPHSK) FTienDSPHSK
			, Sum(FTienGiamDinhTL) FTienGiamDinhTL
			, Sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			, Sum(FTienTCTPTL) FTienTCTPTL
			, Sum(FTienTCHangThangTL) FTienTCHangThangTL
			, Sum(FTienTCPHCNvPVTL) FTienTCPHCNvPVTL
			, Sum(FTienTCCDTNLDTL) FTienTCCDTNLDTL
			, Sum(ISoNgayDSPHSKTL) ISoNgayDSPHSKTL
			, Sum(FTienDSPHSKTL) FTienDSPHSKTL
			FROM #tempDetailCNVCQPDuToan) DetailCNVCQP

			update #tempHSQBSDuToan
			set FTienGiamDinh=ISNULL(DetailHSQBS.FTienGiamDinh,0),
			FTienTroCap1Lan=ISNULL(DetailHSQBS.FTienTroCap1Lan,0),
			FTienTCTP=ISNULL(DetailHSQBS.FTienTCTP,0),
			FTienTCHangThang=ISNULL(DetailHSQBS.FTienTCHangThang,0),
			FTienTCPHCNvPV=ISNULL(DetailHSQBS.FTienTCPHCNvPV,0),
			FTienTCCDTNLD=ISNULL(DetailHSQBS.FTienTCCDTNLD,0),
			ISoNgayDSPHSK=ISNULL(DetailHSQBS.ISoNgayDSPHSK,0),
			FTienDSPHSK=ISNULL(DetailHSQBS.FTienDSPHSK,0),
			FTienGiamDinhTL=ISNULL(DetailHSQBS.FTienGiamDinhTL,0),
			FTienTroCap1LanTL=ISNULL(DetailHSQBS.FTienTroCap1LanTL,0),
			FTienTCTPTL=ISNULL(DetailHSQBS.FTienTCTPTL,0),
			FTienTCHangThangTL=ISNULL(DetailHSQBS.FTienTCHangThangTL,0),
			FTienTCPHCNvPVTL=ISNULL(DetailHSQBS.FTienTCPHCNvPVTL,0),
			FTienTCCDTNLDTL=ISNULL(DetailHSQBS.FTienTCCDTNLDTL,0),
			ISoNgayDSPHSKTL=ISNULL(DetailHSQBS.ISoNgayDSPHSKTL,0),
			FTienDSPHSKTL=ISNULL(DetailHSQBS.FTienDSPHSKTL,0)
			FROM #tempHSQBSDuToan HSQBS,
			(SELECT 
			Sum(FTienGiamDinh) FTienGiamDinh
			, Sum(FTienTroCap1Lan) FTienTroCap1Lan
			, Sum(FTienTCTP) FTienTCTP
			, Sum(FTienTCHangThang) FTienTCHangThang
			, Sum(FTienTCPHCNvPV) FTienTCPHCNvPV
			, Sum(FTienTCCDTNLD) FTienTCCDTNLD
			, Sum(ISoNgayDSPHSK) ISoNgayDSPHSK
			, Sum(FTienDSPHSK) FTienDSPHSK
			, Sum(FTienGiamDinhTL) FTienGiamDinhTL
			, Sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			, Sum(FTienTCTPTL) FTienTCTPTL
			, Sum(FTienTCHangThangTL) FTienTCHangThangTL
			, Sum(FTienTCPHCNvPVTL) FTienTCPHCNvPVTL
			, Sum(FTienTCCDTNLDTL) FTienTCCDTNLDTL
			, Sum(ISoNgayDSPHSKTL) ISoNgayDSPHSKTL
			, Sum(FTienDSPHSKTL) FTienDSPHSKTL
			FROM #tempDetailHSQBSDuToan) DetailHSQBS

			update #tempLDHDDuToan
			set FTienGiamDinh=ISNULL(DetailLDHD.FTienGiamDinh,0),
			FTienTroCap1Lan=ISNULL(DetailLDHD.FTienTroCap1Lan,0),
			FTienTCTP=ISNULL(DetailLDHD.FTienTCTP,0),
			FTienTCHangThang=ISNULL(DetailLDHD.FTienTCHangThang,0),
			FTienTCPHCNvPV=ISNULL(DetailLDHD.FTienTCPHCNvPV,0),
			FTienTCCDTNLD=ISNULL(DetailLDHD.FTienTCCDTNLD,0),
			ISoNgayDSPHSK=ISNULL(DetailLDHD.ISoNgayDSPHSK,0),
			FTienDSPHSK=ISNULL(DetailLDHD.FTienDSPHSK,0),
			FTienGiamDinhTL=ISNULL(DetailLDHD.FTienGiamDinhTL,0),
			FTienTroCap1LanTL=ISNULL(DetailLDHD.FTienTroCap1LanTL,0),
			FTienTCTPTL=ISNULL(DetailLDHD.FTienTCTPTL,0),
			FTienTCHangThangTL=ISNULL(DetailLDHD.FTienTCHangThangTL,0),
			FTienTCPHCNvPVTL=ISNULL(DetailLDHD.FTienTCPHCNvPVTL,0),
			FTienTCCDTNLDTL=ISNULL(DetailLDHD.FTienTCCDTNLDTL,0),
			ISoNgayDSPHSKTL=ISNULL(DetailLDHD.ISoNgayDSPHSKTL,0),
			FTienDSPHSKTL=ISNULL(DetailLDHD.FTienDSPHSKTL,0)
			FROM #tempLDHDDuToan LDHD,
			(SELECT 
			Sum(FTienGiamDinh) FTienGiamDinh
			, Sum(FTienTroCap1Lan) FTienTroCap1Lan
			, Sum(FTienTCTP) FTienTCTP
			, Sum(FTienTCHangThang) FTienTCHangThang
			, Sum(FTienTCPHCNvPV) FTienTCPHCNvPV
			, Sum(FTienTCCDTNLD) FTienTCCDTNLD
			, Sum(ISoNgayDSPHSK) ISoNgayDSPHSK
			, Sum(FTienDSPHSK) FTienDSPHSK
			, Sum(FTienGiamDinhTL) FTienGiamDinhTL
			, Sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			, Sum(FTienTCTPTL) FTienTCTPTL
			, Sum(FTienTCHangThangTL) FTienTCHangThangTL
			, Sum(FTienTCPHCNvPVTL) FTienTCPHCNvPVTL
			, Sum(FTienTCCDTNLDTL) FTienTCCDTNLDTL
			, Sum(ISoNgayDSPHSKTL) ISoNgayDSPHSKTL
			, Sum(FTienDSPHSKTL) FTienDSPHSKTL
			FROM #tempDetailLDHDDuToan) DetailLDHD

	----- Lay ra hach toan
		-- Hạch Toan SQ
			SELECT
				 CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY tbltctn.sTenCanBo ASC))) AS STT
				,tbltctn.sTenCanBo
				,tbltctn.sTenPhanHo
				, tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, CONVERT(varchar, (tbltctn.dNgayQuyetDinh), 101) As SNgayQuyetDinh

				---- Chi giám định mức suy giảm KNLĐ (người)1
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) /@Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END) FTienGiamDinh
			
				---- Trợ cấp 1 lần (người)2
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END) FTienTroCap1Lan


				--- - Chi hỗ Trợ phòng người (người)
				-- - Chi h.trợ chuyển đổi n.nghiệp (người)
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END) FTienTCTP
			
				---- Trợ cấp hàng tháng (người)
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end )FTienTCHangThang
			
				--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPV
				--- - Trợ cấp chết do TNLD. BNN (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh  ELSE 0 END )FTienTCCDTNLD
				--- - DS, PHSK sau TNLĐ, BNN (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ) ISoNgayDSPHSK

				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END) FTienDSPHSK

				---- Chi giám định mức suy giảm KNLĐ (người)1 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) /@Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienGiamDinhTL
				
				---- Trợ cấp 1 lần (người)2 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTroCap1LanTL

				--- - Chi hỗ Trợ phòng người (người) truy linh
				-- - Chi h.trợ chuyển đổi n.nghiệp (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTCTPTL
			
				---- Trợ cấp hàng tháng (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end )FTienTCHangThangTL
			
				--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPVTL
				--- - Trợ cấp chết do TNLD. BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh  ELSE 0 END )FTienTCCDTNLDTL
				--- - DS, PHSK sau TNLĐ, BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa LIKE '9010001-010-011-0003%' THEN Sum(tbltctn.iSoNgayTruyLinh)  ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa LIKE '9010002-010-011-0003%' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ) ISoNgayDSPHSKTL

				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienDSPHSKTL
				,1 as type
				,0 IsHangCha
				,null IsParent
				into #tempDetailSQHachToan
			FROM #TBL_TroCapTaiNanHachToan tbltctn
			where tbltctn.sMaCapBac LIKE '1%'
			GROUP BY tbltctn.sTenCanBo,
				tbltctn.sTenPhanHo,
				tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.dNgayQuyetDinh
				,tbltctn.sXauNoiMa 

		-- Hạch Toan QNCN
			SELECT
				 CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY tbltctn.sTenCanBo ASC))) AS STT
				,tbltctn.sTenCanBo
				,tbltctn.sTenPhanHo
				, tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, CONVERT(varchar, (tbltctn.dNgayQuyetDinh), 101) As SNgayQuyetDinh

				---- Chi giám định mức suy giảm KNLĐ (người)1
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) /@Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END) FTienGiamDinh
			
				---- Trợ cấp 1 lần (người)2
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END) FTienTroCap1Lan


				--- - Chi hỗ Trợ phòng người (người)
				-- - Chi h.trợ chuyển đổi n.nghiệp (người)
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END) FTienTCTP
			
				---- Trợ cấp hàng tháng (người)
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end )FTienTCHangThang
			
				--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPV
				--- - Trợ cấp chết do TNLD. BNN (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh  ELSE 0 END )FTienTCCDTNLD
				--- - DS, PHSK sau TNLĐ, BNN (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ) ISoNgayDSPHSK

				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END) FTienDSPHSK

				---- Chi giám định mức suy giảm KNLĐ (người)1 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) /@Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienGiamDinhTL
				
				---- Trợ cấp 1 lần (người)2 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTroCap1LanTL

				--- - Chi hỗ Trợ phòng người (người) truy linh
				-- - Chi h.trợ chuyển đổi n.nghiệp (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTCTPTL
			
				---- Trợ cấp hàng tháng (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end )FTienTCHangThangTL
			
				--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPVTL
				--- - Trợ cấp chết do TNLD. BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh  ELSE 0 END )FTienTCCDTNLDTL
				--- - DS, PHSK sau TNLĐ, BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa LIKE '9010001-010-011-0003%' THEN Sum(tbltctn.iSoNgayTruyLinh)  ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa LIKE '9010002-010-011-0003%' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ) ISoNgayDSPHSKTL

				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienDSPHSKTL
				,1 as type
				,0 IsHangCha
				,null IsParent
				into #tempDetailQNCNHachToan
			FROM #TBL_TroCapTaiNanHachToan tbltctn
			where tbltctn.sMaCapBac LIKE '2%'
			GROUP BY tbltctn.sTenCanBo,
				tbltctn.sTenPhanHo,
				tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.dNgayQuyetDinh
				,tbltctn.sXauNoiMa 


		-- Hạch Toan CNVCQP
			SELECT
				 CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY tbltctn.sTenCanBo ASC))) AS STT
				,tbltctn.sTenCanBo
				,tbltctn.sTenPhanHo
				, tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, CONVERT(varchar, (tbltctn.dNgayQuyetDinh), 101) As SNgayQuyetDinh

				---- Chi giám định mức suy giảm KNLĐ (người)1
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END) FTienGiamDinh
			
				---- Trợ cấp 1 lần (người)2
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END) FTienTroCap1Lan


				--- - Chi hỗ Trợ phòng người (người)
				-- - Chi h.trợ chuyển đổi n.nghiệp (người)
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END) FTienTCTP
			
				---- Trợ cấp hàng tháng (người)
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end )FTienTCHangThang
			
				--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPV
				--- - Trợ cấp chết do TNLD. BNN (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh  ELSE 0 END )FTienTCCDTNLD
				--- - DS, PHSK sau TNLĐ, BNN (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ) ISoNgayDSPHSK

				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END) FTienDSPHSK

				---- Chi giám định mức suy giảm KNLĐ (người)1 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) /@Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienGiamDinhTL
				
				---- Trợ cấp 1 lần (người)2 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTroCap1LanTL

				--- - Chi hỗ Trợ phòng người (người) truy linh
				-- - Chi h.trợ chuyển đổi n.nghiệp (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTCTPTL
			
				---- Trợ cấp hàng tháng (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end )FTienTCHangThangTL
			
				--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPVTL
				--- - Trợ cấp chết do TNLD. BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh  ELSE 0 END )FTienTCCDTNLDTL
				--- - DS, PHSK sau TNLĐ, BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa LIKE '9010001-010-011-0003%' THEN Sum(tbltctn.iSoNgayTruyLinh)  ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa LIKE '9010002-010-011-0003%' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ) ISoNgayDSPHSKTL

				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienDSPHSKTL
				,1 as type
				,0 IsHangCha
				,null IsParent
				into #tempDetailCNVCQPHachToan
			FROM #TBL_TroCapTaiNanHachToan tbltctn
			where tbltctn.sMaCapBac LIKE '3.1%' OR tbltctn.sMaCapBac LIKE '3.2%' OR tbltctn.sMaCapBac LIKE '3.3%'  OR tbltctn.sMaCapBac = '413' OR tbltctn.sMaCapBac = '415'
			GROUP BY tbltctn.sTenCanBo,
				tbltctn.sTenPhanHo,
				tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.dNgayQuyetDinh
				,tbltctn.sXauNoiMa 

		-- Hạch Toan HSQBS
			SELECT
				 CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY tbltctn.sTenCanBo ASC))) AS STT
				,tbltctn.sTenCanBo
				,tbltctn.sTenPhanHo
				, tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, CONVERT(varchar, (tbltctn.dNgayQuyetDinh), 101) As SNgayQuyetDinh

				---- Chi giám định mức suy giảm KNLĐ (người)1
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END) FTienGiamDinh
			
				---- Trợ cấp 1 lần (người)2
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END) FTienTroCap1Lan


				--- - Chi hỗ Trợ phòng người (người)
				-- - Chi h.trợ chuyển đổi n.nghiệp (người)
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END) FTienTCTP
			
				---- Trợ cấp hàng tháng (người)
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end )FTienTCHangThang
			
				--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPV
				--- - Trợ cấp chết do TNLD. BNN (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh  ELSE 0 END )FTienTCCDTNLD
				--- - DS, PHSK sau TNLĐ, BNN (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ) ISoNgayDSPHSK

				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END) FTienDSPHSK

				---- Chi giám định mức suy giảm KNLĐ (người)1 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) /@Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienGiamDinhTL
				
				---- Trợ cấp 1 lần (người)2 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTroCap1LanTL

				--- - Chi hỗ Trợ phòng người (người) truy linh
				-- - Chi h.trợ chuyển đổi n.nghiệp (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTCTPTL
			
				---- Trợ cấp hàng tháng (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end )FTienTCHangThangTL
			
				--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPVTL
				--- - Trợ cấp chết do TNLD. BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh  ELSE 0 END )FTienTCCDTNLDTL
				--- - DS, PHSK sau TNLĐ, BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa LIKE '9010001-010-011-0003%' THEN Sum(tbltctn.iSoNgayTruyLinh)  ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa LIKE '9010002-010-011-0003%' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ) ISoNgayDSPHSKTL

				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienDSPHSKTL
				,1 as type
				,0 IsHangCha
				,null IsParent
				into #tempDetailHSQBSHachToan
			FROM #TBL_TroCapTaiNanHachToan tbltctn
			where tbltctn.sMaCapBac LIKE '0%'
			GROUP BY tbltctn.sTenCanBo,
				tbltctn.sTenPhanHo,
				tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.dNgayQuyetDinh
				,tbltctn.sXauNoiMa 

		-- Hạch Toan LDHD
			SELECT
				 CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY tbltctn.sTenCanBo ASC))) AS STT
				,tbltctn.sTenCanBo
				,tbltctn.sTenPhanHo
				, tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, CONVERT(varchar, (tbltctn.dNgayQuyetDinh), 101) As SNgayQuyetDinh

				---- Chi giám định mức suy giảm KNLĐ (người)1
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END) FTienGiamDinh
			
				---- Trợ cấp 1 lần (người)2
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END) FTienTroCap1Lan


				--- - Chi hỗ Trợ phòng người (người)
				-- - Chi h.trợ chuyển đổi n.nghiệp (người)
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END) FTienTCTP
			
				---- Trợ cấp hàng tháng (người)
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end )FTienTCHangThang
			
				--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPV
				--- - Trợ cấp chết do TNLD. BNN (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh  ELSE 0 END )FTienTCCDTNLD
				--- - DS, PHSK sau TNLĐ, BNN (người)
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ) ISoNgayDSPHSK

				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END) FTienDSPHSK

				---- Chi giám định mức suy giảm KNLĐ (người)1 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) /@Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienGiamDinhTL
				
				---- Trợ cấp 1 lần (người)2 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTroCap1LanTL

				--- - Chi hỗ Trợ phòng người (người) truy linh
				-- - Chi h.trợ chuyển đổi n.nghiệp (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTCTPTL
			
				---- Trợ cấp hàng tháng (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end )FTienTCHangThangTL
			
				--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPVTL
				--- - Trợ cấp chết do TNLD. BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh  ELSE 0 END )FTienTCCDTNLDTL
				--- - DS, PHSK sau TNLĐ, BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa LIKE '9010001-010-011-0003%' THEN Sum(tbltctn.iSoNgayTruyLinh)  ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa LIKE '9010002-010-011-0003%' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ) ISoNgayDSPHSKTL

				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienDSPHSKTL
				,1 as type
				,0 IsHangCha
				,null IsParent
				into #tempDetailLDHDHachToan
			FROM #TBL_TroCapTaiNanHachToan tbltctn
			where tbltctn.sMaCapBac = '423' OR tbltctn.sMaCapBac = '425' OR tbltctn.sMaCapBac = '43'
			GROUP BY tbltctn.sTenCanBo,
				tbltctn.sTenPhanHo,
				tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.dNgayQuyetDinh
				,tbltctn.sXauNoiMa 

	----- Update Hach Toan : SQ,QNCN,CNVCQP,HSQBS,LDHD
			update SQHachToan
			set FTienGiamDinh=ISNULL(DetailSQHachToan.FTienGiamDinh,0),
			FTienTroCap1Lan=ISNULL(DetailSQHachToan.FTienTroCap1Lan,0),
			FTienTCTP=ISNULL(DetailSQHachToan.FTienTCTP,0),
			FTienTCHangThang=ISNULL(DetailSQHachToan.FTienTCHangThang,0),
			FTienTCPHCNvPV=ISNULL(DetailSQHachToan.FTienTCPHCNvPV,0),
			FTienTCCDTNLD=ISNULL(DetailSQHachToan.FTienTCCDTNLD,0),
			ISoNgayDSPHSK=ISNULL(DetailSQHachToan.ISoNgayDSPHSK,0),
			FTienDSPHSK=ISNULL(DetailSQHachToan.FTienDSPHSK,0),
			FTienGiamDinhTL=ISNULL(DetailSQHachToan.FTienGiamDinhTL,0),
			FTienTroCap1LanTL=ISNULL(DetailSQHachToan.FTienTroCap1LanTL,0),
			FTienTCTPTL=ISNULL(DetailSQHachToan.FTienTCTPTL,0),
			FTienTCHangThangTL=ISNULL(DetailSQHachToan.FTienTCHangThangTL,0),
			FTienTCPHCNvPVTL=ISNULL(DetailSQHachToan.FTienTCPHCNvPVTL,0),
			FTienTCCDTNLDTL=ISNULL(DetailSQHachToan.FTienTCCDTNLDTL,0),
			ISoNgayDSPHSKTL=ISNULL(DetailSQHachToan.ISoNgayDSPHSKTL,0),
			FTienDSPHSKTL=ISNULL(DetailSQHachToan.FTienDSPHSKTL,0)
			FROM #tempSQHachToan SQHachToan,
			(SELECT 
			Sum(FTienGiamDinh) FTienGiamDinh
			, Sum(FTienTroCap1Lan) FTienTroCap1Lan
			, Sum(FTienTCTP) FTienTCTP
			, Sum(FTienTCHangThang) FTienTCHangThang
			, Sum(FTienTCPHCNvPV) FTienTCPHCNvPV
			, Sum(FTienTCCDTNLD) FTienTCCDTNLD
			, Sum(ISoNgayDSPHSK) ISoNgayDSPHSK
			, Sum(FTienDSPHSK) FTienDSPHSK
			, Sum(FTienGiamDinhTL) FTienGiamDinhTL
			, Sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			, Sum(FTienTCTPTL) FTienTCTPTL
			, Sum(FTienTCHangThangTL) FTienTCHangThangTL
			, Sum(FTienTCPHCNvPVTL) FTienTCPHCNvPVTL
			, Sum(FTienTCCDTNLDTL) FTienTCCDTNLDTL
			, Sum(ISoNgayDSPHSKTL) ISoNgayDSPHSKTL
			, Sum(FTienDSPHSKTL) FTienDSPHSKTL
			FROM #tempDetailSQHachToan) DetailSQHachToan

			update QNCNHachToan
			set QNCNHachToan.FTienGiamDinh=ISNULL(DetailQNCNHachToan.FTienGiamDinh,0),
			QNCNHachToan.FTienTroCap1Lan=ISNULL(DetailQNCNHachToan.FTienTroCap1Lan,0),
			QNCNHachToan.FTienTCTP=ISNULL(DetailQNCNHachToan.FTienTCTP,0),
			QNCNHachToan.FTienTCHangThang=ISNULL(DetailQNCNHachToan.FTienTCHangThang,0),
			QNCNHachToan.FTienTCPHCNvPV=ISNULL(DetailQNCNHachToan.FTienTCPHCNvPV,0),
			QNCNHachToan.FTienTCCDTNLD=ISNULL(DetailQNCNHachToan.FTienTCCDTNLD,0),
			QNCNHachToan.ISoNgayDSPHSK=ISNULL(DetailQNCNHachToan.ISoNgayDSPHSK,0),
			QNCNHachToan.FTienDSPHSK=ISNULL(DetailQNCNHachToan.FTienDSPHSK,0),
			QNCNHachToan.FTienGiamDinhTL=ISNULL(DetailQNCNHachToan.FTienGiamDinhTL,0),
			QNCNHachToan.FTienTroCap1LanTL=ISNULL(DetailQNCNHachToan.FTienTroCap1LanTL,0),
			QNCNHachToan.FTienTCTPTL=ISNULL(DetailQNCNHachToan.FTienTCTPTL,0),
			QNCNHachToan.FTienTCHangThangTL=ISNULL(DetailQNCNHachToan.FTienTCHangThangTL,0),
			QNCNHachToan.FTienTCPHCNvPVTL=ISNULL(DetailQNCNHachToan.FTienTCPHCNvPVTL,0),
			QNCNHachToan.FTienTCCDTNLDTL=ISNULL(DetailQNCNHachToan.FTienTCCDTNLDTL,0),
			QNCNHachToan.ISoNgayDSPHSKTL=ISNULL(DetailQNCNHachToan.ISoNgayDSPHSKTL,0),
			QNCNHachToan.FTienDSPHSKTL=ISNULL(DetailQNCNHachToan.FTienDSPHSKTL,0)
			FROM #tempQNCNHachToan QNCNHachToan, 
			(SELECT 
			Sum(FTienGiamDinh) FTienGiamDinh
			, Sum(FTienTroCap1Lan) FTienTroCap1Lan
			, Sum(FTienTCTP) FTienTCTP
			, Sum(FTienTCHangThang) FTienTCHangThang
			, Sum(FTienTCPHCNvPV) FTienTCPHCNvPV
			, Sum(FTienTCCDTNLD) FTienTCCDTNLD
			, Sum(ISoNgayDSPHSK) ISoNgayDSPHSK
			, Sum(FTienDSPHSK) FTienDSPHSK
			, Sum(FTienGiamDinhTL) FTienGiamDinhTL
			, Sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			, Sum(FTienTCTPTL) FTienTCTPTL
			, Sum(FTienTCHangThangTL) FTienTCHangThangTL
			, Sum(FTienTCPHCNvPVTL) FTienTCPHCNvPVTL
			, Sum(FTienTCCDTNLDTL) FTienTCCDTNLDTL
			, Sum(ISoNgayDSPHSKTL) ISoNgayDSPHSKTL
			, Sum(FTienDSPHSKTL) FTienDSPHSKTL
			FROM #tempDetailQNCNHachToan) DetailQNCNHachToan

			update CNVCQPHachToan
			set CNVCQPHachToan.FTienGiamDinh=ISNULL(DetailCNVCQPHachToan.FTienGiamDinh,0),
			CNVCQPHachToan.FTienTroCap1Lan=ISNULL(DetailCNVCQPHachToan.FTienTroCap1Lan,0),
			CNVCQPHachToan.FTienTCTP=ISNULL(DetailCNVCQPHachToan.FTienTCTP,0),
			CNVCQPHachToan.FTienTCHangThang=ISNULL(DetailCNVCQPHachToan.FTienTCHangThang,0),
			CNVCQPHachToan.FTienTCPHCNvPV=ISNULL(DetailCNVCQPHachToan.FTienTCPHCNvPV,0),
			CNVCQPHachToan.FTienTCCDTNLD=ISNULL(DetailCNVCQPHachToan.FTienTCCDTNLD,0),
			CNVCQPHachToan.ISoNgayDSPHSK=ISNULL(DetailCNVCQPHachToan.ISoNgayDSPHSK,0),
			CNVCQPHachToan.FTienDSPHSK=ISNULL(DetailCNVCQPHachToan.FTienDSPHSK,0),
			CNVCQPHachToan.FTienGiamDinhTL=ISNULL(DetailCNVCQPHachToan.FTienGiamDinhTL,0),
			CNVCQPHachToan.FTienTroCap1LanTL=ISNULL(DetailCNVCQPHachToan.FTienTroCap1LanTL,0),
			CNVCQPHachToan.FTienTCTPTL=ISNULL(DetailCNVCQPHachToan.FTienTCTPTL,0),
			CNVCQPHachToan.FTienTCHangThangTL=ISNULL(DetailCNVCQPHachToan.FTienTCHangThangTL,0),
			CNVCQPHachToan.FTienTCPHCNvPVTL=ISNULL(DetailCNVCQPHachToan.FTienTCPHCNvPVTL,0),
			CNVCQPHachToan.FTienTCCDTNLDTL=ISNULL(DetailCNVCQPHachToan.FTienTCCDTNLDTL,0),
			CNVCQPHachToan.ISoNgayDSPHSKTL=ISNULL(DetailCNVCQPHachToan.ISoNgayDSPHSKTL,0),
			CNVCQPHachToan.FTienDSPHSKTL=ISNULL(DetailCNVCQPHachToan.FTienDSPHSKTL,0)
			FROM #tempCNVCQPHachToan CNVCQPHachToan,
			(SELECT 
			Sum(FTienGiamDinh) FTienGiamDinh
			, Sum(FTienTroCap1Lan) FTienTroCap1Lan
			, Sum(FTienTCTP) FTienTCTP
			, Sum(FTienTCHangThang) FTienTCHangThang
			, Sum(FTienTCPHCNvPV) FTienTCPHCNvPV
			, Sum(FTienTCCDTNLD) FTienTCCDTNLD
			, Sum(ISoNgayDSPHSK) ISoNgayDSPHSK
			, Sum(FTienDSPHSK) FTienDSPHSK
			, Sum(FTienGiamDinhTL) FTienGiamDinhTL
			, Sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			, Sum(FTienTCTPTL) FTienTCTPTL
			, Sum(FTienTCHangThangTL) FTienTCHangThangTL
			, Sum(FTienTCPHCNvPVTL) FTienTCPHCNvPVTL
			, Sum(FTienTCCDTNLDTL) FTienTCCDTNLDTL
			, Sum(ISoNgayDSPHSKTL) ISoNgayDSPHSKTL
			, Sum(FTienDSPHSKTL) FTienDSPHSKTL
			FROM #tempDetailCNVCQPHachToan)DetailCNVCQPHachToan

			update HSQBSHachToan
			set HSQBSHachToan.FTienGiamDinh=ISNULL(DetailHSQBSHachToan.FTienGiamDinh,0),
			HSQBSHachToan.FTienTroCap1Lan=ISNULL(DetailHSQBSHachToan.FTienTroCap1Lan,0),
			HSQBSHachToan.FTienTCTP=ISNULL(DetailHSQBSHachToan.FTienTCTP,0),
			HSQBSHachToan.FTienTCHangThang=ISNULL(DetailHSQBSHachToan.FTienTCHangThang,0),
			HSQBSHachToan.FTienTCPHCNvPV=ISNULL(DetailHSQBSHachToan.FTienTCPHCNvPV,0),
			HSQBSHachToan.FTienTCCDTNLD=ISNULL(DetailHSQBSHachToan.FTienTCCDTNLD,0),
			HSQBSHachToan.ISoNgayDSPHSK=ISNULL(DetailHSQBSHachToan.ISoNgayDSPHSK,0),
			HSQBSHachToan.FTienDSPHSK=ISNULL(DetailHSQBSHachToan.FTienDSPHSK,0),
			HSQBSHachToan.FTienGiamDinhTL=ISNULL(DetailHSQBSHachToan.FTienGiamDinhTL,0),
			HSQBSHachToan.FTienTroCap1LanTL=ISNULL(DetailHSQBSHachToan.FTienTroCap1LanTL,0),
			HSQBSHachToan.FTienTCTPTL=ISNULL(DetailHSQBSHachToan.FTienTCTPTL,0),
			HSQBSHachToan.FTienTCHangThangTL=ISNULL(DetailHSQBSHachToan.FTienTCHangThangTL,0),
			HSQBSHachToan.FTienTCPHCNvPVTL=ISNULL(DetailHSQBSHachToan.FTienTCPHCNvPVTL,0),
			HSQBSHachToan.FTienTCCDTNLDTL=ISNULL(DetailHSQBSHachToan.FTienTCCDTNLDTL,0),
			HSQBSHachToan.ISoNgayDSPHSKTL=ISNULL(DetailHSQBSHachToan.ISoNgayDSPHSKTL,0),
			HSQBSHachToan.FTienDSPHSKTL=ISNULL(DetailHSQBSHachToan.FTienDSPHSKTL,0)
			FROM #tempHSQBSHachToan HSQBSHachToan,
			(SELECT 
			Sum(FTienGiamDinh) FTienGiamDinh
			, Sum(FTienTroCap1Lan) FTienTroCap1Lan
			, Sum(FTienTCTP) FTienTCTP
			, Sum(FTienTCHangThang) FTienTCHangThang
			, Sum(FTienTCPHCNvPV) FTienTCPHCNvPV
			, Sum(FTienTCCDTNLD) FTienTCCDTNLD
			, Sum(ISoNgayDSPHSK) ISoNgayDSPHSK
			, Sum(FTienDSPHSK) FTienDSPHSK
			, Sum(FTienGiamDinhTL) FTienGiamDinhTL
			, Sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			, Sum(FTienTCTPTL) FTienTCTPTL
			, Sum(FTienTCHangThangTL) FTienTCHangThangTL
			, Sum(FTienTCPHCNvPVTL) FTienTCPHCNvPVTL
			, Sum(FTienTCCDTNLDTL) FTienTCCDTNLDTL
			, Sum(ISoNgayDSPHSKTL) ISoNgayDSPHSKTL
			, Sum(FTienDSPHSKTL) FTienDSPHSKTL
			FROM #tempDetailHSQBSHachToan) DetailHSQBSHachToan

			update LDHDHachToan
			set LDHDHachToan.FTienGiamDinh=ISNULL(DetailLDHDHachToan.FTienGiamDinh,0),
			LDHDHachToan.FTienTroCap1Lan=ISNULL(DetailLDHDHachToan.FTienTroCap1Lan,0),
			LDHDHachToan.FTienTCTP=ISNULL(DetailLDHDHachToan.FTienTCTP,0),
			LDHDHachToan.FTienTCHangThang=ISNULL(DetailLDHDHachToan.FTienTCHangThang,0),
			LDHDHachToan.FTienTCPHCNvPV=ISNULL(DetailLDHDHachToan.FTienTCPHCNvPV,0),
			LDHDHachToan.FTienTCCDTNLD=ISNULL(DetailLDHDHachToan.FTienTCCDTNLD,0),
			LDHDHachToan.ISoNgayDSPHSK=ISNULL(DetailLDHDHachToan.ISoNgayDSPHSK,0),
			LDHDHachToan.FTienDSPHSK=ISNULL(DetailLDHDHachToan.FTienDSPHSK,0),
			LDHDHachToan.FTienGiamDinhTL=ISNULL(DetailLDHDHachToan.FTienGiamDinhTL,0),
			LDHDHachToan.FTienTroCap1LanTL=ISNULL(DetailLDHDHachToan.FTienTroCap1LanTL,0),
			LDHDHachToan.FTienTCTPTL=ISNULL(DetailLDHDHachToan.FTienTCTPTL,0),
			LDHDHachToan.FTienTCHangThangTL=ISNULL(DetailLDHDHachToan.FTienTCHangThangTL,0),
			LDHDHachToan.FTienTCPHCNvPVTL=ISNULL(DetailLDHDHachToan.FTienTCPHCNvPVTL,0),
			LDHDHachToan.FTienTCCDTNLDTL=ISNULL(DetailLDHDHachToan.FTienTCCDTNLDTL,0),
			LDHDHachToan.ISoNgayDSPHSKTL=ISNULL(DetailLDHDHachToan.ISoNgayDSPHSKTL,0),
			LDHDHachToan.FTienDSPHSKTL=ISNULL(DetailLDHDHachToan.FTienDSPHSKTL,0)
			FROM #tempLDHDHachToan LDHDHachToan,
			(SELECT 
			Sum(FTienGiamDinh) FTienGiamDinh
			, Sum(FTienTroCap1Lan) FTienTroCap1Lan
			, Sum(FTienTCTP) FTienTCTP
			, Sum(FTienTCHangThang) FTienTCHangThang
			, Sum(FTienTCPHCNvPV) FTienTCPHCNvPV
			, Sum(FTienTCCDTNLD) FTienTCCDTNLD
			, Sum(ISoNgayDSPHSK) ISoNgayDSPHSK
			, Sum(FTienDSPHSK) FTienDSPHSK
			, Sum(FTienGiamDinhTL) FTienGiamDinhTL
			, Sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			, Sum(FTienTCTPTL) FTienTCTPTL
			, Sum(FTienTCHangThangTL) FTienTCHangThangTL
			, Sum(FTienTCPHCNvPVTL) FTienTCPHCNvPVTL
			, Sum(FTienTCCDTNLDTL) FTienTCCDTNLDTL
			, Sum(ISoNgayDSPHSKTL) ISoNgayDSPHSKTL
			, Sum(FTienDSPHSKTL) FTienDSPHSKTL
			FROM #tempDetailLDHDHachToan)DetailLDHDHachToan

---------  Tra ve kqua DuToan
			SELECT * INTO #tempkqDuToan FROM
			(
					SELECT * FROM #tempDuToan
					UNION ALL
					SELECT * FROM  #tempSQDuToan
					UNION ALL
					SELECT * FROM  #tempDetailSQDuToan
					UNION ALL
					SELECT * FROM  #tempQNCNDuToan
					UNION ALL
					SELECT * FROM  #tempDetailQNCNDuToan
					UNION ALL
					SELECT * FROM  #tempCNVCQPDuToan
					UNION ALL
					SELECT * FROM  #tempDetailCNVCQPDuToan
					UNION ALL
					SELECT * FROM  #tempHSQBSDuToan
					UNION ALL
					SELECT * FROM  #tempDetailHSQBSDuToan
					UNION ALL
					SELECT * FROM  #tempLDHDDuToan
					UNION ALL
					SELECT * FROM  #tempDetailLDHDDuToan
			) TEMPDuToan
------ Update total khoi du toan
			SELECT 
			Sum(FTienGiamDinh) FTienGiamDinh
			, Sum(FTienTroCap1Lan) FTienTroCap1Lan
			, Sum(FTienTCTP) FTienTCTP
			, Sum(FTienTCHangThang) FTienTCHangThang
			, Sum(FTienTCPHCNvPV) FTienTCPHCNvPV
			, Sum(FTienTCCDTNLD) FTienTCCDTNLD
			, Sum(ISoNgayDSPHSK) ISoNgayDSPHSK
			, Sum(FTienDSPHSK) FTienDSPHSK
			, Sum(FTienGiamDinhTL) FTienGiamDinhTL
			, Sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			, Sum(FTienTCTPTL) FTienTCTPTL
			, Sum(FTienTCHangThangTL) FTienTCHangThangTL
			, Sum(FTienTCPHCNvPVTL) FTienTCPHCNvPVTL
			, Sum(FTienTCCDTNLDTL) FTienTCCDTNLDTL
			, Sum(ISoNgayDSPHSKTL) ISoNgayDSPHSKTL
			, Sum(FTienDSPHSKTL) FTienDSPHSKTL
			, 3 type
			INTO #tempTotalDuToan
			FROM #tempkqDuToan
			WHERE type=2

			update T1
			set T1.FTienGiamDinh=T2.FTienGiamDinh,
			T1.FTienTroCap1Lan=T2.FTienTroCap1Lan,
			T1.FTienTCTP=T2.FTienTCTP,
			T1.FTienTCHangThang=T2.FTienTCHangThang,
			T1.FTienTCPHCNvPV=T2.FTienTCPHCNvPV,
			T1.FTienTCCDTNLD=T2.FTienTCCDTNLD,
			T1.ISoNgayDSPHSK=T2.ISoNgayDSPHSK,
			T1.FTienDSPHSK=T2.FTienDSPHSK,
			T1.FTienGiamDinhTL=T2.FTienGiamDinhTL,
			T1.FTienTroCap1LanTL=T2.FTienTroCap1LanTL,
			T1.FTienTCTPTL=T2.FTienTCTPTL,
			T1.FTienTCHangThangTL=T2.FTienTCHangThangTL,
			T1.FTienTCPHCNvPVTL=T2.FTienTCPHCNvPVTL,
			T1.FTienTCCDTNLDTL=T2.FTienTCCDTNLDTL,
			T1.ISoNgayDSPHSKTL=T2.ISoNgayDSPHSKTL,
			T1.FTienDSPHSKTL=T2.FTienDSPHSKTL
			FROM #tempkqDuToan T1, #tempTotalDuToan T2
			WHERE T1.type=T2.type
			and T1.STT=N'I. Khối dự toán'

---------  Tra ve kqua HachToan
			SELECT * INTO #tempkqHachToan FROM
			(
					SELECT * FROM #tempHachToan
					UNION ALL
					SELECT * FROM  #tempSQHachToan
					UNION ALL
					SELECT * FROM  #tempDetailSQHachToan
					UNION ALL
					SELECT * FROM  #tempQNCNHachToan
					UNION ALL
					SELECT * FROM  #tempDetailQNCNHachToan
					UNION ALL
					SELECT * FROM  #tempCNVCQPHachToan
					UNION ALL
					SELECT * FROM  #tempDetailCNVCQPHachToan
					UNION ALL
					SELECT * FROM  #tempHSQBSHachToan
					UNION ALL
					SELECT * FROM  #tempDetailHSQBSHachToan
					UNION ALL
					SELECT * FROM  #tempLDHDHachToan
					UNION ALL
					SELECT * FROM  #tempDetailLDHDHachToan
			) TEMPHachToan

------ Update total khoi hach toan
			SELECT 
			Sum(FTienGiamDinh) FTienGiamDinh
			, Sum(FTienTroCap1Lan) FTienTroCap1Lan
			, Sum(FTienTCTP) FTienTCTP
			, Sum(FTienTCHangThang) FTienTCHangThang
			, Sum(FTienTCPHCNvPV) FTienTCPHCNvPV
			, Sum(FTienTCCDTNLD) FTienTCCDTNLD
			, Sum(ISoNgayDSPHSK) ISoNgayDSPHSK
			, Sum(FTienDSPHSK) FTienDSPHSK
			, Sum(FTienGiamDinhTL) FTienGiamDinhTL
			, Sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			, Sum(FTienTCTPTL) FTienTCTPTL
			, Sum(FTienTCHangThangTL) FTienTCHangThangTL
			, Sum(FTienTCPHCNvPVTL) FTienTCPHCNvPVTL
			, Sum(FTienTCCDTNLDTL) FTienTCCDTNLDTL
			, Sum(ISoNgayDSPHSKTL) ISoNgayDSPHSKTL
			, Sum(FTienDSPHSKTL) FTienDSPHSKTL
			, 3 type
			INTO #tempTotalHachToan
			FROM #tempkqHachToan
			WHERE type=2

			UPDATE  T1
			set T1.FTienGiamDinh=T2.FTienGiamDinh,
			T1.FTienTroCap1Lan=T2.FTienTroCap1Lan,
			T1.FTienTCTP=T2.FTienTCTP,
			T1.FTienTCHangThang=T2.FTienTCHangThang,
			T1.FTienTCPHCNvPV=T2.FTienTCPHCNvPV,
			T1.FTienTCCDTNLD=T2.FTienTCCDTNLD,
			T1.ISoNgayDSPHSK=T2.ISoNgayDSPHSK,
			T1.FTienDSPHSK=T2.FTienDSPHSK,
			T1.FTienGiamDinhTL=T2.FTienGiamDinhTL,
			T1.FTienTroCap1LanTL=T2.FTienTroCap1LanTL,
			T1.FTienTCTPTL=T2.FTienTCTPTL,
			T1.FTienTCHangThangTL=T2.FTienTCHangThangTL,
			T1.FTienTCPHCNvPVTL=T2.FTienTCPHCNvPVTL,
			T1.FTienTCCDTNLDTL=T2.FTienTCCDTNLDTL,
			T1.ISoNgayDSPHSKTL=T2.ISoNgayDSPHSKTL,
			T1.FTienDSPHSKTL=T2.FTienDSPHSKTL
			FROM #tempkqHachToan T1, #tempTotalHachToan T2
			WHERE T1.type=T2.type
			and T1.STT=N'II. Khối hạch toán'

--------- Tra Ve KQua ALL

		SELECT * INTO #tempKQAll FROM
			(
					SELECT * FROM #tempkqDuToan
					UNION ALL
					SELECT * FROM  #tempkqHachToan
					
			) TEMPKQAll

		select * from #tempKQAll

		DROP TABLE #TBL_TroCapTaiNanDuToan
		DROP TABLE #TBL_TroCapTaiNanHachToan

		DROP TABLE #tempDuToan
		DROP TABLE #tempSQDuToan
		DROP TABLE #tempQNCNDuToan
		DROP TABLE #tempCNVCQPDuToan
		DROP TABLE #tempHSQBSDuToan
		DROP TABLE #tempLDHDDuToan
		DROP TABLE #tempDetailSQDuToan
		DROP TABLE #tempDetailQNCNDuToan
		DROP TABLE #tempDetailCNVCQPDuToan
		DROP TABLE #tempDetailHSQBSDuToan
		DROP TABLE #tempDetailLDHDDuToan
		DROP TABLE #tempTotalDuToan

		DROP TABLE #tempHachToan
		DROP TABLE #tempSQHachToan
		DROP TABLE #tempQNCNHachToan
		DROP TABLE #tempCNVCQPHachToan
		DROP TABLE #tempHSQBSHachToan
		DROP TABLE #tempLDHDHachToan
		DROP TABLE #tempDetailSQHachToan
		DROP TABLE #tempDetailQNCNHachToan
		DROP TABLE #tempDetailCNVCQPHachToan
		DROP TABLE #tempDetailHSQBSHachToan
		DROP TABLE #tempDetailLDHDHachToan
		DROP TABLE #tempTotalHachToan

		DROP TABLE #tempkqDuToan
		DROP TABLE #tempkqHachToan
		DROP TABLE #tempKQAll

end
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT]    Script Date: 7/12/2024 8:13:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT]
	@INamLamViec int,
	@IdMaDonVi nvarchar(MAX),
	@IQuy int,
	@Donvitinh int
AS
BEGIN
SELECT gt.* INTO #tblTroCap FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gt
	WHERE (--- Du toan
			gt.sXauNoiMa like '9010001-010-011-0004%'
			or gt.sXauNoiMa like '9010001-010-011-0005%'
			or gt.sXauNoiMa like '9010001-010-011-0006%'
			or gt.sXauNoiMa like '9010001-010-011-0007%'
			or gt.sXauNoiMa like '9010001-010-011-0008%'
			--- Hoach toan
			or gt.sXauNoiMa like '9010002-010-011-0004%'
			or gt.sXauNoiMa like '9010002-010-011-0005%'
			or gt.sXauNoiMa like '9010002-010-011-0006%'
			or gt.sXauNoiMa like '9010002-010-011-0007%'
			or gt.sXauNoiMa like '9010002-010-011-0008%')
		and gt.iNamLamViec = @INamLamViec and gt.iQuy = @IQuy and gt.iiD_MaDonVi in (SELECT * FROM f_split(@IdMaDonVi))

	--- 9010001-010-011-0004 Tro cap Huu tri
		SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			into #tempDetailHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC
	SELECT * INTO #tempHuuTri FROM
	(
	SELECT 1 bHangCha
			, N'(I)' STT
			, N'TC Hưu Trí'as STenCanBo 
			, '' as STenPhanHo
			, '' as SMaCapBac 
			, null as SSoQuyetDinh
			, null as  DNgayQuyetDinh
			, null as FTienTroCap1Lan
			, null as FTienTroCapKV
			, null as FTienTroCapMT
			, null as FTienTroCap1LanTL
			, null as FTienTroCapKVTL
			, null as FTienTroCapMTTL
			, 0 bHasData
	UNION ALL
	SELECT * FROM #tempDetailHuuTri	
	) tblHuutri

	 if (SELECT COUNT(1) FROM #tempHuuTri) > 2
		UPDATE #tempHuuTri set bHasData = 1

		UPDATE #tempHuuTri
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempHuuTri ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempHuuTri WHERE  bHangCha=0 ) detail
		where #tempHuuTri.bHangCha=1
	--- 9010001-010-011-0005 Tro cap phuc vien

	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			into #tempDetailPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac	
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC
	SELECT * INTO #tempPhucVien FROM
	(
	SELECT 1 bHangCha
			, N'(II)' STT
			, N'TC Phục viên'as STenCanBo 
			, '' as STenPhanHo
			, '' as SMaCapBac 
			, null as SSoQuyetDinh
			, null as  DNgayQuyetDinh
			, null as FTienTroCap1Lan
			, null as FTienTroCapKV
			, null as FTienTroCapMT
			, null as FTienTroCap1LanTL
			, null as FTienTroCapKVTL
			, null as FTienTroCapMTTL
			, 0 bHasData
	UNION ALL
	SELECT * FROM #tempDetailPhucVien
	) tblPhucVien

	 if (SELECT COUNT(1) FROM #tempPhucVien) > 2
		UPDATE #tempPhucVien SET bHasData = 1

		UPDATE #tempPhucVien
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempPhucVien ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempPhucVien WHERE  bHangCha=0 ) detail
		where #tempPhucVien.bHangCha=1

	--- 9010001-010-011-0006 Tro cap xuat ngu

	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			--, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sSoQuyetDinh) AS VARCHAR(6)) STT
			--, Convert(varchar(10),(COUNT(tbltc.sSoQuyetDinh))) + N' Đồng chi' as sTenCanBo
			--, tbltc.sTenPhanHo
			--, '' as SMaCapBac		
			--, tbltc.sSoQuyetDinh
			--, tbltc.dNgayQuyetDinh
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCap1Lan
			--, 0 AS  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS  FTienTroCapMT
			--, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) /1 ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / 1 ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			INTO #tempDetailXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac	
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC
	SELECT * INTO #tempXuatNgu FROM
	(
	SELECT 1 bHangCha
			, N'(III)' STT
			, N'TC Xuất ngũ'as STenCanBo 
			, '' as STenPhanHo
			, '' as SMaCapBac		
			, null as SSoQuyetDinh
			, null as  DNgayQuyetDinh
			, null as FTienTroCap1Lan
			, null as FTienTroCapKV
			, null as FTienTroCapMT
			, null as FTienTroCap1LanTL
			, null as FTienTroCapKVTL
			, null as FTienTroCapMTTL
			, 0 bHasData
	UNION ALL
	SELECT * FROM #tempDetailXuatNgu
	) tblXuatNgu

	 if (SELECT COUNT(1) from #tempXuatNgu) > 2
		UPDATE #tempXuatNgu set bHasData = 1
 
 		UPDATE #tempXuatNgu
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempXuatNgu ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempXuatNgu WHERE  bHangCha=0 ) detail
		where #tempXuatNgu.bHangCha=1
	--- 9010001-010-011-0007 tro cap thoi viec

		SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			INTO #tempDetailThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC
	SELECT * INTO #tempThoiViec FROM
	(
	SELECT 1 bHangCha
			, N'(IV)' STT
			, N'TC Thôi việc'as STenCanBo 
			, '' as STenPhanHo
			, '' as SMaCapBac 
			, null as SSoQuyetDinh
			, null as  DNgayQuyetDinh
			, null as FTienTroCap1Lan
			, null as FTienTroCapKV
			, null as FTienTroCapMT
			, null as FTienTroCap1LanTL
			, null as FTienTroCapKVTL
			, null as FTienTroCapMTTL
			, 0 bHasData
	UNION ALL
	SELECT * FROM #tempDetailThoiViec
	) tblThoiViec

	 if (SELECT COUNT(1) FROM #tempThoiViec) > 2
		UPDATE #tempThoiViec SET bHasData = 1

		UPDATE #tempThoiViec
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempThoiViec ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempThoiViec WHERE  bHangCha=0 ) detail
		where #tempThoiViec.bHangCha=1
	--- 9010001-010-011-0008 tro cap tu tuat

	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			INTO #tempDetailTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC
	SELECT * INTO #tempTuTuat FROM
	(
	SELECT 1 bHangCha
			, N'(V)' STT
			, N'TC Tử tuất'as STenCanBo 
			, '' as STenPhanHo
			, '' as SMaCapBac 
			, null as SSoQuyetDinh
			, null as  DNgayQuyetDinh
			, null as FTienTroCap1Lan
			, null as FTienTroCapKV
			, null as FTienTroCapMT
			, null as FTienTroCap1LanTL
			, null as FTienTroCapKVTL
			, null as FTienTroCapMTTL
			, 0 bHasData
	UNION ALL
	SELECT * FROM #tempDetailTuTuat
	) tblTuTuat

	 if (SELECT COUNT(1) FROM #tempTuTuat) > 2
		UPDATE #tempTuTuat SET bHasData = 1

		UPDATE #tempTuTuat
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempTuTuat ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempTuTuat WHERE  bHangCha=0 ) detail
		where #tempTuTuat.bHangCha=1
		-- ket qua
	SELECT * INTO #tempRESULT  
	FROM
	(
		SELECT * FROM #tempHuuTri
		UNION ALL 
		SELECT * FROM #tempPhucVien
		UNION ALL 
		SELECT * FROM #tempXuatNgu
		UNION ALL
		SELECT * FROM #tempThoiViec
		UNION ALL
		SELECT * FROM #tempTuTuat
	) TBLRESULT

	SELECT * FROM #tempRESULT

	 DROP TABLE #tempHuuTri
	 DROP TABLE #tempPhucVien
	 DROP TABLE #tempXuatNgu
	 DROP TABLE #tempThoiViec
	 DROP TABLE #tempTuTuat
	 DROP TABLE #tempRESULT
	 	DROP TABLE #TempDetailHuuTri
	 DROP TABLE #tempDetailPhucVien
	 DROP TABLE #TempDetailTuTuat
	 DROP TABLE #tempDetailThoiViec
	 DROP TABLE #tempDetailXuatNgu

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
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Khoi]    Script Date: 7/12/2024 8:13:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Khoi]
	@INamLamViec int,
	@IdMaDonVi nvarchar(MAX),
	@IQuy int,
	@Donvitinh int
AS
BEGIN

	---IRemainRow 1: SQ, 2 : QNCN,3:CC, CN, VCQP,4:HSQBS, 5:LĐHĐ
	--- Type  :3 :Huu Tri,phuc vien,xuat ngu,thoi viec, tu tuat
	--- 2: Khoi du Toan, Khoi hach toan
	--- 1 SQ, QNCN,CC, CN, VCQP,HSQBS, LĐHĐ
	---- Ikhoi : 2  Khoi Du toan, 1 Khoi Hach Toan
	SELECT gt.* INTO #tblTroCapKhoiDuToan FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gt
	left join DonVi dv on gt.iiD_MaDonVi=dv.iID_MaDonVi
	WHERE (--- Du toan
			gt.sXauNoiMa like '9010001-010-011-0004%'
			or gt.sXauNoiMa like '9010001-010-011-0005%'
			or gt.sXauNoiMa like '9010001-010-011-0006%'
			or gt.sXauNoiMa like '9010001-010-011-0007%'
			or gt.sXauNoiMa like '9010001-010-011-0008%'
			--- Hoach toan
			or gt.sXauNoiMa like '9010002-010-011-0004%'
			or gt.sXauNoiMa like '9010002-010-011-0005%'
			or gt.sXauNoiMa like '9010002-010-011-0006%'
			or gt.sXauNoiMa like '9010002-010-011-0007%'
			or gt.sXauNoiMa like '9010002-010-011-0008%')
		and gt.iNamLamViec = @INamLamViec and gt.iQuy = @IQuy and gt.iiD_MaDonVi in (SELECT * FROM f_split(@IdMaDonVi))
		and dv.iNamLamViec=@INamLamViec
		and dv.iTrangThai=1
		and dv.iKhoi=2

	SELECT gt.* INTO #tblTroCapKhoiHachToan FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gt
	left join DonVi dv on gt.iiD_MaDonVi=dv.iID_MaDonVi
	WHERE (--- Du toan
			gt.sXauNoiMa like '9010001-010-011-0004%'
			or gt.sXauNoiMa like '9010001-010-011-0005%'
			or gt.sXauNoiMa like '9010001-010-011-0006%'
			or gt.sXauNoiMa like '9010001-010-011-0007%'
			or gt.sXauNoiMa like '9010001-010-011-0008%'
			--- Hoach toan
			or gt.sXauNoiMa like '9010002-010-011-0004%'
			or gt.sXauNoiMa like '9010002-010-011-0005%'
			or gt.sXauNoiMa like '9010002-010-011-0006%'
			or gt.sXauNoiMa like '9010002-010-011-0007%'
			or gt.sXauNoiMa like '9010002-010-011-0008%')
		and gt.iNamLamViec = @INamLamViec and gt.iQuy = @IQuy and gt.iiD_MaDonVi in (SELECT * FROM f_split(@IdMaDonVi))
		and dv.iNamLamViec=@INamLamViec
		and dv.iTrangThai=1
		and dv.iKhoi=1

		--- Huu Tri Khoi Du Toan
			SELECT 
				1 bHangCha
				, N'(I)' STT
				, N'TC Hưu Trí'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempHuuTriDuToan

		--- Phuc Vien Khoi Du Toan
			SELECT 
				1 bHangCha
				, N'(II)' STT
				, N'TC Phục viên 'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempPhucVienDuToan

		--- TC Xuất ngũ Khoi Du Toan
			SELECT 
				1 bHangCha
				, N'(III)' STT
				, N'TC Xuất ngũ'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempXuatNguDuToan

		--- TC Thôi việc  Khoi Du Toan
			SELECT 
				1 bHangCha
				, N'(IV)' STT
				, N'TC Thôi việc'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempThoiViecDuToan

		--- TC Tử tuất  Khoi Du Toan
			SELECT 
				1 bHangCha
				, N'(V)' STT
				, N'TC Tử tuất 'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempTuTuatDuToan

		--- Huu Tri Khoi Hach Toan
			SELECT 
				1 bHangCha
				, N'(I)' STT
				, N'TC Hưu Trí'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempHuuTriHachToan

		--- Phuc Vien Khoi Hach Toan
			SELECT 
				1 bHangCha
				, N'(II)' STT
				, N'TC Phục viên 'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempPhucVienHachToan

		--- TC Xuất ngũ Khoi Hach Toan
			SELECT 
				1 bHangCha
				, N'(III)' STT
				, N'TC Xuất ngũ'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempXuatNguHachToan

		--- TC Thôi việc  Khoi Hach Toan

			SELECT 
				1 bHangCha
				, N'(IV)' STT
				, N'TC Thôi việc'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempThoiViecHachToan

		--- TC Tử tuất  Khoi Hach Toan
			SELECT 
				1 bHangCha
				, N'(V)' STT
				, N'TC Tử tuất 'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempTuTuatHachToan

		--- Khoi Du Toan
			SELECT 
					1 bHangCha
					, N'Khối dự toán' STT
					, ''as STenCanBo 
					, '' as STenPhanHo
					, '' as SMaCapBac 
					, '' as SSoQuyetDinh
					, null as  DNgayQuyetDinh
					, null as FTienTroCap1Lan
					, null as FTienTroCapKV
					, null as FTienTroCapMT
					, null as FTienTroCap1LanTL
					, null as FTienTroCapKVTL
					, null as FTienTroCapMTTL
					, 0 bHasData
					, 2 Type
					, 1 IsParent
					, 0 IRemainRow
					, 2 IKhoi
					into #tempKhoiDuToan

		--- Khoi Hach Toan
			SELECT 
					1 bHangCha
					, N'Khối hạch toán' STT
					, ''as STenCanBo 
					, '' as STenPhanHo
					, '' as SMaCapBac 
					, '' as SSoQuyetDinh
					, null as  DNgayQuyetDinh
					, null as FTienTroCap1Lan
					, null as FTienTroCapKV
					, null as FTienTroCapMT
					, null as FTienTroCap1LanTL
					, null as FTienTroCapKVTL
					, null as FTienTroCapMTTL
					, 0 bHasData
					, 2 Type
					, 1 IsParent
					, 0 IRemainRow
					, 1 IKhoi
					into #tempKhoiHachToan

		--- Si Quan
			SELECT 
				1 bHangCha
				, N'Si quan' STT
				, ''as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 1 Type
				, 1 IsParent
				, 1 IRemainRow
				, 0 IKhoi
				into #tempSiQuan

		--- QNCN
			SELECT 
				1 bHangCha
				, N'QNCN' STT
				, ''as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 1 Type
				, 1 IsParent
				, 2 IRemainRow
				, 0 IKhoi
				into #tempQNCN

		--- CC, CN, VCQP
			SELECT 
				1 bHangCha
				, N'CC, CN, VCQP' STT
				, ''as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 1 Type
				, 1 IsParent
				, 3 IRemainRow
				, 0 IKhoi
				into #tempCNVCQP

        --- HSQBS
			SELECT 
				1 bHangCha
				, N'HSQBS' STT
				, ''as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 1 Type
				, 1 IsParent
				, 4 IRemainRow
				, 0 IKhoi
				into #tempHSQBS

		--- LĐHĐ
			SELECT 
				1 bHangCha
				, N'LĐHĐ' STT
				, ''as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 1 Type
				, 1 IsParent
				, 5 IRemainRow
				, 0 IKhoi
				into #tempLDHD
--- Du Toan
--- Huu Tri Du Toan
		--- Detal Si Quan DuToan Huu Tri
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi
			into #tempDetailSiQuanDuToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

		--- Detal QNCN DuToan Huu Tri
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 2 IKhoi
			into #tempDetailQNCNDuToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal CNVCQP DuToan Huu Tri
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 2 IKhoi
			into #tempDetailCNVCQPDuToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal HSQBS DuToan Huu Tri
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi
			into #tempDetailHSQBSDuToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac LIKE '0%')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal LDHD DuToan Huu Tri
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 2 IKhoi
			into #tempDetailLDHDDuToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

--- Phuc Vien Du Toan
			--- Detal Si Quan  Phục viên 
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi
			into #tempDetailSiQuanDuToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal QNCN DuToan Phục viên 
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 2 IKhoi
			into #tempDetailQNCNDuToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal CNVCQP DuToan Phuc Vien
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 2 IKhoi
			into #tempDetailCNVCQPDuToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal HSQBS DuToan Phục viên 
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi
			into #tempDetailHSQBSDuToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal LDHD DuToan Phuc Vien
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 2 IKhoi
			into #tempDetailLDHDDuToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

--- Xuat Ngu Du Toan
			--- Detal Si Quan  Xuat Ngu
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sSoQuyetDinh) AS VARCHAR(6)) STT
			, Convert(varchar(10),(COUNT(tbltc.sSoQuyetDinh))) + N' Đồng chi' as sTenCanBo
			, tbltc.sTenPhanHo
			, '' as SMaCapBac	
			--, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			--, tbltc.sTenCanBo
			--, tbltc.sTenPhanHo
			--, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) /1 ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / 1 ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi
			into #tempDetailSiQuanDuToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY tbltc.sTenPhanHo
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal QNCN DuToan Xuat Ngu 
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sSoQuyetDinh) AS VARCHAR(6)) STT
			, Convert(varchar(10),(COUNT(tbltc.sSoQuyetDinh))) + N' Đồng chi' as sTenCanBo
			, tbltc.sTenPhanHo
			, '' as SMaCapBac	
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) /1 ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / 1 ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 2 IKhoi
			into #tempDetailQNCNDuToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sTenPhanHo
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal CNVCQP DuToan Phuc Vien
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sSoQuyetDinh) AS VARCHAR(6)) STT
			, Convert(varchar(10),(COUNT(tbltc.sSoQuyetDinh))) + N' Đồng chi' as sTenCanBo
			, tbltc.sTenPhanHo
			, '' as SMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) /1 ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / 1 ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 2 IKhoi
			into #tempDetailCNVCQPDuToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%' )
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sTenPhanHo
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal HSQBS DuToan Xuat Ngu
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sSoQuyetDinh) AS VARCHAR(6)) STT
			, Convert(varchar(10),(COUNT(tbltc.sSoQuyetDinh))) + N' Đồng chi' as sTenCanBo
			, tbltc.sTenPhanHo
			, '' as SMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) /1 ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / 1 ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi
			into #tempDetailHSQBSDuToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY   tbltc.sTenPhanHo
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal LDHD DuToan Xuat Ngu
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sSoQuyetDinh) AS VARCHAR(6)) STT
			, Convert(varchar(10),(COUNT(tbltc.sSoQuyetDinh))) + N' Đồng chi' as sTenCanBo
			, tbltc.sTenPhanHo
			, '' as SMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) /1 ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / 1 ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 2 IKhoi
			into #tempDetailLDHDDuToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY   tbltc.sTenPhanHo
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

--- Thoi Viec Du Toan
			--- Detal Si Quan DuToan Thoi Viec
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi
			into #tempDetailSiQuanDuToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal QNCN DuToan Thoi Viec
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 2 IKhoi
			into #tempDetailQNCNDuToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal CNVCQP DuToan Thoi Viec
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 2 IKhoi
			into #tempDetailCNVCQPDuToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%' )
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal HSQBS DuToan Thoi Viec
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi
			into #tempDetailHSQBSDuToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal LDHD DuToan Thoi Viec
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 2 IKhoi
			into #tempDetailLDHDDuToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

--- Tu Tuat  Du Toan
			--- Detal Si Quan DuToan Tu Tuat
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi
			into #tempDetailSiQuanDuToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal QNCN DuToan Tu Tuat
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 2 IKhoi
			into #tempDetailQNCNDuToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal CNVCQP DuToan Tu Tuat
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 2 IKhoi
			into #tempDetailCNVCQPDuToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%' )
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal HSQBS DuToan Tu Tuat
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi
			into #tempDetailHSQBSDuToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal LDHD DuToan Thoi Viec
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 2 IKhoi
			into #tempDetailLDHDDuToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
--- Hach Toan
--- Huu Tri HachToan
		--- Detal Si Quan HachToan Huu Tri
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi
			into #tempDetailSiQuanHachToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

		--- Detal QNCN HachToan Huu Tri
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi
			into #tempDetailQNCNHachToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal CNVCQP HachToan Huu Tri
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi
			into #tempDetailCNVCQPHachToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal HSQBS HachToan Huu Tri
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi
			into #tempDetailHSQBSHachToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac LIKE '0%')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal LDHD HachToan Huu Tri
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi
			into #tempDetailLDHDHachToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

--- Phuc Vien Hach Toan
			--- Detal Si Quan HachToan Phục viên 
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi
			into #tempDetailSiQuanHachToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal QNCN HachToan Phục viên 
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi
			into #tempDetailQNCNHachToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal CNVCQP HachToan Phuc Vien
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi
			into #tempDetailCNVCQPHachToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal HSQBS HachToan Phục viên 
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi
			into #tempDetailHSQBSHachToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal LDHD HachToan Phuc Vien
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi
			into #tempDetailLDHDHachToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

--- Xuat Ngu Hach Toan
			--- Detal Si Quan HachToan  Xuat Ngu
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) /1 ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / 1 ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi
			into #tempDetailSiQuanHachToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal QNCN HachToan Xuat Ngu 
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) /1 ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / 1 ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi
			into #tempDetailQNCNHachToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal CNVCQP HachToan Phuc Vien
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) /1 ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / 1 ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi
			into #tempDetailCNVCQPHachToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%' )
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal HSQBS HachToan Xuat Ngu
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) /1 ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / 1 ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi
			into #tempDetailHSQBSHachToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal LDHD HachToan Xuat Ngu
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) /1 ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / 1 ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi
			into #tempDetailLDHDHachToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

--- Thoi Viec Du Toan
			--- Detal Si Quan HachToan Thoi Viec
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi
			into #tempDetailSiQuanHachToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal QNCN HachToan Thoi Viec
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi
			into #tempDetailQNCNHachToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal CNVCQP HachToan Thoi Viec
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi
			into #tempDetailCNVCQPHachToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%' )
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal HSQBS HachToan Thoi Viec
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi
			into #tempDetailHSQBSHachToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal LDHD HachToan Thoi Viec
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi
			into #tempDetailLDHDHachToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

--- Tu Tuat  Du Toan
			--- Detal Si Quan HachToan Tu Tuat
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi
			into #tempDetailSiQuanHachToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal QNCN HachToan Tu Tuat
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi
			into #tempDetailQNCNHachToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal CNVCQP HachToan Tu Tuat
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi
			into #tempDetailCNVCQPHachToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%' )
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal HSQBS HachToan Tu Tuat
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi
			into #tempDetailHSQBSHachToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal LDHD HachToan Thoi Viec
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi
			into #tempDetailLDHDHachToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

	--- 9010001-010-011-0004 Tro cap Huu tri
			SELECT * INTO #tempDetailDuToanHuuTri FROM 
			(
				SELECT * FROM #tempKhoiDuToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanDuToanHuuTri
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNDuToanHuuTri
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPDuToanHuuTri
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSDuToanHuuTri
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDDuToanHuuTri
			)tblDetailHuuTri

			SELECT * INTO #tempDetailHachToanHuuTri FROM 
			(
				SELECT * FROM #tempKhoiHachToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanHachToanHuuTri
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNHachToanHuuTri
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPHachToanHuuTri
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSHachToanHuuTri
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDHachToanHuuTri
			)tblDetailHuuTri

			--- UPDATE #tempDetailDuToanHuuTri
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanHuuTri
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanHuuTri
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanHuuTri
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanHuuTri
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanHuuTri
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Du Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanHuuTri
				WHERE Type=0
			) B
			where  A.Type=2

			--- UPDATE ##tempDetailHachToanHuuTri
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanHuuTri
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanHuuTri
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanHuuTri
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanHuuTri
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanHuuTri
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Hach Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanHuuTri
				WHERE Type=0
			) B
			where  A.Type=2

			SELECT * INTO #tempHuuTri FROM
			(
			SELECT * FROM #tempHuuTriDuToan
			UNION ALL
			SELECT * FROM #tempDetailDuToanHuuTri
			UNION ALL
			SELECT * FROM #tempDetailHachToanHuuTri
			
			) tblHuutri

			--- UPDATE #tempHuuTri
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempHuuTri
				WHERE Type=0
			) B
			where  A.Type=3


	----- 9010001-010-011-0005 Tro cap phuc vien
			SELECT * INTO #tempDetailDuToanPhucVien FROM 
			(
				SELECT * FROM #tempKhoiDuToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanDuToanPhucVien
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNDuToanPhucVien
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPDuToanPhucVien
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSDuToanPhucVien
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDDuToanPhucVien
			)tblDetailPhucVien

			SELECT * INTO #tempDetailHachToanPhucVien FROM 
			(
				SELECT * FROM #tempKhoiHachToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanHachToanPhucVien
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNHachToanPhucVien
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPHachToanPhucVien
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSHachToanPhucVien
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDHachToanPhucVien
			)tblDetailPhucVien

		
			--- UPDATE ##tempDetailDuToanPhucVien
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanPhucVien
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanPhucVien
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanPhucVien
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanPhucVien
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanPhucVien
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Du Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanPhucVien
				WHERE Type=0
			) B
			where  A.Type=2

			--- UPDATE ###tempDetailHachToanPhucVien
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanPhucVien
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanPhucVien
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanPhucVien
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanPhucVien
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanPhucVien
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Hach Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanPhucVien
				WHERE Type=0
			) B
			where  A.Type=2

			SELECT * INTO #tempPhucVien FROM
			(
			SELECT * FROM #tempPhucVienDuToan
			UNION ALL
			SELECT * FROM #tempDetailDuToanPhucVien
			UNION ALL
			SELECT * FROM #tempDetailHachToanPhucVien
			) tblPhucVien

			--- UPDATE #tempPhucVien
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempPhucVien
				WHERE Type=0
			) B
			where  A.Type=3

			
	----- 9010001-010-011-0006 Tro cap xuat ngu

			SELECT * INTO #tempDetailDuToanXuatNgu FROM 
			(
				SELECT * FROM #tempKhoiDuToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanDuToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNDuToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPDuToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSDuToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDDuToanXuatNgu
			)tblDetailPXuatNgu


			SELECT * INTO #tempDetailHachToanXuatNgu FROM 
			(
				SELECT * FROM #tempKhoiHachToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanHachToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNHachToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPHachToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSHachToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDHachToanXuatNgu
			)tblDetailXuatNgu

			--- UPDATE #tempDetailDuToanXuatNgu
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanXuatNgu
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanXuatNgu
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanXuatNgu
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanXuatNgu
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanXuatNgu
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Du Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanXuatNgu
				WHERE Type=0
			) B
			where  A.Type=2


			--- UPDATE #tempDetailHachToanXuatNgu
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanXuatNgu
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanXuatNgu
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanXuatNgu
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanXuatNgu
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanXuatNgu
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- Update Hach Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanXuatNgu
				WHERE Type=0
			) B
			where  A.Type=2
			SELECT * INTO #tempXuatNgu FROM
			(
			SELECT * FROM #tempXuatNguDuToan
			UNION ALL
			SELECT * FROM #tempDetailDuToanXuatNgu
			UNION ALL
			SELECT * FROM #tempDetailHachToanXuatNgu
			) tblXuatNgu

			--- UPDATE #tempXuatNgu
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempXuatNgu
				WHERE Type=0
			) B
			where  A.Type=3
	----- 9010001-010-011-0007 tro cap thoi viec
			SELECT * INTO #tempDetailDuToanThoiViec FROM 
			(
				SELECT * FROM #tempKhoiDuToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanDuToanThoiViec
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNDuToanThoiViec
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPDuToanThoiViec
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSDuToanThoiViec
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDDuToanThoiViec
			)tblDetailThoiViec

			SELECT * INTO #tempDetailHachToanThoiViec FROM 
			(
				SELECT * FROM #tempKhoiHachToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanHachToanThoiViec
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNHachToanThoiViec
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPHachToanThoiViec
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSHachToanThoiViec
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDHachToanThoiViec
			)tblDetailThoiViec

	 	--- UPDATE #tempDetailDuToanXuatNgu
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanThoiViec
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanThoiViec
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanThoiViec
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanThoiViec
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanThoiViec
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Du Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanThoiViec
				WHERE Type=0
			) B
			where  A.Type=2

			--- UPDATE #tempDetailHachToanThoiViec
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanThoiViec
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanThoiViec
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanThoiViec
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanThoiViec
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanThoiViec
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Hach Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanThoiViec
				WHERE Type=0
			) B
			where  A.Type=2

			SELECT * INTO #tempThoiViec FROM
			(
			SELECT * FROM #tempThoiViecDuToan
			UNION ALL
			SELECT * FROM #tempDetailDuToanThoiViec
			UNION ALL
			SELECT * FROM #tempDetailHachToanThoiViec
			) tblThoiViec
			 
			 --- UPDATE #tempThoiViec
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempThoiViec
				WHERE Type=0
			) B
			where  A.Type=3
		
	----- 9010001-010-011-0008 tro cap tu tuat
			SELECT * INTO #tempDetailDuToanTuTuat FROM 
			(
				SELECT * FROM #tempKhoiDuToan 
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanDuToanTuTuat
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNDuToanTuTuat
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPDuToanTuTuat
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSDuToanTuTuat
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDDuToanTuTuat
			)tblDetailTuTuat

			SELECT * INTO #tempDetailHachToanTuTuat FROM 
			(
				SELECT * FROM #tempKhoiHachToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanHachToanTuTuat
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNHachToanTuTuat
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPHachToanTuTuat
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSHachToanTuTuat
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDHachToanTuTuat
			)tblDetailTuTuat

			--- UPDATE #tempDetailDuToanTuTuat
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanTuTuat
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanTuTuat
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanTuTuat
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanTuTuat
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanTuTuat
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Du Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanTuTuat
				WHERE Type=0
			) B
			where  A.Type=2

			--- UPDATE #tempDetailHachToanTuTuat
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanTuTuat
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanTuTuat
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanTuTuat
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanTuTuat
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanTuTuat
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Hach Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanTuTuat
				WHERE Type=0
			) B
			where  A.Type=2
			SELECT * INTO #tempTuTuat FROM
			(
			SELECT * FROM #tempTuTuatDuToan
			UNION ALL
			SELECT * FROM #tempDetailDuToanTuTuat
			UNION ALL
			SELECT * FROM #tempDetailHachToanTuTuat
			) tblTuTuat

			--- Update #tempTuTuat
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempTuTuat
				WHERE Type=0
			) B
			where  A.Type=3

			SELECT * INTO #tempRESULT  
			FROM
			(
				SELECT * FROM #tempHuuTri
				UNION ALL 
				SELECT * FROM #tempPhucVien
				UNION ALL 
				SELECT * FROM #tempXuatNgu
				UNION ALL
				SELECT * FROM #tempThoiViec
				UNION ALL
				SELECT * FROM #tempTuTuat
			) TBLRESULT

			SELECT * FROM #tempRESULT


		 DROP TABLE #tempRESULT
		 DROP TABLE #tblTroCapKhoiDuToan 
		 DROP TABLE #tblTroCapKhoiHachToan
		 DROP TABLE #tempHuuTri
		 DROP TABLE #tempDetailDuToanHuuTri
		 DROP TABLE #tempDetailHachToanHuuTri


		 DROP TABLE #tempPhucVien
		 DROP TABLE #tempDetailDuToanPhucVien
		 DROP TABLE #tempDetailHachToanPhucVien

		 DROP TABLE #tempXuatNgu 
		 DROP TABLE #tempDetailDuToanXuatNgu
		 DROP TABLE #tempDetailHachToanXuatNgu

		 DROP TABLE #tempThoiViec
		 DROP TABLE #tempDetailDuToanThoiViec
		 DROP TABLE #tempDetailHachToanThoiViec

		 DROP TABLE #tempTuTuat
		 DROP TABLE #tempDetailDuToanTuTuat
		 DROP TABLE #tempDetailHachToanTuTuat

		 DROP TABLE #tempKhoiDuToan
		 DROP TABLE #tempKhoiHachToan

		 DROP TABLE #tempHuuTriDuToan
		 DROP TABLE #tempPhucVienDuToan
		 DROP TABLE #tempXuatNguDuToan
		 DROP TABLE #tempThoiViecDuToan
		 DROP TABLE #tempTuTuatDuToan

		 DROP TABLE #tempSiQuan
		 DROP TABLE #tempQNCN
		 DROP TABLE #tempCNVCQP
		 DROP TABLE #tempHSQBS
		 DROP TABLE #tempLDHD

		 DROP TABLE #tempDetailSiQuanDuToanHuuTri
		 DROP TABLE #tempDetailQNCNDuToanHuuTri
		 DROP TABLE #tempDetailCNVCQPDuToanHuuTri
		 DROP TABLE #tempDetailHSQBSDuToanHuuTri
		 DROP TABLE #tempDetailLDHDDuToanHuuTri

		 DROP TABLE #tempDetailSiQuanDuToanPhucVien
		 DROP TABLE #tempDetailQNCNDuToanPhucVien
		 DROP TABLE #tempDetailCNVCQPDuToanPhucVien
		 DROP TABLE #tempDetailHSQBSDuToanPhucVien
		 DROP TABLE #tempDetailLDHDDuToanPhucVien
		 
		 DROP TABLE #tempDetailSiQuanDuToanXuatNgu
		 DROP TABLE #tempDetailQNCNDuToanXuatNgu
		 DROP TABLE #tempDetailCNVCQPDuToanXuatNgu
		 DROP TABLE #tempDetailHSQBSDuToanXuatNgu
		 DROP TABLE #tempDetailLDHDDuToanXuatNgu

		 DROP TABLE #tempDetailSiQuanDuToanThoiViec
		 DROP TABLE #tempDetailQNCNDuToanThoiViec
		 DROP TABLE #tempDetailCNVCQPDuToanThoiViec
		 DROP TABLE #tempDetailHSQBSDuToanThoiViec
		 DROP TABLE #tempDetailLDHDDuToanThoiViec

		 DROP TABLE #tempDetailSiQuanDuToanTuTuat
		 DROP TABLE #tempDetailQNCNDuToanTuTuat
		 DROP TABLE #tempDetailCNVCQPDuToanTuTuat
		 DROP TABLE #tempDetailHSQBSDuToanTuTuat
		 DROP TABLE #tempDetailLDHDDuToanTuTuat

		 DROP TABLE #tempDetailSiQuanHachToanHuuTri
		 DROP TABLE #tempDetailQNCNHachToanHuuTri
		 DROP TABLE #tempDetailCNVCQPHachToanHuuTri
		 DROP TABLE #tempDetailHSQBSHachToanHuuTri
		 DROP TABLE #tempDetailLDHDHachToanHuuTri

		 DROP TABLE #tempDetailSiQuanHachToanPhucVien
		 DROP TABLE #tempDetailQNCNHachToanPhucVien
		 DROP TABLE #tempDetailCNVCQPHachToanPhucVien
		 DROP TABLE #tempDetailHSQBSHachToanPhucVien
		 DROP TABLE #tempDetailLDHDHachToanPhucVien
		 
		 DROP TABLE #tempDetailSiQuanHachToanXuatNgu
		 DROP TABLE #tempDetailQNCNHachToanXuatNgu
		 DROP TABLE #tempDetailCNVCQPHachToanXuatNgu
		 DROP TABLE #tempDetailHSQBSHachToanXuatNgu
		 DROP TABLE #tempDetailLDHDHachToanXuatNgu

		 DROP TABLE #tempDetailSiQuanHachToanThoiViec
		 DROP TABLE #tempDetailQNCNHachToanThoiViec
		 DROP TABLE #tempDetailCNVCQPHachToanThoiViec
		 DROP TABLE #tempDetailHSQBSHachToanThoiViec
		 DROP TABLE #tempDetailLDHDHachToanThoiViec

		 DROP TABLE #tempDetailSiQuanHachToanTuTuat
		 DROP TABLE #tempDetailQNCNHachToanTuTuat
		 DROP TABLE #tempDetailCNVCQPHachToanTuTuat
		 DROP TABLE #tempDetailHSQBSHachToanTuTuat
		 DROP TABLE #tempDetailLDHDHachToanTuTuat


		 DROP TABLE #tempHuuTriHachToan
		 DROP TABLE #tempPhucVienHachToan
		 DROP TABLE #tempXuatNguHachToan
		 Drop table #tempThoiViecHachToan
		 Drop table #tempTuTuatHachToan

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
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Khoi_Detail]    Script Date: 7/12/2024 8:13:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Khoi_Detail]
	@INamLamViec int,
	@IdMaDonVi nvarchar(MAX),
	@IQuy int,
	@Donvitinh int
AS
BEGIN

	---IRemainRow 1: SQ, 2 : QNCN,3:CC, CN, VCQP,4:HSQBS, 5:LĐHĐ
	--- Type  :3 :Huu Tri,phuc vien,xuat ngu,thoi viec, tu tuat
	--- 2: Khoi du Toan, Khoi hach toan
	--- 1 SQ, QNCN,CC, CN, VCQP,HSQBS, LĐHĐ
	---- Ikhoi : 2  Khoi Du toan, 1 Khoi Hach Toan
	SELECT gt.* INTO #tblTroCapKhoiDuToan FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gt
	left join DonVi dv on gt.iiD_MaDonVi=dv.iID_MaDonVi
	WHERE (--- Du toan
			gt.sXauNoiMa like '9010001-010-011-0004%'
			or gt.sXauNoiMa like '9010001-010-011-0005%'
			or gt.sXauNoiMa like '9010001-010-011-0006%'
			or gt.sXauNoiMa like '9010001-010-011-0007%'
			or gt.sXauNoiMa like '9010001-010-011-0008%'
			--- Hoach toan
			or gt.sXauNoiMa like '9010002-010-011-0004%'
			or gt.sXauNoiMa like '9010002-010-011-0005%'
			or gt.sXauNoiMa like '9010002-010-011-0006%'
			or gt.sXauNoiMa like '9010002-010-011-0007%'
			or gt.sXauNoiMa like '9010002-010-011-0008%')
		and gt.iNamLamViec = @INamLamViec and gt.iQuy = @IQuy and gt.iiD_MaDonVi in (SELECT * FROM f_split(@IdMaDonVi))
		and dv.iNamLamViec=@INamLamViec
		and dv.iTrangThai=1
		and dv.iKhoi=2
		order by gt.sTenCanBo

	SELECT gt.* INTO #tblTroCapKhoiHachToan FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gt
	left join DonVi dv on gt.iiD_MaDonVi=dv.iID_MaDonVi
	WHERE (--- Du toan
			gt.sXauNoiMa like '9010001-010-011-0004%'
			or gt.sXauNoiMa like '9010001-010-011-0005%'
			or gt.sXauNoiMa like '9010001-010-011-0006%'
			or gt.sXauNoiMa like '9010001-010-011-0007%'
			or gt.sXauNoiMa like '9010001-010-011-0008%'
			--- Hoach toan
			or gt.sXauNoiMa like '9010002-010-011-0004%'
			or gt.sXauNoiMa like '9010002-010-011-0005%'
			or gt.sXauNoiMa like '9010002-010-011-0006%'
			or gt.sXauNoiMa like '9010002-010-011-0007%'
			or gt.sXauNoiMa like '9010002-010-011-0008%')
		and gt.iNamLamViec = @INamLamViec and gt.iQuy = @IQuy and gt.iiD_MaDonVi in (SELECT * FROM f_split(@IdMaDonVi))
		and dv.iNamLamViec=@INamLamViec
		and dv.iTrangThai=1
		and dv.iKhoi=1
		order by gt.sTenCanBo
		--- Huu Tri Khoi Du Toan
			SELECT 
				1 bHangCha
				, N'(I)' STT
				, N'TC Hưu Trí'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempHuuTriDuToan

		--- Phuc Vien Khoi Du Toan
			SELECT 
				1 bHangCha
				, N'(II)' STT
				, N'TC Phục viên 'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempPhucVienDuToan

		--- TC Xuất ngũ Khoi Du Toan
			SELECT 
				1 bHangCha
				, N'(III)' STT
				, N'TC Xuất ngũ'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempXuatNguDuToan

		--- TC Thôi việc  Khoi Du Toan
			SELECT 
				1 bHangCha
				, N'(IV)' STT
				, N'TC Thôi việc'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempThoiViecDuToan

		--- TC Tử tuất  Khoi Du Toan
			SELECT 
				1 bHangCha
				, N'(V)' STT
				, N'TC Tử tuất 'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempTuTuatDuToan

		--- Huu Tri Khoi Hach Toan
			SELECT 
				1 bHangCha
				, N'(I)' STT
				, N'TC Hưu Trí'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempHuuTriHachToan

		--- Phuc Vien Khoi Hach Toan
			SELECT 
				1 bHangCha
				, N'(II)' STT
				, N'TC Phục viên 'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempPhucVienHachToan

		--- TC Xuất ngũ Khoi Hach Toan
			SELECT 
				1 bHangCha
				, N'(III)' STT
				, N'TC Xuất ngũ'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempXuatNguHachToan

		--- TC Thôi việc  Khoi Hach Toan

			SELECT 
				1 bHangCha
				, N'(IV)' STT
				, N'TC Thôi việc'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempThoiViecHachToan

		--- TC Tử tuất  Khoi Hach Toan
			SELECT 
				1 bHangCha
				, N'(V)' STT
				, N'TC Tử tuất 'as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempTuTuatHachToan

		--- Khoi Du Toan
			SELECT 
					1 bHangCha
					, N'Khối dự toán' STT
					, ''as STenCanBo 
					, '' as STenPhanHo
					, '' as SMaCapBac 
					, '' as SSoQuyetDinh
					, null as  DNgayQuyetDinh
					, null as FTienTroCap1Lan
					, null as FTienTroCapKV
					, null as FTienTroCapMT
					, null as FTienTroCap1LanTL
					, null as FTienTroCapKVTL
					, null as FTienTroCapMTTL
					, 0 bHasData
					, 2 Type
					, 1 IsParent
					, 0 IRemainRow
					, 2 IKhoi
					into #tempKhoiDuToan

		--- Khoi Hach Toan
			SELECT 
					1 bHangCha
					, N'Khối hạch toán' STT
					, ''as STenCanBo 
					, '' as STenPhanHo
					, '' as SMaCapBac 
					, '' as SSoQuyetDinh
					, null as  DNgayQuyetDinh
					, null as FTienTroCap1Lan
					, null as FTienTroCapKV
					, null as FTienTroCapMT
					, null as FTienTroCap1LanTL
					, null as FTienTroCapKVTL
					, null as FTienTroCapMTTL
					, 0 bHasData
					, 2 Type
					, 1 IsParent
					, 0 IRemainRow
					, 1 IKhoi
					into #tempKhoiHachToan

		--- Si Quan
			SELECT 
				1 bHangCha
				, N'Si quan' STT
				, ''as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 1 Type
				, 1 IsParent
				, 1 IRemainRow
				, 0 IKhoi
				into #tempSiQuan

		--- QNCN
			SELECT 
				1 bHangCha
				, N'QNCN' STT
				, ''as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 1 Type
				, 1 IsParent
				, 2 IRemainRow
				, 0 IKhoi
				into #tempQNCN

		--- CC, CN, VCQP
			SELECT 
				1 bHangCha
				, N'CC, CN, VCQP' STT
				, ''as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 1 Type
				, 1 IsParent
				, 3 IRemainRow
				, 0 IKhoi
				into #tempCNVCQP

        --- HSQBS
			SELECT 
				1 bHangCha
				, N'HSQBS' STT
				, ''as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 1 Type
				, 1 IsParent
				, 4 IRemainRow
				, 0 IKhoi
				into #tempHSQBS

		--- LĐHĐ
			SELECT 
				1 bHangCha
				, N'LĐHĐ' STT
				, ''as STenCanBo 
				, '' as STenPhanHo
				, '' as SMaCapBac 
				, '' as SSoQuyetDinh
				, null as  DNgayQuyetDinh
				, null as FTienTroCap1Lan
				, null as FTienTroCapKV
				, null as FTienTroCapMT
				, null as FTienTroCap1LanTL
				, null as FTienTroCapKVTL
				, null as FTienTroCapMTTL
				, 0 bHasData
				, 1 Type
				, 1 IsParent
				, 5 IRemainRow
				, 0 IKhoi
				into #tempLDHD
--- Du Toan
--- Huu Tri Du Toan
		--- Detal Si Quan DuToan Huu Tri
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi
			into #tempDetailSiQuanDuToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
		--- Detal QNCN DuToan Huu Tri
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 2 IKhoi
			into #tempDetailQNCNDuToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal CNVCQP DuToan Huu Tri
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 2 IKhoi
			into #tempDetailCNVCQPDuToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal HSQBS DuToan Huu Tri
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi
			into #tempDetailHSQBSDuToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac LIKE '0%')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal LDHD DuToan Huu Tri
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 2 IKhoi
			into #tempDetailLDHDDuToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
--- Phuc Vien Du Toan
			--- Detal Si Quan  Phục viên 
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi
			into #tempDetailSiQuanDuToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal QNCN DuToan Phục viên 
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 2 IKhoi
			into #tempDetailQNCNDuToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal CNVCQP DuToan Phuc Vien
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 2 IKhoi
			into #tempDetailCNVCQPDuToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal HSQBS DuToan Phục viên 
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi
			into #tempDetailHSQBSDuToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal LDHD DuToan Phuc Vien
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 2 IKhoi
			into #tempDetailLDHDDuToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
--- Xuat Ngu Du Toan
			--- Detal Si Quan  Xuat Ngu
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) /1 ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / 1 ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi
			into #tempDetailSiQuanDuToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal QNCN DuToan Xuat Ngu 
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) /1 ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / 1 ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 2 IKhoi
			into #tempDetailQNCNDuToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal CNVCQP DuToan Phuc Vien
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) /1 ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / 1 ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 2 IKhoi
			into #tempDetailCNVCQPDuToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%' )
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal HSQBS DuToan Xuat Ngu
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) /1 ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / 1 ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi
			into #tempDetailHSQBSDuToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal LDHD DuToan Xuat Ngu
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) /1 ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / 1 ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 2 IKhoi
			into #tempDetailLDHDDuToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
--- Thoi Viec Du Toan
			--- Detal Si Quan DuToan Thoi Viec
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi
			into #tempDetailSiQuanDuToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal QNCN DuToan Thoi Viec
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 2 IKhoi
			into #tempDetailQNCNDuToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal CNVCQP DuToan Thoi Viec
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 2 IKhoi
			into #tempDetailCNVCQPDuToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%' )
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal HSQBS DuToan Thoi Viec
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi
			into #tempDetailHSQBSDuToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal LDHD DuToan Thoi Viec
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 2 IKhoi
			into #tempDetailLDHDDuToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
--- Tu Tuat  Du Toan
			--- Detal Si Quan DuToan Tu Tuat
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi
			into #tempDetailSiQuanDuToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal QNCN DuToan Tu Tuat
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 2 IKhoi
			into #tempDetailQNCNDuToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal CNVCQP DuToan Tu Tuat
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 2 IKhoi
			into #tempDetailCNVCQPDuToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%' )
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal HSQBS DuToan Tu Tuat
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi
			into #tempDetailHSQBSDuToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal LDHD DuToan Thoi Viec
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 2 IKhoi
			into #tempDetailLDHDDuToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
--- Hach Toan
--- Huu Tri HachToan
		--- Detal Si Quan HachToan Huu Tri
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi
			into #tempDetailSiQuanHachToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
		--- Detal QNCN HachToan Huu Tri
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi
			into #tempDetailQNCNHachToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal CNVCQP HachToan Huu Tri
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi
			into #tempDetailCNVCQPHachToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal HSQBS HachToan Huu Tri
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi
			into #tempDetailHSQBSHachToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac LIKE '0%')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal LDHD HachToan Huu Tri
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi
			into #tempDetailLDHDHachToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
--- Phuc Vien Hach Toan
			--- Detal Si Quan HachToan Phục viên 
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi
			into #tempDetailSiQuanHachToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal QNCN HachToan Phục viên 
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi
			into #tempDetailQNCNHachToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal CNVCQP HachToan Phuc Vien
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi
			into #tempDetailCNVCQPHachToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal HSQBS HachToan Phục viên 
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi
			into #tempDetailHSQBSHachToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal LDHD HachToan Phuc Vien
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi
			into #tempDetailLDHDHachToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
--- Xuat Ngu Hach Toan
			--- Detal Si Quan HachToan  Xuat Ngu
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) /1 ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / 1 ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi
			into #tempDetailSiQuanHachToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal QNCN HachToan Xuat Ngu 
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) /1 ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / 1 ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi
			into #tempDetailQNCNHachToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal CNVCQP HachToan Phuc Vien
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) /1 ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / 1 ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi
			into #tempDetailCNVCQPHachToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%' )
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal HSQBS HachToan Xuat Ngu
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) /1 ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / 1 ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi
			into #tempDetailHSQBSHachToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal LDHD HachToan Xuat Ngu
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) /1 ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / 1 ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi
			into #tempDetailLDHDHachToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
--- Thoi Viec Du Toan
			--- Detal Si Quan HachToan Thoi Viec
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi
			into #tempDetailSiQuanHachToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal QNCN HachToan Thoi Viec
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi
			into #tempDetailQNCNHachToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal CNVCQP HachToan Thoi Viec
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi
			into #tempDetailCNVCQPHachToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%' )
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal HSQBS HachToan Thoi Viec
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi
			into #tempDetailHSQBSHachToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal LDHD HachToan Thoi Viec
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi
			into #tempDetailLDHDHachToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
--- Tu Tuat  Du Toan
			--- Detal Si Quan HachToan Tu Tuat
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi
			into #tempDetailSiQuanHachToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal QNCN HachToan Tu Tuat
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi
			into #tempDetailQNCNHachToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal CNVCQP HachToan Tu Tuat
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi
			into #tempDetailCNVCQPHachToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%' )
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal HSQBS HachToan Tu Tuat
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi
			into #tempDetailHSQBSHachToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal LDHD HachToan Thoi Viec
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi
			into #tempDetailLDHDHachToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
	--- 9010001-010-011-0004 Tro cap Huu tri
			SELECT * INTO #tempDetailDuToanHuuTri FROM 
			(
				SELECT * FROM #tempKhoiDuToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanDuToanHuuTri
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNDuToanHuuTri
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPDuToanHuuTri
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSDuToanHuuTri
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDDuToanHuuTri
			)tblDetailHuuTri

			SELECT * INTO #tempDetailHachToanHuuTri FROM 
			(
				SELECT * FROM #tempKhoiHachToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanHachToanHuuTri
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNHachToanHuuTri
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPHachToanHuuTri
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSHachToanHuuTri
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDHachToanHuuTri
			)tblDetailHuuTri

			--- UPDATE #tempDetailDuToanHuuTri
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanHuuTri
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanHuuTri
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanHuuTri
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanHuuTri
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanHuuTri
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Du Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanHuuTri
				WHERE Type=0
			) B
			where  A.Type=2

			--- UPDATE ##tempDetailHachToanHuuTri
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanHuuTri
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanHuuTri
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanHuuTri
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanHuuTri
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanHuuTri
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Hach Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanHuuTri
				WHERE Type=0
			) B
			where  A.Type=2

			SELECT * INTO #tempHuuTri FROM
			(
			SELECT * FROM #tempHuuTriDuToan
			UNION ALL
			SELECT * FROM #tempDetailDuToanHuuTri
			UNION ALL
			SELECT * FROM #tempDetailHachToanHuuTri
			
			) tblHuutri

			--- UPDATE #tempHuuTri
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempHuuTri
				WHERE Type=0
			) B
			where  A.Type=3


	----- 9010001-010-011-0005 Tro cap phuc vien
			SELECT * INTO #tempDetailDuToanPhucVien FROM 
			(
				SELECT * FROM #tempKhoiDuToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanDuToanPhucVien
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNDuToanPhucVien
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPDuToanPhucVien
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSDuToanPhucVien
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDDuToanPhucVien
			)tblDetailPhucVien

			SELECT * INTO #tempDetailHachToanPhucVien FROM 
			(
				SELECT * FROM #tempKhoiHachToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanHachToanPhucVien
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNHachToanPhucVien
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPHachToanPhucVien
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSHachToanPhucVien
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDHachToanPhucVien
			)tblDetailPhucVien

		
			--- UPDATE ##tempDetailDuToanPhucVien
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanPhucVien
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanPhucVien
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanPhucVien
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanPhucVien
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanPhucVien
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Du Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanPhucVien
				WHERE Type=0
			) B
			where  A.Type=2

			--- UPDATE ###tempDetailHachToanPhucVien
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanPhucVien
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanPhucVien
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanPhucVien
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanPhucVien
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanPhucVien
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Hach Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanPhucVien
				WHERE Type=0
			) B
			where  A.Type=2

			SELECT * INTO #tempPhucVien FROM
			(
			SELECT * FROM #tempPhucVienDuToan
			UNION ALL
			SELECT * FROM #tempDetailDuToanPhucVien
			UNION ALL
			SELECT * FROM #tempDetailHachToanPhucVien
			) tblPhucVien

			--- UPDATE #tempPhucVien
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempPhucVien
				WHERE Type=0
			) B
			where  A.Type=3

			
	----- 9010001-010-011-0006 Tro cap xuat ngu

			SELECT * INTO #tempDetailDuToanXuatNgu FROM 
			(
				SELECT * FROM #tempKhoiDuToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanDuToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNDuToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPDuToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSDuToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDDuToanXuatNgu
			)tblDetailPXuatNgu


			SELECT * INTO #tempDetailHachToanXuatNgu FROM 
			(
				SELECT * FROM #tempKhoiHachToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanHachToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNHachToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPHachToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSHachToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDHachToanXuatNgu
			)tblDetailXuatNgu

			--- UPDATE #tempDetailDuToanXuatNgu
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanXuatNgu
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanXuatNgu
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanXuatNgu
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanXuatNgu
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanXuatNgu
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Du Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanXuatNgu
				WHERE Type=0
			) B
			where  A.Type=2


			--- UPDATE #tempDetailHachToanXuatNgu
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanXuatNgu
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanXuatNgu
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanXuatNgu
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanXuatNgu
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanXuatNgu
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- Update Hach Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanXuatNgu
				WHERE Type=0
			) B
			where  A.Type=2
			SELECT * INTO #tempXuatNgu FROM
			(
			SELECT * FROM #tempXuatNguDuToan
			UNION ALL
			SELECT * FROM #tempDetailDuToanXuatNgu
			UNION ALL
			SELECT * FROM #tempDetailHachToanXuatNgu
			) tblXuatNgu

			--- UPDATE #tempXuatNgu
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempXuatNgu
				WHERE Type=0
			) B
			where  A.Type=3
	----- 9010001-010-011-0007 tro cap thoi viec
			SELECT * INTO #tempDetailDuToanThoiViec FROM 
			(
				SELECT * FROM #tempKhoiDuToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanDuToanThoiViec
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNDuToanThoiViec
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPDuToanThoiViec
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSDuToanThoiViec
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDDuToanThoiViec
			)tblDetailThoiViec

			SELECT * INTO #tempDetailHachToanThoiViec FROM 
			(
				SELECT * FROM #tempKhoiHachToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanHachToanThoiViec
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNHachToanThoiViec
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPHachToanThoiViec
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSHachToanThoiViec
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDHachToanThoiViec
			)tblDetailThoiViec

	 	--- UPDATE #tempDetailDuToanXuatNgu
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanThoiViec
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanThoiViec
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanThoiViec
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanThoiViec
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanThoiViec
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Du Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanThoiViec
				WHERE Type=0
			) B
			where  A.Type=2

			--- UPDATE #tempDetailHachToanThoiViec
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanThoiViec
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanThoiViec
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanThoiViec
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanThoiViec
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanThoiViec
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Hach Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanThoiViec
				WHERE Type=0
			) B
			where  A.Type=2

			SELECT * INTO #tempThoiViec FROM
			(
			SELECT * FROM #tempThoiViecDuToan
			UNION ALL
			SELECT * FROM #tempDetailDuToanThoiViec
			UNION ALL
			SELECT * FROM #tempDetailHachToanThoiViec
			) tblThoiViec
			 
			 --- UPDATE #tempThoiViec
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempThoiViec
				WHERE Type=0
			) B
			where  A.Type=3
		
	----- 9010001-010-011-0008 tro cap tu tuat
			SELECT * INTO #tempDetailDuToanTuTuat FROM 
			(
				SELECT * FROM #tempKhoiDuToan 
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanDuToanTuTuat
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNDuToanTuTuat
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPDuToanTuTuat
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSDuToanTuTuat
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDDuToanTuTuat
			)tblDetailTuTuat

			SELECT * INTO #tempDetailHachToanTuTuat FROM 
			(
				SELECT * FROM #tempKhoiHachToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanHachToanTuTuat
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNHachToanTuTuat
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPHachToanTuTuat
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSHachToanTuTuat
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDHachToanTuTuat
			)tblDetailTuTuat

			--- UPDATE #tempDetailDuToanTuTuat
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanTuTuat
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanTuTuat
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanTuTuat
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanTuTuat
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanTuTuat
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Du Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanTuTuat
				WHERE Type=0
			) B
			where  A.Type=2

			--- UPDATE #tempDetailHachToanTuTuat
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanTuTuat
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanTuTuat
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanTuTuat
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanTuTuat
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanTuTuat
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Hach Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanTuTuat
				WHERE Type=0
			) B
			where  A.Type=2
			SELECT * INTO #tempTuTuat FROM
			(
			SELECT * FROM #tempTuTuatDuToan
			UNION ALL
			SELECT * FROM #tempDetailDuToanTuTuat
			UNION ALL
			SELECT * FROM #tempDetailHachToanTuTuat
			) tblTuTuat

			--- Update #tempTuTuat
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempTuTuat
				WHERE Type=0
			) B
			where  A.Type=3

			SELECT * INTO #tempRESULT  
			FROM
			(
				SELECT * FROM #tempHuuTri
				UNION ALL 
				SELECT * FROM #tempPhucVien
				UNION ALL 
				SELECT * FROM #tempXuatNgu
				UNION ALL
				SELECT * FROM #tempThoiViec
				UNION ALL
				SELECT * FROM #tempTuTuat
			) TBLRESULT

			SELECT * FROM #tempRESULT


		 DROP TABLE #tempRESULT
		 DROP TABLE #tblTroCapKhoiDuToan 
		 DROP TABLE #tblTroCapKhoiHachToan
		 DROP TABLE #tempHuuTri
		 DROP TABLE #tempDetailDuToanHuuTri
		 DROP TABLE #tempDetailHachToanHuuTri


		 DROP TABLE #tempPhucVien
		 DROP TABLE #tempDetailDuToanPhucVien
		 DROP TABLE #tempDetailHachToanPhucVien

		 DROP TABLE #tempXuatNgu 
		 DROP TABLE #tempDetailDuToanXuatNgu
		 DROP TABLE #tempDetailHachToanXuatNgu

		 DROP TABLE #tempThoiViec
		 DROP TABLE #tempDetailDuToanThoiViec
		 DROP TABLE #tempDetailHachToanThoiViec

		 DROP TABLE #tempTuTuat
		 DROP TABLE #tempDetailDuToanTuTuat
		 DROP TABLE #tempDetailHachToanTuTuat

		 DROP TABLE #tempKhoiDuToan
		 DROP TABLE #tempKhoiHachToan

		 DROP TABLE #tempHuuTriDuToan
		 DROP TABLE #tempPhucVienDuToan
		 DROP TABLE #tempXuatNguDuToan
		 DROP TABLE #tempThoiViecDuToan
		 DROP TABLE #tempTuTuatDuToan

		 DROP TABLE #tempSiQuan
		 DROP TABLE #tempQNCN
		 DROP TABLE #tempCNVCQP
		 DROP TABLE #tempHSQBS
		 DROP TABLE #tempLDHD

		 DROP TABLE #tempDetailSiQuanDuToanHuuTri
		 DROP TABLE #tempDetailQNCNDuToanHuuTri
		 DROP TABLE #tempDetailCNVCQPDuToanHuuTri
		 DROP TABLE #tempDetailHSQBSDuToanHuuTri
		 DROP TABLE #tempDetailLDHDDuToanHuuTri

		 DROP TABLE #tempDetailSiQuanDuToanPhucVien
		 DROP TABLE #tempDetailQNCNDuToanPhucVien
		 DROP TABLE #tempDetailCNVCQPDuToanPhucVien
		 DROP TABLE #tempDetailHSQBSDuToanPhucVien
		 DROP TABLE #tempDetailLDHDDuToanPhucVien
		 
		 DROP TABLE #tempDetailSiQuanDuToanXuatNgu
		 DROP TABLE #tempDetailQNCNDuToanXuatNgu
		 DROP TABLE #tempDetailCNVCQPDuToanXuatNgu
		 DROP TABLE #tempDetailHSQBSDuToanXuatNgu
		 DROP TABLE #tempDetailLDHDDuToanXuatNgu

		 DROP TABLE #tempDetailSiQuanDuToanThoiViec
		 DROP TABLE #tempDetailQNCNDuToanThoiViec
		 DROP TABLE #tempDetailCNVCQPDuToanThoiViec
		 DROP TABLE #tempDetailHSQBSDuToanThoiViec
		 DROP TABLE #tempDetailLDHDDuToanThoiViec

		 DROP TABLE #tempDetailSiQuanDuToanTuTuat
		 DROP TABLE #tempDetailQNCNDuToanTuTuat
		 DROP TABLE #tempDetailCNVCQPDuToanTuTuat
		 DROP TABLE #tempDetailHSQBSDuToanTuTuat
		 DROP TABLE #tempDetailLDHDDuToanTuTuat

		 DROP TABLE #tempDetailSiQuanHachToanHuuTri
		 DROP TABLE #tempDetailQNCNHachToanHuuTri
		 DROP TABLE #tempDetailCNVCQPHachToanHuuTri
		 DROP TABLE #tempDetailHSQBSHachToanHuuTri
		 DROP TABLE #tempDetailLDHDHachToanHuuTri

		 DROP TABLE #tempDetailSiQuanHachToanPhucVien
		 DROP TABLE #tempDetailQNCNHachToanPhucVien
		 DROP TABLE #tempDetailCNVCQPHachToanPhucVien
		 DROP TABLE #tempDetailHSQBSHachToanPhucVien
		 DROP TABLE #tempDetailLDHDHachToanPhucVien
		 
		 DROP TABLE #tempDetailSiQuanHachToanXuatNgu
		 DROP TABLE #tempDetailQNCNHachToanXuatNgu
		 DROP TABLE #tempDetailCNVCQPHachToanXuatNgu
		 DROP TABLE #tempDetailHSQBSHachToanXuatNgu
		 DROP TABLE #tempDetailLDHDHachToanXuatNgu

		 DROP TABLE #tempDetailSiQuanHachToanThoiViec
		 DROP TABLE #tempDetailQNCNHachToanThoiViec
		 DROP TABLE #tempDetailCNVCQPHachToanThoiViec
		 DROP TABLE #tempDetailHSQBSHachToanThoiViec
		 DROP TABLE #tempDetailLDHDHachToanThoiViec

		 DROP TABLE #tempDetailSiQuanHachToanTuTuat
		 DROP TABLE #tempDetailQNCNHachToanTuTuat
		 DROP TABLE #tempDetailCNVCQPHachToanTuTuat
		 DROP TABLE #tempDetailHSQBSHachToanTuTuat
		 DROP TABLE #tempDetailLDHDHachToanTuTuat


		 DROP TABLE #tempHuuTriHachToan
		 DROP TABLE #tempPhucVienHachToan
		 DROP TABLE #tempXuatNguHachToan
		 Drop table #tempThoiViecHachToan
		 Drop table #tempTuTuatHachToan

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
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_gttrocap_huutri_phucvien_xuatngu_thoiviec_tutuat]    Script Date: 7/12/2024 8:13:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_qtcq_gttrocap_huutri_phucvien_xuatngu_thoiviec_tutuat]
	@INamLamViec int,
	@IdMaDonVi nvarchar(MAX),
	@IQuy int,
	@Donvitinh int
AS
BEGIN
SELECT gt.* INTO #tblTroCap FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gt
	WHERE (--- Du toan
			gt.sXauNoiMa like '9010001-010-011-0004%'
			or gt.sXauNoiMa like '9010001-010-011-0005%'
			or gt.sXauNoiMa like '9010001-010-011-0006%'
			or gt.sXauNoiMa like '9010001-010-011-0007%'
			or gt.sXauNoiMa like '9010001-010-011-0008%'
			--- Hoach toan
			or gt.sXauNoiMa like '9010002-010-011-0004%'
			or gt.sXauNoiMa like '9010002-010-011-0005%'
			or gt.sXauNoiMa like '9010002-010-011-0006%'
			or gt.sXauNoiMa like '9010002-010-011-0007%'
			or gt.sXauNoiMa like '9010002-010-011-0008%')
		and gt.iNamLamViec = @INamLamViec and gt.iQuy = @IQuy and gt.iiD_MaDonVi in (SELECT * FROM f_split(@IdMaDonVi))

	--- 9010001-010-011-0004 Tro cap Huu tri

		SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			into #TempDetailHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC
	SELECT * INTO #tempHuuTri FROM
	(
	SELECT 1 bHangCha
			, N'(I)' STT
			, N'TC Hưu Trí'as STenCanBo 
			, '' as STenPhanHo
			, '' as SMaCapBac 
			, null as SSoQuyetDinh
			, null as  DNgayQuyetDinh
			, null as FTienTroCap1Lan
			, null as FTienTroCapKV
			, null as FTienTroCapMT
			, null as FTienTroCap1LanTL
			, null as FTienTroCapKVTL
			, null as FTienTroCapMTTL
			, 0 bHasData
	UNION ALL
	SELECT * FROM #TempDetailHuuTri
	) tblHuutri

	 if (SELECT COUNT(1) FROM #tempHuuTri) > 2
		UPDATE #tempHuuTri set bHasData = 1

	
		UPDATE  A
		SET A.FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		A.FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		A.FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		A.FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		A.FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		A.FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempHuuTri A ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempHuuTri WHERE  bHangCha=0 ) detail
		where A.bHangCha=1


	--- 9010001-010-011-0005 Tro cap phuc vien
	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			INTO #tempDetailPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac	
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC
	SELECT * INTO #tempPhucVien FROM
	(
	SELECT 1 bHangCha
			, N'(II)' STT
			, N'TC Phục viên'as STenCanBo 
			, '' as STenPhanHo
			, '' as SMaCapBac 
			, null as SSoQuyetDinh
			, null as  DNgayQuyetDinh
			, null as FTienTroCap1Lan
			, null as FTienTroCapKV
			, null as FTienTroCapMT
			, null as FTienTroCap1LanTL
			, null as FTienTroCapKVTL
			, null as FTienTroCapMTTL
			, 0 bHasData
	UNION ALL
	SELECT * FROM #tempDetailPhucVien
	) tblPhucVien

	 if (SELECT COUNT(1) FROM #tempPhucVien) > 2
		UPDATE #tempPhucVien SET bHasData = 1
		UPDATE #tempPhucVien
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempPhucVien ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempPhucVien WHERE  bHangCha=0 ) detail
		where #tempPhucVien.bHangCha=1
	--- 9010001-010-011-0006 Tro cap xuat ngu

		SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sSoQuyetDinh) AS VARCHAR(6)) STT
			, Convert(varchar(10),(COUNT(tbltc.sSoQuyetDinh))) + N' Đồng chi' as sTenCanBo
			, tbltc.sTenPhanHo
			, '' as SMaCapBac		
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCap1Lan
			--, 0 AS  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS  FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) /1 ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / 1 ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			 INTO #tempDetailXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%'
	GROUP BY 
			 tbltc.sTenPhanHo
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
--ORDER BY  tbltc.sTenCanBo desc,tbltc.sMaCapBac desc
	SELECT * INTO #tempXuatNgu FROM
	(
	SELECT 1 bHangCha
			, N'(III)' STT
			, N'TC Xuất ngũ'as STenCanBo 
			, '' as STenPhanHo
			, '' as SMaCapBac		
			, null as SSoQuyetDinh
			, null as  DNgayQuyetDinh
			, null as FTienTroCap1Lan
			, null as FTienTroCapKV
			, null as FTienTroCapMT
			, null as FTienTroCap1LanTL
			, null as FTienTroCapKVTL
			, null as FTienTroCapMTTL
			, 0 bHasData
	UNION ALL
	SELECT * FROM #tempDetailXuatNgu
	) tblXuatNgu

	 if (SELECT COUNT(1) from #tempXuatNgu) > 2
		UPDATE #tempXuatNgu set bHasData = 1
		UPDATE #tempXuatNgu
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempXuatNgu ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempXuatNgu WHERE  bHangCha=0 ) detail
		where #tempXuatNgu.bHangCha=1
	--- 9010001-010-011-0007 tro cap thoi viec

	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			into #tempDetailThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC
	SELECT * INTO #tempThoiViec FROM
	(
	SELECT 1 bHangCha
			, N'(IV)' STT
			, N'TC Thôi việc'as STenCanBo 
			, '' as STenPhanHo
			, '' as SMaCapBac 
			, null as SSoQuyetDinh
			, null as  DNgayQuyetDinh
			, null as FTienTroCap1Lan
			, null as FTienTroCapKV
			, null as FTienTroCapMT
			, null as FTienTroCap1LanTL
			, null as FTienTroCapKVTL
			, null as FTienTroCapMTTL
			, 0 bHasData
	UNION ALL
	SELECT * FROM #tempDetailThoiViec
	) tblThoiViec

	 if (SELECT COUNT(1) FROM #tempThoiViec) > 2
		UPDATE #tempThoiViec SET bHasData = 1

		UPDATE #tempThoiViec
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempThoiViec ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempThoiViec WHERE  bHangCha=0 ) detail
		where #tempThoiViec.bHangCha=1
	--- 9010001-010-011-0008 tro cap tu tuat

	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			into #TempDetailTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa 
	ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC
	SELECT * INTO #tempTuTuat FROM
	(
	SELECT 1 bHangCha
			, N'(V)' STT
			, N'TC Tử tuất'as STenCanBo 
			, '' as STenPhanHo
			, '' as SMaCapBac 
			, null as SSoQuyetDinh
			, null as  DNgayQuyetDinh
			, null as FTienTroCap1Lan
			, null as FTienTroCapKV
			, null as FTienTroCapMT
			, null as FTienTroCap1LanTL
			, null as FTienTroCapKVTL
			, null as FTienTroCapMTTL
			, 0 bHasData
	UNION ALL
	SELECT * FROM #TempDetailTuTuat
	) tblTuTuat

	 if (SELECT COUNT(1) FROM #tempTuTuat) > 2
		UPDATE #tempTuTuat SET bHasData = 1

		UPDATE #tempTuTuat
		SET FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempTuTuat ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempTuTuat WHERE  bHangCha=0 ) detail
				where #tempTuTuat.bHangCha=1
		-- ket qua
	SELECT * INTO #tempRESULT  
	FROM
	(
		SELECT * FROM #tempHuuTri
		UNION ALL 
		SELECT * FROM #tempPhucVien
		UNION ALL 
		SELECT * FROM #tempXuatNgu
		UNION ALL
		SELECT * FROM #tempThoiViec
		UNION ALL
		SELECT * FROM #tempTuTuat
	) TBLRESULT

	SELECT * FROM #tempRESULT

	 DROP TABLE #tempHuuTri
	 DROP TABLE #tempPhucVien
	 DROP TABLE #tempXuatNgu
	 DROP TABLE #tempThoiViec
	 DROP TABLE #tempTuTuat
	 DROP TABLE #tempRESULT
	 DROP TABLE #TempDetailHuuTri
	 DROP TABLE #tempDetailPhucVien
	 DROP TABLE #TempDetailTuTuat
	 DROP TABLE #tempDetailThoiViec
	 DROP TABLE #tempDetailXuatNgu

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
