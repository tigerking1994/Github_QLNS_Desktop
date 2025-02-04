/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_tnld]    Script Date: 8/16/2024 2:41:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_luong_tro_cap_tnld]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_tnld]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_thai_san]    Script Date: 8/16/2024 2:41:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_luong_tro_cap_thai_san]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_thai_san]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_om_dau]    Script Date: 8/16/2024 2:41:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_luong_tro_cap_om_dau]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_om_dau]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_ht_pv_tv_tt]    Script Date: 8/16/2024 2:41:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_luong_tro_cap_ht_pv_tv_tt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_ht_pv_tv_tt]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_get_loaichi_thongtri]    Script Date: 8/16/2024 2:41:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_get_loaichi_thongtri]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_get_loaichi_thongtri]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_get_chung_tu_duoc_xem_luy_ke_dttm_bhyt]    Script Date: 8/16/2024 2:41:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_get_chung_tu_duoc_xem_luy_ke_dttm_bhyt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_get_chung_tu_duoc_xem_luy_ke_dttm_bhyt]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_dtt_get_so_quyet_dinh_bhxh_bhyt_bhtn]    Script Date: 8/16/2024 2:41:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_dtt_get_so_quyet_dinh_bhxh_bhyt_bhtn]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_dtt_get_so_quyet_dinh_bhxh_bhyt_bhtn]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_cp_get_donvi_thongtri]    Script Date: 8/16/2024 2:41:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_cp_get_donvi_thongtri]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_cp_get_donvi_thongtri]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_cp_get_donvi_thongtri]    Script Date: 8/16/2024 2:41:31 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_bhxh_dtt_get_so_quyet_dinh_bhxh_bhyt_bhtn]    Script Date: 8/16/2024 2:41:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_bhxh_dtt_get_so_quyet_dinh_bhxh_bhyt_bhtn]
	@NamLamViec int
AS
BEGIN
	select distinct  dtt.sSoQuyetDinh, Convert(varchar, dtt.dNgayQuyetDinh, 101) sNgayQuyetDinh, iLoaiDuToan
		from BH_DTT_BHXH_PhanBo_ChungTu dtt
		where iNamLamViec = @NamLamViec
	union
	select distinct  dtt.sSoQuyetDinh, Convert(varchar, dtt.dNgayQuyetDinh, 101) sNgayQuyetDinh, iLoaiDuToan
		from BH_DTTM_BHYT_ThanNhan_PhanBo dtt
		where iNamLamViec = @NamLamViec

END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_get_chung_tu_duoc_xem_luy_ke_dttm_bhyt]    Script Date: 8/16/2024 2:41:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bhxh_get_chung_tu_duoc_xem_luy_ke_dttm_bhyt]
	@NamLamViec int,
	@SoQuyetDinh nvarchar(500),
	@NgayQuyetDinh nvarchar(500)
AS
BEGIN
	
		select
		sSoChungTu,
		sSoQuyetDinh,
		dNgayQuyetDinh,
		sDS_IDMaDonVi 
	from BH_DTTM_BHYT_ThanNhan_PhanBo
	where iNamLamViec = @NamLamViec
		and sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) 
		and Convert(varchar, dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_get_loaichi_thongtri]    Script Date: 8/16/2024 2:41:31 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_ht_pv_tv_tt]    Script Date: 8/16/2024 2:41:31 PM ******/
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
	select 1 bHangCha, 'I' LoaiTC,  1 RowNum, null STT, 'Sĩ quan' DoiTuong, 'Sĩ quan' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'Sĩ quan' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'I' LoaiTC,  1 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'Sĩ quan' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fHUUTRI_TROCAP1LAN fTROCAP1LAN, fHUUTRI_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
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
	select 1 bHangCha, 'I' LoaiTC,  4 RowNum, null STT, 'CNVCQP' DoiTuong, 'CNVCQP' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'CNVCQP' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'I' LoaiTC,  4 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'CNVCQP' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fHUUTRI_TROCAP1LAN fTROCAP1LAN, fHUUTRI_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	from TBL_HUUTRI_DOC
	where sMaCB in ('3.1', '3.2', '3.3', '413', '415') and (isnull(fHUUTRI_TROCAP1LAN, 0) <> 0 or isnull(fHUUTRI_TROCAPKHUVUC, 0) <> 0)) vcqp
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_HUUTRI_VCQP) > 2
		update TBL_HUUTRI_VCQP set bHasData = 1

	--Lay tro cap Huu tri HDLD
	select * into TBL_HUUTRI_LDHD from
	(select 1 bHangCha, 'I' LoaiTC, 1 RowNum, 'I' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Hưu trí' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'I' LoaiTC,  5 RowNum, null STT, N'HĐLĐ' DoiTuong, N'HĐLĐ' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'HĐLĐ' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null  fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'I' LoaiTC,  5 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', N'HĐLĐ' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fHUUTRI_TROCAP1LAN fTROCAP1LAN, fHUUTRI_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
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
	select 1 bHangCha, 'II' LoaiTC, 1 RowNum, null STT, N'Sĩ quan' DoiTuong, N'Sĩ quan' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'SQ' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'II' LoaiTC, 1 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', N'Sĩ quan' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fPHUCVIEN_TROCAP1LAN fTROCAP1LAN, fPHUCVIEN_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
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
	select 1 bHangCha, 'II' LoaiTC, 4 RowNum, null STT, 'CNVCQP' DoiTuong, 'CNVCQP' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'CNVCQP' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'II' LoaiTC, 4 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'CNVCQP' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fPHUCVIEN_TROCAP1LAN fTROCAP1LAN, fPHUCVIEN_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	from TBL_PHUCVIEN_DOC
	where sMaCB in ('3.1', '3.2', '3.3', '413', '415') and (isnull(fPHUCVIEN_TROCAP1LAN, 0) <> 0 or isnull(fPHUCVIEN_TROCAPKHUVUC, 0) <> 0)) pvvcqp
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_PHUCVIEN_VCQP) > 2
		update TBL_PHUCVIEN_VCQP set bHasData = 1

	--Lay tro cap Phuc vien HDLD
	select * into TBL_PHUCVIEN_LDHD from
	(select 1 bHangCha, 'II' LoaiTC, 1 RowNum, 'II' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Phục viên' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'II' LoaiTC, 5 RowNum, null STT, N'HĐLĐ' DoiTuong, N'HĐLĐ' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'HĐLĐ' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null  fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'II' LoaiTC, 5 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', N'HĐLĐ' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fPHUCVIEN_TROCAP1LAN fTROCAP1LAN, fPHUCVIEN_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
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
	select 1 bHangCha, 'III' LoaiTC, 1 RowNum, null STT, N'Sĩ quan' DoiTuong, N'Sĩ quan' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'Sĩ quan' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'III' LoaiTC, 1 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', N'Sĩ quan' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTHOIVIEC_TROCAP1LAN fTROCAP1LAN, fTHOIVIEC_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
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
	select 1 bHangCha, 'III' LoaiTC, 4 RowNum, null STT, 'CNVCQP' DoiTuong, 'CNVCQP' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'VCQP' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
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
	select 1 bHangCha, 'III' LoaiTC, 5 RowNum, null STT, N'HĐLĐ' DoiTuong, N'HĐLĐ' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'HĐLĐ' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null  fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'III' LoaiTC, 5 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', N'HĐLĐ' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTHOIVIEC_TROCAP1LAN fTROCAP1LAN, fTHOIVIEC_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
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
	select 1 bHangCha, 'IV' LoaiTC, 1 RowNum, null STT, N'Sĩ quan' DoiTuong, N'Sĩ quan' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'Sĩ quan' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'IV' LoaiTC, 1 RowNum,
	CASE
		WHEN TBL.sMaCB IS NOT NULL THEN CAST(ROW_NUMBER() OVER (ORDER BY TBL.sMaCB) AS VARCHAR(6))
		ELSE CAST(ROW_NUMBER() OVER (ORDER BY TL.sMaCB) AS VARCHAR(6))
	END AS STT,
	 '', N'Sĩ quan' LoaiDoiTuong,
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
	where TBL.sMaCB like '1%' ) tvsq
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
	where tbl.sMaCB like '2%' ) tvqncn
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
	where tbl.sMaCB like '0%') tvhsqbs
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TUTUAT_HSQBS) > 2
		update TBL_TUTUAT_HSQBS set bHasData = 1

	--Lay tro cap Tu tuat VCQP
	select * into TBL_TUTUAT_VCQP from
	(select 1 bHangCha, 'IV' LoaiTC, 1 RowNum, 'IV' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Tử tuất' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'IV' LoaiTC, 4 RowNum, null STT, 'CNVCQP' DoiTuong, 'CNVCQP' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'CNVCQP' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'IV' LoaiTC, 4 RowNum,
	CASE
		WHEN TBL.sMaCB IS NOT NULL THEN CAST(ROW_NUMBER() OVER (ORDER BY TBL.sMaCB) AS VARCHAR(6))
		ELSE CAST(ROW_NUMBER() OVER (ORDER BY TL.sMaCB) AS VARCHAR(6))
	END AS STT,
	 '', 'CNVCQP' LoaiDoiTuong,
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
	where tbl.sMaCB  in ('3.1', '3.2', '3.3', '413', '415') ) tvvcqp
	
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TUTUAT_VCQP) > 2
		update TBL_TUTUAT_VCQP set bHasData = 1

	--Lay tro cap Tu tuat HDLD
	select * into TBL_TUTUAT_LDHD from
	(select 1 bHangCha, 'IV' LoaiTC, 1 RowNum, 'IV' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Tử tuất' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'IV' LoaiTC, 5 RowNum, null STT, N'HĐLĐ' DoiTuong, N'HĐLĐ' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'HĐLĐ' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null  fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'IV' LoaiTC, 5 RowNum,
	CASE
		WHEN TBL.sMaCB IS NOT NULL THEN CAST(ROW_NUMBER() OVER (ORDER BY TBL.sMaCB) AS VARCHAR(6))
		ELSE CAST(ROW_NUMBER() OVER (ORDER BY TL.sMaCB) AS VARCHAR(6))
	END AS STT,
	 '', N'HĐLĐ' LoaiDoiTuong,
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
	where tbl.sMaCB in ('43','423','425') ) tvldhd
	

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
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_om_dau]    Script Date: 8/16/2024 2:41:31 PM ******/
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
		1 bHangCha, 1 RowNum, 'I' STT, N'Sĩ Quan' DoiTuong, N'Sĩ Quan' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'Sĩ quan' sTenCbo, null SoNgayBenhDaiNgayD14Ngay, null SoNgayBenhDaiNgayT14Ngay, null SoNgayOmKhacD14Ngay, null SoNgayOmKhacT14Ngay, null SoNgayConOm, null SoNgayDuongSuc, null fLuongCanCu, null fBENHDAINGAY_D14NGAY, null fBENHDAINGAY_T14NGAY, null fOMKHAC_D14NGAY, null fOMKHAC_T14NGAY, null fCONOM, null fDUONGSUCPHSK, 0 bHasData
	union all
	select
		0 bHangCha, 1 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', N'Sĩ Quan' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK, 0 bHasData
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
		1 bHangCha, 4 RowNum, 'IV' STT, N'CNVCQP' DoiTuong, N'CNVCQP' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'CNVCQP' sTenCbo, null SoNgayBenhDaiNgayD14Ngay, null SoNgayBenhDaiNgayT14Ngay, null SoNgayOmKhacD14Ngay, null SoNgayOmKhacT14Ngay, null SoNgayConOm, null SoNgayDuongSuc, null fLuongCanCu, null fBENHDAINGAY_D14NGAY, null fBENHDAINGAY_T14NGAY, null fOMKHAC_D14NGAY, null fOMKHAC_T14NGAY, null fCONOM, null fDUONGSUCPHSK, 0 bHasData
	union all
	select
		0 bHangCha, 4 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', N'CNVCQP' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK, 0 bHasData
	from TBL_TCOD_DOC
	where sMaCB in ('3.1', '3.2', '3.3', '413', '415') and (isnull(fLuongCanCu, 0) <> 0 or isnull(fBENHDAINGAY_D14NGAY, 0) <> 0 or isnull(fBENHDAINGAY_T14NGAY, 0) <> 0 or isnull(fOMKHAC_D14NGAY, 0) <> 0 or isnull(fOMKHAC_T14NGAY, 0) <> 0 or isnull(fCONOM, 0) <> 0  or isnull(fDUONGSUCPHSK, 0) <> 0)) vcqp
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCOD_VCQP) > 1
		update TBL_TCOD_VCQP set bHasData = 1

	--Lấy lương Lao động hợp đông
	select * into TBL_TCOD_LDHD from
	(select
		1 bHangCha, 5 RowNum, 'V' STT, N'HĐLĐ' DoiTuong, N'HĐLĐ' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'LĐHĐ' sTenCbo, null SoNgayBenhDaiNgayD14Ngay, null SoNgayBenhDaiNgayT14Ngay, null SoNgayOmKhacD14Ngay, null SoNgayOmKhacT14Ngay, null SoNgayConOm, null SoNgayDuongSuc, null fLuongCanCu, null fBENHDAINGAY_D14NGAY, null fBENHDAINGAY_T14NGAY, null fOMKHAC_D14NGAY, null fOMKHAC_T14NGAY, null fCONOM, null fDUONGSUCPHSK, 0 bHasData
	union all
	select
		0 bHangCha, 5 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', N'HĐLĐ' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK, 0 bHasData
	from TBL_TCOD_DOC
	where sMaCB in ('43','423','425','3.4') and (isnull(fLuongCanCu, 0) <> 0 or isnull(fBENHDAINGAY_D14NGAY, 0) <> 0 or isnull(fBENHDAINGAY_T14NGAY, 0) <> 0 or isnull(fOMKHAC_D14NGAY, 0) <> 0 or isnull(fOMKHAC_T14NGAY, 0) <> 0 or isnull(fCONOM, 0) <> 0  or isnull(fDUONGSUCPHSK, 0) <> 0)) ldhd
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
	into TBL_TCOD_REPORT
	from TBL_TCOD_RESULT result
	left join TBL_MINUS_BHXH minus on result.sMaCBo = minus.sMaCBo and result.Ma_DonVi = minus.sMaDonVi
	left join TBL_MINUS_BHYT bhyt on result.sMaCBo = bhyt.sMaCBo and result.Ma_DonVi = bhyt.sMaDonVi
	order by RowNum, LoaiDoiTuong, DoiTuong desc

	--
	select row_number() over(order by STT) as row_rank, STT STT_R
	into TBL_RANK
	from TBL_TCOD_REPORT
	where IsHangCha = 1
	and IsHasData = 1

	--
	update TBL_TCOD_REPORT
	set STT = (select r.row_rank from TBL_RANK r where r.STT_R = STT)
	where IsHangCha = 1
	and IsHasData = 1
	--
	select * from TBL_TCOD_REPORT

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCOD]') AND type in (N'U')) drop table TBL_TCOD;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_MINUS_BHXH]') AND type in (N'U')) drop table TBL_MINUS_BHXH;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_MINUS_BHYT]') AND type in (N'U')) drop table TBL_MINUS_BHYT;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCOD_DOC]') AND type in (N'U')) drop table TBL_TCOD_DOC;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCOD_SQ]') AND type in (N'U')) drop table TBL_TCOD_SQ;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCOD_QNCN]') AND type in (N'U')) drop table TBL_TCOD_QNCN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCOD_HSQBS]') AND type in (N'U')) drop table TBL_TCOD_HSQBS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCOD_VCQP]') AND type in (N'U')) drop table TBL_TCOD_VCQP;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCOD_LDHD]') AND type in (N'U')) drop table TBL_TCOD_LDHD;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_RANK]') AND type in (N'U')) drop table TBL_RANK;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCOD_RESULT]') AND type in (N'U')) drop table TBL_TCOD_RESULT;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCOD_REPORT]') AND type in (N'U')) drop table TBL_TCOD_REPORT;

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
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_thai_san]    Script Date: 8/16/2024 2:41:31 PM ******/
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
		 1 bHangCha, 1 RowNum, 'I' STT, N'Sĩ Quan' DoiTuong, N'Sĩ Quan' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'Sĩ Quan' sTenCbo, null SoNgaySINHCON_NUOICON, null SoNgayTROCAP1LAN, null SoNgayKHAMTHAI, null SoNgayDUONGSUCPHSK, null fLuongCanCu, null fSINHCON_NUOICON, null fTROCAP1LAN, null fKHAMTHAI, null fDUONGSUCPHSK, 0 bHasData
	union all
	select
		0 bHangCha, 1 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', N'Sĩ Quan' LoaiDoiTuong, Ma_DonVi,Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgaySINHCON_NUOICON, SoNgayTROCAP1LAN, SoNgayKHAMTHAI, SoNgayDUONGSUCPHSK, fLuongCanCu, fSINHCON_NUOICON, fTROCAP1LAN, fKHAMTHAI, fDUONGSUCPHSK, 0 bHasData
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
		1 bHangCha, 4 RowNum, 'IV' STT, N'CNVCQP' DoiTuong, N'CNVCQP' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'CNVCQP' sTenCbo, null SoNgaySINHCON_NUOICON, null SoNgayTROCAP1LAN, null SoNgayKHAMTHAI, null SoNgayDUONGSUCPHSK, null fLuongCanCu, null fSINHCON_NUOICON, null fTROCAP1LAN, null fKHAMTHAI, null fDUONGSUCPHSK, 0 bHasData
	union all
	select
		0 bHangCha, 4 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', N'CNVCQP' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgaySINHCON_NUOICON, SoNgayTROCAP1LAN, SoNgayKHAMTHAI, SoNgayDUONGSUCPHSK, fLuongCanCu, fSINHCON_NUOICON, fTROCAP1LAN, fKHAMTHAI, fDUONGSUCPHSK, 0 bHasData
	from TBL_TCTS_DOC
	where sMaCB in ('3.1', '3.2', '3.3', '413', '415') and (isnull(fLuongCanCu, 0) <> 0 or isnull(fSINHCON_NUOICON, 0) <> 0 or isnull(fTROCAP1LAN, 0) <> 0 or isnull(fKHAMTHAI, 0) <> 0 or isnull(fDUONGSUCPHSK, 0) <> 0)) vcqp
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCTS_VCQP) > 1
		update TBL_TCTS_VCQP set bHasData = 1

	--Lấy lương Lao động hợp đông
	select * into TBL_TCTS_LDHD from
	(select
		1 bHangCha, 5 RowNum, 'V' STT, N'HĐLĐ' DoiTuong, N'HĐLĐ' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'HĐLĐ' sTenCbo, null SoNgaySINHCON_NUOICON, null SoNgayTROCAP1LAN, null SoNgayKHAMTHAI, null SoNgayDUONGSUCPHSK, null fLuongCanCu, null fSINHCON_NUOICON, null fTROCAP1LAN, null fKHAMTHAI, null fDUONGSUCPHSK, 0 bHasData
	union all
	select
		0 bHangCha, 5 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', N'HĐLĐ' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgaySINHCON_NUOICON, SoNgayTROCAP1LAN, SoNgayKHAMTHAI, SoNgayDUONGSUCPHSK, fLuongCanCu, fSINHCON_NUOICON, fTROCAP1LAN, fKHAMTHAI, fDUONGSUCPHSK, 0 bHasData
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_tnld]    Script Date: 8/16/2024 2:41:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_tnld] 
 	@DsMaDonVi nvarchar(100),
	@NamLamViec int ,
	@Thang int ,
	@DonViTinh int 
