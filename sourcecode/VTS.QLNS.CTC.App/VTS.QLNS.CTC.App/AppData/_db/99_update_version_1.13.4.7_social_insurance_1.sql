/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh]    Script Date: 11/9/2023 5:10:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_xuat_ngu]    Script Date: 11/9/2023 5:10:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_luong_tro_cap_xuat_ngu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_xuat_ngu]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_tnld]    Script Date: 11/9/2023 5:10:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_luong_tro_cap_tnld]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_tnld]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_thai_san]    Script Date: 11/9/2023 5:10:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_luong_tro_cap_thai_san]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_thai_san]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_om_dau]    Script Date: 11/9/2023 5:10:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_luong_tro_cap_om_dau]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_om_dau]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_ht_pv_tv_tt]    Script Date: 11/9/2023 5:10:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_luong_tro_cap_ht_pv_tv_tt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_ht_pv_tv_tt]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_chung_tu_don_vi_tong_hop]    Script Date: 11/9/2023 5:10:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_get_chung_tu_don_vi_tong_hop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_get_chung_tu_don_vi_tong_hop]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_chung_tu_don_vi]    Script Date: 11/9/2023 5:10:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_get_chung_tu_don_vi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_get_chung_tu_don_vi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_chung_tu_don_vi]    Script Date: 11/9/2023 5:10:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_bh_qtt_get_chung_tu_don_vi]
	@YearOfWork int,
	@LoaiTongHop int,
	@DaTongHop bit,
	@QuyNam int
AS
BEGIN
	SELECT
		qtt.iID_QTT_BHXH_ChungTu,
		qtt.sSoChungTu,
		qtt.iID_MaDonVi IIDMaDonVi,
		donvi.sTenDonVi,
		qtt.iQuyNam,
		qtt.iLoaiTongHop,
		qtt.bIsKhoa
	FROM BH_QTT_BHXH_ChungTu qtt
		LEFT JOIN DonVi donvi
		ON qtt.iID_MaDonVi = donvi.iID_MaDonVi
	WHERE donvi.iNamLamViec = @YearOfWork
		AND qtt.iNamLamViec = @YearOfWork
		AND qtt.bDaTongHop = @DaTongHop
		AND qtt.iQuyNam = @QuyNam
		AND qtt.iLoaiTongHop = @LoaiTongHop
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_chung_tu_don_vi_tong_hop]    Script Date: 11/9/2023 5:10:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[sp_bh_qtt_get_chung_tu_don_vi_tong_hop]
	@YearOfWork int,
	@LoaiTongHop int,
	@UserName nvarchar(100),
	@QuyNam int
AS
BEGIN
	DECLARE @CountDonViCha int;
	SELECT 
		@CountDonViCha = count(*) FROM (SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName AND iNamLamViec = @YearOfWork AND iTrangThai = 1) nddv
	INNER JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1 AND iLoai = 0) dv
	ON dv.iID_MaDonVi = nddv.iID_MaDonVi;

	SELECT
		qtt.iID_QTT_BHXH_ChungTu,
		qtt.sSoChungTu,
		qtt.iID_MaDonVi IIDMaDonVi,
		donvi.sTenDonVi,
		qtt.iQuyNam,
		qtt.iLoaiTongHop,
		qtt.bIsKhoa
	INTO #tblChungTuQTT
	FROM BH_QTT_BHXH_ChungTu qtt
		LEFT JOIN DonVi donvi
		ON qtt.iID_MaDonVi = donvi.iID_MaDonVi
	WHERE donvi.iNamLamViec = @YearOfWork
		AND qtt.iNamLamViec = @YearOfWork
		AND qtt.iLoaiTongHop = @LoaiTongHop
		AND qtt.iQuyNam = @QuyNam

	IF @CountDonViCha = 0
		SELECT 
			ct.*
		FROM #tblChungTuQTT ct
		INNER JOIN 
			(SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName and iNamLamViec = @YearOfWork and iTrangThai = 1) dv
		ON ct.IIDMaDonVi = dv.iID_MaDonVi
		ORDER BY sSoChungTu desc;
	ELSE
		SELECT 
			ct.*
		FROM #tblChungTuQTT ct
		LEFT JOIN 
			(SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName and iNamLamViec = @YearOfWork and iTrangThai = 1) dv
		ON ct.IIDMaDonVi = dv.iID_MaDonVi
		WHERE ((dv.iID_MaDonVi IS NULL AND ct.bIsKhoa = 1) OR (dv.iID_MaDonVi IS NOT NULL))
		ORDER BY sSoChungTu desc;

	drop table #tblChungTuQTT;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_ht_pv_tv_tt]    Script Date: 11/9/2023 5:10:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_ht_pv_tv_tt]
	@MaDonVi nvarchar(100),
	@NamLamViec int,
	@Thang int,
	@DonViTinh int
