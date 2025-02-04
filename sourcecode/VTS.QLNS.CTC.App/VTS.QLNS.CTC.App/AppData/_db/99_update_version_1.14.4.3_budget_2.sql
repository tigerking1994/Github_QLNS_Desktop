/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tonghop_skt_benhvien_tuchu]    Script Date: 5/9/2024 10:55:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_tonghop_skt_benhvien_tuchu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_tonghop_skt_benhvien_tuchu]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_so_nhu_cau_phu_luc_5]    Script Date: 5/9/2024 10:55:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_tong_hop_so_nhu_cau_phu_luc_5]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_tong_hop_so_nhu_cau_phu_luc_5]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_so_nhu_cau_phu_luc_4]    Script Date: 5/9/2024 10:55:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_tong_hop_so_nhu_cau_phu_luc_4]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_tong_hop_so_nhu_cau_phu_luc_4]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_so_nhu_cau_phu_luc_3]    Script Date: 5/9/2024 10:55:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_tong_hop_so_nhu_cau_phu_luc_3]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_tong_hop_so_nhu_cau_phu_luc_3]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_clone]    Script Date: 5/9/2024 10:55:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc_clone]    Script Date: 5/9/2024 10:55:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc]    Script Date: 5/9/2024 10:55:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra]    Script Date: 5/9/2024 10:55:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_so_nhu_cau_tong_hop_tung_don_vi]    Script Date: 5/9/2024 10:55:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_so_nhu_cau_tong_hop_tung_don_vi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_so_nhu_cau_tong_hop_tung_don_vi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_so_nhu_cau_tong_hop]    Script Date: 5/9/2024 10:55:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_so_nhu_cau_tong_hop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_so_nhu_cau_tong_hop]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_so_nhu_cau_theo_nganh_phu_luc]    Script Date: 5/9/2024 10:55:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_so_nhu_cau_theo_nganh_phu_luc]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_so_nhu_cau_theo_nganh_phu_luc]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_phan_bo_so_kiem_tra_dv]    Script Date: 5/9/2024 10:55:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_phan_bo_so_kiem_tra_dv]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_phan_bo_so_kiem_tra_dv]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_phan_bo_kiem_tra_theo_nganh_phu_luc_tong_hop]    Script Date: 5/9/2024 10:55:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_phan_bo_kiem_tra_theo_nganh_phu_luc_tong_hop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_phan_bo_kiem_tra_theo_nganh_phu_luc_tong_hop]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_chi_tiet_so_nhu_cau_phu_luc_5]    Script Date: 5/9/2024 10:55:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_chi_tiet_so_nhu_cau_phu_luc_5]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_chi_tiet_so_nhu_cau_phu_luc_5]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_chi_tiet_so_nhu_cau_phu_luc_4]    Script Date: 5/9/2024 10:55:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_chi_tiet_so_nhu_cau_phu_luc_4]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_chi_tiet_so_nhu_cau_phu_luc_4]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_chi_tiet_so_nhu_cau_phu_luc_3]    Script Date: 5/9/2024 10:55:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_chi_tiet_so_nhu_cau_phu_luc_3]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_chi_tiet_so_nhu_cau_phu_luc_3]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_chi_tiet_phan_bo_kiem_tra_nsbd]    Script Date: 5/9/2024 10:55:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_chi_tiet_phan_bo_kiem_tra_nsbd]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_chi_tiet_phan_bo_kiem_tra_nsbd]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_chi_tiet_phan_bo_kiem_tra_nsbd]    Script Date: 5/9/2024 10:55:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_skt_chi_tiet_phan_bo_kiem_tra_nsbd]
	@MaDonVi varchar(max),
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@dvt int
AS
BEGIN
SELECT ml.iID_MLSKT IIdMlskt,
       ml.iID_MLSKTCha IIdMlsktCha,
       ml.sKyHieu,
	   ml.sSTT STT,
	   ml.sSTTBC SSTTBC,
	   ml.bHangCha,
       ml.sNG,
       ml.sMoTa,
       ml.sNG_Cha sNgCha,
       QuyetToan =SUM(IsNull(A.QuyetToan,0))/@dvt,
       DuToan =SUM(IsNull(A.DuToan,0))/@dvt,
       TuChi =SUM(IsNull(A.TuChi,0))/@dvt,
       PhanCap =SUM(IsNull(A.PhanCap,0))/@dvt,
       MuaHangHienVat =SUM(IsNull(A.MuaHangHienVat,0))/@dvt,
       DacThu =SUM(IsNull(A.PhanCap,0))/@dvt,
	   ThongBaoDV = SUM(IsNull(A.ThongBaoDV, 0))/@dvt ,
	   HuyDongTonKho = SUM(ISNULL(A.HuyDongTonKho, 0))/@dvt
FROM
(select * from NS_SKT_MucLuc where iTrangThai = 1 and iNamLamViec = @NamLamViec) ml right join
  (SELECT ml.iID_MLSKT ,
          ml.iID_MLSKTCha,
          ml.sKyHieu,
          ml.sNG,
          ml.sMoTa,
          ml.sNG_Cha,
          ct.iID_MaDonVi,
          QuyetToan =0,
          DuToan =0,
          IsNull(ct.fTuChi, 0) TuChi,
          IsNull(ct.fPhanCap, 0) PhanCap,
          IsNull(ct.fMuaHangCapHienVat, 0) MuaHangHienVat,
          IsNull(ct.fPhanCap, 0) DacThu,
		  IsNull(ct.fThongBaoDonVi, 0) ThongBaoDV,
		  ISNULL(ct.fhuydongtonkho, 0) as HuyDongTonKho
   FROM NS_SKT_ChungTuChiTiet ct
   RIGHT JOIN NS_SKT_MucLuc ml ON ct.sKyHieu = ml.sKyHieu
   AND ml.iNamLamViec = @NamLamViec
   AND ml.iTrangThai = 1
   WHERE ct.iNamLamViec=@NamLamViec
     AND ct.iNamNganSach = @NamNganSach
     AND ct.iID_MaNguonNganSach = @NguonNganSach
     AND ct.iLoai=4
     AND ct.iLoaiChungTu = 2
     AND (@MaDonVi is null OR ct.iID_MaDonVi = @MaDonVi)
	 AND ct.iID_MaDonVi != '999'
   UNION SELECT ml.iID_MLSKT ,
                ml.iID_MLSKTCha,
                ml.sKyHieu,
                ml.sNG,
                ml.sMoTa,
                ml.sNG_Cha,
                ct.iID_MaDonVi,
                QuyetToan =0,
                DuToan =0,
                IsNull(ct.fTuChi, 0) TuChi,
                IsNull(ct.fPhanCap, 0) PhanCap,
                IsNull(ct.fMuaHangCapHienVat, 0) MuaHangHienVat,
                IsNull(ct.fPhanCap, 0) DacThu,
				IsNull(ct.fThongBaoDonVi, 0) ThongBaoDV,
				ISNULL(ct.fhuydongtonkho, 0) as HuyDongTonKho
   FROM NS_SKT_ChungTuChiTiet ct
   RIGHT JOIN NS_SKT_MucLuc ml ON ct.sKyHieu = ml.sKyHieu
   AND ml.iNamLamViec = @NamLamViec
   AND ml.iTrangThai = 1
   WHERE ct.iNamLamViec=@NamLamViec
     AND ct.iNamNganSach = @NamNganSach
     AND ct.iID_MaNguonNganSach = @NguonNganSach
     AND ct.iLoai=4
     AND ct.iLoaiChungTu = 1
	 AND ct.fTuChi > 0) AS A on ml.sKyHieu = A.sKyHieu
