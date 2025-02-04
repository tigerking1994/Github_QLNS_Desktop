/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkp_ql_thongtriloai2_bh]    Script Date: 11/15/2023 4:35:26 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qtc_qkp_ql_thongtriloai2_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qtc_qkp_ql_thongtriloai2_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkp_ql_thongtriloai1_bh]    Script Date: 11/15/2023 4:35:26 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qtc_qkp_ql_thongtriloai1_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qtc_qkp_ql_thongtriloai1_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_nqlkp_kehoach_bh]    Script Date: 11/15/2023 4:35:26 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qtc_nqlkp_kehoach_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qtc_nqlkp_kehoach_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_qkpql]    Script Date: 11/15/2023 4:35:26 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_get_lns_for_qkpql]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_get_lns_for_qkpql]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_qkpk]    Script Date: 11/15/2023 4:35:26 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_get_lns_for_qkpk]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_get_lns_for_qkpk]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_qkcb]    Script Date: 11/15/2023 4:35:26 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_get_lns_for_qkcb]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_get_lns_for_qkcb]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_qbhxh]    Script Date: 11/15/2023 4:35:26 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_get_lns_for_qbhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_get_lns_for_qbhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_nkpql]    Script Date: 11/15/2023 4:35:26 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_get_lns_for_nkpql]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_get_lns_for_nkpql]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_nkpk]    Script Date: 11/15/2023 4:35:26 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_get_lns_for_nkpk]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_get_lns_for_nkpk]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_nkcb]    Script Date: 11/15/2023 4:35:26 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_get_lns_for_nkcb]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_get_lns_for_nkcb]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_nbhxh]    Script Date: 11/15/2023 4:35:26 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_get_lns_for_nbhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_get_lns_for_nbhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_get_donvi_qKPQL_lns]    Script Date: 11/15/2023 4:35:26 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_rpt_get_donvi_qKPQL_lns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_rpt_get_donvi_qKPQL_lns]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_get_donvi_qKPK_lns]    Script Date: 11/15/2023 4:35:26 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_rpt_get_donvi_qKPK_lns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_rpt_get_donvi_qKPK_lns]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_get_donvi_qkcb_lns]    Script Date: 11/15/2023 4:35:26 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_rpt_get_donvi_qkcb_lns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_rpt_get_donvi_qkcb_lns]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_get_donvi_qBHXH_lns]    Script Date: 11/15/2023 4:35:26 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_rpt_get_donvi_qBHXH_lns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_rpt_get_donvi_qBHXH_lns]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_get_donvi_nKPQL_lns]    Script Date: 11/15/2023 4:35:26 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_rpt_get_donvi_nKPQL_lns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_rpt_get_donvi_nKPQL_lns]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_get_donvi_nKPK_lns]    Script Date: 11/15/2023 4:35:26 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_rpt_get_donvi_nKPK_lns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_rpt_get_donvi_nKPK_lns]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_get_donvi_nKCB_lns]    Script Date: 11/15/2023 4:35:26 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_rpt_get_donvi_nKCB_lns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_rpt_get_donvi_nKCB_lns]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_get_donvi_nBHXH_lns]    Script Date: 11/15/2023 4:35:26 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_rpt_get_donvi_nBHXH_lns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_rpt_get_donvi_nBHXH_lns]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_quyettoanchicacchedo_bhxh]    Script Date: 11/15/2023 4:35:26 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_quyettoanchicacchedo_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_quyettoanchicacchedo_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_bhxh]    Script Date: 11/15/2023 4:35:26 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chiquyKCB_chitiet]    Script Date: 11/15/2023 4:35:26 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_chiquyKCB_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_chiquyKCB_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]    Script Date: 11/15/2023 4:35:26 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chinamKCB_chitiet]    Script Date: 11/15/2023 4:35:26 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_chinamKCB_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_chinamKCB_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chinambhxh_chitiet]    Script Date: 11/15/2023 4:35:26 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_chinambhxh_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_chinambhxh_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_bhxh]    Script Date: 11/15/2023 4:35:26 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]    Script Date: 11/15/2023 4:35:26 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_bhxh]    Script Date: 11/15/2023 4:35:26 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqKCB_create_data_summary]    Script Date: 11/15/2023 4:35:26 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtcqKCB_create_data_summary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtcqKCB_create_data_summary]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqBHXH_create_data_summary]    Script Date: 11/15/2023 4:35:26 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtcqBHXH_create_data_summary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtcqBHXH_create_data_summary]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcnKCB_create_data_summary]    Script Date: 11/15/2023 4:35:26 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtcnKCB_create_data_summary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtcnKCB_create_data_summary]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcnBHXH_create_data_summary]    Script Date: 11/15/2023 4:35:26 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtcnBHXH_create_data_summary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtcnBHXH_create_data_summary]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcnBHXH_create_data_summary]    Script Date: 11/15/2023 4:35:26 PM ******/
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

INSERT INTO BH_QTC_Nam_CheDoBHXH_ChiTiet(ID_QTC_Nam_CheDoBHXH_ChiTiet, iID_QTC_Nam_CheDoBHXH, iID_MucLucNganSach, sLoaiTroCap, dNgaySua, dNgayTao, sNguoiSua, sNguoiTao, fTienDuToanDuyet,iSoDuToanDuocDuyet, iTongSo_ThucChi,
	fTongTien_ThucChi, iSoSQ_ThucChi, fTienSQ_ThucChi, iSoQNCN_ThucChi, fTienQNCN_ThucChi, iSoCNVCQP_ThucChi, fTienCNVCQP_ThucChi, iSoHSQBS_ThucChi, fTienHSQBS_ThucChi, fTienThua,
	fTienThieu,fTiLeThucHienTrenDuToan, iSoLDHD_ThucChi, fTienLDHD_ThucChi )
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
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcnKCB_create_data_summary]    Script Date: 11/15/2023 4:35:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtcnKCB_create_data_summary]
@IdChungTu nvarchar(MAX),
@NguoiTao nvarchar(500),
@YearOfWork int,
@IdChungTuSummary nvarchar(MAX)
AS
BEGIN
SET NOCOUNT ON;

