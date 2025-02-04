/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_thang_dulieu_insert]    Script Date: 12/05/2023 4:25:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_bangluong_thang_dulieu_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_bangluong_thang_dulieu_insert]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_get_can_cu_du_toan_so_nhu_cau]    Script Date: 12/05/2023 4:25:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_get_can_cu_du_toan_so_nhu_cau]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_get_can_cu_du_toan_so_nhu_cau]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_get_can_cu_du_toan_so_nhu_cau]    Script Date: 12/05/2023 4:25:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_skt_get_can_cu_du_toan_so_nhu_cau]
	@LstIdChungTu nvarchar(max),
	@LstIdMucLuc nvarchar(max),
	@IdDonVi nvarchar(max),
	@LoaiCanCu nvarchar(max),
	@NamLamViec int
AS
BEGIN
IF @LoaiCanCu = 'BUDGET_ESTIMATE'
SELECT mlns.SXauNoiMa,
       mlns.iID_MLNS IdMlns,
       mlns.iID_MLNS_Cha IdMlnsCha,
       mlns.bHangCha,
       sum(isnull(TuChi,0)) TuChi,
       sum(isnull(HangNhap,0)) HangNhap,
       sum(isnull(HangMua,0)) HangMua,
       sum(isnull(PhanCap,0)) PhanCap,
       sum(isnull(TuChi,0)) MuaHangHienVat,
       sum(isnull(TuChi,0)) DacThu
FROM NS_MucLucNganSach mlns
 JOIN
  (SELECT ctct.SXauNoiMa,
          ctct.iID_MLNS,
          ctct.iID_MLNS_Cha,
          ctct.bHangCha,
          sum(fTuChi) TuChi,
          sum(fHangNhap) HangNhap,
          sum(fHangMua) HangMua,
          sum(fPhanCap) PhanCap,
          sum(fTuChi) MuaHangHienVat,
          sum(fTuChi) DacThu
   FROM NS_DT_ChungTuChiTiet ctct
   JOIN NS_DT_ChungTu ct ON ct.iID_DTChungTu = ctct.iID_DTChungTu
   WHERE ct.iID_DTChungTu in (SELECT * FROM f_split(@LstIdChungTu))
     AND (@LstIdMucLuc = '-1'
          OR ctct.sLNS in ('1040100',
                           '1040200',
                           '1040300'))
	AND ctct.iID_MaDonVi in (select * from f_split(@IdDonVi))
   GROUP BY ctct.SXauNoiMa,
            ctct.iID_MLNS,
            ctct.iID_MLNS_Cha,
            ctct.bHangCha) ctct ON mlns.iID_MLNS = ctct.iID_MLNS AND mlns.iNamLamViec = @NamLamViec
GROUP BY mlns.SXauNoiMa,
         mlns.iID_MLNS,
         mlns.iID_MLNS_Cha,
         mlns.bHangCha;
IF @LoaiCanCu = 'BUDGET_SETTLEMENT'
SELECT mlns.SXauNoiMa,
       mlns.iID_MLNS IdMlns,
       mlns.iID_MLNS_Cha IdMlnsCha,
       mlns.bHangCha,
       sum(isnull(TuChi,0)) TuChi,
       sum(isnull(HangNhap,0)) HangNhap,
       sum(isnull(HangMua,0)) HangMua,
       sum(isnull(PhanCap,0)) PhanCap,
       sum(isnull(TuChi,0)) MuaHangHienVat,
       sum(isnull(TuChi,0)) DacThu
