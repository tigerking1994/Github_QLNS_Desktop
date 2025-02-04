/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh]    Script Date: 8/22/2024 5:26:33 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Nhomdt_Detail]    Script Date: 8/22/2024 5:26:33 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Nhomdt_Detail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Nhomdt_Detail]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Nhomdt]    Script Date: 8/22/2024 5:26:33 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Nhomdt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Nhomdt]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_quykinhphi_quanly_chungtu_chitiet_tao_tonghop]    Script Date: 8/22/2024 5:26:33 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_quykinhphi_quanly_chungtu_chitiet_tao_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_quykinhphi_quanly_chungtu_chitiet_tao_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_luong_bhxh_import_qtc_bhxh]    Script Date: 8/22/2024 5:26:33 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_luong_bhxh_import_qtc_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_luong_bhxh_import_qtc_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_get_loaichi_thongtri]    Script Date: 8/22/2024 5:26:33 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_get_loaichi_thongtri]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_get_loaichi_thongtri]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_cp_get_donvi_thongtri]    Script Date: 8/22/2024 5:26:33 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_cp_get_donvi_thongtri]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_cp_get_donvi_thongtri]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_rpt_qttm_bhyt_than_nhan_nld_tong_hop_don_vi]    Script Date: 8/22/2024 5:26:33 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_rpt_qttm_bhyt_than_nhan_nld_tong_hop_don_vi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_rpt_qttm_bhyt_than_nhan_nld_tong_hop_don_vi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_khoi_Nhomdt]    Script Date: 8/22/2024 5:26:33 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_khoi_Nhomdt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_khoi_Nhomdt]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_khoi_Nhomdt]    Script Date: 8/22/2024 5:26:33 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_khoi_Nhomdt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_khoi_Nhomdt]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_khoi]    Script Date: 8/22/2024 5:26:33 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_khoi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_khoi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_khoi_nhomdt]    Script Date: 8/22/2024 5:26:33 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_khoi_nhomdt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_khoi_nhomdt]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_rpt_quyet_toan_thu_mua_bhyt_than_nhan_nld]    Script Date: 8/22/2024 5:26:33 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qttm_rpt_quyet_toan_thu_mua_bhyt_than_nhan_nld]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qttm_rpt_quyet_toan_thu_mua_bhyt_than_nhan_nld]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_rpt_qttm_bhyt_than_nhan_nld_tong_hop]    Script Date: 8/22/2024 5:26:33 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qttm_rpt_qttm_bhyt_than_nhan_nld_tong_hop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qttm_rpt_qttm_bhyt_than_nhan_nld_tong_hop]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqKPQL_create_datafor_quaterbefore]    Script Date: 8/22/2024 5:26:33 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtcqKPQL_create_datafor_quaterbefore]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtcqKPQL_create_datafor_quaterbefore]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqBHXH_create_datafor_quaterbefore]    Script Date: 8/22/2024 5:26:33 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtcqBHXH_create_datafor_quaterbefore]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtcqBHXH_create_datafor_quaterbefore]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqBHXH_create_data_summary]    Script Date: 8/22/2024 5:26:33 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtcqBHXH_create_data_summary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtcqBHXH_create_data_summary]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtc_get_canbo_salary_by_xaunoima_bhxh]    Script Date: 8/22/2024 5:26:33 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtc_get_canbo_salary_by_xaunoima_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtc_get_canbo_salary_by_xaunoima_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtc_get_canbo_salary_by_xaunoima_bhxh]    Script Date: 8/22/2024 5:26:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_bh_qtc_get_canbo_salary_by_xaunoima_bhxh] 
	-- Add the parameters for the stored procedure here
 	@sXauNoiMa nvarchar(max),
	@sQuarter nvarchar(50) ,
	@Donvi  nvarchar(50),
	@NamLamViec int,
	@sLNS nvarchar(50)
	
