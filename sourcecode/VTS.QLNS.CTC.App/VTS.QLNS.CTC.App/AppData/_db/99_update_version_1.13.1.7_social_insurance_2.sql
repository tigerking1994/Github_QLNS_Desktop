/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_qlkp_chungtu_tonghop_bhxh]    Script Date: 9/15/2023 10:49:30 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_khc_qlkp_chungtu_tonghop_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_khc_qlkp_chungtu_tonghop_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_chungtu_tonghop_bhxh]    Script Date: 9/15/2023 10:49:30 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_khc_chungtu_tonghop_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_khc_chungtu_tonghop_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_phanbo_dieuchinh_chi_tiet]    Script Date: 9/15/2023 10:49:30 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_phanbo_dieuchinh_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_phanbo_dieuchinh_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_chi_tiet]    Script Date: 9/15/2023 10:49:30 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_dieuchinh_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_dieuchinh_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_get_donvi_tonghop_bh]    Script Date: 9/15/2023 10:49:30 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_get_donvi_tonghop_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_get_donvi_tonghop_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_get_donvi_tonghop_bh]    Script Date: 9/15/2023 10:49:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay don vi cua chung tu tổng hop cap phat chi tiet theo id
-- =============================================
Create PROCEDURE [dbo].[sp_cp_get_donvi_tonghop_bh]
	@YearOfWork int,
	@CapPhatIds nvarchar(max)
AS
BEGIN
	SELECT dv.* 
	FROM 
	(
		SELECT DISTINCT sID_MaDonVi as iID_MaDonVi
		FROM BH_CP_ChungTu 
		WHERE
			iID_CP_ChungTu IN (SELECT * FROM f_split(@CapPhatIds))
			AND iLoaiTongHop=2
	) ctct
	LEFT JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork) dv
	on dv.iID_MaDonVi = ctct.iID_MaDonVi
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_chi_tiet]    Script Date: 9/15/2023 10:49:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_dt_dieuchinh_chi_tiet]
	@NamLamViec int,
	@IdDonVi nvarchar(100)
AS
BEGIN
SELECT 
		ct.iID_BH_DTC_ChiTiet ,
		ct.iID_BH_DTC ,
		ct.iID_MaDonVi,
		ct.iID_MucLucNganSach,
		ct.sM,
		ct.sTM,
		ct.sNoiDung,
		ct.fTienDuToanDuocGiao,
		ct.fTienThucHien06ThangDauNam,
		ct.fTienUocThucHien06ThangCuoiNam,
		ct.fTienUocThucHienCaNam,
		ct.fTienSoSanhTang,
		ct.fTienSoSanhGiam,
		ct.sTenDonVi,
		ct.dNgaySua,
		ct.dNgayTao,
		ct.sNguoiSua,
		ct.sNguoiTao,
		ct.sGhiChu
	FROM
		(
			SELECT
				bh.iID_MaDonVi,
				bh.sMoTa,
				ddv.sTenDonVi,
				bhct.*
			FROM 
				BH_DTC_DieuChinhDuToanChi bh
			JOIN 
				BH_DTC_DieuChinhDuToanChi_ChiTiet bhct ON bh.iID_BH_DTC = bhct.iID_BH_DTC 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bh.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bh.iID_MaDonVi = @IdDonVi
				--AND (@BKhoa = 3 OR bh.bIsKhoa = @BKhoa) 
		) ct;

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_phanbo_dieuchinh_chi_tiet]    Script Date: 9/15/2023 10:49:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_dt_phanbo_dieuchinh_chi_tiet]
	@NamLamViec int,
	@IdDonVi nvarchar(100),
	@loaiDanhMucCapChi nvarchar(100),
	@ngayChungTu date
