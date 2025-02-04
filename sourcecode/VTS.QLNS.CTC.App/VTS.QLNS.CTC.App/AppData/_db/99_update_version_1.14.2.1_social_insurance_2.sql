/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_xuat_ngu]    Script Date: 3/26/2024 2:51:36 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_luong_tro_cap_xuat_ngu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_xuat_ngu]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_tnld]    Script Date: 3/26/2024 2:51:36 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_luong_tro_cap_tnld]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_tnld]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_thai_san]    Script Date: 3/26/2024 2:51:36 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_luong_tro_cap_thai_san]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_thai_san]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_om_dau]    Script Date: 3/26/2024 2:51:36 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_luong_tro_cap_om_dau]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_om_dau]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_ht_pv_tv_tt]    Script Date: 3/26/2024 2:51:36 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_luong_tro_cap_ht_pv_tv_tt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_ht_pv_tv_tt]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtc_get_canbo_salary_by_xaunoima_bhxh]    Script Date: 3/26/2024 2:51:36 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtc_get_canbo_salary_by_xaunoima_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtc_get_canbo_salary_by_xaunoima_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtc_get_canbo_salary_by_xaunoima_bhxh]    Script Date: 3/26/2024 2:51:36 PM ******/
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
	@sQuarter nvarchar(50),
	@Donvi  nvarchar(50),
	@NamLamViec int
AS
BEGIN
	DECLARE @STongHop nvarchar(max);	
	select top(1) bangluong.STongHop into #temp1  FROM TL_BangLuong_ThangBHXH bangluongchitiet 
			JOIN  TL_DS_CapNhap_BangLuong bangluong on bangluongchitiet.iID_Parent = bangluong.Id
			where
				bangluongchitiet.iThang IN (SELECT * FROM splitstring(@sQuarter))
					AND bangluongchitiet.sMaCheDo IN (SELECT sMaCheDo 
							FROM TL_DM_CheDoBHXH
							WHERE bDisplay = 1 
								AND sXauNoiMaMlnsBHXH = @sXauNoiMa )
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
			and bangluong.Ma_CachTL = N'CACH2'
			and bangluong.Ma_CBo = @Donvi
			and bangluong.KhoaBangLuong = 1
			and bangluong.Nam = @NamLamViec
			and bangluong.STongHop is not null

	set @STongHop = (select #temp1.STongHop from #temp1);

	select * into #tblSTongHop from splitstring(@STongHop);

	-- Get bang luong chi tiet 
	select * into #temp2 from TL_BangLuong_ThangBHXH bangluongchitiet  where bangluongchitiet.sMaDonVi in (select * from #tblSTongHop)
	and bangluongchitiet.iThang IN (SELECT * FROM splitstring(@sQuarter))
		AND bangluongchitiet.sMaCheDo IN (SELECT sMaCheDo 
										FROM TL_DM_CheDoBHXH
										WHERE bDisplay = 1 
										AND sXauNoiMaMlnsBHXH = @sXauNoiMa )
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
		canbo.dTuNgay AS DTuNgay,
		canbo.dDenNgay AS DDenNgay,
		bangluongchitiet.sMaCB
	FROM #temp2 bangluongchitiet 
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


GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_ht_pv_tv_tt]    Script Date: 3/26/2024 2:51:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_ht_pv_tv_tt]
	@DsMaDonVi nvarchar(1000),
	@NamLamViec int,
	@Thang int,
	@DonViTinh int
