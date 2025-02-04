/****** Object:  StoredProcedure [dbo].[sp_tl_export_danh_muc_che_do_bhxh]    Script Date: 11/29/2023 10:30:58 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_export_danh_muc_che_do_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_export_danh_muc_che_do_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_export_bang_luong_thang_bhxh]    Script Date: 11/29/2023 10:30:58 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_export_bang_luong_thang_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_export_bang_luong_thang_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bhxh_export_can_bo_che_do]    Script Date: 11/29/2023 10:30:58 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_bhxh_export_can_bo_che_do]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_bhxh_export_can_bo_che_do]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_thai_san]    Script Date: 11/29/2023 10:30:58 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_luong_tro_cap_thai_san]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_thai_san]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_om_dau]    Script Date: 11/29/2023 10:30:58 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_luong_tro_cap_om_dau]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_om_dau]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_ht_pv_tv_tt]    Script Date: 11/29/2023 10:30:58 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_luong_tro_cap_ht_pv_tv_tt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_ht_pv_tv_tt]
GO
/****** Object:  StoredProcedure [dbo].[sp_luong_bhxh_import_qtc_bhxh]    Script Date: 11/29/2023 10:30:58 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_luong_bhxh_import_qtc_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_luong_bhxh_import_qtc_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_luong_bhxh_get_all_che_do]    Script Date: 11/29/2023 10:30:58 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_luong_bhxh_get_all_che_do]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_luong_bhxh_get_all_che_do]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_qtcq_get_mlns_che_do_bhxh]    Script Date: 11/29/2023 10:30:58 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_qtcq_get_mlns_che_do_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_qtcq_get_mlns_che_do_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]    Script Date: 11/29/2023 10:30:58 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]    Script Date: 11/29/2023 10:30:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]
@IdChungTu uniqueidentifier,
@SLNS nvarchar(max),
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
		where danhmuc.iNamLamViec = @INamLamViec and danhmuc.sLNs in (SELECT * FROM f_split(@SLNS))
		
		
		
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
			qtcn_ct.fTienLDHD_DeNghi,
			qtcn.iID_MaDonVi iIDMaDonVi,
			qtcn.iNamChungTu iNamLamViec
		into #tblQuyetToanQuyChiTiet
		from BH_QTC_Quy_CheDoBHXH_ChiTiet as qtcn_ct
		inner join BH_QTC_Quy_CheDoBHXH  as qtcn on qtcn_ct.iID_QTC_Quy_CheDoBHXH = qtcn.ID_QTC_Quy_CheDoBHXH
		where qtcn_ct.iID_QTC_Quy_CheDoBHXH = @IdChungTu
			AND qtcn.iNamChungTu=@INamLamViec;

		
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
			chi_tiet.fTienLDHD_DeNghi,
			chi_tiet.iIDMaDonVi,
			chi_tiet.iNamLamViec
		from #tblMucLucNganSach as mucluc
		left join #tblQuyetToanQuyChiTiet as chi_tiet on mucluc.iID_MLNS = chi_tiet.iID_MucLucNganSach
		order by mucluc.sXauNoiMa
	
	drop table #tblMucLucNganSach;
	drop table #tblQuyetToanQuyChiTiet;
end


GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_qtcq_get_mlns_che_do_bhxh]    Script Date: 11/29/2023 10:30:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bhxh_qtcq_get_mlns_che_do_bhxh]
	@NamLamViec int
AS
BEGIN
	select 
		chedo.sMaCheDo,
		chedo.sTenCheDo,
		mlns.* 
	from BH_DM_MucLucNganSach mlns
	left join TL_DM_CheDoBHXH chedo
		on mlns.sXauNoiMa = chedo.sXauNoiMaMlnsBHXH
	where
		mlns.iNamLamViec = @NamLamViec
		and mlns.iTrangThai = 1
		and mlns.sLNS in ('9010001', '9010002')
	order by mlns.sXauNoiMa
END
GO
/****** Object:  StoredProcedure [dbo].[sp_luong_bhxh_get_all_che_do]    Script Date: 11/29/2023 10:30:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_luong_bhxh_get_all_che_do] 
	
AS
BEGIN
	select * from TL_DM_CheDoBHXH
	where sMaCheDoCha is not null and sMaCheDoCha <> ''
	and sMaCheDo not in (select distinct sMaCheDoCha from TL_DM_CheDoBHXH)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_luong_bhxh_import_qtc_bhxh]    Script Date: 11/29/2023 10:30:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_luong_bhxh_import_qtc_bhxh]
	@YearOfWork int,
	@Months nvarchar(20)
AS
BEGIN
	select chedo.sXauNoiMaMlnsBHXH, luong.*
	into luong_temp
	from TL_BangLuong_ThangBHXH luong
	left join TL_DM_CheDoBHXH chedo
		on luong.sMaCheDo = chedo.sMaCheDo
	where 
		luong.iNam = @YearOfWork
		and luong.iThang in (SELECT * FROM f_split(@Months))
		and luong.sMaCheDo in 
		(select distinct sMaCheDo from TL_DM_CheDoBHXH
		where sMaCheDoCha is not null and sMaCheDoCha <> ''
		and sMaCheDo not in (select distinct sMaCheDoCha from TL_DM_CheDoBHXH)
		and (upper(sMaCheDo) not like '%HS%' and upper(sMaCheDo) not like '%HESO%'))

	--Thong tin luong Si quan
	select distinct
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo,
		temp.SoNguoi,
		sum(SoTien) SoTien
	into luong_temp_sq
	from
		(select sXauNoiMaMlnsBHXH, sMaCheDo,
			count(sMaHieuCanBo) SoNguoi,
			isnull(sum(nGiaTri), 0) SoTien
			from luong_temp
			where sMaCB like '1%'
			group by
				sXauNoiMaMlnsBHXH,
				sMaCheDo,
				sMaHieuCanBo) temp
	group by
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo,
		temp.SoNguoi

	--Thong tin luong QNCN
	select distinct
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo,
		temp.SoNguoi,
		sum(SoTien) SoTien
	into luong_temp_qncn
	from
		(select sXauNoiMaMlnsBHXH, sMaCheDo,
			count(sMaHieuCanBo) SoNguoi,
			isnull(sum(nGiaTri), 0) SoTien
			from luong_temp
			where sMaCB like '2%'
			group by 
				sXauNoiMaMlnsBHXH,
				sMaCheDo,
				sMaHieuCanBo) temp
	group by
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo,
		temp.SoNguoi

	--Thong tin luong HSQ_BS
	select distinct
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo,
		temp.SoNguoi,
		sum(SoTien) SoTien
	into luong_temp_hsq_bs
	from
		(select sXauNoiMaMlnsBHXH, sMaCheDo,
			count(sMaHieuCanBo) SoNguoi,
			isnull(sum(nGiaTri), 0) SoTien
			from luong_temp
			where sMaCB like '0%'
			group by 
				sXauNoiMaMlnsBHXH,
				sMaCheDo,
				sMaHieuCanBo) temp
	group by
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo,
		temp.SoNguoi

	--Thong tin luong VCQP
	select distinct
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo,
		temp.SoNguoi,
		sum(SoTien) SoTien
	into luong_temp_vcqp
	from
		(select sXauNoiMaMlnsBHXH, sMaCheDo,
			count(sMaHieuCanBo) SoNguoi,
			isnull(sum(nGiaTri), 0) SoTien
			from luong_temp
			where sMaCB in ('3.1', '3.2', '3.3')
			group by 
				sXauNoiMaMlnsBHXH,
				sMaCheDo,
				sMaHieuCanBo) temp
	group by
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo,
		temp.SoNguoi

	--Thong tin luong hdld
	select distinct
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo,
		temp.SoNguoi,
		sum(SoTien) SoTien
	into luong_temp_hdld
	from
		(select sXauNoiMaMlnsBHXH, sMaCheDo,
			count(sMaHieuCanBo) SoNguoi,
			isnull(sum(nGiaTri), 0) SoTien
			from luong_temp
			where sMaCB = '43'
			group by 
				sXauNoiMaMlnsBHXH,
				sMaCheDo,
				sMaHieuCanBo) temp
	group by
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo,
		temp.SoNguoi

	--Ket qua
	select
		sq.sXauNoiMaMlnsBHXH,
		sq.sMaCheDo MaCheDo,
		sq.SoNguoi SoNguoiSQ,
		sq.SoTien SoTienSQ,
		qncn.SoTien SoTienQNCN,
		qncn.SoNguoi SoNguoiQNCN,
		hsq.SoTien SoTienHSQ,
		hsq.SoNguoi SoNguoiHSQ,
		vcqp.SoTien SoTienVCQP,
		vcqp.SoNguoi SoNguoiVCQP,
		hdld.SoTien SoTienHDLD,
		hdld.SoNguoi SoNguoiHDLD
	from luong_temp_sq sq
	left join luong_temp_qncn qncn on sq.sMaCheDo = qncn.sMaCheDo
	left join luong_temp_hsq_bs hsq  on sq.sMaCheDo = hsq.sMaCheDo
	left join luong_temp_vcqp vcqp on sq.sMaCheDo = vcqp.sMaCheDo
	left join luong_temp_hdld hdld on sq.sMaCheDo = hdld.sMaCheDo

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[luong_temp]') AND type in (N'U'))
	drop table luong_temp;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[luong_temp_sq]') AND type in (N'U'))
	drop table luong_temp_sq;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[luong_temp_qncn]') AND type in (N'U'))
	drop table luong_temp_qncn;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[luong_temp_hsq_bs]') AND type in (N'U'))
	drop table luong_temp_hsq_bs;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[luong_temp_vcqp]') AND type in (N'U'))
	drop table luong_temp_vcqp;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[luong_temp_hdld]') AND type in (N'U'))
	drop table luong_temp_hdld;

END
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_ht_pv_tv_tt]    Script Date: 11/29/2023 10:30:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_ht_pv_tv_tt]
	@DsMaDonVi nvarchar(100),
	@NamLamViec int,
	@Thang int,
	@DonViTinh int
AS
BEGIN
	--Lay thong tin luong theo TC_HUUTRI, TC_PHUCVIEN, TC_THOIVIEC, TC_TUTUAT
	select * into TBL_HUUTRI from
	(select donvi.Ten_DonVi, luong.*
	from TL_BangLuong_ThangBHXH luong
	join TL_DM_DonVi donvi on luong.sMaDonVi = donvi.Ma_DonVi
	where 
	luong.sMaDonVi in (SELECT * FROM f_split(@DsMaDonVi))
	and luong.iNam = @NamLamViec
	and luong.iThang = @Thang
	and luong.sMaCheDo in ('HUUTRI_TROCAP1LAN', 'HUUTRI_TROCAPKHUVUC', 'PHUCVIEN_TROCAP1LAN', 'PHUCVIEN_TROCAPKHUVUC', 'THOIVIEC_TROCAP1LAN', 'THOIVIEC_TROCAPKHUVUC', 'TUTUAT_TROCAP1LAN', 'TUTUAT_TROCAPKHUVUC', 'TROCAPMAITANG')) HUUTRI

	-- Data tro cap Huu tri
	select distinct
		TBL_HUUTRI.sMaCB,
		TBL_HUUTRI.sMaCBo,
		--TBL_HUUTRI.sMaCheDo,
		TBL_HUUTRI.sTenCbo,
		TBL_HUUTRI.Ten_DonVi,
		HUUTRI_TROCAP1LAN.nGiaTri fHUUTRI_TROCAP1LAN,
		HUUTRI_TROCAPKHUVUC.nGiaTri fHUUTRI_TROCAPKHUVUC,
		chedocha.sSoQuyetDinh,
		chedocha.dNgayQuyetDinh
		into TBL_HUUTRI_DOC
	from TBL_HUUTRI TBL_HUUTRI
		left join (select top 1 * from TL_CanBo_CheDoBHXH 
		where sMaCheDo in ('HUUTRI_TROCAPKHUVUC', 'HUUTRI_TROCAP1LAN')
		and ((sSoQuyetDinh is not null and sSoQuyetDinh <> '') 
		or (dNgayQuyetDinh is not null and dNgayQuyetDinh <> ''))) chedocha
		on TBL_HUUTRI.sMaCBo = chedocha.sMaCanBo
		left join
		(select HUUTRI.Id, HUUTRI.sMaDonVi, HUUTRI.nGiaTri, HUUTRI.sMaCB, HUUTRI.sMaCBo, HUUTRI.sMaCheDo, HUUTRI.sTenCbo
		from TBL_HUUTRI HUUTRI
		where HUUTRI.sMaCheDo = 'HUUTRI_TROCAPKHUVUC') HUUTRI_TROCAPKHUVUC
		on TBL_HUUTRI.sMaCBo = HUUTRI_TROCAPKHUVUC.sMaCBo and TBL_HUUTRI.sMaDonVi = HUUTRI_TROCAPKHUVUC.sMaDonVi
		left join
		(select HUUTRI2.Id, HUUTRI2.sMaDonVi, HUUTRI2.nGiaTri, HUUTRI2.sMaCB, HUUTRI2.sMaCBo, HUUTRI2.sMaCheDo, HUUTRI2.sTenCbo
		from TBL_HUUTRI HUUTRI2
		where HUUTRI2.sMaCheDo = 'HUUTRI_TROCAP1LAN') HUUTRI_TROCAP1LAN
		on TBL_HUUTRI.sMaCBo = HUUTRI_TROCAP1LAN.sMaCBo and TBL_HUUTRI.sMaDonVi = HUUTRI_TROCAP1LAN.sMaDonVi

	-- Data tro cap Phuc vien
	select distinct
		TBL_HUUTRI.sMaCB,
		TBL_HUUTRI.sMaCBo,
		TBL_HUUTRI.sMaCheDo,
		TBL_HUUTRI.sTenCbo,
		TBL_HUUTRI.Ten_DonVi,
		PHUCVIEN_TROCAP1LAN.nGiaTri fPHUCVIEN_TROCAP1LAN,
		PHUCVIEN_TROCAPKHUVUC.nGiaTri fPHUCVIEN_TROCAPKHUVUC,
		chedocha.sSoQuyetDinh,
		chedocha.dNgayQuyetDinh
		into TBL_PHUCVIEN_DOC
	from TBL_HUUTRI TBL_HUUTRI
		left join (select top 1 * from TL_CanBo_CheDoBHXH 
		where sMaCheDo in ('PHUCVIEN_TROCAPKHUVUC', 'PHUCVIEN_TROCAP1LAN')
		and ((sSoQuyetDinh is not null and sSoQuyetDinh <> '') 
		or (dNgayQuyetDinh is not null and dNgayQuyetDinh <> ''))) chedocha
		on TBL_HUUTRI.sMaCBo = chedocha.sMaCanBo
		left join
		(select HUUTRI.Id, HUUTRI.sMaDonVi, HUUTRI.nGiaTri, HUUTRI.sMaCB, HUUTRI.sMaCBo, HUUTRI.sMaCheDo, HUUTRI.sTenCbo
		from TBL_HUUTRI HUUTRI
		where HUUTRI.sMaCheDo = 'PHUCVIEN_TROCAPKHUVUC') PHUCVIEN_TROCAPKHUVUC
		on TBL_HUUTRI.sMaCBo = PHUCVIEN_TROCAPKHUVUC.sMaCBo and TBL_HUUTRI.sMaDonVi = PHUCVIEN_TROCAPKHUVUC.sMaDonVi
		left join
		(select HUUTRI2.Id, HUUTRI2.sMaDonVi, HUUTRI2.nGiaTri, HUUTRI2.sMaCB, HUUTRI2.sMaCBo, HUUTRI2.sMaCheDo, HUUTRI2.sTenCbo
		from TBL_HUUTRI HUUTRI2
		where HUUTRI2.sMaCheDo = 'PHUCVIEN_TROCAP1LAN') PHUCVIEN_TROCAP1LAN
		on TBL_HUUTRI.sMaCBo = PHUCVIEN_TROCAP1LAN.sMaCBo and TBL_HUUTRI.sMaDonVi = PHUCVIEN_TROCAP1LAN.sMaDonVi

	-- Data tro cap Thoi Viec
	select distinct
		TBL_HUUTRI.sMaCB,
		TBL_HUUTRI.sMaCBo,
		TBL_HUUTRI.sMaCheDo,
		TBL_HUUTRI.sTenCbo,
		TBL_HUUTRI.Ten_DonVi,
		THOIVIEC_TROCAP1LAN.nGiaTri fTHOIVIEC_TROCAP1LAN,
		THOIVIEC_TROCAPKHUVUC.nGiaTri fTHOIVIEC_TROCAPKHUVUC,
		chedocha.sSoQuyetDinh,
		chedocha.dNgayQuyetDinh
		into TBL_THOIVIEC_DOC
	from TBL_HUUTRI TBL_HUUTRI
		left join (select top 1 * from TL_CanBo_CheDoBHXH 
		where sMaCheDo in ('THOIVIEC_TROCAPKHUVUC', 'THOIVIEC_TROCAP1LAN')
		and ((sSoQuyetDinh is not null and sSoQuyetDinh <> '') 
		or (dNgayQuyetDinh is not null and dNgayQuyetDinh <> ''))) chedocha
		on TBL_HUUTRI.sMaCBo = chedocha.sMaCanBo
		left join
		(select HUUTRI.Id, HUUTRI.sMaDonVi, HUUTRI.nGiaTri, HUUTRI.sMaCB, HUUTRI.sMaCBo, HUUTRI.sMaCheDo, HUUTRI.sTenCbo
		from TBL_HUUTRI HUUTRI
		where HUUTRI.sMaCheDo = 'THOIVIEC_TROCAPKHUVUC') THOIVIEC_TROCAPKHUVUC
		on TBL_HUUTRI.sMaCBo = THOIVIEC_TROCAPKHUVUC.sMaCBo and TBL_HUUTRI.sMaDonVi = THOIVIEC_TROCAPKHUVUC.sMaDonVi
		left join
		(select HUUTRI2.Id, HUUTRI2.sMaDonVi, HUUTRI2.nGiaTri, HUUTRI2.sMaCB, HUUTRI2.sMaCBo, HUUTRI2.sMaCheDo, HUUTRI2.sTenCbo
		from TBL_HUUTRI HUUTRI2
		where HUUTRI2.sMaCheDo = 'THOIVIEC_TROCAP1LAN') THOIVIEC_TROCAP1LAN
		on TBL_HUUTRI.sMaCBo = THOIVIEC_TROCAP1LAN.sMaCBo and TBL_HUUTRI.sMaDonVi = THOIVIEC_TROCAP1LAN.sMaDonVi

	-- Data tro cap Tu tuat
	select distinct
		TBL_HUUTRI.sMaCB,
		TBL_HUUTRI.sMaCBo,
		TBL_HUUTRI.sMaCheDo,
		TBL_HUUTRI.sTenCbo,
		TBL_HUUTRI.Ten_DonVi,
		TUTUAT_TROCAP1LAN.nGiaTri fTUTUAT_TROCAP1LAN,
		TUTUAT_TROCAPKHUVUC.nGiaTri fTUTUAT_TROCAPKHUVUC,
		TROCAPMAITANG.nGiaTri fTROCAPMAITANG,
		chedocha.sSoQuyetDinh,
		chedocha.dNgayQuyetDinh
		into TBL_TUTUAT_DOC
	from TBL_HUUTRI TBL_HUUTRI
		left join (select top 1 * from TL_CanBo_CheDoBHXH 
		where sMaCheDo in ('TUTUAT_TROCAPKHUVUC', 'TROCAPMAITANG', 'TUTUAT_TROCAP1LAN')
		and ((sSoQuyetDinh is not null and sSoQuyetDinh <> '') 
		or (dNgayQuyetDinh is not null and dNgayQuyetDinh <> ''))) chedocha
		on TBL_HUUTRI.sMaCBo = chedocha.sMaCanBo
		left join
		(select HUUTRI.Id, HUUTRI.sMaDonVi, HUUTRI.nGiaTri, HUUTRI.sMaCB, HUUTRI.sMaCBo, HUUTRI.sMaCheDo, HUUTRI.sTenCbo
		from TBL_HUUTRI HUUTRI
		where HUUTRI.sMaCheDo = 'TUTUAT_TROCAPKHUVUC') TUTUAT_TROCAPKHUVUC
		on TBL_HUUTRI.sMaCBo = TUTUAT_TROCAPKHUVUC.sMaCBo and TBL_HUUTRI.sMaDonVi = TUTUAT_TROCAPKHUVUC.sMaDonVi
		left join
		(select HUUTRI1.Id, HUUTRI1.sMaDonVi, HUUTRI1.nGiaTri, HUUTRI1.sMaCB, HUUTRI1.sMaCBo, HUUTRI1.sMaCheDo, HUUTRI1.sTenCbo
		from TBL_HUUTRI HUUTRI1
		where HUUTRI1.sMaCheDo = 'TROCAPMAITANG') TROCAPMAITANG
		on TBL_HUUTRI.sMaCBo = TROCAPMAITANG.sMaCBo and TBL_HUUTRI.sMaDonVi = TROCAPMAITANG.sMaDonVi
		left join
		(select HUUTRI2.Id, HUUTRI2.sMaDonVi, HUUTRI2.nGiaTri, HUUTRI2.sMaCB, HUUTRI2.sMaCBo, HUUTRI2.sMaCheDo, HUUTRI2.sTenCbo
		from TBL_HUUTRI HUUTRI2
		where HUUTRI2.sMaCheDo = 'TUTUAT_TROCAP1LAN') TUTUAT_TROCAP1LAN
		on TBL_HUUTRI.sMaCBo = TUTUAT_TROCAP1LAN.sMaCBo and TBL_HUUTRI.sMaDonVi = TUTUAT_TROCAP1LAN.sMaDonVi

	--Lay tro cap Huu tri Si quan
	select * into TBL_HUUTRI_SQ from
	(select 1 bHangCha, 'I' LoaiTC, 1 RowNum, 'I' STT, null DoiTuong, null LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, N'TC Hưu trí' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 1 bHangCha, 'I' LoaiTC,  1 RowNum, null STT, 'SQ' DoiTuong, 'SQ' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'SQ' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 0 bHangCha, 'I' LoaiTC,  1 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'SQ' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fHUUTRI_TROCAP1LAN fTROCAP1LAN, fHUUTRI_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	from TBL_HUUTRI_DOC
	where sMaCB like '1%' and (isnull(fHUUTRI_TROCAP1LAN, 0) <> 0 or isnull(fHUUTRI_TROCAPKHUVUC, 0) <> 0)) sq
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_HUUTRI_SQ) > 2
		update TBL_HUUTRI_SQ set bHasData = 1

	--Lay tro cap Huu tri QNCN
	select * into TBL_HUUTRI_QNCN from
	(select 1 bHangCha, 'I' LoaiTC, 1 RowNum, 'I' STT, null DoiTuong, null LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, N'TC Hưu trí' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 1 bHangCha, 'I' LoaiTC,  2 RowNum, null STT, 'QNCN' DoiTuong, 'QNCN' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'QNCN' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 0 bHangCha, 'I' LoaiTC,  2 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'QNCN' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fHUUTRI_TROCAP1LAN fTROCAP1LAN, fHUUTRI_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	from TBL_HUUTRI_DOC
	where sMaCB like '2%' and (isnull(fHUUTRI_TROCAP1LAN, 0) <> 0 or isnull(fHUUTRI_TROCAPKHUVUC, 0) <> 0)) qncn
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_HUUTRI_QNCN) > 2
		update TBL_HUUTRI_QNCN set bHasData = 1

	--Lay tro cap Huu tri HSQ_BS
	select * into TBL_HUUTRI_HSQBS from
	(select 1 bHangCha, 'I' LoaiTC, 1 RowNum, 'I' STT, null DoiTuong, null LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, N'TC Hưu trí' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 1 bHangCha, 'I' LoaiTC,  3 RowNum, null STT, 'HSQ_BS' DoiTuong, 'HSQ_BS' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'HSQ_BS' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 0 bHangCha, 'I' LoaiTC,  3 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'HSQ_BS' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fHUUTRI_TROCAP1LAN fTROCAP1LAN, fHUUTRI_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	from TBL_HUUTRI_DOC
	where sMaCB like '0%' and (isnull(fHUUTRI_TROCAP1LAN, 0) <> 0 or isnull(fHUUTRI_TROCAPKHUVUC, 0) <> 0)) hsqbs
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_HUUTRI_HSQBS) > 2
		update TBL_HUUTRI_HSQBS set bHasData = 1

	--Lay tro cap Huu tri VCQP
	select * into TBL_HUUTRI_VCQP from
	(select 1 bHangCha, 'I' LoaiTC, 1 RowNum, 'I' STT, null DoiTuong, null LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, N'TC Hưu trí' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 1 bHangCha, 'I' LoaiTC,  4 RowNum, null STT, 'VCQP' DoiTuong, 'VCQP' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'VCQP' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG, 0 bHasData
	union all
	select 0 bHangCha, 'I' LoaiTC,  4 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'VCQP' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fHUUTRI_TROCAP1LAN fTROCAP1LAN, fHUUTRI_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	from TBL_HUUTRI_DOC
	where sMaCB in ('3.1', '3.2', '3.3') and (isnull(fHUUTRI_TROCAP1LAN, 0) <> 0 or isnull(fHUUTRI_TROCAPKHUVUC, 0) <> 0)) vcqp
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_HUUTRI_VCQP) > 2
		update TBL_HUUTRI_VCQP set bHasData = 1

	--Lay tro cap Huu tri HDLD
	select * into TBL_HUUTRI_LDHD from
	(select 1 bHangCha, 'I' LoaiTC, 1 RowNum, 'I' STT, null DoiTuong, null LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, N'TC Hưu trí' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 1 bHangCha, 'I' LoaiTC,  5 RowNum, null STT, 'LDHD' DoiTuong, 'LDHD' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'LDHD' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null  fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG, 0 bHasData
	union all
	select 0 bHangCha, 'I' LoaiTC,  5 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'LDHD' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fHUUTRI_TROCAP1LAN fTROCAP1LAN, fHUUTRI_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	from TBL_HUUTRI_DOC
	where sMaCB = '43' and (isnull(fHUUTRI_TROCAP1LAN, 0) <> 0 or isnull(fHUUTRI_TROCAPKHUVUC, 0) <> 0)) ldhd
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_HUUTRI_LDHD) > 2
		update TBL_HUUTRI_LDHD set bHasData = 1
	-----------------------------------
	--Lay tro cap Phuc vien Si quan
	select * into TBL_PHUCVIEN_SQ from
	(select 1 bHangCha, 'II' LoaiTC, 1 RowNum, 'II' STT, null DoiTuong, null LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, N'TC Phục viên' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 1 bHangCha, 'II' LoaiTC, 1 RowNum, null STT, 'SQ' DoiTuong, 'SQ' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'SQ' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 0 bHangCha, 'II' LoaiTC, 1 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'SQ' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fPHUCVIEN_TROCAP1LAN fTROCAP1LAN, fPHUCVIEN_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	from TBL_PHUCVIEN_DOC
	where sMaCB like '1%' and (isnull(fPHUCVIEN_TROCAP1LAN, 0) <> 0 or isnull(fPHUCVIEN_TROCAPKHUVUC, 0) <> 0)) pvsq
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_PHUCVIEN_SQ) > 2
		update TBL_PHUCVIEN_SQ set bHasData = 1

	--Lay tro cap Phuc vien QNCN
	select * into TBL_PHUCVIEN_QNCN from
	(select 1 bHangCha, 'II' LoaiTC, 1 RowNum, 'II' STT, null DoiTuong, null LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, N'TC Phục viên' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 1 bHangCha, 'II' LoaiTC, 2 RowNum, null STT, 'QNCN' DoiTuong, 'QNCN' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'QNCN' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 0 bHangCha, 'II' LoaiTC, 2 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'QNCN' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fPHUCVIEN_TROCAP1LAN fTROCAP1LAN, fPHUCVIEN_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	from TBL_PHUCVIEN_DOC
	where sMaCB like '2%' and (isnull(fPHUCVIEN_TROCAP1LAN, 0) <> 0 or isnull(fPHUCVIEN_TROCAPKHUVUC, 0) <> 0)) pvqncn
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_PHUCVIEN_QNCN) > 2
		update TBL_PHUCVIEN_QNCN set bHasData = 1

	--Lay tro cap Phuc vien HSQ_BS
	select * into TBL_PHUCVIEN_HSQBS from
	(select 1 bHangCha, 'II' LoaiTC, 1 RowNum, 'II' STT, null DoiTuong, null LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, N'TC Phục viên' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 1 bHangCha, 'II' LoaiTC, 3 RowNum, null STT, 'HSQ_BS' DoiTuong, 'HSQ_BS' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'HSQ_BS' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 0 bHangCha, 'II' LoaiTC, 3 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'HSQ_BS' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fPHUCVIEN_TROCAP1LAN fTROCAP1LAN, fPHUCVIEN_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	from TBL_PHUCVIEN_DOC
	where sMaCB like '0%' and (isnull(fPHUCVIEN_TROCAP1LAN, 0) <> 0 or isnull(fPHUCVIEN_TROCAPKHUVUC, 0) <> 0)) pvhsqbs
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_PHUCVIEN_HSQBS) > 2
		update TBL_PHUCVIEN_HSQBS set bHasData = 1

	--Lay tro cap Phuc vien VCQP
	select * into TBL_PHUCVIEN_VCQP from
	(select 1 bHangCha, 'II' LoaiTC, 1 RowNum, 'II' STT, null DoiTuong, null LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, N'TC Phục viên' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 1 bHangCha, 'II' LoaiTC, 4 RowNum, null STT, 'VCQP' DoiTuong, 'VCQP' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'VCQP' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG, 0 bHasData
	union all
	select 0 bHangCha, 'II' LoaiTC, 4 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'VCQP' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fPHUCVIEN_TROCAP1LAN fTROCAP1LAN, fPHUCVIEN_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	from TBL_PHUCVIEN_DOC
	where sMaCB in ('3.1', '3.2', '3.3') and (isnull(fPHUCVIEN_TROCAP1LAN, 0) <> 0 or isnull(fPHUCVIEN_TROCAPKHUVUC, 0) <> 0)) pvvcqp
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_PHUCVIEN_VCQP) > 2
		update TBL_PHUCVIEN_VCQP set bHasData = 1

	--Lay tro cap Phuc vien HDLD
	select * into TBL_PHUCVIEN_LDHD from
	(select 1 bHangCha, 'II' LoaiTC, 1 RowNum, 'II' STT, null DoiTuong, null LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, N'TC Phục viên' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 1 bHangCha, 'II' LoaiTC, 5 RowNum, null STT, 'LDHD' DoiTuong, 'LDHD' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'LDHD' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null  fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG, 0 bHasData
	union all
	select 0 bHangCha, 'II' LoaiTC, 5 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'LDHD' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fPHUCVIEN_TROCAP1LAN fTROCAP1LAN, fPHUCVIEN_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	from TBL_PHUCVIEN_DOC
	where sMaCB = '43' and (isnull(fPHUCVIEN_TROCAP1LAN, 0) <> 0 or isnull(fPHUCVIEN_TROCAPKHUVUC, 0) <> 0)) pvldhd
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_PHUCVIEN_LDHD) > 2
		update TBL_PHUCVIEN_LDHD set bHasData = 1
	--------------------------------------------
	--Lay tro cap Thoi viec Si quan
	select * into TBL_THOIVIEC_SQ from
	(select 1 bHangCha, 'III' LoaiTC, 1 RowNum, 'III' STT, null DoiTuong, null LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, N'TC Thôi việc' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 1 bHangCha, 'III' LoaiTC, 1 RowNum, null STT, 'SQ' DoiTuong, 'SQ' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'SQ' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 0 bHangCha, 'III' LoaiTC, 1 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'SQ' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTHOIVIEC_TROCAP1LAN fTROCAP1LAN, fTHOIVIEC_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	from TBL_THOIVIEC_DOC
	where sMaCB like '1%' and (isnull(fTHOIVIEC_TROCAP1LAN, 0) <> 0 or isnull(fTHOIVIEC_TROCAPKHUVUC, 0) <> 0)) tvsq
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_THOIVIEC_SQ) > 2
		update TBL_THOIVIEC_SQ set bHasData = 1

	--Lay tro cap Thoi viec QNCN
	select * into TBL_THOIVIEC_QNCN from
	(select 1 bHangCha, 'III' LoaiTC, 1 RowNum, 'III' STT, null DoiTuong, null LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, N'TC Thôi việc' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 1 bHangCha, 'III' LoaiTC, 2 RowNum, null STT, 'QNCN' DoiTuong, 'QNCN' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'QNCN' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 0 bHangCha, 'III' LoaiTC, 2 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'QNCN' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTHOIVIEC_TROCAP1LAN fTROCAP1LAN, fTHOIVIEC_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	from TBL_THOIVIEC_DOC
	where sMaCB like '2%' and (isnull(fTHOIVIEC_TROCAP1LAN, 0) <> 0 or isnull(fTHOIVIEC_TROCAPKHUVUC, 0) <> 0)) tvqncn
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_THOIVIEC_QNCN) > 2
		update TBL_THOIVIEC_QNCN set bHasData = 1

	--Lay tro cap Thoi viec HSQ_BS
	select * into TBL_THOIVIEC_HSQBS from
	(select 1 bHangCha, 'III' LoaiTC, 1 RowNum, 'III' STT, null DoiTuong, null LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, N'TC Thôi việc' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 1 bHangCha, 'III' LoaiTC, 3 RowNum, null STT, 'HSQ_BS' DoiTuong, 'HSQ_BS' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'HSQ_BS' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 0 bHangCha, 'III' LoaiTC, 3 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'HSQ_BS' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTHOIVIEC_TROCAP1LAN fTROCAP1LAN, fTHOIVIEC_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	from TBL_THOIVIEC_DOC
	where sMaCB like '0%' and (isnull(fTHOIVIEC_TROCAP1LAN, 0) <> 0 or isnull(fTHOIVIEC_TROCAPKHUVUC, 0) <> 0)) tvhsqbs
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_THOIVIEC_HSQBS) > 2
		update TBL_THOIVIEC_HSQBS set bHasData = 1

	--Lay tro cap Thoi viec VCQP
	select * into TBL_THOIVIEC_VCQP from
	(select 1 bHangCha, 'III' LoaiTC, 1 RowNum, 'III' STT, null DoiTuong, null LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, N'TC Thôi việc' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 1 bHangCha, 'III' LoaiTC, 4 RowNum, null STT, 'VCQP' DoiTuong, 'VCQP' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'VCQP' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG, 0 bHasData
	union all
	select 0 bHangCha, 'III' LoaiTC, 4 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'VCQP' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTHOIVIEC_TROCAP1LAN fTROCAP1LAN, fTHOIVIEC_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	from TBL_THOIVIEC_DOC
	where sMaCB in ('3.1', '3.2', '3.3') and (isnull(fTHOIVIEC_TROCAP1LAN, 0) <> 0 or isnull(fTHOIVIEC_TROCAPKHUVUC, 0) <> 0)) tvvcqp
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_THOIVIEC_VCQP) > 2
		update TBL_THOIVIEC_VCQP set bHasData = 1

	--Lay tro cap Thoi viec HDLD
	select * into TBL_THOIVIEC_LDHD from
	(select 1 bHangCha, 'III' LoaiTC, 1 RowNum, 'III' STT, null DoiTuong, null LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, N'TC Thôi việc' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 1 bHangCha, 'III' LoaiTC, 5 RowNum, null STT, 'LDHD' DoiTuong, 'LDHD' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'LDHD' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null  fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG, 0 bHasData
	union all
	select 0 bHangCha, 'III' LoaiTC, 5 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'LDHD' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTHOIVIEC_TROCAP1LAN fTROCAP1LAN, fTHOIVIEC_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	from TBL_THOIVIEC_DOC
	where sMaCB = '43' and (isnull(fTHOIVIEC_TROCAP1LAN, 0) <> 0 or isnull(fTHOIVIEC_TROCAPKHUVUC, 0) <> 0)) tvldhd
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_THOIVIEC_LDHD) > 2
		update TBL_THOIVIEC_LDHD set bHasData = 1
	--------------------------------------------
	--Lay tro cap Tu tuat Si quan
	select * into TBL_TUTUAT_SQ from
	(select 1 bHangCha, 'IV' LoaiTC, 1 RowNum, 'IV' STT, null DoiTuong, null LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, N'TC Tử tuất' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 1 bHangCha, 'IV' LoaiTC, 1 RowNum, null STT, 'SQ' DoiTuong, 'SQ' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'SQ' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 0 bHangCha, 'IV' LoaiTC, 1 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'SQ' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTUTUAT_TROCAP1LAN fTROCAP1LAN, fTUTUAT_TROCAPKHUVUC fTROCAPKHUVUC, fTROCAPMAITANG, 0 bHasData
	from TBL_TUTUAT_DOC
	where sMaCB like '1%' and (isnull(fTUTUAT_TROCAP1LAN, 0) <> 0 or isnull(fTUTUAT_TROCAPKHUVUC, 0) <> 0)) tvsq
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TUTUAT_SQ) > 2
		update TBL_TUTUAT_SQ set bHasData = 1

	--Lay tro cap Tu tuat QNCN
	select * into TBL_TUTUAT_QNCN from
	(select 1 bHangCha, 'IV' LoaiTC, 1 RowNum, 'IV' STT, null DoiTuong, null LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, N'TC Tử tuất' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 1 bHangCha, 'IV' LoaiTC, 2 RowNum, null STT, 'QNCN' DoiTuong, 'QNCN' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'QNCN' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 0 bHangCha, 'IV' LoaiTC, 2 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'QNCN' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTUTUAT_TROCAP1LAN fTROCAP1LAN, fTUTUAT_TROCAPKHUVUC fTROCAPKHUVUC, fTROCAPMAITANG, 0 bHasData
	from TBL_TUTUAT_DOC
	where sMaCB like '2%' and (isnull(fTUTUAT_TROCAP1LAN, 0) <> 0 or isnull(fTUTUAT_TROCAPKHUVUC, 0) <> 0)) tvqncn
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TUTUAT_QNCN) > 2
		update TBL_TUTUAT_QNCN set bHasData = 1

	--Lay tro cap Tu tuat HSQ_BS
	select * into TBL_TUTUAT_HSQBS from
	(select 1 bHangCha, 'IV' LoaiTC, 1 RowNum, 'IV' STT, null DoiTuong, null LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, N'TC Tử tuất' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 1 bHangCha, 'IV' LoaiTC, 3 RowNum, null STT, 'HSQ_BS' DoiTuong, 'HSQ_BS' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'HSQ_BS' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 0 bHangCha, 'IV' LoaiTC, 3 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'HSQ_BS' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTUTUAT_TROCAP1LAN fTROCAP1LAN, fTUTUAT_TROCAPKHUVUC fTROCAPKHUVUC, fTROCAPMAITANG, 0 bHasData
	from TBL_TUTUAT_DOC
	where sMaCB like '0%' and (isnull(fTUTUAT_TROCAP1LAN, 0) <> 0 or isnull(fTUTUAT_TROCAPKHUVUC, 0) <> 0)) tvhsqbs
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TUTUAT_HSQBS) > 2
		update TBL_TUTUAT_HSQBS set bHasData = 1

	--Lay tro cap Tu tuat VCQP
	select * into TBL_TUTUAT_VCQP from
	(select 1 bHangCha, 'IV' LoaiTC, 1 RowNum, 'IV' STT, null DoiTuong, null LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, N'TC Tử tuất' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 1 bHangCha, 'IV' LoaiTC, 4 RowNum, null STT, 'VCQP' DoiTuong, 'VCQP' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'VCQP' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG, 0 bHasData
	union all
	select 0 bHangCha, 'IV' LoaiTC, 4 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'VCQP' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTUTUAT_TROCAP1LAN fTROCAP1LAN, fTUTUAT_TROCAPKHUVUC fTROCAPKHUVUC, fTROCAPMAITANG, 0 bHasData
	from TBL_TUTUAT_DOC
	where sMaCB in ('3.1', '3.2', '3.3') and (isnull(fTUTUAT_TROCAP1LAN, 0) <> 0 or isnull(fTUTUAT_TROCAPKHUVUC, 0) <> 0)) tvvcqp
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TUTUAT_VCQP) > 2
		update TBL_TUTUAT_VCQP set bHasData = 1

	--Lay tro cap Tu tuat HDLD
	select * into TBL_TUTUAT_LDHD from
	(select 1 bHangCha, 'IV' LoaiTC, 1 RowNum, 'IV' STT, null DoiTuong, null LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, N'TC Tử tuất' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 1 bHangCha, 'IV' LoaiTC, 5 RowNum, null STT, 'LDHD' DoiTuong, 'LDHD' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'LDHD' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null  fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG, 0 bHasData
	union all
	select 0 bHangCha, 'IV' LoaiTC, 5 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'LDHD' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTUTUAT_TROCAP1LAN fTROCAP1LAN, fTUTUAT_TROCAPKHUVUC fTROCAPKHUVUC, fTROCAPMAITANG, 0 bHasData
	from TBL_TUTUAT_DOC
	where sMaCB = '43' and (isnull(fTUTUAT_TROCAP1LAN, 0) <> 0 or isnull(fTUTUAT_TROCAPKHUVUC, 0) <> 0)) tvldhd
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TUTUAT_LDHD) > 2
		update TBL_TUTUAT_LDHD set bHasData = 1
	----------------------------------------------
	--Ket qua
	select result.* into TBL_HUUTRI_RESULT from
	(select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_HUUTRI_SQ
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_HUUTRI_QNCN
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_HUUTRI_HSQBS
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_HUUTRI_VCQP
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_HUUTRI_LDHD
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_PHUCVIEN_SQ
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_PHUCVIEN_QNCN
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_PHUCVIEN_HSQBS
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_PHUCVIEN_VCQP
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_PHUCVIEN_LDHD
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_THOIVIEC_SQ
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_THOIVIEC_QNCN
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_THOIVIEC_HSQBS
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_THOIVIEC_VCQP
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_THOIVIEC_LDHD
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_TUTUAT_SQ
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_TUTUAT_QNCN
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_TUTUAT_HSQBS
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_TUTUAT_VCQP
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_TUTUAT_LDHD) result

	select distinct
		LoaiTC,
		RowNum,
		STT,
		DoiTuong, 
		LoaiDoiTuong,
		sMaCB MaCb,
		sMaCBo MaCbo,
		sTenCbo TenCbo,
		sSoQuyetDinh SSoQuyetDinh,
		dNgayQuyetDinh DNgayQuyetDinh,
		TenDonVi,
		fTROCAP1LAN/@DonViTinh fTroCap1Lan,
		fTROCAPKHUVUC/@DonViTinh fTroCapKhuVuc,
		fTROCAPMAITANG/@DonViTinh fTroCapMaiTang,
		fTongSoTien/@DonViTinh fTongSoTien,
		bHangCha IsHangCha,
		bHasData IsHasData
	from TBL_HUUTRI_RESULT
	order by LoaiTC, RowNum, LoaiDoiTuong, DoiTuong desc

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI]') AND type in (N'U')) drop table TBL_HUUTRI;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI_DOC]') AND type in (N'U')) drop table TBL_HUUTRI_DOC;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_PHUCVIEN_DOC]') AND type in (N'U')) drop table TBL_PHUCVIEN_DOC;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THOIVIEC_DOC]') AND type in (N'U')) drop table TBL_THOIVIEC_DOC;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TUTUAT_DOC]') AND type in (N'U')) drop table TBL_TUTUAT_DOC;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI_SQ]') AND type in (N'U')) drop table TBL_HUUTRI_SQ;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI_QNCN]') AND type in (N'U')) drop table TBL_HUUTRI_QNCN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI_HSQBS]') AND type in (N'U')) drop table TBL_HUUTRI_HSQBS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI_VCQP]') AND type in (N'U')) drop table TBL_HUUTRI_VCQP;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI_LDHD]') AND type in (N'U')) drop table TBL_HUUTRI_LDHD;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_PHUCVIEN_SQ]') AND type in (N'U')) drop table TBL_PHUCVIEN_SQ;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_PHUCVIEN_QNCN]') AND type in (N'U')) drop table TBL_PHUCVIEN_QNCN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_PHUCVIEN_HSQBS]') AND type in (N'U')) drop table TBL_PHUCVIEN_HSQBS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_PHUCVIEN_VCQP]') AND type in (N'U')) drop table TBL_PHUCVIEN_VCQP;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_PHUCVIEN_LDHD]') AND type in (N'U')) drop table TBL_PHUCVIEN_LDHD;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THOIVIEC_SQ]') AND type in (N'U')) drop table TBL_THOIVIEC_SQ;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THOIVIEC_QNCN]') AND type in (N'U')) drop table TBL_THOIVIEC_QNCN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THOIVIEC_HSQBS]') AND type in (N'U')) drop table TBL_THOIVIEC_HSQBS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THOIVIEC_VCQP]') AND type in (N'U')) drop table TBL_THOIVIEC_VCQP;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THOIVIEC_LDHD]') AND type in (N'U')) drop table TBL_THOIVIEC_LDHD;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TUTUAT_SQ]') AND type in (N'U')) drop table TBL_TUTUAT_SQ;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TUTUAT_QNCN]') AND type in (N'U')) drop table TBL_TUTUAT_QNCN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TUTUAT_HSQBS]') AND type in (N'U')) drop table TBL_TUTUAT_HSQBS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TUTUAT_VCQP]') AND type in (N'U')) drop table TBL_TUTUAT_VCQP;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TUTUAT_LDHD]') AND type in (N'U')) drop table TBL_TUTUAT_LDHD;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI_RESULT]') AND type in (N'U')) drop table TBL_HUUTRI_RESULT;

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_om_dau]    Script Date: 11/29/2023 10:30:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_om_dau]
	@DsMaDonVi nvarchar(100),
	@NamLamViec int,
	@Thang int,
	@DonViTinh int
AS
BEGIN
	--Lay thong tin luong theo tro cap om dau
	select * into TBL_TCOD from
	(select luongcancu.fLuongCanCu, chedo.fSoNgayHuongBHXH, donvi.Ten_DonVi, luong.*
	from TL_BangLuong_ThangBHXH luong
	join TL_DM_DonVi donvi on luong.sMaDonVi = donvi.Ma_DonVi
	join TL_CanBo_CheDoBHXH chedo on luong.sMaCBo = chedo.sMaCanBo and luong.sMaCheDo = chedo.sMaCheDo
	left join 
	(select Ma_CBo, NAM, thang, Gia_Tri fLuongCanCu from TL_BangLuong_Thang
		where NAM = @NamLamViec
		and thang = @Thang
		and Ma_PhuCap = 'LHT_TT') luongcancu on luong.sMaCBo = luongcancu.Ma_CBo
	where 
	luong.sMaDonVi in (SELECT * FROM f_split(@DsMaDonVi))
	and luong.iNam = @NamLamViec
	and luong.iThang = @Thang
	and luong.sMaCheDo in ('BENHDAINGAY_D14NGAY', 'BENHDAINGAY_T14NGAY', 'OMKHAC_D14NGAY', 'OMKHAC_T14NGAY', 'CONOM', 'OMDAU_DUONGSUCPHSK')) tcod

	select distinct 
		TBL_TCOD.sMaCB,
		TBL_TCOD.sMaCBo,
		--TBL_TCOD.sMaCheDo,
		TBL_TCOD.sTenCbo,
		TBL_TCOD.Ten_DonVi,
		BENHDAINGAYD14NGAY.SoNgayBENHDAINGAYD14NGAY SoNgayBenhDaiNgayD14Ngay,
		BENHDAINGAY_T14NGAY.SoNgayBenhDaiNgayT14Ngay,
		OMKHAC_D14NGAY.SoNgayOmKhacD14Ngay,
		OMKHAC_T14NGAY.SoNgayOmKhacT14Ngay,
		CONOM.SoNgayConOm,
		DUONGSUCPHSK.SoNgayDuongSuc,
		TBL_TCOD.fLuongCanCu,
		BENHDAINGAYD14NGAY.nGiaTri fBENHDAINGAY_D14NGAY,
		BENHDAINGAY_T14NGAY.nGiaTri fBENHDAINGAY_T14NGAY,
		OMKHAC_D14NGAY.nGiaTri fOMKHAC_D14NGAY,
		OMKHAC_T14NGAY.nGiaTri fOMKHAC_T14NGAY,
		CONOM.nGiaTri fCONOM,
		DUONGSUCPHSK.nGiaTri fDUONGSUCPHSK
		into TBL_TCOD_DOC
	from TBL_TCOD TBL_TCOD
		left join
		(select tcod.Id, tcod.sMaDonVi, tcod.nGiaTri, tcod.sMaCB, tcod.sMaCBo, tcod.sMaCheDo, tcod.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayBenhDaiNgayT14Ngay
		from TBL_TCOD tcod left join TL_CanBo_CheDoBHXH chedo on tcod.sMaCBo = chedo.sMaCanBo and tcod.sMaCheDo = chedo.sMaCheDo
		where tcod.sMaCheDo = 'BENHDAINGAY_T14NGAY') BENHDAINGAY_T14NGAY
		on TBL_TCOD.sMaCBo = BENHDAINGAY_T14NGAY.sMaCBo and TBL_TCOD.sMaDonVi = BENHDAINGAY_T14NGAY.sMaDonVi
		left join
		(select tcod_1.Id, tcod_1.sMaDonVi, tcod_1.nGiaTri, tcod_1.sMaCB, tcod_1.sMaCBo, tcod_1.sMaCheDo, tcod_1.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayOmKhacD14Ngay
		from TBL_TCOD tcod_1 left join TL_CanBo_CheDoBHXH chedo on tcod_1.sMaCBo = chedo.sMaCanBo and tcod_1.sMaCheDo = chedo.sMaCheDo
		where tcod_1.sMaCheDo = 'OMKHAC_D14NGAY') OMKHAC_D14NGAY
		on TBL_TCOD.sMaCBo = OMKHAC_D14NGAY.sMaCBo and TBL_TCOD.sMaDonVi = OMKHAC_D14NGAY.sMaDonVi
		left join
		(select tcod_2.Id, tcod_2.sMaDonVi, tcod_2.nGiaTri, tcod_2.sMaCB, tcod_2.sMaCBo, tcod_2.sMaCheDo, tcod_2.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayOmKhacT14Ngay
		from TBL_TCOD tcod_2 left join TL_CanBo_CheDoBHXH chedo on tcod_2.sMaCBo = chedo.sMaCanBo and tcod_2.sMaCheDo = chedo.sMaCheDo
		where tcod_2.sMaCheDo = 'OMKHAC_T14NGAY') OMKHAC_T14NGAY
		on TBL_TCOD.sMaCBo = OMKHAC_T14NGAY.sMaCBo and TBL_TCOD.sMaDonVi = OMKHAC_T14NGAY.sMaDonVi
		left join
		(select conom.Id, conom.sMaDonVi, conom.nGiaTri, conom.sMaCB, conom.sMaCBo, conom.sMaCheDo, conom.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayConOm
		from TBL_TCOD conom left join TL_CanBo_CheDoBHXH chedo on conom.sMaCBo = chedo.sMaCanBo and conom.sTenCbo = chedo.sMaCheDo
		where conom.sMaCheDo = 'CONOM') CONOM
		on TBL_TCOD.sMaCBo = CONOM.sMaCBo and TBL_TCOD.sMaDonVi = CONOM.sMaDonVi
		left join
		(select duongsuc.Id, duongsuc.sMaDonVi, duongsuc.nGiaTri, duongsuc.sMaCB, duongsuc.sMaCBo, duongsuc.sMaCheDo, duongsuc.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayDuongSuc
		from TBL_TCOD duongsuc left join TL_CanBo_CheDoBHXH chedo on duongsuc.sMaCBo = chedo.sMaCanBo and duongsuc.sMaCheDo = chedo.sMaCheDo
		where duongsuc.sMaCheDo = 'OMDAU_DUONGSUCPHSK') DUONGSUCPHSK
		on TBL_TCOD.sMaCBo = DUONGSUCPHSK.sMaCBo and TBL_TCOD.sMaDonVi = DUONGSUCPHSK.sMaDonVi
		left join
		(select tcod_3.Id, tcod_3.sMaDonVi, tcod_3.nGiaTri, tcod_3.sMaCB, tcod_3.sMaCBo, tcod_3.sMaCheDo, tcod_3.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayBENHDAINGAYD14NGAY
		from TBL_TCOD tcod_3 left join TL_CanBo_CheDoBHXH chedo on tcod_3.sMaCBo = chedo.sMaCanBo and tcod_3.sMaCheDo = chedo.sMaCheDo
		where tcod_3.sMaCheDo = 'BENHDAINGAY_D14NGAY') BENHDAINGAYD14NGAY
		on TBL_TCOD.sMaCBo = BENHDAINGAYD14NGAY.sMaCBo and TBL_TCOD.sMaDonVi = BENHDAINGAYD14NGAY.sMaDonVi

	--Lấy lương Sĩ quan
	select * into TBL_TCOD_SQ from
	(select
		1 bHangCha, 1 RowNum, 'I' STT, 'SQ' DoiTuong, 'SQ' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'SQ' sTenCbo, null SoNgayBenhDaiNgayD14Ngay, null SoNgayBenhDaiNgayT14Ngay, null SoNgayOmKhacD14Ngay, null SoNgayOmKhacT14Ngay, null SoNgayConOm, null SoNgayDuongSuc, null fLuongCanCu, null fBENHDAINGAY_D14NGAY, null fBENHDAINGAY_T14NGAY, null fOMKHAC_D14NGAY, null fOMKHAC_T14NGAY, null fCONOM, null fDUONGSUCPHSK, 0 bHasData
	union all
	select
		0 bHangCha, 1 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'SQ' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK, 0 bHasData
	from TBL_TCOD_DOC
	where sMaCB like '1%' and (isnull(fLuongCanCu, 0) <> 0 or isnull(fBENHDAINGAY_D14NGAY, 0) <> 0 or isnull(fBENHDAINGAY_T14NGAY, 0) <> 0 or isnull(fOMKHAC_D14NGAY, 0) <> 0 or isnull(fOMKHAC_T14NGAY, 0) <> 0 or isnull(fCONOM, 0) <> 0  or isnull(fDUONGSUCPHSK, 0) <> 0)) sq
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCOD_SQ) > 1
		update TBL_TCOD_SQ set bHasData = 1

	--Lấy lương QNCN
	select * into TBL_TCOD_QNCN from
	(select
		1 bHangCha, 2 RowNum, 'II' STT, 'QNCN' DoiTuong, 'QNCN' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'QNCN' sTenCbo, null SoNgayBenhDaiNgayD14Ngay, null SoNgayBenhDaiNgayT14Ngay, null SoNgayOmKhacD14Ngay, null SoNgayOmKhacT14Ngay, null SoNgayConOm, null SoNgayDuongSuc, null fLuongCanCu, null fBENHDAINGAY_D14NGAY, null fBENHDAINGAY_T14NGAY, null fOMKHAC_D14NGAY, null fOMKHAC_T14NGAY, null fCONOM, null fDUONGSUCPHSK, 0 bHasData
	union all
	select
		0 bHangCha, 2 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'QNCN' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK, 0 bHasData
	from TBL_TCOD_DOC
	where sMaCB like '2%' and (isnull(fLuongCanCu, 0) <> 0 or isnull(fBENHDAINGAY_D14NGAY, 0) <> 0 or isnull(fBENHDAINGAY_T14NGAY, 0) <> 0 or isnull(fOMKHAC_D14NGAY, 0) <> 0 or isnull(fOMKHAC_T14NGAY, 0) <> 0 or isnull(fCONOM, 0) <> 0  or isnull(fDUONGSUCPHSK, 0) <> 0)) qncn
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCOD_QNCN) > 1
		update TBL_TCOD_QNCN set bHasData = 1

	--Lấy lương HSQ_BS
	select * into TBL_TCOD_HSQBS from
	(select
		1 bHangCha, 3 RowNum, 'III' STT, 'HSQ_BS' DoiTuong, 'HSQ_BS' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'HSQ_BS' sTenCbo, null SoNgayBenhDaiNgayD14Ngay, null SoNgayBenhDaiNgayT14Ngay, null SoNgayOmKhacD14Ngay, null SoNgayOmKhacT14Ngay, null SoNgayConOm, null SoNgayDuongSuc, null fLuongCanCu, null fBENHDAINGAY_D14NGAY, null fBENHDAINGAY_T14NGAY, null fOMKHAC_D14NGAY, null fOMKHAC_T14NGAY, null fCONOM, null fDUONGSUCPHSK, 0 bHasData
	union all
	select
		0 bHangCha, 3 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'HSQ_BS' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK, 0 bHasData
	from TBL_TCOD_DOC
	where sMaCB like '0%' and (isnull(fLuongCanCu, 0) <> 0 or isnull(fBENHDAINGAY_D14NGAY, 0) <> 0 or isnull(fBENHDAINGAY_T14NGAY, 0) <> 0 or isnull(fOMKHAC_D14NGAY, 0) <> 0 or isnull(fOMKHAC_T14NGAY, 0) <> 0 or isnull(fCONOM, 0) <> 0  or isnull(fDUONGSUCPHSK, 0) <> 0)) hsqbs
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCOD_HSQBS) > 1
		update TBL_TCOD_HSQBS set bHasData = 1

	--Lấy lương VCQP
	select * into TBL_TCOD_VCQP from
	(select
		1 bHangCha, 4 RowNum, 'IV' STT, 'VCQP' DoiTuong, 'VCQP' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'VCQP' sTenCbo, null SoNgayBenhDaiNgayD14Ngay, null SoNgayBenhDaiNgayT14Ngay, null SoNgayOmKhacD14Ngay, null SoNgayOmKhacT14Ngay, null SoNgayConOm, null SoNgayDuongSuc, null fLuongCanCu, null fBENHDAINGAY_D14NGAY, null fBENHDAINGAY_T14NGAY, null fOMKHAC_D14NGAY, null fOMKHAC_T14NGAY, null fCONOM, null fDUONGSUCPHSK, 0 bHasData
	union all
	select
		0 bHangCha, 4 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'VCQP' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK, 0 bHasData
	from TBL_TCOD_DOC
	where sMaCB in ('3.1', '3.2', '3.3') and (isnull(fLuongCanCu, 0) <> 0 or isnull(fBENHDAINGAY_D14NGAY, 0) <> 0 or isnull(fBENHDAINGAY_T14NGAY, 0) <> 0 or isnull(fOMKHAC_D14NGAY, 0) <> 0 or isnull(fOMKHAC_T14NGAY, 0) <> 0 or isnull(fCONOM, 0) <> 0  or isnull(fDUONGSUCPHSK, 0) <> 0)) vcqp
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCOD_VCQP) > 1
		update TBL_TCOD_VCQP set bHasData = 1

	--Lấy lương Lao động hợp đông
	select * into TBL_TCOD_LDHD from
	(select
		1 bHangCha, 5 RowNum, 'V' STT, 'LDHD' DoiTuong, 'LDHD' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'LDHD' sTenCbo, null SoNgayBenhDaiNgayD14Ngay, null SoNgayBenhDaiNgayT14Ngay, null SoNgayOmKhacD14Ngay, null SoNgayOmKhacT14Ngay, null SoNgayConOm, null SoNgayDuongSuc, null fLuongCanCu, null fBENHDAINGAY_D14NGAY, null fBENHDAINGAY_T14NGAY, null fOMKHAC_D14NGAY, null fOMKHAC_T14NGAY, null fCONOM, null fDUONGSUCPHSK, 0 bHasData
	union all
	select
		0 bHangCha, 5 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'LDHD' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK, 0 bHasData
	from TBL_TCOD_DOC
	where sMaCB = '43' and (isnull(fLuongCanCu, 0) <> 0 or isnull(fBENHDAINGAY_D14NGAY, 0) <> 0 or isnull(fBENHDAINGAY_T14NGAY, 0) <> 0 or isnull(fOMKHAC_D14NGAY, 0) <> 0 or isnull(fOMKHAC_T14NGAY, 0) <> 0 or isnull(fCONOM, 0) <> 0  or isnull(fDUONGSUCPHSK, 0) <> 0)) ldhd
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCOD_LDHD) > 1
		update TBL_TCOD_LDHD set bHasData = 1

	--Ket qua
	select result.* into TBL_TCOD_RESULT from
	(select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK,
	(isnull(fBENHDAINGAY_D14NGAY, 0) + isnull(fBENHDAINGAY_T14NGAY, 0) + isnull(fOMKHAC_D14NGAY, 0) + isnull(fOMKHAC_T14NGAY, 0) + isnull(fCONOM, 0) + isnull(fDUONGSUCPHSK, 0)) fTongSoTien
	from TBL_TCOD_SQ
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK,
	(isnull(fBENHDAINGAY_D14NGAY, 0) + isnull(fBENHDAINGAY_T14NGAY, 0) + isnull(fOMKHAC_D14NGAY, 0) + isnull(fOMKHAC_T14NGAY, 0) + isnull(fCONOM, 0) + isnull(fDUONGSUCPHSK, 0)) fTongSoTien
	from TBL_TCOD_QNCN
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK,
	(isnull(fBENHDAINGAY_D14NGAY, 0) + isnull(fBENHDAINGAY_T14NGAY, 0) + isnull(fOMKHAC_D14NGAY, 0) + isnull(fOMKHAC_T14NGAY, 0) + isnull(fCONOM, 0) + isnull(fDUONGSUCPHSK, 0)) fTongSoTien
	from TBL_TCOD_HSQBS
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK,
	(isnull(fBENHDAINGAY_D14NGAY, 0) + isnull(fBENHDAINGAY_T14NGAY, 0) + isnull(fOMKHAC_D14NGAY, 0) + isnull(fOMKHAC_T14NGAY, 0) + isnull(fCONOM, 0) + isnull(fDUONGSUCPHSK, 0)) fTongSoTien
	from TBL_TCOD_VCQP
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK,
	(isnull(fBENHDAINGAY_D14NGAY, 0) + isnull(fBENHDAINGAY_T14NGAY, 0) + isnull(fOMKHAC_D14NGAY, 0) + isnull(fOMKHAC_T14NGAY, 0) + isnull(fCONOM, 0) + isnull(fDUONGSUCPHSK, 0)) fTongSoTien
	from TBL_TCOD_LDHD) result

	select
		RowNum,
		STT,
		DoiTuong, 
		LoaiDoiTuong,
		sMaCB MaCb, 
		sMaCBo MaCbo,
		sTenCbo TenCbo,
		TenDonVi,
		SoNgayBenhDaiNgayD14Ngay, 
		SoNgayBenhDaiNgayT14Ngay, 
		SoNgayOmKhacD14Ngay, 
		SoNgayOmKhacT14Ngay, 
		SoNgayConOm, 
		SoNgayDuongSuc, 
		fLuongCanCu FLuongCanCu, 
		fBENHDAINGAY_D14NGAY/@DonViTinh FBenhDaiNgayD14Ngay, 
		fBENHDAINGAY_T14NGAY/@DonViTinh FBenhDauNgayT14Ngay, 
		fOMKHAC_D14NGAY/@DonViTinh FOmKhacD14Ngay, 
		fOMKHAC_T14NGAY/@DonViTinh FOmKhacT14Ngay, 
		fCONOM/@DonViTinh FConOm, 
		fDUONGSUCPHSK/@DonViTinh FDuongSucPHSK,
		fTongSoTien/@DonViTinh FTongSoTien,
		bHangCha IsHangCha,
		bHasData IsHasData
	from TBL_TCOD_RESULT
	order by RowNum, LoaiDoiTuong, DoiTuong desc

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCOD]') AND type in (N'U'))
	drop table TBL_TCOD;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCOD_DOC]') AND type in (N'U'))
	drop table TBL_TCOD_DOC;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCOD_SQ]') AND type in (N'U'))
	drop table TBL_TCOD_SQ;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCOD_QNCN]') AND type in (N'U'))
	drop table TBL_TCOD_QNCN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCOD_HSQBS]') AND type in (N'U'))
	drop table TBL_TCOD_HSQBS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCOD_VCQP]') AND type in (N'U'))
	drop table TBL_TCOD_VCQP;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCOD_LDHD]') AND type in (N'U'))
	drop table TBL_TCOD_LDHD;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCOD_RESULT]') AND type in (N'U'))
	drop table TBL_TCOD_RESULT;

END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_thai_san]    Script Date: 11/29/2023 10:30:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_thai_san]
	@DsMaDonVi nvarchar(100),
	@NamLamViec int,
	@Thang int,
	@DonViTinh int
AS
BEGIN
	--Lay thong tin luong theo tro cap thai san
	select * into TBL_TCTS from
	(select luongcancu.fLuongCanCu, chedo.fSoNgayHuongBHXH, donvi.Ten_DonVi, luong.*
	from TL_BangLuong_ThangBHXH luong
	join TL_DM_DonVi donvi on luong.sMaDonVi = donvi.Ma_DonVi
	join TL_CanBo_CheDoBHXH chedo on luong.sMaCBo = chedo.sMaCanBo and luong.sMaCheDo = chedo.sMaCheDo
	left join 
	(select Ma_CBo, NAM, thang, Gia_Tri fLuongCanCu from TL_BangLuong_Thang
		where NAM = @NamLamViec
		and thang = @Thang
		and Ma_PhuCap = 'LHT_TT') luongcancu on luong.sMaCBo = luongcancu.Ma_CBo
	where 
	luong.sMaDonVi in (SELECT * FROM f_split(@DsMaDonVi))
	and luong.iNam = @NamLamViec
	and luong.iThang = @Thang
	and luong.sMaCheDo in ('SINHCON_NUOICON', 'THAISAN_TROCAP1LAN', 'KHAMTHAI', 'KHHGĐ', 'NAMNGHIKHIVOSINHCON', 'THAISAN_DUONGSUCPHSK')) tcts

	select
		TBL_TCTS.sMaCB,
		TBL_TCTS.sMaCBo,
		--TBL_TCTS.sMaCheDo,
		TBL_TCTS.sTenCbo,
		TBL_TCTS.Ten_DonVi,
		SINHCONNUOICON.SoNgaySINHCONNUOICON SoNgaySINHCON_NUOICON,
		TROCAP1LAN.SoNgayTROCAP1LAN,
		KHAMTHAI.SoNgayKHAMTHAI,
		DUONGSUCPHSK.SoNgayDUONGSUCPHSK,
		TBL_TCTS.fLuongCanCu,
		SINHCONNUOICON.nGiaTri fSINHCON_NUOICON,
		TROCAP1LAN.nGiaTri fTROCAP1LAN,
		KHAMTHAI.nGiaTri fKHAMTHAI,
		DUONGSUCPHSK.nGiaTri fDUONGSUCPHSK
		into TBL_TCTS_DOC
	from TBL_TCTS TBL_TCTS
		left join
		(select tcts.Id, tcts.sMaDonVi, tcts.nGiaTri, tcts.sMaCB, tcts.sMaCBo, tcts.sMaCheDo, tcts.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTROCAP1LAN
		from TBL_TCTS tcts left join TL_CanBo_CheDoBHXH chedo on tcts.sMaCBo = chedo.sMaCanBo and tcts.sMaCheDo = chedo.sMaCheDo
		where tcts.sMaCheDo = 'THAISAN_TROCAP1LAN') TROCAP1LAN
		on TBL_TCTS.sMaCBo = TROCAP1LAN.sMaCBo and TBL_TCTS.sMaDonVi = TROCAP1LAN.sMaDonVi
		left join
		(select tcts_1.Id, tcts_1.sMaDonVi, sum(tcts_1.nGiaTri) nGiaTri, tcts_1.sMaCB, tcts_1.sMaCBo, tcts_1.sMaCheDo, tcts_1.sTenCbo, sum(chedo.fSoNgayHuongBHXH) SoNgayKHAMTHAI
		from TBL_TCTS tcts_1 left join TL_CanBo_CheDoBHXH chedo on tcts_1.sMaCBo = chedo.sMaCanBo and tcts_1.sMaCheDo = chedo.sMaCheDo
		where tcts_1.sMaCheDo in ('KHAMTHAI', 'KHHGĐ', 'NAMNGHIKHIVOSINHCON')
		group by tcts_1.Id, tcts_1.sMaDonVi, tcts_1.sMaCB, tcts_1.sMaCBo, tcts_1.sMaCheDo, tcts_1.sTenCbo) KHAMTHAI
		on TBL_TCTS.sMaCBo = KHAMTHAI.sMaCBo and TBL_TCTS.sMaDonVi = KHAMTHAI.sMaDonVi
		left join
		(select tcts_2.Id, tcts_2.sMaDonVi, tcts_2.nGiaTri, tcts_2.sMaCB, tcts_2.sMaCBo, tcts_2.sMaCheDo, tcts_2.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayDUONGSUCPHSK
		from TBL_TCTS tcts_2 left join TL_CanBo_CheDoBHXH chedo on tcts_2.sMaCBo = chedo.sMaCanBo and tcts_2.sMaCheDo = chedo.sMaCheDo
		where tcts_2.sMaCheDo = 'THAISAN_DUONGSUCPHSK') DUONGSUCPHSK
		on TBL_TCTS.sMaCBo = DUONGSUCPHSK.sMaCBo and TBL_TCTS.sMaDonVi = DUONGSUCPHSK.sMaDonVi
		left join
		(select tcts_3.Id, tcts_3.sMaDonVi, tcts_3.nGiaTri, tcts_3.sMaCB, tcts_3.sMaCBo, tcts_3.sMaCheDo, tcts_3.sTenCbo, chedo.fSoNgayHuongBHXH SoNgaySINHCONNUOICON
		from TBL_TCTS tcts_3 left join TL_CanBo_CheDoBHXH chedo on tcts_3.sMaCBo = chedo.sMaCanBo and tcts_3.sMaCheDo = chedo.sMaCheDo
		where tcts_3.sMaCheDo = 'SINHCON_NUOICON') SINHCONNUOICON
		on TBL_TCTS.sMaCBo = DUONGSUCPHSK.sMaCBo and TBL_TCTS.sMaDonVi = SINHCONNUOICON.sMaDonVi

	--Lấy lương Sĩ quan
	select * into TBL_TCTS_SQ from
	(select
		 1 bHangCha, 1 RowNum, 'I' STT, 'SQ' DoiTuong, 'SQ' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'SQ' sTenCbo, null SoNgaySINHCON_NUOICON, null SoNgayTROCAP1LAN, null SoNgayKHAMTHAI, null SoNgayDUONGSUCPHSK, null fLuongCanCu, null fSINHCON_NUOICON, null fTROCAP1LAN, null fKHAMTHAI, null fDUONGSUCPHSK, 0 bHasData
	union all
	select
		0 bHangCha, 1 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'SQ' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgaySINHCON_NUOICON, SoNgayTROCAP1LAN, SoNgayKHAMTHAI, SoNgayDUONGSUCPHSK, fLuongCanCu, fSINHCON_NUOICON, fTROCAP1LAN, fKHAMTHAI, fDUONGSUCPHSK, 0 bHasData
	from TBL_TCTS_DOC
	where sMaCB like '1%' and (isnull(fLuongCanCu, 0) <> 0 or isnull(fSINHCON_NUOICON, 0) <> 0 or isnull(fTROCAP1LAN, 0) <> 0 or isnull(fKHAMTHAI, 0) <> 0 or isnull(fDUONGSUCPHSK, 0) <> 0)) sq
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCTS_SQ) > 1
		update TBL_TCTS_SQ set bHasData = 1

	--Lấy lương QNCN
	select * into TBL_TCTS_QNCN from
	(select
		1 bHangCha, 2 RowNum, 'II' STT, 'QNCN' DoiTuong, 'QNCN' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'QNCN' sTenCbo, null SoNgaySINHCON_NUOICON, null SoNgayTROCAP1LAN, null SoNgayKHAMTHAI, null SoNgayDUONGSUCPHSK, null fLuongCanCu, null fSINHCON_NUOICON, null fTROCAP1LAN, null fKHAMTHAI, null fDUONGSUCPHSK, 0 bHasData
	union all
	select
		0 bHangCha, 2 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'QNCN' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgaySINHCON_NUOICON, SoNgayTROCAP1LAN, SoNgayKHAMTHAI, SoNgayDUONGSUCPHSK, fLuongCanCu, fSINHCON_NUOICON, fTROCAP1LAN, fKHAMTHAI, fDUONGSUCPHSK, 0 bHasData
	from TBL_TCTS_DOC
	where sMaCB like '2%' and (isnull(fLuongCanCu, 0) <> 0 or isnull(fSINHCON_NUOICON, 0) <> 0 or isnull(fTROCAP1LAN, 0) <> 0 or isnull(fKHAMTHAI, 0) <> 0 or isnull(fDUONGSUCPHSK, 0) <> 0)) qncn
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCTS_QNCN) > 1
		update TBL_TCTS_QNCN set bHasData = 1

	--Lấy lương HSQ_BS
	select * into TBL_TCTS_HSQBS from
	(select
		1 bHangCha, 3 RowNum, 'III' STT, 'HSQ_BS' DoiTuong, 'HSQ_BS' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'HSQ_BS' sTenCbo, null SoNgaySINHCON_NUOICON, null SoNgayTROCAP1LAN, null SoNgayKHAMTHAI, null SoNgayDUONGSUCPHSK, null fLuongCanCu, null fSINHCON_NUOICON, null fTROCAP1LAN, null fKHAMTHAI, null fDUONGSUCPHSK, 0 bHasData
	union all
	select
		0 bHangCha, 3 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'HSQ_BS' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgaySINHCON_NUOICON, SoNgayTROCAP1LAN, SoNgayKHAMTHAI, SoNgayDUONGSUCPHSK, fLuongCanCu, fSINHCON_NUOICON, fTROCAP1LAN, fKHAMTHAI, fDUONGSUCPHSK, 0 bHasData
	from TBL_TCTS_DOC
	where sMaCB like '0%' and (isnull(fLuongCanCu, 0) <> 0 or isnull(fSINHCON_NUOICON, 0) <> 0 or isnull(fTROCAP1LAN, 0) <> 0 or isnull(fKHAMTHAI, 0) <> 0 or isnull(fDUONGSUCPHSK, 0) <> 0)) hsqbs
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCTS_HSQBS) > 1
		update TBL_TCTS_HSQBS set bHasData = 1

	--Lấy lương VCQP
	select * into TBL_TCTS_VCQP from
	(select
		1 bHangCha, 4 RowNum, 'IV' STT, 'VCQP' DoiTuong, 'VCQP' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'VCQP' sTenCbo, null SoNgaySINHCON_NUOICON, null SoNgayTROCAP1LAN, null SoNgayKHAMTHAI, null SoNgayDUONGSUCPHSK, null fLuongCanCu, null fSINHCON_NUOICON, null fTROCAP1LAN, null fKHAMTHAI, null fDUONGSUCPHSK, 0 bHasData
	union all
	select
		0 bHangCha, 4 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'VCQP' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgaySINHCON_NUOICON, SoNgayTROCAP1LAN, SoNgayKHAMTHAI, SoNgayDUONGSUCPHSK, fLuongCanCu, fSINHCON_NUOICON, fTROCAP1LAN, fKHAMTHAI, fDUONGSUCPHSK, 0 bHasData
	from TBL_TCTS_DOC
	where sMaCB in ('3.1', '3.2', '3.3') and (isnull(fLuongCanCu, 0) <> 0 or isnull(fSINHCON_NUOICON, 0) <> 0 or isnull(fTROCAP1LAN, 0) <> 0 or isnull(fKHAMTHAI, 0) <> 0 or isnull(fDUONGSUCPHSK, 0) <> 0)) vcqp
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCTS_VCQP) > 1
		update TBL_TCTS_VCQP set bHasData = 1

	--Lấy lương Lao động hợp đông
	select * into TBL_TCTS_LDHD from
	(select
		1 bHangCha, 5 RowNum, 'V' STT, 'LDHD' DoiTuong, 'LDHD' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'LDHD' sTenCbo, null SoNgaySINHCON_NUOICON, null SoNgayTROCAP1LAN, null SoNgayKHAMTHAI, null SoNgayDUONGSUCPHSK, null fLuongCanCu, null fSINHCON_NUOICON, null fTROCAP1LAN, null fKHAMTHAI, null fDUONGSUCPHSK, 0 bHasData
	union all
	select
		0 bHangCha, 5 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'LDHD' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgaySINHCON_NUOICON, SoNgayTROCAP1LAN, SoNgayKHAMTHAI, SoNgayDUONGSUCPHSK, fLuongCanCu, fSINHCON_NUOICON, fTROCAP1LAN, fKHAMTHAI, fDUONGSUCPHSK, 0 bHasData
	from TBL_TCTS_DOC
	where sMaCB = '43' and (isnull(fLuongCanCu, 0) <> 0 or isnull(fSINHCON_NUOICON, 0) <> 0 or isnull(fTROCAP1LAN, 0) <> 0 or isnull(fKHAMTHAI, 0) <> 0 or isnull(fDUONGSUCPHSK, 0) <> 0)) ldhd
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCTS_LDHD) > 1
		update TBL_TCTS_LDHD set bHasData = 1

	--Ket qua
	select result.* into TBL_TCTS_RESULT from
	(select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgaySINHCON_NUOICON, SoNgayTROCAP1LAN, SoNgayKHAMTHAI, SoNgayDUONGSUCPHSK, fLuongCanCu, fSINHCON_NUOICON, fTROCAP1LAN, fKHAMTHAI, fDUONGSUCPHSK,
	(isnull(fSINHCON_NUOICON, 0) + isnull(fTROCAP1LAN, 0) + isnull(fKHAMTHAI, 0) + isnull(fDUONGSUCPHSK, 0)) fTongSoTien
	from TBL_TCTS_SQ
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgaySINHCON_NUOICON, SoNgayTROCAP1LAN, SoNgayKHAMTHAI, SoNgayDUONGSUCPHSK, fLuongCanCu, fSINHCON_NUOICON, fTROCAP1LAN, fKHAMTHAI, fDUONGSUCPHSK,
	(isnull(fSINHCON_NUOICON, 0) + isnull(fTROCAP1LAN, 0) + isnull(fKHAMTHAI, 0) + isnull(fDUONGSUCPHSK, 0)) fTongSoTien
	from TBL_TCTS_QNCN
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgaySINHCON_NUOICON, SoNgayTROCAP1LAN, SoNgayKHAMTHAI, SoNgayDUONGSUCPHSK, fLuongCanCu, fSINHCON_NUOICON, fTROCAP1LAN, fKHAMTHAI, fDUONGSUCPHSK,
	(isnull(fSINHCON_NUOICON, 0) + isnull(fTROCAP1LAN, 0) + isnull(fKHAMTHAI, 0) + isnull(fDUONGSUCPHSK, 0)) fTongSoTien
	from TBL_TCTS_HSQBS
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgaySINHCON_NUOICON, SoNgayTROCAP1LAN, SoNgayKHAMTHAI, SoNgayDUONGSUCPHSK, fLuongCanCu, fSINHCON_NUOICON, fTROCAP1LAN, fKHAMTHAI, fDUONGSUCPHSK,
	(isnull(fSINHCON_NUOICON, 0) + isnull(fTROCAP1LAN, 0) + isnull(fKHAMTHAI, 0) + isnull(fDUONGSUCPHSK, 0)) fTongSoTien
	from TBL_TCTS_VCQP
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgaySINHCON_NUOICON, SoNgayTROCAP1LAN, SoNgayKHAMTHAI, SoNgayDUONGSUCPHSK, fLuongCanCu, fSINHCON_NUOICON, fTROCAP1LAN, fKHAMTHAI, fDUONGSUCPHSK,
	(isnull(fSINHCON_NUOICON, 0) + isnull(fTROCAP1LAN, 0) + isnull(fKHAMTHAI, 0) + isnull(fDUONGSUCPHSK, 0)) fTongSoTien
	from TBL_TCTS_LDHD) result

	select
		RowNum,
		STT,
		DoiTuong, 
		LoaiDoiTuong,
		sMaCB MaCb, 
		sMaCBo MaCbo, 
		sTenCbo TenCbo,
		TenDonVi,
		SoNgaySINHCON_NUOICON SoNgaySinhConNuoiCon,
		SoNgayTROCAP1LAN SoNgayTroCap1Lan,
		SoNgayKHAMTHAI SoNgayKhamThai,
		SoNgayDUONGSUCPHSK SoNgayDuongSucPHSKThaiSan,
		fLuongCanCu FLuongCanCu,
		fSINHCON_NUOICON/@DonViTinh fSinhConNuoiCon,
		fTROCAP1LAN/@DonViTinh fTroCap1Lan,
		fKHAMTHAI/@DonViTinh fKhamThai,
		fDUONGSUCPHSK/@DonViTinh fDuongSucPHSKThaiSan,
		fTongSoTien/@DonViTinh FTongSoTien,
		bHangCha IsHangCha,
		bHasData IsHasData
	from TBL_TCTS_RESULT
	order by RowNum, LoaiDoiTuong, DoiTuong desc

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTS]') AND type in (N'U'))
	drop table TBL_TCTS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTS_DOC]') AND type in (N'U'))
	drop table TBL_TCTS_DOC;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTS_SQ]') AND type in (N'U'))
	drop table TBL_TCTS_SQ;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTS_QNCN]') AND type in (N'U'))
	drop table TBL_TCTS_QNCN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTS_HSQBS]') AND type in (N'U'))
	drop table TBL_TCTS_HSQBS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTS_VCQP]') AND type in (N'U'))
	drop table TBL_TCTS_VCQP;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTS_LDHD]') AND type in (N'U'))
	drop table TBL_TCTS_LDHD;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTS_RESULT]') AND type in (N'U'))
	drop table TBL_TCTS_RESULT;

END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bhxh_export_can_bo_che_do]    Script Date: 11/29/2023 10:30:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_tl_bhxh_export_can_bo_che_do]
AS
BEGIN
	select distinct
		chedo.sXauNoiMaMlnsBHXH,
		canbo.Ma_Hieu_Canbo sMaHieuCanBo,
		canbo.Ten_CanBo sTenCanBo,
		canbo.Ten_DonVi sTenDonVi,
		canbo.Parent sMaDonVi,
		cbcd.sMaCanBo,
		cb.Ma_Cb SMaCapBac,
		cb.Ten_Cb STenCapBac,
		cbcd.sMaCheDo sMaCheDo,
		cbcd.fSoNgayHuongBHXH,
		cbcd.sSoQuyetDinh,
		cbcd.dNgayQuyetDinh,
		cbcd.iThangLuongCanCuDong,
		isnull(cbcd.fSoTien, 0) fSoTien,
		isnull(cbcd.fGiaTriCanCu, 0) fGiaTriCanCu
	from TL_CanBo_CheDoBHXH cbcd
		left join TL_DM_CanBo canbo on cbcd.sMaCanBo = canbo.Ma_CanBo
		join (
			select canbo.Ma_CanBo,
				capbac.Ma_Cb,
				capbac.Note Ten_Cb
			from TL_DM_CanBo canbo
			join TL_DM_CapBac capbac
			on canbo.Ma_CB = capbac.Ma_Cb
		) cb on cbcd.sMaCanBo = cb.Ma_CanBo
		left join TL_DM_CheDoBHXH chedo
			on cbcd.sMaCheDo = chedo.sMaCheDo
	order by canbo.Ma_Hieu_Canbo desc

END
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_export_bang_luong_thang_bhxh]    Script Date: 11/29/2023 10:30:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_export_bang_luong_thang_bhxh]
	@YearOfWork int,
	@Months nvarchar(20)
AS
BEGIN
	SELECT
			--bangLuongThang.iThang Thang,
			bangLuongThang.iNam Nam,
			--bangLuongThang.sMaCBo MaCbo,
			bangLuongThang.sMaHieuCanBo MaHieuCanBo,
			bangLuongThang.sTenCbo TenCbo,
			bangLuongThang.sMaCB MaCb,
			capbac.Note TenCapBac,
			donvi.Ten_DonVi TenDonVi,
			isnull(sum(bangLuongThang.nGiaTri), 0) GiaTri,
			bangLuongThang.sMaCheDo	MaCheDo
		FROM TL_BangLuong_ThangBHXH bangLuongThang
		LEFT JOIN TL_DM_DonVi donvi
			ON bangLuongThang.sMaDonVi = donvi.ma_donvi
		LEFT JOIN TL_DM_CapBac capbac
			ON bangLuongThang.sMaCB = capbac.Ma_Cb
		WHERE bangLuongThang.iNam = @YearOfWork
			AND bangLuongThang.iThang in (SELECT * FROM f_split(@Months))
		GROUP BY
			--bangLuongThang.iThang,
			bangLuongThang.iNam,
			--bangLuongThang.sMaCBo,
			bangLuongThang.sMaHieuCanBo,
			bangLuongThang.sTenCbo,
			bangLuongThang.sMaCB,
			capbac.Note,
			donvi.Ten_DonVi,
			bangLuongThang.sMaCheDo
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_export_danh_muc_che_do_bhxh]    Script Date: 11/29/2023 10:30:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_tl_export_danh_muc_che_do_bhxh]
AS
BEGIN
	select
		sMaCheDo,
		sTenCheDo,
		case
			when iLoaiCheDo = 1 then N'Trợ cấp ốm đau'
			when iLoaiCheDo = 2 then N'Trợ cấp thai sản'
			when iLoaiCheDo = 3 then N'Trợ cấp tai nạn LĐ'
			when iLoaiCheDo = 4 then N'Trợ cấp hưu trí, phục viên, thôi việc, tử tuất'
			when iLoaiCheDo = 5 then N'Trợ cấp xuất ngũ'
			else N'Hệ số'
		end as sLoaiCheDo,
		case
			when bTinhTheoCongThuc = 1 then N'Có'
			else N'Không'
		end as sTinhTheoCongThuc,
		sMaCheDoCha,
		fGiaTri,
		sXauNoiMaMlnsBHXH,
		sMlnsBHXH
	from TL_DM_CheDoBHXH
	order by 
		iLoaiCheDo,
		sXauNoiMa
END
GO



DELETE FROM [dbo].[BH_DM_MucLucNganSach]
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'f499ad99-67f6-4327-a319-02e9a8c799dd', N'9020002-010-011-0001', N'9020002', N'010', N'011', N'0001', N'', N'', N'', N'', N'A. Quân nhân', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:33.020' AS DateTime), NULL, 0, NULL, NULL, NULL, N'8639c115-bdb3-49d5-9018-a1c9a3d24896', N'72d7a943-3288-4175-be6f-cf5190d2b908', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'3fd501eb-1ce3-4fe5-9d23-03ae086f5638', N'9010001-010-011-0001-0003', N'9010001', N'010', N'011', N'0001', N'0003', N'', N'', N'', N'- Dưỡng sức, phục hồi SK(ngày)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:28.773' AS DateTime), NULL, 0, NULL, NULL, NULL, N'280b18fa-ca4b-4e46-bb55-1bd81e962e7c', N'63e1e32a-460b-4696-aa73-441934842ac0', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'95dca35b-d468-4f09-b4fc-04048cd9be6c', N'9010003-010-011-7000-7049', N'9010003', N'010', N'011', N'7000', N'7049', N'', N'', N'', N'Chi phí khác ', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:24.213' AS DateTime), NULL, 0, NULL, NULL, NULL, N'a6aa1af6-475c-4853-836f-c0bde339c5b8', N'06c2e5e2-42f0-4ff6-a18c-bf19f1ed707d', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'75ccc890-a8cd-46f5-98c5-04cf5b25c78e', N'9050001', N'9050001', N'', N'', N'', N'', N'', N'', N'', N'Kinh phí chăm sóc sức khỏe ban đầu người lao động ', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:39.073' AS DateTime), NULL, 0, NULL, NULL, NULL, N'a519c162-3f2e-47ef-9cdf-2da72cfa40b7', N'2cdf8f93-5d04-45f8-afcc-5100068321e4', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'6c5c42a0-7d68-411b-8482-06cf68797f96', N'9010002-010-011-0001-0001-0001-01', N'9010002', N'010', N'011', N'0001', N'0001', N'0001', N'01', N'', N'+ Thuộc DM bệnh cần chữa trị dài ngày', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:37.650' AS DateTime), NULL, 0, NULL, NULL, NULL, N'9ebe3f5e-f6e8-4268-b744-583b24731221', N'06a74e94-f3ba-4d73-8f80-9ae9563e1000', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'4ed3d39e-8132-46d6-8873-07289eba773a', N'9010002-010-011-0008', N'9010002', N'010', N'011', N'0008', N'', N'', N'', N'', N'8. Tử tuất', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:48.327' AS DateTime), NULL, 0, NULL, NULL, NULL, N'e56cedb7-ed03-485b-beab-c152f42ebadd', N'7a097777-89ac-4d76-a856-d1534c4070a9', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'55332716-f918-4286-a729-082e303aca8c', N'9010006-010-011-0010', N'9010006', N'010', N'011', N'0010', N'', N'', N'', N'', N'1. Kinh phí KCB tại Trường Sa - DK ', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:28.160' AS DateTime), NULL, 0, NULL, NULL, NULL, N'8088e295-2f9c-45b3-a799-d88940922713', N'78861c3d-d2be-4c22-8a9a-2ccc7a63502a', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'77b93582-0809-4119-8af1-08b4c4f86734', N'9010001-010-011-0003-0001-0007-00', N'9010001', N'010', N'011', N'0003', N'0001', N'0007', N'00', N'', N'- Chi hỗ Trợ phòng người (người)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:33.370' AS DateTime), NULL, 0, NULL, NULL, NULL, N'8371001c-2996-42e3-8dcf-7a5ab4a0914c', N'c7647e43-8aca-4882-98f4-3ec7e0bc8714', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'80d58768-ed29-48b7-b3d0-08da91d87ca6', N'9010006-010-011-0010-0001', N'9010006', N'012', N'011', N'0010', N'0001', N'', N'', N'', N'- Vật tư y tế(bông băng, bơm, kim tiêm)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:28.540' AS DateTime), NULL, 0, NULL, NULL, NULL, N'78660144-1b68-4aff-a71c-65b41ae48bbe', N'8088e295-2f9c-45b3-a799-d88940922713', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'2ba83b5a-7a42-4035-98ff-0aa1ea67e9f6', N'9010002-010-011-0005-0001-0001-00', N'9010002', N'010', N'011', N'0005', N'0001', N'0001', N'00', N'', N'- Trợ cấp 1 lần (người)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:46.340' AS DateTime), NULL, 0, NULL, NULL, NULL, N'da87b860-e7a5-4a0a-a1ae-d03ea1b81de9', N'1ebce22c-8a98-40cf-bd20-d85a59e9c7bc', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'4608e7e3-8c98-421e-acf9-0b0d1be7c302', N'9010001-010-011-0002-0001-0003-00-02', N'9010001', N'010', N'011', N'0002', N'0001', N'0003', N'00', N'02', N'+ Lao động nam', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:31.017' AS DateTime), NULL, 0, NULL, NULL, NULL, N'af4633bb-d405-401b-b7c9-c6108917fc89', N'd27fbd1b-73ad-48a0-bcba-111b296a6b1c', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'74739e5f-afe0-4ddc-ac66-0b38a089dc86', N'9030002', N'9030002', N'', N'', N'', N'', N'', N'', N'', N'II. Bảo hiểm y tế thân nhân CN, VCQP(CY khác)', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:36.940' AS DateTime), NULL, 0, NULL, NULL, NULL, N'9ad55244-03c4-4c97-ba26-27af54495842', N'5a1234af-171a-44c9-98ed-9da7deff0188', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'200decee-2e5c-423c-9269-0b67e2560b21', N'9010001-010-011-0007', N'9010001', N'010', N'011', N'0007', N'', N'', N'', N'', N'7. Trợ cấp thôi việc', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:35.660' AS DateTime), NULL, 0, NULL, NULL, NULL, N'58a28603-1d42-40be-bbb3-56f7c15d69ef', N'91465483-df1b-4262-9436-d87f8808cfac', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'4c3eebaa-de28-4d8b-b15f-0c9a89a784c7', N'9010003-010-011-6600-6603', N'9010003', N'010', N'011', N'6600', N'6603', N'', N'', N'', N'Cưước phí bưu chính', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:50.620' AS DateTime), NULL, 0, NULL, NULL, NULL, N'd57050f2-75a6-43e1-b637-cd3a4fe8b4c6', N'4ec9c459-3a9b-4106-a0bf-f0550e0ec73f', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'ccca13ec-f406-4bd5-b94a-0d82da2d07b1', N'9050002', N'9050002', N'', N'', N'', N'', N'', N'', N'', N'Kinh phí chăm sóc sức khỏe ban đầu học sinh sinh viên ', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:39.300' AS DateTime), NULL, 0, NULL, NULL, NULL, N'cf3d919f-997c-4f35-abed-40d1ef5f1f51', N'2cdf8f93-5d04-45f8-afcc-5100068321e4', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'aab46e95-0eff-4641-a614-0f78db14a55f', N'9010002-010-011-0002-0001-0001-00', N'9010002', N'010', N'011', N'0002', N'0001', N'0001', N'00', N'', N'- Sinh Con, nuôi Con nuôi (tháng)', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:40.143' AS DateTime), NULL, 0, NULL, NULL, NULL, N'429c47a4-26ac-4bd3-97f1-7a89b917e6a1', N'6f830377-5b2f-44d8-9a64-9d46c4270a80', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'0e2603c4-d8aa-4245-91f8-11737f095165', N'9010002-010-011-0003-0001-0009-00', N'9010002', N'010', N'011', N'0003', N'0001', N'0009', N'00', N'', N'- DS, PHSK sau TNLĐ, BNN (người)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:44.833' AS DateTime), NULL, 0, NULL, NULL, NULL, N'8aec0461-a021-47fd-845f-c1512a792cb7', N'2d5d9fc0-6e68-4f70-834c-3ef277099bf6', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'0b8aa09d-53a9-45ed-b0cb-12a709311976', N'9010002-010-011-0002-0001-0003-00-02', N'9010002', N'010', N'011', N'0002', N'0001', N'0003', N'00', N'02', N'+ Lao động nam', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:42.340' AS DateTime), NULL, 0, NULL, NULL, NULL, N'01eb6c66-54b8-4f62-9b59-b0ea749cb180', N'cf731c26-af8a-4708-82de-c03bd8b38715', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'1605b959-9c1b-48b4-9c9b-154262f0f85b', N'9010001-010-011-0003-0001-0003-00', N'9010001', N'010', N'011', N'0003', N'0001', N'0003', N'00', N'', N'- Trợ cấp hàng tháng (người)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:32.567' AS DateTime), NULL, 0, NULL, NULL, NULL, N'4c020fb4-019c-4296-ba41-f4bf9488a33e', N'c7647e43-8aca-4882-98f4-3ec7e0bc8714', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'85c30c67-97d9-45cb-8ee9-1767792463a1', N'9010001-010-011-0001-0001-0001-02', N'9010001', N'010', N'011', N'0001', N'0001', N'0001', N'02', N'', N'+ ốm khác', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:27.740' AS DateTime), NULL, 0, NULL, NULL, NULL, N'4ecb27b3-f09b-4972-9d22-965886faab0a', N'a655d431-de68-4238-b921-55850d8bba6b', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'c5b0a724-b295-4649-82e9-181151faec82', N'9010006-010-011-0010-0003', N'9010006', N'014', N'011', N'0010', N'0003', N'', N'', N'', N'- Dụng cụ ý tế', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:28.967' AS DateTime), NULL, 0, NULL, NULL, NULL, N'35d2bc4b-f59b-4f9b-a96f-bf30529d48f7', N'8088e295-2f9c-45b3-a799-d88940922713', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'970d86b5-25d2-41ff-9af0-187c14f04ea1', N'9010002-010-011-0001-0001-0001-01-02', N'9010002', N'010', N'011', N'0001', N'0001', N'0001', N'01', N'02', N'* Từ 14 ngày trở lên/ tháng(ngày)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:38.153' AS DateTime), NULL, 0, NULL, NULL, NULL, N'1bf2509b-91d5-4974-aff0-7f7c426cddc4', N'9ebe3f5e-f6e8-4268-b744-583b24731221', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'06621bde-dd78-4ae5-a840-1b46651b0579', N'9010003-010-011-6550', N'9010003', N'010', N'011', N'6550', N'', N'', N'', N'', N'Vật tư văn phòng', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:49.157' AS DateTime), NULL, 0, NULL, NULL, NULL, N'2e93d30f-796f-4f33-938b-7d2dd2d38c3a', N'885f40fa-4c4d-4ef2-ab31-b075853b028f', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'9ed1cda9-f69f-4fca-ad23-1c5f29a8249e', N'9010001-010-011-0006', N'9010001', N'010', N'011', N'0006', N'', N'', N'', N'', N'6. Trợ cấp Xuất ngũ', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:35.230' AS DateTime), NULL, 0, NULL, NULL, NULL, N'a0d4eadb-0deb-45a6-b3fc-2f00cebd9613', N'91465483-df1b-4262-9436-d87f8808cfac', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'f8039f82-e54f-43e1-b1ee-1edd4864d817', N'9020001-010-011-0001', N'9020001', N'010', N'011', N'0001', N'', N'', N'', N'', N'A. Quân nhân', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:30.963' AS DateTime), NULL, 0, NULL, NULL, NULL, N'8de3ff2f-c2af-4608-afde-cc7e82d173a9', N'eb1f7599-1ee7-4fef-be5e-e1ad25602a66', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'38bca6e0-db43-426d-95e4-1f0e50ae3f8d', N'9020001-010-011-0001-0002', N'9020001', N'013', N'011', N'0001', N'0002', N'', N'', N'', N'3. HSQ, BS', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:31.870' AS DateTime), NULL, 0, NULL, NULL, NULL, N'9394f8c1-479d-438a-813a-22c105b5e731', N'8de3ff2f-c2af-4608-afde-cc7e82d173a9', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, N'3', N'LHT_TT,PCCV_TT,PCTN_TT,PCTNVK_TT')
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'bab737a4-0386-4fba-b34a-1fc99c19c091', N'9010002-010-011-0001-0001-0001-01-02', N'9010002', N'010', N'011', N'0001', N'0001', N'0001', N'01', N'01', N'* Dưới 14 ngày/tháng(ngày)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:37.923' AS DateTime), NULL, 0, NULL, NULL, NULL, N'bc6c9002-f7cc-47ad-8fe1-f80cab19ea01', N'9ebe3f5e-f6e8-4268-b744-583b24731221', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'b0bfafcc-5a69-45c1-a329-20c5987cb2af', N'9010001-010-011-0007-0001-0002-00', N'9010001', N'010', N'011', N'0007', N'0001', N'0002', N'00', N'', N'- Trợ cấp KV 1 lần (người)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:36.067' AS DateTime), NULL, 0, NULL, NULL, NULL, N'f10c833a-c6a4-48d9-82ee-51682f8abe0b', N'58a28603-1d42-40be-bbb3-56f7c15d69ef', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'06433883-c3be-4685-8169-22a8f12e91c0', N'9010003-010-011-6650', N'9010003', N'010', N'011', N'6650', N'', N'', N'', N'', N'Hội nghị', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:52.000' AS DateTime), NULL, 0, NULL, NULL, NULL, N'13a6bbc8-d2ba-4c81-9d29-a6c42bbd2a2b', N'885f40fa-4c4d-4ef2-ab31-b075853b028f', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'7d4fdd6d-437c-44ab-a47c-23bcc19c2f31', N'9010001-010-011-0005', N'9010001', N'010', N'011', N'0005', N'', N'', N'', N'', N'5. Trợ cấp Phục viên', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:34.693' AS DateTime), NULL, 0, NULL, NULL, NULL, N'1da67c9f-2025-4bd0-9d24-20fa64651658', N'91465483-df1b-4262-9436-d87f8808cfac', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'728bf8aa-3bd1-42db-8e91-26c311af8d0d', N'9010005-010-011-0010-0001', N'9010005', N'012', N'011', N'0010', N'0001', N'', N'', N'', N'- Vật tư y tế(bông băng, bơm, kim tiêm)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:27.350' AS DateTime), NULL, 0, NULL, NULL, NULL, N'600f74a3-06c7-4c4e-8859-69a8ab9d212c', N'29147692-3954-4a2b-af22-bcbdb4dbc4ba', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'0a909f34-8372-43c3-86df-28f667caf5cc', N'9010001-010-011-0007-0001-0001-00', N'9010001', N'010', N'011', N'0007', N'0001', N'0001', N'00', N'', N'- Trợ cấp 1 lần (người)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:35.883' AS DateTime), NULL, 0, NULL, NULL, NULL, N'fcc27636-fe76-4224-bb5c-0743a91b7e6f', N'58a28603-1d42-40be-bbb3-56f7c15d69ef', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'e6cdcd56-abdc-4f89-96f6-2c044da4e48d', N'9020002-010-011-0001-0002', N'9020002', N'013', N'011', N'0001', N'0002', N'', N'', N'', N'3. HSQ, BS', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:33.877' AS DateTime), NULL, 0, NULL, NULL, NULL, N'ceec043e-4468-4128-9b09-18cf4b53b025', N'8639c115-bdb3-49d5-9018-a1c9a3d24896', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'117833dd-7c62-4dec-8525-2c92072dc757', N'9010003-010-011-6750-6799', N'9010003', N'010', N'011', N'6750', N'6799', N'', N'', N'', N'Chi phí thuê mướn khác', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:21.990' AS DateTime), NULL, 0, NULL, NULL, NULL, N'5b7a1ead-90a1-42c1-91f3-ef688f45f268', N'8fc998eb-21f2-4c1c-9711-1e010b7da6ba', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'44decfe5-2aa7-44d4-9960-2d6b4703ed0b', N'9010005-010-011-0010-0002', N'9010005', N'013', N'011', N'0010', N'0002', N'', N'', N'', N'- Dịch vụ kỹ thuật', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:27.587' AS DateTime), NULL, 0, NULL, NULL, NULL, N'863a6a36-ed30-4f8f-b0f6-757aab07aee5', N'29147692-3954-4a2b-af22-bcbdb4dbc4ba', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'64e1afc7-c13e-4117-bfa0-3058c9a664a8', N'9010003-010-011-7750-7756', N'9010003', N'010', N'011', N'7750', N'7756', N'', N'', N'', N'Chi các khoản phí và lệ phí ', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:25.540' AS DateTime), NULL, 0, NULL, NULL, NULL, N'bc5851db-3788-461b-b364-acc87fa0cca0', N'c60199d0-3566-4c28-91d5-cbde4cd4f792', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'4561a0b3-df37-4025-8c80-306cd8336186', N'9010002-010-011-0002-0001-0002-00-02', N'9010002', N'010', N'011', N'0002', N'0001', N'0002', N'00', N'02', N'+ Lao động nam', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:41.610' AS DateTime), NULL, 0, NULL, NULL, NULL, N'522cfce4-4187-45a1-b5d2-3bc01146f2a5', N'9d0f9113-0bbc-4b8d-8e9b-60e41686b5ff', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'82db689d-abc6-44ca-bfde-32a7c8d9b23e', N'9010003-010-011-6750', N'9010003', N'010', N'011', N'6750', N'', N'', N'', N'', N'Chi phí thuê mướn ', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:21.703' AS DateTime), NULL, 0, NULL, NULL, NULL, N'8fc998eb-21f2-4c1c-9711-1e010b7da6ba', N'885f40fa-4c4d-4ef2-ab31-b075853b028f', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'09f9bf38-5217-4600-bc43-35482fc09359', N'9010001-010-011-0002-0001-0001-00', N'9010001', N'010', N'011', N'0002', N'0001', N'0001', N'00', N'', N'- Sinh Con, nuôi Con nuôi (tháng)', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:29.343' AS DateTime), NULL, 0, NULL, NULL, NULL, N'232a6a14-37ff-40d7-9753-21a3f0131208', N'09ef0e0c-5c71-4783-b280-646d504af7b5', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'23ff6e80-a874-448d-a362-358bf2799d8e', N'9010003-010-011-6650-6652', N'9010003', N'010', N'011', N'6650', N'6652', N'', N'', N'', N'Bồi dưỡng giảng viên, báo cáo viên', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:18.393' AS DateTime), NULL, 0, NULL, NULL, NULL, N'25a9efc8-4107-42ac-b3fa-00d1875a92d1', N'13a6bbc8-d2ba-4c81-9d29-a6c42bbd2a2b', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'f0fd8d7e-a5d0-48a2-af4e-3651e6f7d845', N'9010003-010-011-6900-6913', N'9010003', N'010', N'011', N'6900', N'6913', N'', N'', N'', N'Tài sản và thiết bị văn phòng ', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:22.663' AS DateTime), NULL, 0, NULL, NULL, NULL, N'9f4276eb-b9d2-4554-a96c-45e8c7d0ff64', N'87c7737b-0150-452a-b22e-02b356bd590f', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'ac808890-d2a6-412d-b266-36e8056a218c', N'9010003', N'9010003', N'', N'', N'', N'', N'', N'', N'', N'Chi hỗ trợ quản lý BHXH', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:40.060' AS DateTime), NULL, 0, NULL, NULL, NULL, N'885f40fa-4c4d-4ef2-ab31-b075853b028f', N'18baf083-e5e0-4246-bc88-27eeb6ab28d4', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'cba232dc-a3c6-472e-89ce-3b02941df42f', N'9020001-010-011-0001-0001', N'9020001', N'012', N'011', N'0001', N'0001', N'', N'', N'', N'2. QNCN', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:31.440' AS DateTime), NULL, 0, NULL, NULL, NULL, N'0d4fa8e6-6deb-4b99-924d-5532cf4181a6', N'8de3ff2f-c2af-4608-afde-cc7e82d173a9', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, N'2', N'LHT_TT,PCCV_TT,PCTN_TT,PCTNVK_TT')
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'a6234120-8352-482c-886b-3b44be91e205', N'9010002-010-011-0008-0001-0001-00', N'9010002', N'010', N'011', N'0008', N'0001', N'0001', N'00', N'', N'- Mai táng phí (người)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:48.577' AS DateTime), NULL, 0, NULL, NULL, NULL, N'b66ca2a3-645d-4879-a0ca-6e4cb3c9b442', N'e56cedb7-ed03-485b-beab-c152f42ebadd', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'be87d84e-deaa-4576-8484-3b6ced8566dc', N'9010002-010-011-0007-0001-0002-00', N'9010002', N'010', N'011', N'0007', N'0001', N'0002', N'00', N'', N'- Trợ cấp KV 1 lần (người)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:48.113' AS DateTime), NULL, 0, NULL, NULL, NULL, N'b23ab43a-001b-4ce2-9f39-0be3ef79c50e', N'774d1074-d499-4f15-9ac0-40b04ad1ba17', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'8bd4e6f7-b3a7-4984-844c-3d29bc39fc78', N'9010003-010-011-6650-6657', N'9010003', N'010', N'011', N'6650', N'6657', N'', N'', N'', N'Các khoản thuê mướn khác phục vụ hội nghị', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:19.733' AS DateTime), NULL, 0, NULL, NULL, NULL, N'dea8ce64-0669-4f7c-9e53-09e0d5ffaa64', N'13a6bbc8-d2ba-4c81-9d29-a6c42bbd2a2b', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'efe88840-f7f2-41bf-bf5d-3ec3893dbc8b', N'9010003-010-011-7000', N'9010003', N'010', N'011', N'7000', N'', N'', N'', N'', N'Chi phí nghiệp vụ c.môn của từng ngành', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:23.653' AS DateTime), NULL, 0, NULL, NULL, NULL, N'06c2e5e2-42f0-4ff6-a18c-bf19f1ed707d', N'885f40fa-4c4d-4ef2-ab31-b075853b028f', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'54c0e1dc-13d0-4402-acb2-3fb50a328d4c', N'904', N'904', N'', N'', N'', N'', N'', N'', N'', N'Kinh phí KCB BHYT', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:38.320' AS DateTime), N'SLNS', 0, NULL, NULL, NULL, N'd06ce6a9-2a2e-4a7b-bad9-a88c61ac4cfe', N'25aa0569-2843-4e00-bd95-b48558a6e028', 2023, NULL, NULL, N'admin', N'admin', NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'9c47dc9c-2655-4536-8218-4023b12c0407', N'9010001-010-011-0001-0002', N'9010001', N'010', N'011', N'0001', N'0002', N'', N'', N'', N'- Con ốm(ngày)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:28.497' AS DateTime), NULL, 0, NULL, NULL, NULL, N'a297c80b-7b65-40e6-823f-fbed19e77501', N'63e1e32a-460b-4696-aa73-441934842ac0', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'f6f89e6c-d5ae-482b-96b3-4105af911ab7', N'9010002-010-011-0003', N'9010002', N'010', N'011', N'0003', N'', N'', N'', N'', N'3. Tai nạn lao động, bệnh NN', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:42.870' AS DateTime), NULL, 0, NULL, NULL, NULL, N'2d5d9fc0-6e68-4f70-834c-3ef277099bf6', N'7a097777-89ac-4d76-a856-d1534c4070a9', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'2c273846-2d06-4b80-af8e-411682715b38', N'9010003-010-011-6900-6912', N'9010003', N'010', N'011', N'6900', N'6912', N'', N'', N'', N'Các thiệt bị công nghệ thông tin ', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:22.427' AS DateTime), NULL, 0, NULL, NULL, NULL, N'da8365b6-7e62-42eb-acf5-b7978c7298c5', N'87c7737b-0150-452a-b22e-02b356bd590f', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'69a6922b-3f25-47fd-8cff-418f0db7d760', N'9010001-010-011-0003-0001-0008-00', N'9010001', N'010', N'011', N'0003', N'0001', N'0008', N'00', N'', N'- Chi h.trợ chuyển đổi n.nghiệp (người)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:33.600' AS DateTime), NULL, 0, NULL, NULL, NULL, N'294076d4-8afb-404d-929d-3d6ecc396771', N'c7647e43-8aca-4882-98f4-3ec7e0bc8714', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'e6fef45f-bbcc-48fc-8a66-4357947c9b87', N'9010003-010-011-6700-6749', N'9010003', N'010', N'011', N'6700', N'6749', N'', N'', N'', N'Khác', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:21.500' AS DateTime), NULL, 0, NULL, NULL, NULL, N'7bb79ca4-d725-4cf4-a28b-e2e3445e74d1', N'd9c3f3ea-9ba6-4767-8691-c9eee1d2b0aa', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'9166c675-f158-43f6-a569-441353739876', N'9010002-010-011-0002-0001-0002-00-01', N'9010002', N'010', N'011', N'0002', N'0001', N'0002', N'00', N'01', N'+ Lao động nữ', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:41.447' AS DateTime), NULL, 0, NULL, NULL, NULL, N'55bbcc88-4a81-4a66-b648-b3049330001c', N'9d0f9113-0bbc-4b8d-8e9b-60e41686b5ff', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'750d25f7-db5a-4be9-b92b-44567ab5d78b', N'9010003-010-011-7000-7049-0002', N'9010003', N'010', N'011', N'7000', N'7049', N'0002', N'', N'', N'- Chi phối hợp kiểm tra, thanh tra, phúc tra, giám sát công tác thu, Chi BHXH, BHYT', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:24.600' AS DateTime), NULL, 0, NULL, NULL, NULL, N'874cd699-e8bb-4d5d-96cc-fc2ef63e3f26', N'a6aa1af6-475c-4853-836f-c0bde339c5b8', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'c76c8e8e-976a-4390-bffe-4573f0e13017', N'9010003-010-011-6550-6551', N'9010003', N'010', N'011', N'6550', N'6551', N'', N'', N'', N'Văn phòng phẩm', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:49.330' AS DateTime), NULL, 0, NULL, NULL, NULL, N'566a0314-25dd-4ab6-9f33-b3779c438c61', N'2e93d30f-796f-4f33-938b-7d2dd2d38c3a', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'99e05260-f149-45c7-9494-45a5de11fc07', N'9010002-010-011-0002', N'9010002', N'010', N'011', N'0002', N'', N'', N'', N'', N'2. Trợ cấp thai sản', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:39.853' AS DateTime), NULL, 0, NULL, NULL, NULL, N'6f830377-5b2f-44d8-9a64-9d46c4270a80', N'7a097777-89ac-4d76-a856-d1534c4070a9', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'b4977313-bb80-434d-ae36-4880e68dbd13', N'9030001-010-011-0001-0002', N'9030001', N'016', N'011', N'0001', N'0002', N'', N'', N'', N'b. Bảo hiểm y tế thân nhân QNCN(thân nhân CMCY)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:36.483' AS DateTime), NULL, 0, NULL, NULL, NULL, N'adcb8096-366c-4729-8a51-1b40eaf78af2', N'a1c989de-eac9-4153-8a60-a34ac387bb88', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'ff3b64d1-147e-4ba0-853f-49a515e1f856', N'9010002-010-011-0003-0001-0008-00', N'9010002', N'010', N'011', N'0003', N'0001', N'0008', N'00', N'', N'- Chi h.trợ chuyển đổi n.nghiệp (người)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:44.560' AS DateTime), NULL, 0, NULL, NULL, NULL, N'5ce55373-18e4-43c6-965d-508d72db9ab5', N'2d5d9fc0-6e68-4f70-834c-3ef277099bf6', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'0761a5e8-a612-44c3-865a-4acc5dd4a2da', N'9010002-010-011-0008-0001-0002-00', N'9010002', N'010', N'011', N'0008', N'0001', N'0002', N'00', N'', N'- Tuất 1 lần (người)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:48.770' AS DateTime), NULL, 0, NULL, NULL, NULL, N'af6267f2-5472-4ea7-bb18-c36596b39611', N'e56cedb7-ed03-485b-beab-c152f42ebadd', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'7915b83d-7643-4cbb-b3c8-4b1c9994d21f', N'9010001-010-011-0002-0001-0001-00-01', N'9010001', N'010', N'011', N'0002', N'0001', N'0001', N'00', N'01', N'+ Lao động nữ(tháng)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:29.470' AS DateTime), NULL, 0, NULL, NULL, NULL, N'b9de30f6-3364-47e3-ab8a-a476893817df', N'232a6a14-37ff-40d7-9753-21a3f0131208', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'930c33d5-a0e3-4277-a896-4c69c9f3af62', N'9010002-010-011-0003-0001-0001-00', N'9010002', N'010', N'011', N'0003', N'0001', N'0001', N'00', N'', N'- Chi giám định mức suy giảm KNLĐ (người)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:43.097' AS DateTime), NULL, 0, NULL, NULL, NULL, N'51e03272-5f23-4dc9-95f1-c5cd7ee125c2', N'2d5d9fc0-6e68-4f70-834c-3ef277099bf6', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'5e5111e8-65e8-405f-a9b7-4d12f99e1714', N'9010005-010-011-0010', N'9010005', N'010', N'011', N'0010', N'', N'', N'', N'', N'1. Kinh phí KCB tại quân y đơn vị 10%', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:26.940' AS DateTime), NULL, 0, NULL, NULL, NULL, N'29147692-3954-4a2b-af22-bcbdb4dbc4ba', N'6ff5676d-c697-46f1-b3c2-5de8a2d593e4', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'4294148e-0762-424b-8fee-4dcfe3518dab', N'9010001-010-011-0004-0001-0002-00', N'9010001', N'010', N'011', N'0004', N'0001', N'0002', N'00', N'', N'- Trợ cấp KV 1 lần (người)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:34.500' AS DateTime), NULL, 0, NULL, NULL, NULL, N'18fcd280-fd4c-405c-b1a1-25d438da44ba', N'b43a61cb-2ad7-4944-ab5a-4ab5cfaef299', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'14fa5045-98ca-4e1c-a02e-4e7098b1289d', N'9010003-010-011-7750-7799-0001', N'9010003', N'010', N'011', N'7750', N'7799', N'0001', N'', N'', N'- Chi thưởng cho tập thể, cá nhân thực hiện tốt công tác Chi trả', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:26.017' AS DateTime), NULL, 0, NULL, NULL, NULL, N'994d7914-cf71-4767-99e4-697f10de2b78', N'65c08712-f281-4123-8efe-8848d21b220b', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'ec7d408e-20a4-4c1c-a301-50f39a268646', N'9010001-010-011-0003-0001-0002-00', N'9010001', N'010', N'011', N'0003', N'0001', N'0002', N'00', N'', N'- Trợ cấp 1 lần (người)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:32.327' AS DateTime), NULL, 0, NULL, NULL, NULL, N'795d2728-9ff9-4213-93f9-5e4bd22903e7', N'c7647e43-8aca-4882-98f4-3ec7e0bc8714', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'f38a1bf3-9ddc-482f-a3d6-513c42a32a1b', N'9010001-010-011-0003-0001-0004-00', N'9010001', N'010', N'011', N'0003', N'0001', N'0004', N'00', N'', N'- Trợ cấp phục hồi chức năng (người)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:32.733' AS DateTime), NULL, 0, NULL, NULL, NULL, N'30b10c02-7a67-41b4-afe2-a4652321a14b', N'c7647e43-8aca-4882-98f4-3ec7e0bc8714', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'2bf14098-7a71-43d8-abc4-53298eaaa207', N'9010004-010-011-0010-0001', N'9010004', N'010', N'011', N'0010', N'0001', N'', N'', N'', N'- Vật tư y tế(bông băng, bơm, kim tiêm)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:41.233' AS DateTime), NULL, 0, NULL, NULL, NULL, N'48112d8e-a78f-4a4c-8492-94cf989a8f7a', N'73357a2d-1879-452f-a85f-836a3090a537', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'1a705fbd-9384-49ae-9cbc-533fbeaa45bc', N'9020001-010-011-0002', N'9020001', N'014', N'011', N'0002', N'', N'', N'', N'', N'B. Người LĐ', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:32.103' AS DateTime), NULL, 0, NULL, NULL, NULL, N'4ce7992c-f413-434c-b785-1b99d6cbd025', N'eb1f7599-1ee7-4fef-be5e-e1ad25602a66', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'b7cc467d-9b9b-4671-b67d-5585ec30791a', N'9010001-010-011-0002-0001-0002-00-01', N'9010001', N'010', N'011', N'0002', N'0001', N'0002', N'00', N'01', N'+ Lao động nữ', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:30.147' AS DateTime), NULL, 0, NULL, NULL, NULL, N'b79bd552-a5b7-4336-bdaf-27e4d99a04af', N'568c1146-2660-4db8-ad31-6e78d8c0daca', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'6b401717-9cb9-46f1-a5fc-56c4e2433f9d', N'9010001-010-011-0004', N'9010001', N'010', N'011', N'0004', N'', N'', N'', N'', N'4. Hưu trí', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:34.100' AS DateTime), NULL, 0, NULL, NULL, NULL, N'b43a61cb-2ad7-4944-ab5a-4ab5cfaef299', N'91465483-df1b-4262-9436-d87f8808cfac', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'1e6bff53-2e48-4850-bff4-573ddd52ce3f', N'9010001-010-011-0008-0001-0002-00', N'9010001', N'010', N'011', N'0008', N'0001', N'0002', N'00', N'', N'- Tuất 1 lần (người)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:36.690' AS DateTime), NULL, 0, NULL, NULL, NULL, N'01e7c76b-294b-4440-b555-d0b33af107cc', N'f707d446-bee8-4299-b361-16ec2fb1d471', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'b5e1bd99-9155-48eb-8b69-5a429e895fbe', N'9010003-010-011-6900', N'9010003', N'010', N'011', N'6900', N'', N'', N'', N'', N'Sửa chữa, duy trì tài sản phục vụ công tác chuyên môn và các công trình cơ sở hạ tầng ', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:22.203' AS DateTime), NULL, 0, NULL, NULL, NULL, N'87c7737b-0150-452a-b22e-02b356bd590f', N'885f40fa-4c4d-4ef2-ab31-b075853b028f', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'56921e32-5770-4d6e-91a8-5ae134d33ba1', N'9010001-010-011-0002', N'9010001', N'010', N'011', N'0002', N'', N'', N'', N'', N'2. Trợ cấp thai sản', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:29.047' AS DateTime), NULL, 0, NULL, NULL, NULL, N'09ef0e0c-5c71-4783-b280-646d504af7b5', N'91465483-df1b-4262-9436-d87f8808cfac', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'42ca8a27-c540-4bc2-9851-5b2f9354fac2', N'9010001-010-011-0002-0001-0003-00-01', N'9010001', N'010', N'011', N'0002', N'0001', N'0003', N'00', N'01', N'+ Lao động nữ', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:30.823' AS DateTime), NULL, 0, NULL, NULL, NULL, N'0e323456-0843-4c30-b020-e686be1f879b', N'd27fbd1b-73ad-48a0-bcba-111b296a6b1c', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'e2cbbb43-86c2-4557-bf3c-5ce72e1dcde0', N'9010002-010-011-0003-0001-0005-00', N'9010002', N'010', N'011', N'0003', N'0001', N'0005', N'00', N'', N'- Trợ cấp phục vụ (người)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:43.910' AS DateTime), NULL, 0, NULL, NULL, NULL, N'22055e87-e713-4b77-97de-65672022c594', N'2d5d9fc0-6e68-4f70-834c-3ef277099bf6', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'a2ecbc1d-260e-4726-8a2a-5e0810962779', N'9010003-010-011-6600', N'9010003', N'010', N'011', N'6600', N'', N'', N'', N'', N'Thông tin, tuyên truyền, liên lạc', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:50.143' AS DateTime), NULL, 0, NULL, NULL, NULL, N'4ec9c459-3a9b-4106-a0bf-f0550e0ec73f', N'885f40fa-4c4d-4ef2-ab31-b075853b028f', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'5bad2938-19cb-459a-85af-5ed760b4c870', N'9030006', N'9030006', N'', N'', N'', N'', N'', N'', N'', N'VI. BHYT người nước ngoài đang học trong các trường QĐ', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:38.080' AS DateTime), NULL, 0, NULL, NULL, NULL, N'1499a32d-8556-4ad5-bff4-81a307916605', N'5a1234af-171a-44c9-98ed-9da7deff0188', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'b3658f45-5285-4e28-a95c-5f6e896399e1', N'9010003-010-011-6600-6606', N'9010003', N'010', N'011', N'6600', N'6606', N'', N'', N'', N'Tuyên truyền (phát thanh, truyền hình)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:50.883' AS DateTime), NULL, 0, NULL, NULL, NULL, N'f42345ce-969b-482c-a5f5-570b0d4cdf36', N'4ec9c459-3a9b-4106-a0bf-f0550e0ec73f', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'c9a82f96-004f-4987-915a-5fa69fc67a6c', N'9010004-010-011-0010', N'9010004', N'010', N'011', N'0010', N'', N'', N'', N'', N'1. Kinh phí KCB tại quân y đơn vị 10%', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:40.577' AS DateTime), NULL, 0, NULL, NULL, NULL, N'73357a2d-1879-452f-a85f-836a3090a537', N'ffb45fa2-8a4b-4918-ba6a-19d9c3da8044', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'0a01ffd7-e7a6-4ce9-a6e1-623b00984e6f', N'9010003-010-011-6550-6553', N'9010003', N'010', N'011', N'6550', N'6553', N'', N'', N'', N'Khoán văn phòng phẩm', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:49.710' AS DateTime), NULL, 0, NULL, NULL, NULL, N'86b2f605-c16b-4158-90d5-c6a9dfd22c61', N'2e93d30f-796f-4f33-938b-7d2dd2d38c3a', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'2f1228c3-29f3-41db-b28e-64be64656cb4', N'9010001-010-011-0002-0001-0002-00', N'9010001', N'010', N'011', N'0002', N'0001', N'0002', N'00', N'', N'- Trợ cấp 1 lần (người)', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:29.913' AS DateTime), NULL, 0, NULL, NULL, NULL, N'568c1146-2660-4db8-ad31-6e78d8c0daca', N'c7647e43-8aca-4882-98f4-3ec7e0bc8714', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'607e05e4-eefc-44ef-ad45-66827a0f13b7', N'9010001-010-011-0003-0001-0006-00', N'9010001', N'010', N'011', N'0003', N'0001', N'0006', N'00', N'', N'- Trợ cấp chết do TNLD. BNN (người)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:33.137' AS DateTime), NULL, 0, NULL, NULL, NULL, N'6a99dbd6-a4ca-49f3-b3d3-0014c20a9a2d', N'c7647e43-8aca-4882-98f4-3ec7e0bc8714', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'b6e44430-4c76-4240-a2dd-66cbfc1bdc2a', N'9010001-010-011-0008-0001-0001-00', N'9010001', N'010', N'011', N'0008', N'0001', N'0001', N'00', N'', N'- Mai táng phí (người)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:36.453' AS DateTime), NULL, 0, NULL, NULL, NULL, N'c1003754-545f-4a6b-acb1-ceb56a4a19c2', N'f707d446-bee8-4299-b361-16ec2fb1d471', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'affa866f-6640-4e72-afc2-685372c07304', N'9010001-010-011-0005-0001-0001-00', N'9010001', N'010', N'011', N'0005', N'0001', N'0001', N'00', N'', N'- Trợ cấp 1 lần (người)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:34.913' AS DateTime), NULL, 0, NULL, NULL, NULL, N'b4187139-caae-46f6-a079-343810e05db2', N'1da67c9f-2025-4bd0-9d24-20fa64651658', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'477f90bd-46a2-4a03-9839-6a118a1bd2d4', N'9020001', N'9020001', N'', N'', N'', N'', N'', N'', N'', N'I. KHỐI DỰ TOÁN', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:30.790' AS DateTime), NULL, 0, NULL, NULL, NULL, N'eb1f7599-1ee7-4fef-be5e-e1ad25602a66', N'f2bf9995-7b34-4d49-b973-3e135bf6e28a', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'6870437d-9ba2-4e65-a4c7-6a48f57fd490', N'9010007-010-011-0010-0001', N'9010007', N'012', N'011', N'0010', N'0001', N'', N'', N'', N'- Vật tư y tế(bông băng, bơm, kim tiêm)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:30.000' AS DateTime), NULL, 0, NULL, NULL, NULL, N'e8d7397c-9105-4d17-a4c8-678332804d6c', N'043d854e-bca5-4ee9-905a-8b78d15b9887', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'bb6d3054-2d64-4a5d-8d92-6bd6b69a5000', N'903', N'903', N'', N'', N'', N'', N'', N'', N'', N'Thu BHYT thân nhân', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:40.933' AS DateTime), NULL, 0, NULL, NULL, NULL, N'5a1234af-171a-44c9-98ed-9da7deff0188', N'25aa0569-2843-4e00-bd95-b48558a6e028', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'057e26f5-e4b8-4250-b288-70240c95b7c8', N'9010006', N'9010006', N'', N'', N'', N'', N'', N'', N'', N'I.Khối dự toán ', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:27.963' AS DateTime), NULL, 0, NULL, NULL, NULL, N'78861c3d-d2be-4c22-8a9a-2ccc7a63502a', N'18baf083-e5e0-4246-bc88-27eeb6ab28d4', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'bea3ea53-2129-4c20-b9aa-728cf355d3e7', N'9010003-010-011-7750-7799', N'9010003', N'010', N'011', N'7750', N'7799', N'', N'', N'', N'Chi các khoản khác', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:25.717' AS DateTime), NULL, 0, NULL, NULL, NULL, N'65c08712-f281-4123-8efe-8848d21b220b', N'c60199d0-3566-4c28-91d5-cbde4cd4f792', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'035377d4-be2a-4f1b-a242-75949a1a280f', N'9010001-010-011-0001-0001-0001-02-02', N'9010001', N'010', N'011', N'0001', N'0001', N'0001', N'02', N'02', N'* Từ 14 ngày trở lên/tháng(ngày)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:28.103' AS DateTime), NULL, 0, NULL, NULL, NULL, N'd0b64142-699e-4f9e-a6f5-824b03051c0e', N'4ecb27b3-f09b-4972-9d22-965886faab0a', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'1cece482-cc7c-431e-9bcd-77a775e6e8e5', N'9030005', N'9030005', N'', N'', N'', N'', N'', N'', N'', N'V. BHYT học viên đào tạo sĩ quan dự bị từ 03 tháng trở lên', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:37.890' AS DateTime), NULL, 0, NULL, NULL, NULL, N'68018115-052e-4d82-b8ab-e40648c1cf49', N'5a1234af-171a-44c9-98ed-9da7deff0188', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'46c189e6-5680-4401-9529-79f7042415e8', N'9030001-010-011-0000-0002', N'9030001', N'012', N'011', N'0000', N'0002', N'', N'', N'', N'b. Bảo hiểm y tế thân nhân QNCN(thân nhân CMCY)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:35.493' AS DateTime), NULL, 0, NULL, NULL, NULL, N'8e4f18ba-73bc-4614-bd34-680af8bb4456', N'800fbf3c-386c-40c7-a6c3-2b1251a94009', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'4e6d156a-2577-4190-a61b-7c0901d59b0f', N'9030001-010-011-0000-0001', N'9030001', N'011', N'011', N'0000', N'0001', N'', N'', N'', N'a. Bảo hiểm y tế thân nhân sĩ quan(thân nhân hàm CY)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:35.260' AS DateTime), NULL, 0, NULL, NULL, NULL, N'f5b01bd6-57b9-4523-9ed0-b8157a89e171', N'800fbf3c-386c-40c7-a6c3-2b1251a94009', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'eec16669-6f09-4e01-8fcb-7c52a406ecdf', N'9010003-010-011-6650-6699', N'9010003', N'010', N'011', N'6650', N'6699', N'', N'', N'', N'Chi phí khác ', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:20.070' AS DateTime), NULL, 0, NULL, NULL, NULL, N'9a6ab999-351c-4878-aa57-0807366a6f70', N'13a6bbc8-d2ba-4c81-9d29-a6c42bbd2a2b', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'0d6e9fbd-9713-4840-bf1f-7c9acade252e', N'9020001-010-011-0002-0000', N'9020001', N'015', N'011', N'0002', N'0000', N'', N'', N'', N'1. CC,CN, VCQP', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:32.320' AS DateTime), NULL, 0, NULL, NULL, NULL, N'0bbae696-0592-4757-8431-f2f40f460b7a', N'4ce7992c-f413-434c-b785-1b99d6cbd025', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, N'4', N'LHT_TT,PCCV_TT,PCTN_TT,PCTNVK_TT')
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'ef5988b6-ecd1-4e20-bd28-7d553f4ab849', N'9010003-010-011-7000-7049-0003', N'9010003', N'010', N'011', N'7000', N'7049', N'0003', N'', N'', N'- Đối chiếu danh sách, bảng lương, đôn đốc thu', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:24.820' AS DateTime), NULL, 0, NULL, NULL, NULL, N'039a4045-76a1-4ae0-927d-e42c4a021223', N'a6aa1af6-475c-4853-836f-c0bde339c5b8', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'1efc31b9-3bcc-4b8f-bce0-7fc966870b49', N'9010006-010-011-0010-0000', N'9010006', N'011', N'011', N'0010', N'0000', N'', N'', N'', N'- Thuốc', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:28.380' AS DateTime), NULL, 0, NULL, NULL, NULL, N'e3b3fc59-1030-41d6-ae90-3c3dd8fe6d1c', N'8088e295-2f9c-45b3-a799-d88940922713', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'7ab0b613-bc86-4f45-ac3d-8909528e4e4d', N'9010003-010-011-6700-6701', N'9010003', N'010', N'011', N'6700', N'6701', N'', N'', N'', N'Tiền vé máy bay, tàu xe', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:20.543' AS DateTime), NULL, 0, NULL, NULL, NULL, N'7d577619-8dea-4632-a2f5-5f53e7cdbff7', N'd9c3f3ea-9ba6-4767-8691-c9eee1d2b0aa', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'ebb43fd8-3b5a-40bb-8ec7-896ec9a3dda0', N'9010003-010-011-6650-6654', N'9010003', N'010', N'011', N'6650', N'6654', N'', N'', N'', N'Tiền thuê phòng ngủ (đối với đại biểu là khách mời', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:18.850' AS DateTime), NULL, 0, NULL, NULL, NULL, N'751a5278-b174-4ea7-a278-968e3c7c8894', N'13a6bbc8-d2ba-4c81-9d29-a6c42bbd2a2b', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'b05d9834-da40-4756-895c-898676f8d3bb', N'9010002-010-011-0002-0001-0003-00', N'9010002', N'010', N'011', N'0002', N'0001', N'0003', N'00', N'', N'- Khám thai, KHH GĐ, nam nghỉ việc khi vợ Sinh Con(ngày)', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:41.933' AS DateTime), NULL, 0, NULL, NULL, NULL, N'cf731c26-af8a-4708-82de-c03bd8b38715', N'6f830377-5b2f-44d8-9a64-9d46c4270a80', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'0e5234d1-98c1-438f-a19a-8994e0482f30', N'9010001-010-011-0002-0001-0004-00', N'9010001', N'010', N'011', N'0002', N'0001', N'0004', N'00', N'', N'- Dưỡng sức, phục hồi SK', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:31.680' AS DateTime), NULL, 0, NULL, NULL, NULL, N'8e092c28-89d2-460b-ab60-5aafbf9e010a', N'09ef0e0c-5c71-4783-b280-646d504af7b5', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'8c849b76-f15a-46c6-b229-89d806847572', N'9020001-010-011-0002-0001', N'9020001', N'016', N'011', N'0002', N'0001', N'', N'', N'', N'2. LĐHĐ', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:32.567' AS DateTime), NULL, 0, NULL, NULL, NULL, N'84ae6d38-5bfb-4c0e-96dc-dcdf8be8cfb7', N'4ce7992c-f413-434c-b785-1b99d6cbd025', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, N'5', N'CONGCHUAN_SN,LHT_TT')
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'9eeb750f-208d-4a9a-a438-8a35bc8c3e2a', N'9010001-010-011-0002-0001-0003-00', N'9010001', N'010', N'011', N'0002', N'0001', N'0003', N'00', N'', N'- Khám thai, KHH GĐ, nam nghỉ việc khi vợ Sinh Con(ngày)', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:30.657' AS DateTime), NULL, 0, NULL, NULL, NULL, N'd27fbd1b-73ad-48a0-bcba-111b296a6b1c', N'09ef0e0c-5c71-4783-b280-646d504af7b5', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'8ba8dcce-6a40-4f95-b9ca-8b083f147dae', N'9030002-010-011-0001', N'9030002', N'010', N'011', N'0001', N'', N'', N'', N'', N'2. Khối hạch toán', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:37.243' AS DateTime), NULL, 0, NULL, NULL, NULL, N'0989f265-2645-430c-9a78-b811b0a2f37f', N'9ad55244-03c4-4c97-ba26-27af54495842', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'8dce72ae-b1db-4329-9020-8b3ace634770', N'9010005', N'9010005', N'', N'', N'', N'', N'', N'', N'', N'II.Khối hạch toán ', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:26.730' AS DateTime), NULL, 0, NULL, NULL, NULL, N'6ff5676d-c697-46f1-b3c2-5de8a2d593e4', N'18baf083-e5e0-4246-bc88-27eeb6ab28d4', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'7795d523-102d-4d63-a054-8b430252ce11', N'901', N'901', N'', N'', N'', N'', N'', N'', N'', N'Chi BHXH', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:39.573' AS DateTime), N'SLNS', 0, NULL, NULL, NULL, N'18baf083-e5e0-4246-bc88-27eeb6ab28d4', N'25aa0569-2843-4e00-bd95-b48558a6e028', 2023, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'b496cb82-61eb-4071-b0b9-8c0f76456efe', N'9030001-010-011-0000-0003', N'9030001', N'013', N'011', N'0000', N'0003', N'', N'', N'', N'c. Bảo hiểm y tế thân nhân HSQ-CS(thân nhân học viên cơ yếu)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:35.803' AS DateTime), NULL, 0, NULL, NULL, NULL, N'100ef748-1f6f-4891-944e-4a11b59a0fcf', N'800fbf3c-386c-40c7-a6c3-2b1251a94009', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'd2c546c0-ae47-4072-bfa0-8d3ae616e1a8', N'9030004', N'9030004', N'', N'', N'', N'', N'', N'', N'', N'IV. BHYT học sinh, sinh viên hệ dân sự', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:37.770' AS DateTime), NULL, 0, NULL, NULL, NULL, N'cf9bf98b-3dde-44a9-8a71-dc0e83df2b5e', N'5a1234af-171a-44c9-98ed-9da7deff0188', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'39d862c6-517b-4f31-8c9e-8d3d3ce0b383', N'9010003-010-011-6600-6618', N'9010003', N'010', N'011', N'6600', N'6618', N'', N'', N'', N'Khoán điện thoại', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:51.367' AS DateTime), NULL, 0, NULL, NULL, NULL, N'781048d2-925a-4fc9-b0c7-752d42b49bd4', N'4ec9c459-3a9b-4106-a0bf-f0550e0ec73f', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'f632c76d-5a6b-4991-939e-8dcbd4fbf9ec', N'9010002-010-011-0002-0001-0003-00-01', N'9010002', N'010', N'011', N'0002', N'0001', N'0003', N'00', N'01', N'+ Lao động nữ', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:42.150' AS DateTime), NULL, 0, NULL, NULL, NULL, N'6f83cc9b-2907-4417-b19b-72f705f090cb', N'cf731c26-af8a-4708-82de-c03bd8b38715', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'db96c86c-57b2-4400-a83c-8ff7002e593d', N'9030001-010-011-0001-0003', N'9030001', N'017', N'011', N'0001', N'0003', N'', N'', N'', N'c. Bảo hiểm y tế thân nhân HSQ-CS(thân nhân học viên cơ yếu)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:36.643' AS DateTime), NULL, 0, NULL, NULL, NULL, N'77b53c15-3232-4207-83be-759cbeeb098b', N'a1c989de-eac9-4153-8a60-a34ac387bb88', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'cbc64d84-22f9-46e1-992d-901c3c8f7a17', N'9010002-010-011-0001-0001-0001-02', N'9010002', N'010', N'011', N'0001', N'0001', N'0001', N'02', N'', N'+ ốm khác', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:38.367' AS DateTime), NULL, 0, NULL, NULL, NULL, N'0c64a47a-ae28-472f-82f7-a2d818d107ba', N'06a74e94-f3ba-4d73-8f80-9ae9563e1000', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'debb393f-f1c3-4239-8b37-917d776c2b46', N'9010002-010-011-0002-0001-0004-00', N'9010002', N'010', N'011', N'0002', N'0001', N'0004', N'00', N'', N'- Dưỡng sức, phục hồi SK', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:42.603' AS DateTime), NULL, 0, NULL, NULL, NULL, N'097b3961-33a9-4e67-a9cb-10cdf604d8dc', N'6f830377-5b2f-44d8-9a64-9d46c4270a80', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'0df0e2c0-1c98-4487-a4d6-932c8fb67f4b', N'9010003-010-011-6650-6655', N'9010003', N'010', N'011', N'6650', N'6655', N'', N'', N'', N'Thuê hội trường, phương tiện vận chuyển', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:19.150' AS DateTime), NULL, 0, NULL, NULL, NULL, N'a7bfb1a0-f712-45bc-9a4d-41f850a4fd52', N'13a6bbc8-d2ba-4c81-9d29-a6c42bbd2a2b', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'146e93b4-7663-4218-8da7-93dac8275c1f', N'9010003-010-011-7000-7049-0004', N'9010003', N'010', N'011', N'7000', N'7049', N'0004', N'', N'', N'- kiểm tra, xác minh, giám sát, quản lý Đối tượng hưởng tại đơn vị', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:24.940' AS DateTime), NULL, 0, NULL, NULL, NULL, N'd400e9c7-319a-4c0f-b449-a30ac04b8c41', N'a6aa1af6-475c-4853-836f-c0bde339c5b8', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'c5e86eba-4169-49ed-bd68-941a9b4788f2', N'9030003', N'9030003', N'', N'', N'', N'', N'', N'', N'', N'III. BHYT học viên đào tạo cán bộ QS cấp xã, phường', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:37.467' AS DateTime), NULL, 0, NULL, NULL, NULL, N'449dec2d-4376-41c9-96fe-c054e152d4bb', N'5a1234af-171a-44c9-98ed-9da7deff0188', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'77625492-37b0-4ba7-8bfe-96babea4fe0b', N'9010003-010-011-6650-6658', N'9010003', N'010', N'011', N'6650', N'6658', N'', N'', N'', N'Chi bù tiền ăn', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:19.953' AS DateTime), NULL, 0, NULL, NULL, NULL, N'0548f574-a5fb-4df5-a08f-a0a4cbad5b00', N'13a6bbc8-d2ba-4c81-9d29-a6c42bbd2a2b', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'b24d61fa-a8b6-4158-bbfb-9a8d1d008c05', N'9010007-010-011-0010-0000', N'9010007', N'011', N'011', N'0010', N'0000', N'', N'', N'', N'- Thuốc', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:29.763' AS DateTime), NULL, 0, NULL, NULL, NULL, N'40d58a93-4702-4a2b-8168-81a6fe18f12d', N'043d854e-bca5-4ee9-905a-8b78d15b9887', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'e718763f-89ce-48cc-a235-9b394c09fac2', N'9010002-010-011-0004-0001-0002-00', N'9010002', N'010', N'011', N'0004', N'0001', N'0002', N'00', N'', N'- Trợ cấp KV 1 lần (người)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:45.550' AS DateTime), NULL, 0, NULL, NULL, NULL, N'939834ed-9ed7-406a-8cc6-b8d4b2930c66', N'69183e88-7f92-473e-a228-47d369f839e7', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'39466468-7c95-491f-b20a-9c3a32c5ca28', N'9010007-010-011-0010', N'9010007', N'010', N'011', N'0010', N'', N'', N'', N'', N'1. Kinh phí KCB tại Trường Sa - DK ', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:29.490' AS DateTime), NULL, 0, NULL, NULL, NULL, N'043d854e-bca5-4ee9-905a-8b78d15b9887', N'a5a37e2d-c950-40cb-a00d-82ec68e4e832', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'3762ebe7-7989-47ac-b85a-9c7b297c7dbc', N'9010002-010-011-0007-0001-0001-00', N'9010002', N'010', N'011', N'0007', N'0001', N'0001', N'00', N'', N'- Trợ cấp 1 lần (người)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:47.957' AS DateTime), NULL, 0, NULL, NULL, NULL, N'2907ac93-5edc-4c3b-a86d-37336769b417', N'774d1074-d499-4f15-9ac0-40b04ad1ba17', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'6f212a28-ecd2-4f56-b8d1-9d6cef5ed1fa', N'9010001-010-011-0003-0001-0009-00', N'9010001', N'010', N'011', N'0003', N'0001', N'0009', N'00', N'', N'- DS, PHSK sau TNLĐ, BNN (người)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:33.873' AS DateTime), NULL, 0, NULL, NULL, NULL, N'77ec4558-40fd-4d3b-864d-df96aceccb53', N'c7647e43-8aca-4882-98f4-3ec7e0bc8714', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'c1ce6be3-ee31-4982-b0d6-9dcdf89cf4ab', N'9010001-010-011-0005-0001-0002-00', N'9010001', N'010', N'011', N'0005', N'0001', N'0002', N'00', N'', N'- Trợ cấp KV 1 lần (người)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:35.077' AS DateTime), NULL, 0, NULL, NULL, NULL, N'36c7cef2-d61f-4073-9fd1-f866a1375b19', N'1da67c9f-2025-4bd0-9d24-20fa64651658', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'5555f91f-1942-4ca9-aeb0-9f1900c597a7', N'9030001-010-011-0000', N'9030001', N'010', N'011', N'0000', N'', N'', N'', N'', N'1. Khối dự toán', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:34.990' AS DateTime), NULL, 0, NULL, NULL, NULL, N'800fbf3c-386c-40c7-a6c3-2b1251a94009', N'4c52e8ab-654e-4b86-afbc-165823b677a2', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'f9cfca5f-9484-494b-a651-9f38b7b48e30', N'9010007-010-011-0010-0002', N'9010007', N'013', N'011', N'0010', N'0002', N'', N'', N'', N'- Dịch vụ kỹ thuật', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:30.210' AS DateTime), NULL, 0, NULL, NULL, NULL, N'45438d5c-982d-41d5-aa67-d28213290663', N'043d854e-bca5-4ee9-905a-8b78d15b9887', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'e82ed126-2c43-4a1a-b8a8-a02d7c588907', N'9010001-010-011-0001-0001-0001-01', N'9010001', N'010', N'011', N'0001', N'0001', N'0001', N'01', N'', N'+ Thuộc DM bệnh cần chữa trị dài ngày', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:26.330' AS DateTime), NULL, 0, NULL, NULL, NULL, N'33eaed8b-c9a6-49aa-96ee-1a9652c99ce9', N'a655d431-de68-4238-b921-55850d8bba6b', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'53503f5b-f26f-432f-9588-a064aa5010fe', N'9010006-010-011-0010-0002', N'9010006', N'013', N'011', N'0010', N'0002', N'', N'', N'', N'- Dịch vụ kỹ thuật', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:28.750' AS DateTime), NULL, 0, NULL, NULL, NULL, N'e1ac0849-a18f-4c20-89e2-0ce336d197bf', N'8088e295-2f9c-45b3-a799-d88940922713', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'4cebf8d9-c880-4c6e-bebc-a1567dce347a', N'9010004-010-011-0010-0002', N'9010004', N'010', N'011', N'0010', N'0002', N'', N'', N'', N'- Dịch vụ kỹ thuật', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:41.627' AS DateTime), NULL, 0, NULL, NULL, NULL, N'b7898495-5a08-4079-834f-f12d2e7939d6', N'73357a2d-1879-452f-a85f-836a3090a537', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'87dca630-ba2e-49a3-b00a-a2cad3e1c05c', N'9010002-010-011-0001-0001-0001-02-02', N'9010002', N'010', N'011', N'0001', N'0001', N'0001', N'02', N'02', N'* Từ 14 ngày trở lên/tháng(ngày)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:38.950' AS DateTime), NULL, 0, NULL, NULL, NULL, N'6d2d85ba-b319-4704-8839-2f10cfdcb670', N'0c64a47a-ae28-472f-82f7-a2d818d107ba', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'19fbc5fa-c660-4fb3-aed1-a50e2448a609', N'9010002-010-011-0003-0001-0006-00', N'9010002', N'010', N'011', N'0003', N'0001', N'0006', N'00', N'', N'- Trợ cấp chết do TNLD. BNN (người)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:44.117' AS DateTime), NULL, 0, NULL, NULL, NULL, N'898acb1c-49f6-4395-9452-418352b0eed1', N'2d5d9fc0-6e68-4f70-834c-3ef277099bf6', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'b5d6031c-9bd2-40ca-9269-a74ad570723e', N'9010001-010-011-0002-0001-0002-00-02', N'9010001', N'010', N'011', N'0002', N'0001', N'0002', N'00', N'02', N'+ Lao động nam', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:30.410' AS DateTime), NULL, 0, NULL, NULL, NULL, N'e1859298-643f-4f14-a387-231474454b25', N'568c1146-2660-4db8-ad31-6e78d8c0daca', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'dde556cf-a696-43ff-8297-a8a2b8670720', N'9010003-010-011-7000-7049-0005', N'9010003', N'010', N'011', N'7000', N'7049', N'0005', N'', N'', N'- Chi hỗ Trợ bệnh viện, bệnh xá KCB BHYT', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:25.060' AS DateTime), NULL, 0, NULL, NULL, NULL, N'1c72986d-0c37-4e1e-b997-75b6d26bee60', N'a6aa1af6-475c-4853-836f-c0bde339c5b8', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'30d777c0-aadf-43f2-86c1-a8ec5d389904', N'9010003-010-011-6650-6656', N'9010003', N'010', N'011', N'6650', N'6656', N'', N'', N'', N'Thuê phiên dịch, biên dịch phục vụ hội nghị', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:19.423' AS DateTime), NULL, 0, NULL, NULL, NULL, N'e48cd68f-5928-4231-97eb-4eba9a644b2f', N'13a6bbc8-d2ba-4c81-9d29-a6c42bbd2a2b', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'f34ac728-7920-4dc5-a8b3-a8f2bb8f2868', N'9010002-010-011-0001-0003', N'9010002', N'010', N'011', N'0001', N'0003', N'', N'', N'', N'- Dưỡng sức, phục hồi SK(ngày)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:39.520' AS DateTime), NULL, 0, NULL, NULL, NULL, N'866cedc7-d4a3-4b0e-bbb9-ebb5b227b413', N'64e4b8a9-87f6-46f8-9e2b-7b32aa4f6f32', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'ce2ef9fc-6aed-49b3-a23c-a96eaf0da445', N'9010002-010-011-0004-0001-0001-00', N'9010002', N'010', N'011', N'0004', N'0001', N'0001', N'00', N'', N'-  Trợ cấp 1 lần', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:45.277' AS DateTime), NULL, 0, NULL, NULL, NULL, N'3d61c53a-3c3d-4a96-8dda-3565c77a2923', N'69183e88-7f92-473e-a228-47d369f839e7', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'8195c253-0e01-47b7-b75e-a9760481eb05', N'9010003-010-011-7000-7012', N'9010003', N'010', N'011', N'7000', N'7012', N'', N'', N'', N'Chi phí hoạt động nghiệp vụ chuyên ngành ', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:23.937' AS DateTime), NULL, 0, NULL, NULL, NULL, N'7b143cca-0dad-4003-8708-d479f83fdd53', N'06c2e5e2-42f0-4ff6-a18c-bf19f1ed707d', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'0ffcc7d3-7d3e-4c0f-a616-a9883d25384e', N'9010001-010-011-0001', N'9010001', N'010', N'011', N'0001', N'', N'', N'', N'', N'1. Trợ cấp ốm đau', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:25.797' AS DateTime), NULL, 0, NULL, NULL, NULL, N'63e1e32a-460b-4696-aa73-441934842ac0', N'91465483-df1b-4262-9436-d87f8808cfac', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'4fecccd4-52f1-47d0-bf9c-abfff76b0046', N'9010003-010-011-6750-6751', N'9010003', N'010', N'011', N'6750', N'6751', N'', N'', N'', N'Thuê phương tiện vận chuyển ', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:21.817' AS DateTime), NULL, 0, NULL, NULL, NULL, N'1ed591dc-2b4a-44f6-9f2f-51dbac62e209', N'8fc998eb-21f2-4c1c-9711-1e010b7da6ba', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'5b01a4bb-182b-4548-8209-ac0e7e67bcc5', N'9010002-010-011-0001-0001-0001-02-01', N'9010002', N'010', N'011', N'0001', N'0001', N'0001', N'02', N'01', N'* Dưới 14 ngày/tháng(ngày)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:38.600' AS DateTime), NULL, 0, NULL, NULL, NULL, N'0a70e747-0f95-4925-b6bd-0b91f4cf8ed1', N'0c64a47a-ae28-472f-82f7-a2d818d107ba', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'181d0e9e-e9e7-42eb-95bd-ac7fdf846548', N'9030001-010-011-0001', N'9030001', N'014', N'011', N'0001', N'', N'', N'', N'', N'2. Khối hạch toán', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:36.087' AS DateTime), NULL, 0, NULL, NULL, NULL, N'a1c989de-eac9-4153-8a60-a34ac387bb88', N'4c52e8ab-654e-4b86-afbc-165823b677a2', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'adbf5e03-9b45-405e-a392-ae951621cef4', N'9010001-010-011-0001-0001-0001-01-02', N'9010001', N'010', N'011', N'0001', N'0001', N'0001', N'02', N'02', N'* Từ 14 ngày trở lên/ tháng(ngày)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:27.573' AS DateTime), NULL, 0, NULL, NULL, NULL, N'f9022301-b3c7-4279-b634-632b69936503', N'33eaed8b-c9a6-49aa-96ee-1a9652c99ce9', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'034f054e-1132-4137-a35f-b03e00a13e9d', N'9010003-010-011-6600-6605', N'9010003', N'010', N'011', N'6600', N'6605', N'', N'', N'', N'Thuê bao kênh vệ tinh; thuê bao cáp truyền hình; cước phí Internet, thuê đường truyền mạng ', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:51.823' AS DateTime), NULL, 0, NULL, NULL, NULL, N'd6d5d9a4-d07f-406a-bf3c-08d6efb181b0', N'4ec9c459-3a9b-4106-a0bf-f0550e0ec73f', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'88171733-435f-4041-8aa9-b04e9fc84207', N'9010001-010-011-0004-0001-0001-00', N'9010001', N'010', N'011', N'0004', N'0001', N'0001', N'00', N'', N'-  Trợ cấp 1 lần', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:34.270' AS DateTime), NULL, 0, NULL, NULL, NULL, N'5cca7dd1-b4e8-4273-86cc-ea4ff207409b', N'b43a61cb-2ad7-4944-ab5a-4ab5cfaef299', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'473152d2-b7ba-4fbb-91fe-b2762dc91fb9', N'9010002-010-011-0001-0002', N'9010002', N'010', N'011', N'0001', N'0002', N'', N'', N'', N'- Con ốm(ngày)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:39.173' AS DateTime), NULL, 0, NULL, NULL, NULL, N'63e1bfb4-2e48-4b3e-b773-0be4cd4a194e', N'64e4b8a9-87f6-46f8-9e2b-7b32aa4f6f32', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'3a3ea2e9-fd9c-4b75-bd3c-b2cbce07f452', N'9010004', N'9010004', N'', N'', N'', N'', N'', N'', N'', N'I.Khối dự toán', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:26.243' AS DateTime), NULL, 0, NULL, NULL, NULL, N'ffb45fa2-8a4b-4918-ba6a-19d9c3da8044', N'18baf083-e5e0-4246-bc88-27eeb6ab28d4', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'8d1b5908-dab3-4e82-b035-b8eb32741c88', N'9010003-010-011-6550-6552', N'9010003', N'010', N'011', N'6550', N'6552', N'', N'', N'', N'Mua sắm công cụ, dụng cụ văn phòng', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:49.510' AS DateTime), NULL, 0, NULL, NULL, NULL, N'bf5ffd8c-e50f-4843-a003-156c9e2a5eba', N'2e93d30f-796f-4f33-938b-7d2dd2d38c3a', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'5a1982af-501c-41f5-b484-bb6d02084fab', N'9010002-010-011-0001', N'9010002', N'010', N'011', N'0001', N'', N'', N'', N'', N'1. Trợ cấp ốm đau', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:37.250' AS DateTime), NULL, 0, NULL, NULL, NULL, N'64e4b8a9-87f6-46f8-9e2b-7b32aa4f6f32', N'7a097777-89ac-4d76-a856-d1534c4070a9', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'4fec6b34-0548-488d-b2c5-bd3191bd03d7', N'9020001-010-011-0001-0000', N'9020001', N'011', N'011', N'0001', N'0000', N'', N'', N'', N'1. Sĩ quan', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:31.220' AS DateTime), NULL, 0, NULL, NULL, NULL, N'e06ad864-6b29-4f0e-bd3b-3e617e9dfda3', N'8de3ff2f-c2af-4608-afde-cc7e82d173a9', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, N'1', N'LHT_TT,PCCV_TT,PCTN_TT,PCTNVK_TT')
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'acf5d609-9565-4b3b-bc87-bd4c40225508', N'9020002-010-011-0002-0001', N'9020002', N'016', N'011', N'0002', N'0001', N'', N'', N'', N'2. LĐHĐ', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:34.457' AS DateTime), NULL, 0, NULL, NULL, NULL, N'9eb683c3-3d96-44c9-9b10-4f947da34008', N'30fb2fc1-3a2d-4e69-bfa4-3cf1e164bdb8', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'c831e6bd-8e01-49da-91df-be4ba5b2fbdc', N'9010002-010-011-0003-0001-0007-00', N'9010002', N'010', N'011', N'0003', N'0001', N'0007', N'00', N'', N'- Chi hỗ Trợ phòng người (người)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:44.337' AS DateTime), NULL, 0, NULL, NULL, NULL, N'8dd7347a-25eb-4be1-b097-c0c6214a40e3', N'2d5d9fc0-6e68-4f70-834c-3ef277099bf6', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'7744bbc7-c8a3-4cb0-83a2-beab508a50c0', N'9040001', N'9040001', N'', N'', N'', N'', N'', N'', N'', N'Kinh phí KCB BHYT quân nhân', 0, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:38.537' AS DateTime), N'SLNS', 0, NULL, NULL, NULL, N'6173fe68-9e45-4c21-92a3-309afb77f73e', N'd06ce6a9-2a2e-4a7b-bad9-a88c61ac4cfe', 2023, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'782fc464-8bbb-4fa5-8928-bf148df1a25b', N'9010004-010-011-0010-0003', N'9010004', N'010', N'011', N'0010', N'0003', N'', N'', N'', N'- Dụng cụ ý tế', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:41.863' AS DateTime), NULL, 0, NULL, NULL, NULL, N'f95d9588-9394-4f73-a57f-23a464fdd002', N'73357a2d-1879-452f-a85f-836a3090a537', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'8b8b86d3-e4ea-40fc-82df-bf5c01192838', N'9010003-010-011-6600-6601', N'9010003', N'010', N'011', N'6600', N'6601', N'', N'', N'', N'Cước phí điện thoại (không bao gồm khoán điện thoại); thuê bao đường điện thoại; fax', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:50.377' AS DateTime), NULL, 0, NULL, NULL, NULL, N'943d7e3d-f8ff-4f8b-bab8-7a43dda8e71f', N'4ec9c459-3a9b-4106-a0bf-f0550e0ec73f', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'02474af3-b50c-4f44-8ea6-c0ac6edc7e38', N'9010002', N'9010002', N'', N'', N'', N'', N'', N'', N'', N'II. KHỐI HẠCH TOÁN', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:39.747' AS DateTime), NULL, 0, NULL, NULL, NULL, N'7a097777-89ac-4d76-a856-d1534c4070a9', N'18baf083-e5e0-4246-bc88-27eeb6ab28d4', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'7276cb8c-1830-4264-aba9-c17f0c26b8a5', N'9010003-010-011-6950', N'9010003', N'010', N'011', N'6950', N'', N'', N'', N'', N'Mua sắm tài sản dùng cho công tác chuyên môn ', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:22.790' AS DateTime), NULL, 0, NULL, NULL, NULL, N'87309c2d-5489-4e52-90c2-a97779e3b5c0', N'885f40fa-4c4d-4ef2-ab31-b075853b028f', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'f61cb578-263f-4c73-a3ea-c18e07db1a77', N'9010001', N'9010001', N'', N'', N'', N'', N'', N'', N'', N'I. KHỐI DỰ TOÁN', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:25.503' AS DateTime), NULL, 0, NULL, NULL, NULL, N'91465483-df1b-4262-9436-d87f8808cfac', N'18baf083-e5e0-4246-bc88-27eeb6ab28d4', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'8a5b44a8-041b-4030-a3c5-c388ee8c5f5d', N'9010002-010-011-0007', N'9010002', N'010', N'011', N'0007', N'', N'', N'', N'', N'7. Trợ cấp thôi việc', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:47.750' AS DateTime), NULL, 0, NULL, NULL, NULL, N'774d1074-d499-4f15-9ac0-40b04ad1ba17', N'7a097777-89ac-4d76-a856-d1534c4070a9', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'ad2938a2-4f89-4861-a62d-c3e2e6d96622', N'9010001-010-011-0001-0001-0001-02-01', N'9010001', N'010', N'011', N'0001', N'0001', N'0001', N'02', N'01', N'* Dưới 14 ngày/tháng(ngày)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:27.920' AS DateTime), NULL, 0, NULL, NULL, NULL, N'ba487675-57b4-4b66-a1f4-27d7b6fbb495', N'4ecb27b3-f09b-4972-9d22-965886faab0a', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'70f0542f-497c-4b29-b7f3-c40a42a75587', N'9010002-010-011-0003-0001-0004-00', N'9010002', N'010', N'011', N'0003', N'0001', N'0004', N'00', N'', N'- Trợ cấp phục hồi chức năng (người)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:43.653' AS DateTime), NULL, 0, NULL, NULL, NULL, N'46c2b17e-c44e-43df-9c08-ce6f6ffbf415', N'2d5d9fc0-6e68-4f70-834c-3ef277099bf6', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'c741d3c5-d220-4b4d-8119-c7a35195f213', N'9010003-010-011-6600-6608', N'9010003', N'010', N'011', N'6600', N'6608', N'', N'', N'', N'Phim ảnh; ấn phẩm truyền thông; sách báo, tạp chí, thư viện ', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:51.073' AS DateTime), NULL, 0, NULL, NULL, NULL, N'6ab29c1d-3216-4124-b989-42946c7cec46', N'4ec9c459-3a9b-4106-a0bf-f0550e0ec73f', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'c64c737d-0cc6-41f7-9381-c80aa579be97', N'9030002-010-011-0000', N'9030002', N'010', N'011', N'0000', N'', N'', N'', N'', N'1. Khối dự toán', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:37.127' AS DateTime), NULL, 0, NULL, NULL, NULL, N'49f6be60-0af9-41f8-8a0e-cac8b9b2dcfe', N'9ad55244-03c4-4c97-ba26-27af54495842', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'3596b144-9f8e-4cdb-9a08-c95aa6841e24', N'902', N'902', N'', N'', N'', N'', N'', N'', N'', N'Thu BHXH, BHYT, BHTN', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:40.757' AS DateTime), NULL, 0, NULL, NULL, NULL, N'f2bf9995-7b34-4d49-b973-3e135bf6e28a', N'25aa0569-2843-4e00-bd95-b48558a6e028', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'e4f3064f-07bb-48c6-a418-ce82171ffff2', N'9010001-010-011-0003-0001-0005-00', N'9010001', N'010', N'011', N'0003', N'0001', N'0005', N'00', N'', N'- Trợ cấp phục vụ (người)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:32.960' AS DateTime), NULL, 0, NULL, NULL, NULL, N'e981466a-d098-40b2-be26-78989d31bd5c', N'c7647e43-8aca-4882-98f4-3ec7e0bc8714', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'f014279d-6932-4ee5-85ea-d09f05714609', N'9010003-010-011-6700', N'9010003', N'010', N'011', N'6700', N'', N'', N'', N'', N'Công tác phí', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:20.353' AS DateTime), NULL, 0, NULL, NULL, NULL, N'd9c3f3ea-9ba6-4767-8691-c9eee1d2b0aa', N'885f40fa-4c4d-4ef2-ab31-b075853b028f', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'6b5b908d-bc68-45fc-b1ea-d1006e4f59eb', N'9010002-010-011-0006-0001-0001-00', N'9010002', N'010', N'011', N'0006', N'0001', N'0001', N'00', N'', N'- Trợ cấp XN 1 lần (người)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:47.160' AS DateTime), NULL, 0, NULL, NULL, NULL, N'fd8cb880-dd35-4db5-a71d-e47c97168672', N'd8fd23af-c23c-4b8f-8858-05fe8e304970', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'8a6e7345-4c7a-41ad-b658-d1996de7fd0d', N'9030001', N'9030001', N'', N'', N'', N'', N'', N'', N'', N'I. Bảo hiểm y tế thân nhân quân nhân', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:34.690' AS DateTime), NULL, 0, NULL, NULL, NULL, N'4c52e8ab-654e-4b86-afbc-165823b677a2', N'5a1234af-171a-44c9-98ed-9da7deff0188', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'786d1d76-9771-4f7a-8fa3-d22f091b644e', N'905', N'905', N'', N'', N'', N'', N'', N'', N'', N'Kinh phí CSSK ban đầu NLDĐ và HSSV', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:41.117' AS DateTime), NULL, 0, NULL, NULL, NULL, N'2cdf8f93-5d04-45f8-afcc-5100068321e4', N'25aa0569-2843-4e00-bd95-b48558a6e028', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'fc7cf8fc-11e4-479e-b204-d2723c22155f', N'9020002-010-011-0001-0001', N'9020002', N'012', N'011', N'0001', N'0001', N'', N'', N'', N'2. QNCN', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:33.560' AS DateTime), NULL, 0, NULL, NULL, NULL, N'e89cbafd-a9b0-4b68-890b-d1d7db5b4824', N'8639c115-bdb3-49d5-9018-a1c9a3d24896', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'c0a4ed2c-1c85-4980-a97c-d31d002878e0', N'9010002-010-011-0004', N'9010002', N'010', N'011', N'0004', N'', N'', N'', N'', N'4. Hưu trí', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:45.060' AS DateTime), NULL, 0, NULL, NULL, NULL, N'69183e88-7f92-473e-a228-47d369f839e7', N'7a097777-89ac-4d76-a856-d1534c4070a9', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'6a81c425-584b-4103-bc4d-d34438d9ac9b', N'9010003-010-011-7000-7001', N'9010003', N'010', N'011', N'7000', N'7001', N'', N'', N'', N'Chi phí hàng hóa, vật tư ', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:23.780' AS DateTime), NULL, 0, NULL, NULL, NULL, N'ea128ac8-2407-4705-a4ba-5fc7c3b0c26a', N'06c2e5e2-42f0-4ff6-a18c-bf19f1ed707d', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'725fcde9-1889-4a2d-95e2-d364047e3007', N'9010003-010-011-6550-6599', N'9010003', N'010', N'011', N'6550', N'6599', N'', N'', N'', N'Vật tư văn phòng khác', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:49.870' AS DateTime), NULL, 0, NULL, NULL, NULL, N'3a14e0af-be8c-4ee3-9279-afc0eb517462', N'2e93d30f-796f-4f33-938b-7d2dd2d38c3a', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'ae328d0c-8785-444c-a6e7-d59abaa5ddd8', N'9010004-010-011-0010-0000', N'9010004', N'010', N'011', N'0010', N'0000', N'', N'', N'', N'- Thuốc', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:41.410' AS DateTime), NULL, 0, NULL, NULL, NULL, N'443cbbb4-f504-43b2-ac0f-77acfeac84c2', N'ffb45fa2-8a4b-4918-ba6a-19d9c3da8044', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'a0a5429d-c91e-4551-9b37-d876defa3f84', N'9010001-010-011-0003-0001-0001-00', N'9010001', N'010', N'011', N'0003', N'0001', N'0001', N'00', N'', N'- Chi giám định mức suy giảm KNLĐ (người)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:32.103' AS DateTime), NULL, 0, NULL, NULL, NULL, N'b6c195d7-cf0e-4add-8961-b2224e9f21b4', N'c7647e43-8aca-4882-98f4-3ec7e0bc8714', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'adbf5a58-da5e-4818-9d5c-d8fab2dbb3db', N'9', N'9', N'', N'', N'', N'', N'', N'', N'', N'THU CHI BHXH, BHYT, BHTN', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:25.193' AS DateTime), NULL, 0, NULL, NULL, NULL, N'25aa0569-2843-4e00-bd95-b48558a6e028', NULL, 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'c2e1082b-40fe-485f-8017-d91d1884d9eb', N'9020002-010-011-0001-0000', N'9020002', N'011', N'011', N'0001', N'0000', N'', N'', N'', N'1. Sĩ quan', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:33.370' AS DateTime), NULL, 0, NULL, NULL, NULL, N'87d5e78c-2370-40b6-b7a8-ec285ed08504', N'8639c115-bdb3-49d5-9018-a1c9a3d24896', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'5a841aa4-82db-4823-91b6-d9301a818a8a', N'9010003-010-011-6700-6703', N'9010003', N'010', N'011', N'6700', N'6703', N'', N'', N'', N'Tiền thuê phòng ngủ', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:20.970' AS DateTime), NULL, 0, NULL, NULL, NULL, N'5d24dd88-913a-4eb5-b2e6-b0e767ea80a4', N'd9c3f3ea-9ba6-4767-8691-c9eee1d2b0aa', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'25f69737-54b1-4fcc-adaa-da38b5ee8589', N'9040002', N'9040002', N'', N'', N'', N'', N'', N'', N'', N'Kinh phí KCB BHYT thân nhân quân nhân và người lao động', 0, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:38.780' AS DateTime), N'SLNS', 0, NULL, NULL, NULL, N'503d8588-e537-4c72-a921-a437c1845d9e', N'd06ce6a9-2a2e-4a7b-bad9-a88c61ac4cfe', 2023, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'4ed79f6b-0ce0-4905-ace5-da97202c3a59', N'9020002-010-011-0002', N'9020002', N'014', N'011', N'0002', N'', N'', N'', N'', N'B. Người LĐ', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:34.047' AS DateTime), NULL, 0, NULL, NULL, NULL, N'30fb2fc1-3a2d-4e69-bfa4-3cf1e164bdb8', N'72d7a943-3288-4175-be6f-cf5190d2b908', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'fb40d996-464e-4350-a268-dbfc683371d9', N'9010002-010-011-0003-0001-0003-00', N'9010002', N'010', N'011', N'0003', N'0001', N'0003', N'00', N'', N'- Trợ cấp hàng tháng (người)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:43.483' AS DateTime), NULL, 0, NULL, NULL, NULL, N'0426ed87-f0f1-4142-8275-c21ed650e5a7', N'2d5d9fc0-6e68-4f70-834c-3ef277099bf6', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'8ebc395d-ea89-465e-962e-ddf1535fd59f', N'9020002', N'9020002', N'', N'', N'', N'', N'', N'', N'', N'I. KHỐI HẠCH TOÁN', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:32.730' AS DateTime), NULL, 0, NULL, NULL, NULL, N'72d7a943-3288-4175-be6f-cf5190d2b908', N'f2bf9995-7b34-4d49-b973-3e135bf6e28a', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'29393e19-e7d1-4f0a-8ad9-e02f128d3c8c', N'9010001-010-011-0008', N'9010001', N'010', N'011', N'0008', N'', N'', N'', N'', N'8. Tử tuất', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:36.280' AS DateTime), NULL, 0, NULL, NULL, NULL, N'f707d446-bee8-4299-b361-16ec2fb1d471', N'91465483-df1b-4262-9436-d87f8808cfac', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'3d58cbc4-f20c-48a6-9191-e1e94efa69e5', N'9010003-010-011-6600-6649', N'9010003', N'010', N'011', N'6600', N'6649', N'', N'', N'', N'Khác', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:51.610' AS DateTime), NULL, 0, NULL, NULL, NULL, N'd4edb565-5853-4cc0-8811-5fc92f1f306e', N'4ec9c459-3a9b-4106-a0bf-f0550e0ec73f', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'0473ec1a-ec2b-4614-8863-e23a1fca1f62', N'9010007', N'9010007', N'', N'', N'', N'', N'', N'', N'', N'II.Khối hạch toán ', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:29.097' AS DateTime), NULL, 0, NULL, NULL, NULL, N'a5a37e2d-c950-40cb-a00d-82ec68e4e832', N'18baf083-e5e0-4246-bc88-27eeb6ab28d4', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'78756830-1e46-48d0-92d3-e2cb14af4f97', N'9010002-010-011-0008-0001-0003-00', N'9010002', N'010', N'011', N'0008', N'0001', N'0003', N'00', N'', N'- Trợ cấp KV 1 lần (người)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:48.900' AS DateTime), NULL, 0, NULL, NULL, NULL, N'0f9b4ef0-dd90-4833-bbf5-ebff80e2a0a3', N'e56cedb7-ed03-485b-beab-c152f42ebadd', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'3b5c5f96-c258-4c8c-9629-e44756a1f9c7', N'9010002-010-011-0005', N'9010002', N'010', N'011', N'0005', N'', N'', N'', N'', N'5. Trợ cấp Phục viên', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:45.767' AS DateTime), NULL, 0, NULL, NULL, NULL, N'1ebce22c-8a98-40cf-bd20-d85a59e9c7bc', N'7a097777-89ac-4d76-a856-d1534c4070a9', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'f6b9c61e-9759-42e6-aa6c-e55bc41b3d8d', N'9010003-010-011-6700-6702', N'9010003', N'010', N'011', N'6700', N'6702', N'', N'', N'', N'Phụ cấp công tác phí', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:20.787' AS DateTime), NULL, 0, NULL, NULL, NULL, N'4436044d-d91e-4f5f-819d-f9aa1290e0f9', N'd9c3f3ea-9ba6-4767-8691-c9eee1d2b0aa', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'd27bd668-e88d-4816-b823-e57ed48fa55c', N'9010003-010-011-7750', N'9010003', N'010', N'011', N'7750', N'', N'', N'', N'', N'Chi khác', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:25.280' AS DateTime), NULL, 0, NULL, NULL, NULL, N'c60199d0-3566-4c28-91d5-cbde4cd4f792', N'885f40fa-4c4d-4ef2-ab31-b075853b028f', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'843ff3aa-de97-488d-8af6-e5aefdd770a5', N'9010002-010-011-0006', N'9010002', N'010', N'011', N'0006', N'', N'', N'', N'', N'6. Trợ cấp Xuất ngũ', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:46.940' AS DateTime), NULL, 0, NULL, NULL, NULL, N'd8fd23af-c23c-4b8f-8858-05fe8e304970', N'7a097777-89ac-4d76-a856-d1534c4070a9', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'842bb876-9dd3-46a6-a628-e6d754a2490f', N'9010002-010-011-0002-0001-0001-00-02', N'9010002', N'010', N'011', N'0002', N'0001', N'0001', N'00', N'02', N'+ Lao động nam(tháng)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:40.837' AS DateTime), NULL, 0, NULL, NULL, NULL, N'0cb5283f-1f2e-4c82-b7d9-9cab5c46c2cf', N'429c47a4-26ac-4bd3-97f1-7a89b917e6a1', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'4a1e0537-1530-4dfd-b64b-e6f93950c992', N'9010002-010-011-0001-0001', N'9010002', N'010', N'011', N'0001', N'0001', N'', N'', N'', N'- Bản thân ốm (ngày)', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:37.437' AS DateTime), NULL, 0, NULL, NULL, NULL, N'06a74e94-f3ba-4d73-8f80-9ae9563e1000', N'64e4b8a9-87f6-46f8-9e2b-7b32aa4f6f32', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'1581706d-87e2-450f-9fae-e76be52a6098', N'9010002-010-011-0003-0001-0002-00', N'9010002', N'010', N'011', N'0003', N'0001', N'0002', N'00', N'', N'- Trợ cấp 1 lần (người)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:43.320' AS DateTime), NULL, 0, NULL, NULL, NULL, N'171536b5-4867-40ab-a842-7284eeca8409', N'2d5d9fc0-6e68-4f70-834c-3ef277099bf6', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'f236eb63-5926-445d-84f8-e77b8b7481bd', N'9030001-010-011-0001-0001', N'9030001', N'015', N'011', N'0001', N'0001', N'', N'', N'', N'a. Bảo hiểm y tế thân nhân sĩ quan(thân nhân hàm CY)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:36.233' AS DateTime), NULL, 0, NULL, NULL, NULL, N'f2303978-ca39-4c7c-8ad1-2215f1d48fab', N'a1c989de-eac9-4153-8a60-a34ac387bb88', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'45379c3b-a3bb-4c00-8940-e82f05437c92', N'9010003-010-011-6650-6651', N'9010003', N'010', N'011', N'6650', N'6651', N'', N'', N'', N'In, mua tài liệu', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:52.217' AS DateTime), NULL, 0, NULL, NULL, NULL, N'3267c7d1-633c-4a4a-ac43-72be46a2d6ae', N'13a6bbc8-d2ba-4c81-9d29-a6c42bbd2a2b', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'4ba9e36a-dfdc-4db0-99cd-e937fe74f674', N'9010003-010-011-6650-6653', N'9010003', N'010', N'011', N'6650', N'6653', N'', N'', N'', N'Tiền vé máy bay, tàu xe (đối với đại biểu là khách', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:18.687' AS DateTime), NULL, 0, NULL, NULL, NULL, N'5fd8d523-3afd-45ff-85bc-80c96d602cb8', N'13a6bbc8-d2ba-4c81-9d29-a6c42bbd2a2b', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'9563c24b-9c92-4a71-b798-e961099e8dfb', N'9010007-010-011-0010-0003', N'9010007', N'014', N'011', N'0010', N'0003', N'', N'', N'', N'- Dụng cụ ý tế', 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:30.490' AS DateTime), N'STM', 0, NULL, NULL, NULL, N'dd5d6fc7-35a0-41b7-a997-dd04923cfc69', N'043d854e-bca5-4ee9-905a-8b78d15b9887', 2023, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, N'3,2', N'')
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'82d7b0c6-fb6e-4455-a37a-e9bc181a40b8', N'9010001-010-011-0003', N'9010001', N'010', N'011', N'0003', N'', N'', N'', N'', N'3. Tai nạn lao động, bệnh NN', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:31.820' AS DateTime), NULL, 0, NULL, NULL, NULL, N'c7647e43-8aca-4882-98f4-3ec7e0bc8714', N'91465483-df1b-4262-9436-d87f8808cfac', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'99881ae2-2230-418a-b37f-ec491d502039', N'9010003-010-011-7000-7049-0001', N'9010003', N'010', N'011', N'7000', N'7049', N'0001', N'', N'', N'- Chi hỗ Trợ cán bộ, nhân viên chuyên trách làm công tác BHXH, BHYT', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:24.427' AS DateTime), NULL, 0, NULL, NULL, NULL, N'302f226e-4c2d-4c58-895b-313e987860ce', N'a6aa1af6-475c-4853-836f-c0bde339c5b8', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'1f638c30-df3e-4b24-9e71-eca20fcb4a71', N'9010001-010-011-0001-0001', N'9010001', N'010', N'011', N'0001', N'0001', N'', N'', N'', N'- Bản thân ốm (ngày)', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:26.100' AS DateTime), NULL, 0, NULL, NULL, NULL, N'a655d431-de68-4238-b921-55850d8bba6b', N'63e1e32a-460b-4696-aa73-441934842ac0', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'91b521b4-3a37-49e6-8913-ed8105f3f40d', N'9010001-010-011-0001-0001-0001-01-01', N'9010001', N'010', N'011', N'0001', N'0001', N'0001', N'01', N'01', N'* Dưới 14 ngày/tháng(ngày)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:27.283' AS DateTime), NULL, 0, NULL, NULL, NULL, N'5c1357a4-f877-4c19-9bf6-f7c85e8c2f99', N'33eaed8b-c9a6-49aa-96ee-1a9652c99ce9', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'889123d0-fec8-48a2-8b69-ee50de36d325', N'9010002-010-011-0002-0001-0001-00-01', N'9010002', N'010', N'011', N'0002', N'0001', N'0001', N'00', N'01', N'+ Lao động nữ(tháng)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:40.443' AS DateTime), NULL, 0, NULL, NULL, NULL, N'aaa3dd36-bf5d-4615-9e90-83db2e3cde00', N'429c47a4-26ac-4bd3-97f1-7a89b917e6a1', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'96805403-fa30-4daf-8f1d-eea25c805513', N'9010003-010-011-6700-6704', N'9010003', N'010', N'011', N'6700', N'6704', N'', N'', N'', N'Khoán công tác phí', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:21.230' AS DateTime), NULL, 0, NULL, NULL, NULL, N'73da6baf-f315-4c97-ac06-346fe53255f0', N'd9c3f3ea-9ba6-4767-8691-c9eee1d2b0aa', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'4d69d8d6-9619-4d63-9951-f20636cc6dd1', N'9020002-010-011-0002-0000', N'9020002', N'015', N'011', N'0002', N'0000', N'', N'', N'', N'1. CC,CN, VCQP', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:34.297' AS DateTime), NULL, 0, NULL, NULL, NULL, N'3bdd2a48-b367-464b-b253-2406afc558f1', N'30fb2fc1-3a2d-4e69-bfa4-3cf1e164bdb8', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'7e304baa-c006-40c4-a68e-f21ffdd029ec', N'9010001-010-011-0002-0001-0001-00-02', N'9010001', N'010', N'011', N'0002', N'0001', N'0001', N'00', N'02', N'+ Lao động nam(tháng)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:29.623' AS DateTime), NULL, 0, NULL, NULL, NULL, N'e23d1638-f729-4e97-82c3-62a1c02fd5a3', N'232a6a14-37ff-40d7-9753-21a3f0131208', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'd07f5aee-fb26-431c-9f86-f2f97fe31140', N'9010005-010-011-0010-0000', N'9010005', N'011', N'011', N'0010', N'0000', N'', N'', N'', N'- Thuốc', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:27.200' AS DateTime), NULL, 0, NULL, NULL, NULL, N'8795dd68-21ff-4a22-b857-bec3d01f77cf', N'29147692-3954-4a2b-af22-bcbdb4dbc4ba', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'b55acbcb-e9fc-48fa-a089-f59ebc5bdbcb', N'9010005-010-011-0010-0003', N'9010005', N'014', N'011', N'0010', N'0003', N'', N'', N'', N'- Dụng cụ ý tế', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:27.753' AS DateTime), NULL, 0, NULL, NULL, NULL, N'3d2fc884-12a6-45b6-8ecc-a2744f87ef34', N'29147692-3954-4a2b-af22-bcbdb4dbc4ba', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'a7d6008f-7291-4cb0-b240-f6c112995744', N'9010002-010-011-0002-0001-0002-00', N'9010002', N'010', N'011', N'0002', N'0001', N'0002', N'00', N'', N'- Trợ cấp 1 lần (người)', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:41.190' AS DateTime), NULL, 0, NULL, NULL, NULL, N'9d0f9113-0bbc-4b8d-8e9b-60e41686b5ff', N'6f830377-5b2f-44d8-9a64-9d46c4270a80', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'57e6f4bc-0734-4de5-a1df-f9533363a194', N'9010001-010-011-0006-0001-0001-00', N'9010001', N'010', N'011', N'0006', N'0001', N'0001', N'00', N'', N'- Trợ cấp XN 1 lần (người)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:35.400' AS DateTime), NULL, 0, NULL, NULL, NULL, N'08c2bc4d-af7e-406e-83f3-74f9a1a353d1', N'a0d4eadb-0deb-45a6-b3fc-2f00cebd9613', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'9140113e-213b-4747-b57d-faa96855ab4e', N'9010002-010-011-0005-0001-0002-00', N'9010002', N'010', N'011', N'0005', N'0001', N'0002', N'00', N'', N'- Trợ cấp KV 1 lần (người)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:46.663' AS DateTime), NULL, 0, NULL, NULL, NULL, N'f9a5e84d-2048-4acc-9028-163e4a23a8f0', N'1ebce22c-8a98-40cf-bd20-d85a59e9c7bc', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'cfd8535a-8f45-478c-8fdb-fb7ad163e76e', N'9010001-010-011-0008-0001-0003-00', N'9010001', N'010', N'011', N'0008', N'0001', N'0003', N'00', N'', N'- Trợ cấp KV 1 lần (người)', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:36.913' AS DateTime), NULL, 0, NULL, NULL, NULL, N'ebc5a51a-320c-4466-8a04-bfeb88e3c881', N'f707d446-bee8-4299-b361-16ec2fb1d471', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap]) VALUES (N'ff8983fa-14be-48d3-b780-fbfaf17bd1b9', N'9010003-010-011-6950-6956', N'9010003', N'010', N'011', N'6950', N'6956', N'', N'', N'', N'Các thiết bị công nghệ thông tin ', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:23.080' AS DateTime), NULL, 0, NULL, NULL, NULL, N'5da67169-8f6e-41e6-a6e4-85d9430d8d2a', N'87309c2d-5489-4e52-90c2-a97779e3b5c0', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL)
GO
