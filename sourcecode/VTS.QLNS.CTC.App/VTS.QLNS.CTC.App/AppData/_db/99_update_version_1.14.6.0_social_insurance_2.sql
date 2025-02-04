/****** Object:  StoredProcedure [dbo].[sp_bh_thamdinhquyettoan_chitiet]    Script Date: 6/7/2024 10:28:59 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_thamdinhquyettoan_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_thamdinhquyettoan_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_thamdinhquyettoan_chitiet]    Script Date: 6/7/2024 10:28:59 AM ******/
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

			-- lấy ra đơn vị từ đơn vị của chứng từ
	SELECT iID_MaDonVi, iID_MaDonVi + ' - ' + sTenDonVi as sTenDonVi into #tblDonVi
	FROM DonVi 
	WHERE 
		INamLamViec = @INamLamViec 
		AND iTrangThai=1
		AND iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))

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
	SELECT #dmtdqtResult.*,
	CASE WHEN #dmtdqtResult.iKieuChu <> 1 THEN #tblDonVi.iID_MaDonVi  ELSE '' END as iID_MaDonVi,
	CASE WHEN #dmtdqtResult.iKieuChu <> 1 THEN #tblDonVi.sTenDonVi  ELSE '' END as sTenDonVi
	FROM #dmtdqtResult,#tblDonVi
	ORDER BY iSTT;

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


