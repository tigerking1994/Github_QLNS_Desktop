/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Nhomdt_Detail]    Script Date: 9/12/2024 2:59:44 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Nhomdt_Detail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Nhomdt_Detail]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Nhomdt_Detail]    Script Date: 9/12/2024 2:59:44 PM ******/
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
			, N'Khối dự toán' STT
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
			, N'Khối hạch toán' STT
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
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_tonghop_noidung_kpql]    Script Date: 9/12/2024 3:58:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_tonghop_noidung_kpql]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_tonghop_noidung_kpql]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_tonghop_noidung_kpql]    Script Date: 9/12/2024 3:58:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_tonghop_noidung_kpql]
@iDChungTu uniqueidentifier,
@IIDDonVi nvarchar(max),
@NamLamViec int,
@DonViTinh int ,
@IsMillionRound bit
AS
BEGIN
	SELECT * into #tblChiQuanLy FROM (
	SELECT '1' AS STT, N'Chi kinh quản lý BHXH, BHYT, BHTN'  AS SNoiDung, 0 FSoTien , 1 bHangCha
	union all
	SELECT '' AS STT,N'Trong đó: Ngành Cán bộ' AS SNoiDung, 
	CASE WHEN @IsMillionRound=1 THEN round((SUM (isnull(fSoTien,0))/ 1000000),0)*1000000/ @DonViTinh ELSE round((SUM (isnull(fSoTien,0))/ @DonViTinh),0) END FSoTien,
	0 bHangCha 
	FROM BH_DuToan_CTCT_KPQL WHERE iID_ChungTu=@iDChungTu AND iID_MaDonVi in (select * from splitstring(@IIDDonVi)) AND iNamLamViec=@NamLamViec
	AND sXauNoiMa='9010011-010-0001'
	union all
	SELECT '' AS STT,N'                   Ngành Quân lực' AS SNoiDung,
	CASE WHEN @IsMillionRound=1 THEN round((SUM (isnull(fSoTien,0))/ 1000000),0)*1000000/ @DonViTinh ELSE round((SUM (isnull(fSoTien,0))/ @DonViTinh),0) END FSoTien,
	0 bHangCha FROM BH_DuToan_CTCT_KPQL WHERE iID_ChungTu=@iDChungTu AND iID_MaDonVi in (select * from splitstring(@IIDDonVi)) AND iNamLamViec=@NamLamViec
	AND sXauNoiMa='9010011-010-0002'
	union all
	SELECT '' AS STT,N'                   Ngành Tài chính' AS SNoiDung,
CASE WHEN @IsMillionRound=1 THEN round((SUM (isnull(fSoTien,0))/ 1000000),0)*1000000/ @DonViTinh ELSE round((SUM (isnull(fSoTien,0))/ @DonViTinh),0) END FSoTien,
	0 bHangCha FROM BH_DuToan_CTCT_KPQL WHERE iID_ChungTu=@iDChungTu AND iID_MaDonVi in (select * from splitstring(@IIDDonVi)) AND iNamLamViec=@NamLamViec
	AND sXauNoiMa='9010011-010-0003'
	union all
	SELECT '' AS STT,N'                Ngành Quân y' AS SNoiDung,
