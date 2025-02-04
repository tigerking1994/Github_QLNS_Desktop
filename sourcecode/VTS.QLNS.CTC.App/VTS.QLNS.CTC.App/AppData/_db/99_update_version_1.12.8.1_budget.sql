/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_so_nhu_cau_phu_luc_3]    Script Date: 24/05/2023 8:00:34 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_tong_hop_so_nhu_cau_phu_luc_3]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_tong_hop_so_nhu_cau_phu_luc_3]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_chi_tiet_so_nhu_cau_phu_luc_3]    Script Date: 24/05/2023 8:00:34 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_chi_tiet_so_nhu_cau_phu_luc_3]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_chi_tiet_so_nhu_cau_phu_luc_3]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_chi_tiet_so_nhu_cau_phu_luc_3]    Script Date: 24/05/2023 8:00:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_skt_chi_tiet_so_nhu_cau_phu_luc_3]
	@LoaiChungTu int,
	@IdDonVi nvarchar(max),
	@NamLamViec int,
	@NamNganSach int,
	@MaNguonNganSach int,
	@dvt int,
	@loaiNNS int
AS
BEGIN
DECLARE @sModule nvarchar(MAX) = 'BUDGET_DEMANDCHECK_DEMAND',
        @idDuToan nvarchar(MAX) = 'BUDGET_ESTIMATE',
        @idSoKiemTra nvarchar(MAX) = 'BUDGET_DEMANDCHECK_CHECK' 
DECLARE @tblDuToan TABLE (iID_MLSKT uniqueidentifier, KyHieu nvarchar(200),TuChi float) 
DECLARE @tblSkt TABLE (iID_MLSKT uniqueidentifier,sKyHieu nvarchar(200) , TuChi float) 
DECLARE @countCanCuDuToan int = 0;

DECLARE @countCanCuSkt int = 0;

DECLARE @countSktChiTiet int = 0;

--count chi tiet

--SELECT @countSktChiTiet = count(*)
--FROM NS_SKT_ChungTuChiTiet
--WHERE iID_MaDonVi = @IdDonVi
--  AND iNamLamViec = @NamLamViec
--  AND iNamNganSach = @NamNganSach
--  AND iID_MaNguonNganSach = @MaNguonNganSach
--  AND iLoaiChungTu = @LoaiChungTu
--  AND iLoai = 0;

---- count can cu du toan

--SELECT @countCanCuDuToan = count(*)
--FROM NS_SKT_ChungTuChiTiet_CanCu
--WHERE iNamLamViec = @NamLamViec
--  AND iID_CanCu IN
--    (SELECT iID_CauHinh_CanCu
--     FROM NS_CauHinh_CanCu
--     WHERE sModule = @sModule
--       AND iID_MaChucNang = @idDuToan
--       AND inamLamViec = @NamLamViec
--       AND iNamCancu = @NamLamViec - 1)
--  AND iiID_CTSoKiemTra in
--    (SELECT iID_CTSoKiemTra
--     FROM NS_SKT_ChungTu
--     WHERE iID_MaDonVi = @IdDonVi
--       AND iNamLamViec = @NamLamViec
--       AND iNamNganSach = @NamNganSach
--       AND iID_MaNguonNganSach = @MaNguonNganSach
--       AND iLoaiChungTu = @LoaiChungTu
--       AND iLoai = 0);

-- count can cu so kiem tra

--SELECT @countCanCuSkt = count(*)
--FROM NS_SKT_ChungTuChiTiet_CanCu
--WHERE iNamLamViec = @NamLamViec
--  AND iID_CanCu IN
--    (SELECT iID_CauHinh_CanCu
--     FROM NS_CauHinh_CanCu
--     WHERE sModule = @sModule
--       AND iID_MaChucNang = @idSoKiemTra
--       AND inamLamViec = @NamLamViec
--       AND iNamCancu = @NamLamViec - 1)
--  AND iiID_CTSoKiemTra in
--    (SELECT iID_CTSoKiemTra
--     FROM NS_SKT_ChungTu
--     WHERE iID_MaDonVi = @IdDonVi
--       AND iNamLamViec = @NamLamViec
--       AND iNamNganSach = @NamNganSach
--       AND iID_MaNguonNganSach = @MaNguonNganSach
--       AND iLoaiChungTu = @LoaiChungTu
--       AND iLoai = 0);

