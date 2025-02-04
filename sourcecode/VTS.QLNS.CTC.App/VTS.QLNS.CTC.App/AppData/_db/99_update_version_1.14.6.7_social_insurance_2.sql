/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]    Script Date: 7/16/2024 2:19:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]    Script Date: 7/16/2024 2:19:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]
@INamLamViec int,
@IdMaDonVi nvarchar(max),
@Lns nvarchar(max),
@Donvitinh int,
@IsTongHop int
as
begin
	SET NOCOUNT ON;
		DECLARE @iDCT uniqueidentifier='00000000-0000-0000-0000-000000000000';
		DECLARE @fSoThamDinh INT;
		SELECT @fSoThamDinh = Sum(fSoThamDinh)
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet 
		WHERE iNamLamViec = @INamLamViec - 1 and iMa = 225 and iID_MaDonVi  IN (SELECT * FROM f_split(@IdMaDonVi))
	---Lấy danh sách mục lục ngân sách
		select 
			danhmuc.iID_MLNS as iID_MLNS,
			danhmuc.iID_MLNS_Cha,
			danhmuc.sLNS,
			danhmuc.sL,
			danhmuc.sK,
			danhmuc.sM,
			danhmuc.sTM,
			danhmuc.sTTM,
			danhmuc.sNG,
			danhmuc.sTNG,
			danhmuc.sXauNoiMa,
			danhmuc.sMoTa,
			danhmuc.bHangCha,
			danhmuc.sDuToanChiTietToi,
			danhmuc.bHangChaDuToan
		into #tblMucLucNganSach
		from BH_DM_MucLucNganSach as danhmuc
		where danhmuc.iNamLamViec = @INamLamViec and danhmuc.sLNs in (select * from splitstring(@Lns))
				and danhmuc.iTrangThai=1

	---Lấy danh sách chi tiết 
		select	
			qtcn_ct.sXauNoiMa,
			qtcn_ct.sNoiDung,
			Sum(qtcn_ct.fTien_DuToanNamTruocChuyenSang)/@Donvitinh as fTien_DuToanNamTruocChuyenSang,
			Sum(qtcn_ct.fTien_DuToanGiaoNamNay)/@Donvitinh as fTien_DuToanGiaoNamNay,
			Sum(qtcn_ct.fTien_TongDuToanDuocGiao)/@Donvitinh as fTien_TongDuToanDuocGiao,
			Sum(qtcn_ct.fTien_ThucChi)/@Donvitinh as fTien_ThucChi,
			Sum(qtcn_ct.fTienThua)/@Donvitinh as fTienThua,
			Sum(qtcn_ct.fTienThieu)/@Donvitinh as fTienThieu,
			Sum(qtcn_ct.fTiLeThucHienTrenDuToan) as fTiLeThucHienTrenDuToan
		into #tbl_qtcn_chitiet
		from BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet as  qtcn_ct
		inner join  BH_QTC_Nam_KCB_QuanYDonVi as qtcn on qtcn_ct.iID_QTC_Nam_KCB_QuanYDonVi = qtcn.ID_QTC_Nam_KCB_QuanYDonVi
		where qtcn.iID_MaDonVi in (select * from dbo.splitstring(@IdMaDonVi)) 
		and qtcn.iNamLamViec = @INamLamViec
		--and ((@IsTongHop = 0 and qtcn.bDaTongHop = 0 and qtcn.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  qtcn.sDSSoChungTuTongHop is not null))
		group by qtcn_ct.sXauNoiMa,qtcn_ct.sNoiDung

		---Kết quả hiển thị trả về chung tu thuong
	if @IsTongHop=0
		select 
			mucluc.iID_MLNS,
			mucluc.iID_MLNS_Cha,
			mucluc.sLNS,
			mucluc.sL,
			mucluc.sK,
			mucluc.sM,
			mucluc.sTM,
			mucluc.sTTM,
			mucluc.sNG,
			mucluc.sTNG,
			mucluc.sXauNoiMa,
			mucluc.bHangCha,
			mucluc.iID_MLNS as iID_MucLucNganSach,
			mucluc.sDuToanChiTietToi,
			mucluc.sMoTa as sNoiDung,
			mucluc.bHangChaDuToan,
			CASE WHEN mucluc.sXauNoiMa = @LNS THEN ROUND(@fSoThamDinh / @Donvitinh,0) ELSE 0 END as fTien_DuToanNamTruocChuyenSang, 
			CASE WHEN mucluc.sXauNoiMa = @LNS THEN ROUND(@fSoThamDinh / @Donvitinh,0) ELSE 0 END as fDuToanNamTruocChuyenSang, 
			ROUND(daDuToan.fTienDuToan,0) as fTien_DuToanGiaoNamNay,
			ROUND(chi_tiet.fTien_TongDuToanDuocGiao,0),
			ROUND(chi_tiet.fTien_ThucChi,0),
			ROUND(chi_tiet.fTienThua,0),
			ROUND(chi_tiet.fTienThieu,0),
			chi_tiet.fTiLeThucHienTrenDuToan

		from #tblMucLucNganSach as mucluc
		left join #tbl_qtcn_chitiet as chi_tiet on mucluc.sXauNoiMa = chi_tiet.sXauNoiMa
		LEFT JOIN (
			-- lấy ra dữ liệu dự toán
			SELECT 
				  SUM(cast(fTienTuChi as float)) AS fTienDuToan,
				  sXauNoiMa
		   FROM BH_DTC_PhanBoDuToanChi_ChiTiet
		   WHERE iID_DTC_PhanBoDuToanChi IN
			   (SELECT ID
				FROM BH_DTC_PhanBoDuToanChi
				WHERE sSoQuyetDinh <> ''
				  AND sSoQuyetDinh IS NOT NULL
				  AND iNamChungTu = @INamLamViec
				  AND bIsKhoa=1)
			 AND iID_MaDonVi in  (SELECT * FROM f_split(@IdMaDonVi))-- donvi
		   GROUP BY sXauNoiMa
		) daDuToan  on mucluc.sXauNoiMa=daDuToan.sXauNoiMa
		order by mucluc.sXauNoiMa
		---Kết quả hiển thị trả về chung tu cha
	else
		select 

			mucluc.iID_MLNS,
			mucluc.iID_MLNS_Cha,
			mucluc.sLNS,
			mucluc.sL,
			mucluc.sK,
			mucluc.sM,
			mucluc.sTM,
			mucluc.sTTM,
			mucluc.sNG,
			mucluc.sTNG,
			mucluc.sXauNoiMa,
			mucluc.bHangCha,
			mucluc.iID_MLNS as iID_MucLucNganSach,
			mucluc.sDuToanChiTietToi,
			mucluc.bHangChaDuToan,
			mucluc.sMoTa as sNoiDung,
			CASE WHEN mucluc.sXauNoiMa = @LNS THEN ROUND(@fSoThamDinh / @Donvitinh,0) ELSE 0 END as fTien_DuToanNamTruocChuyenSang, 
			CASE WHEN mucluc.sXauNoiMa = @LNS THEN ROUND(@fSoThamDinh / @Donvitinh,0) ELSE 0 END as fDuToanNamTruocChuyenSang, 
			ROUND(daDuToan.fTienDuToan,0) as fTien_DuToanGiaoNamNay,
			ROUND(chi_tiet.fTien_TongDuToanDuocGiao,0),
			ROUND(chi_tiet.fTien_ThucChi,0),
			ROUND(chi_tiet.fTienThua,0),
			ROUND(chi_tiet.fTienThieu,0),
			chi_tiet.fTiLeThucHienTrenDuToan

		from #tblMucLucNganSach as mucluc
		left join #tbl_qtcn_chitiet as chi_tiet on mucluc.sXauNoiMa = chi_tiet.sXauNoiMa
		LEFT JOIN (
			-- lấy ra dữ liệu dự toán
			SELECT 
				  SUM(cast(fTienTuChi as float)) AS fTienDuToan,
				  sXauNoiMa
		   FROM BH_DTC_DuToanChiTrenGiao_ChiTiet
		   WHERE iID_DTC_DuToanChiTrenGiao IN
			   (SELECT ID
				FROM BH_DTC_DuToanChiTrenGiao
				WHERE sSoQuyetDinh <> ''
				  AND sSoQuyetDinh IS NOT NULL
				  AND iNamLamViec = @INamLamViec
				  AND bIsKhoa=1)
			 AND iID_MaDonVi in  (SELECT * FROM f_split(@IdMaDonVi))-- donvi
		   GROUP BY sXauNoiMa
		) daDuToan  on mucluc.sXauNoiMa=daDuToan.sXauNoiMa
		order by mucluc.sXauNoiMa

		drop table #tblMucLucNganSach;
		drop table #tbl_qtcn_chitiet;

end
;
;
;
;
;
;
GO

update BH_DM_MucLucNganSach
SET sCPChiTietToi = NULL
where sXauNoiMa = '9010003-010-01'


/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_gttrocap_huutri_phucvien_xuatngu_thoiviec_tutuat]    Script Date: 7/ @Donvitinh7/2024 5:36:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_qtcq_gttrocap_huutri_phucvien_xuatngu_thoiviec_tutuat]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_qtcq_gttrocap_huutri_phucvien_xuatngu_thoiviec_tutuat]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Khoi_Detail]    Script Date: 7/ @Donvitinh7/2024 5:36:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Khoi_Detail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Khoi_Detail]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Khoi]    Script Date: 7/ @Donvitinh7/2024 5:36:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Khoi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Khoi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT]    Script Date: 7/ @Donvitinh7/2024 5:36:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_quykinhphi_quanly_chungtu_chi_tiet]    Script Date: 7/ @Donvitinh7/2024 5:36:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_quykinhphi_quanly_chungtu_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_quykinhphi_quanly_chungtu_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_khoi]    Script Date: 7/ @Donvitinh7/2024 5:36:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_khoi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_khoi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_bhxh]    Script Date: 7/ @Donvitinh7/2024 5:36:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_bhxh]    Script Date: 7/ @Donvitinh7/2024 5:36:05 PM ******/
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
			WHERE (gt.sXauNoiMa like '9010001-010-011-0003%' or gt.sXauNoiMa like '9010002-010-011-0003%')
					AND gt.iNamLamViec = @INamLamViec and gt.iQuy = @IQuy 
					AND dv.iNamLamViec=@INamLamViec
					AND dv.iID_MaDonVi  IN (select * from dbo.splitstring(@IdMaDonVi)) 

		SELECT
			ROW_NUMBER() OVER(ORDER BY tbltctn.sTenCanBo ASC) AS STT
			,tbltctn.sTenCanBo
			,tbltctn.sTenPhanHo
			, tbltctn.sMaCapBac
			, tbltctn.sSoQuyetDinh
			, CONVERT(varchar, (tbltctn.dNgayQuyetDinh), 101) As SNgayQuyetDinh

			---- Chi giám định mức suy giảm KNLĐ (người)1
			, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) /@Donvitinh ELSE 0 END 
			+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END) FTienGiamDinh
			
			
			---- Chi giám định mức suy giảm KNLĐ (người)1 truy lĩnh
			, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) /@Donvitinh ELSE 0 END 
			+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienGiamDinhTL

			---- Trợ cấp 1 lần (người)2
			, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END) FTienTroCap1Lan

			---- Trợ cấp 1 lần (người)2 truy lĩnh
			, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTroCap1LanTL


			--- - Chi hỗ Trợ phòng người (người)
			-- - Chi h.trợ chuyển đổi n.nghiệp (người)
			, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END) FTienTCTP
			
			--- - Chi hỗ Trợ phòng người (người) truy linh
			-- - Chi h.trợ chuyển đổi n.nghiệp (người) truy linh
			, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTCTPTL
			
			---- Trợ cấp hàng tháng (người)
			, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end 
			+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end ) FTienTCHangThang
			
			---- Trợ cấp hàng tháng (người) truy linh
			, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end 
			+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end ) FTienTCHangThangTL
			
			--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người)
			,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPV

			--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người) truy linh
			,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPVTL

			--- - Trợ cấp chết do TNLD. BNN (người) 
			,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh  ELSE 0 END )FTienTCCDTNLD

			--- - Trợ cấp chết do TNLD. BNN (người) truy linh
			,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh  ELSE 0 END )FTienTCCDTNLDTL

			--- - DS, PHSK sau TNLĐ, BNN (người)
			,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END 
			+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ) ISoNgayDSPHSK

			--- - DS, PHSK sau TNLĐ, BNN (người) truy linh
			,( CASE WHEN tbltctn.sXauNoiMa like '9010001-010-011-0003%' THEN Sum(tbltctn.iSoNgayTruyLinh)  ELSE 0 END 
			+ CASE WHEN tbltctn.sXauNoiMa like '9010002-010-011-0003%' THEN Sum(tbltctn.iSoNgayTruyLinh)  ELSE 0 END ) ISoNgayDSPHSKTL

			,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END) FTienDSPHSK
			
			,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienDSPHSKTL,
			0 IsHangCha
		FROM #TBL_TroCapTaiNan tbltctn
		GROUP BY tbltctn.sTenCanBo,
			tbltctn.sTenPhanHo,
			tbltctn.sMaCapBac
			, tbltctn.sSoQuyetDinh
			, tbltctn.dNgayQuyetDinh
			,tbltctn.sXauNoiMa 
		

		DROP TABLE #TBL_TroCapTaiNan


