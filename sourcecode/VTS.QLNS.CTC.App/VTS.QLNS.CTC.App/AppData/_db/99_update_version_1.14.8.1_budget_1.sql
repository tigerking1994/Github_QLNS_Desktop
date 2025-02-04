GO
IF EXISTS (SELECT * FROM [dbo].[DM_ChuKy] WHERE Id_Type = 'rptNS_DuToan_ChiTiet_DonVi')
UPDATE [dbo].[DM_ChuKy] SET TieuDe1_MoTa = N'GIAO DỰ TOÁN CHI NGÂN SÁCH NHÀ NƯỚC NĂM 2024',
TieuDe2_MoTa = N'(Kèm theo Quyết định số: .................../QĐ- ..............ngày ..............tháng............năm 2024 của .............................)',
TieuDe3_MoTa = NULL
WHERE Id_Type = 'rptNS_DuToan_ChiTiet_DonVi'
ELSE
INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) 
VALUES (NEWID(), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rptNS_DuToan_ChiTiet_DonVi', NULL, N'rptNS_DuToan_ChiTiet_DonVi', NULL, NULL, NULL, NULL, NULL, N'DU_TOAN_NHAN_PHAN_BO', NULL, N'Thông báo phân bổ dự toán - Đơn vị', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', N'GIAO DỰ TOÁN CHI NGÂN SÁCH NHÀ NƯỚC NĂM 2024', N'2', N'(Kèm theo Quyết định số: .................../QĐ- ..............ngày ..............tháng............năm 2024 của ...................)', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

/****** Object:  StoredProcedure [dbo].[sp_dutoan_rpt_inchitieu_donvi]    Script Date: 9/17/2024 10:18:26 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dutoan_rpt_inchitieu_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dutoan_rpt_inchitieu_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_dutoan_rpt_inchitieu_donvi]    Script Date: 9/17/2024 10:18:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dutoan_rpt_inchitieu_donvi]				
	 @NamLamViec int,							
	 @NguonNganSach nvarchar(50),
	 @NamNganSach nvarchar(50),
	 @IdDonvi nvarchar(2000),
	 @IdChungTu nvarchar(4000),
	 @NgayQuyetDinh datetime,
	 @Dvt int,
	 @IsLuyKe int
AS	 
BEGIN 
	SET NOCOUNT ON;
	 
SELECT LNS1 = Left(mlns.sLNS, 1),
       LNS3 = Left(mlns.sLNS, 3),
       LNS5 = Left(mlns.sLNS, 5),
       mlns.sLNS AS LNS,
       mlns.sL AS L,
       mlns.sK AS K,
       mlns.sM AS M,
       mlns.sTM AS TM,
       mlns.sTTM AS TTM,
       mlns.sNG AS NG,
       mlns.sTNG AS TNG,
	   mlns.sTNG1 AS TNG1,
	   mlns.sTNG2 AS TNG2,
	   mlns.sTNG3 AS TNG3,
       mlns.sXauNoiMa AS XauNoiMa,
       mlns.sMoTa AS MoTa ,
	   ct.iID_MaDonVi as MaDonVi,
	   '' as TenDonVi,
	   mlns.iID_MLNS as MlnsId,
	   mlns.iID_MLNS_Cha as MlnsIdParent,
       TuChi = sum(fTuChi)/@Dvt ,
       HienVat = sum(fHienVat)/@Dvt,
	   DuPhong = sum(fDuPhong)/@Dvt,
	   HangNhap = sum(fHangNhap)/@Dvt,
	   HangMua = sum(fHangMua)/@Dvt,
	   PhanCap = sum(fPhanCap)/@Dvt,
	   RutKBNN = sum(fRutKBNN)/@Dvt
	FROM NS_DT_ChungTuChiTiet ct
		INNER JOIN NS_MucLucNganSach mlns ON ct.sXauNoiMa = mlns.sXauNoiMa
		AND (iID_DTChungTu in (select * from f_split(@IdChungTu)))
		AND mlns.iNamLamViec = @NamLamViec
	WHERE (@IdDonvi IS NULL
       OR ct.iID_MaDonVi in
         (SELECT *
          FROM f_split(@IdDonvi)))
GROUP BY mlns.sLNS,
         mlns.sL,
         mlns.sK,
         mlns.sM,
         mlns.sTM,
         mlns.sTTM,
         mlns.sNG,
         mlns.sTNG,
		 mlns.sTNG1,
	     mlns.sTNG2,
	     mlns.sTNG3,
         mlns.sXauNoiMa,
         mlns.sMoTa,
		 mlns.iID_MLNS,
		 mlns.iID_MLNS_Cha,
		 ct.iID_MaDonVi
HAVING sum(fTuChi) <> 0
OR sum(fHienVat) <> 0
OR sum(fDuPhong) <> 0
OR sum(fHangNhap) <> 0
OR sum(fHangMua) <> 0
OR sum(fPhanCap) <> 0
OR sum(fRutKBNN) <> 0
END
;
;
;
GO