AS
BEGIN
	--Lay thong tin luong theo TC_HUUTRI, TC_PHUCVIEN, TC_THOIVIEC, TC_TUTUAT
	select * into TBL_HUUTRI from
	(select luong.*
	from TL_BangLuong_ThangBHXH luong
	where 
	luong.sMaDonVi = @MaDonVi
	and luong.iNam = @NamLamViec
	and luong.iThang = @Thang
	and luong.sMaCheDo in ('HUUTRI_TROCAP1LAN', 'HUUTRI_TROCAPKHUVUC', 'PHUCVIEN_TROCAP1LAN', 'PHUCVIEN_TROCAPKHUVUC', 'THOIVIEC_TROCAP1LAN', 'THOIVIEC_TROCAPKHUVUC', 'TUTUAT_TROCAP1LAN', 'TUTUAT_TROCAPKHUVUC', 'TROCAPMAITANG')) HUUTRI

	-- Data tro cap Huu tri
	select
		TBL_HUUTRI.Id,
		TBL_HUUTRI.sMaCB,
		TBL_HUUTRI.sMaCBo,
		TBL_HUUTRI.sMaCheDo,
		TBL_HUUTRI.sTenCbo,
		TBL_HUUTRI.nGiaTri fHUUTRI_TROCAP1LAN,
		HUUTRI_TROCAPKHUVUC.nGiaTri fHUUTRI_TROCAPKHUVUC
		into TBL_HUUTRI_DOC
	from TBL_HUUTRI TBL_HUUTRI
		left join
		(select HUUTRI.Id, HUUTRI.nGiaTri, HUUTRI.sMaCB, HUUTRI.sMaCBo, HUUTRI.sMaCheDo, HUUTRI.sTenCbo
		from TBL_HUUTRI HUUTRI
		where HUUTRI.sMaCheDo = 'HUUTRI_TROCAPKHUVUC') HUUTRI_TROCAPKHUVUC
		on TBL_HUUTRI.sMaCBo = HUUTRI_TROCAPKHUVUC.sMaCBo
	where TBL_HUUTRI.sMaCheDo = 'HUUTRI_TROCAP1LAN'

	-- Data tro cap Phuc vien
	select
		TBL_HUUTRI.Id,
		TBL_HUUTRI.sMaCB,
		TBL_HUUTRI.sMaCBo,
		TBL_HUUTRI.sMaCheDo,
		TBL_HUUTRI.sTenCbo,
		TBL_HUUTRI.nGiaTri fPHUCVIEN_TROCAP1LAN,
		PHUCVIEN_TROCAPKHUVUC.nGiaTri fPHUCVIEN_TROCAPKHUVUC
		into TBL_PHUCVIEN_DOC
	from TBL_HUUTRI TBL_HUUTRI
		left join
		(select HUUTRI.Id, HUUTRI.nGiaTri, HUUTRI.sMaCB, HUUTRI.sMaCBo, HUUTRI.sMaCheDo, HUUTRI.sTenCbo
		from TBL_HUUTRI HUUTRI
		where HUUTRI.sMaCheDo = 'PHUCVIEN_TROCAPKHUVUC') PHUCVIEN_TROCAPKHUVUC
		on TBL_HUUTRI.sMaCBo = PHUCVIEN_TROCAPKHUVUC.sMaCBo
	where TBL_HUUTRI.sMaCheDo = 'PHUCVIEN_TROCAP1LAN'

	-- Data tro cap Thoi Viec
	select
		TBL_HUUTRI.Id,
		TBL_HUUTRI.sMaCB,
		TBL_HUUTRI.sMaCBo,
		TBL_HUUTRI.sMaCheDo,
		TBL_HUUTRI.sTenCbo,
		TBL_HUUTRI.nGiaTri fTHOIVIEC_TROCAP1LAN,
		THOIVIEC_TROCAPKHUVUC.nGiaTri fTHOIVIEC_TROCAPKHUVUC
		into TBL_THOIVIEC_DOC
	from TBL_HUUTRI TBL_HUUTRI
		left join
		(select HUUTRI.Id, HUUTRI.nGiaTri, HUUTRI.sMaCB, HUUTRI.sMaCBo, HUUTRI.sMaCheDo, HUUTRI.sTenCbo
		from TBL_HUUTRI HUUTRI
		where HUUTRI.sMaCheDo = 'THOIVIEC_TROCAPKHUVUC') THOIVIEC_TROCAPKHUVUC
		on TBL_HUUTRI.sMaCBo = THOIVIEC_TROCAPKHUVUC.sMaCBo
	where TBL_HUUTRI.sMaCheDo = 'THOIVIEC_TROCAP1LAN'

	-- Data tro cap Tu tuat
	select
		TBL_HUUTRI.Id,
		TBL_HUUTRI.sMaCB,
		TBL_HUUTRI.sMaCBo,
		TBL_HUUTRI.sMaCheDo,
		TBL_HUUTRI.sTenCbo,
		TBL_HUUTRI.nGiaTri fTUTUAT_TROCAP1LAN,
		TUTUAT_TROCAPKHUVUC.nGiaTri fTUTUAT_TROCAPKHUVUC,
		TROCAPMAITANG.nGiaTri fTROCAPMAITANG
		into TBL_TUTUAT_DOC
	from TBL_HUUTRI TBL_HUUTRI
		left join
		(select HUUTRI.Id, HUUTRI.nGiaTri, HUUTRI.sMaCB, HUUTRI.sMaCBo, HUUTRI.sMaCheDo, HUUTRI.sTenCbo
		from TBL_HUUTRI HUUTRI
		where HUUTRI.sMaCheDo = 'TUTUAT_TROCAPKHUVUC') TUTUAT_TROCAPKHUVUC
		on TBL_HUUTRI.sMaCBo = TUTUAT_TROCAPKHUVUC.sMaCBo
		left join
		(select HUUTRI.Id, HUUTRI.nGiaTri, HUUTRI.sMaCB, HUUTRI.sMaCBo, HUUTRI.sMaCheDo, HUUTRI.sTenCbo
		from TBL_HUUTRI HUUTRI
		where HUUTRI.sMaCheDo = 'TROCAPMAITANG') TROCAPMAITANG
		on TBL_HUUTRI.sMaCBo = TROCAPMAITANG.sMaCBo
	where TBL_HUUTRI.sMaCheDo = 'TUTUAT_TROCAP1LAN'

	--Lay tro cap Huu tri Si quan
	select * into TBL_HUUTRI_SQ from
	(select 1 bHangCha, 'I' LoaiTC, 1 RowNum, 'I' STT, null DoiTuong, null LoaiDoiTuong, null Id, null sMaCB, null sMaCBo, null sMaCheDo, N'TC Hưu trí' sTenCbo, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG
	union
	select 1 bHangCha, 'I' LoaiTC,  1 RowNum, null STT, 'SQ' DoiTuong, 'SQ' LoaiDoiTuong, null Id, null sMaCB, null sMaCBo, null sMaCheDo, 'SQ' sTenCbo, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG
	union
	select 0 bHangCha, 'I' LoaiTC,  1 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'SQ' LoaiDoiTuong, Id, sMaCB, sMaCBo, sMaCheDo, sTenCbo, fHUUTRI_TROCAP1LAN fTROCAP1LAN, fHUUTRI_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG
	from TBL_HUUTRI_DOC
	where sMaCB like '1%') sq

	--Lay tro cap Huu tri QNCN
	select * into TBL_HUUTRI_QNCN from
	(
	select 0 bHangCha, 'I' LoaiTC,  2 RowNum, null STT, 'QNCN' DoiTuong, 'QNCN' LoaiDoiTuong, null Id, null sMaCB, null sMaCBo, null sMaCheDo, 'QNCN' sTenCbo, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG
	union
	select 0 bHangCha, 'I' LoaiTC,  2 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'QNCN' LoaiDoiTuong, Id, sMaCB, sMaCBo, sMaCheDo, sTenCbo, fHUUTRI_TROCAP1LAN fTROCAP1LAN, fHUUTRI_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG
	from TBL_HUUTRI_DOC
	where sMaCB like '2%') qncn

	--Lay tro cap Huu tri HSQ_BS
	select * into TBL_HUUTRI_HSQBS from
	(select 0 bHangCha, 'I' LoaiTC,  3 RowNum, null STT, 'HSQ_BS' DoiTuong, 'HSQ_BS' LoaiDoiTuong, null Id, null sMaCB, null sMaCBo, null sMaCheDo, 'HSQ_BS' sTenCbo, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG
	union
	select 0 bHangCha, 'I' LoaiTC,  3 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'HSQ_BS' LoaiDoiTuong, Id, sMaCB, sMaCBo, sMaCheDo, sTenCbo, fHUUTRI_TROCAP1LAN fTROCAP1LAN, fHUUTRI_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG
	from TBL_HUUTRI_DOC
	where sMaCB like '0%') hsqbs

	--Lay tro cap Huu tri VCQP
	select * into TBL_HUUTRI_VCQP from
	(select 0 bHangCha, 'I' LoaiTC,  4 RowNum, null STT, 'VCQP' DoiTuong, 'VCQP' LoaiDoiTuong, null Id, null sMaCB, null sMaCBo, null sMaCheDo, 'VCQP' sTenCbo, null fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG
	union
	select 0 bHangCha, 'I' LoaiTC,  4 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'VCQP' LoaiDoiTuong, Id, sMaCB, sMaCBo, sMaCheDo, sTenCbo, fHUUTRI_TROCAP1LAN fTROCAP1LAN, fHUUTRI_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG
	from TBL_HUUTRI_DOC
	where sMaCB in ('3.1', '3.2', '3.3')) vcqp

	--Lay tro cap Huu tri HDLD
	select * into TBL_HUUTRI_LDHD from
	(select 0 bHangCha, 'I' LoaiTC,  5 RowNum, null STT, 'LDHD' DoiTuong, 'LDHD' LoaiDoiTuong, null Id, null sMaCB, null sMaCBo, null sMaCheDo, 'LDHD' sTenCbo, null  fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG
	union
	select 0 bHangCha, 'I' LoaiTC,  5 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'LDHD' LoaiDoiTuong, Id, sMaCB, sMaCBo, sMaCheDo, sTenCbo, fHUUTRI_TROCAP1LAN fTROCAP1LAN, fHUUTRI_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG
	from TBL_HUUTRI_DOC
	where sMaCB = '43') ldhd
	-----------------------------------
	--Lay tro cap Phuc vien Si quan
	select * into TBL_PHUCVIEN_SQ from
	(select 1 bHangCha, 'II' LoaiTC, 1 RowNum, 'II' STT, null DoiTuong, null LoaiDoiTuong, null Id, null sMaCB, null sMaCBo, null sMaCheDo, N'TC Phục viên' sTenCbo, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG
	union
	select 1 bHangCha, 'II' LoaiTC, 1 RowNum, null STT, 'SQ' DoiTuong, 'SQ' LoaiDoiTuong, null Id, null sMaCB, null sMaCBo, null sMaCheDo, 'SQ' sTenCbo, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG
	union
	select 0 bHangCha, 'II' LoaiTC, 1 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'SQ' LoaiDoiTuong, Id, sMaCB, sMaCBo, sMaCheDo, sTenCbo, fPHUCVIEN_TROCAP1LAN fTROCAP1LAN, fPHUCVIEN_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG
	from TBL_PHUCVIEN_DOC
	where sMaCB like '1%') pvsq

	--Lay tro cap Phuc vien QNCN
	select * into TBL_PHUCVIEN_QNCN from
	(
	select 0 bHangCha, 'II' LoaiTC, 2 RowNum, null STT, 'QNCN' DoiTuong, 'QNCN' LoaiDoiTuong, null Id, null sMaCB, null sMaCBo, null sMaCheDo, 'QNCN' sTenCbo, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG
	union
	select 0 bHangCha, 'II' LoaiTC, 2 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'QNCN' LoaiDoiTuong, Id, sMaCB, sMaCBo, sMaCheDo, sTenCbo, fPHUCVIEN_TROCAP1LAN fTROCAP1LAN, fPHUCVIEN_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG
	from TBL_PHUCVIEN_DOC
	where sMaCB like '2%') pvqncn

	--Lay tro cap Phuc vien HSQ_BS
	select * into TBL_PHUCVIEN_HSQBS from
	(select 0 bHangCha, 'II' LoaiTC, 3 RowNum, null STT, 'HSQ_BS' DoiTuong, 'HSQ_BS' LoaiDoiTuong, null Id, null sMaCB, null sMaCBo, null sMaCheDo, 'HSQ_BS' sTenCbo, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG
	union
	select 0 bHangCha, 'II' LoaiTC, 3 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'HSQ_BS' LoaiDoiTuong, Id, sMaCB, sMaCBo, sMaCheDo, sTenCbo, fPHUCVIEN_TROCAP1LAN fTROCAP1LAN, fPHUCVIEN_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG
	from TBL_PHUCVIEN_DOC
	where sMaCB like '0%') pvhsqbs

	--Lay tro cap Phuc vien VCQP
	select * into TBL_PHUCVIEN_VCQP from
	(select 0 bHangCha, 'II' LoaiTC, 4 RowNum, null STT, 'VCQP' DoiTuong, 'VCQP' LoaiDoiTuong, null Id, null sMaCB, null sMaCBo, null sMaCheDo, 'VCQP' sTenCbo, null fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG
	union
	select 0 bHangCha, 'II' LoaiTC, 4 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'VCQP' LoaiDoiTuong, Id, sMaCB, sMaCBo, sMaCheDo, sTenCbo, fPHUCVIEN_TROCAP1LAN fTROCAP1LAN, fPHUCVIEN_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG
	from TBL_PHUCVIEN_DOC
	where sMaCB in ('3.1', '3.2', '3.3')) pvvcqp

	--Lay tro cap Phuc vien HDLD
	select * into TBL_PHUCVIEN_LDHD from
	(select 0 bHangCha, 'II' LoaiTC, 5 RowNum, null STT, 'LDHD' DoiTuong, 'LDHD' LoaiDoiTuong, null Id, null sMaCB, null sMaCBo, null sMaCheDo, 'LDHD' sTenCbo, null  fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG
	union
	select 0 bHangCha, 'II' LoaiTC, 5 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'LDHD' LoaiDoiTuong, Id, sMaCB, sMaCBo, sMaCheDo, sTenCbo, fPHUCVIEN_TROCAP1LAN fTROCAP1LAN, fPHUCVIEN_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG
	from TBL_PHUCVIEN_DOC
	where sMaCB = '43') pvldhd
	--------------------------------------------
	--Lay tro cap Thoi viec Si quan
	select * into TBL_THOIVIEC_SQ from
	(select 1 bHangCha, 'III' LoaiTC, 1 RowNum, 'III' STT, null DoiTuong, null LoaiDoiTuong, null Id, null sMaCB, null sMaCBo, null sMaCheDo, N'TC Thôi viêc' sTenCbo, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG
	union
	select 0 bHangCha, 'III' LoaiTC, 1 RowNum, null STT, 'SQ' DoiTuong, 'SQ' LoaiDoiTuong, null Id, null sMaCB, null sMaCBo, null sMaCheDo, 'SQ' sTenCbo, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG
	union
	select 0 bHangCha, 'III' LoaiTC, 1 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'SQ' LoaiDoiTuong, Id, sMaCB, sMaCBo, sMaCheDo, sTenCbo, fTHOIVIEC_TROCAP1LAN fTROCAP1LAN, fTHOIVIEC_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG
	from TBL_THOIVIEC_DOC
	where sMaCB like '1%') tvsq

	--Lay tro cap Thoi viec QNCN
	select * into TBL_THOIVIEC_QNCN from
	(
	select 0 bHangCha, 'III' LoaiTC, 2 RowNum, null STT, 'QNCN' DoiTuong, 'QNCN' LoaiDoiTuong, null Id, null sMaCB, null sMaCBo, null sMaCheDo, 'QNCN' sTenCbo, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG
	union
	select 0 bHangCha, 'III' LoaiTC, 2 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'QNCN' LoaiDoiTuong, Id, sMaCB, sMaCBo, sMaCheDo, sTenCbo, fTHOIVIEC_TROCAP1LAN fTROCAP1LAN, fTHOIVIEC_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG
	from TBL_THOIVIEC_DOC
	where sMaCB like '2%') tvqncn

	--Lay tro cap Thoi viec HSQ_BS
	select * into TBL_THOIVIEC_HSQBS from
	(select 0 bHangCha, 'III' LoaiTC, 3 RowNum, null STT, 'HSQ_BS' DoiTuong, 'HSQ_BS' LoaiDoiTuong, null Id, null sMaCB, null sMaCBo, null sMaCheDo, 'HSQ_BS' sTenCbo, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG
	union
	select 0 bHangCha, 'III' LoaiTC, 3 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'HSQ_BS' LoaiDoiTuong, Id, sMaCB, sMaCBo, sMaCheDo, sTenCbo, fTHOIVIEC_TROCAP1LAN fTROCAP1LAN, fTHOIVIEC_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG
	from TBL_THOIVIEC_DOC
	where sMaCB like '0%') tvhsqbs

	--Lay tro cap Thoi viec VCQP
	select * into TBL_THOIVIEC_VCQP from
	(select 0 bHangCha, 'III' LoaiTC, 4 RowNum, null STT, 'VCQP' DoiTuong, 'VCQP' LoaiDoiTuong, null Id, null sMaCB, null sMaCBo, null sMaCheDo, 'VCQP' sTenCbo, null fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG
	union
	select 0 bHangCha, 'III' LoaiTC, 4 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'VCQP' LoaiDoiTuong, Id, sMaCB, sMaCBo, sMaCheDo, sTenCbo, fTHOIVIEC_TROCAP1LAN fTROCAP1LAN, fTHOIVIEC_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG
	from TBL_THOIVIEC_DOC
	where sMaCB in ('3.1', '3.2', '3.3')) tvvcqp

	--Lay tro cap Thoi viec HDLD
	select * into TBL_THOIVIEC_LDHD from
	(select 0 bHangCha, 'III' LoaiTC, 5 RowNum, null STT, 'LDHD' DoiTuong, 'LDHD' LoaiDoiTuong, null Id, null sMaCB, null sMaCBo, null sMaCheDo, 'LDHD' sTenCbo, null  fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG
	union
	select 0 bHangCha, 'III' LoaiTC, 5 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'LDHD' LoaiDoiTuong, Id, sMaCB, sMaCBo, sMaCheDo, sTenCbo, fTHOIVIEC_TROCAP1LAN fTROCAP1LAN, fTHOIVIEC_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG
	from TBL_THOIVIEC_DOC
	where sMaCB = '43') tvldhd
	--------------------------------------------
	--Lay tro cap Tu tuat Si quan
	select * into TBL_TUTUAT_SQ from
	(select 1 bHangCha, 'IV' LoaiTC, 1 RowNum, 'IV' STT, null DoiTuong, null LoaiDoiTuong, null Id, null sMaCB, null sMaCBo, null sMaCheDo, N'TC Tử tuất' sTenCbo, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG
	union
	select 0 bHangCha, 'IV' LoaiTC, 1 RowNum, null STT, 'SQ' DoiTuong, 'SQ' LoaiDoiTuong, null Id, null sMaCB, null sMaCBo, null sMaCheDo, 'SQ' sTenCbo, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG
	union
	select 0 bHangCha, 'IV' LoaiTC, 1 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'SQ' LoaiDoiTuong, Id, sMaCB, sMaCBo, sMaCheDo, sTenCbo, fTUTUAT_TROCAP1LAN fTROCAP1LAN, fTUTUAT_TROCAPKHUVUC fTROCAPKHUVUC, fTROCAPMAITANG
	from TBL_TUTUAT_DOC
	where sMaCB like '1%') tvsq

	--Lay tro cap Tu tuat QNCN
	select * into TBL_TUTUAT_QNCN from
	(
	select 0 bHangCha, 'IV' LoaiTC, 2 RowNum, null STT, 'QNCN' DoiTuong, 'QNCN' LoaiDoiTuong, null Id, null sMaCB, null sMaCBo, null sMaCheDo, 'QNCN' sTenCbo, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG
	union
	select 0 bHangCha, 'IV' LoaiTC, 2 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'QNCN' LoaiDoiTuong, Id, sMaCB, sMaCBo, sMaCheDo, sTenCbo, fTUTUAT_TROCAP1LAN fTROCAP1LAN, fTUTUAT_TROCAPKHUVUC fTROCAPKHUVUC, fTROCAPMAITANG
	from TBL_TUTUAT_DOC
	where sMaCB like '2%') tvqncn

	--Lay tro cap Tu tuat HSQ_BS
	select * into TBL_TUTUAT_HSQBS from
	(select 0 bHangCha, 'IV' LoaiTC, 3 RowNum, null STT, 'HSQ_BS' DoiTuong, 'HSQ_BS' LoaiDoiTuong, null Id, null sMaCB, null sMaCBo, null sMaCheDo, 'HSQ_BS' sTenCbo, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG
	union
	select 0 bHangCha, 'IV' LoaiTC, 3 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'HSQ_BS' LoaiDoiTuong, Id, sMaCB, sMaCBo, sMaCheDo, sTenCbo, fTUTUAT_TROCAP1LAN fTROCAP1LAN, fTUTUAT_TROCAPKHUVUC fTROCAPKHUVUC, fTROCAPMAITANG
	from TBL_TUTUAT_DOC
	where sMaCB like '0%') tvhsqbs

	--Lay tro cap Tu tuat VCQP
	select * into TBL_TUTUAT_VCQP from
	(select 0 bHangCha, 'IV' LoaiTC, 4 RowNum, null STT, 'VCQP' DoiTuong, 'VCQP' LoaiDoiTuong, null Id, null sMaCB, null sMaCBo, null sMaCheDo, 'VCQP' sTenCbo, null fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG
	union
	select 0 bHangCha, 'IV' LoaiTC, 4 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'VCQP' LoaiDoiTuong, Id, sMaCB, sMaCBo, sMaCheDo, sTenCbo, fTUTUAT_TROCAP1LAN fTROCAP1LAN, fTUTUAT_TROCAPKHUVUC fTROCAPKHUVUC, fTROCAPMAITANG
	from TBL_TUTUAT_DOC
	where sMaCB in ('3.1', '3.2', '3.3')) tvvcqp

	--Lay tro cap Tu tuat HDLD
	select * into TBL_TUTUAT_LDHD from
	(select 0 bHangCha, 'IV' LoaiTC, 5 RowNum, null STT, 'LDHD' DoiTuong, 'LDHD' LoaiDoiTuong, null Id, null sMaCB, null sMaCBo, null sMaCheDo, 'LDHD' sTenCbo, null  fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG
	union
	select 0 bHangCha, 'IV' LoaiTC, 5 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'LDHD' LoaiDoiTuong, Id, sMaCB, sMaCBo, sMaCheDo, sTenCbo, fTUTUAT_TROCAP1LAN fTROCAP1LAN, fTUTUAT_TROCAPKHUVUC fTROCAPKHUVUC, fTROCAPMAITANG
	from TBL_TUTUAT_DOC
	where sMaCB = '43') tvldhd
	----------------------------------------------
	--Ket qua
	select result.* into TBL_HUUTRI_RESULT from
	(select bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Id, sMaCB, sMaCBo, sTenCbo, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_HUUTRI_SQ
	union
	select bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Id, sMaCB, sMaCBo, sTenCbo, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_HUUTRI_QNCN
	union
	select bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Id, sMaCB, sMaCBo, sTenCbo, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_HUUTRI_HSQBS
	union
	select bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Id, sMaCB, sMaCBo, sTenCbo, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_HUUTRI_VCQP
	union
	select bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Id, sMaCB, sMaCBo, sTenCbo, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_HUUTRI_LDHD
	union
	select bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Id, sMaCB, sMaCBo, sTenCbo, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_PHUCVIEN_SQ
	union
	select bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Id, sMaCB, sMaCBo, sTenCbo, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_PHUCVIEN_QNCN
	union
	select bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Id, sMaCB, sMaCBo, sTenCbo, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_PHUCVIEN_HSQBS
	union
	select bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Id, sMaCB, sMaCBo, sTenCbo, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_PHUCVIEN_VCQP
	union
	select bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Id, sMaCB, sMaCBo, sTenCbo, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_PHUCVIEN_LDHD
	union
	select bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Id, sMaCB, sMaCBo, sTenCbo, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_THOIVIEC_SQ
	union
	select bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Id, sMaCB, sMaCBo, sTenCbo, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_THOIVIEC_QNCN
	union
	select bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Id, sMaCB, sMaCBo, sTenCbo, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_THOIVIEC_HSQBS
	union
	select bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Id, sMaCB, sMaCBo, sTenCbo, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_THOIVIEC_VCQP
	union
	select bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Id, sMaCB, sMaCBo, sTenCbo, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_THOIVIEC_LDHD
	union
	select bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Id, sMaCB, sMaCBo, sTenCbo, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_TUTUAT_SQ
	union
	select bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Id, sMaCB, sMaCBo, sTenCbo, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_TUTUAT_QNCN
	union
	select bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Id, sMaCB, sMaCBo, sTenCbo, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_TUTUAT_HSQBS
	union
	select bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Id, sMaCB, sMaCBo, sTenCbo, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_TUTUAT_VCQP
	union
	select bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Id, sMaCB, sMaCBo, sTenCbo, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_TUTUAT_LDHD) result

	select
		LoaiTC,
		RowNum,
		STT,
		DoiTuong, 
		LoaiDoiTuong,
		sMaCB MaCb, 
		sMaCBo MaCbo, 
		sTenCbo TenCbo, 
		fTROCAP1LAN/@DonViTinh fTroCap1Lan,
		fTROCAPKHUVUC/@DonViTinh fTroCapKhuVuc,
		fTROCAPMAITANG/@DonViTinh fTroCapMaiTang,
		fTongSoTien/@DonViTinh fTongSoTien,
		bHangCha IsHangCha
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
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_om_dau]    Script Date: 11/9/2023 5:10:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_om_dau]
	@MaDonVi nvarchar(100),
	@NamLamViec int,
	@Thang int,
	@DonViTinh int
