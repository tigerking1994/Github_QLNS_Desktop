/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkpk_thongtriloai1_bh]    Script Date: 11/20/2023 11:28:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qtc_qkpk_thongtriloai1_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qtc_qkpk_thongtriloai1_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkpk_kehoach_bh]    Script Date: 11/20/2023 11:28:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qtc_qkpk_kehoach_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qtc_qkpk_kehoach_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkp_ql_thongtriloai1_bh]    Script Date: 11/20/2023 11:28:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qtc_qkp_ql_thongtriloai1_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qtc_qkp_ql_thongtriloai1_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkp_ql_chungtu_chi_tiet]    Script Date: 11/20/2023 11:28:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qtc_qkp_ql_chungtu_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qtc_qkp_ql_chungtu_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_nqlkp_kehoach_bh]    Script Date: 11/20/2023 11:28:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qtc_nqlkp_kehoach_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qtc_nqlkp_kehoach_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_nkpk_kehoach_bh]    Script Date: 11/20/2023 11:28:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qtc_nkpk_kehoach_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qtc_nkpk_kehoach_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_qkpql]    Script Date: 11/20/2023 11:28:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_get_lns_for_qkpql]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_get_lns_for_qkpql]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_qkpk]    Script Date: 11/20/2023 11:28:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_get_lns_for_qkpk]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_get_lns_for_qkpk]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_qkcb]    Script Date: 11/20/2023 11:28:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_get_lns_for_qkcb]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_get_lns_for_qkcb]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_qbhxh]    Script Date: 11/20/2023 11:28:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_get_lns_for_qbhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_get_lns_for_qbhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_nkpql]    Script Date: 11/20/2023 11:28:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_get_lns_for_nkpql]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_get_lns_for_nkpql]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_nkpk]    Script Date: 11/20/2023 11:28:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_get_lns_for_nkpk]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_get_lns_for_nkpk]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_nkcb]    Script Date: 11/20/2023 11:28:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_get_lns_for_nkcb]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_get_lns_for_nkcb]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_nbhxh]    Script Date: 11/20/2023 11:28:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_get_lns_for_nbhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_get_lns_for_nbhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_get_donvi_qKPQL_lns]    Script Date: 11/20/2023 11:28:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_rpt_get_donvi_qKPQL_lns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_rpt_get_donvi_qKPQL_lns]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_get_donvi_qKPK_lns]    Script Date: 11/20/2023 11:28:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_rpt_get_donvi_qKPK_lns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_rpt_get_donvi_qKPK_lns]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_get_donvi_qkcb_lns]    Script Date: 11/20/2023 11:28:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_rpt_get_donvi_qkcb_lns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_rpt_get_donvi_qkcb_lns]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_get_donvi_qBHXH_lns]    Script Date: 11/20/2023 11:28:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_rpt_get_donvi_qBHXH_lns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_rpt_get_donvi_qBHXH_lns]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_get_donvi_nKPQL_lns]    Script Date: 11/20/2023 11:28:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_rpt_get_donvi_nKPQL_lns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_rpt_get_donvi_nKPQL_lns]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_get_donvi_nKPK_lns]    Script Date: 11/20/2023 11:28:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_rpt_get_donvi_nKPK_lns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_rpt_get_donvi_nKPK_lns]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_get_donvi_nKCB_lns]    Script Date: 11/20/2023 11:28:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_rpt_get_donvi_nKCB_lns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_rpt_get_donvi_nKCB_lns]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_get_donvi_nBHXH_lns]    Script Date: 11/20/2023 11:28:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_rpt_get_donvi_nBHXH_lns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_rpt_get_donvi_nBHXH_lns]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_kcb]    Script Date: 11/20/2023 11:28:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_kcb]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_kcb]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_bhxh]    Script Date: 11/20/2023 11:28:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]    Script Date: 11/20/2023 11:28:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_bhxh]    Script Date: 11/20/2023 11:28:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqKPQL_create_datafor_quaterbefore]    Script Date: 11/20/2023 11:28:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtcqKPQL_create_datafor_quaterbefore]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtcqKPQL_create_datafor_quaterbefore]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqKPk_create_datafor_quaterbefore]    Script Date: 11/20/2023 11:28:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtcqKPk_create_datafor_quaterbefore]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtcqKPk_create_datafor_quaterbefore]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqKCB_update_total]    Script Date: 11/20/2023 11:28:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtcqKCB_update_total]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtcqKCB_update_total]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqKCB_create_datafor_quaterbefore]    Script Date: 11/20/2023 11:28:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtcqKCB_create_datafor_quaterbefore]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtcqKCB_create_datafor_quaterbefore]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqKCB_create_data_summary]    Script Date: 11/20/2023 11:28:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtcqKCB_create_data_summary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtcqKCB_create_data_summary]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqBHXH_create_datafor_quaterbefore]    Script Date: 11/20/2023 11:28:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtcqBHXH_create_datafor_quaterbefore]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtcqBHXH_create_datafor_quaterbefore]
GO
/****** Object:  StoredProcedure [dbo].[bh_kehoach_chikhac_index]    Script Date: 11/20/2023 11:28:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[bh_kehoach_chikhac_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[bh_kehoach_chikhac_index]
GO
/****** Object:  StoredProcedure [dbo].[bh_kehoach_chikhac_index]    Script Date: 11/20/2023 11:28:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Doantc
-- Create date: 13/06/2023
-- Description:	Lấy danh sách hiển thị lâp kế hoạch chi các chế dộ BHXH

-- =============================================
CREATE PROCEDURE [dbo].[bh_kehoach_chikhac_index]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
	DISTINCT 
		 kcb.iID_BH_KHC_K
		, kcb.sSoChungTu
		, kcb.dNgayChungTu
		, kcb.sSoQuyetDinh
		, kcb.dNgayQuyetDinh
		, kcb.iNamChungTu
		, kcb.iID_DonVi
		, kcb.iID_MaDonVi
		, kcb.sMoTa
		, kcb.iIDLoaiChi
		, kcb.fTongTienDaThucHienNamTruoc
		, kcb.fTongTienUocThucHienNamTruoc
		, kcb.fTongTienKeHoachThucHienNamNay
		, kcb.sTongHop
		, kcb.iID_TongHopID
		, kcb.iLoaiTongHop
		, kcb.bIsKhoa
		, kcb.dNgaySua
		, kcb.dNgayTao
		, kcb.sNguoiSua
		, kcb.sNguoiTao
		, kcb.dNgayTao
		, donVi.sTenDonVi
		, dm.sTenDanhMucLoaiChi
		-- Tong dự toán todo
	FROM BH_KHC_K kcb
	LEFT JOIN DonVi donVi
		ON kcb.iID_MaDonVi = donVi.iID_MaDonVi AND donVi.iID_DonVi = kcb.iID_DonVi
	LEFT JOIN BH_DM_LoaiChi dm ON kcb.iIDLoaiChi=dm.iID
	ORDER BY kcb.sSoChungTu
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqBHXH_create_datafor_quaterbefore]    Script Date: 11/20/2023 11:28:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtcqBHXH_create_datafor_quaterbefore]
@IdChungTu nvarchar(MAX),
@Username nvarchar(500),
@NamLamViec int,
@Quy int,
@MaDonVi nvarchar(MAX)
AS
BEGIN
SET NOCOUNT ON;

	INSERT INTO BH_QTC_Quy_CheDoBHXH_ChiTiet
	( 
		ID_QTC_Quy_CheDoBHXH_ChiTiet,
		iID_QTC_Quy_CheDoBHXH,
		iID_MucLucNganSach,
		iNamChungTu,
		sLoaiTroCap,
		dNgayTao,
		sNguoiTao,
		fTienLuyKeCuoiQuyNay	
	)
	SELECT 
		NEWID(),
		@IdChungTu,
		qtcn_ct.iID_MucLucNganSach,
		@NamLamViec,
		qtcn_ct.sLoaiTroCap,
		GETDATE(),
		@Username,
		SUM(qtcn_ct.fTongTien_DeNghi)
	FROM BH_QTC_Quy_CheDoBHXH_ChiTiet qtcn_ct
		inner join BH_QTC_Quy_CheDoBHXH  as qtcn on qtcn_ct.iID_QTC_Quy_CheDoBHXH = qtcn.ID_QTC_Quy_CheDoBHXH
	WHERE qtcn.iQuyChungTu < @Quy
			AND qtcn.iID_MaDonVi = @MaDonVi
			AND qtcn.iNamChungTu = @NamLamViec
			--AND qtcn.bIsKhoa=1
		GROUP BY qtcn_ct.iID_MucLucNganSach, qtcn_ct.sLoaiTroCap
END
;
;


GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqKCB_create_data_summary]    Script Date: 11/20/2023 11:28:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtcqKCB_create_data_summary]
@IdChungTu nvarchar(MAX),
@NguoiTao nvarchar(500),
@YearOfWork int,
@IdChungTuSummary nvarchar(MAX)
AS
BEGIN
SET NOCOUNT ON;

INSERT INTO BH_QTC_Quy_KCB_ChiTiet(
		ID_QTC_Quy_KCB_ChiTiet,
		iID_QTC_Quy_KCB,
		iID_MucLucNganSach,
		sNoiDung,
		dNgaySua,
		dNgayTao,
		sNguoiSua,
		sNguoiTao,
		fTien_DuToanNamTruocChuyenSang,
		fTien_DuToanGiaoNamNay,
		fTien_TongDuToanDuocGiao,
		fTienThucChi,
		fTienQuyetToanDaDuyet,
		fTienDeNghiQuyetToanQuyNay,
		fTienXacNhanQuyetToanQuyNay
	)
SELECT 
	   NEWID(),
	   @IdChungTuSummary,
       iID_MucLucNganSach,
       sNoiDung,
	   GETDATE(),
	   GETDATE(),
	   @NguoiTao,
	   '',
	   SUM(fTien_DuToanNamTruocChuyenSang),
	   SUM(fTien_DuToanGiaoNamNay),
	   SUM(fTien_TongDuToanDuocGiao),
	   SUM(fTienThucChi),
	   SUM(fTienQuyetToanDaDuyet),
	   SUM(fTienDeNghiQuyetToanQuyNay),
	    SUM(fTienXacNhanQuyetToanQuyNay)
FROM BH_QTC_Quy_KCB_ChiTiet
WHERE  iID_QTC_Quy_KCB IN
    (SELECT *
     FROM f_split(@IdChungTu))
group by iID_MucLucNganSach, sNoiDung

UPDATE BH_QTC_Quy_KCB SET bDaTongHop = 1, iLoaiTongHop = 2 WHERE ID_QTC_Quy_KCB IN (SELECT * FROM f_split(@IdChungTu));
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqKCB_create_datafor_quaterbefore]    Script Date: 11/20/2023 11:28:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtcqKCB_create_datafor_quaterbefore]
@IdChungTu nvarchar(MAX),
@Username nvarchar(500),
@NamLamViec int,
@Quy int,
@MaDonVi nvarchar(MAX)
AS
BEGIN
SET NOCOUNT ON;

	INSERT INTO BH_QTC_Quy_KCB_ChiTiet
	( 
		ID_QTC_Quy_KCB_ChiTiet,
		iID_QTC_Quy_KCB,
		iID_MucLucNganSach,
		sNoiDung,
		dNgayTao,
		sNguoiTao,
		fTienQuyetToanDaDuyet
	)
	SELECT 
		NEWID(),
		@IdChungTu,
		qtcn_ct.iID_MucLucNganSach,
		qtcn_ct.sNoiDung,
		GETDATE(),
		@Username,
		SUM(qtcn_ct.fTienXacNhanQuyetToanQuyNay)
	FROM BH_QTC_Quy_KCB_ChiTiet qtcn_ct
		inner join BH_QTC_Quy_KCB  as qtcn on qtcn_ct.iID_QTC_Quy_KCB = qtcn.ID_QTC_Quy_KCB
	WHERE qtcn.iQuyChungTu < @Quy
			AND qtcn.iID_MaDonVi = @MaDonVi
			AND qtcn.iNamChungTu = @NamLamViec
			--AND qtcn.bIsKhoa=1
		GROUP BY qtcn_ct.iID_MucLucNganSach, qtcn_ct.sNoiDung
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqKCB_update_total]    Script Date: 11/20/2023 11:28:09 AM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqKPk_create_datafor_quaterbefore]    Script Date: 11/20/2023 11:28:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
cREATE PROCEDURE [dbo].[sp_bh_qtcqKPk_create_datafor_quaterbefore]
@IdChungTu nvarchar(MAX),
@Username nvarchar(500),
@NamLamViec int,
@Quy int,
@LoaiChi uniqueidentifier,
@MaDonVi nvarchar(MAX)
AS
BEGIN
SET NOCOUNT ON;

	INSERT INTO BH_QTC_Quy_KPK_ChiTiet
	( 
		ID_QTC_Quy_KPK_ChiTiet,
		iID_QTC_Quy_KPK,
		iID_MucLucNganSach,
		sNoiDung,
		dNgayTao,
		sNguoiTao,
		fTienQuyetToanDaDuyet
	)
	SELECT 
		NEWID(),
		@IdChungTu,
		qtcn_ct.iID_MucLucNganSach,
		qtcn_ct.sNoiDung,
		GETDATE(),
		@Username,
		SUM(qtcn_ct.fTienXacNhanQuyetToanQuyNay)
	FROM BH_QTC_Quy_KPK_ChiTiet qtcn_ct
		inner join BH_QTC_Quy_KPK  as qtcn on qtcn_ct.iID_QTC_Quy_KPK = qtcn.ID_QTC_Quy_KPK
	WHERE qtcn.iQuyChungTu < @Quy
			AND qtcn.iID_MaDonVi = @MaDonVi
			AND qtcn.iNamChungTu = @NamLamViec
			AND qtcn.bIsKhoa=1
			AND iID_LoaiChi=@LoaiChi
		GROUP BY qtcn_ct.iID_MucLucNganSach, qtcn_ct.sNoiDung
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqKPQL_create_datafor_quaterbefore]    Script Date: 11/20/2023 11:28:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtcqKPQL_create_datafor_quaterbefore]
@IdChungTu nvarchar(MAX),
@Username nvarchar(500),
@NamLamViec int,
@Quy int,
@MaDonVi nvarchar(MAX)
AS
BEGIN
SET NOCOUNT ON;

	INSERT INTO BH_QTC_Quy_KinhPhiQuanLy_ChiTiet
	( 
		ID_QTC_Quy_KinhPhiQuanLy_ChiTiet,
		iID_QTC_Quy_KinhPhiQuanLy,
		iID_MucLucNganSach,
		sNoiDung,
		dNgayTao,
		sNguoiTao,
		fTienQuyetToanDaDuyet
	)
	SELECT 
		NEWID(),
		@IdChungTu,
		qtcn_ct.iID_MucLucNganSach,
		qtcn_ct.sNoiDung,
		GETDATE(),
		@Username,
		SUM(qtcn_ct.fTienXacNhanQuyetToanQuyNay)
	FROM BH_QTC_Quy_KinhPhiQuanLy_ChiTiet qtcn_ct
		inner join BH_QTC_Quy_KinhPhiQuanLy  as qtcn on qtcn_ct.iID_QTC_Quy_KinhPhiQuanLy = qtcn.ID_QTC_Quy_KinhPhiQuanLy
	WHERE qtcn.iQuyChungTu < @Quy
			AND qtcn.iID_MaDonVi = @MaDonVi
			AND qtcn.iNamChungTu = @NamLamViec
			--AND qtcn.bIsKhoa=1
		GROUP BY qtcn_ct.iID_MucLucNganSach, qtcn_ct.sNoiDung
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_bhxh]    Script Date: 11/20/2023 11:28:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_bhxh]
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
			danhmuc.bHangCha
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
		and qtcn.iNamChungTu = @INamLamViec
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
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]    Script Date: 11/20/2023 11:28:09 AM ******/
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
		where danhmuc.iNamLamViec = @INamLamViec and danhmuc.sLNs in (select * from splitstring(@Lns))


	---Lấy danh sách chi tiết
		select	
			qtcn_ct.iID_MucLucNganSach,
			qtcn_ct.sNoiDung,
			Sum(qtcn_ct.fTien_DuToanNamTruocChuyenSang) as fTien_DuToanNamTruocChuyenSang,
			Sum(qtcn_ct.fTien_DuToanGiaoNamNay) as fTien_DuToanGiaoNamNay,
			Sum(qtcn_ct.fTien_TongDuToanDuocGiao) as fTien_TongDuToanDuocGiao,
			Sum(qtcn_ct.fTien_ThucChi) as fTien_ThucChi,
			Sum(qtcn_ct.fTien_TongDuToanDuocGiao) - Sum(qtcn_ct.fTien_ThucChi)  as fTienThua,
			Sum(qtcn_ct.fTien_ThucChi) - Sum(qtcn_ct.fTien_TongDuToanDuocGiao) as fTienThieu,
			Sum(qtcn_ct.fTiLeThucHienTrenDuToan) as fTiLeThucHienTrenDuToan

		into #tbl_qtcn_chitiet
		from BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet as  qtcn_ct
		inner join  BH_QTC_Nam_KCB_QuanYDonVi as qtcn on qtcn_ct.iID_QTC_Nam_KCB_QuanYDonVi = qtcn.ID_QTC_Nam_KCB_QuanYDonVi
		where qtcn.iID_MaDonVi in (select * from dbo.splitstring(@IdMaDonVi)) 
		and qtcn.iNamChungTu = @INamLamViec
		--and ((@IsTongHop = 0 and qtcn.bDaTongHop = 0 and qtcn.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  qtcn.sDSSoChungTuTongHop is not null))
		group by qtcn_ct.iID_MucLucNganSach,qtcn_ct.sNoiDung



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
			chi_tiet.fTien_DuToanNamTruocChuyenSang, 
			chi_tiet.fTien_DuToanGiaoNamNay,
			chi_tiet.fTien_TongDuToanDuocGiao,
			chi_tiet.fTien_ThucChi,
			chi_tiet.fTienThua,
			chi_tiet.fTienThieu,
			chi_tiet.fTiLeThucHienTrenDuToan

		from #tblMucLucNganSach as mucluc
		left join #tbl_qtcn_chitiet as chi_tiet on mucluc.iID_MLNS = chi_tiet.iID_MucLucNganSach
		order by mucluc.sXauNoiMa


		drop table #tblMucLucNganSach;
		drop table #tbl_qtcn_chitiet;