LEFT JOIN
  (SELECT iID_MaDonVi AS id,
          sTenDonVi
   FROM DonVi
   WHERE iTrangThai=1
     AND iNamLamViec=@NamLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
GROUP BY ml.iID_MLSKT,
         ml.iID_MLSKTCha,
         ml.sKyHieu,
		 ml.sSTT,
		 ml.sSTTBC,
		 ml.bHangCha,
         ml.sNG,
         ml.sMoTa,
         ml.sNG_Cha
		 order by ml.sKyHieu
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_chi_tiet_so_nhu_cau_phu_luc_3]    Script Date: 5/9/2024 10:55:09 AM ******/
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
       isnull(skt.TuChi, 0) / @dvt AS SoKiemTraNamTruoc,
       isnull(dt.TuChi, 0) / @dvt AS DuToanDauNam,
       isnull(snc.fTonKhoDenNgay, 0) / @dvt AS TonKhoDenNgay,
       isnull(snc.fHuyDongTonKho, 0) / @dvt AS HuyDongTonKho,
       isnull(snc.fTuChi, 0) / @dvt AS TuChi,
	   snc.sGhiChu
FROM NS_SKT_MucLuc ml

LEFT JOIN  #SoNhuCauTongHopGroup snc ON snc.sKyHieu = ml.sKyHieu
LEFT JOIN @tblSkt skt ON skt.sKyHieu = ml.sKyHieu
LEFT JOIN @tblDuToan dt ON dt.KyHieu = ml.sKyHieu

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
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_chi_tiet_so_nhu_cau_phu_luc_4]    Script Date: 5/9/2024 10:55:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_skt_chi_tiet_so_nhu_cau_phu_luc_4]
	@LoaiChungTu int,
	@IdDonVi varchar(max),
	@NamLamViec int,
	@NamNganSach int,
	@MaNguonNganSach int,
	@dvt int
AS
BEGIN
declare @sModule nvarchar(max) = 'BUDGET_DEMANDCHECK_DEMAND',
	@idDuToan nvarchar(max) = 'BUDGET_ESTIMATE',
	@idSoKiemTra nvarchar(max) = 'BUDGET_DEMANDCHECK_CHECK'
declare @tblSkt table (iID_MLSKT uniqueidentifier,sKyHieu nvarchar(200), TuChi float, MuaHangHienVat float, DacThu float)
declare @countCanCuSkt int ;
declare @countSktChiTiet int ; 

--SELECT @countSktChiTiet = count(*)
--FROM NS_SKT_ChungTuChiTiet
--WHERE iID_MaDonVi = @IdDonVi
--  AND iNamLamViec = @NamLamViec
--  AND iNamNganSach = @NamNganSach
--  AND iID_MaNguonNganSach = @MaNguonNganSach
--  AND iLoaiChungTu = @LoaiChungTu
--  AND iLoai = 0;

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

--IF (@countCanCuSkt = 0 and @countSktChiTiet = 0)
--INSERT INTO @tblSkt (iID_MLSKT, sKyHieu,TuChi, MuaHangHienVat, DacThu)
--SELECT ctct.iID_MLSKT,
--       ctct.sKyHieu,
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
INSERT INTO @tblSkt (iID_MLSKT,sKyHieu, TuChi, MuaHangHienVat, DacThu)
SELECT cc.iID_MLSKT, sKyHieu,sum(isnull(cc.fTuChi,0)), sum(isnull(cc.fMuaHangCapHienVat,0)), sum(isnull(cc.fPhanCap,0))
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
       AND iLoai = 0)
	) cc
GROUP BY cc.iID_MLSKT, sKyHieu;


SELECT ctct.iID_MLSKT,
       ctct.sKyHieu,
       ctct.fTuChi,
       ctct.fHuyDongTonKho,
       ctct.fTonKhoDenNgay,
       ctct.fMuaHangCapHienVat,
       ctct.fPhanCap as fDacThu,
       ctct.sGhiChu INTO #SoNhuCauTongHop
FROM NS_SKT_ChungTuChiTiet ctct
JOIN NS_SKT_ChungTu ct ON ct.iID_CTSoKiemTra = ctct.iID_CTSoKiemTra
WHERE ct.iNamLamViec = @NamLamViec
  AND ct.iNamNganSach = @NamNganSach
  AND ct.iID_MaNguonNganSach = @MaNguonNganSach
  AND ct.iLoaiChungTu = @LoaiChungTu
  AND ct.iloai = 0
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
       isnull(skt.DacThu,0) / @dvt AS SoKiemTraDacThuNamTruoc ,
       isnull(snc.fDacThu,0) / @dvt AS DacThu,
	   snc.sGhiChu
FROM NS_SKT_MucLuc ml
LEFT JOIN #SoNhuCauTongHop snc on snc.sKyHieu = ml.sKyHieu 
LEFT JOIN @tblSkt skt on skt.sKyHieu = ml.sKyHieu
WHERE ml.iNamLamViec = @NamLamViec
and (skt.DacThu <> 0 or snc.fDacThu <> 0 or (snc.sGhiChu is not null and snc.sGhiChu != ''));

DROP TABLE #SoNhuCauTongHop;
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_chi_tiet_so_nhu_cau_phu_luc_5]    Script Date: 5/9/2024 10:55:09 AM ******/
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
	@dvt int
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
       isnull(snc.fMuaHangCapHienVat,0) / @dvt AS MuaHangCapHienVat,
	   snc.sGhiChu
FROM NS_SKT_MucLuc ml
LEFT JOIN #SoNhuCauTongHop snc on snc.sKyHieu = ml.sKyHieu
LEFT JOIN @tblSkt skt on skt.KyHieu = ml.sKyHieu
LEFT JOIN @tblDuToan dt on  dt.KyHieu = ml.sKyHieu
WHERE ml.iNamLamViec = @NamLamViec
and (skt.MuaHangHienVat <> 0 or dt.MuaHangHienVat <> 0 or snc.fHuyDongTonKho <> 0 or snc.fMuaHangCapHienVat <> 0 or (snc.sGhiChu is not null and snc.sGhiChu != ''));

DROP TABLE #SoNhuCauTongHop;
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_phan_bo_kiem_tra_theo_nganh_phu_luc_tong_hop]    Script Date: 5/9/2024 10:55:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_skt_phan_bo_kiem_tra_theo_nganh_phu_luc_tong_hop]
	@Nganh varchar(max),
	@MaDonVi varchar(max),
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@Loai int,
	@dvt int,
	@bTongHop bit
AS
BEGIN
SELECT iID_MLSKT IIdMlskt,
       iID_MLSKTCha IIdMlsktCha,
       dt_dv.id idDonVi,
       dt_dv.sTenDonVi,
       dt_dv.iLoai,
       sKyHieu,
       sSTT STT,
       sSTTBC SSTTBC,
       bHangCha,
       sNG,
       sMoTa,
       sNG_Cha sNgCha,
       QuyetToan =SUM(IsNull(A.QuyetToan,0))/@dvt,
       DuToan =SUM(IsNull(A.DuToan,0))/@dvt,
       TuChi =SUM(IsNull(A.TuChi,0))/@dvt,
       PhanCap =SUM(IsNull(A.PhanCap,0))/@dvt,
       MuaHangHienVat =SUM(IsNull(A.MuaHangHienVat,0))/@dvt,
       DacThu =SUM(IsNull(A.PhanCap,0))/@dvt
FROM
  (SELECT ml.iID_MLSKT ,
                ml.iID_MLSKTCha,
                ml.sKyHieu,
                ml.sNG,
                ml.sMoTa,
                ml.sNG_Cha,
                ml.sSTT,
                ml.sSTTBC,
                ml.bHangCha,
                ct.iID_MaDonVi,
                sTenDonVi,
                QuyetToan =0,
                DuToan =0,
                IsNull(ct.fTuChi, 0) TuChi,
                IsNull(ct.fPhanCap, 0) PhanCap,
                IsNull(ct.fMuaHangCapHienVat, 0) MuaHangHienVat,
                IsNull(ct.fPhanCap, 0) DacThu
   FROM NS_SKT_ChungTuChiTiet ct
   INNER JOIN NS_SKT_ChungTu chungtu ON ct.iID_CTSoKiemTra = chungtu.iID_CTSoKiemTra and chungtu.iLoai <> 0
   RIGHT JOIN NS_SKT_MucLuc ml ON ct.sKyHieu = ml.sKyHieu
   AND ml.iNamLamViec = @NamLamViec
   AND ml.iTrangThai = 1
   WHERE ct.iNamLamViec=@NamLamViec
     AND ct.iNamNganSach = @NamNganSach
     AND ct.iID_MaNguonNganSach = @NguonNganSach
     AND CT.iLoai<>3
     --AND ((ct.iLoai = @Loai AND @Loai <> 4) OR (ct.iLoai IN (2, 4) AND @Loai = 4))
     AND ct.iLoaiChungTu = 1
     AND ((@bTongHop = 0)  or (@bTongHop = 1 AND chungtu.bDaTongHop = @bTongHop))
     AND ml.sNG in
       (SELECT *
        FROM f_split(@Nganh)))AS A 
