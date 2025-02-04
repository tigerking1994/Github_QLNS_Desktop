/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_chitiet_noidung_kpql]    Script Date: 9/12/2024 10:19:03 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_chitiet_noidung_kpql]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_chitiet_noidung_kpql]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_chitiet_noidung_kpql]    Script Date: 9/12/2024 10:19:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_chitiet_noidung_kpql]
@iDChungTu uniqueidentifier,
@IIDDonVi nvarchar(50),
@NamLamViec int,
@DonViTinh int ,
@IsMillionRound bit
AS
BEGIN
	SELECT * into #tblChiQuanLy FROM (
	SELECT '1' AS STT, N'Chi kinh quản lý BHXH, BHYT, BHTN'  AS SNoiDung, 0 fSoTien , 1 bHangCha
	union all
	SELECT '' AS STT,N'         Trong đó: Ngành Cán bộ' AS SNoiDung, 
	CASE WHEN @IsMillionRound=1 THEN round((SUM (isnull(fSoTien,0))/ 1000000),0)*1000000/ @DonViTinh ELSE round((SUM (fSoTien)/ @DonViTinh),0) END fSoTien,
	0 bHangCha 
	FROM BH_DuToan_CTCT_KPQL WHERE iID_ChungTu=@iDChungTu AND iID_MaDonVi=@IIDDonVi AND iNamLamViec=@NamLamViec
	AND sXauNoiMa='9010011-010-0001'
	union all
	SELECT '' AS STT,N'                          Ngành Quân lực' AS SNoiDung,
	CASE WHEN @IsMillionRound=1 THEN round((SUM (isnull(fSoTien,0))/ 1000000),0)*1000000/ @DonViTinh ELSE round((SUM (fSoTien)/ @DonViTinh),0) END fSoTien,
	0 bHangCha FROM BH_DuToan_CTCT_KPQL WHERE iID_ChungTu=@iDChungTu AND iID_MaDonVi=@IIDDonVi AND iNamLamViec=@NamLamViec
	AND sXauNoiMa='9010011-010-0002'
	union all
	SELECT '' AS STT,N'                          Ngành Tài chính' AS SNoiDung,
CASE WHEN @IsMillionRound=1 THEN round((SUM (isnull(fSoTien,0))/ 1000000),0)*1000000/ @DonViTinh ELSE round((SUM (fSoTien)/ @DonViTinh),0) END fSoTien,
	0 bHangCha FROM BH_DuToan_CTCT_KPQL WHERE iID_ChungTu=@iDChungTu AND iID_MaDonVi=@IIDDonVi AND iNamLamViec=@NamLamViec
	AND sXauNoiMa='9010011-010-0003'
	union all
	SELECT '' AS STT,N'                          Ngành Quân y' AS SNoiDung,
