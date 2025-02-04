UPDATE NS_QT_ChungTuChiTiet_GiaiThich
SET sMoTa_TinhHinh = CONCAT(N'- Tình hình đảm bảo: ', sMoTa_TinhHinh, CHAR(13)+CHAR(10), N'- Kiến nghị: ', sMoTa_KienNghi)
WHERE ISNULL(sMoTa_TinhHinh, '') <> '' OR ISNULL(sMoTa_KienNghi, '') <> ''
GO

UPDATE NS_QT_ChungTuChiTiet_GiaiThich
SET
fLuongBHXH_CNVQP_Tru = fLuongBHXH_CNVQP_Tru + fLuongBHXH_HD_Tru,
fLuong_CNVQP = fLuong_CNVQP + fLuong_HD,
fLuong_CNVQP_QT = fLuong_CNVQP_QT + fLuong_HD_QT,
fLuong_CNVQP_Tru = fLuong_CNVQP_Tru + fLuong_HD_Tru,
fPhuCapBHXH_CNVQP_Tru = fPhuCapBHXH_CNVQP_Tru + fPhuCapBHXH_HD_Tru,
fPhuCap_CNVQP = fPhuCap_CNVQP + fPhuCap_HD,
fPhuCap_CNVQP_QT = fPhuCap_CNVQP_QT + fPhuCap_HD_QT,
fPhuCap_CNVQP_Tru = fPhuCap_CNVQP_Tru + fPhuCap_HD_Tru
GO

/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_so_nhu_cau_phu_luc_5]    Script Date: 7/24/2024 7:44:52 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_tong_hop_so_nhu_cau_phu_luc_5]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_tong_hop_so_nhu_cau_phu_luc_5]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_chi_tiet_so_nhu_cau_phu_luc_5]    Script Date: 7/24/2024 7:44:52 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_chi_tiet_so_nhu_cau_phu_luc_5]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_chi_tiet_so_nhu_cau_phu_luc_5]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_chi_tiet_so_nhu_cau_phu_luc_5]    Script Date: 7/24/2024 7:44:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_skt_chi_tiet_so_nhu_cau_phu_luc_5]
	@LoaiChungTu int,
	@IdDonVi varchar(max),
	@NamLamViec int,
	@NamNganSach int,
	@MaNguonNganSach int,
	@dvt int,
	@loaiNNS int
AS
BEGIN
declare @sModule nvarchar(max) = 'BUDGET_DEMANDCHECK_DEMAND',
	@idDuToan nvarchar(max) = 'BUDGET_ESTIMATE',
	@idSoKiemTra nvarchar(max) = 'BUDGET_DEMANDCHECK_CHECK'
declare @tblDuToan table (iID_MLSKT uniqueidentifier, KyHieu nvarchar(200),  MuaHangHienVat float, DacThu float)
declare @tblSkt table (iID_MLSKT uniqueidentifier,KyHieu nvarchar(200), TuChi float, MuaHangHienVat float, DacThu float)
declare @countCanCuDuToan int;
declare @countCanCuSkt int ;
declare @countSktChiTiet int ; 
--count chi tiet
--SELECT @countSktChiTiet = count(*) 
--	FROM NS_SKT_ChungTuChiTiet
--	WHERE 
--		iID_MaDonVi = @IdDonVi 
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
--		iID_MaDonVi = @IdDonVi 
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
--		iID_MaDonVi = @IdDonVi 
--		AND iNamLamViec = @NamLamViec
--		AND iNamNganSach = @NamNganSach
--		AND iID_MaNguonNganSach = @MaNguonNganSach
--		AND iLoaiChungTu = @LoaiChungTu
--		AND iLoai = 0);


--IF (@countSktChiTiet = 0 AND @countCanCuDuToan = 0 AND @countCanCuSkt = 0)
--INSERT INTO @tblDuToan (iID_MLSKT, KyHieu,  MuaHangHienVat, DacThu)
--SELECT 
--       null,
--       mp.sSKT_KyHieu KyHieu,
--       Sum(HangNhap) + Sum(HangMua) + Sum(TuChi) MuaHangHienVat,
--       Sum(DacThu) DacThu
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
--		  AND bKhoa = 1
--          AND iLoaiChungTu = @LoaiChungTu
--          AND (iloai = 0 OR iLoai = 1)
--          AND iLoaiDuToan = 1 )
--   AND iID_MaDonVi = @IdDonVi
--   GROUP BY SXauNoiMa) dtct ON mp.sNS_XauNoiMa = dtct.SXauNoiMa
--WHERE mp.iNamLamViec = @NamLamViec
--GROUP BY mp.sSKT_KyHieu
--ELSE
INSERT INTO @tblDuToan (iID_MLSKT, KyHieu, MuaHangHienVat, DacThu)
SELECT cc.iID_MLSKT, sKyHieu,  sum(isnull(cc.fMuaHangCapHienVat,0)), sum(isnull(cc.fPhanCap,0))
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
		iID_MaDonVi = @IdDonVi 
		AND iNamLamViec = @NamLamViec
		AND iNamNganSach = @NamNganSach
		AND (@loaiNNS = 0 OR iLoaiNguonNganSach = @loaiNNS)
		AND iID_MaNguonNganSach = @MaNguonNganSach
		AND iLoaiChungTu = @LoaiChungTu
		AND iLoai = 0)
	) cc
