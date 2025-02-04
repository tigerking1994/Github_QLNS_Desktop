
update TL_DM_CheDoBHXH 
set sLoaiTruyLinh = 'TUTUAT_TROCAPKHUVUC'
where sMaCheDo = 'TUTUAT_TROCAPKHUVUC_TRUYLINH'

update TL_DM_CheDoBHXH 
set sLoaiTruyLinh = 'TROCAPTHEOPHIEUTRUYTRA'
where sMaCheDo = 'TROCAPTHEOPHIEUTRUYTRA_TRUYLINH'

update TL_DM_CheDoBHXH 
set sLoaiTruyLinh = 'HOTRO_CDNN'
where sMaCheDo = 'HOTRO_CDNN_TRUYLINH'

update TL_DM_CheDoBHXH 
set sLoaiTruyLinh = 'TROCAPCHETDOTNLD'
where sMaCheDo = 'TROCAPCHETDOTNLD_TRUYLINH'

update TL_DM_CheDoBHXH 
set sLoaiTruyLinh = 'THOIVIEC_TROCAP1LAN'
where sMaCheDo = 'THOIVIEC_TROCAP1LAN_TRUYLINH'

update TL_DM_CheDoBHXH 
set sLoaiTruyLinh = 'TROCAPMAITANG'
where sMaCheDo = 'TROCAPMAITANG_TRUYLINH'

update TL_DM_CheDoBHXH 
set sLoaiTruyLinh = 'TROCAPPHCN'
where sMaCheDo = 'TROCAPPHCN_TRUYLINH'

update TL_DM_CheDoBHXH 
set sLoaiTruyLinh = 'THOIVIEC_TROCAPKHUVUC'
where sMaCheDo = 'THOIVIEC_TROCAPKHUVUC_TRUYLINH'

update TL_DM_CheDoBHXH 
set sLoaiTruyLinh = 'HUUTRI_TROCAP1LAN'
where sMaCheDo = 'HUUTRI_TROCAP1LAN_TRUYLINH'

update TL_DM_CheDoBHXH 
set sLoaiTruyLinh = 'TAINANLD_TROCAP1LAN'
where sMaCheDo = 'TAINANLD_TROCAP1LAN_TRUYLINH'

update TL_DM_CheDoBHXH 
set sLoaiTruyLinh = 'CHIGIAMDINH'
where sMaCheDo = 'CHIGIAMDINH_TRUYLINH'

update TL_DM_CheDoBHXH 
set sLoaiTruyLinh = 'TAINANLD_DUONGSUCPHSK'
where sMaCheDo = 'TAINANLD_DUONGSUCPHSK_TRUYLINH'

update TL_DM_CheDoBHXH 
set sLoaiTruyLinh = 'HOTRO_PHONGNGUA'
where sMaCheDo = 'HOTRO_PHONGNGUA_TRUYLINH'

update TL_DM_CheDoBHXH 
set sLoaiTruyLinh = 'HUUTRI_TROCAPKHUVUC'
where sMaCheDo = 'HUUTRI_TROCAPKHUVUC_TRUYLINH'


update TL_DM_CheDoBHXH 
set sLoaiTruyLinh = 'TROCAPHANGTHANG'
where sMaCheDo = 'TROCAPHANGTHANG_TRUYLINH'


update TL_DM_CheDoBHXH 
set sLoaiTruyLinh = 'TROCAPPHUCVU'
where sMaCheDo = 'TROCAPPHUCVU_TRUYLINH'

update TL_DM_CheDoBHXH 
set sLoaiTruyLinh = 'TUTUAT_TROCAP1LAN'
where sMaCheDo = 'TUTUAT_TROCAP1LAN_TRUYLINH'




/****** Object:  StoredProcedure [dbo].[sp_bh_qtc_get_canbo_salary_by_xaunoima_bhxh]    Script Date: 8/6/2024 1:53:04 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtc_get_canbo_salary_by_xaunoima_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtc_get_canbo_salary_by_xaunoima_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtc_get_canbo_salary_by_xaunoima_bhxh]    Script Date: 8/6/2024 1:53:04 PM ******/
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
		INNER JOIN #temp2 t2 ON tbl.sMaCBo = t2.sMaCBo AND tbl.sMaDonVi = t2.sMaDonVi AND t2.iThang = tbl.iThang AND tbl.iNam = t2.iNam 
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
GO



