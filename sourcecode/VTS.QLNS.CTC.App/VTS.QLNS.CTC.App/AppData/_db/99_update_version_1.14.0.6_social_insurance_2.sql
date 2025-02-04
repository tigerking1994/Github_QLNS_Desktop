/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkp_ql_chungtu_chi_tiet]    Script Date: 3/1/2024 4:58:30 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qtc_qkp_ql_chungtu_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qtc_qkp_ql_chungtu_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_quykinhphi_quanly_chungtu_chi_tiet]    Script Date: 3/1/2024 4:58:30 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_quykinhphi_quanly_chungtu_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_quykinhphi_quanly_chungtu_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chiquyKCB_chitiet_clone]    Script Date: 3/1/2024 4:58:30 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_chiquyKCB_chitiet_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_chiquyKCB_chitiet_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]    Script Date: 3/1/2024 4:58:30 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_kcb]    Script Date: 3/1/2024 4:58:30 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_kcb]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_kcb]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_bhxh]    Script Date: 3/1/2024 4:58:30 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_bhxh]    Script Date: 3/1/2024 4:58:30 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_getDataQuyetToanGiaiThich]    Script Date: 3/1/2024 4:58:30 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_getDataQuyetToanGiaiThich]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_getDataQuyetToanGiaiThich]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chi_tiet_cssk_hssv_nld]    Script Date: 3/1/2024 5:17:30 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_chungtu_chi_tiet_cssk_hssv_nld]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_chungtu_chi_tiet_cssk_hssv_nld]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chi_tiet_chedo_bhxh]    Script Date: 3/1/2024 5:17:30 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_chungtu_chi_tiet_chedo_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_chungtu_chi_tiet_chedo_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chi_tiet]    Script Date: 3/1/2024 5:17:30 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_chungtu_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_chungtu_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_getDataQuyetToanGiaiThich]    Script Date: 3/1/2024 4:58:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<DungNV>
-- Create date: <01/02/2024>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_bh_getDataQuyetToanGiaiThich] 
	-- Add the parameters for the stored procedure here
	@IdChungTu uniqueidentifier, 
	@XauNoiMa nvarchar(100),
	@NamLamViec int
AS
BEGIN
	declare @IType int = (SELECT TOP(1) iDonViTinh FROM BH_DM_MucLucNganSach WHERE iNamLamViec = @NamLamViec AND sXauNoiMa = @XauNoiMa);--1:Ngay, 2:Thang, 3: nguoi
	DECLARE @DonViTinh int ;
	IF(@IType = 3) -- tinh so nguoi
		BEGIN
			select 
		               COUNT(CASE WHEN sMaCapBac LIKE '1%' then 1 ELSE NULL END) as ISoSQDeNghi,
		               SUM(CASE WHEN sMaCapBac LIKE '1%' then fSoTien ELSE 0 END) as FTienSQDeNghi,
		               COUNT(CASE WHEN sMaCapBac LIKE '2%' then 1 ELSE NULL END) as ISoQNCNDeNghi,
		               SUM(CASE WHEN sMaCapBac LIKE '2%' then fSoTien ELSE 0 END) as FTienQNCNDeNghi,
		               COUNT(CASE WHEN sMaCapBac LIKE '3.1%' OR sMaCapBac LIKE '3.2%' OR sMaCapBac LIKE '3.3%'  OR sMaCapBac = '413' OR sMaCapBac = '415' then 1 ELSE    NULL     END)    as       ISoCNVCQPDeNghi,
		               SUM(CASE WHEN sMaCapBac LIKE '3.1%' OR sMaCapBac LIKE '3.2%' OR sMaCapBac LIKE '3.3%' OR sMaCapBac = '413' OR sMaCapBac = '415' then fSoTien    ELSE 0   END)   as     FTienCNVCQPDeNghi,		
		               COUNT(CASE WHEN sMaCapBac LIKE '0%' then 1 ELSE NULL END) as ISoHSQBSDeNghi,
		               SUM(CASE WHEN sMaCapBac LIKE '0%' then fSoTien ELSE 0 END) as FTienHSQBSDeNghi,		
		               COUNT(CASE WHEN sMaCapBac = '423' OR sMaCapBac = '425' OR sMaCapBac = '43' then 1 ELSE NULL END) as ISoLDHDDeNghi,
		               SUM(CASE WHEN sMaCapBac = '423' OR sMaCapBac = '425' OR sMaCapBac = '43' then fSoTien ELSE 0 END) as FTienLDHDDeNghi,
		               gttc.sXauNoiMa as SXauNoiMa
	               FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gttc
				   LEFT JOIN BH_DM_MucLucNganSach mucluc ON gttc.sXauNoiMa = mucluc.sXauNoiMa and  mucluc.iNamLamViec= @NamLamViec
	               WHERE 
	               		 gttc.iID_QTC_Quy_ChungTu=@IdChungTu
	               		 and gttc.sXauNoiMa = @XauNoiMa
	               GROUP BY gttc.sXauNoiMa;
		END
	ELSE
		BEGIN
		
			select 
		               CASE
							WHEN @IType = 2 THEN SUM(CASE WHEN sMaCapBac LIKE '1%' then iSoNgayHuong ELSE 0 END)/30
							ELSE SUM(CASE WHEN sMaCapBac LIKE '1%' then iSoNgayHuong ELSE 0 END)
					   END
					   AS ISoSQDeNghi,
		               SUM(CASE WHEN sMaCapBac LIKE '1%' then fSoTien ELSE 0 END) as FTienSQDeNghi,
		               CASE
							WHEN @IType = 2 THEN SUM(CASE WHEN sMaCapBac LIKE '2%' then iSoNgayHuong ELSE 0 END)/30
							ELSE SUM(CASE WHEN sMaCapBac LIKE '2%' then iSoNgayHuong ELSE 0 END)
					   END
					   AS ISoQNCNDeNghi,
		               SUM(CASE WHEN sMaCapBac LIKE '2%' then fSoTien ELSE 0 END) as FTienQNCNDeNghi,
		               CASE
							WHEN @IType = 2 THEN SUM(CASE WHEN sMaCapBac LIKE '3.1%' OR sMaCapBac LIKE '3.2%' OR sMaCapBac LIKE '3.3%' OR sMaCapBac = '413' OR sMaCapBac = '415' then iSoNgayHuong ELSE 0 END)/30
							ELSE SUM(CASE WHEN sMaCapBac LIKE '3.1%' OR sMaCapBac LIKE '3.2%' OR sMaCapBac LIKE '3.3%' OR sMaCapBac = '413' OR sMaCapBac = '415' then iSoNgayHuong ELSE 0 END)
					   END
					   AS ISoCNVCQPDeNghi,
		               SUM(CASE WHEN sMaCapBac LIKE '3.1%' OR sMaCapBac LIKE '3.2%' OR sMaCapBac LIKE '3.3%' OR sMaCapBac = '413' OR sMaCapBac = '415' then fSoTien    ELSE 0   END)   as     FTienCNVCQPDeNghi,	
		               CASE
							WHEN @IType = 2 THEN SUM(CASE WHEN SMaCapBac LIKE '0%' then iSoNgayHuong ELSE 0 END)/30
							ELSE SUM(CASE WHEN SMaCapBac LIKE '0%' then iSoNgayHuong ELSE 0 END)
					   END
					   AS ISoHSQBSDeNghi,					   
		               SUM(CASE WHEN sMaCapBac  LIKE '0%' then fSoTien ELSE 0 END) as FTienHSQBSDeNghi,		
		               CASE
							WHEN @IType = 2 THEN SUM(CASE WHEN sMaCapBac = '423' OR sMaCapBac = '425' OR sMaCapBac = '43' then iSoNgayHuong ELSE 0 END)/30
							ELSE SUM(CASE WHEN sMaCapBac = '423' OR sMaCapBac = '425' OR sMaCapBac = '43' then iSoNgayHuong ELSE 0 END)
					   END
					   AS ISoLDHDDeNghi,
		               SUM(CASE WHEN sMaCapBac = '423' OR sMaCapBac = '425' OR sMaCapBac = '43' then fSoTien ELSE 0 END) as FTienLDHDDeNghi,
		               gttc.sXauNoiMa as SXauNoiMa
	               FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gttc
					LEFT JOIN BH_DM_MucLucNganSach mucluc ON gttc.sXauNoiMa = mucluc.sXauNoiMa and  mucluc.iNamLamViec = @NamLamViec
	               WHERE 
	               		 gttc.iID_QTC_Quy_ChungTu=@IdChungTu
	               		 and gttc.sXauNoiMa = @XauNoiMa
	               GROUP BY gttc.sXauNoiMa;		
				   END

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_bhxh]    Script Date: 3/1/2024 4:58:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_bhxh]
@INamLamViec int,
@IdMaDonVi nvarchar(max),
@Lns nvarchar(max),
@Donvitinh int,
@IsTongHop int
as
begin
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
			danhmuc.sDuToanChiTietToi
		into #tblMucLucNganSach
		from BH_DM_MucLucNganSach as danhmuc
		where danhmuc.iNamLamViec = @INamLamViec and danhmuc.sLNs in (select * from dbo.splitstring(@Lns))


	---Lấy danh sách chi tiết
		select	
			qtcn_ct.iID_MucLucNganSach,
			qtcn_ct.sLoaiTroCap,
			Sum(qtcn_ct.fTienDuToanDuyet) as fTienDuToanDuyet,
			Sum(qtcn_ct.iSoDuToanDuocDuyet) as iSoDuToanDuocDuyet,
			Sum(qtcn_ct.fTongTien_ThucChi) as fTongTien_ThucChi,
			Sum(qtcn_ct.iTongSo_ThucChi) as iTongSo_ThucChi,
			Sum(qtcn_ct.iSoSQ_ThucChi) as iSoSQ_ThucChi,
			Sum(qtcn_ct.fTienSQ_ThucChi) as fTienSQ_ThucChi,
			Sum(qtcn_ct.iSoQNCN_ThucChi) as iSoQNCN_ThucChi,
			Sum(qtcn_ct.fTienQNCN_ThucChi) as fTienQNCN_ThucChi,
			Sum(qtcn_ct.iSoCNVCQP_ThucChi) as iSoCNVCQP_ThucChi,
			Sum(qtcn_ct.fTienCNVCQP_ThucChi) as fTienCNVCQP_ThucChi,
			Sum(qtcn_ct.iSoLDHD_ThucChi) as iSoLDHD_ThucChi,
			Sum(qtcn_ct.fTienLDHD_ThucChi) as fTienLDHD_ThucChi,
			Sum(qtcn_ct.iSoHSQBS_ThucChi) as iSoHSQBS_ThucChi,
			Sum(qtcn_ct.fTienHSQBS_ThucChi) as fTienHSQBS_ThucChi,
			Sum(qtcn_ct.fTienDuToanDuyet) - Sum(qtcn_ct.fTongTien_ThucChi) as fTienThua,
			Sum(qtcn_ct.fTongTien_ThucChi) - Sum(qtcn_ct.fTienDuToanDuyet) as fTienThieu
			
		into #tbl_qtcn_chitiet
		from BH_QTC_Nam_CheDoBHXH_ChiTiet as  qtcn_ct
		inner join  BH_QTC_Nam_CheDoBHXH as qtcn on qtcn_ct.iID_QTC_Nam_CheDoBHXH = qtcn.ID_QTC_Nam_CheDoBHXH
		where qtcn.iID_MaDonVi in (select * from dbo.splitstring(@IdMaDonVi)) 
		and qtcn.iNamLamViec = @INamLamViec
		--and ((@IsTongHop = 0 and qtcn.bDaTongHop = 0 and qtcn.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  qtcn.sDSSoChungTuTongHop is not null))
		group by qtcn_ct.iID_MucLucNganSach,qtcn_ct.sLoaiTroCap



		---Kết quả hiển thị trả về
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
			mucluc.sMoTa as sLoaiTroCap,
			chi_tiet.fTienDuToanDuyet, 
			chi_tiet.fTongTien_ThucChi,
			chi_tiet.iTongSo_ThucChi,
			chi_tiet.iSoSQ_ThucChi,
			chi_tiet.fTienSQ_ThucChi,
			chi_tiet.iSoQNCN_ThucChi,
			chi_tiet.fTienQNCN_ThucChi,
			chi_tiet.iSoCNVCQP_ThucChi,
			chi_tiet.fTienCNVCQP_ThucChi,
			chi_tiet.iSoLDHD_ThucChi,
			chi_tiet.fTienLDHD_ThucChi,
			chi_tiet.iSoHSQBS_ThucChi,
			chi_tiet.fTienHSQBS_ThucChi
		from #tblMucLucNganSach as mucluc
		left join #tbl_qtcn_chitiet as chi_tiet on mucluc.iID_MLNS = chi_tiet.iID_MucLucNganSach
		order by mucluc.sXauNoiMa


		drop table #tblMucLucNganSach;
		drop table #tbl_qtcn_chitiet;

