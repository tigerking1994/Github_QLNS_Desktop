GO
DELETE FROM DM_ChuKy WHERE Id_Code = 'rptNS_PhuongAn_PhanBo_SoKiemTra'
GO
INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) 
VALUES (NEWID(), 1, N'11016', NULL, N'11018', NULL, N'11015', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rptNS_PhuongAn_PhanBo_SoKiemTra', NULL, N'rptNS_PhuongAn_PhanBo_SoKiemTra', NULL, NULL, NULL, NULL, NULL, N'SO_KIEM_TRA_PHAN_BO', NULL, N'Báo cáo tổng hợp phân bổ số kiểm tra', N'Trưởng Phòng Tài Chính', NULL, N'Trưởng Phòng Tài Chính', NULL, N'Trưởng Phòng Tài Chính', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'11018', NULL, N'11020', NULL, N'11015', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', N'PHƯƠNG ÁN PHÂN BỔ SỐ KIỂM TRA NGÂN SÁCH NHÀ NƯỚC CHI THƯỜNG XUYÊN CHO QUỐC PHÒNG NĂM 2024', NULL, N'(KHÔNG BAO GỒM NỘI DUNG CHI ĐẦU TƯ XÂY DỰNG CƠ BẢN)', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

/****** Object:  StoredProcedure [dbo].[sp_skt_get_don_vi_cap_2_khac_don_vi_cha]    Script Date: 8/6/2024 8:50:38 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_get_don_vi_cap_2_khac_don_vi_cha]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_get_don_vi_cap_2_khac_don_vi_cha]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc]    Script Date: 8/6/2024 8:50:38 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra]    Script Date: 8/6/2024 8:50:38 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra]    Script Date: 8/6/2024 8:50:38 AM ******/
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
	   ml.sL,
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
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc]    Script Date: 8/6/2024 8:50:38 AM ******/
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
		   ml.sL,
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
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_get_don_vi_cap_2_khac_don_vi_cha]    Script Date: 8/6/2024 8:50:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_skt_get_don_vi_cap_2_khac_don_vi_cha] 
	@NamLamViec int
AS
BEGIN
	SELECT pr.iID_MaDonVi IIDMaDonVi, pr.sTenDonVi TenDonVi
	FROM DonVi pr
	WHERE iNamLamViec = @NamLamViec 
		and iLoai = 1
		and iCapDonVi = (SELECT TOP 1 iCapDonVi FROM DonVi WHERE iNamLamViec = @NamLamViec and iLoai = 0)
END
GO
