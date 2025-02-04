/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_tnld]    Script Date: 1/16/2024 5:43:24 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_luong_tro_cap_tnld]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_tnld]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_tnld]    Script Date: 1/16/2024 5:43:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_tnld] 
	@DsMaDonVi nvarchar(100),
	@NamLamViec int,
	@Thang int,
	@DonViTinh int
AS
BEGIN
	DECLARE @MaCheDo nvarchar(1000) = 'CHIGIAMDINH,TAINANLD_TROCAP1LAN,TROCAPTHEOPHIEUTRUYTRA,TROCAPHANGTHANG,TROCAPPHCN,TROCAPPHUCVU,TROCAPCHETDOTNLD,TAINANLĐ_DUONGSUCPHSK,CHIGIAMDINH_TRUYLINH,TNLD_TROCAP1LAN_TRUYLINH,TROCAPHANGTHANG_TRUYLINH,TROCAPPHCN_TRUYLINH,TROCAPPHUCVU_TRUYLINH,TROCAPCHETDOTNLD_TRUYLINH,TNLD_DUONGSUCPHSK_TRUYLINH,HOTRO_CDNN_TRUYLINH,HOTRO_PHONGNGUA_TRUYLINH'
	--Lay thong tin luong theo tro cap tai nan lao dong
	select * into TBL_TCTNLD from
	(select donvi.Ma_DonVi, donvi.Ten_DonVi, luong.*
	from TL_BangLuong_ThangBHXH luong
	join TL_DM_DonVi donvi on luong.sMaDonVi = donvi.Ma_DonVi
	where 
	luong.sMaDonVi in (SELECT * FROM f_split(@DsMaDonVi))
	and luong.iNam = @NamLamViec
	and luong.iThang = @Thang
	and luong.sMaCheDo in (select * from splitstring(@MaCheDo))) tctnld

	select distinct 
		TBL_TCTNLD.sMaCB,
		TBL_TCTNLD.sMaCBo,
		--TBL_TCTNLD.sMaCheDo,
		TBL_TCTNLD.sTenCbo,
		TBL_TCTNLD.Ma_DonVi,
		TBL_TCTNLD.Ten_DonVi,
		CHIGIAMDINH.nGiaTri fChiGiamDinh,
		TROCAP1LAN.nGiaTri fTroCap1Lan,
		TROCAPTHEOPHIEUTRUYTRA.nGiaTri fTroCapTheoPhieuTruyTra,
		TROCAPHANGTHANG.nGiaTri fTroCapHangThang,
		TROCAPPHCN.nGiaTri fTroCapPHCN,
		TROCAPCHETDOTNLD.nGiaTri fTroCapChetDoTNLD,
		TAINANLĐ_DUONGSUCPHSK.SoNgayDuongSucTNLD,
		TAINANLĐ_DUONGSUCPHSK.nGiaTri fDuongSucTNLD,
		chedocha.sSoQuyetDinh,
		chedocha.dNgayQuyetDinh,
		CHIGIAMDINH_TL.nGiaTri fChiGiamDinhTruyLinh,
		TROCAP1LAN_TL.nGiaTri fTroCap1LanTruyLinh,
		HOTRO_CDNN_TRUYLINH_TL.nGiaTri fHoTroCdnnTruyLinh,
		TROCAPHANGTHANG_TL.nGiaTri fTroCapHangThangTruyLinh,
		TROCAPPHCN_TL.nGiaTri fTroCapPHCNTruyLinh,
		TROCAPCHETDOTNLD_TL.nGiaTri fTroCapChetDoTNLDTruyLinh,
		TAINANLĐ_DUONGSUCPHSK_TL.SoNgayDuongSucTNLD SoNgayDuongSucTNLDTruyLinh,
		TAINANLĐ_DUONGSUCPHSK_TL.nGiaTri fDuongSucTNLDTruyLinh
		into TBL_TCTNLD_DOC
	from TBL_TCTNLD TBL_TCTNLD
		left join (select top 1 * from TL_CanBo_CheDoBHXH 
		where sMaCheDo in (select * from splitstring(@MaCheDo))
		and ((sSoQuyetDinh is not null and sSoQuyetDinh <> '') 
		or (dNgayQuyetDinh is not null and dNgayQuyetDinh <> ''))) chedocha
		on TBL_TCTNLD.sMaCBo = chedocha.sMaCanBo
		left join
		(select tnld.sMaDonVi, tnld.nGiaTri, tnld.sMaCB, tnld.sMaCBo, tnld.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTroCap1Lan
		from TBL_TCTNLD tnld left join TL_CanBo_CheDoBHXH chedo on tnld.sMaCBo = chedo.sMaCanBo and tnld.sMaCheDo = chedo.sMaCheDo
		where tnld.sMaCheDo = 'TAINANLD_TROCAP1LAN') TROCAP1LAN
		on TBL_TCTNLD.sMaCBo = TROCAP1LAN.sMaCBo and TBL_TCTNLD.sMaDonVi = TROCAP1LAN.sMaDonVi
		left join
		(select tnld_1.sMaDonVi, tnld_1.nGiaTri, tnld_1.sMaCB, tnld_1.sMaCBo, tnld_1.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTroCapTheoPhieuTruyTra
		from TBL_TCTNLD tnld_1 left join TL_CanBo_CheDoBHXH chedo on tnld_1.sMaCBo = chedo.sMaCanBo and tnld_1.sMaCheDo = chedo.sMaCheDo
		where tnld_1.sMaCheDo = 'TROCAPTHEOPHIEUTRUYTRA') TROCAPTHEOPHIEUTRUYTRA
		on TBL_TCTNLD.sMaCBo = TROCAPTHEOPHIEUTRUYTRA.sMaCBo and TBL_TCTNLD.sMaDonVi = TROCAPTHEOPHIEUTRUYTRA.sMaDonVi
		left join
		(select tnld_2.sMaDonVi, tnld_2.nGiaTri, tnld_2.sMaCB, tnld_2.sMaCBo, tnld_2.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTroCapHangThang
		from TBL_TCTNLD tnld_2 left join TL_CanBo_CheDoBHXH chedo on tnld_2.sMaCBo = chedo.sMaCanBo and tnld_2.sMaCheDo = chedo.sMaCheDo
		where tnld_2.sMaCheDo = 'TROCAPHANGTHANG') TROCAPHANGTHANG
		on TBL_TCTNLD.sMaCBo = TROCAPHANGTHANG.sMaCBo and TBL_TCTNLD.sMaDonVi = TROCAPHANGTHANG.sMaDonVi
		left join
		(select tnld_3.sMaDonVi, sum(tnld_3.nGiaTri) nGiaTri, tnld_3.sMaCB, tnld_3.sMaCBo, tnld_3.sTenCbo, sum(chedo.fSoNgayHuongBHXH) SoNgayTroCapPHCN
		from TBL_TCTNLD tnld_3 left join TL_CanBo_CheDoBHXH chedo on tnld_3.sMaCBo = chedo.sMaCanBo and tnld_3.sTenCbo = chedo.sMaCheDo
		where tnld_3.sMaCheDo in ('TROCAPPHCN', 'TROCAPPHUCVU')
		group by tnld_3.sMaDonVi, tnld_3.sMaCB, tnld_3.sMaCBo, tnld_3.sTenCbo) TROCAPPHCN
		on TBL_TCTNLD.sMaCBo = TROCAPPHCN.sMaCBo and TBL_TCTNLD.sMaDonVi = TROCAPPHCN.sMaDonVi
		left join
		(select tnld_4.sMaDonVi, tnld_4.nGiaTri, tnld_4.sMaCB, tnld_4.sMaCBo, tnld_4.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTroCapChetDoTNLD
		from TBL_TCTNLD tnld_4 left join TL_CanBo_CheDoBHXH chedo on tnld_4.sMaCBo = chedo.sMaCanBo and tnld_4.sMaCheDo = chedo.sMaCheDo
		where tnld_4.sMaCheDo = 'TROCAPCHETDOTNLD') TROCAPCHETDOTNLD
		on TBL_TCTNLD.sMaCBo = TROCAPCHETDOTNLD.sMaCBo and TBL_TCTNLD.sMaDonVi = TROCAPCHETDOTNLD.sMaDonVi
		left join
		(select tnld_5.sMaDonVi, tnld_5.nGiaTri, tnld_5.sMaCB, tnld_5.sMaCBo, tnld_5.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayDuongSucTNLD
		from TBL_TCTNLD tnld_5 left join TL_CanBo_CheDoBHXH chedo on tnld_5.sMaCBo = chedo.sMaCanBo and tnld_5.sMaCheDo = chedo.sMaCheDo
		where tnld_5.sMaCheDo = 'TAINANLĐ_DUONGSUCPHSK') TAINANLĐ_DUONGSUCPHSK
		on TBL_TCTNLD.sMaCBo = TAINANLĐ_DUONGSUCPHSK.sMaCBo and TBL_TCTNLD.sMaDonVi = TAINANLĐ_DUONGSUCPHSK.sMaDonVi
		left join
		(select tnld_6.sMaDonVi, tnld_6.nGiaTri, tnld_6.sMaCB, tnld_6.sMaCBo, tnld_6.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayCHIGIAMDINH
		from TBL_TCTNLD tnld_6 left join TL_CanBo_CheDoBHXH chedo on tnld_6.sMaCBo = chedo.sMaCanBo and tnld_6.sMaCheDo = chedo.sMaCheDo
		where tnld_6.sMaCheDo = 'CHIGIAMDINH') CHIGIAMDINH
		on TBL_TCTNLD.sMaCBo = CHIGIAMDINH.sMaCBo and TBL_TCTNLD.sMaDonVi = CHIGIAMDINH.sMaDonVi

		-- TRUYLINH
		left join
		(select tnld.sMaDonVi, tnld.nGiaTri, tnld.sMaCB, tnld.sMaCBo, tnld.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTroCap1Lan
		from TBL_TCTNLD tnld left join TL_CanBo_CheDoBHXH chedo on tnld.sMaCBo = chedo.sMaCanBo and tnld.sMaCheDo = chedo.sMaCheDo
		where tnld.sMaCheDo = 'TNLD_TROCAP1LAN_TRUYLINH ') TROCAP1LAN_TL
		on TBL_TCTNLD.sMaCBo = TROCAP1LAN_TL.sMaCBo and TBL_TCTNLD.sMaDonVi = TROCAP1LAN_TL.sMaDonVi
		left join
		(select tnld_1.sMaDonVi, tnld_1.nGiaTri, tnld_1.sMaCB, tnld_1.sMaCBo, tnld_1.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTroCapTheoPhieuTruyTra
		from TBL_TCTNLD tnld_1 left join TL_CanBo_CheDoBHXH chedo on tnld_1.sMaCBo = chedo.sMaCanBo and tnld_1.sMaCheDo = chedo.sMaCheDo
		where tnld_1.sMaCheDo = 'HOTRO_CDNN_TRUYLINH ') HOTRO_CDNN_TRUYLINH_TL
		on TBL_TCTNLD.sMaCBo = HOTRO_CDNN_TRUYLINH_TL.sMaCBo and TBL_TCTNLD.sMaDonVi = HOTRO_CDNN_TRUYLINH_TL.sMaDonVi
		left join
		(select tnld_2.sMaDonVi, tnld_2.nGiaTri, tnld_2.sMaCB, tnld_2.sMaCBo, tnld_2.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTroCapHangThang
		from TBL_TCTNLD tnld_2 left join TL_CanBo_CheDoBHXH chedo on tnld_2.sMaCBo = chedo.sMaCanBo and tnld_2.sMaCheDo = chedo.sMaCheDo
		where tnld_2.sMaCheDo = 'TROCAPHANGTHANG_TRUYLINH') TROCAPHANGTHANG_TL
		on TBL_TCTNLD.sMaCBo = TROCAPHANGTHANG_TL.sMaCBo and TBL_TCTNLD.sMaDonVi = TROCAPHANGTHANG_TL.sMaDonVi
		left join
		(select tnld_3.sMaDonVi, sum(tnld_3.nGiaTri) nGiaTri, tnld_3.sMaCB, tnld_3.sMaCBo, tnld_3.sTenCbo, sum(chedo.fSoNgayHuongBHXH) SoNgayTroCapPHCN
		from TBL_TCTNLD tnld_3 left join TL_CanBo_CheDoBHXH chedo on tnld_3.sMaCBo = chedo.sMaCanBo and tnld_3.sTenCbo = chedo.sMaCheDo
		where tnld_3.sMaCheDo in ('TROCAPPHCN_TRUYLINH', 'TROCAPPHUCVU_TRUYLINH')
		group by tnld_3.sMaDonVi, tnld_3.sMaCB, tnld_3.sMaCBo, tnld_3.sTenCbo) TROCAPPHCN_TL
		on TBL_TCTNLD.sMaCBo = TROCAPPHCN_TL.sMaCBo and TBL_TCTNLD.sMaDonVi = TROCAPPHCN_TL.sMaDonVi
		left join
		(select tnld_4.sMaDonVi, tnld_4.nGiaTri, tnld_4.sMaCB, tnld_4.sMaCBo, tnld_4.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTroCapChetDoTNLD
		from TBL_TCTNLD tnld_4 left join TL_CanBo_CheDoBHXH chedo on tnld_4.sMaCBo = chedo.sMaCanBo and tnld_4.sMaCheDo = chedo.sMaCheDo
		where tnld_4.sMaCheDo = 'TROCAPCHETDOTNLD_TRUYLINH') TROCAPCHETDOTNLD_TL
		on TBL_TCTNLD.sMaCBo = TROCAPCHETDOTNLD_TL.sMaCBo and TBL_TCTNLD.sMaDonVi = TROCAPCHETDOTNLD_TL.sMaDonVi
		left join
		(select tnld_5.sMaDonVi, tnld_5.nGiaTri, tnld_5.sMaCB, tnld_5.sMaCBo, tnld_5.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayDuongSucTNLD
		from TBL_TCTNLD tnld_5 left join TL_CanBo_CheDoBHXH chedo on tnld_5.sMaCBo = chedo.sMaCanBo and tnld_5.sMaCheDo = chedo.sMaCheDo
		where tnld_5.sMaCheDo = 'TNLD_DUONGSUCPHSK_TRUYLINH ') TAINANLĐ_DUONGSUCPHSK_TL
		on TBL_TCTNLD.sMaCBo = TAINANLĐ_DUONGSUCPHSK_TL.sMaCBo and TBL_TCTNLD.sMaDonVi = TAINANLĐ_DUONGSUCPHSK_TL.sMaDonVi
		left join
		(select tnld_6.sMaDonVi, tnld_6.nGiaTri, tnld_6.sMaCB, tnld_6.sMaCBo, tnld_6.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayCHIGIAMDINH
		from TBL_TCTNLD tnld_6 left join TL_CanBo_CheDoBHXH chedo on tnld_6.sMaCBo = chedo.sMaCanBo and tnld_6.sMaCheDo = chedo.sMaCheDo
		where tnld_6.sMaCheDo = 'CHIGIAMDINH_TRUYLINH') CHIGIAMDINH_TL
		on TBL_TCTNLD.sMaCBo = CHIGIAMDINH_TL.sMaCBo and TBL_TCTNLD.sMaDonVi = CHIGIAMDINH_TL.sMaDonVi

	--Lấy lương Sĩ quan
	select * into TBL_TCTNLD_SQ from
	(select
		1 bHangCha, 1 RowNum, 'I' STT, 'SQ' DoiTuong, 'SQ' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'SQ' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null SoNgayDuongSucTNLD, null fChiGiamDinh, null fTroCap1Lan, null fTroCapTheoPhieuTruyTra, null fTroCapHangThang, null fTroCapPHCN, null fTroCapChetDoTNLD, null fDuongSucTNLD, 0 bHasData,
		null SoNgayDuongSucTNLDTruyLinh, null fChiGiamDinhTruyLinh, null fTroCap1LanTruyLinh, null fHoTroCdnnTruyLinh, null fTroCapHangThangTruyLinh, null fTroCapPHCNTruyLinh, null fTroCapChetDoTNLDTruyLinh, null fDuongSucTNLDTruyLinh
	union all
	select
		0 bHangCha, 1 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'SQ' LoaiDoiTuong, Ma_DonVi,  Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fTroCapChetDoTNLD, fDuongSucTNLD,0 bHasData,
		SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh
	from TBL_TCTNLD_DOC
	where sMaCB like '1%' and (isnull(fChiGiamDinh, 0) <> 0 or isnull(fTroCap1Lan, 0) <> 0 or isnull(fTroCapTheoPhieuTruyTra, 0) <> 0 or isnull(fTroCapHangThang, 0) <> 0 or isnull(fTroCapPHCN, 0) <> 0 or isnull(fTroCapChetDoTNLD, 0) <> 0 or isnull(fDuongSucTNLD, 0) <> 0) OR
				ISNULL(fChiGiamDinhTruyLinh,0) <> 0 OR ISNULL(fTroCap1LanTruyLinh,0) <> 0 OR ISNULL(fHoTroCdnnTruyLinh,0) <> 0 OR ISNULL(fTroCapHangThangTruyLinh,0) <> 0 OR
				ISNULL( fTroCapPHCNTruyLinh,0) <> 0 OR ISNULL(fTroCapChetDoTNLDTruyLinh,0) <> 0 OR ISNULL( fDuongSucTNLDTruyLinh, 0) <> 0
		) sq
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCTNLD_SQ) > 1
		update TBL_TCTNLD_SQ set bHasData = 1

	--Lấy lương QNCN
	select * into TBL_TCTNLD_QNCN from
	(select
		1 bHangCha, 2 RowNum, 'II' STT, 'QNCN' DoiTuong, 'QNCN' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'QNCN' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null SoNgayDuongSucTNLD, null fChiGiamDinh, null fTroCap1Lan, null fTroCapTheoPhieuTruyTra, null fTroCapHangThang, null fTroCapPHCN, null fTroCapChetDoTNLD, null fDuongSucTNLD, 0 bHasData,
		null SoNgayDuongSucTNLDTruyLinh, null fChiGiamDinhTruyLinh, null fTroCap1LanTruyLinh, null fHoTroCdnnTruyLinh, null fTroCapHangThangTruyLinh, null fTroCapPHCNTruyLinh, null fTroCapChetDoTNLDTruyLinh, null fDuongSucTNLDTruyLinh
	union all
	select
		0 bHangCha, 2 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'QNCN' LoaiDoiTuong, Ma_DonVi,  Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fTroCapChetDoTNLD, fDuongSucTNLD, 0 bHasData,
		SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh
	from TBL_TCTNLD_DOC
	where sMaCB like '2%' and (isnull(fChiGiamDinh, 0) <> 0 or isnull(fTroCap1Lan, 0) <> 0 or isnull(fTroCapTheoPhieuTruyTra, 0) <> 0 or isnull(fTroCapHangThang, 0) <> 0 or isnull(fTroCapPHCN, 0) <> 0 or isnull(fTroCapChetDoTNLD, 0) <> 0 or isnull(fDuongSucTNLD, 0) <> 0)) qncn
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCTNLD_QNCN) > 1
		update TBL_TCTNLD_QNCN set bHasData = 1

	--Lấy lương HSQ_BS
	select * into TBL_TCTNLD_HSQBS from
	(select
		1 bHangCha, 3 RowNum, 'III' STT, 'HSQ_BS' DoiTuong, 'HSQ_BS' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'HSQ_BS' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null SoNgayDuongSucTNLD, null fChiGiamDinh, null fTroCap1Lan, null fTroCapTheoPhieuTruyTra, null fTroCapHangThang, null fTroCapPHCN, null fTroCapChetDoTNLD, null fDuongSucTNLD, 0 bHasData,
		null SoNgayDuongSucTNLDTruyLinh, null fChiGiamDinhTruyLinh, null fTroCap1LanTruyLinh, null fHoTroCdnnTruyLinh, null fTroCapHangThangTruyLinh, null fTroCapPHCNTruyLinh, null fTroCapChetDoTNLDTruyLinh, null fDuongSucTNLDTruyLinh
	union all
	select
		0 bHangCha, 3 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'HSQ_BS' LoaiDoiTuong, Ma_DonVi,  Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fTroCapChetDoTNLD, fDuongSucTNLD, 0 bHasData,
		SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh
	from TBL_TCTNLD_DOC
	where sMaCB like '0%' and (isnull(fChiGiamDinh, 0) <> 0 or isnull(fTroCap1Lan, 0) <> 0 or isnull(fTroCapTheoPhieuTruyTra, 0) <> 0 or isnull(fTroCapHangThang, 0) <> 0 or isnull(fTroCapPHCN, 0) <> 0 or isnull(fTroCapChetDoTNLD, 0) <> 0 or isnull(fDuongSucTNLD, 0) <> 0) OR
				ISNULL(fChiGiamDinhTruyLinh,0) <> 0 OR ISNULL(fTroCap1LanTruyLinh,0) <> 0 OR ISNULL(fHoTroCdnnTruyLinh,0) <> 0 OR ISNULL(fTroCapHangThangTruyLinh,0) <> 0 OR
				ISNULL( fTroCapPHCNTruyLinh,0) <> 0 OR ISNULL(fTroCapChetDoTNLDTruyLinh,0) <> 0 OR ISNULL( fDuongSucTNLDTruyLinh, 0) <> 0
		)  hsqbs
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCTNLD_HSQBS) > 1
		update TBL_TCTNLD_HSQBS set bHasData = 1

	--Lấy lương VCQP
	select * into TBL_TCTNLD_VCQP from
	(select
		1 bHangCha, 4 RowNum, 'IV' STT, 'VCQP' DoiTuong, 'VCQP' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'VCQP' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null SoNgayDuongSucTNLD, null fChiGiamDinh, null fTroCap1Lan, null fTroCapTheoPhieuTruyTra, null fTroCapHangThang, null fTroCapPHCN, null fTroCapChetDoTNLD, null fDuongSucTNLD, 0 bHasData,
		null SoNgayDuongSucTNLDTruyLinh, null fChiGiamDinhTruyLinh, null fTroCap1LanTruyLinh, null fHoTroCdnnTruyLinh, null fTroCapHangThangTruyLinh, null fTroCapPHCNTruyLinh, null fTroCapChetDoTNLDTruyLinh, null fDuongSucTNLDTruyLinh
	union all
	select
		0 bHangCha, 4 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'VCQP' LoaiDoiTuong, Ma_DonVi,  Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fTroCapChetDoTNLD, fDuongSucTNLD, 0 bHasData,
		SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh
	from TBL_TCTNLD_DOC
	where sMaCB in ('3.1', '3.2', '3.3') and (isnull(fChiGiamDinh, 0) <> 0 or isnull(fTroCap1Lan, 0) <> 0 or isnull(fTroCapTheoPhieuTruyTra, 0) <> 0 or isnull(fTroCapHangThang, 0) <> 0 or isnull(fTroCapPHCN, 0) <> 0 or isnull(fTroCapChetDoTNLD, 0) <> 0 or isnull(fDuongSucTNLD, 0) <> 0)) vcqp
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCTNLD_VCQP) > 1
		update TBL_TCTNLD_VCQP set bHasData = 1

	--Lấy lương Lao động hợp đông
	select * into TBL_TCTNLD_LDHD from
	(select
		1 bHangCha, 5 RowNum, 'V' STT, 'LDHD' DoiTuong, 'LDHD' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'LDHD' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null SoNgayDuongSucTNLD, null fChiGiamDinh, null fTroCap1Lan, null fTroCapTheoPhieuTruyTra, null fTroCapHangThang, null fTroCapPHCN, null fTroCapChetDoTNLD, null fDuongSucTNLD, 0 bHasData,
		null SoNgayDuongSucTNLDTruyLinh, null fChiGiamDinhTruyLinh, null fTroCap1LanTruyLinh, null fHoTroCdnnTruyLinh, null fTroCapHangThangTruyLinh, null fTroCapPHCNTruyLinh, null fTroCapChetDoTNLDTruyLinh, null fDuongSucTNLDTruyLinh
	union all
	select
		0 bHangCha, 5 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'LDHD' LoaiDoiTuong, Ma_DonVi,  Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fTroCapChetDoTNLD, fDuongSucTNLD, 0 bHasData,
		SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh
	from TBL_TCTNLD_DOC
	where sMaCB = '43' and (isnull(fChiGiamDinh, 0) <> 0 or isnull(fTroCap1Lan, 0) <> 0 or isnull(fTroCapTheoPhieuTruyTra, 0) <> 0 or isnull(fTroCapHangThang, 0) <> 0 or isnull(fTroCapPHCN, 0) <> 0 or isnull(fTroCapChetDoTNLD, 0) <> 0 or isnull(fDuongSucTNLD, 0) <> 0) OR
				ISNULL(fChiGiamDinhTruyLinh,0) <> 0 OR ISNULL(fTroCap1LanTruyLinh,0) <> 0 OR ISNULL(fHoTroCdnnTruyLinh,0) <> 0 OR ISNULL(fTroCapHangThangTruyLinh,0) <> 0 OR
				ISNULL( fTroCapPHCNTruyLinh,0) <> 0 OR ISNULL(fTroCapChetDoTNLDTruyLinh,0) <> 0 OR ISNULL( fDuongSucTNLDTruyLinh, 0) <> 0
		)  ldhd
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCTNLD_LDHD) > 1
		update TBL_TCTNLD_LDHD set bHasData = 1

	--Ket qua
	select result.* into TBL_TCTNLD_RESULT from
	(select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fTroCapChetDoTNLD, fDuongSucTNLD,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) fTongSoTienThangNay,
	SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh,
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) fTongSoTienTruyLinh,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) +
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) FTongSoTien
	from TBL_TCTNLD_SQ
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fTroCapChetDoTNLD, fDuongSucTNLD,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) fTongSoTienThangNay,
	SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh,
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) fTongSoTienTruyLinh,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) +
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) FTongSoTien
	from TBL_TCTNLD_QNCN
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fTroCapChetDoTNLD, fDuongSucTNLD,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) fTongSoTienThangNay,
	SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh,
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) fTongSoTienTruyLinh,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) +
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) FTongSoTien
	from TBL_TCTNLD_HSQBS
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fTroCapChetDoTNLD, fDuongSucTNLD,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) fTongSoTienThangNay,
	SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh,
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) fTongSoTienTruyLinh,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) +
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) FTongSoTien
	from TBL_TCTNLD_VCQP
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fTroCapChetDoTNLD, fDuongSucTNLD,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) fTongSoTienThangNay,
	SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh,
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) fTongSoTienTruyLinh,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) +
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) FTongSoTien
	from TBL_TCTNLD_LDHD) result

	select 
		RowNum,
		STT,
		DoiTuong, 
		LoaiDoiTuong,
		sMaCB MaCb, 
		sMaCBo MaCbo, 
		sTenCbo TenCbo,
		sSoQuyetDinh SSoQuyetDinh,
		dNgayQuyetDinh DNgayQuyetDinh,
		Ma_DonVi MaDonVi,
		TenDonVi,
		SoNgayDuongSucTNLD,
		fChiGiamDinh/@DonViTinh fChiGiamDinh,
		fTroCap1Lan/@DonViTinh fTroCap1Lan,
		fTroCapTheoPhieuTruyTra/@DonViTinh fTroCapTheoPhieuTruyTra,
		fTroCapHangThang/@DonViTinh fTroCapHangThang,
		fTroCapPHCN/@DonViTinh fTroCapPHCN,
		fTroCapChetDoTNLD/@DonViTinh fTroCapChetDoTNLD,
		fDuongSucTNLD/@DonViTinh fDuongSucTNLD,
		fTongSoTienThangNay/@DonViTinh fTongSoTienThangNay,
		bHangCha IsHangCha,
		bHasData IsHasData,
		SoNgayDuongSucTNLDTruyLinh,
		fChiGiamDinhTruyLinh/@DonViTinh fChiGiamDinhTruyLinh,
		fTroCap1LanTruyLinh/@DonViTinh fTroCap1LanTruyLinh,
		fHoTroCdnnTruyLinh/@DonViTinh fHoTroCdnnTruyLinh,
		fTroCapHangThangTruyLinh/@DonViTinh fTroCapHangThangTruyLinh,
		fTroCapPHCNTruyLinh/@DonViTinh fTroCapPHCNTruyLinh,
		fTroCapChetDoTNLDTruyLinh/@DonViTinh fTroCapChetDoTNLDTruyLinh,
		fDuongSucTNLDTruyLinh/@DonViTinh fDuongSucTNLDTruyLinh,
		fTongSoTienTruyLinh/@DonViTinh fTongSoTienTruyLinh,
		fTongSoTien/@DonViTinh fTongSoTien

	from TBL_TCTNLD_RESULT
	order by RowNum, LoaiDoiTuong, DoiTuong desc

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD]') AND type in (N'U'))
	drop table TBL_TCTNLD;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_DOC]') AND type in (N'U'))
	drop table TBL_TCTNLD_DOC;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_SQ]') AND type in (N'U'))
	drop table TBL_TCTNLD_SQ;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_QNCN]') AND type in (N'U'))
	drop table TBL_TCTNLD_QNCN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_HSQBS]') AND type in (N'U'))
	drop table TBL_TCTNLD_HSQBS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_VCQP]') AND type in (N'U'))
	drop table TBL_TCTNLD_VCQP;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_LDHD]') AND type in (N'U'))
	drop table TBL_TCTNLD_LDHD;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_RESULT]') AND type in (N'U'))
	drop table TBL_TCTNLD_RESULT;

END
;
;
;
;
;
GO