AS
BEGIN
	--Lay thong tin luong theo TC_HUUTRI, TC_PHUCVIEN, TC_THOIVIEC, TC_TUTUAT
	select * into TBL_HUUTRI from
	(select donvi.Ma_DonVi ,donvi.Ten_DonVi, luong.*
	from TL_BangLuong_ThangBHXH luong
	join TL_DM_DonVi donvi on luong.sMaDonVi = donvi.Ma_DonVi
	where 
	luong.sMaDonVi in (SELECT * FROM f_split(@DsMaDonVi))
	and luong.iNam = @NamLamViec
	and luong.iThang = @Thang
	and luong.sMaCheDo in ('HUUTRI_TROCAP1LAN', 'HUUTRI_TROCAPKHUVUC', 'PHUCVIEN_TROCAP1LAN', 'PHUCVIEN_TROCAPKHUVUC', 'THOIVIEC_TROCAP1LAN', 'THOIVIEC_TROCAPKHUVUC', 'TUTUAT_TROCAP1LAN', 'TUTUAT_TROCAPKHUVUC', 'TROCAPMAITANG','TUTUAT_TROCAP1LAN_TRUYLINH','TUTUAT_TROCAPKHUVUC_TRUYLINH','TROCAPMAITANG_TRUYLINH')) HUUTRI


	-- Data tro cap Huu tri
	select distinct
		TBL_HUUTRI.sMaCB,
		TBL_HUUTRI.sMaCBo,
		--TBL_HUUTRI.sMaCheDo,
		TBL_HUUTRI.sTenCbo,
		TBL_HUUTRI.Ma_DonVi,
		TBL_HUUTRI.Ten_DonVi,
		HUUTRI_TROCAP1LAN.nGiaTri fHUUTRI_TROCAP1LAN,
		HUUTRI_TROCAPKHUVUC.nGiaTri fHUUTRI_TROCAPKHUVUC,
		chedocha.sSoQuyetDinh,
		chedocha.dNgayQuyetDinh
		into TBL_HUUTRI_DOC
	from TBL_HUUTRI TBL_HUUTRI
		left join (select top 1 * from TL_CanBo_CheDoBHXH 
		where sMaCheDo in ('HUUTRI_TROCAPKHUVUC', 'HUUTRI_TROCAP1LAN')
		and ((sSoQuyetDinh is not null and sSoQuyetDinh <> '') 
		or (dNgayQuyetDinh is not null and dNgayQuyetDinh <> ''))) chedocha
		on TBL_HUUTRI.sMaCBo = chedocha.sMaCanBo
		left join
		(select HUUTRI.Id, HUUTRI.sMaDonVi, HUUTRI.nGiaTri, HUUTRI.sMaCB, HUUTRI.sMaCBo, HUUTRI.sMaCheDo, HUUTRI.sTenCbo
		from TBL_HUUTRI HUUTRI
		where HUUTRI.sMaCheDo = 'HUUTRI_TROCAPKHUVUC') HUUTRI_TROCAPKHUVUC
		on TBL_HUUTRI.sMaCBo = HUUTRI_TROCAPKHUVUC.sMaCBo and TBL_HUUTRI.sMaDonVi = HUUTRI_TROCAPKHUVUC.sMaDonVi
		left join
		(select HUUTRI2.Id, HUUTRI2.sMaDonVi, HUUTRI2.nGiaTri, HUUTRI2.sMaCB, HUUTRI2.sMaCBo, HUUTRI2.sMaCheDo, HUUTRI2.sTenCbo
		from TBL_HUUTRI HUUTRI2
		where HUUTRI2.sMaCheDo = 'HUUTRI_TROCAP1LAN') HUUTRI_TROCAP1LAN
		on TBL_HUUTRI.sMaCBo = HUUTRI_TROCAP1LAN.sMaCBo and TBL_HUUTRI.sMaDonVi = HUUTRI_TROCAP1LAN.sMaDonVi

	-- Data tro cap Phuc vien
	select distinct
		TBL_HUUTRI.sMaCB,
		TBL_HUUTRI.sMaCBo,
		--TBL_HUUTRI.sMaCheDo,
		TBL_HUUTRI.sTenCbo,
		TBL_HUUTRI.Ma_DonVi,
		TBL_HUUTRI.Ten_DonVi,
		PHUCVIEN_TROCAP1LAN.nGiaTri fPHUCVIEN_TROCAP1LAN,
		PHUCVIEN_TROCAPKHUVUC.nGiaTri fPHUCVIEN_TROCAPKHUVUC,
		chedocha.sSoQuyetDinh,
		chedocha.dNgayQuyetDinh
		into TBL_PHUCVIEN_DOC
	from TBL_HUUTRI TBL_HUUTRI
		left join (select top 1 * from TL_CanBo_CheDoBHXH 
		where sMaCheDo in ('PHUCVIEN_TROCAPKHUVUC', 'PHUCVIEN_TROCAP1LAN')
		and ((sSoQuyetDinh is not null and sSoQuyetDinh <> '') 
		or (dNgayQuyetDinh is not null and dNgayQuyetDinh <> ''))) chedocha
		on TBL_HUUTRI.sMaCBo = chedocha.sMaCanBo
		left join
		(select HUUTRI.Id, HUUTRI.sMaDonVi, HUUTRI.nGiaTri, HUUTRI.sMaCB, HUUTRI.sMaCBo, HUUTRI.sMaCheDo, HUUTRI.sTenCbo
		from TBL_HUUTRI HUUTRI
		where HUUTRI.sMaCheDo = 'PHUCVIEN_TROCAPKHUVUC') PHUCVIEN_TROCAPKHUVUC
		on TBL_HUUTRI.sMaCBo = PHUCVIEN_TROCAPKHUVUC.sMaCBo and TBL_HUUTRI.sMaDonVi = PHUCVIEN_TROCAPKHUVUC.sMaDonVi
		left join
		(select HUUTRI2.Id, HUUTRI2.sMaDonVi, HUUTRI2.nGiaTri, HUUTRI2.sMaCB, HUUTRI2.sMaCBo, HUUTRI2.sMaCheDo, HUUTRI2.sTenCbo
		from TBL_HUUTRI HUUTRI2
		where HUUTRI2.sMaCheDo = 'PHUCVIEN_TROCAP1LAN') PHUCVIEN_TROCAP1LAN
		on TBL_HUUTRI.sMaCBo = PHUCVIEN_TROCAP1LAN.sMaCBo and TBL_HUUTRI.sMaDonVi = PHUCVIEN_TROCAP1LAN.sMaDonVi

	-- Data tro cap Thoi Viec
	select distinct
		TBL_HUUTRI.sMaCB,
		TBL_HUUTRI.sMaCBo,
		--TBL_HUUTRI.sMaCheDo,
		TBL_HUUTRI.sTenCbo,
		TBL_HUUTRI.Ma_DonVi,
		TBL_HUUTRI.Ten_DonVi,
		THOIVIEC_TROCAP1LAN.nGiaTri fTHOIVIEC_TROCAP1LAN,
		THOIVIEC_TROCAPKHUVUC.nGiaTri fTHOIVIEC_TROCAPKHUVUC,
		chedocha.sSoQuyetDinh,
		chedocha.dNgayQuyetDinh
		into TBL_THOIVIEC_DOC
	from TBL_HUUTRI TBL_HUUTRI
		left join (select top 1 * from TL_CanBo_CheDoBHXH 
		where sMaCheDo in ('THOIVIEC_TROCAPKHUVUC', 'THOIVIEC_TROCAP1LAN')
		and ((sSoQuyetDinh is not null and sSoQuyetDinh <> '') 
		or (dNgayQuyetDinh is not null and dNgayQuyetDinh <> ''))) chedocha
		on TBL_HUUTRI.sMaCBo = chedocha.sMaCanBo
		left join
		(select HUUTRI.Id, HUUTRI.sMaDonVi, HUUTRI.nGiaTri, HUUTRI.sMaCB, HUUTRI.sMaCBo, HUUTRI.sMaCheDo, HUUTRI.sTenCbo
		from TBL_HUUTRI HUUTRI
		where HUUTRI.sMaCheDo = 'THOIVIEC_TROCAPKHUVUC') THOIVIEC_TROCAPKHUVUC
		on TBL_HUUTRI.sMaCBo = THOIVIEC_TROCAPKHUVUC.sMaCBo and TBL_HUUTRI.sMaDonVi = THOIVIEC_TROCAPKHUVUC.sMaDonVi
		left join
		(select HUUTRI2.Id, HUUTRI2.sMaDonVi, HUUTRI2.nGiaTri, HUUTRI2.sMaCB, HUUTRI2.sMaCBo, HUUTRI2.sMaCheDo, HUUTRI2.sTenCbo
		from TBL_HUUTRI HUUTRI2
		where HUUTRI2.sMaCheDo = 'THOIVIEC_TROCAP1LAN') THOIVIEC_TROCAP1LAN
		on TBL_HUUTRI.sMaCBo = THOIVIEC_TROCAP1LAN.sMaCBo and TBL_HUUTRI.sMaDonVi = THOIVIEC_TROCAP1LAN.sMaDonVi

	-- Data tro cap Tu tuat
	select distinct
		TBL_HUUTRI.sMaCB,
		TBL_HUUTRI.sMaCBo,
		--TBL_HUUTRI.sMaCheDo,
		TBL_HUUTRI.sTenCbo,
		TBL_HUUTRI.Ma_DonVi,
		TBL_HUUTRI.Ten_DonVi,
		TUTUAT_TROCAP1LAN.nGiaTri fTUTUAT_TROCAP1LAN,
		TUTUAT_TROCAPKHUVUC.nGiaTri fTUTUAT_TROCAPKHUVUC,
		TROCAPMAITANG.nGiaTri fTROCAPMAITANG,
		chedocha.sSoQuyetDinh,
		chedocha.dNgayQuyetDinh
		into TBL_TUTUAT_DOC
	from TBL_HUUTRI TBL_HUUTRI
		left join (select top 1 * from TL_CanBo_CheDoBHXH 
		where sMaCheDo in ('TUTUAT_TROCAPKHUVUC', 'TROCAPMAITANG', 'TUTUAT_TROCAP1LAN')
		and ((sSoQuyetDinh is not null and sSoQuyetDinh <> '') 
		or (dNgayQuyetDinh is not null and dNgayQuyetDinh <> ''))) chedocha
		on TBL_HUUTRI.sMaCBo = chedocha.sMaCanBo
		left join
		(select HUUTRI.Id, HUUTRI.sMaDonVi, HUUTRI.nGiaTri, HUUTRI.sMaCB, HUUTRI.sMaCBo, HUUTRI.sMaCheDo, HUUTRI.sTenCbo
		from TBL_HUUTRI HUUTRI
		where HUUTRI.sMaCheDo = 'TUTUAT_TROCAPKHUVUC') TUTUAT_TROCAPKHUVUC
		on TBL_HUUTRI.sMaCBo = TUTUAT_TROCAPKHUVUC.sMaCBo and TBL_HUUTRI.sMaDonVi = TUTUAT_TROCAPKHUVUC.sMaDonVi
		left join
		(select HUUTRI1.Id, HUUTRI1.sMaDonVi, HUUTRI1.nGiaTri, HUUTRI1.sMaCB, HUUTRI1.sMaCBo, HUUTRI1.sMaCheDo, HUUTRI1.sTenCbo
		from TBL_HUUTRI HUUTRI1
		where HUUTRI1.sMaCheDo = 'TROCAPMAITANG') TROCAPMAITANG
		on TBL_HUUTRI.sMaCBo = TROCAPMAITANG.sMaCBo and TBL_HUUTRI.sMaDonVi = TROCAPMAITANG.sMaDonVi
		left join
		(select HUUTRI2.Id, HUUTRI2.sMaDonVi, HUUTRI2.nGiaTri, HUUTRI2.sMaCB, HUUTRI2.sMaCBo, HUUTRI2.sMaCheDo, HUUTRI2.sTenCbo
		from TBL_HUUTRI HUUTRI2
		where HUUTRI2.sMaCheDo = 'TUTUAT_TROCAP1LAN') TUTUAT_TROCAP1LAN
		on TBL_HUUTRI.sMaCBo = TUTUAT_TROCAP1LAN.sMaCBo and TBL_HUUTRI.sMaDonVi = TUTUAT_TROCAP1LAN.sMaDonVi


   -- lAY TRUY LINH TU TUAT 1 LAN , KHU VUC
   	select distinct
		TBL_HUUTRI.sMaCB,
		TBL_HUUTRI.sMaCBo,
		--TBL_HUUTRI.sMaCheDo,
		TBL_HUUTRI.sTenCbo,
		TBL_HUUTRI.Ma_DonVi,
		TBL_HUUTRI.Ten_DonVi,
		TUTUAT_TROCAP1LAN_TRUYLINH.nGiaTri fTUTUAT_TROCAP1LAN_TRUYLINH,
		TUTUAT_TROCAPKHUVUC_TRUYLINH.nGiaTri fTUTUAT_TROCAPKHUVUC_TRUYLINH,
		TROCAPMAITANG_TRUYLINH.nGiaTri fTROCAPMAITANG_TRUYLINH,
		chedocha.sSoQuyetDinh,
		chedocha.dNgayQuyetDinh
		into TBL_TUTUAT_TRUYLINH
	from TBL_HUUTRI TBL_HUUTRI
		left join (select top 1 * from TL_CanBo_CheDoBHXH 
		where sMaCheDo in ('TUTUAT_TROCAPKHUVUC_TRUYLINH', 'TROCAPMAITANG_TRUYLINH', 'TUTUAT_TROCAP1LAN_TRUYLINH')
		and ((sSoQuyetDinh is not null and sSoQuyetDinh <> '') 
		or (dNgayQuyetDinh is not null and dNgayQuyetDinh <> ''))) chedocha
		on TBL_HUUTRI.sMaCBo = chedocha.sMaCanBo
		left join
		(select HUUTRI.Id, HUUTRI.sMaDonVi, HUUTRI.nGiaTri, HUUTRI.sMaCB, HUUTRI.sMaCBo, HUUTRI.sMaCheDo, HUUTRI.sTenCbo
		from TBL_HUUTRI HUUTRI
		where HUUTRI.sMaCheDo = 'TUTUAT_TROCAPKHUVUC_TRUYLINH') TUTUAT_TROCAPKHUVUC_TRUYLINH
		on TBL_HUUTRI.sMaCBo = TUTUAT_TROCAPKHUVUC_TRUYLINH.sMaCBo and TBL_HUUTRI.sMaDonVi = TUTUAT_TROCAPKHUVUC_TRUYLINH.sMaDonVi
		left join
		(select HUUTRI1.Id, HUUTRI1.sMaDonVi, HUUTRI1.nGiaTri, HUUTRI1.sMaCB, HUUTRI1.sMaCBo, HUUTRI1.sMaCheDo, HUUTRI1.sTenCbo
		from TBL_HUUTRI HUUTRI1
		where HUUTRI1.sMaCheDo = 'TROCAPMAITANG_TRUYLINH') TROCAPMAITANG_TRUYLINH
		on TBL_HUUTRI.sMaCBo = TROCAPMAITANG_TRUYLINH.sMaCBo and TBL_HUUTRI.sMaDonVi = TROCAPMAITANG_TRUYLINH.sMaDonVi
		left join
		(select HUUTRI2.Id, HUUTRI2.sMaDonVi, HUUTRI2.nGiaTri, HUUTRI2.sMaCB, HUUTRI2.sMaCBo, HUUTRI2.sMaCheDo, HUUTRI2.sTenCbo
		from TBL_HUUTRI HUUTRI2
		where HUUTRI2.sMaCheDo = 'TUTUAT_TROCAP1LAN_TRUYLINH') TUTUAT_TROCAP1LAN_TRUYLINH
		on TBL_HUUTRI.sMaCBo = TUTUAT_TROCAP1LAN_TRUYLINH.sMaCBo and TBL_HUUTRI.sMaDonVi = TUTUAT_TROCAP1LAN_TRUYLINH.sMaDonVi


	--Lay tro cap Huu tri Si quan
	select * into TBL_HUUTRI_SQ from
	(select 1 bHangCha, 'I' LoaiTC, 1 RowNum, 'I' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi,null TenDonVi, null sMaCB, null sMaCBo, N'TC Hưu trí' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'I' LoaiTC,  1 RowNum, null STT, 'SQ' DoiTuong, 'SQ' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'SQ' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'I' LoaiTC,  1 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'SQ' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fHUUTRI_TROCAP1LAN fTROCAP1LAN, fHUUTRI_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	from TBL_HUUTRI_DOC
	where sMaCB like '1%' and (isnull(fHUUTRI_TROCAP1LAN, 0) <> 0 or isnull(fHUUTRI_TROCAPKHUVUC, 0) <> 0)) sq
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_HUUTRI_SQ) > 2
		update TBL_HUUTRI_SQ set bHasData = 1

	--Lay tro cap Huu tri QNCN
	select * into TBL_HUUTRI_QNCN from
	(select 1 bHangCha, 'I' LoaiTC, 1 RowNum, 'I' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Hưu trí' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'I' LoaiTC,  2 RowNum, null STT, 'QNCN' DoiTuong, 'QNCN' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'QNCN' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'I' LoaiTC,  2 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'QNCN' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fHUUTRI_TROCAP1LAN fTROCAP1LAN, fHUUTRI_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	from TBL_HUUTRI_DOC
	where sMaCB like '2%' and (isnull(fHUUTRI_TROCAP1LAN, 0) <> 0 or isnull(fHUUTRI_TROCAPKHUVUC, 0) <> 0)) qncn
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_HUUTRI_QNCN) > 2
		update TBL_HUUTRI_QNCN set bHasData = 1

	--Lay tro cap Huu tri HSQ_BS
	select * into TBL_HUUTRI_HSQBS from
	(select 1 bHangCha, 'I' LoaiTC, 1 RowNum, 'I' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Hưu trí' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'I' LoaiTC,  3 RowNum, null STT, 'HSQ_BS' DoiTuong, 'HSQ_BS' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'HSQ_BS' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'I' LoaiTC,  3 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'HSQ_BS' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fHUUTRI_TROCAP1LAN fTROCAP1LAN, fHUUTRI_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	from TBL_HUUTRI_DOC
	where sMaCB like '0%' and (isnull(fHUUTRI_TROCAP1LAN, 0) <> 0 or isnull(fHUUTRI_TROCAPKHUVUC, 0) <> 0)) hsqbs
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_HUUTRI_HSQBS) > 2
		update TBL_HUUTRI_HSQBS set bHasData = 1

	--Lay tro cap Huu tri VCQP
	select * into TBL_HUUTRI_VCQP from
	(select 1 bHangCha, 'I' LoaiTC, 1 RowNum, 'I' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Hưu trí' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'I' LoaiTC,  4 RowNum, null STT, 'VCQP' DoiTuong, 'VCQP' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'VCQP' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'I' LoaiTC,  4 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'VCQP' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fHUUTRI_TROCAP1LAN fTROCAP1LAN, fHUUTRI_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	from TBL_HUUTRI_DOC
	where sMaCB in ('3.1', '3.2', '3.3', '413', '415') and (isnull(fHUUTRI_TROCAP1LAN, 0) <> 0 or isnull(fHUUTRI_TROCAPKHUVUC, 0) <> 0)) vcqp
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_HUUTRI_VCQP) > 2
		update TBL_HUUTRI_VCQP set bHasData = 1

	--Lay tro cap Huu tri HDLD
	select * into TBL_HUUTRI_LDHD from
	(select 1 bHangCha, 'I' LoaiTC, 1 RowNum, 'I' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Hưu trí' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'I' LoaiTC,  5 RowNum, null STT, 'LDHD' DoiTuong, 'LDHD' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'LDHD' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null  fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'I' LoaiTC,  5 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'LDHD' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fHUUTRI_TROCAP1LAN fTROCAP1LAN, fHUUTRI_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	from TBL_HUUTRI_DOC
	where sMaCB in ('43','423','425') and (isnull(fHUUTRI_TROCAP1LAN, 0) <> 0 or isnull(fHUUTRI_TROCAPKHUVUC, 0) <> 0)) ldhd
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_HUUTRI_LDHD) > 2
		update TBL_HUUTRI_LDHD set bHasData = 1
	-----------------------------------
	--Lay tro cap Phuc vien Si quan
	select * into TBL_PHUCVIEN_SQ from
	(select 1 bHangCha, 'II' LoaiTC, 1 RowNum, 'II' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Phục viên' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'II' LoaiTC, 1 RowNum, null STT, 'SQ' DoiTuong, 'SQ' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'SQ' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'II' LoaiTC, 1 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'SQ' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fPHUCVIEN_TROCAP1LAN fTROCAP1LAN, fPHUCVIEN_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	from TBL_PHUCVIEN_DOC
	where sMaCB like '1%' and (isnull(fPHUCVIEN_TROCAP1LAN, 0) <> 0 or isnull(fPHUCVIEN_TROCAPKHUVUC, 0) <> 0)) pvsq
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_PHUCVIEN_SQ) > 2
		update TBL_PHUCVIEN_SQ set bHasData = 1

	--Lay tro cap Phuc vien QNCN
	select * into TBL_PHUCVIEN_QNCN from
	(select 1 bHangCha, 'II' LoaiTC, 1 RowNum, 'II' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Phục viên' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'II' LoaiTC, 2 RowNum, null STT, 'QNCN' DoiTuong, 'QNCN' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'QNCN' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'II' LoaiTC, 2 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'QNCN' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fPHUCVIEN_TROCAP1LAN fTROCAP1LAN, fPHUCVIEN_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	from TBL_PHUCVIEN_DOC
	where sMaCB like '2%' and (isnull(fPHUCVIEN_TROCAP1LAN, 0) <> 0 or isnull(fPHUCVIEN_TROCAPKHUVUC, 0) <> 0)) pvqncn
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_PHUCVIEN_QNCN) > 2
		update TBL_PHUCVIEN_QNCN set bHasData = 1

	--Lay tro cap Phuc vien HSQ_BS
	select * into TBL_PHUCVIEN_HSQBS from
	(select 1 bHangCha, 'II' LoaiTC, 1 RowNum, 'II' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Phục viên' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'II' LoaiTC, 3 RowNum, null STT, 'HSQ_BS' DoiTuong, 'HSQ_BS' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'HSQ_BS' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'II' LoaiTC, 3 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'HSQ_BS' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fPHUCVIEN_TROCAP1LAN fTROCAP1LAN, fPHUCVIEN_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	from TBL_PHUCVIEN_DOC
	where sMaCB like '0%' and (isnull(fPHUCVIEN_TROCAP1LAN, 0) <> 0 or isnull(fPHUCVIEN_TROCAPKHUVUC, 0) <> 0)) pvhsqbs
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_PHUCVIEN_HSQBS) > 2
		update TBL_PHUCVIEN_HSQBS set bHasData = 1

	--Lay tro cap Phuc vien VCQP
	select * into TBL_PHUCVIEN_VCQP from
	(select 1 bHangCha, 'II' LoaiTC, 1 RowNum, 'II' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Phục viên' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'II' LoaiTC, 4 RowNum, null STT, 'VCQP' DoiTuong, 'VCQP' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'VCQP' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'II' LoaiTC, 4 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'VCQP' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fPHUCVIEN_TROCAP1LAN fTROCAP1LAN, fPHUCVIEN_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	from TBL_PHUCVIEN_DOC
	where sMaCB in ('3.1', '3.2', '3.3', '413', '415') and (isnull(fPHUCVIEN_TROCAP1LAN, 0) <> 0 or isnull(fPHUCVIEN_TROCAPKHUVUC, 0) <> 0)) pvvcqp
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_PHUCVIEN_VCQP) > 2
		update TBL_PHUCVIEN_VCQP set bHasData = 1

	--Lay tro cap Phuc vien HDLD
	select * into TBL_PHUCVIEN_LDHD from
	(select 1 bHangCha, 'II' LoaiTC, 1 RowNum, 'II' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Phục viên' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'II' LoaiTC, 5 RowNum, null STT, 'LDHD' DoiTuong, 'LDHD' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'LDHD' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null  fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'II' LoaiTC, 5 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'LDHD' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fPHUCVIEN_TROCAP1LAN fTROCAP1LAN, fPHUCVIEN_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	from TBL_PHUCVIEN_DOC
	where sMaCB in ('43','423','425') and (isnull(fPHUCVIEN_TROCAP1LAN, 0) <> 0 or isnull(fPHUCVIEN_TROCAPKHUVUC, 0) <> 0)) pvldhd
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_PHUCVIEN_LDHD) > 2
		update TBL_PHUCVIEN_LDHD set bHasData = 1
	--------------------------------------------
	--Lay tro cap Thoi viec Si quan
	select * into TBL_THOIVIEC_SQ from
	(select 1 bHangCha, 'III' LoaiTC, 1 RowNum, 'III' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Thôi việc' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'III' LoaiTC, 1 RowNum, null STT, 'SQ' DoiTuong, 'SQ' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'SQ' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'III' LoaiTC, 1 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'SQ' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTHOIVIEC_TROCAP1LAN fTROCAP1LAN, fTHOIVIEC_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	from TBL_THOIVIEC_DOC
	where sMaCB like '1%' and (isnull(fTHOIVIEC_TROCAP1LAN, 0) <> 0 or isnull(fTHOIVIEC_TROCAPKHUVUC, 0) <> 0)) tvsq
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_THOIVIEC_SQ) > 2
		update TBL_THOIVIEC_SQ set bHasData = 1

	--Lay tro cap Thoi viec QNCN
	select * into TBL_THOIVIEC_QNCN from
	(select 1 bHangCha, 'III' LoaiTC, 1 RowNum, 'III' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Thôi việc' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'III' LoaiTC, 2 RowNum, null STT, 'QNCN' DoiTuong, 'QNCN' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'QNCN' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'III' LoaiTC, 2 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'QNCN' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTHOIVIEC_TROCAP1LAN fTROCAP1LAN, fTHOIVIEC_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	from TBL_THOIVIEC_DOC
	where sMaCB like '2%' and (isnull(fTHOIVIEC_TROCAP1LAN, 0) <> 0 or isnull(fTHOIVIEC_TROCAPKHUVUC, 0) <> 0)) tvqncn
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_THOIVIEC_QNCN) > 2
		update TBL_THOIVIEC_QNCN set bHasData = 1

	--Lay tro cap Thoi viec HSQ_BS
	select * into TBL_THOIVIEC_HSQBS from
	(select 1 bHangCha, 'III' LoaiTC, 1 RowNum, 'III' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Thôi việc' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'III' LoaiTC, 3 RowNum, null STT, 'HSQ_BS' DoiTuong, 'HSQ_BS' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'HSQ_BS' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'III' LoaiTC, 3 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'HSQ_BS' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTHOIVIEC_TROCAP1LAN fTROCAP1LAN, fTHOIVIEC_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	from TBL_THOIVIEC_DOC
	where sMaCB like '0%' and (isnull(fTHOIVIEC_TROCAP1LAN, 0) <> 0 or isnull(fTHOIVIEC_TROCAPKHUVUC, 0) <> 0)) tvhsqbs
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_THOIVIEC_HSQBS) > 2
		update TBL_THOIVIEC_HSQBS set bHasData = 1

	--Lay tro cap Thoi viec VCQP
	select * into TBL_THOIVIEC_VCQP from
	(select 1 bHangCha, 'III' LoaiTC, 1 RowNum, 'III' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Thôi việc' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'III' LoaiTC, 4 RowNum, null STT, 'VCQP' DoiTuong, 'VCQP' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'VCQP' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'III' LoaiTC, 4 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'VCQP' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTHOIVIEC_TROCAP1LAN fTROCAP1LAN, fTHOIVIEC_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	from TBL_THOIVIEC_DOC
	where sMaCB in ('3.1', '3.2', '3.3', '413', '415') and (isnull(fTHOIVIEC_TROCAP1LAN, 0) <> 0 or isnull(fTHOIVIEC_TROCAPKHUVUC, 0) <> 0)) tvvcqp
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_THOIVIEC_VCQP) > 2
		update TBL_THOIVIEC_VCQP set bHasData = 1

	--Lay tro cap Thoi viec HDLD
	select * into TBL_THOIVIEC_LDHD from
	(select 1 bHangCha, 'III' LoaiTC, 1 RowNum, 'III' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Thôi việc' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'III' LoaiTC, 5 RowNum, null STT, 'LDHD' DoiTuong, 'LDHD' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'LDHD' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null  fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'III' LoaiTC, 5 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'LDHD' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTHOIVIEC_TROCAP1LAN fTROCAP1LAN, fTHOIVIEC_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	from TBL_THOIVIEC_DOC
	where sMaCB in ('43','423','425') and (isnull(fTHOIVIEC_TROCAP1LAN, 0) <> 0 or isnull(fTHOIVIEC_TROCAPKHUVUC, 0) <> 0)) tvldhd
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_THOIVIEC_LDHD) > 2
		update TBL_THOIVIEC_LDHD set bHasData = 1
	--------------------------------------------
	--Lay tro cap Tu tuat Si quan
	select * into TBL_TUTUAT_SQ from
	(select 1 bHangCha, 'IV' LoaiTC, 1 RowNum, 'IV' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Tử tuất' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'IV' LoaiTC, 1 RowNum, null STT, 'SQ' DoiTuong, 'SQ' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'SQ' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'IV' LoaiTC, 1 RowNum,
	CASE
		WHEN TBL.sMaCB IS NOT NULL THEN CAST(ROW_NUMBER() OVER (ORDER BY TBL.sMaCB) AS VARCHAR(6))
		ELSE CAST(ROW_NUMBER() OVER (ORDER BY TL.sMaCB) AS VARCHAR(6))
	END AS STT,
	 '', 'SQ' LoaiDoiTuong,
	CASE
		WHEN TBL.Ma_DonVi IS NOT NULL THEN TBL.Ma_DonVi
		ELSE TL.Ma_DonVi
	END AS Ma_DonVi,
	CASE
		WHEN TBL.Ten_DonVi IS NOT NULL THEN TBL.Ten_DonVi
		ELSE TL.Ten_DonVi
	END AS TenDonVi,
	CASE
		WHEN TBL.sMaCB IS NOT NULL THEN TBL.sMaCB
		ELSE TL.sMaCB
	END AS sMaCB,
	CASE
		WHEN TBL.sMaCBo IS NOT NULL THEN TBL.sMaCBo
		ELSE TL.sMaCBo
	END AS sMaCBo,	
	CASE
		WHEN TBL.sTenCbo IS NOT NULL THEN TBL.sTenCbo
		ELSE TL.sTenCbo
	END AS sTenCbo,
	CASE
		WHEN TBL.sSoQuyetDinh IS NOT NULL THEN TBL.sSoQuyetDinh
		ELSE TL.sSoQuyetDinh
	END AS sSoQuyetDinh,
	CASE
		WHEN TBL.dNgayQuyetDinh IS NOT NULL THEN TBL.dNgayQuyetDinh
		ELSE TL.dNgayQuyetDinh
	END AS dNgayQuyetDinh,
	 fTUTUAT_TROCAP1LAN fTROCAP1LAN,
	 fTUTUAT_TROCAPKHUVUC fTROCAPKHUVUC,
	 fTROCAPMAITANG, 0 bHasData,
	 fTUTUAT_TROCAP1LAN_TRUYLINH TROCAP1LANTRUYLINH,
	 fTUTUAT_TROCAPKHUVUC_TRUYLINH fTROCAPKHUVUCTRUYLINH,
	 fTROCAPMAITANG_TRUYLINH fTROCAPMAITANGTRUYLINH
	from TBL_TUTUAT_DOC  TBL
	lefT join TBL_TUTUAT_TRUYLINH TL ON TBL.sMaCBo = TL.sMaCBo and TL.sMaCB like '1%' and (isnull(fTUTUAT_TROCAP1LAN_TRUYLINH, 0) <> 0 or isnull(fTUTUAT_TROCAPKHUVUC_TRUYLINH, 0) <> 0)
	where TBL.sMaCB like '1%' and (isnull(fTUTUAT_TROCAP1LAN, 0) <> 0 or isnull(fTUTUAT_TROCAPKHUVUC, 0) <> 0)) tvsq
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TUTUAT_SQ) > 2
		update TBL_TUTUAT_SQ set bHasData = 1

	--Lay tro cap Tu tuat QNCN
	select * into TBL_TUTUAT_QNCN from
	(select 1 bHangCha, 'IV' LoaiTC, 1 RowNum, 'IV' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Tử tuất' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'IV' LoaiTC, 2 RowNum, null STT, 'QNCN' DoiTuong, 'QNCN' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'QNCN' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'IV' LoaiTC, 2 RowNum,
	CASE
		WHEN TBL.sMaCB IS NOT NULL THEN CAST(ROW_NUMBER() OVER (ORDER BY TBL.sMaCB) AS VARCHAR(6))
		ELSE CAST(ROW_NUMBER() OVER (ORDER BY TL.sMaCB) AS VARCHAR(6))
	END AS STT,
	 '', 'QNCN' LoaiDoiTuong,
	CASE
		WHEN TBL.Ma_DonVi IS NOT NULL THEN TBL.Ma_DonVi
		ELSE TL.Ma_DonVi
	END AS Ma_DonVi,
	CASE
		WHEN TBL.Ten_DonVi IS NOT NULL THEN TBL.Ten_DonVi
		ELSE TL.Ten_DonVi
	END AS TenDonVi,
	CASE
		WHEN TBL.sMaCB IS NOT NULL THEN TBL.sMaCB
		ELSE TL.sMaCB
	END AS sMaCB,
	CASE
		WHEN TBL.sMaCBo IS NOT NULL THEN TBL.sMaCBo
		ELSE TL.sMaCBo
	END AS sMaCBo,	
	CASE
		WHEN TBL.sTenCbo IS NOT NULL THEN TBL.sTenCbo
		ELSE TL.sTenCbo
	END AS sTenCbo,
	CASE
		WHEN TBL.sSoQuyetDinh IS NOT NULL THEN TBL.sSoQuyetDinh
		ELSE TL.sSoQuyetDinh
	END AS sSoQuyetDinh,
	CASE
		WHEN TBL.dNgayQuyetDinh IS NOT NULL THEN TBL.dNgayQuyetDinh
		ELSE TL.dNgayQuyetDinh
	END AS dNgayQuyetDinh,
	 fTUTUAT_TROCAP1LAN fTROCAP1LAN,
	 fTUTUAT_TROCAPKHUVUC fTROCAPKHUVUC,
	 fTROCAPMAITANG, 0 bHasData,
	 fTUTUAT_TROCAP1LAN_TRUYLINH TROCAP1LANTRUYLINH,
	 fTUTUAT_TROCAPKHUVUC_TRUYLINH fTROCAPKHUVUCTRUYLINH,
	 fTROCAPMAITANG_TRUYLINH fTROCAPMAITANGTRUYLINH
	

	from TBL_TUTUAT_DOC TBL
	LEFT JOIN TBL_TUTUAT_TRUYLINH TL ON TBL.sMaCBo = TL.sMaCBo and TL.sMaCB like '2%' and (isnull(fTUTUAT_TROCAP1LAN_TRUYLINH, 0) <> 0 or isnull(fTUTUAT_TROCAPKHUVUC_TRUYLINH, 0) <> 0 OR  isnull(fTROCAPMAITANG_TRUYLINH, 0) <> 0)
	where tbl.sMaCB like '2%' and (isnull(fTUTUAT_TROCAP1LAN, 0) <> 0 or isnull(fTUTUAT_TROCAPKHUVUC, 0) <> 0)) tvqncn
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TUTUAT_QNCN) > 2
		update TBL_TUTUAT_QNCN set bHasData = 1

	--Lay tro cap Tu tuat HSQ_BS
	select * into TBL_TUTUAT_HSQBS from
	(select 1 bHangCha, 'IV' LoaiTC, 1 RowNum, 'IV' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Tử tuất' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'IV' LoaiTC, 3 RowNum, null STT, 'HSQ_BS' DoiTuong, 'HSQ_BS' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'HSQ_BS' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'IV' LoaiTC, 3 RowNum, 
	CASE
		WHEN TBL.sMaCB IS NOT NULL THEN CAST(ROW_NUMBER() OVER (ORDER BY TBL.sMaCB) AS VARCHAR(6))
		ELSE CAST(ROW_NUMBER() OVER (ORDER BY TL.sMaCB) AS VARCHAR(6))
	END AS STT,
	 '', 'HSQ_BS' LoaiDoiTuong,
	CASE
		WHEN TBL.Ma_DonVi IS NOT NULL THEN TBL.Ma_DonVi
		ELSE TL.Ma_DonVi
	END AS Ma_DonVi,
	CASE
		WHEN TBL.Ten_DonVi IS NOT NULL THEN TBL.Ten_DonVi
		ELSE TL.Ten_DonVi
	END AS TenDonVi,
	CASE
		WHEN TBL.sMaCB IS NOT NULL THEN TBL.sMaCB
		ELSE TL.sMaCB
	END AS sMaCB,
	CASE
		WHEN TBL.sMaCBo IS NOT NULL THEN TBL.sMaCBo
		ELSE TL.sMaCBo
	END AS sMaCBo,	
	CASE
		WHEN TBL.sTenCbo IS NOT NULL THEN TBL.sTenCbo
		ELSE TL.sTenCbo
	END AS sTenCbo,
	CASE
		WHEN TBL.sSoQuyetDinh IS NOT NULL THEN TBL.sSoQuyetDinh
		ELSE TL.sSoQuyetDinh
	END AS sSoQuyetDinh,
	CASE
		WHEN TBL.dNgayQuyetDinh IS NOT NULL THEN TBL.dNgayQuyetDinh
		ELSE TL.dNgayQuyetDinh
	END AS dNgayQuyetDinh,
	 fTUTUAT_TROCAP1LAN fTROCAP1LAN,
	 fTUTUAT_TROCAPKHUVUC fTROCAPKHUVUC,
	 fTROCAPMAITANG, 0 bHasData,
	 fTUTUAT_TROCAP1LAN_TRUYLINH TROCAP1LANTRUYLINH,
	 fTUTUAT_TROCAPKHUVUC_TRUYLINH fTROCAPKHUVUCTRUYLINH,
	 fTROCAPMAITANG_TRUYLINH fTROCAPMAITANGTRUYLINH

	from TBL_TUTUAT_DOC TBL
	LEFT JOIN TBL_TUTUAT_TRUYLINH TL ON TBL.sMaCBo = TL.sMaCBo and TL.sMaCB like '0%' and (isnull(fTUTUAT_TROCAP1LAN_TRUYLINH, 0) <> 0 or isnull(fTUTUAT_TROCAPKHUVUC_TRUYLINH, 0) <> 0 OR  isnull(fTROCAPMAITANG_TRUYLINH, 0) <> 0)
	where tbl.sMaCB like '0%' and (isnull(fTUTUAT_TROCAP1LAN, 0) <> 0 or isnull(fTUTUAT_TROCAPKHUVUC, 0) <> 0)) tvhsqbs
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TUTUAT_HSQBS) > 2
		update TBL_TUTUAT_HSQBS set bHasData = 1

	--Lay tro cap Tu tuat VCQP
	select * into TBL_TUTUAT_VCQP from
	(select 1 bHangCha, 'IV' LoaiTC, 1 RowNum, 'IV' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Tử tuất' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'IV' LoaiTC, 4 RowNum, null STT, 'VCQP' DoiTuong, 'VCQP' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'VCQP' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'IV' LoaiTC, 4 RowNum,
	CASE
		WHEN TBL.sMaCB IS NOT NULL THEN CAST(ROW_NUMBER() OVER (ORDER BY TBL.sMaCB) AS VARCHAR(6))
		ELSE CAST(ROW_NUMBER() OVER (ORDER BY TL.sMaCB) AS VARCHAR(6))
	END AS STT,
	 '', 'VCQP' LoaiDoiTuong,
	CASE
		WHEN TBL.Ma_DonVi IS NOT NULL THEN TBL.Ma_DonVi
		ELSE TL.Ma_DonVi
	END AS Ma_DonVi,
	CASE
		WHEN TBL.Ten_DonVi IS NOT NULL THEN TBL.Ten_DonVi
		ELSE TL.Ten_DonVi
	END AS TenDonVi,
	CASE
		WHEN TBL.sMaCB IS NOT NULL THEN TBL.sMaCB
		ELSE TL.sMaCB
	END AS sMaCB,
	CASE
		WHEN TBL.sMaCBo IS NOT NULL THEN TBL.sMaCBo
		ELSE TL.sMaCBo
	END AS sMaCBo,	
	CASE
		WHEN TBL.sTenCbo IS NOT NULL THEN TBL.sTenCbo
		ELSE TL.sTenCbo
	END AS sTenCbo,
	CASE
		WHEN TBL.sSoQuyetDinh IS NOT NULL THEN TBL.sSoQuyetDinh
		ELSE TL.sSoQuyetDinh
	END AS sSoQuyetDinh,
	CASE
		WHEN TBL.dNgayQuyetDinh IS NOT NULL THEN TBL.dNgayQuyetDinh
		ELSE TL.dNgayQuyetDinh
	END AS dNgayQuyetDinh,
	 fTUTUAT_TROCAP1LAN fTROCAP1LAN,
	 fTUTUAT_TROCAPKHUVUC fTROCAPKHUVUC,
	 fTROCAPMAITANG, 0 bHasData,
	 fTUTUAT_TROCAP1LAN_TRUYLINH TROCAP1LANTRUYLINH,
	 fTUTUAT_TROCAPKHUVUC_TRUYLINH fTROCAPKHUVUCTRUYLINH,
	 fTROCAPMAITANG_TRUYLINH fTROCAPMAITANGTRUYLINH

	from TBL_TUTUAT_DOC TBL
	LEFT JOIN TBL_TUTUAT_TRUYLINH TL ON TBL.sMaCBo = TL.sMaCBo and TL.sMaCB  in ('3.1', '3.2', '3.3', '413', '415') and (isnull(fTUTUAT_TROCAP1LAN_TRUYLINH, 0) <> 0 or isnull(fTUTUAT_TROCAPKHUVUC_TRUYLINH, 0) <> 0 OR  isnull(fTROCAPMAITANG_TRUYLINH, 0) <> 0)
	where tbl.sMaCB  in ('3.1', '3.2', '3.3', '413', '415') and (isnull(fTUTUAT_TROCAP1LAN, 0) <> 0 or isnull(fTUTUAT_TROCAPKHUVUC, 0) <> 0)) tvvcqp
	
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TUTUAT_VCQP) > 2
		update TBL_TUTUAT_VCQP set bHasData = 1

	--Lay tro cap Tu tuat HDLD
	select * into TBL_TUTUAT_LDHD from
	(select 1 bHangCha, 'IV' LoaiTC, 1 RowNum, 'IV' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Tử tuất' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'IV' LoaiTC, 5 RowNum, null STT, 'LDHD' DoiTuong, 'LDHD' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'LDHD' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null  fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'IV' LoaiTC, 5 RowNum,
	CASE
		WHEN TBL.sMaCB IS NOT NULL THEN CAST(ROW_NUMBER() OVER (ORDER BY TBL.sMaCB) AS VARCHAR(6))
		ELSE CAST(ROW_NUMBER() OVER (ORDER BY TL.sMaCB) AS VARCHAR(6))
	END AS STT,
	 '', 'LDHD' LoaiDoiTuong,
	CASE
		WHEN TBL.Ma_DonVi IS NOT NULL THEN TBL.Ma_DonVi
		ELSE TL.Ma_DonVi
	END AS Ma_DonVi,
	CASE
		WHEN TBL.Ten_DonVi IS NOT NULL THEN TBL.Ten_DonVi
		ELSE TL.Ten_DonVi
	END AS TenDonVi,
	CASE
		WHEN TBL.sMaCB IS NOT NULL THEN TBL.sMaCB
		ELSE TL.sMaCB
	END AS sMaCB,
	CASE
		WHEN TBL.sMaCBo IS NOT NULL THEN TBL.sMaCBo
		ELSE TL.sMaCBo
	END AS sMaCBo,	
	CASE
		WHEN TBL.sTenCbo IS NOT NULL THEN TBL.sTenCbo
		ELSE TL.sTenCbo
	END AS sTenCbo,
	CASE
		WHEN TBL.sSoQuyetDinh IS NOT NULL THEN TBL.sSoQuyetDinh
		ELSE TL.sSoQuyetDinh
	END AS sSoQuyetDinh,
	CASE
		WHEN TBL.dNgayQuyetDinh IS NOT NULL THEN TBL.dNgayQuyetDinh
		ELSE TL.dNgayQuyetDinh
	END AS dNgayQuyetDinh,
	 fTUTUAT_TROCAP1LAN fTROCAP1LAN,
	 fTUTUAT_TROCAPKHUVUC fTROCAPKHUVUC,
	 fTROCAPMAITANG, 0 bHasData,
	 fTUTUAT_TROCAP1LAN_TRUYLINH TROCAP1LANTRUYLINH,
	 fTUTUAT_TROCAPKHUVUC_TRUYLINH fTROCAPKHUVUCTRUYLINH,
	 fTROCAPMAITANG_TRUYLINH fTROCAPMAITANGTRUYLINH

	from TBL_TUTUAT_DOC TBL
	LEFT JOIN TBL_TUTUAT_TRUYLINH TL ON TBL.sMaCBo = TL.sMaCBo and TL.sMaCB in ('43','423','425')  and (isnull(fTUTUAT_TROCAP1LAN_TRUYLINH, 0) <> 0 or isnull(fTUTUAT_TROCAPKHUVUC_TRUYLINH, 0) <> 0 OR  isnull(fTROCAPMAITANG_TRUYLINH, 0) <> 0)
	where tbl.sMaCB in ('43','423','425') and (isnull(fTUTUAT_TROCAP1LAN, 0) <> 0 or isnull(fTUTUAT_TROCAPKHUVUC, 0) <> 0)) tvldhd
	

	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TUTUAT_LDHD) > 2
		update TBL_TUTUAT_LDHD set bHasData = 1
	----------------------------------------------
	--Ket qua
	select result.* into TBL_HUUTRI_RESULT from
	(select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh,
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien
	from TBL_HUUTRI_SQ
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh,
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien	
	from TBL_HUUTRI_QNCN
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, 
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien
	from TBL_HUUTRI_HSQBS
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh,
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien
	from TBL_HUUTRI_VCQP
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh,
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien	
	from TBL_HUUTRI_LDHD
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh,
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien	
	from TBL_PHUCVIEN_SQ
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh,
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien	
	from TBL_PHUCVIEN_QNCN
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh,
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien	
	from TBL_PHUCVIEN_HSQBS
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, 
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien
	from TBL_PHUCVIEN_VCQP
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh,
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien	
	from TBL_PHUCVIEN_LDHD
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh,
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien	
	from TBL_THOIVIEC_SQ
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh,
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien	
	from TBL_THOIVIEC_QNCN
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh,
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien
	from TBL_THOIVIEC_HSQBS
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh,
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien	
	from TBL_THOIVIEC_VCQP
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh,
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien	
		from TBL_THOIVIEC_LDHD
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh,
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien	
		from TBL_TUTUAT_SQ
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh,
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien	
		from TBL_TUTUAT_QNCN
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, 
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien	
	from TBL_TUTUAT_HSQBS
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh,
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien	
	from TBL_TUTUAT_VCQP
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh,
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien	
	from TBL_TUTUAT_LDHD) result

	select distinct
		LoaiTC SLoaiTC,
		RowNum,
		STT,
		DoiTuong, 
		LoaiDoiTuong,
		sMaCB MaCb,
		sMaCBo MaCbo,
		sTenCbo TenCbo,
		sSoQuyetDinh SSoQuyetDinh,
		dNgayQuyetDinh DNgayQuyetDinh,
		Ma_DonVi MaDonVi,
		TenDonVi,
		fTROCAP1LAN/@DonViTinh FTroCap1Lan,
		fTROCAPKHUVUC/@DonViTinh FTroCapKhuVuc,
		fTROCAPMAITANG/@DonViTinh FTroCapMaiTang,
		fTongSoTienTT/@DonViTinh FTongSoTienThangNay,
		fTROCAP1LANTRUYLINH/@DonViTinh FTroCap1LanTruyLinh,
		fTROCAPKHUVUCTRUYLINH/@DonViTinh FTroCapKhuVucTruyLinh,
		fTROCAPMAITANGTRUYLINH/@DonViTinh FTroCapMaiTangTruyLinh,
		fTongSoTienTL/@DonViTinh FTongSoTienTruyLinh,
		fTongSoTien/@DonViTinh FTongSoTien,
		bHangCha IsHangCha,
		bHasData IsHasData
	from TBL_HUUTRI_RESULT
	order by LoaiTC, RowNum, LoaiDoiTuong, DoiTuong desc

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI]') AND type in (N'U')) drop table TBL_HUUTRI;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI_DOC]') AND type in (N'U')) drop table TBL_HUUTRI_DOC;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_PHUCVIEN_DOC]') AND type in (N'U')) drop table TBL_PHUCVIEN_DOC;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THOIVIEC_DOC]') AND type in (N'U')) drop table TBL_THOIVIEC_DOC;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TUTUAT_DOC]') AND type in (N'U')) drop table TBL_TUTUAT_DOC;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI_SQ]') AND type in (N'U')) drop table TBL_HUUTRI_SQ;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI_QNCN]') AND type in (N'U')) drop table TBL_HUUTRI_QNCN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI_HSQBS]') AND type in (N'U')) drop table TBL_HUUTRI_HSQBS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI_VCQP]') AND type in (N'U')) drop table TBL_HUUTRI_VCQP;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI_LDHD]') AND type in (N'U')) drop table TBL_HUUTRI_LDHD;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_PHUCVIEN_SQ]') AND type in (N'U')) drop table TBL_PHUCVIEN_SQ;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_PHUCVIEN_QNCN]') AND type in (N'U')) drop table TBL_PHUCVIEN_QNCN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_PHUCVIEN_HSQBS]') AND type in (N'U')) drop table TBL_PHUCVIEN_HSQBS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_PHUCVIEN_VCQP]') AND type in (N'U')) drop table TBL_PHUCVIEN_VCQP;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_PHUCVIEN_LDHD]') AND type in (N'U')) drop table TBL_PHUCVIEN_LDHD;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THOIVIEC_SQ]') AND type in (N'U')) drop table TBL_THOIVIEC_SQ;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THOIVIEC_QNCN]') AND type in (N'U')) drop table TBL_THOIVIEC_QNCN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THOIVIEC_HSQBS]') AND type in (N'U')) drop table TBL_THOIVIEC_HSQBS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THOIVIEC_VCQP]') AND type in (N'U')) drop table TBL_THOIVIEC_VCQP;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THOIVIEC_LDHD]') AND type in (N'U')) drop table TBL_THOIVIEC_LDHD;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TUTUAT_SQ]') AND type in (N'U')) drop table TBL_TUTUAT_SQ;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TUTUAT_QNCN]') AND type in (N'U')) drop table TBL_TUTUAT_QNCN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TUTUAT_HSQBS]') AND type in (N'U')) drop table TBL_TUTUAT_HSQBS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TUTUAT_VCQP]') AND type in (N'U')) drop table TBL_TUTUAT_VCQP;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TUTUAT_LDHD]') AND type in (N'U')) drop table TBL_TUTUAT_LDHD;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TUTUAT_TRUYLINH]') AND type in (N'U')) drop table TBL_TUTUAT_TRUYLINH;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI_RESULT]') AND type in (N'U')) drop table TBL_HUUTRI_RESULT;