AS
BEGIN
	DECLARE @STongHop nvarchar(max);	
	DECLARE @XauNoiMaREPLACE nvarchar(max);
	set @XauNoiMaREPLACE =case when @sXauNoiMa like '9010001%' 
									then REPLACE(@sXauNoiMa,'9010001-' , '') 
									else REPLACE(@sXauNoiMa,'9010002-' , '') end  

	SELECT sMaCheDo 
			, bDisplay
			, case when sXauNoiMaMlnsBHXH like '9010001%' 
			then REPLACE(sXauNoiMaMlnsBHXH,'9010001-' , '')
			else REPLACE(sXauNoiMaMlnsBHXH,'9010002-' , '') end  sXauNoiMaMlnsBHXH
				into #tempTL_DM_CheDoBHXH
			FROM TL_DM_CheDoBHXH
			WHERE bDisplay = 1


	--select top(1) bangluong.STongHop into #temp1  FROM TL_BangLuong_ThangBHXH bangluongchitiet 
	--		JOIN  TL_DS_CapNhap_BangLuong bangluong on bangluongchitiet.iID_Parent = bangluong.Id
	--		inner join DonVi dv on dv.iID_MaDonVi=bangluong.Ma_CBo
	--		where
			
	--			bangluongchitiet.iThang IN (SELECT * FROM splitstring(@sQuarter))
	--				AND bangluongchitiet.sMaCheDo IN (SELECT sMaCheDo 
	--						FROM #tempTL_DM_CheDoBHXH
	--						WHERE bDisplay = 1 
	--							AND sXauNoiMaMlnsBHXH = @XauNoiMaREPLACE )
	--				AND (
	--					bangluongchitiet.sMaCB LIKE '1%'  
	--					OR bangluongchitiet.sMaCB LIKE '2%'  
	--					OR bangluongchitiet.sMaCB = '3.1'  
	--					OR bangluongchitiet.sMaCB = '3.2'  
	--					OR bangluongchitiet.sMaCB = '3.3'
	--					OR bangluongchitiet.sMaCB = '413' 
	--					OR bangluongchitiet.sMaCB = '415' 
	--					OR bangluongchitiet.sMaCB = '423' 
	--					OR bangluongchitiet.sMaCB = '425' 
	--					OR bangluongchitiet.sMaCB = '43' 
	--					OR bangluongchitiet.sMaCB LIKE '0%'
	--		)
	--		and bangluong.Ma_CachTL = N'CACH2'
	--		and bangluong.Ma_CBo = @Donvi
	--		and bangluong.KhoaBangLuong = 1
	--		and bangluong.Nam = @NamLamViec
	--		and bangluong.STongHop is not null
	--		and dv.iNamLamViec=@NamLamViec
	--		--(
	--		--	(@sLNS='9010002' and dv.iKhoi!=2)
	--		--	or (dv.iKhoi=2)
	--		--)
			
		

	--set @STongHop = (select #temp1.STongHop from #temp1);

	--select * into #tblSTongHop from splitstring(@STongHop);


	select * into #temp1 from TL_DS_CapNhap_BangLuong
	where Nam=@NamLamViec
	and Thang in  (select * from splitstring(@sQuarter))
	and Ma_CBo=@Donvi
	and STongHop is not null
	and Ma_CachTL='CACH2'
	and Status=1

	-- Get bang luong chi tiet 
	select bangluongchitiet.* into #temp2 from TL_BangLuong_ThangBHXH bangluongchitiet  
	inner join #temp1 bangluong on bangluong.Id=bangluongchitiet.iID_Parent and bangluong.Thang=bangluongchitiet.iThang
	where
		 bangluongchitiet.sMaCheDo IN (SELECT sMaCheDo 
										FROM #tempTL_DM_CheDoBHXH
										WHERE bDisplay = 1 
										AND sXauNoiMaMlnsBHXH = @XauNoiMaREPLACE )
	AND (
		bangluongchitiet.sMaCB LIKE '1%'  
			OR bangluongchitiet.sMaCB LIKE '2%'  
			OR bangluongchitiet.sMaCB = '3.1'  
			OR bangluongchitiet.sMaCB = '3.2'  
			OR bangluongchitiet.sMaCB = '3.3'
			OR bangluongchitiet.sMaCB = '413' 
			OR bangluongchitiet.sMaCB = '415' 
			OR bangluongchitiet.sMaCB = '423' 
			OR bangluongchitiet.sMaCB = '425' 
			OR bangluongchitiet.sMaCB = '43' 
			OR bangluongchitiet.sMaCB LIKE '0%'
	)

	--- Lấy tiền truy lĩnh---
	SELECT tbl.*, t2.fSoNgayTruyLinh, t2.nGiaTriTruyLinh INTO #temp3
	FROM #temp2 tbl
	LEFT JOIN 
		(SELECT tbl.sMaCBo , tbl.sMaDonVi, tbl.iThang, tbl.iNam, SUM(ISNULL(canbo.fSoNgayHuongBHXH, 0)) fSoNgayTruyLinh, SUM(ISNULL(tbl.nGiaTri, 0)) as nGiaTriTruyLinh
		FROM TL_BangLuong_ThangBHXH tbl
		INNER JOIN #temp2 t2 ON tbl.sMaCBo = t2.sMaCBo AND tbl.sMaDonVi = t2.sMaDonVi AND t2.iThang = tbl.iThang AND tbl.iNam = t2.iNam and tbl.iID_Parent=t2.iID_Parent
		LEFT JOIN TL_CanBo_CheDoBHXH canbo ON tbl.sMaCBo = canbo.sMaCanBo and tbl.sMaCheDo=canbo.sMaCheDo
		WHERE tbl.sMaCheDo IN (SELECT sMaCheDo FROM TL_DM_CheDoBHXH where sLoaiTruyLinh IN (select sMaCheDo from #temp2 ))
		GROUP BY tbl.sMaCBo , tbl.sMaDonVi, tbl.iThang, tbl.iNam
		) as t2 ON  tbl.sMaCBo = t2.sMaCBo AND tbl.sMaDonVi = t2.sMaDonVi AND t2.iThang = tbl.iThang AND tbl.iNam = t2.iNam 
	
		
	

	SELECT 
		@sXauNoiMa as SXauNoiMa,
		sMaHieuCanBo as SMaHieuCanBo,
		sTenCbo as STenCanBo,
		sMaCBo As SMaCanBo,
		sMaCB as SMaCapBac,
		capbac.Note aS STenCapBac,
		bangluongchitiet.sMaDonVi as ID_MaPhanHo,
		dv.Ten_DonVi as STenPhanHo,
		canbo.fSoNgayHuongBHXH as ISoNgayHuong,
		canbo.sSoQuyetDinh as SSoQuyetDinh,
		canbo.dNgayQuyetDinh as DNgayQuyetDinh,
		bangluongchitiet.nGiaTri as FSoTien,
		bangluongchitiet.nGiaTriTruyLinh as FTienTruyLinh,
		bangluongchitiet.fSoNgayTruyLinh as ISoNgayTruyLinh,
		canbo.dTuNgay AS DTuNgay,
		canbo.dDenNgay AS DDenNgay,
		bangluongchitiet.sMaCB
	FROM #temp3 bangluongchitiet 
	LEFT JOIN TL_CanBo_CheDoBHXH canbo ON bangluongchitiet.sMaCBo = canbo.sMaCanBo and bangluongchitiet.sMaCheDo=canbo.sMaCheDo
	LEFT JOIN TL_DM_CapBac capbac ON capbac.Ma_Cb  = bangluongchitiet.sMaCB
	LEFT JOIN TL_DM_DonVi dv On dv.Ma_DonVi = bangluongchitiet.sMaDonVi
	where  (
			bangluongchitiet.sMaCB LIKE '1%'  
			OR bangluongchitiet.sMaCB LIKE '2%'  
			OR bangluongchitiet.sMaCB = '3.1'  
			OR bangluongchitiet.sMaCB = '3.2'  
			OR bangluongchitiet.sMaCB = '3.3'
			OR bangluongchitiet.sMaCB = '413' 
			OR bangluongchitiet.sMaCB = '415' 
			OR bangluongchitiet.sMaCB = '423' 
			OR bangluongchitiet.sMaCB = '425' 
			OR bangluongchitiet.sMaCB = '43' 
			OR bangluongchitiet.sMaCB LIKE '0%'
		)
		and canbo.iNam=@NamLamViec
DROP TABLE #temp3,#temp2,#temp1,#tempTL_DM_CheDoBHXH;
	--	SELECT 
	--	N'9010001-010-011-0001-0001-0001-01-02' as SXauNoiMa,
	--	sMaHieuCanBo as SMaHieuCanBo,
	--	sTenCbo as STenCanBo,
	--	sMaCBo As SMaCanBo,
	--	sMaCB as SMaCapBac,
	--	capbac.Note aS STenCapBac,
	--	bangluong.sMaDonVi as ID_MaPhanHo,
	--	dv.Ten_DonVi as STenPhanHo,
	--	canbo.fSoNgayHuongBHXH as ISoNgayHuong,
	--	canbo.sSoQuyetDinh as SSoQuyetDinh,
	--	canbo.dNgayQuyetDinh as DNgayQuyetDinh,
	--	bangluong.nGiaTri as FSoTien,
	--	canbo.dTuNgay AS DTuNgay,
	--	canbo.dDenNgay AS DDenNgay

	--FROM TL_BangLuong_ThangBHXH bangluong
	--LEFT JOIN TL_CanBo_CheDoBHXH canbo ON bangluong.sMaCBo = canbo.sMaCanBo
	--LEFT JOIN TL_DM_CapBac capbac ON capbac.Ma_Cb  = bangluong.sMaCB
	--LEFT JOIN TL_DM_DonVi dv On dv.Ma_DonVi = bangluong.sMaDonVi
	--WHERE 
	--	bangluong.iThang IN (SELECT * FROM splitstring('1,2,3'))
	--	AND bangluong.sMaCheDo IN (SELECT sMaCheDo FROM TL_DM_CheDoBHXH WHERE bDisplay = 1 AND sXauNoiMaMlnsBHXH = N'9010001-010-011-0001-0001-0001-01-02' )
	--	AND (
	--		bangluong.sMaCB LIKE '1%'  
	--		OR bangluong.sMaCB LIKE '2%'  
	--		OR bangluong.sMaCB LIKE '3.1%'  
	--		OR bangluong.sMaCB LIKE '3.2%'  
	--		OR bangluong.sMaCB LIKE '3.3%'  
	--		OR bangluong.sMaCB LIKE '43%' 
	--		OR bangluong.sMaCB LIKE '0%' 
	--	)

	-- Get data 
	--SELECT 
	--	@sXauNoiMa as SXauNoiMa,
	--	sMaHieuCanBo as SMaHieuCanBo,
	--	sTenCbo as STenCanBo,
	--	sMaCBo As SMaCanBo,
	--	sMaCB as SMaCapBac,
	--	capbac.Note aS STenCapBac,
	--	bangluongchitiet.sMaDonVi as ID_MaPhanHo,
	--	dv.sTenDonVi as STenPhanHo,
	--	canbo.fSoNgayHuongBHXH as ISoNgayHuong,
	--	canbo.sSoQuyetDinh as SSoQuyetDinh,
	--	canbo.dNgayQuyetDinh as DNgayQuyetDinh,
	--	bangluongchitiet.nGiaTri as FSoTien,
	--	canbo.dTuNgay AS DTuNgay,
	--	canbo.dDenNgay AS DDenNgay

	--FROM TL_BangLuong_ThangBHXH bangluongchitiet 
	--LEFT JOIN TL_CanBo_CheDoBHXH canbo ON bangluongchitiet.sMaCBo = canbo.sMaCanBo
	--LEFT JOIN TL_DM_CapBac capbac ON capbac.Ma_Cb  = bangluongchitiet.sMaCB
	--LEFT JOIN DonVi dv On dv.iID_MaDonVi = bangluongchitiet.sMaDonVi
	--JOIN  TL_DS_CapNhap_BangLuong bangluong on bangluongchitiet.iID_Parent = bangluong.Id
	--WHERE 
	--	bangluongchitiet.iThang IN (SELECT * FROM splitstring(@sQuarter))
	--	AND bangluongchitiet.sMaCheDo IN (SELECT sMaCheDo FROM TL_DM_CheDoBHXH WHERE bDisplay = 1 AND sXauNoiMaMlnsBHXH = @sXauNoiMa )
	--	AND (
	--		bangluongchitiet.sMaCB LIKE '1%'  
	--		OR bangluongchitiet.sMaCB LIKE '2%'  
	--		OR bangluongchitiet.sMaCB LIKE '3.1%'  
	--		OR bangluongchitiet.sMaCB LIKE '3.2%'  
	--		OR bangluongchitiet.sMaCB LIKE '3.3%'  
	--		OR bangluongchitiet.sMaCB LIKE '43%' 
	--		OR bangluongchitiet.sMaCB LIKE '0%' 
	--	)
	--	and bangluong.Ma_CachTL = N'CACH2'
	--	and bangluong.Ma_CBo=@Donvi
END
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqBHXH_create_data_summary]    Script Date: 8/22/2024 5:26:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtcqBHXH_create_data_summary]
@IdChungTu nvarchar(MAX),
@NguoiTao nvarchar(500),
@YearOfWork int,
@IdChungTuSummary nvarchar(MAX),
@MaDonVi nvarchar(MAX)
AS
BEGIN
SET NOCOUNT ON;

		DECLARE @Quy INT;
		SELECT @Quy =(SELECT top(1)  iQuyChungTu
		FROM BH_QTC_Quy_CheDoBHXH 
		WHERE  ID_QTC_Quy_CheDoBHXH IN
		(SELECT *
		FROM f_split(@IdChungTu)))

		SELECT 
			(SUM(isnull(fTienCNVCQP_DeNghi, 0)) +
			SUM (isnull(fTienHSQBS_DeNghi, 0)) +
			SUM (isnull(fTienLDHD_DeNghi, 0)) +
			SUM (isnull(fTienQNCN_DeNghi, 0)) +
			SUM (isnull(fTienSQ_DeNghi, 0)) )fTienLuyKeCuoiQuyNay,
			(SUM (isnull(iSoCNVCQP_DeNghi, 0)) +
			SUM (isnull(iSoHSQBS_DeNghi, 0)) +
			SUM (isnull(iSoLDHD_DeNghi, 0)) +
			SUM (isnull(iSoQNCN_DeNghi, 0)) +
			SUM (isnull(iSoSQ_DeNghi, 0))  )iSoLuyKeCuoiQuyNay,
			ct.iID_MaDonVi,
			ct.iNamChungTu,
			ctct.iID_MucLucNganSach,
			sXauNoiMa
			into #temp2
		FROM
			(
				SELECT fTienCNVCQP_DeNghi, fTienHSQBS_DeNghi, fTienLDHD_DeNghi, fTienQNCN_DeNghi, fTienSQ_DeNghi, iSoCNVCQP_DeNghi, fTongTien_PheDuyet, iSoHSQBS_DeNghi,
					iSoLDHD_DeNghi,  iSoQNCN_DeNghi, iSoSQ_DeNghi, iID_QTC_Quy_CheDoBHXH, iID_MucLucNganSach,dmns.sXauNoiMa
				FROM BH_QTC_Quy_CheDoBHXH_ChiTiet ct
				INNER JOIN BH_DM_MucLucNganSach dmns on ct.sXauNoiMa = dmns.sXauNoiMa
				where dmns.bHangCha = 0 and dmns.iNamLamViec = @YearOfWork  and dmns.iTrangThai=1) ctct
				INNER JOIN BH_QTC_Quy_CheDoBHXH ct ON ctct.iID_QTC_Quy_CheDoBHXH = ct.ID_QTC_Quy_CheDoBHXH 
					WHERE
			ct.iQuyChungTu < @Quy 
			and ct.iNamChungTu=@YearOfWork
			and ct.iID_MaDonVi=@MaDonVi

		GROUP BY
			ct.iID_MaDonVi,
			ct.iNamChungTu,
			ctct.iID_MucLucNganSach,
			ctct.sXauNoiMa

		select ml.iID_MLNS
		,ml.sMoTa
		,ml.sXauNoiMa
		,T2.fTienLuyKeCuoiQuyNay
		,T2.iSoLuyKeCuoiQuyNay
		into #tblmlns
		from BH_DM_MucLucNganSach ml
		left join #temp2 T2 on ml.sXauNoiMa=t2.sXauNoiMa
		where ml.iTrangThai=1
		and(ml.sLNS='9010001' or ml.sLNS='9010002')
		and iNamLamViec=2024
		and bHangCha=0

		SELECT 
		   iID_MucLucNganSach,
		   sLoaiTroCap,
		   Sum(iSoLuyKeCuoiQuyNay) iSoLuyKeCuoiQuyNay,
		   SUM(fTienLuyKeCuoiQuyNay) fTienLuyKeCuoiQuyNay,
		   SUM(iSoSQ_DeNghi) iSoSQ_DeNghi,
		   SUM(fTienSQ_DeNghi) fTienSQ_DeNghi,
		   SUM(iSoQNCN_DeNghi) iSoQNCN_DeNghi,
		   SUM(fTienQNCN_DeNghi) fTienQNCN_DeNghi,
		   SUM(iSoCNVCQP_DeNghi) iSoCNVCQP_DeNghi,
		   SUM(fTienCNVCQP_DeNghi) fTienCNVCQP_DeNghi,
		   SUM(iSoHSQBS_DeNghi) iSoHSQBS_DeNghi,
		   SUM(fTienHSQBS_DeNghi) fTienHSQBS_DeNghi,
		   SUM(iSoLDHD_DeNghi) iSoLDHD_DeNghi,
		   SUM(fTienLDHD_DeNghi) fTienLDHD_DeNghi,
		   SUM(iTongSo_DeNghi) iTongSo_DeNghi,
		   SUM(fTongTien_DeNghi) fTongTien_DeNghi,
		   SUM(fTongTien_PheDuyet) fTongTien_PheDuyet,
		   sXauNoiMa
		   into #temp1
	FROM BH_QTC_Quy_CheDoBHXH_ChiTiet
	WHERE  iID_QTC_Quy_CheDoBHXH IN
		(SELECT *
		 FROM f_split(@IdChungTu))
	group by iID_MucLucNganSach, sLoaiTroCap,sXauNoiMa

	select 
			isnull(T2.fTienLuyKeCuoiQuyNay,0) 
			+isnull(T1.fTienCNVCQP_DeNghi,0)
			+isnull(T1.fTienHSQBS_DeNghi,0)
			+isnull(T1.fTienLDHD_DeNghi,0)
			+isnull(T1.fTienQNCN_DeNghi,0)
			+isnull(T1.fTienSQ_DeNghi,0) fTienLuyKeCuoiQuyNay
			,isnull(t2.iSoLuyKeCuoiQuyNay,0)
			+ isnull(T1.iSoCNVCQP_DeNghi,0)
			+ isnull(T1.iSoHSQBS_DeNghi,0)
			+ isnull(T1.iSoLDHD_DeNghi,0)
			+ isnull(T1.iSoQNCN_DeNghi,0)
			+ isnull(T1.iSoSQ_DeNghi,0) iSoLuyKeCuoiQuyNay
			,T1.fTienCNVCQP_DeNghi
			, T1.fTienHSQBS_DeNghi
			, T1.fTienLDHD_DeNghi
			, T1.fTienQNCN_DeNghi
			, T1.fTienSQ_DeNghi
			,T1.iSoCNVCQP_DeNghi
			,T1.iSoHSQBS_DeNghi
			,T1.iSoLDHD_DeNghi
			,T1.iSoQNCN_DeNghi
			,T1.iSoSQ_DeNghi
			,t1.fTongTien_DeNghi
			,t1.iTongSo_DeNghi
			,t1.fTongTien_PheDuyet
			,T2.sXauNoiMa
			,t2.iID_MLNS iID_MucLucNganSach
			,t2.sMoTa sLoaiTroCap
			into #tempDuLieu
			FROM 
			#tblmlns T2
			left JOIN  #temp1 T1 ON T2.sXauNoiMa=T1.sXauNoiMa AND T2.iID_MLNS=T1.iID_MucLucNganSach
			where (T1.fTienCNVCQP_DeNghi<>0 or T1.fTienHSQBS_DeNghi<>0 or T1.fTienLDHD_DeNghi<>0 or T1.fTienQNCN_DeNghi<>0 or T1.fTienSQ_DeNghi<> 0
			or T1.iSoCNVCQP_DeNghi<>0 or  T1.iSoHSQBS_DeNghi<>0 or T1.iSoQNCN_DeNghi<> 0 or  T1.iSoLDHD_DeNghi<>0 or T1.iSoSQ_DeNghi<>0
			or T2.fTienLuyKeCuoiQuyNay<>0 or t2.iSoLuyKeCuoiQuyNay<>0 or t1.fTongTien_DeNghi<>0 or t1.iTongSo_DeNghi<> 0 or t1.fTongTien_PheDuyet<>0)


INSERT INTO BH_QTC_Quy_CheDoBHXH_ChiTiet(
		ID_QTC_Quy_CheDoBHXH_ChiTiet,
		iID_QTC_Quy_CheDoBHXH,
		iID_MucLucNganSach,
		sLoaiTroCap,
		dNgaySua,
		dNgayTao,
		sNguoiSua,
		sNguoiTao,
		fTienDuToanDuyet,
		iSoLuyKeCuoiQuyNay,
		fTienLuyKeCuoiQuyNay,
		iSoSQ_DeNghi,
		fTienSQ_DeNghi,
		iSoQNCN_DeNghi,
		fTienQNCN_DeNghi,
		iSoCNVCQP_DeNghi,
		fTienCNVCQP_DeNghi,
		iSoHSQBS_DeNghi,
		fTienHSQBS_DeNghi,
		iSoLDHD_DeNghi,
		fTienLDHD_DeNghi,
		iTongSo_DeNghi,
		fTongTien_DeNghi,
		fTongTien_PheDuyet,
		iNamLamViec,
		sXauNoiMa,
		iIDMaDonVi
	)
SELECT 
	   NEWID(),
	   @IdChungTuSummary,
       iID_MucLucNganSach,
       sLoaiTroCap,
	   GETDATE(),
	   GETDATE(),
	   @NguoiTao,
	   '',
	   null,
	   SUM(iSoLuyKeCuoiQuyNay),
	   SUM(fTienLuyKeCuoiQuyNay),
	   SUM(iSoSQ_DeNghi),
	   SUM(fTienSQ_DeNghi),
	   SUM(iSoQNCN_DeNghi),
	   SUM(fTienQNCN_DeNghi),
	   SUM(iSoCNVCQP_DeNghi),
	   SUM(fTienCNVCQP_DeNghi),
	   SUM(iSoHSQBS_DeNghi),
	   SUM(fTienHSQBS_DeNghi),
	   SUM(iSoLDHD_DeNghi),
	   SUM(fTienLDHD_DeNghi),
	   SUM(iTongSo_DeNghi),
	   SUM(fTongTien_DeNghi),
	   SUM(fTongTien_PheDuyet),
	   @YearOfWork,
	   sXauNoiMa,
	   @MaDonVi
FROM #tempDuLieu
group by iID_MucLucNganSach,sLoaiTroCap,sXauNoiMa

INSERT INTO [dbo].[BH_QTC_Quy_CTCT_GiaiThichTroCap]
           ([iiD_QTC_Quy_CTCT_GiaiThichTroCap]
           ,[dNgayQuyetDinh]
           ,[dNgaySua]
           ,[dNgayTao]
           ,[fSoTien]
           ,[iiD_MaDonVi]
           ,[iiD_MaPhanHo]
           ,[iID_QTC_Quy_ChungTu]
           ,[iNamLamViec]
           ,[iQuy]
           ,[iSoNgayHuong]
           ,[sMa_Hieu_Can_Bo]
           ,[sNguoiSua]
           ,[sNguoiTao]
           ,[sSoQuyetDinh]
           ,[sTenCanBo]
           ,[sTenCapBac]
           ,[sTenPhanHo]
           ,[sXauNoiMa]
           ,[dDenNgay]
           ,[dTuNgay]
           ,[sSoSoBHXH]
           ,[fTienLuongThangDongBHXH]
           ,[sMaCapBac]
           ,[fTienTruyLinh]
           ,[iSoNgayTruyLinh])
	 SELECT 
	   NEWID(),
	   dNgayQuyetDinh,
       null,
	   GETDATE(),
       fSoTien,
	   @MaDonVi,
	   iiD_MaPhanHo,
	   @IdChungTuSummary,
	   @YearOfWork,
	   iQuy,
	   iSoNgayHuong,
	   sMa_Hieu_Can_Bo,
	   '',
	   @NguoiTao,
	   sSoQuyetDinh,
	   sTenCanBo,
	   sTenCapBac,
	   sTenPhanHo,
	   sXauNoiMa,
	   dDenNgay,
	   dTuNgay,
	   sSoSoBHXH,
	   fTienLuongThangDongBHXH,
	   sMaCapBac,
	   fTienTruyLinh,
	   iSoNgayTruyLinh

FROM BH_QTC_Quy_CTCT_GiaiThichTroCap
WHERE  iID_QTC_Quy_ChungTu IN
    (SELECT *
     FROM f_split(@IdChungTu))

UPDATE BH_QTC_Quy_CheDoBHXH SET iLoaiTongHop=2 , bDaTongHop=1  WHERE ID_QTC_Quy_CheDoBHXH IN (SELECT * FROM f_split(@IdChungTu));
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
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqBHXH_create_datafor_quaterbefore]    Script Date: 8/22/2024 5:26:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtcqBHXH_create_datafor_quaterbefore]
@IdChungTu nvarchar(MAX),
@Username nvarchar(500),
@NamLamViec int,
@Quy int,
@MaDonVi nvarchar(MAX)
AS
BEGIN
SET NOCOUNT ON;

		SELECT 
			(SUM(isnull(fTienCNVCQP_DeNghi, 0)) +
			SUM (isnull(fTienHSQBS_DeNghi, 0)) +
			SUM (isnull(fTienLDHD_DeNghi, 0)) +
			SUM (isnull(fTienQNCN_DeNghi, 0)) +
			SUM (isnull(fTienSQ_DeNghi, 0)) )fTienLuyKeCuoiQuyNay,
			(SUM (isnull(iSoCNVCQP_DeNghi, 0)) +
			SUM (isnull(iSoHSQBS_DeNghi, 0)) +
			SUM (isnull(iSoLDHD_DeNghi, 0)) +
			SUM (isnull(iSoQNCN_DeNghi, 0)) +
			SUM (isnull(iSoSQ_DeNghi, 0))  )iSoLuyKeCuoiQuyNay,
			ct.iID_MaDonVi,
			ct.iNamChungTu,
			ctct.iID_MucLucNganSach,
			ctct.sXauNoiMa,
			ctct.sMoTa sLoaiTroCap
			into #temp2
		FROM
			(
				SELECT fTienCNVCQP_DeNghi, fTienHSQBS_DeNghi, fTienLDHD_DeNghi, fTienQNCN_DeNghi, fTienSQ_DeNghi, iSoCNVCQP_DeNghi, fTongTien_PheDuyet, iSoHSQBS_DeNghi,
					iSoLDHD_DeNghi,  iSoQNCN_DeNghi, iSoSQ_DeNghi, iID_QTC_Quy_CheDoBHXH, iID_MucLucNganSach,dmns.sXauNoiMa ,dmns.sMoTa
				FROM BH_QTC_Quy_CheDoBHXH_ChiTiet ct
				INNER JOIN BH_DM_MucLucNganSach dmns on ct.sXauNoiMa = dmns.sXauNoiMa
				where dmns.bHangCha = 0 and dmns.iNamLamViec = @NamLamViec) ctct
				INNER JOIN BH_QTC_Quy_CheDoBHXH ct ON ctct.iID_QTC_Quy_CheDoBHXH = ct.ID_QTC_Quy_CheDoBHXH 
					WHERE
			ct.iQuyChungTu < @Quy 
			and ct.iNamChungTu=@NamLamViec
			and ct.iID_MaDonVi=@MaDonVi

		GROUP BY
			ct.iID_MaDonVi,
			ct.iNamChungTu,
			ctct.iID_MucLucNganSach,
			ctct.sXauNoiMa,
			ctct.sMoTa

	INSERT INTO BH_QTC_Quy_CheDoBHXH_ChiTiet
	( 
		ID_QTC_Quy_CheDoBHXH_ChiTiet,
		iID_QTC_Quy_CheDoBHXH,
		iID_MucLucNganSach,
		iNamLamViec,
		sLoaiTroCap,
		dNgayTao,
		sNguoiTao,
		iSoLuyKeCuoiQuyNay,
		fTienLuyKeCuoiQuyNay,
		sXauNoiMa,
		iIDMaDonVi
	
	)
	SELECT 
		NEWID(),
		@IdChungTu,
		qtcn_ct.iID_MucLucNganSach,
		@NamLamViec,
		qtcn_ct.sLoaiTroCap,
		GETDATE(),
		@Username,
		qtcn_ct.iSoLuyKeCuoiQuyNay,
		qtcn_ct.fTienLuyKeCuoiQuyNay,
		qtcn_ct.sXauNoiMa,
		@MaDonVi
	FROM #temp2 qtcn_ct

END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqKPQL_create_datafor_quaterbefore]    Script Date: 8/22/2024 5:26:33 PM ******/
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


	SELECT 
		qtcn_ct.iID_MucLucNganSach,
		qtcn_ct.sNoiDung,
		SUM(qtcn_ct.fTienXacNhanQuyetToanQuyNay) fTienThucChi,
		SUM(qtcn_ct.fTienXacNhanQuyetToanQuyNay) fTienXacNhanQuyetToanQuyNay,
		qtcn_ct.sXauNoiMa,
		qtcn_ct.iNamLamViec,
		qtcn_ct.iIDMaDonVi,
		ml.sM,
		ml.sTM
		into #temp1
	FROM BH_QTC_Quy_KinhPhiQuanLy_ChiTiet qtcn_ct
		inner join BH_QTC_Quy_KinhPhiQuanLy  as qtcn on qtcn_ct.iID_QTC_Quy_KinhPhiQuanLy = qtcn.ID_QTC_Quy_KinhPhiQuanLy
		left join BH_DM_MucLucNganSach ml on qtcn_ct.sXauNoiMa=ml.sXauNoiMa
	WHERE qtcn.iQuyChungTu < @Quy
			AND qtcn.iID_MaDonVi = @MaDonVi
			AND qtcn.iNamChungTu = @NamLamViec
			and ml.iTrangThai=1
			and ml.iNamLamViec=@NamLamViec
			--AND qtcn.bIsKhoa=1
		GROUP BY qtcn_ct.iID_MucLucNganSach, qtcn_ct.sNoiDung,qtcn_ct.sXauNoiMa,
		qtcn_ct.iNamLamViec,
		qtcn_ct.iIDMaDonVi,ml.sM,
		ml.sTM

	INSERT INTO BH_QTC_Quy_KinhPhiQuanLy_ChiTiet
	( 
		ID_QTC_Quy_KinhPhiQuanLy_ChiTiet,
		iID_QTC_Quy_KinhPhiQuanLy,
		iID_MucLucNganSach,
		sNoiDung,
		dNgayTao,
		sNguoiTao,
		fTienThucChi,
		fTienQuyetToanDaDuyet,
		sXauNoiMa,
		iNamLamViec,
		iIDMaDonVi,
		sM,
		sTM
	)
	SELECT 
		NEWID(),
		@IdChungTu,
		iID_MucLucNganSach,
		sNoiDung,
		GETDATE(),
		@Username,
		fTienThucChi,
		fTienXacNhanQuyetToanQuyNay,
		sXauNoiMa,
		iNamLamViec,
		iIDMaDonVi,
		sM,
		sTM

	FROM #temp1 
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_rpt_qttm_bhyt_than_nhan_nld_tong_hop]    Script Date: 8/22/2024 5:26:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qttm_rpt_qttm_bhyt_than_nhan_nld_tong_hop]
	@NamLamViec int,
	@IdDonVi nvarchar(50),
	@Donvitinh int,
	@IsTongHop bit,
	@IQuy int
