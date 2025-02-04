/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_om_dau]    Script Date: 11/2/2023 5:21:12 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_luong_tro_cap_om_dau]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_om_dau]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_lay_luong_ke_hoach]    Script Date: 11/2/2023 5:21:12 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_lay_luong_ke_hoach]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_lay_luong_ke_hoach]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_get_quan_so_binh_quan_nam]    Script Date: 11/2/2023 5:21:12 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_get_quan_so_binh_quan_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_get_quan_so_binh_quan_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_giai_thich_tong_hop_so_sanh]    Script Date: 11/2/2023 5:21:12 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_giai_thich_tong_hop_so_sanh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_giai_thich_tong_hop_so_sanh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_giai_thich_tong_hop_so_sanh]    Script Date: 11/2/2023 5:21:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_giai_thich_tong_hop_so_sanh]
	@NamLamViec int
AS
BEGIN
	select
	mlns.iID_MLGT,
	mlns.iSTT,
	mlns.sMoTa,
	mlns.sNoiDung,
	mlns.iLoai,
	chungtudonvi.fSoPhaiThuNop,
	chungtudonvi.fSoDaNopTrongNam,
	chungtudonvi.fSoDaNopSau31_12,
	chungtudonvi.fTongSoDaNop,
	chungtudonvi.fSoConPhaiNop,
	chungtudonvi.iQuanSo,
	chungtudonvi.fQuyTienLuongCanCu,
	chungtudonvi.fSoTienGiamDong,
	chungtudonvi.dTuNgay,
	chungtudonvi.dDenNgay
	from
		(select
			iSTT,
			sNoiDung,
			iID_MLGT,
			concat(iSTT, '. ' , sNoiDung) sMoTa,
			iLoai
		from BH_QTT_MucLucGiaiThich) mlns
		left join
			(select distinct
				ctgt.iID_QT_CTCT_GiaiThich,
				ctgt.iID_QTT_BHXH_ChungTu,
				ctgt.sNguoiTao,
				ctgt.sNguoiSua,
				ctgt.dNgayTao,
				ctgt.dNgaySua,
				ctgt.iID_MaDonVi,
				ctgt.iNamLamViec,
				ctgt.iQuyNam,
				ctgt.iQuyNamLoai,
				ctgt.sQuyNamMoTa,
				ctgt.iID_MLNS,
				ctgt.sNoiDung,
				ctgt.fSoPhaiThuNop,
				ctgt.fSoDaNopTrongNam,
				ctgt.fSoDaNopSau31_12,
				ctgt.fTongSoDaNop,
				ctgt.fSoConPhaiNop,
				ctgt.iQuanSo,
				ctgt.fQuyTienLuongCanCu,
				ctgt.fSoTienGiamDong,
				ctgt.dTuNgay,
				ctgt.dDenNgay,
				ctgt.iLoaiGiaiThich
			from BH_QTT_BHXH_CTCT_GiaiThich ctgt
			where ctgt.iQuyNam = @NamLamViec) chungtudonvi
		on mlns.iID_MLGT = chungtudonvi.iID_MLNS
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_get_quan_so_binh_quan_nam]    Script Date: 11/2/2023 5:21:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_bhxh_get_quan_so_binh_quan_nam]
	@YearOfWork int