end
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_bhxh]    Script Date: 11/20/2023 11:28:09 AM ******/
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
			danhmuc.bHangCha
		into #tblMucLucNganSach
		from BH_DM_MucLucNganSach as danhmuc
		where danhmuc.iNamLamViec = @INamLamViec and danhmuc.sLNs  in (select * from dbo.splitstring(@SLN)) 
		
		
		
	---Lấy thông tin chi tiết chứng từ
		select 
			qtcn_ct.iID_MucLucNganSach,
			qtcn_ct.sLoaiTroCap,
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
			mucluc.sMoTa as sLoaiTroCap,
			chi_tiet.fTienDuToanDuyet,
			chi_tiet.iSoLuyKeCuoiQuyNay,
			chi_tiet.fTienLuyKeCuoiQuyNay,
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
		left join #tblQuyetToanQuyChiTiet as chi_tiet on mucluc.iID_MLNS = chi_tiet.iID_MucLucNganSach
		order by mucluc.sXauNoiMa
	
	drop table #tblMucLucNganSach;
	drop table #tblQuyetToanQuyChiTiet;
end


GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_kcb]    Script Date: 11/20/2023 11:28:09 AM ******/
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
			Sum(qtcn_ct.fTien_DuToanNamTruocChuyenSang) as fTien_DuToanNamTruocChuyenSang,
			Sum(qtcn_ct.fTien_DuToanGiaoNamNay) as fTien_DuToanGiaoNamNay,
			Sum(qtcn_ct.fTien_TongDuToanDuocGiao) as fTien_TongDuToanDuocGiao,
			sum(qtcn_ct.fTienThucChi) as fTienThucChi ,
			sum(qtcn_ct.fTienQuyetToanDaDuyet) as fTienQuyetToanDaDuyet ,
			sum(qtcn_ct.fTienDeNghiQuyetToanQuyNay) as fTienDeNghiQuyetToanQuyNay,
			sum(qtcn_ct.fTienXacNhanQuyetToanQuyNay) as fTienXacNhanQuyetToanQuyNay
		into #tblQuyetToanQuyChiTiet
		from BH_QTC_Quy_KCB_ChiTiet as qtcn_ct
		inner join BH_QTC_Quy_KCB  as qtcn on qtcn_ct.iID_QTC_Quy_KCB = qtcn.ID_QTC_Quy_KCB
		where qtcn.iID_MaDonVi in (select * from dbo.splitstring(@IdMaDonVi)) 
		and qtcn.iNamChungTu = @INamLamViec
		--and ((@IsTongHop = 0 and qtcn.bDaTongHop = 0 and qtcn.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  qtcn.sDSSoChungTuTongHop is not null))
		and qtcn.iQuyChungTu = @IQuy
		group by qtcn_ct.iID_MucLucNganSach,qtcn_ct.sNoiDung

		
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
			chi_tiet.fTien_DuToanNamTruocChuyenSang,
			chi_tiet.fTien_DuToanGiaoNamNay,
			chi_tiet.fTien_TongDuToanDuocGiao,
			chi_tiet.fTienThucChi,
			chi_tiet.fTienQuyetToanDaDuyet,
			chi_tiet.fTienDeNghiQuyetToanQuyNay,
			chi_tiet.fTienXacNhanQuyetToanQuyNay
		from #tblMucLucNganSach as mucluc
		left join #tblQuyetToanQuyChiTiet as chi_tiet on mucluc.iID_MLNS = chi_tiet.iID_MucLucNganSach
		order by mucluc.sXauNoiMa
	
	drop table #tblMucLucNganSach;
	drop table #tblQuyetToanQuyChiTiet;
