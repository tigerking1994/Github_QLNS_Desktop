/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_truylinh_chuyenchedo_nq104]    Script Date: 3/26/2024 5:04:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_truylinh_chuyenchedo_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_truylinh_chuyenchedo_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_don_vi_nq104]    Script Date: 3/26/2024 5:04:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_don_vi_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_don_vi_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_don_vi_nq104]    Script Date: 3/26/2024 5:04:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_don_vi_nq104] @thang int, @nam int, @maDonVi nvarchar(MAX),
                                                                                                 @donViTinh int, @maCachTl nvarchar(5) AS DECLARE @Cols AS NVARCHAR(MAX) DECLARE @IsParent AS Bit
SET @IsParent = 0;


SET @Cols = 'LHT_TT,HSBL_TT,PCTNVK_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCCOV_TT,PCDACTHU_SUM,PCTRA_SUM,PCKHAC_SUM,LUONGTHANG_SUM,BHXHCN_TT,BHYTCN_TT,BHTNCN_TT,TA_TONG,THUETNCN_TT,TRICHLUONG_TT,GTKHAC_TT,PHAITRU_SUM,THANHTIEN,TM';

WITH BangLuongThang AS
  (SELECT dsCapNhapBangLuong.Thang AS Thang,
          dsCapNhapBangLuong.Nam AS Nam,
          bangLuong.ma_can_bo AS MaCanBo,
          bangLuong.ma_phu_cap AS MaPhuCap,
          bangLuong.GIA_TRI AS GiaTri
   FROM TL_BangLuong_Thang_Bridge_NQ104 bangLuong
   JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhapBangLuong ON bangLuong.parent = dsCapNhapBangLuong.Id --AND bangLuong.Ma_CB like @ngach + '%'

   WHERE bangLuong.ma_phu_cap IN
       (SELECT *
        FROM f_split(@Cols))
     AND dsCapNhapBangLuong.Ma_CachTL=@maCachTl
     AND dsCapNhapBangLuong.Ma_CBo IN
       (SELECT *
        FROM f_split(@maDonVi))
     AND dsCapNhapBangLuong.Thang=@thang
     AND dsCapNhapBangLuong.Nam=@nam
     AND dsCapNhapBangLuong.Status=1 ),

     ThongTinCanBo AS
  (SELECT canbo.Ma_CanBo MaCanBo,
          donvi.Ma_DonVi MaDonVi,
          donvi.Ten_DonVi DoiTuong,
          case when capbac.Parent IS NULL then capbac.ma_cb
		  when capbac.Parent = '3.3' then '3' 
		  else capbac.Parent end as Ngach
   FROM TL_DM_CanBo canBo
   LEFT JOIN TL_DM_DonVi donvi ON canBo.Parent = donvi.Ma_DonVi
   LEFT JOIN TL_DM_CapBac_NQ104 capbac ON canBo.ma_cb104 = capbac.Ma_Cb AND capbac.nam=@nam
   WHERE canBo.IsDelete = 1
     AND canBo.Khong_Luong = 0
	 AND canBo.Thang = @thang
	 AND canBo.Nam = @nam),
     CanBoLuongNgach AS
  (SELECT MaCanBo,
          Ngach,
          DoiTuong,
          MaDonVi,
          LHT_TT,
          HSBL_TT,
          PCTNVK_TT,
          PCCV_TT,
          PCTN_TT,
          PCKV_TT,
          PCCOV_TT,
          PCDACTHU_SUM,
          PCTRA_SUM,
          PCKHAC_SUM,
          LUONGTHANG_SUM,
          BHXHCN_TT,
          BHYTCN_TT,
          BHTNCN_TT,
          TA_TONG,
          THUETNCN_TT,
          TRICHLUONG_TT,
          GTKHAC_TT,
          PHAITRU_SUM,
          THANHTIEN,
		  TM
   FROM
     (SELECT bangLuong.Thang AS Thang,
             bangLuong.Nam AS Nam,
             canBo.MaCanBo,
             canBo.MaDonVi,
             canBo.DoiTuong,
             canBo.Ngach,
             bangLuong.GiaTri,
             bangLuong.MaPhuCap
      FROM BangLuongThang bangLuong
      INNER JOIN ThongTinCanBo canBo ON bangLuong.MaCanBo = canBo.MaCanBo) x PIVOT (SUM(GiaTri)
                                                                                    FOR MaPhuCap IN (LHT_TT, HSBL_TT, PCTNVK_TT, PCCV_TT, PCTN_TT, PCKV_TT, PCCOV_TT, PCDACTHU_SUM, PCTRA_SUM, PCKHAC_SUM, LUONGTHANG_SUM, BHXHCN_TT, BHYTCN_TT, BHTNCN_TT, TA_TONG, THUETNCN_TT, TRICHLUONG_TT, GTKHAC_TT, PHAITRU_SUM, THANHTIEN, TM)) pvt)

