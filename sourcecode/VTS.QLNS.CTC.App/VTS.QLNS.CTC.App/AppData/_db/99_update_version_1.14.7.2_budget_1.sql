GO
DELETE FROM DM_ChuKy WHERE Id_Code = 'rptNS_PhuongAn_PhanBo_SoKiemTra_02a'
GO
INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) 
VALUES (NEWID(), 1, N'11016', NULL, N'11018', NULL, N'11015', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rptNS_PhuongAn_PhanBo_SoKiemTra_02a', NULL, N'rptNS_PhuongAn_PhanBo_SoKiemTra_02a', NULL, NULL, NULL, NULL, NULL, N'SO_KIEM_TRA_PHUONG_AN_PHAN_BO', NULL, N'Báo cáo phương án phân bổ số kiểm tra', N'Trưởng Phòng Tài Chính', NULL, N'Trưởng Phòng Tài Chính', NULL, N'Trưởng Phòng Tài Chính', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'11018', NULL, N'11020', NULL, N'11015', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', N'CHI TIẾT PHƯƠNG ÁN PHÂN BỔ SỐ KIỂM TRA NGÂN SÁCH NHÀ NƯỚC CHI THƯỜNG XUYÊN CHO QUỐC PHÒNG', NULL, N'THÔNG BÁO PHÂN BỔ CHO CÁC ĐƠN VỊ TRỰC THUỘC', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_phuong_an_phan_bo_skt_02a]    Script Date: 8/8/2024 3:36:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_phuong_an_phan_bo_skt_02a]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_phuong_an_phan_bo_skt_02a]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_phuong_an_phan_bo_skt_02a]    Script Date: 8/8/2024 3:36:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_rpt_skt_phuong_an_phan_bo_skt_02a]
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

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DonViTempSKT]') AND type in (N'U')) drop table DonViTempSKT;

	IF (@Khoi = 0)
		BEGIN
			SELECT * INTO DonViTempSKT FROM DonVi WHERE iTrangThai = 1 AND iNamLamViec = @NamLamViec
		END
	ELSE
		BEGIN
			SELECT * INTO DonViTempSKT FROM DonVi WHERE iTrangThai = 1 AND iNamLamViec = @NamLamViec AND iKhoi = @Khoi
		END

	SELECT iID_MLSKT IIdMlskt,
		   iID_MLSKTCha IIdMlsktCha,
		   dt_dv.iID_MaDonVi idDonVi,
		   dt_dv.sTenDonVi,
		   dt_dv.iLoai,
		   sKyHieu,
		   sSTT STT,
		   sSTTBC SSTTBC,
		   bHangCha,
		   sNG,
		   sMoTa,
		   sNG_Cha sNgCha,
		   QuyetToan =SUM(IsNull(A.QuyetToan,0))/@DVT,
		   DuToan =SUM(IsNull(A.DuToan,0))/@DVT,
		   TuChi =SUM(IsNull(A.TuChi,0))/@DVT,
		   PhanCap =SUM(IsNull(A.PhanCap,0))/@DVT,
		   MuaHangHienVat =SUM(IsNull(A.MuaHangHienVat,0))/@DVT,
		   DacThu =SUM(IsNull(A.PhanCap,0))/@DVT
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
	   RIGHT JOIN NS_SKT_MucLuc ml ON ct.sKyHieu = ml.sKyHieu AND ml.iNamLamViec = @NamLamViec AND ml.iTrangThai = 1
	   WHERE ct.iNamLamViec = @NamLamViec
		 AND ct.iNamNganSach = @NamNganSach
		 AND ct.iID_MaNguonNganSach = @NguonNganSach
		 AND CT.iLoai <> 3
		 AND ct.iLoaiChungTu = 1
		 AND (@loaiNNS = 0 OR chungTu.iLoaiNguonNganSach = @loaiNNS)
		 AND ((@BTongHop = 0) OR (@BTongHop = 1 AND chungtu.bDaTongHop = @BTongHop))
		 AND ml.sNG in
		   (SELECT *
			FROM f_split(@Nganh)))AS A 
	JOIN
	  (SELECT iID_MaDonVi, sTenDonVi, iLoai FROM DonViTempSKT) AS dt_dv ON A.iID_MaDonVi = dt_dv.iID_MaDonVi
		GROUP BY iID_MLSKT,
				 iID_MLSKTCha,
				 dt_dv.iID_MaDonVi,
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

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DonViTempSKT]') AND type in (N'U')) drop table DonViTempSKT;
END
;
;
;
;
;
;
GO
