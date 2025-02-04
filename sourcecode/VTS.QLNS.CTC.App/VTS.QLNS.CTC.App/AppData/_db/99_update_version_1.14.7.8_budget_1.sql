GO
IF EXISTS (SELECT * FROM [dbo].[DM_ChuKy] WHERE Id_Type = 'rptNS_PhanBo_SoKiemTra_DonVi_NSDTN')
UPDATE [dbo].[DM_ChuKy] SET TieuDe1_MoTa = N'TỔNG HỢP PHÂN BỔ SỐ KIỂM TRA NGÂN SÁCH NHÀ NƯỚC CHI THƯỜNG XUYÊN CHO QUỐC PHÒNG CỦA NGÀNH NGHIỆP VỤ TOÀN QUÂN NĂM 2024',
TieuDe2_MoTa = null
WHERE Id_Type = 'rptNS_PhanBo_SoKiemTra_DonVi_NSDTN'
ELSE
INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) 
VALUES (NEWID(), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rptNS_PhanBo_SoKiemTra_DonVi_NSDTN', NULL, N'rptNS_PhanBo_SoKiemTra_DonVi_NSDTN', NULL, NULL, NULL, NULL, NULL, N'SO_KIEM_TRA_PHAN_BO', NULL, N'Báo cáo chi tiết phân bổ số kiểm tra theo đơn vị', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', N'TỔNG HỢP PHÂN BỔ SỐ KIỂM TRA NGÂN SÁCH NHÀ NƯỚC CHI THƯỜNG XUYÊN CHO QUỐC PHÒNG CỦA NGÀNH NGHIỆP VỤ TOÀN QUÂN NĂM 2024', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

GO
IF EXISTS (SELECT * FROM [dbo].[DM_ChuKy] WHERE Id_Type = 'rptNS_PhanBo_SoKiemTra_DonVi_NSSD')
UPDATE [dbo].[DM_ChuKy] SET TieuDe1_MoTa = N'THÔNG BÁO',
TieuDe2_MoTa = N'SỐ KIỂM TRA NGÂN SÁCH NHÀ NƯỚC CHI THƯỜNG XUYÊN CHO QUỐC PHÒNG NĂM 2024',
TieuDe3_MoTa = N'(Kèm theo số quyết định số ...ngày...tháng... năm...của...)'
WHERE Id_Type = 'rptNS_PhanBo_SoKiemTra_DonVi_NSSD'
ELSE
INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) 
VALUES (NEWID(), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rptNS_PhanBo_SoKiemTra_DonVi_NSSD', NULL, N'rptNS_PhanBo_SoKiemTra_DonVi_NSSD', NULL, NULL, NULL, NULL, NULL, N'SO_KIEM_TRA_PHAN_BO', NULL, N'Báo cáo chi tiết phân bổ số kiểm tra theo đơn vị', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', N'THÔNG BÁO', N'2', N'SỐ KIỂM TRA NGÂN SÁCH NHÀ NƯỚC CHI THƯỜNG XUYÊN CHO QUỐC PHÒNG NĂM 2024', N'(Kèm theo số quyết định số ...ngày...tháng... năm...của...)', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

GO
IF EXISTS (SELECT * FROM [dbo].[DM_ChuKy] WHERE Id_Type = 'rptNS_DuToan_TongHop_DonVi')
UPDATE [dbo].[DM_ChuKy] SET TieuDe1_MoTa = N'PHƯƠNG ÁN PHÂN BỔ DỰ TOÁN CHI NGÂN SÁCH NHÀ NƯỚC NĂM 2024',
TieuDe2_MoTa = null
WHERE Id_Type = 'rptNS_DuToan_TongHop_DonVi'
ELSE
INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) 
VALUES (NEWID(), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rptNS_DuToan_TongHop_DonVi', NULL, N'rptNS_DuToan_TongHop_DonVi', NULL, NULL, NULL, NULL, NULL, N'DU_TOAN_NHAN_PHAN_BO', NULL, N'Thông báo phân bổ dự toán - Đơn vị', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', N'PHƯƠNG ÁN PHÂN BỔ DỰ TOÁN CHI NGÂN SÁCH NHÀ NƯỚC NĂM 2024', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_phan_bo_so_kiem_tra_dv]    Script Date: 9/10/2024 4:13:04 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_phan_bo_so_kiem_tra_dv]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_phan_bo_so_kiem_tra_dv]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_chi_tiet_phan_bo_kiem_tra_nsbd]    Script Date: 9/10/2024 4:13:04 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_chi_tiet_phan_bo_kiem_tra_nsbd]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_chi_tiet_phan_bo_kiem_tra_nsbd]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_chi_tiet_phan_bo_kiem_tra_nsbd]    Script Date: 9/10/2024 4:13:04 PM ******/
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
	   ml.SL,
	   ml.SK,
	   ml.sM,
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
		 ml.SL,
		 ml.SK,
		 ml.sM,
         ml.sNG,
         ml.sMoTa,
         ml.sNG_Cha
		 order by ml.sKyHieu
