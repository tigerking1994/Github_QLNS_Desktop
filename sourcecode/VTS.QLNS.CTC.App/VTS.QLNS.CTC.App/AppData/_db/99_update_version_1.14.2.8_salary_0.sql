/****** Object:  StoredProcedure [dbo].[sp_tl_update_dmphucap_canbo_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_update_dmphucap_canbo_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_update_dmphucap_canbo_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_update_dmcapbac_canbo_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_update_dmcapbac_canbo_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_update_dmcapbac_canbo_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_truylinh_chuyenchedo_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_truylinh_chuyenchedo_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_truylinh_chuyenchedo_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_don_vi_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_don_vi_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_don_vi_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_tonghop_luong_theo_don_vi_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_tonghop_luong_theo_don_vi_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_tonghop_luong_theo_don_vi_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_tien_an_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_tien_an_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_tien_an_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_quyettoan_nam_thue_tncn_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_quyettoan_nam_thue_tncn_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_quyettoan_nam_thue_tncn_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_kehoach_chitiet_canbo_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_kehoach_chitiet_canbo_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_kehoach_chitiet_canbo_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_giaithich_songay_phucap_theongay_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_giaithich_songay_phucap_theongay_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_giaithich_songay_phucap_theongay_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_giaithich_phucap_theongay_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_giaithich_phucap_theongay_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_giaithich_phucap_theongay_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_giaithich_phucap_bienphong_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_giaithich_phucap_bienphong_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_giaithich_phucap_bienphong_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_giaithich_phucap_bienphong_heso_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_giaithich_phucap_bienphong_heso_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_giaithich_phucap_bienphong_heso_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_giaithich_chitiet_raquan_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_giaithich_chitiet_raquan_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_giaithich_chitiet_raquan_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_giaithich_chitiet_phucap_vuotkhung_handinh_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_giaithich_chitiet_phucap_vuotkhung_handinh_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_giaithich_chitiet_phucap_vuotkhung_handinh_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_giaithich_chitiet_phucap_truylinh_khac_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_giaithich_chitiet_phucap_truylinh_khac_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_giaithich_chitiet_phucap_truylinh_khac_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_giaithich_chitiet_phucap_khac_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_giaithich_chitiet_phucap_khac_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_giaithich_chitiet_phucap_khac_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_giaithich_chitiet_phucap_hsq_cs_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_giaithich_chitiet_phucap_hsq_cs_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_giaithich_chitiet_phucap_hsq_cs_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_ds_chitra_nganhang_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_ds_chitra_nganhang_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_ds_chitra_nganhang_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_dienbien_luong_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_dienbien_luong_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_dienbien_luong_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_danhsach_chitra_nganhang_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_danhsach_chitra_nganhang_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_danhsach_chitra_nganhang_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_danhsach_capphat_phucap_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_danhsach_capphat_phucap_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_danhsach_capphat_phucap_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_chitiet_quanso_tanggiam_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_chitiet_quanso_tanggiam_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_chitiet_quanso_tanggiam_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_truylinhchuyenchedo_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangthanhtoan_truylinhchuyenchedo_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_truylinhchuyenchedo_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh_2_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh_2_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh_2_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangthanhtoan_luongthang_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_dong_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangthanhtoan_luongthang_dong_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_dong_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_2_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangthanhtoan_luongthang_2_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_2_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangluong_truylinh_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangluong_truylinh_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangluong_truylinh_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangluong_truylinh_dong_phucap]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangluong_truylinh_dong_phucap]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangluong_truylinh_dong_phucap]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangluong_thang_giaithich_chitiet_theo_ngach_capbac_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangluong_thang_giaithich_chitiet_theo_ngach_capbac_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangluong_thang_giaithich_chitiet_theo_ngach_capbac_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangke_trichthue_tncn_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangke_trichthue_tncn_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangke_trichthue_tncn_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_lay_du_lieu_luong_thang_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_lay_du_lieu_luong_thang_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_lay_du_lieu_luong_thang_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_lay_du_lieu_bang_luong_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_lay_du_lieu_bang_luong_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_lay_du_lieu_bang_luong_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_donvi_tao_bangluong_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_donvi_tao_bangluong_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_donvi_tao_bangluong_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_donvi_quanso_nam_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_donvi_quanso_nam_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_donvi_quanso_nam_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_donvi_baocao_quanso_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_donvi_baocao_quanso_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_donvi_baocao_quanso_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_donvi_bangluong_thang_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_donvi_bangluong_thang_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_donvi_bangluong_thang_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_donvi_bang_phucap_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_donvi_bang_phucap_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_donvi_bang_phucap_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_thue_tncn_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_data_thue_tncn_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_data_thue_tncn_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_ra_quan_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_data_ra_quan_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_data_ra_quan_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_find_danhsach_canbo_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_find_danhsach_canbo_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_find_danhsach_canbo_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_find_danhsach_canbo_by_condition_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_find_danhsach_canbo_by_condition_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_find_danhsach_canbo_by_condition_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_find_canbo_quyettoan_quanso_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_find_canbo_quyettoan_quanso_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_find_canbo_quyettoan_quanso_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_find_canbo_quyettoan_quanso_giam_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_find_canbo_quyettoan_quanso_giam_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_find_canbo_quyettoan_quanso_giam_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_export_bang_luong_thang_bhxh_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_export_bang_luong_thang_bhxh_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_export_bang_luong_thang_bhxh_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_delete_can_bo_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_delete_can_bo_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_delete_can_bo_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_danhsach_chitra_nganhang_thunhapkhac_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_danhsach_chitra_nganhang_thunhapkhac_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_danhsach_chitra_nganhang_thunhapkhac_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_chungtu_chitiet_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_chungtu_chitiet_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_chungtu_chitiet_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_cb_phucap_inkiem_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_cb_phucap_inkiem_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_cb_phucap_inkiem_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_TL_CanBo_PhuCap_NQ104_saochep_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_TL_CanBo_PhuCap_NQ104_saochep_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_TL_CanBo_PhuCap_NQ104_saochep_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bhxh_export_can_bo_che_do_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_bhxh_export_can_bo_che_do_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_bhxh_export_can_bo_che_do_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_baocao_phantich_tienan_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_baocao_phantich_tienan_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_baocao_phantich_tienan_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_baocao_luong_nam_ke_hoach_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_baocao_luong_nam_ke_hoach_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_baocao_luong_nam_ke_hoach_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bangthanhtoan_truylinhchuyenchedoqncn_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_bangthanhtoan_truylinhchuyenchedoqncn_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_bangthanhtoan_truylinhchuyenchedoqncn_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_truylinh_dulieu_insert_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_bangluong_truylinh_dulieu_insert_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_bangluong_truylinh_dulieu_insert_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_TL_BangLuong_Thang_NQ104_dulieu_insert_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_TL_BangLuong_Thang_NQ104_dulieu_insert_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_TL_BangLuong_Thang_NQ104_dulieu_insert_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_TL_BangLuong_Thang_NQ104_chitiet_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_TL_BangLuong_Thang_NQ104_chitiet_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_TL_BangLuong_Thang_NQ104_chitiet_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_thang_dulieu_insert_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_bangluong_thang_dulieu_insert_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_bangluong_thang_dulieu_insert_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_thang_chitiet_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_bangluong_thang_chitiet_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_bangluong_thang_chitiet_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bang_tonghop_luong_phucap_bienphong_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_bang_tonghop_luong_phucap_bienphong_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_bang_tonghop_luong_phucap_bienphong_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bang_luong_thang_dong_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_bang_luong_thang_dong_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_bang_luong_thang_dong_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bang_luong_thang_bhxh_chitiet_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_bang_luong_thang_bhxh_chitiet_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_bang_luong_thang_bhxh_chitiet_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_giai_thich_che_do_om_dau_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_qtcq_giai_thich_che_do_om_dau_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_qtcq_giai_thich_che_do_om_dau_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_xuat_ngu_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_luong_tro_cap_xuat_ngu_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_xuat_ngu_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_tnld_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_luong_tro_cap_tnld_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_tnld_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_thai_san_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_luong_tro_cap_thai_san_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_thai_san_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_om_dau_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_luong_tro_cap_om_dau_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_om_dau_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_ht_pv_tv_tt_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_luong_tro_cap_ht_pv_tv_tt_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_ht_pv_tv_tt_nq104]
GO
/****** Object:  StoredProcedure [dbo].[rpt_ds_chi_tra_ca_nhan_ngan_hang_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rpt_ds_chi_tra_ca_nhan_ngan_hang_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rpt_ds_chi_tra_ca_nhan_ngan_hang_nq104]
GO
/****** Object:  StoredProcedure [dbo].[rpt_ds_chi_tra_ca_nhan_ngan_hang_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[rpt_ds_chi_tra_ca_nhan_ngan_hang_nq104]
	-- Add the parameters for the stored procedure here
	@thang int,
	@nam int,
	@maDonVi varchar(500)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT
		canBo.Parent MaDonVi,
		canBo.Ma_CanBo MaCanBo,
		canBo.Ten_CanBo TenCanBo,
		--ISNULL(chucVu.HeSo_Cv, 0) HSChucVu,
		capBac.ma_cb MaCapBac,
		canBo.So_TaiKhoan SoTaiKhoan,
		canBo.Ten_KhoBac NganHang,
		CEILING(ISNULL(bangLuong.Gia_Tri, 0)) THANHTIEN
	FROM TL_BangLuong_Thang_Bridge_NQ104 bangLuong
	INNER JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhatBangLuong
		ON bangLuong.parent = dsCapNhatBangLuong.Id
	INNER JOIN TL_DM_CanBo_NQ104 canBo
		ON canBo.Ma_CanBo = bangLuong.ma_can_bo
	LEFT JOIN TL_DM_ChucVu_NQ104 chucVu
		ON canBo.Ma_CVd104 = chucVu.ma AND chucVu.nam=@Nam
	LEFT JOIN TL_DM_CapBac_NQ104 capBac
		ON canBo.Ma_CB104 = capBac.Ma_Cb AND capBac.nam=@Nam
	WHERE
		bangLuong.ma_phu_cap = 'THANHTIEN'
		AND canBo.TM = 1
		AND ISNULL(bangLuong.Gia_Tri, 0) > 0
		AND dsCapNhatBangLuong.Thang = @thang
		AND dsCapNhatBangLuong.Nam = @nam
		AND canBo.Parent in (SELECT * FROM dbo.splitstring(@maDonVi))
	ORDER BY MaDonVi DESC, MaCapBac DESC, TenCanBo DESC
END

/****** Object:  StoredProcedure [dbo].[sp_khluachonnhathau_get_nguonvon_by_lcnt_update]    Script Date: 15/12/2021 6:36:38 PM ******/
SET ANSI_NULLS ON
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_ht_pv_tv_tt_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_ht_pv_tv_tt_nq104]
	@DsMaDonVi nvarchar(100),
	@NamLamViec int,
	@Thang int,
	@DonViTinh int
AS
BEGIN
	--Lay thong tin luong theo TC_HUUTRI, TC_PHUCVIEN, TC_THOIVIEC, TC_TUTUAT
	select * into TBL_HUUTRI from
	(select donvi.Ten_DonVi, luong.*
	from TL_BangLuong_Thang_NQ104BHXH luong
	join TL_DM_DonVi_NQ104 donvi on luong.sMaDonVi = donvi.Ma_DonVi
	where 
	luong.sMaDonVi in (SELECT * FROM f_split(@DsMaDonVi))
	and luong.iNam = @NamLamViec
	and luong.iThang = @Thang
	and luong.sMaCheDo in ('HUUTRI_TROCAP1LAN', 'HUUTRI_TROCAPKHUVUC', 'PHUCVIEN_TROCAP1LAN', 'PHUCVIEN_TROCAPKHUVUC', 'THOIVIEC_TROCAP1LAN', 'THOIVIEC_TROCAPKHUVUC', 'TUTUAT_TROCAP1LAN', 'TUTUAT_TROCAPKHUVUC', 'TROCAPMAITANG')) HUUTRI

	-- Data tro cap Huu tri
	select distinct
		TBL_HUUTRI.sMaCB,
		TBL_HUUTRI.sMaCBo,
		--TBL_HUUTRI.sMaCheDo,
		TBL_HUUTRI.sTenCbo,
		TBL_HUUTRI.Ten_DonVi,
		HUUTRI_TROCAP1LAN.nGiaTri fHUUTRI_TROCAP1LAN,
		HUUTRI_TROCAPKHUVUC.nGiaTri fHUUTRI_TROCAPKHUVUC,
		chedocha.sSoQuyetDinh,
		chedocha.dNgayQuyetDinh
		into TBL_HUUTRI_DOC
	from TBL_HUUTRI TBL_HUUTRI
		left join (select top 1 * from TL_CanBo_CheDoBHXH 
		where sMaCheDo in ('HUUTRI_TROCAPKHUVUC', 'HUUTRI_TROCAP1LAN')
		and ((sSoQuyetDinh is not null and sSoQuyetDinh <> '') 
		or (dNgayQuyetDinh is not null and dNgayQuyetDinh <> ''))) chedocha
		on TBL_HUUTRI.sMaCBo = chedocha.sMaCanBo
		left join
		(select HUUTRI.Id, HUUTRI.sMaDonVi, HUUTRI.nGiaTri, HUUTRI.sMaCB, HUUTRI.sMaCBo, HUUTRI.sMaCheDo, HUUTRI.sTenCbo
		from TBL_HUUTRI HUUTRI
		where HUUTRI.sMaCheDo = 'HUUTRI_TROCAPKHUVUC') HUUTRI_TROCAPKHUVUC
		on TBL_HUUTRI.sMaCBo = HUUTRI_TROCAPKHUVUC.sMaCBo and TBL_HUUTRI.sMaDonVi = HUUTRI_TROCAPKHUVUC.sMaDonVi
		left join
		(select HUUTRI2.Id, HUUTRI2.sMaDonVi, HUUTRI2.nGiaTri, HUUTRI2.sMaCB, HUUTRI2.sMaCBo, HUUTRI2.sMaCheDo, HUUTRI2.sTenCbo
		from TBL_HUUTRI HUUTRI2
		where HUUTRI2.sMaCheDo = 'HUUTRI_TROCAP1LAN') HUUTRI_TROCAP1LAN
		on TBL_HUUTRI.sMaCBo = HUUTRI_TROCAP1LAN.sMaCBo and TBL_HUUTRI.sMaDonVi = HUUTRI_TROCAP1LAN.sMaDonVi

	-- Data tro cap Phuc vien
	select distinct
		TBL_HUUTRI.sMaCB,
		TBL_HUUTRI.sMaCBo,
		TBL_HUUTRI.sMaCheDo,
		TBL_HUUTRI.sTenCbo,
		TBL_HUUTRI.Ten_DonVi,
		PHUCVIEN_TROCAP1LAN.nGiaTri fPHUCVIEN_TROCAP1LAN,
		PHUCVIEN_TROCAPKHUVUC.nGiaTri fPHUCVIEN_TROCAPKHUVUC,
		chedocha.sSoQuyetDinh,
		chedocha.dNgayQuyetDinh
		into TBL_PHUCVIEN_DOC
	from TBL_HUUTRI TBL_HUUTRI
		left join (select top 1 * from TL_CanBo_CheDoBHXH 
		where sMaCheDo in ('PHUCVIEN_TROCAPKHUVUC', 'PHUCVIEN_TROCAP1LAN')
		and ((sSoQuyetDinh is not null and sSoQuyetDinh <> '') 
		or (dNgayQuyetDinh is not null and dNgayQuyetDinh <> ''))) chedocha
		on TBL_HUUTRI.sMaCBo = chedocha.sMaCanBo
		left join
		(select HUUTRI.Id, HUUTRI.sMaDonVi, HUUTRI.nGiaTri, HUUTRI.sMaCB, HUUTRI.sMaCBo, HUUTRI.sMaCheDo, HUUTRI.sTenCbo
		from TBL_HUUTRI HUUTRI
		where HUUTRI.sMaCheDo = 'PHUCVIEN_TROCAPKHUVUC') PHUCVIEN_TROCAPKHUVUC
		on TBL_HUUTRI.sMaCBo = PHUCVIEN_TROCAPKHUVUC.sMaCBo and TBL_HUUTRI.sMaDonVi = PHUCVIEN_TROCAPKHUVUC.sMaDonVi
		left join
		(select HUUTRI2.Id, HUUTRI2.sMaDonVi, HUUTRI2.nGiaTri, HUUTRI2.sMaCB, HUUTRI2.sMaCBo, HUUTRI2.sMaCheDo, HUUTRI2.sTenCbo
		from TBL_HUUTRI HUUTRI2
		where HUUTRI2.sMaCheDo = 'PHUCVIEN_TROCAP1LAN') PHUCVIEN_TROCAP1LAN
		on TBL_HUUTRI.sMaCBo = PHUCVIEN_TROCAP1LAN.sMaCBo and TBL_HUUTRI.sMaDonVi = PHUCVIEN_TROCAP1LAN.sMaDonVi

	-- Data tro cap Thoi Viec
	select distinct
		TBL_HUUTRI.sMaCB,
		TBL_HUUTRI.sMaCBo,
		TBL_HUUTRI.sMaCheDo,
		TBL_HUUTRI.sTenCbo,
		TBL_HUUTRI.Ten_DonVi,
		THOIVIEC_TROCAP1LAN.nGiaTri fTHOIVIEC_TROCAP1LAN,
		THOIVIEC_TROCAPKHUVUC.nGiaTri fTHOIVIEC_TROCAPKHUVUC,
		chedocha.sSoQuyetDinh,
		chedocha.dNgayQuyetDinh
		into TBL_THOIVIEC_DOC
	from TBL_HUUTRI TBL_HUUTRI
		left join (select top 1 * from TL_CanBo_CheDoBHXH 
		where sMaCheDo in ('THOIVIEC_TROCAPKHUVUC', 'THOIVIEC_TROCAP1LAN')
		and ((sSoQuyetDinh is not null and sSoQuyetDinh <> '') 
		or (dNgayQuyetDinh is not null and dNgayQuyetDinh <> ''))) chedocha
		on TBL_HUUTRI.sMaCBo = chedocha.sMaCanBo
		left join
		(select HUUTRI.Id, HUUTRI.sMaDonVi, HUUTRI.nGiaTri, HUUTRI.sMaCB, HUUTRI.sMaCBo, HUUTRI.sMaCheDo, HUUTRI.sTenCbo
		from TBL_HUUTRI HUUTRI
		where HUUTRI.sMaCheDo = 'THOIVIEC_TROCAPKHUVUC') THOIVIEC_TROCAPKHUVUC
		on TBL_HUUTRI.sMaCBo = THOIVIEC_TROCAPKHUVUC.sMaCBo and TBL_HUUTRI.sMaDonVi = THOIVIEC_TROCAPKHUVUC.sMaDonVi
		left join
		(select HUUTRI2.Id, HUUTRI2.sMaDonVi, HUUTRI2.nGiaTri, HUUTRI2.sMaCB, HUUTRI2.sMaCBo, HUUTRI2.sMaCheDo, HUUTRI2.sTenCbo
		from TBL_HUUTRI HUUTRI2
		where HUUTRI2.sMaCheDo = 'THOIVIEC_TROCAP1LAN') THOIVIEC_TROCAP1LAN
		on TBL_HUUTRI.sMaCBo = THOIVIEC_TROCAP1LAN.sMaCBo and TBL_HUUTRI.sMaDonVi = THOIVIEC_TROCAP1LAN.sMaDonVi

	-- Data tro cap Tu tuat
	select distinct
		TBL_HUUTRI.sMaCB,
		TBL_HUUTRI.sMaCBo,
		TBL_HUUTRI.sMaCheDo,
		TBL_HUUTRI.sTenCbo,
		TBL_HUUTRI.Ten_DonVi,
		TUTUAT_TROCAP1LAN.nGiaTri fTUTUAT_TROCAP1LAN,
		TUTUAT_TROCAPKHUVUC.nGiaTri fTUTUAT_TROCAPKHUVUC,
		TROCAPMAITANG.nGiaTri fTROCAPMAITANG,
		chedocha.sSoQuyetDinh,
		chedocha.dNgayQuyetDinh
		into TBL_TUTUAT_DOC
	from TBL_HUUTRI TBL_HUUTRI
		left join (select top 1 * from TL_CanBo_CheDoBHXH 
		where sMaCheDo in ('TUTUAT_TROCAPKHUVUC', 'TROCAPMAITANG', 'TUTUAT_TROCAP1LAN')
		and ((sSoQuyetDinh is not null and sSoQuyetDinh <> '') 
		or (dNgayQuyetDinh is not null and dNgayQuyetDinh <> ''))) chedocha
		on TBL_HUUTRI.sMaCBo = chedocha.sMaCanBo
		left join
		(select HUUTRI.Id, HUUTRI.sMaDonVi, HUUTRI.nGiaTri, HUUTRI.sMaCB, HUUTRI.sMaCBo, HUUTRI.sMaCheDo, HUUTRI.sTenCbo
		from TBL_HUUTRI HUUTRI
		where HUUTRI.sMaCheDo = 'TUTUAT_TROCAPKHUVUC') TUTUAT_TROCAPKHUVUC
		on TBL_HUUTRI.sMaCBo = TUTUAT_TROCAPKHUVUC.sMaCBo and TBL_HUUTRI.sMaDonVi = TUTUAT_TROCAPKHUVUC.sMaDonVi
		left join
		(select HUUTRI1.Id, HUUTRI1.sMaDonVi, HUUTRI1.nGiaTri, HUUTRI1.sMaCB, HUUTRI1.sMaCBo, HUUTRI1.sMaCheDo, HUUTRI1.sTenCbo
		from TBL_HUUTRI HUUTRI1
		where HUUTRI1.sMaCheDo = 'TROCAPMAITANG') TROCAPMAITANG
		on TBL_HUUTRI.sMaCBo = TROCAPMAITANG.sMaCBo and TBL_HUUTRI.sMaDonVi = TROCAPMAITANG.sMaDonVi
		left join
		(select HUUTRI2.Id, HUUTRI2.sMaDonVi, HUUTRI2.nGiaTri, HUUTRI2.sMaCB, HUUTRI2.sMaCBo, HUUTRI2.sMaCheDo, HUUTRI2.sTenCbo
		from TBL_HUUTRI HUUTRI2
		where HUUTRI2.sMaCheDo = 'TUTUAT_TROCAP1LAN') TUTUAT_TROCAP1LAN
		on TBL_HUUTRI.sMaCBo = TUTUAT_TROCAP1LAN.sMaCBo and TBL_HUUTRI.sMaDonVi = TUTUAT_TROCAP1LAN.sMaDonVi

	--Lay tro cap Huu tri Si quan
	select * into TBL_HUUTRI_SQ from
	(select 1 bHangCha, 'I' LoaiTC, 1 RowNum, 'I' STT, null DoiTuong, null LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, N'TC Hưu trí' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 1 bHangCha, 'I' LoaiTC,  1 RowNum, null STT, 'SQ' DoiTuong, 'SQ' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'SQ' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 0 bHangCha, 'I' LoaiTC,  1 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'SQ' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fHUUTRI_TROCAP1LAN fTROCAP1LAN, fHUUTRI_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	from TBL_HUUTRI_DOC
	where sMaCB like '1%' and (isnull(fHUUTRI_TROCAP1LAN, 0) <> 0 or isnull(fHUUTRI_TROCAPKHUVUC, 0) <> 0)) sq
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_HUUTRI_SQ) > 2
		update TBL_HUUTRI_SQ set bHasData = 1

	--Lay tro cap Huu tri QNCN
	select * into TBL_HUUTRI_QNCN from
	(select 1 bHangCha, 'I' LoaiTC, 1 RowNum, 'I' STT, null DoiTuong, null LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, N'TC Hưu trí' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 1 bHangCha, 'I' LoaiTC,  2 RowNum, null STT, 'QNCN' DoiTuong, 'QNCN' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'QNCN' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 0 bHangCha, 'I' LoaiTC,  2 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'QNCN' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fHUUTRI_TROCAP1LAN fTROCAP1LAN, fHUUTRI_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	from TBL_HUUTRI_DOC
	where sMaCB like '2%' and (isnull(fHUUTRI_TROCAP1LAN, 0) <> 0 or isnull(fHUUTRI_TROCAPKHUVUC, 0) <> 0)) qncn
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_HUUTRI_QNCN) > 2
		update TBL_HUUTRI_QNCN set bHasData = 1

	--Lay tro cap Huu tri HSQ_BS
	select * into TBL_HUUTRI_HSQBS from
	(select 1 bHangCha, 'I' LoaiTC, 1 RowNum, 'I' STT, null DoiTuong, null LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, N'TC Hưu trí' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 1 bHangCha, 'I' LoaiTC,  3 RowNum, null STT, 'HSQ_BS' DoiTuong, 'HSQ_BS' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'HSQ_BS' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 0 bHangCha, 'I' LoaiTC,  3 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'HSQ_BS' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fHUUTRI_TROCAP1LAN fTROCAP1LAN, fHUUTRI_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	from TBL_HUUTRI_DOC
	where sMaCB like '0%' and (isnull(fHUUTRI_TROCAP1LAN, 0) <> 0 or isnull(fHUUTRI_TROCAPKHUVUC, 0) <> 0)) hsqbs
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_HUUTRI_HSQBS) > 2
		update TBL_HUUTRI_HSQBS set bHasData = 1

	--Lay tro cap Huu tri VCQP
	select * into TBL_HUUTRI_VCQP from
	(select 1 bHangCha, 'I' LoaiTC, 1 RowNum, 'I' STT, null DoiTuong, null LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, N'TC Hưu trí' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 1 bHangCha, 'I' LoaiTC,  4 RowNum, null STT, 'VCQP' DoiTuong, 'VCQP' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'VCQP' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG, 0 bHasData
	union all
	select 0 bHangCha, 'I' LoaiTC,  4 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'VCQP' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fHUUTRI_TROCAP1LAN fTROCAP1LAN, fHUUTRI_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	from TBL_HUUTRI_DOC
	where sMaCB in ('3.1', '3.2', '3.3') and (isnull(fHUUTRI_TROCAP1LAN, 0) <> 0 or isnull(fHUUTRI_TROCAPKHUVUC, 0) <> 0)) vcqp
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_HUUTRI_VCQP) > 2
		update TBL_HUUTRI_VCQP set bHasData = 1

	--Lay tro cap Huu tri HDLD
	select * into TBL_HUUTRI_LDHD from
	(select 1 bHangCha, 'I' LoaiTC, 1 RowNum, 'I' STT, null DoiTuong, null LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, N'TC Hưu trí' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 1 bHangCha, 'I' LoaiTC,  5 RowNum, null STT, 'LDHD' DoiTuong, 'LDHD' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'LDHD' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null  fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG, 0 bHasData
	union all
	select 0 bHangCha, 'I' LoaiTC,  5 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'LDHD' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fHUUTRI_TROCAP1LAN fTROCAP1LAN, fHUUTRI_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	from TBL_HUUTRI_DOC
	where sMaCB = '43' and (isnull(fHUUTRI_TROCAP1LAN, 0) <> 0 or isnull(fHUUTRI_TROCAPKHUVUC, 0) <> 0)) ldhd
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_HUUTRI_LDHD) > 2
		update TBL_HUUTRI_LDHD set bHasData = 1
	-----------------------------------
	--Lay tro cap Phuc vien Si quan
	select * into TBL_PHUCVIEN_SQ from
	(select 1 bHangCha, 'II' LoaiTC, 1 RowNum, 'II' STT, null DoiTuong, null LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, N'TC Phục viên' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 1 bHangCha, 'II' LoaiTC, 1 RowNum, null STT, 'SQ' DoiTuong, 'SQ' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'SQ' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 0 bHangCha, 'II' LoaiTC, 1 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'SQ' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fPHUCVIEN_TROCAP1LAN fTROCAP1LAN, fPHUCVIEN_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	from TBL_PHUCVIEN_DOC
	where sMaCB like '1%' and (isnull(fPHUCVIEN_TROCAP1LAN, 0) <> 0 or isnull(fPHUCVIEN_TROCAPKHUVUC, 0) <> 0)) pvsq
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_PHUCVIEN_SQ) > 2
		update TBL_PHUCVIEN_SQ set bHasData = 1

	--Lay tro cap Phuc vien QNCN
	select * into TBL_PHUCVIEN_QNCN from
	(select 1 bHangCha, 'II' LoaiTC, 1 RowNum, 'II' STT, null DoiTuong, null LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, N'TC Phục viên' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 1 bHangCha, 'II' LoaiTC, 2 RowNum, null STT, 'QNCN' DoiTuong, 'QNCN' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'QNCN' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 0 bHangCha, 'II' LoaiTC, 2 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'QNCN' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fPHUCVIEN_TROCAP1LAN fTROCAP1LAN, fPHUCVIEN_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	from TBL_PHUCVIEN_DOC
	where sMaCB like '2%' and (isnull(fPHUCVIEN_TROCAP1LAN, 0) <> 0 or isnull(fPHUCVIEN_TROCAPKHUVUC, 0) <> 0)) pvqncn
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_PHUCVIEN_QNCN) > 2
		update TBL_PHUCVIEN_QNCN set bHasData = 1

	--Lay tro cap Phuc vien HSQ_BS
	select * into TBL_PHUCVIEN_HSQBS from
	(select 1 bHangCha, 'II' LoaiTC, 1 RowNum, 'II' STT, null DoiTuong, null LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, N'TC Phục viên' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 1 bHangCha, 'II' LoaiTC, 3 RowNum, null STT, 'HSQ_BS' DoiTuong, 'HSQ_BS' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'HSQ_BS' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 0 bHangCha, 'II' LoaiTC, 3 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'HSQ_BS' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fPHUCVIEN_TROCAP1LAN fTROCAP1LAN, fPHUCVIEN_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	from TBL_PHUCVIEN_DOC
	where sMaCB like '0%' and (isnull(fPHUCVIEN_TROCAP1LAN, 0) <> 0 or isnull(fPHUCVIEN_TROCAPKHUVUC, 0) <> 0)) pvhsqbs
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_PHUCVIEN_HSQBS) > 2
		update TBL_PHUCVIEN_HSQBS set bHasData = 1

	--Lay tro cap Phuc vien VCQP
	select * into TBL_PHUCVIEN_VCQP from
	(select 1 bHangCha, 'II' LoaiTC, 1 RowNum, 'II' STT, null DoiTuong, null LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, N'TC Phục viên' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 1 bHangCha, 'II' LoaiTC, 4 RowNum, null STT, 'VCQP' DoiTuong, 'VCQP' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'VCQP' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG, 0 bHasData
	union all
	select 0 bHangCha, 'II' LoaiTC, 4 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'VCQP' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fPHUCVIEN_TROCAP1LAN fTROCAP1LAN, fPHUCVIEN_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	from TBL_PHUCVIEN_DOC
	where sMaCB in ('3.1', '3.2', '3.3') and (isnull(fPHUCVIEN_TROCAP1LAN, 0) <> 0 or isnull(fPHUCVIEN_TROCAPKHUVUC, 0) <> 0)) pvvcqp
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_PHUCVIEN_VCQP) > 2
		update TBL_PHUCVIEN_VCQP set bHasData = 1

	--Lay tro cap Phuc vien HDLD
	select * into TBL_PHUCVIEN_LDHD from
	(select 1 bHangCha, 'II' LoaiTC, 1 RowNum, 'II' STT, null DoiTuong, null LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, N'TC Phục viên' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 1 bHangCha, 'II' LoaiTC, 5 RowNum, null STT, 'LDHD' DoiTuong, 'LDHD' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'LDHD' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null  fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG, 0 bHasData
	union all
	select 0 bHangCha, 'II' LoaiTC, 5 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'LDHD' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fPHUCVIEN_TROCAP1LAN fTROCAP1LAN, fPHUCVIEN_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	from TBL_PHUCVIEN_DOC
	where sMaCB = '43' and (isnull(fPHUCVIEN_TROCAP1LAN, 0) <> 0 or isnull(fPHUCVIEN_TROCAPKHUVUC, 0) <> 0)) pvldhd
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_PHUCVIEN_LDHD) > 2
		update TBL_PHUCVIEN_LDHD set bHasData = 1
	--------------------------------------------
	--Lay tro cap Thoi viec Si quan
	select * into TBL_THOIVIEC_SQ from
	(select 1 bHangCha, 'III' LoaiTC, 1 RowNum, 'III' STT, null DoiTuong, null LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, N'TC Thôi việc' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 1 bHangCha, 'III' LoaiTC, 1 RowNum, null STT, 'SQ' DoiTuong, 'SQ' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'SQ' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 0 bHangCha, 'III' LoaiTC, 1 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'SQ' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTHOIVIEC_TROCAP1LAN fTROCAP1LAN, fTHOIVIEC_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	from TBL_THOIVIEC_DOC
	where sMaCB like '1%' and (isnull(fTHOIVIEC_TROCAP1LAN, 0) <> 0 or isnull(fTHOIVIEC_TROCAPKHUVUC, 0) <> 0)) tvsq
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_THOIVIEC_SQ) > 2
		update TBL_THOIVIEC_SQ set bHasData = 1

	--Lay tro cap Thoi viec QNCN
	select * into TBL_THOIVIEC_QNCN from
	(select 1 bHangCha, 'III' LoaiTC, 1 RowNum, 'III' STT, null DoiTuong, null LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, N'TC Thôi việc' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 1 bHangCha, 'III' LoaiTC, 2 RowNum, null STT, 'QNCN' DoiTuong, 'QNCN' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'QNCN' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 0 bHangCha, 'III' LoaiTC, 2 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'QNCN' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTHOIVIEC_TROCAP1LAN fTROCAP1LAN, fTHOIVIEC_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	from TBL_THOIVIEC_DOC
	where sMaCB like '2%' and (isnull(fTHOIVIEC_TROCAP1LAN, 0) <> 0 or isnull(fTHOIVIEC_TROCAPKHUVUC, 0) <> 0)) tvqncn
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_THOIVIEC_QNCN) > 2
		update TBL_THOIVIEC_QNCN set bHasData = 1

	--Lay tro cap Thoi viec HSQ_BS
	select * into TBL_THOIVIEC_HSQBS from
	(select 1 bHangCha, 'III' LoaiTC, 1 RowNum, 'III' STT, null DoiTuong, null LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, N'TC Thôi việc' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 1 bHangCha, 'III' LoaiTC, 3 RowNum, null STT, 'HSQ_BS' DoiTuong, 'HSQ_BS' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'HSQ_BS' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 0 bHangCha, 'III' LoaiTC, 3 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'HSQ_BS' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTHOIVIEC_TROCAP1LAN fTROCAP1LAN, fTHOIVIEC_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	from TBL_THOIVIEC_DOC
	where sMaCB like '0%' and (isnull(fTHOIVIEC_TROCAP1LAN, 0) <> 0 or isnull(fTHOIVIEC_TROCAPKHUVUC, 0) <> 0)) tvhsqbs
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_THOIVIEC_HSQBS) > 2
		update TBL_THOIVIEC_HSQBS set bHasData = 1

	--Lay tro cap Thoi viec VCQP
	select * into TBL_THOIVIEC_VCQP from
	(select 1 bHangCha, 'III' LoaiTC, 1 RowNum, 'III' STT, null DoiTuong, null LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, N'TC Thôi việc' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 1 bHangCha, 'III' LoaiTC, 4 RowNum, null STT, 'VCQP' DoiTuong, 'VCQP' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'VCQP' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG, 0 bHasData
	union all
	select 0 bHangCha, 'III' LoaiTC, 4 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'VCQP' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTHOIVIEC_TROCAP1LAN fTROCAP1LAN, fTHOIVIEC_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	from TBL_THOIVIEC_DOC
	where sMaCB in ('3.1', '3.2', '3.3') and (isnull(fTHOIVIEC_TROCAP1LAN, 0) <> 0 or isnull(fTHOIVIEC_TROCAPKHUVUC, 0) <> 0)) tvvcqp
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_THOIVIEC_VCQP) > 2
		update TBL_THOIVIEC_VCQP set bHasData = 1

	--Lay tro cap Thoi viec HDLD
	select * into TBL_THOIVIEC_LDHD from
	(select 1 bHangCha, 'III' LoaiTC, 1 RowNum, 'III' STT, null DoiTuong, null LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, N'TC Thôi việc' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 1 bHangCha, 'III' LoaiTC, 5 RowNum, null STT, 'LDHD' DoiTuong, 'LDHD' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'LDHD' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null  fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG, 0 bHasData
	union all
	select 0 bHangCha, 'III' LoaiTC, 5 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'LDHD' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTHOIVIEC_TROCAP1LAN fTROCAP1LAN, fTHOIVIEC_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	from TBL_THOIVIEC_DOC
	where sMaCB = '43' and (isnull(fTHOIVIEC_TROCAP1LAN, 0) <> 0 or isnull(fTHOIVIEC_TROCAPKHUVUC, 0) <> 0)) tvldhd
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_THOIVIEC_LDHD) > 2
		update TBL_THOIVIEC_LDHD set bHasData = 1
	--------------------------------------------
	--Lay tro cap Tu tuat Si quan
	select * into TBL_TUTUAT_SQ from
	(select 1 bHangCha, 'IV' LoaiTC, 1 RowNum, 'IV' STT, null DoiTuong, null LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, N'TC Tử tuất' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 1 bHangCha, 'IV' LoaiTC, 1 RowNum, null STT, 'SQ' DoiTuong, 'SQ' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'SQ' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 0 bHangCha, 'IV' LoaiTC, 1 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'SQ' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTUTUAT_TROCAP1LAN fTROCAP1LAN, fTUTUAT_TROCAPKHUVUC fTROCAPKHUVUC, fTROCAPMAITANG, 0 bHasData
	from TBL_TUTUAT_DOC
	where sMaCB like '1%' and (isnull(fTUTUAT_TROCAP1LAN, 0) <> 0 or isnull(fTUTUAT_TROCAPKHUVUC, 0) <> 0)) tvsq
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TUTUAT_SQ) > 2
		update TBL_TUTUAT_SQ set bHasData = 1

	--Lay tro cap Tu tuat QNCN
	select * into TBL_TUTUAT_QNCN from
	(select 1 bHangCha, 'IV' LoaiTC, 1 RowNum, 'IV' STT, null DoiTuong, null LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, N'TC Tử tuất' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 1 bHangCha, 'IV' LoaiTC, 2 RowNum, null STT, 'QNCN' DoiTuong, 'QNCN' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'QNCN' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 0 bHangCha, 'IV' LoaiTC, 2 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'QNCN' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTUTUAT_TROCAP1LAN fTROCAP1LAN, fTUTUAT_TROCAPKHUVUC fTROCAPKHUVUC, fTROCAPMAITANG, 0 bHasData
	from TBL_TUTUAT_DOC
	where sMaCB like '2%' and (isnull(fTUTUAT_TROCAP1LAN, 0) <> 0 or isnull(fTUTUAT_TROCAPKHUVUC, 0) <> 0)) tvqncn
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TUTUAT_QNCN) > 2
		update TBL_TUTUAT_QNCN set bHasData = 1

	--Lay tro cap Tu tuat HSQ_BS
	select * into TBL_TUTUAT_HSQBS from
	(select 1 bHangCha, 'IV' LoaiTC, 1 RowNum, 'IV' STT, null DoiTuong, null LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, N'TC Tử tuất' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 1 bHangCha, 'IV' LoaiTC, 3 RowNum, null STT, 'HSQ_BS' DoiTuong, 'HSQ_BS' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'HSQ_BS' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 0 bHangCha, 'IV' LoaiTC, 3 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'HSQ_BS' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTUTUAT_TROCAP1LAN fTROCAP1LAN, fTUTUAT_TROCAPKHUVUC fTROCAPKHUVUC, fTROCAPMAITANG, 0 bHasData
	from TBL_TUTUAT_DOC
	where sMaCB like '0%' and (isnull(fTUTUAT_TROCAP1LAN, 0) <> 0 or isnull(fTUTUAT_TROCAPKHUVUC, 0) <> 0)) tvhsqbs
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TUTUAT_HSQBS) > 2
		update TBL_TUTUAT_HSQBS set bHasData = 1

	--Lay tro cap Tu tuat VCQP
	select * into TBL_TUTUAT_VCQP from
	(select 1 bHangCha, 'IV' LoaiTC, 1 RowNum, 'IV' STT, null DoiTuong, null LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, N'TC Tử tuất' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 1 bHangCha, 'IV' LoaiTC, 4 RowNum, null STT, 'VCQP' DoiTuong, 'VCQP' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'VCQP' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG, 0 bHasData
	union all
	select 0 bHangCha, 'IV' LoaiTC, 4 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'VCQP' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTUTUAT_TROCAP1LAN fTROCAP1LAN, fTUTUAT_TROCAPKHUVUC fTROCAPKHUVUC, fTROCAPMAITANG, 0 bHasData
	from TBL_TUTUAT_DOC
	where sMaCB in ('3.1', '3.2', '3.3') and (isnull(fTUTUAT_TROCAP1LAN, 0) <> 0 or isnull(fTUTUAT_TROCAPKHUVUC, 0) <> 0)) tvvcqp
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TUTUAT_VCQP) > 2
		update TBL_TUTUAT_VCQP set bHasData = 1

	--Lay tro cap Tu tuat HDLD
	select * into TBL_TUTUAT_LDHD from
	(select 1 bHangCha, 'IV' LoaiTC, 1 RowNum, 'IV' STT, null DoiTuong, null LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, N'TC Tử tuất' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData
	union all
	select 1 bHangCha, 'IV' LoaiTC, 5 RowNum, null STT, 'LDHD' DoiTuong, 'LDHD' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'LDHD' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null  fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG, 0 bHasData
	union all
	select 0 bHangCha, 'IV' LoaiTC, 5 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'LDHD' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTUTUAT_TROCAP1LAN fTROCAP1LAN, fTUTUAT_TROCAPKHUVUC fTROCAPKHUVUC, fTROCAPMAITANG, 0 bHasData
	from TBL_TUTUAT_DOC
	where sMaCB = '43' and (isnull(fTUTUAT_TROCAP1LAN, 0) <> 0 or isnull(fTUTUAT_TROCAPKHUVUC, 0) <> 0)) tvldhd
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TUTUAT_LDHD) > 2
		update TBL_TUTUAT_LDHD set bHasData = 1
	----------------------------------------------
	--Ket qua
	select result.* into TBL_HUUTRI_RESULT from
	(select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_HUUTRI_SQ
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_HUUTRI_QNCN
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_HUUTRI_HSQBS
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_HUUTRI_VCQP
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_HUUTRI_LDHD
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_PHUCVIEN_SQ
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_PHUCVIEN_QNCN
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_PHUCVIEN_HSQBS
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_PHUCVIEN_VCQP
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_PHUCVIEN_LDHD
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_THOIVIEC_SQ
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_THOIVIEC_QNCN
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_THOIVIEC_HSQBS
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_THOIVIEC_VCQP
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_THOIVIEC_LDHD
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_TUTUAT_SQ
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_TUTUAT_QNCN
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_TUTUAT_HSQBS
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_TUTUAT_VCQP
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTien
	from TBL_TUTUAT_LDHD) result

	select distinct
		LoaiTC,
		RowNum,
		STT,
		DoiTuong, 
		LoaiDoiTuong,
		sMaCB MaCb,
		sMaCBo MaCbo,
		sTenCbo TenCbo,
		sSoQuyetDinh SSoQuyetDinh,
		dNgayQuyetDinh DNgayQuyetDinh,
		TenDonVi,
		fTROCAP1LAN/@DonViTinh fTroCap1Lan,
		fTROCAPKHUVUC/@DonViTinh fTroCapKhuVuc,
		fTROCAPMAITANG/@DonViTinh fTroCapMaiTang,
		fTongSoTien/@DonViTinh fTongSoTien,
		bHangCha IsHangCha,
		bHasData IsHasData
	from TBL_HUUTRI_RESULT
	order by LoaiTC, RowNum, LoaiDoiTuong, DoiTuong desc

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI]') AND type in (N'U')) drop table TBL_HUUTRI;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI_DOC]') AND type in (N'U')) drop table TBL_HUUTRI_DOC;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_PHUCVIEN_DOC]') AND type in (N'U')) drop table TBL_PHUCVIEN_DOC;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THOIVIEC_DOC]') AND type in (N'U')) drop table TBL_THOIVIEC_DOC;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TUTUAT_DOC]') AND type in (N'U')) drop table TBL_TUTUAT_DOC;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI_SQ]') AND type in (N'U')) drop table TBL_HUUTRI_SQ;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI_QNCN]') AND type in (N'U')) drop table TBL_HUUTRI_QNCN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI_HSQBS]') AND type in (N'U')) drop table TBL_HUUTRI_HSQBS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI_VCQP]') AND type in (N'U')) drop table TBL_HUUTRI_VCQP;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI_LDHD]') AND type in (N'U')) drop table TBL_HUUTRI_LDHD;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_PHUCVIEN_SQ]') AND type in (N'U')) drop table TBL_PHUCVIEN_SQ;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_PHUCVIEN_QNCN]') AND type in (N'U')) drop table TBL_PHUCVIEN_QNCN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_PHUCVIEN_HSQBS]') AND type in (N'U')) drop table TBL_PHUCVIEN_HSQBS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_PHUCVIEN_VCQP]') AND type in (N'U')) drop table TBL_PHUCVIEN_VCQP;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_PHUCVIEN_LDHD]') AND type in (N'U')) drop table TBL_PHUCVIEN_LDHD;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THOIVIEC_SQ]') AND type in (N'U')) drop table TBL_THOIVIEC_SQ;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THOIVIEC_QNCN]') AND type in (N'U')) drop table TBL_THOIVIEC_QNCN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THOIVIEC_HSQBS]') AND type in (N'U')) drop table TBL_THOIVIEC_HSQBS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THOIVIEC_VCQP]') AND type in (N'U')) drop table TBL_THOIVIEC_VCQP;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THOIVIEC_LDHD]') AND type in (N'U')) drop table TBL_THOIVIEC_LDHD;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TUTUAT_SQ]') AND type in (N'U')) drop table TBL_TUTUAT_SQ;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TUTUAT_QNCN]') AND type in (N'U')) drop table TBL_TUTUAT_QNCN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TUTUAT_HSQBS]') AND type in (N'U')) drop table TBL_TUTUAT_HSQBS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TUTUAT_VCQP]') AND type in (N'U')) drop table TBL_TUTUAT_VCQP;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TUTUAT_LDHD]') AND type in (N'U')) drop table TBL_TUTUAT_LDHD;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI_RESULT]') AND type in (N'U')) drop table TBL_HUUTRI_RESULT;

END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_om_dau_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_om_dau_nq104]
	@DsMaDonVi nvarchar(100),
	@NamLamViec int,
	@Thang int,
	@DonViTinh int
AS
BEGIN
	--Lay thong tin luong theo tro cap om dau
	select * into TBL_TCOD from
	(select luongcancu.fLuongCanCu, chedo.fSoNgayHuongBHXH, donvi.Ten_DonVi, luong.*
	from TL_BangLuong_Thang_NQ104BHXH luong
	join TL_DM_DonVi_NQ104 donvi on luong.sMaDonVi = donvi.Ma_DonVi
	join TL_CanBo_CheDoBHXH chedo on luong.sMaCBo = chedo.sMaCanBo and luong.sMaCheDo = chedo.sMaCheDo
	left join 
	(select Ma_CBo, NAM, thang, Gia_Tri fLuongCanCu from TL_BangLuong_Thang_NQ104
		where NAM = @NamLamViec
		and thang = @Thang
		and Ma_PhuCap = 'LHT_TT') luongcancu on luong.sMaCBo = luongcancu.Ma_CBo
	where 
	luong.sMaDonVi in (SELECT * FROM f_split(@DsMaDonVi))
	and luong.iNam = @NamLamViec
	and luong.iThang = @Thang
	and luong.sMaCheDo in ('BENHDAINGAY_D14NGAY', 'BENHDAINGAY_T14NGAY', 'OMKHAC_D14NGAY', 'OMKHAC_T14NGAY', 'CONOM', 'OMDAU_DUONGSUCPHSK')) tcod

	select distinct 
		TBL_TCOD.sMaCB,
		TBL_TCOD.sMaCBo,
		--TBL_TCOD.sMaCheDo,
		TBL_TCOD.sTenCbo,
		TBL_TCOD.Ten_DonVi,
		BENHDAINGAYD14NGAY.SoNgayBENHDAINGAYD14NGAY SoNgayBenhDaiNgayD14Ngay,
		BENHDAINGAY_T14NGAY.SoNgayBenhDaiNgayT14Ngay,
		OMKHAC_D14NGAY.SoNgayOmKhacD14Ngay,
		OMKHAC_T14NGAY.SoNgayOmKhacT14Ngay,
		CONOM.SoNgayConOm,
		DUONGSUCPHSK.SoNgayDuongSuc,
		TBL_TCOD.fLuongCanCu,
		BENHDAINGAYD14NGAY.nGiaTri fBENHDAINGAY_D14NGAY,
		BENHDAINGAY_T14NGAY.nGiaTri fBENHDAINGAY_T14NGAY,
		OMKHAC_D14NGAY.nGiaTri fOMKHAC_D14NGAY,
		OMKHAC_T14NGAY.nGiaTri fOMKHAC_T14NGAY,
		CONOM.nGiaTri fCONOM,
		DUONGSUCPHSK.nGiaTri fDUONGSUCPHSK
		into TBL_TCOD_DOC
	from TBL_TCOD TBL_TCOD
		left join
		(select tcod.Id, tcod.sMaDonVi, tcod.nGiaTri, tcod.sMaCB, tcod.sMaCBo, tcod.sMaCheDo, tcod.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayBenhDaiNgayT14Ngay
		from TBL_TCOD tcod left join TL_CanBo_CheDoBHXH chedo on tcod.sMaCBo = chedo.sMaCanBo and tcod.sMaCheDo = chedo.sMaCheDo
		where tcod.sMaCheDo = 'BENHDAINGAY_T14NGAY') BENHDAINGAY_T14NGAY
		on TBL_TCOD.sMaCBo = BENHDAINGAY_T14NGAY.sMaCBo and TBL_TCOD.sMaDonVi = BENHDAINGAY_T14NGAY.sMaDonVi
		left join
		(select tcod_1.Id, tcod_1.sMaDonVi, tcod_1.nGiaTri, tcod_1.sMaCB, tcod_1.sMaCBo, tcod_1.sMaCheDo, tcod_1.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayOmKhacD14Ngay
		from TBL_TCOD tcod_1 left join TL_CanBo_CheDoBHXH chedo on tcod_1.sMaCBo = chedo.sMaCanBo and tcod_1.sMaCheDo = chedo.sMaCheDo
		where tcod_1.sMaCheDo = 'OMKHAC_D14NGAY') OMKHAC_D14NGAY
		on TBL_TCOD.sMaCBo = OMKHAC_D14NGAY.sMaCBo and TBL_TCOD.sMaDonVi = OMKHAC_D14NGAY.sMaDonVi
		left join
		(select tcod_2.Id, tcod_2.sMaDonVi, tcod_2.nGiaTri, tcod_2.sMaCB, tcod_2.sMaCBo, tcod_2.sMaCheDo, tcod_2.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayOmKhacT14Ngay
		from TBL_TCOD tcod_2 left join TL_CanBo_CheDoBHXH chedo on tcod_2.sMaCBo = chedo.sMaCanBo and tcod_2.sMaCheDo = chedo.sMaCheDo
		where tcod_2.sMaCheDo = 'OMKHAC_T14NGAY') OMKHAC_T14NGAY
		on TBL_TCOD.sMaCBo = OMKHAC_T14NGAY.sMaCBo and TBL_TCOD.sMaDonVi = OMKHAC_T14NGAY.sMaDonVi
		left join
		(select conom.Id, conom.sMaDonVi, conom.nGiaTri, conom.sMaCB, conom.sMaCBo, conom.sMaCheDo, conom.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayConOm
		from TBL_TCOD conom left join TL_CanBo_CheDoBHXH chedo on conom.sMaCBo = chedo.sMaCanBo and conom.sTenCbo = chedo.sMaCheDo
		where conom.sMaCheDo = 'CONOM') CONOM
		on TBL_TCOD.sMaCBo = CONOM.sMaCBo and TBL_TCOD.sMaDonVi = CONOM.sMaDonVi
		left join
		(select duongsuc.Id, duongsuc.sMaDonVi, duongsuc.nGiaTri, duongsuc.sMaCB, duongsuc.sMaCBo, duongsuc.sMaCheDo, duongsuc.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayDuongSuc
		from TBL_TCOD duongsuc left join TL_CanBo_CheDoBHXH chedo on duongsuc.sMaCBo = chedo.sMaCanBo and duongsuc.sMaCheDo = chedo.sMaCheDo
		where duongsuc.sMaCheDo = 'OMDAU_DUONGSUCPHSK') DUONGSUCPHSK
		on TBL_TCOD.sMaCBo = DUONGSUCPHSK.sMaCBo and TBL_TCOD.sMaDonVi = DUONGSUCPHSK.sMaDonVi
		left join
		(select tcod_3.Id, tcod_3.sMaDonVi, tcod_3.nGiaTri, tcod_3.sMaCB, tcod_3.sMaCBo, tcod_3.sMaCheDo, tcod_3.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayBENHDAINGAYD14NGAY
		from TBL_TCOD tcod_3 left join TL_CanBo_CheDoBHXH chedo on tcod_3.sMaCBo = chedo.sMaCanBo and tcod_3.sMaCheDo = chedo.sMaCheDo
		where tcod_3.sMaCheDo = 'BENHDAINGAY_D14NGAY') BENHDAINGAYD14NGAY
		on TBL_TCOD.sMaCBo = BENHDAINGAYD14NGAY.sMaCBo and TBL_TCOD.sMaDonVi = BENHDAINGAYD14NGAY.sMaDonVi

	--Lấy lương Sĩ quan
	select * into TBL_TCOD_SQ from
	(select
		1 bHangCha, 1 RowNum, 'I' STT, 'SQ' DoiTuong, 'SQ' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'SQ' sTenCbo, null SoNgayBenhDaiNgayD14Ngay, null SoNgayBenhDaiNgayT14Ngay, null SoNgayOmKhacD14Ngay, null SoNgayOmKhacT14Ngay, null SoNgayConOm, null SoNgayDuongSuc, null fLuongCanCu, null fBENHDAINGAY_D14NGAY, null fBENHDAINGAY_T14NGAY, null fOMKHAC_D14NGAY, null fOMKHAC_T14NGAY, null fCONOM, null fDUONGSUCPHSK, 0 bHasData
	union all
	select
		0 bHangCha, 1 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'SQ' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK, 0 bHasData
	from TBL_TCOD_DOC
	where sMaCB like '1%' and (isnull(fLuongCanCu, 0) <> 0 or isnull(fBENHDAINGAY_D14NGAY, 0) <> 0 or isnull(fBENHDAINGAY_T14NGAY, 0) <> 0 or isnull(fOMKHAC_D14NGAY, 0) <> 0 or isnull(fOMKHAC_T14NGAY, 0) <> 0 or isnull(fCONOM, 0) <> 0  or isnull(fDUONGSUCPHSK, 0) <> 0)) sq
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCOD_SQ) > 1
		update TBL_TCOD_SQ set bHasData = 1

	--Lấy lương QNCN
	select * into TBL_TCOD_QNCN from
	(select
		1 bHangCha, 2 RowNum, 'II' STT, 'QNCN' DoiTuong, 'QNCN' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'QNCN' sTenCbo, null SoNgayBenhDaiNgayD14Ngay, null SoNgayBenhDaiNgayT14Ngay, null SoNgayOmKhacD14Ngay, null SoNgayOmKhacT14Ngay, null SoNgayConOm, null SoNgayDuongSuc, null fLuongCanCu, null fBENHDAINGAY_D14NGAY, null fBENHDAINGAY_T14NGAY, null fOMKHAC_D14NGAY, null fOMKHAC_T14NGAY, null fCONOM, null fDUONGSUCPHSK, 0 bHasData
	union all
	select
		0 bHangCha, 2 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'QNCN' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK, 0 bHasData
	from TBL_TCOD_DOC
	where sMaCB like '2%' and (isnull(fLuongCanCu, 0) <> 0 or isnull(fBENHDAINGAY_D14NGAY, 0) <> 0 or isnull(fBENHDAINGAY_T14NGAY, 0) <> 0 or isnull(fOMKHAC_D14NGAY, 0) <> 0 or isnull(fOMKHAC_T14NGAY, 0) <> 0 or isnull(fCONOM, 0) <> 0  or isnull(fDUONGSUCPHSK, 0) <> 0)) qncn
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCOD_QNCN) > 1
		update TBL_TCOD_QNCN set bHasData = 1

	--Lấy lương HSQ_BS
	select * into TBL_TCOD_HSQBS from
	(select
		1 bHangCha, 3 RowNum, 'III' STT, 'HSQ_BS' DoiTuong, 'HSQ_BS' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'HSQ_BS' sTenCbo, null SoNgayBenhDaiNgayD14Ngay, null SoNgayBenhDaiNgayT14Ngay, null SoNgayOmKhacD14Ngay, null SoNgayOmKhacT14Ngay, null SoNgayConOm, null SoNgayDuongSuc, null fLuongCanCu, null fBENHDAINGAY_D14NGAY, null fBENHDAINGAY_T14NGAY, null fOMKHAC_D14NGAY, null fOMKHAC_T14NGAY, null fCONOM, null fDUONGSUCPHSK, 0 bHasData
	union all
	select
		0 bHangCha, 3 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'HSQ_BS' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK, 0 bHasData
	from TBL_TCOD_DOC
	where sMaCB like '0%' and (isnull(fLuongCanCu, 0) <> 0 or isnull(fBENHDAINGAY_D14NGAY, 0) <> 0 or isnull(fBENHDAINGAY_T14NGAY, 0) <> 0 or isnull(fOMKHAC_D14NGAY, 0) <> 0 or isnull(fOMKHAC_T14NGAY, 0) <> 0 or isnull(fCONOM, 0) <> 0  or isnull(fDUONGSUCPHSK, 0) <> 0)) hsqbs
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCOD_HSQBS) > 1
		update TBL_TCOD_HSQBS set bHasData = 1

	--Lấy lương VCQP
	select * into TBL_TCOD_VCQP from
	(select
		1 bHangCha, 4 RowNum, 'IV' STT, 'VCQP' DoiTuong, 'VCQP' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'VCQP' sTenCbo, null SoNgayBenhDaiNgayD14Ngay, null SoNgayBenhDaiNgayT14Ngay, null SoNgayOmKhacD14Ngay, null SoNgayOmKhacT14Ngay, null SoNgayConOm, null SoNgayDuongSuc, null fLuongCanCu, null fBENHDAINGAY_D14NGAY, null fBENHDAINGAY_T14NGAY, null fOMKHAC_D14NGAY, null fOMKHAC_T14NGAY, null fCONOM, null fDUONGSUCPHSK, 0 bHasData
	union all
	select
		0 bHangCha, 4 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'VCQP' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK, 0 bHasData
	from TBL_TCOD_DOC
	where sMaCB in ('3.1', '3.2', '3.3') and (isnull(fLuongCanCu, 0) <> 0 or isnull(fBENHDAINGAY_D14NGAY, 0) <> 0 or isnull(fBENHDAINGAY_T14NGAY, 0) <> 0 or isnull(fOMKHAC_D14NGAY, 0) <> 0 or isnull(fOMKHAC_T14NGAY, 0) <> 0 or isnull(fCONOM, 0) <> 0  or isnull(fDUONGSUCPHSK, 0) <> 0)) vcqp
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCOD_VCQP) > 1
		update TBL_TCOD_VCQP set bHasData = 1

	--Lấy lương Lao động hợp đông
	select * into TBL_TCOD_LDHD from
	(select
		1 bHangCha, 5 RowNum, 'V' STT, 'LDHD' DoiTuong, 'LDHD' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'LDHD' sTenCbo, null SoNgayBenhDaiNgayD14Ngay, null SoNgayBenhDaiNgayT14Ngay, null SoNgayOmKhacD14Ngay, null SoNgayOmKhacT14Ngay, null SoNgayConOm, null SoNgayDuongSuc, null fLuongCanCu, null fBENHDAINGAY_D14NGAY, null fBENHDAINGAY_T14NGAY, null fOMKHAC_D14NGAY, null fOMKHAC_T14NGAY, null fCONOM, null fDUONGSUCPHSK, 0 bHasData
	union all
	select
		0 bHangCha, 5 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'LDHD' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK, 0 bHasData
	from TBL_TCOD_DOC
	where sMaCB = '43' and (isnull(fLuongCanCu, 0) <> 0 or isnull(fBENHDAINGAY_D14NGAY, 0) <> 0 or isnull(fBENHDAINGAY_T14NGAY, 0) <> 0 or isnull(fOMKHAC_D14NGAY, 0) <> 0 or isnull(fOMKHAC_T14NGAY, 0) <> 0 or isnull(fCONOM, 0) <> 0  or isnull(fDUONGSUCPHSK, 0) <> 0)) ldhd
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCOD_LDHD) > 1
		update TBL_TCOD_LDHD set bHasData = 1

	--Ket qua
	select result.* into TBL_TCOD_RESULT from
	(select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK,
	(isnull(fBENHDAINGAY_D14NGAY, 0) + isnull(fBENHDAINGAY_T14NGAY, 0) + isnull(fOMKHAC_D14NGAY, 0) + isnull(fOMKHAC_T14NGAY, 0) + isnull(fCONOM, 0) + isnull(fDUONGSUCPHSK, 0)) fTongSoTien
	from TBL_TCOD_SQ
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK,
	(isnull(fBENHDAINGAY_D14NGAY, 0) + isnull(fBENHDAINGAY_T14NGAY, 0) + isnull(fOMKHAC_D14NGAY, 0) + isnull(fOMKHAC_T14NGAY, 0) + isnull(fCONOM, 0) + isnull(fDUONGSUCPHSK, 0)) fTongSoTien
	from TBL_TCOD_QNCN
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK,
	(isnull(fBENHDAINGAY_D14NGAY, 0) + isnull(fBENHDAINGAY_T14NGAY, 0) + isnull(fOMKHAC_D14NGAY, 0) + isnull(fOMKHAC_T14NGAY, 0) + isnull(fCONOM, 0) + isnull(fDUONGSUCPHSK, 0)) fTongSoTien
	from TBL_TCOD_HSQBS
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK,
	(isnull(fBENHDAINGAY_D14NGAY, 0) + isnull(fBENHDAINGAY_T14NGAY, 0) + isnull(fOMKHAC_D14NGAY, 0) + isnull(fOMKHAC_T14NGAY, 0) + isnull(fCONOM, 0) + isnull(fDUONGSUCPHSK, 0)) fTongSoTien
	from TBL_TCOD_VCQP
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK,
	(isnull(fBENHDAINGAY_D14NGAY, 0) + isnull(fBENHDAINGAY_T14NGAY, 0) + isnull(fOMKHAC_D14NGAY, 0) + isnull(fOMKHAC_T14NGAY, 0) + isnull(fCONOM, 0) + isnull(fDUONGSUCPHSK, 0)) fTongSoTien
	from TBL_TCOD_LDHD) result

	select
		RowNum,
		STT,
		DoiTuong, 
		LoaiDoiTuong,
		sMaCB MaCb, 
		sMaCBo MaCbo,
		sTenCbo TenCbo,
		TenDonVi,
		SoNgayBenhDaiNgayD14Ngay, 
		SoNgayBenhDaiNgayT14Ngay, 
		SoNgayOmKhacD14Ngay, 
		SoNgayOmKhacT14Ngay, 
		SoNgayConOm, 
		SoNgayDuongSuc, 
		fLuongCanCu FLuongCanCu, 
		fBENHDAINGAY_D14NGAY/@DonViTinh FBenhDaiNgayD14Ngay, 
		fBENHDAINGAY_T14NGAY/@DonViTinh FBenhDauNgayT14Ngay, 
		fOMKHAC_D14NGAY/@DonViTinh FOmKhacD14Ngay, 
		fOMKHAC_T14NGAY/@DonViTinh FOmKhacT14Ngay, 
		fCONOM/@DonViTinh FConOm, 
		fDUONGSUCPHSK/@DonViTinh FDuongSucPHSK,
		fTongSoTien/@DonViTinh FTongSoTien,
		bHangCha IsHangCha,
		bHasData IsHasData
	from TBL_TCOD_RESULT
	order by RowNum, LoaiDoiTuong, DoiTuong desc

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCOD]') AND type in (N'U'))
	drop table TBL_TCOD;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCOD_DOC]') AND type in (N'U'))
	drop table TBL_TCOD_DOC;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCOD_SQ]') AND type in (N'U'))
	drop table TBL_TCOD_SQ;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCOD_QNCN]') AND type in (N'U'))
	drop table TBL_TCOD_QNCN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCOD_HSQBS]') AND type in (N'U'))
	drop table TBL_TCOD_HSQBS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCOD_VCQP]') AND type in (N'U'))
	drop table TBL_TCOD_VCQP;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCOD_LDHD]') AND type in (N'U'))
	drop table TBL_TCOD_LDHD;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCOD_RESULT]') AND type in (N'U'))
	drop table TBL_TCOD_RESULT;

END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_thai_san_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_thai_san_nq104]
	@DsMaDonVi nvarchar(100),
	@NamLamViec int,
	@Thang int,
	@DonViTinh int
AS
BEGIN
	--Lay thong tin luong theo tro cap thai san
	select * into TBL_TCTS from
	(select luongcancu.fLuongCanCu, chedo.fSoNgayHuongBHXH, donvi.Ten_DonVi, luong.*
	from TL_BangLuong_Thang_NQ104BHXH luong
	join TL_DM_DonVi_NQ104 donvi on luong.sMaDonVi = donvi.Ma_DonVi
	join TL_CanBo_CheDoBHXH chedo on luong.sMaCBo = chedo.sMaCanBo and luong.sMaCheDo = chedo.sMaCheDo
	left join 
	(select Ma_CBo, NAM, thang, Gia_Tri fLuongCanCu from TL_BangLuong_Thang_NQ104
		where NAM = @NamLamViec
		and thang = @Thang
		and Ma_PhuCap = 'LHT_TT') luongcancu on luong.sMaCBo = luongcancu.Ma_CBo
	where 
	luong.sMaDonVi in (SELECT * FROM f_split(@DsMaDonVi))
	and luong.iNam = @NamLamViec
	and luong.iThang = @Thang
	and luong.sMaCheDo in ('SINHCON_NUOICON', 'THAISAN_TROCAP1LAN', 'KHAMTHAI', N'KHHGD', 'NAMNGHIKHIVOSINHCON', 'THAISAN_DUONGSUCPHSK')) tcts

	select
		TBL_TCTS.sMaCB,
		TBL_TCTS.sMaCBo,
		--TBL_TCTS.sMaCheDo,
		TBL_TCTS.sTenCbo,
		TBL_TCTS.Ten_DonVi,
		SINHCONNUOICON.SoNgaySINHCONNUOICON SoNgaySINHCON_NUOICON,
		TROCAP1LAN.SoNgayTROCAP1LAN,
		KHAMTHAI.SoNgayKHAMTHAI,
		DUONGSUCPHSK.SoNgayDUONGSUCPHSK,
		TBL_TCTS.fLuongCanCu,
		SINHCONNUOICON.nGiaTri fSINHCON_NUOICON,
		TROCAP1LAN.nGiaTri fTROCAP1LAN,
		KHAMTHAI.nGiaTri fKHAMTHAI,
		DUONGSUCPHSK.nGiaTri fDUONGSUCPHSK
		into TBL_TCTS_DOC
	from TBL_TCTS TBL_TCTS
		left join
		(select tcts.Id, tcts.sMaDonVi, tcts.nGiaTri, tcts.sMaCB, tcts.sMaCBo, tcts.sMaCheDo, tcts.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTROCAP1LAN
		from TBL_TCTS tcts left join TL_CanBo_CheDoBHXH chedo on tcts.sMaCBo = chedo.sMaCanBo and tcts.sMaCheDo = chedo.sMaCheDo
		where tcts.sMaCheDo = 'THAISAN_TROCAP1LAN') TROCAP1LAN
		on TBL_TCTS.sMaCBo = TROCAP1LAN.sMaCBo and TBL_TCTS.sMaDonVi = TROCAP1LAN.sMaDonVi
		left join
		(select tcts_1.Id, tcts_1.sMaDonVi, sum(tcts_1.nGiaTri) nGiaTri, tcts_1.sMaCB, tcts_1.sMaCBo, tcts_1.sMaCheDo, tcts_1.sTenCbo, sum(chedo.fSoNgayHuongBHXH) SoNgayKHAMTHAI
		from TBL_TCTS tcts_1 left join TL_CanBo_CheDoBHXH chedo on tcts_1.sMaCBo = chedo.sMaCanBo and tcts_1.sMaCheDo = chedo.sMaCheDo
		where tcts_1.sMaCheDo in ('KHAMTHAI', N'KHHGD', 'NAMNGHIKHIVOSINHCON')
		group by tcts_1.Id, tcts_1.sMaDonVi, tcts_1.sMaCB, tcts_1.sMaCBo, tcts_1.sMaCheDo, tcts_1.sTenCbo) KHAMTHAI
		on TBL_TCTS.sMaCBo = KHAMTHAI.sMaCBo and TBL_TCTS.sMaDonVi = KHAMTHAI.sMaDonVi
		left join
		(select tcts_2.Id, tcts_2.sMaDonVi, tcts_2.nGiaTri, tcts_2.sMaCB, tcts_2.sMaCBo, tcts_2.sMaCheDo, tcts_2.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayDUONGSUCPHSK
		from TBL_TCTS tcts_2 left join TL_CanBo_CheDoBHXH chedo on tcts_2.sMaCBo = chedo.sMaCanBo and tcts_2.sMaCheDo = chedo.sMaCheDo
		where tcts_2.sMaCheDo = 'THAISAN_DUONGSUCPHSK') DUONGSUCPHSK
		on TBL_TCTS.sMaCBo = DUONGSUCPHSK.sMaCBo and TBL_TCTS.sMaDonVi = DUONGSUCPHSK.sMaDonVi
		left join
		(select tcts_3.Id, tcts_3.sMaDonVi, tcts_3.nGiaTri, tcts_3.sMaCB, tcts_3.sMaCBo, tcts_3.sMaCheDo, tcts_3.sTenCbo, chedo.fSoNgayHuongBHXH SoNgaySINHCONNUOICON
		from TBL_TCTS tcts_3 left join TL_CanBo_CheDoBHXH chedo on tcts_3.sMaCBo = chedo.sMaCanBo and tcts_3.sMaCheDo = chedo.sMaCheDo
		where tcts_3.sMaCheDo = 'SINHCON_NUOICON') SINHCONNUOICON
		on TBL_TCTS.sMaCBo = DUONGSUCPHSK.sMaCBo and TBL_TCTS.sMaDonVi = SINHCONNUOICON.sMaDonVi

	--Lấy lương Sĩ quan
	select * into TBL_TCTS_SQ from
	(select
		 1 bHangCha, 1 RowNum, 'I' STT, 'SQ' DoiTuong, 'SQ' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'SQ' sTenCbo, null SoNgaySINHCON_NUOICON, null SoNgayTROCAP1LAN, null SoNgayKHAMTHAI, null SoNgayDUONGSUCPHSK, null fLuongCanCu, null fSINHCON_NUOICON, null fTROCAP1LAN, null fKHAMTHAI, null fDUONGSUCPHSK, 0 bHasData
	union all
	select
		0 bHangCha, 1 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'SQ' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgaySINHCON_NUOICON, SoNgayTROCAP1LAN, SoNgayKHAMTHAI, SoNgayDUONGSUCPHSK, fLuongCanCu, fSINHCON_NUOICON, fTROCAP1LAN, fKHAMTHAI, fDUONGSUCPHSK, 0 bHasData
	from TBL_TCTS_DOC
	where sMaCB like '1%' and (isnull(fLuongCanCu, 0) <> 0 or isnull(fSINHCON_NUOICON, 0) <> 0 or isnull(fTROCAP1LAN, 0) <> 0 or isnull(fKHAMTHAI, 0) <> 0 or isnull(fDUONGSUCPHSK, 0) <> 0)) sq
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCTS_SQ) > 1
		update TBL_TCTS_SQ set bHasData = 1

	--Lấy lương QNCN
	select * into TBL_TCTS_QNCN from
	(select
		1 bHangCha, 2 RowNum, 'II' STT, 'QNCN' DoiTuong, 'QNCN' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'QNCN' sTenCbo, null SoNgaySINHCON_NUOICON, null SoNgayTROCAP1LAN, null SoNgayKHAMTHAI, null SoNgayDUONGSUCPHSK, null fLuongCanCu, null fSINHCON_NUOICON, null fTROCAP1LAN, null fKHAMTHAI, null fDUONGSUCPHSK, 0 bHasData
	union all
	select
		0 bHangCha, 2 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'QNCN' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgaySINHCON_NUOICON, SoNgayTROCAP1LAN, SoNgayKHAMTHAI, SoNgayDUONGSUCPHSK, fLuongCanCu, fSINHCON_NUOICON, fTROCAP1LAN, fKHAMTHAI, fDUONGSUCPHSK, 0 bHasData
	from TBL_TCTS_DOC
	where sMaCB like '2%' and (isnull(fLuongCanCu, 0) <> 0 or isnull(fSINHCON_NUOICON, 0) <> 0 or isnull(fTROCAP1LAN, 0) <> 0 or isnull(fKHAMTHAI, 0) <> 0 or isnull(fDUONGSUCPHSK, 0) <> 0)) qncn
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCTS_QNCN) > 1
		update TBL_TCTS_QNCN set bHasData = 1

	--Lấy lương HSQ_BS
	select * into TBL_TCTS_HSQBS from
	(select
		1 bHangCha, 3 RowNum, 'III' STT, 'HSQ_BS' DoiTuong, 'HSQ_BS' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'HSQ_BS' sTenCbo, null SoNgaySINHCON_NUOICON, null SoNgayTROCAP1LAN, null SoNgayKHAMTHAI, null SoNgayDUONGSUCPHSK, null fLuongCanCu, null fSINHCON_NUOICON, null fTROCAP1LAN, null fKHAMTHAI, null fDUONGSUCPHSK, 0 bHasData
	union all
	select
		0 bHangCha, 3 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'HSQ_BS' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgaySINHCON_NUOICON, SoNgayTROCAP1LAN, SoNgayKHAMTHAI, SoNgayDUONGSUCPHSK, fLuongCanCu, fSINHCON_NUOICON, fTROCAP1LAN, fKHAMTHAI, fDUONGSUCPHSK, 0 bHasData
	from TBL_TCTS_DOC
	where sMaCB like '0%' and (isnull(fLuongCanCu, 0) <> 0 or isnull(fSINHCON_NUOICON, 0) <> 0 or isnull(fTROCAP1LAN, 0) <> 0 or isnull(fKHAMTHAI, 0) <> 0 or isnull(fDUONGSUCPHSK, 0) <> 0)) hsqbs
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCTS_HSQBS) > 1
		update TBL_TCTS_HSQBS set bHasData = 1

	--Lấy lương VCQP
	select * into TBL_TCTS_VCQP from
	(select
		1 bHangCha, 4 RowNum, 'IV' STT, 'VCQP' DoiTuong, 'VCQP' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'VCQP' sTenCbo, null SoNgaySINHCON_NUOICON, null SoNgayTROCAP1LAN, null SoNgayKHAMTHAI, null SoNgayDUONGSUCPHSK, null fLuongCanCu, null fSINHCON_NUOICON, null fTROCAP1LAN, null fKHAMTHAI, null fDUONGSUCPHSK, 0 bHasData
	union all
	select
		0 bHangCha, 4 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'VCQP' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgaySINHCON_NUOICON, SoNgayTROCAP1LAN, SoNgayKHAMTHAI, SoNgayDUONGSUCPHSK, fLuongCanCu, fSINHCON_NUOICON, fTROCAP1LAN, fKHAMTHAI, fDUONGSUCPHSK, 0 bHasData
	from TBL_TCTS_DOC
	where sMaCB in ('3.1', '3.2', '3.3') and (isnull(fLuongCanCu, 0) <> 0 or isnull(fSINHCON_NUOICON, 0) <> 0 or isnull(fTROCAP1LAN, 0) <> 0 or isnull(fKHAMTHAI, 0) <> 0 or isnull(fDUONGSUCPHSK, 0) <> 0)) vcqp
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCTS_VCQP) > 1
		update TBL_TCTS_VCQP set bHasData = 1

	--Lấy lương Lao động hợp đông
	select * into TBL_TCTS_LDHD from
	(select
		1 bHangCha, 5 RowNum, 'V' STT, 'LDHD' DoiTuong, 'LDHD' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, 'LDHD' sTenCbo, null SoNgaySINHCON_NUOICON, null SoNgayTROCAP1LAN, null SoNgayKHAMTHAI, null SoNgayDUONGSUCPHSK, null fLuongCanCu, null fSINHCON_NUOICON, null fTROCAP1LAN, null fKHAMTHAI, null fDUONGSUCPHSK, 0 bHasData
	union all
	select
		0 bHangCha, 5 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'LDHD' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgaySINHCON_NUOICON, SoNgayTROCAP1LAN, SoNgayKHAMTHAI, SoNgayDUONGSUCPHSK, fLuongCanCu, fSINHCON_NUOICON, fTROCAP1LAN, fKHAMTHAI, fDUONGSUCPHSK, 0 bHasData
	from TBL_TCTS_DOC
	where sMaCB = '43' and (isnull(fLuongCanCu, 0) <> 0 or isnull(fSINHCON_NUOICON, 0) <> 0 or isnull(fTROCAP1LAN, 0) <> 0 or isnull(fKHAMTHAI, 0) <> 0 or isnull(fDUONGSUCPHSK, 0) <> 0)) ldhd
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCTS_LDHD) > 1
		update TBL_TCTS_LDHD set bHasData = 1

	--Ket qua
	select result.* into TBL_TCTS_RESULT from
	(select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgaySINHCON_NUOICON, SoNgayTROCAP1LAN, SoNgayKHAMTHAI, SoNgayDUONGSUCPHSK, fLuongCanCu, fSINHCON_NUOICON, fTROCAP1LAN, fKHAMTHAI, fDUONGSUCPHSK,
	(isnull(fSINHCON_NUOICON, 0) + isnull(fTROCAP1LAN, 0) + isnull(fKHAMTHAI, 0) + isnull(fDUONGSUCPHSK, 0)) fTongSoTien
	from TBL_TCTS_SQ
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgaySINHCON_NUOICON, SoNgayTROCAP1LAN, SoNgayKHAMTHAI, SoNgayDUONGSUCPHSK, fLuongCanCu, fSINHCON_NUOICON, fTROCAP1LAN, fKHAMTHAI, fDUONGSUCPHSK,
	(isnull(fSINHCON_NUOICON, 0) + isnull(fTROCAP1LAN, 0) + isnull(fKHAMTHAI, 0) + isnull(fDUONGSUCPHSK, 0)) fTongSoTien
	from TBL_TCTS_QNCN
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgaySINHCON_NUOICON, SoNgayTROCAP1LAN, SoNgayKHAMTHAI, SoNgayDUONGSUCPHSK, fLuongCanCu, fSINHCON_NUOICON, fTROCAP1LAN, fKHAMTHAI, fDUONGSUCPHSK,
	(isnull(fSINHCON_NUOICON, 0) + isnull(fTROCAP1LAN, 0) + isnull(fKHAMTHAI, 0) + isnull(fDUONGSUCPHSK, 0)) fTongSoTien
	from TBL_TCTS_HSQBS
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgaySINHCON_NUOICON, SoNgayTROCAP1LAN, SoNgayKHAMTHAI, SoNgayDUONGSUCPHSK, fLuongCanCu, fSINHCON_NUOICON, fTROCAP1LAN, fKHAMTHAI, fDUONGSUCPHSK,
	(isnull(fSINHCON_NUOICON, 0) + isnull(fTROCAP1LAN, 0) + isnull(fKHAMTHAI, 0) + isnull(fDUONGSUCPHSK, 0)) fTongSoTien
	from TBL_TCTS_VCQP
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgaySINHCON_NUOICON, SoNgayTROCAP1LAN, SoNgayKHAMTHAI, SoNgayDUONGSUCPHSK, fLuongCanCu, fSINHCON_NUOICON, fTROCAP1LAN, fKHAMTHAI, fDUONGSUCPHSK,
	(isnull(fSINHCON_NUOICON, 0) + isnull(fTROCAP1LAN, 0) + isnull(fKHAMTHAI, 0) + isnull(fDUONGSUCPHSK, 0)) fTongSoTien
	from TBL_TCTS_LDHD) result

	select
		RowNum,
		STT,
		DoiTuong, 
		LoaiDoiTuong,
		sMaCB MaCb, 
		sMaCBo MaCbo, 
		sTenCbo TenCbo,
		TenDonVi,
		SoNgaySINHCON_NUOICON SoNgaySinhConNuoiCon,
		SoNgayTROCAP1LAN SoNgayTroCap1Lan,
		SoNgayKHAMTHAI SoNgayKhamThai,
		SoNgayDUONGSUCPHSK SoNgayDuongSucPHSKThaiSan,
		fLuongCanCu FLuongCanCu,
		fSINHCON_NUOICON/@DonViTinh fSinhConNuoiCon,
		fTROCAP1LAN/@DonViTinh fTroCap1Lan,
		fKHAMTHAI/@DonViTinh fKhamThai,
		fDUONGSUCPHSK/@DonViTinh fDuongSucPHSKThaiSan,
		fTongSoTien/@DonViTinh FTongSoTien,
		bHangCha IsHangCha,
		bHasData IsHasData
	from TBL_TCTS_RESULT
	order by RowNum, LoaiDoiTuong, DoiTuong desc

	--IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTS]') AND type in (N'U'))
	--drop table TBL_TCTS;
	--IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTS_DOC]') AND type in (N'U'))
	--drop table TBL_TCTS_DOC;
	--IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTS_SQ]') AND type in (N'U'))
	--drop table TBL_TCTS_SQ;
	--IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTS_QNCN]') AND type in (N'U'))
	--drop table TBL_TCTS_QNCN;
	--IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTS_HSQBS]') AND type in (N'U'))
	--drop table TBL_TCTS_HSQBS;
	--IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTS_VCQP]') AND type in (N'U'))
	--drop table TBL_TCTS_VCQP;
	--IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTS_LDHD]') AND type in (N'U'))
	--drop table TBL_TCTS_LDHD;
	--IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTS_RESULT]') AND type in (N'U'))
	--drop table TBL_TCTS_RESULT;

END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_tnld_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_tnld_nq104] 
	@DsMaDonVi nvarchar(100),
	@NamLamViec int,
	@Thang int,
	@DonViTinh int
AS
BEGIN

	--Lay thong tin luong theo tro cap tai nan lao dong
	select * into TBL_TCTNLD from
	(select donvi.Ten_DonVi, luong.*
	from TL_BangLuong_Thang_NQ104BHXH luong
	join TL_DM_DonVi_NQ104 donvi on luong.sMaDonVi = donvi.Ma_DonVi
	where 
	luong.sMaDonVi in (SELECT * FROM f_split(@DsMaDonVi))
	and luong.iNam = @NamLamViec
	and luong.iThang = @Thang
	and luong.sMaCheDo in ('CHIGIAMDINH', 'TAINANLD_TROCAP1LAN', 'TROCAPTHEOPHIEUTRUYTRA', 'TROCAPHANGTHANG', 'TROCAPPHCN', 'TROCAPPHUCVU', 'TROCAPCHETDOTNLD', 'TAINANLĐ_DUONGSUCPHSK')) tctnld

	select distinct 
		TBL_TCTNLD.sMaCB,
		TBL_TCTNLD.sMaCBo,
		--TBL_TCTNLD.sMaCheDo,
		TBL_TCTNLD.sTenCbo,
		TBL_TCTNLD.Ten_DonVi,
		CHIGIAMDINH.nGiaTri fChiGiamDinh,
		TROCAP1LAN.nGiaTri fTroCap1Lan,
		TROCAPTHEOPHIEUTRUYTRA.nGiaTri fTroCapTheoPhieuTruyTra,
		TROCAPHANGTHANG.nGiaTri fTroCapHangThang,
		TROCAPPHCN.nGiaTri fTroCapPHCN,
		TROCAPCHETDOTNLD.nGiaTri fTroCapChetDoTNLD,
		TAINANLĐ_DUONGSUCPHSK.SoNgayDuongSucTNLD,
		TAINANLĐ_DUONGSUCPHSK.nGiaTri fDuongSucTNLD,
		chedocha.sSoQuyetDinh,
		chedocha.dNgayQuyetDinh
		into TBL_TCTNLD_DOC
	from TBL_TCTNLD TBL_TCTNLD
		left join (select top 1 * from TL_CanBo_CheDoBHXH 
		where sMaCheDo in ('CHIGIAMDINH', 'TAINANLD_TROCAP1LAN', 'TROCAPTHEOPHIEUTRUYTRA', 'TROCAPHANGTHANG', 'TROCAPPHCN', 'TROCAPPHUCVU', 'TROCAPCHETDOTNLD', 'TAINANLĐ_DUONGSUCPHSK')
		and ((sSoQuyetDinh is not null and sSoQuyetDinh <> '') 
		or (dNgayQuyetDinh is not null and dNgayQuyetDinh <> ''))) chedocha
		on TBL_TCTNLD.sMaCBo = chedocha.sMaCanBo
		left join
		(select tnld.Id, tnld.sMaDonVi, tnld.nGiaTri, tnld.sMaCB, tnld.sMaCBo, tnld.sMaCheDo, tnld.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTroCap1Lan
		from TBL_TCTNLD tnld left join TL_CanBo_CheDoBHXH chedo on tnld.sMaCBo = chedo.sMaCanBo and tnld.sMaCheDo = chedo.sMaCheDo
		where tnld.sMaCheDo = 'TAINANLD_TROCAP1LAN') TROCAP1LAN
		on TBL_TCTNLD.sMaCBo = TROCAP1LAN.sMaCBo and TBL_TCTNLD.sMaDonVi = TROCAP1LAN.sMaDonVi
		left join
		(select tnld_1.Id, tnld_1.sMaDonVi, tnld_1.nGiaTri, tnld_1.sMaCB, tnld_1.sMaCBo, tnld_1.sMaCheDo, tnld_1.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTroCapTheoPhieuTruyTra
		from TBL_TCTNLD tnld_1 left join TL_CanBo_CheDoBHXH chedo on tnld_1.sMaCBo = chedo.sMaCanBo and tnld_1.sMaCheDo = chedo.sMaCheDo
		where tnld_1.sMaCheDo = 'TROCAPTHEOPHIEUTRUYTRA') TROCAPTHEOPHIEUTRUYTRA
		on TBL_TCTNLD.sMaCBo = TROCAPTHEOPHIEUTRUYTRA.sMaCBo and TBL_TCTNLD.sMaDonVi = TROCAPTHEOPHIEUTRUYTRA.sMaDonVi
		left join
		(select tnld_2.Id, tnld_2.sMaDonVi, tnld_2.nGiaTri, tnld_2.sMaCB, tnld_2.sMaCBo, tnld_2.sMaCheDo, tnld_2.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTroCapHangThang
		from TBL_TCTNLD tnld_2 left join TL_CanBo_CheDoBHXH chedo on tnld_2.sMaCBo = chedo.sMaCanBo and tnld_2.sMaCheDo = chedo.sMaCheDo
		where tnld_2.sMaCheDo = 'TROCAPHANGTHANG') TROCAPHANGTHANG
		on TBL_TCTNLD.sMaCBo = TROCAPHANGTHANG.sMaCBo and TBL_TCTNLD.sMaDonVi = TROCAPHANGTHANG.sMaDonVi
		left join
		(select tnld_3.Id, tnld_3.sMaDonVi, sum(tnld_3.nGiaTri) nGiaTri, tnld_3.sMaCB, tnld_3.sMaCBo, tnld_3.sMaCheDo, tnld_3.sTenCbo, sum(chedo.fSoNgayHuongBHXH) SoNgayTroCapPHCN
		from TBL_TCTNLD tnld_3 left join TL_CanBo_CheDoBHXH chedo on tnld_3.sMaCBo = chedo.sMaCanBo and tnld_3.sTenCbo = chedo.sMaCheDo
		where tnld_3.sMaCheDo in ('TROCAPPHCN', 'TROCAPPHUCVU')
		group by tnld_3.Id, tnld_3.sMaDonVi, tnld_3.sMaCB, tnld_3.sMaCBo, tnld_3.sMaCheDo, tnld_3.sTenCbo) TROCAPPHCN
		on TBL_TCTNLD.sMaCBo = TROCAPPHCN.sMaCBo and TBL_TCTNLD.sMaDonVi = TROCAPPHCN.sMaDonVi
		left join
		(select tnld_4.Id, tnld_4.sMaDonVi, tnld_4.nGiaTri, tnld_4.sMaCB, tnld_4.sMaCBo, tnld_4.sMaCheDo, tnld_4.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTroCapChetDoTNLD
		from TBL_TCTNLD tnld_4 left join TL_CanBo_CheDoBHXH chedo on tnld_4.sMaCBo = chedo.sMaCanBo and tnld_4.sMaCheDo = chedo.sMaCheDo
		where tnld_4.sMaCheDo = 'TROCAPCHETDOTNLD') TROCAPCHETDOTNLD
		on TBL_TCTNLD.sMaCBo = TROCAPCHETDOTNLD.sMaCBo and TBL_TCTNLD.sMaDonVi = TROCAPCHETDOTNLD.sMaDonVi
		left join
		(select tnld_5.Id, tnld_5.sMaDonVi, tnld_5.nGiaTri, tnld_5.sMaCB, tnld_5.sMaCBo, tnld_5.sMaCheDo, tnld_5.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayDuongSucTNLD
		from TBL_TCTNLD tnld_5 left join TL_CanBo_CheDoBHXH chedo on tnld_5.sMaCBo = chedo.sMaCanBo and tnld_5.sMaCheDo = chedo.sMaCheDo
		where tnld_5.sMaCheDo = 'TAINANLĐ_DUONGSUCPHSK') TAINANLĐ_DUONGSUCPHSK
		on TBL_TCTNLD.sMaCBo = TAINANLĐ_DUONGSUCPHSK.sMaCBo and TBL_TCTNLD.sMaDonVi = TAINANLĐ_DUONGSUCPHSK.sMaDonVi
		left join
		(select tnld_6.Id, tnld_6.sMaDonVi, tnld_6.nGiaTri, tnld_6.sMaCB, tnld_6.sMaCBo, tnld_6.sMaCheDo, tnld_6.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayCHIGIAMDINH
		from TBL_TCTNLD tnld_6 left join TL_CanBo_CheDoBHXH chedo on tnld_6.sMaCBo = chedo.sMaCanBo and tnld_6.sMaCheDo = chedo.sMaCheDo
		where tnld_6.sMaCheDo = 'CHIGIAMDINH') CHIGIAMDINH
		on TBL_TCTNLD.sMaCBo = CHIGIAMDINH.sMaCBo and TBL_TCTNLD.sMaDonVi = CHIGIAMDINH.sMaDonVi

	--Lấy lương Sĩ quan
	select * into TBL_TCTNLD_SQ from
	(select
		1 bHangCha, 1 RowNum, 'I' STT, 'SQ' DoiTuong, 'SQ' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, N'SQ' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null SoNgayDuongSucTNLD, null fChiGiamDinh, null fTroCap1Lan, null fTroCapTheoPhieuTruyTra, null fTroCapHangThang, null fTroCapPHCN, null fTroCapChetDoTNLD, null fDuongSucTNLD, 0 bHasData
	union all
	select
		0 bHangCha, 1 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'SQ' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fTroCapChetDoTNLD, fDuongSucTNLD, 0 bHasData
	from TBL_TCTNLD_DOC
	where sMaCB like '1%' and (isnull(fChiGiamDinh, 0) <> 0 or isnull(fTroCap1Lan, 0) <> 0 or isnull(fTroCapTheoPhieuTruyTra, 0) <> 0 or isnull(fTroCapHangThang, 0) <> 0 or isnull(fTroCapPHCN, 0) <> 0 or isnull(fTroCapChetDoTNLD, 0) <> 0 or isnull(fDuongSucTNLD, 0) <> 0)) sq
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCTNLD_SQ) > 1
		update TBL_TCTNLD_SQ set bHasData = 1

	--Lấy lương QNCN
	select * into TBL_TCTNLD_QNCN from
	(select
		1 bHangCha, 2 RowNum, 'II' STT, 'QNCN' DoiTuong, 'QNCN' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, N'QNCN' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null SoNgayDuongSucTNLD, null fChiGiamDinh, null fTroCap1Lan, null fTroCapTheoPhieuTruyTra, null fTroCapHangThang, null fTroCapPHCN, null fTroCapChetDoTNLD, null fDuongSucTNLD, 0 bHasData
	union all
	select
		0 bHangCha, 2 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'QNCN' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fTroCapChetDoTNLD, fDuongSucTNLD, 0 bHasData
	from TBL_TCTNLD_DOC
	where sMaCB like '2%' and (isnull(fChiGiamDinh, 0) <> 0 or isnull(fTroCap1Lan, 0) <> 0 or isnull(fTroCapTheoPhieuTruyTra, 0) <> 0 or isnull(fTroCapHangThang, 0) <> 0 or isnull(fTroCapPHCN, 0) <> 0 or isnull(fTroCapChetDoTNLD, 0) <> 0 or isnull(fDuongSucTNLD, 0) <> 0)) qncn
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCTNLD_QNCN) > 1
		update TBL_TCTNLD_QNCN set bHasData = 1

	--Lấy lương HSQ_BS
	select * into TBL_TCTNLD_HSQBS from
	(select
		1 bHangCha, 3 RowNum, 'III' STT, 'HSQ_BS' DoiTuong, 'HSQ_BS' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, N'HSQ_BS' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null SoNgayDuongSucTNLD, null fChiGiamDinh, null fTroCap1Lan, null fTroCapTheoPhieuTruyTra, null fTroCapHangThang, null fTroCapPHCN, null fTroCapChetDoTNLD, null fDuongSucTNLD, 0 bHasData
	union all
	select
		0 bHangCha, 3 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'HSQ_BS' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fTroCapChetDoTNLD, fDuongSucTNLD, 0 bHasData
	from TBL_TCTNLD_DOC
	where sMaCB like '0%' and (isnull(fChiGiamDinh, 0) <> 0 or isnull(fTroCap1Lan, 0) <> 0 or isnull(fTroCapTheoPhieuTruyTra, 0) <> 0 or isnull(fTroCapHangThang, 0) <> 0 or isnull(fTroCapPHCN, 0) <> 0 or isnull(fTroCapChetDoTNLD, 0) <> 0 or isnull(fDuongSucTNLD, 0) <> 0)) hsqbs
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCTNLD_HSQBS) > 1
		update TBL_TCTNLD_HSQBS set bHasData = 1

	--Lấy lương VCQP
	select * into TBL_TCTNLD_VCQP from
	(select
		1 bHangCha, 4 RowNum, 'IV' STT, 'VCQP' DoiTuong, 'VCQP' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, N'VCQP' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null SoNgayDuongSucTNLD, null fChiGiamDinh, null fTroCap1Lan, null fTroCapTheoPhieuTruyTra, null fTroCapHangThang, null fTroCapPHCN, null fTroCapChetDoTNLD, null fDuongSucTNLD, 0 bHasData
	union all
	select
		0 bHangCha, 4 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'VCQP' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fTroCapChetDoTNLD, fDuongSucTNLD, 0 bHasData
	from TBL_TCTNLD_DOC
	where sMaCB in ('3.1', '3.2', '3.3') and (isnull(fChiGiamDinh, 0) <> 0 or isnull(fTroCap1Lan, 0) <> 0 or isnull(fTroCapTheoPhieuTruyTra, 0) <> 0 or isnull(fTroCapHangThang, 0) <> 0 or isnull(fTroCapPHCN, 0) <> 0 or isnull(fTroCapChetDoTNLD, 0) <> 0 or isnull(fDuongSucTNLD, 0) <> 0)) vcqp
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCTNLD_VCQP) > 1
		update TBL_TCTNLD_VCQP set bHasData = 1

	--Lấy lương Lao động hợp đông
	select * into TBL_TCTNLD_LDHD from
	(select
		1 bHangCha, 5 RowNum, 'V' STT, 'LDHD' DoiTuong, 'LDHD' LoaiDoiTuong, null TenDonVi, null sMaCB, null sMaCBo, N'LDHD' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null SoNgayDuongSucTNLD, null fChiGiamDinh, null fTroCap1Lan, null fTroCapTheoPhieuTruyTra, null fTroCapHangThang, null fTroCapPHCN, null fTroCapChetDoTNLD, null fDuongSucTNLD, 0 bHasData
	union all
	select
		0 bHangCha, 5 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'LDHD' LoaiDoiTuong, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fTroCapChetDoTNLD, fDuongSucTNLD, 0 bHasData
	from TBL_TCTNLD_DOC
	where sMaCB = '43' and (isnull(fChiGiamDinh, 0) <> 0 or isnull(fTroCap1Lan, 0) <> 0 or isnull(fTroCapTheoPhieuTruyTra, 0) <> 0 or isnull(fTroCapHangThang, 0) <> 0 or isnull(fTroCapPHCN, 0) <> 0 or isnull(fTroCapChetDoTNLD, 0) <> 0 or isnull(fDuongSucTNLD, 0) <> 0)) ldhd
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCTNLD_LDHD) > 1
		update TBL_TCTNLD_LDHD set bHasData = 1

	--Ket qua
	select result.* into TBL_TCTNLD_RESULT from
	(select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fTroCapChetDoTNLD, fDuongSucTNLD,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) fTongSoTien
	from TBL_TCTNLD_SQ
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fTroCapChetDoTNLD, fDuongSucTNLD,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fTroCapChetDoTNLD, 0)) fTongSoTien
	from TBL_TCTNLD_QNCN
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fTroCapChetDoTNLD, fDuongSucTNLD,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fTroCapChetDoTNLD, 0)) fTongSoTien
	from TBL_TCTNLD_HSQBS
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fTroCapChetDoTNLD, fDuongSucTNLD,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fTroCapChetDoTNLD, 0)) fTongSoTien
	from TBL_TCTNLD_VCQP
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fTroCapChetDoTNLD, fDuongSucTNLD,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fTroCapChetDoTNLD, 0)) fTongSoTien
	from TBL_TCTNLD_LDHD) result

	select 
		RowNum,
		STT,
		DoiTuong, 
		LoaiDoiTuong,
		sMaCB MaCb, 
		sMaCBo MaCbo, 
		sTenCbo TenCbo,
		sSoQuyetDinh SSoQuyetDinh,
		dNgayQuyetDinh DNgayQuyetDinh,
		TenDonVi,
		SoNgayDuongSucTNLD,
		fChiGiamDinh/@DonViTinh fChiGiamDinh,
		fTroCap1Lan/@DonViTinh fTroCap1Lan,
		fTroCapTheoPhieuTruyTra/@DonViTinh fTroCapTheoPhieuTruyTra,
		fTroCapHangThang/@DonViTinh fTroCapHangThang,
		fTroCapPHCN/@DonViTinh fTroCapPHCN,
		fTroCapChetDoTNLD/@DonViTinh fTroCapChetDoTNLD,
		fDuongSucTNLD/@DonViTinh fDuongSucTNLD,
		fTongSoTien/@DonViTinh FTongSoTien,
		bHangCha IsHangCha,
		bHasData IsHasData
	from TBL_TCTNLD_RESULT
	order by RowNum, LoaiDoiTuong, DoiTuong desc

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD]') AND type in (N'U'))
	drop table TBL_TCTNLD;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_DOC]') AND type in (N'U'))
	drop table TBL_TCTNLD_DOC;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_SQ]') AND type in (N'U'))
	drop table TBL_TCTNLD_SQ;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_QNCN]') AND type in (N'U'))
	drop table TBL_TCTNLD_QNCN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_HSQBS]') AND type in (N'U'))
	drop table TBL_TCTNLD_HSQBS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_VCQP]') AND type in (N'U'))
	drop table TBL_TCTNLD_VCQP;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_LDHD]') AND type in (N'U'))
	drop table TBL_TCTNLD_LDHD;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_RESULT]') AND type in (N'U'))
	drop table TBL_TCTNLD_RESULT;

END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_xuat_ngu_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_xuat_ngu_nq104]
	@DsMaDonVi nvarchar(100),
	@NamLamViec int,
	@Thang int,
	@DonViTinh int
AS
BEGIN
	--Lay thong tin luong theo tro cap thai san
	select * into TBL_TCXN from
	(select donvi.Ten_DonVi,
		chedocha.sSoQuyetDinh,
		chedocha.dNgayQuyetDinh,
		luong.*
	from TL_BangLuong_Thang_NQ104BHXH luong
	join TL_DM_DonVi_NQ104 donvi on luong.sMaDonVi = donvi.Ma_DonVi
	left join (select top 1 * from TL_CanBo_CheDoBHXH 
		where sMaCheDo in ('XUATNGU_TROCAP1LAN')
		and ((sSoQuyetDinh is not null and sSoQuyetDinh <> '') 
		or (dNgayQuyetDinh is not null and dNgayQuyetDinh <> ''))) chedocha
		on luong.sMaCBo = chedocha.sMaCanBo
	where 
	luong.sMaDonVi in (SELECT * FROM f_split(@DsMaDonVi))
	and luong.iNam = @NamLamViec
	and luong.iThang = @Thang
	and luong.sMaCheDo in ('XUATNGU_TROCAP1LAN')) tcod

	--Lấy lương Sĩ quan
	select * into TBL_TCXN_SQ from
	(select CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, Id, sMaCB, sMaCBo, sMaCheDo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, nGiaTri
	from TBL_TCXN
	where sMaCB like '1%') sq

	--Lấy lương QNCN
	select * into TBL_TCXN_QNCN from
	(select CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, Id, sMaCB, sMaCBo, sMaCheDo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, nGiaTri
	from TBL_TCXN
	where sMaCB like '2%') qncn
	--Lấy lương HSQ_BS
	select * into TBL_TCXN_HSQBS from
	(select CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, Id, sMaCB, sMaCBo, sMaCheDo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, nGiaTri
	from TBL_TCXN
	where sMaCB like '0%') hsqbs

	--Lấy lương VCQP
	select * into TBL_TCXN_VCQP from
	(select CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, Id, sMaCB, sMaCBo, sMaCheDo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, nGiaTri
	from TBL_TCXN
	where sMaCB in ('3.1', '3.2', '3.3')) vcqp

	--Lấy lương Lao động hợp đông
	select * into TBL_TCXN_LDHD from
	(select CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, Id, sMaCB, sMaCBo, sMaCheDo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, nGiaTri
	from TBL_TCXN
	where sMaCB = '43') ldhd

	--Ket qua
	select result.* into TBL_TCXN_RESULT from
	(select STT, Id, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, nGiaTri from TBL_TCXN_SQ
	union
	select STT, Id, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, nGiaTri from TBL_TCXN_QNCN
	union
	select STT, Id, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, nGiaTri from TBL_TCXN_HSQBS
	union
	select STT,Id, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, nGiaTri from TBL_TCXN_VCQP
	union
	select STT, Id, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, nGiaTri from TBL_TCXN_LDHD) result

	select
		STT,
		sMaCB MaCb, 
		sMaCBo MaCbo, 
		sTenCbo TenCbo,
		sSoQuyetDinh,
		dNgayQuyetDinh,
		nGiaTri/@DonViTinh FTroCap1Lan
	from TBL_TCXN_RESULT
	
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCXN]') AND type in (N'U'))
	drop table TBL_TCXN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCXN_SQ]') AND type in (N'U'))
	drop table TBL_TCXN_SQ;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCXN_QNCN]') AND type in (N'U'))
	drop table TBL_TCXN_QNCN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCXN_HSQBS]') AND type in (N'U'))
	drop table TBL_TCXN_HSQBS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCXN_VCQP]') AND type in (N'U'))
	drop table TBL_TCXN_VCQP;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCXN_LDHD]') AND type in (N'U'))
	drop table TBL_TCXN_LDHD;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCXN_RESULT]') AND type in (N'U'))
	drop table TBL_TCXN_RESULT;
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_giai_thich_che_do_om_dau_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_qtcq_giai_thich_che_do_om_dau_nq104]
	@NamLamViec int,
	@DVT int,
	@Quy int,
	@DonVi nvarchar(200)
AS
BEGIN
	---Bệnh dài ngày
	select gt.* into TBL_BenhDaiNgay from BH_QTC_Quy_CTCT_GiaiThichTroCap gt
	where (sXauNoiMa like '9010001-010-011-0001-0001-0001-01%' or sXauNoiMa like '9010002-010-011-0001-0001-0001-01%')
		and iNamLamViec = @NamLamViec and iQuy = @Quy and iiD_MaDonVi in (SELECT * FROM f_split(@DonVi))

	--Sy quan
	select * into TBL_BDN_SiQuan from
	(select 1 bHangCha, N'A' LoaiTC, 1 RowNum, N'A' STT, null DoiTuong, null LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'Thuộc Danh mục bệnh cần chữa trị dài ngày' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 1 bHangCha, N'A' LoaiTC, 1 RowNum, N'I' STT, 'SQ' DoiTuong, 'SQ' LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'Sỹ quan' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 0 bHangCha, 'A' LoaiTC,  1 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT, '', 'SQ' LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, sum(iSoNgayHuong) iSoNgayHuong, sum(fSoTien) fSoTien, iiD_MaPhanHo, 0 bHasData
	from TBL_BenhDaiNgay
	 where sMaCapBac like '1%' 
	 group by sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iiD_MaPhanHo) sq

	 if (select count(1) from TBL_BDN_SiQuan) > 2
		update TBL_BDN_SiQuan set bHasData = 1

	 --QNCN
	select * into TBL_BDN_QNCN from
	(select 1 bHangCha, N'A' LoaiTC, 1 RowNum, N'A' STT, null DoiTuong, null LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'Thuộc Danh mục bệnh cần chữa trị dài ngày' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 1 bHangCha, N'A' LoaiTC, 2 RowNum, N'I' STT, 'QNCN' DoiTuong, 'QNCN' LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'Quân nhân chuyên nghiệp' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 0 bHangCha, 'A' LoaiTC,  2 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT, '', 'QNCN' LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, sum(iSoNgayHuong) iSoNgayHuong, sum(fSoTien) fSoTien, iiD_MaPhanHo, 0 bHasData
	from TBL_BenhDaiNgay
	 where sMaCapBac like '2%' 
	 group by sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iiD_MaPhanHo) qncn

	 if (select count(1) from TBL_BDN_QNCN) > 2
		update TBL_BDN_QNCN set bHasData = 1

	 --HSQ_BS
	select * into TBL_BDN_HSQ_BS from
	(select 1 bHangCha, N'A' LoaiTC, 1 RowNum, N'A' STT, null DoiTuong, null LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'Thuộc Danh mục bệnh cần chữa trị dài ngày' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 1 bHangCha, N'A' LoaiTC, 3 RowNum, N'I' STT, 'HSQ_BS' DoiTuong, 'HSQ_BS' LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'HSQ, BS' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 0 bHangCha, 'A' LoaiTC,  3 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT, '', 'HSQ_BS' LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, sum(iSoNgayHuong) iSoNgayHuong, sum(fSoTien) fSoTien, iiD_MaPhanHo, 0 bHasData
	from TBL_BenhDaiNgay
	 where sMaCapBac like '0%' 
	 group by sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iiD_MaPhanHo) hsq

	 if (select count(1) from TBL_BDN_HSQ_BS) > 2
		update TBL_BDN_HSQ_BS set bHasData = 1

	 --VCQP
	select * into TBL_BDN_VCQP from
	(select 1 bHangCha, N'A' LoaiTC, 1 RowNum, N'A' STT, null DoiTuong, null LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'Thuộc Danh mục bệnh cần chữa trị dài ngày' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 1 bHangCha, N'A' LoaiTC, 4 RowNum, N'I' STT, 'VCQP' DoiTuong, 'VCQP' LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'CC, CN và VC quốc phòng' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 0 bHangCha, 'A' LoaiTC,  4 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT, '', 'VCQP' LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, sum(iSoNgayHuong) iSoNgayHuong, sum(fSoTien) fSoTien, iiD_MaPhanHo, 0 bHasData
	from TBL_BenhDaiNgay
	 where sMaCapBac in ('3.1', '3.2', '3.3')
	 group by sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iiD_MaPhanHo) vcqp

	 if (select count(1) from TBL_BDN_VCQP) > 2
		update TBL_BDN_VCQP set bHasData = 1

	 --HDLD
	select * into TBL_BDN_HDLD from
	(select 1 bHangCha, N'A' LoaiTC, 1 RowNum, N'A' STT, null DoiTuong, null LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'Thuộc Danh mục bệnh cần chữa trị dài ngày' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 1 bHangCha, N'A' LoaiTC, 5 RowNum, N'I' STT, 'HDLD' DoiTuong, 'HDLD' LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'Hợp đồng lao động' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 0 bHangCha, 'A' LoaiTC,  5 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT, '', 'HDLD' LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, sum(iSoNgayHuong) iSoNgayHuong, sum(fSoTien) fSoTien, iiD_MaPhanHo, 0 bHasData
	from TBL_BenhDaiNgay
	 where sMaCapBac = '43'
	 group by sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iiD_MaPhanHo) vcqp

	 if (select count(1) from TBL_BDN_HDLD) > 2
		update TBL_BDN_HDLD set bHasData = 1

	----------------------------
	---Ốm khác
	select * into TBL_OmKhac from BH_QTC_Quy_CTCT_GiaiThichTroCap
	where (sXauNoiMa like '9010001-010-011-0001-0001-0001-02%' or sXauNoiMa like '9010002-010-011-0001-0001-0001-02%')
		and iNamLamViec = @NamLamViec and iQuy = @Quy and iiD_MaDonVi in (SELECT * FROM f_split(@DonVi))

	--Sy quan
	select * into TBL_OK_SiQuan from
	(select 1 bHangCha, N'B' LoaiTC, 1 RowNum, N'B' STT, null DoiTuong, null LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'Không thuộc Danh mục bệnh cần chữa trị dài ngày' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 1 bHangCha, N'B' LoaiTC, 1 RowNum, N'I' STT, 'SQ' DoiTuong, 'SQ' LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'Sỹ quan' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 0 bHangCha, 'B' LoaiTC,  1 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT, '', 'SQ' LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, sum(iSoNgayHuong) iSoNgayHuong, sum(fSoTien) fSoTien, iiD_MaPhanHo, 0 bHasData
	from TBL_OmKhac
	 where sMaCapBac like '1%' 
	 group by sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iiD_MaPhanHo) sq

	 if (select count(1) from TBL_OK_SiQuan) > 2
		update TBL_OK_SiQuan set bHasData = 1

	 --QNCN
	select * into TBL_OK_QNCN from
	(select 1 bHangCha, N'B' LoaiTC, 1 RowNum, N'B' STT, null DoiTuong, null LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'Không thuộc Danh mục bệnh cần chữa trị dài ngày' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 1 bHangCha, N'B' LoaiTC, 2 RowNum, N'I' STT, 'QNCN' DoiTuong, 'QNCN' LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'Quân nhân chuyên nghiệp' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 0 bHangCha, 'B' LoaiTC,  2 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT, '', 'QNCN' LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, sum(iSoNgayHuong) iSoNgayHuong, sum(fSoTien) fSoTien, iiD_MaPhanHo, 0 bHasData
	from TBL_OmKhac
	 where sMaCapBac like '2%' 
	 group by sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iiD_MaPhanHo) qncn

	 if (select count(1) from TBL_OK_QNCN) > 2
		update TBL_OK_QNCN set bHasData = 1

	 --HSQ_BS
	select * into TBL_OK_HSQ_BS from
	(select 1 bHangCha, N'B' LoaiTC, 1 RowNum, N'A' STT, null DoiTuong, null LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'Không thuộc Danh mục bệnh cần chữa trị dài ngày' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 1 bHangCha, N'B' LoaiTC, 3 RowNum, N'I' STT, 'HSQ_BS' DoiTuong, 'HSQ_BS' LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'HSQ, BS' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 0 bHangCha, 'B' LoaiTC,  3 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT, '', 'HSQ_BS' LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, sum(iSoNgayHuong) iSoNgayHuong, sum(fSoTien) fSoTien, iiD_MaPhanHo, 0 bHasData
	from TBL_OmKhac
	 where sMaCapBac like '0%' 
	 group by sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iiD_MaPhanHo) hsq

	 if (select count(1) from TBL_OK_HSQ_BS) > 2
		update TBL_OK_HSQ_BS set bHasData = 1

	 --VCQP
	select * into TBL_OK_VCQP from
	(select 1 bHangCha, N'B' LoaiTC, 1 RowNum, N'B' STT, null DoiTuong, null LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'Không thuộc Danh mục bệnh cần chữa trị dài ngày' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 1 bHangCha, N'B' LoaiTC, 4 RowNum, N'I' STT, 'VCQP' DoiTuong, 'VCQP' LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'CC, CN và VC quốc phòng' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 0 bHangCha, 'B' LoaiTC,  4 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT, '', 'VCQP' LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, sum(iSoNgayHuong) iSoNgayHuong, sum(fSoTien) fSoTien, iiD_MaPhanHo, 0 bHasData
	from TBL_OmKhac
	 where sMaCapBac in ('3.1', '3.2', '3.3')
	 group by sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iiD_MaPhanHo) vcqp

	 if (select count(1) from TBL_OK_VCQP) > 2
		update TBL_OK_VCQP set bHasData = 1

	 --HDLD
	select * into TBL_OK_HDLD from
	(select 1 bHangCha, N'B' LoaiTC, 1 RowNum, N'B' STT, null DoiTuong, null LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'Không thuộc Danh mục bệnh cần chữa trị dài ngày' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 1 bHangCha, N'B' LoaiTC, 5 RowNum, N'I' STT, 'HDLD' DoiTuong, 'HDLD' LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'Hợp đồng lao động' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 0 bHangCha, 'B' LoaiTC,  5 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT, '', 'HDLD' LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, sum(iSoNgayHuong) iSoNgayHuong, sum(fSoTien) fSoTien, iiD_MaPhanHo, 0 bHasData
	from TBL_OmKhac
	 where sMaCapBac = '43'
	 group by sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iiD_MaPhanHo) vcqp

	 if (select count(1) from TBL_OK_HDLD) > 2
		update TBL_OK_HDLD set bHasData = 1
	-----------------------------------
	--Ket qua
	select TBL_GTCD_RESULT.* into TBL_GTCD_RESULT from(
	select bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iSoNgayHuong, fSoTien, iiD_MaPhanHo, bHasData 
	from TBL_BDN_SiQuan
	union all
	select bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iSoNgayHuong, fSoTien,iiD_MaPhanHo, bHasData 
	from TBL_BDN_QNCN
	union all
	select bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iSoNgayHuong, fSoTien, iiD_MaPhanHo, bHasData 
	from TBL_BDN_HSQ_BS
	union all
	select bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iSoNgayHuong, fSoTien, iiD_MaPhanHo, bHasData 
	from TBL_BDN_VCQP
	union all
	select bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iSoNgayHuong, fSoTien, iiD_MaPhanHo, bHasData 
	from TBL_BDN_HDLD
	union all
	select bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iSoNgayHuong, fSoTien, iiD_MaPhanHo, bHasData 
	from TBL_OK_SiQuan
	union all
	select bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iSoNgayHuong, fSoTien, iiD_MaPhanHo, bHasData 
	from TBL_OK_QNCN
	union all
	select bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iSoNgayHuong, fSoTien, iiD_MaPhanHo, bHasData 
	from TBL_OK_HSQ_BS
	union all
	select bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iSoNgayHuong, fSoTien, iiD_MaPhanHo, bHasData 
	from TBL_OK_VCQP
	union all
	select bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iSoNgayHuong, fSoTien, iiD_MaPhanHo, bHasData 
	from TBL_OK_HDLD) TBL_GTCD_RESULT

	select distinct
		gt.bHangCha IsHangCha, 
		gt.LoaiTC, 
		gt.RowNum, 
		gt.STT, 
		gt.DoiTuong,
		gt.LoaiDoiTuong,
		case 
			when gt.bHangCha = 0 then concat(gt.sMa_Hieu_Can_Bo, ' - ', donvi.Ten_DonVi)
			else ''
		end as sMa_Hieu_Can_Bo,
		gt.sTenCanBo, 
		gt.sTenCapBac, 
		gt.sSoSoBHXH, 
		gt.fTienLuongThangDongBHXH, 
		gt.dTuNgay, 
		gt.dDenNgay, 
		gt.iSoNgayHuong, 
		gt.fSoTien/@DVT fSoTien, 
		gt.bHasData from TBL_GTCD_RESULT gt
		left join TL_DM_DonVi_NQ104 donvi on gt.iiD_MaPhanHo = donvi.Ma_DonVi and donvi.iTrangThai = 1
	where bHasData = 1
	order by LoaiTC, RowNum, LoaiDoiTuong, DoiTuong desc

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_BenhDaiNgay]') AND type in (N'U')) drop table TBL_BenhDaiNgay;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_OmKhac]') AND type in (N'U')) drop table TBL_OmKhac;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_BDN_SiQuan]') AND type in (N'U')) drop table TBL_BDN_SiQuan;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_BDN_QNCN]') AND type in (N'U')) drop table TBL_BDN_QNCN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_BDN_HSQ_BS]') AND type in (N'U')) drop table TBL_BDN_HSQ_BS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_BDN_VCQP]') AND type in (N'U')) drop table TBL_BDN_VCQP;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_BDN_HDLD]') AND type in (N'U')) drop table TBL_BDN_HDLD;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_OK_SiQuan]') AND type in (N'U')) drop table TBL_OK_SiQuan;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_OK_QNCN]') AND type in (N'U')) drop table TBL_OK_QNCN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_OK_HSQ_BS]') AND type in (N'U')) drop table TBL_OK_HSQ_BS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_OK_VCQP]') AND type in (N'U')) drop table TBL_OK_VCQP;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_OK_HDLD]') AND type in (N'U')) drop table TBL_OK_HDLD;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_GTCD_RESULT]') AND type in (N'U')) drop table TBL_GTCD_RESULT;

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bang_luong_thang_bhxh_chitiet_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_bang_luong_thang_bhxh_chitiet_nq104]
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
		FROM TL_DM_CanBo_NQ104 canBo
		INNER JOIN TL_DM_DonVi_NQ104 donVi
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
		FROM TL_BangLuong_Thang_NQ104BHXH bangLuongThang
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
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bang_luong_thang_dong_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_bang_luong_thang_dong_nq104] 
	@maDonVi nvarchar(MAX),
	@ngach varchar(10),
	@maPhuCap nvarchar(50),
	@thang int, 
	@nam int 
AS 
BEGIN

DECLARE @query AS NVARCHAR(MAX);

SET @query = 'WITH SoLieuBangLuong AS  (
  SELECT Parent, MaCanBo, MaCapBac, ' + @maPhuCap + ', TenDonVi FROM (
    SELECT
    canBo.Ma_CB AS MaCapBac,
    dsCapNhapBangLuong.Id AS Parent,
      dsCapNhapBangLuong.Thang AS Thang,
      dsCapNhapBangLuong.Nam AS Nam, 
      donVi.Ten_Donvi TenDonVi,
      canBo.Ma_CanBo MaCanBo,
      canBo.Ten_CanBo AS TenCanBo,
      bangLuong.GIA_TRI AS GiaTri,
      bangLuong.MA_PHU_CAP AS MaPhuCap
    FROM TL_BangLuong_Thang_Bridge_NQ104 bangLuong
    JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhapBangLuong ON bangLuong.parent = dsCapNhapBangLuong.Id
    JOIN TL_DM_CanBo_NQ104 canBo ON bangLuong.Ma_Can_Bo=canBo.Ma_CanBo
    JOIN TL_DM_DonVi_NQ104 donVi ON bangLuong.Ma_Don_Vi=donVi.Ma_DonVi
    WHERE
      dsCapNhapBangLuong.Ma_CachTL=''' + 'CACH0' + '''
      AND dsCapNhapBangLuong.Status=1
      AND canBo.IsDelete=1
      AND canBo.Khong_Luong=0
      AND bangLuong.Ma_Phu_Cap IN (''' + @maPhuCap + ''')
  ) x
  PIVOT
  (
    SUM(GiaTri)
    FOR MaPhuCap IN (' + @maPhuCap + ')
  ) pvt
)

SELECT
  canBo.Ma_CB as MaCb,
  null AS HeSoLuong,
  canBo.Parent as MaDonVi,
    COUNT(canBo.Ma_CB) AS QuanSo,
    SUM(bangLuong.' + @maPhuCap + ') AS Tien
FROM SoLieuBangLuong bangLuong
INNER JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhatBangLuong
  ON bangLuong.parent = dsCapNhatBangLuong.Id
INNER JOIN TL_DM_CanBo_NQ104 canBo
  ON canBo.Ma_CanBo = bangLuong.MaCanBo
INNER JOIN TL_DM_CapBac_NQ104 capBac
  ON canBo.Ma_CB104 = capBac.Ma_Cb
WHERE canBo.Parent IN
    (SELECT *
     FROM f_split(''' + @maDonVi + '''))
  AND capBac.XauNoiMa LIKE ''' + @ngach + '%' + '''
  AND dsCapNhatBangLuong.THANG = ' + CAST(@thang AS VARCHAR(2)) + '
  AND dsCapNhatBangLuong.Nam = ' + CAST(@nam AS VARCHAR(4)) + '
  And bangLuong.' + @maPhuCap + ' <> 0
GROUP BY canBo.Ma_CB, canBo.Parent
ORDER BY canBo.Parent, canBo.Ma_CB'
execute(@query)
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bang_tonghop_luong_phucap_bienphong_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_bang_tonghop_luong_phucap_bienphong_nq104]
@maDonVi NVARCHAR(max),
@thang int,
@nam int
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;

DECLARE @Cols AS NVARCHAR(MAX)
DECLARE @Query AS NVARCHAR(MAX)

SET @Cols = 'LHT_HS,PCCV_HS,LHT_TT,PCCV_TT,PCTNVK_TT,NTN,PCTNVK_HS,PCTRA_SUM,PCDACTHU_SUM,PCDACBIET_TT,PCANQP_TT,PCKV_TT,PC8_TT,PCCOV_TT,PCTHUHUT_TT,PCBVBG_TT,PCLAUNAMBG_TT,PCKHAC_SUM,BHCN_TT,THUETNCN_TT,TA_TT,PHAITRUKHAC_SUM,LUONGTHANG_SUM,PHAITRU_SUM,THANHTIEN'
SET @Query =
'
WITH BangLuongThang AS (
SELECT
dsCapNhapBangLuong.Thang AS Thang,
dsCapNhapBangLuong.Nam AS Nam,
bangLuong.ma_can_bo AS MaCanBo,
bangLuong.ma_phu_cap AS MaPhuCap,
bangLuong.GIA_TRI AS GiaTri
FROM TL_BangLuong_Thang_Bridge_NQ104 bangLuong
JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhapBangLuong
ON bangLuong.parent = dsCapNhapBangLuong.Id
WHERE
--bangLuong.Ma_PhuCap IN (SELECT * FROM f_split(''' + @Cols + ''')) AND
    dsCapNhapBangLuong.Ma_CachTL=''CACH0''
AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
AND dsCapNhapBangLuong.Status=1
), ThongTinCanBo AS (
SELECT
donVi.Ma_DonVi AS MaDonVi,
donVi.Ten_Donvi AS TenDonvi,
canBo.Ma_CanBo AS MaCanBo,
canBo.Ten_CanBo AS TenCanBo,
dbo.f_split_empty(canbo.Ten_CanBo) AS Ten,
canBo.Thang_TNN AS Tnn,
ISNULL(canBo.Ma_CB104, ''0'') AS MaCapBac,
ISNULL(capBac.Ten_Cb, ''0'') AS CapBac,
--ISNULL(chucVu.HeSo_Cv, 0) AS HSChucVu,
chucVu.ma AS MaChucVu,
chucVu.ten AS TenChucVu,
CONCAT(canBo.So_TaiKhoan, '' '', canBo.Ma_CanBo) AS Stk,
ISNULL(FORMAT(canBo.Ngay_NN,''MM/yy''), '''') AS NgayNhapNgu,
ISNULL(FORMAT(canBo.Ngay_XN,''MM/yy''), '''') AS NgayXuatNgu,
ISNULL(FORMAT(canBo.Ngay_TN,''MM/yy''), '''') AS NgayTaiNgu,
canBo.Ngay_NN AS NgayNhapNguDate,
canBo.Ngay_XN AS NgayXuatNguDate,
canBo.Ngay_TN AS NgayTaiNguDate,
CASE WHEN canBo.Thang_TNN IS NULL THEN 0 ELSE canBo.Thang_TNN END AS ThangTnn,
capBac.xau_noi_ma XauNoiMa
FROM TL_DM_CanBo_NQ104 canBo
INNER JOIN TL_DM_DonVi_NQ104 donVi
ON canBo.Parent=donVi.Ma_DonVi
LEFT JOIN TL_DM_CapBac_NQ104 capBac
ON canBo.Ma_CB104=capBac.Ma_Cb AND capBac.nam=' + CAST(@Nam AS VARCHAR(4)) + '
LEFT JOIN TL_DM_ChucVu_NQ104 chucVu
ON canBo.ma_cvd104=chucVu.ma AND chucVu.nam=' + CAST(@Nam AS VARCHAR(4)) + '
WHERE
canBo.IsDelete = 1
AND canBo.Khong_Luong = 0
AND canBo.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
AND canBo.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
)
SELECT ROW_NUMBER() over(order by MaCapBac DESC, Ten ASC) as stt, MaDonVi, MaCanBo, TenCanBo, Ten, Tnn, MaCapBac, CapBac, MaChucVu, TenChucVu, Stk, NgayNhapNgu, NgayXuatNgu, NgayTaiNgu, ThangTnn, XauNoiMa, ' + @Cols + ' FROM (
SELECT
bangLuong.Thang AS Thang,
bangLuong.Nam AS Nam,
canBo.MaDonVi,
canBo.TenDonVi,
canBo.MaCanBo,
canBo.TenCanBo,
canBo.Ten,
canBo.MaCapBac,
canBo.CapBac,
--canBo.HSChucVu,
canBo.MaChucVu,
canBo.TenChucVu,
canBo.Stk,
canBo.NgayNhapNgu,
canBo.NgayXuatNgu,
canBo.NgayTaiNgu,
canBo.NgayNhapNguDate,
canBo.NgayXuatNguDate,
canBo.NgayTaiNguDate,
canBo.ThangTnn,
canBo.Tnn,
CASE WHEN bangLuong.MaPhuCap = ' + '''NTN''' + ' THEN dbo.f_luong_ntn(canBo.NgayNhapNguDate, canBo.NgayXuatNguDate, canBo.NgayTaiNguDate, canBo.ThangTnn, 6, 2022) ELSE bangLuong.GiaTri END AS GiaTri,
bangLuong.MaPhuCap,
canBo.XauNoiMa
FROM BangLuongThang bangLuong
INNER JOIN ThongTinCanBo canBo
ON bangLuong.MaCanBo = canBo.MaCanBo
) x
PIVOT
(
SUM(GiaTri)
FOR MaPhuCap IN (' + @Cols + ')
) pvt
--WHERE MaCapBac LIKE ''0%''
ORDER BY MaCapBac DESC, Ten ASC'
execute(@Query)
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_thang_chitiet_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_bangluong_thang_chitiet_nq104]
	@Id UNIQUEIDENTIFIER
AS
DECLARE @cols AS NVARCHAR(MAX);
DECLARE @query  AS NVARCHAR(MAX);;

SET @cols = STUFF(
  (
    SELECT
      DISTINCT ',' + QUOTENAME(phucap.Ma_PhuCap) 
    FROM TL_DM_PhuCap phucap
        FOR XML PATH(''), TYPE
  ).value('.', 'NVARCHAR(MAX)'),1,1,'')
  
SET @query = '
	WITH ThongTinCanBo AS (
		SELECT
			canBo.Ma_CanBo	AS MaCanBo,
			canBo.Ten_CanBo	AS TenCanBo,
			canBo.IsDelete AS IsDelete,
			canbo.Ngay_XN,
			canbo.Ma_TangGiam,
			donVi.Ma_DonVi	AS MaDonVi,
			donVi.Ten_DonVi	AS TenDonVi
		FROM TL_DM_CanBo_NQ104 canBo
		INNER JOIN TL_DM_DonVi_NQ104 donVi
			ON canBo.Parent = donVi.Ma_DonVi
		WHERE canBo.Khong_Luong = 0
	)
	SELECT Thang, Nam, MaCanBo, TenCanBo, TenDonVi, ' + @cols + ' FROM (
		SELECT
			bangLuongThang.THANG		AS Thang,
			bangLuongThang.NAM			AS Nam,
			canBo.MaCanBo				AS MaCanBo,
			canBo.TenCanBo				AS TenCanBo,
			canBo.TenDonVi				AS TenDonVi,
			bridge.gia_tri GiaTri,
			bridge.ma_phu_cap MaPhuCap
		FROM TL_BangLuong_Thang_NQ104 bangLuongThang
		LEFT JOIN TL_BangLuong_Thang_Bridge_NQ104 bridge ON bangLuongThang.Ma_CBo = bridge.ma_can_bo
		INNER JOIN ThongTinCanBo canBo
			ON bangLuongThang.Ma_CBo = canBo.MaCanBo
		WHERE
			(canBo.IsDelete = 1 or (canbo.Ma_TangGiam = ''320'' and month(canbo.Ngay_XN) = bangLuongThang.THANG and year(canbo.Ngay_XN) = bangLuongThang.NAM))
			and bangLuongThang.parent = + ''' + CONVERT(NVARCHAR(100), @Id) + '''
	) x
	PIVOT 
	(
		SUM(GiaTri)
		FOR MaPhuCap IN (' + @cols + ')
	) p '
execute(@query)
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_thang_dulieu_insert_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_bangluong_thang_dulieu_insert_nq104]
@Thang int,
@Nam int,
@MaDonVi NVARCHAR(MAX),
@MaCachTl NVARCHAR(50),
@SoNgay int
AS
BEGIN
	SET NOCOUNT ON;

	select canbo.ma_can_bo MA_CBO, canbo.gia_tri, canbo.ngay_huong_phu_cap HuongPC_SN, canbo.ma_phu_cap ma_phucap, phucap.is_theo_cong_chuan IsTinhTheoSoCongChuan
	into #canBoPhuCap
	from TL_CanBo_PhuCap_Bridge_NQ104 canbo
	left join TL_DM_PhuCap_NQ104 phucap on canbo.ma_phu_cap = phucap.Ma_PhuCap
	where canbo.ma_can_bo like CONCAT(FORMAT(@Nam, 'D2'), FORMAT(@Thang, 'D2'), '%')

	SELECT
	MA_CBO MaCanBo,
	(CASE WHEN pc.Parent = 'TIENAN' THEN 'TA_TT'
	WHEN pc.Parent = 'TIENAN2' THEN 'TA_TT2' ELSE '' END) AS PARENT,
	SUM (
		CASE WHEN PARENT <> N'TIENAN' THEN 0
		WHEN ISNULL(canBoPhuCap.HuongPC_SN, 0) <> 0 AND bHuongPc_Sn = 1 THEN ISNULL(canBoPhuCap.HuongPC_SN, 0) * canBoPhuCap.GIA_TRI
		ELSE ISNULL(@SoNgay, 0) * canBoPhuCap.GIA_TRI
		END
	) GiaTriTA1,
	SUM (
		CASE WHEN PARENT <> N'TIENAN2' THEN 0
		WHEN ISNULL(canBoPhuCap.HuongPC_SN, 0) <> 0 AND bHuongPc_Sn = 1 THEN ISNULL(canBoPhuCap.HuongPC_SN, 0) * canBoPhuCap.GIA_TRI
		ELSE ISNULL(@SoNgay, 0) * canBoPhuCap.GIA_TRI
		END
	) GiaTriTA2

	INTO #SoLieuTienAn
	FROM #canBoPhuCap canBoPhuCap
	INNER JOIN TL_DM_PhuCap_NQ104 as pc on canBoPhuCap.MA_PHUCAP = pc.Ma_PhuCap
	WHERE
	pc.PARENT IN ('TIENAN', 'TIENAN2') and canBoPhuCap.Gia_Tri <> 0 and canBoPhuCap.MA_CBO like CONCAT(FORMAT(@Nam, 'D2'), FORMAT(@Thang, 'D2'), '%')
	GROUP BY canBoPhuCap.MA_CBO, pc.PARENT


	SELECT
	canBo.Ma_CanBo AS MaCanBo,
	canBo.Ten_CanBo AS TenCanBo,
	donVi.Ma_DonVi MaDonVi,
	canBo.Ma_Hieu_CanBo AS MaHieuCanBo,
	capBac.ma_cb MaCapBac,
	canBo.BHTN,
	canBo.PCCV,
	canBo.BNuocNgoai,
	canBo.Ngay_XN as NgayXn
	INTO #ThongTinCanBo
	FROM TL_DM_CanBo_NQ104 canBo
	INNER JOIN TL_DM_DonVi_NQ104 donVi
	ON canBo.Parent = donVi.Ma_DonVi
	INNER JOIN TL_DM_CapBac_NQ104 capBac
	ON canBo.ma_cb104 = capBac.Ma_Cb
	WHERE
	canBo.Thang = @Thang
	AND canBo.Nam = @Nam
	AND canBo.Parent IN (SELECT * FROM f_split(@MaDonVi))
	AND (canBo.IsDelete = 1 or (canbo.Ma_TangGiam = '320' and month(canbo.Ngay_XN) <= @thang and year(canbo.Ngay_XN) = @Nam))
	AND canBo.Khong_Luong <> 1


SELECT
	newid() AS Id,
	@Thang AS Thang,
	@Nam AS Nam,
	canBo.MaCanBo AS MaCbo,
	canBo.TenCanBo AS TenCbo,
	canBo.MaDonVi AS MaDonVi,
	canBo.BNuocNgoai ,
	@MaCachTl AS MaCachTl,
	canBoPhuCap.MA_PHUCAP AS MaPhuCap,
	CASE
	WHEN canBoPhuCap.MA_PHUCAP = 'TCVIECLAM_TT' AND cb.Parent in ('1', '2', '3') THEN 0
	WHEN canBoPhuCap.MA_PHUCAP = 'THANG_TCVIECLAM' AND canbo.NgayXn is null THEN 0
	WHEN canBoPhuCap.MA_PHUCAP = 'THANG_TCVIECLAM' and canbo.NgayXn is not null and year(canbo.NgayXn) <> @NAM THEN 0
	WHEN canBoPhuCap.MA_PHUCAP = 'THANG_TCVIECLAM' and canbo.NgayXn is not null and year(canbo.NgayXn) = @NAM and month(canbo.NgayXn) <> @Thang THEN 0
	WHEN canBoPhuCap.MA_PHUCAP = 'THANG_TCXN' AND canbo.NgayXn is null THEN 0
	WHEN canBoPhuCap.MA_PHUCAP = 'THANG_TCXN' and canbo.NgayXn is not null and year(canbo.NgayXn) <> @NAM THEN 0
	WHEN canBoPhuCap.MA_PHUCAP = 'THANG_TCXN' and canbo.NgayXn is not null and year(canbo.NgayXn) = @NAM and month(canbo.NgayXn) <> @Thang THEN 0
	WHEN (canBoPhuCap.MA_PHUCAP = 'NTN' AND canBoPhuCap.GIA_TRI < 5) OR (canBoPhuCap.MA_PHUCAP = 'BHTNCN_HS' AND canBo.BHTN = 0) OR (canBoPhuCap.MA_PHUCAP = 'PCCOV_HS' AND canBo.PCCV = 0) THEN 0
	WHEN canBoPhuCap.MA_PHUCAP = 'TA_TT' THEN soLieuTienAn.GiaTriTA1
	WHEN canBoPhuCap.MA_PHUCAP = 'TA_TT2' THEN soLieuTienAn.GiaTriTA2
	WHEN canBoPhuCap.MA_PHUCAP = 'TRICHLUONG_TT' THEN (canBoPhuCap.GIA_TRI / 1000) * 1000
	ELSE canBoPhuCap.GIA_TRI
	END AS GiaTri,
	canBo.MaHieuCanBo AS MaHieuCanBo,
	canBo.MaCapBac AS MaCb,
	canBoPhuCap.HuongPC_SN AS HuongPcSn,
	cachTinhLuong.CongThuc AS CongThuc,
	CASE WHEN cachTinhLuong.Id IS NULL THEN 1 ELSE 0 END AS IsCalculated,
	canBoPhuCap.IsTinhTheoSoCongChuan

FROM #canBoPhuCap canBoPhuCap
	INNER JOIN #ThongTinCanBo canBo
	ON canBo.MaCanBo = canBoPhuCap.MA_CBO
	LEFT JOIN #SoLieuTienAn soLieuTienAn
	ON canBoPhuCap.MA_CBO = soLieuTienAn.MaCanBo AND canBoPhuCap.MA_PHUCAP = soLieuTienAn.Parent
	LEFT JOIN (select Id, Ma_Cot, CongThuc from TL_DM_Cach_TinhLuong_Chuan_NQ104) cachTinhLuong
	ON cachTinhLuong.Ma_Cot = canBoPhuCap.MA_PHUCAP
	left join TL_DM_CapBac_NQ104 cb on canBo.MaCapBac = cb.ma_cb
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_TL_BangLuong_Thang_NQ104_chitiet_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_TL_BangLuong_Thang_NQ104_chitiet_nq104]
	@Id UNIQUEIDENTIFIER
AS
DECLARE @cols AS NVARCHAR(MAX);
DECLARE @query  AS NVARCHAR(MAX);;

SET @cols = STUFF(
  (
    SELECT
      DISTINCT ',' + QUOTENAME(phucap.Ma_PhuCap) 
    FROM TL_DM_PhuCap_NQ104 phucap
        FOR XML PATH(''), TYPE
  ).value('.', 'NVARCHAR(MAX)'),1,1,'')
  
SET @query = '
	WITH ThongTinCanBo AS (
		SELECT
			canBo.Ma_CanBo	AS MaCanBo,
			canBo.Ten_CanBo	AS TenCanBo,
			canBo.IsDelete AS IsDelete,
			canbo.Ngay_XN,
			canbo.Ma_TangGiam,
			donVi.Ma_DonVi	AS MaDonVi,
			donVi.Ten_DonVi	AS TenDonVi
		FROM TL_DM_CanBo canBo
		INNER JOIN TL_DM_DonVi_NQ104 donVi
			ON canBo.Parent = donVi.Ma_DonVi
		WHERE canBo.Khong_Luong = 0
	)
	SELECT Thang, Nam, MaCanBo, TenCanBo, TenDonVi, ' + @cols + ' FROM (
		SELECT
			bangLuongThang.THANG		AS Thang,
			bangLuongThang.NAM			AS Nam,
			canBo.MaCanBo				AS MaCanBo,
			canBo.TenCanBo				AS TenCanBo,
			canBo.TenDonVi				AS TenDonVi,
			bangLuongThang.Gia_Tri		AS GiaTri,
			bangLuongThang.Ma_PhuCap	AS MaPhuCap
		FROM TL_BangLuong_Thang_NQ104 bangLuongThang
		INNER JOIN ThongTinCanBo canBo
			ON bangLuongThang.Ma_CBo = canBo.MaCanBo
		WHERE
			(canBo.IsDelete = 1 or (canbo.Ma_TangGiam = ''320'' and month(canbo.Ngay_XN) = bangLuongThang.THANG and year(canbo.Ngay_XN) = bangLuongThang.NAM))
			and bangLuongThang.parent = + ''' + CONVERT(NVARCHAR(100), @Id) + '''
	) x
	PIVOT 
	(
		SUM(GiaTri)
		FOR MaPhuCap IN (' + @cols + ')
	) p '
execute(@query)
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_TL_BangLuong_Thang_NQ104_dulieu_insert_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TungNH
-- Create date: 21/04/2022
-- Description:	Lấy dữ liệu cho thêm mới bảng lương tháng
-- =============================================
CREATE PROCEDURE [dbo].[sp_TL_BangLuong_Thang_NQ104_dulieu_insert_nq104] 
	@Thang int, 
	@Nam int,
	@MaDonVi NVARCHAR(MAX),
	@MaCachTl NVARCHAR(50),
	@SoNgay int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    WITH SoLieuTienAn AS (
		SELECT
			MA_CBO MaCanBo,
			SUM (
				CASE
					WHEN MA_PHUCAP IN ('TA_TRUCQY_DG1', 'TA_TRUCQY_DG2', 'TA_TRUCQY_DG3', 'TA_TRUCQY_DG4', 'TA_DOCHAI_DG', 'TA_OM_DG') THEN ISNULL(HuongPC_SN, 0) * GIA_TRI
					WHEN MA_PHUCAP IN ('TA_BB_DG', 'TA_TRUCTRAI_DG') THEN @SoNgay * GIA_TRI 
					ELSE 0
				END
			) GiaTri
		FROM TL_CanBo_PhuCap_NQ104 canBoPhuCap
		WHERE
			canBoPhuCap.MA_PHUCAP IN ('TA_TRUCQY_DG1', 'TA_TRUCQY_DG2', 'TA_TRUCQY_DG3', 'TA_TRUCQY_DG4', 'TA_DOCHAI_DG', 'TA_OM_DG', 'TA_BB_DG', 'TA_TRUCTRAI_DG')
		GROUP BY canBoPhuCap.MA_CBO
	), ThongTinCanBo AS (
		SELECT
			canBo.Ma_CanBo AS MaCanBo,
			canBo.Ten_CanBo AS TenCanBo,
			donVi.Ma_DonVi MaDonVi,
			canBo.Ma_Hieu_CanBo AS MaHieuCanBo,
			capBac.Ma_CB MaCapBac,
			canBo.BHTN,
			canBo.PCCV
		FROM TL_DM_CanBo_NQ104 canBo
		INNER JOIN TL_DM_DonVi_NQ104 donVi
			ON canBo.Parent = donVi.Ma_DonVi
		INNER  JOIN TL_DM_CapBac_NQ104 capBac
			ON canBo.Ma_CB104 = capBac.Ma_Cb
		WHERE
			canBo.Thang = @Thang
			AND canBo.Nam = @Nam
			AND canBo.Parent IN (SELECT * FROM f_split(@MaDonVi))
	)

	SELECT
		newid()					AS Id,
		@Thang					AS Thang,
		@Nam					AS Nam,
		canBo.MaCanBo			AS MaCbo,
		canBo.TenCanBo			AS TenCbo,
		canBo.MaDonVi			AS MaDonVi,
		@MaCachTl				AS MaCachTl,
		canBoPhuCap.MA_PHUCAP	AS MaPhuCap,
		CASE
			WHEN (canBoPhuCap.MA_PHUCAP = 'TCVIECLAM_TT' AND cb.Parent in (1, 2, 4)) THEN 0 
			WHEN (canBoPhuCap.MA_PHUCAP = 'NTN' AND canBoPhuCap.GIA_TRI < 5) OR (canBoPhuCap.MA_PHUCAP = 'BHTNCN_HS' AND canBo.BHTN = 0) OR (canBoPhuCap.MA_PHUCAP = 'PCCOV_HS' AND canBo.PCCV = 0) THEN 0
			WHEN canBoPhuCap.MA_PHUCAP = 'TA_TT' THEN soLieuTienAn.GiaTri
			WHEN canBoPhuCap.MA_PHUCAP = 'TRICHLUONG_TT' THEN (canBoPhuCap.GIA_TRI / 1000) * 1000
			ELSE canBoPhuCap.GIA_TRI
		END						AS GiaTri,
		canBo.MaHieuCanBo		AS MaHieuCanBo,
		canBo.MaCapBac			AS MaCb,
		canBoPhuCap.HuongPC_SN	AS HuongPcSn,
		cachTinhLuong.CongThuc	AS CongThuc,
		CASE WHEN cachTinhLuong.Id IS NULL THEN 1 ELSE 0 END AS IsCalculated
	FROM TL_CanBo_PhuCap_NQ104 canBoPhuCap
	INNER JOIN ThongTinCanBo canBo
		ON canBo.MaCanBo = canBoPhuCap.MA_CBO
	LEFT JOIN SoLieuTienAn soLieuTienAn
		ON canBoPhuCap.MA_CBO = soLieuTienAn.MaCanBo
	LEFT JOIN TL_DM_Cach_TinhLuong_Chuan_NQ104 cachTinhLuong
		ON cachTinhLuong.Ma_Cot = canBoPhuCap.MA_PHUCAP
	left join TL_DM_CapBac_NQ104 cb on canBo.MaCapBac = cb.Ma_Cb
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_truylinh_dulieu_insert_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_bangluong_truylinh_dulieu_insert_nq104] 
	@Thang int, 
	@Nam int,
	@MaDonVi NVARCHAR(MAX),
	@MaCachTl NVARCHAR(50),
	@SoNgay int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;


	select canbo.ma_can_bo MA_CBO, canbo.gia_tri, canbo.ngay_huong_phu_cap HuongPC_SN, canbo.ma_phu_cap ma_phucap, phucap.is_theo_cong_chuan IsTinhTheoSoCongChuan
	into #canBoPhuCap
	from TL_CanBo_PhuCap_Bridge_NQ104 canbo
	left join TL_DM_PhuCap_NQ104 phucap on canbo.ma_phu_cap = phucap.Ma_PhuCap
	where ma_can_bo like CONCAT(FORMAT(@Nam, 'D2'), FORMAT(@Thang, 'D2'), '%')
	--select MA_CBO, gia_tri, HuongPC_SN, ma_phucap
	--into #canBoPhuCap
	--from TL_CanBo_PhuCap_NQ104 where MA_CBO like CONCAT(FORMAT(@Nam, 'D2'), FORMAT(@Thang, 'D2'), '%')

	SELECT
		canBo.Ma_CanBo AS MaCanBo,
		canBo.Ten_CanBo AS TenCanBo,
		donVi.Ma_DonVi MaDonVi,
		canBo.Ma_Hieu_CanBo AS MaHieuCanBo,
		capBac.Ma_Cb MaCapBac,
		canBo.BHTN,
		canBo.PCCV,
		canBo.BNuocNgoai
	INTO #ThongTinCanBo
	FROM TL_DM_CanBo_NQ104 canBo
	INNER JOIN TL_DM_DonVi_NQ104 donVi
		ON canBo.Parent = donVi.Ma_DonVi
	INNER  JOIN TL_DM_CapBac_NQ104 capBac
		ON canBo.ma_cb104 = capBac.ma_cb
	WHERE
		canBo.Thang = @Thang
		AND canBo.Nam = @Nam
		AND canBo.Ma_CanBo IN (SELECT MA_CBO FROM #canBoPhuCap WHERE MA_PHUCAP LIKE '%TTL%' AND GIA_TRI > 0 GROUP BY MA_CBO)
		AND donVi.Ma_DonVi IN (SELECT * FROM f_split(@MaDonVi))

	SELECT Ma_Cot, pc.Ma_PhuCap into #tmp from (
		SELECT Ma_Cot, CONCAT('|', REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(CAST(CongThuc as nvarchar(max)),'+','|'),'-','|'),'*','|'),'/','|'),'(','|'),')','|'), '|') as CongThuc FROM TL_DM_Cach_TinhLuong_TruyLinh_NQ104 WHERE CongThuc IS NOT NULL) as tbl
		INNER  JOIN TL_DM_PhuCap_NQ104 as pc on tbl.CongThuc LIKE CONCAT('%|', pc.Ma_PhuCap, '|%') 

	select * into #tmpPhuCapLuongTruyLinh
	FROM (
		select ma_cot as Ma_PC from #tmp
		union 
		select ma_phucap as Ma_PC from #tmp
	) AS c

	SELECT
		@Thang					AS Thang,
		@Nam					AS Nam,
		canBo.MaCanBo			AS MaCbo,
		canBo.TenCanBo			AS TenCbo,
		canBo.MaDonVi			AS MaDonVi,
		@MaCachTl				AS MaCachTl,
		canBoPhuCap.MA_PHUCAP	AS MaPhuCap,
		canBo.BNuocNgoai			,
		CASE
			WHEN (canBoPhuCap.MA_PHUCAP = 'NTN' AND canBoPhuCap.GIA_TRI < 5) OR (canBoPhuCap.MA_PHUCAP = 'BHTNCN_HS' AND canBo.BHTN = 0) OR (canBoPhuCap.MA_PHUCAP = 'PCCOV_HS' AND canBo.PCCV = 0) THEN 0
			ELSE canBoPhuCap.GIA_TRI
		END						AS GiaTri,
		canBo.MaHieuCanBo		AS MaHieuCanBo,
		canBo.MaCapBac			AS MaCb,
		canBoPhuCap.HuongPC_SN	AS HuongPcSn,
		cachTinhLuong.CongThuc	AS CongThuc,
		CASE WHEN cachTinhLuong.Id IS NULL THEN 1 ELSE 0 END AS IsCalculated,
		canBoPhuCap.IsTinhTheoSoCongChuan

	FROM #canBoPhuCap canBoPhuCap
	INNER JOIN #tmpPhuCapLuongTruyLinh phuCapLuongTruyLinh 
		ON phuCapLuongTruyLinh.Ma_PC = canBoPhuCap.MA_PHUCAP
	INNER JOIN #ThongTinCanBo canBo
		ON canBo.MaCanBo = canBoPhuCap.MA_CBO
	LEFT JOIN TL_DM_Cach_TinhLuong_TruyLinh_NQ104 cachTinhLuong
		ON cachTinhLuong.Ma_Cot = canBoPhuCap.MA_PHUCAP
	DROP TABLE #ThongTinCanBo
	DROP TABLE #tmp
	DROP TABLE #tmpPhuCapLuongTruyLinh
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bangthanhtoan_truylinhchuyenchedoqncn_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_bangthanhtoan_truylinhchuyenchedoqncn_nq104]
	@Thang int,
	@Nam int,
	@MaDonVi nvarchar(max),
	@MaCanBo nvarchar(max)
as
begin
	SET NOCOUNT ON;

    DECLARE @Cols AS NVARCHAR(MAX)
	DECLARE @Query AS NVARCHAR(MAX)

	SET @Cols = STUFF(
	(
    SELECT
		DISTINCT ',' + QUOTENAME(phucap.Ma_PhuCap) 
    FROM TL_DM_PhuCap_NQ104 phucap
        FOR XML PATH(''), TYPE
	).value('.', 'NVARCHAR(MAX)'),1,1,'')

	SET @Query =
	'SELECT ' + @Cols  +'  FROM (
		SELECT
			bangLuong.Thang AS Thang,
			bangLuong.Nam AS Nam, 
			donVi.Ten_Donvi TenDonVi,
			canBo.Ma_CanBo MaCanBo,
			canBo.Ten_CanBo AS TenCanBo,
			ISNULL(canBo.Ma_CB, ''0'') AS MaCapBac,
			ISNULL(capBac.Ten_Cb, ''0'') AS CapBac,
			ISNULL(chucVu.HeSo_Cv, 0) AS HSChucVu,
			chucVu.Ma_Cv AS MaChucVu,
			chucVu.Ten_Cv AS TenChucVu,
			canBo.So_TaiKhoan AS Stk,
			ISNULL(FORMAT(canBo.Ngay_NN,''MM/yy''), '''') AS NgayNhapNgu,
			ISNULL(FORMAT(canBo.Ngay_XN,''MM/yy''), '''') AS NgayXuatNgu,
			ISNULL(FORMAT(canBo.Ngay_TN,''MM/yy''), '''') AS NgayTaiNgu,
			bangLuong.GIA_TRI AS GiaTri,
			bangLuong.MA_PHUCAP AS MaPhuCap
		FROM TL_BangLuong_Thang_NQ104 bangLuong
		JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhapBangLuong ON bangLuong.parent=dsCapNhapBangLuong.Id
		JOIN TL_DM_CanBo_NQ104 canBo ON bangLuong.Ma_CBo=canBo.Ma_CanBo
		LEFT JOIN TL_DM_CapBac_NQ104 capBac ON canBo.Ma_CB104=capBac.Ma_Cb
		LEFT JOIN TL_DM_ChucVu chucVu ON canBo.Ma_CV=chucVu.Ma_Cv
		JOIN TL_DM_DonVi_NQ104 donVi ON bangLuong.Ma_DonVi=donVi.Ma_DonVi
		WHERE
			dsCapNhapBangLuong.Ma_CachTL=''CACH0''
			AND dsCapNhapBangLuong.Ma_CBo IN (''' + @MaDonVi + ''')
			AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND dsCapNhapBangLuong.Status=1
			AND canBo.IsDelete=1
			AND canBo.Khong_Luong=0
			AND bangLuong.Ma_CBo in (''' + @MaCanBo + ''')
	) x
	PIVOT
	(
		SUM(GiaTri)
		FOR MaPhuCap IN (' + @Cols + ')
	) pvt ORDER BY HSChucVu DESC, MaCapBac DESC, TenCanBo DESC'
	execute(@Query)
end;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_baocao_luong_nam_ke_hoach_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_baocao_luong_nam_ke_hoach_nq104] @spMaDonVi varchar(100), @spNam int
AS
DECLARE @cols AS NVARCHAR(MAX);
DECLARE  @query  AS NVARCHAR(MAX);
DECLARE @maDonVi AS varchar(100);
DECLARE @nam AS NVARCHAR(MAX);

SET @maDonVi = @spMaDonVi
SET @nam = @spNam

SET @cols = STUFF(
  (
    SELECT
      distinct ',' + QUOTENAME(phucap.Ma_PhuCap) 
    FROM TL_DM_PhuCap_NQ104 phucap
        FOR XML PATH(''), TYPE
  ).value('.', 'NVARCHAR(MAX)'),1,1,'')
  
SET @query = 'SELECT Thang, Nam, Ten_DonVi, ' + @cols + ' from 
            (
                select TL_BangLuong_KeHoach.Thang, TL_BangLuong_KeHoach.Nam, TL_DM_DonVi_NQ104.Ten_Donvi, MA_PHUCAP, SUM(Gia_Tri) as Gia_Tri
				from [dbo].[TL_BangLuong_KeHoach]
				join [dbo].[TL_DM_CanBo_NQ104_KeHoach]
				ON TL_BangLuong_KeHoach.Ma_CanBo = TL_DM_CanBo_NQ104_KeHoach.Ma_CanBo
				join [dbo].[TL_DM_DonVi_NQ104]
				ON TL_BangLuong_KeHoach.Ma_DonVi = [dbo].[TL_DM_DonVi_NQ104].Ma_DonVi
				where Ma_CachTL = ''CACH0''
				And TL_BangLuong_KeHoach.Ma_DonVi = ' + @maDonVi + '
				And TL_BangLuong_KeHoach.Nam = ' + @nam + ' ' + '
				Group By Ma_PhuCap, TL_BangLuong_KeHoach.Thang, TL_BangLuong_KeHoach.Nam, TL_DM_DonVi_NQ104.Ten_Donvi, MA_PHUCAP
           ) x
            pivot 
            (
                 SUM(Gia_Tri)
                for MA_PHUCAP in (' + @cols + ')
            ) p '
execute(@query)
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_baocao_phantich_tienan_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_tl_baocao_phantich_tienan_nq104]
	@MaCanBo nvarchar(max),
	@MaPhuCap nvarchar(max)
as
begin
select CanBoPhuCap.MA_CBO as MaCanBo, 
		CanBoPhuCap.MA_PHUCAP as MaPhuCap, 
		CanBoPhuCap.GIA_TRI as GiaTri, 
		CanBo.Parent as MaDonVi,
		CanBoPhuCap.HuongPC_SN as SoNgayHuong,
		tlDmPhuCap.Ten_PhuCap as TenPhuCap
	from TL_CanBo_PhuCap_NQ104 CanBoPhuCap
	inner join TL_DM_CanBo_NQ104 CanBo on CanBoPhuCap.MA_CBO = CanBo.Ma_CanBo
	inner join TL_DM_PhuCap_NQ104 tlDmPhuCap on tlDmPhuCap.Ma_PhuCap = CanBoPhuCap.MA_PHUCAP
	where MA_CBO IN (SELECT * FROM f_split(@MaCanBo)) and 
	CanBoPhuCap.MA_PHUCAP IN (SELECT * FROM f_split(@MaPhuCap)) and 
	CanBoPhuCap.GIA_TRI is not null and CanBoPhuCap.GIA_TRI != 0
end;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bhxh_export_can_bo_che_do_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_bhxh_export_can_bo_che_do_nq104]
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
		left join TL_DM_CanBo_NQ104 canbo on cbcd.sMaCanBo = canbo.Ma_CanBo
		join (
			select canbo.Ma_CanBo,
				capbac.Ma_Cb,
				capbac.Note Ten_Cb
			from TL_DM_CanBo_NQ104 canbo
			join TL_DM_CapBac_NQ104 capbac
			on canbo.Ma_CB104 = capbac.Ma_Cb
		) cb on cbcd.sMaCanBo = cb.Ma_CanBo
		left join TL_DM_CheDoBHXH chedo
			on cbcd.sMaCheDo = chedo.sMaCheDo
	where cbcd.iNam = @YearOfWork
			and cbcd.iThang in (SELECT * FROM f_split(@Months))
	order by canbo.Ma_Hieu_Canbo desc

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_TL_CanBo_PhuCap_NQ104_saochep_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TungNH
-- Create date: 04/05/2022
-- Description:	Sao chép cán bộ sang tháng mới
-- =============================================
CREATE PROCEDURE [dbo].[sp_TL_CanBo_PhuCap_NQ104_saochep_nq104]
	@MaCanBo NVARCHAR(MAX),
	@FromYear int,
	@FromMonth int,
	@ToYear int,
	@ToMonth int,
	@IsCopyValue bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	WITH DsCanBo AS (
		SELECT
			Ma_CanBo,
			Ngay_NN,
			Ngay_XN,
			Ngay_TN,
			Thang_TNN,
			Ma_Hieu_CanBo
		FROM TL_DM_CanBo_NQ104
		WHERE
			Ma_CanBo IN (SELECT * FROM f_split(@MaCanBo))
	), HsPhuCapTruyLinh AS (
		SELECT
			cboPhuCap.MA_CBO,
			cboPhuCap.MA_PHUCAP + '_CU' AS MA_PHUCAP,
			cboPhuCap.GIA_TRI
		FROM TL_CanBo_PhuCap_NQ104 cboPhuCap
		INNER JOIN DsCanBo cbo ON cboPhuCap.MA_CBO = cbo.Ma_CanBo
		WHERE cboPhuCap.MA_PHUCAP IN ('LHT_HS', 'PCCV_HS', 'PCTHUHUT_HS', 'PCCOV_HS', 'PCCU_HS')
	)

	SELECT
		NEWID()					AS Id,
		FORMAT(@ToYear, 'D4') + FORMAT(@ToMonth, 'D2') + cbo.Ma_Hieu_CanBo	AS MaCbo,
		cboPhuCap.MA_PHUCAP		AS MaPhuCap,
		CASE
			WHEN cboPhuCap.MA_PHUCAP IN ('LCS', 'GTNN', 'GTPT_DG', 'TA_DG') THEN phuCap.Gia_Tri 
			WHEN cboPhuCap.MA_PHUCAP IN ('LHT_HS_CU', 'PCCV_HS_CU', 'PCTHUHUT_HS_CU', 'PCCOV_HS_CU', 'PCCU_HS_CU') THEN phuCapTruyLinh.GIA_TRI
			WHEN cboPhuCap.MA_PHUCAP = 'TTL' THEN 0
			WHEN cboPhuCap.ISoThang_Huong IS NOT NULL AND phuCap.IThang_ToiDa IS NOT NULL AND cboPhuCap.ISoThang_Huong >= phuCap.IThang_ToiDa THEN 0
			WHEN cboPhuCap.MA_PHUCAP = 'NTN' THEN (select dbo.f_luong_ntn(cbo.Ngay_NN, cbo.Ngay_XN, cbo.Ngay_TN, cbo.Thang_TNN, @ToMonth, @ToYear))
			WHEN ISNULL(phuCap.bSaoChep, 0) = 0 OR (@IsCopyValue = 1 AND phuCap.bSaoChep = 1) THEN cboPhuCap.GIA_TRI ELSE 0
			--cboPhuCap.bSaoChep IS NOT NULL AND (@IsCopyValue = 0 OR cboPhuCap.bSaoChep = 0) THEN 0 ELSE cboPhuCap.GIA_TRI
		END						AS GiaTri,
		cboPhuCap.HE_SO			AS HeSo,
		cboPhuCap.MA_KMCP		AS MaKmcp,
		cboPhuCap.CONG_THUC		AS CongThuc,
		cboPhuCap.PHANTRAM_CT	AS PhanTramCt,
		cboPhuCap.CHON			AS Chon,
		CASE 
		WHEN cboPhuCap.MA_PHUCAP IN ('LHT_HS') then null ELSE cboPhuCap.HuongPC_SN
		END	                    AS HuongPcSn,
		0						AS Flag,
		cboPhuCap.DateStart		AS DateStart,
		cboPhuCap.DateEnd		AS DateEnd,
		CASE
			WHEN cboPhuCap.ISoThang_Huong IS NOT NULL AND (phuCap.IThang_ToiDa IS NULL OR cboPhuCap.ISoThang_Huong < phuCap.IThang_ToiDa) THEN cboPhuCap.ISoThang_Huong + 1
			ELSE cboPhuCap.ISoThang_Huong
		END						AS ISoThang_Huong,
		cboPhuCap.bSaoChep		AS BSaoChep
	FROM TL_CanBo_PhuCap_NQ104 cboPhuCap
	INNER JOIN DsCanBo cbo ON cboPhuCap.MA_CBO = cbo.Ma_CanBo
	LEFT JOIN HsPhuCapTruyLinh phuCapTruyLinh ON cboPhuCap.MA_CBO = phuCapTruyLinh.MA_CBO AND cboPhuCap.MA_PHUCAP = phuCapTruyLinh.MA_PHUCAP
	LEFT JOIN TL_DM_PhuCap_NQ104 phuCap ON cboPhuCap.MA_PHUCAP = phuCap.Ma_PhuCap
END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_cb_phucap_inkiem_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_cb_phucap_inkiem_nq104]
@thangNam nvarchar(100),
@MaDonVi nvarchar(100)
as
begin
	select MA_CBO as 'MaCbo', MA_PHUCAP as 'MaPhuCap', GIA_TRI as 'GiaTri', flag as 'Flag' , Parent as 'MaDonVi'
	from TL_CanBo_PhuCap_NQ104 inner join TL_DM_CanBo_NQ104 on TL_CanBo_PhuCap_NQ104.MA_CBO = TL_DM_CanBo_NQ104.Ma_CanBo 
	where TL_DM_CanBo_NQ104.IsDelete = 1 and Ma_CanBo like @thangNam + '%' and Parent like @MaDonVi and flag = 1

end
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_chungtu_chitiet_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_chungtu_chitiet_nq104]
	@maDonVi VARCHAR(MAX),
	@thang INT,
	@nam INT,
	@maCachTl NVARCHAR(50)
AS
BEGIN
	
	--SELECT Ma_Cot, pc.Ma_PhuCap INTO #tmpMapping
	--FROM 
	--(SELECT Ma_Cot, CONCAT('|', REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(CAST(CongThuc as nvarchar(max)),'+','|'),'-','|'),'*','|'),'/','|'),'(','|'),')','|'), '|') as CongThuc FROM TL_DM_Cach_TinhLuong_Chuan_NQ104 WHERE CongThuc IS NOT NULL) as tbl
	--INNER  JOIN TL_DM_PhuCap_NQ104 as pc on tbl.CongThuc LIKE CONCAT('%|', pc.Ma_PhuCap, '|%') ;
	DECLARE @checkTongHop INT; 
	SELECT @checkTongHop = count(*) FROM f_split(@maCachTl);


	SELECT Ma_Cot, pc.Ma_PhuCap into #tmp from (
		SELECT Ma_Cot, CONCAT('|', REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(CAST(CongThuc as nvarchar(max)),'+','|'),'-','|'),'*','|'),'/','|'),'(','|'),')','|'), '|') as CongThuc FROM TL_DM_Cach_TinhLuong_Chuan_NQ104 WHERE CongThuc IS NOT NULL) as tbl
		INNER  JOIN TL_DM_PhuCap_NQ104 as pc on tbl.CongThuc LIKE CONCAT('%|', pc.Ma_PhuCap, '|%') or pc.Parent in ('TIENAN', 'TIENAN2')

	select * into #tmpMapping
	FROM (
		select Ma_Cot, Ma_Cot as Ma_PhuCap from #tmp
		union 
		select Ma_PhuCap as Ma_Cot, Ma_PhuCap as Ma_PhuCap from #tmp
	) AS c

	SELECT mp.Ma_Cot, cb.Ma_CanBo, SUM(ISNULL(pc.HuongPC_SN, 0)) as HuongPC_SN INTO #tmpSoNgay
	FROM TL_DM_CanBo_NQ104 as cb
	INNER JOIN TL_CanBo_PhuCap_NQ104 as pc on cb.Ma_CanBo = pc.MA_CBO
	INNER JOIN #tmpMapping as mp on pc.MA_PHUCAP = mp.Ma_PhuCap
	WHERE cb.Parent in (SELECT * FROM f_split(@MaDonVi))
	AND Thang = @thang
	AND Nam = @nam
	GROUP BY mp.Ma_Cot, cb.Ma_CanBo;

	SELECT
		dsCapNhapBangLuong.Ma_CBo	AS MaDonVi,
		dsCapNhapBangLuong.Thang	AS Thang,
		dsCapNhapBangLuong.Nam		AS Nam,
		bangLuong.MA_PHUCAP			AS MaPhuCap,
		bangLuong.Ma_CB				AS MaCapBac,
		case when bangLuong.Ma_CB in ('3.1', '3.2', '3.3', '3.4', '3.5') then bangLuong.Ma_CB else capBac.Parent end AS Ngach,
		--SUM(case when ISNULL(cbpc.HuongPC_SN, 0) = 0 then dbo.fnTotalDayOfMonth(@thang,@nam) else cbpc.HuongPC_SN end) AS SoNgay,
		SUM( 
			case WHEN pc.Parent IN ('TIENAN', 'TIENAN2') AND ISNULL(cbpc1.HuongPC_SN, 0) <> 0 AND bHuongPc_Sn = 1 then cbpc.HuongPC_SN
			WHEN pc.Parent IN ('TIENAN', 'TIENAN2') THEN dbo.fnTotalDayOfMonth(@thang,@nam) else ISNULL(cbpc.HuongPC_SN, 0) end
		) AS SoNgay,
		SUM(
			case WHEN pc.Parent IN ('TIENAN', 'TIENAN2') AND ISNULL(cbpc1.HuongPC_SN, 0) <> 0 AND bHuongPc_Sn = 1 THEN ISNULL(cbpc1.HuongPC_SN, 0) * cbpc1.GIA_TRI
			WHEN pc.Parent IN ('TIENAN', 'TIENAN2') THEN dbo.fnTotalDayOfMonth(@thang,@nam) * cbpc1.GIA_TRI
			ELSE bangLuong.Gia_Tri END
		) AS GiaTri,
		SUM(CASE WHEN @checkTongHop = 2 AND dsCapNhapBangLuong.Ma_CachTL ='CACH5' THEN 0 ELSE 1 END)	AS SoNguoi
	INTO #LuongCapBac
	FROM (SELECT * FROM TL_BangLuong_Thang_NQ104 WHERE NAM = @nam AND THANG = @thang AND Gia_Tri != 0) bangLuong
	INNER JOIN TL_CanBo_PhuCap_NQ104 as cbpc1 on bangLuong.Ma_CBo = cbpc1.MA_CBO AND bangLuong.Ma_PhuCap = cbpc1.MA_PHUCAP
	INNER JOIN TL_DM_PhuCap_NQ104 as pc on bangLuong.Ma_PhuCap = pc.Ma_PhuCap
	LEFT JOIN #tmpSoNgay  as cbpc on bangLuong.Ma_PhuCap = cbpc.Ma_Cot AND bangLuong.Ma_CBo = cbpc.Ma_CanBo
	LEFT JOIN 
	(SELECT * FROM TL_DS_CapNhap_BangLuong_NQ104 WHERE Status = 1 AND NAM = @Nam AND THANG = @Thang AND Ma_CachTL IN (SELECT * FROM f_split(@maCachTl)) AND Ma_CBo IN (SELECT * FROM f_split(@MaDonVi))) dsCapNhapBangLuong 
	ON bangLuong.parent = dsCapNhapBangLuong.Id AND bangLuong.THANG = dsCapNhapBangLuong.Thang and bangLuong.NAM = dsCapNhapBangLuong.Nam 
	LEFT JOIN TL_DM_CapBac_NQ104 capBac ON bangLuong.Ma_CB = capBac.Ma_Cb		
	GROUP BY dsCapNhapBangLuong.Ma_CBo, dsCapNhapBangLuong.Thang, dsCapNhapBangLuong.Nam, bangLuong.MA_PHUCAP,bangLuong.Ma_CB, capBac.Parent
		
	SELECT
		luongCapBac.MaDonVi,
		phuCapMlns.XauNoiMa,
		phuCapMlns.Ma_Cb,
		SoNguoi,
		SoNgay,
		GiaTri,
		luongCapBac.Thang
		INTO #LuongCapBacMlns
	FROM TL_PhuCap_MLNS_NQ104 phuCapMlns
	JOIN #LuongCapBac luongCapBac ON phuCapMlns.Ma_Cb = luongCapBac.Ngach AND phuCapMlns.Ma_PhuCap = luongCapBac.MaPhuCap
	WHERE
		phuCapMlns.Nam = @nam

	Select Ma_DonVi, XauNoiMa, SUM(DDuToan) DuToan
	INTO #DataDuToan
	From TL_QT_ChungTu_NQ104ChiTiet ctchitiet
	Join TL_QT_ChungTu_NQ104 chungtu
	on chungtu.ID = ctchitiet.Id_ChungTu
	Where Nam = @nam
	And Ma_DonVi IN (SELECT * FROM f_split(@maCachTl))
	Group By XauNoiMa, Ma_DonVi

	SELECT
		luong.MaDonVi					AS MaDonVi,
		NEWID()							AS Id,
		iID_MLNS						AS MlnsId, 
		iID_MLNS_Cha					AS MlnsIdParent, 
		sXauNoiMa						AS XauNoiMa, 
		sLNS							AS Lns, 
		sL								AS L, 
		sK								AS K, 
		sM								AS M, 
		sTM								AS TM, 
		sTTM							AS TTM, 
		sNG								AS Ng, 
		sTNG							AS TNG, 
		sTNG1							AS TNG1, 
		sTNG2							AS TNG2, 
		sTNG3							AS TNG3, 
		sMoTa							AS Mota, 
		iNamLamViec						AS NamLamViec,
		bHangCha						AS BHangCha,
		sChiTietToi						AS ChiTietToi,
		CONVERT(decimal, SUM(SoNguoi))	AS SoNguoi,
		SUM(ISNULL(SoNgay, 0))			AS SoNgay,
		SUM(GiaTri)						AS TongCong,
		SUM(GiaTri)						AS DieuChinh,
		@maCachTl						AS MaCachTl,
		ISNULL(dataDuToan.DuToan, 0)	AS DDuToan,
		luong.Ma_Cb						AS MaCb,
		luong.Thang as Thang
	FROM NS_MucLucNganSach mlns
	JOIN #LuongCapBacMlns luong
		ON mlns.sXauNoiMa = luong.XauNoiMa
	LEFT JOIN #DataDuToan dataDuToan
		ON luong.XauNoiMa = dataDuToan.XauNoiMa and luong.MaDonVi = dataDuToan.Ma_DonVi
	WHERE
		sLNS IN ('1', '101', '1010000')
		AND iNamLamViec = @nam
		AND luong.Thang = @thang
	GROUP BY MaDonVi, iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, iNamLamViec, sChiTietToi, bHangCha, DuToan, luong.Ma_Cb, luong.Thang
	ORDER BY MaDonVi, sXauNoiMa

	DROP TABLE #LuongCapBac
	DROP TABLE #DataDuToan
	DROP TABLE #LuongCapBacMlns
	DROP TABLE #tmpSoNgay
	DROP TABLE #tmpMapping
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_danhsach_chitra_nganhang_thunhapkhac_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_tl_danhsach_chitra_nganhang_thunhapkhac_nq104]
	@Thang int,
	@Nam int,
	@MaDonVi NVARCHAR(MAX),
	@IsOrderChucVu bit = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
IF @IsOrderChucVu = 1
	WITH BangLuongThang AS( 
		SELECT 
			BangLuongThang.ma_can_bo AS MaCanBo,
			Sum(BangLuongThang.Gia_Tri) AS GiaTri
		FROM TL_BangLuong_Thang_Bridge_NQ104 BangLuongThang INNER JOIN TL_DS_CapNhap_BangLuong_NQ104 BangLuong
			ON BangLuongThang.parent = BangLuong.Id
		WHERE
			BangLuong.Ma_CachTL = 'CACH0'
			AND BangLuongThang.ma_phu_cap IN (SELECT TL_DM_PhuCap_NQ104.Ma_PhuCap FROM TL_DM_PhuCap_NQ104 WHERE iLoai = 3)
			AND BangLuong.Thang = @Thang
			AND BangLuong.Nam = @Nam
			AND BangLuong.Ma_CBo IN (SELECT * FROM f_split(@MaDonVi))
		GROUP BY BangLuongThang.ma_can_bo
	), ThongTinCanBo AS
	(
		SELECT 
			CanBo.Ma_CanBo AS MaCanBo,
			CanBo.Parent AS MaDonVi,
			CanBo.Ten_CanBo AS TenCanBo,
			dbo.f_split_empty(canbo.Ten_CanBo) AS Ten,
            --ISNULL(chucVu.HeSo_Cv, 0) AS HSChucVu,
			ISNULL(canBo.ma_cb104, '0') AS MaCapBac,
			CanBo.So_TaiKhoan AS SoTaiKhoan
		FROM TL_DM_CanBo_NQ104 CanBo
		INNER JOIN TL_DM_DonVi_NQ104 DonVi
			ON CanBo.Parent = DonVi.Ma_DonVi
        LEFT JOIN TL_DM_ChucVu_NQ104 chucVu ON canBo.ma_cvd104=chucVu.ma AND chucVu.nam=@Nam
		WHERE CanBo.Nam = @Nam
		AND CanBo.Thang = @Thang
		AND CanBo.IsDelete = 1
		AND CanBo.Khong_Luong = 0
	)

	SELECT 
		BangLuongThang.GiaTri,
		ThongTinCanBo.MaCanBo,
		ThongTinCanBo.MaDonVi,
		ThongTinCanBo.TenCanBo,
		ThongTinCanBo.Ten,
		--ThongTinCanBo.HSChucVu,
		ThongTinCanBo.MaCapBac,
		ThongTinCanBo.SoTaiKhoan
	FROM BangLuongThang
	INNER JOIN ThongTinCanBo
	ON BangLuongThang.MaCanBo = ThongTinCanBo.MaCanBo
	ORDER BY 
         MaCapBac,
         Ten 
ELSE
	WITH BangLuongThang AS( 
		SELECT 
			BangLuongThang.ma_can_bo AS MaCanBo,
			Sum(BangLuongThang.Gia_Tri) AS GiaTri
		FROM TL_BangLuong_Thang_Bridge_NQ104 BangLuongThang INNER JOIN TL_DS_CapNhap_BangLuong_NQ104 BangLuong
			ON BangLuongThang.parent = BangLuong.Id
		WHERE
			BangLuong.Ma_CachTL = 'CACH0'
			AND BangLuongThang.Ma_Phu_Cap IN (SELECT TL_DM_PhuCap_NQ104.Ma_PhuCap FROM TL_DM_PhuCap_NQ104 WHERE iLoai = 3)
			AND BangLuong.Thang = @Thang
			AND BangLuong.Nam = @Nam
			AND BangLuong.Ma_CBo IN (SELECT * FROM f_split(@MaDonVi))
		GROUP BY BangLuongThang.ma_can_bo
	), ThongTinCanBo AS
	(
		SELECT 
			CanBo.Ma_CanBo AS MaCanBo,
			CanBo.Parent AS MaDonVi,
			CanBo.Ten_CanBo AS TenCanBo,
			dbo.f_split_empty(canbo.Ten_CanBo) AS Ten,
            --ISNULL(chucVu.HeSo_Cv, 0) AS HSChucVu,
			ISNULL(canBo.ma_cb104, '0') AS MaCapBac,
			CanBo.So_TaiKhoan AS SoTaiKhoan
		FROM TL_DM_CanBo_NQ104 CanBo
		INNER JOIN TL_DM_DonVi_NQ104 DonVi
			ON CanBo.Parent = DonVi.Ma_DonVi
        LEFT JOIN TL_DM_ChucVu_NQ104 chucVu ON canBo.ma_cvd104=chucVu.ma  AND chucVu.nam=@Nam
		WHERE CanBo.Nam = @Nam
		AND CanBo.Thang = @Thang
		AND CanBo.IsDelete = 1
		AND CanBo.Khong_Luong = 0
	)

	SELECT 
		BangLuongThang.GiaTri,
		ThongTinCanBo.MaCanBo,
		ThongTinCanBo.MaDonVi,
		ThongTinCanBo.TenCanBo,
		ThongTinCanBo.Ten,
		--ThongTinCanBo.HSChucVu,
		ThongTinCanBo.MaCapBac,
		ThongTinCanBo.SoTaiKhoan
	FROM BangLuongThang
	INNER JOIN ThongTinCanBo
	ON BangLuongThang.MaCanBo = ThongTinCanBo.MaCanBo
	Where GiaTri > 0
	ORDER BY MaCapBac, Ten 
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_delete_can_bo_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_delete_can_bo_nq104]
	@maCanBo varchar(max)
As
Begin
Delete From TL_DM_CanBo_NQ104
Where Ma_CanBo IN (SELECT *
   FROM f_split(@maCanBo))
Delete From TL_CanBo_PhuCap_NQ104
Where MA_CBO IN (SELECT *
   FROM f_split(@maCanBo))
End
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_export_bang_luong_thang_bhxh_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_export_bang_luong_thang_bhxh_nq104]
	@YearOfWork int,
	@Months nvarchar(20)
AS
BEGIN
	SELECT
			--bangLuongThang.iThang Thang,
			bangLuongThang.iNam Nam,
			--bangLuongThang.sMaCBo MaCbo,
			bangLuongThang.sMaHieuCanBo MaHieuCanBo,
			bangLuongThang.sTenCbo TenCbo,
			bangLuongThang.sMaCB MaCb,
			capbac.Note TenCapBac,
			donvi.Ten_DonVi TenDonVi,
			isnull(sum(bangLuongThang.nGiaTri), 0) GiaTri,
			bangLuongThang.sMaCheDo	MaCheDo
		FROM TL_BangLuong_Thang_NQ104BHXH bangLuongThang
		LEFT JOIN TL_DM_DonVi_NQ104 donvi
			ON bangLuongThang.sMaDonVi = donvi.ma_donvi
		LEFT JOIN TL_DM_CapBac_NQ104 capbac
			ON bangLuongThang.sMaCB = capbac.Ma_Cb
		WHERE bangLuongThang.iNam = @YearOfWork
			AND bangLuongThang.iThang in (SELECT * FROM f_split(@Months))
		GROUP BY
			--bangLuongThang.iThang,
			bangLuongThang.iNam,
			--bangLuongThang.sMaCBo,
			bangLuongThang.sMaHieuCanBo,
			bangLuongThang.sTenCbo,
			bangLuongThang.sMaCB,
			capbac.Note,
			donvi.Ten_DonVi,
			bangLuongThang.sMaCheDo
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_find_canbo_quyettoan_quanso_giam_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_find_canbo_quyettoan_quanso_giam_nq104] @maDonVi nvarchar(MAX), @thang int, @nam int
as
begin

declare @thangtruoc int, @namtruoc int, @strThang nvarchar(2)
if @thang = 1
	begin
		set @thangtruoc = 12;
		set @strThang = concat('0', @thang);
		set @namtruoc = @nam - 1;
	end
else if (@thang > 1 and @thang < 10)
	begin
		set @thangtruoc = @thang - 1;
		set @strThang = concat('0', @thang);
		set @namtruoc = @nam;
	end
else 
	begin
		set @thangtruoc = @thang - 1;
		set @strThang = @thang;
		set @namtruoc = @nam;
end

select	cbThangnay.Id, cbThangnay.BHTN, cbThangnay.bNuocNgoai, cbThangnay.Cb_KeHoach, cbThangnay.Cccd, cbThangnay.DateCreated, cbThangnay.DateModified, cbThangnay.Dia_Chi, cbThangnay.Dien_Thoai, 
		cbThangnay.GTGC, cbThangnay.HeSoLuong, cbThangnay.HsLuongKeHoach, cbThangnay.HsLuongTran, cbThangnay.iTrangThai, cbThangnay.IdLuongTran, cbThangnay.IsDelete, cbThangnay.IsLock, 
		cbThangnay.Is_Nam, cbThangnay.Khong_Luong, cbThangnay.Ma_BL, cbThangnay.Ma_CanBo, cbThangnay.Ma_CB, cbThangnay.Ma_CV, cbThangnay.Ma_DiaBan_HC, cbThangnay.Ma_Hieu_CanBo, 
		cbThangnay.Ma_KhoBac, cbThangnay.Ma_PBan, cbThangnay.MaSo_DV_SDNS, cbThangnay.MaSo_VAT, cbThangnay.Ma_TangGiam, cbThangnay.Ma_TangGiamCu, cbThangnay.MaTK_LQ, cbThangnay.Nam, cbThangnay.Nam_TN, 
		cbThangnay.Nam_VK, cbThangnay.NgayCap_CMT, cbThangnay.Ngay_NhanCB, cbThangnay.Ngay_NN, cbThangnay.NgaySinh, cbThangnay.Ngay_TN, cbThangnay.NgayTruyLinh, cbThangnay.Ngay_XN, cbThangnay.Nhom, 
		cbThangnay.NoiCap_CMT, cbThangnay.NoiCongTac, cbThangnay.PCCV, cbThangnay.Parent, cbThangTruoc.Parent as ParentCu, cbThangnay.[Readonly], cbThangnay.So_CMT, cbThangnay.So_SoLuong, cbThangnay.So_TaiKhoan, cbThangnay.Splits, 
		cbThangnay.Ten_CanBo, cbThangnay.Ten_DonVi, cbThangnay.Ten_KhoBac, cbThangnay.Thang, cbThangnay.Thang_TNN, cbThangnay.ThoiHan_TangCb, 
		cbThangnay.TM, cbThangnay.UserCreator, cbThangnay.UserModifier, cbThangnay.bKhongTinhNTN,
		case 
			when cbThangTruoc.Ma_Cb = cbThangnay.Ma_CB then null 
			else cbThangTruoc.Ma_CB 
		end as Ma_CbCu
from (select * from TL_DM_CanBo_NQ104 where Thang = @thang and Nam = @nam and ParentOld = @maDonVi) cbThangnay
left join (select * from TL_DM_CanBo_NQ104 where Thang = @thangtruoc and Nam = @namtruoc) cbThangTruoc on cbThangnay.Ma_CanBo = concat(@nam , @strThang, substring(cbThangTruoc.Ma_CanBo, 7, len(cbThangTruoc.Ma_CanBo) - 6))
end
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_find_canbo_quyettoan_quanso_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_find_canbo_quyettoan_quanso_nq104] @maDonVi nvarchar(MAX), @thang int, @nam int
as
begin

declare @thangtruoc int, @namtruoc int, @strThang nvarchar(2)
if @thang = 1
	begin
		set @thangtruoc = 12;
		set @strThang = concat('0', @thang);
		set @namtruoc = @nam - 1;
	end
else if (@thang > 1 and @thang < 10)
	begin
		set @thangtruoc = @thang - 1;
		set @strThang = concat('0', @thang);
		set @namtruoc = @nam;
	end
else 
	begin
		set @thangtruoc = @thang - 1;
		set @strThang = @thang;
		set @namtruoc = @nam;
end

select	cbThangnay.Id, cbThangnay.BHTN, cbThangnay.bNuocNgoai, cbThangnay.Cb_KeHoach, cbThangnay.Cccd, cbThangnay.DateCreated, cbThangnay.DateModified, cbThangnay.Dia_Chi, cbThangnay.Dien_Thoai, 
		cbThangnay.GTGC, cbThangnay.HeSoLuong, cbThangnay.HsLuongKeHoach, cbThangnay.HsLuongTran, cbThangnay.iTrangThai, cbThangnay.IdLuongTran, cbThangnay.IsDelete, cbThangnay.IsLock, 
		cbThangnay.Is_Nam, cbThangnay.Khong_Luong, cbThangnay.Ma_BL, cbThangnay.Ma_CanBo, cbThangnay.Ma_CB, cbThangnay.Ma_CV, cbThangnay.Ma_DiaBan_HC, cbThangnay.Ma_Hieu_CanBo, 
		cbThangnay.Ma_KhoBac, cbThangnay.Ma_PBan, cbThangnay.MaSo_DV_SDNS, cbThangnay.MaSo_VAT, cbThangnay.Ma_TangGiam, cbThangnay.Ma_TangGiamCu, cbThangnay.MaTK_LQ, cbThangnay.Nam, cbThangnay.Nam_TN, 
		cbThangnay.Nam_VK, cbThangnay.NgayCap_CMT, cbThangnay.Ngay_NhanCB, cbThangnay.Ngay_NN, cbThangnay.NgaySinh, cbThangnay.Ngay_TN, cbThangnay.NgayTruyLinh, cbThangnay.Ngay_XN, cbThangnay.Nhom, 
		cbThangnay.NoiCap_CMT, cbThangnay.NoiCongTac, cbThangnay.PCCV, cbThangnay.Parent, cbThangTruoc.Parent as ParentCu, cbThangnay.[Readonly], cbThangnay.So_CMT, cbThangnay.So_SoLuong, cbThangnay.So_TaiKhoan, cbThangnay.Splits, 
		cbThangnay.Ten_CanBo, cbThangnay.Ten_DonVi, cbThangnay.Ten_KhoBac, cbThangnay.Thang, cbThangnay.Thang_TNN, cbThangnay.ThoiHan_TangCb, 
		cbThangnay.TM, cbThangnay.UserCreator, cbThangnay.UserModifier, cbThangnay.bKhongTinhNTN,
		case 
			when cbThangTruoc.Ma_Cb = cbThangnay.Ma_CB then null 
			else cbThangTruoc.Ma_CB 
		end as Ma_CbCu
from (select * from TL_DM_CanBo_NQ104 where Thang = @thang and Nam = @nam and Parent = @maDonVi) cbThangnay
left join (select * from TL_DM_CanBo_NQ104 where Thang = @thangtruoc and Nam = @namtruoc) cbThangTruoc on cbThangnay.Ma_CanBo = concat(@nam , @strThang, substring(cbThangTruoc.Ma_CanBo, 7, len(cbThangTruoc.Ma_CanBo) - 6))
end
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_find_danhsach_canbo_by_condition_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_find_danhsach_canbo_by_condition_nq104]
	@thang int,
	@nam int,
	@maDonVi nvarchar(250)
AS
	Select
	  canbo.[Id]
      ,[BHTN]
      ,[bNuocNgoai]
      ,[Cb_KeHoach]
      ,[Cccd]
      ,[DateCreated]
      ,[DateModified]
      ,[Dia_Chi]
      ,[Dien_Thoai]
      ,[GTGC]
      ,[HeSoLuong]
      ,[HsLuongKeHoach]
      ,[HsLuongTran]
      ,canbo.[iTrangThai]
      ,[IdLuongTran]
      ,[IsDelete]
      ,[IsLock]
      ,[Is_Nam]
      ,[Khong_Luong]
      ,[Ma_BL]
      ,[Ma_CanBo]
      ,canbo.[Ma_CB]
      ,[Ma_CbCu]
      ,canbo.[Ma_CV]
      ,[Ma_DiaBan_HC]
      ,[Ma_Hieu_CanBo]
      ,[Ma_KhoBac]
      ,[Ma_PBan]
      ,[MaSo_DV_SDNS]
      ,[MaSo_VAT]
      ,[Ma_TangGiam]
      ,[Ma_TangGiamCu]
      ,[MaTK_LQ]
      ,canbo.[Nam]
      ,[Nam_TN]
      ,[Nam_VK]
      ,[NgayCap_CMT]
      ,[Ngay_NhanCB]
      ,[Ngay_NN]
      ,[NgaySinh]
      ,[Ngay_TN]
      ,[NgayTruyLinh]
      ,[Ngay_XN]
      ,[Nhom]
      ,[NoiCap_CMT]
      ,[NoiCongTac]
      ,[PCCV]
      ,canbo.[Parent]
      ,canbo.[Readonly]
      ,[So_CMT]
      ,[So_SoLuong]
      ,[So_TaiKhoan]
      ,canbo.[Splits]
      ,[Ten_CanBo]
      ,[Ten_KhoBac]
      ,[Thang]
      ,[Thang_TNN]
      ,[ThoiHan_TangCb]
      ,[TM]
      ,[UserCreator]
      ,[UserModifier]
      ,[bKhongTinhNTN],
	  donvi.Ten_DonVi as Ten_DonVi,
	  ISNULL(capbac.Note, '') CapBac,
	  ISNULL(chucvu.Ten_Cv, '') ChucVu,
	  dbo.f_split_empty(canbo.Ten_CanBo) AS Ten
	From TL_DM_CanBo_NQ104 canbo
	Join TL_DM_CapBac_NQ104 capbac on capbac.Ma_Cb = canbo.Ma_CB104
	Left join TL_DM_DonVi_NQ104 donvi on canbo.Parent = donvi.Ma_DonVi 
	Left join TL_DM_ChucVu chucvu on canbo.Ma_CV = chucvu.Ma_Cv
	Where canbo.IsDelete = 1 
		and canbo.Thang = @thang 
		and canbo.Nam = @nam
		and (@maDonVi = '' or canbo.Parent = @maDonVi)
	--Order By canbo.Parent, canbo.Ma_CV desc, canbo.Ma_CB desc
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_find_danhsach_canbo_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_find_danhsach_canbo_nq104]
AS
	Select
	  canbo.[Id]
      ,[BHTN]
      ,[bNuocNgoai]
      ,[Cb_KeHoach]
      ,[Cccd]
      ,[DateCreated]
      ,[DateModified]
      ,[Dia_Chi]
      ,[Dien_Thoai]
      ,[GTGC]
      ,[HeSoLuong]
      ,[HsLuongKeHoach]
      ,[HsLuongTran]
      ,canbo.[iTrangThai]
      ,[IdLuongTran]
      ,[IsDelete]
      ,[IsLock]
      ,[Is_Nam]
      ,[Khong_Luong]
      ,[Ma_BL]
      ,[Ma_CanBo]
      ,canbo.[Ma_CB]
      ,[Ma_CbCu]
      ,canbo.[ma_cvd104] as Ma_CV
      ,[Ma_DiaBan_HC]
      ,[Ma_Hieu_CanBo]
      ,[Ma_KhoBac]
      ,[Ma_PBan]
      ,[MaSo_DV_SDNS]
      ,[MaSo_VAT]
      ,[Ma_TangGiam]
      ,[Ma_TangGiamCu]
      ,[MaTK_LQ]
      ,canbo.[Nam]
      ,[Nam_TN]
      ,[Nam_VK]
      ,[NgayCap_CMT]
      ,[Ngay_NhanCB]
      ,[Ngay_NN]
      ,[NgaySinh]
      ,[Ngay_TN]
      ,[NgayTruyLinh]
      ,[Ngay_XN]
      ,[Nhom]
      ,[NoiCap_CMT]
      ,[NoiCongTac]
      ,[PCCV]
      ,canbo.[Parent]
      ,canbo.[Readonly]
      ,[So_CMT]
      ,[So_SoLuong]
      ,[So_TaiKhoan]
      ,canbo.[Splits]
      ,[Ten_CanBo]
      ,[Ten_KhoBac]
      ,[Thang]
      ,[Thang_TNN]
      ,[ThoiHan_TangCb]
      ,[TM]
      ,[UserCreator]
      ,[UserModifier]
      ,[bKhongTinhNTN],
	  donvi.Ten_DonVi as Ten_DonVi,
	  ISNULL(capbac.ten_cb, '') CapBac,
	  ISNULL(chucvu.ten, '') ChucVu,
	  dbo.f_split_empty(canbo.Ten_CanBo) AS Ten,
	  canbo.bTinhBHXH BTinhBHXH, --anhnh156
	  canbo.dan_toc DanToc,
	  canbo.loai_doi_tuong LoaiDoiTuong,
	  canbo.ma_cb104 MaCb104,
	  canbo.loai Loai,
	  canbo.nhom_chuyen_mon NhomChuyenMon,
	  canbo.ma_bac_luong MaBacLuong,
	  canbo.tien_luong_cb TienLuongCb,
	  canbo.ma_cvd104 MaCvd104,
	  canbo.tien_luong_cvd TienLuongCvd,
	  canbo.ngay_nhan_cb_tu_ngay NgayNhanCbTuNgay,
	  canbo.ngay_nhan_cb_den_ngay NgayNhanCbDenNgay,
	  canbo.ngay_nhan_cvd_tu_ngay NgayNhanCvdTuNgay,
	  canbo.ngay_nhan_cvd_den_ngay NgayNhanCvdDenNgay,
	  canbo.so_thang_tinh_bao_luu_cvd SoThangTinhBaoLuuCvd,
	  canbo.so_thang_tinh_bao_luu_cb SoThangTinhBaoLuuCb,
	  canbo.tien_bao_luu_cb TienBaoLuuCb,
	  canbo.tien_bao_luu_cvd TienBaoLuuCvd
	From TL_DM_CanBo_NQ104 canbo
	Left join TL_DM_CapBac_NQ104 capbac on capbac.Ma_Cb = canbo.ma_cb104
	Left join TL_DM_DonVi_NQ104 donvi on canbo.Parent = donvi.Ma_DonVi 
	Left join TL_DM_ChucVu_NQ104 chucvu on canbo.ma_cvd104 = chucvu.ma
	Where canbo.IsDelete = 1
	--Order By canbo.Parent, canbo.Ma_CV desc, canbo.Ma_CB desc
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_ra_quan_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_get_data_ra_quan_nq104]
As
SELECT TenCb,
		  MaCanBo,
		  MaCb,
		  Thang,
		  Nam,
		  NgayNn,
		  NgayXn,
		  MaDonVi,
		  TenDonVi,
		  SoSoLuong,
		  TenCapBac,
          TIENTAUXE_TT AS TienTauXe,
          TIENANDUONG_TT AS TienAnDuong,
          TIENCTLH_TT AS TienChiaTay,
          GTKHAC_TT AS GiamTruKhac
   FROM
     (SELECT canBo.Ma_CanBo MaCanBo,
             canBo.Ten_CanBo AS TenCb,
			 canbo.ma_cb104  as MaCb,
			 canBo.Thang, 
			 canBo.Nam,
			 canBo.Parent as MaDonVi,
			 donvi.Ten_DonVi as TenDonVi,
			 canBo.So_SoLuong as SoSoLuong,
			 canBo.Ngay_NN as NgayNn,
			 canBo.Ngay_XN as NgayXn,
			 capBac.ten_cb as TenCapBac,
             bangLuong.GIA_TRI AS GiaTri,
             bangLuong.MA_PHU_CAP AS MaPhuCap
      FROM TL_CanBo_PhuCap_Bridge_NQ104 bangLuong
      RIGHT JOIN TL_DM_CanBo_NQ104 canBo ON bangLuong.Ma_Can_Bo = canBo.Ma_CanBo
	  LEFT JOIN TL_DM_DonVi_NQ104 donvi ON canBo.Parent = donvi.Ma_DonVi
	  Join TL_DM_CapBac_NQ104 capBac ON canBo.ma_cb104 = capBac.Ma_Cb
      WHERE
		canBo.IsDelete=1
        AND canBo.Khong_Luong=0
		And canBo.ma_cb104 like '0%') x PIVOT (SUM(GiaTri)
                                           FOR MaPhuCap IN (TIENTAUXE_TT, TIENANDUONG_TT, TIENCTLH_TT, GTKHAC_TT)) pvt
      Order By MaDonVi
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_thue_tncn_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_get_data_thue_tncn_nq104]
As
SELECT TenCb,
		  MaCanBo,
		  MaCb,
		  Thang,
		  Nam,
		  MaDonVi,
		  TenDonVi,
		  SoSoLuong,
          THUONG_TT AS TienThuong,
          THUNHAPKHAC_TT AS LoiIchKhac,
          GIAMTHUE_TT AS TienThueDuocGiam,
          THUEDANOP_TT AS ThueTNCNDaNop
   FROM
     (SELECT canBo.Ma_CanBo MaCanBo,
             canBo.Ten_CanBo AS TenCb,
			 canbo.Ma_CB as MaCb,
			 canBo.Thang, 
			 canBo.Nam,
			 canBo.Parent as MaDonVi,
			 donvi.Ten_DonVi as TenDonVi,
			 canBo.So_SoLuong as SoSoLuong,
             bangLuong.GIA_TRI AS GiaTri,
             bangLuong.MA_PHU_CAP AS MaPhuCap
      FROM TL_CanBo_PhuCap_Bridge_NQ104 bangLuong
      JOIN TL_DM_CanBo_NQ104 canBo ON bangLuong.Ma_Can_Bo=canBo.Ma_CanBo
	  LEFT JOIN TL_DM_DonVi_NQ104 donvi ON canbo.Parent = donvi.Ma_DonVi
      WHERE
		canBo.IsDelete=1
        AND canBo.Khong_Luong=0) x PIVOT (SUM(GiaTri)
                                           FOR MaPhuCap IN (THUONG_TT, THUNHAPKHAC_TT, GIAMTHUE_TT, THUEDANOP_TT)) pvt
      Order By MaDonVi
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_donvi_bang_phucap_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_get_donvi_bang_phucap_nq104] @thang int, @nam int, @cachTinhLuong varchar(20)
AS
BEGIN

select Parent into #tmpCB from TL_DM_CanBo_NQ104 where left (Ma_CB104, 1) = 0 and Thang = @thang and Nam = @nam group by Parent

select donvi.* from #tmpCB canbo
left join TL_DS_CapNhap_BangLuong_NQ104 bangluong  on canbo.Parent = bangluong.Ma_CBo 
left join TL_DM_DonVi_NQ104 donvi on canbo.Parent = donvi.Ma_DonVi
where bangluong.Thang = @thang and bangluong.Nam = @nam and bangluong.Ma_CachTL = @cachTinhLuong

drop table #tmpCB

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_donvi_bangluong_thang_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_get_donvi_bangluong_thang_nq104] @thang int, @nam int, @cachTinhLuong varchar(20)
AS
BEGIN

select Parent into #tmpCB from TL_DM_CanBo_NQ104 where left (Ma_CB104, 1) <> 0 and Thang = @thang and Nam = @nam group by Parent

select donvi.* from #tmpCB canbo
left join TL_DS_CapNhap_BangLuong_NQ104 bangluong  on canbo.Parent = bangluong.Ma_CBo 
left join TL_DM_DonVi_NQ104 donvi on canbo.Parent = donvi.Ma_DonVi
where bangluong.Thang = @thang and bangluong.Nam = @nam and bangluong.Ma_CachTL = @cachTinhLuong

drop table #tmpCB

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_donvi_baocao_quanso_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_get_donvi_baocao_quanso_nq104] 
	-- Add the parameters for the stored procedure here
	@nam int
AS
BEGIN
	select dv.Id , iTrangThai, dv.Ma_DonVi, dv.Parent_id, dv.Ten_DonVi, dv.XauNoiMa 
	from TL_DM_DonVi_NQ104 dv
	join TL_QS_ChungTu qs on dv.Ma_DonVi = qs.Ma_DonVi 
	where qs.sTongHop is null and iTrangThai = 1

	union all

	select iID_DonVi as Id, cast(iTrangThai as bit), iID_MaDonVi as Ma_DonVi, NULL as Parent_id, sTenDonVi as Ten_DonVi, NULL as XauNoiMa 
	from DonVi 
	where iNamLamViec = @nam and iTrangThai = 1
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_donvi_quanso_nam_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_get_donvi_quanso_nam_nq104] @nam int
as
begin
select * from TL_DM_DonVi_NQ104
where Ma_DonVi in (SELECT Parent FROM TL_DM_CanBo_NQ104 WHERE Nam = @nam)
end
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_donvi_tao_bangluong_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_get_donvi_tao_bangluong_nq104] @nam int, @thang int, @cachTinhLuong varchar(20)
As
Begin
WITH 
DonViBangLuong 
as (Select distinct(donVi.Id), donVi.Ma_DonVi, donVi.Parent_id, donVi.Ten_DonVi, donVi.XauNoiMa, donVi.iTrangThai From TL_DM_DonVi_NQ104 as donVi
Left Join TL_DS_CapNhap_BangLuong_NQ104 on donVi.Ma_DonVi = TL_DS_CapNhap_BangLuong_NQ104.Ma_CBo 
And TL_DS_CapNhap_BangLuong_NQ104.Thang = @thang
And TL_DS_CapNhap_BangLuong_NQ104.Nam = @nam
And TL_DS_CapNhap_BangLuong_NQ104.Ma_CachTL = @cachTinhLuong
Where TL_DS_CapNhap_BangLuong_NQ104.Id is Null
AND donVi.iTrangThai = 1),
DonViCanBo 
as (Select distinct(donVi.Id), donVi.Ma_DonVi, donVi.Parent_id, donVi.Ten_DonVi, donVi.XauNoiMa From TL_DM_DonVi_NQ104 as donVi
Left Join TL_DM_CanBo_NQ104 on donVi.Ma_DonVi = TL_DM_CanBo_NQ104.Parent And TL_DM_CanBo_NQ104.IsDelete = 1
And TL_DM_CanBo_NQ104.Thang = @thang
And TL_DM_CanBo_NQ104.Nam = @nam
Where Ma_CanBo is not null)

Select distinct(DonViBangLuong.Id), DonViBangLuong.Ma_DonVi, DonViBangLuong.Parent_id, DonViBangLuong.Ten_DonVi, DonViBangLuong.XauNoiMa,
	DonViBangLuong.iTrangThai From
DonViBangLuong
Join DonViCanBo on DonViBangLuong.Id = DonViCanBo.Id
End
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_lay_du_lieu_bang_luong_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_lay_du_lieu_bang_luong_nq104]
AS

SELECT Thang,
       Nam,
       MA_CBO as MaCanBo,
       Ten_CanBo as TenCanBo,
	   Ma_Hieu_CanBo as MaHieuCanBo,
       Ten_DonVi as TenDonVi,
       Ma_DonVi as MaDonVi,
       LHT_TT as LhtTt,
	   PCTNVK_TT as PctnvkTt,
	   HSBL_TT as HsblTt,
	   PCTN_TT as PctnTt,
	   PCCV_TT as PccvTt,
	   PCCOV_TT as PccovTt,
	   PCTRA_SUM as PctraSum,
	   PCDACTHU_SUM as PcdacthuSum,
	   PCKHAC_SUM as PckhacSum,
	   BHCN_TT as BhcnTt,
	   THUETNCN_TT as ThuetncnTt,
	   TA_TT as TaTt, 
	   THANHTIEN as ThanhTien
FROM
  (SELECT TL_BangLuong_Thang_NQ104.Thang,
          TL_BangLuong_Thang_NQ104.Nam,
          MA_CBO,
          TL_DM_DonVi_NQ104.Ten_Donvi,
          TL_DM_DonVi_NQ104.Ma_DonVi,
          Ten_CanBo,
		  TL_DM_CanBo_NQ104.Ma_Hieu_CanBo,
          GIA_TRI,
          MA_PHUCAP
   FROM [dbo].[TL_BangLuong_Thang_NQ104]
   JOIN [dbo].[TL_DM_CanBo_NQ104] ON TL_BangLuong_Thang_NQ104.Ma_CBo = TL_DM_CanBo_NQ104.Ma_CanBo
   JOIN [dbo].[TL_DM_DonVi_NQ104] ON TL_BangLuong_Thang_NQ104.Ma_DonVi = [dbo].[TL_DM_DonVi_NQ104].Ma_DonVi
   WHERE Ma_CachTL = 'CACH0' ) x pivot (SUM(GIA_TRI)
                                        FOR MA_PHUCAP in (LHT_TT, PCTNVK_TT, HSBL_TT, PCTN_TT, PCCV_TT, PCCOV_TT, PCTRA_SUM, PCDACTHU_SUM, PCKHAC_SUM, BHCN_TT, THUETNCN_TT, TA_TT, THANHTIEN)) p
ORDER BY Nam,
         Thang,
         Ma_DonVi,
         Ten_CanBo
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_lay_du_lieu_luong_thang_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_lay_du_lieu_luong_thang_nq104] @prParent varchar(100)
AS
DECLARE @cols AS NVARCHAR(MAX);
DECLARE @query  AS NVARCHAR(MAX);;
DECLARE @parent AS NVARCHAR(MAX);

SET @parent = @prParent

SET @cols = STUFF(
  (
    SELECT
      distinct ',' + QUOTENAME(phucap.Ma_PhuCap) 
    FROM TL_DM_PhuCap_NQ104 phucap
        FOR XML PATH(''), TYPE
  ).value('.', 'NVARCHAR(MAX)'),1,1,'')
  
SET @query = 'SELECT Thang, Nam, MA_CBO, Ten_CanBo, Ten_DonVi, ' + @cols + ' from 
            (
                select TL_BangLuong_Thang_NQ104.Thang, TL_BangLuong_Thang_NQ104.Nam, 
				MA_CBO, TL_DM_DonVi_NQ104.Ten_Donvi, Ten_CanBo, GIA_TRI, MA_PHUCAP
                from [dbo].[TL_BangLuong_Thang_NQ104]
				join [dbo].[TL_DM_CanBo_NQ104]
				ON TL_BangLuong_Thang_NQ104.Ma_CBo = TL_DM_CanBo_NQ104.Ma_CanBo
				join [dbo].[TL_DM_DonVi_NQ104]
				ON TL_BangLuong_Thang_NQ104.Ma_DonVi = [dbo].[TL_DM_DonVi_NQ104].Ma_DonVi
				where TL_BangLuong_Thang_NQ104.parent = + ''' + @prParent + '''
				and TL_DM_CanBo_NQ104.IsDelete = 1
				and TL_DM_CanBo_NQ104.Khong_Luong = 0
           ) x
            pivot 
            (
                 SUM(GIA_TRI)
                for MA_PHUCAP in (' + @cols + ')
            ) p '
execute(@query)
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangke_trichthue_tncn_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TungNH
-- Create date: 20/12/2021
-- Description:	Lấy dữ liệu nộp thuế thu nhập cá nhân theo tháng
-- =============================================
CREATE PROCEDURE [dbo].[sp_tl_rpt_bangke_trichthue_tncn_nq104]
	@Thang int,
	@Nam AS int,
	@MaCachTL NVARCHAR(50),
	@MaDonVi NVARCHAR(MAX),
	@IsExportAll bit,
	@IsOrderChucVu bit = 0
AS
BEGIN
	SET NOCOUNT ON;

    DECLARE @Cols AS NVARCHAR(MAX)
	DECLARE @Query AS NVARCHAR(MAX)

	SET @Cols = 'GTNN,GTPT_SN,GTPT_DG,GTKHAC_TT,LUONGTHUE_TT,THUETNCN_TT,GIAMTHUE_TT,THUEDANOP_TT,LHT_TT,PCCT_TT,THUONG_TT,THUNHAPKHAC_TT,BHCN_TT,GTPT_TT'
	SET @Query =
	'
	WITH BangLuongThang AS (
		SELECT
			dsCapNhapBangLuong.Thang	AS Thang,
			dsCapNhapBangLuong.Nam		AS Nam,
			bangLuong.Ma_Can_Bo			AS MaCanBo,
			bangLuong.MA_PHU_CAP			AS MaPhuCap,
			bangLuong.GIA_TRI			AS GiaTri
		FROM TL_BangLuong_Thang_Bridge_NQ104 bangLuong
		JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhapBangLuong
			ON bangLuong.parent = dsCapNhapBangLuong.Id
		WHERE
			bangLuong.Ma_Phu_Cap IN (SELECT * FROM f_split(''' + @Cols + '''))
			AND dsCapNhapBangLuong.Ma_CachTL=''' + @MaCachTL + '''
			AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
			AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND dsCapNhapBangLuong.Status=1
	), ThongTinCanBo AS (
		SELECT
			donVi.Ma_DonVi										AS MaDonVi,
			canBo.Ma_CanBo										AS MaCanBo,
			canBo.Ten_CanBo										AS TenCanBo,
			dbo.f_split_empty(canbo.Ten_CanBo)                  AS Ten,
			ISNULL(canBo.Ma_CB104, ''0'')							AS MaCapBac,
			ISNULL(capBac.Ten_Cb, ''0'')						AS CapBac,
			chucVu.Ma										AS MaChucVu,
			chucVu.Ten										AS TenChucVu
		FROM TL_DM_CanBo_NQ104 canBo
		INNER JOIN TL_DM_DonVi_NQ104 donVi
			ON canBo.Parent = donVi.Ma_DonVi
		LEFT JOIN TL_DM_CapBac_NQ104 capBac
			ON canBo.Ma_CB104 = capBac.Ma_Cb
		LEFT JOIN TL_DM_ChucVu_NQ104 chucVu
			ON canBo.Ma_CVd104 = chucVu.Ma
		WHERE
			canBo.IsDelete = 1
			AND canBo.Khong_Luong = 0
			AND canBo.Thang = ' + CAST(@Thang AS VARCHAR(2)) + '
			AND canBo.Nam = ' + CAST(@Nam AS VARCHAR(4)) + '
	)
	SELECT MaDonVi, MaCanBo, TenCanBo, (GTPT_SN*GTPT_DG) GIAM_TRU_PHU_THUOC, 
	(LHT_TT + PCCT_TT + THUONG_TT + ISNULL(THUNHAPKHAC_TT, 0) - GTPT_TT - BHCN_TT - GIAMTHUE_TT) TINHTHUE,
		' + @Cols + ' FROM (
		SELECT
			canBo.MaDonVi,
			canBo.MaCanBo,
			canBo.TenCanBo,
			canBo.Ten,
			canBo.MaCapBac,
			canBo.CapBac,
			canBo.MaChucVu,
			canBo.TenChucVu,
			bangLuong.GiaTri,
			bangLuong.MaPhuCap
		FROM BangLuongThang bangLuong
		INNER JOIN ThongTinCanBo canBo
			ON bangLuong.MaCanBo = canBo.MaCanBo
	) x
	PIVOT
	(
		SUM(GiaTri)
		FOR MaPhuCap IN (' + @Cols + ')
	) pvt
	WHERE (1=1)'

	--WHERE (' + CAST(@IsExportAll AS VARCHAR(1)) + ' = 1 OR THUETNCN_TT > 0)'
	IF @IsOrderChucVu = 1
	SET @Query = @Query +' ORDER BY MaCapBac DESC, Ten ASC';
	ELSE 
	SET @Query = @Query +' ORDER BY MaCapBac DESC, Ten ASC';
	execute(@Query)

	--select @Query
END
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangluong_thang_giaithich_chitiet_theo_ngach_capbac_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TungNH
-- Create date: 25/04/2022
-- Description:	Báo cáo giải thích chi tiết lương theo ngạch, cấp bậc
-- =============================================
CREATE PROCEDURE [dbo].[sp_tl_rpt_bangluong_thang_giaithich_chitiet_theo_ngach_capbac_nq104] 
	@Thang int, 
	@Nam int,
	@MaDonVi NVARCHAR(MAX),
	@MaCachTl NVARCHAR(50),
	@MaPhuCap NVARCHAR(MAX),
	@MaPhuCapCount NVARCHAR(MAX),
	@DonViTinh int,
	@IsSummary bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Query AS NVARCHAR(MAX)
	SET @Query =
	'
    WITH BangLuongThang AS (
		SELECT
			dsCapNhapBangLuong.Thang	AS Thang,
			dsCapNhapBangLuong.Nam		AS Nam,
			bangLuong.Ma_Can_Bo			AS MaCanBo,
			bangLuong.MA_PHU_CAP		AS MaPhuCap,
			bangLuong.GIA_TRI			AS GiaTri
		FROM TL_BangLuong_Thang_Bridge_NQ104 bangLuong
		JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhapBangLuong
			ON bangLuong.parent = dsCapNhapBangLuong.Id
		WHERE
			bangLuong.Ma_Phu_Cap IN (SELECT * FROM f_split(''' + @MaPhuCap + '''))
			AND bangLuong.Gia_Tri <> 0
			AND dsCapNhapBangLuong.Ma_CachTL=''' + @MaCachTl + '''
			AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
			AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND dsCapNhapBangLuong.Status=1
	), 
	macapbac_top_parent as (
		select Ma_Cb, Parent, Ma_Cb AS top_parent 
		from TL_DM_CapBac_NQ104
		where parent is null
		and nam = ' + CAST(@Nam AS VARCHAR(4)) + '

		union all

		select o.ma_cb, o.parent, e.top_parent
		from TL_DM_CapBac_NQ104 o
		inner join macapbac_top_parent e on o.Parent = e.ma_cb
		where nam =' + CAST(@Nam AS VARCHAR(4)) + '
	),
	ThongTinCanBo AS (
		SELECT
			canBo.Ma_CanBo		AS MaCanBo,
			canBo.Ten_CanBo		AS TenCanBo,
			donVi.Ma_DonVi		AS MaDonVi,
			canBo.Ma_Hieu_CanBo	AS MaHieuCanBo,
			capBacParent.Ma_Cb			AS MaCapBac,
			capBacParent.top_parent			AS MaCapBacParent,
			--CASE
				--WHEN capBac.Xau_Noi_Ma LIKE ''1%'' OR capBac.Xau_Noi_Ma LIKE ''4%'' THEN capbacluong.Lht_Hs
				--ELSE canBo.HeSoLuong
			--END AS HeSoLuong
			canBo.HeSoLuong HeSoLuong
		FROM TL_DM_CanBo_NQ104 canBo
		INNER JOIN TL_DM_DonVi_NQ104 donVi
			ON canBo.Parent = donVi.Ma_DonVi
		INNER JOIN TL_DM_CapBac_NQ104 capBac
			ON canBo.ma_cb104 = capBac.Ma_Cb
		LEFT JOIN macapbac_top_parent capBacParent
			on capBac.Ma_CB = capBacParent.ma_cb
		--INNER JOIN TL_DM_CapBac_Luong_NQ104 capbacluong on capBac.xau_noi_ma = capbacluong.xau_noi_ma
		WHERE
			canBo.IsDelete = 1
			AND canBo.Khong_Luong = 0
			AND canBo.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND canBo.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND canBo.Parent IN (SELECT * FROM f_split( ''' + @MaDonVi + '''))
			AND capBac.nam = ' + CAST(@Nam AS VARCHAR(4)) + '
	), SoLieuBaoCao AS (
		SELECT
			CASE WHEN ' + CAST(@IsSummary AS VARCHAR(1)) + ' = 1 THEN NULL ELSE canBo.MaDonVi END	AS MaDonVi,
			canBo.MaCapBac																			AS MaCapBac,
			canBo.MaCapBacParent																	AS MaCapBacParent,
			canBo.HeSoLuong																			AS HeSoLuong,
			bangLuongThang.MaPhuCap																	AS MaPhuCap,
			COUNT(canBo.MaCanBo)																	AS SoNguoi,
			SUM(bangLuongThang.GiaTri) / ' + CAST(@DonViTinh AS VARCHAR(100)) + '					AS GiaTri
		FROM ThongTinCanBo canBo
		INNER JOIN BangLuongThang bangLuongThang
			ON bangLuongThang.MaCanBo = canBo.MaCanBo
		GROUP BY MaDonVi, MaCapBac, HeSoLuong, MaPhuCap, MaCapBacParent
	), CanBoLuongCapBac AS (
		SELECT
			bangLuong.Ma_Don_Vi AS Ma_DonVi,
			canbo.ma_cb104			AS MaCapBac,
			bangLuong.GIA_TRI	AS HeSoLuong
		FROM TL_BangLuong_Thang_Bridge_NQ104 bangLuong
		JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhapBangLuong
			ON bangLuong.parent = dsCapNhapBangLuong.Id
		JOIN TL_DM_CanBo_NQ104 canbo 
			ON bangLuong.ma_hieu_can_bo = canbo.Ma_Hieu_CanBo
				AND canbo.Nam = ' + CAST(@Nam AS VARCHAR(4)) + '
				AND canbo.Thang = ' + CAST(@Thang AS VARCHAR(2)) + '
		WHERE
			bangLuong.Ma_Phu_Cap = ''LHT_HS''
			AND bangLuong.Gia_Tri <> 0
			AND dsCapNhapBangLuong.Ma_CachTL=''' + @MaCachTl + '''
			AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
			AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND dsCapNhapBangLuong.Status=1
	)

	SELECT ''' + @MaCachTl + ''' AS CachTl, (SELECT COUNT(*) FROM CanBoLuongCapBac canBo INNER JOIN TL_DM_CapBac_NQ104 as cb on canBo.MaCapBac = cb.Ma_Cb WHERE ' + (CASE WHEN @IsSummary = 1 THEN '' ELSE ' canBo.Ma_DonVi = pvt.MaDonVi AND ' END)+ ' canBo.MaCapBac = pvt.MaCapBac AND cb.nam = ' + CAST(@Nam AS VARCHAR(4)) + ' AND ((cb.Xau_Noi_Ma LIKE ''1%'' OR cb.Xau_Noi_Ma LIKE ''4%'') OR canBo.HeSoLuong = pvt.HeSoLuong)) AS SoNguoi, pvt.* FROM (
		SELECT
			MaDonVi,
			MaCapBac,
			MaCapBacParent,
			HeSoLuong,
			DATA,
			COLUMN_NAME + MaPhuCap AS PIV_COL
		FROM SoLieuBaoCao
		CROSS APPLY (
			VALUES (''COUNT_'', SoNguoi), ('''', GiaTri) 
		) CS(COLUMN_NAME, DATA)
	) a
	PIVOT (
		SUM(DATA)
		FOR PIV_COL IN (' + @MaPhuCap + ', ' + @MaPhuCapCount + ')
	) pvt ORDER BY MaDonVi, MaCapBac, HeSoLuong'

	execute(@Query)
END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangluong_truylinh_dong_phucap]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_tl_rpt_bangluong_truylinh_dong_phucap]
	@MaDonVi NVARCHAR(MAX), 
	@Thang int,
	@Nam AS int,
	@MaPhuCap NVARCHAR(MAX),
	@MaCachTl NVARCHAR(50),
	@Condition NVARCHAR(50),
	@DonViTinh int,
	@IsOrderChucVu bit = 0 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @Query AS NVARCHAR(MAX)
	SET @Query =
	'
	WITH BangLuongThang AS (
		SELECT
			dsCapNhapBangLuong.Thang	AS Thang,
			dsCapNhapBangLuong.Nam		AS Nam,
			bangLuong.Ma_CBo			AS MaCanBo,
			bangLuong.MA_PHUCAP			AS MaPhuCap,
			bangLuong.GIA_TRI			AS GiaTri
		FROM TL_BangLuong_Thang bangLuong
		JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong
			ON bangLuong.parent = dsCapNhapBangLuong.Id
		WHERE
			bangLuong.Ma_PhuCap IN (SELECT * FROM f_split(''' + @MaPhuCap + '''))
			AND dsCapNhapBangLuong.Ma_CachTL=''' + @MaCachTl + '''
			AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
			AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND dsCapNhapBangLuong.Status=1
	), ThongTinCanBo AS (
		 SELECT
			donVi.Ma_DonVi										AS MaDonVi,
			capBac.Ma_Cb										AS MaCapBac,
			canBo.Ma_CanBo										AS MaCanBo,
			canBo.Ten_CanBo										AS TenCanBo,
			dbo.f_split_empty(canbo.Ten_CanBo)                  AS Ten,
			ISNULL(chucVu.HeSo_Cv, 0)                           AS HSChucVu,
			CONCAT(canBo.So_TaiKhoan, '' '', canBo.Ma_CanBo)	AS SoTaiKhoan,
			ISNULL(FORMAT(canBo.Ngay_NN,''MM/yy''), '''')		AS NgayNhapNgu,
			ISNULL(FORMAT(canBo.Ngay_XN,''MM/yy''), '''')		AS NgayXuatNgu,
			ISNULL(FORMAT(canBo.Ngay_TN,''MM/yy''), '''')		AS NgayTaiNgu
		FROM TL_DM_CanBo_NQ104 canBo
		INNER JOIN TL_DM_DonVi donVi
		  ON canBo.Parent = donVi.Ma_DonVi
		INNER JOIN TL_DM_CapBac_NQ104 capBac
		  ON canBo.Ma_CB104 = capBac.Ma_Cb
		LEFT JOIN TL_DM_ChucVu chucVu
		  ON canBo.Ma_CV=chucVu.Ma_Cv
		WHERE
		  canBo.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
		  AND canBo.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
		  AND donVi.Ma_DonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
	)
	SELECT MaDonVi, MaCapBac, HSChucVu, MaCanBo, TenCanBo, Ten, SoTaiKhoan, NgayNhapNgu, NgayXuatNgu, NgayTaiNgu, ' + @MaPhuCap + ' FROM (
		SELECT
			canBo.MaDonVi,
			canBo.MaCapBac,
			canBo.HSChucVu,
			canBo.MaCanBo,
			canBo.TenCanBo,
			canBo.Ten,
			SoTaiKhoan,
			canBo.NgayNhapNgu,
			canBo.NgayXuatNgu,
			canBo.NgayTaiNgu,
			bangLuong.MaPhuCap,
			CASE
				WHEN bangLuong.MaPhuCap = ''NTN'' THEN bangLuong.GiaTri
				ELSE bangLuong.GiaTri / ' + CAST(@DonViTinh AS VARCHAR(50)) + '
			END AS GiaTri
		FROM BangLuongThang bangLuong
		INNER JOIN ThongTinCanBo canBo
			ON bangLuong.MaCanBo = canBo.MaCanBo
	) x
	PIVOT
	(
		SUM(GiaTri)
		FOR MaPhuCap IN (' + @MaPhuCap + ')
	) pvt WHERE ' + @Condition 
	IF @IsOrderChucVu = 1
	SET @Query = @Query +' ORDER BY HSChucVu , MaCapBac , Ten ';
	ELSE 
	SET @Query = @Query +' ORDER BY MaCapBac , Ten ';
	execute(@Query)
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangluong_truylinh_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TungNH
-- Create date: 09/12/2021
-- Description:	Lấy dữ liệu báo cáo bảng lương tháng
-- =============================================
CREATE PROCEDURE [dbo].[sp_tl_rpt_bangluong_truylinh_nq104]
	@MaDonVi NVARCHAR(max), 
	@Thang int,
	@Nam AS int,
	@IsTruyLinh bit,
	@IsOrderChucVu bit = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @Cols AS NVARCHAR(MAX)
	DECLARE @Query AS NVARCHAR(MAX)

	SET @Cols = 'TLCBTL_TT,TNLCBTL_TT,TLBLCBTL_TT,TLCVCDTL_TT,TNLCVCDTL_TT,TLBLCVCDTL_TT,TTL,NTN,LHT_TT,PCTNVK_TT,HSBL_TT,PCCV_TT,PCTN_TT,PCCOV_TT,TRUYLINHKHAC_SUM,LUONGTHANG_SUM,PHAITRU_SUM,THANHTIEN,TTL_LHT,TTL_PCCV,TTL_PCCOV'
	SET @Query =
	'
	DECLARE @IsTruyLinh bit
	SET @IsTruyLinh = '+ CAST(@IsTruyLinh AS VARCHAR(1)) + ';
	WITH BangLuongThang AS (
		SELECT
			dsCapNhapBangLuong.Thang	AS Thang,
			dsCapNhapBangLuong.Nam		AS Nam,
			bangLuong.Ma_CAN_BO			AS MaCanBo,
			bangLuong.MA_PHU_CAP			AS MaPhuCap,
			bangLuong.GIA_TRI			AS GiaTri
		FROM TL_BangLuong_Thang_Bridge_NQ104 bangLuong
		JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhapBangLuong
			ON bangLuong.parent = dsCapNhapBangLuong.Id
		WHERE
			bangLuong.Ma_Phu_Cap IN (SELECT * FROM f_split(''' + @Cols + '''))
			AND dsCapNhapBangLuong.Ma_CachTL=''CACH5''
			AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
			AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND dsCapNhapBangLuong.Status=1
	), ThongTinCanBo AS (
		SELECT
			donVi.Ma_DonVi		AS MaDonVi,
			donVi.Ten_Donvi		AS TenDonvi,
			canBo.Ma_CanBo		AS MaCanBo,
			canBo.Ten_CanBo		AS TenCanBo,
			dbo.f_split_empty(canbo.Ten_CanBo) AS Ten,
			canBo.Thang_TNN		AS Tnn,
			ISNULL(canBo.ma_cb104, ''0'') AS MaCapBac,
			ISNULL(capBac.Ten_Cb, ''0'') AS CapBac,
			loaiNhom.ten_loai TenLoai,
			loaiNhom.ten_nhom TenNhom,
			capBacLuong.ten_dm CapBacLuong,
			chucVu.Ma AS MaChucVu,
			chucVu.Ten AS TenChucVu,
			CONCAT(canBo.So_TaiKhoan, '' '', canBo.Ma_CanBo) AS Stk,
			ISNULL(FORMAT(canBo.Ngay_NN,''MM/yy''), '''') AS NgayNhapNgu,
			ISNULL(FORMAT(canBo.Ngay_XN,''MM/yy''), '''') AS NgayXuatNgu,
			ISNULL(FORMAT(canBo.Ngay_TN,''MM/yy''), '''') AS NgayTaiNgu
		FROM TL_DM_CanBo_NQ104 canBo
		INNER JOIN TL_DM_DonVi_NQ104 donVi
			ON canBo.Parent=donVi.Ma_DonVi
		LEFT JOIN TL_DM_CapBac_NQ104 capBac
			ON canBo.Ma_CB104=capBac.Ma_Cb


	LEFT JOIN (SELECT
	con.ten_dm ten_loai,
	con.ma_dm loai,
	chau.ten_dm ten_nhom,
	chau.ma_dm nhom,
	cha.loai_doi_tuong loai_doi_tuong,
	chau.xau_noi_ma
FROM TL_DM_CapBac_Luong_NQ104 cha
LEFT JOIN TL_DM_CapBac_Luong_NQ104 con
ON cha.ma_dm = con.ma_dm_cha 
AND cha.loai_doi_tuong = con.loai_doi_tuong 
AND con.xau_noi_ma like cha.xau_noi_ma + ''-%''
AND con.nam = cha.nam
LEFT JOIN TL_DM_CapBac_Luong_NQ104 chau
ON con.ma_dm = chau.ma_dm_cha 
AND con.loai_doi_tuong = chau.loai_doi_tuong 
AND chau.xau_noi_ma like con.xau_noi_ma + ''-%''
AND chau.nam = con.nam
WHERE cha.loai = 0 ) loaiNhom
ON  loaiNhom.loai = canBo.loai
	AND loaiNhom.nhom = canBo.nhom_chuyen_mon 
	AND canBo.loai_doi_tuong in (select * from f_split(loaiNhom.loai_doi_tuong))

LEFT JOIN (SELECT * FROM TL_DM_CapBac_Luong_NQ104 WHERE loai = 3 and nam = ' + CAST(@Nam AS VARCHAR(4)) + ') capBacLuong
ON (
	(
	canBo.loai_doi_tuong IN (''1'',''3.2'',''4'',''5'') 
	AND canBo.loai_doi_tuong IN 
	(SELECT * FROM f_split(capBacLuong.loai_doi_tuong)) 
	)
	OR 
	(
	canBo.loai_doi_tuong NOT IN (''1'',''3.2'',''4'',''5'') 
	AND (capBacLuong.ma_dm_cha = loaiNhom.nhom)  AND capBacLuong.xau_noi_ma like loaiNhom.xau_noi_ma + ''-%''
	)
)
AND capBacLuong.ma_dm = canBo.ma_bac_luong
LEFT JOIN TL_DM_ChucVu_NQ104 chucVu
ON canBo.Ma_CVd104 = chucVu.Ma
WHERE
(canBo.IsDelete = 1 or (canbo.Ma_TangGiam = ''320'' and month(canbo.Ngay_XN) = '+ CAST(@Thang AS VARCHAR(2)) +' and year(canbo.Ngay_XN) = '+ CAST(@Nam AS VARCHAR(4)) +'))
AND canBo.Khong_Luong = 0
AND canBo.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
AND canBo.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
AND capBac.nam = ' + CAST(@Nam AS VARCHAR(4)) + '
AND chucVu.nam = ' + CAST(@Nam AS VARCHAR(4)) + '
)



	SELECT MaDonVi, MaCanBo, TenCanBo, TenNhom, TenLoai, CapBacLuong, Ten, Tnn, MaCapBac, CapBac, MaChucVu, TenChucVu, Stk, NgayNhapNgu, NgayXuatNgu, NgayTaiNgu, ' + @Cols + ' FROM (
		SELECT
			bangLuong.Thang AS Thang,
			bangLuong.Nam AS Nam, 
			canBo.MaDonVi,
			canBo.TenDonVi,
			canBo.MaCanBo,
			canBo.TenCanBo,
			canBo.Ten,
			canBo.MaCapBac,
			canBo.CapBac,
			canBo.MaChucVu,
			canBo.TenChucVu,
			canBo.TenNhom,
			canBo.TenLoai,
			canBo.CapBacLuong,
			canBo.Stk,
			canBo.NgayNhapNgu,
			canBo.NgayXuatNgu,
			canBo.NgayTaiNgu,
			canBo.Tnn,
			bangLuong.GiaTri,
			bangLuong.MaPhuCap
		FROM BangLuongThang bangLuong
		INNER JOIN ThongTinCanBo canBo
			ON bangLuong.MaCanBo = canBo.MaCanBo
	) x
	PIVOT
	(
		SUM(GiaTri)
		FOR MaPhuCap IN (' + @Cols + ')
	) pvt 
	WHERE 
		(@IsTruyLinh = 1 And THANHTIEN > 0) or
		(@IsTruyLinh = 0 And THANHTIEN < 0)'
	IF @IsOrderChucVu = 1
	SET @Query = @Query +' ORDER BY MaCapBac DESC, Ten ASC';
	ELSE 
	SET @Query = @Query +' ORDER BY MaCapBac DESC, Ten ASC';
	execute(@Query)
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_2_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_2_nq104]
@MaDonVi NVARCHAR(max),
@Thang int,
@Nam AS int,
@IsOrderChucVu AS bit = 0,
@IsGiaTriAm AS bit = 0,
@IsCheckedMaHuongLuong AS bit = 0
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;

DECLARE @Cols AS NVARCHAR(MAX)
DECLARE @Query AS NVARCHAR(MAX)

SET @Cols = 'TNLCB_TT,TNLCV_CD_TT,TLCV_CD_TT,TLCB_TT,TLBLCB_TT,TLBLCV_CD_TT,PCCLKHAC_SUM,NTN,LHT_HS,PCTNVK_HS,HSBL_HS,LHT_TT,PCTNVK_TT,HSBL_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCTRA_SUM,PCCOV_TT,PCDACTHU_SUM,PCKHAC_SUM,LUONGTHANG_SUM,BHCN_TT,THUETNCN_TT,TA_TT,GTKHAC_TT,TRICHLUONG_TT,PHAITRU_SUM,TM,THANHTIEN,BHXHCN_TT,BHYTCN_TT,BHTNCN_TT,PCTAUBP_TT,PCBVBG_TT,PCTEMTHU_TT,TA_TONG,PHAITRUKHAC_SUM'
SET @Query =
'
WITH BangLuongThang AS (
SELECT Thang, Nam, MaCanBo, ' + @Cols + ' FROM (
SELECT
dsCapNhapBangLuong.Thang AS Thang,
dsCapNhapBangLuong.Nam AS Nam,
bangLuong.Ma_Can_Bo AS MaCanBo,
bangLuong.MA_PHU_CAP AS MaPhuCap,
bangLuong.GIA_TRI AS GiaTri
FROM TL_BangLuong_Thang_Bridge_NQ104 bangLuong
JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhapBangLuong
ON bangLuong.parent = dsCapNhapBangLuong.Id
WHERE
bangLuong.Ma_Phu_Cap IN (SELECT * FROM f_split(''' + @Cols + '''))
AND dsCapNhapBangLuong.Ma_CachTL=''CACH0''
AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
AND dsCapNhapBangLuong.Status=1
) x
PIVOT
(
SUM(GiaTri)
FOR MaPhuCap IN (' + @Cols + ')
) pvt
), ThongTinCanBo AS (
SELECT
donVi.Ma_DonVi AS MaDonVi,
donVi.Ten_Donvi AS TenDonvi,
canBo.Ma_CanBo AS MaCanBo,
canBo.Ten_CanBo AS TenCanBo,
loaiNhom.ten_loai TenLoai,
loaiNhom.ten_nhom TenNhom,
capBacLuong.ten_dm CapBacLuong,
canBo.lan_nang_luong_cb LanNangLuongCb,
canBo.lan_nang_luong_cvd LanNangLuongCvd,
dbo.f_split_empty(canbo.Ten_CanBo) AS Ten,
canBo.Thang_TNN AS Tnn,
ISNULL(canBo.Ma_CB104, ''0'') AS MaCapBac,
ISNULL(capBac.Ten_Cb, ''0'') AS CapBac,
chucVu.Ma AS MaChucVu,
chucVu.Ten AS TenChucVu,
CASE WHEN 1 = ' + CAST(@IsCheckedMaHuongLuong AS VARCHAR(10)) + ' THEN CONCAT(canBo.So_TaiKhoan, '' '', canBo.Ma_CanBo) ELSE canBo.So_TaiKhoan END AS Stk,
ISNULL(FORMAT(canBo.Ngay_NN,''MM/yy''), '''') AS NgayNhapNgu,
ISNULL(FORMAT(canBo.Ngay_XN,''MM/yy''), '''') AS NgayXuatNgu,
ISNULL(FORMAT(canBo.Ngay_TN,''MM/yy''), '''') AS NgayTaiNgu,
capBac.Xau_Noi_Ma XauNoiMa
FROM TL_DM_CanBo_NQ104 canBo
INNER JOIN TL_DM_DonVi_NQ104 donVi
ON canBo.Parent=donVi.Ma_DonVi
LEFT JOIN TL_DM_CapBac_NQ104 capBac
ON canBo.Ma_CB104 = capBac.Ma_Cb
LEFT JOIN (SELECT
	cha.ten_dm ten_loai,
	cha.loai loai,
	con.ten_dm ten_nhom,
	con.nhom nhom
FROM TL_DM_CapBac_Luong_NQ104 cha
LEFT JOIN TL_DM_CapBac_Luong_NQ104 con
ON cha.loai = con.loai
where cha.loai != ''00'' AND con.loai != ''00'' 
AND cha.nhom = ''00'' AND con.nhom <> ''00''
AND ISNULL(cha.ma_dm, '''') = '''' AND ISNULL(con.ma_dm, '''') = '''') loaiNhom
ON loaiNhom.loai = canBo.loai AND loaiNhom.nhom = canBo.nhom_chuyen_mon
LEFT JOIN TL_DM_CapBac_Luong_NQ104 capBacLuong
ON (
	(
	canBo.loai_doi_tuong IN (''1'',''3.2'',''4'',''5'') 
	AND canBo.loai_doi_tuong IN 
	(SELECT * FROM f_split(capBacLuong.loai_doi_tuong))
	)
	OR 
	(
	canBo.loai_doi_tuong NOT IN (''1'',''3.2'',''4'',''5'') 
	AND (capBacLuong.loai = canBo.loai AND capBacLuong.nhom = canBo.nhom_chuyen_mon)
	)
)
AND capBacLuong.ma_dm = canBo.ma_bac_luong
AND capBacLuong.ma_dm is not null
LEFT JOIN TL_DM_ChucVu_NQ104 chucVu
ON canBo.Ma_CVd104 = chucVu.Ma
WHERE
canBo.IsDelete = 1
AND canBo.Khong_Luong = 0
)

SELECT
bangLuong.*,
canBo.MaDonVi,
canBo.TenDonVi,
canBo.TenCanBo,
canBo.Ten,
canBo.MaCapBac,
canBo.CapBac,
canBo.MaChucVu,
canBo.TenChucVu,
canBo.LanNangLuongCb,
canBo.LanNangLuongCvd,
canBo.TenNhom,
canBo.TenLoai,
canBo.CapBacLuong,
canBo.Stk,
canBo.NgayNhapNgu,
canBo.NgayXuatNgu,
canBo.NgayTaiNgu,
canBo.Tnn,
canBo.XauNoiMa
FROM BangLuongThang bangLuong
INNER JOIN ThongTinCanBo canBo
ON bangLuong.MaCanBo = canBo.MaCanBo'
If @IsGiaTriAm = 1
SET @Query = @Query + ' WHERE bangLuong.THANHTIEN < 0';
IF @IsOrderChucVu = 1
SET @Query = @Query +' ORDER BY MaCapBac DESC, Ten ASC';
ELSE
SET @Query = @Query +' ORDER BY MaCapBac DESC, Ten ASC';
execute(@Query)
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_dong_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_dong_nq104]
@MaDonVi NVARCHAR(max),
@Thang int,
@Nam AS int,
@IsOrderChucVu AS bit = 0,
@lstColumnInclude nvarchar(max),
@lstHeaderInclude nvarchar(max)
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;

DECLARE @Cols AS NVARCHAR(MAX)
DECLARE @Header AS NVARCHAR(MAX)
DECLARE @Query AS NVARCHAR(MAX)

SET @Cols = 'NTN,LHT_HS,PCTNVK_HS,HSBL_HS,LHT_TT,PCTNVK_TT,HSBL_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCTRA_SUM,PCCOV_TT,PCDACTHU_SUM,PCKHAC_SUM,LUONGTHANG_SUM,BHCN_TT,THUETNCN_TT,TA_TT,GTKHAC_TT,TRICHLUONG_TT,PHAITRU_SUM,TM,THANHTIEN,BHXHCN_TT,BHYTCN_TT,BHTNCN_TT,PCTAUBP_TT,PCBVBG_TT,TA_TONG,PHAITRUKHAC_SUM'
SET @Header = 'NTN,LHT_HS,PCTNVK_HS,HSBL_HS,LHT_TT,PCTNVK_TT,HSBL_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCTRA_SUM,PCCOV_TT,PCDACTHU_SUM,PCKHAC_SUM,LUONGTHANG_SUM,BHCN_TT,THUETNCN_TT,TA_TT,GTKHAC_TT,TRICHLUONG_TT,PHAITRU_SUM,TM,THANHTIEN,BHXHCN_TT,BHYTCN_TT,BHTNCN_TT,PCTAUBP_TT,PCBVBG_TT,TA_TONG,PHAITRUKHAC_SUM'

IF(ISNULL(@lstColumnInclude, '') <> '')
BEGIN
	SET @Cols = CONCAT(@Cols, ',', @lstColumnInclude)
	SET @Header = CONCAT(@Header, ',', @lstHeaderInclude)
END

SET @Query =
'
WITH BangLuongThang AS (
SELECT Thang, Nam, MaCanBo, ' + @Header + ' FROM (
SELECT
dsCapNhapBangLuong.Thang AS Thang,
dsCapNhapBangLuong.Nam AS Nam,
bangLuong.Ma_Can_Bo AS MaCanBo,
bangLuong.MA_PHU_CAP AS MaPhuCap,
bangLuong.GIA_TRI AS GiaTri
FROM TL_BangLuong_Thang_Bridge_NQ104 bangLuong
JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhapBangLuong
ON bangLuong.parent = dsCapNhapBangLuong.Id
WHERE
bangLuong.Ma_Phu_Cap IN (SELECT * FROM f_split(''' + @Cols + '''))
AND dsCapNhapBangLuong.Ma_CachTL=''CACH0''
AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
AND dsCapNhapBangLuong.Status=1
) x
PIVOT
(
SUM(GiaTri)
FOR MaPhuCap IN (' + @Cols + ')
) pvt
), ThongTinCanBo AS (
SELECT
donVi.Ma_DonVi AS MaDonVi,
donVi.Ten_Donvi AS TenDonvi,
canBo.Ma_CanBo AS MaCanBo,
canBo.Ten_CanBo AS TenCanBo,
dbo.f_split_empty(canbo.Ten_CanBo) AS Ten,
canBo.Thang_TNN AS Tnn,
ISNULL(canBo.Ma_CB, ''0'') AS MaCapBac,
ISNULL(capBac.Ten_Cb, ''0'') AS CapBac,
ISNULL(chucVu.HeSo_Cv, 0) AS HSChucVu,
chucVu.Ma_Cv AS MaChucVu,
chucVu.Ten_Cv AS TenChucVu,
canBo.So_TaiKhoan AS Stk,
ISNULL(FORMAT(canBo.Ngay_NN,''MM/yy''), '''') AS NgayNhapNgu,
ISNULL(FORMAT(canBo.Ngay_XN,''MM/yy''), '''') AS NgayXuatNgu,
ISNULL(FORMAT(canBo.Ngay_TN,''MM/yy''), '''') AS NgayTaiNgu,
capBac.XauNoiMa
FROM TL_DM_CanBo_NQ104 canBo
INNER JOIN TL_DM_DonVi_NQ104 donVi
ON canBo.Parent=donVi.Ma_DonVi
LEFT JOIN TL_DM_CapBac_NQ104 capBac
ON canBo.Ma_CB104=capBac.Ma_Cb
LEFT JOIN TL_DM_ChucVu chucVu
ON canBo.Ma_CV=chucVu.Ma_Cv
WHERE
canBo.IsDelete = 1
AND canBo.Khong_Luong = 0
)

SELECT
bangLuong.*,
canBo.MaDonVi,
canBo.TenDonVi,
canBo.TenCanBo,
canBo.Ten,
canBo.MaCapBac,
canBo.CapBac,
canBo.HSChucVu,
canBo.MaChucVu,
canBo.TenChucVu,
canBo.Stk,
canBo.NgayNhapNgu,
canBo.NgayXuatNgu,
canBo.NgayTaiNgu,
canBo.Tnn,
canBo.XauNoiMa
FROM BangLuongThang bangLuong
INNER JOIN ThongTinCanBo canBo
ON bangLuong.MaCanBo = canBo.MaCanBo'
IF @IsOrderChucVu = 1
SET @Query = @Query +' ORDER BY HSChucVu DESC, MaCapBac DESC, Ten ASC';
ELSE
SET @Query = @Query +' ORDER BY MaCapBac DESC, Ten ASC';
execute(@Query)
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_nq104]
@MaDonVi NVARCHAR(max),
@Thang int,
@Nam AS int,
@IsOrderChucVu AS bit = 0,
@IsGiaTriAm AS bit = 0,
@IsCheckedMaHuongLuong AS bit = 0,
@IsNew AS bit = 0,
@TyLeHuong AS float
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;

DECLARE @Cols AS NVARCHAR(MAX)
DECLARE @Query AS NVARCHAR(MAX)
DECLARE @ThangTruoc AS int
DECLARE @NamTruoc AS int

IF @Thang = 1 
BEGIN
	SET @ThangTruoc = 12;
	SET @NamTruoc = @Nam - 1;
END
ELSE 
BEGIN
	SET @ThangTruoc =  @Thang - 1;
	SET @NamTruoc = @Nam;
END

SET @Cols = 'LUONGCOBAN_SUM,TNLCB_TT,TNLCV_CD_TT,TLCV_CD_TT,TLCB_TT,TLBLCB_TT,TLBLCV_CD_TT,PCCLKHAC_SUM,NTN,LHT_HS,PCTNVK_HS,HSBL_HS,LHT_TT,PCTNVK_TT,HSBL_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCKVCS_TT,PCTRA_SUM,PCCOV_TT,PCDACTHU_SUM,PCKHAC_SUM,LUONGTHANG_SUM,BHCN_TT,THUETNCN_TT,TA_TT,GTKHAC_TT,TRICHLUONG_TT,PHAITRU_SUM,TM,THANHTIEN,BHXHCN_TT,BHYTCN_TT,BHTNCN_TT,PCTAUBP_TT,PCBVBG_TT,PCTEMTHU_TT,TA_TONG,PHAITRUKHAC_SUM'
SET @Query =
'
WITH CanBoThangTruoc as (
			Select canbo.Ma_Hieu_CanBo, canbo.Parent MaDonViCu, canbo.Ten_DonVi TenDonViCu, canbo.Ma_CB CapBacCu
			From TL_DM_CanBo_NQ104 canbo
			Where 
				canbo.Thang = ' + CAST(@ThangTruoc AS VARCHAR(2)) + '
				And canbo.Nam = ' + CAST(@NamTruoc AS VARCHAR(4)) + '
				AND (canbo.ty_le_huong_nn = ' + CAST(@TyLeHuong AS VARCHAR(4)) + ' or ' + CAST(@TyLeHuong AS VARCHAR(4)) + ' = 0)
				AND canbo.Parent IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		),
blt AS (
	SELECT gia_tri, ma_phu_cap ma_phucap, ma_can_bo ma_cbo, ma_hieu_can_bo Ma_Hieu_CanBo, parent FROM TL_BangLuong_Thang_Bridge_NQ104
	--WHERE THANG = ' + CAST(@Thang AS VARCHAR(2)) + ' 
	--AND NAM = ' + CAST(@Nam AS VARCHAR(4)) + ' 
	--AND Ma_DonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
	WHERE ( ' + CAST(@IsNew AS VARCHAR(2)) + '= 0 OR (ma_hieu_can_bo NOT IN (SELECT Ma_Hieu_CanBo FROM CanBoThangTruoc WHERE MaDonViCu = Ma_Don_Vi)))
),
BangLuongThang AS (
SELECT Thang, Nam, MaCanBo, ' + @Cols + ' FROM (
SELECT
dsCapNhapBangLuong.Thang AS Thang,
dsCapNhapBangLuong.Nam AS Nam,
bangLuong.Ma_CBo AS MaCanBo,
bangLuong.MA_PHUCAP AS MaPhuCap,
bangLuong.GIA_TRI AS GiaTri
FROM blt bangLuong
JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhapBangLuong
ON bangLuong.parent = dsCapNhapBangLuong.Id
WHERE
--bangLuong.Ma_PhuCap IN (SELECT * FROM f_split(''' + @Cols + '''))
 dsCapNhapBangLuong.Ma_CachTL=''CACH0''
AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
AND dsCapNhapBangLuong.Status=1
) x
PIVOT
(
SUM(GiaTri)
FOR MaPhuCap IN (' + @Cols + ')
) pvt
), 
ThongTinCanBo AS (
SELECT
donVi.Ma_DonVi AS MaDonVi,
donVi.Ten_Donvi AS TenDonvi,
canBo.Ma_CanBo AS MaCanBo,
canBo.Ten_CanBo AS TenCanBo,
canBo.lan_nang_luong_cb LanNangLuongCb,
canBo.lan_nang_luong_cvd LanNangLuongCvd,
loaiNhom.ten_loai TenLoai,
loaiNhom.ten_nhom TenNhom,
capBacLuong.ten_dm CapBacLuong,
dbo.f_split_empty(canbo.Ten_CanBo) AS Ten,
canBo.Thang_TNN AS Tnn,
canBo.Nam_TN AS NTN,
dbo.f_luong_ntn(canBo.Ngay_NN, canBo.Ngay_XN, canBo.Ngay_TN, canBo.Thang_TNN, '+ CAST(@Thang AS VARCHAR(2)) +', '+ CAST(@Nam AS VARCHAR(4)) +') AS NamTn,
ISNULL(canBo.Ma_CB104, ''0'') AS MaCapBac,
ISNULL(capBac.Ten_Cb, ''0'') AS CapBac,
--ISNULL(chucVu.HeSo_Cv, 0) AS HSChucVu,
chucVu.Ma AS MaChucVu,
chucVu.Ten AS TenChucVu,
CASE WHEN 1 = ' + CAST(@IsCheckedMaHuongLuong AS VARCHAR(10)) + ' THEN CONCAT(canBo.So_TaiKhoan, '' '', canBo.Ma_CanBo) ELSE canBo.So_TaiKhoan END AS Stk,
ISNULL(FORMAT(canBo.Ngay_NN,''MM/yy''), '''') AS NgayNhapNgu,
ISNULL(FORMAT(canBo.Ngay_XN,''MM/yy''), '''') AS NgayXuatNgu,
ISNULL(FORMAT(canBo.Ngay_TN,''MM/yy''), '''') AS NgayTaiNgu,
capBac.xau_noi_ma XauNoiMa
FROM TL_DM_CanBo_NQ104 canBo
INNER JOIN TL_DM_DonVi_NQ104 donVi
ON canBo.Parent=donVi.Ma_DonVi
LEFT JOIN TL_DM_CapBac_NQ104 capBac
ON canBo.Ma_CB104=capBac.Ma_Cb AND capBac.nam = ' + CAST(@Nam AS VARCHAR(4)) + '
LEFT JOIN (SELECT
	con.ten_dm ten_loai,
	con.ma_dm loai,
	chau.ten_dm ten_nhom,
	chau.ma_dm nhom,
	cha.loai_doi_tuong loai_doi_tuong,
	chau.xau_noi_ma
FROM TL_DM_CapBac_Luong_NQ104 cha
LEFT JOIN TL_DM_CapBac_Luong_NQ104 con
ON cha.ma_dm = con.ma_dm_cha 
AND cha.loai_doi_tuong = con.loai_doi_tuong 
AND con.xau_noi_ma like cha.xau_noi_ma + ''-%''
AND con.nam = cha.nam
LEFT JOIN TL_DM_CapBac_Luong_NQ104 chau
ON con.ma_dm = chau.ma_dm_cha 
AND con.loai_doi_tuong = chau.loai_doi_tuong 
AND chau.xau_noi_ma like con.xau_noi_ma + ''-%''
AND chau.nam = con.nam
WHERE cha.loai = 0 AND con.loai = 1 AND chau.loai = 2) loaiNhom
ON loaiNhom.loai = canBo.loai AND loaiNhom.nhom = canBo.nhom_chuyen_mon 
AND canBo.loai_doi_tuong in (select * from f_split(loaiNhom.loai_doi_tuong))

LEFT JOIN (SELECT * FROM TL_DM_CapBac_Luong_NQ104 WHERE loai = 3 and nam = ' + CAST(@Nam AS VARCHAR(4)) + ') capBacLuong
ON (
	(
	canBo.loai_doi_tuong IN (''1'',''3.2'',''4'',''5'') 
	AND canBo.loai_doi_tuong IN 
	(SELECT * FROM f_split(capBacLuong.loai_doi_tuong)) AND capBacLuong.loai_doi_tuong = loaiNhom.loai_doi_tuong
	)
	OR 
	(
	canBo.loai_doi_tuong NOT IN (''1'',''3.2'',''4'',''5'') 
	AND (capBacLuong.ma_dm_cha = loaiNhom.nhom) AND capBacLuong.loai_doi_tuong = loaiNhom.loai_doi_tuong AND capBacLuong.xau_noi_ma like loaiNhom.xau_noi_ma + ''-%''
	)
)
AND capBacLuong.ma_dm = canBo.ma_bac_luong
LEFT JOIN TL_DM_ChucVu_NQ104 chucVu
ON canBo.Ma_Cvd104 = chucVu.Ma AND chucVu.nam = ' + CAST(@Nam AS VARCHAR(4)) + '
WHERE
canBo.IsDelete = 1
AND (canbo.ty_le_huong_nn = ' + CAST(@TyLeHuong AS VARCHAR(4)) + ' or ' + CAST(@TyLeHuong AS VARCHAR(4)) + ' = 0)
AND canBo.Khong_Luong = 0
--
--
)

SELECT
bangLuong.*,
canBo.MaDonVi,
canBo.TenDonVi,
canBo.TenCanBo,
canBo.Ten,
canBo.MaCapBac,
canBo.CapBac,
--canBo.HSChucVu,
canBo.TenNhom,
canBo.TenLoai,
canBo.CapBacLuong,
canBo.MaChucVu,
canBo.TenChucVu,
canBo.Stk,
canBo.NgayNhapNgu,
canBo.NgayXuatNgu,
canBo.NgayTaiNgu,
canBo.NamTN,
canBo.Tnn,
canBo.NTN,
canBo.LanNangLuongCb,
canBo.LanNangLuongCvd,
canBo.XauNoiMa
FROM BangLuongThang bangLuong
INNER JOIN ThongTinCanBo canBo
ON bangLuong.MaCanBo = canBo.MaCanBo'

If @IsGiaTriAm = 1
SET @Query = @Query + ' WHERE bangLuong.THANHTIEN < 0';
IF @IsOrderChucVu = 1
SET @Query = @Query +' ORDER BY MaCapBac DESC, Ten ASC';
ELSE
SET @Query = @Query +' ORDER BY MaCapBac DESC, Ten ASC';
execute(@Query)
END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh_2_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh_2_nq104]
@MaDonVi NVARCHAR(max),
@Thang int,
@Nam AS int,
@IsOrderChucVu AS bit = 0,
@IsGiaTriAm AS bit = 0,
@IsCheckedMaHuongLuong AS bit = 0
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;

DECLARE @Cols AS NVARCHAR(MAX)
DECLARE @Query AS NVARCHAR(MAX)

SET @Cols = 'TNLCB_TT,TNLCV_CD_TT,TLCV_CD_TT,TLCB_TT,TLBLCB_TT,TLBLCV_CD_TT,PCCLKHAC_SUM,NTN,LHT_HS,PCTNVK_HS,HSBL_HS,LHT_TT,PCTNVK_TT,HSBL_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCTRA_SUM,PCCOV_TT,PCDACTHU_SUM,PCKHAC_SUM,LUONGTHANG_SUM,BHCN_TT,THUETNCN_TT,TA_TT,GTKHAC_TT,TRICHLUONG_TT,PHAITRU_SUM,TM,THANHTIEN,BHXHCN_TT,BHYTCN_TT,BHTNCN_TT,PCTAUBP_TT,PCBVBG_TT,PCTEMTHU_TT,TA_TONG,PHAITRUKHAC_SUM'
SET @Query =
'
WITH 
BangLuongBHXH AS (
	select distinct base.sMaCbo, base.sTenCbo, base.sMaDonVi, base.iNam, base.iThang,
		LHT_TT.GiaTri_LHT_TT,
		PCCV_TT.GiaTri_PCCVBH_TT,
		PCTN_TT.GiaTri_PCTNBH_TT,
		BHXHCN_TT.GiaTri_BHXHCN_TT,
		BHYTCN_TT.GiaTri_BHYTCN_TT
	from TL_BangLuong_ThangBHXH base
	left join (
	select LHT_TT.sMaCbo, LHT_TT.sTenCbo, LHT_TT.sMaDonVi, LHT_TT.iNam, LHT_TT.iThang, sum(nGiaTri) GiaTri_LHT_TT
	from TL_BangLuong_ThangBHXH LHT_TT
	where LHT_TT.iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND LHT_TT.iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND LHT_TT.sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND LHT_TT.sMaCheDo in (''OK_D14N_LBH_TT'', ''OK_T14N_LBH_TT'', ''BDN_D14N_LBH_TT'', ''BDN_T14N_LBH_TT'', ''CONOM_LBH_TT'')
	group by LHT_TT.sMaCbo, LHT_TT.sTenCbo, LHT_TT.sMaDonVi, LHT_TT.iNam, LHT_TT.iThang) LHT_TT on base.sMaCbo = LHT_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCCVBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_PCCVBH_TT'', ''OK_T14N_PCCVBH_TT'', ''BDN_D14N_PCCVBH_TT'', ''BDN_T14N_PCCVBH_TT'', ''CONOM_LBH_PCCVBH_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCCV_TT on base.sMaCbo = PCCV_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCTNBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_PCTNBH_TT'', ''OK_T14N_PCTNBH_TT'', ''BDN_D14N_PCTNBH_TT'', ''BDN_T14N_PCTNBH_TT'', ''CONOM_LBH_PCTNBH_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCTN_TT on base.sMaCbo = PCTN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_BHXHCN_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_BHXHCN_TT'', ''OK_T14N_BHXHCN_TT'', ''BDN_D14N_BHXHCN_TT'', ''BDN_T14N_BHXHCN_TT'', ''CONOM_LBH_BHXHCN_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) BHXHCN_TT on base.sMaCbo = BHXHCN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_BHYTCN_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''BDN_D14N_BHYTCN_TT'', ''OK_D14N_BHYTCN_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) BHYTCN_TT on base.sMaCbo = BHYTCN_TT.sMaCbo

	where base.iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND base.iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND base.sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
),
BangLuongThang AS (
SELECT Thang, Nam, MaCanBo, ' + @Cols + ' FROM (
SELECT
dsCapNhapBangLuong.Thang AS Thang,
dsCapNhapBangLuong.Nam AS Nam,
bangLuong.Ma_Can_Bo AS MaCanBo,
bangLuong.MA_PHU_CAP AS MaPhuCap,
CASE
	WHEN bangLuong.Ma_Phu_Cap = ''LHT_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_LHT_TT, 0)
	WHEN bangLuong.Ma_Phu_Cap = ''PCCV_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_PCCVBH_TT, 0)
	WHEN bangLuong.Ma_Phu_Cap = ''PCTN_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_PCTNBH_TT, 0)
	WHEN bangLuong.Ma_Phu_Cap = ''BHXHCN_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_BHXHCN_TT, 0)
	WHEN bangLuong.Ma_Phu_Cap = ''BHYTCN_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_BHYTCN_TT, 0)
	ELSE bangLuong.Gia_Tri
END AS GiaTri
FROM TL_BangLuong_Thang_Bridge_NQ104 bangLuong
JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhapBangLuong
ON bangLuong.parent = dsCapNhapBangLuong.Id
LEFT JOIN BangLuongBHXH bhxh ON bangLuong.Ma_Can_Bo = bhxh.sMaCbo AND bangLuong.Ma_Don_Vi = bhxh.sMaDonVi
WHERE
bangLuong.Ma_Phu_Cap IN (SELECT * FROM f_split(''' + @Cols + '''))
AND dsCapNhapBangLuong.Ma_CachTL=''CACH0''
AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
AND dsCapNhapBangLuong.Status=1
) x
PIVOT
(
SUM(GiaTri)
FOR MaPhuCap IN (' + @Cols + ')
) pvt
), ThongTinCanBo AS (
SELECT
donVi.Ma_DonVi AS MaDonVi,
donVi.Ten_Donvi AS TenDonvi,
canBo.Ma_CanBo AS MaCanBo,
canBo.Ten_CanBo AS TenCanBo,
dbo.f_split_empty(canbo.Ten_CanBo) AS Ten,
canBo.Thang_TNN AS Tnn,
canBo.lan_nang_luong_cb LanNangLuongCb,
canBo.lan_nang_luong_cvd LanNangLuongCvd,
loaiNhom.ten_loai TenLoai,
loaiNhom.ten_nhom TenNhom,
capBacLuong.ten_dm CapBacLuong,
ISNULL(canBo.Ma_CB104, ''0'') AS MaCapBac,
ISNULL(capBac.Ten_Cb, ''0'') AS CapBac,
chucVu.Ma AS MaChucVu,
chucVu.Ten AS TenChucVu,
CASE WHEN 1 = ' + CAST(@IsCheckedMaHuongLuong AS VARCHAR(10)) + ' THEN CONCAT(canBo.So_TaiKhoan, '' '', canBo.Ma_CanBo) ELSE canBo.So_TaiKhoan END AS Stk,
ISNULL(FORMAT(canBo.Ngay_NN,''MM/yy''), '''') AS NgayNhapNgu,
ISNULL(FORMAT(canBo.Ngay_XN,''MM/yy''), '''') AS NgayXuatNgu,
ISNULL(FORMAT(canBo.Ngay_TN,''MM/yy''), '''') AS NgayTaiNgu,
capBac.xau_noi_ma XauNoiMa
FROM TL_DM_CanBo_NQ104 canBo
INNER JOIN TL_DM_DonVi_NQ104 donVi
ON canBo.Parent=donVi.Ma_DonVi
LEFT JOIN TL_DM_CapBac_NQ104 capBac
ON canBo.Ma_CB104=capBac.Ma_Cb
LEFT JOIN (SELECT
	cha.ten_dm ten_loai,
	cha.loai loai,
	con.ten_dm ten_nhom,
	con.nhom nhom
FROM TL_DM_CapBac_Luong_NQ104 cha
LEFT JOIN TL_DM_CapBac_Luong_NQ104 con
ON cha.loai = con.loai
where cha.loai != ''00'' AND con.loai != ''00'' 
AND cha.nhom = ''00'' AND con.nhom <> ''00''
AND ISNULL(cha.ma_dm, '''') = '''' AND ISNULL(con.ma_dm, '''') = '''') loaiNhom
ON loaiNhom.loai = canBo.loai AND loaiNhom.nhom = canBo.nhom_chuyen_mon
LEFT JOIN TL_DM_CapBac_Luong_NQ104 capBacLuong
ON (
	(
	canBo.loai_doi_tuong IN (''1'',''3.2'',''4'',''5'') 
	AND canBo.loai_doi_tuong IN 
	(SELECT * FROM f_split(capBacLuong.loai_doi_tuong))
	)
	OR 
	(
	canBo.loai_doi_tuong NOT IN (''1'',''3.2'',''4'',''5'') 
	AND (capBacLuong.loai = canBo.loai AND capBacLuong.nhom = canBo.nhom_chuyen_mon)
	)
)
AND capBacLuong.ma_dm = canBo.ma_bac_luong
AND capBacLuong.ma_dm is not null
LEFT JOIN TL_DM_ChucVu_NQ104 chucVu
ON canBo.Ma_CVd104=chucVu.Ma
WHERE
(canBo.IsDelete = 1 or (canbo.Ma_TangGiam = ''320'' and month(canbo.Ngay_XN) = '+ CAST(@Thang AS VARCHAR(2)) +' and year(canbo.Ngay_XN) = '+ CAST(@Nam AS VARCHAR(4)) +'))
AND canBo.Khong_Luong = 0
AND canBo.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
AND canBo.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
)

SELECT
bangLuong.*,
canBo.MaDonVi,
canBo.TenDonVi,
canBo.TenCanBo,
canBo.Ten,
canBo.MaCapBac,
canBo.CapBac,
canBo.MaChucVu,
canBo.TenChucVu,
canBo.LanNangLuongCb,
canBo.LanNangLuongCvd,
canBo.TenNhom,
canBo.TenLoai,
canBo.CapBacLuong,
canBo.Stk,
canBo.NgayNhapNgu,
canBo.NgayXuatNgu,
canBo.NgayTaiNgu,
canBo.Tnn,
canBo.XauNoiMa
FROM BangLuongThang bangLuong
INNER JOIN ThongTinCanBo canBo
ON bangLuong.MaCanBo = canBo.MaCanBo'
If @IsGiaTriAm = 1
SET @Query = @Query + ' WHERE bangLuong.THANHTIEN < 0';
IF @IsOrderChucVu = 1
SET @Query = @Query +' ORDER BY MaCapBac DESC, Ten ASC';
ELSE
SET @Query = @Query +' ORDER BY MaCapBac DESC, Ten ASC';
execute(@Query)
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh_nq104]
@MaDonVi NVARCHAR(max),
@Thang int,
@Nam AS int,
@IsOrderChucVu AS bit = 0,
@IsGiaTriAm AS bit = 0,
@IsCheckedMaHuongLuong AS bit = 0,
@IsNew AS bit = 0,
@TyLeHuong AS float
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;

DECLARE @Cols AS NVARCHAR(MAX)
DECLARE @Query AS NVARCHAR(MAX)
DECLARE @ThangTruoc AS int
DECLARE @NamTruoc AS int

IF @Thang = 1 
BEGIN
	SET @ThangTruoc = 12;
	SET @NamTruoc = @Nam - 1;
END
ELSE 
BEGIN
	SET @ThangTruoc =  @Thang - 1;
	SET @NamTruoc = @Nam;
END

SET @Cols = 'LUONGCOBAN_SUM,TNLCB_TT,TNLCV_CD_TT,TLCV_CD_TT,TLCB_TT,TLBLCB_TT,TLBLCV_CD_TT,PCCLKHAC_SUM,NTN,LHT_HS,PCTNVK_HS,HSBL_HS,LHT_TT,PCTNVK_TT,HSBL_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCKVCS_TT,PCTRA_SUM,PCCOV_TT,PCDACTHU_SUM,PCKHAC_SUM,LUONGTHANG_SUM,BHCN_TT,THUETNCN_TT,TA_TT,GTKHAC_TT,TRICHLUONG_TT,PHAITRU_SUM,TM,THANHTIEN,BHXHCN_TT,BHYTCN_TT,BHTNCN_TT,PCTAUBP_TT,PCBVBG_TT,PCTEMTHU_TT,TA_TONG,PHAITRUKHAC_SUM'
SET @Query =
'
WITH CanBoThangTruoc as (
			Select canbo.Ma_Hieu_CanBo, canbo.Parent MaDonViCu, canbo.Ten_DonVi TenDonViCu, canbo.Ma_CB CapBacCu
			From TL_DM_CanBo_NQ104 canbo
			Where 
				canbo.Thang = ' + CAST(@ThangTruoc AS VARCHAR(2)) + '
				And canbo.Nam = ' + CAST(@NamTruoc AS VARCHAR(4)) + '
				AND (canbo.ty_le_huong_nn = ' + CAST(@TyLeHuong AS VARCHAR(4)) + ' or ' + CAST(@TyLeHuong AS VARCHAR(4)) + ' = 0)
				AND canbo.Parent IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		),
blt AS (
	SELECT * FROM TL_BangLuong_Thang_Bridge_NQ104
	WHERE Ma_Don_Vi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
	AND ( ' + CAST(@IsNew AS VARCHAR(2)) + '= 0 OR (Ma_Hieu_Can_Bo NOT IN (SELECT Ma_Hieu_CanBo FROM CanBoThangTruoc WHERE MaDonViCu = Ma_Don_Vi)))
),
BangLuongBHXH AS (
	select distinct base.sMaCbo, base.sTenCbo, base.sMaDonVi, base.iNam, base.iThang,
		LHT_TT.GiaTri_LHT_TT,
		PCCV_TT.GiaTri_PCCVBH_TT,
		PCTN_TT.GiaTri_PCTNBH_TT,
		BHXHCN_TT.GiaTri_BHXHCN_TT,
		BHYTCN_TT.GiaTri_BHYTCN_TT
	from TL_BangLuong_ThangBHXH base
	left join (
	select LHT_TT.sMaCbo, LHT_TT.sTenCbo, LHT_TT.sMaDonVi, LHT_TT.iNam, LHT_TT.iThang, sum(nGiaTri) GiaTri_LHT_TT
	from TL_BangLuong_ThangBHXH LHT_TT
	where LHT_TT.iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND LHT_TT.iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND LHT_TT.sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND LHT_TT.sMaCheDo in (''OK_D14N_LBH_TT'', ''OK_T14N_LBH_TT'', ''BDN_D14N_LBH_TT'', ''BDN_T14N_LBH_TT'', ''CONOM_LBH_TT'')
	group by LHT_TT.sMaCbo, LHT_TT.sTenCbo, LHT_TT.sMaDonVi, LHT_TT.iNam, LHT_TT.iThang) LHT_TT on base.sMaCbo = LHT_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCCVBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_PCCVBH_TT'', ''OK_T14N_PCCVBH_TT'', ''BDN_D14N_PCCVBH_TT'', ''BDN_T14N_PCCVBH_TT'', ''CONOM_LBH_PCCVBH_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCCV_TT on base.sMaCbo = PCCV_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCTNBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_PCTNBH_TT'', ''OK_T14N_PCTNBH_TT'', ''BDN_D14N_PCTNBH_TT'', ''BDN_T14N_PCTNBH_TT'', ''CONOM_LBH_PCTNBH_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCTN_TT on base.sMaCbo = PCTN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_BHXHCN_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_BHXHCN_TT'', ''OK_T14N_BHXHCN_TT'', ''BDN_D14N_BHXHCN_TT'', ''BDN_T14N_BHXHCN_TT'', ''CONOM_LBH_BHXHCN_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) BHXHCN_TT on base.sMaCbo = BHXHCN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_BHYTCN_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''BDN_D14N_BHYTCN_TT'', ''OK_D14N_BHYTCN_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) BHYTCN_TT on base.sMaCbo = BHYTCN_TT.sMaCbo

	where base.iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND base.iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND base.sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
),
BangLuongThang AS (
SELECT Thang, Nam, MaCanBo, ' + @Cols + ' FROM (
SELECT
dsCapNhapBangLuong.Thang AS Thang,
dsCapNhapBangLuong.Nam AS Nam,
bangLuong.Ma_Can_Bo AS MaCanBo,
bangLuong.MA_PHU_CAP AS MaPhuCap,
CASE
	WHEN bangLuong.Ma_Phu_Cap = ''LHT_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_LHT_TT, 0)
	WHEN bangLuong.Ma_Phu_Cap = ''PCCV_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_PCCVBH_TT, 0)
	WHEN bangLuong.Ma_Phu_Cap = ''PCTN_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_PCTNBH_TT, 0)
	WHEN bangLuong.Ma_Phu_Cap = ''BHXHCN_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_BHXHCN_TT, 0)
	WHEN bangLuong.Ma_Phu_Cap = ''BHYTCN_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_BHYTCN_TT, 0)
	ELSE bangLuong.Gia_Tri
END AS GiaTri
FROM blt bangLuong
JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhapBangLuong
ON bangLuong.parent = dsCapNhapBangLuong.Id
LEFT JOIN BangLuongBHXH bhxh ON bangLuong.Ma_Can_Bo = bhxh.sMaCbo AND bangLuong.Ma_Don_Vi = bhxh.sMaDonVi
WHERE
bangLuong.Ma_Phu_Cap IN (SELECT * FROM f_split(''' + @Cols + '''))
AND dsCapNhapBangLuong.Ma_CachTL=''CACH0''
AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
AND dsCapNhapBangLuong.Status=1
) x
PIVOT
(
SUM(GiaTri)
FOR MaPhuCap IN (' + @Cols + ')
) pvt
), ThongTinCanBo AS (
SELECT
donVi.Ma_DonVi AS MaDonVi,
donVi.Ten_Donvi AS TenDonvi,
canBo.Ma_CanBo AS MaCanBo,
canBo.Ten_CanBo AS TenCanBo,
dbo.f_split_empty(canbo.Ten_CanBo) AS Ten,
loaiNhom.ten_loai TenLoai,
loaiNhom.ten_nhom TenNhom,
capBacLuong.ten_dm CapBacLuong,
canBo.lan_nang_luong_cb LanNangLuongCb,
canBo.lan_nang_luong_cvd LanNangLuongCvd,
canBo.Thang_TNN AS Tnn,
canBo.Nam_TN AS NTN,
dbo.f_luong_ntn(canBo.Ngay_NN, canBo.Ngay_XN, canBo.Ngay_TN, canBo.Thang_TNN, '+ CAST(@Thang AS VARCHAR(2)) +', '+ CAST(@Nam AS VARCHAR(4)) +') AS NamTn,
ISNULL(canBo.Ma_CB104, ''0'') AS MaCapBac,
ISNULL(capBac.Ten_Cb, ''0'') AS CapBac,
chucVu.Ma AS MaChucVu,
chucVu.Ten AS TenChucVu,
CASE WHEN 1 = ' + CAST(@IsCheckedMaHuongLuong AS VARCHAR(10)) + ' THEN CONCAT(canBo.So_TaiKhoan, '' '', canBo.Ma_CanBo) ELSE canBo.So_TaiKhoan END AS Stk,
ISNULL(FORMAT(canBo.Ngay_NN,''MM/yy''), '''') AS NgayNhapNgu,
ISNULL(FORMAT(canBo.Ngay_XN,''MM/yy''), '''') AS NgayXuatNgu,
ISNULL(FORMAT(canBo.Ngay_TN,''MM/yy''), '''') AS NgayTaiNgu,
capBac.Xau_Noi_Ma XauNoiMa
FROM TL_DM_CanBo_NQ104 canBo
INNER JOIN TL_DM_DonVi_NQ104 donVi
ON canBo.Parent = donVi.Ma_DonVi
LEFT JOIN TL_DM_CapBac_NQ104 capBac
ON canBo.Ma_CB104 = capBac.Ma_Cb

LEFT JOIN (SELECT
	con.ten_dm ten_loai,
	con.ma_dm loai,
	chau.ten_dm ten_nhom,
	chau.ma_dm nhom,
	cha.loai_doi_tuong loai_doi_tuong,
	chau.xau_noi_ma
FROM TL_DM_CapBac_Luong_NQ104 cha
LEFT JOIN TL_DM_CapBac_Luong_NQ104 con
ON cha.ma_dm = con.ma_dm_cha 
AND cha.loai_doi_tuong = con.loai_doi_tuong 
AND con.xau_noi_ma like cha.xau_noi_ma + ''-%''
AND con.nam = cha.nam
LEFT JOIN TL_DM_CapBac_Luong_NQ104 chau
ON con.ma_dm = chau.ma_dm_cha 
AND con.loai_doi_tuong = chau.loai_doi_tuong 
AND chau.xau_noi_ma like con.xau_noi_ma + ''-%''
AND chau.nam = con.nam
WHERE cha.loai = 0 AND con.loai = 1 AND chau.loai = 2) loaiNhom
ON loaiNhom.loai = canBo.loai AND loaiNhom.nhom = canBo.nhom_chuyen_mon 
AND canBo.loai_doi_tuong in (select * from f_split(loaiNhom.loai_doi_tuong))

LEFT JOIN (SELECT * FROM TL_DM_CapBac_Luong_NQ104 WHERE loai = 3 and nam = ' + CAST(@Nam AS VARCHAR(4)) + ') capBacLuong
ON (
	(
	canBo.loai_doi_tuong IN (''1'',''3.2'',''4'',''5'') 
	AND canBo.loai_doi_tuong IN 
	(SELECT * FROM f_split(capBacLuong.loai_doi_tuong)) AND capBacLuong.loai_doi_tuong = loaiNhom.loai_doi_tuong
	)
	OR 
	(
	canBo.loai_doi_tuong NOT IN (''1'',''3.2'',''4'',''5'') 
	AND (capBacLuong.ma_dm_cha = loaiNhom.nhom) AND capBacLuong.loai_doi_tuong = loaiNhom.loai_doi_tuong AND capBacLuong.xau_noi_ma like loaiNhom.xau_noi_ma + ''-%''
	)
)
AND capBacLuong.ma_dm = canBo.ma_bac_luong
LEFT JOIN TL_DM_ChucVu_NQ104 chucVu
ON canBo.Ma_CVd104 = chucVu.Ma
WHERE
(canBo.IsDelete = 1 or (canbo.Ma_TangGiam = ''320'' and month(canbo.Ngay_XN) = '+ CAST(@Thang AS VARCHAR(2)) +' and year(canbo.Ngay_XN) = '+ CAST(@Nam AS VARCHAR(4)) +'))
AND canBo.Khong_Luong = 0
AND (canbo.ty_le_huong_nn = ' + CAST(@TyLeHuong AS VARCHAR(4)) + ' or ' + CAST(@TyLeHuong AS VARCHAR(4)) + ' = 0)
AND canBo.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
AND canBo.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
AND capBac.nam = ' + CAST(@Nam AS VARCHAR(4)) + '
AND chucVu.nam = ' + CAST(@Nam AS VARCHAR(4)) + '
)

SELECT
bangLuong.*,
canBo.MaDonVi,
canBo.TenDonVi,
canBo.TenCanBo,
canBo.Ten,
canBo.MaCapBac,
canBo.CapBac,
canBo.LanNangLuongCb,
canBo.LanNangLuongCvd,
canBo.TenNhom,
canBo.TenLoai,
canBo.CapBacLuong,
canBo.MaChucVu,
canBo.TenChucVu,
canBo.Stk,
canBo.NgayNhapNgu,
canBo.NgayXuatNgu,
canBo.NgayTaiNgu,
canBo.NamTN,
canBo.Tnn,
canBo.NTN,
canBo.XauNoiMa
FROM BangLuongThang bangLuong
INNER JOIN ThongTinCanBo canBo
ON bangLuong.MaCanBo = canBo.MaCanBo'
If @IsGiaTriAm = 1
SET @Query = @Query + ' WHERE bangLuong.THANHTIEN < 0';
IF @IsOrderChucVu = 1
SET @Query = @Query +' ORDER BY MaCapBac DESC, Ten ASC';
ELSE
SET @Query = @Query +' ORDER BY MaCapBac DESC, Ten ASC';
execute(@Query)
END
;
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_truylinhchuyenchedo_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_truylinhchuyenchedo_nq104]
	@Thang int,
	@Nam int,
	@MaDonVi nvarchar(max),
	@MaCanBo nvarchar(max)
as
begin
	SET NOCOUNT ON;

    DECLARE @Cols AS NVARCHAR(MAX)
	DECLARE @Query AS NVARCHAR(MAX)

	SET @Cols = STUFF(
	(
    SELECT
		DISTINCT ',' + QUOTENAME(phucap.Ma_PhuCap) 
    FROM TL_DM_PhuCap_NQ104 phucap
        FOR XML PATH(''), TYPE
	).value('.', 'NVARCHAR(MAX)'),1,1,'')

	SET @Query =
	'SELECT MaCanBo, TenCanBo, MaCapBac, CapBac, HSChucVu, MaChucVu, TenChucVu, Stk, NgayNhapNgu, NgayXuatNgu, NgayTaiNgu, ' + @Cols + ' FROM (
		SELECT
			bangLuong.Thang AS Thang,
			bangLuong.Nam AS Nam, 
			donVi.Ten_Donvi TenDonVi,
			canBo.Ma_CanBo MaCanBo,
			canBo.Ten_CanBo AS TenCanBo,
			ISNULL(canBo.Ma_CB, ''0'') AS MaCapBac,
			ISNULL(capBac.Ten_Cb, ''0'') AS CapBac,
			ISNULL(chucVu.HeSo_Cv, 0) AS HSChucVu,
			chucVu.Ma_Cv AS MaChucVu,
			chucVu.Ten_Cv AS TenChucVu,
			canBo.So_TaiKhoan AS Stk,
			ISNULL(FORMAT(canBo.Ngay_NN,''MM/yy''), '''') AS NgayNhapNgu,
			ISNULL(FORMAT(canBo.Ngay_XN,''MM/yy''), '''') AS NgayXuatNgu,
			ISNULL(FORMAT(canBo.Ngay_TN,''MM/yy''), '''') AS NgayTaiNgu,
			bangLuong.GIA_TRI AS GiaTri,
			bangLuong.MA_PHUCAP AS MaPhuCap
		FROM TL_BangLuong_Thang_NQ104 bangLuong
		JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhapBangLuong ON bangLuong.parent=dsCapNhapBangLuong.Id
		JOIN TL_DM_CanBo_NQ104 canBo ON bangLuong.Ma_CBo=canBo.Ma_CanBo
		LEFT JOIN TL_DM_CapBac_NQ104 capBac ON canBo.Ma_CB104=capBac.Ma_Cb
		LEFT JOIN TL_DM_ChucVu chucVu ON canBo.Ma_CV=chucVu.Ma_Cv
		JOIN TL_DM_DonVi_NQ104 donVi ON bangLuong.Ma_DonVi=donVi.Ma_DonVi
		WHERE
			dsCapNhapBangLuong.Ma_CachTL=''CACH0''
			AND dsCapNhapBangLuong.Ma_CBo IN (''' + @MaDonVi + ''')
			AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND dsCapNhapBangLuong.Status=1
			AND canBo.IsDelete=1
			AND canBo.Khong_Luong=0
			AND bangLuong.Ma_CBo in (''' + @MaCanBo + ''')
	) x
	PIVOT
	(
		SUM(GiaTri)
		FOR MaPhuCap IN (' + @Cols + ')
	) pvt ORDER BY HSChucVu DESC, MaCapBac DESC, TenCanBo DESC'
	execute(@Query)
end;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_chitiet_quanso_tanggiam_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_chitiet_quanso_tanggiam_nq104]
	@thang int, @nam int, @thangTruoc int, @namTruoc int, @maDonVi nvarchar(MAX), @sM nvarchar(1)
As
Begin
	if @sM = '3'
		-- giảm
		With CanBoThangTruoc as (
			Select canbo.Ma_Hieu_CanBo, canbo.Parent MaDonViCu, canbo.Ten_DonVi TenDonViCu, canbo.Ma_CB CapBacCu
			From TL_DM_CanBo_NQ104 canbo
			Where 
				canbo.Thang = @thangTruoc
				And canbo.Nam = @namTruoc
		)

		Select 
			canbo.Ten_CanBo TenCanBo,
			CASE 
				WHEN canbo.Ma_TangGiam in ('250', '280') THEN canbothangtruoc.CapBacCu
				ELSE canbo.Ma_CB
			END CapBac,
			CAST('1' as int) as SoLuong,
			CASE 
				WHEN canbo.Ma_TangGiam in ('290') THEN canbothangtruoc.TenDonViCu
				ELSE canbo.Ten_DonVi
			END DonVi,
			CASE 
				WHEN canbo.Ma_TangGiam in ('290') THEN N'Giảm nội bộ'
				ELSE mlqs.sMoTa
			END NoiDung
		From TL_DM_CanBo_NQ104 canbo
			Join NS_QS_MucLuc mlqs On canbo.Ma_TangGiam = mlqs.sKyHieu
			Join TL_DM_DonVi_NQ104 donvi on canbo.Parent = donvi.Ma_DonVi
			Join CanBoThangTruoc canbothangtruoc on canbo.Ma_Hieu_CanBo = canbothangtruoc.Ma_Hieu_CanBo
		Where canbo.Thang = @thang
			And canbo.Nam = @nam
			--And (sM = @sM OR canbo.Ma_TangGiam in ('250', '280', '290'))
			And (sM = @sM OR canbo.Ma_TangGiam in ('250', '280') OR (canbo.Ma_TangGiam in ('290') and canbo.ParentOld IN (SELECT * FROM f_split(@maDonVi))))
			And iNamLamViec = @nam
			And bHangCha = 0
			And (canbo.Parent IN (SELECT * FROM f_split(@maDonVi)) OR (canbo.Ma_TangGiam in ('290') and canbo.ParentOld IN (SELECT * FROM f_split(@maDonVi))))
		Order By Ma_DonVi, CapBac
	else
		-- tăng
		With CanBoThangTruoc as (
			Select canbo.Ma_Hieu_CanBo, canbo.Parent MaDonViCu, canbo.Ten_DonVi TenDonViCu, canbo.Ma_CB CapBacCu
			From TL_DM_CanBo_NQ104 canbo
			Where 
				canbo.Thang = @thangTruoc
				And canbo.Nam = @namTruoc
		),

		KhongTuyenQuan as (
			Select 
				canbo.Ten_CanBo TenCanBo,
				
				CASE 
					WHEN canbo.Ma_TangGiam in ('350', '380') THEN canbothangtruoc.CapBacCu
					ELSE canbo.Ma_CB
				END CapBac,
				CAST('1' as int) as SoLuong,
				CASE
					WHEN canbo.Ma_TangGiam in ('390') THEN canbothangtruoc.TenDonViCu
					ELSE canbo.Ten_DonVi
				END DonVi,
			mlqs.sMoTa NoiDung
			From TL_DM_CanBo_NQ104 canbo
				Join NS_QS_MucLuc mlqs On canbo.Ma_TangGiam = mlqs.sKyHieu
				Join TL_DM_DonVi_NQ104 donvi on canbo.Parent = donvi.Ma_DonVi
				left Join CanBoThangTruoc canbothangtruoc on canbo.Ma_Hieu_CanBo = canbothangtruoc.Ma_Hieu_CanBo
			Where canbo.Thang = @thang
				And canbo.Nam = @nam
				--And (sM = @sM OR canbo.Ma_TangGiam in ('350','380', '390'))
				And (sM = @sM OR canbo.Ma_TangGiam in ('350','380'))
				And iNamLamViec = @nam
				And bHangCha = 0
				And canbo.Parent IN (SELECT * FROM f_split(@maDonVi))
				And canbo.Ma_TangGiam not in ('210', '220')
				),

		TuyenQuan as (
		Select 
			(CAST(COUNT(*) as nvarchar(MAX)) + N' đồng chí') as TenCanBo, 
			canbo.Ma_CB CapBac, 
			COUNT(*) SoLuong,
			donvi.Ten_DonVi DonVi, 
			mlqs.sMoTa NoiDung
			From TL_DM_CanBo_NQ104 canbo
				Join NS_QS_MucLuc mlqs On canbo.Ma_TangGiam = mlqs.sKyHieu
				Join TL_DM_DonVi_NQ104 donvi on canbo.Parent = donvi.Ma_DonVi
			Where canbo.Thang = @thang
				And canbo.Nam = @nam
				And sM = @sM
				And iNamLamViec = @nam
				And bHangCha = 0
				And canbo.Parent IN (SELECT * FROM f_split(@maDonVi))
				And canbo.Ma_TangGiam in ('210', '220')
			Group By canbo.Ma_CB, donvi.Ten_DonVi, mlqs.sMoTa
		)

		Select *
		From KhongTuyenQuan
		Union
		Select *
		From TuyenQuan
		--Order By DonVi, CapBac desc
		Order By DonVi, CapBac
End
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_danhsach_capphat_phucap_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TungNH
-- Create date: 09/12/2021
-- Description:	Lấy dữ liệu báo cáo bảng lương tháng
-- =============================================
CREATE PROCEDURE [dbo].[sp_tl_rpt_danhsach_capphat_phucap_nq104]
	@MaDonVi NVARCHAR(max), 
	@Thang int,
	@Nam AS int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @Cols AS NVARCHAR(MAX)
	DECLARE @Query AS NVARCHAR(MAX)

	SET @Cols = 'TNLCB_TT,TNLCV_CD_TT,TLCV_CD_TT,TLCB_TT,TLBLCB_TT,TLBLCV_CD_TT,NTN,LHT_HS,LHT_TT,PCCV_TT,PCTHD_TT,PCKV_TT,PCKVCS_TT,PCCOV_TT,PCDACTHU_SUM,PCKHAC_SUM,PCTRA_SUM,LUONGTHANG_SUM,PHAITRU_SUM,TM,THANHTIEN,PCNU_TT,HSBL_HS,PCTNVK_HS'
	SET @Query =
	'
	WITH blt AS (
		SELECT * FROM TL_BangLuong_Thang_Bridge_NQ104
		WHERE Ma_Don_Vi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
	), 
	BangLuongThang AS (
		SELECT
			dsCapNhapBangLuong.Thang	AS Thang,
			dsCapNhapBangLuong.Nam		AS Nam,
			bangLuong.Ma_Can_Bo			AS MaCanBo,
			bangLuong.MA_PHU_CAP			AS MaPhuCap,
			bangLuong.GIA_TRI			AS GiaTri
		FROM blt bangLuong
		JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhapBangLuong
			ON bangLuong.parent = dsCapNhapBangLuong.Id
		WHERE
			bangLuong.Ma_Phu_Cap IN (SELECT * FROM f_split(''' + @Cols + '''))
			AND dsCapNhapBangLuong.Ma_CachTL=''CACH0''
			AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
			AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND dsCapNhapBangLuong.Status=1
	), ThongTinCanBo AS (
		SELECT
			donVi.Ma_DonVi		AS MaDonVi,
			donVi.Ten_Donvi		AS TenDonvi,
			canBo.Ma_CanBo		AS MaCanBo,
			canBo.Ten_CanBo		AS TenCanBo,
			dbo.f_split_empty(canbo.Ten_CanBo) AS Ten,
			canBo.Thang_TNN		AS Tnn,
			ISNULL(canBo.Ma_CB104, ''0'') AS MaCapBac,
			ISNULL(capBac.Ten_Cb, ''0'') AS CapBac,
			chucVu.Ma AS MaChucVu,
			chucVu.Ten AS TenChucVu,
			CONCAT(canBo.So_TaiKhoan, '' '', canBo.Ma_CanBo) AS Stk,
			ISNULL(FORMAT(canBo.Ngay_NN,''MM/yy''), '''') AS NgayNhapNgu,
			ISNULL(FORMAT(canBo.Ngay_XN,''MM/yy''), '''') AS NgayXuatNgu,
			ISNULL(FORMAT(canBo.Ngay_TN,''MM/yy''), '''') AS NgayTaiNgu,
			canBo.Ngay_NN AS NgayNhapNguDate,
			canBo.Ngay_XN AS NgayXuatNguDate,
			canBo.Ngay_TN AS NgayTaiNguDate,
			CASE WHEN canBo.Thang_TNN IS NULL THEN 0 ELSE canBo.Thang_TNN END AS ThangTnn,
			capBac.Xau_Noi_Ma XauNoiMa
		FROM TL_DM_CanBo_NQ104 canBo
		INNER JOIN TL_DM_DonVi donVi
			ON canBo.Parent=donVi.Ma_DonVi
		LEFT JOIN TL_DM_CapBac_NQ104 capBac
			ON canBo.Ma_CB104 = capBac.Ma_Cb
		LEFT JOIN TL_DM_ChucVu_NQ104 chucVu
			ON canBo.Ma_Cvd104 =chucVu.Ma
		WHERE
			(canBo.IsDelete = 1 or (canbo.Ma_TangGiam = ''320'' and month(canbo.Ngay_XN) = '+ CAST(@Thang AS VARCHAR(2)) +' and year(canbo.Ngay_XN) = '+ CAST(@Nam AS VARCHAR(4)) +'))
			AND canBo.Khong_Luong = 0
			AND canBo.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND canBo.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
	)
	SELECT MaDonVi, MaCanBo, TenCanBo, Ten, Tnn, MaCapBac, CapBac, MaChucVu, TenChucVu, Stk, NgayNhapNgu, NgayXuatNgu, NgayTaiNgu, ThangTnn, XauNoiMa, ' + @Cols + ' FROM (
		SELECT
			bangLuong.Thang AS Thang,
			bangLuong.Nam AS Nam, 
			canBo.MaDonVi,
			canBo.TenDonVi,
			canBo.MaCanBo,
			canBo.TenCanBo,
		    canBo.Ten,
			canBo.MaCapBac,
			canBo.CapBac,
			canBo.MaChucVu,
			canBo.TenChucVu,
			canBo.Stk,
			canBo.NgayNhapNgu,
			canBo.NgayXuatNgu,
			canBo.NgayTaiNgu,
			canBo.NgayNhapNguDate,
			canBo.NgayXuatNguDate,
			canBo.NgayTaiNguDate,
			canBo.ThangTnn,
			canBo.Tnn,
			CASE WHEN bangLuong.MaPhuCap = ' + '''NTN''' + ' THEN dbo.f_luong_ntn(canBo.NgayNhapNguDate, canBo.NgayXuatNguDate, canBo.NgayTaiNguDate, canBo.ThangTnn, '+ CAST(@Thang AS VARCHAR(2)) +', '+ CAST(@Nam AS VARCHAR(4)) +') ELSE bangLuong.GiaTri END AS GiaTri,
			bangLuong.MaPhuCap,
			canBo.XauNoiMa
		FROM BangLuongThang bangLuong
		INNER JOIN ThongTinCanBo canBo
			ON bangLuong.MaCanBo = canBo.MaCanBo
	) x
	PIVOT
	(
		SUM(GiaTri)
		FOR MaPhuCap IN (' + @Cols + ')
	) pvt
	WHERE MaCapBac LIKE ''0%''
	ORDER BY MaCapBac DESC, Ten ASC'
	execute(@Query)
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_danhsach_chitra_nganhang_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TungNH
-- Create date: 23/04/2022
-- Description:	Lấy dữ liệu cho báo cáo chi trả cá nhân
-- =============================================
CREATE PROCEDURE [dbo].[sp_tl_rpt_danhsach_chitra_nganhang_nq104] 
	@Thang int,
	@Nam int,
	@MaDonVi NVARCHAR(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    WITH BangLuongThang AS (
		SELECT
			bangLuongThang.ma_can_bo	AS MaCanBo,
			SUM(ISNULL(bangLuongThang.Gia_Tri, 0))	AS GiaTri
		FROM TL_BangLuong_Thang_Bridge_NQ104 bangLuongThang
		JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhapBangLuong
			ON bangLuongThang.parent = dsCapNhapBangLuong.Id
		WHERE
			bangLuongThang.ma_phu_cap = 'THANHTIEN'
			AND dsCapNhapBangLuong.Ma_CachTL in ('CACH0', 'CACH5')
			AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(@MaDonVi))
			AND dsCapNhapBangLuong.Thang=@Thang
			AND dsCapNhapBangLuong.Nam=@Nam
			AND dsCapNhapBangLuong.Status=1
		GROUP BY bangLuongThang.ma_can_bo
	), ThongTinCanBo AS (
		SELECT
			donVi.Ma_DonVi		AS MaDonvi,
			canBo.Ma_CanBo		AS MaCanBo,
			canBo.Ten_CanBo		AS TenCanBo,
			canBo.So_TaiKhoan	AS SoTaiKhoan,
			--ISNULL(chucVu.HeSo_Cv, 0) AS HSChucVu,
			ISNULL(canBo.ma_cb104, '0') AS MaCapBac
		FROM TL_DM_CanBo_NQ104 canBo
		INNER JOIN TL_DM_DonVi_NQ104 donVi
			ON canBo.Parent=donVi.Ma_DonVi
		LEFT JOIN TL_DM_ChucVu_NQ104 chucVu
			ON canBo.ma_cvd104=chucVu.ma AND chucVu.nam=@Nam
		WHERE
			canBo.IsDelete = 1
			AND canBo.Khong_Luong = 0
			AND canBo.Thang=@Thang
			AND canBo.Nam=@Nam
			AND canBo.TM = 1
	)

	SELECT
		canBo.MaDonvi,
		canBo.MaCanBo,
		canBo.TenCanBo,
		canBo.SoTaiKhoan,
		bangLuongThang.GiaTri
	FROM BangLuongThang bangLuongThang
	INNER JOIN ThongTinCanBo canBo
		ON bangLuongThang.MaCanBo = canBo.MaCanBo
	ORDER BY MaCapBac DESC, TenCanBo ASC
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_dienbien_luong_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_dienbien_luong_nq104]
	@MaHieuCanBo VARCHAR(127),
	@TuNgay datetime,
	@DenNgay datetime
AS
BEGIN
	SET NOCOUNT ON;

    DECLARE @Cols AS NVARCHAR(MAX)
	DECLARE @Query AS NVARCHAR(MAX)

	SET @Cols = STUFF(
	(
    SELECT
		DISTINCT ',' + QUOTENAME(phucap.Ma_PhuCap) 
    FROM TL_DM_PhuCap_NQ104 phucap
        FOR XML PATH(''), TYPE
	).value('.', 'NVARCHAR(MAX)'),1,1,'')

	SET @Query =
	'SELECT ThangNam, MaCanBo, TenCanBo, MaCapBac, CapBac, HSChucVu, MaChucVu, TenChucVu, Stk, NgayNhapNgu, NgayXuatNgu, NgayTaiNgu, ' + @Cols + ' FROM (
		SELECT
			bangLuong.Thang AS Thang,
			bangLuong.Nam AS Nam, 
			CONCAT(FORMAT(bangluong.Thang, ''D2''),''/'', bangluong.Nam) as ThangNam,
			donVi.Ten_Donvi TenDonVi,
			canBo.Ma_CanBo MaCanBo,
			canBo.Ten_CanBo AS TenCanBo,
			ISNULL(canBo.Ma_CB, ''0'') AS MaCapBac,
			ISNULL(capBac.Ten_Cb, ''0'') AS CapBac,
			ISNULL(chucVu.HeSo_Cv, 0) AS HSChucVu,
			chucVu.Ma_Cv AS MaChucVu,
			chucVu.Ten_Cv AS TenChucVu,
			CONCAT(canBo.So_TaiKhoan, '' '', canBo.Ma_CanBo) AS Stk,
			ISNULL(FORMAT(canBo.Ngay_NN,''MM/yy''), '''') AS NgayNhapNgu,
			ISNULL(FORMAT(canBo.Ngay_XN,''MM/yy''), '''') AS NgayXuatNgu,
			ISNULL(FORMAT(canBo.Ngay_TN,''MM/yy''), '''') AS NgayTaiNgu,
			bangLuong.GIA_TRI AS GiaTri,
			bangLuong.MA_PHUCAP AS MaPhuCap
		FROM TL_BangLuong_Thang_NQ104 bangLuong
		JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhapBangLuong ON bangLuong.parent=dsCapNhapBangLuong.Id
		JOIN TL_DM_CanBo_NQ104 canBo ON bangLuong.Ma_CBo=canBo.Ma_CanBo
		LEFT JOIN TL_DM_CapBac_NQ104 capBac ON canBo.Ma_CB104=capBac.Ma_Cb
		LEFT JOIN TL_DM_ChucVu chucVu ON canBo.Ma_CV=chucVu.Ma_Cv
		JOIN TL_DM_DonVi_NQ104 donVi ON bangLuong.Ma_DonVi=donVi.Ma_DonVi
		WHERE
			dsCapNhapBangLuong.Ma_CachTL=''CACH0''
			AND bangLuong.Ma_Hieu_CanBo = ''' + @MaHieuCanBo + '''
			AND CAST(CONCAT(''01-'',bangluong.THANG,''-'',bangluong.NAM) as datetime) between ''' + CAST(@TuNgay as varchar(MAX)) + ''' and ''' + CAST(@DenNgay as VARCHAR(MAX)) + '''
			AND dsCapNhapBangLuong.Status=1
			AND canBo.IsDelete=1
			AND canBo.Khong_Luong=0
	) x
	PIVOT
	(
		SUM(GiaTri)
		FOR MaPhuCap IN (' + @Cols + ')
	) pvt ORDER BY HSChucVu DESC, MaCapBac DESC, TenCanBo DESC'
	execute(@Query)
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_ds_chitra_nganhang_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_ds_chitra_nganhang_nq104]
	@cachTinhLuong nvarchar(20),
	@maDonVi nvarchar(20),
	@thang int,
	@nam int
AS
BEGIN
	SELECT 
		NULL AS iStt,
		Ten_Cbo AS sTenCbo,
		So_TaiKhoan AS sSoTaiKhoan,
		Ten_KhoBac AS sTenKhoBac,
		--TL_BangLuong_Thang_NQ104.ThanhTien AS fThanhTien,
		NULL AS sNoiDung

	FROM TL_BangLuong_Thang_NQ104
	INNER JOIN TL_DM_CanBo_NQ104
	ON TL_BangLuong_Thang_NQ104.Ma_CBo = TL_DM_CanBo_NQ104.Ma_CanBo
	WHERE Ma_CachTL = @cachTinhLuong
	AND TL_BangLuong_Thang_NQ104.Ma_DonVi = @maDonVi
	AND TL_BangLuong_Thang_NQ104.THANG = @thang
	AND TL_BangLuong_Thang_NQ104.NAM = @nam
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_giaithich_chitiet_phucap_hsq_cs_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_giaithich_chitiet_phucap_hsq_cs_nq104] @thang int, @nam int, @maDonVi nvarchar(MAX),
                                                                                          @donViTinh int AS DECLARE @Cols AS NVARCHAR(MAX)
SET @Cols = 'TNLCB_TT,TNLCV_CD_TT,TLCV_CD_TT,TLCB_TT,TLBLCB_TT,TLBLCV_CD_TT,LHT_TT,HSBL_TT,PCTNVK_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCCOV_TT,PCTRA_SUM,PCKHAC_SUM,LUONGTHANG_SUM,THANHTIEN';

WITH BangLuongThang AS
  (SELECT dsCapNhapBangLuong.Thang AS Thang,
          dsCapNhapBangLuong.Nam AS Nam,
          bangLuong.Ma_Can_Bo AS MaCanBo,
          bangLuong.MA_PHU_CAP AS MaPhuCap,
		  dsCapNhapBangLuong.Ma_CBo MaDonVi,
          bangLuong.GIA_TRI AS GiaTri
   FROM TL_BangLuong_Thang_Bridge_NQ104 bangLuong
   JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhapBangLuong ON bangLuong.parent = dsCapNhapBangLuong.Id
   WHERE bangLuong.Ma_Phu_Cap IN
       (SELECT *
        FROM f_split(@Cols))
     AND dsCapNhapBangLuong.Ma_CachTL='CACH0'
     AND dsCapNhapBangLuong.Ma_CBo IN
       (SELECT *
        FROM f_split(@maDonVi))
     AND dsCapNhapBangLuong.Thang=@thang
     AND dsCapNhapBangLuong.Nam=@nam
     AND dsCapNhapBangLuong.Status=1 ),
     ThongTinCanBo AS
  (SELECT canBo.Ma_CanBo AS MaCanBo,
          canBo.Ten_CanBo AS TenCanBo,
          ISNULL(canBo.Ma_CB104, '0') AS MaCapBac,
          ISNULL(capBac.TenCapBac, '0') AS TenNgach,
          ISNULL(capBac.MaCapBacCha, '0') AS MaNgach
   FROM TL_DM_CanBo_NQ104 canBo
   INNER JOIN TL_DM_DonVi_NQ104 donVi ON canBo.Parent=donVi.Ma_DonVi AND canBo.Parent IN (SELECT * FROM f_split(@maDonVi))
   LEFT JOIN
     (SELECT capbaccon.Ma_Cb MaCapBac,
             capbaccha.Ma_Cb MaCapBacCha,
             capbaccha.ten_cb TenCapBac
      FROM TL_DM_CapBac_NQ104 capbaccon
      LEFT JOIN TL_DM_CapBac_NQ104 capbaccha ON capbaccon.Parent = capbaccha.Ma_Cb) capBac ON canBo.Ma_CB104=capBac.MaCapBac
   LEFT JOIN TL_DM_ChucVu_NQ104 chucVu ON canBo.Ma_Cvd104=chucVu.Ma
   WHERE canBo.IsDelete = 1
     AND canBo.Khong_Luong = 0
	 AND canBo.Thang = @thang
	 AND canBo.Nam = @nam)
	 SELECT TenNgach,
		MaNgach,
		MaDonVi,
		COUNT(MaCanBo) AS SoNguoi,
		CAST(COUNT(LHT_TT) as float) DemLHT_TT,
		SUM(LHT_TT)/@donViTinh LHT_TT,
       SUM(HSBL_TT)/@donViTinh HSBL_TT,
       SUM(PCTNVK_TT)/@donViTinh PCTNVK_TT,
       SUM(PCCV_TT)/@donViTinh PCCV_TT,

	   SUM(TLCB_TT)/@donViTinh TLCB_TT,
	   CAST(COUNT(TLCB_TT) as float) DemTLCB_TT,
	   SUM(TNLCB_TT)/@donViTinh TNLCB_TT,
	   SUM(TLBLCB_TT)/@donViTinh TLBLCB_TT,

	   SUM(TLCV_CD_TT)/@donViTinh TLCV_CD_TT,
	   CAST(COUNT(TLCV_CD_TT) as float) DemTLCV_CD_TT,
	   SUM(TNLCV_CD_TT)/@donViTinh TNLCV_CD_TT,
	   SUM(TLBLCV_CD_TT)/@donViTinh TLBLCV_CD_TT,

	   CAST(COUNT(PCCV_TT) as float) DemPCCV_TT,
       SUM(PCTN_TT)/@donViTinh PCTN_TT,
	   CAST(COUNT(PCTN_TT) as float) DemPCTN_TT,
       SUM(PCKV_TT)/@donViTinh PCKV_TT,
	   CAST(COUNT(PCKV_TT) as float) DemPCKV_TT,
       SUM(PCCOV_TT)/@donViTinh PCCOV_TT,
	   CAST(COUNT(PCCOV_TT) as float) DemPCCOV_TT,
       SUM(PCTRA_SUM)/@donViTinh PCTRA_SUM,
	   CAST(COUNT(PCTRA_SUM) as float) DemPCTRA_SUM,
       SUM(PCKHAC_SUM)/@donViTinh PCKHAC_SUM,
	   CAST(COUNT(PCKHAC_SUM) as float) DemPCKHAC_SUM,
       SUM(LUONGTHANG_SUM)/@donViTinh LUONGTHANG_SUM,
       SUM(THANHTIEN)/@donViTinh THANHTIEN
   FROM
     (SELECT bangLuong.Thang AS Thang,
             bangLuong.Nam AS Nam,
             canBo.MaCanBo,
             canBo.MaCapBac,
             canBo.TenNgach,
             canBo.MaNgach,
             bangLuong.GiaTri,
             bangLuong.MaPhuCap,
			 bangLuong.MaDonVi MaDonVi
      FROM BangLuongThang bangLuong
      INNER JOIN ThongTinCanBo canBo ON bangLuong.MaCanBo = canBo.MaCanBo
	  Where bangLuong.GiaTri > 0) x PIVOT (SUM(GiaTri)
      FOR MaPhuCap IN (TNLCB_TT,TNLCV_CD_TT,TLCV_CD_TT,TLCB_TT,TLBLCB_TT,TLBLCV_CD_TT,LHT_TT, HSBL_TT, PCTNVK_TT, PCCV_TT, PCTN_TT, PCKV_TT, PCCOV_TT, PCDACTHU_SUM, PCTRA_SUM, PCKHAC_SUM, LUONGTHANG_SUM, BHXHCN_TT, BHYTCN_TT, BHTNCN_TT, TA_TONG, THUETNCN_TT, TRICHLUONG_TT, GTKHAC_TT, PHAITRU_SUM, THANHTIEN)) pvt
	Where MaNgach = '4'
	GROUP BY TenNgach,
         MaNgach, MaDonVi
	Order By MaDonVi, MaNgach
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_giaithich_chitiet_phucap_khac_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_giaithich_chitiet_phucap_khac_nq104]
	@Thang int, 
	@Nam int,
	@MaDonVi NVARCHAR(MAX),
	@MaCachTl NVARCHAR(50),
	@MaPhuCap NVARCHAR(MAX),
	@DonViTinh int,
	@IsOrderChucVu bit = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Query AS NVARCHAR(MAX)

	SET @Query =
	'
	WITH BangLuongThang AS (
		SELECT
			dsCapNhapBangLuong.Thang										AS Thang,
			dsCapNhapBangLuong.Nam											AS Nam,
			bangLuong.Ma_Can_Bo												AS MaCanBo,
			bangLuong.MA_PHU_CAP												AS MaPhuCap,
			bangLuong.GIA_TRI / ' + CAST(@DonViTinh AS VARCHAR(100)) + '	AS GiaTri
		FROM TL_BangLuong_Thang_Bridge_NQ104 bangLuong
		JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhapBangLuong
			ON bangLuong.parent = dsCapNhapBangLuong.Id
		WHERE
			bangLuong.Ma_Phu_Cap IN (SELECT * FROM f_split(''' + @MaPhuCap + '''))
			AND dsCapNhapBangLuong.Ma_CachTL=''' + @MaCachTl + '''
			AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
			AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND dsCapNhapBangLuong.Status=1
			AND bangLuong.GIA_TRI != 0
	), ThongTinCanBo AS (
		SELECT
			canBo.Ma_CanBo		AS MaCanBo,
			canBo.Ten_CanBo		AS TenCanBo,
			dbo.f_split_empty(canbo.Ten_CanBo) AS Ten,
			donVi.Ma_DonVi		AS MaDonVi,
			capBac.Ma_Cb			AS MaCapBac,
			capBac.xau_noi_ma XauNoiMa
		FROM TL_DM_CanBo_NQ104 canBo
		INNER JOIN TL_DM_DonVi_NQ104 donVi
		  ON canBo.Parent = donVi.Ma_DonVi
		INNER JOIN TL_DM_CapBac_NQ104 capBac
		  ON canBo.Ma_CB104 = capBac.Ma_Cb
		LEFT JOIN TL_DM_ChucVu_NQ104 chucVu
		  ON canBo.Ma_CVd104 = chucVu.Ma
		WHERE
			canBo.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND canBo.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND donVi.Ma_DonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
	)

	SELECT MaDonVi, MaCanBo, TenCanBo, Ten, MaCapBac, XauNoiMa, ' + @MaPhuCap + ' FROM (
		SELECT
			canBo.MaDonVi,
			canBo.MaCanBo,
			canBo.TenCanBo,
			canBo.Ten,
			canBo.MaCapBac,
			bangLuong.MaPhuCap,
			bangLuong.GiaTri,
			canBo.XauNoiMa
		FROM BangLuongThang bangLuong
		INNER JOIN ThongTinCanBo canBo
			ON bangLuong.MaCanBo = canBo.MaCanBo
	) x
	PIVOT
	(
		SUM(GiaTri)
		FOR MaPhuCap IN (' + @MaPhuCap + ')
	) pvt'

	IF @IsOrderChucVu = 1
	SET @Query = @Query +' ORDER BY MaCapBac , Ten ';
	ELSE 
	SET @Query = @Query +' ORDER BY MaCapBac , Ten ';
	execute(@Query)
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_giaithich_chitiet_phucap_truylinh_khac_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_giaithich_chitiet_phucap_truylinh_khac_nq104]
	@Thang int, 
	@Nam int,
	@MaDonVi NVARCHAR(MAX),
	@MaCachTl NVARCHAR(50),
	@MaPhuCap NVARCHAR(MAX),
	@DonViTinh int,
	@IsOrderChucVu bit = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Query AS NVARCHAR(MAX)

	SET @Query =
	'
	WITH BangLuongThang AS (
		SELECT
			dsCapNhapBangLuong.Thang										AS Thang,
			dsCapNhapBangLuong.Nam											AS Nam,
			bangLuong.Ma_CBo												AS MaCanBo,
			bangLuong.MA_PHUCAP												AS MaPhuCap,
			bangLuong.GIA_TRI / ' + CAST(@DonViTinh AS VARCHAR(100)) + '	AS GiaTri
		FROM TL_BangLuong_Thang_NQ104 bangLuong
		JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhapBangLuong
			ON bangLuong.parent = dsCapNhapBangLuong.Id
		WHERE
			bangLuong.Ma_PhuCap IN (SELECT * FROM f_split(''' + @MaPhuCap + '''))
			AND dsCapNhapBangLuong.Ma_CachTL=''' + @MaCachTl + '''
			AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
			AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND dsCapNhapBangLuong.Status=1
			AND bangLuong.GIA_TRI != 0
	), ThongTinCanBo AS (
		SELECT
			canBo.Ma_CanBo		AS MaCanBo,
			canBo.Ten_CanBo		AS TenCanBo,
			dbo.f_split_empty(canbo.Ten_CanBo) AS Ten,
			ISNULL(chucVu.HeSo_Cv, 0) AS HSChucVu,
			donVi.Ma_DonVi		AS MaDonVi,
			capBac.Ma_Cb			AS MaCapBac,
			capBac.XauNoiMa
		FROM TL_DM_CanBo_NQ104 canBo
		INNER JOIN TL_DM_DonVi_NQ104 donVi
		  ON canBo.Parent = donVi.Ma_DonVi
		INNER JOIN TL_DM_CapBac_NQ104 capBac
		  ON canBo.Ma_CB104 = capBac.Ma_Cb
		LEFT JOIN TL_DM_ChucVu chucVu
		  ON canBo.Ma_CV=chucVu.Ma_Cv
		WHERE
			canBo.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND canBo.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND donVi.Ma_DonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
	)

	SELECT MaDonVi, MaCanBo, TenCanBo, Ten, HSChucVu, MaCapBac, XauNoiMa, ' + @MaPhuCap + ' FROM (
		SELECT
			canBo.MaDonVi,
			canBo.MaCanBo,
			canBo.TenCanBo,
			canBo.Ten,
			canBo.HSChucVu,
			canBo.MaCapBac,
			bangLuong.MaPhuCap,
			bangLuong.GiaTri,
			canBo.XauNoiMa
		FROM BangLuongThang bangLuong
		INNER JOIN ThongTinCanBo canBo
			ON bangLuong.MaCanBo = canBo.MaCanBo
	) x
	PIVOT
	(
		SUM(GiaTri)
		FOR MaPhuCap IN (' + @MaPhuCap + ')
	) pvt'

	IF @IsOrderChucVu = 1
	SET @Query = @Query +' ORDER BY HSChucVu , MaCapBac , Ten ';
	ELSE 
	SET @Query = @Query +' ORDER BY MaCapBac , Ten ';
	execute(@Query)
END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_giaithich_chitiet_phucap_vuotkhung_handinh_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_tl_rpt_giaithich_chitiet_phucap_vuotkhung_handinh_nq104]
	@Thang int, 
	@Nam int,
	@MaDonVi NVARCHAR(MAX),
	@MaCachTl NVARCHAR(50),
	@DonViTinh int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @MaPhuCap NVARCHAR(MAX) SET @MaPhuCap = 'LHT_TT,HSBL_TT,PCTNVK_TT,PCTHD_TT';
	DECLARE @MaPhuCapVKTHD NVARCHAR(MAX) SET @MaPhuCapVKTHD = 'PCTNVK_TT,PCTHD_TT';

    WITH CanBoVK AS (
		SELECT
			bangLuong.ma_can_bo AS MaCanBo
		FROM TL_BangLuong_Thang_Bridge_NQ104 bangLuong
		JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhapBangLuong
			ON bangLuong.parent = dsCapNhapBangLuong.Id
		WHERE
			bangLuong.ma_phu_cap IN (SELECT * FROM f_split(@MaPhuCapVKTHD))
			AND dsCapNhapBangLuong.Ma_CachTL='CACH0'
			AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(@MaDonVi))
			AND dsCapNhapBangLuong.Thang=@Thang
			AND dsCapNhapBangLuong.Nam=@Nam
			AND dsCapNhapBangLuong.Status=1
			AND bangLuong.GIA_TRI != 0
	), BangLuongThang AS (
		SELECT
			dsCapNhapBangLuong.Thang	AS Thang,
			dsCapNhapBangLuong.Nam		AS Nam,
			bangLuong.ma_can_bo			AS MaCanBo,
			bangLuong.ma_phu_cap			AS MaPhuCap,
			bangLuong.GIA_TRI			AS GiaTri
		FROM TL_BangLuong_Thang_Bridge_NQ104 bangLuong
		JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhapBangLuong
			ON bangLuong.parent = dsCapNhapBangLuong.Id
		WHERE
			bangLuong.ma_phu_cap IN (SELECT * FROM f_split(@MaPhuCap))
			AND dsCapNhapBangLuong.Ma_CachTL='CACH0'
			AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(@MaDonVi))
			AND dsCapNhapBangLuong.Thang=@Thang
			AND dsCapNhapBangLuong.Nam=@Nam
			AND dsCapNhapBangLuong.Status=1
	), ThongTinCanBo AS (
		 SELECT
		  canBo.Ma_CanBo		AS MaCanBo,
		  donVi.Ma_DonVi		AS MaDonVi,
		  capBacParent.Ma_Cb	AS Ngach,
		  capBacParent.Ten_Cb	AS DoiTuong,
		  capBac.Ma_Cb			AS MaCapBac
		FROM TL_DM_CanBo_NQ104 canBo
		INNER JOIN TL_DM_DonVi_NQ104 donVi
		  ON canBo.Parent = donVi.Ma_DonVi
		INNER JOIN TL_DM_CapBac_NQ104 capBac
		  ON canBo.Ma_CB104 = capBac.Ma_Cb
		 INNER JOIN TL_DM_CapBac capBacParent
		  ON capBac.Parent = capBacParent.Ma_Cb
		WHERE
		  canBo.Thang = @Thang
		  AND canBo.Nam = @Nam
		  AND donVi.Ma_DonVi IN (SELECT * FROM f_split(@MaDonVi))
		  AND canBo.Ma_CanBo in (SELECT * FROM CanBoVK)
	), SoLieuBaoCao AS (
		SELECT
			canBo.MaDonVi								AS MaDonVi,
			canBo.Ngach									AS Ngach,
			COUNT(bangLuongThang.MaCanBo)				AS SoNguoi,
			canBo.DoiTuong								AS DoiTuong,
			bangLuongThang.MaPhuCap						AS MaPhuCap,
			SUM(bangLuongThang.GiaTri) / @DonViTinh		AS GiaTri
		FROM BangLuongThang bangLuongThang
		INNER JOIN ThongTinCanBo canBo
			ON bangLuongThang.MaCanBo = canBo.MaCanBo
		GROUP BY MaDonVi, Ngach, DoiTuong, MaPhuCap
	)

	SELECT * FROM SoLieuBaoCao
	PIVOT (
		SUM(GiaTri)
		FOR MaPhuCap IN (LHT_TT,HSBL_TT,PCTNVK_TT,PCTHD_TT)
	) pvt ORDER BY MaDonVi, Ngach;
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_giaithich_chitiet_raquan_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TungNH
-- Create date: 09/12/2021
-- Description:	Lấy dữ liệu báo cáo bảng lương tháng
-- =============================================
CREATE PROCEDURE [dbo].[sp_tl_rpt_giaithich_chitiet_raquan_nq104]
	@MaDonVi NVARCHAR(max), 
	@Thang int,
	@Nam AS int,
	@donViTinh int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @Cols AS NVARCHAR(MAX)
	DECLARE @Query AS NVARCHAR(MAX)

	SET @Cols = 'TCRAQUAN_TT,TCVIECLAM_TT,TIENTAUXE_TT,TIENANDUONG_TT,TIENCTLH_TT,GTKHAC_TT'
	SET @Query =
	'WITH BangLuongThang AS (
			SELECT
				dsCapNhapBangLuong.Thang	AS Thang,
				dsCapNhapBangLuong.Nam		AS Nam,
				bangLuong.Ma_Can_Bo			AS MaCanBo,
				bangLuong.MA_PHU_CAP			AS MaPhuCap,
				bangLuong.GIA_TRI			AS GiaTri
			FROM TL_BangLuong_Thang_Bridge_NQ104 bangLuong
			JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhapBangLuong
				ON bangLuong.parent = dsCapNhapBangLuong.Id
			WHERE
				bangLuong.Ma_Phu_Cap IN (SELECT * FROM f_split(''' + @Cols + '''))
				AND dsCapNhapBangLuong.Ma_CachTL=''CACH0''
				AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
				AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
				AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
				AND dsCapNhapBangLuong.Status=1 
	),
	
	ThongTinCanBo AS (
		SELECT
			canBo.Thang,
			canBo.Nam,
			donVi.Ma_DonVi		AS MaDonVi,
			donVi.Ten_Donvi		AS TenDonvi,
			canBo.Ma_CanBo		AS MaCanBo,
			canBo.Ten_CanBo		AS TenCanBo,
			ISNULL(canBo.Ma_CB104, ''0'') AS MaCapBac,
			ISNULL(capBac.Ten_Cb, ''0'') AS CapBac,
			ISNULL(FORMAT(canBo.Ngay_NN,''MM/yy''), '''') AS NgayNhapNgu,
			ISNULL(FORMAT(canBo.Ngay_XN,''MM/yy''), '''') AS NgayXuatNgu,
			canBo.Ngay_NN AS NhapNgu,
			canBo.Ngay_XN AS XuatNgu
		FROM TL_DM_CanBo_NQ104 canBo
		INNER JOIN TL_DM_DonVi_NQ104 donVi
			ON canBo.Parent=donVi.Ma_DonVi
		LEFT JOIN TL_DM_CapBac_NQ104 capBac
			ON canBo.Ma_CB104=capBac.Ma_Cb
		WHERE
			canBo.IsDelete = 1
			AND canBo.Khong_Luong = 0
			AND canBo.Ngay_XN is not null
			AND canBo.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND canBo.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
	)

	SELECT MaDonVi, MaCanBo, TenCanBo, MaCapBac, CapBac, NgayNn, NgayXn, NhapNgu, XuatNgu, Thang, Nam,
		TCRAQUAN_TT,
		TCVIECLAM_TT,
		TIENTAUXE_TT,
		TIENANDUONG_TT,
		TIENCTLH_TT,
		GTKHAC_TT,
		(TCRAQUAN_TT + TCVIECLAM_TT + TIENTAUXE_TT + TIENANDUONG_TT + TIENCTLH_TT - GTKHAC_TT) TIENTONG
	FROM (
		SELECT
			canBo.Thang AS Thang,
			canBo.Nam AS Nam, 
			canBo.MaDonVi,
			canBo.TenDonVi,
			canBo.MaCanBo,
			canBo.TenCanBo,
			canBo.MaCapBac,
			canBo.CapBac,
			canBo.NgayNhapNgu NgayNn,
			canBo.NgayXuatNgu NgayXn,
			canBo.NhapNgu,
			canBo.XuatNgu,
			bangLuong.GiaTri GiaTri,
			bangLuong.MaPhuCap MaPhuCap
		FROM BangLuongThang bangLuong
		INNER JOIN ThongTinCanBo canBo
			ON bangLuong.MaCanBo = canBo.MaCanBo
	) x
	PIVOT
	(
		SUM(GiaTri)
		FOR MaPhuCap IN (' + @Cols + ')
	) pvt
	WHERE MaCapBac LIKE ''0%''
	ORDER BY MaCapBac DESC, TenCanBo DESC'
	execute(@Query)
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_giaithich_phucap_bienphong_heso_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_giaithich_phucap_bienphong_heso_nq104]
	@maPhuCap nvarchar(100), @maPhuCapTien nvarchar(100), @maDonVi nvarchar(MAX), @thang int, @nam int
AS
Begin
	Declare @Cols as NVARCHAR(MAX)
	DECLARE @Query AS NVARCHAR(MAX)
	Set @Cols = @maPhuCap + ',' + @maPhuCapTien
	SET @Query = '
	With Tong As (
		Select MaDonVi, ' + @maPhuCap + ', SUM(' + @maPhuCapTien + ') TienTong, COUNT(*) SoNguoiTong
		FROM
		(SELECT
			dsCapNhapBangLuong.Ma_Cbo	AS MaDonVi,
			dsCapNhapBangLuong.Thang	AS Thang,
			dsCapNhapBangLuong.Nam		AS Nam,
			bangLuong.Ma_Can_Bo			AS MaCanBo,
			bangLuong.MA_PHU_CAP			AS MaPhuCap,
			bangLuong.GIA_TRI			AS GiaTri
		FROM TL_BangLuong_Thang_Bridge_NQ104 bangLuong
		JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhapBangLuong
			ON bangLuong.parent = dsCapNhapBangLuong.Id
		WHERE
			bangLuong.Ma_Phu_Cap IN (''' + @maPhuCap + ''', ''' + @maPhuCapTien + ''')
			AND dsCapNhapBangLuong.Ma_CachTL=''CACH0''
			AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
			AND dsCapNhapBangLuong.Thang=' + CAST(@thang AS VARCHAR(2)) + '
			AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND dsCapNhapBangLuong.Status=1) x
		PIVOT 
		(
			SUM(GiaTri)
			FOR MaPhuCap IN (' + @Cols + ')
		) pvt
		Where ' + @maPhuCap + ' <> 0
		Group By MaDonVi, ' + @maPhuCap + '
		),

	Sq As (
		Select MaDonVi, ' + @maPhuCap + ', SUM(' + @maPhuCapTien + ') TienSq, COUNT(*) SoNguoiSq
		FROM
		(SELECT
			dsCapNhapBangLuong.Ma_Cbo	AS MaDonVi,
			dsCapNhapBangLuong.Thang	AS Thang,
			dsCapNhapBangLuong.Nam		AS Nam,
			bangLuong.Ma_Can_Bo			AS MaCanBo,
			bangLuong.MA_PHU_CAP			AS MaPhuCap,
			bangLuong.GIA_TRI			AS GiaTri
		FROM TL_BangLuong_Thang_Bridge_NQ104 bangLuong
		JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhapBangLuong
			ON bangLuong.parent = dsCapNhapBangLuong.Id
		JOIN TL_DM_CanBo_NQ104 canbo
			ON bangLuong.ma_hieu_can_bo = canbo.Ma_Hieu_CanBo
		WHERE
			bangLuong.Ma_Phu_Cap IN (''' + @maPhuCap + ''', ''' + @maPhuCapTien + ''')
			AND canbo.Ma_CB LIKE (''1%'')
			AND dsCapNhapBangLuong.Ma_CachTL=''CACH0''
			AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
			AND dsCapNhapBangLuong.Thang=' + CAST(@thang AS VARCHAR(2)) + '
			AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND dsCapNhapBangLuong.Status=1) x
		PIVOT 
		(
			SUM(GiaTri)
			FOR MaPhuCap IN (' + @Cols + ')
		) pvt
		Where ' + @maPhuCap + ' <> 0
		Group By MaDonVi, ' + @maPhuCap + '
		),

	Qncn As (
		Select MaDonVi, ' + @maPhuCap + ', SUM(' + @maPhuCapTien + ') TienQncn, COUNT(*) SoNguoiQncn
		FROM
		(SELECT
			dsCapNhapBangLuong.Ma_Cbo	AS MaDonVi,
			dsCapNhapBangLuong.Thang	AS Thang,
			dsCapNhapBangLuong.Nam		AS Nam,
			bangLuong.Ma_Can_Bo			AS MaCanBo,
			bangLuong.MA_PHU_CAP			AS MaPhuCap,
			bangLuong.GIA_TRI			AS GiaTri
		FROM TL_BangLuong_Thang_Bridge_NQ104 bangLuong
		JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhapBangLuong
			ON bangLuong.parent = dsCapNhapBangLuong.Id
		JOIN TL_DM_CanBo_NQ104 canbo
			ON bangLuong.ma_hieu_can_bo = canbo.Ma_Hieu_CanBo
		WHERE
			bangLuong.Ma_Phu_Cap IN (''' + @maPhuCap + ''', ''' + @maPhuCapTien + ''')
			AND canbo.Ma_CB LIKE (''2%'')
			AND dsCapNhapBangLuong.Ma_CachTL=''CACH0''
			AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
			AND dsCapNhapBangLuong.Thang=' + CAST(@thang AS VARCHAR(2)) + '
			AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND dsCapNhapBangLuong.Status=1) x
		PIVOT 
		(
			SUM(GiaTri)
			FOR MaPhuCap IN (' + @Cols + ')
		) pvt
		Where ' + @maPhuCap + ' <> 0
		Group By MaDonVi, ' + @maPhuCap + '
		),

	Cnvc As (
		Select MaDonVi, ' + @maPhuCap + ', SUM(' + @maPhuCapTien + ') TienCnvc, COUNT(*) SoNguoiCnvc
		FROM
		(SELECT
			dsCapNhapBangLuong.Ma_Cbo	AS MaDonVi,
			dsCapNhapBangLuong.Thang	AS Thang,
			dsCapNhapBangLuong.Nam		AS Nam,
			bangLuong.Ma_Can_Bo			AS MaCanBo,
			bangLuong.MA_PHU_CAP			AS MaPhuCap,
			bangLuong.GIA_TRI			AS GiaTri
		FROM TL_BangLuong_Thang_Bridge_NQ104 bangLuong
		JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhapBangLuong
			ON bangLuong.parent = dsCapNhapBangLuong.Id
		JOIN TL_DM_CanBo_NQ104 canbo
			ON bangLuong.ma_hieu_can_bo = canbo.Ma_Hieu_CanBo
		WHERE
			bangLuong.Ma_Phu_Cap IN (''' + @maPhuCap + ''', ''' + @maPhuCapTien + ''')
			AND canbo.Ma_CB LIKE (''4%'')
			AND dsCapNhapBangLuong.Ma_CachTL=''CACH0''
			AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
			AND dsCapNhapBangLuong.Thang=' + CAST(@thang AS VARCHAR(2)) + '
			AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND dsCapNhapBangLuong.Status=1) x
		PIVOT 
		(
			SUM(GiaTri)
			FOR MaPhuCap IN (' + @Cols + ')
		) pvt
		Where ' + @maPhuCap + ' <> 0
		Group By MaDonVi, ' + @maPhuCap + '
		),

	Hsq As (
		Select MaDonVi, ' + @maPhuCap + ', SUM(' + @maPhuCapTien + ') TienHsq, COUNT(*) SoNguoiHsq
		FROM
		(SELECT
			dsCapNhapBangLuong.Ma_Cbo	AS MaDonVi,
			dsCapNhapBangLuong.Thang	AS Thang,
			dsCapNhapBangLuong.Nam		AS Nam,
			bangLuong.Ma_Can_Bo			AS MaCanBo,
			bangLuong.MA_PHU_CAP			AS MaPhuCap,
			bangLuong.GIA_TRI			AS GiaTri
		FROM TL_BangLuong_Thang_Bridge_NQ104 bangLuong
		JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhapBangLuong
			ON bangLuong.parent = dsCapNhapBangLuong.Id
		JOIN TL_DM_CanBo_NQ104 canbo
			ON bangLuong.ma_hieu_can_bo = canbo.Ma_Hieu_CanBo
		WHERE
			bangLuong.Ma_Phu_Cap IN (''' + @maPhuCap + ''', ''' + @maPhuCapTien + ''')
			AND canbo.Ma_CB LIKE (''0%'')
			AND dsCapNhapBangLuong.Ma_CachTL=''CACH0''
			AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
			AND dsCapNhapBangLuong.Thang=' + CAST(@thang AS VARCHAR(2)) + '
			AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND dsCapNhapBangLuong.Status=1) x
		PIVOT 
		(
			SUM(GiaTri)
			FOR MaPhuCap IN (' + @Cols + ')
		) pvt
		Where ' + @maPhuCap + ' <> 0
		Group By MaDonVi, ' + @maPhuCap + '
		)

		Select tong.MaDonVi, tong.' + @maPhuCap + ' as Ten_DonVi, TienTong, SoNguoiTong, TienSq, SoNguoiSq, TienQncn, SoNguoiQncn, TienCnvc, SoNguoiCnvc, TienHsq, SoNguoiHsq
		From Tong tong
		Full Outer Join Sq on tong.MaDonVi = Sq.MaDonVi And tong.' + @maPhuCap + ' = Sq.' + @maPhuCap + '
		Full Outer Join Qncn on tong.MaDonVi = Qncn.MaDonVi And tong.' + @maPhuCap + ' = Qncn.' + @maPhuCap + '
		Full Outer Join Cnvc on tong.MaDonVi = Cnvc.MaDonVi And tong.' + @maPhuCap + ' = Cnvc.' + @maPhuCap + '
		Full Outer Join Hsq on tong.MaDonVi = Hsq.MaDonVi And tong.' + @maPhuCap + ' = Hsq.' + @maPhuCap + '
		'
	execute(@Query)
End
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_giaithich_phucap_bienphong_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_giaithich_phucap_bienphong_nq104]
	@maPhuCap nvarchar(100), @maDonVi nvarchar(MAX), @thang int, @nam int
AS
BEGIN
	With Sq as (
		Select Ma_Don_Vi, SUM(Gia_Tri) AS TienSq, COUNT(*) AS SoNguoiSq
		From TL_BangLuong_Thang_Bridge_NQ104 bangLuong
		Join TL_DS_CapNhap_BangLuong_NQ104 dsLuong On dsLuong.Id = bangLuong.parent
		join TL_DM_CanBo_NQ104 canbo on bangLuong.ma_hieu_can_bo = canbo.Ma_Hieu_CanBo
		Join TL_DM_CapBac_NQ104 capBac On canbo.Ma_CB104 = capbac.Ma_Cb
		Where Ma_Don_Vi IN (SELECT * FROM f_split(@MaDonVi))
			AND dsLuong.Thang = @thang
			AND dsLuong.Nam = @nam
			AND ma_phu_cap = @maPhuCap
			AND dsLuong.Ma_CachTL = 'CACH0'
			AND capBac.Parent = '1'
			AND Gia_Tri <> 0
		Group By ma_don_vi
	),
	Qncn as (
		Select ma_don_vi, SUM(Gia_Tri) AS TienQncn, COUNT(*) AS SoNguoiQncn
		From TL_BangLuong_Thang_Bridge_NQ104 bangLuong
		Join TL_DS_CapNhap_BangLuong_NQ104 dsLuong On dsLuong.Id = bangLuong.parent
		join TL_DM_CanBo_NQ104 canbo on bangLuong.ma_hieu_can_bo = canbo.Ma_Hieu_CanBo
		Join TL_DM_CapBac_NQ104 capBac On canbo.Ma_CB104 = capbac.Ma_Cb
		Where ma_don_vi IN (SELECT * FROM f_split(@MaDonVi))
			AND dsLuong.Thang = @thang
			AND dsLuong.Nam = @nam
			AND ma_phu_cap = @maPhuCap
			AND dsLuong.Ma_CachTL = 'CACH0'
			AND capBac.Parent = '2'
			AND Gia_Tri <> 0
		Group By ma_don_vi
	),
	Cnvc as (
		Select ma_don_vi, SUM(Gia_Tri) AS TienCnvc, COUNT(*) AS SoNguoiCnvc
		From TL_BangLuong_Thang_Bridge_NQ104 bangLuong
		Join TL_DS_CapNhap_BangLuong_NQ104 dsLuong On dsLuong.Id = bangLuong.parent
		join TL_DM_CanBo_NQ104 canbo on bangLuong.ma_hieu_can_bo = canbo.Ma_Hieu_CanBo
		Join TL_DM_CapBac_NQ104 capBac On canbo.Ma_CB104 = capbac.Ma_Cb
		Where ma_don_vi IN (SELECT * FROM f_split(@MaDonVi))
			AND dsLuong.Thang = @thang
			AND dsLuong.Nam = @nam
			AND ma_phu_cap = @maPhuCap
			AND dsLuong.Ma_CachTL = 'CACH0'
			AND capBac.Parent = '3'
			AND Gia_Tri <> 0
		Group By ma_don_vi
	),
	HsqCs as (
		Select ma_don_vi, SUM(Gia_Tri) AS TienHsq, COUNT(*) AS SoNguoiHsq
		From TL_BangLuong_Thang_Bridge_NQ104 bangLuong
		Join TL_DS_CapNhap_BangLuong_NQ104 dsLuong On dsLuong.Id = bangLuong.parent
		join TL_DM_CanBo_NQ104 canbo on bangLuong.ma_hieu_can_bo = canbo.Ma_Hieu_CanBo
		Join TL_DM_CapBac_NQ104 capBac On canbo.Ma_CB104 = capbac.Ma_Cb
		Where ma_don_vi IN (SELECT * FROM f_split(@MaDonVi))
			AND dsLuong.Thang = @thang
			AND dsLuong.Nam = @nam
			AND ma_phu_cap = @maPhuCap
			AND dsLuong.Ma_CachTL = 'CACH0'
			AND capBac.Parent = '4'
			AND Gia_Tri <> 0
		Group By ma_don_vi
	),
	Tong as (
		Select Ma_Don_Vi, SUM(Gia_Tri) AS TienTong, COUNT(*) AS SoNguoiTong
		From TL_BangLuong_Thang_Bridge_NQ104 bangLuong
		Join TL_DS_CapNhap_BangLuong_NQ104 dsLuong On dsLuong.Id = bangLuong.parent
		join TL_DM_CanBo_NQ104 canbo on bangLuong.ma_hieu_can_bo = canbo.Ma_Hieu_CanBo
		Join TL_DM_CapBac_NQ104 capBac On canbo.Ma_CB104 = capbac.Ma_Cb
		Where Ma_Don_Vi IN (SELECT * FROM f_split(@MaDonVi))
			AND dsLuong.Thang = @thang
			AND dsLuong.Nam = @nam
			AND Ma_Phu_Cap = @maPhuCap
			AND dsLuong.Ma_CachTL = 'CACH0'
			AND Gia_Tri <> 0
		Group By Ma_Don_Vi
	)

	Select DonVi.Ma_DonVi, DonVi.Ten_DonVi, TienSq, SoNguoiSq, TienQncn, SoNguoiQncn, TienCnvc, SoNguoiCnvc, TienHsq, SoNguoiHsq, TienTong, SoNguoiTong
	From TL_DM_DonVi_NQ104 donvi
	full outer Join Sq on donvi.Ma_DonVi = Sq.ma_don_vi
	full outer Join Qncn on donvi.Ma_DonVi = Qncn.ma_don_vi
	full outer Join Cnvc on donvi.Ma_DonVi = Cnvc.ma_don_vi
	full outer Join HsqCs on donvi.Ma_DonVi = HsqCs.ma_don_vi
	full outer Join Tong on donvi.Ma_DonVi = Tong.ma_don_vi
	Where DonVi.Ma_DonVi IN (SELECT * FROM f_split(@MaDonVi))
	And (TienSq <> 0 Or TienQncn <> 0 Or TienCnvc <> 0 Or TienHsq <> 0)
	Order By Ma_DonVi
	
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_giaithich_phucap_theongay_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_giaithich_phucap_theongay_nq104]
	@Thang int, 
	@Nam int,
	@MaDonVi NVARCHAR(MAX),
	@MaCachTl NVARCHAR(50),
	@MaPhuCap NVARCHAR(MAX),
	@DonViTinh int,
	@IsOrderChucVu bit = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Query AS NVARCHAR(MAX)

	SET @Query =
	'
	WITH BangLuongThang AS (
		SELECT
			dsCapNhapBangLuong.Thang										AS Thang,
			dsCapNhapBangLuong.Nam											AS Nam,
			bangLuong.Ma_Can_Bo												AS MaCanBo,
			bangLuong.MA_PHU_CAP												AS MaPhuCap,
			bangLuong.GIA_TRI / ' + CAST(@DonViTinh AS VARCHAR(100)) + '	AS GiaTri
		FROM TL_BangLuong_Thang_Bridge_NQ104 bangLuong
		JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhapBangLuong
			ON bangLuong.parent = dsCapNhapBangLuong.Id
		WHERE
			bangLuong.Ma_Phu_Cap IN (SELECT * FROM f_split(''' + @MaPhuCap + '''))
			AND dsCapNhapBangLuong.Ma_CachTL=''' + @MaCachTl + '''
			AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
			AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND dsCapNhapBangLuong.Status=1
	), ThongTinCanBo AS (
		SELECT
			canBo.Ma_CanBo		AS MaCanBo,
			canBo.Ten_CanBo		AS TenCanBo,
			dbo.f_split_empty(canbo.Ten_CanBo) AS Ten,
			donVi.Ma_DonVi		AS MaDonVi,
			capBac.Ma_Cb			AS MaCapBac,
			capBac.xau_noi_ma XauNoiMa
		FROM TL_DM_CanBo_NQ104 canBo
		INNER JOIN TL_DM_DonVi_NQ104 donVi
		  ON canBo.Parent = donVi.Ma_DonVi
		INNER JOIN TL_DM_CapBac_NQ104 capBac
		  ON canBo.Ma_CB104 = capBac.Ma_Cb AND capBac.nam=' + CAST(@Nam AS VARCHAR(4)) + '
		LEFT JOIN TL_DM_ChucVu_NQ104 chucVu
		  ON canBo.Ma_CVd104 = chucVu.Ma AND chucVu.nam=' + CAST(@Nam AS VARCHAR(4)) + '
		WHERE
			canBo.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND canBo.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND donVi.Ma_DonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
	)

	SELECT MaDonVi, MaCanBo, TenCanBo, Ten, MaCapBac, XauNoiMa, ' + @MaPhuCap + ' FROM (
		SELECT
			canBo.MaDonVi,
			canBo.MaCanBo,
			canBo.TenCanBo,
			canBo.Ten,
			canBo.MaCapBac,
			bangLuong.MaPhuCap,
			bangLuong.GiaTri,
			canBo.XauNoiMa
		FROM BangLuongThang bangLuong
		INNER JOIN ThongTinCanBo canBo
			ON bangLuong.MaCanBo = canBo.MaCanBo
	) x
	PIVOT
	(
		SUM(GiaTri)
		FOR MaPhuCap IN (' + @MaPhuCap + ')
	) pvt'

	IF @IsOrderChucVu = 1
	SET @Query = @Query +' ORDER BY MaCapBac , Ten ';
	ELSE 
	SET @Query = @Query +' ORDER BY MaCapBac , Ten ';
	execute(@Query)
END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_giaithich_songay_phucap_theongay_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_giaithich_songay_phucap_theongay_nq104]
	@Thang int, 
	@Nam int,
	@MaDonVi NVARCHAR(MAX),
	@MaCachTl NVARCHAR(50),
	@MaPhuCap NVARCHAR(MAX),
	@DonViTinh int,
	@IsOrderChucVu bit = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Query AS NVARCHAR(MAX)
	SET @Query =
	'
	WITH BangLuongThang AS (
		SELECT
			dsCapNhapBangLuong.Thang										AS Thang,
			dsCapNhapBangLuong.Nam											AS Nam,
			bangLuong.Ma_Can_Bo												AS MaCanBo,
			bangLuong.MA_PHU_CAP												AS MaPhuCap,
			phucapcanbo.ngay_huong_phu_cap									AS SoNgay
		FROM TL_BangLuong_Thang_Bridge_NQ104 bangLuong
		JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhapBangLuong ON bangLuong.parent = dsCapNhapBangLuong.Id
		JOIN TL_CanBo_PhuCap_Bridge_NQ104 phucapcanbo ON phucapcanbo.ma_can_bo = bangLuong.ma_can_bo 
		AND phucapcanbo.ma_phu_cap = bangLuong.ma_phu_cap
		WHERE
			bangLuong.Ma_Phu_Cap IN (SELECT * FROM f_split(''' + @MaPhuCap + '''))
			AND dsCapNhapBangLuong.Ma_CachTL=''' + @MaCachTl + '''
			AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
			AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND dsCapNhapBangLuong.Status=1
	), ThongTinCanBo AS (
		SELECT
			canBo.Ma_CanBo		AS MaCanBo,
			canBo.Ten_CanBo		AS TenCanBo,
			dbo.f_split_empty(canbo.Ten_CanBo) AS Ten,
			donVi.Ma_DonVi		AS MaDonVi,
			capBac.Ma_Cb			AS MaCapBac,
			capBac.xau_noi_ma XauNoiMa
		FROM TL_DM_CanBo_NQ104 canBo
		INNER JOIN TL_DM_DonVi_NQ104 donVi
		  ON canBo.Parent = donVi.Ma_DonVi
		INNER JOIN TL_DM_CapBac_NQ104 capBac
		  ON canBo.Ma_CB104 = capBac.Ma_Cb AND capBac.nam=' + CAST(@Nam AS VARCHAR(4)) + '
		LEFT JOIN TL_DM_ChucVu_NQ104 chucVu
		  ON canBo.Ma_CVd104 = chucVu.Ma AND chucVu.nam=' + CAST(@Nam AS VARCHAR(4)) + '
		WHERE
			canBo.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND canBo.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND donVi.Ma_DonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
	)

	SELECT MaDonVi, MaCanBo, TenCanBo, Ten, MaCapBac, XauNoiMa, ' + @MaPhuCap + ' FROM (
		SELECT
			canBo.MaDonVi,
			canBo.MaCanBo,
			canBo.TenCanBo,
			canBo.Ten,
			canBo.MaCapBac,
			bangLuong.MaPhuCap,
			isnull(bangLuong.SoNgay, dbo.fnTotalDayOfMonth(' + CAST(@Thang AS VARCHAR(2)) + ',' + CAST(@Nam AS VARCHAR(4)) + ')) SoNgay,
			canBo.XauNoiMa
		FROM BangLuongThang bangLuong
		INNER JOIN ThongTinCanBo canBo
			ON bangLuong.MaCanBo = canBo.MaCanBo
	) x
	PIVOT
	(
		SUM(SoNgay)
		FOR MaPhuCap IN (' + @MaPhuCap + ')
	) pvt'

	IF @IsOrderChucVu = 1
	SET @Query = @Query +' ORDER BY MaCapBac , Ten ';
	ELSE 
	SET @Query = @Query +' ORDER BY MaCapBac , Ten ';
	execute(@Query)
END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_kehoach_chitiet_canbo_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_tl_rpt_kehoach_chitiet_canbo_nq104]
	@NamKeHoach int,
	@MaDonVi varchar(500)
AS
BEGIN
SELECT blcl.Ma_Hieu_CanBo MaHieuCanBo,
          blcl.Ma_CB MaCapBac,
          blcl.Ten_CanBo TenCanBo,
          blcl.thangBl Thang,
          blcl.Ma_PhuCap MaPhuCap,
          (sum(ISNULL(blcl.luongKh, 0)) - sum(ISNULL(blcl.luongt, 0))) AS ChenhLech
   FROM
     (SELECT cb.Ma_Hieu_CanBo,
             cb.Ten_CanBo,
             cb.Thang AS thangCb,
             blkh.Thang AS thangBl,
             blkh.Nam,
             blkh.Ma_CB,
             blkh.Ma_PhuCap,
             blkh.Gia_Tri AS luongKh,
             0 AS luongt
      FROM TL_DM_CanBo_NQ104_KeHoach cb
      LEFT JOIN TL_BangLuong_KeHoach blkh ON cb.Ma_Hieu_CanBo = blkh.Ma_Hieu_CanBo
      AND cb.Thang = blkh.Thang
      AND cb.Nam = @NamKeHoach
      WHERE blkh.Nam = @NamKeHoach
        AND blkh.Ma_DonVi = @MaDonVi
		AND exists (select 1 from TL_PhuCap_MLNS_NQ104 where Ma_PhuCap = blkh.Ma_PhuCap and Nam = @NamKeHoach -1)
      UNION ALL SELECT cb.Ma_Hieu_CanBo,
                       cb.Ten_CanBo,
                       cb.Thang AS thangCb,
                       blt.Thang AS thangBl,
                       blt.Nam,
                       blt.Ma_CB,
                       blt.Ma_PhuCap,
                       0 AS luongKh,
                       blt.Gia_Tri AS luongt
      FROM TL_DM_CanBo_NQ104_KeHoach cb
      LEFT JOIN TL_BangLuong_Thang_NQ104 blt ON cb.Ma_Hieu_CanBo = blt.Ma_Hieu_CanBo
      AND cb.Thang = blt.Thang
      AND cb.Nam = @NamKeHoach
	  AND exists (select 1 from TL_PhuCap_MLNS_NQ104 where Ma_PhuCap = blt.Ma_PhuCap and Nam = @NamKeHoach - 1)
      WHERE blt.Nam = (@NamKeHoach - 2)
        AND blt.Ma_DonVi = @MaDonVi) AS blcl
   GROUP BY blcl.Ma_Hieu_CanBo,
            blcl.Ma_CB,
            blcl.Ten_CanBo,
            blcl.thangBl,
            blcl.Ma_PhuCap
END
/****** Object:  StoredProcedure [dbo].[sp_khv_khth_dexuat_dieuchinh_report]    Script Date: 15/12/2021 6:34:55 PM ******/
SET ANSI_NULLS ON
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_quyettoan_nam_thue_tncn_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TungNH
-- Create date: 20/12/2021
-- Description:	Lấy dữ liệu nộp thuế thu nhập cá nhân theo tháng
-- =============================================
CREATE PROCEDURE [dbo].[sp_tl_rpt_quyettoan_nam_thue_tncn_nq104]
	@Nam AS int,
	@MaDonVi NVARCHAR(MAX),
	@IsOrderChucVu bit = 0
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @Query AS NVARCHAR(MAX)

	DECLARE @Cols1 AS NVARCHAR(MAX) DECLARE @Cols2 AS NVARCHAR(MAX)
	SET @Cols1 = 'BHCN_TT,LHT_TT,PCCT_TT'
	SET @Cols2 = 'THUONG_TT,THUNHAPKHAC_TT,GTNN,GIAMTHUE_TT,THUETNCN_TT,THUEDANOP_TT,GTPT_TT';
	SET @Query =
	'
	WITH BangLuongThangCaHai AS (
		SELECT
			dsCapNhapBangLuong.Thang	AS Thang,
			dsCapNhapBangLuong.Nam		AS Nam,
			bangLuong.Ma_Can_Bo			AS MaCanBo,
			bangLuong.MA_PHU_CAP			AS MaPhuCap,
			bangLuong.GIA_TRI			AS GiaTri,
			bangLuong.Ma_Hieu_Can_Bo AS MaHieuCanBo
		FROM TL_BangLuong_Thang_Bridge_NQ104 bangLuong
		JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhapBangLuong
			ON bangLuong.parent = dsCapNhapBangLuong.Id
		WHERE
			bangLuong.Ma_Phu_Cap IN (SELECT * FROM f_split(''' + @Cols1 + '''))
			AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
			AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND dsCapNhapBangLuong.Status=1
	), DanhSachCanBo AS (
		SELECT Parent,
			 Ma_CanBo,
			 Ten_CanBo,
			 Ma_Hieu_CanBo,
			 Ma_CB104,
			 Ma_Cvd104
		FROM 
		(SELECT canBo.Parent,
			 canBo.Ma_CanBo,
			 canBo.Ten_CanBo,
			 canBo.Ma_Hieu_CanBo,
			 canBo.Ma_CB104,
			 canbo.Ma_Cvd104,
			 ROW_NUMBER()
			OVER (PARTITION BY canBo.Parent, canBo.Ma_Hieu_CanBo
		ORDER BY  canBo.Thang DESC) AS RowNum
		FROM TL_DM_CanBo_NQ104 canBo) as dscbt
	WHERE RowNum = 1
	), ThongTinCanBo AS (
		SELECT
			donVi.Ma_DonVi										AS MaDonVi,
			canBo.Ma_CanBo										AS MaCanBo,
			canBo.Ten_CanBo										AS TenCanBo,
			dbo.f_split_empty(canbo.Ten_CanBo)                  AS Ten,
			canBo.Ma_Hieu_CanBo                                 AS MaHieuCanBo,
			ISNULL(canBo.Ma_CB104, ''0'')							AS MaCapBac,
			ISNULL(capBac.Ten_Cb, ''0'')						AS CapBac,
			chucVu.Ma										AS MaChucVu,
			chucVu.Ten										AS TenChucVu
		FROM TL_DM_CanBo_NQ104 canBo
		INNER JOIN TL_DM_DonVi_NQ104 donVi
			ON canBo.Parent = donVi.Ma_DonVi
		LEFT JOIN TL_DM_CapBac_NQ104 capBac
			ON canBo.Ma_CB104 = capBac.Ma_Cb
		LEFT JOIN TL_DM_ChucVu_NQ104 chucVu
			ON canBo.Ma_CVd104 = chucVu.Ma
		WHERE
			canBo.IsDelete = 1
			AND canBo.Khong_Luong = 0
			AND canBo.Nam = ' + CAST(@Nam AS VARCHAR(4)) + '
	),
	BangLuongThang AS
  (SELECT dsCapNhapBangLuong.Nam AS Nam,
          bangLuong.MA_PHU_CAP AS MaPhuCap,
          bangLuong.GIA_TRI AS GiaTri,
          bangLuong.Ma_Hieu_Can_Bo AS MaHieuCanBo,
          bangLuong.Ma_Can_Bo AS MaCanBo
   FROM TL_BangLuong_Thang_Bridge_NQ104 bangLuong
   JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhapBangLuong ON bangLuong.parent = dsCapNhapBangLuong.Id
   WHERE bangLuong.Ma_Phu_Cap IN (SELECT * FROM f_split(''' + @Cols2 + '''))
     AND dsCapNhapBangLuong.Ma_CachTL=''CACH0''
     AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
     AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
     AND dsCapNhapBangLuong.Status=1 ),
	 Cach0 AS
  (SELECT MaDonVi,
          MaHieuCanBo,
          TenCanBo,
		  Ten,
          SUM(GTNN) GTNN,
          SUM(THUETNCN_TT) THUETNCN_TT,
          SUM(GIAMTHUE_TT) GIAMTHUE_TT,
          SUM(THUEDANOP_TT) THUEDANOP_TT,
          SUM(THUONG_TT) THUONG_TT,
          SUM(THUNHAPKHAC_TT) THUNHAPKHAC_TT,
		  SUM(GTPT_TT) GTPT_TT,
		  (SUM(GTPT_TT) - SUM(GTNN)) GTPT_DG_SN
   FROM
     (SELECT canBo.MaDonVi,
             canBo.MaHieuCanBo,
             canBo.TenCanBo,
			 canBo.Ten,
			 canBo.MaCapBac,
             canBo.MaCanBo,
             canBo.TenChucVu,
             bangLuong.GiaTri,
             bangLuong.MaPhuCap
      FROM BangLuongThang bangLuong
      INNER JOIN ThongTinCanBo canBo ON bangLuong.MaCanBo = canBo.MaCanBo) x PIVOT (SUM(GiaTri)
                                                                                    FOR MaPhuCap IN (' + @Cols2 + ')) pvt
   GROUP BY MaDonVi,
            MaHieuCanBo,
            TenCanBo,
			Ten),
     Ca2 AS
  (SELECT MaDonVi,
          MaHieuCanBo,
          TenCanBo,
		  Ten,
          SUM(LHT_TT) LHT_TT,
          SUM(PCCT_TT) PCCT_TT,
          SUM(BHCN_TT) BHCN_TT
   FROM
     (SELECT canBo.MaDonVi,
             canBo.MaHieuCanBo,
             canBo.TenCanBo,
			 canBo.Ten,
			 canBo.MaCapBac,
             canBo.MaCanBo,
             canBo.TenChucVu,
             bangLuong.GiaTri,
             bangLuong.MaPhuCap
      FROM BangLuongThangCaHai bangLuong
      INNER JOIN ThongTinCanBo canBo ON bangLuong.MaCanBo = canBo.MaCanBo) x PIVOT (SUM(GiaTri)
                                                                                    FOR MaPhuCap IN (' + @Cols1 + ')) pvt
		GROUP BY MaDonVi,
            MaHieuCanBo,
            TenCanBo,
			Ten)

			SELECT Cach0.MaDonVi,
       Cach0.MaHieuCanBo,
       Cach0.TenCanBo,
	   Cach0.Ten,
       Ca2.LHT_TT LHT_TT,
       Ca2.PCCT_TT PCCT_TT,
       Ca2.BHCN_TT BHCN_TT,
       Cach0.GTNN GTNN,
       Cach0.THUETNCN_TT THUETNCN_TT,
       Cach0.GIAMTHUE_TT GIAMTHUE_TT,
       Cach0.THUEDANOP_TT THUEDANOP_TT,
       Cach0.THUONG_TT THUONG_TT,
       Cach0.THUNHAPKHAC_TT THUNHAPKHAC_TT,
	   Cach0.GTPT_TT GTPT_TT,
	   Cach0.GTPT_DG_SN GTPT_DG_SN,
	   (LHT_TT + THUONG_TT + PCCT_TT + THUNHAPKHAC_TT - BHCN_TT - GTNN - GTPT_DG_SN - GIAMTHUE_TT) TINHTHUE
		FROM Cach0
		JOIN Ca2 ON Cach0.MaHieuCanBo = Ca2.MaHieuCanBo
		LEFT JOIN DanhSachCanBo ds ON Cach0.MaHieuCanBo = ds.Ma_Hieu_CanBo'

	IF @IsOrderChucVu = 1
	SET @Query = @Query +' ORDER BY ds.Ma_CB104 , Cach0.Ten ';
	ELSE 
	SET @Query = @Query +' ORDER BY ds.Ma_CB104 , Cach0.Ten ';
	execute(@Query)
	--select @Query
END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_tien_an_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_tien_an_nq104] @thang int, @nam int, @maDonVi nvarchar(MAX), @daysInMonth int AS
BEGIN
SELECT 
       canBo.Parent MaDonVi,
	   donvi.Ten_DonVi TenDonVi,
       PhuCapTienAn.MA_PHUCAP MaPhuCap,
       phucap.Ten_PhuCap TienAn,
       PhuCapTienAn.GIA_TRI DinhMuc,
	   'x' as Nhan,
	   CAST(COUNT(canBo.Ma_CanBo) as decimal) SoNguoi,
	   'x' as Nhan, 
		CASE 
			--When PhuCapTienAn.MA_PHUCAP IN ('TA_BB_DG', 'TA_TRUCTRAI_DG') THEN CAST(@daysInMonth as decimal)
			WHEN (PhuCapTienAn.HuongPC_SN IS NULL or PhuCapTienAn.HuongPC_SN = 0) AND phucap.Parent in ('TIENAN2', 'TIENAN')  THEN CAST(@daysInMonth as decimal)
			WHEN phucap.Parent in ('TIENAN2', 'TIENAN')  THEN CAST(PhuCapTienAn.HuongPC_SN as decimal)
		End SoNgay,
	   'ngày' as Dv_tinh,
	   '=' Bang,
	   (PhuCapTienAn.GIA_TRI * COUNT(canBo.Ma_CanBo) * CASE 
			--When PhuCapTienAn.MA_PHUCAP IN ('TA_BB_DG', 'TA_TRUCTRAI_DG') THEN CAST(@daysInMonth as decimal)
			WHEN (PhuCapTienAn.HuongPC_SN IS NULL or PhuCapTienAn.HuongPC_SN = 0) AND phucap.Parent in ('TIENAN2', 'TIENAN')  THEN CAST(@daysInMonth as decimal)
			WHEN phucap.Parent in ('TIENAN2', 'TIENAN')  THEN CAST(PhuCapTienAn.HuongPC_SN as decimal)
		End) ThanhTien
FROM TL_DM_CanBo_NQ104 canBo
JOIN
  (SELECT MA_CBO,
          cbopc.MA_PHUCAP,
          cbopc.GIA_TRI,
          cbopc.HuongPC_SN
   FROM TL_CanBo_PhuCap_NQ104 cbopc
   LEFT JOIN TL_DM_PhuCap_NQ104 mapc ON cbopc.MA_PHUCAP = mapc.Ma_PhuCap
   WHERE mapc.Parent IN ('TIENAN', 'TIENAN2')
     AND cbopc.GIA_TRI > 0) PhuCapTienAn ON canBo.Ma_CanBo = PhuCapTienAn.MA_CBO
JOIN TL_DM_PhuCap_NQ104 phucap ON PhuCapTienAn.MA_PHUCAP = phucap.Ma_PhuCap
JOIN TL_DM_DonVi_NQ104 donvi ON canBo.Parent = donvi.Ma_DonVi
WHERE canBo.Thang = @thang
  AND canBo.Nam = @nam
  And canbo.Parent IN (SELECT * FROM f_split(@maDonVi))
  and (canbo.IsDelete = 1 or (canbo.Ma_TangGiam = '320' and month(canbo.Ngay_XN) = @thang and year(canbo.Ngay_XN) = @nam ))
  Group By canBo.Parent,
	   donvi.Ten_DonVi,
       PhuCapTienAn.MA_PHUCAP,
	   PhuCapTienAn.HuongPC_SN,
       phucap.Ten_PhuCap,
	   phucap.Parent,
       PhuCapTienAn.GIA_TRI
End
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_tonghop_luong_theo_don_vi_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_tonghop_luong_theo_don_vi_nq104] @thang int, @nam int, @maDonVi nvarchar(MAX),
                                                                                           @donViTinh int, @maCachTl nvarchar(5) AS DECLARE @Cols AS NVARCHAR(MAX)
SET @Cols = 'LHT_TT,HSBL_TT,PCTNVK_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCCOV_TT,PCDACTHU_SUM,PCTRA_SUM,PCKHAC_SUM,LUONGTHANG_SUM,BHXHCN_TT,BHYTCN_TT,BHTNCN_TT,TA_TONG,THUETNCN_TT,TRICHLUONG_TT,GTKHAC_TT,PHAITRU_SUM,THANHTIEN,TM';

WITH BangLuongThang AS
  (SELECT dsCapNhapBangLuong.Thang AS Thang,
          dsCapNhapBangLuong.Nam AS Nam,
          bangLuong.Ma_CBo AS MaCanBo,
          bangLuong.MA_PHUCAP AS MaPhuCap,
          bangLuong.GIA_TRI AS GiaTri
   FROM TL_BangLuong_Thang_NQ104 bangLuong
   JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhapBangLuong ON bangLuong.parent = dsCapNhapBangLuong.Id
   WHERE bangLuong.Ma_PhuCap IN
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
          donvi.Ten_DonVi DoiTuong
   FROM TL_DM_CanBo_NQ104 canBo
   LEFT JOIN TL_DM_DonVi_NQ104 donvi ON canBo.Parent = donvi.Ma_DonVi
   WHERE canBo.IsDelete = 1
     AND canBo.Khong_Luong = 0
	 AND canBo.Thang = @thang
	 AND canBo.Nam = @nam),

     CanBoLuongNgach AS
  (SELECT MaCanBo,
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
             bangLuong.GiaTri,
             bangLuong.MaPhuCap
      FROM BangLuongThang bangLuong
      INNER JOIN ThongTinCanBo canBo ON bangLuong.MaCanBo = canBo.MaCanBo) x PIVOT (SUM(GiaTri)
                                                                                    FOR MaPhuCap IN (LHT_TT, HSBL_TT, PCTNVK_TT, PCCV_TT, PCTN_TT, PCKV_TT, PCCOV_TT, PCDACTHU_SUM, PCTRA_SUM, PCKHAC_SUM, LUONGTHANG_SUM, BHXHCN_TT, BHYTCN_TT, BHTNCN_TT, TA_TONG, THUETNCN_TT, TRICHLUONG_TT, GTKHAC_TT, PHAITRU_SUM, THANHTIEN, TM)) pvt)

SELECT DoiTuong,
       MaDonVi,
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
       SUM(GTKHAC_TT)/@donViTinh GTKHAC_TT,
       SUM(PHAITRU_SUM)/@donViTinh PHAITRU_SUM,
       SUM(CASE WHEN TM = 0 THEN THANHTIEN ELSE 0 END)/@donViTinh THANHTIEN_NH,
       SUM(CASE WHEN TM = 1 THEN THANHTIEN ELSE 0 END)/@donViTinh THANHTIEN
FROM CanBoLuongNgach
GROUP BY DoiTuong,
         MaDonVi
ORDER BY MaDonVi

--UNION

--SELECT N'Tổng truy lĩnh' AS DoiTuong,
--       'x' AS MaDonVi,
--       COUNT(*) SoNguoi,
--       SUM(LHT_TT)/@donViTinh LHT_TT,
--       SUM(HSBL_TT)/@donViTinh HSBL_TT,
--       SUM(PCTNVK_TT)/@donViTinh PCTNVK_TT,
--       SUM(PCCV_TT)/@donViTinh PCCV_TT,
--       SUM(PCTN_TT)/@donViTinh PCTN_TT,
--       SUM(PCKV_TT)/@donViTinh PCKV_TT,
--       SUM(PCCOV_TT)/@donViTinh PCCOV_TT,
--       SUM(PCDACTHU_SUM)/@donViTinh PCDACTHU_SUM,
--       SUM(PCTRA_SUM)/@donViTinh PCTRA_SUM,
--       SUM(PCKHAC_SUM)/@donViTinh PCKHAC_SUM,
--       SUM(LUONGTHANG_SUM)/@donViTinh LUONGTHANG_SUM,
--       SUM(BHXHCN_TT)/@donViTinh BHXHCN_TT,
--       SUM(BHYTCN_TT)/@donViTinh BHYTCN_TT,
--       SUM(BHTNCN_TT)/@donViTinh BHTNCN_TT,
--       SUM(TA_TONG)/@donViTinh TA_TONG,
--       SUM(THUETNCN_TT)/@donViTinh THUETNCN_TT,
--       SUM(TRICHLUONG_TT)/@donViTinh TRICHLUONG_TT,
--       SUM(GTKHAC_TT)/@donViTinh GTKHAC_TT,
--       SUM(PHAITRU_SUM)/@donViTinh PHAITRU_SUM,
--       SUM(THANHTIEN)/@donViTinh THANHTIEN
--FROM CanBoLuongTruyLinh
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_don_vi_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
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
          case when capbac.Parent = '3.3' then '3' else capbac.Parent end as Ngach
   FROM TL_DM_CanBo_NQ104 canBo
   LEFT JOIN TL_DM_DonVi_NQ104 donvi ON canBo.Parent = donvi.Ma_DonVi
   LEFT JOIN TL_DM_CapBac_NQ104 capbac ON canBo.Ma_CB = capbac.Ma_Cb
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
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_nq104] @thang int, @nam int, @maDonVi nvarchar(MAX),
                                                                                          @donViTinh int, @isSummary bit, @maCachTl nvarchar(100) AS DECLARE @Cols AS NVARCHAR(MAX)
SET @Cols = 'LHT_TT,HSBL_TT,PCTNVK_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCCOV_TT,PCDACTHU_SUM,PCTRA_SUM,PCKHAC_SUM,LUONGTHANG_SUM,BHXHCN_TT,BHYTCN_TT,BHTNCN_TT,TA_TONG,THUETNCN_TT,TRICHLUONG_TT,GTKHAC_TT,PHAITRU_SUM,THANHTIEN,TM';

WITH BangLuongThang AS
  (SELECT dsCapNhapBangLuong.Thang AS Thang,
          dsCapNhapBangLuong.Nam AS Nam,
          bangLuong.ma_can_bo AS MaCanBo,
          bangLuong.ma_phu_cap AS MaPhuCap,
		  dsCapNhapBangLuong.Ma_CBo MaDonVi,
          bangLuong.GIA_TRI AS GiaTri
   FROM TL_BangLuong_Thang_Bridge_NQ104 bangLuong
   JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhapBangLuong ON bangLuong.parent = dsCapNhapBangLuong.Id
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
  (SELECT canBo.Ma_CanBo AS MaCanBo,
          canBo.Ten_CanBo AS TenCanBo,
          ISNULL(canBo.ma_cb104, '0') AS MaCapBac,
          ISNULL(capBac.TenCapBac, '0') AS TenNgach,
          ISNULL(capBac.MaCapBacCha, '0') AS MaNgach
   FROM TL_DM_CanBo_NQ104 canBo
   INNER JOIN TL_DM_DonVi_NQ104 donVi ON canBo.Parent=donVi.Ma_DonVi AND canBo.Parent IN (SELECT * FROM f_split(@maDonVi))
   LEFT JOIN
     (SELECT capbaccon.Ma_Cb MaCapBac,
             capbaccha.Ma_Cb MaCapBacCha,
             capbaccha.ten_cb TenCapBac
      FROM TL_DM_CapBac_NQ104 capbaccon
      LEFT JOIN TL_DM_CapBac_NQ104 capbaccha ON capbaccon.Parent = capbaccha.Ma_Cb) capBac ON canBo.ma_cb104=capBac.MaCapBac
   LEFT JOIN TL_DM_ChucVu_NQ104 chucVu ON canBo.Ma_CV=chucVu.ma
   WHERE canBo.IsDelete = 1
     AND canBo.Khong_Luong = 0
	 AND canBo.Thang = @thang
	 AND canBo.Nam = @nam)

	 SELECT TenNgach,
          MaNgach,
		  MaDonVi,
          COUNT(MaCanBo) AS SoNguoi,
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
       SUM(GTKHAC_TT)/@donViTinh GTKHAC_TT,
       SUM(PHAITRU_SUM)/@donViTinh PHAITRU_SUM,
	   SUM(CASE WHEN TM = 0 THEN THANHTIEN ELSE 0 END)/@donViTinh THANHTIEN_NH,
       SUM(CASE WHEN TM = 1 THEN THANHTIEN ELSE 0 END)/@donViTinh THANHTIEN
   FROM
     (SELECT bangLuong.Thang AS Thang,
             bangLuong.Nam AS Nam,
             canBo.MaCanBo,
             canBo.MaCapBac,
             canBo.TenNgach,
             canBo.MaNgach,
             bangLuong.GiaTri,
             bangLuong.MaPhuCap,
			 CASE When @isSummary = 1 Then '1' Else bangLuong.MaDonVi end as MaDonVi
      FROM BangLuongThang bangLuong
      INNER JOIN ThongTinCanBo canBo ON bangLuong.MaCanBo = canBo.MaCanBo) x PIVOT (SUM(GiaTri)
                                                                                    FOR MaPhuCap IN (LHT_TT, HSBL_TT, PCTNVK_TT, PCCV_TT, PCTN_TT, PCKV_TT, PCCOV_TT, PCDACTHU_SUM, PCTRA_SUM, PCKHAC_SUM, LUONGTHANG_SUM, BHXHCN_TT, BHYTCN_TT, BHTNCN_TT, TA_TONG, THUETNCN_TT, TRICHLUONG_TT, GTKHAC_TT, PHAITRU_SUM, THANHTIEN, TM)) pvt
	GROUP BY TenNgach,
         MaNgach, MaDonVi
	Order By MaDonVi, MaNgach
	
--UNION
--	SELECT TenNgach,
--		MaNgach,
--		MaDonVi,
--		COUNT(MaCanBo) AS SoNguoi,
--		SUM(LHT_TT)/@donViTinh LHT_TT,
--		SUM(HSBL_TT)/@donViTinh HSBL_TT,
--       SUM(PCTNVK_TT)/@donViTinh PCTNVK_TT,
--       SUM(PCCV_TT)/@donViTinh PCCV_TT,
--       SUM(PCTN_TT)/@donViTinh PCTN_TT,
--       SUM(PCKV_TT)/@donViTinh PCKV_TT,
--       SUM(PCCOV_TT)/@donViTinh PCCOV_TT,
--       SUM(PCDACTHU_SUM)/@donViTinh PCDACTHU_SUM,
--       SUM(PCTRA_SUM)/@donViTinh PCTRA_SUM,
--       SUM(PCKHAC_SUM)/@donViTinh PCKHAC_SUM,
--       SUM(LUONGTHANG_SUM)/@donViTinh LUONGTHANG_SUM,
--       SUM(BHXHCN_TT)/@donViTinh BHXHCN_TT,
--       SUM(BHYTCN_TT)/@donViTinh BHYTCN_TT,
--       SUM(BHTNCN_TT)/@donViTinh BHTNCN_TT,
--       SUM(TA_TONG)/@donViTinh TA_TONG,
--       SUM(THUETNCN_TT)/@donViTinh THUETNCN_TT,
--       SUM(TRICHLUONG_TT)/@donViTinh TRICHLUONG_TT,
--       SUM(GTKHAC_TT)/@donViTinh GTKHAC_TT,
--       SUM(PHAITRU_SUM)/@donViTinh PHAITRU_SUM,
--       SUM(THANHTIEN)/@donViTinh THANHTIEN
--	FROM 
--	(SELECT 
--		N'Tổng truy lĩnh' AS TenNgach,
--		'x' AS MaNgach,
--		bl.GiaTri, bl.MaPhuCap, 
--		CASE When @isSummary = 1 Then '1' Else bl.MaDonVi end as MaDonVi,
--		bl.MaCanBo
--		FROM BangLuongTruyLinh bl INNER JOIN ThongTinCanBo
--			ON bl.MaCanBo = ThongTinCanBo.MaCanBo) x PIVOT (SUM(GiaTri)
--															FOR MaPhuCap IN (LHT_TT, HSBL_TT, PCTNVK_TT, PCCV_TT, PCTN_TT, PCKV_TT, PCCOV_TT, PCDACTHU_SUM, PCTRA_SUM, PCKHAC_SUM, LUONGTHANG_SUM, BHXHCN_TT, BHYTCN_TT, BHTNCN_TT, TA_TONG, THUETNCN_TT, TRICHLUONG_TT, GTKHAC_TT, PHAITRU_SUM, THANHTIEN)) pvt
--	GROUP BY MaDonVi, TenNgach, MaNgach
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_truylinh_chuyenchedo_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
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
   FROM TL_DM_CanBo_NQ104 canBo
   INNER JOIN TL_DM_DonVi_NQ104 donVi ON canBo.Parent=donVi.Ma_DonVi
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
   FROM TL_DM_CanBo_NQ104 canBo
   INNER JOIN TL_DM_DonVi_NQ104 donVi ON canBo.Parent=donVi.Ma_DonVi
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
   FROM TL_DM_CanBo_NQ104 canBo
   INNER JOIN TL_DM_DonVi_NQ104 donVi ON canBo.Parent=donVi.Ma_DonVi
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
   FROM TL_DM_CanBo_NQ104 canBo
   INNER JOIN TL_DM_DonVi_NQ104 donVi ON canBo.Parent=donVi.Ma_DonVi
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
/****** Object:  StoredProcedure [dbo].[sp_tl_update_dmcapbac_canbo_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_tl_update_dmcapbac_canbo_nq104]
@nam int,
@thang int,
@bIsDelete bit,
@capbacIds t_tbl_uniqueidentifier READONLY,
@sMaPhuCapChange nvarchar(MAX)
AS
BEGIN
--DECLARE @tmp as TABLE(sMaPhuCap nvarchar(200))
--INSERT INTO @tmp(sMaPhuCap) VALUES('PCTN_HS'), ('PCTEMTHU_TT'), ('PCNU_HS'), ('PCANQP_HS'), ('THANG_TCXN'), ('BHXHDV_HS'), ('BHXHCN_HS'), ('BHYTDV_HS'), ('BHYTCN_HS'), ('BHTNDV_HS'), ('BHTNCN_HS'), ('LHT_HS'), ('TILE_HUONG'), ('BHXHDVCS_HS'), ('BHYTDVCS_HS')

SELECT cb.* INTO #tmpCapBac
FROM @capbacIds as tmp
INNER JOIN TL_DM_CapBac_NQ104 as cb on tmp.Id = cb.Id

SELECT canbo.Ma_CanBo, capbac.*, canbo.Ngay_XN, canbo.Ngay_NN INTO #tmpCanBoPhuCap
FROM TL_DM_CanBo_NQ104 as canbo
INNER JOIN #tmpCapBac as capbac on canbo.Ma_CB = capbac.Ma_Cb
WHERE Thang = @thang AND Nam = @nam

IF(@bIsDelete = 0)
BEGIN
UPDATE pc
SET
GIA_TRI = (CASE WHEN pc.MA_PHUCAP = 'PCTN_HS' THEN (CASE WHEN tbl.Ma_Cb LIKE '0%' THEN 0 ELSE GIA_TRI END)
WHEN pc.MA_PHUCAP = 'PCTEMTHU_TT' THEN (CASE WHEN tbl.Ma_Cb NOT LIKE '0%' THEN 0 ELSE GIA_TRI END)
WHEN pc.MA_PHUCAP = 'PCNU_HS' THEN (CASE WHEN tbl.Ma_Cb NOT LIKE '0%' THEN 0 ELSE GIA_TRI END)
WHEN pc.MA_PHUCAP = 'PCANQP_HS' THEN (CASE WHEN tbl.Ma_Cb = '415' THEN 0.5
WHEN tbl.Ma_Cb = '413' THEN 0.3
ELSE GIA_TRI END)
WHEN pc.MA_PHUCAP = 'THANG_TCXN' THEN (CASE WHEN tbl.Ma_Cb LIKE '0%' AND tbl.Ngay_XN IS NOT NULL THEN ((DATEDIFF(MONTH, tbl.Ngay_NN, tbl.Ngay_XN)/12)*2) + (CASE WHEN (DATEDIFF(MONTH, tbl.Ngay_NN, tbl.Ngay_XN)%12) > 6 THEN 2 ELSE 1 END) ELSE GIA_TRI END)
WHEN pc.Ma_PhuCap = 'BHXHDVCS_HS' THEN ISNULL(Bhxh_Cq, 0)
WHEN pc.Ma_PhuCap = 'BHXHDV_HS' THEN ISNULL(Bhxh_Cq, 0)
WHEN pc.MA_PHUCAP = 'BHXHCN_HS' THEN ISNULL(Hs_Bhxh, 0)
WHEN pc.MA_PHUCAP = 'BHYTDVCS_HS' THEN ISNULL(Bhyt_Cq, 0)
WHEN pc.MA_PHUCAP = 'BHYTDV_HS' THEN ISNULL(Bhyt_Cq, 0)
WHEN pc.MA_PHUCAP = 'BHYTCN_HS' THEN ISNULL(Hs_Bhyt, 0)
WHEN pc.MA_PHUCAP = 'BHTNDV_HS' THEN ISNULL(Bhtn_Cq, 0)
WHEN pc.MA_PHUCAP = 'BHTNCN_HS' THEN ISNULL(Hs_Bhtn, 0)
WHEN pc.MA_PHUCAP = 'LHT_HS' THEN ISNULL(tbl.Lht_Hs, 0)
WHEN pc.MA_PHUCAP = 'TILE_HUONG' THEN ISNULL(tbl.TiLeHuong, 0)
ELSE GIA_TRI END)
FROM #tmpCanBoPhuCap as tbl
INNER JOIN TL_CanBo_PhuCap_NQ104 as pc on tbl.Ma_CanBo = pc.MA_CBO
WHERE pc.MA_PHUCAP in (select * from f_split(@sMaPhuCapChange))
--INNER JOIN @tmp as tmp on pc.MA_PHUCAP = tmp.sMaPhuCap

UPDATE cb
SET
HeSoLuong = tbl.Lht_Hs,
PCCV = (CASE WHEN tbl.Ma_Cb LIKE '0%' THEN 1 ELSE PCCV END)
FROM #tmpCanBoPhuCap as tbl
INNER JOIN TL_DM_CanBo_NQ104 as cb on tbl.Ma_CanBo = cb.Ma_CanBo
INNER JOIN TL_CanBo_PhuCap_NQ104 as pc on tbl.Ma_CanBo = pc.MA_CBO
--WHERE pc.MA_PHUCAP = 'LHT_HS'
WHERE pc.MA_PHUCAP = 'LHT_HS' and pc.MA_PHUCAP in (select * from f_split(@sMaPhuCapChange))
END
ELSE
BEGIN
UPDATE pc
SET
GIA_TRI = (CASE
WHEN pc.MA_PHUCAP = 'PCANQP_HS' THEN 0
WHEN pc.MA_PHUCAP = 'THANG_TCXN' THEN (CASE WHEN tbl.Ma_Cb LIKE '0%' AND tbl.Ngay_XN IS NOT NULL THEN 0 ELSE GIA_TRI END) -- TinhThangHuongTcxn()
WHEN pc.Ma_PhuCap = 'BHXHDVCS_HS' THEN 0
WHEN pc.Ma_PhuCap = 'BHXHDV_HS' THEN 0
WHEN pc.MA_PHUCAP = 'BHXHCN_HS' THEN 0
WHEN pc.MA_PHUCAP = 'BHYTDVCS_HS' THEN 0
WHEN pc.MA_PHUCAP = 'BHYTDV_HS' THEN 0
WHEN pc.MA_PHUCAP = 'BHYTCN_HS' THEN 0
WHEN pc.MA_PHUCAP = 'BHTNDV_HS' THEN 0
WHEN pc.MA_PHUCAP = 'BHTNCN_HS' THEN 0
WHEN pc.MA_PHUCAP = 'LHT_HS' THEN 0
WHEN pc.MA_PHUCAP = 'TILE_HUONG' THEN 0
ELSE GIA_TRI END)
FROM #tmpCanBoPhuCap as tbl
INNER JOIN TL_CanBo_PhuCap_NQ104 as pc on tbl.Ma_CanBo = pc.MA_CBO
--INNER JOIN @tmp as tmp on pc.MA_PHUCAP = tmp.sMaPhuCap
WHERE pc.MA_PHUCAP in (select * from f_split(@sMaPhuCapChange))

UPDATE cb
SET
HeSoLuong = 0
FROM #tmpCanBoPhuCap as tbl
INNER JOIN TL_DM_CanBo_NQ104 as cb on tbl.Ma_CanBo = cb.Ma_CanBo
INNER JOIN TL_CanBo_PhuCap_NQ104 as pc on tbl.Ma_CanBo = pc.MA_CBO
--WHERE pc.MA_PHUCAP = 'LHT_HS' 
WHERE pc.MA_PHUCAP = 'LHT_HS' and pc.MA_PHUCAP in (select * from f_split(@sMaPhuCapChange))
END

DROP TABLE #tmpCapBac
DROP TABLE #tmpCanBoPhuCap
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_update_dmphucap_canbo_nq104]    Script Date: 4/4/2024 8:56:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_tl_update_dmphucap_canbo_nq104]
@nam int,
@thang int,
@bIsDelete bit,
@phucapIds t_tbl_uniqueidentifier READONLY
AS
BEGIN
	SELECT Ma_PhuCap, Gia_Tri, Ten_NganHang INTO #tmpPC
	FROM @phucapIds as tmp
	INNER JOIN TL_DM_PhuCap_NQ104 as pc on tmp.Id = pc.Id
	
	SELECT cb.Ma_CanBo, pc.Gia_Tri, pc.Ma_PhuCap INTO #tmpCanBoPC
	FROM TL_DM_CanBo_NQ104 as cb, #tmpPC as pc
	WHERE cb.Nam > @nam OR (cb.Thang >= @thang AND cb.Nam = @nam)

	IF(@bIsDelete = 0)
	BEGIN
		CREATE TABLE #tmpUpdate(MaCB nvarchar(100), MaPC nvarchar(500))

		UPDATE tbl
		SET GIA_TRI = pc.Gia_Tri
		OUTPUT inserted.MA_CBO, inserted.MA_PHUCAP INTO #tmpUpdate(MaCB, MaPC)
		FROM TL_CanBo_PhuCap_NQ104 as tbl
		INNER JOIN #tmpCanBoPC as pc ON tbl.MA_PHUCAP = pc.Ma_PhuCap AND tbl.MA_CBO = pc.Ma_CanBo

		INSERT INTO TL_CanBo_PhuCap_NQ104(bSaoChep, CHON, CONG_THUC, GIA_TRI, HE_SO, HuongPC_SN, MA_CBO, MA_KMCP, MA_PHUCAP, PHANTRAM_CT)
		SELECT pc.bSaoChep, pc.CHON, pc.CONG_THUC, tbl.Gia_Tri, HE_SO, HuongPC_SN, tbl.Ma_CanBo, MA_KMCP, tbl.Ma_PhuCap, PHANTRAM_CT
		FROM #tmpCanBoPC as tbl
		INNER JOIN TL_DM_PhuCap_NQ104 as pc on tbl.Ma_PhuCap = pc.Ma_PhuCap
		LEFT JOIN #tmpUpdate as dt on tbl.Ma_CanBo = dt.MaCB AND tbl.Ma_PhuCap = dt.MaPC
		WHERE dt.MaCB IS NULL OR dt.MaPC IS NULL

		IF (EXISTS (SELECT * FROM #tmpPC WHERE Ma_PhuCap = 'TENNGANHANG'))
		BEGIN
			UPDATE TL_DM_CanBo_NQ104 SET Ten_KhoBac = (SELECT Ten_NganHang FROM TL_DM_PhuCap_NQ104 WHERE Ma_PhuCap = 'TENNGANHANG')
			WHERE  Nam > @nam OR (Thang >= @thang AND Nam = @nam)
		END
		DROP TABLE #tmpUpdate
	END
	ELSE
	BEGIN
		DELETE tbl
		FROM TL_CanBo_PhuCap_NQ104 as tbl
		INNER JOIN #tmpCanBoPC as pc ON tbl.MA_PHUCAP = pc.Ma_PhuCap AND tbl.MA_CBO = pc.Ma_CanBo
	END

	DROP TABLE #tmpPC
	DROP TABLE #tmpCanBoPC
END
;
;
GO