SELECT DoiTuong,
       MaDonVi,
       Ngach,
       COUNT(*) SoNguoi,
       SUM(LHT_TT)/@donViTinh LHT_TT,
       SUM(HSBL_TT)/@donViTinh HSBL_TT,
       SUM(PCTNVK_TT)/@donViTinh PCTNVK_TT,
       SUM(PCCV_TT)/@donViTinh PCCV_TT,
       SUM(PCTN_TT)/@donViTinh PCTN_TT,
       SUM(PCKV_TT)/@donViTinh PCKV_TT,
       SUM(PCCOV_TT)/@donViTinh PCCOV_TT,
       SUM(PCDACTHU_SUM)/@donViTinh PCDACTHU_SUM,
       SUM(PCTRA_SUM)/@donViTinh PCTRA_SUM,
       SUM(PCKHAC_SUM)/@donViTinh PCKHAC_SUM,
       SUM(LUONGTHANG_SUM)/@donViTinh LUONGTHANG_SUM,
       SUM(BHXHCN_TT)/@donViTinh BHXHCN_TT,
       SUM(BHYTCN_TT)/@donViTinh BHYTCN_TT,
       SUM(BHTNCN_TT)/@donViTinh BHTNCN_TT,
       SUM(TA_TONG)/@donViTinh TA_TONG,
       SUM(THUETNCN_TT)/@donViTinh THUETNCN_TT,
       SUM(TRICHLUONG_TT)/@donViTinh TRICHLUONG_TT,
       SUM(GTKHAC_TT/@donViTinh) GTKHAC_TT,
       SUM(PHAITRU_SUM)/@donViTinh PHAITRU_SUM,
       SUM(CASE WHEN TM = 0 THEN THANHTIEN ELSE 0 END)/@donViTinh THANHTIEN_NH,
       SUM(CASE WHEN TM = 1 THEN THANHTIEN ELSE 0 END)/@donViTinh THANHTIEN,
       @IsParent IsParent
FROM CanBoLuongNgach
GROUP BY DoiTuong,
         MaDonVi,
         Ngach
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_truylinh_chuyenchedo_nq104]    Script Date: 3/26/2024 5:04:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_truylinh_chuyenchedo_nq104] 
      @maDonVi nvarchar(MAX),                                    
	  @thangTruoc int, 
	  @namTruoc int, 
	  @thangSau int, 
	  @namSau int, 
	  @maHieuCanBo nvarchar(MAX),
	  @IsOrderChucVu bit = 0
	  AS 