end
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_bhxh]    Script Date: 3/1/2024 4:58:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_bhxh]
@INamLamViec int,
@IdMaDonVi nvarchar(max),
@SLN nvarchar(max),
@IsTongHop bit,
@IQuy int,
@Donvitinh int
as
begin
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
			danhmuc.sDuToanChiTietToi,
			danhmuc.bHangCha
		into #tblMucLucNganSach
		from BH_DM_MucLucNganSach as danhmuc
		where danhmuc.iNamLamViec = @INamLamViec and danhmuc.sLNs  in (select * from dbo.splitstring(@SLN)) 
		
		
		
	---Lấy thông tin chi tiết chứng từ
		select 
			qtcn_ct.iID_MucLucNganSach,
			qtcn_ct.sXauNoiMa,
			Sum(qtcn_ct.fTienDuToanDuyet) as fTienDuToanDuyet,
			Sum(qtcn_ct.iSoLuyKeCuoiQuyNay) as iSoLuyKeCuoiQuyNay,
			Sum(qtcn_ct.fTienLuyKeCuoiQuyNay) as fTienLuyKeCuoiQuyNay,
			sum(qtcn_ct.iSoSQ_DeNghi) as iSoSQ_DeNghi ,
			sum(qtcn_ct.fTienSQ_DeNghi) as fTienSQ_DeNghi ,
			sum(qtcn_ct.iSoQNCN_DeNghi) as iSoQNCN_DeNghi,
			sum(qtcn_ct.fTienQNCN_DeNghi) as fTienQNCN_DeNghi ,
			sum(qtcn_ct.iSoCNVCQP_DeNghi) as iSoCNVCQP_DeNghi ,
			sum(qtcn_ct.fTienCNVCQP_DeNghi) as fTienCNVCQP_DeNghi,
			sum(qtcn_ct.iSoHSQBS_DeNghi) as iSoHSQBS_DeNghi,
			sum(qtcn_ct.fTienHSQBS_DeNghi) as fTienHSQBS_DeNghi,
			sum(qtcn_ct.iTongSo_DeNghi) as iTongSo_DeNghi,
			sum(qtcn_ct.fTongTien_DeNghi) as fTongTien_DeNghi,
			sum(qtcn_ct.fTongTien_PheDuyet) as fTongTien_PheDuyet,
			sum(qtcn_ct.iSoLDHD_DeNghi) as iSoLDHD_DeNghi,
			sum(qtcn_ct.fTienLDHD_DeNghi) as fTienLDHD_DeNghi
		into #tblQuyetToanQuyChiTiet
		from BH_QTC_Quy_CheDoBHXH_ChiTiet as qtcn_ct
		inner join BH_QTC_Quy_CheDoBHXH  as qtcn on qtcn_ct.iID_QTC_Quy_CheDoBHXH = qtcn.ID_QTC_Quy_CheDoBHXH
		where qtcn.iID_MaDonVi in (select * from dbo.splitstring(@IdMaDonVi)) 
		and qtcn.iNamChungTu = @INamLamViec
		--and ((@IsTongHop = 0 and qtcn.bDaTongHop = 0 and qtcn.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  qtcn.sDSSoChungTuTongHop is not null))
		and qtcn.iQuyChungTu = @IQuy
		group by qtcn_ct.iID_MucLucNganSach,qtcn_ct.sXauNoiMa

		--- Get tien du toan 
		SELECT ctct.sXauNoiMa,
			SUM(ctct.fTienTuChi)  fTienDuToanDuyet
			into #tempDuToan
			FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct 
			JOIN BH_DTC_PhanBoDuToanChi ct ON ctct.iID_DTC_PhanBoDuToanChi = ct.ID 
				WHERE ctct.iID_MaDonVi in (select * from dbo.splitstring(@IdMaDonVi)) 
				AND BIsKhoa = 1
				AND ct.iNamChungTu = @INamLamViec
				GROUP BY ctct.sXauNoiMa
	--- lay luy kê quy truoc
	SELECT SUM
			(isnull(fTienCNVCQP_DeNghi, 0)) fTienCNVCQP_DeNghi,
			SUM (isnull(fTienHSQBS_DeNghi, 0)) fTienHSQBS_DeNghi,
			SUM (isnull(fTienLDHD_DeNghi, 0)) fTienLDHD_DeNghi,
			SUM (isnull(fTienQNCN_DeNghi, 0)) fTienQNCN_DeNghi,
			SUM (isnull(fTienSQ_DeNghi, 0)) fTienSQ_DeNghi,
			SUM (isnull(iSoCNVCQP_DeNghi, 0)) iSoCNVCQP_DeNghi,
			SUM (isnull(ctct.fTongTien_PheDuyet, 0)) fTongTien_PheDuyet,
			SUM (isnull(iSoHSQBS_DeNghi, 0)) iSoHSQBS_DeNghi,
			SUM (isnull(iSoLDHD_DeNghi, 0)) iSoLDHD_DeNghi,
			SUM (isnull(iSoQNCN_DeNghi, 0)) iSoQNCN_DeNghi,
			SUM (isnull(iSoSQ_DeNghi, 0)) iSoSQ_DeNghi,
			ct.iID_MaDonVi,
			ct.iNamChungTu,
			ctct.sXauNoiMa
			into #tempDuLieuQuyTruoc
		FROM
			BH_QTC_Quy_CheDoBHXH_ChiTiet ctct
			INNER JOIN BH_QTC_Quy_CheDoBHXH ct ON ctct.iID_QTC_Quy_CheDoBHXH = ct.ID_QTC_Quy_CheDoBHXH 
		WHERE
			ct.iQuyChungTu < @IQuy 
			and ct.iID_MaDonVi in (select * from dbo.splitstring(@IdMaDonVi)) 
			and ct.iNamChungTu=@INamLamViec
		GROUP BY
			ct.iID_MaDonVi,
			ct.iNamChungTu,
			ctct.sXauNoiMa

	---Kết quả hiển thị trả về
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
			mucluc.sDuToanChiTietToi,
			mucluc.bHangCha,
			mucluc.iID_MLNS as iID_MucLucNganSach,
			mucluc.sMoTa as sLoaiTroCap,
			duToan.fTienDuToanDuyet,
			(
			isnull(duLieuQuyTruoc.fTienCNVCQP_DeNghi, 0) + isnull(duLieuQuyTruoc.fTienHSQBS_DeNghi, 0) + isnull(duLieuQuyTruoc.fTienLDHD_DeNghi, 0) + isnull(duLieuQuyTruoc.fTienQNCN_DeNghi, 0) + isnull(duLieuQuyTruoc.fTienSQ_DeNghi, 0) 
			) fTienLuyKeCuoiQuyTruoc,
			(
				isnull(duLieuQuyTruoc.iSoCNVCQP_DeNghi, 0) + isnull(duLieuQuyTruoc.iSoHSQBS_DeNghi, 0) + isnull(duLieuQuyTruoc.iSoLDHD_DeNghi, 0) + isnull(duLieuQuyTruoc.iSoQNCN_DeNghi, 0) + isnull(duLieuQuyTruoc.iSoSQ_DeNghi, 0) 
			) iSoLuyKeCuoiQuyTruoc,
			chi_tiet.iSoSQ_DeNghi,
			chi_tiet.fTienSQ_DeNghi,
			chi_tiet.iSoQNCN_DeNghi,
			chi_tiet.fTienQNCN_DeNghi,
			chi_tiet.iSoCNVCQP_DeNghi,
			chi_tiet.fTienCNVCQP_DeNghi,
			chi_tiet.iSoHSQBS_DeNghi,
			chi_tiet.fTienHSQBS_DeNghi,
			chi_tiet.iTongSo_DeNghi,
			chi_tiet.fTongTien_DeNghi,
			chi_tiet.fTongTien_PheDuyet,
			chi_tiet.iSoLDHD_DeNghi,
			chi_tiet.fTienLDHD_DeNghi

		from #tblMucLucNganSach as mucluc
		left join #tblQuyetToanQuyChiTiet as chi_tiet on mucluc.sXauNoiMa = chi_tiet.sXauNoiMa
		left join #tempDuToan duToan on mucluc.sXauNoiMa=duToan.sXauNoiMa
		left join #tempDuLieuQuyTruoc duLieuQuyTruoc on chi_tiet.sXauNoiMa=duLieuQuyTruoc.sXauNoiMa
		order by mucluc.sXauNoiMa
	
	drop table #tblMucLucNganSach;
	drop table #tblQuyetToanQuyChiTiet;
	drop table #tempDuToan;
	drop table #tempDuLieuQuyTruoc;

