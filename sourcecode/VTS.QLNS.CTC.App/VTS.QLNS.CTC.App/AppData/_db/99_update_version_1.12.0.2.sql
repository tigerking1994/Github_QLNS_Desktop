/****** Object:  StoredProcedure [dbo].[sp_skt_nhan_so_kiem_tra]    Script Date: 04/10/2022 5:19:58 PM ******/
DROP PROCEDURE [dbo].[sp_skt_nhan_so_kiem_tra]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt]    Script Date: 04/10/2022 5:19:58 PM ******/
DROP PROCEDURE [dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt]    Script Date: 04/10/2022 5:19:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt] 
@Loai varchar(MAX),
@IdDonVi varchar(MAX),
@NamLamViec int, 
@Dvt int,
@LoaiChungTu int 
AS 
BEGIN
SELECT DISTINCT mucluc.iID AS Id,
                mucluc.iID_MLSKT AS IDMucLuc,
                mucluc.sSTT AS STT,
                mucluc.sMoTa AS MoTa,
                mucluc.iID_MLSKTCha AS IdParent,
                mucluc.sSTTBC AS STTBC,
                mucluc.sM AS M,
                CAST(0 AS BIT) AS IsHangCha,
                chitiet.*
FROM NS_SKT_MucLuc mucluc
LEFT JOIN
  (SELECT KyHieu,
          QuyetToan =sum(QuyetToan) / @Dvt,
          DuToan =sum(DuToan) / @Dvt,
          TuChi =sum(fTuChi) / @Dvt,
          TuChi2 =sum(TuChi2) / @Dvt
   FROM
     (SELECT mucluc.sKyHieu AS KyHieu,
             QuyetToan =0,
             DuToan =0,
             fTuChi,
             TuChi2 =0
      FROM NS_SKT_ChungTuChiTiet chitiet
      LEFT JOIN NS_SKT_MucLuc mucluc ON chitiet.iID_MLSKT = mucluc.iID_MLSKT AND mucluc.iNamLamViec = @NamLamViec
      WHERE chitiet.iNamLamViec = @NamLamViec
        AND chitiet.iLoai in (select * from f_split(@Loai))
        AND chitiet.iLoaiChungTu = @LoaiChungTu
        AND (@IdDonVi IS NULL
             OR chitiet.iID_MaDonVi in
               (SELECT *
                FROM f_split(@IdDonVi)))
      UNION ALL SELECT dbo.f_get_ky_hieu_by_xaunoima(SKT_XauNoiMa,@NamLamViec) AS KyHieu,
                       QuyetToan,
                       DuToan,
                       TuChi = 0,
                       TuChi2 = 0
      FROM f_skt_cancu(@NamLamViec, @IdDonVi, CAST(@LoaiChungTu AS nvarchar(MAX))) AS a
      WHERE dbo.f_get_ky_hieu_by_xaunoima(SKT_XauNoiMa,@NamLamViec) IS NOT NULL -- lap chi tiet du toan

      UNION ALL SELECT dbo.f_get_ky_hieu_by_xaunoima(XauNoiMa,@NamLamViec) AS KyHieu,
                       QuyetToan =0,
                       DuToan = 0,
                       TuChi = 0,
                       TuChi2 = TuChi
      FROM f_skt_dutoan(@NamLamViec, @IdDonVi, CAST(@LoaiChungTu AS nvarchar(MAX))) AS a
      WHERE dbo.f_get_ky_hieu_by_xaunoima(XauNoiMa,@NamLamViec) IS NOT NULL ) AS a
   GROUP BY KyHieu --order by Tuchi
 ) chitiet ON mucluc.sKyHieu = chitiet.KyHieu
WHERE chitiet.KyHieu IS NOT NULL
  AND mucluc.iNamLamViec = @NamLamViec
ORDER BY sSTTBC END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_nhan_so_kiem_tra]    Script Date: 04/10/2022 5:19:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_skt_nhan_so_kiem_tra]
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
		ct.bDaTongHop
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
				sum(fMuaHangCapHienVat) as fTongMuaHangCapHienVat
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
						AND EXISTS (SELECT * FROM NS_SKT_MucLuc WHERE iID_MLSKT = ctct.iID_MLSKT AND iNamLamViec = @NamLamViec AND @LoaiChungTu IN (SELECT * FROM f_split(sLoaiNhap)))
					)
					OR 
					(
						EXISTS 
						(
							SELECT * FROM (SELECT * FROM NS_SKT_MucLuc WHERE iID_MLSKT = ctct.iID_MLSKT AND iNamLamViec = @NamLamViec AND @LoaiChungTu IN (SELECT * FROM f_split(sLoaiNhap))) ml
							JOIN DanhMuc dm ON dm.iID_MaDanhMuc = ml.sNG AND dm.sType = 'NS_Nganh' AND dm.sGiaTri = dv.iID_MaDonVi AND dm.iNamLamViec = @NamLamViec
						)
					)
				)
		GROUP BY iID_CTSoKiemTra
		) ctct
	ON ctct.iID_CTSoKiemTra =  ct.iID_CTSoKiemTra;
END
;
;
;
GO
