IF EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[NH_TH_TongHop]') 
         AND name = 'iID_TiGia'
)
ALTER TABLE [dbo].[NH_TH_TongHop]
DROP COLUMN [iID_TiGia];
ALTER TABLE [dbo].[NH_TH_TongHop]
ADD [iID_TiGia] [uniqueidentifier] NULL;

/****** Object:  StoredProcedure [dbo].[sp_vdt_qt_baocaoquyettoanniendo_phantich]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_qt_baocaoquyettoanniendo_phantich]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_qt_baocaoquyettoanniendo_phantich]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_kehoachvonung_index]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_kehoachvonung_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_kehoachvonung_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_getthongtriquyettoanchitiet_by_id]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_getthongtriquyettoanchitiet_by_id]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_getthongtriquyettoanchitiet_by_id]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_getthongtriquyettoanchitiet]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_getthongtriquyettoanchitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_getthongtriquyettoanchitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_von_bo_tri_5_nam]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_get_von_bo_tri_5_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_get_von_bo_tri_5_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_phe_duyet_quyet_toan]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_get_phe_duyet_quyet_toan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_get_phe_duyet_quyet_toan]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_duan_in_phanbovon_new_2]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_get_duan_in_phanbovon_new_2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_get_duan_in_phanbovon_new_2]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_denghiqt_by_idduan]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_get_denghiqt_by_idduan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_get_denghiqt_by_idduan]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_baocaodquyettoanniendo1]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_get_baocaodquyettoanniendo1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_get_baocaodquyettoanniendo1]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_baocaodquyettoanniendo_vonung]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_get_baocaodquyettoanniendo_vonung]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_get_baocaodquyettoanniendo_vonung]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_all_qddt_nguonvon_by_list_dutoan_id]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_get_all_qddt_nguonvon_by_list_dutoan_id]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_get_all_qddt_nguonvon_by_list_dutoan_id]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_all_dutoan_chiphi_by_duan_quyettoanchitiet_1]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_get_all_dutoan_chiphi_by_duan_quyettoanchitiet_1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_get_all_dutoan_chiphi_by_duan_quyettoanchitiet_1]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_find_phanbovondonvichitietpheduyet]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_find_phanbovondonvichitietpheduyet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_find_phanbovondonvichitietpheduyet]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_bcdutoan_loaicongtrinh]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_bcdutoan_loaicongtrinh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_bcdutoan_loaicongtrinh]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_danhsach_capphat_phucap]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_danhsach_capphat_phucap]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_danhsach_capphat_phucap]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_2]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangthanhtoan_luongthang_2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_2]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangluong_thang_giaithich_chitiet_theo_ngach_capbac]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangluong_thang_giaithich_chitiet_theo_ngach_capbac]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangluong_thang_giaithich_chitiet_theo_ngach_capbac]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_phucap]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_data_phucap]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_data_phucap]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_ct_chi_tiet_export]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_data_ct_chi_tiet_export]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_data_ct_chi_tiet_export]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_ct_chi_tiet]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_data_ct_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_data_ct_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_chungtu_chitiet_namkehoach]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_chungtu_chitiet_namkehoach]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_chungtu_chitiet_namkehoach]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_chungtu_chitiet]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_chungtu_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_chungtu_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_thang_dulieu_insert]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_bangluong_thang_dulieu_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_bangluong_thang_dulieu_insert]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thuchien_ngansach_report]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_thuchien_ngansach_report]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_thuchien_ngansach_report]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thuchien_ngansach_index]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_thuchien_ngansach_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_thuchien_ngansach_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtinthanhtoan_index]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_thongtinthanhtoan_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_thongtinthanhtoan_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtin_hopdong_ngoaithuong_index]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_thongtin_hopdong_ngoaithuong_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_thongtin_hopdong_ngoaithuong_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtin_hopdong_index]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_thongtin_hopdong_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_thongtin_hopdong_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_nhucauchiquy_chitiet_byIDHopDong]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_nhucauchiquy_chitiet_byIDHopDong]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_nhucauchiquy_chitiet_byIDHopDong]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_khlcnhathau_index_2]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_khlcnhathau_index_2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_khlcnhathau_index_2]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_kehoachtongthe_index]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_kehoachtongthe_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_kehoachtongthe_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_hangmuc_bygoithauid]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_hangmuc_bygoithauid]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_hangmuc_bygoithauid]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_trongnuoc_index]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_goithau_trongnuoc_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_goithau_trongnuoc_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_nguonvon_by_khlcnt_listall]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_goithau_nguonvon_by_khlcnt_listall]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_goithau_nguonvon_by_khlcnt_listall]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_hangmuc_by_goithau]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_goithau_hangmuc_by_goithau]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_goithau_hangmuc_by_goithau]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_chiphi_by_khlcnt_listall]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_goithau_chiphi_by_khlcnt_listall]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_goithau_chiphi_by_khlcnt_listall]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_chiphi_by_GoiThau_listall]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_goithau_chiphi_by_GoiThau_listall]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_goithau_chiphi_by_GoiThau_listall]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_chiphi_by_goithau]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_goithau_chiphi_by_goithau]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_goithau_chiphi_by_goithau]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_gethangmucbyidgoithau]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_gethangmucbyidgoithau]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_gethangmucbyidgoithau]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_get_data_report_tinh_hinh_thuc_hien_du_an1]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_get_data_report_tinh_hinh_thuc_hien_du_an1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_get_data_report_tinh_hinh_thuc_hien_du_an1]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_dutoan_index]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_dutoan_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_dutoan_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_dutoan_in_khlcnt]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_dutoan_in_khlcnt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_dutoan_in_khlcnt]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_duan_find_from_qddautu]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_duan_find_from_qddautu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_duan_find_from_qddautu]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_duan_find_from_dutoan]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_duan_find_from_dutoan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_duan_find_from_dutoan]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_duan_find_from_chutruong_dautu]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_duan_find_from_chutruong_dautu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_duan_find_from_chutruong_dautu]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_chenhlechtigia_index]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_chenhlechtigia_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_chenhlechtigia_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_baocao_nhucau_chitiet]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_baocao_nhucau_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_baocao_nhucau_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_luong_get_dmphucap_dctapthecanbo]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_luong_get_dmphucap_dctapthecanbo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_luong_get_dmphucap_dctapthecanbo]
GO
/****** Object:  StoredProcedure [dbo].[sp_insert_nhtonghop]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_insert_nhtonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_insert_nhtonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_insert_nh_tonghop_tang]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_insert_nh_tonghop_tang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_insert_nh_tonghop_tang]
GO
/****** Object:  StoredProcedure [dbo].[sp_insert_nh_tonghop_giam]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_insert_nh_tonghop_giam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_insert_nh_tonghop_giam]
GO
/****** Object:  StoredProcedure [dbo].[sp_get_thongtriquyettoan_chitiet]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_get_thongtriquyettoan_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_get_thongtriquyettoan_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_get_khluachonnhathau_byduanid]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_get_khluachonnhathau_byduanid]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_get_khluachonnhathau_byduanid]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_rpt_dutoan_theodot]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_rpt_dutoan_theodot]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_rpt_dutoan_theodot]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dutoan_dotnhan_phanbo_find_all]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_dutoan_dotnhan_phanbo_find_all]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_dutoan_dotnhan_phanbo_find_all]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dutoan_donvi]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_dutoan_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_dutoan_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_delete_nh_tonghop_giam]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_delete_nh_tonghop_giam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_delete_nh_tonghop_giam]
GO
/****** Object:  StoredProcedure [dbo].[rpt_ds_chi_tra_ca_nhan_ngan_hang]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rpt_ds_chi_tra_ca_nhan_ngan_hang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rpt_ds_chi_tra_ca_nhan_ngan_hang]
GO
/****** Object:  StoredProcedure [dbo].[proc_get_all_nh_baocaoquyettoanniendo_paging]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_get_all_nh_baocaoquyettoanniendo_paging]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_get_all_nh_baocaoquyettoanniendo_paging]
GO
/****** Object:  StoredProcedure [dbo].[proc_get_all_nh_baocaoquyettoanniendo_detail]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_get_all_nh_baocaoquyettoanniendo_detail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_get_all_nh_baocaoquyettoanniendo_detail]
GO
/****** Object:  StoredProcedure [dbo].[proc_get_all_nh_baocao_ketluanbaocao]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_get_all_nh_baocao_ketluanbaocao]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_get_all_nh_baocao_ketluanbaocao]
GO
/****** Object:  UserDefinedFunction [dbo].[f_quyettoan_tonghop]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_quyettoan_tonghop]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_quyettoan_tonghop]
GO
/****** Object:  UserDefinedFunction [dbo].[f_quyettoan]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_quyettoan]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_quyettoan]
GO
/****** Object:  UserDefinedFunction [dbo].[f_qt_dot]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_qt_dot]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_qt_dot]
GO
/****** Object:  UserDefinedFunction [dbo].[f_qt_done]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_qt_done]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_qt_done]
GO
/****** Object:  UserDefinedFunction [dbo].[f_ns_DuToanDonVi]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_ns_DuToanDonVi]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_ns_DuToanDonVi]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_RemoveLeadingZero]    Script Date: 25/11/2022 7:43:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_RemoveLeadingZero]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fn_RemoveLeadingZero]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_RemoveLeadingZero]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fn_RemoveLeadingZero]
(
	@str VARCHAR(MAX),
	@delimiter VARCHAR(MAX)
) RETURNS VARCHAR(MAX)
BEGIN
	DECLARE @index INT
	SET @str = RIGHT(@str, len(@str) - PATINDEX('%[1-9]%', @str) + 1) --Remove leftmost zero
	SET @index = charindex(concat(@delimiter,'0'),@str)
	WHILE(@index > 0)
		BEGIN
			SET @str = replace(@str, concat(@delimiter,'0'), @delimiter)
			SET @index = charindex(concat(@delimiter,'0'), @str)
		END

	RETURN @str
END;
GO
/****** Object:  UserDefinedFunction [dbo].[f_ns_DuToanDonVi]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create FUNCTION [dbo].[f_ns_DuToanDonVi]
(    
      @NamLamViec int,
	  @NamNganSach int,
	  @NguonNganSach int,
	  @NgayChungTu datetime,
	  @id_donvi nvarchar(max),
	  @lns nvarchar(max),
	  @dvt int,
	  @bkhoa int
)
RETURNS TABLE
AS RETURN

	SELECT sLNS,
		   sL,
		   sK,
		   sM,
		   sTM,
		   sTTM,
		   sNG,
		   sTNG,
		   sXauNoiMa,
		   iID_MaDonVi,
		   TuChi =sum(fTuChi) /@dvt,
		   HienVat =sum(fHienVat) /@dvt,
		   sMoTa,
		   iID_MLNS as iID_MLNS
	FROM NS_DT_ChungTuChiTiet
	WHERE iNamLamViec=@NamLamViec
	  AND (@NamNganSach IS NULL
		   OR iNamNganSach in
			 (SELECT *
			  FROM f_split(@NamNganSach)))
	  AND (@NguonNganSach IS NULL
		   OR iID_MaNguonNganSach in
			 (SELECT *
			  FROM f_split(@NguonNganSach)))
	  AND (@id_donvi IS NULL
		   OR iID_MaDonVi in
			 (SELECT *
			  FROM f_split(@id_donvi)))
	  AND (@lns IS NULL
		   OR sLNS in
			 (SELECT *
			  FROM f_split(@lns)))
	  AND iID_DTChungTu in
		(SELECT iID_DTChungTu
		 FROM NS_DT_ChungTu
		 WHERE iNamLamViec=@NamLamViec
		   AND (@NamNganSach IS NULL
				OR iNamNganSach in
				  (SELECT *
				   FROM f_split(@NamNganSach)))
		   AND (@NguonNganSach IS NULL
				OR iID_MaNguonNganSach in
				  (SELECT *
				   FROM f_split(@NguonNganSach)))
		   AND ((@bkhoa = 0) or (@bkhoa = 1 and bKhoa = @bkhoa))
		   AND cast(dNgayChungTu AS date) <= cast(@NgayChungTu AS date))
		   
	GROUP BY sLNS,
			 sL,
			 sK,
			 sM,
			 sTM,
			 sTTM,
			 sNG,
			 sTNG,
			 sXauNoiMa,
			 iID_MaDonVi,
			 sMoTa,
			 iID_MLNS
;
;
GO
/****** Object:  UserDefinedFunction [dbo].[f_qt_done]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*

author:		longsam
date:		11/08/2019
desc:		Lay so da quyet toan

select * from [f_ns](2019,null,null,getdate(),null,null,1)

*/

CREATE FUNCTION [dbo].[f_qt_done]
(    
      @NamLamViec int,
	  @NamNganSach nvarchar(20),
	  @NguonNganSach nvarchar(20),
	  @ithangquyloai int,
	  @ithangquy nvarchar(100),
	  @id_donvi nvarchar(max),
	  @lns nvarchar(max)
)
RETURNS TABLE
AS RETURN

	SELECT sLNS,
		   sL,
		   sK,
		   sM,
		   sTM,
		   sTTM,
		   sNG,
		   sTNG,
		   sTNG1,
		   sTNG2,
		   sTNG3,
		   sXauNoiMa,
		   iID_MaDonVi,
		   TuChi = Sum(fTuChi_PheDuyet),
		   cast(0 AS float) AS HienVat,
		   SoNguoi = Sum(fSoNguoi),
		   SoNgay = Sum(fSoNgay),
		   SoLuot = Sum(fSoLuot)
	FROM NS_QT_ChungTuChiTiet
	WHERE iNamLamViec=@NamLamViec
	  AND (@NamNganSach IS NULL
		   OR iNamNganSach in
			 (SELECT *
			  FROM f_split(@NamNganSach)))
	  AND (@NguonNganSach IS NULL
		   OR iID_MaNguonNganSach in
			 (SELECT *
			  FROM f_split(@NguonNganSach)))
	  AND (@id_donvi IS NULL
		   OR iID_MaDonVi in
			 (SELECT *
			  FROM f_split(@id_donvi)))
	  AND (@lns IS NULL
		   OR sLNS in
			 (SELECT *
			  FROM f_split(@lns)))
      AND iThangQuyLoai = @ithangquyloai
	  AND (iThangQuy in (select * from f_split(@ithangquy)))
	GROUP BY sLNS,
			 sL,
			 sK,
			 sM,
			 sTM,
			 sTTM,
			 sNG,
			 sTNG,
			 sTNG1,
			 sTNG2,
			 sTNG3,
			 sXauNoiMa,
			 iID_MaDonVi
	HAVING sum(fTuChi_PheDuyet)<>0
;
;
;
GO
/****** Object:  UserDefinedFunction [dbo].[f_qt_dot]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*

author:		longsam
date:		11/08/2019
desc:		Lay so da quyet toan

select * from [f_ns](2019,null,null,getdate(),null,null,1)

*/

CREATE FUNCTION [dbo].[f_qt_dot]
(    
      @NamLamViec int,
	  @NamNganSach nvarchar(20),
	  @NguonNganSach nvarchar(20),
	  @ithangquyloai int,
	  @ithangquy nvarchar(100),
	  @id_donvi nvarchar(max),
	  @lns nvarchar(max)
)
RETURNS TABLE
AS RETURN

	SELECT sLNS,
		   sL,
		   sK,
		   sM,
		   sTM,
		   sTTM,
		   sNG,
		   sTNG,
		   sTNG1,
		   sTNG2,
		   sTNG3,
		   sXauNoiMa,
		   iID_MaDonVi,
		   TuChi =sum(fTuChi_PheDuyet),
		   cast(0 AS float) AS HienVat,
		   SoNguoi =sum(fSoNguoi),
		   SoNgay =sum(fSoNgay),
		   SoLuot =sum(fSoLuot)
	FROM NS_QT_ChungTuChiTiet
	WHERE iNamLamViec=@NamLamViec
	  AND (@NamNganSach IS NULL
		   OR iNamNganSach in
			 (SELECT *
			  FROM f_split(@NamNganSach)))
	  AND (@NguonNganSach IS NULL
		   OR iID_MaNguonNganSach in
			 (SELECT *
			  FROM f_split(@NguonNganSach)))
	  AND (@id_donvi IS NULL
		   OR iID_MaDonVi in
			 (SELECT *
			  FROM f_split(@id_donvi)))
	  AND (@lns IS NULL
		   OR sLNS in
			 (SELECT *
			  FROM f_split(@lns)))
	  AND iThangQuyLoai = @ithangquyloai
	  AND (@ithangquy IS NULL
		   OR iThangQuy in
			 (SELECT *
			  FROM f_split(@ithangquy)))
	GROUP BY sLNS,
			 sL,
			 sK,
			 sM,
			 sTM,
			 sTTM,
			 sNG,
			 sTNG,
			 sTNG1,
			 sTNG2,
			 sTNG3,
			 sXauNoiMa,
			 iID_MaDonVi
	HAVING sum(fTuChi_PheDuyet)<>0
;
;
;
GO
/****** Object:  UserDefinedFunction [dbo].[f_quyettoan]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[f_quyettoan]
(	
	@NamLamViec int,
	@NamNganSach nvarchar(20),
	@NguonNganSach nvarchar(20),
	@ithangquy nvarchar(100),
	@IdDonVi nvarchar(max),
	@lns nvarchar(max)
)
RETURNS TABLE 
AS
RETURN 
(
	SELECT sLNS,
			iID_MLNS,
			iID_MaDonVi,
			fTuChi_PheDuyet 
	   FROM NS_QT_ChungTuChiTiet
	   WHERE iID_QTChungTu in
		   (SELECT iID_QTChungTu
			FROM NS_QT_ChungTu
			WHERE iNamLamViec = @NamLamViec
			  AND INamNganSach = @NamNganSach
			  AND iID_MaNguonNganSach = @NguonNganSach
			  AND iThangQuy in (select * from f_split(@ithangquy)))
		 AND (@IdDonVi IS NULL OR iID_MaDonVi in (SELECT * FROM f_split(@IdDonVi)))
		 AND (@lns IS NULL OR sLNS like @lns + '%')
)
;
;
GO
/****** Object:  UserDefinedFunction [dbo].[f_quyettoan_tonghop]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[f_quyettoan_tonghop]
(    
      @NamLamViec int,
	  @NamNganSach nvarchar(20),
	  @NguonNganSach nvarchar(20),
	  @ithangquy nvarchar(100),
	  @IdDonVi nvarchar(max),
	  @lns nvarchar(max)
	  )
RETURNS TABLE
AS RETURN

	SELECT iID_MLNS,
		   iID_MLNS_Cha,
		   iID_MaDonVi,
		   TuChi = Sum(fTuChi_PheDuyet),
		   cast(0 AS float) AS HienVat,
		   SoNguoi = Sum(fSoNguoi),
		   SoNgay = Sum(fSoNgay),
		   SoLuot = Sum(fSoLuot)
	FROM NS_QT_ChungTuChiTiet
	WHERE iNamLamViec=@NamLamViec
	  AND (@NamNganSach IS NULL OR iNamNganSach = @NamNganSach)
	  AND (@NguonNganSach IS NULL OR iID_MaNguonNganSach = @NguonNganSach)
	  AND (@IdDonVi = ''
		   OR iID_MaDonVi in (SELECT * FROM f_split(@IdDonVi)))
	  AND (@lns IS NULL
		   OR sLNS in (SELECT * FROM f_split(@lns)))
	  AND iID_QTChungTu IN 
		(
			SELECT iID_QTChungTu 
			FROM NS_QT_ChungTu
			where iThangQuy in (select * from f_split(@ithangquy))
		)
	GROUP BY iID_MLNS, iID_MLNS_Cha, iID_MaDonVi
	HAVING sum(fTuChi_PheDuyet) <> 0;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[proc_get_all_nh_baocao_ketluanbaocao]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[proc_get_all_nh_baocao_ketluanbaocao]
	-- Add the parameters for the stored procedure here
    @iDonvi uniqueidentifier, 
	@dNgayPheDuyetTu date,
	@dNgayPheDuyetDen date,
	@devideDonViUSD int = null, 
    @devideDonViVND int = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
select  
        qtdact.ID as Id
       ,qtdact.iID_PheDuyetQuyetToanDAHT_ID as IIDPheDuyetQuyetToanDAHTId
	   ,Case When qtdact.iID_KHTT_NhiemVuChiID is null then '00000000-0000-0000-0000-000000000000' else qtdact.iID_KHTT_NhiemVuChiID End as IIDKHTTNhiemVuChiId
       ,Case When qtdact.iID_DuAnID is null then '00000000-0000-0000-0000-000000000000' else qtdact.iID_DuAnID End as IIDDuAnId
       ,qtdact.iID_HopDongID as IIDHopDongId
       ,qtdact.iID_ThanhToan_ChiTietID as IIDThanhToanChiTietId

	   ,IIF(@devideDonViUSD is not null, round(qtdact.fHopDong_USD/@devideDonViUSD,2),qtdact.fHopDong_USD) as FHopDongUsd
       ,IIF(@devideDonViVND is not null, round(qtdact.fHopDong_VND/@devideDonViVND,2),qtdact.fHopDong_VND) as FHopDongVnd
	   ,IIF(@devideDonViUSD is not null, round(qtdact.fKeHoach_TTCP_USD/@devideDonViUSD,2),qtdact.fKeHoach_TTCP_USD) as FKeHoachTTCPUsd

	   ,IIF(@devideDonViUSD is not null, round(qtdact.fKinhPhiDuocCap_Tong_USD/@devideDonViUSD,2),qtdact.fKinhPhiDuocCap_Tong_USD) as FKinhPhiDuocCapTongUsd
       ,IIF(@devideDonViVND is not null, round(qtdact.fKinhPhiDuocCap_Tong_VND/@devideDonViVND,2),qtdact.fKinhPhiDuocCap_Tong_VND) as FKinhPhiDuocCapTongVnd
      
	   ,IIF(@devideDonViUSD is not null, round(qtdact.fQuyetToanDuocDuyet_Tong_USD/@devideDonViUSD,2),qtdact.fQuyetToanDuocDuyet_Tong_USD) as FQuyetToanDuocDuyetTongUsd
       ,IIF(@devideDonViVND is not null, round(qtdact.fQuyetToanDuocDuyet_Tong_VND/@devideDonViVND,2),qtdact.fQuyetToanDuocDuyet_Tong_VND) as FQuyetToanDuocDuyetTongVnd

	   ,IIF(@devideDonViUSD is not null, round(qtdact.fSoSanhKinhPhi_USD/@devideDonViUSD,2),qtdact.fSoSanhKinhPhi_USD) as FSoSanhKinhPhiUsd
       ,IIF(@devideDonViVND is not null, round(qtdact.fSoSanhKinhPhi_VND/@devideDonViVND,2),qtdact.fSoSanhKinhPhi_VND) as FSoSanhKinhPhiVnd
       
	   ,IIF(@devideDonViUSD is not null, round(qtdact.fThuaTraNSNN_USD/@devideDonViUSD,2),qtdact.fThuaTraNSNN_USD) as FThuaTraNSNNUsd
       ,IIF(@devideDonViVND is not null, round(qtdact.fThuaTraNSNN_VND/@devideDonViVND,2),qtdact.fThuaTraNSNN_VND) as FThuaTraNSNNVnd
       
       ,qtdact.iNamBaoCaoTu as INamBaoCaoTu
       ,qtdact.iNamBaoCaoDen as INamBaoCaoDen

       ,ttct.sTenNoiDungChi as STenNoiDungChi
	   ,tt.iID_DonVi as IIDDonViId
	   ,dv.iID_MaDonVi + ' - '+dv.sTenDonVi as STenDonVi
	   ,da.sTenDuAn as STenDuAn
	   ,hd.sTenHopDong as STenHopDong
	   ,nvc.sTenNhiemVuChi as STenNhiemVuChi
	   ,tt.iLoaiNoiDungChi as ILoaiNoiDungChi
	   ,da.fUSD  as FHopDongUsdDuAn
	   ,da.fVND  as FHopDongVndDuAn
	   ,hd.fGiaTriUSD as FHopDongUsdHopDong
	   ,hd.fGiaTriVND  as FHopDongVndHopDong
from NH_QT_PheDuyetQuyetToanDAHT_ChiTiet qtdact
left join NH_QT_PheDuyetQuyetToanDAHT qtda on qtdact.iID_PheDuyetQuyetToanDAHT_ID = qtda.ID
left join NH_DM_NhiemVuChi nvc on qtdact.iID_KHTT_NhiemVuChiID = nvc.ID
left join NH_DA_DuAn da on  qtdact.iID_DuAnId = da.ID 
left join DonVi dv on  qtda.iID_DonViID = dv.iID_DonVi 
left join NH_DA_HopDong hd on qtdact.iID_HopDongID = hd.ID
left join NH_TT_ThanhToan_ChiTiet ttct on qtdact.iID_ThanhToan_ChiTietID = ttct.ID
left join NH_TT_ThanhToan tt on ttct.iID_DeNghiThanhToanID = tt.ID

Where(qtda.dNgayPheDuyet >= @dNgayPheDuyetTu or @dNgayPheDuyetTu is null or @dNgayPheDuyetTu = '') 
and (qtda.dNgayPheDuyet <= @dNgayPheDuyetDen or @dNgayPheDuyetDen is null or @dNgayPheDuyetDen = '') 
and (tt.iID_DonVi = @iDonvi or @iDonvi is null or @iDonvi = '00000000-0000-0000-0000-000000000000')
Order by qtdact.iID_KHTT_NhiemVuChiID , nvc.sTenNhiemVuChi , da.ID , da.sTenDuAn, tt.iLoaiNoiDungChi ,hd.ID , hd.sTenHopDong


END;
GO
/****** Object:  StoredProcedure [dbo].[proc_get_all_nh_baocaoquyettoanniendo_detail]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[proc_get_all_nh_baocaoquyettoanniendo_detail]
	-- Add the parameters for the stored procedure here
	 @iIDQuyetToan uniqueidentifier,
	 @devideDonViUSD float = null,
	 @devideDonViVND float = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select  qtct.*,
	        tb.sLNS as SLNS,
			tb.sL as SL,
			tb.sK as SK,
			tb.sM as SM,
			tb.sTM as STM,
			tb.sTTM as STTM,
			hd.sTenHopDong,
			dmnvc.sTenNhiemVuChi,
			da.sTenDuAn,
			ttct.sTenNoiDungChi,
			tt.iLoaiNoiDungChi,
			qtnd.iID_DonViID as IID_DonVi,
			dv.iID_MaDonVI + ' - '+ dv.sTenDonVi as sTenDonVi 
			,daqd.fGiaTriUSD  as FHopDongDuAnUsd
			,daqd.fGiaTriVND  as FHopDongDuAnVnd
			,hd.fGiaTriUSD as FHopDongUsd
			,hd.fGiaTriVND  as FHopDongVnd
			, qtct.ID as Id
			, qtct.iID_ParentID as IIdParentId
			, qtct.iID_HopDongID as IIdHopDongId
			, IIF(@devideDonViUSD is not null, round(qtct.fKeHoach_TTCP_USD/@devideDonViUSD,2), qtct.fKeHoach_TTCP_USD) as FKeHoachTtcpUsd
			, IIF(@devideDonViVND is not null, round(qtct.fKeHoach_TTCP_VND/@devideDonViVND,2), qtct.fKeHoach_TTCP_VND) as FKeHoachTtcpVnd
			, IIF(@devideDonViUSD is not null, round(qtct.fKeHoach_BQP_USD/@devideDonViUSD,2), qtct.fKeHoach_BQP_USD) as FKeHoachBqpUsd
			, IIF(@devideDonViUSD is not null, round(qtct.fKeHoach_BQP_VND/@devideDonViUSD,2), qtct.fKeHoach_BQP_VND) as FKeHoachBqpVnd
			, IIF(@devideDonViUSD is not null, round(qtct.fQTKinhPhiDuyetCacNamTruoc_USD/@devideDonViUSD,2), qtct.fQTKinhPhiDuyetCacNamTruoc_USD) as FQtKinhPhiDuyetCacNamTruocUsd
			, IIF(@devideDonViVND is not null, round(qtct.fQTKinhPhiDuyetCacNamTruoc_VND/@devideDonViVND,2), qtct.fQTKinhPhiDuyetCacNamTruoc_VND) as FQtKinhPhiDuyetCacNamTruocVnd
			, IIF(@devideDonViUSD is not null, round(qtct.fQTKinhPhiDuocCap_TongSo_USD/@devideDonViUSD,2), qtct.fQTKinhPhiDuocCap_TongSo_USD) as FQtKinhPhiDuocCapTongSoUsd
			, IIF(@devideDonViVND is not null, round(qtct.fQTKinhPhiDuocCap_TongSo_VND/@devideDonViVND,2), qtct.fQTKinhPhiDuocCap_TongSo_VND) as FQtKinhPhiDuocCapTongSoVnd
			, IIF(@devideDonViUSD is not null, round(qtct.fQTKinhPhiDuocCap_NamTruocChuyenSang_USD/@devideDonViUSD,2), qtct.fQTKinhPhiDuocCap_NamTruocChuyenSang_USD) as FQtKinhPhiDuocCapNamTruocChuyenSangUsd
			, IIF(@devideDonViVND is not null, round(qtct.fQTKinhPhiDuocCap_NamTruocChuyenSang_VND/@devideDonViVND,2), qtct.fQTKinhPhiDuocCap_NamTruocChuyenSang_VND) as FQtKinhPhiDuocCapNamTruocChuyenSangVnd
			, IIF(@devideDonViUSD is not null, round(qtct.fQTKinhPhiDuocCap_NamNay_USD/@devideDonViUSD,2), qtct.fQTKinhPhiDuocCap_NamNay_USD) as FQtKinhPhiDuocCapNamNayUsd
			, IIF(@devideDonViVND is not null, round(qtct.fQTKinhPhiDuocCap_NamNay_VND/@devideDonViVND,2), qtct.fQTKinhPhiDuocCap_NamNay_VND) as FQtKinhPhiDuocCapNamNayVnd
			, IIF(@devideDonViUSD is not null, round(qtct.fDeNghiQTNamNay_USD/@devideDonViUSD,2), qtct.fDeNghiQTNamNay_USD) as FDeNghiQtNamNayUsd
			, IIF(@devideDonViVND is not null, round(qtct.fDeNghiQTNamNay_VND/@devideDonViVND,2), qtct.fDeNghiQTNamNay_VND) as FDeNghiQtNamNayVnd
			, IIF(@devideDonViUSD is not null, round(qtct.fDeNghiChuyenNamSau_USD/@devideDonViUSD,2), qtct.fDeNghiChuyenNamSau_USD) as FDeNghiChuyenNamSauUsd
			, IIF(@devideDonViVND is not null, round(qtct.fDeNghiChuyenNamSau_VND/@devideDonViVND,2), qtct.fDeNghiChuyenNamSau_VND) as FDeNghiChuyenNamSauVnd
			, IIF(@devideDonViUSD is not null, round(qtct.fThuaThieuKinhPhiTrongNam_USD/@devideDonViUSD,2), qtct.fThuaThieuKinhPhiTrongNam_USD) as FThuaThieuKinhPhiTrongNamUsd
			, IIF(@devideDonViVND is not null, round(qtct.fThuaThieuKinhPhiTrongNam_VND/@devideDonViVND,2), qtct.fThuaThieuKinhPhiTrongNam_VND) as FThuaThieuKinhPhiTrongNamVnd
			, IIF(@devideDonViUSD is not null, round(qtct.fThuaNopNSNN_USD/@devideDonViUSD,2), qtct.fThuaNopNSNN_USD) as FThuaNopNsnnUsd
			, IIF(@devideDonViVND is not null, round(qtct.fThuaNopNSNN_VND/@devideDonViVND,2), qtct.fThuaNopNSNN_VND) as FThuaNopNsnnVnd
			, IIF(@devideDonViUSD is not null, round(qtct.fLuyKeKinhPhiDuocCap_USD/@devideDonViUSD,2), qtct.fLuyKeKinhPhiDuocCap_USD) as FLuyKeKinhPhiDuocCapUsd
			, IIF(@devideDonViVND is not null, round(qtct.fLuyKeKinhPhiDuocCap_VND/@devideDonViVND,2), qtct.fLuyKeKinhPhiDuocCap_VND) as FLuyKeKinhPhiDuocCapVnd
			, IIF(@devideDonViUSD is not null, round(qtct.fKeHoachChuaGiaiNgan_USD/@devideDonViUSD,2), qtct.fKeHoachChuaGiaiNgan_USD) as FKeHoachChuaGiaiNganUsd
			, IIF(@devideDonViVND is not null, round(qtct.fKeHoachChuaGiaiNgan_VND/@devideDonViVND,2), qtct.fKeHoachChuaGiaiNgan_VND) as FKeHoachChuaGiaiNganVnd
			, qtct.iID_KHTT_NhiemVuChiID as IIdKhttNhiemVuChiId
			, 1 as IsData
	from NH_QT_QuyetToanNienDo_ChiTiet qtct 
	left join NH_QT_QuyetToanNienDo qtnd on qtct.iID_QuyetToanNienDoID = qtnd.ID 
    left join NH_DM_NhiemVuChi dmnvc on qtct.iID_KHTT_NhiemVuChiID = dmnvc.ID
	left join NH_DA_DuAn da on  qtct.iID_DuAnId = da.ID 
	left join NH_DA_QDDauTu daqd on da.ID = daqd.iID_DuAnID
	left join DonVi dv on  qtnd.iID_DonViID = dv.iID_DonVi
	left join NH_DA_HopDong hd on qtct.iID_HopDongID = hd.ID
	left join NH_TT_ThanhToan_ChiTiet ttct on qtct.iID_ThanhToan_ChiTietID = ttct.ID
	left join NS_MucLucNganSach tb  on tb.iID = ttct.iID_MucLucNganSachID
	left join NH_TT_ThanhToan tt on ttct.iID_DeNghiThanhToanID = tt.ID
	where 
        qtct.iID_QuyetToanNienDoID = @iIDQuyetToan
END;

GO
/****** Object:  StoredProcedure [dbo].[proc_get_all_nh_baocaoquyettoanniendo_paging]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[proc_get_all_nh_baocaoquyettoanniendo_paging]
	-- Add the parameters for the stored procedure here

  @iIDDonVi uniqueidentifier,
  @iNamKeHoach float,
  @devideDonViUSD float = null,
  @devideDonViVND float = null

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	/* bang tam*/

select distinct  CAST(
             CASE 
                  WHEN qtct.iNamKeHoach>=gd.iGiaiDoanTu and qtct.iNamKeHoach<=gd.iGiaiDoanDen
                     THEN  qtct.fGiaTriUsd 
                  ELSE null
             END AS float) as fQTKinhPhiDuyetCacNamTruoc_USD,
			 CAST(
             CASE 
                  WHEN qtct.iNamKeHoach>=gd.iGiaiDoanTu and qtct.iNamKeHoach<=gd.iGiaiDoanDen
                     THEN qtct.fGiaTriVnd  
                  ELSE null
             END AS float) as fQTKinhPhiDuyetCacNamTruoc_VND
			 ,gd.iID_DonViThuHuongID,qtct.iID_DuAnId,qtct.iID_HopDongId,qtct.iID_KHTT_NhiemVuChiID
			  into #kpdnt
			 from  
			 (select distinct b.iGiaiDoanTu,b.iGiaiDoanDen,a.iID_DonViThuHuongID from NH_KHTongThe_NhiemVuChi a join NH_KHTongThe b on a.iID_KHTongTheID = b.ID  ) as gd 
			  join (

			 select sum(a.fGiaTriUsd) as fGiaTriUsd,sum(a.fGiaTriVnd) as fGiaTriVnd, a.iID_DonVi,a.iNamKeHoach,a.iID_DuAnId,a.iID_HopDongId,a.iID_KHTT_NhiemVuChiID from NH_TH_TongHop a
             where  a.iNamKeHoach = @iNamKeHoach - 1 and a.bIsLog = 0 and a.iID_DonVi = @iIDDonVi and 
			 (a.sMaDich in ('311','321') and a.sMaNguon = '000' and a.sMaNguonCha in ('302','301') and a.sMaTienTrinh  in ('200','100'))
			 group by a.iID_DonVi,a.iNamKeHoach,a.iID_DuAnId,a.iID_HopDongId,a.iID_KHTT_NhiemVuChiID

			 ) as qtct on 
			 gd.iID_DonViThuHuongID = qtct.iID_DonVi
			 where qtct.iNamKeHoach between gd.iGiaiDoanTu and gd.iGiaiDoanDen
				

    --get fQTKinhPhiDuocCap_NamTruocChuyenSang_USD

		--select (isnull(tbTong13.fGiaTriUsd,0) - isnull(tbTru2.fGiaTriUsdThuHoi,0)) as fQTKinhPhiDuocCap_NamTruocChuyenSang_USD, (isnull(tbTong13.fGiaTriVnd,0) - isnull(tbTru2.fGiaTriVndThuHoi,0)) as fQTKinhPhiDuocCap_NamTruocChuyenSang_VND  
		--, tbTong13.iID_DonVi,tbTong13.iID_DuAnId,tbTong13.iID_HopDongId,tbTong13.iNamKeHoach
  --          into #ntcs
		--	from (
		--	 select sum(isnull(a.fGiaTriUsd,0)) as fGiaTriUsd,sum(isnull(a.fGiaTriVnd,0)) as fGiaTriVnd,a.iID_DonVi,a.iNamKeHoach,a.iID_DuAnId,a.iID_HopDongId
  --           from NH_TH_TongHop a
  --           where  a.iNamKeHoach = @iNamKeHoach and a.bIsLog = 0 and   a.iID_DonVi = @iIDDonVi and
		--	 ((a.iCoQuanThanhToan = 1 and (a.sMaDich in ('112','122') or a.sMaNguon ='0' or a.sMaNguonCha = '102' and a.sMaTienTrinh  ='200'))
		--	 or
		--	 (a.iCoQuanThanhToan = 2 and (a.sMaDich = '0' or a.sMaNguon ='102' or a.sMaNguonCha is null and a.sMaTienTrinh  ='200')))
		--	 group by a.iID_DonVi,a.iNamKeHoach,a.iID_DuAnId,a.iID_HopDongId
		--	 ) as tbTong13 
		--	 left join (
		--	 select sum(isnull(a.fGiaTriUsd,0)) as fGiaTriUsdThuHoi,sum(isnull(a.fGiaTriVnd,0)) as fGiaTriVndThuHoi
		--	 ,a.iID_DonVi,a.iNamKeHoach,a.iID_DuAnId,a.iID_HopDongId
  --           from NH_TH_TongHop a
  --           where  a.iNamKeHoach = @iNamKeHoach and a.bIsLog = 0 and a.iCoQuanThanhToan = 1 and a.iID_DonVi = @iIDDonVi and 
		--	 a.sMaDich = '0' and a.sMaNguon = '122' and a.sMaNguonCha = null and a.sMaTienTrinh  ='200'
		--	 group by a.iID_DonVi,a.iNamKeHoach,a.iID_DuAnId,a.iID_HopDongId
		--	 ) as tbTru2 on tbTong13.iID_DonVi = tbTru2.iID_DonVi and tbTong13.iID_DuAnId = tbTru2.iID_DuAnId and tbTong13.iID_HopDongId = tbTru2.iID_HopDongId

	--get fQTKinhPhiDuocCap_NamNay_USD

select b.iLoaiDeNghi ,b.iCoQuanThanhToan,a.ID, CAST(
				 CASE 
					  WHEN b.iLoaiDeNghi=1 and b.iCoQuanThanhToan = 2 or b.iLoaiDeNghi = 2 and  b.iCoQuanThanhToan = 1 or b.iLoaiDeNghi=3 and  b.iCoQuanThanhToan = 1
						 THEN a.fPheDuyetCapKyNay_USD
					  ELSE null
				 END AS float) as fQTKinhPhiDuocCap_NamNay_USD 
				 into #nnUSD
	from NH_TT_ThanhToan_ChiTiet a left join NH_TT_ThanhToan b on a.iID_DeNghiThanhToanID = b.ID
	where b.iNamNganSach = 1 and b.iNamKeHoach = @iNamKeHoach    

	--get fQTKinhPhiDuocCap_NamNay_VND


	select b.iLoaiDeNghi,b.iCoQuanThanhToan,a.ID,CAST(
				 CASE 
					  WHEN b.iLoaiDeNghi=1 and b.iCoQuanThanhToan = 2 or b.iLoaiDeNghi = 2 and  b.iCoQuanThanhToan = 1 or b.iLoaiDeNghi=3 and  b.iCoQuanThanhToan = 1
						 THEN a.fPheDuyetCapKyNay_VND
					  ELSE null
				 END AS float) as fQTKinhPhiDuocCap_NamNay_VND 
				 into #nnVND
				 from NH_TT_ThanhToan_ChiTiet a left join NH_TT_ThanhToan b on a.iID_DeNghiThanhToanID = b.ID
				 where b.iNamNganSach = 1 and b.iNamKeHoach = @iNamKeHoach      

	----------------------------
	select b.iLoaiDeNghi ,b.iCoQuanThanhToan,a.ID, CAST(
             CASE 
                  WHEN b.iLoaiDeNghi=1 and b.iCoQuanThanhToan = 2 or b.iLoaiDeNghi = 2 and  b.iCoQuanThanhToan = 1 or b.iLoaiDeNghi=3 and  b.iCoQuanThanhToan = 1
                     THEN  a.fPheDuyetCapKyNay_USD
                  ELSE null
             END AS float) as fQTKinhPhiDuocCap_NamTruocChuyenSang_USD
			 into #ntcsUSD
from NH_TT_ThanhToan_ChiTiet a left join NH_TT_ThanhToan b on a.iID_DeNghiThanhToanID = b.ID
where b.iNamNganSach = 2 and b.iNamKeHoach = @iNamKeHoach    
--get fQTKinhPhiDuocCap_NamTruocChuyenSang_VND

select b.iLoaiDeNghi ,b.iCoQuanThanhToan,a.ID, CAST(
             CASE 
                  WHEN b.iLoaiDeNghi=1 and b.iCoQuanThanhToan = 2 or b.iLoaiDeNghi = 2 and  b.iCoQuanThanhToan = 1 or b.iLoaiDeNghi=3 and  b.iCoQuanThanhToan = 1
                     THEN a.fPheDuyetCapKyNay_VND
                  ELSE null
             END AS float) as fQTKinhPhiDuocCap_NamTruocChuyenSang_VND 
			 into #ntcsVND
from NH_TT_ThanhToan_ChiTiet a left join NH_TT_ThanhToan b on a.iID_DeNghiThanhToanID = b.ID
where b.iNamNganSach = 2 and b.iNamKeHoach = @iNamKeHoach  

 --select (isnull(tbTong13.fGiaTriUsd,0) - isnull(tbTru2.fGiaTriUsdThuHoi,0)) as fQTKinhPhiDuocCap_NamNay_USD, (isnull(tbTong13.fGiaTriVnd,0) - isnull(tbTru2.fGiaTriVndThuHoi,0)) as fQTKinhPhiDuocCap_NamNay_VND  
	--, tbTong13.iID_DonVi,tbTong13.iID_DuAnId,tbTong13.iID_HopDongId,tbTong13.iNamKeHoach
	--     into #ntnn
	--		from (
	--		select sum(isnull(a.fGiaTriUsd,0)) as fGiaTriUsd,sum(isnull(a.fGiaTriVnd,0)) as fGiaTriVnd,a.iID_DonVi,a.iNamKeHoach,a.iID_DuAnId,a.iID_HopDongId
 --            from NH_TH_TongHop a
 --            where  a.iNamKeHoach = @iNamKeHoach and a.bIsLog = 0 and   a.iID_DonVi = @iIDDonVi and
	--		 ((a.iCoQuanThanhToan = 1 and (a.sMaDich in ('111','121') or a.sMaNguon ='0' or a.sMaNguonCha = '101' and a.sMaTienTrinh  ='200'))
	--		 or
	--		 (a.iCoQuanThanhToan = 2 and (a.sMaDich = '0' or a.sMaNguon ='101' or a.sMaNguonCha is null and a.sMaTienTrinh  ='200')))
	--		 group by a.iID_DonVi,a.iNamKeHoach,a.iID_DuAnId,a.iID_HopDongId) as tbTong13 
	--		 left join (
	--		 select sum(a.fGiaTriUsd) as fGiaTriUsdThuHoi,sum(a.fGiaTriVnd) as fGiaTriVndThuHoi
	--		 ,a.iID_DonVi,a.iNamKeHoach,a.iID_DuAnId,a.iID_HopDongId
 --            from NH_TH_TongHop a
 --            where  a.iNamKeHoach = @iNamKeHoach and a.bIsLog = 0 and a.iCoQuanThanhToan = 1 and a.iID_DonVi = @iIDDonVi and 
	--		 a.sMaDich = '0' and a.sMaNguon = '121' and a.sMaNguonCha = null and a.sMaTienTrinh  ='200'
	--		 group by a.iID_DonVi,a.iNamKeHoach,a.iID_DuAnId,a.iID_HopDongId
	--		 ) as tbTru2 on tbTong13.iID_DonVi = tbTru2.iID_DonVi and tbTong13.iID_DuAnId = tbTru2.iID_DuAnId and tbTong13.iID_HopDongId = tbTru2.iID_HopDongId


	--get fKuyKeKinhPhiDuocCap_USD
	 --select a.fLuyKeKinhPhiDuocCap_VND,a.fLuyKeKinhPhiDuocCap_USD,a.iID_DuAnID,a.iID_HopDongID,a.iID_KHTT_NhiemVuChiID,b.iNamKeHoach into #lknt from NH_QT_QuyetToanNienDo_ChiTiet a join NH_QT_QuyetToanNienDo b on a.iID_QuyetToanNienDoID = b.ID 
	 -- where b.iNamKeHoach = @iNamKeHoach - 1

	--   select a.fLuyKeKinhPhiDuocCap_VND,a.iID_ThanhToan_ChiTietID,a.fLuyKeKinhPhiDuocCap_USD,a.iID_DuAnID,a.iID_HopDongID,a.iID_KHTT_NhiemVuChiID,b.iNamKeHoach 
 --into #lknt from NH_QT_QuyetToanNienDo_ChiTiet a join NH_QT_QuyetToanNienDo b on a.iID_QuyetToanNienDoID = b.ID 
 -- where b.iNamKeHoach = @iNamKeHoach - 1 and a.iID_ParentID is null

      select a.fGiaTriUsd as fLuyKeKinhPhiDuocCap_USD,a.fGiaTriVnd as fLuyKeKinhPhiDuocCap_VND,a.iID_DuAnID,a.iID_HopDongID,a.iID_DonVi
	into #lknt 
	from NH_TH_TongHop a
  where a.sMaNguon = '303' 
  and a.iNamKeHoach = @iNamKeHoach - 1 and a.iID_DonVi = @iIDDonVi and a.bIsLog = 0
	/* finnal*/
	select distinct  tb.sLNS as SLNS, tb.sL as SL,tb.sK as SK,tb.sM as SM,tb.sTM as STM,tb.sTTM as STTM,dmnvc.sTenNhiemVuChi,da.sTenDuAn,dv.iID_MaDonVi + ' - ' + dv.sTenDonVi as sTenDonVi ,a.sTenNoiDungChi,b.iID_DuAnID ,khttnvc.iID_NhiemVuChiID as IIdKhttNhiemVuChiId,b.iID_HopDongID as IIdHopDongId, hd.sTenHopDong as STenHopDong,a.ID as IID_ThanhToan_ChiTietID,
             
		   IIF(@devideDonViUSD is not null, round(daqd.fGiaTriUSD/@devideDonViUSD,2), daqd.fGiaTriUSD) as FHopDongDuAnUsd,
		   IIF(@devideDonViVND is not null, round(daqd.fGiaTriVND/@devideDonViVND,2), daqd.fGiaTriVND) as FHopDongDuAnVnd,
		   IIF(@devideDonViUSD is not null, round(hd.fGiaTriUSD/@devideDonViUSD,2), hd.fGiaTriUSD) as FHopDongUsd,
		   IIF(@devideDonViVND is not null, round(hd.fGiaTriVND/@devideDonViVND,2), hd.fGiaTriVND) as FHopDongVnd,
		   IIF(@devideDonViUSD is not null, round(khttnvc.fGiaTriKH_TTCP/@devideDonViUSD,2), khttnvc.fGiaTriKH_TTCP) as FKeHoachTtcpUsd,
		   IIF(@devideDonViUSD is not null, round(khttnvc.fGiaTriKH_BQP/@devideDonViUSD,2), khttnvc.fGiaTriKH_BQP) as FKeHoachBqpUsd,
		   IIF(@devideDonViVND is not null, round(khttnvc.fGiaTriKH_BQP_VND/@devideDonViVND,2), khttnvc.fGiaTriKH_BQP_VND) as FKeHoachBqpVnd,
		   IIF(@devideDonViUSD is not null, round(gdkpdnt.fQTKinhPhiDuyetCacNamTruoc_USD/@devideDonViUSD,2), gdkpdnt.fQTKinhPhiDuyetCacNamTruoc_USD) as FQtKinhPhiDuyetCacNamTruocUsd,
		   IIF(@devideDonViVND is not null, round(gdkpdnt.fQTKinhPhiDuyetCacNamTruoc_VND/@devideDonViVND,2), gdkpdnt.fQTKinhPhiDuyetCacNamTruoc_VND) as FQtKinhPhiDuyetCacNamTruocVnd,
		   IIF(@devideDonViUSD is not null, round((isnull(ntcsUSD.fQTKinhPhiDuocCap_NamTruocChuyenSang_USD,0) + isnull(nnUSD.fQTKinhPhiDuocCap_NamNay_USD,0))/@devideDonViUSD,2)
		   , (isnull(ntcsUSD.fQTKinhPhiDuocCap_NamTruocChuyenSang_USD,0) + isnull(nnUSD.fQTKinhPhiDuocCap_NamNay_USD,0))) as FQtKinhPhiDuocCapTongSoUsd,

		   IIF(@devideDonViVND is not null, round((isnull(ntcsVND.fQTKinhPhiDuocCap_NamTruocChuyenSang_VND,0) + isnull(nnVND.fQTKinhPhiDuocCap_NamNay_VND,0))/@devideDonViVND,2)
		   , (isnull(ntcsVND.fQTKinhPhiDuocCap_NamTruocChuyenSang_VND,0) + isnull(nnVND.fQTKinhPhiDuocCap_NamNay_VND,0))) as FQtKinhPhiDuocCapTongSoVnd,

		   IIF(@devideDonViUSD is not null, round(ntcsUSD.fQTKinhPhiDuocCap_NamTruocChuyenSang_USD/@devideDonViUSD,2), ntcsUSD.fQTKinhPhiDuocCap_NamTruocChuyenSang_USD) as FQtKinhPhiDuocCapNamTruocChuyenSangUsd,
		   IIF(@devideDonViVND is not null, round(ntcsVND.fQTKinhPhiDuocCap_NamTruocChuyenSang_VND/@devideDonViVND,2), ntcsVND.fQTKinhPhiDuocCap_NamTruocChuyenSang_VND) as FQtKinhPhiDuocCapNamTruocChuyenSangVnd,
		   IIF(@devideDonViUSD is not null, round(nnUSD.fQTKinhPhiDuocCap_NamNay_USD/@devideDonViUSD,2), nnUSD.fQTKinhPhiDuocCap_NamNay_USD) as FQtKinhPhiDuocCapNamNayUsd,
		   IIF(@devideDonViVND is not null, round(nnVND.fQTKinhPhiDuocCap_NamNay_VND/@devideDonViVND,2), nnVND.fQTKinhPhiDuocCap_NamNay_VND) as FQtKinhPhiDuocCapNamNayVnd,

		   IIF(@devideDonViUSD is not null, round((isnull(ntcsUSD.fQTKinhPhiDuocCap_NamTruocChuyenSang_USD,0) + isnull(nnUSD.fQTKinhPhiDuocCap_NamNay_USD,0))/@devideDonViUSD,2)
		   , (isnull(ntcsUSD.fQTKinhPhiDuocCap_NamTruocChuyenSang_USD,0) + isnull(nnUSD.fQTKinhPhiDuocCap_NamNay_USD,0))) as FThuaThieuKinhPhiTrongNamUsd,
		   IIF(@devideDonViVND is not null, round((isnull(ntcsVND.fQTKinhPhiDuocCap_NamTruocChuyenSang_VND,0) + isnull(nnVND.fQTKinhPhiDuocCap_NamNay_VND,0))/@devideDonViVND,2)
		   , (isnull(ntcsVND.fQTKinhPhiDuocCap_NamTruocChuyenSang_VND,0) + isnull(nnVND.fQTKinhPhiDuocCap_NamNay_VND,0))) as FThuaThieuKinhPhiTrongNamVnd,

		   IIF(@devideDonViUSD is not null, round(isnull(lknt.fLuyKeKinhPhiDuocCap_USD, 0)/@devideDonViUSD,2) , isnull(lknt.fLuyKeKinhPhiDuocCap_USD, 0) ) as FLuyKeKinhPhiDuocCapUsd,
		   IIF(@devideDonViVND is not null, round(isnull(lknt.fLuyKeKinhPhiDuocCap_VND, 0)/@devideDonViVND,2) , isnull(lknt.fLuyKeKinhPhiDuocCap_VND, 0) ) as FLuyKeKinhPhiDuocCapVnd,
		   (IIF(@devideDonViUSD is not null, round(khttnvc.fGiaTriKH_BQP/@devideDonViUSD,2), khttnvc.fGiaTriKH_BQP)) 
		   - IIF(@devideDonViUSD is not null, round((isnull(ntcsUSD.fQTKinhPhiDuocCap_NamTruocChuyenSang_USD,0) 
		   + isnull(nnUSD.fQTKinhPhiDuocCap_NamNay_USD,0))/@devideDonViUSD,2)
		   , (isnull(ntcsUSD.fQTKinhPhiDuocCap_NamTruocChuyenSang_USD,0) + isnull(nnUSD.fQTKinhPhiDuocCap_NamNay_USD,0))) as FKeHoachChuaGiaiNganUsd
		   
		   ,b.iCoQuanThanhToan,b.iLoaiDeNghi,b.iThanhToanTheo,b.iLoaiNoiDungChi,b.iID_DonVi
		   ,a.iID_MucLucNganSachID as IIdMucLucNganSachId, 1 as IsData
	from NH_TT_ThanhToan_ChiTiet a 
	left join NH_TT_ThanhToan b on a.iID_DeNghiThanhToanID = b.ID 
	left join NH_DM_NhiemVuChi dmnvc on b.iID_NhiemVuChiID = dmnvc.ID
	left join NH_KHTongThe_NhiemVuChi khttnvc on khttnvc.iID_KHTongTheID = b.iID_KHTongTheID AND khttnvc.iID_NhiemVuChiID = dmnvc.ID
	left join NH_KHTongThe khtt on khttnvc.iID_KHTongTheID = khtt.ID 
	left join NS_MucLucNganSach tb  on tb.iID = a.iID_MucLucNganSachID 
	left join NH_DA_DuAn da on  b.iID_DuAnId = da.ID 
	left join NH_DA_QDDauTu daqd on da.ID = daqd.iID_DuAnID
	left join DonVi dv on  b.iID_DonVi = dv.iID_DonVi 
	left join NH_DA_HopDong hd on b.iID_HopDongID = hd.ID
	left join #kpdnt as gdkpdnt on (b.iID_DuAnID  is null and gdkpdnt.iID_DuAnId is null or b.iID_DuAnID= gdkpdnt.iID_DuAnId )  and (b.iID_HopDongID  is null and gdkpdnt.iID_HopDongId is null or b.iID_HopDongID= gdkpdnt.iID_HopDongId )   
	left join NH_QT_QuyetToanNienDo_ChiTiet qtct on khttnvc.ID = qtct.iID_KHTT_NhiemVuChiID 
	--left join #ntcs as ntcs on (b.iID_DonVi  is null and ntcs.iID_DonVi is null or b.iID_DonVi= ntcs.iID_DonVi )  and (b.iID_DuAnID  is null and ntcs.iID_DuAnID is null or b.iID_DuAnID= ntcs.iID_DuAnID ) and (b.iID_HopDongID  is null and ntcs.iID_HopDongID is null or b.iID_HopDongID= ntcs.iID_HopDongID ) 
 --   left join #ntnn as ntnn on (b.iID_DonVi  is null and ntnn.iID_DonVi is null or b.iID_DonVi= ntnn.iID_DonVi )  and (b.iID_DuAnID  is null and ntnn.iID_DuAnID is null or b.iID_DuAnID= ntnn.iID_DuAnID ) and (b.iID_HopDongID  is null and ntnn.iID_HopDongID is null or b.iID_HopDongID= ntnn.iID_HopDongID ) 
	left join #ntcsUSD as ntcsUSD on b.iLoaiDeNghi = ntcsUSD.iLoaiDeNghi and b.iCoQuanThanhToan = ntcsUSD.iCoQuanThanhToan and a.ID = ntcsUSD.ID
left join #ntcsVND as ntcsVND on b.iLoaiDeNghi = ntcsUSD.iLoaiDeNghi and b.iCoQuanThanhToan = ntcsUSD.iCoQuanThanhToan and a.ID = ntcsVND.ID
--left join #ntcs as ntcs on (b.iID_DonVi  is null and ntcs.iID_DonVi is null or b.iID_DonVi= ntcs.iID_DonVi )  and (b.iID_DuAnID  is null and ntcs.iID_DuAnID is null or b.iID_DuAnID= ntcs.iID_DuAnID ) and (b.iID_HopDongID  is null and ntcs.iID_HopDongID is null or b.iID_HopDongID= ntcs.iID_HopDongID ) 
left join #nnUSD as nnUSD on b.iLoaiDeNghi = nnUSD.iLoaiDeNghi and b.iCoQuanThanhToan = nnUSD.iCoQuanThanhToan and a.ID = nnUSD.ID
left join #nnVND as nnVND on b.iLoaiDeNghi = nnVND.iLoaiDeNghi and b.iCoQuanThanhToan = nnVND.iCoQuanThanhToan and a.ID = nnVND.ID
	left join #lknt as lknt on (b.iID_DonVi  is null and lknt.iID_DonVi is null or b.iID_DonVi= lknt.iID_DonVi )  and (b.iID_DuAnID  is null and lknt.iID_DuAnID is null or b.iID_DuAnID= lknt.iID_DuAnID ) and (b.iID_HopDongID  is null and lknt.iID_HopDongID is null or b.iID_HopDongID= lknt.iID_HopDongID )  
	where b.iNamKeHoach = @iNamKeHoach AND (@iIDDonVi is   null or b.iID_DonVi in (@iIDDonVi)) AND b.iTrangThai = 2
	/* finnal*/
	DROP TABLE #kpdnt
	--DROP TABLE #ntcs
	--DROP TABLE #ntnn
	DROP TABLE #ntcsUSD
	DROP TABLE #ntcsVND
	DROP TABLE #nnUSD
	DROP TABLE #nnVND
	DROP TABLE #lknt
END;

GO
/****** Object:  StoredProcedure [dbo].[rpt_ds_chi_tra_ca_nhan_ngan_hang]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[rpt_ds_chi_tra_ca_nhan_ngan_hang]
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
		ISNULL(chucVu.HeSo_Cv, 0) HSChucVu,
		capBac.Ma_Cb MaCapBac,
		canBo.So_TaiKhoan SoTaiKhoan,
		canBo.Ten_KhoBac NganHang,
		CEILING(ISNULL(bangLuong.Gia_Tri, 0)) THANHTIEN
	FROM TL_BangLuong_Thang bangLuong
	INNER JOIN TL_DS_CapNhap_BangLuong dsCapNhatBangLuong
		ON bangLuong.parent = dsCapNhatBangLuong.Id
	INNER JOIN TL_DM_CanBo canBo
		ON canBo.Ma_CanBo = bangLuong.Ma_CBo
	LEFT JOIN TL_DM_ChucVu chucVu
		ON canBo.Ma_CV = chucVu.Ma_Cv
	LEFT JOIN TL_DM_CapBac capBac
		ON canBo.Ma_CB = capBac.Ma_Cb
	WHERE
		bangLuong.Ma_PhuCap = 'THANHTIEN'
		AND canBo.TM = 1
		AND ISNULL(bangLuong.Gia_Tri, 0) > 0
		AND dsCapNhatBangLuong.Thang = @thang
		AND dsCapNhatBangLuong.Nam = @nam
		AND canBo.Parent in (SELECT * FROM dbo.splitstring(@maDonVi))
	ORDER BY MaDonVi DESC, HSChucVu DESC, MaCapBac DESC, TenCanBo DESC
END

/****** Object:  StoredProcedure [dbo].[sp_khluachonnhathau_get_nguonvon_by_lcnt_update]    Script Date: 15/12/2021 6:36:38 PM ******/
SET ANSI_NULLS ON
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_delete_nh_tonghop_giam]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_delete_nh_tonghop_giam]
	@sLoai nvarchar(100),
    @uIdQuyetDinh uniqueidentifier
AS
BEGIN
	CREATE TABLE #tmp(iId uniqueidentifier)
	CREATE TABLE #tmpNguon(sMaNguon nvarchar(100))
	CREATE TABLE #tmpDich(sMaDich nvarchar(100))

	IF(@sLoai = 'TTCP')
	BEGIN
		INSERT INTO #tmpNguon VALUES ('101'), ('102'), ('121'), ('122')
		INSERT INTO #tmpDich VALUES ('111'), ('112'), ('121'), ('122')
	END
	IF(@sLoai = 'QTND')
	BEGIN
		INSERT INTO #tmpDich VALUES ('201')
	END
	IF(@sLoai = 'QUYET_TOAN')
	BEGIN
		INSERT INTO #tmpNguon VALUES ('301'), ('302'), ('303')
	END

	-- dao nguoc but toan
	INSERT INTO NH_TH_TongHop(iID_DuAnId, iID_KHTT_NhiemVuChiID, iID_HopDongId, iID_DonVi, iID_MaDonViQuanLy, iCoQuanThanhToan, dNgayDeNghi, iNamKeHoach, iQuyKeHoach, iID_ChungTu, sMaNguon, sMaDich, sMaNguonCha, fGiaTriUsd, fGiaTriVnd, iID_TiGia, bIsLog, iStatus, sMaTienTrinh)
	OUTPUT inserted.Id INTO #tmp(iId)
	SELECT tbl.iID_DuAnId, tbl.iID_KHTT_NhiemVuChiID, tbl.iID_HopDongId, iID_DonVi, iID_MaDonViQuanLy, iCoQuanThanhToan, tbl.dNgayDeNghi, iNamKeHoach, iQuyKeHoach, iID_ChungTu, tbl.sMaDich, tbl.sMaNguon, tbl.sMaNguonCha, fGiaTriUsd, fGiaTriVnd, iID_TiGia, 1, 2, '300'
	FROM NH_TH_TongHop as tbl
	LEFT JOIN #tmpDich as md on tbl.sMaDich COLLATE DATABASE_DEFAULT = md.sMaDich COLLATE DATABASE_DEFAULT AND tbl.sMaNguon = '000'
	LEFT JOIN #tmpNguon as mn on tbl.sMaNguon COLLATE DATABASE_DEFAULT = mn.sMaNguon COLLATE DATABASE_DEFAULT AND tbl.sMaDich = '000'
	WHERE tbl.iID_ChungTu = @uIdQuyetDinh 
		AND bIsLog = 0
		AND sMaTienTrinh COLLATE DATABASE_DEFAULT in ('100', '200') 

	-- khoa but toan da update
	UPDATE tbl 
	SET 
		bIsLog = 1
	FROM NH_TH_TongHop as tbl
	LEFT JOIN #tmpDich as md on tbl.sMaDich COLLATE DATABASE_DEFAULT = md.sMaDich COLLATE DATABASE_DEFAULT AND tbl.sMaNguon = '000'
	LEFT JOIN #tmpNguon as mn on tbl.sMaNguon COLLATE DATABASE_DEFAULT = mn.sMaNguon COLLATE DATABASE_DEFAULT AND tbl.sMaDich = '000'
	WHERE tbl.iID_ChungTu = @uIdQuyetDinh 
		AND bIsLog = 0
		AND sMaTienTrinh COLLATE DATABASE_DEFAULT in ('100', '200') 
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dutoan_donvi]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_dt_dutoan_donvi]
	-- Add the parameters for the stored procedure here
	 @dvt int,									
	 @NamLamViec int,								
	 @NamNganSach int,					
	 @NguonNganSach int,
	 @id_donvi nvarchar(2000),
	 @NgayChungTu datetime,
	 @lns nvarchar(max),
	 @bkhoa int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT --isnull(ctct.iID_DTCTChiTiet, NEWID()) AS iID_DTCTChiTiet,
       --ctct.iID_DTChungTu,
       mlns.iID_MLNS,
       mlns.iID_MLNS_Cha,
       mlns.sXauNoiMa,
       mlns.sLNS,
       mlns.sL,
       mlns.sK,
       mlns.sM,
       mlns.sTM,
       mlns.sTTM,
       mlns.sNG,
       mlns.sTNG,
       mlns.sTNG1,
       mlns.sTNG2,
       mlns.sTNG3,
       mlns.sMoTa,
       mlns.bHangCha as IsHangCha,
       ctct.iID_MaDonVi,
       sum(isnull(ctct.TuChi, 0))/@dvt AS TuChi,
       sum(isnull(ctct.HienVat, 0))/@dvt AS HienVat
	FROM NS_MucLucNganSach mlns
	LEFT JOIN
	  (select * from f_ns_DuToanDonVi(@NamLamViec, @NamNganSach, @NguonNganSach, @NgayChungTu, @id_donvi, NULL, @dvt, @bkhoa)) ctct ON mlns.iID_MLNS = ctct.iID_MLNS
	LEFT JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1) dv
	ON dv.iID_MaDonVi = ctct.iID_MaDonVi
	WHERE mlns.iNamLamViec = @NamLamViec
	  AND mlns.bHangChaDuToan IS NOT NULL
	  AND (@LNS is null or mlns.sLNS in
		(SELECT *
		 FROM dbo.f_split(@LNS)))
	GROUP BY mlns.iID_MLNS,
       mlns.iID_MLNS_Cha,
       mlns.sXauNoiMa,
       mlns.sLNS,
       mlns.sL,
       mlns.sK,
       mlns.sM,
       mlns.sTM,
       mlns.sTTM,
       mlns.sNG,
       mlns.sTNG,
       mlns.sTNG1,
       mlns.sTNG2,
       mlns.sTNG3,
       mlns.sMoTa,
       mlns.bHangCha,
	   ctct.iID_MaDonVi
	ORDER BY mlns.sXauNoiMa;

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dutoan_dotnhan_phanbo_find_all]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_dt_dutoan_dotnhan_phanbo_find_all]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@Type int,
	@VoucherType int,
	@Date datetime,
	@Index int
AS
BEGIN
	DECLARE @DieuChinh int = 4;
	WITH tblNhanPhanBo AS (
		SELECT *, 
			   fTongTuChi + fTongHienVat + fTongPhanCap + fTongHangNhap + fTongHangMua + fTongDuPhong + fTongTonKho AS SoPhanBo
		FROM NS_DT_ChungTu 
		WHERE iNamLamViec = @YearOfWork 
			AND iNamNganSach = @YearOfBudget 
			AND iID_MaNguonNganSach = @BudgetSource
			AND iLoai = @Type 
			AND iLoaiChungTu = @VoucherType
			AND (fTongTuChi <> 0 OR fTongHienVat <> 0 OR fTongPhanCap <> 0 OR fTongHangNhap <> 0 OR fTongHangMua <> 0 OR fTongDuPhong <> 0 OR fTongTonKho <> 0)
			AND (
				(dNgayQuyetDinh IS NOT NULL AND CAST(dNgayQuyetDinh AS DATE) <= CAST(@Date AS DATE)) 
				OR 
				(dNgayQuyetDinh IS NULL AND dNgayChungTu IS NOT NULL AND CAST(dNgayChungTu AS DATE) <= CAST(@Date AS DATE)))
	),
	tblChungTuNhanPhanBoMap AS (
		SELECT * 
		FROM NS_DT_Nhan_PhanBo_Map 
		WHERE iID_CTDuToan_Nhan IN (SELECT iID_DTChungTu FROM tblNhanPhanBo)
	),
	tblDaPhanBo AS (
		SELECT ctpb.iID_CTDuToan_Nhan, 
			   SUM(fTuChi) + SUM(fHienVat) + SUM(fPhanCap) + SUM(fHangNhap) + SUM(fHangMua) + SUM(fDuPhong) + SUM(fTonKho) AS DaPhanBo 
		FROM tblChungTuNhanPhanBoMap ctpb
		LEFT JOIN 
			(SELECT dtctct.*, dtct.iLoaiDuToan, dtct.iID_DotNhan FROM NS_DT_ChungTuChiTiet dtctct
			 LEFT JOIN NS_DT_ChungTu dtct
			 ON dtctct.iID_DTChungTu = dtct.iID_DTChungTu
			 WHERE dtctct.iNamLamViec = @YearOfWork 
				   AND dtct.iLoai = 1
				   AND dtct.iLoaiChungTu = 1
				   AND (dNgayQuyetDinh IS NOT NULL AND (CAST(dNgayQuyetDinh AS DATE) < CAST(@Date AS DATE) OR (CAST(dNgayQuyetDinh AS DATE) = CAST(@Date AS DATE) AND @Index > iSoChungTuIndex))) 
				   OR (dNgayQuyetDinh IS NULL AND dNgayChungTu IS NOT NULL AND (CAST(dNgayChungTu AS DATE) < CAST(@Date AS DATE) OR (CAST(dNgayChungTu AS DATE) = CAST(@Date AS DATE) AND @Index > iSoChungTuIndex)))
				   AND dtctct.iNamNganSach = @YearOfBudget 
				   AND dtctct.iID_MaNguonNganSach = @BudgetSource) ctct
		ON ctpb.iID_CTDuToan_Nhan = ctct.iID_CTDuToan_Nhan
		WHERE ctct.iID_DTChungTu IS NOT NULL
		and 
		(
			(ctpb.iID_CTDuToan_PhanBo = ctct.iID_DTChungTu AND ctct.iLoaiDuToan <> @DieuChinh)
			OR
			(ctct.iLoaiDuToan = @DieuChinh AND ctct.iID_DotNhan = ctpb.iID_CTDuToan_PhanBo)
		)
		GROUP BY ctpb.iID_CTDuToan_Nhan
	)

	SELECT 
		   npb.iID_DTChungTu AS Id,
		   npb.sSoChungTu AS SSoChungTu,
		   npb.dNgayChungTu AS DNgayChungTu,
		   npb.sDSLNS AS SDslns,
		   npb.sDSID_MaDonVi AS SDsidMaDonVi,
		   npb.sSoQuyetDinh AS SSoQuyetDinh,
		   npb.dNgayQuyetDinh AS DNgayQuyetDinh,
		   ISNULL(npb.SoPhanBo, 0) AS SoPhanBo,
		   ISNULL(DaPhanBo, 0) AS DaPhanBo,
		   ISNULL(npb.SoPhanBo, 0) - ISNULL(DaPhanBo, 0) AS SoChuaPhanBo 
	FROM tblNhanPhanBo npb
	LEFT JOIN tblDaPhanBo dpb
	ON npb.iID_DTChungTu = dpb.iID_CTDuToan_Nhan
	WHERE ISNULL(npb.SoPhanBo, 0) <> ISNULL(DaPhanBo, 0)
	ORDER BY npb.sSoChungTu

END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_rpt_dutoan_theodot]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_dt_rpt_dutoan_theodot]
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	--@VoucherDate datetime,
	@ChungTuId nvarchar(max),
	@dvt int
AS
BEGIN
	SELECT --isnull(ctct.iID_DTCTChiTiet, NEWID()) AS iID_DTCTChiTiet,
       --ctct.iID_DTChungTu,
       mlns.iID_MLNS,
       mlns.iID_MLNS_Cha,
       mlns.sXauNoiMa,
       mlns.sLNS,
       mlns.sL,
       mlns.sK,
       mlns.sM,
       mlns.sTM,
       mlns.sTTM,
       mlns.sNG,
       mlns.sTNG,
       mlns.sTNG1,
       mlns.sTNG2,
       mlns.sTNG3,
       mlns.sMoTa,
       mlns.bHangCha,
       ctct.iNamNganSach,
       ctct.iID_MaNguonNganSach,
       ctct.iNamLamViec,
       sum(isnull(ctct.iPhanCap, 0)) AS iPhanCap,
       ctct.iID_MaDonVi,
       --sum(isnull(ctct.sGhiChu, '')) AS sGhiChu,
       sum(isnull(ctct.fHangMua, 0))/@dvt AS fHangMua,
       sum(isnull(ctct.fHangNhap, 0))/@dvt AS fHangNhap,
       sum(isnull(ctct.fDuPhong, 0))/@dvt AS fDuPhong,
       sum(isnull(ctct.fPhanCap, 0))/@dvt AS fPhanCap,
       sum(isnull(ctct.fTuChi, 0))/@dvt AS fTuChi,
       sum(isnull(ctct.fHienVat, 0))/@dvt AS fHienVat,
	   sum(isnull(ctct.fTonKho, 0))/@dvt AS fTonKho,
       --ctct.dNgayTao,
       --ctct.sNguoiTao,
       --ctct.dNgaySua,
       --ctct.sNguoiSua, 
	   ctct.iID_CTDuToan_Nhan,
	   --ctct.Id_DotPhanBoTruoc,
	   isnull(ctct.iDuLieuNhan, 0) as iDuLieuNhan,
	   mlns.sChiTietToi,
	   dv.sTenDonVi,
	   mlns.bHangChaDuToan
	FROM NS_MucLucNganSach mlns
	LEFT JOIN
	  (SELECT *
	   FROM NS_DT_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
		 AND iNamNganSach = @YearOfBudget
		 AND iID_MaNguonNganSach = @BudgetSource
		 AND iPhanCap = 0
		 AND iDuLieuNhan = 0
		 AND iID_DTChungTu IN
		   (SELECT iID_DTChungTu
			FROM NS_DT_ChungTu
			WHERE iLoai = 0
			  AND iNamLamViec = @YearOfWork
			  AND iNamNganSach = @YearOfBudget
			  AND iID_MaNguonNganSach = @BudgetSource
			  AND convert(nvarchar(MAX), iID_DTChungTu) IN
				(SELECT *
				 FROM dbo.f_split(@ChungTuId)) ) ) ctct ON mlns.iID_MLNS = ctct.iID_MLNS
	LEFT JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1) dv
	ON dv.iID_MaDonVi = ctct.iID_MaDonVi
	WHERE mlns.iNamLamViec = @YearOfWork
	  AND mlns.bHangChaDuToan IS NOT NULL
	 -- AND mlns.sLNS in
		--(SELECT *
		-- FROM dbo.f_split(@LNS))
	GROUP BY mlns.iID_MLNS,
       mlns.iID_MLNS_Cha,
       mlns.sXauNoiMa,
       mlns.sLNS,
       mlns.sL,
       mlns.sK,
       mlns.sM,
       mlns.sTM,
       mlns.sTTM,
       mlns.sNG,
       mlns.sTNG,
       mlns.sTNG1,
       mlns.sTNG2,
       mlns.sTNG3,
       mlns.sMoTa,
       mlns.bHangCha,
       ctct.iNamNganSach,
       ctct.iID_MaNguonNganSach,
       ctct.iNamLamViec,
       ctct.iID_MaDonVi,
       --ctct.dNgayTao,
       --ctct.sNguoiTao,
       --ctct.dNgaySua,
       --ctct.sNguoiSua, 
	   ctct.iID_CTDuToan_Nhan,
	   --ctct.Id_DotPhanBoTruoc,
	   isnull(ctct.iDuLieuNhan, 0),
	   mlns.sChiTietToi,
	   dv.sTenDonVi,
	   mlns.bHangChaDuToan
	ORDER BY mlns.sXauNoiMa;
END
/****** Object:  StoredProcedure [dbo].[sp_dt_phan_bo_du_toan_chi_tiet_used_selected_in_dialog]    Script Date: 17/12/2021 8:07:59 AM ******/
SET ANSI_NULLS ON
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_get_khluachonnhathau_byduanid]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_get_khluachonnhathau_byduanid]
@iId uniqueidentifier
AS
BEGIN
	SELECT *  
	FROM VDT_QDDT_KHLCNhaThau
	WHERE iID_DuAnID = @iId and bActive = 1
END 
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_get_thongtriquyettoan_chitiet]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_get_thongtriquyettoan_chitiet]
	@Id_ThongTriQuyetToanID UNIQUEIDENTIFIER = NULL
AS
BEGIN
	SELECT
		TTQT_CT.ID AS ID,
		DM_NVC.ID AS iID_KHTT_NhiemVuChiID,
		DM_NVC.sTenNhiemVuChi,
		TTQT_CT.iID_DuAnID,
		DA.sTenDuAn,
		TTQT_CT.iID_HopDongID,
		HD.sTenHopDong,
		TTQT_CT.iID_ThanhToan_ChiTietID,
		TTCT.sTenNoiDungChi,
		TTQT_CT.fDeNghiQuyetToanNam_USD AS fDeNghiQuyetToanNam_USD,
		TTQT_CT.fDeNghiQuyetToanNam_VND AS fDeNghiQuyetToanNam_VND,
		TTQT_CT.fThuaNopTraNSNN_USD AS fThuaNopTraNSNN_USD,
		TTQT_CT.fThuaNopTraNSNN_VND AS fThuaNopTraNSNN_VND,
		TTCT.iID_MucLucNganSachID,
		TT.ID AS iID_ThanhToanID,
		TT.iLoaiNoiDungChi,
		MLNS.sM,
		MLNS.sTM,
		MLNS.sTTM,
		MLNS.sNG
	FROM NH_QT_ThongTriQuyetToan_ChiTiet TTQT_CT
	
	-- Join to get data
	LEFT JOIN NH_TT_ThanhToan_ChiTiet AS TTCT ON TTQT_CT.iID_ThanhToan_ChiTietID = TTCT.ID
	LEFT JOIN NH_TT_ThanhToan AS TT ON TTCT.iID_DeNghiThanhToanID = TT.ID
	LEFT JOIN NS_MucLucNganSach AS MLNS ON TTCT.iID_MucLucNganSachID = MLNS.iID
	left join NH_DM_NhiemVuChi DM_NVC on TT.iID_NhiemVuChiID = DM_NVC.ID
	LEFT JOIN NH_DA_DuAn AS DA ON TTQT_CT.iID_DuAnID = DA.ID
	LEFT JOIN NH_DA_HopDong AS HD ON TTQT_CT.iID_HopDongID = HD.ID
	WHERE iID_ThongTriQuyetToanID = @Id_ThongTriQuyetToanID
END;


GO
/****** Object:  StoredProcedure [dbo].[sp_insert_nh_tonghop_giam]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_insert_nh_tonghop_giam]
	-- Add the parameters for the stored procedure here
	@sLoai nvarchar(100),
	@iTypeExecute int,   -- 1: add, 2: update, 3: delete, 4: điều chỉnh
	@uIdQuyetDinh uniqueidentifier,
	@iIDQuyetDinhOld uniqueidentifier
AS
BEGIN
	CREATE TABLE #lstMaDich(sMaDich nvarchar(100))

	IF(@sLoai = 'TTCP')
	BEGIN
		INSERT INTO #lstMaDich(sMaDich)
		VALUES('TTCP'), ('111'), ('112'), ('121'), ('122')
	END
	ELSE IF(@sLoai = 'QTND')
	BEGIN
		INSERT INTO #lstMaDich(sMaDich)
		VALUES('TTQT'), ('201')
	END
	ELSE IF(@sLoai = 'KHOI_TAO')
	BEGIN
		INSERT INTO #lstMaDich(sMaDich)
		VALUES('KHOI_TAO'), ('311'), ('321')
	END
	ELSE IF(@sLoai = 'QUYET_TOAN')
	BEGIN
		INSERT INTO #lstMaDich(sMaDich)
		VALUES('QUYET_TOAN'), ('311'), ('321')
	END

	IF(@iTypeExecute in (2,3,4))
	BEGIN 
		IF (@iTypeExecute in (2,3))
		BEGIN
			-- dao nguoc but toan
			INSERT INTO NH_TH_TongHop(iID_DuAnID,iID_KHTT_NhiemVuChiID, iID_HopDongId, iID_DonVi, iID_MaDonViQuanLy, iCoQuanThanhToan, dNgayDeNghi, iNamKeHoach, iID_ChungTu, sMaNguon, sMaDich, sMaNguonCha, fGiaTriUsd, fGiaTriVnd, iID_TiGia, bIsLog, iStatus, sMaTienTrinh,
												iID_MucLucNganSach)
			SELECT tbl.iID_DuAnID, tbl.iID_KHTT_NhiemVuChiID, tbl.iID_HopDongId, tbl.iID_DonVi, iID_MaDonViQuanLy, tbl.iCoQuanThanhToan, tbl.dNgayDeNghi, iNamKeHoach, iID_ChungTu, tbl.sMaDich, tbl.sMaNguon, tbl.sMaNguonCha, fGiaTriUsd, fGiaTriVnd, iID_TiGia, 1, 2, '300',
				iID_MucLucNganSach
			FROM NH_TH_TongHop as tbl
			INNER JOIN #lstMaDich as dt on tbl.sMaDich COLLATE DATABASE_DEFAULT = dt.sMaDich COLLATE DATABASE_DEFAULT
			WHERE tbl.iID_ChungTu = @uIdQuyetDinh 
				AND bIsLog = 0 AND sMaTienTrinh = (CASE WHEN @sLoai = 'QUYET_TOAN' THEN '100' ELSE '200' END)

			-- khoa but toan da update
			UPDATE tbl 
			SET 
				bIsLog = 1
			FROM NH_TH_TongHop as tbl
			INNER JOIN #lstMaDich as dt on tbl.sMaDich COLLATE DATABASE_DEFAULT = dt.sMaDich COLLATE DATABASE_DEFAULT
			WHERE tbl.iID_ChungTu = @uIdQuyetDinh
				AND bIsLog = 0 AND sMaTienTrinh in ('100', '200')
		END
		ELSE IF (@iTypeExecute = 4)
		BEGIN
			-- dao nguoc but toan
			INSERT INTO NH_TH_TongHop(iID_DuAnID, iID_KHTT_NhiemVuChiID, iID_HopDongId, iID_DonVi, iID_MaDonViQuanLy, iCoQuanThanhToan, dNgayDeNghi, iNamKeHoach, iID_ChungTu, sMaNguon, sMaDich, sMaNguonCha, fGiaTriUsd, fGiaTriVnd, iID_TiGia, bIsLog, iStatus, sMaTienTrinh,
												iID_MucLucNganSach)
			SELECT tbl.iID_DuAnID, tbl.iID_KHTT_NhiemVuChiID, tbl.iID_HopDongId, tbl.iID_DonVi, iID_MaDonViQuanLy, tbl.iCoQuanThanhToan, tbl.dNgayDeNghi, iNamKeHoach, iID_ChungTu, tbl.sMaDich, tbl.sMaNguon, tbl.sMaNguonCha, fGiaTriUsd, fGiaTriVnd, iID_TiGia, 1, 2, '300',
					iID_MucLucNganSach
			FROM NH_TH_TongHop as tbl
			INNER JOIN #lstMaDich as dt on tbl.sMaDich COLLATE DATABASE_DEFAULT = dt.sMaDich COLLATE DATABASE_DEFAULT
			WHERE tbl.iID_ChungTu = @iIDQuyetDinhOld 
				AND bIsLog = 0 AND sMaTienTrinh in ('100', '200')

			-- khoa but toan da update
			UPDATE tbl 
			SET 
				bIsLog = 1
			FROM NH_TH_TongHop as tbl
			INNER JOIN #lstMaDich as dt on tbl.sMaDich COLLATE DATABASE_DEFAULT = dt.sMaDich COLLATE DATABASE_DEFAULT
			WHERE tbl.iID_ChungTu = @iIDQuyetDinhOld
				AND bIsLog = 0 AND sMaTienTrinh in ('100', '200')
		END

		-- deleted thi khong xu ly nua
		IF(@iTypeExecute = 3)
		BEGIN
			RETURN
		END
	END
	IF(@sLoai = 'TTCP')
	BEGIN
		-- insert bút toan moi vao 
		INSERT INTO NH_TH_TongHop(iID_DuAnID,iID_KHTT_NhiemVuChiID, iID_HopDongId, iID_DonVi, iID_MaDonViQuanLy, iCoQuanThanhToan, dNgayDeNghi, iNamKeHoach, iQuyKeHoach, iID_ChungTu, sMaNguon, sMaDich, sMaNguonCha, fGiaTriUsd, fGiaTriVnd, iID_TiGia, iStatus, sMaTienTrinh, iID_MucLucNganSach, bIsLog)
		SELECT tbl.iID_DuAnID, ktt.ID, tbl.iID_HopDongId, tbl.iID_DonVi, tbl.iID_MaDonVi, tbl.iCoQuanThanhToan, tbl.dNgayDeNghi, tbl.iNamKeHoach, tbl.iQuyKeHoach, tbl.ID
		      , '000', (CASE WHEN tbl.iNamNganSach = 1 AND tbl.iLoaiDeNghi = 2 THEN '111' WHEN tbl.iNamNganSach = 1 AND tbl.iLoaiDeNghi = 3 THEN '121' WHEN tbl.iNamNganSach = 2 AND tbl.iLoaiDeNghi = 2 THEN '112' ELSE '122' END)
			  , (CASE WHEN tbl.iNamNganSach = 1 THEN '101' ELSE '102' END)
			  , dt.fPheDuyetCapKyNay_USD, dt.fPheDuyetCapKyNay_VND, tbl.iID_TiGiaID, 0, '200', dt.iID_MucLucNganSachID, 0
		FROM NH_TT_ThanhToan as tbl
		INNER JOIN NH_KHTongThe_NhiemVuChi as ktt on ktt.iID_KHTongTheID = tbl.iID_KHTongTheID and ktt.iID_NhiemVuChiID = tbl.iID_NhiemVuChiID
		INNER JOIN NH_TT_ThanhToan_ChiTiet as dt on tbl.ID = dt.iID_DeNghiThanhToanID
		WHERE tbl.Id = @uIdQuyetDinh
	END
	ELSE IF(@sLoai = 'QTND')
	BEGIN
		-- insert but toan moi vao 
		INSERT INTO NH_TH_TongHop(iID_DuAnID, iID_KHTT_NhiemVuChiID, iID_HopDongId, iID_DonVi, iID_MaDonViQuanLy, iNamKeHoach, iID_ChungTu, sMaNguon, sMaDich, sMaNguonCha, fGiaTriUsd, fGiaTriVnd, iID_TiGia, iStatus, sMaTienTrinh, iID_MucLucNganSach, bIsLog)
		SELECT dt.iID_DuAnID,dt.iID_KHTT_NhiemVuChiID, dt.iID_HopDongId, tbl.iID_DonViID, tbl.iID_MaDonVi,
				tbl.iNamKeHoach, tbl.Id , '000', '201', '101', dt.fGiaTriUSD, dt.fGiaTriVND, tbl.iID_TiGiaID, 0, '200', 
				dt.iID_MucLucNganSachID, 0
		FROM NH_QT_QuyetToanNienDo as tbl
		INNER JOIN (select Id,iID_QuyetToanNienDoID,iID_DuAnID,iID_KHTT_NhiemVuChiID, iID_HopDongId, iID_MucLucNganSachID, fGiaTriUSD, fGiaTriVND
						from 
						(select Id,iID_QuyetToanNienDoID,iID_KHTT_NhiemVuChiID,iID_DuAnID, iID_HopDongId, iID_MucLucNganSachID, fThuaNopNSNN_USD as fGiaTriUSD, fThuaNopNSNN_VND as fGiaTriVND from NH_QT_QuyetToanNienDo_ChiTiet where bIsSaveTongHop = 1) as tbl
                         ) as dt on dt.iID_QuyetToanNienDoID = tbl.Id
		WHERE tbl.Id = @uIdQuyetDinh
	END
	ELSE IF(@sLoai = 'KHOI_TAO')
	BEGIN
		-- insert but toan moi vao 
		INSERT INTO NH_TH_TongHop(iID_DuAnID, iID_HopDongId, iID_DonVi, iID_MaDonViQuanLy, iNamKeHoach, iID_ChungTu, sMaNguon, sMaDich, sMaNguonCha, fGiaTriUsd, fGiaTriVnd, iID_TiGia, iStatus, sMaTienTrinh, bIsLog)
		SELECT dt.iID_DuAnID, dt.iID_HopDongId, tbl.iID_DonViID, tbl.iID_MaDonVi,
				tbl.iNamKhoiTao, tbl.Id , '000', dt.sMaDich, dt.sMaNguonCha, dt.fGiaTriUSD, dt.fGiaTriVND, tbl.iID_TiGiaID, 0, '200', 0
		FROM NH_KT_KhoiTaoCapPhat as tbl
		INNER JOIN (select Id, iID_KhoiTaoCapPhatID, iID_DuAnID, iID_HopDongId, dt.fGiaTriUSD, dt.fGiaTriVND, dt.sMaDich, dt.sMaNguonCha
						from 
						(select Id, iID_KhoiTaoCapPhatID, iID_DuAnID, iID_HopDongId, fQTKinhPhiDuyetCacNamTruoc_USD, fQTKinhPhiDuyetCacNamTruoc_VND, fDeNghiQTNamNay_USD, fDeNghiQTNamNay_VND from NH_KT_KhoiTaoCapPhat_ChiTiet) as tbl
						CROSS APPLY (VALUES(fQTKinhPhiDuyetCacNamTruoc_USD, fQTKinhPhiDuyetCacNamTruoc_VND, '311', '302'),
						(fDeNghiQTNamNay_USD, fDeNghiQTNamNay_VND, '321', '301'))dt(fGiaTriUSD,fGiaTriVND, sMaDich, sMaNguonCha)
					) as dt on dt.iID_KhoiTaoCapPhatID = tbl.Id
		WHERE tbl.Id = @uIdQuyetDinh
	END
	ELSE IF(@sLoai = 'QUYET_TOAN')
	BEGIN
		-- insert but toan moi vao 
		INSERT INTO NH_TH_TongHop(iID_DuAnID, iID_KHTT_NhiemVuChiID, iID_HopDongId, iID_DonVi, iID_MaDonViQuanLy, iNamKeHoach, iID_ChungTu, sMaNguon, sMaDich, sMaNguonCha, fGiaTriUsd, fGiaTriVnd, iID_TiGia, iStatus, sMaTienTrinh, iID_MucLucNganSach, bIsLog)
		SELECT dt.iID_DuAnID, dt.iID_KHTT_NhiemVuChiID, dt.iID_HopDongId, tbl.iID_DonViID, tbl.iID_MaDonVi,
				tbl.iNamKeHoach, tbl.Id , '000', dt.sMaDich, dt.sMaNguonCha, dt.fGiaTriUSD, dt.fGiaTriVND, tbl.iID_TiGiaID, 0, '100', 
				dt.iID_MucLucNganSachID, 0
		FROM NH_QT_QuyetToanNienDo as tbl
		INNER JOIN (select Id,iID_QuyetToanNienDoID,iID_KHTT_NhiemVuChiID ,iID_DuAnID, iID_HopDongId, iID_MucLucNganSachID, dt.fGiaTriUSD, dt.fGiaTriVND, dt.sMaDich, dt.sMaNguonCha
						from 
						(select Id,iID_QuyetToanNienDoID, iID_KHTT_NhiemVuChiID,iID_DuAnID, iID_HopDongId, iID_MucLucNganSachID, fQTKinhPhiDuyetCacNamTruoc_USD, fQTKinhPhiDuyetCacNamTruoc_VND, fDeNghiQTNamNay_USD, fDeNghiQTNamNay_VND from NH_QT_QuyetToanNienDo_ChiTiet where bIsSaveTongHop = 1) as tbl
						CROSS APPLY (VALUES(fQTKinhPhiDuyetCacNamTruoc_USD, fQTKinhPhiDuyetCacNamTruoc_VND, '311', '302'),
						(fDeNghiQTNamNay_USD, fDeNghiQTNamNay_VND, '321', '301'))dt(fGiaTriUSD,fGiaTriVND, sMaDich, sMaNguonCha)
					) as dt on dt.iID_QuyetToanNienDoID = tbl.Id
		WHERE tbl.Id = @uIdQuyetDinh
	END
END
GO
/****** Object:  StoredProcedure [dbo].[sp_insert_nh_tonghop_tang]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROC [dbo].[sp_insert_nh_tonghop_tang]
	-- Add the parameters for the stored procedure here
	@sLoai nvarchar(100),
	@iTypeExecute int,   -- 1: add, 2: update, 3: delete, 4: điều chỉnh
	@uIdQuyetDinh uniqueidentifier,
	@iIDQuyetDinhOld uniqueidentifier
AS
BEGIN
	CREATE TABLE #lstMaNguon(sMaNguon nvarchar(100))

	IF(@sLoai = 'TTCP')
	BEGIN
		INSERT INTO #lstMaNguon(sMaNguon)
		VALUES('TTCP'), ('101'), ('102'), ('121'), ('122')
	END
	ELSE IF(@sLoai = 'KHOI_TAO')
	BEGIN
		INSERT INTO #lstMaNguon(sMaNguon)
		VALUES('KHOI_TAO'), ('303')
	END
	ELSE IF(@sLoai = 'QUYET_TOAN')
	BEGIN
		INSERT INTO #lstMaNguon(sMaNguon)
		VALUES('QUYET_TOAN'), ('301'), ('302'), ('303')
	END

	IF(@iTypeExecute in (2,3,4))
	BEGIN 
		IF (@iTypeExecute in (2,3))
		BEGIN
			-- dao nguoc but toan
			INSERT INTO NH_TH_TongHop(iID_DuAnID, iID_KHTT_NhiemVuChiID, iID_HopDongId, iID_DonVi, iID_MaDonViQuanLy, iCoQuanThanhToan, dNgayDeNghi, iNamKeHoach, iID_ChungTu, sMaNguon, sMaDich, sMaNguonCha, fGiaTriUsd, fGiaTriVnd, iID_TiGia, bIsLog, iStatus, sMaTienTrinh,
												iID_MucLucNganSach)
			SELECT tbl.iID_DuAnID, tbl.iID_KHTT_NhiemVuChiID, tbl.iID_HopDongId, tbl.iID_DonVi, iID_MaDonViQuanLy, tbl.iCoQuanThanhToan, tbl.dNgayDeNghi, iNamKeHoach, iID_ChungTu, tbl.sMaDich, tbl.sMaNguon, tbl.sMaNguonCha, fGiaTriUsd, fGiaTriVnd, iID_TiGia, 1, 2, '300',
				iID_MucLucNganSach
			FROM NH_TH_TongHop as tbl
			INNER JOIN #lstMaNguon as dt on tbl.sMaNguon COLLATE DATABASE_DEFAULT = dt.sMaNguon COLLATE DATABASE_DEFAULT
			WHERE tbl.iID_ChungTu = @uIdQuyetDinh 
				AND bIsLog = 0 AND sMaTienTrinh = (CASE WHEN @sLoai = 'QUYET_TOAN' THEN '100' ELSE '200' END)

			-- khoa but toan da update
			UPDATE tbl 
			SET 
				bIsLog = 1
			FROM NH_TH_TongHop as tbl
			INNER JOIN #lstMaNguon as dt on tbl.sMaNguon COLLATE DATABASE_DEFAULT = dt.sMaNguon COLLATE DATABASE_DEFAULT
			WHERE tbl.iID_ChungTu = @uIdQuyetDinh
				AND bIsLog = 0 AND sMaTienTrinh in ('100', '200')
		END
		ELSE IF (@iTypeExecute = 4)
		BEGIN
			-- dao nguoc but toan
			INSERT INTO NH_TH_TongHop(iID_DuAnID, iID_KHTT_NhiemVuChiID, iID_HopDongId, iID_DonVi, iID_MaDonViQuanLy, iCoQuanThanhToan, dNgayDeNghi, iNamKeHoach, iID_ChungTu, sMaNguon, sMaDich, sMaNguonCha, fGiaTriUsd, fGiaTriVnd, iID_TiGia, bIsLog, iStatus, sMaTienTrinh,
												iID_MucLucNganSach)
			SELECT tbl.iID_DuAnID, tbl.iID_KHTT_NhiemVuChiID, tbl.iID_HopDongId, tbl.iID_DonVi, iID_MaDonViQuanLy, tbl.iCoQuanThanhToan, tbl.dNgayDeNghi, iNamKeHoach, iID_ChungTu, tbl.sMaDich, tbl.sMaNguon, tbl.sMaNguonCha, fGiaTriUsd, fGiaTriVnd, iID_TiGia, 1, 2, '300',
					iID_MucLucNganSach
			FROM NH_TH_TongHop as tbl
			INNER JOIN #lstMaNguon as dt on tbl.sMaNguon COLLATE DATABASE_DEFAULT = dt.sMaNguon COLLATE DATABASE_DEFAULT
			WHERE tbl.iID_ChungTu = @iIDQuyetDinhOld 
				AND bIsLog = 0 AND sMaTienTrinh in ('100', '200')

			-- khoa but toan da update
			UPDATE tbl 
			SET 
				bIsLog = 1
			FROM NH_TH_TongHop as tbl
			INNER JOIN #lstMaNguon as dt on tbl.sMaNguon COLLATE DATABASE_DEFAULT = dt.sMaNguon COLLATE DATABASE_DEFAULT
			WHERE tbl.iID_ChungTu = @iIDQuyetDinhOld
				AND bIsLog = 0 AND sMaTienTrinh in ('100', '200')
		END

		-- deleted thi khong xu ly nua
		IF(@iTypeExecute = 3)
		BEGIN
			RETURN
		END
	END
	IF(@sLoai = 'TTCP')
	BEGIN
		-- insert bút toan moi vao 
		INSERT INTO NH_TH_TongHop(iID_DuAnID, iID_KHTT_NhiemVuChiID, iID_HopDongId, iID_DonVi, iID_MaDonViQuanLy, iCoQuanThanhToan, dNgayDeNghi, iNamKeHoach, iQuyKeHoach, iID_ChungTu, sMaNguon, sMaDich, fGiaTriUsd, fGiaTriVnd, iID_TiGia, iStatus, sMaTienTrinh, iID_MucLucNganSach, bIsLog)
		SELECT tbl.iID_DuAnID, ktt.ID, tbl.iID_HopDongId, tbl.iID_DonVi, tbl.iID_MaDonVi, tbl.iCoQuanThanhToan, tbl.dNgayDeNghi, tbl.iNamKeHoach, tbl.iQuyKeHoach, tbl.ID, (CASE WHEN tbl.iNamNganSach = 1 THEN '101' ELSE '102' END), '000', dt.fPheDuyetCapKyNay_USD, dt.fPheDuyetCapKyNay_VND, tbl.iID_TiGiaID, 0, '200', dt.iID_MucLucNganSachID, 0
		FROM NH_TT_ThanhToan as tbl
		INNER JOIN NH_KHTongThe_NhiemVuChi as ktt on ktt.iID_KHTongTheID = tbl.iID_KHTongTheID and ktt.iID_NhiemVuChiID = tbl.iID_NhiemVuChiID
		INNER JOIN NH_TT_ThanhToan_ChiTiet as dt on tbl.ID = dt.iID_DeNghiThanhToanID
		WHERE tbl.Id = @uIdQuyetDinh AND tbl.iLoaiDeNghi = 1
		-- insert but toan moi vao: thu hồi nợ
		INSERT INTO NH_TH_TongHop(iID_DuAnID, iID_KHTT_NhiemVuChiID, iID_HopDongId, iID_DonVi, iID_MaDonViQuanLy, iCoQuanThanhToan, dNgayDeNghi, iNamKeHoach, iQuyKeHoach, iID_ChungTu, sMaNguon, sMaDich, fGiaTriUsd, fGiaTriVnd, iID_TiGia, iStatus, sMaTienTrinh, bIsLog)
		SELECT tbl.iID_DuAnID, ktt.ID, tbl.iID_HopDongId, tbl.iID_DonVi, tbl.iID_MaDonVi, tbl.iCoQuanThanhToan, tbl.dNgayDeNghi, tbl.iNamKeHoach, tbl.iQuyKeHoach, tbl.ID, (CASE WHEN tbl.iNamNganSach = 1 THEN '121' ELSE '122' END), '000', tbl.fThuHoiTamUngPheDuyet_BangSo_USD, tbl.fThuHoiTamUngPheDuyet_BangSo_VND, tbl.iID_TiGiaID, 0, '200', 0
		FROM NH_TT_ThanhToan as tbl
		INNER JOIN NH_KHTongThe_NhiemVuChi as ktt on ktt.iID_KHTongTheID = tbl.iID_KHTongTheID and ktt.iID_NhiemVuChiID = tbl.iID_NhiemVuChiID
		WHERE tbl.Id = @uIdQuyetDinh AND tbl.iLoaiDeNghi = 3
	END
	ELSE IF(@sLoai = 'KHOI_TAO')
	BEGIN
		-- insert but toan moi vao 
		INSERT INTO NH_TH_TongHop(iID_DuAnID, iID_HopDongId, iID_DonVi, iID_MaDonViQuanLy, iNamKeHoach, iID_ChungTu, sMaNguon, sMaDich, fGiaTriUsd, fGiaTriVnd, iID_TiGia, iStatus, sMaTienTrinh, bIsLog)
		SELECT dt.iID_DuAnID, dt.iID_HopDongId, tbl.iID_DonViID, tbl.iID_MaDonVi, tbl.iNamKhoiTao, tbl.Id , '303', '000', dt.fLuyKeKinhPhiDuocCap_USD, dt.fLuyKeKinhPhiDuocCap_VND, tbl.iID_TiGiaID, 0, '200', 0
		FROM NH_KT_KhoiTaoCapPhat as tbl
		INNER JOIN NH_KT_KhoiTaoCapPhat_ChiTiet as dt on dt.iID_KhoiTaoCapPhatID = tbl.Id
		WHERE tbl.Id = @uIdQuyetDinh
	END
	ELSE IF(@sLoai = 'QUYET_TOAN')
	BEGIN
		-- insert but toan moi vao 
		INSERT INTO NH_TH_TongHop(iID_DuAnID, iID_KHTT_NhiemVuChiID, iID_HopDongId,iID_DonVi, iID_MaDonViQuanLy, iNamKeHoach, iID_ChungTu, sMaNguon, sMaDich, fGiaTriUsd, fGiaTriVnd, iID_TiGia, iStatus, sMaTienTrinh, iID_MucLucNganSach, bIsLog)
		SELECT dt.iID_DuAnID, dt.iID_KHTT_NhiemVuChiID, dt.iID_HopDongId,tbl.iID_DonViID, tbl.iID_MaDonVi,
				tbl.iNamKeHoach, tbl.Id , dt.sMaNguon, '000', dt.fGiaTriUSD, dt.fGiaTriVND, tbl.iID_TiGiaID, 0, '100', 
				dt.iID_MucLucNganSachID, 0
		FROM NH_QT_QuyetToanNienDo as tbl
		INNER JOIN (select Id,iID_QuyetToanNienDoID,iID_KHTT_NhiemVuChiID,iID_DuAnID, iID_HopDongId, iID_MucLucNganSachID, dt.fGiaTriUSD, dt.fGiaTriVND, dt.sMaNguon
						from 
						(select Id,iID_QuyetToanNienDoID,iID_KHTT_NhiemVuChiID,iID_DuAnID, iID_HopDongId, iID_MucLucNganSachID, fQTKinhPhiDuocCap_NamTruocChuyenSang_USD, fQTKinhPhiDuocCap_NamTruocChuyenSang_VND, fQTKinhPhiDuocCap_NamNay_USD, fQTKinhPhiDuocCap_NamNay_VND, fLuyKeKinhPhiDuocCap_USD, fLuyKeKinhPhiDuocCap_VND from NH_QT_QuyetToanNienDo_ChiTiet where bIsSaveTongHop = 1) as tbl
						CROSS APPLY (VALUES(fQTKinhPhiDuocCap_NamTruocChuyenSang_USD, fQTKinhPhiDuocCap_NamTruocChuyenSang_VND, '302'),
						(fQTKinhPhiDuocCap_NamNay_USD, fQTKinhPhiDuocCap_NamNay_VND, '301'),
						(fLuyKeKinhPhiDuocCap_USD, fLuyKeKinhPhiDuocCap_VND, '303'))dt(fGiaTriUSD,fGiaTriVND, sMaNguon)
					) as dt on dt.iID_QuyetToanNienDoID = tbl.Id
		WHERE tbl.Id = @uIdQuyetDinh
	END
END
GO
/****** Object:  StoredProcedure [dbo].[sp_insert_nhtonghop]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_insert_nhtonghop]
	@iIDChungTu uniqueidentifier,
	@sLoai nvarchar(100),
	@data t_tbl_nh_tonghop READONLY
AS
BEGIN
    DECLARE @iIDDonVi uniqueidentifier
	DECLARE @iIDMaDonViQuanLy nvarchar(100)
	DECLARE @iNamKeHoach int
	DECLARE @iQuyKeHoach int
	DECLARE @iIDTiGia uniqueidentifier
	DECLARE @iCoQuanThanhToan int
	DECLARE @dNgayDeNghi date

	IF (@sLoai = 'TTCP')
	BEGIN
		SELECT @iIDDonVi = iID_DonVi,
		    @iIDMaDonViQuanLy = iID_MaDonVi,
			@iNamKeHoach = iNamKeHoach,
			@iQuyKeHoach = iQuyKeHoach,
			@iIDTiGia = iID_TiGiaID,
			@iCoQuanThanhToan = iCoQuanThanhToan,
			@dNgayDeNghi = dNgayDeNghi
		FROM NH_TT_ThanhToan WHERE Id = @iIDChungTu
	END
	ELSE IF (@sLoai = 'QUYET_TOAN')
	BEGIN
		SELECT @iIDDonVi = iID_DonViID,
		    @iIDMaDonViQuanLy = iID_MaDonVi,
			@iNamKeHoach = iNamKeHoach,
			@iIDTiGia = iID_TiGiaID
		FROM NH_QT_QuyetToanNienDo WHERE Id = @iIDChungTu
	END

	INSERT INTO NH_TH_TongHop(iID_DuAnId, iID_HopDongId, iID_DonVi, iID_MaDonViQuanLy, iCoQuanThanhToan, dNgayDeNghi, iNamKeHoach, iQuyKeHoach, iID_ChungTu, sMaNguon, sMaDich, sMaNguonCha, fGiaTriUsd, fGiaTriVnd, iID_TiGia, iID_MucLucNganSach, iStatus, sMaTienTrinh, bIsLog)
	SELECT tbl.iID_DuAnId, tbl.iID_HopDongId, @iIDDonVi,  @iIDMaDonViQuanLy, @iCoQuanThanhToan, @dNgayDeNghi, @iNamKeHoach, @iQuyKeHoach, @iIDChungTu, tbl.sMaNguon, tbl.sMaDich, tbl.sMaNguonCha, tbl.fGiaTriUsd, tbl.fGiaTriVnd, @iIDTiGia, tbl.iID_MucLucNganSach, tbl.iStatus, case when @sLoai = 'QUYET_TOAN' then '100' else '200' end, 0
	FROM @data as tbl
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_luong_get_dmphucap_dctapthecanbo]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_luong_get_dmphucap_dctapthecanbo]
AS
BEGIN
	CREATE TABLE #tmpExclude(code nvarchar(200))
	INSERT INTO #tmpExclude(code) VALUES('THUONG_TT'),('GIAMTHUE_TT'),('THUNHAPKHAC_TT'),('THUEDANOP_TT'),('TIENCTLH_TT'),('TIENANDUONG_TT'),('TIENTAUXE_TT')

	CREATE TABLE #tmp(id uniqueidentifier, code nvarchar(200))
	CREATE TABLE #child(id uniqueidentifier)

	INSERT INTO #tmp(id, code)
	SELECT Id , Ma_PhuCap
	FROM TL_DM_PhuCap as pc
	LEFT JOIN #tmpExclude as ec on pc.Ma_PhuCap = ec.code
	WHERE ISNULL(Parent, '') = ''
	AND Chon = 1 AND Is_Formula = 0 AND Is_Readonly = 0 AND ec.code IS NULL

	INSERT INTO #child(id)
	SELECT dt.Id
	FROM #tmp as tmp
	INNER JOIN TL_DM_PhuCap as dt on tmp.code = dt.Parent
	LEFT JOIN #tmpExclude as ec on dt.Ma_PhuCap = ec.code
	WHERE dt.Chon = 1 AND Is_Formula = 0 AND Is_Readonly = 0 AND ec.code IS NULL

	SELECT tbl.*
	FROM #child as tmp
	INNER JOIN TL_DM_PhuCap AS tbl on tmp.id = tbl.Id

	DROP TABLE #tmp
	DROP TABLE #child
END
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_baocao_nhucau_chitiet]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_baocao_nhucau_chitiet]
	@idNhuCauChiQuy uniqueidentifier
AS
BEGIN
	
	SELECT NCCQ.iID_DonViID , CT.*, CT.fNhuCauQuyNay_USD as fChiNgoaiTeUSDTyGia , fNhuCauQuyNay_VND as fChiTrongNuocVNDTyGia ,
Case When CT.iID_HopDongID is not null then HD.sTenHopDong
 		Else CT.sNoiDung
	End as sTenHopDong ,
	dv.sTenDonVi as sTenDonvi , BQP.ID as ID_NhiemVuChi,
	Case When DMBQP.sTenNhiemVuChi is not null then DMBQP.sTenNhiemVuChi
 		Else N'Chưa có chương trình'
	End as sTenNhiemVuChi,
Case 
	when CT.iID_HopDongID is null then CT.fNoiDungChiUSD
	when HD.iID_DuAnID is null then HD.fGiaTriUSD
	else DA.fUSD
	End as GiaTriHopDongUSD ,
Case 
	when CT.iID_HopDongID is null then CT.fNoiDungChiVND
	when HD.iID_DuAnID is null then HD.fGiaTriVND
	else DA.fVND
	End as GiaTriHopDongVND,
	BQP.fGiaTriKH_TTCP as GiaTriTongTheUSD,
	BQP.fGiaTriKH_BQP as GiaTriBQPUSD,

Case 
	when CT.iID_HopDongID is null then CT.fKinhPhiDuocCapCacNamTruoc_USD
	else QTND.KinhPhiUSD End as KinhPhiUSD ,
Case 
	when CT.iID_HopDongID is null then CT.fKinhPhiDuocCapCacNamTruoc_VND
	else QTND.KinhPhiVND End as KinhPhiVND ,

Case 
	when CT.iID_HopDongID is null then CT.fKinhPhiDaChiCacNamTruoc_USD
	else KPDC.KinhPhiDaChiUSD End as KinhPhiDaChiUSD ,
Case 
	when CT.iID_HopDongID is null then CT.fKinhPhiDaChiCacNamTruoc_VND
	else KPDCVND.KinhPhiDaChiVND End as KinhPhiDaChiVND,

Case 
	when CT.iID_HopDongID is null then CT.fKinhPhiDuocCapDenCuoiQuyTruoc_USD
	else QTNDToY.KinhPhiToYUSD End as KinhPhiToYUSD ,
Case 
	when CT.iID_HopDongID is null then CT.fKinhPhiDuocCapDenCuoiQuyTruoc_VND
	else QTNDToY.KinhPhiToYVND End as KinhPhiToYVND ,

Case 
	when CT.iID_HopDongID is null then CT.fKinhPhiDaChiDenCuoiQuyTruoc_USD
	else KPDCToY.KinhPhiDaChiUSD End as KinhPhiDaChiToYUSD ,
Case 
	when CT.iID_HopDongID is null then CT.fKinhPhiDaChiDenCuoiQuyTruoc_VND
	else KPDCVNDToY.KinhPhiDaChiVND End as KinhPhiDaChiToYVND

FROM NH_NhuCauChiQuy_ChiTiet CT
left join NH_NhuCauChiQuy NCCQ on NCCQ.ID = CT.iID_NhuCauChiQuyID  
left join NH_DA_HopDong HD on HD.ID = CT.iID_HopDongID  
left join NH_DA_DuAn DA on DA.ID = HD.iID_DuAnID 
left join DonVi dv on dv.iID_DonVi = NCCQ.iID_DonViID  
left join NH_KHTongThe_NhiemVuChi BQP on BQP.ID = HD.iID_KHTongThe_NhiemVuChiID  
left join NH_DM_NhiemVuChi DMBQP on DMBQP.ID = BQP.iID_NhiemVuChiID  
left join (
		Select NDCT.iID_HopDongID, Sum(NDCT.fQTKinhPhiDuocCap_TongSo_USD) as KinhPhiUSD, sum(NDCT.fQTKinhPhiDuocCap_TongSo_VND) as KinhPhiVND
		from NH_QT_QuyetToanNienDo_ChiTiet NDCT group by NDCT.iID_HopDongID)
	QTND on QTND.iID_HopDongID = HD.ID
left join (
		select ThanhToan.iLoaiDeNghi , ThanhToan.iID_HopDongID , sum(ThanhToan.fChuyenKhoan_BangSo) as KinhPhiDaChiUSD from NH_TT_ThanhToan ThanhToan
		join NH_TT_ThanhToan_ChiTiet b 
		on ThanhToan.ID = b.iID_DeNghiThanhToanID 
		where ThanhToan.iLoaiNoiDungChi = 1 and ThanhToan.iNamKeHoach = DATEPART(YEAR,GETDATE()) and ThanhToan.dNgayDeNghi < convert(datetime,(concat(year(GETDATE())-1,'-12-31 00:00:00.000')))
		Group BY ThanhToan.iLoaiDeNghi, ThanhToan.iID_HopDongID having ThanhToan.iLoaiDeNghi = 2 or ThanhToan.iLoaiDeNghi = 3
	) KPDC on KPDC.iID_HopDongID = HD.ID
left join (
		select ThanhToan.iLoaiDeNghi , ThanhToan.iID_HopDongID , sum(ThanhToan.fChuyenKhoan_BangSo) as KinhPhiDaChiVND from NH_TT_ThanhToan ThanhToan
		join NH_TT_ThanhToan_ChiTiet b 
		on ThanhToan.ID = b.iID_DeNghiThanhToanID 
		where ThanhToan.iLoaiNoiDungChi = 2 and ThanhToan.iNamKeHoach = DATEPART(YEAR,GETDATE()) and ThanhToan.dNgayDeNghi < convert(datetime,(concat(year(GETDATE())-1,'-12-31 00:00:00.000')))
		Group BY ThanhToan.iLoaiDeNghi, ThanhToan.iID_HopDongID having ThanhToan.iLoaiDeNghi = 2 or ThanhToan.iLoaiDeNghi = 3
	) KPDCVND on KPDCVND.iID_HopDongID = HD.ID
left join (
	Select NDCT.iID_HopDongID, Sum(NDCT.fQTKinhPhiDuocCap_TongSo_USD) as KinhPhiToYUSD, sum(NDCT.fQTKinhPhiDuocCap_TongSo_VND) as KinhPhiToYVND
		from NH_QT_QuyetToanNienDo_ChiTiet NDCT
		left join NH_QT_QuyetToanNienDo NDCTParent on NDCTParent.ID = NDCT.iID_QuyetToanNienDoID
		Where NDCTParent.iNamKeHoach = DATEPART(YEAR,GETDATE())
		group by NDCT.iID_HopDongID) QTNDToY on QTNDToY.iID_HopDongID = HD.ID
left join (
		select ThanhToan.iLoaiDeNghi , ThanhToan.iID_HopDongID , sum(ThanhToan.fChuyenKhoan_BangSo) as KinhPhiDaChiUSD from NH_TT_ThanhToan ThanhToan
		join NH_TT_ThanhToan_ChiTiet b 
		on ThanhToan.ID = b.iID_DeNghiThanhToanID 
		where ThanhToan.iLoaiNoiDungChi = 1 and ThanhToan.dNgayDeNghi > convert(datetime,(concat(year(GETDATE()),'-1-1 00:00:00.000'))) and 
			((ThanhToan.dNgayDeNghi < convert(datetime,(concat(year(GETDATE()),'-9-30 00:00:00.000'))) and DATEPART(quarter,GETDATE()) = 4)
			or (ThanhToan.dNgayDeNghi < convert(datetime,(concat(year(GETDATE()),'-6-30 00:00:00.000'))) and DATEPART(quarter,GETDATE()) = 3)
			or (ThanhToan.dNgayDeNghi < convert(datetime,(concat(year(GETDATE()),'-3-31 00:00:00.000'))) and DATEPART(quarter,GETDATE()) = 2)
			)
		Group BY ThanhToan.iLoaiDeNghi, ThanhToan.iID_HopDongID having ThanhToan.iLoaiDeNghi = 2 or ThanhToan.iLoaiDeNghi = 3
	) KPDCToY on KPDCToY.iID_HopDongID = HD.ID
left join (
		select ThanhToan.iLoaiDeNghi , ThanhToan.iID_HopDongID , sum(ThanhToan.fChuyenKhoan_BangSo) as KinhPhiDaChiVND from NH_TT_ThanhToan ThanhToan
		join NH_TT_ThanhToan_ChiTiet b 
		on ThanhToan.ID = b.iID_DeNghiThanhToanID 
		where ThanhToan.iLoaiNoiDungChi = 2 and ThanhToan.dNgayDeNghi > convert(datetime,(concat(year(GETDATE()),'-1-1 00:00:00.000'))) and 
			((ThanhToan.dNgayDeNghi < convert(datetime,(concat(year(GETDATE()),'-9-30 00:00:00.000'))) and DATEPART(quarter,GETDATE()) = 4)
			or (ThanhToan.dNgayDeNghi < convert(datetime,(concat(year(GETDATE()),'-6-30 00:00:00.000'))) and DATEPART(quarter,GETDATE()) = 3)
			or (ThanhToan.dNgayDeNghi < convert(datetime,(concat(year(GETDATE()),'-3-31 00:00:00.000'))) and DATEPART(quarter,GETDATE()) = 2)
			)
		Group BY ThanhToan.iLoaiDeNghi, ThanhToan.iID_HopDongID having ThanhToan.iLoaiDeNghi = 2 or ThanhToan.iLoaiDeNghi = 3
	) KPDCVNDToY on KPDCVNDToY.iID_HopDongID = HD.ID
	WHERE CT.iID_NhuCauChiQuyID = @idNhuCauChiQuy
ORDER BY ID_NhiemVuChi desc,sTenNhiemVuChi, sTenDonvi , iID_NhuCauChiQuyID;
		
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_chenhlechtigia_index]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_chenhlechtigia_index] 
@iDDonVi uniqueidentifier,
@iDChuongTrinh uniqueidentifier,
@iDHopDong uniqueidentifier
AS
BEGIN
	Create table #Temp
	(
		ID uniqueidentifier,
		sTen nvarchar(MAX),
		fTienKHTTBQPCapUSD float,
		fTienKHTTBQPCapVND float,
		fTienTheoHopDongUSD float,
		fTienTheoHopDongVND float,
		fKinhPhiDuocCapChoCDTUSD float,
		fKinhPhiDuocCapChoCDTVND float,
		fKinhPhiDaThanhToanUSD float,
		fKinhPhiDaThanhToanVND float,
		fTiGiaCLHopDongVsCDTUSD float,
		fTiGiaCLHopDongVsCDTVND float,
		fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD float,
		fTiGiaCLKinhPhiDuocCapVsGiaiNganVND float,
		IDParent uniqueidentifier,
		IsHangCha bit
	)	
	
	-- Insert đề nghị thanh toán
	insert into #Temp (ID,sTen,fTienKHTTBQPCapUSD,fTienKHTTBQPCapVND,fTienTheoHopDongUSD,fTienTheoHopDongVND,fKinhPhiDuocCapChoCDTUSD,fKinhPhiDuocCapChoCDTVND
			,fKinhPhiDaThanhToanUSD,fKinhPhiDaThanhToanVND,fTiGiaCLHopDongVsCDTUSD,fTiGiaCLHopDongVsCDTVND,fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,fTiGiaCLKinhPhiDuocCapVsGiaiNganVND
			,IDParent,IsHangCha)
	select ID, sTen,fTienKHTTBQPCapUSD,fTienKHTTBQPCapVND,fTienTheoHopDongUSD,fTienTheoHopDongVND,fKinhPhiDuocCapChoCDTUSD,fKinhPhiDuocCapChoCDTVND
			,fKinhPhiDaThanhToanUSD,fKinhPhiDaThanhToanVND,fTiGiaCLHopDongVsCDTUSD,fTiGiaCLHopDongVsCDTVND,fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,fTiGiaCLKinhPhiDuocCapVsGiaiNganVND
			,IDParent,IsHangCha
	from(
		SELECT 
		NEWID() AS ID,
		(tt.sSoDeNghi +
			' - ' + (case when tt.iLoaiNoiDungChi = 1 then N'Chi ngoại tệ' else N'Chi trong nước' end) + 
			' - ' + (case when tt.iLoaiDeNghi = 1 then N'Cấp kinh phí'
						when tt.iLoaiDeNghi = 2 then N'Tạm ứng'
						else N'Thanh toán' end)
		) as sTen,
		NULL AS fTienKHTTBQPCapUSD,
		NULL AS fTienKHTTBQPCapVND,
		NULL AS fTienTheoHopDongUSD,
		NULL AS fTienTheoHopDongVND,
		(case when tt.iCoQuanThanhToan = 1 then chitiet.fKinhPhiDuocCapChoCDTUSD 
			else (case when tt.iLoaiDeNghi = 1 then chitiet.fKinhPhiDuocCapChoCDTUSD else null end) end)
		 AS fKinhPhiDuocCapChoCDTUSD,
		(case when tt.iCoQuanThanhToan = 1 then chitiet.fKinhPhiDuocCapChoCDTVND 
			else (case when tt.iLoaiDeNghi = 1 then chitiet.fKinhPhiDuocCapChoCDTVND else null end) end)
		AS fKinhPhiDuocCapChoCDTVND,
		(case when tt.iCoQuanThanhToan = 1 then chitiet.fKinhPhiDaThanhToanUSD 
			else (case when tt.iLoaiDeNghi = 2 or tt.iLoaiDeNghi = 3 then chitiet.fKinhPhiDaThanhToanUSD else null end) end)
		AS fKinhPhiDaThanhToanUSD,
		(case when tt.iCoQuanThanhToan = 1 then chitiet.fKinhPhiDaThanhToanVND 
			else (case when tt.iLoaiDeNghi = 2 or tt.iLoaiDeNghi = 3 then chitiet.fKinhPhiDaThanhToanVND else null end) end)
		AS fKinhPhiDaThanhToanVND,
		NULL AS fTiGiaCLHopDongVsCDTUSD,
		NULL AS fTiGiaCLHopDongVsCDTVND,
		NULL AS fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,
		NULL AS fTiGiaCLKinhPhiDuocCapVsGiaiNganVND,
		(case when tt.iID_HopDongID is null or tt.iID_HopDongID = CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER) then tt.iID_KHTongTheID else tt.iID_HopDongID end) as IDParent,
		CAST(0 AS bit) AS IsHangCha
		FROM NH_TT_ThanhToan AS tt 
		INNER JOIN (
			SELECT 
			Sum(ISNULL(fPheDuyetCapKyNay_USD, 0)) AS fKinhPhiDuocCapChoCDTUSD, 
			Sum(ISNULL(fPheDuyetCapKyNay_VND, 0)) AS fKinhPhiDuocCapChoCDTVND, 
			Sum(ISNULL(fPheDuyetCapKyNay_USD, 0)) AS fKinhPhiDaThanhToanUSD,
			Sum(ISNULL(fPheDuyetCapKyNay_VND, 0)) AS fKinhPhiDaThanhToanVND,
			tt_ct.iID_DeNghiThanhToanID
			FROM NH_TT_ThanhToan_ChiTiet AS tt_ct
			GROUP BY tt_ct.iID_DeNghiThanhToanID
		) AS chitiet ON chitiet.iID_DeNghiThanhToanID = tt.ID
		WHERE (@iDHopDong IS NULL OR (tt.iID_HopDongID IS NOT NULL AND tt.iID_HopDongID = @iDHopDong))
	) AS A

	-- Insert hợp đồng
	insert into #Temp (ID,sTen,fTienKHTTBQPCapUSD,fTienKHTTBQPCapVND,fTienTheoHopDongUSD,fTienTheoHopDongVND,fKinhPhiDuocCapChoCDTUSD,fKinhPhiDuocCapChoCDTVND
			,fKinhPhiDaThanhToanUSD,fKinhPhiDaThanhToanVND,fTiGiaCLHopDongVsCDTUSD,fTiGiaCLHopDongVsCDTVND,fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,fTiGiaCLKinhPhiDuocCapVsGiaiNganVND
			,IDParent,IsHangCha)
	select ID, sTen,fTienKHTTBQPCapUSD,fTienKHTTBQPCapVND,fTienTheoHopDongUSD,fTienTheoHopDongVND,fKinhPhiDuocCapChoCDTUSD,fKinhPhiDuocCapChoCDTVND
			,fKinhPhiDaThanhToanUSD,fKinhPhiDaThanhToanVND,fTiGiaCLHopDongVsCDTUSD,fTiGiaCLHopDongVsCDTVND,fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,fTiGiaCLKinhPhiDuocCapVsGiaiNganVND
			,IDParent,IsHangCha
	from(
		SELECT 
		hd.ID,
		hd.sTenHopDong as sTen,
		NULL AS fTienKHTTBQPCapUSD,
		NULL AS fTienKHTTBQPCapVND,
		hd.fGiaTriUSD AS fTienTheoHopDongUSD,
		hd.fGiaTriVND AS fTienTheoHopDongVND,
		SUM(ISNULL(temp.fKinhPhiDuocCapChoCDTUSD, 0)) AS fKinhPhiDuocCapChoCDTUSD,
		SUM(ISNULL(temp.fKinhPhiDuocCapChoCDTVND, 0)) AS fKinhPhiDuocCapChoCDTVND,
		SUM(ISNULL(temp.fKinhPhiDaThanhToanUSD, 0)) AS fKinhPhiDaThanhToanUSD,
		SUM(ISNULL(temp.fKinhPhiDaThanhToanVND, 0)) AS fKinhPhiDaThanhToanVND,
		ISNULL(hd.fGiaTriUSD, 0) - SUM(ISNULL(temp.fKinhPhiDuocCapChoCDTUSD, 0)) AS fTiGiaCLHopDongVsCDTUSD,
		ISNULL(hd.fGiaTriVND, 0) - SUM(ISNULL(temp.fKinhPhiDuocCapChoCDTVND, 0)) AS fTiGiaCLHopDongVsCDTVND,
		SUM(ISNULL(temp.fKinhPhiDuocCapChoCDTUSD, 0)) - SUM(ISNULL(temp.fKinhPhiDaThanhToanUSD, 0)) AS fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,
		SUM(ISNULL(temp.fKinhPhiDuocCapChoCDTVND, 0)) - SUM(ISNULL(temp.fKinhPhiDaThanhToanVND, 0)) AS fTiGiaCLKinhPhiDuocCapVsGiaiNganVND,
		hd.iID_KHTongThe_NhiemVuChiID AS IDParent,
		CAST(1 AS bit) AS IsHangCha
		FROM NH_DA_HopDong AS hd
		INNER JOIN #Temp as temp on hd.ID = temp.IDParent
		WHERE (@iDHopDong IS NULL OR hd.ID = @iDHopDong) AND (@iDChuongTrinh IS NULL OR hd.iID_KHTongThe_NhiemVuChiID = @iDChuongTrinh)
		GROUP BY hd.ID, hd.sTenHopDong, hd.fGiaTriUSD, hd.fGiaTriVND, hd.iID_KHTongThe_NhiemVuChiID
	) AS B

	-- Insert chương trình
	insert into #Temp (ID,sTen,fTienKHTTBQPCapUSD,fTienKHTTBQPCapVND,fTienTheoHopDongUSD,fTienTheoHopDongVND,fKinhPhiDuocCapChoCDTUSD,fKinhPhiDuocCapChoCDTVND
			,fKinhPhiDaThanhToanUSD,fKinhPhiDaThanhToanVND,fTiGiaCLHopDongVsCDTUSD,fTiGiaCLHopDongVsCDTVND,fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,fTiGiaCLKinhPhiDuocCapVsGiaiNganVND
			,IDParent,IsHangCha)
	select ID, sTen,fTienKHTTBQPCapUSD,fTienKHTTBQPCapVND,fTienTheoHopDongUSD,fTienTheoHopDongVND,fKinhPhiDuocCapChoCDTUSD,fKinhPhiDuocCapChoCDTVND
			,fKinhPhiDaThanhToanUSD,fKinhPhiDaThanhToanVND,fTiGiaCLHopDongVsCDTUSD,fTiGiaCLHopDongVsCDTVND,fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,fTiGiaCLKinhPhiDuocCapVsGiaiNganVND
			,IDParent,IsHangCha
	from(
		SELECT
		nvc.ID,
		dmnvc.sTenNhiemVuChi AS sTen,
		nvc.fGiaTriKH_BQP AS fTienKHTTBQPCapUSD,
		nvc.fGiaTriKH_BQP_VND AS fTienKHTTBQPCapVND,
		NULL AS fTienTheoHopDongUSD,
		NULL AS fTienTheoHopDongVND,
		SUM(ISNULL(temp.fKinhPhiDuocCapChoCDTUSD, 0)) AS fKinhPhiDuocCapChoCDTUSD,
		SUM(ISNULL(temp.fKinhPhiDuocCapChoCDTVND, 0)) AS fKinhPhiDuocCapChoCDTVND,
		SUM(ISNULL(temp.fKinhPhiDaThanhToanUSD, 0)) AS fKinhPhiDaThanhToanUSD,
		SUM(ISNULL(temp.fKinhPhiDaThanhToanVND, 0)) AS fKinhPhiDaThanhToanVND,
		NULL AS fTiGiaCLHopDongVsCDTUSD,
		NULL AS fTiGiaCLHopDongVsCDTVND,
		SUM(ISNULL(temp.fKinhPhiDuocCapChoCDTUSD, 0)) - SUM(ISNULL(temp.fKinhPhiDaThanhToanUSD, 0)) AS fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,
		SUM(ISNULL(temp.fKinhPhiDuocCapChoCDTVND, 0)) - SUM(ISNULL(temp.fKinhPhiDaThanhToanVND, 0)) AS fTiGiaCLKinhPhiDuocCapVsGiaiNganVND,
		nvc.iID_DonViThuHuongID AS IDParent,
		CAST(1 AS bit) AS IsHangCha
		FROM NH_KHTongThe_NhiemVuChi AS nvc
		LEFT JOIN NH_DM_NhiemVuChi AS dmnvc ON dmnvc.ID = nvc.iID_NhiemVuChiID
		INNER JOIN #Temp AS temp ON nvc.ID = temp.IDParent
		WHERE (@iDDonVi IS NULL OR nvc.iID_DonViThuHuongID = @iDDonVi) AND (@iDChuongTrinh IS NULL OR nvc.ID = @iDChuongTrinh)
		GROUP BY nvc.ID,dmnvc.sTenNhiemVuChi,nvc.fGiaTriKH_BQP,nvc.fGiaTriKH_BQP_VND,nvc.iID_DonViThuHuongID
	) AS C

	-- Insert đơn vị
	insert into #Temp (ID,sTen,fTienKHTTBQPCapUSD,fTienKHTTBQPCapVND,fTienTheoHopDongUSD,fTienTheoHopDongVND,fKinhPhiDuocCapChoCDTUSD,fKinhPhiDuocCapChoCDTVND
			,fKinhPhiDaThanhToanUSD,fKinhPhiDaThanhToanVND,fTiGiaCLHopDongVsCDTUSD,fTiGiaCLHopDongVsCDTVND,fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,fTiGiaCLKinhPhiDuocCapVsGiaiNganVND
			,IDParent,IsHangCha)
	select ID, sTen,fTienKHTTBQPCapUSD,fTienKHTTBQPCapVND,fTienTheoHopDongUSD,fTienTheoHopDongVND,fKinhPhiDuocCapChoCDTUSD,fKinhPhiDuocCapChoCDTVND
			,fKinhPhiDaThanhToanUSD,fKinhPhiDaThanhToanVND,fTiGiaCLHopDongVsCDTUSD,fTiGiaCLHopDongVsCDTVND,fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,fTiGiaCLKinhPhiDuocCapVsGiaiNganVND
			,IDParent,IsHangCha
	from(
		SELECT DISTINCT
		dv.iID_DonVi AS ID,
		dv.sTenDonVi AS sTen,
		NULL AS fTienKHTTBQPCapUSD,
		NULL AS fTienKHTTBQPCapVND,
		NULL AS fTienTheoHopDongUSD,
		NULL AS fTienTheoHopDongVND,
		NULL AS fKinhPhiDuocCapChoCDTUSD,
		NULL AS fKinhPhiDuocCapChoCDTVND,
		NULL AS fKinhPhiDaThanhToanUSD,
		NULL AS fKinhPhiDaThanhToanVND,
		NULL AS fTiGiaCLHopDongVsCDTUSD,
		NULL AS fTiGiaCLHopDongVsCDTVND,
		NULL AS fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,
		NULL AS fTiGiaCLKinhPhiDuocCapVsGiaiNganVND,
		NULL AS IDParent,
		CAST(1 AS bit) AS IsHangCha
		FROM DonVi AS dv
		INNER JOIN #Temp as temp on dv.iID_DonVi = temp.IDParent
		WHERE (@iDDonVi IS NULL OR dv.iID_DonVi = @iDDonVi)
	) AS D
		
	;WITH  #Tree(ID,sTen,fTienKHTTBQPCapUSD,fTienKHTTBQPCapVND,fTienTheoHopDongUSD,fTienTheoHopDongVND,fKinhPhiDuocCapChoCDTUSD,fKinhPhiDuocCapChoCDTVND
			,fKinhPhiDaThanhToanUSD,fKinhPhiDaThanhToanVND,fTiGiaCLHopDongVsCDTUSD,fTiGiaCLHopDongVsCDTVND,fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,fTiGiaCLKinhPhiDuocCapVsGiaiNganVND
			,IDParent,IsHangCha,position)
	AS (
		select temp.ID,temp.sTen,
			temp.fTienKHTTBQPCapUSD,temp.fTienKHTTBQPCapVND,
			temp.fTienTheoHopDongUSD,temp.fTienTheoHopDongVND,
			temp.fKinhPhiDuocCapChoCDTUSD,temp.fKinhPhiDuocCapChoCDTVND,
			temp.fKinhPhiDaThanhToanUSD,temp.fKinhPhiDaThanhToanVND,
			temp.fTiGiaCLHopDongVsCDTUSD,temp.fTiGiaCLHopDongVsCDTVND,
			temp.fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,temp.fTiGiaCLKinhPhiDuocCapVsGiaiNganVND,
			temp.IDParent,temp.IsHangCha,
			CAST(ROW_NUMBER() OVER(ORDER BY temp.sTen) AS NVARCHAR(MAX)) AS position
		from #Temp as temp
		where temp.IDParent is null
		UNION ALL
		select
			child.ID,child.sTen,
			child.fTienKHTTBQPCapUSD,child.fTienKHTTBQPCapVND,
			child.fTienTheoHopDongUSD,child.fTienTheoHopDongVND,
			child.fKinhPhiDuocCapChoCDTUSD,child.fKinhPhiDuocCapChoCDTVND,
			child.fKinhPhiDaThanhToanUSD,child.fKinhPhiDaThanhToanVND,
			child.fTiGiaCLHopDongVsCDTUSD,child.fTiGiaCLHopDongVsCDTVND,
			child.fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,child.fTiGiaCLKinhPhiDuocCapVsGiaiNganVND,
			child.IDParent,child.IsHangCha,
			CONCAT(parent.position,'.',CAST(ROW_NUMBER() OVER(ORDER BY child.sTen) AS NVARCHAR(MAX))) AS position
		from #Temp as child 
		inner join #Tree as parent on parent.ID = child.IDParent
	)
	SELECT * INTO #Data FROM #Tree;
	
	SELECT position,sTen,cast('/' + replace(position, '.', '/') + '/' as hierarchyid) AS sort,
		fTienKHTTBQPCapUSD,fTienKHTTBQPCapVND,
		fTienTheoHopDongUSD,fTienTheoHopDongVND,
		fKinhPhiDuocCapChoCDTUSD,fKinhPhiDuocCapChoCDTVND,
		fKinhPhiDaThanhToanUSD,fKinhPhiDaThanhToanVND,
		fTiGiaCLHopDongVsCDTUSD,fTiGiaCLHopDongVsCDTVND,
		fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,fTiGiaCLKinhPhiDuocCapVsGiaiNganVND,
		IsHangCha
	FROM #Data dt
	ORDER BY sort;

	DROP TABLE #Temp;
	DROP TABLE #Data;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_duan_find_from_chutruong_dautu]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_duan_find_from_chutruong_dautu]
	@YearOfWork INT, 
	@MaDonVi NVARCHAR(50),
	@ChuTruongDauTuId UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @DuAnId UNIQUEIDENTIFIER;
	SELECT @DuAnId = iID_DuAnID FROM NH_DA_ChuTruongDauTu WHERE ID = @ChuTruongDauTuId;

    SELECT
		duAn.ID AS Id,
		duAn.iID_KHTT_NhiemVuChiID AS IIdKhttNhiemVuChiId,
		duAn.sMaDuAn AS SMaDuAn,
		duAn.sTenDuAn AS STenDuAn,
		duAn.iID_DonViQuanLyID AS IIdDonViQuanLyId,
		duAn.iID_ChuDauTuID AS IIdChuDauTuId,
		duAn.iID_MaChuDauTu AS IIdMaChuDauTu,
		duAn.iID_MaDonViQuanLy AS IIdMaDonViQuanLy,
		duAn.iID_CapPheDuyetID AS IIdCapPheDuyetId,
		duAn.sKhoiCong AS SKhoiCong,
		duAn.sKetThuc AS SKetThuc,
		duAn.bIsDuPhong AS BIsDuPhong,
		duAn.sDiaDiem AS SDiaDiem,
		duAn.sMucTieu AS SMucTieu,
		duAn.sQuyMo AS SQuyMo,
		duAn.iID_TiGiaUSD_EURID AS IIdTiGiaUsdEurid,
		duAn.iID_TiGiaUSD_VNDID AS IIdTiGiaUsdVndid,
		duAn.iID_TiGiaUSD_NgoaiTeKhacID AS IIdTiGiaUsdNgoaiTeKhacId,
		duAn.fUSD AS FUsd,
		duAn.fNgoaiTeKhac AS FNgoaiTeKhac,
		duAn.fVND AS FVnd,
		duAn.fEUR AS FEur,
		duAn.dNgayTao AS DNgayTao,
		duAn.sNguoiTao AS SNguoiTao,
		duAn.dNgaySua AS DNgaySua,
		duAn.sNguoiSua AS SNguoiSua,
		duAn.dNgayXoa AS DNgayXoa,
		duAn.sNguoiXoa AS SNguoiXoa,
		duAn.iID_ChuTruongDauTuID AS IIdChuTruongDauTuId,
		duAn.iID_TiGiaID AS IIdTiGiaId,
		duAn.sMaNgoaiTeKhac AS SMaNgoaiTeKhac,
		duAn.iLoai AS ILoai, --KhaiPD update 18/11/2022
		NULL AS STenDonVi,
		NULL AS STenPheDuyet,
		NULL AS STenChuDauTu
	FROM NH_DA_DuAn duAn
	WHERE
		1=1
		AND duAn.iID_MaDonViQuanLy = @MaDonVi
		AND duAn.ID NOT IN (SELECT DISTINCT(iID_DuAnID) FROM NH_DA_ChuTruongDauTu WHERE iID_DuAnID IS NOT NULL AND (@ChuTruongDauTuId IS NULL OR iID_DuAnID <> @DuAnId)) -- Lấy dự án chưa có quyết định đầu tư
END
;

GO
/****** Object:  StoredProcedure [dbo].[sp_nh_duan_find_from_dutoan]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_duan_find_from_dutoan]
	@YearOfWork INT, 
	@MaDonVi NVARCHAR(50),
	@DuToanId UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

    SELECT
		duAn.ID AS Id,
		duAn.iID_KHTT_NhiemVuChiID AS IIdKhttNhiemVuChiId,
		duAn.sMaDuAn AS SMaDuAn,
		duAn.sTenDuAn AS STenDuAn,
		duAn.iID_DonViQuanLyID AS IIdDonViQuanLyId,
		duAn.iID_ChuDauTuID AS IIdChuDauTuId,
		duAn.iID_MaChuDauTu AS IIdMaChuDauTu,
		duAn.iID_MaDonViQuanLy AS IIdMaDonViQuanLy,
		duAn.iID_CapPheDuyetID AS IIdCapPheDuyetId,
		duAn.sKhoiCong AS SKhoiCong,
		duAn.sKetThuc AS SKetThuc,
		duAn.bIsDuPhong AS BIsDuPhong,
		duAn.sDiaDiem AS SDiaDiem,
		duAn.sMucTieu AS SMucTieu,
		duAn.sQuyMo AS SQuyMo,
		duAn.iID_TiGiaUSD_EURID AS IIdTiGiaUsdEurid,
		duAn.iID_TiGiaUSD_VNDID AS IIdTiGiaUsdVndid,
		duAn.iID_TiGiaUSD_NgoaiTeKhacID AS IIdTiGiaUsdNgoaiTeKhacId,
		duAn.fUSD AS FUsd,
		duAn.fNgoaiTeKhac AS FNgoaiTeKhac,
		duAn.fVND AS FVnd,
		duAn.fEUR AS FEur,
		duAn.dNgayTao AS DNgayTao,
		duAn.sNguoiTao AS SNguoiTao,
		duAn.dNgaySua AS DNgaySua,
		duAn.sNguoiSua AS SNguoiSua,
		duAn.dNgayXoa AS DNgayXoa,
		duAn.sNguoiXoa AS SNguoiXoa,
		duAn.iID_ChuTruongDauTuID AS IIdChuTruongDauTuId,
		duAn.iID_TiGiaID AS IIdTiGiaId,
		duAn.sMaNgoaiTeKhac AS SMaNgoaiTeKhac,
		duAn.iLoai AS ILoai,
		NULL AS STenDonVi,
		NULL AS STenPheDuyet,
		NULL AS STenChuDauTu
	FROM NH_DA_DuAn duAn
	WHERE
		1=1
		AND duAn.iID_MaDonViQuanLy = @MaDonVi
		AND duAn.ID IN (SELECT iID_DuAnID FROM NH_DA_QDDauTu) -- Lấy dự án đã có chủ trương đầu tư
		--AND duAn.ID NOT IN (SELECT DISTINCT(iID_DuAnID) FROM NH_DA_DuToan WHERE iID_DuAnID IS NOT NULL AND (@DuToanId IS NULL OR ID <> @DuToanId)) -- Lấy dự án chưa có quyết định đầu tư
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_duan_find_from_qddautu]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_duan_find_from_qddautu]
	@YearOfWork INT, 
	@MaDonVi NVARCHAR(50),
	@ILoai INT,
	@QdDauTuId UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @DuAnId UNIQUEIDENTIFIER;
	SELECT @DuAnId = iID_DuAnID FROM NH_DA_QDDauTu WHERE ID = @QdDauTuId;

    SELECT
		duAn.ID AS Id,
		duAn.iID_KHTT_NhiemVuChiID AS IIdKhttNhiemVuChiId,
		duAn.sMaDuAn AS SMaDuAn,
		duAn.sTenDuAn AS STenDuAn,
		duAn.iID_DonViQuanLyID AS IIdDonViQuanLyId,
		duAn.iID_ChuDauTuID AS IIdChuDauTuId,
		duAn.iID_MaChuDauTu AS IIdMaChuDauTu,
		duAn.iID_MaDonViQuanLy AS IIdMaDonViQuanLy,
		duAn.iID_CapPheDuyetID AS IIdCapPheDuyetId,
		duAn.sKhoiCong AS SKhoiCong,
		duAn.sKetThuc AS SKetThuc,
		duAn.bIsDuPhong AS BIsDuPhong,
		duAn.sDiaDiem AS SDiaDiem,
		duAn.sMucTieu AS SMucTieu,
		duAn.sQuyMo AS SQuyMo,
		duAn.iID_TiGiaUSD_EURID AS IIdTiGiaUsdEurid,
		duAn.iID_TiGiaUSD_VNDID AS IIdTiGiaUsdVndid,
		duAn.iID_TiGiaUSD_NgoaiTeKhacID AS IIdTiGiaUsdNgoaiTeKhacId,
		duAn.fUSD AS FUsd,
		duAn.fNgoaiTeKhac AS FNgoaiTeKhac,
		duAn.fVND AS FVnd,
		duAn.fEUR AS FEur,
		duAn.dNgayTao AS DNgayTao,
		duAn.sNguoiTao AS SNguoiTao,
		duAn.dNgaySua AS DNgaySua,
		duAn.sNguoiSua AS SNguoiSua,
		duAn.dNgayXoa AS DNgayXoa,
		duAn.sNguoiXoa AS SNguoiXoa,
		duAn.iID_ChuTruongDauTuID AS IIdChuTruongDauTuId,
		duAn.iID_TiGiaID AS IIdTiGiaId,
		duAn.sMaNgoaiTeKhac AS SMaNgoaiTeKhac,
		duAn.iLoai AS ILoai, --KhaiPD update 18/11
		NULL AS STenDonVi,
		NULL AS STenPheDuyet,
		NULL AS STenChuDauTu
	FROM NH_DA_DuAn duAn
	WHERE
		1=1
		AND duAn.iID_MaDonViQuanLy = @MaDonVi
		AND duAn.ID IN (SELECT iID_DuAnID FROM NH_DA_ChuTruongDauTu) -- Lấy dự án đã có chủ trương đầu tư
		AND duAn.ID NOT IN (SELECT DISTINCT(iID_DuAnID) FROM NH_DA_QDDauTu WHERE iID_DuAnID IS NOT NULL AND ILoai = @ILoai AND (@QdDauTuId IS NULL OR iID_DuAnID <> @DuAnId)) -- Lấy dự án chưa có quyết định đầu tư
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_dutoan_in_khlcnt]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_dutoan_in_khlcnt]
@iId uniqueidentifier,
@iIdDuAnId uniqueidentifier
AS
BEGIN
	 DECLARE @iExist int = (SELECT COUNT(*) FROM NH_DA_KHLCNhaThau WHERE Id = @iId)
	 IF(@iExist = 0)
	 BEGIN
		SELECT tbl.*, tbl.iLoaiDuToan as IdLoaiDuToan
		FROM NH_DA_DuToan as tbl
		LEFT JOIN (SELECT DISTINCT iID_DuToanID FROM NH_DA_KHLCNhaThau WHERE bIsActive = 1 AND iID_DuToanID IS NOT NULL) as dt on tbl.ID = dt.iID_DuToanID
		WHERE tbl.iID_DuAnID = @iIdDuAnId AND dt.iID_DuToanID IS NULL
	 END
	 ELSE
	 BEGIN
		SELECT dt.*
		FROM NH_DA_KHLCNhaThau as tbl
		INNER JOIN NH_DA_DuToan as dt on tbl.iID_DuToanID = dt.ID
	 END
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_dutoan_index]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_dutoan_index] 
	@YearOfWork int,
	@ILoai int
AS
BEGIN
	
	WITH SoLieuDieuChinh AS (
		SELECT 
			ct.ID,
			ct.iID_ParentId
		FROM NH_DA_DuToan ct 
		WHERE ct.iID_ParentId IS NOT NULL 
		UNION ALL 
		SELECT 
			ct.ID,
			ct.iID_ParentId
		FROM NH_DA_DuToan ct 
		JOIN SoLieuDieuChinh ctpr ON ct.iID_ParentId = ctpr.ID 
	  WHERE 
		ct.iID_ParentId IS NOT NULL
	), 
	SoLanDieuChinh AS (
		SELECT 
			sdc.Id, 
			sdc.iID_ParentId, 
			COUNT(sdc.Id) AS iSoLanDieuChinh 
		FROM SoLieuDieuChinh sdc 
		GROUP BY 
			sdc.iID_ParentId, 
			sdc.ID
	),
	ThongTinNguonVon AS (
		SELECT 
			nguonVon.iID_DuToanID AS iID_DuToanID, 
			SUM(nguonVon.fGiaTriNgoaiTeKhac) AS fGiaTriNgoaiTeKhac,
			SUM(nguonVon.fGiaTriUSD) AS fGiaTriUSD,
			SUM(nguonVon.fGiaTriVND) AS fGiaTriVND,
			SUM(nguonVon.fGiaTriEUR) AS fGiaTriEUR
		FROM NH_DA_DuToan_NguonVon nguonVon
		GROUP BY 
			nguonVon.iID_DuToanID
	)
	
	SELECT
		duToan.ID AS Id,
		duToan.iID_QDDauTuID AS IIdQdDauTuId,
		duToan.iID_DuAnID AS IIdDuAnId,
		duToan.sSoQuyetDinh AS SSoQuyetDinh,
		duToan.dNgayQuyetDinh AS DNgayQuyetDinh,
		duToan.sMoTa AS SMoTa,
		duToan.sTenChuongTrinh AS STenChuongTrinh,
		duToan.iID_TiGiaUSD_VNDID AS IIdTiGiaUsdVndId,
		duToan.iID_TiGiaUSD_EURID AS IIdTiGiaUsdEurId,
		duToan.iID_TiGiaUSD_NgoaiTeKhacID AS IIdTiGiaUsdNgoaiTeKhacId,
		duToan.fGiaTriNgoaiTeKhac AS FGiaTriNgoaiTeKhac,
		duToan.fGiaTriUSD AS FGiaTriUsd,
		duToan.fGiaTriVND AS FGiaTriVnd,
		duToan.fGiaTriEUR AS FGiaTriEur,
		duToan.dNgayTao AS DNgayTao,
		duToan.sNguoiTao AS SNguoiTao,
		duToan.dNgaySua AS DNgaySua,
		duToan.sNguoiSua AS SNguoiSua,
		duToan.dNgayXoa AS DNgayXoa,
		duToan.sNguoiXoa AS SNguoiXoa,
		duToan.bIsActive AS BIsActive,
		duToan.bIsGoc AS BIsGoc,
		duToan.bIsKhoa AS BIsKhoa,
		duToan.bIsXoa AS BIsXoa,
		duToan.iID_DuToanGocID AS IIdDuToanGocId,
		duToan.iID_TiGiaID AS IIdTiGiaId,
		duToan.sMaNgoaiTeKhac AS SMaNgoaiTeKhac,
		duToan.iID_ParentID AS IIdParentId,
		duToan.iLoaiDuToan AS IdLoaiDuToan,
		--donvi.sTenDonVi AS STenDonVi, 
		CONCAT(donvi.iID_MaDonVi, ' - ', donvi.sTenDonVi) AS STenDonVi,
		donvi.iID_MaDonVi AS IIdMaDonViQuanLy,
		CONCAT(duAn.sMaDuAn, ' - ', duAn.sTenDuAn) AS STenDuAn,
		ISNULL(soLieuDieuChinh.iSoLanDieuChinh, 0) AS ILanDieuChinh,
		duToanParent.sSoQuyetDinh AS SDieuChinhTu,
		(SELECT COUNT(Id) FROM Attachment WHERE ModuleType = 406 AND ObjectId = duToan.ID) AS TotalFiles
	FROM NH_DA_DuToan duToan	
	LEFT JOIN NH_DA_DuToan duToanParent
		ON duToan.iID_ParentID = duToanParent.ID
	LEFT JOIN DonVi donVi
		ON duToan.iID_MaDonViQuanLy = donVi.iID_MaDonVi AND donVi.iNamLamViec = @YearOfWork
	LEFT JOIN NH_DA_DuAn duAn
		ON duToan.iID_DuAnID = duAn.ID
	LEFT JOIN SoLanDieuChinh soLieuDieuChinh
		ON soLieuDieuChinh.ID = duToan.ID
	LEFT JOIN ThongTinNguonVon nguonVon
		ON nguonVon.iID_DuToanID = duToan.ID
	WHERE duToan.iLoai = @ILoai 
	ORDER BY dNgayQuyetDinh DESC, sSoQuyetDinh DESC
END;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_nh_get_data_report_tinh_hinh_thuc_hien_du_an1]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_get_data_report_tinh_hinh_thuc_hien_du_an1]
	@IdDuAn nvarchar(max),
	@NgayBatDau datetime,
	@NgayKetThuc datetime
AS
BEGIN
	DECLARE @fGiaTriDuocCapUsd float
	DECLARE @fGiaTriDuocCapVnd float
	DECLARE @fGiaTriTTTU_Usd float
	DECLARE @fGiaTriTTTU_Vnd float

    -- Tính lũy kế được cấp
	SELECT @fGiaTriDuocCapUsd = SUM(ISNULL(tbl.fGiaTriUsd, 0)), @fGiaTriDuocCapVnd = SUM(ISNULL(tbl.fGiaTriVnd, 0))
	FROM NH_TH_TongHop tbl
	WHERE tbl.iID_DuAnId = @IdDuAn AND tbl.bIsLog = 0 AND tbl.iStatus = 0
		AND (@NgayBatDau is null or tbl.dNgayDeNghi >= @NgayBatDau)
		AND (@NgayKetThuc is null or tbl.dNgayDeNghi <= @NgayKetThuc)
		AND  (tbl.sMaNguon in ('101', '102') OR (tbl.sMaDich in ('111', '112', '121', '122') AND tbl.iCoQuanThanhToan = 1))
	GROUP BY tbl.iID_DuAnId

	-- Tính tổng thanh toán + tạm ứng
	SELECT @fGiaTriTTTU_Usd = SUM(ISNULL(tbl.fGiaTriUsd, 0)), @fGiaTriTTTU_Vnd = SUM(ISNULL(tbl.fGiaTriVnd, 0)) 
	FROM NH_TH_TongHop tbl
	WHERE tbl.iID_DuAnId = @IdDuAn AND tbl.bIsLog = 0 AND tbl.iStatus = 0
		AND (@NgayBatDau is null or tbl.dNgayDeNghi >= @NgayBatDau)
		AND (@NgayKetThuc is null or tbl.dNgayDeNghi <= @NgayKetThuc)
		AND tbl.sMaDich in ('111', '112', '121', '122') 
	GROUP BY tbl.iID_DuAnId

	select tt.ID, tt.sSoDeNghi, tt.dNgayDeNghi, 
		concat(DM_ChuDauTu.iID_MaDonVi,'-',DM_ChuDauTu.sTenDonVi) as sChuDauTu, 
		nt.sTenNhaThau as TenNhaThau, 
		tt.iLoaiNoiDungChi, 
		tt.iCoQuanThanhToan, 
		tt.iLoaiDeNghi, 
		(
			select 
					distinct mlns.sXauNoiMa 
				from NS_MucLucNganSach mlns
				where 
					(mlns.iID = pdttct.iID_MucLucNganSachID or mlns.iID_MLNS = pdttct.iID_MLNS_ID)
		) as Mlns,
		tthd.id as IdHopDong,
		tthd.sSoHopDong as SoHopDong, 
		tt.fTongDeNghi_USD,
		tt.fTongDeNghi_VND,
		tt.fTongPheDuyet_BangSo_USD,
		tt.fTongPheDuyet_BangSo_VND,
		@fGiaTriDuocCapUsd as fGiaTriDuocCap_USD,
		@fGiaTriDuocCapVnd as fGiaTriDuocCap_VND,
		@fGiaTriTTTU_Usd as fGiaTriTTTU_USD,
		@fGiaTriTTTU_Vnd as fGiaTriTTTU_VND
	from NH_TT_ThanhToan tt 
	left join DM_ChuDauTu on tt.iID_ChuDauTuID  = DM_ChuDauTu.iID_DonVi
	left join NH_DA_QDDauTu  qddt on tt.iID_NhaThauID  = qddt.ID 
	left join NH_DM_NhaThau  nt on tt.iID_NhaThauID  = nt.ID 
	left join NH_TT_ThanhToan_ChiTiet pdttct on pdttct.iID_DeNghiThanhToanID = tt.ID
	left join NH_DA_HopDong tthd on tthd.ID = tt.iID_HopDongID
	where  (@IdDuAn IS NULL  OR tt.iID_DuAnID = @IdDuAn)
		AND (@NgayBatDau is null or tt.dNgayDeNghi >= @NgayBatDau)
		AND (@NgayKetThuc is null or tt.dNgayDeNghi <= @NgayKetThuc)
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_gethangmucbyidgoithau]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_gethangmucbyidgoithau]
@IdGoiThau uniqueidentifier
AS
BEGIN
	select hm.iID_GoiThau_HangMucID as Id,
	hm.iID_GoiThau_ChiPhiID as IIdGoiThauChiPhiId,
	hm.fTienGoiThau_USD as FTienGoiThauUsd,
	hm.fTienGoiThau_VND as FTienGoiThauVnd,
	hm.fTienGoiThau_EUR as FTienGoiThauEur,
	hm.fTienGoiThau_NgoaiTeKhac as FTienGoiThauNgoaiTeKhac,
	hm.sMaHangMuc,
	hm.sTenHangMuc,
	cp.sTenChiPhi,
	hm.sMaOrder,
	hm.iID_ParentID as IIdParentId
	from nh_da_goithau_hangmuc hm
	inner join nh_da_goithau_chiphi cp on hm.iID_GoiThau_ChiPhiID = cp.Id
	inner join NH_DA_GoiThau_NguonVon nv on nv.iID_GoiThau_NguonVonID = cp.iID_GoiThau_NguonVonID
	inner join nh_da_goithau gt on gt.iID_GoiThauID = nv.iID_GoiThauID
	where gt.iID_GoiThauID = @idGoiThau
	order by hm.sMaHangMuc
END
;

GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_chiphi_by_goithau]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_goithau_chiphi_by_goithau]
@iIdKhlcnt uniqueidentifier
AS
BEGIN
	SELECT cp.ID as IIdChiPhiID, cp.iID_ParentID as IIdParentID, cp.sTenChiPhi as STenChiPhi, cp.sMaOrder as SMaOrder,
			ISNULL(dt.fTienGoiThau_EUR, 0) as FGiaTriEUR, ISNULL(dt.fTienGoiThau_NgoaiTeKhac, 0) as FGiaTriNgoaiTeKhac ,
			ISNULL(dt.fTienGoiThau_USD, 0) as FGiaTriUSD, ISNULL(dt.fTienGoiThau_VND, 0) as FGiaTriVND, dt.iID_GoiThauID as IIdGoiThauID
	FROM NH_DA_GoiThau as tbl
	inner JOIN NH_DA_GoiThau_ChiPhi as dt on tbl.iID_GoiThauID = dt.iID_GoiThauID
	inner JOIN NH_DA_DuToan_ChiPhi as cp on dt.iID_DuToan_ChiPhiID = cp.ID
	WHERE tbl.iID_GoiThauID = @iIdKhlcnt
	UNION ALL
	SELECT cp.ID as IIdChiPhiID, cp.iID_ParentID as IIdParentID, cp.sTenChiPhi as STenChiPhi, cp.sMaOrder as SMaOrder,
			ISNULL(dt.fTienGoiThau_EUR, 0) as FGiaTriEUR, ISNULL(dt.fTienGoiThau_NgoaiTeKhac, 0) as FGiaTriNgoaiTeKhac ,
			ISNULL(dt.fTienGoiThau_USD, 0) as FGiaTriUSD, ISNULL(dt.fTienGoiThau_VND, 0) as FGiaTriVND, dt.iID_GoiThauID as IIdGoiThauID
	FROM NH_DA_GoiThau as tbl
	inner JOIN NH_DA_GoiThau_ChiPhi as dt on tbl.iID_GoiThauID = dt.iID_GoiThauID
	inner JOIN NH_DA_QDDauTu_ChiPhi as cp on dt.iID_QDDauTu_ChiPhiID = cp.ID
	WHERE tbl.iID_GoiThauID = @iIdKhlcnt
END
;

GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_chiphi_by_GoiThau_listall]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_goithau_chiphi_by_GoiThau_listall]
@iIdKhlcnt uniqueidentifier
AS
BEGIN
	SELECT * FROM NH_DA_GoiThau_ChiPhi cp
	Left Join NH_DA_GoiThau gt on gt.iID_GoiThauID = cp.iID_GoiThauID
	Where gt.iID_GoiThauId = @iIdKhlcnt
END
;


GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_chiphi_by_khlcnt_listall]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_goithau_chiphi_by_khlcnt_listall]
@iIdKhlcnt uniqueidentifier
AS
BEGIN
	SELECT * FROM NH_DA_GoiThau_ChiPhi cp
	Left Join NH_DA_GoiThau gt on gt.iID_GoiThauID = cp.iID_GoiThauID
	Where gt.iId_KHLCNhaThau = @iIdKhlcnt
END
;

GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_hangmuc_by_goithau]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_goithau_hangmuc_by_goithau]
@iIdKhlcnt uniqueidentifier
AS
BEGIN
	SELECT DISTINCT hm.ID as IIdHangMucID, hm.iID_ParentID as IIdParentID, hm.iID_DuToan_ChiPhiID as IIdChiPhiID, 
		hm.sMaHangMuc as SMaHangMuc, hm.sTenHangMuc as STenHangMuc, hm.sMaOrder as SMaOrder,
		dt.fTienGoiThau_EUR as FGiaTriPheDuyetEUR, dt.fTienGoiThau_NgoaiTeKhac as FGiaTriPheDuyetNgoaiTeKhac ,
		dt.fTienGoiThau_USD as FGiaTriPheDuyetUSD, dt.fTienGoiThau_VND as FGiaTriPheDuyetVND, tbl.iID_GoiThauID as IIdGoiThauID,
		CAST(0 as float) as FGiaTriNgoaiTeKhac, 
		CAST(0 as float) as FGiaTriUSD, 
		CAST(0 as float) as FGiaTriEUR, 
		CAST(0 as float) as FGiaTriVND
	FROM NH_DA_GoiThau as tbl	
	INNER JOIN NH_DA_GoiThau_ChiPhi as cp on tbl.iID_GoiThauID = cp.iID_GoiThauID
	INNER JOIN NH_DA_GoiThau_HangMuc as dt on cp.Id = dt.iID_GoiThau_ChiPhiID
	INNER JOIN NH_DA_DuToan_HangMuc as hm on dt.iID_DuToan_HangMucID = hm.ID
	WHERE tbl.iID_GoiThauID = @iIdKhlcnt
	UNION ALL
	SELECT hm.ID as IIdHangMucID, hm.iID_ParentID as IIdParentID, hm.iID_QDDauTu_ChiPhiID as IIdChiPhiID, 
		hm.sMaHangMuc as SMaHangMuc, hm.sTenHangMuc as STenHangMuc, hm.sMaOrder as SMaOrder,
		dt.fTienGoiThau_EUR as FGiaTriPheDuyetEUR, dt.fTienGoiThau_NgoaiTeKhac as FGiaTriPheDuyetNgoaiTeKhac ,
		dt.fTienGoiThau_USD as FGiaTriPheDuyetUSD, dt.fTienGoiThau_VND as FGiaTriPheDuyetVND, tbl.iID_GoiThauID as IIdGoiThauID,
		CAST(0 as float) as FGiaTriNgoaiTeKhac, 
		CAST(0 as float) as FGiaTriUSD, 
		CAST(0 as float) as FGiaTriEUR, 
		CAST(0 as float) as FGiaTriVND
	FROM NH_DA_GoiThau as tbl	
	INNER JOIN NH_DA_GoiThau_ChiPhi as cp on tbl.iID_GoiThauID = cp.iID_GoiThauID
	INNER JOIN NH_DA_GoiThau_HangMuc as dt on cp.Id = dt.iID_GoiThau_ChiPhiID
	INNER JOIN NH_DA_QDDauTu_HangMuc as hm on dt.iID_QDDauTu_HangMucID = hm.ID
	WHERE tbl.iID_GoiThauID = @iIdKhlcnt
END
;


GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_nguonvon_by_khlcnt_listall]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_goithau_nguonvon_by_khlcnt_listall]
@iIdKhlcnt uniqueidentifier
AS
BEGIN
	SELECT * FROM NH_DA_GoiThau_NguonVon cp
	Left Join NH_DA_GoiThau gt on gt.iID_GoiThauID = cp.iID_GoiThauID
	Where gt.iId_KHLCNhaThau = @iIdKhlcnt
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_trongnuoc_index]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_nh_goithau_trongnuoc_index]
	@ILoai int,
	@IThuocMenu int
as begin
	Select 
		goiThau.iID_GoiThauID as Id,
		goiThau.iID_GoiThauGocID as IIdGoiThauGocId,
		goiThau.iID_ParentID as IIdParentId,
		goiThau.iID_NhaThauID as IIdNhaThauId,
		case when goiThau.iCheckLuong is null then khlcnt.iID_DuToanID else   goiThau.iID_DuToanID  end  as IIdDuToanId,
		goiThau.iID_CacQuyetDinhID as IIdCacQuyetDinhId,
		goiThau.iID_ParentAdjustID as IIdParentAdjustId,
		goiThau.iId_KHLCNhaThau as IIdKhlcnhaThau,
		goiThau.iID_TiGiaUSD_NgoaiTeKhacID as IIdTiGiaUSDNgoaiTeKhacId,
		goiThau.iID_TiGiaUSD_VNDID as IIdTiGiaUSDVNDId,
		goiThau.iID_TiGiaUSD_EURID as IIdTiGiaUSDEURId,
		goiThau.iID_HinhThucChonNhaThauID as IIdHinhThucChonNhaThauId,
		goiThau.iID_PhuongThucDauThauID as IIdPhuongThucDauThauId,
		goiThau.iID_LoaiHopDongID as IIdLoaiHopDongId,
		--DuAn.ID as IIdDuAnId,
		DuAn.ID as IIdDuAnId,
		goiThau.sSoQuyetDinh as SSoQuyetDinh,
		case when   goiThau.iCheckLuong is null then LCNhaThau.iID_DonViQuanLyID else  goiThau.iID_DonViQuanLyID  end  as IID_DonViQuanLyID,
		case when   goiThau.iCheckLuong is null then nvc.ID else  goiThau.iID_KHTT_NhiemVuChiID  end  as IID_KHTT_NhiemVuChiID,
		case when goiThau.iCheckLuong is null then LCNhaThau.dNgayQuyetDinh else goiThau.dNgayQuyetDinh end as DNgayQuyetDinh, --KhaiPD update 13/10/2022
		goiThau.sMaGoiThau as SMaGoiThau,
		goiThau.sTenGoiThau as STenGoiThau,
		goiThau.LoaiGoiThau as LoaiGoiThau,
		goiThau.dBatDauChonNhaThau as DBatDauChonNhaThau,
		goiThau.dKetThucChonNhaThau as DKetThucChonNhaThau,
		goiThau.iThoiGianThucHien as IThoiGianThucHien,
		goiThau.fGiaGoiThauEUR as FGiaGoiThauEUR,
		goiThau.fGiaGoiThauUSD as FGiaGoiThauUSD,
		goiThau.fGiaGoiThauVND as FGiaGoiThauVND,
		goiThau.fGiaGoiThauNgoaiTeKhac as fGiaGoiThauNgoaiTeKhac,
		goiThau.bIsGoc as BIsGoc,
		goiThau.sSoKeHoachDatHang as SSoKeHoachDatHang,
		goiThau.dNgayKeHoach as DNgayKeHoach,
		goiThau.iCheckLuong as IcheckLuong,
		goiThau.bActive as BActive,
		goiThau.iLanDieuChinh as ILanDieuChinh,
		goiThau.bIsKhoa as BIsKhoa,
		goiThau.dNgayTao as DNgayTao,
		goiThau.sNguoiTao as SNguoiTao,
		goiThau.sNguoiSua as SNguoiSua,
		goiThau.dNgaySua as DNgaySua,
		goiThau.dNgayXoa as DNgayXoa,
		goiThau.sNguoiXoa as SNguoiXoa,
		 case when goiThau.iCheckLuong is null then khlcnt.iID_TiGiaID else   goiThau.iID_TiGiaID  end  as IIdTiGiaId,
		 case when goiThau.iCheckLuong is null then khlcnt.sMaNgoaiTeKhac else   goiThau.sMaNgoaiTeKhac  end  as SMaNgoaiTeKhac,
		goiThau.bIsXoa as BIsXoa,
		 concat(DonVi.iID_MaDonVi,' -', DonVi.sTenDonVi )as TenDonVi,
		nvc.sTenNhiemVuChi as STenNhiemVuChi,
		DuAn.sTenDuAn as STenDuAn,
		HinhThucChonNhaThau.sTenHinhThucChonNhaThau as STenHinhThucChonNhaThau,
		PhuongThucChonNhaThau.sTenPhuongThucChonNhaThau as STenPhuongThucChonNhaThau,
		ChuDauTu.sTenDonVi as STenChuDauTu,
		DuAn.sDiaDiem as SDiaDiem,
		QDDauTu.fGiaTriUSD as FQDDTTongPheDuyetUSD,
		QDDauTu.fGiaTriVND as FQDDTTongPheDuyetVND,
		QDDauTu.fGiaTriEUR as FQDDTTongPheDuyetEUR,
		QDDauTu.fGiaTriNgoaiTeKhac as FQDDTTongPheDuyetNgoaiTeKhac,
		LoaiHopDong.sTenLoaiHopDong as STenHopDong,
		DuToan.fGiaTriUSD as FDTTongPheDuyetUSD,
		DuToan.fGiaTriVND as FDTTongPheDuyetVND,
		DuToan.fGiaTriEUR as FDTTongPheDuyetEUR,
		DuToan.fGiaTriNgoaiTeKhac as FDTTongPheDuyetNgoaiTeKhac,
		khttnvc.ID as IIdKHTTNhiemVuChiId,  
		( SELECT COUNT ( Id ) FROM Attachment WHERE ModuleType = 405 AND ObjectId = goiThau.iID_GoiThauID ) AS TotalFiles,
		CASE
		WHEN goiThau.iID_ParentAdjustId IS NULL THEN
		'' ELSE ( SELECT TOP 1 hdpr.sMaGoiThau FROM NH_DA_GoiThau hdpr WHERE hdpr.iID_GoiThauID = goiThau.iID_ParentAdjustId ) 
		END DieuChinhTu ,
		LCNhaThau.sMoTa as SMota
		from  NH_DA_GoiThau goiThau
	left join NH_DA_KHLCNhaThau LCNhaThau
		on LCNhaThau.Id = GoiThau.iId_KHLCNhaThau
	left join NH_DA_DuAn DuAn
		on goiThau.iID_DuAnID = DuAn.ID
	left join DonVi on (LCNhaThau.iID_DonViQuanLyID = DonVi.iID_DonVi AND goiThau.iCheckLuong is null) OR (goiThau.iID_DonViQuanLyID = DonVi.iID_DonVi  AND goiThau.iCheckLuong = 1)
	left join DM_ChuDauTu ChuDauTu
		on DuAn.iID_ChuDauTuID = ChuDauTu.iID_DonVi
	left join NH_KHTongThe_NhiemVuChi khttnvc 
		on  (LCNhaThau.iID_KHTT_NhiemVuChiID = khttnvc.ID  AND goiThau.iCheckLuong is null) OR (goiThau.iID_KHTT_NhiemVuChiID = khttnvc.ID AND goiThau.iCheckLuong = 1) 
	left join NH_DM_NhiemVuChi nvc
		--on (khttnvc.iID_NhiemVuChiID = nvc.ID  AND goiThau.iCheckLuong is null) OR (goiThau.iID_KHTT_NhiemVuChiID = nvc.ID AND goiThau.iCheckLuong = 1) 
		on khttnvc.iID_NhiemVuChiID = nvc.ID
	left join NH_DA_QDDauTu QDDauTu
		on LCNhaThau.iID_QDDauTuID = QDDauTu.ID
	left join NH_DA_DuToan DuToan
		on LCNhaThau.iID_DuToanID = DuToan.ID
	left join NH_DM_HinhThucChonNhaThau HinhThucChonNhaThau
		on goiThau.iID_HinhThucChonNhaThauID = HinhThucChonNhaThau.ID 
	left join NH_DM_PhuongThucChonNhaThau PhuongThucChonNhaThau
		on goiThau.iID_PhuongThucDauThauID = PhuongThucChonNhaThau.ID 
	left join NH_DM_LoaiHopDong LoaiHopDong
		on goiThau.iID_LoaiHopDongID = LoaiHopDong.iID_LoaiHopDongID 
	left join NH_DA_KHLCNhaThau khlcnt
		on khlcnt.Id=goiThau.iId_KHLCNhaThau
	WHERE goiThau.iLoai = @ILoai and goiThau.iThuocMenu = @IThuocMenu
	 ORDER BY goiThau.dNgayTao DESC
end
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_hangmuc_bygoithauid]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_hangmuc_bygoithauid]
	@idGoiThau uniqueidentifier
	
AS BEGIN
	SELECT
		HangMuc.iID_GoiThau_HangMucID as Id,
		HangMuc.iID_GoiThau_ChiPhiID as IIdGoiThauChiPhiId,
		HangMuc.iID_QDDauTu_HangMucID as IIdQDDauTuHangMucId,
		HangMuc.iID_DuToan_HangMucID as IIdDuToanChiPhiId,
		HangMuc.fTienGoiThau_USD as FTienGoiThauUsd,
		HangMuc.fTienGoiThau_VND as FTienGoiThauVnd,
		HangMuc.fTienGoiThau_EUR as FTienGoiThauEur,
		HangMuc.fTienGoiThau_NgoaiTeKhac as FTienGoiThauNgoaiTeKhac,
		QDDT.sTenHangMuc as STenHangMucQDDT,
		DuToan.sTenHangMuc as STenHangMucDT,
		DuToan.iID_ParentID as IIdParentId,
		DuToanChiPhi.sTenChiPhi as STenChiPhiDT
	FROM NH_DA_GoiThau_HangMuc HangMuc
	LEFT JOIN NH_DA_QDDauTu_HangMuc QDDT
		ON HangMuc.iID_QDDauTu_HangMucID = QDDT.ID
	LEFT JOIN NH_DA_DuToan_HangMuc DuToan
		ON HangMuc.iID_DuToan_HangMucID = DuToan.ID
	LEFT JOIN NH_DA_GoiThau_ChiPhi ChiPhi
	    ON HangMuc.iID_GoiThau_ChiPhiID = ChiPhi.ID
	LEFT JOIN NH_DA_QDDauTu_ChiPhi QDDTChiPhi
		ON ChiPhi.iID_QDDauTu_ChiPhiID = QDDTChiPhi.ID
	LEFT JOIN NH_DA_DuToan_ChiPhi DuToanChiPhi
		ON ChiPhi.iID_DuToan_ChiPhiID = DuToanChiPhi.ID
	WHERE 
		1=1
		AND ChiPhi.iID_GoiThauID = @idGoiThau
	ORDER BY DuToanChiPhi.sTenChiPhi,
	         case 
				when DuToan.iID_ParentID is null
				then DuToan.ID 
				else    (
						select  ID 
						from    NH_DA_DuToan_HangMuc parent 
						where   parent.ID = DuToan.iID_ParentID
						) 
				end
			,case when DuToan.iID_ParentID is null then 1 end desc
			,DuToan.ID

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_kehoachtongthe_index]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--ALTER TABLE NH_KHTongThe
--ADD iGiaiDoanTu_TTCP int,
--    iGiaiDoanDen_TTCP int,
--	iGiaiDoanTu_BQP int,
--	iGiaiDoanDen_BQP int;

CREATE PROC [dbo].[sp_nh_kehoachtongthe_index]
AS BEGIN
	WITH ListAllTongThe AS (
		SELECT NEWID() AS id, NULL AS iNamKeHoach,
			NULL AS iGiaiDoanTu, NULL AS iGiaiDoanDen,
			NULL AS iGiaiDoanTu_TTCP, NULL AS iGiaiDoanDen_TTCP,
			NULL AS iGiaiDoanTu_BQP, NULL AS iGiaiDoanDen_BQP,
			NULL AS iIdParentId,
			NULL AS iIdParentAdjustId,
			NULL AS iIdGocId, t.sSoKeHoachTTCP,
			NULL AS dNgayKeHoachTTCP,
			NULL AS sMoTaChiTietKhttcp,
			SUM(t.fTongGiaTri_KHTTCP) fTongGiaTriKhttcp,
			NULL AS sSoKeHoachBQP, NULL AS dNgayKeHoachBQP,
			NULL AS sMoTaChiTietKhbqp,
			SUM(t.fTongGiaTri_KHBQP) AS fTongGiaTriKhbqp,
			SUM(t.fTongGiaTri_KHBQP_VND) AS fTongGiaTriKhbqpVnd,
			NULL AS dNgayTao, NULL AS sNguoiTao,
			NULL AS dNgaySua, NULL AS sNguoiSua,
			NULL AS dNgayXoa, NULL AS sNguoiXoa,
			NULL AS bIsActive, NULL AS bIsGoc, NULL AS bIsKhoa,
			NULL AS iLanDieuChinh, NULL AS iLoai,
			NULL AS TotalFiles,
			NULL AS DieuChinhTu,
			NULL AS IdParentTongThe
		FROM NH_KHTongThe AS t
		GROUP BY t.sSoKeHoachTTCP
		
		UNION ALL

		SELECT t.ID id, t.iNamKeHoach iNamKeHoach,
			t.iGiaiDoanTu, t.iGiaiDoanDen,
			t.iGiaiDoanTu_TTCP, t.iGiaiDoanDen_TTCP,
			t.iGiaiDoanTu_BQP, t.iGiaiDoanDen_BQP,
			t.iID_ParentID iIdParentId,
			t.iID_ParentAdjustID iIdParentAdjustId,
			t.iID_GocID iIdGocId, t.sSoKeHoachTTCP,
			t.dNgayKeHoachTTCP,
			t.sMoTaChiTiet_KHTTCP sMoTaChiTietKhttcp,
			t.fTongGiaTri_KHTTCP fTongGiaTriKhttcp,
			t.sSoKeHoachBQP, t.dNgayKeHoachBQP,
			t.sMoTaChiTiet_KHBQP sMoTaChiTietKhbqp,
			t.fTongGiaTri_KHBQP fTongGiaTriKhbqp,
			t.fTongGiaTri_KHBQP_VND fTongGiaTriKhbqpVnd,
			t.dNgayTao, t.sNguoiTao,
			t.dNgaySua, t.sNguoiSua,
			t.dNgayXoa, t.sNguoiXoa,
			t.bIsActive, t.bIsGoc, t.bIsKhoa,
			t.iLanDieuChinh, t.iLoai,
			(SELECT COUNT(Id) FROM Attachment WHERE ModuleType = 402 AND ObjectId = t.ID) AS TotalFiles,
			CASE WHEN t.iID_ParentAdjustID is null THEN '' ELSE ( SELECT khpr.sSoKeHoachBQP FROM NH_KHTongThe khpr WHERE khpr.ID = t.iID_ParentAdjustID ) END DieuChinhTu,
			t.ID AS IdParentTongThe
		FROM NH_KHTongThe AS t
	)
	SELECT * FROM ListAllTongThe
END;

GO
/****** Object:  StoredProcedure [dbo].[sp_nh_khlcnhathau_index_2]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_khlcnhathau_index_2]
AS
BEGIN
	SELECT tbl.Id, tbl.SSoQuyetDinh, tbl.DNgayQuyetDinh, tbl.SMoTa, nvc.iID_DonViThuHuongID as IIdDonViQuanLy, nvc.iID_MaDonViThuHuong as SMaDonViQuanLy,CONCAT(dv.iID_MaDonVi, ' - ', dv.sTenDonVi) AS sTenDonVi, 
		tbl.iID_DuAnID as IIdDuAnID, tbl.ILanDieuChinh, tbl.iID_ParentID as IIdParentID , pr.sSoQuyetDinh as SSoQuyetDinhParent,dutoan.sTenChuongTrinh as STenChuongTrinh,
		tbl.BIsKhoa, tbl.BIsActive, CAST(0 as int) as ITepDinhKem, tbl.SNguoiTao, tbl.iID_QDDauTuID as IIdQDDauTuID, tbl.iID_DuToanID as IIdDuToanID, 
		tbl.iID_TiGiaID as IIdTiGiaID, tbl.sMaNgoaiTeKhac, tbl.iLoaiKHLCNT, tbl.iLoai, tbl.iThuocMenu, tbl.iID_DonViQuanLyID as IIdDonViQuanLyId ,
		tbl.iID_MaDonViQuanLy as IidMaDonViQuanLy, tbl.iID_KHTT_NhiemVuChiID as IIdKHTTNhiemVuChiId
	FROM NH_DA_KHLCNhaThau as tbl
	left join NH_KHTongThe_NhiemVuChi as nvc on tbl.iID_KHTT_NhiemVuChiID = nvc.ID  
	LEFT JOIN DonVi as dv on nvc.iID_DonViThuHuongID = dv.iID_DonVi
	LEFT JOIN NH_DA_KHLCNhaThau as pr on tbl.iID_ParentID = pr.Id
	LEFT JOIN NH_DA_DuToan as dutoan on tbl.iID_DuToanID = dutoan.ID
	ORDER BY tbl.dNgayTao DESC
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_nhucauchiquy_chitiet_byIDHopDong]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_nhucauchiquy_chitiet_byIDHopDong]
	@idHopDong uniqueidentifier
AS
BEGIN
	
	Select QTND.KinhPhiUSD as fKinhPhiDuocCapCacNamTruoc_USD , QTND.KinhPhiVND as fKinhPhiDuocCapCacNamTruoc_VND ,
QTND.KinhPhiEUR as fKinhPhiDuocCapCacNamTruoc_EUR , QTND.KinhPhiNgoaiTeKhac as fKinhPhiDuocCapCacNamTruoc_NgoaiTeKhac,
KPDC.KinhPhiDaChiUSD  as fKinhPhiDaChiCacNamTruoc_USD,  KPDCVND.KinhPhiDaChiVND as fKinhPhiDaChiCacNamTruoc_VND,
QTNDToY.KinhPhiToYUSD as fKinhPhiDuocCapDenCuoiQuyTruoc_USD , QTNDToY.KinhPhiToYVND as fKinhPhiDuocCapDenCuoiQuyTruoc_VND , 
QTNDToY.KinhPhiToYEUR as fKinhPhiDuocCapDenCuoiQuyTruoc_EUR , QTNDToY.KinhPhiToYNgoaiTeKhac as fKinhPhiDuocCapDenCuoiQuyTruoc_NgoaiTeKhac,
KPDCToY.KinhPhiDaChiUSD as fKinhPhiDaChiDenCuoiQuyTruoc_USD , KPDCToY.KinhPhiDaChiVND as fKinhPhiDaChiDenCuoiQuyTruoc_VND
from NH_DA_HopDong HD
left join NH_DA_DuAn DA on DA.ID = HD.iID_DuAnID 
left join NH_KHTongThe_NhiemVuChi BQP on BQP.ID = HD.iID_KHTongThe_NhiemVuChiID  
left join NH_DM_NhiemVuChi DMBQP on DMBQP.ID = BQP.iID_NhiemVuChiID  
left join (
		Select NDCT.iID_HopDongID, Sum(NDCT.fQTKinhPhiDuocCap_TongSo_USD) as KinhPhiUSD, sum(NDCT.fQTKinhPhiDuocCap_TongSo_VND) as KinhPhiVND ,
		Sum(NDCT.fQTKinhPhiDuocCap_TongSo_EUR) as KinhPhiEUR, sum(NDCT.fQTKinhPhiDuocCap_TongSo_NgoaiTeKhac) as KinhPhiNgoaiTeKhac
		from NH_QT_QuyetToanNienDo_ChiTiet NDCT group by NDCT.iID_HopDongID)
	QTND on QTND.iID_HopDongID = HD.ID
left join (
		select ThanhToan.iLoaiDeNghi , ThanhToan.iID_HopDongID , sum(ThanhToan.fChuyenKhoan_BangSo) as KinhPhiDaChiUSD from NH_TT_ThanhToan ThanhToan
		join NH_TT_ThanhToan_ChiTiet b 
		on ThanhToan.ID = b.iID_DeNghiThanhToanID 
		where ThanhToan.iLoaiNoiDungChi = 1 and ThanhToan.iNamKeHoach = DATEPART(YEAR,GETDATE()) and ThanhToan.dNgayDeNghi < convert(datetime,(concat(year(GETDATE())-1,'-12-31 00:00:00.000')))
		Group BY ThanhToan.iLoaiDeNghi, ThanhToan.iID_HopDongID having ThanhToan.iLoaiDeNghi = 2 or ThanhToan.iLoaiDeNghi = 3
	) KPDC on KPDC.iID_HopDongID = HD.ID
left join (
		select ThanhToan.iLoaiDeNghi , ThanhToan.iID_HopDongID , sum(ThanhToan.fChuyenKhoan_BangSo) as KinhPhiDaChiVND from NH_TT_ThanhToan ThanhToan
		join NH_TT_ThanhToan_ChiTiet b 
		on ThanhToan.ID = b.iID_DeNghiThanhToanID 
		where ThanhToan.iLoaiNoiDungChi = 2 and ThanhToan.iNamKeHoach = DATEPART(YEAR,GETDATE()) and ThanhToan.dNgayDeNghi < convert(datetime,(concat(year(GETDATE())-1,'-12-31 00:00:00.000')))
		Group BY ThanhToan.iLoaiDeNghi, ThanhToan.iID_HopDongID having ThanhToan.iLoaiDeNghi = 2 or ThanhToan.iLoaiDeNghi = 3
	) KPDCVND on KPDCVND.iID_HopDongID = HD.ID
left join (
	Select NDCT.iID_HopDongID, Sum(NDCT.fQTKinhPhiDuocCap_TongSo_USD) as KinhPhiToYUSD, sum(NDCT.fQTKinhPhiDuocCap_TongSo_VND) as KinhPhiToYVND,
	Sum(NDCT.fQTKinhPhiDuocCap_TongSo_EUR) as KinhPhiToYEUR, sum(NDCT.fQTKinhPhiDuocCap_TongSo_NgoaiTeKhac) as KinhPhiToYNgoaiTeKhac
		from NH_QT_QuyetToanNienDo_ChiTiet NDCT
		left join NH_QT_QuyetToanNienDo NDCTParent on NDCTParent.ID = NDCT.iID_QuyetToanNienDoID
		Where NDCTParent.iNamKeHoach = DATEPART(YEAR,GETDATE()) 
		--and DATEPART(quarter,NDCTParent.dNgayDeNghi) < DATEPART(quarter,GETDATE())
		group by NDCT.iID_HopDongID) QTNDToY on QTNDToY.iID_HopDongID = HD.ID
left join (
		select ThanhToan.iLoaiDeNghi , ThanhToan.iID_HopDongID , sum(ThanhToan.fTraDonViThuHuongPheDuyet_BangSo_USD) as KinhPhiDaChiUSD , 
		sum(ThanhToan.fTraDonViThuHuongPheDuyet_BangSo_VND) as KinhPhiDaChiVND
		from NH_TT_ThanhToan ThanhToan
		join NH_TT_ThanhToan_ChiTiet b 
		on ThanhToan.ID = b.iID_DeNghiThanhToanID 
		where ThanhToan.iTrangThai = 2 and ThanhToan.dNgayDeNghi > convert(datetime,(concat(year(GETDATE()),'-1-1 00:00:00.000'))) and 
			((ThanhToan.dNgayDeNghi < convert(datetime,(concat(year(GETDATE()),'-9-30 00:00:00.000'))) and DATEPART(quarter,GETDATE()) = 4)
			or (ThanhToan.dNgayDeNghi < convert(datetime,(concat(year(GETDATE()),'-6-30 00:00:00.000'))) and DATEPART(quarter,GETDATE()) = 3)
			or (ThanhToan.dNgayDeNghi < convert(datetime,(concat(year(GETDATE()),'-3-31 00:00:00.000'))) and DATEPART(quarter,GETDATE()) = 2)
			)
		Group BY ThanhToan.iLoaiDeNghi, ThanhToan.iID_HopDongID having ThanhToan.iLoaiDeNghi = 2 or ThanhToan.iLoaiDeNghi = 3
	) KPDCToY on KPDCToY.iID_HopDongID = HD.ID

		WHERE HD.ID = @idHopDong
		
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtin_hopdong_index]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_thongtin_hopdong_index]
@iThuocMenu INT = NULL
AS
BEGIN
SELECT DISTINCT
	hd.Id,
	hd.sSoHopDong AS SSoHopDong,
	hd.dNgayHopDong AS DNgayHopDong,
	hd.sTenHopDong AS STenHopDong,
	hd.dKhoiCongDuKien AS DKhoiCongDuKien,
	hd.dKetThucDuKien AS DKetThucDuKien,
	hd.iID_KHTongThe_NhiemVuChiID AS IIdKhTongTheNhiemVuChiId,
	hd.iID_LoaiHopDongID AS IIdLoaiHopDongId,
	hd.iID_ParentAdjustID AS IIdParentAdjustId,
	hd.iID_GoiThauID AS IIdGoiThauId,
	hd.iID_ParentID AS IIdParentId,
	hd.iID_NhaThauThucHienID AS IIdNhaThauThucHienId,
	hd.iID_TiGiaID AS IIdTiGiaId,
	hd.iID_DuAnID AS IIdDuAnId,
	hd.iLoai AS ILoai,
	gtnt.fGiaTRiHopDong_USD AS FGiaTriUsd,
	gtnt.fGiaTRiHopDong_VND AS FGiaTriVnd,
	gtnt.fGiaTRiHopDong_EUR AS FGiaTriEur,
	gtnt.fGiaTriHopDong_NgoaiTeKhac AS FGiaTriNgoaiTeKhac,
	hd.sMaNgoaiTeKhac AS SMaNgoaiTeKhac,
	hd.sNguoiTao AS SNguoiTao,
	hd.sNguoiSua AS SNguoiSua,
	hd.sNguoiXoa AS SNguoiXoa,
	hd.dNgaySua AS DNgaySua,
	hd.dNgayTao AS DNgayTao,
	hd.dNgayXoa AS DNgayXoa,
	hd.bIsActive AS BIsActive,
	hd.bIsGoc AS BIsGoc,
	hd.bIsKhoa AS BIsKhoa,
	lhd.sTenLoaiHopDong AS SLoaiHopDong,
	hd.iLanDieuChinh AS ILanDieuChinh,
	nvChi.iID_KHTongTheID AS IIdKhTongTheId,
	nvChi.STenChuongTrinh AS STenChuongTrinh,
	da.sTenDuAn AS STenDuAn,
	CONCAT(donVi.iID_MaDonVi, ' - ', donVi.sTenDonVi) AS STenDonVi,
	hd.iID_DonViQuanLyID AS IIdDonViQuanLyId,
	hd.iID_MaDonViQuanLy AS IIdMaDonViQuanLy,
	CASE
		WHEN hd.iID_ParentAdjustId IS NULL THEN
		'' ELSE ( SELECT TOP 1 hdpr.sSoHopDong FROM NH_DA_HopDong hdpr WHERE hdpr.Id = hd.iID_ParentAdjustId ) 
	END DieuChinhTu
	
FROM NH_DA_HopDong hd
LEFT JOIN NH_DM_LoaiHopDong lhd on hd.iID_LoaiHopDongID = lhd.iID_LoaiHopDongID
LEFT JOIN NH_DA_DuAn da on hd.iID_DuAnID = da.ID
LEFT JOIN DonVi donVi
ON hd.iID_DonViQuanLyID = donVi.iID_DonVi AND hd.iID_MaDonViQuanLy = donVi.iID_MaDonVi
LEFT JOIN
		(SELECT  n.ID, n.iID_KHTongTheID, d.sTenNhiemVuChi AS STenChuongTrinh, n.iID_NhiemVuChiID
		 FROM NH_KHTongThe_NhiemVuChi AS n 
		 INNER JOIN NH_DM_NhiemVuChi AS d 
		 ON n.iID_NhiemVuChiID = d.ID) 
AS nvChi
ON hd.iID_KHTongThe_NhiemVuChiID = nvChi.ID
LEFT JOIN (select iID_HopDongID, sum(fGiaTRiHopDong_USD) as fGiaTRiHopDong_USD
				, sum(fGiaTRiHopDong_VND) as fGiaTRiHopDong_VND
				, sum(fGiaTRiHopDong_EUR) as fGiaTRiHopDong_EUR
				, sum(fGiaTriHopDong_NgoaiTeKhac) as fGiaTriHopDong_NgoaiTeKhac
				from NH_DA_HopDong_GoiThau_NhaThau 
				group by iID_HopDongID
	      ) gtnt on hd.ID = gtnt.iID_HopDongID
WHERE @iThuocMenu IS NULL OR hd.iThuocMenu = @iThuocMenu
ORDER BY
	hd.dNgayTao DESC
END;

GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtin_hopdong_ngoaithuong_index]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_thongtin_hopdong_ngoaithuong_index]
@iThuocMenu INT = NULL
AS
BEGIN
SELECT DISTINCT
	hd.Id,
	hd.sSoHopDong AS SSoHopDong,
	hd.dNgayHopDong AS DNgayHopDong,
	hd.sTenHopDong AS STenHopDong,
	hd.dKhoiCongDuKien AS DKhoiCongDuKien,
	hd.dKetThucDuKien AS DKetThucDuKien,
	hd.iID_KHTongThe_NhiemVuChiID AS IIdKhTongTheNhiemVuChiId,
	hd.iID_LoaiHopDongID AS IIdLoaiHopDongId,
	hd.iID_ParentAdjustID AS IIdParentAdjustId,
	hd.iID_GoiThauID AS IIdGoiThauId,
	hd.iID_ParentID AS IIdParentId,
	hd.iID_NhaThauThucHienID AS IIdNhaThauThucHienId,
	hd.iID_TiGiaID AS IIdTiGiaId,
	hd.iID_DuAnID AS IIdDuAnId,
	hd.iLoai AS ILoai,
	hd.fGiaTriHopDongUSD AS FGiaTriUsd,
	hd.fGiaTriHopDongVND AS FGiaTriVnd,
	hd.fGiaTriHopDongEUR AS FGiaTriEur,
	hd.fGiaTriHopDongNgoaiTeKhac AS FGiaTriNgoaiTeKhac,
	hd.sMaNgoaiTeKhac AS SMaNgoaiTeKhac,
	hd.sNguoiTao AS SNguoiTao,
	hd.sNguoiSua AS SNguoiSua,
	hd.sNguoiXoa AS SNguoiXoa,
	hd.dNgaySua AS DNgaySua,
	hd.dNgayTao AS DNgayTao,
	hd.dNgayXoa AS DNgayXoa,
	hd.bIsActive AS BIsActive,
	hd.bIsGoc AS BIsGoc,
	hd.bIsKhoa AS BIsKhoa,
	lhd.sTenLoaiHopDong AS SLoaiHopDong,
	hd.iLanDieuChinh AS ILanDieuChinh,
	nvChi.iID_KHTongTheID AS IIdKhTongTheId,
	nvChi.STenChuongTrinh AS STenChuongTrinh,
	da.sTenDuAn AS STenDuAn,
	CONCAT(donVi.iID_MaDonVi, ' - ', donVi.sTenDonVi) AS STenDonVi,
	hd.iID_DonViQuanLyID AS IIdDonViQuanLyId,
	hd.iID_MaDonViQuanLy AS IIdMaDonViQuanLy,
	CASE
		WHEN hd.iID_ParentAdjustId IS NULL THEN
		'' ELSE ( SELECT TOP 1 hdpr.sSoHopDong FROM NH_DA_HopDong hdpr WHERE hdpr.Id = hd.iID_ParentAdjustId ) 
	END DieuChinhTu
	
FROM NH_DA_HopDong hd
LEFT JOIN NH_DM_LoaiHopDong lhd on hd.iID_LoaiHopDongID = lhd.iID_LoaiHopDongID
LEFT JOIN NH_DA_DuAn da on hd.iID_DuAnID = da.ID
LEFT JOIN DonVi donVi
ON hd.iID_DonViQuanLyID = donVi.iID_DonVi AND hd.iID_MaDonViQuanLy = donVi.iID_MaDonVi
LEFT JOIN
		(SELECT  n.ID, n.iID_KHTongTheID, d.sTenNhiemVuChi AS STenChuongTrinh, n.iID_NhiemVuChiID
		 FROM NH_KHTongThe_NhiemVuChi AS n 
		 INNER JOIN NH_DM_NhiemVuChi AS d 
		 ON n.iID_NhiemVuChiID = d.ID) 
AS nvChi
ON hd.iID_KHTongThe_NhiemVuChiID = nvChi.ID
WHERE @iThuocMenu IS NULL OR hd.iThuocMenu = @iThuocMenu
ORDER BY
	hd.dNgayTao DESC
END
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtinthanhtoan_index]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_thongtinthanhtoan_index]
	@YearOfWork int,
	@iTrangThai int,
	@bIsDeNghi bit
AS BEGIN
Select thanhtoanchitiet.iID_DeNghiThanhToanID as IdDeNghiThanhToan
	   ,sum(Isnull(thanhtoanchitiet.fDeNghiCapKyNay_USD,0)) as FTongDeNghiKyNayUsd
	   ,sum(Isnull(thanhtoanchitiet.fDeNghiCapKyNay_VND,0)) as FTongDeNghiKyNayVnd
	   ,sum(Isnull(thanhtoanchitiet.fDeNghiCapKyNay_EUR,0)) as FTongDeNghiKyNayEur
	   ,sum(Isnull(thanhtoanchitiet.fDeNghiCapKyNay_NgoaiTeKhac,0)) as FTongDeNghiKyNayNgoaiTeKhac
	   ,sum(Isnull(thanhtoanchitiet.fPheDuyetCapKyNay_USD,0)) as FTongPheDuyetCapKyNayUsd
	   ,sum(Isnull(thanhtoanchitiet.fPheDuyetCapKyNay_VND,0)) as FTongPheDuyetCapKyNayVnd
	   ,sum(Isnull(thanhtoanchitiet.fPheDuyetCapKyNay_EUR,0)) as FTongPheDuyetCapKyNayEur
	   ,sum(Isnull(thanhtoanchitiet.fPheDuyetCapKyNay_NgoaiTeKhac,0)) as FTongPheDuyetCapKyNayNgoaiTeKhac
	   into #tempThanhToanChiTiet
from NH_TT_ThanhToan_ChiTiet thanhtoanchitiet
group by thanhtoanchitiet.iID_DeNghiThanhToanID;


SELECT thanhtoan.ID as Id
      ,thanhtoan.iID_DonViCapTren as IIdDonViCapTren
      ,thanhtoan.iID_MaDonViCapTren as IIdMaDonViCapTren
      ,thanhtoan.iID_DonVi as IIdDonVi
      ,thanhtoan.iID_MaDonVi as IIdMaDonVi
      ,thanhtoan.sSoDeNghi as SSoDeNghi
      ,thanhtoan.dNgayDeNghi as DNgayDeNghi
      ,thanhtoan.sKinhGui as SKinhGui
      ,thanhtoan.iID_KHTongTheID as IIdKhtongTheId
      ,thanhtoan.iID_NhiemVuChiID as IIdNhiemVuChiId
      ,thanhtoan.iID_ChuDauTuID as IIdChuDauTuId
      ,thanhtoan.iID_MaChuDauTu as IIdMaChuDauTu
      ,thanhtoan.iID_HopDongID as IIdHopDongId
      ,thanhtoan.sCanCu as SCanCu
      ,thanhtoan.fSoDuTamUng as FSoDuTamUng
      ,thanhtoan.iLoaiDeNghi as ILoaiDeNghi
      ,thanhtoan.iQuyKeHoach as IQuyKeHoach
      ,thanhtoan.iNamKeHoach as INamKeHoach
      ,thanhtoan.iID_NguonVonID as IIdNguonVonId
      ,thanhtoan.iID_TiGiaID as IIdTiGiaId
      ,thanhtoan.sMaNgoaiTeKhac as SMaNgoaiTeKhac
      ,thanhtoan.iLoaiNoiDungChi as ILoaiNoiDungChi
      ,thanhtoan.fTongDeNghi_BangSo as FTongDeNghiBangSo
      ,thanhtoan.sTongDeNghi_BangChu as STongDeNghiBangChu
      ,thanhtoan.fThuHoiTamUng_BangSo as FThuHoiTamUngBangSo
      ,thanhtoan.fThuHoiTamUng_BangChu as FThuHoiTamUngBangChu
      ,thanhtoan.fTraDonViThuHuong_BangSo as FTraDonViThuHuongBangSo
      ,thanhtoan.fTraDonViThuHuong_BangChu as FTraDonViThuHuongBangChu
      ,thanhtoan.iID_NhaThauID as IIdNhaThauId
      ,thanhtoan.fChuyenKhoan_BangSo as FChuyenKhoanBangSo
      ,thanhtoan.sChuyenKhoan_BangChu as SChuyenKhoanBangChu
      ,thanhtoan.fTienMat_BangSo as FTienMatBangSo
      ,thanhtoan.sTienMat_BangChu as STienMatBangChu
      ,thanhtoan.iID_NhaThau_NguoiNhanID as IIdNhaThauNguoiNhanId
      ,thanhtoan.sTruongPhong as STruongPhong
      ,thanhtoan.sThuTruongDonVi as SThuTruongDonVi
      ,thanhtoan.sNguoiTao as SNguoiTao
      ,thanhtoan.dNgayTao as DNgayTao
      ,thanhtoan.sNguoiSua as SNguoiSua
      ,thanhtoan.dNgaySua as DNgaySua
      ,thanhtoan.sNguoiXoa as SNguoiXoa
      ,thanhtoan.dNgayXoa as DNgayXoa
	  ,thanhtoan.sSoTaiKhoan as SSoTaiKhoan
	  ,thanhtoan.sNganHang as SNganHang
	  ,thanhtoan.sNguoiLienHe as SNguoiLienHe
	  ,thanhtoan.sNoiCapCMND as SNoiCapCMND
	  ,thanhtoan.dNgayCapCMND as DNgayCapCMND
	  ,thanhtoan.sSoCMND as SSoCMND
      ,CASE 
		WHEN (@bIsDeNghi = 1 AND thanhtoan.bTongHop = 1) OR thanhtoan.bIsKhoa = 1
			THEN  1 
		ELSE 0
		END as BIsKhoa
      ,thanhtoan.bIsXoa as BIsXoa
      ,thanhtoan.iTrangThai as ITrangThai
      ,thanhtoan.iID_NhaThau_NganHangID as IIdNhaThauNganHangId
	  ,CONCAT(donvi.iID_MaDonVi, ' - ', donvi.sTenDonVi) AS STenDonViMaDonVi
	  ,nhiemvuchi.sTenNhiemVuChi As STenNhiemVuChi
	  ,CONCAT(hopdong.sSoHopDong, ' - ', hopdong.sTenHopDong) AS STenHopDongSoHopDong
	  ,nguonns.sTen as TenNguonVon
	  ,thanhtoanchitiet.FTongDeNghiKyNayUsd as FTongDeNghiKyNayUsd
	  ,thanhtoanchitiet.FTongDeNghiKyNayVnd as FTongDeNghiKyNayVnd
	  ,thanhtoanchitiet.FTongDeNghiKyNayEur as FTongDeNghiKyNayEur
	  ,thanhtoanchitiet.FTongDeNghiKyNayNgoaiTeKhac as FTongDeNghiKyNayNgoaiTeKhac
	  ,thanhtoanchitiet.FTongPheDuyetCapKyNayUsd as FTongPheDuyetCapKyNayUsd
	  ,thanhtoanchitiet.FTongPheDuyetCapKyNayVnd as FTongPheDuyetCapKyNayVnd
	  ,thanhtoanchitiet.FTongPheDuyetCapKyNayEur as FTongPheDuyetCapKyNayEur
	  ,thanhtoanchitiet.FTongPheDuyetCapKyNayNgoaiTeKhac as FTongPheDuyetCapKyNayNgoaiTeKhac
	  ,thanhtoan.dNgayPheDuyet as DNgayPheDuyet
	  ,(SELECT COUNT(Id) FROM Attachment WHERE ModuleType = 411 AND ObjectId = thanhtoan.ID) AS TotalFiles,
	  thanhtoan.iID_Parent as ParentId,
	  thanhtoan.bTongHop as BTongHop,
	  thanhtoan.iCoQuanThanhToan as ICoQuanThanhToan
  FROM NH_TT_ThanhToan thanhtoan
  left join DonVi donvi on donvi.iID_DonVi = thanhtoan.iID_DonVi
  left join NH_DM_NhiemVuChi nhiemvuchi on nhiemvuchi.ID = thanhtoan.iID_NhiemVuChiID
  left join NH_DA_HopDong hopdong on hopdong.Id = thanhtoan.iID_HopDongID
  left join NguonNganSach nguonns on thanhtoan.iID_NguonVonID = nguonns.iID_MaNguonNganSach 
  left join #tempThanhToanChiTiet thanhtoanchitiet on thanhtoanchitiet.IdDeNghiThanhToan = thanhtoan.ID
  where (@iTrangThai = -1 OR thanhtoan.iTrangThai = @iTrangThai) and (bTongHop is null or bTongHop != 1)
  order by thanhtoan.dNgayTao desc;

drop table #tempThanhToanChiTiet;
END;

GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thuchien_ngansach_index]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_nh_thuchien_ngansach_index]
	(@tabTable int, 
	@iQuyList int,
	@iNam int,
	@iTuNam int,
	@iDenNam int,
	@iDonvi uniqueidentifier)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- select thanh toan su dung kinh phi tu dau nam quy nay
	SELECT  
		th.Id, th.iID_ChungTu, th.iCoQuanThanhToan,
		ckpNamTruocDGN.fGiaTriUsd as ckpNamTruocUSDDGN ,ckpNamTruocDGN.fGiaTriVnd as ckpNamTruocVNDDGN,
		ckpNamNayDGN.fGiaTriUsd as ckpNamNayUSDDGN ,ckpNamNayDGN.fGiaTriVnd as ckpNamNayVNDDGN,
		ttNamTruocDGN.fGiaTriUsd as ttNamTruocUSDDGN ,ttNamTruocDGN.fGiaTriVnd as ttNamTruocVNDDGN,
		ttNamNayDGN.fGiaTriUsd as ttNamNayUSDDGN ,ttNamNayDGN.fGiaTriVnd as ttNamNayVNDDGN,
		twNamTruocDGN.fGiaTriUsd as twNamTruocUSDDGN ,twNamTruocDGN.fGiaTriVnd as twNamTruocVNDDGN,
		twNamNayDGN.fGiaTriUsd as twNamNayUSDDGN ,twNamNayDGN.fGiaTriVnd as twNamNayVNDDGN,
		thNamTrcDGN.fGiaTriUsd as thNamTrcUSDDGN ,thNamTrcDGN.fGiaTriVnd as thNamTrcVNDDGN,
		thNamNayDGN.fGiaTriUsd as thNamNayUSDDGN ,thNamNayDGN.fGiaTriVnd as thNamNayVNDDGN,
		------------------------------------------------------------------------------------------------
		ckpNamTruoc.fGiaTriUsd as ckpNamTruocUSD ,ckpNamTruoc.fGiaTriVnd as ckpNamTruocVND,
		ckpNamNay.fGiaTriUsd as ckpNamNayUSD ,ckpNamNay.fGiaTriVnd as ckpNamNayVND,
		ttNamTruoc.fGiaTriUsd as ttNamTruocUSD ,ttNamTruoc.fGiaTriVnd as ttNamTruocVND,
		ttNamNay.fGiaTriUsd as ttNamNayUSD ,ttNamNay.fGiaTriVnd as ttNamNayVND,
		twNamTruoc.fGiaTriUsd as twNamTruocUSD ,twNamTruoc.fGiaTriVnd as twNamTruocVND,
		twNamNay.fGiaTriUsd as twNamNayUSD ,twNamNay.fGiaTriVnd as twNamNayVND,
		thNamTrc.fGiaTriUsd as thNamTrcUSD ,thNamTrc.fGiaTriVnd as thNamTrcVND,
		thNamNay.fGiaTriUsd as thNamNayUSD ,thNamNay.fGiaTriVnd as thNamNayVND
		------------------------------------------------------------------------------------------------
		--ckpNamTruocToYear.fGiaTriUsd as ckpNamTruocUSDToYear ,ckpNamTruocToYear.fGiaTriVnd as ckpNamTruocVNDToYear,
		--ckpNamNayToYear.fGiaTriUsd as ckpNamNayUSDToYear ,ckpNamNayToYear.fGiaTriVnd as ckpNamNayVNDToYear,
		--ttNamTruocToYear.fGiaTriUsd as ttNamTruocUSDToYear ,ttNamTruocToYear.fGiaTriVnd as ttNamTruocVNDToYear,
		--ttNamNayToYear.fGiaTriUsd as ttNamNayUSDToYear ,ttNamNayToYear.fGiaTriVnd as ttNamNayVNDToYear,
		--twNamTruocToYear.fGiaTriUsd as twNamTruocUSDToYear ,twNamTruocToYear.fGiaTriVnd as twNamTruocVNDToYear,
		--twNamNayToYear.fGiaTriUsd as twNamNayUSDToYear ,twNamNayToYear.fGiaTriVnd as twNamNayVNDToYear,
		--thNamTrcToYear.fGiaTriUsd as thNamTrcUSDToYear ,thNamTrcToYear.fGiaTriVnd as thNamTrcVNDToYear,
		--thNamNayToYear.fGiaTriUsd as thNamNayUSDToYear ,thNamNayToYear.fGiaTriVnd as thNamNayVNDToYear
	INTO #TongHop
	FROM NH_TH_TongHop th
		-- Đã giải ngân
		--Start Cấp kinh phí năm trước chuyển sang
		left join NH_TH_TongHop ckpNamTruocDGN on ckpNamTruocDGN.ID = th.ID and ckpNamTruocDGN.bIsLog = 0 and ckpNamTruocDGN.sMaTienTrinh != '300'
														and ckpNamTruocDGN.sMaNguon = '102' and ckpNamTruocDGN.sMaDich = '000' 
														and ckpNamTruocDGN.iNamKeHoach < @iNam
		--Start Cấp kinh phí năm nay
		left join NH_TH_TongHop ckpNamNayDGN on ckpNamNayDGN.ID = th.ID and ckpNamNayDGN.bIsLog = 0 and ckpNamNayDGN.sMaTienTrinh != '300'
														and ckpNamNayDGN.sMaNguon = '101' and ckpNamNayDGN.sMaDich = '000' 
														and ckpNamNayDGN.iNamKeHoach < @iNam
		--Start Thanh toán sử dụng kinh phí năm trước chuyển sang
		left join NH_TH_TongHop ttNamTruocDGN on ttNamTruocDGN.ID = th.ID and ttNamTruocDGN.bIsLog = 0 and ttNamTruocDGN.sMaTienTrinh != '300'
														and ttNamTruocDGN.sMaNguon = '000' and ttNamTruocDGN.sMaDich = '112' 
														and ttNamTruocDGN.iNamKeHoach < @iNam
		--Start Thanh toán sử dụng kinh phí năm nay
		left join NH_TH_TongHop ttNamNayDGN on ttNamNayDGN.ID = th.ID and ttNamNayDGN.bIsLog = 0 and ttNamNayDGN.sMaTienTrinh != '300'
														and ttNamNayDGN.sMaNguon = '000' and ttNamNayDGN.sMaDich = '111' 
														and ttNamNayDGN.iNamKeHoach < @iNam
		--Start Tạm ứng sử dụng kinh phí năm trước chuyển sang
		left join NH_TH_TongHop twNamTruocDGN on twNamTruocDGN.ID = th.ID and twNamTruocDGN.bIsLog = 0 and twNamTruocDGN.sMaTienTrinh != '300'
														and twNamTruocDGN.sMaNguon = '000' and twNamTruocDGN.sMaDich = '122' 
														and twNamTruocDGN.iNamKeHoach < @iNam
		--Start Tạm ứng sử dụng kinh phí năm nay
		left join NH_TH_TongHop twNamNayDGN on twNamNayDGN.ID = th.ID and twNamNayDGN.bIsLog = 0 and twNamNayDGN.sMaTienTrinh != '300'
														and twNamNayDGN.sMaNguon = '000' and twNamNayDGN.sMaDich = '121' 
														and twNamNayDGN.iNamKeHoach < @iNam
		--Start Thu hồi tạm ứng năm trước chuyển sang
		left join NH_TH_TongHop thNamTrcDGN on thNamTrcDGN.ID = th.ID and thNamTrcDGN.bIsLog = 0 and thNamTrcDGN.sMaTienTrinh != '300'
														and thNamTrcDGN.sMaNguon = '122' and thNamTrcDGN.sMaDich = '000' 
														and thNamTrcDGN.iNamKeHoach < @iNam
		--Start Thu hồi tạm ứng năm nay
		left join NH_TH_TongHop thNamNayDGN on thNamNayDGN.ID = th.ID and thNamNayDGN.bIsLog = 0 and thNamNayDGN.sMaTienTrinh != '300'
														and thNamNayDGN.sMaNguon = '121' and thNamNayDGN.sMaDich = '000' 
														and thNamNayDGN.iNamKeHoach < @iNam
		------------------------------------------------------------------------------------------------
		-- Kinh phí được cấp
		--Start Cấp kinh phí năm trước chuyển sang
		left join NH_TH_TongHop ckpNamTruoc on ckpNamTruoc.ID = th.ID and ckpNamTruoc.bIsLog = 0 and ckpNamTruoc.sMaTienTrinh != '300'
														and ckpNamTruoc.sMaNguon = '102' and ckpNamTruoc.sMaDich = '000' 
														and ckpNamTruoc.iNamKeHoach = @iNam and ckpNamTruoc.iQuyKeHoach <= @iQuyList
		--Start Cấp kinh phí năm nay
		left join NH_TH_TongHop ckpNamNay on ckpNamNay.ID = th.ID and ckpNamNay.bIsLog = 0 and ckpNamNay.sMaTienTrinh != '300'
														and ckpNamNay.sMaNguon = '101' and ckpNamNay.sMaDich = '000' 
														and ckpNamNay.iNamKeHoach = @iNam and ckpNamNay.iQuyKeHoach <= @iQuyList
		--Start Thanh toán sử dụng kinh phí năm trước chuyển sang
		left join NH_TH_TongHop ttNamTruoc on ttNamTruoc.ID = th.ID and ttNamTruoc.bIsLog = 0 and ttNamTruoc.sMaTienTrinh != '300'
														and ttNamTruoc.sMaNguon = '000' and ttNamTruoc.sMaDich = '112' 
														and ttNamTruoc.iNamKeHoach = @iNam and ttNamTruoc.iQuyKeHoach <= @iQuyList
		--Start Thanh toán sử dụng kinh phí năm nay
		left join NH_TH_TongHop ttNamNay on ttNamNay.ID = th.ID and ttNamNay.bIsLog = 0 and ttNamNay.sMaTienTrinh != '300'
														and ttNamNay.sMaNguon = '000' and ttNamNay.sMaDich = '111' 
														and ttNamNay.iNamKeHoach = @iNam and ttNamNay.iQuyKeHoach <= @iQuyList
		--Start Tạm ứng sử dụng kinh phí năm trước chuyển sang
		left join NH_TH_TongHop twNamTruoc on twNamTruoc.ID = th.ID and twNamTruoc.bIsLog = 0 and twNamTruoc.sMaTienTrinh != '300'
														and twNamTruoc.sMaNguon = '000' and twNamTruoc.sMaDich = '122' 
														and twNamTruoc.iNamKeHoach = @iNam and twNamTruoc.iQuyKeHoach <= @iQuyList
		--Start Tạm ứng sử dụng kinh phí năm nay
		left join NH_TH_TongHop twNamNay on twNamNay.ID = th.ID and twNamNay.bIsLog = 0 and twNamNay.sMaTienTrinh != '300'
														and twNamNay.sMaNguon = '000' and twNamNay.sMaDich = '121' 
														and twNamNay.iNamKeHoach = @iNam and twNamNay.iQuyKeHoach <= @iQuyList
		--Start Thu hồi tạm ứng năm trước chuyển sang
		left join NH_TH_TongHop thNamTrc on thNamTrc.ID = th.ID and thNamTrc.bIsLog = 0 and thNamTrc.sMaTienTrinh != '300'
														and thNamTrc.sMaNguon = '122' and thNamTrc.sMaDich = '000' 
														and thNamTrc.iNamKeHoach = @iNam and thNamTrc.iQuyKeHoach <= @iQuyList
		--Start Thu hồi tạm ứng năm nay
		left join NH_TH_TongHop thNamNay on thNamNay.ID = th.ID and thNamNay.bIsLog = 0 and thNamNay.sMaTienTrinh != '300'
														and thNamNay.sMaNguon = '121' and thNamNay.sMaDich = '000' 
														and thNamNay.iNamKeHoach = @iNam and thNamNay.iQuyKeHoach <= @iQuyList
		------------------------------------------------------------------------------------------------
		---- Kinh phí được cấp Nam Nay
		----Start Cấp kinh phí năm trước chuyển sang
		--left join NH_TH_TongHop ckpNamTruocToYear on ckpNamTruocToYear.ID = th.ID and ckpNamTruocToYear.bIsLog = 0 and ckpNamTruocToYear.sMaTienTrinh != '300'
		--												and ckpNamTruocToYear.sMaNguon = '102' and ckpNamTruocToYear.sMaDich = '000' 
		--												and ckpNamTruocToYear.iNamKeHoach = @iNam -- and ckpNamTruocToYear.iQuyKeHoach <= @iQuyList
		----Start Cấp kinh phí năm nay
		--left join NH_TH_TongHop ckpNamNayToYear on ckpNamNayToYear.ID = th.ID and ckpNamNayToYear.bIsLog = 0 and ckpNamNayToYear.sMaTienTrinh != '300'
		--												and ckpNamNayToYear.sMaNguon = '101' and ckpNamNayToYear.sMaDich = '000' 
		--												and ckpNamNayToYear.iNamKeHoach = @iNam -- and ckpNamNayToYear.iQuyKeHoach <= @iQuyList
		----Start Thanh toán sử dụng kinh phí năm trước chuyển sang
		--left join NH_TH_TongHop ttNamTruocToYear on ttNamTruocToYear.ID = th.ID and ttNamTruocToYear.bIsLog = 0 and ttNamTruocToYear.sMaTienTrinh != '300'
		--												and ttNamTruocToYear.sMaNguon = '000' and ttNamTruocToYear.sMaDich = '112' 
		--												and ttNamTruocToYear.iNamKeHoach = @iNam -- and ttNamTruocToYear.iQuyKeHoach <= @iQuyList
		----Start Thanh toán sử dụng kinh phí năm nay
		--left join NH_TH_TongHop ttNamNayToYear on ttNamNayToYear.ID = th.ID and ttNamNayToYear.bIsLog = 0 and ttNamNayToYear.sMaTienTrinh != '300'
		--												and ttNamNayToYear.sMaNguon = '000' and ttNamNayToYear.sMaDich = '111' 
		--												and ttNamNayToYear.iNamKeHoach = @iNam -- and ttNamNayToYear.iQuyKeHoach <= @iQuyList
		----Start Tạm ứng sử dụng kinh phí năm trước chuyển sang
		--left join NH_TH_TongHop twNamTruocToYear on twNamTruocToYear.ID = th.ID and twNamTruocToYear.bIsLog = 0 and twNamTruocToYear.sMaTienTrinh != '300'
		--												and twNamTruocToYear.sMaNguon = '000' and twNamTruocToYear.sMaDich = '122' 
		--												and twNamTruocToYear.iNamKeHoach = @iNam -- and twNamTruocToYear.iQuyKeHoach <= @iQuyList
		----Start Tạm ứng sử dụng kinh phí năm nay
		--left join NH_TH_TongHop twNamNayToYear on twNamNayToYear.ID = th.ID and twNamNayToYear.bIsLog = 0 and twNamNayToYear.sMaTienTrinh != '300'
		--												and twNamNayToYear.sMaNguon = '000' and twNamNayToYear.sMaDich = '121' 
		--												and twNamNayToYear.iNamKeHoach = @iNam -- and twNamNayToYear.iQuyKeHoach <= @iQuyList
		----Start Thu hồi tạm ứng năm trước chuyển sang
		--left join NH_TH_TongHop thNamTrcToYear on thNamTrcToYear.ID = th.ID and thNamTrcToYear.bIsLog = 0 and thNamTrcToYear.sMaTienTrinh != '300'
		--												and thNamTrcToYear.sMaNguon = '122' and thNamTrcToYear.sMaDich = '000' 
		--												and thNamTrcToYear.iNamKeHoach = @iNam -- and thNamTrcToYear.iQuyKeHoach <= @iQuyList
		----Start Thu hồi tạm ứng năm nay
		--left join NH_TH_TongHop thNamNayToYear on thNamNayToYear.ID = th.ID and thNamNayToYear.bIsLog = 0 and thNamNayToYear.sMaTienTrinh != '300'
		--												and thNamNayToYear.sMaNguon = '121' and thNamNayToYear.sMaDich = '000' 
		--												and thNamNayToYear.iNamKeHoach = @iNam -- and thNamNayToYear.iQuyKeHoach <= @iQuyList
    -- Insert statements for procedure here
	SELECT ttct.*, 
		tt.iQuyKeHoach,
		tt.iNamKeHoach, 
		dm_nvc.sTenNhiemVuChi,
		da.sTenDuAn,
		hd.sTenHopDong,
		tt.iLoaiNoiDungChi,
		dmCDT.sTenDonVi as sTenCDT,
		tt.iID_DonVi as IDDonVi,
		nvc.fGiaTriKH_TTCP as NCVTTCP,
		nvc.fGiaTriKH_BQP as NhiemVuChi , 
		QTNDCT.fLuyKeKinhPhiDuocCap_USD , QTNDCT.fLuyKeKinhPhiDuocCap_VND,
		QTNDCT.fDeNghiQTNamNay_USD , QTNDCT.fDeNghiQTNamNay_VND,
		KHTT.iGiaiDoanDen , KHTT.iGiaiDoanTu ,
		Case When nvc.iID_NhiemVuChiID is null then '00000000-0000-0000-0000-000000000000' else nvc.iID_NhiemVuChiID End as IDNhiemVuChi,
		Case When da.ID is null then '00000000-0000-0000-0000-000000000000' else da.ID End as IDDuAn,
		Case When hd.ID is null then '00000000-0000-0000-0000-000000000000' else hd.ID End as IDHopDong,
		Case When hd.fGiaTriUSD > 0 then hd.fGiaTriUSD Else da.fUSD End as HopDongUSD ,
		Case When hd.fGiaTriVND > 0 then hd.fGiaTriVND Else da.fVND End as HopDongVND ,
		KinhPhiDuoCapCacNamTruoc.KinhPhiUSD as KinhPhiUSD ,
		KinhPhiDuoCapCacNamTruoc.KinhPhiVND as KinhPhiVND, --Kinh phí được cấp các năm trước
		KinhPhiDuoCapTuDauNam.KinhPhiDuocCapToYUSD as KinhPhiToYUSD,
		KinhPhiDuoCapTuDauNam.KinhPhiDuocCapToYVND as KinhPhiToYVND, -- Kinh phí được cấp từ đầu năm đến quý này
		KinhPhiDaGiaiNganCacNamTruoc.KinhPhiUSD as KinhPhiDaChiUSD,
		KinhPhiDaGiaiNganCacNamTruoc.KinhPhiVND as KinhPhiDaChiVND , --Kinh phí đã giải ngân các năm trước
		KinhPhiDuoCapTuDauNam.KinhPhiDaGiaiNganToYUSD as KinhPhiDaChiToYUSD,
		KinhPhiDuoCapTuDauNam.KinhPhiDaGiaiNganToYVND as KinhPhiDaChiToYVND -- Kinh phí đã giải ngân từ đầu năm đến quý này

		into #TMP
		FROM NH_TT_ThanhToan_ChiTiet ttct 
		left join NH_TT_ThanhToan tt on ttct.iID_DeNghiThanhToanID = tt.ID 
		left join NH_DA_HopDong hd on hd.ID = tt.iID_HopDongID
		left join NH_DA_DuAn da on da.ID = tt.iID_DuAnID
		left join DM_ChuDauTu dmCDT on da.iID_ChuDauTuID = dmCDT.iID_DonVi
		left join NH_KHTongThe_NhiemVuChi nvc on nvc.iID_NhiemVuChiID = tt.iID_NhiemVuChiID and nvc.iID_KHTongTheID = tt.iID_KHTongTheID
		left join NH_DM_NhiemVuChi dm_nvc on dm_nvc.ID = nvc.iID_NhiemVuChiID
		left join NH_KHTongThe KHTT on KHTT.ID = nvc.iID_KHTongTheID
		
-- Tính cho giai đoạn lấy từ QTND chi tiết
	left join (
		Select NDCT.iID_ThanhToan_ChiTietID, 
		ISNULL(SUM(NDCT.fLuyKeKinhPhiDuocCap_USD),0) as fLuyKeKinhPhiDuocCap_USD, 
		ISNULL(SUM(NDCT.fLuyKeKinhPhiDuocCap_VND),0) as fLuyKeKinhPhiDuocCap_VND,
		ISNULL(SUM(NDCT.fDeNghiQTNamNay_USD),0) as fDeNghiQTNamNay_USD,
		ISNULL(SUM(NDCT.fDeNghiQTNamNay_VND),0) as fDeNghiQTNamNay_VND,
		ISNULL(SUM(NDCT.fQTKinhPhiDuyetCacNamTruoc_USD),0) as fQTKinhPhiDuyetCacNamTruoc_USD,
		ISNULL(SUM(NDCT.fQTKinhPhiDuyetCacNamTruoc_VND),0) as fQTKinhPhiDuyetCacNamTruoc_VND
		from NH_QT_QuyetToanNienDo_ChiTiet NDCT group by NDCT.iID_ThanhToan_ChiTietID
	)QTNDCT on QTNDCT.iID_ThanhToan_ChiTietID = ttct.ID

	left join (
		Select tt.ID, ISNULL(SUM(th.fGiaTriUsd),0) as KinhPhiUSD , ISNULL(SUM(th.fGiaTriVnd),0) as KinhPhiVND from NH_TT_ThanhToan tt
		left join NH_TH_TongHop th on th.bIsLog = 0 and th.sMaTienTrinh != '300' and th.iNamKeHoach = (@iNam - 1) and th.sMaDich = '000' and th.sMaNguon = '303' 
					and th.iID_DonVi = tt.iID_DonVi
					and (th.iID_DuAnId = tt.iID_DuAnId or ( th.iID_DuAnId is null and tt.iID_DuAnId is null))
					and (th.iID_HopDongId = tt.iID_HopDongId or ( th.iID_HopDongId is null and tt.iID_HopDongId is null))
		Group By tt.ID
	)KinhPhiDuoCapCacNamTruoc on KinhPhiDuoCapCacNamTruoc.ID = tt.ID

	left join (
		Select tt.ID, ISNULL(SUM(th.fGiaTriUsd),0) as KinhPhiUSD , ISNULL(SUM(th.fGiaTriVnd),0) as KinhPhiVND from NH_TT_ThanhToan tt
		left join NH_TH_TongHop th on th.bIsLog = 0 and th.sMaTienTrinh != '300' and th.iNamKeHoach = (@iNam - 1) and th.sMaDich = '311' and th.sMaNguon = '000' 
					and th.iID_DonVi = tt.iID_DonVi
					and (th.iID_DuAnId = tt.iID_DuAnId or ( th.iID_DuAnId is null and tt.iID_DuAnId is null))
					and (th.iID_HopDongId = tt.iID_HopDongId or ( th.iID_HopDongId is null and tt.iID_HopDongId is null))
		Group By tt.ID
	)KinhPhiDaGiaiNganCacNamTruoc on KinhPhiDaGiaiNganCacNamTruoc.ID = tt.ID
	--Start
	left join (
		SELECT tt.ID, 
			Case When th.iCoQuanThanhToan = 1 then (ISNULL(SUM(th.ttNamTruocUSD),0) + 
													ISNULL(SUM(th.ttNamNayUSD),0) + 
													ISNULL(SUM(th.twNamTruocUSD),0) + 
													ISNULL(SUM(th.twNamNayUSD),0) - 
													ISNULL(SUM(th.thNamTrcUSD),0) - 
													ISNULL(SUM(th.thNamNayUSD),0)) 
				When th.iCoQuanThanhToan = 2 then (ISNULL(SUM(th.ckpNamTruocUSD),0) + 
													ISNULL(SUM(th.ckpNamNayUSD),0))
				Else 0 End as KinhPhiDuocCapToYUSD,
			Case When th.iCoQuanThanhToan = 1 then (ISNULL(SUM(th.ttNamTruocVND),0) + 
													ISNULL(SUM(th.ttNamNayVND),0) + 
													ISNULL(SUM(th.twNamTruocVND),0) + 
													ISNULL(SUM(th.twNamNayVND),0) - 
													ISNULL(SUM(th.thNamTrcVND),0) - 
													ISNULL(SUM(th.thNamNayVND),0)) 
				When th.iCoQuanThanhToan = 2 then (ISNULL(SUM(th.ckpNamTruocVND),0) + 
													ISNULL(SUM(th.ckpNamNayVND),0))
				Else 0 End as KinhPhiDuocCapToYVND,
			(ISNULL(SUM(th.ttNamTruocUSDDGN),0) + 
			ISNULL(SUM(th.ttNamNayUSDDGN),0) + 
			ISNULL(SUM(th.twNamTruocUSDDGN),0) + 
			ISNULL(SUM(th.twNamNayUSDDGN),0) - 
			ISNULL(SUM(th.thNamTrcUSDDGN),0) - 
			ISNULL(SUM(th.thNamNayUSDDGN),0)) as KinhPhiDaGiaiNganToYUSD,
			(ISNULL(SUM(th.ttNamTruocVNDDGN),0) + 
			ISNULL(SUM(th.ttNamNayVNDDGN),0) + 
			ISNULL(SUM(th.twNamTruocVNDDGN),0) + 
			ISNULL(SUM(th.twNamNayVNDDGN),0) - 
			ISNULL(SUM(th.thNamTrcVNDDGN),0) - 
			ISNULL(SUM(th.thNamNayVNDDGN),0)) as KinhPhiDaGiaiNganToYVND
		FROM NH_TT_ThanhToan tt
			left join #TongHop th on th.iID_ChungTu = tt.id
			Group By tt.ID, th.iCoQuanThanhToan
	)KinhPhiDuoCapTuDauNam on KinhPhiDuoCapTuDauNam.ID = tt.ID

Where tt.iID_DonVi = @iDonvi or @iDonvi is null or @iDonvi = '00000000-0000-0000-0000-000000000000'
Order by tt.iID_NhiemVuChiID , dm_nvc.sTenNhiemVuChi , da.ID , da.sTenDuAn, tt.iLoaiNoiDungChi ,hd.ID , hd.sTenHopDong
SELECT * into #Main FROM #TMP tt
Where ((@tabTable = 0 and (tt.iQuyKeHoach = @iQuyList or @iQuyList = 0) and tt.iNamKeHoach = @iNam ) 
		or (@tabTable = 1 and @iTuNam <= tt.iNamKeHoach and tt.iNamKeHoach <= @iDenNam)) 
		and (tt.IDDonVi = @iDonvi or @iDonvi is null or @iDonvi = '00000000-0000-0000-0000-000000000000')
SELECT distinct tt.*, 
	--TMPMain.KinhPhiUSDSelect as KinhPhiUSD , TMPMain.KinhPhiVNDSelect as KinhPhiVND , 
	TMPMain.KinhPhiToYUSDSelect as KinhPhiToYUSD , TMPMain.KinhPhiToYVNDSelect as KinhPhiToYVND , 
	TMPMain.KinhPhiDaChiUSDSelect as KinhPhiDaChiUSD , TMPMain.KinhPhiDaChiVNDSelect as KinhPhiDaChiVND , 
	TMPMain.KinhPhiDaChiToYUSDSelect as KinhPhiDaChiToYUSD , TMPMain.KinhPhiDaChiToYVNDSelect as KinhPhiDaChiToYVND 
FROM #Main tt
Left join (
	Select TMP.IDNhiemVuChi , TMP.IDDuAn , TMP.sTenHopDong , TMP.IDHopDong , 
	--SUM(ISNULL(TMP.KinhPhiUSD, 0)) as KinhPhiUSDSelect , SUM(ISNULL(TMP.KinhPhiVND, 0)) as KinhPhiVNDSelect ,
	SUM(ISNULL(TMP.KinhPhiToYUSD, 0)) as KinhPhiToYUSDSelect , SUM(ISNULL(TMP.KinhPhiToYVND, 0)) as KinhPhiToYVNDSelect , 
	SUM(ISNULL(TMP.KinhPhiDaChiUSD, 0)) as KinhPhiDaChiUSDSelect , SUM(ISNULL(TMP.KinhPhiDaChiVND, 0)) as KinhPhiDaChiVNDSelect , 
	SUM(ISNULL(TMP.KinhPhiDaChiToYUSD, 0)) as KinhPhiDaChiToYUSDSelect , SUM(ISNULL(TMP.KinhPhiDaChiToYVND, 0)) as KinhPhiDaChiToYVNDSelect 
	from #TMP TMP
	where TMP.IDNhiemVuChi is not null 
	Group by  TMP.IDNhiemVuChi , TMP.IDDuAn , TMP.IDHopDong ,TMP.sTenHopDong
) TMPMain on TMPMain.IDNhiemVuChi = tt.IDNhiemVuChi and ((tt.IDDuAN is null and TMPMain.IDNhiemVuChi is null) or TMPMain.IDDuAn = tt.IDDuAn) 
and ((tt.IDHopDong is null and TMPMain.IDDuAn is null) or TMPMain.IDHopDong	 = tt.IDHopDong) 
Order by tt.IDNhiemVuChi , tt.sTenNhiemVuChi , tt.IDDuAn , tt.sTenDuAn, tt.iLoaiNoiDungChi ,tt.IDHopDong , tt.sTenHopDong
DROP TABLE #TMP
DROP TABLE #Main
DROP TABLE #TongHop



END
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thuchien_ngansach_report]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_thuchien_ngansach_report]
	@tabindex int, 
	@iQuyPrint int,
	@iTuNamPrint int,
	@iDenNamPrint int,
	@iDonvi uniqueidentifier
AS
BEGIN
		Select distinct ttct.*, tt.iNamKeHoach, nvc.ID as IDNhiemVuChi, da.ID as IDDuAn, tt.iLoaiNoiDungChi,hd.ID as IDHopDong,
dm_nvc.sTenNhiemVuChi , da.sTenDuAn , hd.sTenHopDong,tt.iLoaiNoiDungChi, dmCDT.sTenDonVi as sTenCDT,
Case When hd.fGiaTriUSD > 0 then hd.fGiaTriUSD Else da.fUSD End as HopDongUSD ,
Case When hd.fGiaTriVND > 0 then hd.fGiaTriVND Else da.fVND End as HopDongVND ,
nvc.fGiaTriKH_TTCP as NCVTTCP ,nvc.fGiaTriKH_BQP as NhiemVuChi , 
QTND.KinhPhiUSD as KinhPhiUSD , QTND.KinhPhiVND as KinhPhiVND,
QTNDToY.KinhPhiToYUSD as KinhPhiToYUSD ,QTNDToY.KinhPhiToYVND as KinhPhiToYVND,
KPDC.KinhPhiDaChiUSD as KinhPhiDaChiUSD , KPDCVND.KinhPhiDaChiVND as KinhPhiDaChiVND , 
KPDCToY.KinhPhiDaChiUSD as KinhPhiDaChiToYUSD , KPDCVNDToY.KinhPhiDaChiVND as KinhPhiDaChiToYVND,
QTNDCT.fLuyKeKinhPhiDuocCap_USD , QTNDCT.fLuyKeKinhPhiDuocCap_VND,
KHTT.iGiaiDoanDen , KHTT.iGiaiDoanTu , tt.iID_DonVi
from NH_TT_ThanhToan_ChiTiet ttct
left join NH_TT_ThanhToan tt on ttct.iID_DeNghiThanhToanID = tt.ID 
left join NH_DA_HopDong hd on hd.ID = tt.iID_HopDongID
left join NH_DA_DuAn da on da.ID = hd.iID_DuAnID
left join DM_ChuDauTu dmCDT on da.iID_ChuDauTuID = dmCDT.iID_DonVi
left join NH_KHTongThe_NhiemVuChi nvc on nvc.iID_NhiemVuChiID = tt.iID_NhiemVuChiID
left join NH_DM_NhiemVuChi dm_nvc on dm_nvc.ID = nvc.iID_NhiemVuChiID
left join NH_KHTongThe KHTT on KHTT.ID = tt.iID_KHTongTheID
left join NH_QT_QuyetToanNienDo_ChiTiet QTNDCT on QTNDCT.iID_KHTT_NhiemVuChiID = nvc.ID
left join (
		Select NDCT.iID_HopDongID, Sum(NDCT.fQTKinhPhiDuocCap_TongSo_USD) as KinhPhiUSD, sum(NDCT.fQTKinhPhiDuocCap_TongSo_VND) as KinhPhiVND
		from NH_QT_QuyetToanNienDo_ChiTiet NDCT group by NDCT.iID_HopDongID)
	QTND on QTND.iID_HopDongID = hd.ID
left join (
		select ThanhToan.iLoaiDeNghi , ThanhToan.iID_HopDongID , sum(ThanhToan.fChuyenKhoan_BangSo) as KinhPhiDaChiUSD from NH_TT_ThanhToan ThanhToan
		join NH_TT_ThanhToan_ChiTiet b 
		on ThanhToan.ID = b.iID_DeNghiThanhToanID 
		where ThanhToan.iLoaiNoiDungChi = 1 and ThanhToan.dNgayDeNghi > convert(datetime,(concat(year(GETDATE()),'-1-1 00:00:00.000'))) and 
			((ThanhToan.dNgayDeNghi < convert(datetime,(concat(year(GETDATE()),'-9-30 00:00:00.000'))) and DATEPART(quarter,GETDATE()) = 4)
			or (ThanhToan.dNgayDeNghi < convert(datetime,(concat(year(GETDATE()),'-6-30 00:00:00.000'))) and DATEPART(quarter,GETDATE()) = 3)
			or (ThanhToan.dNgayDeNghi < convert(datetime,(concat(year(GETDATE()),'-3-31 00:00:00.000'))) and DATEPART(quarter,GETDATE()) = 2)
			)
		Group BY ThanhToan.iLoaiDeNghi, ThanhToan.iID_HopDongID having ThanhToan.iLoaiDeNghi = 2 or ThanhToan.iLoaiDeNghi = 3
	) KPDCToY on KPDCToY.iID_HopDongID = hd.ID
left join (
		select ThanhToan.iLoaiDeNghi , ThanhToan.iID_HopDongID , sum(ThanhToan.fChuyenKhoan_BangSo) as KinhPhiDaChiVND from NH_TT_ThanhToan ThanhToan
		join NH_TT_ThanhToan_ChiTiet b 
		on ThanhToan.ID = b.iID_DeNghiThanhToanID 
		where ThanhToan.iLoaiNoiDungChi = 2 and ThanhToan.dNgayDeNghi > convert(datetime,(concat(year(GETDATE()),'-1-1 00:00:00.000'))) and 
			((ThanhToan.dNgayDeNghi < convert(datetime,(concat(year(GETDATE()),'-9-30 00:00:00.000'))) and DATEPART(quarter,GETDATE()) = 4)
			or (ThanhToan.dNgayDeNghi < convert(datetime,(concat(year(GETDATE()),'-6-30 00:00:00.000'))) and DATEPART(quarter,GETDATE()) = 3)
			or (ThanhToan.dNgayDeNghi < convert(datetime,(concat(year(GETDATE()),'-3-31 00:00:00.000'))) and DATEPART(quarter,GETDATE()) = 2)
			)
		Group BY ThanhToan.iLoaiDeNghi, ThanhToan.iID_HopDongID having ThanhToan.iLoaiDeNghi = 2 or ThanhToan.iLoaiDeNghi = 3
	) KPDCVNDToY on KPDCVNDToY.iID_HopDongID = hd.ID
left join (
		select ThanhToan.iLoaiDeNghi , ThanhToan.iID_HopDongID , sum(ThanhToan.fChuyenKhoan_BangSo) as KinhPhiDaChiUSD from NH_TT_ThanhToan ThanhToan
		join NH_TT_ThanhToan_ChiTiet b 
		on ThanhToan.ID = b.iID_DeNghiThanhToanID 
		where ThanhToan.iLoaiNoiDungChi = 1 and ThanhToan.iNamKeHoach = DATEPART(YEAR,GETDATE()) and ThanhToan.dNgayDeNghi < convert(datetime,(concat(year(GETDATE())-1,'-12-31 00:00:00.000')))
		Group BY ThanhToan.iLoaiDeNghi, ThanhToan.iID_HopDongID having ThanhToan.iLoaiDeNghi = 2 or ThanhToan.iLoaiDeNghi = 3
	) KPDC on KPDC.iID_HopDongID = hd.ID
left join (
		select ThanhToan.iLoaiDeNghi , ThanhToan.iID_HopDongID , sum(ThanhToan.fChuyenKhoan_BangSo) as KinhPhiDaChiVND from NH_TT_ThanhToan ThanhToan
		join NH_TT_ThanhToan_ChiTiet b 
		on ThanhToan.ID = b.iID_DeNghiThanhToanID 
		where ThanhToan.iLoaiNoiDungChi = 2 and ThanhToan.iNamKeHoach = DATEPART(YEAR,GETDATE()) and ThanhToan.dNgayDeNghi < convert(datetime,(concat(year(GETDATE())-1,'-12-31 00:00:00.000')))
		Group BY ThanhToan.iLoaiDeNghi, ThanhToan.iID_HopDongID having ThanhToan.iLoaiDeNghi = 2 or ThanhToan.iLoaiDeNghi = 3
	) KPDCVND on KPDCVND.iID_HopDongID = hd.ID
left join (
	Select NDCT.iID_HopDongID, Sum(NDCT.fQTKinhPhiDuocCap_TongSo_USD) as KinhPhiToYUSD, sum(NDCT.fQTKinhPhiDuocCap_TongSo_VND) as KinhPhiToYVND
		from NH_QT_QuyetToanNienDo_ChiTiet NDCT
		left join NH_QT_QuyetToanNienDo NDCTParent on NDCTParent.ID = NDCT.iID_QuyetToanNienDoID
		Where NDCTParent.iNamKeHoach = DATEPART(YEAR,GETDATE())
		group by NDCT.iID_HopDongID) 
		QTNDToY on QTNDToY.iID_HopDongID = hd.ID

		Where ((@tabindex = 0 and ( (@iQuyPrint = -2 or (Month(tt.dNgayDeNghi) >= (@iQuyPrint - 1 )*3 + 1)) and (@iTuNamPrint = -2 or ( 
(Year(tt.dNgayDeNghi) >=  @iTuNamPrint))) )) or (@tabindex = 1 and 
((@iTuNamPrint = -2 or Year(tt.dNgayDeNghi) >=  @iTuNamPrint ) and (@iDenNamPrint = -2 or Year(tt.dNgayDeNghi) <=  @iDenNamPrint ))
)) and (tt.iID_DonVi = @iDonvi or @iDonvi is null or @iDonvi = '00000000-0000-0000-0000-000000000000')
Order by nvc.ID desc , dm_nvc.sTenNhiemVuChi , da.ID desc , da.sTenDuAn, tt.iLoaiNoiDungChi ,hd.ID , hd.sTenHopDong

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_thang_dulieu_insert]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_tl_bangluong_thang_dulieu_insert] 
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
				CASE WHEN PARENT <> N'TIENAN' THEN 0
					WHEN canBoPhuCap.MA_PHUCAP IN ('TA_BB_DG', 'TA_TRUCTRAI_DG') THEN ISNULL(@SoNgay, 0) * canBoPhuCap.GIA_TRI 
					ELSE ISNULL(canBoPhuCap.HuongPC_SN, 0) * canBoPhuCap.GIA_TRI
				END
			) GiaTriTA1,
			SUM (
				CASE WHEN PARENT <> N'TIENAN2' THEN 0
					WHEN canBoPhuCap.MA_PHUCAP IN ('TA_BB_DG', 'TA_TRUCTRAI_DG') THEN ISNULL(@SoNgay, 0) * canBoPhuCap.GIA_TRI 
					ELSE ISNULL(canBoPhuCap.HuongPC_SN, 0) * canBoPhuCap.GIA_TRI
				END
			) GiaTriTA2
		FROM TL_CanBo_PhuCap canBoPhuCap
		INNER JOIN TL_DM_PhuCap as pc on canBoPhuCap.MA_PHUCAP = pc.Ma_PhuCap
		--WHERE
			--canBoPhuCap.MA_PHUCAP IN ('TA_TRUCQY_DG1', 'TA_TRUCQY_DG2', 'TA_TRUCQY_DG3', 'TA_TRUCQY_DG4', 'TA_DOCHAI_DG', 'TA_OM_DG', 'TA_BB_DG', 'TA_TRUCTRAI_DG')
		WHERE 
			pc.PARENT IN ('TIENAN', 'TIENAN2')
		GROUP BY canBoPhuCap.MA_CBO
	),

	ThongTinCanBo AS (
		SELECT
			canBo.Ma_CanBo AS MaCanBo,
			canBo.Ten_CanBo AS TenCanBo,
			donVi.Ma_DonVi MaDonVi,
			canBo.Ma_Hieu_CanBo AS MaHieuCanBo,
			capBac.Ma_Cb MaCapBac,
			canBo.BHTN,
			canBo.PCCV
		FROM TL_DM_CanBo canBo
		INNER JOIN TL_DM_DonVi donVi
			ON canBo.Parent = donVi.Ma_DonVi
		INNER  JOIN TL_DM_CapBac capBac
			ON canBo.Ma_CB = capBac.Ma_Cb
		WHERE
			canBo.Thang = @Thang
			AND canBo.Nam = @Nam
			AND canBo.Parent IN (SELECT * FROM f_split(@MaDonVi))
			AND canBo.IsDelete = 1
			AND canBo.Khong_Luong <> 1
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
			WHEN (canBoPhuCap.MA_PHUCAP = 'TCVIECLAM_TT' AND cb.Parent in ('1', '2', '3')) THEN 0 
			WHEN (canBoPhuCap.MA_PHUCAP = 'NTN' AND canBoPhuCap.GIA_TRI < 5) OR (canBoPhuCap.MA_PHUCAP = 'BHTNCN_HS' AND canBo.BHTN = 0) OR (canBoPhuCap.MA_PHUCAP = 'PCCOV_HS' AND canBo.PCCV = 0) THEN 0
			WHEN canBoPhuCap.MA_PHUCAP = 'TA_TT' THEN soLieuTienAn.GiaTriTA1
			WHEN canBoPhuCap.MA_PHUCAP = 'TA_TT2' THEN soLieuTienAn.GiaTriTA2
			WHEN canBoPhuCap.MA_PHUCAP = 'TRICHLUONG_TT' THEN (canBoPhuCap.GIA_TRI / 1000) * 1000
			ELSE canBoPhuCap.GIA_TRI
		END						AS GiaTri,
		canBo.MaHieuCanBo		AS MaHieuCanBo,
		canBo.MaCapBac			AS MaCb,
		canBoPhuCap.HuongPC_SN	AS HuongPcSn,
		cachTinhLuong.CongThuc	AS CongThuc,
		CASE WHEN cachTinhLuong.Id IS NULL THEN 1 ELSE 0 END AS IsCalculated
	FROM TL_CanBo_PhuCap canBoPhuCap
	INNER JOIN ThongTinCanBo canBo
		ON canBo.MaCanBo = canBoPhuCap.MA_CBO
	LEFT JOIN SoLieuTienAn soLieuTienAn
		ON canBoPhuCap.MA_CBO = soLieuTienAn.MaCanBo
	LEFT JOIN TL_DM_Cach_TinhLuong_Chuan cachTinhLuong
		ON cachTinhLuong.Ma_Cot = canBoPhuCap.MA_PHUCAP
	left join TL_DM_CapBac cb on canBo.MaCapBac = cb.Ma_Cb
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_chungtu_chitiet]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_tl_chungtu_chitiet]
	@maDonVi VARCHAR(MAX),
	@thang INT,
	@nam INT,
	@maCachTl NVARCHAR(50)
AS
BEGIN
	
SELECT Ma_Cot, pc.Ma_PhuCap INTO #tmpMapping
	FROM 
	(SELECT Ma_Cot, CONCAT('|', REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(CAST(CongThuc as nvarchar(max)),'+','|'),'-','|'),'*','|'),'/','|'),'(','|'),')','|'), '|') as CongThuc FROM TL_DM_Cach_TinhLuong_Chuan WHERE CongThuc IS NOT NULL) as tbl
	INNER  JOIN TL_DM_PhuCap as pc on tbl.CongThuc LIKE CONCAT('%|', pc.Ma_PhuCap, '|%') ;

	SELECT mp.Ma_Cot, cb.Ma_CanBo, SUM(ISNULL(pc.HuongPC_SN, 0)) as HuongPC_SN INTO #tmpSoNgay
	FROM TL_DM_CanBo as cb
	INNER JOIN TL_CanBo_PhuCap as pc on cb.Ma_CanBo = pc.MA_CBO
	INNER JOIN #tmpMapping as mp on pc.MA_PHUCAP = mp.Ma_PhuCap
	WHERE cb.Parent in (SELECT * FROM f_split(@MaDonVi))
	AND Thang = @thang
	AND Nam = @nam
	GROUP BY mp.Ma_Cot, cb.Ma_CanBo;

	WITH LuongCapBac AS (
		SELECT
			dsCapNhapBangLuong.Ma_CBo	AS MaDonVi,
			dsCapNhapBangLuong.Thang	AS Thang,
			dsCapNhapBangLuong.Nam		AS Nam,
			bangLuong.MA_PHUCAP			AS MaPhuCap,
			bangLuong.Ma_CB				AS MaCapBac,
			capBac.Parent				AS Ngach,
			SUM(ISNULL(cbpc.HuongPC_SN, 0)) AS SoNgay,
			SUM(
				CASE WHEN pc.Ma_PhuCap IN ('TA_BB_DG', 'TA_TRUCTRAI_DG') THEN (dbo.fnTotalDayOfMonth(@thang,@nam)*bangLuong.Gia_Tri)
					WHEN pc.Parent IN ('TIENAN', 'TIENAN2') THEN ISNULL(cbpc1.HuongPC_SN, 0) * bangLuong.Gia_Tri
					ELSE bangLuong.Gia_Tri END
			)		AS GiaTri,
			COUNT(bangLuong.Ma_CBo)		AS SoNguoi
		FROM TL_BangLuong_Thang bangLuong
		INNER JOIN TL_CanBo_PhuCap as cbpc1 on bangLuong.Ma_CBo = cbpc1.MA_CBO AND bangLuong.Ma_PhuCap = cbpc1.MA_PHUCAP
		INNER JOIN TL_DM_PhuCap as pc on bangLuong.Ma_PhuCap = pc.Ma_PhuCap
		LEFT JOIN #tmpSoNgay  as cbpc on bangLuong.Ma_PhuCap = cbpc.Ma_Cot AND bangLuong.Ma_CBo = cbpc.Ma_CanBo
		JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong
			ON bangLuong.parent = dsCapNhapBangLuong.Id
		JOIN TL_DM_CapBac capBac
			ON bangLuong.Ma_CB = capBac.Ma_Cb
		WHERE
			dsCapNhapBangLuong.Ma_CachTL IN (SELECT * FROM f_split(@maCachTl))
			AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(@MaDonVi))
			AND dsCapNhapBangLuong.Thang=@Thang
			AND dsCapNhapBangLuong.Nam=@Nam
			AND dsCapNhapBangLuong.Status=1
			AND bangLuong.Gia_Tri != 0
		GROUP BY dsCapNhapBangLuong.Ma_CBo, dsCapNhapBangLuong.Thang, dsCapNhapBangLuong.Nam, bangLuong.MA_PHUCAP,bangLuong.Ma_CB, capBac.Parent
	), LuongCapBacMlns AS (
		SELECT
			luongCapBac.MaDonVi,
			phuCapMlns.XauNoiMa,
			SoNguoi,
			SoNgay,
			GiaTri
		FROM TL_PhuCap_MLNS phuCapMlns
		JOIN LuongCapBac luongCapBac ON phuCapMlns.Ma_Cb = luongCapBac.Ngach AND phuCapMlns.Ma_PhuCap = luongCapBac.MaPhuCap
		WHERE
			phuCapMlns.Nam = @nam
	),

	DataDuToan as (
		Select Ma_DonVi, XauNoiMa, SUM(DDuToan) DuToan
		From TL_QT_ChungTuChiTiet ctchitiet
		Join TL_QT_ChungTu chungtu
		on chungtu.ID = ctchitiet.Id_ChungTu
		Where Nam = @nam
		And Ma_DonVi IN (SELECT * FROM f_split(@maCachTl))
		Group By XauNoiMa, Ma_DonVi
	)

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
		ISNULL(dataDuToan.DuToan, 0)	AS DDuToan
	FROM NS_MucLucNganSach mlns
	JOIN LuongCapBacMlns luong
		ON mlns.sXauNoiMa = luong.XauNoiMa
	LEFT JOIN DataDuToan dataDuToan
		ON luong.XauNoiMa = dataDuToan.XauNoiMa and luong.MaDonVi = dataDuToan.Ma_DonVi
	WHERE
		sLNS IN ('1', '101', '1010000')
		AND iNamLamViec = @nam
	GROUP BY MaDonVi, iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, iNamLamViec, sChiTietToi, bHangCha, DuToan
	ORDER BY MaDonVi, sXauNoiMa

	DROP TABLE #tmpSoNgay
	DROP TABLE #tmpMapping
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_chungtu_chitiet_namkehoach]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_tl_chungtu_chitiet_namkehoach]
@maDonVi varchar(50), @thang int, @nam int

As
select iID_MLNS as MlnsId, 
	   iID_MLNS_Cha as MlnsIdParent, 
	   sXauNoiMa as XauNoiMa, 
	   sLNS as Lns, 
	   sL as L, 
	   sK as K, 
	   sM as M, 
	   sTM as TM, 
	   sTTM as TTM, 
	   sNG as Ng, 
	   sTNG as TNG, 
	   sTNG1 as TNG1, 
	   sTNG2 as TNG2, 
	   sTNG3 as TNG3, 
	   sMoTa as Mota, 
	   iNamLamViec as NamLamViec,
	   bHangCha as BHangCha,
	   sChiTietToi as ChiTietToi,
	   Sum(Tong) as TongCong,
	   NULL AS DDuToan,
	   CAST(0 as int) as SoNgay
from NS_MucLucNganSach
left join
(select TL_PhuCap_MLNS.Ma_PhuCap, XauNoiMa, MoTa, TL_PhuCap_MLNS.Ma_Cb, Ma_DonVi, THANG, Luong_CapBac.NAM, SUM(Gia_Tri) as Tong
from TL_PhuCap_MLNS
join
(select Ma_PhuCap, Ma_DonVi, THANG, NAM, TL_BangLuong_KeHoach.Ma_CB, TL_DM_CapBac.Parent, Gia_Tri
from [dbo].[TL_BangLuong_KeHoach]
Join [dbo].[TL_DM_CapBac]
On TL_BangLuong_KeHoach.Ma_CB = TL_DM_CapBac.Ma_Cb) as Luong_CapBac
on TL_PhuCap_MLNS.Ma_Cb = Luong_CapBac.Parent and TL_PhuCap_MLNS.Ma_PhuCap = Luong_CapBac.Ma_PhuCap
Where Ma_DonVi = @maDonVi
And THANG = @thang
And Luong_CapBac.NAM = @nam
And TL_PhuCap_MLNS.Nam = @nam
group by TL_PhuCap_MLNS.Ma_PhuCap, XauNoiMa, MoTa, TL_PhuCap_MLNS.Ma_Cb, Ma_DonVi, THANG, Luong_CapBac.NAM) as Luong_Capbac_Mlns
on NS_MucLucNganSach.sXauNoiMa = Luong_Capbac_Mlns.XauNoiMa
Where sLNS IN ('1', '101', '1010000')
And iNamLamViec = @nam
group by iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, iNamLamViec,sChiTietToi, bHangCha
order by sXauNoiMa
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_ct_chi_tiet]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_tl_get_data_ct_chi_tiet] @idChungTu nvarchar(MAX),
                                                       @nam int, @maCachTl nvarchar(50) AS BEGIN

with ctct as (
  select Id as Id, XauNoiMa,MaCachTl,  Sum(TongCong) AS TongCong,
       convert(decimal,Sum(SoNguoi)) as SoNguoi,
	   convert(decimal,Sum(SoNgay)) as SoNgay,
       Sum(DieuChinh) AS DieuChinh,
     Sum(DDuToan) As DDuToan
  from TL_QT_ChungTuChiTiet 
  where   Id_ChungTu in (SELECT *  FROM f_split(@idChungTu))
    AND MaCachTl in (SELECT *  FROM f_split(@maCachTl))
  group by id, XauNoiMa, MaCachTl
)
SELECT 
     ctct.Id as Id,
     iID_MLNS AS MlnsId,
       iID_MLNS_Cha AS MlnsIdParent,
       sXauNoiMa AS XauNoiMa,
       sLNS AS Lns,
       sL AS L,
       sK AS K,
       sM AS M,
       sTM AS TM,
       sTTM AS TTM,
       sNG AS Ng,
       sTNG AS TNG,
       sTNG1 AS TNG1,
       sTNG2 AS TNG2,
       sTNG3 AS TNG3,
       sMoTa AS Mota,
     MaCachTl as MaCachTl,
       iNamLamViec AS NamLamViec,
       mlns.bHangCha AS BHangCha,
       sChiTietToi AS ChiTietToi,
       TongCong,
    SoNguoi,
       DieuChinh,
     DDuToan, 
	 SoNgay
FROM NS_MucLucNganSach mlns  
LEFT JOIN ctct ctct ON mlns.sXauNoiMa = ctct.XauNoiMa

WHERE sLNS IN ('1',
               '101',
               '1010000')
  AND iNamLamViec = 2022

order by mlns.sXauNoiMa

/*
SELECT 
     --ctct.Id as Id,
     iID_MLNS AS MlnsId,
       iID_MLNS_Cha AS MlnsIdParent,
       sXauNoiMa AS XauNoiMa,
       sLNS AS Lns,
       sL AS L,
       sK AS K,
       sM AS M,
       sTM AS TM,
       sTTM AS TTM,
       sNG AS Ng,
       sTNG AS TNG,
       sTNG1 AS TNG1,
       sTNG2 AS TNG2,
       sTNG3 AS TNG3,
       sMoTa AS Mota,
     MaCachTl as MaCachTl,
       iNamLamViec AS NamLamViec,
       mlns.bHangCha AS BHangCha,
       sChiTietToi AS ChiTietToi,
       Sum(TongCong) AS TongCong,
       convert(decimal,Sum(SoNguoi)) as SoNguoi,
       Sum(DieuChinh) AS DieuChinh,
     Sum(DDuToan) As DDuToan
FROM NS_MucLucNganSach mlns
LEFT JOIN TL_QT_ChungTuChiTiet ctct ON mlns.sXauNoiMa = ctct.XauNoiMa
AND ctct.Id_ChungTu in (SELECT *
   FROM f_split(@idChungTu))
AND ctct.MaCachTl in
  (SELECT *
   FROM f_split(@maCachTl))
WHERE sLNS IN ('1',
               '101',
               '1010000')
  AND iNamLamViec = @nam
GROUP BY 
     --ctct.Id,
     iID_MLNS,
         iID_MLNS_Cha,
         sXauNoiMa,
         sLNS,
         sL,
         sK,
         sM,
         sTM,
         sTTM,
         sNG,
         sTNG,
         sTNG1,
         sTNG2,
         sTNG3,
         sMoTa,
         sChiTietToi,
         mlns.bHangCha,
         iNamLamViec,
     MaCachTl
ORDER BY sXauNoiMa
*/


END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_ct_chi_tiet_export]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_tl_get_data_ct_chi_tiet_export]
	@lstId nvarchar(max),
	@lstCach nvarchar(100)
AS
BEGIN
	CREATE TABLE #tmp(id nvarchar(100))
	DECLARE @isHaveCachTinhLuong bit = 0

	if(ISNULL(@lstCach, '') <> '')
	BEGIN
		INSERT INTO #tmp(id)
		SELECT *
		FROM f_split(@lstCach)

		SET @isHaveCachTinhLuong = 1
	END

	SELECT tbl.ID, Thang INTO #tblMaxThang
	FROM f_split(@lstId) as tmp
	INNER JOIN TL_QT_ChungTu as tbl on tmp.Item = tbl.ID;

	with ctct as (
	  select XauNoiMa,MaCachTl,  Sum(TongCong) AS TongCong,
		   Sum(DieuChinh) AS DieuChinh,
		 Sum(ISNULL(dt.DDuToan, 0)) As DDuToan
	  from TL_QT_ChungTu as tbl
	  INNER JOIN TL_QT_ChungTuChiTiet as dt on tbl.Id = dt.Id_ChungTu
	  where  (ISNULL(@lstId, '') <> '' AND tbl.ID in (SELECT *  FROM f_split(@lstId)))
		AND ((dt.MaCachTl = '' AND @isHaveCachTinhLuong = 0) OR (@isHaveCachTinhLuong = 1 AND dt.MaCachTl in (SELECT id  FROM #tmp)))
	  group by dt.XauNoiMa, dt.MaCachTl
	),
	lstSoNguoi as (
		SELECT XauNoiMa,
			SoNguoi,SoNgay
		from TL_QT_ChungTu as tbl
		INNER JOIN TL_QT_ChungTuChiTiet as dt on tbl.Id = dt.Id_ChungTu
		where tbl.ID in (SELECT TOP(1) ID FROM #tblMaxThang ORDER BY Thang DESC)
		AND ((@isHaveCachTinhLuong = 0 AND dt.MaCachTl = '') OR (@isHaveCachTinhLuong = 1 AND dt.MaCachTl in (SELECT id  FROM #tmp)))
	)
SELECT 
     NEWID() as Id,
     iID_MLNS AS MlnsId,
       iID_MLNS_Cha AS MlnsIdParent,
       sXauNoiMa AS XauNoiMa,
       sLNS AS Lns,
       sL AS L,
       sK AS K,
       sM AS M,
       sTM AS TM,
       sTTM AS TTM,
       sNG AS Ng,
       sTNG AS TNG,
       sTNG1 AS TNG1,
       sTNG2 AS TNG2,
       sTNG3 AS TNG3,
       sMoTa AS Mota,
     MaCachTl as MaCachTl,
       iNamLamViec AS NamLamViec,
       mlns.bHangCha AS BHangCha,
       sChiTietToi AS ChiTietToi,
       TongCong,
    SoNguoi,
	SoNgay,
       DieuChinh,
     DDuToan
FROM NS_MucLucNganSach mlns  
LEFT JOIN ctct ctct ON mlns.sXauNoiMa = ctct.XauNoiMa
LEFT JOIN lstSoNguoi as sn on mlns.sXauNoiMa = sn.XauNoiMa

WHERE sLNS IN ('1',
               '101',
               '1010000')
  AND iNamLamViec = 2022

order by mlns.sXauNoiMa

DROP TABLE #tblMaxThang
DROP TABLE #tmp
END
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_phucap]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_tl_get_data_phucap] @maCanBo AS nvarchar(50) AS BEGIN

CREATE TABLE #tmpExclude(code nvarchar(200))
INSERT INTO #tmpExclude(code) VALUES('THUONG_TT'),('GIAMTHUE_TT'),('THUNHAPKHAC_TT'),('THUEDANOP_TT'),('TIENCTLH_TT'),('TIENANDUONG_TT'),('TIENTAUXE_TT')

SELECT PhuCap.Id AS Id,
       PhuCap.Ma_PhuCap AS MaPhuCap,
       PhuCap.Parent AS Parent,
	   PhuCap.bGiaTri as BGiaTri,
	   PhuCap.bHuongPc_Sn as BHuongPcSn,
	   PhuCap.Ten_PhuCap AS TenPhuCap,
       CONCAT(PhuCapCha.Ma_PhuCap, '-', PhuCapCha.Ten_PhuCap) AS ParentName,
       CanboPhucap.DateStart AS DateStart,
       CanboPhucap.ISoThang_Huong AS ISoThangHuong,
       CanboPhucap.HuongPC_SN AS HuongPCSN,
     (case
     when CanboPhucap.GIA_TRI is null then PhuCap.Gia_Tri
     else CanboPhucap.GIA_TRI
     end) as GiaTri,
	 PhuCap.FGiaTriNhoNhat,
	 PhuCap.FGiaTriLonNhat,
	 PhuCap.fGiaTriPhuCap_KemTheo as FGiaTriPhuCapKemTheo,
	 PhuCap.iId_PhuCap_KemTheo as IIdPhuCapKemTheo,
	 PhuCap.iId_Ma_PhuCap_KemTheo as IIdMaPhuCapKemTheo
FROM TL_DM_PhuCap PhuCap
LEFT JOIN #tmpExclude as ec on PhuCap.Ma_PhuCap = ec.code
LEFT JOIN TL_DM_PhuCap PhuCapCha ON PhuCap.Parent = PhuCapCha.Ma_PhuCap
LEFT JOIN TL_CanBo_PhuCap AS CanboPhucap ON PhuCap.Ma_PhuCap = CanboPhucap.MA_PHUCAP
AND CanboPhucap.MA_CBO = @maCanBo
WHERE PhuCap.Chon = 1
  AND PhuCap.Is_Formula = 0
  AND PhuCap.Is_Readonly = 0
  AND PhuCap.Parent IN ( select Ma_PhuCap from TL_DM_PhuCap where Parent = '' and Chon = 1)
  AND  ec.code IS NULL
Order By ParentName
end
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangluong_thang_giaithich_chitiet_theo_ngach_capbac]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		TungNH
-- Create date: 25/04/2022
-- Description:	Báo cáo giải thích chi tiết lương theo ngạch, cấp bậc
-- =============================================
CREATE PROCEDURE [dbo].[sp_tl_rpt_bangluong_thang_giaithich_chitiet_theo_ngach_capbac] 
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
			bangLuong.Ma_CBo			AS MaCanBo,
			bangLuong.MA_PHUCAP			AS MaPhuCap,
			bangLuong.GIA_TRI			AS GiaTri
		FROM TL_BangLuong_Thang bangLuong
		JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong
			ON bangLuong.parent = dsCapNhapBangLuong.Id
		WHERE
			bangLuong.Ma_PhuCap IN (SELECT * FROM f_split(''' + @MaPhuCap + '''))
			AND bangLuong.Gia_Tri <> 0
			AND dsCapNhapBangLuong.Ma_CachTL=''' + @MaCachTl + '''
			AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
			AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND dsCapNhapBangLuong.Status=1
	), ThongTinCanBo AS (
		SELECT
			canBo.Ma_CanBo		AS MaCanBo,
			canBo.Ten_CanBo		AS TenCanBo,
			donVi.Ma_DonVi		AS MaDonVi,
			canBo.Ma_Hieu_CanBo	AS MaHieuCanBo,
			capBac.Ma_Cb			AS MaCapBac,
			CASE
				WHEN capBac.XauNoiMa LIKE ''1%'' OR capBac.XauNoiMa LIKE ''4%'' THEN capBac.Lht_Hs
				ELSE canBo.HeSoLuong
			END AS HeSoLuong
		FROM TL_DM_CanBo canBo
		INNER JOIN TL_DM_DonVi donVi
			ON canBo.Parent = donVi.Ma_DonVi
		INNER JOIN TL_DM_CapBac capBac
			ON canBo.Ma_CB = capBac.Ma_Cb
		WHERE
			canBo.IsDelete = 1
			AND canBo.Khong_Luong = 0
			AND canBo.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND canBo.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND canBo.Parent IN (SELECT * FROM f_split( ''' + @MaDonVi + '''))
	), SoLieuBaoCao AS (
		SELECT
			CASE WHEN ' + CAST(@IsSummary AS VARCHAR(1)) + ' = 1 THEN NULL ELSE canBo.MaDonVi END	AS MaDonVi,
			canBo.MaCapBac																			AS MaCapBac,
			canBo.HeSoLuong																			AS HeSoLuong,
			bangLuongThang.MaPhuCap																	AS MaPhuCap,
			COUNT(canBo.MaCanBo)																	AS SoNguoi,
			SUM(bangLuongThang.GiaTri) / ' + CAST(@DonViTinh AS VARCHAR(100)) + '					AS GiaTri
		FROM ThongTinCanBo canBo
		INNER JOIN BangLuongThang bangLuongThang
			ON bangLuongThang.MaCanBo = canBo.MaCanBo
		GROUP BY MaDonVi, MaCapBac, HeSoLuong, MaPhuCap
	), CanBoLuongCapBac AS (
		SELECT
			bangLuong.Ma_CB			AS MaCapBac,
			bangLuong.GIA_TRI		AS HeSoLuong
		FROM TL_BangLuong_Thang bangLuong
		JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong
			ON bangLuong.parent = dsCapNhapBangLuong.Id
		WHERE
			bangLuong.Ma_PhuCap = ''LHT_HS''
			AND bangLuong.Gia_Tri <> 0
			AND dsCapNhapBangLuong.Ma_CachTL=''' + @MaCachTl + '''
			AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
			AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND dsCapNhapBangLuong.Status=1
	)

	SELECT ''' + @MaCachTl + ''' AS CachTl, (SELECT COUNT(*) FROM CanBoLuongCapBac canBo INNER JOIN TL_DM_CapBac as cb on canBo.MaCapBac = cb.Ma_Cb WHERE canBo.MaCapBac = pvt.MaCapBac AND ((cb.XauNoiMa LIKE ''1%'' OR cb.XauNoiMa LIKE ''4%'') OR canBo.HeSoLuong = pvt.HeSoLuong)) AS SoNguoi, pvt.* FROM (
		SELECT
			MaDonVi,
			MaCapBac,
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
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_2]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_2]
@MaDonVi NVARCHAR(max),
@Thang int,
@Nam AS int,
@IsOrderChucVu AS bit = 0,
@IsGiaTriAm AS bit = 0
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;

DECLARE @Cols AS NVARCHAR(MAX)
DECLARE @Query AS NVARCHAR(MAX)

SET @Cols = 'NTN,LHT_HS,PCTNVK_HS,HSBL_HS,LHT_TT,PCTNVK_TT,HSBL_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCTRA_SUM,PCCOV_TT,PCDACTHU_SUM,PCKHAC_SUM,LUONGTHANG_SUM,BHCN_TT,THUETNCN_TT,TA_TT,GTKHAC_TT,TRICHLUONG_TT,PHAITRU_SUM,TM,THANHTIEN,BHXHCN_TT,BHYTCN_TT,BHTNCN_TT,PCTAUBP_TT,PCBVBG_TT,PCTEMTHU_TT,TA_TONG,PHAITRUKHAC_SUM'
SET @Query =
'
WITH BangLuongThang AS (
SELECT Thang, Nam, MaCanBo, ' + @Cols + ' FROM (
SELECT
dsCapNhapBangLuong.Thang AS Thang,
dsCapNhapBangLuong.Nam AS Nam,
bangLuong.Ma_CBo AS MaCanBo,
bangLuong.MA_PHUCAP AS MaPhuCap,
bangLuong.GIA_TRI AS GiaTri
FROM TL_BangLuong_Thang bangLuong
JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong
ON bangLuong.parent = dsCapNhapBangLuong.Id
WHERE
bangLuong.Ma_PhuCap IN (SELECT * FROM f_split(''' + @Cols + '''))
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
FROM TL_DM_CanBo canBo
INNER JOIN TL_DM_DonVi donVi
ON canBo.Parent=donVi.Ma_DonVi
LEFT JOIN TL_DM_CapBac capBac
ON canBo.Ma_CB=capBac.Ma_Cb
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
If @IsGiaTriAm = 1
SET @Query = @Query + ' WHERE bangLuong.THANHTIEN < 0';
IF @IsOrderChucVu = 1
SET @Query = @Query +' ORDER BY HSChucVu DESC, MaCapBac DESC, Ten ASC';
ELSE
SET @Query = @Query +' ORDER BY MaCapBac DESC, Ten ASC';
execute(@Query)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_danhsach_capphat_phucap]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TungNH
-- Create date: 09/12/2021
-- Description:	Lấy dữ liệu báo cáo bảng lương tháng
-- =============================================
CREATE PROCEDURE [dbo].[sp_tl_rpt_danhsach_capphat_phucap]
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

	SET @Cols = 'NTN,LHT_HS,LHT_TT,PCCV_TT,PCTHD_TT,PCKV_TT,PCCOV_TT,PCDACTHU_SUM,PCKHAC_SUM,PCTRA_SUM,LUONGTHANG_SUM,PHAITRU_SUM,TM,THANHTIEN,PCNU_TT,HSBL_HS,PCTNVK_HS'
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
			bangLuong.Ma_PhuCap IN (SELECT * FROM f_split(''' + @Cols + '''))
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
			ISNULL(canBo.Ma_CB, ''0'') AS MaCapBac,
			ISNULL(capBac.Ten_Cb, ''0'') AS CapBac,
			ISNULL(chucVu.HeSo_Cv, 0) AS HSChucVu,
			chucVu.Ma_Cv AS MaChucVu,
			chucVu.Ten_Cv AS TenChucVu,
			CONCAT(canBo.So_TaiKhoan, '' '', canBo.Ma_CanBo) AS Stk,
			ISNULL(FORMAT(canBo.Ngay_NN,''MM/yy''), '''') AS NgayNhapNgu,
			ISNULL(FORMAT(canBo.Ngay_XN,''MM/yy''), '''') AS NgayXuatNgu,
			ISNULL(FORMAT(canBo.Ngay_TN,''MM/yy''), '''') AS NgayTaiNgu,
			canBo.Ngay_NN AS NgayNhapNguDate,
			canBo.Ngay_XN AS NgayXuatNguDate,
			canBo.Ngay_TN AS NgayTaiNguDate,
			CASE WHEN canBo.Thang_TNN IS NULL THEN 0 ELSE canBo.Thang_TNN END AS ThangTnn,
			capBac.XauNoiMa
		FROM TL_DM_CanBo canBo
		INNER JOIN TL_DM_DonVi donVi
			ON canBo.Parent=donVi.Ma_DonVi
		LEFT JOIN TL_DM_CapBac capBac
			ON canBo.Ma_CB=capBac.Ma_Cb
		LEFT JOIN TL_DM_ChucVu chucVu
			ON canBo.Ma_CV=chucVu.Ma_Cv
		WHERE
			canBo.IsDelete = 1
			AND canBo.Khong_Luong = 0
			AND canBo.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND canBo.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
	)
	SELECT MaDonVi, MaCanBo, TenCanBo, Ten, Tnn, MaCapBac, CapBac, HSChucVu, MaChucVu, TenChucVu, Stk, NgayNhapNgu, NgayXuatNgu, NgayTaiNgu, ThangTnn, XauNoiMa, ' + @Cols + ' FROM (
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
			canBo.HSChucVu,
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
	WHERE MaCapBac LIKE ''0%''
	ORDER BY HSChucVu DESC, MaCapBac DESC, Ten ASC'
	execute(@Query)
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_bcdutoan_loaicongtrinh]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_bcdutoan_loaicongtrinh]
@iLoaiChungTu int,
@iNamKeHoach int,
@fDonViTinh float,
@DonViIds t_tbl_string READONLY
AS
BEGIN
	SELECT dt.iID_LoaiCongTrinh, 
		SUM(ISNULL((CASE WHEN tbl.Id <> tbl.iID_PhanBoGocID THEN dt.fCapPhatBangLenhChiDC ELSE dt.fCapPhatBangLenhChi END), 0)) as fCapPhatBangLenhChi,
		SUM(ISNULL((CASE WHEN tbl.Id <> tbl.iID_PhanBoGocID THEN dt.fCapPhatTaiKhoBacDC ELSE dt.fCapPhatTaiKhoBac END), 0)) as fCapPhatTaiKhoBac INTO #tmp
	FROM VDT_KHV_PhanBoVon as tbl
	INNER JOIN VDT_KHV_PhanBoVon_ChiTiet as dt on tbl.Id = dt.iID_PhanBoVonID
	INNER JOIN @DonViIds as dv on tbl.iID_MaDonViQuanLy = dv.sId
	WHERE tbl.iNamKeHoach = @iNamKeHoach AND
		((@iLoaiChungTu = 1 AND tbl.Id = tbl.iID_PhanBoGocID) OR (@iLoaiChungTu = 2 AND tbl.bActive = 1))
	GROUP BY dt.iID_LoaiCongTrinh

	SELECT lct.STenLoaiCongTrinh, 
		lct.iID_LoaiCongTrinh as IIDLoaiCongTrinh, 
		lct.iID_Parent as IIDParent, 
		ISNULL(lct.iThuTu, 0) as IThuTu,
		lct.L, lct.K, lct.M, lct.TM, lct.TTM, lct.NG,
		(ISNULL(tmp.fCapPhatBangLenhChi, 0)/ @fDonViTinh) as FCapPhatBangLenhChi,
		(ISNULL(tmp.fCapPhatTaiKhoBac, 0)/ @fDonViTinh) as FCapPhatTaiKhoBac,
		CAST(0 as bit) as BIsHangCha
	FROM VDT_DM_LoaiCongTrinh as lct
	LEFT JOIN #tmp as tmp on tmp.iID_LoaiCongTrinh = lct.iID_LoaiCongTrinh

	DROP TABLE #tmp
END

GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_find_phanbovondonvichitietpheduyet]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_vdt_find_phanbovondonvichitietpheduyet]
@iIdPhanBoVon uniqueidentifier,
@dNgayLap datetime
AS
BEGIN
	select 
		distinct
		--pbvct.Id,
		da.iID_DuAnID,
		--pbvct.iID_PhanBoVonID,
		da.sTenDuAn,
		da.sMaDuAn,
		da.sTrangThaiDuAn,
		da.sKhoiCong,
		da.sKetThuc,
		da.sMaKetNoi,
		da.iID_MaDonViThucHienDuAnID as IIdMaDonViThucHienDuAn,
		dv.sTenDonVi as STenDonViThucHienDuAn,
		lct.sTenLoaiCongTrinh,
		'' as sTenCapPheDuyet,
		cast(0 as float) as fGiaTriDauTu,
		pbvct.iID_LoaiCongTrinh as iID_LoaiCongTrinhID,
		null as iID_CapPheDuyetID,
		'' as sLNS,
		'' as sL,
		'' as sK,
		'' as sM,
		'' as sTM,
		'' as sTTM,
		'' as sNG,
		cast(0 as float) as fVonDaBoTri,
		cast(0 as float) as fVonConLai,
		cast(0 as float) as fChiTieuBoXungTrongNam,
		cast(0 as float) as fNamTruocChuyenSang,
		cast(0 as float) as fThuUngXDCB,
		cast(0 as float) as fChiTieuNganSach,
		pbvct.sGhiChu as sGhiChu,
		cast(0 as float) as fChiTieuGoc,
		cast(0 as float) as FCapPhatTaiKhoBac,
		cast(0 as float) as FCapPhatBangLenhChi,
		cast(0 as float) as FGiaTriThuHoiNamTruocKhoBac,
		cast(0 as float) as FGiaTriThuHoiNamTruocLenhChi,
		pbvct.fGiaTriThuHoi as FGiaTriThuHoi,
		pbvct.fGiaTrPhanBo as FGiaTrPhanBo,
		--isnull(pbvct.Id, NEWID()) as IIdPhanBoVonId,
		pbvct.iID_PhanBoVon_DonVi_PheDuyet_ID as IIdPhanBoVon,
		pbvct.ILoaiDuAn as ILoaiDuAn,
		pbv.Id as IdChungTu,
		pbv.iID_ParentId as IdChungTuParent,
		pbv.bActive as BActive,
		pbv.bIsGoc as IsGoc,
		case when pbvct.fThanhToanDeXuat is not null then pbvct.fThanhToanDeXuat else pbvdxct.fThanhToan end as fThanhToanDeXuat
	from
		VDT_KHV_PhanBoVon_DonVi_ChiTiet_PheDuyet pbvct
	inner join
		VDT_KHV_PhanBoVon_DonVi_PheDuyet pbv
	on pbvct.iID_PhanBoVon_DonVi_PheDuyet_ID = pbv.Id
	inner join 
	VDT_KHV_PhanBoVon_DonVi_ChiTiet pbvdxct on pbvdxct.iID_DuAnID = pbvct.iID_DuAnID
	left join
		VDT_DA_DuAn da
	on
		pbvct.iID_DuAnID = da.iID_DuAnID
	left join
		VDT_DM_LoaiCongTrinh lct
	on
		pbvct.iID_LoaiCongTrinh = lct.iID_LoaiCongTrinh
	left join
		VDT_DM_DonViThucHienDuAn dv
	on da.iID_MaDonViThucHienDuAnID = dv.iID_MaDonVi
	where
		pbvct.iID_PhanBoVon_DonVi_PheDuyet_ID = @iIdPhanBoVon
		and pbv.bIsGoc = 1 and pbvdxct.bActive = 1
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_all_dutoan_chiphi_by_duan_quyettoanchitiet_1]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_get_all_dutoan_chiphi_by_duan_quyettoanchitiet_1]
@iIdDuToanId uniqueidentifier
AS
BEGIN
	SELECT cp.iID_ChiPhiID, cp.iID_DuAn_ChiPhi, SUM(ISNULL(cp.fTienPheDuyetQDDT, 0)) as GiaTriPheDuyet INTO #tmpDuToanChiPhi
	FROM VDT_DA_DuToan as tbl
	INNER JOIN VDT_DA_DuToan_ChiPhi as cp on tbl.iID_DuToanID = cp.iID_DuToanID
	WHERE tbl.bActive = 1 AND tbl.iID_DuToanID = @iIdDuToanId
	GROUP BY cp.iID_ChiPhiID, cp.iID_DuAn_ChiPhi

	SELECT	
		NEWID() as Id,
		cp.sTenChiPhi as TenChiPhi,
		NULL as IdDuToanChiPhi,
		tmp.iID_ChiPhiID as IdChiPhi,
		NULL AS IdDuToan,
		tmp.GiaTriPheDuyet,
		tmp.iID_DuAn_ChiPhi as IdChiPhiDuAn,
		CAST(0 as bit) as IsHangCha,
		NULL as IsHangCha,
		(CASE WHEN cp.iID_ChiPhi_Parent IS NULL THEN CAST(1 AS bit) ELSE CAST(0 as bit) END) as IsLoaiChiPhi,
		cp.IThuTu,
		cp.iID_ChiPhi_Parent as IdChiPhiDuAnParent,
		CAST(1 as bit) as IsDuAnChiPhiOld,
		CAST(1 as bit) as IsEditHangMuc,
		CAST(cp.iID_DuAn_ChiPhi as nvarchar(max)) as MaOrder,
		NULL as FGiaTriDieuChinh,
		NULL as GiaTriTruocDieuChinh,
		CAST(1 as int) as PhanCap,
		cp.iID_DuAn_ChiPhi as ChiPhiId,
		cp.sMaChiPhi as MaChiPhi
	FROM #tmpDuToanChiPhi as tmp
	INNER JOIN VDT_DM_DuAn_ChiPhi as cp on tmp.iID_DuAn_ChiPhi = cp.iID_DuAn_ChiPhi
	ORDER BY cp.iThuTu

	DROP TABLE #tmpDuToanChiPhi
END
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_all_qddt_nguonvon_by_list_dutoan_id]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_vdt_get_all_qddt_nguonvon_by_list_dutoan_id]
  @qddtId ntext
AS
BEGIN
  declare @DuAnId nvarchar(300)
  set @DuAnId = (select iID_DuAnID FROM VDT_DA_QDDauTu where iID_QDDauTuID = cast(@qddtId AS nvarchar(300)))

  select 
  nv.iID_NguonVonID as IIdNguonVonId,
  SUM(nv.fTienPheDuyet) as GiaTriPheDuyet,
  ns.sTen as TenNguonVon,
  pdttct.fGiaTriThanhToanTN + pdttct.fGiaTriThanhToanNN as DaThanhToan,
  cast(0 as float) as TienDeNghi,
  SUM(cast(ISNULL(nguonvonchitiet.fCapPhatTaiKhoBac,0) as float)) AS fCapPhatTaiKhoBac,
  SUM(cast(isnull(nguonvonchitiet.fCapPhatBangLenhChi,0) as float)) AS fCapPhatBangLenhChi
  from VDT_DA_QDDauTu_NguonVon nv
  inner join NguonNganSach ns ON ns.iID_MaNguonNganSach = nv.iID_NguonVonID
  left join VDT_KHV_PhanBoVon_ChiTiet nguonvonchitiet on nguonvonchitiet.iID_DuAnID = @DuAnId
  left join VDT_TT_DeNghiThanhToan dntt on dntt.iID_DuAnId = @DuAnId
  left join VDT_TT_PheDuyetThanhToan_ChiTiet pdttct on pdttct.iID_DeNghiThanhToanID = dntt.Id  
  where nv.iID_QDDauTuID in (select * FROM f_split(@qddtId))
  group by nv.iID_NguonVonID,ns.sTen,pdttct.fGiaTriThanhToanTN,pdttct.fGiaTriThanhToanNN
  
END;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_baocaodquyettoanniendo_vonung]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--exec sp_vdt_get_baocaodquyettoanniendo_vonung '008',2023,2,1

CREATE PROC [dbo].[sp_vdt_get_baocaodquyettoanniendo_vonung]
@iIdMaDonVi nvarchar(100), 
@iNamKeHoach int, 
@iIdNguonVon int
AS
BEGIN
	CREATE TABLE #tmpUnion(IIDDuAnID uniqueidentifier, SMaDuAn nvarchar(500) , SDiaDiem nvarchar(500), STenDuAn nvarchar(500))

	INSERT INTO #tmpUnion(IIDDuAnID, SMaDuAn, SDiaDiem, STenDuAn)
	SELECT DISTINCT da.iID_DuAnID as IIDDuAnID, da.SMaDuAn, da.SDiaDiem, da.STenDuAn
	FROM VDT_KHV_KeHoachVonUng as tbl
	INNER JOIN VDT_KHV_KeHoachVonUng_ChiTiet as dt on tbl.Id = dt.iID_KeHoachUngID
	INNER JOIN VDT_DA_DuAn as da on dt.iID_DuAnID = da.iID_DuAnID
	WHERE da.iID_MaDonViThucHienDuAnID = @iIdMaDonVi AND tbl.iNamKeHoach = @iNamKeHoach AND tbl.iID_NguonVonID = @iIdNguonVon
	UNION ALL
	SELECT DISTINCT da.iID_DuAnID as IIDDuAnID, da.SMaDuAn, da.SDiaDiem, da.STenDuAn
	FROM VDT_QT_BCQuyetToanNienDo as tbl
	INNER JOIN VDT_QT_BCQuyetToanNienDo_ChiTiet_01 as dt on tbl.Id = dt.iID_BCQuyetToanNienDo
	INNER JOIN VDT_DA_DuAn as da on dt.iID_DuAnID = da.iID_DuAnID
	WHERE iNamKeHoach = @iNamKeHoach AND ((ISNULL(dt.fKHUngTrcChuaThuHoiTrcNamQuyetToan, 0) - ISNULL(dt.fThuHoiUngTruoc, 0) + ISNULL(dt.fKHUngNamNay, 0) - 
				(ISNULL(dt.fLKThanhToanDenTrcNamQuyetToan_KHUng, 0) - ISNULL(dt.fGiaTriThuHoiTheoGiaiNganThucTe, 0) + ISNULL(dt.fThanhToan_KHUngNamTrcChuyenSang, 0) + ISNULL(dt.fThanhToan_KHUngNamNay, 0))) <> 0)
	UNION ALL
	SELECT dt.iID_DuAnID as IIDDuAnID, da.SMaDuAn, da.SDiaDiem, da.STenDuAn
	FROM 
	(SELECT iID_DuAnID ,
		(CASE WHEN sMaNguon = '321b' AND sMaDich = '000' AND sMaTienTrinh = '100' THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongTien,
		(CASE WHEN sMaDich = '321b' AND sMaNguon = '000' AND sMaTienTrinh = '100' THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fKhauTru
	FROM VDT_TongHop_NguonNSDauTu
	WHERE iNamKeHoach = (@iNamKeHoach - 1) AND iID_MaDonViQuanLy = @iIdMaDonVi AND iID_NguonVonID = @iIdNguonVon
	GROUP BY iID_DuAnID, sMaNguon, sMaDich, sMaTienTrinh) as dt
	INNER JOIN VDT_DA_DuAn as da on dt.iID_DuAnID = da.iID_DuAnID
	WHERE (ISNULL(fTongTien, 0) - ISNULL(fKhauTru, 0)) > 0
	UNION ALL
	SELECT dt.iID_DuAnID as IIDDuAnID, da.SMaDuAn, da.SDiaDiem, da.STenDuAn
	FROM 
	(SELECT iID_DuAnID ,
		(CASE WHEN sMaNguon = '322b' AND sMaDich = '000' AND sMaTienTrinh = '100' THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongTien,
		(CASE WHEN sMaDich = '322b' AND sMaNguon = '000' AND sMaTienTrinh = '100' THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fKhauTru
	FROM VDT_TongHop_NguonNSDauTu
	WHERE iNamKeHoach = (@iNamKeHoach - 1) AND iID_MaDonViQuanLy = @iIdMaDonVi AND iID_NguonVonID = @iIdNguonVon
	GROUP BY iID_DuAnID, sMaNguon, sMaDich, sMaTienTrinh) as dt
	INNER JOIN VDT_DA_DuAn as da on dt.iID_DuAnID = da.iID_DuAnID
	WHERE (ISNULL(fTongTien, 0) - ISNULL(fKhauTru, 0)) > 0

	SELECT DISTINCT * INTO #tmp
	FROM #tmpUnion

	---- Kho bac
	BEGIN
		SELECT IIDDuAnID,
				SUM(ISNULL(fUngTruocChuaThuHoiNamTruoc, 0)) as fUngTruocChuaThuHoiNamTruoc,
				SUM(ISNULL(fUngTruocChuaThuHoiNamTruocDelete, 0)) as fUngTruocChuaThuHoiNamTruocDelete,
				SUM(ISNULL(fLuyKeThanhToanNamTruoc, 0)) as fLuyKeThanhToanNamTruoc,
				SUM(ISNULL(fLuyKeThanhToanNamTruocDelete, 0)) as fLuyKeThanhToanNamTruocDelete,
				SUM(ISNULL(fLuyKeTTKLHTChuaThuHoi, 0)) as fLuyKeTTKLHTChuaThuHoi,
				SUM(ISNULL(fLuyKeTTKLHTChuaThuHoiDelete, 0)) as fLuyKeTTKLHTChuaThuHoiDelete,
				SUM(ISNULL(fLuyKeThuHoiUngNamTruoc, 0)) as fLuyKeThuHoiUngNamTruoc,
				SUM(ISNULL(fLuyKeThuHoiUngNamTruocDelete, 0)) as fLuyKeThuHoiUngNamTruocDelete INTO #tmpNamTruocKB
		FROM
		(
			SELECT tmp.IIDDuAnID,
				(CASE WHEN sMaNguon = '321b' AND sMaTienTrinh = '100' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fUngTruocChuaThuHoiNamTruoc,
				(CASE WHEN sMaDich = '321b' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fUngTruocChuaThuHoiNamTruocDelete,

				(CASE WHEN sMaDich = '403' AND sMaNguonCha = '321a' AND sMaTienTrinh = '100' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fLuyKeThanhToanNamTruoc,
				(CASE WHEN sMaNguon = '403' AND sMaNguonCha = '321a' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fLuyKeThanhToanNamTruocDelete,

				(CASE WHEN sMaDich = '403a' AND sMaTienTrinh = '100' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fLuyKeTTKLHTChuaThuHoi,
				(CASE WHEN sMaNguon = '403a' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fLuyKeTTKLHTChuaThuHoiDelete,

				(CASE WHEN sMaNguon = '211a' AND (sMaNguonCha IS NULL OR sMaNguonCha in ('121a', '131')) AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fLuyKeThuHoiUngNamTruoc,
				(CASE WHEN sMaDich = '211a' AND (sMaNguonCha IS NULL OR sMaNguonCha in ('121a', '131')) AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fLuyKeThuHoiUngNamTruocDelete
			FROM (SELECT DISTINCT * FROM #tmp) as tmp
			INNER JOIN VDT_TongHop_NguonNSDauTu as dt on tmp.IIDDuAnID = dt.iID_DuAnID
			WHERE iNamKeHoach = @iNamKeHoach -1
			GROUP BY tmp.IIDDuAnID, sMaDich, sMaNguon, sMaNguonCha, iID_NguonVonID, sMaTienTrinh
		) as tbl
		GROUP BY IIDDuAnID

		SELECT IIDDuAnID,
			SUM(ISNULL(fThanhToanKLHTNamTruocChuyenSang, 0)) as fThanhToanKLHTNamTruocChuyenSang,
			SUM(ISNULL(fThanhToanKLHTNamTruocChuyenSangDelete, 0)) as fThanhToanKLHTNamTruocChuyenSangDelete,
			SUM(ISNULL(fThanhToanUngNamTruocChuyenSang, 0)) as fThanhToanUngNamTruocChuyenSang,
			SUM(ISNULL(fThanhToanUngNamTruocChuyenSangDelete, 0)) as fThanhToanUngNamTruocChuyenSangDelete,
			
			SUM(ISNULL(fLuyKeThanhToanKHVU, 0)) as fLuyKeThanhToanKHVU,
			SUM(ISNULL(fLuyKeThanhToanKHVUDelete, 0)) as fLuyKeThanhToanKHVUDelete,

			SUM(ISNULL(fThuHoiUngTruocNamTruoc, 0)) as fThuHoiUngTruocNamTruoc,
			SUM(ISNULL(fThuHoiUngTruocNamTruocDelete, 0)) as fThuHoiUngTruocNamTruocDelete,

			SUM(ISNULL(fThuHoiTamUngNamNayVonNamTruoc, 0)) as fThuHoiTamUngNamNayVonNamTruoc,
			SUM(ISNULL(fThuHoiTamUngNamNayVonNamTruocDelete, 0)) as fThuHoiTamUngNamNayVonNamTruocDelete,
			SUM(ISNULL(fThuHoiTamUngNamTruocVonNamTruoc, 0)) as fThuHoiTamUngNamTruocVonNamTruoc,
			SUM(ISNULL(fThuHoiTamUngNamTruocVonNamTruocDelete, 0)) as fThuHoiTamUngNamTruocVonNamTruocDelete,

			SUM(ISNULL(fThuHoiVonUngNamNay, 0)) as fThuHoiVonUngNamNay,
			SUM(ISNULL(fThuHoiVonUngNamNayDelete, 0)) as fThuHoiVonUngNamNayDelete,

			SUM(ISNULL(fThuHoiVonUngKeHoachNamNay, 0)) as fThuHoiVonUngKeHoachNamNay,
			SUM(ISNULL(fThuHoiVonUngKeHoachNamNayDelete, 0)) as fThuHoiVonUngKeHoachNamNayDelete,

			SUM(ISNULL(fKHVUNamNay, 0)) as fKHVUNamNay,
			SUM(ISNULL(fKHVUNamNayDelete, 0)) as fKHVUNamNayDelete,

			SUM(ISNULL(fThanhToanKLHTTamUngNamNay, 0)) as fThanhToanKLHTTamUngNamNay,
			SUM(ISNULL(fThanhToanKLHTTamUngNamNayDelete, 0)) as fThanhToanKLHTTamUngNamNayDelete,
			SUM(ISNULL(fThanhToanUngNamNay, 0)) as fThanhToanUngNamNay,
			SUM(ISNULL(fThanhToanUngNamNayDelete, 0)) as fThanhToanUngNamNayDelete,

			SUM(ISNULL(fThuHoiTamUngNamNay, 0)) as fThuHoiTamUngNamNay,
			SUM(ISNULL(fThuHoiTamUngNamNayDelete, 0)) as fThuHoiTamUngNamNayDelete,
			SUM(ISNULL(fThuHoiTamUngNamTruoc, 0)) as fThuHoiTamUngNamTruoc,
			SUM(ISNULL(fThuHoiTamUngNamTruocDelete, 0)) as fThuHoiTamUngNamTruocDelete INTO #tmpNamNayKB
		FROM
		(
			SELECT tmp.IIDDuAnID,
				(CASE WHEN sMaDich = '201' AND sMaNguonCha = '131' AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fThanhToanKLHTNamTruocChuyenSang,
				(CASE WHEN sMaNguon = '201' AND sMaNguonCha = '131' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fThanhToanKLHTNamTruocChuyenSangDelete,
				(CASE WHEN sMaDich = '211a' AND sMaNguonCha = '131' AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fThanhToanUngNamTruocChuyenSang,
				(CASE WHEN sMaNguon = '211a' AND sMaNguonCha = '131' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fThanhToanUngNamTruocChuyenSangDelete,
				
				(CASE WHEN sMaDich = '201' AND sMaNguonCha in ('121a','131') AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fLuyKeThanhToanKHVU,
				(CASE WHEN sMaNguon = '201' AND sMaNguonCha in ('121a','131') AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fLuyKeThanhToanKHVUDelete,

				(CASE WHEN sMaNguon = '211a' AND sMaNguonCha = '101' AND iLoaiUng = 2 AND sMaTienTrinh = '200' THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fThuHoiUngTruocNamTruoc,
				(CASE WHEN sMaDich = '211a' AND sMaNguonCha = '101' AND iLoaiUng = 2 AND sMaTienTrinh = '300' THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fThuHoiUngTruocNamTruocDelete,

				(CASE WHEN sMaNguon = '211a' AND sMaNguonCha = '131' AND sMaTienTrinh = '200' AND (iThuHoiTUCheDo = 2 OR iThuHoiTUCheDo IS NULL) AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fThuHoiTamUngNamNayVonNamTruoc,
				(CASE WHEN sMaDich = '211a' AND sMaNguonCha = '131' AND sMaTienTrinh = '300' AND (iThuHoiTUCheDo = 2 OR iThuHoiTUCheDo IS NULL) AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fThuHoiTamUngNamNayVonNamTruocDelete,
				(CASE WHEN sMaNguon = '211a' AND sMaNguonCha = '131' AND sMaTienTrinh = '200' AND iThuHoiTUCheDo = 1 AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fThuHoiTamUngNamTruocVonNamTruoc,
				(CASE WHEN sMaDich = '211a' AND sMaNguonCha = '131' AND sMaTienTrinh = '300' AND iThuHoiTUCheDo = 1 AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fThuHoiTamUngNamTruocVonNamTruocDelete,

				(CASE WHEN sMaDich = '121a'AND sMaNguon = '000' AND bKeHoach = 1 AND sMaTienTrinh = '200' THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fThuHoiVonUngNamNay,
				(CASE WHEN sMaNguon = '121a'AND sMaDich = '000' AND bKeHoach = 1 AND sMaTienTrinh = '300' THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fThuHoiVonUngNamNayDelete,

				(CASE WHEN sMaDich = '121a'AND bKeHoach = 0 AND sMaTienTrinh = '200' THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fThuHoiVonUngKeHoachNamNay,
				(CASE WHEN sMaNguon = '121a'AND bKeHoach = 0 AND sMaTienTrinh = '300' THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fThuHoiVonUngKeHoachNamNayDelete,

				(CASE WHEN sMaNguon = '121a' AND sMaDich = '000' AND sMaTienTrinh = '200' THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fKHVUNamNay,
				(CASE WHEN sMaDich = '121a' AND sMaNguon = '000' AND sMaTienTrinh = '300' THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fKHVUNamNayDelete,

				(CASE WHEN sMaDich = '201' AND sMaNguonCha = '121a' AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fThanhToanKLHTTamUngNamNay,
				(CASE WHEN sMaNguon = '201' AND sMaNguonCha = '121a' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fThanhToanKLHTTamUngNamNayDelete,
				(CASE WHEN sMaDich = '211a' AND sMaNguonCha = '121a' AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) FThanhToanUngNamNay,
				(CASE WHEN sMaNguon = '211a' AND sMaNguonCha = '121a' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fThanhToanUngNamNayDelete,

				(CASE WHEN sMaNguon = '211a' AND sMaNguonCha = '121a' AND sMaTienTrinh = '200' AND iThuHoiTUCheDo = 2 AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fThuHoiTamUngNamNay,
				(CASE WHEN sMaDich = '211a' AND sMaNguonCha = '121a' AND sMaTienTrinh = '300' AND iThuHoiTUCheDo = 2 AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fThuHoiTamUngNamNayDelete,
				(CASE WHEN sMaNguon = '211a' AND sMaNguonCha = '121a' AND sMaTienTrinh = '200' AND iThuHoiTUCheDo = 1 AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fThuHoiTamUngNamTruoc,
				(CASE WHEN sMaDich = '211a' AND sMaNguonCha = '121a' AND sMaTienTrinh = '300' AND iThuHoiTUCheDo = 1 AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fThuHoiTamUngNamTruocDelete
			FROM (SELECT DISTINCT * FROM #tmp) as tmp
			INNER JOIN VDT_TongHop_NguonNSDauTu as dt on tmp.IIDDuAnID = dt.iID_DuAnID
			WHERE iNamKeHoach = @iNamKeHoach 
			GROUP BY tmp.IIDDuAnID, sMaDich, sMaNguon, sMaNguonCha, iID_NguonVonID, sMaTienTrinh, iThuHoiTUCheDo, iLoaiUng, bKeHoach
		) as tbl
		GROUP BY IIDDuAnID
	END
	
	-- CQTC
	BEGIN
		SELECT IIDDuAnID,
				SUM(ISNULL(fUngTruocChuaThuHoiNamTruoc, 0)) as fUngTruocChuaThuHoiNamTruoc,
				SUM(ISNULL(fUngTruocChuaThuHoiNamTruocDelete, 0)) as fUngTruocChuaThuHoiNamTruocDelete,
				SUM(ISNULL(fLuyKeThanhToanNamTruoc, 0)) as fLuyKeThanhToanNamTruoc,
				SUM(ISNULL(fLuyKeThanhToanNamTruocDelete, 0)) as fLuyKeThanhToanNamTruocDelete,
				SUM(ISNULL(fLuyKeTTKLHTChuaThuHoi, 0)) as fLuyKeTTKLHTChuaThuHoi,
				SUM(ISNULL(fLuyKeTTKLHTChuaThuHoiDelete, 0)) as fLuyKeTTKLHTChuaThuHoiDelete,
				SUM(ISNULL(fLuyKeThuHoiUngNamTruoc, 0)) as fLuyKeThuHoiUngNamTruoc,
				SUM(ISNULL(fLuyKeThuHoiUngNamTruocDelete, 0)) as fLuyKeThuHoiUngNamTruocDelete INTO #tmpNamTruocCQTC
		FROM
		(
			SELECT tmp.IIDDuAnID,
				(CASE WHEN sMaNguon = '322b' AND sMaTienTrinh = '100' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fUngTruocChuaThuHoiNamTruoc,
				(CASE WHEN sMaDich = '322b' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fUngTruocChuaThuHoiNamTruocDelete,

				(CASE WHEN sMaDich = '404' AND sMaNguonCha = '322a' AND sMaTienTrinh = '100' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fLuyKeThanhToanNamTruoc,
				(CASE WHEN sMaNguon = '404' AND sMaNguonCha = '322a' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fLuyKeThanhToanNamTruocDelete,
				
				(CASE WHEN sMaDich = '404a' AND sMaTienTrinh = '100' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fLuyKeTTKLHTChuaThuHoi,
				(CASE WHEN sMaNguon = '404a' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fLuyKeTTKLHTChuaThuHoiDelete,

				(CASE WHEN sMaNguon = '212a' AND (sMaNguonCha IS NULL OR sMaNguonCha in ('122a', '132')) AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fLuyKeThuHoiUngNamTruoc,
				(CASE WHEN sMaDich = '212a' AND (sMaNguonCha IS NULL OR sMaNguonCha in ('122a', '132')) AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fLuyKeThuHoiUngNamTruocDelete
			FROM (SELECT DISTINCT * FROM #tmp) as tmp
			INNER JOIN VDT_TongHop_NguonNSDauTu as dt on tmp.IIDDuAnID = dt.iID_DuAnID
			WHERE iNamKeHoach = @iNamKeHoach - 1
			GROUP BY tmp.IIDDuAnID, sMaDich, sMaNguon, sMaNguonCha, iID_NguonVonID, sMaTienTrinh
		) as tbl
		GROUP BY IIDDuAnID

		SELECT IIDDuAnID,
			SUM(ISNULL(fThanhToanKLHTNamTruocChuyenSang, 0)) as fThanhToanKLHTNamTruocChuyenSang,
			SUM(ISNULL(fThanhToanKLHTNamTruocChuyenSangDelete, 0)) as fThanhToanKLHTNamTruocChuyenSangDelete,
			SUM(ISNULL(fThanhToanUngNamTruocChuyenSang, 0)) as fThanhToanUngNamTruocChuyenSang,
			SUM(ISNULL(fThanhToanUngNamTruocChuyenSangDelete, 0)) as fThanhToanUngNamTruocChuyenSangDelete,
			SUM(ISNULL(fThuHoiUngTruocNamTruoc, 0)) as fThuHoiUngTruocNamTruoc,
			SUM(ISNULL(fThuHoiUngTruocNamTruocDelete, 0)) as fThuHoiUngTruocNamTruocDelete,
			
			SUM(ISNULL(fThuHoiTamUngNamNayVonNamTruoc, 0)) as fThuHoiTamUngNamNayVonNamTruoc,
			SUM(ISNULL(fThuHoiTamUngNamNayVonNamTruocDelete, 0)) as fThuHoiTamUngNamNayVonNamTruocDelete,
			SUM(ISNULL(fThuHoiTamUngNamTruocVonNamTruoc, 0)) as fThuHoiTamUngNamTruocVonNamTruoc,
			SUM(ISNULL(fThuHoiTamUngNamTruocVonNamTruocDelete, 0)) as fThuHoiTamUngNamTruocVonNamTruocDelete,
			SUM(ISNULL(fLuyKeThanhToanKHVU, 0)) as fLuyKeThanhToanKHVU,
			SUM(ISNULL(fLuyKeThanhToanKHVUDelete, 0)) as fLuyKeThanhToanKHVUDelete,

			SUM(ISNULL(fLuyKeThanhToanTrongNam, 0)) as fLuyKeThanhToanTrongNam,
			SUM(ISNULL(fLuyKeThanhToanTrongNamDelete, 0)) as fLuyKeThanhToanTrongNamDelete,
			SUM(ISNULL(fThuHoiVonUngNamNay, 0)) as fThuHoiVonUngNamNay,
			SUM(ISNULL(fThuHoiVonUngNamNayDelete, 0)) as fThuHoiVonUngNamNayDelete,

			SUM(ISNULL(fThuHoiVonUngKeHoachNamNay, 0)) as fThuHoiVonUngKeHoachNamNay,
			SUM(ISNULL(fThuHoiVonUngKeHoachNamNayDelete, 0)) as fThuHoiVonUngKeHoachNamNayDelete,

			SUM(ISNULL(fKHVUNamNay, 0)) as fKHVUNamNay,
			SUM(ISNULL(fKHVUNamNayDelete, 0)) as fKHVUNamNayDelete,
			
			SUM(ISNULL(fThanhToanKLHTTamUngNamNay, 0)) as fThanhToanKLHTTamUngNamNay,
			SUM(ISNULL(fThanhToanKLHTTamUngNamNayDelete, 0)) as fThanhToanKLHTTamUngNamNayDelete,
			SUM(ISNULL(fThanhToanUngNamNay, 0)) as fThanhToanUngNamNay,
			SUM(ISNULL(fThanhToanUngNamNayDelete, 0)) as fThanhToanUngNamNayDelete,

			SUM(ISNULL(fThuHoiTamUngNamNay, 0)) as fThuHoiTamUngNamNay,
			SUM(ISNULL(fThuHoiTamUngNamNayDelete, 0)) as fThuHoiTamUngNamNayDelete,
			SUM(ISNULL(fThuHoiTamUngNamTruoc, 0)) as fThuHoiTamUngNamTruoc,
			SUM(ISNULL(fThuHoiTamUngNamTruocDelete, 0)) as fThuHoiTamUngNamTruocDelete INTO #tmpNamNayCQTC
		FROM
		(
			SELECT tmp.IIDDuAnID,
				(CASE WHEN sMaDich = '202' AND sMaNguonCha = '132' AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fThanhToanKLHTNamTruocChuyenSang,
				(CASE WHEN sMaNguon = '202' AND sMaNguonCha = '132' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fThanhToanKLHTNamTruocChuyenSangDelete,
				(CASE WHEN sMaDich = '212a' AND sMaNguonCha = '132' AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fThanhToanUngNamTruocChuyenSang,
				(CASE WHEN sMaNguon = '212a' AND sMaNguonCha = '132' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fThanhToanUngNamTruocChuyenSangDelete,
				
				(CASE WHEN sMaNguon = '212a' AND sMaNguonCha = '102' AND iLoaiUng = 2 AND sMaTienTrinh = '200' THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fThuHoiUngTruocNamTruoc,
				(CASE WHEN sMaDich = '212a' AND sMaNguonCha = '102' AND iLoaiUng = 2 AND sMaTienTrinh = '300' THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fThuHoiUngTruocNamTruocDelete,
				
				(CASE WHEN sMaDich = '202' AND sMaNguonCha in ('122a','132') AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fLuyKeThanhToanKHVU,
				(CASE WHEN sMaNguon = '202' AND sMaNguonCha in ('122a','132') AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fLuyKeThanhToanKHVUDelete,

				(CASE WHEN sMaNguon = '212a' AND sMaNguonCha = '132' AND sMaTienTrinh = '200' AND (iThuHoiTUCheDo = 2 OR iThuHoiTUCheDo IS NULL) AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fThuHoiTamUngNamNayVonNamTruoc,
				(CASE WHEN sMaDich = '212a' AND sMaNguonCha = '132' AND sMaTienTrinh = '300' AND (iThuHoiTUCheDo = 2 OR iThuHoiTUCheDo IS NULL) AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fThuHoiTamUngNamNayVonNamTruocDelete,
				(CASE WHEN sMaNguon = '212a' AND sMaNguonCha = '132' AND sMaTienTrinh = '200' AND iThuHoiTUCheDo = 1 AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fThuHoiTamUngNamTruocVonNamTruoc,
				(CASE WHEN sMaDich = '212a' AND sMaNguonCha = '132' AND sMaTienTrinh = '300' AND iThuHoiTUCheDo = 1 AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fThuHoiTamUngNamTruocVonNamTruocDelete,

				(CASE WHEN sMaNguon = '212a' AND sMaNguonCha = '132' AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fLuyKeThanhToanTrongNam,
				(CASE WHEN sMaDich = '212a' AND sMaNguonCha = '132' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fLuyKeThanhToanTrongNamDelete,

				(CASE WHEN sMaDich = '122a' AND sMaNguon = '000' AND bKeHoach = 1 AND sMaTienTrinh = '200' THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fThuHoiVonUngNamNay,
				(CASE WHEN sMaNguon = '122a' AND sMaDich = '000' AND bKeHoach = 1 AND sMaTienTrinh = '300' THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fThuHoiVonUngNamNayDelete,
				
				(CASE WHEN sMaDich = '122a'AND bKeHoach = 0 AND sMaTienTrinh = '200' THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fThuHoiVonUngKeHoachNamNay,
				(CASE WHEN sMaNguon = '122a'AND bKeHoach = 0 AND sMaTienTrinh = '300' THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fThuHoiVonUngKeHoachNamNayDelete,

				(CASE WHEN sMaNguon = '122a' AND sMaDich = '000' AND sMaTienTrinh = '200' THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fKHVUNamNay,
				(CASE WHEN sMaDich = '122a' AND sMaNguon = '000' AND sMaTienTrinh = '300' THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fKHVUNamNayDelete,
			
				(CASE WHEN sMaDich = '202' AND sMaNguonCha = '122a' AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fThanhToanKLHTTamUngNamNay,
				(CASE WHEN sMaNguon = '202' AND sMaNguonCha = '122a' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fThanhToanKLHTTamUngNamNayDelete,
				(CASE WHEN sMaDich = '212a' AND sMaNguonCha = '122a' AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) FThanhToanUngNamNay,
				(CASE WHEN sMaNguon = '212a' AND sMaNguonCha = '122a' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fThanhToanUngNamNayDelete,

				(CASE WHEN sMaNguon = '212a' AND sMaNguonCha = '122a' AND sMaTienTrinh = '200' AND iThuHoiTUCheDo = 2 AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fThuHoiTamUngNamNay,
				(CASE WHEN sMaDich = '212a' AND sMaNguonCha = '122a' AND sMaTienTrinh = '300' AND iThuHoiTUCheDo = 2 AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fThuHoiTamUngNamNayDelete,
				(CASE WHEN sMaNguon = '212a' AND sMaNguonCha = '122a' AND sMaTienTrinh = '200' AND iThuHoiTUCheDo = 1 AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fThuHoiTamUngNamTruoc,
				(CASE WHEN sMaDich = '212a' AND sMaNguonCha = '122a' AND sMaTienTrinh = '300' AND iThuHoiTUCheDo = 1 AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) fThuHoiTamUngNamTruocDelete
			FROM (SELECT DISTINCT * FROM #tmp) as tmp
			INNER JOIN VDT_TongHop_NguonNSDauTu as dt on tmp.IIDDuAnID = dt.iID_DuAnID
			WHERE iNamKeHoach = @iNamKeHoach 
			GROUP BY tmp.IIDDuAnID, sMaDich, sMaNguon, sMaNguonCha, iID_NguonVonID, sMaTienTrinh, iThuHoiTUCheDo, iLoaiUng, bKeHoach
		) as tbl
		GROUP BY IIDDuAnID
	END
	
	SELECT tmp.IIDDuAnID, tmp.SMaDuAn, tmp.SDiaDiem, tmp.STenDuAn, CAST(2 as int) as ICoQuanThanhToan,
		SUM(ISNULL(nt.fUngTruocChuaThuHoiNamTruoc, 0) - ISNULL(nt.fUngTruocChuaThuHoiNamTruocDelete, 0)) as fUngTruocChuaThuHoiNamTruoc, 
		SUM(ISNULL(nt.fLuyKeThanhToanNamTruoc, 0) - ISNULL(nt.fLuyKeThanhToanNamTruocDelete, 0)) as fLuyKeThanhToanNamTruoc, 
			
		SUM(ISNULL(nn.fThanhToanKLHTNamTruocChuyenSang, 0) - ISNULL(nn.fThanhToanKLHTNamTruocChuyenSangDelete, 0)) as fThanhToanKLHTNamTruocChuyenSang,
		SUM(ISNULL(nn.fThanhToanUngNamTruocChuyenSang, 0) - ISNULL(nn.fThanhToanUngNamTruocChuyenSangDelete, 0)) as fThanhToanUngNamTruocChuyenSang,
			
		SUM(ISNULL(nn.fThuHoiTamUngNamNayVonNamTruoc, 0) - ISNULL(nn.fThuHoiTamUngNamNayVonNamTruocDelete, 0)) as fThuHoiTamUngNamNayVonNamTruoc,
		SUM(ISNULL(nn.fThuHoiTamUngNamTruocVonNamTruoc, 0) - ISNULL(nn.fThuHoiTamUngNamTruocVonNamTruocDelete, 0)) as fThuHoiTamUngNamTruocVonNamTruoc,

		SUM(ISNULL(nn.fThuHoiVonUngNamNay, 0) - ISNULL(nn.fThuHoiVonUngNamNayDelete, 0)) as fThuHoiVonNamNay,
		SUM(ISNULL(nn.fKHVUNamNay, 0) - ISNULL(nn.fKHVUNamNayDelete, 0)) as fKHVUNamNay,
			
		SUM(ISNULL(nn.fThanhToanKLHTTamUngNamNay, 0) - ISNULL(nn.fThanhToanKLHTTamUngNamNayDelete, 0)) as fThanhToanKLHTTamUngNamNay ,
		SUM(ISNULL(nn.fThanhToanUngNamNay, 0) - ISNULL(nn.fThanhToanUngNamNayDelete, 0)) as fThanhToanUngNamNay ,
		SUM(ISNULL(nn.fThuHoiTamUngNamNay, 0) - ISNULL(nn.fThuHoiTamUngNamNayDelete, 0)) as fThuHoiTamUngNamNay ,
		SUM(ISNULL(nn.fThuHoiTamUngNamTruoc, 0) - ISNULL(nn.fThuHoiTamUngNamTruocDelete, 0)) as fThuHoiTamUngNamTruoc ,
		SUM((CASE WHEN @iIdNguonVon = 2 THEN
			(ISNULL(nn.fThuHoiVonUngKeHoachNamNay, 0) - ISNULL(nn.fThuHoiVonUngKeHoachNamNayDelete, 0))
		ELSE
			((ISNULL(nt.fLuyKeTTKLHTChuaThuHoi, 0) + ISNULL(nn.fThuHoiUngTruocNamTruoc, 0) + ISNULL(nn.fLuyKeThanhToanKHVU, 0))
			- (ISNULL(nt.fLuyKeTTKLHTChuaThuHoiDelete, 0) + ISNULL(nn.fThuHoiUngTruocNamTruocDelete, 0) + ISNULL(nn.fLuyKeThanhToanKHVUDelete, 0))) END)) as FGiaTriThuHoiTheoGiaiNganThucTe
	FROM (SELECT DISTINCT * FROM #tmp) as tmp
	LEFT JOIN #tmpNamNayCQTC as nn on tmp.IIDDuAnID = nn.IIDDuAnID
	LEFT JOIN #tmpNamTruocCQTC as nt on tmp.IIDDuAnID = nt.IIDDuAnID
	GROUP BY tmp.IIDDuAnID, tmp.sMaDuAn, tmp.sDiaDiem, tmp.sTenDuAn
	UNION ALL
	SELECT tmp.IIDDuAnID, tmp.SMaDuAn, tmp.SDiaDiem, tmp.STenDuAn,  CAST(1 as int) as ICoQuanThanhToan,
		SUM(ISNULL(nt.fUngTruocChuaThuHoiNamTruoc, 0) - ISNULL(nt.fUngTruocChuaThuHoiNamTruocDelete, 0)) as fUngTruocChuaThuHoiNamTruoc, 
		SUM(ISNULL(nt.fLuyKeThanhToanNamTruoc, 0) - ISNULL(nt.fLuyKeThanhToanNamTruocDelete, 0)) as fLuyKeThanhToanNamTruoc, 

		SUM(ISNULL(nn.fThanhToanKLHTNamTruocChuyenSang, 0) - ISNULL(nn.fThanhToanKLHTNamTruocChuyenSangDelete, 0)) as fThanhToanKLHTNamTruocChuyenSang,
		SUM(ISNULL(nn.fThanhToanUngNamTruocChuyenSang, 0) - ISNULL(nn.fThanhToanUngNamTruocChuyenSangDelete, 0)) as fThanhToanUngNamTruocChuyenSang,
			
		SUM(ISNULL(nn.fThuHoiTamUngNamNayVonNamTruoc, 0) - ISNULL(nn.fThuHoiTamUngNamNayVonNamTruocDelete, 0)) as fThuHoiTamUngNamNayVonNamTruoc,
		SUM(ISNULL(nn.fThuHoiTamUngNamTruocVonNamTruoc, 0) - ISNULL(nn.fThuHoiTamUngNamTruocVonNamTruocDelete, 0)) as fThuHoiTamUngNamTruocVonNamTruoc,

		SUM(ISNULL(nn.fThuHoiVonUngNamNay, 0) - ISNULL(nn.fThuHoiVonUngNamNayDelete, 0)) as fThuHoiVonNamNay,
		SUM(ISNULL(nn.fKHVUNamNay, 0) - ISNULL(nn.fKHVUNamNayDelete, 0)) as fKHVUNamNay,

		SUM(ISNULL(nn.fThanhToanKLHTTamUngNamNay, 0) - ISNULL(nn.fThanhToanKLHTTamUngNamNayDelete, 0)) as fThanhToanKLHTTamUngNamNay ,
		SUM(ISNULL(nn.fThanhToanUngNamNay, 0) - ISNULL(nn.fThanhToanUngNamNayDelete, 0)) as fThanhToanUngNamNay ,
		SUM(ISNULL(nn.fThuHoiTamUngNamNay, 0) - ISNULL(nn.fThuHoiTamUngNamNayDelete, 0)) as fThuHoiTamUngNamNay ,
		SUM(ISNULL(nn.fThuHoiTamUngNamTruoc, 0) - ISNULL(nn.fThuHoiTamUngNamTruocDelete, 0)) as fThuHoiTamUngNamTruoc ,
		SUM((CASE WHEN @iIdNguonVon = 2 THEN
			(ISNULL(nn.fThuHoiVonUngKeHoachNamNay, 0) - ISNULL(nn.fThuHoiVonUngKeHoachNamNayDelete, 0))
		ELSE
			((ISNULL(nt.fLuyKeTTKLHTChuaThuHoi, 0) + ISNULL(nn.fThuHoiUngTruocNamTruoc, 0) + ISNULL(nn.fLuyKeThanhToanKHVU, 0))
			- (ISNULL(nt.fLuyKeTTKLHTChuaThuHoiDelete, 0) + ISNULL(nn.fThuHoiUngTruocNamTruocDelete, 0) + ISNULL(nn.fLuyKeThanhToanKHVUDelete, 0))) END)) as FGiaTriThuHoiTheoGiaiNganThucTe
	FROM (SELECT DISTINCT * FROM #tmp) as tmp
	LEFT JOIN #tmpNamNayKB as nn on tmp.IIDDuAnID = nn.IIDDuAnID
	LEFT JOIN #tmpNamTruocKB as nt on tmp.IIDDuAnID = nt.IIDDuAnID
	GROUP BY tmp.IIDDuAnID, tmp.sMaDuAn, tmp.sDiaDiem, tmp.sTenDuAn

	DROP TABLE #tmpNamNayCQTC
	DROP TABLE #tmpNamTruocCQTC
	DROP TABLE #tmpNamNayKB
	DROP TABLE #tmpNamTruocKB
	DROP TABLE #tmp
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_baocaodquyettoanniendo1]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_vdt_get_baocaodquyettoanniendo1]
@iIdMaDonVi nvarchar(100),
@iNamKeHoach int,
@iIdNguonVon int
AS
BEGIN
	SELECT DISTINCT da.iID_DuAnID as IIDDuAnID, da.SMaDuAn, da.SDiaDiem, da.STenDuAn INTO #tmp
	FROM VDT_KHV_PhanBoVon as tbl
	INNER JOIN VDT_KHV_PhanBoVon_ChiTiet as dt on tbl.Id = dt.iID_PhanBoVonID
	INNER JOIN VDT_DA_DuAn as da on dt.iID_DuAnID = da.iID_DuAnID
	WHERE da.iID_MaDonViThucHienDuAnID = @iIdMaDonVi AND tbl.iNamKeHoach = @iNamKeHoach AND tbl.iID_NguonVonID = @iIdNguonVon
	UNION ALL
	SELECT DISTINCT da.iID_DuAnID as IIDDuAnID, da.SMaDuAn, da.SDiaDiem, da.STenDuAn
	FROM VDT_QT_BCQuyetToanNienDo as tbl
	INNER JOIN VDT_QT_BCQuyetToanNienDo_ChiTiet_01 as dt on tbl.Id = dt.iID_BCQuyetToanNienDo
	INNER JOIN VDT_DA_DuAn as da on dt.iID_DuAnID = da.iID_DuAnID
	WHERE iNamKeHoach = @iNamKeHoach AND (ISNULL(dt.fGiaTriNamTruocChuyenNamSau, 0) <> 0 OR ISNULL(dt.fGiaTriNamNayChuyenNamSau, 0) <> 0)


	-- Tong muc dau tu
	SELECT tmp.IIDDuAnID, SUM(ISNULL(qd.fTongMucDauTuPheDuyet, 0)) as fTongMucDauTu INTO #tmpTongMucDauTu
	FROM (SELECT DISTINCT * FROM #tmp) as tmp
	INNER JOIN VDT_DA_QDDauTu as qd on tmp.IIDDuAnID = qd.iID_DuAnID
	WHERE qd.BActive = 1
	GROUP BY tmp.IIDDuAnID

	---- Kho bac
	BEGIN
		-- TongHopDuLieu nam truoc
		SELECT tbl.IIDDuAnID, 
			SUM(ISNULL(fLuyKeThanhToanNamTruoc, 0)) fLuyKeThanhToanNamTruoc,
			SUM(ISNULL(fLuyKeThanhToanNamTruocDelete, 0)) fLuyKeThanhToanNamTruocDelete,
			SUM(ISNULL(FTamUngTheoCheDoChuaThuHoiNamTruoc, 0)) FTamUngTheoCheDoChuaThuHoiNamTruoc,
			SUM(ISNULL(FTamUngTheoCheDoChuaThuHoiNamTruocDelete, 0)) FTamUngTheoCheDoChuaThuHoiNamTruocDelete,
			SUM(ISNULL(fDieuChinhGiamNamTruoc, 0)) fDieuChinhGiamNamTruoc,
			SUM(ISNULL(fDieuChinhGiamNamTruocDelete, 0)) fDieuChinhGiamNamTruocDelete INTO #tmpNamTruocKB
		FROM
		(
			SELECT tmp.IIDDuAnID,
				   (CASE WHEN (sMaDich = '403' AND sMaNguonCha = '301' AND sMaTienTrinh = '100') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fLuyKeThanhToanNamTruoc,
				   (CASE WHEN (sMaNguon = '403' AND sMaNguonCha = '301' AND sMaTienTrinh = '300') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fLuyKeThanhToanNamTruocDelete,

				   (CASE WHEN (sMaDich = '413a' AND sMaNguonCha = '301' AND sMaTienTrinh = '100') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as FTamUngTheoCheDoChuaThuHoiNamTruoc,
				   (CASE WHEN (sMaNguon = '413a' AND sMaNguonCha = '301' AND sMaTienTrinh = '300') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as FTamUngTheoCheDoChuaThuHoiNamTruocDelete,

				   (CASE WHEN (sMaNguon = '211c' AND (sMaNguonCha IS NULL OR sMaNguonCha NOT IN ('121a', '131')) AND sMaTienTrinh = '100') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fDieuChinhGiamNamTruoc,
				   (CASE WHEN (sMaDich = '211c' AND (sMaNguonCha IS NULL OR sMaNguonCha NOT IN ('121a', '131')) AND sMaTienTrinh = '300') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fDieuChinhGiamNamTruocDelete
			FROM (SELECT DISTINCT * FROM #tmp) as tmp
			INNER JOIN VDT_TongHop_NguonNSDauTu as dt on tmp.IIDDuAnID = dt.iID_DuAnID
			WHERE dt.iNamKeHoach = @iNamKeHoach-1
			GROUP BY tmp.IIDDuAnID, sMaDich, sMaNguon, sMaNguonCha, sMaTienTrinh
		) as tbl
		 GROUP BY tbl.IIDDuAnID

		-- TongHopDuLieu nam nay
		SELECT tbl.IIDDuAnID,
			SUM(ISNULL(fTamUngNamTruocThuHoiNamNay, 0)) fTamUngNamTruocThuHoiNamNay,
			SUM(ISNULL(fTamUngNamTruocThuHoiNamNayDelete, 0)) fTamUngNamTruocThuHoiNamNayDelete,
			SUM(ISNULL(fKHVNamTruocChuyenNamNay, 0)) fKHVNamTruocChuyenNamNay,
			SUM(ISNULL(fKHVNamTruocChuyenNamNayDelete, 0)) fKHVNamTruocChuyenNamNayDelete,
			SUM(ISNULL(fTongThanhToanSuDungVonNamTruoc, 0)) fTongThanhToanSuDungVonNamTruoc,
			SUM(ISNULL(fTongThanhToanSuDungVonNamTruocDelete, 0)) fTongThanhToanSuDungVonNamTruocDelete,
			SUM(ISNULL(fTamUngNamNayDungVonNamTruoc, 0)) fTamUngNamNayDungVonNamTruoc,
			SUM(ISNULL(fTamUngNamNayDungVonNamTruocDelete, 0)) fTamUngNamNayDungVonNamTruocDelete,
			SUM(ISNULL(fThuHoiTamUngNamNayDungVonNamTruoc, 0)) fThuHoiTamUngNamNayDungVonNamTruoc,
			SUM(ISNULL(fThuHoiTamUngNamNayDungVonNamTruocDelete, 0)) fThuHoiTamUngNamNayDungVonNamTruocDelete,
			SUM(ISNULL(fKHVNamNay, 0)) fKHVNamNay,
			SUM(ISNULL(fKHVNamNayDelete, 0)) fKHVNamNayDelete,
			SUM(ISNULL(fTongThanhToanSuDungVonNamNay, 0)) fTongThanhToanSuDungVonNamNay,
			SUM(ISNULL(fTongThanhToanSuDungVonNamNayDelete, 0)) fTongThanhToanSuDungVonNamNayDelete,
			SUM(ISNULL(fThuHoiUngCacNamTruoc, 0)) fThuHoiUngCacNamTruoc,
			SUM(ISNULL(fThuHoiUngCacNamTruocDelete, 0)) fThuHoiUngCacNamTruocDelete,
			SUM(ISNULL(fKeHoachUngNamNay, 0)) fKeHoachUngNamNay,
			SUM(ISNULL(fKeHoachUngNamNayDelete, 0)) fKeHoachUngNamNayDelete,
			SUM(ISNULL(fTongTamUngNamNay, 0)) fTongTamUngNamNay,
			SUM(ISNULL(fTongTamUngNamNayDelete, 0)) fTongTamUngNamNayDelete,
			SUM(ISNULL(fTongThuHoiTamUngNamNay, 0)) fTongThuHoiTamUngNamNay,
			SUM(ISNULL(fTongThuHoiTamUngNamNayDelete, 0)) fTongThuHoiTamUngNamNayDelete,
			SUM(ISNULL(fThuHoiUngNamTruoc, 0)) fThuHoiUngNamTruoc,
			SUM(ISNULL(fThuHoiUngNamTruocDelete, 0)) fThuHoiUngNamTruocDelete,
			SUM(ISNULL(fTongThuHoiUngNamNay, 0)) fTongThuHoiUngNamNay,
			SUM(ISNULL(fTongThuHoiUngNamNayDelete, 0)) fTongThuHoiUngNamNayDelete INTO #tmpNamNayKB
		FROM
		(
			SELECT  tmp.IIDDuAnID,  
					(CASE WHEN (sMaNguon = '211a' AND iThuHoiTUCheDo = 1 AND ILoaiUng = 1 AND sMaNguonCha NOT IN ('121a', '131') AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTamUngNamTruocThuHoiNamNay,
					(CASE WHEN (sMaDich = '211a' AND iThuHoiTUCheDo = 1 AND ILoaiUng = 1 AND sMaNguonCha NOT IN ('121a', '131') AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTamUngNamTruocThuHoiNamNayDelete,
					
					(CASE WHEN sMaNguon = '111' AND sMaDich = '000' AND sMaTienTrinh = '100' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fKHVNamTruocChuyenNamNay,
					(CASE WHEN sMaDich = '111' AND sMaNguon = '000' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fKHVNamTruocChuyenNamNayDelete,
					
					(CASE WHEN (sMaDich = '201' AND sMaNguonCha = '111' AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThanhToanSuDungVonNamTruoc,
					(CASE WHEN (sMaNguon = '201' AND sMaNguonCha = '111' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThanhToanSuDungVonNamTruocDelete,
					
					(CASE WHEN (sMaDich = '211a' AND sMaNguonCha = '111' AND iThuHoiTUCheDo IS NULL AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTamUngNamNayDungVonNamTruoc,
					(CASE WHEN (sMaNguon = '211a' AND sMaNguonCha = '111' AND iThuHoiTUCheDo IS NULL AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTamUngNamNayDungVonNamTruocDelete,
					
					(CASE WHEN (sMaNguon = '211a' AND sMaNguonCha = '111' AND iThuHoiTUCheDo = 2 AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fThuHoiTamUngNamNayDungVonNamTruoc,
					(CASE WHEN (sMaDich = '211a' AND sMaNguonCha = '111' AND iThuHoiTUCheDo = 2 AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fThuHoiTamUngNamNayDungVonNamTruocDelete,
					
					(CASE WHEN sMaNguon = '101' AND sMaDich = '000' AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fKHVNamNay,
					(CASE WHEN sMaDich = '101' AND sMaNguon = '000' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fKHVNamNayDelete,
					
					(CASE WHEN (sMaDich = '201' AND sMaNguonCha = '101' AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThanhToanSuDungVonNamNay,
					(CASE WHEN (sMaNguon = '201' AND sMaNguonCha = '101' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThanhToanSuDungVonNamNayDelete,
					
					(CASE WHEN (sMaNguon = '211a' AND sMaNguonCha = '101' AND iThuHoiTUCheDo = 1 AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fThuHoiUngCacNamTruoc,
					(CASE WHEN (sMaDich = '211a' AND sMaNguonCha = '101' AND iThuHoiTUCheDo = 1 AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fThuHoiUngCacNamTruocDelete,
					
					(CASE WHEN (sMaDich = '121a' AND sMaNguonCha = '101' AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fKeHoachUngNamNay,
					(CASE WHEN (sMaNguon = '121a' AND sMaNguonCha = '101' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fKeHoachUngNamNayDelete,

					(CASE WHEN (sMaDich = '211a' AND sMaNguonCha = '101' AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongTamUngNamNay,
					(CASE WHEN (sMaNguon = '211a' AND sMaNguonCha = '101' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongTamUngNamNayDelete,
					
					(CASE WHEN (sMaNguon = '211a' AND sMaNguonCha = '101' AND iThuHoiTUCheDo = 2 AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThuHoiTamUngNamNay,
					(CASE WHEN (sMaDich = '211a' AND sMaNguonCha = '101' AND iThuHoiTUCheDo = 2 AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThuHoiTamUngNamNayDelete,

					(CASE WHEN (sMaNguon = '211a' AND sMaNguonCha = '111' AND iThuHoiTUCheDo = 1 AND sMaTienTrinh = '200') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fThuHoiUngNamTruoc,
					(CASE WHEN (sMaDich = '211a' AND sMaNguonCha = '111' AND iThuHoiTUCheDo = 1 AND sMaTienTrinh = '300') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fThuHoiUngNamTruocDelete,

					(CASE WHEN (sMaDich = '121a' AND sMaNguonCha = '101' AND sMaTienTrinh = (CASE WHEN iID_NguonVonID = 1 THEN '100' ELSE '200' END) AND iID_NguonVonID = @iIdNguonVon AND ISNULL(bKeHoach, 1) = (CASE WHEN iID_NguonVonID = 1 THEN ISNULL(bKeHoach, 1) ELSE 0 END)) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThuHoiUngNamNay,
				   (CASE WHEN (sMaNguon = '121a' AND sMaNguonCha = '101' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon AND ISNULL(bKeHoach, 1) = (CASE WHEN iID_NguonVonID = 1 THEN ISNULL(bKeHoach, 1) ELSE 0 END)) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThuHoiUngNamNayDelete
			FROM (SELECT DISTINCT * FROM #tmp) as tmp
			INNER JOIN VDT_TongHop_NguonNSDauTu as dt on tmp.IIDDuAnID = dt.iID_DuAnID
			WHERE dt.iNamKeHoach = @iNamKeHoach
			GROUP BY tmp.IIDDuAnID, sMaDich, sMaNguon, sMaNguonCha, iID_NguonVonID, sMaTienTrinh, iThuHoiTUCheDo, ILoaiUng, bKeHoach
		) as tbl
		GROUP BY tbl.IIDDuAnID
	
	END
	
	-- co quan tai chinh
	BEGIN
		SELECT tbl.IIDDuAnID, 
				SUM(ISNULL(fLuyKeThanhToanNamTruoc, 0)) fLuyKeThanhToanNamTruoc,
				SUM(ISNULL(fLuyKeThanhToanNamTruocDelete, 0)) fLuyKeThanhToanNamTruocDelete,
				
				SUM(ISNULL(FTamUngTheoCheDoChuaThuHoiNamTruoc, 0)) FTamUngTheoCheDoChuaThuHoiNamTruoc,
				SUM(ISNULL(FTamUngTheoCheDoChuaThuHoiNamTruocDelete, 0)) FTamUngTheoCheDoChuaThuHoiNamTruocDelete,
				SUM(ISNULL(fDieuChinhGiamNamTruoc, 0)) fDieuChinhGiamNamTruoc,
				SUM(ISNULL(fDieuChinhGiamNamTruocDelete, 0)) fDieuChinhGiamNamTruocDelete INTO #tmpNamTruoc
			FROM
			(
				-- TongHopDuLieu nam truoc
				SELECT tmp.IIDDuAnID,
					(CASE WHEN (sMaDich = '404' AND sMaNguonCha = '302' AND sMaTienTrinh = '100') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fLuyKeThanhToanNamTruoc,
					(CASE WHEN (sMaNguon = '404' AND sMaNguonCha = '302' AND sMaTienTrinh = '300') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fLuyKeThanhToanNamTruocDelete,

					

					(CASE WHEN (sMaDich = '414a' AND sMaNguonCha = '302' AND sMaTienTrinh = '100') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as FTamUngTheoCheDoChuaThuHoiNamTruoc,
					(CASE WHEN (sMaNguon = '414a' AND sMaNguonCha = '302' AND sMaTienTrinh = '300') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as FTamUngTheoCheDoChuaThuHoiNamTruocDelete,

					(CASE WHEN (sMaNguon = '212c' AND (sMaNguonCha IS NULL OR sMaNguonCha NOT IN ('122a', '132')) AND sMaTienTrinh = '100') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fDieuChinhGiamNamTruoc,
					(CASE WHEN (sMaDich = '212c' AND (sMaNguonCha IS NULL OR sMaNguonCha NOT IN ('122a', '132')) AND sMaTienTrinh = '300') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fDieuChinhGiamNamTruocDelete
				FROM (SELECT DISTINCT * FROM #tmp) as tmp
				INNER JOIN VDT_TongHop_NguonNSDauTu as dt on tmp.IIDDuAnID = dt.iID_DuAnID
				WHERE dt.iNamKeHoach = @iNamKeHoach -1
				GROUP BY tmp.IIDDuAnID, sMaDich, sMaNguon, sMaNguonCha, sMaTienTrinh
			) as tbl
		GROUP BY tbl.IIDDuAnID

		-- TongHopDuLieu nam nay
		SELECT tbl.IIDDuAnID,
			SUM(ISNULL(fTamUngNamTruocThuHoiNamNay, 0)) fTamUngNamTruocThuHoiNamNay,
			SUM(ISNULL(fTamUngNamTruocThuHoiNamNayDelete, 0)) fTamUngNamTruocThuHoiNamNayDelete,
			SUM(ISNULL(fKHVNamTruocChuyenNamNay, 0)) fKHVNamTruocChuyenNamNay,
			SUM(ISNULL(fKHVNamTruocChuyenNamNayDelete, 0)) fKHVNamTruocChuyenNamNayDelete,
			SUM(ISNULL(fTongThanhToanSuDungVonNamTruoc, 0)) fTongThanhToanSuDungVonNamTruoc,
			SUM(ISNULL(fTongThanhToanSuDungVonNamTruocDelete, 0)) fTongThanhToanSuDungVonNamTruocDelete,
			SUM(ISNULL(fTamUngNamNayDungVonNamTruoc, 0)) fTamUngNamNayDungVonNamTruoc,
			SUM(ISNULL(fTamUngNamNayDungVonNamTruocDelete, 0)) fTamUngNamNayDungVonNamTruocDelete,
			SUM(ISNULL(fThuHoiTamUngNamNayDungVonNamTruoc, 0)) fThuHoiTamUngNamNayDungVonNamTruoc,
			SUM(ISNULL(fThuHoiTamUngNamNayDungVonNamTruocDelete, 0)) fThuHoiTamUngNamNayDungVonNamTruocDelete,
			SUM(ISNULL(fKHVNamNay, 0)) fKHVNamNay,
			SUM(ISNULL(fKHVNamNayDelete, 0)) fKHVNamNayDelete,
			SUM(ISNULL(fTongThanhToanSuDungVonNamNay, 0)) fTongThanhToanSuDungVonNamNay,
			SUM(ISNULL(fTongThanhToanSuDungVonNamNayDelete, 0)) fTongThanhToanSuDungVonNamNayDelete,
			SUM(ISNULL(fThuHoiUngCacNamTruoc, 0)) fThuHoiUngCacNamTruoc,
			SUM(ISNULL(fThuHoiUngCacNamTruocDelete, 0)) fThuHoiUngCacNamTruocDelete,
			SUM(ISNULL(fKeHoachUngNamNay, 0)) fKeHoachUngNamNay,
			SUM(ISNULL(fKeHoachUngNamNayDelete, 0)) fKeHoachUngNamNayDelete,
			SUM(ISNULL(fTongTamUngNamNay, 0)) fTongTamUngNamNay,
			SUM(ISNULL(fTongTamUngNamNayDelete, 0)) fTongTamUngNamNayDelete,
			SUM(ISNULL(fTongThuHoiTamUngNamNay, 0)) fTongThuHoiTamUngNamNay,
			SUM(ISNULL(fTongThuHoiTamUngNamNayDelete, 0)) fTongThuHoiTamUngNamNayDelete,
			SUM(ISNULL(fThuHoiUngNamTruoc, 0)) fThuHoiUngNamTruoc,
			SUM(ISNULL(fThuHoiUngNamTruocDelete, 0)) fThuHoiUngNamTruocDelete,
			SUM(ISNULL(fTongThuHoiUngNamNay, 0)) fTongThuHoiUngNamNay,
			SUM(ISNULL(fTongThuHoiUngNamNayDelete, 0)) fTongThuHoiUngNamNayDelete INTO #tmpNamNay
		FROM
		(
			SELECT  tmp.IIDDuAnID, 
					(CASE WHEN (sMaNguon = '212a' AND iThuHoiTUCheDo = 1 AND ILoaiUng = 1 AND sMaNguonCha NOT IN ('122a', '132') AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTamUngNamTruocThuHoiNamNay,
					(CASE WHEN (sMaDich = '212a' AND iThuHoiTUCheDo = 1 AND ILoaiUng = 1 AND sMaNguonCha NOT IN ('122a', '132') AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTamUngNamTruocThuHoiNamNayDelete,

					(CASE WHEN sMaNguon = '112' AND sMaDich = '000' AND sMaTienTrinh = '100' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fKHVNamTruocChuyenNamNay,
					(CASE WHEN sMaDich = '112' AND sMaNguon = '000' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fKHVNamTruocChuyenNamNayDelete,

					(CASE WHEN (sMaDich = '202' AND sMaNguonCha = '112' AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThanhToanSuDungVonNamTruoc,
					(CASE WHEN (sMaNguon = '202' AND sMaNguonCha = '112' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThanhToanSuDungVonNamTruocDelete,

					(CASE WHEN (sMaDich = '212a' AND sMaNguonCha = '112' AND iThuHoiTUCheDo IS NULL AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTamUngNamNayDungVonNamTruoc,
					(CASE WHEN (sMaNguon = '212a' AND sMaNguonCha = '112' AND iThuHoiTUCheDo IS NULL AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTamUngNamNayDungVonNamTruocDelete,

					(CASE WHEN (sMaNguon = '212a' AND sMaNguonCha = '112' AND iThuHoiTUCheDo = 2 AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fThuHoiTamUngNamNayDungVonNamTruoc,
					(CASE WHEN (sMaDich = '212a' AND sMaNguonCha = '112' AND iThuHoiTUCheDo = 2 AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fThuHoiTamUngNamNayDungVonNamTruocDelete,

					(CASE WHEN sMaNguon = '102' AND sMaDich = '000' AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fKHVNamNay,
					(CASE WHEN sMaDich = '102' AND sMaNguon = '000' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fKHVNamNayDelete,

					(CASE WHEN (sMaDich = '202' AND sMaNguonCha = '102' AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThanhToanSuDungVonNamNay,
					(CASE WHEN (sMaNguon = '202' AND sMaNguonCha = '102' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThanhToanSuDungVonNamNayDelete,
					
					(CASE WHEN (sMaNguon = '212a' AND sMaNguonCha = '102' AND iThuHoiTUCheDo = 1 AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fThuHoiUngCacNamTruoc,
					(CASE WHEN (sMaDich = '212a' AND sMaNguonCha = '102' AND iThuHoiTUCheDo = 1 AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fThuHoiUngCacNamTruocDelete,
					
					(CASE WHEN (sMaDich = '122a' AND sMaNguonCha = '102' AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fKeHoachUngNamNay,
					(CASE WHEN (sMaNguon = '122a' AND sMaNguonCha = '102' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fKeHoachUngNamNayDelete,

					(CASE WHEN (sMaDich = '212a' AND sMaNguonCha = '102' AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongTamUngNamNay,
					(CASE WHEN (sMaNguon = '212a' AND sMaNguonCha = '102' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongTamUngNamNayDelete,

					(CASE WHEN (sMaNguon = '212a' AND sMaNguonCha = '102' AND iThuHoiTUCheDo = 2 AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThuHoiTamUngNamNay,
					(CASE WHEN (sMaDich = '212a' AND sMaNguonCha = '102' AND iThuHoiTUCheDo = 2 AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThuHoiTamUngNamNayDelete,

					(CASE WHEN (sMaNguon = '212a' AND sMaNguonCha = '112' AND iThuHoiTUCheDo = 1 AND sMaTienTrinh = '200') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fThuHoiUngNamTruoc,
					(CASE WHEN (sMaDich = '212a' AND sMaNguonCha = '112' AND iThuHoiTUCheDo = 1 AND sMaTienTrinh = '300') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fThuHoiUngNamTruocDelete,
					
					(CASE WHEN (sMaDich = '122a' AND sMaNguonCha = '102' AND sMaTienTrinh = (CASE WHEN iID_NguonVonID = 1 THEN '100' ELSE '200' END) AND iID_NguonVonID = @iIdNguonVon  AND ISNULL(bKeHoach, 1) = (CASE WHEN iID_NguonVonID = 1 THEN ISNULL(bKeHoach, 1) ELSE 0 END)) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThuHoiUngNamNay,
					(CASE WHEN (sMaNguon = '122a' AND sMaNguonCha = '102' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon  AND ISNULL(bKeHoach, 1) = (CASE WHEN iID_NguonVonID = 1 THEN ISNULL(bKeHoach, 1) ELSE 0 END)) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThuHoiUngNamNayDelete
			FROM (SELECT DISTINCT * FROM #tmp) as tmp
			INNER JOIN VDT_TongHop_NguonNSDauTu as dt on tmp.IIDDuAnID = dt.iID_DuAnID
			WHERE dt.iNamKeHoach = @iNamKeHoach
			GROUP BY tmp.IIDDuAnID, sMaDich, sMaNguon, sMaNguonCha, iID_NguonVonID, sMaTienTrinh, iThuHoiTUCheDo, ILoaiUng, bKeHoach
		) as tbl
		GROUP BY tbl.IIDDuAnID
	END

	
	SELECT tmp.IIDDuAnID, tmp.SMaDuAn, tmp.SDiaDiem, tmp.STenDuAn, CAST(1 as int) as ICoQuanThanhToan,
		(ISNULL(nn.fKHVNamNay, 0) - ISNULL(nn.fKHVNamNayDelete, 0)) as FKHVNamNay, 
		(ISNULL(nn.fKHVNamTruocChuyenNamNay, 0) - ISNULL(nn.fKHVNamTruocChuyenNamNayDelete, 0)) as FKHVNamTruocChuyenNamNay, 
		(ISNULL(nn.fTamUngNamTruocThuHoiNamNay, 0) - ISNULL(nn.fTamUngNamTruocThuHoiNamNayDelete, 0)) as FTamUngNamTruocThuHoiNamNay,
		(ISNULL(nn.fThuHoiTamUngNamNayDungVonNamTruoc, 0) - ISNULL(nn.fThuHoiTamUngNamNayDungVonNamTruocDelete, 0)) as FThuHoiTamUngNamNayDungVonNamTruoc, 
		(ISNULL(nn.fTongTamUngNamNay, 0) - ISNULL(nn.fTongTamUngNamNayDelete, 0)) as FTongTamUngNamNay, 
		(((ISNULL(nn.fTongThanhToanSuDungVonNamNay, 0) - ISNULL(nn.fThuHoiUngCacNamTruoc, 0) + ISNULL(nn.fTongThuHoiUngNamNay, 0)) 
			- (ISNULL(nn.fTongThanhToanSuDungVonNamNayDelete, 0) - ISNULL(nn.fThuHoiUngCacNamTruocDelete, 0) + ISNULL(nn.fTongThuHoiUngNamNayDelete, 0)))) as FTongThanhToanSuDungVonNamNay,
		((ISNULL(nn.fTongThanhToanSuDungVonNamTruoc, 0) - ISNULL(nn.fThuHoiUngNamTruoc, 0)) - (ISNULL(nn.fTongThanhToanSuDungVonNamTruocDelete, 0) - ISNULL(nn.fThuHoiUngNamTruocDelete, 0))) as FTongThanhToanSuDungVonNamTruoc, 
		(ISNULL(nn.fTongThuHoiTamUngNamNay, 0) - ISNULL(nn.fTongThuHoiTamUngNamNayDelete, 0)) as FTongThuHoiTamUngNamNay, 
		(ISNULL(nn.fTamUngNamNayDungVonNamTruoc, 0) - ISNULL(nn.fTamUngNamNayDungVonNamTruocDelete, 0)) as FTamUngNamNayDungVonNamTruoc,
		(ISNULL(nt.fLuyKeThanhToanNamTruoc, 0) - ISNULL(nt.fLuyKeThanhToanNamTruocDelete, 0)) as FLuyKeThanhToanNamTruoc, 
		(ISNULL(nt.FTamUngTheoCheDoChuaThuHoiNamTruoc, 0) - ISNULL(nt.FTamUngTheoCheDoChuaThuHoiNamTruocDelete, 0)) as FTamUngTheoCheDoChuaThuHoiNamTruoc,
		ISNULL(dt.fTongMucDauTu, 0) as FTongMucDauTu,
		CAST(0 as float) as FGiaTriTamUngDieuChinhGiam,
		CAST(0 as float) as FGiaTriNamTruocChuyenNamSau,
		CAST(0 as float) as FGiaTriNamNayChuyenNamSau
	FROM (SELECT DISTINCT * FROM #tmp) as tmp
	LEFT JOIN #tmpNamNayKB as nn on tmp.IIDDuAnID = nn.IIDDuAnID
	LEFT JOIN #tmpNamTruocKB as nt on tmp.IIDDuAnID = nt.IIDDuAnID
	LEFT JOIN #tmpTongMucDauTu as dt on tmp.IIDDuAnID = dt.IIDDuAnID
	UNION ALL
	SELECT tmp.IIDDuAnID, tmp.SMaDuAn, tmp.SDiaDiem, tmp.STenDuAn, CAST(2 as int) as ICoQuanThanhToan,
		(ISNULL(nn.fKHVNamNay, 0) - ISNULL(nn.fKHVNamNayDelete, 0)) as FKHVNamNay, 
		(ISNULL(nn.fKHVNamTruocChuyenNamNay, 0) - ISNULL(nn.fKHVNamTruocChuyenNamNayDelete, 0)) as FKHVNamTruocChuyenNamNay, 
		(ISNULL(nn.fTamUngNamTruocThuHoiNamNay, 0) - ISNULL(nn.fTamUngNamTruocThuHoiNamNayDelete, 0)) as FTamUngNamTruocThuHoiNamNay,
		(ISNULL(nn.fThuHoiTamUngNamNayDungVonNamTruoc, 0) - ISNULL(nn.fThuHoiTamUngNamNayDungVonNamTruocDelete, 0)) as FThuHoiTamUngNamNayDungVonNamTruoc, 
		(ISNULL(nn.fTongTamUngNamNay, 0) - ISNULL(nn.fTongTamUngNamNayDelete, 0)) as FTongTamUngNamNay, 
		(((ISNULL(nn.fTongThanhToanSuDungVonNamNay, 0) - ISNULL(nn.fThuHoiUngCacNamTruoc, 0) + ISNULL(nn.fTongThuHoiUngNamNay, 0)) 
			- (ISNULL(nn.fTongThanhToanSuDungVonNamNayDelete, 0) - ISNULL(nn.fThuHoiUngCacNamTruocDelete, 0) + ISNULL(nn.fTongThuHoiUngNamNayDelete, 0)))) as FTongThanhToanSuDungVonNamNay,
		((ISNULL(nn.fTongThanhToanSuDungVonNamTruoc, 0) - ISNULL(nn.fThuHoiUngNamTruoc, 0)) - (ISNULL(nn.fTongThanhToanSuDungVonNamTruocDelete, 0) - ISNULL(nn.fThuHoiUngNamTruocDelete, 0))) as FTongThanhToanSuDungVonNamTruoc, 
		(ISNULL(nn.fTongThuHoiTamUngNamNay, 0) - ISNULL(nn.fTongThuHoiTamUngNamNayDelete, 0)) as FTongThuHoiTamUngNamNay, 
		(ISNULL(nn.fTamUngNamNayDungVonNamTruoc, 0) - ISNULL(nn.fTamUngNamNayDungVonNamTruocDelete, 0)) as FTamUngNamNayDungVonNamTruoc,
		(ISNULL(nt.fLuyKeThanhToanNamTruoc, 0) - ISNULL(nt.fLuyKeThanhToanNamTruocDelete, 0)) as FLuyKeThanhToanNamTruoc, 
		(ISNULL(nt.FTamUngTheoCheDoChuaThuHoiNamTruoc, 0) - ISNULL(nt.FTamUngTheoCheDoChuaThuHoiNamTruocDelete, 0)) as FTamUngTheoCheDoChuaThuHoiNamTruoc, 
		ISNULL(dt.fTongMucDauTu, 0) as FTongMucDauTu,
		CAST(0 as float) as FGiaTriTamUngDieuChinhGiam,
		CAST(0 as float) as FGiaTriNamTruocChuyenNamSau,
		CAST(0 as float) as FGiaTriNamNayChuyenNamSau
	FROM (SELECT DISTINCT * FROM #tmp) as tmp
	LEFT JOIN #tmpNamNay as nn on tmp.IIDDuAnID = nn.IIDDuAnID
	LEFT JOIN #tmpNamTruoc as nt on tmp.IIDDuAnID = nt.IIDDuAnID
	LEFT JOIN #tmpTongMucDauTu as dt on tmp.IIDDuAnID = dt.IIDDuAnID

	DROP TABLE #tmpNamTruoc
	DROP TABLE #tmpNamNay
	DROP TABLE #tmpNamTruocKB
	DROP TABLE #tmpNamNayKB
	DROP TABLE #tmp
	DROP TABLE #tmpTongMucDauTu
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_denghiqt_by_idduan]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_get_denghiqt_by_idduan]
@IdDuAn uniqueidentifier
AS
BEGIN

	SELECT * FROM VDT_QT_DeNghiQuyetToan
	WHERE iID_DuAnID = @IdDuAn
		
END
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_duan_in_phanbovon_new_2]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_vdt_get_duan_in_phanbovon_new_2]
	@idPhanBoVonDeXuat nvarchar(max),
	@nguonVonID int
AS
Begin
	select 
		distinct
		pbvdvct.iID_DuAnID,
		pbvdvct.iID_LoaiCongTrinh as iID_LoaiCongTrinh,
		pbvdvct.ILoaiDuAn as ILoaiDuAn,
		case when pbct.fThanhToanDeXuat = null then pbvdvct.fThanhToanDeXuat else pbct.fThanhToanDeXuat end as fThanhToanDeXuat
		into #tmp_duan
	from VDT_KHV_PhanBoVon_DonVi_ChiTiet_PheDuyet pbvdvct
	INNER JOIN VDT_DA_DuAn as da on pbvdvct.iID_DuAnID = da.iID_DuAnID
	left join VDT_KHV_PhanBoVon_ChiTiet pbct on pbct.iID_DuAnID = pbvdvct.iID_DuAnID
	where
		pbvdvct.iID_PhanBoVon_DonVi_PheDuyet_ID in (select  * from dbo.splitstring(@idPhanBoVonDeXuat));

	select
		distinct
		null as IdChungTu,
		null as IdChungTuParent,
		cast(1 as bit) as BActive,
		cast(1 as bit) as IsGoc,
		da.iID_DuAnID as iID_DuAnID,
		da.sTenDuAn as sTenDuAn,
		da.sMaDuAn as sMaDuAn,
		da.sTrangThaiDuAn as sTrangThaiDuAn,
		da.sKhoiCong as sKhoiCong,
		da.sKetThuc as sKetThuc,
		lct.sTenLoaiCongTrinh as sTenLoaiCongTrinh,
		da.sMaKetNoi as sMaKetNoi,
		cda.sTen as sTenCapPheDuyet,
		cast(0 as float) as fGiaTriDauTu,
		tmp_da.iID_LoaiCongTrinh as iID_LoaiCongTrinhID,
		da.iID_CapPheDuyetID as iID_CapPheDuyetID,
		lct.LNS as sLNS,
		lct.L as sL,
		lct.K as sK,
		lct.M as sM,
		lct.TM as sTM,
		lct.TTM as sTTM,
		lct.NG as sNG,
		cast(0 as float) as fVonDaBoTri,
		cast(0 as float) as fVonConLai,
		cast(0 as float) as fChiTieuBoXungTrongNam,
		cast(0 as float) as fNamTruocChuyenSang,
		cast(0 as float) as fThuUngXDCB,
		cast(0 as float) as fChiTieuNganSach,
		'' as sGhiChu,
		cast(0 as float) as FCapPhatTaiKhoBac,
		cast(0 as float) as FCapPhatTaiKhoBacDC,
		cast(0 as float) as FCapPhatBangLenhChi,
		cast(0 as float) as FCapPhatBangLenhChiDC,
		cast(0 as float) as FGiaTriThuHoiNamTruocKhoBac,
		cast(0 as float) as FGiaTriThuHoiNamTruocKhoBacDC,
		cast(0 as float) as FGiaTriThuHoiNamTruocLenhChi,
		cast(0 as float) as FGiaTriThuHoiNamTruocLenhChiDC,
		cast(0 as float) as fChiTieuGoc,
		tmp_da.ILoaiDuAn as ILoaiDuAn,
		dv.sTenDonVi as STenDonViThucHienDuAn,
		dv.iID_MaDonVi as IIdMaDonViThucHienDuAn,
		tmp_da.fThanhToanDeXuat as fThanhToanDeXuat
	from
		VDT_DA_DuAn da
	inner join
		#tmp_duan tmp_da
	on da.iID_DuAnID = tmp_da.iID_DuAnID
	left join 
		VDT_DM_PhanCapDuAn cda 
	on da.iID_CapPheDuyetID = cda.iID_PhanCapID
	left join
		VDT_DM_LoaiCongTrinh lct
	on tmp_da.iID_LoaiCongTrinh = lct.iID_LoaiCongTrinh
	left join
		VDT_DM_DonViThucHienDuAn dv
	on 
		da.iID_MaDonViThucHienDuAnID = dv.iID_MaDonVi

	drop table #tmp_duan

End
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_phe_duyet_quyet_toan]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_vdt_get_phe_duyet_quyet_toan]
	@YearOfWork int,
	@UserName nvarchar(100)
AS	 
BEGIN 
	SET NOCOUNT ON;

	DECLARE @CountDonViCha int;
	SELECT 
		@CountDonViCha = count(*) FROM (SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName AND iNamLamViec = @YearOfWork AND iTrangThai = 1) nddv
	INNER JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1 AND iLoai = 0) dv
	ON dv.iID_MaDonVi = nddv.iID_MaDonVi;

	select 
	qt.Id,
	qt.sSoQuyetDinh SoQuyetDinh,
	qt.dNgayQuyetDinh as NgayQuyetDinh,
	da.iID_DuAnID as IdDuAn,
	qt.iID_DeNghiQuyetToanID as IdDeNghiQuyetToan,
	da.sTenDuAn as TenDuAn,
	da.sMaDuAn as MaDuAn,
	dautu.TongMucDauTuPheDuyet as TongMucDauTuPheDuyet,
	qt.fTienQuyetToanPheDuyet as TienQuyetToanPheDuyet,
	qt.bKhoa as BKhoa,
	qt.iID_MaDonVi as MaDonVi,
	qt.sUserCreate as UserCreate,
	isnull(qt.fChiPhiThietHai, 0) as ChiPhiThietHai,
	isnull(qt.fChiPhiKhongTaoNenTaiSan, 0) as ChiPhiKhongTaoNenTaiSan,
	isnull(qt.fTaiSanDaiHanThuocCDTQuanLy, 0) as TaiSanDaiHanThuocCDTQuanLy,
	isnull(qt.fTaiSanDaiHanDonViKhacQuanLy, 0) as TaiSanDaiHanDonViKhacQuanLy,
	isnull(qt.fTaiSanNganHanThuocCDTQuanLy, 0) as TaiSanNganHanThuocCDTQuanLy,
	isnull(qt.fTaiSanNganHanDonViKhacQuanLy, 0) as TaiSanNganHanDonViKhacQuanLy
	FROM 
	VDT_QT_QuyetToan qt
	left join VDT_DA_DuAn da on qt.iID_DuAnID = da.iID_DuAnID
	left join 
	(
		select sum(qddt.fTongMucDauTuPheDuyet) as TongMucDauTuPheDuyet,da.iID_DuAnID FROM VDT_DA_DuAn da
		left join VDT_DA_QDDauTu qddt on qddt.iID_DuAnID = da.iID_DuAnID
		group by da.iID_DuAnID
	) dautu on dautu.iID_DuAnID = da.iID_DuAnID
	WHERE 
	(
		( EXISTS (SELECT * from f_split(qt.iID_MaDonVi) INTERSECT SELECT iID_MaDonVi FROM NguoiDung_DonVi  WHERE iID_MaNguoiDung = @UserName and iNamLamViec = @YearOfWork)
			AND (@CountDonViCha = 0)
		)
		OR (@CountDonViCha <> 0 AND qt.bKhoa = 1)
		OR 
		(   EXISTS (SELECT * from f_split(qt.iID_MaDonVi) INTERSECT SELECT iID_MaDonVi FROM NguoiDung_DonVi  WHERE iID_MaNguoiDung = @UserName and iNamLamViec = @YearOfWork)
			AND (@CountDonViCha <> 0)
		)
	)
	order by qt.dDateCreate desc
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_von_bo_tri_5_nam]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_vdt_get_von_bo_tri_5_nam]
	@lstId nvarchar(max),
	@YearPlan int
AS
BEGIN
	select ISNULL(SUM(khthddct.fHanMucDauTu),0)
	from VDT_KHV_KeHoach5Nam khthdd
	INNER JOIN VDT_KHV_KeHoach5Nam_ChiTiet khthddct
		on khthdd.iID_KeHoach5NamID = khthddct.iID_KeHoach5NamID
	where khthdd.iGiaiDoanTu <= @YearPlan AND khthdd.iGiaiDoanDen >= @YearPlan 
	and khthddct.iID_DuAnID = @lstId 
	--and khthddct.bActive = 1
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_getthongtriquyettoanchitiet]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_vdt_getthongtriquyettoanchitiet]
@iIdQuyetToanId uniqueidentifier
AS
BEGIN
	SELECT dt.iID_DuAnID as IIdDuAnId, da.STenDuAn, 
		da.iID_LoaiCongTrinhID as IIdLoaiCongTrinhId, lct.STenLoaiCongTrinh,
		lct.LNS as SLns, lct.L as SL, lct.K as SK, lct.M as SM, lct.TM as STm, lct.TTM as STtm, lct.NG as SNg,ml.iID as IIdMucLucNganSach,
		(ISNULL(dt.FThuHoiUngNamTrc, 0) + ISNULL(dt.fThanhToanKLHT_CTNamTrcChuyenSang, 0) + ISNULL(dt.fThanhToanKLHT_CTNamNay, 0)) as FSoTien, 
		NULL as IIdMucId, NULL as IIdTieuMucId, NULL as IIdTietMucId, NULL as IIdNganhId
	FROM VDT_QT_BCQuyetToanNienDo as tbl
	INNER JOIN VDT_QT_BCQuyetToanNienDo_ChiTiet_01 as dt on tbl.Id = dt.iID_BCQuyetToanNienDo
	INNER JOIN VDT_DA_DuAn as da on dt.iID_DuAnID = da.iID_DuAnID
	LEFT JOIN VDT_DM_LoaiCongTrinh as lct on da.iID_LoaiCongTrinhID = lct.iID_LoaiCongTrinh
	LEFT JOIN NS_MucLucNganSach as ml on ISNULL(lct.LNS, '') = ISNULL(ml.sLNS, '') AND ISNULL(lct.L, '') = ISNULL(ml.sL, '') AND ISNULL(lct.K, '') = ISNULL(ml.sK, '') 
		AND ISNULL(lct.M, '') = ISNULL(ml.sM, '') AND ISNULL(lct.TM, '') = ISNULL(ml.sTM, '') AND ISNULL(lct.TTM, '') = ISNULL(ml.sTTM, '') AND ISNULL(lct.NG, '') = ISNULL(ml.sNG, '') 
		AND ml.sTNG IS NULL AND ml.sTNG1 IS NULL AND ml.sTNG2 IS NULL AND ml.sTNG3 IS NULL AND ml.iNamLamViec = tbl.iNamKeHoach
	WHERE tbl.Id = @iIdQuyetToanId
END

GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_getthongtriquyettoanchitiet_by_id]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_vdt_getthongtriquyettoanchitiet_by_id]
@iIdThongTriId uniqueidentifier
AS
BEGIN
	SELECT dt.iID_DuAnID as IIdDuAnId, da.STenDuAn, 
		da.iID_LoaiCongTrinhID as IIdLoaiCongTrinhId, lct.STenLoaiCongTrinh,
		lct.LNS as SLns, lct.L as SL, lct.K as SK, lct.M as SM, lct.TM as STm, lct.TTM as STtm, lct.NG as SNg,
		ISNULL(dt.fSoTien, 0) as FSoTien, dt.iID_MucID as IIdMucId, dt.iID_TieuMucID as IIdTieuMucId, dt.iID_TietMucID as IIdTietMucId, dt.iID_NganhID as IIdNganhId
	FROM VDT_ThongTri_ChiTiet as dt
	INNER JOIN VDT_DA_DuAn as da on dt.iID_DuAnID = da.iID_DuAnID
	LEFT JOIN VDT_DM_LoaiCongTrinh as lct on dt.iID_LoaiCongTrinhID = lct.iID_LoaiCongTrinh
	WHERE dt.iID_ThongTriID = @iIdThongTriId
END
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_kehoachvonung_index]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_vdt_kehoachvonung_index]
AS
BEGIN
	--So lan dieu chinh
	WITH SoLieuDieuChinh AS 
	 (
		SELECT 
			ct.Id, ct.iID_ParentId, ct.sSoQuyetDinh, ct.iNamKeHoach
		FROM 
			VDT_KHV_KeHoachVonUng ct 
		WHERE 
			ct.iID_ParentId is not null

		UNION ALL
		SELECT 
			ct.Id, ct.iID_ParentId, ct.sSoQuyetDinh, ct.iNamKeHoach
		FROM 
			VDT_KHV_KeHoachVonUng ct JOIN SoLieuDieuChinh ctpr ON ct.iID_ParentId = ctpr.Id
		WHERE ct.iID_ParentId is not null
	  ),SoLanDieuChinh AS (
		   SELECT 
			sdc.iID_ParentId, sdc.iNamKeHoach, COUNT(sdc.Id) AS iSoLanDieuChinh
		  FROM 
			SoLieuDieuChinh sdc
		  GROUP BY sdc.iID_ParentId, sdc.iNamKeHoach
	  )


	SELECT tbl.*, dv.sTenDonVi as sTenDonViQuanLy, nv.sTen as sTenNguonVon, ml.sMoTa as sTenLoaiNguonVon, ('(' + isnull(cast(dc.iSoLanDieuChinh as nvarchar(max)), 0) + ')') as sSoLanDieuChinh
	FROM VDT_KHV_KeHoachVonUng as tbl
	LEFT JOIN DonVi as dv on tbl.iID_DonViQuanLyID = dv.iID_DonVi
	LEFT JOIN NguonNganSach as nv on tbl.iID_NguonVonID = nv.iID_MaNguonNganSach
	LEFT JOIN NS_MucLucNganSach as ml on tbl.iID_LoaiNguonVonID = ml.iId
	LEFT JOIN SoLanDieuChinh dc on tbl.iID_ParentId = dc.iID_ParentId and tbl.iNamKeHoach = dc.iNamKeHoach
	WHERE tbl.dDateDelete IS NULL
	ORDER BY tbl.dDateCreate DESC
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_qt_baocaoquyettoanniendo_phantich]    Script Date: 25/11/2022 7:43:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_vdt_qt_baocaoquyettoanniendo_phantich]
@iIdMaDonVi nvarchar(100),
@iNamKeHoach int,
@iIdNguonVon int
AS
BEGIN
	SELECT DISTINCT da.iID_DuAnID as IIDDuAnID, da.SMaDuAn, da.SDiaDiem, da.STenDuAn, ISNULL(pc.sMa, '') as sMaPhanCap INTO #tmpUnion
	FROM VDT_KHV_PhanBoVon as tbl
	INNER JOIN VDT_KHV_PhanBoVon_ChiTiet as dt on tbl.Id = dt.iID_PhanBoVonID
	INNER JOIN VDT_DA_DuAn as da on dt.iID_DuAnID = da.iID_DuAnID
	LEFT JOIN VDT_DM_PhanCapDuAn as pc on da.iID_CapPheDuyetID = pc.iID_PhanCapID
	WHERE da.iID_MaDonViThucHienDuAnID = @iIdMaDonVi AND tbl.iNamKeHoach = @iNamKeHoach AND tbl.iID_NguonVonID = @iIdNguonVon
	UNION ALL
	SELECT DISTINCT da.iID_DuAnID as IIDDuAnID, da.SMaDuAn, da.SDiaDiem, da.STenDuAn, ISNULL(pc.sMa, '') as sMaPhanCap
	FROM VDT_QT_BCQuyetToanNienDo as tbl
	INNER JOIN VDT_QT_BCQuyetToanNienDo_ChiTiet_01 as dt on tbl.Id = dt.iID_BCQuyetToanNienDo
	INNER JOIN VDT_DA_DuAn as da on dt.iID_DuAnID = da.iID_DuAnID
	LEFT JOIN VDT_DM_PhanCapDuAn as pc on da.iID_CapPheDuyetID = pc.iID_PhanCapID
	WHERE iNamKeHoach = @iNamKeHoach AND (ISNULL(dt.fGiaTriNamTruocChuyenNamSau, 0) <> 0 OR ISNULL(dt.fGiaTriNamNayChuyenNamSau, 0) <> 0)

	SELECT DISTINCT IIDDuAnID as IIDDuAnID, SMaDuAn, SDiaDiem, STenDuAn, sMaPhanCap  INTO #tmp
	FROM #tmpUnion

	-- Bao cao nam truoc
	SELECT iID_DuAnID, 
		SUM(ISNULL(dt.fDuToanCNSChuaGiaiNganTaiKB, 0)) as FDuToanCNSChuaGiaiNganTaiKB,		-- col 1
		SUM(ISNULL(dt.fDuToanCNSChuaGiaiNganTaiDV, 0)) as FDuToanCNSChuaGiaiNganTaiDv,		-- col 2
		SUM(ISNULL(dt.fDuToanCNSChuaGiaiNganTaiCuc, 0)) as FDuToanCNSChuaGiaiNganTaiCuc		-- col 3
		INTO #tmpBaoCaoNamTruoc
	FROM VDT_QT_BCQuyetToanNienDo as tbl
	INNER JOIN VDT_QT_BCQuyetToanNienDo_PhanTich as dt on tbl.Id = dt.iID_BCQuyetToanNienDo
	INNER JOIN #tmp as tmp on dt.iID_DuAnID = tmp.IIDDuAnID
	WHERE tbl.iNamKeHoach = (@iNamKeHoach - 1) AND tbl.iID_NguonVonID = @iIdNguonVon
	GROUP BY iID_DuAnID

	-- Gia tri nam truoc
	SELECT IIDDuAnID,
		(SUM(ISNULL(fLuyKeTamUngChuaThuHoi_KhvNam, 0)) - SUM(ISNULL(fLuyKeTamUngChuaThuHoi_KhvNamDelete, 0))) as fLuyKeTamUngChuaThuHoi_KhvNam,
		(SUM(ISNULL(fLuyKeTamUngChuaThuHoi_KhvNam_UQ, 0)) - SUM(ISNULL(fLuyKeTamUngChuaThuHoi_KhvNam_UQDelete, 0))) as fLuyKeTamUngChuaThuHoi_KhvNam_UQ ,
		(SUM(ISNULL(fTamUngChuaThuHoiKHVN, 0)) - SUM(ISNULL(fTamUngChuaThuHoiKHVNDelete, 0))) as fTamUngChuaThuHoiKHVN 
		INTO #tmpNamTruoc
	FROM
	(
		SELECT tmp.IIDDuAnID,
			(CASE WHEN sMaDich = '414a' AND sMaNguonCha = '302' AND sMaTienTrinh = '100' AND sMaPhanCap <> 'UQ' THEN ISNULL(fGiaTri, 0) ELSE 0 END) as fLuyKeTamUngChuaThuHoi_KhvNam,				-- col 18
			(CASE WHEN sMaNguon = '414a' AND sMaNguonCha = '302' AND sMaTienTrinh = '300' AND sMaPhanCap <> 'UQ' THEN ISNULL(fGiaTri, 0) ELSE 0 END) as fLuyKeTamUngChuaThuHoi_KhvNamDelete,

			(CASE WHEN sMaDich = '414a' AND sMaNguonCha = '302' AND sMaTienTrinh = '100' AND sMaPhanCap = 'UC' THEN ISNULL(fGiaTri, 0) ELSE 0 END) as fLuyKeTamUngChuaThuHoi_KhvNam_UQ,				-- col 19
			(CASE WHEN sMaNguon = '414a' AND sMaNguonCha = '302' AND sMaTienTrinh = '300' AND sMaPhanCap = 'UC' THEN ISNULL(fGiaTri, 0) ELSE 0 END) as fLuyKeTamUngChuaThuHoi_KhvNam_UQDelete,

			(CASE WHEN sMaDich = '413a' AND sMaNguonCha = '301' AND sMaTienTrinh = '100' THEN ISNULL(fGiaTri, 0) ELSE 0 END) as fTamUngChuaThuHoiKHVN,				-- col 19
			(CASE WHEN sMaNguon = '413a' AND sMaNguonCha = '301' AND sMaTienTrinh = '300' THEN ISNULL(fGiaTri, 0) ELSE 0 END) as fTamUngChuaThuHoiKHVNDelete

		FROM #tmp as tmp
		INNER JOIN VDT_TongHop_NguonNSDauTu as dt on tmp.IIDDuAnID = dt.iID_DuAnID
		WHERE dt.iID_NguonVonID = @iIdNguonVon AND dt.iNamKeHoach = (@iNamKeHoach - 1)
	) as tbl
	GROUP BY IIDDuAnID


	-- Gia tri nam nay
	SELECT IIDDuAnID,
		(SUM(ISNULL(fChiTieuNamNayKb, 0)) - SUM(ISNULL(fChiTieuNamNayKbDelete, 0))) as fChiTieuNamNayKb,
		(SUM(ISNULL(fChiTieuNamNayLc, 0)) - SUM(ISNULL(fChiTieuNamNayLcDelete, 0))) as fChiTieuNamNayLc,
		(SUM(ISNULL(fTamUngNamNayKB, 0)) - SUM(ISNULL(fTamUngNamNayKBDelete, 0))) as fTamUngNamNayKB,
		(SUM(ISNULL(fThuHoiUngNamNayKB, 0)) - SUM(ISNULL(fThuHoiUngNamNayKBDelete, 0))) as fThuHoiUngNamNayKB,
		(SUM(ISNULL(fTamUngNamNayLC, 0)) - SUM(ISNULL(fTamUngNamNayLCDelete, 0))) as fTamUngNamNayLC,
		(SUM(ISNULL(fThanhToanKHVNNamNay, 0)) - SUM(ISNULL(fThanhToanKHVNNamNayDelete, 0))) as fThanhToanKHVNNamNay,
		(SUM(ISNULL(fThanhToanKHVNChuyenSang, 0)) - SUM(ISNULL(fThanhToanKHVNChuyenSangDelete, 0))) as fThanhToanKHVNChuyenSang
		INTO #tmpNamNay
	FROM
	(
		SELECT tmp.IIDDuAnID,
			(CASE WHEN sMaNguon = '101' AND sMaTienTrinh = '200' THEN ISNULL(fGiaTri, 0) ELSE 0 END) as fChiTieuNamNayKb,		-- col 6
			(CASE WHEN sMaDich = '101' AND sMaTienTrinh = '300' THEN ISNULL(fGiaTri, 0) ELSE 0 END) as fChiTieuNamNayKbDelete,

			(CASE WHEN sMaNguon = '102' AND sMaTienTrinh = '200' THEN ISNULL(fGiaTri, 0) ELSE 0 END) as fChiTieuNamNayLc,		-- col 7
			(CASE WHEN sMaDich = '102' AND sMaTienTrinh = '300' THEN ISNULL(fGiaTri, 0) ELSE 0 END) as fChiTieuNamNayLcDelete,

			(CASE WHEN sMaDich = '202' AND sMaNguonCha = '112' AND sMaTienTrinh = '200' THEN ISNULL(fGiaTri, 0) ELSE 0 END) as fThanhToanKHVNChuyenSang,		-- col 10
			(CASE WHEN sMaNguon = '202' AND sMaNguonCha = '112' AND sMaTienTrinh = '300' THEN ISNULL(fGiaTri, 0) ELSE 0 END) as fThanhToanKHVNChuyenSangDelete,

			(CASE WHEN sMaNguon = '212a' AND iThuHoiTUCheDo in (1,2) AND sMaTienTrinh = '200' AND sMaNguonCha = '102' AND sMaPhanCap <> 'UQ' THEN ISNULL(fGiaTri, 0) ELSE 0 END) as fTamUngNamNayKB,		-- col 18
			(CASE WHEN sMaDich = '212a' AND iThuHoiTUCheDo in (1,2) AND sMaTienTrinh = '300' AND sMaNguonCha = '102' AND sMaPhanCap <> 'UQ' THEN ISNULL(fGiaTri, 0) ELSE 0 END) as fTamUngNamNayKBDelete,

			(CASE WHEN sMaNguon = '211a' AND iThuHoiTUCheDo in (1,2) AND sMaTienTrinh = '200' AND sMaNguonCha = '101' AND sMaPhanCap = 'UQ' THEN ISNULL(fGiaTri, 0) ELSE 0 END) as fThuHoiUngNamNayKB,		-- col 19
			(CASE WHEN sMaDich = '211a' AND iThuHoiTUCheDo in (1,2) AND sMaTienTrinh = '300' AND sMaNguonCha = '101' AND sMaPhanCap = 'UQ' THEN ISNULL(fGiaTri, 0) ELSE 0 END) as fThuHoiUngNamNayKBDelete,

			(CASE WHEN sMaDich = '212a' AND sMaTienTrinh = '200' AND sMaNguonCha = '102' AND sMaPhanCap <> 'UQ' THEN ISNULL(fGiaTri, 0) ELSE 0 END) as fTamUngNamNayLC,		-- col 18
			(CASE WHEN sMaNguon = '212a' AND sMaTienTrinh = '300' AND sMaNguonCha = '102' AND sMaPhanCap <> 'UQ' THEN ISNULL(fGiaTri, 0) ELSE 0 END) as fTamUngNamNayLCDelete, 

			(CASE WHEN sMaDich = '202' AND sMaTienTrinh = '200' AND sMaNguonCha = '102' THEN ISNULL(fGiaTri, 0) ELSE 0 END) as fThanhToanKHVNNamNay,		-- col 21
			(CASE WHEN sMaNguon = '202' AND sMaTienTrinh = '300' AND sMaNguonCha = '102' THEN ISNULL(fGiaTri, 0) ELSE 0 END) as fThanhToanKHVNNamNayDelete
		FROM #tmp as tmp
		INNER JOIN VDT_TongHop_NguonNSDauTu as dt on tmp.IIDDuAnID = dt.iID_DuAnID
		WHERE dt.iID_NguonVonID = @iIdNguonVon AND dt.iNamKeHoach = @iNamKeHoach
	) as tbl
	GROUP BY IIDDuAnID

	-- Thong tri nam nay
	SELECT IIDDuAnID,
		SUM(ISNULL(fCapHopThuc, 0)) as fCapHopThuc,
		SUM(ISNULL(fCapKinhPhi, 0)) as fCapKinhPhi,
		SUM(ISNULL(fCapHopThucBoXung, 0)) as fCapHopThucBoXung,
		SUM(ISNULL(fCapKinhPhiBoXung, 0)) as fCapKinhPhiBoXung,
		SUM(ISNULL(fCapHopThucChuyenSang, 0)) as fCapHopThucChuyenSang,
		SUM(ISNULL(fCapKinhPhiChuyenSang, 0)) as fCapKinhPhiChuyenSang
		INTO #tmpThongTri
	FROM
	(
		SELECT tmp.IIDDuAnID,
			(CASE WHEN tbl.iLoaiThongTri = 4 THEN ISNULL(dt.fSoTien, 0) ELSE 0 END) as fCapHopThuc,
			(CASE WHEN tbl.iLoaiThongTri = 3 THEN ISNULL(dt.fSoTien, 0) ELSE 0 END) as fCapKinhPhi,
			(CASE WHEN tbl.iLoaiThongTri = 4 AND tbl.iNamNganSach = 1 THEN ISNULL(dt.fSoTien, 0) ELSE 0 END) as fCapHopThucBoXung,
			(CASE WHEN tbl.iLoaiThongTri = 3 AND tbl.iNamNganSach = 1 THEN ISNULL(dt.fSoTien, 0) ELSE 0 END) as fCapKinhPhiBoXung,
			(CASE WHEN tbl.iLoaiThongTri = 4 AND tbl.iNamNganSach = 2 THEN ISNULL(dt.fSoTien, 0) ELSE 0 END) as fCapHopThucChuyenSang,
			(CASE WHEN tbl.iLoaiThongTri = 3 AND tbl.iNamNganSach = 2 THEN ISNULL(dt.fSoTien, 0) ELSE 0 END) as fCapKinhPhiChuyenSang
		FROM VDT_ThongTri as tbl
		INNER JOIN VDT_ThongTri_ChiTiet as dt on tbl.Id = dt.iID_ThongTriID
		INNER JOIN #tmp as tmp on dt.iID_DuAnID = tmp.IIDDuAnID
		LEFT JOIN NguonNganSach as nv on tbl.sMaNguonVon = nv.sMoTa
		WHERE tbl.iNamThongTri = @iNamKeHoach 
			AND (nv.iID_MaNguonNganSach IS NULL OR nv.iID_MaNguonNganSach = @iIdNguonVon)
	) as tbl
	GROUP BY IIDDuAnID



	SELECT tmp.* , da.sTenDuAn,
		ISNULL(bcNamTruoc.FDuToanCNSChuaGiaiNganTaiKB, 0) as FDuToanCnsChuaGiaiNganTaiKbNamTruoc,												-- col 1
		ISNULL(bcNamTruoc.fDuToanCNSChuaGiaiNganTaiDV, 0) as FDuToanCnsChuaGiaiNganTaiDvNamTruoc,												-- col 2
		ISNULL(bcNamTruoc.fDuToanCNSChuaGiaiNganTaiCuc, 0) as FDuToanCnsChuaGiaiNganTaiCucNamTruoc,												-- col 3
		ISNULL(nn.fChiTieuNamNayKB, 0) as FChiTieuNamNayKb,																						-- col 6
		ISNULL(nn.fChiTieuNamNayLC, 0) as FChiTieuNamNayLc,																						-- col 7
		(ISNULL(nn.fThanhToanKHVNChuyenSang, 0) + ISNULL(tt.fCapHopThucBoXung, 0) + ISNULL(fCapKinhPhiBoXung, 0)) as FSoCapNamTrcCs,			-- col 10
		(ISNULL(nn.fThanhToanKHVNNamNay, 0) + ISNULL(tt.fCapHopThucChuyenSang, 0) + ISNULL(tt.fCapKinhPhiChuyenSang, 0)) as FSoCapNamNay,		-- col 11
		CAST(0 as float) as FDnQuyetToanNamTrc,																									-- col 13
		CAST(0 as float) as FDnQuyetToanNamNay,																									-- col 14
		(ISNULL(fLuyKeTamUngChuaThuHoi_KhvNam, 0) - ISNULL(fTamUngNamNayKB, 0) + ISNULL(fTamUngNamNayLC, 0)) as FTuChuaThuHoiTaiCuc,			-- col 18
		(ISNULL(fLuyKeTamUngChuaThuHoi_KhvNam_UQ, 0) - ISNULL(fTamUngNamNayKB, 0) + ISNULL(fTamUngNamNayLC, 0)) as FTuChuaThuHoiTaiDonVi,		-- col 19
		(ISNULL(bcNamTruoc.fDuToanCNSChuaGiaiNganTaiCuc, 0) + ISNULL(nn.fChiTieuNamNayKB, 0)													-- col 21
			- (ISNULL(fLuyKeTamUngChuaThuHoi_KhvNam, 0) - ISNULL(fTamUngNamNayKB, 0) + ISNULL(fTamUngNamNayLC, 0))
			- fThanhToanKHVNNamNay) as FDuToanCnsChuaGiaiNganTaiCuc,
		(ISNULL(bcNamTruoc.FDuToanCNSChuaGiaiNganTaiDv, 0) + ISNULL(tt.fCapHopThuc, 0) + ISNULL(tt.fCapKinhPhi, 0)								-- col 22
			- (ISNULL(nn.fTamUngNamNayLC, 0)) + ISNULL(nn.fThanhToanKHVNNamNay, 0) - ISNULL(nn.fTamUngNamNayKB, 0)) as FDuToanCnsChuaGiaiNganTaiDv	,																						
		(ISNULL(bcNamTruoc.fDuToanCNSChuaGiaiNganTaiKB, 0) + ISNULL(nn.fChiTieuNamNayKB, 0) - ISNULL(tt.fCapHopThuc, 0)) as FDuToanCnsChuaGiaiNganTaiKb	,
		CAST(0 as float) as FDuToanThuHoi
	FROM #tmp as tmp
	INNER JOIN VDT_DA_DuAn as da on tmp.IIDDuAnID = da.iID_DuAnID
	LEFT JOIN #tmpBaoCaoNamTruoc as bcNamTruoc on tmp.IIDDuAnID = bcNamTruoc.iID_DuAnID
	LEFT JOIN #tmpThongTri as tt on tmp.IIDDuAnID = tt.IIDDuAnID
	LEFT JOIN #tmpNamNay as nn on tmp.IIDDuAnID = nn.IIDDuAnID
	LEFT JOIN #tmpNamTruoc as nt on tmp.IIDDuAnID = nt.IIDDuAnID

	DROP TABLE #tmpNamNay
	DROP TABLE #tmpNamTruoc
	DROP TABLE #tmpUnion
	DROP TABLE #tmp
END
;
;
GO

IF NOT EXISTS (SELECT * FROM TL_Bao_Cao WHERE Ma_BaoCao=N'4')
INSERT [dbo].[TL_Bao_Cao] ([Id], [Ma_BaoCao], [Ten_BaoCao], [Ma_Parent], [IsParent], [Note]) VALUES (N'cb63cc92-7409-ec11-80d1-005056b53cfb', N'4', N'Danh sách chi trả cá nhân (Chi tiết theo Tên & STK)', N'15', 0, NULL)

UPDATE TL_DM_CapBac set Bhxh_Cq = 0.1750, Bhyt_Cq = 0.030 where Parent in (3.1, 3.2, 3.3);

IF EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[NH_TH_TongHop]') 
         AND name = 'iID_TiGia'
)
ALTER TABLE [dbo].[NH_TH_TongHop]
DROP COLUMN [iID_TiGia];
ALTER TABLE [dbo].[NH_TH_TongHop]
ADD [iID_TiGia] [uniqueidentifier] NULL;
GO

delete from TL_PhuCap_MLNS where Ma_PhuCap in ('BHCN_TT', 'BHYTCN_TT') and Nam in ('2022', '2023')
GO


--thêm mới cấu hình chỉ tiêu lương cho BHXHDV_TT

IF NOT EXISTS (SELECT * FROM TL_PhuCap_MLNS WHERE Ma_Cb='1' AND Ma_PhuCap='BHXHDV_TT')
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (N'44b4f641-76f7-4ad9-8509-1220ea8e87ec', NULL, NULL, NULL, NULL, N'64118331-2dbc-4fc9-9208-361dba90393e', N'ec4c6d03-e9f6-415a-903b-2b65b6591032', N'ce2e2fbb-bc23-4c26-bf3c-89aa7d66f2fc', N'1540e36e-f290-4266-8c35-65c96eb47b5e', N'011', N'010', N'1010000', N'6300', N'CACH0', N'1', NULL, N'BHXHDV_TT', N'Sĩ quan', 2022, N'00', N'Sĩ quan', N'Bảo hiểm xã hội đơn vị đóng (thành tiền)', N'6301', N'10', N'admin', NULL, N'1010000-010-011-6300-6301-10-00', NULL, NULL, NULL, NULL)
GO

IF NOT EXISTS (SELECT * FROM TL_PhuCap_MLNS WHERE Ma_Cb='2' AND Ma_PhuCap='BHXHDV_TT')
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (N'502f83e9-f6f6-414e-91a7-3e381849109f', NULL, NULL, NULL, NULL, N'64118331-2dbc-4fc9-9208-361dba90393e', N'e9973c97-6aea-4720-8079-0db60a481aab', N'ce2e2fbb-bc23-4c26-bf3c-89aa7d66f2fc', N'1540e36e-f290-4266-8c35-65c96eb47b5e', N'011', N'010', N'1010000', N'6300', N'CACH0', N'2', NULL, N'BHXHDV_TT', N'QNCN', 2022, N'00', N'QNCN', N'Bảo hiểm xã hội đơn vị đóng (thành tiền)', N'6301', N'20', N'admin', NULL, N'1010000-010-011-6300-6301-20-00', NULL, NULL, NULL, NULL)
GO

IF NOT EXISTS (SELECT * FROM TL_PhuCap_MLNS WHERE Ma_Cb='4' AND Ma_PhuCap='BHXHDV_TT')
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (N'4d5435ba-a296-4c65-a796-16bd80847d6d', NULL, NULL, NULL, NULL, N'64118331-2dbc-4fc9-9208-361dba90393e', N'a68a9084-5a99-4b8b-a8b2-b2a356d86709', N'ce2e2fbb-bc23-4c26-bf3c-89aa7d66f2fc', N'1540e36e-f290-4266-8c35-65c96eb47b5e', N'011', N'010', N'1010000', N'6300', N'CACH0', N'4', NULL, N'BHXHDV_TT', N'HSQ-CS', 2022, N'00', N'HSQ-CS', N'Bảo hiểm xã hội đơn vị đóng (thành tiền)', N'6301', N'80', N'admin', NULL, N'1010000-010-011-6300-6301-80-00', NULL, NULL, NULL, NULL)
GO

IF NOT EXISTS (SELECT * FROM TL_PhuCap_MLNS WHERE Ma_Cb='3.3' AND Ma_PhuCap='BHXHDV_TT')
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (N'94e66208-9a7d-4d69-8aae-024e5007bbb8', NULL, NULL, NULL, NULL, N'64118331-2dbc-4fc9-9208-361dba90393e', N'37dd0fd9-0b63-4673-86da-3773dea747a7', N'ce2e2fbb-bc23-4c26-bf3c-89aa7d66f2fc', N'1540e36e-f290-4266-8c35-65c96eb47b5e', N'011', N'010', N'1010000', N'6300', N'CACH0', N'3.3', NULL, N'BHXHDV_TT', N'VCQP', 2022, N'00', N'VCQP', N'Bảo hiểm xã hội đơn vị đóng (thành tiền)', N'6301', N'30', N'admin', NULL, N'1010000-010-011-6300-6301-30-00', NULL, NULL, NULL, NULL)
GO

--thêm mới cấu hình chỉ tiêu lương cho BHYTDV_TT
IF NOT EXISTS (SELECT * FROM TL_PhuCap_MLNS WHERE Ma_Cb='4' AND Ma_PhuCap='BHYTDV_TT')
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (N'20b962d7-027d-4b70-bbd2-8eff899a32f5', NULL, NULL, NULL, NULL, N'64118331-2dbc-4fc9-9208-361dba90393e', N'745c8b0b-fbe1-4efa-b048-db164d82dd27', N'ce2e2fbb-bc23-4c26-bf3c-89aa7d66f2fc', N'a4b656c1-8abc-4633-833a-b538c05fa56b', N'011', N'010', N'1010000', N'6300', N'CACH0', N'4', NULL, N'BHYTDV_TT', N'Bảo hiểm y tế - Hạ sĩ quan, binh sĩ', 2022, N'00', N'Bảo hiểm y tế - Hạ sĩ quan, binh sĩ', N'Bảo hiểm y tế đơn vị đóng (thành tiền)', N'6302', N'80', N'admin', NULL, N'1010000-010-011-6300-6302-80-00', NULL, NULL, NULL, NULL)
GO

IF NOT EXISTS (SELECT * FROM TL_PhuCap_MLNS WHERE Ma_Cb='1' AND Ma_PhuCap='BHYTDV_TT')
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (N'dc10af2c-c701-470d-b9d9-da04f37cea61', NULL, NULL, NULL, NULL, N'64118331-2dbc-4fc9-9208-361dba90393e', N'4f1bb23d-411a-4601-8461-ce88526cba7c', N'ce2e2fbb-bc23-4c26-bf3c-89aa7d66f2fc', N'a4b656c1-8abc-4633-833a-b538c05fa56b', N'011', N'010', N'1010000', N'6300', N'CACH0', N'1', NULL, N'BHYTDV_TT', N'Bảo hiểm y tế - SQ', 2022, N'00', N'Bảo hiểm y tế - SQ', N'Bảo hiểm y tế đơn vị đóng (thành tiền)', N'6302', N'10', N'admin', NULL, N'1010000-010-011-6300-6302-10-00', NULL, NULL, NULL, NULL)
GO

IF NOT EXISTS (SELECT * FROM TL_PhuCap_MLNS WHERE Ma_Cb='2' AND Ma_PhuCap='BHYTDV_TT')
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (N'4335ad6b-2afe-4e62-8682-867f72404788', NULL, NULL, NULL, NULL, N'64118331-2dbc-4fc9-9208-361dba90393e', N'7a37d904-de94-4796-bec6-0ab64f21bcc5', N'ce2e2fbb-bc23-4c26-bf3c-89aa7d66f2fc', N'a4b656c1-8abc-4633-833a-b538c05fa56b', N'011', N'010', N'1010000', N'6300', N'CACH0', N'2', NULL, N'BHYTDV_TT', N'Bảo hiểm y tế - QNCN', 2022, N'00', N'Bảo hiểm y tế - QNCN', N'Bảo hiểm y tế đơn vị đóng (thành tiền)', N'6302', N'20', N'admin', NULL, N'1010000-010-011-6300-6302-20-00', NULL, NULL, NULL, NULL)
GO

--update--
update TL_PhuCap_MLNS set TL_PhuCap_MLNS.idPhuCap = b.id from TL_PhuCap_MLNS inner join TL_DM_PhuCap b
on TL_PhuCap_MLNS.Ma_PhuCap = b.Ma_PhuCap and TL_PhuCap_MLNS.idPhuCap in ('1540E36E-F290-4266-8C35-65C96EB47B5E', 'A4B656C1-8ABC-4633-833A-B538C05FA56B')
update TL_PhuCap_MLNS set TL_PhuCap_MLNS.idMlns = b.iID from TL_PhuCap_MLNS inner join NS_MucLucNganSach b
on TL_PhuCap_MLNS.XauNoiMa = b.sXauNoiMa and b.iNamLamViec = 2022
and TL_PhuCap_MLNS.idMlns in ('EC4C6D03-E9F6-415A-903B-2B65B6591032', 'E9973C97-6AEA-4720-8079-0DB60A481AAB', 'A68A9084-5A99-4B8B-A8B2-B2A356D86709', '37DD0FD9-0B63-4673-86DA-3773DEA747A7', '745C8B0B-FBE1-4EFA-B048-DB164D82DD27', '4F1BB23D-411A-4601-8461-CE88526CBA7C', '7A37D904-DE94-4796-BEC6-0AB64F21BCC5')
update TL_PhuCap_MLNS set TL_PhuCap_MLNS.idCachTinhLuong = b.id from TL_PhuCap_MLNS inner join TL_DM_ThemCachTinhLuong b
on TL_PhuCap_MLNS.Ma_CachTL = b.Ma_ThemCachTL and TL_PhuCap_MLNS.idCachTinhLuong = '64118331-2DBC-4FC9-9208-361DBA90393E'



update TL_DM_CapBac set Bhxh_Cq = 0.2250, Bhyt_Cq = 0.0450 where Ma_Cb like '0%'

IF NOT EXISTS (SELECT * FROM TL_Bao_Cao WHERE Ma_BaoCao=N'4')
INSERT [dbo].[TL_Bao_Cao] ([Id], [Ma_BaoCao], [Ten_BaoCao], [Ma_Parent], [IsParent], [Note]) VALUES (N'cb63cc92-7409-ec11-80d1-005056b53cfb', N'4', N'Danh sách chi trả cá nhân (Chi tiết theo Tên & STK)', N'15', 0, NULL)
GO

update TL_DM_CapBac set Bhxh_Cq = 0.1750, Bhyt_Cq = 0.030 where Parent in (3.1, 3.2, 3.3)

