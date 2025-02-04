delete from BH_DM_ThamDinhQuyetToan
where ima > 258
go

  /****** Object:  StoredProcedure [dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_ttbyt]    Script Date: 02/02/2024 4:04:35 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_ttbyt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_ttbyt]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_kcb_quanydonvi]    Script Date: 02/02/2024 4:04:35 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_kcb_quanydonvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_kcb_quanydonvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_cssk_hssv_nld]    Script Date: 02/02/2024 4:04:35 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_cssk_hssv_nld]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_cssk_hssv_nld]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_chedo_bhxh]    Script Date: 02/02/2024 4:04:35 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_chedo_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_chedo_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_thamdinhquyettoan_chitiet]    Script Date: 02/02/2024 4:04:35 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_thamdinhquyettoan_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_thamdinhquyettoan_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_thamdinhquyettoan_chitiet]    Script Date: 02/02/2024 4:04:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_thamdinhquyettoan_chitiet]
@IdChungTu uniqueidentifier,
@INamLamViec int,
@IdDonVi nvarchar(max)
AS
BEGIN
	SELECT * INTO #dmtdqt
	FROM BH_DM_ThamDinhQuyetToan WHERE iNamLamViec = @INamLamViec AND iTrangThai = 1
	ORDER BY iMa

	SELECT 
		iID,
		ctct.iID_BH_TDQT_ChungTuChiTiet,
		dmtdqp.iMa,
		iMaCha,
		sSTT,
		sNoiDung,
		sXauNoiMa,
		iKieuChu,
		dmtdqp.iTrangThai,
		dmtdqp.iNamLamViec,
		dmtdqp.sNguoiTao, dmtdqp.sNguoiSua,
		ISNULL(ctct.fSoBaoCao, temp.fSoBaoCao) fSoBaoCao,
		ctct.fSoThamDinh,
		ctct.fQuanNhan,
		ctct.fCNVLDHD
	FROM #dmtdqt dmtdqp
	
	LEFT JOIN

	(
	SELECT 7 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 8 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 9 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 10 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 12 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 13 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 14 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 15 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 19 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 20 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 21 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec


	UNION

	SELECT 22 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 24 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 25 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec


	UNION

	SELECT 26 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 27 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec


	UNION

	SELECT 29 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0002'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 30 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0002'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec


	UNION

	SELECT 34 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))

	UNION

	SELECT 35 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec


	UNION

	SELECT 36 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 37 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec


	UNION

	SELECT 39 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 40 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 41 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 42 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 46 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 47 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec


	UNION

	SELECT 48 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 49 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 51 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 52 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 53 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 54 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec


	UNION

	SELECT 56 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0002'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 57 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0002'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 70 iMa, SUM(ctct.fThu_BHTN_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020001-010-011-0001-0000', '9020001-010-011-0001-0001', '9020001-010-011-0001-0002', '9020001-010-011-0002-0000', '9020001-010-011-0002-0001')
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 71 iMa, SUM(ctct.fTruyThu_BHTN_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020001-010-011-0001-0000', '9020001-010-011-0001-0001', '9020001-010-011-0001-0002', '9020001-010-011-0002-0000', '9020001-010-011-0002-0001')
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 73 iMa, SUM(ctct.fThu_BHTN_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020001-010-011-0001-0000', '9020001-010-011-0001-0001', '9020001-010-011-0001-0002', '9020001-010-011-0002-0000', '9020001-010-011-0002-0001')
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 74 iMa, SUM(ctct.fTruyThu_BHTN_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020001-010-011-0001-0000', '9020001-010-011-0001-0001', '9020001-010-011-0001-0002', '9020001-010-011-0002-0000', '9020001-010-011-0002-0001')
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 77 iMa, SUM(ctct.fThu_BHTN_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020002-010-011-0001-0000', '9020002-010-011-0001-0001', '9020002-010-011-0001-0002', '9020002-010-011-0002-0000', '9020002-010-011-0002-0001')
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 78 iMa, SUM(ctct.fTruyThu_BHTN_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020002-010-011-0001-0000', '9020002-010-011-0001-0001', '9020002-010-011-0001-0002', '9020002-010-011-0002-0000', '9020002-010-011-0002-0001')
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 80 iMa, SUM(ctct.fThu_BHTN_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020002-010-011-0001-0000', '9020002-010-011-0001-0001', '9020002-010-011-0001-0002', '9020002-010-011-0002-0000', '9020002-010-011-0002-0001')
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	UNION

	SELECT 81 iMa, SUM(ctct.fTruyThu_BHTN_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020002-010-011-0001-0000', '9020002-010-011-0001-0001', '9020002-010-011-0001-0002', '9020002-010-011-0002-0000', '9020002-010-011-0002-0001')
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	
	UNION

	SELECT 95 iMa, SUM(ctct.fThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 96 iMa, SUM(ctct.fThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 98 iMa, SUM(ctct.fTruyThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 99 iMa, SUM(ctct.fTruyThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec


	UNION

	SELECT 102 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 103 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 105 iMa, SUM(ctct.fTruyThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 106 iMa, SUM(ctct.fTruyThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 110 iMa, SUM(ctct.fThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 111 iMa, SUM(ctct.fThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 113 iMa, SUM(ctct.fTruyThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 114 iMa, SUM(ctct.fTruyThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec


	UNION

	SELECT 17 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 118 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 120 iMa, SUM(ctct.fTruyThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 121 iMa, SUM(ctct.fTruyThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	
	UNION

	SELECT 133 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 134 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 135 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0002'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 136 iMa, SUM(ctct.fTruyThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020001-010-011-0001-0000', '9020001-010-011-0001-0001', '9020001-010-011-0001-0002')
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 133 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 134 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 138 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0002'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 139 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0002'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 140 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0002'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 141 iMa, SUM(ctct.fTruyThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020002-010-011-0001-0000', '9020002-010-011-0001-0001', '9020002-010-011-0001-0002')
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	
	UNION

	SELECT 151 iMa, SUM(ctct.fSoPhaiThu) fSoBaoCao FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
	LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct 
	ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
	WHERE sXauNoiMa LIKE '9030001-010-011-0001%' OR sXauNoiMa LIKE '9030001-010-011-0002%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 155 iMa, SUM(ctct.fSoPhaiThu) fSoBaoCao FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
	LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct 
	ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
	WHERE sXauNoiMa LIKE '9030002%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	
	UNION

	SELECT 159 iMa, SUM(ctct.fSoPhaiThu) fSoBaoCao FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
	LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct 
	ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
	WHERE sXauNoiMa = '9030003'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 163 iMa, SUM(ctct.fSoPhaiThu) fSoBaoCao FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
	LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct 
	ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
	WHERE sXauNoiMa = '9030003'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 167 iMa, SUM(ctct.fSoPhaiThu) fSoBaoCao FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
	LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct 
	ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
	WHERE sXauNoiMa = '9030006'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 171 iMa, SUM(ctct.fSoPhaiThu) fSoBaoCao FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
	LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct 
	ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
	WHERE sXauNoiMa = '9030004'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 178 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	LEFT JOIN BH_DTC_PhanBoDuToanChi ct 
	ON ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE sXauNoiMa LIKE '901001%'
	AND ct.sID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))

	UNION

	SELECT 179 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	LEFT JOIN BH_DTC_PhanBoDuToanChi ct 
	ON ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE sXauNoiMa LIKE '901002%'
	AND ct.sID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))

	UNION

	SELECT 180 iMa, SUM(ctct.fTienDaCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa LIKE '901001%' OR sXauNoiMa LIKE '901002%'
	AND ct.sID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))

	UNION

	SELECT 183 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0001%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 184 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0002%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 185 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0003%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 186 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0004%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 187 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0005%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 188 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0006%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 189 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0007%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	
	UNION

	SELECT 190 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0008%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 193 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010002-010-011-0001%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 194 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010002-010-011-0002%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 195 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010002-010-011-0003%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 196 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010002-010-011-0004%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 197 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010002-010-011-0005%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 198 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010002-010-011-0006%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 199 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010002-010-011-0007%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	
	UNION

	SELECT 200 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0008%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 183 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0001%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	
	UNION

	SELECT 208 iMa, SUM(ctct.fTienDaCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa = '9050001'
	AND ct.sID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))

	UNION

	SELECT 209 iMa, SUM(ctct.fTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_KPK_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_KPK ct 
	ON ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
	WHERE sXauNoiMa = '9050001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 214 iMa, SUM(ctct.fTienDaCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa = '9050002'
	AND ct.sID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))

	UNION

	SELECT 215 iMa, SUM(ctct.fTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_KPK_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_KPK ct 
	ON ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
	WHERE sXauNoiMa = '9050002'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec

	UNION

	SELECT 218 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	LEFT JOIN BH_DTC_PhanBoDuToanChi ct 
	ON ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE sXauNoiMa LIKE '901004%' OR sXauNoiMa LIKE '9010005%'
	AND ct.sID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))

	UNION

	SELECT 222 iMa, SUM(ctct.fTienDaCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa LIKE '901004%' OR sXauNoiMa LIKE '9010005%'
	AND ct.sID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))

	UNION

	SELECT 223 iMa, SUM(ctct.fTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_KPK_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_KPK ct 
	ON ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
	WHERE sXauNoiMa LIKE '901004%' OR sXauNoiMa LIKE '9010005%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))

	UNION

	SELECT 229 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	LEFT JOIN BH_DTC_PhanBoDuToanChi ct 
	ON ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE sXauNoiMa LIKE '9010009%' 
	AND ct.sID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))

	UNION

	SELECT 230 iMa, SUM(ctct.fTienDaCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa LIKE '9010009%' 
	AND ct.sID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))

	UNION

	SELECT 231 iMa, SUM(ctct.fTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_KPK_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_KPK ct 
	ON ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
	WHERE sXauNoiMa LIKE '9010009%' 
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))


	UNION

	SELECT 236 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	LEFT JOIN BH_DTC_PhanBoDuToanChi ct 
	ON ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE sXauNoiMa LIKE '901006%' OR sXauNoiMa LIKE '9010007%'
	AND ct.sID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))


	UNION

	SELECT 237 iMa, SUM(ctct.fTienDaCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa LIKE '901006%' OR sXauNoiMa LIKE '9010007%'
	AND ct.sID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))

	UNION

	SELECT 238 iMa, SUM(ctct.fTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_KPK_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_KPK ct 
	ON ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
	WHERE sXauNoiMa LIKE '901006%' OR sXauNoiMa LIKE '9010007%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))

	UNION

	SELECT 242 iMa, SUM(ctct.fTienDaCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa LIKE '9010010-010-011-0001%'
	AND ct.sID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))

	UNION

	SELECT 243 iMa, SUM(ctct.fTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_KPK_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_KPK ct 
	ON ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
	WHERE sXauNoiMa LIKE '9010010-010-011-0001%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))

	UNION

	SELECT 248 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	LEFT JOIN BH_DTC_PhanBoDuToanChi ct 
	ON ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE sXauNoiMa LIKE '9010003%'
	AND ct.sID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))


	UNION

	SELECT 251 iMa, SUM(ctct.fTienDaCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa LIKE '9010003%'
	AND ct.sID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))

	UNION

	SELECT 252 iMa, SUM(ctct.fTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_KinhPhiQuanLy ct 
	ON ct.ID_QTC_Nam_KinhPhiQuanLy = ctct.iID_QTC_Nam_KinhPhiQuanLy
	WHERE sXauNoiMa LIKE '9010003%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))	

	) AS temp ON temp.iMa = dmtdqp.iMa

	LEFT JOIN BH_ThamDinhQuyetToan_ChungTuChiTiet ctct ON dmtdqp.iMa = ctct.iMa 
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ctct.iNamLamViec = @INamLamViec
	LEFT JOIN BH_ThamDinhQuyetToan_ChungTu ct 
	ON ctct.iID_BH_TDQT_ChungTu = ct.iID_BH_TDQT_ChungTu 
	AND ct.iID_BH_TDQT_ChungTu = @IdChungTu AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ct.iNamLamViec = @INamLamViec
	LEFT JOIN (SELECT * from DonVi WHERE iNamLamViec = @INamLamViec AND iTrangThai = 1) dv ON dv.iID_MaDonVi = ctct.iID_MaDonVi

	ORDER BY iMa
	
	DROP TABLE #dmtdqt;
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_chedo_bhxh]    Script Date: 02/02/2024 4:04:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_chedo_bhxh]
@INamLamViec int,
@IdDonVi nvarchar(max)
AS
BEGIN
	select 
		ct.iID_MaDonVi + ' - ' + dv.sTenDonVi as sTenDonVi,
		ct.sMoTa,
		dv.iKhoi,
		sum(ct.fOmDau) fOmDau,
		sum(ct.fThaiSan) fThaiSan,
		sum(ct.fTNLDBNN) fTNLDBNN,
		sum(ct.fHuuTri) fHuuTri,
		sum(ct.fPhucVien) fPhucVien,
		sum(ct.fXuatNgu) fXuatNgu,
		sum(ct.fThoiViec) fThoiViec,
		sum(ct.fTuTuat) fTuTuat,
		iKieuChu = 3
	into #data
	from
	(select 
	iID_MaDonVi,
	case when iMa > 182 and iMa < 191 then N'Khối dự toán'
	when iMa > 192 and iMa < 201 then N'Khối hạch toán'
	else N'Khối khác' end as sMoTa,
	case when ima = 183 or ima = 193 then fSoThamDinh
			else 0 end as fOmDau,
	case when ima = 184 or ima = 194 then fSoThamDinh
			else 0 end as fThaiSan,
	case when ima = 185 or ima = 195 then fSoThamDinh
			else 0 end as fTNLDBNN,
	case when ima = 186 or ima = 196 then fSoThamDinh
			else 0 end as fHuuTri,
	case when ima = 187 or ima = 197 then fSoThamDinh
			else 0 end as fPhucVien,
	case when ima = 188 or ima = 198 then fSoThamDinh
			else 0 end as fXuatNgu,
	case when ima = 189 or ima = 199 then fSoThamDinh
			else 0 end as fThoiViec,
	case when ima = 190 or ima = 200 then fSoThamDinh
			else 0 end as fTuTuat
	from BH_ThamDinhQuyetToan_ChungTuChiTiet
	where 
	iID_MaDonVi in (select * from f_split(@IdDonVi)) and 
	iNamLamViec = @INamLamViec) ct
	left join (SELECT * from DonVi WHERE iNamLamViec = @INamLamViec AND iTrangThai = 1) dv ON dv.iID_MaDonVi = ct.iID_MaDonVi
	where fOmDau > 0 or fThaiSan > 0 or fTNLDBNN > 0 or fHuuTri > 0 or fPhucVien > 0 or fXuatNgu > 0 or fThoiViec > 0 or fTuTuat > 0
	group by ct.iID_MaDonVi, dv.sTenDonVi, ct.sMoTa, dv.iKhoi

	select 
	sSTT = '',
	sTenDonVi = N'A. Đơn vị dự toán',
		sum(ct.fOmDau) fOmDau,
		sum(ct.fThaiSan) fThaiSan,
		sum(ct.fTNLDBNN) fTNLDBNN,
		sum(ct.fHuuTri) fHuuTri,
		sum(ct.fPhucVien) fPhucVien,
		sum(ct.fXuatNgu) fXuatNgu,
		sum(ct.fThoiViec) fThoiViec,
		sum(ct.fTuTuat) fTuTuat,
		iKieuChu = 1
	into #rowA
	from (select * from #data where iKhoi = 2) ct

		select 
	sSTT = '',
	sTenDonVi = N'+ Khối dự toán',
		sum(ct.fOmDau) fOmDau,
		sum(ct.fThaiSan) fThaiSan,
		sum(ct.fTNLDBNN) fTNLDBNN,
		sum(ct.fHuuTri) fHuuTri,
		sum(ct.fPhucVien) fPhucVien,
		sum(ct.fXuatNgu) fXuatNgu,
		sum(ct.fThoiViec) fThoiViec,
		sum(ct.fTuTuat) fTuTuat,
		iKieuChu = 1
	into #rowA1
	from (select * from #data where iKhoi = 2 and sMoTa = N'Khối dự toán') ct

			select 
	sSTT = '',
	sTenDonVi = N'+ Khối hạch toán',
		sum(ct.fOmDau) fOmDau,
		sum(ct.fThaiSan) fThaiSan,
		sum(ct.fTNLDBNN) fTNLDBNN,
		sum(ct.fHuuTri) fHuuTri,
		sum(ct.fPhucVien) fPhucVien,
		sum(ct.fXuatNgu) fXuatNgu,
		sum(ct.fThoiViec) fThoiViec,
		sum(ct.fTuTuat) fTuTuat,
		iKieuChu = 1
	into #rowA2
	from (select * from #data where iKhoi = 2 and sMoTa = N'Khối hạch toán') ct

	select 
	sSTT = '',
	sTenDonVi = N'B. Đơn vị hạch toán',
		sum(ct.fOmDau) fOmDau,
		sum(ct.fThaiSan) fThaiSan,
		sum(ct.fTNLDBNN) fTNLDBNN,
		sum(ct.fHuuTri) fHuuTri,
		sum(ct.fPhucVien) fPhucVien,
		sum(ct.fXuatNgu) fXuatNgu,
		sum(ct.fThoiViec) fThoiViec,
		sum(ct.fTuTuat) fTuTuat,
		iKieuChu = 1
	into #rowB
	from (select * from #data where iKhoi = 1) ct

		select 
	sSTT = '',
	sTenDonVi = N'+ Khối dự toán',
		sum(ct.fOmDau) fOmDau,
		sum(ct.fThaiSan) fThaiSan,
		sum(ct.fTNLDBNN) fTNLDBNN,
		sum(ct.fHuuTri) fHuuTri,
		sum(ct.fPhucVien) fPhucVien,
		sum(ct.fXuatNgu) fXuatNgu,
		sum(ct.fThoiViec) fThoiViec,
		sum(ct.fTuTuat) fTuTuat,
		iKieuChu = 1
	into #rowB1
	from (select * from #data where iKhoi = 1 and sMoTa = N'Khối dự toán') ct

			select 
	sSTT = '',
	sTenDonVi = N'+ Khối hạch toán',
		sum(ct.fOmDau) fOmDau,
		sum(ct.fThaiSan) fThaiSan,
		sum(ct.fTNLDBNN) fTNLDBNN,
		sum(ct.fHuuTri) fHuuTri,
		sum(ct.fPhucVien) fPhucVien,
		sum(ct.fXuatNgu) fXuatNgu,
		sum(ct.fThoiViec) fThoiViec,
		sum(ct.fTuTuat) fTuTuat,
		iKieuChu = 1
	into #rowB2
	from (select * from #data where iKhoi = 1 and sMoTa = N'Khối hạch toán') ct

				select 
	sSTT = '',
	sTenDonVi,
	iKhoi,
		sum(ct.fOmDau) fOmDau,
		sum(ct.fThaiSan) fThaiSan,
		sum(ct.fTNLDBNN) fTNLDBNN,
		sum(ct.fHuuTri) fHuuTri,
		sum(ct.fPhucVien) fPhucVien,
		sum(ct.fXuatNgu) fXuatNgu,
		sum(ct.fThoiViec) fThoiViec,
		sum(ct.fTuTuat) fTuTuat,
		iKieuChu = 3
	into #rowDV
	from #data ct
	group by ct.sTenDonVi, ct.sTenDonVi, ct.iKhoi

	select * 
	into #rowDV1
	from (select 
		ROW_NUMBER() OVER (ORDER BY sTenDonVi) AS sSTT,
		sTenDonVi,
		sMoTa = null,
		fOmDau,
		fThaiSan,
		fTNLDBNN,
		fHuuTri,
		fPhucVien,
		fXuatNgu,
		fThoiViec,
		fTuTuat,
		iKieuChu 
		from #rowDV where iKhoi = 2) dv1
	union all (select 
		'' AS sSTT,
		sTenDonVi,
		sMoTa,
		fOmDau,
		fThaiSan,
		fTNLDBNN,
		fHuuTri,
		fPhucVien,
		fXuatNgu,
		fThoiViec,
		fTuTuat,
		iKieuChu = 2
	from #data where iKhoi = 2) 
	order by sTenDonVi, sMoTa

	select *
	into #rowDV2
	from (select 
		ROW_NUMBER() OVER (ORDER BY sTenDonVi) AS sSTT,
		sTenDonVi,
		sMoTa = null,
		fOmDau,
		fThaiSan,
		fTNLDBNN,
		fHuuTri,
		fPhucVien,
		fXuatNgu,
		fThoiViec,
		fTuTuat,
		iKieuChu
		from #rowDV where iKhoi = 1) dv2
	union all (select 
		'' AS sSTT,
		sTenDonVi,
		sMoTa,
		fOmDau,
		fThaiSan,
		fTNLDBNN,
		fHuuTri,
		fPhucVien,
		fXuatNgu,
		fThoiViec,
		fTuTuat,
		iKieuChu = 2
	from #data where iKhoi = 1) 
	order by sTenDonVi, sMoTa

	select * from #rowA
	union all select * from #rowA1
	union all select * from #rowA2
	union all (select 
		sSTT,
		isnull('       ' + sMoTa, sTenDonVi) sTenDonVi,
		fOmDau,
		fThaiSan,
		fTNLDBNN,
		fHuuTri,
		fPhucVien,
		fXuatNgu,
		fThoiViec,
		fTuTuat,
		iKieuChu
	from #rowDV1)
	union all select * from #rowB
	union all select * from #rowB1
	union all select * from #rowB2
	union all (select 
			sSTT,
		isnull('       ' + sMoTa, sTenDonVi) sTenDonVi,
		fOmDau,
		fThaiSan,
		fTNLDBNN,
		fHuuTri,
		fPhucVien,
		fXuatNgu,
		fThoiViec,
		fTuTuat,
		iKieuChu
	from #rowDV2)
END
;

GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_cssk_hssv_nld]    Script Date: 02/02/2024 4:04:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_cssk_hssv_nld]
@INamLamViec int,
@IdDonVi nvarchar(max),
@Loai int
AS
BEGIN
	if (@Loai = 1)
	select 
		ct.iID_MaDonVi + ' - ' + dv.sTenDonVi as sTenDonVi,
		sum(ct.fDuToanNamTruoc) fDuToanNamTruoc,
		sum(ct.fDuToanNamNay) fDuToanNamNay,
		sum(ct.fQuyetToan) fQuyetToan
	from
	(select 
	iID_MaDonVi,
	case ima when 213 then fSoThamDinh
			else 0 end as fDuToanNamTruoc,
	case ima when 214 then fSoThamDinh 
			else 0 end as fDuToanNamNay,
	case ima when 215 then fSoThamDinh
			else 0 end as fQuyetToan
	from BH_ThamDinhQuyetToan_ChungTuChiTiet
	where iID_MaDonVi in (select * from f_split(@IdDonVi))
	and iNamLamViec = @INamLamViec) ct
	left join (SELECT * from DonVi WHERE iNamLamViec = @INamLamViec AND iTrangThai = 1) dv ON dv.iID_MaDonVi = ct.iID_MaDonVi
	where fDuToanNamTruoc > 0 or fDuToanNamNay > 0 or fQuyetToan > 0
	group by ct.iID_MaDonVi, dv.sTenDonVi
	order by ct.iID_MaDonVi

	else if (@Loai = 2)
		select 
		ct.iID_MaDonVi + ' - ' + dv.sTenDonVi as sTenDonVi,
		sum(ct.fDuToanNamTruoc) fDuToanNamTruoc,
		sum(ct.fDuToanNamNay) fDuToanNamNay,
		sum(ct.fQuyetToan) fQuyetToan
	from
	(select 
	iID_MaDonVi,
	case ima when 207 then fSoThamDinh
			else 0 end as fDuToanNamTruoc,
	case ima when 208 then fSoThamDinh 
			else 0 end as fDuToanNamNay,
	case ima when 209 then fSoThamDinh
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

GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_kcb_quanydonvi]    Script Date: 02/02/2024 4:04:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_kcb_quanydonvi]
@INamLamViec int,
@IdDonVi nvarchar(max)
AS
BEGIN
	select 
		ct.iID_MaDonVi + ' - ' + dv.sTenDonVi as sTenDonVi,
		sum(ct.fDuToanNamTruoc) fDuToanNamTruoc,
		sum(ct.fDuToanNamNay) fDuToanNamNay,
		sum(ct.fQuyetToan) fQuyetToan
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

GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_ttbyt]    Script Date: 02/02/2024 4:04:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_ttbyt]
@INamLamViec int,
@IdDonVi nvarchar(max)
AS
BEGIN
	select 
		ct.iID_MaDonVi + ' - ' + dv.sTenDonVi as sTenDonVi,
		sum(ct.fDuToanNamTruoc) fDuToanNamTruoc,
		sum(ct.fDuToanNamNay) fDuToanNamNay,
		sum(ct.fQuyetToan) fQuyetToan
	from
	(select 
	iID_MaDonVi,
	case ima when 228 then fSoThamDinh
			else 0 end as fDuToanNamTruoc,
	case ima when 229 then fSoThamDinh 
			else 0 end as fDuToanNamNay,
	case ima when 231 then fSoThamDinh
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

GO
