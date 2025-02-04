/****** Object:  StoredProcedure [dbo].[sp_mlskt_revert_all]    Script Date: 06/10/2023 11:14:37 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_mlskt_revert_all]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_mlskt_revert_all]
GO
/****** Object:  StoredProcedure [dbo].[sp_mlskt_revert_all]    Script Date: 06/10/2023 11:14:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_mlskt_revert_all]
	@NamLamViec int
AS
BEGIN

	-- Revert lại số ký hiệu cũ
	DELETE FROM NS_SKT_MucLuc WHERE iNamLamViec = 2024;

	INSERT INTO NS_SKT_MucLuc (
		iID,
		bHangCha,
		dNgaySua,
		dNgayTao,
		dNguoiSua,
		dNguoiTao,
		iID_MLSKT,
		iID_MLSKTCha,
		iNamLamViec,
		iTrangThai,
		KyHieuCha,
		[Log],
		Muc,
		sKyHieu,
		sLoaiNhap,
		sM,
		sMoTa,
		sNG_Cha,
		sNG,
		sSTT,
		sSTTBC,
		Tag,
		sKyHieuCu)
	SELECT
		iID,
		bHangCha,
		dNgaySua,
		dNgayTao,
		dNguoiSua,
		dNguoiTao,
		iID_MLSKT,
		iID_MLSKTCha,
		iNamLamViec,
		iTrangThai,
		KyHieuCha,
		[Log],
		Muc,
		sKyHieu,
		sLoaiNhap,
		sM,
		sMoTa,
		sNG_Cha,
		sNG,
		sSTT,
		sSTTBC,
		Tag,
		sKyHieuCu
	FROM ns_skt_mucluc_backup_2024
	WHERE iNamLamViec = 2024;

	-- Revert dữ liệu chứng từ chi tiết
	DELETE FROM NS_SKT_ChungTuChiTiet WHERE iNamLamViec = 2024;

	INSERT INTO NS_SKT_ChungTuChiTiet (
		iID_CTSoKiemTraChiTiet,
		dNgaySua,
		dNgayTao,
		fHienVat,
		fHuyDongTonKho,
		fMuaHangCapHienVat,
		fPhanCap,
		fThongBaoDonVi,
		fTonKhoDenNgay,
		fTuChi,
		fTuChiDeNghi,
		iID_CTSoKiemTra,
		iID_MaDonVi,
		iID_MaNguonNganSach,
		iID_MLSKT,
		iLoai,
		iLoaiChungTu,
		iNamLamViec,
		iNamNganSach,
		sGhiChu,
		sKyHieu,
		sMoTa,
		sNguoiSua,
		sNguoiTao,
		sTenDonVi)
	SELECT
		iID_CTSoKiemTraChiTiet,
		dNgaySua,
		dNgayTao,
		fHienVat,
		fHuyDongTonKho,
		fMuaHangCapHienVat,
		fPhanCap,
		fThongBaoDonVi,
		fTonKhoDenNgay,
		fTuChi,
		fTuChiDeNghi,
		iID_CTSoKiemTra,
		iID_MaDonVi,
		iID_MaNguonNganSach,
		iID_MLSKT,
		iLoai,
		iLoaiChungTu,
		iNamLamViec,
		iNamNganSach,
		sGhiChu,
		sKyHieu,
		sMoTa,
		sNguoiSua,
		sNguoiTao,
		sTenDonVi
	FROM ns_skt_chungtuchitiet_backup_2024
	WHERE iNamLamViec = 2024;

	-- Revert dữ liệu bảng map
	DELETE FROM ns_mlskt_mlns WHERE iNamLamViec = 2024;

	INSERT INTO ns_mlskt_mlns (
		iID_MLSKT_MLNS,
		dNgaySua,
		dNgayTao,
		iNamLamViec,
		iTrangThai,
		[Log],
		sNguoiSua,
		sNguoiTao,
		sNS_XauNoiMa,
		sSKT_KyHieu,
		Tag)
	SELECT
		iID_MLSKT_MLNS,
		dNgaySua,
		dNgayTao,
		iNamLamViec,
		iTrangThai,
		[Log],
		sNguoiSua,
		sNguoiTao,
		sNS_XauNoiMa,
		sSKT_KyHieu,
		Tag
	FROM ns_mlskt_mlns_backup_2024
	WHERE iNamLamViec = 2024;

END
;
GO