FROM NS_MucLucNganSach mlns
 JOIN
  (SELECT ctct.SXauNoiMa,
          ctct.iID_MLNS,
          ctct.iID_MLNS_Cha,
          ctct.bHangCha,
          sum(fTuChi_PheDuyet) TuChi,
          sum(fTuChi_PheDuyet) HangNhap,
          sum(fTuChi_PheDuyet) HangMua,
          sum(fTuChi_PheDuyet) PhanCap,
          sum(fTuChi_PheDuyet) MuaHangHienVat,
          sum(fTuChi_PheDuyet) DacThu
   FROM NS_QT_ChungTuChiTiet ctct
   JOIN NS_QT_ChungTu ct ON ct.iID_QTChungTu = ctct.iID_QTChungTu
   WHERE ct.iID_QTChungTu in (SELECT * FROM f_split(@LstIdChungTu))
     AND (@LstIdMucLuc = '-1'
          OR ctct.sLNS in ('1040200',
                           '1040300'))
   GROUP BY ctct.SXauNoiMa,
            ctct.iID_MLNS,
            ctct.iID_MLNS_Cha,
            ctct.bHangCha) ctct ON mlns.iID_MLNS = ctct.iID_MLNS AND mlns.iNamLamViec = @NamLamViec
GROUP BY mlns.SXauNoiMa,
         mlns.iID_MLNS,
         mlns.iID_MLNS_Cha,
         mlns.bHangCha;
IF @LoaiCanCu = 'BUDGET_ALLOCATION'
SELECT mlns.SXauNoiMa,
       mlns.iID_MLNS IdMlns,
       mlns.iID_MLNS_Cha IdMlnsCha,
       mlns.bHangCha,
       sum(isnull(TuChi,0)) TuChi,
       sum(isnull(HangNhap,0)) HangNhap,
       sum(isnull(HangMua,0)) HangMua,
       sum(isnull(PhanCap,0)) PhanCap,
       sum(isnull(TuChi,0)) MuaHangHienVat,
       sum(isnull(TuChi,0)) DacThu
FROM NS_MucLucNganSach mlns
 JOIN
  (SELECT ctct.SXauNoiMa,
          ctct.iID_MLNS,
          ctct.iID_MLNS_Cha,
          ctct.bHangCha,
          sum(fTuChi) TuChi,
          sum(fTuChi) HangNhap,
          sum(fTuChi) HangMua,
          sum(fTuChi) PhanCap,
          sum(fTuChi) MuaHangHienVat,
          sum(fTuChi) DacThu
   FROM NS_CP_ChungTuChiTiet ctct
   JOIN NS_CP_ChungTu ct ON ct.iID_CTCapPhat = ctct.iID_CTCapPhat
   WHERE ct.iID_CTCapPhat in (SELECT * FROM f_split(@LstIdChungTu))
     AND (@LstIdMucLuc = '-1'
          OR ctct.sLNS in ('1040200',
                           '1040300'))
   GROUP BY ctct.SXauNoiMa,
            ctct.iID_MLNS,
            ctct.iID_MLNS_Cha,
            ctct.bHangCha) ctct ON mlns.iID_MLNS = ctct.iID_MLNS AND mlns.iNamLamViec = @NamLamViec
