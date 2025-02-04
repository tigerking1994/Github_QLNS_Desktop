/****** Object:  StoredProcedure [dbo].[sp_khc_cd_bhxh_nhap_so_nhu_cau]    Script Date: 11/1/2024 6:09:37 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_khc_cd_bhxh_nhap_so_nhu_cau]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_khc_cd_bhxh_nhap_so_nhu_cau]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_so_sanh_nam_truoc_nam_nay]    Script Date: 11/1/2024 6:11:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_so_sanh_nam_truoc_nam_nay]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_so_sanh_nam_truoc_nam_nay]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc]    Script Date: 10/31/2024 2:16:50 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra]    Script Date: 10/31/2024 2:16:50 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra]    Script Date: 10/31/2024 2:16:50 PM ******/
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
	   case when ml.iID_MLSKTCha = '00000000-0000-0000-0000-000000000000' or ml.iID_MLSKTCha is null then ml.sL
		   else null end sL,
	   ml.sK,
	   ml.sNG,
	   ml.sKyHieu,
       ml.sSTT,
       ml.sSTTBC,
       ml.sMoTa,
       ml.bHangCha ,
        IsNull(sum(ct.sumToTal), 0) / @dvt SoKiemTraDuocThongBao,
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
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc]    Script Date: 10/31/2024 2:16:50 PM ******/
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
		   case when ml.iID_MLSKTCha = '00000000-0000-0000-0000-000000000000' or ml.iID_MLSKTCha is null then ml.sL
		   else null end sL,
		   ml.sK,
		   ml.sNG,
		   ml.sKyHieu,
		   ml.sSTT,
		   ml.sSTTBC,
		   ml.sMoTa,
		   ml.bHangCha ,
		   IsNull(sum(ct.sumToTal), 0) / @dvt SoKiemTraDuocThongBao,
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
;
;
GO