end
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_khoi]    Script Date: 7/ @Donvitinh7/2024 5:36:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_khoi]
@INamLamViec int,
@IdMaDonVi nvarchar(max),
@IQuy int,
@Donvitinh int
as
begin
	---Lấy thông tin chi tiết giai thich tro cap du toan
	SELECT gt.* into #TBL_TroCapTaiNanDuToan 
			FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gt
			LEFT JOIN BH_QTC_Quy_CheDoBHXH ct ON gt.iID_QTC_Quy_ChungTu=ct.ID_QTC_Quy_CheDoBHXH
			LEFT JOIN DonVi dv on dv.iID_MaDonVi=ct.iID_MaDonVi
			WHERE (gt.sXauNoiMa like '9010001-010-011-0003%' or gt.sXauNoiMa like '9010002-010-011-0003%')
					AND gt.iNamLamViec = @INamLamViec and gt.iQuy = @IQuy 
					AND dv.iNamLamViec=@INamLamViec
					AND dv.iID_MaDonVi  IN (select * from dbo.splitstring(@IdMaDonVi)) 
					AND dv.iKhoi=2
					AND ct.iNamChungTu=@INamLamViec
	---Lấy thông tin chi tiết giai thich tro cap hach toan
	SELECT gt.* into #TBL_TroCapTaiNanHachToan 
			FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gt
			LEFT JOIN BH_QTC_Quy_CheDoBHXH ct ON gt.iID_QTC_Quy_ChungTu=ct.ID_QTC_Quy_CheDoBHXH
			LEFT JOIN DonVi dv on dv.iID_MaDonVi=ct.iID_MaDonVi
			WHERE (gt.sXauNoiMa like '9010001-010-011-0003%' or gt.sXauNoiMa like '9010002-010-011-0003%')
					AND gt.iNamLamViec = @INamLamViec and gt.iQuy = @IQuy 
					AND dv.iNamLamViec=@INamLamViec
					AND dv.iID_MaDonVi  IN (select * from dbo.splitstring(@IdMaDonVi)) 
					AND dv.iKhoi=1
					AND ct.iNamChungTu=@INamLamViec

	---I. Khối dự toán
		SELECT 
		N'I. Khối dự toán' As STT
		,'' As sTenCanBo
		,'' As sTenPhanHo
		,'' As sMaCapBac
		,'' As sSoQuyetDinh
		,'' As SNgayQuyetDinh
		,0 As FTienGiamDinh
		,0 As FTienTroCap1Lan
		,0 As FTienTCTP
		,0 As FTienTCHangThang
		,0 As FTienTCPHCNvPV
		,0 as FTienTCCDTNLD
		,0 as ISoNgayDSPHSK
		,0 as FTienDSPHSK
		,0 As FTienGiamDinhTL
		,0 As FTienTroCap1LanTL
		,0 As FTienTCTPTL
		,0 As FTienTCHangThangTL
		,0 As FTienTCPHCNvPVTL
		,0 as FTienTCCDTNLDTL
		,0 as ISoNgayDSPHSKTL
		,0 as FTienDSPHSKTL
		,3 as type
		,1 IsHangCha
		,1 IsParent
		into #tempDuToan

		--- Total SQ DuToan
		SELECT 
		N'1. Sĩ quan' As STT
		,'' As sTenCanBo
		,'' As sTenPhanHo
		,'' As sMaCapBac
		,'' As sSoQuyetDinh
		,'' As SNgayQuyetDinh
		,0 As FTienGiamDinh
		,0 As FTienTroCap1Lan
		,0 As FTienTCTP
		,0 As FTienTCHangThang
		,0 As FTienTCPHCNvPV
		,0 as FTienTCCDTNLD
		,0 as ISoNgayDSPHSK
		,0 as FTienDSPHSK
		,0 As FTienGiamDinhTL
		,0 As FTienTroCap1LanTL
		,0 As FTienTCTPTL
		,0 As FTienTCHangThangTL
		,0 As FTienTCPHCNvPVTL
		,0 as FTienTCCDTNLDTL
		,0 as ISoNgayDSPHSKTL
		,0 as FTienDSPHSKTL
		,2 as type
		,1 IsHangCha
		,1 IsParent
		into #tempSQDuToan

		--- Total QNCN DuToan
		SELECT 
		N'2. QNCN' As STT
		,'' As sTenCanBo
		,'' As sTenPhanHo
		,'' As sMaCapBac
		,'' As sSoQuyetDinh
		,'' As SNgayQuyetDinh
		,0 As FTienGiamDinh
		,0 As FTienTroCap1Lan
		,0 As FTienTCTP
		,0 As FTienTCHangThang
		,0 As FTienTCPHCNvPV
		,0 as FTienTCCDTNLD
		,0 as ISoNgayDSPHSK
		,0 as FTienDSPHSK
		,0 As FTienGiamDinhTL
		,0 As FTienTroCap1LanTL
		,0 As FTienTCTPTL
		,0 As FTienTCHangThangTL
		,0 As FTienTCPHCNvPVTL
		,0 as FTienTCCDTNLDTL
		,0 as ISoNgayDSPHSKTL
		,0 as FTienDSPHSKTL
		,2 as type
		,1 IsHangCha
		,1 IsParent
		into #tempQNCNDuToan

		--- Total CNVCQP DuToan
		SELECT 
		N'3. CNVCQP' As STT
		,'' As sTenCanBo
		,'' As sTenPhanHo
		,'' As sMaCapBac
		,'' As sSoQuyetDinh
		,'' As SNgayQuyetDinh
		,0 As FTienGiamDinh
		,0 As FTienTroCap1Lan
		,0 As FTienTCTP
		,0 As FTienTCHangThang
		,0 As FTienTCPHCNvPV
		,0 as FTienTCCDTNLD
		,0 as ISoNgayDSPHSK
		,0 as FTienDSPHSK
		,0 As FTienGiamDinhTL
		,0 As FTienTroCap1LanTL
		,0 As FTienTCTPTL
		,0 As FTienTCHangThangTL
		,0 As FTienTCPHCNvPVTL
		,0 as FTienTCCDTNLDTL
		,0 as ISoNgayDSPHSKTL
		,0 as FTienDSPHSKTL
		,2 as type
		,1 IsHangCha
		,1 IsParent
		into #tempCNVCQPDuToan

		--- Total HSQ, BS  DuToan
		SELECT 
		N'4. HSQ, BS' As STT
		,'' As sTenCanBo
		,'' As sTenPhanHo
		,'' As sMaCapBac
		,'' As sSoQuyetDinh
		,'' As SNgayQuyetDinh
		,0 As FTienGiamDinh
		,0 As FTienTroCap1Lan
		,0 As FTienTCTP
		,0 As FTienTCHangThang
		,0 As FTienTCPHCNvPV
		,0 as FTienTCCDTNLD
		,0 as ISoNgayDSPHSK
		,0 as FTienDSPHSK
		,0 As FTienGiamDinhTL
		,0 As FTienTroCap1LanTL
		,0 As FTienTCTPTL
		,0 As FTienTCHangThangTL
		,0 As FTienTCPHCNvPVTL
		,0 as FTienTCCDTNLDTL
		,0 as ISoNgayDSPHSKTL
		,0 as FTienDSPHSKTL
		,2 as type
		,1 IsHangCha
		,1 IsParent
		into #tempHSQBSDuToan

		---	Total LDHD DuToan
		SELECT 
		N'5. LĐHĐ' As STT
		,'' As sTenCanBo
		,'' As sTenPhanHo
		,'' As sMaCapBac
		,'' As sSoQuyetDinh
		,'' As SNgayQuyetDinh
		,0 As FTienGiamDinh
		,0 As FTienTroCap1Lan
		,0 As FTienTCTP
		,0 As FTienTCHangThang
		,0 As FTienTCPHCNvPV
		,0 as FTienTCCDTNLD
		,0 as ISoNgayDSPHSK
		,0 as FTienDSPHSK
		,0 As FTienGiamDinhTL
		,0 As FTienTroCap1LanTL
		,0 As FTienTCTPTL
		,0 As FTienTCHangThangTL
		,0 As FTienTCPHCNvPVTL
		,0 as FTienTCCDTNLDTL
		,0 as ISoNgayDSPHSKTL
		,0 as FTienDSPHSKTL
		,2 as type
		,1 IsHangCha
		,1 IsParent
		into #tempLDHDDuToan		

	---II. Khối hạch toán
		SELECT 
		N'II. Khối hạch toán' As STT
		,'' As sTenCanBo
		,'' As sTenPhanHo
		,'' As sMaCapBac
		,'' As sSoQuyetDinh
		,'' As SNgayQuyetDinh
		,0 As FTienGiamDinh
		,0 As FTienTroCap1Lan
		,0 As FTienTCTP
		,0 As FTienTCHangThang
		,0 As FTienTCPHCNvPV
		,0 as FTienTCCDTNLD
		,0 as ISoNgayDSPHSK
		,0 as FTienDSPHSK
		,0 As FTienGiamDinhTL
		,0 As FTienTroCap1LanTL
		,0 As FTienTCTPTL
		,0 As FTienTCHangThangTL
		,0 As FTienTCPHCNvPVTL
		,0 as FTienTCCDTNLDTL
		,0 as ISoNgayDSPHSKTL
		,0 as FTienDSPHSKTL
		,3 as type
		,1 IsHangCha
		,1 IsParent
		into #tempHachToan
		--- Total SQ HachToan
		SELECT 
		N'1. Sĩ quan' As STT
		,'' As sTenCanBo
		,'' As sTenPhanHo
		,'' As sMaCapBac
		,'' As sSoQuyetDinh
		,'' As SNgayQuyetDinh
		,0 As FTienGiamDinh
		,0 As FTienTroCap1Lan
		,0 As FTienTCTP
		,0 As FTienTCHangThang
		,0 As FTienTCPHCNvPV
		,0 as FTienTCCDTNLD
		,0 as ISoNgayDSPHSK
		,0 as FTienDSPHSK
		,0 As FTienGiamDinhTL
		,0 As FTienTroCap1LanTL
		,0 As FTienTCTPTL
		,0 As FTienTCHangThangTL
		,0 As FTienTCPHCNvPVTL
		,0 as FTienTCCDTNLDTL
		,0 as ISoNgayDSPHSKTL
		,0 as FTienDSPHSKTL
		,2 as type
		,1 IsHangCha
		,1 IsParent
		into #tempSQHachToan

		--- Total QNCN HachToan
		SELECT 
		N'2. QNCN' As STT
		,'' As sTenCanBo
		,'' As sTenPhanHo
		,'' As sMaCapBac
		,'' As sSoQuyetDinh
		,'' As SNgayQuyetDinh
		,0 As FTienGiamDinh
		,0 As FTienTroCap1Lan
		,0 As FTienTCTP
		,0 As FTienTCHangThang
		,0 As FTienTCPHCNvPV
		,0 as FTienTCCDTNLD
		,0 as ISoNgayDSPHSK
		,0 as FTienDSPHSK
		,0 As FTienGiamDinhTL
		,0 As FTienTroCap1LanTL
		,0 As FTienTCTPTL
		,0 As FTienTCHangThangTL
		,0 As FTienTCPHCNvPVTL
		,0 as FTienTCCDTNLDTL
		,0 as ISoNgayDSPHSKTL
		,0 as FTienDSPHSKTL
		,2 as type
		,1 IsHangCha
		,1 IsParent
		into #tempQNCNHachToan

		--- Total CNVCQP HachToan
		SELECT 
		N'3. CNVCQP' As STT
		,'' As sTenCanBo
		,'' As sTenPhanHo
		,'' As sMaCapBac
		,'' As sSoQuyetDinh
		,'' As SNgayQuyetDinh
		,0 As FTienGiamDinh
		,0 As FTienTroCap1Lan
		,0 As FTienTCTP
		,0 As FTienTCHangThang
		,0 As FTienTCPHCNvPV
		,0 as FTienTCCDTNLD
		,0 as ISoNgayDSPHSK
		,0 as FTienDSPHSK
		,0 As FTienGiamDinhTL
		,0 As FTienTroCap1LanTL
		,0 As FTienTCTPTL
		,0 As FTienTCHangThangTL
		,0 As FTienTCPHCNvPVTL
		,0 as FTienTCCDTNLDTL
		,0 as ISoNgayDSPHSKTL
		,0 as FTienDSPHSKTL
		,2 as type
		,1 IsHangCha
		,1 IsParent
		into #tempCNVCQPHachToan

		--- Total HSQBS  HachToan
		SELECT 
		N'4. HSQ, BS' As STT
		,'' As sTenCanBo
		,'' As sTenPhanHo
		,'' As sMaCapBac
		,'' As sSoQuyetDinh
		,'' As SNgayQuyetDinh
		,0 As FTienGiamDinh
		,0 As FTienTroCap1Lan
		,0 As FTienTCTP
		,0 As FTienTCHangThang
		,0 As FTienTCPHCNvPV
		,0 as FTienTCCDTNLD
		,0 as ISoNgayDSPHSK
		,0 as FTienDSPHSK
		,0 As FTienGiamDinhTL
		,0 As FTienTroCap1LanTL
		,0 As FTienTCTPTL
		,0 As FTienTCHangThangTL
		,0 As FTienTCPHCNvPVTL
		,0 as FTienTCCDTNLDTL
		,0 as ISoNgayDSPHSKTL
		,0 as FTienDSPHSKTL
		,2 as type
		,1 IsHangCha
		,1 IsParent
		into #tempHSQBSHachToan

		--- Total LĐHĐ HachToan
		SELECT 
		N'5. LĐHĐ' As STT
		,'' As sTenCanBo
		,'' As sTenPhanHo
		,'' As sMaCapBac
		,'' As sSoQuyetDinh
		,'' As SNgayQuyetDinh
		,0 As FTienGiamDinh
		,0 As FTienTroCap1Lan
		,0 As FTienTCTP
		,0 As FTienTCHangThang
		,0 As FTienTCPHCNvPV
		,0 as FTienTCCDTNLD
		,0 as ISoNgayDSPHSK
		,0 as FTienDSPHSK
		,0 As FTienGiamDinhTL
		,0 As FTienTroCap1LanTL
		,0 As FTienTCTPTL
		,0 As FTienTCHangThangTL
		,0 As FTienTCPHCNvPVTL
		,0 as FTienTCCDTNLDTL
		,0 as ISoNgayDSPHSKTL
		,0 as FTienDSPHSKTL
		,2 as type
		,1 IsHangCha
		,1 IsParent
		into #tempLDHDHachToan	

	----- Lay ra  du toan
		-- Du Toan SQ
			SELECT
				 CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY tbltctn.sTenCanBo ASC))) AS STT
				,tbltctn.sTenCanBo
				,tbltctn.sTenPhanHo
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

				---- Chi giám định mức suy giảm KNLĐ (người)1 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) /@Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienGiamDinhTL
			
				---- Trợ cấp 1 lần (người)2 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTroCap1LanTL


				--- - Chi hỗ Trợ phòng người (người) truy linh
				-- - Chi h.trợ chuyển đổi n.nghiệp (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTCTPTL
			
				---- Trợ cấp hàng tháng (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end )FTienTCHangThangTL
			
				--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPVTL
				--- - Trợ cấp chết do TNLD. BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh  ELSE 0 END )FTienTCCDTNLDTL
				--- - DS, PHSK sau TNLĐ, BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa LIKE '9010001-010-011-0003%' THEN Sum(tbltctn.iSoNgayTruyLinh)  ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa LIKE '9010002-010-011-0003%' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ) ISoNgayDSPHSKTL

				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienDSPHSKTL
				,1 as type
				,0 IsHangCha
				,null IsParent
				into #tempDetailSQDuToan
			FROM #TBL_TroCapTaiNanDuToan tbltctn
			where tbltctn.sMaCapBac LIKE '1%'
			GROUP BY tbltctn.sTenCanBo,
				tbltctn.sTenPhanHo,
				tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.dNgayQuyetDinh
				,tbltctn.sXauNoiMa 

		-- Du Toan QNCN
			SELECT
				 CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY tbltctn.sTenCanBo ASC))) AS STT
				,tbltctn.sTenCanBo
				,tbltctn.sTenPhanHo
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

				---- Chi giám định mức suy giảm KNLĐ (người)1 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) /@Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienGiamDinhTL
			
				---- Trợ cấp 1 lần (người)2 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTroCap1LanTL

				--- - Chi hỗ Trợ phòng người (người) truy linh
				-- - Chi h.trợ chuyển đổi n.nghiệp (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTCTPTL
			
				---- Trợ cấp hàng tháng (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end )FTienTCHangThangTL
			
				--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPVTL
				--- - Trợ cấp chết do TNLD. BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh  ELSE 0 END )FTienTCCDTNLDTL
				--- - DS, PHSK sau TNLĐ, BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa LIKE '9010001-010-011-0003%' THEN Sum(tbltctn.iSoNgayTruyLinh)  ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa LIKE '9010002-010-011-0003%' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ) ISoNgayDSPHSKTL

				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienDSPHSKTL
				,1 as type
				,0 IsHangCha
				,null IsParent
				into  #tempDetailQNCNDuToan
			FROM #TBL_TroCapTaiNanDuToan tbltctn
			where tbltctn.sMaCapBac LIKE '2%'
			GROUP BY tbltctn.sTenCanBo,
				tbltctn.sTenPhanHo,
				tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.dNgayQuyetDinh
				,tbltctn.sXauNoiMa 

		-- Du Toan CNVCQP
			SELECT
				 CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY tbltctn.sTenCanBo ASC))) AS STT
				,tbltctn.sTenCanBo
				,tbltctn.sTenPhanHo
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

				---- Chi giám định mức suy giảm KNLĐ (người)1 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) /@Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienGiamDinhTL
			
				---- Trợ cấp 1 lần (người)2 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTroCap1LanTL


				--- - Chi hỗ Trợ phòng người (người) truy linh
				-- - Chi h.trợ chuyển đổi n.nghiệp (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTCTPTL
			
				---- Trợ cấp hàng tháng (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end )FTienTCHangThangTL
			
				--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPVTL
				--- - Trợ cấp chết do TNLD. BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh  ELSE 0 END )FTienTCCDTNLDTL
				--- - DS, PHSK sau TNLĐ, BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa LIKE '9010001-010-011-0003%' THEN Sum(tbltctn.iSoNgayTruyLinh)  ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa LIKE '9010002-010-011-0003%' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ) ISoNgayDSPHSKTL

				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienDSPHSKTL
				,1 as type
				,0 IsHangCha
				,null IsParent
				into #tempDetailCNVCQPDuToan
			FROM #TBL_TroCapTaiNanDuToan tbltctn
			where tbltctn.sMaCapBac LIKE '3.1%' OR tbltctn.sMaCapBac LIKE '3.2%' OR tbltctn.sMaCapBac LIKE '3.3%'  OR tbltctn.sMaCapBac = '413' OR tbltctn.sMaCapBac = '415'
			GROUP BY tbltctn.sTenCanBo,
				tbltctn.sTenPhanHo,
				tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.dNgayQuyetDinh
				,tbltctn.sXauNoiMa 

		-- Du Toan HSQBS
			SELECT
				 CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY tbltctn.sTenCanBo ASC))) AS STT
				,tbltctn.sTenCanBo
				,tbltctn.sTenPhanHo
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
				---- Chi giám định mức suy giảm KNLĐ (người)1 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) /@Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienGiamDinhTL
			
				---- Trợ cấp 1 lần (người)2 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTroCap1LanTL


				--- - Chi hỗ Trợ phòng người (người) truy linh
				-- - Chi h.trợ chuyển đổi n.nghiệp (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTCTPTL
			
				---- Trợ cấp hàng tháng (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end )FTienTCHangThangTL
			
				--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPVTL
				--- - Trợ cấp chết do TNLD. BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh  ELSE 0 END )FTienTCCDTNLDTL
				--- - DS, PHSK sau TNLĐ, BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa LIKE '9010001-010-011-0003%' THEN Sum(tbltctn.iSoNgayTruyLinh)  ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa LIKE '9010002-010-011-0003%' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ) ISoNgayDSPHSKTL

				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienDSPHSKTL
				,1 as type
				,0 IsHangCha
				,null IsParent
				 into #tempDetailHSQBSDuToan
			FROM #TBL_TroCapTaiNanDuToan tbltctn
			where tbltctn.sMaCapBac LIKE '0%'
			GROUP BY tbltctn.sTenCanBo,
				tbltctn.sTenPhanHo,
				tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.dNgayQuyetDinh
				,tbltctn.sXauNoiMa 

		-- Du Toan LDHD
			SELECT
				 CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY tbltctn.sTenCanBo ASC))) AS STT
				,tbltctn.sTenCanBo
				,tbltctn.sTenPhanHo
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
				
				---- Chi giám định mức suy giảm KNLĐ (người)1 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) /@Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienGiamDinhTL
				
				---- Trợ cấp 1 lần (người)2 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTroCap1LanTL

				--- - Chi hỗ Trợ phòng người (người) truy linh
				-- - Chi h.trợ chuyển đổi n.nghiệp (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTCTPTL
			
				---- Trợ cấp hàng tháng (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end )FTienTCHangThangTL
			
				--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPVTL
				--- - Trợ cấp chết do TNLD. BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh  ELSE 0 END )FTienTCCDTNLDTL
				--- - DS, PHSK sau TNLĐ, BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa LIKE '9010001-010-011-0003%' THEN Sum(tbltctn.iSoNgayTruyLinh)  ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa LIKE '9010002-010-011-0003%' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ) ISoNgayDSPHSKTL

				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienDSPHSKTL
				,1 as type
				,0 IsHangCha
				,null IsParent
				into #tempDetailLDHDDuToan
			FROM #TBL_TroCapTaiNanDuToan tbltctn
			where tbltctn.sMaCapBac = '423' OR tbltctn.sMaCapBac = '425' OR tbltctn.sMaCapBac = '43'
			GROUP BY tbltctn.sTenCanBo,
				tbltctn.sTenPhanHo,
				tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.dNgayQuyetDinh
				,tbltctn.sXauNoiMa 

	----- Update Du Toan : SQ,QNCN,CNVCQP,HSQBS,LDHD

			update SQ
			set SQ.FTienGiamDinh=ISNULL(DetailSQ.FTienGiamDinh,0),
			SQ.FTienTroCap1Lan=ISNULL(DetailSQ.FTienTroCap1Lan,0),
			SQ.FTienTCTP=ISNULL(DetailSQ.FTienTCTP,0),
			SQ.FTienTCHangThang=ISNULL(DetailSQ.FTienTCHangThang,0),
			SQ.FTienTCPHCNvPV=ISNULL(DetailSQ.FTienTCPHCNvPV,0),
			SQ.FTienTCCDTNLD=ISNULL(DetailSQ.FTienTCCDTNLD,0),
			SQ.ISoNgayDSPHSK=ISNULL(DetailSQ.ISoNgayDSPHSK,0),
			SQ.FTienDSPHSK=ISNULL(DetailSQ.FTienDSPHSK,0),
			SQ.FTienGiamDinhTL=ISNULL(DetailSQ.FTienGiamDinhTL,0),
			SQ.FTienTroCap1LanTL=ISNULL(DetailSQ.FTienTroCap1LanTL,0),
			SQ.FTienTCTPTL=ISNULL(DetailSQ.FTienTCTPTL,0),
			SQ.FTienTCHangThangTL=ISNULL(DetailSQ.FTienTCHangThangTL,0),
			SQ.FTienTCPHCNvPVTL=ISNULL(DetailSQ.FTienTCPHCNvPVTL,0),
			SQ.FTienTCCDTNLDTL=ISNULL(DetailSQ.FTienTCCDTNLDTL,0),
			SQ.ISoNgayDSPHSKTL=ISNULL(DetailSQ.ISoNgayDSPHSKTL,0),
			SQ.FTienDSPHSKTL=ISNULL(DetailSQ.FTienDSPHSKTL,0)
			FROM #tempSQDuToan SQ,
			(SELECT 
			Sum(FTienGiamDinh) FTienGiamDinh
			, Sum(FTienTroCap1Lan) FTienTroCap1Lan
			, Sum(FTienTCTP) FTienTCTP
			, Sum(FTienTCHangThang) FTienTCHangThang
			, Sum(FTienTCPHCNvPV) FTienTCPHCNvPV
			, Sum(FTienTCCDTNLD) FTienTCCDTNLD
			, Sum(ISoNgayDSPHSK) ISoNgayDSPHSK
			, Sum(FTienDSPHSK) FTienDSPHSK
			, Sum(FTienGiamDinhTL) FTienGiamDinhTL
			, Sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			, Sum(FTienTCTPTL) FTienTCTPTL
			, Sum(FTienTCHangThangTL) FTienTCHangThangTL
			, Sum(FTienTCPHCNvPVTL) FTienTCPHCNvPVTL
			, Sum(FTienTCCDTNLDTL) FTienTCCDTNLDTL
			, Sum(ISoNgayDSPHSKTL) ISoNgayDSPHSKTL
			, Sum(FTienDSPHSKTL) FTienDSPHSKTL
			, 2 type
			FROM #tempDetailSQDuToan) DetailSQ
			where DetailSQ.type=SQ.type

			update QNCN
			set QNCN.FTienGiamDinh=ISNULL(DetailQNCN.FTienGiamDinh,0),
			QNCN.FTienTroCap1Lan=ISNULL(DetailQNCN.FTienTroCap1Lan,0),
			QNCN.FTienTCTP=ISNULL(DetailQNCN.FTienTCTP,0),
			QNCN.FTienTCHangThang=ISNULL(DetailQNCN.FTienTCHangThang,0),
			QNCN.FTienTCPHCNvPV=ISNULL(DetailQNCN.FTienTCPHCNvPV,0),
			QNCN.FTienTCCDTNLD=ISNULL(DetailQNCN.FTienTCCDTNLD,0),
			QNCN.ISoNgayDSPHSK=ISNULL(DetailQNCN.ISoNgayDSPHSK,0),
			QNCN.FTienDSPHSK=ISNULL(DetailQNCN.FTienDSPHSK,0),
			QNCN.FTienGiamDinhTL=ISNULL(DetailQNCN.FTienGiamDinhTL,0),
			QNCN.FTienTroCap1LanTL=ISNULL(DetailQNCN.FTienTroCap1LanTL,0),
			QNCN.FTienTCTPTL=ISNULL(DetailQNCN.FTienTCTPTL,0),
			QNCN.FTienTCHangThangTL=ISNULL(DetailQNCN.FTienTCHangThangTL,0),
			QNCN.FTienTCPHCNvPVTL=ISNULL(DetailQNCN.FTienTCPHCNvPVTL,0),
			QNCN.FTienTCCDTNLDTL=ISNULL(DetailQNCN.FTienTCCDTNLDTL,0),
			QNCN.ISoNgayDSPHSKTL=ISNULL(DetailQNCN.ISoNgayDSPHSKTL,0),
			QNCN.FTienDSPHSKTL=ISNULL(DetailQNCN.FTienDSPHSKTL,0)
			FROM #tempQNCNDuToan QNCN,
			(SELECT 
			Sum(FTienGiamDinh) FTienGiamDinh
			, Sum(FTienTroCap1Lan) FTienTroCap1Lan
			, Sum(FTienTCTP) FTienTCTP
			, Sum(FTienTCHangThang) FTienTCHangThang
			, Sum(FTienTCPHCNvPV) FTienTCPHCNvPV
			, Sum(FTienTCCDTNLD) FTienTCCDTNLD
			, Sum(ISoNgayDSPHSK) ISoNgayDSPHSK
			, Sum(FTienDSPHSK) FTienDSPHSK
			, Sum(FTienGiamDinhTL) FTienGiamDinhTL
			, Sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			, Sum(FTienTCTPTL) FTienTCTPTL
			, Sum(FTienTCHangThangTL) FTienTCHangThangTL
			, Sum(FTienTCPHCNvPVTL) FTienTCPHCNvPVTL
			, Sum(FTienTCCDTNLDTL) FTienTCCDTNLDTL
			, Sum(ISoNgayDSPHSKTL) ISoNgayDSPHSKTL
			, Sum(FTienDSPHSKTL) FTienDSPHSKTL
			FROM #tempDetailQNCNDuToan) DetailQNCN


			update CNVCQP
			set CNVCQP.FTienGiamDinh=ISNULL(DetailCNVCQP.FTienGiamDinh,0),
			CNVCQP.FTienTroCap1Lan=ISNULL(DetailCNVCQP.FTienTroCap1Lan,0),
			CNVCQP.FTienTCTP=ISNULL(DetailCNVCQP.FTienTCTP,0),
			CNVCQP.FTienTCHangThang=ISNULL(DetailCNVCQP.FTienTCHangThang,0),
			CNVCQP.FTienTCPHCNvPV=ISNULL(DetailCNVCQP.FTienTCPHCNvPV,0),
			CNVCQP.FTienTCCDTNLD=ISNULL(DetailCNVCQP.FTienTCCDTNLD,0),
			CNVCQP.ISoNgayDSPHSK=ISNULL(DetailCNVCQP.ISoNgayDSPHSK,0),
			CNVCQP.FTienDSPHSK=ISNULL(DetailCNVCQP.FTienDSPHSK,0),
			CNVCQP.FTienGiamDinhTL=ISNULL(DetailCNVCQP.FTienGiamDinhTL,0),
			CNVCQP.FTienTroCap1LanTL=ISNULL(DetailCNVCQP.FTienTroCap1LanTL,0),
			CNVCQP.FTienTCTPTL=ISNULL(DetailCNVCQP.FTienTCTPTL,0),
			CNVCQP.FTienTCHangThangTL=ISNULL(DetailCNVCQP.FTienTCHangThangTL,0),
			CNVCQP.FTienTCPHCNvPVTL=ISNULL(DetailCNVCQP.FTienTCPHCNvPVTL,0),
			CNVCQP.FTienTCCDTNLDTL=ISNULL(DetailCNVCQP.FTienTCCDTNLDTL,0),
			CNVCQP.ISoNgayDSPHSKTL=ISNULL(DetailCNVCQP.ISoNgayDSPHSKTL,0),
			CNVCQP.FTienDSPHSKTL=ISNULL(DetailCNVCQP.FTienDSPHSKTL,0)
			FROM #tempCNVCQPDuToan CNVCQP,
			(SELECT 
			Sum(FTienGiamDinh) FTienGiamDinh
			, Sum(FTienTroCap1Lan) FTienTroCap1Lan
			, Sum(FTienTCTP) FTienTCTP
			, Sum(FTienTCHangThang) FTienTCHangThang
			, Sum(FTienTCPHCNvPV) FTienTCPHCNvPV
			, Sum(FTienTCCDTNLD) FTienTCCDTNLD
			, Sum(ISoNgayDSPHSK) ISoNgayDSPHSK
			, Sum(FTienDSPHSK) FTienDSPHSK
			, Sum(FTienGiamDinhTL) FTienGiamDinhTL
			, Sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			, Sum(FTienTCTPTL) FTienTCTPTL
			, Sum(FTienTCHangThangTL) FTienTCHangThangTL
			, Sum(FTienTCPHCNvPVTL) FTienTCPHCNvPVTL
			, Sum(FTienTCCDTNLDTL) FTienTCCDTNLDTL
			, Sum(ISoNgayDSPHSKTL) ISoNgayDSPHSKTL
			, Sum(FTienDSPHSKTL) FTienDSPHSKTL
			FROM #tempDetailCNVCQPDuToan) DetailCNVCQP

			update #tempHSQBSDuToan
			set FTienGiamDinh=ISNULL(DetailHSQBS.FTienGiamDinh,0),
			FTienTroCap1Lan=ISNULL(DetailHSQBS.FTienTroCap1Lan,0),
			FTienTCTP=ISNULL(DetailHSQBS.FTienTCTP,0),
			FTienTCHangThang=ISNULL(DetailHSQBS.FTienTCHangThang,0),
			FTienTCPHCNvPV=ISNULL(DetailHSQBS.FTienTCPHCNvPV,0),
			FTienTCCDTNLD=ISNULL(DetailHSQBS.FTienTCCDTNLD,0),
			ISoNgayDSPHSK=ISNULL(DetailHSQBS.ISoNgayDSPHSK,0),
			FTienDSPHSK=ISNULL(DetailHSQBS.FTienDSPHSK,0),
			FTienGiamDinhTL=ISNULL(DetailHSQBS.FTienGiamDinhTL,0),
			FTienTroCap1LanTL=ISNULL(DetailHSQBS.FTienTroCap1LanTL,0),
			FTienTCTPTL=ISNULL(DetailHSQBS.FTienTCTPTL,0),
			FTienTCHangThangTL=ISNULL(DetailHSQBS.FTienTCHangThangTL,0),
			FTienTCPHCNvPVTL=ISNULL(DetailHSQBS.FTienTCPHCNvPVTL,0),
			FTienTCCDTNLDTL=ISNULL(DetailHSQBS.FTienTCCDTNLDTL,0),
			ISoNgayDSPHSKTL=ISNULL(DetailHSQBS.ISoNgayDSPHSKTL,0),
			FTienDSPHSKTL=ISNULL(DetailHSQBS.FTienDSPHSKTL,0)
			FROM #tempHSQBSDuToan HSQBS,
			(SELECT 
			Sum(FTienGiamDinh) FTienGiamDinh
			, Sum(FTienTroCap1Lan) FTienTroCap1Lan
			, Sum(FTienTCTP) FTienTCTP
			, Sum(FTienTCHangThang) FTienTCHangThang
			, Sum(FTienTCPHCNvPV) FTienTCPHCNvPV
			, Sum(FTienTCCDTNLD) FTienTCCDTNLD
			, Sum(ISoNgayDSPHSK) ISoNgayDSPHSK
			, Sum(FTienDSPHSK) FTienDSPHSK
			, Sum(FTienGiamDinhTL) FTienGiamDinhTL
			, Sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			, Sum(FTienTCTPTL) FTienTCTPTL
			, Sum(FTienTCHangThangTL) FTienTCHangThangTL
			, Sum(FTienTCPHCNvPVTL) FTienTCPHCNvPVTL
			, Sum(FTienTCCDTNLDTL) FTienTCCDTNLDTL
			, Sum(ISoNgayDSPHSKTL) ISoNgayDSPHSKTL
			, Sum(FTienDSPHSKTL) FTienDSPHSKTL
			FROM #tempDetailHSQBSDuToan) DetailHSQBS

			update #tempLDHDDuToan
			set FTienGiamDinh=ISNULL(DetailLDHD.FTienGiamDinh,0),
			FTienTroCap1Lan=ISNULL(DetailLDHD.FTienTroCap1Lan,0),
			FTienTCTP=ISNULL(DetailLDHD.FTienTCTP,0),
			FTienTCHangThang=ISNULL(DetailLDHD.FTienTCHangThang,0),
			FTienTCPHCNvPV=ISNULL(DetailLDHD.FTienTCPHCNvPV,0),
			FTienTCCDTNLD=ISNULL(DetailLDHD.FTienTCCDTNLD,0),
			ISoNgayDSPHSK=ISNULL(DetailLDHD.ISoNgayDSPHSK,0),
			FTienDSPHSK=ISNULL(DetailLDHD.FTienDSPHSK,0),
			FTienGiamDinhTL=ISNULL(DetailLDHD.FTienGiamDinhTL,0),
			FTienTroCap1LanTL=ISNULL(DetailLDHD.FTienTroCap1LanTL,0),
			FTienTCTPTL=ISNULL(DetailLDHD.FTienTCTPTL,0),
			FTienTCHangThangTL=ISNULL(DetailLDHD.FTienTCHangThangTL,0),
			FTienTCPHCNvPVTL=ISNULL(DetailLDHD.FTienTCPHCNvPVTL,0),
			FTienTCCDTNLDTL=ISNULL(DetailLDHD.FTienTCCDTNLDTL,0),
			ISoNgayDSPHSKTL=ISNULL(DetailLDHD.ISoNgayDSPHSKTL,0),
			FTienDSPHSKTL=ISNULL(DetailLDHD.FTienDSPHSKTL,0)
			FROM #tempLDHDDuToan LDHD,
			(SELECT 
			Sum(FTienGiamDinh) FTienGiamDinh
			, Sum(FTienTroCap1Lan) FTienTroCap1Lan
			, Sum(FTienTCTP) FTienTCTP
			, Sum(FTienTCHangThang) FTienTCHangThang
			, Sum(FTienTCPHCNvPV) FTienTCPHCNvPV
			, Sum(FTienTCCDTNLD) FTienTCCDTNLD
			, Sum(ISoNgayDSPHSK) ISoNgayDSPHSK
			, Sum(FTienDSPHSK) FTienDSPHSK
			, Sum(FTienGiamDinhTL) FTienGiamDinhTL
			, Sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			, Sum(FTienTCTPTL) FTienTCTPTL
			, Sum(FTienTCHangThangTL) FTienTCHangThangTL
			, Sum(FTienTCPHCNvPVTL) FTienTCPHCNvPVTL
			, Sum(FTienTCCDTNLDTL) FTienTCCDTNLDTL
			, Sum(ISoNgayDSPHSKTL) ISoNgayDSPHSKTL
			, Sum(FTienDSPHSKTL) FTienDSPHSKTL
			FROM #tempDetailLDHDDuToan) DetailLDHD

	----- Lay ra hach toan
		-- Hạch Toan SQ
			SELECT
				 CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY tbltctn.sTenCanBo ASC))) AS STT
				,tbltctn.sTenCanBo
				,tbltctn.sTenPhanHo
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

				---- Chi giám định mức suy giảm KNLĐ (người)1 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) /@Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienGiamDinhTL
				
				---- Trợ cấp 1 lần (người)2 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTroCap1LanTL

				--- - Chi hỗ Trợ phòng người (người) truy linh
				-- - Chi h.trợ chuyển đổi n.nghiệp (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTCTPTL
			
				---- Trợ cấp hàng tháng (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end )FTienTCHangThangTL
			
				--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPVTL
				--- - Trợ cấp chết do TNLD. BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh  ELSE 0 END )FTienTCCDTNLDTL
				--- - DS, PHSK sau TNLĐ, BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa LIKE '9010001-010-011-0003%' THEN Sum(tbltctn.iSoNgayTruyLinh)  ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa LIKE '9010002-010-011-0003%' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ) ISoNgayDSPHSKTL

				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienDSPHSKTL
				,1 as type
				,0 IsHangCha
				,null IsParent
				into #tempDetailSQHachToan
			FROM #TBL_TroCapTaiNanHachToan tbltctn
			where tbltctn.sMaCapBac LIKE '1%'
			GROUP BY tbltctn.sTenCanBo,
				tbltctn.sTenPhanHo,
				tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.dNgayQuyetDinh
				,tbltctn.sXauNoiMa 

		-- Hạch Toan QNCN
			SELECT
				 CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY tbltctn.sTenCanBo ASC))) AS STT
				,tbltctn.sTenCanBo
				,tbltctn.sTenPhanHo
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

				---- Chi giám định mức suy giảm KNLĐ (người)1 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) /@Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienGiamDinhTL
				
				---- Trợ cấp 1 lần (người)2 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTroCap1LanTL

				--- - Chi hỗ Trợ phòng người (người) truy linh
				-- - Chi h.trợ chuyển đổi n.nghiệp (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTCTPTL
			
				---- Trợ cấp hàng tháng (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end )FTienTCHangThangTL
			
				--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPVTL
				--- - Trợ cấp chết do TNLD. BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh  ELSE 0 END )FTienTCCDTNLDTL
				--- - DS, PHSK sau TNLĐ, BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa LIKE '9010001-010-011-0003%' THEN Sum(tbltctn.iSoNgayTruyLinh)  ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa LIKE '9010002-010-011-0003%' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ) ISoNgayDSPHSKTL

				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienDSPHSKTL
				,1 as type
				,0 IsHangCha
				,null IsParent
				into #tempDetailQNCNHachToan
			FROM #TBL_TroCapTaiNanHachToan tbltctn
			where tbltctn.sMaCapBac LIKE '2%'
			GROUP BY tbltctn.sTenCanBo,
				tbltctn.sTenPhanHo,
				tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.dNgayQuyetDinh
				,tbltctn.sXauNoiMa 


		-- Hạch Toan CNVCQP
			SELECT
				 CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY tbltctn.sTenCanBo ASC))) AS STT
				,tbltctn.sTenCanBo
				,tbltctn.sTenPhanHo
				, tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, CONVERT(varchar, (tbltctn.dNgayQuyetDinh), 101) As SNgayQuyetDinh

				---- Chi giám định mức suy giảm KNLĐ (người)1
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
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

				---- Chi giám định mức suy giảm KNLĐ (người)1 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) /@Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienGiamDinhTL
				
				---- Trợ cấp 1 lần (người)2 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTroCap1LanTL

				--- - Chi hỗ Trợ phòng người (người) truy linh
				-- - Chi h.trợ chuyển đổi n.nghiệp (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTCTPTL
			
				---- Trợ cấp hàng tháng (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end )FTienTCHangThangTL
			
				--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPVTL
				--- - Trợ cấp chết do TNLD. BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh  ELSE 0 END )FTienTCCDTNLDTL
				--- - DS, PHSK sau TNLĐ, BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa LIKE '9010001-010-011-0003%' THEN Sum(tbltctn.iSoNgayTruyLinh)  ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa LIKE '9010002-010-011-0003%' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ) ISoNgayDSPHSKTL

				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienDSPHSKTL
				,1 as type
				,0 IsHangCha
				,null IsParent
				into #tempDetailCNVCQPHachToan
			FROM #TBL_TroCapTaiNanHachToan tbltctn
			where tbltctn.sMaCapBac LIKE '3.1%' OR tbltctn.sMaCapBac LIKE '3.2%' OR tbltctn.sMaCapBac LIKE '3.3%'  OR tbltctn.sMaCapBac = '413' OR tbltctn.sMaCapBac = '415'
			GROUP BY tbltctn.sTenCanBo,
				tbltctn.sTenPhanHo,
				tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.dNgayQuyetDinh
				,tbltctn.sXauNoiMa 

		-- Hạch Toan HSQBS
			SELECT
				 CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY tbltctn.sTenCanBo ASC))) AS STT
				,tbltctn.sTenCanBo
				,tbltctn.sTenPhanHo
				, tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, CONVERT(varchar, (tbltctn.dNgayQuyetDinh), 101) As SNgayQuyetDinh

				---- Chi giám định mức suy giảm KNLĐ (người)1
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
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

				---- Chi giám định mức suy giảm KNLĐ (người)1 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) /@Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienGiamDinhTL
				
				---- Trợ cấp 1 lần (người)2 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTroCap1LanTL

				--- - Chi hỗ Trợ phòng người (người) truy linh
				-- - Chi h.trợ chuyển đổi n.nghiệp (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTCTPTL
			
				---- Trợ cấp hàng tháng (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end )FTienTCHangThangTL
			
				--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPVTL
				--- - Trợ cấp chết do TNLD. BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh  ELSE 0 END )FTienTCCDTNLDTL
				--- - DS, PHSK sau TNLĐ, BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa LIKE '9010001-010-011-0003%' THEN Sum(tbltctn.iSoNgayTruyLinh)  ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa LIKE '9010002-010-011-0003%' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ) ISoNgayDSPHSKTL

				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienDSPHSKTL
				,1 as type
				,0 IsHangCha
				,null IsParent
				into #tempDetailHSQBSHachToan
			FROM #TBL_TroCapTaiNanHachToan tbltctn
			where tbltctn.sMaCapBac LIKE '0%'
			GROUP BY tbltctn.sTenCanBo,
				tbltctn.sTenPhanHo,
				tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.dNgayQuyetDinh
				,tbltctn.sXauNoiMa 

		-- Hạch Toan LDHD
			SELECT
				 CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY tbltctn.sTenCanBo ASC))) AS STT
				,tbltctn.sTenCanBo
				,tbltctn.sTenPhanHo
				, tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, CONVERT(varchar, (tbltctn.dNgayQuyetDinh), 101) As SNgayQuyetDinh

				---- Chi giám định mức suy giảm KNLĐ (người)1
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
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

				---- Chi giám định mức suy giảm KNLĐ (người)1 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) /@Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienGiamDinhTL
				
				---- Trợ cấp 1 lần (người)2 truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTroCap1LanTL

				--- - Chi hỗ Trợ phòng người (người) truy linh
				-- - Chi h.trợ chuyển đổi n.nghiệp (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 END) FTienTCTPTL
			
				---- Trợ cấp hàng tháng (người) truy linh
				, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fTienTruyLinh)/ @Donvitinh ELSE 0 end )FTienTCHangThangTL
			
				--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPVTL
				--- - Trợ cấp chết do TNLD. BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh  ELSE 0 END )FTienTCCDTNLDTL
				--- - DS, PHSK sau TNLĐ, BNN (người) truy linh
				,( CASE WHEN tbltctn.sXauNoiMa LIKE '9010001-010-011-0003%' THEN Sum(tbltctn.iSoNgayTruyLinh)  ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa LIKE '9010002-010-011-0003%' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ) ISoNgayDSPHSKTL

				,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END 
				+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fTienTruyLinh) / @Donvitinh ELSE 0 END) FTienDSPHSKTL
				,1 as type
				,0 IsHangCha
				,null IsParent
				into #tempDetailLDHDHachToan
			FROM #TBL_TroCapTaiNanHachToan tbltctn
			where tbltctn.sMaCapBac = '423' OR tbltctn.sMaCapBac = '425' OR tbltctn.sMaCapBac = '43'
			GROUP BY tbltctn.sTenCanBo,
				tbltctn.sTenPhanHo,
				tbltctn.sMaCapBac
				, tbltctn.sSoQuyetDinh
				, tbltctn.dNgayQuyetDinh
				,tbltctn.sXauNoiMa 

	----- Update Hach Toan : SQ,QNCN,CNVCQP,HSQBS,LDHD
			update SQHachToan
			set FTienGiamDinh=ISNULL(DetailSQHachToan.FTienGiamDinh,0),
			FTienTroCap1Lan=ISNULL(DetailSQHachToan.FTienTroCap1Lan,0),
			FTienTCTP=ISNULL(DetailSQHachToan.FTienTCTP,0),
			FTienTCHangThang=ISNULL(DetailSQHachToan.FTienTCHangThang,0),
			FTienTCPHCNvPV=ISNULL(DetailSQHachToan.FTienTCPHCNvPV,0),
			FTienTCCDTNLD=ISNULL(DetailSQHachToan.FTienTCCDTNLD,0),
			ISoNgayDSPHSK=ISNULL(DetailSQHachToan.ISoNgayDSPHSK,0),
			FTienDSPHSK=ISNULL(DetailSQHachToan.FTienDSPHSK,0),
			FTienGiamDinhTL=ISNULL(DetailSQHachToan.FTienGiamDinhTL,0),
			FTienTroCap1LanTL=ISNULL(DetailSQHachToan.FTienTroCap1LanTL,0),
			FTienTCTPTL=ISNULL(DetailSQHachToan.FTienTCTPTL,0),
			FTienTCHangThangTL=ISNULL(DetailSQHachToan.FTienTCHangThangTL,0),
			FTienTCPHCNvPVTL=ISNULL(DetailSQHachToan.FTienTCPHCNvPVTL,0),
			FTienTCCDTNLDTL=ISNULL(DetailSQHachToan.FTienTCCDTNLDTL,0),
			ISoNgayDSPHSKTL=ISNULL(DetailSQHachToan.ISoNgayDSPHSKTL,0),
			FTienDSPHSKTL=ISNULL(DetailSQHachToan.FTienDSPHSKTL,0)
			FROM #tempSQHachToan SQHachToan,
			(SELECT 
			Sum(FTienGiamDinh) FTienGiamDinh
			, Sum(FTienTroCap1Lan) FTienTroCap1Lan
			, Sum(FTienTCTP) FTienTCTP
			, Sum(FTienTCHangThang) FTienTCHangThang
			, Sum(FTienTCPHCNvPV) FTienTCPHCNvPV
			, Sum(FTienTCCDTNLD) FTienTCCDTNLD
			, Sum(ISoNgayDSPHSK) ISoNgayDSPHSK
			, Sum(FTienDSPHSK) FTienDSPHSK
			, Sum(FTienGiamDinhTL) FTienGiamDinhTL
			, Sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			, Sum(FTienTCTPTL) FTienTCTPTL
			, Sum(FTienTCHangThangTL) FTienTCHangThangTL
			, Sum(FTienTCPHCNvPVTL) FTienTCPHCNvPVTL
			, Sum(FTienTCCDTNLDTL) FTienTCCDTNLDTL
			, Sum(ISoNgayDSPHSKTL) ISoNgayDSPHSKTL
			, Sum(FTienDSPHSKTL) FTienDSPHSKTL
			FROM #tempDetailSQHachToan) DetailSQHachToan

			update QNCNHachToan
			set QNCNHachToan.FTienGiamDinh=ISNULL(DetailQNCNHachToan.FTienGiamDinh,0),
			QNCNHachToan.FTienTroCap1Lan=ISNULL(DetailQNCNHachToan.FTienTroCap1Lan,0),
			QNCNHachToan.FTienTCTP=ISNULL(DetailQNCNHachToan.FTienTCTP,0),
			QNCNHachToan.FTienTCHangThang=ISNULL(DetailQNCNHachToan.FTienTCHangThang,0),
			QNCNHachToan.FTienTCPHCNvPV=ISNULL(DetailQNCNHachToan.FTienTCPHCNvPV,0),
			QNCNHachToan.FTienTCCDTNLD=ISNULL(DetailQNCNHachToan.FTienTCCDTNLD,0),
			QNCNHachToan.ISoNgayDSPHSK=ISNULL(DetailQNCNHachToan.ISoNgayDSPHSK,0),
			QNCNHachToan.FTienDSPHSK=ISNULL(DetailQNCNHachToan.FTienDSPHSK,0),
			QNCNHachToan.FTienGiamDinhTL=ISNULL(DetailQNCNHachToan.FTienGiamDinhTL,0),
			QNCNHachToan.FTienTroCap1LanTL=ISNULL(DetailQNCNHachToan.FTienTroCap1LanTL,0),
			QNCNHachToan.FTienTCTPTL=ISNULL(DetailQNCNHachToan.FTienTCTPTL,0),
			QNCNHachToan.FTienTCHangThangTL=ISNULL(DetailQNCNHachToan.FTienTCHangThangTL,0),
			QNCNHachToan.FTienTCPHCNvPVTL=ISNULL(DetailQNCNHachToan.FTienTCPHCNvPVTL,0),
			QNCNHachToan.FTienTCCDTNLDTL=ISNULL(DetailQNCNHachToan.FTienTCCDTNLDTL,0),
			QNCNHachToan.ISoNgayDSPHSKTL=ISNULL(DetailQNCNHachToan.ISoNgayDSPHSKTL,0),
			QNCNHachToan.FTienDSPHSKTL=ISNULL(DetailQNCNHachToan.FTienDSPHSKTL,0)
			FROM #tempQNCNHachToan QNCNHachToan, 
			(SELECT 
			Sum(FTienGiamDinh) FTienGiamDinh
			, Sum(FTienTroCap1Lan) FTienTroCap1Lan
			, Sum(FTienTCTP) FTienTCTP
			, Sum(FTienTCHangThang) FTienTCHangThang
			, Sum(FTienTCPHCNvPV) FTienTCPHCNvPV
			, Sum(FTienTCCDTNLD) FTienTCCDTNLD
			, Sum(ISoNgayDSPHSK) ISoNgayDSPHSK
			, Sum(FTienDSPHSK) FTienDSPHSK
			, Sum(FTienGiamDinhTL) FTienGiamDinhTL
			, Sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			, Sum(FTienTCTPTL) FTienTCTPTL
			, Sum(FTienTCHangThangTL) FTienTCHangThangTL
			, Sum(FTienTCPHCNvPVTL) FTienTCPHCNvPVTL
			, Sum(FTienTCCDTNLDTL) FTienTCCDTNLDTL
			, Sum(ISoNgayDSPHSKTL) ISoNgayDSPHSKTL
			, Sum(FTienDSPHSKTL) FTienDSPHSKTL
			FROM #tempDetailQNCNHachToan) DetailQNCNHachToan

			update CNVCQPHachToan
			set CNVCQPHachToan.FTienGiamDinh=ISNULL(DetailCNVCQPHachToan.FTienGiamDinh,0),
			CNVCQPHachToan.FTienTroCap1Lan=ISNULL(DetailCNVCQPHachToan.FTienTroCap1Lan,0),
			CNVCQPHachToan.FTienTCTP=ISNULL(DetailCNVCQPHachToan.FTienTCTP,0),
			CNVCQPHachToan.FTienTCHangThang=ISNULL(DetailCNVCQPHachToan.FTienTCHangThang,0),
			CNVCQPHachToan.FTienTCPHCNvPV=ISNULL(DetailCNVCQPHachToan.FTienTCPHCNvPV,0),
			CNVCQPHachToan.FTienTCCDTNLD=ISNULL(DetailCNVCQPHachToan.FTienTCCDTNLD,0),
			CNVCQPHachToan.ISoNgayDSPHSK=ISNULL(DetailCNVCQPHachToan.ISoNgayDSPHSK,0),
			CNVCQPHachToan.FTienDSPHSK=ISNULL(DetailCNVCQPHachToan.FTienDSPHSK,0),
			CNVCQPHachToan.FTienGiamDinhTL=ISNULL(DetailCNVCQPHachToan.FTienGiamDinhTL,0),
			CNVCQPHachToan.FTienTroCap1LanTL=ISNULL(DetailCNVCQPHachToan.FTienTroCap1LanTL,0),
			CNVCQPHachToan.FTienTCTPTL=ISNULL(DetailCNVCQPHachToan.FTienTCTPTL,0),
			CNVCQPHachToan.FTienTCHangThangTL=ISNULL(DetailCNVCQPHachToan.FTienTCHangThangTL,0),
			CNVCQPHachToan.FTienTCPHCNvPVTL=ISNULL(DetailCNVCQPHachToan.FTienTCPHCNvPVTL,0),
			CNVCQPHachToan.FTienTCCDTNLDTL=ISNULL(DetailCNVCQPHachToan.FTienTCCDTNLDTL,0),
			CNVCQPHachToan.ISoNgayDSPHSKTL=ISNULL(DetailCNVCQPHachToan.ISoNgayDSPHSKTL,0),
			CNVCQPHachToan.FTienDSPHSKTL=ISNULL(DetailCNVCQPHachToan.FTienDSPHSKTL,0)
			FROM #tempCNVCQPHachToan CNVCQPHachToan,
			(SELECT 
			Sum(FTienGiamDinh) FTienGiamDinh
			, Sum(FTienTroCap1Lan) FTienTroCap1Lan
			, Sum(FTienTCTP) FTienTCTP
			, Sum(FTienTCHangThang) FTienTCHangThang
			, Sum(FTienTCPHCNvPV) FTienTCPHCNvPV
			, Sum(FTienTCCDTNLD) FTienTCCDTNLD
			, Sum(ISoNgayDSPHSK) ISoNgayDSPHSK
			, Sum(FTienDSPHSK) FTienDSPHSK
			, Sum(FTienGiamDinhTL) FTienGiamDinhTL
			, Sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			, Sum(FTienTCTPTL) FTienTCTPTL
			, Sum(FTienTCHangThangTL) FTienTCHangThangTL
			, Sum(FTienTCPHCNvPVTL) FTienTCPHCNvPVTL
			, Sum(FTienTCCDTNLDTL) FTienTCCDTNLDTL
			, Sum(ISoNgayDSPHSKTL) ISoNgayDSPHSKTL
			, Sum(FTienDSPHSKTL) FTienDSPHSKTL
			FROM #tempDetailCNVCQPHachToan)DetailCNVCQPHachToan

			update HSQBSHachToan
			set HSQBSHachToan.FTienGiamDinh=ISNULL(DetailHSQBSHachToan.FTienGiamDinh,0),
			HSQBSHachToan.FTienTroCap1Lan=ISNULL(DetailHSQBSHachToan.FTienTroCap1Lan,0),
			HSQBSHachToan.FTienTCTP=ISNULL(DetailHSQBSHachToan.FTienTCTP,0),
			HSQBSHachToan.FTienTCHangThang=ISNULL(DetailHSQBSHachToan.FTienTCHangThang,0),
			HSQBSHachToan.FTienTCPHCNvPV=ISNULL(DetailHSQBSHachToan.FTienTCPHCNvPV,0),
			HSQBSHachToan.FTienTCCDTNLD=ISNULL(DetailHSQBSHachToan.FTienTCCDTNLD,0),
			HSQBSHachToan.ISoNgayDSPHSK=ISNULL(DetailHSQBSHachToan.ISoNgayDSPHSK,0),
			HSQBSHachToan.FTienDSPHSK=ISNULL(DetailHSQBSHachToan.FTienDSPHSK,0),
			HSQBSHachToan.FTienGiamDinhTL=ISNULL(DetailHSQBSHachToan.FTienGiamDinhTL,0),
			HSQBSHachToan.FTienTroCap1LanTL=ISNULL(DetailHSQBSHachToan.FTienTroCap1LanTL,0),
			HSQBSHachToan.FTienTCTPTL=ISNULL(DetailHSQBSHachToan.FTienTCTPTL,0),
			HSQBSHachToan.FTienTCHangThangTL=ISNULL(DetailHSQBSHachToan.FTienTCHangThangTL,0),
			HSQBSHachToan.FTienTCPHCNvPVTL=ISNULL(DetailHSQBSHachToan.FTienTCPHCNvPVTL,0),
			HSQBSHachToan.FTienTCCDTNLDTL=ISNULL(DetailHSQBSHachToan.FTienTCCDTNLDTL,0),
			HSQBSHachToan.ISoNgayDSPHSKTL=ISNULL(DetailHSQBSHachToan.ISoNgayDSPHSKTL,0),
			HSQBSHachToan.FTienDSPHSKTL=ISNULL(DetailHSQBSHachToan.FTienDSPHSKTL,0)
			FROM #tempHSQBSHachToan HSQBSHachToan,
			(SELECT 
			Sum(FTienGiamDinh) FTienGiamDinh
			, Sum(FTienTroCap1Lan) FTienTroCap1Lan
			, Sum(FTienTCTP) FTienTCTP
			, Sum(FTienTCHangThang) FTienTCHangThang
			, Sum(FTienTCPHCNvPV) FTienTCPHCNvPV
			, Sum(FTienTCCDTNLD) FTienTCCDTNLD
			, Sum(ISoNgayDSPHSK) ISoNgayDSPHSK
			, Sum(FTienDSPHSK) FTienDSPHSK
			, Sum(FTienGiamDinhTL) FTienGiamDinhTL
			, Sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			, Sum(FTienTCTPTL) FTienTCTPTL
			, Sum(FTienTCHangThangTL) FTienTCHangThangTL
			, Sum(FTienTCPHCNvPVTL) FTienTCPHCNvPVTL
			, Sum(FTienTCCDTNLDTL) FTienTCCDTNLDTL
			, Sum(ISoNgayDSPHSKTL) ISoNgayDSPHSKTL
			, Sum(FTienDSPHSKTL) FTienDSPHSKTL
			FROM #tempDetailHSQBSHachToan) DetailHSQBSHachToan

			update LDHDHachToan
			set LDHDHachToan.FTienGiamDinh=ISNULL(DetailLDHDHachToan.FTienGiamDinh,0),
			LDHDHachToan.FTienTroCap1Lan=ISNULL(DetailLDHDHachToan.FTienTroCap1Lan,0),
			LDHDHachToan.FTienTCTP=ISNULL(DetailLDHDHachToan.FTienTCTP,0),
			LDHDHachToan.FTienTCHangThang=ISNULL(DetailLDHDHachToan.FTienTCHangThang,0),
			LDHDHachToan.FTienTCPHCNvPV=ISNULL(DetailLDHDHachToan.FTienTCPHCNvPV,0),
			LDHDHachToan.FTienTCCDTNLD=ISNULL(DetailLDHDHachToan.FTienTCCDTNLD,0),
			LDHDHachToan.ISoNgayDSPHSK=ISNULL(DetailLDHDHachToan.ISoNgayDSPHSK,0),
			LDHDHachToan.FTienDSPHSK=ISNULL(DetailLDHDHachToan.FTienDSPHSK,0),
			LDHDHachToan.FTienGiamDinhTL=ISNULL(DetailLDHDHachToan.FTienGiamDinhTL,0),
			LDHDHachToan.FTienTroCap1LanTL=ISNULL(DetailLDHDHachToan.FTienTroCap1LanTL,0),
			LDHDHachToan.FTienTCTPTL=ISNULL(DetailLDHDHachToan.FTienTCTPTL,0),
			LDHDHachToan.FTienTCHangThangTL=ISNULL(DetailLDHDHachToan.FTienTCHangThangTL,0),
			LDHDHachToan.FTienTCPHCNvPVTL=ISNULL(DetailLDHDHachToan.FTienTCPHCNvPVTL,0),
			LDHDHachToan.FTienTCCDTNLDTL=ISNULL(DetailLDHDHachToan.FTienTCCDTNLDTL,0),
			LDHDHachToan.ISoNgayDSPHSKTL=ISNULL(DetailLDHDHachToan.ISoNgayDSPHSKTL,0),
			LDHDHachToan.FTienDSPHSKTL=ISNULL(DetailLDHDHachToan.FTienDSPHSKTL,0)
			FROM #tempLDHDHachToan LDHDHachToan,
			(SELECT 
			Sum(FTienGiamDinh) FTienGiamDinh
			, Sum(FTienTroCap1Lan) FTienTroCap1Lan
			, Sum(FTienTCTP) FTienTCTP
			, Sum(FTienTCHangThang) FTienTCHangThang
			, Sum(FTienTCPHCNvPV) FTienTCPHCNvPV
			, Sum(FTienTCCDTNLD) FTienTCCDTNLD
			, Sum(ISoNgayDSPHSK) ISoNgayDSPHSK
			, Sum(FTienDSPHSK) FTienDSPHSK
			, Sum(FTienGiamDinhTL) FTienGiamDinhTL
			, Sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			, Sum(FTienTCTPTL) FTienTCTPTL
			, Sum(FTienTCHangThangTL) FTienTCHangThangTL
			, Sum(FTienTCPHCNvPVTL) FTienTCPHCNvPVTL
			, Sum(FTienTCCDTNLDTL) FTienTCCDTNLDTL
			, Sum(ISoNgayDSPHSKTL) ISoNgayDSPHSKTL
			, Sum(FTienDSPHSKTL) FTienDSPHSKTL
			FROM #tempDetailLDHDHachToan)DetailLDHDHachToan

---------  Tra ve kqua DuToan
			SELECT * INTO #tempkqDuToan FROM
			(
					SELECT * FROM #tempDuToan
					UNION ALL
					SELECT * FROM  #tempSQDuToan
					UNION ALL
					SELECT * FROM  #tempDetailSQDuToan
					UNION ALL
					SELECT * FROM  #tempQNCNDuToan
					UNION ALL
					SELECT * FROM  #tempDetailQNCNDuToan
					UNION ALL
					SELECT * FROM  #tempCNVCQPDuToan
					UNION ALL
					SELECT * FROM  #tempDetailCNVCQPDuToan
					UNION ALL
					SELECT * FROM  #tempHSQBSDuToan
					UNION ALL
					SELECT * FROM  #tempDetailHSQBSDuToan
					UNION ALL
					SELECT * FROM  #tempLDHDDuToan
					UNION ALL
					SELECT * FROM  #tempDetailLDHDDuToan
			) TEMPDuToan
------ Update total khoi du toan
			SELECT 
			Sum(FTienGiamDinh) FTienGiamDinh
			, Sum(FTienTroCap1Lan) FTienTroCap1Lan
			, Sum(FTienTCTP) FTienTCTP
			, Sum(FTienTCHangThang) FTienTCHangThang
			, Sum(FTienTCPHCNvPV) FTienTCPHCNvPV
			, Sum(FTienTCCDTNLD) FTienTCCDTNLD
			, Sum(ISoNgayDSPHSK) ISoNgayDSPHSK
			, Sum(FTienDSPHSK) FTienDSPHSK
			, Sum(FTienGiamDinhTL) FTienGiamDinhTL
			, Sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			, Sum(FTienTCTPTL) FTienTCTPTL
			, Sum(FTienTCHangThangTL) FTienTCHangThangTL
			, Sum(FTienTCPHCNvPVTL) FTienTCPHCNvPVTL
			, Sum(FTienTCCDTNLDTL) FTienTCCDTNLDTL
			, Sum(ISoNgayDSPHSKTL) ISoNgayDSPHSKTL
			, Sum(FTienDSPHSKTL) FTienDSPHSKTL
			, 3 type
			INTO #tempTotalDuToan
			FROM #tempkqDuToan
			WHERE type=2

			update T1
			set T1.FTienGiamDinh=T2.FTienGiamDinh,
			T1.FTienTroCap1Lan=T2.FTienTroCap1Lan,
			T1.FTienTCTP=T2.FTienTCTP,
			T1.FTienTCHangThang=T2.FTienTCHangThang,
			T1.FTienTCPHCNvPV=T2.FTienTCPHCNvPV,
			T1.FTienTCCDTNLD=T2.FTienTCCDTNLD,
			T1.ISoNgayDSPHSK=T2.ISoNgayDSPHSK,
			T1.FTienDSPHSK=T2.FTienDSPHSK,
			T1.FTienGiamDinhTL=T2.FTienGiamDinhTL,
			T1.FTienTroCap1LanTL=T2.FTienTroCap1LanTL,
			T1.FTienTCTPTL=T2.FTienTCTPTL,
			T1.FTienTCHangThangTL=T2.FTienTCHangThangTL,
			T1.FTienTCPHCNvPVTL=T2.FTienTCPHCNvPVTL,
			T1.FTienTCCDTNLDTL=T2.FTienTCCDTNLDTL,
			T1.ISoNgayDSPHSKTL=T2.ISoNgayDSPHSKTL,
			T1.FTienDSPHSKTL=T2.FTienDSPHSKTL
			FROM #tempkqDuToan T1, #tempTotalDuToan T2
			WHERE T1.type=T2.type
			and T1.STT=N'I. Khối dự toán'

---------  Tra ve kqua HachToan
			SELECT * INTO #tempkqHachToan FROM
			(
					SELECT * FROM #tempHachToan
					UNION ALL
					SELECT * FROM  #tempSQHachToan
					UNION ALL
					SELECT * FROM  #tempDetailSQHachToan
					UNION ALL
					SELECT * FROM  #tempQNCNHachToan
					UNION ALL
					SELECT * FROM  #tempDetailQNCNHachToan
					UNION ALL
					SELECT * FROM  #tempCNVCQPHachToan
					UNION ALL
					SELECT * FROM  #tempDetailCNVCQPHachToan
					UNION ALL
					SELECT * FROM  #tempHSQBSHachToan
					UNION ALL
					SELECT * FROM  #tempDetailHSQBSHachToan
					UNION ALL
					SELECT * FROM  #tempLDHDHachToan
					UNION ALL
					SELECT * FROM  #tempDetailLDHDHachToan
			) TEMPHachToan

------ Update total khoi hach toan
			SELECT 
			Sum(FTienGiamDinh) FTienGiamDinh
			, Sum(FTienTroCap1Lan) FTienTroCap1Lan
			, Sum(FTienTCTP) FTienTCTP
			, Sum(FTienTCHangThang) FTienTCHangThang
			, Sum(FTienTCPHCNvPV) FTienTCPHCNvPV
			, Sum(FTienTCCDTNLD) FTienTCCDTNLD
			, Sum(ISoNgayDSPHSK) ISoNgayDSPHSK
			, Sum(FTienDSPHSK) FTienDSPHSK
			, Sum(FTienGiamDinhTL) FTienGiamDinhTL
			, Sum(FTienTroCap1LanTL) FTienTroCap1LanTL
			, Sum(FTienTCTPTL) FTienTCTPTL
			, Sum(FTienTCHangThangTL) FTienTCHangThangTL
			, Sum(FTienTCPHCNvPVTL) FTienTCPHCNvPVTL
			, Sum(FTienTCCDTNLDTL) FTienTCCDTNLDTL
			, Sum(ISoNgayDSPHSKTL) ISoNgayDSPHSKTL
			, Sum(FTienDSPHSKTL) FTienDSPHSKTL
			, 3 type
			INTO #tempTotalHachToan
			FROM #tempkqHachToan
			WHERE type=2

			UPDATE  T1
			set T1.FTienGiamDinh=T2.FTienGiamDinh,
			T1.FTienTroCap1Lan=T2.FTienTroCap1Lan,
			T1.FTienTCTP=T2.FTienTCTP,
			T1.FTienTCHangThang=T2.FTienTCHangThang,
			T1.FTienTCPHCNvPV=T2.FTienTCPHCNvPV,
			T1.FTienTCCDTNLD=T2.FTienTCCDTNLD,
			T1.ISoNgayDSPHSK=T2.ISoNgayDSPHSK,
			T1.FTienDSPHSK=T2.FTienDSPHSK,
			T1.FTienGiamDinhTL=T2.FTienGiamDinhTL,
			T1.FTienTroCap1LanTL=T2.FTienTroCap1LanTL,
			T1.FTienTCTPTL=T2.FTienTCTPTL,
			T1.FTienTCHangThangTL=T2.FTienTCHangThangTL,
			T1.FTienTCPHCNvPVTL=T2.FTienTCPHCNvPVTL,
			T1.FTienTCCDTNLDTL=T2.FTienTCCDTNLDTL,
			T1.ISoNgayDSPHSKTL=T2.ISoNgayDSPHSKTL,
			T1.FTienDSPHSKTL=T2.FTienDSPHSKTL
			FROM #tempkqHachToan T1, #tempTotalHachToan T2
			WHERE T1.type=T2.type
			and T1.STT=N'II. Khối hạch toán'

--------- Tra Ve KQua ALL

		SELECT * INTO #tempKQAll FROM
			(
					SELECT * FROM #tempkqDuToan
					UNION ALL
					SELECT * FROM  #tempkqHachToan
					
			) TEMPKQAll

		select * from #tempKQAll

		DROP TABLE #TBL_TroCapTaiNanDuToan
		DROP TABLE #TBL_TroCapTaiNanHachToan

		DROP TABLE #tempDuToan
		DROP TABLE #tempSQDuToan
		DROP TABLE #tempQNCNDuToan
		DROP TABLE #tempCNVCQPDuToan
		DROP TABLE #tempHSQBSDuToan
		DROP TABLE #tempLDHDDuToan
		DROP TABLE #tempDetailSQDuToan
		DROP TABLE #tempDetailQNCNDuToan
		DROP TABLE #tempDetailCNVCQPDuToan
		DROP TABLE #tempDetailHSQBSDuToan
		DROP TABLE #tempDetailLDHDDuToan
		DROP TABLE #tempTotalDuToan

		DROP TABLE #tempHachToan
		DROP TABLE #tempSQHachToan
		DROP TABLE #tempQNCNHachToan
		DROP TABLE #tempCNVCQPHachToan
		DROP TABLE #tempHSQBSHachToan
		DROP TABLE #tempLDHDHachToan
		DROP TABLE #tempDetailSQHachToan
		DROP TABLE #tempDetailQNCNHachToan
		DROP TABLE #tempDetailCNVCQPHachToan
		DROP TABLE #tempDetailHSQBSHachToan
		DROP TABLE #tempDetailLDHDHachToan
		DROP TABLE #tempTotalHachToan

		DROP TABLE #tempkqDuToan
		DROP TABLE #tempkqHachToan
		DROP TABLE #tempKQAll

end
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_quykinhphi_quanly_chungtu_chi_tiet]    Script Date: 7/ @Donvitinh7/2024 5:36:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_qtc_quykinhphi_quanly_chungtu_chi_tiet]
	@VoucherId uniqueidentifier,
	@LNS nvarchar(max),
	@YearOfWork int,
	@AgencyId nvarchar(500),
	@VoucherDate datetime,
	@iID_LoaiDanhMucChi uniqueidentifier,
	@iQuyChungTu int,
	@Loai int
AS
BEGIN
	DECLARE @iD uniqueidentifier='00000000-0000-0000-0000-000000000000';
	DECLARE @CountIndex INT;
	SELECT * into #tempLNS from f_split(@LNS);
	SELECT * into #tempAgency from  f_split(@AgencyId);
	SELECT @CountIndex = COUNT(*) FROM 
									BH_QTC_Quy_KinhPhiQuanLy_ChiTiet 
									WHERE iID_QTC_Quy_KinhPhiQuanLy =@VoucherId
SELECT 
  iID_MLNS, 
  iID_MLNS_Cha, 
  sXauNoiMa, 
  sLNS, 
  sL, 
  sK, 
  sM, 
  sTM, 
  sTTM, 
  sNG, 
  sTNG, 
  sTNG1, 
  sTNG2, 
  sTNG3, 
  sMoTa, 
  bHangCha, 
  sDuToanChiTietToi,
  bHangChaDuToan
  into #tblNsMlns
FROM 
  BH_DM_MucLucNganSach 
where 
  iNamLamViec = @YearOfWork
  and iTrangThai=1

		-- lấy ra dữ liệu dự toán
	SELECT 
		  SUM(fTienTuChi) AS fTienDuToanDuocGiao,
          0 AS fTienDeNghiQuyetToanQuyNay,
          0 AS fTienQuyetToanDaDuyet,
		  0 AS fTienThucChi,
		  0 AS fTienXacNhanQuyetToanQuyNay,
          iID_MaDonVi,
          iID_MucLucNganSach,
		  sXauNoiMa
		  into #tblPhanBoDuToan
   FROM BH_DTC_PhanBoDuToanChi_ChiTiet
   WHERE iID_DTC_PhanBoDuToanChi IN
       (SELECT ID
        FROM BH_DTC_PhanBoDuToanChi
        WHERE sSoQuyetDinh <> ''
          AND sSoQuyetDinh IS NOT NULL
          AND iNamChungTu = @YearOfWork
          --AND iID_LoaiDanhMucChi = @iID_LoaiDanhMucChi
		  AND bIsKhoa=1
          AND cast(dNgayQuyetDinh AS date) <= cast(@VoucherDate AS date))
     AND iID_MaDonVi in  (SELECT * FROM f_split(@AgencyId))-- donvi
   GROUP BY iID_MaDonVi, 
   iID_MucLucNganSach,
   sXauNoiMa

	SELECT DISTINCT iID_MucLucNganSach
	--, iID_MLNS_Cha 
	into #tblPhanBoDuToanGroupbyiID_MLNS from #tblPhanBoDuToan

	SELECT T.* INTO #tblNsMlns1
	FROM #tblNsMlns T
	INNER JOIN #tblPhanBoDuToanGroupbyiID_MLNS E ON T.iID_MLNS = E.iID_MucLucNganSach 


	;WITH C AS  
	(  
	  SELECT *
	  FROM #tblNsMlns1
	  UNION ALL 
	  SELECT T.*
	  FROM #tblNsMlns T
	  INNER JOIN C
	  ON T.iID_MLNS = C.iID_MLNS_Cha
	)

	SELECT DISTINCT * into #mlnsPhanBo from C;

	-- lấy ra mlns theo phân cấp
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa,
			CASE
				WHEN (sNG is null OR sNG = '') THEN cast(1 as bit)
			ELSE cast(0 as bit)
			END AS bHangCha	
			into #tblMlnsByPhanCap
	FROM #mlnsPhanBo 
	WHERE 
		sLNS IN (SELECT * FROM #tempLNS)

	-- lấy ra đơn vị từ đơn vị của chứng từ
	SELECT iID_MaDonVi, iID_MaDonVi + ' - ' + sTenDonVi as sTenDonVi into #tblDonVi
	FROM DonVi 
	WHERE 
		INamLamViec = @YearOfWork 
		AND iID_MaDonVi IN (SELECT * FROM #tempAgency)

	-- Tao bang tam luu chu mlng con
	SELECT * INTO #tblMlnsByPhanCapExistDonVi
	FROM #tblMlnsByPhanCap tbl, #tblDonVi dv
	WHERE  tbl.bHangCha = 0 OR (tbl.bHangCha = 1 and (IsNull(tbl.sM, '') != '' OR tbl.sM <> '' OR IsNull(tbl.sTM, '') != '')  )
	AND tbl.sTM !=''  


	-- Tao bang tam luu chu mlng cha
	SELECT *, '' AS iID_MaDonVi, '' AS sTenDonVi  INTO #tblMlnsByPhanCapNotDonVi
	FROM #tblMlnsByPhanCap 
		WHERE bHangCha = 1 AND ( (IsNull(sM, '') = '' or sM = '')) AND ( (IsNull(sTM, '') = '' or sTM = '')) OR ((ISNULL(sTM,'')='' OR sTM='')) OR sTM='0'

	-- map bảng mlnsPhanCap và đơn vị
	SELECT * INTO #tblMLNS FROM (
		SELECT *
		FROM #tblMlnsByPhanCapExistDonVi tbl
		WHERE sTM !='0'
		UNION ALL
		SELECT *
		FROM #tblMlnsByPhanCapNotDonVi 
	) mlns

	-- map bảng mlns ns và đơn vị
	SELECT * INTO #tblMlnsRoot FROM (
		SELECT * FROM #tblNsMlns, #tblDonVi 
		--WHERE bHangCha = 0
		--UNION ALL
		--SELECT *, null AS iID_MaDonVi, null AS sTenDonVi  
		--FROM #tblNsMlns 
		--WHERE bHangCha = 1
	) mlns

	-- lấy dữ liệu đã cấp
	SELECT  
		SUM(fTienDeNghiQuyetToanQuyNay) AS fTienQuyetToanDaDuyet,
		CT.iID_MaDonVi,
		CTCT.sXauNoiMa
		into #tblDataQuyetToanDaDuyetQuyTruoc
	FROM
	BH_QTC_Quy_KinhPhiQuanLy CT
	INNER JOIN BH_QTC_Quy_KinhPhiQuanLy_chiTiet CTCT
	ON CT.ID_QTC_Quy_KinhPhiQuanLy=CTCT.iID_QTC_Quy_KinhPhiQuanLy
	WHERE CT.iNamChungTu = @YearOfWork
		  AND CT.iID_MaDonVi IN (SELECT * FROM f_split(@AgencyId))
		  AND CT.iQuyChungTu<@iQuyChungTu

	GROUP BY  CT.iID_MaDonVi,CTCT.sXauNoiMa

	SELECT ctct.sXauNoiMa,
							SUM(ctct.fTienTuChi) fTienTuChi
							into #tempDuToanTrenGiao
							FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct 
							JOIN BH_DTC_DuToanChiTrenGiao ct 
							ON ctct.iID_DTC_DuToanChiTrenGiao = ct.ID 
		WHERE ct.iID_MaDonVi = @AgencyId
		AND BIsKhoa = 1
		AND ct.iNamLamViec = @YearOfWork
		GROUP BY ctct.sXauNoiMa

	-- Get data
	IF (@Loai=1)
	SELECT
		isnull(ctct.ID_QTC_Quy_KinhPhiQuanLy_ChiTiet, @iD) AS ID_QTC_Quy_KinhPhiQuanLy_ChiTiet,
		@VoucherId AS iID_QTC_Quy_KinhPhiQuanLy,
		mlnsPhanBo.iID_MLNS AS iID_MucLucNganSach,
		mlnsPhanBo.iID_MLNS_Cha AS IdParent,
		mlnsPhanBo.sXauNoiMa AS sXauNoiMa,
		mlnsPhanBo.sLNS AS SLNS,
		mlnsPhanBo.sL AS SL,
		mlnsPhanBo.sK AS SK,
		mlnsPhanBo.sM AS SM,
		mlnsPhanBo.sTM AS STM,
		mlnsPhanBo.sTTM AS STTM,
		mlnsPhanBo.sNG AS SNG,
		mlnsPhanBo.sTNG AS STNG,
		mlnsPhanBo.sTNG1 AS STNG1,
		mlnsPhanBo.sTNG2 AS STNG2,
		mlnsPhanBo.sTNG3 AS STNG3,
		mlnsPhanBo.sMoTa AS SNoiDung,
		mlnsPhanBo.bHangCha As IsHangCha,
		mlnsPhanBo.sDuToanChiTietToi,
		mlnsPhanBo.bHangChaDuToan,
		@YearOfWork AS INamLamViec,
		mlnsPhanBo.iID_MaDonVi AS IID_MaDonVi,
		mlnsPhanBo.sTenDonVi AS STenDonVi,
		isnull(ctct.fTienDeNghiQuyetToanQuyNay, 0) as fTienDeNghiQuyetToanQuyNay,
		isnull(daCapDuToan.fTienDuToanDuocGiao, 0) as fTienDuToanDuocGiao,
		isnull(dataQuyTruoc.fTienQuyetToanDaDuyet, 0) as fTienQuyetToanDaDuyet,
		isnull(ctct.fTienThucChi, 0) as fTienThucChi,
		isnull(ctct.fTienXacNhanQuyetToanQuyNay, 0) as fTienXacNhanQuyetToanQuyNay,
		ctct.sGhiChu AS SGhiChu,
		isnull(ctct.dNgayTao, getdate()) AS DNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS DNgaySua,
		ctct.sNguoiTao AS SNguoiTao,
		ctct.sNguoiSua AS SNguoiSua
	FROM #tblMlnsRoot AS mlnsPhanBo
	LEFT JOIN
		(SELECT *
			FROM 
				BH_QTC_Quy_KinhPhiQuanLy_chiTiet
			WHERE 
		 		iID_QTC_Quy_KinhPhiQuanLy = @VoucherId
		) ctct
	ON mlnsPhanBo.iID_MLNS = ctct.iID_MucLucNganSach --and mlnsPhanBo.iID_MaDonVi = ctct.iID_MaDonVi
	LEFT JOIN BH_QTC_Quy_KinhPhiQuanLy ct ON ctct.iID_QTC_Quy_KinhPhiQuanLy = ct.ID_QTC_Quy_KinhPhiQuanLy
	LEFT JOIN #tblPhanBoDuToan daCapDuToan
	ON mlnsPhanBo.sXauNoiMa = daCapDuToan.sXauNoiMa 
	LEFT JOIN #tblDataQuyetToanDaDuyetQuyTruoc dataQuyTruoc
	ON mlnsPhanBo.sXauNoiMa = dataQuyTruoc.sXauNoiMa and  mlnsPhanBo.iID_MaDonVi=dataQuyTruoc.iID_MaDonVi
	WHERE mlnsPhanBo.sLNS IN (SELECT * FROM #tempLNS)
	ORDER BY mlnsPhanBo.sXauNoiMa, mlnsPhanBo.iID_MaDonVi

	ELSE
		SELECT
		isnull(ctct.ID_QTC_Quy_KinhPhiQuanLy_ChiTiet, @iD) AS ID_QTC_Quy_KinhPhiQuanLy_ChiTiet,
		@VoucherId AS iID_QTC_Quy_KinhPhiQuanLy,
		mlnsPhanBo.iID_MLNS AS iID_MucLucNganSach,
		mlnsPhanBo.iID_MLNS_Cha AS IdParent,
		mlnsPhanBo.sXauNoiMa AS sXauNoiMa,
		mlnsPhanBo.sLNS AS SLNS,
		mlnsPhanBo.sL AS SL,
		mlnsPhanBo.sK AS SK,
		mlnsPhanBo.sM AS SM,
		mlnsPhanBo.sTM AS STM,
		mlnsPhanBo.sTTM AS STTM,
		mlnsPhanBo.sNG AS SNG,
		mlnsPhanBo.sTNG AS STNG,
		mlnsPhanBo.sTNG1 AS STNG1,
		mlnsPhanBo.sTNG2 AS STNG2,
		mlnsPhanBo.sTNG3 AS STNG3,
		mlnsPhanBo.sMoTa AS SNoiDung,
		mlnsPhanBo.bHangCha As IsHangCha,
		mlnsPhanBo.sDuToanChiTietToi,
		mlnsPhanBo.bHangChaDuToan,
		@YearOfWork AS INamLamViec,
		mlnsPhanBo.iID_MaDonVi AS IID_MaDonVi,
		mlnsPhanBo.sTenDonVi AS STenDonVi,
		isnull(ctct.fTienDeNghiQuyetToanQuyNay, 0) as fTienDeNghiQuyetToanQuyNay,
		isnull(daCapDuToan.fTienTuChi, 0) as fTienDuToanDuocGiao,
		isnull(dataQuyTruoc.fTienQuyetToanDaDuyet, 0) as fTienQuyetToanDaDuyet,
		isnull(ctct.fTienThucChi, 0) as fTienThucChi,
		isnull(ctct.fTienXacNhanQuyetToanQuyNay, 0) as fTienXacNhanQuyetToanQuyNay,
		ctct.sGhiChu AS SGhiChu,
		isnull(ctct.dNgayTao, getdate()) AS DNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS DNgaySua,
		ctct.sNguoiTao AS SNguoiTao,
		ctct.sNguoiSua AS SNguoiSua
	FROM #tblMlnsRoot AS mlnsPhanBo
	LEFT JOIN
		(SELECT *
			FROM 
				BH_QTC_Quy_KinhPhiQuanLy_chiTiet
			WHERE 
		 		iID_QTC_Quy_KinhPhiQuanLy = @VoucherId
		) ctct
	ON mlnsPhanBo.iID_MLNS = ctct.iID_MucLucNganSach --and mlnsPhanBo.iID_MaDonVi = ctct.iID_MaDonVi
	LEFT JOIN BH_QTC_Quy_KinhPhiQuanLy ct ON ctct.iID_QTC_Quy_KinhPhiQuanLy = ct.ID_QTC_Quy_KinhPhiQuanLy
	LEFT JOIN #tempDuToanTrenGiao daCapDuToan
	ON mlnsPhanBo.sXauNoiMa = daCapDuToan.sXauNoiMa
	LEFT JOIN #tblDataQuyetToanDaDuyetQuyTruoc dataQuyTruoc
	ON mlnsPhanBo.sXauNoiMa = dataQuyTruoc.sXauNoiMa and  mlnsPhanBo.iID_MaDonVi=dataQuyTruoc.iID_MaDonVi
	WHERE mlnsPhanBo.sLNS IN (SELECT * FROM #tempLNS)
	ORDER BY mlnsPhanBo.sXauNoiMa, mlnsPhanBo.iID_MaDonVi;
	--END
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT]    Script Date: 7/ @Donvitinh7/2024 5:36:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT]
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
			or gt.sXauNoiMa like '9010002-010-011-0004%'
			or gt.sXauNoiMa like '9010002-010-011-0005%'
			or gt.sXauNoiMa like '9010002-010-011-0006%'
			or gt.sXauNoiMa like '9010002-010-011-0007%'
			or gt.sXauNoiMa like '9010002-010-011-0008%')
		and gt.iNamLamViec = @INamLamViec and gt.iQuy = @IQuy and gt.iiD_MaDonVi in (SELECT * FROM f_split(@IdMaDonVi))

	--- 9010001-010-011-0004 Tro cap Huu tri
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			into #tempDetailHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC
	SELECT * INTO #tempHuuTri FROM
	(
	SELECT 1 bHangCha
			, N'(I)' STT
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
	SELECT * FROM #tempDetailHuuTri	
	) tblHuutri

	 if (SELECT COUNT(1) FROM #tempHuuTri) > 2
		UPDATE #tempHuuTri set bHasData = 1

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
		FROM #tempHuuTri WHERE  bHangCha=0 ) detail
		where #tempHuuTri.bHangCha=1
	--- 9010001-010-011-0005 Tro cap phuc vien

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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			into #tempDetailPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac	
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC
	SELECT * INTO #tempPhucVien FROM
	(
	SELECT 1 bHangCha
			, N'(II)' STT
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
	SELECT * FROM #tempDetailPhucVien
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
		FROM #tempPhucVien WHERE  bHangCha=0 ) detail
		where #tempPhucVien.bHangCha=1

	--- 9010001-010-011-0006 Tro cap xuat ngu

	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			--, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sSoQuyetDinh) AS VARCHAR(6)) STT
			--, Convert(varchar(10),(COUNT(tbltc.sSoQuyetDinh))) + N' Đồng chi' as sTenCanBo
			--, tbltc.sTenPhanHo
			--, '' as SMaCapBac		
			--, tbltc.sSoQuyetDinh
			--, tbltc.dNgayQuyetDinh
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			--, 0 AS  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS  FTienTroCapMT
			--, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			INTO #tempDetailXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac	
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC
	SELECT * INTO #tempXuatNgu FROM
	(
	SELECT 1 bHangCha
			, N'(III)' STT
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
	SELECT * FROM #tempDetailXuatNgu
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
		FROM #tempXuatNgu WHERE  bHangCha=0 ) detail
		where #tempXuatNgu.bHangCha=1
	--- 9010001-010-011-0007 tro cap thoi viec

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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			INTO #tempDetailThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC
	SELECT * INTO #tempThoiViec FROM
	(
	SELECT 1 bHangCha
			, N'(IV)' STT
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
	SELECT * FROM #tempDetailThoiViec
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
		FROM #tempThoiViec WHERE  bHangCha=0 ) detail
		where #tempThoiViec.bHangCha=1
	--- 9010001-010-011-0008 tro cap tu tuat

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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			INTO #tempDetailTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC
	SELECT * INTO #tempTuTuat FROM
	(
	SELECT 1 bHangCha
			, N'(V)' STT
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
	SELECT * FROM #tempDetailTuTuat
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
		FROM #tempTuTuat WHERE  bHangCha=0 ) detail
		where #tempTuTuat.bHangCha=1
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
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Khoi]    Script Date: 7/ @Donvitinh7/2024 5:36:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Khoi]
	@INamLamViec int,
	@IdMaDonVi nvarchar(MAX),
	@IQuy int,
	@Donvitinh int
AS
BEGIN

	---IRemainRow 1: SQ, 2 : QNCN,3:CC, CN, VCQP,4:HSQBS, 5:LĐHĐ
	--- Type  :3 :Huu Tri,phuc vien,xuat ngu,thoi viec, tu tuat
	--- 2: Khoi du Toan, Khoi hach toan
	--- 1 SQ, QNCN,CC, CN, VCQP,HSQBS, LĐHĐ
	---- Ikhoi : 2  Khoi Du toan, 1 Khoi Hach Toan
	SELECT gt.* INTO #tblTroCapKhoiDuToan FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gt
	left join DonVi dv on gt.iiD_MaDonVi=dv.iID_MaDonVi
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
		and dv.iKhoi=2

	SELECT gt.* INTO #tblTroCapKhoiHachToan FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gt
	left join DonVi dv on gt.iiD_MaDonVi=dv.iID_MaDonVi
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
		and dv.iKhoi=1

		--- Huu Tri Khoi Du Toan
			SELECT 
				1 bHangCha
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
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempHuuTriDuToan

		--- Phuc Vien Khoi Du Toan
			SELECT 
				1 bHangCha
				, N'(II)' STT
				, N'TC Phục viên 'as STenCanBo 
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
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempPhucVienDuToan

		--- TC Xuất ngũ Khoi Du Toan
			SELECT 
				1 bHangCha
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
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempXuatNguDuToan

		--- TC Thôi việc  Khoi Du Toan
			SELECT 
				1 bHangCha
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
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempThoiViecDuToan

		--- TC Tử tuất  Khoi Du Toan
			SELECT 
				1 bHangCha
				, N'(V)' STT
				, N'TC Tử tuất 'as STenCanBo 
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
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempTuTuatDuToan

		--- Huu Tri Khoi Hach Toan
			SELECT 
				1 bHangCha
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
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempHuuTriHachToan

		--- Phuc Vien Khoi Hach Toan
			SELECT 
				1 bHangCha
				, N'(II)' STT
				, N'TC Phục viên 'as STenCanBo 
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
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempPhucVienHachToan

		--- TC Xuất ngũ Khoi Hach Toan
			SELECT 
				1 bHangCha
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
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempXuatNguHachToan

		--- TC Thôi việc  Khoi Hach Toan

			SELECT 
				1 bHangCha
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
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempThoiViecHachToan

		--- TC Tử tuất  Khoi Hach Toan
			SELECT 
				1 bHangCha
				, N'(V)' STT
				, N'TC Tử tuất 'as STenCanBo 
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
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempTuTuatHachToan

		--- Khoi Du Toan
			SELECT 
					1 bHangCha
					, N'Khối dự toán' STT
					, ''as STenCanBo 
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
					, 0 IRemainRow
					, 2 IKhoi
					into #tempKhoiDuToan

		--- Khoi Hach Toan
			SELECT 
					1 bHangCha
					, N'Khối hạch toán' STT
					, ''as STenCanBo 
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
					, 0 IRemainRow
					, 1 IKhoi
					into #tempKhoiHachToan

		--- Si Quan
			SELECT 
				1 bHangCha
				, N'Si quan' STT
				, ''as STenCanBo 
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
				, 1 Type
				, 1 IsParent
				, 1 IRemainRow
				, 0 IKhoi
				into #tempSiQuan

		--- QNCN
			SELECT 
				1 bHangCha
				, N'QNCN' STT
				, ''as STenCanBo 
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
				, 1 Type
				, 1 IsParent
				, 2 IRemainRow
				, 0 IKhoi
				into #tempQNCN

		--- CC, CN, VCQP
			SELECT 
				1 bHangCha
				, N'CC, CN, VCQP' STT
				, ''as STenCanBo 
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
				, 1 Type
				, 1 IsParent
				, 3 IRemainRow
				, 0 IKhoi
				into #tempCNVCQP

        --- HSQBS
			SELECT 
				1 bHangCha
				, N'HSQBS' STT
				, ''as STenCanBo 
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
				, 1 Type
				, 1 IsParent
				, 4 IRemainRow
				, 0 IKhoi
				into #tempHSQBS

		--- LĐHĐ
			SELECT 
				1 bHangCha
				, N'LĐHĐ' STT
				, ''as STenCanBo 
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
				, 1 Type
				, 1 IsParent
				, 5 IRemainRow
				, 0 IKhoi
				into #tempLDHD
--- Du Toan
--- Huu Tri Du Toan
		--- Detal Si Quan DuToan Huu Tri
			SELECT  
			0 bHangCha 
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi
			into #tempDetailSiQuanDuToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

		--- Detal QNCN DuToan Huu Tri
			SELECT  
			0 bHangCha 
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 2 IKhoi
			into #tempDetailQNCNDuToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal CNVCQP DuToan Huu Tri
			SELECT  
			0 bHangCha 
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 2 IKhoi
			into #tempDetailCNVCQPDuToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal HSQBS DuToan Huu Tri
			SELECT  
			0 bHangCha 
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi
			into #tempDetailHSQBSDuToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac LIKE '0%')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal LDHD DuToan Huu Tri
			SELECT  
			0 bHangCha 
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 2 IKhoi
			into #tempDetailLDHDDuToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

--- Phuc Vien Du Toan
			--- Detal Si Quan  Phục viên 
			SELECT  
			0 bHangCha 
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi
			into #tempDetailSiQuanDuToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal QNCN DuToan Phục viên 
			SELECT  
			0 bHangCha 
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 2 IKhoi
			into #tempDetailQNCNDuToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal CNVCQP DuToan Phuc Vien
			SELECT  
			0 bHangCha 
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 2 IKhoi
			into #tempDetailCNVCQPDuToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal HSQBS DuToan Phục viên 
			SELECT  
			0 bHangCha 
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi
			into #tempDetailHSQBSDuToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal LDHD DuToan Phuc Vien
			SELECT  
			0 bHangCha 
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 2 IKhoi
			into #tempDetailLDHDDuToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

--- Xuat Ngu Du Toan
			--- Detal Si Quan  Xuat Ngu
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sSoQuyetDinh) AS VARCHAR(6)) STT
			, Convert(varchar(10),(COUNT(tbltc.sSoQuyetDinh))) + N' Đồng chí' as sTenCanBo
			, tbltc.sTenPhanHo
			, '' as SMaCapBac	
			--, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			--, tbltc.sTenCanBo
			--, tbltc.sTenPhanHo
			--, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi
			into #tempDetailSiQuanDuToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY tbltc.sTenPhanHo
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal QNCN DuToan Xuat Ngu 
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sSoQuyetDinh) AS VARCHAR(6)) STT
			, Convert(varchar(10),(COUNT(tbltc.sSoQuyetDinh))) + N' Đồng chí' as sTenCanBo
			, tbltc.sTenPhanHo
			, '' as SMaCapBac	
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 2 IKhoi
			into #tempDetailQNCNDuToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sTenPhanHo
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal CNVCQP DuToan Phuc Vien
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sSoQuyetDinh) AS VARCHAR(6)) STT
			, Convert(varchar(10),(COUNT(tbltc.sSoQuyetDinh))) + N' Đồng chí' as sTenCanBo
			, tbltc.sTenPhanHo
			, '' as SMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 2 IKhoi
			into #tempDetailCNVCQPDuToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%' )
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sTenPhanHo
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal HSQBS DuToan Xuat Ngu
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sSoQuyetDinh) AS VARCHAR(6)) STT
			, Convert(varchar(10),(COUNT(tbltc.sSoQuyetDinh))) + N' Đồng chí' as sTenCanBo
			, tbltc.sTenPhanHo
			, '' as SMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi
			into #tempDetailHSQBSDuToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY   tbltc.sTenPhanHo
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal LDHD DuToan Xuat Ngu
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sSoQuyetDinh) AS VARCHAR(6)) STT
			, Convert(varchar(10),(COUNT(tbltc.sSoQuyetDinh))) + N' Đồng chí' as sTenCanBo
			, tbltc.sTenPhanHo
			, '' as SMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 2 IKhoi
			into #tempDetailLDHDDuToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY   tbltc.sTenPhanHo
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