GROUP BY cc.iID_MLSKT, sKyHieu;


--IF (@countSktChiTiet = 0 AND @countCanCuDuToan = 0 AND @countCanCuSkt = 0)
--INSERT INTO @tblSkt (iID_MLSKT, KyHieu, TuChi, MuaHangHienVat, DacThu)
--SELECT ctct.iID_MLSKT,sKyHieu,
--       ctct.fTuChi,
--       ctct.fMuaHangCapHienVat,
--       ctct.fPhanCap
--FROM NS_SKT_ChungTuChiTiet ctct
--JOIN NS_SKT_ChungTu ct ON ct.iID_CTSoKiemTra = ctct.iID_CTSoKiemTra
--WHERE ct.iNamLamViec = @NamLamViec - 1
--  AND ct.iNamNganSach = @NamNganSach
--  AND ct.iID_MaNguonNganSach = @MaNguonNganSach
--  AND ct.iLoaiChungTu = @LoaiChungTu
--  AND ct.iloai = 3
--  --AND ct.iID_MaDonVi = @IdDonVi
--  AND (ctct.iLoai = 3 and exists(select 1 from DonVi where iID_MaDonVi = @IdDonVi and iLoai = 0) or ctct.iLoai = 4)
--  AND ctct.iID_MaDonVi = @IdDonVi
--ELSE
INSERT INTO @tblSkt (iID_MLSKT,KyHieu, TuChi, MuaHangHienVat, DacThu)
SELECT cc.iID_MLSKT, sKyHieu, sum(isnull(cc.fTuChi,0)), sum(isnull(cc.fMuaHangCapHienVat,0)), sum(isnull(cc.fPhanCap,0))
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
		iID_MaDonVi = @IdDonVi 
		AND iNamLamViec = @NamLamViec
		AND iNamNganSach = @NamNganSach
		AND (@loaiNNS = 0 OR iLoaiNguonNganSach = @loaiNNS)
		AND iID_MaNguonNganSach = @MaNguonNganSach
		AND iLoaiChungTu = @LoaiChungTu
		AND iLoai = 0)
	) cc
GROUP BY cc.iID_MLSKT, sKyHieu;

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
  AND ct.iloai = 0
  AND (@loaiNNS = 0 OR ct.iLoaiNguonNganSach = @loaiNNS)
  --AND ct.bKhoa = 1
  AND ct.iID_MaDonVi = @IdDonVi;

SELECT ml.sSTT STT,
	   ml.sSTTBC SSTTBC,
       ml.bHangCha,
	   ml.iID_MLSKT,
	   ml.iID_MLSKTCha,
	   ml.sL,
	   ml.sK,
	   ml.sM,
	   ml.sNG,
	   ml.sKyHieu,
	   ml.sMoTa,
       isnull(skt.MuaHangHienVat,0) / @dvt AS SoKiemTraMHHVNamTruoc ,
       isnull(dt.MuaHangHienVat,0) / @dvt AS DuToanDauNam ,
       isnull(snc.fHuyDongTonKho,0) / @dvt AS HuyDongTonKho,
       isnull(snc.fTonKhoDenNgay,0) / @dvt AS TonKhoDenNgay,
       isnull(snc.fMuaHangCapHienVat,0) / @dvt AS MuaHangCapHienVat,
	   snc.sGhiChu
FROM NS_SKT_MucLuc ml
LEFT JOIN #SoNhuCauTongHop snc on snc.sKyHieu = ml.sKyHieu
LEFT JOIN @tblSkt skt on skt.KyHieu = ml.sKyHieu
LEFT JOIN @tblDuToan dt on  dt.KyHieu = ml.sKyHieu
WHERE ml.iNamLamViec = @NamLamViec
and (skt.MuaHangHienVat <> 0 or dt.MuaHangHienVat <> 0 or snc.fHuyDongTonKho <> 0 or snc.fTonKhoDenNgay <> 0 or snc.fMuaHangCapHienVat <> 0 or (snc.sGhiChu is not null and snc.sGhiChu != ''));

DROP TABLE #SoNhuCauTongHop;
END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_so_nhu_cau_phu_luc_5]    Script Date: 7/24/2024 7:44:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_skt_tong_hop_so_nhu_cau_phu_luc_5]
	@LoaiChungTu int,
	@IdDonVi nvarchar(max),
	@NamLamViec int,
	@NamNganSach int,
	@MaNguoiNganSach int,
	@MaBQuanLy varchar(max),
	@dvt int,
	@loaiNNS int