GROUP BY mlns.SXauNoiMa,
         mlns.iID_MLNS,
         mlns.iID_MLNS_Cha,
         mlns.bHangCha;
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_thang_dulieu_insert]    Script Date: 12/05/2023 4:25:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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


	SELECT
	MA_CBO MaCanBo,
	(CASE WHEN pc.Parent = 'TIENAN' THEN 'TA_TT'
	WHEN pc.Parent = 'TIENAN2' THEN 'TA_TT2' ELSE '' END) AS PARENT,
	SUM (
		--CASE WHEN PARENT <> N'TIENAN' THEN 0
		--WHEN canBoPhuCap.MA_PHUCAP IN ('TA_BB_DG', 'TA_TRUCTRAI_DG') THEN ISNULL(@SoNgay, 0) * canBoPhuCap.GIA_TRI
		--ELSE ISNULL(canBoPhuCap.HuongPC_SN, 0) * canBoPhuCap.GIA_TRI
		--END
		CASE WHEN PARENT <> N'TIENAN' THEN 0
		WHEN ISNULL(canBoPhuCap.HuongPC_SN, 0) <> 0 AND bHuongPc_Sn = 1 THEN ISNULL(canBoPhuCap.HuongPC_SN, 0) * canBoPhuCap.GIA_TRI
		ELSE ISNULL(@SoNgay, 0) * canBoPhuCap.GIA_TRI
		END
	) GiaTriTA1,
	SUM (
		--CASE WHEN PARENT <> N'TIENAN2' THEN 0
		--WHEN canBoPhuCap.MA_PHUCAP IN ('TA_BB_DG', 'TA_TRUCTRAI_DG') THEN ISNULL(@SoNgay, 0) * canBoPhuCap.GIA_TRI
		--ELSE ISNULL(canBoPhuCap.HuongPC_SN, 0) * canBoPhuCap.GIA_TRI
		--END
		CASE WHEN PARENT <> N'TIENAN2' THEN 0
		WHEN ISNULL(canBoPhuCap.HuongPC_SN, 0) <> 0 AND bHuongPc_Sn = 1 THEN ISNULL(canBoPhuCap.HuongPC_SN, 0) * canBoPhuCap.GIA_TRI
		ELSE ISNULL(@SoNgay, 0) * canBoPhuCap.GIA_TRI
		END
	) GiaTriTA2
	INTO #SoLieuTienAn
	FROM TL_CanBo_PhuCap canBoPhuCap
	INNER JOIN TL_DM_PhuCap as pc on canBoPhuCap.MA_PHUCAP = pc.Ma_PhuCap
	--WHERE
	--canBoPhuCap.MA_PHUCAP IN ('TA_TRUCQY_DG1', 'TA_TRUCQY_DG2', 'TA_TRUCQY_DG3', 'TA_TRUCQY_DG4', 'TA_DOCHAI_DG', 'TA_OM_DG', 'TA_BB_DG', 'TA_TRUCTRAI_DG')
	WHERE
	pc.PARENT IN ('TIENAN', 'TIENAN2') and canBoPhuCap.Gia_Tri <> 0
	GROUP BY canBoPhuCap.MA_CBO, pc.PARENT


	SELECT
	canBo.Ma_CanBo AS MaCanBo,
	canBo.Ten_CanBo AS TenCanBo,
	donVi.Ma_DonVi MaDonVi,
	canBo.Ma_Hieu_CanBo AS MaHieuCanBo,
	capBac.Ma_Cb MaCapBac,
	canBo.BHTN,
	canBo.PCCV,
	canBo.BNuocNgoai,
	canBo.Ngay_XN as NgayXn
	INTO #ThongTinCanBo
	FROM TL_DM_CanBo canBo
	INNER JOIN TL_DM_DonVi donVi
	ON canBo.Parent = donVi.Ma_DonVi
	INNER JOIN TL_DM_CapBac capBac
	ON canBo.Ma_CB = capBac.Ma_Cb
	WHERE
	canBo.Thang = @Thang
	AND canBo.Nam = @Nam
	AND canBo.Parent IN (SELECT * FROM f_split(@MaDonVi))
	AND (canBo.IsDelete = 1 or (canbo.Ma_TangGiam = '320' and month(canbo.Ngay_XN) <= @thang and year(canbo.Ngay_XN) = @Nam))
	AND canBo.Khong_Luong <> 1