--IF (@countSktChiTiet = 0 AND @countCanCuDuToan = 0 AND @countCanCuSkt = 0)
--INSERT INTO @tblDuToan (iID_MLSKT, KyHieu, TuChi)
--SELECT NULL,
--       mp.sSKT_KyHieu KyHieu,
--       Sum(TuChi) TuChi
--FROM NS_MLSKT_MLNS mp
--JOIN
--  (SELECT SXauNoiMa,
--          sum(fTuChi) TuChi,
--          sum(fHangNhap) HangNhap,
--          sum(fHangMua) HangMua,
--          sum(fPhanCap) PhanCap,
--          sum(fTuChi) MuaHangHienVat,
--          sum(fTuChi) DacThu
--   FROM NS_DT_ChungTuChiTiet
--   WHERE iID_DTChungTu in
--       (SELECT iID_DTChungTu
--        FROM NS_DT_ChungTu
--        WHERE iNamLamViec = @NamLamViec - 1
--          AND iNamNganSach = @NamNganSach
--          AND iID_MaNguonNganSach = @MaNguonNganSach
--          AND bKhoa = 1
--          AND iLoaiChungTu = @LoaiChungTu
--          AND (iloai = 0
--               OR iLoai = 1)
--          AND iLoaiDuToan = 1 )
--     AND iID_MaDonVi = @IdDonVi
--   GROUP BY SXauNoiMa) dtct ON mp.sNS_XauNoiMa = dtct.SXauNoiMa
--WHERE mp.iNamLamViec = @NamLamViec
--GROUP BY mp.sSKT_KyHieu 
--ELSE
INSERT INTO @tblDuToan (iID_MLSKT, KyHieu, TuChi)
SELECT cc.iID_MLSKT,
       cc.sKyHieu,
       sum(isnull(cc.fTuChi, 0))
FROM
  (SELECT *
   FROM NS_SKT_ChungTuChiTiet_CanCu
   WHERE iNamLamViec = @NamLamViec
     AND iID_CanCu IN
       (SELECT iID_CauHinh_CanCu
        FROM NS_CauHinh_CanCu
        WHERE sModule = @sModule
          AND iID_MaChucNang = @idDuToan
          AND inamLamViec = @NamLamViec
          AND iNamCancu = @NamLamViec - 1)
     AND iiID_CTSoKiemTra in
       (SELECT iID_CTSoKiemTra
        FROM NS_SKT_ChungTu
        WHERE iID_MaDonVi = @IdDonVi
          AND iNamLamViec = @NamLamViec
          AND iNamNganSach = @NamNganSach
          AND iID_MaNguonNganSach = @MaNguonNganSach
          AND iLoaiChungTu = @LoaiChungTu
		  AND (@loaiNNS = 0 OR iLoaiNguonNganSach = @loaiNNS)
          AND iLoai = 0) ) cc
GROUP BY cc.iID_MLSKT,cc.sKyHieu;

--IF (@countSktChiTiet = 0 AND @countCanCuDuToan = 0 AND @countCanCuSkt = 0)
--INSERT INTO @tblSkt (iID_MLSKT,sKyHieu, TuChi)
--SELECT ctct.iID_MLSKT,ctct.sKyHieu,
--       ctct.fTuChi
--FROM NS_SKT_ChungTuChiTiet ctct
--JOIN NS_SKT_ChungTu ct ON ct.iID_CTSoKiemTra = ctct.iID_CTSoKiemTra
--WHERE ct.iNamLamViec = @NamLamViec - 1
--  AND ct.iNamNganSach = @NamNganSach
--  AND ct.iID_MaNguonNganSach = @MaNguonNganSach
--  AND ct.iLoaiChungTu = @LoaiChungTu
--  AND ct.iloai = 3 --AND ct.iID_MaDonVi = @IdDonVi

