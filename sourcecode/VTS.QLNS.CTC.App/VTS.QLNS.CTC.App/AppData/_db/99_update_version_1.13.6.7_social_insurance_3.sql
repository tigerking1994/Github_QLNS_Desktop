/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_nkpk_phucluc_bh]    Script Date: 12/13/2023 10:37:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qtc_nkpk_phucluc_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qtc_nkpk_phucluc_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_nkpk_kehoach_bh]    Script Date: 12/13/2023 10:37:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qtc_nkpk_kehoach_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qtc_nkpk_kehoach_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_rpt_get_donvi_nKPK_lns]    Script Date: 12/13/2023 10:37:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_rpt_get_donvi_nKPK_lns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_rpt_get_donvi_nKPK_lns]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_rpt_get_donvi_nKCB_lns]    Script Date: 12/13/2023 10:37:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_rpt_get_donvi_nKCB_lns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_rpt_get_donvi_nKCB_lns]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_nkqk_chitiet_export_excel_bh]    Script Date: 12/13/2023 10:37:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_nkqk_chitiet_export_excel_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_nkqk_chitiet_export_excel_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_nkqk_chitiet_bh]    Script Date: 12/13/2023 10:37:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_nkqk_chitiet_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_nkqk_chitiet_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_nkpk_create_data_summary_bh]    Script Date: 12/13/2023 10:37:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_nkpk_create_data_summary_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_nkpk_create_data_summary_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_namkinh_phikhac_gettien_thuchi_bh]    Script Date: 12/13/2023 10:37:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_namkinh_phikhac_gettien_thuchi_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_namkinh_phikhac_gettien_thuchi_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_namkinh_phikhac_chungtu_index_bh]    Script Date: 12/13/2023 10:37:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_namkinh_phikhac_chungtu_index_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_namkinh_phikhac_chungtu_index_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_nkpql]    Script Date: 12/13/2023 10:37:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_get_lns_for_nkpql]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_get_lns_for_nkpql]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_nkpk]    Script Date: 12/13/2023 10:37:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_get_lns_for_nkpk]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_get_lns_for_nkpk]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_nkcb]    Script Date: 12/13/2023 10:37:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_get_lns_for_nkcb]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_get_lns_for_nkcb]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_nbhxh]    Script Date: 12/13/2023 10:37:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_get_lns_for_nbhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_get_lns_for_nbhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_phulucquyettoannam_KCB]    Script Date: 12/13/2023 10:37:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_phulucquyettoannam_KCB]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_phulucquyettoannam_KCB]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_danhsachquyettoannamKCB_index]    Script Date: 12/13/2023 10:37:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_danhsachquyettoannamKCB_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_danhsachquyettoannamKCB_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chinamKCB_chitiet]    Script Date: 12/13/2023 10:37:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_chinamKCB_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_chinamKCB_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]    Script Date: 12/13/2023 10:37:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqKCB_create_data_summary]    Script Date: 12/13/2023 10:37:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtcqKCB_create_data_summary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtcqKCB_create_data_summary]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_create_quyet_toan_chinamKPK_chitiet_theo4quy]    Script Date: 12/13/2023 10:37:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_create_quyet_toan_chinamKPK_chitiet_theo4quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_create_quyet_toan_chinamKPK_chitiet_theo4quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_create_quyet_toan_chinamkcb_chitiet_theo4quy]    Script Date: 12/13/2023 10:37:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_create_quyet_toan_chinamkcb_chitiet_theo4quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_create_quyet_toan_chinamkcb_chitiet_theo4quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_create_quyet_toan_chinamkcb_chitiet_theo4quy]    Script Date: 12/13/2023 10:37:43 AM ******/
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
				SUM(ctqt_quy_chitiet.fTienThucChi) as fTien_ThucChi
				
		from BH_QTC_Quy_KCB as ctqt_quy
		inner join BH_QTC_Quy_KCB_ChiTiet as ctqt_quy_chitiet on ctqt_quy.ID_QTC_Quy_KCB = ctqt_quy_chitiet.iID_QTC_Quy_KCB
		where ctqt_quy.iID_MaDonVi = @IdMaDonVi and ctqt_quy.iNamChungTu = @INamLamViec
				and ctqt_quy.iQuyChungTu=4
		--and ((@IsTongHop = 0 and ctqt_quy.bDaTongHop = 0 and ctqt_quy.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  ctqt_quy.sDSSoChungTuTongHop is not null))
		group by ctqt_quy_chitiet.iID_MucLucNganSach,
				ctqt_quy_chitiet.sNoiDung,
				ctqt_quy_chitiet.sXauNoiMa,
				ctqt_quy_chitiet.iNamLamViec,
				ctqt_quy_chitiet.iID_MaDonVi) as tb_qtcy