end


GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_get_donvi_nBHXH_lns]    Script Date: 11/20/2023 11:28:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_cp_rpt_get_donvi_nBHXH_lns]
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
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_get_donvi_nKCB_lns]    Script Date: 11/20/2023 11:28:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_cp_rpt_get_donvi_nKCB_lns]
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
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_get_donvi_nKPK_lns]    Script Date: 11/20/2023 11:28:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_cp_rpt_get_donvi_nKPK_lns]
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
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_get_donvi_nKPQL_lns]    Script Date: 11/20/2023 11:28:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_cp_rpt_get_donvi_nKPQL_lns]
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
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_get_donvi_qBHXH_lns]    Script Date: 11/20/2023 11:28:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_cp_rpt_get_donvi_qBHXH_lns]
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
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_get_donvi_qkcb_lns]    Script Date: 11/20/2023 11:28:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_cp_rpt_get_donvi_qkcb_lns]
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
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_get_donvi_qKPK_lns]    Script Date: 11/20/2023 11:28:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_cp_rpt_get_donvi_qKPK_lns]
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
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_get_donvi_qKPQL_lns]    Script Date: 11/20/2023 11:28:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_cp_rpt_get_donvi_qKPQL_lns]
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
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_nbhxh]    Script Date: 11/20/2023 11:28:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qtc_get_lns_for_nbhxh]
	@namLamViec int,
	@donVi nvarchar(max),
	@ngayChungTu datetime,
	@userName nvarchar(100)