END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_om_dau]    Script Date: 3/26/2024 2:51:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_om_dau]
	@DsMaDonVi nvarchar(100),
	@NamLamViec int,
	@Thang int,
	@DonViTinh int
AS
BEGIN
	--Lay thong tin luong theo tro cap om dau
	select * into TBL_TCOD from
	(select luongcancu.fLuongCanCu, chedo.fSoNgayHuongBHXH, donvi.Ma_DonVi, donvi.Ten_DonVi, luong.*
	from TL_BangLuong_ThangBHXH luong
	join TL_DM_DonVi donvi on luong.sMaDonVi = donvi.Ma_DonVi
	join TL_CanBo_CheDoBHXH chedo on luong.sMaCBo = chedo.sMaCanBo and luong.sMaCheDo = chedo.sMaCheDo
	left join 
	(select Ma_Hieu_CanBo, NAM, thang, sum(Gia_Tri) fLuongCanCu from TL_BangLuong_Thang
		where Ma_PhuCap in ('LHT_TT', 'PCCV_TT', 'PCTN_TT', 'PCTNVK_TT', 'HSBL_TT')
		group by Ma_Hieu_CanBo, NAM, thang) luongcancu on luong.sMaHieuCanBo = luongcancu.Ma_Hieu_CanBo
			and luongcancu.Nam = chedo.iNamCanCuDong
			and luongcancu.thang = chedo.iThangLuongCanCuDong
	where 
	luong.sMaDonVi in (SELECT * FROM f_split(@DsMaDonVi))
	and luong.iNam = @NamLamViec
	and luong.iThang = @Thang
	and luong.sMaCheDo in ('BENHDAINGAY_D14NGAY', 'BENHDAINGAY_T14NGAY', 'OMKHAC_D14NGAY', 'OMKHAC_T14NGAY', 'CONOM', 'OMDAU_DUONGSUCPHSK')) tcod

	--Lay gia tri phai tru BHXH
	select sMaCBo, sMaDonVi, sum(nGiaTri) nGiaTri
	into TBL_MINUS_BHXH
	from TL_BangLuong_ThangBHXH
	where upper(sMaCheDo) in ('OK_D14N_BHXHCN_TT','BDN_D14N_BHXHCN_TT')
		and sMaDonVi in (SELECT * FROM f_split(@DsMaDonVi))
		and iNam = @NamLamViec
		and iThang = @Thang
	group by sMaCBo, sMaDonVi

	--Lay gia tri phai tru BHYT
	select sMaCBo, sMaDonVi, sum(nGiaTri) nGiaTri
	into TBL_MINUS_BHYT
	from TL_BangLuong_ThangBHXH
	where upper(sMaCheDo) in ('BDN_D14N_BHYTCN_TT','OK_D14N_BHYTCN_TT')
		and sMaDonVi in (SELECT * FROM f_split(@DsMaDonVi))
		and iNam = @NamLamViec
		and iThang = @Thang
	group by sMaCBo, sMaDonVi

	---
	select distinct 
		TBL_TCOD.sMaCB,
		TBL_TCOD.sMaCBo,
		--TBL_TCOD.sMaCheDo,
		TBL_TCOD.sTenCbo,
		TBL_TCOD.Ma_DonVi,
		TBL_TCOD.Ten_DonVi,
		BENHDAINGAYD14NGAY.SoNgayBENHDAINGAYD14NGAY SoNgayBenhDaiNgayD14Ngay,
		BENHDAINGAY_T14NGAY.SoNgayBenhDaiNgayT14Ngay,
		OMKHAC_D14NGAY.SoNgayOmKhacD14Ngay,
		OMKHAC_T14NGAY.SoNgayOmKhacT14Ngay,
		CONOM.SoNgayConOm,
		DUONGSUCPHSK.SoNgayDuongSuc,
		TBL_TCOD.fLuongCanCu,
		BENHDAINGAYD14NGAY.nGiaTri fBENHDAINGAY_D14NGAY,
		BENHDAINGAY_T14NGAY.nGiaTri fBENHDAINGAY_T14NGAY,
		OMKHAC_D14NGAY.nGiaTri fOMKHAC_D14NGAY,
		OMKHAC_T14NGAY.nGiaTri fOMKHAC_T14NGAY,
		CONOM.nGiaTri fCONOM,
		DUONGSUCPHSK.nGiaTri fDUONGSUCPHSK
		into TBL_TCOD_DOC
	from TBL_TCOD TBL_TCOD
		left join
		(select tcod.sMaDonVi, tcod.nGiaTri, tcod.sMaCB, tcod.sMaCBo, tcod.sTenCbo, tcod.fSoNgayHuongBHXH SoNgayBenhDaiNgayT14Ngay
		from TBL_TCOD tcod
		where tcod.sMaCheDo = 'BENHDAINGAY_T14NGAY') BENHDAINGAY_T14NGAY
		on TBL_TCOD.sMaCBo = BENHDAINGAY_T14NGAY.sMaCBo and TBL_TCOD.sMaDonVi = BENHDAINGAY_T14NGAY.sMaDonVi
		left join
		(select tcod_1.sMaDonVi, tcod_1.nGiaTri, tcod_1.sMaCB, tcod_1.sMaCBo, tcod_1.sTenCbo, tcod_1.fSoNgayHuongBHXH SoNgayOmKhacD14Ngay
		from TBL_TCOD tcod_1
		where tcod_1.sMaCheDo = 'OMKHAC_D14NGAY') OMKHAC_D14NGAY
		on TBL_TCOD.sMaCBo = OMKHAC_D14NGAY.sMaCBo and TBL_TCOD.sMaDonVi = OMKHAC_D14NGAY.sMaDonVi
		left join
		(select tcod_2.sMaDonVi, tcod_2.nGiaTri, tcod_2.sMaCB, tcod_2.sMaCBo, tcod_2.sTenCbo, tcod_2.fSoNgayHuongBHXH SoNgayOmKhacT14Ngay
		from TBL_TCOD tcod_2
		where tcod_2.sMaCheDo = 'OMKHAC_T14NGAY') OMKHAC_T14NGAY
		on TBL_TCOD.sMaCBo = OMKHAC_T14NGAY.sMaCBo and TBL_TCOD.sMaDonVi = OMKHAC_T14NGAY.sMaDonVi
		left join
		(select conom.sMaDonVi, conom.nGiaTri, conom.sMaCB, conom.sMaCBo, conom.sTenCbo, conom.fSoNgayHuongBHXH SoNgayConOm
		from TBL_TCOD conom
		where conom.sMaCheDo = 'CONOM') CONOM
		on TBL_TCOD.sMaCBo = CONOM.sMaCBo and TBL_TCOD.sMaDonVi = CONOM.sMaDonVi
		left join
		(select duongsuc.sMaDonVi, duongsuc.nGiaTri, duongsuc.sMaCB, duongsuc.sMaCBo, duongsuc.sTenCbo, duongsuc.fSoNgayHuongBHXH SoNgayDuongSuc
		from TBL_TCOD duongsuc
		where duongsuc.sMaCheDo = 'OMDAU_DUONGSUCPHSK') DUONGSUCPHSK
		on TBL_TCOD.sMaCBo = DUONGSUCPHSK.sMaCBo and TBL_TCOD.sMaDonVi = DUONGSUCPHSK.sMaDonVi
		left join
		(select tcod_3.sMaDonVi, tcod_3.nGiaTri, tcod_3.sMaCB, tcod_3.sMaCBo, tcod_3.sTenCbo, tcod_3.fSoNgayHuongBHXH SoNgayBENHDAINGAYD14NGAY
		from TBL_TCOD tcod_3
		where tcod_3.sMaCheDo = 'BENHDAINGAY_D14NGAY') BENHDAINGAYD14NGAY
		on TBL_TCOD.sMaCBo = BENHDAINGAYD14NGAY.sMaCBo and TBL_TCOD.sMaDonVi = BENHDAINGAYD14NGAY.sMaDonVi

	--Lấy lương Sĩ quan
	select * into TBL_TCOD_SQ from
	(select
		1 bHangCha, 1 RowNum, 'I' STT, 'SQ' DoiTuong, 'SQ' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'SQ' sTenCbo, null SoNgayBenhDaiNgayD14Ngay, null SoNgayBenhDaiNgayT14Ngay, null SoNgayOmKhacD14Ngay, null SoNgayOmKhacT14Ngay, null SoNgayConOm, null SoNgayDuongSuc, null fLuongCanCu, null fBENHDAINGAY_D14NGAY, null fBENHDAINGAY_T14NGAY, null fOMKHAC_D14NGAY, null fOMKHAC_T14NGAY, null fCONOM, null fDUONGSUCPHSK, 0 bHasData
	union all
	select
		0 bHangCha, 1 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'SQ' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK, 0 bHasData
	from TBL_TCOD_DOC
	where sMaCB like '1%' and (isnull(fLuongCanCu, 0) <> 0 or isnull(fBENHDAINGAY_D14NGAY, 0) <> 0 or isnull(fBENHDAINGAY_T14NGAY, 0) <> 0 or isnull(fOMKHAC_D14NGAY, 0) <> 0 or isnull(fOMKHAC_T14NGAY, 0) <> 0 or isnull(fCONOM, 0) <> 0  or isnull(fDUONGSUCPHSK, 0) <> 0)) sq
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCOD_SQ) > 1
		update TBL_TCOD_SQ set bHasData = 1

	--Lấy lương QNCN
	select * into TBL_TCOD_QNCN from
	(select
		1 bHangCha, 2 RowNum, 'II' STT, 'QNCN' DoiTuong, 'QNCN' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'QNCN' sTenCbo, null SoNgayBenhDaiNgayD14Ngay, null SoNgayBenhDaiNgayT14Ngay, null SoNgayOmKhacD14Ngay, null SoNgayOmKhacT14Ngay, null SoNgayConOm, null SoNgayDuongSuc, null fLuongCanCu, null fBENHDAINGAY_D14NGAY, null fBENHDAINGAY_T14NGAY, null fOMKHAC_D14NGAY, null fOMKHAC_T14NGAY, null fCONOM, null fDUONGSUCPHSK, 0 bHasData
	union all
	select
		0 bHangCha, 2 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'QNCN' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK, 0 bHasData
	from TBL_TCOD_DOC
	where sMaCB like '2%' and (isnull(fLuongCanCu, 0) <> 0 or isnull(fBENHDAINGAY_D14NGAY, 0) <> 0 or isnull(fBENHDAINGAY_T14NGAY, 0) <> 0 or isnull(fOMKHAC_D14NGAY, 0) <> 0 or isnull(fOMKHAC_T14NGAY, 0) <> 0 or isnull(fCONOM, 0) <> 0  or isnull(fDUONGSUCPHSK, 0) <> 0)) qncn
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCOD_QNCN) > 1
		update TBL_TCOD_QNCN set bHasData = 1

	--Lấy lương HSQ_BS
	select * into TBL_TCOD_HSQBS from
	(select
		1 bHangCha, 3 RowNum, 'III' STT, 'HSQ_BS' DoiTuong, 'HSQ_BS' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'HSQ_BS' sTenCbo, null SoNgayBenhDaiNgayD14Ngay, null SoNgayBenhDaiNgayT14Ngay, null SoNgayOmKhacD14Ngay, null SoNgayOmKhacT14Ngay, null SoNgayConOm, null SoNgayDuongSuc, null fLuongCanCu, null fBENHDAINGAY_D14NGAY, null fBENHDAINGAY_T14NGAY, null fOMKHAC_D14NGAY, null fOMKHAC_T14NGAY, null fCONOM, null fDUONGSUCPHSK, 0 bHasData
	union all
	select
		0 bHangCha, 3 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'HSQ_BS' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK, 0 bHasData
	from TBL_TCOD_DOC
	where sMaCB like '0%' and (isnull(fLuongCanCu, 0) <> 0 or isnull(fBENHDAINGAY_D14NGAY, 0) <> 0 or isnull(fBENHDAINGAY_T14NGAY, 0) <> 0 or isnull(fOMKHAC_D14NGAY, 0) <> 0 or isnull(fOMKHAC_T14NGAY, 0) <> 0 or isnull(fCONOM, 0) <> 0  or isnull(fDUONGSUCPHSK, 0) <> 0)) hsqbs
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCOD_HSQBS) > 1
		update TBL_TCOD_HSQBS set bHasData = 1

	--Lấy lương VCQP
	select * into TBL_TCOD_VCQP from
	(select
		1 bHangCha, 4 RowNum, 'IV' STT, 'VCQP' DoiTuong, 'VCQP' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'VCQP' sTenCbo, null SoNgayBenhDaiNgayD14Ngay, null SoNgayBenhDaiNgayT14Ngay, null SoNgayOmKhacD14Ngay, null SoNgayOmKhacT14Ngay, null SoNgayConOm, null SoNgayDuongSuc, null fLuongCanCu, null fBENHDAINGAY_D14NGAY, null fBENHDAINGAY_T14NGAY, null fOMKHAC_D14NGAY, null fOMKHAC_T14NGAY, null fCONOM, null fDUONGSUCPHSK, 0 bHasData
	union all
	select
		0 bHangCha, 4 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'VCQP' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK, 0 bHasData
	from TBL_TCOD_DOC
	where sMaCB in ('3.1', '3.2', '3.3', '413', '415') and (isnull(fLuongCanCu, 0) <> 0 or isnull(fBENHDAINGAY_D14NGAY, 0) <> 0 or isnull(fBENHDAINGAY_T14NGAY, 0) <> 0 or isnull(fOMKHAC_D14NGAY, 0) <> 0 or isnull(fOMKHAC_T14NGAY, 0) <> 0 or isnull(fCONOM, 0) <> 0  or isnull(fDUONGSUCPHSK, 0) <> 0)) vcqp
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCOD_VCQP) > 1
		update TBL_TCOD_VCQP set bHasData = 1

	--Lấy lương Lao động hợp đông
	select * into TBL_TCOD_LDHD from
	(select
		1 bHangCha, 5 RowNum, 'V' STT, 'LDHD' DoiTuong, 'LDHD' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'LDHD' sTenCbo, null SoNgayBenhDaiNgayD14Ngay, null SoNgayBenhDaiNgayT14Ngay, null SoNgayOmKhacD14Ngay, null SoNgayOmKhacT14Ngay, null SoNgayConOm, null SoNgayDuongSuc, null fLuongCanCu, null fBENHDAINGAY_D14NGAY, null fBENHDAINGAY_T14NGAY, null fOMKHAC_D14NGAY, null fOMKHAC_T14NGAY, null fCONOM, null fDUONGSUCPHSK, 0 bHasData
	union all
	select
		0 bHangCha, 5 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'LDHD' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK, 0 bHasData
	from TBL_TCOD_DOC
	where sMaCB in ('43','423','425') and (isnull(fLuongCanCu, 0) <> 0 or isnull(fBENHDAINGAY_D14NGAY, 0) <> 0 or isnull(fBENHDAINGAY_T14NGAY, 0) <> 0 or isnull(fOMKHAC_D14NGAY, 0) <> 0 or isnull(fOMKHAC_T14NGAY, 0) <> 0 or isnull(fCONOM, 0) <> 0  or isnull(fDUONGSUCPHSK, 0) <> 0)) ldhd
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCOD_LDHD) > 1
		update TBL_TCOD_LDHD set bHasData = 1

	--Ket qua
	select result.* into TBL_TCOD_RESULT from
	(select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK,
	(isnull(fBENHDAINGAY_D14NGAY, 0) + isnull(fBENHDAINGAY_T14NGAY, 0) + isnull(fOMKHAC_D14NGAY, 0) + isnull(fOMKHAC_T14NGAY, 0) + isnull(fCONOM, 0) + isnull(fDUONGSUCPHSK, 0)) fTongSoTien
	from TBL_TCOD_SQ
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK,
	(isnull(fBENHDAINGAY_D14NGAY, 0) + isnull(fBENHDAINGAY_T14NGAY, 0) + isnull(fOMKHAC_D14NGAY, 0) + isnull(fOMKHAC_T14NGAY, 0) + isnull(fCONOM, 0) + isnull(fDUONGSUCPHSK, 0)) fTongSoTien
	from TBL_TCOD_QNCN
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK,
	(isnull(fBENHDAINGAY_D14NGAY, 0) + isnull(fBENHDAINGAY_T14NGAY, 0) + isnull(fOMKHAC_D14NGAY, 0) + isnull(fOMKHAC_T14NGAY, 0) + isnull(fCONOM, 0) + isnull(fDUONGSUCPHSK, 0)) fTongSoTien
	from TBL_TCOD_HSQBS
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK,
	(isnull(fBENHDAINGAY_D14NGAY, 0) + isnull(fBENHDAINGAY_T14NGAY, 0) + isnull(fOMKHAC_D14NGAY, 0) + isnull(fOMKHAC_T14NGAY, 0) + isnull(fCONOM, 0) + isnull(fDUONGSUCPHSK, 0)) fTongSoTien
	from TBL_TCOD_VCQP
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK,
	(isnull(fBENHDAINGAY_D14NGAY, 0) + isnull(fBENHDAINGAY_T14NGAY, 0) + isnull(fOMKHAC_D14NGAY, 0) + isnull(fOMKHAC_T14NGAY, 0) + isnull(fCONOM, 0) + isnull(fDUONGSUCPHSK, 0)) fTongSoTien
	from TBL_TCOD_LDHD) result

	select
		result.RowNum,
		result.STT,
		result.DoiTuong, 
		result.LoaiDoiTuong,
		result.sMaCB MaCb, 
		result.sMaCBo MaCbo,
		result.sTenCbo TenCbo,
		result.Ma_DonVi MaDonVi,
		result.TenDonVi,
		result.SoNgayBenhDaiNgayD14Ngay, 
		result.SoNgayBenhDaiNgayT14Ngay, 
		result.SoNgayOmKhacD14Ngay, 
		result.SoNgayOmKhacT14Ngay, 
		result.SoNgayConOm, 
		result.SoNgayDuongSuc, 
		result.fLuongCanCu/@DonViTinh FLuongCanCu, 
		result.fBENHDAINGAY_D14NGAY/@DonViTinh FBenhDaiNgayD14Ngay, 
		result.fBENHDAINGAY_T14NGAY/@DonViTinh FBenhDauNgayT14Ngay, 
		result.fOMKHAC_D14NGAY/@DonViTinh FOmKhacD14Ngay, 
		result.fOMKHAC_T14NGAY/@DonViTinh FOmKhacT14Ngay, 
		result.fCONOM/@DonViTinh FConOm, 
		result.fDUONGSUCPHSK/@DonViTinh FDuongSucPHSK,
		result.fTongSoTien/@DonViTinh FTongSoTien,
		minus.nGiaTri/@DonViTinh FSoPhaiTruBHXH,
		bhyt.nGiaTri/@DonViTinh FSoPhaiTruBHYT,
		(isnull(minus.nGiaTri, 0) + isnull(bhyt.nGiaTri, 0))/@DonViTinh FTongPhaiTru,
		(isnull(result.fTongSoTien, 0) - (isnull(minus.nGiaTri, 0) + isnull(bhyt.nGiaTri, 0)))/@DonViTinh FDuocNhan,
		result.bHangCha IsHangCha,
		result.bHasData IsHasData
	from TBL_TCOD_RESULT result
	left join TBL_MINUS_BHXH minus on result.sMaCBo = minus.sMaCBo and result.Ma_DonVi = minus.sMaDonVi
	left join TBL_MINUS_BHYT bhyt on result.sMaCBo = bhyt.sMaCBo and result.Ma_DonVi = bhyt.sMaDonVi
	order by RowNum, LoaiDoiTuong, DoiTuong desc

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCOD]') AND type in (N'U')) drop table TBL_TCOD;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_MINUS_BHXH]') AND type in (N'U')) drop table TBL_MINUS_BHXH;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_MINUS_BHYT]') AND type in (N'U')) drop table TBL_MINUS_BHYT;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCOD_DOC]') AND type in (N'U')) drop table TBL_TCOD_DOC;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCOD_SQ]') AND type in (N'U')) drop table TBL_TCOD_SQ;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCOD_QNCN]') AND type in (N'U')) drop table TBL_TCOD_QNCN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCOD_HSQBS]') AND type in (N'U')) drop table TBL_TCOD_HSQBS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCOD_VCQP]') AND type in (N'U')) drop table TBL_TCOD_VCQP;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCOD_LDHD]') AND type in (N'U')) drop table TBL_TCOD_LDHD;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCOD_RESULT]') AND type in (N'U')) drop table TBL_TCOD_RESULT;

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
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_thai_san]    Script Date: 3/26/2024 2:51:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_thai_san]
	@DsMaDonVi nvarchar(100),
	@NamLamViec int,
	@Thang int,
	@DonViTinh int
