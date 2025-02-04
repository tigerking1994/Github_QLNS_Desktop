/****** Object:  StoredProcedure [dbo].[sp_skt_solieuchitiet_donvi0_chitiet_index_2]    Script Date: 02/11/2023 6:27:23 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_solieuchitiet_donvi0_chitiet_index_2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_solieuchitiet_donvi0_chitiet_index_2]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_get_can_cu_du_toan_lap_du_toan_dau_nam]    Script Date: 02/11/2023 6:27:23 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_get_can_cu_du_toan_lap_du_toan_dau_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_get_can_cu_du_toan_lap_du_toan_dau_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_get_can_cu_du_toan_lap_du_toan_dau_nam]    Script Date: 02/11/2023 6:27:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_skt_get_can_cu_du_toan_lap_du_toan_dau_nam]
	@LoaiChungTu int,
	@ILoai int,
	@IdDonVi varchar(max),
	@NamLamViec int,
	@NamNganSach int,
	@MaNguoiNganSach int
AS
BEGIN
SELECT    iID_MLNS,
          SXauNoiMa,
          sum(fTuChi) TuChi,
          sum(fHangNhap) HangNhap,
          sum(fHangMua) HangMua,
          sum(fPhanCap) PhanCap,
          sum(fTuChi) MuaHangHienVat,
          sum(fTuChi) DacThu
   FROM NS_DT_ChungTuChiTiet ctct
   join NS_DT_ChungTu ct on ct.iID_DTChungTu = ctct.iID_DTChungTu
   WHERE ct.iNamLamViec = @NamLamViec
     AND ct.iNamNganSach = 2
	 and ct.iID_MaNguonNganSach = @MaNguoiNganSach
	 and ct.bKhoa = 1
	 and ct.iLoai = @ILoai
	 and ct.iLoaiDuToan != 2
	 and ct.iLoaiChungTu = @LoaiChungTu
	 and (@ILoai = 0 OR @IdDonVi = '-1' OR ctct.iID_MaDonVi = @IdDonVi)
   GROUP BY iID_MLNS, SXauNoiMa;
END
;
;


GO
/****** Object:  StoredProcedure [dbo].[sp_skt_solieuchitiet_donvi0_chitiet_index_2]    Script Date: 02/11/2023 6:27:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_skt_solieuchitiet_donvi0_chitiet_index_2]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@Loai int,
	@TypeGet int,
	@AgencyId nvarchar(500),
	@LoaiChungTu nvarchar(50),
	@ListChungTuTongHop nvarchar(max),
	@Lns nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;

	select distinct iID_MaDonVi into #listDonVi from NS_DTDauNam_ChungTu where iID_CTDTDauNam IN (select * FROM f_split(@ListChungTuTongHop))
	SELECT DISTINCT NEWID() AS Id,
                NEWID() AS IdDb,
                mlns.iID_MLNS AS MlnsId,
                mlns.iID_MLNS_Cha AS MlnsIdParent,
                mlns.sXauNoiMa AS XauNoiMa,
                mlns.sLNS AS LNS,
                mlns.sL AS L,
                mlns.sK AS K,
                mlns.sM AS M,
                mlns.sTM AS TM,
                mlns.sTTM AS TTM,
                mlns.sNG AS NG,
                mlns.sTNG AS TNG,
                mlns.sTNG1 AS TNG1,
                mlns.sTNG2 AS TNG2,
                mlns.sTNG3 AS TNG3,
                mlns.sMoTa AS MoTa,
                '' AS Chuong,
                mlns.bHangCha,
                mlns.sChiTietToi AS ChiTietToi,
                --chitiet.iID_MaDonVi AS IdDonVi,
                mlns.iID_MaDonVi AS IdDonVi,
                donvi.sTenDonVi AS TenDonVi,
                chitiet.TuChi AS ChiTiet,
				chitiet.UocThucHien,
                chitiet.HangNhap,
                chitiet.HangMua,
                chitiet.PhanCap,
                chitiet.ChuaPhanCap,
                map.sSKT_KyHieu AS SKT_KyHieu,
				mlns.bHangChaDuToan
FROM (select mlns1.iID_MLNS,
		     mlns1.iID_MLNS_Cha,
			 mlns1.sXauNoiMa,
             mlns1.sLNS,
             mlns1.sL,
             mlns1.sK,
             mlns1.sM,
             mlns1.sTM,
             mlns1.sTTM,
             mlns1.sNG,
             mlns1.sTNG,
             mlns1.sTNG1,
             mlns1.sTNG2,
             mlns1.sTNG3,
             mlns1.sMoTa,
             mlns1.bHangCha,
             mlns1.sChiTietToi,
             mlns1.bHangChaDuToan,
             mlns1.iNamLamViec,
			 mlns1.iID_MaDonVi
			 from NS_MucLucNganSach mlns1 where bHangChaDuToan = 1 and iNamLamViec = @YearOfWork
			 union all
	select	 mlns2.iID_MLNS,
		     mlns2.iID_MLNS_Cha,
			 mlns2.sXauNoiMa,
             mlns2.sLNS,
             mlns2.sL,
             mlns2.sK,
             mlns2.sM,
             mlns2.sTM,
             mlns2.sTTM,
             mlns2.sNG,
             mlns2.sTNG,
             mlns2.sTNG1,
             mlns2.sTNG2,
             mlns2.sTNG3,
             mlns2.sMoTa,
             mlns2.bHangCha,
             mlns2.sChiTietToi,
             mlns2.bHangChaDuToan,
             mlns2.iNamLamViec,
			 #listDonVi.iID_MaDonVi
			 from NS_MucLucNganSach mlns2, #listDonVi where bHangChaDuToan = 0 and iNamLamViec = @YearOfWork) mlns 
LEFT JOIN
  (SELECT sXauNoiMa,
          fDuPhong AS DuPhong,
		  fUocThucHien AS UocThucHien,
          fTuChi AS TuChi,
          fHangNhap AS HangNhap,
          fHangMua AS HangMua,
          fPhanCap AS PhanCap,
          fChuaPhanCap AS ChuaPhanCap,
		  iID_MaDonVi
   FROM NS_DTDauNam_ChungTuChiTiet
   WHERE iNamLamViec = @YearOfWork
     AND (iLoai = 3)
     AND (
			(@Loai = 0 AND iID_MaDonVi = @AgencyId)
            OR (@Loai = 1 AND iID_MaDonVi <> @AgencyId AND @ListChungTuTongHop <> '' AND iID_CTDTDauNam IN (select * FROM f_split(@ListChungTuTongHop)))
		 )
     AND iNamNganSach = @YearOfBudget
     AND iID_MaNguonNganSach = @BudgetSource
     AND iLoaiChungTu = @LoaiChungTu
     ) chitiet ON mlns.sXauNoiMa = chitiet.sXauNoiMa AND mlns.iID_MaDonVi = chitiet.iID_MaDonVi
LEFT JOIN (SELECT * from DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1) donvi on donvi.iID_MaDonVi = mlns.iID_MaDonVi

LEFT JOIN 
( select * FROM  NS_MLSKT_MLNS   where iNamLamViec = @YearOfWork
) MAP ON mlns.sXauNoiMa = map.sNS_XauNoiMa  

WHERE mlns.iNamLamViec = @YearOfWork
  --AND (map.iNamLamViec = @YearOfWork
  --     OR mlns.bHangCha =1) 
  AND mlns.bHangChaDuToan IS NOT NULL
	   and(mlns.sLNS = '1'
            OR ((mlns.sLNS like '104%'
                    AND @LoaiChungTu = '2')
                OR (mlns.sLNS not like '104%'
                    AND @LoaiChungTu = '1')))
					AND mlns.sLNS IN (SELECT * from f_split(@Lns))

ORDER BY mlns.sLNS,
         mlns.sL,
         mlns.sK,
         mlns.sM,
         mlns.sTM,
         mlns.sTTM,
         mlns.sNG,
         mlns.sTNG
		 END;
;
;
;
GO