END
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_phan_bo_so_kiem_tra_dv]    Script Date: 9/10/2024 4:13:04 PM ******/
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
SELECT case when isnull(ml.sNG_Cha, '') = '' and ml.iID_MLSKTCha <> '00000000-0000-0000-0000-000000000000' then null
	   else ml.SL end AS L ,
	   ml.SK AS K ,
	   ml.sM AS M ,
	   ml.sNG AS NG ,
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


GROUP BY case when isnull(ml.sNG_Cha, '') = '' and ml.iID_MLSKTCha <> '00000000-0000-0000-0000-000000000000' then null
	     else ml.SL end,
		 ml.sK , ml.sM , ml.sNG ,
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
;
GO


GO
DELETE FROM DM_ChuKy WHERE Id_Code = 'rptNS_PhuongAn_PhanBo_SoKiemTra_02b'
GO
INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) 
VALUES (NEWID(), 1, N'11016', NULL, N'11018', NULL, N'11015', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rptNS_PhuongAn_PhanBo_SoKiemTra_02b', NULL, N'rptNS_PhuongAn_PhanBo_SoKiemTra_02b', NULL, NULL, NULL, NULL, NULL, N'SO_KIEM_TRA_PHUONG_AN_PHAN_BO', NULL, N'Báo cáo phương án phân bổ số kiểm tra', N'Trưởng Phòng Tài Chính', NULL, N'Trưởng Phòng Tài Chính', NULL, N'Trưởng Phòng Tài Chính', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'11018', NULL, N'11020', NULL, N'11015', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', N'CHI TIẾT PHƯƠNG ÁN PHÂN BỔ NỘI DUNG TĂNG (GIẢM) SỐ KIỂM TRA NGÂN SÁCH NHÀ NƯỚC', NULL, N'CHI THƯỜNG XUYÊN CHO QUỐC PHÒNG CỦA CÁC ĐƠN VỊ TRỰC THUỘC', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO



/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_phuong_an_phan_bo_skt_02b]    Script Date: 8/13/2024 4:51:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_phuong_an_phan_bo_skt_02b]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_phuong_an_phan_bo_skt_02b]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_phuong_an_phan_bo_skt_02b]    Script Date: 8/13/2024 4:51:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_rpt_skt_phuong_an_phan_bo_skt_02b]
	@Nganh varchar(max),
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@Khoi int,
	@DVT int,
	@BTongHop bit,
	@loaiNNS int