AS  
BEGIN
	--Lay thong tin luong theo tro cap thai san
	select * into TBL_TCTS from
	(select luongcancu.fLuongCanCu, chedo.fSoNgayHuongBHXH, donvi.Ma_DonVi,donvi.Ten_DonVi, luong.*
	from TL_BangLuong_ThangBHXH luong
	join TL_DM_DonVi donvi on luong.sMaDonVi = donvi.Ma_DonVi
	join TL_CanBo_CheDoBHXH chedo on luong.sMaCBo = chedo.sMaCanBo and luong.sMaCheDo = chedo.sMaCheDo
	left join 
	(select Ma_Hieu_CanBo, NAM, thang, sum(Gia_Tri) fLuongCanCu from TL_BangLuong_Thang
		where Ma_PhuCap in ('LHT_TT', 'PCCV_TT', 'PCTN_TT', 'PCTNVK_TT', 'HSBL_TT')
		group by Ma_Hieu_CanBo, NAM, thang) luongcancu on luong.sMaHieuCanBo = luongcancu.Ma_Hieu_CanBo
			and luongcancu.Nam = chedo.iNamCanCuDong
			and luongcancu.thang = chedo.iThangLuongCanCuDong
	where 
	luong.sMaDonVi in (SELECT * FROM f_split(@DsMaDonVi))
	and luong.iNam = @NamLamViec
	and luong.iThang = @Thang
	and luong.sMaCheDo in ('SINHCON_NUOICON_LDN', 'SINHCON_NUOICON_LDNU', 'THAISAN_TROCAP1LAN_LDN', 'THAISAN_TROCAP1LAN_LDNU', 'KHAMTHAI', N'KHHGD', 'NAMNGHIKHIVOSINHCON', 'THAISAN_DUONGSUCPHSK')) tcts
	select distinct
		TBL_TCTS.sMaCB,
		TBL_TCTS.sMaCBo,
		--TBL_TCTS.sMaCheDo,
		TBL_TCTS.sTenCbo,
		TBL_TCTS.Ma_DonVi,
		TBL_TCTS.Ten_DonVi,
		SINHCONNUOICON.SoNgaySINHCONNUOICON SoNgaySINHCON_NUOICON,
		TROCAP1LAN.SoNgayTROCAP1LAN,
		KHAMTHAI.SoNgayKHAMTHAI,
		DUONGSUCPHSK.SoNgayDUONGSUCPHSK,
		TBL_TCTS.fLuongCanCu,
		SINHCONNUOICON.nGiaTri fSINHCON_NUOICON,
		TROCAP1LAN.nGiaTri fTROCAP1LAN,
		KHAMTHAI.nGiaTri fKHAMTHAI,
		DUONGSUCPHSK.nGiaTri fDUONGSUCPHSK
		into TBL_TCTS_DOC
	from TBL_TCTS TBL_TCTS
		left join
		(select tcts.sMaDonVi, sum(tcts.nGiaTri) nGiaTri, tcts.sMaCB, tcts.sMaCBo, tcts.sTenCbo, sum(chedo.fSoNgayHuongBHXH) SoNgayTROCAP1LAN
		from TBL_TCTS tcts left join TL_CanBo_CheDoBHXH chedo on tcts.sMaCBo = chedo.sMaCanBo and tcts.sMaCheDo = chedo.sMaCheDo
		where tcts.sMaCheDo in ('THAISAN_TROCAP1LAN_LDN', 'THAISAN_TROCAP1LAN_LDNU')
		group by tcts.sMaDonVi, tcts.sMaCB, tcts.sMaCBo, tcts.sTenCbo) TROCAP1LAN
		on TBL_TCTS.sMaCBo = TROCAP1LAN.sMaCBo and TBL_TCTS.sMaDonVi = TROCAP1LAN.sMaDonVi
		left join
		(select tcts_1.sMaDonVi, sum(tcts_1.nGiaTri) nGiaTri, tcts_1.sMaCB, tcts_1.sMaCBo, tcts_1.sTenCbo, sum(chedo.fSoNgayHuongBHXH) SoNgayKHAMTHAI
		from TBL_TCTS tcts_1 left join TL_CanBo_CheDoBHXH chedo on tcts_1.sMaCBo = chedo.sMaCanBo and tcts_1.sMaCheDo = chedo.sMaCheDo
		where tcts_1.sMaCheDo in ('KHAMTHAI', N'KHHGD', 'NAMNGHIKHIVOSINHCON')
		group by tcts_1.sMaDonVi, tcts_1.sMaCB, tcts_1.sMaCBo, tcts_1.sTenCbo) KHAMTHAI
		on TBL_TCTS.sMaCBo = KHAMTHAI.sMaCBo and TBL_TCTS.sMaDonVi = KHAMTHAI.sMaDonVi
		left join
		(select tcts_2.sMaDonVi, tcts_2.nGiaTri, tcts_2.sMaCB, tcts_2.sMaCBo, tcts_2.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayDUONGSUCPHSK
		from TBL_TCTS tcts_2 left join TL_CanBo_CheDoBHXH chedo on tcts_2.sMaCBo = chedo.sMaCanBo and tcts_2.sMaCheDo = chedo.sMaCheDo
		where tcts_2.sMaCheDo = 'THAISAN_DUONGSUCPHSK') DUONGSUCPHSK
		on TBL_TCTS.sMaCBo = DUONGSUCPHSK.sMaCBo and TBL_TCTS.sMaDonVi = DUONGSUCPHSK.sMaDonVi
		left join
		(select tcts_3.sMaDonVi, sum(tcts_3.nGiaTri) nGiaTri, tcts_3.sMaCB, tcts_3.sMaCBo, tcts_3.sTenCbo, sum(chedo.fSoNgayHuongBHXH) SoNgaySINHCONNUOICON
		from TBL_TCTS tcts_3 left join TL_CanBo_CheDoBHXH chedo on tcts_3.sMaCBo = chedo.sMaCanBo and tcts_3.sMaCheDo = chedo.sMaCheDo
		where tcts_3.sMaCheDo in ('SINHCON_NUOICON_LDN', 'SINHCON_NUOICON_LDNU')
		group by tcts_3.sMaDonVi, tcts_3.sMaCB, tcts_3.sMaCBo, tcts_3.sTenCbo) SINHCONNUOICON
		on TBL_TCTS.sMaCBo = SINHCONNUOICON.sMaCBo and TBL_TCTS.sMaDonVi = SINHCONNUOICON.sMaDonVi

	--Lấy lương Sĩ quan
	select * into TBL_TCTS_SQ from
	(select
		 1 bHangCha, 1 RowNum, 'I' STT, 'SQ' DoiTuong, 'SQ' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'SQ' sTenCbo, null SoNgaySINHCON_NUOICON, null SoNgayTROCAP1LAN, null SoNgayKHAMTHAI, null SoNgayDUONGSUCPHSK, null fLuongCanCu, null fSINHCON_NUOICON, null fTROCAP1LAN, null fKHAMTHAI, null fDUONGSUCPHSK, 0 bHasData
	union all
	select
		0 bHangCha, 1 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'SQ' LoaiDoiTuong, Ma_DonVi,Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgaySINHCON_NUOICON, SoNgayTROCAP1LAN, SoNgayKHAMTHAI, SoNgayDUONGSUCPHSK, fLuongCanCu, fSINHCON_NUOICON, fTROCAP1LAN, fKHAMTHAI, fDUONGSUCPHSK, 0 bHasData
	from TBL_TCTS_DOC
	where sMaCB like '1%' and (isnull(fLuongCanCu, 0) <> 0 or isnull(fSINHCON_NUOICON, 0) <> 0 or isnull(fTROCAP1LAN, 0) <> 0 or isnull(fKHAMTHAI, 0) <> 0 or isnull(fDUONGSUCPHSK, 0) <> 0)) sq
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCTS_SQ) > 1
		update TBL_TCTS_SQ set bHasData = 1

	--Lấy lương QNCN
	select * into TBL_TCTS_QNCN from
	(select
		1 bHangCha, 2 RowNum, 'II' STT, 'QNCN' DoiTuong, 'QNCN' LoaiDoiTuong,  null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'QNCN' sTenCbo, null SoNgaySINHCON_NUOICON, null SoNgayTROCAP1LAN, null SoNgayKHAMTHAI, null SoNgayDUONGSUCPHSK, null fLuongCanCu, null fSINHCON_NUOICON, null fTROCAP1LAN, null fKHAMTHAI, null fDUONGSUCPHSK, 0 bHasData
	union all
	select
		0 bHangCha, 2 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'QNCN' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgaySINHCON_NUOICON, SoNgayTROCAP1LAN, SoNgayKHAMTHAI, SoNgayDUONGSUCPHSK, fLuongCanCu, fSINHCON_NUOICON, fTROCAP1LAN, fKHAMTHAI, fDUONGSUCPHSK, 0 bHasData
	from TBL_TCTS_DOC
	where sMaCB like '2%' and (isnull(fLuongCanCu, 0) <> 0 or isnull(fSINHCON_NUOICON, 0) <> 0 or isnull(fTROCAP1LAN, 0) <> 0 or isnull(fKHAMTHAI, 0) <> 0 or isnull(fDUONGSUCPHSK, 0) <> 0)) qncn
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCTS_QNCN) > 1
		update TBL_TCTS_QNCN set bHasData = 1

	--Lấy lương HSQ_BS
	select * into TBL_TCTS_HSQBS from
	(select
		1 bHangCha, 3 RowNum, 'III' STT, 'HSQ_BS' DoiTuong, 'HSQ_BS' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'HSQ_BS' sTenCbo, null SoNgaySINHCON_NUOICON, null SoNgayTROCAP1LAN, null SoNgayKHAMTHAI, null SoNgayDUONGSUCPHSK, null fLuongCanCu, null fSINHCON_NUOICON, null fTROCAP1LAN, null fKHAMTHAI, null fDUONGSUCPHSK, 0 bHasData
	union all
	select
		0 bHangCha, 3 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'HSQ_BS' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgaySINHCON_NUOICON, SoNgayTROCAP1LAN, SoNgayKHAMTHAI, SoNgayDUONGSUCPHSK, fLuongCanCu, fSINHCON_NUOICON, fTROCAP1LAN, fKHAMTHAI, fDUONGSUCPHSK, 0 bHasData
	from TBL_TCTS_DOC
	where sMaCB like '0%' and (isnull(fLuongCanCu, 0) <> 0 or isnull(fSINHCON_NUOICON, 0) <> 0 or isnull(fTROCAP1LAN, 0) <> 0 or isnull(fKHAMTHAI, 0) <> 0 or isnull(fDUONGSUCPHSK, 0) <> 0)) hsqbs
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCTS_HSQBS) > 1
		update TBL_TCTS_HSQBS set bHasData = 1

	--Lấy lương VCQP
	select * into TBL_TCTS_VCQP from
	(select
		1 bHangCha, 4 RowNum, 'IV' STT, 'VCQP' DoiTuong, 'VCQP' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'VCQP' sTenCbo, null SoNgaySINHCON_NUOICON, null SoNgayTROCAP1LAN, null SoNgayKHAMTHAI, null SoNgayDUONGSUCPHSK, null fLuongCanCu, null fSINHCON_NUOICON, null fTROCAP1LAN, null fKHAMTHAI, null fDUONGSUCPHSK, 0 bHasData
	union all
	select
		0 bHangCha, 4 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'VCQP' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgaySINHCON_NUOICON, SoNgayTROCAP1LAN, SoNgayKHAMTHAI, SoNgayDUONGSUCPHSK, fLuongCanCu, fSINHCON_NUOICON, fTROCAP1LAN, fKHAMTHAI, fDUONGSUCPHSK, 0 bHasData
	from TBL_TCTS_DOC
	where sMaCB in ('3.1', '3.2', '3.3', '413', '415') and (isnull(fLuongCanCu, 0) <> 0 or isnull(fSINHCON_NUOICON, 0) <> 0 or isnull(fTROCAP1LAN, 0) <> 0 or isnull(fKHAMTHAI, 0) <> 0 or isnull(fDUONGSUCPHSK, 0) <> 0)) vcqp
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCTS_VCQP) > 1
		update TBL_TCTS_VCQP set bHasData = 1

	--Lấy lương Lao động hợp đông
	select * into TBL_TCTS_LDHD from
	(select
		1 bHangCha, 5 RowNum, 'V' STT, 'LDHD' DoiTuong, 'LDHD' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'LDHD' sTenCbo, null SoNgaySINHCON_NUOICON, null SoNgayTROCAP1LAN, null SoNgayKHAMTHAI, null SoNgayDUONGSUCPHSK, null fLuongCanCu, null fSINHCON_NUOICON, null fTROCAP1LAN, null fKHAMTHAI, null fDUONGSUCPHSK, 0 bHasData
	union all
	select
		0 bHangCha, 5 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'LDHD' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgaySINHCON_NUOICON, SoNgayTROCAP1LAN, SoNgayKHAMTHAI, SoNgayDUONGSUCPHSK, fLuongCanCu, fSINHCON_NUOICON, fTROCAP1LAN, fKHAMTHAI, fDUONGSUCPHSK, 0 bHasData
	from TBL_TCTS_DOC
	where sMaCB in ('43','423','425') and (isnull(fLuongCanCu, 0) <> 0 or isnull(fSINHCON_NUOICON, 0) <> 0 or isnull(fTROCAP1LAN, 0) <> 0 or isnull(fKHAMTHAI, 0) <> 0 or isnull(fDUONGSUCPHSK, 0) <> 0)) ldhd
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCTS_LDHD) > 1
		update TBL_TCTS_LDHD set bHasData = 1

	--Ket qua
	select result.* into TBL_TCTS_RESULT from
	(select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgaySINHCON_NUOICON, SoNgayTROCAP1LAN, SoNgayKHAMTHAI, SoNgayDUONGSUCPHSK, fLuongCanCu, fSINHCON_NUOICON, fTROCAP1LAN, fKHAMTHAI, fDUONGSUCPHSK,
	(isnull(fSINHCON_NUOICON, 0) + isnull(fTROCAP1LAN, 0) + isnull(fKHAMTHAI, 0) + isnull(fDUONGSUCPHSK, 0)) fTongSoTien
	from TBL_TCTS_SQ
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi,TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgaySINHCON_NUOICON, SoNgayTROCAP1LAN, SoNgayKHAMTHAI, SoNgayDUONGSUCPHSK, fLuongCanCu, fSINHCON_NUOICON, fTROCAP1LAN, fKHAMTHAI, fDUONGSUCPHSK,
	(isnull(fSINHCON_NUOICON, 0) + isnull(fTROCAP1LAN, 0) + isnull(fKHAMTHAI, 0) + isnull(fDUONGSUCPHSK, 0)) fTongSoTien
	from TBL_TCTS_QNCN
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgaySINHCON_NUOICON, SoNgayTROCAP1LAN, SoNgayKHAMTHAI, SoNgayDUONGSUCPHSK, fLuongCanCu, fSINHCON_NUOICON, fTROCAP1LAN, fKHAMTHAI, fDUONGSUCPHSK,
	(isnull(fSINHCON_NUOICON, 0) + isnull(fTROCAP1LAN, 0) + isnull(fKHAMTHAI, 0) + isnull(fDUONGSUCPHSK, 0)) fTongSoTien
	from TBL_TCTS_HSQBS
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgaySINHCON_NUOICON, SoNgayTROCAP1LAN, SoNgayKHAMTHAI, SoNgayDUONGSUCPHSK, fLuongCanCu, fSINHCON_NUOICON, fTROCAP1LAN, fKHAMTHAI, fDUONGSUCPHSK,
	(isnull(fSINHCON_NUOICON, 0) + isnull(fTROCAP1LAN, 0) + isnull(fKHAMTHAI, 0) + isnull(fDUONGSUCPHSK, 0)) fTongSoTien
	from TBL_TCTS_VCQP
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgaySINHCON_NUOICON, SoNgayTROCAP1LAN, SoNgayKHAMTHAI, SoNgayDUONGSUCPHSK, fLuongCanCu, fSINHCON_NUOICON, fTROCAP1LAN, fKHAMTHAI, fDUONGSUCPHSK,
	(isnull(fSINHCON_NUOICON, 0) + isnull(fTROCAP1LAN, 0) + isnull(fKHAMTHAI, 0) + isnull(fDUONGSUCPHSK, 0)) fTongSoTien
	from TBL_TCTS_LDHD) result

	select distinct
		RowNum,
		STT,
		DoiTuong, 
		LoaiDoiTuong,
		sMaCB MaCb, 
		sMaCBo MaCbo, 
		sTenCbo TenCbo,
		Ma_DonVi MaDonVi,
		TenDonVi,
		SoNgaySINHCON_NUOICON SoNgaySinhConNuoiCon,
		SoNgayTROCAP1LAN SoNgayTroCap1Lan,
		SoNgayKHAMTHAI SoNgayKhamThai,
		SoNgayDUONGSUCPHSK SoNgayDuongSucPHSKThaiSan,
		fLuongCanCu/@DonViTinh FLuongCanCu,
		fSINHCON_NUOICON/@DonViTinh fSinhConNuoiCon,
		fTROCAP1LAN/@DonViTinh fTroCap1Lan,
		fKHAMTHAI/@DonViTinh fKhamThai,
		fDUONGSUCPHSK/@DonViTinh fDuongSucPHSKThaiSan,
		fTongSoTien/@DonViTinh FTongSoTien,
		bHangCha IsHangCha,
		bHasData IsHasData
	from TBL_TCTS_RESULT
	order by RowNum, LoaiDoiTuong, DoiTuong desc

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTS]') AND type in (N'U'))
	drop table TBL_TCTS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTS_DOC]') AND type in (N'U'))
	drop table TBL_TCTS_DOC;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTS_SQ]') AND type in (N'U'))
	drop table TBL_TCTS_SQ;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTS_QNCN]') AND type in (N'U'))
	drop table TBL_TCTS_QNCN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTS_HSQBS]') AND type in (N'U'))
	drop table TBL_TCTS_HSQBS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTS_VCQP]') AND type in (N'U'))
	drop table TBL_TCTS_VCQP;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTS_LDHD]') AND type in (N'U'))
	drop table TBL_TCTS_LDHD;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTS_RESULT]') AND type in (N'U'))
	drop table TBL_TCTS_RESULT;

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
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_tnld]    Script Date: 3/26/2024 2:51:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_tnld] 
	@DsMaDonVi nvarchar(100),
	@NamLamViec int,
	@Thang int,
	@DonViTinh int
