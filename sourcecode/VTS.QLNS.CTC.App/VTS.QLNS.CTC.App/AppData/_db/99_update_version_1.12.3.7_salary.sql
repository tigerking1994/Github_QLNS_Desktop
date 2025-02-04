IF NOT EXISTS (SELECT * FROM TL_Bao_Cao WHERE Ma_BaoCao = '1.19')
INSERT INTO TL_Bao_Cao(IsParent, Ma_BaoCao, Ma_Parent, Ten_BaoCao)
VALUES(0, '1.19', '1', N'Bảng lương tháng (Mẫu hiển thị động)')
GO

/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_dong]    Script Date: 02/12/2022 11:32:59 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangthanhtoan_luongthang_dong]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_dong]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_quyettoan_chungtu_chitiet_tao_tonghop]    Script Date: 02/12/2022 11:32:59 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_quyettoan_chungtu_chitiet_tao_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_quyettoan_chungtu_chitiet_tao_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_donvi_bangluong_thang]    Script Date: 07/12/2022 3:42:23 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_donvi_bangluong_thang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_donvi_bangluong_thang]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_donvi_bang_phucap]    Script Date: 07/12/2022 3:42:23 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_donvi_bang_phucap]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_donvi_bang_phucap]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_update_dmphucap_canbo]    Script Date: 08/12/2022 2:38:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_update_dmphucap_canbo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_update_dmphucap_canbo]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_tien_an]    Script Date: 08/12/2022 2:38:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_tien_an]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_tien_an]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_truylinh_dulieu_insert]    Script Date: 08/12/2022 2:38:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_bangluong_truylinh_dulieu_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_bangluong_truylinh_dulieu_insert]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_thang_dulieu_insert]    Script Date: 08/12/2022 2:38:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_bangluong_thang_dulieu_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_bangluong_thang_dulieu_insert]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_thang_dulieu_insert]    Script Date: 08/12/2022 2:38:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_dong]    Script Date: 02/12/2022 11:32:59 AM ******/

