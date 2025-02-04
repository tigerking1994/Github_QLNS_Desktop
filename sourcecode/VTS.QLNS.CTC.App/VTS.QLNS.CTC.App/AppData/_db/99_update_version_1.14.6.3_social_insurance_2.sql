/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkpk_kehoach_bh]    Script Date: 6/20/2024 2:58:43 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qtc_qkpk_kehoach_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qtc_qkpk_kehoach_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkp_ql_thongtriloai2_bh]    Script Date: 6/20/2024 2:58:43 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qtc_qkp_ql_thongtriloai2_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qtc_qkp_ql_thongtriloai2_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkp_ql_chungtu_chi_tiet]    Script Date: 6/20/2024 2:58:43 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qtc_qkp_ql_chungtu_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qtc_qkp_ql_chungtu_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_kcb]    Script Date: 6/20/2024 2:58:43 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_kcb]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_kcb]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_bhxh]    Script Date: 6/20/2024 2:58:43 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_create_quyet_toan_chinamKPQL_chitiet_theo4quy]    Script Date: 6/20/2024 2:58:43 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_create_quyet_toan_chinamKPQL_chitiet_theo4quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_create_quyet_toan_chinamKPQL_chitiet_theo4quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_create_quyet_toan_chinamKPK_chitiet_theo4quy]    Script Date: 6/20/2024 2:58:43 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_create_quyet_toan_chinamKPK_chitiet_theo4quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_create_quyet_toan_chinamKPK_chitiet_theo4quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_create_quyet_toan_chinamkcb_chitiet_theo4quy]    Script Date: 6/20/2024 2:58:43 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_create_quyet_toan_chinamkcb_chitiet_theo4quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_create_quyet_toan_chinamkcb_chitiet_theo4quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_create_quyet_toan_chinambhxh_chitiet_theo4quy]    Script Date: 6/20/2024 2:58:43 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_create_quyet_toan_chinambhxh_chitiet_theo4quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_create_quyet_toan_chinambhxh_chitiet_theo4quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]    Script Date: 6/20/2024 4:51:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_create_quyet_toan_chinambhxh_chitiet_theo4quy]    Script Date: 6/20/2024 2:58:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_create_quyet_toan_chinambhxh_chitiet_theo4quy]
@IdChungTu uniqueidentifier,
@IdMaDonVi nvarchar(50),
@INamLamViec int,
@User  nvarchar(50),
@IsTongHop bit
as
begin
	insert into BH_QTC_Nam_CheDoBHXH_ChiTiet (	
			ID_QTC_Nam_CheDoBHXH_ChiTiet,
			iID_QTC_Nam_CheDoBHXH, 
			iID_MucLucNganSach, 
			sLoaiTroCap,
			sXauNoiMa,
			iID_MaDonVi,
			iNamLamViec,
			dNgaySua,
			dNgayTao,
			sNguoiSua,
			sNguoiTao,
			fTienDuToanDuyet,
			iSoSQ_ThucChi,
			fTienSQ_ThucChi,
			iSoQNCN_ThucChi,
			fTienQNCN_ThucChi,
			iSoCNVCQP_ThucChi,
			fTienCNVCQP_ThucChi,
			iSoHSQBS_ThucChi,
			fTienHSQBS_ThucChi,
			iSoLDHD_ThucChi,
			fTienLDHD_ThucChi,
			iTongSo_ThucChi,
			fTongTien_ThucChi
			)
	select 
			NEWID(),
			@IdChungTu,
			tb_qtcy.iID_MucLucNganSach,
			tb_qtcy.sLoaiTroCap,
			tb_qtcy.sXauNoiMa,
			tb_qtcy.iIDMaDonVi,
			tb_qtcy.iNamLamViec,
			null,
			null,
			null,
			@User,
			tb_qtcy.fTienDuToanDuyet,
			tb_qtcy.iSoSQ_ThucChi,
			tb_qtcy.fTienSQ_ThucChi,
			tb_qtcy.iSoQNCN_ThucChi,
			tb_qtcy.fTienQNCN_ThucChi,
			tb_qtcy.iSoCNVCQP_ThucChi,
			tb_qtcy.fTienCNVCQP_ThucChi,
			tb_qtcy.iSoHSQBS_ThucChi,
			tb_qtcy.fTienHSQBS_ThucChi,
			tb_qtcy.iSoLDHD_ThucChi,
			tb_qtcy.fTienLDHD_ThucChi,
			tb_qtcy.iTongSo_ThucChi,
			tb_qtcy.fTongTien_ThucChi

	from 
	(
		select 
				ctqt_quy_chitiet.iID_MucLucNganSach, 
				ctqt_quy_chitiet.sLoaiTroCap,
				ctqt_quy_chitiet.sXauNoiMa,
				ctqt_quy_chitiet.iNamLamViec,
				ctqt_quy_chitiet.iIDMaDonVi,
				null as fTienDuToanDuyet,
				SUM(ctqt_quy_chitiet.iSoSQ_DeNghi) as iSoSQ_ThucChi,
				SUM(ctqt_quy_chitiet.fTienSQ_DeNghi) as fTienSQ_ThucChi,
				SUM(ctqt_quy_chitiet.iSoQNCN_DeNghi) as iSoQNCN_ThucChi,
				SUM(ctqt_quy_chitiet.fTienQNCN_DeNghi) as fTienQNCN_ThucChi,
				SUM(ctqt_quy_chitiet.iSoCNVCQP_DeNghi) as iSoCNVCQP_ThucChi,
				SUM(ctqt_quy_chitiet.fTienCNVCQP_DeNghi) as fTienCNVCQP_ThucChi,
				SUM(ctqt_quy_chitiet.iSoHSQBS_DeNghi) as iSoHSQBS_ThucChi,
				SUM(ctqt_quy_chitiet.fTienHSQBS_DeNghi) as fTienHSQBS_ThucChi,
				SUM(ctqt_quy_chitiet.iSoLDHD_DeNghi) as iSoLDHD_ThucChi,
				SUM(ctqt_quy_chitiet.fTienLDHD_DeNghi) as fTienLDHD_ThucChi,
				SUM(ctqt_quy_chitiet.iTongSo_DeNghi) as iTongSo_ThucChi,
				SUM(ctqt_quy_chitiet.fTongTien_DeNghi) as fTongTien_ThucChi

		from BH_QTC_Quy_CheDoBHXH as ctqt_quy
		inner join BH_QTC_Quy_CheDoBHXH_ChiTiet as ctqt_quy_chitiet on ctqt_quy.ID_QTC_Quy_CheDoBHXH = ctqt_quy_chitiet.iID_QTC_Quy_CheDoBHXH
		where ctqt_quy.iID_MaDonVi = @IdMaDonVi and ctqt_quy.iNamChungTu = @INamLamViec
				and ( ctqt_quy_chitiet.iTongSo_DeNghi >0
				or ctqt_quy_chitiet.fTongTien_DeNghi>0)
		--and ((@IsTongHop = 0 and ctqt_quy.bDaTongHop = 0 and ctqt_quy.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  ctqt_quy.sDSSoChungTuTongHop is not null))
		group by ctqt_quy_chitiet.iID_MucLucNganSach, ctqt_quy_chitiet.sLoaiTroCap
				,ctqt_quy_chitiet.sXauNoiMa,
				ctqt_quy_chitiet.iNamLamViec,
				ctqt_quy_chitiet.iIDMaDonVi) as tb_qtcy

