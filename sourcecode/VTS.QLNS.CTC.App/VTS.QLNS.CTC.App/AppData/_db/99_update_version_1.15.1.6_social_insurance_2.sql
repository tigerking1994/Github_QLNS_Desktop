/****** Object:  StoredProcedure [dbo].[sp_dtc_phanbo_get_donvi_bandau_or_bosung]    Script Date: 11/25/2024 5:48:52 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dtc_phanbo_get_donvi_bandau_or_bosung]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dtc_phanbo_get_donvi_bandau_or_bosung]
GO
/****** Object:  StoredProcedure [dbo].[sp_dtc_phanbo_get_donvi]    Script Date: 11/25/2024 5:48:52 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dtc_phanbo_get_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dtc_phanbo_get_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_dtc_phanbo_get_donvi]    Script Date: 11/25/2024 5:48:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dtc_phanbo_get_donvi]
@NamLamViec int,
@IDLoaiChi uniqueidentifier,
@MaLoaichi nvarchar(50),
@IdChungTu nvarchar(4000)
AS BEGIN
SET NOCOUNT ON;

SELECT DISTINCT 
		donvi.iID_DonVi AS Id,
		donvi.iID_MaDonVi as IIDMaDonVi,
		donvi.sTenDonVi as TenDonVi,
		donvi.sKyHieu as KyHieu,
		donvi.sMoTa as MoTa,
		donvi.iLoai as Loai,
		donvi.iNamLamViec as NamLamViec,
		donvi.iTrangThai as iTrangThai

FROM BH_DTC_PhanBoDuToanChi chungtu
INNER JOIN  BH_DTC_PhanBoDuToanChi_ChiTiet  chitiet ON chungtu.ID = chitiet.iID_DTC_PhanBoDuToanChi
INNER JOIN
(
SELECT *
	   FROM DonVi
	   WHERE iNamLamViec = @NamLamViec
) donvi ON donvi.iID_MaDonVi =chitiet.iID_MaDonVi
WHERE chungtu.iNamChungTu = @NamLamViec
  AND chungtu.sID_MaDonVi <> ''
  AND chungtu.sID_MaDonVi IS NOT NULL
 -- AND chungtu.iLoaiDotNhanPhanBo=@DotNhan
  AND chungTu.ID in (select * from f_split(@IdChungTu))

  AND donvi.iNamLamViec = @NamLamViec
  AND donvi.iTrangThai = 1

  AND chitiet.sMaLoaiChi=@MaLoaichi
  AND chitiet.iNamLamViec = @NamLamViec
  AND chitiet.fTienTuChi!=0
	END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dtc_phanbo_get_donvi_bandau_or_bosung]    Script Date: 11/25/2024 5:48:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dtc_phanbo_get_donvi_bandau_or_bosung]
@NamLamViec int,
@IDLoaiChi uniqueidentifier,
@MaLoaichi nvarchar(50),
@IdChungTu nvarchar(4000),
@DotNhan int
AS BEGIN
SET NOCOUNT ON;

SELECT DISTINCT 
		donvi.iID_DonVi AS Id,
		donvi.iID_MaDonVi as IIDMaDonVi,
		donvi.sTenDonVi as TenDonVi,
		donvi.sKyHieu as KyHieu,
		donvi.sMoTa as MoTa,
		donvi.iLoai as Loai,
		donvi.iNamLamViec as NamLamViec,
		donvi.iTrangThai as iTrangThai


FROM BH_DTC_PhanBoDuToanChi chungtu
INNER JOIN  BH_DTC_PhanBoDuToanChi_ChiTiet  chitiet ON chungtu.ID = chitiet.iID_DTC_PhanBoDuToanChi
INNER JOIN
(
SELECT *
	   FROM DonVi
	   WHERE iNamLamViec = @NamLamViec
) donvi ON donvi.iID_MaDonVi =chitiet.iID_MaDonVi
WHERE chungtu.iNamChungTu = @NamLamViec
  AND chungtu.sID_MaDonVi <> ''
  AND chungtu.sID_MaDonVi IS NOT NULL
  AND chungtu.iLoaiDotNhanPhanBo=@DotNhan
  AND chungTu.ID in (select * from f_split(@IdChungTu))

  AND donvi.iNamLamViec = @NamLamViec
  AND donvi.iTrangThai = 1

  AND chitiet.sMaLoaiChi=@MaLoaichi
  AND chitiet.iNamLamViec = @NamLamViec
  AND chitiet.fTienTuChi!=0
	END
;
;
;
;
;
;
GO