end
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_kcb]    Script Date: 3/1/2024 4:58:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_kcb]
@INamLamViec int,
@IdMaDonVi nvarchar(max),
@SLN nvarchar(max),
@IsTongHop bit,
@IQuy int,
@Donvitinh int
as
begin
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
			danhmuc.bHangCha
		into #tblMucLucNganSach
		from BH_DM_MucLucNganSach as danhmuc
		where danhmuc.iNamLamViec = @INamLamViec and danhmuc.sLNs  in (select * from dbo.splitstring(@SLN)) 
		
		
		
	---Lấy thông tin chi tiết chứng từ
		select 
			qtcn_ct.iID_MucLucNganSach,
			qtcn_ct.sNoiDung,
			Sum(qtcn_ct.fTien_DuToanNamTruocChuyenSang) as FTienDuToanNamTruocChuyenSang,
			Sum(qtcn_ct.fTien_DuToanGiaoNamNay) as FTienDuToanGiaoNamNay,
			Sum(qtcn_ct.fTien_TongDuToanDuocGiao) as FTienTongDuToanDuocGiao,
			sum(qtcn_ct.fTienThucChi) as FTienThucChi ,
			sum(qtcn_ct.fTienQuyetToanDaDuyet) as FTienQuyetToanDaDuyet ,
			sum(qtcn_ct.fTienDeNghiQuyetToanQuyNay) as FTienDeNghiQuyetToanQuyNay,
			sum(qtcn_ct.fTienXacNhanQuyetToanQuyNay) as FTienXacNhanQuyetToanQuyNay
		into #tblQuyetToanQuyChiTiet
		from BH_QTC_Quy_KCB_ChiTiet as qtcn_ct
		inner join BH_QTC_Quy_KCB  as qtcn on qtcn_ct.iID_QTC_Quy_KCB = qtcn.ID_QTC_Quy_KCB
		where qtcn.iID_MaDonVi in (select * from dbo.splitstring(@IdMaDonVi)) 
		and qtcn.iNamChungTu = @INamLamViec
		--and ((@IsTongHop = 0 and qtcn.bDaTongHop = 0 and qtcn.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  qtcn.sDSSoChungTuTongHop is not null))
		and qtcn.iQuyChungTu = @IQuy
		group by qtcn_ct.iID_MucLucNganSach,qtcn_ct.sNoiDung

   		-- lấy ra dữ liệu dự toán
	SELECT 
		  SUM(fTienTuChi) AS FTienDuToanGiaoNamNay,
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
          AND iNamChungTu = @INamLamViec
		  AND bIsKhoa=1
          --AND cast(dNgayQuyetDinh AS date) <= cast(@DNgayChungTu AS date)) 
		  )
     AND iID_MaDonVi in  (SELECT * FROM f_split(@IdMaDonVi))-- donvi
   GROUP BY iID_MaDonVi, 
   iID_MucLucNganSach,
   sXauNoiMa
   --- Get nhan phan bo tren giao
   	SELECT 
		  SUM(fTienTuChi) AS FTienDuToanGiaoNamNay,
          iID_MaDonVi,
          iID_MucLucNganSach,
		  sXauNoiMa
		  into #tblNhanPhanBoTrenGiao
   FROM BH_DTC_DuToanChiTrenGiao_ChiTiet
   WHERE iID_DTC_DuToanChiTrenGiao IN
       (SELECT ID
        FROM BH_DTC_DuToanChiTrenGiao
        WHERE sSoQuyetDinh <> ''
          AND sSoQuyetDinh IS NOT NULL
          AND iNamLamViec = @INamLamViec
		  AND bIsKhoa=1
          --AND cast(dNgayQuyetDinh AS date) <= cast(@DNgayChungTu AS date)) 
		  )
     AND iID_MaDonVi in  (SELECT * FROM f_split(@IdMaDonVi))-- donvi
   GROUP BY iID_MaDonVi, 
   iID_MucLucNganSach,
   sXauNoiMa

   SELECT CTCT.* 
		INTO #TempChungTuQuyTruoc
	FROM BH_QTC_QUY_KCB_Chitiet CTCT
		WHERE CTCT.iID_QTC_Quy_KCB IN
	(
		SELECT ID_QTC_Quy_KCB FROM  BH_QTC_QUY_KCB 
		WHERE iID_MaDonVi IN ( SELECT * FROM  f_split(@IdMaDonVi))
					AND iNamChungTu=@INamLamViec
					AND iQuyChungTu=1
	)

   -- chung tu thuong
		if @IsTongHop=1
	---Kết quả hiển thị trả về
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
			mucluc.sMoTa as sNoiDung,
			quy_truoc.fTien_DuToanNamTruocChuyenSang as FTienDuToanNamTruocChuyenSang,
			dt.FTienDuToanGiaoNamNay,
			chi_tiet.FTienTongDuToanDuocGiao,
			chi_tiet.fTienThucChi,
			chi_tiet.fTienQuyetToanDaDuyet,
			chi_tiet.fTienDeNghiQuyetToanQuyNay,
			chi_tiet.fTienXacNhanQuyetToanQuyNay
		from #tblMucLucNganSach as mucluc
		left join #tblQuyetToanQuyChiTiet as chi_tiet on mucluc.iID_MLNS = chi_tiet.iID_MucLucNganSach
		left join #tblPhanBoDuToan as dt on mucluc.sXauNoiMa=dt.sXauNoiMa
		left join #TempChungTuQuyTruoc as quy_truoc on mucluc.sXauNoiMa=quy_truoc.sXauNoiMa
		order by mucluc.sXauNoiMa
	else
		---- chung tu tong hop
		---Kết quả hiển thị trả về
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
			mucluc.sMoTa as sNoiDung,
			quy_truoc.fTien_DuToanNamTruocChuyenSang as FTienDuToanNamTruocChuyenSang,
			dt.FTienDuToanGiaoNamNay,
			chi_tiet.FTienTongDuToanDuocGiao,
			chi_tiet.fTienThucChi,
			chi_tiet.fTienQuyetToanDaDuyet,
			chi_tiet.fTienDeNghiQuyetToanQuyNay,
			chi_tiet.fTienXacNhanQuyetToanQuyNay
		from #tblMucLucNganSach as mucluc
		left join #tblQuyetToanQuyChiTiet as chi_tiet on mucluc.iID_MLNS = chi_tiet.iID_MucLucNganSach
		left join #tblNhanPhanBoTrenGiao as dt on mucluc.sXauNoiMa=dt.sXauNoiMa
		left join #TempChungTuQuyTruoc as quy_truoc on mucluc.sXauNoiMa=quy_truoc.sXauNoiMa
		order by mucluc.sXauNoiMa
end


GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]    Script Date: 3/1/2024 4:58:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet] @IdChungTu uniqueidentifier,
	@SLNS nvarchar (MAX),
	@INamLamViec INT,
	@MaDonVi nvarchar (100),
	@Loai BIT
	AS BEGIN
	DECLARE
		@quy INT;
	SELECT
		@quy = iquychungtu 
	FROM
		BH_QTC_Quy_CheDoBHXH 
	WHERE
		ID_QTC_Quy_CheDoBHXH = @IdChungTu;
---Lấy danh sách mục lục ngân sách
	SELECT
		danhmuc.iID_MLNS AS iID_MLNS,
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
		danhmuc.sDuToanChiTietToi INTO #tblMucLucNganSach 
	FROM
		BH_DM_MucLucNganSach AS danhmuc 
	WHERE
		danhmuc.iNamLamViec = @INamLamViec 
		AND danhmuc.sLNs IN (SELECT * FROM f_split (@SLNS)) ---Lấy thông tin chi tiết chứng từ
	SELECT
		qtcn_ct.ID_QTC_Quy_CheDoBHXH_ChiTiet,
		qtcn_ct.iID_MucLucNganSach,
		qtcn_ct.sLoaiTroCap,
		qtcn_ct.fTienDuToanDuyet,
		(
			isnull(qtcn_ct_truoc.fTienCNVCQP_DeNghi, 0) + isnull(qtcn_ct_truoc.fTienHSQBS_DeNghi, 0) + isnull(qtcn_ct_truoc.fTienLDHD_DeNghi, 0) + isnull(qtcn_ct_truoc.fTienQNCN_DeNghi, 0) + isnull(qtcn_ct_truoc.fTienSQ_DeNghi, 0) 
		) fTienLuyKeCuoiQuyTruoc,
		(
			isnull(qtcn_ct_truoc.iSoCNVCQP_DeNghi, 0) + isnull(qtcn_ct_truoc.iSoHSQBS_DeNghi, 0) + isnull(qtcn_ct_truoc.iSoLDHD_DeNghi, 0) + isnull(qtcn_ct_truoc.iSoQNCN_DeNghi, 0) + isnull(qtcn_ct_truoc.iSoSQ_DeNghi, 0) 
		) iSoLuyKeCuoiQuyTruoc,
		qtcn_ct.iSoSQ_DeNghi,
		qtcn_ct.fTienSQ_DeNghi,
		qtcn_ct.iSoQNCN_DeNghi,
		qtcn_ct.fTienQNCN_DeNghi,
		qtcn_ct.iSoCNVCQP_DeNghi,
		qtcn_ct.fTienCNVCQP_DeNghi,
		qtcn_ct.iSoHSQBS_DeNghi,
		qtcn_ct.fTienHSQBS_DeNghi,
--qtcn_ct.iTongSo_DeNghi,
--qtcn_ct.fTongTien_DeNghi,
		qtcn_ct.fTongTien_PheDuyet,
		qtcn_ct.iSoLDHD_DeNghi,
		qtcn_ct.fTienLDHD_DeNghi,
		qtcn.iID_MaDonVi iIDMaDonVi,
		qtcn.iNamChungTu iNamLamViec INTO #tblQuyetToanQuyChiTiet 
	FROM
		BH_QTC_Quy_CheDoBHXH_ChiTiet AS qtcn_ct
		INNER JOIN BH_QTC_Quy_CheDoBHXH AS qtcn ON qtcn_ct.iID_QTC_Quy_CheDoBHXH = qtcn.ID_QTC_Quy_CheDoBHXH
		LEFT JOIN (
		SELECT SUM
			(isnull(fTienCNVCQP_DeNghi, 0)) fTienCNVCQP_DeNghi,
			SUM (isnull(fTienHSQBS_DeNghi, 0)) fTienHSQBS_DeNghi,
			SUM (isnull(fTienLDHD_DeNghi, 0)) fTienLDHD_DeNghi,
			SUM (isnull(fTienQNCN_DeNghi, 0)) fTienQNCN_DeNghi,
			SUM (isnull(fTienSQ_DeNghi, 0)) fTienSQ_DeNghi,
			SUM (isnull(iSoCNVCQP_DeNghi, 0)) iSoCNVCQP_DeNghi,
			SUM (isnull(ctct.fTongTien_PheDuyet, 0)) fTongTien_PheDuyet,
			SUM (isnull(iSoHSQBS_DeNghi, 0)) iSoHSQBS_DeNghi,
			SUM (isnull(iSoLDHD_DeNghi, 0)) iSoLDHD_DeNghi,
			SUM (isnull(iSoQNCN_DeNghi, 0)) iSoQNCN_DeNghi,
			SUM (isnull(iSoSQ_DeNghi, 0)) iSoSQ_DeNghi,
			ct.iID_MaDonVi,
			ct.iNamChungTu 
		FROM
			BH_QTC_Quy_CheDoBHXH_ChiTiet ctct
			INNER JOIN BH_QTC_Quy_CheDoBHXH ct ON ctct.iID_QTC_Quy_CheDoBHXH = ct.ID_QTC_Quy_CheDoBHXH 
		WHERE
			ct.iQuyChungTu < @quy 
		GROUP BY
			ct.iID_MaDonVi,
			ct.iNamChungTu 
		) qtcn_ct_truoc ON qtcn.iID_MaDonVi = qtcn_ct_truoc.iID_MaDonVi 
		AND qtcn.iNamChungTu = qtcn_ct_truoc.iNamChungTu 
	WHERE
		qtcn_ct.iID_QTC_Quy_CheDoBHXH = @IdChungTu 
		AND qtcn.iNamChungTu=@INamLamViec;
---Kết quả hiển thị trả về
	IF
		(@Loai = 1) SELECT
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
		mucluc.sDuToanChiTietToi,
		chi_tiet.ID_QTC_Quy_CheDoBHXH_ChiTiet,
		mucluc.iID_MLNS AS iID_MucLucNganSach,
		mucluc.sMoTa AS sLoaiTroCap,
		(isnull(dt.fTienTuChi, 0)) fTienDuToanDuyet,
		chi_tiet.iSoLuyKeCuoiQuyTruoc,
		chi_tiet.fTienLuyKeCuoiQuyTruoc,
		chi_tiet.iSoSQ_DeNghi,
		chi_tiet.fTienSQ_DeNghi,
		chi_tiet.iSoQNCN_DeNghi,
		chi_tiet.fTienQNCN_DeNghi,
		chi_tiet.iSoCNVCQP_DeNghi,
		chi_tiet.fTienCNVCQP_DeNghi,
		chi_tiet.iSoHSQBS_DeNghi,
		chi_tiet.fTienHSQBS_DeNghi,
--chi_tiet.iTongSo_DeNghi,
--chi_tiet.fTongTien_DeNghi,
		chi_tiet.fTongTien_PheDuyet,
		chi_tiet.iSoLDHD_DeNghi,
		chi_tiet.fTienLDHD_DeNghi,
		chi_tiet.iIDMaDonVi,
		chi_tiet.iNamLamViec 
	FROM
		#tblMucLucNganSach AS mucluc
		LEFT JOIN (
			SELECT ctct.sXauNoiMa, SUM(ctct.fTienTuChi)  fTienTuChi
			FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct 
			JOIN BH_DTC_PhanBoDuToanChi ct ON ctct.iID_DTC_PhanBoDuToanChi = ct.ID 
				WHERE ctct.iID_MaDonVi = @MaDonVi 
				AND BIsKhoa = 1
				AND ct.iNamChungTu = @INamLamViec
				GROUP BY ctct.sXauNoiMa) dt ON dt.sXauNoiMa = mucluc.sXauNoiMa
		LEFT JOIN #tblQuyetToanQuyChiTiet AS chi_tiet ON mucluc.iID_MLNS = chi_tiet.iID_MucLucNganSach 
	ORDER BY
		mucluc.sXauNoiMa ELSE SELECT
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
		mucluc.sDuToanChiTietToi,
		chi_tiet.ID_QTC_Quy_CheDoBHXH_ChiTiet,
		mucluc.iID_MLNS AS iID_MucLucNganSach,
		mucluc.sMoTa AS sLoaiTroCap,
		(isnull(dt.fTienTuChi, 0) ) fTienDuToanDuyet,
		chi_tiet.iSoLuyKeCuoiQuyTruoc,
		chi_tiet.fTienLuyKeCuoiQuyTruoc,
		chi_tiet.iSoSQ_DeNghi,
		chi_tiet.fTienSQ_DeNghi,
		chi_tiet.iSoQNCN_DeNghi,
		chi_tiet.fTienQNCN_DeNghi,
		chi_tiet.iSoCNVCQP_DeNghi,
		chi_tiet.fTienCNVCQP_DeNghi,
		chi_tiet.iSoHSQBS_DeNghi,
		chi_tiet.fTienHSQBS_DeNghi,