AS
BEGIN
	declare @LuongKeHoach table (Id uniqueidentifier,Nam int, Ma_CanBo varchar(20), Ma_PhuCap nvarchar(50), Ma_CB varchar(20), Gia_Tri numeric(15, 4));

	INSERT INTO @LuongKeHoach (Nam, Ma_CanBo, Ma_CB)
	SELECT DISTINCT Nam, Ma_CanBo, Ma_CB
		FROM TL_BangLuong_KeHoach 
		WHERE Nam = @YearOfWork

		SELECT '9020001-010-011-0001-0000' XauNoiMa,
		count(1)/12 AS QSBQ 
		FROM @LuongKeHoach 
		WHERE Ma_CB LIKE '1%' --Lấy quân số bình quân năm của cấp bậc Sĩ quan
		
		UNION
		SELECT '9020001-010-011-0001-0001',
		count(1)/12 AS QSBQ_QNCN FROM @LuongKeHoach 
		where Ma_CB LIKE '2%' --Lấy quân số bình quân năm của cấp bậc Quân nhân chuyên nghiệp
		
		UNION
		SELECT '9020001-010-011-0001-0002',
		count(1)/12 AS QSBQ_HSQ FROM @LuongKeHoach 
		where Ma_CB LIKE '0%' --Lấy quân số bình quân năm của cấp bậc Hạ sĩ quan
		
		UNION
		SELECT '9020001-010-011-0002-0000',
		count(1)/12 AS QSBQ_VCQP FROM @LuongKeHoach 
		where Ma_CB in ('3.1', '3.2', '3.3') --Lấy quân số bình quân năm của cấp bậc CC, CN, VCQP
		
		UNION
		SELECT '9020001-010-011-0002-0001',
		count(1)/12 AS QSBQ_LDHD FROM @LuongKeHoach 
		where Ma_CB = '43' --Lấy quân số bình quân năm của cấp bậc LDHD
		
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_lay_luong_ke_hoach]    Script Date: 11/2/2023 5:21:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bhxh_lay_luong_ke_hoach]
	@NamLamViec int,
	@LuongChinh nvarchar(50),
	@PhuCapCV nvarchar(50),
	@PhuCapTNN nvarchar(50),
	@PhuCapTNVK nvarchar(50)
AS
BEGIN
declare @TBL_SiQuan table (TenPhuCap nvarchar(20), MaPhuCap nvarchar(200), GiaTri float);
declare @TBL_QNCN table (TenPhuCap nvarchar(20), MaPhuCap nvarchar(200), GiaTri float);
declare @TBL_HSQBS table (TenPhuCap nvarchar(20), MaPhuCap nvarchar(200), GiaTri float);
declare @TBL_VCQP table (TenPhuCap nvarchar(20), MaPhuCap nvarchar(200), GiaTri float);
declare @TBL_LDHD table (TenPhuCap nvarchar(20), MaPhuCap nvarchar(200), GiaTri float);

		--Lấy lương Sĩ quan
		SELECT
		'9020001-010-011-0001-0000' XauNoiMa,
		LKH.Ma_PhuCap MaPhuCap,
		SUM(IsNull(LKH.Gia_Tri, 0)) GiaTri
		FROM
			(SELECT 
				Id, 
				Ma_CB, 
				Ma_Hieu_CanBo, 
				Ma_PhuCap, 
				Gia_Tri, 
				Nam
			FROM TL_BangLuong_KeHoach
			WHERE
				Nam = @NamLamViec
				AND ((Ma_CB LIKE '1%' AND Ma_PhuCap in (@LuongChinh, @PhuCapCV, @PhuCapTNN, @PhuCapTNVK)))) LKH
			GROUP BY
				LKH.Ma_PhuCap,
				LKH.Nam

		--Lấy lương QNCN
		UNION
		SELECT
			'9020001-010-011-0001-0001',
			LKH.Ma_PhuCap MaPhuCap,
			SUM(IsNull(LKH.Gia_Tri, 0)) GiaTri
		FROM
			(SELECT 
			Id, 
			Ma_CB, 
			Ma_Hieu_CanBo, 
			Ma_PhuCap, 
			Gia_Tri, 
			Nam 
		FROM TL_BangLuong_KeHoach
		WHERE
			Nam = @NamLamViec
			AND ((Ma_CB LIKE '2%' AND Ma_PhuCap in (@LuongChinh, @PhuCapCV, @PhuCapTNN, @PhuCapTNVK)))) LKH
		GROUP BY
			LKH.Ma_PhuCap,
			LKH.Nam

		--Lấy lương HSQ_BS
		UNION
		SELECT
			'9020001-010-011-0001-0002',
			LKH.Ma_PhuCap MaPhuCap,
			SUM(IsNull(LKH.Gia_Tri, 0)) GiaTri
		FROM
			(SELECT 
			Id, 
			Ma_CB, 
			Ma_Hieu_CanBo, 
			Ma_PhuCap, 
			Gia_Tri, 
			Nam 
		FROM TL_BangLuong_KeHoach
		WHERE
			Nam = @NamLamViec
			AND ((Ma_CB LIKE '0%' AND Ma_PhuCap in (@LuongChinh, @PhuCapCV, @PhuCapTNN, @PhuCapTNVK)))) LKH
		GROUP BY
			LKH.Ma_PhuCap,
			LKH.Nam

		--Lấy lương VCQP
		UNION
		SELECT
			'9020001-010-011-0002-0000',
			LKH.Ma_PhuCap MaPhuCap,
			SUM(IsNull(LKH.Gia_Tri, 0)) GiaTri
		FROM
		(SELECT 
			Id, 
			Ma_CB, 
			Ma_Hieu_CanBo, 
			Ma_PhuCap, 
			Gia_Tri, 
			Nam 
		FROM TL_BangLuong_KeHoach
		WHERE
			Nam = @NamLamViec
			AND ((Ma_CB in ('3.1', '3.2', '3.3') AND Ma_PhuCap in (@LuongChinh, @PhuCapCV, @PhuCapTNN, @PhuCapTNVK)))) LKH
		GROUP BY
			LKH.Ma_PhuCap,
			LKH.Nam

		--Lấy lương Lao động hợp đông
		UNION
		SELECT
			'9020001-010-011-0002-0001',
			LKH.Ma_PhuCap MaPhuCap,
			SUM(IsNull(LKH.Gia_Tri, 0)) GiaTri
		FROM
			(SELECT
				Id, 
				Ma_CB, 
				Ma_Hieu_CanBo, 
				Ma_PhuCap, 
				Gia_Tri, 
				Nam 
		FROM TL_BangLuong_KeHoach
		WHERE
			Nam = @NamLamViec
			AND ((Ma_CB = '43' AND Ma_PhuCap in (@LuongChinh, @PhuCapCV, @PhuCapTNN, @PhuCapTNVK)))) LKH
		GROUP BY
			LKH.Ma_PhuCap,
			LKH.Nam;

	