LEFT JOIN
  (SELECT iID_MaDonVi AS id,
          sTenDonVi, iLoai
   FROM DonVi
   WHERE iTrangThai=1
   AND iNamLamViec=@NamLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   --WHERE (@Loai = 0 and dt_dv.iLoai != 0) or @Loai <> 0
GROUP BY iID_MLSKT,
         iID_MLSKTCha,
         dt_dv.id,
         dt_dv.sTenDonVi,
         sKyHieu,
         sSTT,
         sSTTBC,
         bHangCha,
         sNG,
         sMoTa,
         sNG_Cha,
         iLoai
         order by sKyHieu
END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_phan_bo_so_kiem_tra_dv]    Script Date: 5/9/2024 10:55:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_skt_phan_bo_so_kiem_tra_dv]
	@idDV varchar(20),
	@idChungTu varchar(200),
	@LoaiChungTu int,
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@dvt int,
	@iLoai int,
	@iLoaiNNS int
AS
BEGIN
SELECT ml.sM AS M ,
       ml.sKyHieu AS KyHieu ,
       ml.sMoTa AS MoTa ,
       ml.iID_MLSKT AS IdMucLuc ,
       ml.iID_MLSKTCha AS IdParent ,
       ml.sSTT AS STT ,
       ml.sSTTBC AS SSTTBC ,
       ml.bHangCha ,
	   ct.sGhiChu,
       TuChi =ISNULL(SUM(ct.fTuChi), 0)/@dvt ,
       HuyDong =ISNULL(SUM(ct.fHuyDongTonKho), 0)/@dvt ,
       PhanCap =ISNULL(SUM(ct.fPhanCap), 0)/@dvt ,
       MuaHangHienVat =ISNULL(SUM(ct.fMuaHangCapHienVat), 0)/@dvt ,
       DacThu =ISNULL(SUM(ct.fPhanCap), 0)/@dvt ,
       TongCongNSSD = ISNULL(SUM(ct.fTuChi), 0)/@dvt ,
       TongCongNSBD = ISNULL(SUM(ct.fTuChi), 0)/@dvt
FROM NS_SKT_MucLuc ml
LEFT JOIN
(
	SELECT ctct.* FROM NS_SKT_ChungTuChiTiet ctct
	INNER JOIN NS_SKT_ChungTu ctc 
	ON ctct.iID_CTSoKiemTra = ctc.iID_CTSoKiemTra 
	AND ctc.iNamLamViec = ctct.iNamLamViec
	AND ctc.iNamLamViec = @NamLamViec
	AND (@iLoaiNNS = 0 OR ctc.iLoaiNguonNganSach = @iLoaiNNS)
) ct
ON ml.sKyHieu = ct.sKyHieu AND ml.iNamLamViec = ct.iNamLamViec
AND ml.iTrangThai=1
AND ml.iNamLamViec = @NamLamViec
AND ct.iNamLamViec = @NamLamViec
AND ct.iLoai = @iLoai
AND ct.iLoaiChungTu = @LoaiChungTu
AND ct.iID_MaDonVi = @idDV
--AND ct.iID_CTSoKiemTra = @idChungTu
AND ct.iNamNganSach = @NamNganSach
AND ct.iID_MaNguonNganSach = @NguonNganSach


GROUP BY ml.sM ,
         ml.sKyHieu ,
         ml.sMoTa ,
         ml.iID_MLSKT ,
         ml.iID_MLSKTCha ,
         ml.sSTT ,
         ml.sSTTBC ,
         ml.bHangCha,
		 ct.sGhiChu
ORDER BY ml.sKyHieu;
END
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_so_nhu_cau_theo_nganh_phu_luc]    Script Date: 5/9/2024 10:55:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_skt_so_nhu_cau_theo_nganh_phu_luc]
	@Nganh varchar(max),
	@MaDonVi varchar(max),
	@IdChungTu varchar(max),
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@Loai int,
	@dvt int
AS
BEGIN
SELECT iID_MLSKT IIdMlskt,
       iID_MLSKTCha IIdMlsktCha,
	   dt_dv.id idDonVi,
	   dt_dv.sTenDonVi,
       sKyHieu,
	   sSTT STT,
	   sSTTBC STTBC,
	   bHangCha,
       sNG,
       sMoTa,
       sNG_Cha sNgCha,
       QuyetToan =SUM(IsNull(A.QuyetToan,0))/@dvt,
       DuToan =SUM(IsNull(A.DuToan,0))/@dvt,
       TuChi =SUM(IsNull(A.TuChi,0))/@dvt,
       PhanCap =SUM(IsNull(A.PhanCap,0))/@dvt,
       MuaHangHienVat =SUM(IsNull(A.MuaHangHienVat,0))/@dvt,
       DacThu =SUM(IsNull(A.PhanCap,0))/@dvt
FROM
  (SELECT ml.iID_MLSKT ,
                ml.iID_MLSKTCha,
                ml.sKyHieu,
                ml.sNG,
                ml.sMoTa,
                ml.sNG_Cha,
				ml.sSTT,
				ml.sSTTBC,
				ml.bHangCha,
                ct.iID_MaDonVi,
				sTenDonVi,
                QuyetToan =0,
                DuToan =0,
                IsNull(ct.fTuChi, 0) TuChi,
                IsNull(ct.fPhanCap, 0) PhanCap,
                IsNull(ct.fMuaHangCapHienVat, 0) MuaHangHienVat,
                IsNull(ct.fPhanCap, 0) DacThu
   FROM NS_SKT_ChungTuChiTiet ct
   RIGHT JOIN NS_SKT_MucLuc ml ON ct.sKyHieu = ml.sKyHieu
   AND ml.iNamLamViec = @NamLamViec
   AND ml.iTrangThai = 1
   WHERE ct.iNamLamViec=@NamLamViec
     AND ct.iNamNganSach = @NamNganSach
     AND ct.iID_MaNguonNganSach = @NguonNganSach
     AND ct.iLoai= @Loai
     AND ct.iLoaiChungTu = 1
	 AND ct.iID_CTSoKiemTra in (select * from f_split(@IdChungTu))
     AND ml.sNG in
       (SELECT *
        FROM f_split(@Nganh)))AS A 
LEFT JOIN
  (SELECT iID_MaDonVi AS id,
          sTenDonVi
   FROM DonVi
   WHERE iTrangThai=1
     AND iNamLamViec=@NamLamViec AND iLoai=1) AS dt_dv ON A.iID_MaDonVi=dt_dv.id		--th�m iLoai = 1
GROUP BY iID_MLSKT,
         iID_MLSKTCha,
		 dt_dv.id,
	     dt_dv.sTenDonVi,
         sKyHieu,
		 sSTT,
		 sSTTBC,
		 bHangCha,
         sNG,
         sMoTa,
         sNG_Cha
		 order by sKyHieu
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_so_nhu_cau_tong_hop]    Script Date: 5/9/2024 10:55:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_skt_so_nhu_cau_tong_hop]
	@ListIdChungTuTongHop ntext,
	@LoaiChungTu int,
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@dvt int,
	@iLoai int