CASE WHEN @IsMillionRound=1 THEN round((SUM (isnull(fSoTien,0))/ 1000000),0)*1000000/ @DonViTinh ELSE round((SUM (fSoTien)/ @DonViTinh),0) END fSoTien,
	0 bHangCha FROM BH_DuToan_CTCT_KPQL WHERE iID_ChungTu=@iDChungTu AND iID_MaDonVi=@IIDDonVi AND iNamLamViec=@NamLamViec
	AND sXauNoiMa='9010011-010-0004'
	) bhxhbhytbhtn

	UPDATE A
	SET A.fSoTien=B.fSoTien
	FROM #tblChiQuanLy A, ( SELECT SUM(fSoTien) fSoTien from #tblChiQuanLy) B
	WHERE A.bHangCha=1
	
	SELECT * INTO #tblHoTroandChi FROM (
	SELECT '2' AS STT,N'Hỗ trợ cán bộ, nhân viên làm công tác BHXH, BHYT' AS SNoiDung,
	CASE WHEN @IsMillionRound=1 THEN round((SUM (isnull(fSoTien,0))/ 1000000),0)*1000000/ @DonViTinh ELSE round((SUM (fSoTien)/ @DonViTinh),0) END fSoTien,
	1 bHangCha FROM BH_DuToan_CTCT_KPQL WHERE iID_ChungTu=@iDChungTu AND iID_MaDonVi=@IIDDonVi AND iNamLamViec=@NamLamViec
	AND sXauNoiMa='9010011-011'
	union all
	SELECT '3' AS STT,N'Chi tập huấn nghiệp vụ BHXH, BHYT, BHTN (Cơ quan Quân lực chủ trì phối hợp với cơ quan Tài chính, Cán bộ, Quân y tổ chức thực hiện)' AS SNoiDung,
	CASE WHEN @IsMillionRound=1 THEN round((SUM (isnull(fSoTien,0))/ 1000000),0)*1000000/ @DonViTinh ELSE round((SUM (fSoTien)/ @DonViTinh),0) END fSoTien,
	1 bHangCha FROM BH_DuToan_CTCT_KPQL WHERE iID_ChungTu=@iDChungTu AND iID_MaDonVi=@IIDDonVi AND iNamLamViec=@NamLamViec
	AND sXauNoiMa='9010011-012') TBL
	
	SELECT * INTO #tblHoTroThu FROM
	(
	SELECT '4' AS STT,N'Hỗ trợ quản lý thu, chi BHXH, BHYT, BHTN ở đơn vị cơ sở (do ngành Tài chính quản lý báo cáo Thủ trưởng phân cấp cho đơn vị cơ sở)' AS SNoiDung, 0 fSoTien,1 bHangCha 
	union all
	SELECT '' AS STT,N'         Trong đó: Chi thường xuyên đặc thù' AS SNoiDung,
	CASE WHEN @IsMillionRound=1 THEN round((SUM (isnull(fSoTien,0))/ 1000000),0)*1000000/ @DonViTinh ELSE round((SUM (fSoTien)/ @DonViTinh),0) END fSoTien,
	0 bHangCha FROM BH_DuToan_CTCT_KPQL WHERE iID_ChungTu=@iDChungTu AND iID_MaDonVi=@IIDDonVi AND iNamLamViec=@NamLamViec
	AND sXauNoiMa LIKE '9010011-013-0001%'
	union all
	SELECT '' AS STT,N'                          Chi không thường xuyên' AS SNoiDung,
	CASE WHEN @IsMillionRound=1 THEN round((SUM (isnull(fSoTien,0))/ 1000000),0)*1000000/ @DonViTinh ELSE round((SUM (fSoTien)/ @DonViTinh),0) END fSoTien,
	0 bHangCha FROM BH_DuToan_CTCT_KPQL WHERE iID_ChungTu=@iDChungTu AND iID_MaDonVi=@IIDDonVi AND iNamLamViec=@NamLamViec
	AND sXauNoiMa = '9010011-013-0002') TBL

	UPDATE A
	set A.fSoTien=B.fSoTien
	FROM #tblHoTroThu A ,(SELECT SUM(fSoTien) fSoTien FROM #tblHoTroThu) B
	WHERE A.bHangCha=1

	SELECT * FROM (
	SELECT * FROM #tblChiQuanLy
	UNION ALL 
	SELECT * FROM #tblHoTroandChi
	UNION ALL 
	SELECT * FROM #tblHoTroThu) TBL 

	DROP TABLE #tblChiQuanLy
	DROP TABLE #tblHoTroandChi
	DROP TABLE #tblHoTroThu
END
;
;
;
;
;
;
GO
--9010011 - Chi kinh phí quản lý BHXH, BHYT (giao chi tiết cho đơn vị)
--9010011-010 - Chi quản lý BHXH, BHYT
--9010011-010-0001 - Ngành Cán bộ
--9010011-010-0002 - Ngành Quân lực
--9010011-010-0003 - Ngành Tài chính
--9010011-010-0004 - Ngành Quân y
--9010011-011 - Hỗ trợ cán bộ chuyên trách làm công tác BHXH, BHYT
--9010011-012 - Chi tập huấn nghiệp vụ BHXH, BHYT, BHTN
--9010011-013 - Hỗ trợ quản lý thu, chi BHXH, BHYT ở đơn vị cơ sở
--9010011-013-0001 - Chi thường xuyên đặc thù
--9010011-013-0001-0001 - Ngành Cán bộ
--9010011-013-0001-0002 - Ngành Quân lực
--9010011-013-0001-0003 - Ngành Tài chính
--9010011-013-0001-0004 - Ngành Quân y
--9010011-013-0002 - Chi không thường xuyên

--2023
GO
DELETE FROM BH_DM_MucLucNganSach WHERE sxaunoima in ('9010011','9010011-010','9010011-010-0001','9010011-010-0002','9010011-010-0003','9010011-010-0004','9010011-011','9010011-012','9010011-013','9010011-013-0001','9010011-013-0001-0001','9010011-013-0001-0002','9010011-013-0001-0003','9010011-013-0001-0004','9010011-013-0002')
	AND iNamLamViec = 2023
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 1, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2023-08-26T11:32:29.5989902' AS DateTime2), CAST(N'2023-08-26T11:31:18.8342156' AS DateTime2), NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9' and iNamLamViec = 2023)
, NULL, NULL, N'STM', NULL, 0, 2023, 1, NULL, NULL, N'NG', N'', NULL, NULL, N'9010011', NULL, NULL, N'Chi kinh phí quản lý BHXH, BHYT (giao chi tiết cho đơn vị)', N'', N'admin', N'admin', NULL, N'', NULL, N'', N'', N'', N'', N'', NULL, N'9010011', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)

GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 1, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2023-08-26T11:32:29.5989902' AS DateTime2), CAST(N'2023-08-26T11:31:18.8342156' AS DateTime2), NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9010011' and iNamLamViec = 2023)
, NULL, NULL, N'STM', NULL, 0, 2023, 1, NULL, NULL, N'NG', N'', NULL, N'010', N'9010011', NULL, NULL, N'Chi quản lý BHXH, BHYT', N'', N'admin', N'admin', NULL, N'', NULL, N'', N'', N'', N'', N'', NULL, N'9010011-010', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)

GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2023-08-26T11:32:29.5989902' AS DateTime2), CAST(N'2023-08-26T11:31:18.8342156' AS DateTime2), NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9010011-010' and iNamLamViec = 2023)
, NULL, NULL, N'STM', NULL, 0, 2023, 1, NULL, NULL, N'NG', N'', N'0001', N'010', N'9010011', NULL, NULL, N'- Ngành Cán bộ', N'', N'admin', N'admin', NULL, N'', NULL, N'', N'', N'', N'', N'', NULL, N'9010011-010-0001', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)

GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2023-08-26T11:32:29.5989902' AS DateTime2), CAST(N'2023-08-26T11:31:18.8342156' AS DateTime2), NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9010011-010' and iNamLamViec = 2023)
, NULL, NULL, N'STM', NULL, 0, 2023, 1, NULL, NULL, N'NG', N'', N'0002', N'010', N'9010011', NULL, NULL, N'- Ngành Quân lực', N'', N'admin', N'admin', NULL, N'', NULL, N'', N'', N'', N'', N'', NULL, N'9010011-010-0002', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)

GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2023-08-26T11:32:29.5989902' AS DateTime2), CAST(N'2023-08-26T11:31:18.8342156' AS DateTime2), NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9010011-010' and iNamLamViec = 2023)
, NULL, NULL, N'STM', NULL, 0, 2023, 1, NULL, NULL, N'NG', N'', N'0003', N'010', N'9010011', NULL, NULL, N'- Ngành Tài chính', N'', N'admin', N'admin', NULL, N'', NULL, N'', N'', N'', N'', N'', NULL, N'9010011-010-0003', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)

GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2023-08-26T11:32:29.5989902' AS DateTime2), CAST(N'2023-08-26T11:31:18.8342156' AS DateTime2), NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9010011-010' and iNamLamViec = 2023)
, NULL, NULL, N'STM', NULL, 0, 2023, 1, NULL, NULL, N'NG', N'', N'0004', N'010', N'9010011', NULL, NULL, N'- Ngành Quân y', N'', N'admin', N'admin', NULL, N'', NULL, N'', N'', N'', N'', N'', NULL, N'9010011-010-0004', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2023-08-26T11:32:29.5989902' AS DateTime2), CAST(N'2023-08-26T11:31:18.8342156' AS DateTime2), NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9010011' and iNamLamViec = 2023)
, NULL, NULL, N'STM', NULL, 0, 2023, 1, NULL, NULL, N'NG', N'', NULL, N'011', N'9010011', NULL, NULL, N'Hỗ trợ cán bộ chuyên trách làm công tác BHXH, BHYT', N'', N'admin', N'admin', NULL, N'', NULL, N'', N'', N'', N'', N'', NULL, N'9010011-011', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2023-08-26T11:32:29.5989902' AS DateTime2), CAST(N'2023-08-26T11:31:18.8342156' AS DateTime2), NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9010011' and iNamLamViec = 2023)
, NULL, NULL, N'STM', NULL, 0, 2023, 1, NULL, NULL, N'NG', N'', NULL, N'012', N'9010011', NULL, NULL, N'Chi tập huấn nghiệp vụ BHXH, BHYT, BHTN', N'', N'admin', N'admin', NULL, N'', NULL, N'', N'', N'', N'', N'', NULL, N'9010011-012', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)

GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 1, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2023-08-26T11:32:29.5989902' AS DateTime2), CAST(N'2023-08-26T11:31:18.8342156' AS DateTime2), NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9010011' and iNamLamViec = 2023)
, NULL, NULL, N'STM', NULL, 0, 2023, 1, NULL, NULL, N'NG', N'', NULL, N'013', N'9010011', NULL, NULL, N'Hỗ trợ quản lý thu, chi BHXH, BHYT ở đơn vị cơ sở', N'', N'admin', N'admin', NULL, N'', NULL, N'', N'', N'', N'', N'', NULL, N'9010011-013', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)

GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 1, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2023-08-26T11:32:29.5989902' AS DateTime2), CAST(N'2023-08-26T11:31:18.8342156' AS DateTime2), NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9010011-013' and iNamLamViec = 2023)
, NULL, NULL, N'STM', NULL, 0, 2023, 1, NULL, NULL, N'NG', N'', N'0001', N'013', N'9010011', NULL, NULL, N'Chi thường xuyên đặc thù', N'', N'admin', N'admin', NULL, N'', NULL, N'', N'', N'', N'', N'', NULL, N'9010011-013-0001', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)

GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2023-08-26T11:32:29.5989902' AS DateTime2), CAST(N'2023-08-26T11:31:18.8342156' AS DateTime2), NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9010011-013-0001' and iNamLamViec = 2023)
, NULL, NULL, N'STM', NULL, 0, 2023, 1, NULL, NULL, N'NG', N'', N'0001', N'013', N'9010011', N'0001', NULL, N'- Ngành Cán bộ', N'', N'admin', N'admin', NULL, N'', NULL, N'', N'', N'', N'', N'', NULL, N'9010011-013-0001-0001', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)

GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2023-08-26T11:32:29.5989902' AS DateTime2), CAST(N'2023-08-26T11:31:18.8342156' AS DateTime2), NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9010011-013-0001' and iNamLamViec = 2023)
, NULL, NULL, N'STM', NULL, 0, 2023, 1, NULL, NULL, N'NG', N'', N'0001', N'013', N'9010011', N'0002', NULL, N'- Ngành Quân lực', N'', N'admin', N'admin', NULL, N'', NULL, N'', N'', N'', N'', N'', NULL, N'9010011-013-0001-0002', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)

GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2023-08-26T11:32:29.5989902' AS DateTime2), CAST(N'2023-08-26T11:31:18.8342156' AS DateTime2), NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9010011-013-0001' and iNamLamViec = 2023)
, NULL, NULL, N'STM', NULL, 0, 2023, 1, NULL, NULL, N'NG', N'', N'0001', N'013', N'9010011', N'0003', NULL, N'- Ngành Tài chính', N'', N'admin', N'admin', NULL, N'', NULL, N'', N'', N'', N'', N'', NULL, N'9010011-013-0001-0003', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)

GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2023-08-26T11:32:29.5989902' AS DateTime2), CAST(N'2023-08-26T11:31:18.8342156' AS DateTime2), NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9010011-013-0001' and iNamLamViec = 2023)
, NULL, NULL, N'STM', NULL, 0, 2023, 1, NULL, NULL, N'NG', N'', N'0001', N'013', N'9010011', N'0004', NULL, N'- Ngành Quân y', N'', N'admin', N'admin', NULL, N'', NULL, N'', N'', N'', N'', N'', NULL, N'9010011-013-0001-0004', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)

GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2023-08-26T11:32:29.5989902' AS DateTime2), CAST(N'2023-08-26T11:31:18.8342156' AS DateTime2), NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9010011-013' and iNamLamViec = 2023)
, NULL, NULL, N'STM', NULL, 0, 2023, 1, NULL, NULL, N'NG', N'', N'0002', N'013', N'9010011', NULL, NULL, N'Chi không thường xuyên', N'', N'admin', N'admin', NULL, N'', NULL, N'', N'', N'', N'', N'', NULL, N'9010011-013-0002', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)

