/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkpk_thongtriloai2_bh]    Script Date: 11/24/2023 5:04:01 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qtc_qkpk_thongtriloai2_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qtc_qkpk_thongtriloai2_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkpk_thongtriloai1_bh]    Script Date: 11/24/2023 5:04:01 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qtc_qkpk_thongtriloai1_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qtc_qkpk_thongtriloai1_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkp_ql_thongtriloai2_bh]    Script Date: 11/24/2023 5:04:01 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qtc_qkp_ql_thongtriloai2_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qtc_qkp_ql_thongtriloai2_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkp_ql_thongtriloai1_bh]    Script Date: 11/24/2023 5:04:01 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qtc_qkp_ql_thongtriloai1_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qtc_qkp_ql_thongtriloai1_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkcb_thongtriloai1_bh]    Script Date: 11/24/2023 5:04:01 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qtc_qkcb_thongtriloai1_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qtc_qkcb_thongtriloai1_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qbhxh_thongtriloai1_bh]    Script Date: 11/24/2023 5:04:01 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qtc_qbhxh_thongtriloai1_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qtc_qbhxh_thongtriloai1_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_kpql_chitiet]    Script Date: 11/24/2023 5:04:01 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_khc_kpql_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_khc_kpql_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_chungtu_tonghop_bhxh]    Script Date: 11/24/2023 5:04:01 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_khc_chungtu_tonghop_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_khc_chungtu_tonghop_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_bhxh_chitiet]    Script Date: 11/24/2023 5:04:01 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_khc_bhxh_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_khc_bhxh_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_rpt_get_donvi_qKPQL_lns]    Script Date: 11/24/2023 5:04:01 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_rpt_get_donvi_qKPQL_lns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_rpt_get_donvi_qKPQL_lns]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_rpt_get_donvi_qKPK_lns]    Script Date: 11/24/2023 5:04:01 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_rpt_get_donvi_qKPK_lns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_rpt_get_donvi_qKPK_lns]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_rpt_get_donvi_qkcb_lns]    Script Date: 11/24/2023 5:04:01 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_rpt_get_donvi_qkcb_lns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_rpt_get_donvi_qkcb_lns]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_rpt_get_donvi_qBHXH_lns]    Script Date: 11/24/2023 5:04:01 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_rpt_get_donvi_qBHXH_lns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_rpt_get_donvi_qBHXH_lns]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_rpt_get_donvi_nKPQL_lns]    Script Date: 11/24/2023 5:04:01 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_rpt_get_donvi_nKPQL_lns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_rpt_get_donvi_nKPQL_lns]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_rpt_get_donvi_nKPK_lns]    Script Date: 11/24/2023 5:04:01 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_rpt_get_donvi_nKPK_lns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_rpt_get_donvi_nKPK_lns]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_rpt_get_donvi_nKCB_lns]    Script Date: 11/24/2023 5:04:01 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_rpt_get_donvi_nKCB_lns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_rpt_get_donvi_nKCB_lns]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_rpt_get_donvi_nBHXH_lns]    Script Date: 11/24/2023 5:04:01 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_rpt_get_donvi_nBHXH_lns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_rpt_get_donvi_nBHXH_lns]
GO
/****** Object:  StoredProcedure [dbo].[sp_khc_rpt_get_donvi_BHXH]    Script Date: 11/24/2023 5:04:01 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_khc_rpt_get_donvi_BHXH]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_khc_rpt_get_donvi_BHXH]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqKCB_update_total]    Script Date: 11/24/2023 5:04:01 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtcqKCB_update_total]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtcqKCB_update_total]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqBHXH_update_total]    Script Date: 11/24/2023 5:04:01 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtcqBHXH_update_total]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtcqBHXH_update_total]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcnKCB_update_total]    Script Date: 11/24/2023 5:04:01 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtcnKCB_update_total]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtcnKCB_update_total]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcnBHXH_update_total]    Script Date: 11/24/2023 5:04:01 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtcnBHXH_update_total]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtcnBHXH_update_total]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcnBHXH_create_data_summary]    Script Date: 11/24/2023 5:04:01 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtcnBHXH_create_data_summary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtcnBHXH_create_data_summary]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_create_quyet_toan_chinamkcb_chitiet_theo4quy]    Script Date: 11/24/2023 5:04:01 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_create_quyet_toan_chinamkcb_chitiet_theo4quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_create_quyet_toan_chinamkcb_chitiet_theo4quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_create_quyet_toan_chinamkcb_chitiet_theo4quy]    Script Date: 11/24/2023 5:04:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_create_quyet_toan_chinamkcb_chitiet_theo4quy]
@IdChungTu uniqueidentifier,
@IdMaDonVi nvarchar(50),
@INamLamViec int,
@User  nvarchar(50),
@IsTongHop bit
as
begin
	insert into BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet (	
			ID_QTC_Nam_KCB_QuanYDonVi_ChiTiet,
			iID_QTC_Nam_KCB_QuanYDonVi, 
			iID_MucLucNganSach, 
			sNoiDung, 
			dNgaySua,
			dNgayTao,
			sNguoiSua,
			sNguoiTao,
			fTien_ThucChi

			)
	select 
			NEWID(),
			@IdChungTu,
			tb_qtcy.iID_MucLucNganSach,
			tb_qtcy.sNoiDung,
			null,
			GETDATE(),
			null,
			@User,
			tb_qtcy.fTien_ThucChi
			
	from 
	(
		select 
				ctqt_quy_chitiet.iID_MucLucNganSach, 
				ctqt_quy_chitiet.sNoiDung,

				SUM(ctqt_quy_chitiet.fTienThucChi) as fTien_ThucChi
				
		from BH_QTC_Quy_KCB as ctqt_quy
		inner join BH_QTC_Quy_KCB_ChiTiet as ctqt_quy_chitiet on ctqt_quy.ID_QTC_Quy_KCB = ctqt_quy_chitiet.iID_QTC_Quy_KCB
		where ctqt_quy.iID_MaDonVi = @IdMaDonVi and ctqt_quy.iNamChungTu = @INamLamViec
				and ctqt_quy.iQuyChungTu=4
		--and ((@IsTongHop = 0 and ctqt_quy.bDaTongHop = 0 and ctqt_quy.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  ctqt_quy.sDSSoChungTuTongHop is not null))
		group by ctqt_quy_chitiet.iID_MucLucNganSach, ctqt_quy_chitiet.sNoiDung) as tb_qtcy

end
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcnBHXH_create_data_summary]    Script Date: 11/24/2023 5:04:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtcnBHXH_create_data_summary]
@IdChungTu nvarchar(MAX),
@NguoiTao nvarchar(500),
@YearOfWork int,
@IdChungTuSummary nvarchar(MAX)
AS
BEGIN
SET NOCOUNT ON;

INSERT INTO BH_QTC_Nam_CheDoBHXH_ChiTiet(
			ID_QTC_Nam_CheDoBHXH_ChiTiet
			, iID_QTC_Nam_CheDoBHXH
			, iID_MucLucNganSach
			, sLoaiTroCap
			, dNgaySua
			, dNgayTao
			, sNguoiSua
			, sNguoiTao
			, fTienDuToanDuyet
			, iSoDuToanDuocDuyet
			, iTongSo_ThucChi
			, fTongTien_ThucChi
			, iSoSQ_ThucChi
			, fTienSQ_ThucChi
			, iSoQNCN_ThucChi
			, fTienQNCN_ThucChi
			, iSoCNVCQP_ThucChi
			, fTienCNVCQP_ThucChi
			, iSoHSQBS_ThucChi
			, fTienHSQBS_ThucChi
			, fTienThua
			, fTienThieu
			, fTiLeThucHienTrenDuToan
			, iSoLDHD_ThucChi
			, fTienLDHD_ThucChi )
SELECT 
	   NEWID(),
	   @IdChungTuSummary,
       iID_MucLucNganSach,
       sLoaiTroCap,
	   GETDATE(),
	   GETDATE(),
	   @NguoiTao,
	   '',
	   SUM(fTienDuToanDuyet),
	   SUM(iSoDuToanDuocDuyet),
	   SUM(iTongSo_ThucChi),
	   SUM(fTongTien_ThucChi),
	   SUM(iSoSQ_ThucChi),
	   SUM(fTienSQ_ThucChi),
	   SUM(iSoQNCN_ThucChi),
	   SUM(fTienQNCN_ThucChi),
	   SUM(iSoCNVCQP_ThucChi),
	   SUM(fTienCNVCQP_ThucChi),
	   SUM(iSoHSQBS_ThucChi),
	   SUM(fTienHSQBS_ThucChi),
	   SUM(fTienThua),
	   SUM(fTienThieu),
	   SUM(fTiLeThucHienTrenDuToan),
	   SUM(iSoLDHD_ThucChi),
	   SUM(fTienLDHD_ThucChi)
FROM BH_QTC_Nam_CheDoBHXH_ChiTiet
WHERE  iID_QTC_Nam_CheDoBHXH IN
    (SELECT *
     FROM f_split(@IdChungTu))
group by iID_MucLucNganSach, sLoaiTroCap

UPDATE BH_QTC_Nam_CheDoBHXH SET bDaTongHop = 1 , iLoaiTongHop=2 WHERE ID_QTC_Nam_CheDoBHXH IN (SELECT * FROM f_split(@IdChungTu));
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcnBHXH_update_total]    Script Date: 11/24/2023 5:04:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtcnBHXH_update_total] 
	@VoucherId nvarchar(255),
	@UserModify nvarchar(100)
AS
BEGIN
	declare @FTongTienDuToanDuyet float;

	declare @ITongSoSQDeNghi float;
	declare @FTongTienSQDeNghi float;

	declare @ITongSoQNCNDeNghi float;
	declare @FTongTienQNCNDeNghi float;

	declare @ITongSoCNVCQPDeNghi float;
	declare @FTongTienCNVCQPDeNghi float;

	declare @ITongSoHSQBSDeNghi float;
	declare @FTongTienHSQBSDeNghi float;

	declare @ITongSoLDHDDeNghi float;
	declare @FTongTienLDHDDeNghi float;

	declare @ITongSoDeNghi float;
	declare @FTongTienDeNghi float;
	declare @FTongTienPheDuyet float;

;


	select 
		@FTongTienDuToanDuyet = SUM(fTienDuToanDuyet) ,

		@ITongSoSQDeNghi = SUM(iSoSQ_ThucChi) ,
		@FTongTienSQDeNghi = SUM(fTienSQ_ThucChi) ,

		@ITongSoQNCNDeNghi = SUM(iSoQNCN_ThucChi) ,
		@FTongTienQNCNDeNghi = SUM(fTienQNCN_ThucChi) ,

		@ITongSoCNVCQPDeNghi = SUM(iSoCNVCQP_ThucChi) ,
		@FTongTienCNVCQPDeNghi = SUM(fTienCNVCQP_ThucChi) ,

		@ITongSoLDHDDeNghi = SUM(iSoLDHD_ThucChi) ,
		@FTongTienLDHDDeNghi = SUM(fTienLDHD_ThucChi) ,

		@ITongSoHSQBSDeNghi = SUM(iSoHSQBS_ThucChi) ,
		@FTongTienHSQBSDeNghi = SUM(fTienHSQBS_ThucChi),
		
		@ITongSoDeNghi = SUM(iTongSo_ThucChi) ,
		@FTongTienDeNghi = SUM(fTongTien_ThucChi)


	FROM BH_QTC_Nam_CheDoBHXH_ChiTiet where iID_QTC_Nam_CheDoBHXH = @VoucherId;


	update BH_QTC_Nam_CheDoBHXH 
	set fTongTien_DuToanDuyet = @FTongTienDuToanDuyet, 

		iTongSoSQ_DeNghi = @ITongSoSQDeNghi,
		fTongTienSQ_DeNghi = @FTongTienSQDeNghi,

		iTongSoQNCN_DeNghi =  @ITongSoCNVCQPDeNghi,
		fTongTienQNCN_DeNghi = @FTongTienQNCNDeNghi,

		iTongSoCNVCQP_DeNghi = @ITongSoCNVCQPDeNghi,
		fTongTienCNVCQP_DeNghi = @FTongTienCNVCQPDeNghi,

		iTongSoHSQBS_DeNghi = @ITongSoHSQBSDeNghi,
		fTongTienHSQBS_DeNghi = @FTongTienHSQBSDeNghi,

		iTongSoLDHD_DeNghi=@ITongSoLDHDDeNghi,
		fTongTienLDHD_DeNghi=@FTongTienLDHDDeNghi,

		iTongSo_DeNghi = @ITongSoDeNghi,
		fTongTien_DeNghi = @FTongTienDeNghi,

		dNgaySua = GETDATE(), 
		sNguoiSua = @UserModify  
	where ID_QTC_Nam_CheDoBHXH = @VoucherId; 
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcnKCB_update_total]    Script Date: 11/24/2023 5:04:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_qtcnKCB_update_total] 
	@VoucherId nvarchar(255),
	@UserModify nvarchar(100)
AS
BEGIN
	declare @FTongTienDuToanNamTruocChuyenSang float;
	declare @FTongTienDuToanGiaoNamNay float;
	declare @FTongTienTongDuToanDuocGiao float;
	declare @FTongTienThucChi float;
	declare @FTongTienThua float;
	declare @FTongTienThieu float;
	declare @FTongTiLeThucHienTrenDuToan float;

	select 
		@FTongTienDuToanNamTruocChuyenSang = SUM(fTien_DuToanNamTruocChuyenSang) ,
		@FTongTienDuToanGiaoNamNay= SUM(fTien_DuToanGiaoNamNay),
		@FTongTienTongDuToanDuocGiao = SUM(fTien_TongDuToanDuocGiao),
		@FTongTienThucChi = SUM(fTien_ThucChi) ,
		@FTongTienThua = SUM(fTienThua) ,
		@FTongTienThieu = SUM(fTienThieu) ,
		@FTongTiLeThucHienTrenDuToan = SUM(fTiLeThucHienTrenDuToan)

	FROM BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet where iID_QTC_Nam_KCB_QuanYDonVi = @VoucherId;


	update BH_QTC_Nam_KCB_QuanYDonVi 
	set fTongTien_DuToanNamTruocChuyenSang = @FTongTienDuToanNamTruocChuyenSang, 
		fTongTien_DuToanGiaoNamNay = @FTongTienDuToanGiaoNamNay, 
		fTongTien_TongDuToanDuocGiao = @FTongTienTongDuToanDuocGiao,
		fTongTien_ThucChi = @FTongTienThucChi,
		fTongTienThua = @FTongTienThua,
		fTongTienThieu =  @FTongTienThieu,
		fTiLeThucHienTrenDuToan = @FTongTiLeThucHienTrenDuToan,
		dNgaySua = GETDATE(), 
		sNguoiSua = @UserModify  
	where ID_QTC_Nam_KCB_QuanYDonVi = @VoucherId; 
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqBHXH_update_total]    Script Date: 11/24/2023 5:04:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtcqBHXH_update_total] 
	@VoucherId nvarchar(255),
	@UserModify nvarchar(100)
AS
BEGIN
	declare @FTienDuToanDuyet float;
	declare @ISoLuyKeCuoiQuyNay int;
	declare @FTienLuyKeCuoiQuyNay float;
	declare @ISoSQ_DeNghi int;
	declare @FTienSQ_DeNghi float;
	declare @ISoQNCN_DeNghi int;
	declare @FTienQNCN_DeNghi float;
	declare @ISoCNVCQP_DeNghi int;
	declare @FTienCNVCQP_DeNghi float;
	declare @ISoHSQBS_DeNghi int;
	declare @FTienHSQBS_DeNghi float;
	declare @ISoLDHD_DeNghi int;
	declare @FTienLDHD_DeNghi float;
	declare @ITongSo_DeNghi int;
	declare @FTongTien_DeNghi float;
	declare @FTongTien_PheDuyet float;

	select 
		@FTienDuToanDuyet = SUM(fTienDuToanDuyet) ,
		@ISoLuyKeCuoiQuyNay= SUM(iSoLuyKeCuoiQuyNay),
		@FTienLuyKeCuoiQuyNay = SUM(fTienLuyKeCuoiQuyNay),
		@ISoSQ_DeNghi = SUM(iSoSQ_DeNghi),
		@FTienSQ_DeNghi = SUM(fTienSQ_DeNghi),
		@ISoQNCN_DeNghi = SUM(iSoQNCN_DeNghi),
		@FTienQNCN_DeNghi = SUM(fTienQNCN_DeNghi),
		@ISoCNVCQP_DeNghi = SUM(iSoCNVCQP_DeNghi),
		@FTienCNVCQP_DeNghi = SUM(fTienCNVCQP_DeNghi),
		@ISoHSQBS_DeNghi = SUM(iSoHSQBS_DeNghi),
		@FTienHSQBS_DeNghi = SUM(fTienHSQBS_DeNghi),

		@ISoLDHD_DeNghi = SUM(iSoLDHD_DeNghi),
		@FTienLDHD_DeNghi = SUM(fTienLDHD_DeNghi),

		@ITongSo_DeNghi = SUM(iTongSo_DeNghi),
		@FTongTien_DeNghi = SUM(fTongTien_DeNghi),
		@FTongTien_PheDuyet = SUM(fTongTien_PheDuyet)

	FROM BH_QTC_Quy_CheDoBHXH_ChiTiet where iID_QTC_Quy_CheDoBHXH = @VoucherId;


	update BH_QTC_Quy_CheDoBHXH 
	set fTongTien_DuToanDuyet = @FTienDuToanDuyet, 
		iTongSo_LuyKeCuoiQuyNay = @ISoLuyKeCuoiQuyNay, 
		fTongTien_LuyKeCuoiQuyNay = @FTienLuyKeCuoiQuyNay,
		iTongSoSQ_DeNghi = @ISoSQ_DeNghi,
		fTongTienSQ_DeNghi = @FTienSQ_DeNghi,
		iTongSoQNCN_DeNghi =  @ISoQNCN_DeNghi,
		fTongTienQNCN_DeNghi = @FTienQNCN_DeNghi,
		iTongSoCNVCQP_DeNghi = @ISoCNVCQP_DeNghi,
		fTongTienCNVCQP_DeNghi = @FTienCNVCQP_DeNghi,
		iTongSoHSQBS_DeNghi = @ISoHSQBS_DeNghi,
		fTongTienHSQBS_DeNghi = @FTienHSQBS_DeNghi,

		iTongSoLDHD_DeNghi = @ISoLDHD_DeNghi,
		fTongTienLDHD_DeNghi = @FTienLDHD_DeNghi,

		iTongSo_DeNghi = @ITongSo_DeNghi,
		fTongTien_DeNghi = @FTongTien_DeNghi,
		dNgaySua = GETDATE(), 
		sNguoiSua = @UserModify  
	where ID_QTC_Quy_CheDoBHXH = @VoucherId; 
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqKCB_update_total]    Script Date: 11/24/2023 5:04:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtcqKCB_update_total] 
	@VoucherId nvarchar(255),
	@UserModify nvarchar(100)
AS
BEGIN
	declare @FTongTien_DuToanNamTruocChuyenSang float;
	declare @FTongTien_DuToanGiaoNamNay float;
	declare @FTongTien_TongDuToanDuocGiao float;
	declare @FTongTienThucChi float;
	declare @FTongTienQuyetToanDaDuyet float;
	declare @FTongTienDeNghiQuyetToanQuyNay float;
	declare @FTongTienXacNhanQuyetToanQuyNay float;

	select 
		@FTongTien_DuToanNamTruocChuyenSang = SUM(fTien_DuToanNamTruocChuyenSang) ,
		@FTongTien_DuToanGiaoNamNay= SUM(fTien_DuToanGiaoNamNay),
		@FTongTien_TongDuToanDuocGiao = SUM(fTien_TongDuToanDuocGiao),
		@FTongTienThucChi = SUM(fTienThucChi),
		@FTongTienQuyetToanDaDuyet = SUM(fTienQuyetToanDaDuyet),
		@FTongTienDeNghiQuyetToanQuyNay = SUM(fTienDeNghiQuyetToanQuyNay),
		@FTongTienXacNhanQuyetToanQuyNay = SUM(fTienXacNhanQuyetToanQuyNay)

	FROM BH_QTC_Quy_KCB_ChiTiet where iID_QTC_Quy_KCB = @VoucherId;


	update BH_QTC_Quy_KCB 
	set fTongTien_DuToanNamTruocChuyenSang = @FTongTien_DuToanNamTruocChuyenSang, 
		fTongTien_DuToanGiaoNamNay = @FTongTien_DuToanGiaoNamNay, 
		fTongTien_TongDuToanDuocGiao = @FTongTien_TongDuToanDuocGiao,
		fTongTienThucChi = @FTongTienThucChi,
		fTongTienQuyetToanDaDuyet = @FTongTienQuyetToanDaDuyet,
		fTongTienDeNghiQuyetToanQuyNay =  @FTongTienDeNghiQuyetToanQuyNay,
		fTongTienXacNhanQuyetToanQuyNay = @FTongTienXacNhanQuyetToanQuyNay,
		dNgaySua = GETDATE(), 
		sNguoiSua = @UserModify  
	where ID_QTC_Quy_KCB = @VoucherId; 
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_khc_rpt_get_donvi_BHXH]    Script Date: 11/24/2023 5:04:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_khc_rpt_get_donvi_BHXH]
@NamLamViec int
AS BEGIN
SET NOCOUNT ON;

SELECT 
	donvi.iID_DonVi AS Id,
	donvi.iID_MaDonVi as IIDMaDonVi,
	donvi.sTenDonVi as TenDonVi,
	donvi.sKyHieu as KyHieu,
	donvi.sMoTa as MoTa,
	donvi.iLoai as Loai,
	donvi.iNamLamViec as NamLamViec,
	donvi.iTrangThai as iTrangThai,
	donvi.dNgayTao as DNgayTao,
	donvi.sNguoiTao as SNguoiTao,
	donvi.dNgaySua as DNgaySua,
	donvi.dNgaySua as SNguoiSua,
	donvi.*

FROM BH_KHC_CheDoBHXH chungtu
LEFT JOIN DonVi donvi ON chungtu.iID_MaDonVi = donvi.iID_MaDonVi
WHERE chungtu.iNamChungTu = @NamLamViec
  AND chungtu.iID_MaDonVi <> ''
  AND chungtu.iID_MaDonVi IS NOT NULL
  AND donvi.iNamLamViec = @NamLamViec
  --AND chungtu.iLoaiTongHop=@LoaiChungTu
ORDER BY cast(chungtu.dNgayChungTu AS date) END
;
;



GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_rpt_get_donvi_nBHXH_lns]    Script Date: 11/24/2023 5:04:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[sp_qtc_rpt_get_donvi_nBHXH_lns]
@NamLamViec int,
@LoaiChungTu int
AS BEGIN
SET NOCOUNT ON;

SELECT 
	donvi.iID_DonVi AS Id,
	donvi.iID_MaDonVi as IIDMaDonVi,
	donvi.sTenDonVi as TenDonVi,
	donvi.sKyHieu as KyHieu,
	donvi.sMoTa as MoTa,
	donvi.iLoai as Loai,
	donvi.iNamLamViec as NamLamViec,
	donvi.iTrangThai as iTrangThai,
	donvi.dNgayTao as DNgayTao,
	donvi.sNguoiTao as SNguoiTao,
	donvi.dNgaySua as DNgaySua,
	donvi.dNgaySua as SNguoiSua,
	donvi.*

FROM BH_QTC_Nam_CheDoBHXH chungtu
LEFT JOIN DonVi donvi ON chungtu.iID_MaDonVi = donvi.iID_MaDonVi
WHERE chungtu.iNamChungTu = @NamLamViec
  AND chungtu.iID_MaDonVi <> ''
  AND chungtu.iID_MaDonVi IS NOT NULL
  AND donvi.iNamLamViec = @NamLamViec
  --AND chungtu.iLoaiTongHop=@LoaiChungTu
ORDER BY cast(chungtu.dNgayChungTu AS date) END
;
;


GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_rpt_get_donvi_nKCB_lns]    Script Date: 11/24/2023 5:04:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[sp_qtc_rpt_get_donvi_nKCB_lns]
@NamLamViec int,
@LoaiChungTu int
AS BEGIN
SET NOCOUNT ON;

SELECT 
	donvi.iID_DonVi AS Id,
	donvi.iID_MaDonVi as IIDMaDonVi,
	donvi.sTenDonVi as TenDonVi,
	donvi.sKyHieu as KyHieu,
	donvi.sMoTa as MoTa,
	donvi.iLoai as Loai,
	donvi.iNamLamViec as NamLamViec,
	donvi.iTrangThai as iTrangThai,
	donvi.dNgayTao as DNgayTao,
	donvi.sNguoiTao as SNguoiTao,
	donvi.dNgaySua as DNgaySua,
	donvi.dNgaySua as SNguoiSua,
	donvi.*

FROM BH_QTC_Nam_KCB_QuanYDonVi chungtu
LEFT JOIN DonVi donvi ON chungtu.iID_MaDonVi = donvi.iID_MaDonVi
WHERE chungtu.iNamChungTu = @NamLamViec
  AND chungtu.iID_MaDonVi <> ''
  AND chungtu.iID_MaDonVi IS NOT NULL
  AND donvi.iNamLamViec = @NamLamViec
  --AND chungtu.iLoaiTongHop=@LoaiChungTu
ORDER BY cast(chungtu.dNgayChungTu AS date) END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_rpt_get_donvi_nKPK_lns]    Script Date: 11/24/2023 5:04:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_qtc_rpt_get_donvi_nKPK_lns]
@NamLamViec int,
@LoaiChungTu int,
@LoaiChi uniqueidentifier
AS BEGIN
SET NOCOUNT ON;

SELECT 
	donvi.iID_DonVi AS Id,
	donvi.iID_MaDonVi as IIDMaDonVi,
	donvi.sTenDonVi as TenDonVi,
	donvi.sKyHieu as KyHieu,
	donvi.sMoTa as MoTa,
	donvi.iLoai as Loai,
	donvi.iNamLamViec as NamLamViec,
	donvi.iTrangThai as iTrangThai,
	donvi.dNgayTao as DNgayTao,
	donvi.sNguoiTao as SNguoiTao,
	donvi.dNgaySua as DNgaySua,
	donvi.dNgaySua as SNguoiSua,
	donvi.*

FROM BH_QTC_Nam_KPK chungtu
LEFT JOIN DonVi donvi ON chungtu.iID_MaDonVi = donvi.iID_MaDonVi
WHERE chungtu.iNamChungTu = @NamLamViec
  AND chungtu.iID_MaDonVi <> ''
  AND chungtu.iID_MaDonVi IS NOT NULL
  AND donvi.iNamLamViec = @NamLamViec
  --AND chungtu.iLoaiTongHop=@LoaiChungTu
  AND chungtu.iID_LoaiChi=@LoaiChi
ORDER BY cast(chungtu.dNgayChungTu AS date) END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_rpt_get_donvi_nKPQL_lns]    Script Date: 11/24/2023 5:04:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[sp_qtc_rpt_get_donvi_nKPQL_lns]
@NamLamViec int,
@LoaiChungTu int
AS BEGIN
SET NOCOUNT ON;

SELECT 
	donvi.iID_DonVi AS Id,
	donvi.iID_MaDonVi as IIDMaDonVi,
	donvi.sTenDonVi as TenDonVi,
	donvi.sKyHieu as KyHieu,
	donvi.sMoTa as MoTa,
	donvi.iLoai as Loai,
	donvi.iNamLamViec as NamLamViec,
	donvi.iTrangThai as iTrangThai,
	donvi.dNgayTao as DNgayTao,
	donvi.sNguoiTao as SNguoiTao,
	donvi.dNgaySua as DNgaySua,
	donvi.dNgaySua as SNguoiSua,
	donvi.*

FROM BH_QTC_Nam_KinhPhiQuanLy chungtu
LEFT JOIN DonVi donvi ON chungtu.iID_MaDonVi = donvi.iID_MaDonVi
WHERE chungtu.iNamChungTu = @NamLamViec
  AND chungtu.iID_MaDonVi <> ''
  AND chungtu.iID_MaDonVi IS NOT NULL
  AND donvi.iNamLamViec = @NamLamViec
 -- AND chungtu.iLoaiTongHop=@LoaiChungTu
ORDER BY cast(chungtu.dNgayChungTu AS date) END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_rpt_get_donvi_qBHXH_lns]    Script Date: 11/24/2023 5:04:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[sp_qtc_rpt_get_donvi_qBHXH_lns]
@NamLamViec int,
@Quy int,
@LoaiChungTu int
AS BEGIN
SET NOCOUNT ON;

SELECT 
	donvi.iID_DonVi AS Id,
	donvi.iID_MaDonVi as IIDMaDonVi,
	donvi.sTenDonVi as TenDonVi,
	donvi.sKyHieu as KyHieu,
	donvi.sMoTa as MoTa,
	donvi.iLoai as Loai,
	donvi.iNamLamViec as NamLamViec,
	donvi.iTrangThai as iTrangThai,
	donvi.dNgayTao as DNgayTao,
	donvi.sNguoiTao as SNguoiTao,
	donvi.dNgaySua as DNgaySua,
	donvi.dNgaySua as SNguoiSua,
	donvi.*

FROM BH_QTC_Quy_CheDoBHXH chungtu
LEFT JOIN DonVi donvi ON chungtu.iID_MaDonVi = donvi.iID_MaDonVi
WHERE chungtu.iNamChungTu = @NamLamViec
  AND chungtu.iQuyChungTu=@Quy
  AND chungtu.iID_MaDonVi <> ''
  AND chungtu.iID_MaDonVi IS NOT NULL
  AND donvi.iNamLamViec = @NamLamViec
  --AND chungtu.iLoaiTongHop=@LoaiChungTu
ORDER BY cast(chungtu.dNgayChungTu AS date) END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_rpt_get_donvi_qkcb_lns]    Script Date: 11/24/2023 5:04:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[sp_qtc_rpt_get_donvi_qkcb_lns]
@NamLamViec int,
@Quy int,
@LoaiChungTu int
AS BEGIN
SET NOCOUNT ON;

SELECT 
	donvi.iID_DonVi AS Id,
	donvi.iID_MaDonVi as IIDMaDonVi,
	donvi.sTenDonVi as TenDonVi,
	donvi.sKyHieu as KyHieu,
	donvi.sMoTa as MoTa,
	donvi.iLoai as Loai,
	donvi.iNamLamViec as NamLamViec,
	donvi.iTrangThai as iTrangThai,
	donvi.dNgayTao as DNgayTao,
	donvi.sNguoiTao as SNguoiTao,
	donvi.dNgaySua as DNgaySua,
	donvi.dNgaySua as SNguoiSua,
	donvi.*

FROM BH_QTC_Quy_KCB chungtu
LEFT JOIN DonVi donvi ON chungtu.iID_MaDonVi = donvi.iID_MaDonVi
WHERE chungtu.iNamChungTu = @NamLamViec
  AND chungtu.iQuyChungTu=@Quy
  AND chungtu.iID_MaDonVi <> ''
  AND chungtu.iID_MaDonVi IS NOT NULL
  AND donvi.iNamLamViec = @NamLamViec
  --AND chungtu.iLoaiTongHop=@LoaiChungTu
ORDER BY cast(chungtu.dNgayChungTu AS date) END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_rpt_get_donvi_qKPK_lns]    Script Date: 11/24/2023 5:04:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[sp_qtc_rpt_get_donvi_qKPK_lns]
@NamLamViec int,
@Quy int,
@LoaiChungTu int,
@LoaiChi uniqueidentifier
AS BEGIN
SET NOCOUNT ON;

SELECT 
	donvi.iID_DonVi AS Id,
	donvi.iID_MaDonVi as IIDMaDonVi,
	donvi.sTenDonVi as TenDonVi,
	donvi.sKyHieu as KyHieu,
	donvi.sMoTa as MoTa,
	donvi.iLoai as Loai,
	donvi.iNamLamViec as NamLamViec,
	donvi.iTrangThai as iTrangThai,
	donvi.dNgayTao as DNgayTao,
	donvi.sNguoiTao as SNguoiTao,
	donvi.dNgaySua as DNgaySua,
	donvi.dNgaySua as SNguoiSua,
	donvi.*

FROM BH_QTC_Quy_KPK chungtu
LEFT JOIN DonVi donvi ON chungtu.iID_MaDonVi = donvi.iID_MaDonVi
WHERE chungtu.iNamChungTu = @NamLamViec
  AND chungtu.iQuyChungTu=@Quy
  AND chungtu.iID_MaDonVi <> ''
  AND chungtu.iID_MaDonVi IS NOT NULL
  AND donvi.iNamLamViec = @NamLamViec
 -- AND chungtu.iLoaiTongHop=@LoaiChungTu
  AND chungtu.iID_LoaiChi=@LoaiChi
ORDER BY cast(chungtu.dNgayChungTu AS date) END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_rpt_get_donvi_qKPQL_lns]    Script Date: 11/24/2023 5:04:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[sp_qtc_rpt_get_donvi_qKPQL_lns]
@NamLamViec int,
@Quy int,
@LoaiChungTu int
AS BEGIN
SET NOCOUNT ON;

SELECT 
	donvi.iID_DonVi AS Id,
	donvi.iID_MaDonVi as IIDMaDonVi,
	donvi.sTenDonVi as TenDonVi,
	donvi.sKyHieu as KyHieu,
	donvi.sMoTa as MoTa,
	donvi.iLoai as Loai,
	donvi.iNamLamViec as NamLamViec,
	donvi.iTrangThai as iTrangThai,
	donvi.dNgayTao as DNgayTao,
	donvi.sNguoiTao as SNguoiTao,
	donvi.dNgaySua as DNgaySua,
	donvi.dNgaySua as SNguoiSua,
	donvi.*

FROM BH_QTC_Quy_KinhPhiQuanLy chungtu
LEFT JOIN DonVi donvi ON chungtu.iID_MaDonVi = donvi.iID_MaDonVi
WHERE chungtu.iNamChungTu = @NamLamViec
  AND chungtu.iQuyChungTu=@Quy
  AND chungtu.iID_MaDonVi <> ''
  AND chungtu.iID_MaDonVi IS NOT NULL
  AND donvi.iNamLamViec = @NamLamViec
  --AND chungtu.iLoaiTongHop=@LoaiChungTu
ORDER BY cast(chungtu.dNgayChungTu AS date) END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_bhxh_chitiet]    Script Date: 11/24/2023 5:04:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay chung tu theo don vi và theo lns
CREATE PROCEDURE [dbo].[sp_rpt_khc_bhxh_chitiet]
	 @NamLamViec int,
	 @IdDonVi nvarchar(max),
	 @Dvt int
AS
BEGIN 
	SET NOCOUNT ON;
	-- lấy ra mlns theo phân cấp
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha
			into #tblMlnsByPhanCap
		FROM BH_DM_MucLucNganSach 
		WHERE 
		iNamLamViec = 2023 
		AND iTrangThai = 1
		AND (sLNS ='9010001' OR sLNS='9010002')

		SELECT 
		tblml.iID_MLNS as IID_MucLucNganSach,
		tblml.iID_MLNS_Cha as IdParent,
		tblml.sXauNoiMa  ,
		tblml.sLNS,
		tblml.sL,
		tblml.sK,
		tblml.sM,
		tblml.sTM,
		tblml.sTTM,
		tblml.sNG,
		tblml.sTNG,
		tblml.sTNG1,
		tblml.sTNG2,
		tblml.sTNG3,
		tblml.sMoTa,
		tblml.bHangCha,
		ISNULL(chitiet.fTienCNVQP, 0)/@Dvt fTienCNVQP,
		ISNULL(chitiet.fTienDaThucHienNamTruoc, 0)/@Dvt fTienDaThucHienNamTruoc,
		ISNULL(chitiet.fTienHSQBS, 0)/@Dvt fTienHSQBS,
		ISNULL(chitiet.fTienKeHoachThucHienNamNay, 0)/@Dvt fTienKeHoachThucHienNamNay,
		ISNULL(chitiet.fTienLDHD, 0)/@Dvt fTienLDHD,
		ISNULL(chitiet.fTienQNCN, 0)/@Dvt fTienQNCN,
		ISNULL(chitiet.fTienSQ, 0)/@Dvt fTienSQ,
		ISNULL(chitiet.fTienUocThucHienNamTruoc, 0)/@Dvt fTienUocThucHienNamTruoc,
		chitiet.iSoCNVQP,
		chitiet.iSoDaThucHienNamTruoc,
		chitiet.iSoHSQBS,
		chitiet.iSoKeHoachThucHienNamNay,
		chitiet.iSoLDHD,
		chitiet.iSoQNCN,
		chitiet.iSoSQ,
		chitiet.iSoUocThucHienNamTruoc,
		chitiet.sGhiChu
		
		FROM #tblMlnsByPhanCap tblml
		LEFT JOIN
		(SELECT CTCT.* FROM
			BH_KHC_CheDoBHXH_ChiTiet CTCT
			WHERE  CTCT.iID_KHC_CheDoBHXH IN
			(
				SELECT CT.ID
						FROM BH_KHC_CheDoBHXH CT
						WHERE CT.iID_MaDonVi=@IdDonVi
							AND CT.sSoChungTu <> ''
							AND CT.sSoChungTu IS NOT NULL
							AND CT.INamChungTu=@NamLamViec
			)) chitiet ON 

			chitiet.iID_MucLucNganSach=tblml.iID_MLNS

		ORDER BY tblml.sXauNoiMa

		drop table #tblMlnsByPhanCap
END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_chungtu_tonghop_bhxh]    Script Date: 11/24/2023 5:04:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_khc_chungtu_tonghop_bhxh]
	@listTenDonVi ntext,
	@namLamViec int,
	@iLoaiTongHop int
AS
BEGIN
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha
			INTO #tblMlnsByPhanCap
	FROM BH_DM_MucLucNganSach 
	WHERE 
		iNamLamViec = @namLamViec 
		AND iTrangThai = 1
		and (sLNS='9010001' or sLNS='9010002')

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
		mlns.sMoTa as SMoTa,
		mlns.bHangCha as IsHangCha,
		ct.fTienCNVQP,
		ct.fTienDaThucHienNamTruoc,
		ct.fTienHSQBS,
		ct.fTienKeHoachThucHienNamNay,
		ct.fTienLDHD,
		ct.fTienQNCN,
		ct.fTienSQ,
		ct.fTienUocThucHienNamTruoc,
		ct.iID_KHC_CheDoBHXH
		INTO #tempchitiet
		FROM
		#tblMlnsByPhanCap mlns
		left join 
		(SELECT * FROM BH_KHC_CheDoBHXH_ChiTiet 
				WHERE iID_KHC_CheDoBHXH in
					( SELECT ID FROM BH_KHC_CheDoBHXH
								WHERE iNamChungTu=@namLamViec
								AND iID_MaDonVi IN (select * from f_split(@listTenDonVi))
								)) ct
								On mlns.iID_MLNS=ct.iID_MucLucNganSach

		WHERE  ct.iID_KHC_CheDoBHXH is not null

		SELECT 
		dv.sTenDonVi ,
		dv.iID_MaDonVi as IDDonVi,
		tblct.SM,
		tblct.SLNS,
		SUM(tblct.fTienKeHoachThucHienNamNay) TienKeHoachThucHienNamNay
		FROM #tempchitiet tblct
		inner join BH_KHC_CheDoBHXH ct on ct.ID=tblct.iID_KHC_CheDoBHXH
		left join DonVi dv on ct.iID_DonVi=dv.iID_DonVi and ct.iID_MaDonVi=dv.iID_MaDonVi
		WHERE dv.iNamLamViec=2023
		GROUP BY dv.sTenDonVi,
				dv.iID_MaDonVi, 
				tblct.SLNS, 
				tblct.SM
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_kpql_chitiet]    Script Date: 11/24/2023 5:04:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay chung tu theo don vi và theo lns
CREATE PROCEDURE [dbo].[sp_rpt_khc_kpql_chitiet]
	 @NamLamViec int,
	 @IdDonVi nvarchar(max),
	 @Dvt int
AS
BEGIN 
	SET NOCOUNT ON;
	-- lấy ra mlns theo phân cấp
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha
			into #tblMlnsByPhanCap
		FROM BH_DM_MucLucNganSach 
		WHERE 
		iNamLamViec = 2023 
		AND iTrangThai = 1
		AND sLNS ='9010003'

		SELECT 
		tblml.iID_MLNS as IID_MucLucNganSach,
		tblml.iID_MLNS_Cha as IdParent,
		tblml.sXauNoiMa  ,
		tblml.sLNS,
		tblml.sL,
		tblml.sK,
		tblml.sM,
		tblml.sTM,
		tblml.sTTM,
		tblml.sNG,
		tblml.sTNG,
		tblml.sTNG1,
		tblml.sTNG2,
		tblml.sTNG3,
		tblml.sMoTa as SNoiDung,
		tblml.bHangCha,
		ISNULL(chitiet.fTienDaThucHienNamTruoc, 0)/@Dvt FTienDaThucHienNamTruoc,
		ISNULL(chitiet.fTienUocThucHienNamTruoc, 0)/@Dvt FTienUocThucHienNamTruoc,
		ISNULL(chitiet.fTienKeHoachThucHienNamNay, 0)/@Dvt FTienKeHoachThucHienNamNay,
		ISNULL(chitiet.fTienCanBo, 0)/@Dvt FTienCanBo,
		ISNULL(chitiet.fTienQuanLuc, 0)/@Dvt FTienQuanLuc,
		ISNULL(chitiet.fTienTaiChinh, 0)/@Dvt FTienTaiChinh,
		ISNULL(chitiet.fTienQuanY, 0)/@Dvt FTienQuanY,
		chitiet.sGhiChu
		
		FROM #tblMlnsByPhanCap tblml
		LEFT JOIN
		(SELECT CTCT.* FROM
			BH_KHC_KinhPhiQuanLy_ChiTiet CTCT
			WHERE  CTCT.iID_KHC_KinhPhiQuanLy IN
			(
				SELECT CT.iID_BH_KHC_KinhPhiQuanLy
						FROM BH_KHC_KinhPhiQuanLy CT
						WHERE CT.iID_MaDonVi=@IdDonVi
							AND CT.sSoChungTu <> ''
							AND CT.sSoChungTu IS NOT NULL
							AND CT.INamChungTu=@NamLamViec
			)) chitiet ON 

			chitiet.iID_MucLucNganSach=tblml.iID_MLNS

		ORDER BY tblml.sXauNoiMa

		drop table #tblMlnsByPhanCap
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qbhxh_thongtriloai1_bh]    Script Date: 11/24/2023 5:04:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay chung tu theo don vi và theo lns
CREATE PROCEDURE [dbo].[sp_rpt_qtc_qbhxh_thongtriloai1_bh]
	 @NamLamViec int,
	 @iQuy nvarchar(100),
	 @IdDonVi nvarchar(max),
	 @UserName nvarchar(100),
	 @iLoaiTongHop int,
	 @Dvt int
AS
BEGIN 
	SET NOCOUNT ON;

	IF @iLoaiTongHop=1
	BEGIN
	SELECT  ct.iID_MaDonVi as SMaDonVi,
		ISNULL(ct.fTongTien_DeNghi,0)/ @Dvt FTongTienDeNghi,
		ct.iID_DonVi as IID_DonVi,
		dv.sTenDonVi as STenDonVi
		FROM BH_QTC_Quy_CheDoBHXH ct
		LEFT JOIN DonVi dv ON ct.iID_DonVi=dv.iID_DonVi
			WHERE ct.iNamChungTu=@NamLamViec
				    AND ct.iQuyChungTu=@iQuy
					--AND ct.bIsKhoa=1
					--AND ct.iLoaiTongHop=@iLoaiTongHop
					AND ct.iID_MaDonVi IN (select * from f_split(@IdDonVi))
	END
	ELSE
	BEGIN
	SELECT  ct.iID_MaDonVi as SMaDonVi,
		ISNULL(ct.fTongTien_DeNghi,0)/ @Dvt FTongTienDeNghi,
		ct.iID_DonVi as IID_DonVi,
		dv.sTenDonVi as STenDonVi
		FROM BH_QTC_Quy_CheDoBHXH ct
		LEFT JOIN DonVi dv ON ct.iID_DonVi=dv.iID_DonVi
			WHERE ct.iNamChungTu=@NamLamViec
				    AND ct.iQuyChungTu=@iQuy
					--AND ct.bIsKhoa=1
					--AND ct.iLoaiTongHop=@iLoaiTongHop
					AND ct.iID_MaDonVi IN (select * from f_split(@IdDonVi))
	END

END
;
;


GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkcb_thongtriloai1_bh]    Script Date: 11/24/2023 5:04:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay chung tu theo don vi và theo lns
CREATE PROCEDURE [dbo].[sp_rpt_qtc_qkcb_thongtriloai1_bh]
	 @NamLamViec int,
	 @iQuy nvarchar(100),
	 @IdDonVi nvarchar(max),
	 @UserName nvarchar(100),
	 @iLoaiTongHop int,
	 @Dvt int
AS
BEGIN 
	SET NOCOUNT ON;

	IF @iLoaiTongHop=1
	BEGIN
	SELECT  ct.iID_MaDonVi as SMaDonVi,
		ISNULL(ct.fTongTienDeNghiQuyetToanQuyNay,0)/ @Dvt FTongTienDeNghi,
		ct.iID_DonVi as IID_DonVi,
		dv.sTenDonVi as STenDonVi
		FROM BH_QTC_Quy_KCB ct
		LEFT JOIN DonVi dv ON ct.iID_DonVi=dv.iID_DonVi
			WHERE ct.iNamChungTu=@NamLamViec
				    AND ct.iQuyChungTu=@iQuy
					--AND ct.bIsKhoa=1
					--AND ct.iLoaiTongHop=@iLoaiTongHop
					AND ct.iID_MaDonVi IN (select * from f_split(@IdDonVi))
	END
	ELSE
	BEGIN
	SELECT  ct.iID_MaDonVi as SMaDonVi,
		ISNULL(ct.fTongTienDeNghiQuyetToanQuyNay,0)/ @Dvt FTongTienDeNghi,
		ct.iID_DonVi as IID_DonVi,
		dv.sTenDonVi as STenDonVi
		FROM BH_QTC_Quy_KCB ct
		LEFT JOIN DonVi dv ON ct.iID_DonVi=dv.iID_DonVi
			WHERE ct.iNamChungTu=@NamLamViec
				    AND ct.iQuyChungTu=@iQuy
					--AND ct.bIsKhoa=1
					--AND ct.iLoaiTongHop=@iLoaiTongHop
					AND ct.iID_MaDonVi IN (select * from f_split(@IdDonVi))
	END

END
;
;


GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkp_ql_thongtriloai1_bh]    Script Date: 11/24/2023 5:04:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay chung tu theo don vi và theo lns
CREATE PROCEDURE [dbo].[sp_rpt_qtc_qkp_ql_thongtriloai1_bh]
	 @NamLamViec int,
	 @iQuy nvarchar(100),
	 @IdDonVi nvarchar(max),
	 @UserName nvarchar(100),
	 @iLoaiTongHop int,
	 @Dvt int
AS
BEGIN 
	SET NOCOUNT ON;

	IF @iLoaiTongHop=1
	BEGIN
	SELECT  ct.iID_MaDonVi as SMaDonVi,
		ISNULL(ct.fTongTienDeNghiQuyetToanQuyNay,0)/ @Dvt FTongTienDeNghi,
		ct.iID_DonVi as IID_DonVi,
		dv.sTenDonVi as STenDonVi
		FROM BH_QTC_Quy_KinhPhiQuanLy ct
		LEFT JOIN DonVi dv ON ct.iID_DonVi=dv.iID_DonVi
			WHERE ct.iNamChungTu=@NamLamViec
				    AND ct.iQuyChungTu=@iQuy
					--AND ct.bIsKhoa=1
					--AND ct.iLoaiTongHop=@iLoaiTongHop
					AND ct.iID_MaDonVi IN (select * from f_split(@IdDonVi))
	END
	ELSE
	BEGIN
	SELECT  ct.iID_MaDonVi as SMaDonVi,
		ISNULL(ct.fTongTienDeNghiQuyetToanQuyNay,0)/ @Dvt FTongTienDeNghi,
		ct.iID_DonVi as IID_DonVi,
		dv.sTenDonVi as STenDonVi
		FROM BH_QTC_Quy_KinhPhiQuanLy ct
		LEFT JOIN DonVi dv ON ct.iID_DonVi=dv.iID_DonVi
			WHERE ct.iNamChungTu=@NamLamViec
				    AND ct.iQuyChungTu=@iQuy
					--AND ct.bIsKhoa=1
					--AND ct.iLoaiTongHop=@iLoaiTongHop
					AND ct.iID_MaDonVi IN (select * from f_split(@IdDonVi))
	END

END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkp_ql_thongtriloai2_bh]    Script Date: 11/24/2023 5:04:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay chung tu theo don vi và theo lns
CREATE PROCEDURE [dbo].[sp_rpt_qtc_qkp_ql_thongtriloai2_bh]
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
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha
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
		mlns.sMoTa as SMoTa,
		mlns.bHangCha as IsHangCha,
		ISNULL(ctct.fTienDeNghiQuyetToanQuyNay, 0) / @Dvt as FTienDeNghiQuyetToanQuyNay 
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
		mlns.sMoTa as SMoTa,
		mlns.bHangCha as IsHangCha,
		ISNULL(ctct.fTienXacNhanQuyetToanQuyNay, 0) / @Dvt as FTienXacNhanQuyetToanQuyNay 
	FROM 
		#tblMlnsByPhanCap mlns
	LEFT JOIN 
		(SELECT * FROM BH_QTC_Quy_KinhPhiQuanLy_ChiTiet 
				WHERE iID_QTC_Quy_KinhPhiQuanLy in
					( SELECT ID_QTC_Quy_KinhPhiQuanLy FROM BH_QTC_Quy_KinhPhiQuanLy
								WHERE iNamChungTu=@NamLamViec
								AND iQuyChungTu=@iQuy
								AND iLoaiTongHop=@LoaiTongHop
								AND iID_MaDonVi IN (select * from f_split(@IdDonVi))
								)) ctct
	on mlns.iID_MLNS = ctct.iID_MucLucNganSach 
	Order by mlns.sXauNoiMa
END
END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkpk_thongtriloai1_bh]    Script Date: 11/24/2023 5:04:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay báo cáo thong tri theo don vi
CREATE PROCEDURE [dbo].[sp_rpt_qtc_qkpk_thongtriloai1_bh]
	 @NamLamViec int,
	 @iQuy nvarchar(100),
	 @IdDonVi nvarchar(max),
	 @UserName nvarchar(100),
	 @iLoaiTongHop int,
	 @IDLoaichi uniqueidentifier,
	 @Dvt int
AS
BEGIN 
	SET NOCOUNT ON;
IF @iLoaiTongHop=1
BEGIN
	SELECT 
				A.IID_DonVi,
				A.FTongTienDeNghiQuyetToanQuyNay,
				A.sLNS,
				A.SMaDonVi,
				dv.sTenDonVi,
				dv.iLoai
					FROM
					(SELECT 
					ct.iID_MaDonVi as SMaDonVi,
					ISNULL(Sum(ctct.fTienDeNghiQuyetToanQuyNay),0)/ @Dvt FTongTienDeNghiQuyetToanQuyNay,
					ct.iID_DonVi as IID_DonVi,
					dm.sLNS
					FROM BH_QTC_Quy_KPK ct
					LEFT JOIN BH_QTC_Quy_KPK_ChiTiet ctct
					ON ct.ID_QTC_Quy_KPK=ctct.iID_QTC_Quy_KPK
					LEFT JOIN BH_DM_MucLucNganSach dm on ctct.iID_MucLucNganSach=dm.iID_MLNS
					WHERE ct.iNamChungTu=@NamLamViec
				    AND ct.iQuyChungTu=@iQuy
					AND ct.iID_LoaiChi=@IDLoaichi
					--AND ct.bIsKhoa=1
					--AND ct.iLoaiTongHop=@iLoaiTongHop
					AND ct.iID_MaDonVi IN (select * from f_split(@IdDonVi)
					)
					group by  ct.iID_MaDonVi,ct.iID_DonVi,dm.sLNS
					) A
					LEFT  JOIN DonVi dv on A.iID_DonVi= dv.iID_DonVi
					
END
ELSE
BEGIN
			SELECT 
				A.IID_DonVi,
				A.FTongTienDeNghiQuyetToanQuyNay,
				A.sLNS,
				A.SMaDonVi,
				dv.sTenDonVi,
				dv.iLoai
					FROM
					(SELECT 
					ct.iID_MaDonVi as SMaDonVi,
					ISNULL(Sum(ctct.fTienDeNghiQuyetToanQuyNay),0)/ @Dvt FTongTienDeNghiQuyetToanQuyNay,
					ct.iID_DonVi as IID_DonVi,
					dm.sLNS
					FROM BH_QTC_Quy_KPK ct
					LEFT JOIN BH_QTC_Quy_KPK_ChiTiet ctct
					ON ct.ID_QTC_Quy_KPK=ctct.iID_QTC_Quy_KPK
					LEFT JOIN BH_DM_MucLucNganSach dm on ctct.iID_MucLucNganSach=dm.iID_MLNS
					WHERE ct.iNamChungTu=@NamLamViec
				    AND ct.iQuyChungTu=@iQuy
					AND ct.iID_LoaiChi=@IDLoaichi
					--AND ct.bIsKhoa=1
					--AND ct.iLoaiTongHop=@iLoaiTongHop
					AND ct.iID_MaDonVi IN (select * from f_split(@IdDonVi))
					group by  ct.iID_MaDonVi,ct.iID_DonVi,dm.sLNS) A
					LEFT  JOIN DonVi dv on A.iID_DonVi= dv.iID_DonVi
END

END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkpk_thongtriloai2_bh]    Script Date: 11/24/2023 5:04:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay chung tu theo don vi và theo lns
CREATE PROCEDURE [dbo].[sp_rpt_qtc_qkpk_thongtriloai2_bh]
	 @NamLamViec int,
	 @iQuy nvarchar(100),
	 @IdDonVi nvarchar(max),
	 @LNS nvarchar(max),
	 @UserName nvarchar(100),
	 @IdLoaiChi uniqueidentifier,
	 @LoaiTongHop int ,
	 @Dvt int
AS
BEGIN 
	SET NOCOUNT ON;
	-- lấy ra mlns theo phân cấp
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha
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
		mlns.sMoTa as SMoTa,
		mlns.bHangCha as IsHangCha,
		ISNULL(ctct.fTienDeNghiQuyetToanQuyNay, 0) / @Dvt as FTienDeNghiQuyetToanQuyNay 
	FROM 
		#tblMlnsByPhanCap mlns
	LEFT JOIN 
		(SELECT * FROM BH_QTC_Quy_KPK_ChiTiet 
				WHERE iID_QTC_Quy_KPK in
					( SELECT ID_QTC_Quy_KPK FROM BH_QTC_Quy_KPK
								WHERE iNamChungTu=@NamLamViec
								AND iQuyChungTu=@iQuy
								AND iID_LoaiChi=@IdLoaiChi
								AND iID_MaDonVi IN (select * from f_split(@IdDonVi))
								)) ctct
	on mlns.iID_MLNS = ctct.iID_MucLucNganSach 
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
		mlns.sMoTa as SMoTa,
		mlns.bHangCha as IsHangCha,
		ISNULL(ctct.fTienDeNghiQuyetToanQuyNay, 0) / @Dvt as FTienDeNghiQuyetToanQuyNay 
	FROM 
		#tblMlnsByPhanCap mlns
	LEFT JOIN 
		(SELECT * FROM BH_QTC_Quy_KPK_ChiTiet 
				WHERE iID_QTC_Quy_KPK in
					( SELECT ID_QTC_Quy_KPK FROM BH_QTC_Quy_KPK
								WHERE iNamChungTu=@NamLamViec
								AND iQuyChungTu=@iQuy
								--AND bIsKhoa=1
								--AND iLoaiTongHop=@LoaiTongHop
								AND iID_LoaiChi=@IdLoaiChi
								AND iID_MaDonVi IN (select * from f_split(@IdDonVi))
								)) ctct
	on mlns.iID_MLNS = ctct.iID_MucLucNganSach 
	Order by mlns.sXauNoiMa
END 
END
;
;

GO