--- Thoi Viec Du Toan
			--- Detal Si Quan DuToan Thoi Viec
			SELECT  
			0 bHangCha 
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi
			into #tempDetailSiQuanDuToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal QNCN DuToan Thoi Viec
			SELECT  
			0 bHangCha 
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 2 IKhoi
			into #tempDetailQNCNDuToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal CNVCQP DuToan Thoi Viec
			SELECT  
			0 bHangCha 
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 2 IKhoi
			into #tempDetailCNVCQPDuToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%' )
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal HSQBS DuToan Thoi Viec
			SELECT  
			0 bHangCha 
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi
			into #tempDetailHSQBSDuToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal LDHD DuToan Thoi Viec
			SELECT  
			0 bHangCha 
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 2 IKhoi
			into #tempDetailLDHDDuToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

--- Tu Tuat  Du Toan
			--- Detal Si Quan DuToan Tu Tuat
			SELECT  
			0 bHangCha 
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi
			into #tempDetailSiQuanDuToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal QNCN DuToan Tu Tuat
			SELECT  
			0 bHangCha 
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 2 IKhoi
			into #tempDetailQNCNDuToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal CNVCQP DuToan Tu Tuat
			SELECT  
			0 bHangCha 
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 2 IKhoi
			into #tempDetailCNVCQPDuToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%' )
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal HSQBS DuToan Tu Tuat
			SELECT  
			0 bHangCha 
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi
			into #tempDetailHSQBSDuToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal LDHD DuToan Thoi Viec
			SELECT  
			0 bHangCha 
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 2 IKhoi
			into #tempDetailLDHDDuToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