--chi_tiet.iTongSo_DeNghi,
--chi_tiet.fTongTien_DeNghi,
		chi_tiet.fTongTien_PheDuyet,
		chi_tiet.iSoLDHD_DeNghi,
		chi_tiet.fTienLDHD_DeNghi,
		chi_tiet.iIDMaDonVi,
		chi_tiet.iNamLamViec 
	FROM
		#tblMucLucNganSach AS mucluc
		LEFT JOIN (
					SELECT ctct.sXauNoiMa,
							SUM(ctct.fTienTuChi) fTienTuChi
							FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct 
							JOIN BH_DTC_DuToanChiTrenGiao ct ON ctct.iID_DTC_DuToanChiTrenGiao = ct.ID 
		WHERE ct.iID_MaDonVi = @MaDonVi
		AND BIsKhoa = 1
		AND ct.iNamLamViec = @INamLamViec
		GROUP BY ctct.sXauNoiMa) dt ON dt.sXauNoiMa = mucluc.sXauNoiMa
		LEFT JOIN #tblQuyetToanQuyChiTiet AS chi_tiet ON mucluc.iID_MLNS = chi_tiet.iID_MucLucNganSach 
	ORDER BY
		mucluc.sXauNoiMa DROP TABLE #tblMucLucNganSach;
	DROP TABLE #tblQuyetToanQuyChiTiet;
	
END;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chiquyKCB_chitiet_clone]    Script Date: 3/1/2024 4:58:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_quyet_toan_chiquyKCB_chitiet_clone]
	@IdChungTu uniqueidentifier,
	@IDLoaiChi uniqueidentifier,
	@SLNS nvarchar(max),
	@SMaLoaiChi nvarchar(50),
	@IIdMaDonVi nvarchar(500),
	@DNgayChungTu datetime,
	@iQuyChungTu int,
	@INamLamViec int,
	@Loai int