AS
BEGIN

	select 
			ctct.iID_MLNS,
			sum(ctct.fDuToan) fDuToan,
			ctct.sXauNoiMa
			into #tempDuToan
		from
			BH_DTTM_BHYT_ThanNhan_ChiTiet ctct
			join BH_DTTM_BHYT_ThanNhan ct on ctct.iID_DTTM_BHYT_ThanNhan = ct.iID_DTTM_BHYT_ThanNhan
		where ct.iNamLamViec = @NamLamViec
			and ct.iID_MaDonVi = @IdDonVi
			group by
			ctct.iID_MLNS,ctct.sXauNoiMa

	select
		chungtudonvi.iID_QTTM_BHYT_ChungTu_ChiTiet,
		mlns.iID_MLNS,
		mlns.sMoTa,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		mlns.sLNS,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		mlns.sTTM,
		mlns.sNG,
		mlns.sTNG,
		mlns.sXauNoiMa,
		mlns.sMoTa,
		@NamLamViec iNamLamViec,
		dutoan.fDuToan/@Donvitinh fDuToan,
		chungtudonvi.fDaQuyetToan/@Donvitinh fDaQuyetToan,
		(isnull(dutoan.fDuToan, 0) - isnull(chungtudonvi.fDaQuyetToan, 0) - isnull(chungtudonvi.fSoPhaiThu, 0))/@Donvitinh fConLai,
		chungtudonvi.fSoPhaiThu/@Donvitinh fSoPhaiThu
		from
		(select
			BH_DM_MucLucNganSach.iID_MLNS,
			BH_DM_MucLucNganSach.iID_MLNS_Cha,
			BH_DM_MucLucNganSach.bHangCha,
			BH_DM_MucLucNganSach.sLNS,
			BH_DM_MucLucNganSach.sL,
			BH_DM_MucLucNganSach.sK,
			BH_DM_MucLucNganSach.sM,
			BH_DM_MucLucNganSach.sTM,
			BH_DM_MucLucNganSach.sTTM,
			BH_DM_MucLucNganSach.sNG,
			BH_DM_MucLucNganSach.sTNG,
			BH_DM_MucLucNganSach.sXauNoiMa,
			BH_DM_MucLucNganSach.sMoTa
		from BH_DM_MucLucNganSach 
		where sLNS like '903%'
		and iNamLamViec = @NamLamViec) mlns
		left join
		(select
			ct.iID_MaDonVi,
			ct.iNamLamViec,
			ctct.iID_QTTM_BHYT_ChungTu_ChiTiet,
			ctct.fConLai,
			ctct.fDaQuyetToan,
			ctct.fDuToan,
			ctct.fSoPhaiThu,
			ctct.iID_MLNS,
			ctct.iID_MLNS_Cha,
			ctct.sGhiChu,
			ctct.sLNS,
			ctct.sXauNoiMa,
			ctct.iID_QTTM_BHYT_ChungTu
			from
			BH_QTTM_BHYT_Chung_Tu ct
			join
			BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct on ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
			where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam = @IQuy
			and ct.iID_MaDonVi = @IdDonVi
			and ct.iLoaiTongHop = 2
			--and ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end
				) chungtudonvi on mlns.iID_MLNS = chungtudonvi.iID_MLNS
		left join
		(select ctct.sXauNoiMa, isnull(sum(ctct.fDuToan), 0) fDuToan 
			from BH_DTTM_BHYT_ThanNhan_ChiTiet ctct
			join BH_DTTM_BHYT_ThanNhan ct on ctct.iID_DTTM_BHYT_ThanNhan = ct.iID_DTTM_BHYT_ThanNhan
			where ct.iNamLamViec = @NamLamViec
			and ct.iID_MaDonVi = @IdDonVi
			and ct.bIsKhoa = 1
			group by ctct.sXauNoiMa) dutoan on mlns.sXauNoiMa = dutoan.sXauNoiMa
			
		order by mlns.sXauNoiMa
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_rpt_quyet_toan_thu_mua_bhyt_than_nhan_nld]    Script Date: 8/22/2024 5:26:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qttm_rpt_quyet_toan_thu_mua_bhyt_than_nhan_nld]
	@NamLamViec int,
	@IdDonVi nvarchar(50),
	@Donvitinh int,
	@IsTongHop bit,
	@IQuy int