--- Hach Toan
--- Huu Tri HachToan
		--- Detal Si Quan HachToan Huu Tri
			SELECT  
			0 bHangCha 
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi
			into #tempDetailSiQuanHachToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

		--- Detal QNCN HachToan Huu Tri
			SELECT  
			0 bHangCha 
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi
			into #tempDetailQNCNHachToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal CNVCQP HachToan Huu Tri
			SELECT  
			0 bHangCha 
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi
			into #tempDetailCNVCQPHachToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal HSQBS HachToan Huu Tri
			SELECT  
			0 bHangCha 
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi
			into #tempDetailHSQBSHachToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac LIKE '0%')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal LDHD HachToan Huu Tri
			SELECT  
			0 bHangCha 
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi
			into #tempDetailLDHDHachToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

--- Phuc Vien Hach Toan
			--- Detal Si Quan HachToan Phục viên 
			SELECT  
			0 bHangCha 
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi
			into #tempDetailSiQuanHachToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal QNCN HachToan Phục viên 
			SELECT  
			0 bHangCha 
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi
			into #tempDetailQNCNHachToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal CNVCQP HachToan Phuc Vien
			SELECT  
			0 bHangCha 
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi
			into #tempDetailCNVCQPHachToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal HSQBS HachToan Phục viên 
			SELECT  
			0 bHangCha 
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi
			into #tempDetailHSQBSHachToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal LDHD HachToan Phuc Vien
			SELECT  
			0 bHangCha 
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi
			into #tempDetailLDHDHachToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

