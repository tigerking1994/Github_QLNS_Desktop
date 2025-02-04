/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc]    Script Date: 8/22/2024 7:19:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra]    Script Date: 8/22/2024 7:19:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra]    Script Date: 8/22/2024 7:19:25 PM ******/
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

DECLARE @DonViBanThan NVARCHAR(MAX)
	SET @DonViBanThan = (SELECT SUBSTRING(( SELECT ',' + iID_MaDonVi AS [text()] 
						FROM DonVi pr
						WHERE iNamLamViec = @NamLamViec 
								and iLoai = 1
								and iCapDonVi = (SELECT TOP 1 iCapDonVi FROM DonVi WHERE iNamLamViec = @NamLamViec and iLoai = 0)
						FOR XML PATH (''), TYPE
							).value('text()[1]','nvarchar(max)'), 2, 1000))

SELECT ml.iID_MLSKTCha IIdMlsktCha,
       ml.iID_MLSKT IIdMlskt,
       ml.sM,
	   case when ml.iID_MLSKTCha = '00000000-0000-0000-0000-000000000000' then ml.sL
		   else null end sL,
	   ml.sK,
	   ml.sNG,
	   ml.sKyHieu,
       ml.sSTT,
       ml.sSTTBC,
       ml.sMoTa,
       ml.bHangCha ,
       --IsNull(sum(ct.sumToTal), 0)/@dvt TongTuChi ,
		(IsNull(sum(ct.sumDonViBanthan), 0) + IsNull(sum(ct.sumDonVi), 0))/@dvt TongTuChi,
		IsNull(sum(ct.sumTotalMuaHangHienVat), 0)/@dvt TongMuaHangHienVat ,
		IsNull(sum(ct.sumTotalDacThu), 0)/@dvt TongDacThu ,
		IsNull(sum(ct.sumDonVi), 0)/@dvt TongTuChiPB ,
		IsNull(sum(ct.sumDonViMuaHangHienVat), 0)/@dvt TongMuaHangHienVatPB ,
		IsNull(sum(ct.sumDonViDacThu), 0)/@dvt TongDacThuPB,
		IsNull(sum(ct.sumDonViBanthan), 0)/@dvt TuChiBanThan
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
		  sumDonViBanthan = 0
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

   UNION ALL 
   
   SELECT ml.iID_MLSKT,
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
		sum(ISNull(banthan.fTuChi, 0)) sumDonViBanthan
   FROM NS_SKT_MucLuc ml
   LEFT JOIN 
	(
	SELECT ctct.sKyHieu, sum(isnull(ctct.fTuChi, 0)) fTuChi, sum(isnull(ctct.fMuaHangCapHienVat, 0)) fMuaHangCapHienVat, sum(isnull(ctct.fPhanCap, 0)) fPhanCap FROM NS_SKT_ChungTuChiTiet ctct
	JOIN NS_SKT_ChungTu ct ON ctct.iID_CTSoKiemTra = ct.iID_CTSoKiemTra AND ct.iNamLamViec = ctct.iNamLamViec AND (@iLoaiNNS = 0 OR ct.iLoaiNguonNganSach = @iLoaiNNS)
	WHERE ctct.iNamLamViec = @NamLamViec
		AND ctct.iNamNganSach = @NamNganSach
		AND ctct.iID_MaNguonNganSach = @NguonNganSach
		AND ctct.iID_MaDonVi in (SELECT * FROM f_split(@idDV))
		AND ctct.iID_MaDonVi not in (SELECT * FROM f_split(@DonViBanThan))
		AND (ctct.iLoai = 4 or ctct.iLoai = 2)
		AND ctct.iLoaiChungTu = @LoaiChungTu
	GROUP BY ctct.sKyHieu
	) ctc ON ml.sKyHieu = ctc.sKyHieu AND ml.iNamLamViec = @NamLamViec
	LEFT JOIN 
	(
	SELECT ctct.sKyHieu, sum(isnull(ctct.fTuChi, 0)) fTuChi FROM NS_SKT_ChungTuChiTiet ctct
	JOIN NS_SKT_ChungTu ct ON ctct.iID_CTSoKiemTra = ct.iID_CTSoKiemTra AND ct.iNamLamViec = ctct.iNamLamViec AND (@iLoaiNNS = 0 OR ct.iLoaiNguonNganSach = @iLoaiNNS)
	WHERE ctct.iNamLamViec = @NamLamViec
		AND ctct.iNamNganSach = @NamNganSach
		AND ctct.iID_MaNguonNganSach = @NguonNganSach
		AND ctct.iID_MaDonVi in (SELECT * FROM f_split(@DonViBanThan))
		AND (ctct.iLoai = 4 or ctct.iLoai = 2)
		AND ctct.iLoaiChungTu = @LoaiChungTu
	GROUP BY ctct.sKyHieu
	) banthan ON ml.sKyHieu = banthan.sKyHieu AND ml.iNamLamViec = @NamLamViec

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
		 ml.sL,
		 ml.sK,
		 ml.sNG,
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
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc]    Script Date: 8/22/2024 7:19:25 PM ******/
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
	DECLARE @DonViBanThan NVARCHAR(MAX)
	SET @DonViBanThan = (SELECT SUBSTRING(( SELECT ',' + iID_MaDonVi AS [text()] 
						FROM DonVi pr
						WHERE iNamLamViec = @NamLamViec 
								and iLoai = 1
								and iCapDonVi = (SELECT TOP 1 iCapDonVi FROM DonVi WHERE iNamLamViec = @NamLamViec and iLoai = 0)
						FOR XML PATH (''), TYPE
							).value('text()[1]','nvarchar(max)'), 2, 1000))

	SELECT ml.iID_MLSKTCha IIdMlsktCha,
		   ml.iID_MLSKT IIdMlskt,
		   ml.sM,
		   case when ml.iID_MLSKTCha = '00000000-0000-0000-0000-000000000000' then ml.sL
		   else null end sL,
		   ml.sK,
		   ml.sNG,
		   ml.sKyHieu,
		   ml.sSTT,
		   ml.sSTTBC,
		   ml.sMoTa,
		   ml.bHangCha ,
		   --IsNull(sum(ct.sumToTal), 0)/@dvt TongTuChi ,
		   (IsNull(sum(ct.sumDonViBanthan), 0) + IsNull(sum(ct.sumDonVi), 0))/@dvt TongTuChi,
		   IsNull(sum(ct.sumTotalMuaHangHienVat), 0)/@dvt TongMuaHangHienVat ,
		   IsNull(sum(ct.sumTotalDacThu), 0)/@dvt TongDacThu ,
		   IsNull(sum(ct.sumDonVi), 0)/@dvt TongTuChiPB ,
		   IsNull(sum(ct.sumDonViMuaHangHienVat), 0)/@dvt TongMuaHangHienVatPB ,
		   IsNull(sum(ct.sumDonViDacThu), 0)/@dvt TongDacThuPB,
		   IsNull(sum(ct.sumDonViBanthan), 0)/@dvt TuChiBanThan
	FROM NS_SKT_MucLuc ml
	LEFT JOIN
	  (
	  SELECT ml.iID_MLSKT,
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
			  sumDonViBanthan = 0
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

	   UNION ALL 

	   SELECT ml.iID_MLSKT,
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
			sum(ISNull(banthan.fTuChi, 0)) sumDonViBanthan
	   FROM NS_SKT_MucLuc ml
	   LEFT JOIN 
	   (
		SELECT ctct.sKyHieu, sum(isnull(ctct.fTuChi, 0)) fTuChi, sum(isnull(ctct.fMuaHangCapHienVat, 0)) fMuaHangCapHienVat, sum(isnull(ctct.fPhanCap, 0)) fPhanCap FROM NS_SKT_ChungTuChiTiet ctct
		JOIN NS_SKT_ChungTu ct ON ctct.iID_CTSoKiemTra = ct.iID_CTSoKiemTra AND ct.iNamLamViec = ctct.iNamLamViec AND (@iLoaiNNS = 0 OR ct.iLoaiNguonNganSach = @iLoaiNNS)
		WHERE ctct.iNamLamViec = @NamLamViec
		   AND ctct.iNamNganSach = @NamNganSach
		   AND ctct.iID_MaNguonNganSach = @NguonNganSach
		   AND ctct.iID_MaDonVi in (SELECT * FROM f_split(@idDV))
		   AND ctct.iID_MaDonVi not in (SELECT * FROM f_split(@DonViBanThan))
		   AND (ctct.iLoai = 4 or ctct.iLoai = 2)
		   AND ctct.iLoaiChungTu = @LoaiChungTu
		GROUP BY ctct.sKyHieu
	   ) ctc ON ml.sKyHieu = ctc.sKyHieu AND ml.iNamLamViec = @NamLamViec
	   LEFT JOIN 
	   (
		SELECT ctct.sKyHieu, sum(isnull(ctct.fTuChi, 0)) fTuChi FROM NS_SKT_ChungTuChiTiet ctct
		JOIN NS_SKT_ChungTu ct ON ctct.iID_CTSoKiemTra = ct.iID_CTSoKiemTra AND ct.iNamLamViec = ctct.iNamLamViec AND (@iLoaiNNS = 0 OR ct.iLoaiNguonNganSach = @iLoaiNNS)
		WHERE ctct.iNamLamViec = @NamLamViec
		   AND ctct.iNamNganSach = @NamNganSach
		   AND ctct.iID_MaNguonNganSach = @NguonNganSach
		   AND ctct.iID_MaDonVi in (SELECT * FROM f_split(@DonViBanThan))
		   AND (ctct.iLoai = 4 or ctct.iLoai = 2)
		   AND ctct.iLoaiChungTu = @LoaiChungTu
		GROUP BY ctct.sKyHieu
	   ) banthan ON ml.sKyHieu = banthan.sKyHieu AND ml.iNamLamViec = @NamLamViec
	   
	   --WHERE (ctc.iLoai = 4 or ctc.iLoai = 2) --WHERE ctc.iLoai = 4 
		 --AND ctc.iLoaiChungTu = @LoaiChungTu
		 --AND iID_MaDonVi in
		 --  (SELECT *
			--FROM f_split(@idDV))
		 --AND exists
		 --  (SELECT iID_CTSoKiemTra
			--FROM NS_SKT_ChungTu ct
			--WHERE ct.iID_CTSoKiemTra = ctc.iID_CTSoKiemTra)
		 
	   GROUP BY ml.iID_MLSKT,
				ml.sKyHieu,
				ml.sSTT,
				ml.sSTTBC,
				ml.sMoTa
		) AS ct ON ml.sKyHieu = ct.sKyHieu
	WHERE 
	ml.iNamLamViec = @NamLamViec
	AND ml.iTrangThai = 1
	GROUP BY ml.iID_MLSKTCha,
			 ml.iID_MLSKT,
			 ml.sM,
			 ml.sL,
			 ml.sK,
			 ml.sNG,
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
;
;
GO