end
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_create_quyet_toan_chinamkcb_chitiet_theo4quy]    Script Date: 6/20/2024 2:58:43 PM ******/
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
			sXauNoiMa,
			iNamLamViec,
			iID_MaDonVi,
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
			tb_qtcy.sXauNoiMa,
			tb_qtcy.iNamLamViec,
			tb_qtcy.iID_MaDonVi,
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
				ctqt_quy_chitiet.sXauNoiMa,
				ctqt_quy_chitiet.iNamLamViec,
				ctqt_quy_chitiet.iID_MaDonVi, 
				SUM(ctqt_quy_chitiet.FTienDeNghiQuyetToanQuyNay) as fTien_ThucChi
				
		from BH_QTC_Quy_KCB as ctqt_quy
		inner join BH_QTC_Quy_KCB_ChiTiet as ctqt_quy_chitiet on ctqt_quy.ID_QTC_Quy_KCB = ctqt_quy_chitiet.iID_QTC_Quy_KCB
		where ctqt_quy.iID_MaDonVi = @IdMaDonVi and ctqt_quy.iNamChungTu = @INamLamViec
		and ctqt_quy_chitiet.iNamLamViec=@INamLamViec
		and ctqt_quy_chitiet.FTienDeNghiQuyetToanQuyNay>0
				--and ctqt_quy.iQuyChungTu=4
		--and ((@IsTongHop = 0 and ctqt_quy.bDaTongHop = 0 and ctqt_quy.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  ctqt_quy.sDSSoChungTuTongHop is not null))
		group by ctqt_quy_chitiet.iID_MucLucNganSach,
				ctqt_quy_chitiet.sNoiDung,
				ctqt_quy_chitiet.sXauNoiMa,
				ctqt_quy_chitiet.iNamLamViec,
				ctqt_quy_chitiet.iID_MaDonVi) as tb_qtcy

