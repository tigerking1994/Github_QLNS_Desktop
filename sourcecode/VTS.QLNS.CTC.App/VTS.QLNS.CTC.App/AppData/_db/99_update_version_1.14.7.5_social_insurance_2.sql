/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh]    Script Date: 8/30/2024 3:10:06 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_phuongan_pbdtc_tachchi_kpql]    Script Date: 8/30/2024 3:10:06 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_phuongan_pbdtc_tachchi_kpql]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_phuongan_pbdtc_tachchi_kpql]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_phuongan_pbdtc]    Script Date: 8/30/2024 3:10:06 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_phuongan_pbdtc]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_phuongan_pbdtc]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_om_dau]    Script Date: 8/30/2024 3:10:06 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_luong_tro_cap_om_dau]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_om_dau]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_get_thongtri_nhieuloaichi]    Script Date: 8/30/2024 3:10:06 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_rpt_get_thongtri_nhieuloaichi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_rpt_get_thongtri_nhieuloaichi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitiet_kpql]    Script Date: 8/30/2024 3:10:06 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_nhandutoanchitiet_kpql]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_nhandutoanchitiet_kpql]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_finphanbodutoan_chitiet_kpql]    Script Date: 8/30/2024 3:10:06 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_finphanbodutoan_chitiet_kpql]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_finphanbodutoan_chitiet_kpql]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_finddutoan_trengiaochitiet_kpql]    Script Date: 8/30/2024 3:10:06 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_finddutoan_trengiaochitiet_kpql]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_finddutoan_trengiaochitiet_kpql]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_dtc_get_so_quyet_dinh_bhxh_bhyt_bhtn]    Script Date: 8/30/2024 3:10:06 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_dtc_get_so_quyet_dinh_bhxh_bhyt_bhtn]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_dtc_get_so_quyet_dinh_bhxh_bhyt_bhtn]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_dtc_get_so_quyet_dinh_bhxh_bhyt_bhtn]    Script Date: 8/30/2024 3:10:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[sp_bhxh_dtc_get_so_quyet_dinh_bhxh_bhyt_bhtn]
	@NamLamViec int
AS
BEGIN
	select distinct  dtt.sSoQuyetDinh, Convert(varchar, dtt.dNgayQuyetDinh, 101) sNgayQuyetDinh, iLoaiDotNhanPhanBo
		from BH_DTC_PhanBoDuToanChi dtt
		where iNamChungTu = @NamLamViec
END
;
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_finddutoan_trengiaochitiet_kpql]    Script Date: 8/30/2024 3:10:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bhxh_finddutoan_trengiaochitiet_kpql]
@iDChungTu uniqueidentifier,
@IIDDonVi nvarchar(50),
@NamLamViec int
AS
BEGIN

	DECLARE @SNoiDung nvarchar(max);
	DECLARE @fSoTien float;
	set @SNoiDung=(select sMoTa from BH_DM_MucLucNganSach
		where iNamLamViec=@NamLamViec
		and iTrangThai=1
		and sXauNoiMa='9010003')


set @fSoTien =	(Select fTienTuChi  from BH_DTC_DuToanChiTrenGiao_ChiTiet
	where iID_DTC_DuToanChiTrenGiao=@iDChungTu
	and iID_MaDonVi=@IIDDonVi
	and sXauNoiMa='9010003'
	)

	Select @SNoiDung SNoiDung, @fSoTien as fSoTien, 1 IsRemainRow, 1 BHangCha
END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_finphanbodutoan_chitiet_kpql]    Script Date: 8/30/2024 3:10:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bhxh_finphanbodutoan_chitiet_kpql]
@iDChungTu uniqueidentifier,
@IIDDonVi nvarchar(50),
@NamLamViec int
AS
BEGIN

	DECLARE @SNoiDung nvarchar(max);
	DECLARE @STenDonVi nvarchar(max);
	DECLARE @fSoTien float;
	set @SNoiDung=(select sMoTa from BH_DM_MucLucNganSach
		where iNamLamViec=@NamLamViec
		and iTrangThai=1
		and sXauNoiMa='9010003')

	set @fSoTien=(Select  isnull(ctct.fTienTuChi,0) from BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	left join DonVi dv on ctct.iID_MaDonVi=dv.iID_MaDonVi
	where ctct.iID_DTC_PhanBoDuToanChi=@iDChungTu
	and ctct.iID_MaDonVi=@IIDDonVi
	and ctct.sXauNoiMa='9010003'
	and dv.iTrangThai=1
	and dv.iNamLamViec=@NamLamViec)

	set @STenDonVi= (Select sTenDonVi from DonVi
	where iNamLamViec=@NamLamViec
	and iID_MaDonVi=@IIDDonVi
	and iTrangThai=1)

		Select @SNoiDung SNoiDung, isnull(@fSoTien,0) as fSoTien, 1 IsRemainRow, 1 BHangCha ,@STenDonVi STenDonVi

END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitiet_kpql]    Script Date: 8/30/2024 3:10:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bhxh_nhandutoanchitiet_kpql]
@iDChungTu uniqueidentifier,
@IIDDonVi nvarchar(50)
AS
BEGIN

Select * from BH_DuToan_CTCT_KPQL
where iID_ChungTu=@iDChungTu
and iID_MaDonVi=@IIDDonVi
order by sXauNoiMa

END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_get_thongtri_nhieuloaichi]    Script Date: 8/30/2024 3:10:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_cp_rpt_get_thongtri_nhieuloaichi]
@NamLamViec int,
@IdMaDonVi nvarchar(max),
@Quy int,
@donViTinh int,
@lstsIDLoaiChi nvarchar(max)
AS BEGIN
SET NOCOUNT ON;


			select 0 IsHangCha,N'Chi các chế độ BHXH' as SMoTa, Sum(isnull(FTienKeHoachCap,0)) /@donViTinh FTienKeHoach from BH_CP_ChungTu_ChiTiet
			where iID_CP_ChungTu in 
			(select iID_CP_ChungTu from BH_CP_ChungTu 
				where iQuy=@Quy
				and iNamChungTu=@NamLamViec
				and iID_LoaiCap IN (select * from splitstring(@lstsIDLoaiChi))
			)
			and sXauNoiMa = '901'
			and iID_MaDonVi=@IdMaDonVi
			Union all

			select  0 IsHangCha,N'Chi kinh phí quản lý BHXH, BHYT' as SMoTa, Sum(isnull(FTienKeHoachCap,0)) /@donViTinh FTienKeHoach  from BH_CP_ChungTu_ChiTiet
			where iID_CP_ChungTu in 
			(select iID_CP_ChungTu from BH_CP_ChungTu 
				where iQuy=@Quy
				and iNamChungTu=@NamLamViec
				and iID_LoaiCap IN (select * from splitstring(@lstsIDLoaiChi))
			)
			and sXauNoiMa = '9010003'
			and iID_MaDonVi=@IdMaDonVi
			Union all

			select  0 IsHangCha,N'Kinh phí KCB tại quân y đơn vị 10%' as SMoTa, Sum(isnull(FTienKeHoachCap,0)) /@donViTinh FTienKeHoach  from BH_CP_ChungTu_ChiTiet
			where iID_CP_ChungTu in 
			(select iID_CP_ChungTu from BH_CP_ChungTu 
				where iQuy=@Quy
				and iNamChungTu=@NamLamViec
				and iID_LoaiCap IN (select * from splitstring(@lstsIDLoaiChi))
			)
			and sXauNoiMa = '9010004'
			and iID_MaDonVi=@IdMaDonVi
			Union all

			select  0 IsHangCha,N'Kinh phí KCB tại Trường Sa - DK' as SMoTa, Sum(isnull(FTienKeHoachCap,0)) FTienKeHoach  from BH_CP_ChungTu_ChiTiet
			where iID_CP_ChungTu in 
			(select iID_CP_ChungTu from BH_CP_ChungTu 
				where iQuy=@Quy
				and iNamChungTu=@NamLamViec
				and iID_LoaiCap IN (select * from splitstring(@lstsIDLoaiChi))
			)
			and sXauNoiMa = '9010006'
			and iID_MaDonVi=@IdMaDonVi
			Union all

			select  0 IsHangCha,N'Chi kinh phí chăm sóc sức khỏe ban đầu HSSV & NLĐ' as SMoTa, Sum(isnull(FTienKeHoachCap,0)) /@donViTinh FTienKeHoach from BH_CP_ChungTu_ChiTiet
			where iID_CP_ChungTu in 
			(select iID_CP_ChungTu from BH_CP_ChungTu 
				where iQuy=@Quy
				and iNamChungTu=@NamLamViec
				and iID_LoaiCap IN (select * from splitstring(@lstsIDLoaiChi))
			)
			and sXauNoiMa like '9050001%'

			Union all

			select  0 IsHangCha,N'Chi từ nguồn kết dư Quỹ KCB BHYT quân nhân' as SMoTa, Sum(isnull(FTienKeHoachCap,0)) /@donViTinh FTienKeHoach  from BH_CP_ChungTu_ChiTiet
			where iID_CP_ChungTu in 
			(select iID_CP_ChungTu from BH_CP_ChungTu 
				where iQuy=@Quy
				and iNamChungTu=@NamLamViec
				and iID_LoaiCap IN (select * from splitstring(@lstsIDLoaiChi))
			)
			and sXauNoiMa = '9010008'
			and iID_MaDonVi=@IdMaDonVi
			Union all

			select  0 IsHangCha,N'Kinh phí mua sắm trang thiết bị y tế' as SMoTa, Sum(isnull(FTienKeHoachCap,0)) /@donViTinh FTienKeHoach from BH_CP_ChungTu_ChiTiet
			where iID_CP_ChungTu in 
			(select iID_CP_ChungTu from BH_CP_ChungTu 
				where iQuy=@Quy
				and iNamChungTu=@NamLamViec
				and iID_LoaiCap IN (select * from splitstring(@lstsIDLoaiChi))
			)
			and sXauNoiMa = '9010009'
			and iID_MaDonVi=@IdMaDonVi
			Union all

			select  0 IsHangCha,N'Chi hỗ trợ người lao động tham gia BHTN' as SMoTa, Sum(isnull(FTienKeHoachCap,0)) /@donViTinh FTienKeHoach  from BH_CP_ChungTu_ChiTiet
			where iID_CP_ChungTu in 
			(select iID_CP_ChungTu from BH_CP_ChungTu 
				where iQuy=@Quy
				and iNamChungTu=@NamLamViec
				and iID_LoaiCap IN (select * from splitstring(@lstsIDLoaiChi))
			)
			and sXauNoiMa = '90100010'
			and iID_MaDonVi=@IdMaDonVi
END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_om_dau]    Script Date: 8/30/2024 3:10:06 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_phuongan_pbdtc]    Script Date: 8/30/2024 3:10:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_phuongan_pbdtc]
	@NamLamViec int,
	@SoQuyetDinh nvarchar(max),
	@NgayQuyetDinh nvarchar(max),
	@MaDonVi nvarchar(max),
	@DonViTinh int,
	@IsMillionRound bit