AS
BEGIN
SELECT ml.sM AS M ,
       ml.sKyHieu AS KyHieu ,
       ml.sMoTa AS MoTa ,
       ml.iID_MLSKT IdMucLuc ,
       ml.iID_MLSKTCha AS IdParent ,
       ml.sSTT AS STT ,
       ml.sSTTBC AS SSTTBC ,
       ml.bHangCha ,
       TuChi =ISNULL(SUM(ct.fTuChi), 0)/@dvt ,
       HuyDong =ISNULL(SUM(ct.fHuyDongTonKho), 0)/@dvt ,
       PhanCap =ISNULL(SUM(ct.fPhanCap), 0)/@dvt ,
       MuaHangHienVat =ISNULL(SUM(ct.fMuaHangCapHienVat), 0)/@dvt ,
       TongCongNSSD = ISNULL(SUM(ct.fTuChi), 0)/@dvt ,
       TongCongNSBD = ISNULL(SUM(ct.fTuChi), 0)/@dvt
FROM NS_SKT_MucLuc ml
LEFT JOIN NS_SKT_ChungTuChiTiet ct ON ml.sKyHieu = ct.sKyHieu
AND ml.iTrangThai=1
AND ml.iNamLamViec=@NamLamViec
AND ct.iNamLamViec = @NamLamViec
AND ct.iNamNganSach = @NamNganSach
AND ct.iID_MaNguonNganSach = @NguonNganSach
AND ct.iLoaiChungTu =@LoaiChungTu
AND ct.iLoai=@iLoai
AND (iID_MaDonVi in
       (SELECT *
        FROM f_split(@ListIdChungTuTongHop)))
GROUP BY ml.sM ,
         ml.sKyHieu ,
         ml.sMoTa ,
         ml.iID_MLSKT ,
         ml.iID_MLSKTCha ,
         ml.sSTT ,
         ml.sSTTBC ,
         ml.bHangCha
ORDER BY ml.sKyHieu;
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_so_nhu_cau_tong_hop_tung_don_vi]    Script Date: 5/9/2024 10:55:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_skt_so_nhu_cau_tong_hop_tung_don_vi]
	@ListIdChungTuTongHop ntext,
	@LoaiChungTu int,
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@dvt int,
	@iLoai int,
	@MaBQuanLy varchar(max),
	@loaiNNS int
AS
BEGIN

SELECT map.sSKT_KyHieu INTO #KyHieuSktBQuanLy
FROM NS_MLSKT_MLNS MAP
JOIN NS_MucLucNganSach mlns ON mlns.sXauNoiMa = map.sNS_XauNoiMa
AND map.iNamLamViec = mlns.iNamLamViec
WHERE map.iNamLamViec = @NamLamViec
AND mlns.iNamLamViec = @NamLamViec and mlns.iID_MaBQuanLy = @MaBQuanLy;

SELECT ml.sM AS M ,
       ml.sKyHieu AS KyHieu ,
       ml.sMoTa AS MoTa ,
       ml.iID_MLSKT IdMucLuc ,
       ml.iID_MLSKTCha AS IdParent ,
       ml.sSTT AS STT ,
       ml.sSTTBC AS SSTTBC ,
       ml.bHangCha ,
	   ct.iID_MaDonVi AS IdDonVi,
       TuChi =ISNULL(SUM(ct.fTuChi), 0)/@dvt ,
       HuyDong =ISNULL(SUM(ct.fHuyDongTonKho), 0)/@dvt ,
       PhanCap =ISNULL(SUM(ct.fPhanCap), 0)/@dvt ,
       MuaHangHienVat =ISNULL(SUM(ct.fMuaHangCapHienVat), 0)/@dvt ,
       TongCongNSSD = ISNULL(SUM(ct.fTuChi), 0)/@dvt ,
       TongCongNSBD = ISNULL(SUM(ct.fTuChi), 0)/@dvt
FROM NS_SKT_MucLuc ml
JOIN NS_SKT_ChungTuChiTiet ct ON ml.sKyHieu = ct.sKyHieu
JOIN NS_SKT_ChungTu chungTu ON ct.iID_CTSoKiemTra = chungtu.iID_CTSoKiemTra
AND ml.iTrangThai=1
AND (@loaiNNS = 0 OR chungTu.iLoaiNguonNganSach = @loaiNNS)
AND ml.iNamLamViec=@NamLamViec
AND ct.iNamLamViec = @NamLamViec
AND ct.iNamNganSach = @NamNganSach
AND ct.iID_MaNguonNganSach = @NguonNganSach
AND ct.iLoaiChungTu =@LoaiChungTu
AND ct.iLoai=@iLoai
AND (ct.iID_MaDonVi in
       (SELECT *
        FROM f_split(@ListIdChungTuTongHop)))
AND (@MaBQuanLy = '0' OR ml.sKyHieu in (SELECT * FROM #KyHieuSktBQuanLy))
GROUP BY ml.sM ,
         ml.sKyHieu ,
         ml.sMoTa ,
         ml.iID_MLSKT ,
         ml.iID_MLSKTCha ,
         ml.sSTT ,
         ml.sSTTBC ,
         ml.bHangCha,
		 ct.iID_MaDonVi
ORDER BY ml.sKyHieu;

drop table #KyHieuSktBQuanLy;
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra]    Script Date: 5/9/2024 10:55:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra]
	@idDV varchar(max),
	@LoaiChungTu int,
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@dvt int,
	@iLoaiNNS int
AS
BEGIN

SELECT ml.iID_MLSKTCha IIdMlsktCha,
       ml.iID_MLSKT IIdMlskt,
       ml.sM,
       ml.sKyHieu,
       ml.sSTT,
       ml.sSTTBC,
       ml.sMoTa,
       ml.bHangCha ,
       IsNull(sum(ct.sumToTal), 0)/@dvt TongTuChi ,
       IsNull(sum(ct.sumTotalMuaHangHienVat), 0)/@dvt TongMuaHangHienVat ,
       IsNull(sum(ct.sumTotalDacThu), 0)/@dvt TongDacThu ,
       IsNull(sum(ct.sumDonVi), 0)/@dvt TongTuChiPB ,
       IsNull(sum(ct.sumDonViMuaHangHienVat), 0)/@dvt TongMuaHangHienVatPB ,
       IsNull(sum(ct.sumDonViDacThu), 0)/@dvt TongDacThuPB