INSERT INTO BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet(ID_QTC_Nam_KCB_QuanYDonVi_ChiTiet, iID_QTC_Nam_KCB_QuanYDonVi, iID_MucLucNganSach, sNoiDung, dNgaySua, dNgayTao, sNguoiSua, sNguoiTao,fTien_DuToanNamTruocChuyenSang,
fTien_DuToanGiaoNamNay, fTien_TongDuToanDuocGiao, fTien_ThucChi, fTienThua,fTienThieu)
SELECT 
	   NEWID(),
	   @IdChungTuSummary,
       iID_MucLucNganSach,
       sNoiDung,
	   GETDATE(),
	   GETDATE(),
	   @NguoiTao,
	   '',
	   Sum(fTien_DuToanNamTruocChuyenSang),
	   Sum(fTien_DuToanGiaoNamNay),
	   Sum(fTien_TongDuToanDuocGiao),
	   Sum(fTien_ThucChi),
	   Sum(fTienThua),
	   Sum(fTienThieu)
	   
FROM BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet
WHERE  iID_QTC_Nam_KCB_QuanYDonVi IN
    (SELECT *
     FROM f_split(@IdChungTu))
group by iID_MucLucNganSach, sNoiDung

UPDATE BH_QTC_Nam_KCB_QuanYDonVi SET bDaTongHop = 1, iLoaiTongHop = 2 WHERE ID_QTC_Nam_KCB_QuanYDonVi IN (SELECT * FROM f_split(@IdChungTu));
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqBHXH_create_data_summary]    Script Date: 11/15/2023 4:35:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtcqBHXH_create_data_summary]
@IdChungTu nvarchar(MAX),
@NguoiTao nvarchar(500),
@YearOfWork int,
@IdChungTuSummary nvarchar(MAX)
AS
BEGIN
SET NOCOUNT ON;

INSERT INTO BH_QTC_Quy_CheDoBHXH_ChiTiet(
		ID_QTC_Quy_CheDoBHXH_ChiTiet,
		iID_QTC_Quy_CheDoBHXH,
		iID_MucLucNganSach,
		sLoaiTroCap,
		dNgaySua,
		dNgayTao,
		sNguoiSua,
		sNguoiTao,
		fTienDuToanDuyet,
		iSoLuyKeCuoiQuyNay,
		fTienLuyKeCuoiQuyNay,
		iSoSQ_DeNghi,
		fTienSQ_DeNghi,
		iSoQNCN_DeNghi,
		fTienQNCN_DeNghi,
		iSoCNVCQP_DeNghi,
		fTienCNVCQP_DeNghi,
		iSoHSQBS_DeNghi,
		fTienHSQBS_DeNghi,
		iSoLDHD_DeNghi,
		fTienLDHD_DeNghi,
		iTongSo_DeNghi,
		fTongTien_DeNghi,
		fTongTien_PheDuyet,
		iNamChungTu
	)
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
	   SUM(iSoLuyKeCuoiQuyNay),
	   SUM(fTienLuyKeCuoiQuyNay),
	   SUM(iSoSQ_DeNghi),
	   SUM(fTienSQ_DeNghi),
	   SUM(iSoQNCN_DeNghi),
	   SUM(fTienQNCN_DeNghi),
	   SUM(iSoCNVCQP_DeNghi),
	   SUM(fTienCNVCQP_DeNghi),
	   SUM(iSoHSQBS_DeNghi),
	   SUM(fTienHSQBS_DeNghi),
	   SUM(iSoLDHD_DeNghi),
	   SUM(fTienLDHD_DeNghi),
	   SUM(iTongSo_DeNghi),
	   SUM(fTongTien_DeNghi),
	   SUM(fTongTien_PheDuyet),
	   @YearOfWork

FROM BH_QTC_Quy_CheDoBHXH_ChiTiet
WHERE  iID_QTC_Quy_CheDoBHXH IN
    (SELECT *
     FROM f_split(@IdChungTu))
group by iID_MucLucNganSach, sLoaiTroCap