AS
BEGIN

	--Lay thong tin luong theo tro cap om dau
	select * into TBL_TCOD from
	(select luongcancu.fLuongCanCu, chedo.fSoNgayHuongBHXH, luong.*
	from TL_BangLuong_ThangBHXH luong
	join TL_CanBo_CheDoBHXH chedo on luong.sMaCBo = chedo.sMaCanBo and luong.sMaCheDo = chedo.sMaCheDo
	left join 
	(select Ma_CBo, NAM, thang, sum(Gia_Tri) fLuongCanCu from TL_BangLuong_Thang
		where NAM = @NamLamViec
		and thang = @Thang
		group by Ma_CBo, NAM, thang) luongcancu on luong.sMaCBo = luongcancu.Ma_CBo
	where 
	luong.sMaDonVi = @MaDonVi
	and luong.iNam = @NamLamViec
	and luong.iThang = @Thang
	and luong.sMaCheDo in ('BENHDAINGAY_D14NGAY', 'BENHDAINGAY_T14NGAY', 'OMKHAC_D14NGAY', 'OMKHAC_T14NGAY', 'CONOM', 'OMDAU_DUONGSUCPHSK')) tcod

	select TBL_TCOD.Id,
		TBL_TCOD.sMaCB,
		TBL_TCOD.sMaCBo,
		TBL_TCOD.sMaCheDo,
		TBL_TCOD.sTenCbo,
		chedo.fSoNgayHuongBHXH SoNgayBenhDaiNgayD14Ngay,
		BENHDAINGAY_T14NGAY.SoNgayBenhDaiNgayT14Ngay,
		OMKHAC_D14NGAY.SoNgayOmKhacD14Ngay,
		OMKHAC_T14NGAY.SoNgayOmKhacT14Ngay,
		CONOM.SoNgayConOm,
		DUONGSUCPHSK.SoNgayDuongSuc,
		TBL_TCOD.fLuongCanCu,
		TBL_TCOD.nGiaTri fBENHDAINGAY_D14NGAY,
		BENHDAINGAY_T14NGAY.nGiaTri fBENHDAINGAY_T14NGAY,
		OMKHAC_D14NGAY.nGiaTri fOMKHAC_D14NGAY,
		OMKHAC_T14NGAY.nGiaTri fOMKHAC_T14NGAY,
		CONOM.nGiaTri fCONOM,
		DUONGSUCPHSK.nGiaTri fDUONGSUCPHSK
		into TBL_TCOD_DOC
	from TBL_TCOD TBL_TCOD left join TL_CanBo_CheDoBHXH chedo on TBL_TCOD.sMaCBo = chedo.sMaCanBo and TBL_TCOD.sMaCheDo = chedo.sMaCheDo
		left join
		(select tcod.Id, tcod.nGiaTri, tcod.sMaCB, tcod.sMaCBo, tcod.sMaCheDo, tcod.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayBenhDaiNgayT14Ngay
		from TBL_TCOD tcod left join TL_CanBo_CheDoBHXH chedo on tcod.sMaCBo = chedo.sMaCanBo and tcod.sMaCheDo = chedo.sMaCheDo
		where tcod.sMaCheDo = 'BENHDAINGAY_T14NGAY') BENHDAINGAY_T14NGAY
		on TBL_TCOD.sMaCBo = BENHDAINGAY_T14NGAY.sMaCBo
		left join
		(select tcod_1.Id, tcod_1.nGiaTri, tcod_1.sMaCB, tcod_1.sMaCBo, tcod_1.sMaCheDo, tcod_1.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayOmKhacD14Ngay
		from TBL_TCOD tcod_1 left join TL_CanBo_CheDoBHXH chedo on tcod_1.sMaCBo = chedo.sMaCanBo and tcod_1.sMaCheDo = chedo.sMaCheDo
		where tcod_1.sMaCheDo = 'OMKHAC_D14NGAY') OMKHAC_D14NGAY
		on TBL_TCOD.sMaCBo = OMKHAC_D14NGAY.sMaCBo
		left join
		(select tcod_2.Id, tcod_2.nGiaTri, tcod_2.sMaCB, tcod_2.sMaCBo, tcod_2.sMaCheDo, tcod_2.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayOmKhacT14Ngay
		from TBL_TCOD tcod_2 left join TL_CanBo_CheDoBHXH chedo on tcod_2.sMaCBo = chedo.sMaCanBo and tcod_2.sMaCheDo = chedo.sMaCheDo
		where tcod_2.sMaCheDo = 'OMKHAC_T14NGAY') OMKHAC_T14NGAY
		on TBL_TCOD.sMaCBo = OMKHAC_T14NGAY.sMaCBo
		left join
		(select conom.Id, conom.nGiaTri, conom.sMaCB, conom.sMaCBo, conom.sMaCheDo, conom.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayConOm
		from TBL_TCOD conom left join TL_CanBo_CheDoBHXH chedo on conom.sMaCBo = chedo.sMaCanBo and conom.sTenCbo = chedo.sMaCheDo
		where conom.sMaCheDo = 'CONOM') CONOM
		on TBL_TCOD.sMaCBo = CONOM.sMaCBo
		left join
		(select duongsuc.Id, duongsuc.nGiaTri, duongsuc.sMaCB, duongsuc.sMaCBo, duongsuc.sMaCheDo, duongsuc.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayDuongSuc
		from TBL_TCOD duongsuc left join TL_CanBo_CheDoBHXH chedo on duongsuc.sMaCBo = chedo.sMaCanBo and duongsuc.sMaCheDo = chedo.sMaCheDo
		where duongsuc.sMaCheDo = 'OMDAU_DUONGSUCPHSK') DUONGSUCPHSK
		on TBL_TCOD.sMaCBo = DUONGSUCPHSK.sMaCBo
	where TBL_TCOD.sMaCheDo = 'BENHDAINGAY_D14NGAY'

	--Lấy lương Sĩ quan
	select * into TBL_TCOD_SQ from
	(select
		1 RowNum, 'I' STT, 'SQ' DoiTuong, 'SQ' LoaiDoiTuong, null Id, null sMaCB, null sMaCBo, null sMaCheDo, null sTenCbo, null SoNgayBenhDaiNgayD14Ngay, null SoNgayBenhDaiNgayT14Ngay, null SoNgayOmKhacD14Ngay, null SoNgayOmKhacT14Ngay, null SoNgayConOm, null SoNgayDuongSuc, null fLuongCanCu, null fBENHDAINGAY_D14NGAY, null fBENHDAINGAY_T14NGAY, null fOMKHAC_D14NGAY, null fOMKHAC_T14NGAY, null fCONOM, null fDUONGSUCPHSK
	union
	select
		1 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'SQ' LoaiDoiTuong, Id, sMaCB, sMaCBo, sMaCheDo, sTenCbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK
	from TBL_TCOD_DOC
	where sMaCB like '1%') sq

	--Lấy lương QNCN
	select * into TBL_TCOD_QNCN from
	(select
		2 RowNum, 'II' STT, 'QNCN' DoiTuong, 'QNCN' LoaiDoiTuong, null Id, null sMaCB, null sMaCBo, null sMaCheDo, null sTenCbo, null SoNgayBenhDaiNgayD14Ngay, null SoNgayBenhDaiNgayT14Ngay, null SoNgayOmKhacD14Ngay, null SoNgayOmKhacT14Ngay, null SoNgayConOm, null SoNgayDuongSuc, null fLuongCanCu, null fBENHDAINGAY_D14NGAY, null fBENHDAINGAY_T14NGAY, null fOMKHAC_D14NGAY, null fOMKHAC_T14NGAY, null fCONOM, null fDUONGSUCPHSK
	union
	select
		2 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'QNCN' LoaiDoiTuong, Id, sMaCB, sMaCBo, sMaCheDo, sTenCbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK
	from TBL_TCOD_DOC
	where sMaCB like '2%') qncn

	--Lấy lương HSQ_BS
	select * into TBL_TCOD_HSQBS from
	(select
		3 RowNum, 'III' STT, 'HSQ_BS' DoiTuong, 'HSQ_BS' LoaiDoiTuong, null Id, null sMaCB, null sMaCBo, null sMaCheDo, null sTenCbo, null SoNgayBenhDaiNgayD14Ngay, null SoNgayBenhDaiNgayT14Ngay, null SoNgayOmKhacD14Ngay, null SoNgayOmKhacT14Ngay, null SoNgayConOm, null SoNgayDuongSuc, null fLuongCanCu, null fBENHDAINGAY_D14NGAY, null fBENHDAINGAY_T14NGAY, null fOMKHAC_D14NGAY, null fOMKHAC_T14NGAY, null fCONOM, null fDUONGSUCPHSK
	union
	select
		3 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'HSQ_BS' LoaiDoiTuong, Id, sMaCB, sMaCBo, sMaCheDo, sTenCbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK
	from TBL_TCOD_DOC
	where sMaCB like '0%') hsqbs

	--Lấy lương VCQP
	select * into TBL_TCOD_VCQP from
	(select
		4 RowNum, 'IV' STT, 'VCQP' DoiTuong, 'VCQP' LoaiDoiTuong, null Id, null sMaCB, null sMaCBo, null sMaCheDo, null sTenCbo, null SoNgayBenhDaiNgayD14Ngay, null SoNgayBenhDaiNgayT14Ngay, null SoNgayOmKhacD14Ngay, null SoNgayOmKhacT14Ngay, null SoNgayConOm, null SoNgayDuongSuc, null fLuongCanCu, null fBENHDAINGAY_D14NGAY, null fBENHDAINGAY_T14NGAY, null fOMKHAC_D14NGAY, null fOMKHAC_T14NGAY, null fCONOM, null fDUONGSUCPHSK
	union
	select
		4 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'VCQP' LoaiDoiTuong, Id, sMaCB, sMaCBo, sMaCheDo, sTenCbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK
	from TBL_TCOD_DOC
	where sMaCB in ('3.1', '3.2', '3.3')) vcqp

	--Lấy lương Lao động hợp đông
	select * into TBL_TCOD_LDHD from
	(select
		5 RowNum, 'V' STT, 'LDHD' DoiTuong, 'LDHD' LoaiDoiTuong, null Id, null sMaCB, null sMaCBo, null sMaCheDo, null sTenCbo, null SoNgayBenhDaiNgayD14Ngay, null SoNgayBenhDaiNgayT14Ngay, null SoNgayOmKhacD14Ngay, null SoNgayOmKhacT14Ngay, null SoNgayConOm, null SoNgayDuongSuc, null fLuongCanCu, null fBENHDAINGAY_D14NGAY, null fBENHDAINGAY_T14NGAY, null fOMKHAC_D14NGAY, null fOMKHAC_T14NGAY, null fCONOM, null fDUONGSUCPHSK
	union
	select
		5 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'LDHD' LoaiDoiTuong, Id, sMaCB, sMaCBo, sMaCheDo, sTenCbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK
	from TBL_TCOD_DOC
	where sMaCB = '43') ldhd

	--Ket qua
	select result.* into TBL_TCOD_RESULT from
	(select
	RowNum, STT, DoiTuong, LoaiDoiTuong, Id, sMaCB, sMaCBo, sTenCbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, sMaCheDo, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK,
	(isnull(fBENHDAINGAY_D14NGAY, 0) + isnull(fBENHDAINGAY_T14NGAY, 0) + isnull(fOMKHAC_D14NGAY, 0) + isnull(fOMKHAC_T14NGAY, 0) + isnull(fCONOM, 0) + isnull(fDUONGSUCPHSK, 0)) fTongSoTien
	from TBL_TCOD_SQ
	union
	select
	RowNum, STT, DoiTuong, LoaiDoiTuong, Id, sMaCB, sMaCBo, sTenCbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, sMaCheDo, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK,
	(isnull(fBENHDAINGAY_D14NGAY, 0) + isnull(fBENHDAINGAY_T14NGAY, 0) + isnull(fOMKHAC_D14NGAY, 0) + isnull(fOMKHAC_T14NGAY, 0) + isnull(fCONOM, 0) + isnull(fDUONGSUCPHSK, 0)) fTongSoTien
	from TBL_TCOD_QNCN
	union
	select
	RowNum, STT, DoiTuong, LoaiDoiTuong, Id, sMaCB, sMaCBo, sTenCbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, sMaCheDo, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK,
	(isnull(fBENHDAINGAY_D14NGAY, 0) + isnull(fBENHDAINGAY_T14NGAY, 0) + isnull(fOMKHAC_D14NGAY, 0) + isnull(fOMKHAC_T14NGAY, 0) + isnull(fCONOM, 0) + isnull(fDUONGSUCPHSK, 0)) fTongSoTien
	from TBL_TCOD_HSQBS
	union
	select
	RowNum, STT, DoiTuong, LoaiDoiTuong, Id, sMaCB, sMaCBo, sTenCbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, sMaCheDo, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK,
	(isnull(fBENHDAINGAY_D14NGAY, 0) + isnull(fBENHDAINGAY_T14NGAY, 0) + isnull(fOMKHAC_D14NGAY, 0) + isnull(fOMKHAC_T14NGAY, 0) + isnull(fCONOM, 0) + isnull(fDUONGSUCPHSK, 0)) fTongSoTien
	from TBL_TCOD_VCQP
	union
	select
	RowNum, STT, DoiTuong, LoaiDoiTuong, Id, sMaCB, sMaCBo, sTenCbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, sMaCheDo, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK,
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
		SoNgayBenhDaiNgayD14Ngay, 
		SoNgayBenhDaiNgayT14Ngay, 
		SoNgayOmKhacD14Ngay, 
		SoNgayOmKhacT14Ngay, 
		SoNgayConOm, 
		SoNgayDuongSuc, 
		fLuongCanCu FLuongCanCu, 
		sMaCheDo MaCheDo, 
		fBENHDAINGAY_D14NGAY/@DonViTinh FBenhDaiNgayD14Ngay, 
		fBENHDAINGAY_T14NGAY/@DonViTinh FBenhDauNgayT14Ngay, 
		fOMKHAC_D14NGAY/@DonViTinh FOmKhacD14Ngay, 
		fOMKHAC_T14NGAY/@DonViTinh FOmKhacT14Ngay, 
		fCONOM/@DonViTinh FConOm, 
		fDUONGSUCPHSK/@DonViTinh FDuongSucPHSK,
		fTongSoTien/@DonViTinh FTongSoTien
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
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_thai_san]    Script Date: 11/9/2023 5:10:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_thai_san]
	@MaDonVi nvarchar(100),
	@NamLamViec int,
	@Thang int,
	@DonViTinh int
