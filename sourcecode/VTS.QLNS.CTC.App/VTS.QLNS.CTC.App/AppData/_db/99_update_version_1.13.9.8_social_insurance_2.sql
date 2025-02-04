/****** Object:  StoredProcedure [dbo].[sp_dtc_phanbochi_intheodotngaychecked_get_donvi]    Script Date: 2/6/2024 10:32:04 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dtc_phanbochi_intheodotngaychecked_get_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dtc_phanbochi_intheodotngaychecked_get_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_dtc_phanbochi_inluykechecked_get_donvi]    Script Date: 2/6/2024 10:32:04 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dtc_phanbochi_inluykechecked_get_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dtc_phanbochi_inluykechecked_get_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_dtc_phanbo_getfor_inluyke_donvi]    Script Date: 2/6/2024 10:32:04 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dtc_phanbo_getfor_inluyke_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dtc_phanbo_getfor_inluyke_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_dtc_phanbo_getfor_dotnhan_donvi]    Script Date: 2/6/2024 10:32:04 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dtc_phanbo_getfor_dotnhan_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dtc_phanbo_getfor_dotnhan_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_dtc_phanbo_get_donvi]    Script Date: 2/6/2024 10:32:04 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dtc_phanbo_get_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dtc_phanbo_get_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitheodieuchinh]    Script Date: 2/6/2024 10:32:04 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_nhandutoanchitheodieuchinh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_nhandutoanchitheodieuchinh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_get_data_dtdc]    Script Date: 2/6/2024 10:32:04 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_get_data_dtdc]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_get_data_dtdc]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_get_data_dtdc]    Script Date: 2/6/2024 10:32:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_get_data_dtdc]
	@NamLamViec int,
	@SoChungTu nvarchar(500),
	@SLNS nvarchar(500),
	@IDLoaiChi nvarchar(500)
AS
BEGIN
	select
		ct.iID_MaDonVi IIdMaDonVi,
		mlns.sXauNoiMa,
		mlns.iID_MLNS IID_MucLucNganSach,
		CASE WHEN (ISNULL(ctct.fTienSoSanhTang,0))-  (ISNULL(ctct.fTienSoSanhGiam,0)) >0 THEN (ISNULL(ctct.fTienSoSanhTang,0))-  (ISNULL(ctct.fTienSoSanhGiam,0))
				ELSE -(((ISNULL(ctct.fTienSoSanhGiam,0))-ISNULL(ctct.fTienSoSanhTang,0))) END FTienTangGiam
		into #temp
	from
	BH_DM_MucLucNganSach mlns
	left join
	BH_DTC_DieuChinhDuToanChi_ChiTiet ctct on ctct.iID_MucLucNganSach = mlns.iID_MLNS
	join
	(select * from BH_DTC_DieuChinhDuToanChi
		where iNamLamViec = @NamLamViec
		and iID_LoaiCap=@IDLoaiChi
		and sSoChungTu in ((SELECT * FROM f_split(@SoChungTu)))) ct on ctct.iID_BH_DTC = ct.iID_BH_DTC
	where mlns.iNamLamViec=@NamLamViec


	if	@SLNS='9010001,9010002'
		select SUBSTRING(A.sXauNoiMa,1,20) sXauNoiMa ,A.IIdMaDonVi, 
		SUM(A.FTienTangGiam) FTienTangGiam
		from #temp A
		Group by SUBSTRING(A.sXauNoiMa,1,20),A.IIdMaDonVi
		order by SUBSTRING(A.sXauNoiMa,1,20)
	else if @SLNS='905,9050001,9050002'
	select SUBSTRING(A.sXauNoiMa,1,3) sXauNoiMa ,A.IIdMaDonVi, 
		SUM(A.FTienTangGiam) FTienTangGiam
		from #temp A
		Group by SUBSTRING(A.sXauNoiMa,1,3),A.IIdMaDonVi
		order by SUBSTRING(A.sXauNoiMa,1,3)
	else if @SLNS='9010004'
	select SUBSTRING(A.sXauNoiMa,1,7) sXauNoiMa ,A.IIdMaDonVi, 
		SUM(A.FTienTangGiam) FTienTangGiam
		from #temp A
		Group by SUBSTRING(A.sXauNoiMa,1,7),A.IIdMaDonVi
		order by SUBSTRING(A.sXauNoiMa,1,7)
	else if @SLNS='9010006'
	select SUBSTRING(A.sXauNoiMa,1,7) sXauNoiMa ,A.IIdMaDonVi, 
		SUM(A.FTienTangGiam) FTienTangGiam
		from #temp A
		Group by SUBSTRING(A.sXauNoiMa,1,7),A.IIdMaDonVi
		order by SUBSTRING(A.sXauNoiMa,1,7)
	else 
	select SUBSTRING(A.sXauNoiMa,1,7) sXauNoiMa ,A.IIdMaDonVi, 
		SUM(A.FTienTangGiam) FTienTangGiam
		from #temp A
		Group by SUBSTRING(A.sXauNoiMa,1,7),A.IIdMaDonVi
		order by SUBSTRING(A.sXauNoiMa,1,7)
	--select dm.* , tbl.fTienKeHoachThucHienNamNay, tbl.IIdMaDonVi from BH_DM_MucLucNganSach  dm
	--left join 
	--#temp tbl on dm.iID_MLNS=tbl.IID_MucLucNganSach
	--where iNamLamViec=@NamLamViec and sLNS in (select * from splitstring(@SLNS))
	--order by dm.sXauNoiMa 

	drop table #temp

END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitheodieuchinh]    Script Date: 2/6/2024 10:32:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bhxh_nhandutoanchitheodieuchinh]
@iDNdtctg uniqueidentifier,
@sLns nvarchar(max),
@NamLamViec int,
@IIDDonVi nvarchar(50),
@IDLoaiChi nvarchar(100)

AS
BEGIN

SELECT 
	A.sLNS,
	A.sTM,
	A.sTTM,
	A.sM,
	A.sNG,
	A.sMoTa AS sNoiDung,
	A.sXauNoiMa,
	A.iID_MLNS,
	A.iID_MLNS_Cha,
	A.bHangCha,
	A.bHangCha as isHangCha,
	B.iID_DTC_DuToanChiTrenGiao,
	A.iID_MLNS AS iID_MucLucNganSach,
	B.FTienTangGiam as fTienTuChi,
	A.iNamlamViec,
	A.sCPChiTietToi,
	A.sDuToanChiTietToi,
	@IIDDonVi as iID_MaDonVi
	FROM BH_DM_MucLucNganSach AS A
	LEFT JOIN (
		SELECT 	
			@iDNdtctg as iID_DTC_DuToanChiTrenGiao
			, ctct.iID_MucLucNganSach
			, ctct.sNoiDung
			, ctct.fTienSoSanhTang
			, ctct.fTienSoSanhGiam
			, CASE WHEN  SUM(ISNULL(ctct.fTienSoSanhTang,0))-   SUM(ISNULL(ctct.fTienSoSanhGiam,0)) >0 THEN SUM(ISNULL(ctct.fTienSoSanhTang,0))-   SUM(ISNULL(ctct.fTienSoSanhGiam,0)) 
				ELSE - ( SUM(ISNULL(ctct.fTienSoSanhGiam,0))-SUM(ISNULL(ctct.fTienSoSanhTang,0))) END FTienTangGiam
		FROM BH_DTC_DieuChinhDuToanChi_ChiTiet ctct
	LEFT JOIN BH_DTC_DieuChinhDuToanChi ct on ctct.iID_BH_DTC=ct.iID_BH_DTC
	WHERE (ct.iLoaiTongHop=2 ) and ct.iID_MaDonVi=@IIDDonVi
		AND ct.iNamLamViec=@NamLamViec
		AND ct.bIsKhoa=1
		AND ct.iID_LoaiCap=@IDLoaiChi
		GROUP BY ctct.iID_MucLucNganSach, ctct.sNoiDung, ctct.fTienSoSanhTang
			, ctct.fTienSoSanhGiam) AS B
	ON B.iID_MucLucNganSach = A.iID_MLNS
	WHERE   A.sLNS IN (SELECT * FROM f_split(@sLns))
		AND A.iNamlamViec=@NamLamViec
	order by A.sXauNoiMa
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dtc_phanbo_get_donvi]    Script Date: 2/6/2024 10:32:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dtc_phanbo_get_donvi]
@NamLamViec int,
@IDLoaiChi uniqueidentifier,
@MaLoaichi nvarchar(50),
@SoQuyetDinh nvarchar(50),
@DNgayQuyetDinh nvarchar(50),
@DotNhan int
AS BEGIN
SET NOCOUNT ON;

SELECT 
	donvi.iID_DonVi AS Id,
	donvi.iID_MaDonVi as IIDMaDonVi,
	donvi.sTenDonVi as TenDonVi,
	donvi.sKyHieu as KyHieu,
	donvi.sMoTa as MoTa,
	donvi.iLoai as Loai,
	donvi.iNamLamViec as NamLamViec,
	donvi.iTrangThai as iTrangThai,
	donvi.dNgayTao as DNgayTao,
	donvi.sNguoiTao as SNguoiTao,
	donvi.dNgaySua as DNgaySua,
	donvi.dNgaySua as SNguoiSua,
	donvi.*

FROM BH_DTC_PhanBoDuToanChi chungtu
LEFT JOIN DonVi donvi ON donvi.iID_MaDonVi in (SELECT * FROM splitstring(chungtu.sID_MaDonVi))
WHERE chungtu.iNamChungTu = @NamLamViec
  AND chungtu.sID_MaDonVi <> ''
  AND chungtu.sID_MaDonVi IS NOT NULL
  AND donvi.iNamLamViec = @NamLamViec
  AND chungtu.sSoQuyetDinh=@SoQuyetDinh
   AND CONVERT(nvarchar ,chungtu.dNgayQuyetDinh,103) = @DNgayQuyetDinh 
   AND chungtu.iLoaiDotNhanPhanBo=@DotNhan
  --AND chungtu.iID_LoaiDanhMucChi=@IDLoaiChi
  AND chungtu.sMaLoaiChi=@MaLoaichi
  --AND chungtu.iLoaiTongHop=@LoaiChungTu
ORDER BY cast(chungtu.dNgayChungTu AS date) END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dtc_phanbo_getfor_dotnhan_donvi]    Script Date: 2/6/2024 10:32:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dtc_phanbo_getfor_dotnhan_donvi]
@NamLamViec int,
@IDLoaiChi uniqueidentifier,
@MaLoaichi nvarchar(50),
--@SoQuyetDinh nvarchar(50),
@DNgayQuyetDinh nvarchar(50)
AS BEGIN
SET NOCOUNT ON;

SELECT 
	donvi.iID_DonVi AS Id,
	donvi.iID_MaDonVi as IIDMaDonVi,
	donvi.sTenDonVi as TenDonVi,
	donvi.sKyHieu as KyHieu,
	donvi.sMoTa as MoTa,
	donvi.iLoai as Loai,
	donvi.iNamLamViec as NamLamViec,
	donvi.iTrangThai as iTrangThai,
	donvi.dNgayTao as DNgayTao,
	donvi.sNguoiTao as SNguoiTao,
	donvi.dNgaySua as DNgaySua,
	donvi.dNgaySua as SNguoiSua,
	donvi.*

FROM BH_DTC_PhanBoDuToanChi chungtu
LEFT JOIN DonVi donvi ON donvi.iID_MaDonVi in (SELECT * FROM splitstring(chungtu.sID_MaDonVi))
WHERE chungtu.iNamChungTu = @NamLamViec
  AND chungtu.sID_MaDonVi <> ''
  AND chungtu.sID_MaDonVi IS NOT NULL
  AND donvi.iNamLamViec = @NamLamViec
 -- AND chungtu.sSoQuyetDinh=@SoQuyetDinh
  AND CONVERT(nvarchar ,chungtu.dNgayQuyetDinh,103) <= @DNgayQuyetDinh 
  --AND chungtu.iID_LoaiDanhMucChi=@IDLoaiChi
  AND chungtu.sMaLoaiChi=@MaLoaichi
  --AND chungtu.iLoaiTongHop=@LoaiChungTu
ORDER BY cast(chungtu.dNgayChungTu AS date) END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dtc_phanbo_getfor_inluyke_donvi]    Script Date: 2/6/2024 10:32:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_dtc_phanbo_getfor_inluyke_donvi]
@NamLamViec int,
@IDLoaiChi uniqueidentifier,
@MaLoaichi nvarchar(50),
@SoQuyetDinh nvarchar(50),
@SNgayQuyetDinh nvarchar(50)
AS BEGIN
SET NOCOUNT ON;

SELECT 
	donvi.iID_DonVi AS Id,
	donvi.iID_MaDonVi as IIDMaDonVi,
	donvi.sTenDonVi as TenDonVi,
	donvi.sKyHieu as KyHieu,
	donvi.sMoTa as MoTa,
	donvi.iLoai as Loai,
	donvi.iNamLamViec as NamLamViec,
	donvi.iTrangThai as iTrangThai,
	donvi.dNgayTao as DNgayTao,
	donvi.sNguoiTao as SNguoiTao,
	donvi.dNgaySua as DNgaySua,
	donvi.dNgaySua as SNguoiSua,
	donvi.*

FROM BH_DTC_PhanBoDuToanChi chungtu
LEFT JOIN DonVi donvi ON donvi.iID_MaDonVi in (SELECT * FROM splitstring(chungtu.sID_MaDonVi))
WHERE chungtu.iNamChungTu = @NamLamViec
  AND chungtu.sID_MaDonVi <> ''
  AND chungtu.sID_MaDonVi IS NOT NULL
  AND donvi.iNamLamViec = @NamLamViec
  AND chungtu.sSoQuyetDinh=@SoQuyetDinh
  AND CAST(chungtu.dNgayQuyetDinh AS date) <= @SNgayQuyetDinh
  --AND cast(chungtu.dNgayQuyetDinh AS date) <= @SNgayQuyetDinh)
  --AND chungtu.iID_LoaiDanhMucChi=@IDLoaiChi
  AND chungtu.sMaLoaiChi=@MaLoaichi
  --AND chungtu.iLoaiTongHop=@LoaiChungTu
ORDER BY cast(chungtu.dNgayChungTu AS date) END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dtc_phanbochi_inluykechecked_get_donvi]    Script Date: 2/6/2024 10:32:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dtc_phanbochi_inluykechecked_get_donvi]
@NamLamViec int,
@IDLoaiChi uniqueidentifier,
@MaLoaichi nvarchar(50),
@SoQuyetDinh nvarchar(50),
@DNgayQuyetDinh nvarchar(50),
@DotNhan int
AS BEGIN
SET NOCOUNT ON;

SELECT 
	donvi.iID_DonVi AS Id,
	donvi.iID_MaDonVi as IIDMaDonVi,
	donvi.sTenDonVi as TenDonVi,
	donvi.sKyHieu as KyHieu,
	donvi.sMoTa as MoTa,
	donvi.iLoai as Loai,
	donvi.iNamLamViec as NamLamViec,
	donvi.iTrangThai as iTrangThai,
	donvi.dNgayTao as DNgayTao,
	donvi.sNguoiTao as SNguoiTao,
	donvi.dNgaySua as DNgaySua,
	donvi.dNgaySua as SNguoiSua,
	donvi.*

FROM BH_DTC_PhanBoDuToanChi chungtu
LEFT JOIN DonVi donvi ON donvi.iID_MaDonVi in (SELECT * FROM splitstring(chungtu.sID_MaDonVi))
WHERE chungtu.iNamChungTu = @NamLamViec
  AND chungtu.sID_MaDonVi <> ''
  AND chungtu.sID_MaDonVi IS NOT NULL
  AND donvi.iNamLamViec = @NamLamViec
 -- AND chungtu.sSoQuyetDinh=@SoQuyetDinh -- ?? lấy lũy kế sao lại gán bằng Số quyết định dungnv719 sửa.
   AND( CONVERT(nvarchar ,chungtu.dNgayQuyetDinh,103) <= @DNgayQuyetDinh 
       )
	And chungtu.iLoaiDotNhanPhanBo=@DotNhan
  --AND chungtu.iID_LoaiDanhMucChi=@IDLoaiChi
  AND chungtu.sMaLoaiChi=@MaLoaichi
  --AND chungtu.iLoaiTongHop=@LoaiChungTu
ORDER BY cast(chungtu.dNgayChungTu AS date) END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dtc_phanbochi_intheodotngaychecked_get_donvi]    Script Date: 2/6/2024 10:32:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dtc_phanbochi_intheodotngaychecked_get_donvi]
@NamLamViec int,
@IDLoaiChi uniqueidentifier,
@MaLoaichi nvarchar(50),
@SoQuyetDinh nvarchar(50),
@DNgayQuyetDinh nvarchar(50),
@DotNhan int 
AS BEGIN
SET NOCOUNT ON;

SELECT 
	donvi.iID_DonVi AS Id,
	donvi.iID_MaDonVi as IIDMaDonVi,
	donvi.sTenDonVi as TenDonVi,
	donvi.sKyHieu as KyHieu,
	donvi.sMoTa as MoTa,
	donvi.iLoai as Loai,
	donvi.iNamLamViec as NamLamViec,
	donvi.iTrangThai as iTrangThai,
	donvi.dNgayTao as DNgayTao,
	donvi.sNguoiTao as SNguoiTao,
	donvi.dNgaySua as DNgaySua,
	donvi.dNgaySua as SNguoiSua,
	donvi.*

FROM BH_DTC_PhanBoDuToanChi chungtu
LEFT JOIN DonVi donvi ON donvi.iID_MaDonVi in (SELECT * FROM splitstring(chungtu.sID_MaDonVi))
WHERE chungtu.iNamChungTu = @NamLamViec
  AND chungtu.sID_MaDonVi <> ''
  AND chungtu.sID_MaDonVi IS NOT NULL
  AND donvi.iNamLamViec = @NamLamViec
  AND chungtu.sSoQuyetDinh=@SoQuyetDinh
   AND CONVERT(nvarchar ,chungtu.dNgayQuyetDinh,103) = @DNgayQuyetDinh 
   and chungtu.iLoaiDotNhanPhanBo=@DotNhan
  --AND chungtu.iID_LoaiDanhMucChi=@IDLoaiChi
  AND chungtu.sMaLoaiChi=@MaLoaichi
  --AND chungtu.iLoaiTongHop=@LoaiChungTu
ORDER BY cast(chungtu.dNgayChungTu AS date) END
;
;
;
GO