end
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_create_quyet_toan_chinamKPK_chitiet_theo4quy]    Script Date: 12/13/2023 10:37:43 AM ******/
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
			ISNULL(CTCT.fTienThucChi, 0) fTienThucChi
		FROM
		BH_QTC_Quy_KPK AS CT
		LEFT JOIN BH_QTC_Quy_KPK_ChiTiet CTCT ON  CT.ID_QTC_Quy_KPK=CTCT.iID_QTC_Quy_KPK
		WHERE CT.iID_MaDonVi=@IdMaDonVi 
			AND CT.iQuyChungTu=4
			AND CT.bIsKhoa=1
			AND CT.iNamChungTu=@INamLamViec
			AND  CT.iID_LoaiChi=@IDLoaiCap
	) as tb_qtcy;

end
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqKCB_create_data_summary]    Script Date: 12/13/2023 10:37:43 AM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]    Script Date: 12/13/2023 10:37:43 AM ******/
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
				and danhmuc.iTrangThai=1


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
		and qtcn.iNamLamViec = @INamLamViec
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
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chinamKCB_chitiet]    Script Date: 12/13/2023 10:37:43 AM ******/
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
				and danhmuc.iTrangThai=1
		
		
		
	---Lấy thông tin chi tiết chứng từ
		select 
			qtcn_ct.ID_QTC_Nam_KCB_QuanYDonVi_ChiTiet,
			qtcn_ct.iID_MucLucNganSach,
			qtcn_ct.sNoiDung,
			qtcn_ct.sXauNoiMa,
			qtcn_ct.iNamLamViec,
			qtcn_ct.iID_MaDonVi,
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
		chi_tiet.iID_MaDonVi,
		chi_tiet.iNamLamViec,
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
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_danhsachquyettoannamKCB_index]    Script Date: 12/13/2023 10:37:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_danhsachquyettoannamKCB_index]
@INamLamViec int,
@Search nvarchar(max)
As
Begin
	select 
		qtn.ID_QTC_Nam_KCB_QuanYDonVi,
		qtn.iID_DonVi,
		qtn.iID_MaDonVi,
		qtn.sSoChungTu,
		qtn.dNgayChungTu,
		qtn.bThucChiTheo4Quy,
		qtn.iNamLamViec,
		qtn.sSoQuyetDinh,
		qtn.dNgayQuyetDinh,
		qtn.sMoTa,
		qtn.bIsKhoa,
		qtn.iLoaiTongHop,
		qtn.sTongHop,
		qtn.fTongTien_DuToanNamTruocChuyenSang,
		qtn.fTongTien_DuToanGiaoNamNay,
		qtn.fTongTien_TongDuToanDuocGiao,
		qtn.fTongTien_ThucChi,
		isnull(qtn.fTongTien_TongDuToanDuocGiao,0) - isnull(qtn.fTongTien_ThucChi,0) as fTongTienThua,
		isnull(qtn.fTongTien_ThucChi,0) - isnull(qtn.fTongTien_TongDuToanDuocGiao,0) as fTongTienThieu,
		qtn.bDaTongHop,
		qtn.sDSSoChungTuTongHop,
		dv.sTenDonVi,
		qtn.sNguoiTao,
		qtn.sDSLNS
	from BH_QTC_Nam_KCB_QuanYDonVi as qtn
	left join DonVi as dv on qtn.iID_DonVi = dv.iID_DonVi
	where dv.iNamLamViec = @INamLamViec
	and (@Search is null or ( @Search is not null and  qtn.sSoChungTu like N'%'+@Search+ '%'))

	

