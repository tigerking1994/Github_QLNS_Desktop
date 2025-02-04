/****** Object:  StoredProcedure [dbo].[sp_skt_mucluc_index_chungtu_bvtc]    Script Date: 10/10/2023 6:16:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_mucluc_index_chungtu_bvtc]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_mucluc_index_chungtu_bvtc]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_mucluc_index_chungtu_bvtc]    Script Date: 10/10/2023 6:16:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_skt_mucluc_index_chungtu_bvtc]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetOfSource int,
	@VoucherId nvarchar(max),
	@Loai nvarchar(max),
	@LoaiChungTu int,
	@AgencyId nvarchar(500)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT mucluc.iID AS Id,
       mucluc.iID_MLSKT AS IdMucLuc,
       mucluc.sKyHieu AS KyHieu,
       mucluc.sM AS M,
       mucluc.sSTT AS STT,
       mucluc.sMoTa AS MoTa,
       mucluc.bHangCha ,
       mucluc.iNamLamViec AS NamLamViec,
       mucluc.dNgayTao AS DateCreated,
       mucluc.dNguoiTao AS UserCreator,
       mucluc.dNgaySua AS DateModified,
       mucluc.dNguoiSua AS UserModifier,
       mucluc.Muc,
       '' AS LNS,
       mucluc.iID_MLSKTCha AS IdParent ,
       datachitiet.TuChi ,
       ISNULL(datachitiet.HangMua, 0) AS HangMua ,
       ISNULL(datachitiet.HangNhap, 0) AS HangNhap ,
       ISNULL(datachitiet.PhanCap, 0) AS PhanCap ,
       ISNULL(datachitiet.MuaHangHienVat, 0) AS MuaHangHienVat ,
       ISNULL(datachitiet.DacThu, 0) AS DacThu,

	   ISNULL(dutoandaunam.TuChi, 0) AS DtTuChi,
	   ISNULL(dutoandaunam.HangNhap, 0) AS DtHangNhap,
	   ISNULL(dutoandaunam.HangMua, 0) AS DtHangMua,
	   ISNULL(dutoandaunam.PhanCap, 0) AS DtPhanCap,
	   ISNULL(dutoandaunam.DuPhong, 0) AS DtDuPhong,
	   ISNULL(dutoandaunam.ChuaPhanCap, 0) AS DtChuaPhanCap
FROM NS_SKT_MucLuc mucluc
LEFT JOIN
  (SELECT SUM(fTuChi) AS TuChi,
          CAST(0 AS FLOAT) AS HangMua,
          CAST(0 AS FLOAT) AS HangNhap,
          SUM(fPhanCap) AS PhanCap,
          SUM(fMuaHangCapHienVat) AS MuaHangHienVat,
          SUM(fPhanCap) AS DacThu,
          sKyHieu
   FROM NS_SKT_ChungTuChiTiet as chitiet
   inner join NS_SKT_ChungTu as chungtu on chitiet.iID_CTSoKiemTra = chungtu.iID_CTSoKiemTra
   WHERE chitiet.iNamLamViec = @YearOfWork
     AND chitiet.iNamNganSach = @YearOfBudget
	 AND chitiet.iID_MaNguonNganSach = @BudgetOfSource
     AND chitiet.iLoaiChungTu = @LoaiChungTu
     AND chitiet.iLoai in (select * from f_split(@loai))
     AND chitiet.iID_MaDonVi = @AgencyId
	 AND chungtu.bKhoa = 1
   GROUP BY sKyHieu) datachitiet ON mucluc.sKyHieu = datachitiet.sKyHieu

LEFT JOIN 
	(
	select 
	SUM(chitiet.fTuChi) AS TuChi, 
	SUM(chitiet.fHangNhap) AS HangNhap,
	SUM(chitiet.fHangMua) AS HangMua,
	SUM(chitiet.fPhanCap) AS PhanCap,
	SUM(chitiet.fDuPhong) AS DuPhong,
	SUM(chitiet.fChuaPhanCap) AS ChuaPhanCap,
	mucluc.sKyHieu

	FROM NS_DTDauNam_ChungTuChiTiet chitiet
	left join (select * FROM NS_MLSKT_MLNS where iNamLamViec = @YearOfWork) map on chitiet.sXauNoiMa = map.sNS_XauNoiMa
	left join (select * FROM NS_SKT_MucLuc where iNamLamViec = @YearOfWork) mucluc on map.sSKT_KyHieu = mucluc.sKyHieu
	where
	chitiet.iNamLamViec = @YearOfWork
     AND chitiet.iNamNganSach = @YearOfBudget
	 AND chitiet.iID_MaNguonNganSach = @BudgetOfSource
     AND chitiet.iLoaiChungTu = @LoaiChungTu
	 AND chitiet.iID_MaDonVi = @AgencyId
	 AND chitiet.iID_CTDTDauNam <> @VoucherId
	 AND mucluc.sKyHieu is not null
	group by mucluc.sKyHieu
	) dutoandaunam on dutoandaunam.sKyHieu = mucluc.sKyHieu

WHERE mucluc.iNamLamViec = @YearOfWork
  AND mucluc.iTrangThai = 1
ORDER BY mucluc.sKyHieu
END
;
;
GO
