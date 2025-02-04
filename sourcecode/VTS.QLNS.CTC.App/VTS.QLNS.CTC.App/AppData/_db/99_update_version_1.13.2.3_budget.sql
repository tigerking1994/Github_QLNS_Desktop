/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_clone]    Script Date: 10/3/2023 1:56:07 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc_clone]    Script Date: 10/3/2023 1:56:07 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc]    Script Date: 10/3/2023 1:56:07 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra]    Script Date: 10/3/2023 1:56:07 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra]    Script Date: 10/3/2023 1:56:07 PM ******/
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
	@dvt int
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
   LEFT JOIN NS_SKT_ChungTuChiTiet ctc ON ml.iID_MLSKT = ctc.iID_MLSKT
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
   LEFT JOIN NS_SKT_ChungTuChiTiet ctc ON ml.iID_MLSKT = ctc.iID_MLSKT
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
            ml.sMoTa) AS ct ON ml.iID_MLSKT = ct.iID_MLSKT
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
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc]    Script Date: 10/3/2023 1:56:07 PM ******/
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
	@dvt int
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
   LEFT JOIN (SELECT ctct.* FROM NS_SKT_ChungTuChiTiet ctct inner join NS_SKT_ChungTu ct on ctct.iID_CTSoKiemTra = ct.iID_CTSoKiemTra 
   WHERE (ctct.iLoai = 3 OR (ctct.iLoai = 2 and ct.iLoai = 2))
     AND ctct.iLoaiChungTu = @LoaiChungTu) ctc
    ON ml.iID_MLSKT = ctc.iID_MLSKT 
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
   LEFT JOIN NS_SKT_ChungTuChiTiet ctc ON ml.iID_MLSKT = ctc.iID_MLSKT
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
            ml.sMoTa) AS ct ON ml.iID_MLSKT = ct.iID_MLSKT
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
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc_clone]    Script Date: 10/3/2023 1:56:07 PM ******/
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
	@dvt int
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
   LEFT JOIN (SELECT ctct.* FROM NS_SKT_ChungTuChiTiet ctct inner join NS_SKT_ChungTu ct on ctct.iID_CTSoKiemTra = ct.iID_CTSoKiemTra 
   WHERE (ctct.iLoai = 3 OR (ctct.iLoai = 2 and ct.iLoai = 2))
     AND ctct.iLoaiChungTu = @LoaiChungTu) ctc
    ON ml.iID_MLSKT = ctc.iID_MLSKT 
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
   LEFT JOIN NS_SKT_ChungTuChiTiet ctc ON ml.iID_MLSKT = ctc.iID_MLSKT
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
			ctc.iID_MaDonVi) AS ct ON ml.iID_MLSKT = ct.iID_MLSKT
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
--select * from NS_SKT_MucLuc
--select * from NS_SKT_ChungTuChiTiet
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_clone]    Script Date: 10/3/2023 1:56:07 PM ******/
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
	@dvt int
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
   LEFT JOIN NS_SKT_ChungTuChiTiet ctc ON ml.iID_MLSKT = ctc.iID_MLSKT
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
   LEFT JOIN NS_SKT_ChungTuChiTiet ctc ON ml.iID_MLSKT = ctc.iID_MLSKT
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
			ctc.iID_MaDonVi
			) AS ct ON ml.iID_MLSKT = ct.iID_MLSKT
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
GO