IF @IsOrderChucVu = 1 
WITH BangLuongThangTruoc AS
  (SELECT dsCapNhapBangLuong.Thang AS Thang,
          dsCapNhapBangLuong.Nam AS Nam,
          bangLuong.ma_can_bo AS MaCanBo,
          bangLuong.ma_phu_cap AS MaPhuCap,
          bangLuong.GIA_TRI AS GiaTri,
          bangLuong.ma_hieu_can_bo AS MaHieuCanBo
   FROM TL_BangLuong_Thang_Bridge_NQ104 bangLuong
   JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhapBangLuong ON bangLuong.parent = dsCapNhapBangLuong.Id
   WHERE bangLuong.ma_phu_cap IN ('LHT_HS',
                                 'LHT_TT',
                                 'PCTN_TT',
                                 'PCCOV_TT',
                                 'BHXHCN_TT',
                                 'BHYTCN_TT',
                                 'TTL')
     AND dsCapNhapBangLuong.Ma_CachTL='CACH0'
     AND dsCapNhapBangLuong.Ma_CBo IN
       (SELECT *
        FROM f_split(@MaDonVi))
     AND dsCapNhapBangLuong.Thang=@thangTruoc
     AND dsCapNhapBangLuong.Nam=@namTruoc
     AND dsCapNhapBangLuong.Status=1),
                           CanBoThangTruoc AS
  (SELECT donVi.Ma_DonVi AS MaDonVi,
          donVi.Ten_Donvi AS TenDonvi,
          canBo.Ma_CanBo AS MaCanBo,
          canBo.Ten_CanBo AS TenCanBo,
          dbo.f_split_empty(canbo.Ten_CanBo) AS Ten,
          --ISNULL(chucVu.HeSo_Cv, 0) AS HSChucVu,
          canBo.Thang_TNN AS Tnn,
          canBo.Ma_Hieu_CanBo AS MaHieuCanBo,
          ISNULL(canBo.ma_cb104, '0') AS MaCapBac,
          ISNULL(capBac.Ten_Cb, '0') AS CapBac,
          CONCAT(canBo.So_TaiKhoan, ' ', canBo.Ma_CanBo) AS Stk,
          ISNULL(FORMAT(canBo.Ngay_NN, 'MM/yy'), '') AS NgayNhapNgu,
          ISNULL(FORMAT(canBo.Ngay_XN, 'MM/yy'), '') AS NgayXuatNgu,
          ISNULL(FORMAT(canBo.Ngay_TN, 'MM/yy'), '') AS NgayTaiNgu
   FROM TL_DM_CanBo canBo
   INNER JOIN TL_DM_DonVi donVi ON canBo.Parent=donVi.Ma_DonVi
   LEFT JOIN TL_DM_CapBac_NQ104 capBac ON canBo.ma_cb104=capBac.Ma_Cb AND capBac.nam=@namTruoc
   LEFT JOIN TL_DM_ChucVu_NQ104 chucVu ON canBo.ma_cvd104=chucVu.ma AND chucVu.nam=@namTruoc
   WHERE canBo.IsDelete = 1
     AND canBo.Ma_Hieu_CanBo IN
       (SELECT *
        FROM f_split(@maHieuCanBo))
     AND canBo.Khong_Luong = 0
     AND canBo.Thang=@thangTruoc
     AND canBo.Nam=@namTruoc),
                           ThangTruoc AS
  (SELECT MaDonVi,
          MaCanBo,
          TenCanBo,
          Ten,
          --HSChucVu,
          MaCapBac,
          MaHieuCanBo,
          LHT_HS AS HeSoCu,
          PCTN_TT AS PctnTtCu,
          PCCOV_TT AS PccovTtCu,
          BHXHCN_TT AS BhxhcnTtCu,
          BHYTCN_TT AS BhytcnTtCu,
          LHT_TT AS LhtTtCu
   FROM
     (SELECT bangLuong.Thang AS Thang,
             bangLuong.Nam AS Nam,
             canBoThangTruoc.MaDonVi,
             canBoThangTruoc.TenDonVi,
             canBoThangTruoc.MaCanBo,
             canBoThangTruoc.TenCanBo,
             canBoThangTruoc.Ten,
             --canBoThangTruoc.HSChucVu,
             canBoThangTruoc.MaCapBac,
             canBoThangTruoc.MaHieuCanBo,
             bangLuong.GiaTri,
             bangLuong.MaPhuCap
      FROM BangLuongThangTruoc bangLuong
      INNER JOIN CanBoThangTruoc canBoThangTruoc ON bangLuong.MaCanBo = canBoThangTruoc.MaCanBo) x PIVOT (SUM(GiaTri)
                                                                                                          FOR MaPhuCap IN (LHT_HS, LHT_TT, PCTN_TT, PCCOV_TT, BHXHCN_TT, BHYTCN_TT, TTL)) pvt),
                           BangLuongThangSau AS
  (SELECT dsCapNhapBangLuong.Thang AS Thang,
          dsCapNhapBangLuong.Nam AS Nam,
          bangLuong.ma_can_bo AS MaCanBo,
          bangLuong.ma_phu_cap AS MaPhuCap,
          bangLuong.GIA_TRI AS GiaTri,
          bangLuong.ma_hieu_can_bo AS MaHieuCanBo
   FROM TL_BangLuong_Thang_Bridge_NQ104 bangLuong
   JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhapBangLuong ON bangLuong.parent = dsCapNhapBangLuong.Id
   WHERE bangLuong.ma_phu_cap IN ('LHT_HS',
                                 'LHT_TT',
                                 'PCTN_TT',
                                 'PCCOV_TT',
                                 'BHXHCN_TT',
                                 'BHYTCN_TT',
                                 'TTL')
     AND dsCapNhapBangLuong.Ma_CachTL='CACH0'
     AND dsCapNhapBangLuong.Ma_CBo IN
       (SELECT *
        FROM f_split(@MaDonVi))
     AND dsCapNhapBangLuong.Thang=@thangSau
     AND dsCapNhapBangLuong.Nam=@namSau
     AND dsCapNhapBangLuong.Status=1),
                           CanBoThangSau AS
  (SELECT donVi.Ma_DonVi AS MaDonVi,
          donVi.Ten_Donvi AS TenDonvi,
          canBo.Ma_CanBo AS MaCanBo,
          canBo.Ten_CanBo AS TenCanBo,
          dbo.f_split_empty(canbo.Ten_CanBo) AS Ten,
          --ISNULL(chucVu.HeSo_Cv, 0) AS HSChucVu,
          canBo.Thang_TNN AS Tnn,
          canBo.Ma_Hieu_CanBo AS MaHieuCanBo,
          canBo.So_TaiKhoan AS SoTaiKhoan,
          ISNULL(canBo.ma_cb104, '0') AS MaCapBac,
          ISNULL(capBac.Ten_Cb, '0') AS CapBac,
          CONCAT(canBo.So_TaiKhoan, ' ', canBo.Ma_CanBo) AS Stk,
          ISNULL(FORMAT(canBo.Ngay_NN, 'MM/yy'), '') AS NgayNhapNgu,
          ISNULL(FORMAT(canBo.Ngay_XN, 'MM/yy'), '') AS NgayXuatNgu,
          ISNULL(FORMAT(canBo.Ngay_TN, 'MM/yy'), '') AS NgayTaiNgu
   FROM TL_DM_CanBo canBo
   INNER JOIN TL_DM_DonVi donVi ON canBo.Parent=donVi.Ma_DonVi
   LEFT JOIN TL_DM_CapBac_NQ104 capBac ON canBo.ma_cb104=capBac.Ma_Cb  AND capBac.nam=@namSau
   LEFT JOIN TL_DM_ChucVu_NQ104 chucVu ON canBo.ma_cvd104=chucVu.ma AND chucVu.nam=@namSau
   WHERE canBo.IsDelete = 1
     AND canBo.Ma_Hieu_CanBo IN
       (SELECT *
        FROM f_split(@maHieuCanBo))
     AND canBo.Khong_Luong = 0
     AND canBo.Thang=@thangSau
     AND canBo.Nam=@namSau),
                           ThangSau AS
  (SELECT MaDonVi,
          MaCanBo,
          TenCanBo,
          Ten,
          --HSChucVu,
          MaCapBac,
          MaHieuCanBo,
          SoTaiKhoan,
          NgayNhapNgu,
          NgayXuatNgu,
          NgayTaiNgu,
          LHT_HS AS HeSoMoi,
          PCTN_TT AS PctnTtMoi,
          PCCOV_TT AS PccovTtMoi,
          BHXHCN_TT AS BhxhcnTtMoi,
          BHYTCN_TT AS BhytcnTtMoi,
          LHT_TT AS LhtTtMoi,
          (CASE
               WHEN TTL = 0 THEN 1
               ELSE TTL
           END) AS TTL
   FROM
     (SELECT bangLuong.Thang AS Thang,
             bangLuong.Nam AS Nam,
             canBoThangTruoc.MaDonVi,
             canBoThangTruoc.TenDonVi,
             canBoThangTruoc.Ten,
             --canBoThangTruoc.HSChucVu,
             canBoThangTruoc.MaCanBo,
             canBoThangTruoc.TenCanBo,
             canBoThangTruoc.MaCapBac,
             canBoThangTruoc.MaHieuCanBo,
             canBoThangTruoc.NgayNhapNgu AS NgayNhapNgu,
             canBoThangTruoc.NgayXuatNgu AS NgayXuatNgu,
             canBoThangTruoc.NgayXuatNgu AS NgayTaiNgu,
             canBoThangTruoc.SoTaiKhoan AS SoTaiKhoan,
             bangLuong.GiaTri,
             bangLuong.MaPhuCap
      FROM BangLuongThangSau bangLuong
      INNER JOIN CanBoThangSau canBoThangTruoc ON bangLuong.MaCanBo = canBoThangTruoc.MaCanBo) x PIVOT (SUM(GiaTri)
                                                                                                        FOR MaPhuCap IN (LHT_HS, LHT_TT, PCTN_TT, PCCOV_TT, BHXHCN_TT, BHYTCN_TT, TTL)) pvt)
