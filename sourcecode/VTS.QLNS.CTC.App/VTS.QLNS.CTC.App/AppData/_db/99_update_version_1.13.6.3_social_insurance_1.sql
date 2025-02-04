/****** Object:  StoredProcedure [dbo].[sp_tl_bhxh_export_can_bo_che_do]    Script Date: 12/4/2023 8:40:19 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_bhxh_export_can_bo_che_do]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_bhxh_export_can_bo_che_do]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bang_luong_thang_bhxh_chitiet]    Script Date: 12/4/2023 8:40:19 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_bang_luong_thang_bhxh_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_bang_luong_thang_bhxh_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bang_luong_thang_bhxh_chitiet]    Script Date: 12/4/2023 8:40:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_tl_bang_luong_thang_bhxh_chitiet]
	@Id UNIQUEIDENTIFIER
AS
DECLARE @cols AS NVARCHAR(MAX);
DECLARE @query  AS NVARCHAR(MAX);

SET @cols = STUFF(
  (
    SELECT
      DISTINCT ',' + QUOTENAME(chedo.sMaCheDo) 
    FROM TL_DM_CheDoBHXH chedo where chedo.sMaCheDoCha is not null and sMaCheDoCha <> ''
								and sMaCheDo not in (select distinct sMaCheDoCha from TL_DM_CheDoBHXH)
        FOR XML PATH(''), TYPE
  ).value('.', 'NVARCHAR(MAX)'),1,1,'')
  
SET @query = '
	WITH ThongTinCanBo AS (
		SELECT
			canBo.Ma_CanBo	AS MaCanBo,
			canBo.Ten_CanBo	AS TenCanBo,
			donVi.Ma_DonVi	AS MaDonVi,
			donVi.Ten_DonVi	AS TenDonVi
		FROM TL_DM_CanBo canBo
		INNER JOIN TL_DM_DonVi donVi
			ON canBo.Parent = donVi.Ma_DonVi
		WHERE
			canBo.IsDelete = 1
			AND canBo.Khong_Luong = 0
	)
	SELECT Thang, Nam, MaCanBo, TenCanBo, TenDonVi, ' + @cols + ' FROM (
		SELECT
			bangLuongThang.iThang		AS Thang,
			bangLuongThang.iNam			AS Nam,
			canBo.MaCanBo				AS MaCanBo,
			canBo.TenCanBo				AS TenCanBo,
			canBo.TenDonVi				AS TenDonVi,
			bangLuongThang.nGiaTri		AS GiaTri,
			bangLuongThang.sMaCheDo	AS MaCheDo
		FROM TL_BangLuong_ThangBHXH bangLuongThang
		INNER JOIN ThongTinCanBo canBo
			ON bangLuongThang.sMaCBo = canBo.MaCanBo
		WHERE
			bangLuongThang.iID_Parent = + ''' + CONVERT(NVARCHAR(100), @Id) + '''
	) x
	PIVOT 
	(
		SUM(GiaTri)
		FOR MaCheDo IN (' + @cols + ')
	) p '
execute(@query)
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bhxh_export_can_bo_che_do]    Script Date: 12/4/2023 8:40:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_tl_bhxh_export_can_bo_che_do]
	@YearOfWork int,
	@Months nvarchar(20)
AS
BEGIN
	select distinct
		chedo.sXauNoiMaMlnsBHXH,
		canbo.Ma_Hieu_Canbo sMaHieuCanBo,
		canbo.Ten_CanBo sTenCanBo,
		canbo.Ten_DonVi sTenDonVi,
		canbo.Parent sMaDonVi,
		cbcd.sMaCanBo,
		cb.Ma_Cb SMaCapBac,
		cb.Ten_Cb STenCapBac,
		cbcd.sMaCheDo sMaCheDo,
		cbcd.fSoNgayHuongBHXH,
		cbcd.sSoQuyetDinh,
		cbcd.dNgayQuyetDinh,
		cbcd.iThangLuongCanCuDong,
		isnull(cbcd.fSoTien, 0) fSoTien,
		isnull(cbcd.fGiaTriCanCu, 0) fGiaTriCanCu
	from TL_CanBo_CheDoBHXH cbcd
		left join TL_DM_CanBo canbo on cbcd.sMaCanBo = canbo.Ma_CanBo
		join (
			select canbo.Ma_CanBo,
				capbac.Ma_Cb,
				capbac.Note Ten_Cb
			from TL_DM_CanBo canbo
			join TL_DM_CapBac capbac
			on canbo.Ma_CB = capbac.Ma_Cb
		) cb on cbcd.sMaCanBo = cb.Ma_CanBo
		left join TL_DM_CheDoBHXH chedo
			on cbcd.sMaCheDo = chedo.sMaCheDo
	where cbcd.iNam = @YearOfWork
			and cbcd.iThang in (SELECT * FROM f_split(@Months))
	order by canbo.Ma_Hieu_Canbo desc

END
GO