AS
BEGIN
SELECT 
		ct.ID ,
		ct.iID_DTC_PhanBoDuToanChi ,
		ct.iID_DonVi,
		ct.iID_MaDonVi,
		ct.iID_MucLucNganSach,
		ct.iID_DTC_DuToanChiTrenGiao,
		ct.sLNS,
		ct.sM,
		ct.sTM,
		ct.sTTM,
		ct.sNG,
		ct.sNoiDung,
		ct.iID_LoaiCap,
		ISNULL(ct.fTongTien, 0) as FTongTien,
		ISNULL(ct.fTienTuChi, 0) as FTienTuChi,
		ISNULL(ct.fTienHienVat, 0) as FTienHienVat,
		ct.dNgaySua,
		ct.dNgayTao,
		ct.sNguoiSua,
		ct.sNguoiTao,
		ct.sGhiChu
	FROM
		(
			SELECT
				bh.sMoTa,
				ddv.sTenDonVi,
				bhct.*
			FROM 
				BH_DTC_PhanBoDuToanChi bh
			JOIN 
				BH_DTC_PhanBoDuToanChi_ChiTiet bhct ON bh.ID = bhct.iID_DTC_PhanBoDuToanChi 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bhct.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bhct.iID_MaDonVi = @IdDonVi
				and bh.bIsKhoa = 1
				AND bh.sSoQuyetDinh IS NOT NULL
				AND bh.sSoQuyetDinh <> ''
				AND cast(bh.DngayChungTu as date) <= cast(@ngayChungTu as date)
				AND iID_LoaiDanhMucChi=@loaiDanhMucCapChi
		) ct;

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_chungtu_tonghop_bhxh]    Script Date: 9/15/2023 10:49:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_khc_chungtu_tonghop_bhxh]
	@listTenDonVi ntext,
	@namLamViec int,
	@iLoaiTongHop int
AS
BEGIN
declare @DataDuToan table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), sM nvarchar(50),sLNSDT nvarchar(50), iSoDaThucHienNamTruocDT int, iSoUocThucHienNamTruocDT int, iSoKeHoachThucHienNamNayDT int, iSoSQDT int, iSoQNCNDT float,iSoCNVQPDT int,iSoLDHDDT int,  iSoHSQBSDT int,
fTienDaThucHienNamTruocDT float,fTienUocThucHienNamTruocDT float, fTienKeHoachThucHienNamNayDT float,fTienSQDT float,fTienQNCNDT float, fTienCNVQPDT float,fTienLDHDDT float,fTienHSQBSDT float
);
declare @DataHachToan table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), sM nvarchar(50),sLNSHT nvarchar(50), iSoDaThucHienNamTruocHT int, iSoUocThucHienNamTruocHT int, iSoKeHoachThucHienNamNayHT int, iSoSQHT int, iSoQNCNHT float,iSoCNVQPHT int,iSoLDHDHT int,  iSoHSQBSHT int,
fTienDaThucHienNamTruocHT float,fTienUocThucHienNamTruocHT float, fTienKeHoachThucHienNamNayHT float,fTienSQHT float,fTienQNCNHT float, fTienCNVQPHT float,fTienLDHDHT float,fTienHSQBSHT float
);

INSERT INTO @DataDuToan (idDonVi,sTenDonVI , sM,sLNSDT , iSoDaThucHienNamTruocDT , iSoUocThucHienNamTruocDT , iSoKeHoachThucHienNamNayDT , iSoSQDT , iSoQNCNDT ,iSoCNVQPDT ,iSoLDHDDT ,  iSoHSQBSDT ,
fTienDaThucHienNamTruocDT ,fTienUocThucHienNamTruocDT , fTienKeHoachThucHienNamNayDT ,fTienSQDT ,fTienQNCNDT , fTienCNVQPDT ,fTienLDHDDT ,fTienHSQBSDT 
)
	SELECT 
	   dt_dv.sTenDonVi,
	   dt_dv.id idDonVi,
	   A.sM,
	   A.sLNS,
	   SoDaThucHienNamTruoc=SUM(IsNull(A.iSoDaThucHienNamTruoc,0)),
	   SoUocThucHienNamTruoc=SUM(IsNull(A.iSoUocThucHienNamTruoc,0)),
	   SoKeHoachThucHienNamNay=SUM(IsNull(A.iSoKeHoachThucHienNamNay,0)),
	   SoSQ=SUM(IsNull(A.iSoSQ,0)),
	   SoQNCN=SUM(IsNull(A.iSoQNCN,0)),
	   SoCNVQP=SUM(IsNull(A.iSoCNVQP,0)),
	   SoLDHD=SUM(IsNull(A.iSoLDHD,0)),
	   SoHSQBS=SUM(IsNull(A.iSoHSQBS,0)),

	   TienDaThucHienNamTruoc=SUM(IsNull(A.fTienDaThucHienNamTruoc,0)),
	   TienUocThucHienNamTruoc=SUM(IsNull(A.fTienUocThucHienNamTruoc,0)),
	   TienKeHoachThucHienNamNay=SUM(IsNull(A.fTienKeHoachThucHienNamNay,0)),
	   TienSQ=SUM(IsNull(A.fTienSQ,0)),
	   TienQNCN=SUM(IsNull(A.fTienQNCN,0)),
	   TienCNVQP=SUM(IsNull(A.fTienCNVQP,0)),
	   TienLDHD=SUM(IsNull(A.fTienLDHD,0)),
	   TienHSQBS=SUM(IsNull(A.fTienHSQBS,0))