end
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_create_quyet_toan_chinamKPK_chitiet_theo4quy]    Script Date: 6/20/2024 2:58:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_create_quyet_toan_chinamKPK_chitiet_theo4quy]
@IdChungTu uniqueidentifier,
@IdMaDonVi nvarchar(max),
@INamLamViec int,
@User  nvarchar(50),
@IDLoaiCap uniqueidentifier
as
begin
INSERT INTO BH_QTC_Nam_KPK_ChiTiet
(
	ID_QTC_Nam_KPK_ChiTiet,
	iID_QTC_Nam_KPK,
	iID_MucLucNganSach,
	sNoiDung,
	sXauNoiMa,
	iID_MaDonVi,
	iNamLamViec,
	dNgaySua,
	dNgayTao,
	sNguoiSua,
	sNguoiTao,
	fTien_ThucChi
)
SELECT 
	NEWID(),
	@IdChungTu,
	tb_qtcy.iID_MucLucNganSach,
	tb_qtcy.sNoiDung,
	tb_qtcy.sXauNoiMa,
	tb_qtcy.iIDMaDonVi,
	tb_qtcy.iNamLamViec,
	NULL,
	GETDATE(),
	NULL,
	@User,
	tb_qtcy.fTienThucChi
	FROM 
	(
		SELECT 
			CTCT.iID_MucLucNganSach,
			CTCT.sNoiDung,
			CTCT.sXauNoiMa,
			CTCT.iIDMaDonVi,
			CTCT.iNamLamViec,
			ISNULL(CTCT.fTienDeNghiQuyetToanQuyNay, 0) fTienThucChi
		FROM
		BH_QTC_Quy_KPK AS CT
		LEFT JOIN BH_QTC_Quy_KPK_ChiTiet CTCT ON  CT.ID_QTC_Quy_KPK=CTCT.iID_QTC_Quy_KPK
		WHERE CT.iID_MaDonVi=@IdMaDonVi
			AND CTCT.iNamLamViec=@INamLamViec
			--AND CT.iQuyChungTu=4
			--AND CT.bIsKhoa=1
			AND CT.iNamChungTu=@INamLamViec
			AND  CT.iID_LoaiChi=@IDLoaiCap
	) as tb_qtcy;