FROM NS_SKT_MucLuc ml
LEFT JOIN
  (SELECT ml.iID_MLSKT,
          ml.sKyHieu,
          ml.sSTT,
          ml.sSTTBC,
          ml.sMoTa ,
          sum(ISNull(ctc.fTuChi, 0)) sumTotal ,
          sum(ISNull(ctc.fMuaHangCapHienVat, 0)) sumTotalMuaHangHienVat ,
          sum(ISNull(ctc.fPhanCap, 0)) sumTotalDacThu ,
          sumDonVi = 0 ,
          sumDonViMuaHangHienVat = 0 ,
          sumDonViDacThu = 0
   FROM NS_SKT_MucLuc ml
   LEFT JOIN 
   (
	SELECT ctct.* FROM NS_SKT_ChungTuChiTiet ctct
	JOIN NS_SKT_ChungTu ct ON ctct.iID_CTSoKiemTra = ct.iID_CTSoKiemTra AND ct.iNamLamViec = ctct.iNamLamViec
	AND (@iLoaiNNS = 0 OR ct.iLoaiNguonNganSach = @iLoaiNNS)
   ) ctc ON ml.sKyHieu = ctc.sKyHieu
   AND ml.iNamLamViec = @NamLamViec
   AND ctc.iNamLamViec = @NamLamViec
   AND ctc.iNamNganSach = @NamNganSach
   AND ctc.iID_MaNguonNganSach = @NguonNganSach
   WHERE ctc.iLoai = 3
     AND ctc.iLoaiChungTu = @LoaiChungTu
   GROUP BY ml.iID_MLSKT,
            ml.sKyHieu,
            ml.sSTT,
            ml.sSTTBC,
            ml.sMoTa
   UNION ALL SELECT ml.iID_MLSKT,
                    ml.sKyHieu,
                    ml.sSTT,
                    ml.sSTTBC,
                    ml.sMoTa ,
                    sumTotal = 0 ,
                    sumTotalMuaHangHienVat = 0 ,
                    sumTotalDacThu = 0 ,
                    sum(ISNull(ctc.fTuChi, 0)) sumDonVi ,
                    sum(ISNull(ctc.fMuaHangCapHienVat, 0)) sumDonViMuaHangHienVat ,
                    sum(ISNull(ctc.fPhanCap, 0)) sumDonViDacThu
   FROM NS_SKT_MucLuc ml
   LEFT JOIN 
   (
	SELECT ctct.* FROM NS_SKT_ChungTuChiTiet ctct
	JOIN NS_SKT_ChungTu ct ON ctct.iID_CTSoKiemTra = ct.iID_CTSoKiemTra AND ct.iNamLamViec = ctct.iNamLamViec
	AND (@iLoaiNNS = 0 OR ct.iLoaiNguonNganSach = @iLoaiNNS)
   ) ctc ON ml.sKyHieu = ctc.sKyHieu
   AND ml.iNamLamViec = @NamLamViec
   AND ctc.iNamLamViec = @NamLamViec
   AND ctc.iNamNganSach = @NamNganSach
   AND ctc.iID_MaNguonNganSach = @NguonNganSach
   WHERE (ctc.iLoai = 4  or ctc.iLoai =2) --WHERE ctc.iLoai = 4 
     AND ctc.iLoaiChungTu = @LoaiChungTu
     AND iID_MaDonVi in
       (SELECT *
        FROM f_split(@idDV))
     AND exists
       (SELECT iID_CTSoKiemTra
        FROM NS_SKT_ChungTu ct
        WHERE ct.iID_CTSoKiemTra = ctc.iID_CTSoKiemTra)
   GROUP BY ml.iID_MLSKT,
            ml.sKyHieu,
            ml.sSTT,
            ml.sSTTBC,
            ml.sMoTa) AS ct ON ml.sKyHieu = ct.sKyHieu
WHERE ml.iNamLamViec = @NamLamViec
AND ml.iTrangThai = 1
GROUP BY ml.iID_MLSKTCha,
         ml.iID_MLSKT,
         ml.sM,
         ml.sKyHieu,
         ml.sSTT,
         ml.sSTTBC,
         ml.sMoTa,
         ml.bHangCha
ORDER BY ml.sKyHieu;
END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc]    Script Date: 5/9/2024 10:55:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc]
	@idDV varchar(max),
	@LoaiChungTu int,
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@dvt int,
	@iLoaiNNS int
AS
BEGIN

SELECT ml.iID_MLSKTCha IIdMlsktCha,
       ml.iID_MLSKT IIdMlskt,
       ml.sM,
       ml.sKyHieu,
       ml.sSTT,
       ml.sSTTBC,
       ml.sMoTa,
       ml.bHangCha ,
       IsNull(sum(ct.sumToTal), 0)/@dvt TongTuChi ,
       IsNull(sum(ct.sumTotalMuaHangHienVat), 0)/@dvt TongMuaHangHienVat ,
       IsNull(sum(ct.sumTotalDacThu), 0)/@dvt TongDacThu ,
       IsNull(sum(ct.sumDonVi), 0)/@dvt TongTuChiPB ,
       IsNull(sum(ct.sumDonViMuaHangHienVat), 0)/@dvt TongMuaHangHienVatPB ,
       IsNull(sum(ct.sumDonViDacThu), 0)/@dvt TongDacThuPB
FROM NS_SKT_MucLuc ml
LEFT JOIN
  (SELECT ml.iID_MLSKT,
          ml.sKyHieu,
          ml.sSTT,
		  ml.sSTTBC,
          ml.sMoTa ,
          sum(ISNull(ctc.fTuChi, 0)) sumTotal ,
          sum(ISNull(ctc.fMuaHangCapHienVat, 0)) sumTotalMuaHangHienVat ,
          sum(ISNull(ctc.fPhanCap, 0)) sumTotalDacThu ,
          sumDonVi = 0 ,
          sumDonViMuaHangHienVat = 0 ,
          sumDonViDacThu = 0
   FROM NS_SKT_MucLuc ml
   LEFT JOIN 
   (SELECT ctct.* FROM NS_SKT_ChungTuChiTiet ctct 
   JOIN NS_SKT_ChungTu ct ON ctct.iID_CTSoKiemTra = ct.iID_CTSoKiemTra 
   WHERE (ctct.iLoai = 3 OR (ctct.iLoai = 2 and ct.iLoai = 2))
   AND ctct.iLoaiChungTu = @LoaiChungTu
   AND (@iLoaiNNS = 0 OR ct.iLoaiNguonNganSach = @iLoaiNNS)
   ) ctc
   ON ml.sKyHieu = ctc.sKyHieu 
   AND ml.iNamLamViec = @NamLamViec
   AND ctc.iNamLamViec = @NamLamViec
   AND ctc.iNamNganSach = @NamNganSach
   AND ctc.iID_MaNguonNganSach = @NguonNganSach
   
   GROUP BY ml.iID_MLSKT,
            ml.sKyHieu,
            ml.sSTT,
			ml.sSTTBC,
            ml.sMoTa
   UNION ALL SELECT ml.iID_MLSKT,
                    ml.sKyHieu,
                    ml.sSTT,
					ml.sSTTBC,
                    ml.sMoTa ,
                    sumTotal = 0 ,
                    sumTotalMuaHangHienVat = 0 ,
                    sumTotalDacThu = 0 ,
                    sum(ISNull(ctc.fTuChi, 0)) sumDonVi ,
                    sum(ISNull(ctc.fMuaHangCapHienVat, 0)) sumDonViMuaHangHienVat ,
                    sum(ISNull(ctc.fPhanCap, 0)) sumDonViDacThu
   FROM NS_SKT_MucLuc ml
   LEFT JOIN 
   (
	SELECT ctct.* FROM NS_SKT_ChungTuChiTiet ctct
	JOIN NS_SKT_ChungTu ct ON ctct.iID_CTSoKiemTra = ct.iID_CTSoKiemTra AND ct.iNamLamViec = ctct.iNamLamViec
	AND (@iLoaiNNS = 0 OR ct.iLoaiNguonNganSach = @iLoaiNNS)
   ) ctc ON ml.sKyHieu = ctc.sKyHieu
   AND ml.iNamLamViec = @NamLamViec
   AND ctc.iNamLamViec = @NamLamViec
   AND ctc.iNamNganSach = @NamNganSach
   AND ctc.iID_MaNguonNganSach = @NguonNganSach
   WHERE (ctc.iLoai = 4  or ctc.iLoai =2) --WHERE ctc.iLoai = 4 
     AND ctc.iLoaiChungTu = @LoaiChungTu
     AND iID_MaDonVi in
       (SELECT *
        FROM f_split(@idDV))
     AND exists
       (SELECT iID_CTSoKiemTra
        FROM NS_SKT_ChungTu ct
        WHERE ct.iID_CTSoKiemTra = ctc.iID_CTSoKiemTra)
   GROUP BY ml.iID_MLSKT,
            ml.sKyHieu,
            ml.sSTT,
			ml.sSTTBC,
            ml.sMoTa) AS ct ON ml.sKyHieu = ct.sKyHieu
WHERE 
ml.iNamLamViec = @NamLamViec
AND ml.iTrangThai = 1
GROUP BY ml.iID_MLSKTCha,
         ml.iID_MLSKT,
         ml.sM,
         ml.sKyHieu,
         ml.sSTT,
	     ml.sSTTBC,
         ml.sMoTa,
         ml.bHangCha
ORDER BY ml.sKyHieu;
END
;
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc_clone]    Script Date: 5/9/2024 10:55:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc_clone]
	@idDV varchar(max),
	@LoaiChungTu int,
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@dvt int,
	@iLoaiNNS int
AS
BEGIN