AS
BEGIN
	--WITH tblLNS AS (
	--	SELECT DISTINCT sLNS
	--	FROM BH_DM_MucLucNganSach ml
	--	WHERE iID_MLNS in 
	--			(
	--				SELECT  ct.iID_MucLucNganSach
	--				FROM BH_QTC_Quy_KinhPhiQuanLy_ChiTiet ct
	--				WHERE ct.iID_QTC_Quy_KinhPhiQuanLy in 
	--				(SELECT ID_QTC_Quy_KinhPhiQuanLy 
	--				FROM BH_QTC_Quy_KinhPhiQuanLy 
	--				WHERE iNamChungTu = @namLamViec 
	--					AND iQuyChungTu=@Quy
	--					AND iID_MaDonVi IN (select * from f_split(@donVi))
	--					AND cast(dNgayQuyetDinh as date) <= cast(@ngayChungTu as date))
	--), 

	--WITH tblLNS AS (
	--	SELECT DISTINCT sLNS
	--	FROM BH_DM_MucLucNganSach ml
	--	WHERE iID_MLNS in 
	--			(
	--				SELECT  ct.iID_MucLucNganSach
	--				FROM BH_QTC_Quy_KinhPhiQuanLy_ChiTiet ct
	--				WHERE ct.iID_QTC_Quy_KinhPhiQuanLy in 
	--				(SELECT ID_QTC_Quy_KinhPhiQuanLy 
	--				FROM BH_QTC_Quy_KinhPhiQuanLy 
	--				WHERE iNamChungTu = @namLamViec 
	--					AND iQuyChungTu=@Quy
	--					AND iID_MaDonVi IN (select * from f_split(@donVi))
	--					AND cast(dNgayQuyetDinh as date) <= cast(@ngayChungTu as date)))
	--),
		WITH tblLNS AS (
		SELECT DISTINCT sLNS
		FROM BH_DM_MucLucNganSach
		WHERE iID_MLNS in 
				(SELECT  ct.iID_MucLucNganSach
				FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ct
				WHERE ct.iID_QTC_Nam_CheDoBHXH in 
					(SELECT ID_QTC_Nam_CheDoBHXH 
					FROM BH_QTC_Nam_CheDoBHXH 
					WHERE iNamChungTu = @namLamViec 
					AND iID_MaDonVi IN (select * from f_split(@donVi))
					AND cast(dNgayQuyetDinh as date) <= cast(@ngayChungTu as date)))
	), 
	LNSTree AS (
		SELECT *
		FROM BH_DM_MucLucNganSach
		WHERE 
			sL = ''
			AND iNamLamViec = @namLamViec
			AND sLNS in (SELECT * FROM tblLNS)
		UNION ALL
		SELECT mlnsParent.*
		FROM BH_DM_MucLucNganSach mlnsParent
		INNER JOIN LNSTree
			ON mlnsParent.iID_MLNS = lnstree.iID_MLNS_Cha
		WHERE 
			mlnsParent.sL = '' 
			AND mlnsParent.iNamLamViec = @namLamViec
	)
	SELECT mlns.* 
	FROM (SELECT DISTINCT * FROM LNSTree) mlns
	WHERE mlns.iNamlamviec=@namLamViec
	ORDER BY sXauNoiMa
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_nkcb]    Script Date: 11/20/2023 11:28:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qtc_get_lns_for_nkcb]
	@namLamViec int,
	@donVi nvarchar(max),
	@ngayChungTu datetime,
	@userName nvarchar(100)
AS
BEGIN
	--WITH tblLNS AS (
	--	SELECT DISTINCT sLNS
	--	FROM BH_DM_MucLucNganSach ml
	--	WHERE iID_MLNS in 
	--			(
	--				SELECT  ct.iID_MucLucNganSach
	--				FROM BH_QTC_Quy_KinhPhiQuanLy_ChiTiet ct
	--				WHERE ct.iID_QTC_Quy_KinhPhiQuanLy in 
	--				(SELECT ID_QTC_Quy_KinhPhiQuanLy 
	--				FROM BH_QTC_Quy_KinhPhiQuanLy 
	--				WHERE iNamChungTu = @namLamViec 
	--					AND iQuyChungTu=@Quy
	--					AND iID_MaDonVi IN (select * from f_split(@donVi))
	--					AND cast(dNgayQuyetDinh as date) <= cast(@ngayChungTu as date))
	--), 

	--WITH tblLNS AS (
	--	SELECT DISTINCT sLNS
	--	FROM BH_DM_MucLucNganSach ml
	--	WHERE iID_MLNS in 
	--			(
	--				SELECT  ct.iID_MucLucNganSach
	--				FROM BH_QTC_Quy_KinhPhiQuanLy_ChiTiet ct
	--				WHERE ct.iID_QTC_Quy_KinhPhiQuanLy in 
	--				(SELECT ID_QTC_Quy_KinhPhiQuanLy 
	--				FROM BH_QTC_Quy_KinhPhiQuanLy 
	--				WHERE iNamChungTu = @namLamViec 
	--					AND iQuyChungTu=@Quy
	--					AND iID_MaDonVi IN (select * from f_split(@donVi))
	--					AND cast(dNgayQuyetDinh as date) <= cast(@ngayChungTu as date)))
	--),
		WITH tblLNS AS (
		SELECT DISTINCT sLNS
		FROM BH_DM_MucLucNganSach
		WHERE iID_MLNS in 
				(SELECT  ct.iID_MucLucNganSach
				FROM BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet ct
				WHERE ct.iID_QTC_Nam_KCB_QuanYDonVi in 
					(SELECT iID_QTC_Nam_KCB_QuanYDonVi 
					FROM BH_QTC_Nam_KCB_QuanYDonVi 
					WHERE iNamChungTu = @namLamViec 
					AND iID_MaDonVi IN (select * from f_split(@donVi))
					AND cast(dNgayQuyetDinh as date) <= cast(@ngayChungTu as date)))
	), 
	LNSTree AS (
		SELECT *
		FROM BH_DM_MucLucNganSach
		WHERE 
			sL = ''
			AND iNamLamViec = @namLamViec
			AND sLNS in (SELECT * FROM tblLNS)
		UNION ALL
		SELECT mlnsParent.*
		FROM BH_DM_MucLucNganSach mlnsParent
		INNER JOIN LNSTree
			ON mlnsParent.iID_MLNS = lnstree.iID_MLNS_Cha
		WHERE 
			mlnsParent.sL = '' 
			AND mlnsParent.iNamLamViec = @namLamViec
	)
	SELECT mlns.* 
	FROM (SELECT DISTINCT * FROM LNSTree) mlns
	WHERE mlns.iNamlamviec=@namLamViec
	ORDER BY sXauNoiMa
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_nkpk]    Script Date: 11/20/2023 11:28:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qtc_get_lns_for_nkpk]
	@namLamViec int,
	@donVi nvarchar(max),
	@ngayChungTu datetime,
	@userName nvarchar(100),
	@LoaiChi uniqueidentifier