end
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_create_quyet_toan_chinamKPQL_chitiet_theo4quy]    Script Date: 6/20/2024 2:58:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_create_quyet_toan_chinamKPQL_chitiet_theo4quy]
@IdChungTu uniqueidentifier,
@IdMaDonVi nvarchar(max),
@INamLamViec int,
@User  nvarchar(50)
as
begin
INSERT INTO BH_QTC_Nam_KinhPhiQuanLy_ChiTiet
(
	ID_QTC_Nam_KinhPhiQuanLy_ChiTiet,
	iID_QTC_Nam_KinhPhiQuanLy,
	iID_MucLucNganSach,
	sM,
	sTM,
	sNoiDung,
	sXauNoiMa,
	iID_MaDonVi,
	iNamLamViec,
	dNgaySua,
	dNgayTao,
	sNguoiSua,
	sNguoiTao,
	fTien_ThucChi
)
SELECT 
	NEWID(),
	@IdChungTu,
	tb_qtcy.iID_MucLucNganSach,
	tb_qtcy.sM,
	tb_qtcy.sTM,
	tb_qtcy.sNoiDung,
	tb_qtcy.sXauNoiMa,
	tb_qtcy.iIDMaDonVi,
	tb_qtcy.iNamLamViec,
	NULL,
	GETDATE(),
	NULL,
	@user,
	tb_qtcy.fTienThucChi
	FROM 
	(
		SELECT 
			CTCT.iID_MucLucNganSach,
			CTCT.sM,
			CTCT.sTM,
			CTCT.sNoiDung,
			CTCT.sXauNoiMa,
			CTCT.iIDMaDonVi,
			CTCT.iNamLamViec,
			sum(CTCT.fTienDeNghiQuyetToanQuyNay) fTienThucChi
		FROM
		BH_QTC_Quy_KinhPhiQuanLy AS CT
		LEFT JOIN BH_QTC_Quy_KinhPhiQuanLy_ChiTiet CTCT ON  CT.ID_QTC_Quy_KinhPhiQuanLy=CTCT.iID_QTC_Quy_KinhPhiQuanLy
		WHERE CT.iID_MaDonVi=@IdMaDonVi
		and CTCT.fTienDeNghiQuyetToanQuyNay>0
		AND CTCT.iNamLamViec=@INamLamViec
			AND CT.iNamChungTu=@INamLamViec
			GROUP BY CTCT.iID_MucLucNganSach,
			CTCT.sM,
			CTCT.sTM,
			CTCT.sNoiDung,
			CTCT.sXauNoiMa,
			CTCT.iIDMaDonVi,
			CTCT.iNamLamViec
	) as tb_qtcy;

end
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_bhxh]    Script Date: 6/20/2024 2:58:43 PM ******/
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

		--- Get tien du toan 
		SELECT ctct.sXauNoiMa,
			SUM(ctct.fTienTuChi)  fTienDuToanDuyet
			into #tempDuToanTrenGiao
			FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct 
			JOIN BH_DTC_DuToanChiTrenGiao ct ON ctct.iID_DTC_DuToanChiTrenGiao = ct.ID 
				WHERE ctct.iID_MaDonVi in (select * from dbo.splitstring(@IdMaDonVi)) 
				AND BIsKhoa = 1
				AND ct.iNamLamViec = @INamLamViec
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
			(
				SELECT fTienCNVCQP_DeNghi, fTienHSQBS_DeNghi, fTienLDHD_DeNghi, fTienQNCN_DeNghi, fTienSQ_DeNghi, iSoCNVCQP_DeNghi, fTongTien_PheDuyet, iSoHSQBS_DeNghi,
					iSoLDHD_DeNghi,  iSoQNCN_DeNghi, iSoSQ_DeNghi, iID_QTC_Quy_CheDoBHXH, ct.sXauNoiMa
				FROM BH_QTC_Quy_CheDoBHXH_ChiTiet ct
				INNER JOIN BH_DM_MucLucNganSach dmns on ct.sXauNoiMa = dmns.sXauNoiMa
				where dmns.bHangCha = 0 and dmns.iNamLamViec = @INamLamViec
			)ctct
			INNER JOIN BH_QTC_Quy_CheDoBHXH ct ON ctct.iID_QTC_Quy_CheDoBHXH = ct.ID_QTC_Quy_CheDoBHXH 
		WHERE
			ct.iQuyChungTu < @IQuy 
		GROUP BY
			ct.iID_MaDonVi,
			ct.iNamChungTu,
			ctct.sXauNoiMa

	IF @IsTongHop=0
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
			left join #tempDuLieuQuyTruoc duLieuQuyTruoc on mucluc.sXauNoiMa=duLieuQuyTruoc.sXauNoiMa
			order by mucluc.sXauNoiMa
	ELSE
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
			left join #tblQuyetToanQuyChiTiet as chi_tiet on mucluc.iID_MLNS = chi_tiet.iID_MucLucNganSach
			left join #tempDuToanTrenGiao duToan on mucluc.sXauNoiMa=duToan.sXauNoiMa
			left join #tempDuLieuQuyTruoc duLieuQuyTruoc on mucluc.sXauNoiMa=duLieuQuyTruoc.sXauNoiMa
			order by mucluc.sXauNoiMa

	drop table #tblMucLucNganSach;
	drop table #tblQuyetToanQuyChiTiet;
	drop table #tempDuToan;
	drop table #tempDuLieuQuyTruoc;