SELECT
	newid() AS Id,
	@Thang AS Thang,
	@Nam AS Nam,
	canBo.MaCanBo AS MaCbo,
	canBo.TenCanBo AS TenCbo,
	canBo.MaDonVi AS MaDonVi,
	canBo.BNuocNgoai ,
	@MaCachTl AS MaCachTl,
	canBoPhuCap.MA_PHUCAP AS MaPhuCap,
	CASE
	WHEN canBoPhuCap.MA_PHUCAP = 'TCVIECLAM_TT' AND cb.Parent in ('1', '2', '3') THEN 0
	WHEN canBoPhuCap.MA_PHUCAP = 'THANG_TCVIECLAM' AND canbo.NgayXn is null THEN 0
	WHEN canBoPhuCap.MA_PHUCAP = 'THANG_TCVIECLAM' and canbo.NgayXn is not null and year(canbo.NgayXn) <> @NAM THEN 0
	WHEN canBoPhuCap.MA_PHUCAP = 'THANG_TCVIECLAM' and canbo.NgayXn is not null and year(canbo.NgayXn) = @NAM and month(canbo.NgayXn) <> @Thang THEN 0
	WHEN canBoPhuCap.MA_PHUCAP = 'THANG_TCXN' AND canbo.NgayXn is null THEN 0
	WHEN canBoPhuCap.MA_PHUCAP = 'THANG_TCXN' and canbo.NgayXn is not null and year(canbo.NgayXn) <> @NAM THEN 0
	WHEN canBoPhuCap.MA_PHUCAP = 'THANG_TCXN' and canbo.NgayXn is not null and year(canbo.NgayXn) = @NAM and month(canbo.NgayXn) <> @Thang THEN 0
	WHEN (canBoPhuCap.MA_PHUCAP = 'NTN' AND canBoPhuCap.GIA_TRI < 5) OR (canBoPhuCap.MA_PHUCAP = 'BHTNCN_HS' AND canBo.BHTN = 0) OR (canBoPhuCap.MA_PHUCAP = 'PCCOV_HS' AND canBo.PCCV = 0) THEN 0
	WHEN canBoPhuCap.MA_PHUCAP = 'TA_TT' THEN soLieuTienAn.GiaTriTA1
	WHEN canBoPhuCap.MA_PHUCAP = 'TA_TT2' THEN soLieuTienAn.GiaTriTA2
	WHEN canBoPhuCap.MA_PHUCAP = 'TRICHLUONG_TT' THEN (canBoPhuCap.GIA_TRI / 1000) * 1000
	ELSE canBoPhuCap.GIA_TRI
	END AS GiaTri,
	canBo.MaHieuCanBo AS MaHieuCanBo,
	canBo.MaCapBac AS MaCb,
	canBoPhuCap.HuongPC_SN AS HuongPcSn,
	cachTinhLuong.CongThuc AS CongThuc,
	CASE WHEN cachTinhLuong.Id IS NULL THEN 1 ELSE 0 END AS IsCalculated,
	canBoPhuCap.bCapNhat as IsCapNhat
FROM TL_CanBo_PhuCap canBoPhuCap
	INNER JOIN #ThongTinCanBo canBo
	ON canBo.MaCanBo = canBoPhuCap.MA_CBO
	LEFT JOIN #SoLieuTienAn soLieuTienAn
	ON canBoPhuCap.MA_CBO = soLieuTienAn.MaCanBo AND canBoPhuCap.MA_PHUCAP = soLieuTienAn.Parent
	LEFT JOIN TL_DM_Cach_TinhLuong_Chuan cachTinhLuong
	ON cachTinhLuong.Ma_Cot = canBoPhuCap.MA_PHUCAP
	left join TL_DM_CapBac cb on canBo.MaCapBac = cb.Ma_Cb

	DELETE blt
	FROM TL_BangLuong_Thang blt
	INNER JOIN #ThongTinCanBo ttcb ON blt.Ma_CBo = ttcb.MaCanBo
	INNER JOIN TL_CanBo_PhuCap cbpc ON cbpc.MA_CBO = blt.Ma_CBo
	WHERE bCapNhat = 1

	UPDATE cbpc
	SET bCapNhat = 0
	FROM TL_CanBo_PhuCap cbpc
	INNER JOIN #ThongTinCanBo ttcb ON cbpc.MA_CBO = ttcb.MaCanBo

END
;
;
;
;
GO