AS
BEGIN

	--Lay thong tin luong theo tro cap thai san
	select * into TBL_TCTS from
	(select luongcancu.fLuongCanCu, chedo.fSoNgayHuongBHXH, luong.*
	from TL_BangLuong_ThangBHXH luong
	join TL_CanBo_CheDoBHXH chedo on luong.sMaCBo = chedo.sMaCanBo and luong.sMaCheDo = chedo.sMaCheDo
	left join 
	(select Ma_CBo, NAM, thang, sum(Gia_Tri) fLuongCanCu from TL_BangLuong_Thang
		where NAM = @NamLamViec
		and thang = @Thang
		group by Ma_CBo, NAM, thang) luongcancu on luong.sMaCBo = luongcancu.Ma_CBo
	where 
	luong.sMaDonVi = @MaDonVi
	and luong.iNam = @NamLamViec
	and luong.iThang = @Thang
	and luong.sMaCheDo in ('SINHCON_NUOICON', 'THAISAN_TROCAP1LAN', 'KHAMTHAI', 'KHHGĐ', 'NAMNGHIKHIVOSINHCON', 'THAISAN_DUONGSUCPHSK')) tcod

	select TBL_TCTS.Id,
		TBL_TCTS.sMaCB,
		TBL_TCTS.sMaCBo,
		TBL_TCTS.sMaCheDo,
		TBL_TCTS.sTenCbo,
		chedo.fSoNgayHuongBHXH SoNgaySINHCON_NUOICON,
		TROCAP1LAN.SoNgayTROCAP1LAN,
		KHAMTHAI.SoNgayKHAMTHAI,
		DUONGSUCPHSK.SoNgayDUONGSUCPHSK,
		TBL_TCTS.fLuongCanCu,
		TBL_TCTS.nGiaTri fSINHCON_NUOICON,
		TROCAP1LAN.nGiaTri fTROCAP1LAN,
		KHAMTHAI.nGiaTri fKHAMTHAI,
		DUONGSUCPHSK.nGiaTri fDUONGSUCPHSK
		into TBL_TCTS_DOC
	from TBL_TCTS TBL_TCTS left join TL_CanBo_CheDoBHXH chedo on TBL_TCTS.sMaCBo = chedo.sMaCanBo and TBL_TCTS.sMaCheDo = chedo.sMaCheDo
		left join
		(select tcod.Id, tcod.nGiaTri, tcod.sMaCB, tcod.sMaCBo, tcod.sMaCheDo, tcod.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTROCAP1LAN
		from TBL_TCTS tcod left join TL_CanBo_CheDoBHXH chedo on tcod.sMaCBo = chedo.sMaCanBo and tcod.sMaCheDo = chedo.sMaCheDo
		where tcod.sMaCheDo = 'THAISAN_TROCAP1LAN') TROCAP1LAN
		on TBL_TCTS.sMaCBo = TROCAP1LAN.sMaCBo
		left join
		(select tcod_1.Id, sum(tcod_1.nGiaTri) nGiaTri, tcod_1.sMaCB, tcod_1.sMaCBo, tcod_1.sMaCheDo, tcod_1.sTenCbo, sum(chedo.fSoNgayHuongBHXH) SoNgayKHAMTHAI
		from TBL_TCTS tcod_1 left join TL_CanBo_CheDoBHXH chedo on tcod_1.sMaCBo = chedo.sMaCanBo and tcod_1.sMaCheDo = chedo.sMaCheDo
		where tcod_1.sMaCheDo in ('KHAMTHAI', 'KHHGĐ', 'NAMNGHIKHIVOSINHCON')
		group by tcod_1.Id, tcod_1.sMaCB, tcod_1.sMaCBo, tcod_1.sMaCheDo, tcod_1.sTenCbo) KHAMTHAI
		on TBL_TCTS.sMaCBo = KHAMTHAI.sMaCBo
		left join
		(select tcod_2.Id, tcod_2.nGiaTri, tcod_2.sMaCB, tcod_2.sMaCBo, tcod_2.sMaCheDo, tcod_2.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayDUONGSUCPHSK
		from TBL_TCTS tcod_2 left join TL_CanBo_CheDoBHXH chedo on tcod_2.sMaCBo = chedo.sMaCanBo and tcod_2.sMaCheDo = chedo.sMaCheDo
		where tcod_2.sMaCheDo = 'THAISAN_DUONGSUCPHSK') DUONGSUCPHSK
		on TBL_TCTS.sMaCBo = DUONGSUCPHSK.sMaCBo
	where TBL_TCTS.sMaCheDo = 'SINHCON_NUOICON'

	--Lấy lương Sĩ quan
	select * into TBL_TCTS_SQ from
	(select
		1 RowNum, 'I' STT, 'SQ' DoiTuong, 'SQ' LoaiDoiTuong, null Id, null sMaCB, null sMaCBo, null sMaCheDo, null sTenCbo, null SoNgaySINHCON_NUOICON, null SoNgayTROCAP1LAN, null SoNgayKHAMTHAI, null SoNgayDUONGSUCPHSK, null fLuongCanCu, null fSINHCON_NUOICON, null fTROCAP1LAN, null fKHAMTHAI, null fDUONGSUCPHSK
	union
	select
		1 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'SQ' LoaiDoiTuong, Id, sMaCB, sMaCBo, sMaCheDo, sTenCbo, SoNgaySINHCON_NUOICON, SoNgayTROCAP1LAN, SoNgayKHAMTHAI, SoNgayDUONGSUCPHSK, fLuongCanCu, fSINHCON_NUOICON, fTROCAP1LAN, fKHAMTHAI, fDUONGSUCPHSK
	from TBL_TCTS_DOC
	where sMaCB like '1%') sq

	--Lấy lương QNCN
	select * into TBL_TCTS_QNCN from
	(select
		2 RowNum, 'II' STT, 'QNCN' DoiTuong, 'QNCN' LoaiDoiTuong, null Id, null sMaCB, null sMaCBo, null sMaCheDo, null sTenCbo, null SoNgaySINHCON_NUOICON, null SoNgayTROCAP1LAN, null SoNgayKHAMTHAI, null SoNgayDUONGSUCPHSK, null fLuongCanCu, null fSINHCON_NUOICON, null fTROCAP1LAN, null fKHAMTHAI, null fDUONGSUCPHSK
	union
	select
		2 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'QNCN' LoaiDoiTuong, Id, sMaCB, sMaCBo, sMaCheDo, sTenCbo, SoNgaySINHCON_NUOICON, SoNgayTROCAP1LAN, SoNgayKHAMTHAI, SoNgayDUONGSUCPHSK, fLuongCanCu, fSINHCON_NUOICON, fTROCAP1LAN, fKHAMTHAI, fDUONGSUCPHSK
	from TBL_TCTS_DOC
	where sMaCB like '2%') qncn

	--Lấy lương HSQ_BS
	select * into TBL_TCTS_HSQBS from
	(select
		3 RowNum, 'III' STT, 'HSQ_BS' DoiTuong, 'HSQ_BS' LoaiDoiTuong, null Id, null sMaCB, null sMaCBo, null sMaCheDo, null sTenCbo, null SoNgaySINHCON_NUOICON, null SoNgayTROCAP1LAN, null SoNgayKHAMTHAI, null SoNgayDUONGSUCPHSK, null fLuongCanCu, null fSINHCON_NUOICON, null fTROCAP1LAN, null fKHAMTHAI, null fDUONGSUCPHSK
	union
	select
		3 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'HSQ_BS' LoaiDoiTuong, Id, sMaCB, sMaCBo, sMaCheDo, sTenCbo, SoNgaySINHCON_NUOICON, SoNgayTROCAP1LAN, SoNgayKHAMTHAI, SoNgayDUONGSUCPHSK, fLuongCanCu, fSINHCON_NUOICON, fTROCAP1LAN, fKHAMTHAI, fDUONGSUCPHSK
	from TBL_TCTS_DOC
	where sMaCB like '0%') hsqbs

	--Lấy lương VCQP
	select * into TBL_TCTS_VCQP from
	(select
		4 RowNum, 'IV' STT, 'VCQP' DoiTuong, 'VCQP' LoaiDoiTuong, null Id, null sMaCB, null sMaCBo, null sMaCheDo, null sTenCbo, null SoNgaySINHCON_NUOICON, null SoNgayTROCAP1LAN, null SoNgayKHAMTHAI, null SoNgayDUONGSUCPHSK, null fLuongCanCu, null fSINHCON_NUOICON, null fTROCAP1LAN, null fKHAMTHAI, null fDUONGSUCPHSK
	union
	select
		4 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'VCQP' LoaiDoiTuong, Id, sMaCB, sMaCBo, sMaCheDo, sTenCbo, SoNgaySINHCON_NUOICON, SoNgayTROCAP1LAN, SoNgayKHAMTHAI, SoNgayDUONGSUCPHSK, fLuongCanCu, fSINHCON_NUOICON, fTROCAP1LAN, fKHAMTHAI, fDUONGSUCPHSK
	from TBL_TCTS_DOC
	where sMaCB in ('3.1', '3.2', '3.3')) vcqp

	--Lấy lương Lao động hợp đông
	select * into TBL_TCTS_LDHD from
	(select
		5 RowNum, 'V' STT, 'LDHD' DoiTuong, 'LDHD' LoaiDoiTuong, null Id, null sMaCB, null sMaCBo, null sMaCheDo, null sTenCbo, null SoNgaySINHCON_NUOICON, null SoNgayTROCAP1LAN, null SoNgayKHAMTHAI, null SoNgayDUONGSUCPHSK, null fLuongCanCu, null fSINHCON_NUOICON, null fTROCAP1LAN, null fKHAMTHAI, null fDUONGSUCPHSK
	union
	select
		5 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'LDHD' LoaiDoiTuong, Id, sMaCB, sMaCBo, sMaCheDo, sTenCbo, SoNgaySINHCON_NUOICON, SoNgayTROCAP1LAN, SoNgayKHAMTHAI, SoNgayDUONGSUCPHSK, fLuongCanCu, fSINHCON_NUOICON, fTROCAP1LAN, fKHAMTHAI, fDUONGSUCPHSK
	from TBL_TCTS_DOC
	where sMaCB = '43') ldhd

	--Ket qua
	select result.* into TBL_TCTS_RESULT from
	(select
	RowNum, STT, DoiTuong, LoaiDoiTuong, Id, sMaCB, sMaCBo, sTenCbo, SoNgaySINHCON_NUOICON, SoNgayTROCAP1LAN, SoNgayKHAMTHAI, SoNgayDUONGSUCPHSK, fLuongCanCu, sMaCheDo, fSINHCON_NUOICON, fTROCAP1LAN, fKHAMTHAI, fDUONGSUCPHSK,
	(isnull(fSINHCON_NUOICON, 0) + isnull(fTROCAP1LAN, 0) + isnull(fKHAMTHAI, 0) + isnull(fDUONGSUCPHSK, 0)) fTongSoTien
	from TBL_TCTS_SQ
	union
	select
	RowNum, STT, DoiTuong, LoaiDoiTuong, Id, sMaCB, sMaCBo, sTenCbo, SoNgaySINHCON_NUOICON, SoNgayTROCAP1LAN, SoNgayKHAMTHAI, SoNgayDUONGSUCPHSK, fLuongCanCu, sMaCheDo, fSINHCON_NUOICON, fTROCAP1LAN, fKHAMTHAI, fDUONGSUCPHSK,
	(isnull(fSINHCON_NUOICON, 0) + isnull(fTROCAP1LAN, 0) + isnull(fKHAMTHAI, 0) + isnull(fDUONGSUCPHSK, 0)) fTongSoTien
	from TBL_TCTS_QNCN
	union
	select
	RowNum, STT, DoiTuong, LoaiDoiTuong, Id, sMaCB, sMaCBo, sTenCbo, SoNgaySINHCON_NUOICON, SoNgayTROCAP1LAN, SoNgayKHAMTHAI, SoNgayDUONGSUCPHSK, fLuongCanCu, sMaCheDo, fSINHCON_NUOICON, fTROCAP1LAN, fKHAMTHAI, fDUONGSUCPHSK,
	(isnull(fSINHCON_NUOICON, 0) + isnull(fTROCAP1LAN, 0) + isnull(fKHAMTHAI, 0) + isnull(fDUONGSUCPHSK, 0)) fTongSoTien
	from TBL_TCTS_HSQBS
	union
	select
	RowNum, STT, DoiTuong, LoaiDoiTuong, Id, sMaCB, sMaCBo, sTenCbo, SoNgaySINHCON_NUOICON, SoNgayTROCAP1LAN, SoNgayKHAMTHAI, SoNgayDUONGSUCPHSK, fLuongCanCu, sMaCheDo, fSINHCON_NUOICON, fTROCAP1LAN, fKHAMTHAI, fDUONGSUCPHSK,
	(isnull(fSINHCON_NUOICON, 0) + isnull(fTROCAP1LAN, 0) + isnull(fKHAMTHAI, 0) + isnull(fDUONGSUCPHSK, 0)) fTongSoTien
	from TBL_TCTS_VCQP
	union
	select
	RowNum, STT, DoiTuong, LoaiDoiTuong, Id, sMaCB, sMaCBo, sTenCbo, SoNgaySINHCON_NUOICON, SoNgayTROCAP1LAN, SoNgayKHAMTHAI, SoNgayDUONGSUCPHSK, fLuongCanCu, sMaCheDo, fSINHCON_NUOICON, fTROCAP1LAN, fKHAMTHAI, fDUONGSUCPHSK,
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
		SoNgaySINHCON_NUOICON SoNgaySinhConNuoiCon,
		SoNgayTROCAP1LAN SoNgayTroCap1Lan,
		SoNgayKHAMTHAI SoNgayKhamThai,
		SoNgayDUONGSUCPHSK SoNgayDuongSucPHSKThaiSan,
		fLuongCanCu FLuongCanCu, 
		sMaCheDo MaCheDo, 
		fSINHCON_NUOICON/@DonViTinh fSinhConNuoiCon,
		fTROCAP1LAN/@DonViTinh fTroCap1Lan,
		fKHAMTHAI/@DonViTinh fKhamThai,
		fDUONGSUCPHSK/@DonViTinh fDuongSucPHSKThaiSan,
		fTongSoTien/@DonViTinh FTongSoTien
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
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_tnld]    Script Date: 11/9/2023 5:10:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_tnld] 
	@MaDonVi nvarchar(100),
	@NamLamViec int,
	@Thang int,
	@DonViTinh int