end
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_kcb]    Script Date: 6/20/2024 2:58:43 PM ******/
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
	DECLARE @fSoThamDinh INT;
	SELECT @fSoThamDinh = sum(fSoThamDinh)
	FROM BH_ThamDinhQuyetToan_ChungTuChiTiet 
	WHERE iNamLamViec = @INamLamViec - 1 and iMa = 225 and  iID_MaDonVi IN (SELECT * FROM f_split(@IdMaDonVi))
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
		and iTrangThai=1
		
		
		
	---Lấy thông tin chi tiết chứng từ
		select 
			qtcn_ct.sXauNoiMa,
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
		group by qtcn_ct.sXauNoiMa,qtcn_ct.sNoiDung

   		-- lấy ra dữ liệu dự toán
	SELECT 
		  SUM(fTienTuChi) AS FTienDuToanGiaoNamNay,
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
   GROUP BY sXauNoiMa
   --- Get nhan phan bo tren giao
   	SELECT 
		  SUM(fTienTuChi) AS FTienDuToanGiaoNamNay,
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
   GROUP BY sXauNoiMa

   SELECT SUM(CTCT.fTien_DuToanNamTruocChuyenSang)  fTien_DuToanNamTruocChuyenSang,
					CTCT.sXauNoiMa
		INTO #TempChungTuQuyTruoc
	FROM BH_QTC_QUY_KCB_Chitiet CTCT
		WHERE CTCT.iID_QTC_Quy_KCB IN
	(
		SELECT ID_QTC_Quy_KCB FROM  BH_QTC_QUY_KCB 
		WHERE iID_MaDonVi IN ( SELECT * FROM  f_split(@IdMaDonVi))
					AND iNamChungTu=@INamLamViec
					AND iQuyChungTu=1
	)
	group by CTCT.sXauNoiMa

	-- lay du lieu da quyet toan 
	SELECT  
		SUM(FTienDeNghiQuyetToanQuyNay) AS fTienQuyetToanDaDuyet,
		CTCT.sXauNoiMa
		into #TemptblTienDaDuyet
	FROM
	BH_QTC_QUY_KCB CT
	INNER JOIN BH_QTC_QUY_KCB_Chitiet CTCT
	ON CT.ID_QTC_Quy_KCB=CTCT.iID_QTC_Quy_KCB
	WHERE CT.iNamChungTu = @INamLamViec
		  --AND CAST(CT.dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
		  AND CT.iID_MaDonVi IN (SELECT * FROM f_split(@IdMaDonVi))
		  --AND CT.bIsKhoa=1
		  AND CT.iQuyChungTu<@IQuy
	GROUP BY  CTCT.sXauNoiMa

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
			CASE WHEN mucluc.sXauNoiMa = @SLN THEN @fSoThamDinh  ELSE 0 END as FTienDuToanNamTruocChuyenSang,
			dt.FTienDuToanGiaoNamNay FTienDuToanGiaoNamNay,
			chi_tiet.FTienTongDuToanDuocGiao FTienTongDuToanDuocGiao,
			(isnull(chi_tiet.fTienThucChi,0) + isnull(tienDuyet.fTienQuyetToanDaDuyet,0)) fTienThucChi,
			chi_tiet.fTienQuyetToanDaDuyet fTienQuyetToanDaDuyet,
			chi_tiet.fTienDeNghiQuyetToanQuyNay fTienDeNghiQuyetToanQuyNay,
			chi_tiet.fTienXacNhanQuyetToanQuyNay fTienXacNhanQuyetToanQuyNay
		from #tblMucLucNganSach as mucluc
		left join #tblQuyetToanQuyChiTiet as chi_tiet on mucluc.sXauNoiMa = chi_tiet.sXauNoiMa
		left join #tblPhanBoDuToan as dt on mucluc.sXauNoiMa=dt.sXauNoiMa
		left join #TempChungTuQuyTruoc as quy_truoc on mucluc.sXauNoiMa=quy_truoc.sXauNoiMa
		left join #TemptblTienDaDuyet tienDuyet on mucluc.sXauNoiMa=tienDuyet.sXauNoiMa
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
			CASE WHEN mucluc.sXauNoiMa = @SLN THEN @fSoThamDinh  ELSE 0 END as FTienDuToanNamTruocChuyenSang,
			--quy_truoc.fTien_DuToanNamTruocChuyenSang as FTienDuToanNamTruocChuyenSang,
			dt.FTienDuToanGiaoNamNay FTienDuToanGiaoNamNay,
			chi_tiet.FTienTongDuToanDuocGiao FTienTongDuToanDuocGiao,
						(isnull(chi_tiet.fTienThucChi,0) + isnull(tienDuyet.fTienQuyetToanDaDuyet,0)) fTienThucChi,
			chi_tiet.fTienQuyetToanDaDuyet fTienQuyetToanDaDuyet,
			chi_tiet.fTienDeNghiQuyetToanQuyNay fTienDeNghiQuyetToanQuyNay,
			chi_tiet.fTienXacNhanQuyetToanQuyNay fTienXacNhanQuyetToanQuyNay
		from #tblMucLucNganSach as mucluc
		left join #tblQuyetToanQuyChiTiet as chi_tiet on mucluc.sXauNoiMa = chi_tiet.sXauNoiMa
		left join #tblNhanPhanBoTrenGiao as dt on mucluc.sXauNoiMa=dt.sXauNoiMa
		left join #TempChungTuQuyTruoc as quy_truoc on mucluc.sXauNoiMa=quy_truoc.sXauNoiMa
		left join #TemptblTienDaDuyet tienDuyet on mucluc.sXauNoiMa=tienDuyet.sXauNoiMa
		order by mucluc.sXauNoiMa
end
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkp_ql_chungtu_chi_tiet]    Script Date: 6/20/2024 2:58:43 PM ******/
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

	-- lấy dữ liệu đã cấp
	SELECT  
		SUM(fTienDeNghiQuyetToanQuyNay) AS fTienQuyetToanDaDuyet,
		CTCT.sXauNoiMa
		into #tblDataQuyetToanDaDuyetQuyTruoc
	FROM
	BH_QTC_Quy_KinhPhiQuanLy CT
	INNER JOIN BH_QTC_Quy_KinhPhiQuanLy_chiTiet CTCT
	ON CT.ID_QTC_Quy_KinhPhiQuanLy=CTCT.iID_QTC_Quy_KinhPhiQuanLy
	WHERE CT.iNamChungTu = @NamLamViec
		  AND CT.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
		  AND CT.iQuyChungTu<@iQuy
	GROUP BY CTCT.sXauNoiMa

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
		ISNULL(SUM(dataQuyTruoc.fTienQuyetToanDaDuyet), 0) / @Dvt + ISNULL(SUM(ctct.fTienDeNghiQuyetToanQuyNay), 0) / @Dvt   as FTienThucChi, 
		ISNULL(SUM(dataQuyTruoc.fTienQuyetToanDaDuyet), 0) / @Dvt as FTienQuyetToanDaDuyet, 
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
	LEFT JOIN #tblDataQuyetToanDaDuyetQuyTruoc  dataQuyTruoc ON mlns.sXauNoiMa=dataQuyTruoc.sXauNoiMa
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
		ISNULL(SUM(dataQuyTruoc.fTienQuyetToanDaDuyet), 0) / @Dvt + ISNULL(SUM(ctct.fTienDeNghiQuyetToanQuyNay), 0) / @Dvt   as FTienThucChi, 
		ISNULL(SUM(dataQuyTruoc.fTienQuyetToanDaDuyet), 0) / @Dvt as FTienQuyetToanDaDuyet, 
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
		LEFT JOIN #tblDataQuyetToanDaDuyetQuyTruoc  dataQuyTruoc ON mlns.sXauNoiMa=dataQuyTruoc.sXauNoiMa
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkp_ql_thongtriloai2_bh]    Script Date: 6/20/2024 2:58:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
				WHERE iIDMaDonVi  IN (select * from f_split(@IdDonVi)) and
				iID_QTC_Quy_KinhPhiQuanLy in
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
				WHERE iIDMaDonVi  IN (select * from f_split(@IdDonVi)) and
				iID_QTC_Quy_KinhPhiQuanLy in
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkpk_kehoach_bh]    Script Date: 6/20/2024 2:58:43 PM ******/
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
	DECLARE @fSoThamDinh INT;
	SELECT @fSoThamDinh = sum(fSoThamDinh)
	FROM BH_ThamDinhQuyetToan_ChungTuChiTiet 
	WHERE iNamLamViec = @NamLamViec - 1 and iMa = 240 and  iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
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

	SELECT  
			SUM(fTienDeNghiQuyetToanQuyNay) AS fTienQuyetToanDaDuyet,
			CTCT.sXauNoiMa
			into #TemptblTienDaDuyet
		FROM
		BH_QTC_Quy_KPK CT
		INNER JOIN BH_QTC_Quy_KPK_ChiTiet CTCT
		ON CT.ID_QTC_Quy_KPK=CTCT.iID_QTC_Quy_KPK
		WHERE CT.iNamChungTu = @NamLamViec
			  --AND CAST(CT.dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
			  AND CT.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
			  --AND CT.bIsKhoa=1
			  AND CT.iQuyChungTu<@iQuy
		GROUP BY  CTCT.sXauNoiMa
		

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
		CASE WHEN mlns.sXauNoiMa = '9010006' THEN @fSoThamDinh/@Dvt  ELSE 0 END as FTien_DuToanNamTruocChuyenSang,
		--ISNULL((TempChungTuQuyTruoc.fTien_DuToanNamTruocChuyenSang), 0) / FTien_DuToanNamTruocChuyenSang as FTien_DuToanNamTruocChuyenSang,
		ISNULL((dt.fTienDuToanDuocGiao), 0) / @Dvt as FTien_DuToanGiaoNamNay, 
		ISNULL(Sum(ctct.fTien_TongDuToanDuocGiao), 0) / @Dvt as FTien_TongDuToanDuocGiao, 
		(ISNULL(Sum(ctct.fTienThucChi), 0) / @Dvt + isnull(tienDuyet.fTienQuyetToanDaDuyet,0) / @Dvt) as FTienThucChi, 
		ISNULL(Sum(tienDuyet.fTienQuyetToanDaDuyet), 0) / @Dvt as FTienQuyetToanDaDuyet, 
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
	on mlns.sXauNoiMa = ctct.sXauNoiMa 
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

	LEFT JOIN (
				SELECT SUM(CTCT.fTien_DuToanNamTruocChuyenSang)  fTien_DuToanNamTruocChuyenSang,
								CTCT.sXauNoiMa

				FROM BH_QTC_Quy_KPK_ChiTiet CTCT
					WHERE CTCT.iID_QTC_Quy_KPK IN
				(
					SELECT iID_QTC_Quy_KPK FROM  BH_QTC_Quy_KPK 
					WHERE iID_MaDonVi IN ( SELECT * FROM  f_split(@IdDonVi))
								AND iNamChungTu=@NamLamViec
								AND iQuyChungTu=1
				)
				group by CTCT.sXauNoiMa
	)TempChungTuQuyTruoc ON TempChungTuQuyTruoc.sXauNoiMa=mlns.sXauNoiMa

	LEFT JOIN #TemptblTienDaDuyet tienDuyet on mlns.sXauNoiMa=tienDuyet.sXauNoiMa
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
		mlns.bHangCha,
		dt.fTienDuToanDuocGiao,
		TempChungTuQuyTruoc.fTien_DuToanNamTruocChuyenSang,
		tienDuyet.fTienQuyetToanDaDuyet
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
		CASE WHEN mlns.sXauNoiMa = '9010006' THEN @fSoThamDinh/@Dvt  ELSE 0 END as FTien_DuToanNamTruocChuyenSang,
		--ISNULL((TempChungTuQuyTruoc.fTien_DuToanNamTruocChuyenSang), 0) / @Dvt as FTien_DuToanNamTruocChuyenSang,
		ISNULL(dt.fTienTuChi, 0) / @Dvt as FTien_DuToanGiaoNamNay, 
		ISNULL(Sum(ctct.fTien_TongDuToanDuocGiao), 0) / @Dvt as FTien_TongDuToanDuocGiao, 
		(ISNULL(Sum(ctct.fTienThucChi), 0) / @Dvt + isnull(tienDuyet.fTienQuyetToanDaDuyet,0) / @Dvt) as FTienThucChi, 
		ISNULL(Sum(tienDuyet.fTienQuyetToanDaDuyet), 0) / @Dvt as FTienQuyetToanDaDuyet, 
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
		LEFT JOIN (
				SELECT SUM(CTCT.fTien_DuToanNamTruocChuyenSang)  fTien_DuToanNamTruocChuyenSang,
								CTCT.sXauNoiMa

				FROM BH_QTC_Quy_KPK_ChiTiet CTCT
					WHERE CTCT.iID_QTC_Quy_KPK IN
				(
					SELECT iID_QTC_Quy_KPK FROM  BH_QTC_Quy_KPK 
					WHERE iID_MaDonVi IN ( SELECT * FROM  f_split(@IdDonVi))
								AND iNamChungTu=@NamLamViec
								AND iQuyChungTu=1
				)
				group by CTCT.sXauNoiMa
				)TempChungTuQuyTruoc ON TempChungTuQuyTruoc.sXauNoiMa=mlns.sXauNoiMa
				LEFT JOIN #TemptblTienDaDuyet tienDuyet on mlns.sXauNoiMa=tienDuyet.sXauNoiMa
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
		mlns.bHangCha,
		dt.fTienTuChi,
		TempChungTuQuyTruoc.fTien_DuToanNamTruocChuyenSang,
		tienDuyet.fTienQuyetToanDaDuyet
	Order by mlns.sXauNoiMa
END
END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]    Script Date: 6/20/2024 4:51:20 PM ******/
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
			danhmuc.sDuToanChiTietToi
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
			CASE WHEN mucluc.sXauNoiMa = @LNS THEN @fSoThamDinh / @Donvitinh ELSE 0 END as fTien_DuToanNamTruocChuyenSang, 
			CASE WHEN mucluc.sXauNoiMa = @LNS THEN @fSoThamDinh / @Donvitinh ELSE 0 END as fDuToanNamTruocChuyenSang, 
			daDuToan.fTienDuToan as fTien_DuToanGiaoNamNay,
			chi_tiet.fTien_TongDuToanDuocGiao,
			chi_tiet.fTien_ThucChi,
			chi_tiet.fTienThua,
			chi_tiet.fTienThieu,
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
			mucluc.sMoTa as sNoiDung,
			CASE WHEN mucluc.sXauNoiMa = @LNS THEN @fSoThamDinh / @Donvitinh ELSE 0 END as fTien_DuToanNamTruocChuyenSang,
			CASE WHEN mucluc.sXauNoiMa = @LNS THEN @fSoThamDinh / @Donvitinh ELSE 0 END as fDuToanNamTruocChuyenSang, 
			daDuToan.fTienDuToan as  fTien_DuToanGiaoNamNay,
			chi_tiet.fTien_TongDuToanDuocGiao,
			chi_tiet.fTien_ThucChi,
			chi_tiet.fTienThua,
			chi_tiet.fTienThieu,
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
GO
