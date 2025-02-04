/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_gttrocap_huutri_phucvien_xuatngu_thoiviec_tutuat]    Script Date: 12/18/2023 2:54:14 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_qtcq_gttrocap_huutri_phucvien_xuatngu_thoiviec_tutuat]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_qtcq_gttrocap_huutri_phucvien_xuatngu_thoiviec_tutuat]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_chuatonghopquytruoc_bhxh]    Script Date: 12/18/2023 2:54:14 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_chuatonghopquytruoc_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_chuatonghopquytruoc_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_bhxh]    Script Date: 12/18/2023 2:54:14 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_bhxh]    Script Date: 12/18/2023 2:54:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_bhxh]
@INamLamViec int,
@IdMaDonVi nvarchar(max),
@IQuy int,
@Donvitinh int
as
begin
	---Lấy thông tin chi tiết giai thich tro cap
	SELECT gt.* into #TBL_TroCapTaiNan 
			FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gt
			LEFT JOIN BH_QTC_Quy_CheDoBHXH ct ON gt.iID_QTC_Quy_ChungTu=ct.ID_QTC_Quy_CheDoBHXH
			LEFT JOIN DonVi dv on dv.iID_MaDonVi=ct.iID_MaDonVi
			WHERE (gt.sXauNoiMa like '9010001-010-011-0003%' or gt.sXauNoiMa like '9020001-010-011-0003%')
					AND gt.iNamLamViec = @INamLamViec and gt.iQuy = @IQuy 
					AND dv.iNamLamViec=@INamLamViec
					AND dv.iID_MaDonVi  IN (select * from dbo.splitstring(@IdMaDonVi)) 

		SELECT
			ROW_NUMBER() OVER(ORDER BY tbltctn.sTenCanBo ASC) AS STT
			,tbltctn.sTenCanBo
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

			------ Chi giám định mức suy giảm KNLĐ (người)1
			--, CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) /@Donvitinh ELSE 0 END FTienGiamDinh
			--, CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END FTienGiamDinhTL
			------ Trợ cấp 1 lần (người)2
			--, CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end FTienTroCap1Lan
			--, CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END FTienTroCap1LanTL

			----- - Chi hỗ Trợ phòng người (người)
			---- - Chi h.trợ chuyển đổi n.nghiệp (người)
			--, CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END FTienTCTP
			--, CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END FTienTCTPTL
			------ Trợ cấp hàng tháng (người)
			--, CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end FTienTCHangThang
			--, CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end FTienTCHangThangTL
			----- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người)
			--, CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END FTienTCPHCNvPV
			--, CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END FTienTCPHCNvPVTL
			----- - Trợ cấp chết do TNLD. BNN (người)
			--, CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END FTienTCCDTNLD
			--, CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh  ELSE 0 END FTienTCCDTNLDTL
			----- - DS, PHSK sau TNLĐ, BNN (người)
			--, CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ISoNgayDSPHSK
			--, CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ISoNgayDSPHSKTL

			--, CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END FTienDSPHSK
			--, CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END FTienDSPHSKTL
		
		FROM #TBL_TroCapTaiNan tbltctn
		GROUP BY tbltctn.sTenCanBo, tbltctn.sMaCapBac
			, tbltctn.sSoQuyetDinh
			, tbltctn.dNgayQuyetDinh
			,tbltctn.sXauNoiMa 
		

		DROP TABLE #TBL_TroCapTaiNan


end
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_chuatonghopquytruoc_bhxh]    Script Date: 12/18/2023 2:54:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_chuatonghopquytruoc_bhxh]
@INamLamViec int,
@IdMaDonVi nvarchar(max),
@IQuy int,
@Donvitinh int
as
begin
	---Lấy thông tin chi tiết giai thich tro cap
	SELECT gt.* into #TBL_TroCapTaiNan 
			FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gt
			LEFT JOIN BH_QTC_Quy_CheDoBHXH ct ON gt.iID_QTC_Quy_ChungTu=ct.ID_QTC_Quy_CheDoBHXH
			LEFT JOIN DonVi dv on dv.iID_MaDonVi=ct.iID_MaDonVi
			WHERE (gt.sXauNoiMa like '9010001-010-011-0003%' or gt.sXauNoiMa like '9020001-010-011-0003%')
					AND gt.iNamLamViec = @INamLamViec AND gt.iQuy = @IQuy 
					AND dv.iNamLamViec=@INamLamViec
					AND ct.iLoaiTongHop =1
					AND dv.iID_MaDonVi  IN (select * from dbo.splitstring(@IdMaDonVi)) 

		SELECT
			ROW_NUMBER() OVER(ORDER BY tbltctn.sTenCanBo ASC) AS STT
			,tbltctn.sTenCanBo
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

			------ Chi giám định mức suy giảm KNLĐ (người)1
			--, CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) /@Donvitinh ELSE 0 END FTienGiamDinh
			--, CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END FTienGiamDinhTL
			------ Trợ cấp 1 lần (người)2
			--, CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end FTienTroCap1Lan
			--, CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END FTienTroCap1LanTL

			----- - Chi hỗ Trợ phòng người (người)
			---- - Chi h.trợ chuyển đổi n.nghiệp (người)
			--, CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END FTienTCTP
			--, CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END FTienTCTPTL
			------ Trợ cấp hàng tháng (người)
			--, CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end FTienTCHangThang
			--, CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end FTienTCHangThangTL
			----- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người)
			--, CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END FTienTCPHCNvPV
			--, CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END FTienTCPHCNvPVTL
			----- - Trợ cấp chết do TNLD. BNN (người)
			--, CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END FTienTCCDTNLD
			--, CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh  ELSE 0 END FTienTCCDTNLDTL
			----- - DS, PHSK sau TNLĐ, BNN (người)
			--, CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ISoNgayDSPHSK
			--, CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ISoNgayDSPHSKTL

			--, CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END FTienDSPHSK
			--, CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END FTienDSPHSKTL
		
		FROM #TBL_TroCapTaiNan tbltctn
		GROUP BY tbltctn.sTenCanBo, tbltctn.sMaCapBac
			, tbltctn.sSoQuyetDinh
			, tbltctn.dNgayQuyetDinh
			,tbltctn.sXauNoiMa 
		

		DROP TABLE #TBL_TroCapTaiNan


end
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_gttrocap_huutri_phucvien_xuatngu_thoiviec_tutuat]    Script Date: 12/18/2023 2:54:14 PM ******/
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
			, N'TC Phục viên'as STenCanBo 
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
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, 0 AS  FTienTroCap1Lan
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
GO