DELETE FROM [dbo].[BH_DM_LoaiChi]
GO
INSERT [dbo].[BH_DM_LoaiChi] ([iID], [sMaLoaiChi], [sTenDanhMucLoaiChi], [iNamLamViec], [dNgaySua], [dNgayTao], [sNguoiSua], [sNguoiTao], [sMoTa], [iTrangThai], [sLNS], [sDSXauNoiMa]) VALUES (N'ee2053ea-6cd5-40bb-a706-f03138a96c55', N'08', N'Chi hỗ trợ BHTN', 2023, CAST(N'2023-12-11T16:52:00' AS SmallDateTime), CAST(N'2023-12-07T10:52:00' AS SmallDateTime), N'admin', N'admin', N'', 1, N'9010010', N'9010010, 9010010-010-011-0001, 9010010-010-011-0001-0001, 9010010-010-011-0001-0002, 9010010-010-011-0001-0003, 9010010-010-011-0001-0004, 9010010-010-011-0001-0005, 9010010-010-011-0001-0006, 9010010-010-011-0002, 9010010-010-011-0002-0001, 9010010-010-011-0002-0002, 9010010-010-011-0002-0003, 9010010-010-011-0002-0004, 9010010-010-011-0002-0005, 9010010-010-011-0002-0006')
GO
INSERT [dbo].[BH_DM_LoaiChi] ([iID], [sMaLoaiChi], [sTenDanhMucLoaiChi], [iNamLamViec], [dNgaySua], [dNgayTao], [sNguoiSua], [sNguoiTao], [sMoTa], [iTrangThai], [sLNS], [sDSXauNoiMa]) VALUES (N'400c8606-1dfc-447c-a9c3-5198cbd879ad', N'05', N'Chi từ nguồn kết dư Quỹ KCB BHYT quân nhân', 2024, CAST(N'2023-12-11T16:52:00' AS SmallDateTime), CAST(N'2022-07-26T16:03:00' AS SmallDateTime), N'admin', N'admin', N'', 1, N'9010008', N'9010008, 9010008-010-011-0001, 9010008-010-011-0002')
GO
INSERT [dbo].[BH_DM_LoaiChi] ([iID], [sMaLoaiChi], [sTenDanhMucLoaiChi], [iNamLamViec], [dNgaySua], [dNgayTao], [sNguoiSua], [sNguoiTao], [sMoTa], [iTrangThai], [sLNS], [sDSXauNoiMa]) VALUES (N'9da35c5c-80b3-43de-8788-fe110e434ed6', N'06', N'Kinh phí mua sắm trang thiết bị y tế', 2024, CAST(N'2023-12-11T16:52:00' AS SmallDateTime), CAST(N'2023-12-07T10:58:00' AS SmallDateTime), N'admin', N'admin', N'', 1, N'9010009', N'9010009, 9010009-010-011-0001, 9010009-010-011-0002')
GO
INSERT [dbo].[BH_DM_LoaiChi] ([iID], [sMaLoaiChi], [sTenDanhMucLoaiChi], [iNamLamViec], [dNgaySua], [dNgayTao], [sNguoiSua], [sNguoiTao], [sMoTa], [iTrangThai], [sLNS], [sDSXauNoiMa]) VALUES (N'e12a946d-92bd-4cb4-a999-4d05ff2913f0', N'01', N'Chi các chế độ BHXH', 2024, CAST(N'2024-06-10T08:00:00' AS SmallDateTime), CAST(N'2022-07-26T16:03:00' AS SmallDateTime), N'admin', N'admin', N'', 1, N'901,9010001,9010002', N'901, 9010001, 9010001-010-011-0001, 9010001-010-011-0001-0001, 9010001-010-011-0001-0001-0001-01, 9010001-010-011-0001-0001-0001-01-01, 9010001-010-011-0001-0001-0001-01-02, 9010001-010-011-0001-0001-0001-02, 9010001-010-011-0001-0001-0001-02-01, 9010001-010-011-0001-0001-0001-02-02, 9010001-010-011-0001-0002, 9010001-010-011-0001-0003, 9010001-010-011-0002, 9010001-010-011-0002-0001-0001-00, 9010001-010-011-0002-0001-0001-00-01, 9010001-010-011-0002-0001-0001-00-02, 9010001-010-011-0002-0001-0002-00, 9010001-010-011-0002-0001-0002-00-01, 9010001-010-011-0002-0001-0002-00-02, 9010001-010-011-0002-0001-0003-00, 9010001-010-011-0002-0001-0003-00-01, 9010001-010-011-0002-0001-0003-00-02, 9010001-010-011-0002-0001-0004-00, 9010001-010-011-0003, 9010001-010-011-0003-0001-0001-00, 9010001-010-011-0003-0001-0002-00, 9010001-010-011-0003-0001-0003-00, 9010001-010-011-0003-0001-0004-00, 9010001-010-011-0003-0001-0005-00, 9010001-010-011-0003-0001-0006-00, 9010001-010-011-0003-0001-0007-00, 9010001-010-011-0003-0001-0008-00, 9010001-010-011-0003-0001-0009-00, 9010001-010-011-0004, 9010001-010-011-0004-0001-0001-00, 9010001-010-011-0004-0001-0002-00, 9010001-010-011-0005, 9010001-010-011-0005-0001-0001-00, 9010001-010-011-0005-0001-0002-00, 9010001-010-011-0006, 9010001-010-011-0006-0001-0001-00, 9010001-010-011-0007, 9010001-010-011-0007-0001-0001-00, 9010001-010-011-0007-0001-0002-00, 9010001-010-011-0008, 9010001-010-011-0008-0001-0001-00, 9010001-010-011-0008-0001-0002-00, 9010001-010-011-0008-0001-0003-00, 9010002, 9010002-010-011-0001, 9010002-010-011-0001-0001, 9010002-010-011-0001-0001-0001-01, 9010002-010-011-0001-0001-0001-01-01, 9010002-010-011-0001-0001-0001-01-02, 9010002-010-011-0001-0001-0001-02, 9010002-010-011-0001-0001-0001-02-01, 9010002-010-011-0001-0001-0001-02-02, 9010002-010-011-0001-0002, 9010002-010-011-0001-0003, 9010002-010-011-0002, 9010002-010-011-0002-0001-0001-00, 9010002-010-011-0002-0001-0001-00-01, 9010002-010-011-0002-0001-0001-00-02, 9010002-010-011-0002-0001-0002-00, 9010002-010-011-0002-0001-0002-00-01, 9010002-010-011-0002-0001-0002-00-02, 9010002-010-011-0002-0001-0003-00, 9010002-010-011-0002-0001-0003-00-01, 9010002-010-011-0002-0001-0003-00-02, 9010002-010-011-0002-0001-0004-00, 9010002-010-011-0003, 9010002-010-011-0003-0001-0001-00, 9010002-010-011-0003-0001-0002-00, 9010002-010-011-0003-0001-0003-00, 9010002-010-011-0003-0001-0004-00, 9010002-010-011-0003-0001-0005-00, 9010002-010-011-0003-0001-0006-00, 9010002-010-011-0003-0001-0007-00, 9010002-010-011-0003-0001-0008-00, 9010002-010-011-0003-0001-0009-00, 9010002-010-011-0004, 9010002-010-011-0004-0001-0001-00, 9010002-010-011-0004-0001-0002-00, 9010002-010-011-0005, 9010002-010-011-0005-0001-0001-00, 9010002-010-011-0005-0001-0002-00, 9010002-010-011-0006, 9010002-010-011-0006-0001-0001-00, 9010002-010-011-0007, 9010002-010-011-0007-0001-0001-00, 9010002-010-011-0007-0001-0002-00, 9010002-010-011-0008, 9010002-010-011-0008-0001-0001-00, 9010002-010-011-0008-0001-0002-00, 9010002-010-011-0008-0001-0003-00')
GO
INSERT [dbo].[BH_DM_LoaiChi] ([iID], [sMaLoaiChi], [sTenDanhMucLoaiChi], [iNamLamViec], [dNgaySua], [dNgayTao], [sNguoiSua], [sNguoiTao], [sMoTa], [iTrangThai], [sLNS], [sDSXauNoiMa]) VALUES (N'0db5b7a4-1579-4a9f-9859-c26ce592a6c9', N'02', N'Chi kinh phí quản lý BHXH, BHYT', 2024, CAST(N'2024-06-10T08:00:00' AS SmallDateTime), CAST(N'2022-07-26T16:03:00' AS SmallDateTime), N'admin', N'admin', N'', 1, N'9010003', N'9010003, 9010003-010, 9010003-010-011, 9010003-010-011-6000, 9010003-010-011-6000-6001, 9010003-010-011-6000-6003, 9010003-010-011-6000-6049, 9010003-010-011-6050, 9010003-010-011-6050-6051, 9010003-010-011-6050-6099, 9010003-010-011-6100, 9010003-010-011-6100-6101, 9010003-010-011-6100-6102, 9010003-010-011-6100-6103, 9010003-010-011-6100-6105, 9010003-010-011-6100-6107, 9010003-010-011-6100-6113, 9010003-010-011-6100-6114, 9010003-010-011-6100-6115, 9010003-010-011-6100-6116, 9010003-010-011-6100-6121, 9010003-010-011-6100-6149, 9010003-010-011-6150, 9010003-010-011-6150-6155, 9010003-010-011-6150-6199, 9010003-010-011-6200, 9010003-010-011-6200-6201, 9010003-010-011-6200-6202, 9010003-010-011-6200-6249, 9010003-010-011-6250, 9010003-010-011-6250-6251, 9010003-010-011-6250-6252, 9010003-010-011-6250-6253, 9010003-010-011-6250-6254, 9010003-010-011-6250-6257, 9010003-010-011-6250-6299, 9010003-010-011-6300, 9010003-010-011-6300-6301, 9010003-010-011-6300-6302, 9010003-010-011-6300-6303, 9010003-010-011-6300-6304, 9010003-010-011-6300-6349, 9010003-010-011-6400, 9010003-010-011-6400-6401, 9010003-010-011-6400-6404, 9010003-010-011-6400-6449, 9010003-010-012, 9010003-010-012-6500, 9010003-010-012-6500-6501, 9010003-010-012-6500-6502, 9010003-010-012-6500-6503, 9010003-010-012-6500-6504, 9010003-010-012-6500-6505, 9010003-010-012-6500-6549, 9010003-010-012-6550, 9010003-010-012-6550-6551, 9010003-010-012-6550-6552, 9010003-010-012-6550-6553, 9010003-010-012-6550-6599, 9010003-010-012-6600, 9010003-010-012-6600-6601, 9010003-010-012-6600-6603, 9010003-010-012-6600-6605, 9010003-010-012-6600-6606, 9010003-010-012-6600-6608, 9010003-010-012-6600-6618, 9010003-010-012-6600-6649, 9010003-010-012-6650, 9010003-010-012-6650-6651, 9010003-010-012-6650-6652, 9010003-010-012-6650-6653, 9010003-010-012-6650-6654, 9010003-010-012-6650-6655, 9010003-010-012-6650-6656, 9010003-010-012-6650-6657, 9010003-010-012-6650-6658, 9010003-010-012-6650-6699, 9010003-010-012-6700, 9010003-010-012-6700-6701, 9010003-010-012-6700-6702, 9010003-010-012-6700-6703, 9010003-010-012-6700-6704, 9010003-010-012-6700-6749, 9010003-010-012-6750, 9010003-010-012-6750-6751, 9010003-010-012-6750-6752, 9010003-010-012-6750-6754, 9010003-010-012-6750-6755, 9010003-010-012-6750-6756, 9010003-010-012-6750-6757, 9010003-010-012-6750-6758, 9010003-010-012-6750-6761, 9010003-010-012-6750-6799, 9010003-010-012-6800, 9010003-010-012-6800-6801, 9010003-010-012-6800-6802, 9010003-010-012-6800-6803, 9010003-010-012-6800-6805, 9010003-010-012-6800-6806, 9010003-010-012-6800-6849, 9010003-010-012-6850, 9010003-010-012-6850-6851, 9010003-010-012-6850-6852, 9010003-010-012-6850-6853, 9010003-010-012-6850-6855, 9010003-010-012-6850-6899, 9010003-010-012-6900, 9010003-010-012-6900-6901, 9010003-010-012-6900-6902, 9010003-010-012-6900-6903, 9010003-010-012-6900-6905, 9010003-010-012-6900-6907, 9010003-010-012-6900-6912, 9010003-010-012-6900-6913, 9010003-010-012-6900-6921, 9010003-010-012-6900-6949, 9010003-010-012-6950, 9010003-010-012-6950-6951, 9010003-010-012-6950-6952, 9010003-010-012-6950-6953, 9010003-010-012-6950-6954, 9010003-010-012-6950-6955, 9010003-010-012-6950-6956, 9010003-010-012-6950-6999, 9010003-010-012-7000, 9010003-010-012-7000-7001, 9010003-010-012-7000-7004, 9010003-010-012-7000-7012, 9010003-010-012-7000-7017, 9010003-010-012-7000-7049, 9010003-010-012-7000-7049-0001, 9010003-010-012-7000-7049-0002, 9010003-010-012-7000-7049-0003, 9010003-010-012-7000-7049-0004, 9010003-010-012-7000-7049-0005, 9010003-010-013, 9010003-010-013-7750, 9010003-010-013-7750-7751, 9010003-010-013-7750-7756, 9010003-010-013-7750-7757, 9010003-010-013-7750-7761, 9010003-010-013-7750-7767, 9010003-010-013-7750-7799, 9010003-010-013-7750-7799-0001, 9010003-010-013-7850, 9010003-010-013-7850-7851, 9010003-010-013-7850-7852, 9010003-010-013-7850-7853, 9010003-010-013-7850-7854, 9010003-010-013-7850-7899, 9010003-010-013-7900, 9010003-010-013-7900-7903, 9010003-010-013-7900-7949, 9010003-010-013-7950, 9010003-010-013-7950-7951, 9010003-010-013-7950-7952, 9010003-010-013-7950-7954, 9010003-010-013-7950-7999')
GO
INSERT [dbo].[BH_DM_LoaiChi] ([iID], [sMaLoaiChi], [sTenDanhMucLoaiChi], [iNamLamViec], [dNgaySua], [dNgayTao], [sNguoiSua], [sNguoiTao], [sMoTa], [iTrangThai], [sLNS], [sDSXauNoiMa]) VALUES (N'35ff0052-56c4-4c79-b100-747871de4c66', N'03', N'Chi kinh phí KCB tại quân y đơn vị ', 2024, CAST(N'2024-01-12T09:56:00' AS SmallDateTime), CAST(N'2022-07-26T16:03:00' AS SmallDateTime), N'admin', N'admin', N'', 1, N'9010004', N'9010004, 9010004-010-011-0001, 9010004-010-011-0001-0000, 9010004-010-011-0001-0001, 9010004-010-011-0001-0002, 9010004-010-011-0001-0003, 9010004-010-011-0002, 9010004-010-011-0002-0000, 9010004-010-011-0002-0001, 9010004-010-011-0002-0002, 9010004-010-011-0002-0003')
GO
INSERT [dbo].[BH_DM_LoaiChi] ([iID], [sMaLoaiChi], [sTenDanhMucLoaiChi], [iNamLamViec], [dNgaySua], [dNgayTao], [sNguoiSua], [sNguoiTao], [sMoTa], [iTrangThai], [sLNS], [sDSXauNoiMa]) VALUES (N'2b59b196-9e43-4ffd-bf01-275d92ea6ede', N'04', N'Chi kinh phí KCB tại Trường Sa', 2024, CAST(N'2024-01-12T09:56:00' AS SmallDateTime), CAST(N'2022-07-26T16:03:00' AS SmallDateTime), N'admin', N'admin', N'', 1, N'9010006', N'9010006, 9010006-010-011-0001, 9010006-010-011-0001-0000, 9010006-010-011-0001-0001, 9010006-010-011-0001-0002, 9010006-010-011-0001-0003, 9010006-010-011-0002, 9010006-010-011-0002-0000, 9010006-010-011-0002-0001, 9010006-010-011-0002-0002, 9010006-010-011-0002-0003')
GO
INSERT [dbo].[BH_DM_LoaiChi] ([iID], [sMaLoaiChi], [sTenDanhMucLoaiChi], [iNamLamViec], [dNgaySua], [dNgayTao], [sNguoiSua], [sNguoiTao], [sMoTa], [iTrangThai], [sLNS], [sDSXauNoiMa]) VALUES (N'f2c33654-12dd-4468-8368-b361ade457fc', N'08', N'Chi hỗ trợ BHTN', 2024, CAST(N'2023-12-11T16:52:00' AS SmallDateTime), CAST(N'2023-12-07T10:52:00' AS SmallDateTime), N'admin', N'admin', N'', 1, N'9010010', N'9010010, 9010010-010-011-0001, 9010010-010-011-0001-0001, 9010010-010-011-0001-0002, 9010010-010-011-0001-0003, 9010010-010-011-0001-0004, 9010010-010-011-0001-0005, 9010010-010-011-0001-0006, 9010010-010-011-0002, 9010010-010-011-0002-0001, 9010010-010-011-0002-0002, 9010010-010-011-0002-0003, 9010010-010-011-0002-0004, 9010010-010-011-0002-0005, 9010010-010-011-0002-0006')
GO
INSERT [dbo].[BH_DM_LoaiChi] ([iID], [sMaLoaiChi], [sTenDanhMucLoaiChi], [iNamLamViec], [dNgaySua], [dNgayTao], [sNguoiSua], [sNguoiTao], [sMoTa], [iTrangThai], [sLNS], [sDSXauNoiMa]) VALUES (N'6d0cb545-dc20-4d9d-be72-2b133c1e9774', N'03', N'Chi kinh phí KCB tại quân y đơn vị ', 2023, CAST(N'2024-01-12T09:57:00' AS SmallDateTime), CAST(N'2022-07-26T16:03:00' AS SmallDateTime), N'admin', N'admin', N'', 1, N'9010004', N'9010004, 9010004-010-011-0001, 9010004-010-011-0001-0000, 9010004-010-011-0001-0001, 9010004-010-011-0001-0002, 9010004-010-011-0001-0003, 9010004-010-011-0002, 9010004-010-011-0002-0000, 9010004-010-011-0002-0001, 9010004-010-011-0002-0002, 9010004-010-011-0002-0003')
GO
INSERT [dbo].[BH_DM_LoaiChi] ([iID], [sMaLoaiChi], [sTenDanhMucLoaiChi], [iNamLamViec], [dNgaySua], [dNgayTao], [sNguoiSua], [sNguoiTao], [sMoTa], [iTrangThai], [sLNS], [sDSXauNoiMa]) VALUES (N'3e321e6d-8ea6-40de-bbb9-7c5befeb387d', N'04', N'Chi kinh phí KCB tại Trường Sa', 2023, CAST(N'2024-01-12T09:57:00' AS SmallDateTime), CAST(N'2022-07-26T16:03:00' AS SmallDateTime), N'admin', N'admin', N'', 1, N'9010006', N'9010006, 9010006-010-011-0001, 9010006-010-011-0001-0000, 9010006-010-011-0001-0001, 9010006-010-011-0001-0002, 9010006-010-011-0001-0003, 9010006-010-011-0002, 9010006-010-011-0002-0000, 9010006-010-011-0002-0001, 9010006-010-011-0002-0002, 9010006-010-011-0002-0003')
GO
INSERT [dbo].[BH_DM_LoaiChi] ([iID], [sMaLoaiChi], [sTenDanhMucLoaiChi], [iNamLamViec], [dNgaySua], [dNgayTao], [sNguoiSua], [sNguoiTao], [sMoTa], [iTrangThai], [sLNS], [sDSXauNoiMa]) VALUES (N'0bd3f182-1261-472f-8e01-fb8ce57b97c2', N'07', N'Chi kinh phí chăm sóc sức khỏe ban đầu HSSV & NLĐ', 2023, CAST(N'2024-01-16T17:05:00' AS SmallDateTime), CAST(N'2022-07-26T16:03:00' AS SmallDateTime), N'admin', N'admin', N'', 1, N'905,9050001,9050002', N'905, 9050001-010-011-0001, 9050002-010-011-0002')
GO
INSERT [dbo].[BH_DM_LoaiChi] ([iID], [sMaLoaiChi], [sTenDanhMucLoaiChi], [iNamLamViec], [dNgaySua], [dNgayTao], [sNguoiSua], [sNguoiTao], [sMoTa], [iTrangThai], [sLNS], [sDSXauNoiMa]) VALUES (N'107ff3e7-ee97-4b26-b148-b0631f60a2fa', N'05', N'Chi từ nguồn kết dư Quỹ KCB BHYT quân nhân', 2023, CAST(N'2023-12-11T16:52:00' AS SmallDateTime), CAST(N'2022-07-26T16:03:00' AS SmallDateTime), N'admin', N'admin', N'', 1, N'9010008', N'9010008, 9010008-010-011-0001, 9010008-010-011-0002')
GO
INSERT [dbo].[BH_DM_LoaiChi] ([iID], [sMaLoaiChi], [sTenDanhMucLoaiChi], [iNamLamViec], [dNgaySua], [dNgayTao], [sNguoiSua], [sNguoiTao], [sMoTa], [iTrangThai], [sLNS], [sDSXauNoiMa]) VALUES (N'c6e13dda-639c-45b8-8bdb-cf2a5d6822ed', N'06', N'Kinh phí mua sắm trang thiết bị y tế', 2023, CAST(N'2023-12-11T16:52:00' AS SmallDateTime), CAST(N'2023-12-07T10:58:00' AS SmallDateTime), N'admin', N'admin', N'', 1, N'9010009', N'9010009, 9010009-010-011-0001, 9010009-010-011-0002')
GO
INSERT [dbo].[BH_DM_LoaiChi] ([iID], [sMaLoaiChi], [sTenDanhMucLoaiChi], [iNamLamViec], [dNgaySua], [dNgayTao], [sNguoiSua], [sNguoiTao], [sMoTa], [iTrangThai], [sLNS], [sDSXauNoiMa]) VALUES (N'e6059464-c63d-49a4-bfa7-bb44b3d442da', N'07', N'Chi kinh phí chăm sóc sức khỏe ban đầu HSSV & NLĐ', 2024, CAST(N'2024-01-16T17:05:00' AS SmallDateTime), CAST(N'2022-07-26T16:03:00' AS SmallDateTime), N'admin', N'admin', N'', 1, N'905,9050001,9050002', N'905, 9050001-010-011-0001, 9050002-010-011-0002')
GO
INSERT [dbo].[BH_DM_LoaiChi] ([iID], [sMaLoaiChi], [sTenDanhMucLoaiChi], [iNamLamViec], [dNgaySua], [dNgayTao], [sNguoiSua], [sNguoiTao], [sMoTa], [iTrangThai], [sLNS], [sDSXauNoiMa]) VALUES (N'b04c48e3-1bbc-4981-b59e-bbdf0ccece84', N'02', N'Chi kinh phí quản lý BHXH, BHYT', 2023, CAST(N'2024-06-10T08:02:00' AS SmallDateTime), CAST(N'2022-07-26T16:03:00' AS SmallDateTime), N'admin', N'admin', N'', 1, N'9010003', N'9010003, 9010003-010, 9010003-010-011, 9010003-010-011-6000, 9010003-010-011-6000-6001, 9010003-010-011-6000-6003, 9010003-010-011-6000-6049, 9010003-010-011-6050, 9010003-010-011-6050-6051, 9010003-010-011-6050-6099, 9010003-010-011-6100, 9010003-010-011-6100-6101, 9010003-010-011-6100-6102, 9010003-010-011-6100-6103, 9010003-010-011-6100-6105, 9010003-010-011-6100-6107, 9010003-010-011-6100-6113, 9010003-010-011-6100-6114, 9010003-010-011-6100-6115, 9010003-010-011-6100-6116, 9010003-010-011-6100-6121, 9010003-010-011-6100-6149, 9010003-010-011-6150, 9010003-010-011-6150-6155, 9010003-010-011-6150-6199, 9010003-010-011-6200, 9010003-010-011-6200-6201, 9010003-010-011-6200-6202, 9010003-010-011-6200-6249, 9010003-010-011-6250, 9010003-010-011-6250-6251, 9010003-010-011-6250-6252, 9010003-010-011-6250-6253, 9010003-010-011-6250-6254, 9010003-010-011-6250-6257, 9010003-010-011-6250-6299, 9010003-010-011-6300, 9010003-010-011-6300-6301, 9010003-010-011-6300-6302, 9010003-010-011-6300-6303, 9010003-010-011-6300-6304, 9010003-010-011-6300-6349, 9010003-010-011-6400, 9010003-010-011-6400-6401, 9010003-010-011-6400-6404, 9010003-010-011-6400-6449, 9010003-010-012, 9010003-010-012-6500, 9010003-010-012-6500-6501, 9010003-010-012-6500-6502, 9010003-010-012-6500-6503, 9010003-010-012-6500-6504, 9010003-010-012-6500-6505, 9010003-010-012-6500-6549, 9010003-010-012-6550, 9010003-010-012-6550-6551, 9010003-010-012-6550-6552, 9010003-010-012-6550-6553, 9010003-010-012-6550-6599, 9010003-010-012-6600, 9010003-010-012-6600-6601, 9010003-010-012-6600-6603, 9010003-010-012-6600-6605, 9010003-010-012-6600-6606, 9010003-010-012-6600-6608, 9010003-010-012-6600-6618, 9010003-010-012-6600-6649, 9010003-010-012-6650, 9010003-010-012-6650-6651, 9010003-010-012-6650-6652, 9010003-010-012-6650-6653, 9010003-010-012-6650-6654, 9010003-010-012-6650-6655, 9010003-010-012-6650-6656, 9010003-010-012-6650-6657, 9010003-010-012-6650-6658, 9010003-010-012-6650-6699, 9010003-010-012-6700, 9010003-010-012-6700-6701, 9010003-010-012-6700-6702, 9010003-010-012-6700-6703, 9010003-010-012-6700-6704, 9010003-010-012-6700-6749, 9010003-010-012-6750, 9010003-010-012-6750-6751, 9010003-010-012-6750-6752, 9010003-010-012-6750-6754, 9010003-010-012-6750-6755, 9010003-010-012-6750-6756, 9010003-010-012-6750-6757, 9010003-010-012-6750-6758, 9010003-010-012-6750-6761, 9010003-010-012-6750-6799, 9010003-010-012-6800, 9010003-010-012-6800-6801, 9010003-010-012-6800-6802, 9010003-010-012-6800-6803, 9010003-010-012-6800-6805, 9010003-010-012-6800-6806, 9010003-010-012-6800-6849, 9010003-010-012-6850, 9010003-010-012-6850-6851, 9010003-010-012-6850-6852, 9010003-010-012-6850-6853, 9010003-010-012-6850-6855, 9010003-010-012-6850-6899, 9010003-010-012-6900, 9010003-010-012-6900-6901, 9010003-010-012-6900-6902, 9010003-010-012-6900-6903, 9010003-010-012-6900-6905, 9010003-010-012-6900-6907, 9010003-010-012-6900-6912, 9010003-010-012-6900-6913, 9010003-010-012-6900-6921, 9010003-010-012-6900-6949, 9010003-010-012-6950, 9010003-010-012-6950-6951, 9010003-010-012-6950-6952, 9010003-010-012-6950-6953, 9010003-010-012-6950-6954, 9010003-010-012-6950-6955, 9010003-010-012-6950-6956, 9010003-010-012-6950-6999, 9010003-010-012-7000, 9010003-010-012-7000-7001, 9010003-010-012-7000-7004, 9010003-010-012-7000-7012, 9010003-010-012-7000-7017, 9010003-010-012-7000-7049, 9010003-010-012-7000-7049-0001, 9010003-010-012-7000-7049-0002, 9010003-010-012-7000-7049-0003, 9010003-010-012-7000-7049-0004, 9010003-010-012-7000-7049-0005, 9010003-010-013, 9010003-010-013-7750, 9010003-010-013-7750-7751, 9010003-010-013-7750-7756, 9010003-010-013-7750-7757, 9010003-010-013-7750-7761, 9010003-010-013-7750-7767, 9010003-010-013-7750-7799, 9010003-010-013-7750-7799-0001, 9010003-010-013-7850, 9010003-010-013-7850-7851, 9010003-010-013-7850-7852, 9010003-010-013-7850-7853, 9010003-010-013-7850-7854, 9010003-010-013-7850-7899, 9010003-010-013-7900, 9010003-010-013-7900-7903, 9010003-010-013-7900-7949, 9010003-010-013-7950, 9010003-010-013-7950-7951, 9010003-010-013-7950-7952, 9010003-010-013-7950-7954, 9010003-010-013-7950-7999')
GO
INSERT [dbo].[BH_DM_LoaiChi] ([iID], [sMaLoaiChi], [sTenDanhMucLoaiChi], [iNamLamViec], [dNgaySua], [dNgayTao], [sNguoiSua], [sNguoiTao], [sMoTa], [iTrangThai], [sLNS], [sDSXauNoiMa]) VALUES (N'1e909740-3235-4be4-b992-6c7d101ec384', N'01', N'Chi các chế độ BHXH', 2023, CAST(N'2024-06-10T08:02:00' AS SmallDateTime), CAST(N'2022-07-26T16:03:00' AS SmallDateTime), N'admin', N'admin', N'', 1, N'901,9010001,9010002', N'901, 9010001, 9010001-010-011-0001, 9010001-010-011-0001-0001, 9010001-010-011-0001-0001-0001-01, 9010001-010-011-0001-0001-0001-01-01, 9010001-010-011-0001-0001-0001-01-02, 9010001-010-011-0001-0001-0001-02, 9010001-010-011-0001-0001-0001-02-01, 9010001-010-011-0001-0001-0001-02-02, 9010001-010-011-0001-0002, 9010001-010-011-0001-0003, 9010001-010-011-0002, 9010001-010-011-0002-0001-0001-00, 9010001-010-011-0002-0001-0001-00-01, 9010001-010-011-0002-0001-0001-00-02, 9010001-010-011-0002-0001-0002-00, 9010001-010-011-0002-0001-0002-00-01, 9010001-010-011-0002-0001-0002-00-02, 9010001-010-011-0002-0001-0003-00, 9010001-010-011-0002-0001-0003-00-01, 9010001-010-011-0002-0001-0003-00-02, 9010001-010-011-0002-0001-0004-00, 9010001-010-011-0003, 9010001-010-011-0003-0001-0001-00, 9010001-010-011-0003-0001-0002-00, 9010001-010-011-0003-0001-0003-00, 9010001-010-011-0003-0001-0004-00, 9010001-010-011-0003-0001-0005-00, 9010001-010-011-0003-0001-0006-00, 9010001-010-011-0003-0001-0007-00, 9010001-010-011-0003-0001-0008-00, 9010001-010-011-0003-0001-0009-00, 9010001-010-011-0004, 9010001-010-011-0004-0001-0001-00, 9010001-010-011-0004-0001-0002-00, 9010001-010-011-0005, 9010001-010-011-0005-0001-0001-00, 9010001-010-011-0005-0001-0002-00, 9010001-010-011-0006, 9010001-010-011-0006-0001-0001-00, 9010001-010-011-0007, 9010001-010-011-0007-0001-0001-00, 9010001-010-011-0007-0001-0002-00, 9010001-010-011-0008, 9010001-010-011-0008-0001-0001-00, 9010001-010-011-0008-0001-0002-00, 9010001-010-011-0008-0001-0003-00, 9010002, 9010002-010-011-0001, 9010002-010-011-0001-0001, 9010002-010-011-0001-0001-0001-01, 9010002-010-011-0001-0001-0001-01-01, 9010002-010-011-0001-0001-0001-01-02, 9010002-010-011-0001-0001-0001-02, 9010002-010-011-0001-0001-0001-02-01, 9010002-010-011-0001-0001-0001-02-02, 9010002-010-011-0001-0002, 9010002-010-011-0001-0003, 9010002-010-011-0002, 9010002-010-011-0002-0001-0001-00, 9010002-010-011-0002-0001-0001-00-01, 9010002-010-011-0002-0001-0001-00-02, 9010002-010-011-0002-0001-0002-00, 9010002-010-011-0002-0001-0002-00-01, 9010002-010-011-0002-0001-0002-00-02, 9010002-010-011-0002-0001-0003-00, 9010002-010-011-0002-0001-0003-00-01, 9010002-010-011-0002-0001-0003-00-02, 9010002-010-011-0002-0001-0004-00, 9010002-010-011-0003, 9010002-010-011-0003-0001-0001-00, 9010002-010-011-0003-0001-0002-00, 9010002-010-011-0003-0001-0003-00, 9010002-010-011-0003-0001-0004-00, 9010002-010-011-0003-0001-0005-00, 9010002-010-011-0003-0001-0006-00, 9010002-010-011-0003-0001-0007-00, 9010002-010-011-0003-0001-0008-00, 9010002-010-011-0003-0001-0009-00, 9010002-010-011-0004, 9010002-010-011-0004-0001-0001-00, 9010002-010-011-0004-0001-0002-00, 9010002-010-011-0005, 9010002-010-011-0005-0001-0001-00, 9010002-010-011-0005-0001-0002-00, 9010002-010-011-0006, 9010002-010-011-0006-0001-0001-00, 9010002-010-011-0007, 9010002-010-011-0007-0001-0001-00, 9010002-010-011-0007-0001-0002-00, 9010002-010-011-0008, 9010002-010-011-0008-0001-0001-00, 9010002-010-011-0008-0001-0002-00, 9010002-010-011-0008-0001-0003-00')
GO