CREATE PROCEDURE [dbo].[sp_tl_bangluong_thang_dulieu_insert] 
	@Thang int, 
	@Nam int,
	@MaDonVi NVARCHAR(MAX),
	@MaCachTl NVARCHAR(50),
	@SoNgay int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    WITH SoLieuTienAn AS (
		SELECT
			MA_CBO MaCanBo,
			SUM (
				CASE WHEN PARENT <> N'TIENAN' THEN 0
					WHEN canBoPhuCap.MA_PHUCAP IN ('TA_BB_DG', 'TA_TRUCTRAI_DG') THEN ISNULL(@SoNgay, 0) * canBoPhuCap.GIA_TRI 
					ELSE ISNULL(canBoPhuCap.HuongPC_SN, 0) * canBoPhuCap.GIA_TRI
				END
			) GiaTriTA1,
			SUM (
				CASE WHEN PARENT <> N'TIENAN2' THEN 0
					WHEN canBoPhuCap.MA_PHUCAP IN ('TA_BB_DG', 'TA_TRUCTRAI_DG') THEN ISNULL(@SoNgay, 0) * canBoPhuCap.GIA_TRI 
					ELSE ISNULL(canBoPhuCap.HuongPC_SN, 0) * canBoPhuCap.GIA_TRI
				END
			) GiaTriTA2
		FROM TL_CanBo_PhuCap canBoPhuCap
		INNER JOIN TL_DM_PhuCap as pc on canBoPhuCap.MA_PHUCAP = pc.Ma_PhuCap
		--WHERE
			--canBoPhuCap.MA_PHUCAP IN ('TA_TRUCQY_DG1', 'TA_TRUCQY_DG2', 'TA_TRUCQY_DG3', 'TA_TRUCQY_DG4', 'TA_DOCHAI_DG', 'TA_OM_DG', 'TA_BB_DG', 'TA_TRUCTRAI_DG')
		WHERE 
			pc.PARENT IN ('TIENAN', 'TIENAN2')
		GROUP BY canBoPhuCap.MA_CBO
	),

	ThongTinCanBo AS (
		SELECT
			canBo.Ma_CanBo AS MaCanBo,
			canBo.Ten_CanBo AS TenCanBo,
			donVi.Ma_DonVi MaDonVi,
			canBo.Ma_Hieu_CanBo AS MaHieuCanBo,
			capBac.Ma_Cb MaCapBac,
			canBo.BHTN,
			canBo.PCCV,
			canBo.BNuocNgoai
		FROM TL_DM_CanBo canBo
		INNER JOIN TL_DM_DonVi donVi
			ON canBo.Parent = donVi.Ma_DonVi
		INNER  JOIN TL_DM_CapBac capBac
			ON canBo.Ma_CB = capBac.Ma_Cb
		WHERE
			canBo.Thang = @Thang
			AND canBo.Nam = @Nam
			AND canBo.Parent IN (SELECT * FROM f_split(@MaDonVi))
			AND canBo.IsDelete = 1
			AND canBo.Khong_Luong <> 1
	)

	SELECT
		newid()					AS Id,
		@Thang					AS Thang,
		@Nam					AS Nam,
		canBo.MaCanBo			AS MaCbo,
		canBo.TenCanBo			AS TenCbo,
		canBo.MaDonVi			AS MaDonVi,
		canBo.BNuocNgoai				,		
		@MaCachTl				AS MaCachTl,
		canBoPhuCap.MA_PHUCAP	AS MaPhuCap,
		CASE
			WHEN (canBoPhuCap.MA_PHUCAP = 'TCVIECLAM_TT' AND cb.Parent in ('1', '2', '3')) THEN 0 
			WHEN (canBoPhuCap.MA_PHUCAP = 'NTN' AND canBoPhuCap.GIA_TRI < 5) OR (canBoPhuCap.MA_PHUCAP = 'BHTNCN_HS' AND canBo.BHTN = 0) OR (canBoPhuCap.MA_PHUCAP = 'PCCOV_HS' AND canBo.PCCV = 0) THEN 0
			WHEN canBoPhuCap.MA_PHUCAP = 'TA_TT' THEN soLieuTienAn.GiaTriTA1
			WHEN canBoPhuCap.MA_PHUCAP = 'TA_TT2' THEN soLieuTienAn.GiaTriTA2
			WHEN canBoPhuCap.MA_PHUCAP = 'TRICHLUONG_TT' THEN (canBoPhuCap.GIA_TRI / 1000) * 1000
			ELSE canBoPhuCap.GIA_TRI
		END						AS GiaTri,
		canBo.MaHieuCanBo		AS MaHieuCanBo,
		canBo.MaCapBac			AS MaCb,
		canBoPhuCap.HuongPC_SN	AS HuongPcSn,
		cachTinhLuong.CongThuc	AS CongThuc,
		CASE WHEN cachTinhLuong.Id IS NULL THEN 1 ELSE 0 END AS IsCalculated
	FROM TL_CanBo_PhuCap canBoPhuCap
	INNER JOIN ThongTinCanBo canBo
		ON canBo.MaCanBo = canBoPhuCap.MA_CBO
	LEFT JOIN SoLieuTienAn soLieuTienAn
		ON canBoPhuCap.MA_CBO = soLieuTienAn.MaCanBo
	LEFT JOIN TL_DM_Cach_TinhLuong_Chuan cachTinhLuong
		ON cachTinhLuong.Ma_Cot = canBoPhuCap.MA_PHUCAP
	left join TL_DM_CapBac cb on canBo.MaCapBac = cb.Ma_Cb
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_truylinh_dulieu_insert]    Script Date: 08/12/2022 2:38:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_tl_bangluong_truylinh_dulieu_insert] 
	@Thang int, 
	@Nam int,
	@MaDonVi NVARCHAR(MAX),
	@MaCachTl NVARCHAR(50),
	@SoNgay int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   WITH ThongTinCanBo AS (
		SELECT
			canBo.Ma_CanBo AS MaCanBo,
			canBo.Ten_CanBo AS TenCanBo,
			donVi.Ma_DonVi MaDonVi,
			canBo.Ma_Hieu_CanBo AS MaHieuCanBo,
			capBac.Ma_Cb MaCapBac,
			canBo.BHTN,
			canBo.PCCV,
			canBo.BNuocNgoai
		FROM TL_DM_CanBo canBo
		INNER JOIN TL_DM_DonVi donVi
			ON canBo.Parent = donVi.Ma_DonVi
		INNER  JOIN TL_DM_CapBac capBac
			ON canBo.Ma_CB = capBac.Ma_Cb
		WHERE
			canBo.Thang = @Thang
			AND canBo.Nam = @Nam
			AND canBo.Ma_CanBo IN (SELECT MA_CBO FROM TL_CanBo_PhuCap WHERE MA_PHUCAP LIKE '%TTL%' AND GIA_TRI > 0 GROUP BY MA_CBO)
			AND donVi.Ma_DonVi IN (SELECT * FROM f_split(@MaDonVi))
	)

	SELECT
		@Thang					AS Thang,
		@Nam					AS Nam,
		canBo.MaCanBo			AS MaCbo,
		canBo.TenCanBo			AS TenCbo,
		canBo.MaDonVi			AS MaDonVi,
		@MaCachTl				AS MaCachTl,
		canBoPhuCap.MA_PHUCAP	AS MaPhuCap,
		canBo.BNuocNgoai			,
		CASE
			WHEN (canBoPhuCap.MA_PHUCAP = 'NTN' AND canBoPhuCap.GIA_TRI < 5) OR (canBoPhuCap.MA_PHUCAP = 'BHTNCN_HS' AND canBo.BHTN = 0) OR (canBoPhuCap.MA_PHUCAP = 'PCCOV_HS' AND canBo.PCCV = 0) THEN 0
			ELSE canBoPhuCap.GIA_TRI
		END						AS GiaTri,
		canBo.MaHieuCanBo		AS MaHieuCanBo,
		canBo.MaCapBac			AS MaCb,
		canBoPhuCap.HuongPC_SN	AS HuongPcSn,
		cachTinhLuong.CongThuc	AS CongThuc,
		CASE WHEN cachTinhLuong.Id IS NULL THEN 1 ELSE 0 END AS IsCalculated
	FROM TL_CanBo_PhuCap canBoPhuCap
	INNER JOIN ThongTinCanBo canBo
		ON canBo.MaCanBo = canBoPhuCap.MA_CBO
	LEFT JOIN TL_DM_Cach_TinhLuong_TruyLinh cachTinhLuong
		ON cachTinhLuong.Ma_Cot = canBoPhuCap.MA_PHUCAP
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_tien_an]    Script Date: 08/12/2022 2:38:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_tien_an] @thang int, @nam int, @maDonVi nvarchar(MAX), @daysInMonth int AS
BEGIN
SELECT 
       canBo.Parent MaDonVi,
	   donvi.Ten_DonVi TenDonVi,
       PhuCapTienAn.MA_PHUCAP MaPhuCap,
       phucap.Ten_PhuCap TienAn,
       PhuCapTienAn.GIA_TRI DinhMuc,
	   'x' as Nhan,
	   CAST(COUNT(canBo.Ma_CanBo) as decimal) SoNguoi,
	   'x' as Nhan, 
		CASE 
			When PhuCapTienAn.MA_PHUCAP IN ('TA_BB_DG', 'TA_TRUCTRAI_DG') THEN CAST(@daysInMonth as decimal)
			--WHEN PhuCapTienAn.MA_PHUCAP IN ('TA_DOCHAI_DG', 'TA_OM_DG', 'TA_TRUCQY_DG1', 'TA_TRUCQY_DG2', 'TA_TRUCQY_DG3', 'TA_TRUCQY_DG4') THEN CAST(SUM(PhuCapTienAn.HuongPC_SN) as decimal)
			WHEN PhuCapTienAn.HuongPC_SN IS NULL THEN 0
			WHEN phucap.Parent = 'TIENAN2'  THEN CAST(SUM(PhuCapTienAn.HuongPC_SN) as decimal)
		End SoNgay,
	   'ngày' as Dv_tinh,
	   '=' Bang,
	   (PhuCapTienAn.GIA_TRI * COUNT(canBo.Ma_CanBo) * CASE 
			When PhuCapTienAn.MA_PHUCAP IN ('TA_BB_DG', 'TA_TRUCTRAI_DG') THEN CAST(@daysInMonth as decimal)
			--WHEN PhuCapTienAn.MA_PHUCAP IN ('TA_DOCHAI_DG', 'TA_OM_DG', 'TA_TRUCQY_DG1', 'TA_TRUCQY_DG2', 'TA_TRUCQY_DG3', 'TA_TRUCQY_DG4') THEN CAST(SUM(PhuCapTienAn.HuongPC_SN) as decimal)
			WHEN PhuCapTienAn.HuongPC_SN IS NULL THEN 0
			WHEN phucap.Parent = 'TIENAN2'  THEN CAST(SUM(PhuCapTienAn.HuongPC_SN) as decimal)
		End) ThanhTien
