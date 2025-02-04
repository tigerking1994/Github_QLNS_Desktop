/****** Object:  StoredProcedure [dbo].[sp_skt_donvi_benhvientuchu_report]    Script Date: 18/10/2023 5:29:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_donvi_benhvientuchu_report]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_donvi_benhvientuchu_report]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tonghop_skt_benhvien_tuchu]    Script Date: 18/10/2023 5:29:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_tonghop_skt_benhvien_tuchu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_tonghop_skt_benhvien_tuchu]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_clone]    Script Date: 18/10/2023 5:29:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc_clone]    Script Date: 18/10/2023 5:29:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc]    Script Date: 18/10/2023 5:29:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra]    Script Date: 18/10/2023 5:29:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra]    Script Date: 18/10/2023 5:29:05 PM ******/
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
            ml.sMoTa
   UNION ALL SELECT ml.iID_MLSKT,
                    ml.sKyHieu,
                    ml.sSTT,
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
            ml.sMoTa) AS ct ON ml.sKyHieu = ct.sKyHieu
WHERE ml.iNamLamViec = @NamLamViec
AND ml.iTrangThai = 1
GROUP BY ml.iID_MLSKTCha,
         ml.iID_MLSKT,
         ml.sM,
         ml.sKyHieu,
         ml.sSTT,
         ml.sMoTa,
         ml.bHangCha
ORDER BY ml.sKyHieu;
END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc]    Script Date: 18/10/2023 5:29:05 PM ******/
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
            ml.sMoTa
   UNION ALL SELECT ml.iID_MLSKT,
                    ml.sKyHieu,
                    ml.sSTT,
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
            ml.sMoTa) AS ct ON ml.sKyHieu = ct.sKyHieu
WHERE 
ml.iNamLamViec = @NamLamViec
AND ml.iTrangThai = 1
GROUP BY ml.iID_MLSKTCha,
         ml.iID_MLSKT,
         ml.sM,
         ml.sKyHieu,
         ml.sSTT,
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
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc_clone]    Script Date: 18/10/2023 5:29:05 PM ******/
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
            ml.sMoTa,
			ctc.iID_MaDonVi
   UNION ALL SELECT ml.iID_MLSKT,
                    ml.sKyHieu,
                    ml.sSTT,
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
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_clone]    Script Date: 18/10/2023 5:29:05 PM ******/
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
            ml.sMoTa,
			ctc.iID_MaDonVi
   UNION ALL SELECT ml.iID_MLSKT,
                    ml.sKyHieu,
                    ml.sSTT,
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
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tonghop_skt_benhvien_tuchu]    Script Date: 18/10/2023 5:29:05 PM ******/
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
	GROUP BY ml.iID_MLSKTCha, ml.iID_MLSKT, ml.sKyHieu, ml.sStt, ml.sMoTa, ml.bHangCha
	ORDER BY sKyHieu
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_donvi_benhvientuchu_report]    Script Date: 18/10/2023 5:29:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_skt_donvi_benhvientuchu_report]
@iNamLamViec int,
@iLoaiNNS int
AS
BEGIN
	SELECT DISTINCT dv.*
	FROM 
	(SELECT ctct.* FROM NS_SKT_ChungTuChiTiet ctct
	JOIN NS_SKT_ChungTu ct ON ctct.iID_CTSoKiemTra = ct.iID_CTSoKiemTra AND ct.iNamLamViec = ctct.iNamLamViec
	AND (@iLoaiNNS = 0 OR ct.iLoaiNguonNganSach = @iLoaiNNS)) AS tbl
	INNER JOIN DonVi dv ON tbl.iID_MaDonVi = dv.iID_MaDonVi AND tbl.iNamLamViec = dv.iNamLamViec

	WHERE 
		tbl.iLoai = 2
		AND tbl.iNamLamViec = @iNamLamViec 
		AND tbl.iLoaiChungTu = 1
		AND dv.iTrangThai = 1
END
;
GO
