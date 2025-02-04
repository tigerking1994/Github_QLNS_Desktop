/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_chi_tiet_so_nhu_cau_phu_luc_3]    Script Date: 10/9/2023 3:46:58 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_chi_tiet_so_nhu_cau_phu_luc_3]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_chi_tiet_so_nhu_cau_phu_luc_3]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_chi_tiet_so_nhu_cau_phu_luc_3]    Script Date: 10/9/2023 3:46:58 PM ******/
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

SELECT iID_MLSKT,
       sKyHieu,
       sum(isnull(fTuChi,0)) fTuChi,
       sum(isnull(fHuyDongTonKho,0)) fHuyDongTonKho,
       sum(isnull(fTonKhoDenNgay,0)) fTonKhoDenNgay,
       sum(isnull(fMuaHangCapHienVat,0)) fMuaHangCapHienVat,
       sum(isnull(fPhanCap,0)) fPhanCap,
       STUFF((
			SELECT ', ' + ctct.sGhiChu
			FROM #SoNhuCauTongHop ctct
			WHERE (ctct.sKyHieu = sncTongHop.sKyHieu AND ctct.sGhiChu <> '') 
			--WHERE (ctct.iID_MLSKT = sncTongHop.iID_MLSKT AND ctct.sGhiChu <> '') 
			FOR XML PATH(''),TYPE).value('(./text())[1]','NVARCHAR(MAX)')
		  ,1,2,'') AS sGhiChu
	   
	   INTO #SoNhuCauTongHopGroup
	   FROM #SoNhuCauTongHop sncTongHop
GROUP BY iID_MLSKT, sKyHieu;

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

LEFT JOIN  #SoNhuCauTongHopGroup snc ON snc.sKyHieu = ml.sKyHieu
LEFT JOIN @tblSkt skt ON (skt.sKyHieu = ml.sKyHieuCu and skt.sKyHieu <> '' and skt.sKyHieu is not null and @NamLamViec = 2024) OR (skt.sKyHieu = ml.sKyHieu and skt.sKyHieu <> '' and skt.sKyHieu is not null and @NamLamViec <> 2024)
LEFT JOIN @tblDuToan dt ON (dt.KyHieu = ml.sKyHieuCu and dt.KyHieu <> '' and dt.KyHieu is not null and @NamLamViec = 2024) OR (dt.KyHieu = ml.sKyHieu and dt.KyHieu <> '' and dt.KyHieu is not null and @NamLamViec <> 2024)

--LEFT JOIN  #SoNhuCauTongHopGroup snc ON snc.iID_MLSKT = ml.iID_MLSKT
--LEFT JOIN @tblSkt skt ON skt.sKyHieu = ml.sKyHieu and skt.iID_MLSKT <> CAST(0x0 as uniqueidentifier)
--LEFT JOIN @tblDuToan dt ON dt.KyHieu = ml.sKyHieu and dt.iID_MLSKT <>  CAST(0x0 as uniqueidentifier)
WHERE ml.iNamLamViec = @NamLamViec
  AND (skt.TuChi <> 0
       OR dt.TuChi <> 0
       OR snc.fTonKhoDenNgay <> 0
       OR snc.fHuyDongTonKho <> 0
       OR snc.fTuChi <> 0
       OR ISNULL(snc.sGhiChu, '') != '');

DROP TABLE #SoNhuCauTongHopGroup;
DROP TABLE #SoNhuCauTongHop;


END
;
;
;
GO