AS
BEGIN
	DECLARE @iD uniqueidentifier='00000000-0000-0000-0000-000000000000';
	DECLARE @CountIndex INT;
	SELECT * into #tempLNS from f_split(@SLNS);
	SELECT * into #tempAgency from  f_split(@IIdMaDonVi);
	SELECT @CountIndex = COUNT(*) FROM 
									BH_QTC_QUY_KCB_Chitiet 
									WHERE iID_QTC_Quy_KCB =@IdChungTu

	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha,sDuToanChiTietToi
			into #tblNsMlns
	FROM BH_DM_MucLucNganSach 
	where iNamLamViec = @INamLamViec 

		-- lấy ra dữ liệu dự toán
	SELECT 
		  SUM(fTienTuChi) AS fTien_DuToanGiaoNamNay,
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
          AND iNamChungTu = @INamLamViec
          AND iID_LoaiDanhMucChi = @IDLoaiChi
		  AND sMaLoaiChi=@SMaLoaiChi
		  AND bIsKhoa=1
          AND cast(dNgayQuyetDinh AS date) <= cast(@DNgayChungTu AS date))
     AND iID_MaDonVi in  (SELECT * FROM f_split(@IIdMaDonVi))-- donvi
   GROUP BY iID_MaDonVi, 
   iID_MucLucNganSach,
   sXauNoiMa

   	SELECT 
		  SUM(fTienTuChi) AS fTien_DuToanGiaoNamNay,
          iID_MaDonVi,
          iID_MucLucNganSach,
		  sXauNoiMa
		  into #tblNhanPhanBoTrenGiao
   FROM BH_DTC_DuToanChiTrenGiao_ChiTiet
   WHERE iID_DTC_DuToanChiTrenGiao IN
       (SELECT ID
        FROM BH_DTC_DuToanChiTrenGiao
        WHERE sSoQuyetDinh <> ''
          AND sSoQuyetDinh IS NOT NULL
          AND iNamLamViec = @INamLamViec
          AND iID_LoaiDanhMucChi = @IDLoaiChi
		  AND sMaLoaiChi=@SMaLoaiChi
		  AND bIsKhoa=1
          --AND cast(dNgayQuyetDinh AS date) <= cast(@DNgayChungTu AS date)) 
		  )
     AND iID_MaDonVi in  (SELECT * FROM f_split(@IIdMaDonVi))-- donvi
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
		INamLamViec = @INamLamViec 
		AND iID_MaDonVi IN (SELECT * FROM #tempAgency)

	-- Tao bang tam luu chu mlng con
	SELECT * INTO #tblMlnsByPhanCapExistDonVi
	FROM #tblMlnsByPhanCap tbl, #tblDonVi dv
	WHERE  tbl.bHangCha = 0 OR (tbl.bHangCha = 1 and (IsNull(tbl.sM, '') != '' OR tbl.sM <> '' OR IsNull(tbl.sTM, '') != '')  )
	AND tbl.sTM !=''  


	-- Tao bang tam luu chu mlng cha
	SELECT *, '' AS iID_MaDonVi, '' AS sTenDonVi  INTO #tblMlnsByPhanCapNotDonVi
	FROM #tblMlnsByPhanCap 
		WHERE bHangCha = 1 AND ( (IsNull(sM, '') = '' or sM = '')) AND ( (IsNull(sTM, '') = '' or sTM = ''))
		OR ((ISNULL(sTM,'')='' OR sTM='')) OR sTM='0'

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
		WHERE bHangCha = 0
		UNION ALL
		SELECT *, null AS iID_MaDonVi, null AS sTenDonVi  
		FROM #tblNsMlns 
		WHERE bHangCha = 1
	) mlns

	SELECT CTCT.* 
		INTO #TempChungTuQuyTruoc
	FROM BH_QTC_QUY_KCB_Chitiet CTCT
		WHERE CTCT.iID_QTC_Quy_KCB IN
	(
		SELECT ID_QTC_Quy_KCB FROM  BH_QTC_QUY_KCB 
		WHERE iID_MaDonVi IN ( SELECT * FROM  f_split(@IIdMaDonVi))
					AND iNamChungTu=@INamLamViec
					AND iQuyChungTu=1
	)
	--- chung tu thuong
	if 	@Loai=1	
	-- Get data
	SELECT
		isnull(ctct.ID_QTC_QUY_KCB_Chitiet, @iD) AS ID_QTC_QUY_KCB_Chitiet,
		@IdChungTu AS iID_QTC_QUY_KCB,
		mlnsPhanBo.iID_MLNS,
		mlnsPhanBo.iID_MLNS_Cha,
		mlnsPhanBo.iID_MLNS as iID_MucLucNganSach,
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
		mlnsPhanBo.bHangCha ,
		mlnsPhanBo.sDuToanChiTietToi,
		@INamLamViec AS INamLamViec,
		mlnsPhanBo.iID_MaDonVi AS IID_MaDonVi,
		mlnsPhanBo.sTenDonVi AS STenDonVi,
		isnull(tblQuyTruoc.fTien_DuToanNamTruocChuyenSang, 0) as FTienDuToanNamTruocChuyenSang,
		isnull(daCapDuToan.fTien_DuToanGiaoNamNay, 0) as FTienDuToanGiaoNamNay,
		isnull(ctct.fTien_TongDuToanDuocGiao, 0) as FTienTongDuToanDuocGiao,
		isnull(ctct.fTienThucChi, 0) as FTienThucChi,
		isnull(ctct.fTienQuyetToanDaDuyet, 0) as FTienQuyetToanDaDuyet,
		isnull(ctct.fTienDeNghiQuyetToanQuyNay, 0) as FTienDeNghiQuyetToanQuyNay,
		isnull(ctct.fTienXacNhanQuyetToanQuyNay, 0) as FTienXacNhanQuyetToanQuyNay,
		isnull(ctct.dNgayTao, getdate()) AS DNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS DNgaySua,
		ctct.sNguoiTao AS SNguoiTao,
		ctct.sNguoiSua AS SNguoiSua
	FROM #tblMlnsRoot AS mlnsPhanBo
	LEFT JOIN
		(SELECT *
			FROM 
				BH_QTC_QUY_KCB_Chitiet
			WHERE 
		 		iID_QTC_QUY_KCB = @IdChungTu
		) ctct
	ON mlnsPhanBo.iID_MLNS = ctct.iID_MucLucNganSach --and mlnsPhanBo.iID_MaDonVi = ctct.iID_MaDonVi
	LEFT JOIN BH_QTC_QUY_KCB ct ON ctct.iID_QTC_QUY_KCB = ct.ID_QTC_QUY_KCB
	LEFT JOIN #tblPhanBoDuToan daCapDuToan
	ON mlnsPhanBo.sXauNoiMa = daCapDuToan.sXauNoiMa
	LEFT JOIN #TempChungTuQuyTruoc tblQuyTruoc on mlnsPhanBo.sXauNoiMa=tblQuyTruoc.sXauNoiMa
	WHERE mlnsPhanBo.sLNS IN (SELECT * FROM #tempLNS)
	ORDER BY mlnsPhanBo.sXauNoiMa, mlnsPhanBo.iID_MaDonVi;
	------ Chứng từ tong hop
	ELSE 
	---- Get data
	SELECT
		isnull(ctct.ID_QTC_QUY_KCB_Chitiet, @iD) AS ID_QTC_QUY_KCB_Chitiet,
		@IdChungTu AS iID_QTC_QUY_KCB,
		mlnsPhanBo.iID_MLNS ,
		mlnsPhanBo.iID_MLNS_Cha ,
		mlnsPhanBo.iID_MLNS as iID_MucLucNganSach,
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
		mlnsPhanBo.bHangCha ,
		mlnsPhanBo.sDuToanChiTietToi,
		@INamLamViec AS INamLamViec,
		mlnsPhanBo.iID_MaDonVi AS IID_MaDonVi,
		mlnsPhanBo.sTenDonVi AS STenDonVi,
		isnull(ctct.fTien_DuToanNamTruocChuyenSang, 0) as FTienDuToanNamTruocChuyenSang,
		isnull(daCapDuToan.fTien_DuToanGiaoNamNay, 0) as FTienDuToanGiaoNamNay,
		isnull(ctct.fTien_TongDuToanDuocGiao, 0) as FTienTongDuToanDuocGiao,
		isnull(ctct.fTienThucChi, 0) as FTienThucChi,
		isnull(ctct.fTienQuyetToanDaDuyet, 0) as FTienQuyetToanDaDuyet,
		isnull(ctct.fTienDeNghiQuyetToanQuyNay, 0) as FTienDeNghiQuyetToanQuyNay,
		isnull(ctct.fTienXacNhanQuyetToanQuyNay, 0) as FTienXacNhanQuyetToanQuyNay,
		isnull(ctct.dNgayTao, getdate()) AS DNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS DNgaySua,
		ctct.sNguoiTao AS SNguoiTao,
		ctct.sNguoiSua AS SNguoiSua
	FROM #tblMlnsRoot AS mlnsPhanBo
	LEFT JOIN
		(SELECT *
			FROM 
				BH_QTC_QUY_KCB_Chitiet
			WHERE 
		 		iID_QTC_QUY_KCB = @IdChungTu
		) ctct
	ON mlnsPhanBo.iID_MLNS = ctct.iID_MucLucNganSach --and mlnsPhanBo.iID_MaDonVi = ctct.iID_MaDonVi
	LEFT JOIN BH_QTC_QUY_KCB ct ON ctct.iID_QTC_QUY_KCB = ct.ID_QTC_QUY_KCB
	LEFT JOIN #tblNhanPhanBoTrenGiao daCapDuToan
	ON mlnsPhanBo.sXauNoiMa = daCapDuToan.sXauNoiMa 
	LEFT JOIN #TempChungTuQuyTruoc tblQuyTruoc on mlnsPhanBo.sXauNoiMa=tblQuyTruoc.sXauNoiMa
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
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_quykinhphi_quanly_chungtu_chi_tiet]    Script Date: 3/1/2024 4:58:30 PM ******/
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

	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha,sDuToanChiTietToi
			into #tblNsMlns
	FROM BH_DM_MucLucNganSach 
	where iNamLamViec = @YearOfWork 

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
          AND iID_LoaiDanhMucChi = @iID_LoaiDanhMucChi
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
		WHERE bHangCha = 0
		UNION ALL
		SELECT *, null AS iID_MaDonVi, null AS sTenDonVi  
		FROM #tblNsMlns 
		WHERE bHangCha = 1
	) mlns

	--IF @CountIndex=0
	--BEGIN
	-- lấy dữ liệu đã cấp
	--SELECT  
	--	0 AS fTienDuToanDuocGiao,
	--	SUM(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay,
	--	SUM(fTienQuyetToanDaDuyet) AS fTienQuyetToanDaDuyet,
	--	SUM(fTienThucChi) AS fTienThucChi,
	--	SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay,
	--	CT.iID_MaDonVi,
	--    CTCT.iID_MucLucNganSach
	--	into #tblDataDaCapQuyTruoc
	--FROM
	--BH_QTC_Quy_KinhPhiQuanLy CT
	--INNER JOIN BH_QTC_Quy_KinhPhiQuanLy_chiTiet CTCT
	--ON CT.ID_QTC_Quy_KinhPhiQuanLy=CTCT.iID_QTC_Quy_KinhPhiQuanLy
	--WHERE CT.iNamChungTu = @YearOfWork
	--	  --AND i = @iID_LoaiDanhMucChi
 --         AND CAST(CT.dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
	--	  AND CT.ID_QTC_Quy_KinhPhiQuanLy=@VoucherId
	--	  AND CT.iID_MaDonVi IN (SELECT * FROM f_split(@AgencyId))
	--	  AND CT.iQuyChungTu<@iQuyChungTu

	--GROUP BY  CT.iID_MaDonVi,CTCT.iID_MucLucNganSach
	--SELECT * into #tblDataDuToan FROM #tblPhanBoDuToan

	--SELECT sum(fTienDuToanDuocGiao) AS fTienDuToanDuocGiao, sum(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay, sum(fTienQuyetToanDaDuyet) fTienQuyetToanDaDuyet, SUM(fTienThucChi) AS fTienThucChi,SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay, iID_MaDonVi, iID_MucLucNganSach into #tblDataDaCapDuToan FROM (
	--	SELECT * FROM #tblDataDaCap
	--	UNION ALL
	--	SELECT * FROM #tblDataDuToan
	--	) data
	--GROUP BY iID_MaDonVi, iID_MucLucNganSach;
	----select * from  #tblMlnsRoot
	--SELECT mlns.iID_MLNS,
	--	   mlns.iID_MLNS_Cha,
	--	   mlns.sXauNoiMa,
	--	   fTienDuToanDuocGiao,
	--	   fTienDeNghiQuyetToanQuyNay,
	--	   fTienQuyetToanDaDuyet,
	--	   fTienThucChi,
	--	   fTienXacNhanQuyetToanQuyNay,
	--	   isnull(mlns.iID_MaDonVi, daCapDuToan.iID_MaDonVi) as iID_MaDonVi
	--	   INTO #tblDaCapDuToan
	--FROM #tblMlnsRoot mlns
	--LEFT JOIN
	--  #tblDataDaCapDuToan daCapDuToan
	--ON mlns.iID_MLNS = daCapDuToan.iID_MucLucNganSach and ((mlns.iID_MaDonVi is not null and mlns.iID_MaDonVi = daCapDuToan.iID_MaDonVi) or mlns.iID_MaDonVi is null)

	--SELECT iID_MLNS, iID_MLNS_Cha, iID_MaDonVi, sum(fTienDuToanDuocGiao) AS fTienDuToanDuocGiao, sum(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay, SUM(fTienQuyetToanDaDuyet) as fTienQuyetToanDaDuyet,SUM(fTienThucChi) as fTienThucChi,SUM(fTienXacNhanQuyetToanQuyNay) as fTienXacNhanQuyetToanQuyNay INTO #tblDaCapDuToanResult FROM (
	--	SELECT distinct T.iID_MLNS,  
	--		   T.iID_MLNS_Cha,
	--		   --isnull(T.iID_MaDonVi, T.iID_MaDonVi) as iID_MaDonVi,
	--		   T.iID_MaDonVi,
	--		   T.fTienDuToanDuocGiao,
	--		   T.fTienDeNghiQuyetToanQuyNay,
	--		   T.fTienQuyetToanDaDuyet,
	--		   T.fTienThucChi,
	--		   T.fTienXacNhanQuyetToanQuyNay
	--	FROM #tblDaCapDuToan T 
	--	WHERE T.fTienDuToanDuocGiao <> 0) data
	--GROUP BY iID_MLNS, iID_MLNS_Cha, iID_MaDonVi
	--OPTION (maxrecursion 0)

	---- Get data
	--SELECT
	--	isnull(ctct.ID_QTC_Quy_KinhPhiQuanLy_ChiTiet, @iD) AS ID_QTC_Quy_KinhPhiQuanLy_ChiTiet,
	--	@VoucherId AS iID_QTC_Quy_KinhPhiQuanLy,
	--	mlnsPhanBo.iID_MLNS AS iID_MucLucNganSach,
	--	mlnsPhanBo.iID_MLNS_Cha AS IdParent,
	--	mlnsPhanBo.sXauNoiMa AS sXauNoiMa,
	--	mlnsPhanBo.sLNS AS SLNS,
	--	mlnsPhanBo.sL AS SL,
	--	mlnsPhanBo.sK AS SK,
	--	mlnsPhanBo.sM AS SM,
	--	mlnsPhanBo.sTM AS STM,
	--	mlnsPhanBo.sTTM AS STTM,
	--	mlnsPhanBo.sNG AS SNG,
	--	mlnsPhanBo.sTNG AS STNG,
	--	mlnsPhanBo.sTNG1 AS STNG1,
	--	mlnsPhanBo.sTNG2 AS STNG2,
	--	mlnsPhanBo.sTNG3 AS STNG3,
	--	mlnsPhanBo.sMoTa AS SNoiDung,
	--	mlnsPhanBo.bHangCha As IsHangCha,
	--	mlnsPhanBo.sDuToanChiTietToi,
	--	@YearOfWork AS INamLamViec,
	--	mlnsPhanBo.iID_MaDonVi AS IID_MaDonVi,
	--	mlnsPhanBo.sTenDonVi AS STenDonVi,
	--	isnull(daCapDuToan.fTienDeNghiQuyetToanQuyNay, 0) as FTienDeNghiQuyetToanQuyNay,
	--	isnull(daCapDuToan.fTienDuToanDuocGiao, 0) as FTienDuToanDuocGiao,
	--	isnull(daCapDuToan.fTienQuyetToanDaDuyet, 0) as FTienQuyetToanDaDuyet,
	--	isnull(daCapDuToan.fTienThucChi, 0) as FTienThucChi,
	--	isnull(daCapDuToan.fTienXacNhanQuyetToanQuyNay, 0) as FTienXacNhanQuyetToanQuyNay,
	--	ctct.sGhiChu AS SGhiChu,
	--	isnull(ctct.dNgayTao, getdate()) AS DNgayTao,
	--	isnull(ctct.dNgaySua, getdate()) AS DNgaySua,
	--	ctct.sNguoiTao AS SNguoiTao,
	--	ctct.sNguoiSua AS SNguoiSua
	--FROM #tblMlnsRoot AS mlnsPhanBo
	--LEFT JOIN
	--	(SELECT *
	--		FROM 
	--			BH_QTC_Quy_KinhPhiQuanLy_chiTiet
	--		WHERE 
	--	 		iID_QTC_Quy_KinhPhiQuanLy = @VoucherId
	--	) ctct
	--ON mlnsPhanBo.iID_MLNS = ctct.iID_MucLucNganSach --and mlnsPhanBo.iID_MaDonVi = ctct.iID_MaDonVi
	--LEFT JOIN BH_QTC_Quy_KinhPhiQuanLy ct ON ctct.iID_QTC_Quy_KinhPhiQuanLy = ct.ID_QTC_Quy_KinhPhiQuanLy
	--LEFT JOIN #tblDaCapDuToanResult daCapDuToan
	--ON mlnsPhanBo.iID_MLNS = daCapDuToan.iID_MLNS and daCapDuToan.iID_MaDonVi LIKE '%' + mlnsPhanBo.iID_MaDonVi + '%' 
	--WHERE mlnsPhanBo.sLNS IN (SELECT * FROM #tempLNS)
	--ORDER BY mlnsPhanBo.sXauNoiMa, mlnsPhanBo.iID_MaDonVi;
	--END
	--ELSE 
	--	BEGIN
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
		  --AND i = @iID_LoaiDanhMucChi
          AND CAST(CT.dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
		  --AND CT.ID_QTC_Quy_KinhPhiQuanLy=@VoucherId
		  AND CT.iID_MaDonVi IN (SELECT * FROM f_split(@AgencyId))
		  AND CT.iQuyChungTu<@iQuyChungTu

	GROUP BY  CT.iID_MaDonVi,CTCT.sXauNoiMa


	--SELECT sum(fTienDuToanDuocGiao) AS fTienDuToanDuocGiao, sum(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay, SUM(fTienQuyetToanDaDuyet) AS fTienQuyetToanDaDuyet,SUM(fTienThucChi) AS fTienThucChi,SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay, iID_MaDonVi, iID_MucLucNganSach into #tblDataDaCapDuToanExist FROM (
	--	SELECT * FROM #tblDataDaCapExist
	--	) data
	--GROUP BY iID_MaDonVi, iID_MucLucNganSach;
	----select * from  #tblMlnsRoot
	--SELECT mlns.iID_MLNS,
	--	   mlns.iID_MLNS_Cha,
	--	   mlns.sXauNoiMa,
	--	   fTienDuToanDuocGiao,
	--	   fTienDeNghiQuyetToanQuyNay,
	--	   fTienQuyetToanDaDuyet,
	--	   fTienThucChi,
	--	   fTienXacNhanQuyetToanQuyNay,
	--	   isnull(mlns.iID_MaDonVi, daCapDuToan.iID_MaDonVi) as iID_MaDonVi
	--	   INTO #tblDaCapDuToanExist
	--FROM #tblMlnsRoot mlns
	--LEFT JOIN
	--  #tblDataDaCapDuToanExist daCapDuToan
	--ON mlns.iID_MLNS = daCapDuToan.iID_MucLucNganSach and ((mlns.iID_MaDonVi is not null and mlns.iID_MaDonVi = daCapDuToan.iID_MaDonVi) or mlns.iID_MaDonVi is null)

	--SELECT iID_MLNS, iID_MLNS_Cha, iID_MaDonVi, sum(fTienDuToanDuocGiao) AS fTienDuToanDuocGiao, sum(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay, SUM(fTienQuyetToanDaDuyet) as fTienQuyetToanDaDuyet,SUM(fTienThucChi) AS fTienThucChi, SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay INTO #tblDaCapDuToanResultExist FROM (
	--	SELECT distinct T.iID_MLNS,  
	--		   T.iID_MLNS_Cha,
	--		   --isnull(T.iID_MaDonVi, T.iID_MaDonVi) as iID_MaDonVi,
	--		   T.iID_MaDonVi,
	--		   T.fTienDuToanDuocGiao,
	--		   T.fTienDeNghiQuyetToanQuyNay,
	--		   T.fTienQuyetToanDaDuyet,
	--		   T.fTienThucChi,
	--		   T.fTienXacNhanQuyetToanQuyNay
	--	FROM #tblDaCapDuToanExist T 
	--	WHERE  T.fTienDuToanDuocGiao <> 0 OR T.fTienDeNghiQuyetToanQuyNay<>0 OR   T.fTienQuyetToanDaDuyet<>0 OR T.fTienThucChi <>0 OR T.fTienXacNhanQuyetToanQuyNay<>0) data
	--GROUP BY iID_MLNS, iID_MLNS_Cha, iID_MaDonVi
	--OPTION (maxrecursion 0)

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
	IF (@Loai=0)
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
	ON mlnsPhanBo.sXauNoiMa = daCapDuToan.sXauNoiMa and  mlnsPhanBo.iID_MaDonVi=daCapDuToan.iID_MaDonVi
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
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkp_ql_chungtu_chi_tiet]    Script Date: 3/1/2024 4:58:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_qtc_qkp_ql_chungtu_chi_tiet]
	 @NamLamViec int,
	 @iQuy nvarchar(100),
	 @IdDonVi nvarchar(max),
	 @LNS nvarchar(max),
	 @UserName nvarchar(100),
	 @LoaiTongHop int,
	 @Dvt int
AS
BEGIN 
	SET NOCOUNT ON;
	-- lấy ra mlns theo phân cấp
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha,sDuToanChiTietToi
			into #tblMlnsByPhanCap
	FROM BH_DM_MucLucNganSach 
	WHERE 
		iNamLamViec = @NamLamViec 
		AND iTrangThai = 1
		--AND (
		--	(@PhanCap = 'M' AND (sTM IS NULL OR sTM = ''))
		--	OR (@PhanCap = 'TM' AND (sTTM IS NULL OR sTTM = ''))
		--	OR (@PhanCap = 'NG' AND (sTNG IS NULL OR sTNG = ''))
		--	)
		AND sLNS IN (SELECT * FROM f_split(@LNS))
		--(
		--			SELECT DISTINCT VALUE
		--			FROM 
		--			(
		--				SELECT 
		--					CAST(LEFT(sLNS, 1) AS nvarchar(10)) LNS1, 
		--					CAST(LEFT(sLNS, 3) AS nvarchar(10)) LNS3, 
		--					CAST(LEFT(sLNS, 5) AS nvarchar(10)) LNS5, 
		--					CAST(sLNS AS nvarchar(10)) LNS 
		--				FROM
		--					NS_NguoiDung_LNS 
		--				WHERE 
		--					sMaNguoiDung = @UserName
		--					AND INamLamViec = @NamLamViec
		--					AND sLNS IN (SELECT * FROM f_split(@LNS))
		--			) LNS
		--			UNPIVOT
		--			(
		--				value
		--				FOR col in (LNS1, LNS3, LNS5, LNS)
		--			) un) 

IF @LoaiTongHop=1
BEGIN
SELECT 
		mlns.iID_MLNS as IdMlns,
		mlns.iID_MLNS_Cha as IdMlnsCha,
		mlns.sXauNoiMa as XauNoiMa,
		mlns.sLNS as SLNS,
		mlns.sL as SL,
		mlns.sK as SK,
		mlns.sM as SM,
		mlns.sTM as STM,
		mlns.sTTM as STTM,
		mlns.sNG as SNG,
		mlns.sTNG as STNG,
		mlns.sMoTa as SNoiDung,
		mlns.bHangCha as IsHangCha,
		mlns.sDuToanChiTietToi,
		ISNULL((dt.fTienDuToanDuocGiao), 0) / @Dvt as FTienDuToanDuocGiao,
		ISNULL(SUM(ctct.fTienThucChi), 0) / @Dvt as FTienThucChi, 
		ISNULL(SUM(ctct.fTienQuyetToanDaDuyet), 0) / @Dvt as FTienQuyetToanDaDuyet, 
		ISNULL(SUM(ctct.fTienDeNghiQuyetToanQuyNay), 0) / @Dvt as FTienDeNghiQuyetToanQuyNay, 
		ISNULL(SUM(ctct.fTienXacNhanQuyetToanQuyNay), 0) / @Dvt as FTienXacNhanQuyetToanQuyNay 
	FROM 
		#tblMlnsByPhanCap mlns
	LEFT JOIN 
		(SELECT * FROM BH_QTC_Quy_KinhPhiQuanLy_ChiTiet 
				WHERE iID_QTC_Quy_KinhPhiQuanLy in
					( SELECT ID_QTC_Quy_KinhPhiQuanLy FROM BH_QTC_Quy_KinhPhiQuanLy
								WHERE iNamChungTu=@NamLamViec
								AND iQuyChungTu=@iQuy
								--AND bIsKhoa=1
								AND iID_MaDonVi IN (select * from f_split(@IdDonVi))
								)) ctct
	on mlns.iID_MLNS = ctct.iID_MucLucNganSach
	LEFT JOIN (
			-- lấy ra dữ liệu dự toán
				SELECT 
					  SUM(fTienTuChi) AS fTienDuToanDuocGiao,
					  sXauNoiMa
			   FROM BH_DTC_PhanBoDuToanChi_ChiTiet
			   WHERE iID_DTC_PhanBoDuToanChi IN
				   (SELECT ID
					FROM BH_DTC_PhanBoDuToanChi
					WHERE sSoQuyetDinh <> ''
					  AND sSoQuyetDinh IS NOT NULL
					  AND iNamChungTu = @NamLamViec
					  AND bIsKhoa=1
					  --AND cast(dNgayQuyetDinh AS date) <= cast(@VoucherDate AS date))
				 AND iID_MaDonVi in  (SELECT * FROM f_split(@IdDonVi))-- donvi
				) GROUP BY sXauNoiMa)dt on dt.sXauNoiMa=mlns.sXauNoiMa
	Group by mlns.iID_MLNS
			, mlns.iID_MLNS_Cha
			, mlns.sXauNoiMa
			, mlns.sLNS
			, mlns.sL
			, mlns.sK
			, mlns.sM
			, mlns.sTM
			, mlns.sTTM
			, mlns.sNG
			, mlns.sTNG
			, mlns.sMoTa
			, mlns.bHangCha
			, mlns.sDuToanChiTietToi
			, dt.fTienDuToanDuocGiao
	Order by mlns.sXauNoiMa
END
ELSE
BEGIN
SELECT 
		mlns.iID_MLNS as IdMlns,
		mlns.iID_MLNS_Cha as IdMlnsCha,
		mlns.sXauNoiMa as XauNoiMa,
		mlns.sLNS as SLNS,
		mlns.sL as SL,
		mlns.sK as SK,
		mlns.sM as SM,
		mlns.sTM as STM,
		mlns.sTTM as STTM,
		mlns.sNG as SNG,
		mlns.sTNG as STNG,
		mlns.sMoTa as SNoiDung,
		mlns.bHangCha as IsHangCha,
		mlns.sDuToanChiTietToi,
		ISNULL((dt.fTienTuChi), 0) / @Dvt as FTienDuToanDuocGiao,
		ISNULL(SUM(ctct.fTienThucChi), 0) / @Dvt as FTienThucChi, 
		ISNULL(SUM(ctct.fTienQuyetToanDaDuyet), 0) / @Dvt as FTienQuyetToanDaDuyet, 
		ISNULL(SUM(ctct.fTienDeNghiQuyetToanQuyNay), 0) / @Dvt as FTienDeNghiQuyetToanQuyNay, 
		ISNULL(SUM(ctct.fTienXacNhanQuyetToanQuyNay), 0) / @Dvt as FTienXacNhanQuyetToanQuyNay 
	FROM 
		#tblMlnsByPhanCap mlns
	LEFT JOIN 
		(SELECT * FROM BH_QTC_Quy_KinhPhiQuanLy_ChiTiet 
				WHERE iID_QTC_Quy_KinhPhiQuanLy in
					( SELECT ID_QTC_Quy_KinhPhiQuanLy FROM BH_QTC_Quy_KinhPhiQuanLy
								WHERE iNamChungTu=@NamLamViec
								AND iQuyChungTu=@iQuy
								--AND iLoaiTongHop=@LoaiTongHop
								AND iID_MaDonVi IN (select * from f_split(@IdDonVi))
								)) ctct
	on mlns.iID_MLNS = ctct.iID_MucLucNganSach 
	LEFT JOIN (
		SELECT ctct.sXauNoiMa,
					SUM(ctct.fTienTuChi) fTienTuChi
					FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct 
					JOIN BH_DTC_DuToanChiTrenGiao ct 
					ON ctct.iID_DTC_DuToanChiTrenGiao = ct.ID 
					WHERE ct.iID_MaDonVi = @IdDonVi
					AND BIsKhoa = 1
					AND ct.iNamLamViec = @NamLamViec
					GROUP BY ctct.sXauNoiMa)
		dt on dt.sXauNoiMa=mlns.sXauNoiMa
	Group by mlns.iID_MLNS
		, mlns.iID_MLNS_Cha
		, mlns.sXauNoiMa
		, mlns.sLNS
		, mlns.sL
		, mlns.sK
		, mlns.sM
		, mlns.sTM
		, mlns.sTTM
		, mlns.sNG
		, mlns.sTNG
		, mlns.sMoTa
		, mlns.bHangCha
		, mlns.sDuToanChiTietToi
		, dt.fTienTuChi
	Order by mlns.sXauNoiMa
END
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chi_tiet]    Script Date: 3/1/2024 5:17:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_cp_chungtu_chi_tiet]
	@VoucherId uniqueidentifier ,
	@LNS nvarchar(max),
	@YearOfWork int,
	@AgencyId nvarchar(500),
	@VoucherDate datetime,
	@iID_LoaiDanhMucChi nvarchar(255)
