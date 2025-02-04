IF NOT EXISTS (SELECT * FROM DanhMuc WHERE iID_MaDanhMuc = 'HTTP_USER')
INSERT [dbo].[DanhMuc] ([iID_DanhMuc], [sType], [iID_MaDanhMuc], [sTen], [sGiaTri], [sMoTa], [iThuTu], [iNamLamViec], [iTrangThai], [dNgayTao], [sNguoiTao], [dNgaySua], [sNguoiSua], [Tag], [Log], [NganSachNganh]) VALUES (NEWID(), N'DM_CauHinh', N'HTTP_USER', N'HTTP User', N'Admin', N'HTTP User', 999, 2023, 1, CURRENT_TIMESTAMP, N'Admin', CURRENT_TIMESTAMP, N'Admin', NULL, NULL, NULL)
IF NOT EXISTS (SELECT * FROM DanhMuc WHERE iID_MaDanhMuc = 'HTTP_PASSWORD')
INSERT [dbo].[DanhMuc] ([iID_DanhMuc], [sType], [iID_MaDanhMuc], [sTen], [sGiaTri], [sMoTa], [iThuTu], [iNamLamViec], [iTrangThai], [dNgayTao], [sNguoiTao], [dNgaySua], [sNguoiSua], [Tag], [Log], [NganSachNganh]) VALUES (NEWID(), N'DM_CauHinh', N'HTTP_PASSWORD', N'HTTP Password', N'123456', N'HTTP Password', 999, 2023, 1, CURRENT_TIMESTAMP, N'Admin', CURRENT_TIMESTAMP, N'Admin', NULL, NULL, NULL)
IF NOT EXISTS (SELECT * FROM DanhMuc WHERE iID_MaDanhMuc = 'HTTP_PORT')
INSERT [dbo].[DanhMuc] ([iID_DanhMuc], [sType], [iID_MaDanhMuc], [sTen], [sGiaTri], [sMoTa], [iThuTu], [iNamLamViec], [iTrangThai], [dNgayTao], [sNguoiTao], [dNgaySua], [sNguoiSua], [Tag], [Log], [NganSachNganh]) VALUES (NEWID(), N'DM_CauHinh', N'HTTP_PORT', N'HTTP Port', N'80', N'Server Upload File', 999, 2023, 1, CURRENT_TIMESTAMP, N'Admin', CURRENT_TIMESTAMP, N'Admin', NULL, NULL, NULL)
IF NOT EXISTS (SELECT * FROM DanhMuc WHERE iID_MaDanhMuc = 'HTTP_HOST')
INSERT [dbo].[DanhMuc] ([iID_DanhMuc], [sType], [iID_MaDanhMuc], [sTen], [sGiaTri], [sMoTa], [iThuTu], [iNamLamViec], [iTrangThai], [dNgayTao], [sNguoiTao], [dNgaySua], [sNguoiSua], [Tag], [Log], [NganSachNganh]) VALUES (NEWID(), N'DM_CauHinh', N'HTTP_HOST', N'HTTP Host', N'http://10.60.108.246', N'Server Upload File', 999, 2023, 1, CURRENT_TIMESTAMP, N'Admin', CURRENT_TIMESTAMP, N'Admin', NULL, NULL, NULL)