FROM
  (SELECT ml.iID_MLNS ,
                ml.iID_MLNS_Cha,
                ml.sXauNoiMa,
                ml.sNG,
				ml.sM,
				ml.sLNS,
                ml.sMoTa,
				ml.bHangCha,
				ct.iID_DonVi,
				ct.iID_MaDonVi,
                ctct.iSoDaThucHienNamTruoc,
				ctct.iSoUocThucHienNamTruoc,
				ctct.iSoKeHoachThucHienNamNay,
				ctct.iSoSQ,
				ctct.iSoQNCN,
				ctct.iSoCNVQP,
				ctct.iSoLDHD,
				ctct.iSoHSQBS,
				ctct.fTienDaThucHienNamTruoc,
				ctct.fTienUocThucHienNamTruoc,
				ctct.fTienKeHoachThucHienNamNay,
				ctct.fTienSQ,
				ctct.fTienQNCN,
				ctct.fTienCNVQP,
				ctct.fTienLDHD,
				ctct.fTienHSQBS
   FROM BH_KHC_CheDoBHXH_ChiTiet ctct
   LEFT JOIN BH_KHC_CheDoBHXH ct ON ct.ID = ctct.iID_KHC_CheDoBHXH
   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MucLucNganSach = ml.iID_MLNS
   AND ml.iNamLamViec = @namLamViec--@namLamViec
   AND ml.iTrangThai = 1
   AND ml.sLNS = 9010001
   WHERE ct.iNamChungTu = @namLamViec--@namLamViec
	 AND ct.iLoaiTongHop = @iLoaiTongHop) AS A 
   LEFT JOIN
  (SELECT iID_MaDonVi AS id,
          sTenDonVi, iLoai
   FROM DonVi
   WHERE iTrangThai = 1
   AND iNamLamViec = @namLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   --AND iNamLamViec = @namLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   
GROUP BY dt_dv.sTenDonVi,
		dt_dv.id,
		 A.sM,A.sLNS;

INSERT INTO @DataHachToan (idDonVi,sTenDonVI , sM,sLNSHT , iSoDaThucHienNamTruocHT , iSoUocThucHienNamTruocHT , iSoKeHoachThucHienNamNayHT , iSoSQHT , iSoQNCNHT ,iSoCNVQPHT ,iSoLDHDHT ,  iSoHSQBSHT ,
fTienDaThucHienNamTruocHT ,fTienUocThucHienNamTruocHT , fTienKeHoachThucHienNamNayHT ,fTienSQHT ,fTienQNCNHT , fTienCNVQPHT ,fTienLDHDHT ,fTienHSQBSHT 
)
	SELECT 
	   dt_dv.sTenDonVi,
	   dt_dv.id idDonVi,
	   A.sM,
	   A.sLNS,
	   SoDaThucHienNamTruoc=SUM(IsNull(A.iSoDaThucHienNamTruoc,0)),
	   SoUocThucHienNamTruoc=SUM(IsNull(A.iSoUocThucHienNamTruoc,0)),
	   SoKeHoachThucHienNamNay=SUM(IsNull(A.iSoKeHoachThucHienNamNay,0)),
	   SoSQ=SUM(IsNull(A.iSoSQ,0)),
	   SoQNCN=SUM(IsNull(A.iSoQNCN,0)),
	   SoCNVQP=SUM(IsNull(A.iSoCNVQP,0)),
	   SoLDHD=SUM(IsNull(A.iSoLDHD,0)),
	   SoHSQBS=SUM(IsNull(A.iSoHSQBS,0)),
	   TienDaThucHienNamTruoc=SUM(IsNull(A.fTienDaThucHienNamTruoc,0)),
	   TienUocThucHienNamTruoc=SUM(IsNull(A.fTienUocThucHienNamTruoc,0)),
	   TienKeHoachThucHienNamNay=SUM(IsNull(A.fTienKeHoachThucHienNamNay,0)),
	   TienSQ=SUM(IsNull(A.fTienSQ,0)),
	   TienQNCN=SUM(IsNull(A.fTienQNCN,0)),
	   TienCNVQP=SUM(IsNull(A.fTienCNVQP,0)),
	   TienLDHD=SUM(IsNull(A.fTienLDHD,0)),
	   TienHSQBS=SUM(IsNull(A.fTienHSQBS,0))
FROM
  (SELECT ml.iID_MLNS ,
                ml.iID_MLNS_Cha,
                ml.sXauNoiMa,
                ml.sNG,
				ml.sM,
				ml.sLNS,
                ml.sMoTa,
				ml.bHangCha,
				ct.iID_DonVi,
				ct.iID_MaDonVi,
                ctct.iSoDaThucHienNamTruoc,
				ctct.iSoUocThucHienNamTruoc,
				ctct.iSoKeHoachThucHienNamNay,
				ctct.iSoSQ,
				ctct.iSoQNCN,
				ctct.iSoCNVQP,
				ctct.iSoLDHD,
				ctct.iSoHSQBS,
				ctct.fTienDaThucHienNamTruoc,
				ctct.fTienUocThucHienNamTruoc,
				ctct.fTienKeHoachThucHienNamNay,
				ctct.fTienSQ,
				ctct.fTienQNCN,
				ctct.fTienCNVQP,
				ctct.fTienLDHD,
				ctct.fTienHSQBS
   FROM BH_KHC_CheDoBHXH_ChiTiet ctct
   LEFT JOIN BH_KHC_CheDoBHXH ct ON ct.ID = ctct.iID_KHC_CheDoBHXH
   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MucLucNganSach = ml.iID_MLNS
   AND ml.iNamLamViec =@namLamViec --@namLamViec
   AND ml.iTrangThai = 1
   AND ml.sLNS = 9010002
   WHERE ct.iNamChungTu =@namLamViec --@namLamViec
	 AND ct.iLoaiTongHop = @iLoaiTongHop) AS A 
   LEFT JOIN
  (SELECT iID_MaDonVi AS id,
          sTenDonVi, iLoai
   FROM DonVi
   WHERE iTrangThai = 1
   --AND iNamLamViec = @namLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   AND iNamLamViec = @namLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   
GROUP BY dt_dv.sTenDonVi,
		dt_dv.id,
		 A.sM,A.sLNS;

SELECT * INTO #Temp From (
		  SELECT * from @DataDuToan
		   UNION ALL
		   SELECT * from @DataHachToan) tbl

SELECT idDonVi as IDDonVi
,sTenDonVi as STenDonVi
,sM as SM
, sLNSDT as SLNS
,IsNull(iSoDaThucHienNamTruocDT, 0) SoDaThucHienNamTruoc
,IsNull(iSoUocThucHienNamTruocDT, 0) SoUocThucHienNamTruoc
, IsNull(iSoKeHoachThucHienNamNayDT, 0) SoKeHoachThucHienNamNay
,IsNull(iSoSQDT, 0) SoSQ
, IsNull(iSoQNCNDT, 0) SoQNCN
, IsNull(iSoCNVQPDT, 0) SoCNVQP
,IsNull(iSoLDHDDT, 0) SoLDHD
,IsNull(iSoHSQBSDT, 0) SoHSQBS
,IsNull(fTienDaThucHienNamTruocDT, 0) TienDaThucHienNamTruoc
,IsNull(fTienUocThucHienNamTruocDT, 0) TienUocThucHienNamTruoc
,IsNull(fTienKeHoachThucHienNamNayDT, 0) TienKeHoachThucHienNamNay
,IsNull(fTienSQDT, 0) TienSQ
,IsNull(fTienQNCNDT, 0) TienQNCN
,IsNull(fTienLDHDDT, 0) TienLDHD
, IsNull(fTienHSQBSDT, 0) TienHSQBS
FROM #Temp 
WHERE sTenDonVI in 
    (SELECT *
     FROM f_split(@listTenDonVi))

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_qlkp_chungtu_tonghop_bhxh]    Script Date: 9/15/2023 10:49:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_khc_qlkp_chungtu_tonghop_bhxh]
	@listTenDonVi ntext,
	@namLamViec int,
	@iLoaiTongHop int