CASE WHEN @IsMillionRound=1 THEN round((SUM (isnull(fSoTien,0))/ 1000000),0)*1000000/ @DonViTinh ELSE round((SUM (isnull(fSoTien,0))/ @DonViTinh),0) END FSoTien,
	0 bHangCha FROM BH_DuToan_CTCT_KPQL WHERE iID_ChungTu=@iDChungTu AND iID_MaDonVi in (select * from splitstring(@IIDDonVi)) AND iNamLamViec=@NamLamViec
	AND sXauNoiMa='9010011-010-0004'
	) bhxhbhytbhtn

	UPDATE A
	SET A.fSoTien=B.fSoTien
	FROM #tblChiQuanLy A, ( SELECT SUM(fSoTien) fSoTien from #tblChiQuanLy) B
	WHERE A.bHangCha=1
	
	SELECT * INTO #tblHoTroandChi FROM (
	SELECT '2' AS STT,N'Hỗ trợ cán bộ, nhân viên làm công tác BHXH, BHYT' AS SNoiDung,
	CASE WHEN @IsMillionRound=1 THEN round((SUM (isnull(fSoTien,0))/ 1000000),0)*1000000/ @DonViTinh ELSE round((SUM (isnull(fSoTien,0))/ @DonViTinh),0) END FSoTien,
	1 bHangCha FROM BH_DuToan_CTCT_KPQL WHERE iID_ChungTu=@iDChungTu AND iID_MaDonVi in (select * from splitstring(@IIDDonVi)) AND iNamLamViec=@NamLamViec
	AND sXauNoiMa='9010011-011'
	union all
	SELECT '3' AS STT,N'Chi tập huấn nghiệp vụ BHXH, BHYT, BHTN (Cơ quan Quân lực chủ trì phối hợp với cơ quan Tài chính, Cán bộ, Quân y tổ chức thực hiện)' AS SNoiDung,
	CASE WHEN @IsMillionRound=1 THEN round((SUM (isnull(fSoTien,0))/ 1000000),0)*1000000/ @DonViTinh ELSE round((SUM (isnull(fSoTien,0))/ @DonViTinh),0) END FSoTien,
	1 bHangCha FROM BH_DuToan_CTCT_KPQL WHERE iID_ChungTu=@iDChungTu AND iID_MaDonVi in (select * from splitstring(@IIDDonVi)) AND iNamLamViec=@NamLamViec
	AND sXauNoiMa='9010011-012') TBL
	
	SELECT * INTO #tblHoTroThu FROM
	(
	SELECT '4' AS STT,N'Hỗ trợ quản lý thu, chi BHXH, BHYT, BHTN ở đơn vị cơ sở (do ngành Tài chính quản lý báo cáo Thủ trưởng phân cấp cho đơn vị cơ sở)' AS SNoiDung, 0 FSoTien,1 bHangCha 
	union all
	SELECT '' AS STT,N'                    Trong đó: Chi thường xuyên đặc thù' AS SNoiDung,
	CASE WHEN @IsMillionRound=1 THEN round((SUM (isnull(fSoTien,0))/ 1000000),0)*1000000/ @DonViTinh ELSE round((SUM (isnull(fSoTien,0))/ @DonViTinh),0) END FSoTien,
	0 bHangCha FROM BH_DuToan_CTCT_KPQL WHERE iID_ChungTu=@iDChungTu AND iID_MaDonVi in (select * from splitstring(@IIDDonVi)) AND iNamLamViec=@NamLamViec
	AND sXauNoiMa LIKE '9010011-013-0001%'
	union all
	SELECT '' AS STT,N'                                  Chi không thường xuyên' AS SNoiDung,
	CASE WHEN @IsMillionRound=1 THEN round((SUM (isnull(fSoTien,0))/ 1000000),0)*1000000/ @DonViTinh ELSE round((SUM (isnull(fSoTien,0))/ @DonViTinh),0) END FSoTien,
	0 bHangCha FROM BH_DuToan_CTCT_KPQL WHERE iID_ChungTu=@iDChungTu AND iID_MaDonVi in (select * from splitstring(@IIDDonVi)) AND iNamLamViec=@NamLamViec
	AND sXauNoiMa = '9010011-013-0002') TBL

	UPDATE A
	set A.fSoTien=B.fSoTien
	FROM #tblHoTroThu A ,(SELECT SUM(fSoTien) fSoTien FROM #tblHoTroThu) B
	WHERE A.bHangCha=1

	SELECT * FROM (
	SELECT * FROM #tblChiQuanLy
	UNION ALL 
	SELECT * FROM #tblHoTroandChi
	UNION ALL 
	SELECT * FROM #tblHoTroThu) TBL 

	DROP TABLE #tblChiQuanLy
	DROP TABLE #tblHoTroandChi
	DROP TABLE #tblHoTroThu
END
;
;
;
;
;
;
GO