End
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_phulucquyettoannam_KCB]    Script Date: 12/13/2023 10:37:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_phulucquyettoannam_KCB]
@INamLamViec int,
@IdMaDonVi nvarchar(max),
@Lns nvarchar(max),
@Donvitinh int,
@IsTongHop int
as
begin
	---Lấy quyết toán năm trước
	select
		donvi.sTenDonVi,
		sum(chungtu_ct.fTien_TongDuToanDuocGiao) as fChiTieuNamTruoc,
		0 as fChiTieuNamNay,
		0 as fTongCong,
		0 as fTienQuyetToan,
		0 as fTienThua,
		0 as fTienThieu
	into #tblNamTruoc
	from BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet as chungtu_ct
	inner join BH_QTC_Nam_KCB_QuanYDonVi as chungtu on chungtu_ct.iID_QTC_Nam_KCB_QuanYDonVi = chungtu.ID_QTC_Nam_KCB_QuanYDonVi
	inner join (
					select * from BH_DM_MucLucNganSach where iNamLamViec = @INamLamViec -1 and sLNs in (select * from dbo.splitstring(@Lns))
				) as danhmuc on danhmuc.iID_MLNS = chungtu_ct.iID_MucLucNganSach
	inner join (
					select * from DonVi where iNamLamViec = @INamLamViec -1 
				) as donvi on donvi.iID_MaDonVi = chungtu.iID_MaDonVi
	where chungtu.iNamLamViec = @INamLamViec -1
	and chungtu.iID_MaDonVi in (select * from dbo.splitstring(@IdMaDonVi))
	--and ((@IsTongHop = 0 and chungtu.bDaTongHop = 0 and chungtu.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  chungtu.sDSSoChungTuTongHop is not null))
	group by donvi.sTenDonVi

	---Lấy quyết toán năm trước

	select
		donvi.sTenDonVi,
		0 fChiTieuNamTruoc,
		sum(chungtu_ct.fTien_TongDuToanDuocGiao) as fChiTieuNamNay,
		0 as fTongCong,
		sum(fTien_ThucChi) as fTienQuyetToan,
		0 as fTienThua,
		0 as fTienThieu
	into #tblNamNay
	from BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet as chungtu_ct
	inner join BH_QTC_Nam_KCB_QuanYDonVi as chungtu on chungtu_ct.iID_QTC_Nam_KCB_QuanYDonVi = chungtu.ID_QTC_Nam_KCB_QuanYDonVi
	inner join (
					select * from BH_DM_MucLucNganSach where iNamLamViec = @INamLamViec and sLNs in (select * from dbo.splitstring(@Lns))
				) as danhmuc on danhmuc.iID_MLNS = chungtu_ct.iID_MucLucNganSach
	inner join (
					select * from DonVi where iNamLamViec = @INamLamViec
				) as donvi on donvi.iID_MaDonVi = chungtu.iID_MaDonVi
	where chungtu.iNamLamViec = @INamLamViec
	and chungtu.iID_MaDonVi in (select * from dbo.splitstring(@IdMaDonVi))
	--and ((@IsTongHop = 0 and chungtu.bDaTongHop = 0 and chungtu.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  chungtu.sDSSoChungTuTongHop is not null))
	group by donvi.sTenDonVi

	--- Kết quả trả về

	select
		ROW_NUMBER () OVER (ORDER BY sTenDonVi) sTT,
		sTenDonVi,
		sum(fChiTieuNamTruoc) as fChiTieuNamTruoc,
		sum(fChiTieuNamNay) as fChiTieuNamNay,
		isnull(sum(fChiTieuNamTruoc) ,0) + isnull(sum(fChiTieuNamNay) ,0) as fTongCong,
		sum(fTienQuyetToan) as fTienQuyetToan,
		isnull(sum(fChiTieuNamTruoc) ,0) + isnull(sum(fChiTieuNamNay) ,0) - isnull(sum(fTienQuyetToan) ,0) as fTienThua,
		isnull(sum(fTienQuyetToan) ,0) - isnull(sum(fChiTieuNamTruoc) ,0) + isnull(sum(fChiTieuNamNay) ,0) as fTienThieu
	from
	(
		select * from #tblNamTruoc
		union all
		select * from #tblNamNay
	) as result

	group by sTenDonVi
	order by sTenDonVi


	drop table #tblNamTruoc;
	drop table #tblNamNay;
