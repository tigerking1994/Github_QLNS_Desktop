/****** Object:  StoredProcedure [dbo].[sp_bhxh_lay_luong_ke_hoach]    Script Date: 12/18/2023 3:12:29 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_lay_luong_ke_hoach]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_lay_luong_ke_hoach]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_lay_luong_ke_hoach]    Script Date: 12/18/2023 3:12:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bhxh_lay_luong_ke_hoach]
	@NamLamViec int,
	@LuongChinh nvarchar(50),
	@PhuCapCV nvarchar(50),
	@PhuCapTNN nvarchar(50),
	@PhuCapTNVK nvarchar(50),
	@lstIdChungTus nvarchar(max)
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
			FROM TL_BangLuong_KeHoach kh
			WHERE
				Nam = @NamLamViec
				AND ((Ma_CB LIKE '1%' AND Ma_PhuCap in (@LuongChinh, @PhuCapCV, @PhuCapTNN, @PhuCapTNVK)))
				AND kh.parent IN (SELECT * FROM splitstring(@lstIdChungTus))
				) LKH
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
		FROM TL_BangLuong_KeHoach kh
		WHERE
			Nam = @NamLamViec
			AND kh.parent IN (SELECT * FROM splitstring(@lstIdChungTus))
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
		FROM TL_BangLuong_KeHoach kh
		WHERE
			Nam = @NamLamViec
			AND kh.parent IN (SELECT * FROM splitstring(@lstIdChungTus))
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
		FROM TL_BangLuong_KeHoach kh
		WHERE
			Nam = @NamLamViec
			AND kh.parent IN (SELECT * FROM splitstring(@lstIdChungTus))
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
		FROM TL_BangLuong_KeHoach kh
		WHERE
			Nam = @NamLamViec
			AND kh.parent IN (SELECT * FROM splitstring(@lstIdChungTus))
			AND ((Ma_CB = '43' AND Ma_PhuCap in (@LuongChinh, @PhuCapCV, @PhuCapTNN, @PhuCapTNVK)))) LKH
		GROUP BY
			LKH.Ma_PhuCap,
			LKH.Nam;

	
END
;
GO