AS
BEGIN
declare @DataKhoi table (idDonVi nvarchar(50),sTenDonVI nvarchar(200),
fTienDaThucHienNamTruoc float,fTienUocThucHienNamTruoc float, fTienKeHoachThucHienNamNay float,fTienCanBo float,fTienQuanLuc float, fTienTaiChinh float,fTienQuanY float
);

INSERT INTO @DataKhoi (idDonVi,sTenDonVI , 
fTienDaThucHienNamTruoc ,fTienUocThucHienNamTruoc , fTienKeHoachThucHienNamNay ,fTienCanBo ,fTienQuanLuc , fTienTaiChinh ,fTienQuanY 
)
	SELECT 
	   dt_dv.sTenDonVi,
	   dt_dv.id idDonVi,
	   TienDaThucHienNamTruoc=SUM(IsNull(A.fTienDaThucHienNamTruoc,0)),
	   TienUocThucHienNamTruoc=SUM(IsNull(A.fTienUocThucHienNamTruoc,0)),
	   TienKeHoachThucHienNamNay=SUM(IsNull(A.fTienKeHoachThucHienNamNay,0)),
	   TienSQ=SUM(IsNull(A.fTienCanBo,0)),
	   TienQNCN=SUM(IsNull(A.fTienQuanLuc,0)),
	   TienCNVQP=SUM(IsNull(A.fTienTaiChinh,0)),
	   TienLDHD=SUM(IsNull(A.fTienQuanY,0))
