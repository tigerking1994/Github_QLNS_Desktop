/****** Object:  StoredProcedure [dbo].[sp_luong_get_dmphucap_dctapthecanbo_nq104]    Script Date: 4/19/2024 5:26:07 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_luong_get_dmphucap_dctapthecanbo_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_luong_get_dmphucap_dctapthecanbo_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_luong_get_dmphucap_dctapthecanbo_nq104]    Script Date: 4/19/2024 5:26:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_luong_get_dmphucap_dctapthecanbo_nq104]
AS
BEGIN
	CREATE TABLE #tmpExclude(code nvarchar(200))
	INSERT INTO #tmpExclude(code) VALUES('THUONG_TT'),('GIAMTHUE_TT'),('THUNHAPKHAC_TT'),('THUEDANOP_TT'),('TIENCTLH_TT'),('TIENANDUONG_TT'),('TIENTAUXE_TT')

	CREATE TABLE #tmp(id uniqueidentifier, code nvarchar(200))
	CREATE TABLE #child(id uniqueidentifier)

	INSERT INTO #tmp(id, code)
	SELECT Id , Ma_PhuCap
	FROM TL_DM_PhuCap_NQ104 as pc
	LEFT JOIN #tmpExclude as ec on pc.Ma_PhuCap = ec.code
	WHERE ISNULL(Parent, '') = ''
	AND Chon = 1 AND Is_Formula = 0 AND Is_Readonly = 0 AND ec.code IS NULL

	INSERT INTO #child(id)
	SELECT dt.Id
	FROM #tmp as tmp
	INNER JOIN TL_DM_PhuCap_NQ104 as dt on tmp.code = dt.Parent
	LEFT JOIN #tmpExclude as ec on dt.Ma_PhuCap = ec.code
	WHERE dt.Chon = 1 AND Is_Formula = 0 AND Is_Readonly = 0 AND ec.code IS NULL

	SELECT tbl.*
	FROM #child as tmp
	INNER JOIN TL_DM_PhuCap_NQ104 AS tbl on tmp.id = tbl.Id

	DROP TABLE #tmp
	DROP TABLE #child
END
;
GO





/****** Object:  StoredProcedure [dbo].[sp_tl_find_canbo_dieuchinh_nq104]    Script Date: 4/22/2024 9:45:28 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_find_canbo_dieuchinh_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_find_canbo_dieuchinh_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_find_canbo_dieuchinh_nq104]    Script Date: 4/22/2024 9:45:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_find_canbo_dieuchinh_nq104] @thang int, @nam int, @maDonVi nvarchar(MAX),
                                                                         @maCapBac nvarchar(MAX),
                                                                                   @hskv float, @maTangGiam nvarchar(100),
                                                                                                              @maChucVu nvarchar(20),
                                                                                                                        @tienAn float,
																															@ngayNhapNgu datetime,
																																@isHsq bit AS BEGIN
if @hskv is not null or @tienAn is not null
With DieuKien1 as (SELECT canbo.*
	FROM TL_DM_CanBo_NQ104 canbo
	WHERE (@thang IS NOT NULL
		   AND canbo.Thang = @thang
		   OR @thang IS NULL)
	  AND (@nam IS NOT NULL
		   AND canbo.Nam = @nam
		   OR @nam IS NULL)
	  AND (@maDonVi IS NOT NULL
		   AND canbo.Parent IN (SELECT *
				FROM f_split(@maDonVi))
		   OR @maDonVi IS NULL)
	  AND (@maCapBac IS NOT NULL
		   AND canbo.Ma_CB IN (SELECT *
				FROM f_split(@maCapBac))
		   OR @maCapBac IS NULL)
	  AND (@maChucVu IS NOT NULL
		   AND canbo.Ma_CV = @maChucVu
		   OR @maChucVu IS NULL)
	  AND (@maTangGiam IS NOT NULL
		   AND canbo.Ma_TangGiam = @maTangGiam
		   OR @maTangGiam IS NULL)
	  AND (@ngayNhapNgu IS NOT NULL 
		   AND canbo.Ngay_NN = @ngayNhapNgu
		   OR @ngayNhapNgu IS NULL)
	  AND (@isHsq = 0
		   OR canbo.Ma_CB like '0%')),
	  DieuKien2 as (SELECT tbl.*
			FROM 
			(SELECT Ma_CBo MaCanBo , SUM(CASE WHEN Ma_PhuCap = 'PCKV_HS' AND GIA_TRI = 0.2 THEN 1 ELSE 0 END) as flg1,SUM(CASE WHEN Ma_PhuCap like N'TA%' AND GIA_TRI = 20000 THEN 1 ELSE 0 END) as flg2
				FROM TL_CanBo_PhuCap_NQ104 
				WHERE (Ma_PhuCap = 'PCKV_HS' AND GIA_TRI = 0.2) OR (Ma_PhuCap like N'TA%' AND GIA_TRI = 20000) 
				GROUP BY Ma_CBo) as tbl)
	 
	 Select DieuKien1.*, capbac.Note as CapBac, chucvu.Ten_Cv as ChucVu
	 From DieuKien1
	 Join DieuKien2 On DieuKien1.Ma_CanBo = DieuKien2.MaCanBo
	 Left Join Tl_Dm_CapBac capbac on DieuKien1.Ma_CB = capbac.Ma_Cb
	 Left Join Tl_Dm_ChucVu chucvu on DieuKien1.Ma_CV = chucvu.Ma_Cv
	 Where
	    (@hskv is not null and flg1 = 1 or @hskv is null)
		and (@tienAn is not null and flg2 = 1 or @tienAn is null)
else 
SELECT canbo.*, capbac.Note as CapBac, chucvu.ten as ChucVu
	FROM TL_DM_CanBo_NQ104 canbo
	Left Join Tl_Dm_CapBac_NQ104 capbac on canbo.ma_cb104 = capbac.Ma_Cb
	Left Join Tl_Dm_ChucVu_NQ104 chucvu on canbo.ma_cvd104 = chucvu.ma
	WHERE (@thang IS NOT NULL
		   AND canbo.Thang = @thang
		   OR @thang IS NULL)
	  AND (@nam IS NOT NULL
		   AND canbo.Nam = @nam
		   OR @nam IS NULL)
	  AND (@maDonVi IS NOT NULL
		   AND canbo.Parent IN (SELECT *
				FROM f_split(@maDonVi))
		   OR @maDonVi IS NULL)
	  AND (@maCapBac IS NOT NULL
		   AND canbo.Ma_CB IN (SELECT *
				FROM f_split(@maCapBac))
		   OR @maCapBac IS NULL)
	  AND (@maChucVu IS NOT NULL
		   AND canbo.Ma_CV = @maChucVu
		   OR @maChucVu IS NULL)
	  AND (@maTangGiam IS NOT NULL
		   AND canbo.Ma_TangGiam = @maTangGiam
		   OR @maTangGiam IS NULL)
	  AND (@ngayNhapNgu IS NOT NULL 
		   AND canbo.Ngay_NN = @ngayNhapNgu
		   OR @ngayNhapNgu IS NULL)
	  AND (@isHsq = 0
		   OR canbo.Ma_CB like '0%')
End
;
;
;
GO