AS
BEGIN


	select 
	ctct.sXauNoiMa,
	sum(ctct.fDuToan) fDuToan,
	ctct.iID_MaDonVi
	into #tempDuToan
	from
	BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct
	where ctct.iNamLamViec = @NamLamViec
	and ctct.iID_MaDonVi = @IdDonVi
	group by
	ctct.sXauNoiMa ,ctct.iID_MaDonVi

	select
		chungtudonvi.iID_QTTM_BHYT_ChungTu_ChiTiet,
		mlns.iID_MLNS,
		mlns.sMoTa,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		mlns.sLNS,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		mlns.sTTM,
		mlns.sNG,
		mlns.sTNG,
		mlns.sXauNoiMa,
		mlns.sMoTa,
		@NamLamViec iNamLamViec,
		dtoan.fDuToan/@Donvitinh fDuToan,
		chungtudonvi.fDaQuyetToan/@Donvitinh fDaQuyetToan,
		(isnull(dtoan.fDuToan, 0) - isnull(chungtudonvi.fDaQuyetToan, 0) - isnull(chungtudonvi.fSoPhaiThu, 0))/@Donvitinh fConLai,
		chungtudonvi.fSoPhaiThu/@Donvitinh fSoPhaiThu
		from
		(select
			BH_DM_MucLucNganSach.iID_MLNS,
			BH_DM_MucLucNganSach.iID_MLNS_Cha,
			BH_DM_MucLucNganSach.bHangCha,
			BH_DM_MucLucNganSach.sLNS,
			BH_DM_MucLucNganSach.sL,
			BH_DM_MucLucNganSach.sK,
			BH_DM_MucLucNganSach.sM,
			BH_DM_MucLucNganSach.sTM,
			BH_DM_MucLucNganSach.sTTM,
			BH_DM_MucLucNganSach.sNG,
			BH_DM_MucLucNganSach.sTNG,
			BH_DM_MucLucNganSach.sXauNoiMa,
			BH_DM_MucLucNganSach.sMoTa
		from BH_DM_MucLucNganSach 
		where sLNS like '903%'
		and iNamLamViec = @NamLamViec) mlns
		left join
		(select
			ct.iID_MaDonVi,
			ct.iNamLamViec,
			ctct.iID_QTTM_BHYT_ChungTu_ChiTiet,
			ctct.fConLai,
			ctct.fDaQuyetToan,
			ctct.fDuToan,
			ctct.fSoPhaiThu,
			ctct.iID_MLNS,
			ctct.iID_MLNS_Cha,
			ctct.sGhiChu,
			ctct.sLNS,
			ctct.sXauNoiMa,
			ctct.iID_QTTM_BHYT_ChungTu
			from
			BH_QTTM_BHYT_Chung_Tu ct
			join
			BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct on ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
			where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam = @IQuy
			and ct.iID_MaDonVi = @IdDonVi
			and ct.iLoaiTongHop = 1
			--and ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end
				) chungtudonvi 
			on mlns.iID_MLNS = chungtudonvi.iID_MLNS
				Left join #tempDuToan dtoan on dtoan.sXauNoiMa=mlns.sXauNoiMa
		order by mlns.sXauNoiMa
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_khoi_nhomdt]    Script Date: 8/22/2024 5:26:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_khoi_nhomdt]
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
						FROM BH_QTC_Quy_CheDoBHXH_ChiTiet gt
						Inner JOIN BH_QTC_Quy_CheDoBHXH ct ON gt.iID_QTC_Quy_CheDoBHXH=ct.ID_QTC_Quy_CheDoBHXH
						LEFT JOIN DonVi dv on dv.iID_MaDonVi=ct.iID_MaDonVi
						WHERE (gt.sXauNoiMa like '9010001-010-011-0001%' )
								AND gt.iNamLamViec = @INamLamViec 
								AND dv.iNamLamViec=@INamLamViec
								AND dv.iID_MaDonVi  IN (select * from dbo.splitstring(@IdMaDonVi)) 
								--AND dv.iKhoi=2
								AND ct.iNamChungTu=@INamLamViec
								AND ct.iQuyChungTu = @IQuy 

	---Lấy thông tin chi tiết giai thich tro cap hach toan
		SELECT gt.*,dv.sTenDonVi 
			into #TBL_TroCapOmDauHachToan 
						FROM BH_QTC_Quy_CheDoBHXH_ChiTiet gt
						Inner JOIN BH_QTC_Quy_CheDoBHXH ct ON gt.iID_QTC_Quy_CheDoBHXH=ct.ID_QTC_Quy_CheDoBHXH
						LEFT JOIN DonVi dv on dv.iID_MaDonVi=ct.iID_MaDonVi
						WHERE ( gt.sXauNoiMa like '9010002-010-011-0001%')
								AND gt.iNamLamViec = @INamLamViec  
								AND dv.iNamLamViec=@INamLamViec
								AND dv.iID_MaDonVi  IN (select * from dbo.splitstring(@IdMaDonVi)) 
								AND ct.iNamChungTu=@INamLamViec
								--AND dv.iKhoi=1
								AND ct.iQuyChungTu = @IQuy 

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

	
--- Lay thong tin giai thich theo khoi du toan
		---Du Toan SQ
		SELECT 
				gt.iIDMaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01' then sum(gt.iTongSo_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.iTongSo_DeNghi) else 0 end ) iSoNgayDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01'then sum(gt.fTongTien_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.fTongTien_DeNghi) else 0 end ) fSoTienDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.iTongSo_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.iTongSo_DeNghi) else 0 end ) iSoNgayTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.fTongTien_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.fTongTien_DeNghi) else 0 end ) fSoTienTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.iTongSo_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.iTongSo_DeNghi) else 0 end ) iSoNgayDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.fTongTien_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.fTongTien_DeNghi) else 0 end ) fSoTienDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.iTongSo_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.iTongSo_DeNghi) else 0 end ) iSoNgayTren14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.fTongTien_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.fTongTien_DeNghi) else 0 end ) fSoTienTren14OmKhac,

				
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.iTongSo_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.iTongSo_DeNghi) else 0 end ) iSoNgayConOm,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.fTongTien_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.fTongTien_DeNghi) else 0 end ) fSoTienConOm,


				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.iTongSo_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.iTongSo_DeNghi) else 0 end ) iSoNgayPHSK,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.fTongTien_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.fTongTien_DeNghi) else 0 end ) fSoTienPHSK

				INTO #tempDetalDuToanSum 
			FROM #TBL_TroCapOmDauDuToan gt
			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

				
		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iIDMaDonVi ASC))) AS sTT,
				iIDMaDonVi iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay,
				SUM(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay,
				SUM(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay,
				SUM(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay,
				SUM(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac,
				SUM(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac,
				SUM(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac,
				SUM(fSoTienTren14OmKhac) fSoTienTren14OmKhac,

				SUM(iSoNgayConOm) iSoNgayConOm,
				SUM(fSoTienConOm) fSoTienConOm,
				SUM(iSoNgayPHSK) iSoNgayPHSK,
				SUM(fSoTienPHSK) fSoTienPHSK
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalDuToan 
				FROM #tempDetalDuToanSum
				Group by iIDMaDonVi,
				sTenDonVi

		---Du Toan QNCN
		SELECT 
								gt.iIDMaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01' then sum(gt.iTongSo_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.iTongSo_DeNghi) else 0 end ) iSoNgayDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01'then sum(gt.fTongTien_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.fTongTien_DeNghi) else 0 end ) fSoTienDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.iTongSo_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.iTongSo_DeNghi) else 0 end ) iSoNgayTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.fTongTien_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.fTongTien_DeNghi) else 0 end ) fSoTienTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.iTongSo_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.iTongSo_DeNghi) else 0 end ) iSoNgayDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.fTongTien_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.fTongTien_DeNghi) else 0 end ) fSoTienDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.iTongSo_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.iTongSo_DeNghi) else 0 end ) iSoNgayTren14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.fTongTien_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.fTongTien_DeNghi) else 0 end ) fSoTienTren14OmKhac,

				
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.iTongSo_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.iTongSo_DeNghi) else 0 end ) iSoNgayConOm,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.fTongTien_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.fTongTien_DeNghi) else 0 end ) fSoTienConOm,


				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.iTongSo_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.iTongSo_DeNghi) else 0 end ) iSoNgayPHSK,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.fTongTien_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.fTongTien_DeNghi) else 0 end ) fSoTienPHSK

				INTO #tempDetalHachToanSum 
			FROM #TBL_TroCapOmDauHachToan gt

			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

								
		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iIDMaDonVi ASC))) AS sTT,
				iIDMaDonVi iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay,
				SUM(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay,
				SUM(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay,
				SUM(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay,
				SUM(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac,
				SUM(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac,
				SUM(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac,
				SUM(fSoTienTren14OmKhac) fSoTienTren14OmKhac,

				SUM(iSoNgayConOm) iSoNgayConOm,
				SUM(fSoTienConOm) fSoTienConOm,
				SUM(iSoNgayPHSK) iSoNgayPHSK,
				SUM(fSoTienPHSK) fSoTienPHSK
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalHachToan
				FROM #tempDetalHachToanSum
				Group by iIDMaDonVi,
				sTenDonVi

---------  Tra ve kqua DuToan
			SELECT * INTO #tempkqDuToan FROM
				(	SELECT * FROM  #tempDuToan
					UNION ALL
					SELECT * FROM  #tempDetalDuToan
			) TEMPHachToan


---------  Tra ve kqua HachToan
			SELECT * INTO #tempkqHachToan FROM
				(	SELECT * FROM  #tempHachToan
					UNION ALL
					SELECT * FROM  #tempDetalHachToan
			) TEMPHachToan

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
			FROM #tempkqDuToan A,
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
			, 3 type
			FROM #tempkqDuToan
			WHERE type=1) Detail
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
			FROM #tempkqHachToan A,
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
			, 3 type
			FROM #tempkqHachToan
			WHERE type=1) Detail
			where A.type=Detail.type



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

		DROP TABLE #tempDetalDuToanSum

		drop table #tempHachToan
		DROP TABLE #tempDetalHachToan
		DROP TABLE #tempDetalHachToanSum

		DROP TABLE #tempkqDuToan
		DROP TABLE #tempkqHachToan
		DROP TABLE #tempKQAll

end
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_khoi]    Script Date: 8/22/2024 5:26:33 PM ******/
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
						FROM BH_QTC_Quy_CheDoBHXH_ChiTiet gt
						Inner JOIN BH_QTC_Quy_CheDoBHXH ct ON gt.iID_QTC_Quy_CheDoBHXH=ct.ID_QTC_Quy_CheDoBHXH
						Inner JOIN DonVi dv on dv.iID_MaDonVi=ct.iID_MaDonVi
						WHERE (gt.sXauNoiMa like '9010001-010-011-0002%' )
								AND gt.iNamLamViec = @INamLamViec 
								AND dv.iNamLamViec=@INamLamViec
								AND dv.iID_MaDonVi  IN (select * from dbo.splitstring(@IdMaDonVi)) 
								--AND dv.iKhoi=2
								AND ct.iNamChungTu=@INamLamViec
								AND ct.iQuyChungTu = @IQuy 

	---Lấy thông tin chi tiết giai thich tro cap hach toan
		SELECT gt.*,dv.sTenDonVi 
			into #TBL_TroCapThaiSanHachToan 
						FROM BH_QTC_Quy_CheDoBHXH_ChiTiet gt
						Inner JOIN BH_QTC_Quy_CheDoBHXH ct ON gt.iID_QTC_Quy_CheDoBHXH=ct.ID_QTC_Quy_CheDoBHXH
						Inner JOIN DonVi dv on dv.iID_MaDonVi=ct.iID_MaDonVi
						WHERE ( gt.sXauNoiMa like '9010002-010-011-0002%')
								AND gt.iNamLamViec = @INamLamViec 
								AND dv.iNamLamViec=@INamLamViec
								AND dv.iID_MaDonVi  IN (select * from dbo.splitstring(@IdMaDonVi)) 
								AND ct.iNamChungTu=@INamLamViec
								--AND dv.iKhoi=1
								AND ct.iQuyChungTu = @IQuy 

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
		N'4. HSQ, BS' as sTT
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
		N'5. LĐHĐ' as sTT
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
		N'4. HSQ, BS' as sTT
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
		N'5. LĐHĐ' as sTT
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
				gt.iIDMaDonVi iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%' then sum(gt.iSoSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iSoSQ_DeNghi) else 0 end )/30 iSoNgaySinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%'then sum(gt.fTienSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.fTienSQ_DeNghi) else 0 end ) fSoTienSinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.iSoSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.iSoSQ_DeNghi) else 0 end ) iSoNgaySinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.fTienSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.fTienSQ_DeNghi) else 0 end ) fSoTienSinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.iSoSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.iSoSQ_DeNghi) else 0 end ) iSoNgayKhamThaiKHHGD,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fTienSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fTienSQ_DeNghi) else 0 end ) fSoTienKhamThaiKHHGD,

				
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.iSoSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.iSoSQ_DeNghi) else 0 end ) iSoNgayPHSKThaiSan,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.fTienSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.fTienSQ_DeNghi) else 0 end ) fSoTienPHSKThaiSan
				
				INTO #tempDetalSQDuToanSum
			FROM #TBL_TroCapThaiSanDuToan gt
			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa


		SELECT 
			CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iID_MaDonVi ASC))) AS sTT,
				iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon,
				SUM(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon,
				SUM(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon,
				SUM(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon,
				SUM(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD,
				SUM(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD,
				SUM(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan,
				SUM(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalSQDuToan 
				FROM #tempDetalSQDuToanSum
				Group by iID_MaDonVi,
				sTenDonVi


		---Du Toan QNCN
		SELECT 
				gt.iIDMaDonVi iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%' then sum(gt.iSoQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iSoQNCN_DeNghi) else 0 end )/30 iSoNgaySinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%'then sum(gt.fTienQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.fTienQNCN_DeNghi) else 0 end ) fSoTienSinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.iSoQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.iSoQNCN_DeNghi) else 0 end ) iSoNgaySinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.fTienQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.fTienQNCN_DeNghi) else 0 end ) fSoTienSinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.iSoQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.iSoQNCN_DeNghi) else 0 end ) iSoNgayKhamThaiKHHGD,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fTienQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fTienQNCN_DeNghi) else 0 end ) fSoTienKhamThaiKHHGD,

				
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.iSoQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.iSoQNCN_DeNghi) else 0 end ) iSoNgayPHSKThaiSan,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.fTienQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.fTienQNCN_DeNghi) else 0 end ) fSoTienPHSKThaiSan

				INTO #tempDetalQNCNDuToanSum 
			FROM #TBL_TroCapThaiSanDuToan gt
			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa
		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iID_MaDonVi ASC))) AS sTT,
				iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon,
				SUM(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon,
				SUM(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon,
				SUM(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon,
				SUM(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD,
				SUM(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD,
				SUM(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan,
				SUM(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalQNCNDuToan 
				FROM #tempDetalQNCNDuToanSum
				Group by iID_MaDonVi,
				sTenDonVi


		---Du Toan CNVCQP
		SELECT 

				gt.iIDMaDonVi iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%' then sum(gt.iSoCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iSoCNVCQP_DeNghi) else 0 end )/30 iSoNgaySinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%'then sum(gt.fTienCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.fTienCNVCQP_DeNghi) else 0 end ) fSoTienSinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.iSoCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.iSoCNVCQP_DeNghi) else 0 end ) iSoNgaySinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.fTienCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.fTienCNVCQP_DeNghi) else 0 end ) fSoTienSinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.iSoCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.iSoCNVCQP_DeNghi) else 0 end ) iSoNgayKhamThaiKHHGD,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fTienCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fTienCNVCQP_DeNghi) else 0 end ) fSoTienKhamThaiKHHGD,

				
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.iSoCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.iSoCNVCQP_DeNghi) else 0 end ) iSoNgayPHSKThaiSan,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.fTienCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.fTienCNVCQP_DeNghi) else 0 end ) fSoTienPHSKThaiSan

				INTO #tempDetalCNVCQPDuToanSum 
			FROM #TBL_TroCapThaiSanDuToan gt
			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iID_MaDonVi ASC))) AS sTT,
				iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon,
				SUM(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon,
				SUM(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon,
				SUM(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon,
				SUM(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD,
				SUM(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD,
				SUM(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan,
				SUM(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalCNVCQPDuToan 
				FROM #tempDetalCNVCQPDuToanSum
				Group by iID_MaDonVi,
				sTenDonVi


		-- Du Toan HSQBS
		SELECT 
				gt.iIDMaDonVi iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%' then sum(gt.iSoHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iSoHSQBS_DeNghi) else 0 end )/30 iSoNgaySinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%'then sum(gt.fTienHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.fTienHSQBS_DeNghi) else 0 end ) fSoTienSinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.iSoHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.iSoHSQBS_DeNghi) else 0 end ) iSoNgaySinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.fTienHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.fTienHSQBS_DeNghi) else 0 end ) fSoTienSinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.iSoHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.iSoHSQBS_DeNghi) else 0 end ) iSoNgayKhamThaiKHHGD,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fTienHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fTienHSQBS_DeNghi) else 0 end ) fSoTienKhamThaiKHHGD,

				
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.iSoHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.iSoHSQBS_DeNghi) else 0 end ) iSoNgayPHSKThaiSan,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.fTienHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.fTienHSQBS_DeNghi) else 0 end ) fSoTienPHSKThaiSan

				INTO #tempDetalHSQBSDuToanSum 
			FROM #TBL_TroCapThaiSanDuToan gt
			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iID_MaDonVi ASC))) AS sTT,
				iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon,
				SUM(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon,
				SUM(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon,
				SUM(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon,
				SUM(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD,
				SUM(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD,
				SUM(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan,
				SUM(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalHSQBSDuToan 
				FROM #tempDetalHSQBSDuToanSum
				Group by iID_MaDonVi,
				sTenDonVi

		-- Du Toan LDHD
		SELECT 
				gt.iIDMaDonVi iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%' then sum(gt.iSoLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iSoLDHD_DeNghi) else 0 end )/30 iSoNgaySinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%'then sum(gt.fTienLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.fTienLDHD_DeNghi) else 0 end ) fSoTienSinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.iSoLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.iSoLDHD_DeNghi) else 0 end ) iSoNgaySinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.fTienLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.fTienLDHD_DeNghi) else 0 end ) fSoTienSinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.iSoLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.iSoLDHD_DeNghi) else 0 end ) iSoNgayKhamThaiKHHGD,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fTienLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fTienLDHD_DeNghi) else 0 end ) fSoTienKhamThaiKHHGD,

				
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.iSoLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.iSoLDHD_DeNghi) else 0 end ) iSoNgayPHSKThaiSan,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.fTienLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.fTienLDHD_DeNghi) else 0 end ) fSoTienPHSKThaiSan

				INTO #tempDetalLDHDDuToanSum 
			FROM #TBL_TroCapThaiSanDuToan gt
			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa
		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iID_MaDonVi ASC))) AS sTT,
				iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon,
				SUM(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon,
				SUM(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon,
				SUM(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon,
				SUM(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD,
				SUM(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD,
				SUM(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan,
				SUM(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalLDHDDuToan 
				FROM #tempDetalLDHDDuToanSum
				Group by iID_MaDonVi,
				sTenDonVi

		--- Lay thong tin giai thich theo khoi Hach Toan
		---Hach Toan SQ
		SELECT 
				gt.iIDMaDonVi iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%' then sum(gt.iSoSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iSoSQ_DeNghi) else 0 end )/30 iSoNgaySinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%'then sum(gt.fTienSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.fTienSQ_DeNghi) else 0 end ) fSoTienSinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.iSoSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.iSoSQ_DeNghi) else 0 end ) iSoNgaySinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.fTienSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.fTienSQ_DeNghi) else 0 end ) fSoTienSinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.iSoSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.iSoSQ_DeNghi) else 0 end ) iSoNgayKhamThaiKHHGD,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fTienSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fTienSQ_DeNghi) else 0 end ) fSoTienKhamThaiKHHGD,

				
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.iSoSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.iSoSQ_DeNghi) else 0 end ) iSoNgayPHSKThaiSan,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.fTienSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.fTienSQ_DeNghi) else 0 end ) fSoTienPHSKThaiSan

				INTO #tempDetalSQHachToanSum 
			FROM #TBL_TroCapThaiSanHachToan gt
			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iID_MaDonVi ASC))) AS sTT,
				iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon,
				SUM(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon,
				SUM(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon,
				SUM(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon,
				SUM(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD,
				SUM(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD,
				SUM(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan,
				SUM(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalSQHachToan 
				FROM #tempDetalSQHachToanSum
				Group by iID_MaDonVi,
				sTenDonVi
		---Hach Toan QNCN
		SELECT 
				gt.iIDMaDonVi iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%' then sum(gt.iSoQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iSoQNCN_DeNghi) else 0 end )/30 iSoNgaySinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%'then sum(gt.fTienQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.fTienQNCN_DeNghi) else 0 end ) fSoTienSinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.iSoQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.iSoQNCN_DeNghi) else 0 end ) iSoNgaySinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.fTienQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.fTienQNCN_DeNghi) else 0 end ) fSoTienSinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.iSoQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.iSoQNCN_DeNghi) else 0 end ) iSoNgayKhamThaiKHHGD,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fTienQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fTienQNCN_DeNghi) else 0 end ) fSoTienKhamThaiKHHGD,

				
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.iSoQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.iSoQNCN_DeNghi) else 0 end ) iSoNgayPHSKThaiSan,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.fTienQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.fTienQNCN_DeNghi) else 0 end ) fSoTienPHSKThaiSan

				INTO #tempDetalQNCNHachToanSum 
			FROM #TBL_TroCapThaiSanHachToan gt
			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iID_MaDonVi ASC))) AS sTT,
				iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon,
				SUM(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon,
				SUM(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon,
				SUM(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon,
				SUM(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD,
				SUM(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD,
				SUM(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan,
				SUM(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalQNCNHachToan 
				FROM #tempDetalQNCNHachToanSum
				Group by iID_MaDonVi,
				sTenDonVi

		---Hach Toan CNVCQP
		SELECT 
				gt.iIDMaDonVi iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%' then sum(gt.iSoCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iSoCNVCQP_DeNghi) else 0 end )/30 iSoNgaySinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%'then sum(gt.fTienCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.fTienCNVCQP_DeNghi) else 0 end ) fSoTienSinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.iSoCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.iSoCNVCQP_DeNghi) else 0 end ) iSoNgaySinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.fTienCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.fTienCNVCQP_DeNghi) else 0 end ) fSoTienSinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.iSoCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.iSoCNVCQP_DeNghi) else 0 end ) iSoNgayKhamThaiKHHGD,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fTienCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fTienCNVCQP_DeNghi) else 0 end ) fSoTienKhamThaiKHHGD,

				
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.iSoCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.iSoCNVCQP_DeNghi) else 0 end ) iSoNgayPHSKThaiSan,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.fTienCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.fTienCNVCQP_DeNghi) else 0 end ) fSoTienPHSKThaiSan

				INTO #tempDetalCNVCQPHachToanSum
			FROM #TBL_TroCapThaiSanHachToan gt
			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iID_MaDonVi ASC))) AS sTT,
				iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon,
				SUM(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon,
				SUM(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon,
				SUM(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon,
				SUM(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD,
				SUM(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD,
				SUM(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan,
				SUM(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalCNVCQPHachToan 
				FROM #tempDetalCNVCQPHachToanSum
				Group by iID_MaDonVi,
				sTenDonVi

		-- Hach Toan HSQBS
		SELECT 
				gt.iIDMaDonVi iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%' then sum(gt.iSoHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iSoHSQBS_DeNghi) else 0 end )/30 iSoNgaySinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%'then sum(gt.fTienHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.fTienHSQBS_DeNghi) else 0 end ) fSoTienSinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.iSoHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.iSoHSQBS_DeNghi) else 0 end ) iSoNgaySinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.fTienHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.fTienHSQBS_DeNghi) else 0 end ) fSoTienSinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.iSoHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.iSoHSQBS_DeNghi) else 0 end ) iSoNgayKhamThaiKHHGD,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fTienHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fTienHSQBS_DeNghi) else 0 end ) fSoTienKhamThaiKHHGD,

				
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.iSoHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.iSoHSQBS_DeNghi) else 0 end ) iSoNgayPHSKThaiSan,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.fTienHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.fTienHSQBS_DeNghi) else 0 end ) fSoTienPHSKThaiSan

				INTO #tempDetalHSQBSHachToanSum
			FROM #TBL_TroCapThaiSanHachToan gt
			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iID_MaDonVi ASC))) AS sTT,
				iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon,
				SUM(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon,
				SUM(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon,
				SUM(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon,
				SUM(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD,
				SUM(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD,
				SUM(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan,
				SUM(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalHSQBSHachToan 
				FROM #tempDetalHSQBSHachToanSum
				Group by iID_MaDonVi,
				sTenDonVi

		-- Hach Toan LDHD
		SELECT 
				gt.iIDMaDonVi iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%' then sum(gt.iSoLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iSoLDHD_DeNghi) else 0 end )/30 iSoNgaySinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%'then sum(gt.fTienLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.fTienLDHD_DeNghi) else 0 end ) fSoTienSinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.iSoLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.iSoLDHD_DeNghi) else 0 end ) iSoNgaySinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.fTienLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.fTienLDHD_DeNghi) else 0 end ) fSoTienSinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.iSoLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.iSoLDHD_DeNghi) else 0 end ) iSoNgayKhamThaiKHHGD,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fTienLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fTienLDHD_DeNghi) else 0 end ) fSoTienKhamThaiKHHGD,

				
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.iSoLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.iSoLDHD_DeNghi) else 0 end ) iSoNgayPHSKThaiSan,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.fTienLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.fTienLDHD_DeNghi) else 0 end ) fSoTienPHSKThaiSan

				INTO #tempDetalLDHDHachToanSum
			FROM #TBL_TroCapThaiSanHachToan gt
			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iID_MaDonVi ASC))) AS sTT,
				iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon,
				SUM(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon,
				SUM(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon,
				SUM(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon,
				SUM(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD,
				SUM(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD,
				SUM(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan,
				SUM(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalLDHDHachToan 
				FROM #tempDetalLDHDHachToanSum
				Group by iID_MaDonVi,
				sTenDonVi

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
					SELECT * FROM  #tempDetalLDHDDuToan
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
		DROP TABLE #tempDetalSQDuToanSum
		DROP TABLE #tempDetalQNCNDuToanSum
		DROP TABLE #tempDetalCNVCQPDuToanSum
		DROP TABLE #tempDetalHSQBSDuToanSum
		DROP TABLE #tempDetalLDHDDuToanSum


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

		DROP TABLE #tempDetalSQHachToanSum
		DROP TABLE #tempDetalQNCNHachToanSum
		DROP TABLE #tempDetalCNVCQPHachToanSum
		DROP TABLE #tempDetalHSQBSHachToanSum
		DROP TABLE #tempDetalLDHDHachToanSum


		DROP TABLE #tempkqDuToan
		DROP TABLE #tempkqHachToan
		DROP TABLE #tempKQAll

end
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_khoi_Nhomdt]    Script Date: 8/22/2024 5:26:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_khoi_Nhomdt]
@INamLamViec int,
@IdMaDonVi nvarchar(max),
@IQuy int,
@Donvitinh int
as
begin
	---Lấy thông tin chi tiết giai thich tro cap du toan
		SELECT gt.* ,dv.sTenDonVi
			into #TBL_TroCapThaiSanDuToan 
						FROM BH_QTC_Quy_CheDoBHXH_ChiTiet gt
						Inner JOIN BH_QTC_Quy_CheDoBHXH ct ON gt.iID_QTC_Quy_CheDoBHXH=ct.ID_QTC_Quy_CheDoBHXH
						Inner JOIN DonVi dv on dv.iID_MaDonVi=ct.iID_MaDonVi
						WHERE (gt.sXauNoiMa like '9010001-010-011-0002%' )
								AND gt.iNamLamViec = @INamLamViec 
								AND dv.iNamLamViec=@INamLamViec
								AND dv.iID_MaDonVi  IN (select * from dbo.splitstring(@IdMaDonVi)) 
								--AND dv.iKhoi=2
								AND ct.iNamChungTu=@INamLamViec
								AND ct.iQuyChungTu = @IQuy 

	---Lấy thông tin chi tiết giai thich tro cap hach toan
		SELECT gt.*,dv.sTenDonVi 
			into #TBL_TroCapThaiSanHachToan 
						FROM BH_QTC_Quy_CheDoBHXH_ChiTiet gt
						Inner JOIN BH_QTC_Quy_CheDoBHXH ct ON gt.iID_QTC_Quy_CheDoBHXH=ct.ID_QTC_Quy_CheDoBHXH
						Inner JOIN DonVi dv on dv.iID_MaDonVi=ct.iID_MaDonVi
						WHERE ( gt.sXauNoiMa like '9010002-010-011-0002%')
								AND gt.iNamLamViec = @INamLamViec 
								AND dv.iNamLamViec=@INamLamViec
								AND dv.iID_MaDonVi  IN (select * from dbo.splitstring(@IdMaDonVi)) 
								AND ct.iNamChungTu=@INamLamViec
								--AND dv.iKhoi=1
								AND ct.iQuyChungTu = @IQuy 

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

		
		--- Lay thong tin giai thich theo khoi du toan


		---Du Toan SQ
		SELECT 
				gt.iIDMaDonVi iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%' then sum(gt.iTongSo_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iTongSo_DeNghi) else 0 end )/30 iSoNgaySinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%'then sum(gt.fTongTien_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.fTongTien_DeNghi) else 0 end ) fSoTienSinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.iTongSo_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.iTongSo_DeNghi) else 0 end ) iSoNgaySinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.fTongTien_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.fTongTien_DeNghi) else 0 end ) fSoTienSinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.iTongSo_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.iTongSo_DeNghi) else 0 end ) iSoNgayKhamThaiKHHGD,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fTongTien_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fTongTien_DeNghi) else 0 end ) fSoTienKhamThaiKHHGD,

				
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.iTongSo_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.iTongSo_DeNghi) else 0 end ) iSoNgayPHSKThaiSan,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.fTongTien_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.fTongTien_DeNghi) else 0 end ) fSoTienPHSKThaiSan
				
				INTO #tempDetalDuToanSum
			FROM #TBL_TroCapThaiSanDuToan gt
			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa


		SELECT 
			CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iID_MaDonVi ASC))) AS sTT,
				iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon,
				SUM(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon,
				SUM(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon,
				SUM(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon,
				SUM(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD,
				SUM(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD,
				SUM(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan,
				SUM(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalDuToan 
				FROM #tempDetalDuToanSum
				Group by iID_MaDonVi,
				sTenDonVi


		---Du Toan QNCN
		SELECT 
				gt.iIDMaDonVi iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%' then sum(gt.iTongSo_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iTongSo_DeNghi) else 0 end )/30 iSoNgaySinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%'then sum(gt.fTongTien_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.fTongTien_DeNghi) else 0 end ) fSoTienSinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.iTongSo_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.iTongSo_DeNghi) else 0 end ) iSoNgaySinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.fTongTien_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.fTongTien_DeNghi) else 0 end ) fSoTienSinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.iTongSo_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.iTongSo_DeNghi) else 0 end ) iSoNgayKhamThaiKHHGD,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fTongTien_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fTongTien_DeNghi) else 0 end ) fSoTienKhamThaiKHHGD,

				
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.iTongSo_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.iTongSo_DeNghi) else 0 end ) iSoNgayPHSKThaiSan,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.fTongTien_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.fTongTien_DeNghi) else 0 end ) fSoTienPHSKThaiSan

				INTO #tempDetalHachToanSum 
			FROM #TBL_TroCapThaiSanHachToan gt
			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa
		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iID_MaDonVi ASC))) AS sTT,
				iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon,
				SUM(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon,
				SUM(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon,
				SUM(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon,
				SUM(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD,
				SUM(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD,
				SUM(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan,
				SUM(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalHachToan
				FROM #tempDetalHachToanSum
				Group by iID_MaDonVi,
				sTenDonVi


---Tra ve kqua DuToan
			SELECT * INTO #tempkqDuToan FROM
			(
					SELECT * FROM #tempDuToan
					UNION ALL
					SELECT * FROM  #tempDetalDuToan
			) TEMPDuToan

------ Update total khoi du toan

			update T1
			set T1.iSoNgaySinhConNNuoiCon=T2.iSoNgaySinhConNNuoiCon,
			T1.fSoTienSinhConNNuoiCon=T2.fSoTienSinhConNNuoiCon,
			T1.iSoNgaySinhTroCapSinhCon=T2.iSoNgaySinhTroCapSinhCon,
			T1.fSoTienSinhTroCapSinhCon=T2.fSoTienSinhTroCapSinhCon,
			T1.iSoNgayKhamThaiKHHGD=T2.iSoNgayKhamThaiKHHGD,
			T1.fSoTienKhamThaiKHHGD=T2.fSoTienKhamThaiKHHGD,
			T1.iSoNgayPHSKThaiSan=T2.iSoNgayPHSKThaiSan,
			T1.fSoTienPHSKThaiSan=T2.fSoTienPHSKThaiSan
			FROM #tempkqDuToan T1, 
			(SELECT 
			Sum(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon
			, Sum(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon
			, Sum(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon
			, Sum(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon
			, Sum(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD
			, Sum(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD
			, Sum(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan
			, Sum(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
			, 3 type
			FROM #tempkqDuToan
			WHERE type=1) T2
			WHERE T1.type=T2.type


---------  Tra ve kqua HachToan
			SELECT * INTO #tempkqHachToan FROM
			(
					SELECT * FROM #tempHachToan
					UNION ALL
					SELECT * FROM  #tempDetalHachToan
					
			) TEMPHachToan


			update T1
			set T1.iSoNgaySinhConNNuoiCon=T2.iSoNgaySinhConNNuoiCon,
			T1.fSoTienSinhConNNuoiCon=T2.fSoTienSinhConNNuoiCon,
			T1.iSoNgaySinhTroCapSinhCon=T2.iSoNgaySinhTroCapSinhCon,
			T1.fSoTienSinhTroCapSinhCon=T2.fSoTienSinhTroCapSinhCon,
			T1.iSoNgayKhamThaiKHHGD=T2.iSoNgayKhamThaiKHHGD,
			T1.fSoTienKhamThaiKHHGD=T2.fSoTienKhamThaiKHHGD,
			T1.iSoNgayPHSKThaiSan=T2.iSoNgayPHSKThaiSan,
			T1.fSoTienPHSKThaiSan=T2.fSoTienPHSKThaiSan
			FROM #tempkqHachToan T1, 
			(SELECT 
			Sum(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon
			, Sum(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon
			, Sum(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon
			, Sum(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon
			, Sum(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD
			, Sum(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD
			, Sum(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan
			, Sum(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
			, 3 type
			FROM #tempkqHachToan
			WHERE type=1) T2
			WHERE T1.type=T2.type

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

		DROP TABLE #tempDetalDuToan



		drop table #tempHachToan

		DROP TABLE #tempDetalHachToan

		DROP TABLE #tempkqDuToan
		DROP TABLE #tempkqHachToan
		DROP TABLE #tempKQAll

end
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_khoi_Nhomdt]    Script Date: 8/22/2024 5:26:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_khoi_Nhomdt]
@INamLamViec int,
@IdMaDonVi nvarchar(max),
@IQuy int,
@Donvitinh int
as
begin
	---Lấy thông tin chi tiết giai thich tro cap du toan
	SELECT gt.*,dv.sTenDonVi into #TBL_TroCapTaiNanDuToan 
			FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gt
			LEFT JOIN BH_QTC_Quy_CheDoBHXH ct ON gt.iID_QTC_Quy_ChungTu=ct.ID_QTC_Quy_CheDoBHXH
			LEFT JOIN DonVi dv on dv.iID_MaDonVi=ct.iID_MaDonVi
			WHERE (gt.sXauNoiMa like '9010001-010-011-0003%')
					AND gt.iNamLamViec = @INamLamViec and gt.iQuy = @IQuy 
					AND dv.iNamLamViec=@INamLamViec
					AND dv.iID_MaDonVi  IN (select * from dbo.splitstring(@IdMaDonVi)) 
					--AND dv.iKhoi=2
					AND ct.iNamChungTu=@INamLamViec
	---Lấy thông tin chi tiết giai thich tro cap hach toan
	SELECT gt.*,dv.sTenDonVi into #TBL_TroCapTaiNanHachToan 
			FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gt
			LEFT JOIN BH_QTC_Quy_CheDoBHXH ct ON gt.iID_QTC_Quy_ChungTu=ct.ID_QTC_Quy_CheDoBHXH
			LEFT JOIN DonVi dv on dv.iID_MaDonVi=ct.iID_MaDonVi
			WHERE ( gt.sXauNoiMa like '9010002-010-011-0003%')
					AND gt.iNamLamViec = @INamLamViec and gt.iQuy = @IQuy 
					AND dv.iNamLamViec=@INamLamViec
					AND dv.iID_MaDonVi  IN (select * from dbo.splitstring(@IdMaDonVi)) 
					--AND dv.iKhoi=1
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
		,0 As FTienGiamDinhTL
		,0 As FTienTroCap1Lan
		,0 As FTienTroCap1LanTL
		,0 As FTienTCTP
		,0 As FTienTCTPTL
		,0 As FTienTCHangThang
		,0 As FTienTCHangThangTL
		,0 As FTienTCPHCNvPV
		,0 As FTienTCPHCNvPVTL
		,0 as FTienTCCDTNLD
		,0 as FTienTCCDTNLDTL
		,0 as ISoNgayDSPHSK
		,0 as ISoNgayDSPHSKTL
		,0 as FTienDSPHSK
		,0 as FTienDSPHSKTL
		,3 as type
		,1 IsHangCha
		,1 IsParent
		into #tempDuToan



	---II. Khối hạch toán
		SELECT 
		N'II. Khối hạch toán' As STT
		,'' As sTenCanBo
		,'' As sTenPhanHo
		,'' As sMaCapBac
		,'' As sSoQuyetDinh
		,'' As SNgayQuyetDinh
		,0 As FTienGiamDinh
		,0 As FTienGiamDinhTL
		,0 As FTienTroCap1Lan
		,0 As FTienTroCap1LanTL
		,0 As FTienTCTP
		,0 As FTienTCTPTL
		,0 As FTienTCHangThang
		,0 As FTienTCHangThangTL
		,0 As FTienTCPHCNvPV
		,0 As FTienTCPHCNvPVTL
		,0 as FTienTCCDTNLD
		,0 as FTienTCCDTNLDTL
		,0 as ISoNgayDSPHSK
		,0 as ISoNgayDSPHSKTL
		,0 as FTienDSPHSK
		,0 as FTienDSPHSKTL
		,3 as type
		,1 IsHangCha
		,1 IsParent
		into #tempHachToan
		--- Total SQ HachToan
		

	----- Lay ra  du toan
		-- Du Toan SQ
			SELECT
				 CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY tbltctn.sTenCanBo ASC))) AS STT
				,tbltctn.sTenCanBo
				,tbltctn.sTenDonVi sTenPhanHo
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
				into #tempDetailDuToanSum
			FROM #TBL_TroCapTaiNanDuToan tbltctn
			GROUP BY tbltctn.sTenCanBo,
				tbltctn.sTenDonVi,
				tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.dNgayQuyetDinh
				,tbltctn.sXauNoiMa 

			SELECT
				 CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY tbltctn.sTenCanBo ASC))) AS STT
				,tbltctn.sTenCanBo
				,tbltctn.sTenPhanHo
				, tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.SNgayQuyetDinh
				, SUM(FTienGiamDinh) FTienGiamDinh
				, SUM(FTienGiamDinhTL) FTienGiamDinhTL
				, SUM(FTienTroCap1Lan) FTienTroCap1Lan
				, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
				, SUM(FTienTCTP) FTienTCTP
				, SUM(FTienTCTPTL) FTienTCTPTL
				, SUM(FTienTCHangThang) FTienTCHangThang
				, SUM(FTienTCHangThangTL) FTienTCHangThangTL
				, SUM(FTienTCPHCNvPV) FTienTCPHCNvPV
				, SUM(FTienTCPHCNvPVTL) FTienTCPHCNvPVTL
				, SUM(FTienTCCDTNLD) FTienTCCDTNLD
				, SUM(FTienTCCDTNLDTL) FTienTCCDTNLDTL
				, SUM(ISoNgayDSPHSK) ISoNgayDSPHSK
				, SUM(ISoNgayDSPHSKTL) ISoNgayDSPHSKTL
				, SUM(FTienDSPHSK) FTienDSPHSK
				, SUM(FTienDSPHSKTL) FTienDSPHSKTL
				,1 as type
				,0 IsHangCha
				,null IsParent
				INTO #tempDetailDuToan
				FROM #tempDetailDuToanSum tbltctn
				GROUP BY tbltctn.sTenCanBo,
				tbltctn.sTenPhanHo,
				tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.SNgayQuyetDinh

		-- Du Toan QNCN
			SELECT
				 CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY tbltctn.sTenCanBo ASC))) AS STT
				,tbltctn.sTenCanBo
				,tbltctn.sTenDonVi sTenPhanHo
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
				into  #tempDetailHachToanSum
			FROM #TBL_TroCapTaiNanHachToan tbltctn
			GROUP BY tbltctn.sTenCanBo,
				tbltctn.sTenDonVi,
				tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.dNgayQuyetDinh
				,tbltctn.sXauNoiMa 

			SELECT
				 CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY tbltctn.sTenCanBo ASC))) AS STT
				,tbltctn.sTenCanBo
				,tbltctn.sTenPhanHo
				, tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.SNgayQuyetDinh
				, SUM(FTienGiamDinh) FTienGiamDinh
				, SUM(FTienGiamDinhTL) FTienGiamDinhTL
				, SUM(FTienTroCap1Lan) FTienTroCap1Lan
				, SUM(FTienTroCap1LanTL) FTienTroCap1LanTL
				, SUM(FTienTCTP) FTienTCTP
				, SUM(FTienTCTPTL) FTienTCTPTL
				, SUM(FTienTCHangThang) FTienTCHangThang
				, SUM(FTienTCHangThangTL) FTienTCHangThangTL
				, SUM(FTienTCPHCNvPV) FTienTCPHCNvPV
				, SUM(FTienTCPHCNvPVTL) FTienTCPHCNvPVTL
				, SUM(FTienTCCDTNLD) FTienTCCDTNLD
				, SUM(FTienTCCDTNLDTL) FTienTCCDTNLDTL
				, SUM(ISoNgayDSPHSK) ISoNgayDSPHSK
				, SUM(ISoNgayDSPHSKTL) ISoNgayDSPHSKTL
				, SUM(FTienDSPHSK) FTienDSPHSK
				, SUM(FTienDSPHSKTL) FTienDSPHSKTL
				,1 as type
				,0 IsHangCha
				,null IsParent
				INTO #tempDetailHachToan
				FROM #tempDetailHachToanSum tbltctn
				GROUP BY tbltctn.sTenCanBo,
				tbltctn.sTenPhanHo,
				tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.SNgayQuyetDinh

		

---------  Tra ve kqua DuToan
			SELECT * INTO #tempkqDuToan FROM
			(
					SELECT * FROM #tempDuToan
					UNION ALL
					SELECT * FROM  #tempDetailDuToan
					
			) TEMPDuToan

------ Update total khoi du toan
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
			FROM #tempkqDuToan T1, (			SELECT 
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
			FROM #tempkqDuToan
			WHERE type=1) T2
			WHERE T1.type=T2.type


---------  Tra ve kqua HachToan
			SELECT * INTO #tempkqHachToan FROM
			(
					SELECT * FROM #tempHachToan
					UNION ALL
					SELECT * FROM  #tempDetailHachToan
			) TEMPHachToan


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
			FROM #tempkqHachToan T1, (SELECT 
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
			FROM #tempkqHachToan
			WHERE type=1) T2
			WHERE T1.type=T2.type


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
		DROP TABLE #tempDetailDuToan
		DROP TABLE #tempDetailDuToanSum
		DROP TABLE #tempHachToan
		DROP TABLE #tempDetailHachToan
		DROP TABLE #tempDetailHachToanSum

		DROP TABLE #tempkqDuToan
		DROP TABLE #tempkqHachToan
		DROP TABLE #tempKQAll

end
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_rpt_qttm_bhyt_than_nhan_nld_tong_hop_don_vi]    Script Date: 8/22/2024 5:26:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_rpt_qttm_bhyt_than_nhan_nld_tong_hop_don_vi] 
	@NamLamViec int,
	@IdDonVis nvarchar(50),
	@Donvitinh int,
	@IQuy int
AS
BEGIN

	select 
	ctct.sXauNoiMa,
	sum(ctct.fDuToan) fDuToan
	into #tempDuToan
	from
	BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct
	where ctct.iNamLamViec = @NamLamViec
	and ctct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))
	group by
	ctct.sXauNoiMa ,ctct.iID_MaDonVi

	select
		mlns.iID_MLNS,
		mlns.sMoTa,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		mlns.sXauNoiMa,
		@NamLamViec iNamLamViec,
		sum(dToan.fDuToan)/@Donvitinh fDuToan,
		sum(chungtudonvi.fDaQuyetToan)/@Donvitinh fDaQuyetToan,
		(sum(isnull(dToan.fDuToan,0))- sum(isnull(chungtudonvi.fDaQuyetToan,0))- sum(isnull(chungtudonvi.fSoPhaiThu,0))) /@Donvitinh fConLai,
		sum(chungtudonvi.fSoPhaiThu)/@Donvitinh fSoPhaiThu
		from
		(select
			BH_DM_MucLucNganSach.iID_MLNS,
			BH_DM_MucLucNganSach.iID_MLNS_Cha,
			BH_DM_MucLucNganSach.bHangCha,
			BH_DM_MucLucNganSach.sLNS,
			BH_DM_MucLucNganSach.sL,
			BH_DM_MucLucNganSach.sK,
			BH_DM_MucLucNganSach.sM,
			BH_DM_MucLucNganSach.sTM,
			BH_DM_MucLucNganSach.sTTM,
			BH_DM_MucLucNganSach.sNG,
			BH_DM_MucLucNganSach.sTNG,
			BH_DM_MucLucNganSach.sXauNoiMa,
			BH_DM_MucLucNganSach.sMoTa
		from BH_DM_MucLucNganSach 
		where sLNS like '903%'
		and iNamLamViec = @NamLamViec) mlns
		left join
		(select
			ct.iID_MaDonVi,
			ct.iNamLamViec,
			ctct.iID_QTTM_BHYT_ChungTu_ChiTiet,
			ctct.fConLai,
			ctct.fDaQuyetToan,
			ctct.fDuToan,
			ctct.fSoPhaiThu,
			ctct.iID_MLNS,
			ctct.iID_MLNS_Cha,
			ctct.sGhiChu,
			ctct.sLNS,
			ctct.sXauNoiMa,
			ctct.iID_QTTM_BHYT_ChungTu
			from
			BH_QTTM_BHYT_Chung_Tu ct
			join
			BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct on ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
			where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam = @IQuy
			and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))
			and ct.iLoaiTongHop = 1
			--and ct.bDaTongHop = 0
				) chungtudonvi 
			on mlns.iID_MLNS = chungtudonvi.iID_MLNS
			left join #tempDuToan dToan on dToan.sXauNoiMa=mlns.sXauNoiMa
			group by
				mlns.iID_MLNS,
				mlns.sMoTa,
				mlns.iID_MLNS_Cha,
				mlns.bHangCha,
				mlns.sXauNoiMa
		order by mlns.sXauNoiMa
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_cp_get_donvi_thongtri]    Script Date: 8/22/2024 5:26:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay don vi cua chung tu tổng hop cap phat chi tiet theo id
-- =============================================
CREATE PROCEDURE [dbo].[sp_bhxh_cp_get_donvi_thongtri]
	@YearOfWork int,
	@Quy int