--- Xuat Ngu Hach Toan
			--- Detal Si Quan HachToan  Xuat Ngu
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi
			into #tempDetailSiQuanHachToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal QNCN HachToan Xuat Ngu 
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi
			into #tempDetailQNCNHachToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal CNVCQP HachToan Phuc Vien
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi
			into #tempDetailCNVCQPHachToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%' )
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal HSQBS HachToan Xuat Ngu
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi
			into #tempDetailHSQBSHachToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal LDHD HachToan Xuat Ngu
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi
			into #tempDetailLDHDHachToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

--- Thoi Viec Du Toan
			--- Detal Si Quan HachToan Thoi Viec
			SELECT  
			0 bHangCha 
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi
			into #tempDetailSiQuanHachToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal QNCN HachToan Thoi Viec
			SELECT  
			0 bHangCha 
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi
			into #tempDetailQNCNHachToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal CNVCQP HachToan Thoi Viec
			SELECT  
			0 bHangCha 
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi
			into #tempDetailCNVCQPHachToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%' )
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal HSQBS HachToan Thoi Viec
			SELECT  
			0 bHangCha 
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi
			into #tempDetailHSQBSHachToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal LDHD HachToan Thoi Viec
			SELECT  
			0 bHangCha 
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi
			into #tempDetailLDHDHachToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

