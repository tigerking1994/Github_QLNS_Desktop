/****** Object:  StoredProcedure [dbo].[sp_bhxh_lay_luong_ke_hoach]    Script Date: 11/1/2023 1:49:02 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_lay_luong_ke_hoach]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_lay_luong_ke_hoach]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_get_quan_so_binh_quan_nam]    Script Date: 11/1/2023 1:49:02 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_get_quan_so_binh_quan_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_get_quan_so_binh_quan_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_get_quan_so_binh_quan_nam]    Script Date: 11/1/2023 1:49:02 PM ******/
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
	@YearOfWork int
AS
BEGIN
	declare @LuongKeHoach table (Id uniqueidentifier,Nam int, Ma_CanBo varchar(20), Ma_PhuCap nvarchar(50), Ma_CB varchar(20), Gia_Tri numeric(15, 4));

	INSERT INTO @LuongKeHoach (Nam, Ma_CanBo, Ma_CB)
	SELECT DISTINCT Nam, Ma_CanBo, Ma_CB
		FROM TL_BangLuong_KeHoach 
		WHERE Nam = @YearOfWork

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
		where Ma_CB in ('3.1', '3.2', '3.3') --Lấy quân số bình quân năm của cấp bậc CC, CN, VCQP
		
		UNION
		SELECT '9020001-010-011-0002-0001',
		count(1)/12 AS QSBQ_LDHD FROM @LuongKeHoach 
		where Ma_CB = '43' --Lấy quân số bình quân năm của cấp bậc LDHD
		
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_lay_luong_ke_hoach]    Script Date: 11/1/2023 1:49:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bhxh_lay_luong_ke_hoach]
	@NamLamViec int,
	@LuongChinh nvarchar(50),
	@PhuCapCV nvarchar(50),
	@PhuCapTNN nvarchar(50),
	@PhuCapTNVK nvarchar(50)
AS
BEGIN
declare @TBL_SiQuan table (TenPhuCap nvarchar(20), MaPhuCap nvarchar(200), GiaTri float);
declare @TBL_QNCN table (TenPhuCap nvarchar(20), MaPhuCap nvarchar(200), GiaTri float);
declare @TBL_HSQBS table (TenPhuCap nvarchar(20), MaPhuCap nvarchar(200), GiaTri float);
declare @TBL_VCQP table (TenPhuCap nvarchar(20), MaPhuCap nvarchar(200), GiaTri float);
declare @TBL_LDHD table (TenPhuCap nvarchar(20), MaPhuCap nvarchar(200), GiaTri float);

		--Lấy lương Sĩ quan
		SELECT
		'9020001-010-011-0001-0000' XauNoiMa,
		LKH.Ma_PhuCap MaPhuCap,
		SUM(IsNull(LKH.Gia_Tri, 0)) GiaTri
		FROM
			(SELECT 
				Id, 
				Ma_CB, 
				Ma_Hieu_CanBo, 
				Ma_PhuCap, 
				Gia_Tri, 
				Nam
			FROM TL_BangLuong_KeHoach
			WHERE
				Nam = @NamLamViec
				AND ((Ma_CB LIKE '1%' AND Ma_PhuCap in (@LuongChinh, @PhuCapCV, @PhuCapTNN, @PhuCapTNVK)))) LKH
			GROUP BY
				LKH.Ma_PhuCap,
				LKH.Nam

		--Lấy lương QNCN
		UNION
		SELECT
			'9020001-010-011-0001-0001',
			LKH.Ma_PhuCap MaPhuCap,
			SUM(IsNull(LKH.Gia_Tri, 0)) GiaTri
		FROM
			(SELECT 
			Id, 
			Ma_CB, 
			Ma_Hieu_CanBo, 
			Ma_PhuCap, 
			Gia_Tri, 
			Nam 
		FROM TL_BangLuong_KeHoach
		WHERE
			Nam = @NamLamViec
			AND ((Ma_CB LIKE '2%' AND Ma_PhuCap in (@LuongChinh, @PhuCapCV, @PhuCapTNN, @PhuCapTNVK)))) LKH
		GROUP BY
			LKH.Ma_PhuCap,
			LKH.Nam

		--Lấy lương HSQ_BS
		UNION
		SELECT
			'9020001-010-011-0001-0002',
			LKH.Ma_PhuCap MaPhuCap,
			SUM(IsNull(LKH.Gia_Tri, 0)) GiaTri
		FROM
			(SELECT 
			Id, 
			Ma_CB, 
			Ma_Hieu_CanBo, 
			Ma_PhuCap, 
			Gia_Tri, 
			Nam 
		FROM TL_BangLuong_KeHoach
		WHERE
			Nam = @NamLamViec
			AND ((Ma_CB LIKE '0%' AND Ma_PhuCap in (@LuongChinh, @PhuCapCV, @PhuCapTNN, @PhuCapTNVK)))) LKH
		GROUP BY
			LKH.Ma_PhuCap,
			LKH.Nam

		--Lấy lương VCQP
		UNION
		SELECT
			'9020001-010-011-0002-0000',
			LKH.Ma_PhuCap MaPhuCap,
			SUM(IsNull(LKH.Gia_Tri, 0)) GiaTri
		FROM
		(SELECT 
			Id, 
			Ma_CB, 
			Ma_Hieu_CanBo, 
			Ma_PhuCap, 
			Gia_Tri, 
			Nam 
		FROM TL_BangLuong_KeHoach
		WHERE
			Nam = @NamLamViec
			AND ((Ma_CB in ('3.1', '3.2', '3.3') AND Ma_PhuCap in (@LuongChinh, @PhuCapCV, @PhuCapTNN, @PhuCapTNVK)))) LKH
		GROUP BY
			LKH.Ma_PhuCap,
			LKH.Nam

		--Lấy lương Lao động hợp đông
		UNION
		SELECT
			'9020001-010-011-0002-0001',
			LKH.Ma_PhuCap MaPhuCap,
			SUM(IsNull(LKH.Gia_Tri, 0)) GiaTri
		FROM
			(SELECT
				Id, 
				Ma_CB, 
				Ma_Hieu_CanBo, 
				Ma_PhuCap, 
				Gia_Tri, 
				Nam 
		FROM TL_BangLuong_KeHoach
		WHERE
			Nam = @NamLamViec
			AND ((Ma_CB = '43' AND Ma_PhuCap in (@LuongChinh, @PhuCapCV, @PhuCapTNN, @PhuCapTNVK)))) LKH
		GROUP BY
			LKH.Ma_PhuCap,
			LKH.Nam;

	
END
GO