SELECT ThangSau.TenCanBo AS TenCanBo,
       ThangSau.TenCanBo AS Ten,
       ThangSau.TenCanBo AS HSChucVu,
       ThangSau.SoTaiKhoan AS SoTaiKhoan,
       ThangSau.MaCapBac AS MaCapBac,
       ThangSau.MaDonVi AS MaDonVi,
       NgayNhapNgu,--format(NgayNhapNgu, 'MM/yy') AS NgayNhapNgu,
 NgayXuatNgu,--format(NgayXuatNgu, 'MM/yy') AS NgayXuatNgu,
 NgayTaiNgu,--format(NgayTaiNgu, 'MM/yy') AS NgayTaiNgu,
 convert(decimal(10, 2), HeSoMoi) AS HeSoMoi,
 convert(decimal(10, 2), HeSoCu) AS HeSoCu,
 TTL,
 Ceiling(LhtTtMoi * TTL) AS LhtTtMoi,
 Ceiling(PctnTtMoi * TTL) AS PctnTtMoi,
 Ceiling(PccovTtMoi * TTL) AS PccovTtMoi,
 Ceiling(BhxhcnTtMoi * TTL) AS BhxhcnTtMoi,
 Ceiling(LhtTtCu * TTL) AS LhtTtCu,
 Ceiling(PctnTtCu * TTL) AS PctnTtCu,
 Ceiling(PccovTtCu * TTL) AS PccovTtCu,
 Ceiling(BhxhcnTtCu * TTL) AS BhxhcnTtCu,
 Ceiling(BhytcnTtCu * TTL) AS BhytcnTtCu