AS
BEGIN
	
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DonViTempSKT02B]') AND type in (N'U')) drop table DonViTempSKT02B;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DonViTempSKT02BN1]') AND type in (N'U')) drop table DonViTempSKT02BN1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[skt_n_1]') AND type in (N'U')) drop table skt_n_1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[result_skt_n1]') AND type in (N'U')) drop table result_skt_n1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[skt_n]') AND type in (N'U')) drop table skt_n;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[result_skt_n]') AND type in (N'U')) drop table result_skt_n;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[result_dt]') AND type in (N'U')) drop table result_dt;

	IF (@Khoi = 0)
		BEGIN
			SELECT iID_MaDonVi, sTenDonVi, iLoai INTO DonViTempSKT02B FROM DonVi WHERE iTrangThai = 1 AND iNamLamViec = @NamLamViec
			SELECT iID_MaDonVi, sTenDonVi, iLoai INTO DonViTempSKT02BN1 FROM DonVi WHERE iTrangThai = 1 AND iNamLamViec = @NamLamViec - 1
		END
	ELSE
		BEGIN
			SELECT iID_MaDonVi, sTenDonVi, iLoai INTO DonViTempSKT02B FROM DonVi WHERE iTrangThai = 1 AND iNamLamViec = @NamLamViec AND iKhoi = @Khoi
			SELECT iID_MaDonVi, sTenDonVi, iLoai INTO DonViTempSKT02BN1 FROM DonVi WHERE iTrangThai = 1 AND iNamLamViec = @NamLamViec - 1 AND iKhoi = @Khoi
		END

	-- Data năm hiện hành, năm n-1
	select ctct.iID_MaDonVi, ctct.sTenDonVi, ctct.sKyHieu, ctct.iLoai, ctct.fTuChi
	into skt_n_1
	from NS_SKT_ChungTuChiTiet ctct
	join NS_SKT_ChungTu ct on ctct.iID_CTSoKiemTra = ct.iID_CTSoKiemTra
	join DonViTempSKT02BN1 dt_dv ON ctct.iID_MaDonVi = dt_dv.iID_MaDonVi
	where ctct.iLoaiChungTu = 1
		and ctct.iNamLamViec = @NamLamViec - 1
		and ct.iID_MaNguonNganSach = @NguonNganSach
		and ct.iNamNganSach = @NamNganSach
		and (@loaiNNS = 0 OR ct.iLoaiNguonNganSach = @loaiNNS)
		and ((@BTongHop = 0) OR (@BTongHop = 1 AND ct.bDaTongHop = @BTongHop))

	--Result năm n-1
	select 
	   ml.sL,
	   ml.sK,
	   ml.sM,
	   ml.sNG,
	   ml.sKyHieu,
       ml.sMoTa,
       ml.iID_MLSKT IIdMlskt,
       ml.iID_MLSKTCha IIdMlsktCha,
       ml.sSTT STT,
       ml.sSTTBC SSTTBC,
       ml.bHangCha,
	   skt.iID_MaDonVi IdDonVi,
	   skt.sTenDonVi,
       sum(isnull(skt.fTuChi, 0)) fSoKiemTraNS
	into result_skt_n1
	from NS_SKT_MucLuc ml
	left join skt_n_1 skt on ml.sKyHieu = skt.sKyHieu
	where skt.iLoai = 4 
		and ml.iTrangThai = 1
		and ml.iNamLamViec = @NamLamViec - 1
		and ml.sNG in (SELECT * FROM f_split(@Nganh))
	group by ml.sL,
	   ml.sK,
	   ml.sM,
	   ml.sNG,
       ml.sKyHieu,
       ml.sMoTa,
       ml.iID_MLSKT,
       ml.iID_MLSKTCha,
       ml.sSTT,
       ml.sSTTBC,
       ml.bHangCha,
	   skt.iID_MaDonVi,
	   skt.sTenDonVi

	-- Data dự toán năm n-1
	select map.sSKT_KyHieu,
		dt_ctct.iID_MaDonVi,
		sum(isnull(dt_ctct.fTuChi, 0)) fDuToanDauNam
	into result_dt
	from NS_DT_ChungTuChiTiet dt_ctct
	join NS_MLSKT_MLNS map on dt_ctct.sXauNoiMa = map.sNS_XauNoiMa
	Join NS_DT_ChungTu dt_ct on dt_ctct.iID_DTChungTu = dt_ct.iID_DTChungTu
	where dt_ct.iLoaiDuToan = 1
		and dt_ct.iLoai = 1
		and dt_ctct.iNamLamViec = @NamLamViec - 1
		and map.iNamLamViec = @NamLamViec - 1
		and dt_ct.iid_MaNguonNganSach = @NguonNganSach
		and dt_ct.iNamNganSach = @NamNganSach
	group by map.sSKT_KyHieu, dt_ctct.iID_MaDonVi

	-----------------------------------------------
	-- Data năm kế hoạch, năm n
	select ctct.iID_MaDonVi, ctct.sTenDonVi, ctct.sKyHieu, ctct.fTuChi
	into skt_n
	from NS_SKT_ChungTuChiTiet ctct
	join NS_SKT_ChungTu ct on ctct.iID_CTSoKiemTra = ct.iID_CTSoKiemTra
	join DonViTempSKT02B dt_dv on ctct.iID_MaDonVi = dt_dv.iID_MaDonVi
	where ctct.iLoaiChungTu = 1
		and ctct.iLoai = 4
		and ctct.iNamLamViec = @NamLamViec
		and ct.iID_MaNguonNganSach = @NguonNganSach
		and ct.iNamNganSach = @NamNganSach
		and (@loaiNNS = 0 OR ct.iLoaiNguonNganSach = @loaiNNS)
		and ((@BTongHop = 0) OR (@BTongHop = 1 AND ct.bDaTongHop = @BTongHop))

	--Result năm n
	select 
	   ml.sL,
	   ml.sK,
	   ml.sM,
	   ml.sNG,
       ml.sKyHieuCu,
       ml.sMoTa,
       ml.iID_MLSKT IIdMlskt,
       ml.iID_MLSKTCha IIdMlsktCha,
       ml.sSTT STT,
       ml.sSTTBC SSTTBC,
       ml.bHangCha,
	   n.iID_MaDonVi IdDonVi,
	   n.sTenDonVi,
       sum(isnull(n.fTuChi, 0)) fSoDuKienPB
	into result_skt_n
	from NS_SKT_MucLuc ml
	join skt_n n on ml.sKyHieu = n.sKyHieu
	where ml.iTrangThai = 1
		and ml.iNamLamViec = @NamLamViec
		and ml.sNG in (SELECT * FROM f_split(@Nganh))
	group by ml.sL,
	   ml.sK,
	   ml.sM,
	   ml.sNG,
       ml.sKyHieuCu,
       ml.sMoTa,
       ml.iID_MLSKT,
       ml.iID_MLSKTCha,
       ml.sSTT,
       ml.sSTTBC,
       ml.bHangCha,
	   n.iID_MaDonVi,
	   n.sTenDonVi
	--------------------------------------
	select 
	   n.sL,
	   n.sK,
	   n.sM,
	   n.sNG,
       n.sKyHieuCu sKyHieu,
       n.sMoTa,
       n.IIdMlskt,
       n.IIdMlsktCha,
       n.STT,
       n.SSTTBC,
       n.bHangCha,
	   n.IdDonVi,
	   n.sTenDonVi,
	   n1.fSoKiemTraNS/@DVT fSoKiemTraNS,
	   dt.fDuToanDauNam/@DVT fDuToanDauNam,
       n.fSoDuKienPB/@DVT fSoDuKienPB,
	   case when (isnull(n.fSoDuKienPB, 0) - isnull(n1.fSoKiemTraNS, 0)) > 0 then (isnull(n.fSoDuKienPB, 0) - isnull(n1.fSoKiemTraNS, 0))/@DVT
	   else null end fTang,
	   case when (isnull(n1.fSoKiemTraNS, 0) - isnull(n.fSoDuKienPB, 0)) > 0 then (isnull(n1.fSoKiemTraNS, 0) - isnull(n.fSoDuKienPB, 0))/@DVT
	   else null end fGiam
	from 
	result_skt_n n
	left join result_skt_n1 n1 on n.IdDonVi = n1.IdDonVi and n.sKyHieuCu = n1.sKyHieu
	left join result_dt dt on n.IdDonVi = dt.iID_MaDonVi and n.sKyHieuCu = dt.sSKT_KyHieu
	where n.bHangCha = 0
		and isnull(n.IdDonVi, '') <> '' 
	order by n.sKyHieuCu

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DonViTempSKT02B]') AND type in (N'U')) drop table DonViTempSKT02B;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DonViTempSKT02BN1]') AND type in (N'U')) drop table DonViTempSKT02BN1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[skt_n_1]') AND type in (N'U')) drop table skt_n_1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[result_skt_n1]') AND type in (N'U')) drop table result_skt_n1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[skt_n]') AND type in (N'U')) drop table skt_n;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[result_skt_n]') AND type in (N'U')) drop table result_skt_n;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[result_dt]') AND type in (N'U')) drop table result_dt;

END
GO