AS
BEGIN
declare @sModule nvarchar(max) = 'BUDGET_DEMANDCHECK_DEMAND',
	@idDuToan nvarchar(max) = 'BUDGET_ESTIMATE',
	@idSoKiemTra nvarchar(max) = 'BUDGET_DEMANDCHECK_CHECK'
declare @tblDuToan table (iID_MLSKT uniqueidentifier, KyHieu nvarchar(200), TuChi float, MuaHangHienVat float, DacThu float)
declare @tblSkt table (iID_MLSKT uniqueidentifier, KyHieu nvarchar(200), TuChi float, MuaHangHienVat float, DacThu float)
declare @countCanCuDuToan int;
declare @countCanCuSkt int ;
declare @countSktChiTiet int ; 
declare @countDonVi int;
SELECT @countDonVi = count(*) FROM f_split(@IdDonVi);


INSERT INTO @tblDuToan (iID_MLSKT, KyHieu, TuChi, MuaHangHienVat, DacThu)
SELECT cc.iID_MLSKT, sKyHieu, sum(isnull(cc.fTuChi,0)), sum(isnull(cc.fMuaHangCapHienVat,0)), sum(isnull(cc.fPhanCap,0))
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
		AND iID_MaNguonNganSach = @MaNguoiNganSach
		AND iLoaiChungTu = @LoaiChungTu
		AND (@loaiNNS = 0 OR iLoaiNguonNganSach = @loaiNNS)
		AND iLoai = 0)
	) cc
GROUP BY cc.iID_MLSKT, sKyHieu;


INSERT INTO @tblSkt (iID_MLSKT,KyHieu, TuChi, MuaHangHienVat, DacThu)
SELECT cc.iID_MLSKT, cc.sKyHieu, sum(isnull(cc.fTuChi,0)), sum(isnull(cc.fMuaHangCapHienVat,0)), sum(isnull(cc.fPhanCap,0))
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
		AND iID_MaNguonNganSach = @MaNguoiNganSach
		AND iLoaiChungTu = @LoaiChungTu
		AND (@loaiNNS = 0 OR iLoaiNguonNganSach = @loaiNNS)
		AND iLoai = 0)
	) cc
GROUP BY cc.iID_MLSKT, cc.sKyHieu;

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
  AND ct.iID_MaNguonNganSach = @MaNguoiNganSach
  AND ct.iLoaiChungTu = @LoaiChungTu
  AND ct.iloai = 0
  AND (@loaiNNS = 0 OR ct.iLoaiNguonNganSach = @loaiNNS)
  --AND ct.bKhoa = 1
  AND ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVi))
  ) as sncChiTiet
GROUP BY iID_MLSKT, sKyHieu, sGhiChu;

  SELECT map.sSKT_KyHieu INTO #KyHieuSktBQuanLy
FROM NS_MLSKT_MLNS MAP
JOIN NS_MucLucNganSach mlns ON mlns.sXauNoiMa = map.sNS_XauNoiMa
AND map.iNamLamViec = mlns.iNamLamViec
WHERE map.iNamLamViec = @NamLamViec
AND mlns.iNamLamViec = @NamLamViec and mlns.iID_MaBQuanLy = @MaBQuanLy;

SELECT ml.sSTT STT,
	   ml.sSTTBC SSTTBC,
       ml.bHangCha,
	   ml.iID_MLSKT,
	   ml.iID_MLSKTCha,
	   ml.sL,
	   ml.sK,
	   ml.sM,
	   ml.sNG,
	   ml.sKyHieu,
	   ml.sMoTa,
       isnull(skt.MuaHangHienVat,0) / @dvt AS SoKiemTraMHHVNamTruoc ,
       isnull(dt.MuaHangHienVat,0) / @dvt AS DuToanDauNam ,
       isnull(snc.fTonKhoDenNgay,0) / @dvt AS TonKhoDenNgay,
       isnull(snc.fHuyDongTonKho,0) / @dvt AS HuyDongTonKho,
       isnull(snc.fMuaHangCapHienVat,0) / @dvt AS MuaHangCapHienVat,
	   snc.sGhiChu
FROM NS_SKT_MucLuc ml
LEFT JOIN #SoNhuCauTongHop snc on snc.sKyHieu = ml.sKyHieu
LEFT JOIN @tblSkt skt on skt.KyHieu = ml.sKyHieu
LEFT JOIN @tblDuToan dt on  dt.KyHieu = ml.sKyHieu
WHERE ml.iNamLamViec = @NamLamViec
AND (@MaBQuanLy = '0' OR ml.sKyHieu in (SELECT * FROM #KyHieuSktBQuanLy))
and (skt.MuaHangHienVat <> 0 or dt.MuaHangHienVat <> 0 or snc.fHuyDongTonKho <> 0 or snc.fTonKhoDenNgay <> 0 or snc.fMuaHangCapHienVat <> 0 or (snc.sGhiChu is not null and snc.sGhiChu != ''));

DROP TABLE #SoNhuCauTongHop;
DROP TABLE #KyHieuSktBQuanLy;
END
;
;
;
;
;
;
GO
