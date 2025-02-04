/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_dieuchinh_quanso_kehoach_nq104]    Script Date: 4/24/2024 8:22:08 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_dieuchinh_quanso_kehoach_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_dieuchinh_quanso_kehoach_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_find_canbo_dieuchinh_nq104]    Script Date: 4/24/2024 8:22:08 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_find_canbo_dieuchinh_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_find_canbo_dieuchinh_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_dieuchinh_phucap_nq104]    Script Date: 4/24/2024 8:22:08 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_dieuchinh_phucap_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_dieuchinh_phucap_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_dieuchinh_phucap_nq104]    Script Date: 4/24/2024 8:22:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_dieuchinh_phucap_nq104]
as
	Select
		TL_PhuCap_DieuChinh_NQ104.Id as Id,
		TL_DM_PhuCap_NQ104.Id as IdPhuCap,
		TL_DM_PhuCap_NQ104.Ma_PhuCap as MaPhuCap, 
		TL_DM_PhuCap_NQ104.Ten_PhuCap as TenPhuCap,
		TL_DM_PhuCap_NQ104.Gia_Tri as GiaTriCu,
		TL_PhuCap_DieuChinh_NQ104.GiaTri_Moi as GiaTriMoi,
		TL_PhuCap_DieuChinh_NQ104.ApDung_Tu as ApDungTu,
		TL_PhuCap_DieuChinh_NQ104.DateCreated as DateCreated,
		TL_PhuCap_DieuChinh_NQ104.UserCreator as UserCreator,
		TL_PhuCap_DieuChinh_NQ104.DateModified as DateModified,
		TL_PhuCap_DieuChinh_NQ104.UserModifier as UserModifier
	From TL_DM_PhuCap_NQ104
	Join TL_PhuCap_DieuChinh_NQ104
	On TL_DM_PhuCap_NQ104.Id = TL_PhuCap_DieuChinh_NQ104.Id_PhuCap
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_find_canbo_dieuchinh_nq104]    Script Date: 4/24/2024 8:22:08 AM ******/
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
	 
	 Select DieuKien1.*, capbac.ten_cb as CapBac, chucvu.ten as ChucVu
	 From DieuKien1
	 Join DieuKien2 On DieuKien1.Ma_CanBo = DieuKien2.MaCanBo
	 Left Join Tl_Dm_CapBac_NQ104 capbac on DieuKien1.ma_cb104 = capbac.Ma_Cb
	 Left Join Tl_Dm_ChucVu_NQ104 chucvu on DieuKien1.ma_cvd104 = chucvu.ma
	 Where
	    (@hskv is not null and flg1 = 1 or @hskv is null)
		and (@tienAn is not null and flg2 = 1 or @tienAn is null)
else 
SELECT canbo.*, capbac.ten_cb as CapBac, chucvu.ten as ChucVu
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
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_dieuchinh_quanso_kehoach_nq104]    Script Date: 4/24/2024 8:22:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_dieuchinh_quanso_kehoach_nq104] 
	@Nam int, @maDonVi nvarchar(50)
AS
Begin
Select 
	TL_DieuChinh_QS_KeHoach_Nq104.Thang As iThang,
	TL_DieuChinh_QS_KeHoach_Nq104.Tang_TuyenSinh As iTangTuyenQuan,
	TL_DieuChinh_QS_KeHoach_Nq104.Luong_TuyenSinh As fLuongTuyenSinh,
	TL_QS_KeHoach_ChiTiet_NQ104.fSoBinhNhi As fSoBinhNhi,
	CEILING(ISNULL(TL_QS_KeHoach_ChiTiet_NQ104.fPCRQ_BinhNhi, 0)) As fPcrqBinhNhi,
	TL_QS_KeHoach_ChiTiet_NQ104.fSoBinhNhat As fSoBinhNhat,
	CEILING(ISNULL(TL_QS_KeHoach_ChiTiet_NQ104.fPCRQ_BinhNhat, 0)) As fPcrqBinhNhat,
	TL_QS_KeHoach_ChiTiet_NQ104.fSoHaSi As fSoHaSi,
	CEILING(ISNULL(TL_QS_KeHoach_ChiTiet_NQ104.fPCRQ_HaSi, 0)) As fPcrqHaSi,
	TL_QS_KeHoach_ChiTiet_NQ104.fSoTrungSi As fSoTrungSi,
	CEILING(ISNULL(TL_QS_KeHoach_ChiTiet_NQ104.fPCRQ_TrungSi, 0)) As fPcrqTrungSi,
	TL_QS_KeHoach_ChiTiet_NQ104.fSoThuongSi As fSoThuongSi,
	CEILING(ISNULL(TL_QS_KeHoach_ChiTiet_NQ104.fPCRQ_ThuongSi, 0)) As fPcrqThuongSi