FROM ThangTruoc
JOIN ThangSau ON ThangTruoc.MaHieuCanBo = ThangSau.MaHieuCanBo
ORDER BY 
         ThangSau.MaCapBac,
         ThangSau.Ten 


ELSE 
WITH BangLuongThangTruoc AS
  (SELECT dsCapNhapBangLuong.Thang AS Thang,
          dsCapNhapBangLuong.Nam AS Nam,
          bangLuong.ma_can_bo AS MaCanBo,
          bangLuong.ma_phu_cap AS MaPhuCap,
          bangLuong.GIA_TRI AS GiaTri,
          bangLuong.ma_hieu_can_bo AS MaHieuCanBo
   FROM TL_BangLuong_Thang_Bridge_NQ104 bangLuong
   JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhapBangLuong ON bangLuong.parent = dsCapNhapBangLuong.Id
   WHERE bangLuong.ma_phu_cap IN ('LHT_HS',
                                 'LHT_TT',
                                 'PCTN_TT',
                                 'PCCOV_TT',
                                 'BHXHCN_TT',
                                 'BHYTCN_TT',
                                 'TTL')
     AND dsCapNhapBangLuong.Ma_CachTL='CACH0'
     AND dsCapNhapBangLuong.Ma_CBo IN
       (SELECT *
        FROM f_split(@MaDonVi))
     AND dsCapNhapBangLuong.Thang=@thangTruoc
     AND dsCapNhapBangLuong.Nam=@namTruoc
     AND dsCapNhapBangLuong.Status=1),
                                CanBoThangTruoc AS
  (SELECT donVi.Ma_DonVi AS MaDonVi,
          donVi.Ten_Donvi AS TenDonvi,
          canBo.Ma_CanBo AS MaCanBo,
          canBo.Ten_CanBo AS TenCanBo,
          dbo.f_split_empty(canbo.Ten_CanBo) AS Ten,
          --ISNULL(chucVu.HeSo_Cv, 0) AS HSChucVu,
          canBo.Thang_TNN AS Tnn,
          canBo.Ma_Hieu_CanBo AS MaHieuCanBo,
          ISNULL(canBo.ma_cb104, '0') AS MaCapBac,
          ISNULL(capBac.Ten_Cb, '0') AS CapBac,
          CONCAT(canBo.So_TaiKhoan, ' ', canBo.Ma_CanBo) AS Stk,
          ISNULL(FORMAT(canBo.Ngay_NN, 'MM/yy'), '') AS NgayNhapNgu,
          ISNULL(FORMAT(canBo.Ngay_XN, 'MM/yy'), '') AS NgayXuatNgu,
          ISNULL(FORMAT(canBo.Ngay_TN, 'MM/yy'), '') AS NgayTaiNgu
   FROM TL_DM_CanBo canBo
   INNER JOIN TL_DM_DonVi donVi ON canBo.Parent=donVi.Ma_DonVi
   LEFT JOIN TL_DM_CapBac_NQ104 capBac ON canBo.ma_cb104=capBac.Ma_Cb AND capBac.nam=@namTruoc
   LEFT JOIN TL_DM_ChucVu_NQ104 chucVu ON canBo.ma_cvd104=chucVu.ma AND chucVu.nam=@namTruoc
   WHERE canBo.IsDelete = 1
     AND canBo.Ma_Hieu_CanBo IN
       (SELECT *
        FROM f_split(@maHieuCanBo))
     AND canBo.Khong_Luong = 0
     AND canBo.Thang=@thangTruoc
     AND canBo.Nam=@namTruoc),
                                ThangTruoc AS
  (SELECT MaDonVi,
          MaCanBo,
          TenCanBo,
          Ten,
          --HSChucVu,
          MaCapBac,
          MaHieuCanBo,
          LHT_HS AS HeSoCu,
          PCTN_TT AS PctnTtCu,
          PCCOV_TT AS PccovTtCu,
          BHXHCN_TT AS BhxhcnTtCu,
          BHYTCN_TT AS BhytcnTtCu,
          LHT_TT AS LhtTtCu
   FROM
     (SELECT bangLuong.Thang AS Thang,
             bangLuong.Nam AS Nam,
             canBoThangTruoc.MaDonVi,
             canBoThangTruoc.TenDonVi,
             canBoThangTruoc.MaCanBo,
             canBoThangTruoc.TenCanBo,
             canBoThangTruoc.Ten,
             --canBoThangTruoc.HSChucVu,
             canBoThangTruoc.MaCapBac,
             canBoThangTruoc.MaHieuCanBo,
             bangLuong.GiaTri,
             bangLuong.MaPhuCap
      FROM BangLuongThangTruoc bangLuong
      INNER JOIN CanBoThangTruoc canBoThangTruoc ON bangLuong.MaCanBo = canBoThangTruoc.MaCanBo) x PIVOT (SUM(GiaTri)
                                                                                                          FOR MaPhuCap IN (LHT_HS, LHT_TT, PCTN_TT, PCCOV_TT, BHXHCN_TT, BHYTCN_TT, TTL)) pvt),
                                BangLuongThangSau AS
  (SELECT dsCapNhapBangLuong.Thang AS Thang,
          dsCapNhapBangLuong.Nam AS Nam,
          bangLuong.ma_can_bo AS MaCanBo,
          bangLuong.ma_phu_cap AS MaPhuCap,
          bangLuong.GIA_TRI AS GiaTri,
          bangLuong.ma_hieu_can_bo AS MaHieuCanBo
   FROM TL_BangLuong_Thang_Bridge_NQ104 bangLuong
   JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhapBangLuong ON bangLuong.parent = dsCapNhapBangLuong.Id
   WHERE bangLuong.ma_phu_cap IN ('LHT_HS',
                                 'LHT_TT',
                                 'PCTN_TT',
                                 'PCCOV_TT',
                                 'BHXHCN_TT',
                                 'BHYTCN_TT',
                                 'TTL')
     AND dsCapNhapBangLuong.Ma_CachTL='CACH0'
     AND dsCapNhapBangLuong.Ma_CBo IN
       (SELECT *
        FROM f_split(@MaDonVi))
     AND dsCapNhapBangLuong.Thang=@thangSau
     AND dsCapNhapBangLuong.Nam=@namSau
     AND dsCapNhapBangLuong.Status=1),
                                CanBoThangSau AS
  (SELECT donVi.Ma_DonVi AS MaDonVi,
          donVi.Ten_Donvi AS TenDonvi,
          canBo.Ma_CanBo AS MaCanBo,
          canBo.Ten_CanBo AS TenCanBo,
          dbo.f_split_empty(canbo.Ten_CanBo) AS Ten,
          --ISNULL(chucVu.HeSo_Cv, 0) AS HSChucVu,
          canBo.Thang_TNN AS Tnn,
          canBo.Ma_Hieu_CanBo AS MaHieuCanBo,
          canBo.So_TaiKhoan AS SoTaiKhoan,
          ISNULL(canBo.ma_cb104, '0') AS MaCapBac,
          ISNULL(capBac.Ten_Cb, '0') AS CapBac,
          CONCAT(canBo.So_TaiKhoan, ' ', canBo.Ma_CanBo) AS Stk,
          ISNULL(FORMAT(canBo.Ngay_NN, 'MM/yy'), '') AS NgayNhapNgu,
          ISNULL(FORMAT(canBo.Ngay_XN, 'MM/yy'), '') AS NgayXuatNgu,
          ISNULL(FORMAT(canBo.Ngay_TN, 'MM/yy'), '') AS NgayTaiNgu
   FROM TL_DM_CanBo canBo
   INNER JOIN TL_DM_DonVi donVi ON canBo.Parent=donVi.Ma_DonVi
   LEFT JOIN TL_DM_CapBac_NQ104 capBac ON canBo.ma_cb104=capBac.Ma_Cb AND capBac.nam=@namSau
   LEFT JOIN TL_DM_ChucVu_NQ104 chucVu ON canBo.ma_cvd104=chucVu.ma AND chucVu.nam=@namSau
   WHERE canBo.IsDelete = 1
     AND canBo.Ma_Hieu_CanBo IN
       (SELECT *
        FROM f_split(@maHieuCanBo))
     AND canBo.Khong_Luong = 0
     AND canBo.Thang=@thangSau
     AND canBo.Nam=@namSau),
                                ThangSau AS
  (SELECT MaDonVi,
          MaCanBo,
          TenCanBo,
          Ten,
          --HSChucVu,
          MaCapBac,
          MaHieuCanBo,
          SoTaiKhoan,
          NgayNhapNgu,
          NgayXuatNgu,
          NgayTaiNgu,
          LHT_HS AS HeSoMoi,
          PCTN_TT AS PctnTtMoi,
          PCCOV_TT AS PccovTtMoi,
          BHXHCN_TT AS BhxhcnTtMoi,
          BHYTCN_TT AS BhytcnTtMoi,
          LHT_TT AS LhtTtMoi,
          (CASE
               WHEN TTL = 0 THEN 1
               ELSE TTL
           END) AS TTL
   FROM
     (SELECT bangLuong.Thang AS Thang,
             bangLuong.Nam AS Nam,
             canBoThangTruoc.MaDonVi,
             canBoThangTruoc.TenDonVi,
             canBoThangTruoc.Ten,
             --canBoThangTruoc.HSChucVu,
             canBoThangTruoc.MaCanBo,
             canBoThangTruoc.TenCanBo,
             canBoThangTruoc.MaCapBac,
             canBoThangTruoc.MaHieuCanBo,
             canBoThangTruoc.NgayNhapNgu AS NgayNhapNgu,
             canBoThangTruoc.NgayXuatNgu AS NgayXuatNgu,
             canBoThangTruoc.NgayXuatNgu AS NgayTaiNgu,
             canBoThangTruoc.SoTaiKhoan AS SoTaiKhoan,
             bangLuong.GiaTri,
             bangLuong.MaPhuCap
      FROM BangLuongThangSau bangLuong
      INNER JOIN CanBoThangSau canBoThangTruoc ON bangLuong.MaCanBo = canBoThangTruoc.MaCanBo) x PIVOT (SUM(GiaTri)
                                                                                                        FOR MaPhuCap IN (LHT_HS, LHT_TT, PCTN_TT, PCCOV_TT, BHXHCN_TT, BHYTCN_TT, TTL)) pvt)