AS
BEGIN

	--Lay thong tin luong theo tro cap tai nan lao dong
	select * into TBL_TCTNLD from
	(select luong.*
	from TL_BangLuong_ThangBHXH luong
	where 
	luong.sMaDonVi = @MaDonVi
	and luong.iNam = @NamLamViec
	and luong.iThang = @Thang
	and luong.sMaCheDo in ('CHIGIAMDINH', 'TAINANLD_TROCAP1LAN', 'TROCAPTHEOPHIEUTRUYTRA', 'TROCAPHANGTHANG', 'TROCAPPHCN', 'TROCAPPHUCVU', 'TROCAPCHETDOTNLD', 'TAINANLĐ_DUONGSUCPHSK')) tctnld

	select TBL_TCTNLD.Id,
		TBL_TCTNLD.sMaCB,
		TBL_TCTNLD.sMaCBo,
		TBL_TCTNLD.sMaCheDo,
		TBL_TCTNLD.sTenCbo,
		TBL_TCTNLD.nGiaTri fChiGiamDinh,
		TROCAP1LAN.nGiaTri fTroCap1Lan,
		TROCAPTHEOPHIEUTRUYTRA.nGiaTri fTroCapTheoPhieuTruyTra,
		TROCAPHANGTHANG.nGiaTri fTroCapHangThang,
		TROCAPPHCN.nGiaTri fTroCapPHCN,
		TROCAPCHETDOTNLD.nGiaTri fTroCapChetDoTNLD,
		TAINANLĐ_DUONGSUCPHSK.SoNgayDuongSucTNLD,
		TAINANLĐ_DUONGSUCPHSK.nGiaTri fDuongSucTNLD
		into TBL_TCTNLD_DOC
	from TBL_TCTNLD TBL_TCTNLD
		left join
		(select tnld.Id, tnld.nGiaTri, tnld.sMaCB, tnld.sMaCBo, tnld.sMaCheDo, tnld.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTroCap1Lan
		from TBL_TCTNLD tnld left join TL_CanBo_CheDoBHXH chedo on tnld.sMaCBo = chedo.sMaCanBo and tnld.sMaCheDo = chedo.sMaCheDo
		where tnld.sMaCheDo = 'TAINANLD_TROCAP1LAN') TROCAP1LAN
		on TBL_TCTNLD.sMaCBo = TROCAP1LAN.sMaCBo
		left join
		(select tnld_1.Id, tnld_1.nGiaTri, tnld_1.sMaCB, tnld_1.sMaCBo, tnld_1.sMaCheDo, tnld_1.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTroCapTheoPhieuTruyTra
		from TBL_TCTNLD tnld_1 left join TL_CanBo_CheDoBHXH chedo on tnld_1.sMaCBo = chedo.sMaCanBo and tnld_1.sMaCheDo = chedo.sMaCheDo
		where tnld_1.sMaCheDo = 'TROCAPTHEOPHIEUTRUYTRA') TROCAPTHEOPHIEUTRUYTRA
		on TBL_TCTNLD.sMaCBo = TROCAPTHEOPHIEUTRUYTRA.sMaCBo
		left join
		(select tnld_2.Id, tnld_2.nGiaTri, tnld_2.sMaCB, tnld_2.sMaCBo, tnld_2.sMaCheDo, tnld_2.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTroCapHangThang
		from TBL_TCTNLD tnld_2 left join TL_CanBo_CheDoBHXH chedo on tnld_2.sMaCBo = chedo.sMaCanBo and tnld_2.sMaCheDo = chedo.sMaCheDo
		where tnld_2.sMaCheDo = 'TROCAPHANGTHANG') TROCAPHANGTHANG
		on TBL_TCTNLD.sMaCBo = TROCAPHANGTHANG.sMaCBo
		left join
		(select tnld_3.Id, sum(tnld_3.nGiaTri) nGiaTri, tnld_3.sMaCB, tnld_3.sMaCBo, tnld_3.sMaCheDo, tnld_3.sTenCbo, sum(chedo.fSoNgayHuongBHXH) SoNgayTroCapPHCN
		from TBL_TCTNLD tnld_3 left join TL_CanBo_CheDoBHXH chedo on tnld_3.sMaCBo = chedo.sMaCanBo and tnld_3.sTenCbo = chedo.sMaCheDo
		where tnld_3.sMaCheDo in ('TROCAPPHCN', 'TROCAPPHUCVU')
		group by tnld_3.Id, tnld_3.sMaCB, tnld_3.sMaCBo, tnld_3.sMaCheDo, tnld_3.sTenCbo) TROCAPPHCN
		on TBL_TCTNLD.sMaCBo = TROCAPPHCN.sMaCBo
		left join
		(select tnld_4.Id, tnld_4.nGiaTri, tnld_4.sMaCB, tnld_4.sMaCBo, tnld_4.sMaCheDo, tnld_4.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTroCapChetDoTNLD
		from TBL_TCTNLD tnld_4 left join TL_CanBo_CheDoBHXH chedo on tnld_4.sMaCBo = chedo.sMaCanBo and tnld_4.sMaCheDo = chedo.sMaCheDo
		where tnld_4.sMaCheDo = 'TROCAPCHETDOTNLD') TROCAPCHETDOTNLD
		on TBL_TCTNLD.sMaCBo = TROCAPCHETDOTNLD.sMaCBo
		left join
		(select tnld_5.Id, tnld_5.nGiaTri, tnld_5.sMaCB, tnld_5.sMaCBo, tnld_5.sMaCheDo, tnld_5.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayDuongSucTNLD
		from TBL_TCTNLD tnld_5 left join TL_CanBo_CheDoBHXH chedo on tnld_5.sMaCBo = chedo.sMaCanBo and tnld_5.sMaCheDo = chedo.sMaCheDo
		where tnld_5.sMaCheDo = 'TAINANLĐ_DUONGSUCPHSK') TAINANLĐ_DUONGSUCPHSK
		on TBL_TCTNLD.sMaCBo = TAINANLĐ_DUONGSUCPHSK.sMaCBo
	where TBL_TCTNLD.sMaCheDo = 'CHIGIAMDINH'

	--Lấy lương Sĩ quan
	select * into TBL_TCTNLD_SQ from
	(select
		1 RowNum, 'I' STT, 'SQ' DoiTuong, 'SQ' LoaiDoiTuong, null Id, null sMaCB, null sMaCBo, null sMaCheDo, null sTenCbo, null SoNgayDuongSucTNLD, null fChiGiamDinh, null fTroCap1Lan, null fTroCapTheoPhieuTruyTra, null fTroCapHangThang, null fTroCapPHCN, null fTroCapChetDoTNLD, null fDuongSucTNLD
	union
	select
		1 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'SQ' LoaiDoiTuong, Id, sMaCB, sMaCBo, sMaCheDo, sTenCbo, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fTroCapChetDoTNLD, fDuongSucTNLD
	from TBL_TCTNLD_DOC
	where sMaCB like '1%') sq

	--Lấy lương QNCN
	select * into TBL_TCTNLD_QNCN from
	(select
		2 RowNum, 'II' STT, 'QNCN' DoiTuong, 'QNCN' LoaiDoiTuong, null Id, null sMaCB, null sMaCBo, null sMaCheDo, null sTenCbo, null SoNgayDuongSucTNLD, null fChiGiamDinh, null fTroCap1Lan, null fTroCapTheoPhieuTruyTra, null fTroCapHangThang, null fTroCapPHCN, null fTroCapChetDoTNLD, null fDuongSucTNLD
	union
	select
		2 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'QNCN' LoaiDoiTuong, Id, sMaCB, sMaCBo, sMaCheDo, sTenCbo, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fTroCapChetDoTNLD, fDuongSucTNLD
	from TBL_TCTNLD_DOC
	where sMaCB like '2%') qncn

	--Lấy lương HSQ_BS
	select * into TBL_TCTNLD_HSQBS from
	(select
		3 RowNum, 'III' STT, 'HSQ_BS' DoiTuong, 'HSQ_BS' LoaiDoiTuong, null Id, null sMaCB, null sMaCBo, null sMaCheDo, null sTenCbo, null SoNgayDuongSucTNLD, null fChiGiamDinh, null fTroCap1Lan, null fTroCapTheoPhieuTruyTra, null fTroCapHangThang, null fTroCapPHCN, null fTroCapChetDoTNLD, null fDuongSucTNLD
	union
	select
		3 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'HSQ_BS' LoaiDoiTuong, Id, sMaCB, sMaCBo, sMaCheDo, sTenCbo, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fTroCapChetDoTNLD, fDuongSucTNLD
	from TBL_TCTNLD_DOC
	where sMaCB like '0%') hsqbs

	--Lấy lương VCQP
	select * into TBL_TCTNLD_VCQP from
	(select
		4 RowNum, 'IV' STT, 'VCQP' DoiTuong, 'VCQP' LoaiDoiTuong, null Id, null sMaCB, null sMaCBo, null sMaCheDo, null sTenCbo, null SoNgayDuongSucTNLD, null fChiGiamDinh, null fTroCap1Lan, null fTroCapTheoPhieuTruyTra, null fTroCapHangThang, null fTroCapPHCN, null fTroCapChetDoTNLD, null fDuongSucTNLD
	union
	select
		4 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'VCQP' LoaiDoiTuong, Id, sMaCB, sMaCBo, sMaCheDo, sTenCbo, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fTroCapChetDoTNLD, fDuongSucTNLD
	from TBL_TCTNLD_DOC
	where sMaCB in ('3.1', '3.2', '3.3')) vcqp

	--Lấy lương Lao động hợp đông
	select * into TBL_TCTNLD_LDHD from
	(select
		5 RowNum, 'V' STT, 'LDHD' DoiTuong, 'LDHD' LoaiDoiTuong, null Id, null sMaCB, null sMaCBo, null sMaCheDo, null sTenCbo, null SoNgayDuongSucTNLD, null fChiGiamDinh, null fTroCap1Lan, null fTroCapTheoPhieuTruyTra, null fTroCapHangThang, null fTroCapPHCN, null fTroCapChetDoTNLD, null fDuongSucTNLD
	union
	select
		5 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'LDHD' LoaiDoiTuong, Id, sMaCB, sMaCBo, sMaCheDo, sTenCbo, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fTroCapChetDoTNLD, fDuongSucTNLD
	from TBL_TCTNLD_DOC
	where sMaCB = '43') ldhd

	--Ket qua
	select result.* into TBL_TCTNLD_RESULT from
	(select
	RowNum, STT, DoiTuong, LoaiDoiTuong, Id, sMaCB, sMaCBo, sTenCbo, SoNgayDuongSucTNLD, sMaCheDo, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fTroCapChetDoTNLD, fDuongSucTNLD,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) fTongSoTien
	from TBL_TCTNLD_SQ
	union
	select
	RowNum, STT, DoiTuong, LoaiDoiTuong, Id, sMaCB, sMaCBo, sTenCbo, SoNgayDuongSucTNLD,sMaCheDo, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fTroCapChetDoTNLD, fDuongSucTNLD,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fTroCapChetDoTNLD, 0)) fTongSoTien
	from TBL_TCTNLD_QNCN
	union
	select
	RowNum, STT, DoiTuong, LoaiDoiTuong, Id, sMaCB, sMaCBo, sTenCbo, SoNgayDuongSucTNLD, sMaCheDo, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fTroCapChetDoTNLD, fDuongSucTNLD,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fTroCapChetDoTNLD, 0)) fTongSoTien
	from TBL_TCTNLD_HSQBS
	union
	select
	RowNum, STT, DoiTuong, LoaiDoiTuong, Id, sMaCB, sMaCBo, sTenCbo, SoNgayDuongSucTNLD, sMaCheDo, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fTroCapChetDoTNLD, fDuongSucTNLD,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fTroCapChetDoTNLD, 0)) fTongSoTien
	from TBL_TCTNLD_VCQP
	union
	select
	RowNum, STT, DoiTuong, LoaiDoiTuong, Id, sMaCB, sMaCBo, sTenCbo, SoNgayDuongSucTNLD, sMaCheDo, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fTroCapChetDoTNLD, fDuongSucTNLD,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fTroCapChetDoTNLD, 0)) fTongSoTien
	from TBL_TCTNLD_LDHD) result

	select
		RowNum,
		STT,
		DoiTuong, 
		LoaiDoiTuong,
		sMaCB MaCb, 
		sMaCBo MaCbo, 
		sTenCbo TenCbo, 
		SoNgayDuongSucTNLD,
		fChiGiamDinh/@DonViTinh fChiGiamDinh,
		fTroCap1Lan/@DonViTinh fTroCap1Lan,
		fTroCapTheoPhieuTruyTra/@DonViTinh fTroCapTheoPhieuTruyTra,
		fTroCapHangThang/@DonViTinh fTroCapHangThang,
		fTroCapPHCN/@DonViTinh fTroCapPHCN,
		fTroCapChetDoTNLD/@DonViTinh fTroCapChetDoTNLD,
		fDuongSucTNLD/@DonViTinh fDuongSucTNLD,
		fTongSoTien/@DonViTinh FTongSoTien
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
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_xuat_ngu]    Script Date: 11/9/2023 5:10:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_xuat_ngu]
	@MaDonVi nvarchar(100),
	@NamLamViec int,
	@Thang int,
	@DonViTinh int