end
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_nbhxh]    Script Date: 12/13/2023 10:37:43 AM ******/
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
	--				WHERE iNamLamViec = @namLamViec 
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
	--				WHERE iNamLamViec = @namLamViec 
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
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_nkcb]    Script Date: 12/13/2023 10:37:43 AM ******/
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
	--				WHERE iNamLamViec = @namLamViec 
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
	--				WHERE iNamLamViec = @namLamViec 
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
					(SELECT ID_QTC_Nam_KCB_QuanYDonVi 
					FROM BH_QTC_Nam_KCB_QuanYDonVi 
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
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_nkpk]    Script Date: 12/13/2023 10:37:43 AM ******/
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
	--				WHERE iNamLamViec = @namLamViec 
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
	--				WHERE iNamLamViec = @namLamViec 
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
					WHERE iNamLamViec = @namLamViec 
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
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_nkpql]    Script Date: 12/13/2023 10:37:43 AM ******/
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
	--				WHERE iNamLamViec = @namLamViec 
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
	--				WHERE iNamLamViec = @namLamViec 
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
					(SELECT ID_QTC_Nam_KinhPhiQuanLy 
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


select * from BH_QTC_Nam_KinhPhiQuanLy
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_namkinh_phikhac_chungtu_index_bh]    Script Date: 12/13/2023 10:37:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Doantc
-- Create date: 13/06/2023
-- Description:	Lấy danh sách hiển thị chứng từ quyết toán chi nam kinh phí khac

-- =============================================
CREATE PROCEDURE [dbo].[sp_qtc_namkinh_phikhac_chungtu_index_bh]
	@YearOfWork int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
	DISTINCT 
		 ct.ID_QTC_Nam_KPK
		, ct.iID_DonVi
		, ct.iID_MaDonVi
		, ct.sSoChungTu
		, ct.dNgayChungTu
		, ct.sSoQuyetDinh
		, ct.dNgayQuyetDinh
		, ct.bThucChiTheo4Quy
		, ct.iNamLamViec
		, ct.sMoTa
		, ct.dNgaySua
		, ct.dNgayTao
		, ct.sNguoiSua
		, ct.sNguoiTao
		, ct.sTongHop
		, ct.iID_TongHopID
		, ct.iLoaiTongHop
		, ct.bIsKhoa
		, ct.fTongTien_DuToanNamTruocChuyenSang
		, ct.fTongTien_DuToanGiaoNamNay
		, ct.fTongTien_TongDuToanDuocGiao
		, ct.fTongTien_ThucChi
		, ct.fTongTienThua
		, ct.fTongTienThieu
		, ct.fTiLeThucHienTrenDuToan
		, ct.iID_LoaiChi
		, ct.sDSLNS
		, ct.bDaTongHop
		, lc.sTenDanhMucLoaiChi
		, dv.sTenDonVi
		-- Tong dự toán todo
	FROM BH_QTC_Nam_KPK ct
	LEFT JOIN DonVi dv on ct.iID_DonVi=dv.iID_DonVi 
	and ct.iID_MaDonVi=dv.iID_MaDonVi 
	and dv.iNamLamViec=ct.iNamLamViec
	LEFT JOIN BH_DM_LoaiChi lc on lc.iID=ct.iID_LoaiChi
	WHERE ct.iNamLamViec=@YearOfWork 
	and dv.iTrangThai=1
	Order by ct.sSoChungTu