AS
BEGIN
	DECLARE @MaCheDo nvarchar(1000) = 'CHIGIAMDINH,TAINANLD_TROCAP1LAN,TROCAPTHEOPHIEUTRUYTRA,TROCAPHANGTHANG,TROCAPPHCN,TROCAPPHUCVU,TROCAPCHETDOTNLD,TAINANLĐ_DUONGSUCPHSK,CHIGIAMDINH_TRUYLINH,TNLD_TROCAP1LAN_TRUYLINH,TROCAPHANGTHANG_TRUYLINH,TROCAPPHCN_TRUYLINH,TROCAPPHUCVU_TRUYLINH,TROCAPCHETDOTNLD_TRUYLINH,TNLD_DUONGSUCPHSK_TRUYLINH,HOTRO_CDNN_TRUYLINH,HOTRO_PHONGNGUA_TRUYLINH'
	--Lay thong tin luong theo tro cap tai nan lao dong
	select * into TBL_TCTNLD from
	(select donvi.Ma_DonVi, donvi.Ten_DonVi, luong.*
	from TL_BangLuong_ThangBHXH luong
	join TL_DM_DonVi donvi on luong.sMaDonVi = donvi.Ma_DonVi
	where 
	luong.sMaDonVi in (SELECT * FROM f_split(@DsMaDonVi))
	and luong.iNam = @NamLamViec
	and luong.iThang = @Thang
	and luong.sMaCheDo in (select * from splitstring(@MaCheDo))) tctnld

	select distinct 
		TBL_TCTNLD.sMaCB,
		TBL_TCTNLD.sMaCBo,
		--TBL_TCTNLD.sMaCheDo,
		TBL_TCTNLD.sTenCbo,
		TBL_TCTNLD.Ma_DonVi,
		TBL_TCTNLD.Ten_DonVi,
		CHIGIAMDINH.nGiaTri fChiGiamDinh,
		TROCAP1LAN.nGiaTri fTroCap1Lan,
		TROCAPTHEOPHIEUTRUYTRA.nGiaTri fTroCapTheoPhieuTruyTra,
		TROCAPHANGTHANG.nGiaTri fTroCapHangThang,
		TROCAPPHCN.nGiaTri fTroCapPHCN,
		TROCAPCHETDOTNLD.nGiaTri fTroCapChetDoTNLD,
		TAINANLĐ_DUONGSUCPHSK.SoNgayDuongSucTNLD,
		TAINANLĐ_DUONGSUCPHSK.nGiaTri fDuongSucTNLD,
		chedocha.sSoQuyetDinh,
		chedocha.dNgayQuyetDinh,
		CHIGIAMDINH_TL.nGiaTri fChiGiamDinhTruyLinh,
		TROCAP1LAN_TL.nGiaTri fTroCap1LanTruyLinh,
		HOTRO_CDNN_TRUYLINH_TL.nGiaTri fHoTroCdnnTruyLinh,
		TROCAPHANGTHANG_TL.nGiaTri fTroCapHangThangTruyLinh,
		TROCAPPHCN_TL.nGiaTri fTroCapPHCNTruyLinh,
		TROCAPCHETDOTNLD_TL.nGiaTri fTroCapChetDoTNLDTruyLinh,
		TAINANLĐ_DUONGSUCPHSK_TL.SoNgayDuongSucTNLD SoNgayDuongSucTNLDTruyLinh,
		TAINANLĐ_DUONGSUCPHSK_TL.nGiaTri fDuongSucTNLDTruyLinh
		into TBL_TCTNLD_DOC
	from TBL_TCTNLD TBL_TCTNLD
		left join (select top 1 * from TL_CanBo_CheDoBHXH 
		where sMaCheDo in (select * from splitstring(@MaCheDo))
		and ((sSoQuyetDinh is not null and sSoQuyetDinh <> '') 
		or (dNgayQuyetDinh is not null and dNgayQuyetDinh <> ''))) chedocha
		on TBL_TCTNLD.sMaCBo = chedocha.sMaCanBo
		left join
		(select tnld.sMaDonVi, tnld.nGiaTri, tnld.sMaCB, tnld.sMaCBo, tnld.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTroCap1Lan
		from TBL_TCTNLD tnld left join TL_CanBo_CheDoBHXH chedo on tnld.sMaCBo = chedo.sMaCanBo and tnld.sMaCheDo = chedo.sMaCheDo
		where tnld.sMaCheDo = 'TAINANLD_TROCAP1LAN') TROCAP1LAN
		on TBL_TCTNLD.sMaCBo = TROCAP1LAN.sMaCBo and TBL_TCTNLD.sMaDonVi = TROCAP1LAN.sMaDonVi
		left join
		(select tnld_1.sMaDonVi, tnld_1.nGiaTri, tnld_1.sMaCB, tnld_1.sMaCBo, tnld_1.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTroCapTheoPhieuTruyTra
		from TBL_TCTNLD tnld_1 left join TL_CanBo_CheDoBHXH chedo on tnld_1.sMaCBo = chedo.sMaCanBo and tnld_1.sMaCheDo = chedo.sMaCheDo
		where tnld_1.sMaCheDo = 'TROCAPTHEOPHIEUTRUYTRA') TROCAPTHEOPHIEUTRUYTRA
		on TBL_TCTNLD.sMaCBo = TROCAPTHEOPHIEUTRUYTRA.sMaCBo and TBL_TCTNLD.sMaDonVi = TROCAPTHEOPHIEUTRUYTRA.sMaDonVi
		left join
		(select tnld_2.sMaDonVi, tnld_2.nGiaTri, tnld_2.sMaCB, tnld_2.sMaCBo, tnld_2.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTroCapHangThang
		from TBL_TCTNLD tnld_2 left join TL_CanBo_CheDoBHXH chedo on tnld_2.sMaCBo = chedo.sMaCanBo and tnld_2.sMaCheDo = chedo.sMaCheDo
		where tnld_2.sMaCheDo = 'TROCAPHANGTHANG') TROCAPHANGTHANG
		on TBL_TCTNLD.sMaCBo = TROCAPHANGTHANG.sMaCBo and TBL_TCTNLD.sMaDonVi = TROCAPHANGTHANG.sMaDonVi
		left join
		(select tnld_3.sMaDonVi, sum(tnld_3.nGiaTri) nGiaTri, tnld_3.sMaCB, tnld_3.sMaCBo, tnld_3.sTenCbo, sum(chedo.fSoNgayHuongBHXH) SoNgayTroCapPHCN
		from TBL_TCTNLD tnld_3 left join TL_CanBo_CheDoBHXH chedo on tnld_3.sMaCBo = chedo.sMaCanBo and tnld_3.sTenCbo = chedo.sMaCheDo
		where tnld_3.sMaCheDo in ('TROCAPPHCN', 'TROCAPPHUCVU')
		group by tnld_3.sMaDonVi, tnld_3.sMaCB, tnld_3.sMaCBo, tnld_3.sTenCbo) TROCAPPHCN
		on TBL_TCTNLD.sMaCBo = TROCAPPHCN.sMaCBo and TBL_TCTNLD.sMaDonVi = TROCAPPHCN.sMaDonVi
		left join
		(select tnld_4.sMaDonVi, tnld_4.nGiaTri, tnld_4.sMaCB, tnld_4.sMaCBo, tnld_4.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTroCapChetDoTNLD
		from TBL_TCTNLD tnld_4 left join TL_CanBo_CheDoBHXH chedo on tnld_4.sMaCBo = chedo.sMaCanBo and tnld_4.sMaCheDo = chedo.sMaCheDo
		where tnld_4.sMaCheDo = 'TROCAPCHETDOTNLD') TROCAPCHETDOTNLD
		on TBL_TCTNLD.sMaCBo = TROCAPCHETDOTNLD.sMaCBo and TBL_TCTNLD.sMaDonVi = TROCAPCHETDOTNLD.sMaDonVi
		left join
		(select tnld_5.sMaDonVi, tnld_5.nGiaTri, tnld_5.sMaCB, tnld_5.sMaCBo, tnld_5.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayDuongSucTNLD
		from TBL_TCTNLD tnld_5 left join TL_CanBo_CheDoBHXH chedo on tnld_5.sMaCBo = chedo.sMaCanBo and tnld_5.sMaCheDo = chedo.sMaCheDo
		where tnld_5.sMaCheDo = 'TAINANLĐ_DUONGSUCPHSK') TAINANLĐ_DUONGSUCPHSK
		on TBL_TCTNLD.sMaCBo = TAINANLĐ_DUONGSUCPHSK.sMaCBo and TBL_TCTNLD.sMaDonVi = TAINANLĐ_DUONGSUCPHSK.sMaDonVi
		left join
		(select tnld_6.sMaDonVi, tnld_6.nGiaTri, tnld_6.sMaCB, tnld_6.sMaCBo, tnld_6.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayCHIGIAMDINH
		from TBL_TCTNLD tnld_6 left join TL_CanBo_CheDoBHXH chedo on tnld_6.sMaCBo = chedo.sMaCanBo and tnld_6.sMaCheDo = chedo.sMaCheDo
		where tnld_6.sMaCheDo = 'CHIGIAMDINH') CHIGIAMDINH
		on TBL_TCTNLD.sMaCBo = CHIGIAMDINH.sMaCBo and TBL_TCTNLD.sMaDonVi = CHIGIAMDINH.sMaDonVi

		-- TRUYLINH
		left join
		(select tnld.sMaDonVi, tnld.nGiaTri, tnld.sMaCB, tnld.sMaCBo, tnld.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTroCap1Lan
		from TBL_TCTNLD tnld left join TL_CanBo_CheDoBHXH chedo on tnld.sMaCBo = chedo.sMaCanBo and tnld.sMaCheDo = chedo.sMaCheDo
		where tnld.sMaCheDo = 'TNLD_TROCAP1LAN_TRUYLINH ') TROCAP1LAN_TL
		on TBL_TCTNLD.sMaCBo = TROCAP1LAN_TL.sMaCBo and TBL_TCTNLD.sMaDonVi = TROCAP1LAN_TL.sMaDonVi
		left join
		(select tnld_1.sMaDonVi, tnld_1.nGiaTri, tnld_1.sMaCB, tnld_1.sMaCBo, tnld_1.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTroCapTheoPhieuTruyTra
		from TBL_TCTNLD tnld_1 left join TL_CanBo_CheDoBHXH chedo on tnld_1.sMaCBo = chedo.sMaCanBo and tnld_1.sMaCheDo = chedo.sMaCheDo
		where tnld_1.sMaCheDo = 'HOTRO_CDNN_TRUYLINH ') HOTRO_CDNN_TRUYLINH_TL
		on TBL_TCTNLD.sMaCBo = HOTRO_CDNN_TRUYLINH_TL.sMaCBo and TBL_TCTNLD.sMaDonVi = HOTRO_CDNN_TRUYLINH_TL.sMaDonVi
		left join
		(select tnld_2.sMaDonVi, tnld_2.nGiaTri, tnld_2.sMaCB, tnld_2.sMaCBo, tnld_2.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTroCapHangThang
		from TBL_TCTNLD tnld_2 left join TL_CanBo_CheDoBHXH chedo on tnld_2.sMaCBo = chedo.sMaCanBo and tnld_2.sMaCheDo = chedo.sMaCheDo
		where tnld_2.sMaCheDo = 'TROCAPHANGTHANG_TRUYLINH') TROCAPHANGTHANG_TL
		on TBL_TCTNLD.sMaCBo = TROCAPHANGTHANG_TL.sMaCBo and TBL_TCTNLD.sMaDonVi = TROCAPHANGTHANG_TL.sMaDonVi
		left join
		(select tnld_3.sMaDonVi, sum(tnld_3.nGiaTri) nGiaTri, tnld_3.sMaCB, tnld_3.sMaCBo, tnld_3.sTenCbo, sum(chedo.fSoNgayHuongBHXH) SoNgayTroCapPHCN
		from TBL_TCTNLD tnld_3 left join TL_CanBo_CheDoBHXH chedo on tnld_3.sMaCBo = chedo.sMaCanBo and tnld_3.sTenCbo = chedo.sMaCheDo
		where tnld_3.sMaCheDo in ('TROCAPPHCN_TRUYLINH', 'TROCAPPHUCVU_TRUYLINH')
		group by tnld_3.sMaDonVi, tnld_3.sMaCB, tnld_3.sMaCBo, tnld_3.sTenCbo) TROCAPPHCN_TL
		on TBL_TCTNLD.sMaCBo = TROCAPPHCN_TL.sMaCBo and TBL_TCTNLD.sMaDonVi = TROCAPPHCN_TL.sMaDonVi
		left join
		(select tnld_4.sMaDonVi, tnld_4.nGiaTri, tnld_4.sMaCB, tnld_4.sMaCBo, tnld_4.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTroCapChetDoTNLD
		from TBL_TCTNLD tnld_4 left join TL_CanBo_CheDoBHXH chedo on tnld_4.sMaCBo = chedo.sMaCanBo and tnld_4.sMaCheDo = chedo.sMaCheDo
		where tnld_4.sMaCheDo = 'TROCAPCHETDOTNLD_TRUYLINH') TROCAPCHETDOTNLD_TL
		on TBL_TCTNLD.sMaCBo = TROCAPCHETDOTNLD_TL.sMaCBo and TBL_TCTNLD.sMaDonVi = TROCAPCHETDOTNLD_TL.sMaDonVi
		left join
		(select tnld_5.sMaDonVi, tnld_5.nGiaTri, tnld_5.sMaCB, tnld_5.sMaCBo, tnld_5.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayDuongSucTNLD
		from TBL_TCTNLD tnld_5 left join TL_CanBo_CheDoBHXH chedo on tnld_5.sMaCBo = chedo.sMaCanBo and tnld_5.sMaCheDo = chedo.sMaCheDo
		where tnld_5.sMaCheDo = 'TNLD_DUONGSUCPHSK_TRUYLINH ') TAINANLĐ_DUONGSUCPHSK_TL
		on TBL_TCTNLD.sMaCBo = TAINANLĐ_DUONGSUCPHSK_TL.sMaCBo and TBL_TCTNLD.sMaDonVi = TAINANLĐ_DUONGSUCPHSK_TL.sMaDonVi
		left join
		(select tnld_6.sMaDonVi, tnld_6.nGiaTri, tnld_6.sMaCB, tnld_6.sMaCBo, tnld_6.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayCHIGIAMDINH
		from TBL_TCTNLD tnld_6 left join TL_CanBo_CheDoBHXH chedo on tnld_6.sMaCBo = chedo.sMaCanBo and tnld_6.sMaCheDo = chedo.sMaCheDo
		where tnld_6.sMaCheDo = 'CHIGIAMDINH_TRUYLINH') CHIGIAMDINH_TL
		on TBL_TCTNLD.sMaCBo = CHIGIAMDINH_TL.sMaCBo and TBL_TCTNLD.sMaDonVi = CHIGIAMDINH_TL.sMaDonVi

	--Lấy lương Sĩ quan
	select * into TBL_TCTNLD_SQ from
	(select
		1 bHangCha, 1 RowNum, 'I' STT, 'SQ' DoiTuong, 'SQ' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'SQ' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null SoNgayDuongSucTNLD, null fChiGiamDinh, null fTroCap1Lan, null fTroCapTheoPhieuTruyTra, null fTroCapHangThang, null fTroCapPHCN, null fTroCapChetDoTNLD, null fDuongSucTNLD, 0 bHasData,
		null SoNgayDuongSucTNLDTruyLinh, null fChiGiamDinhTruyLinh, null fTroCap1LanTruyLinh, null fHoTroCdnnTruyLinh, null fTroCapHangThangTruyLinh, null fTroCapPHCNTruyLinh, null fTroCapChetDoTNLDTruyLinh, null fDuongSucTNLDTruyLinh
	union all
	select
		0 bHangCha, 1 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'SQ' LoaiDoiTuong, Ma_DonVi,  Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fTroCapChetDoTNLD, fDuongSucTNLD,0 bHasData,
		SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh
	from TBL_TCTNLD_DOC
	where sMaCB like '1%' and (isnull(fChiGiamDinh, 0) <> 0 or isnull(fTroCap1Lan, 0) <> 0 or isnull(fTroCapTheoPhieuTruyTra, 0) <> 0 or isnull(fTroCapHangThang, 0) <> 0 or isnull(fTroCapPHCN, 0) <> 0 or isnull(fTroCapChetDoTNLD, 0) <> 0 or isnull(fDuongSucTNLD, 0) <> 0) OR
				ISNULL(fChiGiamDinhTruyLinh,0) <> 0 OR ISNULL(fTroCap1LanTruyLinh,0) <> 0 OR ISNULL(fHoTroCdnnTruyLinh,0) <> 0 OR ISNULL(fTroCapHangThangTruyLinh,0) <> 0 OR
				ISNULL( fTroCapPHCNTruyLinh,0) <> 0 OR ISNULL(fTroCapChetDoTNLDTruyLinh,0) <> 0 OR ISNULL( fDuongSucTNLDTruyLinh, 0) <> 0
		) sq
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCTNLD_SQ) > 1
		update TBL_TCTNLD_SQ set bHasData = 1

	--Lấy lương QNCN
	select * into TBL_TCTNLD_QNCN from
	(select
		1 bHangCha, 2 RowNum, 'II' STT, 'QNCN' DoiTuong, 'QNCN' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'QNCN' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null SoNgayDuongSucTNLD, null fChiGiamDinh, null fTroCap1Lan, null fTroCapTheoPhieuTruyTra, null fTroCapHangThang, null fTroCapPHCN, null fTroCapChetDoTNLD, null fDuongSucTNLD, 0 bHasData,
		null SoNgayDuongSucTNLDTruyLinh, null fChiGiamDinhTruyLinh, null fTroCap1LanTruyLinh, null fHoTroCdnnTruyLinh, null fTroCapHangThangTruyLinh, null fTroCapPHCNTruyLinh, null fTroCapChetDoTNLDTruyLinh, null fDuongSucTNLDTruyLinh
	union all
	select
		0 bHangCha, 2 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'QNCN' LoaiDoiTuong, Ma_DonVi,  Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fTroCapChetDoTNLD, fDuongSucTNLD, 0 bHasData,
		SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh
	from TBL_TCTNLD_DOC
	where sMaCB like '2%' and (isnull(fChiGiamDinh, 0) <> 0 or isnull(fTroCap1Lan, 0) <> 0 or isnull(fTroCapTheoPhieuTruyTra, 0) <> 0 or isnull(fTroCapHangThang, 0) <> 0 or isnull(fTroCapPHCN, 0) <> 0 or isnull(fTroCapChetDoTNLD, 0) <> 0 or isnull(fDuongSucTNLD, 0) <> 0)) qncn
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCTNLD_QNCN) > 1
		update TBL_TCTNLD_QNCN set bHasData = 1

	--Lấy lương HSQ_BS
	select * into TBL_TCTNLD_HSQBS from
	(select
		1 bHangCha, 3 RowNum, 'III' STT, 'HSQ_BS' DoiTuong, 'HSQ_BS' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'HSQ_BS' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null SoNgayDuongSucTNLD, null fChiGiamDinh, null fTroCap1Lan, null fTroCapTheoPhieuTruyTra, null fTroCapHangThang, null fTroCapPHCN, null fTroCapChetDoTNLD, null fDuongSucTNLD, 0 bHasData,
		null SoNgayDuongSucTNLDTruyLinh, null fChiGiamDinhTruyLinh, null fTroCap1LanTruyLinh, null fHoTroCdnnTruyLinh, null fTroCapHangThangTruyLinh, null fTroCapPHCNTruyLinh, null fTroCapChetDoTNLDTruyLinh, null fDuongSucTNLDTruyLinh
	union all
	select
		0 bHangCha, 3 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'HSQ_BS' LoaiDoiTuong, Ma_DonVi,  Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fTroCapChetDoTNLD, fDuongSucTNLD, 0 bHasData,
		SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh
	from TBL_TCTNLD_DOC
	where sMaCB like '0%' and (isnull(fChiGiamDinh, 0) <> 0 or isnull(fTroCap1Lan, 0) <> 0 or isnull(fTroCapTheoPhieuTruyTra, 0) <> 0 or isnull(fTroCapHangThang, 0) <> 0 or isnull(fTroCapPHCN, 0) <> 0 or isnull(fTroCapChetDoTNLD, 0) <> 0 or isnull(fDuongSucTNLD, 0) <> 0) OR
				ISNULL(fChiGiamDinhTruyLinh,0) <> 0 OR ISNULL(fTroCap1LanTruyLinh,0) <> 0 OR ISNULL(fHoTroCdnnTruyLinh,0) <> 0 OR ISNULL(fTroCapHangThangTruyLinh,0) <> 0 OR
				ISNULL( fTroCapPHCNTruyLinh,0) <> 0 OR ISNULL(fTroCapChetDoTNLDTruyLinh,0) <> 0 OR ISNULL( fDuongSucTNLDTruyLinh, 0) <> 0
		)  hsqbs
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCTNLD_HSQBS) > 1
		update TBL_TCTNLD_HSQBS set bHasData = 1

	--Lấy lương VCQP
	select * into TBL_TCTNLD_VCQP from
	(select
		1 bHangCha, 4 RowNum, 'IV' STT, 'VCQP' DoiTuong, 'VCQP' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'VCQP' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null SoNgayDuongSucTNLD, null fChiGiamDinh, null fTroCap1Lan, null fTroCapTheoPhieuTruyTra, null fTroCapHangThang, null fTroCapPHCN, null fTroCapChetDoTNLD, null fDuongSucTNLD, 0 bHasData,
		null SoNgayDuongSucTNLDTruyLinh, null fChiGiamDinhTruyLinh, null fTroCap1LanTruyLinh, null fHoTroCdnnTruyLinh, null fTroCapHangThangTruyLinh, null fTroCapPHCNTruyLinh, null fTroCapChetDoTNLDTruyLinh, null fDuongSucTNLDTruyLinh
	union all
	select
		0 bHangCha, 4 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'VCQP' LoaiDoiTuong, Ma_DonVi,  Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fTroCapChetDoTNLD, fDuongSucTNLD, 0 bHasData,
		SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh
	from TBL_TCTNLD_DOC
	where sMaCB in ('3.1', '3.2', '3.3', '413', '415') and (isnull(fChiGiamDinh, 0) <> 0 or isnull(fTroCap1Lan, 0) <> 0 or isnull(fTroCapTheoPhieuTruyTra, 0) <> 0 or isnull(fTroCapHangThang, 0) <> 0 or isnull(fTroCapPHCN, 0) <> 0 or isnull(fTroCapChetDoTNLD, 0) <> 0 or isnull(fDuongSucTNLD, 0) <> 0)) vcqp
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCTNLD_VCQP) > 1
		update TBL_TCTNLD_VCQP set bHasData = 1

	--Lấy lương Lao động hợp đông
	select * into TBL_TCTNLD_LDHD from
	(select
		1 bHangCha, 5 RowNum, 'V' STT, 'LDHD' DoiTuong, 'LDHD' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'LDHD' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null SoNgayDuongSucTNLD, null fChiGiamDinh, null fTroCap1Lan, null fTroCapTheoPhieuTruyTra, null fTroCapHangThang, null fTroCapPHCN, null fTroCapChetDoTNLD, null fDuongSucTNLD, 0 bHasData,
		null SoNgayDuongSucTNLDTruyLinh, null fChiGiamDinhTruyLinh, null fTroCap1LanTruyLinh, null fHoTroCdnnTruyLinh, null fTroCapHangThangTruyLinh, null fTroCapPHCNTruyLinh, null fTroCapChetDoTNLDTruyLinh, null fDuongSucTNLDTruyLinh
	union all
	select
		0 bHangCha, 5 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'LDHD' LoaiDoiTuong, Ma_DonVi,  Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fTroCapChetDoTNLD, fDuongSucTNLD, 0 bHasData,
		SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh
	from TBL_TCTNLD_DOC
	where sMaCB in ('43','423','425') and (isnull(fChiGiamDinh, 0) <> 0 or isnull(fTroCap1Lan, 0) <> 0 or isnull(fTroCapTheoPhieuTruyTra, 0) <> 0 or isnull(fTroCapHangThang, 0) <> 0 or isnull(fTroCapPHCN, 0) <> 0 or isnull(fTroCapChetDoTNLD, 0) <> 0 or isnull(fDuongSucTNLD, 0) <> 0) OR
				ISNULL(fChiGiamDinhTruyLinh,0) <> 0 OR ISNULL(fTroCap1LanTruyLinh,0) <> 0 OR ISNULL(fHoTroCdnnTruyLinh,0) <> 0 OR ISNULL(fTroCapHangThangTruyLinh,0) <> 0 OR
				ISNULL( fTroCapPHCNTruyLinh,0) <> 0 OR ISNULL(fTroCapChetDoTNLDTruyLinh,0) <> 0 OR ISNULL( fDuongSucTNLDTruyLinh, 0) <> 0
		)  ldhd
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCTNLD_LDHD) > 1
		update TBL_TCTNLD_LDHD set bHasData = 1

	--Ket qua
	select result.* into TBL_TCTNLD_RESULT from
	(select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fTroCapChetDoTNLD, fDuongSucTNLD,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) fTongSoTienThangNay,
	SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh,
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) fTongSoTienTruyLinh,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) +
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) FTongSoTien
	from TBL_TCTNLD_SQ
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fTroCapChetDoTNLD, fDuongSucTNLD,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) fTongSoTienThangNay,
	SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh,
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) fTongSoTienTruyLinh,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) +
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) FTongSoTien
	from TBL_TCTNLD_QNCN
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fTroCapChetDoTNLD, fDuongSucTNLD,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) fTongSoTienThangNay,
	SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh,
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) fTongSoTienTruyLinh,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) +
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) FTongSoTien
	from TBL_TCTNLD_HSQBS
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fTroCapChetDoTNLD, fDuongSucTNLD,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) fTongSoTienThangNay,
	SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh,
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) fTongSoTienTruyLinh,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) +
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) FTongSoTien
	from TBL_TCTNLD_VCQP
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fTroCapChetDoTNLD, fDuongSucTNLD,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) fTongSoTienThangNay,
	SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh,
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) fTongSoTienTruyLinh,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) +
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) FTongSoTien
	from TBL_TCTNLD_LDHD) result

	select 
		RowNum,
		STT,
		DoiTuong, 
		LoaiDoiTuong,
		sMaCB MaCb, 
		sMaCBo MaCbo, 
		sTenCbo TenCbo,
		sSoQuyetDinh SSoQuyetDinh,
		dNgayQuyetDinh DNgayQuyetDinh,
		Ma_DonVi MaDonVi,
		TenDonVi,
		SoNgayDuongSucTNLD,
		fChiGiamDinh/@DonViTinh fChiGiamDinh,
		fTroCap1Lan/@DonViTinh fTroCap1Lan,
		fTroCapTheoPhieuTruyTra/@DonViTinh fTroCapTheoPhieuTruyTra,
		fTroCapHangThang/@DonViTinh fTroCapHangThang,
		fTroCapPHCN/@DonViTinh fTroCapPHCN,
		fTroCapChetDoTNLD/@DonViTinh fTroCapChetDoTNLD,
		fDuongSucTNLD/@DonViTinh fDuongSucTNLD,
		fTongSoTienThangNay/@DonViTinh fTongSoTienThangNay,
		bHangCha IsHangCha,
		bHasData IsHasData,
		SoNgayDuongSucTNLDTruyLinh,
		fChiGiamDinhTruyLinh/@DonViTinh fChiGiamDinhTruyLinh,
		fTroCap1LanTruyLinh/@DonViTinh fTroCap1LanTruyLinh,
		fHoTroCdnnTruyLinh/@DonViTinh fHoTroCdnnTruyLinh,
		fTroCapHangThangTruyLinh/@DonViTinh fTroCapHangThangTruyLinh,
		fTroCapPHCNTruyLinh/@DonViTinh fTroCapPHCNTruyLinh,
		fTroCapChetDoTNLDTruyLinh/@DonViTinh fTroCapChetDoTNLDTruyLinh,
		fDuongSucTNLDTruyLinh/@DonViTinh fDuongSucTNLDTruyLinh,
		fTongSoTienTruyLinh/@DonViTinh fTongSoTienTruyLinh,
		fTongSoTien/@DonViTinh fTongSoTien

	from TBL_TCTNLD_RESULT
	order by RowNum, LoaiDoiTuong, DoiTuong desc

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD]') AND type in (N'U'))
	drop table TBL_TCTNLD;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_DOC]') AND type in (N'U'))
	drop table TBL_TCTNLD_DOC;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_SQ]') AND type in (N'U'))
	drop table TBL_TCTNLD_SQ;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_QNCN]') AND type in (N'U'))
	drop table TBL_TCTNLD_QNCN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_HSQBS]') AND type in (N'U'))
	drop table TBL_TCTNLD_HSQBS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_VCQP]') AND type in (N'U'))
	drop table TBL_TCTNLD_VCQP;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_LDHD]') AND type in (N'U'))
	drop table TBL_TCTNLD_LDHD;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_RESULT]') AND type in (N'U'))
	drop table TBL_TCTNLD_RESULT;