AS
BEGIN

		 Select 
		 'I' STT,
		 '' SMaDonVi,
		 N'KHỐI DỰ TOÁN' as STenDonVi,
		 0 FTienTroCapOmDau,
		 0 FTienTroCapThaiSan,
		 0 FTienTroTNLDBNN,
		 0 FTienTroCapHuuTri,
		 0 FTienTroCapPhucVien,
		 0 FTienTroCapXuatNgu,
		 0 FTienTroCapThoiViec,
		 0 FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 0 FTienHoTroCanBo,
		 0 FTienChiTapHuan,
		 0 FTienHoTroQuanLy,
		 0 FTienTongCongKQPL,
		 0 FTienChiKCBQYDV,
		 0 FTienChiKCBTSDK,
		 0 FTienChiTNKDQKCBBHYT,
		 0 FTienKPMSTTBYT,
		 0 FTienChiKPCSSK,
		 0 FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 1 BHangCha,
		 1 IdParent,
		 1 Type,
		 1 child
		 into #tempKhoiDuToan

		 Select 
		 N'TỔNG CỘNG' STT,
		 '' SMaDonVi,
		 '' as STenDonVi,
		 0 FTienTroCapOmDau,
		 0 FTienTroCapThaiSan,
		 0 FTienTroTNLDBNN,
		 0 FTienTroCapHuuTri,
		 0 FTienTroCapPhucVien,
		 0 FTienTroCapXuatNgu,
		 0 FTienTroCapThoiViec,
		 0 FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 0 FTienHoTroCanBo,
		 0 FTienChiTapHuan,
		 0 FTienHoTroQuanLy,
		 0 FTienTongCongKQPL,
		 0 FTienChiKCBQYDV,
		 0 FTienChiKCBTSDK,
		 0 FTienChiTNKDQKCBBHYT,
		 0 FTienKPMSTTBYT,
		 0 FTienChiKPCSSK,
		 0 FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 1 BHangCha,
		 0 IdParent,
		 0 Type,
		 1 child
		 into #tempTongCong

		  Select 
		 'II' STT,
		 '' SMaDonVi,
		 N'KHỐI HẠCH TOÁN' as STenDonVi,
		 0 FTienTroCapOmDau,
		 0 FTienTroCapThaiSan,
		 0 FTienTroTNLDBNN,
		 0 FTienTroCapHuuTri,
		 0 FTienTroCapPhucVien,
		 0 FTienTroCapXuatNgu,
		 0 FTienTroCapThoiViec,
		 0 FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 0 FTienHoTroCanBo,
		 0 FTienChiTapHuan,
		 0 FTienHoTroQuanLy,
		 0 FTienTongCongKQPL,
		 0 FTienChiKCBQYDV,
		 0 FTienChiKCBTSDK,
		 0 FTienChiTNKDQKCBBHYT,
		 0 FTienKPMSTTBYT,
		 0 FTienChiKPCSSK,
		 0 FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 1 BHangCha,
		 2 IdParent,
		 2 Type,
		 1 child
		 into #tempKhoiHachToan

		  --- BHXH
		Select ctct.* into #tblDuToanBHXH from BH_DTC_PhanBoDuToanChi_ChiTiet ctct
		join BH_DTC_PhanBoDuToanChi ct on ctct.iID_DTC_PhanBoDuToanChi = ct.ID
		where ctct.sXauNoiMa LIKE '9010001%'
		and (ct.iLoaiDotNhanPhanBo =1 or ct.iLoaiDotNhanPhanBo=2)
		and ct.sSoQuyetDinh =@SoQuyetDinh
		and  Convert(varchar, ct.dNgayQuyetDinh, 101)=@NgayQuyetDinh
		and ctct.iNamLamViec = @NamLamViec
		and ctct.iID_MaDonVi in (select * from splitstring(@MaDonVi))

		Select ctct.* into #tblHachToanBHXH from BH_DTC_PhanBoDuToanChi_ChiTiet ctct
		join BH_DTC_PhanBoDuToanChi ct on ctct.iID_DTC_PhanBoDuToanChi = ct.ID
		where ctct.sXauNoiMa LIKE '9010002%'
		and (ct.iLoaiDotNhanPhanBo =1 or ct.iLoaiDotNhanPhanBo=2)
		and ct.sSoQuyetDinh = @SoQuyetDinh
		and  Convert(varchar, ct.dNgayQuyetDinh, 101)=@NgayQuyetDinh
		and ctct.iNamLamViec = @NamLamViec
		and  ctct.iID_MaDonVi in (select * from splitstring(@MaDonVi))

		-- KPQL

		Select ctct.* into #tblDuToanKPQL from BH_DuToan_CTCT_KPQL  ctct
		left join DonVi dv on ctct.iID_MaDonVi = dv.iID_MaDonVi
		left join BH_DTC_PhanBoDuToanChi ct on ctct.iID_ChungTu=ct.ID
		where ct.sSoQuyetDinh = @SoQuyetDinh
		and  Convert(varchar, ct.dNgayQuyetDinh, 101)=@NgayQuyetDinh
		and (ct.iLoaiDotNhanPhanBo =1 or ct.iLoaiDotNhanPhanBo=2)
		and ctct.iNamLamViec = @NamLamViec
		and dv.iNamLamViec=@NamLamViec
		and dv.iKhoi=2
		and ctct.iID_MaDonVi in (select * from splitstring(@MaDonVi))

		Select ctct.* into #tblHachToanKPQL from BH_DuToan_CTCT_KPQL  ctct
		left join DonVi dv on ctct.iID_MaDonVi = dv.iID_MaDonVi
		left join BH_DTC_PhanBoDuToanChi ct on ctct.iID_ChungTu=ct.ID
		where ct.sSoQuyetDinh = @SoQuyetDinh
		and  Convert(varchar, ct.dNgayQuyetDinh, 101)=@NgayQuyetDinh
		and (ct.iLoaiDotNhanPhanBo =1 or ct.iLoaiDotNhanPhanBo=2)
		and ctct.iNamLamViec = @NamLamViec
		and dv.iNamLamViec=@NamLamViec
		and dv.iKhoi!=2
		and  ctct.iID_MaDonVi in (select * from splitstring(@MaDonVi))

		--- Khac :KCBQY,KCBTS,Quỹ KCB BHYT quân nhân, mua sắm trang thiết bị y tế,chăm sóc sức khỏe ban đầu HSSV & NLĐ,hỗ trợ BHTN
		Select ctct.* into #tblDuToanKhac from BH_DTC_PhanBoDuToanChi_ChiTiet ctct
		join BH_DTC_PhanBoDuToanChi ct on ctct.iID_DTC_PhanBoDuToanChi = ct.ID
		left join DonVi dv on ctct.iID_MaDonVi=dv.iID_MaDonVi
		where dv.iKhoi=2
		and (ct.iLoaiDotNhanPhanBo =1 or ct.iLoaiDotNhanPhanBo=2)
		and ct.sSoQuyetDinh = @SoQuyetDinh
		and  Convert(varchar, ct.dNgayQuyetDinh, 101)=@NgayQuyetDinh
		and ctct.iNamLamViec = @NamLamViec
		and ctct.iID_MaDonVi in (select * from splitstring(@MaDonVi))


		Select ctct.* into #tblHachToanKhac from BH_DTC_PhanBoDuToanChi_ChiTiet ctct
		join BH_DTC_PhanBoDuToanChi ct on ctct.iID_DTC_PhanBoDuToanChi = ct.ID
		left join DonVi dv on ctct.iID_MaDonVi=dv.iID_MaDonVi
		where dv.iKhoi!=2
		and (ct.iLoaiDotNhanPhanBo =1 or ct.iLoaiDotNhanPhanBo=2)
		and ct.sSoQuyetDinh =@SoQuyetDinh
		and  Convert(varchar, ct.dNgayQuyetDinh, 101)=@NgayQuyetDinh
		and ctct.iNamLamViec = @NamLamViec
		and ctct.iID_MaDonVi in (select * from splitstring(@MaDonVi))

		--- Detail BHXH
		Select 
		 Case when sXauNoiMa='9010001-010-011-0001' then SUM(isnull(fTienTuChi,0)) else 0 end  FTienTroCapOmDau,
		 Case when sXauNoiMa='9010001-010-011-0002' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroCapThaiSan,
		 Case when sXauNoiMa='9010001-010-011-0003' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroTNLDBNN,
		 Case when sXauNoiMa='9010001-010-011-0004' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroCapHuuTri,
		 Case when sXauNoiMa='9010001-010-011-0005' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroCapPhucVien,
		 Case when sXauNoiMa='9010001-010-011-0006' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroCapXuatNgu,
		 Case when sXauNoiMa='9010001-010-011-0007' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroCapThoiViec,
		 Case when sXauNoiMa='9010001-010-011-0008' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 0 FTienHoTroCanBo,
		 0 FTienChiTapHuan,
		 0 FTienHoTroQuanLy,
		 0 FTienTongCongKQPL,
		 0 FTienChiKCBQYDV,
		 0 FTienChiKCBTSDK,
		 0 FTienChiTNKDQKCBBHYT,
		 0 FTienKPMSTTBYT,
		 0 FTienChiKPCSSK,
		 0 FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 iID_MaDonVi
		 INTO #tempDetailDuToanBHXH
		 from #tblDuToanBHXH
		 GROUP BY sXauNoiMa,iID_MaDonVi

		 SELECT 
		 sum(FTienTroCapOmDau) FTienTroCapOmDau,
		 sum(FTienTroCapThaiSan) FTienTroCapThaiSan,
		 sum(FTienTroTNLDBNN) FTienTroTNLDBNN,
		 sum(FTienTroCapHuuTri) FTienTroCapHuuTri,
		 sum(FTienTroCapPhucVien) FTienTroCapPhucVien,
		 sum(FTienTroCapXuatNgu) FTienTroCapXuatNgu,
		 sum(FTienTroCapThoiViec) FTienTroCapThoiViec,
		 sum(FTienTroCapTuTuat) FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 0 FTienHoTroCanBo,
		 0 FTienChiTapHuan,
		 0 FTienHoTroQuanLy,
		 0 FTienTongCongKQPL,
		 0 FTienChiKCBQYDV,
		 0 FTienChiKCBTSDK,
		 0 FTienChiTNKDQKCBBHYT,
		 0 FTienKPMSTTBYT,
		 0 FTienChiKPCSSK,
		 0 FTienChiHTBHTN,
		 0 FTienTongCongAll,
		  A.iID_MaDonVi,
		  dv.sTenDonVi
		  into #tblDuToanSumBHXH
		FROM #tempDetailDuToanBHXH A
		left join DonVi dv on A.iID_MaDonVi=dv.iID_MaDonVi
		where iNamLamViec=@NamLamViec
		and dv.iTrangThai=1
		  GROUP BY A.iID_MaDonVi,dv.sTenDonVi


		Select 
		 Case when sXauNoiMa='9010002-010-011-0001' then SUM(isnull(fTienTuChi,0)) else 0 end  FTienTroCapOmDau,
		 Case when sXauNoiMa='9010002-010-011-0002' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroCapThaiSan,
		 Case when sXauNoiMa='9010002-010-011-0003' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroTNLDBNN,
		 Case when sXauNoiMa='9010002-010-011-0004' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroCapHuuTri,
		 Case when sXauNoiMa='9010002-010-011-0005' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroCapPhucVien,
		 Case when sXauNoiMa='9010002-010-011-0006' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroCapXuatNgu,
		 Case when sXauNoiMa='9010002-010-011-0007' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroCapThoiViec,
		 Case when sXauNoiMa='9010002-010-011-0008' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 0 FTienHoTroCanBo,
		 0 FTienChiTapHuan,
		 0 FTienHoTroQuanLy,
		 0 FTienTongCongKQPL,
		 0 FTienChiKCBQYDV,
		 0 FTienChiKCBTSDK,
		 0 FTienChiTNKDQKCBBHYT,
		 0 FTienKPMSTTBYT,
		 0 FTienChiKPCSSK,
		 0 FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 iID_MaDonVi
		 INTO #tempDetailHachToanBHXH
		 from #tblHachToanBHXH
		 GROUP BY sXauNoiMa,iID_MaDonVi

		 SELECT 
		 sum(FTienTroCapOmDau) FTienTroCapOmDau,
		 sum(FTienTroCapThaiSan) FTienTroCapThaiSan,
		 sum(FTienTroTNLDBNN) FTienTroTNLDBNN,
		 sum(FTienTroCapHuuTri) FTienTroCapHuuTri,
		 sum(FTienTroCapPhucVien) FTienTroCapPhucVien,
		 sum(FTienTroCapXuatNgu) FTienTroCapXuatNgu,
		 sum(FTienTroCapThoiViec) FTienTroCapThoiViec,
		 sum(FTienTroCapTuTuat) FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 0 FTienHoTroCanBo,
		 0 FTienChiTapHuan,
		 0 FTienHoTroQuanLy,
		 0 FTienTongCongKQPL,
		 0 FTienChiKCBQYDV,
		 0 FTienChiKCBTSDK,
		 0 FTienChiTNKDQKCBBHYT,
		 0 FTienKPMSTTBYT,
		 0 FTienChiKPCSSK,
		 0 FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 A.iID_MaDonVi,
		 dv.sTenDonVi
		 into #tblHachToanSumBHXH
		 FROM #tempDetailHachToanBHXH A
		left join DonVi dv on A.iID_MaDonVi=dv.iID_MaDonVi
		where iNamLamViec=@NamLamViec
		and dv.iTrangThai=1
		  GROUP BY A.iID_MaDonVi,dv.sTenDonVi

		--- KPQL
		Select 
		 0 FTienTroCapOmDau,
		 0 FTienTroCapThaiSan,
		 0 FTienTroTNLDBNN,
		 0 FTienTroCapHuuTri,
		 0 FTienTroCapPhucVien,
		 0 FTienTroCapXuatNgu,
		 0 FTienTroCapThoiViec,
		 0 FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 case when sXauNoiMa='9010011-011' then Sum(isnull(fSoTien,0)) else 0 end FTienHoTroCanBo,
		 case when sXauNoiMa='9010011-012' then Sum(isnull(fSoTien,0)) else 0 end FTienChiTapHuan,
		 case when sXauNoiMa like '9010011-010%' or sXauNoiMa like '9010011-013-0001%'  or sXauNoiMa like '9010011-013-0002%' then Sum(isnull(fSoTien,0)) else 0 end FTienHoTroQuanLy,
		 0 FTienTongCongKQPL,
		 0 FTienChiKCBQYDV,
		 0 FTienChiKCBTSDK,
		 0 FTienChiTNKDQKCBBHYT,
		 0 FTienKPMSTTBYT,
		 0 FTienChiKPCSSK,
		 0 FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 iID_MaDonVi
		 into #tblDuToanDetailKPQL
		 from #tblDuToanKPQL
		 GROUP BY sXauNoiMa,iID_MaDonVi

		 select 
		 0 FTienTroCapOmDau,
		 0 FTienTroCapThaiSan,
		 0 FTienTroTNLDBNN,
		 0 FTienTroCapHuuTri,
		 0 FTienTroCapPhucVien,
		 0 FTienTroCapXuatNgu,
		 0 FTienTroCapThoiViec,
		 0 FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 sum(FTienHoTroCanBo) FTienHoTroCanBo,
		 sum(FTienChiTapHuan) FTienChiTapHuan,
		 sum(FTienHoTroQuanLy) FTienHoTroQuanLy,
		 0 FTienTongCongKQPL,
		 0 FTienChiKCBQYDV,
		 0 FTienChiKCBTSDK,
		 0 FTienChiTNKDQKCBBHYT,
		 0 FTienKPMSTTBYT,
		 0 FTienChiKPCSSK,
		 0 FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 A.iID_MaDonVi,
		 dv.sTenDonVi
		 into #tblDuToanSumKPQL
		 from #tblDuToanDetailKPQL A
		 left join  DonVi dv on A.iID_MaDonVi=dv.iID_MaDonVi
		 where dv.iNamLamViec=@NamLamViec
		 and dv.iTrangThai=1
		 group by   A.iID_MaDonVi,dv.sTenDonVi


		 Select 
		 0 FTienTroCapOmDau,
		 0 FTienTroCapThaiSan,
		 0 FTienTroTNLDBNN,
		 0 FTienTroCapHuuTri,
		 0 FTienTroCapPhucVien,
		 0 FTienTroCapXuatNgu,
		 0 FTienTroCapThoiViec,
		 0 FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 case when sXauNoiMa='9010011-011' then Sum(isnull(fSoTien,0)) else 0 end FTienHoTroCanBo,
		 case when sXauNoiMa='9010011-012' then Sum(isnull(fSoTien,0)) else 0 end FTienChiTapHuan,
		 case when sXauNoiMa like '9010011-010%' or sXauNoiMa like '9010011-013-0001%'  or sXauNoiMa like '9010011-013-0002%' then Sum(isnull(fSoTien,0)) else 0 end FTienHoTroQuanLy,
		 0 FTienTongCongKQPL,
		 0 FTienChiKCBQYDV,
		 0 FTienChiKCBTSDK,
		 0 FTienChiTNKDQKCBBHYT,
		 0 FTienKPMSTTBYT,
		 0 FTienChiKPCSSK,
		 0 FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 iID_MaDonVi
		 into #tblHachToanDetailKPQL
		 from #tblHachToanKPQL
		 GROUP BY sXauNoiMa,iID_MaDonVi

		 select 
		 0 FTienTroCapOmDau,
		 0 FTienTroCapThaiSan,
		 0 FTienTroTNLDBNN,
		 0 FTienTroCapHuuTri,
		 0 FTienTroCapPhucVien,
		 0 FTienTroCapXuatNgu,
		 0 FTienTroCapThoiViec,
		 0 FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 sum(FTienHoTroCanBo) FTienHoTroCanBo,
		 sum(FTienChiTapHuan) FTienChiTapHuan,
		 sum(FTienHoTroQuanLy) FTienHoTroQuanLy,
		 0 FTienTongCongKQPL,
		 0 FTienChiKCBQYDV,
		 0 FTienChiKCBTSDK,
		 0 FTienChiTNKDQKCBBHYT,
		 0 FTienKPMSTTBYT,
		 0 FTienChiKPCSSK,
		 0 FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 A.iID_MaDonVi,
		 dv.sTenDonVi
		 into #tblHachToanSumKPQL
		 from #tblHachToanDetailKPQL A
		 left join  DonVi dv on A.iID_MaDonVi=dv.iID_MaDonVi
		 where dv.iNamLamViec=@NamLamViec
		 and dv.iTrangThai=1
		 group by   A.iID_MaDonVi,dv.sTenDonVi

		 --- Khac :KCBQY,KCBTS,Quỹ KCB BHYT quân nhân, mua sắm trang thiết bị y tế,chăm sóc sức khỏe ban đầu HSSV & NLĐ,hỗ trợ BHTN
		 select 
		 0 FTienTroCapOmDau,
		 0 FTienTroCapThaiSan,
		 0 FTienTroTNLDBNN,
		 0 FTienTroCapHuuTri,
		 0 FTienTroCapPhucVien,
		 0 FTienTroCapXuatNgu,
		 0 FTienTroCapThoiViec,
		 0 FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 0 FTienHoTroCanBo,
		 0 FTienChiTapHuan,
		 0 FTienHoTroQuanLy,
		 0 FTienTongCongKQPL,
		 case when sXauNoiMa='9010004' then SUM(isnull(fTienTuChi,0)) else 0 end FTienChiKCBQYDV,
		 case when sXauNoiMa='9010006' then SUM(isnull(fTienTuChi,0)) else 0 end FTienChiKCBTSDK,
		 case when sXauNoiMa='9010008' then SUM(isnull(fTienTuChi,0)) else 0 end  FTienChiTNKDQKCBBHYT,
		 case when sXauNoiMa='9010009' then SUM(isnull(fTienTuChi,0)) else 0 end FTienKPMSTTBYT,
		 case when sXauNoiMa='905' then SUM(isnull(fTienTuChi,0)) else 0 end FTienChiKPCSSK,
		 case when sXauNoiMa='9010010' then SUM(isnull(fTienTuChi,0)) else 0 end FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 iID_MaDonVi
		 into #tempDuToanDetailKhac
		 from #tblDuToanKhac
		 group by sXauNoiMa,iID_MaDonVi

		 select 
		  0 FTienTroCapOmDau,
		 0 FTienTroCapThaiSan,
		 0 FTienTroTNLDBNN,
		 0 FTienTroCapHuuTri,
		 0 FTienTroCapPhucVien,
		 0 FTienTroCapXuatNgu,
		 0 FTienTroCapThoiViec,
		 0 FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 0 FTienHoTroCanBo,
		 0 FTienChiTapHuan,
		 0 FTienHoTroQuanLy,
		 0 FTienTongCongKQPL,
		 sum(FTienChiKCBQYDV) FTienChiKCBQYDV,
		 sum(FTienChiKCBTSDK) FTienChiKCBTSDK,
		 sum(FTienChiTNKDQKCBBHYT)  FTienChiTNKDQKCBBHYT,
		 sum(FTienKPMSTTBYT) FTienKPMSTTBYT,
		 sum(FTienChiKPCSSK) FTienChiKPCSSK,
		 sum(FTienChiHTBHTN) FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 A.iID_MaDonVi,
		 dv.sTenDonVi
		  into #tblDuToanSumKhac
		 from #tempDuToanDetailKhac A 
		 left join DonVi dv on A.iID_MaDonVi=dv.iID_MaDonVi
		 where iNamLamViec=@NamLamViec
		 and iTrangThai=1
		 group by A.iID_MaDonVi,dv.sTenDonVi


		  select 
		 0 FTienTroCapOmDau,
		 0 FTienTroCapThaiSan,
		 0 FTienTroTNLDBNN,
		 0 FTienTroCapHuuTri,
		 0 FTienTroCapPhucVien,
		 0 FTienTroCapXuatNgu,
		 0 FTienTroCapThoiViec,
		 0 FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 0 FTienHoTroCanBo,
		 0 FTienChiTapHuan,
		 0 FTienHoTroQuanLy,
		 0 FTienTongCongKQPL,
		 case when sXauNoiMa='9010004' then SUM(isnull(fTienTuChi,0)) else 0 end FTienChiKCBQYDV,
		 case when sXauNoiMa='9010006' then SUM(isnull(fTienTuChi,0)) else 0 end FTienChiKCBTSDK,
		 case when sXauNoiMa='9010008' then SUM(isnull(fTienTuChi,0)) else 0 end  FTienChiTNKDQKCBBHYT,
		 case when sXauNoiMa='9010009' then SUM(isnull(fTienTuChi,0)) else 0 end FTienKPMSTTBYT,
		 case when sXauNoiMa='905' then SUM(isnull(fTienTuChi,0)) else 0 end FTienChiKPCSSK,
		 case when sXauNoiMa='9010010' then SUM(isnull(fTienTuChi,0)) else 0 end FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 iID_MaDonVi
		 into #tempHachToanDetailKhac
		 from #tblHachToanKhac
		 group by sXauNoiMa,iID_MaDonVi

		 select 
		  0 FTienTroCapOmDau,
		 0 FTienTroCapThaiSan,
		 0 FTienTroTNLDBNN,
		 0 FTienTroCapHuuTri,
		 0 FTienTroCapPhucVien,
		 0 FTienTroCapXuatNgu,
		 0 FTienTroCapThoiViec,
		 0 FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 0 FTienHoTroCanBo,
		 0 FTienChiTapHuan,
		 0 FTienHoTroQuanLy,
		 0 FTienTongCongKQPL,
		 sum(FTienChiKCBQYDV) FTienChiKCBQYDV,
		 sum(FTienChiKCBTSDK) FTienChiKCBTSDK,
		 sum(FTienChiTNKDQKCBBHYT)  FTienChiTNKDQKCBBHYT,
		 sum(FTienKPMSTTBYT) FTienKPMSTTBYT,
		 sum(FTienChiKPCSSK) FTienChiKPCSSK,
		 sum(FTienChiHTBHTN) FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 A.iID_MaDonVi,
		 dv.sTenDonVi
		 into #tblHachToanSumKhac
		 from #tempHachToanDetailKhac A 
		 left join DonVi dv on A.iID_MaDonVi=dv.iID_MaDonVi
		 where iNamLamViec=@NamLamViec
		 and iTrangThai=1
		 group by A.iID_MaDonVi,dv.sTenDonVi

		 Select * into #tblDuToanDetailAll from
		 (
			select * from #tblDuToanSumBHXH
			union all
			select * from #tblDuToanSumKPQL
			union all
			select * from #tblDuToanSumKhac
		 )tbl 


		  Select *  into #tblHachToanDetailAll from
		 (
			select * from #tblHachToanSumBHXH
			union all
			select * from #tblHachToanSumKPQL
			union all
			select * from #tblHachToanSumKhac
		 )tbl 

		 select 
		 CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iID_MaDonVi ASC))) AS STT,
		 iID_MaDonVi SMaDonVi,
		 sTenDonVi as STenDonVi,
		 Sum(FTienTroCapOmDau) FTienTroCapOmDau,
		 Sum(FTienTroCapThaiSan) FTienTroCapThaiSan,
		 Sum(FTienTroTNLDBNN) FTienTroTNLDBNN,
		 Sum(FTienTroCapHuuTri) FTienTroCapHuuTri,
		 Sum(FTienTroCapPhucVien) FTienTroCapPhucVien,
		 Sum(FTienTroCapXuatNgu) FTienTroCapXuatNgu,
		 Sum(FTienTroCapThoiViec) FTienTroCapThoiViec,
		 Sum(FTienTroCapTuTuat) FTienTroCapTuTuat,
		 Sum(FTienTongCongBHXH) FTienTongCongBHXH,
		 Sum(FTienHoTroCanBo) FTienHoTroCanBo,
		 Sum(FTienChiTapHuan) FTienChiTapHuan,
		 Sum(FTienHoTroQuanLy) FTienHoTroQuanLy,
		 Sum(FTienTongCongKQPL) FTienTongCongKQPL,
		 sum(FTienChiKCBQYDV) FTienChiKCBQYDV,
		 SUM(FTienChiKCBTSDK) FTienChiKCBTSDK,
		 sum(FTienChiTNKDQKCBBHYT) FTienChiTNKDQKCBBHYT,
		 sum(FTienKPMSTTBYT) FTienKPMSTTBYT,
		 sum(FTienChiKPCSSK) FTienChiKPCSSK,
		 sum(FTienChiHTBHTN) FTienChiHTBHTN,
		 sum(FTienTongCongAll) FTienTongCongAll,
		 0 BHangCha,
		 null IdParent,
		 1 Type,
		 0 child
		 into #tempDuToanSumAll
		from 
		#tblDuToanDetailAll
		group by iID_MaDonVi ,sTenDonVi
		order by iID_MaDonVi


		select 
			CONVERT(nvarchar(10),(ROW_NUMBER() OVER(ORDER BY iID_MaDonVi ASC))) AS STT,
		 iID_MaDonVi SMaDonVi,
		 sTenDonVi as STenDonVi,
		 Sum(FTienTroCapOmDau) FTienTroCapOmDau,
		 Sum(FTienTroCapThaiSan) FTienTroCapThaiSan,
		 Sum(FTienTroTNLDBNN) FTienTroTNLDBNN,
		 Sum(FTienTroCapHuuTri) FTienTroCapHuuTri,
		 Sum(FTienTroCapPhucVien) FTienTroCapPhucVien,
		 Sum(FTienTroCapXuatNgu) FTienTroCapXuatNgu,
		 Sum(FTienTroCapThoiViec) FTienTroCapThoiViec,
		 Sum(FTienTroCapTuTuat) FTienTroCapTuTuat,
		 Sum(FTienTongCongBHXH) FTienTongCongBHXH,
		 Sum(FTienHoTroCanBo) FTienHoTroCanBo,
		 Sum(FTienChiTapHuan) FTienChiTapHuan,
		 Sum(FTienHoTroQuanLy) FTienHoTroQuanLy,
		 Sum(FTienTongCongKQPL) FTienTongCongKQPL,
		 sum(FTienChiKCBQYDV) FTienChiKCBQYDV,
		 SUM(FTienChiKCBTSDK) FTienChiKCBTSDK,
		 sum(FTienChiTNKDQKCBBHYT) FTienChiTNKDQKCBBHYT,
		 sum(FTienKPMSTTBYT) FTienKPMSTTBYT,
		 sum(FTienChiKPCSSK) FTienChiKPCSSK,
		 sum(FTienChiHTBHTN) FTienChiHTBHTN,
		 sum(FTienTongCongAll) FTienTongCongAll,
		 0 BHangCha,
		 null IdParent,
		 2 Type,
		 0 child
		 into #tempHachToanSumAll
		from 
		#tblHachToanDetailAll
		group by iID_MaDonVi ,sTenDonVi
		order by iID_MaDonVi


		select * into #tblResult from 
		(
			Select * from #tempTongCong
			union all
			Select * from #tempKhoiDuToan
			union all 
			select * from #tempDuToanSumAll
			union all
			Select * from #tempKhoiHachToan
			union all 
			select * from #tempHachToanSumAll
		)tbl

		---update du toan
		update A 
		set  A.FTienTroCapOmDau= B.FTienTroCapOmDau,
			 A.FTienTroCapThaiSan= B.FTienTroCapThaiSan,
			 A.FTienTroTNLDBNN= B.FTienTroTNLDBNN,
			 A.FTienTroCapHuuTri= B.FTienTroCapHuuTri,
			 A.FTienTroCapPhucVien= B.FTienTroCapPhucVien,
			 A.FTienTroCapXuatNgu= B.FTienTroCapXuatNgu,
			 A.FTienTroCapThoiViec= B.FTienTroCapThoiViec,
			 A.FTienTroCapTuTuat= B.FTienTroCapTuTuat,
			 A.FTienTongCongBHXH= B.FTienTongCongBHXH,
			 A.FTienHoTroCanBo= B.FTienHoTroCanBo,
			 A.FTienChiTapHuan= B.FTienChiTapHuan,
			 A.FTienHoTroQuanLy= B.FTienHoTroQuanLy,
			 A.FTienTongCongKQPL= B.FTienTongCongKQPL,
			 A.FTienChiKCBQYDV= B.FTienChiKCBQYDV,
			 A.FTienChiKCBTSDK= B.FTienChiKCBTSDK,
			 A.FTienChiTNKDQKCBBHYT= B.FTienChiTNKDQKCBBHYT,
			 A.FTienKPMSTTBYT= B.FTienKPMSTTBYT,
			 A.FTienChiKPCSSK= B.FTienChiKPCSSK,
			 A.FTienChiHTBHTN= B.FTienChiHTBHTN,
			 A.FTienTongCongAll= B.FTienTongCongAll
		from #tblResult A,
		(
			select 
			 Sum(FTienTroCapOmDau) FTienTroCapOmDau,
			 Sum(FTienTroCapThaiSan) FTienTroCapThaiSan,
			 Sum(FTienTroTNLDBNN) FTienTroTNLDBNN,
			 Sum(FTienTroCapHuuTri) FTienTroCapHuuTri,
			 Sum(FTienTroCapPhucVien) FTienTroCapPhucVien,
			 Sum(FTienTroCapXuatNgu) FTienTroCapXuatNgu,
			 Sum(FTienTroCapThoiViec) FTienTroCapThoiViec,
			 Sum(FTienTroCapTuTuat) FTienTroCapTuTuat,
			 Sum(FTienTongCongBHXH) FTienTongCongBHXH,
			 Sum(FTienHoTroCanBo) FTienHoTroCanBo,
			 Sum(FTienChiTapHuan) FTienChiTapHuan,
			 Sum(FTienHoTroQuanLy) FTienHoTroQuanLy,
			 Sum(FTienTongCongKQPL) FTienTongCongKQPL,
			 sum(FTienChiKCBQYDV) FTienChiKCBQYDV,
			 SUM(FTienChiKCBTSDK) FTienChiKCBTSDK,
			 sum(FTienChiTNKDQKCBBHYT) FTienChiTNKDQKCBBHYT,
			 sum(FTienKPMSTTBYT) FTienKPMSTTBYT,
			 sum(FTienChiKPCSSK) FTienChiKPCSSK,
			 sum(FTienChiHTBHTN) FTienChiHTBHTN,
			 sum(FTienTongCongAll) FTienTongCongAll
			 from #tblResult
			 where 
			 Type=1
		) B
		where IdParent=1
		---update hach toan
		update A 
		set  A.FTienTroCapOmDau= B.FTienTroCapOmDau,
			 A.FTienTroCapThaiSan= B.FTienTroCapThaiSan,
			 A.FTienTroTNLDBNN= B.FTienTroTNLDBNN,
			 A.FTienTroCapHuuTri= B.FTienTroCapHuuTri,
			 A.FTienTroCapPhucVien= B.FTienTroCapPhucVien,
			 A.FTienTroCapXuatNgu= B.FTienTroCapXuatNgu,
			 A.FTienTroCapThoiViec= B.FTienTroCapThoiViec,
			 A.FTienTroCapTuTuat= B.FTienTroCapTuTuat,
			 A.FTienTongCongBHXH= B.FTienTongCongBHXH,
			 A.FTienHoTroCanBo= B.FTienHoTroCanBo,
			 A.FTienChiTapHuan= B.FTienChiTapHuan,
			 A.FTienHoTroQuanLy= B.FTienHoTroQuanLy,
			 A.FTienTongCongKQPL= B.FTienTongCongKQPL,
			 A.FTienChiKCBQYDV= B.FTienChiKCBQYDV,
			 A.FTienChiKCBTSDK= B.FTienChiKCBTSDK,
			 A.FTienChiTNKDQKCBBHYT= B.FTienChiTNKDQKCBBHYT,
			 A.FTienKPMSTTBYT= B.FTienKPMSTTBYT,
			 A.FTienChiKPCSSK= B.FTienChiKPCSSK,
			 A.FTienChiHTBHTN= B.FTienChiHTBHTN,
			 A.FTienTongCongAll= B.FTienTongCongAll
		from #tblResult A,
		(
			select 
			 Sum(FTienTroCapOmDau) FTienTroCapOmDau,
			 Sum(FTienTroCapThaiSan) FTienTroCapThaiSan,
			 Sum(FTienTroTNLDBNN) FTienTroTNLDBNN,
			 Sum(FTienTroCapHuuTri) FTienTroCapHuuTri,
			 Sum(FTienTroCapPhucVien) FTienTroCapPhucVien,
			 Sum(FTienTroCapXuatNgu) FTienTroCapXuatNgu,
			 Sum(FTienTroCapThoiViec) FTienTroCapThoiViec,
			 Sum(FTienTroCapTuTuat) FTienTroCapTuTuat,
			 Sum(FTienTongCongBHXH) FTienTongCongBHXH,
			 Sum(FTienHoTroCanBo) FTienHoTroCanBo,
			 Sum(FTienChiTapHuan) FTienChiTapHuan,
			 Sum(FTienHoTroQuanLy) FTienHoTroQuanLy,
			 Sum(FTienTongCongKQPL) FTienTongCongKQPL,
			 sum(FTienChiKCBQYDV) FTienChiKCBQYDV,
			 SUM(FTienChiKCBTSDK) FTienChiKCBTSDK,
			 sum(FTienChiTNKDQKCBBHYT) FTienChiTNKDQKCBBHYT,
			 sum(FTienKPMSTTBYT) FTienKPMSTTBYT,
			 sum(FTienChiKPCSSK) FTienChiKPCSSK,
			 sum(FTienChiHTBHTN) FTienChiHTBHTN,
			 sum(FTienTongCongAll) FTienTongCongAll
			 from #tblResult
			 where 
			 Type=2
		) B
		where IdParent=2

		update A 
		set  A.FTienTroCapOmDau= B.FTienTroCapOmDau,
			 A.FTienTroCapThaiSan= B.FTienTroCapThaiSan,
			 A.FTienTroTNLDBNN= B.FTienTroTNLDBNN,
			 A.FTienTroCapHuuTri= B.FTienTroCapHuuTri,
			 A.FTienTroCapPhucVien= B.FTienTroCapPhucVien,
			 A.FTienTroCapXuatNgu= B.FTienTroCapXuatNgu,
			 A.FTienTroCapThoiViec= B.FTienTroCapThoiViec,
			 A.FTienTroCapTuTuat= B.FTienTroCapTuTuat,
			 A.FTienTongCongBHXH= B.FTienTongCongBHXH,
			 A.FTienHoTroCanBo= B.FTienHoTroCanBo,
			 A.FTienChiTapHuan= B.FTienChiTapHuan,
			 A.FTienHoTroQuanLy= B.FTienHoTroQuanLy,
			 A.FTienTongCongKQPL= B.FTienTongCongKQPL,
			 A.FTienChiKCBQYDV= B.FTienChiKCBQYDV,
			 A.FTienChiKCBTSDK= B.FTienChiKCBTSDK,
			 A.FTienChiTNKDQKCBBHYT= B.FTienChiTNKDQKCBBHYT,
			 A.FTienKPMSTTBYT= B.FTienKPMSTTBYT,
			 A.FTienChiKPCSSK= B.FTienChiKPCSSK,
			 A.FTienChiHTBHTN= B.FTienChiHTBHTN,
			 A.FTienTongCongAll= B.FTienTongCongAll
		from #tblResult A,
		(
			select 
			 Sum(FTienTroCapOmDau) FTienTroCapOmDau,
			 Sum(FTienTroCapThaiSan) FTienTroCapThaiSan,
			 Sum(FTienTroTNLDBNN) FTienTroTNLDBNN,
			 Sum(FTienTroCapHuuTri) FTienTroCapHuuTri,
			 Sum(FTienTroCapPhucVien) FTienTroCapPhucVien,
			 Sum(FTienTroCapXuatNgu) FTienTroCapXuatNgu,
			 Sum(FTienTroCapThoiViec) FTienTroCapThoiViec,
			 Sum(FTienTroCapTuTuat) FTienTroCapTuTuat,
			 Sum(FTienTongCongBHXH) FTienTongCongBHXH,
			 Sum(FTienHoTroCanBo) FTienHoTroCanBo,
			 Sum(FTienChiTapHuan) FTienChiTapHuan,
			 Sum(FTienHoTroQuanLy) FTienHoTroQuanLy,
			 Sum(FTienTongCongKQPL) FTienTongCongKQPL,
			 sum(FTienChiKCBQYDV) FTienChiKCBQYDV,
			 SUM(FTienChiKCBTSDK) FTienChiKCBTSDK,
			 sum(FTienChiTNKDQKCBBHYT) FTienChiTNKDQKCBBHYT,
			 sum(FTienKPMSTTBYT) FTienKPMSTTBYT,
			 sum(FTienChiKPCSSK) FTienChiKPCSSK,
			 sum(FTienChiHTBHTN) FTienChiHTBHTN,
			 sum(FTienTongCongAll) FTienTongCongAll
			 from #tblResult
			 where 
			 child=0
		) B
		where IdParent=0
		if(@IsMillionRound = 1)
			select STT,
				SMaDonVi,
				STenDonVi,
				round(ISNULL(FTienTroCapOmDau,0)/1000000,0)*1000000/@DonViTinh FTienTroCapOmDau,
				round(ISNULL(FTienTroCapThaiSan,0)/1000000,0)*1000000/@DonViTinh FTienTroCapThaiSan,
				round(ISNULL(FTienTroTNLDBNN,0)/1000000,0)*1000000/@DonViTinh FTienTroTNLDBNN,
				round(ISNULL(FTienTroCapHuuTri,0)/1000000,0)*1000000/@DonViTinh FTienTroCapHuuTri,
				round(ISNULL(FTienTroCapPhucVien,0)/1000000,0)*1000000/@DonViTinh FTienTroCapPhucVien,
				round(ISNULL(FTienTroCapXuatNgu,0)/1000000,0)*1000000/@DonViTinh FTienTroCapXuatNgu,
				round(ISNULL(FTienTroCapThoiViec,0)/1000000,0)*1000000/@DonViTinh FTienTroCapThoiViec,
				round(ISNULL(FTienTroCapTuTuat,0)/1000000,0)*1000000/@DonViTinh FTienTroCapTuTuat,
				--round(ISNULL(FTienTongCongBHXH,0)/1000000,0)*1000000/@DonViTinh FTienTongCongBHXH,
				round(ISNULL(FTienHoTroCanBo,0)/1000000,0)*1000000/@DonViTinh FTienHoTroCanBo,
				round(ISNULL(FTienChiTapHuan,0)/1000000,0)*1000000/@DonViTinh FTienChiTapHuan,
				round(ISNULL(FTienHoTroQuanLy,0)/1000000,0)*1000000/@DonViTinh FTienHoTroQuanLy,
				--round(ISNULL(FTienTongCongKQPL,0)/1000000,0)*1000000/@DonViTinh FTienTongCongKQPL,
				round(ISNULL(FTienChiKCBQYDV,0)/1000000,0)*1000000/@DonViTinh FTienChiKCBQYDV,
				round(ISNULL(FTienChiKCBTSDK,0)/1000000,0)*1000000/@DonViTinh FTienChiKCBTSDK,
				round(ISNULL(FTienChiTNKDQKCBBHYT,0)/1000000,0)*1000000/@DonViTinh FTienChiTNKDQKCBBHYT,
				round(ISNULL(FTienKPMSTTBYT,0)/1000000,0)*1000000/@DonViTinh FTienKPMSTTBYT,
				round(ISNULL(FTienChiKPCSSK,0)/1000000,0)*1000000/@DonViTinh FTienChiKPCSSK,
				round(ISNULL(FTienChiHTBHTN,0)/1000000,0)*1000000/@DonViTinh FTienChiHTBHTN,
				--round(ISNULL(FTienTongCongAll,0)/1000000,0)*1000000/@DonViTinh FTienTongCongAll,
				BHangCha,
				Type,
				IdParent,
				child
			from #tblResult 
			else
			select STT,
				SMaDonVi,
				STenDonVi,
				round(ISNULL(FTienTroCapOmDau,0)/@DonViTinh,0) FTienTroCapOmDau,
				round(ISNULL(FTienTroCapThaiSan,0)/@DonViTinh,0) FTienTroCapThaiSan,
				round(ISNULL(FTienTroTNLDBNN,0)/@DonViTinh,0) FTienTroTNLDBNN,
				round(ISNULL(FTienTroCapHuuTri,0)/@DonViTinh,0) FTienTroCapHuuTri,
				round(ISNULL(FTienTroCapPhucVien,0)/@DonViTinh,0) FTienTroCapPhucVien,
				round(ISNULL(FTienTroCapXuatNgu,0)/@DonViTinh,0) FTienTroCapXuatNgu,
				round(ISNULL(FTienTroCapThoiViec,0)/@DonViTinh,0) FTienTroCapThoiViec,
				round(ISNULL(FTienTroCapTuTuat,0)/@DonViTinh,0) FTienTroCapTuTuat,
				--round(ISNULL(FTienTongCongBHXH,0)/@DonViTinh,0) FTienTongCongBHXH,
				round(ISNULL(FTienHoTroCanBo,0)/@DonViTinh,0) FTienHoTroCanBo,
				round(ISNULL(FTienChiTapHuan,0)/@DonViTinh,0) FTienChiTapHuan,
				round(ISNULL(FTienHoTroQuanLy,0)/@DonViTinh,0) FTienHoTroQuanLy,
				--round(ISNULL(FTienTongCongKQPL,0)/@DonViTinh,0) FTienTongCongKQPL,
				round(ISNULL(FTienChiKCBQYDV,0)/@DonViTinh,0) FTienChiKCBQYDV,
				round(ISNULL(FTienChiKCBTSDK,0)/@DonViTinh,0) FTienChiKCBTSDK,
				round(ISNULL(FTienChiTNKDQKCBBHYT,0)/@DonViTinh,0) FTienChiTNKDQKCBBHYT,
				round(ISNULL(FTienKPMSTTBYT,0)/@DonViTinh,0) FTienKPMSTTBYT,
				round(ISNULL(FTienChiKPCSSK,0)/@DonViTinh,0) FTienChiKPCSSK,
				round(ISNULL(FTienChiHTBHTN,0)/@DonViTinh,0) FTienChiHTBHTN,
				--round(ISNULL(FTienTongCongAll,0)/@DonViTinh,0) FTienTongCongAll,
				BHangCha,
				Type,
				IdParent,
				child
			from #tblResult 

		Drop table #tblDuToanBHXH
		Drop table #tblHachToanBHXH
		Drop table #tblDuToanKPQL
		Drop table #tblHachToanKPQL
		Drop table #tblDuToanKhac
		Drop table #tblHachToanKhac
		DROP TABLE #tempTongCong
		DROP TABLE #tempKhoiDuToan
		DROP TABLE #tempKhoiHachToan
		DROP TABLE #tempDetailDuToanBHXH
		Drop table #tblHachToanDetailKPQL
		Drop table #tblDuToanDetailKPQL
		Drop table #tempDuToanDetailKhac
		Drop table #tempHachToanDetailKhac
		Drop table #tempDetailHachToanBHXH

		Drop table #tblDuToanSumBHXH
		drop table #tblHachToanSumBHXH
		drop table #tblHachToanSumKhac
		drop table #tblDuToanSumKhac
		drop table #tblDuToanSumKPQL
		drop table #tblHachToanSumKPQL
		drop table #tblDuToanDetailAll
		drop table #tblHachToanDetailAll

		drop table #tempDuToanSumAll
		drop table #tempHachToanSumAll
		drop table #tblResult

