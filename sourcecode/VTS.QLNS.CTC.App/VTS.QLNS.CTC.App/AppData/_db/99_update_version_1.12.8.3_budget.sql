UPDATE NS_SKT_ChungTu
SET iLoaiNguonNganSach = 1
WHERE iLoaiNguonNganSach IS NULL
AND iLoai = 0

/****** Object:  StoredProcedure [dbo].[sp_skt_nhap_so_nhu_cau_clone]    Script Date: 25/05/2023 5:07:52 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_nhap_so_nhu_cau_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_nhap_so_nhu_cau_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_nhap_so_nhu_cau_clone]    Script Date: 25/05/2023 5:07:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_skt_nhap_so_nhu_cau_clone] 
	@Loai int,
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@LoaiChungTu int,
	@UserName nvarchar(100),
	@loaiNguonNganSach int 

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
		ct.sSoChungTu,
		ct.dNgayChungTu,
		ct.sSoQuyetDinh,
		ct.dNgayQuyetDinh,
		ct.sMoTa,
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
		ct.SDssoChungTuTongHop,
		ct.fTongTuChi,
		ct.fTongPhanCap,
		ct.fTongMuaHangCapHienVat,
		ct.sTenDonVi,
		ct.bDaTongHop,
		ct.iLoaiNguonNganSach
		into #sktChungTu
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
				AND ct.iLoai = @Loai 
				AND ct.iLoaiChungTu = @LoaiChungTu
				AND (@loaiNguonNganSach = 0 OR ct.iLoaiNguonNganSach = @loaiNguonNganSach)   

		) ct;
	--LEFT JOIN 
	--	(
	--		SELECT 
	--			iID_CTSoKiemTra, 
	--			sum(fTuChi) as fTongTuChi,
	--			sum(fPhanCap) as fTongPhanCap,
	--			sum(fMuaHangCapHienVat) as fTongMuaHangCapHienVat
	--		FROM 
	--			NS_SKT_ChungTuChiTiet ctct
	--		LEFT JOIN 
	--			(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi
	--		WHERE 
	--			ctct.iLoai = @Loai
	--			AND ctct.iNamLamViec = @NamLamViec
	--			AND ctct.iNamNganSach = @NamNganSach
	--			AND ctct.iID_MaNguonNganSach = @NguonNganSach
	--			AND 
	--			(
	--				(
	--					(dv.iLoai = '0' OR @LoaiChungTu = 1) 
	--					AND EXISTS (SELECT * FROM NS_SKT_MucLuc WHERE iID_MLSKT = ctct.iID_MLSKT AND iNamLamViec = @NamLamViec AND @LoaiChungTu IN (SELECT * FROM f_split(sLoaiNhap)))
	--				)
	--				OR 
	--				(
	--					EXISTS 
	--					(
	--						SELECT * FROM (SELECT * FROM NS_SKT_MucLuc WHERE iID_MLSKT = ctct.iID_MLSKT AND iNamLamViec = @NamLamViec AND @LoaiChungTu IN (SELECT * FROM f_split(sLoaiNhap))) ml
	--						JOIN DanhMuc dm ON dm.iID_MaDanhMuc = ml.sNG AND dm.sType = 'NS_Nganh' AND dm.sGiaTri = dv.iID_MaDonVi AND dm.iNamLamViec = @NamLamViec
	--					)
	--				)
				
	--			)
	--	GROUP BY iID_CTSoKiemTra
	--	) ctct
	--ON ctct.iID_CTSoKiemTra =  ct.iID_CTSoKiemTra;

	IF @CountDonViCha = 0
		SELECT 
			sktct.*
		FROM #sktChungTu sktct
		INNER JOIN 
			(SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName and iNamLamViec = @NamLamViec and iTrangThai = 1) dv
		ON sktct.iID_MaDonVi = dv.iID_MaDonVi
		ORDER BY sSoChungTu desc;
	ELSE
		SELECT 
			sktct.*
		FROM #sktChungTu sktct
		LEFT JOIN 
			(SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName and iNamLamViec = @NamLamViec and iTrangThai = 1) dv
		ON sktct.iID_MaDonVi = dv.iID_MaDonVi
		WHERE ((dv.iID_MaDonVi IS NULL AND sktct.bKhoa = 1) OR (dv.iID_MaDonVi IS NOT NULL))
		ORDER BY sSoChungTu desc;

	drop table #sktChungTu;
END
;
;
;
GO