/****** Object:  StoredProcedure [dbo].[sp_skt_so_sanh_nam_truoc_nam_nay]    Script Date: 11/1/2024 6:11:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_skt_so_sanh_nam_truoc_nam_nay]
	@Loai nvarchar(max),
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@LoaiChungTu int,
	@IdDonVi nvarchar(max),
	@NganSach nvarchar(5),
	@UserName nvarchar(100),
	@LoaiBaoCao int,
	@KieuBaoCao int,
	@DonViTinh int
AS
BEGIN

	select 
	mucluc.sKyHieu,
	mucluc.iID_MLSKT,
	mucluc.iID_MLSKTCha,
	mucluc.SL sL,
	mucluc.SK sK,
	mucluc.sM,
	mucluc.sNG,
	mucluc.sMoTa MoTa,
	mucluc.bHangCha,
	case when @LoaiBaoCao = 1 then sum(chitiet_before.fTuChi) / @DonViTinh
	when @LoaiBaoCao = 2 then sum(chitiet_before.fMuaHangCapHienVat) / @DonViTinh
	else sum(chitiet_before.fPhanCap) / @DonViTinh end soLieuCot1,

	case when @LoaiBaoCao = 1 then sum(chitiet_now.fTuChi) / @DonViTinh
	when @LoaiBaoCao = 2 then sum(chitiet_now.fMuaHangCapHienVat) / @DonViTinh
	else sum(chitiet_now.fPhanCap) / @DonViTinh end soLieuCot2,

	sum(chitiet_before.fTuChi) fTuChiNamTruoc,
	sum(chitiet_now.fTuChi) fTuChiNamNay,
	sum(chitiet_before.fMuaHangCapHienVat) fMuaHangCapHienVatNamTruoc,
	sum(chitiet_now.fMuaHangCapHienVat) fMuaHangCapHienVatNamNay,
	sum(chitiet_before.fPhanCap) fDacThuNamTruoc,
	sum(chitiet_now.fPhanCap) fDacThuNamNay
	into #tempTable1
	from ns_skt_mucluc mucluc
	left join 
	(select ctct.*, donvi.sTenDonVi sTenDonVi1 from NS_SKT_ChungTuChiTiet ctct 
	join NS_SKT_ChungTu ct on ctct.iID_CTSoKiemTra = ct.iID_CTSoKiemTra and ctct.iNamLamViec = ct.iNamLamViec
	join DonVi donvi on ctct.iID_MaDonVi = donvi.iID_MaDonVi and ct.iNamLamViec = donvi.iNamLamViec
	join NguoiDung_DonVi nguoidung on nguoidung.iID_MaDonVi = donvi.iID_MaDonVi and nguoidung.iNamLamViec = donvi.iNamLamViec
	where ctct.iNamLamViec = @NamLamViec - 1
	and donvi.iTrangThai = 1
	and donvi.iID_MaDonVi in (select * from f_split(@IdDonVi))

	and ctct.iNamLamViec = @NamLamViec - 1
	and ctct.iNamNganSach = @NamNganSach
	and ctct.iID_MaNguonNganSach = @NguonNganSach
	and ctct.iLoai = @Loai

	and ct.iNamLamViec = @NamLamViec - 1
	and ct.iNamNganSach = @NamNganSach
	and ct.iID_MaNguonNganSach = @NguonNganSach
	and ct.iLoaiChungTu = @LoaiChungTu

	and nguoidung.iID_MaNguoiDung = @UserName
	and nguoidung.iTrangThai = 1

	) chitiet_before on ((@NamLamViec = 2024 and mucluc.sKyHieuCu = chitiet_before.sKyHieu) or (@NamLamViec <> 2024 and mucluc.sKyHieu = chitiet_before.sKyHieu))
	left join 
	(select ctct.*, donvi.sTenDonVi sTenDonVi1 from NS_SKT_ChungTuChiTiet ctct 
	join NS_SKT_ChungTu ct on ctct.iID_CTSoKiemTra = ct.iID_CTSoKiemTra and ctct.iNamLamViec = ct.iNamLamViec
	join DonVi donvi on ctct.iID_MaDonVi = donvi.iID_MaDonVi and ct.iNamLamViec = donvi.iNamLamViec
	join NguoiDung_DonVi nguoidung on nguoidung.iID_MaDonVi = donvi.iID_MaDonVi and nguoidung.iNamLamViec = donvi.iNamLamViec
	where ctct.iNamLamViec = @NamLamViec
	and donvi.iTrangThai = 1
	and donvi.iID_MaDonVi in (select * from f_split(@IdDonVi))

	and ctct.iNamLamViec = @NamLamViec
	and ctct.iNamNganSach = @NamNganSach
	and ctct.iID_MaNguonNganSach = @NguonNganSach
	and ctct.iLoai = @Loai

	and ct.iNamLamViec = @NamLamViec
	and ct.iNamNganSach = @NamNganSach
	and ct.iID_MaNguonNganSach = @NguonNganSach
	and ct.iLoaiChungTu = @LoaiChungTu

	) chitiet_now on mucluc.sKyHieu = chitiet_now.sKyHieu and chitiet_before.iID_MaDonVi = chitiet_now.iID_MaDonVi

	--where @LoaiNhap in (select * from f_split(mucluc.sLoaiNhap))
	where mucluc.iNamLamViec = @NamLamViec

	group by mucluc.sKyHieu,
	mucluc.iID_MLSKT,
	mucluc.iID_MLSKTCha,
	mucluc.SL,
	mucluc.SK,
	mucluc.sM,
	mucluc.sNG,
	mucluc.sMoTa,
	mucluc.bHangCha
	order by mucluc.sKyHieu

	select 
	mucluc.sKyHieu,
	mucluc.iID_MLSKT,
	mucluc.iID_MLSKTCha,
	mucluc.SL sL,
	mucluc.SK sK,
	'' sM,
	'' sNG,
	--mucluc.sM,
	--mucluc.sNG,
	case when isnull(chitiet_now.sTenDonVi1, '') = '' and isnull(chitiet_before.sTenDonVi1, '') = '' then mucluc.sMoTa
	when isnull(chitiet_now.sTenDonVi1, '') = '' then concat('   + ', chitiet_before.sTenDonVi1)
	else concat('   + ', chitiet_now.sTenDonVi1)
	end as MoTa,
	mucluc.bHangCha,
	case when @LoaiBaoCao = 1 then sum(chitiet_before.fTuChi) / @DonViTinh
	when @LoaiBaoCao = 2 then sum(chitiet_before.fMuaHangCapHienVat) / @DonViTinh
	else sum(chitiet_before.fPhanCap) / @DonViTinh end soLieuCot1,

	case when @LoaiBaoCao = 1 then sum(chitiet_now.fTuChi) / @DonViTinh
	when @LoaiBaoCao = 2 then sum(chitiet_now.fMuaHangCapHienVat) / @DonViTinh
	else sum(chitiet_now.fPhanCap) / @DonViTinh end soLieuCot2,

	sum(chitiet_before.fTuChi) fTuChiNamTruoc,
	sum(chitiet_now.fTuChi) fTuChiNamNay,
	sum(chitiet_before.fMuaHangCapHienVat) fMuaHangCapHienVatNamTruoc,
	sum(chitiet_now.fMuaHangCapHienVat) fMuaHangCapHienVatNamNay,
	sum(chitiet_before.fPhanCap) fDacThuNamTruoc,
	sum(chitiet_now.fPhanCap) fDacThuNamNay
	into #tempTable2
	from ns_skt_mucluc mucluc
	left join 
	(select ctct.*, donvi.sTenDonVi sTenDonVi1 from NS_SKT_ChungTuChiTiet ctct 
	join NS_SKT_ChungTu ct on ctct.iID_CTSoKiemTra = ct.iID_CTSoKiemTra and ctct.iNamLamViec = ct.iNamLamViec
	join DonVi donvi on ctct.iID_MaDonVi = donvi.iID_MaDonVi and ct.iNamLamViec = donvi.iNamLamViec
	join NguoiDung_DonVi nguoidung on nguoidung.iID_MaDonVi = donvi.iID_MaDonVi and nguoidung.iNamLamViec = donvi.iNamLamViec
	where ctct.iNamLamViec = @NamLamViec - 1
	and donvi.iTrangThai = 1
	and donvi.iID_MaDonVi in (select * from f_split(@IdDonVi))

	and ctct.iNamLamViec = @NamLamViec - 1
	and ctct.iNamNganSach = @NamNganSach
	and ctct.iID_MaNguonNganSach = @NguonNganSach
	and ctct.iLoai = @Loai

	and ct.iNamLamViec = @NamLamViec - 1
	and ct.iNamNganSach = @NamNganSach
	and ct.iID_MaNguonNganSach = @NguonNganSach
	and ct.iLoaiChungTu = @LoaiChungTu

	and nguoidung.iID_MaNguoiDung = 'admin'
	and nguoidung.iTrangThai = 1

	) chitiet_before on ((@NamLamViec = 2024 and mucluc.sKyHieuCu = chitiet_before.sKyHieu) or (@NamLamViec <> 2024 and mucluc.sKyHieu = chitiet_before.sKyHieu))
	left join 
	(select ctct.*, donvi.sTenDonVi sTenDonVi1 from NS_SKT_ChungTuChiTiet ctct 
	join NS_SKT_ChungTu ct on ctct.iID_CTSoKiemTra = ct.iID_CTSoKiemTra and ctct.iNamLamViec = ct.iNamLamViec
	join DonVi donvi on ctct.iID_MaDonVi = donvi.iID_MaDonVi and ct.iNamLamViec = donvi.iNamLamViec
	join NguoiDung_DonVi nguoidung on nguoidung.iID_MaDonVi = donvi.iID_MaDonVi and nguoidung.iNamLamViec = donvi.iNamLamViec
	where ctct.iNamLamViec = @NamLamViec
	and donvi.iTrangThai = 1
	and donvi.iID_MaDonVi in (select * from f_split(@IdDonVi))

	and ctct.iNamLamViec = @NamLamViec
	and ctct.iNamNganSach = @NamNganSach
	and ctct.iID_MaNguonNganSach = @NguonNganSach
	and ctct.iLoai = @Loai

	and ct.iNamLamViec = @NamLamViec
	and ct.iNamNganSach = @NamNganSach
	and ct.iID_MaNguonNganSach = @NguonNganSach
	and ct.iLoaiChungTu = @LoaiChungTu

	) chitiet_now on mucluc.sKyHieu = chitiet_now.sKyHieu and chitiet_before.iID_MaDonVi = chitiet_now.iID_MaDonVi

	--where @LoaiNhap in (select * from f_split(mucluc.sLoaiNhap))
	where mucluc.iNamLamViec = @NamLamViec

	group by mucluc.sKyHieu,
	mucluc.iID_MLSKT,
	mucluc.iID_MLSKTCha,
	mucluc.SL,
	mucluc.SK,
	mucluc.sM,
	mucluc.sNG,
	mucluc.sMoTa,
	mucluc.bHangCha,
	chitiet_now.iID_MaDonVi,
	chitiet_now.sTenDonVi1,
	chitiet_before.sTenDonVi1
	order by mucluc.sKyHieu

	if (@KieuBaoCao = 1)
	select * from #tempTable1
	else
	select * from #tempTable1
	union all
	select * from #tempTable2
	order by sKyHieu, sNG desc, MoTa

END
;
;
;
;
;
;
;
GO