END
;
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_phuongan_pbdtc_tachchi_kpql]    Script Date: 8/30/2024 3:10:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[sp_rpt_bhxh_phuongan_pbdtc_tachchi_kpql]
	@NamLamViec int,
	@SoQuyetDinh nvarchar(max),
	@NgayQuyetDinh nvarchar(max),
	@MaDonVi nvarchar(max),
	@DonViTinh int,
	@IsMillionRound bit

AS
BEGIN

		 Select 
		 'I' STT,
		 '' SMaDonVi,
		 N'KHỐI DỰ TOÁN' as STenDonVi,
		 0 FTienTroCapOmDau,
		 0 FTienTroCapThaiSan,
		 0 FTienTroTNLDBNN,
		 0 FTienTroCapHuuTri,
		 0 FTienTroCapPhucVien,
		 0 FTienTroCapXuatNgu,
		 0 FTienTroCapThoiViec,
		 0 FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 0 FTienHoTroCanBo,
		 0 FTienChiTapHuan,
		 0 FTienHoTroQuanLy,
		 0 FTienQuanLyNganhCB,
		 0 FTienQuanLyNganhQL,
		 0 FTienQuanLyNganhTC,
		 0 FTienQuanLyNganhQY,
		 0 FTienTongCongKQPL,
		 0 FTienChiKCBQYDV,
		 0 FTienChiKCBTSDK,
		 0 FTienChiTNKDQKCBBHYT,
		 0 FTienKPMSTTBYT,
		 0 FTienChiKPCSSK,
		 0 FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 1 BHangCha,
		 1 IdParent,
		 1 Type,
		 1 child
		 into #tempKhoiDuToan

		 Select 
		 N'TỔNG CỘNG' STT,
		 '' SMaDonVi,
		 '' as STenDonVi,
		 0 FTienTroCapOmDau,
		 0 FTienTroCapThaiSan,
		 0 FTienTroTNLDBNN,
		 0 FTienTroCapHuuTri,
		 0 FTienTroCapPhucVien,
		 0 FTienTroCapXuatNgu,
		 0 FTienTroCapThoiViec,
		 0 FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 0 FTienHoTroCanBo,
		 0 FTienChiTapHuan,
		 0 FTienHoTroQuanLy,
		 0 FTienQuanLyNganhCB,
		 0 FTienQuanLyNganhQL,
		 0 FTienQuanLyNganhTC,
		 0 FTienQuanLyNganhQY,
		 0 FTienTongCongKQPL,
		 0 FTienChiKCBQYDV,
		 0 FTienChiKCBTSDK,
		 0 FTienChiTNKDQKCBBHYT,
		 0 FTienKPMSTTBYT,
		 0 FTienChiKPCSSK,
		 0 FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 1 BHangCha,
		 0 IdParent,
		 0 Type,
		 1 child
		 into #tempTongCong

		  Select 
		 'II' STT,
		 '' SMaDonVi,
		 N'KHỐI HẠCH TOÁN' as STenDonVi,
		 0 FTienTroCapOmDau,
		 0 FTienTroCapThaiSan,
		 0 FTienTroTNLDBNN,
		 0 FTienTroCapHuuTri,
		 0 FTienTroCapPhucVien,
		 0 FTienTroCapXuatNgu,
		 0 FTienTroCapThoiViec,
		 0 FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 0 FTienHoTroCanBo,
		 0 FTienChiTapHuan,
		 0 FTienHoTroQuanLy,
		 0 FTienQuanLyNganhCB,
		 0 FTienQuanLyNganhQL,
		 0 FTienQuanLyNganhTC,
		 0 FTienQuanLyNganhQY,
		 0 FTienTongCongKQPL,
		 0 FTienChiKCBQYDV,
		 0 FTienChiKCBTSDK,
		 0 FTienChiTNKDQKCBBHYT,
		 0 FTienKPMSTTBYT,
		 0 FTienChiKPCSSK,
		 0 FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 1 BHangCha,
		 2 IdParent,
		 2 Type,
		 1 child
		 into #tempKhoiHachToan

		  --- BHXH
		Select ctct.* into #tblDuToanBHXH from BH_DTC_PhanBoDuToanChi_ChiTiet ctct
		join BH_DTC_PhanBoDuToanChi ct on ctct.iID_DTC_PhanBoDuToanChi = ct.ID
		where ctct.sXauNoiMa LIKE '9010001%'
		and (ct.iLoaiDotNhanPhanBo =1 or ct.iLoaiDotNhanPhanBo=2)
		and ct.sSoQuyetDinh =@SoQuyetDinh
		and  Convert(varchar, ct.dNgayQuyetDinh, 101)=@NgayQuyetDinh
		and ctct.iNamLamViec = @NamLamViec
		and ctct.iID_MaDonVi in (select * from splitstring(@MaDonVi))

		Select ctct.* into #tblHachToanBHXH from BH_DTC_PhanBoDuToanChi_ChiTiet ctct
		join BH_DTC_PhanBoDuToanChi ct on ctct.iID_DTC_PhanBoDuToanChi = ct.ID
		where ctct.sXauNoiMa LIKE '9010002%'
		and (ct.iLoaiDotNhanPhanBo =1 or ct.iLoaiDotNhanPhanBo=2)
		and ct.sSoQuyetDinh = @SoQuyetDinh
		and  Convert(varchar, ct.dNgayQuyetDinh, 101)=@NgayQuyetDinh
		and ctct.iNamLamViec = @NamLamViec
		and  ctct.iID_MaDonVi in (select * from splitstring(@MaDonVi))

		-- KPQL

		Select ctct.* into #tblDuToanKPQL from BH_DuToan_CTCT_KPQL  ctct
		left join DonVi dv on ctct.iID_MaDonVi = dv.iID_MaDonVi
		left join BH_DTC_PhanBoDuToanChi ct on ctct.iID_ChungTu=ct.ID
		where ct.sSoQuyetDinh = @SoQuyetDinh
		and  Convert(varchar, ct.dNgayQuyetDinh, 101)=@NgayQuyetDinh
		and (ct.iLoaiDotNhanPhanBo =1 or ct.iLoaiDotNhanPhanBo=2)
		and ctct.iNamLamViec = @NamLamViec
		and dv.iNamLamViec=@NamLamViec
		and dv.iKhoi=2
		and ctct.iID_MaDonVi in (select * from splitstring(@MaDonVi))

		Select ctct.* into #tblHachToanKPQL from BH_DuToan_CTCT_KPQL  ctct
		left join DonVi dv on ctct.iID_MaDonVi = dv.iID_MaDonVi
		left join BH_DTC_PhanBoDuToanChi ct on ctct.iID_ChungTu=ct.ID
		where ct.sSoQuyetDinh = @SoQuyetDinh
		and  Convert(varchar, ct.dNgayQuyetDinh, 101)=@NgayQuyetDinh
		and (ct.iLoaiDotNhanPhanBo =1 or ct.iLoaiDotNhanPhanBo=2)
		and ctct.iNamLamViec = @NamLamViec
		and dv.iNamLamViec=@NamLamViec
		and dv.iKhoi!=2
		and  ctct.iID_MaDonVi in (select * from splitstring(@MaDonVi))

		--- Khac :KCBQY,KCBTS,Quỹ KCB BHYT quân nhân, mua sắm trang thiết bị y tế,chăm sóc sức khỏe ban đầu HSSV & NLĐ,hỗ trợ BHTN
		Select ctct.* into #tblDuToanKhac from BH_DTC_PhanBoDuToanChi_ChiTiet ctct
		join BH_DTC_PhanBoDuToanChi ct on ctct.iID_DTC_PhanBoDuToanChi = ct.ID
		left join DonVi dv on ctct.iID_MaDonVi=dv.iID_MaDonVi
		where dv.iKhoi=2
		and (ct.iLoaiDotNhanPhanBo =1 or ct.iLoaiDotNhanPhanBo=2)
		and ct.sSoQuyetDinh = @SoQuyetDinh
		and  Convert(varchar, ct.dNgayQuyetDinh, 101)=@NgayQuyetDinh
		and ctct.iNamLamViec = @NamLamViec
		and ctct.iID_MaDonVi in (select * from splitstring(@MaDonVi))


		Select ctct.* into #tblHachToanKhac from BH_DTC_PhanBoDuToanChi_ChiTiet ctct
		join BH_DTC_PhanBoDuToanChi ct on ctct.iID_DTC_PhanBoDuToanChi = ct.ID
		left join DonVi dv on ctct.iID_MaDonVi=dv.iID_MaDonVi
		where dv.iKhoi!=2
		and (ct.iLoaiDotNhanPhanBo =1 or ct.iLoaiDotNhanPhanBo=2)
		and ct.sSoQuyetDinh =@SoQuyetDinh
		and  Convert(varchar, ct.dNgayQuyetDinh, 101)=@NgayQuyetDinh
		and ctct.iNamLamViec = @NamLamViec
		and ctct.iID_MaDonVi in (select * from splitstring(@MaDonVi))

		--- Detail BHXH
		Select 
		 Case when sXauNoiMa='9010001-010-011-0001' then SUM(isnull(fTienTuChi,0)) else 0 end  FTienTroCapOmDau,
		 Case when sXauNoiMa='9010001-010-011-0002' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroCapThaiSan,
		 Case when sXauNoiMa='9010001-010-011-0003' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroTNLDBNN,
		 Case when sXauNoiMa='9010001-010-011-0004' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroCapHuuTri,
		 Case when sXauNoiMa='9010001-010-011-0005' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroCapPhucVien,
		 Case when sXauNoiMa='9010001-010-011-0006' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroCapXuatNgu,
		 Case when sXauNoiMa='9010001-010-011-0007' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroCapThoiViec,
		 Case when sXauNoiMa='9010001-010-011-0008' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 0 FTienHoTroCanBo,
		 0 FTienChiTapHuan,
		 0 FTienHoTroQuanLy,
		 0 FTienTongCongKQPL,
		 0 FTienQuanLyNganhCB,
		 0 FTienQuanLyNganhQL,
		 0 FTienQuanLyNganhTC,
		 0 FTienQuanLyNganhQY,
		 0 FTienChiKCBQYDV,
		 0 FTienChiKCBTSDK,
		 0 FTienChiTNKDQKCBBHYT,
		 0 FTienKPMSTTBYT,
		 0 FTienChiKPCSSK,
		 0 FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 iID_MaDonVi
		 INTO #tempDetailDuToanBHXH
		 from #tblDuToanBHXH
		 GROUP BY sXauNoiMa,iID_MaDonVi

		 SELECT 
		 sum(FTienTroCapOmDau) FTienTroCapOmDau,
		 sum(FTienTroCapThaiSan) FTienTroCapThaiSan,
		 sum(FTienTroTNLDBNN) FTienTroTNLDBNN,
		 sum(FTienTroCapHuuTri) FTienTroCapHuuTri,
		 sum(FTienTroCapPhucVien) FTienTroCapPhucVien,
		 sum(FTienTroCapXuatNgu) FTienTroCapXuatNgu,
		 sum(FTienTroCapThoiViec) FTienTroCapThoiViec,
		 sum(FTienTroCapTuTuat) FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 0 FTienHoTroCanBo,
		 0 FTienChiTapHuan,
		 0 FTienHoTroQuanLy,
		 0 FTienQuanLyNganhCB,
		 0 FTienQuanLyNganhQL,
		 0 FTienQuanLyNganhTC,
		 0 FTienQuanLyNganhQY,
		 0 FTienTongCongKQPL,
		 0 FTienChiKCBQYDV,
		 0 FTienChiKCBTSDK,
		 0 FTienChiTNKDQKCBBHYT,
		 0 FTienKPMSTTBYT,
		 0 FTienChiKPCSSK,
		 0 FTienChiHTBHTN,
		 0 FTienTongCongAll,
		  A.iID_MaDonVi,
		  dv.sTenDonVi
		  into #tblDuToanSumBHXH
		FROM #tempDetailDuToanBHXH A
		left join DonVi dv on A.iID_MaDonVi=dv.iID_MaDonVi
		where iNamLamViec=@NamLamViec
		and dv.iTrangThai=1
		  GROUP BY A.iID_MaDonVi,dv.sTenDonVi


		Select 
		 Case when sXauNoiMa='9010002-010-011-0001' then SUM(isnull(fTienTuChi,0)) else 0 end  FTienTroCapOmDau,
		 Case when sXauNoiMa='9010002-010-011-0002' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroCapThaiSan,
		 Case when sXauNoiMa='9010002-010-011-0003' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroTNLDBNN,
		 Case when sXauNoiMa='9010002-010-011-0004' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroCapHuuTri,
		 Case when sXauNoiMa='9010002-010-011-0005' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroCapPhucVien,
		 Case when sXauNoiMa='9010002-010-011-0006' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroCapXuatNgu,
		 Case when sXauNoiMa='9010002-010-011-0007' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroCapThoiViec,
		 Case when sXauNoiMa='9010002-010-011-0008' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 0 FTienHoTroCanBo,
		 0 FTienChiTapHuan,
		 0 FTienHoTroQuanLy,
		 0 FTienQuanLyNganhCB,
		 0 FTienQuanLyNganhQL,
		 0 FTienQuanLyNganhTC,
		 0 FTienQuanLyNganhQY,
		 0 FTienTongCongKQPL,
		 0 FTienChiKCBQYDV,
		 0 FTienChiKCBTSDK,
		 0 FTienChiTNKDQKCBBHYT,
		 0 FTienKPMSTTBYT,
		 0 FTienChiKPCSSK,
		 0 FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 iID_MaDonVi
		 INTO #tempDetailHachToanBHXH
		 from #tblHachToanBHXH
		 GROUP BY sXauNoiMa,iID_MaDonVi

		 SELECT 
		 sum(FTienTroCapOmDau) FTienTroCapOmDau,
		 sum(FTienTroCapThaiSan) FTienTroCapThaiSan,
		 sum(FTienTroTNLDBNN) FTienTroTNLDBNN,
		 sum(FTienTroCapHuuTri) FTienTroCapHuuTri,
		 sum(FTienTroCapPhucVien) FTienTroCapPhucVien,
		 sum(FTienTroCapXuatNgu) FTienTroCapXuatNgu,
		 sum(FTienTroCapThoiViec) FTienTroCapThoiViec,
		 sum(FTienTroCapTuTuat) FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 0 FTienHoTroCanBo,
		 0 FTienChiTapHuan,
		 0 FTienHoTroQuanLy,
		 0 FTienQuanLyNganhCB,
		 0 FTienQuanLyNganhQL,
		 0 FTienQuanLyNganhTC,
		 0 FTienQuanLyNganhQY,
		 0 FTienTongCongKQPL,
		 0 FTienChiKCBQYDV,
		 0 FTienChiKCBTSDK,
		 0 FTienChiTNKDQKCBBHYT,
		 0 FTienKPMSTTBYT,
		 0 FTienChiKPCSSK,
		 0 FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 A.iID_MaDonVi,
		 dv.sTenDonVi
		 into #tblHachToanSumBHXH
		 FROM #tempDetailHachToanBHXH A
		left join DonVi dv on A.iID_MaDonVi=dv.iID_MaDonVi
		where iNamLamViec=@NamLamViec
		and dv.iTrangThai=1
		  GROUP BY A.iID_MaDonVi,dv.sTenDonVi

		--- KPQL
		Select 
		 0 FTienTroCapOmDau,
		 0 FTienTroCapThaiSan,
		 0 FTienTroTNLDBNN,
		 0 FTienTroCapHuuTri,
		 0 FTienTroCapPhucVien,
		 0 FTienTroCapXuatNgu,
		 0 FTienTroCapThoiViec,
		 0 FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 case when sXauNoiMa='9010011-011' then Sum(isnull(fSoTien,0)) else 0 end FTienHoTroCanBo,
		 case when sXauNoiMa='9010011-012' then Sum(isnull(fSoTien,0)) else 0 end FTienChiTapHuan,
		 0 FTienHoTroQuanLy,
		 case when sXauNoiMa = '9010011-010-0001' or sXauNoiMa = '9010011-013-0001-0001' then Sum(isnull(fSoTien,0)) else 0 end FTienQuanLyNganhCB,
		 case when sXauNoiMa = '9010011-010-0002' or sXauNoiMa = '9010011-013-0001-0002' then Sum(isnull(fSoTien,0)) else 0 end FTienQuanLyNganhQL,
		 case when sXauNoiMa = '9010011-010-0003' or sXauNoiMa = '9010011-013-0001-0003' or sXauNoiMa = '9010011-013-0002' then Sum(isnull(fSoTien,0)) else 0 end FTienQuanLyNganhTC,
		 case when sXauNoiMa = '9010011-010-0004' or sXauNoiMa = '9010011-013-0001-0004' then Sum(isnull(fSoTien,0)) else 0 end FTienQuanLyNganhQY,
		 0 FTienTongCongKQPL,
		 0 FTienChiKCBQYDV,
		 0 FTienChiKCBTSDK,
		 0 FTienChiTNKDQKCBBHYT,
		 0 FTienKPMSTTBYT,
		 0 FTienChiKPCSSK,
		 0 FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 iID_MaDonVi
		 into #tblDuToanDetailKPQL
		 from #tblDuToanKPQL
		 GROUP BY sXauNoiMa,iID_MaDonVi

		 select 
		 0 FTienTroCapOmDau,
		 0 FTienTroCapThaiSan,
		 0 FTienTroTNLDBNN,
		 0 FTienTroCapHuuTri,
		 0 FTienTroCapPhucVien,
		 0 FTienTroCapXuatNgu,
		 0 FTienTroCapThoiViec,
		 0 FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 sum(FTienHoTroCanBo) FTienHoTroCanBo,
		 sum(FTienChiTapHuan) FTienChiTapHuan,
		 sum(FTienHoTroQuanLy) FTienHoTroQuanLy,
		 sum(FTienQuanLyNganhCB) FTienQuanLyNganhCB,
		 sum(FTienQuanLyNganhQL) FTienQuanLyNganhQL,
		 sum(FTienQuanLyNganhTC) FTienQuanLyNganhTC,
		 sum(FTienQuanLyNganhQY) FTienQuanLyNganhQY,
		 0 FTienTongCongKQPL,
		 0 FTienChiKCBQYDV,
		 0 FTienChiKCBTSDK,
		 0 FTienChiTNKDQKCBBHYT,
		 0 FTienKPMSTTBYT,
		 0 FTienChiKPCSSK,
		 0 FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 A.iID_MaDonVi,
		 dv.sTenDonVi
		 into #tblDuToanSumKPQL
		 from #tblDuToanDetailKPQL A
		 left join  DonVi dv on A.iID_MaDonVi=dv.iID_MaDonVi
		 where dv.iNamLamViec=@NamLamViec
		 and dv.iTrangThai=1
		 group by   A.iID_MaDonVi,dv.sTenDonVi


		 Select 
		 0 FTienTroCapOmDau,
		 0 FTienTroCapThaiSan,
		 0 FTienTroTNLDBNN,
		 0 FTienTroCapHuuTri,
		 0 FTienTroCapPhucVien,
		 0 FTienTroCapXuatNgu,
		 0 FTienTroCapThoiViec,
		 0 FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 case when sXauNoiMa='9010011-011' then Sum(isnull(fSoTien,0)) else 0 end FTienHoTroCanBo,
		 case when sXauNoiMa='9010011-012' then Sum(isnull(fSoTien,0)) else 0 end FTienChiTapHuan,
		 0 FTienHoTroQuanLy,
		 case when sXauNoiMa = '9010011-010-0001' or sXauNoiMa = '9010011-013-0001-0001' then Sum(isnull(fSoTien,0)) else 0 end FTienQuanLyNganhCB,
		 case when sXauNoiMa = '9010011-010-0002' or sXauNoiMa = '9010011-013-0001-0002' then Sum(isnull(fSoTien,0)) else 0 end FTienQuanLyNganhQL,
		 case when sXauNoiMa = '9010011-010-0003' or sXauNoiMa = '9010011-013-0001-0003' or sXauNoiMa = '9010011-013-0002' then Sum(isnull(fSoTien,0)) else 0 end FTienQuanLyNganhTC,
		 case when sXauNoiMa = '9010011-010-0004' or sXauNoiMa = '9010011-013-0001-0004' then Sum(isnull(fSoTien,0)) else 0 end FTienQuanLyNganhQY,
		 0 FTienTongCongKQPL,
		 0 FTienChiKCBQYDV,
		 0 FTienChiKCBTSDK,
		 0 FTienChiTNKDQKCBBHYT,
		 0 FTienKPMSTTBYT,
		 0 FTienChiKPCSSK,
		 0 FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 iID_MaDonVi
		 into #tblHachToanDetailKPQL
		 from #tblHachToanKPQL
		 GROUP BY sXauNoiMa,iID_MaDonVi

		 select 
		 0 FTienTroCapOmDau,
		 0 FTienTroCapThaiSan,
		 0 FTienTroTNLDBNN,
		 0 FTienTroCapHuuTri,
		 0 FTienTroCapPhucVien,
		 0 FTienTroCapXuatNgu,
		 0 FTienTroCapThoiViec,
		 0 FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 sum(FTienHoTroCanBo) FTienHoTroCanBo,
		 sum(FTienChiTapHuan) FTienChiTapHuan,
		 sum(FTienHoTroQuanLy) FTienHoTroQuanLy,
		 sum(FTienQuanLyNganhCB) FTienQuanLyNganhCB,
		 sum(FTienQuanLyNganhQL) FTienQuanLyNganhQL,
		 sum(FTienQuanLyNganhTC) FTienQuanLyNganhTC,
		 sum(FTienQuanLyNganhQY) FTienQuanLyNganhQY,
		 0 FTienTongCongKQPL,
		 0 FTienChiKCBQYDV,
		 0 FTienChiKCBTSDK,
		 0 FTienChiTNKDQKCBBHYT,
		 0 FTienKPMSTTBYT,
		 0 FTienChiKPCSSK,
		 0 FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 A.iID_MaDonVi,
		 dv.sTenDonVi
		 into #tblHachToanSumKPQL
		 from #tblHachToanDetailKPQL A
		 left join  DonVi dv on A.iID_MaDonVi=dv.iID_MaDonVi
		 where dv.iNamLamViec=@NamLamViec
		 and dv.iTrangThai=1
		 group by   A.iID_MaDonVi,dv.sTenDonVi

		 --- Khac :KCBQY,KCBTS,Quỹ KCB BHYT quân nhân, mua sắm trang thiết bị y tế,chăm sóc sức khỏe ban đầu HSSV & NLĐ,hỗ trợ BHTN
		 select 
		 0 FTienTroCapOmDau,
		 0 FTienTroCapThaiSan,
		 0 FTienTroTNLDBNN,
		 0 FTienTroCapHuuTri,
		 0 FTienTroCapPhucVien,
		 0 FTienTroCapXuatNgu,
		 0 FTienTroCapThoiViec,
		 0 FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 0 FTienHoTroCanBo,
		 0 FTienChiTapHuan,
		 0 FTienHoTroQuanLy,
		 0 FTienQuanLyNganhCB,
		 0 FTienQuanLyNganhQL,
		 0 FTienQuanLyNganhTC,
		 0 FTienQuanLyNganhQY,
		 0 FTienTongCongKQPL,
		 case when sXauNoiMa='9010004' then SUM(isnull(fTienTuChi,0)) else 0 end FTienChiKCBQYDV,
		 case when sXauNoiMa='9010006' then SUM(isnull(fTienTuChi,0)) else 0 end FTienChiKCBTSDK,
		 case when sXauNoiMa='9010008' then SUM(isnull(fTienTuChi,0)) else 0 end  FTienChiTNKDQKCBBHYT,
		 case when sXauNoiMa='9010009' then SUM(isnull(fTienTuChi,0)) else 0 end FTienKPMSTTBYT,
		 case when sXauNoiMa='905' then SUM(isnull(fTienTuChi,0)) else 0 end FTienChiKPCSSK,
		 case when sXauNoiMa='9010010' then SUM(isnull(fTienTuChi,0)) else 0 end FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 iID_MaDonVi
		 into #tempDuToanDetailKhac
		 from #tblDuToanKhac
		 group by sXauNoiMa,iID_MaDonVi

		 select 
		  0 FTienTroCapOmDau,
		 0 FTienTroCapThaiSan,
		 0 FTienTroTNLDBNN,
		 0 FTienTroCapHuuTri,
		 0 FTienTroCapPhucVien,
		 0 FTienTroCapXuatNgu,
		 0 FTienTroCapThoiViec,
		 0 FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 0 FTienHoTroCanBo,
		 0 FTienChiTapHuan,
		 0 FTienHoTroQuanLy,
		 0 FTienQuanLyNganhCB,
		 0 FTienQuanLyNganhQL,
		 0 FTienQuanLyNganhTC,
		 0 FTienQuanLyNganhQY,
		 0 FTienTongCongKQPL,
		 sum(FTienChiKCBQYDV) FTienChiKCBQYDV,
		 sum(FTienChiKCBTSDK) FTienChiKCBTSDK,
		 sum(FTienChiTNKDQKCBBHYT)  FTienChiTNKDQKCBBHYT,
		 sum(FTienKPMSTTBYT) FTienKPMSTTBYT,
		 sum(FTienChiKPCSSK) FTienChiKPCSSK,
		 sum(FTienChiHTBHTN) FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 A.iID_MaDonVi,
		 dv.sTenDonVi
		  into #tblDuToanSumKhac
		 from #tempDuToanDetailKhac A 
		 left join DonVi dv on A.iID_MaDonVi=dv.iID_MaDonVi
		 where iNamLamViec=@NamLamViec
		 and iTrangThai=1
		 group by A.iID_MaDonVi,dv.sTenDonVi


		  select 
		 0 FTienTroCapOmDau,
		 0 FTienTroCapThaiSan,
		 0 FTienTroTNLDBNN,
		 0 FTienTroCapHuuTri,
		 0 FTienTroCapPhucVien,
		 0 FTienTroCapXuatNgu,
		 0 FTienTroCapThoiViec,
		 0 FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 0 FTienHoTroCanBo,
		 0 FTienChiTapHuan,
		 0 FTienHoTroQuanLy,
		 0 FTienQuanLyNganhCB,
		 0 FTienQuanLyNganhQL,
		 0 FTienQuanLyNganhTC,
		 0 FTienQuanLyNganhQY,
		 0 FTienTongCongKQPL,
		 case when sXauNoiMa='9010004' then SUM(isnull(fTienTuChi,0)) else 0 end FTienChiKCBQYDV,
		 case when sXauNoiMa='9010006' then SUM(isnull(fTienTuChi,0)) else 0 end FTienChiKCBTSDK,
		 case when sXauNoiMa='9010008' then SUM(isnull(fTienTuChi,0)) else 0 end  FTienChiTNKDQKCBBHYT,
		 case when sXauNoiMa='9010009' then SUM(isnull(fTienTuChi,0)) else 0 end FTienKPMSTTBYT,
		 case when sXauNoiMa='905' then SUM(isnull(fTienTuChi,0)) else 0 end FTienChiKPCSSK,
		 case when sXauNoiMa='9010010' then SUM(isnull(fTienTuChi,0)) else 0 end FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 iID_MaDonVi
		 into #tempHachToanDetailKhac
		 from #tblHachToanKhac
		 group by sXauNoiMa,iID_MaDonVi

		 select 
		  0 FTienTroCapOmDau,
		 0 FTienTroCapThaiSan,
		 0 FTienTroTNLDBNN,
		 0 FTienTroCapHuuTri,
		 0 FTienTroCapPhucVien,
		 0 FTienTroCapXuatNgu,
		 0 FTienTroCapThoiViec,
		 0 FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 0 FTienHoTroCanBo,
		 0 FTienChiTapHuan,
		 0 FTienHoTroQuanLy,
		 0 FTienQuanLyNganhCB,
		 0 FTienQuanLyNganhQL,
		 0 FTienQuanLyNganhTC,
		 0 FTienQuanLyNganhQY,
		 0 FTienTongCongKQPL,
		 sum(FTienChiKCBQYDV) FTienChiKCBQYDV,
		 sum(FTienChiKCBTSDK) FTienChiKCBTSDK,
		 sum(FTienChiTNKDQKCBBHYT)  FTienChiTNKDQKCBBHYT,
		 sum(FTienKPMSTTBYT) FTienKPMSTTBYT,
		 sum(FTienChiKPCSSK) FTienChiKPCSSK,
		 sum(FTienChiHTBHTN) FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 A.iID_MaDonVi,
		 dv.sTenDonVi
		 into #tblHachToanSumKhac
		 from #tempHachToanDetailKhac A 
		 left join DonVi dv on A.iID_MaDonVi=dv.iID_MaDonVi
		 where iNamLamViec=@NamLamViec
		 and iTrangThai=1
		 group by A.iID_MaDonVi,dv.sTenDonVi

		 Select * into #tblDuToanDetailAll from
		 (
			select * from #tblDuToanSumBHXH
			union all
			select * from #tblDuToanSumKPQL
			union all
			select * from #tblDuToanSumKhac
		 )tbl 


		  Select *  into #tblHachToanDetailAll from
		 (
			select * from #tblHachToanSumBHXH
			union all
			select * from #tblHachToanSumKPQL
			union all
			select * from #tblHachToanSumKhac
		 )tbl 

		 select 
		 CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iID_MaDonVi ASC))) AS STT,
		 iID_MaDonVi SMaDonVi,
		 sTenDonVi as STenDonVi,
		 Sum(FTienTroCapOmDau) FTienTroCapOmDau,
		 Sum(FTienTroCapThaiSan) FTienTroCapThaiSan,
		 Sum(FTienTroTNLDBNN) FTienTroTNLDBNN,
		 Sum(FTienTroCapHuuTri) FTienTroCapHuuTri,
		 Sum(FTienTroCapPhucVien) FTienTroCapPhucVien,
		 Sum(FTienTroCapXuatNgu) FTienTroCapXuatNgu,
		 Sum(FTienTroCapThoiViec) FTienTroCapThoiViec,
		 Sum(FTienTroCapTuTuat) FTienTroCapTuTuat,
		 Sum(FTienTongCongBHXH) FTienTongCongBHXH,
		 Sum(FTienHoTroCanBo) FTienHoTroCanBo,
		 Sum(FTienChiTapHuan) FTienChiTapHuan,
		 Sum(FTienHoTroQuanLy) FTienHoTroQuanLy,
		 sum(FTienQuanLyNganhCB) FTienQuanLyNganhCB,
		 sum(FTienQuanLyNganhQL) FTienQuanLyNganhQL,
		 sum(FTienQuanLyNganhTC) FTienQuanLyNganhTC,
		 sum(FTienQuanLyNganhQY) FTienQuanLyNganhQY,
		 Sum(FTienTongCongKQPL) FTienTongCongKQPL,
		 sum(FTienChiKCBQYDV) FTienChiKCBQYDV,
		 SUM(FTienChiKCBTSDK) FTienChiKCBTSDK,
		 sum(FTienChiTNKDQKCBBHYT) FTienChiTNKDQKCBBHYT,
		 sum(FTienKPMSTTBYT) FTienKPMSTTBYT,
		 sum(FTienChiKPCSSK) FTienChiKPCSSK,
		 sum(FTienChiHTBHTN) FTienChiHTBHTN,
		 sum(FTienTongCongAll) FTienTongCongAll,
		 0 BHangCha,
		 null IdParent,
		 1 Type,
		 0 child
		 into #tempDuToanSumAll
		from 
		#tblDuToanDetailAll
		group by iID_MaDonVi ,sTenDonVi
		order by iID_MaDonVi


		select 
			CONVERT(nvarchar(10),(ROW_NUMBER() OVER(ORDER BY iID_MaDonVi ASC))) AS STT,
		 iID_MaDonVi SMaDonVi,
		 sTenDonVi as STenDonVi,
		 Sum(FTienTroCapOmDau) FTienTroCapOmDau,
		 Sum(FTienTroCapThaiSan) FTienTroCapThaiSan,
		 Sum(FTienTroTNLDBNN) FTienTroTNLDBNN,
		 Sum(FTienTroCapHuuTri) FTienTroCapHuuTri,
		 Sum(FTienTroCapPhucVien) FTienTroCapPhucVien,
		 Sum(FTienTroCapXuatNgu) FTienTroCapXuatNgu,
		 Sum(FTienTroCapThoiViec) FTienTroCapThoiViec,
		 Sum(FTienTroCapTuTuat) FTienTroCapTuTuat,
		 Sum(FTienTongCongBHXH) FTienTongCongBHXH,
		 Sum(FTienHoTroCanBo) FTienHoTroCanBo,
		 Sum(FTienChiTapHuan) FTienChiTapHuan,
		 Sum(FTienHoTroQuanLy) FTienHoTroQuanLy,
		 sum(FTienQuanLyNganhCB) FTienQuanLyNganhCB,
		 sum(FTienQuanLyNganhQL) FTienQuanLyNganhQL,
		 sum(FTienQuanLyNganhTC) FTienQuanLyNganhTC,
		 sum(FTienQuanLyNganhQY) FTienQuanLyNganhQY,
		 Sum(FTienTongCongKQPL) FTienTongCongKQPL,
		 sum(FTienChiKCBQYDV) FTienChiKCBQYDV,
		 SUM(FTienChiKCBTSDK) FTienChiKCBTSDK,
		 sum(FTienChiTNKDQKCBBHYT) FTienChiTNKDQKCBBHYT,
		 sum(FTienKPMSTTBYT) FTienKPMSTTBYT,
		 sum(FTienChiKPCSSK) FTienChiKPCSSK,
		 sum(FTienChiHTBHTN) FTienChiHTBHTN,
		 sum(FTienTongCongAll) FTienTongCongAll,
		 0 BHangCha,
		 null IdParent,
		 2 Type,
		 0 child
		 into #tempHachToanSumAll
		from 
		#tblHachToanDetailAll
		group by iID_MaDonVi ,sTenDonVi
		order by iID_MaDonVi


		select * into #tblResult from 
		(
			Select * from #tempTongCong
			union all
			Select * from #tempKhoiDuToan
			union all 
			select * from #tempDuToanSumAll
			union all
			Select * from #tempKhoiHachToan
			union all 
			select * from #tempHachToanSumAll
		)tbl

		---update du toan
		update A 
		set  A.FTienTroCapOmDau= B.FTienTroCapOmDau,
			 A.FTienTroCapThaiSan= B.FTienTroCapThaiSan,
			 A.FTienTroTNLDBNN= B.FTienTroTNLDBNN,
			 A.FTienTroCapHuuTri= B.FTienTroCapHuuTri,
			 A.FTienTroCapPhucVien= B.FTienTroCapPhucVien,
			 A.FTienTroCapXuatNgu= B.FTienTroCapXuatNgu,
			 A.FTienTroCapThoiViec= B.FTienTroCapThoiViec,
			 A.FTienTroCapTuTuat= B.FTienTroCapTuTuat,
			 A.FTienTongCongBHXH= B.FTienTongCongBHXH,
			 A.FTienHoTroCanBo= B.FTienHoTroCanBo,
			 A.FTienChiTapHuan= B.FTienChiTapHuan,
			 A.FTienHoTroQuanLy= B.FTienHoTroQuanLy,
			 A.FTienQuanLyNganhCB= B.FTienQuanLyNganhCB,
			 A.FTienQuanLyNganhQL= B.FTienQuanLyNganhQL,
			 A.FTienQuanLyNganhTC= B.FTienQuanLyNganhTC,
			 A.FTienQuanLyNganhQY= B.FTienQuanLyNganhQY,
			 A.FTienTongCongKQPL= B.FTienTongCongKQPL,
			 A.FTienChiKCBQYDV= B.FTienChiKCBQYDV,
			 A.FTienChiKCBTSDK= B.FTienChiKCBTSDK,
			 A.FTienChiTNKDQKCBBHYT= B.FTienChiTNKDQKCBBHYT,
			 A.FTienKPMSTTBYT= B.FTienKPMSTTBYT,
			 A.FTienChiKPCSSK= B.FTienChiKPCSSK,
			 A.FTienChiHTBHTN= B.FTienChiHTBHTN,
			 A.FTienTongCongAll= B.FTienTongCongAll
		from #tblResult A,
		(
			select 
			 Sum(FTienTroCapOmDau) FTienTroCapOmDau,
			 Sum(FTienTroCapThaiSan) FTienTroCapThaiSan,
			 Sum(FTienTroTNLDBNN) FTienTroTNLDBNN,
			 Sum(FTienTroCapHuuTri) FTienTroCapHuuTri,
			 Sum(FTienTroCapPhucVien) FTienTroCapPhucVien,
			 Sum(FTienTroCapXuatNgu) FTienTroCapXuatNgu,
			 Sum(FTienTroCapThoiViec) FTienTroCapThoiViec,
			 Sum(FTienTroCapTuTuat) FTienTroCapTuTuat,
			 Sum(FTienTongCongBHXH) FTienTongCongBHXH,
			 Sum(FTienHoTroCanBo) FTienHoTroCanBo,
			 Sum(FTienChiTapHuan) FTienChiTapHuan,
			 Sum(FTienHoTroQuanLy) FTienHoTroQuanLy,
			 sum(FTienQuanLyNganhCB) FTienQuanLyNganhCB,
			 sum(FTienQuanLyNganhQL) FTienQuanLyNganhQL,
			 sum(FTienQuanLyNganhTC) FTienQuanLyNganhTC,
			 sum(FTienQuanLyNganhQY) FTienQuanLyNganhQY,
			 Sum(FTienTongCongKQPL) FTienTongCongKQPL,
			 sum(FTienChiKCBQYDV) FTienChiKCBQYDV,
			 SUM(FTienChiKCBTSDK) FTienChiKCBTSDK,
			 sum(FTienChiTNKDQKCBBHYT) FTienChiTNKDQKCBBHYT,
			 sum(FTienKPMSTTBYT) FTienKPMSTTBYT,
			 sum(FTienChiKPCSSK) FTienChiKPCSSK,
			 sum(FTienChiHTBHTN) FTienChiHTBHTN,
			 sum(FTienTongCongAll) FTienTongCongAll
			 from #tblResult
			 where 
			 Type=1
		) B
		where IdParent=1
		---update hach toan
		update A 
		set  A.FTienTroCapOmDau= B.FTienTroCapOmDau,
			 A.FTienTroCapThaiSan= B.FTienTroCapThaiSan,
			 A.FTienTroTNLDBNN= B.FTienTroTNLDBNN,
			 A.FTienTroCapHuuTri= B.FTienTroCapHuuTri,
			 A.FTienTroCapPhucVien= B.FTienTroCapPhucVien,
			 A.FTienTroCapXuatNgu= B.FTienTroCapXuatNgu,
			 A.FTienTroCapThoiViec= B.FTienTroCapThoiViec,
			 A.FTienTroCapTuTuat= B.FTienTroCapTuTuat,
			 A.FTienTongCongBHXH= B.FTienTongCongBHXH,
			 A.FTienHoTroCanBo= B.FTienHoTroCanBo,
			 A.FTienChiTapHuan= B.FTienChiTapHuan,
			 A.FTienHoTroQuanLy= B.FTienHoTroQuanLy,
			 A.FTienQuanLyNganhCB= B.FTienQuanLyNganhCB,
			 A.FTienQuanLyNganhQL= B.FTienQuanLyNganhQL,
			 A.FTienQuanLyNganhTC= B.FTienQuanLyNganhTC,
			 A.FTienQuanLyNganhQY= B.FTienQuanLyNganhQY,
			 A.FTienTongCongKQPL= B.FTienTongCongKQPL,
			 A.FTienChiKCBQYDV= B.FTienChiKCBQYDV,
			 A.FTienChiKCBTSDK= B.FTienChiKCBTSDK,
			 A.FTienChiTNKDQKCBBHYT= B.FTienChiTNKDQKCBBHYT,
			 A.FTienKPMSTTBYT= B.FTienKPMSTTBYT,
			 A.FTienChiKPCSSK= B.FTienChiKPCSSK,
			 A.FTienChiHTBHTN= B.FTienChiHTBHTN,
			 A.FTienTongCongAll= B.FTienTongCongAll
		from #tblResult A,
		(
			select 
			 Sum(FTienTroCapOmDau) FTienTroCapOmDau,
			 Sum(FTienTroCapThaiSan) FTienTroCapThaiSan,
			 Sum(FTienTroTNLDBNN) FTienTroTNLDBNN,
			 Sum(FTienTroCapHuuTri) FTienTroCapHuuTri,
			 Sum(FTienTroCapPhucVien) FTienTroCapPhucVien,
			 Sum(FTienTroCapXuatNgu) FTienTroCapXuatNgu,
			 Sum(FTienTroCapThoiViec) FTienTroCapThoiViec,
			 Sum(FTienTroCapTuTuat) FTienTroCapTuTuat,
			 Sum(FTienTongCongBHXH) FTienTongCongBHXH,
			 Sum(FTienHoTroCanBo) FTienHoTroCanBo,
			 Sum(FTienChiTapHuan) FTienChiTapHuan,
			 Sum(FTienHoTroQuanLy) FTienHoTroQuanLy,
			 sum(FTienQuanLyNganhCB) FTienQuanLyNganhCB,
			 sum(FTienQuanLyNganhQL) FTienQuanLyNganhQL,
			 sum(FTienQuanLyNganhTC) FTienQuanLyNganhTC,
			 sum(FTienQuanLyNganhQY) FTienQuanLyNganhQY,
			 Sum(FTienTongCongKQPL) FTienTongCongKQPL,
			 sum(FTienChiKCBQYDV) FTienChiKCBQYDV,
			 SUM(FTienChiKCBTSDK) FTienChiKCBTSDK,
			 sum(FTienChiTNKDQKCBBHYT) FTienChiTNKDQKCBBHYT,
			 sum(FTienKPMSTTBYT) FTienKPMSTTBYT,
			 sum(FTienChiKPCSSK) FTienChiKPCSSK,
			 sum(FTienChiHTBHTN) FTienChiHTBHTN,
			 sum(FTienTongCongAll) FTienTongCongAll
			 from #tblResult
			 where 
			 Type=2
		) B
		where IdParent=2

		update A 
		set  A.FTienTroCapOmDau= B.FTienTroCapOmDau,
			 A.FTienTroCapThaiSan= B.FTienTroCapThaiSan,
			 A.FTienTroTNLDBNN= B.FTienTroTNLDBNN,
			 A.FTienTroCapHuuTri= B.FTienTroCapHuuTri,
			 A.FTienTroCapPhucVien= B.FTienTroCapPhucVien,
			 A.FTienTroCapXuatNgu= B.FTienTroCapXuatNgu,
			 A.FTienTroCapThoiViec= B.FTienTroCapThoiViec,
			 A.FTienTroCapTuTuat= B.FTienTroCapTuTuat,
			 A.FTienTongCongBHXH= B.FTienTongCongBHXH,
			 A.FTienHoTroCanBo= B.FTienHoTroCanBo,
			 A.FTienChiTapHuan= B.FTienChiTapHuan,
			 A.FTienHoTroQuanLy= B.FTienHoTroQuanLy,
			 A.FTienQuanLyNganhCB= B.FTienQuanLyNganhCB,
			 A.FTienQuanLyNganhQL= B.FTienQuanLyNganhQL,
			 A.FTienQuanLyNganhTC= B.FTienQuanLyNganhTC,
			 A.FTienQuanLyNganhQY= B.FTienQuanLyNganhQY,
			 A.FTienTongCongKQPL= B.FTienTongCongKQPL,
			 A.FTienChiKCBQYDV= B.FTienChiKCBQYDV,
			 A.FTienChiKCBTSDK= B.FTienChiKCBTSDK,
			 A.FTienChiTNKDQKCBBHYT= B.FTienChiTNKDQKCBBHYT,
			 A.FTienKPMSTTBYT= B.FTienKPMSTTBYT,
			 A.FTienChiKPCSSK= B.FTienChiKPCSSK,
			 A.FTienChiHTBHTN= B.FTienChiHTBHTN,
			 A.FTienTongCongAll= B.FTienTongCongAll
		from #tblResult A,
		(
			select 
			 Sum(FTienTroCapOmDau) FTienTroCapOmDau,
			 Sum(FTienTroCapThaiSan) FTienTroCapThaiSan,
			 Sum(FTienTroTNLDBNN) FTienTroTNLDBNN,
			 Sum(FTienTroCapHuuTri) FTienTroCapHuuTri,
			 Sum(FTienTroCapPhucVien) FTienTroCapPhucVien,
			 Sum(FTienTroCapXuatNgu) FTienTroCapXuatNgu,
			 Sum(FTienTroCapThoiViec) FTienTroCapThoiViec,
			 Sum(FTienTroCapTuTuat) FTienTroCapTuTuat,
			 Sum(FTienTongCongBHXH) FTienTongCongBHXH,
			 Sum(FTienHoTroCanBo) FTienHoTroCanBo,
			 Sum(FTienChiTapHuan) FTienChiTapHuan,
			 Sum(FTienHoTroQuanLy) FTienHoTroQuanLy,
			 sum(FTienQuanLyNganhCB) FTienQuanLyNganhCB,
			 sum(FTienQuanLyNganhQL) FTienQuanLyNganhQL,
			 sum(FTienQuanLyNganhTC) FTienQuanLyNganhTC,
			 sum(FTienQuanLyNganhQY) FTienQuanLyNganhQY,
			 Sum(FTienTongCongKQPL) FTienTongCongKQPL,
			 sum(FTienChiKCBQYDV) FTienChiKCBQYDV,
			 SUM(FTienChiKCBTSDK) FTienChiKCBTSDK,
			 sum(FTienChiTNKDQKCBBHYT) FTienChiTNKDQKCBBHYT,
			 sum(FTienKPMSTTBYT) FTienKPMSTTBYT,
			 sum(FTienChiKPCSSK) FTienChiKPCSSK,
			 sum(FTienChiHTBHTN) FTienChiHTBHTN,
			 sum(FTienTongCongAll) FTienTongCongAll
			 from #tblResult
			 where 
			 child=0
		) B
		where IdParent=0
		if(@IsMillionRound = 1)
			select STT,
				SMaDonVi,
				STenDonVi,
				round(ISNULL(FTienTroCapOmDau,0)/1000000,0)*1000000/@DonViTinh FTienTroCapOmDau,
				round(ISNULL(FTienTroCapThaiSan,0)/1000000,0)*1000000/@DonViTinh FTienTroCapThaiSan,
				round(ISNULL(FTienTroTNLDBNN,0)/1000000,0)*1000000/@DonViTinh FTienTroTNLDBNN,
				round(ISNULL(FTienTroCapHuuTri,0)/1000000,0)*1000000/@DonViTinh FTienTroCapHuuTri,
				round(ISNULL(FTienTroCapPhucVien,0)/1000000,0)*1000000/@DonViTinh FTienTroCapPhucVien,
				round(ISNULL(FTienTroCapXuatNgu,0)/1000000,0)*1000000/@DonViTinh FTienTroCapXuatNgu,
				round(ISNULL(FTienTroCapThoiViec,0)/1000000,0)*1000000/@DonViTinh FTienTroCapThoiViec,
				round(ISNULL(FTienTroCapTuTuat,0)/1000000,0)*1000000/@DonViTinh FTienTroCapTuTuat,
				--round(ISNULL(FTienTongCongBHXH,0)/1000000,0)*1000000/@DonViTinh FTienTongCongBHXH,
				round(ISNULL(FTienHoTroCanBo,0)/1000000,0)*1000000/@DonViTinh FTienHoTroCanBo,
				round(ISNULL(FTienChiTapHuan,0)/1000000,0)*1000000/@DonViTinh FTienChiTapHuan,
				round(ISNULL(FTienHoTroQuanLy,0)/1000000,0)*1000000/@DonViTinh FTienHoTroQuanLy,
				round(ISNULL(FTienQuanLyNganhCB,0)/1000000,0)*1000000/@DonViTinh FTienQuanLyNganhCB,
				round(ISNULL(FTienQuanLyNganhQL,0)/1000000,0)*1000000/@DonViTinh FTienQuanLyNganhQL,
				round(ISNULL(FTienQuanLyNganhTC,0)/1000000,0)*1000000/@DonViTinh FTienQuanLyNganhTC,
				round(ISNULL(FTienQuanLyNganhQY,0)/1000000,0)*1000000/@DonViTinh FTienQuanLyNganhQY,
				--round(ISNULL(FTienTongCongKQPL,0)/1000000,0)*1000000/@DonViTinh FTienTongCongKQPL,
				round(ISNULL(FTienChiKCBQYDV,0)/1000000,0)*1000000/@DonViTinh FTienChiKCBQYDV,
				round(ISNULL(FTienChiKCBTSDK,0)/1000000,0)*1000000/@DonViTinh FTienChiKCBTSDK,
				round(ISNULL(FTienChiTNKDQKCBBHYT,0)/1000000,0)*1000000/@DonViTinh FTienChiTNKDQKCBBHYT,
				round(ISNULL(FTienKPMSTTBYT,0)/1000000,0)*1000000/@DonViTinh FTienKPMSTTBYT,
				round(ISNULL(FTienChiKPCSSK,0)/1000000,0)*1000000/@DonViTinh FTienChiKPCSSK,
				round(ISNULL(FTienChiHTBHTN,0)/1000000,0)*1000000/@DonViTinh FTienChiHTBHTN,
				--round(ISNULL(FTienTongCongAll,0)/1000000,0)*1000000/@DonViTinh FTienTongCongAll,
				BHangCha,
				Type,
				IdParent,
				child
			from #tblResult 
			else
			select STT,
				SMaDonVi,
				STenDonVi,
				round(ISNULL(FTienTroCapOmDau,0)/@DonViTinh,0) FTienTroCapOmDau,
				round(ISNULL(FTienTroCapThaiSan,0)/@DonViTinh,0) FTienTroCapThaiSan,
				round(ISNULL(FTienTroTNLDBNN,0)/@DonViTinh,0) FTienTroTNLDBNN,
				round(ISNULL(FTienTroCapHuuTri,0)/@DonViTinh,0) FTienTroCapHuuTri,
				round(ISNULL(FTienTroCapPhucVien,0)/@DonViTinh,0) FTienTroCapPhucVien,
				round(ISNULL(FTienTroCapXuatNgu,0)/@DonViTinh,0) FTienTroCapXuatNgu,
				round(ISNULL(FTienTroCapThoiViec,0)/@DonViTinh,0) FTienTroCapThoiViec,
				round(ISNULL(FTienTroCapTuTuat,0)/@DonViTinh,0) FTienTroCapTuTuat,
				--round(ISNULL(FTienTongCongBHXH,0)/@DonViTinh,0) FTienTongCongBHXH,
				round(ISNULL(FTienHoTroCanBo,0)/@DonViTinh,0) FTienHoTroCanBo,
				round(ISNULL(FTienChiTapHuan,0)/@DonViTinh,0) FTienChiTapHuan,
				round(ISNULL(FTienHoTroQuanLy,0)/@DonViTinh,0) FTienHoTroQuanLy,
				round(ISNULL(FTienQuanLyNganhCB,0)/@DonViTinh,0) FTienQuanLyNganhCB,
				round(ISNULL(FTienQuanLyNganhQL,0)/@DonViTinh,0) FTienQuanLyNganhQL,
				round(ISNULL(FTienQuanLyNganhTC,0)/@DonViTinh,0) FTienQuanLyNganhTC,
				round(ISNULL(FTienQuanLyNganhQY,0)/@DonViTinh,0) FTienQuanLyNganhQY,
				--round(ISNULL(FTienTongCongKQPL,0)/@DonViTinh,0) FTienTongCongKQPL,
				round(ISNULL(FTienChiKCBQYDV,0)/@DonViTinh,0) FTienChiKCBQYDV,
				round(ISNULL(FTienChiKCBTSDK,0)/@DonViTinh,0) FTienChiKCBTSDK,
				round(ISNULL(FTienChiTNKDQKCBBHYT,0)/@DonViTinh,0) FTienChiTNKDQKCBBHYT,
				round(ISNULL(FTienKPMSTTBYT,0)/@DonViTinh,0) FTienKPMSTTBYT,
				round(ISNULL(FTienChiKPCSSK,0)/@DonViTinh,0) FTienChiKPCSSK,
				round(ISNULL(FTienChiHTBHTN,0)/@DonViTinh,0) FTienChiHTBHTN,
				--round(ISNULL(FTienTongCongAll,0)/@DonViTinh,0) FTienTongCongAll,
				BHangCha,
				Type,
				IdParent,
				child
			from #tblResult 

		Drop table #tblDuToanBHXH
		Drop table #tblHachToanBHXH
		Drop table #tblDuToanKPQL
		Drop table #tblHachToanKPQL
		Drop table #tblDuToanKhac
		Drop table #tblHachToanKhac
		DROP TABLE #tempTongCong
		DROP TABLE #tempKhoiDuToan
		DROP TABLE #tempKhoiHachToan
		DROP TABLE #tempDetailDuToanBHXH
		Drop table #tblHachToanDetailKPQL
		Drop table #tblDuToanDetailKPQL
		Drop table #tempDuToanDetailKhac
		Drop table #tempHachToanDetailKhac
		Drop table #tempDetailHachToanBHXH

		Drop table #tblDuToanSumBHXH
		drop table #tblHachToanSumBHXH
		drop table #tblHachToanSumKhac
		drop table #tblDuToanSumKhac
		drop table #tblDuToanSumKPQL
		drop table #tblHachToanSumKPQL
		drop table #tblDuToanDetailAll
		drop table #tblHachToanDetailAll

		drop table #tempDuToanSumAll
		drop table #tempHachToanSumAll
		drop table #tblResult