AS
BEGIN
	SELECT 
	distinct
	dv.* 
	FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN  DonVi  dv on dv.iID_MaDonVi IN (SELECT * FROM splitstring(ctct.iID_MaDonVi))
	where ctct.iID_CP_ChungTu in 
	(
		select cp.iID_CP_ChungTu from BH_CP_ChungTu cp
		where cp.iNamChungTu=@YearOfWork
			and cp.iQuy=@Quy
	)
	and dv.iNamLamViec=@YearOfWork
	order by dv.iID_MaDonVi
END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_get_loaichi_thongtri]    Script Date: 8/22/2024 5:26:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay don vi cua chung tu tổng hop cap phat chi tiet theo id
-- =============================================
CREATE PROCEDURE [dbo].[sp_cp_get_loaichi_thongtri]
	@YearOfWork int,
	@Quy int,
	@LstDonVi nvarchar(max)
AS
BEGIN
	select
	distinct
	dm.* from BH_DM_LoaiChi dm
	inner join 
	(select iID_LoaiCap from BH_CP_ChungTu
	where iID_CP_ChungTu in
	(select iID_CP_ChungTu from BH_CP_ChungTu_ChiTiet
	where 
	iNamChungTu=@YearOfWork
	and
	iID_MaDonVi in (select * from splitstring(@LstDonVi)) 
	)
	and iQuy=@Quy
	) tblIdLoaiChi on dm.iID=tblIdLoaiChi.iID_LoaiCap
	where dm.iNamLamViec=@YearOfWork
	and dm.iTrangThai=1
	order by sMaLoaiChi