AS
BEGIN
	--Lay thong tin luong theo tro cap thai san
	select * into TBL_TCXN from
	(select luong.*
	from TL_BangLuong_ThangBHXH luong
	where 
	luong.sMaDonVi = @MaDonVi
	and luong.iNam = @NamLamViec
	and luong.iThang = @Thang
	and luong.sMaCheDo in ('XUATNGU_TROCAP1LAN')) tcod

	--Lấy lương Sĩ quan
	select * into TBL_TCXN_SQ from
	(select CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, Id, sMaCB, sMaCBo, sMaCheDo, sTenCbo, nGiaTri
	from TBL_TCXN
	where sMaCB like '1%') sq

	--Lấy lương QNCN
	select * into TBL_TCXN_QNCN from
	(select CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, Id, sMaCB, sMaCBo, sMaCheDo, sTenCbo, nGiaTri
	from TBL_TCXN
	where sMaCB like '2%') qncn

	--Lấy lương HSQ_BS
	select * into TBL_TCXN_HSQBS from
	(select CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, Id, sMaCB, sMaCBo, sMaCheDo, sTenCbo, nGiaTri
	from TBL_TCXN
	where sMaCB like '0%') hsqbs

	--Lấy lương VCQP
	select * into TBL_TCXN_VCQP from
	(select CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, Id, sMaCB, sMaCBo, sMaCheDo, sTenCbo, nGiaTri
	from TBL_TCXN
	where sMaCB in ('3.1', '3.2', '3.3')) vcqp

	--Lấy lương Lao động hợp đông
	select * into TBL_TCXN_LDHD from
	(select CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, Id, sMaCB, sMaCBo, sMaCheDo, sTenCbo, nGiaTri
	from TBL_TCXN
	where sMaCB = '43') ldhd

	--Ket qua
	select result.* into TBL_TCXN_RESULT from
	(select STT, Id, sMaCB, sMaCBo, sTenCbo, nGiaTri from TBL_TCXN_SQ
	union
	select STT, Id, sMaCB, sMaCBo, sTenCbo, nGiaTri from TBL_TCXN_QNCN
	union
	select STT, Id, sMaCB, sMaCBo, sTenCbo, nGiaTri from TBL_TCXN_HSQBS
	union
	select STT,Id, sMaCB, sMaCBo, sTenCbo, nGiaTri from TBL_TCXN_VCQP
	union
	select STT, Id, sMaCB, sMaCBo, sTenCbo, nGiaTri from TBL_TCXN_LDHD) result

	select
		STT,
		sMaCB MaCb, 
		sMaCBo MaCbo, 
		sTenCbo TenCbo, 
		nGiaTri/@DonViTinh FTroCap1Lan
	from TBL_TCXN_RESULT
	
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCXN]') AND type in (N'U'))
	drop table TBL_TCXN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCXN_SQ]') AND type in (N'U'))
	drop table TBL_TCXN_SQ;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCXN_QNCN]') AND type in (N'U'))
	drop table TBL_TCXN_QNCN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCXN_HSQBS]') AND type in (N'U'))
	drop table TBL_TCXN_HSQBS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCXN_VCQP]') AND type in (N'U'))
	drop table TBL_TCXN_VCQP;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCXN_LDHD]') AND type in (N'U'))
	drop table TBL_TCXN_LDHD;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCXN_RESULT]') AND type in (N'U'))
	drop table TBL_TCXN_RESULT;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh]    Script Date: 11/9/2023 5:10:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh]