SELECT ml.iID_MLSKTCha IIdMlsktCha,
       ml.iID_MLSKT IIdMlskt,
       ml.sM,
       ml.sKyHieu,
       ml.sSTT,
       ml.sSTTBC,
       ml.sMoTa,
       ml.bHangCha ,
       IsNull(sum(ct.sumToTal), 0)/@dvt TongTuChi ,
       IsNull(sum(ct.sumTotalMuaHangHienVat), 0)/@dvt TongMuaHangHienVat ,
       IsNull(sum(ct.sumTotalDacThu), 0)/@dvt TongDacThu ,
       IsNull(sum(ct.sumDonVi), 0)/@dvt TongTuChiPB ,
       IsNull(sum(ct.sumDonViMuaHangHienVat), 0)/@dvt TongMuaHangHienVatPB ,
       IsNull(sum(ct.sumDonViDacThu), 0)/@dvt TongDacThuPB,
	   -- update 02/10/2023 dungnv them donvi
	   ct.IIdMaDonVi

FROM NS_SKT_MucLuc ml
LEFT JOIN
  (SELECT ml.iID_MLSKT,
          ml.sKyHieu,
          ml.sSTT,
		  ml.sSTTBC,
          ml.sMoTa ,
          sum(ISNull(ctc.fTuChi, 0)) sumTotal ,
          sum(ISNull(ctc.fMuaHangCapHienVat, 0)) sumTotalMuaHangHienVat ,
          sum(ISNull(ctc.fPhanCap, 0)) sumTotalDacThu ,
          sumDonVi = 0 ,
          sumDonViMuaHangHienVat = 0 ,
          sumDonViDacThu = 0,
		  ctc.iID_MaDonVi as IIdMaDonVi
   FROM NS_SKT_MucLuc ml
   LEFT JOIN 
   (SELECT ctct.* FROM NS_SKT_ChungTuChiTiet ctct 
	JOIN NS_SKT_ChungTu ct ON ctct.iID_CTSoKiemTra = ct.iID_CTSoKiemTra 
	WHERE (ctct.iLoai = 3 OR (ctct.iLoai = 2 and ct.iLoai = 2))
    AND ctct.iLoaiChungTu = @LoaiChungTu
	AND (@iLoaiNNS = 0 OR ct.iLoaiNguonNganSach = @iLoaiNNS)
   ) ctc
   ON ml.sKyHieu = ctc.sKyHieu 
   AND ml.iNamLamViec = @NamLamViec
   AND ctc.iNamLamViec = @NamLamViec
   AND ctc.iNamNganSach = @NamNganSach
   AND ctc.iID_MaNguonNganSach = @NguonNganSach
   
   GROUP BY ml.iID_MLSKT,
            ml.sKyHieu,
            ml.sSTT,
			ml.sSTTBC,
            ml.sMoTa,
			ctc.iID_MaDonVi
   UNION ALL SELECT ml.iID_MLSKT,
                    ml.sKyHieu,
                    ml.sSTT,
					ml.sSTTBC,
                    ml.sMoTa ,
                    sumTotal = 0 ,
                    sumTotalMuaHangHienVat = 0 ,
                    sumTotalDacThu = 0 ,
                    sum(ISNull(ctc.fTuChi, 0)) sumDonVi ,
                    sum(ISNull(ctc.fMuaHangCapHienVat, 0)) sumDonViMuaHangHienVat ,
                    sum(ISNull(ctc.fPhanCap, 0)) sumDonViDacThu,
					ctc.iID_MaDonVi as IIdMaDonVi

   FROM NS_SKT_MucLuc ml
   LEFT JOIN 
   (
	SELECT ctct.* FROM NS_SKT_ChungTuChiTiet ctct
	JOIN NS_SKT_ChungTu ct ON ctct.iID_CTSoKiemTra = ct.iID_CTSoKiemTra AND ct.iNamLamViec = ctct.iNamLamViec
	AND (@iLoaiNNS = 0 OR ct.iLoaiNguonNganSach = @iLoaiNNS)
   ) ctc ON ml.sKyHieu = ctc.sKyHieu
   AND ml.iNamLamViec = @NamLamViec
   AND ctc.iNamLamViec = @NamLamViec
   AND ctc.iNamNganSach = @NamNganSach
   AND ctc.iID_MaNguonNganSach = @NguonNganSach
   WHERE (ctc.iLoai = 4  or ctc.iLoai =2) --WHERE ctc.iLoai = 4 
     AND ctc.iLoaiChungTu = @LoaiChungTu
     AND iID_MaDonVi in
       (SELECT *
        FROM f_split(@idDV))
     AND exists
       (SELECT iID_CTSoKiemTra
        FROM NS_SKT_ChungTu ct
        WHERE ct.iID_CTSoKiemTra = ctc.iID_CTSoKiemTra)
   GROUP BY ml.iID_MLSKT,
            ml.sKyHieu,
            ml.sSTT,
			ml.sSTTBC,
            ml.sMoTa,
			ctc.iID_MaDonVi) AS ct ON ml.sKyHieu = ct.sKyHieu
WHERE 
ml.iNamLamViec = @NamLamViec
AND ml.iTrangThai = 1
GROUP BY ml.iID_MLSKTCha,
         ml.iID_MLSKT,
         ml.sM,
         ml.sKyHieu,
         ml.sSTT,
		 ml.sSTTBC,
         ml.sMoTa,
         ml.bHangCha,
		 ct.IIdMaDonVi
ORDER BY ml.sKyHieu;
END
;
;
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_clone]    Script Date: 5/9/2024 10:55:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_clone]
	@idDV varchar(max),
	@LoaiChungTu int,
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@dvt int,
	@iLoaiNNS int
AS
BEGIN

SELECT ml.iID_MLSKTCha IIdMlsktCha,
       ml.iID_MLSKT IIdMlskt,
       ml.sM,
       ml.sKyHieu,
       ml.sSTT,
       ml.sSTTBC,
       ml.sMoTa,
       ml.bHangCha ,
       IsNull(sum(ct.sumToTal), 0)/@dvt TongTuChi ,
       IsNull(sum(ct.sumTotalMuaHangHienVat), 0)/@dvt TongMuaHangHienVat ,
       IsNull(sum(ct.sumTotalDacThu), 0)/@dvt TongDacThu ,
       IsNull(sum(ct.sumDonVi), 0)/@dvt TongTuChiPB ,
       IsNull(sum(ct.sumDonViMuaHangHienVat), 0)/@dvt TongMuaHangHienVatPB ,
       IsNull(sum(ct.sumDonViDacThu), 0)/@dvt TongDacThuPB,
	   ct.IIdMaDonVi