END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_luong_bhxh_import_qtc_bhxh]    Script Date: 8/22/2024 5:26:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_luong_bhxh_import_qtc_bhxh]
	@YearOfWork int,
	@Months nvarchar(20)
AS
BEGIN
	select chedo.sXauNoiMaMlnsBHXH, luong.*
	into luong_temp
	from TL_BangLuong_ThangBHXH luong
	left join TL_DM_CheDoBHXH chedo
		on luong.sMaCheDo = chedo.sMaCheDo
	where 
		luong.iNam = @YearOfWork
		and luong.iID_Parent in
		(	select Id from  TL_DS_CapNhap_BangLuong
			where Thang in (SELECT * FROM f_split(@Months))
			and Nam=@YearOfWork
			and  ISNULL(STongHop,'')<>''
			and Ma_CachTL='CACH2'
			)
		and luong.iThang in (SELECT * FROM f_split(@Months))
		and luong.sMaCheDo in 
		(select distinct sMaCheDo from TL_DM_CheDoBHXH
		where sMaCheDoCha is not null and sMaCheDoCha <> ''
		--and sMaCheDo not in (select distinct sMaCheDoCha from TL_DM_CheDoBHXH)
		and (upper(sMaCheDo) not like '%_HS%' and upper(sMaCheDo) not like '%_HESO%'))

	--Thong tin luong Si quan
	select distinct
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo,
		temp.SoNguoi,
		sum(SoTien) SoTien
	into luong_temp_sq
	from
		(select sXauNoiMaMlnsBHXH, sMaCheDo,
			count(sMaHieuCanBo) SoNguoi,
			isnull(sum(nGiaTri), 0) SoTien
			from luong_temp
			where sMaCB like '1%'
			group by
				sXauNoiMaMlnsBHXH,
				sMaCheDo,
				sMaHieuCanBo) temp
	group by
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo,
		temp.SoNguoi

	--Thong tin luong QNCN
	select distinct
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo,
		temp.SoNguoi,
		sum(SoTien) SoTien
	into luong_temp_qncn
	from
		(select sXauNoiMaMlnsBHXH, sMaCheDo,
			count(sMaHieuCanBo) SoNguoi,
			isnull(sum(nGiaTri), 0) SoTien
			from luong_temp
			where sMaCB like '2%'
			group by 
				sXauNoiMaMlnsBHXH,
				sMaCheDo,
				sMaHieuCanBo) temp
	group by
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo,
		temp.SoNguoi

	--Thong tin luong HSQ_BS
	select distinct
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo,
		temp.SoNguoi,
		sum(SoTien) SoTien
	into luong_temp_hsq_bs
	from
		(select sXauNoiMaMlnsBHXH, sMaCheDo,
			count(sMaHieuCanBo) SoNguoi,
			isnull(sum(nGiaTri), 0) SoTien
			from luong_temp
			where sMaCB like '0%'
			group by 
				sXauNoiMaMlnsBHXH,
				sMaCheDo,
				sMaHieuCanBo) temp
	group by
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo,
		temp.SoNguoi

	--Thong tin luong VCQP
	select distinct
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo,
		temp.SoNguoi,
		sum(SoTien) SoTien
	into luong_temp_vcqp
	from
		(select sXauNoiMaMlnsBHXH, sMaCheDo,
			count(sMaHieuCanBo) SoNguoi,
			isnull(sum(nGiaTri), 0) SoTien
			from luong_temp
			where sMaCB in ('3.1', '3.2', '3.3')
			group by 
				sXauNoiMaMlnsBHXH,
				sMaCheDo,
				sMaHieuCanBo) temp
	group by
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo,
		temp.SoNguoi

	--Thong tin luong hdld
	select distinct
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo,
		temp.SoNguoi,
		sum(SoTien) SoTien
	into luong_temp_hdld
	from
		(select sXauNoiMaMlnsBHXH, sMaCheDo,
			count(sMaHieuCanBo) SoNguoi,
			isnull(sum(nGiaTri), 0) SoTien
			from luong_temp
			where sMaCB in ('3.4', '423', '425','43')
			group by 
				sXauNoiMaMlnsBHXH,
				sMaCheDo,
				sMaHieuCanBo) temp
	group by
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo,
		temp.SoNguoi

	--Ket qua
	select distinct
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo MaCheDo,
		sq.SoNguoi SoNguoiSQ,
		sq.SoTien SoTienSQ,
		qncn.SoTien SoTienQNCN,
		qncn.SoNguoi SoNguoiQNCN,
		hsq.SoTien SoTienHSQ,
		hsq.SoNguoi SoNguoiHSQ,
		vcqp.SoTien SoTienVCQP,
		vcqp.SoNguoi SoNguoiVCQP,
		hdld.SoTien SoTienHDLD,
		hdld.SoNguoi SoNguoiHDLD
	from luong_temp temp
	left join luong_temp_sq sq on temp.sMaCheDo = sq.sMaCheDo
	left join luong_temp_qncn qncn on temp.sMaCheDo = qncn.sMaCheDo
	left join luong_temp_hsq_bs hsq on temp.sMaCheDo = hsq.sMaCheDo
	left join luong_temp_vcqp vcqp on temp.sMaCheDo = vcqp.sMaCheDo
	left join luong_temp_hdld hdld on temp.sMaCheDo = hdld.sMaCheDo
	where isnull(temp.sXauNoiMaMlnsBHXH, '') <> ''

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[luong_temp]') AND type in (N'U'))
	drop table luong_temp;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[luong_temp_sq]') AND type in (N'U'))
	drop table luong_temp_sq;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[luong_temp_qncn]') AND type in (N'U'))
	drop table luong_temp_qncn;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[luong_temp_hsq_bs]') AND type in (N'U'))
	drop table luong_temp_hsq_bs;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[luong_temp_vcqp]') AND type in (N'U'))
	drop table luong_temp_vcqp;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[luong_temp_hdld]') AND type in (N'U'))
	drop table luong_temp_hdld;

END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_quykinhphi_quanly_chungtu_chitiet_tao_tonghop]    Script Date: 8/22/2024 5:26:33 PM ******/
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


  DECLARE @Quy INT;
		SELECT @Quy =(SELECT top(1)  iQuyChungTu
		FROM BH_QTC_Quy_KinhPhiQuanLy 
		WHERE  ID_QTC_Quy_KinhPhiQuanLy IN
		(SELECT *
		FROM f_split(@ListIdChungTuTongHop)))


		SELECT 
		SUM(ISNULL(fTienThucChi,0)) fTienThucChi,
		SUM(ISNULL(fTienDeNghiQuyetToanQuyNay,0)) fTienQuyetToanDaDuyet,
		sXauNoiMa
		into #temp2
		FROM
		( SELECT fTienThucChi
			,fTienDeNghiQuyetToanQuyNay
			,fTienQuyetToanDaDuyet
			,fTienXacNhanQuyetToanQuyNay
			,iID_QTC_Quy_KinhPhiQuanLy
			,ct.sXauNoiMa
				FROM BH_QTC_Quy_KinhPhiQuanLy_ChiTiet ct
				INNER JOIN BH_DM_MucLucNganSach dmns on ct.sXauNoiMa = dmns.sXauNoiMa
				where dmns.bHangCha = 0 and dmns.iNamLamViec = @NamLamViec and ct.iNamLamViec=@NamLamViec
				
			) ctct
				INNER JOIN BH_QTC_Quy_KinhPhiQuanLy ct ON ctct.iID_QTC_Quy_KinhPhiQuanLy = ct.ID_QTC_Quy_KinhPhiQuanLy 
					WHERE
			ct.iQuyChungTu < @Quy
			and ct.iNamChungTu=@NamLamViec
			and ct.iID_MaDonVi=@SMaDonVi
		GROUP BY sXauNoiMa

		select ml.iID_MLNS iID_MucLucNganSach
		,ml.sMoTa sNoiDung
		,ml.sXauNoiMa
		,ml.sM
		,ml.sTM
		,T2.fTienThucChi
		,T2.fTienQuyetToanDaDuyet
		into #tblmlns
		from BH_DM_MucLucNganSach ml
		left join #temp2 T2 on ml.sXauNoiMa=t2.sXauNoiMa
		where ml.iTrangThai=1
		and ml.sLNS='9010003'
		and iNamLamViec=@NamLamViec
		and bHangCha=0

		
		SELECT 
		  iID_MucLucNganSach , 
		  sM,
		  sTM,
		  sNoiDung, 
		  sum(fTienThucChi) fTienThucChi, 
		  sum(fTienQuyetToanDaDuyet) fTienQuyetToanDaDuyet, 
		  sum(fTienDeNghiQuyetToanQuyNay) fTienDeNghiQuyetToanQuyNay, 
		  sum(fTienXacNhanQuyetToanQuyNay) fTienXacNhanQuyetToanQuyNay, 
		  sXauNoiMa
		   into #temp1
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
		  sNoiDung,sXauNoiMa

		select 
	T1.iID_MucLucNganSach
	,t1.fTienThucChi
	,t1.fTienDeNghiQuyetToanQuyNay
	,t1.fTienQuyetToanDaDuyet
	,t1.fTienXacNhanQuyetToanQuyNay
	,t2.sM
	,t2.sTM
	,t2.sXauNoiMa
	,T1.sNoiDung
	into #temp3
	from #temp1 T1
	left join BH_DM_MucLucNganSach T2 on T1.SXauNoiMa=T2.SXauNoiMa
	where T2.iNamLamViec=@NamLamViec
	and T2.iTrangThai=1

		  	
	SELECT * INTO #TblXAUNOIMA FROM
	(SELECT iID_MucLucNganSach,sXauNoiMa,sM,sTM,sNoiDung FROM  #tblmlns
	UNION 
	SELECT iID_MucLucNganSach,sXauNoiMa,sM,sTM,sNoiDung FROM  #temp3) TML

	 select  t1.iID_MucLucNganSach  
	,t1.sM
	,t1.sNoiDung
	,t1.sTM
	,t1.sXauNoiMa
	,isnull(T3.fTienThucChi,0) + isnull(T2.fTienDeNghiQuyetToanQuyNay,0)  fTienThucChi
	,isnull(T3.fTienThucChi,0)  fTienQuyetToanDaDuyet
	,isnull(T2.fTienDeNghiQuyetToanQuyNay,0) fTienDeNghiQuyetToanQuyNay
	,isnull(T2.fTienXacNhanQuyetToanQuyNay,0) fTienXacNhanQuyetToanQuyNay
	into #tempDuLieu
	from #TblXAUNOIMA T1
	left join #temp3 T2 on t2.sXauNoiMa=T1.sXauNoiMa
	left join #tblmlns T3 on T3.sXauNoiMa=t1.sXauNoiMa
	order by t1.sXauNoiMa

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
  0, 
  sum(fTienThucChi), 
  sum(fTienQuyetToanDaDuyet), 
  sum(fTienDeNghiQuyetToanQuyNay), 
  sum(fTienXacNhanQuyetToanQuyNay), 
  Null, 
  GETDATE(), 
  Null, 
  @Nguoitao,
  sXauNoiMa,
  @NamLamViec,
  @SMaDonVi
FROM 
 #tempDuLieu
where fTienThucChi<>0 or fTienDeNghiQuyetToanQuyNay<>0 or fTienQuyetToanDaDuyet<>0 or fTienXacNhanQuyetToanQuyNay<>0
GROUP BY 
  iID_MucLucNganSach, 
  sM,
  sTM,
  sNoiDung,sXauNoiMa
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
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Nhomdt]    Script Date: 8/22/2024 5:26:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Nhomdt]
	@INamLamViec int,
	@IdMaDonVi nvarchar(MAX),
	@IQuy int,
	@Donvitinh int
