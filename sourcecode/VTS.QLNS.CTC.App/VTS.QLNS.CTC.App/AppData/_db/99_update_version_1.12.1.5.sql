/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_thang_dulieu_insert]    Script Date: 24/10/2022 2:30:30 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_bangluong_thang_dulieu_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_bangluong_thang_dulieu_insert]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_donvi_benhvientuchu_report]    Script Date: 24/10/2022 2:30:30 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_donvi_benhvientuchu_report]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_donvi_benhvientuchu_report]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tonghop_skt_benhvien_tuchu]    Script Date: 24/10/2022 2:30:30 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_tonghop_skt_benhvien_tuchu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_tonghop_skt_benhvien_tuchu]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_trinh_ky]    Script Date: 24/10/2022 2:30:30 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_trinh_ky]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_trinh_ky]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc]    Script Date: 24/10/2022 2:30:30 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra]    Script Date: 24/10/2022 2:30:30 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra]    Script Date: 24/10/2022 2:30:30 PM ******/
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
AND ml.iNamLamViec = @NamLamViec
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
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc]    Script Date: 24/10/2022 2:30:31 PM ******/
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
   LEFT JOIN NS_SKT_ChungTuChiTiet ctc ON ml.iID_MLSKT = ctc.iID_MLSKT
   AND ml.iNamLamViec = @NamLamViec
   AND ctc.iNamLamViec = @NamLamViec
   AND ctc.iNamNganSach = @NamNganSach
   AND ctc.iID_MaNguonNganSach = @NguonNganSach
   WHERE (ctc.iLoai = 3 OR ctc.iLoai = 2)
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
AND ml.iNamLamViec = @NamLamViec
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
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_trinh_ky]    Script Date: 24/10/2022 2:30:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_trinh_ky]
	@idDV varchar(max),
	@LoaiChungTu int,
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@ChungTuSnc varchar(max),
	@dvt int
AS
BEGIN
SELECT rs.sTenDonVi,
	   rs.iID_MaDonVi MaDonVi,
       IsNull(sum(rs.sumSnc), 0)/@dvt TongSncTuChi ,
       IsNull(sum(rs.sumSncMuaHangHienVat), 0)/@dvt TongSncMuaHangHienVat ,
       IsNull(sum(rs.sumSncDacThu), 0)/@dvt TongSncDacThu ,
       IsNull(sum(rs.sumSkt), 0)/@dvt TongSktTuChi ,
       IsNull(sum(rs.sumSktMuaHangHienVat), 0)/@dvt TongSktMuaHangHienVat ,
       IsNull(sum(rs.sumSktDacThu), 0)/@dvt TongSktDacThu,
	   IsNull(sum(rs.sumSktNamTruoc), 0)/@dvt TongSktTuChiNamTruoc ,
       IsNull(sum(rs.sumSktMuaHangHienVatNamTruoc), 0)/@dvt TongSktMuaHangHienVatNamTruoc ,
       IsNull(sum(rs.sumSktDacThuNamTruoc), 0)/@dvt TongSktDacThuNamTruoc