AS
BEGIN
	--WITH tblLNS AS (
	--	SELECT DISTINCT sLNS
	--	FROM BH_DM_MucLucNganSach ml
	--	WHERE iID_MLNS in 
	--			(
	--				SELECT  ct.iID_MucLucNganSach
	--				FROM BH_QTC_Quy_KinhPhiQuanLy_ChiTiet ct
	--				WHERE ct.iID_QTC_Quy_KinhPhiQuanLy in 
	--				(SELECT ID_QTC_Quy_KinhPhiQuanLy 
	--				FROM BH_QTC_Quy_KinhPhiQuanLy 
	--				WHERE iNamChungTu = @namLamViec 
	--					AND iQuyChungTu=@Quy
	--					AND iID_MaDonVi IN (select * from f_split(@donVi))
	--					AND cast(dNgayQuyetDinh as date) <= cast(@ngayChungTu as date))
	--), 

	--WITH tblLNS AS (
	--	SELECT DISTINCT sLNS
	--	FROM BH_DM_MucLucNganSach ml
	--	WHERE iID_MLNS in 
	--			(
	--				SELECT  ct.iID_MucLucNganSach
	--				FROM BH_QTC_Quy_KinhPhiQuanLy_ChiTiet ct
	--				WHERE ct.iID_QTC_Quy_KinhPhiQuanLy in 
	--				(SELECT ID_QTC_Quy_KinhPhiQuanLy 
	--				FROM BH_QTC_Quy_KinhPhiQuanLy 
	--				WHERE iNamChungTu = @namLamViec 
	--					AND iQuyChungTu=@Quy
	--					AND iID_MaDonVi IN (select * from f_split(@donVi))
	--					AND cast(dNgayQuyetDinh as date) <= cast(@ngayChungTu as date)))
	--),
		WITH tblLNS AS (
		SELECT DISTINCT sLNS
		FROM BH_DM_MucLucNganSach
		WHERE iID_MLNS in 
				(SELECT  ct.iID_MucLucNganSach
				FROM BH_QTC_Nam_KPK_ChiTiet ct
				WHERE ct.iID_QTC_Nam_KPK in 
					(SELECT ID_QTC_Nam_KPK 
					FROM BH_QTC_Nam_KPK 
					WHERE iNamChungTu = @namLamViec 
					AND iID_LoaiChi=@LoaiChi
					AND iID_MaDonVi IN (select * from f_split(@donVi))
					AND cast(dNgayQuyetDinh as date) <= cast(@ngayChungTu as date)))
	), 
	LNSTree AS (
		SELECT *
		FROM BH_DM_MucLucNganSach
		WHERE 
			sL = ''
			AND iNamLamViec = @namLamViec
			AND sLNS in (SELECT * FROM tblLNS)
		UNION ALL
		SELECT mlnsParent.*
		FROM BH_DM_MucLucNganSach mlnsParent
		INNER JOIN LNSTree
			ON mlnsParent.iID_MLNS = lnstree.iID_MLNS_Cha
		WHERE 
			mlnsParent.sL = '' 
			AND mlnsParent.iNamLamViec = @namLamViec
	)
	SELECT mlns.* 
	FROM (SELECT DISTINCT * FROM LNSTree) mlns
	WHERE mlns.iNamlamviec=@namLamViec
	ORDER BY sXauNoiMa
END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_nkpql]    Script Date: 11/20/2023 11:28:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qtc_get_lns_for_nkpql]
	@namLamViec int,
	@donVi nvarchar(max),
	@ngayChungTu datetime,
	@userName nvarchar(100)
AS
BEGIN
	--WITH tblLNS AS (
	--	SELECT DISTINCT sLNS
	--	FROM BH_DM_MucLucNganSach ml
	--	WHERE iID_MLNS in 
	--			(
	--				SELECT  ct.iID_MucLucNganSach
	--				FROM BH_QTC_Quy_KinhPhiQuanLy_ChiTiet ct
	--				WHERE ct.iID_QTC_Quy_KinhPhiQuanLy in 
	--				(SELECT ID_QTC_Quy_KinhPhiQuanLy 
	--				FROM BH_QTC_Quy_KinhPhiQuanLy 
	--				WHERE iNamChungTu = @namLamViec 
	--					AND iQuyChungTu=@Quy
	--					AND iID_MaDonVi IN (select * from f_split(@donVi))
	--					AND cast(dNgayQuyetDinh as date) <= cast(@ngayChungTu as date))
	--), 

	--WITH tblLNS AS (
	--	SELECT DISTINCT sLNS
	--	FROM BH_DM_MucLucNganSach ml
	--	WHERE iID_MLNS in 
	--			(
	--				SELECT  ct.iID_MucLucNganSach
	--				FROM BH_QTC_Quy_KinhPhiQuanLy_ChiTiet ct
	--				WHERE ct.iID_QTC_Quy_KinhPhiQuanLy in 
	--				(SELECT ID_QTC_Quy_KinhPhiQuanLy 
	--				FROM BH_QTC_Quy_KinhPhiQuanLy 
	--				WHERE iNamChungTu = @namLamViec 
	--					AND iQuyChungTu=@Quy
	--					AND iID_MaDonVi IN (select * from f_split(@donVi))
	--					AND cast(dNgayQuyetDinh as date) <= cast(@ngayChungTu as date)))
	--),
		WITH tblLNS AS (
		SELECT DISTINCT sLNS
		FROM BH_DM_MucLucNganSach
		WHERE iID_MLNS in 
				(SELECT  ct.iID_MucLucNganSach
				FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet ct
				WHERE ct.iID_QTC_Nam_KinhPhiQuanLy in 
					(SELECT iID_QTC_Nam_KinhPhiQuanLy 
					FROM BH_QTC_Nam_KinhPhiQuanLy 
					WHERE iNamLamViec = @namLamViec 
					AND iID_MaDonVi IN (select * from f_split(@donVi))
					AND cast(dNgayQuyetDinh as date) <= cast(@ngayChungTu as date)))
	), 
	LNSTree AS (
		SELECT *
		FROM BH_DM_MucLucNganSach
		WHERE 
			sL = ''
			AND iNamLamViec = @namLamViec
			AND sLNS in (SELECT * FROM tblLNS)
		UNION ALL
		SELECT mlnsParent.*
		FROM BH_DM_MucLucNganSach mlnsParent
		INNER JOIN LNSTree
			ON mlnsParent.iID_MLNS = lnstree.iID_MLNS_Cha
		WHERE 
			mlnsParent.sL = '' 
			AND mlnsParent.iNamLamViec = @namLamViec
	)
	SELECT mlns.* 
	FROM (SELECT DISTINCT * FROM LNSTree) mlns
	WHERE mlns.iNamlamviec=@namLamViec
	ORDER BY sXauNoiMa
END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_qbhxh]    Script Date: 11/20/2023 11:28:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qtc_get_lns_for_qbhxh]
	@namLamViec int,
	@donVi nvarchar(max),
	@Quy int,
	@ngayChungTu datetime,
	@userName nvarchar(100)
AS
BEGIN
	--WITH tblLNS AS (
	--	SELECT DISTINCT sLNS
	--	FROM BH_DM_MucLucNganSach ml
	--	WHERE iID_MLNS in 
	--			(
	--				SELECT  ct.iID_MucLucNganSach
	--				FROM BH_QTC_Quy_KinhPhiQuanLy_ChiTiet ct
	--				WHERE ct.iID_QTC_Quy_KinhPhiQuanLy in 
	--				(SELECT ID_QTC_Quy_KinhPhiQuanLy 
	--				FROM BH_QTC_Quy_KinhPhiQuanLy 
	--				WHERE iNamChungTu = @namLamViec 
	--					AND iQuyChungTu=@Quy
	--					AND iID_MaDonVi IN (select * from f_split(@donVi))
	--					AND cast(dNgayQuyetDinh as date) <= cast(@ngayChungTu as date))
	--), 

	--WITH tblLNS AS (
	--	SELECT DISTINCT sLNS
	--	FROM BH_DM_MucLucNganSach ml
	--	WHERE iID_MLNS in 
	--			(
	--				SELECT  ct.iID_MucLucNganSach
	--				FROM BH_QTC_Quy_KinhPhiQuanLy_ChiTiet ct
	--				WHERE ct.iID_QTC_Quy_KinhPhiQuanLy in 
	--				(SELECT ID_QTC_Quy_KinhPhiQuanLy 
	--				FROM BH_QTC_Quy_KinhPhiQuanLy 
	--				WHERE iNamChungTu = @namLamViec 
	--					AND iQuyChungTu=@Quy
	--					AND iID_MaDonVi IN (select * from f_split(@donVi))
	--					AND cast(dNgayQuyetDinh as date) <= cast(@ngayChungTu as date)))
	--),
		WITH tblLNS AS (
		SELECT DISTINCT sLNS
		FROM BH_DM_MucLucNganSach
		WHERE iID_MLNS in 
				(SELECT  ct.iID_MucLucNganSach
				FROM BH_QTC_Quy_CheDoBHXH_ChiTiet ct
				WHERE ct.iID_QTC_Quy_CheDoBHXH in 
					(SELECT ID_QTC_Quy_CheDoBHXH 
					FROM BH_QTC_Quy_CheDoBHXH 
					WHERE iNamChungTu = @namLamViec 
					AND iQuyChungTu=@Quy
					AND iID_MaDonVi IN (select * from f_split(@donVi))
					AND cast(dNgayChungTu as date) <= cast(@ngayChungTu as date)))
	), 
	LNSTree AS (
		SELECT *
		FROM BH_DM_MucLucNganSach
		WHERE 
			sL = ''
			AND iNamLamViec = @namLamViec
			AND sLNS in (SELECT * FROM tblLNS)
		UNION ALL
		SELECT mlnsParent.*
		FROM BH_DM_MucLucNganSach mlnsParent
		INNER JOIN LNSTree
			ON mlnsParent.iID_MLNS = lnstree.iID_MLNS_Cha
		WHERE 
			mlnsParent.sL = '' 
			AND mlnsParent.iNamLamViec = @namLamViec
	)
	SELECT mlns.* 
	FROM (SELECT DISTINCT * FROM LNSTree) mlns
	WHERE mlns.iNamlamviec=@namLamViec
	ORDER BY sXauNoiMa
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_qkcb]    Script Date: 11/20/2023 11:28:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qtc_get_lns_for_qkcb]
	@namLamViec int,
	@donVi nvarchar(max),
	@Quy int,
	@ngayChungTu datetime,
	@userName nvarchar(100)