END
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_om_dau]    Script Date: 11/2/2023 5:21:12 PM ******/
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
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_RESULT]') AND type in (N'U'))
	drop table TBL_RESULT;

	--Lay thong tin luong theo tro cap om dau
	select * into TBL_TCOD from
	(select luongcancu.fLuongCanCu, chedo.fSoNgayHuongBHXH, luong.*
	from TL_BangLuong_ThangBHXH luong
	join TL_CanBo_CheDoBHXH chedo on luong.Ma_CBo = chedo.sMaCanBo and luong.Ma_CheDo = chedo.sMaCheDo
	left join 
	(select Ma_CBo, NAM, thang, sum(Gia_Tri) fLuongCanCu from TL_BangLuong_Thang
		where NAM = @NamLamViec
		and thang = @Thang
		group by Ma_CBo, NAM, thang) luongcancu on luong.Ma_CBo = luongcancu.Ma_CBo
	where 
	luong.Ma_DonVi = @MaDonVi
	and luong.NAM = @NamLamViec
	and luong.thang = @Thang
	and luong.Ma_CheDo in ('BENHDAINGAY_D14NGAY', 'BENHDAINGAY_T14NGAY', 'OMKHAC_D14NGAY', 'OMKHAC_T14NGAY', 'CONOM', 'DUONGSUCPHSK')) tcod

	select TBL_TCOD.Id,
		TBL_TCOD.Ma_CB,
		TBL_TCOD.Ma_CBo,
		TBL_TCOD.Ma_CheDo,
		TBL_TCOD.Ten_Cbo,
		chedo.fSoNgayHuongBHXH SoNgayBenhDaiNgayD14Ngay,
		BENHDAINGAY_T14NGAY.SoNgayBenhDaiNgayT14Ngay,
		OMKHAC_D14NGAY.SoNgayOmKhacD14Ngay,
		OMKHAC_T14NGAY.SoNgayOmKhacT14Ngay,
		CONOM.SoNgayConOm,
		DUONGSUCPHSK.SoNgayDuongSuc,
		TBL_TCOD.fLuongCanCu,
		TBL_TCOD.Gia_Tri fBENHDAINGAY_D14NGAY,
		BENHDAINGAY_T14NGAY.Gia_Tri fBENHDAINGAY_T14NGAY,
		OMKHAC_D14NGAY.Gia_Tri fOMKHAC_D14NGAY,
		OMKHAC_T14NGAY.Gia_Tri fOMKHAC_T14NGAY,
		CONOM.Gia_Tri fCONOM,
		DUONGSUCPHSK.Gia_Tri fDUONGSUCPHSK
		into TBL_TCOD_DOC
	from TBL_TCOD TBL_TCOD left join TL_CanBo_CheDoBHXH chedo on TBL_TCOD.Ma_CBo = chedo.sMaCanBo and TBL_TCOD.Ma_CheDo = chedo.sMaCheDo
		left join
		(select tcod.Id, tcod.Gia_Tri, tcod.Ma_CB, tcod.Ma_CBo, tcod.Ma_CheDo, tcod.Ten_Cbo, chedo.fSoNgayHuongBHXH SoNgayBenhDaiNgayT14Ngay
		from TBL_TCOD tcod left join TL_CanBo_CheDoBHXH chedo on tcod.Ma_CBo = chedo.sMaCanBo and tcod.Ma_CheDo = chedo.sMaCheDo
		where Ma_CheDo = 'BENHDAINGAY_T14NGAY') BENHDAINGAY_T14NGAY
		on TBL_TCOD.Ma_CBo = BENHDAINGAY_T14NGAY.Ma_CBo
		left join
		(select tcod_1.Id, tcod_1.Gia_Tri, tcod_1.Ma_CB, tcod_1.Ma_CBo, tcod_1.Ma_CheDo, tcod_1.Ten_Cbo, chedo.fSoNgayHuongBHXH SoNgayOmKhacD14Ngay
		from TBL_TCOD tcod_1 left join TL_CanBo_CheDoBHXH chedo on tcod_1.Ma_CBo = chedo.sMaCanBo and tcod_1.Ma_CheDo = chedo.sMaCheDo
		where Ma_CheDo = 'OMKHAC_D14NGAY') OMKHAC_D14NGAY
		on TBL_TCOD.Ma_CBo = OMKHAC_D14NGAY.Ma_CBo
		left join
		(select tcod_2.Id, tcod_2.Gia_Tri, tcod_2.Ma_CB, tcod_2.Ma_CBo, tcod_2.Ma_CheDo, tcod_2.Ten_Cbo, chedo.fSoNgayHuongBHXH SoNgayOmKhacT14Ngay
		from TBL_TCOD tcod_2 left join TL_CanBo_CheDoBHXH chedo on tcod_2.Ma_CBo = chedo.sMaCanBo and tcod_2.Ma_CheDo = chedo.sMaCheDo
		where Ma_CheDo = 'OMKHAC_T14NGAY') OMKHAC_T14NGAY
		on TBL_TCOD.Ma_CBo = OMKHAC_T14NGAY.Ma_CBo
		left join
		(select conom.Id, conom.Gia_Tri, conom.Ma_CB, conom.Ma_CBo, conom.Ma_CheDo, conom.Ten_Cbo, chedo.fSoNgayHuongBHXH SoNgayConOm
		from TBL_TCOD conom left join TL_CanBo_CheDoBHXH chedo on conom.Ma_CBo = chedo.sMaCanBo and conom.Ma_CheDo = chedo.sMaCheDo
		where Ma_CheDo = 'CONOM') CONOM
		on TBL_TCOD.Ma_CBo = CONOM.Ma_CBo
		left join
		(select duongsuc.Id, duongsuc.Gia_Tri, duongsuc.Ma_CB, duongsuc.Ma_CBo, duongsuc.Ma_CheDo, duongsuc.Ten_Cbo, chedo.fSoNgayHuongBHXH SoNgayDuongSuc
		from TBL_TCOD duongsuc left join TL_CanBo_CheDoBHXH chedo on duongsuc.Ma_CBo = chedo.sMaCanBo and duongsuc.Ma_CheDo = chedo.sMaCheDo
		where Ma_CheDo = 'DUONGSUCPHSK') DUONGSUCPHSK
		on TBL_TCOD.Ma_CBo = DUONGSUCPHSK.Ma_CBo
	where TBL_TCOD.Ma_CheDo = 'BENHDAINGAY_D14NGAY'

	--Lấy lương Sĩ quan
	select * into TBL_TCOD_SQ from
	(select
		'SQ' DoiTuong, 'SQ' LoaiDoiTuong, null Id, null Ma_CB, null Ma_CBo, null Ma_CheDo, null Ten_Cbo, null SoNgayBenhDaiNgayD14Ngay, null SoNgayBenhDaiNgayT14Ngay, null SoNgayOmKhacD14Ngay, null SoNgayOmKhacT14Ngay, null SoNgayConOm, null SoNgayDuongSuc, null fLuongCanCu, null fBENHDAINGAY_D14NGAY, null fBENHDAINGAY_T14NGAY, null fOMKHAC_D14NGAY, null fOMKHAC_T14NGAY, null fCONOM, null fDUONGSUCPHSK
	union
	select
		'', 'SQ' LoaiDoiTuong, Id, Ma_CB, Ma_CBo, Ma_CheDo, Ten_Cbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK
	from TBL_TCOD_DOC
	where Ma_CB like '1%') sq

	--Lấy lương QNCN
	select * into TBL_TCOD_QNCN from
	(select
		'QNCN' DoiTuong, 'QNCN' LoaiDoiTuong, null Id, null Ma_CB, null Ma_CBo, null Ma_CheDo, null Ten_Cbo, null SoNgayBenhDaiNgayD14Ngay, null SoNgayBenhDaiNgayT14Ngay, null SoNgayOmKhacD14Ngay, null SoNgayOmKhacT14Ngay, null SoNgayConOm, null SoNgayDuongSuc, null fLuongCanCu, null fBENHDAINGAY_D14NGAY, null fBENHDAINGAY_T14NGAY, null fOMKHAC_D14NGAY, null fOMKHAC_T14NGAY, null fCONOM, null fDUONGSUCPHSK
	union
	select
		'', 'QNCN' LoaiDoiTuong, Id, Ma_CB, Ma_CBo, Ma_CheDo, Ten_Cbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK
	from TBL_TCOD_DOC
	where Ma_CB like '2%') qncn

	--Lấy lương HSQ_BS
	select * into TBL_TCOD_HSQBS from
	(select
		'HSQ_BS' DoiTuong, 'HSQ_BS' LoaiDoiTuong, null Id, null Ma_CB, null Ma_CBo, null Ma_CheDo, null Ten_Cbo, null SoNgayBenhDaiNgayD14Ngay, null SoNgayBenhDaiNgayT14Ngay, null SoNgayOmKhacD14Ngay, null SoNgayOmKhacT14Ngay, null SoNgayConOm, null SoNgayDuongSuc, null fLuongCanCu, null fBENHDAINGAY_D14NGAY, null fBENHDAINGAY_T14NGAY, null fOMKHAC_D14NGAY, null fOMKHAC_T14NGAY, null fCONOM, null fDUONGSUCPHSK
	union
	select
		'', 'HSQ_BS' LoaiDoiTuong, Id, Ma_CB, Ma_CBo, Ma_CheDo, Ten_Cbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK
	from TBL_TCOD_DOC
	where Ma_CB like '0%') hsqbs

	--Lấy lương VCQP
	select * into TBL_TCOD_VCQP from
	(select
		'VCQP' DoiTuong, 'VCQP' LoaiDoiTuong, null Id, null Ma_CB, null Ma_CBo, null Ma_CheDo, null Ten_Cbo, null SoNgayBenhDaiNgayD14Ngay, null SoNgayBenhDaiNgayT14Ngay, null SoNgayOmKhacD14Ngay, null SoNgayOmKhacT14Ngay, null SoNgayConOm, null SoNgayDuongSuc, null fLuongCanCu, null fBENHDAINGAY_D14NGAY, null fBENHDAINGAY_T14NGAY, null fOMKHAC_D14NGAY, null fOMKHAC_T14NGAY, null fCONOM, null fDUONGSUCPHSK
	union
	select
		'', 'VCQP' LoaiDoiTuong, Id, Ma_CB, Ma_CBo, Ma_CheDo, Ten_Cbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK
	from TBL_TCOD_DOC
	where Ma_CB in ('3.1', '3.2', '3.3')) vcqp

	--Lấy lương Lao động hợp đông
	select * into TBL_TCOD_LDHD from
	(select
		'LDHD' DoiTuong, 'LDHD' LoaiDoiTuong, null Id, null Ma_CB, null Ma_CBo, null Ma_CheDo, null Ten_Cbo, null SoNgayBenhDaiNgayD14Ngay, null SoNgayBenhDaiNgayT14Ngay, null SoNgayOmKhacD14Ngay, null SoNgayOmKhacT14Ngay, null SoNgayConOm, null SoNgayDuongSuc, null fLuongCanCu, null fBENHDAINGAY_D14NGAY, null fBENHDAINGAY_T14NGAY, null fOMKHAC_D14NGAY, null fOMKHAC_T14NGAY, null fCONOM, null fDUONGSUCPHSK
	union
	select
		'', 'LDHD' LoaiDoiTuong, Id, Ma_CB, Ma_CBo, Ma_CheDo, Ten_Cbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK
	from TBL_TCOD_DOC
	where Ma_CB = '43') ldhd

	--Ket qua
	select result.* into TBL_RESULT from
	(select
	DoiTuong, LoaiDoiTuong, Id, Ma_CB, Ma_CBo, Ten_Cbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, Ma_CheDo, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK,
	(isnull(fBENHDAINGAY_D14NGAY, 0) + isnull(fBENHDAINGAY_T14NGAY, 0) + isnull(fOMKHAC_D14NGAY, 0) + isnull(fOMKHAC_T14NGAY, 0) + isnull(fCONOM, 0) + isnull(fDUONGSUCPHSK, 0)) fTongSoTien
	from TBL_TCOD_SQ
	union
	select
	DoiTuong, LoaiDoiTuong, Id, Ma_CB, Ma_CBo, Ten_Cbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, Ma_CheDo, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK,
	(isnull(fBENHDAINGAY_D14NGAY, 0) + isnull(fBENHDAINGAY_T14NGAY, 0) + isnull(fOMKHAC_D14NGAY, 0) + isnull(fOMKHAC_T14NGAY, 0) + isnull(fCONOM, 0) + isnull(fDUONGSUCPHSK, 0)) fTongSoTien
	from TBL_TCOD_QNCN
	union
	select
	DoiTuong, LoaiDoiTuong, Id, Ma_CB, Ma_CBo, Ten_Cbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, Ma_CheDo, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK,
	(isnull(fBENHDAINGAY_D14NGAY, 0) + isnull(fBENHDAINGAY_T14NGAY, 0) + isnull(fOMKHAC_D14NGAY, 0) + isnull(fOMKHAC_T14NGAY, 0) + isnull(fCONOM, 0) + isnull(fDUONGSUCPHSK, 0)) fTongSoTien
	from TBL_TCOD_HSQBS
	union
	select
	DoiTuong, LoaiDoiTuong, Id, Ma_CB, Ma_CBo, Ten_Cbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, Ma_CheDo, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK,
	(isnull(fBENHDAINGAY_D14NGAY, 0) + isnull(fBENHDAINGAY_T14NGAY, 0) + isnull(fOMKHAC_D14NGAY, 0) + isnull(fOMKHAC_T14NGAY, 0) + isnull(fCONOM, 0) + isnull(fDUONGSUCPHSK, 0)) fTongSoTien
	from TBL_TCOD_VCQP
	union
	select
	DoiTuong, LoaiDoiTuong, Id, Ma_CB, Ma_CBo, Ten_Cbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, Ma_CheDo, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK,
	(isnull(fBENHDAINGAY_D14NGAY, 0) + isnull(fBENHDAINGAY_T14NGAY, 0) + isnull(fOMKHAC_D14NGAY, 0) + isnull(fOMKHAC_T14NGAY, 0) + isnull(fCONOM, 0) + isnull(fDUONGSUCPHSK, 0)) fTongSoTien
	from TBL_TCOD_LDHD) result

	select
		DoiTuong, 
		LoaiDoiTuong,
		Ma_CB MaCb, 
		Ma_CBo MaCbo, 
		Ten_Cbo TenCbo, 
		SoNgayBenhDaiNgayD14Ngay, 
		SoNgayBenhDaiNgayT14Ngay, 
		SoNgayOmKhacD14Ngay, 
		SoNgayOmKhacT14Ngay, 
		SoNgayConOm, 
		SoNgayDuongSuc, 
		fLuongCanCu FLuongCanCu, 
		Ma_CheDo MaCheDo, 
		fBENHDAINGAY_D14NGAY/@DonViTinh FBenhDaiNgayD14Ngay, 
		fBENHDAINGAY_T14NGAY/@DonViTinh FBenhDauNgayT14Ngay, 
		fOMKHAC_D14NGAY/@DonViTinh FOmKhacD14Ngay, 
		fOMKHAC_T14NGAY/@DonViTinh FOmKhacT14Ngay, 
		fCONOM/@DonViTinh FConOm, 
		fDUONGSUCPHSK/@DonViTinh FDuongSucPHSK,
		fTongSoTien/@DonViTinh FTongSoTien
	from TBL_RESULT
	order by LoaiDoiTuong
END
GO
