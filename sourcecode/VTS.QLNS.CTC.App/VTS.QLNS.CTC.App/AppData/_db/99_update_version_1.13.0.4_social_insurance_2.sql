/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_kcb_chungtu_tonghop_bhxh]    Script Date: 7/25/2023 9:00:47 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_khc_kcb_chungtu_tonghop_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_khc_kcb_chungtu_tonghop_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_khc_kcb_chungtu_chitiet_tao_tonghop]    Script Date: 7/25/2023 9:00:47 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_khc_kcb_chungtu_chitiet_tao_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_khc_kcb_chungtu_chitiet_tao_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_khc_kcb_chi_tiet]    Script Date: 7/25/2023 9:00:47 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_khc_kcb_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_khc_kcb_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[bh_kehoach_chikhac_index]    Script Date: 7/25/2023 9:00:47 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[bh_kehoach_chikhac_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[bh_kehoach_chikhac_index]
GO
/****** Object:  StoredProcedure [dbo].[bh_kehoach_chikhac_index]    Script Date: 7/25/2023 9:00:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Doantc
-- Create date: 13/06/2023
-- Description:	Lấy danh sách hiển thị lâp kế hoạch chi các chế dộ BHXH

-- =============================================
CREATE PROCEDURE [dbo].[bh_kehoach_chikhac_index]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
	DISTINCT 
		 kcb.iID_BH_KHC_KCB 
		, kcb.sSoChungTu
		, kcb.dNgayChungTu
		, kcb.sSoQuyetDinh
		, kcb.dNgayQuyetDinh
		, kcb.iNamChungTu
		, kcb.iID_DonVi
		, kcb.iID_MaDonVi
		, kcb.sMoTa
		, kcb.iLoaiKCB
		, kcb.fTongTienDaThucHienNamTruoc
		, kcb.fTongTienUocThucHienNamTruoc
		, kcb.fTongTienKeHoachThucHienNamNay
		, kcb.sTongHop
		, kcb.iID_TongHopID
		, kcb.iLoaiTongHop
		, kcb.bIsKhoa

		, kcb.dNgaySua
		, kcb.dNgayTao
		, kcb.sNguoiSua
		, kcb.sNguoiTao
		, kcb.dNgayTao
		, donVi.sTenDonVi
		-- Tong dự toán todo
	FROM BH_KHC_KCB kcb
	LEFT JOIN DonVi donVi
		ON kcb.iID_MaDonVi = donVi.iID_MaDonVi AND donVi.iID_DonVi = kcb.iID_DonVi
END
GO
/****** Object:  StoredProcedure [dbo].[sp_khc_kcb_chi_tiet]    Script Date: 7/25/2023 9:00:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[sp_khc_kcb_chi_tiet]
	@NamLamViec int,
	@IdDonVi nvarchar(100),
	@iID_BH_KHC_KCB uniqueidentifier
AS
BEGIN
SELECT 
		ct.iID_BH_KHC_KCB_ChiTiet ,
		ct.iID_KHC_KCB ,
		ct.iID_MaDonVi,
		ct.iID_MucLucNganSach,
		ct.sMoTa,
		ct.sNoiDung,
		ct.fTienDaThucHienNamTruoc,
		ct.fTienUocThucHienNamTruoc,
		ct.fTienKeHoachThucHienNamNay,
		ct.sGhiChu,
		ct.dNgaySua,
		ct.dNgayTao,
		ct.sNguoiSua,
		ct.sNguoiTao,
		ct.sTenDonVi

	FROM
		(
			SELECT
				bh.iID_MaDonVi,
				bh.sMoTa,
				ddv.sTenDonVi,
				bhct.*
			FROM 
				BH_KHC_KCB bh
			JOIN 
				BH_KHC_KCB_ChiTiet bhct ON bh.iID_BH_KHC_KCB = bhct.iID_KHC_KCB 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bh.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bh.iID_MaDonVi = @IdDonVi
				--AND (@BKhoa = 3 OR bh.bIsKhoa = @BKhoa) 
		) ct
		where ct.iID_KHC_KCB=@iID_BH_KHC_KCB
		;

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_khc_kcb_chungtu_chitiet_tao_tonghop]    Script Date: 7/25/2023 9:00:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  -- Author:    <Author,,Name>
  -- Create date: <Create Date,,>
  -- Description:  <Description,,>
  -- =============================================
  Create PROCEDURE [dbo].[sp_khc_kcb_chungtu_chitiet_tao_tonghop] 
  @ListIdChungTuTongHop ntext, 
  @Nguoitao nvarchar(50),
  @IdChungTu nvarchar(100), 
  @NamLamViec int
  AS BEGIN 
  INSERT INTO [dbo].[BH_KHC_KCB_ChiTiet] (
    iID_BH_KHC_KCB_ChiTiet, iID_KHC_KCB, 
    iID_MucLucNganSach,  sNoiDung, 
    fTienDaThucHienNamTruoc, fTienUocThucHienNamTruoc, 
    fTienKeHoachThucHienNamNay, dNgaySua, dNgayTao, 
    sNguoiSua, sNguoiTao
  ) 
SELECT 
DISTINCT
  NEWID(), 
  @idChungTu, 
  iID_MucLucNganSach, 
  sNoiDung, 
  sum(fTienDaThucHienNamTruoc), 
  sum(fTienUocThucHienNamTruoc), 
  sum(fTienKeHoachThucHienNamNay), 
  Null, 
  GETDATE(), 
  Null, 
  @Nguoitao 
FROM 
  BH_KHC_KCB_ChiTiet 
WHERE 
  iID_KHC_KCB in (
    SELECT 
      * 
    FROM 
      f_split(@ListIdChungTuTongHop)
  ) 
GROUP BY 
  iID_MucLucNganSach, 
  sNoiDung;
--danh dau chung tu da tong hop
update 
  BH_KHC_KCB 
set 
  iLoaiTongHop = 2 
where 
  iID_BH_KHC_KCB in (
    SELECT 
      * 
    FROM 
      f_split(@ListIdChungTuTongHop)
  ) 
  
END;



GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_kcb_chungtu_tonghop_bhxh]    Script Date: 7/25/2023 9:00:47 AM ******/
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