FROM Tl_Dm_CanBo canBo
JOIN
  (SELECT MA_CBO,
          cbopc.MA_PHUCAP,
          cbopc.GIA_TRI,
          cbopc.HuongPC_SN
   FROM TL_CanBo_PhuCap cbopc
   LEFT JOIN Tl_DM_PhuCap mapc ON cbopc.MA_PHUCAP = mapc.Ma_PhuCap
   WHERE mapc.Parent IN ('TIENAN', 'TIENAN2')
     AND cbopc.GIA_TRI > 0) PhuCapTienAn ON canBo.Ma_CanBo = PhuCapTienAn.MA_CBO
JOIN Tl_dm_PhuCap phucap ON PhuCapTienAn.MA_PHUCAP = phucap.Ma_PhuCap
JOIN TL_DM_DonVi donvi ON canBo.Parent = donvi.Ma_DonVi
WHERE canBo.Thang = @thang
  AND canBo.Nam = @nam
  And canbo.Parent IN (SELECT * FROM f_split(@maDonVi))
  Group By canBo.Parent,
	   donvi.Ten_DonVi,
       PhuCapTienAn.MA_PHUCAP,
	   PhuCapTienAn.HuongPC_SN,
       phucap.Ten_PhuCap,
	   phucap.Parent,
       PhuCapTienAn.GIA_TRI