--2024
GO
DELETE FROM BH_DM_MucLucNganSach WHERE sxaunoima in ('9010011','9010011-010','9010011-010-0001','9010011-010-0002','9010011-010-0003','9010011-010-0004','9010011-011','9010011-012','9010011-013','9010011-013-0001','9010011-013-0001-0001','9010011-013-0001-0002','9010011-013-0001-0003','9010011-013-0001-0004','9010011-013-0002')
	AND iNamLamViec = 2024
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 1, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2024-08-26T11:32:29.5989902' AS DateTime2), CAST(N'2024-08-26T11:31:18.8342156' AS DateTime2), NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9' and iNamLamViec = 2024)
, NULL, NULL, N'STM', NULL, 0, 2024, 1, NULL, NULL, N'NG', N'', NULL, NULL, N'9010011', NULL, NULL, N'Chi kinh phí quản lý BHXH, BHYT (giao chi tiết cho đơn vị)', N'', N'admin', N'admin', NULL, N'', NULL, N'', N'', N'', N'', N'', NULL, N'9010011', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)

GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 1, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2024-08-26T11:32:29.5989902' AS DateTime2), CAST(N'2024-08-26T11:31:18.8342156' AS DateTime2), NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9010011' and iNamLamViec = 2024)
, NULL, NULL, N'STM', NULL, 0, 2024, 1, NULL, NULL, N'NG', N'', NULL, N'010', N'9010011', NULL, NULL, N'Chi quản lý BHXH, BHYT', N'', N'admin', N'admin', NULL, N'', NULL, N'', N'', N'', N'', N'', NULL, N'9010011-010', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)

GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2024-08-26T11:32:29.5989902' AS DateTime2), CAST(N'2024-08-26T11:31:18.8342156' AS DateTime2), NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9010011-010' and iNamLamViec = 2024)
, NULL, NULL, N'STM', NULL, 0, 2024, 1, NULL, NULL, N'NG', N'', N'0001', N'010', N'9010011', NULL, NULL, N'- Ngành Cán bộ', N'', N'admin', N'admin', NULL, N'', NULL, N'', N'', N'', N'', N'', NULL, N'9010011-010-0001', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)

GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2024-08-26T11:32:29.5989902' AS DateTime2), CAST(N'2024-08-26T11:31:18.8342156' AS DateTime2), NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9010011-010' and iNamLamViec = 2024)
, NULL, NULL, N'STM', NULL, 0, 2024, 1, NULL, NULL, N'NG', N'', N'0002', N'010', N'9010011', NULL, NULL, N'- Ngành Quân lực', N'', N'admin', N'admin', NULL, N'', NULL, N'', N'', N'', N'', N'', NULL, N'9010011-010-0002', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)

GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2024-08-26T11:32:29.5989902' AS DateTime2), CAST(N'2024-08-26T11:31:18.8342156' AS DateTime2), NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9010011-010' and iNamLamViec = 2024)
, NULL, NULL, N'STM', NULL, 0, 2024, 1, NULL, NULL, N'NG', N'', N'0003', N'010', N'9010011', NULL, NULL, N'- Ngành Tài chính', N'', N'admin', N'admin', NULL, N'', NULL, N'', N'', N'', N'', N'', NULL, N'9010011-010-0003', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)

GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2024-08-26T11:32:29.5989902' AS DateTime2), CAST(N'2024-08-26T11:31:18.8342156' AS DateTime2), NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9010011-010' and iNamLamViec = 2024)
, NULL, NULL, N'STM', NULL, 0, 2024, 1, NULL, NULL, N'NG', N'', N'0004', N'010', N'9010011', NULL, NULL, N'- Ngành Quân y', N'', N'admin', N'admin', NULL, N'', NULL, N'', N'', N'', N'', N'', NULL, N'9010011-010-0004', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2024-08-26T11:32:29.5989902' AS DateTime2), CAST(N'2024-08-26T11:31:18.8342156' AS DateTime2), NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9010011' and iNamLamViec = 2024)
, NULL, NULL, N'STM', NULL, 0, 2024, 1, NULL, NULL, N'NG', N'', NULL, N'011', N'9010011', NULL, NULL, N'Hỗ trợ cán bộ chuyên trách làm công tác BHXH, BHYT', N'', N'admin', N'admin', NULL, N'', NULL, N'', N'', N'', N'', N'', NULL, N'9010011-011', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2024-08-26T11:32:29.5989902' AS DateTime2), CAST(N'2024-08-26T11:31:18.8342156' AS DateTime2), NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9010011' and iNamLamViec = 2024)
, NULL, NULL, N'STM', NULL, 0, 2024, 1, NULL, NULL, N'NG', N'', NULL, N'012', N'9010011', NULL, NULL, N'Chi tập huấn nghiệp vụ BHXH, BHYT, BHTN', N'', N'admin', N'admin', NULL, N'', NULL, N'', N'', N'', N'', N'', NULL, N'9010011-012', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)

GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 1, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2024-08-26T11:32:29.5989902' AS DateTime2), CAST(N'2024-08-26T11:31:18.8342156' AS DateTime2), NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9010011' and iNamLamViec = 2024)
, NULL, NULL, N'STM', NULL, 0, 2024, 1, NULL, NULL, N'NG', N'', NULL, N'013', N'9010011', NULL, NULL, N'Hỗ trợ quản lý thu, chi BHXH, BHYT ở đơn vị cơ sở', N'', N'admin', N'admin', NULL, N'', NULL, N'', N'', N'', N'', N'', NULL, N'9010011-013', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)

GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 1, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2024-08-26T11:32:29.5989902' AS DateTime2), CAST(N'2024-08-26T11:31:18.8342156' AS DateTime2), NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9010011-013' and iNamLamViec = 2024)
, NULL, NULL, N'STM', NULL, 0, 2024, 1, NULL, NULL, N'NG', N'', N'0001', N'013', N'9010011', NULL, NULL, N'Chi thường xuyên đặc thù', N'', N'admin', N'admin', NULL, N'', NULL, N'', N'', N'', N'', N'', NULL, N'9010011-013-0001', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)

GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2024-08-26T11:32:29.5989902' AS DateTime2), CAST(N'2024-08-26T11:31:18.8342156' AS DateTime2), NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9010011-013-0001' and iNamLamViec = 2024)
, NULL, NULL, N'STM', NULL, 0, 2024, 1, NULL, NULL, N'NG', N'', N'0001', N'013', N'9010011', N'0001', NULL, N'- Ngành Cán bộ', N'', N'admin', N'admin', NULL, N'', NULL, N'', N'', N'', N'', N'', NULL, N'9010011-013-0001-0001', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)

GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2024-08-26T11:32:29.5989902' AS DateTime2), CAST(N'2024-08-26T11:31:18.8342156' AS DateTime2), NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9010011-013-0001' and iNamLamViec = 2024)
, NULL, NULL, N'STM', NULL, 0, 2024, 1, NULL, NULL, N'NG', N'', N'0001', N'013', N'9010011', N'0002', NULL, N'- Ngành Quân lực', N'', N'admin', N'admin', NULL, N'', NULL, N'', N'', N'', N'', N'', NULL, N'9010011-013-0001-0002', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)

GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2024-08-26T11:32:29.5989902' AS DateTime2), CAST(N'2024-08-26T11:31:18.8342156' AS DateTime2), NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9010011-013-0001' and iNamLamViec = 2024)
, NULL, NULL, N'STM', NULL, 0, 2024, 1, NULL, NULL, N'NG', N'', N'0001', N'013', N'9010011', N'0003', NULL, N'- Ngành Tài chính', N'', N'admin', N'admin', NULL, N'', NULL, N'', N'', N'', N'', N'', NULL, N'9010011-013-0001-0003', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)

GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2024-08-26T11:32:29.5989902' AS DateTime2), CAST(N'2024-08-26T11:31:18.8342156' AS DateTime2), NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9010011-013-0001' and iNamLamViec = 2024)
, NULL, NULL, N'STM', NULL, 0, 2024, 1, NULL, NULL, N'NG', N'', N'0001', N'013', N'9010011', N'0004', NULL, N'- Ngành Quân y', N'', N'admin', N'admin', NULL, N'', NULL, N'', N'', N'', N'', N'', NULL, N'9010011-013-0001-0004', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)

GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2024-08-26T11:32:29.5989902' AS DateTime2), CAST(N'2024-08-26T11:31:18.8342156' AS DateTime2), NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9010011-013' and iNamLamViec = 2024)
, NULL, NULL, N'STM', NULL, 0, 2024, 1, NULL, NULL, N'NG', N'', N'0002', N'013', N'9010011', NULL, NULL, N'Chi không thường xuyên', N'', N'admin', N'admin', NULL, N'', NULL, N'', N'', N'', N'', N'', NULL, N'9010011-013-0002', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)