FROM (SELECT dv.sTenDonVi,
		  dv.iID_MaDonVi,
		  sum(ISNull(ctc.fTuChi, 0)) sumSnc ,
          sum(ISNull(ctc.fMuaHangCapHienVat, 0)) sumSncMuaHangHienVat ,
          sum(ISNull(ctc.fPhanCap, 0)) sumSncDacThu ,
          sumSkt = 0 ,
          sumSktMuaHangHienVat = 0 ,
          sumSktDacThu = 0,
		  sumSktNamTruoc = 0 ,
          sumSktMuaHangHienVatNamTruoc = 0 ,
          sumSktDacThuNamTruoc = 0
   FROM DonVi dv
   JOIN NS_SKT_ChungTuChiTiet ctc ON dv.iID_MaDonVi = ctc.iID_MaDonVi
   AND dv.iNamLamViec = @NamLamViec
   AND ctc.iNamLamViec = @NamLamViec
   AND ctc.iNamNganSach = @NamNganSach
   AND ctc.iID_MaNguonNganSach = @NguonNganSach
   WHERE ctc.iLoai = 0
     AND ctc.iLoaiChungTu = @LoaiChungTu
	 AND ctc.iID_MaDonVi in
       (SELECT *
        FROM f_split(@idDV))
     AND exists
       (SELECT iID_CTSoKiemTra
        FROM NS_SKT_ChungTu ct
        WHERE ct.iID_CTSoKiemTra = ctc.iID_CTSoKiemTra and ct.iID_CTSoKiemTra in (select * from f_split(@ChungTuSnc)))
   GROUP BY dv.sTenDonVi,
		   dv.iID_MaDonVi
   UNION ALL SELECT dv.sTenDonVi,
		  dv.iID_MaDonVi,
		  sumSnc = 0,
          sumSncMuaHangHienVat = 0,
          sumSncDacThu = 0,
          sum(ISNull(ctc.fTuChi, 0)) sumSkt ,
          sum(ISNull(ctc.fMuaHangCapHienVat, 0)) sumSktMuaHangHienVat ,
          sum(ISNull(ctc.fPhanCap, 0)) sumSktDacThu,
		  sumSktNamTruoc = 0 ,
          sumSktMuaHangHienVatNamTruoc = 0 ,
          sumSktDacThuNamTruoc = 0
   FROM DonVi dv
   JOIN NS_SKT_ChungTuChiTiet ctc ON dv.iID_MaDonVi = ctc.iID_MaDonVi
   AND dv.iNamLamViec = @NamLamViec
   AND ctc.iNamLamViec = @NamLamViec
   AND ctc.iNamNganSach = @NamNganSach
   AND ctc.iID_MaNguonNganSach = @NguonNganSach
	WHERE (ctc.iLoai = 4 or ctc.iLoai =2)--WHERE (ctc.iLoai = 4 )
     AND ctc.iLoaiChungTu = @LoaiChungTu
	 AND ctc.iID_MaDonVi in
       (SELECT *
        FROM f_split(@idDV))
     AND exists
       (SELECT iID_CTSoKiemTra
        FROM NS_SKT_ChungTu ct
        WHERE ct.iID_CTSoKiemTra = ctc.iID_CTSoKiemTra)
   GROUP BY dv.sTenDonVi,
		   dv.iID_MaDonVi
UNION ALL SELECT dv.sTenDonVi,
		  dv.iID_MaDonVi,
		  sumSnc = 0 ,
          sumSncMuaHangHienVat = 0,
          sumSncDacThu = 0,
          sumSkt = 0 ,
          sumSktMuaHangHienVat = 0 ,
          sumSktDacThu = 0,
		  sum(ISNull(ctc.fTuChi, 0)) sumSktNamTruoc,
          sum(ISNull(ctc.fMuaHangCapHienVat, 0)) sumSktMuaHangHienVatNamTruoc,
          sum(ISNull(ctc.fPhanCap, 0)) sumSktDacThuNamTruoc
   FROM DonVi dv
   JOIN NS_SKT_ChungTuChiTiet ctc ON dv.iID_MaDonVi = ctc.iID_MaDonVi
   AND dv.iNamLamViec = @NamLamViec
   AND ctc.iNamLamViec = @NamLamViec - 1
   AND ctc.iNamNganSach = @NamNganSach
   AND ctc.iID_MaNguonNganSach = @NguonNganSach
   WHERE (ctc.iLoai = 4 or ctc.iLoai =2)--WHERE (ctc.iLoai = 4 )
     AND ctc.iLoaiChungTu = @LoaiChungTu 
	 AND ctc.iID_MaDonVi in
       (SELECT *
        FROM f_split(@idDV))
     AND exists
       (SELECT iID_CTSoKiemTra
        FROM NS_SKT_ChungTu ct
        WHERE ct.iID_CTSoKiemTra = ctc.iID_CTSoKiemTra)
   GROUP BY dv.sTenDonVi,
		   dv.iID_MaDonVi) as rs
		   Where rs.sumSnc != 0 
		   or rs.sumSncMuaHangHienVat != 0 
		   or rs.sumSncDacThu != 0 
		   or rs.sumSkt != 0 
		   or rs.sumSktMuaHangHienVat != 0 
		   or rs.sumSktDacThu != 0 
		   or rs.sumSktNamTruoc != 0 
		   or rs.sumSktMuaHangHienVatNamTruoc != 0 
		   or rs.sumSktDacThuNamTruoc != 0 
		   GROUP BY rs.sTenDonVi,
					rs.iID_MaDonVi;
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tonghop_skt_benhvien_tuchu]    Script Date: 24/10/2022 2:30:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_rpt_skt_tonghop_skt_benhvien_tuchu]
@lstDonVi nvarchar(max),
@iNamLamViec int
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
	LEFT JOIN (select * from NS_SKT_ChungTuChiTiet where iLoai = 2 and iNamLamViec = @iNamLamViec and iLoaiChungTu = 1 and iID_MaDonVi in (SELECT * FROM #tmpDonVi)) as dt on ml.sKyHieu = dt.sKyHieu
	WHERE ml.iTrangThai = 1
		AND ml.iNamLamViec = @iNamLamViec
	GROUP BY ml.iID_MLSKTCha, ml.iID_MLSKT, ml.sKyHieu, ml.sStt, ml.sMoTa, ml.bHangCha
	ORDER BY sKyHieu
END


GO
/****** Object:  StoredProcedure [dbo].[sp_skt_donvi_benhvientuchu_report]    Script Date: 24/10/2022 2:30:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_skt_donvi_benhvientuchu_report]
@iNamLamViec int
AS
BEGIN
	SELECT DISTINCT dv.*
	FROM NS_SKT_ChungTu as tbl
	INNER JOIN DonVi as dv on tbl.iID_MaDonVi = dv.iID_MaDonVi 
	WHERE tbl.iLoai = 2
		AND tbl.iNamLamViec = @iNamLamViec 
		AND dv.iNamLamViec = @iNamLamViec
		AND tbl.iLoaiChungTu = 1
		AND dv.iTrangThai = 1
END
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_thang_dulieu_insert]    Script Date: 24/10/2022 2:30:31 PM ******/
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


UPDATE TL_PhuCap_MLNS
SET Ma_Cb = '3.3'
WHERE Ma_Cb = '3'