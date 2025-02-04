/****** Object:  StoredProcedure [dbo].[sp_skt_nhan_so_kiem_tra_1]    Script Date: 04/10/2023 2:56:14 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_nhan_so_kiem_tra_1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_nhan_so_kiem_tra_1]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_chung_tu_chi_tiet]    Script Date: 04/10/2023 2:56:14 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_chung_tu_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_chung_tu_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_chung_tu_chi_tiet]    Script Date: 04/10/2023 2:56:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_skt_chung_tu_chi_tiet]
	@Loai int,
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@LoaiNguonNganSach int,
	@LoaiChungTu int,
	@IdDonVi nvarchar(100),
	@BKhoa int
AS
BEGIN
SELECT 
		ct.iID_CTSoKiemTraChiTiet,
		ct.iID_CTSoKiemTra,
		ct.iID_MaDonVi,
		ct.iID_MLSKT,
		ct.sMoTa,
		ct.fHuyDongTonKho,
		ct.fTonKhoDenNgay,
		ct.fThongBaoDonVi,
		ct.fTuChi,
		ct.fTuChiDeNghi,
		ct.sGhiChu,
		ct.iLoai,
		ct.iNamLamViec,
		ct.iNamNganSach,
		ct.iID_MaNguonNganSach,
		ct.dNgayTao,
		ct.sNguoiTao,
		ct.dNgaySua,
		ct.sNguoiSua,
		ct.fHienVat,
		ct.fPhanCap,
		ct.fMuaHangCapHienVat,
		ct.iLoaiChungTu,
		ct.sKyHieu,
		ct.sTenDonVi
	FROM
		(
			SELECT 
				ctct.*
			FROM 
				NS_SKT_ChungTu ct
			JOIN 
				NS_SKT_ChungTuChiTiet ctct ON ct.iID_CTSoKiemTra = ctct.iID_CTSoKiemTra 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON ctct.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE 
				ctct.iNamLamViec = @NamLamViec
				AND ctct.iNamNganSach = @NamNganSach
				AND ctct.iID_MaNguonNganSach = @NguonNganSach
				AND ctct.iLoai = @Loai 
				AND ctct.iLoaiChungTu = @LoaiChungTu
				AND ct.iLoaiNguonNganSach = @LoaiNguonNganSach
				AND (@IdDonVi = '-1' OR ctct.iID_MaDonVi = @IdDonVi)
				AND (@BKhoa = 3 OR ct.bKhoa = @BKhoa) 
		) ct;

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_nhan_so_kiem_tra_1]    Script Date: 04/10/2023 2:56:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_skt_nhan_so_kiem_tra_1]
	@Loai nvarchar(max),
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@LoaiChungTu int,
	@UserName nvarchar(100)
AS
BEGIN
	DECLARE @CountDonViCha int;
	SELECT 
		@CountDonViCha = count(*) FROM (SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName AND iNamLamViec = @NamLamViec AND iTrangThai = 1) nddv
	INNER JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 AND iLoai = 0) dv
	ON dv.iID_MaDonVi = nddv.iID_MaDonVi;

	SELECT 
		ct.iID_CTSoKiemTra,
		ct.iID_MaDonVi,
		ct.SSoChungTu,
		ct.DNgayChungTu,
		ct.SSoQuyetDinh,
		ct.DNgayQuyetDinh,
		ct.SMoTa,
		ct.iLoai,
		ct.bKhoa,
		ct.iNamLamViec,
		ct.iNamNganSach,
		ct.iID_MaNguonNganSach,
		ct.DNgayTao,
		ct.DNgaySua,
		ct.SNguoiTao,
		ct.SNguoiSua,
		ct.iSoChungTuIndex,
		ct.iLoaiChungTu,
		ct.sDSSoChungTuTongHop,
		ctct.fTongTuChi,
		ctct.fTongPhanCap,
		ctct.fTongMuaHangCapHienVat,
		ct.sTenDonVi,
		ct.bDaTongHop,
		ctct.iLoai as iLoai_CTCT,
		ct.iLoaiNguonNganSach
	Into #Temp
	FROM
		(
			SELECT 
				ct.*, ddv.sTenDonVi 
			FROM 
				NS_SKT_ChungTu ct
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON ct.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE 
				ct.iNamLamViec = @NamLamViec 
				AND ct.iNamNganSach = @NamNganSach
				AND ct.iID_MaNguonNganSach = @NguonNganSach
				--AND ct.iLoai = @Loai 
				AND ct.iLoai in (select * from f_split(@Loai)) 
				AND ct.iLoaiChungTu = @LoaiChungTu
				-- AND EXISTS (SELECT * from f_split(ct.iID_MaDonVi) INTERSECT SELECT iID_MaDonVi FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName and iNamLamViec = @NamLamViec and iTrangThai = 1)
				-- AND ((@CountDonViCha = 0 and bKhoa = 1) OR (@CountDonViCha <> 0))

				AND ((EXISTS (SELECT * from f_split(ct.iID_MaDonVi) INTERSECT SELECT iID_MaDonVi FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName and iNamLamViec = @NamLamViec and iTrangThai = 1)
				AND (@CountDonViCha = 0 and bKhoa = 1)) OR (@CountDonViCha <> 0))

		) ct
	LEFT JOIN 
		(
			SELECT 
				iID_CTSoKiemTra,
				sum(fTuChi) as fTongTuChi,
				sum(fPhanCap) as fTongPhanCap,
				sum(fMuaHangCapHienVat) as fTongMuaHangCapHienVat,
				ctct.iLoai as iLoai
			FROM 
				NS_SKT_ChungTuChiTiet ctct
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi
			WHERE 
				--ctct.iLoai = @Loai
				ctct.iLoai in (select * from f_split(@Loai))
				AND ctct.iNamLamViec = @NamLamViec
				AND ctct.iNamNganSach = @NamNganSach
				AND ctct.iID_MaNguonNganSach = @NguonNganSach
				AND 
				(
					(
						(dv.iLoai = '0' OR @LoaiChungTu = 1) 
						AND EXISTS (SELECT * FROM NS_SKT_MucLuc WHERE sKyHieu = ctct.sKyHieu AND iNamLamViec = @NamLamViec AND @LoaiChungTu IN (SELECT * FROM f_split(sLoaiNhap)))
					)
					OR 
					(
						EXISTS 
						(
							SELECT * FROM (SELECT * FROM NS_SKT_MucLuc WHERE sKyHieu = ctct.sKyHieu AND iNamLamViec = @NamLamViec AND @LoaiChungTu IN (SELECT * FROM f_split(sLoaiNhap))) ml
							JOIN DanhMuc dm ON dm.iID_MaDanhMuc = ml.sNG AND dm.sType = 'NS_Nganh' AND dm.sGiaTri = dv.iID_MaDonVi AND dm.iNamLamViec = @NamLamViec
						)
					)
				)
		GROUP BY iID_CTSoKiemTra, ctct.iLoai
		) ctct
	ON ctct.iID_CTSoKiemTra =  ct.iID_CTSoKiemTra;

select * from #Temp
except
select * from #Temp
where iLoai = 3 and iLoai_CTCT = 2 

END
;
;
;
;
;
GO