SELECT ThangSau.TenCanBo AS TenCanBo,
       ThangSau.TenCanBo AS Ten,
       ThangSau.TenCanBo AS HSChucVu,
       ThangSau.SoTaiKhoan AS SoTaiKhoan,
       ThangSau.MaCapBac AS MaCapBac,
       ThangSau.MaDonVi AS MaDonVi,
       NgayNhapNgu,--format(NgayNhapNgu, 'MM/yy') AS NgayNhapNgu,
 NgayXuatNgu,--format(NgayXuatNgu, 'MM/yy') AS NgayXuatNgu,
 NgayTaiNgu,--format(NgayTaiNgu, 'MM/yy') AS NgayTaiNgu,
 convert(decimal(10, 2), HeSoMoi) AS HeSoMoi,
 convert(decimal(10, 2), HeSoCu) AS HeSoCu,
 TTL,
 Ceiling(LhtTtMoi * TTL) AS LhtTtMoi,
 Ceiling(PctnTtMoi * TTL) AS PctnTtMoi,
 Ceiling(PccovTtMoi * TTL) AS PccovTtMoi,
 Ceiling(BhxhcnTtMoi * TTL) AS BhxhcnTtMoi,
 Ceiling(LhtTtCu * TTL) AS LhtTtCu,
 Ceiling(PctnTtCu * TTL) AS PctnTtCu,
 Ceiling(PccovTtCu * TTL) AS PccovTtCu,
 Ceiling(BhxhcnTtCu * TTL) AS BhxhcnTtCu,
 Ceiling(BhytcnTtCu * TTL) AS BhytcnTtCu
FROM ThangTruoc
JOIN ThangSau ON ThangTruoc.MaHieuCanBo = ThangSau.MaHieuCanBo
ORDER BY ThangSau.MaCapBac,
         ThangSau.Ten
;
;
;
GO
