/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_khssv_nld_chungtu_tonghop_bhxh]    Script Date: 11/29/2023 5:56:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_khc_khssv_nld_chungtu_tonghop_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_khc_khssv_nld_chungtu_tonghop_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_kcbqy_chitiet]    Script Date: 11/29/2023 5:56:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_khc_kcbqy_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_khc_kcbqy_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_kcb_chungtu_tonghop_bhxh]    Script Date: 11/29/2023 5:56:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_khc_kcb_chungtu_tonghop_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_khc_kcb_chungtu_tonghop_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_khc_rpt_get_donvi_KPQL]    Script Date: 11/29/2023 5:56:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_khc_rpt_get_donvi_KPQL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_khc_rpt_get_donvi_KPQL]
GO
/****** Object:  StoredProcedure [dbo].[sp_khc_rpt_get_donvi_KCBQY]    Script Date: 11/29/2023 5:56:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_khc_rpt_get_donvi_KCBQY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_khc_rpt_get_donvi_KCBQY]
GO
/****** Object:  StoredProcedure [dbo].[sp_khc_rpt_get_donvi_BHXH]    Script Date: 11/29/2023 5:56:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_khc_rpt_get_donvi_BHXH]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_khc_rpt_get_donvi_BHXH]
GO
/****** Object:  StoredProcedure [dbo].[sp_khc_rpt_get_donvi_BHXH]    Script Date: 11/29/2023 5:56:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_khc_rpt_get_donvi_BHXH]
@NamLamViec int
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

FROM BH_KHC_CheDoBHXH chungtu
LEFT JOIN DonVi donvi ON chungtu.iID_MaDonVi = donvi.iID_MaDonVi
WHERE chungtu.iNamChungTu = @NamLamViec
  AND chungtu.iID_MaDonVi <> ''
  AND chungtu.iID_MaDonVi IS NOT NULL
  AND donvi.iNamLamViec = @NamLamViec
  --AND chungtu.iLoaiTongHop=@LoaiChungTu
ORDER BY cast(chungtu.dNgayChungTu AS date) END
;
;



GO
/****** Object:  StoredProcedure [dbo].[sp_khc_rpt_get_donvi_KCBQY]    Script Date: 11/29/2023 5:56:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[sp_khc_rpt_get_donvi_KCBQY]
@NamLamViec int
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

FROM BH_KHC_KCB chungtu
LEFT JOIN DonVi donvi ON chungtu.iID_MaDonVi = donvi.iID_MaDonVi
WHERE chungtu.iNamChungTu = @NamLamViec
  AND chungtu.iID_MaDonVi <> ''
  AND chungtu.iID_MaDonVi IS NOT NULL
  AND donvi.iNamLamViec = @NamLamViec
  --AND chungtu.iLoaiTongHop=@LoaiChungTu
ORDER BY cast(chungtu.dNgayChungTu AS date) END
;
;



GO
/****** Object:  StoredProcedure [dbo].[sp_khc_rpt_get_donvi_KPQL]    Script Date: 11/29/2023 5:56:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[sp_khc_rpt_get_donvi_KPQL]
@NamLamViec int
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

FROM BH_KHC_KinhPhiQuanLy chungtu
LEFT JOIN DonVi donvi ON chungtu.iID_MaDonVi = donvi.iID_MaDonVi
WHERE chungtu.iNamChungTu = @NamLamViec
  AND chungtu.iID_MaDonVi <> ''
  AND chungtu.iID_MaDonVi IS NOT NULL
  AND donvi.iNamLamViec = @NamLamViec
  --AND chungtu.iLoaiTongHop=@LoaiChungTu
ORDER BY cast(chungtu.dNgayChungTu AS date) END
;
;



GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_kcb_chungtu_tonghop_bhxh]    Script Date: 11/29/2023 5:56:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_khc_kcb_chungtu_tonghop_bhxh]
	@listTenDonVi ntext,
	@namLamViec int,
	@iLoaiTongHop int
AS
BEGIN
declare @DataDuToan table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), sM nvarchar(50),sLNSDT nvarchar(50), 
fTienDaThucHienNamTruocDT float,fTienUocThucHienNamTruocDT float, fTienKeHoachThucHienNamNayDT float
);
declare @DataHachToan table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), sM nvarchar(50),sLNSHT nvarchar(50),
fTienDaThucHienNamTruocHT float,fTienUocThucHienNamTruocHT float, fTienKeHoachThucHienNamNayHT float
);

INSERT INTO @DataDuToan (idDonVi,sTenDonVI , sM,sLNSDT ,
fTienDaThucHienNamTruocDT ,fTienUocThucHienNamTruocDT , fTienKeHoachThucHienNamNayDT 
)
	SELECT 
	   dt_dv.sTenDonVi,
	   dt_dv.id idDonVi,
	   A.sM,
	   A.sLNS,

	   TienDaThucHienNamTruoc=SUM(IsNull(A.fTienDaThucHienNamTruoc,0)),
	   TienUocThucHienNamTruoc=SUM(IsNull(A.fTienUocThucHienNamTruoc,0)),
	   TienKeHoachThucHienNamNay=SUM(IsNull(A.fTienKeHoachThucHienNamNay,0))


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

				ctct.fTienDaThucHienNamTruoc,
				ctct.fTienUocThucHienNamTruoc,
				ctct.fTienKeHoachThucHienNamNay

   FROM BH_KHC_KCB_ChiTiet ctct
   LEFT JOIN BH_KHC_KCB ct ON ct.iID_BH_KHC_KCB = ctct.iID_KHC_KCB
   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MucLucNganSach = ml.iID_MLNS
   AND ml.iNamLamViec = @namLamViec--@namLamViec
   AND ml.iTrangThai = 1
   AND ml.sLNS = 9010004
   WHERE ct.iNamChungTu = @namLamViec--@namLamViec
	 --AND ct.iLoaiTongHop = @iLoaiTongHop
	 ) AS A 
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

INSERT INTO @DataHachToan (idDonVi,sTenDonVI , sM,sLNSHT ,
fTienDaThucHienNamTruocHT ,fTienUocThucHienNamTruocHT , fTienKeHoachThucHienNamNayHT 
)
	SELECT 
	   dt_dv.sTenDonVi,
	   dt_dv.id idDonVi,
	   A.sM,
	   A.sLNS,

	   TienDaThucHienNamTruoc=SUM(IsNull(A.fTienDaThucHienNamTruoc,0)),
	   TienUocThucHienNamTruoc=SUM(IsNull(A.fTienUocThucHienNamTruoc,0)),
	   TienKeHoachThucHienNamNay=SUM(IsNull(A.fTienKeHoachThucHienNamNay,0))

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

				ctct.fTienDaThucHienNamTruoc,
				ctct.fTienUocThucHienNamTruoc,
				ctct.fTienKeHoachThucHienNamNay

   FROM BH_KHC_KCB_ChiTiet ctct
   LEFT JOIN BH_KHC_KCB ct ON ct.iID_BH_KHC_KCB = ctct.iID_KHC_KCB
   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MucLucNganSach = ml.iID_MLNS
   AND ml.iNamLamViec =@namLamViec --@namLamViec
   AND ml.iTrangThai = 1
   AND ml.sLNS = 9010005
   WHERE ct.iNamChungTu =@namLamViec --@namLamViec
	 --AND ct.iLoaiTongHop = @iLoaiTongHop
	 ) AS A 
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

SELECT dt.idDonVi, 
dt.sTenDonVI, 
dt.sM,
dt.sLNSDT,
ht.sLNSHT,

IsNull(dt.fTienDaThucHienNamTruocDT, 0) TienDaThucHienNamTruocDT,
IsNull(ht.fTienDaThucHienNamTruocHT, 0) TienDaThucHienNamTruocHT,

IsNull(dt.fTienUocThucHienNamTruocDT, 0) TienUocThucHienNamTruocDT,
IsNull(ht.fTienUocThucHienNamTruocHT, 0) TienUocThucHienNamTruocHT,

IsNull(dt.fTienKeHoachThucHienNamNayDT, 0) TienKeHoachThucHienNamNayDT,
IsNull(ht.fTienKeHoachThucHienNamNayHT, 0) TienKeHoachThucHienNamNayHT

FROM @DataDuToan dt
LEFT JOIN @DataHachToan ht ON dt.idDonVi = ht.idDonVi
where dt.sTenDonVI in 
    (SELECT *
     FROM f_split(@listTenDonVi))

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_kcbqy_chitiet]    Script Date: 11/29/2023 5:56:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay chung tu theo don vi 
CREATE PROCEDURE [dbo].[sp_rpt_khc_kcbqy_chitiet]
	 @NamLamViec int,
	 @IdDonVi nvarchar(max),
	 @Dvt int
AS
BEGIN 
	SET NOCOUNT ON;
	-- lấy ra mlns theo phân cấp
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha
			into #tblMlnsByPhanCap
		FROM BH_DM_MucLucNganSach 
		WHERE 
		iNamLamViec = 2023 
		AND iTrangThai = 1
		AND (sLNS ='9010004' Or sLNS ='9010005')

		SELECT 
		tblml.iID_MLNS as IID_MucLucNganSach,
		tblml.iID_MLNS_Cha as IdParent,
		tblml.sXauNoiMa  ,
		tblml.sLNS,
		tblml.sL,
		tblml.sK,
		tblml.sM,
		tblml.sTM,
		tblml.sTTM,
		tblml.sNG,
		tblml.sTNG,
		tblml.sTNG1,
		tblml.sTNG2,
		tblml.sTNG3,
		tblml.sMoTa,
		tblml.bHangCha as IsHangCha,
		ISNULL(chitiet.fTienDaThucHienNamTruoc, 0)/@Dvt fTienDaThucHienNamTruoc,
		ISNULL(chitiet.fTienUocThucHienNamTruoc, 0)/@Dvt fTienUocThucHienNamTruoc,
		ISNULL(chitiet.fTienKeHoachThucHienNamNay, 0)/@Dvt fTienKeHoachThucHienNamNay,
		chitiet.sGhiChu
		
		FROM #tblMlnsByPhanCap tblml
		LEFT JOIN
		(SELECT CTCT.* FROM
			BH_KHC_KCB_ChiTiet CTCT
			WHERE  CTCT.iID_BH_KHC_KCB_ChiTiet IN
			(
				SELECT CT.iID_BH_KHC_KCB
						FROM BH_KHC_KCB CT
						WHERE CT.iID_MaDonVi=@IdDonVi
							AND CT.sSoChungTu <> ''
							AND CT.sSoChungTu IS NOT NULL
							AND CT.INamChungTu=@NamLamViec
			)) chitiet ON 

			chitiet.iID_MucLucNganSach=tblml.iID_MLNS

		ORDER BY tblml.sXauNoiMa

		drop table #tblMlnsByPhanCap
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_khssv_nld_chungtu_tonghop_bhxh]    Script Date: 11/29/2023 5:56:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_khc_khssv_nld_chungtu_tonghop_bhxh]
	@listTenDonVi ntext,
	@namLamViec int,
	@iLoaiTongHop int,
	@listSLNS nvarchar(max)

AS
BEGIN
	SELECT 
	   dt_dv.sTenDonVi,
	   dt_dv.id idDonVi,
	   A.sM,
	   A.sLNS,
	   fTienDaThucHienNamTruocDT=SUM(IsNull(A.fTienDaThucHienNamTruoc,0)),
	   fTienUocThucHienNamTruocDT=SUM(IsNull(A.fTienUocThucHienNamTruoc,0)),
	   fTienKeHoachThucHienNamNayDT=SUM(IsNull(A.fTienKeHoachThucHienNamNay,0))
	   into #temp
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
				ctct.fTienDaThucHienNamTruoc,
				ctct.fTienUocThucHienNamTruoc,
				ctct.fTienKeHoachThucHienNamNay

   FROM BH_KHC_K_ChiTiet ctct
   LEFT JOIN BH_KHC_K ct ON ct.iID_BH_KHC_K = ctct.iID_KHC_K
   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MucLucNganSach = ml.iID_MLNS
   AND ml.iNamLamViec = @namLamViec--@namLamViec
   AND ml.iTrangThai = 1
   AND ml.sLNS IN  (SELECT *
     FROM f_split(@listSLNS))
   WHERE ct.iNamChungTu = @namLamViec--@namLamViec
	 AND ct.iLoaiTongHop = @iLoaiTongHop) AS A 
   LEFT JOIN
  (SELECT iID_MaDonVi AS id,
          sTenDonVi, iLoai
   FROM DonVi
   WHERE iTrangThai = 1
   AND iNamLamViec = @namLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id  
GROUP BY dt_dv.sTenDonVi,
		dt_dv.id,
		 A.sM,A.sLNS;

SELECT dt.idDonVi, 
dt.sTenDonVI, 
dt.sM,
dt.sLNS,
IsNull(dt.fTienDaThucHienNamTruocDT, 0) TienDaThucHienNamTruocDT,
IsNull(dt.fTienUocThucHienNamTruocDT, 0) TienUocThucHienNamTruocDT,
IsNull(dt.fTienKeHoachThucHienNamNayDT, 0) TienKeHoachThucHienNamNayDT
FROM #temp dt
WHERE dt.idDonVi in 
    (SELECT *
     FROM f_split(@listTenDonVi))	 
END
;
GO