UPDATE BH_QTC_Quy_CheDoBHXH SET iLoaiTongHop=2 , bDaTongHop=1  WHERE ID_QTC_Quy_CheDoBHXH IN (SELECT * FROM f_split(@IdChungTu));
END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqKCB_create_data_summary]    Script Date: 11/15/2023 4:35:26 PM ******/
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
	   SUM(fTienXacNhanQuyetToanQuyNay),
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
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_bhxh]    Script Date: 11/15/2023 4:35:26 PM ******/
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
		and ((@IsTongHop = 0 and qtcn.bDaTongHop = 0 and qtcn.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  qtcn.sDSSoChungTuTongHop is not null))
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
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]    Script Date: 11/15/2023 4:35:26 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_bhxh]    Script Date: 11/15/2023 4:35:26 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chinambhxh_chitiet]    Script Date: 11/15/2023 4:35:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_chinambhxh_chitiet]
@IdChungTu uniqueidentifier,
@INamLamViec int,
@IsTongHop4Quy bit
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
		where danhmuc.iNamLamViec = @INamLamViec and danhmuc.sLNs in ('9010001', '9010002')
		
		
		
	---Lấy thông tin chi tiết chứng từ
		select 
			qtcn_ct.ID_QTC_Nam_CheDoBHXH_ChiTiet,
			qtcn_ct.iID_MucLucNganSach,
			qtcn_ct.sLoaiTroCap,

			qtcn_ct.fTienDuToanDuyet, ---3
			qtcn_ct.iSoDuToanDuocDuyet, --2

			qtcn_ct.iTongSo_ThucChi,
			qtcn_ct.fTongTien_ThucChi, ---5

			qtcn_ct.iSoSQ_ThucChi, ---6
			qtcn_ct.fTienSQ_ThucChi, ---7

			qtcn_ct.iSoQNCN_ThucChi, ----8
			qtcn_ct.fTienQNCN_ThucChi,---9

			qtcn_ct.iSoCNVCQP_ThucChi,---10
			qtcn_ct.fTienCNVCQP_ThucChi, ----11

			qtcn_ct.iSoLDHD_ThucChi, ----13
			qtcn_ct.fTienLDHD_ThucChi, ---14

			qtcn_ct.iSoHSQBS_ThucChi, ----15
			qtcn_ct.fTienHSQBS_ThucChi, ---16

			isnull(qtcn_ct.fTienDuToanDuyet,0) - isnull(qtcn_ct.fTongTien_ThucChi,0)  as fTienThua,
			isnull(qtcn_ct.fTongTien_ThucChi,0) - isnull(qtcn_ct.fTienDuToanDuyet,0) as  fTienThieu

		into #tblQuyetToanNamChiTiet
		from BH_QTC_Nam_CheDoBHXH_ChiTiet as qtcn_ct
		inner join BH_QTC_Nam_CheDoBHXH  as qtcn on qtcn_ct.iID_QTC_Nam_CheDoBHXH = qtcn.ID_QTC_Nam_CheDoBHXH
		where qtcn_ct.iID_QTC_Nam_CheDoBHXH = @IdChungTu;

		
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
		chi_tiet.ID_QTC_Nam_CheDoBHXH_ChiTiet,
		mucluc.iID_MLNS as iID_MucLucNganSach,
		mucluc.sMoTa as sLoaiTroCap,

		chi_tiet.fTienDuToanDuyet, 
		chi_tiet.iSoDuToanDuocDuyet,

		chi_tiet.iTongSo_ThucChi, 
		chi_tiet.fTongTien_ThucChi,

		chi_tiet.iSoSQ_ThucChi,
		chi_tiet.fTienSQ_ThucChi,

		chi_tiet.iSoQNCN_ThucChi,
		chi_tiet.fTienQNCN_ThucChi,

		chi_tiet.iSoCNVCQP_ThucChi,
		chi_tiet.fTienCNVCQP_ThucChi,

		chi_tiet.iSoLDHD_ThucChi,
		chi_tiet.fTienLDHD_ThucChi,

		chi_tiet.iSoHSQBS_ThucChi,
		chi_tiet.fTienHSQBS_ThucChi,

		chi_tiet.fTienThua,
		chi_tiet.fTienThieu
	from #tblMucLucNganSach as mucluc
	left join #tblQuyetToanNamChiTiet as chi_tiet on mucluc.iID_MLNS = chi_tiet.iID_MucLucNganSach
	order by mucluc.sXauNoiMa
	
	drop table #tblMucLucNganSach;
	drop table #tblQuyetToanNamChiTiet;
end


GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chinamKCB_chitiet]    Script Date: 11/15/2023 4:35:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_chinamKCB_chitiet]
@IdChungTu uniqueidentifier,
@Lns nvarchar(max),
@INamLamViec int,
@IsTongHop4Quy bit
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
		
		
		
	---Lấy thông tin chi tiết chứng từ
		select 
			qtcn_ct.ID_QTC_Nam_KCB_QuanYDonVi_ChiTiet,
			qtcn_ct.iID_MucLucNganSach,
			qtcn_ct.sNoiDung,
			qtcn_ct.fTien_DuToanNamTruocChuyenSang,
			qtcn_ct.fTien_DuToanGiaoNamNay,
			qtcn_ct.fTien_TongDuToanDuocGiao,
			qtcn_ct.fTien_ThucChi,
			isnull(qtcn_ct.fTien_TongDuToanDuocGiao,0) - isnull(qtcn_ct.fTien_ThucChi,0)  as fTienThua,
			isnull(qtcn_ct.fTien_ThucChi,0) - isnull(qtcn_ct.fTien_TongDuToanDuocGiao,0) as  fTienThieu
		into #tblQuyetToanNamChiTiet
		from BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet as qtcn_ct
		inner join BH_QTC_Nam_KCB_QuanYDonVi  as qtcn on qtcn_ct.iID_QTC_Nam_KCB_QuanYDonVi = qtcn.ID_QTC_Nam_KCB_QuanYDonVi
		where qtcn_ct.iID_QTC_Nam_KCB_QuanYDonVi = @IdChungTu;

		
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
		chi_tiet.ID_QTC_Nam_KCB_QuanYDonVi_ChiTiet,
		mucluc.iID_MLNS as iID_MucLucNganSach,
		mucluc.sMoTa as sNoiDung,
		chi_tiet.fTien_DuToanNamTruocChuyenSang, 
		chi_tiet.fTien_DuToanGiaoNamNay,
		chi_tiet.fTien_TongDuToanDuocGiao,
		chi_tiet.fTien_ThucChi,
		chi_tiet.fTienThua,
		chi_tiet.fTienThieu
	from #tblMucLucNganSach as mucluc
	left join #tblQuyetToanNamChiTiet as chi_tiet on mucluc.iID_MLNS = chi_tiet.iID_MucLucNganSach
	order by mucluc.sXauNoiMa
	
	drop table #tblMucLucNganSach;
	drop table #tblQuyetToanNamChiTiet;
end


GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]    Script Date: 11/15/2023 4:35:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]
@IdChungTu uniqueidentifier,
@INamLamViec int
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
		where danhmuc.iNamLamViec = @INamLamViec and danhmuc.sLNs in ('9010001', '9020002')
		
		
		
	---Lấy thông tin chi tiết chứng từ
		select 
			qtcn_ct.ID_QTC_Quy_CheDoBHXH_ChiTiet,
			qtcn_ct.iID_MucLucNganSach,
			qtcn_ct.sLoaiTroCap,
			qtcn_ct.fTienDuToanDuyet,
			qtcn_ct.iSoLuyKeCuoiQuyNay,
			qtcn_ct.fTienLuyKeCuoiQuyNay,
			qtcn_ct.iSoSQ_DeNghi,
			qtcn_ct.fTienSQ_DeNghi,
			qtcn_ct.iSoQNCN_DeNghi,
			qtcn_ct.fTienQNCN_DeNghi,
			qtcn_ct.iSoCNVCQP_DeNghi,
			qtcn_ct.fTienCNVCQP_DeNghi,
			qtcn_ct.iSoHSQBS_DeNghi,
			qtcn_ct.fTienHSQBS_DeNghi,
			qtcn_ct.iTongSo_DeNghi,
			qtcn_ct.fTongTien_DeNghi,
			qtcn_ct.fTongTien_PheDuyet,
			qtcn_ct.iSoLDHD_DeNghi,
			qtcn_ct.fTienLDHD_DeNghi
		into #tblQuyetToanQuyChiTiet
		from BH_QTC_Quy_CheDoBHXH_ChiTiet as qtcn_ct
		inner join BH_QTC_Quy_CheDoBHXH  as qtcn on qtcn_ct.iID_QTC_Quy_CheDoBHXH = qtcn.ID_QTC_Quy_CheDoBHXH
		where qtcn_ct.iID_QTC_Quy_CheDoBHXH = @IdChungTu;

		
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
			chi_tiet.ID_QTC_Quy_CheDoBHXH_ChiTiet,
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
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chiquyKCB_chitiet]    Script Date: 11/15/2023 4:35:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_chiquyKCB_chitiet]
@IdChungTu uniqueidentifier,
@INamLamViec int
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
		where danhmuc.iNamLamViec = @INamLamViec and danhmuc.sLNs in ('9010004', '9010005')
		
		
		
	---Lấy thông tin chi tiết chứng từ
		select 
			qtcn_ct.ID_QTC_Quy_KCB_ChiTiet,
			qtcn_ct.iID_MucLucNganSach,
			qtcn_ct.sNoiDung,
			qtcn_ct.fTien_DuToanNamTruocChuyenSang,
			qtcn_ct.fTien_DuToanGiaoNamNay,
			qtcn_ct.fTien_TongDuToanDuocGiao,
			qtcn_ct.fTienThucChi,
			qtcn_ct.fTienQuyetToanDaDuyet,
			qtcn_ct.fTienDeNghiQuyetToanQuyNay,
			qtcn_ct.fTienXacNhanQuyetToanQuyNay
		into #tblQuyetToanQuyChiTiet
		from BH_QTC_Quy_KCB_ChiTiet as qtcn_ct
		inner join BH_QTC_Quy_KCB  as qtcn on qtcn_ct.iID_QTC_Quy_KCB = qtcn.ID_QTC_Quy_KCB
		where qtcn_ct.iID_QTC_Quy_KCB = @IdChungTu;

		
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
			chi_tiet.ID_QTC_Quy_KCB_ChiTiet,
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
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_bhxh]    Script Date: 11/15/2023 4:35:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_bhxh]
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

		---Lấy danh sách đơn vị được chọn
		select 
			iID_DonVi,
			iID_MaDonVi,
			sTenDonVi
		into #tblDonvi
		from DonVi where iNamLamViec = @INamLamViec and iID_MaDonVi  in (select * from dbo.splitstring(@IdMaDonVi)) 


		---Lấy thông tin chi tiết chứng từ
		select
			ROW_NUMBER() OVER(ORDER BY as_qtct_1.iID_MaDonVi ASC) AS sTT,
			as_qtct_1.iID_MaDonVi,
			dv.sTenDonVi,
			Sum(iSoNgayDuoi14BenhDaiNgay) as iSoNgayDuoi14BenhDaiNgay,
			Sum(fSoTienDuoi14BenhDaiNgay) as fSoTienDuoi14BenhDaiNgay,
			Sum(iSoNgayTren14BenhDaiNgay) as iSoNgayTren14BenhDaiNgay,
			Sum(fSoTienTren14BenhDaiNgay) as fSoTienTren14BenhDaiNgay,
			Sum(iSoNgayDuoi14OmKhac) as iSoNgayDuoi14OmKhac,
			Sum(fSoTienDuoi14OmKhac) as fSoTienDuoi14OmKhac,
			Sum(iSoNgayTren14OmKhac) as iSoNgayTren14OmKhac,
			Sum(fSoTienTren14OmKhac) as fSoTienTren14OmKhac,
			Sum(iSoNgayConOm) as iSoNgayConOm,
			Sum(fSoTienConOm) as fSoTienConOm,
			Sum(iSoNgayPHSK) as iSoNgayPHSK,
			Sum(fSoTienPHSK) as fSoTienPHSK,
			isnull(Sum(fSoTienDuoi14BenhDaiNgay),0) + isnull(Sum(fSoTienTren14BenhDaiNgay),0) + isnull(Sum(fSoTienDuoi14OmKhac),0) + isnull(Sum(fSoTienTren14OmKhac),0) + isnull(Sum(fSoTienConOm),0)
			+ isnull(Sum(fSoTienPHSK),0) as fTongTien
			
		from
			(
			select
				as_qtct.iID_MaDonVi,
				case when as_qtct.sXauNoiMa = '010-011-0001-0001-01-01-01' then iTongSo_DeNghi else 0 end iSoNgayDuoi14BenhDaiNgay,
				case when as_qtct.sXauNoiMa = '010-011-0001-0001-01-01-01' then fTongTien_DeNghi else 0 end fSoTienDuoi14BenhDaiNgay,
				case when as_qtct.sXauNoiMa = '010-011-0001-0001-01-01-02' then iTongSo_DeNghi else 0 end iSoNgayTren14BenhDaiNgay,
				case when as_qtct.sXauNoiMa = '010-011-0001-0001-01-01-02' then fTongTien_DeNghi else 0 end fSoTienTren14BenhDaiNgay,
				case when as_qtct.sXauNoiMa = '010-011-0001-0001-01-02-01' then iTongSo_DeNghi else 0 end iSoNgayDuoi14OmKhac,
				case when as_qtct.sXauNoiMa = '010-011-0001-0001-01-02-01' then fTongTien_DeNghi else 0 end fSoTienDuoi14OmKhac,
				case when as_qtct.sXauNoiMa = '010-011-0001-0001-01-02-02' then iTongSo_DeNghi else 0 end iSoNgayTren14OmKhac,
				case when as_qtct.sXauNoiMa = '010-011-0001-0001-01-02-02' then fTongTien_DeNghi else 0 end fSoTienTren14OmKhac,
				case when as_qtct.sXauNoiMa = '010-011-0001-0002-02-00' then iTongSo_DeNghi else 0 end iSoNgayConOm,
				case when as_qtct.sXauNoiMa = '010-011-0001-0002-02-00' then fTongTien_DeNghi else 0 end fSoTienConOm,
				case when as_qtct.sXauNoiMa = '010-011-0001-0003-03-00' then iTongSo_DeNghi else 0 end iSoNgayPHSK,
				case when as_qtct.sXauNoiMa = '010-011-0001-0003-03-00' then fTongTien_DeNghi else 0 end fSoTienPHSK
			from
			(
				select 
					qtcn.iID_MaDonVi,
					case when sLNS = '9010001' then REPLACE(sXauNoiMa,'9010001-' , '') else REPLACE(sXauNoiMa,'9010002-' , '') end sXauNoiMa,
					Sum(qtcn_ct.iTongSo_DeNghi) as iTongSo_DeNghi,
					Sum(qtcn_ct.fTongTien_DeNghi)  as fTongTien_DeNghi
				from BH_QTC_Quy_CheDoBHXH_ChiTiet as qtcn_ct
				inner join BH_QTC_Quy_CheDoBHXH  as qtcn on qtcn_ct.iID_QTC_Quy_CheDoBHXH = qtcn.ID_QTC_Quy_CheDoBHXH
				inner join #tblMucLucNganSach ml on ml.iID_MLNS = qtcn_ct.iID_MucLucNganSach
				and ((@IsTongHop = 0 and qtcn.bDaTongHop = 0 and qtcn.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  qtcn.sDSSoChungTuTongHop is not null))
				and qtcn.iQuyChungTu = @IQuy
				group by qtcn.iID_MaDonVi,case when sLNS = '9010001' then REPLACE(sXauNoiMa,'9010001-' , '') else REPLACE(sXauNoiMa,'9010002-' , '') end

			) as as_qtct) as as_qtct_1

			inner join #tblDonvi  dv on dv.iID_MaDonVi = as_qtct_1.iID_MaDonVi
			group by as_qtct_1.iID_MaDonVi, dv.sTenDonVi
		
		

		drop table #tblMucLucNganSach;
		drop table #tblDonvi