AS
BEGIN
		SELECT gt.*,dv.sTenDonVi INTO #tblTroCap FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gt
		left join DonVi dv on dv.iID_MaDonVi=gt.iiD_MaDonVi
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

			SELECT 1 bHangCha
			, N'(I)' STT
			, N'Khối dự toán'as STenCanBo 
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
			, 0 Child
			into #tblDuToan

			SELECT 1 bHangCha
			, N'(I)' STT
			, N'Khối hạch toán'as STenCanBo 
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
			, 0 Child
			into #tblHachToan

	--- 9010001-010-011-0004 Tro cap Huu tri du toan
		SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
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
			into #tempDetailHuuTriSum
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

	--- 9010001-010-011-0004 Tro cap Huu tri Hach toan
		SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
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
			into #tempDetailHuuTriHachToanSum
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010002-010-011-0004%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

	
	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo  
			, tbltc. sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,sum(FTienTroCap1Lan) FTienTroCap1Lan
			,sum(FTienTroCapKV) FTienTroCapKV
			,sum(FTienTroCapMT) FTienTroCapMT
			,sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			,sum(FTienTroCapKVTL) FTienTroCapKVTL
			,sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			, 0 Type
			, null IsParent
			, 1 Child
			INTO #TempDetailHuuTri
			from  #TempDetailHuuTriSum tbltc
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
			,tbltc.sTenCanBo  
			, tbltc. sTenPhanHo
			,tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh

		SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo  
			, tbltc. sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			,sum(FTienTroCapKVTL) FTienTroCapKVTL
			,sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			, 0 Type
			, null IsParent
			, 2 Child
			INTO #tempDetailHuuTriHachToan
			from  #tempDetailHuuTriHachToanSum tbltc
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
			,tbltc.sTenCanBo  
			, tbltc. sTenPhanHo
			,tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh



	SELECT * INTO #tempHuuTri FROM
	(
	SELECT 1 bHangCha
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
			, 2 Type
			, null IsParent
			, 0 Child

	UNION ALL
	SELECT * FROM #tblDuToan
	UNION ALL
	SELECT * FROM #tempDetailHuuTri	
	UNION ALL
	SELECT * FROM #tblHachToan
	UNION ALL
	SELECT * FROM #tempDetailHuuTriHachToan	
	) tblHuutri

	 if (SELECT COUNT(1) FROM #tempHuuTri) > 2
		UPDATE #tempHuuTri set bHasData = 1
		--- update Khối dự toán
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
		FROM #tempHuuTri WHERE  Child=1 ) detail
		where #tempHuuTri.STenCanBo=N'Khối dự toán'

		--- update Khối hach toan
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
		FROM #tempHuuTri WHERE  Child=2 ) detail
		where #tempHuuTri.STenCanBo=N'Khối hạch toán'

		--- update TC  Huu tri
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
		FROM #tempHuuTri WHERE  Child=2 or Child=1 ) detail
		where #tempHuuTri.STenCanBo=N'TC Hưu Trí'

	--- 9010001-010-011-0005 Tro cap phuc vien Du toan
	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
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
			into #tempDetailPhucVienSum
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0005%' 
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac	
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo  
			, tbltc. sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			,sum(FTienTroCapKVTL) FTienTroCapKVTL
			,sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			, 0 Type
			, null IsParent
			, 1 Child
			INTO #tempDetailPhucVien
			from  #tempDetailPhucVienSum tbltc
			GROUP BY  tbltc.sMa_Hieu_Can_Bo,tbltc.sTenCanBo  
			, tbltc. sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			ORDER BY tbltc.sTenCanBo ASC


--- 9010001-010-011-0005 Tro cap phuc vien hach toan
	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
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
			into #tempDetailPhucVienHachToanSum
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE  tbltc.sXauNoiMa like '9010002-010-011-0005%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac	
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo  
			, tbltc. sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			,sum(FTienTroCapKVTL) FTienTroCapKVTL
			,sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			, 0 Type
			, null IsParent
			, 2 Child
			INTO #tempDetailPhucVienHachToan
			from  #tempDetailPhucVienHachToanSum tbltc
			GROUP BY  tbltc.sMa_Hieu_Can_Bo,tbltc.sTenCanBo  
			, tbltc. sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			ORDER BY tbltc.sTenCanBo ASC


	SELECT * INTO #tempPhucVien FROM
	(
	SELECT 1 bHangCha
			, N'(II)' STT
			, N'TC Phục viên'as STenCanBo 
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
			, null IsParent
			, 0 Child
	UNION ALL
	SELECT * FROM #tblDuToan
	UNION ALL
	SELECT * FROM #tempDetailPhucVien
	UNION ALL 
	SELECT * FROM #tblHachToan
	UNION ALL 
	SELECT * FROM #tempDetailPhucVienHachToan
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
		FROM #tempPhucVien WHERE  Child=1 ) detail
		where #tempPhucVien.STenCanBo=N'Khối dự toán'

		--- update Khối hach toan
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
		FROM #tempPhucVien WHERE  Child=2 ) detail
		where #tempPhucVien.STenCanBo=N'Khối hạch toán'

		--- update  Phục viên
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
		FROM #tempPhucVien WHERE  Child=2 or Child=1 ) detail
		where #tempPhucVien.STenCanBo=N'TC Phục viên'


	--- 9010001-010-011-0006 Tro cap xuat ngu du toan

	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS  FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			INTO #tempDetailXuatNguSum
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0006%' 
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac	
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sSoQuyetDinh) AS VARCHAR(6)) STT
			, CASE WHEN  tbltc.sSoQuyetDinh is null or  tbltc.sSoQuyetDinh='' then  Convert(varchar(10),(COUNT(tbltc.sSoQuyetDinh))) else   Convert(varchar(10),(COUNT(tbltc.sMa_Hieu_Can_Bo))) end  + N' Đồng chí' as sTenCanBo
			, tbltc.sTenPhanHo
			, '' SMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			, sum(FTienTroCapKVTL) FTienTroCapKVTL
			, sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 Child
			INTO #tempDetailXuatNgu
			from  #tempDetailXuatNguSum tbltc
			GROUP BY tbltc.sTenPhanHo
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh


	--- 9010001-010-011-0006 Tro cap xuat ngu hạch toán

	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS  FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			INTO #tempDetailXuatNguHachToanSum
	FROM  #tblTroCap tbltc
	WHERE  tbltc.sXauNoiMa like '9010002-010-011-0006%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac	
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sSoQuyetDinh) AS VARCHAR(6)) STT
			, CASE WHEN  tbltc.sSoQuyetDinh is null or  tbltc.sSoQuyetDinh='' then  Convert(varchar(10),(COUNT(tbltc.sSoQuyetDinh))) else   Convert(varchar(10),(COUNT(tbltc.sMa_Hieu_Can_Bo))) end  + N' Đồng chí' as sTenCanBo
			, tbltc.sTenPhanHo
			, '' SMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			, sum(FTienTroCapKVTL) FTienTroCapKVTL
			, sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			, 0 Type
			, null IsParent
			, 2 Child
			INTO #tempDetailXuatNguHachToan
			from  #tempDetailXuatNguHachToanSum tbltc
			GROUP BY tbltc.sTenPhanHo
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh

	SELECT * INTO #tempXuatNgu FROM
	(
	SELECT 1 bHangCha
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
			, 2 Type
			, null IsParent
			, 0 Child
	UNION ALL
	SELECT * FROM #tblDuToan
	UNION ALL 
	SELECT * FROM #tempDetailXuatNgu
	UNION ALL 
	SELECT * FROM #tblHachToan
	UNION ALL 
	SELECT * FROM #tempDetailXuatNguHachToan
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
		FROM #tempXuatNgu WHERE  Child=1 ) detail
		where #tempXuatNgu.STenCanBo=N'Khối dự toán'

		--- update Khối hach toan
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
		FROM #tempXuatNgu WHERE  Child=2 ) detail
		where #tempXuatNgu.STenCanBo=N'Khối hạch toán'

		--- update  TC Xuất ngũ
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
		FROM #tempXuatNgu WHERE  Child=2 or Child=1 ) detail
		where #tempXuatNgu.STenCanBo=N'TC Xuất ngũ'

	--- 9010001-010-011-0007 tro cap thoi viec du toan

		SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
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
			INTO #tempDetailThoiViecSum
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0007%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC


	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo  
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			,sum(FTienTroCapKVTL) FTienTroCapKVTL
			,sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			, 0 Type
			, null IsParent
			, 1 Child
			into #tempDetailThoiViec
			from  #tempDetailThoiViecSum tbltc
			GROUP BY   tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo 
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
		ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

	--- 9010001-010-011-0007 tro cap thoi viec hach toan
		SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
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
			INTO #tempDetailThoiViecHachToanSum
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010002-010-011-0007%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo  
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			,sum(FTienTroCapKVTL) FTienTroCapKVTL
			,sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			, 0 Type
			, null IsParent
			, 2 Child
			into #tempDetailThoiViecHachToan
			from  #tempDetailThoiViecHachToanSum tbltc
			GROUP BY   tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo 
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
		ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

	SELECT * INTO #tempThoiViec FROM
	(
	SELECT 1 bHangCha
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
			, 2 Type
			, null IsParent
			, 0 Child
	UNION ALL
	SELECT * FROM #tblDuToan
	UNION ALL
	SELECT * FROM #tempDetailThoiViec
	UNION ALL
	SELECT * FROM #tblHachToan
	UNION ALL 
	SELECT * FROM #tempDetailThoiViecHachToan
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
		FROM #tempThoiViec WHERE  Child=1 ) detail
		where #tempThoiViec.STenCanBo=N'Khối dự toán'

		--- update Khối hach toan
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
		FROM #tempThoiViec WHERE  Child=2 ) detail
		where #tempThoiViec.STenCanBo=N'Khối hạch toán'

		--- update  TC Thôi việc
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
		FROM #tempThoiViec WHERE  Child=2 or Child=1 ) detail
		where #tempThoiViec.STenCanBo=N'TC Thôi việc'

	--- 9010001-010-011-0008 tro cap tu tuat du toan

	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			 , tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
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
			INTO #tempDetailTuTuatSum
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0008%' 
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC


	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo  
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			,sum(FTienTroCapKVTL) FTienTroCapKVTL
			,sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			, 0 Type
			, null IsParent
			, 1 Child
			into #tempDetailTuTuat
			from  #tempDetailTuTuatSum tbltc
			GROUP BY   tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo 
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

--- 9010001-010-011-0008 tro cap tu tuat hach toan
	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			 , tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
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
			INTO #tempDetailTuTuatHachToanSum
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010002-010-011-0008%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC


	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo  
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			,sum(FTienTroCapKVTL) FTienTroCapKVTL
			,sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			, 0 Type
			, null IsParent
			, 2 Child
			into #tempDetailTuTuatHachToan
			from  #tempDetailTuTuatHachToanSum tbltc
			GROUP BY   tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo 
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

	SELECT * INTO #tempTuTuat FROM
	(
	SELECT 1 bHangCha
			, N'(V)' STT
			, N'TC Tử tuất'as STenCanBo 
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
			, null IsParent
			, 0 Child
	UNION ALL
	SELECT * FROM #tblDuToan
	UNION ALL
	SELECT * FROM #tempDetailTuTuat
	UNION ALL
	SELECT  * FROM #tblHachToan
	UNION ALL 
	SELECT * FROM #tempDetailTuTuatHachToan
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
		FROM #tempTuTuat WHERE  Child=1 ) detail
		where #tempTuTuat.STenCanBo=N'Khối dự toán'

		--- update Khối hach toan
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
		FROM #tempTuTuat WHERE  Child=2 ) detail
		where #tempTuTuat.STenCanBo=N'Khối hạch toán'

		--- update  TC Tử tuất
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
		FROM #tempTuTuat WHERE  Child=2 or Child=1 ) detail
		where #tempTuTuat.STenCanBo=N'TC Tử tuất'
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
	 DROP TABLE #tempDetailHuuTriSum
	 DROP TABLE #tempDetailPhucVienSum
	 DROP TABLE #TempDetailTuTuatSum
	 DROP TABLE #tempDetailThoiViecSum
	 DROP TABLE #tempDetailXuatNguSum

	 DROP TABLE #tempDetailHuuTriHachToan
	 DROP TABLE #tempDetailPhucVienHachToan
	 DROP TABLE #TempDetailTuTuatHachToan
	 DROP TABLE #tempDetailThoiViecHachToan
	 DROP TABLE #tempDetailXuatNguHachToan
	 DROP TABLE #tempDetailHuuTriHachToanSum
	 DROP TABLE #tempDetailPhucVienHachToanSum
	 DROP TABLE #TempDetailTuTuatHachToanSum
	 DROP TABLE #tempDetailThoiViecHachToanSum
	 DROP TABLE #tempDetailXuatNguHachToanSum

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
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Nhomdt_Detail]    Script Date: 8/22/2024 5:26:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Nhomdt_Detail]
	@INamLamViec int,
	@IdMaDonVi nvarchar(MAX),
	@IQuy int,
	@Donvitinh int
AS
BEGIN
		SELECT gt.*,dv.sTenDonVi INTO #tblTroCap FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gt
		left join DonVi dv on dv.iID_MaDonVi=gt.iiD_MaDonVi
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

			SELECT 1 bHangCha
			, N'(I)' STT
			, N'Khối dự toán'as STenCanBo 
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
			, 0 Child
			into #tblDuToan

			SELECT 1 bHangCha
			, N'(I)' STT
			, N'Khối hạch toán'as STenCanBo 
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
			, 0 Child
			into #tblHachToan

	--- 9010001-010-011-0004 Tro cap Huu tri du toan
		SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
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
			into #tempDetailHuuTriSum
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

	--- 9010001-010-011-0004 Tro cap Huu tri Hach toan
		SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
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
			into #tempDetailHuuTriHachToanSum
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010002-010-011-0004%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

	
	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo  
			, tbltc. sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,sum(FTienTroCap1Lan) FTienTroCap1Lan
			,sum(FTienTroCapKV) FTienTroCapKV
			,sum(FTienTroCapMT) FTienTroCapMT
			,sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			,sum(FTienTroCapKVTL) FTienTroCapKVTL
			,sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			, 0 Type
			, null IsParent
			, 1 Child
			INTO #TempDetailHuuTri
			from  #TempDetailHuuTriSum tbltc
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
			,tbltc.sTenCanBo  
			, tbltc. sTenPhanHo
			,tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh

		SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo  
			, tbltc. sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			,sum(FTienTroCapKVTL) FTienTroCapKVTL
			,sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			, 0 Type
			, null IsParent
			, 2 Child
			INTO #tempDetailHuuTriHachToan
			from  #tempDetailHuuTriHachToanSum tbltc
			GROUP BY  tbltc.sMa_Hieu_Can_Bo
			,tbltc.sTenCanBo  
			, tbltc. sTenPhanHo
			,tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh



	SELECT * INTO #tempHuuTri FROM
	(
	SELECT 1 bHangCha
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
			, 2 Type
			, null IsParent
			, 0 Child

	UNION ALL
	SELECT * FROM #tblDuToan
	UNION ALL
	SELECT * FROM #tempDetailHuuTri	
	UNION ALL
	SELECT * FROM #tblHachToan
	UNION ALL
	SELECT * FROM #tempDetailHuuTriHachToan	
	) tblHuutri

	 if (SELECT COUNT(1) FROM #tempHuuTri) > 2
		UPDATE #tempHuuTri set bHasData = 1
		--- update Khối dự toán
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
		FROM #tempHuuTri WHERE  Child=1 ) detail
		where #tempHuuTri.STenCanBo=N'Khối dự toán'

		--- update Khối hach toan
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
		FROM #tempHuuTri WHERE  Child=2 ) detail
		where #tempHuuTri.STenCanBo=N'Khối hạch toán'

		--- update TC  Huu tri
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
		FROM #tempHuuTri WHERE  Child=2 or Child=1 ) detail
		where #tempHuuTri.STenCanBo=N'TC Hưu Trí'

	--- 9010001-010-011-0005 Tro cap phuc vien Du toan
	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
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
			into #tempDetailPhucVienSum
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0005%' 
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac	
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo  
			, tbltc. sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			,sum(FTienTroCapKVTL) FTienTroCapKVTL
			,sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			, 0 Type
			, null IsParent
			, 1 Child
			INTO #tempDetailPhucVien
			from  #tempDetailPhucVienSum tbltc
			GROUP BY  tbltc.sMa_Hieu_Can_Bo,tbltc.sTenCanBo  
			, tbltc. sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			ORDER BY tbltc.sTenCanBo ASC


--- 9010001-010-011-0005 Tro cap phuc vien hach toan
	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
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
			into #tempDetailPhucVienHachToanSum
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE  tbltc.sXauNoiMa like '9010002-010-011-0005%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac	
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo  
			, tbltc. sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			,sum(FTienTroCapKVTL) FTienTroCapKVTL
			,sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			, 0 Type
			, null IsParent
			, 2 Child
			INTO #tempDetailPhucVienHachToan
			from  #tempDetailPhucVienHachToanSum tbltc
			GROUP BY  tbltc.sMa_Hieu_Can_Bo,tbltc.sTenCanBo  
			, tbltc. sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			ORDER BY tbltc.sTenCanBo ASC


	SELECT * INTO #tempPhucVien FROM
	(
	SELECT 1 bHangCha
			, N'(II)' STT
			, N'TC Phục viên'as STenCanBo 
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
			, null IsParent
			, 0 Child
	UNION ALL
	SELECT * FROM #tblDuToan
	UNION ALL
	SELECT * FROM #tempDetailPhucVien
	UNION ALL 
	SELECT * FROM #tblHachToan
	UNION ALL 
	SELECT * FROM #tempDetailPhucVienHachToan
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
		FROM #tempPhucVien WHERE  Child=1 ) detail
		where #tempPhucVien.STenCanBo=N'Khối dự toán'

		--- update Khối hach toan
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
		FROM #tempPhucVien WHERE  Child=2 ) detail
		where #tempPhucVien.STenCanBo=N'Khối hạch toán'

		--- update  Phục viên
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
		FROM #tempPhucVien WHERE  Child=2 or Child=1 ) detail
		where #tempPhucVien.STenCanBo=N'TC Phục viên'


	--- 9010001-010-011-0006 Tro cap xuat ngu du toan

	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS  FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			INTO #tempDetailXuatNguSum
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0006%' 
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac	
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, '' SMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			, sum(FTienTroCapKVTL) FTienTroCapKVTL
			, sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 Child
			INTO #tempDetailXuatNgu
			from  #tempDetailXuatNguSum tbltc
			GROUP BY 
					 tbltc.sMa_Hieu_Can_Bo
					, tbltc.sTenCanBo
					, tbltc.sTenPhanHo 
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh


	--- 9010001-010-011-0006 Tro cap xuat ngu hạch toán

	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS  FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			INTO #tempDetailXuatNguHachToanSum
	FROM  #tblTroCap tbltc
	WHERE  tbltc.sXauNoiMa like '9010002-010-011-0006%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac	
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, '' SMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			, sum(FTienTroCapKVTL) FTienTroCapKVTL
			, sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			, 0 Type
			, null IsParent
			, 2 Child
			INTO #tempDetailXuatNguHachToan
			from  #tempDetailXuatNguHachToanSum tbltc
			GROUP BY 
					 tbltc.sMa_Hieu_Can_Bo
					, tbltc.sTenCanBo
					, tbltc.sTenPhanHo 
					, tbltc.sSoQuyetDinh
					, tbltc.dNgayQuyetDinh

	SELECT * INTO #tempXuatNgu FROM
	(
	SELECT 1 bHangCha
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
			, 2 Type
			, null IsParent
			, 0 Child
	UNION ALL
	SELECT * FROM #tblDuToan
	UNION ALL 
	SELECT * FROM #tempDetailXuatNgu
	UNION ALL 
	SELECT * FROM #tblHachToan
	UNION ALL 
	SELECT * FROM #tempDetailXuatNguHachToan
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
		FROM #tempXuatNgu WHERE  Child=1 ) detail
		where #tempXuatNgu.STenCanBo=N'Khối dự toán'

		--- update Khối hach toan
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
		FROM #tempXuatNgu WHERE  Child=2 ) detail
		where #tempXuatNgu.STenCanBo=N'Khối hạch toán'

		--- update  TC Xuất ngũ
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
		FROM #tempXuatNgu WHERE  Child=2 or Child=1 ) detail
		where #tempXuatNgu.STenCanBo=N'TC Xuất ngũ'

	--- 9010001-010-011-0007 tro cap thoi viec du toan

		SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
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
			INTO #tempDetailThoiViecSum
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0007%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC


	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo  
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			,sum(FTienTroCapKVTL) FTienTroCapKVTL
			,sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			, 0 Type
			, null IsParent
			, 1 Child
			into #tempDetailThoiViec
			from  #tempDetailThoiViecSum tbltc
			GROUP BY   tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo 
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
		ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

	--- 9010001-010-011-0007 tro cap thoi viec hach toan
		SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
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
			INTO #tempDetailThoiViecHachToanSum
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010002-010-011-0007%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo  
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			,sum(FTienTroCapKVTL) FTienTroCapKVTL
			,sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			, 0 Type
			, null IsParent
			, 2 Child
			into #tempDetailThoiViecHachToan
			from  #tempDetailThoiViecHachToanSum tbltc
			GROUP BY   tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo 
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
		ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

	SELECT * INTO #tempThoiViec FROM
	(
	SELECT 1 bHangCha
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
			, 2 Type
			, null IsParent
			, 0 Child
	UNION ALL
	SELECT * FROM #tblDuToan
	UNION ALL
	SELECT * FROM #tempDetailThoiViec
	UNION ALL
	SELECT * FROM #tblHachToan
	UNION ALL 
	SELECT * FROM #tempDetailThoiViecHachToan
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
		FROM #tempThoiViec WHERE  Child=1 ) detail
		where #tempThoiViec.STenCanBo=N'Khối dự toán'

		--- update Khối hach toan
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
		FROM #tempThoiViec WHERE  Child=2 ) detail
		where #tempThoiViec.STenCanBo=N'Khối hạch toán'

		--- update  TC Thôi việc
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
		FROM #tempThoiViec WHERE  Child=2 or Child=1 ) detail
		where #tempThoiViec.STenCanBo=N'TC Thôi việc'

	--- 9010001-010-011-0008 tro cap tu tuat du toan

	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			 , tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
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
			INTO #tempDetailTuTuatSum
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0008%' 
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC


	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo  
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			,sum(FTienTroCapKVTL) FTienTroCapKVTL
			,sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			, 0 Type
			, null IsParent
			, 1 Child
			into #tempDetailTuTuat
			from  #tempDetailTuTuatSum tbltc
			GROUP BY   tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo 
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

--- 9010001-010-011-0008 tro cap tu tuat hach toan
	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			 , tbltc.sTenDonVi sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sMa_Hieu_Can_Bo
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
			INTO #tempDetailTuTuatHachToanSum
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010002-010-011-0008%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenDonVi
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC


	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo  
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, sum(FTienTroCap1Lan) FTienTroCap1Lan
			, sum(FTienTroCapKV) FTienTroCapKV
			, sum(FTienTroCapMT) FTienTroCapMT
			, sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			,sum(FTienTroCapKVTL) FTienTroCapKVTL
			,sum(FTienTroCapMTTL) FTienTroCapMTTL
			, 0 bHasData 
			, 0 Type
			, null IsParent
			, 2 Child
			into #tempDetailTuTuatHachToan
			from  #tempDetailTuTuatHachToanSum tbltc
			GROUP BY   tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo 
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC

	SELECT * INTO #tempTuTuat FROM
	(
	SELECT 1 bHangCha
			, N'(V)' STT
			, N'TC Tử tuất'as STenCanBo 
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
			, null IsParent
			, 0 Child
	UNION ALL
	SELECT * FROM #tblDuToan
	UNION ALL
	SELECT * FROM #tempDetailTuTuat
	UNION ALL
	SELECT  * FROM #tblHachToan
	UNION ALL 
	SELECT * FROM #tempDetailTuTuatHachToan
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
		FROM #tempTuTuat WHERE  Child=1 ) detail
		where #tempTuTuat.STenCanBo=N'Khối dự toán'

		--- update Khối hach toan
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
		FROM #tempTuTuat WHERE  Child=2 ) detail
		where #tempTuTuat.STenCanBo=N'Khối hạch toán'

		--- update  TC Tử tuất
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
		FROM #tempTuTuat WHERE  Child=2 or Child=1 ) detail
		where #tempTuTuat.STenCanBo=N'TC Tử tuất'
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
	 DROP TABLE #tempDetailHuuTriSum
	 DROP TABLE #tempDetailPhucVienSum
	 DROP TABLE #TempDetailTuTuatSum
	 DROP TABLE #tempDetailThoiViecSum
	 DROP TABLE #tempDetailXuatNguSum

	 DROP TABLE #tempDetailHuuTriHachToan
	 DROP TABLE #tempDetailPhucVienHachToan
	 DROP TABLE #TempDetailTuTuatHachToan
	 DROP TABLE #tempDetailThoiViecHachToan
	 DROP TABLE #tempDetailXuatNguHachToan
	 DROP TABLE #tempDetailHuuTriHachToanSum
	 DROP TABLE #tempDetailPhucVienHachToanSum
	 DROP TABLE #TempDetailTuTuatHachToanSum
	 DROP TABLE #tempDetailThoiViecHachToanSum
	 DROP TABLE #tempDetailXuatNguHachToanSum

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
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh]    Script Date: 8/22/2024 5:26:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh]
@MaDonVi NVARCHAR(max),
@Thang int,
@Nam AS int,
@IsOrderChucVu AS bit = 0,
@IsGiaTriAm AS bit = 0,
@IsCheckedMaHuongLuong AS bit = 0,
@IsNew AS bit = 0
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;

DECLARE @Cols AS NVARCHAR(MAX)
DECLARE @Query AS NVARCHAR(MAX)
DECLARE @ThangTruoc AS int
DECLARE @NamTruoc AS int

IF @Thang = 1 
BEGIN
	SET @ThangTruoc = 12;
	SET @NamTruoc = @Nam - 1;
END
ELSE 
BEGIN
	SET @ThangTruoc =  @Thang - 1;
	SET @NamTruoc = @Nam;
END

SET @Cols = 'PCNU_TT,PCCOV_HS,NTN,LHT_HS,PCTNVK_HS,HSBL_HS,LHT_TT,PCTNVK_TT,HSBL_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCKVCS_TT,PCTRA_SUM,PCCOV_TT,PCDACTHU_SUM,PCKHAC_SUM,LUONGTHANG_SUM,BHCN_TT,THUETNCN_TT,TA_TT,GTKHAC_TT,TRICHLUONG_TT,PHAITRU_SUM,TM,THANHTIEN,BHXHCN_TT,BHYTCN_TT,BHTNCN_TT,PCTAUBP_TT,PCBVBG_TT,PCTEMTHU_TT,TA_TONG,PHAITRUKHAC_SUM'
SET @Query =
'
WITH CanBoThangTruoc as (
			Select canbo.Ma_Hieu_CanBo, canbo.Parent MaDonViCu, canbo.Ten_DonVi TenDonViCu, canbo.Ma_CB CapBacCu
			From TL_DM_CanBo canbo
			Where 
				canbo.Thang = ' + CAST(@ThangTruoc AS VARCHAR(2)) + '
				And canbo.Nam = ' + CAST(@NamTruoc AS VARCHAR(4)) + '
				AND canbo.Parent IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		),
blt AS (
	SELECT * FROM TL_BangLuong_Thang
	WHERE THANG = ' + CAST(@Thang AS VARCHAR(2)) + ' 
	AND NAM = ' + CAST(@Nam AS VARCHAR(4)) + ' 
	AND Ma_DonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
	AND ( ' + CAST(@IsNew AS VARCHAR(2)) + '= 0 OR (Ma_Hieu_CanBo NOT IN (SELECT Ma_Hieu_CanBo FROM CanBoThangTruoc WHERE MaDonViCu = Ma_DonVi)))
),
BangLuongBHXH AS (
	select distinct base.sMaCbo, base.sTenCbo, base.sMaDonVi, base.iNam, base.iThang,
		LHT_TT.GiaTri_LHT_TT,
		PCCV_TT.GiaTri_PCCVBH_TT,
		PCTN_TT.GiaTri_PCTNBH_TT,
		BHXHCN_TT.GiaTri_BHXHCN_TT,
		BHYTCN_TT.GiaTri_BHYTCN_TT,
		PCTNVK_TT.GiaTri_PCTNVKBH_TT,
		HSBL_TT.GiaTri_HSBLBH_TT
	from TL_BangLuong_ThangBHXH base
	left join (
	select LHT_TT.sMaCbo, LHT_TT.sTenCbo, LHT_TT.sMaDonVi, LHT_TT.iNam, LHT_TT.iThang, sum(nGiaTri) GiaTri_LHT_TT
	from TL_BangLuong_ThangBHXH LHT_TT
	where LHT_TT.iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND LHT_TT.iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND LHT_TT.sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND LHT_TT.sMaCheDo in (''OK_D14N_LBH_TT'', ''OK_T14N_LBH_TT'', ''BDN_D14N_LBH_TT'', ''BDN_T14N_LBH_TT'', ''CONOM_LBH_TT'', ''KT_LBH_TT'', ''NAMNGHIVIEC_LBH_TT'', ''KHHGD_LBH_TT'')
	group by LHT_TT.sMaCbo, LHT_TT.sTenCbo, LHT_TT.sMaDonVi, LHT_TT.iNam, LHT_TT.iThang) LHT_TT on base.sMaCbo = LHT_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCCVBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_PCCVBH_TT'', ''OK_T14N_PCCVBH_TT'', ''BDN_D14N_PCCVBH_TT'', ''BDN_T14N_PCCVBH_TT'', ''CONOM_PCCVBH_TT'', ''KT_PCCVBH_TT'', ''NAMNGHIVIEC_PCCVBH_TT'', ''KHHGD_PCCVBH_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCCV_TT on base.sMaCbo = PCCV_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCTNBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_PCTNBH_TT'', ''OK_T14N_PCTNBH_TT'', ''BDN_D14N_PCTNBH_TT'', ''BDN_T14N_PCTNBH_TT'', ''CONOM_PCTNBH_TT'', ''KT_PCTNBH_TT'', ''NAMNGHIVIEC_PCTNBH_TT'', ''KHHGD_PCTNBH_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCTN_TT on base.sMaCbo = PCTN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_BHXHCN_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_BHXHCN_TT'', ''OK_T14N_BHXHCN_TT'', ''BDN_D14N_BHXHCN_TT'', ''BDN_T14N_BHXHCN_TT'', ''CONOM_BHXHCN_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) BHXHCN_TT on base.sMaCbo = BHXHCN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_BHYTCN_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''BDN_D14N_BHYTCN_TT'', ''OK_D14N_BHYTCN_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) BHYTCN_TT on base.sMaCbo = BHYTCN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCTNVKBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_PCTNVKBH_TT'', ''OK_T14N_PCTNVKBH_TT'', ''BDN_D14N_PCTNVKBH_TT'', ''BDN_T14N_PCTNVKBH_TT'', ''CONOM_PCTNVKBH_TT'', ''KT_PCTNVKBH_TT'', ''NAMNGHIVIEC_PCTNVKBH_TT'', ''KHHGD_PCTNVKBH_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCTNVK_TT on base.sMaCbo = PCTNVK_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_HSBLBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_HSBLBH_TT'', ''OK_T14N_HSBLBH_TT'', ''BDN_D14N_HSBLBH_TT'', ''BDN_T14N_HSBLBH_TT'', ''CONOM_HSBLBH_TT'', ''KT_HSBLBH_TT'', ''NAMNGHIVIEC_HSBLBH_TT'', ''KHHGD_HSBLBH_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) HSBL_TT on base.sMaCbo = HSBL_TT.sMaCbo

	where base.iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND base.iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND base.sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
),
BangLuongThang AS (
SELECT Thang, Nam, MaCanBo, ' + @Cols + ' FROM (
SELECT
dsCapNhapBangLuong.Thang AS Thang,
dsCapNhapBangLuong.Nam AS Nam,
bangLuong.Ma_CBo AS MaCanBo,
bangLuong.MA_PHUCAP AS MaPhuCap,
CASE
	WHEN bangLuong.Ma_PhuCap = ''LHT_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_LHT_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''PCCV_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_PCCVBH_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''PCTN_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_PCTNBH_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''BHXHCN_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_BHXHCN_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''BHYTCN_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_BHYTCN_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''PCTNVK_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_PCTNVKBH_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''HSBL_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_HSBLBH_TT, 0)
	ELSE bangLuong.Gia_Tri
END AS GiaTri
FROM blt bangLuong
JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong
ON bangLuong.parent = dsCapNhapBangLuong.Id
LEFT JOIN BangLuongBHXH bhxh ON bangLuong.Ma_CBo = bhxh.sMaCbo AND bangLuong.Ma_DonVi = bhxh.sMaDonVi AND bangLuong.NAM = bhxh.iNam AND bangLuong.THANG = bhxh.iThang
WHERE
bangLuong.Ma_PhuCap IN (SELECT * FROM f_split(''' + @Cols + '''))
AND dsCapNhapBangLuong.Ma_CachTL=''CACH0''
AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
AND dsCapNhapBangLuong.Status=1
) x
PIVOT
(
SUM(GiaTri)
FOR MaPhuCap IN (' + @Cols + ')
) pvt
), ThongTinCanBo AS (
SELECT
donVi.Ma_DonVi AS MaDonVi,
donVi.Ten_Donvi AS TenDonvi,
canBo.Ma_CanBo AS MaCanBo,
canBo.Ten_CanBo AS TenCanBo,
dbo.f_split_empty(canbo.Ten_CanBo) AS Ten,
canBo.Thang_TNN AS Tnn,
canBo.Nam_TN AS NTN,
dbo.f_luong_ntn(canBo.Ngay_NN, canBo.Ngay_XN, canBo.Ngay_TN, canBo.Thang_TNN, '+ CAST(@Thang AS VARCHAR(2)) +', '+ CAST(@Nam AS VARCHAR(4)) +') AS NamTn,
ISNULL(canBo.Ma_CB, ''0'') AS MaCapBac,
ISNULL(capBac.Ten_Cb, ''0'') AS CapBac,
ISNULL(chucVu.HeSo_Cv, 0) AS HSChucVu,
chucVu.Ma_Cv AS MaChucVu,
chucVu.Ten_Cv AS TenChucVu,
CASE WHEN 1 = ' + CAST(@IsCheckedMaHuongLuong AS VARCHAR(10)) + ' THEN CONCAT(canBo.So_TaiKhoan, '' '', canBo.Ma_CanBo) ELSE canBo.So_TaiKhoan END AS Stk,
ISNULL(FORMAT(canBo.Ngay_NN,''MM/yy''), '''') AS NgayNhapNgu,
ISNULL(FORMAT(canBo.Ngay_XN,''MM/yy''), '''') AS NgayXuatNgu,
ISNULL(FORMAT(canBo.Ngay_TN,''MM/yy''), '''') AS NgayTaiNgu,
capBac.XauNoiMa
FROM TL_DM_CanBo canBo
INNER JOIN TL_DM_DonVi donVi
ON canBo.Parent=donVi.Ma_DonVi
LEFT JOIN TL_DM_CapBac capBac
ON canBo.Ma_CB=capBac.Ma_Cb
LEFT JOIN TL_DM_ChucVu chucVu
ON canBo.Ma_CV=chucVu.Ma_Cv
WHERE
(canBo.IsDelete = 1 or (canbo.Ma_TangGiam = ''320'' and month(canbo.Ngay_XN) = '+ CAST(@Thang AS VARCHAR(2)) +' and year(canbo.Ngay_XN) = '+ CAST(@Nam AS VARCHAR(4)) +'))
AND canBo.Khong_Luong = 0
AND canBo.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
AND canBo.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
)

SELECT
bangLuong.*,
canBo.MaDonVi,
canBo.TenDonVi,
canBo.TenCanBo,
canBo.Ten,
canBo.MaCapBac,
canBo.CapBac,
canBo.HSChucVu,
canBo.MaChucVu,
canBo.TenChucVu,
canBo.Stk,
canBo.NgayNhapNgu,
canBo.NgayXuatNgu,
canBo.NgayTaiNgu,
canBo.NamTN,
canBo.Tnn,
--canBo.NTN,
canBo.XauNoiMa
into tbl_luong_bhxh
FROM BangLuongThang bangLuong
INNER JOIN ThongTinCanBo canBo
ON bangLuong.MaCanBo = canBo.MaCanBo'
If @IsGiaTriAm = 1
SET @Query = @Query + ' WHERE bangLuong.THANHTIEN < 0';
IF @IsOrderChucVu = 1
SET @Query = @Query +' ORDER BY HSChucVu DESC, MaCapBac DESC, Ten ASC';
ELSE
SET @Query = @Query +' ORDER BY MaCapBac DESC, Ten ASC';
execute(@Query)

UPDATE tbl_luong_bhxh SET 
PCCOV_TT = (isnull(LHT_TT, 0) + isnull(PCCV_TT, 0) + isnull(PCTNVK_TT, 0) + isnull(HSBL_TT, 0)) * isnull(PCCOV_HS, 0)

UPDATE tbl_luong_bhxh SET
LUONGTHANG_SUM = isnull(LHT_TT, 0) + isnull(PCTN_TT, 0) + isnull(PCTNVK_TT, 0) + isnull(PCCV_TT, 0) + isnull(PCCOV_TT, 0) + isnull(PCTRA_SUM, 0) + isnull(PCDACTHU_SUM, 0) + isnull(PCKHAC_SUM, 0) + isnull(HSBL_TT, 0) + isnull(PCKV_TT, 0) + isnull(PCBVBG_TT, 0) + isnull(PCNU_TT, 0)

-- Update so phai tru BHXH, BHYT, BHTN khi hưởng chế dộ khác BENHDAINGAY_TD14NGAY và OMKHAC_D14NGAY
UPDATE tbl_luong_bhxh SET BHXHCN_TT = 0, BHYTCN_TT = 0, BHTNCN_TT = 0
WHERE MaCanBo in (
select sMaCBo from TL_BangLuong_ThangBHXH
where iNam = @Nam
and iThang = @Thang
and sMaCheDo in ('BENHDAINGAY_T14NGAY', 'OMKHAC_T14NGAY', 'CONOM', 'OMDAU_DUONGSUCPHSK', 'TAINANLD_DUONGSUCPHSK', 'KHAMTHAI', 'KHHGD', 'NAMNGHIKHIVOSINHCON', 'THAISAN_DUONGSUCPHSK'))

UPDATE tbl_luong_bhxh SET
BHCN_TT = isnull(BHXHCN_TT, 0) + isnull(BHYTCN_TT, 0) + isnull(BHTNCN_TT, 0)

UPDATE tbl_luong_bhxh SET
PHAITRU_SUM = isnull(BHXHCN_TT, 0) + isnull(BHYTCN_TT, 0) + isnull(BHTNCN_TT, 0) + isnull(THUETNCN_TT, 0) + isnull(TA_TONG, 0) + isnull(PHAITRUKHAC_SUM, 0)

UPDATE tbl_luong_bhxh SET
THANHTIEN = isnull(LUONGTHANG_SUM, 0) - isnull(PHAITRU_SUM, 0)


IF @IsOrderChucVu = 1
select * from tbl_luong_bhxh
ORDER BY HSChucVu DESC, MaCapBac DESC, Ten ASC
else
select * from tbl_luong_bhxh
ORDER BY MaCapBac DESC, Ten ASC

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_luong_bhxh]') AND type in (N'U')) drop table tbl_luong_bhxh;

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
GO

Delete DM_ChuKy
where Id_Code='rptBH_ThongTriTongHop_Thu_All_TongHopChung'

INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) VALUES (N'52f21824-7a86-4fd7-bde5-f320133c66be', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rptBH_ThongTriTongHop_Thu_All_TongHopChung', NULL, N'rptBH_ThongTriTongHop_Thu_All_TongHopChung', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Thu bảo hiểm xã hội, bảo hiểm y tế, bảo hiểm thất nghiệp', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'3', N'', N'', NULL, NULL, NULL, NULL, NULL, NULL)
GO