--  AND (ctct.iLoai = 3
--       AND exists
--         (SELECT 1
--          FROM DonVi
--          WHERE iID_MaDonVi = @IdDonVi
--            AND iLoai = 0)
--       OR ctct.iLoai = 4)
--  AND ctct.iID_MaDonVi = @IdDonVi;

--ELSE
INSERT INTO @tblSkt (iID_MLSKT, sKyHieu, TuChi)
SELECT cc.iID_MLSKT,cc.sKyHieu,
       sum(isnull(cc.fTuChi, 0))
FROM
  (SELECT *
   FROM NS_SKT_ChungTuChiTiet_CanCu
   WHERE iNamLamViec = @NamLamViec
     AND iID_CanCu IN
       (SELECT iID_CauHinh_CanCu
        FROM NS_CauHinh_CanCu
        WHERE sModule = @sModule
          AND iID_MaChucNang = @idSoKiemTra
          AND inamLamViec = @NamLamViec
          AND iNamCancu = @NamLamViec - 1)
     AND iiID_CTSoKiemTra in
       (SELECT iID_CTSoKiemTra
        FROM NS_SKT_ChungTu
        WHERE iID_MaDonVi = @IdDonVi
          AND iNamLamViec = @NamLamViec
          AND iNamNganSach = @NamNganSach
          AND iID_MaNguonNganSach = @MaNguonNganSach
          AND iLoaiChungTu = @LoaiChungTu
		  AND (@loaiNNS = 0 OR iLoaiNguonNganSach = @loaiNNS)
          AND iLoai = 0) ) cc
GROUP BY cc.iID_MLSKT,cc.sKyHieu;


SELECT ctct.iID_MLSKT,
       ctct.sKyHieu,
       ctct.fTuChi,
       ctct.fHuyDongTonKho,
       ctct.fTonKhoDenNgay,
       ctct.fMuaHangCapHienVat,
       ctct.fPhanCap,
       ctct.sGhiChu INTO #SoNhuCauTongHop
FROM NS_SKT_ChungTuChiTiet ctct
JOIN NS_SKT_ChungTu ct ON ct.iID_CTSoKiemTra = ctct.iID_CTSoKiemTra
WHERE ct.iNamLamViec = @NamLamViec
  AND ct.iNamNganSach = @NamNganSach
  AND ct.iID_MaNguonNganSach = @MaNguonNganSach
  AND ct.iLoaiChungTu = @LoaiChungTu
  AND ct.iloai = 0 --AND ct.bKhoa = 1
  AND (@loaiNNS = 0 OR ct.iLoaiNguonNganSach = @loaiNNS)
  AND ct.iID_MaDonVi in
    (SELECT *
     FROM f_split(@IdDonVi));


SELECT ml.sSTT STT,
       ml.bHangCha,
       ml.iID_MLSKT,
       ml.iID_MLSKTCha,
       ml.sKyHieu,
       ml.sMoTa,
       isnull(skt.TuChi, 0) / @dvt AS SoKiemTraNamTruoc,
       isnull(dt.TuChi, 0) / @dvt AS DuToanDauNam,
       isnull(snc.fTonKhoDenNgay, 0) / @dvt AS TonKhoDenNgay,
       isnull(snc.fHuyDongTonKho, 0) / @dvt AS HuyDongTonKho,
       isnull(snc.fTuChi, 0) / @dvt AS TuChi,
       snc.sGhiChu
FROM NS_SKT_MucLuc ml
LEFT JOIN #SoNhuCauTongHop snc ON snc.iID_MLSKT = ml.iID_MLSKT
LEFT JOIN @tblSkt skt ON skt.sKyHieu = ml.sKyHieu and skt.iID_MLSKT <> CAST(0x0 as uniqueidentifier)
LEFT JOIN @tblDuToan dt ON dt.KyHieu = ml.sKyHieu and dt.iID_MLSKT <>  CAST(0x0 as uniqueidentifier)
WHERE ml.iNamLamViec = @NamLamViec
  AND (skt.TuChi <> 0
       OR dt.TuChi <> 0
       OR snc.fTonKhoDenNgay <> 0
       OR snc.fHuyDongTonKho <> 0
       OR snc.fTuChi <> 0
       OR ISNULL(snc.sGhiChu, '') != '');