--- Tu Tuat  Du Toan
			--- Detal Si Quan HachToan Tu Tuat
			SELECT  
			0 bHangCha 
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi
			into #tempDetailSiQuanHachToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal QNCN HachToan Tu Tuat
			SELECT  
			0 bHangCha 
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi
			into #tempDetailQNCNHachToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal CNVCQP HachToan Tu Tuat
			SELECT  
			0 bHangCha 
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi
			into #tempDetailCNVCQPHachToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%' )
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal HSQBS HachToan Tu Tuat
			SELECT  
			0 bHangCha 
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi
			into #tempDetailHSQBSHachToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

			--- Detal LDHD HachToan Thoi Viec
			SELECT  
			0 bHangCha 
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi
			into #tempDetailLDHDHachToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa

	--- 9010001-010-011-0004 Tro cap Huu tri
			SELECT * INTO #tempDetailDuToanHuuTri FROM 
			(
				SELECT * FROM #tempKhoiDuToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanDuToanHuuTri
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNDuToanHuuTri
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPDuToanHuuTri
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSDuToanHuuTri
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDDuToanHuuTri
			)tblDetailHuuTri

			SELECT * INTO #tempDetailHachToanHuuTri FROM 
			(
				SELECT * FROM #tempKhoiHachToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanHachToanHuuTri
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNHachToanHuuTri
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPHachToanHuuTri
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSHachToanHuuTri
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDHachToanHuuTri
			)tblDetailHuuTri

			--- UPDATE #tempDetailDuToanHuuTri
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanHuuTri
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanHuuTri
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanHuuTri
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanHuuTri
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanHuuTri
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Du Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanHuuTri
				WHERE Type=0
			) B
			where  A.Type=2

			--- UPDATE ##tempDetailHachToanHuuTri
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanHuuTri
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanHuuTri
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanHuuTri
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanHuuTri
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanHuuTri
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Hach Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanHuuTri
				WHERE Type=0
			) B
			where  A.Type=2

			SELECT * INTO #tempHuuTri FROM
			(
			SELECT * FROM #tempHuuTriDuToan
			UNION ALL
			SELECT * FROM #tempDetailDuToanHuuTri
			UNION ALL
			SELECT * FROM #tempDetailHachToanHuuTri
			
			) tblHuutri

			--- UPDATE #tempHuuTri
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempHuuTri
				WHERE Type=0
			) B
			where  A.Type=3


	----- 9010001-010-011-0005 Tro cap phuc vien
			SELECT * INTO #tempDetailDuToanPhucVien FROM 
			(
				SELECT * FROM #tempKhoiDuToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanDuToanPhucVien
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNDuToanPhucVien
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPDuToanPhucVien
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSDuToanPhucVien
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDDuToanPhucVien
			)tblDetailPhucVien

			SELECT * INTO #tempDetailHachToanPhucVien FROM 
			(
				SELECT * FROM #tempKhoiHachToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanHachToanPhucVien
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNHachToanPhucVien
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPHachToanPhucVien
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSHachToanPhucVien
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDHachToanPhucVien
			)tblDetailPhucVien

		
			--- UPDATE ##tempDetailDuToanPhucVien
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanPhucVien
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanPhucVien
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanPhucVien
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanPhucVien
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanPhucVien
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Du Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanPhucVien
				WHERE Type=0
			) B
			where  A.Type=2

			--- UPDATE ###tempDetailHachToanPhucVien
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanPhucVien
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanPhucVien
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanPhucVien
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanPhucVien
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanPhucVien
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Hach Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanPhucVien
				WHERE Type=0
			) B
			where  A.Type=2

			SELECT * INTO #tempPhucVien FROM
			(
			SELECT * FROM #tempPhucVienDuToan
			UNION ALL
			SELECT * FROM #tempDetailDuToanPhucVien
			UNION ALL
			SELECT * FROM #tempDetailHachToanPhucVien
			) tblPhucVien

			--- UPDATE #tempPhucVien
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempPhucVien
				WHERE Type=0
			) B
			where  A.Type=3

			
	----- 9010001-010-011-0006 Tro cap xuat ngu

			SELECT * INTO #tempDetailDuToanXuatNgu FROM 
			(
				SELECT * FROM #tempKhoiDuToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanDuToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNDuToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPDuToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSDuToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDDuToanXuatNgu
			)tblDetailPXuatNgu


			SELECT * INTO #tempDetailHachToanXuatNgu FROM 
			(
				SELECT * FROM #tempKhoiHachToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanHachToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNHachToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPHachToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSHachToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDHachToanXuatNgu
			)tblDetailXuatNgu

			--- UPDATE #tempDetailDuToanXuatNgu
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanXuatNgu
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanXuatNgu
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanXuatNgu
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanXuatNgu
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanXuatNgu
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Du Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanXuatNgu
				WHERE Type=0
			) B
			where  A.Type=2


			--- UPDATE #tempDetailHachToanXuatNgu
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanXuatNgu
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanXuatNgu
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanXuatNgu
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanXuatNgu
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanXuatNgu
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- Update Hach Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanXuatNgu
				WHERE Type=0
			) B
			where  A.Type=2
			SELECT * INTO #tempXuatNgu FROM
			(
			SELECT * FROM #tempXuatNguDuToan
			UNION ALL
			SELECT * FROM #tempDetailDuToanXuatNgu
			UNION ALL
			SELECT * FROM #tempDetailHachToanXuatNgu
			) tblXuatNgu

			--- UPDATE #tempXuatNgu
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempXuatNgu
				WHERE Type=0
			) B
			where  A.Type=3
	----- 9010001-010-011-0007 tro cap thoi viec
			SELECT * INTO #tempDetailDuToanThoiViec FROM 
			(
				SELECT * FROM #tempKhoiDuToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanDuToanThoiViec
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNDuToanThoiViec
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPDuToanThoiViec
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSDuToanThoiViec
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDDuToanThoiViec
			)tblDetailThoiViec

			SELECT * INTO #tempDetailHachToanThoiViec FROM 
			(
				SELECT * FROM #tempKhoiHachToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanHachToanThoiViec
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNHachToanThoiViec
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPHachToanThoiViec
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSHachToanThoiViec
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDHachToanThoiViec
			)tblDetailThoiViec

	 	--- UPDATE #tempDetailDuToanXuatNgu
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanThoiViec
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanThoiViec
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanThoiViec
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanThoiViec
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanThoiViec
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Du Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanThoiViec
				WHERE Type=0
			) B
			where  A.Type=2

			--- UPDATE #tempDetailHachToanThoiViec
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanThoiViec
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanThoiViec
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanThoiViec
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanThoiViec
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanThoiViec
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Hach Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanThoiViec
				WHERE Type=0
			) B
			where  A.Type=2

			SELECT * INTO #tempThoiViec FROM
			(
			SELECT * FROM #tempThoiViecDuToan
			UNION ALL
			SELECT * FROM #tempDetailDuToanThoiViec
			UNION ALL
			SELECT * FROM #tempDetailHachToanThoiViec
			) tblThoiViec
			 
			 --- UPDATE #tempThoiViec
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempThoiViec
				WHERE Type=0
			) B
			where  A.Type=3
		
	----- 9010001-010-011-0008 tro cap tu tuat
			SELECT * INTO #tempDetailDuToanTuTuat FROM 
			(
				SELECT * FROM #tempKhoiDuToan 
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanDuToanTuTuat
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNDuToanTuTuat
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPDuToanTuTuat
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSDuToanTuTuat
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDDuToanTuTuat
			)tblDetailTuTuat

			SELECT * INTO #tempDetailHachToanTuTuat FROM 
			(
				SELECT * FROM #tempKhoiHachToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanHachToanTuTuat
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNHachToanTuTuat
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPHachToanTuTuat
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSHachToanTuTuat
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDHachToanTuTuat
			)tblDetailTuTuat

			--- UPDATE #tempDetailDuToanTuTuat
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanTuTuat
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanTuTuat
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanTuTuat
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanTuTuat
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanTuTuat
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Du Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanTuTuat
				WHERE Type=0
			) B
			where  A.Type=2

			--- UPDATE #tempDetailHachToanTuTuat
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanTuTuat
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanTuTuat
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanTuTuat
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanTuTuat
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanTuTuat
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Hach Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanTuTuat
				WHERE Type=0
			) B
			where  A.Type=2
			SELECT * INTO #tempTuTuat FROM
			(
			SELECT * FROM #tempTuTuatDuToan
			UNION ALL
			SELECT * FROM #tempDetailDuToanTuTuat
			UNION ALL
			SELECT * FROM #tempDetailHachToanTuTuat
			) tblTuTuat

			--- Update #tempTuTuat
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempTuTuat
				WHERE Type=0
			) B
			where  A.Type=3

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


		 DROP TABLE #tempRESULT
		 DROP TABLE #tblTroCapKhoiDuToan 
		 DROP TABLE #tblTroCapKhoiHachToan
		 DROP TABLE #tempHuuTri
		 DROP TABLE #tempDetailDuToanHuuTri
		 DROP TABLE #tempDetailHachToanHuuTri


		 DROP TABLE #tempPhucVien
		 DROP TABLE #tempDetailDuToanPhucVien
		 DROP TABLE #tempDetailHachToanPhucVien

		 DROP TABLE #tempXuatNgu 
		 DROP TABLE #tempDetailDuToanXuatNgu
		 DROP TABLE #tempDetailHachToanXuatNgu

		 DROP TABLE #tempThoiViec
		 DROP TABLE #tempDetailDuToanThoiViec
		 DROP TABLE #tempDetailHachToanThoiViec

		 DROP TABLE #tempTuTuat
		 DROP TABLE #tempDetailDuToanTuTuat
		 DROP TABLE #tempDetailHachToanTuTuat

		 DROP TABLE #tempKhoiDuToan
		 DROP TABLE #tempKhoiHachToan

		 DROP TABLE #tempHuuTriDuToan
		 DROP TABLE #tempPhucVienDuToan
		 DROP TABLE #tempXuatNguDuToan
		 DROP TABLE #tempThoiViecDuToan
		 DROP TABLE #tempTuTuatDuToan

		 DROP TABLE #tempSiQuan
		 DROP TABLE #tempQNCN
		 DROP TABLE #tempCNVCQP
		 DROP TABLE #tempHSQBS
		 DROP TABLE #tempLDHD

		 DROP TABLE #tempDetailSiQuanDuToanHuuTri
		 DROP TABLE #tempDetailQNCNDuToanHuuTri
		 DROP TABLE #tempDetailCNVCQPDuToanHuuTri
		 DROP TABLE #tempDetailHSQBSDuToanHuuTri
		 DROP TABLE #tempDetailLDHDDuToanHuuTri

		 DROP TABLE #tempDetailSiQuanDuToanPhucVien
		 DROP TABLE #tempDetailQNCNDuToanPhucVien
		 DROP TABLE #tempDetailCNVCQPDuToanPhucVien
		 DROP TABLE #tempDetailHSQBSDuToanPhucVien
		 DROP TABLE #tempDetailLDHDDuToanPhucVien
		 
		 DROP TABLE #tempDetailSiQuanDuToanXuatNgu
		 DROP TABLE #tempDetailQNCNDuToanXuatNgu
		 DROP TABLE #tempDetailCNVCQPDuToanXuatNgu
		 DROP TABLE #tempDetailHSQBSDuToanXuatNgu
		 DROP TABLE #tempDetailLDHDDuToanXuatNgu

		 DROP TABLE #tempDetailSiQuanDuToanThoiViec
		 DROP TABLE #tempDetailQNCNDuToanThoiViec
		 DROP TABLE #tempDetailCNVCQPDuToanThoiViec
		 DROP TABLE #tempDetailHSQBSDuToanThoiViec
		 DROP TABLE #tempDetailLDHDDuToanThoiViec

		 DROP TABLE #tempDetailSiQuanDuToanTuTuat
		 DROP TABLE #tempDetailQNCNDuToanTuTuat
		 DROP TABLE #tempDetailCNVCQPDuToanTuTuat
		 DROP TABLE #tempDetailHSQBSDuToanTuTuat
		 DROP TABLE #tempDetailLDHDDuToanTuTuat

		 DROP TABLE #tempDetailSiQuanHachToanHuuTri
		 DROP TABLE #tempDetailQNCNHachToanHuuTri
		 DROP TABLE #tempDetailCNVCQPHachToanHuuTri
		 DROP TABLE #tempDetailHSQBSHachToanHuuTri
		 DROP TABLE #tempDetailLDHDHachToanHuuTri

		 DROP TABLE #tempDetailSiQuanHachToanPhucVien
		 DROP TABLE #tempDetailQNCNHachToanPhucVien
		 DROP TABLE #tempDetailCNVCQPHachToanPhucVien
		 DROP TABLE #tempDetailHSQBSHachToanPhucVien
		 DROP TABLE #tempDetailLDHDHachToanPhucVien
		 
		 DROP TABLE #tempDetailSiQuanHachToanXuatNgu
		 DROP TABLE #tempDetailQNCNHachToanXuatNgu
		 DROP TABLE #tempDetailCNVCQPHachToanXuatNgu
		 DROP TABLE #tempDetailHSQBSHachToanXuatNgu
		 DROP TABLE #tempDetailLDHDHachToanXuatNgu

		 DROP TABLE #tempDetailSiQuanHachToanThoiViec
		 DROP TABLE #tempDetailQNCNHachToanThoiViec
		 DROP TABLE #tempDetailCNVCQPHachToanThoiViec
		 DROP TABLE #tempDetailHSQBSHachToanThoiViec
		 DROP TABLE #tempDetailLDHDHachToanThoiViec

		 DROP TABLE #tempDetailSiQuanHachToanTuTuat
		 DROP TABLE #tempDetailQNCNHachToanTuTuat
		 DROP TABLE #tempDetailCNVCQPHachToanTuTuat
		 DROP TABLE #tempDetailHSQBSHachToanTuTuat
		 DROP TABLE #tempDetailLDHDHachToanTuTuat


		 DROP TABLE #tempHuuTriHachToan
		 DROP TABLE #tempPhucVienHachToan
		 DROP TABLE #tempXuatNguHachToan
		 Drop table #tempThoiViecHachToan
		 Drop table #tempTuTuatHachToan

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
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Khoi_Detail]    Script Date: 7/ @Donvitinh7/2024 5:36:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Khoi_Detail]
	@INamLamViec int,
	@IdMaDonVi nvarchar(MAX),
	@IQuy int,
	@Donvitinh int
