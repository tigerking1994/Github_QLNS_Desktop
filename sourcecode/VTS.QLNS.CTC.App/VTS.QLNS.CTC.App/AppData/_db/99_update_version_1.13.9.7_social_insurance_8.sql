/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_gttrocap_huutri_phucvien_xuatngu_thoiviec_tutuat]    Script Date: 2/5/2024 5:32:06 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_qtcq_gttrocap_huutri_phucvien_xuatngu_thoiviec_tutuat]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_qtcq_gttrocap_huutri_phucvien_xuatngu_thoiviec_tutuat]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_rpt_thamdinh_quyet_toan_kpql_kpktsdk]    Script Date: 2/5/2024 5:32:06 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_rpt_thamdinh_quyet_toan_kpql_kpktsdk]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_rpt_thamdinh_quyet_toan_kpql_kpktsdk]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_rpt_thamdinh_quyet_toan_kpql_kpktsdk]    Script Date: 2/5/2024 5:32:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_rpt_thamdinh_quyet_toan_kpql_kpktsdk]
	@NamLamViec int,
	@LstSelectedUnit ntext,
	@TypeValue int
AS
BEGIN
		declare @DTNAMTRUOC table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), FTienDaThucHienNamTruoc float);
		declare @DTNAMNAY table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), FTienNamNay float);
		declare @QT table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), FTienQuyetToan float);
		
		INSERT INTO @DTNAMTRUOC (sTenDonVI, idDonVi, FTienDaThucHienNamTruoc)
			SELECT dt_dv.sTenDonVi, dt_dv.id idDonVi, SUM(IsNull(A.ThuBHXHNLDDong, 0))
			FROM
			  (
				SELECT ct.iID_MaDonVi, IsNull(ctct.fSoThamDinh, 0) ThuBHXHNLDDong
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN BH_ThamDinhQuyetToan_ChungTu ct ON ct.iID_BH_TDQT_ChungTu = ctct.iID_BH_TDQT_ChungTu AND ctct.iNamLamViec = @NamLamViec
				WHERE ct.iNamLamViec = @NamLamViec AND ((@TypeValue = 6 AND ctct.iMa = 247) OR (@TypeValue = 8 AND ctct.iMa = 235))
			   ) AS A 
			   JOIN (
						SELECT iID_MaDonVi AS id, sTenDonVi, iLoai
						FROM DonVi
						WHERE iTrangThai = 1
						AND iNamLamViec = @NamLamViec
						AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
				) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
			GROUP BY dt_dv.sTenDonVi, dt_dv.id;

		INSERT INTO @DTNAMNAY (sTenDonVI, idDonVi, FTienNamNay)
			SELECT dt_dv.sTenDonVi, dt_dv.id idDonVi, SUM(IsNull(A.ThuBHXHNLDDong, 0))
			FROM
			  (
				SELECT ct.iID_MaDonVi, IsNull(ctct.fSoThamDinh, 0) ThuBHXHNLDDong
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN BH_ThamDinhQuyetToan_ChungTu ct ON ct.iID_BH_TDQT_ChungTu = ctct.iID_BH_TDQT_ChungTu AND ctct.iNamLamViec = @NamLamViec
				WHERE ct.iNamLamViec = @NamLamViec AND ((@TypeValue = 6 AND ctct.iMa = 248) OR (@TypeValue = 8 AND ctct.iMa = 236))
			   ) AS A 
			   JOIN (
						SELECT iID_MaDonVi AS id, sTenDonVi, iLoai
						FROM DonVi
						WHERE iTrangThai = 1
						AND iNamLamViec = @NamLamViec
						AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
				) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
			GROUP BY dt_dv.sTenDonVi, dt_dv.id;

		INSERT INTO @QT (sTenDonVI, idDonVi, FTienQuyetToan)
			SELECT dt_dv.sTenDonVi, dt_dv.id idDonVi, SUM(IsNull(A.ThuBHXHNLDDong, 0))
			FROM
			  (
				SELECT ct.iID_MaDonVi, IsNull(ctct.fSoThamDinh, 0) ThuBHXHNLDDong
				FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
				LEFT JOIN BH_ThamDinhQuyetToan_ChungTu ct ON ct.iID_BH_TDQT_ChungTu = ctct.iID_BH_TDQT_ChungTu AND ctct.iNamLamViec = @NamLamViec
				WHERE ct.iNamLamViec = @NamLamViec AND ((@TypeValue = 6 AND ctct.iMa = 252) OR (@TypeValue = 8 AND ctct.iMa = 238))
			   ) AS A 
			   JOIN (
						SELECT iID_MaDonVi AS id, sTenDonVi, iLoai
						FROM DonVi
						WHERE iTrangThai = 1
						AND iNamLamViec = @NamLamViec
						AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
				) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
			GROUP BY dt_dv.sTenDonVi, dt_dv.id;

		SELECT dtnt.idDonVi, dtnt.sTenDonVI, 
		IsNull(dtnt.FTienDaThucHienNamTruoc, 0) FTienDaThucHienNamTruoc,
		IsNull(dtnn.FTienNamNay, 0) FTienNamNay,
		IsNull(dtnt.FTienDaThucHienNamTruoc, 0) + IsNull(dtnn.FTienNamNay, 0) FTienCong,
		IsNull(qt.FTienQuyetToan, 0) FTienQuyetToan,
		CASE 
			WHEN (IsNull(qt.FTienQuyetToan, 0) - (IsNull(dtnt.FTienDaThucHienNamTruoc, 0) + IsNull(dtnn.FTienNamNay, 0))) < 0
			THEN ABS(IsNull(qt.FTienQuyetToan, 0) - (IsNull(dtnt.FTienDaThucHienNamTruoc, 0) + IsNull(dtnn.FTienNamNay, 0))) END as FTienThieu,
		CASE WHEN (IsNull(qt.FTienQuyetToan, 0) - (IsNull(dtnt.FTienDaThucHienNamTruoc, 0) + IsNull(dtnn.FTienNamNay, 0))) > 0
			THEN ABS(IsNull(qt.FTienQuyetToan, 0) - (IsNull(dtnt.FTienDaThucHienNamTruoc, 0) + IsNull(dtnn.FTienNamNay, 0))) END as FTienThua
		FROM @DTNAMTRUOC dtnt
		LEFT JOIN @DTNAMNAY dtnn ON dtnt.idDonVi = dtnn.idDonVi
		LEFT JOIN @QT qt ON dtnt.idDonVi = qt.idDonVi
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_gttrocap_huutri_phucvien_xuatngu_thoiviec_tutuat]    Script Date: 2/5/2024 5:32:06 PM ******/
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
			or gt.sXauNoiMa like '9010001-010-011-0004%'
			or gt.sXauNoiMa like '9010001-010-011-0005%'
			or gt.sXauNoiMa like '9010001-010-011-0006%'
			or gt.sXauNoiMa like '9010001-010-011-0007%'
			or gt.sXauNoiMa like '9010001-010-011-0008%')
		and gt.iNamLamViec = @INamLamViec and gt.iQuy = @IQuy and gt.iiD_MaDonVi in (SELECT * FROM f_split(@IdMaDonVi))

	--- 9010001-010-011-0004 Tro cap Huu tri
	SELECT * INTO #tempHuuTri FROM
	(
	SELECT 1 bHangCha
			, N'I' STT
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
			, 0 as FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
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
	) tblHuutri

	 if (SELECT COUNT(1) FROM #tempHuuTri) > 2
		UPDATE #tempHuuTri set bHasData = 1

	--- 9010001-010-011-0005 Tro cap phuc vien
	SELECT * INTO #tempPhucVien FROM
	(
	SELECT 1 bHangCha
			, N'II' STT
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
			, 0 as FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
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
	) tblPhucVien

	 if (SELECT COUNT(1) FROM #tempPhucVien) > 2
		UPDATE #tempPhucVien SET bHasData = 1

	--- 9010001-010-011-0006 Tro cap xuat ngu
	SELECT * INTO #tempXuatNgu FROM
	(
	SELECT 1 bHangCha
			, N'III' STT
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
	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, Convert(varchar(10),(COUNT(tbltc.sMaCapBac))) + N' Đồng chi' as sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCap1Lan
			--, 0 AS  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS  FTienTroCapMT
			--, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
			, 0 as FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-00065%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	) tblXuatNgu

	 if (SELECT COUNT(1) from #tempXuatNgu) > 2
		UPDATE #tempXuatNgu set bHasData = 1

	--- 9010001-010-011-0007 tro cap thoi viec
	SELECT * INTO #tempThoiViec FROM
	(
	SELECT 1 bHangCha
			, N'IV' STT
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
			, 0 as FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
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
	) tblThoiViec

	 if (SELECT COUNT(1) FROM #tempThoiViec) > 2
		UPDATE #tempThoiViec SET bHasData = 1

	--- 9010001-010-011-0008 tro cap tu tuat
	SELECT * INTO #tempTuTuat FROM
	(
	SELECT 1 bHangCha
			, N'IV' STT
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
			, 0 as FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
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
	) tblTuTuat

	 if (SELECT COUNT(1) FROM #tempTuTuat) > 2
		UPDATE #tempTuTuat SET bHasData = 1

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

END
;
;
;
;
;
GO
