/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chi_tiet]    Script Date: 5/24/2024 4:44:50 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_kht_bhxh_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_kht_bhxh_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_year_mlns_bhxh]    Script Date: 5/24/2024 4:44:50 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_clone_year_mlns_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_clone_year_mlns_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_year_mlns_bhxh]    Script Date: 5/24/2024 4:44:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_clone_year_mlns_bhxh]
	@source int,
	@dest int,
	@userCreate nvarchar(200)
AS
BEGIN
	
	
insert into BH_DM_MucLucNganSach (
	iID,
	sXauNoiMa,
	sLNS,
	sL,
	sK,
	sM,
	sTM,
	sTTM,
	sNG,
	sTNG,
	sMoTa,
	bHangCha,
	iTrangThai,
	bDuPhong,
	bHangChaDuToan,
	bHangChaQuyetToan,
	bHangMua,
	bHangNhap,
	bHienVat,
	bNgay,
	bPhanCap,
	bSoNguoi,
	bTonKho,
	bTuChi,
	sChiTietToi,
	dNgaySua,
	dNgayTao,
	iLoai,
	iLock,
	iID_MaDonVi,
	iID_MaBQuanLy,
	[Log],
	iID_MLNS,
	iID_MLNS_Cha,
	iNamLamViec,
	sCPChiTietToi,
	sDuToanChiTietToi,
	sNguoiSua,
	sNguoiTao,
	sNhapTheoTruong,
	sQuyetToanChiTietToi,
	Tag,
	sTNG1,
	sTNG2,
	sTNG3,
	iLoaiNganSach,
	sMaCB,
	sMaPhuCap,
	bHangChaDuToanDieuChinh,
	sDuToanDieuChinhChiTietToi,
	iDonViTinh,
	fTyLe_BHXH_NSD,
	fTyLe_BHXH_NLD,
	fTyLe_BHYT_NSD,
	fTyLe_BHYT_NLD,
	fTyLe_BHTN_NSD,
	fTyLe_BHTN_NLD,
	sLuongChinh,
	sNS_LuongChinh,
	sNS_PCCV,
	sNS_PCTN,
	sNS_PCTNVK,
	sPCCV,
	sPCTN,
	sPCTNVK,
	sNS_HSBL)
select newid(),
	sXauNoiMa,
	sLNS,
	sL,
	sK,
	sM,
	sTM,
	sTTM,
	sNG,
	sTNG,
	sMoTa,
	bHangCha,
	iTrangThai,
	bDuPhong,
	bHangChaDuToan,
	bHangChaQuyetToan,
	bHangMua,
	bHangNhap,
	bHienVat,
	bNgay,
	bPhanCap,
	bSoNguoi,
	bTonKho,
	bTuChi,
	sChiTietToi,
	dNgaySua,
	getdate(),
	iLoai,
	iLock,
	iID_MaDonVi,
	iID_MaBQuanLy,
	[Log],
	iID_MLNS,
	iID_MLNS_Cha,
	@dest,
	sCPChiTietToi,
	sDuToanChiTietToi,
	sNguoiSua,
	@userCreate,
	sNhapTheoTruong,
	sQuyetToanChiTietToi,
	Tag,
	sTNG1,
	sTNG2,
	sTNG3,
	iLoaiNganSach,
	sMaCB,
	sMaPhuCap,
	bHangChaDuToanDieuChinh,
	sDuToanDieuChinhChiTietToi,
	iDonViTinh,
	fTyLe_BHXH_NSD,
	fTyLe_BHXH_NLD,
	fTyLe_BHYT_NSD,
	fTyLe_BHYT_NLD,
	fTyLe_BHTN_NSD,
	fTyLe_BHTN_NLD,
	sLuongChinh,
	sNS_LuongChinh,
	sNS_PCCV,
	sNS_PCTN,
	sNS_PCTNVK,
	sPCCV,
	sPCTN,
	sPCTNVK,
	sNS_HSBL
from BH_DM_MucLucNganSach
where iNamLamViec = @source and sXauNoiMa not in (select sXauNoiMa from BH_DM_MucLucNganSach where iNamLamViec = @dest)

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chi_tiet]    Script Date: 5/24/2024 4:44:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_kht_bhxh_chi_tiet]
	@KhtBHXHId nvarchar(100),
	@NamLamViec int,
	@MaDonVi nvarchar(100)
AS
BEGIN
SELECT 
		ct.iID_KHT_BHXH as BhKhtBHXHId,
		ct.*
	FROM
		(
			SELECT
				ddv.iID_MaDonVi,
				bh.sMoTa,
				ddv.sTenDonVi,
				bhct.iID_KHT_BHXHChiTiet,
				bhct.iID_KHT_BHXH,
				bhct.iID_LoaiDoiTuong,
				bhct.sTenLoaiDoiTuong,
				bhct.iQSBQNam,
				bhct.fLuongChinh,
				bhct.fPhuCapChucVu,
				bhct.fPCTNNghe,
				bhct.fPCTNVuotKhung,
				bhct.fNghiOm,
				bhct.fHSBL,
				(isnull(bhct.fLuongChinh, 0) + isnull(bhct.fPhuCapChucVu, 0) + isnull(bhct.fPCTNNghe, 0) + isnull(bhct.fPCTNVuotKhung, 0) + isnull(bhct.fNghiOm, 0) + isnull(bhct.fHSBL, 0)) fTongQTLN,
				bhct.fThu_BHXH_NLD,
				bhct.fThu_BHXH_NSD, 
				(isnull(bhct.fThu_BHXH_NLD, 0) + isnull(bhct.fThu_BHXH_NSD, 0)) fTongThuBHXH, 
				bhct.fThu_BHYT_NLD,
				bhct.fThu_BHYT_NSD,
				(isnull(bhct.fThu_BHYT_NLD, 0) + isnull(bhct.fThu_BHYT_NSD, 0)) fTongThuBHYT,
				bhct.fThu_BHTN_NLD,
				bhct.fThu_BHTN_NSD,
				(isnull(bhct.fThu_BHTN_NLD, 0) + isnull(bhct.fThu_BHTN_NSD, 0)) fTongThuBHTN,
				(isnull(bhct.fThu_BHXH_NLD, 0) + isnull(bhct.fThu_BHXH_NSD, 0) + isnull(bhct.fThu_BHYT_NLD, 0) + isnull(bhct.fThu_BHYT_NSD, 0) + isnull(bhct.fThu_BHTN_NLD, 0) + isnull(bhct.fThu_BHTN_NSD, 0)) fTongCong,
				bhct.iID_MucLucNganSach,
				bhct.dNgayTao,
				bhct.dNgaySua,
				bhct.sNguoiTao,
				bhct.sNguoiSua,
				bhct.iNamLamViec,
				bhct.sLNS,
				bhct.sXauNoiMa
			FROM 
				BH_KHT_BHXH bh
			JOIN 
				BH_KHT_BHXH_ChiTiet bhct ON bh.iID_KHT_BHXH = bhct.iID_KHT_BHXH 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bh.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bh.iID_KHT_BHXH = @KhtBHXHId
				and bh.iID_MaDonVi = @MaDonVi
		) ct;

END
;
;
;
GO