AS
BEGIN

	---IRemainRow 1: SQ, 2 : QNCN,3:CC, CN, VCQP,4:HSQBS, 5:LĐHĐ
	--- Type  :3 :Huu Tri,phuc vien,xuat ngu,thoi viec, tu tuat
	--- 2: Khoi du Toan, Khoi hach toan
	--- 1 SQ, QNCN,CC, CN, VCQP,HSQBS, LĐHĐ
	---- Ikhoi : 2  Khoi Du toan, 1 Khoi Hach Toan
	SELECT gt.* INTO #tblTroCapKhoiDuToan FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gt
	left join DonVi dv on gt.iiD_MaDonVi=dv.iID_MaDonVi
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
		and dv.iKhoi=2
		order by gt.sTenCanBo

	SELECT gt.* INTO #tblTroCapKhoiHachToan FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gt
	left join DonVi dv on gt.iiD_MaDonVi=dv.iID_MaDonVi
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
		and dv.iKhoi=1
		order by gt.sTenCanBo
		--- Huu Tri Khoi Du Toan
			SELECT 
				1 bHangCha
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
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempHuuTriDuToan

		--- Phuc Vien Khoi Du Toan
			SELECT 
				1 bHangCha
				, N'(II)' STT
				, N'TC Phục viên 'as STenCanBo 
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
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempPhucVienDuToan

		--- TC Xuất ngũ Khoi Du Toan
			SELECT 
				1 bHangCha
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
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempXuatNguDuToan

		--- TC Thôi việc  Khoi Du Toan
			SELECT 
				1 bHangCha
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
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempThoiViecDuToan

		--- TC Tử tuất  Khoi Du Toan
			SELECT 
				1 bHangCha
				, N'(V)' STT
				, N'TC Tử tuất 'as STenCanBo 
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
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempTuTuatDuToan

		--- Huu Tri Khoi Hach Toan
			SELECT 
				1 bHangCha
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
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempHuuTriHachToan

		--- Phuc Vien Khoi Hach Toan
			SELECT 
				1 bHangCha
				, N'(II)' STT
				, N'TC Phục viên 'as STenCanBo 
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
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempPhucVienHachToan

		--- TC Xuất ngũ Khoi Hach Toan
			SELECT 
				1 bHangCha
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
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempXuatNguHachToan

		--- TC Thôi việc  Khoi Hach Toan

			SELECT 
				1 bHangCha
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
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempThoiViecHachToan

		--- TC Tử tuất  Khoi Hach Toan
			SELECT 
				1 bHangCha
				, N'(V)' STT
				, N'TC Tử tuất 'as STenCanBo 
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
				, 3 Type
				, 0 IsParent
				, 0 IRemainRow
				, 0 IKhoi
				into #tempTuTuatHachToan

		--- Khoi Du Toan
			SELECT 
					1 bHangCha
					, N'Khối dự toán' STT
					, ''as STenCanBo 
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
					, 0 IRemainRow
					, 2 IKhoi
					into #tempKhoiDuToan

		--- Khoi Hach Toan
			SELECT 
					1 bHangCha
					, N'Khối hạch toán' STT
					, ''as STenCanBo 
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
					, 0 IRemainRow
					, 1 IKhoi
					into #tempKhoiHachToan

		--- Si Quan
			SELECT 
				1 bHangCha
				, N'Si quan' STT
				, ''as STenCanBo 
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
				, 1 Type
				, 1 IsParent
				, 1 IRemainRow
				, 0 IKhoi
				into #tempSiQuan

		--- QNCN
			SELECT 
				1 bHangCha
				, N'QNCN' STT
				, ''as STenCanBo 
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
				, 1 Type
				, 1 IsParent
				, 2 IRemainRow
				, 0 IKhoi
				into #tempQNCN

		--- CC, CN, VCQP
			SELECT 
				1 bHangCha
				, N'CC, CN, VCQP' STT
				, ''as STenCanBo 
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
				, 1 Type
				, 1 IsParent
				, 3 IRemainRow
				, 0 IKhoi
				into #tempCNVCQP

        --- HSQBS
			SELECT 
				1 bHangCha
				, N'HSQBS' STT
				, ''as STenCanBo 
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
				, 1 Type
				, 1 IsParent
				, 4 IRemainRow
				, 0 IKhoi
				into #tempHSQBS

		--- LĐHĐ
			SELECT 
				1 bHangCha
				, N'LĐHĐ' STT
				, ''as STenCanBo 
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
				, 1 Type
				, 1 IsParent
				, 5 IRemainRow
				, 0 IKhoi
				into #tempLDHD
--- Du Toan
--- Huu Tri Du Toan
		--- Detal Si Quan DuToan Huu Tri
			SELECT  
			0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi
			into #tempDetailSiQuanDuToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
		--- Detal QNCN DuToan Huu Tri
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 2 IKhoi
			into #tempDetailQNCNDuToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal CNVCQP DuToan Huu Tri
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 2 IKhoi
			into #tempDetailCNVCQPDuToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal HSQBS DuToan Huu Tri
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi
			into #tempDetailHSQBSDuToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac LIKE '0%')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal LDHD DuToan Huu Tri
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 2 IKhoi
			into #tempDetailLDHDDuToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
--- Phuc Vien Du Toan
			--- Detal Si Quan  Phục viên 
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi
			into #tempDetailSiQuanDuToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal QNCN DuToan Phục viên 
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 2 IKhoi
			into #tempDetailQNCNDuToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal CNVCQP DuToan Phuc Vien
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 2 IKhoi
			into #tempDetailCNVCQPDuToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal HSQBS DuToan Phục viên 
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi
			into #tempDetailHSQBSDuToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal LDHD DuToan Phuc Vien
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 2 IKhoi
			into #tempDetailLDHDDuToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
--- Xuat Ngu Du Toan
			--- Detal Si Quan  Xuat Ngu
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi
			into #tempDetailSiQuanDuToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal QNCN DuToan Xuat Ngu 
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 2 IKhoi
			into #tempDetailQNCNDuToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal CNVCQP DuToan Phuc Vien
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 2 IKhoi
			into #tempDetailCNVCQPDuToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%' )
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal HSQBS DuToan Xuat Ngu
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi
			into #tempDetailHSQBSDuToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal LDHD DuToan Xuat Ngu
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 2 IKhoi
			into #tempDetailLDHDDuToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
--- Thoi Viec Du Toan
			--- Detal Si Quan DuToan Thoi Viec
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi
			into #tempDetailSiQuanDuToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal QNCN DuToan Thoi Viec
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 2 IKhoi
			into #tempDetailQNCNDuToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal CNVCQP DuToan Thoi Viec
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 2 IKhoi
			into #tempDetailCNVCQPDuToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%' )
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal HSQBS DuToan Thoi Viec
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi
			into #tempDetailHSQBSDuToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal LDHD DuToan Thoi Viec
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 2 IKhoi
			into #tempDetailLDHDDuToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
--- Tu Tuat  Du Toan
			--- Detal Si Quan DuToan Tu Tuat
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 2 IKhoi
			into #tempDetailSiQuanDuToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal QNCN DuToan Tu Tuat
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 2 IKhoi
			into #tempDetailQNCNDuToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal CNVCQP DuToan Tu Tuat
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 2 IKhoi
			into #tempDetailCNVCQPDuToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiDuToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%' )
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal HSQBS DuToan Tu Tuat
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 2 IKhoi
			into #tempDetailHSQBSDuToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal LDHD DuToan Thoi Viec
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 2 IKhoi
			into #tempDetailLDHDDuToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiDuToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
--- Hach Toan
--- Huu Tri HachToan
		--- Detal Si Quan HachToan Huu Tri
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi
			into #tempDetailSiQuanHachToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
		--- Detal QNCN HachToan Huu Tri
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi
			into #tempDetailQNCNHachToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal CNVCQP HachToan Huu Tri
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi
			into #tempDetailCNVCQPHachToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal HSQBS HachToan Huu Tri
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi
			into #tempDetailHSQBSHachToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac LIKE '0%')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal LDHD HachToan Huu Tri
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi
			into #tempDetailLDHDHachToanHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%')
	AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
--- Phuc Vien Hach Toan
			--- Detal Si Quan HachToan Phục viên 
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi
			into #tempDetailSiQuanHachToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal QNCN HachToan Phục viên 
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi
			into #tempDetailQNCNHachToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal CNVCQP HachToan Phuc Vien
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi
			into #tempDetailCNVCQPHachToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal HSQBS HachToan Phục viên 
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi
			into #tempDetailHSQBSHachToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal LDHD HachToan Phuc Vien
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi
			into #tempDetailLDHDHachToanPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
--- Xuat Ngu Hach Toan
			--- Detal Si Quan HachToan  Xuat Ngu
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi
			into #tempDetailSiQuanHachToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal QNCN HachToan Xuat Ngu 
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi
			into #tempDetailQNCNHachToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal CNVCQP HachToan Phuc Vien
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi
			into #tempDetailCNVCQPHachToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%' )
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal HSQBS HachToan Xuat Ngu
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi
			into #tempDetailHSQBSHachToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal LDHD HachToan Xuat Ngu
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi
			into #tempDetailLDHDHachToanXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
--- Thoi Viec Du Toan
			--- Detal Si Quan HachToan Thoi Viec
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi
			into #tempDetailSiQuanHachToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal QNCN HachToan Thoi Viec
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi
			into #tempDetailQNCNHachToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal CNVCQP HachToan Thoi Viec
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi
			into #tempDetailCNVCQPHachToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%' )
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal HSQBS HachToan Thoi Viec
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi
			into #tempDetailHSQBSHachToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal LDHD HachToan Thoi Viec
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi
			into #tempDetailLDHDHachToanThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