End
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_update_dmphucap_canbo]    Script Date: 08/12/2022 2:38:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_tl_update_dmphucap_canbo]
@nam int,
@thang int,
@bIsDelete bit,
@phucapIds t_tbl_uniqueidentifier READONLY
AS
BEGIN
	SELECT Ma_PhuCap, Gia_Tri, Ten_NganHang INTO #tmpPC
	FROM @phucapIds as tmp
	INNER JOIN TL_DM_PhuCap as pc on tmp.Id = pc.Id
	
	SELECT cb.Ma_CanBo, pc.Gia_Tri, pc.Ma_PhuCap INTO #tmpCanBoPC
	FROM TL_DM_CanBo as cb, #tmpPC as pc
	WHERE cb.Nam = @nam AND cb.Thang = @thang

	IF(@bIsDelete = 0)
	BEGIN
		CREATE TABLE #tmpUpdate(MaCB nvarchar(100), MaPC nvarchar(500))

		UPDATE tbl
		SET GIA_TRI = pc.Gia_Tri
		OUTPUT inserted.MA_CBO, inserted.MA_PHUCAP INTO #tmpUpdate(MaCB, MaPC)
		FROM TL_CanBo_PhuCap as tbl
		INNER JOIN #tmpCanBoPC as pc ON tbl.MA_PHUCAP = pc.Ma_PhuCap AND tbl.MA_CBO = pc.Ma_CanBo

		INSERT INTO TL_CanBo_PhuCap(bSaoChep, CHON, CONG_THUC, GIA_TRI, HE_SO, HuongPC_SN, MA_CBO, MA_KMCP, MA_PHUCAP, PHANTRAM_CT)
		SELECT pc.bSaoChep, pc.CHON, pc.CONG_THUC, tbl.Gia_Tri, HE_SO, HuongPC_SN, tbl.Ma_CanBo, MA_KMCP, tbl.Ma_PhuCap, PHANTRAM_CT
		FROM #tmpCanBoPC as tbl
		INNER JOIN TL_DM_PhuCap as pc on tbl.Ma_PhuCap = pc.Ma_PhuCap
		LEFT JOIN #tmpUpdate as dt on tbl.Ma_CanBo = dt.MaCB AND tbl.Ma_PhuCap = dt.MaPC
		WHERE dt.MaCB IS NULL OR dt.MaPC IS NULL

		IF (EXISTS (SELECT * FROM #tmpPC WHERE Ma_PhuCap = 'TENNGANHANG'))
		BEGIN
			UPDATE TL_DM_CanBo SET Ten_KhoBac = (SELECT Ten_NganHang FROM TL_DM_PhuCap WHERE Ma_PhuCap = 'TENNGANHANG')
			WHERE Thang = @thang AND Nam = @nam
		END
		DROP TABLE #tmpUpdate
	END
	ELSE
	BEGIN
		DELETE tbl
		FROM TL_CanBo_PhuCap as tbl
		INNER JOIN #tmpCanBoPC as pc ON tbl.MA_PHUCAP = pc.Ma_PhuCap AND tbl.MA_CBO = pc.Ma_CanBo
	END

	DROP TABLE #tmpPC
	DROP TABLE #tmpCanBoPC
END
GO

/****** Object:  StoredProcedure [dbo].[sp_tl_get_donvi_bang_phucap]    Script Date: 07/12/2022 3:42:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[sp_tl_get_donvi_bang_phucap] @thang int, @nam int, @cachTinhLuong varchar(20)
AS
BEGIN

select Parent into #tmpCB from TL_DM_CanBo where left (Ma_CB, 1) = 0 and Thang = @thang and Nam = @nam group by Parent

select donvi.* from #tmpCB canbo
left join TL_DS_CapNhap_BangLuong bangluong  on canbo.Parent = bangluong.Ma_CBo 
left join TL_DM_DonVi donvi on canbo.Parent = donvi.Ma_DonVi
where bangluong.Thang = @thang and bangluong.Nam = @nam and bangluong.Ma_CachTL = @cachTinhLuong

drop table #tmpCB

END
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_donvi_bangluong_thang]    Script Date: 07/12/2022 3:42:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[sp_tl_get_donvi_bangluong_thang] @thang int, @nam int, @cachTinhLuong varchar(20)
AS
BEGIN

select Parent into #tmpCB from TL_DM_CanBo where left (Ma_CB, 1) <> 0 and Thang = @thang and Nam = @nam group by Parent

select donvi.* from #tmpCB canbo
left join TL_DS_CapNhap_BangLuong bangluong  on canbo.Parent = bangluong.Ma_CBo 
left join TL_DM_DonVi donvi on canbo.Parent = donvi.Ma_DonVi
where bangluong.Thang = @thang and bangluong.Nam = @nam and bangluong.Ma_CachTL = @cachTinhLuong

drop table #tmpCB

END
GO

/****** Object:  StoredProcedure [dbo].[sp_tl_quyettoan_chungtu_chitiet_tao_tonghop]    Script Date: 05/12/2022 3:10:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_tl_quyettoan_chungtu_chitiet_tao_tonghop]
	@ListIdChungTuTongHop ntext,
	@IdChungTu nvarchar(100),
	@IdDonVi nvarchar(10),
	@TenDonVi nvarchar(250),
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int
AS
BEGIN

DECLARE @iIdChungTuOld uniqueidentifier,
	@iIdChungTuDuToanOld uniqueidentifier

SELECT TOP(1) @iIdChungTuOld = ol.ID, @iIdChungTuDuToanOld = ol.IIdChungTuDuToan
FROM TL_QT_ChungTu as ol
INNER JOIN (SELECT * FROM TL_QT_ChungTu WHERE ID = @IdChungTu) as nw 
on ol.Thang = (CASE WHEN nw.Thang = 1 THEN 12 ELSE nw.Thang - 1 END)
	AND ol.Nam = (CASE WHEN nw.Thang = 1 THEN nw.Nam - 1 ELSE nw.Nam END)
	AND ol.Ma_DonVi = nw.Ma_DonVi
	AND ol.sTongHop IS NOT NULL
ORDER BY ol.Ngay_tao DESC

CREATE TABLE #tmp(sXauNoiMa nvarchar(100), DDuToan decimal)

IF(@iIdChungTuOld IS NOT NULL AND @iIdChungTuDuToanOld IS NOT NULL)
BEGIN
	UPDATE TL_QT_ChungTu SET IIdChungTuDuToan = @iIdChungTuDuToanOld WHERE ID = @IdChungTu

	INSERT INTO #tmp(sXauNoiMa, DDuToan)
	SELECT XauNoiMa, SUM(ISNULL(DDuToan, 0))
	FROM TL_QT_ChungTuChiTiet 
	WHERE Id_ChungTu = @iIdChungTuOld
	GROUP BY XauNoiMa
END

INSERT INTO [dbo].[TL_QT_ChungTuChiTiet] ([Id_ChungTu] , [MLNS_Id] , [MLNS_Id_Parent] , [XauNoiMa] , [LNS] , [L] , [K] , [M] , [TM] , [TTM] , [NG] , [TNG] , [TNG1] , [TNG2] , [TNG3] , [MoTa] , [Chuong] , [NamNganSach] , [NguonNganSach] , [NamLamViec] , [iTrangThai] , [iThangQuy] , [Id_DonVi] , [TenDonVi] , [MucAn] , [GhiChu] , [DateCreated] , [UserCreator] , [DateModified] , [UserModifier] , [TongCong] , [BHangCha] , [ChiTietToi] , [SoNgay] , [SoNguoi] , [DieuChinh] , [MaCachTl], [DDuToan])
SELECT @idChungTu,
       MLNS_Id,
       MLNS_Id_Parent,
       XauNoiMa,
       LNS,
       L,
       K,
       M,
       TM,
       TTM,
       NG,
       TNG,
       TNG1,
       TNG2,
       TNG3,
       MoTa,
       NULL,
       @NamNganSach,
       @NguonNganSach,
       @NamLamViec,
       1,
       NULL,
       @IdDonVi,
       @TenDonVi,
       NULL,
       NULL,
       GETDATE(),
       NULL,
       NULL,
       NULL,
       sum(Isnull(TongCong, 0)),
       BHangCha,
       NULL,
       sum(Isnull(SoNgay, 0)),
       sum(Isnull(SoNguoi, 0)),
       sum(Isnull(DieuChinh, 0)),
	   MaCachTl,
	   SUM(ISNULL(tmp.DDuToan, 0))
FROM TL_QT_ChungTuChiTiet ct
LEFT JOIN #tmp as tmp on ct.XauNoiMa = tmp.sXauNoiMa
WHERE ct.Id_ChungTu in
    (SELECT *
     FROM f_split(@ListIdChungTuTongHop))
GROUP BY MLNS_Id,
         MLNS_Id_Parent,
         XauNoiMa,
         LNS,
         L,
         K,
         M,
         TM,
         TTM,
         NG,
         TNG,
         TNG1,
         TNG2,
         TNG3,
         MoTa,
         BHangCha,
		 MaCachTl,
		 tmp.sXauNoiMa

	-- add du toan thang truoc da co
	INSERT INTO [dbo].[TL_QT_ChungTuChiTiet] ([Id_ChungTu] , [MLNS_Id] , [MLNS_Id_Parent] , [XauNoiMa] , [LNS] , [L] , [K] , [M] , [TM] , [TTM] , [NG] , [TNG] , [TNG1] , [TNG2] , [TNG3] , [MoTa] , [Chuong] , [NamNganSach] , [NguonNganSach] , [NamLamViec] , [iTrangThai] , [iThangQuy] , [Id_DonVi] , [TenDonVi] , [MucAn] , [GhiChu] , [DateCreated] , [UserCreator] , [DateModified] , [UserModifier] , [TongCong] , [BHangCha] , [ChiTietToi] , [SoNgay] , [SoNguoi] , [DieuChinh] , [MaCachTl], [DDuToan])
	SELECT @idChungTu, 
		ml.iID_MLNS, 
		ml.iID_MLNS_Cha, 
		tmp.sXauNoiMa, 
		ml.sLNS,
		ml.sL, 
		ml.sK, 
		ml.sM, 
		ml.sTM, 
		ml.sTTM, 
		ml.sNG,
		ml.sTNG, 
		ml.sTNG1, 
		ml.sTNG2, 
		ml.sTNG3,
		ml.sMoTa,
		NULL, 
		@NamNganSach,
       @NguonNganSach,
       @NamLamViec,
       1,
       NULL,
       @IdDonVi,
       @TenDonVi,
       NULL,
       NULL,
       GETDATE(),
       NULL,
       NULL,
       NULL,
       Isnull(TongCong, 0),
       ml.bHangCha,
       NULL,
       Isnull(SoNgay, 0),
       Isnull(SoNguoi, 0),
       Isnull(DieuChinh, 0),
	   '',
	   ISNULL(tmp.DDuToan, 0)
	FROM #tmp as tmp
	INNER JOIN NS_MucLucNganSach as ml on ml.iNamLamViec = @NamLamViec AND ml.sXauNoiMa = tmp.sXauNoiMa
	LEFT JOIN TL_QT_ChungTuChiTiet as dt on tmp.sXauNoiMa = dt.XauNoiMa AND dt.Id_ChungTu = @idChungTu
	WHERE dt.XauNoiMa IS NULL AND ISNULL(tmp.DDuToan, 0) <> 0

	DROP TABLE #tmp
END
;
;
GO

/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_dong]    Script Date: 05/12/2022 3:10:07 PM ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_dong]
@MaDonVi NVARCHAR(max),
@Thang int,
@Nam AS int,
@IsOrderChucVu AS bit = 0,
@lstColumnInclude nvarchar(max),
@lstHeaderInclude nvarchar(max)
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;

DECLARE @Cols AS NVARCHAR(MAX)
DECLARE @Header AS NVARCHAR(MAX)
DECLARE @Query AS NVARCHAR(MAX)

SET @Cols = 'NTN,LHT_HS,PCTNVK_HS,HSBL_HS,LHT_TT,PCTNVK_TT,HSBL_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCTRA_SUM,PCCOV_TT,PCDACTHU_SUM,PCKHAC_SUM,LUONGTHANG_SUM,BHCN_TT,THUETNCN_TT,TA_TT,GTKHAC_TT,TRICHLUONG_TT,PHAITRU_SUM,TM,THANHTIEN,BHXHCN_TT,BHYTCN_TT,BHTNCN_TT,PCTAUBP_TT,PCBVBG_TT,PCTEMTHU_TT,TA_TONG,PHAITRUKHAC_SUM'
SET @Header = 'NTN,LHT_HS,PCTNVK_HS,HSBL_HS,LHT_TT,PCTNVK_TT,HSBL_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCTRA_SUM,PCCOV_TT,PCDACTHU_SUM,PCKHAC_SUM,LUONGTHANG_SUM,BHCN_TT,THUETNCN_TT,TA_TT,GTKHAC_TT,TRICHLUONG_TT,PHAITRU_SUM,TM,THANHTIEN,BHXHCN_TT,BHYTCN_TT,BHTNCN_TT,PCTAUBP_TT,PCBVBG_TT,PCTEMTHU_TT,TA_TONG,PHAITRUKHAC_SUM'

IF(ISNULL(@lstColumnInclude, '') <> '')
BEGIN
	SET @Cols = CONCAT(@Cols, ',', @lstColumnInclude)
	SET @Header = CONCAT(@Header, ',', @lstHeaderInclude)
END

SET @Query =
'
WITH BangLuongThang AS (
SELECT Thang, Nam, MaCanBo, ' + @Header + ' FROM (
SELECT
dsCapNhapBangLuong.Thang AS Thang,
dsCapNhapBangLuong.Nam AS Nam,
bangLuong.Ma_CBo AS MaCanBo,
bangLuong.MA_PHUCAP AS MaPhuCap,
bangLuong.GIA_TRI AS GiaTri
FROM TL_BangLuong_Thang bangLuong
JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong
ON bangLuong.parent = dsCapNhapBangLuong.Id
WHERE
bangLuong.Ma_PhuCap IN (SELECT * FROM f_split(''' + @Cols + '''))
AND dsCapNhapBangLuong.Ma_CachTL=''CACH0''
AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
AND dsCapNhapBangLuong.Status=1
) x
PIVOT
(
SUM(GiaTri)
FOR MaPhuCap IN (' + @Cols + ')
) pvt
), ThongTinCanBo AS (
SELECT
donVi.Ma_DonVi AS MaDonVi,
donVi.Ten_Donvi AS TenDonvi,
canBo.Ma_CanBo AS MaCanBo,
canBo.Ten_CanBo AS TenCanBo,
dbo.f_split_empty(canbo.Ten_CanBo) AS Ten,
canBo.Thang_TNN AS Tnn,
ISNULL(canBo.Ma_CB, ''0'') AS MaCapBac,
ISNULL(capBac.Ten_Cb, ''0'') AS CapBac,
ISNULL(chucVu.HeSo_Cv, 0) AS HSChucVu,
chucVu.Ma_Cv AS MaChucVu,
chucVu.Ten_Cv AS TenChucVu,
canBo.So_TaiKhoan AS Stk,
ISNULL(FORMAT(canBo.Ngay_NN,''MM/yy''), '''') AS NgayNhapNgu,
ISNULL(FORMAT(canBo.Ngay_XN,''MM/yy''), '''') AS NgayXuatNgu,
ISNULL(FORMAT(canBo.Ngay_TN,''MM/yy''), '''') AS NgayTaiNgu,
capBac.XauNoiMa
FROM TL_DM_CanBo canBo
INNER JOIN TL_DM_DonVi donVi
ON canBo.Parent=donVi.Ma_DonVi
LEFT JOIN TL_DM_CapBac capBac
ON canBo.Ma_CB=capBac.Ma_Cb
LEFT JOIN TL_DM_ChucVu chucVu
ON canBo.Ma_CV=chucVu.Ma_Cv
WHERE
canBo.IsDelete = 1
AND canBo.Khong_Luong = 0
)

SELECT
bangLuong.*,
canBo.MaDonVi,
canBo.TenDonVi,
canBo.TenCanBo,
canBo.Ten,
canBo.MaCapBac,
canBo.CapBac,
canBo.HSChucVu,
canBo.MaChucVu,
canBo.TenChucVu,
canBo.Stk,
canBo.NgayNhapNgu,
canBo.NgayXuatNgu,
canBo.NgayTaiNgu,
canBo.Tnn,
canBo.XauNoiMa
FROM BangLuongThang bangLuong
INNER JOIN ThongTinCanBo canBo
ON bangLuong.MaCanBo = canBo.MaCanBo'
IF @IsOrderChucVu = 1
SET @Query = @Query +' ORDER BY HSChucVu DESC, MaCapBac DESC, Ten ASC';
ELSE
SET @Query = @Query +' ORDER BY MaCapBac DESC, Ten ASC';
execute(@Query)
END
GO


update TL_PhuCap_MLNS set Ma_Cb = 4 where Ma_PhuCap = 'TA_BB_DG' and Ma_Cb = 4.0
GO
delete from TL_PhuCap_MLNS where Ma_PhuCap = 'PCTRA_SUM' and Nam=2022
GO
update TL_DM_PhuCap set Gia_Tri=0.2250 where Ma_PhuCap = 'BHXHDVCS_HS'
GO
update TL_DM_PhuCap set Gia_Tri=0.0450 where Ma_PhuCap = 'BHYTDVCS_HS'
GO