from TL_DieuChinh_QS_KeHoach_NQ104
join TL_QS_KeHoach_ChiTiet_NQ104
On TL_DieuChinh_QS_KeHoach_Nq104.Thang = TL_QS_KeHoach_ChiTiet_NQ104.Thang
and TL_DieuChinh_QS_KeHoach_Nq104.Nam = TL_QS_KeHoach_ChiTiet_NQ104.Nam
Where TL_DieuChinh_QS_KeHoach_Nq104.Nam = @Nam
and TL_DieuChinh_QS_KeHoach_Nq104.Ma_DonVi = @maDonVi
and TL_QS_KeHoach_ChiTiet_NQ104.Ma_DonVi = @maDonVi
and TL_QS_KeHoach_ChiTiet_NQ104.Nam = @Nam
order by TL_DieuChinh_QS_KeHoach_Nq104.Thang
End
;
;
;
GO


/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_phucap_kehoach_nq104]    Script Date: 4/25/2024 10:01:52 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_data_phucap_kehoach_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_data_phucap_kehoach_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_delete_can_bo_phu_cap_ke_hoach_nq104]    Script Date: 4/25/2024 10:01:52 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_delete_can_bo_phu_cap_ke_hoach_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_delete_can_bo_phu_cap_ke_hoach_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_delete_can_bo_phu_cap_ke_hoach_nq104]    Script Date: 4/25/2024 10:01:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_tl_delete_can_bo_phu_cap_ke_hoach_nq104]
	@maCanBo varchar(max)
As
Begin
Delete From TL_CanBo_PhuCap_KeHoach_NQ104
Where Ma_CanBo IN (SELECT * FROM f_split(@maCanBo))
End
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_phucap_kehoach_nq104]    Script Date: 4/25/2024 10:01:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_get_data_phucap_kehoach_nq104] @maCanBo AS nvarchar(50) AS BEGIN
SELECT PhuCap.Id AS Id,
       PhuCap.Ma_PhuCap AS MaPhuCap,
       PhuCap.Parent AS Parent,
	   PhuCap.Ten_PhuCap AS TenPhuCap,
	   PhuCap.bGiaTri as BGiaTri,
	   PhuCap.bHuongPc_Sn as BHuongPcSn,
       CONCAT(PhuCapCha.Ma_PhuCap, '-', PhuCapCha.Ten_PhuCap) AS ParentName,
       CanboPhucap.GIA_TRI AS GiaTri,
       CanboPhucap.DateStart AS DateStart,
       CanboPhucap.ISoThang_Huong AS ISoThangHuong,
       CanboPhucap.HuongPC_SN AS HuongPCSN,
		 PhuCap.FGiaTriNhoNhat,
		 PhuCap.FGiaTriLonNhat,
		 PhuCap.fGiaTriPhuCap_KemTheo as FGiaTriPhuCapKemTheo,
		 PhuCap.iId_PhuCap_KemTheo as IIdPhuCapKemTheo,
		 PhuCap.iId_Ma_PhuCap_KemTheo as IIdMaPhuCapKemTheo
FROM TL_DM_PhuCap_NQ104 PhuCap
LEFT JOIN TL_DM_PhuCap_NQ104 PhuCapCha ON PhuCap.Parent = PhuCapCha.Ma_PhuCap
LEFT JOIN TL_CanBo_PhuCap_KeHoach_NQ104 AS CanboPhucap ON PhuCap.Ma_PhuCap = CanboPhucap.MA_PHUCAP
AND CanboPhucap.Ma_CanBo = @maCanBo
WHERE PhuCap.Chon = 1
  AND PhuCap.Is_Formula = 0
  AND PhuCap.Is_Readonly = 0
  AND PhuCap.Parent IN ( select Ma_PhuCap from TL_DM_PhuCap_NQ104 where Parent = '' and Chon = 1)
Order By ParentName
END
;
;
;
;
GO