END


GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_namkinh_phikhac_gettien_thuchi_bh]    Script Date: 12/13/2023 10:37:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		
-- Create date: 
-- Description:	Lấy danh sách hiển thị thực chi theo quý của chứng từ quyết toán năm chi kinh phí khác chi tiết
CREATE PROCEDURE [dbo].[sp_qtc_namkinh_phikhac_gettien_thuchi_bh]
	@YearOfWork int,
	@AgencyId nvarchar(500),
	@VoucherDate datetime,
	@IQuyChungTu int,
	@ILoaiChi uniqueidentifier
AS
BEGIN

	SELECT  
		SUM(CTCT.fTienThucChi) AS FTien_ThucChi,
		CT.iID_MaDonVi,
		CTCT.iID_MucLucNganSach
	FROM
	BH_QTC_Quy_KPK CT
	INNER JOIN BH_QTC_Quy_KPK_ChiTiet CTCT
	ON CT.ID_QTC_Quy_KPK=CTCT.iID_QTC_Quy_KPK
	WHERE CT.iNamChungTu = @YearOfWork
          AND CAST(CT.dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
		  AND CT.iID_MaDonVi IN (SELECT * FROM f_split(@AgencyId))
		  AND CT.bIsKhoa=1
		  AND CT.iQuyChungTu=@IQuyChungTu
		  AND CT.iID_LoaiChi=@ILoaiChi
	GROUP BY  CT.iID_MaDonVi,CTCT.iID_MucLucNganSach

END

GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_nkpk_create_data_summary_bh]    Script Date: 12/13/2023 10:37:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_qtc_nkpk_create_data_summary_bh]
@IdChungTu nvarchar(MAX),
@IDMaDonVi nvarchar(500),
@NguoiTao nvarchar(500),
@YearOfWork int,
@LstIdChungTuSummary nvarchar(MAX)
AS
BEGIN
SET NOCOUNT ON;

INSERT INTO BH_QTC_Nam_KPK_ChiTiet
(ID_QTC_Nam_KPK_ChiTiet,
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
fTien_DuToanNamTruocChuyenSang,
fTien_DuToanGiaoNamNay,
fTien_TongDuToanDuocGiao,
fTien_ThucChi,
fTienThua,
fTienThieu,
fTiLeThucHienTrenDuToan
 )
SELECT 
	   NEWID(),
	   @IdChungTu,
       iID_MucLucNganSach,
	   sNoiDung,
	   sXauNoiMa,
	   @IDMaDonVi,
	   iNamLamViec,
	   null,
	   GETDATE(),
	   @NguoiTao,
	   '',
	   SUM(fTien_DuToanNamTruocChuyenSang),
	   SUM(fTien_DuToanGiaoNamNay),
	   SUM(fTien_TongDuToanDuocGiao),
	   SUM(fTien_ThucChi),
	   SUM(fTienThua),
	   SUM(fTienThieu),
	   SUM(fTiLeThucHienTrenDuToan)
FROM BH_QTC_Nam_KPK_ChiTiet
WHERE  iID_QTC_Nam_KPK IN
    (SELECT *
     FROM f_split(@LstIdChungTuSummary))
group by iID_MucLucNganSach,sNoiDung,sXauNoiMa,iNamLamViec