AS
BEGIN

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

	DECLARE @MaCheDo nvarchar(1000) = 'CHIGIAMDINH,HOTRO_CDNN,HOTRO_PHONGNGUA,TAINANLD_DUONGSUCPHSK,TAINANLD_TROCAP1LAN,TROCAPCHETDOTNLD,TROCAPPHCN,TROCAPPHUCVU,TROCAPTHEOPHIEUTRUYTRA,TROCAPHANGTHANG,CHIGIAMDINH_TRUYLINH,HOTRO_CDNN_TRUYLINH,HOTRO_PHONGNGUA_TRUYLINH,TAINANLD_DUONGSUCPHSK_TRUYLINH,TAINANLD_TROCAP1LAN_TRUYLINH,TROCAPCHETDOTNLD_TRUYLINH,TROCAPPHCN_TRUYLINH,TROCAPPHUCVU_TRUYLINH,TROCAPHANGTHANG_TRUYLINH,TROCAPTHEOPHIEUTRUYTRA_TRUYLINH'
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
		HOTROCDNN.nGiaTri fHoTroCdnn,
		HOTROPHONGNGUA.nGiaTri fHoTroPhongNgua,
		TROCAPCHETDOTNLD.nGiaTri fTroCapChetDoTNLD,
		TAINANLD_DUONGSUCPHSK.SoNgayDuongSucTNLD,
		TAINANLD_DUONGSUCPHSK.nGiaTri fDuongSucTNLD,
		chedocha.sSoQuyetDinh,
		chedocha.dNgayQuyetDinh,
		CHIGIAMDINH_TL.nGiaTri fChiGiamDinhTruyLinh,
		TROCAP1LAN_TL.nGiaTri fTroCap1LanTruyLinh,
		HOTRO_CDNN_TRUYLINH_TL.nGiaTri fHoTroCdnnTruyLinh,
		HOTRO_PHONGNGUA_TRUYLINH_TL.nGiaTri fHoTroPhongNguaTruyLinh,
		TROCAPHANGTHANG_TL.nGiaTri fTroCapHangThangTruyLinh,
		TROCAPPHCN_TL.nGiaTri fTroCapPHCNTruyLinh,
		TROCAPCHETDOTNLD_TL.nGiaTri fTroCapChetDoTNLDTruyLinh,
		TAINANLD_DUONGSUCPHSK_TL.SoNgayDuongSucTNLD SoNgayDuongSucTNLDTruyLinh,
		TAINANLD_DUONGSUCPHSK_TL.nGiaTri fDuongSucTNLDTruyLinh
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
		(select tnld_3_1.sMaDonVi, sum(tnld_3_1.nGiaTri) nGiaTri, tnld_3_1.sMaCB, tnld_3_1.sMaCBo, tnld_3_1.sTenCbo, sum(chedo.fSoNgayHuongBHXH) SoNgayHOTROCDNN
		from TBL_TCTNLD tnld_3_1 left join TL_CanBo_CheDoBHXH chedo on tnld_3_1.sMaCBo = chedo.sMaCanBo and tnld_3_1.sTenCbo = chedo.sMaCheDo
		where tnld_3_1.sMaCheDo in ('HOTRO_CDNN')
		group by tnld_3_1.sMaDonVi, tnld_3_1.sMaCB, tnld_3_1.sMaCBo, tnld_3_1.sTenCbo) HOTROCDNN
		on TBL_TCTNLD.sMaCBo = HOTROCDNN.sMaCBo and TBL_TCTNLD.sMaDonVi = HOTROCDNN.sMaDonVi
		left join
		(select tnld_3_2.sMaDonVi, sum(tnld_3_2.nGiaTri) nGiaTri, tnld_3_2.sMaCB, tnld_3_2.sMaCBo, tnld_3_2.sTenCbo, sum(chedo.fSoNgayHuongBHXH) SoNgayHOTROPHONGNGUA
		from TBL_TCTNLD tnld_3_2 left join TL_CanBo_CheDoBHXH chedo on tnld_3_2.sMaCBo = chedo.sMaCanBo and tnld_3_2.sTenCbo = chedo.sMaCheDo
		where tnld_3_2.sMaCheDo in ('HOTRO_PHONGNGUA')
		group by tnld_3_2.sMaDonVi, tnld_3_2.sMaCB, tnld_3_2.sMaCBo, tnld_3_2.sTenCbo) HOTROPHONGNGUA
		on TBL_TCTNLD.sMaCBo = HOTROPHONGNGUA.sMaCBo and TBL_TCTNLD.sMaDonVi = HOTROPHONGNGUA.sMaDonVi
		left join
		(select tnld_4.sMaDonVi, tnld_4.nGiaTri, tnld_4.sMaCB, tnld_4.sMaCBo, tnld_4.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTroCapChetDoTNLD
		from TBL_TCTNLD tnld_4 left join TL_CanBo_CheDoBHXH chedo on tnld_4.sMaCBo = chedo.sMaCanBo and tnld_4.sMaCheDo = chedo.sMaCheDo
		where tnld_4.sMaCheDo = 'TROCAPCHETDOTNLD') TROCAPCHETDOTNLD
		on TBL_TCTNLD.sMaCBo = TROCAPCHETDOTNLD.sMaCBo and TBL_TCTNLD.sMaDonVi = TROCAPCHETDOTNLD.sMaDonVi
		left join
		(select tnld_5.sMaDonVi, tnld_5.nGiaTri, tnld_5.sMaCB, tnld_5.sMaCBo, tnld_5.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayDuongSucTNLD
		from TBL_TCTNLD tnld_5 left join TL_CanBo_CheDoBHXH chedo on tnld_5.sMaCBo = chedo.sMaCanBo and tnld_5.sMaCheDo = chedo.sMaCheDo
		where tnld_5.sMaCheDo = 'TAINANLD_DUONGSUCPHSK') TAINANLD_DUONGSUCPHSK
		on TBL_TCTNLD.sMaCBo = TAINANLD_DUONGSUCPHSK.sMaCBo and TBL_TCTNLD.sMaDonVi = TAINANLD_DUONGSUCPHSK.sMaDonVi
		left join
		(select tnld_6.sMaDonVi, tnld_6.nGiaTri, tnld_6.sMaCB, tnld_6.sMaCBo, tnld_6.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayCHIGIAMDINH
		from TBL_TCTNLD tnld_6 left join TL_CanBo_CheDoBHXH chedo on tnld_6.sMaCBo = chedo.sMaCanBo and tnld_6.sMaCheDo = chedo.sMaCheDo
		where tnld_6.sMaCheDo = 'CHIGIAMDINH') CHIGIAMDINH
		on TBL_TCTNLD.sMaCBo = CHIGIAMDINH.sMaCBo and TBL_TCTNLD.sMaDonVi = CHIGIAMDINH.sMaDonVi

		-- TRUYLINH
		left join
		(select tnld.sMaDonVi, tnld.nGiaTri, tnld.sMaCB, tnld.sMaCBo, tnld.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTroCap1Lan
		from TBL_TCTNLD tnld left join TL_CanBo_CheDoBHXH chedo on tnld.sMaCBo = chedo.sMaCanBo and tnld.sMaCheDo = chedo.sMaCheDo
		where tnld.sMaCheDo = 'TAINANLD_TROCAP1LAN_TRUYLINH') TROCAP1LAN_TL
		on TBL_TCTNLD.sMaCBo = TROCAP1LAN_TL.sMaCBo and TBL_TCTNLD.sMaDonVi = TROCAP1LAN_TL.sMaDonVi
		left join
		(select tnld_1.sMaDonVi, tnld_1.nGiaTri, tnld_1.sMaCB, tnld_1.sMaCBo, tnld_1.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTroCapTheoPhieuTruyTra
		from TBL_TCTNLD tnld_1 left join TL_CanBo_CheDoBHXH chedo on tnld_1.sMaCBo = chedo.sMaCanBo and tnld_1.sMaCheDo = chedo.sMaCheDo
		where tnld_1.sMaCheDo = 'HOTRO_CDNN_TRUYLINH') HOTRO_CDNN_TRUYLINH_TL
		on TBL_TCTNLD.sMaCBo = HOTRO_CDNN_TRUYLINH_TL.sMaCBo and TBL_TCTNLD.sMaDonVi = HOTRO_CDNN_TRUYLINH_TL.sMaDonVi
		left join
		(select tnld_pn.sMaDonVi, tnld_pn.nGiaTri, tnld_pn.sMaCB, tnld_pn.sMaCBo, tnld_pn.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayHoTroPhongNgua
		from TBL_TCTNLD tnld_pn left join TL_CanBo_CheDoBHXH chedo on tnld_pn.sMaCBo = chedo.sMaCanBo and tnld_pn.sMaCheDo = chedo.sMaCheDo
		where tnld_pn.sMaCheDo = 'HOTRO_PHONGNGUA_TRUYLINH') HOTRO_PHONGNGUA_TRUYLINH_TL
		on TBL_TCTNLD.sMaCBo = HOTRO_PHONGNGUA_TRUYLINH_TL.sMaCBo and TBL_TCTNLD.sMaDonVi = HOTRO_PHONGNGUA_TRUYLINH_TL.sMaDonVi
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
		where tnld_5.sMaCheDo = 'TAINANLD_DUONGSUCPHSK_TRUYLINH ') TAINANLD_DUONGSUCPHSK_TL
		on TBL_TCTNLD.sMaCBo = TAINANLD_DUONGSUCPHSK_TL.sMaCBo and TBL_TCTNLD.sMaDonVi = TAINANLD_DUONGSUCPHSK_TL.sMaDonVi
		left join
		(select tnld_6.sMaDonVi, tnld_6.nGiaTri, tnld_6.sMaCB, tnld_6.sMaCBo, tnld_6.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayCHIGIAMDINH
		from TBL_TCTNLD tnld_6 left join TL_CanBo_CheDoBHXH chedo on tnld_6.sMaCBo = chedo.sMaCanBo and tnld_6.sMaCheDo = chedo.sMaCheDo
		where tnld_6.sMaCheDo = 'CHIGIAMDINH_TRUYLINH') CHIGIAMDINH_TL
		on TBL_TCTNLD.sMaCBo = CHIGIAMDINH_TL.sMaCBo and TBL_TCTNLD.sMaDonVi = CHIGIAMDINH_TL.sMaDonVi

	--Lấy lương Sĩ quan
	select * into TBL_TCTNLD_SQ from
	(select
		1 bHangCha, 1 RowNum, 'I' STT, N'Sĩ quan' DoiTuong, N'Sĩ quan' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'Sĩ quan' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null SoNgayDuongSucTNLD, null fChiGiamDinh, null fTroCap1Lan, null fTroCapTheoPhieuTruyTra, null fTroCapHangThang, null fTroCapPHCN, null fHoTroCdnn, null fHoTroPhongNgua, null fTroCapChetDoTNLD, null fDuongSucTNLD, 0 bHasData,
		null SoNgayDuongSucTNLDTruyLinh, null fChiGiamDinhTruyLinh, null fTroCap1LanTruyLinh, null fHoTroCdnnTruyLinh, null fHoTroPhongNguaTruyLinh, null fTroCapHangThangTruyLinh, null fTroCapPHCNTruyLinh, null fTroCapChetDoTNLDTruyLinh, null fDuongSucTNLDTruyLinh
	union all
	select
		0 bHangCha, 1 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', N'Sĩ quan' LoaiDoiTuong, Ma_DonVi,  Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fHoTroCdnn, fHoTroPhongNgua, fTroCapChetDoTNLD, fDuongSucTNLD,0 bHasData,
		SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fHoTroPhongNguaTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh
	from TBL_TCTNLD_DOC
	where sMaCB like '1%' and (isnull(fChiGiamDinh, 0) <> 0 or isnull(fTroCap1Lan, 0) <> 0 or isnull(fTroCapTheoPhieuTruyTra, 0) <> 0 or isnull(fTroCapHangThang, 0) <> 0 or isnull(fTroCapPHCN, 0) <> 0 or isnull(fHoTroCdnn, 0) <> 0 or isnull(fHoTroPhongNgua, 0) <> 0 or isnull(fTroCapChetDoTNLD, 0) <> 0 or isnull(fDuongSucTNLD, 0) <> 0 OR
				ISNULL(fChiGiamDinhTruyLinh,0) <> 0 OR ISNULL(fTroCap1LanTruyLinh,0) <> 0 OR ISNULL(fHoTroCdnnTruyLinh,0) <> 0 OR ISNULL(fHoTroPhongNguaTruyLinh,0) <> 0 OR ISNULL(fTroCapHangThangTruyLinh,0) <> 0 OR
				ISNULL( fTroCapPHCNTruyLinh,0) <> 0 OR ISNULL(fTroCapChetDoTNLDTruyLinh,0) <> 0 OR ISNULL( fDuongSucTNLDTruyLinh, 0) <> 0)
		) sq
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCTNLD_SQ) > 1
		update TBL_TCTNLD_SQ set bHasData = 1

	--Lấy lương QNCN
	select * into TBL_TCTNLD_QNCN from
	(select
		1 bHangCha, 2 RowNum, 'II' STT, 'QNCN' DoiTuong, 'QNCN' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'QNCN' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null SoNgayDuongSucTNLD, null fChiGiamDinh, null fTroCap1Lan, null fTroCapTheoPhieuTruyTra, null fTroCapHangThang, null fTroCapPHCN, null fHoTroCdnn, null fHoTroPhongNgua, null fTroCapChetDoTNLD, null fDuongSucTNLD, 0 bHasData,
		null SoNgayDuongSucTNLDTruyLinh, null fChiGiamDinhTruyLinh, null fTroCap1LanTruyLinh, null fHoTroCdnnTruyLinh, null fHoTroPhongNguaTruyLinh, null fTroCapHangThangTruyLinh, null fTroCapPHCNTruyLinh, null fTroCapChetDoTNLDTruyLinh, null fDuongSucTNLDTruyLinh
	union all
	select
		0 bHangCha, 2 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'QNCN' LoaiDoiTuong, Ma_DonVi,  Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fHoTroCdnn, fHoTroPhongNgua, fTroCapChetDoTNLD, fDuongSucTNLD, 0 bHasData,
		SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fHoTroPhongNguaTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh
	from TBL_TCTNLD_DOC
	where sMaCB like '2%' and (isnull(fChiGiamDinh, 0) <> 0 or isnull(fTroCap1Lan, 0) <> 0 or isnull(fTroCapTheoPhieuTruyTra, 0) <> 0 or isnull(fTroCapHangThang, 0) <> 0 or isnull(fTroCapPHCN, 0) <> 0 or isnull(fHoTroCdnn, 0) <> 0 or isnull(fHoTroPhongNgua, 0) <> 0 or isnull(fTroCapChetDoTNLD, 0) <> 0 or isnull(fDuongSucTNLD, 0) <> 0)) qncn
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCTNLD_QNCN) > 1
		update TBL_TCTNLD_QNCN set bHasData = 1

	--Lấy lương HSQ_BS
	select * into TBL_TCTNLD_HSQBS from
	(select
		1 bHangCha, 3 RowNum, 'III' STT, 'HSQ_BS' DoiTuong, 'HSQ_BS' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'HSQ_BS' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null SoNgayDuongSucTNLD, null fChiGiamDinh, null fTroCap1Lan, null fTroCapTheoPhieuTruyTra, null fTroCapHangThang, null fTroCapPHCN, null fHoTroCdnn, null fHoTroPhongNgua, null fTroCapChetDoTNLD, null fDuongSucTNLD, 0 bHasData,
		null SoNgayDuongSucTNLDTruyLinh, null fChiGiamDinhTruyLinh, null fTroCap1LanTruyLinh, null fHoTroCdnnTruyLinh, null fHoTroPhongNguaTruyLinh, null fTroCapHangThangTruyLinh, null fTroCapPHCNTruyLinh, null fTroCapChetDoTNLDTruyLinh, null fDuongSucTNLDTruyLinh
	union all
	select
		0 bHangCha, 3 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'HSQ_BS' LoaiDoiTuong, Ma_DonVi,  Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fHoTroCdnn, fHoTroPhongNgua, fTroCapChetDoTNLD, fDuongSucTNLD, 0 bHasData,
		SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fHoTroPhongNguaTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh
	from TBL_TCTNLD_DOC
	where sMaCB like '0%' and (isnull(fChiGiamDinh, 0) <> 0 or isnull(fTroCap1Lan, 0) <> 0 or isnull(fTroCapTheoPhieuTruyTra, 0) <> 0 or isnull(fTroCapHangThang, 0) <> 0 or isnull(fTroCapPHCN, 0) <> 0 or isnull(fHoTroCdnn, 0) <> 0 or isnull(fHoTroPhongNgua, 0) <> 0 or isnull(fTroCapChetDoTNLD, 0) <> 0 or isnull(fDuongSucTNLD, 0) <> 0 OR
				ISNULL(fChiGiamDinhTruyLinh,0) <> 0 OR ISNULL(fTroCap1LanTruyLinh,0) <> 0 OR ISNULL(fHoTroCdnnTruyLinh,0) <> 0 OR ISNULL(fHoTroPhongNguaTruyLinh,0) <> 0 OR ISNULL(fTroCapHangThangTruyLinh,0) <> 0 OR
				ISNULL( fTroCapPHCNTruyLinh,0) <> 0 OR ISNULL(fTroCapChetDoTNLDTruyLinh,0) <> 0 OR ISNULL( fDuongSucTNLDTruyLinh, 0) <> 0)
		)  hsqbs
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCTNLD_HSQBS) > 1
		update TBL_TCTNLD_HSQBS set bHasData = 1

	--Lấy lương VCQP
	select * into TBL_TCTNLD_VCQP from
	(select
		1 bHangCha, 4 RowNum, 'IV' STT, 'CNVCQP' DoiTuong, 'CNVCQP' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'CNVCQP' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null SoNgayDuongSucTNLD, null fChiGiamDinh, null fTroCap1Lan, null fTroCapTheoPhieuTruyTra, null fTroCapHangThang, null fTroCapPHCN, null fHoTroCdnn, null fHoTroPhongNgua, null fTroCapChetDoTNLD, null fDuongSucTNLD, 0 bHasData,
		null SoNgayDuongSucTNLDTruyLinh, null fChiGiamDinhTruyLinh, null fTroCap1LanTruyLinh, null fHoTroCdnnTruyLinh, null fHoTroPhongNguaTruyLinh, null fTroCapHangThangTruyLinh, null fTroCapPHCNTruyLinh, null fTroCapChetDoTNLDTruyLinh, null fDuongSucTNLDTruyLinh
	union all
	select
		0 bHangCha, 4 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'CNVCQP' LoaiDoiTuong, Ma_DonVi,  Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fHoTroCdnn, fHoTroPhongNgua, fTroCapChetDoTNLD, fDuongSucTNLD, 0 bHasData,
		SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fHoTroPhongNguaTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh
	from TBL_TCTNLD_DOC
	where sMaCB in ('3.1', '3.2', '3.3', '413', '415') and (isnull(fChiGiamDinh, 0) <> 0 or isnull(fTroCap1Lan, 0) <> 0 or isnull(fTroCapTheoPhieuTruyTra, 0) <> 0 or isnull(fTroCapHangThang, 0) <> 0 or isnull(fTroCapPHCN, 0) <> 0 or isnull(fHoTroCdnn, 0) <> 0 or isnull(fHoTroPhongNgua, 0) <> 0 or isnull(fTroCapChetDoTNLD, 0) <> 0 or isnull(fDuongSucTNLD, 0) <> 0)) vcqp
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCTNLD_VCQP) > 1
		update TBL_TCTNLD_VCQP set bHasData = 1

	--Lấy lương Lao Dộng hợp Dông
	select * into TBL_TCTNLD_LDHD from
	(select
		1 bHangCha, 5 RowNum, 'V' STT, N'HĐLĐ' DoiTuong, N'HĐLĐ' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'HĐLĐ' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null SoNgayDuongSucTNLD, null fChiGiamDinh, null fTroCap1Lan, null fTroCapTheoPhieuTruyTra, null fTroCapHangThang, null fTroCapPHCN, null fHoTroCdnn, null fHoTroPhongNgua, null fTroCapChetDoTNLD, null fDuongSucTNLD, 0 bHasData,
		null SoNgayDuongSucTNLDTruyLinh, null fChiGiamDinhTruyLinh, null fTroCap1LanTruyLinh, null fHoTroCdnnTruyLinh, null fHoTroPhongNguaTruyLinh, null fTroCapHangThangTruyLinh, null fTroCapPHCNTruyLinh, null fTroCapChetDoTNLDTruyLinh, null fDuongSucTNLDTruyLinh
	union all
	select
		0 bHangCha, 5 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', N'HĐLĐ' LoaiDoiTuong, Ma_DonVi,  Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fHoTroCdnn, fHoTroPhongNgua, fTroCapChetDoTNLD, fDuongSucTNLD, 0 bHasData,
		SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fHoTroPhongNguaTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh
	from TBL_TCTNLD_DOC
	where sMaCB in ('43','423','425') and (isnull(fChiGiamDinh, 0) <> 0 or isnull(fTroCap1Lan, 0) <> 0 or isnull(fTroCapTheoPhieuTruyTra, 0) <> 0 or isnull(fTroCapHangThang, 0) <> 0 or isnull(fTroCapPHCN, 0) <> 0 or isnull(fHoTroCdnn, 0) <> 0 or isnull(fHoTroPhongNgua, 0) <> 0 or isnull(fTroCapChetDoTNLD, 0) <> 0 or isnull(fDuongSucTNLD, 0) <> 0 OR
				ISNULL(fChiGiamDinhTruyLinh,0) <> 0 OR ISNULL(fTroCap1LanTruyLinh,0) <> 0 OR ISNULL(fHoTroCdnnTruyLinh,0) <> 0 OR ISNULL(fHoTroPhongNguaTruyLinh,0) <> 0 OR ISNULL(fTroCapHangThangTruyLinh,0) <> 0 OR
				ISNULL( fTroCapPHCNTruyLinh,0) <> 0 OR ISNULL(fTroCapChetDoTNLDTruyLinh,0) <> 0 OR ISNULL( fDuongSucTNLDTruyLinh, 0) <> 0)
		)  ldhd
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCTNLD_LDHD) > 1
		update TBL_TCTNLD_LDHD set bHasData = 1

	--Ket qua
	select result.* into TBL_TCTNLD_RESULT from
	(select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fHoTroCdnn, fHoTroPhongNgua, fTroCapChetDoTNLD, fDuongSucTNLD,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fHoTroCdnn, 0) + isnull(fHoTroPhongNgua, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) fTongSoTienThangNay,
	SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fHoTroPhongNguaTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh,
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fHoTroPhongNguaTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) fTongSoTienTruyLinh,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fHoTroCdnn, 0) + isnull(fHoTroPhongNgua, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) +
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fHoTroPhongNguaTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) FTongSoTien
	from TBL_TCTNLD_SQ
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fHoTroCdnn, fHoTroPhongNgua, fTroCapChetDoTNLD, fDuongSucTNLD,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fHoTroCdnn, 0) + isnull(fHoTroPhongNgua, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) fTongSoTienThangNay,
	SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fHoTroPhongNguaTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh,
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fHoTroPhongNguaTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) fTongSoTienTruyLinh,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fHoTroCdnn, 0) + isnull(fHoTroPhongNgua, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) +
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fHoTroPhongNguaTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) FTongSoTien
	from TBL_TCTNLD_QNCN
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fHoTroCdnn, fHoTroPhongNgua, fTroCapChetDoTNLD, fDuongSucTNLD,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fHoTroCdnn, 0) + isnull(fHoTroPhongNgua, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) fTongSoTienThangNay,
	SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fHoTroPhongNguaTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh,
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fHoTroPhongNguaTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) fTongSoTienTruyLinh,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fHoTroCdnn, 0) + isnull(fHoTroPhongNgua, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) +
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fHoTroPhongNguaTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) FTongSoTien
	from TBL_TCTNLD_HSQBS
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fHoTroCdnn, fHoTroPhongNgua, fTroCapChetDoTNLD, fDuongSucTNLD,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fHoTroCdnn, 0) + isnull(fHoTroPhongNgua, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) fTongSoTienThangNay,
	SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fHoTroPhongNguaTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh,
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fHoTroPhongNguaTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) fTongSoTienTruyLinh,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fHoTroCdnn, 0) + isnull(fHoTroPhongNgua, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) +
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fHoTroPhongNguaTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) FTongSoTien
	from TBL_TCTNLD_VCQP
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fHoTroCdnn, fHoTroPhongNgua, fTroCapChetDoTNLD, fDuongSucTNLD,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fHoTroCdnn, 0) + isnull(fHoTroPhongNgua, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) fTongSoTienThangNay,
	SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fHoTroPhongNguaTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh,
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fHoTroPhongNguaTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) fTongSoTienTruyLinh,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fHoTroCdnn, 0) + isnull(fHoTroPhongNgua, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) +
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fHoTroPhongNguaTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) FTongSoTien
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
		fHoTroCdnn/@DonViTinh fHoTroCdnn,
		fHoTroPhongNgua/@DonViTinh fHoTroPhongNgua,
		fTroCapChetDoTNLD/@DonViTinh fTroCapChetDoTNLD,
		fDuongSucTNLD/@DonViTinh fDuongSucTNLD,
		fTongSoTienThangNay/@DonViTinh fTongSoTienThangNay,
		bHangCha IsHangCha,
		bHasData IsHasData,
		SoNgayDuongSucTNLDTruyLinh,
		fChiGiamDinhTruyLinh/@DonViTinh fChiGiamDinhTruyLinh,
		fTroCap1LanTruyLinh/@DonViTinh fTroCap1LanTruyLinh,
		fHoTroCdnnTruyLinh/@DonViTinh fHoTroCdnnTruyLinh,
		fHoTroPhongNguaTruyLinh/@DonViTinh fHoTroPhongNguaTruyLinh,
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
;
;
GO

INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) VALUES (N'64f612b3-5448-4fa3-bd45-fb98b2e08105', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rptBH_CHI_BHXH_CapPhat_Thong_Tri_Type', NULL, N'rptBH_CHI_BHXH_CapPhat_Thong_Tri_Type', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', N'THÔNG TRI', NULL, N'Cấp kinh phí chi BHXH, BHYT', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

/****** Object:  StoredProcedure [dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_kcb_quanydonvi]    Script Date: 8/16/2024 3:10:24 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_kcb_quanydonvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_kcb_quanydonvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_kcb_quanydonvi]    Script Date: 8/16/2024 3:10:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_kcb_quanydonvi]
@INamLamViec int,
@IdDonVi nvarchar(max),
@DonViTinh int
AS
BEGIN
	select 
		dv.sTenDonVi as sTenDonVi,
		sum(ct.fDuToanNamTruoc) /@DonViTinh fDuToanNamTruoc,
		sum(ct.fDuToanNamNay) /@DonViTinh fDuToanNamNay,
		sum(ct.fQuyetToan) /@DonViTinh fQuyetToan
	from
	(select 
	iID_MaDonVi,
	case ima when 220 then fSoThamDinh
			else 0 end as fDuToanNamTruoc,
	case ima when 221 then fSoThamDinh 
			else 0 end as fDuToanNamNay,
	case ima when 223 then fSoThamDinh
			else 0 end as fQuyetToan
	from BH_ThamDinhQuyetToan_ChungTuChiTiet
	where iID_MaDonVi in (select * from f_split(@IdDonVi))
	and iNamLamViec = @INamLamViec) ct
	left join (SELECT * from DonVi WHERE iNamLamViec = @INamLamViec AND iTrangThai = 1) dv ON dv.iID_MaDonVi = ct.iID_MaDonVi
	where fDuToanNamTruoc > 0 or fDuToanNamNay > 0 or fQuyetToan > 0
	group by ct.iID_MaDonVi, dv.sTenDonVi
	order by ct.iID_MaDonVi
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_kht_du_toan_thu_bhxh_bhyt_bhtn]    Script Date: 8/16/2024 5:41:02 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_kht_du_toan_thu_bhxh_bhyt_bhtn]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_kht_du_toan_thu_bhxh_bhyt_bhtn]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_kht_du_toan_thu_bhxh_bhyt_bhtn]    Script Date: 8/16/2024 5:41:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_kht_du_toan_thu_bhxh_bhyt_bhtn] 
	@NamLamViec int,
	@LstSelectedUnit ntext,
	@SoQuyetDinh nvarchar(500),
	@NgayQuyetDinh nvarchar(500),
	@DVT int,
	@IsMillionRound bit
AS
BEGIN
	SELECT 
		   dt_dv.sTenDonVi,
		   dt_dv.id idDonVi,
		   A.*
		   into #tempPhanBoDuLieu
		FROM
		  (SELECT 

				ctct.*

		   FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
		   LEFT JOIN BH_DTT_BHXH_PhanBo_ChungTu ct ON ct.iID_DTT_BHXH_PhanBo_ChungTu = ctct.iID_DTT_BHXH_ChungTu
		   WHERE ct.iNamLamViec = @NamLamViec
		   AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh))
		   AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))) AS A 
		   JOIN
		  (SELECT iID_MaDonVi AS id,
				  sTenDonVi, iLoai
		   FROM DonVi
		   WHERE iTrangThai = 1
		   AND iNamLamViec = @NamLamViec
		   AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi=dt_dv.id

	
	SELECT
		dt_dv.id,
		dt_dv.sTenDonVi,
		A.*
		into #tempNhanPbDuLieu
		FROM
		  (SELECT
			ctct.*
		   FROM BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct
		   JOIN BH_DTTM_BHYT_ThanNhan_PhanBo ct ON ct.iID_DTTM_BHYT_ThanNhan_PhanBo = ctct.iID_DTTM_BHYT_ThanNhan_PhanBo
		   WHERE ct.iNamLamViec = @namLamViec
		   AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@soQuyetDinh))
		   AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@ngayQuyetDinh))
		   ) AS A 
		   JOIN
		  (SELECT iID_MaDonVi AS id,
				  sTenDonVi, iLoai
		   FROM DonVi
		   WHERE iTrangThai = 1
		   AND iNamLamViec = @namLamViec
		   AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi = dt_dv.id




		select 
		'' STT
		,N'TỔNG CỘNG' STenDonVi
		,'' iID_MaDonVi
		,0 FTienBHXHNLD
		,0 FTienBHXHNSDLDD
		,0 FTienCongBHXH
		,0 FTienBHTNNLD
		,0 FTienBHTNNSDLDD
		,0 FTienCongBHTN
		,0 FTienBHYTNLDNLD
		,0 FTienBHYTNSDLDD
		,0 FTienCongBHYTNLD
		,0 FTienBHYTQNNLD
		,0 FTienBHYTQNNSDNLD
		,0 FTienCongBHYTQN
		,0 FTienBHYTTNQN
		,0 FTienBHYTTNCNCNVQP
		,0 FTienCongBHYTTN
		,0 FTienCongALL
		,0 Type
		,1 bHangCha
		into #tempTotal

	
		select 
		'I' STT
		,N'KHỐI DỰ TOÁN' STenDonVi
		,'' iID_MaDonVi
		,0 FTienBHXHNLD
		,0 FTienBHXHNSDLDD
		,0 FTienCongBHXH
		,0 FTienBHTNNLD
		,0 FTienBHTNNSDLDD
		,0 FTienCongBHTN
		,0 FTienBHYTNLDNLD
		,0 FTienBHYTNSDLDD
		,0 FTienCongBHYTNLD
		,0 FTienBHYTQNNLD
		,0 FTienBHYTQNNSDNLD
		,0 FTienCongBHYTQN
		,0 FTienBHYTTNQN
		,0 FTienBHYTTNCNCNVQP
		,0 FTienCongBHYTTN
		,0 FTienCongALL
		,1 Type
		,1 bHangCha
		into #tempKhoiDuToan

		select 
		'II' STT
		,N'KHỐI HẠCH TOÁN' STenDonVi
		,'' iID_MaDonVi
		,0 FTienBHXHNLD
		,0 FTienBHXHNSDLDD
		,0 FTienCongBHXH
		,0 FTienBHTNNLD
		,0 FTienBHTNNSDLDD
		,0 FTienCongBHTN
		,0 FTienBHYTNLDNLD
		,0 FTienBHYTNSDLDD
		,0 FTienCongBHYTNLD
		,0 FTienBHYTQNNLD
		,0 FTienBHYTQNNSDNLD
		,0 FTienCongBHYTQN
		,0 FTienBHYTTNQN
		,0 FTienBHYTTNCNCNVQP
		,0 FTienCongBHYTTN
		,0 FTienCongALL
		,1 Type
		,1 bHangCha
		into #tempKhoiHachToan

--- Thu BHXH - NLĐ đóng
-- KHỐI DỰ TOÁN
	Select * into #tempDetailKhoiDuToan
		from
		(Select 
		sTenDonVi
		,iID_MaDonVi
		,sum(fBHXH_NLD) FTienBHXHNLD
		,0 FTienBHXHNSDLDD
		,0 FTienCongBHXH
		,0 FTienBHTNNLD
		,0 FTienBHTNNSDLDD
		,0 FTienCongBHTN
		,0 FTienBHYTNLDNLD
		,0 FTienBHYTNSDLDD
		,0 FTienCongBHYTNLD
		,0 FTienBHYTQNNLD
		,0 FTienBHYTQNNSDNLD
		,0 FTienCongBHYTQN
		,0 FTienBHYTTNQN
		,0 FTienBHYTTNCNCNVQP
		,0 FTienCongBHYTTN
		,0 FTienCongALL
		from  
		#tempPhanBoDuLieu 
		where (sXauNoiMa like '9020001-010-011-0001%' 
		OR sXauNoiMa like '9020001-010-011-0002%' )
		AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		Group by sTenDonVi
		,iID_MaDonVi
		Union all
	--- Thu BHXH - NSD đóng
	-- KHỐI DỰ TOÁN
		Select 
		sTenDonVi
		,iID_MaDonVi
		,0 FTienBHXHNLD
		,sum(fBHXH_NSD)  FTienBHXHNSDLDD
		,0 FTienCongBHXH
		,0 FTienBHTNNLD
		,0 FTienBHTNNSDLDD
		,0 FTienCongBHTN
		,0 FTienBHYTNLDNLD
		,0 FTienBHYTNSDLDD
		,0 FTienCongBHYTNLD
		,0 FTienBHYTQNNLD
		,0 FTienBHYTQNNSDNLD
		,0 FTienCongBHYTQN
		,0 FTienBHYTTNQN
		,0 FTienBHYTTNCNCNVQP
		,0 FTienCongBHYTTN
		,0 FTienCongALL
		from 
		#tempPhanBoDuLieu 
		where (sXauNoiMa like '9020001-010-011-0001%' OR sXauNoiMa like '9020001-010-011-0002%') 
		AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		Group by sTenDonVi
		,iID_MaDonVi
		Union all
	--Thu BHTN- NLĐ đóng
	-- KHỐI DỰ TOÁN
		Select 
		sTenDonVi
		,iID_MaDonVi
		,0 FTienBHXHNLD
		,0  FTienBHXHNSDLDD
		,0 FTienCongBHXH
		,sum(fBHTN_NLD) FTienBHTNNLD
		,0 FTienBHTNNSDLDD
		,0 FTienCongBHTN
		,0 FTienBHYTNLDNLD
		,0 FTienBHYTNSDLDD
		,0 FTienCongBHYTNLD
		,0 FTienBHYTQNNLD
		,0 FTienBHYTQNNSDNLD
		,0 FTienCongBHYTQN
		,0 FTienBHYTTNQN
		,0 FTienBHYTTNCNCNVQP
		,0 FTienCongBHYTTN
		,0 FTienCongALL
		from  
		#tempPhanBoDuLieu
		where (sXauNoiMa like '9020001-010-011-0001%' OR sXauNoiMa like '9020001-010-011-0002%') 
		AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		Group by sTenDonVi
		,iID_MaDonVi

		Union all
	--Thu BHTN - NSD đóng
	-- KHỐI DỰ TOÁN
		Select 
		sTenDonVi
		,iID_MaDonVi
		,0 FTienBHXHNLD
		,0  FTienBHXHNSDLDD
		,0 FTienCongBHXH
		,0 FTienBHTNNLD
		,sum(fBHTN_NSD) FTienBHTNNSDLDD
		,0 FTienCongBHTN
		,0 FTienBHYTNLDNLD
		,0 FTienBHYTNSDLDD
		,0 FTienCongBHYTNLD
		,0 FTienBHYTQNNLD
		,0 FTienBHYTQNNSDNLD
		,0 FTienCongBHYTQN
		,0 FTienBHYTTNQN
		,0 FTienBHYTTNCNCNVQP
		,0 FTienCongBHYTTN
		,0 FTienCongALL
		from 
		#tempPhanBoDuLieu 
		where (sXauNoiMa like '9020001-010-011-0001%' OR sXauNoiMa like '9020001-010-011-0002%') 
		AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		Group by sTenDonVi
		,iID_MaDonVi

		Union all
	-- BHYT NGƯỜI LĐ -  NLĐ đóng
	-- KHỐI DỰ TOÁN
		Select
		sTenDonVi
		,iID_MaDonVi
		,0 FTienBHXHNLD
		,0  FTienBHXHNSDLDD
		,0 FTienCongBHXH
		,0 FTienBHTNNLD
		,0 FTienBHTNNSDLDD
		,0 FTienCongBHTN
		,sum(fBHYT_NLD) FTienBHYTNLDNLD
		,0 FTienBHYTNSDLDD
		,0 FTienCongBHYTNLD
		,0 FTienBHYTQNNLD
		,0 FTienBHYTQNNSDNLD
		,0 FTienCongBHYTQN
		,0 FTienBHYTTNQN
		,0 FTienBHYTTNCNCNVQP
		,0 FTienCongBHYTTN
		,0 FTienCongALL
		from 
		#tempPhanBoDuLieu
		where sXauNoiMa like '9020001-010-011-0002%'  
		AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		Group by sTenDonVi
		,iID_MaDonVi

		Union all
	--BHYT NGƯỜI LĐ -  NSD LĐ đóng
	-- KHỐI DỰ TOÁN
		Select
		sTenDonVi
		,iID_MaDonVi
		,0 FTienBHXHNLD
		,0  FTienBHXHNSDLDD
		,0 FTienCongBHXH
		,0 FTienBHTNNLD
		,0 FTienBHTNNSDLDD
		,0 FTienCongBHTN
		,0 FTienBHYTNLDNLD
		,sum(fBHYT_NSD) FTienBHYTNSDLDD
		,0 FTienCongBHYTNLD
		,0 FTienBHYTQNNLD
		,0 FTienBHYTQNNSDNLD
		,0 FTienCongBHYTQN
		,0 FTienBHYTTNQN
		,0 FTienBHYTTNCNCNVQP
		,0 FTienCongBHYTTN
		,0 FTienCongALL
	 from 
		#tempPhanBoDuLieu
		where sXauNoiMa like '9020001-010-011-0002%'  
		AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		Group by sTenDonVi
		,iID_MaDonVi

		Union all
	--BHYT QN - NLĐ đóng
	--- KHỐI DỰ TOÁN
		Select
		sTenDonVi
		,iID_MaDonVi
		,0 FTienBHXHNLD
		,0  FTienBHXHNSDLDD
		,0 FTienCongBHXH
		,0 FTienBHTNNLD
		,0 FTienBHTNNSDLDD
		,0 FTienCongBHTN
		,0 FTienBHYTNLDNLD
		,0 FTienBHYTNSDLDD
		,0 FTienCongBHYTNLD
		,sum(fBHYT_NLD) FTienBHYTQNNLD
		,0 FTienBHYTQNNSDNLD
		,0 FTienCongBHYTQN
		,0 FTienBHYTTNQN
		,0 FTienBHYTTNCNCNVQP
		,0 FTienCongBHYTTN
		,0 FTienCongALL 
		from 
		#tempPhanBoDuLieu 
		where sXauNoiMa like '9020001-010-011-0001%' 
		AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		Group by sTenDonVi
		,iID_MaDonVi

		Union all
		--- BHYT QN - NSD LĐ đóng
		--- KHỐI DỰ TOÁN
		Select
		sTenDonVi
		,iID_MaDonVi
		,0 FTienBHXHNLD
		,0  FTienBHXHNSDLDD
		,0 FTienCongBHXH
		,0 FTienBHTNNLD
		,0 FTienBHTNNSDLDD
		,0 FTienCongBHTN
		,0 FTienBHYTNLDNLD
		,0 FTienBHYTNSDLDD
		,0 FTienCongBHYTNLD
		,0 FTienBHYTQNNLD
		,sum(fBHYT_NSD)  FTienBHYTQNNSDNLD
		,0 FTienCongBHYTQN
		,0 FTienBHYTTNQN
		,0 FTienBHYTTNCNCNVQP
		,0 FTienCongBHYTTN
		,0 FTienCongALL 
		from #tempPhanBoDuLieu 
		where sXauNoiMa like '9020001-010-011-0001%' 
		AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		Group by sTenDonVi
		,iID_MaDonVi

		Union all
	--Thẻ BHYT thân nhân - Quân nhân
	--- KHỐI DỰ TOÁN
		Select
		sTenDonVi
		,iID_MaDonVi
		,0 FTienBHXHNLD
		,0  FTienBHXHNSDLDD
		,0 FTienCongBHXH
		,0 FTienBHTNNLD
		,0 FTienBHTNNSDLDD
		,0 FTienCongBHTN
		,0 FTienBHYTNLDNLD
		,0 FTienBHYTNSDLDD
		,0 FTienCongBHYTNLD
		,0 FTienBHYTQNNLD
		,0  FTienBHYTQNNSDNLD
		,0 FTienCongBHYTQN
		,sum(fDuToan) FTienBHYTTNQN
		,0 FTienBHYTTNCNCNVQP
		,0 FTienCongBHYTTN
		,0 FTienCongALL 
		from #tempNhanPbDuLieu 
		where sXauNoiMa like '9030001-010-011-0001%' 
		AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		Group by sTenDonVi
		,iID_MaDonVi

		Union all
	-- Thẻ BHYT thân nhân - CN & CNVQP
	-- KHỐI DỰ TOÁN
		Select
		sTenDonVi
		,iID_MaDonVi
		,0 FTienBHXHNLD
		,0  FTienBHXHNSDLDD
		,0 FTienCongBHXH
		,0 FTienBHTNNLD
		,0 FTienBHTNNSDLDD
		,0 FTienCongBHTN
		,0 FTienBHYTNLDNLD
		,0 FTienBHYTNSDLDD
		,0 FTienCongBHYTNLD
		,0 FTienBHYTQNNLD
		,0  FTienBHYTQNNSDNLD
		,0 FTienCongBHYTQN
		,0 FTienBHYTTNQN
		,sum(fDuToan) FTienBHYTTNCNCNVQP
		,0 FTienCongBHYTTN
		,0 FTienCongALL 
		from #tempNhanPbDuLieu 
		where sXauNoiMa like '9030002-010-011-0000%' 
		AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit)) 
		Group by sTenDonVi
		,iID_MaDonVi )KhoiDuToan

--- Thu BHXH - NLĐ đóng
-- KHỐI HẠCH TOÁN
	Select * into #tempDetailKhoiHachToan
		from
		(Select 
		sTenDonVi
		,iID_MaDonVi
		,sum(fBHXH_NLD) FTienBHXHNLD
		,0 FTienBHXHNSDLDD
		,0 FTienCongBHXH
		,0 FTienBHTNNLD
		,0 FTienBHTNNSDLDD
		,0 FTienCongBHTN
		,0 FTienBHYTNLDNLD
		,0 FTienBHYTNSDLDD
		,0 FTienCongBHYTNLD
		,0 FTienBHYTQNNLD
		,0 FTienBHYTQNNSDNLD
		,0 FTienCongBHYTQN
		,0 FTienBHYTTNQN
		,0 FTienBHYTTNCNCNVQP
		,0 FTienCongBHYTTN
		,0 FTienCongALL
		from  
		#tempPhanBoDuLieu 
		where (sXauNoiMa like '9020002-010-011-0001%' OR sXauNoiMa like '9020002-010-011-0002%')
		AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		Group by sTenDonVi
		,iID_MaDonVi

		Union all
	--- Thu BHXH - NSD đóng
	-- KHỐI HẠCH TOÁN
		Select 
		sTenDonVi
		,iID_MaDonVi
		,0 FTienBHXHNLD
		,sum(fBHXH_NSD)  FTienBHXHNSDLDD
		,0 FTienCongBHXH
		,0 FTienBHTNNLD
		,0 FTienBHTNNSDLDD
		,0 FTienCongBHTN
		,0 FTienBHYTNLDNLD
		,0 FTienBHYTNSDLDD
		,0 FTienCongBHYTNLD
		,0 FTienBHYTQNNLD
		,0 FTienBHYTQNNSDNLD
		,0 FTienCongBHYTQN
		,0 FTienBHYTTNQN
		,0 FTienBHYTTNCNCNVQP
		,0 FTienCongBHYTTN
		,0 FTienCongALL
		from 
		#tempPhanBoDuLieu 
		where (sXauNoiMa like '9020002-010-011-0001%' OR sXauNoiMa like '9020002-010-011-0002%') 
		AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		Group by sTenDonVi
		,iID_MaDonVi

		Union all
	--Thu BHTN- NLĐ đóng
	-- KHỐI HẠCH TOÁN
		Select 
		sTenDonVi
		,iID_MaDonVi
		,0 FTienBHXHNLD
		,0  FTienBHXHNSDLDD
		,0 FTienCongBHXH
		,sum(fBHTN_NLD) FTienBHTNNLD
		,0 FTienBHTNNSDLDD
		,0 FTienCongBHTN
		,0 FTienBHYTNLDNLD
		,0 FTienBHYTNSDLDD
		,0 FTienCongBHYTNLD
		,0 FTienBHYTQNNLD
		,0 FTienBHYTQNNSDNLD
		,0 FTienCongBHYTQN
		,0 FTienBHYTTNQN
		,0 FTienBHYTTNCNCNVQP
		,0 FTienCongBHYTTN
		,0 FTienCongALL
		from  
		#tempPhanBoDuLieu
		where (sXauNoiMa like '9020002-010-011-0001%' OR sXauNoiMa like '9020002-010-011-0002%')
		AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		Group by sTenDonVi
		,iID_MaDonVi

		Union all
	--Thu BHTN - NSD đóng
	-- KHỐI HẠCH TOÁN
		Select 
		sTenDonVi
		,iID_MaDonVi
		,0 FTienBHXHNLD
		,0  FTienBHXHNSDLDD
		,0 FTienCongBHXH
		,0 FTienBHTNNLD
		,sum(fBHTN_NSD) FTienBHTNNSDLDD
		,0 FTienCongBHTN
		,0 FTienBHYTNLDNLD
		,0 FTienBHYTNSDLDD
		,0 FTienCongBHYTNLD
		,0 FTienBHYTQNNLD
		,0 FTienBHYTQNNSDNLD
		,0 FTienCongBHYTQN
		,0 FTienBHYTTNQN
		,0 FTienBHYTTNCNCNVQP
		,0 FTienCongBHYTTN
		,0 FTienCongALL
		from 
		#tempPhanBoDuLieu 
		where (sXauNoiMa like '9020002-010-011-0001%' OR sXauNoiMa like '9020002-010-011-0002%')
		AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		Group by sTenDonVi
		,iID_MaDonVi
	
		Union all
	-- BHYT NGƯỜI LĐ -  NLĐ đóng
	--- KHỐI HẠCH TOÁN
		Select
		sTenDonVi
		,iID_MaDonVi
		,0 FTienBHXHNLD
		,0  FTienBHXHNSDLDD
		,0 FTienCongBHXH
		,0 FTienBHTNNLD
		,0 FTienBHTNNSDLDD
		,0 FTienCongBHTN
		,sum(fBHYT_NLD) FTienBHYTNLDNLD
		,0 FTienBHYTNSDLDD
		,0 FTienCongBHYTNLD
		,0 FTienBHYTQNNLD
		,0 FTienBHYTQNNSDNLD
		,0 FTienCongBHYTQN
		,0 FTienBHYTTNQN
		,0 FTienBHYTTNCNCNVQP
		,0 FTienCongBHYTTN
		,0 FTienCongALL
		from 
		#tempPhanBoDuLieu
		where sXauNoiMa like '9020002-010-011-0002%'  
		AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		Group by sTenDonVi
		,iID_MaDonVi

		Union all
	--BHYT NGƯỜI LĐ -  NSD LĐ đóng
	--- KHỐI HẠCH TOÁN
		Select
		sTenDonVi
		,iID_MaDonVi
		,0 FTienBHXHNLD
		,0  FTienBHXHNSDLDD
		,0 FTienCongBHXH
		,0 FTienBHTNNLD
		,0 FTienBHTNNSDLDD
		,0 FTienCongBHTN
		,0 FTienBHYTNLDNLD
		,sum(fBHYT_NSD) FTienBHYTNSDLDD
		,0 FTienCongBHYTNLD
		,0 FTienBHYTQNNLD
		,0 FTienBHYTQNNSDNLD
		,0 FTienCongBHYTQN
		,0 FTienBHYTTNQN
		,0 FTienBHYTTNCNCNVQP
		,0 FTienCongBHYTTN
		,0 FTienCongALL
		from 
		#tempPhanBoDuLieu
		where sXauNoiMa like '9020002-010-011-0002%'  
		AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		Group by sTenDonVi
		,iID_MaDonVi

		Union all
	--BHYT QN - NLĐ đóng
	--- KHỐI HẠCH TOÁN
		Select
		sTenDonVi
		,iID_MaDonVi
		,0 FTienBHXHNLD
		,0  FTienBHXHNSDLDD
		,0 FTienCongBHXH
		,0 FTienBHTNNLD
		,0 FTienBHTNNSDLDD
		,0 FTienCongBHTN
		,0 FTienBHYTNLDNLD
		,0 FTienBHYTNSDLDD
		,0 FTienCongBHYTNLD
		,sum(fBHYT_NLD) FTienBHYTQNNLD
		,0 FTienBHYTQNNSDNLD
		,0 FTienCongBHYTQN
		,0 FTienBHYTTNQN
		,0 FTienBHYTTNCNCNVQP
		,0 FTienCongBHYTTN
		,0 FTienCongALL 
		from 
		#tempPhanBoDuLieu 
		where sXauNoiMa like '9020002-010-011-0001%' 
		AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		Group by sTenDonVi
		,iID_MaDonVi

		Union all
	--- BHYT QN - NSD LĐ đóng
	--- KHỐI HẠCH TOÁN
		Select
		sTenDonVi
		,iID_MaDonVi
		,0 FTienBHXHNLD
		,0  FTienBHXHNSDLDD
		,0 FTienCongBHXH
		,0 FTienBHTNNLD
		,0 FTienBHTNNSDLDD
		,0 FTienCongBHTN
		,0 FTienBHYTNLDNLD
		,0 FTienBHYTNSDLDD
		,0 FTienCongBHYTNLD
		,0 FTienBHYTQNNLD
		,sum(fBHYT_NSD)  FTienBHYTQNNSDNLD
		,0 FTienCongBHYTQN
		,0 FTienBHYTTNQN
		,0 FTienBHYTTNCNCNVQP
		,0 FTienCongBHYTTN
		,0 FTienCongALL 
		from #tempPhanBoDuLieu 
		where sXauNoiMa like '9020002-010-011-0001%' 
		AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		Group by sTenDonVi
		,iID_MaDonVi

		Union all
	--Thẻ BHYT thân nhân - Quân nhân
	--- KHỐI HẠCH TOÁN
		Select
		sTenDonVi
		,iID_MaDonVi
		,0 FTienBHXHNLD
		,0  FTienBHXHNSDLDD
		,0 FTienCongBHXH
		,0 FTienBHTNNLD
		,0 FTienBHTNNSDLDD
		,0 FTienCongBHTN
		,0 FTienBHYTNLDNLD
		,0 FTienBHYTNSDLDD
		,0 FTienCongBHYTNLD
		,0 FTienBHYTQNNLD
		,0  FTienBHYTQNNSDNLD
		,0 FTienCongBHYTQN
		,sum(fDuToan) FTienBHYTTNQN
		,0 FTienBHYTTNCNCNVQP
		,0 FTienCongBHYTTN
		,0 FTienCongALL 
		from #tempNhanPbDuLieu 
		where sXauNoiMa like '9030001-010-011-0002%' 
		AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		Group by sTenDonVi
		,iID_MaDonVi

		Union all
	-- Thẻ BHYT thân nhân - CN & CNVQP
	--- KHỐI HẠCH TOÁN
		Select
		sTenDonVi
		,iID_MaDonVi
		,0 FTienBHXHNLD
		,0  FTienBHXHNSDLDD
		,0 FTienCongBHXH
		,0 FTienBHTNNLD
		,0 FTienBHTNNSDLDD
		,0 FTienCongBHTN
		,0 FTienBHYTNLDNLD
		,0 FTienBHYTNSDLDD
		,0 FTienCongBHYTNLD
		,0 FTienBHYTQNNLD
		,0  FTienBHYTQNNSDNLD
		,0 FTienCongBHYTQN
		,0 FTienBHYTTNQN
		,sum(fDuToan) FTienBHYTTNCNCNVQP
		,0 FTienCongBHYTTN
		,0 FTienCongALL 
		from #tempNhanPbDuLieu 
		where sXauNoiMa like '9030002-010-011-0001%'  
		AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		Group by sTenDonVi
		,iID_MaDonVi	)KhoiHachToan

		---- Sum theo DuToan
	Select
		 CAST(ROW_NUMBER() OVER(ORDER BY iID_MaDonVi) AS NVARCHAR(MAX))  STT
		,sTenDonVi 
		,iID_MaDonVi
		,SUM(isnull(FTienBHXHNLD,0)) FTienBHXHNLD
		,SUM(isnull(FTienBHXHNSDLDD,0))  FTienBHXHNSDLDD
		,SUM(isnull(FTienCongBHXH,0)) FTienCongBHXH
		,SUM(isnull(FTienBHTNNLD,0)) FTienBHTNNLD
		,SUM(isnull(FTienBHTNNSDLDD,0)) FTienBHTNNSDLDD
		,SUM(isnull(FTienCongBHTN,0)) FTienCongBHTN
		,SUM(isnull(FTienBHYTNLDNLD,0)) FTienBHYTNLDNLD
		,SUM(isnull(FTienBHYTNSDLDD,0)) FTienBHYTNSDLDD
		,SUM(isnull(FTienCongBHYTNLD,0)) FTienCongBHYTNLD
		,SUM(isnull(FTienBHYTQNNLD,0)) FTienBHYTQNNLD
		,SUM(isnull(FTienBHYTQNNSDNLD,0))  FTienBHYTQNNSDNLD
		,SUM(isnull(FTienCongBHYTQN,0)) FTienCongBHYTQN
		,SUM(isnull(FTienBHYTTNQN,0)) FTienBHYTTNQN
		,SUM(isnull(FTienBHYTTNCNCNVQP,0))FTienBHYTTNCNCNVQP
		,SUM(isnull(FTienCongBHYTTN,0)) FTienCongBHYTTN
		,SUM(isnull(FTienCongALL,0)) FTienCongALL 
		, 2 Type
		,0 bHangCha
		into #tempSumDetailDuToan
		from #tempDetailKhoiDuToan
		group by sTenDonVi
		,iID_MaDonVi


---- Sum theo HachToan
	Select
		CAST(ROW_NUMBER() OVER(ORDER BY iID_MaDonVi) AS NVARCHAR(MAX))  STT
		,sTenDonVi
		,iID_MaDonVi
		,SUM(isnull(FTienBHXHNLD,0)) FTienBHXHNLD
		,SUM(isnull(FTienBHXHNSDLDD,0))  FTienBHXHNSDLDD
		,SUM(isnull(FTienCongBHXH,0)) FTienCongBHXH
		,SUM(isnull(FTienBHTNNLD,0)) FTienBHTNNLD
		,SUM(isnull(FTienBHTNNSDLDD,0)) FTienBHTNNSDLDD
		,SUM(isnull(FTienCongBHTN,0)) FTienCongBHTN
		,SUM(isnull(FTienBHYTNLDNLD,0)) FTienBHYTNLDNLD
		,SUM(isnull(FTienBHYTNSDLDD,0)) FTienBHYTNSDLDD
		,SUM(isnull(FTienCongBHYTNLD,0)) FTienCongBHYTNLD
		,SUM(isnull(FTienBHYTQNNLD,0)) FTienBHYTQNNLD
		,SUM(isnull(FTienBHYTQNNSDNLD,0))  FTienBHYTQNNSDNLD
		,SUM(isnull(FTienCongBHYTQN,0)) FTienCongBHYTQN
		,SUM(isnull(FTienBHYTTNQN,0)) FTienBHYTTNQN
		,SUM(isnull(FTienBHYTTNCNCNVQP,0))FTienBHYTTNCNCNVQP
		,SUM(isnull(FTienCongBHYTTN,0)) FTienCongBHYTTN
		,SUM(isnull(FTienCongALL,0)) FTienCongALL 
		,2 Type
		,0 bHangCha
		into #tempSumDetailHachToan
		from #tempDetailKhoiHachToan
		group by sTenDonVi
		,iID_MaDonVi

		select * into #tempDuToan from (
		select * from
		#tempKhoiDuToan
		Union all 
		select * from #tempSumDetailDuToan)DuToan

		select * into #tempHachToan from 
		(select * from
		#tempKhoiHachToan
		Union all 
		select * from #tempSumDetailHachToan)HachToan

		update A
		set A.FTienBHXHNLD=B.FTienBHXHNLD,
			A.FTienBHXHNSDLDD=B.FTienBHXHNSDLDD,	
			A.FTienCongBHXH=B.FTienCongBHXH,	
			A.FTienBHTNNLD=B.FTienBHTNNLD,	
			A.FTienBHTNNSDLDD=B.FTienBHTNNSDLDD,	
			A.FTienCongBHTN=B.FTienCongBHTN,	
			A.FTienBHYTNLDNLD=B.FTienBHYTNLDNLD,	
			A.FTienBHYTNSDLDD=B.FTienBHYTNSDLDD,	
			A.FTienCongBHYTNLD=B.FTienCongBHYTNLD,	
			A.FTienBHYTQNNLD=B.FTienBHYTQNNLD,	
			A.FTienBHYTQNNSDNLD=B.FTienBHYTQNNSDNLD,
			A.FTienCongBHYTQN=B.FTienCongBHYTQN,	
			A.FTienBHYTTNQN=B.FTienBHYTTNQN,	
			A.FTienBHYTTNCNCNVQP=B.FTienBHYTTNCNCNVQP,	
			A.FTienCongBHYTTN=B.FTienCongBHYTTN,	
			A.FTienCongALL=B.FTienCongALL
		from  #tempDuToan A,
		(select 
			SUM(isnull(FTienBHXHNLD,0)) FTienBHXHNLD
			,SUM(isnull(FTienBHXHNSDLDD,0))  FTienBHXHNSDLDD
			,SUM(isnull(FTienCongBHXH,0)) FTienCongBHXH
			,SUM(isnull(FTienBHTNNLD,0)) FTienBHTNNLD
			,SUM(isnull(FTienBHTNNSDLDD,0)) FTienBHTNNSDLDD
			,SUM(isnull(FTienCongBHTN,0)) FTienCongBHTN
			,SUM(isnull(FTienBHYTNLDNLD,0)) FTienBHYTNLDNLD
			,SUM(isnull(FTienBHYTNSDLDD,0)) FTienBHYTNSDLDD
			,SUM(isnull(FTienCongBHYTNLD,0)) FTienCongBHYTNLD
			,SUM(isnull(FTienBHYTQNNLD,0)) FTienBHYTQNNLD
			,SUM(isnull(FTienBHYTQNNSDNLD,0))  FTienBHYTQNNSDNLD
			,SUM(isnull(FTienCongBHYTQN,0)) FTienCongBHYTQN
			,SUM(isnull(FTienBHYTTNQN,0)) FTienBHYTTNQN
			,SUM(isnull(FTienBHYTTNCNCNVQP,0))FTienBHYTTNCNCNVQP
			,SUM(isnull(FTienCongBHYTTN,0)) FTienCongBHYTTN
			,SUM(isnull(FTienCongALL,0)) FTienCongALL 
		from #tempDuToan
		where  Type=2
		) B
		where A.Type=1

		update A
		set A.FTienBHXHNLD=B.FTienBHXHNLD,
			A.FTienBHXHNSDLDD=B.FTienBHXHNSDLDD,	
			A.FTienCongBHXH=B.FTienCongBHXH,	
			A.FTienBHTNNLD=B.FTienBHTNNLD,	
			A.FTienBHTNNSDLDD=B.FTienBHTNNSDLDD,	
			A.FTienCongBHTN=B.FTienCongBHTN,	
			A.FTienBHYTNLDNLD=B.FTienBHYTNLDNLD,	
			A.FTienBHYTNSDLDD=B.FTienBHYTNSDLDD,	
			A.FTienCongBHYTNLD=B.FTienCongBHYTNLD,	
			A.FTienBHYTQNNLD=B.FTienBHYTQNNLD,	
			A.FTienBHYTQNNSDNLD=B.FTienBHYTQNNSDNLD,
			A.FTienCongBHYTQN=B.FTienCongBHYTQN,	
			A.FTienBHYTTNQN=B.FTienBHYTTNQN,	
			A.FTienBHYTTNCNCNVQP=B.FTienBHYTTNCNCNVQP,	
			A.FTienCongBHYTTN=B.FTienCongBHYTTN,	
			A.FTienCongALL=B.FTienCongALL
		from  #tempHachToan A,
		(select 
			SUM(isnull(FTienBHXHNLD,0)) FTienBHXHNLD
			,SUM(isnull(FTienBHXHNSDLDD,0))  FTienBHXHNSDLDD
			,SUM(isnull(FTienCongBHXH,0)) FTienCongBHXH
			,SUM(isnull(FTienBHTNNLD,0)) FTienBHTNNLD
			,SUM(isnull(FTienBHTNNSDLDD,0)) FTienBHTNNSDLDD
			,SUM(isnull(FTienCongBHTN,0)) FTienCongBHTN
			,SUM(isnull(FTienBHYTNLDNLD,0)) FTienBHYTNLDNLD
			,SUM(isnull(FTienBHYTNSDLDD,0)) FTienBHYTNSDLDD
			,SUM(isnull(FTienCongBHYTNLD,0)) FTienCongBHYTNLD
			,SUM(isnull(FTienBHYTQNNLD,0)) FTienBHYTQNNLD
			,SUM(isnull(FTienBHYTQNNSDNLD,0))  FTienBHYTQNNSDNLD
			,SUM(isnull(FTienCongBHYTQN,0)) FTienCongBHYTQN
			,SUM(isnull(FTienBHYTTNQN,0)) FTienBHYTTNQN
			,SUM(isnull(FTienBHYTTNCNCNVQP,0))FTienBHYTTNCNCNVQP
			,SUM(isnull(FTienCongBHYTTN,0)) FTienCongBHYTTN
			,SUM(isnull(FTienCongALL,0)) FTienCongALL 
		from #tempHachToan
		where  Type=2
		) B
		where A.Type=1

		select * into #tempDetailTotal from (
		select * from #tempTotal
		Union all
		select * from #tempDuToan
		Union all
		select * from #tempHachToan)Total

		update A
		set A.FTienBHXHNLD=B.FTienBHXHNLD,
			A.FTienBHXHNSDLDD=B.FTienBHXHNSDLDD,	
			A.FTienCongBHXH=B.FTienCongBHXH,	
			A.FTienBHTNNLD=B.FTienBHTNNLD,	
			A.FTienBHTNNSDLDD=B.FTienBHTNNSDLDD,	
			A.FTienCongBHTN=B.FTienCongBHTN,	
			A.FTienBHYTNLDNLD=B.FTienBHYTNLDNLD,	
			A.FTienBHYTNSDLDD=B.FTienBHYTNSDLDD,	
			A.FTienCongBHYTNLD=B.FTienCongBHYTNLD,	
			A.FTienBHYTQNNLD=B.FTienBHYTQNNLD,	
			A.FTienBHYTQNNSDNLD=B.FTienBHYTQNNSDNLD,
			A.FTienCongBHYTQN=B.FTienCongBHYTQN,	
			A.FTienBHYTTNQN=B.FTienBHYTTNQN,	
			A.FTienBHYTTNCNCNVQP=B.FTienBHYTTNCNCNVQP,	
			A.FTienCongBHYTTN=B.FTienCongBHYTTN,	
			A.FTienCongALL=B.FTienCongALL
		FROM #tempDetailTotal A,
		(select 
			SUM(isnull(FTienBHXHNLD,0)) FTienBHXHNLD
			,SUM(isnull(FTienBHXHNSDLDD,0))  FTienBHXHNSDLDD
			,SUM(isnull(FTienCongBHXH,0)) FTienCongBHXH
			,SUM(isnull(FTienBHTNNLD,0)) FTienBHTNNLD
			,SUM(isnull(FTienBHTNNSDLDD,0)) FTienBHTNNSDLDD
			,SUM(isnull(FTienCongBHTN,0)) FTienCongBHTN
			,SUM(isnull(FTienBHYTNLDNLD,0)) FTienBHYTNLDNLD
			,SUM(isnull(FTienBHYTNSDLDD,0)) FTienBHYTNSDLDD
			,SUM(isnull(FTienCongBHYTNLD,0)) FTienCongBHYTNLD
			,SUM(isnull(FTienBHYTQNNLD,0)) FTienBHYTQNNLD
			,SUM(isnull(FTienBHYTQNNSDNLD,0))  FTienBHYTQNNSDNLD
			,SUM(isnull(FTienCongBHYTQN,0)) FTienCongBHYTQN
			,SUM(isnull(FTienBHYTTNQN,0)) FTienBHYTTNQN
			,SUM(isnull(FTienBHYTTNCNCNVQP,0))FTienBHYTTNCNCNVQP
			,SUM(isnull(FTienCongBHYTTN,0)) FTienCongBHYTTN
			,SUM(isnull(FTienCongALL,0)) FTienCongALL 
		from #tempDetailTotal
		where  Type=2)B
		where A.Type=0

	if (@IsMillionRound = 1)
		SELECT 
		dt.STT 
		,dt.sTenDonVi STenDonVi
		,dt.iID_MaDonVi
		,round(IsNull(dt.FTienBHXHNLD, 0) / 1000000, 0) * 1000000 / @DVT FTienBHXHNLD
		,round(IsNull(dt.FTienBHXHNSDLDD, 0) / 1000000, 0) * 1000000 / @DVT FTienBHXHNSDLDD
		--,round(IsNull(dt.FTienCongBHXH, 0) / 1000000, 0) * 1000000 / @DVT FTienCongBHXH
		,round(IsNull(dt.FTienBHTNNLD, 0) / 1000000, 0) * 1000000 / @DVT FTienBHTNNLD
		,round(IsNull(dt.FTienBHTNNSDLDD, 0) / 1000000, 0) * 1000000 / @DVT FTienBHTNNSDLDD
		--,round(IsNull(dt.FTienCongBHTN, 0) / 1000000, 0) * 1000000 / @DVT FTienCongBHTN
		,round(IsNull(dt.FTienBHYTNLDNLD, 0) / 1000000, 0) * 1000000 / @DVT FTienBHYTNLDNLD
		,round(IsNull(dt.FTienBHYTNSDLDD, 0) / 1000000, 0) * 1000000 / @DVT FTienBHYTNSDLDD
		--,round(IsNull(dt.FTienCongBHYTNLD, 0) / 1000000, 0) * 1000000 / @DVT FTienCongBHYTNLD
		,round(IsNull(dt.FTienBHYTQNNLD, 0) / 1000000, 0) * 1000000 / @DVT FTienBHYTQNNLD
		,round(IsNull(dt.FTienBHYTQNNSDNLD, 0) / 1000000, 0) * 1000000 / @DVT FTienBHYTQNNSDNLD
		--,round(IsNull(dt.FTienCongBHYTQN, 0) / 1000000, 0) * 1000000 / @DVT FTienCongBHYTQN
		,round(IsNull(dt.FTienBHYTTNQN, 0) / 1000000, 0) * 1000000 / @DVT FTienBHYTTNQN
		,round(IsNull(dt.FTienBHYTTNCNCNVQP, 0) / 1000000, 0) * 1000000 / @DVT FTienBHYTTNCNCNVQP
		--,round(IsNull(dt.FTienCongBHYTTN, 0) / 1000000, 0) * 1000000 / @DVT FTienCongBHYTTN
		--,round(IsNull(dt.FTienCongALL, 0) / 1000000, 0) * 1000000 / @DVT FTienCongALL
		,dt.Type
		,dt.bHangCha
		FROM #tempDetailTotal dt
	else
		SELECT 
		dt.STT 
		,dt.sTenDonVi STenDonVi
		,dt.iID_MaDonVi
		,round(ISNULL(dt.FTienBHXHNLD,0)/ @DVT,0) FTienBHXHNLD
		,round(IsNull(dt.FTienBHXHNLD, 0) / @DVT,0) FTienBHXHNLD
		,round(IsNull(dt.FTienBHXHNSDLDD, 0) / @DVT,0) FTienBHXHNSDLDD
		--,IsNull(dt.FTienCongBHXH, 0) / @DVT FTienCongBHXH
		,round(IsNull(dt.FTienBHTNNLD, 0)  / @DVT,0) FTienBHTNNLD
		,round(IsNull(dt.FTienBHTNNSDLDD, 0)/ @DVT,0) FTienBHTNNSDLDD
		--,IsNull(dt.FTienCongBHTN, 0)/ @DVT FTienCongBHTN
		,round(IsNull(dt.FTienBHYTNLDNLD, 0)/ @DVT,0) FTienBHYTNLDNLD
		,round(IsNull(dt.FTienBHYTNSDLDD, 0) / @DVT,0) FTienBHYTNSDLDD
		--,IsNull(dt.FTienCongBHYTNLD, 0) / @DVT FTienCongBHYTNLD                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   / @DVT FTienCongBHYTNLD
		,round(IsNull(dt.FTienBHYTQNNLD, 0) / @DVT,0) FTienBHYTQNNLD
		,round(IsNull(dt.FTienBHYTQNNSDNLD, 0) / @DVT,0) FTienBHYTQNNSDNLD
		--,IsNull(dt.FTienCongBHYTQN, 0)  / @DVT FTienCongBHYTQN
		,round(IsNull(dt.FTienBHYTTNQN, 0) / @DVT,0) FTienBHYTTNQN
		,round(IsNull(dt.FTienBHYTTNCNCNVQP, 0)  / @DVT,0) FTienBHYTTNCNCNVQP
		--,IsNull(dt.FTienCongBHYTTN, 0)  / @DVT FTienCongBHYTTN
		--,IsNull(dt.FTienCongALL, 0)  / @DVT FTienCongALL
		,dt.Type
		,dt.bHangCha BHangCha
		FROM #tempDetailTotal dt

		drop table #tempSumDetailDuToan
		drop table #tempDetailKhoiDuToan
		drop table #tempDetailKhoiHachToan
		drop table #tempSumDetailHachToan
		drop table #tempKhoiDuToan
		drop table #tempKhoiHachToan
		Drop table #tempTotal
		Drop table #tempDuToan
		Drop table #tempHachToan
		Drop table #tempDetailTotal
END
;
;
;
;
;
GO