FROM
  (SELECT 
				ct.iID_DonVi,
				ct.iID_MaDonVi,
				ctct.fTienDaThucHienNamTruoc,
				ctct.fTienUocThucHienNamTruoc,
				ctct.fTienKeHoachThucHienNamNay,
				ctct.fTienCanBo,
				ctct.fTienQuanLuc,
				ctct.fTienTaiChinh,
				ctct.fTienQuanY
   FROM BH_KHC_KinhPhiQuanLy_ChiTiet ctct
   LEFT JOIN BH_KHC_KinhPhiQuanLy ct ON ct.iID_BH_KHC_KinhPhiQuanLy = ctct.iID_KHC_KinhPhiQuanLy
   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MucLucNganSach = ml.iID_MLNS
   AND ml.iNamLamViec = @namLamViec--@namLamViec
   AND ml.iTrangThai = 1
   AND ml.sLNS =9010003 --9020000
   WHERE ct.iNamChungTu = @namLamViec--@namLamViec
	 AND ct.iLoaiTongHop = @iLoaiTongHop) AS A 
   LEFT JOIN
  (SELECT iID_MaDonVi AS id,
          sTenDonVi, iLoai
   FROM DonVi
   WHERE iTrangThai = 1
   AND iNamLamViec = @namLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   --AND iNamLamViec = @namLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   
GROUP BY dt_dv.sTenDonVi,
		dt_dv.id;

SELECT dt.idDonVi, 
dt.sTenDonVI, 
IsNull(dt.fTienDaThucHienNamTruoc, 0) FTienDaThucHienNamTruoc,
IsNull(dt.fTienUocThucHienNamTruoc, 0) FTienUocThucHienNamTruoc,
IsNull(dt.fTienKeHoachThucHienNamNay, 0) FTienKeHoachThucHienNamNay,
IsNull(dt.fTienCanBo, 0) FTienCanBo,
IsNull(dt.fTienQuanLuc, 0) FTienQuanLuc,
IsNull(dt.fTienTaiChinh, 0) FTienTaiChinh,
IsNull(dt.fTienQuanY, 0) FTienQuanY
FROM @DataKhoi dt
where dt.sTenDonVI in 
    (SELECT *
     FROM f_split(@listTenDonVi))
END
;


GO