/****** Object:  StoredProcedure [dbo].[sp_qs_chungtu_chitiet_tonghop]    Script Date: 12/07/2023 4:48:43 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qs_chungtu_chitiet_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qs_chungtu_chitiet_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_chungtu_chitiet]    Script Date: 12/07/2023 4:48:43 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qs_chungtu_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qs_chungtu_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_chungtu_chitiet]    Script Date: 12/07/2023 4:48:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_qs_chungtu_chitiet]
	@YearOfWork int,
	@VoucherId nvarchar(100),
	@AgencyId nvarchar(20)
AS
BEGIN
	SELECT iID_QSCTChiTiet,
		   sM,
		   ml.sKyHieu,
		   sMoTa,
		   bHangCha,
		   iID_MaDonVi,
		   fSoThieuUy,
		   fSoTrungUy,
		   fSoThuongUy,
		   fSoDaiUy,
		   fSoThieuTa,
		   fSoTrungTa,
		   fSoThuongTa,
		   fSoDaiTa,
		   fSoTuong,
		   fSoTSQ,
		   fSoBinhNhi,
		   fSoBinhNhat,
		   fSoHaSi,
		   fSoTrungSi,
		   fSoThuongSi,
		   fSoQNCN,
		   fSoVCQP,
		   fSoCNVQP,
		   fSoCY_H,
		   fSoCY_KT,
		   fSoLDHD,
		   fSoCcqp,
		   sGhiChu
	FROM
	  (SELECT iID_QSCTChiTiet,
			  sKyHieu,
			  iThangQuy,
			  iID_MaDonVi,
			  fSoThieuUy,
			  fSoTrungUy,
			  fSoThuongUy,
			  fSoDaiUy,
			  fSoThieuTa,
			  fSoTrungTa,
			  fSoThuongTa,
			  fSoDaiTa,
			  fSoTuong,
			  fSoTSQ,
			  fSoBinhNhi,
			  fSoBinhNhat,
			  fSoHaSi,
			  fSoTrungSi,
			  fSoThuongSi,
			  fSoQNCN,
			  fSoVCQP,
			  fSoCNVQP,
			  fSoCY_H,
			  fSoCY_KT,
			  fSoLDHD,
			  fSoCcqp,
			  sGhiChu
	   FROM NS_QS_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
		 AND iID_QSChungTu = @VoucherId
		 AND (@AgencyId IS NULL
			  OR iID_MaDonVi = @AgencyId) ) ctct -- lay mucluc quan so
	RIGHT JOIN
	  (SELECT sKyHieu,
			  sM,
			  sMoTa,
			  bHangCha
	   FROM NS_QS_MucLuc
	   WHERE iTrangThai = 1
		 AND iNamLamViec = @YearOfWork
		 AND sHienThi <> '2') AS ml ON ctct.sKyHieu = ml.sKyHieu
	ORDER BY ml.sKyHieu;
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_chungtu_chitiet_tonghop]    Script Date: 12/07/2023 4:48:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qs_chungtu_chitiet_tonghop]
	@YearOfWork int,
	@VoucherId nvarchar(100),
	@AgencyId nvarchar(MAX)
AS
BEGIN
	SELECT NULL AS iID_QSCTChiTiet,
	 ml.sKyHieu,
	 sM,
	 sMoTa,
	 bHangCha,
	 NULL AS iID_MaDonVi,
	 fSoThieuUy,
	 fSoTrungUy,
	 fSoThuongUy,
	 fSoDaiUy,
	 fSoThieuTa,
	 fSoTrungTa,
	 fSoThuongTa,
	 fSoDaiTa,
	 fSoTuong,
	 fSoTSQ,
	 fSoBinhNhi,
	 fSoBinhNhat,
	 fSoHaSi,
	 fSoTrungSi,
	 fSoThuongSi,
	 fSoQNCN,
	 fSoVCQP,
	 fSoCNVQP,
	 fSoCY_H,
	 fSoCY_KT,
	 fSoLDHD,
	 fSoCcqp,
	 '' AS SGhiChu
	FROM
	  (SELECT sKyHieu,
			  iThangQuy,
			  fSoThieuUy = sum(fSoThieuUy),
			  fSoTrungUy = sum(fSoTrungUy),
			  fSoThuongUy = sum(fSoThuongUy),
			  fSoDaiUy = sum(fSoDaiUy),
			  fSoThieuTa = sum(fSoThieuTa),
			  fSoTrungTa = sum(fSoTrungTa),
			  fSoThuongTa = sum(fSoThuongTa),
			  fSoDaiTa = sum(fSoDaiTa),
			  fSoTuong = sum(fSoTuong),
			  fSoTSQ = sum(fSoTSQ),
			  fSoBinhNhi = sum(fSoBinhNhi),
			  fSoBinhNhat = sum(fSoBinhNhat),
			  fSoHaSi = sum(fSoHaSi),
			  fSoTrungSi = sum(fSoTrungSi),
			  fSoThuongSi = sum(fSoThuongSi),
			  fSoQNCN = sum(fSoQNCN),
			  fSoVCQP = sum(fSoVCQP),
			  fSoCNVQP = sum(fSoCNVQP),
			  fSoCY_H = sum(fSoCY_H),
			  fSoCY_KT = sum(fSoCY_KT),
			  fSoLDHD = sum(fSoLDHD),
			  fSoCcqp = sum(fSoCcqp)
	   FROM NS_QS_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
		 AND iID_QSChungTu = @VoucherId
		 AND (@AgencyId IS NULL
			  OR iID_MaDonVi in
				(SELECT *
				 FROM splitstring(@AgencyId)))
	   GROUP BY sKyHieu,
				iThangQuy)ctct -- lay mucluc quan so
	RIGHT JOIN
	  (SELECT sKyHieu,
			  sM,
			  sMoTa,
			  bHangCha
	   FROM NS_QS_MucLuc
	   WHERE iTrangThai = 1
		 AND iNamLamViec = @YearOfWork
		 AND sHienThi <> '2') AS ml ON ctct.sKyHieu = ml.sKyHieu
	ORDER BY ml.sKyHieu

END
;
;
GO