FROM NS_SKT_MucLuc ml
LEFT JOIN
  (SELECT ml.iID_MLSKT,
          ml.sKyHieu,
          ml.sSTT,
          ml.sSTTBC,
          ml.sMoTa ,
          sum(ISNull(ctc.fTuChi, 0)) sumTotal ,
          sum(ISNull(ctc.fMuaHangCapHienVat, 0)) sumTotalMuaHangHienVat ,
          sum(ISNull(ctc.fPhanCap, 0)) sumTotalDacThu ,
          sumDonVi = 0 ,
          sumDonViMuaHangHienVat = 0 ,
          sumDonViDacThu = 0,
		  ctc.iID_MaDonVi as IIdMaDonVi
   FROM NS_SKT_MucLuc ml
   LEFT JOIN 
   (
	SELECT ctct.* FROM NS_SKT_ChungTuChiTiet ctct
	JOIN NS_SKT_ChungTu ct ON ctct.iID_CTSoKiemTra = ct.iID_CTSoKiemTra AND ct.iNamLamViec = ctct.iNamLamViec
	AND (@iLoaiNNS = 0 OR ct.iLoaiNguonNganSach = @iLoaiNNS)
   ) ctc ON ml.sKyHieu = ctc.sKyHieu
   AND ml.iNamLamViec = @NamLamViec
   AND ctc.iNamLamViec = @NamLamViec
   AND ctc.iNamNganSach = @NamNganSach
   AND ctc.iID_MaNguonNganSach = @NguonNganSach
   WHERE ctc.iLoai = 3
     AND ctc.iLoaiChungTu = @LoaiChungTu
   GROUP BY ml.iID_MLSKT,
            ml.sKyHieu,
            ml.sSTT,
            ml.sSTTBC,
            ml.sMoTa,
			ctc.iID_MaDonVi
   UNION ALL SELECT ml.iID_MLSKT,
                    ml.sKyHieu,
                    ml.sSTT,
					ml.sSTTBC,
                    ml.sMoTa ,
                    sumTotal = 0 ,
                    sumTotalMuaHangHienVat = 0 ,
                    sumTotalDacThu = 0 ,
                    sum(ISNull(ctc.fTuChi, 0)) sumDonVi ,
                    sum(ISNull(ctc.fMuaHangCapHienVat, 0)) sumDonViMuaHangHienVat ,
                    sum(ISNull(ctc.fPhanCap, 0)) sumDonViDacThu,
					ctc.iID_MaDonVi as IIdMaDonVi

   FROM NS_SKT_MucLuc ml
   LEFT JOIN 
   (
	SELECT ctct.* FROM NS_SKT_ChungTuChiTiet ctct
	JOIN NS_SKT_ChungTu ct ON ctct.iID_CTSoKiemTra = ct.iID_CTSoKiemTra AND ct.iNamLamViec = ctct.iNamLamViec
	AND (@iLoaiNNS = 0 OR ct.iLoaiNguonNganSach = @iLoaiNNS)
   ) ctc ON ml.sKyHieu = ctc.sKyHieu
   AND ml.iNamLamViec = @NamLamViec
   AND ctc.iNamLamViec = @NamLamViec
   AND ctc.iNamNganSach = @NamNganSach
   AND ctc.iID_MaNguonNganSach = @NguonNganSach
   WHERE (ctc.iLoai = 4  or ctc.iLoai = 2) --WHERE ctc.iLoai = 4 
     AND ctc.iLoaiChungTu = @LoaiChungTu
     AND iID_MaDonVi in
       (SELECT *
        FROM f_split(@idDV))
     AND exists
       (SELECT iID_CTSoKiemTra
        FROM NS_SKT_ChungTu ct
        WHERE ct.iID_CTSoKiemTra = ctc.iID_CTSoKiemTra)
   GROUP BY ml.iID_MLSKT,
            ml.sKyHieu,
            ml.sSTT,
			ml.sSTTBC,
            ml.sMoTa,
			ctc.iID_MaDonVi
			) AS ct ON ml.sKyHieu = ct.sKyHieu
WHERE 
ml.iNamLamViec = @NamLamViec
AND ml.iTrangThai = 1
GROUP BY ml.iID_MLSKTCha,
         ml.iID_MLSKT,
         ml.sM,
         ml.sKyHieu,
         ml.sSTT,
	     ml.sSTTBC,
         ml.sMoTa,
         ml.bHangCha,
		 ct.IIdMaDonVi
ORDER BY ml.sKyHieu;
END
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_so_nhu_cau_phu_luc_3]    Script Date: 5/9/2024 10:55:09 AM ******/
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
			WHERE (ctct.sKyHieu = sncChiTiet.sKyHieu AND ctct.sGhiChu <> '') 
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
       isnull(skt.TuChi,0) / @dvt AS SoKiemTraNamTruoc ,
       isnull(dt.TuChi,0) / @dvt AS DuToanDauNam ,
       isnull(snc.fTonKhoDenNgay,0) / @dvt AS TonKhoDenNgay,
       isnull(snc.fHuyDongTonKho,0) / @dvt AS HuyDongTonKho,
       isnull(snc.fTuChi,0) / @dvt AS TuChi,
	   snc.sGhiChu
FROM NS_SKT_MucLuc ml
LEFT JOIN #SoNhuCauTongHop snc on snc.sKyHieu = ml.sKyHieu
LEFT JOIN @tblSkt skt on skt.KyHieu = ml.sKyHieu
LEFT JOIN @tblDuToan dt on dt.KyHieu = ml.sKyHieu
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
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_so_nhu_cau_phu_luc_4]    Script Date: 5/9/2024 10:55:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_skt_tong_hop_so_nhu_cau_phu_luc_4]
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
declare @tblSkt table (iID_MLSKT uniqueidentifier, KyHieu nvarchar(200), MuaHangHienVat float, DacThu float)
declare @countCanCuSkt int ;
declare @countSktChiTiet int ; 
--SELECT @countSktChiTiet = count(*) 
--	FROM NS_SKT_ChungTuChiTiet
--	WHERE 
--		iID_MaDonVi = @IdDonVi 
--		AND iNamLamViec = @NamLamViec
--		AND iNamNganSach = @NamNganSach
--		AND iID_MaNguonNganSach = @MaNguonNganSach
--		AND iLoaiChungTu = @LoaiChungTu
--		AND iLoai = 0;

--	SELECT @countCanCuSkt = count(*) 
--	FROM NS_SKT_ChungTuChiTiet_CanCu 
--	WHERE 
--		iNamLamViec = @NamLamViec
--		AND iID_CanCu IN (SELECT iID_CauHinh_CanCu 
--						FROM NS_CauHinh_CanCu 
--						WHERE sModule = @sModule 
--							AND iID_MaChucNang = @idSoKiemTra 
--							AND inamLamViec = @NamLamViec
--							AND iNamCancu = @NamLamViec - 1)
--								   		AND iiID_CTSoKiemTra in 
--		(SELECT iID_CTSoKiemTra
--			FROM NS_SKT_ChungTu
--		WHERE 
--		iID_MaDonVi = @IdDonVi 
--		AND iNamLamViec = @NamLamViec
--		AND iNamNganSach = @NamNganSach
--		AND iID_MaNguonNganSach = @MaNguonNganSach
--		AND iLoaiChungTu = @LoaiChungTu
--		AND iLoai = 0);

--IF (@countCanCuSkt = 0 and @countSktChiTiet = 0)
--INSERT INTO @tblSkt (iID_MLSKT, KyHieu, MuaHangHienVat, DacThu)
--SELECT ctct.iID_MLSKT,
--       sKyHieu,
--       ctct.fMuaHangCapHienVat,
--       ctct.fPhanCap as fDacThu 
--FROM NS_SKT_ChungTuChiTiet ctct
--JOIN NS_SKT_ChungTu ct ON ct.iID_CTSoKiemTra = ctct.iID_CTSoKiemTra
--WHERE ct.iNamLamViec = @NamLamViec - 1
--  AND ct.iNamNganSach = @NamNganSach
--  AND ct.iID_MaNguonNganSach = @MaNguonNganSach
--  AND ct.iLoaiChungTu = @LoaiChungTu
--  AND ct.iloai = 3
--  --AND ct.iID_MaDonVi = @IdDonVi
--  AND ctct.iLoai = 3;
--  --AND ctct.iID_MaDonVi = @IdDonVi;
--  ELSE
INSERT INTO @tblSkt (iID_MLSKT, KyHieu, MuaHangHienVat, DacThu)
SELECT cc.iID_MLSKT, sKyHieu, sum(isnull(cc.fMuaHangCapHienVat,0)), sum(isnull(cc.fPhanCap,0))
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
		iID_MaDonVi in  (SELECT * FROM f_split(@IdDonVi))
		AND iNamLamViec = @NamLamViec
		AND iNamNganSach = @NamNganSach
		AND iID_MaNguonNganSach = @MaNguonNganSach
		AND iLoaiChungTu = @LoaiChungTu
		AND iLoai = 0)
	) cc
GROUP BY cc.iID_MLSKT,sKyHieu;


select iID_MLSKT,
       sKyHieu,
       sum(isnull(fTuChi,0)) fTuChi,
       sum(isnull(fHuyDongTonKho,0)) fHuyDongTonKho,
       sum(isnull(fTonKhoDenNgay,0)) fTonKhoDenNgay,
       sum(isnull(fMuaHangCapHienVat,0)) fMuaHangCapHienVat,
       sum(isnull(fDacThu,0)) fDacThu,
       sGhiChu INTO #SoNhuCauTongHop 
	   from 