--2025
GO
DELETE FROM BH_DM_MucLucNganSach WHERE sxaunoima in ('9010011','9010011-010','9010011-010-0001','9010011-010-0002','9010011-010-0003','9010011-010-0004','9010011-011','9010011-012','9010011-013','9010011-013-0001','9010011-013-0001-0001','9010011-013-0001-0002','9010011-013-0001-0003','9010011-013-0001-0004','9010011-013-0002')
	AND iNamLamViec = 2025
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 1, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2025-08-26T11:32:29.5989902' AS DateTime2), CAST(N'2025-08-26T11:31:18.8342156' AS DateTime2), NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9' and iNamLamViec = 2025)
, NULL, NULL, N'STM', NULL, 0, 2025, 1, NULL, NULL, N'NG', N'', NULL, NULL, N'9010011', NULL, NULL, N'Chi kinh phí quản lý BHXH, BHYT (giao chi tiết cho đơn vị)', N'', N'admin', N'admin', NULL, N'', NULL, N'', N'', N'', N'', N'', NULL, N'9010011', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)

GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 1, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2025-08-26T11:32:29.5989902' AS DateTime2), CAST(N'2025-08-26T11:31:18.8342156' AS DateTime2), NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9010011' and iNamLamViec = 2025)
, NULL, NULL, N'STM', NULL, 0, 2025, 1, NULL, NULL, N'NG', N'', NULL, N'010', N'9010011', NULL, NULL, N'Chi quản lý BHXH, BHYT', N'', N'admin', N'admin', NULL, N'', NULL, N'', N'', N'', N'', N'', NULL, N'9010011-010', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)

GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2025-08-26T11:32:29.5989902' AS DateTime2), CAST(N'2025-08-26T11:31:18.8342156' AS DateTime2), NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9010011-010' and iNamLamViec = 2025)
, NULL, NULL, N'STM', NULL, 0, 2025, 1, NULL, NULL, N'NG', N'', N'0001', N'010', N'9010011', NULL, NULL, N'- Ngành Cán bộ', N'', N'admin', N'admin', NULL, N'', NULL, N'', N'', N'', N'', N'', NULL, N'9010011-010-0001', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)

GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2025-08-26T11:32:29.5989902' AS DateTime2), CAST(N'2025-08-26T11:31:18.8342156' AS DateTime2), NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9010011-010' and iNamLamViec = 2025)
, NULL, NULL, N'STM', NULL, 0, 2025, 1, NULL, NULL, N'NG', N'', N'0002', N'010', N'9010011', NULL, NULL, N'- Ngành Quân lực', N'', N'admin', N'admin', NULL, N'', NULL, N'', N'', N'', N'', N'', NULL, N'9010011-010-0002', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)

GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2025-08-26T11:32:29.5989902' AS DateTime2), CAST(N'2025-08-26T11:31:18.8342156' AS DateTime2), NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9010011-010' and iNamLamViec = 2025)
, NULL, NULL, N'STM', NULL, 0, 2025, 1, NULL, NULL, N'NG', N'', N'0003', N'010', N'9010011', NULL, NULL, N'- Ngành Tài chính', N'', N'admin', N'admin', NULL, N'', NULL, N'', N'', N'', N'', N'', NULL, N'9010011-010-0003', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)

GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2025-08-26T11:32:29.5989902' AS DateTime2), CAST(N'2025-08-26T11:31:18.8342156' AS DateTime2), NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9010011-010' and iNamLamViec = 2025)
, NULL, NULL, N'STM', NULL, 0, 2025, 1, NULL, NULL, N'NG', N'', N'0004', N'010', N'9010011', NULL, NULL, N'- Ngành Quân y', N'', N'admin', N'admin', NULL, N'', NULL, N'', N'', N'', N'', N'', NULL, N'9010011-010-0004', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2025-08-26T11:32:29.5989902' AS DateTime2), CAST(N'2025-08-26T11:31:18.8342156' AS DateTime2), NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9010011' and iNamLamViec = 2025)
, NULL, NULL, N'STM', NULL, 0, 2025, 1, NULL, NULL, N'NG', N'', NULL, N'011', N'9010011', NULL, NULL, N'Hỗ trợ cán bộ chuyên trách làm công tác BHXH, BHYT', N'', N'admin', N'admin', NULL, N'', NULL, N'', N'', N'', N'', N'', NULL, N'9010011-011', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2025-08-26T11:32:29.5989902' AS DateTime2), CAST(N'2025-08-26T11:31:18.8342156' AS DateTime2), NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9010011' and iNamLamViec = 2025)
, NULL, NULL, N'STM', NULL, 0, 2025, 1, NULL, NULL, N'NG', N'', NULL, N'012', N'9010011', NULL, NULL, N'Chi tập huấn nghiệp vụ BHXH, BHYT, BHTN', N'', N'admin', N'admin', NULL, N'', NULL, N'', N'', N'', N'', N'', NULL, N'9010011-012', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)

GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 1, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2025-08-26T11:32:29.5989902' AS DateTime2), CAST(N'2025-08-26T11:31:18.8342156' AS DateTime2), NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9010011' and iNamLamViec = 2025)
, NULL, NULL, N'STM', NULL, 0, 2025, 1, NULL, NULL, N'NG', N'', NULL, N'013', N'9010011', NULL, NULL, N'Hỗ trợ quản lý thu, chi BHXH, BHYT ở đơn vị cơ sở', N'', N'admin', N'admin', NULL, N'', NULL, N'', N'', N'', N'', N'', NULL, N'9010011-013', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)

GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 1, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2025-08-26T11:32:29.5989902' AS DateTime2), CAST(N'2025-08-26T11:31:18.8342156' AS DateTime2), NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9010011-013' and iNamLamViec = 2025)
, NULL, NULL, N'STM', NULL, 0, 2025, 1, NULL, NULL, N'NG', N'', N'0001', N'013', N'9010011', NULL, NULL, N'Chi thường xuyên đặc thù', N'', N'admin', N'admin', NULL, N'', NULL, N'', N'', N'', N'', N'', NULL, N'9010011-013-0001', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)

GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2025-08-26T11:32:29.5989902' AS DateTime2), CAST(N'2025-08-26T11:31:18.8342156' AS DateTime2), NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9010011-013-0001' and iNamLamViec = 2025)
, NULL, NULL, N'STM', NULL, 0, 2025, 1, NULL, NULL, N'NG', N'', N'0001', N'013', N'9010011', N'0001', NULL, N'- Ngành Cán bộ', N'', N'admin', N'admin', NULL, N'', NULL, N'', N'', N'', N'', N'', NULL, N'9010011-013-0001-0001', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)

GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2025-08-26T11:32:29.5989902' AS DateTime2), CAST(N'2025-08-26T11:31:18.8342156' AS DateTime2), NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9010011-013-0001' and iNamLamViec = 2025)
, NULL, NULL, N'STM', NULL, 0, 2025, 1, NULL, NULL, N'NG', N'', N'0001', N'013', N'9010011', N'0002', NULL, N'- Ngành Quân lực', N'', N'admin', N'admin', NULL, N'', NULL, N'', N'', N'', N'', N'', NULL, N'9010011-013-0001-0002', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)

GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2025-08-26T11:32:29.5989902' AS DateTime2), CAST(N'2025-08-26T11:31:18.8342156' AS DateTime2), NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9010011-013-0001' and iNamLamViec = 2025)
, NULL, NULL, N'STM', NULL, 0, 2025, 1, NULL, NULL, N'NG', N'', N'0001', N'013', N'9010011', N'0003', NULL, N'- Ngành Tài chính', N'', N'admin', N'admin', NULL, N'', NULL, N'', N'', N'', N'', N'', NULL, N'9010011-013-0001-0003', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)

GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2025-08-26T11:32:29.5989902' AS DateTime2), CAST(N'2025-08-26T11:31:18.8342156' AS DateTime2), NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9010011-013-0001' and iNamLamViec = 2025)
, NULL, NULL, N'STM', NULL, 0, 2025, 1, NULL, NULL, N'NG', N'', N'0001', N'013', N'9010011', N'0004', NULL, N'- Ngành Quân y', N'', N'admin', N'admin', NULL, N'', NULL, N'', N'', N'', N'', N'', NULL, N'9010011-013-0001-0004', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)

GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2025-08-26T11:32:29.5989902' AS DateTime2), CAST(N'2025-08-26T11:31:18.8342156' AS DateTime2), NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9010011-013' and iNamLamViec = 2025)
, NULL, NULL, N'STM', NULL, 0, 2025, 1, NULL, NULL, N'NG', N'', N'0002', N'013', N'9010011', NULL, NULL, N'Chi không thường xuyên', N'', N'admin', N'admin', NULL, N'', NULL, N'', N'', N'', N'', N'', NULL, N'9010011-013-0002', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