AS
BEGIN
	--WITH tblLNS AS (
	--	SELECT DISTINCT sLNS
	--	FROM BH_DM_MucLucNganSach ml
	--	WHERE iID_MLNS in 
	--			(
	--				SELECT  ct.iID_MucLucNganSach
	--				FROM BH_QTC_Quy_KinhPhiQuanLy_ChiTiet ct
	--				WHERE ct.iID_QTC_Quy_KinhPhiQuanLy in 
	--				(SELECT ID_QTC_Quy_KinhPhiQuanLy 
	--				FROM BH_QTC_Quy_KinhPhiQuanLy 
	--				WHERE iNamChungTu = @namLamViec 
	--					AND iQuyChungTu=@Quy
	--					AND iID_MaDonVi IN (select * from f_split(@donVi))
	--					AND cast(dNgayQuyetDinh as date) <= cast(@ngayChungTu as date))
	--), 

	--WITH tblLNS AS (
	--	SELECT DISTINCT sLNS
	--	FROM BH_DM_MucLucNganSach ml
	--	WHERE iID_MLNS in 
	--			(
	--				SELECT  ct.iID_MucLucNganSach
	--				FROM BH_QTC_Quy_KinhPhiQuanLy_ChiTiet ct
	--				WHERE ct.iID_QTC_Quy_KinhPhiQuanLy in 
	--				(SELECT ID_QTC_Quy_KinhPhiQuanLy 
	--				FROM BH_QTC_Quy_KinhPhiQuanLy 
	--				WHERE iNamChungTu = @namLamViec 
	--					AND iQuyChungTu=@Quy
	--					AND iID_MaDonVi IN (select * from f_split(@donVi))
	--					AND cast(dNgayQuyetDinh as date) <= cast(@ngayChungTu as date)))
	--),
		WITH tblLNS AS (
		SELECT DISTINCT sLNS
		FROM BH_DM_MucLucNganSach
		WHERE iID_MLNS in 
				(SELECT  ct.iID_MucLucNganSach
				FROM BH_QTC_Quy_KCB_ChiTiet ct
				WHERE ct.iID_QTC_Quy_KCB in 
					(SELECT iID_QTC_Quy_KCB 
					FROM BH_QTC_Quy_KCB 
					WHERE iNamChungTu = @namLamViec 
					AND iQuyChungTu=@Quy
					AND iID_MaDonVi IN (select * from f_split(@donVi))
					AND cast(dNgayChungTu as date) <= cast(@ngayChungTu as date)))
	), 
	LNSTree AS (
		SELECT *
		FROM BH_DM_MucLucNganSach
		WHERE 
			sL = ''
			AND iNamLamViec = @namLamViec
			AND sLNS in (SELECT * FROM tblLNS)
		UNION ALL
		SELECT mlnsParent.*
		FROM BH_DM_MucLucNganSach mlnsParent
		INNER JOIN LNSTree
			ON mlnsParent.iID_MLNS = lnstree.iID_MLNS_Cha
		WHERE 
			mlnsParent.sL = '' 
			AND mlnsParent.iNamLamViec = @namLamViec
	)
	SELECT mlns.* 
	FROM (SELECT DISTINCT * FROM LNSTree) mlns
	WHERE mlns.iNamlamviec=@namLamViec
	ORDER BY sXauNoiMa
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_qkpk]    Script Date: 11/20/2023 11:28:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qtc_get_lns_for_qkpk]
	@namLamViec int,
	@donVi nvarchar(max),
	@Quy int,
	@ngayChungTu datetime,
	@userName nvarchar(100),
	@LoaiChi uniqueidentifier
AS
BEGIN
		WITH tblLNS AS (
		SELECT DISTINCT sLNS
		FROM BH_DM_MucLucNganSach
		WHERE iID_MLNS in 
				(SELECT  ct.iID_MucLucNganSach
				FROM BH_QTC_Quy_KPK_ChiTiet ct
				WHERE ct.iID_QTC_Quy_KPK in 
					(SELECT ID_QTC_Quy_KPK 
					FROM BH_QTC_Quy_KPK 
					WHERE iNamChungTu = @namLamViec 
					AND iQuyChungTu=@Quy
					AND iID_LoaiChi=@LoaiChi
					AND iID_MaDonVi IN (select * from f_split(@donVi))
					AND cast(dNgayChungTu as date) <= cast(@ngayChungTu as date)))
	), 
	LNSTree AS (
		SELECT *
		FROM BH_DM_MucLucNganSach
		WHERE 
			sL = ''
			AND iNamLamViec = @namLamViec
			AND sLNS in (SELECT * FROM tblLNS)
		UNION ALL
		SELECT mlnsParent.*
		FROM BH_DM_MucLucNganSach mlnsParent
		INNER JOIN LNSTree
			ON mlnsParent.iID_MLNS = lnstree.iID_MLNS_Cha
		WHERE 
			mlnsParent.sL = '' 
			AND mlnsParent.iNamLamViec = @namLamViec
	)
	SELECT mlns.* 
	FROM (SELECT DISTINCT * FROM LNSTree) mlns
	WHERE mlns.iNamlamviec=@namLamViec
	ORDER BY sXauNoiMa
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_qkpql]    Script Date: 11/20/2023 11:28:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qtc_get_lns_for_qkpql]
	@namLamViec int,
	@donVi nvarchar(max),
	@Quy int,
	@ngayChungTu datetime,
	@userName nvarchar(100)
AS
BEGIN
	--WITH tblLNS AS (
	--	SELECT DISTINCT sLNS
	--	FROM BH_DM_MucLucNganSach ml
	--	WHERE iID_MLNS in 
	--			(
	--				SELECT  ct.iID_MucLucNganSach
	--				FROM BH_QTC_Quy_KinhPhiQuanLy_ChiTiet ct
	--				WHERE ct.iID_QTC_Quy_KinhPhiQuanLy in 
	--				(SELECT ID_QTC_Quy_KinhPhiQuanLy 
	--				FROM BH_QTC_Quy_KinhPhiQuanLy 
	--				WHERE iNamChungTu = @namLamViec 
	--					AND iQuyChungTu=@Quy
	--					AND iID_MaDonVi IN (select * from f_split(@donVi))
	--					AND cast(dNgayQuyetDinh as date) <= cast(@ngayChungTu as date))
	--), 

	--WITH tblLNS AS (
	--	SELECT DISTINCT sLNS
	--	FROM BH_DM_MucLucNganSach ml
	--	WHERE iID_MLNS in 
	--			(
	--				SELECT  ct.iID_MucLucNganSach
	--				FROM BH_QTC_Quy_KinhPhiQuanLy_ChiTiet ct
	--				WHERE ct.iID_QTC_Quy_KinhPhiQuanLy in 
	--				(SELECT ID_QTC_Quy_KinhPhiQuanLy 
	--				FROM BH_QTC_Quy_KinhPhiQuanLy 
	--				WHERE iNamChungTu = @namLamViec 
	--					AND iQuyChungTu=@Quy
	--					AND iID_MaDonVi IN (select * from f_split(@donVi))
	--					AND cast(dNgayQuyetDinh as date) <= cast(@ngayChungTu as date)))
	--),
		WITH tblLNS AS (
		SELECT DISTINCT sLNS
		FROM BH_DM_MucLucNganSach
		WHERE iID_MLNS in 
				(SELECT  ct.iID_MucLucNganSach
				FROM BH_QTC_Quy_KinhPhiQuanLy_ChiTiet ct
				WHERE ct.iID_QTC_Quy_KinhPhiQuanLy in 
					(SELECT ID_QTC_Quy_KinhPhiQuanLy 
					FROM BH_QTC_Quy_KinhPhiQuanLy 
					WHERE iNamChungTu = @namLamViec 
					AND iQuyChungTu=@Quy
					AND iID_MaDonVi IN (select * from f_split(@donVi))
					AND cast(dNgayChungTu as date) <= cast(@ngayChungTu as date)))
	), 
	LNSTree AS (
		SELECT *
		FROM BH_DM_MucLucNganSach
		WHERE 
			sL = ''
			AND iNamLamViec = @namLamViec
			AND sLNS in (SELECT * FROM tblLNS)
		UNION ALL
		SELECT mlnsParent.*
		FROM BH_DM_MucLucNganSach mlnsParent
		INNER JOIN LNSTree
			ON mlnsParent.iID_MLNS = lnstree.iID_MLNS_Cha
		WHERE 
			mlnsParent.sL = '' 
			AND mlnsParent.iNamLamViec = @namLamViec
	)
	SELECT mlns.* 
	FROM (SELECT DISTINCT * FROM LNSTree) mlns
	WHERE mlns.iNamlamviec=@namLamViec
	ORDER BY sXauNoiMa
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_nkpk_kehoach_bh]    Script Date: 11/20/2023 11:28:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay chi tiet chung tu 
CREATE PROCEDURE [dbo].[sp_rpt_qtc_nkpk_kehoach_bh]
	 @NamLamViec int,
	 @iD uniqueidentifier,
	 @IdDonVi nvarchar(max),
	 @LNS nvarchar(max),
	 @LoaiChi uniqueidentifier