--- Tu Tuat  Du Toan
			--- Detal Si Quan HachToan Tu Tuat
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 1 IRemainRow
			, 1 IKhoi
			into #tempDetailSiQuanHachToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
	AND tbltc.sMaCapBac like  '1%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal QNCN HachToan Tu Tuat
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 2 IRemainRow
			, 1 IKhoi
			into #tempDetailQNCNHachToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
	AND tbltc.sMaCapBac  LIKE '2%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal CNVCQP HachToan Tu Tuat
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 3 IRemainRow
			, 1 IKhoi
			into #tempDetailCNVCQPHachToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCapKhoiHachToan tbltc
	WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%' )
	AND ( tbltc.sMaCapBac LIKE '3.1%' 
			OR tbltc.sMaCapBac LIKE '3.2%' 
			OR tbltc.sMaCapBac LIKE '3.3%'  
			OR tbltc.sMaCapBac = '413' 
			OR tbltc.sMaCapBac = '415')
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal HSQBS HachToan Tu Tuat
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 4 IRemainRow
			, 1 IKhoi
			into #tempDetailHSQBSHachToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
		AND ( tbltc.sMaCapBac LIKE '0%')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
			--- Detal LDHD HachToan Thoi Viec
			SELECT  
			0 bHangCha 
		   , CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo desc) AS VARCHAR(6)) STT
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
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			, 0 Type
			, null IsParent
			, 5 IRemainRow
			, 1 IKhoi
			into #tempDetailLDHDHachToanTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
		FROM  #tblTroCapKhoiHachToan tbltc
		WHERE (tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%')
		AND ( tbltc.sMaCapBac  = '423' OR tbltc.sMaCapBac = '425' OR tbltc.sMaCapBac = '43')
		GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sTenCanBo ASC
	--- 9010001-010-011-0004 Tro cap Huu tri
			SELECT * INTO #tempDetailDuToanHuuTri FROM 
			(
				SELECT * FROM #tempKhoiDuToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanDuToanHuuTri
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNDuToanHuuTri
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPDuToanHuuTri
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSDuToanHuuTri
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDDuToanHuuTri
			)tblDetailHuuTri

			SELECT * INTO #tempDetailHachToanHuuTri FROM 
			(
				SELECT * FROM #tempKhoiHachToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanHachToanHuuTri
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNHachToanHuuTri
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPHachToanHuuTri
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSHachToanHuuTri
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDHachToanHuuTri
			)tblDetailHuuTri

			--- UPDATE #tempDetailDuToanHuuTri
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanHuuTri
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanHuuTri
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanHuuTri
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanHuuTri
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanHuuTri
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Du Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanHuuTri
				WHERE Type=0
			) B
			where  A.Type=2

			--- UPDATE ##tempDetailHachToanHuuTri
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanHuuTri
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanHuuTri
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanHuuTri
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanHuuTri
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanHuuTri
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Hach Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanHuuTri
				WHERE Type=0
			) B
			where  A.Type=2

			SELECT * INTO #tempHuuTri FROM
			(
			SELECT * FROM #tempHuuTriDuToan
			UNION ALL
			SELECT * FROM #tempDetailDuToanHuuTri
			UNION ALL
			SELECT * FROM #tempDetailHachToanHuuTri
			
			) tblHuutri

			--- UPDATE #tempHuuTri
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempHuuTri A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempHuuTri
				WHERE Type=0
			) B
			where  A.Type=3


	----- 9010001-010-011-0005 Tro cap phuc vien
			SELECT * INTO #tempDetailDuToanPhucVien FROM 
			(
				SELECT * FROM #tempKhoiDuToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanDuToanPhucVien
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNDuToanPhucVien
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPDuToanPhucVien
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSDuToanPhucVien
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDDuToanPhucVien
			)tblDetailPhucVien

			SELECT * INTO #tempDetailHachToanPhucVien FROM 
			(
				SELECT * FROM #tempKhoiHachToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanHachToanPhucVien
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNHachToanPhucVien
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPHachToanPhucVien
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSHachToanPhucVien
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDHachToanPhucVien
			)tblDetailPhucVien

		
			--- UPDATE ##tempDetailDuToanPhucVien
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanPhucVien
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanPhucVien
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanPhucVien
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanPhucVien
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanPhucVien
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Du Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanPhucVien
				WHERE Type=0
			) B
			where  A.Type=2

			--- UPDATE ###tempDetailHachToanPhucVien
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanPhucVien
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanPhucVien
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanPhucVien
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanPhucVien
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanPhucVien
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Hach Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanPhucVien
				WHERE Type=0
			) B
			where  A.Type=2

			SELECT * INTO #tempPhucVien FROM
			(
			SELECT * FROM #tempPhucVienDuToan
			UNION ALL
			SELECT * FROM #tempDetailDuToanPhucVien
			UNION ALL
			SELECT * FROM #tempDetailHachToanPhucVien
			) tblPhucVien

			--- UPDATE #tempPhucVien
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempPhucVien A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempPhucVien
				WHERE Type=0
			) B
			where  A.Type=3

			
	----- 9010001-010-011-0006 Tro cap xuat ngu

			SELECT * INTO #tempDetailDuToanXuatNgu FROM 
			(
				SELECT * FROM #tempKhoiDuToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanDuToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNDuToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPDuToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSDuToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDDuToanXuatNgu
			)tblDetailPXuatNgu


			SELECT * INTO #tempDetailHachToanXuatNgu FROM 
			(
				SELECT * FROM #tempKhoiHachToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanHachToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNHachToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPHachToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSHachToanXuatNgu
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDHachToanXuatNgu
			)tblDetailXuatNgu

			--- UPDATE #tempDetailDuToanXuatNgu
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanXuatNgu
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanXuatNgu
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanXuatNgu
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanXuatNgu
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanXuatNgu
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Du Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanXuatNgu
				WHERE Type=0
			) B
			where  A.Type=2


			--- UPDATE #tempDetailHachToanXuatNgu
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanXuatNgu
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanXuatNgu
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanXuatNgu
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanXuatNgu
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanXuatNgu
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- Update Hach Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanXuatNgu
				WHERE Type=0
			) B
			where  A.Type=2
			SELECT * INTO #tempXuatNgu FROM
			(
			SELECT * FROM #tempXuatNguDuToan
			UNION ALL
			SELECT * FROM #tempDetailDuToanXuatNgu
			UNION ALL
			SELECT * FROM #tempDetailHachToanXuatNgu
			) tblXuatNgu

			--- UPDATE #tempXuatNgu
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempXuatNgu A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempXuatNgu
				WHERE Type=0
			) B
			where  A.Type=3
	----- 9010001-010-011-0007 tro cap thoi viec
			SELECT * INTO #tempDetailDuToanThoiViec FROM 
			(
				SELECT * FROM #tempKhoiDuToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanDuToanThoiViec
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNDuToanThoiViec
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPDuToanThoiViec
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSDuToanThoiViec
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDDuToanThoiViec
			)tblDetailThoiViec

			SELECT * INTO #tempDetailHachToanThoiViec FROM 
			(
				SELECT * FROM #tempKhoiHachToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanHachToanThoiViec
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNHachToanThoiViec
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPHachToanThoiViec
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSHachToanThoiViec
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDHachToanThoiViec
			)tblDetailThoiViec

	 	--- UPDATE #tempDetailDuToanXuatNgu
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanThoiViec
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanThoiViec
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanThoiViec
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanThoiViec
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanThoiViec
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Du Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanThoiViec
				WHERE Type=0
			) B
			where  A.Type=2

			--- UPDATE #tempDetailHachToanThoiViec
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanThoiViec
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanThoiViec
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanThoiViec
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanThoiViec
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanThoiViec
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Hach Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanThoiViec
				WHERE Type=0
			) B
			where  A.Type=2

			SELECT * INTO #tempThoiViec FROM
			(
			SELECT * FROM #tempThoiViecDuToan
			UNION ALL
			SELECT * FROM #tempDetailDuToanThoiViec
			UNION ALL
			SELECT * FROM #tempDetailHachToanThoiViec
			) tblThoiViec
			 
			 --- UPDATE #tempThoiViec
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempThoiViec A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempThoiViec
				WHERE Type=0
			) B
			where  A.Type=3
		
	----- 9010001-010-011-0008 tro cap tu tuat
			SELECT * INTO #tempDetailDuToanTuTuat FROM 
			(
				SELECT * FROM #tempKhoiDuToan 
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanDuToanTuTuat
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNDuToanTuTuat
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPDuToanTuTuat
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSDuToanTuTuat
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDDuToanTuTuat
			)tblDetailTuTuat

			SELECT * INTO #tempDetailHachToanTuTuat FROM 
			(
				SELECT * FROM #tempKhoiHachToan
				UNION ALL
				SELECT * FROM #tempSiQuan
				UNION ALL
				SELECT  * FROM #tempDetailSiQuanHachToanTuTuat
				UNION ALL
				SELECT * FROM  #tempQNCN
				UNION ALL
				SELECT  * FROM #tempDetailQNCNHachToanTuTuat
				UNION ALL
				SELECT * FROM  #tempCNVCQP
				UNION ALL
				SELECT  * FROM #tempDetailCNVCQPHachToanTuTuat
				UNION ALL
				SELECT * FROM  #tempHSQBS
				UNION ALL
				SELECT  * FROM #tempDetailHSQBSHachToanTuTuat
				UNION ALL
				SELECT * FROM  #tempLDHD
				UNION ALL
				SELECT  * FROM #tempDetailLDHDHachToanTuTuat
			)tblDetailTuTuat

			--- UPDATE #tempDetailDuToanTuTuat
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanTuTuat
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanTuTuat
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanTuTuat
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanTuTuat
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanTuTuat
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Du Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailDuToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailDuToanTuTuat
				WHERE Type=0
			) B
			where  A.Type=2

			--- UPDATE #tempDetailHachToanTuTuat
			--- UPDATE SI QUAN IRemainRow=1
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanTuTuat
				WHERE IRemainRow=1 AND Type=0
			) B
			where A.IRemainRow=1 AND A.Type=1

			--- UPDATE QNCN IRemainRow=2
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanTuTuat
				WHERE IRemainRow=2 AND Type=0
			) B
			where A.IRemainRow=2 AND A.Type=1

		--- UPDATE CNVCQP IRemainRow=3
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanTuTuat
				WHERE IRemainRow=3 AND Type=0
			) B
			where A.IRemainRow=3 AND A.Type=1

			--- UPDATE HSQBS IRemainRow=4
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanTuTuat
				WHERE IRemainRow=4 AND Type=0
			) B
			where A.IRemainRow=4 AND A.Type=1

			--- UPDATE LDHD IRemainRow=5
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanTuTuat
				WHERE IRemainRow=5 AND Type=0
			) B
			where A.IRemainRow=5 AND A.Type=1

			--- UPDATE Khoi Hach Toan
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempDetailHachToanTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempDetailHachToanTuTuat
				WHERE Type=0
			) B
			where  A.Type=2
			SELECT * INTO #tempTuTuat FROM
			(
			SELECT * FROM #tempTuTuatDuToan
			UNION ALL
			SELECT * FROM #tempDetailDuToanTuTuat
			UNION ALL
			SELECT * FROM #tempDetailHachToanTuTuat
			) tblTuTuat

			--- Update #tempTuTuat
			UPDATE A
			SET 
				A.FTienTroCap1Lan=ISNULL(B.FTienTroCap1Lan,0),
				A.FTienTroCapKV=ISNULL(B.FTienTroCapKV,0),
				A.FTienTroCapMT=ISNULL(B.FTienTroCapMT,0),
				A.FTienTroCap1LanTL=ISNULL(B.FTienTroCap1LanTL,0),
				A.FTienTroCapKVTL=ISNULL(B.FTienTroCapKVTL,0),
				A.FTienTroCapMTTL=ISNULL(B.FTienTroCapMTTL,0)
			FROM #tempTuTuat A,
			(SELECT 
				SUM(FTienTroCap1Lan) FTienTroCap1Lan,
				SUM(FTienTroCapKV) FTienTroCapKV,
				SUM(FTienTroCapMT) FTienTroCapMT,
				SUM(FTienTroCap1LanTL) FTienTroCap1LanTL,
				SUM(FTienTroCapKVTL) FTienTroCapKVTL,
				SUM(FTienTroCapMTTL) FTienTroCapMTTL
				FROM #tempTuTuat
				WHERE Type=0
			) B
			where  A.Type=3

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


		 DROP TABLE #tempRESULT
		 DROP TABLE #tblTroCapKhoiDuToan 
		 DROP TABLE #tblTroCapKhoiHachToan
		 DROP TABLE #tempHuuTri
		 DROP TABLE #tempDetailDuToanHuuTri
		 DROP TABLE #tempDetailHachToanHuuTri


		 DROP TABLE #tempPhucVien
		 DROP TABLE #tempDetailDuToanPhucVien
		 DROP TABLE #tempDetailHachToanPhucVien

		 DROP TABLE #tempXuatNgu 
		 DROP TABLE #tempDetailDuToanXuatNgu
		 DROP TABLE #tempDetailHachToanXuatNgu

		 DROP TABLE #tempThoiViec
		 DROP TABLE #tempDetailDuToanThoiViec
		 DROP TABLE #tempDetailHachToanThoiViec

		 DROP TABLE #tempTuTuat
		 DROP TABLE #tempDetailDuToanTuTuat
		 DROP TABLE #tempDetailHachToanTuTuat

		 DROP TABLE #tempKhoiDuToan
		 DROP TABLE #tempKhoiHachToan

		 DROP TABLE #tempHuuTriDuToan
		 DROP TABLE #tempPhucVienDuToan
		 DROP TABLE #tempXuatNguDuToan
		 DROP TABLE #tempThoiViecDuToan
		 DROP TABLE #tempTuTuatDuToan

		 DROP TABLE #tempSiQuan
		 DROP TABLE #tempQNCN
		 DROP TABLE #tempCNVCQP
		 DROP TABLE #tempHSQBS
		 DROP TABLE #tempLDHD

		 DROP TABLE #tempDetailSiQuanDuToanHuuTri
		 DROP TABLE #tempDetailQNCNDuToanHuuTri
		 DROP TABLE #tempDetailCNVCQPDuToanHuuTri
		 DROP TABLE #tempDetailHSQBSDuToanHuuTri
		 DROP TABLE #tempDetailLDHDDuToanHuuTri

		 DROP TABLE #tempDetailSiQuanDuToanPhucVien
		 DROP TABLE #tempDetailQNCNDuToanPhucVien
		 DROP TABLE #tempDetailCNVCQPDuToanPhucVien
		 DROP TABLE #tempDetailHSQBSDuToanPhucVien
		 DROP TABLE #tempDetailLDHDDuToanPhucVien
		 
		 DROP TABLE #tempDetailSiQuanDuToanXuatNgu
		 DROP TABLE #tempDetailQNCNDuToanXuatNgu
		 DROP TABLE #tempDetailCNVCQPDuToanXuatNgu
		 DROP TABLE #tempDetailHSQBSDuToanXuatNgu
		 DROP TABLE #tempDetailLDHDDuToanXuatNgu

		 DROP TABLE #tempDetailSiQuanDuToanThoiViec
		 DROP TABLE #tempDetailQNCNDuToanThoiViec
		 DROP TABLE #tempDetailCNVCQPDuToanThoiViec
		 DROP TABLE #tempDetailHSQBSDuToanThoiViec
		 DROP TABLE #tempDetailLDHDDuToanThoiViec

		 DROP TABLE #tempDetailSiQuanDuToanTuTuat
		 DROP TABLE #tempDetailQNCNDuToanTuTuat
		 DROP TABLE #tempDetailCNVCQPDuToanTuTuat
		 DROP TABLE #tempDetailHSQBSDuToanTuTuat
		 DROP TABLE #tempDetailLDHDDuToanTuTuat

		 DROP TABLE #tempDetailSiQuanHachToanHuuTri
		 DROP TABLE #tempDetailQNCNHachToanHuuTri
		 DROP TABLE #tempDetailCNVCQPHachToanHuuTri
		 DROP TABLE #tempDetailHSQBSHachToanHuuTri
		 DROP TABLE #tempDetailLDHDHachToanHuuTri

		 DROP TABLE #tempDetailSiQuanHachToanPhucVien
		 DROP TABLE #tempDetailQNCNHachToanPhucVien
		 DROP TABLE #tempDetailCNVCQPHachToanPhucVien
		 DROP TABLE #tempDetailHSQBSHachToanPhucVien
		 DROP TABLE #tempDetailLDHDHachToanPhucVien
		 
		 DROP TABLE #tempDetailSiQuanHachToanXuatNgu
		 DROP TABLE #tempDetailQNCNHachToanXuatNgu
		 DROP TABLE #tempDetailCNVCQPHachToanXuatNgu
		 DROP TABLE #tempDetailHSQBSHachToanXuatNgu
		 DROP TABLE #tempDetailLDHDHachToanXuatNgu

		 DROP TABLE #tempDetailSiQuanHachToanThoiViec
		 DROP TABLE #tempDetailQNCNHachToanThoiViec
		 DROP TABLE #tempDetailCNVCQPHachToanThoiViec
		 DROP TABLE #tempDetailHSQBSHachToanThoiViec
		 DROP TABLE #tempDetailLDHDHachToanThoiViec

		 DROP TABLE #tempDetailSiQuanHachToanTuTuat
		 DROP TABLE #tempDetailQNCNHachToanTuTuat
		 DROP TABLE #tempDetailCNVCQPHachToanTuTuat
		 DROP TABLE #tempDetailHSQBSHachToanTuTuat
		 DROP TABLE #tempDetailLDHDHachToanTuTuat


		 DROP TABLE #tempHuuTriHachToan
		 DROP TABLE #tempPhucVienHachToan
		 DROP TABLE #tempXuatNguHachToan
		 Drop table #tempThoiViecHachToan
		 Drop table #tempTuTuatHachToan

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
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_gttrocap_huutri_phucvien_xuatngu_thoiviec_tutuat]    Script Date: 7/ @Donvitinh7/2024 5:36:05 PM ******/
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
			or gt.sXauNoiMa like '9010002-010-011-0004%'
			or gt.sXauNoiMa like '9010002-010-011-0005%'
			or gt.sXauNoiMa like '9010002-010-011-0006%'
			or gt.sXauNoiMa like '9010002-010-011-0007%'
			or gt.sXauNoiMa like '9010002-010-011-0008%')
		and gt.iNamLamViec = @INamLamViec and gt.iQuy = @IQuy and gt.iiD_MaDonVi in (SELECT * FROM f_split(@IdMaDonVi))

	--- 9010001-010-011-0004 Tro cap Huu tri

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

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			into #TempDetailHuuTri
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC
	SELECT * INTO #tempHuuTri FROM
	(
	SELECT 1 bHangCha
			, N'(I)' STT
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
	SELECT * FROM #TempDetailHuuTri
	) tblHuutri

	 if (SELECT COUNT(1) FROM #tempHuuTri) > 2
		UPDATE #tempHuuTri set bHasData = 1

	
		UPDATE  A
		SET A.FTienTroCap1Lan=isnull(detail.FTienTroCap1Lan,0),
		A.FTienTroCapKV=isnull(detail.FTienTroCapKV,0),
		A.FTienTroCapMT=isnull(detail.FTienTroCapMT,0),
		A.FTienTroCap1LanTL=isnull(detail.FTienTroCap1LanTL,0),
		A.FTienTroCapKVTL=isnull(detail.FTienTroCapKVTL,0),
		A.FTienTroCapMTTL=isnull(detail.FTienTroCapMTTL,0)
		FROM #tempHuuTri A ,(
		SELECT 
		SUM(FTienTroCap1Lan) AS FTienTroCap1Lan,
		SUM(FTienTroCapKV) AS FTienTroCapKV,
		SUM(FTienTroCapMT) AS FTienTroCapMT,
		SUM(FTienTroCap1LanTL) AS FTienTroCap1LanTL,
		SUM(FTienTroCapKVTL) AS FTienTroCapKVTL,
		SUM(FTienTroCapMTTL) AS FTienTroCapMTTL
		FROM #tempHuuTri WHERE  bHangCha=0 ) detail
		where A.bHangCha=1


	--- 9010001-010-011-0005 Tro cap phuc vien
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

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			INTO #tempDetailPhucVien
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac	
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC
	SELECT * INTO #tempPhucVien FROM
	(
	SELECT 1 bHangCha
			, N'(II)' STT
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
	SELECT * FROM #tempDetailPhucVien
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
		FROM #tempPhucVien WHERE  bHangCha=0 ) detail
		where #tempPhucVien.bHangCha=1
	--- 9010001-010-011-0006 Tro cap xuat ngu

		SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sSoQuyetDinh) AS VARCHAR(6)) STT
			, CASE WHEN  tbltc.sSoQuyetDinh is null or  tbltc.sSoQuyetDinh='' then  Convert(varchar(10),(COUNT(tbltc.sSoQuyetDinh))) else   Convert(varchar(10),(COUNT(tbltc.sTenPhanHo))) end  + N' Đồng chí' as sTenCanBo
			--, Convert(varchar(10),(COUNT(tbltc.sSoQuyetDinh))) + N' Đồng chí' as sTenCanBo
			, tbltc.sTenPhanHo
			, '' as SMaCapBac		
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			--, 0 AS  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS  FTienTroCapMT
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			 INTO #tempDetailXuatNgu
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-0006%'
	GROUP BY 
			 tbltc.sTenPhanHo
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
--ORDER BY  tbltc.sTenCanBo desc,tbltc.sMaCapBac desc
	SELECT * INTO #tempXuatNgu FROM
	(
	SELECT 1 bHangCha
			, N'(III)' STT
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
	SELECT * FROM #tempDetailXuatNgu
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
		FROM #tempXuatNgu WHERE  bHangCha=0 ) detail
		where #tempXuatNgu.bHangCha=1
	--- 9010001-010-011-0007 tro cap thoi viec

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

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			into #tempDetailThoiViec
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC
	SELECT * INTO #tempThoiViec FROM
	(
	SELECT 1 bHangCha
			, N'(IV)' STT
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
	SELECT * FROM #tempDetailThoiViec
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
		FROM #tempThoiViec WHERE  bHangCha=0 ) detail
		where #tempThoiViec.bHangCha=1
	--- 9010001-010-011-0008 tro cap tu tuat

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

			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCap1LanTL
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  FTienTroCapKVTL
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fTienTruyLinh) / @Donvitinh ELSE 0 END)  AS FTienTroCapMTTL
			, 0 bHasData
			into #TempDetailTuTuat
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa 
	ORDER BY tbltc.sMaCapBac desc,tbltc.sTenCanBo ASC
	SELECT * INTO #tempTuTuat FROM
	(
	SELECT 1 bHangCha
			, N'(V)' STT
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
	SELECT * FROM #TempDetailTuTuat
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
		FROM #tempTuTuat WHERE  bHangCha=0 ) detail
				where #tempTuTuat.bHangCha=1
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