DROP TABLE #SoNhuCauTongHop;

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_so_nhu_cau_phu_luc_3]    Script Date: 24/05/2023 8:00:34 PM ******/
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
	@dvt int,
	@loaiNNS int
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
		AND (@loaiNNS = 0 OR iLoaiNguonNganSach = @loaiNNS)
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
		AND (@loaiNNS = 0 OR iLoaiNguonNganSach = @loaiNNS)
		AND iLoai = 0)
	) cc
GROUP BY cc.iID_MLSKT,cc.sKyHieu;

SELECT ctct.iID_MLSKT,
       ctct.sKyHieu,
       isnull(ctct.fTuChi,0) fTuChi,
       isnull(ctct.fHuyDongTonKho,0) fHuyDongTonKho,
       isnull(ctct.fTonKhoDenNgay,0) fTonKhoDenNgay,
       isnull(ctct.fMuaHangCapHienVat,0) fMuaHangCapHienVat,
       isnull(ctct.fPhanCap,0) fPhanCap,
	   ctct.sGhiChu
       --case when @countDonVi = 1 then ctct.sGhiChu else '' end as sGhiChu
	   INTO #sncChiTiet
FROM NS_SKT_ChungTuChiTiet ctct
JOIN NS_SKT_ChungTu ct ON ct.iID_CTSoKiemTra = ctct.iID_CTSoKiemTra
WHERE ct.iNamLamViec = @NamLamViec
  AND ct.iNamNganSach = @NamNganSach
  AND ct.iID_MaNguonNganSach = @MaNguonNganSach
  AND ct.iLoaiChungTu = @LoaiChungTu
  AND ct.iloai = 0
  AND (@loaiNNS = 0 OR ct.iLoaiNguonNganSach = @loaiNNS)
  --AND ct.bKhoa = 1
  AND ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVi))


select iID_MLSKT,
       sKyHieu,
       sum(isnull(fTuChi,0)) fTuChi,
       sum(isnull(fHuyDongTonKho,0)) fHuyDongTonKho,
       sum(isnull(fTonKhoDenNgay,0)) fTonKhoDenNgay,
       sum(isnull(fMuaHangCapHienVat,0)) fMuaHangCapHienVat,
       sum(isnull(fPhanCap,0)) fPhanCap,
       STUFF((
			SELECT ', ' + ctct.sGhiChu
			FROM #sncChiTiet ctct
			WHERE (ctct.iID_MLSKT = sncChiTiet.iID_MLSKT AND ctct.sGhiChu <> '') 
			FOR XML PATH(''),TYPE).value('(./text())[1]','NVARCHAR(MAX)')
		  ,1,2,'') AS sGhiChu
	   
	   INTO #SoNhuCauTongHop 
	   from #sncChiTiet sncChiTiet
GROUP BY iID_MLSKT, sKyHieu;


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
LEFT JOIN @tblDuToan dt on dt.KyHieu = ml.sKyHieu and skt.iID_MLSKT = dt.iID_MLSKT
WHERE ml.iNamLamViec = @NamLamViec
AND (@MaBQuanLy = '0' OR ml.sKyHieu in (SELECT * FROM #KyHieuSktBQuanLy))
AND (skt.TuChi <> 0 OR dt.TuChi <> 0 OR snc.fTonKhoDenNgay <> 0 OR snc.fHuyDongTonKho <> 0 OR snc.fTuChi <> 0 OR (snc.sGhiChu is not null and snc.sGhiChu != ''));

DROP TABLE #SoNhuCauTongHop;
DROP TABLE #KyHieuSktBQuanLy;
DROP TABLE #sncChiTiet;

END
;
;
;
;
;
;
GO