AS
BEGIN 
	SET NOCOUNT ON;
	DECLARE @iDCT uniqueidentifier='00000000-0000-0000-0000-000000000000';
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

SELECT   isnull(ctct.ID_QTC_Nam_KPK_ChiTiet, @iDCT) as ID_QTC_Nam_KPK_ChiTiet,
		@iD as iID_QTC_Nam_KPK,
		mlns.iID_MLNS as iID_MucLucNganSach,
		mlns.iID_MLNS_Cha as IdParent,
		mlns.sXauNoiMa as sXauNoiMa,
		mlns.sLNS as SLNS,
		mlns.sL as SL,
		mlns.sK as SK,
		mlns.sM as SM,
		mlns.sTM as STM,
		mlns.sTTM as STTM,
		mlns.sNG as SNG,
		mlns.sTNG as STNG,
		mlns.sMoTa as SNoiDung,
		@NamLamViec AS INamLamViec,
		mlns.bHangCha as IsHangCha,
		isnull(ctct.dNgayTao, getdate()) AS dNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS dNgaySua,
		ctct.sNguoiTao AS sNguoiTao,
		ctct.sNguoiSua AS sNguoiSua,
		ISNULL(Sum(ctct.fTien_DuToanNamTruocChuyenSang), 0)  as fTien_DuToanNamTruocChuyenSang, 
		ISNULL(Sum(ctct.fTien_DuToanGiaoNamNay), 0)  as fTien_DuToanGiaoNamNay,
		ISNULL(Sum(ctct.fTien_TongDuToanDuocGiao), 0)  as fTien_TongDuToanDuocGiao, 
		ISNULL(Sum(ctct.fTien_ThucChi), 0)  as fTien_ThucChi, 
		ISNULL(Sum(ctct.fTienThua), 0)  as fTienThua, 
		ISNULL(Sum(ctct.fTienThieu), 0)  as fTienThieu, 
		ISNULL(Sum(ctct.fTiLeThucHienTrenDuToan), 0)  as fTiLeThucHienTrenDuToan
	FROM 
		#tblMlnsByPhanCap mlns
	LEFT JOIN 
		(SELECT * FROM BH_QTC_Nam_KPK_ChiTiet 
				WHERE iID_QTC_Nam_KPK in
					( SELECT ID_QTC_Nam_KPK FROM BH_QTC_Nam_KPK
								WHERE iNamChungTu=@NamLamViec
								AND iID_LoaiChi=@LoaiChi
								AND iID_MaDonVi IN (select * from f_split(@IdDonVi))
								)) ctct
	on mlns.iID_MLNS = ctct.iID_MucLucNganSach 

	Group by ctct.ID_QTC_Nam_KPK_ChiTiet,
		ctct.iID_QTC_Nam_KPK,
		mlns.iID_MLNS ,
		mlns.iID_MLNS_Cha ,
		mlns.sXauNoiMa ,
		mlns.sLNS ,
		mlns.sL ,
		mlns.sK ,
		mlns.sM ,
		mlns.sTM ,
		mlns.sTTM ,
		mlns.sNG ,
		mlns.sTNG ,
		mlns.sMoTa ,
		mlns.bHangCha ,
		ctct.sNguoiTao ,
		ctct.sNguoiSua, 
		ctct.dNgayTao,
		ctct.dNgaySua
	Order by mlns.sXauNoiMa
END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_nqlkp_kehoach_bh]    Script Date: 11/20/2023 11:28:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay chi tiet chung tu 
CREATE PROCEDURE [dbo].[sp_rpt_qtc_nqlkp_kehoach_bh]
	 @NamLamViec int,
	 @iD uniqueidentifier,
	 @IdDonVi nvarchar(max),
	 @LNS nvarchar(max),
	 @DonViTnh int
AS
BEGIN 
	SET NOCOUNT ON;
	DECLARE @iDCT uniqueidentifier='00000000-0000-0000-0000-000000000000';
	-- lấy ra mlns theo phân cấp
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha
			into #tblMlnsByPhanCap
	FROM BH_DM_MucLucNganSach 
	WHERE 
		iNamLamViec = 2023 
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

SELECT   isnull(ctct.ID_QTC_Nam_KinhPhiQuanLy_ChiTiet, @iDCT) as ID_QTC_Nam_KinhPhiQuanLy_ChiTiet,
		ctct.IID_QTC_Nam_KinhPhiQuanLy,
		mlns.iID_MLNS as iID_MucLucNganSach,
		mlns.iID_MLNS_Cha as IdParent,
		mlns.sXauNoiMa as sXauNoiMa,
		mlns.sLNS as SLNS,
		mlns.sL as SL,
		mlns.sK as SK,
		mlns.sM as SM,
		mlns.sTM as STM,
		mlns.sTTM as STTM,
		mlns.sNG as SNG,
		mlns.sTNG as STNG,
		mlns.sMoTa as SNoiDung,
		@NamLamViec AS INamLamViec,
		mlns.bHangCha as IsHangCha,
		isnull(ctct.dNgayTao, getdate()) AS dNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS dNgaySua,
		ctct.sNguoiTao AS sNguoiTao,
		ctct.sNguoiSua AS sNguoiSua,
		ISNULL(Sum(ctct.fTien_DuToanNamTruocChuyenSang), 0) / @DonViTnh  as fTien_DuToanNamTruocChuyenSang, 
		ISNULL(Sum(ctct.fTien_DuToanGiaoNamNay), 0) / @DonViTnh  as fTien_DuToanGiaoNamNay,
		ISNULL(Sum(ctct.fTien_DuToanNamTruocChuyenSang), 0) / @DonViTnh + ISNULL(Sum(ctct.fTien_DuToanGiaoNamNay), 0)  as fTien_TongDuToanDuocGiao, 
		ISNULL(Sum(ctct.fTien_ThucChi), 0) / @DonViTnh  as fTien_ThucChi, 
		ISNULL(Sum(ctct.fTien_ThucChi), 0) / @DonViTnh  as fTienThua, 
		ISNULL(Sum(ctct.fTienThieu), 0) / @DonViTnh  as fTienThieu, 
		ISNULL(Sum(ctct.fTiLeThucHienTrenDuToan), 0) / @DonViTnh  as fTiLeThucHienTrenDuToan
	FROM 
		#tblMlnsByPhanCap mlns
	LEFT JOIN 
		(SELECT * FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet 
				WHERE iID_QTC_Nam_KinhPhiQuanLy in
					( SELECT ID_QTC_Nam_KinhPhiQuanLy FROM BH_QTC_Nam_KinhPhiQuanLy
								WHERE iNamChungTu=@NamLamViec
								AND iID_MaDonVi IN (select * from f_split(@IdDonVi))
								)) ctct
	on mlns.iID_MLNS = ctct.iID_MucLucNganSach
	Group by ctct.ID_QTC_Nam_KinhPhiQuanLy_ChiTiet,
		ctct.IID_QTC_Nam_KinhPhiQuanLy,
		mlns.iID_MLNS ,
		mlns.iID_MLNS_Cha ,
		mlns.sXauNoiMa ,
		mlns.sLNS ,
		mlns.sL ,
		mlns.sK ,
		mlns.sM ,
		mlns.sTM ,
		mlns.sTTM ,
		mlns.sNG ,
		mlns.sTNG ,
		mlns.sMoTa ,
		mlns.bHangCha ,
		ctct.sNguoiTao ,
		ctct.sNguoiSua, 
		ctct.dNgayTao,
		ctct.dNgaySua

	Order by mlns.sXauNoiMa

	drop table #tblMlnsByPhanCap;

