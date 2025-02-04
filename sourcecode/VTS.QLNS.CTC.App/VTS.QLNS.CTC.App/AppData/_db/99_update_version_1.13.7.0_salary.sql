/****** Object:  StoredProcedure [dbo].[sp_tl_export_quy_luong_can_cu]    Script Date: 12/20/2023 10:56:33 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_export_quy_luong_can_cu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_export_quy_luong_can_cu]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_thongtinkehoachthu_index]    Script Date: 12/20/2023 10:56:33 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_thongtinkehoachthu_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_thongtinkehoachthu_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_get_quan_so_binh_quan_nam]    Script Date: 12/20/2023 10:56:33 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_get_quan_so_binh_quan_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_get_quan_so_binh_quan_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_get_quan_so_binh_quan_nam]    Script Date: 12/20/2023 10:56:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_bhxh_get_quan_so_binh_quan_nam]
	 @YearOfWork int,
	 @sLuongKehoachId nvarchar(max)
AS
BEGIN
	declare @LuongKeHoach table (Id uniqueidentifier,Nam int, Ma_CanBo varchar(20), Ma_PhuCap nvarchar(50), Ma_CB varchar(20), Gia_Tri numeric(15, 4));

	INSERT INTO @LuongKeHoach (Nam, Ma_CanBo, Ma_CB)
	SELECT DISTINCT Nam, Ma_CanBo, Ma_CB
		FROM TL_BangLuong_KeHoach 
		WHERE Nam = @YearOfWork AND parent IN (SELECT * FROM splitstring(@sLuongKehoachId))

		SELECT '9020001-010-011-0001-0000' XauNoiMa,
		count(1)/12 AS QSBQ 
		FROM @LuongKeHoach 
		WHERE Ma_CB LIKE '1%' --Lấy quân số bình quân năm của cấp bậc Sĩ quan
		
		UNION
		SELECT '9020001-010-011-0001-0001',
		count(1)/12 AS QSBQ_QNCN FROM @LuongKeHoach 
		where Ma_CB LIKE '2%' --Lấy quân số bình quân năm của cấp bậc Quân nhân chuyên nghiệp
		
		UNION
		SELECT '9020001-010-011-0001-0002',
		count(1)/12 AS QSBQ_HSQ FROM @LuongKeHoach 
		where Ma_CB LIKE '0%' --Lấy quân số bình quân năm của cấp bậc Hạ sĩ quan
		
		UNION
		SELECT '9020001-010-011-0002-0000',
		count(1)/12 AS QSBQ_VCQP FROM @LuongKeHoach 
		where Ma_CB in ('3.1', '3.2', '3.3') OR Ma_CB LIKE ('41%') OR Ma_CB LIKE ('42%') --Lấy quân số bình quân năm của cấp bậc CC, CN, VCQP
		
		UNION
		SELECT '9020001-010-011-0002-0001',
		count(1)/12 AS QSBQ_LDHD FROM @LuongKeHoach 
		where Ma_CB = '43' --Lấy quân số bình quân năm của cấp bậc LDHD
		
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_thongtinkehoachthu_index]    Script Date: 12/20/2023 10:56:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bhxh_thongtinkehoachthu_index] 
	@YearOfWork int
AS
BEGIN
	SELECT
	KHT.iID_KHT_BHXH,
	KHT.sSoChungTu,
	KHT.iNamChungTu,
	KHT.dNgayChungTu,
	KHT.iID_MaDonVi,
	DV.sTenDonVi AS sTenDonVi,
	KHT.sMoTa,
	KHT.bIsKhoa,
	KHT.fTongKeHoach,
	KHT.sSoQuyetDinh,
	KHT.dNgayQuyetDinh,
	KHT.sNguoiTao,
	KHT.sNguoiSua,
	KHT.dNgayTao,
	KHT.dNgaySua,
	KHT.iLoaiTongHop,
	KHT.sTongHop,
	KHT.fBHXH_NLD FThuBHXHNLDDong,
	KHT.fBHXH_NSD FThuBHXHNSDDong,
	KHT.fTongBHXH FThuBHXH,
	KHT.fBHYT_NLD FThuBHYTNLDDong,
	KHT.fBHYT_NSD FThuBHYTNSDDong,
	KHT.fTongBHYT FTongBHYT,
	KHT.fBHTN_NLD FThuBHTNNLDDong,
	KHT.fBHTN_NSD FThuBHTNNSDDong,
	KHT.fTongBHTN FThuBHTN,
	KHT.fTong,
	KHT.bDaTongHop,
	KHT.sBangLuongKeHoach SBangLuongKeHoach
	FROM BH_KHT_BHXH KHT
	LEFT JOIN DonVi DV
	ON KHT.iID_MaDonVi = DV.iID_MaDonVi
	WHERE DV.iNamLamViec = @YearOfWork
	ORDER BY KHT.dNgayQuyetDinh DESC
END;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_export_quy_luong_can_cu]    Script Date: 12/20/2023 10:56:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_export_quy_luong_can_cu]
	@NamLamViec int,
	@LuongChinh nvarchar(50),
	@PhuCapCV nvarchar(50),
	@PhuCapTNN nvarchar(50),
	@PhuCapTNVK nvarchar(50),
	@PhuCapHSBL nvarchar(50),
	@lstIdChungTu nvarchar(max)
AS 
BEGIN
	CREATE TABLE #result(Id uniqueidentifier, IIdParent uniqueidentifier, SNoiDung nvarchar(200),SXauNoiMa nvarchar(200), ILeVel int, IThuTu int , QSBQ float,LHT_TT float, PCCV_TT Float, PCTN_TT Float, PCTNVK_TT Float, HSBL_TT Float)
	DECLARE @iIDParent1 uniqueidentifier = NewID();
	DECLARE @iIDParent21 uniqueidentifier = NewID();
	DECLARE @iIDParent22 uniqueidentifier = NewID();
	--INSERT TIER 1
	INSERT INTO #result(Id, IIdParent, SNoiDung,SXauNoiMa, ILeVel, IThuTu, QSBQ, LHT_TT, PCCV_TT, PCTN_TT, PCTNVK_TT,HSBL_TT)
	(SELECT @iIDParent1, NULL, N'I. Khối dự toán',NULL,0, 0, 0, 0, 0, 0, 0,0
	--INSERT TIER 2
	UNION ALL 
	SELECT @iIDParent21, @iIDParent1, N'A. Quân nhân',NULL, 1, 1, 0, 0, 0, 0, 0,0
	--INSERT TIER 3 ' lương sĩ quan'
	UNION ALL 
	SELECT NEWID(), @iIDParent21, N'1. Sĩ quan', '9020001-010-011-0001-0000',2, 1,
	COUNT(Gia_Tri)/(12*5),--/(12*5) Vì chia cho 12 lấy bình quân và chia cho 5 vì lấy value 5 Ma_PhuCap--
	SUM( CASE WHEN Ma_PhuCap = @LuongChinh THEN ISNULL(Gia_Tri, 0) ELSE 0 END),
	SUM( CASE WHEN Ma_PhuCap = @PhuCapCV THEN ISNULL(Gia_Tri, 0) ELSE 0 END),
	SUM( CASE WHEN Ma_PhuCap = @PhuCapTNN THEN ISNULL(Gia_Tri, 0) ELSE 0 END),
	SUM( CASE WHEN Ma_PhuCap = @PhuCapTNVK THEN ISNULL(Gia_Tri, 0) ELSE 0 END),
	SUM( CASE WHEN Ma_PhuCap = @PhuCapHSBL THEN ISNULL(Gia_Tri, 0) ELSE 0 END)
	FROM TL_BangLuong_KeHoach kh
	WHERE
		Nam = @NamLamViec
		AND ((Ma_CB LIKE '1%' AND Ma_PhuCap in (@LuongChinh, @PhuCapCV, @PhuCapTNN, @PhuCapTNVK, @PhuCapHSBL)))
		AND kh.parent IN (SELECT * FROM splitstring(@lstIdChungTu)) 
	
	--INSERT TIER 3 ' lương QNCN'
	UNION ALL 
	SELECT NEWID(), @iIDParent21, N'2. QNCN', '9020001-010-011-0001-0001', 2, 2,
	COUNT(Gia_Tri)/(12*5),
	SUM( CASE WHEN Ma_PhuCap = @LuongChinh THEN ISNULL(Gia_Tri, 0) ELSE 0 END),
	SUM( CASE WHEN Ma_PhuCap = @PhuCapCV THEN ISNULL(Gia_Tri, 0) ELSE 0 END),
	SUM( CASE WHEN Ma_PhuCap = @PhuCapTNN THEN ISNULL(Gia_Tri, 0) ELSE 0 END),
	SUM( CASE WHEN Ma_PhuCap = @PhuCapTNVK THEN ISNULL(Gia_Tri, 0) ELSE 0 END),
	SUM( CASE WHEN Ma_PhuCap = @PhuCapHSBL THEN ISNULL(Gia_Tri, 0) ELSE 0 END)
	FROM TL_BangLuong_KeHoach kh
	WHERE
		Nam = @NamLamViec
		AND ((Ma_CB LIKE '2%' AND Ma_PhuCap in (@LuongChinh, @PhuCapCV, @PhuCapTNN, @PhuCapTNVK, @PhuCapHSBL)))
		AND kh.parent IN (SELECT * FROM splitstring(@lstIdChungTu)) 

	--INSERT TIER 3 ' lương HSQ-BS'
	UNION ALL 
	SELECT NEWID(), @iIDParent21, N'3. HSQ, BS', '9020001-010-011-0001-0002', 2, 2,
	COUNT(Gia_Tri)/(12*5),

	SUM( CASE WHEN Ma_PhuCap = @LuongChinh THEN ISNULL(Gia_Tri, 0) ELSE 0 END),
	SUM( CASE WHEN Ma_PhuCap = @PhuCapCV THEN ISNULL(Gia_Tri, 0) ELSE 0 END),
	SUM( CASE WHEN Ma_PhuCap = @PhuCapTNN THEN ISNULL(Gia_Tri, 0) ELSE 0 END),
	SUM( CASE WHEN Ma_PhuCap = @PhuCapTNVK THEN ISNULL(Gia_Tri, 0) ELSE 0 END),
	SUM( CASE WHEN Ma_PhuCap = @PhuCapHSBL THEN ISNULL(Gia_Tri, 0) ELSE 0 END)
	FROM TL_BangLuong_KeHoach kh
	WHERE
		Nam = @NamLamViec
		AND ((Ma_CB LIKE '0%' AND Ma_PhuCap in (@LuongChinh, @PhuCapCV, @PhuCapTNN, @PhuCapTNVK, @PhuCapHSBL)))
		AND kh.parent IN (SELECT * FROM splitstring(@lstIdChungTu)) 


	--INSERT TIER 2.2
	UNION ALL 
	SELECT @iIDParent22, @iIDParent1, N'B. Người lao động',NULL, 1, 2, 0, 0, 0, 0, 0,0
	--INSERT TIER 3 ' CC,CN,VCQP'
	UNION ALL 
	SELECT NEWID(), @iIDParent22, N'1. CC, CN, VCQP','9020001-010-011-0002-0000', 2, 1,
	COUNT(Gia_Tri)/(12*5),
	SUM( CASE WHEN Ma_PhuCap = @LuongChinh THEN ISNULL(Gia_Tri, 0) ELSE 0 END),
	SUM( CASE WHEN Ma_PhuCap = @PhuCapCV THEN ISNULL(Gia_Tri, 0) ELSE 0 END),
	SUM( CASE WHEN Ma_PhuCap = @PhuCapTNN THEN ISNULL(Gia_Tri, 0) ELSE 0 END),
	SUM( CASE WHEN Ma_PhuCap = @PhuCapTNVK THEN ISNULL(Gia_Tri, 0) ELSE 0 END),
	SUM( CASE WHEN Ma_PhuCap = @PhuCapHSBL THEN ISNULL(Gia_Tri, 0) ELSE 0 END)
	FROM TL_BangLuong_KeHoach kh
	WHERE
		Nam = @NamLamViec
		AND (((Ma_CB IN ('3.1','3.2','3.3') OR Ma_CB LIKE ('41%') OR Ma_CB LIKE ('42%')) AND Ma_PhuCap in (@LuongChinh, @PhuCapCV, @PhuCapTNN, @PhuCapTNVK, @PhuCapHSBL)))
		AND kh.parent IN (SELECT * FROM splitstring(@lstIdChungTu)) 
	
	--INSERT TIER 3 ' lương LĐHĐ'
	UNION ALL 
	SELECT NEWID(), @iIDParent21, N'2. LĐHĐ', '9020001-010-011-0002-0001', 2, 2,
	COUNT(Gia_Tri)/(12*5),

	SUM( CASE WHEN Ma_PhuCap = @LuongChinh THEN ISNULL(Gia_Tri, 0) ELSE 0 END),
	SUM( CASE WHEN Ma_PhuCap = @PhuCapCV THEN ISNULL(Gia_Tri, 0) ELSE 0 END),
	SUM( CASE WHEN Ma_PhuCap = @PhuCapTNN THEN ISNULL(Gia_Tri, 0) ELSE 0 END),
	SUM( CASE WHEN Ma_PhuCap = @PhuCapTNVK THEN ISNULL(Gia_Tri, 0) ELSE 0 END),
	SUM( CASE WHEN Ma_PhuCap = @PhuCapHSBL THEN ISNULL(Gia_Tri, 0) ELSE 0 END)
	FROM TL_BangLuong_KeHoach kh
	WHERE
		Nam = @NamLamViec
		AND ((Ma_CB LIKE ('43%') AND Ma_PhuCap in (@LuongChinh, @PhuCapCV, @PhuCapTNN, @PhuCapTNVK, @PhuCapHSBL)))
		AND kh.parent IN (SELECT * FROM splitstring(@lstIdChungTu)) 

	);

	SELECT * FROM #result;
	DROP TABLE #result;
END
GO