UPDATE BH_QTC_Nam_KPK SET bDaTongHop = 1 WHERE ID_QTC_Nam_KPK IN (SELECT * FROM f_split(@LstIdChungTuSummary));
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_nkqk_chitiet_bh]    Script Date: 12/13/2023 10:37:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay chi tiet chung tu 
CREATE PROCEDURE [dbo].[sp_qtc_nkqk_chitiet_bh]
	 @NamLamViec int,
	 @iD uniqueidentifier,
	 @IdDonVi nvarchar(max),
	 @LNS nvarchar(max)
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
		ctct.iID_MaDonVi as IIdMaDonVi,
		ISNULL(ctct.fTien_DuToanNamTruocChuyenSang, 0)  as fTien_DuToanNamTruocChuyenSang, 
		ISNULL(ctct.fTien_DuToanGiaoNamNay, 0)  as fTien_DuToanGiaoNamNay,
		ISNULL(ctct.fTien_TongDuToanDuocGiao, 0)  as fTien_TongDuToanDuocGiao, 
		ISNULL(ctct.fTien_ThucChi, 0)  as fTien_ThucChi, 
		ISNULL(ctct.fTienThua, 0)  as fTienThua, 
		ISNULL(ctct.fTienThieu, 0)  as fTienThieu, 
		ISNULL(ctct.fTiLeThucHienTrenDuToan, 0)  as fTiLeThucHienTrenDuToan
	FROM 
		#tblMlnsByPhanCap mlns
	LEFT JOIN 
		(SELECT * FROM BH_QTC_Nam_KPK_ChiTiet 
				WHERE iID_QTC_Nam_KPK in
					( SELECT ID_QTC_Nam_KPK FROM BH_QTC_Nam_KPK
								WHERE iNamLamViec=@NamLamViec
								AND iID_QTC_Nam_KPK=@iD
								AND iID_MaDonVi IN (select * from f_split(@IdDonVi))
								)) ctct
	on mlns.iID_MLNS = ctct.iID_MucLucNganSach 
	Order by mlns.sXauNoiMa
END
;
;


GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_nkqk_chitiet_export_excel_bh]    Script Date: 12/13/2023 10:37:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay chi tiet chung tu 
CREATE PROCEDURE [dbo].[sp_qtc_nkqk_chitiet_export_excel_bh]
	 @NamLamViec int,
	 @iD uniqueidentifier,
	 @IdDonVi nvarchar(max),
	 @LNS nvarchar(max)
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
		ISNULL(ctct.fTien_DuToanNamTruocChuyenSang, 0)  as fTien_DuToanNamTruocChuyenSang, 
		ISNULL(ctct.fTien_DuToanGiaoNamNay, 0)  as fTien_DuToanGiaoNamNay,
		ISNULL(ctct.fTien_TongDuToanDuocGiao, 0)  as fTien_TongDuToanDuocGiao, 
		ISNULL(ctct.fTien_ThucChi, 0)  as fTien_ThucChi, 
		ISNULL(ctct.fTienThua, 0)  as fTienThua, 
		ISNULL(ctct.fTienThieu, 0)  as fTienThieu, 
		ISNULL(ctct.fTiLeThucHienTrenDuToan, 0)  as fTiLeThucHienTrenDuToan
	FROM 
		#tblMlnsByPhanCap mlns
	LEFT JOIN 
		(SELECT * FROM BH_QTC_Nam_KPK_ChiTiet 
				WHERE iID_QTC_Nam_KPK in
					( SELECT iID_QTC_Nam_KPK FROM BH_QTC_Nam_KPK
								WHERE iNamLamViec=@NamLamViec
								AND iID_QTC_Nam_KPK=@iD
								AND iID_MaDonVi IN (select * from f_split(@IdDonVi))
								)) ctct
	on mlns.iID_MLNS = ctct.iID_MucLucNganSach 
	Order by mlns.sXauNoiMa
END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_rpt_get_donvi_nKCB_lns]    Script Date: 12/13/2023 10:37:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_qtc_rpt_get_donvi_nKCB_lns]
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
WHERE chungtu.iNamLamViec = @NamLamViec
  AND chungtu.iID_MaDonVi <> ''
  AND chungtu.iID_MaDonVi IS NOT NULL
  AND donvi.iNamLamViec = @NamLamViec
  --AND chungtu.iLoaiTongHop=@LoaiChungTu