END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkp_ql_chungtu_chi_tiet]    Script Date: 11/20/2023 11:28:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay ke hoach chung tu theo don vi
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
		mlns.sMoTa as SNoiDung,
		mlns.bHangCha as IsHangCha,
		ISNULL(SUM(ctct.fTienDuToanDuocGiao), 0) / @Dvt as FTienDuToanDuocGiao,
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
		ISNULL(SUM(ctct.fTienDuToanDuocGiao), 0) / @Dvt as FTienDuToanDuocGiao,
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
	Order by mlns.sXauNoiMa
END
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkp_ql_thongtriloai1_bh]    Script Date: 11/20/2023 11:28:09 AM ******/
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
		ISNULL(ct.fTongTienXacNhanQuyetToanQuyNay,0)/ @Dvt FTongTienXacNhanQuyetToanQuyNay,
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
		ISNULL(ct.fTongTienXacNhanQuyetToanQuyNay,0)/ @Dvt FTongTienXacNhanQuyetToanQuyNay,
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
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkpk_kehoach_bh]    Script Date: 11/20/2023 11:28:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay ke hoach chung tu theo don vi
CREATE PROCEDURE [dbo].[sp_rpt_qtc_qkpk_kehoach_bh]
	 @NamLamViec int,
	 @iQuy nvarchar(100),
	 @IdDonVi nvarchar(max),
	 @LNS nvarchar(max),
	 @UserName nvarchar(100),
	 @IdLoaiChi uniqueidentifier,
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
		mlns.sMoTa as SNoiDung,
		mlns.bHangCha as IsHangCha,
		ISNULL(Sum(ctct.fTien_DuToanNamTruocChuyenSang), 0) / @Dvt as FTien_DuToanNamTruocChuyenSang,
		ISNULL(Sum(ctct.fTien_DuToanGiaoNamNay), 0) / @Dvt as FTien_DuToanGiaoNamNay, 
		ISNULL(Sum(ctct.fTien_TongDuToanDuocGiao), 0) / @Dvt as FTien_TongDuToanDuocGiao, 
		ISNULL(Sum(ctct.fTienThucChi), 0) / @Dvt as FTienThucChi, 
		ISNULL(Sum(ctct.fTienQuyetToanDaDuyet), 0) / @Dvt as FTienQuyetToanDaDuyet, 
		ISNULL(Sum(ctct.fTienDeNghiQuyetToanQuyNay), 0) / @Dvt as FTienDeNghiQuyetToanQuyNay, 
		ISNULL(Sum(ctct.fTienXacNhanQuyetToanQuyNay), 0) / @Dvt as FTienXacNhanQuyetToanQuyNay 
	FROM 
		#tblMlnsByPhanCap mlns
	LEFT JOIN 
		(SELECT * FROM BH_QTC_Quy_KPK_ChiTiet 
				WHERE iID_QTC_Quy_KPK in
					( SELECT ID_QTC_Quy_KPK FROM BH_QTC_Quy_KPK
								WHERE iNamChungTu=@NamLamViec
								AND iQuyChungTu=@iQuy
								AND iID_LoaiChi=@IdLoaiChi
								--AND bIsKhoa=1
								AND iID_MaDonVi IN (select * from f_split(@IdDonVi))
								)) ctct
	on mlns.iID_MLNS = ctct.iID_MucLucNganSach 
	group by mlns.iID_MLNS,
		mlns.iID_MLNS_Cha ,
		mlns.sXauNoiMa ,
		mlns.sLNS ,
		mlns.sL ,
		mlns.sK ,
		mlns.sM ,
		mlns.sTM ,
		mlns.sTTM ,
		mlns.sNG ,
		mlns.sTNG ,
		mlns.sMoTa ,
		mlns.bHangCha 
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
		ISNULL(Sum(ctct.fTien_DuToanNamTruocChuyenSang), 0) / @Dvt as FTien_DuToanNamTruocChuyenSang,
		ISNULL(Sum(ctct.fTien_DuToanGiaoNamNay), 0) / @Dvt as FTien_DuToanGiaoNamNay, 
		ISNULL(Sum(ctct.fTien_TongDuToanDuocGiao), 0) / @Dvt as FTien_TongDuToanDuocGiao, 
		ISNULL(Sum(ctct.fTienThucChi), 0) / @Dvt as FTienThucChi, 
		ISNULL(Sum(ctct.fTienQuyetToanDaDuyet), 0) / @Dvt as FTienQuyetToanDaDuyet, 
		ISNULL(Sum(ctct.fTienDeNghiQuyetToanQuyNay), 0) / @Dvt as FTienDeNghiQuyetToanQuyNay, 
		ISNULL(Sum(ctct.fTienXacNhanQuyetToanQuyNay), 0) / @Dvt as FTienXacNhanQuyetToanQuyNay 
	FROM 
		#tblMlnsByPhanCap mlns
	LEFT JOIN 
			(SELECT * FROM BH_QTC_Quy_KPK_ChiTiet 
							WHERE iID_QTC_Quy_KPK in
								( SELECT ID_QTC_Quy_KPK FROM BH_QTC_Quy_KPK
											WHERE iNamChungTu=@NamLamViec
											AND iQuyChungTu=@iQuy
											AND iID_LoaiChi=@IdLoaiChi
											--AND bIsKhoa=1
											AND iID_MaDonVi IN (select * from f_split(@IdDonVi))
											)) ctct
	on mlns.iID_MLNS = ctct.iID_MucLucNganSach 

	group by mlns.iID_MLNS,
		mlns.iID_MLNS_Cha ,
		mlns.sXauNoiMa ,
		mlns.sLNS ,
		mlns.sL ,
		mlns.sK ,
		mlns.sM ,
		mlns.sTM ,
		mlns.sTTM ,
		mlns.sNG ,
		mlns.sTNG ,
		mlns.sMoTa ,
		mlns.bHangCha 
	Order by mlns.sXauNoiMa
END
END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkpk_thongtriloai1_bh]    Script Date: 11/20/2023 11:28:09 AM ******/
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
				A.FTongTienXacNhanQuyetToanQuyNay,
				A.sLNS,
				A.SMaDonVi,
				dv.sTenDonVi,
				dv.iLoai
					FROM
					(SELECT 
					ct.iID_MaDonVi as SMaDonVi,
					ISNULL(Sum(ctct.fTienXacNhanQuyetToanQuyNay),0)/ @Dvt FTongTienXacNhanQuyetToanQuyNay,
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
				A.FTongTienXacNhanQuyetToanQuyNay,
				A.sLNS,
				A.SMaDonVi,
				dv.sTenDonVi,
				dv.iLoai
					FROM
					(SELECT 
					ct.iID_MaDonVi as SMaDonVi,
					ISNULL(Sum(ctct.fTienXacNhanQuyetToanQuyNay),0)/ @Dvt FTongTienXacNhanQuyetToanQuyNay,
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