end

GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_quyettoanchicacchedo_bhxh]    Script Date: 11/15/2023 4:35:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_quyettoanchicacchedo_bhxh]
@INamLamViec int,
@IdDonVi NVARCHAR(MAX),
@DonViTinh int,
@LNS NVARCHAR(MAX),
@IsTongHop int
as
begin
	--- Lấy danh sách các đơn vi được chọn
	select 
		ROW_NUMBER() OVER(PARTITION BY DonVi.iKhoi  ORDER BY DonVi.iID_MaDonVi ASC) AS sTT,
		DonVi.iID_DonVi,
		DonVi.iID_MaDonVi,
		DonVi.sTenDonVi,
		DonVi.iKhoi
	into  #tblDonVi
	from DonVi where iNamLamViec = @INamLamViec and iID_MaDonVi  IN (SELECT * FROM f_split(@IdDonVi)) ;

	--- Lấy danh sách mục lục ngân sách
	select 
		BH_DM_MucLucNganSach.iID_MLNS as iID_MLNS,
		BH_DM_MucLucNganSach.iID_MLNS_Cha,
		BH_DM_MucLucNganSach.sLNS,
		BH_DM_MucLucNganSach.sL,
		BH_DM_MucLucNganSach.sK,
		BH_DM_MucLucNganSach.sM,
		BH_DM_MucLucNganSach.sTM,
		BH_DM_MucLucNganSach.sTTM,
		BH_DM_MucLucNganSach.sNG,
		BH_DM_MucLucNganSach.sTNG,
		BH_DM_MucLucNganSach.sXauNoiMa,
		BH_DM_MucLucNganSach.sMoTa as sNoiDung,
		BH_DM_MucLucNganSach.bHangCha
		into #tblMucLucNganSach
		from BH_DM_MucLucNganSach 
		where   iNamLamViec = @INamLamViec  and (sLNS  in ('9010001', '9010002'))


	--- hiển thị mục lục ngân sách theo đơn vị
	select  
		case when #tblMucLucNganSach.sLNS = '9010001' then N'     Khối dự toán' else N'     Khối hạch toán' end sTenDonVi,
		#tblMucLucNganSach.sLNS,
		#tblDonVi.iID_DonVi,
		#tblDonVi.iID_MaDonVi,
		#tblDonVi.iKhoi
	into #donvi_MLNS
	from #tblMucLucNganSach cross join #tblDonVi 
	where #tblMucLucNganSach.iID_MLNS_Cha is null

	---Lấy thông tin quyết toán chi tiết 
	select 
		tbl_qtc.iKhoi,
		tbl_qtc.iID_MaDonVi,
		tbl_qtc.sLNS,
		tbl_qtc.sL,
		tbl_qtc.sK,
		tbl_qtc.sM,
		case when tbl_qtc.sM = 1 then tbl_qtc.fTienDuToanDuyet else 0 end fTroCapOmDau,
		case when tbl_qtc.sM = 2 then tbl_qtc.fTienDuToanDuyet else 0 end fTroCapThaiSan,
		case when tbl_qtc.sM = 3 then tbl_qtc.fTienDuToanDuyet else 0 end fTroCapTaiNanNN,
		case when tbl_qtc.sM = 4 then tbl_qtc.fTienDuToanDuyet else 0 end fTroCapHuuTri,
		case when tbl_qtc.sM = 5 then tbl_qtc.fTienDuToanDuyet else 0 end fTroCapPhucVien,
		case when tbl_qtc.sM = 6 then tbl_qtc.fTienDuToanDuyet else 0 end fTroCapXuatNgu,
		case when tbl_qtc.sM = 7 then tbl_qtc.fTienDuToanDuyet else 0 end fTroCapThoiViec,
		case when tbl_qtc.sM = 8 then tbl_qtc.fTienDuToanDuyet else 0 end fTroCapTuTuat
	into #tbl_qtcn_chitiet
	from
	(
		select 
			Sum(qtcn_ct.fTienDuToanDuyet) as fTienDuToanDuyet,
			#tblDonVi.iKhoi,
			#tblDonVi.iID_MaDonVi,
			#tblMucLucNganSach.sLNS,
			#tblMucLucNganSach.sL,
			#tblMucLucNganSach.sK,
			#tblMucLucNganSach.sM

		from BH_QTC_Nam_CheDoBHXH_ChiTiet as qtcn_ct
		inner join BH_QTC_Nam_CheDoBHXH  as qtcn on qtcn_ct.iID_QTC_Nam_CheDoBHXH = qtcn.ID_QTC_Nam_CheDoBHXH
		inner join #tblMucLucNganSach on qtcn_ct.iID_MucLucNganSach = #tblMucLucNganSach.iID_MLNS
		inner join #tblDonVi on qtcn.iID_MaDonVi = #tblDonVi.iID_MaDonVi
		where qtcn.iNamChungTu = @INamLamViec 
		--and ((@IsTongHop = 0 and qtcn.bDaTongHop = 0 and qtcn.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  qtcn.sDSSoChungTuTongHop is not null))
		group by #tblDonVi.iKhoi, #tblDonVi.iID_MaDonVi, #tblMucLucNganSach.sLNS, #tblMucLucNganSach.sL,#tblMucLucNganSach.sK,#tblMucLucNganSach.sM
	) as tbl_qtc


	--- Lấy dữ liệu cấp nhỏ nhất - cấp 4
	select 
		null as STT,
		#donvi_MLNS.sTenDonVi,
		#donvi_MLNS.iID_MaDonVi,
		#donvi_MLNS.iKhoi,
		#donvi_MLNS.sLNS,
		sum(#tbl_qtcn_chitiet.fTroCapOmDau) as fTroCapOmDau,
		sum(#tbl_qtcn_chitiet.fTroCapThaiSan) as fTroCapThaiSan,
		sum(#tbl_qtcn_chitiet.fTroCapTaiNanNN) as fTroCapTaiNanNN,
		sum(#tbl_qtcn_chitiet.fTroCapHuuTri) as fTroCapHuuTri,
		sum(#tbl_qtcn_chitiet.fTroCapPhucVien) as fTroCapPhucVien,
		sum(#tbl_qtcn_chitiet.fTroCapXuatNgu) as fTroCapXuatNgu,
		sum(#tbl_qtcn_chitiet.fTroCapThoiViec) as fTroCapThoiViec,
		sum(#tbl_qtcn_chitiet.fTroCapTuTuat) as fTroCapTuTuat,
		4 as level,
		0 as bHangCha
	into #tbl_cap4
	from #donvi_MLNS 
	left join #tbl_qtcn_chitiet on #donvi_MLNS.iID_MaDonVi = #tbl_qtcn_chitiet.iID_MaDonVi and #donvi_MLNS.iKhoi = #tbl_qtcn_chitiet.iKhoi
	and #tbl_qtcn_chitiet.sLNS = #donvi_MLNS.sLNS
	group by #donvi_MLNS.sTenDonVi, #donvi_MLNS.iID_MaDonVi, #donvi_MLNS.iKhoi, #donvi_MLNS.sLNS
	order by #donvi_MLNS.iKhoi,#donvi_MLNS.iID_MaDonVi

	--- Lấy dữ liệu cấp 3
	select 
		#tblDonVi.sTT,
		#tblDonVi.sTenDonVi ,
		#tblDonVi.iID_MaDonVi,
		#tblDonVi.iKhoi,
		'' as sLNS, 
		sum(#tbl_cap4.fTroCapOmDau) as fTroCapOmDau,
		sum(#tbl_cap4.fTroCapThaiSan) as fTroCapThaiSan,
		sum(#tbl_cap4.fTroCapTaiNanNN) as fTroCapTaiNanNN,
		sum(#tbl_cap4.fTroCapHuuTri) as fTroCapHuuTri,
		sum(#tbl_cap4.fTroCapPhucVien) as fTroCapPhucVien,
		sum(#tbl_cap4.fTroCapXuatNgu) as fTroCapXuatNgu,
		sum(#tbl_cap4.fTroCapThoiViec) as fTroCapThoiViec,
		sum(#tbl_cap4.fTroCapTuTuat) as fTroCapTuTuat,
		3 as level,
		0 as bHangCha
	into #tbl_cap3
	from #tblDonVi
	left join #tbl_cap4 on #tblDonVi.iID_MaDonVi = #tbl_cap4.iID_MaDonVi and #tblDonVi.iKhoi = #tbl_cap4.iKhoi
	group by #tblDonVi.sTT, #tblDonVi.sTenDonVi, #tblDonVi.iID_MaDonVi, #tblDonVi.iKhoi

	---Lấy dữ liệu đơn vị cấp 2
	select 
		null as STT,
		case when #tbl_cap4.sLNS = '9010001' then N'   +Khối dự toán' else N'   +Khối hạch toán' end sTenDonVi,
		'' as iID_MaDonVi,
		#tbl_cap4.iKhoi,
		#tbl_cap4.sLNS as sLNS, 
		sum(fTroCapOmDau) as fTroCapOmDau,
		sum(fTroCapThaiSan) as fTroCapThaiSan,
		sum(fTroCapTaiNanNN) as fTroCapTaiNanNN,
		sum(fTroCapHuuTri) as fTroCapHuuTri,
		sum(fTroCapPhucVien) as fTroCapPhucVien,
		sum(fTroCapXuatNgu) as fTroCapXuatNgu,
		sum(fTroCapThoiViec) as fTroCapThoiViec,
		sum(fTroCapTuTuat) as fTroCapTuTuat,
		2 as level,
		1 as bHangCha
	into #tbl_cap2
	from #tbl_cap4
	group by #tbl_cap4.iKhoi, #tbl_cap4.sLNS


	---Lấy dữ liệu đơn vị cấp 1
	select 
		null as STT,
		case when #tbl_cap4.iKhoi = 2 then N'   A.Đơn vị Dự toán' else N'   B.Đơn vị Hạch toán' end sTenDonVi,
		'' as iID_MaDonVi,
		#tbl_cap4.iKhoi,
		'' as sLNS, 
		sum(fTroCapOmDau) as fTroCapOmDau,
		sum(fTroCapThaiSan) as fTroCapThaiSan,
		sum(fTroCapTaiNanNN) as fTroCapTaiNanNN,
		sum(fTroCapHuuTri) as fTroCapHuuTri,
		sum(fTroCapPhucVien) as fTroCapPhucVien,
		sum(fTroCapXuatNgu) as fTroCapXuatNgu,
		sum(fTroCapThoiViec) as fTroCapThoiViec,
		sum(fTroCapTuTuat) as fTroCapTuTuat,
		1 as level,
		1 as bHangCha
	into #tbl_cap1
	from #tbl_cap4
	group by #tbl_cap4.iKhoi

	---Hiển thị kết quả trả về
	select 
		sTT,
		sTenDonVi,
		iID_MaDonVi,
		iKhoi,
		sLNS,
		fTroCapOmDau,
		fTroCapThaiSan,
		fTroCapTaiNanNN,
		fTroCapHuuTri,
		fTroCapPhucVien,
		fTroCapXuatNgu,
		fTroCapThoiViec,
		fTroCapTuTuat,
		level,
		bHangCha
	into tblResult
	from
		(
		select * from #tbl_cap1
		union all 
		select * from #tbl_cap2
		union all 
		select * from #tbl_cap3
		union all 
		select * from #tbl_cap4) as tblrt
	where isnull(fTroCapOmDau,0) != 0 or isnull(fTroCapThaiSan,0) != 0 or isnull(fTroCapTaiNanNN,0) != 0 or isnull(fTroCapHuuTri,0) != 0 or isnull(fTroCapPhucVien,0) != 0
			or isnull(fTroCapXuatNgu,0) != 0 or isnull(fTroCapThoiViec,0) != 0 or isnull(fTroCapTuTuat,0) != 0
	order by iKhoi desc,iID_MaDonVi,level, sLNS
	
	----insert dòng tổng cộng
	insert into tblResult (sTenDonVi, iID_MaDonVi, iKhoi, fTroCapOmDau, fTroCapThaiSan, fTroCapTaiNanNN, fTroCapHuuTri, fTroCapPhucVien, fTroCapXuatNgu, fTroCapThoiViec, fTroCapTuTuat,
							level, bHangCha)
	select * from
		(
			select  N'          Tổng cộng' as sTenDonVi,
					'' as iID_MaDonVi,
					null as iKhoi,
					sum(fTroCapOmDau) as fTroCapOmDau,
					sum(fTroCapThaiSan) as fTroCapThaiSan,
					sum(fTroCapTaiNanNN) as fTroCapTaiNanNN,
					sum(fTroCapHuuTri) as fTroCapHuuTri,
					sum(fTroCapPhucVien) as fTroCapPhucVien,
					sum(fTroCapXuatNgu) as fTroCapXuatNgu,
					sum(fTroCapThoiViec) as fTroCapThoiViec,
					sum(fTroCapTuTuat) as fTroCapTuTuat,
					7 as level,
					1 as bHangCha
			from tblResult
			where tblResult.level = 1
		) as tbltongcong


	---- Insert dòng dự toán
	insert into tblResult (sTenDonVi, iID_MaDonVi, iKhoi, fTroCapOmDau, fTroCapThaiSan, fTroCapTaiNanNN, fTroCapHuuTri, fTroCapPhucVien, fTroCapXuatNgu, fTroCapThoiViec, fTroCapTuTuat,
							level, bHangCha)
	select * from
		(
			select  N'        Khối Dự toán' as sTenDonVi,
					'' as iID_MaDonVi,
					null as iKhoi,
					sum(fTroCapOmDau) as fTroCapOmDau,
					sum(fTroCapThaiSan) as fTroCapThaiSan,
					sum(fTroCapTaiNanNN) as fTroCapTaiNanNN,
					sum(fTroCapHuuTri) as fTroCapHuuTri,
					sum(fTroCapPhucVien) as fTroCapPhucVien,
					sum(fTroCapXuatNgu) as fTroCapXuatNgu,
					sum(fTroCapThoiViec) as fTroCapThoiViec,
					sum(fTroCapTuTuat) as fTroCapTuTuat,
					8 as level,
					1 as bHangCha
			from tblResult
			where tblResult.level = 4 and tblResult.sLNS = '9010001'
		) as tbldutoan



	---- Insert dòng Hạch toán
	insert into tblResult (sTenDonVi, iID_MaDonVi, iKhoi, fTroCapOmDau, fTroCapThaiSan, fTroCapTaiNanNN, fTroCapHuuTri, fTroCapPhucVien, fTroCapXuatNgu, fTroCapThoiViec, fTroCapTuTuat,
							level, bHangCha)
	select * from
		(
			select  N'        Khối Hạch toán' as sTenDonVi,
					'' as iID_MaDonVi,
					null as iKhoi,
					sum(fTroCapOmDau) as fTroCapOmDau,
					sum(fTroCapThaiSan) as fTroCapThaiSan,
					sum(fTroCapTaiNanNN) as fTroCapTaiNanNN,
					sum(fTroCapHuuTri) as fTroCapHuuTri,
					sum(fTroCapPhucVien) as fTroCapPhucVien,
					sum(fTroCapXuatNgu) as fTroCapXuatNgu,
					sum(fTroCapThoiViec) as fTroCapThoiViec,
					sum(fTroCapTuTuat) as fTroCapTuTuat,
					9 as level,
					1 as bHangCha
			from tblResult
			where tblResult.level = 4 and  tblResult.sLNS = '9010002'
		) as tbldutoan


	select  * from tblResult order by iKhoi desc,iID_MaDonVi,level, sLNS


	drop table #tblDonVi;
	drop table  #tblMucLucNganSach;
	drop table #donvi_MLNS;
	drop table #tbl_qtcn_chitiet;
	drop table #tbl_cap4;
	drop table #tbl_cap3;
	drop table #tbl_cap2;
	drop table #tbl_cap1;
	drop table tblResult;
end
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_get_donvi_nBHXH_lns]    Script Date: 11/15/2023 4:35:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[sp_cp_rpt_get_donvi_nBHXH_lns]
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
  AND chungtu.iLoaiTongHop=@LoaiChungTu
ORDER BY cast(chungtu.dNgayChungTu AS date) END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_get_donvi_nKCB_lns]    Script Date: 11/15/2023 4:35:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[sp_cp_rpt_get_donvi_nKCB_lns]
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
  AND chungtu.iLoaiTongHop=@LoaiChungTu
ORDER BY cast(chungtu.dNgayChungTu AS date) END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_get_donvi_nKPK_lns]    Script Date: 11/15/2023 4:35:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[sp_cp_rpt_get_donvi_nKPK_lns]
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
  AND chungtu.iLoaiTongHop=@LoaiChungTu
  AND chungtu.iID_LoaiChi=@LoaiChi
ORDER BY cast(chungtu.dNgayChungTu AS date) END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_get_donvi_nKPQL_lns]    Script Date: 11/15/2023 4:35:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[sp_cp_rpt_get_donvi_nKPQL_lns]
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
  AND chungtu.iLoaiTongHop=@LoaiChungTu
ORDER BY cast(chungtu.dNgayChungTu AS date) END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_get_donvi_qBHXH_lns]    Script Date: 11/15/2023 4:35:26 PM ******/
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
  AND chungtu.iLoaiTongHop=@LoaiChungTu
ORDER BY cast(chungtu.dNgayChungTu AS date) END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_get_donvi_qkcb_lns]    Script Date: 11/15/2023 4:35:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[sp_cp_rpt_get_donvi_qkcb_lns]
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
  AND chungtu.iLoaiTongHop=@LoaiChungTu
ORDER BY cast(chungtu.dNgayChungTu AS date) END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_get_donvi_qKPK_lns]    Script Date: 11/15/2023 4:35:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[sp_cp_rpt_get_donvi_qKPK_lns]
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
  AND chungtu.iLoaiTongHop=@LoaiChungTu
  AND chungtu.iID_LoaiChi=@LoaiChi
ORDER BY cast(chungtu.dNgayChungTu AS date) END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_get_donvi_qKPQL_lns]    Script Date: 11/15/2023 4:35:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[sp_cp_rpt_get_donvi_qKPQL_lns]
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
  AND chungtu.iLoaiTongHop=@LoaiChungTu
ORDER BY cast(chungtu.dNgayChungTu AS date) END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_nbhxh]    Script Date: 11/15/2023 4:35:26 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_nkcb]    Script Date: 11/15/2023 4:35:26 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_nkpk]    Script Date: 11/15/2023 4:35:26 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_nkpql]    Script Date: 11/15/2023 4:35:26 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_qbhxh]    Script Date: 11/15/2023 4:35:26 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_qkcb]    Script Date: 11/15/2023 4:35:26 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_qkpk]    Script Date: 11/15/2023 4:35:26 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_qkpql]    Script Date: 11/15/2023 4:35:26 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_nqlkp_kehoach_bh]    Script Date: 11/15/2023 4:35:26 PM ******/
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
		ISNULL(ctct.fTien_DuToanNamTruocChuyenSang, 0) / @DonViTnh  as fTien_DuToanNamTruocChuyenSang, 
		ISNULL(ctct.fTien_DuToanGiaoNamNay, 0) / @DonViTnh  as fTien_DuToanGiaoNamNay,
		ISNULL(ctct.fTien_DuToanNamTruocChuyenSang, 0) / @DonViTnh + ISNULL(ctct.fTien_DuToanGiaoNamNay, 0)  as fTien_TongDuToanDuocGiao, 
		ISNULL(ctct.fTien_ThucChi, 0) / @DonViTnh  as fTien_ThucChi, 
		ISNULL(ctct.fTien_ThucChi, 0) / @DonViTnh  as fTienThua, 
		ISNULL(ctct.fTienThieu, 0) / @DonViTnh  as fTienThieu, 
		ISNULL(ctct.fTiLeThucHienTrenDuToan, 0) / @DonViTnh  as fTiLeThucHienTrenDuToan
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
	Order by mlns.sXauNoiMa

	drop table #tblMlnsByPhanCap;

END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkp_ql_thongtriloai1_bh]    Script Date: 11/15/2023 4:35:26 PM ******/
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
					AND ct.iLoaiTongHop=@iLoaiTongHop
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
					AND ct.iLoaiTongHop=@iLoaiTongHop
					AND ct.iID_MaDonVi IN (select * from f_split(@IdDonVi))
	END

END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkp_ql_thongtriloai2_bh]    Script Date: 11/15/2023 4:35:26 PM ******/
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
		ISNULL(ctct.fTienXacNhanQuyetToanQuyNay, 0) / @Dvt as FTienXacNhanQuyetToanQuyNay 
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