ORDER BY cast(chungtu.dNgayChungTu AS date) END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_rpt_get_donvi_nKPK_lns]    Script Date: 12/13/2023 10:37:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_qtc_rpt_get_donvi_nKPK_lns]
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
WHERE chungtu.iNamLamViec = @NamLamViec
  AND chungtu.iID_MaDonVi <> ''
  AND chungtu.iID_MaDonVi IS NOT NULL
  AND donvi.iNamLamViec = @NamLamViec
  --AND chungtu.iLoaiTongHop=@LoaiChungTu
  AND chungtu.iID_LoaiChi=@LoaiChi
ORDER BY cast(chungtu.dNgayChungTu AS date) END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_nkpk_kehoach_bh]    Script Date: 12/13/2023 10:37:43 AM ******/
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
								WHERE iNamLamViec=@NamLamViec
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
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_nkpk_phucluc_bh]    Script Date: 12/13/2023 10:37:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_qtc_nkpk_phucluc_bh]
	@listTenDonVi ntext,
	@namLamViec int,
	@LNS nvarchar(200)
AS
BEGIN
declare @DataKhoi table (idDonVi nvarchar(50),sTenDonVI nvarchar(200),Loai int,
FTienDaThucHienNamTruoc float,FTienNamNay float,FTienCong float,FTienQuyetToan float, FTienThua float,FTienThieu float
);

INSERT INTO @DataKhoi (idDonVi,sTenDonVI , Loai,
FTienDaThucHienNamTruoc ,FTienNamNay , FTienCong ,FTienQuyetToan , FTienThua ,FTienThieu 
)
	SELECT 
	   dt_dv.sTenDonVi,
	   dt_dv.id idDonVi,
	   dt_dv.iLoai Loai,
	   FTienDaThucHienNamTruoc=SUM(IsNull(A.fTien_DuToanNamTruocChuyenSang,0)),
	   FTienNamNay=SUM(IsNull(A.fTien_DuToanGiaoNamNay,0)),
	   FTienQuyetToan=SUM(IsNull(A.fTien_TongDuToanDuocGiao,0)),
	   FTienCong=SUM(IsNull(A.fTien_ThucChi,0)),
	   FTienThua=SUM(IsNull(A.fTienThua,0)),
	   FTienThieu=SUM(IsNull(A.fTienThieu,0))
FROM
  (SELECT 
				ct.iID_DonVi,
				ct.iID_MaDonVi,
				ctct.fTien_DuToanNamTruocChuyenSang,
				ctct.fTien_DuToanGiaoNamNay,
				ctct.fTien_TongDuToanDuocGiao,
				ctct.fTien_ThucChi,
				ctct.fTienThua,
				ctct.fTienThieu,
				ctct.fTiLeThucHienTrenDuToan
   FROM BH_QTC_Nam_KPK_ChiTiet ctct
   LEFT JOIN BH_QTC_Nam_KPK ct ON ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MucLucNganSach = ml.iID_MLNS
   AND ml.iNamLamViec = @namLamViec--@namLamViec
   AND ml.iTrangThai = 1
   AND ml.sLNS IN  (SELECT * FROM f_split(@LNS))
   WHERE ct.iNamLamViec = @namLamViec--@namLamViec
	---
	) AS A 
   LEFT JOIN
  (SELECT iID_MaDonVi AS id,
          sTenDonVi, iLoai
   FROM DonVi
   WHERE iTrangThai = 1
   AND iNamLamViec = @namLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   --AND iNamLamViec = @namLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   
GROUP BY dt_dv.sTenDonVi,
		dt_dv.id,
		dt_dv.iLoai;

SELECT dt.idDonVi, 
dt.sTenDonVI, 
dt.Loai,
IsNull(dt.FTienDaThucHienNamTruoc, 0) FTienDaThucHienNamTruoc,
IsNull(dt.FTienNamNay, 0) FTienNamNay,
IsNull(dt.FTienCong, 0) FTienCong,
IsNull(dt.FTienQuyetToan, 0) FTienQuyetToan,
IsNull(dt.FTienThua, 0) FTienThua,
IsNull(dt.FTienThieu, 0) FTienThieu
FROM @DataKhoi dt
where dt.sTenDonVI in 
    (SELECT *
     FROM f_split(@listTenDonVi))
END
;


GO
