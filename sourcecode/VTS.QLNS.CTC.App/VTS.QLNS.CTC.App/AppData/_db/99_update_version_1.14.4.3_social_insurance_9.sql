/****** Object:  StoredProcedure [dbo].[sp_rpt_bh_thamdinhquyettoan_chitiet]    Script Date: 5/9/2024 4:02:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bh_thamdinhquyettoan_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bh_thamdinhquyettoan_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_thamdinhquyettoan_chitiet]    Script Date: 5/9/2024 4:02:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_thamdinhquyettoan_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_thamdinhquyettoan_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_thamdinhquyettoan_chitiet]    Script Date: 5/9/2024 4:02:20 PM ******/
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
	
	declare @LoaiDonVi nvarchar(50) = (select iLoai from DonVi where iNamLamViec = @INamLamViec and iID_MaDonVi = @IdDonVi);

	SELECT * INTO #dmtdqt
	FROM BH_DM_ThamDinhQuyetToan WHERE iNamLamViec = @INamLamViec AND iTrangThai = 1
	ORDER BY iSTT

	SELECT 
		iID,
		ctct.iID_BH_TDQT_ChungTuChiTiet,
		dmtdqp.iMa,
		dmtdqp.iSTT,
		iMaCha,
		sSTT,
		sNoiDung,
		sXauNoiMa,
		iKieuChu,
		dmtdqp.iTrangThai,
		dmtdqp.iNamLamViec,
		dmtdqp.sNguoiTao, dmtdqp.sNguoiSua,
		CASE WHEN ctct.fSoBaoCao is null or ctct.fSoBaoCao = 0 THEN temp.fSoBaoCao
			 ELSE ctct.fSoBaoCao
		END AS fSoBaoCao,
		ctct.fSoThamDinh,
		CASE WHEN ctct.fQuanNhan is null or ctct.fQuanNhan = 0 THEN trocapnuoicon.fQuanNhan
			ELSE ctct.fQuanNhan 
		END AS fQuanNhan,
		CASE WHEN ctct.fCNVLDHD is null or ctct.fCNVLDHD = 0 THEN trocapnuoicon.fCNVLDHD
			ELSE ctct.fCNVLDHD 
		END AS fCNVLDHD
		
	INTO #dmtdqtResult
	FROM #dmtdqt dmtdqp
	LEFT JOIN
	(SELECT 257 iMa, (ISNULL(SUM(ctct.fTienSQ_ThucChi), 0) + ISNULL(SUM(ctct.fTienQNCN_ThucChi), 0)) fQuanNhan,
		(ISNULL(SUM(ctct.fTienCNVCQP_ThucChi), 0) + ISNULL(SUM(ctct.fTienLDHD_ThucChi), 0)) fCNVLDHD
		FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
		LEFT JOIN BH_QTC_Nam_CheDoBHXH ct ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
		WHERE (ctct.sXauNoiMa LIKE '9010001-010-011-0002-0001-0001-00%' OR ctct.sXauNoiMa LIKE '9010002-010-011-0002-0001-0001-00%')
		AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
		AND ct.iNamLamViec = @INamLamViec
		AND ct.bIsKhoa = 1) trocapnuoicon ON dmtdqp.iMa = trocapnuoicon.iMa
	LEFT JOIN
	(
	SELECT 7 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 8 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1


	UNION
	--phu nhan phu quan khoi du toan
	SELECT 259 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0003'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1


	UNION

	SELECT 9 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 10 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 12 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 13 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 14 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 15 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 19 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 20 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 21 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1


	UNION

	SELECT 22 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 24 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 25 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 26 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 27 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 29 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0002'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 30 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0002'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 34 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 35 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1


UNION
 --phu nhan phu quan khoi hach toan
	SELECT 260 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0003'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 36 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 37 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 39 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 40 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 41 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 42 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 46 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 47 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 48 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 49 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 51 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 52 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 53 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 54 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 56 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0002'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 57 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0002'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 70 iMa, SUM(ctct.fThu_BHTN_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020001-010-011-0001-0000', '9020001-010-011-0001-0001', '9020001-010-011-0001-0002', '9020001-010-011-0002-0000', '9020001-010-011-0002-0001')
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 71 iMa, SUM(ctct.fTruyThu_BHTN_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020001-010-011-0001-0000', '9020001-010-011-0001-0001', '9020001-010-011-0001-0002', '9020001-010-011-0002-0000', '9020001-010-011-0002-0001')
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 73 iMa, SUM(ctct.fThu_BHTN_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020001-010-011-0001-0000', '9020001-010-011-0001-0001', '9020001-010-011-0001-0002', '9020001-010-011-0002-0000', '9020001-010-011-0002-0001')
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 74 iMa, SUM(ctct.fTruyThu_BHTN_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020001-010-011-0001-0000', '9020001-010-011-0001-0001', '9020001-010-011-0001-0002', '9020001-010-011-0002-0000', '9020001-010-011-0002-0001')
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 77 iMa, SUM(ctct.fThu_BHTN_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020002-010-011-0001-0000', '9020002-010-011-0001-0001', '9020002-010-011-0001-0002', '9020002-010-011-0002-0000', '9020002-010-011-0002-0001')
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 78 iMa, SUM(ctct.fTruyThu_BHTN_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020002-010-011-0001-0000', '9020002-010-011-0001-0001', '9020002-010-011-0001-0002', '9020002-010-011-0002-0000', '9020002-010-011-0002-0001')
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 80 iMa, SUM(ctct.fThu_BHTN_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020002-010-011-0001-0000', '9020002-010-011-0001-0001', '9020002-010-011-0001-0002', '9020002-010-011-0002-0000', '9020002-010-011-0002-0001')
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	UNION

	SELECT 81 iMa, SUM(ctct.fTruyThu_BHTN_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020002-010-011-0001-0000', '9020002-010-011-0001-0001', '9020002-010-011-0001-0002', '9020002-010-011-0002-0000', '9020002-010-011-0002-0001')
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 95 iMa, SUM(ctct.fThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 96 iMa, SUM(ctct.fThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 98 iMa, SUM(ctct.fTruyThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 99 iMa, SUM(ctct.fTruyThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 102 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 103 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 105 iMa, SUM(ctct.fTruyThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 106 iMa, SUM(ctct.fTruyThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 110 iMa, SUM(ctct.fThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 111 iMa, SUM(ctct.fThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 113 iMa, SUM(ctct.fTruyThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 114 iMa, SUM(ctct.fTruyThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 17 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 118 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 120 iMa, SUM(ctct.fTruyThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 121 iMa, SUM(ctct.fTruyThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 133 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 134 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 135 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0002'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 136 iMa, SUM(ctct.fTruyThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020001-010-011-0001-0000', '9020001-010-011-0001-0001', '9020001-010-011-0001-0002')
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 133 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 134 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 138 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 139 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 140 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0002'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 141 iMa, SUM(ctct.fTruyThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020002-010-011-0001-0000', '9020002-010-011-0001-0001', '9020002-010-011-0001-0002')
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 151 iMa, SUM(ctct.fSoPhaiThu) fSoBaoCao FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
	LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct 
	ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
	WHERE sXauNoiMa LIKE '9030001-010-011-0001%' OR sXauNoiMa LIKE '9030001-010-011-0002%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 155 iMa, SUM(ctct.fSoPhaiThu) fSoBaoCao FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
	LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct 
	ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
	WHERE sXauNoiMa LIKE '9030002%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 159 iMa, SUM(ctct.fSoPhaiThu) fSoBaoCao FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
	LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct 
	ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
	WHERE sXauNoiMa = '9030003'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 163 iMa, SUM(ctct.fSoPhaiThu) fSoBaoCao FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
	LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct 
	ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
	WHERE sXauNoiMa = '9030003'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 167 iMa, SUM(ctct.fSoPhaiThu) fSoBaoCao FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
	LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct 
	ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
	WHERE sXauNoiMa = '9030006'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 171 iMa, SUM(ctct.fSoPhaiThu) fSoBaoCao FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
	LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct 
	ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
	WHERE sXauNoiMa = '9030004'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 178 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	JOIN BH_DTC_PhanBoDuToanChi ct on ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE sXauNoiMa LIKE '9010001%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 179 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	JOIN BH_DTC_PhanBoDuToanChi ct on ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE sXauNoiMa LIKE '9010002%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 180 iMa, SUM(ctct.fTienKeHoachCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	--WHERE (ctct.sXauNoiMa LIKE '9010001%' OR ctct.sXauNoiMa LIKE '9010002%')
	WHERE (ctct.sXauNoiMa LIKE '901%')
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 183 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0001%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 184 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0002%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 185 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0003%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 186 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0004%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 187 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0005%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 188 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0006%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 189 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0007%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 190 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0008%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 193 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010002-010-011-0001%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 194 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010002-010-011-0002%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 195 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010002-010-011-0003%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 196 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010002-010-011-0004%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 197 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010002-010-011-0005%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 198 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010002-010-011-0006%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 199 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010002-010-011-0007%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 200 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0008%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 183 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0001%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	--UNION

	--SELECT 207 iMa, ISNULL(SUM(ctct.fTienKeHoachCap), 0) - (SELECT ISNULL(SUM(fTien_ThucChi), 0) FROM BH_QTC_Nam_KPK_ChiTiet WHERE sXauNoiMa like '9050001-010%' AND iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND iNamLamViec = @INamLamViec - 1) fSoBaoCao 
	--FROM BH_CP_ChungTu_ChiTiet ctct
	--LEFT JOIN BH_CP_ChungTu ct 
	--ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	--WHERE sXauNoiMa like '9050001%'
	--AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	--AND ct.iNamChungTu = @INamLamViec - 1
	--AND ct.bIsKhoa = 1
	
	UNION

	SELECT 208 iMa, SUM(ctct.fTienKeHoachCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa like '9050001%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 209 iMa, SUM(ctct.fTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_KPK_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_KPK ct 
	ON ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
	WHERE sXauNoiMa like '9050001%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	--UNION

	--SELECT 213 iMa, ISNULL(SUM(ctct.fTienKeHoachCap), 0) - (SELECT ISNULL(SUM(fTien_ThucChi), 0) FROM BH_QTC_Nam_KPK_ChiTiet WHERE sXauNoiMa LIKE '9050002-010%' AND iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND iNamLamViec = @INamLamViec - 1) fSoBaoCao 
	--FROM BH_CP_ChungTu_ChiTiet ctct
	--LEFT JOIN BH_CP_ChungTu ct 
	--ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	--WHERE sXauNoiMa LIKE '9050002%'
	--AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	--AND ct.iNamChungTu = @INamLamViec - 1
	--AND ct.bIsKhoa = 1
	
	UNION

	SELECT 214 iMa, SUM(ctct.fTienKeHoachCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa like '9050002%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 215 iMa, SUM(ctct.fTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_KPK_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_KPK ct 
	ON ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
	WHERE sXauNoiMa like '9050002%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 218 iMa, 0.1 * (ISNULL(SUM(ctct.fBHYT_NLD), 0) + ISNULL(SUM(ctct.fBHYT_NSD), 0)) fSoBaoCao 
	FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
	LEFT JOIN  BH_DTT_BHXH_PhanBo_ChungTu ct 
	ON ct.iID_DTT_BHXH_PhanBo_ChungTu = ctct.iID_DTT_BHXH_ChungTu
	WHERE (ctct.sXauNoiMa like '9020001-010-011-0001%' OR ctct.sXauNoiMa like '9020002-010-011-0001%')
		AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
		AND ct.iNamLamViec = @INamLamViec
		AND ct.bKhoa = 1

	UNION

	--SELECT 220 iMa, 0.1 * (ISNULL(SUM(ctct.fBHYT_NLD), 0) + ISNULL(SUM(ctct.fBHYT_NSD), 0) - (SELECT ISNULL(SUM(ctct.fThu_BHYT_NLD), 0) + ISNULL(SUM(ctct.fThu_BHYT_NSD), 0)
	--	FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	--	JOIN BH_QTT_BHXH_ChungTu ct ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	--	WHERE (ctct.sXauNoiMa LIKE '9020001-010-011-0001%' OR ctct.sXauNoiMa like '9020002-010-011-0001%') 
	--		AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) 
	--		AND ctct.iNamLamViec = @INamLamViec - 1
	--		AND ct.iQuyNam = @INamLamViec - 1
	--		AND ct.bIsKhoa = 1)) fSoBaoCao
	--FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
	--JOIN BH_DTT_BHXH_PhanBo_ChungTu ct on ct.iID_DTT_BHXH_PhanBo_ChungTu = ctct.iID_DTT_BHXH_ChungTu
	--WHERE (ctct.sXauNoiMa LIKE '9020001-010-011-0001%' OR ctct.sXauNoiMa like '9020002-010-011-0001%')
	--AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	--AND ctct.iNamLamViec = @INamLamViec - 1
	--AND ct.bKhoa = 1

	--UNION

	SELECT 221 iMa, (ISNULL(SUM(ctct.fThu_BHYT_NLD), 0) + ISNULL(SUM(ctct.fThu_BHYT_NSD), 0)) * 0.1 fSoBaoCao
	FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	JOIN BH_QTT_BHXH_ChungTu ct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE (ctct.sXauNoiMa LIKE '9020001-010-011-0001%' OR ctct.sXauNoiMa LIKE '9020002-010-011-0001%')
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 222 iMa, SUM(ctct.fTienKeHoachCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa LIKE '9010004%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 223 iMa, SUM(ctct.fTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_KCB_QuanYDonVi ct 
	ON ct.ID_QTC_Nam_KCB_QuanYDonVi = ctct.iID_QTC_Nam_KCB_QuanYDonVi
	WHERE ctct.sXauNoiMa LIKE '9010004%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 228 iMa, ISNULL(SUM(ctct.fTienTuChi), 0) - (SELECT ISNULL(SUM(ctct.fTien_ThucChi), 0) 
		FROM BH_QTC_Nam_KPK_ChiTiet ctct
		JOIN BH_QTC_Nam_KPK ct on ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
		WHERE ctct.sXauNoiMa LIKE '9010009-010%' AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ctct.iNamLamViec = @INamLamViec - 1 AND ct.bIsKhoa = 1) fSoBaoCao 
	FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	JOIN BH_DTC_PhanBoDuToanChi ct on ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE ctct.sXauNoiMa LIKE '9010009%' 
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec - 1
	AND ct.bIsKhoa = 1

	UNION

	SELECT 229 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	LEFT JOIN BH_DTC_PhanBoDuToanChi ct 
	ON ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE sXauNoiMa LIKE '9010009%' 
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 230 iMa, SUM(ctct.fTienKeHoachCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa LIKE '9010009%' 
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 231 iMa, SUM(ctct.fTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_KPK_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_KPK ct 
	ON ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
	WHERE sXauNoiMa LIKE '9010009-010%' 
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 235 iMa, ISNULL(SUM(ctct.fTienTuChi), 0) - (SELECT ISNULL(SUM(ctct.fTien_ThucChi), 0) 
		FROM BH_QTC_Nam_KPK_ChiTiet ctct
		JOIN BH_QTC_Nam_KPK ct on ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
		WHERE ctct.sXauNoiMa LIKE '9010006-010%' AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ctct.iNamLamViec = @INamLamViec - 1 AND ct.bIsKhoa = 1) fSoBaoCao 
	FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	JOIN  BH_DTC_PhanBoDuToanChi ct on ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE sXauNoiMa LIKE '9010006%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) 
	AND ctct.iNamLamViec = @INamLamViec - 1
	AND ct.bIsKhoa = 1

	UNION

	SELECT 236 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	LEFT JOIN BH_DTC_PhanBoDuToanChi ct 
	ON ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE sXauNoiMa LIKE '9010006%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 237 iMa, SUM(ctct.fTienKeHoachCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa LIKE '9010006%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 238 iMa, SUM(ctct.fTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_KPK_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_KPK ct 
	ON ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
	WHERE sXauNoiMa LIKE '9010006%' OR sXauNoiMa LIKE '9010007%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 242 iMa, SUM(ctct.fTienKeHoachCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa LIKE '9010010%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 243 iMa, SUM(ctct.fTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_KPK_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_KPK ct 
	ON ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
	WHERE sXauNoiMa LIKE '9010010-010%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 247 iMa, ISNULL(SUM(ctct.fTienTuChi), 0) - (SELECT ISNULL(SUM(ctct.fTien_ThucChi), 0) 
		FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet ctct
		JOIN BH_QTC_Nam_KinhPhiQuanLy ct on ct.ID_QTC_Nam_KinhPhiQuanLy = ctct.iID_QTC_Nam_KinhPhiQuanLy
		WHERE ctct.sXauNoiMa LIKE '9010003%' AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ctct.iNamLamViec = @INamLamViec - 1 and sM is not null and sM <> '' AND ct.bIsKhoa = 1) fSoBaoCao 
	FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	JOIN BH_DTC_PhanBoDuToanChi ct on ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE sXauNoiMa LIKE '9010003%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec - 1
	AND ct.bIsKhoa = 1

	UNION

	SELECT 248 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	LEFT JOIN BH_DTC_PhanBoDuToanChi ct 
	ON ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE sXauNoiMa LIKE '9010003%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 250 iMa, ISNULL(SUM(ctct.fTienKeHoachCap), 0) - (SELECT ISNULL(SUM(ctct.fTien_ThucChi), 0) 
		FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet ctct
		JOIN BH_QTC_Nam_KinhPhiQuanLy ct on ID_QTC_Nam_KinhPhiQuanLy = ID_QTC_Nam_KinhPhiQuanLy
		WHERE ctct.sXauNoiMa LIKE '9010003-010%' AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ctct.iNamLamViec = @INamLamViec - 1 AND ct.bIsKhoa = 1) fSoBaoCao 
	FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa LIKE '9010003%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec - 1
	AND ct.bIsKhoa = 1

	UNION

	SELECT 251 iMa, SUM(ctct.fTienKeHoachCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa LIKE '9010003%'
	AND ct.sID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 252 iMa, SUM(ctct.fTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_KinhPhiQuanLy ct 
	ON ct.ID_QTC_Nam_KinhPhiQuanLy = ctct.iID_QTC_Nam_KinhPhiQuanLy
	WHERE sXauNoiMa LIKE '9010003%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	) AS temp ON temp.iMa = dmtdqp.iMa
	
	LEFT JOIN BH_ThamDinhQuyetToan_ChungTuChiTiet ctct ON dmtdqp.iMa = ctct.iMa 
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ctct.iNamLamViec = @INamLamViec
	LEFT JOIN BH_ThamDinhQuyetToan_ChungTu ct ON ctct.iID_BH_TDQT_ChungTu = ct.iID_BH_TDQT_ChungTu 
	AND ct.iID_BH_TDQT_ChungTu = @IdChungTu AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ct.iNamLamViec = @INamLamViec
	LEFT JOIN (SELECT * from DonVi WHERE iNamLamViec = @INamLamViec AND iTrangThai = 1) dv ON dv.iID_MaDonVi = ctct.iID_MaDonVi

	--ORDER BY iMa

	--Lấy dữ liệu chứng từ đơn vị cha
	SELECT dvc.iMa, dvc.fSoBaoCao INTO #dmtdqtCha FROM
	(
	SELECT 178 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct JOIN BH_DTC_DuToanChiTrenGiao ct ON ct.ID = ctct.iID_DTC_DuToanChiTrenGiao
	WHERE ctct.sXauNoiMa LIKE '9010001%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 179 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct JOIN BH_DTC_DuToanChiTrenGiao ct ON ct.ID = ctct.iID_DTC_DuToanChiTrenGiao
	WHERE ctct.sXauNoiMa LIKE '9010002%'
		AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
		AND ctct.iNamLamViec = @INamLamViec
		AND ct.bIsKhoa = 1

	UNION

	SELECT 218 iMa, 0.1 * (ISNULL(SUM(ctct.fThu_BHYT_NLD), 0) + ISNULL(SUM(ctct.fThu_BHYT_NSD), 0)) fSoBaoCao 
	FROM BH_DTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN  BH_DTT_BHXH_ChungTu ct 
	ON ct.iID_DTT_BHXH = ctct.iID_DTT_BHXH
	WHERE (ctct.sXauNoiMa like '9020001-010-011-0001%' OR ctct.sXauNoiMa like '9020002-010-011-0001%')
		AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
		AND ct.iNamLamViec = @INamLamViec
		AND ct.bIsKhoa = 1

	UNION

	--SELECT 220 iMa, 0.1 * (ISNULL(SUM(fThu_BHYT_NLD + fThu_BHYT_NSD), 0) - (SELECT ISNULL(SUM(ctct.fThu_BHYT_NLD + ctct.fThu_BHYT_NSD), 0) 
	--	FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	--	JOIN BH_QTT_BHXH_ChungTu ct ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	--	WHERE (ctct.sXauNoiMa LIKE '9020001-010-011-0001%' OR ctct.sXauNoiMa like '9020002-010-011-0001%') 
	--		AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) 
	--		AND ctct.iNamLamViec = @INamLamViec - 1
	--		AND ct.iQuyNam = @INamLamViec - 1
	--		AND ct.bIsKhoa = 1)) fSoBaoCao
	--FROM BH_DTT_BHXH_ChungTu_ChiTiet ctct
	--JOIN BH_DTT_BHXH_ChungTu ct ON ct.iID_DTT_BHXH = ctct.iID_DTT_BHXH
	--WHERE (ctct.sXauNoiMa LIKE '9020001-010-011-0001%' OR ctct.sXauNoiMa like '9020002-010-011-0001%')
	--AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	--AND ctct.iNamLamViec = @INamLamViec - 1
	--AND ct.bIsKhoa = 1 

	--UNION

	SELECT 228 iMa, ISNULL(SUM(fTienTuChi), 0) - (SELECT ISNULL(SUM(ctct.fTien_ThucChi), 0) 
		FROM BH_QTC_Nam_KPK_ChiTiet ctct
		JOIN BH_QTC_Nam_KPK ct ON ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
		WHERE ctct.sXauNoiMa LIKE '9010009-010%' AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ctct.iNamLamViec = @INamLamViec - 1 AND ct.bIsKhoa = 1) fSoBaoCao 
	FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct
	JOIN BH_DTC_DuToanChiTrenGiao ct ON ct.ID = ctct.iID_DTC_DuToanChiTrenGiao
	WHERE ctct.sXauNoiMa LIKE '9010009%' 
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec - 1
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 229 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct
	LEFT JOIN BH_DTC_DuToanChiTrenGiao ct 
	ON ct.ID = ctct.iID_DTC_DuToanChiTrenGiao
	WHERE 
	sXauNoiMa LIKE '9010009%' 
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 235 iMa, ISNULL(SUM(ctct.fTienTuChi), 0) - (SELECT ISNULL(SUM(ctct.fTien_ThucChi), 0) 
		FROM BH_QTC_Nam_KPK_ChiTiet ctct
		JOIN BH_QTC_Nam_KPK ct ON ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
		WHERE ctct.sXauNoiMa LIKE '9010006-010%' AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ctct.iNamLamViec = @INamLamViec - 1 AND ct.bIsKhoa = 1) fSoBaoCao 
	FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct
	JOIN BH_DTC_DuToanChiTrenGiao ct ON ct.ID = ctct.iID_DTC_DuToanChiTrenGiao
	WHERE ctct.sXauNoiMa LIKE '9010006%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec - 1
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 236 iMa, SUM(ctct.fTienTuChi) fSoBaoCao 
	FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct 
	JOIN BH_DTC_DuToanChiTrenGiao ct ON ct.ID = ctct.iID_DTC_DuToanChiTrenGiao
	WHERE ctct.sXauNoiMa LIKE '9010006%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 247 iMa, ISNULL(SUM(ctct.fTienTuChi), 0) - (SELECT ISNULL(SUM(ctct.fTien_ThucChi), 0) 
		FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet ctct
		JOIN BH_QTC_Nam_KinhPhiQuanLy ct ON ct.ID_QTC_Nam_KinhPhiQuanLy = ctct.iID_QTC_Nam_KinhPhiQuanLy
		WHERE ctct.sXauNoiMa LIKE '9010003%' AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ctct.iNamLamViec = @INamLamViec - 1 and sM is not null and sM <> '' AND ct.BIsKhoa = 1) fSoBaoCao 
	FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct
	JOIN BH_DTC_DuToanChiTrenGiao ct ON ct.ID = ctct.iID_DTC_DuToanChiTrenGiao
	WHERE sXauNoiMa LIKE '9010003%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec - 1
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 248 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct
	JOIN BH_DTC_DuToanChiTrenGiao ct ON ct.ID = ctct.iID_DTC_DuToanChiTrenGiao
	WHERE ctct.sXauNoiMa LIKE '9010003%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	) dvc

	IF @LoaiDonVi = 0
		UPDATE dmtdqtResult
		SET dmtdqtResult.fSoBaoCao = dmtdqtCha.fSoBaoCao
		FROM #dmtdqtResult AS dmtdqtResult
		JOIN #dmtdqtCha AS dmtdqtCha ON dmtdqtResult.iMa = dmtdqtCha.iMa;

	--Result
	SELECT * FROM #dmtdqtResult
	ORDER BY iSTT;

	DROP TABLE #dmtdqt;
	DROP TABLE #dmtdqtCha;
	DROP TABLE #dmtdqtResult;
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bh_thamdinhquyettoan_chitiet]    Script Date: 5/9/2024 4:02:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bh_thamdinhquyettoan_chitiet]
@INamLamViec int,
@IdDonVi nvarchar(max),
@DonViTinh int
AS
BEGIN

	declare @LoaiDonVi nvarchar(50) = (select iLoai from DonVi where iNamLamViec = @INamLamViec and iID_MaDonVi = @IdDonVi);

	SELECT * INTO #dmtdqt
	FROM BH_DM_ThamDinhQuyetToan WHERE iNamLamViec = @INamLamViec AND iTrangThai = 1
	ORDER BY iMa
	SELECT 
		iID,
		iMa,
		iSTT,
		iMaCha,
		sSTT,
		sNoiDung,
		sXauNoiMa,
		iKieuChu,
		iTrangThai,
		iNamLamViec,
		sNguoiTao,
		sNguoiSua,
		SUM(fSoBaoCao) fSoBaoCao,
		SUM(fSoThamDinh) fSoThamDinh,
		SUM(fQuanNhan) fQuanNhan,
		SUM(fCNVLDHD) fCNVLDHD
	INTO #dmtdqtResult
	FROM
	(SELECT 
		iID,
		dmtdqp.iMa,
		dmtdqp.iSTT,
		iMaCha,
		sSTT,
		sNoiDung,
		sXauNoiMa,
		iKieuChu,
		dmtdqp.iTrangThai,
		dmtdqp.iNamLamViec,
		dmtdqp.sNguoiTao, dmtdqp.sNguoiSua,
		CASE WHEN ctct.fSoBaoCao is null or ctct.fSoBaoCao = 0 THEN temp.fSoBaoCao
			 ELSE ctct.fSoBaoCao
		END AS fSoBaoCao,
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
	AND ct.bIsKhoa = 1

	UNION

	SELECT 8 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 9 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 10 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 12 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 13 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 14 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 15 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 19 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 20 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 21 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1


	UNION

	SELECT 22 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 24 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 25 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 26 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 27 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 29 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0002'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 30 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0002'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 34 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 35 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 36 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 37 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 39 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 40 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 41 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 42 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 46 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 47 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 48 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 49 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 51 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 52 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 53 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 54 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 56 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0002'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 57 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0002'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 70 iMa, SUM(ctct.fThu_BHTN_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020001-010-011-0001-0000', '9020001-010-011-0001-0001', '9020001-010-011-0001-0002', '9020001-010-011-0002-0000', '9020001-010-011-0002-0001')
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 71 iMa, SUM(ctct.fTruyThu_BHTN_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020001-010-011-0001-0000', '9020001-010-011-0001-0001', '9020001-010-011-0001-0002', '9020001-010-011-0002-0000', '9020001-010-011-0002-0001')
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 73 iMa, SUM(ctct.fThu_BHTN_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020001-010-011-0001-0000', '9020001-010-011-0001-0001', '9020001-010-011-0001-0002', '9020001-010-011-0002-0000', '9020001-010-011-0002-0001')
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 74 iMa, SUM(ctct.fTruyThu_BHTN_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020001-010-011-0001-0000', '9020001-010-011-0001-0001', '9020001-010-011-0001-0002', '9020001-010-011-0002-0000', '9020001-010-011-0002-0001')
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 77 iMa, SUM(ctct.fThu_BHTN_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020002-010-011-0001-0000', '9020002-010-011-0001-0001', '9020002-010-011-0001-0002', '9020002-010-011-0002-0000', '9020002-010-011-0002-0001')
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 78 iMa, SUM(ctct.fTruyThu_BHTN_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020002-010-011-0001-0000', '9020002-010-011-0001-0001', '9020002-010-011-0001-0002', '9020002-010-011-0002-0000', '9020002-010-011-0002-0001')
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 80 iMa, SUM(ctct.fThu_BHTN_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020002-010-011-0001-0000', '9020002-010-011-0001-0001', '9020002-010-011-0001-0002', '9020002-010-011-0002-0000', '9020002-010-011-0002-0001')
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	UNION

	SELECT 81 iMa, SUM(ctct.fTruyThu_BHTN_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020002-010-011-0001-0000', '9020002-010-011-0001-0001', '9020002-010-011-0001-0002', '9020002-010-011-0002-0000', '9020002-010-011-0002-0001')
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 95 iMa, SUM(ctct.fThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 96 iMa, SUM(ctct.fThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 98 iMa, SUM(ctct.fTruyThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 99 iMa, SUM(ctct.fTruyThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 102 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 103 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 105 iMa, SUM(ctct.fTruyThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 106 iMa, SUM(ctct.fTruyThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 110 iMa, SUM(ctct.fThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 111 iMa, SUM(ctct.fThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 113 iMa, SUM(ctct.fTruyThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 114 iMa, SUM(ctct.fTruyThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 17 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 118 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 120 iMa, SUM(ctct.fTruyThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 121 iMa, SUM(ctct.fTruyThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 133 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 134 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 135 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0002'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 136 iMa, SUM(ctct.fTruyThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020001-010-011-0001-0000', '9020001-010-011-0001-0001', '9020001-010-011-0001-0002')
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 133 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 134 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 138 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 139 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 140 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0002'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 141 iMa, SUM(ctct.fTruyThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020002-010-011-0001-0000', '9020002-010-011-0001-0001', '9020002-010-011-0001-0002')
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 151 iMa, SUM(ctct.fSoPhaiThu) fSoBaoCao FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
	LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct 
	ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
	WHERE sXauNoiMa LIKE '9030001-010-011-0001%' OR sXauNoiMa LIKE '9030001-010-011-0002%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 155 iMa, SUM(ctct.fSoPhaiThu) fSoBaoCao FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
	LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct 
	ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
	WHERE sXauNoiMa LIKE '9030002%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 159 iMa, SUM(ctct.fSoPhaiThu) fSoBaoCao FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
	LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct 
	ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
	WHERE sXauNoiMa = '9030003'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 163 iMa, SUM(ctct.fSoPhaiThu) fSoBaoCao FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
	LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct 
	ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
	WHERE sXauNoiMa = '9030003'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 167 iMa, SUM(ctct.fSoPhaiThu) fSoBaoCao FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
	LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct 
	ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
	WHERE sXauNoiMa = '9030006'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 171 iMa, SUM(ctct.fSoPhaiThu) fSoBaoCao FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
	LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct 
	ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
	WHERE sXauNoiMa = '9030004'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 178 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	JOIN BH_DTC_PhanBoDuToanChi ct on ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE sXauNoiMa LIKE '9010001%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 179 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	JOIN BH_DTC_PhanBoDuToanChi ct on ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE sXauNoiMa LIKE '9010002%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 180 iMa, SUM(ctct.fTienKeHoachCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	--WHERE (ctct.sXauNoiMa LIKE '9010001%' OR ctct.sXauNoiMa LIKE '9010002%')
	WHERE (ctct.sXauNoiMa LIKE '901%')
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 183 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0001%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 184 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0002%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 185 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0003%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 186 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0004%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 187 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0005%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 188 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0006%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 189 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0007%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 190 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0008%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 193 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010002-010-011-0001%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 194 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010002-010-011-0002%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 195 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010002-010-011-0003%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 196 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010002-010-011-0004%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 197 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010002-010-011-0005%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 198 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010002-010-011-0006%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 199 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010002-010-011-0007%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 200 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0008%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 183 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0001%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 207 iMa, ISNULL(SUM(ctct.fTienKeHoachCap), 0) - (SELECT ISNULL(SUM(fTien_ThucChi), 0) FROM BH_QTC_Nam_KPK_ChiTiet WHERE sXauNoiMa like '9050001-010%' AND iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND iNamLamViec = @INamLamViec - 1) fSoBaoCao 
	FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa like '9050001%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec - 1
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 208 iMa, SUM(ctct.fTienKeHoachCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa like '9050001%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 209 iMa, SUM(ctct.fTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_KPK_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_KPK ct 
	ON ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
	WHERE sXauNoiMa like '9050001%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	--UNION

	--SELECT 213 iMa, ISNULL(SUM(ctct.fTienKeHoachCap), 0) - (SELECT ISNULL(SUM(fTien_ThucChi), 0) FROM BH_QTC_Nam_KPK_ChiTiet WHERE sXauNoiMa LIKE '9050002-010%' AND iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND iNamLamViec = @INamLamViec - 1) fSoBaoCao 
	--FROM BH_CP_ChungTu_ChiTiet ctct
	--LEFT JOIN BH_CP_ChungTu ct 
	--ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	--WHERE sXauNoiMa LIKE '9050002%'
	--AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	--AND ct.iNamChungTu = @INamLamViec - 1
	--AND ct.bIsKhoa = 1
	
	UNION

	SELECT 214 iMa, SUM(ctct.fTienKeHoachCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa like '9050002%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 215 iMa, SUM(ctct.fTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_KPK_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_KPK ct 
	ON ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
	WHERE sXauNoiMa like '9050002%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 218 iMa, 0.1 * (ISNULL(SUM(ctct.fBHYT_NLD), 0) + ISNULL(SUM(ctct.fBHYT_NSD), 0)) fSoBaoCao 
	FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
	LEFT JOIN  BH_DTT_BHXH_PhanBo_ChungTu ct 
	ON ct.iID_DTT_BHXH_PhanBo_ChungTu = ctct.iID_DTT_BHXH_ChungTu
	WHERE (ctct.sXauNoiMa like '9020001-010-011-0001%' OR ctct.sXauNoiMa like '9020002-010-011-0001%')
		AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
		AND ct.iNamLamViec = @INamLamViec
		AND ct.bKhoa = 1

	UNION

	--SELECT 220 iMa, 0.1 * (ISNULL(SUM(ctct.fBHYT_NLD), 0) + ISNULL(SUM(ctct.fBHYT_NSD), 0) - (SELECT ISNULL(SUM(ctct.fThu_BHYT_NLD), 0) + ISNULL(SUM(ctct.fThu_BHYT_NSD), 0)
	--	FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	--	JOIN BH_QTT_BHXH_ChungTu ct ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	--	WHERE (ctct.sXauNoiMa LIKE '9020001-010-011-0001%' OR ctct.sXauNoiMa like '9020002-010-011-0001%') 
	--		AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) 
	--		AND ctct.iNamLamViec = @INamLamViec - 1
	--		AND ct.iQuyNam = @INamLamViec - 1
	--		AND ct.bIsKhoa = 1)) fSoBaoCao
	--FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
	--JOIN BH_DTT_BHXH_PhanBo_ChungTu ct on ct.iID_DTT_BHXH_PhanBo_ChungTu = ctct.iID_DTT_BHXH_ChungTu
	--WHERE (ctct.sXauNoiMa LIKE '9020001-010-011-0001%' OR ctct.sXauNoiMa like '9020002-010-011-0001%')
	--AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	--AND ctct.iNamLamViec = @INamLamViec - 1
	--AND ct.bKhoa = 1

	--UNION

	SELECT 221 iMa, (ISNULL(SUM(ctct.fThu_BHYT_NLD), 0) + ISNULL(SUM(ctct.fThu_BHYT_NSD), 0)) * 0.1 fSoBaoCao
	FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	JOIN BH_QTT_BHXH_ChungTu ct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE (ctct.sXauNoiMa LIKE '9020001-010-011-0001%' OR ctct.sXauNoiMa LIKE '9020002-010-011-0001%')
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 222 iMa, SUM(ctct.fTienKeHoachCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa LIKE '9010004%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 223 iMa, SUM(ctct.fTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_KCB_QuanYDonVi ct 
	ON ct.ID_QTC_Nam_KCB_QuanYDonVi = ctct.iID_QTC_Nam_KCB_QuanYDonVi
	WHERE ctct.sXauNoiMa LIKE '9010004%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 228 iMa, ISNULL(SUM(ctct.fTienTuChi), 0) - (SELECT ISNULL(SUM(ctct.fTien_ThucChi), 0) 
		FROM BH_QTC_Nam_KPK_ChiTiet ctct
		JOIN BH_QTC_Nam_KPK ct on ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
		WHERE ctct.sXauNoiMa LIKE '9010009-010%' AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ctct.iNamLamViec = @INamLamViec - 1 AND ct.bIsKhoa = 1) fSoBaoCao 
	FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	JOIN BH_DTC_PhanBoDuToanChi ct on ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE ctct.sXauNoiMa LIKE '9010009%' 
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec - 1
	AND ct.bIsKhoa = 1

	UNION

	SELECT 229 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	LEFT JOIN BH_DTC_PhanBoDuToanChi ct 
	ON ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE sXauNoiMa LIKE '9010009%' 
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 230 iMa, SUM(ctct.fTienKeHoachCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa LIKE '9010009%' 
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 231 iMa, SUM(ctct.fTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_KPK_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_KPK ct 
	ON ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
	WHERE sXauNoiMa LIKE '9010009-010%' 
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 235 iMa, ISNULL(SUM(ctct.fTienTuChi), 0) - (SELECT ISNULL(SUM(ctct.fTien_ThucChi), 0) 
		FROM BH_QTC_Nam_KPK_ChiTiet ctct
		JOIN BH_QTC_Nam_KPK ct on ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
		WHERE ctct.sXauNoiMa LIKE '9010006-010%' AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ctct.iNamLamViec = @INamLamViec - 1 AND ct.bIsKhoa = 1) fSoBaoCao 
	FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	JOIN  BH_DTC_PhanBoDuToanChi ct on ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE sXauNoiMa LIKE '9010006%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) 
	AND ctct.iNamLamViec = @INamLamViec - 1
	AND ct.bIsKhoa = 1

	UNION

	SELECT 236 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	LEFT JOIN BH_DTC_PhanBoDuToanChi ct 
	ON ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE sXauNoiMa LIKE '9010006%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 237 iMa, SUM(ctct.fTienKeHoachCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa LIKE '9010006%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 238 iMa, SUM(ctct.fTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_KPK_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_KPK ct 
	ON ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
	WHERE sXauNoiMa LIKE '9010006%' OR sXauNoiMa LIKE '9010007%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 242 iMa, SUM(ctct.fTienKeHoachCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa LIKE '9010010%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 243 iMa, SUM(ctct.fTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_KPK_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_KPK ct 
	ON ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
	WHERE sXauNoiMa LIKE '9010010-010%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 247 iMa, ISNULL(SUM(ctct.fTienTuChi), 0) - (SELECT ISNULL(SUM(ctct.fTien_ThucChi), 0) 
		FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet ctct
		JOIN BH_QTC_Nam_KinhPhiQuanLy ct on ct.ID_QTC_Nam_KinhPhiQuanLy = ctct.iID_QTC_Nam_KinhPhiQuanLy
		WHERE ctct.sXauNoiMa LIKE '9010003%' AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ctct.iNamLamViec = @INamLamViec - 1 and sM is not null and sM <> '' AND ct.bIsKhoa = 1) fSoBaoCao 
	FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	JOIN BH_DTC_PhanBoDuToanChi ct on ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE sXauNoiMa LIKE '9010003%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec - 1
	AND ct.bIsKhoa = 1

	UNION

	SELECT 248 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	LEFT JOIN BH_DTC_PhanBoDuToanChi ct 
	ON ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE sXauNoiMa LIKE '9010003%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 250 iMa, ISNULL(SUM(ctct.fTienKeHoachCap), 0) - (SELECT ISNULL(SUM(ctct.fTien_ThucChi), 0) 
		FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet ctct
		JOIN BH_QTC_Nam_KinhPhiQuanLy ct on ID_QTC_Nam_KinhPhiQuanLy = ID_QTC_Nam_KinhPhiQuanLy
		WHERE ctct.sXauNoiMa LIKE '9010003-010%' AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ctct.iNamLamViec = @INamLamViec - 1 AND ct.bIsKhoa = 1) fSoBaoCao 
	FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa LIKE '9010003%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec - 1
	AND ct.bIsKhoa = 1

	UNION

	SELECT 251 iMa, SUM(ctct.fTienKeHoachCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa LIKE '9010003%'
	AND ct.sID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 252 iMa, SUM(ctct.fTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_KinhPhiQuanLy ct 
	ON ct.ID_QTC_Nam_KinhPhiQuanLy = ctct.iID_QTC_Nam_KinhPhiQuanLy
	WHERE sXauNoiMa LIKE '9010003%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	) AS temp ON temp.iMa = dmtdqp.iMa

	LEFT JOIN BH_ThamDinhQuyetToan_ChungTuChiTiet ctct ON dmtdqp.iMa = ctct.iMa 
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ctct.iNamLamViec = @INamLamViec
	LEFT JOIN BH_ThamDinhQuyetToan_ChungTu ct 
	ON ctct.iID_BH_TDQT_ChungTu = ct.iID_BH_TDQT_ChungTu 
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ct.iNamLamViec = @INamLamViec
	LEFT JOIN (SELECT * from DonVi WHERE iNamLamViec = @INamLamViec AND iTrangThai = 1) dv ON dv.iID_MaDonVi = ctct.iID_MaDonVi) r
	GROUP BY 
	r.iID,
	r.iMa,
	r.iSTT,
	r.iMaCha,
	r.sSTT,
	r.sNoiDung,
	r.sXauNoiMa,
	r.iKieuChu,
	r.iTrangThai,
	r.iNamLamViec,
	r.sNguoiTao,
	r.sNguoiSua

	--Lấy dữ liệu chứng từ đơn vị cha
	SELECT dvc.iMa, dvc.fSoBaoCao INTO #dmtdqtCha FROM
	(
	SELECT 178 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct JOIN BH_DTC_DuToanChiTrenGiao ct ON ct.ID = ctct.iID_DTC_DuToanChiTrenGiao
	WHERE ctct.sXauNoiMa LIKE '9010001%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 179 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct JOIN BH_DTC_DuToanChiTrenGiao ct ON ct.ID = ctct.iID_DTC_DuToanChiTrenGiao
	WHERE ctct.sXauNoiMa LIKE '9010002%'
		AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
		AND ctct.iNamLamViec = @INamLamViec
		AND ct.bIsKhoa = 1

	UNION

	SELECT 218 iMa, 0.1 * (ISNULL(SUM(ctct.fThu_BHYT_NLD), 0) + ISNULL(SUM(ctct.fThu_BHYT_NSD), 0)) fSoBaoCao 
	FROM BH_DTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN  BH_DTT_BHXH_ChungTu ct 
	ON ct.iID_DTT_BHXH = ctct.iID_DTT_BHXH
	WHERE (ctct.sXauNoiMa like '9020001-010-011-0001%' OR ctct.sXauNoiMa like '9020002-010-011-0001%')
		AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
		AND ct.iNamLamViec = @INamLamViec
		AND ct.bIsKhoa = 1

	UNION

	--SELECT 220 iMa, 0.1 * (ISNULL(SUM(fThu_BHYT_NLD + fThu_BHYT_NSD), 0) - (SELECT ISNULL(SUM(ctct.fThu_BHYT_NLD + ctct.fThu_BHYT_NSD), 0) 
	--	FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	--	JOIN BH_QTT_BHXH_ChungTu ct ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	--	WHERE (ctct.sXauNoiMa LIKE '9020001-010-011-0001%' OR ctct.sXauNoiMa like '9020002-010-011-0001%') 
	--		AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) 
	--		AND ctct.iNamLamViec = @INamLamViec - 1
	--		AND ct.iQuyNam = @INamLamViec - 1
	--		AND ct.bIsKhoa = 1)) fSoBaoCao
	--FROM BH_DTT_BHXH_ChungTu_ChiTiet ctct
	--JOIN BH_DTT_BHXH_ChungTu ct ON ct.iID_DTT_BHXH = ctct.iID_DTT_BHXH
	--WHERE (ctct.sXauNoiMa LIKE '9020001-010-011-0001%' OR ctct.sXauNoiMa like '9020002-010-011-0001%')
	--AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	--AND ctct.iNamLamViec = @INamLamViec - 1
	--AND ct.bIsKhoa = 1 

	--UNION

	SELECT 228 iMa, ISNULL(SUM(fTienTuChi), 0) - (SELECT ISNULL(SUM(ctct.fTien_ThucChi), 0) 
		FROM BH_QTC_Nam_KPK_ChiTiet ctct
		JOIN BH_QTC_Nam_KPK ct ON ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
		WHERE ctct.sXauNoiMa LIKE '9010009-010%' AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ctct.iNamLamViec = @INamLamViec - 1 AND ct.bIsKhoa = 1) fSoBaoCao 
	FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct
	JOIN BH_DTC_DuToanChiTrenGiao ct ON ct.ID = ctct.iID_DTC_DuToanChiTrenGiao
	WHERE ctct.sXauNoiMa LIKE '9010009%' 
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec - 1
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 229 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct
	LEFT JOIN BH_DTC_DuToanChiTrenGiao ct 
	ON ct.ID = ctct.iID_DTC_DuToanChiTrenGiao
	WHERE 
	sXauNoiMa LIKE '9010009%' 
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 235 iMa, ISNULL(SUM(ctct.fTienTuChi), 0) - (SELECT ISNULL(SUM(ctct.fTien_ThucChi), 0) 
		FROM BH_QTC_Nam_KPK_ChiTiet ctct
		JOIN BH_QTC_Nam_KPK ct ON ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
		WHERE ctct.sXauNoiMa LIKE '9010006-010%' AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ctct.iNamLamViec = @INamLamViec - 1 AND ct.bIsKhoa = 1) fSoBaoCao 
	FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct
	JOIN BH_DTC_DuToanChiTrenGiao ct ON ct.ID = ctct.iID_DTC_DuToanChiTrenGiao
	WHERE ctct.sXauNoiMa LIKE '9010006%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec - 1
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 236 iMa, SUM(ctct.fTienTuChi) fSoBaoCao 
	FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct 
	JOIN BH_DTC_DuToanChiTrenGiao ct ON ct.ID = ctct.iID_DTC_DuToanChiTrenGiao
	WHERE ctct.sXauNoiMa LIKE '9010006%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 247 iMa, ISNULL(SUM(ctct.fTienTuChi), 0) - (SELECT ISNULL(SUM(ctct.fTien_ThucChi), 0) 
		FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet ctct
		JOIN BH_QTC_Nam_KinhPhiQuanLy ct ON ct.ID_QTC_Nam_KinhPhiQuanLy = ctct.iID_QTC_Nam_KinhPhiQuanLy
		WHERE ctct.sXauNoiMa LIKE '9010003%' AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ctct.iNamLamViec = @INamLamViec - 1 and sM is not null and sM <> '' AND ct.BIsKhoa = 1) fSoBaoCao 
	FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct
	JOIN BH_DTC_DuToanChiTrenGiao ct ON ct.ID = ctct.iID_DTC_DuToanChiTrenGiao
	WHERE sXauNoiMa LIKE '9010003%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec - 1
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 248 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct
	JOIN BH_DTC_DuToanChiTrenGiao ct ON ct.ID = ctct.iID_DTC_DuToanChiTrenGiao
	WHERE ctct.sXauNoiMa LIKE '9010003%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION
 --phu nhan phu quan khoi hach toan
	SELECT 260 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0003'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	
	UNION
	--phu nhan phu quan khoi du toan
	SELECT 259 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0003'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	) dvc

	IF @LoaiDonVi = 0
		UPDATE dmtdqtResult
		SET dmtdqtResult.fSoBaoCao = dmtdqtCha.fSoBaoCao
		FROM #dmtdqtResult AS dmtdqtResult
		JOIN #dmtdqtCha AS dmtdqtCha ON dmtdqtResult.iMa = dmtdqtCha.iMa;

	SELECT dvcl.iMa, dvcl.fSoBaoCao INTO #dmtdqtCal FROM
	(
	SELECT 61 iMa, case when (select fSoBaoCao FROM #dmtdqtResult where iMa = 3) <> 0 then (select fSoBaoCao FROM #dmtdqtResult where iMa = 60) / (select fSoBaoCao FROM #dmtdqtResult where iMa = 3) else 0 end as fSoBaoCao
	UNION
	SELECT 64 iMa, case when (select fSoBaoCao FROM #dmtdqtResult where iMa = 3) <> 0 then (select fSoBaoCao FROM #dmtdqtResult where iMa = 63) / (select fSoBaoCao FROM #dmtdqtResult where iMa = 3) else 0 end as fSoBaoCao
	UNION
	SELECT 65 iMa, ((select fSoBaoCao FROM #dmtdqtResult where iMa = 3) - (select fSoBaoCao FROM #dmtdqtResult where iMa = 62)) as fSoBaoCao
	UNION
	SELECT 85 iMa, case when (select fSoBaoCao FROM #dmtdqtResult where iMa = 67) <> 0 then (select fSoBaoCao FROM #dmtdqtResult where iMa = 84) / (select fSoBaoCao FROM #dmtdqtResult where iMa = 67) else 0 end as fSoBaoCao
	UNION
	SELECT 88 iMa, case when (select fSoBaoCao FROM #dmtdqtResult where iMa = 67) <> 0 then (select fSoBaoCao FROM #dmtdqtResult where iMa = 87) / (select fSoBaoCao FROM #dmtdqtResult where iMa = 67) else 0 end as fSoBaoCao
	UNION
	SELECT 89 iMa, ((select fSoBaoCao FROM #dmtdqtResult where iMa = 67) - (select fSoBaoCao FROM #dmtdqtResult where iMa = 86)) as fSoBaoCao
	UNION
	SELECT 125 iMa, case when (select fSoBaoCao FROM #dmtdqtResult where iMa = 91) <> 0 then (select fSoBaoCao FROM #dmtdqtResult where iMa = 124) / (select fSoBaoCao FROM #dmtdqtResult where iMa = 91) else 0 end as fSoBaoCao
	UNION
	SELECT 128 iMa, case when (select fSoBaoCao FROM #dmtdqtResult where iMa = 91) <> 0 then (select fSoBaoCao FROM #dmtdqtResult where iMa = 127) / (select fSoBaoCao FROM #dmtdqtResult where iMa = 91) else 0 end as fSoBaoCao
	UNION
	SELECT 129 iMa, ((select fSoBaoCao FROM #dmtdqtResult where iMa = 91) - (select fSoBaoCao FROM #dmtdqtResult where iMa = 126)) as fSoBaoCao
	UNION
	SELECT 145 iMa, case when (select fSoBaoCao FROM #dmtdqtResult where iMa = 131) <> 0 then (select fSoBaoCao FROM #dmtdqtResult where iMa = 144) / (select fSoBaoCao FROM #dmtdqtResult where iMa = 131) else 0 end as fSoBaoCao
	UNION
	SELECT 148 iMa, case when (select fSoBaoCao FROM #dmtdqtResult where iMa = 131) <> 0 then (select fSoBaoCao FROM #dmtdqtResult where iMa = 147) / (select fSoBaoCao FROM #dmtdqtResult where iMa = 131) else 0 end as fSoBaoCao
	UNION
	SELECT 149 iMa, ((select fSoBaoCao FROM #dmtdqtResult where iMa = 131) - (select fSoBaoCao FROM #dmtdqtResult where iMa = 146)) as fSoBaoCao
	UNION
	SELECT 153 iMa, ((select fSoBaoCao FROM #dmtdqtResult where iMa = 151) - (select fSoBaoCao FROM #dmtdqtResult where iMa = 152)) as fSoBaoCao
	UNION
	SELECT 157 iMa, ((select fSoBaoCao FROM #dmtdqtResult where iMa = 155) - (select fSoBaoCao FROM #dmtdqtResult where iMa = 156)) as fSoBaoCao
	UNION
	SELECT 161 iMa, ((select fSoBaoCao FROM #dmtdqtResult where iMa = 159) - (select fSoBaoCao FROM #dmtdqtResult where iMa = 160)) as fSoBaoCao
	UNION
	SELECT 165 iMa, ((select fSoBaoCao FROM #dmtdqtResult where iMa = 163) - (select fSoBaoCao FROM #dmtdqtResult where iMa = 164)) as fSoBaoCao
	UNION
	SELECT 169 iMa, ((select fSoBaoCao FROM #dmtdqtResult where iMa = 167) - (select fSoBaoCao FROM #dmtdqtResult where iMa = 168)) as fSoBaoCao
	UNION
	SELECT 173 iMa, ((select fSoBaoCao FROM #dmtdqtResult where iMa = 171) - (select fSoBaoCao FROM #dmtdqtResult where iMa = 172)) as fSoBaoCao
	UNION
	SELECT 202 iMa, ((select fSoBaoCao FROM #dmtdqtResult where iMa = 180) - (select fSoBaoCao FROM #dmtdqtResult where iMa = 181)) as fSoBaoCao
	UNION
	SELECT 210 iMa, ((select fSoBaoCao FROM #dmtdqtResult where iMa = 209) - (select fSoBaoCao FROM #dmtdqtResult where iMa = 206)) as fSoBaoCao
	--UNION
	--SELECT 216 iMa, ((select fSoBaoCao FROM #dmtdqtResult where iMa = 212) - (select fSoBaoCao FROM #dmtdqtResult where iMa = 215)) as fSoBaoCao
	UNION
	SELECT 224 iMa, ((select fSoBaoCao FROM #dmtdqtResult where iMa = 223) - (select fSoBaoCao FROM #dmtdqtResult where iMa = 222)) as fSoBaoCao
	UNION
	SELECT 225 iMa, ((select fSoBaoCao FROM #dmtdqtResult where iMa = 218) - (select fSoBaoCao FROM #dmtdqtResult where iMa = 223)) as fSoBaoCao
	UNION
	SELECT 232 iMa, ((select fSoBaoCao FROM #dmtdqtResult where iMa = 230) - (select fSoBaoCao FROM #dmtdqtResult where iMa = 231)) as fSoBaoCao
	UNION
	SELECT 239 iMa, ((select fSoBaoCao FROM #dmtdqtResult where iMa = 237) - (select fSoBaoCao FROM #dmtdqtResult where iMa = 238)) as fSoBaoCao
	UNION
	SELECT 240 iMa, ((select fSoBaoCao FROM #dmtdqtResult where iMa = 234) - (select fSoBaoCao FROM #dmtdqtResult where iMa = 238)) as fSoBaoCao
	UNION
	SELECT 244 iMa, ((select fSoBaoCao FROM #dmtdqtResult where iMa = 242) - (select fSoBaoCao FROM #dmtdqtResult where iMa = 243)) as fSoBaoCao
	UNION
	SELECT 253 iMa, ((select fSoBaoCao FROM #dmtdqtResult where iMa = 249) - (select fSoBaoCao FROM #dmtdqtResult where iMa = 252)) as fSoBaoCao
	UNION
	SELECT 254 iMa, ((select fSoBaoCao FROM #dmtdqtResult where iMa = 246) - (select fSoBaoCao FROM #dmtdqtResult where iMa = 252)) as fSoBaoCao
	) dvcl

	--Result
	SELECT dmtdqtResult.iID, dmtdqtResult.iMa, dmtdqtResult.iMaCha, dmtdqtResult.sSTT, dmtdqtResult.sNoiDung, dmtdqtResult.sXauNoiMa, dmtdqtResult.iKieuChu, dmtdqtResult.iTrangThai,
	dmtdqtResult.iNamLamViec, dmtdqtResult.sNguoiTao, dmtdqtResult.sNguoiSua,
	CASE WHEN dmtdqtResult.fSoBaoCao is null or dmtdqtResult.fSoBaoCao = 0 THEN dmtdqtCal.fSoBaoCao/@DonViTinh
		ELSE dmtdqtResult.fSoBaoCao/@DonViTinh
	END AS fSoBaoCao,
	dmtdqtResult.fSoThamDinh/@DonViTinh fSoThamDinh,
	dmtdqtResult.fQuanNhan/@DonViTinh fQuanNhan,
	dmtdqtResult.fCNVLDHD/@DonViTinh fCNVLDHD
	FROM #dmtdqtResult dmtdqtResult
	LEFT JOIN #dmtdqtCal dmtdqtCal ON dmtdqtResult.iMa = dmtdqtCal.iMa
	ORDER BY iSTT
	
	DROP TABLE #dmtdqt;
	DROP TABLE #dmtdqtCha;
	DROP TABLE #dmtdqtResult;
END
;
;
;
;
;
GO
;;;;
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__BH_DM_ThamD__iID__3F872ABD]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[BH_DM_ThamDinhQuyetToan] DROP CONSTRAINT [DF__BH_DM_ThamD__iID__3F872ABD]
END
GO
/****** Object:  Index [PK_BH_DM_ThamDinhQuyetToan]    Script Date: 5/9/2024 5:42:53 PM ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[BH_DM_ThamDinhQuyetToan]') AND name = N'PK_BH_DM_ThamDinhQuyetToan')
ALTER TABLE [dbo].[BH_DM_ThamDinhQuyetToan] DROP CONSTRAINT [PK_BH_DM_ThamDinhQuyetToan]
GO
/****** Object:  Table [dbo].[BH_DM_ThamDinhQuyetToan]    Script Date: 5/9/2024 5:42:53 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BH_DM_ThamDinhQuyetToan]') AND type in (N'U'))
DROP TABLE [dbo].[BH_DM_ThamDinhQuyetToan]
GO
/****** Object:  Table [dbo].[BH_DM_ThamDinhQuyetToan]    Script Date: 5/9/2024 5:42:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BH_DM_ThamDinhQuyetToan](
	[iID] [uniqueidentifier] NOT NULL,
	[iKieuChu] [int] NOT NULL,
	[iMa] [int] NULL,
	[iMaCha] [int] NULL,
	[iNamLamViec] [int] NOT NULL,
	[iTrangThai] [bit] NOT NULL,
	[sNguoiSua] [nvarchar](50) NULL,
	[sNguoiTao] [nvarchar](50) NULL,
	[sNoiDung] [nvarchar](200) NULL,
	[sSTT] [nvarchar](50) NULL,
	[sXauNoiMa] [nvarchar](max) NULL,
	[iSTT] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'f4813548-55bc-4529-828c-0c9032438de5', 3, 210, NULL, 2023, 1, N'admin', N'admin', N'Kinh phí cấp thừa (+), thiếu (-)', N'c)', NULL, 212)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'1d4130a3-019b-448d-b33e-7b6447ae6c6e', 2, 211, NULL, 2023, 1, N'admin', N'admin', N'Học sinh, sinh viên', N'1.2.', NULL, 213)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'99b98f08-8660-4df4-8aa5-f1b49a45576d', 3, 212, NULL, 2023, 1, N'admin', N'admin', N'Số kinh phí được sử dụng', N'a)', NULL, 214)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'95104f15-5460-4e40-94a7-16a1f97c29fb', 3, 213, 212, 2023, 1, N'admin', N'admin', N'Năm trước chuyển sang', N'-', NULL, 215)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'd937e479-a624-4d8c-b2e1-84a8519e8569', 3, 214, 212, 2023, 1, N'admin', N'admin', N'Được cấp trong năm', N'-', NULL, 216)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'35b75490-aff3-4919-a155-f41aef6a6e7d', 3, 215, NULL, 2023, 1, N'admin', N'admin', N'Số quyết toán', N'b)', NULL, 217)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'8dc97202-7a00-4c1e-8348-aa9dfd914fd4', 3, 216, NULL, 2023, 1, N'admin', N'admin', N'Kinh phí cấp thừa (+), thiếu (-)', N'c)', NULL, 218)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'7e6dda54-f728-4b4d-bdcd-04270cb0c7f1', 1, 217, NULL, 2023, 1, N'admin', N'admin', N'Kinh phí KCB tại quân y đơn vị (10%)', N'2.', NULL, 219)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'81799a15-c9ed-4c22-97a9-afc7e4502042', 2, 218, NULL, 2023, 1, N'admin', N'admin', N'Dự toán Bộ giao', N'2.1.', NULL, 220)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'778489ad-e747-4c49-8237-cefa59b680b4', 2, 219, NULL, 2023, 1, N'admin', N'admin', N'Tính 10% số thu', N'2.2.', NULL, 221)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'ad2b8819-edc3-4ce4-a2b1-dce6028e8480', 3, 220, 219, 2023, 1, N'admin', N'admin', N'Năm trước chuyển sang', N'-', NULL, 222)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'd38aa59f-1b71-45f1-b38e-a16ebd2cae08', 3, 221, 219, 2023, 1, N'admin', N'admin', N'Năm nay', N'-', NULL, 223)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'5fb0fe36-1e9c-4c09-8c9d-4291e351aa59', 2, 222, NULL, 2023, 1, N'admin', N'admin', N'Số kinh phí được cấp', N'2.3.', NULL, 224)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'0a612718-7e18-4865-bf44-9a7aa9e6fc6b', 2, 223, NULL, 2023, 1, N'admin', N'admin', N'Số quyết toán', N'2.4.', NULL, 225)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'3e3bce82-09f3-435b-b0a9-bf8c72f56f5f', 2, 224, NULL, 2023, 1, N'admin', N'admin', N'Kinh phí cấp thừa (+), thiếu (-)', N'2.5.', NULL, 226)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'84c354b5-e98f-4081-a8bd-4c5d0890d58c', 2, 225, NULL, 2023, 1, N'admin', N'admin', N'Dự toán (10%) chuyển năm sau', N'2.6.', NULL, 227)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'aa48bb46-c822-46cd-a8a0-50deba4ce78d', 1, 226, NULL, 2023, 1, N'admin', N'admin', N'Chi mua sắm trang thiết bị y tế', N'3.', NULL, 228)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'61d91b79-da78-4b6f-8a96-60f1ab5c12ad', 2, 227, NULL, 2023, 1, N'admin', N'admin', N'Dự toán', N'3.1.', NULL, 229)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'0ba43f59-2cca-447f-8987-ab8470a5f53a', 3, 228, 227, 2023, 1, N'admin', N'admin', N'Năm trước chuyển sang ', N'-', NULL, 230)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'0a66b1b5-a3a0-4c14-88c0-99e82c3d20be', 3, 229, 227, 2023, 1, N'admin', N'admin', N'Năm nay ', N'-', NULL, 231)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'69e9ed52-0463-4832-8bdf-c40e8ab7e060', 2, 230, NULL, 2023, 1, N'admin', N'admin', N'Số kinh phí được cấp', N'3.2.', NULL, 232)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'aac4320a-e58b-4b7e-9323-90bf4c09b548', 2, 231, NULL, 2023, 1, N'admin', N'admin', N'Số quyết toán', N'3.3.', NULL, 233)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'1c09d043-8f61-4d97-ba3f-74e88d802d20', 2, 232, NULL, 2023, 1, N'admin', N'admin', N'Kinh phí thừa (+), thiếu (-)', N'3.4', NULL, 234)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'0175fdff-50dc-4982-9542-22db465fd130', 1, 233, NULL, 2023, 1, N'admin', N'admin', N'Chi khám chữa bệnh tại TS - DK', N'4.', NULL, 235)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'3a857e14-b78f-46f5-8522-2b8b664582b3', 2, 234, NULL, 2023, 1, N'admin', N'admin', N'Dự toán', N'4.1.', NULL, 236)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'54cbda4f-e4c1-4795-b385-1395e3f40e34', 3, 235, 234, 2023, 1, N'admin', N'admin', N'Năm trước chuyển sang ', N'- ', NULL, 237)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'255bca64-be6d-409c-a7bc-34a3795c94ab', 3, 236, 234, 2023, 1, N'admin', N'admin', N'Năm nay ', N'-', NULL, 238)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'34dbd63c-643c-4229-9324-ba3fe44058b6', 2, 237, NULL, 2023, 1, N'admin', N'admin', N'Số kinh phí được cấp', N'4.2.', NULL, 239)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'a6a24ea0-b5eb-4123-ac44-e2a38aee5aaa', 2, 238, NULL, 2023, 1, N'admin', N'admin', N'Số quyết toán', N'4.3.', NULL, 240)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'e36999bb-078d-4939-8849-5193cef6c0cd', 2, 239, NULL, 2023, 1, N'admin', N'admin', N'Kinh phí thừa (+), thiếu (-)', N'4.4.', NULL, 241)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'60f90cef-8b92-487f-84ec-c68865c48354', 2, 240, NULL, 2023, 1, N'admin', N'admin', N'Dự toán chuyển năm sau', N'4.5.', NULL, 242)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'9873ffa8-eb19-4b53-a290-a29cca2f279b', 1, 241, NULL, 2023, 1, N'admin', N'admin', N'CHI BẢO HIỂM THẤT NGHIỆP', N'III.', NULL, 243)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'2142b60a-8252-477b-8ca5-53306286ec82', 3, 242, NULL, 2023, 1, N'admin', N'admin', N'Số kinh phí được cấp', N'1.', NULL, 244)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'bd5492ae-ae8c-4d98-b485-201a9f3a8e0c', 3, 243, NULL, 2023, 1, N'admin', N'admin', N'Số quyết toán', N'2.', NULL, 245)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'1f5d1e5e-c693-42cf-9b39-023104383fcb', 3, 244, NULL, 2023, 1, N'admin', N'admin', N'Kinh phí thừa (+), thiếu (-)', N'3.', NULL, 246)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'891da486-be8b-46bd-b920-e565d311a278', 1, 245, NULL, 2023, 1, N'admin', N'admin', N'KINH PHÍ QUẢN LÝ BHXH, BHYT', N'IV. ', NULL, 247)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'5bb4d2ca-cf09-4a8c-b582-0d38ec69463f', 1, 246, NULL, 2023, 1, N'admin', N'admin', N'Dự toán', N'1.', NULL, 248)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'7f7ab121-152e-4e0a-81af-10c860866bc4', 2, 247, 246, 2023, 1, N'admin', N'admin', N'Năm trước chuyển sang', N'1.1.', NULL, 249)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'b5304a63-c47a-499c-a911-0bf4170500d7', 2, 248, 246, 2023, 1, N'admin', N'admin', N'Năm nay', N'1.2.', NULL, 250)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'aaad2dad-3674-4b74-bb5e-49d3ed3abc66', 1, 249, NULL, 2023, 1, N'admin', N'admin', N'Số kinh phí được cấp', N'2.', NULL, 251)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'b09686d6-3173-494d-8847-528ca63dfbba', 2, 250, 249, 2023, 1, N'admin', N'admin', N'Năm trước chuyển sang', N'2.1', NULL, 252)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'9fd44425-0389-4aa2-9837-570667967d80', 2, 251, 249, 2023, 1, N'admin', N'admin', N'Năm nay ', N'2.2.', NULL, 253)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'c1e7252e-aa49-4875-acb7-e7b2c3aba715', 3, 252, NULL, 2023, 1, N'admin', N'admin', N'Số quyết toán', N'3.', NULL, 254)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'87c00cc1-1708-426a-b440-d477edce1fd6', 3, 96, 94, 2024, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL, 98)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'72fc45cb-2c71-4d81-bca5-445969ee1c18', 3, 97, 93, 2024, 1, N'admin', N'admin', N'Truy thu', N'a2.', NULL, 99)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'17ac9d06-faac-4397-ad86-a7d2a0327ba7', 3, 98, 97, 2024, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL, 100)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'a3013e61-adbf-4e5e-9d62-63e790c47c40', 3, 99, 97, 2024, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL, 101)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'cce8653b-40bc-4b58-8bb3-b620d22f9465', 2, 100, 92, 2024, 1, N'admin', N'admin', N'Người sử dụng lao động', N'b.', NULL, 102)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'8a56e818-e96e-4758-bb45-98cce5bad687', 3, 101, 100, 2024, 1, N'admin', N'admin', N'Trong năm', N'b.1', NULL, 103)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'4ae063ad-0456-49da-be6f-dbb771c5e0e2', 3, 102, 101, 2024, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL, 104)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'3391944f-8d74-4518-bb59-6ef64b82023b', 3, 103, 101, 2024, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL, 105)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'f9b4aa43-13c5-492e-b18a-e95d84be0bf8', 3, 104, 100, 2024, 1, N'admin', N'admin', N'Truy thu', N'b.2.', NULL, 106)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'4e14f7c0-47fe-4fc4-8890-bc9175966e81', 3, 105, 104, 2024, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL, 107)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'5015ef89-b18a-4021-b48b-06b25cf44029', 3, 106, 104, 2024, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL, 108)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'ac0d5fb2-bfaa-4873-8ed9-7e4362700515', 1, 107, NULL, 2024, 1, N'admin', N'admin', N'Khối hạch toán', N'1.2.', NULL, 109)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'6d4f57a2-65d0-4566-bffe-6f337da883cd', 2, 108, 107, 2024, 1, N'admin', N'admin', N'Người lao động', N'a.', NULL, 110)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'74715d81-4361-4da3-9aa8-f2cc634ce61b', 3, 109, 108, 2024, 1, N'admin', N'admin', N'Trong năm', N'a.1.', NULL, 111)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'd5d76f45-7244-4672-af4c-8ef200ea898e', 3, 110, 109, 2024, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL, 112)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'e09ac71b-4004-4fdb-a5dd-7e83ea3e21a7', 3, 111, 109, 2024, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL, 113)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'51bf7e39-51e7-4cf5-99b8-6fee711bcf08', 3, 112, 108, 2024, 1, N'admin', N'admin', N'Truy thu', N'a2.', NULL, 114)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'1ba48b28-7373-4c19-a59b-ef5a03b888c5', 3, 113, 112, 2024, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL, 115)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'635e9f9a-da4a-44dd-9446-a24ca9fc5905', 3, 114, 112, 2024, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL, 116)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'5170d6a4-b529-41d8-9792-4493c30d008e', 2, 115, 107, 2024, 1, N'admin', N'admin', N'Người sử dụng lao động', N'b.', NULL, 117)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'c710b46e-64b6-4e41-b183-a1a63321c651', 3, 116, 115, 2024, 1, N'admin', N'admin', N'Trong năm', N'b.1.', NULL, 118)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'fb6b57f6-87a1-4714-9a76-f72ec8ecdbc2', 3, 117, 116, 2024, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL, 119)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'f055f2e4-9124-44da-b4b6-001922c81375', 3, 118, 116, 2024, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL, 120)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'6fcdb994-904f-469f-a697-98fb4df20890', 3, 119, 115, 2024, 1, N'admin', N'admin', N'Truy thu', N'b.2.', NULL, 121)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'f4bd1a73-2adc-4fbc-920c-8ab740aef483', 3, 120, 119, 2024, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL, 122)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'84e228c2-d994-42e1-8dec-1688e054d1fa', 3, 121, 119, 2024, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL, 123)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'13ea74a8-f435-4c4b-b14b-b02381ea80fa', 1, 122, NULL, 2024, 1, N'admin', N'admin', N'Số đã nộp', N'2.', NULL, 124)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'af83936e-7072-41a9-8724-fda139856c20', 2, 123, NULL, 2024, 1, N'admin', N'admin', N'Đến ngày 31/12/….', N'-', NULL, 125)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'68599d8e-6bc5-4c98-9cf5-6920e3d0701b', 3, 124, NULL, 2024, 1, N'admin', N'admin', N'Số tiền', N'+', NULL, 126)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'f9cc0921-dd23-4f5d-82c0-738f5889f64e', 3, 125, NULL, 2024, 1, N'admin', N'admin', N'Tỷ lệ so với số phải nộp (%)', N'+', NULL, 127)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'd6d728c6-9503-4790-88d8-940eab00fef1', 2, 126, NULL, 2024, 1, N'admin', N'admin', N'Đến ngày thẩm định', N'-', NULL, 128)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'459d7e29-fdfd-4667-a65d-0034fd34e9fe', 3, 127, NULL, 2024, 1, N'admin', N'admin', N'Số tiền', N'+', NULL, 129)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'2826de5c-62f4-4e94-a98f-060185474692', 3, 128, NULL, 2024, 1, N'admin', N'admin', N'Tỷ lệ so với số phải nộp (%)', N'+', NULL, 130)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'bf79813f-9c49-43df-b89a-46770a83cbde', 1, 129, NULL, 2024, 1, N'admin', N'admin', N'Số nộp thừa (+), thiếu (-)', N'3.', NULL, 131)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'2f53ccfd-aa43-4dde-85db-ebf7dbce0c3b', 1, 130, NULL, 2024, 1, N'admin', N'admin', N'BẢO HIỂM Y TẾ QUÂN NHÂN', N'IV. ', NULL, 132)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'671f218d-0026-4445-b84b-a09774b41d2c', 1, 131, NULL, 2024, 1, N'admin', N'admin', N'Số phải nộp', N'1.', NULL, 133)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'e5324a68-3064-4280-a447-4f4c31d1b39e', 1, 132, 131, 2024, 1, N'admin', N'admin', N'Khối dự toán', N'1.1.', NULL, 134)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'e59d6e30-d35c-4baf-9f9f-4534dff7ec02', 3, 133, 132, 2024, 1, N'admin', N'admin', N'Sĩ quan', N'-', NULL, 135)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'0f52d64f-5835-4851-9c87-1ee108dd14b8', 3, 134, 132, 2024, 1, N'admin', N'admin', N'QNCN', N'-', NULL, 136)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'c4fee7f9-ec9a-436d-aa26-e4b60f822402', 3, 135, 132, 2024, 1, N'admin', N'admin', N'HSQ, BS', N'-', NULL, 137)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'98f1167e-5f0e-4bb2-8ba1-b85423fad450', 3, 136, 132, 2024, 1, N'admin', N'admin', N'Truy thu', N'-', NULL, 138)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'8ba0b60f-6c38-4922-90f3-71307b29304a', 1, 137, 131, 2024, 1, N'admin', N'admin', N'Khối hạch toán', N'1.2.', NULL, 139)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'93644242-5778-4b5d-9432-8c03ae7cde5a', 3, 138, 137, 2024, 1, N'admin', N'admin', N'Sĩ quan', N'-', NULL, 140)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'fa61a8e7-3739-4422-a4b6-b41045ee120a', 3, 139, 137, 2024, 1, N'admin', N'admin', N'QNCN', N'-', NULL, 141)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'1313661b-dc6f-4f8c-946f-9d301f822fad', 3, 140, 137, 2024, 1, N'admin', N'admin', N'HSQ, BS', N'-', NULL, 142)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'79a6161f-d27d-42b2-a611-fda580455072', 3, 141, 137, 2024, 1, N'admin', N'admin', N'Truy thu', N'-', NULL, 143)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'5a4f1ede-1ed3-4c1d-88a7-2f0f60e76784', 1, 142, NULL, 2024, 1, N'admin', N'admin', N'Số đã nộp', N'2.', NULL, 144)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'fa4b145d-7074-4734-8661-54f930c9e958', 2, 143, NULL, 2024, 1, N'admin', N'admin', N'Đến ngày 31/12/….', N'-', NULL, 145)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'47c45a4a-4ba8-491f-a5ec-efef02fc6802', 3, 144, NULL, 2024, 1, N'admin', N'admin', N'Số tiền', N'+', NULL, 146)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'057896d1-a5df-4d39-9aed-1022d5bb2a99', 3, 145, NULL, 2024, 1, N'admin', N'admin', N'Tỷ lệ so với số phải nộp (%)', N'+', NULL, 147)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'79655f3d-2f4a-43d5-9287-34697508bff9', 2, 146, NULL, 2024, 1, N'admin', N'admin', N'Đến ngày thẩm định', N'-', NULL, 148)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'70f643ba-7111-46c3-81ec-4308f1451ed2', 3, 147, NULL, 2024, 1, N'admin', N'admin', N'Số tiền', N'+', NULL, 149)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'5e1cfa63-5da1-48bf-a1bd-60589b74abb7', 1, 162, NULL, 2023, 1, N'admin', N'admin', N'BẢO HIỂM Y TẾ HS, SV', N'VIII.', NULL, 164)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'279883f4-1875-4366-9f5b-81564c5128b8', 3, 163, NULL, 2023, 1, N'admin', N'admin', N'Số phải nộp', N'1.', NULL, 165)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'083a4c23-e7f3-46f7-b0f6-1a0d0f0df860', 3, 164, NULL, 2023, 1, N'admin', N'admin', N'Số đã nộp', N'2.', NULL, 166)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'a116c3b2-2a49-445b-97a8-27c9db1d0c58', 3, 165, NULL, 2023, 1, N'admin', N'admin', N'Số đã nộp thừa (+), thiếu (-)', N'3.', NULL, 167)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'2d3740f9-b74d-4d76-9b51-f8b66a1e88df', 1, 166, NULL, 2023, 1, N'admin', N'admin', N'BHYT Lưu học sinh', N'IX.', NULL, 168)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'3cca05bf-1a36-4685-a4e8-4f053604df52', 3, 167, NULL, 2023, 1, N'admin', N'admin', N'Số phải nộp', N'1.', NULL, 169)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'e83a174f-38b7-4bdc-a468-daaea85d3b24', 3, 168, NULL, 2023, 1, N'admin', N'admin', N'Số đã nộp', N'2.', NULL, 170)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'65f8dae6-9381-4692-97a3-d2804ee01003', 3, 169, NULL, 2023, 1, N'admin', N'admin', N'Số đã nộp thừa (+), thiếu (-)', N'3.', NULL, 171)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'613e060f-61f0-49f0-96a7-ff2cb01fe767', 1, 170, NULL, 2023, 1, N'admin', N'admin', N'BHYT Sĩ quan dự bị', N'X.', NULL, 172)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'b0956214-18ec-4221-a914-37d9a2be2802', 3, 171, NULL, 2023, 1, N'admin', N'admin', N'Số phải nộp', N'1.', NULL, 173)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'3c7adb02-b61e-482e-b14b-21a42dbd8d09', 3, 172, NULL, 2023, 1, N'admin', N'admin', N'Số đã nộp', N'2.', NULL, 174)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'e4e8125b-2e59-4844-ac08-a2ef44f5a965', 3, 173, NULL, 2023, 1, N'admin', N'admin', N'Số đã nộp thừa (+), thiếu (-)', N'3.', NULL, 175)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'09d00a1d-0bd2-4d80-a35b-7a05a5928c5d', 1, 174, NULL, 2023, 1, N'admin', N'admin', N'BHYT CÁC ĐỐI TƯỢNG KHÁC (nếu có)', N'XI.', NULL, 176)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'341723af-f5ac-48d7-aaad-783b7d9a5576', 1, 175, NULL, 2023, 1, N'admin', N'admin', N'CHI BẢO HIỂM XÃ HỘI, BẢO HIỂM Y TẾ', N'B.', NULL, 177)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'a8e121ac-8a46-4113-b256-3bade62434f4', 1, 176, NULL, 2023, 1, N'admin', N'admin', N'CHI CÁC CHẾ ĐỘ BẢO HIỂM XÃ HỘI', N'I.', NULL, 178)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'd5cea85c-df2a-4953-b0e1-daecb713dc51', 1, 177, NULL, 2023, 1, N'admin', N'admin', N'Dự toán', N'1.', NULL, 179)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'd85404c9-ffca-4a86-9f7d-b729ed42d023', 3, 178, 177, 2023, 1, N'admin', N'admin', N'Khối dự toán', N'-', NULL, 180)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'1c2ba237-339f-4f8e-9179-49cb5c0395aa', 3, 179, 177, 2023, 1, N'admin', N'admin', N'Khối hạch toán', N'-', NULL, 181)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'b0aab09a-7ed4-4b91-bf44-066a624e826d', 1, 180, NULL, 2023, 1, N'admin', N'admin', N'Số cấp kinh phí', N'2.', NULL, 182)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'305e3ddc-d05b-4704-adf7-ecc5ee50e99d', 1, 181, NULL, 2023, 1, N'admin', N'admin', N'Số quyết toán', N'3.', NULL, 183)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'bf895110-ea64-4e91-86fa-4ad948ad39fa', 2, 182, 181, 2023, 1, N'admin', N'admin', N'Khối dự toán', N'3.1.', NULL, 184)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'f95efcde-c5e4-4841-b907-935b2ecf5804', 3, 183, 182, 2023, 1, N'admin', N'admin', N'Trợ cấp ốm đau', N'-', NULL, 185)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'70ddfc1b-2a42-465b-b9a4-4562b2eb2a22', 3, 184, 182, 2023, 1, N'admin', N'admin', N'Trợ cấp thai sản', N'-', NULL, 186)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'86af35b0-3339-4271-a4bb-fcb67a5ba556', 3, 185, 182, 2023, 1, N'admin', N'admin', N'Trợ cấp TNLĐ, BNN', N'-', NULL, 187)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'da5dd1f1-b303-48c6-b66b-998d88eb8281', 3, 186, 182, 2023, 1, N'admin', N'admin', N'Trợ cấp hưu trí', N'-', NULL, 188)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'0abae97c-d745-4a1d-959f-1d0b375a0b50', 3, 187, 182, 2023, 1, N'admin', N'admin', N'Trợ cấp phục viên', N'-', NULL, 189)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'd25e3f89-9f4c-4ce5-a1c0-f48e09587870', 3, 188, 182, 2023, 1, N'admin', N'admin', N'Trợ cấp xuất ngũ', N'-', NULL, 190)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'23dac86a-791d-4188-8623-67b043815392', 3, 189, 182, 2023, 1, N'admin', N'admin', N'Trợ cấp thôi việc', N'-', NULL, 191)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'd92d15dc-4f50-486e-a104-1071b0bc2267', 3, 190, 182, 2023, 1, N'admin', N'admin', N'Trợ cấp tử tuất', N'-', NULL, 192)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'ea3549bf-d19e-4e0b-91fd-f39dd48f5c18', 2, 192, 181, 2023, 1, N'admin', N'admin', N'Khối hạch toán', N'3.2.', NULL, 194)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'b27ff0d7-c7c1-453d-b154-81a0adf8d669', 3, 193, 192, 2023, 1, N'admin', N'admin', N'Trợ cấp ốm đau', N'-', NULL, 195)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'7d4f643a-cc34-41e6-9cd1-3e3e1111b624', 3, 194, 192, 2023, 1, N'admin', N'admin', N'Trợ cấp thai sản', N'-', NULL, 196)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'7b2b246a-b396-40cb-ade4-04c0244d3c76', 3, 195, 192, 2023, 1, N'admin', N'admin', N'Trợ cấp TNLĐ, BNN', N'-', NULL, 197)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'ddeba7b6-022a-4271-9214-4b502a158765', 3, 196, 192, 2023, 1, N'admin', N'admin', N'Trợ cấp hưu trí', N'-', NULL, 198)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'3f5d9565-da6c-45ec-9326-f7273c6447f0', 3, 197, 192, 2023, 1, N'admin', N'admin', N'Trợ cấp phục viên', N'-', NULL, 199)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'9c471147-9fe6-4815-8834-21c7a29acd99', 3, 198, 192, 2023, 1, N'admin', N'admin', N'Trợ cấp xuất ngũ', N'-', NULL, 200)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'ff6aa4fe-4413-4a3c-8b40-eedec5a4042b', 3, 199, 192, 2023, 1, N'admin', N'admin', N'Trợ cấp thôi việc', N'-', NULL, 201)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'3efc6e11-e973-4167-b65b-b0228c67dd2f', 3, 200, 192, 2023, 1, N'admin', N'admin', N'Trợ cấp tử tuất', N'-', NULL, 202)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'3a923e15-9164-4425-bca5-cd6344159fb5', 1, 202, NULL, 2023, 1, N'admin', N'admin', N'Kinh phí cấp thừa (+), thiếu (-)', N'4.', NULL, 204)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'23cb1ff9-87b8-4523-ab36-59b2d83a5217', 1, 203, NULL, 2023, 1, N'admin', N'admin', N'CHI BẢO HIỂM Y TẾ', N'II.', NULL, 205)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'486bb6bd-31c3-4269-bf56-e65b7c833cce', 1, 204, NULL, 2023, 1, N'admin', N'admin', N'Chăm sóc sức khỏe ban đầu', N'1.', NULL, 206)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'29460366-ef3c-4d95-baaf-d770ae48fd37', 2, 205, NULL, 2023, 1, N'admin', N'admin', N'Người lao động', N'1.1.', NULL, 207)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'e814021b-7ab9-4edc-9bdb-1a7aff7c59ac', 3, 206, NULL, 2023, 1, N'admin', N'admin', N'Số kinh phí được sử dụng', N'a)', NULL, 208)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'545bba9f-c3c5-490c-8881-400c29d35fc9', 3, 207, 206, 2023, 1, N'admin', N'admin', N'Năm trước chuyển sang', N'-', NULL, 209)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'34e4d295-10c1-4228-9945-f822e8cd1d1b', 3, 208, 206, 2023, 1, N'admin', N'admin', N'Được cấp trong năm', N'-', NULL, 210)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'b8391488-f831-4031-842d-f876df5bb725', 3, 209, NULL, 2023, 1, N'admin', N'admin', N'Số quyết toán', N'b)', NULL, 211)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'e59dd50f-5a65-40c0-bc02-46a249166ef5', 3, 259, 6, 2024, 1, N'admin', N'admin', N'Phu nhân, phu quân', N'-', NULL, 9)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'5c7e7d70-e770-43b6-9916-1df2e89b394f', 3, 260, 33, 2024, 1, N'admin', N'admin', N'Phu nhân, phu quân', N'-', NULL, 37)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'b54dff89-af01-4c38-bfb9-6edd75e53846', 3, 253, NULL, 2023, 1, N'admin', N'admin', N'Kinh phí cấp thừa (+), thiếu (-)', N'4.', NULL, 255)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'f0a37e93-b6e4-481c-9852-d2e83fbe1823', 3, 254, NULL, 2023, 1, N'admin', N'admin', N'Dự toán chuyển năm sau', N'5.', NULL, 256)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'41a9e0d1-7238-484c-9796-7e74ce8c3ab1', 1, 255, NULL, 2023, 1, N'admin', N'admin', N'XÁC ĐỊNH CƠ SỞ TÍNH NỘP BHYT TỪ QUỸ BHXH', N'C.', NULL, 257)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'5a12c326-71c6-4f09-956c-0f0d2cd904ea', 3, 256, NULL, 2023, 1, N'admin', N'admin', N'Số tháng hưởng trợ cấp ốm đau thuộc danh mục bệnh cần chữa trị dài ngày không tính đóng BHXH', N'1.', NULL, 258)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'37c4e2b2-42a6-436b-b29d-f2a9c37edf52', 3, 257, NULL, 2023, 1, N'admin', N'admin', N'Số tiền trợ cấp sinh con và nuôi con nuôi', N'2.', NULL, 259)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'a882a1bc-61c0-4882-a048-bafa2ae82108', 1, 258, NULL, 2023, 1, N'admin', N'admin', N'GIẢI THÍCH SỐ LIỆU CHÊNH LỆCH (nếu có)', N'D.', NULL, 260)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'c53e4915-cc33-4a74-938c-e818351f0f0c', 1, 1, NULL, 2024, 1, N'admin', N'admin', N'THU, NỘP BHXH, BHTN, BHYT', N'A.', NULL, 1)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'e8a29103-27c5-4230-b5fc-7fb07d47671a', 1, 2, NULL, 2024, 1, N'admin', N'admin', N'BẢO HIỂM XÃ HỘI', N'I.', NULL, 2)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'8e7155ef-9bb8-4f49-bad1-1d82ccbaf0cd', 1, 3, NULL, 2024, 1, N'admin', N'admin', N'Số phải nộp', N'1.', NULL, 3)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'd6996df4-b40c-453b-b1d3-ba6f84541b99', 1, 4, 3, 2024, 1, N'admin', N'admin', N'Khối dự toán', N'1.1.', NULL, 4)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'9bff9fa8-bd70-4a88-b200-78be54c87087', 2, 5, 4, 2024, 1, N'admin', N'admin', N'Người lao động', N'a.', NULL, 5)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'dfecc10a-6a50-41b5-8edc-eb9c8896844a', 3, 6, 5, 2024, 1, N'admin', N'admin', N'Trong năm', N'a.1.', NULL, 6)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'3d844721-69fc-400e-ba72-228f4a1fe9f1', 3, 7, 6, 2024, 1, N'admin', N'admin', N'Sĩ quan', N'-', NULL, 7)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'd30abfd4-f837-4841-b89b-a99e416f7459', 3, 8, 6, 2024, 1, N'admin', N'admin', N'QNCN', N'-', NULL, 8)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'3867bb47-47cc-4d34-ada2-8d31a3eef1c4', 3, 9, 6, 2024, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL, 10)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'278021ab-a014-4f30-9d1d-0e1bf8737883', 3, 10, 6, 2024, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL, 11)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'48f5e9be-fa4d-49bf-8b2a-1bc1bc2ab9b8', 3, 11, 5, 2024, 1, N'admin', N'admin', N'Truy thu', N'a2.', NULL, 12)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'1215533d-3336-4054-a8d0-2130fdc76d0c', 3, 12, 11, 2024, 1, N'admin', N'admin', N'Sĩ quan', N'-', NULL, 13)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'bd63ca9c-430f-415d-a069-e33e2fc03913', 3, 13, 11, 2024, 1, N'admin', N'admin', N'QNCN', N'-', NULL, 14)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'495c663d-dacb-42fc-9d50-a03552c3ab12', 3, 14, 11, 2024, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL, 15)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'b51685af-6a57-44ab-a2de-d4469ad2d3b4', 3, 15, 11, 2024, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL, 16)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'5d12adec-946c-459a-b389-e70265498b95', 2, 16, 4, 2024, 1, N'admin', N'admin', N'Người sử dụng lao động', N'b.', NULL, 17)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'b099a908-e1a4-40f8-a52f-f84b80c779fd', 3, 17, 16, 2024, 1, N'admin', N'admin', N'Người hưởng lương', N'b.1.', NULL, 18)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'976de8f2-f1f9-4ee5-9ed5-eea3f4630f07', 3, 18, 17, 2024, 1, N'admin', N'admin', N'Trong năm', N'*', NULL, 19)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'1a98082e-0c2d-4f37-84ee-c829469c30f8', 3, 19, 18, 2024, 1, N'admin', N'admin', N'Sĩ quan', N'-', NULL, 20)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'f37db09d-5538-4b62-94c1-04cc21e05d82', 3, 20, 18, 2024, 1, N'admin', N'admin', N'QNCN', N'-', NULL, 21)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'320aa068-0626-4766-a104-4ae9ba80d812', 3, 21, 18, 2024, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL, 22)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'a16e03bd-8444-4cc1-ac4f-a358a1ece5e3', 3, 22, 18, 2024, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL, 23)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'ac26d384-11b4-491f-b252-1ac48b0dbf23', 3, 23, 17, 2024, 1, N'admin', N'admin', N'Truy thu', N'*', NULL, 24)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'd1925f4b-cb89-4f86-9c8a-404fb0f8d02c', 3, 24, 23, 2024, 1, N'admin', N'admin', N'Sĩ quan', N'-', NULL, 25)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'e3e1b716-b081-4159-94dc-89eb2764fb22', 3, 25, 23, 2024, 1, N'admin', N'admin', N'QNCN', N'-', NULL, 26)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'e41cc31a-ae43-45cb-900b-de234dd1cdce', 3, 26, 23, 2024, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL, 27)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'6898ceb0-14bc-484d-969a-cb07f6f2bc1b', 3, 27, 23, 2024, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL, 28)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'b2a4a4aa-cb45-47c2-8a5c-9e6687667882', 3, 28, 16, 2024, 1, N'admin', N'admin', N'Người hưởng PC (HSQ, BS)', N'b.2.', NULL, 29)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'dc0a58c7-c702-4cc2-864f-990493464fa5', 3, 29, 28, 2024, 1, N'admin', N'admin', N'Trong năm', N'-', NULL, 30)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'57ba56eb-f424-4a97-bd20-8f216d10d83a', 3, 30, 28, 2024, 1, N'admin', N'admin', N'Truy thu', N'-', NULL, 31)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'4fffff82-34e3-4209-84b6-d8c473a9d29f', 1, 31, 3, 2024, 1, N'admin', N'admin', N'Khối hạch toán', N'1.2.', NULL, 32)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'd71035a3-ffcc-4e7f-ace5-1adfc6acd8cc', 2, 32, 31, 2024, 1, N'admin', N'admin', N'Người lao động', N'a.', NULL, 33)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'a5824961-9c42-469e-88ab-b9238fb6db0f', 3, 33, 32, 2024, 1, N'admin', N'admin', N'Trong năm', N'a.1.', NULL, 34)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'674363d4-1ead-4971-a7f1-baef57fe5b7c', 3, 34, 33, 2024, 1, N'admin', N'admin', N'Sĩ quan', N'-', NULL, 35)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'ada6e7db-e83d-42ee-9714-f0ef61ac8941', 3, 35, 33, 2024, 1, N'admin', N'admin', N'QNCN', N'-', NULL, 36)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'28486bd7-daf1-43cb-9555-0828fcd559d2', 3, 36, 33, 2024, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL, 38)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'b4c0a910-a558-4019-986c-4013348286a4', 3, 37, 33, 2024, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL, 39)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'9a5362de-551c-469c-8c1d-5f3bba3e7f25', 3, 38, 32, 2024, 1, N'admin', N'admin', N'Truy thu', N'a2.', NULL, 40)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'1397a25b-2cb3-4f1d-bbd2-5d83e0c4780b', 3, 39, 38, 2024, 1, N'admin', N'admin', N'Sĩ quan', N'-', NULL, 41)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'227c2898-bc98-436c-acab-2b531b0ad131', 3, 40, 38, 2024, 1, N'admin', N'admin', N'QNCN', N'-', NULL, 42)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'ddb0dc3d-15f4-4132-8ced-5341554debc9', 3, 41, 38, 2024, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL, 43)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'0fbb5e72-8052-4ecf-8933-373008780d62', 3, 42, 38, 2024, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL, 44)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'a218da60-a5b5-4002-90e3-203e23246fd5', 2, 43, 31, 2024, 1, N'admin', N'admin', N'Người sử dụng lao động', N'b.', NULL, 45)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'9381a691-c31c-4746-b401-4ab5f1893652', 3, 44, 43, 2024, 1, N'admin', N'admin', N'Người hưởng lương', N'b.1', NULL, 46)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'9303ff9f-10eb-4c53-b921-0bfc67776866', 3, 259, 6, 2023, 1, N'admin', N'admin', N'Phu nhân, phu quân', N'-', NULL, 9)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'0940b8e8-0461-40ce-8e59-c85d1467d63d', 3, 260, 33, 2023, 1, N'admin', N'admin', N'Phu nhân, phu quân', N'-', NULL, 37)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'daeea9d9-82a2-4b80-833e-774ee6f5b35d', 3, 114, 112, 2023, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL, 116)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'f926125a-b4ac-43f7-b6f9-3be9a35fe93d', 2, 115, 107, 2023, 1, N'admin', N'admin', N'Người sử dụng lao động', N'b.', NULL, 117)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'd1dbce07-052a-41f9-a6a2-5e17a7030971', 3, 116, 115, 2023, 1, N'admin', N'admin', N'Trong năm', N'b.1.', NULL, 118)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'08b1db99-32db-4ffc-a494-38a064d976cd', 3, 117, 116, 2023, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL, 119)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'03e068ec-e129-4a85-917d-b552e689c500', 3, 118, 116, 2023, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL, 120)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'6fe70e21-f2d1-4467-995e-c7bdf2ffefee', 3, 119, 115, 2023, 1, N'admin', N'admin', N'Truy thu', N'b.2.', NULL, 121)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'b84d65ec-b4e6-4508-95df-ad8e381843e8', 3, 120, 119, 2023, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL, 122)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'6e4052e0-b35a-46d6-b980-851396bc35d0', 3, 121, 119, 2023, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL, 123)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'425f43a3-8ed7-45be-b79c-77015ac64395', 1, 122, NULL, 2023, 1, N'admin', N'admin', N'Số đã nộp', N'2.', NULL, 124)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'd3549b69-b4d8-49e0-a796-f486e0fd4d33', 2, 123, NULL, 2023, 1, N'admin', N'admin', N'Đến ngày 31/12/….', N'-', NULL, 125)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'2717752d-3565-4589-a752-369b8ece378d', 3, 124, NULL, 2023, 1, N'admin', N'admin', N'Số tiền', N'+', NULL, 126)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'f256b07e-70a4-4342-96f4-f1be75c20671', 3, 125, NULL, 2023, 1, N'admin', N'admin', N'Tỷ lệ so với số phải nộp (%)', N'+', NULL, 127)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'9ce1f167-ae0a-426b-a5b3-3bb507f9e120', 2, 126, NULL, 2023, 1, N'admin', N'admin', N'Đến ngày thẩm định', N'-', NULL, 128)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'584a09d3-f76b-4d62-ac7a-9b48111f2455', 3, 127, NULL, 2023, 1, N'admin', N'admin', N'Số tiền', N'+', NULL, 129)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'aa41a6b0-24f9-4dd1-bf29-d1414d670c3b', 3, 128, NULL, 2023, 1, N'admin', N'admin', N'Tỷ lệ so với số phải nộp (%)', N'+', NULL, 130)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'25d6ab1a-9f55-477c-a8b8-0c67da293f76', 1, 129, NULL, 2023, 1, N'admin', N'admin', N'Số nộp thừa (+), thiếu (-)', N'3.', NULL, 131)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'13641572-6542-40a9-8917-c38b4fd879c1', 1, 130, NULL, 2023, 1, N'admin', N'admin', N'BẢO HIỂM Y TẾ QUÂN NHÂN', N'IV. ', NULL, 132)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'1bd25fc7-8ebc-4f82-b723-41b75189cbed', 1, 131, NULL, 2023, 1, N'admin', N'admin', N'Số phải nộp', N'1.', NULL, 133)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'b4094dcd-d1fd-415f-a3f2-1de7a1261967', 1, 132, 131, 2023, 1, N'admin', N'admin', N'Khối dự toán', N'1.1.', NULL, 134)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'd4a74db7-1ac9-4906-95ed-4c4bf234196d', 3, 133, 132, 2023, 1, N'admin', N'admin', N'Sĩ quan', N'-', NULL, 135)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'2c8d5f6b-1c97-462a-bb50-28bd0feeacb6', 3, 134, 132, 2023, 1, N'admin', N'admin', N'QNCN', N'-', NULL, 136)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'723fe616-5470-4100-8901-61e83ae7aafa', 3, 135, 132, 2023, 1, N'admin', N'admin', N'HSQ, BS', N'-', NULL, 137)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'e865e8c8-e1ad-4281-9a90-d7280ec4eab4', 3, 136, 132, 2023, 1, N'admin', N'admin', N'Truy thu', N'-', NULL, 138)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'ec5a0bdf-73df-4937-ba7f-77d0dd0ad178', 1, 137, 131, 2023, 1, N'admin', N'admin', N'Khối hạch toán', N'1.2.', NULL, 139)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'b6c2eebd-e4af-42dc-bcf2-96382d1df85c', 3, 138, 137, 2023, 1, N'admin', N'admin', N'Sĩ quan', N'-', NULL, 140)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'bd1e50b3-4aa1-4f1a-9502-09ecbf1b4f28', 3, 139, 137, 2023, 1, N'admin', N'admin', N'QNCN', N'-', NULL, 141)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'496d86c4-996e-4a78-8e38-e917e57124ab', 3, 140, 137, 2023, 1, N'admin', N'admin', N'HSQ, BS', N'-', NULL, 142)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'0eb4bfa5-959a-46c4-9b10-ad4957a0d4a4', 3, 141, 137, 2023, 1, N'admin', N'admin', N'Truy thu', N'-', NULL, 143)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'18781955-b9a1-45e0-bf16-390bfa6c24fe', 1, 142, NULL, 2023, 1, N'admin', N'admin', N'Số đã nộp', N'2.', NULL, 144)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'f063e3fc-2e2b-4bba-89cd-b5ee4148480d', 2, 143, NULL, 2023, 1, N'admin', N'admin', N'Đến ngày 31/12/….', N'-', NULL, 145)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'2907c02c-178f-4b87-b93b-61775cd4b698', 3, 144, NULL, 2023, 1, N'admin', N'admin', N'Số tiền', N'+', NULL, 146)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'1d06adf6-0073-4982-b2a4-78385fa63f6f', 3, 145, NULL, 2023, 1, N'admin', N'admin', N'Tỷ lệ so với số phải nộp (%)', N'+', NULL, 147)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'556d3de5-febe-4575-be1d-b9719eb6f3b6', 2, 146, NULL, 2023, 1, N'admin', N'admin', N'Đến ngày thẩm định', N'-', NULL, 148)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'f19dfa76-7567-4ca4-a520-81fb8856e19f', 3, 147, NULL, 2023, 1, N'admin', N'admin', N'Số tiền', N'+', NULL, 149)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'87aef7c7-d0ae-405a-aa8a-af1b4e5a4f89', 3, 148, NULL, 2023, 1, N'admin', N'admin', N'Tỷ lệ so với số phải nộp (%)', N'+', NULL, 150)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'51db7c7a-ecde-44f6-9cb6-7f8ffc99db4a', 1, 149, NULL, 2023, 1, N'admin', N'admin', N'Số nộp thừa (+), thiếu (-)', N'3.', NULL, 151)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'b3428fd8-642d-4e38-b449-300cd8719d9c', 1, 150, NULL, 2023, 1, N'admin', N'admin', N'BẢO HIỂM Y TẾ TNQN', N'V.', NULL, 152)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'7beaf682-5af6-4b88-90f5-b68ffceab194', 3, 151, NULL, 2023, 1, N'admin', N'admin', N'Số phải nộp', N'1.', NULL, 153)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'8ea73040-4b4e-4397-902c-4f866588b4e8', 3, 152, NULL, 2023, 1, N'admin', N'admin', N'Số đã nộp', N'2.', NULL, 154)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'd7d63ea8-b232-437d-a337-e228d2842e49', 3, 153, NULL, 2023, 1, N'admin', N'admin', N'Số đã nộp thừa (+), thiếu (-)', N'3.', NULL, 155)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'9907b672-987d-4a47-8e6d-e237d753841c', 1, 154, NULL, 2023, 1, N'admin', N'admin', N'BẢO HIỂM Y TẾ TN CN, VCQP', N'VI.', NULL, 156)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'ded822e6-02d5-4335-a806-9215c8d9be66', 3, 155, NULL, 2023, 1, N'admin', N'admin', N'Số phải nộp', N'1.', NULL, 157)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'b78bb17a-c774-4a93-9971-b9a04b11dfdd', 3, 156, NULL, 2023, 1, N'admin', N'admin', N'Số đã nộp', N'2.', NULL, 158)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'8502f1d7-9542-4da9-8647-19511309cf8e', 3, 157, NULL, 2023, 1, N'admin', N'admin', N'Số đã nộp thừa (+), thiếu (-)', N'3.', NULL, 159)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'19253430-83b8-41d8-bc05-863d7a2d1f9f', 1, 158, NULL, 2023, 1, N'admin', N'admin', N'BẢO HIỂM Y TẾ QS XÃ PHƯỜNG', N'VII.', NULL, 160)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'a62a65d9-30fe-41c3-9218-25b50a19d18a', 3, 159, NULL, 2023, 1, N'admin', N'admin', N'Số phải nộp', N'1.', NULL, 161)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'60215cf3-df8e-493c-b36a-5c2c9dbe0525', 3, 160, NULL, 2023, 1, N'admin', N'admin', N'Số đã nộp', N'2.', NULL, 162)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'501542b2-ac96-4681-97d7-f15c809d1dfb', 3, 161, NULL, 2023, 1, N'admin', N'admin', N'Số đã nộp thừa (+), thiếu (-)', N'3.', NULL, 163)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'e8968b18-846b-42c0-8775-35f3833982ef', 3, 63, NULL, 2023, 1, N'admin', N'admin', N'Số tiền', N'+', NULL, 65)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'9c84690d-25e5-4dfb-a2aa-2ad0fb59a986', 1, 65, NULL, 2023, 1, N'admin', N'admin', N'Số nộp thừa (+), thiếu (-)', N'3.', NULL, 67)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'a5809ee9-67cf-41f9-9f87-36ff898d8386', 1, 66, NULL, 2023, 1, N'admin', N'admin', N'BẢO HIỂM THẤT NGHIỆP', N'II.', NULL, 68)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'09283e0d-9881-46b8-b8d5-80c3ff12c3d8', 1, 67, NULL, 2023, 1, N'admin', N'admin', N'Số phải nộp', N'1', NULL, 69)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'8c349955-6b35-429f-905b-2bd48b639ed2', 1, 68, 67, 2023, 1, N'admin', N'admin', N'Khối dự toán', N'1.1.', NULL, 70)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'1aaa4250-fb00-4a74-ac0e-370e1050add8', 2, 69, 68, 2023, 1, N'admin', N'admin', N'Người lao động', N'a.', NULL, 71)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'61bd030b-463c-40e7-875f-96ad3bd4ba73', 3, 70, 69, 2023, 1, N'admin', N'admin', N'Trong năm', N'-', NULL, 72)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'7d69fc9f-3a4c-481d-8dfd-aebdee47c569', 3, 71, 69, 2023, 1, N'admin', N'admin', N'Truy thu', N'-', NULL, 73)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'f9186bcf-cb9d-4e02-86f3-47fb13226f06', 2, 72, 68, 2023, 1, N'admin', N'admin', N'Người sử dụng lao động', N'b.', NULL, 74)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'296c6e40-24a6-466d-8a93-cb0c2c93381d', 3, 73, 72, 2023, 1, N'admin', N'admin', N'Trong năm', N'-', NULL, 75)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'678c1ebd-90f0-4d7d-ac62-91f4b4104f74', 3, 74, 72, 2023, 1, N'admin', N'admin', N'Truy thu', N'-', NULL, 76)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'2cd98471-8ad0-4bbc-abe3-a76967196b55', 1, 75, 67, 2023, 1, N'admin', N'admin', N'Khối hạch toán', N'1.2.', NULL, 77)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'efef742b-de53-49e1-94b9-e622c585a0d8', 2, 76, 75, 2023, 1, N'admin', N'admin', N'Người lao động', N'a.', NULL, 78)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'b027a65a-ea0b-458c-8ca6-239e3493ac77', 3, 77, 76, 2023, 1, N'admin', N'admin', N'Trong năm', N'-', NULL, 79)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'e6b5126c-01b1-491b-95bb-85368577abf2', 3, 78, 76, 2023, 1, N'admin', N'admin', N'Truy thu', N'-', NULL, 80)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'4615f000-2e1b-4507-9eea-1d95dc362a57', 2, 79, 75, 2023, 1, N'admin', N'admin', N'Người sử dụng lao động', N'b.', NULL, 81)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'fed645fd-225c-4336-8fd0-958fd60ba556', 3, 80, 79, 2023, 1, N'admin', N'admin', N'Trong năm', N'-', NULL, 82)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'66bf1c8b-d249-4e3d-a4a2-374b05195eff', 3, 81, 79, 2023, 1, N'admin', N'admin', N'Truy thu', N'-', NULL, 83)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'3dfa9e23-9198-4156-b2f8-e0b300712a6b', 1, 82, NULL, 2023, 1, N'admin', N'admin', N'Số đã nộp', N'2.', NULL, 84)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'184d3c0e-1222-44d7-95e2-489a52f19e77', 2, 83, NULL, 2023, 1, N'admin', N'admin', N'Đến ngày 31/12/….', N'-', NULL, 85)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'c9c8bc00-3013-4b49-ad28-1ba22206b167', 3, 84, NULL, 2023, 1, N'admin', N'admin', N'Số tiền', N'+', NULL, 86)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'f3543cf1-64d9-4e5c-a01f-afa98be403e2', 3, 85, NULL, 2023, 1, N'admin', N'admin', N'Tỷ lệ so với số phải nộp (%)', N'+', NULL, 87)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'a4069c50-e64f-4972-a590-da62fef70e56', 2, 86, NULL, 2023, 1, N'admin', N'admin', N'Đến ngày thẩm định', N'-', NULL, 88)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'0f7277d8-e4c4-4d32-8129-10a16cca8adb', 3, 87, NULL, 2023, 1, N'admin', N'admin', N'Số tiền', N'+', NULL, 89)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'8b2bb1bb-0dc5-4e19-bda9-d6d4bcb87d4e', 3, 88, NULL, 2023, 1, N'admin', N'admin', N'Tỷ lệ so với số phải nộp (%)', N'+', NULL, 90)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'2596a55b-fea5-4040-b65b-83f9d372f05b', 1, 89, NULL, 2023, 1, N'admin', N'admin', N'Số nộp thừa (+), thiếu (-)', N'3.', NULL, 91)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'feaa7797-0a54-44b8-ac68-f5b8b1f7a7ce', 1, 90, NULL, 2023, 1, N'admin', N'admin', N'BẢO HIỂM Y TẾ NGƯỜI LĐ', N'III.', NULL, 92)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'31fc3f8b-9db4-4d9d-a443-fec93c8ee8f5', 1, 91, NULL, 2023, 1, N'admin', N'admin', N'Số phải nộp', N'1.', NULL, 93)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'8c98782a-c353-4b33-9257-80e0c7ce5ce8', 1, 92, 91, 2023, 1, N'admin', N'admin', N'Khối dự toán', N'1.1.', NULL, 94)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'f5378bb0-ecbb-4f52-b8cf-a2b62180c19d', 2, 93, 92, 2023, 1, N'admin', N'admin', N'Người lao động', N'a.', NULL, 95)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'9121b4c2-6f23-46cc-bf96-392498d62dff', 3, 94, 93, 2023, 1, N'admin', N'admin', N'Trong năm', N'a.1.', NULL, 96)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'1b4e91fe-fe0f-4790-86fd-4951d7a00e0d', 3, 95, 94, 2023, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL, 97)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'4326af6e-2077-438b-823c-60238d30f5f7', 3, 96, 94, 2023, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL, 98)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'e09d6897-afb2-4250-94e4-47009985ec72', 3, 97, 93, 2023, 1, N'admin', N'admin', N'Truy thu', N'a2.', NULL, 99)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'cb87d2cc-18d2-4128-adba-4c266664d504', 3, 98, 97, 2023, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL, 100)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'7edb71c9-431b-4967-9a39-82b54303190e', 3, 99, 97, 2023, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL, 101)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'b97f6d6b-67bc-494a-9f6c-e4085410738e', 2, 100, 92, 2023, 1, N'admin', N'admin', N'Người sử dụng lao động', N'b.', NULL, 102)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'c5f7f613-8bc2-4a57-8d1f-1984f693b581', 3, 101, 100, 2023, 1, N'admin', N'admin', N'Trong năm', N'b.1', NULL, 103)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'fdb39d84-6855-4af9-bac6-8871273d1251', 3, 102, 101, 2023, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL, 104)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'08ac24a3-30e6-4883-afcd-bf7b1d822e8e', 3, 103, 101, 2023, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL, 105)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'cd6f3976-6de7-4958-8a71-ffbb7cf4c8b8', 3, 104, 100, 2023, 1, N'admin', N'admin', N'Truy thu', N'b.2.', NULL, 106)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'8101ae8b-0b00-44ae-9b59-41f2a56392f1', 3, 105, 104, 2023, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL, 107)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'd0ff7e27-32db-4635-aae8-f1afe313a5a1', 3, 106, 104, 2023, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL, 108)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'0f2f9ed3-bd90-41db-b43b-9ec8086bedc6', 1, 107, NULL, 2023, 1, N'admin', N'admin', N'Khối hạch toán', N'1.2.', NULL, 109)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'aba2c2a7-3613-4501-ab26-4ecd1e080bb3', 2, 108, 107, 2023, 1, N'admin', N'admin', N'Người lao động', N'a.', NULL, 110)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'3a50fb37-8af6-40d7-b648-de1a4affbdd0', 3, 109, 108, 2023, 1, N'admin', N'admin', N'Trong năm', N'a.1.', NULL, 111)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'753948ab-41d9-4d70-982c-6198e2062ce2', 3, 110, 109, 2023, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL, 112)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'41034b8b-1a11-4000-9204-77f97ed2fb46', 3, 111, 109, 2023, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL, 113)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'81abbd45-deb4-46a7-8cc0-af8839b30049', 3, 112, 108, 2023, 1, N'admin', N'admin', N'Truy thu', N'a2.', NULL, 114)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'f799ae74-ebd5-481b-b0c6-cf379b5318bf', 3, 113, 112, 2023, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL, 115)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'9da85f88-feca-4c01-8bc8-6830f591d9d9', 3, 45, 44, 2024, 1, N'admin', N'admin', N'Trong năm', N'*', NULL, 47)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'c83285e6-97c2-408d-9253-6678a11f64f5', 3, 46, 45, 2024, 1, N'admin', N'admin', N'Sĩ quan', N'-', NULL, 48)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'482627a9-7d6e-45d7-8987-9a0de8e5d18d', 3, 47, 45, 2024, 1, N'admin', N'admin', N'QNCN', N'-', NULL, 49)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'1fc50ce3-6a3a-49a1-828e-79a2e0e1772a', 3, 48, 45, 2024, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL, 50)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'ef9fdc57-80fc-4c52-bdf4-835cb34671a1', 3, 49, 45, 2024, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL, 51)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'89a8eda3-f5e5-4aec-96a6-6b80ca0b2fd6', 3, 50, 44, 2024, 1, N'admin', N'admin', N'Truy thu', N'*', NULL, 52)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'0d146310-f622-45be-ae2c-8af9376c69cc', 3, 51, 50, 2024, 1, N'admin', N'admin', N'Sĩ quan', N'-', NULL, 53)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'a122e951-0bc3-44f3-a787-02dab523e9d0', 3, 52, 50, 2024, 1, N'admin', N'admin', N'QNCN', N'-', NULL, 54)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'32ed8696-0c92-49c3-a714-b90db494994b', 3, 53, 50, 2024, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL, 55)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'82fda8f5-294c-4177-849b-9d9f5c711551', 3, 54, 50, 2024, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL, 56)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'3f637d4a-ef55-43a6-af20-114af3630737', 3, 55, 43, 2024, 1, N'admin', N'admin', N'Người hưởng PC (HSQ, BS)', N'b.2.', NULL, 57)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'39d4eb72-2b79-4bf6-84fe-e384e8bd2cd9', 3, 56, 55, 2024, 1, N'admin', N'admin', N'Trong năm', N'-', NULL, 58)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'1b8439db-2b2f-457b-a3fd-83d4a1fe5627', 3, 57, 55, 2024, 1, N'admin', N'admin', N'Truy thu', N'-', NULL, 59)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'796006cb-7aec-4edd-86a8-f4040501e2a6', 1, 58, NULL, 2024, 1, N'admin', N'admin', N'Số đã nộp', N'2.', NULL, 60)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'3d13025c-1ff6-463b-9344-4bf9f0c5e51a', 2, 59, NULL, 2024, 1, N'admin', N'admin', N'Đến ngày 31/12/….', N'-', NULL, 61)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'7f176438-7ba2-47ec-b80d-ca625252d012', 3, 60, NULL, 2024, 1, N'admin', N'admin', N'Số tiền', N'+', NULL, 62)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'a9038043-ac42-42d3-a2ba-84d4a761d245', 3, 61, NULL, 2024, 1, N'admin', N'admin', N'Tỷ lệ so với số phải nộp (%)', N'+', NULL, 63)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'81b2ddff-6a33-427b-b0e7-45548c17914b', 2, 62, NULL, 2024, 1, N'admin', N'admin', N'Đến ngày thẩm định', N'-', NULL, 64)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'a030c832-600d-4d76-8ca2-2f2c47df2f9c', 3, 63, NULL, 2024, 1, N'admin', N'admin', N'Số tiền', N'+', NULL, 65)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'2d2ec00e-971b-4b18-8a81-9ddb6255a8e5', 3, 64, NULL, 2024, 1, N'admin', N'admin', N'Tỷ lệ so với số phải nộp (%)', N'+', NULL, 66)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'78e47d15-4f26-44d1-99dd-0ab30f8d386c', 1, 65, NULL, 2024, 1, N'admin', N'admin', N'Số nộp thừa (+), thiếu (-)', N'3.', NULL, 67)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'239dee5d-4b7c-4d5e-a116-91c6b49804ab', 1, 66, NULL, 2024, 1, N'admin', N'admin', N'BẢO HIỂM THẤT NGHIỆP', N'II.', NULL, 68)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'81a6e8cb-2fe9-4c5b-8537-c5e4a244f5b3', 1, 67, NULL, 2024, 1, N'admin', N'admin', N'Số phải nộp', N'1', NULL, 69)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'79a000ca-e365-41a6-a49a-e356f892dc3d', 1, 68, 67, 2024, 1, N'admin', N'admin', N'Khối dự toán', N'1.1.', NULL, 70)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'dd673239-beef-4efb-8cef-552c58a40ee0', 2, 69, 68, 2024, 1, N'admin', N'admin', N'Người lao động', N'a.', NULL, 71)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'be052e23-854b-4b0b-a856-313919b92d3d', 3, 70, 69, 2024, 1, N'admin', N'admin', N'Trong năm', N'-', NULL, 72)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'ed635939-d5ea-4abe-ba35-8177cef1189a', 3, 71, 69, 2024, 1, N'admin', N'admin', N'Truy thu', N'-', NULL, 73)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'5cd568f4-08e5-4b72-9ded-43bce43173c8', 2, 72, 68, 2024, 1, N'admin', N'admin', N'Người sử dụng lao động', N'b.', NULL, 74)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'8778dcbf-f7a0-4f70-a8f3-05e70d9b65ac', 3, 73, 72, 2024, 1, N'admin', N'admin', N'Trong năm', N'-', NULL, 75)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'4df70045-4cfa-44ab-980d-34d90a545109', 3, 74, 72, 2024, 1, N'admin', N'admin', N'Truy thu', N'-', NULL, 76)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'6768990d-c966-49ca-a253-1ced6bced3fe', 1, 75, 67, 2024, 1, N'admin', N'admin', N'Khối hạch toán', N'1.2.', NULL, 77)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'74946fc3-5804-43e7-af98-ccd9e2e7c36f', 2, 76, 75, 2024, 1, N'admin', N'admin', N'Người lao động', N'a.', NULL, 78)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'2ef0636c-fbb4-42f8-b9f8-fa14588de69f', 3, 77, 76, 2024, 1, N'admin', N'admin', N'Trong năm', N'-', NULL, 79)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'bed9fb55-e9eb-4bbd-8ce9-b5421b4704f3', 3, 78, 76, 2024, 1, N'admin', N'admin', N'Truy thu', N'-', NULL, 80)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'211711bf-f0ee-465c-ade4-f3a764e9133b', 2, 79, 75, 2024, 1, N'admin', N'admin', N'Người sử dụng lao động', N'b.', NULL, 81)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'8bd54996-493f-46d0-91b5-a08ad2d94cb4', 3, 80, 79, 2024, 1, N'admin', N'admin', N'Trong năm', N'-', NULL, 82)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'8f927b36-a475-4fbc-8778-6eda11bb432c', 3, 81, 79, 2024, 1, N'admin', N'admin', N'Truy thu', N'-', NULL, 83)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'8864a1fe-9016-4dd4-b885-dd56300f163f', 1, 82, NULL, 2024, 1, N'admin', N'admin', N'Số đã nộp', N'2.', NULL, 84)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'e0d200d2-2c8a-4049-8a0a-a9bdb697febe', 2, 83, NULL, 2024, 1, N'admin', N'admin', N'Đến ngày 31/12/….', N'-', NULL, 85)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'a9e51b07-abef-4154-8174-d131ab0e0b3a', 3, 84, NULL, 2024, 1, N'admin', N'admin', N'Số tiền', N'+', NULL, 86)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'6bb3f4c7-144e-4491-b824-c16a8f5af95f', 3, 85, NULL, 2024, 1, N'admin', N'admin', N'Tỷ lệ so với số phải nộp (%)', N'+', NULL, 87)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'33c66331-7e9f-439d-877f-5011964a798e', 2, 86, NULL, 2024, 1, N'admin', N'admin', N'Đến ngày thẩm định', N'-', NULL, 88)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'82b1f3b8-8081-4055-83a5-7701f33f3f72', 3, 87, NULL, 2024, 1, N'admin', N'admin', N'Số tiền', N'+', NULL, 89)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'970634e5-6587-4ce0-aa0f-66400eeba5f5', 3, 88, NULL, 2024, 1, N'admin', N'admin', N'Tỷ lệ so với số phải nộp (%)', N'+', NULL, 90)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'b9f01688-bf71-448c-b22b-f7d446efd589', 1, 89, NULL, 2024, 1, N'admin', N'admin', N'Số nộp thừa (+), thiếu (-)', N'3.', NULL, 91)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'ae3c3bca-2dad-4288-98c8-cb5f7a7c975d', 1, 90, NULL, 2024, 1, N'admin', N'admin', N'BẢO HIỂM Y TẾ NGƯỜI LĐ', N'III.', NULL, 92)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'3cba1202-4e6f-4f51-8362-3f94fd4651c9', 1, 91, NULL, 2024, 1, N'admin', N'admin', N'Số phải nộp', N'1.', NULL, 93)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'df7a7d1b-a8e9-4ca4-adf0-78e883d21370', 1, 92, 91, 2024, 1, N'admin', N'admin', N'Khối dự toán', N'1.1.', NULL, 94)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'c8611de2-8e60-4dec-91f4-90ef53b55edc', 2, 93, 92, 2024, 1, N'admin', N'admin', N'Người lao động', N'a.', NULL, 95)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'cdc0ae44-f6b7-4c8b-8ddc-b48c4b1cb87e', 3, 94, 93, 2024, 1, N'admin', N'admin', N'Trong năm', N'a.1.', NULL, 96)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'c37d57f7-9000-4e20-8a5c-8f140500a09e', 3, 95, 94, 2024, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL, 97)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'e2787df3-2fb6-4e3f-bec9-8607900fa74e', 1, 1, NULL, 2023, 1, N'admin', N'admin', N'THU, NỘP BHXH, BHTN, BHYT', N'A.', NULL, 1)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'f4de9cf2-a90a-4fde-a575-897c14580f52', 1, 2, NULL, 2023, 1, N'admin', N'admin', N'BẢO HIỂM XÃ HỘI', N'I.', NULL, 2)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'406c6dc4-de4f-46b6-9fee-1bdab9a16bfa', 1, 3, NULL, 2023, 1, N'admin', N'admin', N'Số phải nộp', N'1.', NULL, 3)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'06b4f812-744a-42af-9048-07de1284c59c', 1, 4, 3, 2023, 1, N'admin', N'admin', N'Khối dự toán', N'1.1.', NULL, 4)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'ce5e283b-0a91-4298-bd87-949be2f51ddb', 2, 5, 4, 2023, 1, N'admin', N'admin', N'Người lao động', N'a.', NULL, 5)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'd4282489-8b5b-4775-a855-703a369046a0', 3, 6, 5, 2023, 1, N'admin', N'admin', N'Trong năm', N'a.1.', NULL, 6)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'f5fb0c15-9daa-4ece-952b-5331c1481c5b', 3, 7, 6, 2023, 1, N'admin', N'admin', N'Sĩ quan', N'-', NULL, 7)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'c07903c2-4772-4304-8d12-e06fad89963d', 3, 8, 6, 2023, 1, N'admin', N'admin', N'QNCN', N'-', NULL, 8)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'e9140f77-3a5c-4b6e-95e3-3ba3d044b8b0', 3, 9, 6, 2023, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL, 10)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'86f298f9-345c-4c62-9f5e-5021b94fbde6', 3, 10, 6, 2023, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL, 11)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'0f6024f8-f6c0-4061-b1e6-07dd9f6325ff', 3, 11, 5, 2023, 1, N'admin', N'admin', N'Truy thu', N'a2.', NULL, 12)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'97c6bf37-e3d6-4e75-9cf0-bc8e28315691', 3, 12, 11, 2023, 1, N'admin', N'admin', N'Sĩ quan', N'-', NULL, 13)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'399a53a5-8a95-4ac8-959f-b8d11acc13e8', 3, 13, 11, 2023, 1, N'admin', N'admin', N'QNCN', N'-', NULL, 14)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'ab861fac-c730-4f0a-bf4d-365a0c7ecb40', 3, 14, 11, 2023, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL, 15)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'56121e01-83ed-4362-b4ef-9036043044f9', 3, 15, 11, 2023, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL, 16)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'71124ce2-94ac-4404-8b20-a321964f0fc1', 2, 16, 4, 2023, 1, N'admin', N'admin', N'Người sử dụng lao động', N'b.', NULL, 17)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'01ee4ad7-5897-41b6-81f0-57ff092cf65c', 3, 17, 16, 2023, 1, N'admin', N'admin', N'Người hưởng lương', N'b.1.', NULL, 18)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'365e556e-d01c-4fd3-a38c-f317e6176466', 3, 18, 17, 2023, 1, N'admin', N'admin', N'Trong năm', N'*', NULL, 19)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'42dcd991-8778-4061-8c19-4ffd161e019b', 3, 19, 18, 2023, 1, N'admin', N'admin', N'Sĩ quan', N'-', NULL, 20)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'bbb9ca41-8ddb-4bb3-b750-c46b1b1dd128', 3, 20, 18, 2023, 1, N'admin', N'admin', N'QNCN', N'-', NULL, 21)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'b72e752c-d3f5-4267-bb34-42bd30e11a80', 3, 21, 18, 2023, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL, 22)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'9badf6bf-d307-4cd1-8ccd-6068e56166c3', 3, 22, 18, 2023, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL, 23)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'17415007-5050-4cb6-bb0c-11b9a15a3a54', 3, 23, 17, 2023, 1, N'admin', N'admin', N'Truy thu', N'*', NULL, 24)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'68e4f597-e0db-4a37-9b44-5dce0a82e744', 3, 24, 23, 2023, 1, N'admin', N'admin', N'Sĩ quan', N'-', NULL, 25)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'25ffd520-86d4-48c0-b49e-53cfac28ca61', 3, 25, 23, 2023, 1, N'admin', N'admin', N'QNCN', N'-', NULL, 26)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'6f0772b5-6145-4caa-8cbd-0e9939cda87f', 3, 26, 23, 2023, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL, 27)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'2ca2ebe8-8206-48ac-8bf2-f61c2ebc5aa3', 3, 27, 23, 2023, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL, 28)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'1b2ea7a8-bce6-4908-8662-edbde3982048', 3, 28, 16, 2023, 1, N'admin', N'admin', N'Người hưởng PC (HSQ, BS)', N'b.2.', NULL, 29)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'712d528b-1eab-4368-adff-45c6bb00e874', 3, 29, 28, 2023, 1, N'admin', N'admin', N'Trong năm', N'-', NULL, 30)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'c83cf085-fcd6-4104-aaa9-22764457cfce', 3, 30, 28, 2023, 1, N'admin', N'admin', N'Truy thu', N'-', NULL, 31)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'6b9372dd-560a-4f66-b253-0a4666258b1d', 1, 31, 3, 2023, 1, N'admin', N'admin', N'Khối hạch toán', N'1.2.', NULL, 32)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'e438087d-38e6-4da2-a67e-b7339c62da2a', 2, 32, 31, 2023, 1, N'admin', N'admin', N'Người lao động', N'a.', NULL, 33)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'2a8cb71e-1489-4f66-8e2c-a808c82820d7', 3, 33, 32, 2023, 1, N'admin', N'admin', N'Trong năm', N'a.1.', NULL, 34)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'be4a9e7f-3961-4df6-85df-ff1ecf791c53', 3, 34, 33, 2023, 1, N'admin', N'admin', N'Sĩ quan', N'-', NULL, 35)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'0c8771a0-1c36-4c97-9a8c-bf6ac4e944b8', 3, 35, 33, 2023, 1, N'admin', N'admin', N'QNCN', N'-', NULL, 36)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'be73b8ee-cccc-4cd4-ab69-136ab4ae0fff', 3, 36, 33, 2023, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL, 38)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'144290a4-a1b9-4f78-a1d4-fe1034a75e9a', 3, 37, 33, 2023, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL, 39)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'67479577-235e-4a36-96b4-c79466bd4481', 3, 38, 32, 2023, 1, N'admin', N'admin', N'Truy thu', N'a2.', NULL, 40)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'3cecb0c3-71cb-4a86-9063-0facf934bad2', 3, 39, 38, 2023, 1, N'admin', N'admin', N'Sĩ quan', N'-', NULL, 41)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'59601511-6e69-4cd4-9baa-2358de979c74', 3, 40, 38, 2023, 1, N'admin', N'admin', N'QNCN', N'-', NULL, 42)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'9ff4725e-e83d-4959-961e-04bbe115590c', 3, 41, 38, 2023, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL, 43)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'05d7b658-4770-4c75-a8c2-48fae15f2e3d', 3, 42, 38, 2023, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL, 44)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'353fa996-11c5-45ce-9e6b-8c4c755d4109', 2, 43, 31, 2023, 1, N'admin', N'admin', N'Người sử dụng lao động', N'b.', NULL, 45)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'1c948705-3936-41c9-8065-fb5eb4dc5349', 3, 44, 43, 2023, 1, N'admin', N'admin', N'Người hưởng lương', N'b.1', NULL, 46)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'80754983-544a-4270-b63e-3332e624a18c', 3, 45, 44, 2023, 1, N'admin', N'admin', N'Trong năm', N'*', NULL, 47)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'4870f959-dbf4-455b-8ea2-17eb9ebb26e2', 3, 46, 45, 2023, 1, N'admin', N'admin', N'Sĩ quan', N'-', NULL, 48)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'e0405cd4-e71b-47bf-a1d9-f5ae92866a1d', 3, 47, 45, 2023, 1, N'admin', N'admin', N'QNCN', N'-', NULL, 49)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'3bc39fd1-5fb6-4efd-a201-793c02777e31', 3, 48, 45, 2023, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL, 50)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'638e8423-1394-46f3-ad6a-2603021ffe68', 3, 49, 45, 2023, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL, 51)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'fd0e5468-116d-48b4-a504-b969eefd1bfa', 3, 50, 44, 2023, 1, N'admin', N'admin', N'Truy thu', N'*', NULL, 52)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'39765b93-2a4b-4777-84ce-bd557aee08e9', 3, 51, 50, 2023, 1, N'admin', N'admin', N'Sĩ quan', N'-', NULL, 53)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'db8f5eec-41f0-4c51-92a3-4045f92fc3e1', 3, 52, 50, 2023, 1, N'admin', N'admin', N'QNCN', N'-', NULL, 54)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'0bcf3e82-e97c-4b4e-a3f1-aec01f162556', 3, 53, 50, 2023, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL, 55)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'd5fe2a21-fa82-4b95-939e-451ca50bffd4', 3, 54, 50, 2023, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL, 56)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'e454bd9a-9dbf-4b0f-b38f-6d9150501ee6', 3, 55, 43, 2023, 1, N'admin', N'admin', N'Người hưởng PC (HSQ, BS)', N'b.2.', NULL, 57)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'36905e30-3d34-46ef-b224-8b0536a6ba0b', 3, 56, 55, 2023, 1, N'admin', N'admin', N'Trong năm', N'-', NULL, 58)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'd7ed7c1f-11df-4de0-b21a-6277d5826777', 3, 57, 55, 2023, 1, N'admin', N'admin', N'Truy thu', N'-', NULL, 59)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'2d30f2da-a3d9-41cc-8793-2b6622ca1bb8', 1, 58, NULL, 2023, 1, N'admin', N'admin', N'Số đã nộp', N'2.', NULL, 60)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'bb961a9d-190d-4694-a619-3dc8cbfdfba5', 2, 59, NULL, 2023, 1, N'admin', N'admin', N'Đến ngày 31/12/….', N'-', NULL, 61)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'11061d8e-1516-4aca-bea2-245c73792282', 3, 60, NULL, 2023, 1, N'admin', N'admin', N'Số tiền', N'+', NULL, 62)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'ede2573d-e0b5-4f57-8ebf-06290c9073f6', 3, 61, NULL, 2023, 1, N'admin', N'admin', N'Tỷ lệ so với số phải nộp (%)', N'+', NULL, 63)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'3138e386-9e70-4e91-992c-4fbbf184d008', 2, 62, NULL, 2023, 1, N'admin', N'admin', N'Đến ngày thẩm định', N'-', NULL, 64)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'b3fd713f-a0dc-47f1-8162-35eaad47f023', 3, 148, NULL, 2024, 1, N'admin', N'admin', N'Tỷ lệ so với số phải nộp (%)', N'+', NULL, 150)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'8628d258-00de-4860-9b28-6d34ea84c544', 1, 149, NULL, 2024, 1, N'admin', N'admin', N'Số nộp thừa (+), thiếu (-)', N'3.', NULL, 151)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'40ad4352-e862-41ce-8c42-0bf0168b4970', 1, 150, NULL, 2024, 1, N'admin', N'admin', N'BẢO HIỂM Y TẾ TNQN', N'V.', NULL, 152)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'29d44145-0bf0-4468-94b9-b13c5d818a32', 3, 151, NULL, 2024, 1, N'admin', N'admin', N'Số phải nộp', N'1.', NULL, 153)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'b3b63df4-f775-4d9f-aa18-5b51895f568b', 3, 152, NULL, 2024, 1, N'admin', N'admin', N'Số đã nộp', N'2.', NULL, 154)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'b244c32b-18a8-4507-a96b-6e5ba0cb0290', 3, 153, NULL, 2024, 1, N'admin', N'admin', N'Số đã nộp thừa (+), thiếu (-)', N'3.', NULL, 155)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'989840ef-551d-4e5f-888d-0db1a4ad2399', 1, 154, NULL, 2024, 1, N'admin', N'admin', N'BẢO HIỂM Y TẾ TN CN, VCQP', N'VI.', NULL, 156)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'c4316d7d-710c-4b31-ab22-1a9f21596ada', 3, 155, NULL, 2024, 1, N'admin', N'admin', N'Số phải nộp', N'1.', NULL, 157)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'74f1ac2b-7155-48e3-b7fd-7b8a5aa7b65f', 3, 156, NULL, 2024, 1, N'admin', N'admin', N'Số đã nộp', N'2.', NULL, 158)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'aa265d90-ee94-4676-8713-1a31af36d42c', 3, 157, NULL, 2024, 1, N'admin', N'admin', N'Số đã nộp thừa (+), thiếu (-)', N'3.', NULL, 159)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'115d48fc-0ecf-4fc9-a00d-1378afe7d4e4', 1, 158, NULL, 2024, 1, N'admin', N'admin', N'BẢO HIỂM Y TẾ QS XÃ PHƯỜNG', N'VII.', NULL, 160)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'9fc98bda-0519-47f3-b4b9-7ff6074261e0', 3, 159, NULL, 2024, 1, N'admin', N'admin', N'Số phải nộp', N'1.', NULL, 161)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'9cb780df-e590-42a3-8ae0-538253e1b599', 3, 160, NULL, 2024, 1, N'admin', N'admin', N'Số đã nộp', N'2.', NULL, 162)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'40dbffe1-5ffd-4185-b734-a858d7bdb574', 3, 161, NULL, 2024, 1, N'admin', N'admin', N'Số đã nộp thừa (+), thiếu (-)', N'3.', NULL, 163)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'3e3cbc21-0ef3-40a9-87b8-b34416c80372', 1, 162, NULL, 2024, 1, N'admin', N'admin', N'BẢO HIỂM Y TẾ HS, SV', N'VIII.', NULL, 164)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'8eb55252-a096-4efe-80a2-72f022d01242', 3, 163, NULL, 2024, 1, N'admin', N'admin', N'Số phải nộp', N'1.', NULL, 165)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'59c03c94-c8a3-48d6-9be7-a151b3c72183', 3, 164, NULL, 2024, 1, N'admin', N'admin', N'Số đã nộp', N'2.', NULL, 166)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'afc1c2b8-39b0-4ad5-b46c-8ab3dacc1e7d', 3, 165, NULL, 2024, 1, N'admin', N'admin', N'Số đã nộp thừa (+), thiếu (-)', N'3.', NULL, 167)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'4b629fe9-c246-4b26-af7c-62df8b067743', 1, 166, NULL, 2024, 1, N'admin', N'admin', N'BHYT Lưu học sinh', N'IX.', NULL, 168)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'e041dff1-34ac-455d-a029-1220e122513e', 3, 167, NULL, 2024, 1, N'admin', N'admin', N'Số phải nộp', N'1.', NULL, 169)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'2b6aa6d0-e048-4544-90e1-86ae6e738d57', 3, 168, NULL, 2024, 1, N'admin', N'admin', N'Số đã nộp', N'2.', NULL, 170)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'12fd2f4a-38b7-4b74-a40a-de68abf965c3', 3, 169, NULL, 2024, 1, N'admin', N'admin', N'Số đã nộp thừa (+), thiếu (-)', N'3.', NULL, 171)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'70f71ac7-6e3f-47d3-bdcf-82bf57f38912', 1, 170, NULL, 2024, 1, N'admin', N'admin', N'BHYT Sĩ quan dự bị', N'X.', NULL, 172)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'bebe8e07-d3ac-4800-bb85-47c065997a9e', 3, 171, NULL, 2024, 1, N'admin', N'admin', N'Số phải nộp', N'1.', NULL, 173)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'c324ccab-1978-47c7-a502-1d64d5f84178', 3, 172, NULL, 2024, 1, N'admin', N'admin', N'Số đã nộp', N'2.', NULL, 174)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'217aadb0-3f5d-4174-844c-40d9aae6ffc1', 3, 173, NULL, 2024, 1, N'admin', N'admin', N'Số đã nộp thừa (+), thiếu (-)', N'3.', NULL, 175)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'09ee50d8-662e-4e8a-baeb-dfced75f66ee', 1, 174, NULL, 2024, 1, N'admin', N'admin', N'BHYT CÁC ĐỐI TƯỢNG KHÁC (nếu có)', N'XI.', NULL, 176)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'b4ae5cc4-8725-4a43-8ede-8a6550bbe69c', 1, 175, NULL, 2024, 1, N'admin', N'admin', N'CHI BẢO HIỂM XÃ HỘI, BẢO HIỂM Y TẾ', N'B.', NULL, 177)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'13d63355-5472-4ecb-8c09-0b8a19b2d134', 1, 176, NULL, 2024, 1, N'admin', N'admin', N'CHI CÁC CHẾ ĐỘ BẢO HIỂM XÃ HỘI', N'I.', NULL, 178)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'2bf3cabc-f9fd-48a7-a6e5-981f48541df6', 1, 177, NULL, 2024, 1, N'admin', N'admin', N'Dự toán', N'1.', NULL, 179)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'1478a605-1a9b-4ae4-a933-2461cd2eb30c', 3, 178, 177, 2024, 1, N'admin', N'admin', N'Khối dự toán', N'-', NULL, 180)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'1d5f384c-a168-4cbf-9265-dfdd40aac46e', 3, 179, 177, 2024, 1, N'admin', N'admin', N'Khối hạch toán', N'-', NULL, 181)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'98d81c9a-3218-452c-893e-ea04cda1ca54', 1, 180, NULL, 2024, 1, N'admin', N'admin', N'Số cấp kinh phí', N'2.', NULL, 182)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'669b6ee3-8595-45e5-8c5c-bdbca73511ca', 1, 181, NULL, 2024, 1, N'admin', N'admin', N'Số quyết toán', N'3.', NULL, 183)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'85b68490-c49c-4d94-a974-fbb5af800ecb', 2, 182, 181, 2024, 1, N'admin', N'admin', N'Khối dự toán', N'3.1.', NULL, 184)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'3a79052e-627f-49a1-8cf0-d1c0131de618', 3, 183, 182, 2024, 1, N'admin', N'admin', N'Trợ cấp ốm đau', N'-', NULL, 185)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'49bb21d9-1af1-4ea8-ad72-f2ceb366f9f2', 3, 184, 182, 2024, 1, N'admin', N'admin', N'Trợ cấp thai sản', N'-', NULL, 186)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'e1981dd2-0de2-437a-bd18-056d24dc8b4e', 3, 185, 182, 2024, 1, N'admin', N'admin', N'Trợ cấp TNLĐ, BNN', N'-', NULL, 187)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'9a26e6ba-c811-4544-a48a-8ec63f57a1c9', 3, 186, 182, 2024, 1, N'admin', N'admin', N'Trợ cấp hưu trí', N'-', NULL, 188)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'2450c1aa-7ce8-4397-8925-64d1395fd523', 3, 187, 182, 2024, 1, N'admin', N'admin', N'Trợ cấp phục viên', N'-', NULL, 189)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'd7842f12-28d3-46b1-9272-93739b2a236a', 3, 188, 182, 2024, 1, N'admin', N'admin', N'Trợ cấp xuất ngũ', N'-', NULL, 190)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'91c8488d-4003-4aa8-bf46-f63042c9fdd4', 3, 189, 182, 2024, 1, N'admin', N'admin', N'Trợ cấp thôi việc', N'-', NULL, 191)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'f2be2226-1b55-43c0-a888-8afc52cf7013', 3, 190, 182, 2024, 1, N'admin', N'admin', N'Trợ cấp tử tuất', N'-', NULL, 192)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'ba3b7edb-4252-4270-9c30-72c3772884cf', 2, 192, 181, 2024, 1, N'admin', N'admin', N'Khối hạch toán', N'3.2.', NULL, 194)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'126cb1c2-b37e-4b9e-b147-f2ef36996596', 3, 193, 192, 2024, 1, N'admin', N'admin', N'Trợ cấp ốm đau', N'-', NULL, 195)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'dfe9c2a5-d425-4184-bb44-2df7f8508960', 3, 194, 192, 2024, 1, N'admin', N'admin', N'Trợ cấp thai sản', N'-', NULL, 196)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'e2abc574-3570-4b26-a403-21316efb2faf', 3, 195, 192, 2024, 1, N'admin', N'admin', N'Trợ cấp TNLĐ, BNN', N'-', NULL, 197)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'9b51dc24-0a66-4b74-8cee-600811691657', 3, 196, 192, 2024, 1, N'admin', N'admin', N'Trợ cấp hưu trí', N'-', NULL, 198)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'e7b5460c-47e7-4d62-b7fe-dd5eef5cf560', 3, 197, 192, 2024, 1, N'admin', N'admin', N'Trợ cấp phục viên', N'-', NULL, 199)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'5b928dcc-2aeb-40e2-9c6a-5a40f481a8de', 3, 198, 192, 2024, 1, N'admin', N'admin', N'Trợ cấp xuất ngũ', N'-', NULL, 200)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'379ebec4-ab1d-4cfe-ba16-273f2315337d', 3, 199, 192, 2024, 1, N'admin', N'admin', N'Trợ cấp thôi việc', N'-', NULL, 201)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'6940163f-4387-44d3-9025-17462807c946', 3, 200, 192, 2024, 1, N'admin', N'admin', N'Trợ cấp tử tuất', N'-', NULL, 202)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'47934426-eb9b-45a0-863d-b2071ec06714', 1, 202, NULL, 2024, 1, N'admin', N'admin', N'Kinh phí cấp thừa (+), thiếu (-)', N'4.', NULL, 204)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'4b0e8931-0005-4796-a3bc-80b8da5326cf', 1, 203, NULL, 2024, 1, N'admin', N'admin', N'CHI BẢO HIỂM Y TẾ', N'II.', NULL, 205)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'0c2e0098-0983-43ab-9626-e498926bb0e2', 3, 64, NULL, 2023, 1, N'admin', N'admin', N'Tỷ lệ so với số phải nộp (%)', N'+', NULL, 66)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'63bac372-8676-4cf5-9d3f-57e8be27477a', 1, 204, NULL, 2024, 1, N'admin', N'admin', N'Chăm sóc sức khỏe ban đầu', N'1.', NULL, 206)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'217c715f-6f36-46fc-9746-76779277b576', 2, 205, NULL, 2024, 1, N'admin', N'admin', N'Người lao động', N'1.1.', NULL, 207)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'19220f24-28fb-43f2-89ec-1829fdc49c98', 3, 206, NULL, 2024, 1, N'admin', N'admin', N'Số kinh phí được sử dụng', N'a)', NULL, 208)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'4c2bbe42-8668-43dd-866f-6a6410037aff', 3, 207, 206, 2024, 1, N'admin', N'admin', N'Năm trước chuyển sang', N'-', NULL, 209)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'e389c4e9-0284-46c1-861d-6494bccf79e6', 3, 208, 206, 2024, 1, N'admin', N'admin', N'Được cấp trong năm', N'-', NULL, 210)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'916eecbc-b3a0-4bca-9ac0-e6400ea072df', 3, 209, NULL, 2024, 1, N'admin', N'admin', N'Số quyết toán', N'b)', NULL, 211)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'd49fc048-ca01-4ff5-89a8-62859832aefe', 3, 210, NULL, 2024, 1, N'admin', N'admin', N'Kinh phí cấp thừa (+), thiếu (-)', N'c)', NULL, 212)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'6724b359-a657-459b-a530-1813522ba0b6', 2, 211, NULL, 2024, 1, N'admin', N'admin', N'Học sinh, sinh viên', N'1.2.', NULL, 213)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'b84d57c4-e001-45e8-8c31-72c8b607c8be', 3, 212, NULL, 2024, 1, N'admin', N'admin', N'Số kinh phí được sử dụng', N'a)', NULL, 214)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'0ca6ca2a-d174-4a76-8c71-e18b87d3cccf', 3, 213, 212, 2024, 1, N'admin', N'admin', N'Năm trước chuyển sang', N'-', NULL, 215)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'b63fd0ca-f5be-4b47-9767-81c7dbf9370f', 3, 214, 212, 2024, 1, N'admin', N'admin', N'Được cấp trong năm', N'-', NULL, 216)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'74c3f325-aefe-4f9b-b2db-e4e43da6b2e2', 3, 215, NULL, 2024, 1, N'admin', N'admin', N'Số quyết toán', N'b)', NULL, 217)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'e7bba7aa-9504-467d-99c9-61367a242016', 3, 216, NULL, 2024, 1, N'admin', N'admin', N'Kinh phí cấp thừa (+), thiếu (-)', N'c)', NULL, 218)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'47589376-9e6e-47a7-be8a-d0ace656283a', 1, 217, NULL, 2024, 1, N'admin', N'admin', N'Kinh phí KCB tại quân y đơn vị (10%)', N'2.', NULL, 219)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'71f89f60-9f7e-4824-9fb5-31d6f43e897c', 2, 218, NULL, 2024, 1, N'admin', N'admin', N'Dự toán Bộ giao', N'2.1.', NULL, 220)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'd29c8b0e-28ca-4997-a2d2-bb83b8eea2cd', 2, 219, NULL, 2024, 1, N'admin', N'admin', N'Tính 10% số thu', N'2.2.', NULL, 221)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'9fbd4142-e432-4248-a087-b905fa2c5ce7', 3, 220, 219, 2024, 1, N'admin', N'admin', N'Năm trước chuyển sang', N'-', NULL, 222)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'820bf179-2603-44f3-ad25-001bf546d8a2', 3, 221, 219, 2024, 1, N'admin', N'admin', N'Năm nay', N'-', NULL, 223)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'fcfe5504-bdf7-4f77-8bf1-df760c0f324f', 2, 222, NULL, 2024, 1, N'admin', N'admin', N'Số kinh phí được cấp', N'2.3.', NULL, 224)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'c9e943e1-bd0c-4caf-85e8-bc727274e68d', 2, 223, NULL, 2024, 1, N'admin', N'admin', N'Số quyết toán', N'2.4.', NULL, 225)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'91d03e07-daa3-424d-ac07-d29cc83a29db', 2, 224, NULL, 2024, 1, N'admin', N'admin', N'Kinh phí cấp thừa (+), thiếu (-)', N'2.5.', NULL, 226)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'a3155d00-bd97-4792-a5a0-42dbff5e460f', 2, 225, NULL, 2024, 1, N'admin', N'admin', N'Dự toán (10%) chuyển năm sau', N'2.6.', NULL, 227)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'6135ac51-a4ba-4af5-afc9-00e03697bc7d', 1, 226, NULL, 2024, 1, N'admin', N'admin', N'Chi mua sắm trang thiết bị y tế', N'3.', NULL, 228)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'e71a95ad-48b5-42d1-87a0-c81bdeeb4f5f', 2, 227, NULL, 2024, 1, N'admin', N'admin', N'Dự toán', N'3.1.', NULL, 229)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'35ac389c-9b41-4752-9161-fdcfea9454ab', 3, 228, 227, 2024, 1, N'admin', N'admin', N'Năm trước chuyển sang ', N'-', NULL, 230)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'bd6e6a4d-b7a2-4b02-b488-cb710d182eef', 3, 229, 227, 2024, 1, N'admin', N'admin', N'Năm nay ', N'-', NULL, 231)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'0623fe47-e0cf-42e6-8771-675061909d1e', 2, 230, NULL, 2024, 1, N'admin', N'admin', N'Số kinh phí được cấp', N'3.2.', NULL, 232)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'84c518f2-bff6-46ac-bc3b-25e4caf7d7b2', 2, 231, NULL, 2024, 1, N'admin', N'admin', N'Số quyết toán', N'3.3.', NULL, 233)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'487cda89-ad83-4bcc-b58c-e4cddf1c74e4', 2, 232, NULL, 2024, 1, N'admin', N'admin', N'Kinh phí thừa (+), thiếu (-)', N'3.4', NULL, 234)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'b0fd27e0-8a60-42c1-86f9-7692c76c60b6', 1, 233, NULL, 2024, 1, N'admin', N'admin', N'Chi khám chữa bệnh tại TS - DK', N'4.', NULL, 235)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'bcefbf0d-7e29-4b0a-9ff9-aba928f60bb1', 2, 234, NULL, 2024, 1, N'admin', N'admin', N'Dự toán', N'4.1.', NULL, 236)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'6ae623c0-56e0-4cfc-a487-74e31f299c3c', 3, 235, 234, 2024, 1, N'admin', N'admin', N'Năm trước chuyển sang ', N'- ', NULL, 237)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'bcf2f202-9d83-4c0f-b968-5dfe25170a79', 3, 236, 234, 2024, 1, N'admin', N'admin', N'Năm nay ', N'-', NULL, 238)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'21620bc6-98f8-4797-b80d-f63792b00e2e', 2, 237, NULL, 2024, 1, N'admin', N'admin', N'Số kinh phí được cấp', N'4.2.', NULL, 239)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'6f3d26c9-6dd9-408b-a2d6-b84dc1cb6249', 2, 238, NULL, 2024, 1, N'admin', N'admin', N'Số quyết toán', N'4.3.', NULL, 240)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'5a0afe5a-3d7b-44a4-9e3b-8e2157dc0607', 2, 239, NULL, 2024, 1, N'admin', N'admin', N'Kinh phí thừa (+), thiếu (-)', N'4.4.', NULL, 241)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'6722f1e5-939f-484f-84da-d37e2bc63dc1', 2, 240, NULL, 2024, 1, N'admin', N'admin', N'Dự toán chuyển năm sau', N'4.5.', NULL, 242)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'5992aae2-1ca4-445c-a927-836f7f79dcad', 1, 241, NULL, 2024, 1, N'admin', N'admin', N'CHI BẢO HIỂM THẤT NGHIỆP', N'III.', NULL, 243)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'c261857d-1a01-496d-8fa2-22cbfdcb1292', 3, 242, NULL, 2024, 1, N'admin', N'admin', N'Số kinh phí được cấp', N'1.', NULL, 244)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'56109f81-d1bc-497d-9521-38522af8bafb', 3, 243, NULL, 2024, 1, N'admin', N'admin', N'Số quyết toán', N'2.', NULL, 245)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'3966cbe7-4f08-4a88-a27e-759f42bf3d5e', 3, 244, NULL, 2024, 1, N'admin', N'admin', N'Kinh phí thừa (+), thiếu (-)', N'3.', NULL, 246)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'7217ad6f-b410-43d9-8242-6cc94bd8869d', 1, 245, NULL, 2024, 1, N'admin', N'admin', N'KINH PHÍ QUẢN LÝ BHXH, BHYT', N'IV. ', NULL, 247)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'9f972bb1-7422-46d0-8d96-b3c53009c1f9', 1, 246, NULL, 2024, 1, N'admin', N'admin', N'Dự toán', N'1.', NULL, 248)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'ec100ed3-f560-4f7c-b79a-e36d6eeb8afd', 2, 247, 246, 2024, 1, N'admin', N'admin', N'Năm trước chuyển sang', N'1.1.', NULL, 249)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'4cf7291c-2ab6-4783-b5b1-867823ab5e68', 2, 248, 246, 2024, 1, N'admin', N'admin', N'Năm nay', N'1.2.', NULL, 250)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'1c69a2d6-f56a-46f3-afb9-d082ec816dc6', 1, 249, NULL, 2024, 1, N'admin', N'admin', N'Số kinh phí được cấp', N'2.', NULL, 251)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'c18aa862-6742-4621-93e7-ab1fbfc4df15', 2, 250, 249, 2024, 1, N'admin', N'admin', N'Năm trước chuyển sang', N'2.1', NULL, 252)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'6d0c5c77-9133-4649-997c-57b7b7757fbb', 2, 251, 249, 2024, 1, N'admin', N'admin', N'Năm nay ', N'2.2.', NULL, 253)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'c55b471f-0570-40d8-97c7-091fd144a22e', 3, 252, NULL, 2024, 1, N'admin', N'admin', N'Số quyết toán', N'3.', NULL, 254)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'f278284d-bec5-4e36-adb7-a47a7c15bf94', 3, 253, NULL, 2024, 1, N'admin', N'admin', N'Kinh phí cấp thừa (+), thiếu (-)', N'4.', NULL, 255)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'9d45e3eb-dbbd-4456-a8dd-3b181550ce1c', 3, 254, NULL, 2024, 1, N'admin', N'admin', N'Dự toán chuyển năm sau', N'5.', NULL, 256)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'7c819899-02d8-42c2-a745-5d77c6b9e02b', 1, 255, NULL, 2024, 1, N'admin', N'admin', N'XÁC ĐỊNH CƠ SỞ TÍNH NỘP BHYT TỪ QUỸ BHXH', N'C.', NULL, 257)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'0cf29224-4177-4c87-a78a-332b1105605a', 3, 256, NULL, 2024, 1, N'admin', N'admin', N'Số tháng hưởng trợ cấp ốm đau thuộc danh mục bệnh cần chữa trị dài ngày không tính đóng BHXH', N'1.', NULL, 258)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'6b8a9f32-ce4e-4538-9b86-574a4b0b4f68', 3, 257, NULL, 2024, 1, N'admin', N'admin', N'Số tiền trợ cấp sinh con và nuôi con nuôi', N'2.', NULL, 259)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa], [iSTT]) VALUES (N'3bfc4996-d0da-46a6-988d-a75bc01fa018', 1, 258, NULL, 2024, 1, N'admin', N'admin', N'GIẢI THÍCH SỐ LIỆU CHÊNH LỆCH (nếu có)', N'D.', NULL, 260)
GO
/****** Object:  Index [PK_BH_DM_ThamDinhQuyetToan]    Script Date: 5/9/2024 5:42:53 PM ******/
ALTER TABLE [dbo].[BH_DM_ThamDinhQuyetToan] ADD  CONSTRAINT [PK_BH_DM_ThamDinhQuyetToan] PRIMARY KEY NONCLUSTERED 
(
	[iID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BH_DM_ThamDinhQuyetToan] ADD  DEFAULT (newid()) FOR [iID]
GO
;;;

/****** Object:  StoredProcedure [dbo].[sp_bh_report_tham_dinh_quyet_toan_tong_hop_thu_chi]    Script Date: 5/10/2024 12:12:57 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_report_tham_dinh_quyet_toan_tong_hop_thu_chi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_report_tham_dinh_quyet_toan_tong_hop_thu_chi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_report_tham_dinh_quyet_toan_tong_hop_thu_chi]    Script Date: 5/10/2024 12:12:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_report_tham_dinh_quyet_toan_tong_hop_thu_chi]
	@NamLamViec int ,
	@LstMaDonVi nvarchar(max),
	@Type int, --1:thu, 2:chi
	@DVT int
AS
BEGIN
    -- Insert statements for procedure here
	CREATE TABLE #result(STT nvarchar(50),IIdChungTu uniqueidentifier, IIdParent uniqueidentifier, SNoiDung nvarchar(200), ILevel int, IThuTu int, ILoaiChi int , FDuToan float, FHachToan Float, IKinhPhiKCB int)
	IF(@Type = 1)
		BEGIN
			DECLARE @IIdThuBHYT uniqueidentifier = NewID();
			INSERT INTO #result(STT,IIdChungTu, IIdParent, SNoiDung, ILevel, IThuTu, ILoaiChi, FDuToan, FHachToan, IKinhPhiKCB)
			--<DATA INSERT>--
			(SELECT '1',NEWID(), NULL, N'Thu Bảo hiểm xã hội', 1, 1, @Type,
				SUM( CASE WHEN iMa in (7, 8, 9, 10, 12, 13, 14, 15, 19, 20, 21, 22, 24, 25, 26, 27, 29, 30, 259) THEN ISNULL(fSoThamDinh, 0) ELSE 0 END),
				SUM( CASE WHEN iMa in (34, 35, 36, 37, 39, 40, 41, 42, 46, 47, 48, 49, 51, 52, 53, 54, 56, 57, 260) THEN ISNULL(fSoThamDinh, 0) ELSE 0 END),
				0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet
				WHERE iNamLamViec = @NamLamViec 
					AND iMa in (7, 8, 9, 10, 12, 13, 14, 15, 19, 20, 21, 22, 24, 25, 26, 27, 29, 30, 34, 35, 36, 37, 39, 40, 41, 42, 46, 47, 48, 49, 51, 52, 53, 54, 56, 57,259,260)
					AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			UNION ALL
			SELECT '2',NEWID(), NULL, N'Thu Bảo hiểm thất nghiệp', 1, 2, @Type,
				SUM( CASE WHEN iMa in (70, 71, 73, 74) THEN ISNULL(fSoThamDinh, 0) ELSE 0 END),
				SUM( CASE WHEN iMa in (77, 78, 80, 81) THEN ISNULL(fSoThamDinh, 0) ELSE 0 END),
				0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet
				WHERE iNamLamViec = @NamLamViec 
					AND iMa in (70, 71, 73, 74, 77, 78, 80, 81)
					AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			UNION ALL
			SELECT '3', @IIdThuBHYT, NULL, N'Thu Bảo hiểm y tế', 1, 3, @Type,
				0,
				0,
				0
			UNION ALL
			SELECT '3.1',NEWID(), @IIdThuBHYT, N'Thu BHYT quân nhân', 2, 1, @Type,
				SUM( CASE WHEN iMa in (133, 134, 135, 136) THEN ISNULL(fSoThamDinh, 0) ELSE 0 END),
				SUM( CASE WHEN iMa in (138, 139, 140, 141) THEN ISNULL(fSoThamDinh, 0) ELSE 0 END),
				0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet
				WHERE iNamLamViec = @NamLamViec 
					AND iMa in (133, 134, 135, 136, 138, 139, 140, 141)
					AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			UNION ALL
			SELECT '3.2',NEWID(), @IIdThuBHYT, N'Thu BHYT người lao động', 2, 2, @Type,
				SUM( CASE WHEN iMa in (95, 96, 98, 99, 102, 103, 105, 106) THEN ISNULL(fSoThamDinh, 0) ELSE 0 END),
				SUM( CASE WHEN iMa in (110, 111, 113, 114, 117, 118, 120, 121) THEN ISNULL(fSoThamDinh, 0) ELSE 0 END),
				0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet
				WHERE iNamLamViec = @NamLamViec 
					AND iMa in (95, 96, 98, 99, 102, 103, 105, 106, 110, 111, 113, 114, 117, 118, 120, 121)
					AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			UNION ALL
			SELECT '3.3',NEWID(), @IIdThuBHYT, N'Thu BHYT thân nhân quân nhân', 2, 3, @Type,
				SUM( CASE WHEN iMa = 151 AND dv.iKhoi = 2 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
				SUM( CASE WHEN iMa = 151 AND dv.iKhoi = 1 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
				0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
				WHERE ctct.iNamLamViec = @NamLamViec 
					AND ctct.iMa = 151
					AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
					AND dv.iTrangThai = 1
			UNION ALL
			SELECT '3.4',NEWID(), @IIdThuBHYT, N'Thu BHYT thân nhân CNVCQP', 2, 4, @Type,
				SUM( CASE WHEN iMa = 155 AND dv.iKhoi = 2 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
				SUM( CASE WHEN iMa = 155 AND dv.iKhoi = 1 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
				0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
				WHERE ctct.iNamLamViec = @NamLamViec 
					AND ctct.iMa = 155
					AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
					AND dv.iTrangThai = 1
			UNION ALL
			SELECT '3.5',NEWID(), @IIdThuBHYT, N'Thu BHYT HVQS Xã phường', 2, 5, @Type,
				SUM( CASE WHEN iMa = 159 AND dv.iKhoi = 2 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
				SUM( CASE WHEN iMa = 159 AND dv.iKhoi = 1 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
				0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
				WHERE ctct.iNamLamViec = @NamLamViec
					AND ctct.iMa = 159
					AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
					AND dv.iTrangThai = 1
			UNION ALL
			SELECT '3.6',NEWID(), @IIdThuBHYT, N'Thu BHYT SQDB', 2, 6, @Type,
				SUM( CASE WHEN iMa = 171 AND dv.iKhoi = 2 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
				SUM( CASE WHEN iMa = 171 AND dv.iKhoi = 1 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
				0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
				WHERE ctct.iNamLamViec = @NamLamViec
					AND ctct.iMa = 171
					AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
					AND dv.iTrangThai = 1
			UNION ALL
			SELECT '3.7',NEWID(), @IIdThuBHYT, N'Thu BHYT HS,SV', 2, 7, @Type,
				SUM( CASE WHEN iMa = 163 AND dv.iKhoi = 2 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
				SUM( CASE WHEN iMa = 163 AND dv.iKhoi = 1 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
				0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
				WHERE ctct.iNamLamViec = @NamLamViec
					AND ctct.iMa = 163
					AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
					AND dv.iTrangThai = 1
			UNION ALL
			SELECT '3.8',NEWID(), @IIdThuBHYT, N'Thu BHYT Lưu HS', 2, 8, @Type,
				SUM( CASE WHEN iMa = 167 AND dv.iKhoi = 2 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
				SUM( CASE WHEN iMa = 167 AND dv.iKhoi = 1 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
				0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
				WHERE ctct.iNamLamViec = @NamLamViec
					AND ctct.iMa = 167
					AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
					AND dv.iTrangThai = 1
			);
			SELECT STT, IIdChungTu, IIdParent, SNoiDung, ILevel, IThuTu, ILoaiChi, FDuToan/@DVT FDuToan, FHachToan/@DVT FHachToan, IKinhPhiKCB from #result

		END
	ELSE
		BEGIN
			---Phần chi---
			DECLARE @IIdChiCDBHYT uniqueidentifier = NewID();
			DECLARE @IIdKinhPhiKCB uniqueidentifier = NewID();
			DECLARE @IIdChiTieuKPQL uniqueidentifier = NewID();
			DECLARE @IIdChiTieuKPKCB uniqueidentifier = NewID();
			DECLARE @IIdChiTieuKPKCBTSDK uniqueidentifier = NewID();
			DECLARE @IIdChiCSK uniqueidentifier = NewID();
			INSERT INTO #result(STT,IIdChungTu, IIdParent, SNoiDung, ILevel, IThuTu, ILoaiChi, FDuToan, FHachToan,IKinhPhiKCB)
			--<DATA INSERT>--
			(
			SELECT 'I',@IIdChiCDBHYT, NULL, N'Chi các chế độ BHXH', 1, 1, @Type,
				0,
				0,
				0
			UNION ALL
			SELECT '1',NEWID(), @IIdChiCDBHYT, N'Trợ cấp ốm đau', 2, 1, @Type,
				SUM( CASE WHEN iMa = 183 THEN ISNULL(fSoThamDinh, 0) ELSE 0 END),
				SUM( CASE WHEN iMa = 193 THEN ISNULL(fSoThamDinh, 0) ELSE 0 END),
				0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet
				WHERE iNamLamViec = @NamLamViec 
					AND iMa in (193, 183)
					AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			UNION ALL
			SELECT '2',NEWID(), @IIdChiCDBHYT, N'Trợ cấp thai sản', 2, 2, @Type,
				SUM( CASE WHEN iMa = 184 THEN ISNULL(fSoThamDinh, 0) ELSE 0 END),
				SUM( CASE WHEN iMa = 194 THEN ISNULL(fSoThamDinh, 0) ELSE 0 END),
				0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet
				WHERE iNamLamViec = @NamLamViec 
					AND iMa in (184, 194)
					AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			UNION ALL
			SELECT '3',NEWID(), @IIdChiCDBHYT, N'Trợ cấp tai nạn lao động, BNN', 2, 3, @Type,
				SUM( CASE WHEN iMa = 185 THEN ISNULL(fSoThamDinh, 0) ELSE 0 END),
				SUM( CASE WHEN iMa = 195 THEN ISNULL(fSoThamDinh, 0) ELSE 0 END),
				0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet
				WHERE iNamLamViec = @NamLamViec 
					AND iMa in (185, 195)
					AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			UNION ALL
			SELECT '4',NEWID(), @IIdChiCDBHYT, N'Trợ cấp hưu trí', 2, 4, @Type,
				SUM( CASE WHEN iMa = 186 THEN ISNULL(fSoThamDinh, 0) ELSE 0 END),
				SUM( CASE WHEN iMa = 196 THEN ISNULL(fSoThamDinh, 0) ELSE 0 END),
				0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet
				WHERE iNamLamViec = @NamLamViec 
					AND iMa in (186, 196)
					AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			UNION ALL
			SELECT '5',NEWID(), @IIdChiCDBHYT, N'Trợ cấp phục viên', 2, 5, @Type,
				SUM( CASE WHEN iMa = 187 THEN ISNULL(fSoThamDinh, 0) ELSE 0 END),
				SUM( CASE WHEN iMa = 197 THEN ISNULL(fSoThamDinh, 0) ELSE 0 END),
				0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet
				WHERE iNamLamViec = @NamLamViec 
					AND iMa in (187, 197)
					AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			UNION ALL
			SELECT '6',NEWID(), @IIdChiCDBHYT, N'Trợ cấp xuất ngũ', 2, 6, @Type,
				SUM( CASE WHEN iMa = 188 THEN ISNULL(fSoThamDinh, 0) ELSE 0 END),
				SUM( CASE WHEN iMa = 198 THEN ISNULL(fSoThamDinh, 0) ELSE 0 END),
				0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet
				WHERE iNamLamViec = @NamLamViec 
					AND iMa in (188, 198)
					AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			UNION ALL
			SELECT '7',NEWID(), @IIdChiCDBHYT, N'Trợ cấp thôi việc', 2, 7, @Type,
					SUM( CASE WHEN iMa = 189 THEN ISNULL(fSoThamDinh, 0) ELSE 0 END),
					SUM( CASE WHEN iMa = 199 THEN ISNULL(fSoThamDinh, 0) ELSE 0 END),
					0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet
				WHERE iNamLamViec = @NamLamViec 
					AND iMa in (189, 199)
					AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			UNION ALL
			SELECT '8',NEWID(), @IIdChiCDBHYT, N'Trợ cấp tử tuất', 2, 8, @Type,
					SUM( CASE WHEN iMa = 190 THEN ISNULL(fSoThamDinh, 0) ELSE 0 END),
					SUM( CASE WHEN iMa = 200 THEN ISNULL(fSoThamDinh, 0) ELSE 0 END),
					0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet
				WHERE iNamLamViec = @NamLamViec 
					AND iMa in (190, 200)
					AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			UNION ALL ---II---
			SELECT 'II',NEWID(), NULL, N'Kinh phí quản lý BHXH, BHYT', 1, 2, @Type,
					SUM( CASE WHEN iMa = 252 AND dv.iKhoi = 2 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					SUM( CASE WHEN iMa = 252 AND dv.iKhoi = 1 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					2
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
				WHERE ctct.iNamLamViec = @NamLamViec 
					AND ctct.iMa = 252
					AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
					AND dv.iTrangThai = 1
			UNION ALL

			SELECT '1', @IIdChiTieuKPQL, @IIdKinhPhiKCB, N'Dự toán', 2, 1, @Type,
				0,
				0,
				1
			UNION ALL
			SELECT '-',NEWID(), @IIdChiTieuKPQL, N'Năm trước chuyển sang', 3, 1, @Type,
					SUM( CASE WHEN iMa = 247 AND dv.iKhoi = 2 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					SUM( CASE WHEN iMa = 247 AND dv.iKhoi = 1 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
				WHERE ctct.iNamLamViec = @NamLamViec 
					AND ctct.iMa = 247
					AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
					AND dv.iTrangThai = 1
			UNION ALL
			SELECT '-',NEWID(), @IIdChiTieuKPQL, N'Năm nay', 3, 2, @Type,
					SUM( CASE WHEN iMa = 248 AND dv.iKhoi = 2 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					SUM( CASE WHEN iMa = 248 AND dv.iKhoi = 1 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
				WHERE ctct.iNamLamViec = @NamLamViec 
					AND ctct.iMa = 248
					AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
					AND dv.iTrangThai = 1

			UNION ALL
			SELECT '2',NEWID(), @IIdKinhPhiKCB, N'Số quyết toán', 2, 2, @Type,
					SUM( CASE WHEN iMa = 252 AND dv.iKhoi = 2 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					SUM( CASE WHEN iMa = 252 AND dv.iKhoi = 1 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					2
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
				WHERE ctct.iNamLamViec = @NamLamViec 
					AND ctct.iMa = 252
					AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
					AND dv.iTrangThai = 1

			UNION ALL
			SELECT '3',NEWID(), @IIdKinhPhiKCB, N'Dự toán chuyển năm sau', 2, 2, @Type,
					SUM( CASE WHEN iMa = 254 AND dv.iKhoi = 2 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					SUM( CASE WHEN iMa = 254 AND dv.iKhoi = 1 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					2
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
				WHERE ctct.iNamLamViec = @NamLamViec 
					AND ctct.iMa = 254
					AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
					AND dv.iTrangThai = 1
			UNION ALL
			SELECT 'III',@IIdKinhPhiKCB, NULL, N'Kinh phí KCB tại quân y đơn vị', 1, 3, @Type,
					SUM( CASE WHEN iMa = 223 AND dv.iKhoi = 2 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					SUM( CASE WHEN iMa = 223 AND dv.iKhoi = 1 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					2
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
				WHERE ctct.iNamLamViec = @NamLamViec 
					AND ctct.iMa = 223
					AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
					AND dv.iTrangThai = 1
			UNION ALL
			SELECT '1',@IIdChiTieuKPKCB, @IIdKinhPhiKCB, N'Dự toán', 2, 1, @Type,
				0,
				0,
				1
			UNION ALL
			SELECT '-',NEWID(), @IIdChiTieuKPKCB, N'Năm trước chuyển sang', 3, 1, @Type,
					SUM( CASE WHEN iMa = 220 AND dv.iKhoi = 2 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					SUM( CASE WHEN iMa = 220 AND dv.iKhoi = 1 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
				WHERE ctct.iNamLamViec = @NamLamViec 
					AND ctct.iMa = 220
					AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
					AND dv.iTrangThai = 1
			UNION ALL
			SELECT '-',NEWID(), @IIdChiTieuKPKCB, N'Năm nay', 3, 2, @Type,
					SUM( CASE WHEN iMa = 221 AND dv.iKhoi = 2 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					SUM( CASE WHEN iMa = 221 AND dv.iKhoi = 1 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
				WHERE ctct.iNamLamViec = @NamLamViec 
					AND ctct.iMa = 221
					AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
					AND dv.iTrangThai = 1
			UNION ALL
			SELECT '2',NEWID(), @IIdKinhPhiKCB, N'Số quyết toán', 2, 2, @Type,
					SUM( CASE WHEN iMa = 223 AND dv.iKhoi = 2 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					SUM( CASE WHEN iMa = 223 AND dv.iKhoi = 1 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					2
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
				WHERE ctct.iNamLamViec = @NamLamViec 
					AND ctct.iMa = 223
					AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
					AND dv.iTrangThai = 1
			UNION ALL
			SELECT '3',NEWID(), @IIdKinhPhiKCB, N'Dự toán chuyển năm sau', 2, 3, @Type,
					SUM( CASE WHEN iMa = 225 AND dv.iKhoi = 2 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					SUM( CASE WHEN iMa = 225 AND dv.iKhoi = 1 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					3
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
				WHERE ctct.iNamLamViec = @NamLamViec 
					AND ctct.iMa = 225
					AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
					AND dv.iTrangThai = 1
			UNION ALL --IV--
			SELECT 'IV',NEWID(), NULL, N'Kinh phí KCB tại trường sa - DK', 1, 4, @Type,
					SUM( CASE WHEN iMa = 238 AND dv.iKhoi = 2 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					SUM( CASE WHEN iMa = 238 AND dv.iKhoi = 1 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					2
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
				WHERE ctct.iNamLamViec = @NamLamViec 
					AND ctct.iMa = 238
					AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
					AND dv.iTrangThai = 1
			UNION ALL
			SELECT '1',@IIdChiTieuKPKCBTSDK, @IIdKinhPhiKCB, N'Dự toán', 2, 1, @Type,
					0,
					0,
					1
			UNION ALL
			SELECT '-',NEWID(), @IIdChiTieuKPKCBTSDK, N'Năm trước chuyển sang', 3, 1, @Type,
					SUM( CASE WHEN iMa = 235 AND dv.iKhoi = 2 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					SUM( CASE WHEN iMa = 235 AND dv.iKhoi = 1 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
				WHERE ctct.iNamLamViec = @NamLamViec 
					AND ctct.iMa = 235
					AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
					AND dv.iTrangThai = 1
			UNION ALL
			SELECT '-',NEWID(), @IIdChiTieuKPKCBTSDK, N'Năm nay', 3, 2, @Type,
					SUM( CASE WHEN iMa = 236 AND dv.iKhoi = 2 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					SUM( CASE WHEN iMa = 236 AND dv.iKhoi = 1 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
				WHERE ctct.iNamLamViec = @NamLamViec 
					AND ctct.iMa = 236
					AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
					AND dv.iTrangThai = 1
			UNION ALL
			SELECT '2',NEWID(), @IIdKinhPhiKCB, N'Số quyết toán', 2, 2, @Type,
					SUM( CASE WHEN iMa = 238 AND dv.iKhoi = 2 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					SUM( CASE WHEN iMa = 238 AND dv.iKhoi = 1 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					2
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
				WHERE ctct.iNamLamViec = @NamLamViec 
					AND ctct.iMa = 238
					AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
					AND dv.iTrangThai = 1
			UNION ALL
			SELECT '3',NEWID(), @IIdKinhPhiKCB, N'Dự toán chuyển năm sau', 2, 4, @Type,
					SUM( CASE WHEN iMa = 240 AND dv.iKhoi = 2 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					SUM( CASE WHEN iMa = 240 AND dv.iKhoi = 1 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					3
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
				WHERE ctct.iNamLamViec = @NamLamViec 
					AND ctct.iMa = 240
					AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
					AND dv.iTrangThai = 1
			UNION ALL --V--
			SELECT 'V',NEWID(), NULL, N'Chi phí mua sắm thiết bị y tế', 1, 5, @Type,
					SUM( CASE WHEN iMa = 231 AND dv.iKhoi = 2 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					SUM( CASE WHEN iMa = 231 AND dv.iKhoi = 1 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
				WHERE ctct.iNamLamViec = @NamLamViec 
					AND ctct.iMa = 231
					AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
					AND dv.iTrangThai = 1

			UNION ALL --VI--
			SELECT 'VI',@IIdChiCSK, NULL, N'Chi chăm sóc sức khỏe ban đầu', 1, 6, @Type,
					SUM( CASE WHEN iMa in (209, 215) AND dv.iKhoi = 2 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					SUM( CASE WHEN iMa in (209, 215) AND dv.iKhoi = 1 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
				WHERE ctct.iNamLamViec = @NamLamViec 
					AND ctct.iMa in (209, 215)
					AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
					AND dv.iTrangThai = 1
			
			UNION ALL --VII-- 
			SELECT 'VII',NEWID(), NULL, N'Chi hỗ trợ người lao động tham gia BHTN', 1, 7, @Type,
					SUM( CASE WHEN iMa = 243 AND dv.iKhoi = 2 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					SUM( CASE WHEN iMa = 243 AND dv.iKhoi = 1 THEN ISNULL(ctct.fSoThamDinh, 0) ELSE 0 END),
					0
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
				WHERE ctct.iNamLamViec = @NamLamViec 
					AND ctct.iMa = 243
					AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
					AND dv.iTrangThai = 1
			);
			SELECT STT, IIdChungTu, IIdParent, SNoiDung, ILevel, IThuTu, ILoaiChi, FDuToan/@DVT FDuToan, FHachToan/@DVT FHachToan, IKinhPhiKCB from #result
			--SELECT * from #result

		END
		DROP TABLE #result;
END
;
;
;
GO