AS
BEGIN
	DECLARE @iD uniqueidentifier='00000000-0000-0000-0000-000000000000';
	DECLARE @CountIndex INT;
	SELECT * into #tempLNS from f_split(@LNS);
	SELECT * into #tempAgency from  f_split(@AgencyId);
	SELECT @CountIndex = COUNT(*) 
	FROM BH_CP_ChungTu_ChiTiet
	WHERE iID_CP_ChungTu = @VoucherId

	DECLARE
		@quy INT;
	SELECT
		@quy = iQuy 
	FROM
		BH_CP_ChungTu 
	WHERE
		iID_CP_ChungTu = @VoucherId;

	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha,sCPChiTietToi
			into #tblNsMlns
	FROM BH_DM_MucLucNganSach 
	where iNamLamViec = @YearOfWork 



		-- lấy ra dữ liệu dự toán
	SELECT 
		  SUM(fTienTuChi) AS fTienDuToan,
		  --fTongTien AS fTienDuToan,
           0 AS fTienKeHoachCap,
          0 AS fTienDaCap,
          iID_MaDonVi,
          iID_MucLucNganSach
		  into #tblPhanBoDuToan
   FROM BH_DTC_PhanBoDuToanChi_ChiTiet
   WHERE iID_DTC_PhanBoDuToanChi IN
       (SELECT ID
        FROM BH_DTC_PhanBoDuToanChi
        WHERE sSoQuyetDinh <> ''
          AND sSoQuyetDinh IS NOT NULL
          AND iNamChungTu = @YearOfWork
          AND iID_LoaiDanhMucChi = @iID_LoaiDanhMucChi
		  AND bIsKhoa=1
          AND cast(dNgayQuyetDinh AS date) <= cast(@VoucherDate AS date))
     AND iID_MaDonVi in  (SELECT * FROM f_split(@AgencyId))-- donvi
   GROUP BY iID_MaDonVi, 
   iID_MucLucNganSach

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
		WHERE sLNS in (select * from splitstring(@LNS)) and  bHangCha = 1
			and( sL <>'' or sL is not null) and (sCPChiTietToi <>'' or sCPChiTietToi is not null)
		UNION ALL
		SELECT *, null AS iID_MaDonVi, null AS sTenDonVi  
		FROM #tblNsMlns 
		WHERE sLNS in (select * from splitstring(@LNS)) and  bHangCha = 1
			and( sL ='' or sL is null) 
			and (sCPChiTietToi ='' or sCPChiTietToi is null)
	) mlns

	SELECT 
	SUM(ctct.fTienKeHoachCap) fTienDaCap,
	SUM(ctct.fTienKeHoachCap) fTienKeHoachCap,
	ctct.iID_MaDonVi,
	ct.iNamChungTu,
	ctct.sXauNoiMa
	into #tempDuToanCapQuyTruoc
	FROM BH_CP_ChungTu_ChiTiet ctct
	INNER JOIN BH_CP_ChungTu ct ON ctct.iID_CP_ChungTu=ct.iID_CP_ChungTu
	where ct.iQuy<@quy 
	and ct.iID_LoaiCap=@iID_LoaiDanhMucChi
	and CAST(dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
	and ctct.iID_MaDonVi in (select * from splitstring(@AgencyId)) 
	and ct.iNamChungTu=@YearOfWork
	GROUP BY
			ctct.iID_MaDonVi,
			ct.iNamChungTu,
			ctct.sXauNoiMa
	

	---- lấy dữ liệu đã cấp
	--SELECT SUM(fTienDuToan) AS fTienDuToan,
	--	  SUM(fTienKeHoachCap) AS fTienKeHoachCap,
	--	  SUM(fTienDaCap) AS fTienDaCap,
 --         iID_MaDonVi,
 --         iID_MucLucNganSach
	--	  into #tblDataDaCapExist
	--FROM BH_CP_ChungTu_ChiTiet
	--WHERE iID_CP_ChungTu IN
 --      (
	--	SELECT iID_CP_ChungTu
 --       FROM BH_CP_ChungTu
 --       WHERE iNamChungTu = @YearOfWork
	--	  AND iID_LoaiCap = @iID_LoaiDanhMucChi
 --         AND CAST(dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
	--	  AND EXISTS(SELECT * FROM #tempAgency INTERSECT SELECT * FROM f_split(@AgencyId))
	--	  AND iID_CP_ChungTu=@VoucherId
	--	)
	--	AND iID_MaDonVi IN (SELECT * FROM #tempAgency)
	--GROUP BY iID_MaDonVi, iID_MucLucNganSach

	--SELECT sum(fTienDuToan) AS fTienDuToan, sum(fTienKeHoachCap) AS fTienKeHoachCap, sum(fTienDaCap) fTienDaCap, iID_MaDonVi, iID_MucLucNganSach into #tblDataDaCapDuToanExist FROM (
	--	SELECT * FROM #tblDataDaCapExist
	--	) data
	--GROUP BY iID_MaDonVi, iID_MucLucNganSach;
	----select * from  #tblMlnsRoot
	--SELECT mlns.iID_MLNS,
	--	   mlns.iID_MLNS_Cha,
	--	   mlns.sXauNoiMa,
	--	   fTienDuToan,
	--	   fTienDaCap,
	--	   fTienKeHoachCap,
	--	   isnull(mlns.iID_MaDonVi, daCapDuToan.iID_MaDonVi) as iID_MaDonVi
	--	   INTO #tblDaCapDuToanExist
	--FROM #tblMlnsRoot mlns
	--LEFT JOIN
	--  #tblDataDaCapDuToanExist daCapDuToan
	--ON mlns.iID_MLNS = daCapDuToan.iID_MucLucNganSach and ((mlns.iID_MaDonVi is not null and mlns.iID_MaDonVi = daCapDuToan.iID_MaDonVi) or mlns.iID_MaDonVi is null)

	--SELECT iID_MLNS, iID_MLNS_Cha, iID_MaDonVi, sum(fTienDuToan) AS fTienDuToan, sum(fTienDaCap) AS fTienDaCap, SUM(fTienKeHoachCap) as fTienKeHoachCap INTO #tblDaCapDuToanResultExist FROM (
	--	SELECT distinct T.iID_MLNS,  
	--		   T.iID_MLNS_Cha,
	--		   --isnull(T.iID_MaDonVi, T.iID_MaDonVi) as iID_MaDonVi,
	--		   T.iID_MaDonVi,
	--		   T.fTienDuToan,
	--		   T.fTienDaCap,
	--		   T.fTienKeHoachCap
	--	FROM #tblDaCapDuToanExist T 
	--	WHERE T.fTienDaCap <> 0 OR T.fTienDuToan <> 0 OR t.fTienKeHoachCap<>0) data
	--GROUP BY iID_MLNS, iID_MLNS_Cha, iID_MaDonVi
	--option (maxrecursion 0)



	-- Get data
	SELECT
		isnull(ctct.iID_CP_ChungTu_ChiTiet, @iD) AS iID_CP_ChungTu_ChiTiet,
		@VoucherId AS iID_CP_ChungTu,
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
		mlnsPhanBo.sCPChiTietToi sDuToanChiTietToi,
		@YearOfWork AS INamLamViec,
		mlnsPhanBo.iID_MaDonVi AS IID_MaDonVi,
		mlnsPhanBo.sTenDonVi AS STenDonVi,
		isnull(daCaCap.fTienDaCap, 0) as FTienDaCaQuyTruoc,
		isnull(daCaCap.fTienKeHoachCap, 0) as FTienKeHoachCapQuyTruoc,
		isnull(daCapDuToan.fTienDuToan, 0) as FTienDuToan,
		isnull(ctct.fTienKeHoachCap, 0) as FTienKeHoachCap,
		ctct.sGhiChu AS SGhiChu,
		isnull(ctct.dNgayTao, getdate()) AS DNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS DNgaySua,
		ctct.sNguoiTao AS SNguoiTao,
		ctct.sNguoiSua AS SNguoiSua
	FROM #tblMlnsRoot AS mlnsPhanBo
	LEFT JOIN
		(SELECT *
			FROM 
				BH_CP_ChungTu_ChiTiet
			WHERE 
		 		iID_CP_ChungTu = @VoucherId
		) ctct
	ON mlnsPhanBo.iID_MLNS = ctct.iID_MucLucNganSach and mlnsPhanBo.iID_MaDonVi = ctct.iID_MaDonVi
	LEFT JOIN BH_CP_ChungTu ct ON ctct.iID_CP_ChungTu = ct.iID_CP_ChungTu
	LEFT JOIN #tblPhanBoDuToan daCapDuToan
	on mlnsPhanBo.iID_MLNS = daCapDuToan.iID_MucLucNganSach and daCapDuToan.iID_MaDonVi = mlnsPhanBo.iID_MaDonVi 
	LEFT JOIN #tempDuToanCapQuyTruoc daCaCap on mlnsPhanBo.iID_MaDonVi=daCaCap.iID_MaDonVi and mlnsPhanBo.sXauNoiMa=daCaCap.sXauNoiMa
	where mlnsPhanBo.sLNS IN (SELECT * FROM #tempLNS)
	order by mlnsPhanBo.sXauNoiMa, mlnsPhanBo.iID_MaDonVi;
	
END
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chi_tiet_chedo_bhxh]    Script Date: 3/1/2024 5:17:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_cp_chungtu_chi_tiet_chedo_bhxh]
	@VoucherId uniqueidentifier ,
	@LNS nvarchar(max),
	@YearOfWork int,
	@AgencyId nvarchar(500),
	@VoucherDate datetime,
	@iID_LoaiDanhMucChi nvarchar(255)
AS
BEGIN
	DECLARE @iD uniqueidentifier='00000000-0000-0000-0000-000000000000';
	DECLARE @CountIndex INT;
	SELECT * into #tempLNS from f_split(@LNS);
	SELECT * into #tempAgency from  f_split(@AgencyId);
	SELECT @CountIndex = COUNT(*) 
	FROM BH_CP_ChungTu_ChiTiet
	WHERE iID_CP_ChungTu = @VoucherId
	DECLARE
		@quy INT;
	SELECT
		@quy = iQuy 
	FROM
		BH_CP_ChungTu 
	WHERE
		iID_CP_ChungTu = @VoucherId;
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha,sCPChiTietToi
			into #tblNsMlns
	FROM BH_DM_MucLucNganSach 
	where iNamLamViec = @YearOfWork 
	and iTrangThai=1
	and sLNS in (select * from splitstring(@LNS))
	--and  bHangCha = 1 AND ( (IsNull(sM, '') = '' or sM = '')) AND ( (IsNull(sTM, '') = '' or sTM = '')) OR ((ISNULL(sTM,'')='' OR sTM='')) OR sTM='0'

	-- lấy ra dữ liệu dự toán
	SELECT 
		  SUM(fTienTuChi) AS fTienDuToan,
		  --fTongTien AS fTienDuToan,
           0 AS fTienKeHoachCap,
          0 AS fTienDaCap,
          iID_MaDonVi,
          iID_MucLucNganSach
		  into #tblPhanBoDuToan
   FROM BH_DTC_PhanBoDuToanChi_ChiTiet
   WHERE iID_DTC_PhanBoDuToanChi IN
       (SELECT ID
        FROM BH_DTC_PhanBoDuToanChi
        WHERE sSoQuyetDinh <> ''
          AND sSoQuyetDinh IS NOT NULL
          AND iNamChungTu = @YearOfWork
          AND iID_LoaiDanhMucChi = @iID_LoaiDanhMucChi
		  AND bIsKhoa=1
          AND cast(dNgayQuyetDinh AS date) <= cast(@VoucherDate AS date))
     AND iID_MaDonVi in  (SELECT * FROM f_split(@AgencyId))-- donvi
   GROUP BY iID_MaDonVi, 
   iID_MucLucNganSach

   SELECT 
	SUM(ctct.fTienKeHoachCap) fTienDaCap,
	SUM(ctct.fTienKeHoachCap)  fTienKeHoachCap,
	ctct.iID_MaDonVi,
	ct.iNamChungTu,
	ctct.sXauNoiMa
	into #tempDuToanCapQuyTruoc
	FROM BH_CP_ChungTu_ChiTiet ctct
	INNER JOIN BH_CP_ChungTu ct ON ctct.iID_CP_ChungTu=ct.iID_CP_ChungTu
	where ct.iQuy<@quy 
	and ct.iID_LoaiCap=@iID_LoaiDanhMucChi
	and CAST(dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
	and ctct.iID_MaDonVi in (select * from splitstring(@AgencyId)) 
	and ct.iNamChungTu=@YearOfWork
	GROUP BY
			ctct.iID_MaDonVi,
			ct.iNamChungTu,
			ctct.sXauNoiMa
	
	-- lấy dữ liệu đã cấp
	--SELECT ISNULL(SUM(fTienDuToan),0) AS fTienDuToan,
	--	  ISNULL(SUM(fTienKeHoachCap),0)  AS fTienKeHoachCap,
	--	  ISNULL(SUM(fTienDaCap),0)  AS fTienDaCap,
 --         iID_MaDonVi,
 --         iID_MucLucNganSach
	--	  into #tblDataDaCap
	--FROM BH_CP_ChungTu_ChiTiet
	--WHERE iID_CP_ChungTu IN
 --      (
	--	SELECT iID_CP_ChungTu
 --       FROM BH_CP_ChungTu
 --       WHERE iNamChungTu = @YearOfWork
	--	  AND iID_LoaiCap = @iID_LoaiDanhMucChi
 --         AND CAST(dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
	--	  AND EXISTS(SELECT * FROM #tempAgency INTERSECT SELECT * FROM f_split(@AgencyId))
	--	  AND iID_CP_ChungTu=@VoucherId
	--	)
	--	AND iID_MaDonVi IN (SELECT * FROM #tempAgency)
	--GROUP BY iID_MaDonVi, iID_MucLucNganSach

	-- tao bang tam chua don vi
	SELECT iID_MaDonVi, iID_MaDonVi + ' - ' + sTenDonVi as sTenDonVi into #tblDonVi
	FROM DonVi 
	WHERE 
		INamLamViec = @YearOfWork 
		AND iID_MaDonVi IN (SELECT * FROM f_split(@AgencyId))
	
	
	-- Tao bang tam luu chu mlns cha co don vi
	SELECT * INTO #tblMlnsExistDonVi
	FROM #tblNsMlns ,#tblDonVi dv
		WHERE bHangCha = 1 AND ( (IsNull(sM, '') = '' or sM = '')) AND ( (IsNull(sTM, '') = '' or sTM = '')) OR ((ISNULL(sTM,'')='' OR sTM='')) OR sTM='0'
	
	-- Tao bang tam luu chu mlns va tien du toan chi che do bhxh
	SELECT ml.sXauNoiMa,
		SUM(pb.fTienDuToan) fTienDuToan ,
		ml.iID_MaDonVi 
		INTO #tempMlnsbhxh 
		FROM #tblMlnsExistDonVi ml
   LEFT JOIN #tblPhanBoDuToan pb ON pb.iID_MucLucNganSach=ml.iID_MLNS and ml.iID_MaDonVi=pb.iID_MaDonVi
   GROUP BY ml.sXauNoiMa,ml.iID_MaDonVi
		ORDER BY ml.sXauNoiMa

	SELECT SUBSTRING(A.sXauNoiMa,1,3) sXauNoiMa,
		A.iID_MaDonVi, 
		SUM(A.fTienDuToan) fTienDuToan
		INTO #tblDaCapDuToanResult
		FROM #tempMlnsbhxh  A
		GROUP BY SUBSTRING(A.sXauNoiMa,1,3),A.iID_MaDonVi
		ORDER BY SUBSTRING(A.sXauNoiMa,1,3),A.iID_MaDonVi

	-- Get data
	SELECT
		isnull(ctct.iID_CP_ChungTu_ChiTiet, @iD) AS iID_CP_ChungTu_ChiTiet,
		@VoucherId AS iID_CP_ChungTu,
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
		mlnsPhanBo.sCPChiTietToi sDuToanChiTietToi,
		@YearOfWork AS INamLamViec,
		mlnsPhanBo.iID_MaDonVi AS IID_MaDonVi,
		mlnsPhanBo.sTenDonVi AS STenDonVi,
		isnull(daCaCap.fTienDaCap, 0) as FTienDaCaQuyTruoc,
		isnull(daCaCap.fTienKeHoachCap, 0) as FTienKeHoachCapQuyTruoc,
		isnull(daCapDuToan.fTienDuToan, 0) as FTienDuToan,
		isnull(ctct.fTienKeHoachCap, 0) as FTienKeHoachCap,
		ctct.sGhiChu AS SGhiChu,
		isnull(ctct.dNgayTao, getdate()) AS DNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS DNgaySua,
		ctct.sNguoiTao AS SNguoiTao,
		ctct.sNguoiSua AS SNguoiSua
	FROM #tblMlnsExistDonVi AS mlnsPhanBo
	LEFT JOIN
		(SELECT *
			FROM 
				BH_CP_ChungTu_ChiTiet
			WHERE 
		 		iID_CP_ChungTu = @VoucherId
		) ctct
	ON mlnsPhanBo.iID_MLNS = ctct.iID_MucLucNganSach and mlnsPhanBo.iID_MaDonVi = ctct.iID_MaDonVi
	LEFT JOIN BH_CP_ChungTu ct ON ctct.iID_CP_ChungTu = ct.iID_CP_ChungTu
	LEFT JOIN #tblDaCapDuToanResult daCapDuToan
	on mlnsPhanBo.sXauNoiMa = daCapDuToan.sXauNoiMa and daCapDuToan.iID_MaDonVi= mlnsPhanBo.iID_MaDonVi
	LEFT JOIN #tempDuToanCapQuyTruoc daCaCap on mlnsPhanBo.iID_MaDonVi=daCaCap.iID_MaDonVi and mlnsPhanBo.sXauNoiMa=daCaCap.sXauNoiMa
	where mlnsPhanBo.sLNS ='901'
	order by mlnsPhanBo.sXauNoiMa, mlnsPhanBo.iID_MaDonVi;

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
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chi_tiet_cssk_hssv_nld]    Script Date: 3/1/2024 5:17:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_cp_chungtu_chi_tiet_cssk_hssv_nld]
	@VoucherId uniqueidentifier ,
	@LNS nvarchar(max),
	@YearOfWork int,
	@AgencyId nvarchar(500),
	@VoucherDate datetime,
	@iID_LoaiDanhMucChi nvarchar(255)
AS
BEGIN
	DECLARE @iD uniqueidentifier='00000000-0000-0000-0000-000000000000';
	DECLARE @CountIndex INT;
	SELECT * into #tempLNS from f_split(@LNS);
	SELECT * into #tempAgency from  f_split(@AgencyId);
	SELECT @CountIndex = COUNT(*) 
	FROM BH_CP_ChungTu_ChiTiet
	WHERE iID_CP_ChungTu = @VoucherId
	DECLARE
		@quy INT;
	SELECT
		@quy = iQuy 
	FROM
		BH_CP_ChungTu 
	WHERE
		iID_CP_ChungTu = @VoucherId;
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha,sCPChiTietToi
			into #tblNsMlns
	FROM BH_DM_MucLucNganSach 
	where iNamLamViec = @YearOfWork 
	and iTrangThai=1
	and sLNS in (select * from splitstring(@LNS))
	--and  bHangCha = 1 AND ( (IsNull(sM, '') = '' or sM = '')) AND ( (IsNull(sTM, '') = '' or sTM = '')) OR ((ISNULL(sTM,'')='' OR sTM='')) OR sTM='0'

	-- lấy ra dữ liệu dự toán
	SELECT 
		  SUM(fTienTuChi) AS fTienDuToan,
		  --fTongTien AS fTienDuToan,
           0 AS fTienKeHoachCap,
          0 AS fTienDaCap,
          iID_MaDonVi,
          iID_MucLucNganSach
		  into #tblPhanBoDuToan
   FROM BH_DTC_PhanBoDuToanChi_ChiTiet
   WHERE iID_DTC_PhanBoDuToanChi IN
       (SELECT ID
        FROM BH_DTC_PhanBoDuToanChi
        WHERE sSoQuyetDinh <> ''
          AND sSoQuyetDinh IS NOT NULL
          AND iNamChungTu = @YearOfWork
          AND iID_LoaiDanhMucChi = @iID_LoaiDanhMucChi
		  AND bIsKhoa=1
          AND cast(dNgayQuyetDinh AS date) <= cast(@VoucherDate AS date))
     AND iID_MaDonVi in  (SELECT * FROM f_split(@AgencyId))-- donvi
   GROUP BY iID_MaDonVi, 
   iID_MucLucNganSach

	-- lấy dữ liệu đã cấp
	   SELECT 
	SUM(ctct.fTienKeHoachCap) fTienDaCap,
	SUM(ctct.fTienKeHoachCap) fTienKeHoachCap,
	ctct.iID_MaDonVi,
	ct.iNamChungTu,
	ctct.sXauNoiMa
	into #tempDuToanCapQuyTruoc
	FROM BH_CP_ChungTu_ChiTiet ctct
	INNER JOIN BH_CP_ChungTu ct ON ctct.iID_CP_ChungTu=ct.iID_CP_ChungTu
	where ct.iQuy<@quy 
	and ct.iID_LoaiCap=@iID_LoaiDanhMucChi
	and CAST(dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
	and ctct.iID_MaDonVi in (select * from splitstring(@AgencyId)) 
	and ct.iNamChungTu=@YearOfWork
	GROUP BY
			ctct.iID_MaDonVi,
			ct.iNamChungTu,
			ctct.sXauNoiMa
	--SELECT ISNULL(SUM(fTienDuToan),0) AS fTienDuToan,
	--	  ISNULL(SUM(fTienKeHoachCap),0)  AS fTienKeHoachCap,
	--	  ISNULL(SUM(fTienDaCap),0)  AS fTienDaCap,
 --         iID_MaDonVi,
 --         iID_MucLucNganSach
	--	  into #tblDataDaCap
	--FROM BH_CP_ChungTu_ChiTiet
	--WHERE iID_CP_ChungTu IN
 --      (
	--	SELECT iID_CP_ChungTu
 --       FROM BH_CP_ChungTu
 --       WHERE iNamChungTu = @YearOfWork
	--	  AND iID_LoaiCap = @iID_LoaiDanhMucChi
 --         AND CAST(dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
	--	  AND EXISTS(SELECT * FROM #tempAgency INTERSECT SELECT * FROM f_split(@AgencyId))
	--	  AND iID_CP_ChungTu=@VoucherId
	--	)
	--	AND iID_MaDonVi IN (SELECT * FROM #tempAgency)
	--GROUP BY iID_MaDonVi, iID_MucLucNganSach

	-- tao bang tam chua don vi
	SELECT iID_MaDonVi, iID_MaDonVi + ' - ' + sTenDonVi as sTenDonVi into #tblDonVi
	FROM DonVi 
	WHERE 
		INamLamViec = @YearOfWork 
		AND iID_MaDonVi IN (SELECT * FROM f_split(@AgencyId))
	
	
	
	-- Tao bang tam luu chu mlns cha co don vi
	SELECT *,'' AS iID_MaDonVi, '' AS sTenDonVi   INTO #tblMlnsByPhanCapNotDonVi
	FROM #tblNsMlns 
	WHERE bHangCha = 1 AND ( (IsNull(sM, '') = '' or sM = '')) AND ( (IsNull(sTM, '') = '' or sTM = '')) 
	
		-- Tao bang tam luu chu mlns cha co don vi
	SELECT * INTO #tblMlnsByPhanCapExistDonVi
	FROM #tblNsMlns ,#tblDonVi dv
		WHERE  bHangCha=0

		-- map bảng mlnsPhanCap và đơn vị
	SELECT * INTO #tblMLNS FROM (
		SELECT *
		FROM #tblMlnsByPhanCapExistDonVi tbl
		UNION ALL
		SELECT *
		FROM #tblMlnsByPhanCapNotDonVi 
	) mlns

	
	---- Tao bang tam luu chu mlns va tien du toan chi che do bhxh
	--SELECT ml.sXauNoiMa,
	--	SUM(pb.fTienDuToan) fTienDuToan ,
	--	ml.iID_MaDonVi 
	--	INTO #tempMlnsbhxh 
	--	FROM #tblMlnsExistDonVi ml
 --  LEFT JOIN #tblPhanBoDuToan pb ON pb.iID_MucLucNganSach=ml.iID_MLNS and ml.iID_MaDonVi=pb.iID_MaDonVi
 --  GROUP BY ml.sXauNoiMa,ml.iID_MaDonVi
	--	ORDER BY ml.sXauNoiMa

	--SELECT SUBSTRING(A.sXauNoiMa,1,3) sXauNoiMa,
	--	A.iID_MaDonVi, 
	--	SUM(A.fTienDuToan) fTienDuToan
	--	INTO #tblDaCapDuToanResult
	--	FROM #tempMlnsbhxh  A
	--	GROUP BY SUBSTRING(A.sXauNoiMa,1,3),A.iID_MaDonVi
	--	ORDER BY SUBSTRING(A.sXauNoiMa,1,3),A.iID_MaDonVi

	-- Get data
	SELECT
		isnull(ctct.iID_CP_ChungTu_ChiTiet, @iD) AS iID_CP_ChungTu_ChiTiet,
		@VoucherId AS iID_CP_ChungTu,
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
		mlnsPhanBo.sCPChiTietToi sDuToanChiTietToi,
		@YearOfWork AS INamLamViec,
		mlnsPhanBo.iID_MaDonVi AS IID_MaDonVi,
		mlnsPhanBo.sTenDonVi AS STenDonVi,
		isnull(daCaCap.fTienDaCap, 0) as FTienDaCaQuyTruoc,
		isnull(daCaCap.fTienKeHoachCap, 0) as FTienKeHoachCapQuyTruoc,
		0 as FTienDuToan,
		isnull(ctct.fTienKeHoachCap, 0) as FTienKeHoachCap,
		ctct.sGhiChu AS SGhiChu,
		isnull(ctct.dNgayTao, getdate()) AS DNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS DNgaySua,
		ctct.sNguoiTao AS SNguoiTao,
		ctct.sNguoiSua AS SNguoiSua
	FROM #tblMLNS AS mlnsPhanBo
	LEFT JOIN
		(SELECT *
			FROM 
				BH_CP_ChungTu_ChiTiet
			WHERE 
		 		iID_CP_ChungTu = @VoucherId
				
		) ctct
	ON mlnsPhanBo.iID_MLNS = ctct.iID_MucLucNganSach and mlnsPhanBo.iID_MaDonVi = ctct.iID_MaDonVi
	LEFT JOIN BH_CP_ChungTu ct ON ctct.iID_CP_ChungTu = ct.iID_CP_ChungTu
	LEFT JOIN #tempDuToanCapQuyTruoc daCaCap on mlnsPhanBo.iID_MaDonVi=daCaCap.iID_MaDonVi and mlnsPhanBo.sXauNoiMa=daCaCap.sXauNoiMa
	--LEFT JOIN #tempMlnsbhxh daCapDuToan
	--on mlnsPhanBo.sXauNoiMa = daCapDuToan.sXauNoiMa and daCapDuToan.iID_MaDonVi= mlnsPhanBo.iID_MaDonVi
	
	order by mlnsPhanBo.sXauNoiMa ,mlnsPhanBo.iID_MaDonVi;

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