(SELECT ctct.iID_MLSKT,
       ctct.sKyHieu,
       isnull(ctct.fTuChi,0) fTuChi,
       isnull(ctct.fHuyDongTonKho,0) fHuyDongTonKho,
       isnull(ctct.fTonKhoDenNgay,0) fTonKhoDenNgay,
       isnull(ctct.fMuaHangCapHienVat,0) fMuaHangCapHienVat,
       isnull(ctct.fPhanCap,0) fDacThu,
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
       isnull(skt.DacThu,0) / @dvt AS SoKiemTraDacThuNamTruoc ,
       isnull(snc.fDacThu,0) / @dvt AS DacThu,
	   snc.sGhiChu
FROM NS_SKT_MucLuc ml
LEFT JOIN #SoNhuCauTongHop snc on snc.sKyHieu = ml.sKyHieu 
LEFT JOIN @tblSkt skt on skt.KyHieu = ml.sKyHieu 
WHERE ml.iNamLamViec = @NamLamViec
AND (@MaBQuanLy = '0' OR ml.sKyHieu in (SELECT * FROM #KyHieuSktBQuanLy))
and (skt.DacThu <> 0 or snc.fDacThu <> 0 or (snc.sGhiChu is not null and snc.sGhiChu != ''));

DROP TABLE #SoNhuCauTongHop;
DROP TABLE #KyHieuSktBQuanLy;
END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_so_nhu_cau_phu_luc_5]    Script Date: 5/9/2024 10:55:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_skt_tong_hop_so_nhu_cau_phu_luc_5]
	@LoaiChungTu int,
	@IdDonVi varchar(max),
	@NamLamViec int,
	@NamNganSach int,
	@MaNguoiNganSach int,
	@MaBQuanLy varchar(max),
	@dvt int
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

----count chi tiet
--SELECT @countSktChiTiet = count(*) 
--	FROM NS_SKT_ChungTuChiTiet
--	WHERE 
--		iID_MaDonVi in (select * from f_split(@IdDonVi)) 
--		AND iNamLamViec = @NamLamViec
--		AND iNamNganSach = @NamNganSach
--		AND iID_MaNguonNganSach = @MaNguoiNganSach
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
--		AND iID_MaNguonNganSach = @MaNguoiNganSach
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
--		AND iID_MaNguonNganSach = @MaNguoiNganSach
--		AND iLoaiChungTu = @LoaiChungTu
--		AND iLoai = 0);

--IF (@countSktChiTiet = 0 and @countCanCuDuToan = 0 and @countCanCuSkt = 0)
--INSERT INTO @tblDuToan (iID_MLSKT, KyHieu, TuChi, MuaHangHienVat, DacThu)
--SELECT null,
--       mp.sSKT_KyHieu KyHieu,
--       Sum(TuChi) TuChi,
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
--          AND iID_MaNguonNganSach = @MaNguoiNganSach
--          AND iLoaiChungTu = @LoaiChungTu
--          AND (iloai = 0 OR iLoai = 1)
--          AND iLoaiDuToan = 1 )
--		  AND iID_MaDonVi in (SELECT * FROM f_split(@IdDonVi))
--   GROUP BY SXauNoiMa) dtct ON mp.sNS_XauNoiMa = dtct.SXauNoiMa
--WHERE mp.iNamLamViec = @NamLamViec
--GROUP BY mp.sSKT_KyHieu
--ELSE
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
		AND iLoai = 0)
	) cc
GROUP BY cc.iID_MLSKT, sKyHieu;


--IF (@countSktChiTiet = 0 and @countCanCuDuToan = 0 and @countCanCuSkt = 0)
--INSERT INTO @tblSkt (iID_MLSKT, TuChi, MuaHangHienVat, DacThu)
--SELECT ctct.iID_MLSKT,
--       ctct.fTuChi,
--       ctct.fMuaHangCapHienVat,
--       ctct.fPhanCap
--FROM NS_SKT_ChungTuChiTiet ctct
--JOIN NS_SKT_ChungTu ct ON ct.iID_CTSoKiemTra = ctct.iID_CTSoKiemTra
--WHERE ct.iNamLamViec = @NamLamViec - 1
--  AND ct.iNamNganSach = @NamNganSach
--  AND ct.iID_MaNguonNganSach = @MaNguoiNganSach
--  AND ct.iLoaiChungTu = @LoaiChungTu
--  AND ct.iloai = 3
--  --AND ct.iID_MaDonVi = @IdDonVi
--  AND ctct.iLoai = 3
--  --AND ctct.iID_MaDonVi = @IdDonVi;
--  ELSE
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
       isnull(snc.fHuyDongTonKho,0) / @dvt AS HuyDongTonKho,
       isnull(snc.fMuaHangCapHienVat,0) / @dvt AS MuaHangCapHienVat,
	   snc.sGhiChu
FROM NS_SKT_MucLuc ml
LEFT JOIN #SoNhuCauTongHop snc on snc.sKyHieu = ml.sKyHieu
LEFT JOIN @tblSkt skt on skt.KyHieu = ml.sKyHieu
LEFT JOIN @tblDuToan dt on  dt.KyHieu = ml.sKyHieu
WHERE ml.iNamLamViec = @NamLamViec
AND (@MaBQuanLy = '0' OR ml.sKyHieu in (SELECT * FROM #KyHieuSktBQuanLy))
and (skt.MuaHangHienVat <> 0 or dt.MuaHangHienVat <> 0 or snc.fHuyDongTonKho <> 0 or snc.fMuaHangCapHienVat <> 0 or (snc.sGhiChu is not null and snc.sGhiChu != ''));

DROP TABLE #SoNhuCauTongHop;
DROP TABLE #KyHieuSktBQuanLy;
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tonghop_skt_benhvien_tuchu]    Script Date: 5/9/2024 10:55:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_rpt_skt_tonghop_skt_benhvien_tuchu]
@lstDonVi nvarchar(max),
@iNamLamViec int,
@iLoaiNNS int

AS
BEGIN
	SELECT * INTO #tmpDonVi
	FROM f_split(@lstDonVi)
	
	SELECT ml.iID_MLSKTCha as IIdMlsktCha,
		ml.iID_MLSKT IIdMlskt,
		ml.sKyHieu,
		ml.sStt,
		ml.sSTTBC,
		ml.sMoTa,
		ml.bHangCha,
		SUM(ISNULL(dt.fTuChi, 0)) as TongTuChi,
		0 as TongTuChiPB,
		SUM(ISNULL(dt.fMuaHangCapHienVat, 0)) as TongMuaHangHienVat,
		SUM(ISNULL(dt.fMuaHangCapHienVat, 0)) as TongMuaHangHienVatPB,
		SUM(ISNULL(dt.fPhanCap, 0)) as TongDacThu,
		SUM(ISNULL(dt.fPhanCap, 0)) as TongDacThuPB
	FROM NS_SKT_MucLuc as ml 
	LEFT JOIN (
	SELECT ctct.* FROM NS_SKT_ChungTuChiTiet ctct
	JOIN NS_SKT_ChungTu ct ON ctct.iID_CTSoKiemTra = ct.iID_CTSoKiemTra AND ct.iNamLamViec = ctct.iNamLamViec
	AND (@iLoaiNNS = 0 OR ct.iLoaiNguonNganSach = @iLoaiNNS)
	WHERE ctct.iLoai = 2 AND ctct.iNamLamViec = @iNamLamViec AND ctct.iLoaiChungTu = 1 
	AND ctct.iID_MaDonVi IN (SELECT * FROM #tmpDonVi)) AS dt ON ml.sKyHieu = dt.sKyHieu
	WHERE ml.iTrangThai = 1
		AND ml.iNamLamViec = @iNamLamViec
	GROUP BY ml.iID_MLSKTCha, ml.iID_MLSKT, ml.sKyHieu, ml.sStt, ml.sSTTBC, ml.sMoTa, ml.bHangCha
	ORDER BY sKyHieu
END
;
;
GO
