/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chi_tiet]    Script Date: 22/06/2023 6:50:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_kht_bhxh_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_kht_bhxh_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_khc_cd_bhxh_chi_tiet]    Script Date: 22/06/2023 6:50:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_khc_cd_bhxh_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_khc_cd_bhxh_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_thongtinkehoachthu_index]    Script Date: 22/06/2023 6:50:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_thongtinkehoachthu_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_thongtinkehoachthu_index]
GO
/****** Object:  StoredProcedure [dbo].[bh_xh_kehoach_chi_index]    Script Date: 22/06/2023 6:50:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[bh_xh_kehoach_chi_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[bh_xh_kehoach_chi_index]
GO
/****** Object:  StoredProcedure [dbo].[bh_xh_kehoach_chi_index]    Script Date: 22/06/2023 6:50:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Doantc
-- Create date: 13/06/2023
-- Description:	Lấy danh sách hiển thị lâp kế hoạch chi các chế dộ BHXH

-- =============================================
CREATE PROCEDURE [dbo].[bh_xh_kehoach_chi_index]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
		 CHBHXH.ID AS iID_BH_KHC_KinhPhiQuanLy
		, CHBHXH.sSoChungTu
		, CHBHXH.dNgayChungTu
		, CHBHXH.sSoQuyetDinh
		, CHBHXH.iNamChungTu
		, CHBHXH.iID_DonVi
		, CHBHXH.iID_MaDonVi
		, CHBHXH.sMoTa
		, CHBHXH.iTongSoDaThucHienNamTruoc
		, CHBHXH.iTongSoUocThucHienNamTruoc

		, CHBHXH.iTongSoKeHoachThucHienNamNay
		, CHBHXH.iTongSoSQ
		, CHBHXH.iTongSoQNCN
		, CHBHXH.iTongSoCNVQP
		, CHBHXH.iTongSoLDHD
		, CHBHXH.iTongSoHSQBS

		, CHBHXH.fTongTienDaThucHienNamTruoc

		, CHBHXH.fTongTienUocThucHienNamTruoc
		, CHBHXH.fTongTienKeHoachThucHienNamNay

		, CHBHXH.fTongTienSQ
		, CHBHXH.fTongTienQNCN
		, CHBHXH.fTongTienCNVQP
		, CHBHXH.fTongTienLDHD
		, CHBHXH.fTongTienHSQBS

		, CHBHXH.sTongHop
		, CHBHXH.iID_TongHopID
		, CHBHXH.iLoaiTongHop
		, CHBHXH.bIsKhoa

		, CHBHXH.dNgaySua
		, CHBHXH.dNgayTao
		, CHBHXH.dNgayQuyetDinh
		, CHBHXH.sNguoiSua
		, CHBHXH.sNguoiTao
		, CHBHXH.dNgayTao
		, donVi.sTenDonVi
		, (CHBHXH.fTongTienSQ+CHBHXH.fTongTienDaThucHienNamTruoc+CHBHXH.fTongTienKeHoachThucHienNamNay+CHBHXH.fTongTienQNCN+CHBHXH.fTongTienCNVQP+CHBHXH.fTongTienLDHD+CHBHXH.fTongTienUocThucHienNamTruoc+CHBHXH.fTongTienHSQBS) as isTongTien
		-- Tong dự toán todo
	FROM BH_KHC_CheDoBHXH CHBHXH
	LEFT JOIN DonVi donVi
		ON CHBHXH.iID_MaDonVi = donVi.iID_MaDonVi AND donVi.iID_DonVi = CHBHXH.iID_DonVi
	LEFT JOIN BH_KHC_CheDoBHXH_ChiTiet CHBHXHCT ON CHBHXH.ID=CHBHXHCT.iID_KHC_CheDoBHXH
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_thongtinkehoachthu_index]    Script Date: 22/06/2023 6:50:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bhxh_thongtinkehoachthu_index] 
	@YearOfWork int
AS
BEGIN
	SELECT
	KHT.iID_KHT_BHXH,
	KHT.sSoChungTu,
	KHT.iNamChungTu,
	KHT.dNgayChungTu,
	KHT.iID_MaDonVi,
	DV.sTenDonVi AS sTenDonVi,
	KHT.sMoTa,
	KHT.bIsKhoa,
	KHT.fTongKeHoach,
	KHT.sSoQuyetDinh,
	KHT.dNgayQuyetDinh,
	KHT.sNguoiTao,
	KHT.sNguoiSua,
	KHT.dNgayTao,
	KHT.dNgaySua
	FROM BH_KHT_BHXH KHT
	LEFT JOIN DonVi DV
	ON KHT.iID_MaDonVi = DV.iID_MaDonVi
	WHERE DV.iNamLamViec = @YearOfWork
	ORDER BY KHT.dNgayQuyetDinh DESC
END;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_khc_cd_bhxh_chi_tiet]    Script Date: 22/06/2023 6:50:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_khc_cd_bhxh_chi_tiet]
	@NamLamViec int,
	@IdDonVi nvarchar(100)
AS
BEGIN
SELECT 
		ct.iID_KHC_CheDoBHXHChiTiet ,
		ct.iID_KHC_CheDoBHXH ,
		ct.iID_MaDonVi,
		ct.iID_MucLucNganSach,
		ct.sMoTa,
		ct.sLoaiTroCap,
		ct.iSoDaThucHienNamTruoc,
		ct.iSoUocThucHienNamTruoc,
		ct.iSoKeHoachThucHienNamNay,
		ct.iSoSQ,
		ct.iSoQNCN,
		ct.iSoCNVQP,
		ct.iSoHSQBS,
		ct.iSoLDHD,
		ct.fTienDaThucHienNamTruoc,
		ct.fTienUocThucHienNamTruoc,
		ct.fTienKeHoachThucHienNamNay,
		ct.fTienSQ,
		ct.fTienQNCN,
		ct.fTienCNVQP,
		ct.fTienLDHD,
		ct.fTienHSQBS,
		ct.sTenDonVi,
		ct.sGhiChu,
		ct.dNgaySua,
		ct.dNgayTao,
		ct.sNguoiSua,
		ct.sNguoiTao
	FROM
		(
			SELECT
				bh.iID_MaDonVi,
				bh.sMoTa,
				ddv.sTenDonVi,
				bhct.*
			FROM 
				BH_KHC_CheDoBHXH bh
			JOIN 
				BH_KHC_CheDoBHXH_ChiTiet bhct ON bh.ID = bhct.iID_KHC_CheDoBHXH 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bh.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bh.iID_MaDonVi = @IdDonVi
				--AND (@BKhoa = 3 OR bh.bIsKhoa = @BKhoa) 
		) ct;

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chi_tiet]    Script Date: 22/06/2023 6:50:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_kht_bhxh_chi_tiet]
	@NamLamViec int,
	@IdDonVi nvarchar(100)
AS
BEGIN
SELECT 
		ct.iID_KHT_BHXH as BhKhtBHXHId,
		ct.*
	FROM
		(
			SELECT
				bh.iID_MaDonVi,
				bh.sMoTa,
				ddv.sTenDonVi,
				bhct.*
			FROM 
				BH_KHT_BHXH bh
			JOIN 
				BH_KHT_BHXH_ChiTiet bhct ON bh.iID_KHT_BHXH = bhct.iID_KHT_BHXH 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bh.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bh.iID_MaDonVi = @IdDonVi
		) ct;

END
;
;
GO


GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB]) VALUES (N'2e9cc5d3-54d5-48f4-a161-107792664000', N'9010001-010-011-0001-0001', N'9010001', N'10', N'11', N'1', N'1', N'', N'', N'', N'- Bản thân ốm (ngày)', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-06-22T15:09:01.570' AS DateTime), NULL, 0, NULL, NULL, NULL, N'a655d431-de68-4238-b921-55850d8bba6b', N'd4e84ab8-e7db-4886-8dac-99bb4ab91522', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB]) VALUES (N'bcd0e41f-e5d2-45a3-90b5-166c87fe7ceb', N'9020002-010-011-0001-0001', N'9020002', N'10', N'11', N'1', N'1', N'', N'', N'', N'2. QNCN', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-06-22T08:22:34.067' AS DateTime), NULL, 0, NULL, NULL, NULL, N'e89cbafd-a9b0-4b68-890b-d1d7db5b4824', N'8639c115-bdb3-49d5-9018-a1c9a3d24896', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB]) VALUES (N'c6d28fe9-f6a2-4050-bf53-166e7c233d14', N'9010001-010-011-0002-0001-01-00-01', N'9010001', N'10', N'11', N'2', N'1', N'1', N'0', N'1', N'+ Lao động nữ', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-06-22T15:09:06.303' AS DateTime), NULL, 0, NULL, NULL, NULL, N'b9de30f6-3364-47e3-ab8a-a476893817df', N'232a6a14-37ff-40d7-9753-21a3f0131208', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB]) VALUES (N'77584ca3-d04b-4f15-bc03-17f17a901420', N'9010001-010-011-0002-0000-00-00', N'9010001', N'10', N'11', N'2', N'0', N'0', N'0', N'', N'Trợ cấp thai sản (Chỉ tiêu)', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-06-22T15:09:05.680' AS DateTime), NULL, 0, NULL, NULL, NULL, N'b9ac23b6-5669-4be3-a4a2-6e0be29dc943', N'09ef0e0c-5c71-4783-b280-646d504af7b5', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB]) VALUES (N'05d68c10-47f9-4406-adf8-1f29dc4782a0', N'9020001-010-011-0002-0001', N'9020001', N'10', N'11', N'2', N'1', N'', N'', N'', N'2. LĐHĐ', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-06-22T08:22:32.980' AS DateTime), NULL, 0, NULL, NULL, NULL, N'84ae6d38-5bfb-4c0e-96dc-dcdf8be8cfb7', N'4ce7992c-f413-434c-b785-1b99d6cbd025', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB]) VALUES (N'250af9f7-1494-4845-94eb-2243ba8dc37e', N'9020001-010-011-0001-0000', N'9020001', N'10', N'11', N'1', N'0', N'', N'', N'', N'1. Sĩ quan', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-06-22T08:22:29.950' AS DateTime), NULL, 0, NULL, NULL, NULL, N'e06ad864-6b29-4f0e-bd3b-3e617e9dfda3', N'8de3ff2f-c2af-4608-afde-cc7e82d173a9', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB]) VALUES (N'8002b4d4-2c78-4431-b3a3-2ef7f5cc8023', N'9010001-010-011-0002-0001-01-00-02', N'9010001', N'10', N'11', N'2', N'1', N'1', N'0', N'2', N'+ Lao động nam', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-06-22T15:09:06.687' AS DateTime), NULL, 0, NULL, NULL, NULL, N'e23d1638-f729-4e97-82c3-62a1c02fd5a3', N'232a6a14-37ff-40d7-9753-21a3f0131208', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB]) VALUES (N'bca77868-4c55-4e19-b960-34767c68cf9e', N'9010001-010-011-0002-0001-01-00', N'9010001', N'10', N'11', N'2', N'1', N'1', N'0', N'', N'- Sinh con, nuôi con nuôi (tháng)', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-06-22T15:09:06.010' AS DateTime), NULL, 0, NULL, NULL, NULL, N'232a6a14-37ff-40d7-9753-21a3f0131208', N'b9ac23b6-5669-4be3-a4a2-6e0be29dc943', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB]) VALUES (N'e80a079a-b818-4866-ad60-455ae33d05ee', N'9020001-010-011-0001-0001', N'9020001', N'10', N'11', N'1', N'1', N'', N'', N'', N'2. QNCN', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-06-22T08:22:31.363' AS DateTime), NULL, 0, NULL, NULL, NULL, N'0d4fa8e6-6deb-4b99-924d-5532cf4181a6', N'8de3ff2f-c2af-4608-afde-cc7e82d173a9', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB]) VALUES (N'5cf173c7-5d89-488f-91a5-5ec0297a5034', N'9010001-010-011-0002-0001-03-00-02', N'9010001', N'10', N'11', N'2', N'1', N'3', N'0', N'2', N'+ Lao động nam', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-06-22T15:09:09.567' AS DateTime), NULL, 0, NULL, NULL, NULL, N'af4633bb-d405-401b-b7c9-c6108917fc89', N'd27fbd1b-73ad-48a0-bcba-111b296a6b1c', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB]) VALUES (N'613ea397-3482-4378-a5d3-60369a22d0c3', N'9020002-010-011-0002-0000', N'9020002', N'10', N'11', N'2', N'0', N'', N'', N'', N'1. CC,CN, VCQP', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-06-22T08:22:35.383' AS DateTime), NULL, 0, NULL, NULL, NULL, N'3bdd2a48-b367-464b-b253-2406afc558f1', N'30fb2fc1-3a2d-4e69-bfa4-3cf1e164bdb8', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB]) VALUES (N'010a9ab8-490a-44a5-a061-62a0bcfa528c', N'9010001-010-011-0002-0001-02-00-01', N'9010001', N'10', N'11', N'2', N'1', N'2', N'0', N'1', N'+ Lao động nữ', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-06-22T15:09:07.453' AS DateTime), NULL, 0, NULL, NULL, NULL, N'b79bd552-a5b7-4336-bdaf-27e4d99a04af', N'568c1146-2660-4db8-ad31-6e78d8c0daca', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB]) VALUES (N'7e74753d-906d-475c-b69f-658171e272be', N'9010001-010-011-0001-0001-01-01', N'9010001', N'10', N'11', N'1', N'1', N'1', N'1', N'', N'+ Thuộc DM bệnh dài ngày', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-06-22T15:09:01.873' AS DateTime), NULL, 0, NULL, NULL, NULL, N'89d5d8d1-205f-4cab-8839-51bb14888807', N'a655d431-de68-4238-b921-55850d8bba6b', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB]) VALUES (N'91fdc5ff-cd18-4888-adbd-6c6d234752ab', N'9010001-010-011-0001', N'9010001', N'10', N'11', N'1', N'', N'', N'', N'', N'1. Trợ cấp ốm đau', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-06-22T15:08:59.510' AS DateTime), NULL, 0, NULL, NULL, NULL, N'63e1e32a-460b-4696-aa73-441934842ac0', N'91465483-df1b-4262-9436-d87f8808cfac', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB]) VALUES (N'8cd2832d-e090-4e29-a8db-6d5c11569aad', N'9020001-010-011-0001', N'9020001', N'10', N'11', N'1', N'', N'', N'', N'', N'A. Quân nhân', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-06-22T08:22:29.397' AS DateTime), NULL, 0, NULL, NULL, NULL, N'8de3ff2f-c2af-4608-afde-cc7e82d173a9', N'3d59da6e-de0f-4534-a551-54d050d99060', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB]) VALUES (N'd4ab6ebe-9275-41f5-b9cf-70c18e37ce74', N'9020002-010-011-0001', N'9020002', N'10', N'11', N'1', N'', N'', N'', N'', N'A. Quân nhân', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-06-22T08:22:33.717' AS DateTime), NULL, 0, NULL, NULL, NULL, N'8639c115-bdb3-49d5-9018-a1c9a3d24896', N'6db21b59-7078-4a44-b0cb-9c6bd0cde94d', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB]) VALUES (N'865ab77a-235c-4321-83fc-71b7c3e0362e', N'9010001-010-011-0001-0001-01-02', N'9010001', N'10', N'11', N'1', N'1', N'1', N'2', N'', N'+ Ốm khác', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-06-22T15:09:02.193' AS DateTime), NULL, 0, NULL, NULL, NULL, N'8245d79e-27e0-4028-81e8-03a624c609df', N'a655d431-de68-4238-b921-55850d8bba6b', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB]) VALUES (N'd00b59c1-06f2-4a9f-aee2-764a3cb4f0c6', N'9020002-010-011-0001-0002', N'9020002', N'10', N'11', N'1', N'2', N'', N'', N'', N'3. HSQ, BS', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-06-22T08:22:34.253' AS DateTime), NULL, 0, NULL, NULL, NULL, N'ceec043e-4468-4128-9b09-18cf4b53b025', N'8639c115-bdb3-49d5-9018-a1c9a3d24896', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB]) VALUES (N'7390b586-3b1c-47ce-97a4-977347f3b9b7', N'9010001-010-011-0002-0001-02-00-02', N'9010001', N'10', N'11', N'2', N'1', N'2', N'0', N'2', N'+ Lao động nam', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-06-22T15:09:07.870' AS DateTime), NULL, 0, NULL, NULL, NULL, N'e1859298-643f-4f14-a387-231474454b25', N'568c1146-2660-4db8-ad31-6e78d8c0daca', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB]) VALUES (N'ce0e7cfb-7159-4005-ad3b-9d16c14ef9b1', N'9020001', N'9020001', N'', N'', N'', N'', N'', N'', N'', N'I. KHỐI DỰ TOÁN', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-06-22T08:22:28.700' AS DateTime), NULL, 0, NULL, NULL, NULL, N'3d59da6e-de0f-4534-a551-54d050d99060', N'a2ac88e5-dc93-4baa-b461-79f9a7da770f', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB]) VALUES (N'8a30e24f-9c95-4221-88b4-ac3f39befae8', N'9010001-010-011-0001-0003-03-00', N'9010001', N'10', N'11', N'1', N'3', N'3', N'0', N'', N'- Dưỡng sức, PHSK', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-06-22T15:09:04.060' AS DateTime), NULL, 0, NULL, NULL, NULL, N'280b18fa-ca4b-4e46-bb55-1bd81e962e7c', N'd4e84ab8-e7db-4886-8dac-99bb4ab91522', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB]) VALUES (N'ec3a5688-9818-4e8b-acc8-af16c66fd6e8', N'9010001-010-011-0002-0001-04-00', N'9010001', N'10', N'11', N'2', N'1', N'4', N'0', N'', N'- Dưỡng sức, phục hồi SK', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-06-22T15:09:09.920' AS DateTime), NULL, 0, NULL, NULL, NULL, N'8e092c28-89d2-460b-ab60-5aafbf9e010a', N'b9ac23b6-5669-4be3-a4a2-6e0be29dc943', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB]) VALUES (N'e118d1dc-a71b-4435-8274-b1c303c652fa', N'9010001-010-011-0001-0000-00-00', N'9010001', N'10', N'11', N'1', N'0', N'0', N'0', N'', N'Trợ cấp ốm đau (Chỉ tiêu)', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-06-22T15:09:00.243' AS DateTime), NULL, 0, NULL, NULL, NULL, N'd4e84ab8-e7db-4886-8dac-99bb4ab91522', N'63e1e32a-460b-4696-aa73-441934842ac0', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB]) VALUES (N'36d97f82-734a-488c-8217-b2e09be675ef', N'9010001-010-011-0002', N'9010001', N'10', N'11', N'2', N'', N'', N'', N'', N'2. Trợ cấp thai sản', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-06-22T15:09:04.610' AS DateTime), NULL, 0, NULL, NULL, NULL, N'09ef0e0c-5c71-4783-b280-646d504af7b5', N'91465483-df1b-4262-9436-d87f8808cfac', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB]) VALUES (N'6e97e402-7b0c-42d4-a700-b7203ee3035f', N'9020002-010-011-0002-0001', N'9020002', N'10', N'11', N'2', N'1', N'', N'', N'', N'2. LĐHĐ', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-06-22T08:22:36.143' AS DateTime), NULL, 0, NULL, NULL, NULL, N'9eb683c3-3d96-44c9-9b10-4f947da34008', N'30fb2fc1-3a2d-4e69-bfa4-3cf1e164bdb8', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB]) VALUES (N'46be6d72-3013-491b-98b8-ba52699588a9', N'9020001-010-011-0002-0000', N'9020001', N'10', N'11', N'2', N'0', N'', N'', N'', N'1. CC,CN, VCQP', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-06-22T08:22:32.313' AS DateTime), NULL, 0, NULL, NULL, NULL, N'0bbae696-0592-4757-8431-f2f40f460b7a', N'4ce7992c-f413-434c-b785-1b99d6cbd025', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB]) VALUES (N'878d98cd-8553-4005-8585-bad63f392013', N'9010001', N'9010001', N'', N'', N'', N'', N'', N'', N'', N'I. KHỐI DỰ TOÁN', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-06-22T15:08:59.103' AS DateTime), NULL, 0, NULL, NULL, NULL, N'91465483-df1b-4262-9436-d87f8808cfac', NULL, 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB]) VALUES (N'8bd4d794-50d6-4270-83b8-c34528879ce8', N'9010001-010-011-0002-0001-03-00-01', N'9010001', N'10', N'11', N'2', N'1', N'3', N'0', N'1', N'+ Lao động nữ', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-06-22T15:09:09.137' AS DateTime), NULL, 0, NULL, NULL, NULL, N'0e323456-0843-4c30-b020-e686be1f879b', N'd27fbd1b-73ad-48a0-bcba-111b296a6b1c', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB]) VALUES (N'c9e07890-ceaa-4c01-8e50-c8b1a7fe3fc0', N'9020002-010-011-0002', N'9020002', N'10', N'11', N'2', N'', N'', N'', N'', N'B. Người LĐ', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-06-22T08:22:34.843' AS DateTime), NULL, 0, NULL, NULL, NULL, N'30fb2fc1-3a2d-4e69-bfa4-3cf1e164bdb8', N'6db21b59-7078-4a44-b0cb-9c6bd0cde94d', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB]) VALUES (N'9031bd5a-230c-41b5-93d2-d3b6106a3a69', N'9020001-010-011-0001-0002', N'9020001', N'10', N'11', N'1', N'2', N'', N'', N'', N'3. HSQ, BS', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-06-22T08:22:31.730' AS DateTime), NULL, 0, NULL, NULL, NULL, N'9394f8c1-479d-438a-813a-22c105b5e731', N'8de3ff2f-c2af-4608-afde-cc7e82d173a9', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB]) VALUES (N'57e9f5d9-88ce-4c1f-a097-d9f06133e9d8', N'9010001-010-011-0002-0001-03-00', N'9010001', N'10', N'11', N'2', N'1', N'3', N'0', N'', N'- Khám thai, KHH GĐ (ngày)', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-06-22T15:09:08.280' AS DateTime), NULL, 0, NULL, NULL, NULL, N'd27fbd1b-73ad-48a0-bcba-111b296a6b1c', N'b9ac23b6-5669-4be3-a4a2-6e0be29dc943', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB]) VALUES (N'b21567df-56f5-4ece-8edc-e44aceab40dc', N'9020002-010-011-0001-0000', N'9020002', N'10', N'11', N'1', N'0', N'', N'', N'', N'1. Sĩ quan', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-06-22T08:22:33.827' AS DateTime), NULL, 0, NULL, NULL, NULL, N'87d5e78c-2370-40b6-b7a8-ec285ed08504', N'8639c115-bdb3-49d5-9018-a1c9a3d24896', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB]) VALUES (N'66976482-853d-485e-a322-e6c3b7d39aaf', N'9010001-010-011-0001-0002-02-00', N'9010001', N'10', N'11', N'1', N'2', N'2', N'0', N'', N'- Con ốm', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-06-22T15:09:03.663' AS DateTime), NULL, 0, NULL, NULL, NULL, N'a297c80b-7b65-40e6-823f-fbed19e77501', N'd4e84ab8-e7db-4886-8dac-99bb4ab91522', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB]) VALUES (N'03dde048-6d43-4510-9f78-ed4f3e126a14', N'9020001-010-011-0002', N'9020001', N'10', N'11', N'2', N'', N'', N'', N'', N'B. Người LĐ', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-06-22T08:22:32.117' AS DateTime), NULL, 0, NULL, NULL, NULL, N'4ce7992c-f413-434c-b785-1b99d6cbd025', N'3d59da6e-de0f-4534-a551-54d050d99060', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB]) VALUES (N'4cba4d1b-d4bd-4a2b-95bb-ee482d2cb172', N'9010001-010-011-0002-0001-02-00', N'9010001', N'10', N'11', N'2', N'1', N'2', N'0', N'', N'- Trợ cấp 1 lần (người)', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-06-22T15:09:07.113' AS DateTime), NULL, 0, NULL, NULL, NULL, N'568c1146-2660-4db8-ad31-6e78d8c0daca', N'b9ac23b6-5669-4be3-a4a2-6e0be29dc943', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB]) VALUES (N'656a52cd-6e54-474d-b89f-f006f7c12b1d', N'9010001-010-011-0001-0001-01-02-02', N'9010001', N'10', N'11', N'1', N'1', N'1', N'2', N'2', N'* Từ 14 ngày trở lên', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-06-22T15:09:02.850' AS DateTime), NULL, 0, NULL, NULL, NULL, N'63c6f9a0-3e99-4b15-8bec-9c8ffb75be29', N'8245d79e-27e0-4028-81e8-03a624c609df', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB]) VALUES (N'4ed7ebda-4ec7-44f7-8592-f508ead6593a', N'9020002', N'9020002', N'', N'', N'', N'', N'', N'', N'', N'I. KHỐI HẠCH TOÁN', 1, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-06-22T08:22:33.487' AS DateTime), NULL, 0, NULL, NULL, NULL, N'6db21b59-7078-4a44-b0cb-9c6bd0cde94d', N'a2ac88e5-dc93-4baa-b461-79f9a7da770f', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB]) VALUES (N'd5aa4ee4-e9ee-4090-9d17-fec94f95979a', N'9010001-010-011-0001-0001-01-02-01', N'9010001', N'10', N'11', N'1', N'1', N'1', N'2', N'1', N'* Dưới 14 ngày', 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-06-22T15:09:02.497' AS DateTime), NULL, 0, NULL, NULL, NULL, N'5c1357a4-f877-4c19-9bf6-f7c85e8c2f99', N'8245d79e-27e0-4028-81e8-03a624c609df', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL)
GO