END
;
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh]    Script Date: 8/30/2024 3:10:06 PM ******/
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
	left join TL_DS_CapNhap_BangLuong bangluong  ON LHT_TT.iID_Parent = bangluong.Id
	where LHT_TT.iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND LHT_TT.iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND isnull(bangluong.STongHop, '''') <> ''''
		AND LHT_TT.sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND LHT_TT.sMaCheDo in (''OK_D14N_LBH_TT'', ''OK_T14N_LBH_TT'', ''BDN_D14N_LBH_TT'', ''BDN_T14N_LBH_TT'', ''CONOM_LBH_TT'', ''KT_LBH_TT'', ''NAMNGHIVIEC_LBH_TT'', ''KHHGD_LBH_TT'')
	group by LHT_TT.sMaCbo, LHT_TT.sTenCbo, LHT_TT.sMaDonVi, LHT_TT.iNam, LHT_TT.iThang) LHT_TT on base.sMaCbo = LHT_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(BH_TT.nGiaTri) GiaTri_PCCVBH_TT
	from TL_BangLuong_ThangBHXH BH_TT
	left join TL_DS_CapNhap_BangLuong bangluong  ON BH_TT.iID_Parent = bangluong.Id
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND isnull(bangluong.STongHop, '''') <> ''''
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_PCCVBH_TT'', ''OK_T14N_PCCVBH_TT'', ''BDN_D14N_PCCVBH_TT'', ''BDN_T14N_PCCVBH_TT'', ''CONOM_PCCVBH_TT'', ''KT_PCCVBH_TT'', ''NAMNGHIVIEC_PCCVBH_TT'', ''KHHGD_PCCVBH_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCCV_TT on base.sMaCbo = PCCV_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(BH_TT.nGiaTri) GiaTri_PCTNBH_TT
	from TL_BangLuong_ThangBHXH BH_TT
	left join TL_DS_CapNhap_BangLuong bangluong  ON BH_TT.iID_Parent = bangluong.Id
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND isnull(bangluong.STongHop, '''') <> ''''
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_PCTNBH_TT'', ''OK_T14N_PCTNBH_TT'', ''BDN_D14N_PCTNBH_TT'', ''BDN_T14N_PCTNBH_TT'', ''CONOM_PCTNBH_TT'', ''KT_PCTNBH_TT'', ''NAMNGHIVIEC_PCTNBH_TT'', ''KHHGD_PCTNBH_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCTN_TT on base.sMaCbo = PCTN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(BH_TT.nGiaTri) GiaTri_BHXHCN_TT
	from TL_BangLuong_ThangBHXH BH_TT
	left join TL_DS_CapNhap_BangLuong bangluong  ON BH_TT.iID_Parent = bangluong.Id
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND isnull(bangluong.STongHop, '''') <> ''''
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_BHXHCN_TT'', ''OK_T14N_BHXHCN_TT'', ''BDN_D14N_BHXHCN_TT'', ''BDN_T14N_BHXHCN_TT'', ''CONOM_BHXHCN_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) BHXHCN_TT on base.sMaCbo = BHXHCN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(BH_TT.nGiaTri) GiaTri_BHYTCN_TT
	from TL_BangLuong_ThangBHXH BH_TT
	left join TL_DS_CapNhap_BangLuong bangluong  ON BH_TT.iID_Parent = bangluong.Id
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND isnull(bangluong.STongHop, '''') <> ''''
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''BDN_D14N_BHYTCN_TT'', ''OK_D14N_BHYTCN_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) BHYTCN_TT on base.sMaCbo = BHYTCN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(BH_TT.nGiaTri) GiaTri_PCTNVKBH_TT
	from TL_BangLuong_ThangBHXH BH_TT
	left join TL_DS_CapNhap_BangLuong bangluong  ON BH_TT.iID_Parent = bangluong.Id
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND isnull(bangluong.STongHop, '''') <> ''''
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_PCTNVKBH_TT'', ''OK_T14N_PCTNVKBH_TT'', ''BDN_D14N_PCTNVKBH_TT'', ''BDN_T14N_PCTNVKBH_TT'', ''CONOM_PCTNVKBH_TT'', ''KT_PCTNVKBH_TT'', ''NAMNGHIVIEC_PCTNVKBH_TT'', ''KHHGD_PCTNVKBH_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCTNVK_TT on base.sMaCbo = PCTNVK_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(BH_TT.nGiaTri) GiaTri_HSBLBH_TT
	from TL_BangLuong_ThangBHXH BH_TT
	left join TL_DS_CapNhap_BangLuong bangluong  ON BH_TT.iID_Parent = bangluong.Id
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND isnull(bangluong.STongHop, '''') <> ''''
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_HSBLBH_TT'', ''OK_T14N_HSBLBH_TT'', ''BDN_D14N_HSBLBH_TT'', ''BDN_T14N_HSBLBH_TT'', ''CONOM_HSBLBH_TT'', ''KT_HSBLBH_TT'', ''NAMNGHIVIEC_HSBLBH_TT'', ''KHHGD_HSBLBH_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) HSBL_TT on base.sMaCbo = HSBL_TT.sMaCbo
	left join TL_DS_CapNhap_BangLuong bangluong  ON base.iID_Parent = bangluong.Id
	where base.iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND base.iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND base.sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND bangluong.STongHop is not null
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
INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) VALUES (N'be209003-38bd-4cb8-ad2c-6b4215291c14', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rptBHXH_PhuongAn_DuToan_TachKPQL', NULL, N'rptBHXH_PhuongAn_DuToan_TachKPQL', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Báo cáo phương án phân bổ dự toán chi BHXH, BHYT, BHTN', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', N'Phụ lục', NULL, N'PHƯƠNG ÁN PHÂN BỔ DỰ TOÁN CHI CHẾ ĐỘ BẢO HIỂM XÃ HỘI, HỖ TRỢ KINH PHÍ QUẢN LÝ BHXH, BHYT VÀ CHI KHÁM CHỮA BỆNH NĂM', N'(Kèm theo văn bản ngày......tháng.......năm......của..............)', NULL, NULL, NULL, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) VALUES (N'cbc201c5-490f-4b1b-b1e3-b621d81d8dd8', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rptBHXH_PhuongAn_DuToan_GopKPQL', NULL, N'rptBHXH_PhuongAn_DuToan_GopKPQL', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Báo cáo phương án phân bổ dự toán chi BHXH, BHYT, BHTN', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', N'Phụ lục', NULL, N'PHƯƠNG ÁN PHÂN BỔ DỰ TOÁN CHI CHẾ ĐỘ BẢO HIỂM XÃ HỘI, HỖ TRỢ KINH PHÍ QUẢN LÝ BHXH, BHYT VÀ CHI KHÁM CHỮA BỆNH NĂM', N'(Kèm theo văn bản ngày......tháng.......năm......của..............)', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

/****** Object:  StoredProcedure [dbo].[sp_tl_chungtu_chitiet_bhxh_cach4]    Script Date: 8/30/2024 4:32:57 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_chungtu_chitiet_bhxh_cach4]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_chungtu_chitiet_bhxh_cach4]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_chungtu_chitiet_bhxh_cach3]    Script Date: 8/30/2024 4:32:57 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_chungtu_chitiet_bhxh_cach3]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_chungtu_chitiet_bhxh_cach3]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_chungtu_chitiet_bhxh_cach2]    Script Date: 8/30/2024 4:32:57 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_chungtu_chitiet_bhxh_cach2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_chungtu_chitiet_bhxh_cach2]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_chungtu_chitiet_bhxh_cach2]    Script Date: 8/30/2024 4:32:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_tl_chungtu_chitiet_bhxh_cach2]
	@maDonVi VARCHAR(MAX),
	@thang INT,
	@nam INT,
	@maCachTl NVARCHAR(50)
AS
BEGIN
	
	DECLARE @checkTongHop INT; 
	SELECT @checkTongHop = count(*) FROM f_split(@maCachTl);


	SELECT Ma_Cot, pc.Ma_PhuCap into #tmp from (
		SELECT Ma_Cot, CONCAT('|', REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(CAST(CongThuc as nvarchar(max)),'+','|'),'-','|'),'*','|'),'/','|'),'(','|'),')','|'), '|') as CongThuc FROM TL_DM_Cach_TinhLuong_Chuan WHERE CongThuc IS NOT NULL) as tbl
		INNER  JOIN TL_DM_PhuCap as pc on tbl.CongThuc LIKE CONCAT('%|', pc.Ma_PhuCap, '|%') or pc.Parent in ('TIENAN', 'TIENAN2')

	select * into #tmpMapping
	FROM (
		select Ma_Cot, Ma_Cot as Ma_PhuCap from #tmp
		union 
		select Ma_PhuCap as Ma_Cot, Ma_PhuCap as Ma_PhuCap from #tmp
	) AS c

	SELECT mp.Ma_Cot, cb.Ma_CanBo, SUM(ISNULL(pc.fSoNgayHuongBHXH, 0)) as HuongPC_SN INTO #tmpSoNgay
	FROM TL_DM_CanBo as cb
	INNER JOIN TL_CanBo_CheDoBHXH as pc on cb.Ma_CanBo = pc.sMaCanBo
	INNER JOIN #tmpMapping as mp on pc.sMaCheDo = mp.Ma_PhuCap
	WHERE cb.Parent in (SELECT * FROM f_split(@MaDonVi))
	AND Thang = @thang
	AND Nam = @nam
	GROUP BY mp.Ma_Cot, cb.Ma_CanBo;

	SELECT
		dsCapNhapBangLuong.Ma_CBo	AS MaDonVi,
		dsCapNhapBangLuong.Thang	AS Thang,
		dsCapNhapBangLuong.Nam		AS Nam,
		bangLuong.sMaCheDo			AS MaPhuCap,
		bangLuong.sMaCB				AS MaCapBac,
		case when bangLuong.sMaCB in ('3.1', '3.2', '3.3', '3.4', '3.5', '413', '415') then bangLuong.sMaCB else capBac.Parent end AS Ngach,
		--SUM(case when ISNULL(cbpc.HuongPC_SN, 0) = 0 then dbo.fnTotalDayOfMonth(@thang,@nam) else cbpc.HuongPC_SN end) AS SoNgay,
		SUM(ISNULL(cbpc.HuongPC_SN, 0)) AS SoNgay,
		SUM(bangLuong.nGiaTri) AS GiaTri,
		SUM(CASE WHEN @checkTongHop = 2 AND dsCapNhapBangLuong.Ma_CachTL ='CACH5' THEN 0 ELSE 1 END)	AS SoNguoi
	INTO #LuongCapBac
	FROM (SELECT * FROM TL_BangLuong_ThangBHXH WHERE iNam = @nam AND iThang = @thang AND nGiaTri != 0) bangLuong
	INNER JOIN TL_CanBo_CheDoBHXH as cbpc1 on bangLuong.sMaCBo = cbpc1.sMaCanBo AND bangLuong.sMaCheDo = cbpc1.sMaCheDo
	INNER JOIN TL_DM_CheDoBHXH as pc on bangLuong.sMaCheDo = pc.sMaCheDo
	LEFT JOIN #tmpSoNgay  as cbpc on bangLuong.sMaCheDo = cbpc.Ma_Cot AND bangLuong.sMaCBo = cbpc.Ma_CanBo
	JOIN 
	(SELECT * FROM TL_DS_CapNhap_BangLuong WHERE NAM = @Nam AND THANG = @Thang AND Ma_CachTL IN (SELECT * FROM f_split(@maCachTl)) AND Ma_CBo IN (SELECT * FROM f_split(@MaDonVi))  and isnull(STongHop, '') <> '') dsCapNhapBangLuong 
	ON bangLuong.iID_Parent = dsCapNhapBangLuong.Id AND bangLuong.iThang = dsCapNhapBangLuong.Thang and bangLuong.iNam = dsCapNhapBangLuong.Nam 
	LEFT JOIN TL_DM_CapBac capBac ON bangLuong.sMaCB = capBac.Ma_Cb		
	GROUP BY dsCapNhapBangLuong.Ma_CBo, dsCapNhapBangLuong.Thang, dsCapNhapBangLuong.Nam, bangLuong.sMaCheDo,bangLuong.sMaCB, capBac.Parent
		
	--Biay Nam chot sua
	update #LuongCapBac set Ngach = '3.3' where MaCapBac in ('413', '415');

	SELECT distinct
		luongCapBac.MaDonVi,
		phuCapMlns.XauNoiMa,
		phuCapMlns.Ma_Cb,
		SoNguoi,
		SoNgay,
		GiaTri,
		luongCapBac.Thang
		INTO #LuongCapBacMlns
	FROM TL_PhuCap_MLNS phuCapMlns
	JOIN #LuongCapBac luongCapBac ON phuCapMlns.Ma_Cb = luongCapBac.Ngach AND phuCapMlns.Ma_PhuCap = luongCapBac.MaPhuCap
	WHERE
		phuCapMlns.Nam = @nam

	Select Ma_DonVi, XauNoiMa, SUM(DDuToan) DuToan
	INTO #DataDuToan
	From TL_QT_ChungTuChiTiet ctchitiet
	Join TL_QT_ChungTu chungtu
	on chungtu.ID = ctchitiet.Id_ChungTu
	Where Nam = @nam
	And Ma_DonVi IN (SELECT * FROM f_split(@maCachTl))
	Group By XauNoiMa, Ma_DonVi

	SELECT
		luong.MaDonVi					AS MaDonVi,
		NEWID()							AS Id,
		iID_MLNS						AS MlnsId, 
		iID_MLNS_Cha					AS MlnsIdParent, 
		sXauNoiMa						AS XauNoiMa, 
		sLNS							AS Lns, 
		sL								AS L, 
		sK								AS K, 
		sM								AS M, 
		sTM								AS TM, 
		sTTM							AS TTM, 
		sNG								AS Ng, 
		sTNG							AS TNG, 
		sTNG1							AS TNG1, 
		sTNG2							AS TNG2, 
		sTNG3							AS TNG3, 
		sMoTa							AS Mota, 
		iNamLamViec						AS NamLamViec,
		bHangCha						AS BHangCha,
		sChiTietToi						AS ChiTietToi,
		CONVERT(decimal, SUM(SoNguoi))	AS SoNguoi,
		SUM(ISNULL(SoNgay, 0))			AS SoNgay,
		SUM(GiaTri)						AS TongCong,
		SUM(GiaTri)						AS DieuChinh,
		@maCachTl						AS MaCachTl,
		ISNULL(dataDuToan.DuToan, 0)	AS DDuToan,
		luong.Ma_Cb						AS MaCb,
		luong.Thang as Thang
	FROM NS_MucLucNganSach mlns
	JOIN #LuongCapBacMlns luong
		ON mlns.sXauNoiMa = luong.XauNoiMa
	LEFT JOIN #DataDuToan dataDuToan
		ON luong.XauNoiMa = dataDuToan.XauNoiMa and luong.MaDonVi = dataDuToan.Ma_DonVi
	WHERE
		sLNS IN ('1', '101', '1010000')
		AND iNamLamViec = @nam
		AND luong.Thang = @thang
	GROUP BY MaDonVi, iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, iNamLamViec, sChiTietToi, bHangCha, DuToan, luong.Ma_Cb, luong.Thang
	ORDER BY MaDonVi, sXauNoiMa

	DROP TABLE #LuongCapBac
	DROP TABLE #DataDuToan
	DROP TABLE #LuongCapBacMlns
	DROP TABLE #tmpSoNgay
	DROP TABLE #tmpMapping
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_chungtu_chitiet_bhxh_cach3]    Script Date: 8/30/2024 4:32:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_tl_chungtu_chitiet_bhxh_cach3]
	@maDonVi VARCHAR(MAX),
	@thang INT,
	@nam INT,
	@maCachTl NVARCHAR(50)
AS
BEGIN

	DECLARE @checkTongHop INT; 
	SELECT @checkTongHop = count(*) FROM f_split(@maCachTl);

	SELECT Ma_Cot, pc.Ma_PhuCap into #tmp from (
		SELECT Ma_Cot, CONCAT('|', REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(CAST(CongThuc as nvarchar(max)),'+','|'),'-','|'),'*','|'),'/','|'),'(','|'),')','|'), '|') as CongThuc FROM TL_DM_Cach_TinhLuong_Chuan WHERE CongThuc IS NOT NULL) as tbl
		INNER  JOIN TL_DM_PhuCap as pc on tbl.CongThuc LIKE CONCAT('%|', pc.Ma_PhuCap, '|%') or pc.Parent in ('TIENAN', 'TIENAN2')

	select * into #tmpMapping
	FROM (
		select Ma_Cot, Ma_Cot as Ma_PhuCap from #tmp
		union 
		select Ma_PhuCap as Ma_Cot, Ma_PhuCap as Ma_PhuCap from #tmp
	) AS c

	SELECT mp.Ma_Cot, cb.Ma_CanBo, SUM(ISNULL(pc.fSoNgayHuongBHXH, 0)) as HuongPC_SN 
	INTO #tmpSoNgay
	FROM TL_DM_CanBo as cb
	INNER JOIN TL_CanBo_CheDoBHXH as pc on cb.Ma_CanBo = pc.sMaCanBo
	INNER JOIN #tmpMapping as mp on pc.sMaCheDo = mp.Ma_PhuCap
	WHERE cb.Parent in (SELECT * FROM f_split(@MaDonVi))
		AND Thang = @thang
		AND Nam = @nam
	GROUP BY mp.Ma_Cot, cb.Ma_CanBo;

	SELECT
		dsCapNhapBangLuong.Ma_CBo	AS MaDonVi,
		dsCapNhapBangLuong.Thang	AS Thang,
		dsCapNhapBangLuong.Nam		AS Nam,
		bangLuong.MA_PHUCAP			AS MaPhuCap,
		--bangLuong.Ma_CB				AS MaCapBac,
		canbo.Ma_CB MaCapBac,
		case when canbo.Ma_CB in ('3.1', '3.2', '3.3', '3.4', '3.5', '413', '415') then canbo.Ma_CB else capBac.Parent end AS Ngach,
		SUM(ISNULL(cbpc.HuongPC_SN, 0)) AS SoNgay,
		SUM(bangLuong.Gia_Tri) AS GiaTri,
		SUM(CASE WHEN @checkTongHop = 2 AND dsCapNhapBangLuong.Ma_CachTL ='CACH5' THEN 0 ELSE 1 END) AS SoNguoi
	INTO #LuongCapBac
	FROM (SELECT * FROM TL_BangLuong_Thang WHERE Gia_Tri != 0 AND Ma_PhuCap in ('LHT_TT', 'PCCV_TT', 'PCTN_TT', 'PCTNVK_TT', 'HSBL_TT')) bangLuong
	INNER JOIN (
		select * from TL_CanBo_CheDoBHXH 
		where inam = @nam 
			and ithang = @thang 
			and sMaCheDo in ('TAINANLD_DUONGSUCPHSK', 'THAISAN_DUONGSUCPHSK', 'OMDAU_DUONGSUCPHSK', 'OMKHAC_T14NGAY', 'BENHDAINGAY_T14NGAY','CONOM', 'KHAMTHAI', 'KHHGD', 'NAMNGHIKHIVOSINHCON')
		) as cbpc1 on bangLuong.Ma_Hieu_CanBo = SUBSTRING(cbpc1.sMaCanBo, 7, 2) and bangLuong.NAM = cbpc1.iNamCanCuDong AND bangLuong.THANG = cbpc1.iThangLuongCanCuDong
	LEFT JOIN #tmpSoNgay as cbpc on bangLuong.Ma_PhuCap = cbpc.Ma_Cot AND bangLuong.Ma_CBo = cbpc.Ma_CanBo
	JOIN (
		SELECT * FROM TL_DS_CapNhap_BangLuong 
		WHERE Status = 1 AND Ma_CBo IN (SELECT * FROM f_split(@MaDonVi)) 
		and isnull(STongHop, '') <> '' 
	) dsCapNhapBangLuong 
	ON bangLuong.parent = dsCapNhapBangLuong.Id AND bangLuong.THANG = dsCapNhapBangLuong.Thang and bangLuong.NAM = dsCapNhapBangLuong.Nam 
	LEFT JOIN (
		select Ma_Hieu_CanBo, Ma_CB from TL_DM_CanBo CanBo where Nam = @nam and Thang = @thang
	) canbo ON bangLuong.Ma_Hieu_CanBo = canbo.Ma_Hieu_CanBo
	LEFT JOIN TL_DM_CapBac capBac ON canbo.Ma_CB = capBac.Ma_Cb
	GROUP BY dsCapNhapBangLuong.Ma_CBo, dsCapNhapBangLuong.Thang, dsCapNhapBangLuong.Nam, bangLuong.MA_PHUCAP, canbo.Ma_CB, capBac.Parent
		
	SELECT distinct
		luongCapBac.MaDonVi,
		phuCapMlns.XauNoiMa,
		phuCapMlns.Ma_Cb,
		SoNguoi,
		SoNgay,
		GiaTri,
		luongCapBac.Thang
		INTO #LuongCapBacMlns
	FROM TL_PhuCap_MLNS phuCapMlns
	JOIN #LuongCapBac luongCapBac ON phuCapMlns.Ma_Cb = luongCapBac.Ngach AND phuCapMlns.Ma_PhuCap = luongCapBac.MaPhuCap
	WHERE
		phuCapMlns.Nam = @nam

	Select Ma_DonVi, XauNoiMa, SUM(DDuToan) DuToan
	INTO #DataDuToan
	From TL_QT_ChungTuChiTiet ctchitiet
	Join TL_QT_ChungTu chungtu
	on chungtu.ID = ctchitiet.Id_ChungTu
	Where Nam = @nam
	And Ma_DonVi IN (SELECT * FROM f_split(@maCachTl))
	Group By XauNoiMa, Ma_DonVi

	SELECT
		luong.MaDonVi					AS MaDonVi,
		NEWID()							AS Id,
		iID_MLNS						AS MlnsId, 
		iID_MLNS_Cha					AS MlnsIdParent, 
		luong.XauNoiMa					AS XauNoiMa, 
		sLNS							AS Lns, 
		sL								AS L, 
		sK								AS K, 
		sM								AS M, 
		sTM								AS TM, 
		sTTM							AS TTM, 
		sNG								AS Ng, 
		sTNG							AS TNG, 
		sTNG1							AS TNG1, 
		sTNG2							AS TNG2, 
		sTNG3							AS TNG3, 
		sMoTa							AS Mota, 
		iNamLamViec						AS NamLamViec,
		bHangCha						AS BHangCha,
		sChiTietToi						AS ChiTietToi,
		CONVERT(decimal, SUM(SoNguoi))	AS SoNguoi,
		SUM(ISNULL(SoNgay, 0))			AS SoNgay,
		SUM(GiaTri)						AS TongCong,
		SUM(GiaTri)						AS DieuChinh,
		@maCachTl						AS MaCachTl,
		ISNULL(dataDuToan.DuToan, 0)	AS DDuToan,
		luong.Ma_Cb						AS MaCb,
		luong.Thang as Thang
	FROM NS_MucLucNganSach mlns
	JOIN #LuongCapBacMlns luong ON mlns.sXauNoiMa = luong.XauNoiMa
	LEFT JOIN #DataDuToan dataDuToan ON luong.XauNoiMa = dataDuToan.XauNoiMa and luong.MaDonVi = dataDuToan.Ma_DonVi
	WHERE
		mlns.sLNS IN ('1', '101', '1010000')
		AND mlns.iNamLamViec = @nam
		--AND luong.Thang = @thangTruoc
	GROUP BY MaDonVi, iID_MLNS, iID_MLNS_Cha, luong.XauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, iNamLamViec, sChiTietToi, bHangCha, DuToan, luong.Ma_Cb, luong.Thang
	ORDER BY MaDonVi, luong.XauNoiMa

	DROP TABLE #LuongCapBac
	DROP TABLE #DataDuToan
	DROP TABLE #LuongCapBacMlns
	DROP TABLE #tmpSoNgay
	DROP TABLE #tmpMapping
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_chungtu_chitiet_bhxh_cach4]    Script Date: 8/30/2024 4:32:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_tl_chungtu_chitiet_bhxh_cach4]
	@maDonVi VARCHAR(MAX),
	@thang INT,
	@nam INT,
	@maCachTl NVARCHAR(50)
AS
BEGIN
	
	DECLARE @checkTongHop INT; 
	SELECT @checkTongHop = count(*) FROM f_split(@maCachTl);


	SELECT Ma_Cot, pc.Ma_PhuCap into #tmp from (
		SELECT Ma_Cot, CONCAT('|', REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(CAST(CongThuc as nvarchar(max)),'+','|'),'-','|'),'*','|'),'/','|'),'(','|'),')','|'), '|') as CongThuc FROM TL_DM_Cach_TinhLuong_Chuan WHERE CongThuc IS NOT NULL) as tbl
		INNER  JOIN TL_DM_PhuCap as pc on tbl.CongThuc LIKE CONCAT('%|', pc.Ma_PhuCap, '|%') or pc.Parent in ('TIENAN', 'TIENAN2')

	select * into #tmpMapping
	FROM (
		select Ma_Cot, Ma_Cot as Ma_PhuCap from #tmp
		union 
		select Ma_PhuCap as Ma_Cot, Ma_PhuCap as Ma_PhuCap from #tmp
	) AS c

	SELECT mp.Ma_Cot, cb.Ma_CanBo, SUM(ISNULL(pc.fSoNgayHuongBHXH, 0)) as HuongPC_SN INTO #tmpSoNgay
	FROM TL_DM_CanBo as cb
	INNER JOIN TL_CanBo_CheDoBHXH as pc on cb.Ma_CanBo = pc.sMaCanBo
	INNER JOIN #tmpMapping as mp on pc.sMaCheDo = mp.Ma_PhuCap
	WHERE cb.Parent in (SELECT * FROM f_split(@MaDonVi))
	AND Thang = @thang
	AND Nam = @nam
	GROUP BY mp.Ma_Cot, cb.Ma_CanBo;

	SELECT
		dsCapNhapBangLuong.Ma_CBo	AS MaDonVi,
		dsCapNhapBangLuong.Thang	AS Thang,
		dsCapNhapBangLuong.Nam		AS Nam,
		bangLuong.sMaCheDo			AS MaPhuCap,
		bangLuong.sMaCB				AS MaCapBac,
		case when bangLuong.sMaCB in ('3.1', '3.2', '3.3', '3.4', '3.5', '413', '415') then bangLuong.sMaCB else capBac.Parent end AS Ngach,
		SUM(ISNULL(cbpc.HuongPC_SN, 0)) AS SoNgay,
		SUM(bangLuong.nGiaTri) AS GiaTri,
		SUM(CASE WHEN @checkTongHop = 2 AND dsCapNhapBangLuong.Ma_CachTL ='CACH5' THEN 0 ELSE 1 END)	AS SoNguoi
	INTO #LuongCapBac
	FROM (SELECT * FROM TL_BangLuong_ThangBHXH WHERE iNam = @nam AND iThang = @thang AND nGiaTri != 0) bangLuong
	INNER JOIN (select * from TL_CanBo_CheDoBHXH where iNam = @nam and iThang = @thang
		and sMaCheDo in ('BDN_D14N_LBH_TT','BDN_D14N_PCCVBH_TT','BDN_D14N_PCTNBH_TT','BDN_D14N_PCTNVKBH_TT','BDN_D14N_HSBLBH_TT','OK_D14N_LBH_TT','OK_D14N_PCCVBH_TT','OK_D14N_PCTNBH_TT','OK_D14N_PCTNVKBH_TT','OK_D14N_HSBLBH_TT')) cbpc1 
	on bangLuong.sMaCBo = cbpc1.sMaCanBo AND bangLuong.sMaCheDo = cbpc1.sMaCheDo
	INNER JOIN TL_DM_CheDoBHXH as pc on bangLuong.sMaCheDo = pc.sMaCheDo
	LEFT JOIN #tmpSoNgay  as cbpc on bangLuong.sMaCheDo = cbpc.Ma_Cot AND bangLuong.sMaCBo = cbpc.Ma_CanBo
	JOIN 
	(SELECT * FROM TL_DS_CapNhap_BangLuong WHERE NAM = @Nam AND THANG = @Thang AND Ma_CBo IN (SELECT * FROM f_split(@MaDonVi)) and isnull(STongHop, '') <> '') dsCapNhapBangLuong 
	ON bangLuong.iID_Parent = dsCapNhapBangLuong.Id AND bangLuong.iThang = dsCapNhapBangLuong.Thang and bangLuong.iNam = dsCapNhapBangLuong.Nam 
	LEFT JOIN TL_DM_CapBac capBac ON bangLuong.sMaCB = capBac.Ma_Cb		
	GROUP BY dsCapNhapBangLuong.Ma_CBo, dsCapNhapBangLuong.Thang, dsCapNhapBangLuong.Nam, bangLuong.sMaCheDo,bangLuong.sMaCB, capBac.Parent
		
	SELECT distinct
		luongCapBac.MaDonVi,
		phuCapMlns.XauNoiMa,
		phuCapMlns.Ma_Cb,
		SoNguoi,
		SoNgay,
		GiaTri,
		luongCapBac.Thang
		INTO #LuongCapBacMlns
	FROM TL_PhuCap_MLNS phuCapMlns
	JOIN #LuongCapBac luongCapBac ON phuCapMlns.Ma_Cb = luongCapBac.Ngach AND phuCapMlns.Ma_PhuCap = luongCapBac.MaPhuCap
	WHERE
		phuCapMlns.Nam = @nam

	Select Ma_DonVi, XauNoiMa, SUM(DDuToan) DuToan
	INTO #DataDuToan
	From TL_QT_ChungTuChiTiet ctchitiet
	Join TL_QT_ChungTu chungtu
	on chungtu.ID = ctchitiet.Id_ChungTu
	Where Nam = @nam
	And Ma_DonVi IN (SELECT * FROM f_split(@maCachTl))
	Group By XauNoiMa, Ma_DonVi

	SELECT
		luong.MaDonVi					AS MaDonVi,
		NEWID()							AS Id,
		iID_MLNS						AS MlnsId, 
		iID_MLNS_Cha					AS MlnsIdParent, 
		sXauNoiMa						AS XauNoiMa, 
		sLNS							AS Lns, 
		sL								AS L, 
		sK								AS K, 
		sM								AS M, 
		sTM								AS TM, 
		sTTM							AS TTM, 
		sNG								AS Ng, 
		sTNG							AS TNG, 
		sTNG1							AS TNG1, 
		sTNG2							AS TNG2, 
		sTNG3							AS TNG3, 
		sMoTa							AS Mota, 
		iNamLamViec						AS NamLamViec,
		bHangCha						AS BHangCha,
		sChiTietToi						AS ChiTietToi,
		CONVERT(decimal, SUM(SoNguoi))	AS SoNguoi,
		SUM(ISNULL(SoNgay, 0))			AS SoNgay,
		SUM(GiaTri)						AS TongCong,
		SUM(GiaTri)						AS DieuChinh,
		@maCachTl						AS MaCachTl,
		ISNULL(dataDuToan.DuToan, 0)	AS DDuToan,
		luong.Ma_Cb						AS MaCb,
		luong.Thang as Thang
	FROM NS_MucLucNganSach mlns
	JOIN #LuongCapBacMlns luong
		ON mlns.sXauNoiMa = luong.XauNoiMa
	LEFT JOIN #DataDuToan dataDuToan
		ON luong.XauNoiMa = dataDuToan.XauNoiMa and luong.MaDonVi = dataDuToan.Ma_DonVi
	WHERE
		sLNS IN ('1', '101', '1010000')
		AND iNamLamViec = @nam
		AND luong.Thang = @thang
	GROUP BY MaDonVi, iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, iNamLamViec, sChiTietToi, bHangCha, DuToan, luong.Ma_Cb, luong.Thang
	ORDER BY MaDonVi, sXauNoiMa

	DROP TABLE #LuongCapBac
	DROP TABLE #DataDuToan
	DROP TABLE #LuongCapBacMlns
	DROP TABLE #tmpSoNgay
	DROP TABLE #tmpMapping
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_tru_bhxh]    Script Date: 9/4/2024 9:49:20 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_tru_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_tru_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_don_vi_tru_bhxh]    Script Date: 9/4/2024 9:49:20 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_don_vi_tru_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_don_vi_tru_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_don_vi]    Script Date: 9/4/2024 9:49:20 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_don_vi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_don_vi]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_tonghop_luong_theo_ngach]    Script Date: 9/4/2024 9:49:20 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_tonghop_luong_theo_ngach]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_tonghop_luong_theo_ngach]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_tonghop_luong_theo_don_vi_tru_bhxh]    Script Date: 9/4/2024 9:49:20 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_tonghop_luong_theo_don_vi_tru_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_tonghop_luong_theo_don_vi_tru_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_tonghop_luong_theo_don_vi]    Script Date: 9/4/2024 9:49:20 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_tonghop_luong_theo_don_vi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_tonghop_luong_theo_don_vi]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_tonghop_luong_theo_don_vi]    Script Date: 9/4/2024 9:49:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_tonghop_luong_theo_don_vi] @thang int, @nam int, @maDonVi nvarchar(MAX),
                                                                                           @donViTinh int, @maCachTl nvarchar(5) AS DECLARE @Cols AS NVARCHAR(MAX)
SET @Cols = 'LHT_TT,HSBL_TT,PCTNVK_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCCOV_TT,PCDACTHU_SUM,PCTRA_SUM,PCKHAC_SUM,LUONGTHANG_SUM,BHXHCN_TT,BHYTCN_TT,BHTNCN_TT,TA_TONG,THUETNCN_TT,TRICHLUONG_TT,GTKHAC_TT,PHAITRU_SUM,THANHTIEN';

WITH BangLuongThang AS
  (SELECT dsCapNhapBangLuong.Thang AS Thang,
          dsCapNhapBangLuong.Nam AS Nam,
          bangLuong.Ma_CBo AS MaCanBo,
          bangLuong.MA_PHUCAP AS MaPhuCap,
          bangLuong.GIA_TRI AS GiaTri
   FROM TL_BangLuong_Thang bangLuong
   JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong ON bangLuong.parent = dsCapNhapBangLuong.Id
   WHERE bangLuong.Ma_PhuCap IN
       (SELECT *
        FROM f_split(@Cols))
     AND dsCapNhapBangLuong.Ma_CachTL=@maCachTl
     AND dsCapNhapBangLuong.Ma_CBo IN
       (SELECT *
        FROM f_split(@maDonVi))
     AND dsCapNhapBangLuong.Thang=@thang
     AND dsCapNhapBangLuong.Nam=@nam
     AND dsCapNhapBangLuong.Status=1 ),

     ThongTinCanBo AS
  (SELECT canbo.Ma_CanBo MaCanBo,
          donvi.Ma_DonVi MaDonVi,
          donvi.Ten_DonVi DoiTuong,
		  canBo.TM TM
   FROM TL_DM_CanBo canBo
   LEFT JOIN TL_DM_DonVi donvi ON canBo.Parent = donvi.Ma_DonVi
   WHERE canBo.IsDelete = 1
     AND canBo.Khong_Luong = 0
	 AND canBo.Thang = @thang
	 AND canBo.Nam = @nam),

     CanBoLuongNgach AS
  (SELECT MaCanBo,
          DoiTuong,
          MaDonVi,
          LHT_TT,
          HSBL_TT,
          PCTNVK_TT,
          PCCV_TT,
          PCTN_TT,
          PCKV_TT,
          PCCOV_TT,
          PCDACTHU_SUM,
          PCTRA_SUM,
          PCKHAC_SUM,
          LUONGTHANG_SUM,
          BHXHCN_TT,
          BHYTCN_TT,
          BHTNCN_TT,
          TA_TONG,
          THUETNCN_TT,
          TRICHLUONG_TT,
          GTKHAC_TT,
          PHAITRU_SUM,
          THANHTIEN,
		  TM
   FROM
     (SELECT bangLuong.Thang AS Thang,
             bangLuong.Nam AS Nam,
             canBo.MaCanBo,
             canBo.MaDonVi,
             canBo.DoiTuong,
             bangLuong.GiaTri,
             bangLuong.MaPhuCap,
			 canBo.TM TM
      FROM BangLuongThang bangLuong
      INNER JOIN ThongTinCanBo canBo ON bangLuong.MaCanBo = canBo.MaCanBo) x PIVOT (SUM(GiaTri)
                                                                                    FOR MaPhuCap IN (LHT_TT, HSBL_TT, PCTNVK_TT, PCCV_TT, PCTN_TT, PCKV_TT, PCCOV_TT, PCDACTHU_SUM, PCTRA_SUM, PCKHAC_SUM, LUONGTHANG_SUM, BHXHCN_TT, BHYTCN_TT, BHTNCN_TT, TA_TONG, THUETNCN_TT, TRICHLUONG_TT, GTKHAC_TT, PHAITRU_SUM, THANHTIEN)) pvt)

SELECT DoiTuong,
       MaDonVi,
       COUNT(*) SoNguoi,
       SUM(LHT_TT)/@donViTinh LHT_TT,
       SUM(HSBL_TT)/@donViTinh HSBL_TT,
       SUM(PCTNVK_TT)/@donViTinh PCTNVK_TT,
       SUM(PCCV_TT)/@donViTinh PCCV_TT,
       SUM(PCTN_TT)/@donViTinh PCTN_TT,
       SUM(PCKV_TT)/@donViTinh PCKV_TT,
       SUM(PCCOV_TT)/@donViTinh PCCOV_TT,
       SUM(PCDACTHU_SUM)/@donViTinh PCDACTHU_SUM,
       SUM(PCTRA_SUM)/@donViTinh PCTRA_SUM,
       SUM(PCKHAC_SUM)/@donViTinh PCKHAC_SUM,
       SUM(LUONGTHANG_SUM)/@donViTinh LUONGTHANG_SUM,
       SUM(BHXHCN_TT)/@donViTinh BHXHCN_TT,
       SUM(BHYTCN_TT)/@donViTinh BHYTCN_TT,
       SUM(BHTNCN_TT)/@donViTinh BHTNCN_TT,
       SUM(TA_TONG)/@donViTinh TA_TONG,
       SUM(THUETNCN_TT)/@donViTinh THUETNCN_TT,
       SUM(TRICHLUONG_TT)/@donViTinh TRICHLUONG_TT,
       SUM(GTKHAC_TT)/@donViTinh GTKHAC_TT,
       SUM(PHAITRU_SUM)/@donViTinh PHAITRU_SUM,
       SUM(CASE WHEN TM = 1 THEN THANHTIEN ELSE 0 END)/@donViTinh THANHTIEN_NH,
       SUM(CASE WHEN TM = 0 THEN THANHTIEN ELSE 0 END)/@donViTinh THANHTIEN
FROM CanBoLuongNgach
GROUP BY DoiTuong,
         MaDonVi
ORDER BY MaDonVi

--UNION

--SELECT N'Tổng truy lĩnh' AS DoiTuong,
--       'x' AS MaDonVi,
--       COUNT(*) SoNguoi,
--       SUM(LHT_TT)/@donViTinh LHT_TT,
--       SUM(HSBL_TT)/@donViTinh HSBL_TT,
--       SUM(PCTNVK_TT)/@donViTinh PCTNVK_TT,
--       SUM(PCCV_TT)/@donViTinh PCCV_TT,
--       SUM(PCTN_TT)/@donViTinh PCTN_TT,
--       SUM(PCKV_TT)/@donViTinh PCKV_TT,
--       SUM(PCCOV_TT)/@donViTinh PCCOV_TT,
--       SUM(PCDACTHU_SUM)/@donViTinh PCDACTHU_SUM,
--       SUM(PCTRA_SUM)/@donViTinh PCTRA_SUM,
--       SUM(PCKHAC_SUM)/@donViTinh PCKHAC_SUM,
--       SUM(LUONGTHANG_SUM)/@donViTinh LUONGTHANG_SUM,
--       SUM(BHXHCN_TT)/@donViTinh BHXHCN_TT,
--       SUM(BHYTCN_TT)/@donViTinh BHYTCN_TT,
--       SUM(BHTNCN_TT)/@donViTinh BHTNCN_TT,
--       SUM(TA_TONG)/@donViTinh TA_TONG,
--       SUM(THUETNCN_TT)/@donViTinh THUETNCN_TT,
--       SUM(TRICHLUONG_TT)/@donViTinh TRICHLUONG_TT,
--       SUM(GTKHAC_TT)/@donViTinh GTKHAC_TT,
--       SUM(PHAITRU_SUM)/@donViTinh PHAITRU_SUM,
--       SUM(THANHTIEN)/@donViTinh THANHTIEN
--FROM CanBoLuongTruyLinh
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_tonghop_luong_theo_don_vi_tru_bhxh]    Script Date: 9/4/2024 9:49:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_tonghop_luong_theo_don_vi_tru_bhxh] 
	@thang int, 
	@nam int, 
	@maDonVi nvarchar(MAX),
	@donViTinh int, 
	@maCachTl nvarchar(5) 
AS 
	DECLARE @Cols AS NVARCHAR(MAX)
SET @Cols = 'LHT_TT,HSBL_TT,PCTNVK_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCCOV_TT,PCDACTHU_SUM,PCTRA_SUM,PCKHAC_SUM,LUONGTHANG_SUM,BHXHCN_TT,BHYTCN_TT,BHTNCN_TT,TA_TONG,THUETNCN_TT,TRICHLUONG_TT,GTKHAC_TT,PHAITRU_SUM,THANHTIEN';

WITH BangLuongBHXH AS (
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
	where LHT_TT.iThang = @Thang
		AND LHT_TT.iNam = @Nam
		AND LHT_TT.sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND LHT_TT.sMaCheDo in ('OK_D14N_LBH_TT', 'OK_T14N_LBH_TT', 'BDN_D14N_LBH_TT', 'BDN_T14N_LBH_TT', 'CONOM_LBH_TT', 'KT_LBH_TT', 'NAMNGHIVIEC_LBH_TT', 'KHHGD_LBH_TT')
	group by LHT_TT.sMaCbo, LHT_TT.sTenCbo, LHT_TT.sMaDonVi, LHT_TT.iNam, LHT_TT.iThang) LHT_TT on base.sMaCbo = LHT_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCCVBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = @Thang
		AND iNam = @Nam
		AND sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND sMaCheDo in ('OK_D14N_PCCVBH_TT', 'OK_T14N_PCCVBH_TT', 'BDN_D14N_PCCVBH_TT', 'BDN_T14N_PCCVBH_TT', 'CONOM_PCCVBH_TT', 'KT_PCCVBH_TT', 'NAMNGHIVIEC_PCCVBH_TT', 'KHHGD_PCCVBH_TT')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCCV_TT on base.sMaCbo = PCCV_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCTNBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = @Thang
		AND iNam = @Nam
		AND sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND sMaCheDo in ('OK_D14N_PCTNBH_TT', 'OK_T14N_PCTNBH_TT', 'BDN_D14N_PCTNBH_TT', 'BDN_T14N_PCTNBH_TT', 'CONOM_PCTNBH_TT', 'KT_PCTNBH_TT', 'NAMNGHIVIEC_PCTNBH_TT', 'KHHGD_PCTNBH_TT')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCTN_TT on base.sMaCbo = PCTN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_BHXHCN_TT
	from TL_BangLuong_ThangBHXH
	where iThang = @Thang
		AND iNam = @Nam
		AND sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND sMaCheDo in ('OK_D14N_BHXHCN_TT', 'OK_T14N_BHXHCN_TT', 'BDN_D14N_BHXHCN_TT', 'BDN_T14N_BHXHCN_TT', 'CONOM_BHXHCN_TT')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) BHXHCN_TT on base.sMaCbo = BHXHCN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_BHYTCN_TT
	from TL_BangLuong_ThangBHXH
	where iThang = @Thang
		AND iNam = @Nam
		AND sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND sMaCheDo in ('BDN_D14N_BHYTCN_TT', 'OK_D14N_BHYTCN_TT')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) BHYTCN_TT on base.sMaCbo = BHYTCN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCTNVKBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = @Thang
		AND iNam = @Nam
		AND sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND sMaCheDo in ('OK_D14N_PCTNVKBH_TT', 'OK_T14N_PCTNVKBH_TT', 'BDN_D14N_PCTNVKBH_TT', 'BDN_T14N_PCTNVKBH_TT', 'CONOM_PCTNVKBH_TT', 'KT_PCTNVKBH_TT', 'NAMNGHIVIEC_PCTNVKBH_TT', 'KHHGD_PCTNVKBH_TT')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCTNVK_TT on base.sMaCbo = PCTNVK_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_HSBLBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = @Thang
		AND iNam = @Nam
		AND sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND sMaCheDo in ('OK_D14N_HSBLBH_TT', 'OK_T14N_HSBLBH_TT', 'BDN_D14N_HSBLBH_TT', 'BDN_T14N_HSBLBH_TT', 'CONOM_HSBLBH_TT', 'KT_HSBLBH_TT', 'NAMNGHIVIEC_HSBLBH_TT', 'KHHGD_HSBLBH_TT')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) HSBL_TT on base.sMaCbo = HSBL_TT.sMaCbo

	where base.iThang = @Thang
		AND base.iNam = @Nam
		AND base.sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
),
BangLuongThang AS
  (SELECT dsCapNhapBangLuong.Thang AS Thang,
          dsCapNhapBangLuong.Nam AS Nam,
          bangLuong.Ma_CBo AS MaCanBo,
          bangLuong.MA_PHUCAP AS MaPhuCap,
		  dsCapNhapBangLuong.Ma_CBo MaDonVi,
          bangLuong.GIA_TRI AS GiaTri
   FROM TL_BangLuong_Thang bangLuong
   JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong ON bangLuong.parent = dsCapNhapBangLuong.Id
   WHERE bangLuong.Ma_PhuCap IN
       (SELECT *
        FROM f_split(@Cols))
     AND dsCapNhapBangLuong.Ma_CachTL=@maCachTl
     AND dsCapNhapBangLuong.Ma_CBo IN
       (SELECT *
        FROM f_split(@maDonVi))
     AND dsCapNhapBangLuong.Thang=@thang
     AND dsCapNhapBangLuong.Nam=@nam
     AND dsCapNhapBangLuong.Status=1 ),

     ThongTinCanBo AS
  (SELECT canbo.Ma_CanBo MaCanBo,
          donvi.Ma_DonVi MaDonVi,
          donvi.Ten_DonVi DoiTuong,
		  canBo.TM TM
   FROM TL_DM_CanBo canBo
   LEFT JOIN TL_DM_DonVi donvi ON canBo.Parent = donvi.Ma_DonVi
   WHERE canBo.IsDelete = 1
     AND canBo.Khong_Luong = 0
	 AND canBo.Thang = @thang
	 AND canBo.Nam = @nam),

     CanBoLuongNgach AS
  (SELECT MaCanBo,
          DoiTuong,
          MaDonVi,
          LHT_TT,
          HSBL_TT,
          PCTNVK_TT,
          PCCV_TT,
          PCTN_TT,
          PCKV_TT,
          PCCOV_TT,
          PCDACTHU_SUM,
          PCTRA_SUM,
          PCKHAC_SUM,
          LUONGTHANG_SUM,
          BHXHCN_TT,
          BHYTCN_TT,
          BHTNCN_TT,
          TA_TONG,
          THUETNCN_TT,
          TRICHLUONG_TT,
          GTKHAC_TT,
          PHAITRU_SUM,
          THANHTIEN,
		  TM
   FROM
     (SELECT bangLuong.Thang AS Thang,
             bangLuong.Nam AS Nam,
             canBo.MaCanBo,
             canBo.MaDonVi,
             canBo.DoiTuong,
			 canBo.TM TM,
              CASE
				WHEN bangLuong.MaPhuCap = 'LHT_TT' THEN isnull(bangLuong.GiaTri, 0) - isnull(bhxh.GiaTri_LHT_TT, 0)
				WHEN bangLuong.MaPhuCap = 'PCCV_TT' THEN isnull(bangLuong.GiaTri, 0) - isnull(bhxh.GiaTri_PCCVBH_TT, 0)
				WHEN bangLuong.MaPhuCap = 'PCTN_TT' THEN isnull(bangLuong.GiaTri, 0) - isnull(bhxh.GiaTri_PCTNBH_TT, 0)
				WHEN bangLuong.MaPhuCap = 'BHXHCN_TT' THEN isnull(bangLuong.GiaTri, 0) - isnull(bhxh.GiaTri_BHXHCN_TT, 0)
				WHEN bangLuong.MaPhuCap = 'BHYTCN_TT' THEN isnull(bangLuong.GiaTri, 0) - isnull(bhxh.GiaTri_BHYTCN_TT, 0)
				WHEN bangLuong.MaPhuCap = 'PCTNVK_TT' THEN isnull(bangLuong.GiaTri, 0) - isnull(bhxh.GiaTri_PCTNVKBH_TT, 0)
				WHEN bangLuong.MaPhuCap = 'HSBL_TT' THEN isnull(bangLuong.GiaTri, 0) - isnull(bhxh.GiaTri_HSBLBH_TT, 0)
				ELSE bangLuong.GiaTri
			END AS GiaTri,
             bangLuong.MaPhuCap
      FROM BangLuongThang bangLuong
      INNER JOIN ThongTinCanBo canBo ON bangLuong.MaCanBo = canBo.MaCanBo
	  LEFT JOIN BangLuongBHXH bhxh ON bangLuong.MaCanBo = bhxh.sMaCbo AND bangLuong.MaDonVi = bhxh.sMaDonVi
	  ) x PIVOT (SUM(GiaTri)
      FOR MaPhuCap IN (LHT_TT, HSBL_TT, PCTNVK_TT, PCCV_TT, PCTN_TT, PCKV_TT, PCCOV_TT, PCDACTHU_SUM, PCTRA_SUM, PCKHAC_SUM, LUONGTHANG_SUM, BHXHCN_TT, BHYTCN_TT, BHTNCN_TT, TA_TONG, THUETNCN_TT, TRICHLUONG_TT, GTKHAC_TT, PHAITRU_SUM, THANHTIEN)) pvt)

SELECT DoiTuong,
       MaDonVi,
       COUNT(*) SoNguoi,
       SUM(LHT_TT)/@donViTinh LHT_TT,
       SUM(HSBL_TT)/@donViTinh HSBL_TT,
       SUM(PCTNVK_TT)/@donViTinh PCTNVK_TT,
       SUM(PCCV_TT)/@donViTinh PCCV_TT,
       SUM(PCTN_TT)/@donViTinh PCTN_TT,
       SUM(PCKV_TT)/@donViTinh PCKV_TT,
       SUM(PCCOV_TT)/@donViTinh PCCOV_TT,
       SUM(PCDACTHU_SUM)/@donViTinh PCDACTHU_SUM,
       SUM(PCTRA_SUM)/@donViTinh PCTRA_SUM,
       SUM(PCKHAC_SUM)/@donViTinh PCKHAC_SUM,
       SUM(LUONGTHANG_SUM)/@donViTinh LUONGTHANG_SUM,
       SUM(BHXHCN_TT)/@donViTinh BHXHCN_TT,
       SUM(BHYTCN_TT)/@donViTinh BHYTCN_TT,
       SUM(BHTNCN_TT)/@donViTinh BHTNCN_TT,
       SUM(TA_TONG)/@donViTinh TA_TONG,
       SUM(THUETNCN_TT)/@donViTinh THUETNCN_TT,
       SUM(TRICHLUONG_TT)/@donViTinh TRICHLUONG_TT,
       SUM(GTKHAC_TT)/@donViTinh GTKHAC_TT,
       SUM(PHAITRU_SUM)/@donViTinh PHAITRU_SUM,
       SUM(CASE WHEN TM = 1 THEN THANHTIEN ELSE 0 END)/@donViTinh THANHTIEN_NH,
       SUM(CASE WHEN TM = 0 THEN THANHTIEN ELSE 0 END)/@donViTinh THANHTIEN
	   INTO #tbl_luong_bhxh
FROM CanBoLuongNgach
GROUP BY DoiTuong,
         MaDonVi
ORDER BY MaDonVi
	UPDATE #tbl_luong_bhxh SET 
	PCCOV_TT = (isnull(LHT_TT, 0) + isnull(PCCV_TT, 0) + isnull(PCTNVK_TT, 0) + isnull(HSBL_TT, 0)) --* isnull(PCCOV_HS, 0)

	UPDATE #tbl_luong_bhxh SET
	LUONGTHANG_SUM = isnull(LHT_TT, 0) + isnull(PCTN_TT, 0) + isnull(PCTNVK_TT, 0) + isnull(PCCV_TT, 0) + isnull(PCCOV_TT, 0) + isnull(PCTRA_SUM, 0) + isnull(PCDACTHU_SUM, 0) + isnull(PCKHAC_SUM, 0) + isnull(HSBL_TT, 0) + isnull(PCKV_TT, 0)-- + isnull(PCBVBG_TT, 0) + isnull(PCNU_TT, 0)

	UPDATE #tbl_luong_bhxh SET
	PHAITRU_SUM = isnull(BHXHCN_TT, 0) + isnull(BHYTCN_TT, 0) + isnull(BHTNCN_TT, 0) + isnull(THUETNCN_TT, 0) + isnull(TA_TONG, 0) + isnull(TRICHLUONG_TT, 0) + isnull(GTKHAC_TT, 0)

	--UPDATE #tbl_luong_bhxh SET
	--BHCN_TT = isnull(BHXHCN_TT, 0) + isnull(BHYTCN_TT, 0) + isnull(BHTNCN_TT, 0)

	UPDATE #tbl_luong_bhxh SET
	THANHTIEN = isnull(LUONGTHANG_SUM, 0) - isnull(PHAITRU_SUM, 0)

	select * from #tbl_luong_bhxh;

;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_tonghop_luong_theo_ngach]    Script Date: 9/4/2024 9:49:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_tonghop_luong_theo_ngach] @thang int, @nam int, @maDonVi nvarchar(MAX),
                                                                                          @donViTinh int, @isSummary bit, @maCachTl nvarchar(5) AS DECLARE @Cols AS NVARCHAR(MAX)
SET @Cols = 'LHT_TT,HSBL_TT,PCTNVK_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCCOV_TT,PCDACTHU_SUM,PCTRA_SUM,PCKHAC_SUM,LUONGTHANG_SUM,BHXHCN_TT,BHYTCN_TT,BHTNCN_TT,TA_TONG,THUETNCN_TT,TRICHLUONG_TT,GTKHAC_TT,PHAITRU_SUM,THANHTIEN';

WITH BangLuongThang AS
  (SELECT dsCapNhapBangLuong.Thang AS Thang,
          dsCapNhapBangLuong.Nam AS Nam,
          bangLuong.Ma_CBo AS MaCanBo,
          bangLuong.MA_PHUCAP AS MaPhuCap,
		  dsCapNhapBangLuong.Ma_CBo MaDonVi,
          bangLuong.GIA_TRI AS GiaTri
   FROM TL_BangLuong_Thang bangLuong
   JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong ON bangLuong.parent = dsCapNhapBangLuong.Id
   WHERE bangLuong.Ma_PhuCap IN
       (SELECT *
        FROM f_split(@Cols))
     AND dsCapNhapBangLuong.Ma_CachTL=@maCachTl
     AND dsCapNhapBangLuong.Ma_CBo IN
       (SELECT *
        FROM f_split(@maDonVi))
     AND dsCapNhapBangLuong.Thang=@thang
     AND dsCapNhapBangLuong.Nam=@nam
     AND dsCapNhapBangLuong.Status=1 ),

     ThongTinCanBo AS
  (SELECT canBo.Ma_CanBo AS MaCanBo,
          canBo.Ten_CanBo AS TenCanBo,
		  canBo.TM TM,
          ISNULL(canBo.Ma_CB, '0') AS MaCapBac,
          ISNULL(capBac.TenCapBac, '0') AS TenNgach,
          ISNULL(capBac.MaCapBacCha, '0') AS MaNgach
   FROM TL_DM_CanBo canBo
   INNER JOIN TL_DM_DonVi donVi ON canBo.Parent=donVi.Ma_DonVi AND canBo.Parent IN (SELECT * FROM f_split(@maDonVi))
   LEFT JOIN
     (SELECT capbaccon.Ma_Cb MaCapBac,
             capbaccha.Ma_Cb MaCapBacCha,
             capbaccha.Note TenCapBac
      FROM TL_DM_CapBac capbaccon
      LEFT JOIN TL_DM_CapBac capbaccha ON capbaccon.Parent = capbaccha.Ma_Cb) capBac ON canBo.Ma_CB=capBac.MaCapBac
   LEFT JOIN TL_DM_ChucVu chucVu ON canBo.Ma_CV=chucVu.Ma_Cv
   WHERE canBo.IsDelete = 1
     AND canBo.Khong_Luong = 0
	 AND canBo.Thang = @thang
	 AND canBo.Nam = @nam)

	 SELECT TenNgach,
          MaNgach,
		  MaDonVi,
          COUNT(MaCanBo) AS SoNguoi,
          SUM(LHT_TT)/@donViTinh LHT_TT,
       SUM(HSBL_TT)/@donViTinh HSBL_TT,
       SUM(PCTNVK_TT)/@donViTinh PCTNVK_TT,
       SUM(PCCV_TT)/@donViTinh PCCV_TT,
       SUM(PCTN_TT)/@donViTinh PCTN_TT,
       SUM(PCKV_TT)/@donViTinh PCKV_TT,
       SUM(PCCOV_TT)/@donViTinh PCCOV_TT,
       SUM(PCDACTHU_SUM)/@donViTinh PCDACTHU_SUM,
       SUM(PCTRA_SUM)/@donViTinh PCTRA_SUM,
       SUM(PCKHAC_SUM)/@donViTinh PCKHAC_SUM,
       SUM(LUONGTHANG_SUM)/@donViTinh LUONGTHANG_SUM,
       SUM(BHXHCN_TT)/@donViTinh BHXHCN_TT,
       SUM(BHYTCN_TT)/@donViTinh BHYTCN_TT,
       SUM(BHTNCN_TT)/@donViTinh BHTNCN_TT,
       SUM(TA_TONG)/@donViTinh TA_TONG,
       SUM(THUETNCN_TT)/@donViTinh THUETNCN_TT,
       SUM(TRICHLUONG_TT)/@donViTinh TRICHLUONG_TT,
       SUM(GTKHAC_TT)/@donViTinh GTKHAC_TT,
       SUM(PHAITRU_SUM)/@donViTinh PHAITRU_SUM,
	   SUM(CASE WHEN TM = 1 THEN THANHTIEN ELSE 0 END)/@donViTinh THANHTIEN_NH,
       SUM(CASE WHEN TM = 0 THEN THANHTIEN ELSE 0 END)/@donViTinh THANHTIEN
   FROM
     (SELECT bangLuong.Thang AS Thang,
             bangLuong.Nam AS Nam,
             canBo.MaCanBo,
             canBo.MaCapBac,
             canBo.TenNgach,
			 canBo.TM,
             canBo.MaNgach,
             bangLuong.GiaTri,
             bangLuong.MaPhuCap,
			 CASE When @isSummary = 1 Then '1' Else bangLuong.MaDonVi end as MaDonVi
      FROM BangLuongThang bangLuong
      INNER JOIN ThongTinCanBo canBo ON bangLuong.MaCanBo = canBo.MaCanBo) x PIVOT (SUM(GiaTri)
                                                                                    FOR MaPhuCap IN (LHT_TT, HSBL_TT, PCTNVK_TT, PCCV_TT, PCTN_TT, PCKV_TT, PCCOV_TT, PCDACTHU_SUM, PCTRA_SUM, PCKHAC_SUM, LUONGTHANG_SUM, BHXHCN_TT, BHYTCN_TT, BHTNCN_TT, TA_TONG, THUETNCN_TT, TRICHLUONG_TT, GTKHAC_TT, PHAITRU_SUM, THANHTIEN)) pvt
	GROUP BY TenNgach,
         MaNgach, MaDonVi
	Order By MaDonVi, MaNgach
	
--UNION
--	SELECT TenNgach,
--		MaNgach,
--		MaDonVi,
--		COUNT(MaCanBo) AS SoNguoi,
--		SUM(LHT_TT)/@donViTinh LHT_TT,
--		SUM(HSBL_TT)/@donViTinh HSBL_TT,
--       SUM(PCTNVK_TT)/@donViTinh PCTNVK_TT,
--       SUM(PCCV_TT)/@donViTinh PCCV_TT,
--       SUM(PCTN_TT)/@donViTinh PCTN_TT,
--       SUM(PCKV_TT)/@donViTinh PCKV_TT,
--       SUM(PCCOV_TT)/@donViTinh PCCOV_TT,
--       SUM(PCDACTHU_SUM)/@donViTinh PCDACTHU_SUM,
--       SUM(PCTRA_SUM)/@donViTinh PCTRA_SUM,
--       SUM(PCKHAC_SUM)/@donViTinh PCKHAC_SUM,
--       SUM(LUONGTHANG_SUM)/@donViTinh LUONGTHANG_SUM,
--       SUM(BHXHCN_TT)/@donViTinh BHXHCN_TT,
--       SUM(BHYTCN_TT)/@donViTinh BHYTCN_TT,
--       SUM(BHTNCN_TT)/@donViTinh BHTNCN_TT,
--       SUM(TA_TONG)/@donViTinh TA_TONG,
--       SUM(THUETNCN_TT)/@donViTinh THUETNCN_TT,
--       SUM(TRICHLUONG_TT)/@donViTinh TRICHLUONG_TT,
--       SUM(GTKHAC_TT)/@donViTinh GTKHAC_TT,
--       SUM(PHAITRU_SUM)/@donViTinh PHAITRU_SUM,
--       SUM(THANHTIEN)/@donViTinh THANHTIEN
--	FROM 
--	(SELECT 
--		N'Tổng truy lĩnh' AS TenNgach,
--		'x' AS MaNgach,
--		bl.GiaTri, bl.MaPhuCap, 
--		CASE When @isSummary = 1 Then '1' Else bl.MaDonVi end as MaDonVi,
--		bl.MaCanBo
--		FROM BangLuongTruyLinh bl INNER JOIN ThongTinCanBo
--			ON bl.MaCanBo = ThongTinCanBo.MaCanBo) x PIVOT (SUM(GiaTri)
--															FOR MaPhuCap IN (LHT_TT, HSBL_TT, PCTNVK_TT, PCCV_TT, PCTN_TT, PCKV_TT, PCCOV_TT, PCDACTHU_SUM, PCTRA_SUM, PCKHAC_SUM, LUONGTHANG_SUM, BHXHCN_TT, BHYTCN_TT, BHTNCN_TT, TA_TONG, THUETNCN_TT, TRICHLUONG_TT, GTKHAC_TT, PHAITRU_SUM, THANHTIEN)) pvt
--	GROUP BY MaDonVi, TenNgach, MaNgach
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_don_vi]    Script Date: 9/4/2024 9:49:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_don_vi] @thang int, @nam int, @maDonVi nvarchar(MAX),
                                                                                                 @donViTinh int, @maCachTl nvarchar(5) AS DECLARE @Cols AS NVARCHAR(MAX) DECLARE @IsParent AS Bit
SET @IsParent = 0;


SET @Cols = 'LHT_TT,HSBL_TT,PCTNVK_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCCOV_TT,PCDACTHU_SUM,PCTRA_SUM,PCKHAC_SUM,LUONGTHANG_SUM,BHXHCN_TT,BHYTCN_TT,BHTNCN_TT,TA_TONG,THUETNCN_TT,TRICHLUONG_TT,GTKHAC_TT,PHAITRU_SUM,THANHTIEN';

WITH BangLuongThang AS
  (SELECT dsCapNhapBangLuong.Thang AS Thang,
          dsCapNhapBangLuong.Nam AS Nam,
          bangLuong.Ma_CBo AS MaCanBo,
          bangLuong.MA_PHUCAP AS MaPhuCap,
          bangLuong.GIA_TRI AS GiaTri
   FROM TL_BangLuong_Thang bangLuong
   JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong ON bangLuong.parent = dsCapNhapBangLuong.Id --AND bangLuong.Ma_CB like @ngach + '%'

   WHERE bangLuong.Ma_PhuCap IN
       (SELECT *
        FROM f_split(@Cols))
     AND dsCapNhapBangLuong.Ma_CachTL=@maCachTl
     AND dsCapNhapBangLuong.Ma_CBo IN
       (SELECT *
        FROM f_split(@maDonVi))
     AND dsCapNhapBangLuong.Thang=@thang
     AND dsCapNhapBangLuong.Nam=@nam
     AND dsCapNhapBangLuong.Status=1 ),

     ThongTinCanBo AS
  (SELECT canbo.Ma_CanBo MaCanBo,
          donvi.Ma_DonVi MaDonVi,
		  canBo.TM TM,
          donvi.Ten_DonVi DoiTuong,
          case when capbac.Parent in('3.3','3.4') then '3' else capbac.Parent end as Ngach
   FROM TL_DM_CanBo canBo
   LEFT JOIN TL_DM_DonVi donvi ON canBo.Parent = donvi.Ma_DonVi
   LEFT JOIN TL_DM_CapBac capbac ON canBo.Ma_CB = capbac.Ma_Cb
   WHERE canBo.IsDelete = 1
     AND canBo.Khong_Luong = 0
	 AND canBo.Thang = @thang
	 AND canBo.Nam = @nam),
     CanBoLuongNgach AS
  (SELECT MaCanBo,
          Ngach,
          DoiTuong,
          MaDonVi,
          LHT_TT,
          HSBL_TT,
          PCTNVK_TT,
          PCCV_TT,
          PCTN_TT,
          PCKV_TT,
          PCCOV_TT,
          PCDACTHU_SUM,
          PCTRA_SUM,
          PCKHAC_SUM,
          LUONGTHANG_SUM,
          BHXHCN_TT,
          BHYTCN_TT,
          BHTNCN_TT,
          TA_TONG,
          THUETNCN_TT,
          TRICHLUONG_TT,
          GTKHAC_TT,
          PHAITRU_SUM,
          THANHTIEN,
		  TM
   FROM
     (SELECT bangLuong.Thang AS Thang,
             bangLuong.Nam AS Nam,
             canBo.MaCanBo,
             canBo.MaDonVi,
			 canBo.TM,
             canBo.DoiTuong,
             canBo.Ngach,
             bangLuong.GiaTri,
             bangLuong.MaPhuCap
      FROM BangLuongThang bangLuong
      INNER JOIN ThongTinCanBo canBo ON bangLuong.MaCanBo = canBo.MaCanBo) x PIVOT (SUM(GiaTri)
                                                                                    FOR MaPhuCap IN (LHT_TT, HSBL_TT, PCTNVK_TT, PCCV_TT, PCTN_TT, PCKV_TT, PCCOV_TT, PCDACTHU_SUM, PCTRA_SUM, PCKHAC_SUM, LUONGTHANG_SUM, BHXHCN_TT, BHYTCN_TT, BHTNCN_TT, TA_TONG, THUETNCN_TT, TRICHLUONG_TT, GTKHAC_TT, PHAITRU_SUM, THANHTIEN)) pvt)

SELECT DoiTuong,
       MaDonVi,
       Ngach,
       COUNT(*) SoNguoi,
       SUM(LHT_TT)/@donViTinh LHT_TT,
       SUM(HSBL_TT)/@donViTinh HSBL_TT,
       SUM(PCTNVK_TT)/@donViTinh PCTNVK_TT,
       SUM(PCCV_TT)/@donViTinh PCCV_TT,
       SUM(PCTN_TT)/@donViTinh PCTN_TT,
       SUM(PCKV_TT)/@donViTinh PCKV_TT,
       SUM(PCCOV_TT)/@donViTinh PCCOV_TT,
       SUM(PCDACTHU_SUM)/@donViTinh PCDACTHU_SUM,
       SUM(PCTRA_SUM)/@donViTinh PCTRA_SUM,
       SUM(PCKHAC_SUM)/@donViTinh PCKHAC_SUM,
       SUM(LUONGTHANG_SUM)/@donViTinh LUONGTHANG_SUM,
       SUM(BHXHCN_TT)/@donViTinh BHXHCN_TT,
       SUM(BHYTCN_TT)/@donViTinh BHYTCN_TT,
       SUM(BHTNCN_TT)/@donViTinh BHTNCN_TT,
       SUM(TA_TONG)/@donViTinh TA_TONG,
       SUM(THUETNCN_TT)/@donViTinh THUETNCN_TT,
       SUM(TRICHLUONG_TT)/@donViTinh TRICHLUONG_TT,
       SUM(GTKHAC_TT/@donViTinh) GTKHAC_TT,
       SUM(PHAITRU_SUM)/@donViTinh PHAITRU_SUM,
	   SUM(CASE WHEN TM = 1 THEN THANHTIEN ELSE 0 END)/@donViTinh THANHTIEN_NH,
       SUM(CASE WHEN TM = 0 THEN THANHTIEN ELSE 0 END)/@donViTinh THANHTIEN,
       @IsParent IsParent
FROM CanBoLuongNgach
GROUP BY DoiTuong,
         MaDonVi,
         Ngach
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_don_vi_tru_bhxh]    Script Date: 9/4/2024 9:49:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_don_vi_tru_bhxh] 
	@thang int, 
	@nam int, 
	@maDonVi nvarchar(MAX),
	@donViTinh int, 
	@maCachTl nvarchar(5) 
AS 
DECLARE 
@Cols AS NVARCHAR(MAX) DECLARE @IsParent AS Bit
SET @IsParent = 0;


SET @Cols = 'LHT_TT,HSBL_TT,PCTNVK_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCCOV_TT,PCDACTHU_SUM,PCTRA_SUM,PCKHAC_SUM,LUONGTHANG_SUM,BHXHCN_TT,BHYTCN_TT,BHTNCN_TT,TA_TONG,THUETNCN_TT,TRICHLUONG_TT,GTKHAC_TT,PHAITRU_SUM,THANHTIEN';

WITH BangLuongBHXH AS (
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
	where LHT_TT.iThang = @Thang
		AND LHT_TT.iNam = @Nam
		AND LHT_TT.sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND LHT_TT.sMaCheDo in ('OK_D14N_LBH_TT', 'OK_T14N_LBH_TT', 'BDN_D14N_LBH_TT', 'BDN_T14N_LBH_TT', 'CONOM_LBH_TT', 'KT_LBH_TT', 'NAMNGHIVIEC_LBH_TT', 'KHHGD_LBH_TT')
	group by LHT_TT.sMaCbo, LHT_TT.sTenCbo, LHT_TT.sMaDonVi, LHT_TT.iNam, LHT_TT.iThang) LHT_TT on base.sMaCbo = LHT_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCCVBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = @Thang
		AND iNam = @Nam
		AND sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND sMaCheDo in ('OK_D14N_PCCVBH_TT', 'OK_T14N_PCCVBH_TT', 'BDN_D14N_PCCVBH_TT', 'BDN_T14N_PCCVBH_TT', 'CONOM_PCCVBH_TT', 'KT_PCCVBH_TT', 'NAMNGHIVIEC_PCCVBH_TT', 'KHHGD_PCCVBH_TT')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCCV_TT on base.sMaCbo = PCCV_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCTNBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = @Thang
		AND iNam = @Nam
		AND sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND sMaCheDo in ('OK_D14N_PCTNBH_TT', 'OK_T14N_PCTNBH_TT', 'BDN_D14N_PCTNBH_TT', 'BDN_T14N_PCTNBH_TT', 'CONOM_PCTNBH_TT', 'KT_PCTNBH_TT', 'NAMNGHIVIEC_PCTNBH_TT', 'KHHGD_PCTNBH_TT')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCTN_TT on base.sMaCbo = PCTN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_BHXHCN_TT
	from TL_BangLuong_ThangBHXH
	where iThang = @Thang
		AND iNam = @Nam
		AND sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND sMaCheDo in ('OK_D14N_BHXHCN_TT', 'OK_T14N_BHXHCN_TT', 'BDN_D14N_BHXHCN_TT', 'BDN_T14N_BHXHCN_TT', 'CONOM_BHXHCN_TT')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) BHXHCN_TT on base.sMaCbo = BHXHCN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_BHYTCN_TT
	from TL_BangLuong_ThangBHXH
	where iThang = @Thang
		AND iNam = @Nam
		AND sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND sMaCheDo in ('BDN_D14N_BHYTCN_TT', 'OK_D14N_BHYTCN_TT')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) BHYTCN_TT on base.sMaCbo = BHYTCN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCTNVKBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = @Thang
		AND iNam = @Nam
		AND sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND sMaCheDo in ('OK_D14N_PCTNVKBH_TT', 'OK_T14N_PCTNVKBH_TT', 'BDN_D14N_PCTNVKBH_TT', 'BDN_T14N_PCTNVKBH_TT', 'CONOM_PCTNVKBH_TT', 'KT_PCTNVKBH_TT', 'NAMNGHIVIEC_PCTNVKBH_TT', 'KHHGD_PCTNVKBH_TT')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCTNVK_TT on base.sMaCbo = PCTNVK_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_HSBLBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = @Thang
		AND iNam = @Nam
		AND sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND sMaCheDo in ('OK_D14N_HSBLBH_TT', 'OK_T14N_HSBLBH_TT', 'BDN_D14N_HSBLBH_TT', 'BDN_T14N_HSBLBH_TT', 'CONOM_HSBLBH_TT', 'KT_HSBLBH_TT', 'NAMNGHIVIEC_HSBLBH_TT', 'KHHGD_HSBLBH_TT')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) HSBL_TT on base.sMaCbo = HSBL_TT.sMaCbo

	where base.iThang = @Thang
		AND base.iNam = @Nam
		AND base.sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
),
BangLuongThang AS
  (SELECT dsCapNhapBangLuong.Thang AS Thang,
          dsCapNhapBangLuong.Nam AS Nam,
          bangLuong.Ma_CBo AS MaCanBo,
          bangLuong.MA_PHUCAP AS MaPhuCap,
		  dsCapNhapBangLuong.Ma_CBo MaDonVi,
          bangLuong.GIA_TRI AS GiaTri
   FROM TL_BangLuong_Thang bangLuong
   JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong ON bangLuong.parent = dsCapNhapBangLuong.Id --AND bangLuong.Ma_CB like @ngach + '%'

   WHERE bangLuong.Ma_PhuCap IN
       (SELECT *
        FROM f_split(@Cols))
     AND dsCapNhapBangLuong.Ma_CachTL=@maCachTl
     AND dsCapNhapBangLuong.Ma_CBo IN
       (SELECT *
        FROM f_split(@maDonVi))
     AND dsCapNhapBangLuong.Thang=@thang
     AND dsCapNhapBangLuong.Nam=@nam
     AND dsCapNhapBangLuong.Status=1 ),

     ThongTinCanBo AS
  (SELECT canbo.Ma_CanBo MaCanBo,
          donvi.Ma_DonVi MaDonVi,
          donvi.Ten_DonVi DoiTuong,
		  canBo.TM TM,
          case when capbac.Parent in ('3.3','3.4') then '3' else capbac.Parent end as Ngach
   FROM TL_DM_CanBo canBo
   LEFT JOIN TL_DM_DonVi donvi ON canBo.Parent = donvi.Ma_DonVi
   LEFT JOIN TL_DM_CapBac capbac ON canBo.Ma_CB = capbac.Ma_Cb
   WHERE canBo.IsDelete = 1
     AND canBo.Khong_Luong = 0
	 AND canBo.Thang = @thang
	 AND canBo.Nam = @nam),
     CanBoLuongNgach AS
  (SELECT MaCanBo,
          Ngach,
          DoiTuong,
          MaDonVi,
          LHT_TT,
          HSBL_TT,
          PCTNVK_TT,
          PCCV_TT,
          PCTN_TT,
          PCKV_TT,
          PCCOV_TT,
          PCDACTHU_SUM,
          PCTRA_SUM,
          PCKHAC_SUM,
          LUONGTHANG_SUM,
          BHXHCN_TT,
          BHYTCN_TT,
          BHTNCN_TT,
          TA_TONG,
          THUETNCN_TT,
          TRICHLUONG_TT,
          GTKHAC_TT,
          PHAITRU_SUM,
          THANHTIEN,
		  TM
   FROM
     (SELECT bangLuong.Thang AS Thang,
             bangLuong.Nam AS Nam,
             canBo.MaCanBo,
             canBo.MaDonVi,
			 canBo.TM,
             canBo.DoiTuong,
             canBo.Ngach,
             CASE
				WHEN bangLuong.MaPhuCap = 'LHT_TT' THEN isnull(bangLuong.GiaTri, 0) - isnull(bhxh.GiaTri_LHT_TT, 0)
				WHEN bangLuong.MaPhuCap = 'PCCV_TT' THEN isnull(bangLuong.GiaTri, 0) - isnull(bhxh.GiaTri_PCCVBH_TT, 0)
				WHEN bangLuong.MaPhuCap = 'PCTN_TT' THEN isnull(bangLuong.GiaTri, 0) - isnull(bhxh.GiaTri_PCTNBH_TT, 0)
				WHEN bangLuong.MaPhuCap = 'BHXHCN_TT' THEN isnull(bangLuong.GiaTri, 0) - isnull(bhxh.GiaTri_BHXHCN_TT, 0)
				WHEN bangLuong.MaPhuCap = 'BHYTCN_TT' THEN isnull(bangLuong.GiaTri, 0) - isnull(bhxh.GiaTri_BHYTCN_TT, 0)
				WHEN bangLuong.MaPhuCap = 'PCTNVK_TT' THEN isnull(bangLuong.GiaTri, 0) - isnull(bhxh.GiaTri_PCTNVKBH_TT, 0)
				WHEN bangLuong.MaPhuCap = 'HSBL_TT' THEN isnull(bangLuong.GiaTri, 0) - isnull(bhxh.GiaTri_HSBLBH_TT, 0)
				ELSE bangLuong.GiaTri
			END AS GiaTri,
             bangLuong.MaPhuCap
      FROM BangLuongThang bangLuong
      INNER JOIN ThongTinCanBo canBo ON bangLuong.MaCanBo = canBo.MaCanBo
	  LEFT JOIN BangLuongBHXH bhxh ON bangLuong.MaCanBo = bhxh.sMaCbo AND bangLuong.MaDonVi = bhxh.sMaDonVi
	  ) x PIVOT (SUM(GiaTri)
      FOR MaPhuCap IN (LHT_TT, HSBL_TT, PCTNVK_TT, PCCV_TT, PCTN_TT, PCKV_TT, PCCOV_TT, PCDACTHU_SUM, PCTRA_SUM, PCKHAC_SUM, LUONGTHANG_SUM, BHXHCN_TT, BHYTCN_TT, BHTNCN_TT, TA_TONG, THUETNCN_TT, TRICHLUONG_TT, GTKHAC_TT, PHAITRU_SUM, THANHTIEN)) pvt)

SELECT DoiTuong,
       MaDonVi,
       Ngach,
       COUNT(*) SoNguoi,
       SUM(LHT_TT)/@donViTinh LHT_TT,
       SUM(HSBL_TT)/@donViTinh HSBL_TT,
       SUM(PCTNVK_TT)/@donViTinh PCTNVK_TT,
       SUM(PCCV_TT)/@donViTinh PCCV_TT,
       SUM(PCTN_TT)/@donViTinh PCTN_TT,
       SUM(PCKV_TT)/@donViTinh PCKV_TT,
       SUM(PCCOV_TT)/@donViTinh PCCOV_TT,
       SUM(PCDACTHU_SUM)/@donViTinh PCDACTHU_SUM,
       SUM(PCTRA_SUM)/@donViTinh PCTRA_SUM,
       SUM(PCKHAC_SUM)/@donViTinh PCKHAC_SUM,
       SUM(LUONGTHANG_SUM)/@donViTinh LUONGTHANG_SUM,
       SUM(BHXHCN_TT)/@donViTinh BHXHCN_TT,
       SUM(BHYTCN_TT)/@donViTinh BHYTCN_TT,
       SUM(BHTNCN_TT)/@donViTinh BHTNCN_TT,
       SUM(TA_TONG)/@donViTinh TA_TONG,
       SUM(THUETNCN_TT)/@donViTinh THUETNCN_TT,
       SUM(TRICHLUONG_TT)/@donViTinh TRICHLUONG_TT,
       SUM(GTKHAC_TT/@donViTinh) GTKHAC_TT,
       SUM(PHAITRU_SUM)/@donViTinh PHAITRU_SUM,
	   SUM(CASE WHEN TM = 1 THEN THANHTIEN ELSE 0 END)/@donViTinh THANHTIEN_NH,
       SUM(CASE WHEN TM = 0 THEN THANHTIEN ELSE 0 END)/@donViTinh THANHTIEN,
       @IsParent IsParent
	   INTO #tbl_luong_bhxh
	FROM CanBoLuongNgach
	GROUP BY DoiTuong,
			MaDonVi,
			Ngach


	UPDATE #tbl_luong_bhxh SET 
	PCCOV_TT = (isnull(LHT_TT, 0) + isnull(PCCV_TT, 0) + isnull(PCTNVK_TT, 0) + isnull(HSBL_TT, 0)) --* isnull(PCCOV_HS, 0)

	UPDATE #tbl_luong_bhxh SET
	LUONGTHANG_SUM = isnull(LHT_TT, 0) + isnull(PCTN_TT, 0) + isnull(PCTNVK_TT, 0) + isnull(PCCV_TT, 0) + isnull(PCCOV_TT, 0) + isnull(PCTRA_SUM, 0) + isnull(PCDACTHU_SUM, 0) + isnull(PCKHAC_SUM, 0) + isnull(HSBL_TT, 0) + isnull(PCKV_TT, 0)-- + isnull(PCBVBG_TT, 0) + isnull(PCNU_TT, 0)

	UPDATE #tbl_luong_bhxh SET
	PHAITRU_SUM = isnull(BHXHCN_TT, 0) + isnull(BHYTCN_TT, 0) + isnull(BHTNCN_TT, 0) + isnull(THUETNCN_TT, 0) + isnull(TA_TONG, 0) + isnull(TRICHLUONG_TT, 0) + isnull(GTKHAC_TT, 0)

	--UPDATE #tbl_luong_bhxh SET
	--BHCN_TT = isnull(BHXHCN_TT, 0) + isnull(BHYTCN_TT, 0) + isnull(BHTNCN_TT, 0)

	UPDATE #tbl_luong_bhxh SET
	THANHTIEN = isnull(LUONGTHANG_SUM, 0) - isnull(PHAITRU_SUM, 0)

	select * from #tbl_luong_bhxh;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_tru_bhxh]    Script Date: 9/4/2024 9:49:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_tru_bhxh] 
	@thang int, 
	@nam int, 
	@maDonVi nvarchar(MAX),
	@donViTinh int, 
	@isSummary bit, 
	@maCachTl nvarchar(5)
AS 

DECLARE @Cols AS NVARCHAR(MAX)
SET @Cols = 'LHT_TT,HSBL_TT,PCTNVK_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCCOV_TT,PCDACTHU_SUM,PCTRA_SUM,PCKHAC_SUM,LUONGTHANG_SUM,BHXHCN_TT,BHYTCN_TT,BHTNCN_TT,TA_TONG,THUETNCN_TT,TRICHLUONG_TT,GTKHAC_TT,PHAITRU_SUM,THANHTIEN';

WITH BangLuongBHXH AS (
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
	where LHT_TT.iThang = @Thang
		AND LHT_TT.iNam = @Nam
		AND LHT_TT.sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND LHT_TT.sMaCheDo in ('OK_D14N_LBH_TT', 'OK_T14N_LBH_TT', 'BDN_D14N_LBH_TT', 'BDN_T14N_LBH_TT', 'CONOM_LBH_TT', 'KT_LBH_TT', 'NAMNGHIVIEC_LBH_TT', 'KHHGD_LBH_TT')
	group by LHT_TT.sMaCbo, LHT_TT.sTenCbo, LHT_TT.sMaDonVi, LHT_TT.iNam, LHT_TT.iThang) LHT_TT on base.sMaCbo = LHT_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCCVBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = @Thang
		AND iNam = @Nam
		AND sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND sMaCheDo in ('OK_D14N_PCCVBH_TT', 'OK_T14N_PCCVBH_TT', 'BDN_D14N_PCCVBH_TT', 'BDN_T14N_PCCVBH_TT', 'CONOM_PCCVBH_TT', 'KT_PCCVBH_TT', 'NAMNGHIVIEC_PCCVBH_TT', 'KHHGD_PCCVBH_TT')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCCV_TT on base.sMaCbo = PCCV_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCTNBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = @Thang
		AND iNam = @Nam
		AND sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND sMaCheDo in ('OK_D14N_PCTNBH_TT', 'OK_T14N_PCTNBH_TT', 'BDN_D14N_PCTNBH_TT', 'BDN_T14N_PCTNBH_TT', 'CONOM_PCTNBH_TT', 'KT_PCTNBH_TT', 'NAMNGHIVIEC_PCTNBH_TT', 'KHHGD_PCTNBH_TT')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCTN_TT on base.sMaCbo = PCTN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_BHXHCN_TT
	from TL_BangLuong_ThangBHXH
	where iThang = @Thang
		AND iNam = @Nam
		AND sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND sMaCheDo in ('OK_D14N_BHXHCN_TT', 'OK_T14N_BHXHCN_TT', 'BDN_D14N_BHXHCN_TT', 'BDN_T14N_BHXHCN_TT', 'CONOM_BHXHCN_TT')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) BHXHCN_TT on base.sMaCbo = BHXHCN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_BHYTCN_TT
	from TL_BangLuong_ThangBHXH
	where iThang = @Thang
		AND iNam = @Nam
		AND sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND sMaCheDo in ('BDN_D14N_BHYTCN_TT', 'OK_D14N_BHYTCN_TT')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) BHYTCN_TT on base.sMaCbo = BHYTCN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCTNVKBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = @Thang
		AND iNam = @Nam
		AND sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND sMaCheDo in ('OK_D14N_PCTNVKBH_TT', 'OK_T14N_PCTNVKBH_TT', 'BDN_D14N_PCTNVKBH_TT', 'BDN_T14N_PCTNVKBH_TT', 'CONOM_PCTNVKBH_TT', 'KT_PCTNVKBH_TT', 'NAMNGHIVIEC_PCTNVKBH_TT', 'KHHGD_PCTNVKBH_TT')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCTNVK_TT on base.sMaCbo = PCTNVK_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_HSBLBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = @Thang
		AND iNam = @Nam
		AND sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND sMaCheDo in ('OK_D14N_HSBLBH_TT', 'OK_T14N_HSBLBH_TT', 'BDN_D14N_HSBLBH_TT', 'BDN_T14N_HSBLBH_TT', 'CONOM_HSBLBH_TT', 'KT_HSBLBH_TT', 'NAMNGHIVIEC_HSBLBH_TT', 'KHHGD_HSBLBH_TT')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) HSBL_TT on base.sMaCbo = HSBL_TT.sMaCbo

	where base.iThang = @Thang
		AND base.iNam = @Nam
		AND base.sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
),
BangLuongThang AS
  (SELECT dsCapNhapBangLuong.Thang AS Thang,
          dsCapNhapBangLuong.Nam AS Nam,
          bangLuong.Ma_CBo AS MaCanBo,
          bangLuong.MA_PHUCAP AS MaPhuCap,
		  dsCapNhapBangLuong.Ma_CBo MaDonVi,
          bangLuong.GIA_TRI AS GiaTri
   FROM TL_BangLuong_Thang bangLuong
   JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong ON bangLuong.parent = dsCapNhapBangLuong.Id
   WHERE bangLuong.Ma_PhuCap IN
       (SELECT *
        FROM f_split(@Cols))
     AND dsCapNhapBangLuong.Ma_CachTL=@maCachTl
     AND dsCapNhapBangLuong.Ma_CBo IN
       (SELECT *
        FROM f_split(@maDonVi))
     AND dsCapNhapBangLuong.Thang=@thang
     AND dsCapNhapBangLuong.Nam=@nam
     AND dsCapNhapBangLuong.Status=1 ),

     ThongTinCanBo AS
  (SELECT canBo.Ma_CanBo AS MaCanBo,
          canBo.Ten_CanBo AS TenCanBo,
		  canBo.TM TM,
          ISNULL(canBo.Ma_CB, '0') AS MaCapBac,
          ISNULL(capBac.TenCapBac, '0') AS TenNgach,
          ISNULL(capBac.MaCapBacCha, '0') AS MaNgach
   FROM TL_DM_CanBo canBo
   INNER JOIN TL_DM_DonVi donVi ON canBo.Parent=donVi.Ma_DonVi AND canBo.Parent IN (SELECT * FROM f_split(@maDonVi))
   LEFT JOIN
     (SELECT capbaccon.Ma_Cb MaCapBac,
             capbaccha.Ma_Cb MaCapBacCha,
             capbaccha.Note TenCapBac
      FROM TL_DM_CapBac capbaccon
      LEFT JOIN TL_DM_CapBac capbaccha ON capbaccon.Parent = capbaccha.Ma_Cb) capBac ON canBo.Ma_CB=capBac.MaCapBac
   LEFT JOIN TL_DM_ChucVu chucVu ON canBo.Ma_CV=chucVu.Ma_Cv
   WHERE canBo.IsDelete = 1
     AND canBo.Khong_Luong = 0
	 AND canBo.Thang = @thang
	 AND canBo.Nam = @nam)

	 SELECT TenNgach,
          MaNgach,
		  MaDonVi,
          COUNT(MaCanBo) AS SoNguoi,
          SUM(LHT_TT)/@donViTinh LHT_TT,
		   SUM(HSBL_TT)/@donViTinh HSBL_TT,
		   SUM(PCTNVK_TT)/@donViTinh PCTNVK_TT,
		   SUM(PCCV_TT)/@donViTinh PCCV_TT,
		   SUM(PCTN_TT)/@donViTinh PCTN_TT,
		   SUM(PCKV_TT)/@donViTinh PCKV_TT,
		   SUM(PCCOV_TT)/@donViTinh PCCOV_TT,
		   SUM(PCDACTHU_SUM)/@donViTinh PCDACTHU_SUM,
		   SUM(PCTRA_SUM)/@donViTinh PCTRA_SUM,
		   SUM(PCKHAC_SUM)/@donViTinh PCKHAC_SUM,
		   SUM(LUONGTHANG_SUM)/@donViTinh LUONGTHANG_SUM,
		   SUM(BHXHCN_TT)/@donViTinh BHXHCN_TT,
		   SUM(BHYTCN_TT)/@donViTinh BHYTCN_TT,
		   SUM(BHTNCN_TT)/@donViTinh BHTNCN_TT,
		   SUM(TA_TONG)/@donViTinh TA_TONG,
		   SUM(THUETNCN_TT)/@donViTinh THUETNCN_TT,
		   SUM(TRICHLUONG_TT)/@donViTinh TRICHLUONG_TT,
		   SUM(GTKHAC_TT)/@donViTinh GTKHAC_TT,
		   SUM(PHAITRU_SUM)/@donViTinh PHAITRU_SUM,
		   SUM(CASE WHEN TM = 1 THEN THANHTIEN ELSE 0 END)/@donViTinh THANHTIEN_NH,
           SUM(CASE WHEN TM = 0 THEN THANHTIEN ELSE 0 END)/@donViTinh THANHTIEN
	INTO #tbl_luong_bhxh
	FROM
     (SELECT bangLuong.Thang AS Thang,
             bangLuong.Nam AS Nam,
             canBo.MaCanBo,
             canBo.MaCapBac,
			 canBo.TM,
             canBo.TenNgach,
             canBo.MaNgach,
             CASE
				WHEN bangLuong.MaPhuCap = 'LHT_TT' THEN isnull(bangLuong.GiaTri, 0) - isnull(bhxh.GiaTri_LHT_TT, 0)
				WHEN bangLuong.MaPhuCap = 'PCCV_TT' THEN isnull(bangLuong.GiaTri, 0) - isnull(bhxh.GiaTri_PCCVBH_TT, 0)
				WHEN bangLuong.MaPhuCap = 'PCTN_TT' THEN isnull(bangLuong.GiaTri, 0) - isnull(bhxh.GiaTri_PCTNBH_TT, 0)
				WHEN bangLuong.MaPhuCap = 'BHXHCN_TT' THEN isnull(bangLuong.GiaTri, 0) - isnull(bhxh.GiaTri_BHXHCN_TT, 0)
				WHEN bangLuong.MaPhuCap = 'BHYTCN_TT' THEN isnull(bangLuong.GiaTri, 0) - isnull(bhxh.GiaTri_BHYTCN_TT, 0)
				WHEN bangLuong.MaPhuCap = 'PCTNVK_TT' THEN isnull(bangLuong.GiaTri, 0) - isnull(bhxh.GiaTri_PCTNVKBH_TT, 0)
				WHEN bangLuong.MaPhuCap = 'HSBL_TT' THEN isnull(bangLuong.GiaTri, 0) - isnull(bhxh.GiaTri_HSBLBH_TT, 0)
				ELSE bangLuong.GiaTri
			END AS GiaTri,
             bangLuong.MaPhuCap,
			 CASE When @isSummary = 1 Then '1' Else bangLuong.MaDonVi end as MaDonVi
      FROM BangLuongThang bangLuong
      INNER JOIN ThongTinCanBo canBo ON bangLuong.MaCanBo = canBo.MaCanBo
	  LEFT JOIN BangLuongBHXH bhxh ON bangLuong.MaCanBo = bhxh.sMaCbo AND bangLuong.MaDonVi = bhxh.sMaDonVi) 
	  x PIVOT (SUM(GiaTri)
      FOR MaPhuCap IN (LHT_TT, HSBL_TT, PCTNVK_TT, PCCV_TT, PCTN_TT, PCKV_TT, PCCOV_TT, PCDACTHU_SUM, PCTRA_SUM, PCKHAC_SUM, LUONGTHANG_SUM, BHXHCN_TT, BHYTCN_TT, BHTNCN_TT, TA_TONG, THUETNCN_TT, TRICHLUONG_TT, GTKHAC_TT, PHAITRU_SUM, THANHTIEN)) pvt
	GROUP BY TenNgach,
         MaNgach, MaDonVi
	Order By MaDonVi, MaNgach

	UPDATE #tbl_luong_bhxh SET 
	PCCOV_TT = (isnull(LHT_TT, 0) + isnull(PCCV_TT, 0) + isnull(PCTNVK_TT, 0) + isnull(HSBL_TT, 0)) --* isnull(PCCOV_HS, 0)

	UPDATE #tbl_luong_bhxh SET
	LUONGTHANG_SUM = isnull(LHT_TT, 0) + isnull(PCTN_TT, 0) + isnull(PCTNVK_TT, 0) + isnull(PCCV_TT, 0) + isnull(PCCOV_TT, 0) + isnull(PCTRA_SUM, 0) + isnull(PCDACTHU_SUM, 0) + isnull(PCKHAC_SUM, 0) + isnull(HSBL_TT, 0) + isnull(PCKV_TT, 0)-- + isnull(PCBVBG_TT, 0) + isnull(PCNU_TT, 0)

	UPDATE #tbl_luong_bhxh SET
	PHAITRU_SUM = isnull(BHXHCN_TT, 0) + isnull(BHYTCN_TT, 0) + isnull(BHTNCN_TT, 0) + isnull(THUETNCN_TT, 0) + isnull(TA_TONG, 0) + isnull(TRICHLUONG_TT, 0) + isnull(GTKHAC_TT, 0)

	--UPDATE #tbl_luong_bhxh SET
	--BHCN_TT = isnull(BHXHCN_TT, 0) + isnull(BHYTCN_TT, 0) + isnull(BHTNCN_TT, 0)

	UPDATE #tbl_luong_bhxh SET
	THANHTIEN = isnull(LUONGTHANG_SUM, 0) - isnull(PHAITRU_SUM, 0)

	select * from #tbl_luong_bhxh;


;
;
;
;
GO