@ChungTuId NVARCHAR(255),
@DonViTinh int
AS
BEGIN
--BHXH
--Lấy dữ liệu NLĐ, NSD của khối dự toán
select child.* into tbl_child from
(
select
5 STT,
N'5' MaSo,
N'- Người lao động đóng' NoiDung,
sum(fThuBHXH_NLD) DttDauNam,
sum(fThuBHXH_NLD_QTDauNam) Dtt6ThangDauNam,
sum(fThuBHXH_NLD_QTCuoiNam) Dtt6ThangCuoiNam,
sum(fThuBHXH_NLD_QTDauNam) + sum(fThuBHXH_NLD_QTCuoiNam) TongCong,
((sum(fThuBHXH_NLD_QTDauNam) + sum(fThuBHXH_NLD_QTCuoiNam)) - sum(fThuBHXH_NLD)) Tang,
(sum(fThuBHXH_NLD) - (sum(fThuBHXH_NLD_QTDauNam) + sum(fThuBHXH_NLD_QTCuoiNam))) Giam,
'DT' Khoi,
'BHXH' Thu,
'NLD' Type,
0 BHangCha
from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
where 
ctct.iID_DTT_BHXH_DieuChinh = @ChungTuId
and ctct.sLNS = '9020001' -- Khối dự toán
union
select
6 STT,
N'6' MaSo,
N'- Người sử dụng LĐ đóng' NoiDung,
sum(fThuBHXH_NSD) BhxhNsdDauNam,
sum(fThuBHXH_NSD_QTDauNam) BhxhNsd6ThangDauNam,
sum(fThuBHXH_NSD_QTCuoiNam) BhxhNsd6ThangCuoiNam,
sum(fThuBHXH_NSD_QTDauNam) + sum(fThuBHXH_NSD_QTCuoiNam) TongCong,
((sum(fThuBHXH_NSD_QTDauNam) + sum(fThuBHXH_NSD_QTCuoiNam)) - sum(fThuBHXH_NSD)) Tang,
(sum(fThuBHXH_NSD) - (sum(fThuBHXH_NSD_QTDauNam) + sum(fThuBHXH_NSD_QTCuoiNam))) Giam,
'DT' Khoi,
'BHXH' Thu,
'NSD' Type,
0 BHangCha
from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
where 
ctct.iID_DTT_BHXH_DieuChinh = @ChungTuId
and ctct.sLNS = '9020001' -- Khối dự toán

--Lấy dữ liệu NLĐ, NSD khối hạch toán
union
select
8 STT,
N'8' MaSo,
N'- Người lao động đóng' NoiDung,
sum(fThuBHXH_NLD) DttDauNam,
sum(fThuBHXH_NLD_QTDauNam) Dtt6ThangDauNam,
sum(fThuBHXH_NLD_QTCuoiNam) Dtt6ThangCuoiNam,
sum(fThuBHXH_NLD_QTDauNam) + sum(fThuBHXH_NLD_QTCuoiNam) TongCong,
((sum(fThuBHXH_NLD_QTDauNam) + sum(fThuBHXH_NLD_QTCuoiNam)) - sum(fThuBHXH_NLD)) Tang,
(sum(fThuBHXH_NLD) - (sum(fThuBHXH_NLD_QTDauNam) + sum(fThuBHXH_NLD_QTCuoiNam))) Giam,
'HT' Khoi,
'BHXH' Thu,
'NLD' Type,
0 BHangCha
from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
where 
ctct.iID_DTT_BHXH_DieuChinh = @ChungTuId
and ctct.sLNS = '9020002' -- Khối hạch toán
union
select
9 STT,
N'9' MaSo,
N'- Người sử dụng LĐ đóng' NoiDung,
sum(fThuBHXH_NSD) BhxhNsdDauNam,
sum(fThuBHXH_NSD_QTDauNam) Dtt6ThangDauNam,
sum(fThuBHXH_NSD_QTCuoiNam) Dtt6ThangCuoiNam,
sum(fThuBHXH_NSD_QTDauNam) + sum(fThuBHXH_NSD_QTCuoiNam) TongCong,
((sum(fThuBHXH_NSD_QTDauNam) + sum(fThuBHXH_NSD_QTCuoiNam)) - sum(fThuBHXH_NSD)) Tang,
(sum(fThuBHXH_NSD) - (sum(fThuBHXH_NSD_QTDauNam) + sum(fThuBHXH_NSD_QTCuoiNam))) Giam,
N'HT' Khoi,
N'BHXH' Thu,
'NSD' Type,
0 BHangCha
from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
where 
ctct.iID_DTT_BHXH_DieuChinh = @ChungTuId
and ctct.sLNS = '9020002' -- Khối hạch toán
-----------------------------------------
--BHYT
-- Khối dự toán
union
select
16 STT,
N'16' MaSo,
N'- BHYT quân nhân' NoiDung,
sum(0) DttDauNam,
sum(0) Dtt6ThangDauNam,
sum(0) Dtt6ThangCuoiNam,
sum(0) TongCong,
sum(0) Tang,
sum(0) Giam,
'DT' Khoi,
'BHYT' Thu,
'QN' Type,
0 BHangCha
from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
where 
ctct.iID_DTT_BHXH_DieuChinh = @ChungTuId
and ctct.sLNS = '9020001' -- Khối dự toán
and ctct.sXauNoiMa in
(
'9020001-010-011-0001-0000',  -- Sĩ quan
'9020001-010-011-0001-0001'	, -- QNCN
'9020001-010-011-0001-0002'  -- HSQ, BS
)
union
select
18 STT,
N'18' MaSo,
N'+ Người lao động đóng' NoiDung,
sum(fThuBHYT_NLD) DttDauNam,
sum(fThuBHYT_NLD_QTDauNam) Dtt6ThangDauNam,
sum(fThuBHYT_NLD_QTCuoiNam) Dtt6ThangCuoiNam,
sum(fThuBHYT_NLD_QTDauNam) + sum(fThuBHYT_NLD_QTCuoiNam) TongCong,
((sum(fThuBHYT_NLD_QTDauNam) + sum(fThuBHYT_NLD_QTCuoiNam)) - sum(fThuBHYT_NLD)) Tang,
(sum(fThuBHYT_NLD) - (sum(fThuBHYT_NLD_QTDauNam) + sum(fThuBHYT_NLD_QTCuoiNam))) Giam,
'DT' Khoi,
'BHYT' Thu,
'NLD' Type,
0 BHangCha
from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
where 
ctct.iID_DTT_BHXH_DieuChinh = @ChungTuId
and ctct.sLNS = '9020001' -- Khối dự toán
and ctct.sXauNoiMa in
(
'9020001-010-011-0002-0000',  -- CC,CN, VCQP
'9020001-010-011-0002-0001'  -- LĐHĐ
)
union
select
19 STT,
N'19' MaSo,
N'+ Người sử dụng LĐ đóng' NoiDung,
sum(fThuBHYT_NSD) DttDauNam,
sum(fThuBHYT_NSD_QTDauNam) Dtt6ThangDauNam,
sum(fThuBHYT_NSD_QTCuoiNam) Dtt6ThangCuoiNam,
sum(fThuBHYT_NSD_QTDauNam) + sum(fThuBHYT_NSD_QTCuoiNam) TongCong,
((sum(fThuBHYT_NSD_QTDauNam) + sum(fThuBHYT_NSD_QTCuoiNam)) - sum(fThuBHYT_NSD)) Tang,
(sum(fThuBHYT_NSD) - (sum(fThuBHYT_NSD_QTDauNam) + sum(fThuBHYT_NSD_QTCuoiNam))) Giam,
'DT' Khoi,
'BHYT' Thu,
'NSD' Type,
0 BHangCha
from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
where 
ctct.iID_DTT_BHXH_DieuChinh = @ChungTuId
and ctct.sLNS = '9020001' -- Khối dự toán
and ctct.sXauNoiMa in
(
'9020001-010-011-0002-0000',  -- CC,CN, VCQP
'9020001-010-011-0002-0001'  -- LĐHĐ
)
-- Khối hạch toán
union
select
21 STT,
N'21' MaSo,
N'- BHYT quân nhân' NoiDung,
sum(0) DttDauNam,
sum(0) Dtt6ThangDauNam,
sum(0) Dtt6ThangCuoiNam,
sum(0) TongCong,
sum(0) Tang,
sum(0) Giam,
'HT' Khoi,
'BHYT' Thu,
'QN' Type,
0 BHangCha
from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
where 
ctct.iID_DTT_BHXH_DieuChinh = @ChungTuId
and ctct.sLNS = '9020002' -- Khối hạch toán
and ctct.sXauNoiMa in
(
'9020002-010-011-0001-0000',  -- Sĩ quan
'9020002-010-011-0001-0001'	, -- QNCN
'9020002-010-011-0001-0002'  -- HSQ, BS
)
union
select
23 STT,
N'23' MaSo,
N'+ Người lao động đóng' NoiDung,
sum(fThuBHYT_NLD) DttDauNam,
sum(fThuBHYT_NLD_QTDauNam) Dtt6ThangDauNam,
sum(fThuBHYT_NLD_QTCuoiNam) Dtt6ThangCuoiNam,
sum(fThuBHYT_NLD_QTDauNam) + sum(fThuBHYT_NLD_QTCuoiNam) TongCong,
((sum(fThuBHYT_NLD_QTDauNam) + sum(fThuBHYT_NLD_QTCuoiNam)) - sum(fThuBHYT_NLD)) Tang,
(sum(fThuBHYT_NLD) - (sum(fThuBHYT_NLD_QTDauNam) + sum(fThuBHYT_NLD_QTCuoiNam))) Giam,
'HT' Khoi,
'BHYT' Thu,
'NLD' Type,
0 BHangCha
from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
where 
ctct.iID_DTT_BHXH_DieuChinh = @ChungTuId
and ctct.sLNS = '9020002' -- Khối hạch toán
and ctct.sXauNoiMa in
(
'9020002-010-011-0002-0000',  -- CC,CN, VCQP
'9020002-010-011-0002-0001'  -- LĐHĐ
)
union
select
24 STT,
N'24' MaSo,
N'+ Người sử dụng LĐ đóng' NoiDung,
sum(fThuBHYT_NSD) DttDauNam,
sum(fThuBHYT_NSD_QTDauNam) Dtt6ThangDauNam,
sum(fThuBHYT_NSD_QTCuoiNam) Dtt6ThangCuoiNam,
sum(fThuBHYT_NSD_QTDauNam) + sum(fThuBHYT_NSD_QTCuoiNam) TongCong,
((sum(fThuBHYT_NSD_QTDauNam) + sum(fThuBHYT_NSD_QTCuoiNam)) - sum(fThuBHYT_NSD)) Tang,
(sum(fThuBHYT_NSD) - (sum(fThuBHYT_NSD_QTDauNam) + sum(fThuBHYT_NSD_QTCuoiNam))) Giam,
'HT' Khoi,
'BHYT' Thu,
'NSD' Type,
0 BHangCha
from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
where 
ctct.iID_DTT_BHXH_DieuChinh = @ChungTuId
and ctct.sLNS = '9020002' -- Khối hạch toán
and ctct.sXauNoiMa in
(
'9020002-010-011-0002-0000',  -- CC,CN, VCQP
'9020002-010-011-0002-0001'  -- LĐHĐ
)
--BHTN
--Lấy dữ liệu khối dự toán
union
select
29 STT,
N'29' MaSo,
N'- Người lao động đóng' NoiDung,
sum(fThuBHTN_NLD) DttDauNam,
sum(fThuBHTN_NLD_QTDauNam) Dtt6ThangDauNam,
sum(fThuBHTN_NLD_QTCuoiNam) Dtt6ThangCuoiNam,
sum(fThuBHTN_NLD_QTDauNam) + sum(fThuBHTN_NLD_QTCuoiNam) TongCong,
((sum(fThuBHTN_NLD_QTDauNam) + sum(fThuBHTN_NLD_QTCuoiNam)) - sum(fThuBHTN_NLD)) Tang,
(sum(fThuBHTN_NLD) - (sum(fThuBHTN_NLD_QTDauNam) + sum(fThuBHTN_NLD_QTCuoiNam))) Giam,
'DT' Khoi,
'BHTN' Thu,
'NLD' Type,
0 BHangCha
from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
where 
ctct.iID_DTT_BHXH_DieuChinh = @ChungTuId
and ctct.sLNS = '9020001' -- Khối dự toán
union
select
30 STT,
N'30' MaSo,
N'- Người sử dụng LĐ đóng' NoiDung,
sum(fThuBHTN_NSD) BhxhNsdDauNam,
sum(fThuBHTN_NSD_QTDauNam) BhxhNsd6ThangDauNam,
sum(fThuBHTN_NSD_QTCuoiNam) BhxhNsd6ThangCuoiNam,
sum(fThuBHTN_NSD_QTDauNam) + sum(fThuBHTN_NSD_QTCuoiNam) TongCong,
((sum(fThuBHTN_NSD_QTDauNam) + sum(fThuBHTN_NSD_QTCuoiNam)) - sum(fThuBHXH_NSD)) Tang,
(sum(fThuBHTN_NSD) - (sum(fThuBHTN_NSD_QTDauNam) + sum(fThuBHTN_NSD_QTCuoiNam))) Giam,
'DT' Khoi,
'BHTN' Thu,
'NSD' Type,
0 BHangCha
from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
where 
ctct.iID_DTT_BHXH_DieuChinh = @ChungTuId
and ctct.sLNS = '9020001' -- Khối dự toán

--Lấy dữ liệu khối hạch toán
union
select
32 STT,
N'32' MaSo,
N'- Người lao động đóng' NoiDung,
sum(fThuBHTN_NLD) DttDauNam,
sum(fThuBHTN_NLD_QTDauNam) Dtt6ThangDauNam,
sum(fThuBHTN_NLD_QTCuoiNam) Dtt6ThangCuoiNam,
sum(fThuBHTN_NLD_QTDauNam) + sum(fThuBHTN_NLD_QTCuoiNam) TongCong,
((sum(fThuBHTN_NLD_QTDauNam) + sum(fThuBHTN_NLD_QTCuoiNam)) - sum(fThuBHTN_NLD)) Tang,
(sum(fThuBHTN_NLD) - (sum(fThuBHTN_NLD_QTDauNam) + sum(fThuBHTN_NLD_QTCuoiNam))) Giam,
'HT' Khoi,
'BHTN' Thu,
'NLD' Type,
0 BHangCha
from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
where 
ctct.iID_DTT_BHXH_DieuChinh = @ChungTuId
and ctct.sLNS = '9020002' -- Khối hạch toán
union
select
33 STT,
N'33' MaSo,
N'- Người sử dụng LĐ đóng' NoiDung,
sum(fThuBHTN_NSD) BhxhNsdDauNam,
sum(fThuBHTN_NSD_QTDauNam) Dtt6ThangDauNam,
sum(fThuBHTN_NSD_QTCuoiNam) Dtt6ThangCuoiNam,
sum(fThuBHTN_NSD_QTDauNam) + sum(fThuBHTN_NSD_QTCuoiNam) TongCong,
((sum(fThuBHTN_NSD_QTDauNam) + sum(fThuBHTN_NSD_QTCuoiNam)) - sum(fThuBHTN_NSD)) Tang,
(sum(fThuBHTN_NSD) - (sum(fThuBHTN_NSD_QTDauNam) + sum(fThuBHTN_NSD_QTCuoiNam))) Giam,
'HT' Khoi,
'BHTN' Thu,
'NSD' Type,
0 BHangCha
from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
where 
ctct.iID_DTT_BHXH_DieuChinh = @ChungTuId
and ctct.sLNS = '9020002' -- Khối hạch toán
) child
-----------------------------------------------------------------
--Lấy dữ liệu mục lục con
--BHXH
select child_ml.* into tbl_child_cat from
(
select
2 STT,
N'2=5+8' MaSo,
N'- Người lao động đóng' NoiDung,
sum(DttDauNam) DttDauNam,
sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
'' Khoi,
'BHXH' Thu,
'' Type,
0 BHangCha
from tbl_child
where Thu = 'BHXH'
and Type = 'NLD'
union
select
3 STT,
N'3=6+9' MaSo,
N'- Người sử dụng LĐ đóng' NoiDung,
sum(DttDauNam) DttDauNam,
sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
'' Khoi,
N'BHXH' Thu,
'' Type,
0 BHangCha
from tbl_child
where Thu = 'BHXH'
and Type = 'NSD'
union
select
4 STT,
N'4=5+6' MaSo,
N'a) Khối dự toán' NoiDung,
sum(DttDauNam) DttDauNam,
sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
'DT' Khoi,
'BHXH' Thu,
'BHXH' Type,
0 BHangCha
from tbl_child
where Thu = 'BHXH'
and Khoi = 'DT'
union
select
7 STT,
N'7=8+9' MaSo,
N'a) Khối hạch toán' NoiDung,
sum(DttDauNam) DttDauNam,
sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
'HT' Khoi,
'BHXH' Thu,
'BHXH' Type,
0 BHangCha
from tbl_child
where Thu = 'BHXH'
and Khoi = 'HT'
--BHYT
union
select
11 STT,
N'11=16+21' MaSo,
N'- BHYT quân nhân' NoiDung,
sum(DttDauNam) DttDauNam,
sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
'' Khoi,
'BHYT' Thu,
'QN' Type,
0 BHangCha
from tbl_child
where Thu = 'BHYT'
and Type = 'QN'
union
select
13 STT,
N'13=18+23' MaSo,
N'+ Người lao động đóng' NoiDung,
sum(DttDauNam) DttDauNam,
sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
'' Khoi,
'BHYT' Thu,
'LDHD' Type,
0 BHangCha
from tbl_child
where Thu = 'BHYT'
and Type = 'NLD'
union
select
14 STT,
N'14=19+24' MaSo,
N'+ Người sử dụng LĐ đóng' NoiDung,
sum(DttDauNam) DttDauNam,
sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
'' Khoi,
'BHYT' Thu,
'LDHD' Type,
0 BHangCha
from tbl_child
where Thu = 'BHYT'
and Type = 'NSD'
union
select
17 STT,
N'17=18+19' MaSo,
N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
sum(DttDauNam) DttDauNam,
sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
'DT' Khoi,
'BHYT' Thu,
'' Type,
0 BHangCha
from tbl_child
where Thu = 'BHYT'
and Khoi = 'DT'
union
select
22 STT,
N'22=23+24' MaSo,
N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
sum(DttDauNam) DttDauNam,
sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
'HT' Khoi,
'BHYT' Thu,
'' Type,
0 BHangCha
from tbl_child
where Thu = 'BHYT'
and Khoi = 'HT'
--BHTN
union
select
26 STT,
N'26=29+32' MaSo,
N'- Người lao động đóng' NoiDung,
sum(DttDauNam) DttDauNam,
sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
'' Khoi,
'BHTN' Thu,
'' Type,
0 BHangCha
from tbl_child
where Thu = 'BHTN'
and Type = 'NLD'
union
select
27 STT,
N'27=30+33' MaSo,
N'- Người sử dụng LĐ đóng' NoiDung,
sum(DttDauNam) DttDauNam,
sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
'' Khoi,
'BHTN' Thu,
'' Type,
0 BHangCha
from tbl_child
where Thu = 'BHTN'
and Type = 'NSD'
union
select
28 STT,
N'28=29+330' MaSo,
N'a) Khối dự toán' NoiDung,
sum(DttDauNam) DttDauNam,
sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
'DT' Khoi,
'BHTN' Thu,
'BHTN' Type,
0 BHangCha
from tbl_child
where Thu = 'BHTN'
and Khoi = 'DT'
union
select
31 STT,
N'31=32+33' MaSo,
N'a) Khối hạch toán' NoiDung,
sum(DttDauNam) DttDauNam,
sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
'HT' Khoi,
'BHTN' Thu,
'BHTN' Type,
0 BHangCha
from tbl_child
where Thu = 'BHTN'
and Khoi = 'HT'
) child_ml