END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_xuat_ngu]    Script Date: 3/26/2024 2:51:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_xuat_ngu]
	@DsMaDonVi nvarchar(100),
	@NamLamViec int,
	@Thang int,
	@DonViTinh int
AS
BEGIN
	--Lay thong tin luong theo tro cap thai san
	select * into TBL_TCXN from
	(select donvi.Ma_DonVi, donvi.Ten_DonVi,
		chedocha.sSoQuyetDinh,
		chedocha.dNgayQuyetDinh,
		luong.*
	from TL_BangLuong_ThangBHXH luong
	join TL_DM_DonVi donvi on luong.sMaDonVi = donvi.Ma_DonVi
	left join (select top 1 * from TL_CanBo_CheDoBHXH 
		where sMaCheDo in ('XUATNGU_TROCAP1LAN')
		and ((sSoQuyetDinh is not null and sSoQuyetDinh <> '') 
		or (dNgayQuyetDinh is not null and dNgayQuyetDinh <> ''))) chedocha
		on luong.sMaCBo = chedocha.sMaCanBo
	where 
	luong.sMaDonVi in (SELECT * FROM f_split(@DsMaDonVi))
	and luong.iNam = @NamLamViec
	and luong.iThang = @Thang
	and luong.sMaCheDo in ('XUATNGU_TROCAP1LAN')) tcod

	--Lấy lương Sĩ quan
	select * into TBL_TCXN_SQ from
	(select null STT, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sMaCheDo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, nGiaTri
	from TBL_TCXN
	where sMaCB like '1%') sq

	--Lấy lương QNCN
	select * into TBL_TCXN_QNCN from
	(select null STT, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sMaCheDo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, nGiaTri
	from TBL_TCXN
	where sMaCB like '2%') qncn
	--Lấy lương HSQ_BS
	select * into TBL_TCXN_HSQBS from
	(select null STT, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sMaCheDo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, nGiaTri
	from TBL_TCXN
	where sMaCB like '0%') hsqbs

	--Lấy lương VCQP
	select * into TBL_TCXN_VCQP from
	(select null STT, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sMaCheDo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, nGiaTri
	from TBL_TCXN
	where sMaCB in ('3.1', '3.2', '3.3', '413', '415')) vcqp

	--Lấy lương Lao động hợp đông
	select * into TBL_TCXN_LDHD from
	(select CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sMaCheDo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, nGiaTri
	from TBL_TCXN
	where sMaCB in ('43','423','425')) ldhd

	--Ket qua
	select result.* into TBL_TCXN_RESULT from
	(select STT, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, nGiaTri from TBL_TCXN_SQ
	union
	select STT, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, nGiaTri from TBL_TCXN_QNCN
	union
	select STT, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, nGiaTri from TBL_TCXN_HSQBS
	union
	select STT, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, nGiaTri from TBL_TCXN_VCQP
	union
	select STT, Ma_DonVi, TenDonVi,  sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, nGiaTri from TBL_TCXN_LDHD) result

	select
		CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT,
		Ma_DonVi MaDonVi,
		TenDonVi,
		sMaCB MaCb, 
		sMaCBo MaCbo, 
		sTenCbo TenCbo,
		sSoQuyetDinh,
		dNgayQuyetDinh,
		nGiaTri/@DonViTinh FTroCap1Lan
	from TBL_TCXN_RESULT
	order by TenDonVi
	
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCXN]') AND type in (N'U'))
	drop table TBL_TCXN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCXN_SQ]') AND type in (N'U'))
	drop table TBL_TCXN_SQ;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCXN_QNCN]') AND type in (N'U'))
	drop table TBL_TCXN_QNCN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCXN_HSQBS]') AND type in (N'U'))
	drop table TBL_TCXN_HSQBS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCXN_VCQP]') AND type in (N'U'))
	drop table TBL_TCXN_VCQP;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCXN_LDHD]') AND type in (N'U'))
	drop table TBL_TCXN_LDHD;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCXN_RESULT]') AND type in (N'U'))
	drop table TBL_TCXN_RESULT;
END
;
;
;
;
;
GO
