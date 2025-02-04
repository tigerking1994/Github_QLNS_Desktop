/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_thang_dulieu_insert]    Script Date: 29/06/2022 6:18:03 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_bangluong_thang_dulieu_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_bangluong_thang_dulieu_insert]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_so_nhu_cau_phu_luc_3]    Script Date: 29/06/2022 6:18:03 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_tong_hop_so_nhu_cau_phu_luc_3]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_tong_hop_so_nhu_cau_phu_luc_3]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_dutoan_index]    Script Date: 29/06/2022 6:18:03 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_dutoan_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_dutoan_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_check_used_mlskt]    Script Date: 29/06/2022 6:18:03 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_check_used_mlskt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_check_used_mlskt]
GO
/****** Object:  UserDefinedFunction [dbo].[f_luong_ntn]    Script Date: 29/06/2022 6:18:03 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_luong_ntn]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_luong_ntn]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_get_skt_chungtuchitiet]    Script Date: 30/06/2022 12:37:22 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_get_skt_chungtuchitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_get_skt_chungtuchitiet]
GO
/****** Object:  UserDefinedFunction [dbo].[f_luong_ntn]    Script Date: 29/06/2022 6:18:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TungNH
-- Create date: 04/05/2022
-- Description:	Tính năm thâm niên
-- =============================================
CREATE FUNCTION [dbo].[f_luong_ntn]
(
	@NgayNN DATETIME,
	@NgayXN DATETIME,
	@NgayTN DATETIME,
	@ThangTNN int,
	@Thang int,
	@Nam int
)
RETURNS int
AS
BEGIN
	DECLARE @NamThamNien int SET @NamThamNien = 0
	DECLARE @monthDiff int SET @monthDiff = 0
	DECLARE @monthDiff2 int SET @monthDiff2 = 0

	IF (@NgayNN IS NOT NULL)
	BEGIN
		IF (@NgayXN IS NULL AND @NgayTN IS NULL)
		BEGIN
			SET @monthDiff = (@Nam - YEAR(@NgayNN)) * 12 + @Thang - MONTH(@NgayNN) + 1 + @ThangTNN
			IF(@monthDiff % 12 >= 1)
				BEGIN
					SET @NamThamNien = @monthDiff / 12
				END
			ELSE
				BEGIN
					SET @NamThamNien = @monthDiff / 12 - 1
				END
		END

		ELSE
		BEGIN
			IF (@NgayTN IS NULL)
			BEGIN
				SET @monthDiff = (YEAR(@NgayXN) - YEAR(@NgayNN)) * 12 + @Thang - MONTH(@NgayNN) + 1 + @ThangTNN
				IF(@monthDiff % 12 >= 1)
					BEGIN
						SET @NamThamNien = @monthDiff / 12
					END
				ELSE
					BEGIN
						SET @NamThamNien = @monthDiff / 12 - 1
					END
			END

			ELSE
			BEGIN
				DECLARE @Lan1 int SET @Lan1 = 12 * (YEAR(@NgayXN) - YEAR(@NgayNN)) + MONTH(@NgayXN) - MONTH(@NgayNN) + 1
				
				IF (@Lan1 < 6)
				BEGIN
					set @monthDiff2 = 12 * (@Nam - YEAR(@NgayTN)) + @Thang - MONTH(@NgayTN) + @ThangTNN + 1
					IF(@monthDiff2 % 12 >= 1)
						BEGIN
							SET @NamThamNien = @monthDiff2 / 12
						END
					ELSE
						BEGIN
							SET @NamThamNien = @monthDiff2 / 12 - 1
						END
				END

				ELSE IF (@Lan1 >= 6 AND @Lan1 <= 12)
				BEGIN
					set @monthDiff2 = 12 * (@Nam - YEAR(@NgayNN)) + @Thang - MONTH(@NgayNN) + @ThangTNN + 1
					IF(@monthDiff2 % 12 >= 1)
						BEGIN
							SET @NamThamNien = @monthDiff2 / 12
						END
					ELSE
						BEGIN
							SET @NamThamNien = @monthDiff2 / 12 - 1
						END
				END

				ELSE 
				BEGIN
					set @monthDiff2 = 12 * (@Nam - YEAR(@NgayTN)) + @Thang - MONTH(@NgayTN) + 1 + @ThangTNN + @Lan1
					IF(@monthDiff2 % 12 >= 1)
						BEGIN
							SET @NamThamNien = @monthDiff2 / 12
						END
					ELSE
						BEGIN
							SET @NamThamNien = @monthDiff2 / 12 - 1
						END
				END
			END
		END
	END
	RETURN @NamThamNien
END
GO
/****** Object:  StoredProcedure [dbo].[sp_check_used_mlskt]    Script Date: 29/06/2022 6:18:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_check_used_mlskt] 
	-- Add the parameters for the stored procedure here
	@YearOfWork int,
	@iid_mlskt uniqueidentifier,
	@iResult bit output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	Declare @count int;
	select @count = count(*) from (
		select iid_mlskt from NS_SKT_ChungTuChiTiet 
		where iNamLamViec = @YearOfWork) tbl
		where iid_mlskt = @iid_mlskt
		--union
		--select iid_mlskt from NS_MLSKT_MLNS t1 LEFT JOIN NS_SKT_MucLuc t2 on t1.sSKT_KyHieu = t2.sKyHieu and t1.iNamLamViec = t2.iNamLamViec 
		--where t1.iNamLamViec = @YearOfWork) tbl where iid_mlskt = @iid_mlskt

	if @count > 0
		set @iResult = 1
	else
		set @iResult = 0

END





GO
/****** Object:  StoredProcedure [dbo].[sp_nh_dutoan_index]    Script Date: 29/06/2022 6:18:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_dutoan_index] 
	@YearOfWork int,
	@ILoai int
AS
BEGIN
	
	WITH SoLieuDieuChinh AS (
		SELECT 
			ct.ID,
			ct.iID_ParentId
		FROM NH_DA_DuToan ct 
		WHERE ct.iID_ParentId IS NOT NULL 
		UNION ALL 
		SELECT 
			ct.ID,
			ct.iID_ParentId
		FROM NH_DA_DuToan ct 
		JOIN SoLieuDieuChinh ctpr ON ct.iID_ParentId = ctpr.ID 
	  WHERE 
		ct.iID_ParentId IS NOT NULL
	), 
	SoLanDieuChinh AS (
		SELECT 
			sdc.Id, 
			sdc.iID_ParentId, 
			COUNT(sdc.Id) AS iSoLanDieuChinh 
		FROM SoLieuDieuChinh sdc 
		GROUP BY 
			sdc.iID_ParentId, 
			sdc.ID
	),
	ThongTinNguonVon AS (
		SELECT 
			nguonVon.iID_DuToanID AS iID_DuToanID, 
			SUM(nguonVon.fGiaTriNgoaiTeKhac) AS fGiaTriNgoaiTeKhac,
			SUM(nguonVon.fGiaTriUSD) AS fGiaTriUSD,
			SUM(nguonVon.fGiaTriVND) AS fGiaTriVND,
			SUM(nguonVon.fGiaTriEUR) AS fGiaTriEUR
		FROM NH_DA_DuToan_NguonVon nguonVon
		GROUP BY 
			nguonVon.iID_DuToanID
	)
	
	SELECT
		duToan.ID AS Id,
		duToan.iID_QDDauTuID AS IIdQdDauTuId,
		duToan.iID_DuAnID AS IIdDuAnId,
		duToan.sSoQuyetDinh AS SSoQuyetDinh,
		duToan.dNgayQuyetDinh AS DNgayQuyetDinh,
		duToan.sMoTa AS SMoTa,
		duToan.sTenChuongTrinh AS STenChuongTrinh,
		duToan.iID_TiGiaUSD_VNDID AS IIdTiGiaUsdVndId,
		duToan.iID_TiGiaUSD_EURID AS IIdTiGiaUsdEurId,
		duToan.iID_TiGiaUSD_NgoaiTeKhacID AS IIdTiGiaUsdNgoaiTeKhacId,
		duToan.fGiaTriNgoaiTeKhac AS FGiaTriNgoaiTeKhac,
		duToan.fGiaTriUSD AS FGiaTriUsd,
		duToan.fGiaTriVND AS FGiaTriVnd,
		duToan.fGiaTriEUR AS FGiaTriEur,
		duToan.dNgayTao AS DNgayTao,
		duToan.sNguoiTao AS SNguoiTao,
		duToan.dNgaySua AS DNgaySua,
		duToan.sNguoiSua AS SNguoiSua,
		duToan.dNgayXoa AS DNgayXoa,
		duToan.sNguoiXoa AS SNguoiXoa,
		duToan.bIsActive AS BIsActive,
		duToan.bIsGoc AS BIsGoc,
		duToan.bIsKhoa AS BIsKhoa,
		duToan.bIsXoa AS BIsXoa,
		duToan.iID_DuToanGocID AS IIdDuToanGocId,
		duToan.iID_TiGiaID AS IIdTiGiaId,
		duToan.sMaNgoaiTeKhac AS SMaNgoaiTeKhac,
		duToan.iID_ParentID AS IIdParentId,
		donvi.sTenDonVi AS STenDonVi,
		CONCAT(duAn.sMaDuAn, ' - ', duAn.sTenDuAn) AS STenDuAn,
		ISNULL(soLieuDieuChinh.iSoLanDieuChinh, 0) AS ILanDieuChinh,
		duToanParent.sSoQuyetDinh AS SDieuChinhTu,
		(SELECT COUNT(Id) FROM Attachment WHERE ModuleType = 406 AND ObjectId = duToan.ID) AS TotalFiles
	FROM NH_DA_DuToan duToan		
	LEFT JOIN NH_DA_DuToan duToanParent
		ON duToan.iID_ParentID = duToanParent.ID
	LEFT JOIN DonVi donVi
		ON duToan.iID_MaDonViQuanLy = donVi.iID_MaDonVi AND donVi.iNamLamViec = @YearOfWork
	LEFT JOIN NH_DA_DuAn duAn
		ON duToan.iID_DuAnID = duAn.ID
	LEFT JOIN SoLanDieuChinh soLieuDieuChinh
		ON soLieuDieuChinh.ID = duToan.ID
	LEFT JOIN ThongTinNguonVon nguonVon
		ON nguonVon.iID_DuToanID = duToan.ID
	WHERE duToan.iLoai = @ILoai 
	ORDER BY dNgayQuyetDinh DESC, sSoQuyetDinh DESC
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_so_nhu_cau_phu_luc_3]    Script Date: 29/06/2022 6:18:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_rpt_skt_tong_hop_so_nhu_cau_phu_luc_3]
	@LoaiChungTu int,
	@IdDonVi varchar(max),
	@NamLamViec int,
	@NamNganSach int,
	@MaNguonNganSach int,
	@MaBQuanLy varchar(max),
	@dvt int
AS
BEGIN
declare @countDonVi int;
SELECT @countDonVi = count(*) FROM f_split(@IdDonVi);
declare @sModule nvarchar(max) = 'BUDGET_DEMANDCHECK_DEMAND',
	@idDuToan nvarchar(max) = 'BUDGET_ESTIMATE',
	@idSoKiemTra nvarchar(max) = 'BUDGET_DEMANDCHECK_CHECK'
declare @tblDuToan table (iID_MLSKT uniqueidentifier, KyHieu nvarchar(200), TuChi float)
declare @tblSkt table (iID_MLSKT uniqueidentifier, KyHieu nvarchar(200), TuChi float)
declare @countCanCuDuToan int;
declare @countCanCuSkt int ;
declare @countSktChiTiet int ; 

--count chi tiet
--SELECT @countSktChiTiet = count(*) 
--	FROM NS_SKT_ChungTuChiTiet
--	WHERE 
--		iID_MaDonVi in (select * from f_split(@IdDonVi)) 
--		AND iNamLamViec = @NamLamViec
--		AND iNamNganSach = @NamNganSach
--		AND iID_MaNguonNganSach = @MaNguonNganSach
--		AND iLoaiChungTu = @LoaiChungTu
--		AND iLoai = 0;

---- count can cu du toan
--SELECT @countCanCuDuToan = count(*) 
--	FROM NS_SKT_ChungTuChiTiet_CanCu 
--	WHERE 
--		iNamLamViec = @NamLamViec
--		AND iID_CanCu IN (SELECT iID_CauHinh_CanCu 
--						FROM NS_CauHinh_CanCu 
--						WHERE sModule = @sModule 
--							AND iID_MaChucNang = @idDuToan 
--							AND inamLamViec = @NamLamViec
--							AND iNamCancu = @NamLamViec - 1)
--		AND iiID_CTSoKiemTra in 
--		(SELECT iID_CTSoKiemTra
--			FROM NS_SKT_ChungTu
--		WHERE 
--		iID_MaDonVi in (select * from f_split(@IdDonVi))
--		AND iNamLamViec = @NamLamViec
--		AND iNamNganSach = @NamNganSach
--		AND iID_MaNguonNganSach = @MaNguonNganSach
--		AND iLoaiChungTu = @LoaiChungTu
--		AND iLoai = 0);

---- count can cu so kiem tra
--SELECT @countCanCuSkt = count(*) 
--	FROM NS_SKT_ChungTuChiTiet_CanCu 
--	WHERE 
--		iNamLamViec = @NamLamViec
--		AND iID_CanCu IN (SELECT iID_CauHinh_CanCu 
--						FROM NS_CauHinh_CanCu 
--						WHERE sModule = @sModule 
--							AND iID_MaChucNang = @idSoKiemTra 
--							AND inamLamViec = @NamLamViec
--							AND iNamCancu = @NamLamViec - 1)
--		AND iiID_CTSoKiemTra in 
--		(SELECT iID_CTSoKiemTra
--			FROM NS_SKT_ChungTu
--		WHERE 
--		iID_MaDonVi in (select * from f_split(@IdDonVi))
--		AND iNamLamViec = @NamLamViec
--		AND iNamNganSach = @NamNganSach
--		AND iID_MaNguonNganSach = @MaNguonNganSach
--		AND iLoaiChungTu = @LoaiChungTu
--		AND iLoai = 0);

--IF (@countSktChiTiet = 0 and @countCanCuDuToan = 0 and @countCanCuSkt = 0)
--	INSERT INTO @tblDuToan (iID_MLSKT, KyHieu, TuChi)
--	SELECT null, mp.sSKT_KyHieu KyHieu,
--       Sum(TuChi) TuChi
--	FROM NS_MLSKT_MLNS mp
--	JOIN
--	  (SELECT SXauNoiMa,
--			  sum(fTuChi) TuChi,
--			  sum(fHangNhap) HangNhap,
--			  sum(fHangMua) HangMua,
--			  sum(fPhanCap) PhanCap,
--			  sum(fTuChi) MuaHangHienVat,
--			  sum(fTuChi) DacThu
--	   FROM NS_DT_ChungTuChiTiet
--	   WHERE iID_DTChungTu in
--		   (SELECT iID_DTChungTu
--			FROM NS_DT_ChungTu
--			WHERE iNamLamViec = @NamLamViec - 1
--			  AND iNamNganSach = @NamNganSach
--			  AND iID_MaNguonNganSach = @MaNguonNganSach
--			  AND iLoaiChungTu = @LoaiChungTu
--			  AND (iloai = 0 OR iLoai = 1)
--			  AND iLoaiDuToan = 1 )
--			  AND iID_MaDonVi in (SELECT * FROM f_split(@IdDonVi))
--	   GROUP BY SXauNoiMa) dtct ON mp.sNS_XauNoiMa = dtct.SXauNoiMa
--	WHERE mp.iNamLamViec = @NamLamViec
--	GROUP BY mp.sSKT_KyHieu
--ELSE
	INSERT INTO @tblDuToan (iID_MLSKT, KyHieu, TuChi)
SELECT cc.iID_MLSKT, sKyHieu, sum(isnull(cc.fTuChi,0))
FROM 
	(SELECT * 
	FROM NS_SKT_ChungTuChiTiet_CanCu 
	WHERE 
		iNamLamViec = @NamLamViec
		AND iID_CanCu IN (SELECT iID_CauHinh_CanCu 
						FROM NS_CauHinh_CanCu 
						WHERE sModule = @sModule 
							AND iID_MaChucNang = @idDuToan 
							AND inamLamViec = @NamLamViec
							AND iNamCancu = @NamLamViec - 1)
			AND iiID_CTSoKiemTra in 
		(SELECT iID_CTSoKiemTra
			FROM NS_SKT_ChungTu
		WHERE 
		iID_MaDonVi in (select * from f_split(@IdDonVi))
		AND iNamLamViec = @NamLamViec
		AND iNamNganSach = @NamNganSach
		AND iID_MaNguonNganSach = @MaNguonNganSach
		AND iLoaiChungTu = @LoaiChungTu
		AND iLoai = 0)

	) cc
GROUP BY cc.iID_MLSKT,sKyHieu;

--IF (@countSktChiTiet = 0 and @countCanCuDuToan = 0 and @countCanCuSkt = 0)
--	INSERT INTO @tblSkt (iID_MLSKT, KyHieu, TuChi)
--	SELECT ctct.iID_MLSKT, sKyHieu,ctct.fTuChi
--	FROM NS_SKT_ChungTuChiTiet ctct
--	JOIN NS_SKT_ChungTu ct ON ct.iID_CTSoKiemTra = ctct.iID_CTSoKiemTra
--	WHERE ct.iNamLamViec = @NamLamViec - 1
--	  AND ct.iNamNganSach = @NamNganSach
--	  AND ct.iID_MaNguonNganSach = @MaNguonNganSach
--	  AND ct.iLoaiChungTu = @LoaiChungTu
--	  AND ct.iloai = 3
--	  AND ctct.iLoai = 3
--ELSE
INSERT INTO @tblSkt (iID_MLSKT,KyHieu, TuChi)
SELECT cc.iID_MLSKT, cc.sKyHieu, sum(isnull(cc.fTuChi,0))
FROM 
	(SELECT * 
	FROM NS_SKT_ChungTuChiTiet_CanCu 
	WHERE 
		iNamLamViec = @NamLamViec
		AND iID_CanCu IN (SELECT iID_CauHinh_CanCu 
						FROM NS_CauHinh_CanCu 
						WHERE sModule = @sModule 
							AND iID_MaChucNang = @idSoKiemTra 
							AND inamLamViec = @NamLamViec
							AND iNamCancu = @NamLamViec - 1)
	   	AND iiID_CTSoKiemTra in 
		(SELECT iID_CTSoKiemTra
			FROM NS_SKT_ChungTu
		WHERE 
		iID_MaDonVi in (select * from f_split(@IdDonVi))
		AND iNamLamViec = @NamLamViec
		AND iNamNganSach = @NamNganSach
		AND iID_MaNguonNganSach = @MaNguonNganSach
		AND iLoaiChungTu = @LoaiChungTu
		AND iLoai = 0)
	) cc
GROUP BY cc.iID_MLSKT,cc.sKyHieu;

select iID_MLSKT,
       sKyHieu,
       sum(isnull(fTuChi,0)) fTuChi,
       sum(isnull(fHuyDongTonKho,0)) fHuyDongTonKho,
       sum(isnull(fTonKhoDenNgay,0)) fTonKhoDenNgay,
       sum(isnull(fMuaHangCapHienVat,0)) fMuaHangCapHienVat,
       sum(isnull(fPhanCap,0)) fPhanCap,
       sGhiChu INTO #SoNhuCauTongHop 
	   from 
(SELECT ctct.iID_MLSKT,
       ctct.sKyHieu,
       isnull(ctct.fTuChi,0) fTuChi,
       isnull(ctct.fHuyDongTonKho,0) fHuyDongTonKho,
       isnull(ctct.fTonKhoDenNgay,0) fTonKhoDenNgay,
       isnull(ctct.fMuaHangCapHienVat,0) fMuaHangCapHienVat,
       isnull(ctct.fPhanCap,0) fPhanCap,
       case when @countDonVi = 1 then ctct.sGhiChu else '' end as sGhiChu
FROM NS_SKT_ChungTuChiTiet ctct
JOIN NS_SKT_ChungTu ct ON ct.iID_CTSoKiemTra = ctct.iID_CTSoKiemTra
WHERE ct.iNamLamViec = @NamLamViec
  AND ct.iNamNganSach = @NamNganSach
  AND ct.iID_MaNguonNganSach = @MaNguonNganSach
  AND ct.iLoaiChungTu = @LoaiChungTu
  AND ct.iloai = 0
  --AND ct.bKhoa = 1
  AND ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVi))
  ) as sncChiTiet
GROUP BY iID_MLSKT, sKyHieu, sGhiChu;

SELECT map.sSKT_KyHieu INTO #KyHieuSktBQuanLy
FROM NS_MLSKT_MLNS MAP
JOIN NS_MucLucNganSach mlns ON mlns.sXauNoiMa = map.sNS_XauNoiMa
AND map.iNamLamViec = mlns.iNamLamViec
WHERE map.iNamLamViec = @NamLamViec
AND sLNS in (select sLNS from NS_MucLucNganSach where iNamLamViec = @NamLamViec and iID_MaBQuanLy = @MaBQuanLy);

SELECT ml.sSTT STT,
       ml.bHangCha,
	   ml.iID_MLSKT,
	   ml.iID_MLSKTCha,
	   ml.sKyHieu,
	   ml.sMoTa,
       isnull(skt.TuChi,0) / @dvt AS SoKiemTraNamTruoc ,
       isnull(dt.TuChi,0) / @dvt AS DuToanDauNam ,
       isnull(snc.fTonKhoDenNgay,0) / @dvt AS TonKhoDenNgay,
       isnull(snc.fHuyDongTonKho,0) / @dvt AS HuyDongTonKho,
       isnull(snc.fTuChi,0) / @dvt AS TuChi,
	   snc.sGhiChu
FROM NS_SKT_MucLuc ml
LEFT JOIN #SoNhuCauTongHop snc on snc.iID_MLSKT = ml.iID_MLSKT
LEFT JOIN @tblSkt skt on skt.KyHieu = ml.sKyHieu
LEFT JOIN @tblDuToan dt on dt.KyHieu = ml.sKyHieu
WHERE ml.iNamLamViec = @NamLamViec
AND (@MaBQuanLy = '0' OR ml.sKyHieu in (SELECT * FROM #KyHieuSktBQuanLy))
AND (skt.TuChi <> 0 OR dt.TuChi <> 0 OR snc.fTonKhoDenNgay <> 0 OR snc.fHuyDongTonKho <> 0 OR snc.fTuChi <> 0 OR (snc.sGhiChu is not null and snc.sGhiChu != ''));

DROP TABLE #SoNhuCauTongHop;
DROP TABLE #KyHieuSktBQuanLy;
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_thang_dulieu_insert]    Script Date: 29/06/2022 6:18:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		TungNH
-- Create date: 21/04/2022
-- Description:	Lấy dữ liệu cho thêm mới bảng lương tháng
-- =============================================
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
				CASE
					WHEN MA_PHUCAP IN ('TA_TRUCQY_DG1', 'TA_TRUCQY_DG2', 'TA_TRUCQY_DG3', 'TA_TRUCQY_DG4', 'TA_DOCHAI_DG', 'TA_OM_DG') THEN ISNULL(HuongPC_SN, 0) * GIA_TRI
					WHEN MA_PHUCAP IN ('TA_BB_DG', 'TA_TRUCTRAI_DG') THEN @SoNgay * GIA_TRI 
					ELSE 0
				END
			) GiaTri
		FROM TL_CanBo_PhuCap canBoPhuCap
		WHERE
			canBoPhuCap.MA_PHUCAP IN ('TA_TRUCQY_DG1', 'TA_TRUCQY_DG2', 'TA_TRUCQY_DG3', 'TA_TRUCQY_DG4', 'TA_DOCHAI_DG', 'TA_OM_DG', 'TA_BB_DG', 'TA_TRUCTRAI_DG')
		GROUP BY canBoPhuCap.MA_CBO
	), ThongTinCanBo AS (
		SELECT
			canBo.Ma_CanBo AS MaCanBo,
			canBo.Ten_CanBo AS TenCanBo,
			donVi.Ma_DonVi MaDonVi,
			canBo.Ma_Hieu_CanBo AS MaHieuCanBo,
			capBac.Ma_Cb MaCapBac,
			canBo.BHTN,
			canBo.PCCV
		FROM TL_DM_CanBo canBo
		INNER JOIN TL_DM_DonVi donVi
			ON canBo.Parent = donVi.Ma_DonVi
		INNER  JOIN TL_DM_CapBac capBac
			ON canBo.Ma_CB = capBac.Ma_Cb
		WHERE
			canBo.Thang = @Thang
			AND canBo.Nam = @Nam
			AND canBo.Parent IN (SELECT * FROM f_split(@MaDonVi))
	)

	SELECT
		newid()					AS Id,
		@Thang					AS Thang,
		@Nam					AS Nam,
		canBo.MaCanBo			AS MaCbo,
		canBo.TenCanBo			AS TenCbo,
		canBo.MaDonVi			AS MaDonVi,
		@MaCachTl				AS MaCachTl,
		canBoPhuCap.MA_PHUCAP	AS MaPhuCap,
		CASE
			WHEN (canBoPhuCap.MA_PHUCAP = 'TCVIECLAM_TT' AND cb.Parent in (1, 2, 3)) THEN 0 
			WHEN (canBoPhuCap.MA_PHUCAP = 'NTN' AND canBoPhuCap.GIA_TRI < 5) OR (canBoPhuCap.MA_PHUCAP = 'BHTNCN_HS' AND canBo.BHTN = 0) OR (canBoPhuCap.MA_PHUCAP = 'PCCOV_HS' AND canBo.PCCV = 0) THEN 0
			WHEN canBoPhuCap.MA_PHUCAP = 'TA_TT' THEN soLieuTienAn.GiaTri
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
GO


delete  [TL_DM_Cach_TinhLuong_Chuan]  where  [Ma_Cot] = 'TCRAQUAN_TT' and [Ma_CachTL]= 'CACH0' 
Go
INSERT [TL_DM_Cach_TinhLuong_Chuan] ([Ma_CachTL], [Ma_Cot], [Ten_Cot], [CongThuc]) 
VALUES ('CACH0', 'TCRAQUAN_TT', N'Trợ cấp xuất ngũ', N'THANG_TCXN*LCS')
Go

Update [TL_DM_PhuCap]
set Ten_PhuCap =N'Trợ cấp việc làm'
where Ma_PhuCap='TCVIECLAM_TT'




/****** Object:  StoredProcedure [dbo].[sp_ns_get_skt_chungtuchitiet]    Script Date: 30/06/2022 12:37:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ns_get_skt_chungtuchitiet]
	@YearOfWork int,
	@iID_MaBQuanLy nvarchar(200)
	
AS
BEGIN
	SET NOCOUNT ON;

	if(@iID_MaBQuanLy = '0') 
		begin
			select distinct mlskt.sKyHieu sSKT_KyHieu
			from NS_MLSKT_MLNS map
			inner join NS_MucLucNganSach ns
			on ns.sXauNoiMa = map.sNS_XauNoiMa
			inner join NS_SKT_MucLuc mlskt  on  map.sSKT_KyHieu = mlskt.sKyHieu 
			where ns.sLNS in (select sLNS from  NS_MucLucNganSach)
			and ns.iNamLamViec = @YearOfWork
			and map.iNamLamViec = @YearOfWork
			and mlskt.iNamLamViec = @YearOfWork		
			order by sSKT_KyHieu
		end


	if(@iID_MaBQuanLy != '0')
		begin
			select distinct mlskt.sKyHieu sSKT_KyHieu
			from NS_MLSKT_MLNS map
			inner join NS_MucLucNganSach ns
			on ns.sXauNoiMa = map.sNS_XauNoiMa
			inner join NS_SKT_MucLuc mlskt  on  map.sSKT_KyHieu = mlskt.sKyHieu 
			where ns.sLNS in (select sLNS from  NS_MucLucNganSach dm where dm.iID_MaBQuanLy = @iID_MaBQuanLy)
			and ns.iNamLamViec = @YearOfWork
			and map.iNamLamViec = @YearOfWork
			and mlskt.iNamLamViec = @YearOfWork
			order by sSKT_KyHieu
		end


	--if(@iID_MaBQuanLy = '0')
	--	begin
	--		select distinct mlskt.sKyHieu sSKT_KyHieu, mlskt.sM, mlskt.sNG_Cha, mlskt.sNG 
	--		into #NS_MLSKT_MLNS_map_tem
	--		from NS_MLSKT_MLNS map
	--		inner join NS_MucLucNganSach ns
	--		on ns.sXauNoiMa = map.sNS_XauNoiMa
	--		inner join NS_SKT_MucLuc mlskt  on  map.sSKT_KyHieu = mlskt.sKyHieu 
	--		where ns.sLNS in (select sLNS from  NS_MucLucNganSach)
	--		and ns.iNamLamViec = @YearOfWork
	--		and map.iNamLamViec = @YearOfWork
	--		and mlskt.iNamLamViec = @YearOfWork

	--		select distinct sSKT_KyHieu from  #NS_MLSKT_MLNS_map_tem 
	--		union all
	--		select distinct  mlskt.sKyHieu from  NS_SKT_MucLuc  mlskt inner join #NS_MLSKT_MLNS_map_tem on mlskt.sNG_Cha = #NS_MLSKT_MLNS_map_tem.sNG_Cha 
	--		where (mlskt.sNG is null or mlskt.sNG  = '') and  mlskt.iNamLamViec = @YearOfWork
	--		union all
	--		select  distinct  mlskt.sKyHieu from  NS_SKT_MucLuc  mlskt inner join #NS_MLSKT_MLNS_map_tem on mlskt.sM = #NS_MLSKT_MLNS_map_tem.sM 
	--		where (mlskt.sNG is null or mlskt.sNG ='')  and (mlskt.sNG_Cha is null or mlskt.sNG_Cha = '') 
	--		and mlskt.iNamLamViec = @YearOfWork
	--		order by sSKT_KyHieu

	--		drop table #NS_MLSKT_MLNS_map_tem
	--	end
		
	--if(@iID_MaBQuanLy != '0')
	--	begin
	--		select distinct mlskt.sKyHieu sSKT_KyHieu, mlskt.sM, mlskt.sNG_Cha, mlskt.sNG 
	--		into #NS_MLSKT_MLNS_map_tem_
	--		from NS_MLSKT_MLNS map
	--		inner join NS_MucLucNganSach ns
	--		on ns.sXauNoiMa = map.sNS_XauNoiMa
	--		inner join NS_SKT_MucLuc mlskt  on  map.sSKT_KyHieu = mlskt.sKyHieu 
	--		where ns.sLNS in (select sLNS from  NS_MucLucNganSach dm where dm.iID_MaBQuanLy = @iID_MaBQuanLy)
	--		and ns.iNamLamViec = @YearOfWork
	--		and map.iNamLamViec = @YearOfWork
	--		and mlskt.iNamLamViec = @YearOfWork

	--		select distinct sSKT_KyHieu from  #NS_MLSKT_MLNS_map_tem_ 
	--		union all
	--		select distinct  mlskt.sKyHieu from  NS_SKT_MucLuc  mlskt inner join #NS_MLSKT_MLNS_map_tem_ on mlskt.sNG_Cha = #NS_MLSKT_MLNS_map_tem_.sNG_Cha 
	--		where (mlskt.sNG is null or  mlskt.sNG  = '') and  mlskt.iNamLamViec = @YearOfWork
	--		union all
	--		select  distinct  mlskt.sKyHieu from  NS_SKT_MucLuc  mlskt inner join #NS_MLSKT_MLNS_map_tem_ on mlskt.sM = #NS_MLSKT_MLNS_map_tem_.sM 
	--		where (mlskt.sNG is null or mlskt.sNG ='')  and (mlskt.sNG_Cha is null or mlskt.sNG_Cha = '') 
	--		and mlskt.iNamLamViec = @YearOfWork
	--		order by sSKT_KyHieu

	--		drop table #NS_MLSKT_MLNS_map_tem_
	--	end
END
;

GO