select tmp.* into temp1
from
(
select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_child
union
select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_child_cat
) tmp
-----------------------------------------------------------------
-- Lấy dữ liệu mục lục cha
select parent.* into tbl_parent from (
select
1 STT,
N'1=4+7' MaSo,
N'1. Thu Bảo hiểm xã hội' NoiDung,
sum(DttDauNam) DttDauNam,
sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
'' Khoi,
'BHXH' Thu,
'' Type,
1 BHangCha
from tbl_child_cat
where Thu = 'BHXH'
and Type = 'BHXH'
union
select
25 STT,
N'25=28+31' MaSo,
N'3. Thu Bảo hiểm thất nghiệp' NoiDung,
sum(DttDauNam) DttDauNam,
sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
'' Khoi,
'BHXH' Thu,
'' Type,
1 BHangCha
from tbl_child_cat
where Thu = 'BHTN'
and Type = 'BHTN'
union
select
12 STT,
N'12=13+14' MaSo,
N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
sum(DttDauNam) DttDauNam,
sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
'' Khoi,
'BHYT' Thu,
'LDHD' Type,
0 BHangCha
from tbl_child_cat
where Thu = 'BHYT'
and Type = 'LDHD'
union
select
15 STT,
N'15=16+17' MaSo,
N'a) Khối dự toán' NoiDung,
sum(DttDauNam) DttDauNam,
sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
'DT' Khoi,
'BHYT' Thu,
'BHYT' Type,
0 BHangCha
from tbl_child_cat
where STT in (16, 17)
union
select
20 STT,
N'20=21+22' MaSo,
N'b) Khối hạch toán' NoiDung,
sum(DttDauNam) DttDauNam,
sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
'DT' Khoi,
'BHYT' Thu,
'BHYT' Type,
0 BHangCha
from tbl_child_cat
where STT in (21, 22)
) parent

--------------------------------------------

select result.* into tbl_ddt_bhxh from
(
select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from temp1
union
select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_parent
union
-- Lấy tổng số thu BHYT
select
10 STT,
N'10=15+20' MaSo,
N'2. Thu Bảo hiểm y tế' NoiDung,
sum(DttDauNam) DttDauNam,
sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
(sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) TongCong,
((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
'' Khoi,
'BHYT' Thu,
'BHYT' Type,
1 BHangCha
from tbl_parent
where STT in (15, 20)
) result
order by result.STT;

select STT, MaSo, NoiDung, DttDauNam/@DonViTinh DttDauNam, Dtt6ThangDauNam/@DonViTinh Dtt6ThangDauNam, Dtt6ThangCuoiNam/@DonViTinh Dtt6ThangCuoiNam, TongCong/@DonViTinh TongCong, Tang/@DonViTinh Tang, Giam/@DonViTinh Giam, Khoi, Thu, Type, BHangCha from tbl_ddt_bhxh;

---------------------------------------------
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child]') AND type in (N'U')) drop table tbl_child;
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child_cat]') AND type in (N'U')) drop table tbl_child_cat;
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp1]') AND type in (N'U')) drop table temp1;
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_parent]') AND type in (N'U')) drop table tbl_parent;
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_ddt_bhxh]') AND type in (N'U')) drop table tbl_ddt_bhxh;

END
GO
