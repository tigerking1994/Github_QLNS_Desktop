/****** Object:  StoredProcedure [dbo].[sp_nh_thuchien_ngansach_report]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_thuchien_ngansach_report]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_thuchien_ngansach_report]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thuchien_ngansach_index]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_thuchien_ngansach_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_thuchien_ngansach_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtricapphat_dspheduyetthanhtoan]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_thongtricapphat_dspheduyetthanhtoan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_thongtricapphat_dspheduyetthanhtoan]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtri_capphat_index]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_thongtri_capphat_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_thongtri_capphat_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtinthanhtoan_index]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_thongtinthanhtoan_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_thongtinthanhtoan_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtinnhathauhopdong_by_goithauid]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_thongtinnhathauhopdong_by_goithauid]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_thongtinnhathauhopdong_by_goithauid]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtinnguonvon_byidgoithau]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_thongtinnguonvon_byidgoithau]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_thongtinnguonvon_byidgoithau]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtinduan_index]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_thongtinduan_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_thongtinduan_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtin_hopdong_ngoaithuong_index]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_thongtin_hopdong_ngoaithuong_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_thongtin_hopdong_ngoaithuong_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtin_hopdong_index]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_thongtin_hopdong_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_thongtin_hopdong_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtin_hopdong_goithau_index]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_thongtin_hopdong_goithau_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_thongtin_hopdong_goithau_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtin_goithau_index]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_thongtin_goithau_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_thongtin_goithau_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_rpt_quyettoan_niendo_quy]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_rpt_quyettoan_niendo_quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_rpt_quyettoan_niendo_quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_rpt_quyettoan_niendo_nam]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_rpt_quyettoan_niendo_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_rpt_quyettoan_niendo_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_report_thongtri_capphat]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_report_thongtri_capphat]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_report_thongtri_capphat]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_quyettoan_niendo_tonghop_index]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_quyettoan_niendo_tonghop_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_quyettoan_niendo_tonghop_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_quyettoan_niendo_index]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_quyettoan_niendo_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_quyettoan_niendo_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_quyettoan_niendo_detail_fetch_data]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_quyettoan_niendo_detail_fetch_data]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_quyettoan_niendo_detail_fetch_data]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_quanli_giao_duan_index]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_quanli_giao_duan_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_quanli_giao_duan_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_qt_thong_tri_quyet_toan_index]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_qt_thong_tri_quyet_toan_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_qt_thong_tri_quyet_toan_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_qt_taisan_thongketaisan]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_qt_taisan_thongketaisan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_qt_taisan_thongketaisan]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_qddautu_nguonvon]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_qddautu_nguonvon]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_qddautu_nguonvon]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_qddautu_index]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_qddautu_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_qddautu_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_qddautu_hangmuc]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_qddautu_hangmuc]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_qddautu_hangmuc]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_qddautu_get_duan_in_khlcnhathau]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_qddautu_get_duan_in_khlcnhathau]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_qddautu_get_duan_in_khlcnhathau]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_qddautu_get_duan_By_iID_DonViID]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_qddautu_get_duan_By_iID_DonViID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_qddautu_get_duan_By_iID_DonViID]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_qddautu_chiphi]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_qddautu_chiphi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_qddautu_chiphi]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_phuongannhapkhau_index]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_phuongannhapkhau_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_phuongannhapkhau_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_pheduyet_quyettoandaht_index]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_pheduyet_quyettoandaht_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_pheduyet_quyettoandaht_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_pheduyet_quyettoandaht_detail]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_pheduyet_quyettoandaht_detail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_pheduyet_quyettoandaht_detail]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_pheduyet_quyettoandaht_create]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_pheduyet_quyettoandaht_create]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_pheduyet_quyettoandaht_create]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_nhucauchiquy_index]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_nhucauchiquy_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_nhucauchiquy_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_nhucauchiquy_chitiet_byIDHopDong]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_nhucauchiquy_chitiet_byIDHopDong]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_nhucauchiquy_chitiet_byIDHopDong]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_nhiemvuchi_findtree_by_ids]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_nhiemvuchi_findtree_by_ids]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_nhiemvuchi_findtree_by_ids]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_nhiemvuchi_bykehoachtongthe_donvi]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_nhiemvuchi_bykehoachtongthe_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_nhiemvuchi_bykehoachtongthe_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_mstnkehoachdathang_index]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_mstnkehoachdathang_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_mstnkehoachdathang_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_mstnkehoachdathang_bycondition]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_mstnkehoachdathang_bycondition]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_mstnkehoachdathang_bycondition]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_mstn_khdh_delete_by_id]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_mstn_khdh_delete_by_id]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_mstn_khdh_delete_by_id]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_kt_khoitaocapphat_chitiet_ById]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_kt_khoitaocapphat_chitiet_ById]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_kt_khoitaocapphat_chitiet_ById]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_kt_khoitaocapphat_chitiet]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_kt_khoitaocapphat_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_kt_khoitaocapphat_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_kt_khoi_tao_cap_phat]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_kt_khoi_tao_cap_phat]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_kt_khoi_tao_cap_phat]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_khtongthe_nhiemvuchi_bydonvi]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_khtongthe_nhiemvuchi_bydonvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_khtongthe_nhiemvuchi_bydonvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_khlcnt_delete_goithau_detail]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_khlcnt_delete_goithau_detail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_khlcnt_delete_goithau_detail]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_khlcnt_delete_goithau]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_khlcnt_delete_goithau]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_khlcnt_delete_goithau]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_khlcnhathau_index_2]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_khlcnhathau_index_2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_khlcnhathau_index_2]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_khlcnhathau_index]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_khlcnhathau_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_khlcnhathau_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_khlcnhathau_get_duan]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_khlcnhathau_get_duan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_khlcnhathau_get_duan]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_khlcnhathau_delete_by_id]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_khlcnhathau_delete_by_id]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_khlcnhathau_delete_by_id]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_khchitiet_index]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_khchitiet_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_khchitiet_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_kehoachtongthe_index]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_kehoachtongthe_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_kehoachtongthe_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_hopdongtrongnuoc_index]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_hopdongtrongnuoc_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_hopdongtrongnuoc_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_hdnk_delete_quyetdinhdampham]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_hdnk_delete_quyetdinhdampham]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_hdnk_delete_quyetdinhdampham]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_hdnk_cacquyetdinh_index]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_hdnk_cacquyetdinh_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_hdnk_cacquyetdinh_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_hangmuc_bygoithauid]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_hangmuc_bygoithauid]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_hangmuc_bygoithauid]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_hangmuc_bychiphiid]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_hangmuc_bychiphiid]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_hangmuc_bychiphiid]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_trongnuoc_index]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_goithau_trongnuoc_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_goithau_trongnuoc_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_nguonvon_by_khlcnt_listall]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_goithau_nguonvon_by_khlcnt_listall]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_goithau_nguonvon_by_khlcnt_listall]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_nguonvon_by_khlcnt]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_goithau_nguonvon_by_khlcnt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_goithau_nguonvon_by_khlcnt]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_nguonvon_by_goithau_listall]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_goithau_nguonvon_by_goithau_listall]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_goithau_nguonvon_by_goithau_listall]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_nguonvon_by_goithau]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_goithau_nguonvon_by_goithau]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_goithau_nguonvon_by_goithau]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_hangmuc_by_khlcnt]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_goithau_hangmuc_by_khlcnt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_goithau_hangmuc_by_khlcnt]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_hangmuc_by_goithau]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_goithau_hangmuc_by_goithau]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_goithau_hangmuc_by_goithau]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_detail]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_goithau_detail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_goithau_detail]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_chiphi_by_khlcnt_listall]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_goithau_chiphi_by_khlcnt_listall]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_goithau_chiphi_by_khlcnt_listall]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_chiphi_by_khlcnt]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_goithau_chiphi_by_khlcnt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_goithau_chiphi_by_khlcnt]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_chiphi_by_GoiThau_listall]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_goithau_chiphi_by_GoiThau_listall]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_goithau_chiphi_by_GoiThau_listall]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_chiphi_by_goithau]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_goithau_chiphi_by_goithau]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_goithau_chiphi_by_goithau]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_gethangmucbyidgoithau]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_gethangmucbyidgoithau]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_gethangmucbyidgoithau]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_gethangmuc_hopdongtrongnuoc_byidgoithau]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_gethangmuc_hopdongtrongnuoc_byidgoithau]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_gethangmuc_hopdongtrongnuoc_byidgoithau]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_get_duan_export_ctc]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_get_duan_export_ctc]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_get_duan_export_ctc]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_get_du_an_info_report]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_get_du_an_info_report]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_get_du_an_info_report]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_get_du_an_info_ngansach]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_get_du_an_info_ngansach]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_get_du_an_info_ngansach]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_get_data_report_tinh_hinh_thuc_hien_du_an1]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_get_data_report_tinh_hinh_thuc_hien_du_an1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_get_data_report_tinh_hinh_thuc_hien_du_an1]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_get_data_report_tinh_hinh_thuc_hien_du_an]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_get_data_report_tinh_hinh_thuc_hien_du_an]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_get_data_report_tinh_hinh_thuc_hien_du_an]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_find_one_by_IdKhTongThe_and_IdNhiemVuChi]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_find_one_by_IdKhTongThe_and_IdNhiemVuChi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_find_one_by_IdKhTongThe_and_IdNhiemVuChi]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_find_KHTongThe_nhiemVuChi_by_Id]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_find_KHTongThe_nhiemVuChi_by_Id]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_find_KHTongThe_nhiemVuChi_by_Id]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_find_KhTongThe_by_NvChiId]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_find_KhTongThe_by_NvChiId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_find_KhTongThe_by_NvChiId]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_find_KHTongThe_and_DmNhiemVuChi]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_find_KHTongThe_and_DmNhiemVuChi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_find_KHTongThe_and_DmNhiemVuChi]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_find_by_IdNhiemVuChi]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_find_by_IdNhiemVuChi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_find_by_IdNhiemVuChi]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_find_by_IdKhTongThe_and_maDonVi]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_find_by_IdKhTongThe_and_maDonVi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_find_by_IdKhTongThe_and_maDonVi]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_find_by_IdKhTongThe_and_IdDonVi]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_find_by_IdKhTongThe_and_IdDonVi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_find_by_IdKhTongThe_and_IdDonVi]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_find_by_IdKhTongThe]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_find_by_IdKhTongThe]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_find_by_IdKhTongThe]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_find_all_nvc_by_IdKhTongThe_giaiDoan]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_find_all_nvc_by_IdKhTongThe_giaiDoan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_find_all_nvc_by_IdKhTongThe_giaiDoan]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_find_all_donVi_by_KhTongTheId]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_find_all_donVi_by_KhTongTheId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_find_all_donVi_by_KhTongTheId]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_dutoan_nguonvon]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_dutoan_nguonvon]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_dutoan_nguonvon]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_dutoan_index]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_dutoan_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_dutoan_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_dutoan_in_khlcnt]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_dutoan_in_khlcnt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_dutoan_in_khlcnt]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_dutoan_hangmuc]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_dutoan_hangmuc]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_dutoan_hangmuc]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_dutoan_getall_tkktvatongdutoan]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_dutoan_getall_tkktvatongdutoan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_dutoan_getall_tkktvatongdutoan]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_dutoan_get_qddautu_hangmuc_by_qddautu_chiphi]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_dutoan_get_qddautu_hangmuc_by_qddautu_chiphi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_dutoan_get_qddautu_hangmuc_by_qddautu_chiphi]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_dutoan_get_dutoan_hangmuc_by_dutoan_chiphi]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_dutoan_get_dutoan_hangmuc_by_dutoan_chiphi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_dutoan_get_dutoan_hangmuc_by_dutoan_chiphi]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_dutoan_get_all_dutoan_nguonvon_by_duanid]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_dutoan_get_all_dutoan_nguonvon_by_duanid]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_dutoan_get_all_dutoan_nguonvon_by_duanid]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_dutoan_get_all_dutoan_nguonvon]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_dutoan_get_all_dutoan_nguonvon]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_dutoan_get_all_dutoan_nguonvon]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_dutoan_get_all_dutoan_chiphi_by_dutoan_id]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_dutoan_get_all_dutoan_chiphi_by_dutoan_id]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_dutoan_get_all_dutoan_chiphi_by_dutoan_id]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_dutoan_get_all_dutoan_chiphi_by_duan]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_dutoan_get_all_dutoan_chiphi_by_duan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_dutoan_get_all_dutoan_chiphi_by_duan]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_dutoan_delete_dutoan]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_dutoan_delete_dutoan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_dutoan_delete_dutoan]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_DuToan_chiphi]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_DuToan_chiphi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_DuToan_chiphi]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_duan_find_from_qddautu]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_duan_find_from_qddautu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_duan_find_from_qddautu]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_duan_find_from_dutoan]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_duan_find_from_dutoan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_duan_find_from_dutoan]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_duan_find_from_chutruong_dautu]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_duan_find_from_chutruong_dautu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_duan_find_from_chutruong_dautu]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_delete_duan_qddt_hd_qdk_by_idKhoiTao]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_delete_duan_qddt_hd_qdk_by_idKhoiTao]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_delete_duan_qddt_hd_qdk_by_idKhoiTao]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_da_delete_quyetdinhhopdong]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_da_delete_quyetdinhhopdong]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_da_delete_quyetdinhhopdong]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_da_delete_hopdong]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_da_delete_hopdong]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_da_delete_hopdong]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_chuyendulieu_quyettoan_index]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_chuyendulieu_quyettoan_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_chuyendulieu_quyettoan_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_chutruongdautu_index]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_chutruongdautu_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_chutruongdautu_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_chiphi_bygoithauid]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_chiphi_bygoithauid]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_chiphi_bygoithauid]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_chenhlechtigia_index_New_TH]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_chenhlechtigia_index_New_TH]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_chenhlechtigia_index_New_TH]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_chenhlechtigia_index]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_chenhlechtigia_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_chenhlechtigia_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_cacquyetdinhnguonvon_byidgoithau]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_cacquyetdinhnguonvon_byidgoithau]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_cacquyetdinhnguonvon_byidgoithau]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_cacquyetdinhchiphi_byidgoithau]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_cacquyetdinhchiphi_byidgoithau]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_cacquyetdinhchiphi_byidgoithau]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_baocao_nhucau_chitiet]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_baocao_nhucau_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_baocao_nhucau_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_get_nh_da_quyetdinhkhac_index]    Script Date: 11/3/2023 11:47:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_get_nh_da_quyetdinhkhac_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_get_nh_da_quyetdinhkhac_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_get_nh_da_quyetdinhkhac_index]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_get_nh_da_quyetdinhkhac_index]
	-- Add the parameters for the stored procedure here
	@iThuocMenu int
AS
BEGIN

	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT *, 
			qdk.ID as Id,
			qdk.iID_ParentID as IIdParentId,
			qdk.iID_DonViQuanLyID,
			dv.iID_MaDonVi as IIdMaDonViQuanLy,
			dv.sTenDonVi as STenDonVi,
			qdk.sTenQuyetDinh as STenQuyetDinh,
			qdk.sSoQuyetDinh as SSoQuyetDinh,
			qdk.sMoTa as SMoTa,
			qdk.iID_KHTT_NhiemVuChiID,
			nvChi.STenChuongTrinh,
			qdk.bIsActive AS BIsActive,
			qdk.bIsGoc AS BIsGoc,
			qdk.bIsKhoa AS BIsKhoa,
			qdk.bIsXoa AS BIsXoa ,
			qdk.dNgayQuyetDinh as DNgayQuyetDinh,
			qdk.dNgayTao as DNgayTao,
			qdk.sNguoiTao as SNguoiTao,
			qdk.dNgaySua as DNgaySua,
			qdk.sNguoiSua as SNguoiSua,
			qdk.dNgayXoa as  DNgayXoa,
			qdk.sNguoiXoa as SNguoiXoa,
			qdk.iLanDieuChinh as ILanDieuChinh,
			qdk.iThuocMenu as IThuocMenu,
			qdk.fGiaTriUSD as FGiaTriUSD,
			qdk.fGiaTriVND as FGiaTriVND


	FROM NH_DA_QuyetDinhKhac qdk
	LEFT  JOIN DonVi dv on dv.iID_DonVi = qdk.iID_DonViQuanLyID
	LEFT JOIN (SELECT  n.ID, n.iID_KHTongTheID, d.sTenNhiemVuChi AS STenChuongTrinh, n.iID_NhiemVuChiID
	 FROM NH_KHTongThe_NhiemVuChi AS n 
	 INNER JOIN NH_DM_NhiemVuChi AS d 
	 ON n.iID_NhiemVuChiID = d.ID
	) AS nvChi ON qdk.iID_KHTT_NhiemVuChiID = nvChi.ID

	WHERE qdk.iThuocMenu = @iThuocMenu
	ORDER BY qdk.dNgayTao DESC
END
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_baocao_nhucau_chitiet]    Script Date: 11/3/2023 11:47:41 AM ******/
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_cacquyetdinhchiphi_byidgoithau]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_cacquyetdinhchiphi_byidgoithau]
	@idGoiThau uniqueidentifier
AS BEGIN
	SELECT 
	chiphi.Id as Id,
	chiphi.fTienGoiThau_USD as FTienGoiThauUsd,
	chiphi.fTienGoiThau_VND as FTienGoiThauVnd,
	chiphi.fTienGoiThau_EUR as FTienGoiThauEur,
	chiphi.fTienGoiThau_NgoaiTeKhac as FTienGoiThauNgoaiTeKhac,
	quyetdinhchiphi.fGiaTriUSD as FGiaTriUSD,
	quyetdinhchiphi.fGiaTriVND as FGiaTriVND,
	quyetdinhchiphi.fGiaTriEUR as FGiaTriEUR,
	quyetdinhchiphi.fGiaTriNgoaiTeKhac as FGiaTriNgoaiTeKhac,
	dmChiPhi.sTenChiPhi as STenChiPhi,
	chiphi.iID_CacQuyetDinh_ChiPhiID as IIdCacQuyetDinhChiPhiId,
	chiphi.iID_QDDauTu_ChiPhiID as IIdQDDauTuChiPhiId,
	chiphi.iID_DuToan_ChiPhiID as IIdDuToanChiPhiId,
	chiphi.iID_GoiThauID as IIdGoiThauID
	FROM NH_DA_GoiThau_ChiPhi chiphi
	INNER JOIN NH_HDNK_CacQuyetDinh_ChiPhi quyetdinhchiphi 
		ON	chiphi.iID_CacQuyetDinh_ChiPhiID = quyetdinhchiphi.ID
	LEFT JOIN NH_DM_ChiPhi dmChiPhi
		ON quyetdinhchiphi.iID_ChiPhiID = dmChiPhi.iID_ChiPhi
	WHERE chiphi.iID_GoiThauID = @idGoiThau
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_cacquyetdinhnguonvon_byidgoithau]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_cacquyetdinhnguonvon_byidgoithau]
	@idGoiThau uniqueidentifier
AS BEGIN
	SELECT 
	NguonVon.iID_GoiThau_NguonVonID as Id,
	NguonVon.fTienGoiThau_USD as FTienGoiThauUsd,
	NguonVon.fTienGoiThau_VND as FTienGoiThauVnd,
	NguonVon.fTienGoiThau_EUR as FTienGoiThauEur,
	NguonVon.fTienGoiThau_NgoaiTeKhac as FTienGoiThauNgoaiTeKhac,
	quyetdinhnguonvon.fGiaTriUSD as FGiaTriUSD,
	quyetdinhnguonvon.fGiaTriVND as FGiaTriVND,
	quyetdinhnguonvon.fGiaTriEUR as FGiaTriEUR,
	quyetdinhnguonvon.fGiaTriNgoaiTeKhac as FGiaTriNgoaiTeKhac,
	nguonVon.iID_CacQuyetDinh_NguonVonID as IIdCacQuyetDinhNguonVonId,
	nguonVon.iID_QDDauTu_NguonVonID as IIdQDDauTuNguonVonId,
	nguonVon.iID_DuToan_NguonVonID as IIdDuToanNguonVonId,
	nguonVon.iID_GoiThauID as IIdGoiThauID, 
	NguonNganSach.sTen as STenNguonVon
	FROM NH_DA_GoiThau_NguonVon nguonVon
	INNER JOIN NH_HDNK_CacQuyetDinh_NguonVon quyetdinhnguonvon
		ON	NguonVon.iID_CacQuyetDinh_NguonVonID = quyetdinhnguonvon.ID
	LEFT JOIN NguonNganSach
		ON NguonNganSach.iID_MaNguonNganSach = nguonVon.iID_NguonVonID
	WHERE nguonVon.iID_GoiThauID = @idGoiThau
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_chenhlechtigia_index]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_chenhlechtigia_index] 
@iDDonVi uniqueidentifier,
@iDChuongTrinh uniqueidentifier,
@iDHopDong uniqueidentifier,
@iIDDuAn uniqueidentifier

AS  
BEGIN
	Create table #Temp
	(
		ID uniqueidentifier,
		sTen nvarchar(MAX),
		fTienKHTTBQPCapUSD float,
		fTienKHTTBQPCapVND float,
		fTienTheoDuAnUSD float,
		fTienTheoDuAnVND float,
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
	insert into #Temp (ID,sTen,fTienKHTTBQPCapUSD,fTienKHTTBQPCapVND,fTienTheoDuAnUSD, fTienTheoDuAnVND, fTienTheoHopDongUSD,fTienTheoHopDongVND,fKinhPhiDuocCapChoCDTUSD,fKinhPhiDuocCapChoCDTVND
			,fKinhPhiDaThanhToanUSD,fKinhPhiDaThanhToanVND,fTiGiaCLHopDongVsCDTUSD,fTiGiaCLHopDongVsCDTVND,fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,fTiGiaCLKinhPhiDuocCapVsGiaiNganVND
			,IDParent,IsHangCha)
	select ID, sTen,fTienKHTTBQPCapUSD,fTienKHTTBQPCapVND,fTienTheoDuAnUSD, fTienTheoDuAnVND,fTienTheoHopDongUSD,fTienTheoHopDongVND,fKinhPhiDuocCapChoCDTUSD,fKinhPhiDuocCapChoCDTVND
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
		NULL AS fTienTheoDuAnUSD,
		NULL AS fTienTheoDuAnVND,
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
	insert into #Temp (ID,sTen,fTienKHTTBQPCapUSD,fTienKHTTBQPCapVND,fTienTheoDuAnUSD, fTienTheoDuAnVND, fTienTheoHopDongUSD,fTienTheoHopDongVND,fKinhPhiDuocCapChoCDTUSD,fKinhPhiDuocCapChoCDTVND
			,fKinhPhiDaThanhToanUSD,fKinhPhiDaThanhToanVND,fTiGiaCLHopDongVsCDTUSD,fTiGiaCLHopDongVsCDTVND,fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,fTiGiaCLKinhPhiDuocCapVsGiaiNganVND
			,IDParent,IsHangCha)
	select ID, sTen,fTienKHTTBQPCapUSD,fTienKHTTBQPCapVND,fTienTheoDuAnUSD, fTienTheoDuAnVND,fTienTheoHopDongUSD,fTienTheoHopDongVND,fKinhPhiDuocCapChoCDTUSD,fKinhPhiDuocCapChoCDTVND
			,fKinhPhiDaThanhToanUSD,fKinhPhiDaThanhToanVND,fTiGiaCLHopDongVsCDTUSD,fTiGiaCLHopDongVsCDTVND,fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,fTiGiaCLKinhPhiDuocCapVsGiaiNganVND
			,IDParent,IsHangCha
	from(
		SELECT 
		hd.ID,
		hd.sTenHopDong as sTen,
		NULL AS fTienKHTTBQPCapUSD,
		NULL AS fTienKHTTBQPCapVND,
		NULL AS fTienTheoDuAnUSD,
		NULL AS fTienTheoDuAnVND,
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
	insert into #Temp (ID,sTen,fTienKHTTBQPCapUSD,fTienKHTTBQPCapVND,fTienTheoDuAnUSD, fTienTheoDuAnVND, fTienTheoHopDongUSD,fTienTheoHopDongVND,fKinhPhiDuocCapChoCDTUSD,fKinhPhiDuocCapChoCDTVND
			,fKinhPhiDaThanhToanUSD,fKinhPhiDaThanhToanVND,fTiGiaCLHopDongVsCDTUSD,fTiGiaCLHopDongVsCDTVND,fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,fTiGiaCLKinhPhiDuocCapVsGiaiNganVND
			,IDParent,IsHangCha)
	select ID, sTen,fTienKHTTBQPCapUSD,fTienKHTTBQPCapVND,fTienTheoDuAnUSD, fTienTheoDuAnVND,fTienTheoHopDongUSD,fTienTheoHopDongVND,fKinhPhiDuocCapChoCDTUSD,fKinhPhiDuocCapChoCDTVND
			,fKinhPhiDaThanhToanUSD,fKinhPhiDaThanhToanVND,fTiGiaCLHopDongVsCDTUSD,fTiGiaCLHopDongVsCDTVND,fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,fTiGiaCLKinhPhiDuocCapVsGiaiNganVND
			,IDParent,IsHangCha
	from(
		SELECT
		nvc.ID,
		dmnvc.sTenNhiemVuChi AS sTen,
		nvc.fGiaTriKH_BQP AS fTienKHTTBQPCapUSD,
		nvc.fGiaTriKH_BQP_VND AS fTienKHTTBQPCapVND,
		NULL AS fTienTheoDuAnUSD,
		NULL AS fTienTheoDuAnVND,		
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
	insert into #Temp (ID,sTen,fTienKHTTBQPCapUSD,fTienKHTTBQPCapVND,fTienTheoDuAnUSD, fTienTheoDuAnVND, fTienTheoHopDongUSD,fTienTheoHopDongVND,fKinhPhiDuocCapChoCDTUSD,fKinhPhiDuocCapChoCDTVND
			,fKinhPhiDaThanhToanUSD,fKinhPhiDaThanhToanVND,fTiGiaCLHopDongVsCDTUSD,fTiGiaCLHopDongVsCDTVND,fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,fTiGiaCLKinhPhiDuocCapVsGiaiNganVND
			,IDParent,IsHangCha)
	select ID, sTen,fTienKHTTBQPCapUSD,fTienKHTTBQPCapVND,fTienTheoDuAnUSD, fTienTheoDuAnVND,fTienTheoHopDongUSD,fTienTheoHopDongVND,fKinhPhiDuocCapChoCDTUSD,fKinhPhiDuocCapChoCDTVND
			,fKinhPhiDaThanhToanUSD,fKinhPhiDaThanhToanVND,fTiGiaCLHopDongVsCDTUSD,fTiGiaCLHopDongVsCDTVND,fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,fTiGiaCLKinhPhiDuocCapVsGiaiNganVND
			,IDParent,IsHangCha
	from(
		SELECT DISTINCT
		dv.iID_DonVi AS ID,
		dv.sTenDonVi AS sTen,
		NULL AS fTienKHTTBQPCapUSD,
		NULL AS fTienKHTTBQPCapVND,
		NULL AS fTienTheoDuAnUSD,
		NULL AS fTienTheoDuAnVND,
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
		
		  -- Insert dự án
INSERT INTO #Temp (ID,sTen,fTienKHTTBQPCapUSD,fTienKHTTBQPCapVND,fTienTheoDuAnUSD, fTienTheoDuAnVND,fTienTheoHopDongUSD,fTienTheoHopDongVND,fKinhPhiDuocCapChoCDTUSD,fKinhPhiDuocCapChoCDTVND ,fKinhPhiDaThanhToanUSD,fKinhPhiDaThanhToanVND,fTiGiaCLHopDongVsCDTUSD,fTiGiaCLHopDongVsCDTVND,fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,fTiGiaCLKinhPhiDuocCapVsGiaiNganVND ,IDParent,IsHangCha)
SELECT ID,
		 sTen,
		fTienKHTTBQPCapUSD,
		fTienKHTTBQPCapVND,
		fTienTheoDuAnUSD,
		fTienTheoDuAnVND,
		fTienTheoHopDongUSD,
		fTienTheoHopDongVND,
		fKinhPhiDuocCapChoCDTUSD,
		fKinhPhiDuocCapChoCDTVND ,
		fKinhPhiDaThanhToanUSD,
		fKinhPhiDaThanhToanVND,
		fTiGiaCLHopDongVsCDTUSD,
		fTiGiaCLHopDongVsCDTVND,
		fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,
		fTiGiaCLKinhPhiDuocCapVsGiaiNganVND ,
		IDParent,
		IsHangCha from
	(SELECT duan.ID AS ID,
		 duan.sTenDuAn AS sTen,
		 NULL AS fTienKHTTBQPCapUSD,
		 NULL AS fTienKHTTBQPCapVND,
		 duan.fUSD AS fTienTheoDuAnUSD,
		 duan.fVND AS fTienTheoDuAnVND,
		 NULL AS fTienTheoHopDongUSD,
		 NULL AS fTienTheoHopDongVND,
		 SUM(ISNULL(temp.fKinhPhiDuocCapChoCDTUSD,
		 0)) AS fKinhPhiDuocCapChoCDTUSD,
		 SUM(ISNULL(temp.fKinhPhiDuocCapChoCDTVND,
		 0)) AS fKinhPhiDuocCapChoCDTVND,
		 SUM(ISNULL(temp.fKinhPhiDaThanhToanUSD,
		 0)) AS fKinhPhiDaThanhToanUSD,
		 SUM(ISNULL(temp.fKinhPhiDaThanhToanVND,
		 0)) AS fKinhPhiDaThanhToanVND,
		 ISNULL(duan.fUSD,
		 0) - SUM(ISNULL(temp.fKinhPhiDuocCapChoCDTUSD,
		 0)) AS fTiGiaCLHopDongVsCDTUSD,
		 ISNULL(duan.fVND,
		 0) - SUM(ISNULL(temp.fKinhPhiDuocCapChoCDTVND,
		 0)) AS fTiGiaCLHopDongVsCDTVND,
		 SUM(ISNULL(temp.fKinhPhiDuocCapChoCDTUSD,
		 0)) - SUM(ISNULL(temp.fKinhPhiDaThanhToanUSD,
		 0)) AS fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,
		 SUM(ISNULL(temp.fKinhPhiDuocCapChoCDTVND,
		 0)) - SUM(ISNULL(temp.fKinhPhiDaThanhToanVND,
		 0)) AS fTiGiaCLKinhPhiDuocCapVsGiaiNganVND,
		 duan.iID_KHTT_NhiemVuChiID  AS IDParent,
		 CAST(1 AS bit) AS IsHangCha
	FROM NH_DA_DuAn AS duan
	INNER JOIN #Temp AS temp
		ON duan.iID_KHTT_NhiemVuChiID = temp.ID
	WHERE (@iIDDuAn IS NULL
			OR duan.ID = @iIDDuAn)
			AND (@iDChuongTrinh IS NULL
			OR duan.iID_KHTT_NhiemVuChiID = @iDChuongTrinh)
	GROUP BY  duan.ID, duan.sTenDuAn, duan.fUSD, duan.fVND, duan.iID_KHTT_NhiemVuChiID ) AS E

	;WITH  #Tree(ID,sTen,fTienKHTTBQPCapUSD,fTienKHTTBQPCapVND, fTienTheoDuAnUSD, fTienTheoDuAnVND, fTienTheoHopDongUSD,fTienTheoHopDongVND,fKinhPhiDuocCapChoCDTUSD,fKinhPhiDuocCapChoCDTVND
			,fKinhPhiDaThanhToanUSD,fKinhPhiDaThanhToanVND,fTiGiaCLHopDongVsCDTUSD,fTiGiaCLHopDongVsCDTVND,fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,fTiGiaCLKinhPhiDuocCapVsGiaiNganVND
			,IDParent,IsHangCha,position)
	AS (
		select temp.ID,temp.sTen,
			temp.fTienKHTTBQPCapUSD,temp.fTienKHTTBQPCapVND,
			temp.fTienTheoDuAnUSD, temp.fTienTheoDuAnVND,
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
			child.fTienTheoDuAnUSD,child.fTienTheoDuAnVND,
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
		fTienTheoDuAnUSD, fTienTheoDuAnVND,
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
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_chenhlechtigia_index_New_TH]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_chenhlechtigia_index_New_TH] 
		@iDDonVi uniqueidentifier,
		@iDChuongTrinh uniqueidentifier,
		@iDHopDong uniqueidentifier,
		@iIDDuAn uniqueidentifier,
		@iNamKeHoach int

AS  
BEGIN

	--Tinh toan data tong hop--
	DECLARE @lstMaNguon nvarchar(255) ='101,102,111,112,121,122,131,132,141,142';
	DECLARE @lstMaNguonPlusDC nvarchar(255) ='101,102,111,112,121,122';
	DECLARE @lstMaNguonPlusGN nvarchar(255) ='111,112,121,122,141,142';
	DECLARE @lstMaNguonMinus nvarchar(255) ='131,132'
	DECLARE @lstMaNguonNamTruoc nvarchar(255) ='306,308'
	--Get data tonghop
	SELECT th.Id, th.iID_ChungTu,th.fGiaTriUsd, th.fGiaTriVnd, th.sMaNguon, th.sMaDich, th.sMaNguonCha, th.sMaTienTrinh 
	into #DataTHNamNay
	from NH_TH_TongHop th
	where (th.sMaNguon in (Select * from f_split(@lstMaNguon)) OR th.sMaNguonCha in (Select * from f_split(@lstMaNguon)))
			AND th.iNamKeHoach = @iNamKeHoach;
	SELECT th.Id, th.iID_ChungTu,th.fGiaTriUsd, th.fGiaTriVnd, th.sMaNguon, th.sMaDich, th.sMaNguonCha, th.sMaTienTrinh 
	into #DataTHNamTruoc
	from NH_TH_TongHop th
	where (th.sMaNguon in (Select * from f_split(@lstMaNguonNamTruoc)) OR th.sMaNguonCha in (Select * from f_split(@lstMaNguonNamTruoc)))
			AND th.iNamKeHoach = @iNamKeHoach - 1;
	-- handler data
	--Data trừ nguồn 131,132
	(SELECT th.iID_ChungTu, SUM(ISNULL(th.fGiaTriUsd, 0)) as fGiaTriUsd, SUM(ISNULL(th.fGiaTriVnd, 0)) as fGiaTriVnd into #t1
	FROM #DataTHNamNay th	
	WHERE  th.sMaNguon in (Select * from f_split(@lstMaNguonMinus)) 
	GROUP BY th.iID_ChungTu)
	---
	(SELECT th.iID_ChungTu, SUM(ISNULL(th.fGiaTriUsd, 0)) as fGiaTriUsd, SUM(ISNULL(th.fGiaTriVnd, 0)) as fGiaTriVnd into #t2
	FROM #DataTHNamTruoc th	
	WHERE   th.sMaNguonCha in (Select * from f_split(@lstMaNguonMinus))
	GROUP BY th.iID_ChungTu)
	--> Output
	SELECT t1.iID_ChungTu, (t1.fGiaTriUsd - t2.fGiaTriUsd) as fGiaTriUsd, (t1.fGiaTriVnd - t2.fGiaTriVnd) as fGiaTriVnd INTO #tbl_data_minus from #t1 t1
	inner join #t2 t2 on t1.iID_ChungTu = t2.iID_ChungTu 
	DROP TABLE #t1,#t2;

	--Data nguồn 306
	(SELECT th.iID_ChungTu, SUM(ISNULL(th.fGiaTriUsd, 0)) as fGiaTriUsd, SUM(ISNULL(th.fGiaTriVnd, 0)) as fGiaTriVnd into #t3
	FROM #DataTHNamTruoc th	
	WHERE  th.sMaNguon = '306'
	GROUP BY th.iID_ChungTu)
	(SELECT th.iID_ChungTu, SUM(ISNULL(th.fGiaTriUsd, 0)) as fGiaTriUsd, SUM(ISNULL(th.fGiaTriVnd, 0)) as fGiaTriVnd into #t4
	FROM #DataTHNamTruoc th	
	WHERE  th.sMaNguonCha = '306'
	GROUP BY th.iID_ChungTu)
	--> Output
	SELECT t3.iID_ChungTu, (t3.fGiaTriUsd - t4.fGiaTriUsd) as fGiaTriUsd, (t3.fGiaTriVnd - t4.fGiaTriVnd) as fGiaTriVnd INTO #tbl_data_306 from #t3 t3
	inner join #t4 t4 on t3.iID_ChungTu = t4.iID_ChungTu 
	DROP TABLE #t3,#t4;

	--Data nguồn 308
	(SELECT th.iID_ChungTu, SUM(ISNULL(th.fGiaTriUsd, 0)) as fGiaTriUsd, SUM(ISNULL(th.fGiaTriVnd, 0)) as fGiaTriVnd into #t5
	FROM #DataTHNamTruoc th	
	WHERE  th.sMaNguon = '308'
	GROUP BY th.iID_ChungTu)
	(SELECT th.iID_ChungTu, SUM(ISNULL(th.fGiaTriUsd, 0)) as fGiaTriUsd, SUM(ISNULL(th.fGiaTriVnd, 0)) as fGiaTriVnd into #t6
	FROM #DataTHNamTruoc th	
	WHERE  th.sMaNguonCha = '308'
	GROUP BY th.iID_ChungTu)
	--> Output
	SELECT t5.iID_ChungTu, (t5.fGiaTriUsd - t6.fGiaTriUsd) as fGiaTriUsd, (t5.fGiaTriVnd - t6.fGiaTriVnd) as fGiaTriVnd INTO #tbl_data_308 
	from #t5 t5
	inner join #t6 t6 on t5.iID_ChungTu =t6.iID_ChungTu 
	DROP TABLE #t5,#t6;

	--Data nguồn 101,102,111,112,121,122 Thanh toan
	(SELECT th.iID_ChungTu, SUM(ISNULL(th.fGiaTriUsd, 0)) as fGiaTriUsd, SUM(ISNULL(th.fGiaTriVnd, 0)) as fGiaTriVnd into #t7
	FROM #DataTHNamNay th	
	WHERE  th.sMaNguon in (Select * from f_split(@lstMaNguonPlusDC))
	GROUP BY th.iID_ChungTu);

	(SELECT th.iID_ChungTu, SUM(ISNULL(th.fGiaTriUsd, 0)) as fGiaTriUsd, SUM(ISNULL(th.fGiaTriVnd, 0)) as fGiaTriVnd into #t8
	FROM #DataTHNamNay th	
	WHERE  th.sMaNguonCha in (Select * from f_split(@lstMaNguonPlusDC))
	GROUP BY th.iID_ChungTu);
	--> Output
	SELECT t7.iID_ChungTu, (t7.fGiaTriUsd - t8.fGiaTriUsd) as fGiaTriUsd, (t7.fGiaTriVnd - t8.fGiaTriVnd) as fGiaTriVnd INTO #tbl_data_TH_DC 
	from #t7 t7
	inner join #t8 t8 on t7.iID_ChungTu = t8.iID_ChungTu 
	DROP TABLE #t7,#t8;

	--Data nguồn 111,112,121,122,141,142 Thanh toan
	(SELECT th.iID_ChungTu, SUM(ISNULL(th.fGiaTriUsd, 0)) as fGiaTriUsd, SUM(ISNULL(th.fGiaTriVnd, 0)) as fGiaTriVnd into #t9
	FROM #DataTHNamNay th	
	WHERE  th.sMaNguon in (Select * from f_split(@lstMaNguonPlusGN))
	GROUP BY th.iID_ChungTu);

	(SELECT th.iID_ChungTu, SUM(ISNULL(th.fGiaTriUsd, 0)) as fGiaTriUsd, SUM(ISNULL(th.fGiaTriVnd, 0)) as fGiaTriVnd into #t10
	FROM #DataTHNamNay th	
	WHERE  th.sMaNguonCha in (Select * from f_split(@lstMaNguonPlusGN))
	GROUP BY th.iID_ChungTu);
	--> Output
	SELECT t9.iID_ChungTu, (t9.fGiaTriUsd - t10.fGiaTriUsd) as fGiaTriUsd, (t9.fGiaTriVnd - t10.fGiaTriVnd) as fGiaTriVnd INTO #tbl_data_TH_GN 
	from #t9 t9
	inner join #t10 t10 on t9.iID_ChungTu = t10.iID_ChungTu 
	DROP TABLE #t9,#t10;

	----> OUTPUT TongHop <-----
	with #tbl_lst_IdChungTu(iID_ChungTu)
	AS
	(
		select distinct iID_ChungTu from #DataTHNamNay
		union
		select distinct iID_ChungTu from #DataTHNamTruoc

	)
	SELECT pr.iID_ChungTu,
			(ISNULL(t306.fGiaTriUsd,0) + ISNULL(dc.fGiaTriUsd,0) - ISNULL(mn.fGiaTriUsd,0)) as fGiaTriKPCapUsd,
			(ISNULL(t306.fGiaTriVnd,0) + ISNULL(dc.fGiaTriVnd,0) - ISNULL(mn.fGiaTriVnd,0)) as fGiaTriKPCapVnd,
			(ISNULL(t308.fGiaTriUsd,0) + ISNULL(gn.fGiaTriUsd,0) - ISNULL(mn.fGiaTriUsd,0)) as fGiaTriKPGiaNganUsd,
			(ISNULL(t308.fGiaTriVnd,0) + ISNULL(gn.fGiaTriVnd,0) - ISNULL(mn.fGiaTriVnd,0)) as fGiaTriKPGiaNganVnd
		INTO #DataTongHop
	from #tbl_lst_IdChungTu pr
	LEFT JOIN #tbl_data_TH_DC dc on dc.iID_ChungTu = pr.iID_ChungTu
	LEFT JOIN #tbl_data_TH_GN gn on gn.iID_ChungTu = pr.iID_ChungTu
	LEFT JOIN #tbl_data_minus mn on mn.iID_ChungTu =pr.iID_ChungTu
	LEFT JOIN #tbl_data_306 t306 on t306.iID_ChungTu = pr.iID_ChungTu
	LEFT JOIN #tbl_data_308 t308 on t308.iID_ChungTu = pr.iID_ChungTu

	DROP TABLE #tbl_data_TH_DC;
	DROP TABLE #tbl_data_TH_GN;
	DROP TABLE #tbl_data_minus;
	DROP TABLE #tbl_data_306;
	DROP TABLE #tbl_data_308;
	---->End Tong Hop<------

	Create table #Temp
	(
		ID uniqueidentifier,
		sTen nvarchar(MAX),
		fTienKHTTBQPCapUSD float,
		fTienKHTTBQPCapVND float,
		fTienTheoDuAnUSD float,
		fTienTheoDuAnVND float,
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
	insert into #Temp (ID,sTen,fTienKHTTBQPCapUSD,fTienKHTTBQPCapVND,fTienTheoDuAnUSD, fTienTheoDuAnVND, fTienTheoHopDongUSD,fTienTheoHopDongVND,fKinhPhiDuocCapChoCDTUSD,fKinhPhiDuocCapChoCDTVND
			,fKinhPhiDaThanhToanUSD,fKinhPhiDaThanhToanVND,fTiGiaCLHopDongVsCDTUSD,fTiGiaCLHopDongVsCDTVND,fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,fTiGiaCLKinhPhiDuocCapVsGiaiNganVND
			,IDParent,IsHangCha)
	select ID, sTen,fTienKHTTBQPCapUSD,fTienKHTTBQPCapVND,fTienTheoDuAnUSD, fTienTheoDuAnVND,fTienTheoHopDongUSD,fTienTheoHopDongVND,fKinhPhiDuocCapChoCDTUSD,fKinhPhiDuocCapChoCDTVND
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
		NULL AS fTienTheoDuAnUSD,
		NULL AS fTienTheoDuAnVND,
		NULL AS fTienTheoHopDongUSD,
		NULL AS fTienTheoHopDongVND,
		--(case when tt.iCoQuanThanhToan = 1 then chitiet.fKinhPhiDuocCapChoCDTUSD 
		--	else (case when tt.iLoaiDeNghi = 1 then chitiet.fKinhPhiDuocCapChoCDTUSD else null end) end)
		-- AS fKinhPhiDuocCapChoCDTUSD,
		--(case when tt.iCoQuanThanhToan = 1 then chitiet.fKinhPhiDuocCapChoCDTVND 
		--	else (case when tt.iLoaiDeNghi = 1 then chitiet.fKinhPhiDuocCapChoCDTVND else null end) end)
		--AS fKinhPhiDuocCapChoCDTVND,
		--(case when tt.iCoQuanThanhToan = 1 then chitiet.fKinhPhiDaThanhToanUSD 
		--	else (case when tt.iLoaiDeNghi = 2 or tt.iLoaiDeNghi = 3 then chitiet.fKinhPhiDaThanhToanUSD else null end) end)
		--AS fKinhPhiDaThanhToanUSD,
		--(case when tt.iCoQuanThanhToan = 1 then chitiet.fKinhPhiDaThanhToanVND 
		--	else (case when tt.iLoaiDeNghi = 2 or tt.iLoaiDeNghi = 3 then chitiet.fKinhPhiDaThanhToanVND else null end) end)
		--AS fKinhPhiDaThanhToanVND,
		ISNULL(th.fGiaTriKPCapUsd, 0) as fKinhPhiDuocCapChoCDTUSD,
		ISNULL(th.fGiaTriKPCapVnd, 0) as fKinhPhiDuocCapChoCDTVND,
		ISNULL(th.fGiaTriKPGiaNganUsd, 0) as fKinhPhiDaThanhToanUSD,
		ISNULL(th.fGiaTriKPGiaNganVnd, 0) as fKinhPhiDaThanhToanVND,
		NULL AS fTiGiaCLHopDongVsCDTUSD,
		NULL AS fTiGiaCLHopDongVsCDTVND,
		NULL AS fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,
		NULL AS fTiGiaCLKinhPhiDuocCapVsGiaiNganVND,
		(case when tt.iID_HopDongID is null or tt.iID_HopDongID = CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER) then tt.iID_KHTongTheID else tt.iID_HopDongID end) as IDParent,
		CAST(0 AS bit) AS IsHangCha
		FROM NH_TT_ThanhToan AS tt 
		--INNER JOIN (
		--	SELECT 
		--	Sum(ISNULL(fPheDuyetCapKyNay_USD, 0)) AS fKinhPhiDuocCapChoCDTUSD, 
		--	Sum(ISNULL(fPheDuyetCapKyNay_VND, 0)) AS fKinhPhiDuocCapChoCDTVND, 
		--	Sum(ISNULL(fPheDuyetCapKyNay_USD, 0)) AS fKinhPhiDaThanhToanUSD,
		--	Sum(ISNULL(fPheDuyetCapKyNay_VND, 0)) AS fKinhPhiDaThanhToanVND,
		--	tt_ct.iID_DeNghiThanhToanID
		--	FROM NH_TT_ThanhToan_ChiTiet AS tt_ct
		--	GROUP BY tt_ct.iID_DeNghiThanhToanID
		--) AS chitiet ON chitiet.iID_DeNghiThanhToanID = tt.ID
		LEFT JOIN #DataTongHop th on th.iID_ChungTu = tt.ID
		WHERE (@iDHopDong IS NULL OR (tt.iID_HopDongID IS NOT NULL AND tt.iID_HopDongID = @iDHopDong))
	) AS A

	-- Insert hợp đồng
	insert into #Temp (ID,sTen,fTienKHTTBQPCapUSD,fTienKHTTBQPCapVND,fTienTheoDuAnUSD, fTienTheoDuAnVND, fTienTheoHopDongUSD,fTienTheoHopDongVND,fKinhPhiDuocCapChoCDTUSD,fKinhPhiDuocCapChoCDTVND
			,fKinhPhiDaThanhToanUSD,fKinhPhiDaThanhToanVND,fTiGiaCLHopDongVsCDTUSD,fTiGiaCLHopDongVsCDTVND,fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,fTiGiaCLKinhPhiDuocCapVsGiaiNganVND
			,IDParent,IsHangCha)
	select ID, sTen,fTienKHTTBQPCapUSD,fTienKHTTBQPCapVND,fTienTheoDuAnUSD, fTienTheoDuAnVND,fTienTheoHopDongUSD,fTienTheoHopDongVND,fKinhPhiDuocCapChoCDTUSD,fKinhPhiDuocCapChoCDTVND
			,fKinhPhiDaThanhToanUSD,fKinhPhiDaThanhToanVND,fTiGiaCLHopDongVsCDTUSD,fTiGiaCLHopDongVsCDTVND,fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,fTiGiaCLKinhPhiDuocCapVsGiaiNganVND
			,IDParent,IsHangCha
	from(
		SELECT 
		hd.ID,
		hd.sTenHopDong as sTen,
		NULL AS fTienKHTTBQPCapUSD,
		NULL AS fTienKHTTBQPCapVND,
		NULL AS fTienTheoDuAnUSD,
		NULL AS fTienTheoDuAnVND,
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
	insert into #Temp (ID,sTen,fTienKHTTBQPCapUSD,fTienKHTTBQPCapVND,fTienTheoDuAnUSD, fTienTheoDuAnVND, fTienTheoHopDongUSD,fTienTheoHopDongVND,fKinhPhiDuocCapChoCDTUSD,fKinhPhiDuocCapChoCDTVND
			,fKinhPhiDaThanhToanUSD,fKinhPhiDaThanhToanVND,fTiGiaCLHopDongVsCDTUSD,fTiGiaCLHopDongVsCDTVND,fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,fTiGiaCLKinhPhiDuocCapVsGiaiNganVND
			,IDParent,IsHangCha)
	select ID, sTen,fTienKHTTBQPCapUSD,fTienKHTTBQPCapVND,fTienTheoDuAnUSD, fTienTheoDuAnVND,fTienTheoHopDongUSD,fTienTheoHopDongVND,fKinhPhiDuocCapChoCDTUSD,fKinhPhiDuocCapChoCDTVND
			,fKinhPhiDaThanhToanUSD,fKinhPhiDaThanhToanVND,fTiGiaCLHopDongVsCDTUSD,fTiGiaCLHopDongVsCDTVND,fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,fTiGiaCLKinhPhiDuocCapVsGiaiNganVND
			,IDParent,IsHangCha
	from(
		SELECT
		nvc.ID,
		dmnvc.sTenNhiemVuChi AS sTen,
		nvc.fGiaTriKH_BQP AS fTienKHTTBQPCapUSD,
		nvc.fGiaTriKH_BQP_VND AS fTienKHTTBQPCapVND,
		NULL AS fTienTheoDuAnUSD,
		NULL AS fTienTheoDuAnVND,		
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
	insert into #Temp (ID,sTen,fTienKHTTBQPCapUSD,fTienKHTTBQPCapVND,fTienTheoDuAnUSD, fTienTheoDuAnVND, fTienTheoHopDongUSD,fTienTheoHopDongVND,fKinhPhiDuocCapChoCDTUSD,fKinhPhiDuocCapChoCDTVND
			,fKinhPhiDaThanhToanUSD,fKinhPhiDaThanhToanVND,fTiGiaCLHopDongVsCDTUSD,fTiGiaCLHopDongVsCDTVND,fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,fTiGiaCLKinhPhiDuocCapVsGiaiNganVND
			,IDParent,IsHangCha)
	select ID, sTen,fTienKHTTBQPCapUSD,fTienKHTTBQPCapVND,fTienTheoDuAnUSD, fTienTheoDuAnVND,fTienTheoHopDongUSD,fTienTheoHopDongVND,fKinhPhiDuocCapChoCDTUSD,fKinhPhiDuocCapChoCDTVND
			,fKinhPhiDaThanhToanUSD,fKinhPhiDaThanhToanVND,fTiGiaCLHopDongVsCDTUSD,fTiGiaCLHopDongVsCDTVND,fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,fTiGiaCLKinhPhiDuocCapVsGiaiNganVND
			,IDParent,IsHangCha
	from(
		SELECT DISTINCT
		dv.iID_DonVi AS ID,
		dv.sTenDonVi AS sTen,
		NULL AS fTienKHTTBQPCapUSD,
		NULL AS fTienKHTTBQPCapVND,
		NULL AS fTienTheoDuAnUSD,
		NULL AS fTienTheoDuAnVND,
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
		
		    -- Insert dự án
INSERT INTO #Temp (ID,sTen,fTienKHTTBQPCapUSD,fTienKHTTBQPCapVND,fTienTheoDuAnUSD, fTienTheoDuAnVND,fTienTheoHopDongUSD,fTienTheoHopDongVND,fKinhPhiDuocCapChoCDTUSD,fKinhPhiDuocCapChoCDTVND ,fKinhPhiDaThanhToanUSD,fKinhPhiDaThanhToanVND,fTiGiaCLHopDongVsCDTUSD,fTiGiaCLHopDongVsCDTVND,fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,fTiGiaCLKinhPhiDuocCapVsGiaiNganVND ,IDParent,IsHangCha)
SELECT ID,
		 sTen,
		fTienKHTTBQPCapUSD,
		fTienKHTTBQPCapVND,
		fTienTheoDuAnUSD,
		fTienTheoDuAnVND,
		fTienTheoHopDongUSD,
		fTienTheoHopDongVND,
		fKinhPhiDuocCapChoCDTUSD,
		fKinhPhiDuocCapChoCDTVND ,
		fKinhPhiDaThanhToanUSD,
		fKinhPhiDaThanhToanVND,
		fTiGiaCLHopDongVsCDTUSD,
		fTiGiaCLHopDongVsCDTVND,
		fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,
		fTiGiaCLKinhPhiDuocCapVsGiaiNganVND ,
		IDParent,
		IsHangCha from
	(SELECT duan.ID AS ID,
		 duan.sTenDuAn AS sTen,
		 NULL AS fTienKHTTBQPCapUSD,
		 NULL AS fTienKHTTBQPCapVND,
		 duan.fUSD AS fTienTheoDuAnUSD,
		 duan.fVND AS fTienTheoDuAnVND,
		 NULL AS fTienTheoHopDongUSD,
		 NULL AS fTienTheoHopDongVND,
		 SUM(ISNULL(temp.fKinhPhiDuocCapChoCDTUSD,
		 0)) AS fKinhPhiDuocCapChoCDTUSD,
		 SUM(ISNULL(temp.fKinhPhiDuocCapChoCDTVND,
		 0)) AS fKinhPhiDuocCapChoCDTVND,
		 SUM(ISNULL(temp.fKinhPhiDaThanhToanUSD,
		 0)) AS fKinhPhiDaThanhToanUSD,
		 SUM(ISNULL(temp.fKinhPhiDaThanhToanVND,
		 0)) AS fKinhPhiDaThanhToanVND,
		 ISNULL(duan.fUSD,
		 0) - SUM(ISNULL(temp.fKinhPhiDuocCapChoCDTUSD,
		 0)) AS fTiGiaCLHopDongVsCDTUSD,
		 ISNULL(duan.fVND,
		 0) - SUM(ISNULL(temp.fKinhPhiDuocCapChoCDTVND,
		 0)) AS fTiGiaCLHopDongVsCDTVND,
		 SUM(ISNULL(temp.fKinhPhiDuocCapChoCDTUSD,
		 0)) - SUM(ISNULL(temp.fKinhPhiDaThanhToanUSD,
		 0)) AS fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,
		 SUM(ISNULL(temp.fKinhPhiDuocCapChoCDTVND,
		 0)) - SUM(ISNULL(temp.fKinhPhiDaThanhToanVND,
		 0)) AS fTiGiaCLKinhPhiDuocCapVsGiaiNganVND,
		 duan.iID_KHTT_NhiemVuChiID  AS IDParent,
		 CAST(1 AS bit) AS IsHangCha
	FROM NH_DA_DuAn AS duan
	INNER JOIN #Temp AS temp
		ON duan.iID_KHTT_NhiemVuChiID = temp.ID
	WHERE (@iIDDuAn IS NULL
			OR duan.ID = @iIDDuAn)
			AND (@iDChuongTrinh IS NULL
			OR duan.iID_KHTT_NhiemVuChiID = @iDChuongTrinh)
	GROUP BY  duan.ID, duan.sTenDuAn, duan.fUSD, duan.fVND, duan.iID_KHTT_NhiemVuChiID ) AS E

	;WITH  #Tree(ID,sTen,fTienKHTTBQPCapUSD,fTienKHTTBQPCapVND, fTienTheoDuAnUSD, fTienTheoDuAnVND, fTienTheoHopDongUSD,fTienTheoHopDongVND,fKinhPhiDuocCapChoCDTUSD,fKinhPhiDuocCapChoCDTVND
			,fKinhPhiDaThanhToanUSD,fKinhPhiDaThanhToanVND,fTiGiaCLHopDongVsCDTUSD,fTiGiaCLHopDongVsCDTVND,fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,fTiGiaCLKinhPhiDuocCapVsGiaiNganVND
			,IDParent,IsHangCha,position)
	AS (
		select temp.ID,temp.sTen,
			temp.fTienKHTTBQPCapUSD,temp.fTienKHTTBQPCapVND,
			temp.fTienTheoDuAnUSD, temp.fTienTheoDuAnVND,
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
			child.fTienTheoDuAnUSD,child.fTienTheoDuAnVND,
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
		fTienTheoDuAnUSD, fTienTheoDuAnVND,
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
	DROP TABLE #DataTongHop;
	DROP TABLE #DataTHNamNay;
	DROP TABLE #DataTHNamTruoc;

END;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_chiphi_bygoithauid]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_chiphi_bygoithauid]
	@idGoiThau uniqueidentifier
	
AS BEGIN
	SELECT
		ChiPhi.Id as Id,
		ChiPhi.iID_GoiThauID as IIdGoiThauId,
		ChiPhi.iID_QDDauTu_ChiPhiID as IIdQddauTuChiPhiId,
		ChiPhi.iID_DuToan_ChiPhiID as IIdDuToanChiPhiId,
		ChiPhi.fTienGoiThau_USD as FTienGoiThauUsd,
		ChiPhi.fTienGoiThau_VND as FTienGoiThauVnd,
		ChiPhi.fTienGoiThau_EUR as FTienGoiThauEur,
		ChiPhi.fTienGoiThau_NgoaiTeKhac as FTienGoiThauNgoaiTeKhac,
		QDDTChiPhi.sTenChiPhi as STenChiPhiQDDT,
		DuToan.sTenChiPhi as STenChiPhiDT
	FROM NH_DA_GoiThau_ChiPhi ChiPhi
	LEFT JOIN NH_DA_QDDauTu_ChiPhi QDDTChiPhi
		ON ChiPhi.iID_QDDauTu_ChiPhiID = QDDTChiPhi.ID
	LEFT JOIN NH_DA_DuToan_ChiPhi DuToan
		ON ChiPhi.iID_DuToan_ChiPhiID = DuToan.ID
	WHERE 
		1=1
		AND ChiPhi.iID_GoiThauID = @idGoiThau
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_chutruongdautu_index]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_chutruongdautu_index]
	@YearOfWork int,
	@iLoai int
AS BEGIN

	WITH SoLieuDieuChinh AS (
		SELECT 
			ct.ID,
			ct.iID_ParentId
		FROM  NH_DA_ChuTruongDauTu ct 
		WHERE ct.iID_ParentId IS NOT NULL 
		UNION ALL 
		SELECT 
			ct.ID,
			ct.iID_ParentId
		FROM NH_DA_ChuTruongDauTu ct 
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
			chuTruongDauTuNguonVon.iID_ChuTruongDauTuID AS iID_ChuTruongDauTuID, 
			SUM(chuTruongDauTuNguonVon.fGiaTriNgoaiTeKhac) AS fGiaTriNgoaiTeKhac,
			SUM(chuTruongDauTuNguonVon.fGiaTriUSD) AS fGiaTriUSD,
			SUM(chuTruongDauTuNguonVon.fGiaTriVND) AS fGiaTriVND,
			SUM(chuTruongDauTuNguonVon.fGiaTriEUR) AS fGiaTriEUR
		FROM NH_DA_ChuTruongDauTu_NguonVon chuTruongDauTuNguonVon
		GROUP BY 
			chuTruongDauTuNguonVon.iID_ChuTruongDauTuID
	)
	SELECT
		chuTruongDauTu.ID AS Id,
		chuTruongDauTu.iID_KHTT_NhiemVuChiID AS IIdKhttNhiemVuChiId,
		chuTruongDauTu.iID_DuAnID AS IIdDuAnId,
		chuTruongDauTu.iID_ChuDauTuID AS IIdChuDauTuId,
		chuTruongDauTu.iID_MaChuDauTu AS IIdMaChuDauTu,
		chuTruongDauTu.iID_DonViQuanLyID AS IIdDonViQuanLyId,
		chuTruongDauTu.iID_MaDonViQuanLy AS IIdMaDonViQuanLy,
		chuTruongDauTu.iID_CapPheDuyetID AS IIdCapPheDuyetId,
		chuTruongDauTu.iID_TiGiaUSD_VNDID AS IIdTiGiaUsdVndId,
		chuTruongDauTu.iID_TiGiaUSD_EURID AS IIdTiGiaUsdEurId,
		chuTruongDauTu.iID_TiGiaUSD_NgoaiTeKhacID AS IIdTiGiaUsdNgoaiTeKhacId,
		chuTruongDauTu.iID_ParentID AS IIdParentId,
		chuTruongDauTu.sSoQuyetDinh AS SSoQuyetDinh,
		chuTruongDauTu.dNgayQuyetDinh AS DNgayQuyetDinh,
		chuTruongDauTu.sMota AS SMoTa,
		chuTruongDauTu.sKhoiCong AS SKhoiCong,
		chuTruongDauTu.sKetThuc AS SKetThuc,
		chuTruongDauTu.sDiaDiem AS SDiaDiem,
		chuTruongDauTu.sMucTieu AS SMucTieu,
		chuTruongDauTu.sQuyMo AS SQuyMo,
		ISNULL(nguonVon.fGiaTriNgoaiTeKhac, 0) AS FGiaTriNgoaiTeKhac,
		ISNULL(nguonVon.fGiaTriUSD, 0) AS FGiaTriUSD,
		ISNULL(nguonVon.fGiaTriVND, 0) AS FGiaTriVND,
		ISNULL(nguonVon.fGiaTriEUR, 0) AS FGiaTriEUR,
		chuTruongDauTu.dNgayTao AS DNgayTao,
		chuTruongDauTu.sNguoiTao AS SNguoiTao,
		chuTruongDauTu.dNgaySua AS DNgaySua,
		chuTruongDauTu.sNguoiSua AS SNguoiSua,
		chuTruongDauTu.dNgayXoa AS DNgayXoa,
		chuTruongDauTu.sNguoiXoa AS SNguoiXoa,
		chuTruongDauTu.bIsActive AS BIsActive,
		chuTruongDauTu.bIsGoc AS BIsGoc,
		chuTruongDauTu.bIsKhoa AS BIsKhoa,
		chuTruongDauTu.bIsXoa AS BIsXoa,
		CONCAT(donVi.iID_MaDonVi, ' - ', donVi.sTenDonVi) AS STenDonVi,
		CONCAT(duAn.sMaDuAn, ' - ', duAn.sTenDuAn) AS STenDuAn,
		ISNULL(soLieuDieuChinh.iSoLanDieuChinh, 0) AS ILanDieuChinh,
		chuTruongDauTuParent.sSoQuyetDinh AS SDieuChinhTu,
		(SELECT COUNT(Id) FROM Attachment WHERE ModuleType = 404 AND ObjectId = chuTruongDauTu.ID) AS TotalFiles ,
		nvChi.iID_NhiemVuChiID as iIDNhiemVuChiID,
		nvChi.iID_KHTongTheID as IIdKHTongTheID,
		nvChi.STenChuongTrinh as STenChuongTrinh
	FROM NH_DA_ChuTruongDauTu chuTruongDauTu
	LEFT JOIN NH_DA_ChuTruongDauTu chuTruongDauTuParent
		ON chuTruongDauTu.iID_ParentID = chuTruongDauTuParent.ID
	LEFT JOIN DonVi donVi
		ON chuTruongDauTu.iID_MaDonViQuanLy = donVi.iID_MaDonVi AND donVi.iNamLamViec = @YearOfWork
	LEFT JOIN NH_DA_DuAn duAn
		ON chuTruongDauTu.iID_DuAnID = duAn.ID
	LEFT JOIN SoLanDieuChinh soLieuDieuChinh
		ON soLieuDieuChinh.ID = chuTruongDauTu.ID
	LEFT JOIN ThongTinNguonVon nguonVon
		ON nguonVon.iID_ChuTruongDauTuID = chuTruongDauTu.ID
	LEFT JOIN (SELECT  n.ID, n.iID_KHTongTheID, d.sTenNhiemVuChi AS STenChuongTrinh, n.iID_NhiemVuChiID
	 FROM NH_KHTongThe_NhiemVuChi AS n 
	 INNER JOIN NH_DM_NhiemVuChi AS d 
	 ON n.iID_NhiemVuChiID = d.ID
	) AS nvChi ON chuTruongDauTu.iID_KHTT_NhiemVuChiID = nvChi.ID
	WHERE chuTruongDauTu.iLoai = @iLoai
	ORDER BY chuTruongDauTu.dNgayQuyetDinh DESC, chuTruongDauTu.sSoQuyetDinh DESC;
END;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_chuyendulieu_quyettoan_index]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_chuyendulieu_quyettoan_index]
AS
BEGIN
	SELECT 
		cqt.ID AS Id,
		cqt.sSoChungTu,
		cqt.dNgayChungTu,
		cqt.iID_DonViID,
		cqt.iLoaiThoiGian,
		cqt.iThoiGian,
		cqt.sMoTa,
		cqt.iID_MaDonVi,
		cqt.sNguoiTao,
		cqt.dNgayTao,
		cqt.sNguoiSua,
		cqt.dNgaySua,
		CONCAT(DonVi.iID_MaDonVi, ' - ', ISNULL(DonVi.sTenDonVi,'')) AS STenDonVi,
		(CASE WHEN cqt.iLoaiThoiGian = 1 THEN 'Tháng'
					WHEN cqt.iLoaiThoiGian = 2 THEN 'Quý'
					ELSE ''
		END) AS STenLoaiThoiGian,
		(CASE WHEN cqt.iLoaiThoiGian = 1 THEN (CASE WHEN cqt.iThoiGian IS NOT NULL OR cqt.iThoiGian <> 0 THEN CONCAT(N'Tháng ', cqt.iThoiGian) ELSE '' END)
					WHEN cqt.iLoaiThoiGian = 2 THEN (CASE WHEN cqt.iThoiGian IS NOT NULL OR cqt.iThoiGian <> 0 THEN CONCAT(N'Quý ', dbo.ToRoman(cqt.iThoiGian)) ELSE '' END)
					ELSE ''
		END) AS STenThoiGian
	FROM NH_QT_ChuyenQuyetToan cqt
	LEFT JOIN DonVi ON cqt.iID_DonViID = DonVi.iID_DonVi
	ORDER BY cqt.dNgayTao DESC
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_da_delete_hopdong]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_da_delete_hopdong]
  @id AS uniqueidentifier 
AS
BEGIN
	DELETE NH_DA_HopDong WHERE ID = @id  
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_da_delete_quyetdinhhopdong]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_da_delete_quyetdinhhopdong] 
@idHopDong uniqueidentifier,
@idQuyetDinh uniqueidentifier
AS BEGIN  
 DELETE NH_DA_HopDong_ChiPhi WHERE iID_HopDongID = @idHopDong AND iID_CacQuyetDinhID = @idQuyetDinh
 DELETE NH_DA_HopDong_NguonVon WHERE iID_HopDongID = @idHopDong AND iID_CacQuyetDinhID = @idQuyetDinh
 DELETE NH_DA_HopDong_CacQuyetDinh WHERE iID_CacQuyetDinhID = @idQuyetDinh AND iID_HopDongID = @idHopDong  
 DELETE NH_DA_HopDong_HangMuc WHERE iID_HopDong_ChiPhiID 
		IN (SELECT ID 
				FROM NH_DA_HopDong_ChiPhi 
				WHERE iID_HopDongID = @idHopDong AND iID_CacQuyetDinhID = @idQuyetDinh
		)
END;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_delete_duan_qddt_hd_qdk_by_idKhoiTao]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_nh_delete_duan_qddt_hd_qdk_by_idKhoiTao] 
	-- Add the parameters for the stored procedure here
	@iIdKhoiTaoQdk uniqueidentifier,
	@type int ---= 1 xoa KT, = 2 xoa ca nhung muc lien quan
AS
BEGIN

IF(@type = 2)
BEGIN
	--duan
	delete NH_DA_DuAn
	where ID IN (select iID_DuAnID from NH_KT_KhoiTaoCapPhat_ChiTiet where iID_KhoiTaoCapPhatID = @iIdKhoiTaoQdk AND iID_DuAnID is not null);
	
	--qddt
	delete NH_DA_QDDauTu
	where iID_DuAnID IN (select iID_DuAnID from NH_KT_KhoiTaoCapPhat_ChiTiet where iID_KhoiTaoCapPhatID = @iIdKhoiTaoQdk AND iID_DuAnID is not null);
	 
	 -- qdk
	 delete NH_DA_QuyetDinhKhac
	 where ID IN (select iID_QuyetDinhKhacID from NH_KT_KhoiTaoCapPhat_ChiTiet where iID_KhoiTaoCapPhatID = @iIdKhoiTaoQdk);
	 
	 --chi tiet qdk
	 delete NH_DA_QuyetDinhKhac_ChiPhi
	 where iID_QuyetDinhKhacID IN (select iID_QuyetDinhKhacID from NH_KT_KhoiTaoCapPhat_ChiTiet where iID_KhoiTaoCapPhatID = @iIdKhoiTaoQdk );

	 --delete hop dong
	delete NH_DA_HopDong
	where ID IN (select iID_HopDongID from NH_KT_KhoiTaoCapPhat_ChiTiet where iID_KhoiTaoCapPhatID = @iIdKhoiTaoQdk);

	--delete chi tiet
	delete NH_DA_HopDong_ChiPhi
	where iID_HopDongID IN (select iID_HopDongID from NH_KT_KhoiTaoCapPhat_ChiTiet where iID_KhoiTaoCapPhatID = @iIdKhoiTaoQdk );

	--delete khoi tao
	DELETE NH_KT_KhoiTaoCapPhat
	where ID = @iIdKhoiTaoQdk;

	DELETE NH_KT_KhoiTaoCapPhat_ChiTiet
	WHERE iID_KhoiTaoCapPhatID = @iIdKhoiTaoQdk;
END
ELSE
BEGIN
	DELETE NH_KT_KhoiTaoCapPhat
	where ID = @iIdKhoiTaoQdk;

	DELETE NH_KT_KhoiTaoCapPhat_ChiTiet
	WHERE iID_KhoiTaoCapPhatID = @iIdKhoiTaoQdk;
END
END
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_duan_find_from_chutruong_dautu]    Script Date: 11/3/2023 11:47:41 AM ******/
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_duan_find_from_dutoan]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TungNH
-- Create date: 16/03/2022
-- Description:	Lấy danh sách dự án cho màn dự toán ngoại hối
-- =============================================
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_duan_find_from_qddautu]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TungNH
-- Create date: 16/03/2022
-- Description:	Lấy danh sách dự án cho màn quyết định đầu tư
-- =============================================
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
		--AND duAn.ID IN (SELECT iID_DuAnID FROM NH_DA_ChuTruongDauTu) -- Lấy dự án đã có chủ trương đầu tư
		AND duAn.ID NOT IN (SELECT DISTINCT(iID_DuAnID) FROM NH_DA_QDDauTu WHERE iID_DuAnID IS NOT NULL AND ILoai = @ILoai AND (@QdDauTuId IS NULL OR iID_DuAnID <> @DuAnId)) -- Lấy dự án chưa có quyết định đầu tư
END;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_DuToan_chiphi]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_DuToan_chiphi]
@iIdDuToanId uniqueidentifier
AS
BEGIN
	SELECT tbl.ID as IIDChiPhiID, tbl.iID_ParentID as IIDParentID, tbl.STenChiPhi, tbl.SMaOrder , NULL as IIDGoiThauID, tbl.iID_DuToan_NguonVonID AS IIdNguonVonId,
		ISNULL(tbl.FGiaTriNgoaiTeKhac, 0) as FGiaTriNgoaiTeKhac, ISNULL(tbl.FGiaTriUSD, 0) as FGiaTriUSD, ISNULL(tbl.FGiaTriVND, 0) as FGiaTriVND, ISNULL(tbl.FGiaTriEUR, 0) as FGiaTriEUR,
		CAST(0 as float) as FGiaTriPheDuyetUSD,
		CAST(0 as float) as FGiaTriPheDuyetVND,
		CAST(0 as float) as FGiaTriPheDuyetEUR
	FROM NH_DA_DuToan_ChiPhi as tbl
	INNER JOIN NH_DA_DuToan_NguonVon nguonvon
		ON tbl.iID_DuToan_NguonVonID = nguonvon.ID
	WHERE nguonvon.iID_DuToanID = @iIdDuToanId
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_dutoan_delete_dutoan]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_dutoan_delete_dutoan] 
	@id nvarchar(100)
	
AS
BEGIN
	
	DELETE NH_DA_DuToan_HangMuc WHERE iID_DuToan_ChiPhiID IN (SELECT ID FROM NH_DA_DuToan_ChiPhi WHERE iID_DuToanID = @id)
	DELETE NH_DA_DuToan_ChiPhi WHERE iID_DuToanID = @id
  DELETE NH_DA_DuToan_Nguonvon WHERE iID_DuToanID = @id	
	DELETE NH_DA_DuToan WHERE ID = @id
END;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_dutoan_get_all_dutoan_chiphi_by_duan]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_dutoan_get_all_dutoan_chiphi_by_duan] 
	@duAnId nvarchar(200)
AS
BEGIN
	
	declare @qdDauTuId uniqueidentifier;
	set @qdDauTuId = (select top 1 ID from NH_DA_QDDauTu where iID_DuAnID = @duAnId and bIsActive = 1);

select 
		null as Id,
		NEWID() as IIdDuToanChiPhiId,
		case when qdcp.STenChiPhi IS NULL THEN dmcp.sTenChiPhi ELSE qdcp.sTenChiPhi END as STenChiPhi,
		qdcp.iID_ChiPhiID as IIdChiPhiId,
		null as IIdDuToanId,
		CAST(0 as float) as fGiaTriUSD,
		CAST(0 as float) as fGiaTriVND,
		CAST(0 as float) as fGiaTriEUR,
		CAST(0 as float) as fGiaTriNgoaiTeKhac,
		qdcp.fGiaTriUSD as fGiaTriUSDQDDTPheDuyet,
		qdcp.fGiaTriVND as fGiaTriVNDQDDTPheDuyet,
		qdcp.fGiaTriEUR as fGiaTriEURQDDTPheDuyet,
		qdcp.fGiaTriNgoaiTeKhac as fGiaTriNgoaiTeKhacQDDTPheDuyet,
		qdcp.ID as IIdQDDauTuChiPhiId,
		qdcp.iID_ParentID as IIdQDDauTuChiPhiParentId,
		null as IIdParentId,
		cast(1 as bit) as IsEditHangMuc,
		cast(1 as bit) as IsNew,
		qdcp.sMaOrder as SMaOrder,
		case when ((select count(*) from NH_DA_QDDauTu_ChiPhi where iID_ParentID = qdcp.ID) > 0 OR qdcp.iID_ParentID IS NULL) THEN cast(1 as bit) ELSE cast(0 as bit) END AS IsHangCha
	from NH_DA_QDDauTu_ChiPhi qdcp
		left join NH_DM_ChiPhi dmcp ON qdcp.iID_ChiPhiID = dmcp.iID_ChiPhi
		where qdcp.iID_QDDauTuID = @qdDauTuId
		ORDER BY qdcp.SMaOrder
		
END;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_dutoan_get_all_dutoan_chiphi_by_dutoan_id]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_dutoan_get_all_dutoan_chiphi_by_dutoan_id] 
	@duToanId uniqueidentifier
AS
BEGIN
	
select 
		dtcp.ID as Id,
		dtcp.ID as IIdDuToanChiPhi,
		case when dtcp.STenChiPhi IS NULL THEN dmcp.sTenChiPhi ELSE dtcp.sTenChiPhi END as STenChiPhi,
		dtcp.iID_ChiPhiID as IdChiPhiId,
		iID_DuToanID as IIdDuToanId,
		dtcp.fGiaTriUSD as fGiaTriUSD,
		dtcp.fGiaTriVND as fGiaTriVND,
		dtcp.fGiaTriEUR as fGiaTriEUR,
		dtcp.fGiaTriNgoaiTeKhac as fGiaTriNgoaiTeKhac,
		qddtcp.fGiaTriUSD as fGiaTriUSDQDDTPheDuyet,
		qddtcp.fGiaTriVND as fGiaTriVNDQDDTPheDuyet,
		qddtcp.fGiaTriEUR as fGiaTriEURQDDTPheDuyet,
		qddtcp.fGiaTriNgoaiTeKhac as fGiaTriNgoaiTeKhacQDDTPheDuyet,
		qddtcp.ID as IIdQDDauTuChiPhiId,
		qddtcp.iID_ParentID as IIdQDDauTuChiPhiParentId,
		dtcp.iID_ParentID as IIdParentId,
		cast(1 as bit) as IsEditHangMuc,
		cast(0 as bit) as IsNew,
		dtcp.sMaOrder as SMaOrder,
		case when ((select count(*) from NH_DA_DuToan_ChiPhi where iID_ParentID = dtcp.ID) > 0 OR dtcp.iID_ParentID IS NULL) THEN cast(1 as bit) ELSE cast(0 as bit) END AS IsHangCha
	from NH_DA_DuToan_ChiPhi dtcp
		left join NH_DM_ChiPhi dmcp ON dtcp.iID_ChiPhiID = dmcp.iID_ChiPhi
		INNER JOIN NH_DA_QDDauTu_ChiPhi qddtcp ON dtcp.iID_QDDauTu_ChiPhiID = qddtcp.Id
		WHERE dtcp.iID_DuToanID = @duToanId
		ORDER BY dtcp.SMaOrder
		
END;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_dutoan_get_all_dutoan_nguonvon]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_dutoan_get_all_dutoan_nguonvon] 
	@duToanId nvarchar(200)
AS
BEGIN
	select ns.sTen as TenNguonVon,
	nv.ID as IIdDuToanNguonVonId,
	nv.iID_NguonVonID as IIdNguonVonId,
	nv.iID_DuToanID as IIdDuToanId,
	nv.iID_QDDauTu_NguonVonID as IIdQDDauTuNguonVonId,
	nv.fGiaTriNgoaiTeKhac as FGiaTriNgoaiTeKhac,
	nv.fGiaTriUSD as FGiaTriUSD,
	nv.fGiaTriVND as FGiaTriVND,
	nv.fGiaTriEUR as FGiaTriEUR,
	qddautu_nv.fGiaTriNgoaiTeKhac as FGiaTriNgoaiTeKhacQDDTPheDuyet,
	qddautu_nv.fGiaTriUSD as FGiaTriUSDQDDTPheDuyet,
	qddautu_nv.fGiaTriVND as FGiaTriVNDQDDTPheDuyet,
	qddautu_nv.fGiaTriEUR as FGiaTriEURQDDTPheDuyet,
	cast(0 as bit) as IsNew
	from NH_DA_DuToan_Nguonvon nv
	inner join NguonNganSach ns ON ns.iID_MaNguonNganSach = nv.iID_NguonVonID
	INNER JOIN NH_DA_QDDauTu_NguonVon qddautu_nv ON nv.iID_QDDauTu_NguonVonID = qddautu_nv.ID
	where nv.iID_DuToanID =  @duToanId
		
END;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_dutoan_get_all_dutoan_nguonvon_by_duanid]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_dutoan_get_all_dutoan_nguonvon_by_duanid] @duanId uniqueidentifier AS BEGIN
	SELECT
		ns.sTen AS TenNguonVon,
		null as IIdDuToanNguonVonId,
		NULL AS IIdDuToanId,
		qdnv.iID_NguonVonID AS IIdNguonVonId,
		qdnv.ID AS IIdQDDauTuNguonVonId,
		qdnv.FGiaTriNgoaiTeKhac as FGiaTriNgoaiTeKhacQDDTPheDuyet,
		qdnv.FGiaTriUSD as FGiaTriUSDQDDTPheDuyet,
		qdnv.FGiaTriVND as FGiaTriVNDQDDTPheDuyet,
		qdnv.FGiaTriEUR as FGiaTriEURQDDTPheDuyet,
		null as FGiaTriNgoaiTeKhac,
		null as FGiaTriUSD,
		null as FGiaTriVND,
		null as FGiaTriEUR,
		cast(1 as bit) as IsNew
	FROM
		NH_DA_QDDauTu_NguonVon qdnv
		INNER JOIN NguonNganSach ns ON ns.iID_MaNguonNganSach = qdnv.iID_NguonVonID
		INNER JOIN NH_DA_QDDauTu qd ON qd.ID = qdnv.iID_QDDauTuID 
		AND qd.bIsActive = 1 
	WHERE
	qd.iID_DuAnID =  @duAnId 
END;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_dutoan_get_dutoan_hangmuc_by_dutoan_chiphi]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_dutoan_get_dutoan_hangmuc_by_dutoan_chiphi]
@duToanChiPhi uniqueidentifier
as
begin
	select 
		dthm.ID as Id,
		dthm.ID as IIdDuToanHangMucId,
		qdhm.ID as IIdQDDauTuHangMucId,
		qdhm.iID_QDDauTu_ChiPhiID as IIdQDDauTuChiPhiId,
		qdhm.iID_ParentID as IIdQDDauTuHangMucParentId,
		dthm.iID_LoaiCongTrinhID as IIdLoaiCongTrinhId,
		dthm.iID_DuToan_ChiPhiID as IIdDuToanChiPhiId,
		dthm.sMaHangMuc as SMaHangMuc,
		dthm.sTenHangMuc as STenHangMuc,
		null as IIdDuToanId,
		dthm.iID_ParentID as IIdParentId,
		dthm.iID_HangMucPhanChiaID as IIdHangMucPhanChiaId,
		dthm.sMaOrder as SMaOrder,
		qdhm.fGiaTriNgoaiTeKhac as FGiaTriNgoaiTeKhacQDDTPheDuyet,
		qdhm.fGiaTriUSD as FGiaTriUSDQDDTPheDuyet,
		qdhm.fGiaTriVND as FGiaTriVNDQDDTPheDuyet,
		qdhm.fGiaTriEUR as FGiaTriEURQDDTPheDuyet,
		dthm.FGiaTriNgoaiTeKhac as FGiaTriNgoaiTeKhac,
		dthm.FGiaTriUSD as FGiaTriUSD,
		dthm.FGiaTriEUR as FGiaTriEUR,
		dthm.FGiaTriVND as FGiaTriVND,
		cast(0 as bit) as IsNew,
		case when ((select count(*) from NH_DA_DuToan_HangMuc where iID_ParentID = dthm.ID) > 0 OR dthm.iID_ParentID IS NULL) THEN cast(1 as bit) ELSE cast(0 as bit) END AS IsHangCha
	FROM NH_DA_DuToan_HangMuc dthm
	LEFT JOIN NH_DA_QDDauTu_HangMuc qdhm ON dthm.iID_QDDauTu_HangMucID = qdhm.ID
	WHERE dthm.iID_DuToan_ChiPhiID = @duToanChiPhi
	ORDER BY dthm.sMaOrder
end;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_dutoan_get_qddautu_hangmuc_by_qddautu_chiphi]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_dutoan_get_qddautu_hangmuc_by_qddautu_chiphi]
@qdDautuChiPhiId uniqueidentifier
as
begin
	select 
		null as Id,
		NEWID() as IIdDuToanHangMucId,
		ID as IIdQDDauTuHangMucId,
		iID_QDDauTu_ChiPhiID as IIdQDDauTuChiPhiId,
		iID_ParentID as IIdQDDauTuHangMucParentId,
		qdhm.iID_LoaiCongTrinhID as IIdLoaiCongTrinhId,
		null as IIdDuToanChiPhiId,
		sMaHangMuc as SMaHangMuc,
		sTenHangMuc as STenHangMuc,
		null as IIdDuToanId,
		null as IIdParentId,
		null as IIdHangMucPhanChiaId,
		sMaOrder as SMaOrder,
		fGiaTriNgoaiTeKhac as FGiaTriNgoaiTeKhacQDDTPheDuyet,
		fGiaTriUSD as FGiaTriUSDQDDTPheDuyet,
		fGiaTriVND as FGiaTriVNDQDDTPheDuyet,
		fGiaTriEUR as FGiaTriEURQDDTPheDuyet,
		null as FGiaTriNgoaiTeKhac,
		null as FGiaTriUSD,
		null as FGiaTriEUR,
		null as FGiaTriVND,
		cast(1 as bit) as IsNew,
		case when ((select count(*) from NH_DA_QDDauTu_HangMuc where iID_ParentID = qdhm.ID) > 0 OR qdhm.iID_ParentID IS NULL) THEN cast(1 as bit) ELSE cast(0 as bit) END AS IsHangCha
	FROM NH_DA_QDDauTu_HangMuc qdhm
	WHERE iID_QDDauTu_ChiPhiID = @qdDautuChiPhiId
	ORDER BY qdhm.sMaOrder
end;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_dutoan_getall_tkktvatongdutoan]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_dutoan_getall_tkktvatongdutoan] 
	@YearOfWork int
	
AS
BEGIN
	
-- 	WITH SoLieuDieuChinh AS 
-- 	 (
-- 		SELECT 
-- 			ct.iID_DuToanID, ct.iID_ParentId
-- 		FROM 
-- 			VDT_DA_DuToan ct 
-- 		WHERE 
-- 			ct.iID_ParentId is not null
-- 
-- 		UNION ALL
-- 
-- 		SELECT 
-- 			ct.iID_DuToanID, ct.iID_ParentId
-- 		FROM 
-- 			VDT_DA_DuToan ct JOIN SoLieuDieuChinh ctpr ON ct.iID_ParentId = ctpr.iID_DuToanID
-- 		WHERE ct.iID_ParentId is not null
-- 	  ),SoLanDieuChinh AS (
-- 		   SELECT
-- 			sdc.iID_DuToanID,sdc.iID_ParentId,  COUNT(sdc.iID_DuToanID) AS iSoLanDieuChinh
-- 		  FROM 
-- 			SoLieuDieuChinh sdc
-- 		  GROUP BY sdc.iID_ParentId,sdc.iID_DuToanID
-- 	  )
	
	SELECT
		dt.ID,
		dt.sSoQuyetDinh as SSoQuyetDinh,
		concat(duan.sMaDuAn, ' - ', duan.sTenDuAn) as STenDuAn,
		dt.dNgayQuyetDinh as DNgayQuyetDinh,
		dt.sMoTa as SMoTa,
		dt.iID_DuAnID as IIdDuAnId,
		dt.fGiaTriNgoaiTeKhac as FGiaTriNgoaiTeKhac,
		dt.fGiaTriUSD as FGiaTriUSD,
		dt.fGiaTriVND as FGiaTriVND,
		dt.fGiaTriEUR as FGiaTriEUR,
		dt.bIsGoc as BIsGoc,
		dt.bIsActive as BIsActive,
		dt.bIsXoa,
		dt.iID_ParentID as IIdParentId,
		dt.iID_DuToanGocID as IIdDuToanGocId,
		dt.iID_TiGiaID as IIDTiGiaID,
		dt.sMaNgoaiTeKhac as SMaNgoaiTeKhac,
		dt.sNguoiTao as SUserCreate,
		dt.dNgayTao as DDateCreate,
		dt.sNguoiSua as SUserUpdate,
		dt.dNgaySua as DDateUpdate,
		concat(dv.iID_MaDonVi, ' - ', dv.sTenDonVi) as STenDonVi,
		dv.iID_DonVi as Id_DonVi,
		duan.iID_MaDonViQuanLy as IIdMaDonViQuanLy,
		duan.sDiaDiem as SDiaDiem,
		duan.sKhoiCong as SKhoiCong,
		duan.sKetThuc as SKetThuc,
		(duan.sKhoiCong + '-' + duan.sKetThuc) as ThoiGianThucHien,
		duan.iID_DonViQuanLyID as IIdDonViQuanLyId,
		dt.bIsKhoa as BIsKhoa,
		dt.iLanDieuChinh as ILanDieuChinh,
		dt.iID_QDDauTuID as IIdQddauTuId,
		--(SELECT SUM ( fTongDuToanPheDuyet ) FROM VDT_DA_DuToan WHERE iID_DuAnID = dt.iID_DuAnID ) AS FTongMucDauTuSauDieuChinh,
		--isnull(tbl.iSoLanDieuChinh,0) AS SoLanDieuChinh,
		(SELECT COUNT(Id) FROM Attachment WHERE ModuleType = 406 AND ObjectId = dt.ID) AS TotalFiles
	FROM
		NH_DA_DuToan dt		
		LEFT JOIN NH_DA_DuAn duan ON dt.iID_DuAnID = duan.ID
		LEFT JOIN DonVi dv ON duan.iID_DonViQuanLyID = dv.iID_DonVi 
		--LEFT JOIN SoLanDieuChinh tbl ON tbl.iID_DuToanID = dt.iID_DuToanID
		
	WHERE
		dt.iID_DuToanGocID is not null and dt.bIsActive = 1
		ORDER BY dNgayQuyetDinh desc
END;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_dutoan_hangmuc]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_dutoan_hangmuc]
@iIdDuToanId uniqueidentifier
AS
BEGIN
	SELECT tbl.iID_DuToan_ChiPhiID as IIDChiPhiID, tbl.ID as IIDHangMucID, tbl.iID_ParentID as IIDParentID, tbl.STenHangMuc, tbl.SMaHangMuc, tbl.SMaOrder , NULL as IIDGoiThauID,
		ISNULL(tbl.FGiaTriNgoaiTeKhac, 0) as FGiaTriNgoaiTeKhac, ISNULL(tbl.FGiaTriUSD, 0) as FGiaTriUSD, ISNULL(tbl.FGiaTriVND, 0) as FGiaTriVND, ISNULL(tbl.FGiaTriEUR, 0) as FGiaTriEUR,
		CAST(0 as float) as FGiaTriPheDuyetUSD,
		CAST(0 as float) as FGiaTriPheDuyetVND,
		CAST(0 as float) as FGiaTriPheDuyetEUR,
		CAST(0 as float) as FGiaTriPheDuyetNgoaiTeKhac
	FROM NH_DA_DuToan_ChiPhi as cp
	INNER JOIN NH_DA_DuToan_HangMuc as tbl on cp.ID = tbl.iID_DuToan_ChiPhiID
	INNER JOIN NH_DA_DuToan_NguonVon as nguonvon on cp.iID_DuToan_NguonVonID = nguonvon.ID
	WHERE nguonvon.iID_DuToanID = @iIdDuToanId
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_dutoan_in_khlcnt]    Script Date: 11/3/2023 11:47:41 AM ******/
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
		SELECT 
				tbl.ID, 
				tbl.iLoaiDuToan as IdLoaiDuToan, 
				tbl.fGiaTriEUR as FGiaTriEur,  
				tbl.fGiaTriNgoaiTeKhac as FGiaTriNgoaiTeKhac, 
				tbl.fGiaTriUSD as FGiaTriUsd, 
				tbl.fGiaTriVND as FGiaTriVnd, 
				tbl.iID_DonViQuanLyID as IIdDonViQuanLyId, 
				tbl.iID_TiGiaID as IIdTiGiaId, 
				tbl.iID_KHTT_NhiemVuChiID as IIdKHTTNhiemVuChiId, 
				tbl.sTenChuongTrinh as STenChuongTrinh, 
				tbl.fTiGiaNhap as FTiGiaNhap, 
				tbl.bIsActive as BIsActive, 
				tbl.bIsGoc as BIsGoc, 
				tbl.bIsKhoa as BIsKhoa, 
				tbl.bIsXoa as BIsXoa, 
				tbl.dNgayQuyetDinh as DNgayQuyetDinh, 
				tbl.dNgaySua as DNgaySua, 
				tbl.dNgayTao as DNgayTao, 
				tbl.sNguoiTao as SNguoiTao, 
				tbl.sNguoiSua as SNguoiSua, 
				tbl.dNgayXoa as DNgayXoa, 
				tbl.sNguoiXoa as SNguoiXoa, 
				tbl.iID_DuAnID as IIdDuAnId, 
				tbl.iID_DuToanGocID as IIdDuToanGocId, 
				tbl.iID_MaDonViQuanLy as IIdMaDonViQuanLy, 
				tbl.iID_ParentID as IIdParentId, 
				tbl.iID_QDDauTuID as IIdQdDauTuId, 
				tbl.iID_TiGiaUSD_EURID as IIdTiGiaUsdEurid, 
				tbl.iID_TiGiaUSD_NgoaiTeKhacID as IIdTiGiaUsdNgoaiTeKhacId, 
				tbl.iID_TiGiaUSD_VNDID as IIdTiGiaUsdVndid, 
				tbl.iID_TiGiaID as IIdTiGiaId,
				tbl.iLoai as ILoai,
				tbl.sMota as SMoTa,
				tbl.iLanDieuChinh as ILanDieuChinh,
				tbl.sMaNgoaiTeKhac as SMaNgoaiTeKhac,
				Case When iLoaiDuToan = 1 then Concat(N'Dự toán mua sắm: ',sSoQuyetDinh)  
				  When iLoaiDuToan = 2 then Concat(N'Dự toán đặt hàng: ',sSoQuyetDinh)  else sSoQuyetDinh end as sSoQuyetDinh
		FROM NH_DA_DuToan as tbl
		LEFT JOIN (SELECT DISTINCT iID_DuToanID FROM NH_DA_KHLCNhaThau WHERE bIsActive = 1 AND iID_DuToanID IS NOT NULL) as dt on tbl.ID = dt.iID_DuToanID
		WHERE tbl.iID_DuAnID = @iIdDuAnId and tbl.bIsActive=1
	 END
	 ELSE
	 BEGIN
		SELECT 
				dt.ID, 
				dt.iLoaiDuToan as IdLoaiDuToan, 
				dt.fGiaTriEUR as FGiaTriEur,  
				dt.fGiaTriNgoaiTeKhac as FGiaTriNgoaiTeKhac, 
				dt.fGiaTriUSD as FGiaTriUsd, 
				dt.fGiaTriVND as FGiaTriVnd, 
				dt.iID_DonViQuanLyID as IIdDonViQuanLyId, 
				dt.iID_TiGiaID as IIdTiGiaId, 
				dt.iID_KHTT_NhiemVuChiID as IIdKHTTNhiemVuChiId, 
				dt.sTenChuongTrinh as STenChuongTrinh, 
				dt.fTiGiaNhap as FTiGiaNhap, 
				dt.bIsActive as BIsActive, 
				dt.bIsGoc as BIsGoc, 
				dt.bIsKhoa as BIsKhoa, 
				dt.bIsXoa as BIsXoa, 
				dt.dNgayQuyetDinh as DNgayQuyetDinh, 
				dt.dNgaySua as DNgaySua, 
				dt.dNgayTao as DNgayTao, 
				dt.sNguoiTao as SNguoiTao, 
				dt.sNguoiSua as SNguoiSua, 
				dt.dNgayXoa as DNgayXoa, 
				dt.sNguoiXoa as SNguoiXoa, 
				dt.iID_DuAnID as IIdDuAnId, 
				dt.iID_DuToanGocID as IIdDuToanGocId, 
				dt.iID_MaDonViQuanLy as IIdMaDonViQuanLy, 
				dt.iID_ParentID as IIdParentId, 
				dt.iID_QDDauTuID as IIdQdDauTuId, 
				dt.iID_TiGiaUSD_EURID as IIdTiGiaUsdEurid, 
				dt.iID_TiGiaUSD_NgoaiTeKhacID as IIdTiGiaUsdNgoaiTeKhacId, 
				dt.iID_TiGiaUSD_VNDID as IIdTiGiaUsdVndid, 
				dt.iID_TiGiaID as IIdTiGiaId,
				dt.iLoai as ILoai,
				dt.sSoQuyetDinh as SSoQuyetDinh,
				dt.sMota as SMoTa,
				dt.iLanDieuChinh as ILanDieuChinh,
				dt.sMaNgoaiTeKhac as SMaNgoaiTeKhac
		--dt.*,dt.iLoaiDuToan as IdLoaiDuToan
		FROM NH_DA_KHLCNhaThau as tbl
		INNER JOIN NH_DA_DuToan as dt on tbl.iID_DuToanID = dt.ID
	 END
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_dutoan_index]    Script Date: 11/3/2023 11:47:41 AM ******/
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
		duToan.fTiGiaNhap AS FTiGiaNhap,
		--donvi.sTenDonVi AS STenDonVi, 
		CONCAT(donvi.iID_MaDonVi, ' - ', donvi.sTenDonVi) AS STenDonVi,
		donvi.iID_MaDonVi AS IIdMaDonViQuanLy,
		CONCAT(duAn.sMaDuAn, ' - ', duAn.sTenDuAn) AS STenDuAn,
		ISNULL(soLieuDieuChinh.iSoLanDieuChinh, 0) AS ILanDieuChinh,
		duToanParent.sSoQuyetDinh AS SDieuChinhTu,
		(SELECT COUNT(Id) FROM Attachment WHERE ModuleType = 406 AND ObjectId = duToan.ID) AS TotalFiles,
		nvChi.iID_NhiemVuChiID as IIdKHTTNhiemVuChiId,
		nvChi.iID_KHTongTheID as IIdKHTongTheID,
		nvChi.STenChuongTrinh as STenChuongTrinh
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
	LEFT JOIN (SELECT  n.ID, n.iID_KHTongTheID, d.sTenNhiemVuChi AS STenChuongTrinh, n.iID_NhiemVuChiID
	 FROM NH_KHTongThe_NhiemVuChi AS n 
	 INNER JOIN NH_DM_NhiemVuChi AS d 
	 ON n.iID_NhiemVuChiID = d.ID
	) AS nvChi ON duToan.iID_KHTT_NhiemVuChiID = nvChi.ID
	WHERE duToan.iLoai = @ILoai 
	ORDER BY dNgayQuyetDinh DESC, sSoQuyetDinh DESC
END;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_dutoan_nguonvon]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_dutoan_nguonvon]
@iIdDuToanId uniqueidentifier
AS
BEGIN
	SELECT tbl.iID_NguonVonID as IIDNguonVonID, nv.sTen as STenNguonVon, NULL as IIDGoiThauID,
		ISNULL(tbl.FGiaTriNgoaiTeKhac, 0) as FGiaTriNgoaiTeKhac, ISNULL(tbl.FGiaTriUSD, 0) as FGiaTriUSD, ISNULL(tbl.FGiaTriVND, 0) as FGiaTriVND, ISNULL(tbl.FGiaTriEUR, 0) as FGiaTriEUR,
		CAST(0 as float) as FGiaTriPheDuyetUSD,
		CAST(0 as float) as FGiaTriPheDuyetVND,
		CAST(0 as float) as FGiaTriPheDuyetEUR,
		tbl.ID AS Id
	FROM NH_DA_DuToan_NguonVon as tbl
	INNER JOIN NguonNganSach as nv on tbl.iID_NguonVonID = nv.iID_MaNguonNganSach
	WHERE tbl.iID_DuToanID = @iIdDuToanId
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_find_all_donVi_by_KhTongTheId]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_find_all_donVi_by_KhTongTheId]
	@IdKhTongThe uniqueidentifier
AS
BEGIN
    SELECT DISTINCT
	tt_nvc.iID_KHTongTheID,
	tt_nvc.iID_DonViThuHuongID as Id,
	DV.iID_DonVi,
	DV.iID_MaDonVi as IIDMaDonVi,
	DV.sTenDonVi as TenDonVi,
	DV.iNamLamViec as NamLamViec
	FROM NH_KHTongThe_NhiemVuChi tt_nvc
	JOIN DonVi DV ON tt_nvc.iID_MaDonViThuHuong = DV.iID_MaDonVi
	JOIN NH_DM_NhiemVuChi nvc ON tt_nvc.iID_NhiemVuChiID = nvc.ID 
	WHERE 
	tt_nvc.iID_KHTongTheID = @IdKhTongThe
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_find_all_nvc_by_IdKhTongThe_giaiDoan]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_find_all_nvc_by_IdKhTongThe_giaiDoan]
	@IdKhTongThe uniqueidentifier
AS
BEGIN
    SELECT
    tt_nvc.ID,
    tt_nvc.iID_KHTongTheID,
    tt_nvc.iID_NhiemVuChiID,
    tt_nvc.iID_DonViThuHuongID,
    tt_nvc.fGiaTriKH_TTCP AS FGiaTriKhTTCP,
    tt_nvc.fGiaTriKH_BQP AS FGiaTriKhBQP,
    FORMATMESSAGE( '%s - %s', DonVi.iID_MaDonVi, DonVi.sTenDonVi ) sTenDonVi,
    donvi.iID_MaDonVi AS SMaDonViThuHuong,
    nvc.sMaNhiemVuChi,
    nvc.sTenNhiemVuChi,
    nvc.iLoaiNhiemVuChi
	FROM NH_KHTongThe_NhiemVuChi tt_nvc
	JOIN NH_KHTongThe khtt ON khtt.ID = tt_nvc.iID_KHTongTheID 
	JOIN DonVi DonVi ON DonVi.iID_DonVi = tt_nvc.iID_DonViThuHuongID
	JOIN NH_DM_NhiemVuChi nvc ON tt_nvc.iID_NhiemVuChiID = nvc.ID 
	WHERE khtt.iID_ParentID = @IdKhTongThe
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_find_by_IdKhTongThe]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_find_by_IdKhTongThe]
	@IdKhTongThe uniqueidentifier
AS
BEGIN
    SELECT
    tt_nvc.ID,
    tt_nvc.iID_KHTongTheID,
    tt_nvc.iID_NhiemVuChiID,
    tt_nvc.iID_DonViThuHuongID,
    tt_nvc.fGiaTriKH_TTCP AS FGiaTriKhTTCP,
    tt_nvc.fGiaTriKH_BQP AS FGiaTriKhBQP,
    FORMATMESSAGE( '%s - %s', DonVi.iID_MaDonVi, DonVi.sTenDonVi ) sTenDonVi,
    donvi.iID_MaDonVi AS SMaDonViThuHuong,
    nvc.sMaNhiemVuChi,
    nvc.sTenNhiemVuChi,
    nvc.iLoaiNhiemVuChi
	FROM NH_KHTongThe_NhiemVuChi tt_nvc
	JOIN DonVi DonVi ON DonVi.iID_DonVi = tt_nvc.iID_DonViThuHuongID
	JOIN NH_DM_NhiemVuChi nvc ON tt_nvc.iID_NhiemVuChiID = nvc.ID 
	WHERE tt_nvc.iID_KHTongTheID = @IdKhTongThe
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_find_by_IdKhTongThe_and_IdDonVi]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_find_by_IdKhTongThe_and_IdDonVi]
	@IdKhTongThe uniqueidentifier,
	@IdDonVi uniqueidentifier
AS
BEGIN
SELECT
    tt_nvc.ID,
    tt_nvc.iID_KHTongTheID,
    tt_nvc.iID_NhiemVuChiID,
    tt_nvc.iID_DonViThuHuongID,
    donvi.sTenDonVi AS STenDonVi,
    donvi.iID_MaDonVi AS SMaDonViThuHuong,
    tt_nvc.fGiaTriKH_TTCP AS FGiaTriKhTTCP,
    tt_nvc.fGiaTriKH_BQP AS FGiaTriKhBQP,
    nvc.sMaNhiemVuChi,
    nvc.sTenNhiemVuChi,
    nvc.iLoaiNhiemVuChi 
FROM NH_KHTongThe_NhiemVuChi tt_nvc
JOIN NH_DM_NhiemVuChi nvc ON tt_nvc.iID_NhiemVuChiID = nvc.ID 
JOIN DonVi donvi on DonVi.iID_DonVi = tt_nvc.iID_DonViThuHuongID
WHERE
    tt_nvc.iID_KHTongTheID = @IdKhTongThe
    AND tt_nvc.iID_DonViThuHuongID = @IdDonVi
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_find_by_IdKhTongThe_and_maDonVi]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_find_by_IdKhTongThe_and_maDonVi]
	@IdKhTongThe uniqueidentifier,
	@MaDonVi nvarchar(MAX)
AS
BEGIN
	SELECT 
    tt_nvc.ID, 
    tt_nvc.iID_KHTongTheID, 
    tt_nvc.iID_NhiemVuChiID, 
    tt_nvc.iID_DonViThuHuongID, 
    donvi.sTenDonVi AS STenDonVi, 
    donvi.iID_MaDonVi AS SMaDonViThuHuong, 
    tt_nvc.fGiaTriKH_TTCP AS FGiaTriKhTTCP, 
    tt_nvc.fGiaTriKH_BQP AS FGiaTriKhBQP, 
    nvc.sMaNhiemVuChi, 
    nvc.sTenNhiemVuChi, 
    nvc.iLoaiNhiemVuChi  
FROM NH_KHTongThe_NhiemVuChi tt_nvc 
JOIN NH_DM_NhiemVuChi nvc ON tt_nvc.iID_NhiemVuChiID = nvc.ID  
JOIN DonVi donvi on DonVi.iID_DonVi = tt_nvc.iID_DonViThuHuongID 
WHERE 
    tt_nvc.iID_KHTongTheID = @IdKhTongThe 
    AND tt_nvc.iID_MaDonViThuHuong = @MaDonVi 
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_find_by_IdNhiemVuChi]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_find_by_IdNhiemVuChi]
	@idNhiemVuChi uniqueidentifier
AS
BEGIN
	SELECT *
	FROM NH_DA_GoiThau gt
	join NH_KHTongThe_NhiemVuChi nvc
	on gt.iID_KHTT_NhiemVuChiID = nvc.ID
	join NH_DM_NhiemVuChi dmnvc
	on nvc.iID_NhiemVuChiID = dmnvc.ID
	WHERE
	dmnvc.Id =@idNhiemVuChi
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_find_KHTongThe_and_DmNhiemVuChi]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_find_KHTongThe_and_DmNhiemVuChi]
	@idKhTongThe uniqueidentifier
AS
BEGIN
    SELECT 
    n.ID
   ,n.iID_KHTongTheID
   ,n.iID_NhiemVuChiID
   ,n.iID_DonViThuHuongID
   ,n.fGiaTriKH_TTCP AS FGiaTriKhTTCP
   ,n.fGiaTriKH_BQP AS FGiaTriKhBQP
   ,n.fGiaTriKH_BQP_VND AS FGiaTriKhBQPVnd
   ,n.iID_MaDonViThuHuong
   ,n.sMaOrder
   ,d.iID_ParentID AS ParentNhiemVuChiId
   ,d.sTenNhiemVuChi AS STenNhiemVuChi
	FROM NH_KHTongThe_NhiemVuChi AS n
	INNER JOIN NH_DM_NhiemVuChi AS d
	ON n.iID_NhiemVuChiID = d.ID
	WHERE n.iID_KHTongTheID = @idKhTongThe
	ORDER BY sMaOrder
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_find_KhTongThe_by_NvChiId]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_find_KhTongThe_by_NvChiId]
	@IdNvChi uniqueidentifier
AS
BEGIN
    select * from NH_KHTongThe kh
	inner join NH_KHTongThe_NhiemVuChi nv 
	on nv.iID_KHTongTheID = kh.ID 
	where nv.ID = @IdNvChi 
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_find_KHTongThe_nhiemVuChi_by_Id]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_find_KHTongThe_nhiemVuChi_by_Id]
	@Id uniqueidentifier
AS
BEGIN
    SELECT
    tt_nvc.ID,
    tt_nvc.iID_KHTongTheID,
    tt_nvc.iID_NhiemVuChiID,
    tt_nvc.iID_DonViThuHuongID,
    donvi.sTenDonVi AS STenDonVi,
    donvi.iID_MaDonVi AS SMaDonViThuHuong,
    tt_nvc.fGiaTriKH_TTCP AS FGiaTriKhTTCP,
    tt_nvc.fGiaTriKH_BQP AS FGiaTriKhBQP,
    nvc.sMaNhiemVuChi,
    nvc.sTenNhiemVuChi,
    nvc.iLoaiNhiemVuChi
	FROM NH_KHTongThe_NhiemVuChi tt_nvc
	JOIN NH_DM_NhiemVuChi nvc ON tt_nvc.iID_NhiemVuChiID = nvc.ID
	JOIN DonVi donvi on DonVi.iID_DonVi = tt_nvc.iID_DonViThuHuongID
	WHERE tt_nvc.ID = @Id
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_find_one_by_IdKhTongThe_and_IdNhiemVuChi]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_find_one_by_IdKhTongThe_and_IdNhiemVuChi]
	@IdKhTongThe uniqueidentifier,
	@IdNhiemVuChi uniqueidentifier
AS
BEGIN
	SELECT 
     tt_nvc.ID, 
     tt_nvc.iID_KHTongTheID, 
     tt_nvc.iID_NhiemVuChiID, 
     tt_nvc.iID_DonViThuHuongID, 
     tt_nvc.fGiaTriKH_TTCP AS FGiaTriKhTTCP, 
     tt_nvc.fGiaTriKH_BQP AS FGiaTriKhBQP, 
     FORMATMESSAGE( '%s - %s', DonVi.iID_MaDonVi, DonVi.sTenDonVi ) sTenDonVi, 
     donvi.iID_MaDonVi AS SMaDonViThuHuong, 
     nvc.sMaNhiemVuChi, 
     nvc.sTenNhiemVuChi, 
     nvc.iLoaiNhiemVuChi  
 FROM NH_KHTongThe_NhiemVuChi tt_nvc 
 JOIN DonVi ON DonVi.iID_DonVi = tt_nvc.iID_DonViThuHuongID 
 JOIN NH_DM_NhiemVuChi nvc ON tt_nvc.iID_NhiemVuChiID = nvc.ID  
 WHERE
    tt_nvc.iID_KHTongTheID = @IdKhTongThe 
    AND tt_nvc.iID_NhiemVuChiID = @IdNhiemVuChi 
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_get_data_report_tinh_hinh_thuc_hien_du_an]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_get_data_report_tinh_hinh_thuc_hien_du_an]
	@IdDuAn nvarchar(max),
	@NgayPheDuyet datetime
AS
BEGIN
	select
	tthd.id as IdHopDong,
	(
		select 
				distinct mlns.sXauNoiMa 
			from NS_MucLucNganSach mlns
			where 
				(mlns.iID = pdttct.iID_MucLucNganSachID or mlns.iID_MLNS = pdttct.iID_MLNS_ID)
	) as Mlns, 
	dmnt.sTenNhaThau as TenNhaThau, 
	tthd.sSoHopDong as SoHopDong, 
	tthd.iThoiGianThucHien as ThoiGianThucHien, 
	tthd.fGiaTriHopDongVND as GiaTriHopDong,
	pdtt.dNgayPheDuyet as NgayThanhToan,
    case when iLoaiDeNghi = 3 then pdttct.fPheDuyetCapKyNay_VND else 0 end as SoThanhToan,
	case when iLoaiDeNghi = 2 then pdttct.fPheDuyetCapKyNay_VND else 0 end as SoTamUng,
	pdtt.fThuHoiTamUngPheDuyet_BangSo as SoThuHoiTamUng
	from NH_DA_HopDong tthd
	LEFT JOIN NH_DM_NhaThau dmnt on tthd.iID_NhaThauThucHienID = dmnt.Id
	LEFT JOIN NH_TT_ThanhToan pdtt on tthd.id = pdtt.iID_HopDongID
	LEFT JOIN NH_TT_ThanhToan_ChiTiet pdttct on pdtt.id = pdttct.iID_DeNghiThanhToanID
	where
	    pdtt.iTrangThai = 2
		AND tthd.iID_DuAnID = @IdDuAn
		AND pdtt.dNgayPheDuyet <= @NgayPheDuyet
		AND tthd.bIsActive = 1
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_get_data_report_tinh_hinh_thuc_hien_du_an1]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_get_data_report_tinh_hinh_thuc_hien_du_an1]
	@IdDuAn nvarchar(max),
	@NgayBatDau datetime,
	@NgayKetThuc datetime,
	@iID_HopDongID nvarchar(max),
	@iID_KHTongTheID nvarchar(max)
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
		--(
		--	select 
		--			distinct mlns.sXauNoiMa 
		--		from NS_MucLucNganSach mlns
		--		where 
		--			(mlns.iID = pdttct.iID_MucLucNganSachID OR mlns.iID_MLNS = pdttct.iID_MLNS_ID)
		--) as Mlns,
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
	--left join NH_TT_ThanhToan_ChiTiet pdttct on pdttct.iID_DeNghiThanhToanID = tt.ID
	left join NH_DA_HopDong tthd on tthd.ID = tt.iID_HopDongID
	where  (@IdDuAn IS NULL  OR tt.iID_DuAnID = @IdDuAn)
		AND (@NgayBatDau is null or tt.dNgayDeNghi >= @NgayBatDau)
		AND (@NgayKetThuc is null or tt.dNgayDeNghi <= @NgayKetThuc)
		AND (ISNULL(@iID_HopDongID,'') ='' OR tt.iID_HopDongID = @iID_HopDongID)
		AND  (ISNULL(@iID_KHTongTheID,'') ='' OR tt.iID_NhiemVuChiID = @iID_KHTongTheID)

END
;
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_nh_get_du_an_info_ngansach]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_get_du_an_info_ngansach]
	@IdDuAn nvarchar(500)
AS
BEGIN
	SET NOCOUNT ON;
		select 
		SUM(nguonvon.fGiaTriUSD) as SoTienUsd,
		SUM(nguonvon.fGiaTriEUR) as SoTienEur,
		SUM(nguonvon.fGiaTriVND) as SoTienVnd,
		SUM(nguonvon.fGiaTriNgoaiTeKhac) as SoTienNgoaiTeKhac,
		nguonvon.iID_NguonVonID as IdNguonVon,
		ngansach.sTen as Ten
		FROM Nh_DA_QDDauTu dautu 
		INNER join Nh_DA_QDDauTu_NguonVon nguonvon on dautu.id = nguonvon.iID_QDDauTuID
		INNER join NguonNganSach ngansach on ngansach.iID_MaNguonNganSach = nguonvon.iID_NguonVonID
		where dautu.iID_DuAnID  = @IdDuAn
		group by nguonvon.iID_NguonVonID,ngansach.sTen
		order by nguonvon.iID_NguonVonID
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_get_du_an_info_report]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_get_du_an_info_report]
	@MaDonVi nvarchar(500),
	@YearOfWork int
AS
BEGIN
	SET NOCOUNT ON;
		select 
	da.*,
	cdt.sTenDonVi as sTenChuDauTu,
	da_dautu.dNgayQuyetDinh as dNgayQuyetDinhDauTu,
	da_dautu.sSoQuyetDinh as sSoQuyetDinhDauTu,
	cap_pd.sTen as sPhanCap,
	tong_muc_dau_tu.TongMucDauTuEur,
	tong_muc_dau_tu.TongMucDauTuUsd, 
	tong_muc_dau_tu.TongMucDauTuVnd, 
	tong_muc_dau_tu.TongMucDauTuNgoaiTeKhac, 
	da_dautu.sSoQuyetDinh as SoQuyetDinh,
	da_dautu.dNgayQuyetDinh as NgayThangNam
	FROM Nh_DA_DuAn da
	left join 
	(
		select 
		SUM(dautu_chiphi.fGiaTriEUR) as TongMucDauTuEur
		,SUM(dautu_chiphi.fGiaTriUSD) as TongMucDauTuUsd
		,SUM(dautu_chiphi.fGiaTriVND) as TongMucDauTuVnd
		,SUM(dautu_chiphi.fGiaTriNgoaiTeKhac) as TongMucDauTuNgoaiTeKhac
		,dautu.iID_DuAnID
		FROM Nh_DA_DuAn da 
		
		left join Nh_DA_QDDauTu dautu on da.id = dautu.iID_DuAnID
		left join Nh_DA_QDDauTu_ChiPhi dautu_chiphi on dautu.id = dautu_chiphi.iID_QDDauTuID
		group by dautu.iID_DuAnID
	) tong_muc_dau_tu on tong_muc_dau_tu.iID_DuAnID = da.id
	inner join DM_ChuDauTu cdt on da.iID_ChuDauTuID = cdt.iId_Donvi
	inner join NH_DM_PhanCapPheDuyet cap_pd on da.iID_CapPheDuyetID = cap_pd.ID
	left join Nh_DA_QDDauTu da_dautu on da.id = da_dautu.iID_DuAnID
	where da.iID_MaDonViQuanLy = @MaDonVi and da_dautu.bIsActive = 1;
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_get_duan_export_ctc]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_nh_get_duan_export_ctc]
	@iLoai INT = NULL
AS
BEGIN
	SELECT da.Id, da.sMaDuAn as SMaDuAn, da.sTenDuAn as STenDuAn
	       , ct.sSoQuyetDinh as SSoDauTu, CONVERT(varchar, ct.dNgayQuyetDinh, 103) as DNgayDauTu
		   , qd.sSoQuyetDinh as SSoQuyetDinh, CONVERT(varchar, qd.dNgayQuyetDinh, 103) as DNgayQuyetDinh
		   , da.iID_MaChuDauTu as IIdMaChuDauTu, pc.sMa as SMaPhanCapPheDuyet
		   , da.sKhoiCong as SKhoiCong, da.sKetThuc as SKetThuc
	FROM NH_DA_DuAn da
	LEFT JOIN NH_DA_ChuTruongDauTu ct ON ct.iID_DuAnID = da.ID
	LEFT JOIN NH_DA_QDDauTu qd ON qd.iID_DuAnID = da.ID
	LEFT JOIN NH_DM_PhanCapPheDuyet pc ON pc.ID = da.iID_CapPheDuyetID
	WHERE da.iLoai = @iLoai;
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_gethangmuc_hopdongtrongnuoc_byidgoithau]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_gethangmuc_hopdongtrongnuoc_byidgoithau]
@IdGoiThau uniqueidentifier
AS
BEGIN
	select hd.ID as Id,
	hd.sSoHopDong as SSoHopDong,
	hd.dNgayHopDong as DNgayHopDong,
	hd.iID_LoaiHopDongID as IID_LoaiHopDongID,
	gtnt.iID_NhaThauID as IID_NhaThauID,
	gtntt.fGiaTRiHopDong_USD AS FGiaTriUsd,
	gtntt.fGiaTRiHopDong_VND AS FGiaTriVnd,
	lhd.sTenLoaiHopDong AS STenLoaiHopDong,
	nt.sTenNhaThau as STenNhaThau,
	gtntt.fGiaTRiHopDong_EUR AS FGiaTriEur,
	gtntt.fGiaTriHopDong_NgoaiTeKhac AS FGiaTriNgoaiTeKhac
	FROM NH_DA_HopDong hd
	LEFT JOIN NH_DA_HopDong_GoiThau_NhaThau gtnt on gtnt.iID_HopDongID=hd.ID 
	LEFT JOIN nh_da_goithau gt on gt.iID_GoiThauID = gtnt.iID_GoiThauID
	LEFT JOIN (select iID_HopDongID, sum(fGiaTRiHopDong_USD) as fGiaTRiHopDong_USD
				, sum(fGiaTRiHopDong_VND) as fGiaTRiHopDong_VND
				, sum(fGiaTRiHopDong_EUR) as fGiaTRiHopDong_EUR
				, sum(fGiaTriHopDong_NgoaiTeKhac) as fGiaTriHopDong_NgoaiTeKhac
				from NH_DA_HopDong_GoiThau_NhaThau 
				where NH_DA_HopDong_GoiThau_NhaThau.isCheck=1
				group by iID_HopDongID
	      ) gtntt on hd.ID = gtntt.iID_HopDongID
	LEFT JOIN NH_DM_LoaiHopDong lhd on hd.iID_LoaiHopDongID = lhd.iID_LoaiHopDongID
	LEFT JOIN NH_DM_NhaThau nt  on hd.iID_NhaThauThucHienID= nt.Id
	WHERE gtnt.iID_GoiThauID = @idGoiThau

	order by hd.sTenHopDong
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_gethangmucbyidgoithau]    Script Date: 11/3/2023 11:47:41 AM ******/
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_chiphi_by_goithau]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_goithau_chiphi_by_goithau]
@iIdKhlcnt uniqueidentifier
AS
BEGIN
	SELECT DISTINCT cp.ID as IIdChiPhiID, cp.iID_ParentID as IIdParentID, cp.sTenChiPhi as STenChiPhi, cp.sMaOrder as SMaOrder,
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
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_chiphi_by_GoiThau_listall]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_goithau_chiphi_by_GoiThau_listall]
@iIdKhlcnt uniqueidentifier
AS
BEGIN
	SELECT cp.iID_GoiThauID as IIdGoiThauId, 
			cp.iID_DuToan_ChiPhiID as IIdDuToanChiPhiId,
			cp.iID_CacQuyetDinh_ChiPhiID as IIdCacQuyetDinhChiPhiId,
			cp.iID_ChiPhiID as IIdChiPhiId,
			cp.iID_GoiThau_NguonVonID as IIdGoiThauNguonVonId,
			cp.iID_QDDauTu_ChiPhiID as IIdQdDauTuChiPhiId,
			cp.iID_ParentID as IIdParentId,
			cp.sMaOrder,
			cp.sTenChiPhi,
			cp.*

	FROM NH_DA_GoiThau_ChiPhi cp
	Left Join NH_DA_GoiThau gt on gt.iID_GoiThauID = cp.iID_GoiThauID
	Where gt.iID_GoiThauId = @iIdKhlcnt
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_chiphi_by_khlcnt]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_goithau_chiphi_by_khlcnt]
@iIdKhlcnt uniqueidentifier
AS
BEGIN
	SELECT cp.ID as IIdChiPhiID, cp.iID_ParentID as IIdParentID, cp.sTenChiPhi as STenChiPhi, cp.sMaOrder as SMaOrder,
			ISNULL(dt.fTienGoiThau_EUR, 0) as FGiaTriEUR, ISNULL(dt.fTienGoiThau_NgoaiTeKhac, 0) as FGiaTriNgoaiTeKhac ,
			ISNULL(dt.fTienGoiThau_USD, 0) as FGiaTriUSD, ISNULL(dt.fTienGoiThau_VND, 0) as FGiaTriVND, dt.iID_GoiThauID as IIdGoiThauID,
			1 as IsChecked
	FROM NH_DA_GoiThau as tbl
	INNER JOIN NH_DA_GoiThau_ChiPhi as dt on tbl.iID_GoiThauID = dt.iID_GoiThauID
	INNER JOIN NH_DA_DuToan_ChiPhi as cp on dt.iID_DuToan_ChiPhiID = cp.ID
	WHERE tbl.iId_KHLCNhaThau = @iIdKhlcnt
	UNION ALL
	SELECT cp.ID as IIdChiPhiID, cp.iID_ParentID as IIdParentID, cp.sTenChiPhi as STenChiPhi, cp.sMaOrder as SMaOrder,
			ISNULL(dt.fTienGoiThau_EUR, 0) as FGiaTriEUR, ISNULL(dt.fTienGoiThau_NgoaiTeKhac, 0) as FGiaTriNgoaiTeKhac ,
			ISNULL(dt.fTienGoiThau_USD, 0) as FGiaTriUSD, ISNULL(dt.fTienGoiThau_VND, 0) as FGiaTriVND, dt.iID_GoiThauID as IIdGoiThauID,
			1 as IsChecked
	FROM NH_DA_GoiThau as tbl
	INNER JOIN NH_DA_GoiThau_ChiPhi as dt on tbl.iID_GoiThauID = dt.iID_GoiThauID
	INNER JOIN NH_DA_QDDauTu_ChiPhi as cp on dt.iID_QDDauTu_ChiPhiID = cp.ID
	WHERE tbl.iId_KHLCNhaThau = @iIdKhlcnt
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_chiphi_by_khlcnt_listall]    Script Date: 11/3/2023 11:47:41 AM ******/
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_detail]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_goithau_detail]
	
AS BEGIN
	SELECT
	goithau.iID_GoiThauID as IIdGoiThauId,
	khtongthe.ID as IIdkeHoachTongTheId,
	TTnhiemvuchi.iID_DonViThuHuongID as IIdDonViThuHuongId,
	dmnhiemvuchi.sMaNhiemVuChi as SMaNhiemVuChi,
	dmnhiemvuchi.sTenNhiemVuChi as STenNhiemVuchi,
	cacquyetdinh.ID as IdCacQuyetDinh,
	cacquyetdinh.sSoQuyetDinh as SoQuyetDinh,
	TTnhiemvuchi.ID as IIdKeHoachTongTheNHiemVuChiId,
	TTnhiemvuchi.iID_NhiemVuChiID as IIdNhiemVuChiId,
	cacquyetdinh.iLoaiQuyetDinh as ILoaiQuyetDinh
	FROM NH_DA_GoiThau goithau
	INNER JOIN NH_HDNK_CacQuyetDinh cacquyetdinh
		ON goithau.iID_CacQuyetDinhID = cacquyetdinh.ID
	INNER JOIN NH_KHTongThe_NhiemVuChi TTnhiemvuchi
		ON cacquyetdinh.iID_KHTongThe_NhiemVuChiID = TTnhiemvuchi.ID
	LEFT join NH_DM_NhiemVuChi dmnhiemvuchi
		ON TTnhiemvuchi.iID_NhiemVuChiID = dmnhiemvuchi.ID
	INNER JOIN NH_KHTongThe khtongthe
		ON TTnhiemvuchi.iID_KHTongTheID = khtongthe.ID
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_hangmuc_by_goithau]    Script Date: 11/3/2023 11:47:41 AM ******/
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
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_hangmuc_by_khlcnt]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_goithau_hangmuc_by_khlcnt]
@iIdKhlcnt uniqueidentifier
AS
BEGIN
	SELECT hm.ID as IIdHangMucID, hm.iID_ParentID as IIdParentID, hm.iID_DuToan_ChiPhiID as IIdChiPhiID, 
		hm.sMaHangMuc as SMaHangMuc, hm.sTenHangMuc as STenHangMuc, hm.sMaOrder as SMaOrder,
		dt.fTienGoiThau_EUR as FGiaTriPheDuyetEUR, dt.fTienGoiThau_NgoaiTeKhac as FGiaTriPheDuyetNgoaiTeKhac ,
		dt.fTienGoiThau_USD as FGiaTriPheDuyetUSD, dt.fTienGoiThau_VND as FGiaTriPheDuyetVND, tbl.iID_GoiThauID as IIdGoiThauID,
		CAST(0 as float) as FGiaTriNgoaiTeKhac, 
		CAST(0 as float) as FGiaTriUSD, 
		CAST(0 as float) as FGiaTriEUR, 
		CAST(0 as float) as FGiaTriVND,
		1 as IsChecked
	FROM NH_DA_GoiThau as tbl	
	INNER JOIN NH_DA_GoiThau_ChiPhi as cp on tbl.iID_GoiThauID = cp.iID_GoiThauID
	INNER JOIN NH_DA_GoiThau_HangMuc as dt on cp.Id = dt.iID_GoiThau_ChiPhiID
	INNER JOIN NH_DA_DuToan_HangMuc as hm on dt.iID_DuToan_HangMucID = hm.ID
	WHERE tbl.iId_KHLCNhaThau = @iIdKhlcnt
	UNION ALL
	SELECT hm.ID as IIdHangMucID, hm.iID_ParentID as IIdParentID, hm.iID_QDDauTu_ChiPhiID as IIdChiPhiID, 
		hm.sMaHangMuc as SMaHangMuc, hm.sTenHangMuc as STenHangMuc, hm.sMaOrder as SMaOrder,
		dt.fTienGoiThau_EUR as FGiaTriPheDuyetEUR, dt.fTienGoiThau_NgoaiTeKhac as FGiaTriPheDuyetNgoaiTeKhac ,
		dt.fTienGoiThau_USD as FGiaTriPheDuyetUSD, dt.fTienGoiThau_VND as FGiaTriPheDuyetVND, tbl.iID_GoiThauID as IIdGoiThauID,
		CAST(0 as float) as FGiaTriNgoaiTeKhac, 
		CAST(0 as float) as FGiaTriUSD, 
		CAST(0 as float) as FGiaTriEUR, 
		CAST(0 as float) as FGiaTriVND,
		1 as IsChecked
	FROM NH_DA_GoiThau as tbl	
	INNER JOIN NH_DA_GoiThau_ChiPhi as cp on tbl.iID_GoiThauID = cp.iID_GoiThauID
	INNER JOIN NH_DA_GoiThau_HangMuc as dt on cp.Id = dt.iID_GoiThau_ChiPhiID
	INNER JOIN NH_DA_QDDauTu_HangMuc as hm on dt.iID_QDDauTu_HangMucID = hm.ID
	WHERE tbl.iId_KHLCNhaThau = @iIdKhlcnt
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_nguonvon_by_goithau]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_goithau_nguonvon_by_goithau]
@iIdKhlcnt uniqueidentifier
AS
BEGIN
	SELECT dt.iID_NguonVonID as IIdNguonVonID, nv.sTen as STenNguonVon, 
		ISNULL(dt.fTienGoiThau_NgoaiTeKhac, 0) as FGiaTriNgoaiTeKhac,
		ISNULL(dt.fTienGoiThau_USD, 0) as FGiaTriUSD,
		ISNULL(dt.fTienGoiThau_VND, 0) as FGiaTriVND,
		ISNULL(dt.fTienGoiThau_EUR, 0) as FGiaTriEUR,
		dt.iID_GoiThauID as IIdGoiThauID
	FROM NH_DA_GoiThau as tbl
	INNER JOIN NH_DA_GoiThau_NguonVon as dt on tbl.iID_GoiThauID = dt.iID_GoiThauID
	INNER JOIN NguonNganSach as nv on dt.iID_NguonVonID = nv.iID_MaNguonNganSach
	WHERE tbl.iID_GoiThauID = @iIdKhlcnt
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_nguonvon_by_goithau_listall]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_nh_goithau_nguonvon_by_goithau_listall]
@iIdKhlcnt uniqueidentifier
AS
BEGIN
	SELECT cp.iID_GoiThau_NguonVonID,
			cp.iID_GoiThauID as IIdGoiThauId,
			cp.iID_DuToan_NguonVonID as IIdDuToanNguonVonId,
			cp.iID_NguonVonID as IIdNguonVonID ,
			cp.iID_CacQuyetDinh_NguonVonID as IIdCacQuyetDinhNguonVonId,
			cp.iID_ChuTruongDauTu_NguonVonID as IIdChuTruongDauTuNguonVonId,
			cp.iID_DuAn_NguonVonID as IIdDuAnNguonVonId,
			cp.iID_QDDauTu_NguonVonID as IIdQdDauTuNguonVonId,
			cp.sMaOrder

	FROM NH_DA_GoiThau_NguonVon cp
	Left Join NH_DA_GoiThau gt on gt.iID_GoiThauID = cp.iID_GoiThauID
	Where gt.iID_GoiThauID = @iIdKhlcnt
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_nguonvon_by_khlcnt]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_goithau_nguonvon_by_khlcnt]
@iIdKhlcnt uniqueidentifier
AS
BEGIN
	SELECT dt.iID_NguonVonID as IIdNguonVonID, nv.sTen as STenNguonVon, 
		ISNULL(dt.fTienGoiThau_NgoaiTeKhac, 0) as FGiaTriNgoaiTeKhac,
		ISNULL(dt.fTienGoiThau_USD, 0) as FGiaTriUSD,
		ISNULL(dt.fTienGoiThau_VND, 0) as FGiaTriVND,
		ISNULL(dt.fTienGoiThau_EUR, 0) as FGiaTriEUR,
		dt.iID_GoiThauID as IIdGoiThauID,
		1 as IsChecked
	FROM NH_DA_GoiThau as tbl
	INNER JOIN NH_DA_GoiThau_NguonVon as dt on tbl.iID_GoiThauID = dt.iID_GoiThauID
	INNER JOIN NguonNganSach as nv on dt.iID_NguonVonID = nv.iID_MaNguonNganSach
	WHERE tbl.iId_KHLCNhaThau = @iIdKhlcnt
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_nguonvon_by_khlcnt_listall]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_nh_goithau_nguonvon_by_khlcnt_listall]
@iIdKhlcnt uniqueidentifier
AS
BEGIN
	SELECT * FROM NH_DA_GoiThau_NguonVon cp
	Left Join NH_DA_GoiThau gt on gt.iID_GoiThauID = cp.iID_GoiThauID
	Where gt.iId_KHLCNhaThau = @iIdKhlcnt
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_trongnuoc_index]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_nh_goithau_trongnuoc_index]
	@ILoai int,
	@IThuocMenu int
as begin
	Select DISTINCT
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
		goiThau.fTiGiaNhap as FTiGiaNhap,
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
		--goiThau.sSoKeHoachDatHang as SSoKeHoachDatHang,
		--goiThau.dNgayKeHoach as DNgayKeHoach,
		LCNhaThau.sSoQuyetDinh as SSoKeHoachDatHang,
		LCNhaThau.dNgayQuyetDinh as DNgayKeHoach,
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
		( SELECT COUNT ( Id ) FROM Attachment WHERE ModuleType = 405 AND ObjectId = goiThau.iID_GoiThauID ) AS TotalFiles,
		CASE
		WHEN goiThau.iID_ParentAdjustId IS NULL THEN
		'' ELSE ( SELECT TOP 1 hdpr.sMaGoiThau FROM NH_DA_GoiThau hdpr WHERE hdpr.iID_GoiThauID = goiThau.iID_ParentAdjustId ) 
		END DieuChinhTu ,
		LCNhaThau.sMoTa as SMota,
		nvChi.ID as IIdKHTTNhiemVuChiId,  
		nvChi.iID_NhiemVuChiID as IIDNhiemVuChiID,
		nvChi.iID_KHTongTheID as IIdKHTongTheID,
		nvChi.STenChuongTrinh as STenChuongTrinh

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
	LEFT JOIN (SELECT  n.ID, n.iID_KHTongTheID, d.sTenNhiemVuChi AS STenChuongTrinh, n.iID_NhiemVuChiID
	 FROM NH_KHTongThe_NhiemVuChi AS n 
	 INNER JOIN NH_DM_NhiemVuChi AS d 
	 ON n.iID_NhiemVuChiID = d.ID
	) AS nvChi ON goiThau.iID_KHTT_NhiemVuChiID = nvChi.ID
	WHERE goiThau.iLoai = @ILoai and goiThau.iThuocMenu = @IThuocMenu
	 ORDER BY goiThau.dNgayTao DESC
end
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_hangmuc_bychiphiid]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_hangmuc_bychiphiid]
	@idChiPhi uniqueidentifier
	
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
		DuToan.sTenHangMuc as STenHangMucDT
	FROM NH_DA_GoiThau_HangMuc HangMuc
	LEFT JOIN NH_DA_QDDauTu_HangMuc QDDT
		ON HangMuc.iID_QDDauTu_HangMucID = QDDT.ID
	LEFT JOIN NH_DA_DuToan_HangMuc DuToan
		ON HangMuc.iID_DuToan_HangMucID = DuToan.ID
	WHERE 
		1=1
		AND HangMuc.iID_GoiThau_ChiPhiID = @idChiPhi
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_hangmuc_bygoithauid]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROC [dbo].[sp_nh_hangmuc_bygoithauid]
	@idGoiThau uniqueidentifier
	
AS BEGIN
	SELECT
		HangMuc.iID_GoiThau_HangMucID as Id,
		HangMuc.isCheck as IsCheck,
		HangMuc.iID_GoiThau_ChiPhiID as IIdGoiThauChiPhiId,
		HangMuc.iIDGoiThauCheck as IIDGoiThauCheck,
		HangMuc.iID_QDDauTu_HangMucID as IIdQDDauTuHangMucId,
		HangMuc.iID_DuToan_HangMucID as IIdDuToanChiPhiId,
		HangMuc.fTienGoiThau_USD as FTienGoiThauUsd,
		HangMuc.fTienGoiThau_VND as FTienGoiThauVnd,
		HangMuc.fTienGoiThau_EUR as FTienGoiThauEur,
		HangMuc.fTienGoiThau_NgoaiTeKhac as FTienGoiThauNgoaiTeKhac,
		QDDT.sTenHangMuc as STenHangMucQDDT,
		DuToan.sTenHangMuc as STenHangMucDT,
		HangMuc.iID_ParentID as IIdParentId,
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_hdnk_cacquyetdinh_index]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_hdnk_cacquyetdinh_index] 
@iLoai int = NULL
AS 
	BEGIN 
	SELECT
	cqd.ID AS Id,
		cqd.sSoQuyetDinh AS SSoQuyetDinh,
		cqd.dNgayQuyetDinh AS DNgayQuyetDinh,
		cqd.sMoTaChiTiet_QuyetDinh AS SMoTaChiTietQuyetDinh,
		cqd.iID_TiGiaID AS IIdTiGiaId,
		cqd.iID_KHTongThe_NhiemVuChiID AS IIdKhTongTheNhiemVuChiId,
		cqd.iID_DuAnID AS IIdDuAnId,
		cqd.sMaNgoaiTeKhac AS SMaNgoaiTeKhac,
		cqd.iLoaiQuyetDinh AS ILoaiQuyetDinh,
		cqd.iID_DonViThucHien AS IIdDonViThucHien,
		cqd.iID_DonViQuanLy AS IIdDonViQuanLy,
		cqd.fGiaTriUSD AS FGiaTriUSD,
		cqd.fGiaTriVND AS FGiaTriVND,
		cqd.fGiaTriEUR AS FGiaTriEUR,
		cqd.fGiaTriNgoaiTeKhac AS FGiaTriNgoaiTeKhac,
		cqd.iID_GocID AS IIdGocId,
		cqd.dNgayTao AS DNgayTao,
		cqd.sNguoiTao AS SNguoiTao,
		cqd.dNgaySua AS DNgaySua,
		cqd.sNguoiSua AS SNguoiSua,
		cqd.dNgayXoa AS DNgayXoa,
		cqd.sNguoiXoa AS SNguoiXoa,
		cqd.bIsActive AS BIsActive,
		cqd.bIsGoc AS BIsGoc,
		cqd.bIsKhoa AS BIsKhoa,
		cqd.iLanDieuChinh AS ILanDieuChinh,
		cqd.bIsXoa AS BIsXoa,
		cqd.iID_ParentId AS IIdParentId,
		cqd.iID_ParentAdjustId AS IIdParentAdjustId,
		cqd.iLoai AS ILoai,
		cqd.fTiGiaNhap AS FTiGiaNhap,
		CONCAT(donVi.iID_MaDonVi, ' - ', donVi.sTenDonVi) AS STenDonVi,
		nvChi.STenChuongTrinh,
		nvChi.iID_KHTongTheID AS IIdKhTongTheId,
		cqd.iID_PhuongAnNhapKhauID AS IIdPhuongAnNhapKhauId,
		CASE
			WHEN paNhapKhau.sMoTa IS NULL THEN paNhapKhau.sSoQuyetDinh
			ELSE CONCAT(paNhapKhau.sSoQuyetDinh, ' - ', paNhapKhau.sMoTa)
		END SPhuongAnNhapKhau,
		CASE
			WHEN cqd.iID_ParentAdjustId IS NULL THEN
			'' ELSE ( SELECT TOP 1 cqdpr.sSoQuyetDinh FROM NH_HDNK_CacQuyetDinh cqdpr WHERE cqdpr.Id = cqd.iID_ParentAdjustId ) 
		END DieuChinhTu,
		(CASE
		WHEN da.sTenDuAn IS NULL THEN ''
		WHEN da.sMaDuAn IS NULL THEN da.sTenDuAn
		ELSE CONCAT(da.sMaDuAn, ' - ', da.sTenDuAn)
		END) AS STenDuAn
FROM
	NH_HDNK_CacQuyetDinh cqd
LEFT JOIN DonVi donVi
ON cqd.iID_DonViQuanLy = donVi.iID_DonVi AND cqd.iID_MaDonViQuanLy = donVi.iID_MaDonVi
LEFT JOIN
		(SELECT  n.ID, n.iID_KHTongTheID, d.sTenNhiemVuChi AS STenChuongTrinh
		 FROM NH_KHTongThe_NhiemVuChi AS n 
		 INNER JOIN NH_DM_NhiemVuChi AS d 
		 ON n.iID_NhiemVuChiID = d.ID) 
AS nvChi
ON cqd.iID_KHTongThe_NhiemVuChiID = nvChi.ID
LEFT JOIN NH_HDNK_PhuongAnNhapKhau paNhapKhau
ON cqd.iID_PhuongAnNhapKhauID = paNhapKhau.ID
LEFT JOIN NH_DA_DuAn da ON da.ID = cqd.iID_DuAnID
WHERE @iLoai IS NULL OR cqd.iLoai = @iLoai
ORDER BY
	cqd.dNgayTao DESC END;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_hdnk_delete_quyetdinhdampham]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_hdnk_delete_quyetdinhdampham] 
@id uniqueidentifier,  
@parentId uniqueidentifier 
AS BEGIN  
 DELETE NH_HDNK_CacQuyetDinh_ChiPhi_HangMuc WHERE iID_CacQuyetDinh_ChiPhiID IN (SELECT ID FROM NH_HDNK_CacQuyetDinh_ChiPhi where iID_CacQuyetDinhID = @id)
 DELETE NH_HDNK_CacQuyetDinh_ChiPhi WHERE iID_CacQuyetDinhID = @id
 DELETE NH_HDNK_CacQuyetDinh_NguonVon WHERE iID_CacQuyetDinhID = @id  
 DELETE NH_HDNK_CacQuyetDinh WHERE ID = @id  
END;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_hopdongtrongnuoc_index]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_hopdongtrongnuoc_index]

AS BEGIN
	SELECT 
	hopdong.Id AS Id,
	hopdong.sSoHopDong AS SSoHopDong,
	hopdong.dNgayHopDong AS DNgayHopDong,
	hopdong.sTenHopDong AS STenHopDong,
	hopdong.dKhoiCongDuKien AS DKhoiCongDuKien,
	hopdong.dKetThucDuKien AS DKetThucDuKien,
	hopdong.iID_LoaiHopDongID AS IIdLoaiHopDongId,
	hopdong.iID_CacQuyetDinhID AS IIdCacQuyetDinhId,
	hopdong.iID_KHTongThe_NhiemVuChiID AS IIdKhTongTheNhiemVuChiId,
	hopdong.iID_NhaThauThucHienID AS IIdNhaThauThucHienId,
	hopdong.iID_ParentAdjustID AS IIdParentAdjustId,
	hopdong.iID_ParentID AS IIdParentId,
	hopdong.iID_GoiThauID AS IIdGoiThauId,
	hopdong.iID_TiGiaID AS IIdTiGiaId,
	hopdong.iID_DuAnID AS IIdDuAnId,
	hopdong.fGiaTriHopDongNgoaiTeKhac AS FGiaTriHopDongNgoaiTeKhac,
	hopdong.fGiaTriHopDongUSD AS FGiaTriHopDongUSD,
	hopdong.fGiaTriHopDongEUR AS FGiaTriHopDongEUR,
	hopdong.fGiaTriHopDongVND AS FGiaTriHopDongVND,
	hopdong.dNgayTao AS DNgayTao,
	hopdong.sNguoiTao AS SNguoiTao,
	hopdong.dNgaySua AS DNgaySua,
	hopdong.sNguoiSua AS SNguoiSua,
	hopdong.sMaNgoaiTeKhac AS SMaNgoaiTeKhac,
	hopdong.dNgayXoa AS DNgayXoa,
	hopdong.sNguoiXoa AS SNguoiXoa,
	hopdong.bIsActive AS BIsActive,
	hopdong.bIsGoc AS BIsGoc,
	hopdong.bIsKhoa As BIsKhoa,
	hopdong.iLanDieuChinh AS ILanDieuChinh,
	duan.sTenDuAn AS STenDuAn,
	DonVi.sTenDonVi As STenDonVi,
	quyetdinh.sSoQuyetDinh as SSoQuyeDinh,
	DonVi.iID_DonVi AS IIdDonViId,
	( SELECT COUNT ( Id ) FROM Attachment WHERE ModuleType = 406 AND ObjectId = hopdong.ID ) AS TotalFiles,
	CASE
		WHEN hopdong.iID_ParentAdjustId IS NULL THEN
		'' ELSE ( SELECT TOP 1 hdpr.sSoHopDong FROM NH_DA_HopDong hdpr WHERE hdpr.Id = hopdong.iID_ParentAdjustId ) 
	END DieuChinhTu
	FROM NH_DA_HopDong hopdong
	LEFT JOIN NH_DA_DuAn duan
		ON hopdong.iID_DuAnID = duan.ID
	LEFT JOIN DonVi
		ON duan.iID_DonViQuanLyID = DonVi.iID_DonVi
	LEFT JOIN NH_HDNK_CacQuyetDinh quyetdinh
		ON hopdong.iID_CacQuyetDinhID = quyetdinh.ID
	where hopdong.iLoai = 4
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_kehoachtongthe_index]    Script Date: 11/3/2023 11:47:41 AM ******/
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_khchitiet_index]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_khchitiet_index] 
@YearOfWork int 
AS 
	BEGIN
	SELECT 
		ct.ID AS Id, 
		ct.sSoKeHoach AS SSoKeHoach, 
		ct.dNgayKeHoach AS DNgayKeHoach, 
		ct.sMoTaChiTiet AS SMoTaChiTiet,  
		ct.fGiaTriNgoaiTeKhac AS FGiaTriNgoaiTeKhac, 
		ct.fGiaTriUSD AS FGiaTriUSD, 
		ct.fGiaTriVND AS FGiaTriVND,
		ct.iID_ParentId AS IIdParentId,  
		ct.iID_ParentAdjustId AS IIdParentAdjustId,  
		ct.iID_GocID AS IIdGocId, 
		ct.dNgayTao AS DNgayTao, 
		ct.sNguoiTao AS SNguoiTao, 
		ct.dNgaySua AS DNgaySua, 
		ct.sNguoiSua AS SNguoiSua, 
		ct.dNgayXoa AS DNgayXoa, 
		ct.sNguoiXoa AS SNguoiXoa, 
		ct.bIsActive AS BIsActive, 
		ct.bIsGoc AS BIsGoc, 
		ct.bIsKhoa AS BIsKhoa, 
		ct.iLanDieuChinh AS ILanDieuChinh,
		tt.sSoKeHoachBQP AS SSoKeHoachTongTheBQP, 
		tt.ID AS IIdKHTongTheId, 
		nvc.iID_DonViThuHuongID AS IIdDonViThuHuongId, 
		(SELECT COUNT(Id) FROM Attachment WHERE ModuleType = 401 AND ObjectId = ct.ID) AS TotalFiles, 
		CASE WHEN ct.iID_ParentAdjustId is null THEN '' ELSE (
    SELECT TOP 1 ctpr.sSoKeHoach 
    FROM NH_KHChiTiet ctpr WHERE ctpr.Id = ct.iID_ParentAdjustId) END DieuChinhTu 
FROM 
  NH_KHChiTiet ct 
  LEFT JOIN NH_KHChiTiet_HopDong hd ON ct.id = hd.iID_KHChiTietID 
  LEFT JOIN NH_KHTongThe_NhiemVuChi nvc ON hd.iID_KHTongThe_NhiemVuChiID = nvc.ID 
  LEFT JOIN NH_KHTongThe tt ON nvc.iID_KHTongTheID = tt.ID 
ORDER BY 
  ct.dNgayTao DESC END;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_khlcnhathau_delete_by_id]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_khlcnhathau_delete_by_id]
@iId uniqueidentifier
AS
BEGIN
	SELECT iID_GoiThauID INTO #tmpGoiThau
	FROM NH_DA_GoiThau WHERE iId_KHLCNhaThau = @iId

	DELETE dt
	FROM #tmpGoiThau as tbl 
	INNER JOIN NH_DA_GoiThau_NguonVon as dt on tbl.iID_GoiThauID = dt.iID_GoiThauID

	DELETE dt
	FROM #tmpGoiThau as tbl 
	INNER JOIN NH_DA_GoiThau_ChiPhi as dt on tbl.iID_GoiThauID = dt.iID_GoiThauID
	INNER JOIN NH_DA_GoiThau_HangMuc as mp on mp.iID_GoiThau_ChiPhiID = dt.Id

	DELETE dt
	FROM #tmpGoiThau as tbl 
	INNER JOIN NH_DA_GoiThau_ChiPhi as dt on tbl.iID_GoiThauID = dt.iID_GoiThauID

	DELETE tbl
	FROM #tmpGoiThau as tmp
	INNER JOIN NH_DA_GoiThau as tbl on tmp.iID_GoiThauID = tbl.iID_GoiThauID

	DELETE NH_DA_KHLCNhaThau WHERE Id = @iId

	DROP TABLE #tmpGoiThau
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_khlcnhathau_get_duan]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_khlcnhathau_get_duan]
@iId uniqueidentifier
AS
BEGIN
	SELECT iID_DuAnID INTO #tmpDuAn FROM NH_DA_KHLCNhaThau WHERE Id <> @iId

	SELECT da.*
	FROM NH_DA_QDDauTu as tbl
	INNER JOIN NH_DA_DuAn as da on tbl.iID_DuAnID = da.ID
	LEFT JOIN #tmpDuAn as tmp on da.ID = tmp.iID_DuAnID
	WHERE tbl.bIsActive = 1 AND tmp.iID_DuAnID IS NULL

	DROP TABLE #tmpDuAn
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_khlcnhathau_index]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_khlcnhathau_index]
	@iThuocMenu int
AS
BEGIN
	SELECT
		tbl.Id, tbl.sSoQuyetDinh AS SSoQuyetDinh, tbl.dNgayQuyetDinh AS DNgayQuyetDinh, tbl.sMoTa AS SMoTa, da.iID_DonViQuanLyID as IIdDonViQuanLy, da.iID_MaDonViQuanLy as SMaDonViQuanLy, CONCAT(dv.iID_MaDonVi, ' - ', dv.sTenDonVi) AS STenDonVi,tbl.fTiGiaNhap as FTiGiaNhap,
		tbl.iID_DuAnID as IIdDuAnID, da.STenDuAn, tbl.ILanDieuChinh, tbl.iID_ParentID as IIdParentID , pr.sSoQuyetDinh as SSoQuyetDinhParent,
		tbl.BIsKhoa, tbl.BIsActive, CAST(0 as int) as ITepDinhKem, tbl.SNguoiTao, tbl.iID_QDDauTuID as IIdQDDauTuID, tbl.iID_DuToanID as IIdDuToanID, 
		tbl.iID_TiGiaID as IIdTiGiaID, tbl.sMaNgoaiTeKhac, tbl.iLoaiKHLCNT, tbl.iLoai, tbl.iThuocMenu, tbl.iID_DonViQuanLyID as IIdDonViQuanLyId ,
		tbl.iID_MaDonViQuanLy as IidMaDonViQuanLy, tbl.iID_KHTT_NhiemVuChiID as IIdKHTTNhiemVuChiId,
		nvChi.iID_NhiemVuChiID as IIDNhiemVuChiID,
		nvChi.iID_KHTongTheID as IIdKHTongTheID,
		nvChi.STenChuongTrinh as STenChuongTrinh
	FROM NH_DA_KHLCNhaThau as tbl
	INNER JOIN NH_DA_DuAn as da on tbl.iID_DuAnID = da.ID
	LEFT JOIN DonVi as dv on da.iID_DonViQuanLyID = dv.iID_DonVi
	LEFT JOIN NH_DA_KHLCNhaThau as pr on tbl.iID_ParentID = pr.Id
	LEFT JOIN (SELECT  n.ID, n.iID_KHTongTheID, d.sTenNhiemVuChi AS STenChuongTrinh, n.iID_NhiemVuChiID
	 FROM NH_KHTongThe_NhiemVuChi AS n 
	 INNER JOIN NH_DM_NhiemVuChi AS d 
	 ON n.iID_NhiemVuChiID = d.ID
	) AS nvChi ON tbl.iID_KHTT_NhiemVuChiID = nvChi.ID
	WHERE tbl.iThuocMenu = @iThuocMenu
	ORDER BY tbl.dNgayTao DESC
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_khlcnhathau_index_2]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_khlcnhathau_index_2]
AS
BEGIN
	SELECT tbl.Id, tbl.sSoQuyetDinh AS SSoQuyetDinh, tbl.dNgayQuyetDinh AS DNgayQuyetDinh, tbl.sMoTa AS SMoTa, nvc.iID_DonViThuHuongID as IIdDonViQuanLy, nvc.iID_MaDonViThuHuong as SMaDonViQuanLy,CONCAT(dv.iID_MaDonVi, ' - ', dv.sTenDonVi) AS STenDonVi, tbl.fTiGiaNhap as FTiGiaNhap,
		tbl.iID_DuAnID as IIdDuAnID, tbl.ILanDieuChinh, tbl.iID_ParentID as IIdParentID , pr.sSoQuyetDinh as SSoQuyetDinhParent,dutoan.sTenChuongTrinh as STenChuongTrinh,
		tbl.BIsKhoa, tbl.BIsActive, CAST(0 as int) as ITepDinhKem, tbl.SNguoiTao, tbl.iID_QDDauTuID as IIdQDDauTuID, tbl.iID_DuToanID as IIdDuToanID, 
		tbl.iID_TiGiaID as IIdTiGiaID, tbl.sMaNgoaiTeKhac, tbl.iLoaiKHLCNT, tbl.iLoai, tbl.iThuocMenu, tbl.iID_DonViQuanLyID as IIdDonViQuanLyId ,
		tbl.iID_MaDonViQuanLy as IidMaDonViQuanLy, tbl.iID_KHTT_NhiemVuChiID as IIdKHTTNhiemVuChiId, tbl.fTiGiaNhap as FTiGiaNhap,
		nvChi.iID_NhiemVuChiID as IIDNhiemVuChiID,
		nvChi.iID_KHTongTheID as IIdKHTongTheID,
		nvChi.STenChuongTrinh as STenChuongTrinh
	FROM NH_DA_KHLCNhaThau as tbl
	left join NH_KHTongThe_NhiemVuChi as nvc on tbl.iID_KHTT_NhiemVuChiID = nvc.ID  
	LEFT JOIN DonVi as dv on nvc.iID_DonViThuHuongID = dv.iID_DonVi
	LEFT JOIN NH_DA_KHLCNhaThau as pr on tbl.iID_ParentID = pr.Id
	LEFT JOIN NH_DA_DuToan as dutoan on tbl.iID_DuToanID = dutoan.ID
	LEFT JOIN (SELECT  n.ID, n.iID_KHTongTheID, d.sTenNhiemVuChi AS STenChuongTrinh, n.iID_NhiemVuChiID
	 FROM NH_KHTongThe_NhiemVuChi AS n 
	 INNER JOIN NH_DM_NhiemVuChi AS d 
	 ON n.iID_NhiemVuChiID = d.ID
	) AS nvChi ON tbl.iID_KHTT_NhiemVuChiID = nvChi.ID
	--WHERE tbl.bIsActive=1
	ORDER BY tbl.dNgayTao DESC
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_khlcnt_delete_goithau]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_khlcnt_delete_goithau]
@iIdGoiThaus t_tbl_uniqueidentifier READONLY
AS 
BEGIN
	DELETE dt
	FROM @iIdGoiThaus as tbl
	INNER JOIN NH_DA_GoiThau as dt on tbl.Id = dt.iID_GoiThauID
	
	EXEC sp_nh_khlcnt_delete_goithau_detail @iIdGoiThaus
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_khlcnt_delete_goithau_detail]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_khlcnt_delete_goithau_detail]
@iIdGoiThaus t_tbl_uniqueidentifier READONLY
AS
BEGIN
	DELETE dt
	FROM @iIdGoiThaus tbl
	INNER JOIN NH_DA_GoiThau_NguonVon as dt on tbl.Id = dt.iID_GoiThauID
	
	DELETE dt
	FROM  @iIdGoiThaus tbl
	INNER JOIN NH_DA_GoiThau_ChiPhi as cp on tbl.Id = cp.iID_GoiThauID
	INNER JOIN NH_DA_GoiThau_HangMuc as dt on cp.Id = dt.iID_GoiThau_ChiPhiID

	DELETE dt
	FROM @iIdGoiThaus tbl
	INNER JOIN NH_DA_GoiThau_ChiPhi as dt on tbl.Id = dt.iID_GoiThauID

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_khtongthe_nhiemvuchi_bydonvi]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_khtongthe_nhiemvuchi_bydonvi]
	@iDDonVi uniqueidentifier
AS BEGIN

	SELECT
	    tt_nvc.ID AS Id,
	    tt_nvc.iID_KHTongTheID,
	    tt_nvc.iID_NhiemVuChiID,
	    tt_nvc.iID_DonViThuHuongID,
	    donvi.sTenDonVi AS STenDonVi,
	    donvi.iID_MaDonVi AS SMaDonViThuHuong,
	    tt_nvc.fGiaTriKH_TTCP AS FGiaTriKhTTCP,
	    tt_nvc.fGiaTriKH_BQP AS FGiaTriKhBQP,
	    nvc.sMaNhiemVuChi,
	    nvc.sTenNhiemVuChi,
	    nvc.iLoaiNhiemVuChi 
	FROM NH_KHTongThe_NhiemVuChi tt_nvc
	JOIN NH_DM_NhiemVuChi nvc ON tt_nvc.iID_NhiemVuChiID = nvc.ID
	JOIN DonVi donvi on DonVi.iID_DonVi = tt_nvc.iID_DonViThuHuongID
	WHERE @iDDonVi IS NULL OR tt_nvc.iID_DonViThuHuongID = @iDDonVi
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_kt_khoi_tao_cap_phat]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_kt_khoi_tao_cap_phat]
	@INamLamViec int
AS
BEGIN
    SELECT
		khoiTaoCP.ID				AS Id,
		khoiTaoCP.[dNgayKhoiTao]	AS DNgayKhoiTao,
		khoiTaoCP.[iNamKhoiTao]		AS INamKhoiTao,
		khoiTaoCP.[iID_DonViID]		AS IIdDonViID,
		khoiTaoCP.[iID_MaDonVi]		AS IIdMaDonVi,
		khoiTaoCP.[iID_TiGiaID]		AS IIdTiGiaID,
		khoiTaoCP.[sMoTa]			AS SMoTa,
		khoiTaoCP.[dNgayTao]		AS DNgayTao,
		khoiTaoCP.[sNguoiTao]		AS SNguoiTao,
		khoiTaoCP.[dNgaySua]		AS DNgaySua,
		khoiTaoCP.[sNguoiSua]		AS SNguoiSua,
		khoiTaoCP.[dNgayXoa]		AS DNgayXoa,
		khoiTaoCP.[sNguoiXoa]		AS SNguoiXoa,
		khoiTaoCP.bIsKhoa			AS BIsKhoa,
		khoiTaoCP.[iID_TongHopID] 	AS IIdTongHopID,
		khoiTaoCP.[sTongHop] 		AS STongHop,
		khoiTaoCP.bIsXoa			AS BIsXoa,
		donVi.sTenDonVi				AS STenDonVi
		
	FROM NH_KT_KhoiTaoCapPhat khoiTaoCP
	LEFT JOIN DonVi donVi
		ON khoiTaoCP.iID_MaDonVi = donVi.iID_MaDonVi and donvi.iNamLamViec = @INamLamViec
	ORDER BY khoiTaoCP.[dNgayKhoiTao] DESC
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_kt_khoitaocapphat_chitiet]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_kt_khoitaocapphat_chitiet] @KhoiTaoCapPhatId NVARCHAR(2000)
AS
BEGIN
    SELECT
		khoiTaoCP_CT.ID									AS Id,
		khoiTaoCP_CT.iID_KhoiTaoCapPhatID				AS IIdKhoiTaoCapPhatID,
		khoiTaoCP_CT.iID_DuAnID							AS IIdDuAnID,
		khoiTaoCP_CT.iID_HopDongID						AS IIdHopDongID,
		khoiTaoCP_CT.fQTKinhPhiDuyetCacNamTruoc_USD		AS FQTKinhPhiDuyetCacNamTruocUSD,
		khoiTaoCP_CT.fQTKinhPhiDuyetCacNamTruoc_VND		AS FQTKinhPhiDuyetCacNamTruocVND,
		khoiTaoCP_CT.fDeNghiQTNamNay_USD				AS FDeNghiQTNamNayUSD,
		khoiTaoCP_CT.fDeNghiQTNamNay_VND				AS FDeNghiQTNamNayVND,
		khoiTaoCP_CT.fLuyKeKinhPhiDuocCap_USD			AS FLuyKeKinhPhiDuocCapUSD,
		khoiTaoCP_CT.fLuyKeKinhPhiDuocCap_VND			AS FLuyKeKinhPhiDuocCapVND,
		khoiTaoCP_CT.iID_ParentID						AS IIdParentID,
		duAn.sMaDuAn									AS SMaDuAn,
		duAn.sTenDuAn									AS STenDuAn,
		hopDong.sSoHopDong								AS SMaHopDong,
		hopDong.sTenHopDong								AS STenHopDong
		
	FROM NH_KT_KhoiTaoCapPhat_ChiTiet khoiTaoCP_CT
	LEFT JOIN NH_DA_DuAn duAn
		ON khoiTaoCP_CT.iID_DuAnID = duAn.Id
	LEFT JOIN NH_DA_HopDong hopDong
		ON khoiTaoCP_CT.iID_HopDongID = hopDong.Id
	WHERE khoiTaoCP_CT.iID_KhoiTaoCapPhatID = @KhoiTaoCapPhatId
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_kt_khoitaocapphat_chitiet_ById]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_kt_khoitaocapphat_chitiet_ById] @KhoiTaoCapPhatId NVARCHAR(500)
AS
BEGIN
    SELECT
		khoiTaoCP_CT.ID									AS Id,
		khoiTaoCP_CT.iID_KhoiTaoCapPhatID				AS IIdKhoiTaoCapPhatID,
		khoiTaoCP_CT.iID_DuAnID							AS IIdDuAnID,
		khoiTaoCP_CT.iID_HopDongID						AS IIdHopDongID,
		khoiTaoCP_CT.fQTKinhPhiDuyetCacNamTruoc_USD		AS FQTKinhPhiDuyetCacNamTruocUSD,
		khoiTaoCP_CT.fQTKinhPhiDuyetCacNamTruoc_VND		AS FQTKinhPhiDuyetCacNamTruocVND,
		khoiTaoCP_CT.fDeNghiQTNamNay_USD				AS FDeNghiQTNamNayUSD,
		khoiTaoCP_CT.fDeNghiQTNamNay_VND				AS FDeNghiQTNamNayVND,
		khoiTaoCP_CT.fLuyKeKinhPhiDuocCap_USD			AS FLuyKeKinhPhiDuocCapUSD,
		khoiTaoCP_CT.fLuyKeKinhPhiDuocCap_VND			AS FLuyKeKinhPhiDuocCapVND,
		khoiTaoCP_CT.iID_ParentID						AS IIdParentID,
		duAn.sMaDuAn									AS SMaDuAn,
		duAn.sTenDuAn									AS STenDuAn,
		hopDong.sSoHopDong								AS SSoHopDong,
		hopDong.sTenHopDong								AS STenHopDong,
		nvc.sMaNhiemVuChi                               AS SMaNhiemVuChi,
		nvc.sTenNhiemVuChi								AS STenNhiemVuChi,
		qdk.sSoQuyetDinh								AS SSoQuyetDinhKhac,
		qdk.sTenQuyetDinh								AS STenQuyetDinh,
		chiphi.sMaChiPhi								AS SMaChiPhi,
		chiphi.sTenChiPhi								AS STenChiPhi,
		(CASE
			WHEN chiphi.sMaChiPhi is not null AND chiphi.sTenChiPhi IS NOT NULL THEN CONCAT(chiphi.sMaChiPhi,' - ',chiphi.sTenVietTat)
			ELSE CONCAT(chiphi.sMaChiPhi,'',chiphi.sTenVietTat)
			END
		)																	AS STenChiPhiDetail,
		khoiTaoCP_CT.iLoaiNoiDung											AS ILoaiNoiDung,
		khoiTaoCP_CT.sTenNoiDung											AS STenNoiDung,
		khoiTaoCP_CT.sMaNoiDung												AS SMaNoiDung,
		qdDauTu.sSoQuyetDinh												AS SSoQuyetDinhDauTu,
		khoiTaoCP_CT.fLuyKeKinhPhiDaGiaiNganTrongNamNay_USD					AS FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd,
		khoiTaoCP_CT.fLuyKeKinhPhiDaGiaiNganTrongNamNay_VND					AS FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd,
		khoiTaoCP_CT.fLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNay_USD			AS FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd,
		khoiTaoCP_CT.fLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNay_VND			AS FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd,
		khoiTaoCP_CT.fLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNay_USD			AS FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd,
		khoiTaoCP_CT.fLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNay_VND			AS FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd,
		khoiTaoCP_CT.fLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNay_USD			AS FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd,
		khoiTaoCP_CT.fLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNay_VND			AS FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd,
		khoiTaoCP_CT.fDeNghiChuyenNamSau_VND								AS FDeNghiChuyenNamSauVnd,
		khoiTaoCP_CT.fDeNghiChuyenNamSau_USD								AS FDeNghiChuyenNamSauUsd,
		khoiTaoCP_CT.fKinhPhiThuaNopNSNN_USD								AS FKinhPhiThuaNopNsnnUSD,
		khoiTaoCP_CT.fKinhPhiThuaNopNSNN_VND								AS FKinhPhiThuaNopNsnnVND,
		khoiTaoCP_CT.fConLaiChuaGiaiNgan_USD								AS FConLaiChuaGiaiNganUSD,
		khoiTaoCP_CT.fConLaiChuaGiaiNgan_VND								AS FConLaiChuaGiaiNganVND	
		INTO #tmppp
		
	FROM NH_KT_KhoiTaoCapPhat_ChiTiet khoiTaoCP_CT
	LEFT JOIN NH_DA_DuAn duAn
		ON khoiTaoCP_CT.iID_DuAnID = duAn.Id
	LEFT JOIN NH_DA_QDDauTu qdDauTu ON qdDauTu.iID_DuAnID = duAn.ID
	LEFT JOIN NH_DA_HopDong hopDong
		ON khoiTaoCP_CT.iID_HopDongID = hopDong.ID
	LEFT JOIN NH_DA_QuyetDinhKhac qdk ON qdk.ID = khoiTaoCP_CT.iID_QuyetDinhKhacID
	LEFT JOIN NH_DM_ChiPhi chiphi ON chiphi.iID_ChiPhi = khoiTaoCP_CT.iID_ChiPhiID
	LEFT JOIN NH_KHTongThe_NhiemVuChi khtt on khtt.ID = khoiTaoCP_CT.iID_KHTT_NhiemVuChiID
	LEFT JOIN NH_DM_NhiemVuChi nvc on nvc.ID = khtt.iID_NhiemVuChiID

	WHERE khoiTaoCP_CT.iID_KhoiTaoCapPhatID = @KhoiTaoCapPhatId;
	--ORDER BY OrderName
	

	With #treeViews
	AS
	(
		SELECT *, CAST( ROW_NUMBER() OVER(ORDER by SMaNoiDung) AS nvarchar(max)) AS position FROM #tmppp where IIdParentID IS NULL OR ILoaiNoiDung = 1
		UNION ALL
		SELECT 
				chil.Id,
				chil.IIdKhoiTaoCapPhatID,
				chil.IIdDuAnID,
				chil.IIdHopDongID,
				chil.FQTKinhPhiDuyetCacNamTruocUSD,
				chil.FQTKinhPhiDuyetCacNamTruocVND,
				chil.FDeNghiQTNamNayUSD,
				chil.FDeNghiQTNamNayVND,
				chil.FLuyKeKinhPhiDuocCapUSD,
				chil.FLuyKeKinhPhiDuocCapVND,
				chil.IIdParentID,
				chil.SMaDuAn,
				chil.STenDuAn,
				chil.SSoHopDong,
				chil.STenHopDong,
				chil.SMaNhiemVuChi,
				chil.STenNhiemVuChi,
				chil.SSoQuyetDinhKhac,
				chil.STenQuyetDinh,
				chil.SMaChiPhi,
				chil.STenChiPhi,
				chil.STenChiPhiDetail,
				chil.ILoaiNoiDung,
				chil.STenNoiDung,
				chil.SMaNoiDung,
				chil.SSoQuyetDinhDauTu,
				chil.FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd,
				chil.FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd,
				chil.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd,
				chil.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd,
				chil.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd,
				chil.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd,
				chil.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd,
				chil.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd,
				chil.FDeNghiChuyenNamSauVnd,
				chil.FDeNghiChuyenNamSauUsd,
				chil.FKinhPhiThuaNopNsnnUSD,
				chil.FKinhPhiThuaNopNsnnVND,
				chil.FConLaiChuaGiaiNganUSD,
				chil.FConLaiChuaGiaiNganVND,
				CONCAT(pr.position,'.',CAST(ROW_NUMBER() OVER(ORDER BY chil.SMaNoiDung) AS NVARCHAR(MAX))) AS position
		
		FROM #treeViews pr
		INNER JOIN #tmppp chil ON pr.Id = chil.IIdParentID

	)

		SELECT *,
		 	cast('/' + replace(position, '.', '/') + '/' as hierarchyid) AS sort
		 FROM  #treeViews
		 ORDER  BY sort;

	DROP TABLE  #tmppp;
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_mstn_khdh_delete_by_id]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROC [dbo].[sp_nh_mstn_khdh_delete_by_id]
@iId uniqueidentifier
AS
BEGIN
	DELETE NH_MSTN_KeHoachDatHang_DanhMuc WHERE iID_KeHoachDatHang = @iId;
	DELETE NH_MSTN_KeHoachDatHang WHERE ID = @iId;
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_mstnkehoachdathang_bycondition]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_mstnkehoachdathang_bycondition]
	@DonviId uniqueidentifier,
	@KeHoachTongTheId uniqueidentifier,
	@ChuongTrinhId uniqueidentifier
AS
BEGIN
	SELECT tbl.Id, tbl.SSoQuyetDinh, tbl.DNgayQuyetDinh, tbl.SMoTa, tbl.iID_DonViID as IIdDonViQuanLy, dv.sTenDonVi,
		tbl.ILanDieuChinh, tbl.iID_ParentID as IIdParentID,
		tbl.BIsActive, tbl.SNguoiTao, 
		tbl.iID_TiGiaID as IIdTiGiaID, tbl.sMaNgoaiTeKhac, tbl.fTiGiaNhap AS FTiGiaNhap,
		tbl.fGiaTriEUR as FGiaTriEur, tbl.fGiaTriUSD as FGiaTriUsd, tbl.fGiaTriVND as FGiaTriVnd, tbl.fGiaTriNgoaiTeKhac as FGiaTriNgoaiTeKhac,
		tbl.iID_MaDonVi as SMaDonViQuanLy,
		tbl.iID_KHTT_NhiemVuChiID as IIdKHTTNhiemVuChiId,
		dmnvc.sTenNhiemVuChi as STenChuongTrinh,
		nvc.iID_KHTongTheID as IIdKhtongTheId
	FROM NH_MSTN_KeHoachDatHang as tbl
	LEFT JOIN DonVi as dv on tbl.iID_DonViID = dv.iID_DonVi
	LEFT JOIN NH_KHTongThe_NhiemVuChi as nvc on tbl.iID_KHTT_NhiemVuChiID = nvc.ID  
	LEFT JOIN NH_DM_NhiemVuChi as dmnvc on nvc.iID_NhiemVuChiID = dmnvc.ID
	WHERE tbl.bIsActive=1
		AND (@DonviId IS NULL OR tbl.iID_DonViID = @DonviId)
		AND (@KeHoachTongTheId IS NULL OR nvc.iID_KHTongTheID = @KeHoachTongTheId)
		AND (@ChuongTrinhId IS NULL OR tbl.iID_KHTT_NhiemVuChiID = @ChuongTrinhId)
	ORDER BY tbl.dNgayTao DESC
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_mstnkehoachdathang_index]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_nh_mstnkehoachdathang_index]
AS
BEGIN
	SELECT tbl.Id, tbl.SSoQuyetDinh, tbl.DNgayQuyetDinh, tbl.SMoTa,
		tbl.iID_DonViID AS IIdDonViQuanLy, CONCAT(dv.iID_MaDonVi, ' - ', dv.sTenDonVi) AS STenDonVi,
		tbl.ILanDieuChinh, tbl.iID_ParentID AS IIdParentID , pr.sSoQuyetDinh AS SSoQuyetDinhParent,
		tbl.BIsActive, tbl.SNguoiTao, 
		tbl.iID_TiGiaID AS IIdTiGiaID, tbl.sMaNgoaiTeKhac, tbl.fTiGiaNhap AS FTiGiaNhap,
		tbl.fGiaTriEUR AS FGiaTriEur, tbl.fGiaTriUSD AS FGiaTriUsd, tbl.fGiaTriVND AS FGiaTriVnd, tbl.fGiaTriNgoaiTeKhac AS FGiaTriNgoaiTeKhac,
		tbl.iID_MaDonVi AS SMaDonViQuanLy, tbl.iID_KHTT_NhiemVuChiID AS IIdKHTTNhiemVuChiId,
		dmnvc.sTenNhiemVuChi AS STenChuongTrinh, nvc.iID_KHTongTheID AS IIdKhtongTheId,
		CONCAT(N'KHTT ', khtt.iGiaiDoanTu_BQP, '-', khtt.iGiaiDoanDen_BQP, N' - Số KH: ', khtt.sSoKeHoachBQP) AS STenKeHoachTongThe
	FROM NH_MSTN_KeHoachDatHang AS tbl
	LEFT JOIN DonVi AS dv ON tbl.iID_DonViID = dv.iID_DonVi
	LEFT JOIN NH_MSTN_KeHoachDatHang AS pr ON tbl.iID_ParentID = pr.Id
	LEFT JOIN NH_KHTongThe_NhiemVuChi AS nvc ON tbl.iID_KHTT_NhiemVuChiID = nvc.ID
	LEFT JOIN NH_KHTongThe AS khtt ON nvc.iID_KHTongTheID = khtt.ID
	LEFT JOIN NH_DM_NhiemVuChi AS dmnvc ON nvc.iID_NhiemVuChiID = dmnvc.ID  
	ORDER BY tbl.dNgayTao DESC
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_nhiemvuchi_bykehoachtongthe_donvi]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_nhiemvuchi_bykehoachtongthe_donvi]
	@idKeHoachTongThe uniqueidentifier,
	@idDonVi uniqueidentifier
AS BEGIN

	SELECT 
		KHTTNhiemVuChi.ID as IIdKHTTNhiemVuChiId,
		NhiemVuChi.Id as Id,
		NhiemVuChi.sMaNhiemVuChi as SMaNhiemVuChi,
		NhiemVuChi.sTenNhiemVuChi as STenNhiemVuChi,
		NhiemVuChi.sMoTaChiTiet as SMoTaChiTiet,
		NhiemVuChi.iLoaiNhiemVuChi as iLoaiNhiemVuChi
	FROM NH_DM_NhiemVuChi NhiemVuChi
	INNER JOIN NH_KHTongThe_NhiemVuChi KHTTNhiemVuChi
		ON NhiemVuChi.ID = KHTTNhiemVuChi.iID_NhiemVuChiID
	INNER JOIN NH_KHTongThe NHKHTongThe
		ON KHTTNhiemVuChi.iID_KHTongTheID = NHKHTongThe.ID
	WHERE
		1=1
		AND NHKHTongThe.ID = @idKeHoachTongThe
		AND KHTTNhiemVuChi.iID_DonViThuHuongID = @idDonVi
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_nhiemvuchi_findtree_by_ids]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TungNH
-- Create date: 10/05/2022
-- Description:	Lấy toàn bộ cây nhiệm vụ chi theo danh sách id
-- =============================================
CREATE PROCEDURE [dbo].[sp_nh_nhiemvuchi_findtree_by_ids]
	@Ids NVARCHAR(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    WITH #CTE AS 
	(
		SELECT 1 AS [Level], Id AS GroupId, * FROM NH_DM_NhiemVuChi WHERE Id IN (SELECT * FROM f_split(@Ids))
		UNION ALL
		SELECT [Level] + 1 AS [Level], GroupId, t.* FROM NH_DM_NhiemVuChi t JOIN #CTE c ON c.iID_ParentID = t.ID
	)
	SELECT
		c.[Level] AS [Level],
		c.ID AS Id,
		c.iID_ParentID AS IIdParentId,
		c.sMaNhiemVuChi AS SMaNhiemVuChi,
		c.sTenNhiemVuChi AS STenNhiemVuChi,
		c.sMoTaChiTiet AS SMoTaChiTiet,
		c.iLoaiNhiemVuChi AS ILoaiNhiemVuChi,
		khttNhiemVuChi.ID AS IIdKHTTNhiemVuChiId,
		fGiaTriKH_TTCP AS FKeHoachTtcpUsd,
		fGiaTriKH_BQP AS FKeHoachBqpUsd
	FROM #CTE c
	LEFT JOIN NH_KHTongThe_NhiemVuChi khttNhiemVuChi
		ON c.ID = khttNhiemVuChi.iID_NhiemVuChiID
	ORDER BY c.GroupId, c.sMaOrder;
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_nhucauchiquy_chitiet_byIDHopDong]    Script Date: 11/3/2023 11:47:41 AM ******/
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_nhucauchiquy_index]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_nhucauchiquy_index]
AS BEGIN
	SELECT 
		chiquy.ID AS ID,
		chiquy.iID_ParentID AS IIdParentId,
		chiquy.iID_GocID AS IIdGocId,
		chiquy.sSoDeNghi AS SSoDeNghi,
		chiquy.dNgayDeNghi AS DNgayDeNghi,
		chiquy.iNamKeHoach AS INamKeHoach,
		chiquy.iQuy AS IQuy,
		chiquy.iID_DonViID AS IIdDonViId,
		chiquy.iID_MaDonVi AS IIdMaDonVi,
		chiquy.iID_NguonVonID AS IIdNguonVonId,
		chiquy.sNguoiLap AS SNguoiLap,
		chiquy.iID_TiGiaID AS IIdTiGiaId,
		chiquy.sMaNgoaiTeKhac AS SMaNgoaiTeKhac,
		chiquy.dNgayTao AS DNgayTao,
		chiquy.sNguoiTao AS SNguoiTao,
		chiquy.dNgaySua AS DNgaySua,
		chiquy.sNguoiSua AS SNguoiSua,
		chiquy.dNgayXoa AS DNgayXoa,
		chiquy.sNguoiXoa AS SNguoiXoa,
		chiquy.bIsActive AS BIsActive,
		chiquy.bIsGoc AS BIsGoc,
		chiquy.bIsKhoa AS BIsKhoa,
		chiquy.iLanDieuChinh AS iLanDieuChinh,
		chiquy.bIsXoa AS BIsXoa,
		( SELECT COUNT ( Id ) FROM Attachment WHERE ModuleType = 413 AND ObjectId = chiquy.ID ) AS TotalFiles,
		CONCAT(DonVi.iID_MaDonVi , ' - ', DonVi.sTenDonVi) AS STenDonVi,
		NguonNganSach.sTen AS STenNguonVon,
		chiquy.sTongHop AS STongHop
	FROM NH_NhuCauChiQuy chiquy
	LEFT JOIN DonVi 
	ON chiquy.iID_DonViID = DonVi.iID_DonVi
	LEFT JOIN NguonNganSach
	ON chiquy.iID_NguonVonID = NguonNganSach.iID_MaNguonNganSach
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_pheduyet_quyettoandaht_create]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_nh_pheduyet_quyettoandaht_create]
	-- Add the parameters for the stored procedure here
      @iIDDonVi uniqueidentifier,
	  @iNamBaoCaoTu int ,
	  @iNamBaoCaoDen int ,
	  @devideDonViUSD int = null,
      @devideDonViVND int = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	declare @fromDate datetime = cast(concat(CONVERT(varchar(10), @iNamBaoCaoTu),'-01-01 00:00:00.000') as datetime);
	declare @toDate datetime = cast(concat(CONVERT(varchar(10), @iNamBaoCaoDen),'-01-01 00:00:00.000') as datetime);

    -- Insert statements for procedure here
		--bang tam
select  a.iID_ThanhToan_ChiTietID,b.iNamKeHoach,sum(a.fQTKinhPhiDuocCap_TongSo_USD) as fQTKinhPhiDuocCap_TongSo_USD,sum(fQTKinhPhiDuocCap_TongSo_VND) as fQTKinhPhiDuocCap_TongSo_VND,b.iID_DonViID
into #tmpkpdc 
from NH_QT_QuyetToanNienDo_ChiTiet a 
left join NH_QT_QuyetToanNienDo b on a.iID_QuyetToanNienDoID = b.ID
where b.iNamKeHoach between @iNamBaoCaoTu and @iNamBaoCaoDen and b.iID_DonViID = @iIDDonVi
group by b.iNamKeHoach,b.iID_DonViID, a.iID_ThanhToan_ChiTietID

--
select a.iID_ThanhToan_ChiTietID,(isnull(a.fQTKinhPhiDuyetCacNamTruoc_USD,0) + isnull(a.fDeNghiQTNamNay_USD,0)) as fQuyetToanDuocDuyet_Tong_USD,
(isnull(a.fQTKinhPhiDuyetCacNamTruoc_VND,0) + isnull(a.fDeNghiQTNamNay_VND,0)) as fQuyetToanDuocDuyet_Tong_VND 
into #qtdd
from NH_QT_QuyetToanNienDo_ChiTiet a left join NH_QT_QuyetToanNienDo b on a.iID_QuyetToanNienDoID = b.ID
where b.iNamKeHoach between @iNamBaoCaoTu and @iNamBaoCaoDen and a.iID_ParentID is null and b.iID_DonViID = @iIDDonVi 

    -- Insert statements for procedure here
	select distinct 
	ttct.sTenNoiDungChi as STenNoiDungChi
	,tt.iID_DonVi as IIDDonViId
	,dv.iID_MaDonVi + ' - '+dv.sTenDonVi as sTenDonVi
	,da.sTenDuAn as STenDuAn
	,hd.sTenHopDong as STenHopDong	
	,nvc.sTenNhiemVuChi as STenNhiemVuChi
	,tt.iLoaiNoiDungChi as ILoaiNoiDungChi
	,tt.iID_NhiemVuChiID as IIDKHTTNhiemVuChiId

	,ttct.ID  as IIDThanhToanChiTietId
	,tt.iID_DuAnID as IIDDuAnId
	,tt.iID_HopDongID as IIDHopDongId
	 ,sum(IIF(@devideDonViUSD is not null, round(da.fUSD/@devideDonViUSD,2), da.fUSD))  as FHopDongUsdDuAn
	   ,sum(IIF(@devideDonViVND is not null, round(da.fVND/@devideDonViVND,2), da.fVND))  as FHopDongVndDuAn
	   ,sum(IIF(@devideDonViUSD is not null, round(hd.fGiaTriUSD/@devideDonViUSD,2), hd.fGiaTriUSD))  as FHopDongUsdHopDong
	   ,sum(IIF(@devideDonViVND is not null, round(hd.fGiaTriVND/@devideDonViVND,2), hd.fGiaTriVND))  as FHopDongVndHopDong
	   ,tongThe.iGiaiDoanDen as INamBaoCaoDen
	   ,tongThe.iGiaiDoanTu as INamBaoCaoTu
	   ,IIF(@devideDonViUSD is not null, round(sum(tongTheNvc.fGiaTriKH_TTCP)/@devideDonViUSD,2), sum(tongTheNvc.fGiaTriKH_TTCP)) as FKeHoachTTCPUsd
	   ,IIF(@devideDonViUSD is not null,round(sum(tmpkpdc.fQTKinhPhiDuocCap_TongSo_USD)/@devideDonViUSD,2), sum(tmpkpdc.fQTKinhPhiDuocCap_TongSo_USD)) as FKinhPhiDuocCapTongUsd
	   ,IIF(@devideDonViVND is not null, round(sum(tmpkpdc.fQTKinhPhiDuocCap_TongSo_VND)/@devideDonViVND,2), sum(tmpkpdc.fQTKinhPhiDuocCap_TongSo_VND)) as FKinhPhiDuocCapTongVnd
	   ,IIF(@devideDonViUSD is not null, round(sum(isnull(qtdd.fQuyetToanDuocDuyet_Tong_USD,0))/@devideDonViUSD,2), sum(isnull(qtdd.fQuyetToanDuocDuyet_Tong_USD,0))) as FQuyetToanDuocDuyetTongUsd
	   ,IIF(@devideDonViVND is not null, round(sum(isnull(qtdd.fQuyetToanDuocDuyet_Tong_VND,0))/@devideDonViVND,2), sum(isnull(qtdd.fQuyetToanDuocDuyet_Tong_VND,0))) as FQuyetToanDuocDuyetTongVnd

	   --,IIF(@devideDonViUSD is not null, round(sum(tmpkpdc.fQTKinhPhiDuocCap_TongSo_USD)-sum(isnull(qtndct.fQTKinhPhiDuyetCacNamTruoc_USD,0)+isnull(qtndct.fDeNghiQTNamNay_USD,0))/@devideDonViUSD,2), sum(tmpkpdc.fQTKinhPhiDuocCap_TongSo_USD)-sum(isnull(qtndct.fQTKinhPhiDuyetCacNamTruoc_USD,0)+isnull(qtndct.fDeNghiQTNamNay_USD,0))) as FSoSanhKinhPhiUsd
	   --,IIF(@devideDonViVND is not null, round(sum(tmpkpdc.fQTKinhPhiDuocCap_TongSo_VND)-sum(isnull(qtndct.fQTKinhPhiDuyetCacNamTruoc_VND,0)+isnull(qtndct.fDeNghiQTNamNay_VND,0))/@devideDonViVND,2), sum(tmpkpdc.fQTKinhPhiDuocCap_TongSo_VND)-sum(isnull(qtndct.fQTKinhPhiDuyetCacNamTruoc_VND,0)+isnull(qtndct.fDeNghiQTNamNay_VND,0))) as FSoSanhKinhPhiVnd

from NH_TT_ThanhToan_ChiTiet ttct 
left join NH_TT_ThanhToan tt on ttct.iID_DeNghiThanhToanID = tt.ID
left join NH_KHTongThe tongThe on tt.iID_KHTongTheID = tongThe.ID
left join NH_DM_NhiemVuChi nvc on tt.iID_NhiemVuChiID = nvc.ID
left join NH_KHTongThe_NhiemVuChi tongTheNvc on nvc.ID = tongTheNvc.iID_NhiemVuChiID
left join DonVi dv on  tt.iID_DonVi = dv.iID_DonVi 
left join NH_DA_DuAn da on tt.iID_DuAnID = da.ID
left join NH_DA_HopDong hd on tt.iID_HopDongID = hd.ID
left join #tmpkpdc tmpkpdc on tt.iID_DonVi = tmpkpdc.iID_DonViID and tmpkpdc.iNamKeHoach between tongThe.iGiaiDoanTu and tongThe.iGiaiDoanDen and ttct.ID = tmpkpdc.iID_ThanhToan_ChiTietID
left join #qtdd qtdd on ttct.Id = qtdd.iID_ThanhToan_ChiTietID
left join NH_QT_QuyetToanNienDo qtnd on tongThe.iGiaiDoanDen = qtnd.iNamKeHoach
left join NH_QT_QuyetToanNienDo_ChiTiet qtndct on qtnd.ID = qtndct.iID_QuyetToanNienDoID
where 
tt.dNgayDeNghi >=
@fromDate
and 
tt.dNgayDeNghi <=
@toDate
and 
tt.iID_DonVi = @iIDDonVi
group by ttct.sTenNoiDungChi,tongThe.iGiaiDoanDen,tongThe.iGiaiDoanTu,da.sTenDuAn,tt.iID_DonVi,dv.iID_MaDonVi
,dv.sTenDonVi,hd.sTenHopDong,nvc.sTenNhiemVuChi,tt.iLoaiNoiDungChi,tt.iID_NhiemVuChiID,ttct.ID,tt.iID_HopDongID,tt.iID_DuAnID
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_pheduyet_quyettoandaht_detail]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_nh_pheduyet_quyettoandaht_detail] 
	-- Add the parameters for the stored procedure here
  @iIDDonVi uniqueidentifier,
  @iDPheDuyetQuyetToan uniqueidentifier,
  @devideDonViUSD float = null,
  @devideDonViVND float = null

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
		select 
	    qtdact.ID as ID
       ,qtdact.iID_PheDuyetQuyetToanDAHT_ID as IIDPheDuyetQuyetToanDAHTId
	   ,Case When qtdact.iID_KHTT_NhiemVuChiID is null then '00000000-0000-0000-0000-000000000000' else qtdact.iID_KHTT_NhiemVuChiID End as IIDKHTTNhiemVuChiId
	   ,Case When qtdact.iID_DuAnID is null then '00000000-0000-0000-0000-000000000000' else qtdact.iID_DuAnID End as IIDDuAnId
	   ,qtdact.iID_HopDongID as IIDHopDongId
       ,qtdact.iID_ThanhToan_ChiTietID as IIDThanhToanChiTietId
       ,IIF(@devideDonViUSD is not null, round(qtdact.fHopDong_USD/@devideDonViUSD,2), qtdact.fHopDong_USD) as FHopDongUsd
	   ,IIF(@devideDonViVND is not null, round(qtdact.fHopDong_VND/@devideDonViVND,2), qtdact.fHopDong_VND) as FHopDongVnd
	   ,IIF(@devideDonViUSD is not null, round(qtdact.fKeHoach_TTCP_USD/@devideDonViUSD,2), qtdact.fKeHoach_TTCP_USD) as FKeHoachTTCPUsd
	   ,IIF(@devideDonViUSD is not null, round(qtdact.fKinhPhiDuocCap_Tong_USD/@devideDonViUSD,2), qtdact.fKinhPhiDuocCap_Tong_USD) as FKinhPhiDuocCapTongUsd
	   ,IIF(@devideDonViVND is not null, round(qtdact.fKinhPhiDuocCap_Tong_VND/@devideDonViVND,2), qtdact.fKinhPhiDuocCap_Tong_VND) as FKinhPhiDuocCapTongVnd
	   ,IIF(@devideDonViUSD is not null, round(qtdact.fQuyetToanDuocDuyet_Tong_USD/@devideDonViUSD,2), qtdact.fQuyetToanDuocDuyet_Tong_USD) as FQuyetToanDuocDuyetTongUsd
	   ,IIF(@devideDonViVND is not null, round(qtdact.fQuyetToanDuocDuyet_Tong_VND/@devideDonViVND,2), qtdact.fQuyetToanDuocDuyet_Tong_VND) as FQuyetToanDuocDuyetTongVnd
	   ,IIF(@devideDonViUSD is not null, round(qtdact.fSoSanhKinhPhi_USD/@devideDonViUSD,2), qtdact.fSoSanhKinhPhi_USD) as FSoSanhKinhPhiUsd
	   ,IIF(@devideDonViVND is not null, round(qtdact.fSoSanhKinhPhi_VND/@devideDonViVND,2), qtdact.fSoSanhKinhPhi_VND) as FSoSanhKinhPhiVnd
	   ,IIF(@devideDonViUSD is not null, round(qtdact.fThuaTraNSNN_USD/@devideDonViUSD,2), qtdact.fThuaTraNSNN_USD) as FThuaTraNSNNUsd
	   ,IIF(@devideDonViVND is not null, round(qtdact.fThuaTraNSNN_VND/@devideDonViVND,2), qtdact.fThuaTraNSNN_VND) as FThuaTraNSNNVnd
	   ,IIF(@devideDonViUSD is not null, round(da.fUSD/@devideDonViUSD,2), da.fUSD) as FHopDongUsdDuAn
	   ,IIF(@devideDonViVND is not null, round(da.fVND/@devideDonViVND,2), da.fVND) as FHopDongVndDuAn
	   ,IIF(@devideDonViUSD is not null, round(hd.fGiaTriUSD/@devideDonViUSD,2), hd.fGiaTriUSD) as FHopDongUsdHopDong
	   ,IIF(@devideDonViVND is not null, round(hd.fGiaTriVND/@devideDonViVND,2), hd.fGiaTriVND) as FHopDongVndHopDong

       ,qtdact.iNamBaoCaoTu as INamBaoCaoTu
       ,qtdact.iNamBaoCaoDen as INamBaoCaoDen
	   ,ttct.sTenNoiDungChi as STenNoiDungChi
	   ,tt.iID_DonVi as IIDDonViId
	   ,dv.iID_MaDonVi + ' - '+dv.sTenDonVi as STenDonVi
	   ,da.sTenDuAn as STenDuAn
	   ,hd.sTenHopDong as STenHopDong
	   ,ttct.sTenNoiDungChi as STenNoiDungChi
	   ,nvc.sTenNhiemVuChi as STenNhiemVuChi
	   ,tt.iLoaiNoiDungChi  as ILoaiNoiDungChi
	   
from NH_QT_PheDuyetQuyetToanDAHT_ChiTiet qtdact
left join NH_QT_PheDuyetQuyetToanDAHT qtda on qtdact.iID_PheDuyetQuyetToanDAHT_ID = qtda.ID
left join  NH_KHTongThe_NhiemVuChi khbqp on qtdact.iID_KHTT_NhiemVuChiID = khbqp.ID
left join  NH_DM_NhiemVuChi nvc on khbqp.iID_NhiemVuChiID = nvc.ID
left join NH_DA_DuAn da on  qtdact.iID_DuAnId = da.ID 
left join DonVi dv on  qtda.iID_DonViID = dv.iID_DonVi 
left join NH_DA_HopDong hd on qtdact.iID_HopDongID = hd.ID
left join NH_TT_ThanhToan_ChiTiet ttct on qtdact.iID_ThanhToan_ChiTietID = ttct.ID
left join NH_TT_ThanhToan tt on ttct.iID_DeNghiThanhToanID = tt.ID
where (@iDPheDuyetQuyetToan IS NULL OR qtdact.ID = @iDPheDuyetQuyetToan)
  order by qtda.iID_DonViID
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_pheduyet_quyettoandaht_index]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_nh_pheduyet_quyettoandaht_index]
	-- Add the parameters for the stored procedure here
	@YearOfWork int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
	pdqtdaht.ID as ID,
	pdqtdaht.bIsXoa as BIsXoa,
	pdqtdaht.dNgayPheDuyet as DNgayPheDuyet,
	pdqtdaht.dNgaySua as DNgaySua,
	pdqtdaht.dNgayTao as DNgayTao,
	pdqtdaht.dNgayXoa as DNgayXoa,
	pdqtdaht.iID_DonViID as IIDDonViId,
	pdqtdaht.iID_MaDonVi as IIDMaDonVi,
	pdqtdaht.iID_TiGiaID as IIDTiGiaId,
	pdqtdaht.iNamBaoCaoDen as INamBaoCaoDen,
	pdqtdaht.iNamBaoCaoTu as INamBaoCaoTu,
	pdqtdaht.sMoTa as SMoTa,
	pdqtdaht.sNguoiSua as SNguoiSua,
	pdqtdaht.sNguoiTao as SNguoiTao,
	pdqtdaht.sNguoiXoa as SNguoiXoa,
	pdqtdaht.sSoPheDuyet as SSoPheDuyet,
	dv.sTenDonVi,tg.sTenTiGia from NH_QT_PheDuyetQuyetToanDAHT pdqtdaht
	left join DonVi dv on pdqtdaht.iID_DonViID = dv.iID_DonVi 
	and pdqtdaht.iID_MaDonVi COLLATE SQL_Latin1_General_CP1_CI_AS = dv.iID_MaDonVi
	and dv.iNamLamViec = @YearOfWork
	left join NH_DM_TiGia tg on pdqtdaht.iID_TiGiaID = tg.ID

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_phuongannhapkhau_index]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_phuongannhapkhau_index]
@iLoai INT = NULL
AS
BEGIN
	SELECT
		paNhapKhau.ID AS Id,
		paNhapKhau.iID_KHTT_NhiemVuChiID AS IIdKhttNhiemVuChiId,
		paNhapKhau.iID_QDDauTuID AS IIdQddauTuId,
		paNhapKhau.iID_ChuTruongDauTuID AS IIdChuTruongDauTuId,
		paNhapKhau.iID_DuAnID AS IIdDuAnId,
		paNhapKhau.iID_DonViQuanLyID AS IIdDonViQuanLyId,
		paNhapKhau.iID_MaDonViQuanLy AS IIdMaDonViQuanLy,
		paNhapKhau.sSoQuyetDinh AS SSoQuyetDinh,
		paNhapKhau.dNgayQuyetDinh AS DNgayQuyetDinh,
		paNhapKhau.sMoTa AS SMoTa,
		paNhapKhau.iID_TiGiaID AS IIdTiGiaId,
		paNhapKhau.sMaNgoaiTeKhac AS SMaNgoaiTeKhac,
		paNhapKhau.iID_PhuongAnNhapKhauGocID AS IIdPhuongAnNhapKhauGocId,
		paNhapKhau.dNgayTao AS DNgayTao,
		paNhapKhau.sNguoiTao AS SNguoiTao,
		paNhapKhau.dNgaySua AS DNgaySua,
		paNhapKhau.sNguoiSua AS SNguoiSua,
		paNhapKhau.dNgayXoa AS DNgayXoa,
		paNhapKhau.sNguoiXoa AS SNguoiXoa,
		paNhapKhau.bIsActive AS BIsActive,
		paNhapKhau.bIsKhoa AS BIsKhoa,
		ISNULL(paNhapKhau.iLanDieuChinh, 0) AS ILanDieuChinh,
		paNhapKhau.bIsGoc AS BIsGoc,
		paNhapKhau.iID_ParentID AS IIdParentId,
		paNhapKhau.bIsXoa AS BIsXoa,
		paNhapKhau.iLoai AS ILoai,
		paNhapKhau.sLoaiSoCu AS SLoaiSoCu,
		CONCAT(donVi.iID_MaDonVi, ' - ', donVi.sTenDonVi) AS STenDonVi,
		nvChi.STenChuongTrinh,
		nvChi.iID_KHTongTheID AS IIdKhTongTheId,
		paNhapKhauParent.sSoQuyetDinh AS SDieuChinhTu
	FROM NH_HDNK_PhuongAnNhapKhau paNhapKhau
	LEFT JOIN NH_HDNK_PhuongAnNhapKhau paNhapKhauParent
	ON paNhapKhau.iID_ParentID = paNhapKhauParent.ID
	LEFT JOIN DonVi donVi
	ON paNhapKhau.iID_DonViQuanLyID = donVi.iID_DonVi AND paNhapKhau.iID_MaDonViQuanLy = donVi.iID_MaDonVi
	LEFT JOIN
		(SELECT  n.ID, n.iID_KHTongTheID, d.sTenNhiemVuChi AS STenChuongTrinh
		 FROM NH_KHTongThe_NhiemVuChi AS n 
		 INNER JOIN NH_DM_NhiemVuChi AS d 
		 ON n.iID_NhiemVuChiID = d.ID) 
	AS nvChi
	ON paNhapKhau.iID_KHTT_NhiemVuChiID = nvChi.ID
	WHERE (@iLoai IS NULL) OR (paNhapKhau.iLoai = @iLoai)
END;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_qddautu_chiphi]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_qddautu_chiphi]
@iIdQdDauTuId uniqueidentifier
AS
BEGIN
	SELECT qdnv.iID_NguonVonID as IIdNguonVonIdInt, tbl.ID as IIdChiPhiID, tbl.iID_ParentID as IIdParentID, tbl.STenChiPhi, tbl.SMaOrder , NULL as IIdGoiThauID, 
		ISNULL(tbl.FGiaTriNgoaiTeKhac, 0) as FGiaTriNgoaiTeKhac, ISNULL(tbl.FGiaTriUSD, 0) as FGiaTriUSD, 
		ISNULL(tbl.FGiaTriVND, 0) as FGiaTriVND, ISNULL(tbl.FGiaTriEUR, 0) as FGiaTriEUR,
		CAST(0 as float) as FGiaTriPheDuyetNgoaiTeKhac,
		CAST(0 as float) as FGiaTriPheDuyetUSD,
		CAST(0 as float) as FGiaTriPheDuyetVND,
		CAST(0 as float) as FGiaTriPheDuyetEUR
	FROM NH_DA_QDDauTu_ChiPhi as tbl
	LEFT JOIN NH_DA_QDDauTu_NguonVon as qdnv on qdnv.ID = tbl.iID_QDDauTu_NguonVonID
	WHERE tbl.iID_QDDauTuID = @iIdQdDauTuId
END;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_qddautu_get_duan_By_iID_DonViID]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_qddautu_get_duan_By_iID_DonViID]
@iIdKhlcntId uniqueidentifier,
@iId uniqueidentifier,
@iLoai int
AS
BEGIN
	SELECT DISTINCT da.*
	FROM NH_DA_QDDauTu as tbl
	INNER JOIN NH_DA_DuAn as da on tbl.iID_DuAnID = da.ID
	WHERE tbl.bIsActive = 1 AND da.iID_DonViQuanLyID = @iId AND da.iLoai=@iLoai

END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_qddautu_get_duan_in_khlcnhathau]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_qddautu_get_duan_in_khlcnhathau]
@iId uniqueidentifier,
@sMaDonVi nvarchar(100)
AS
BEGIN
	SELECT DISTINCT da.*
	FROM NH_DA_QDDauTu as tbl
	INNER JOIN NH_DA_DuAn as da on tbl.iID_DuAnID = da.ID
	WHERE tbl.bIsActive = 1 AND da.iID_MaDonViQuanLy = @sMaDonVi

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_qddautu_hangmuc]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_qddautu_hangmuc]
@iIdQdDauTuId uniqueidentifier
AS
BEGIN
	SELECT 
		tbl.iID_QDDauTu_ChiPhiID as IIdChiPhiID,
		tbl.ID as IIdHangMucID,
		tbl.iID_ParentID as IIdParentID,
		tbl.STenHangMuc,
		tbl.SMaHangMuc,
		tbl.SMaOrder,
		NULL as IIdGoiThauID,
		ISNULL(tbl.FGiaTriNgoaiTeKhac, 0) AS FGiaTriNgoaiTeKhac,
		ISNULL(tbl.FGiaTriUSD, 0) AS FGiaTriUSD,
		ISNULL(tbl.FGiaTriVND, 0) AS FGiaTriVND,
		ISNULL(tbl.FGiaTriEUR, 0) AS FGiaTriEUR,
		CAST(0 as float) as FGiaTriPheDuyetNgoaiTeKhac,
		CAST(0 as float) as FGiaTriPheDuyetUSD,
		CAST(0 as float) as FGiaTriPheDuyetVND,
		CAST(0 as float) as FGiaTriPheDuyetEUR
	FROM NH_DA_QDDauTu_ChiPhi as cp
	INNER JOIN NH_DA_QDDauTu_HangMuc as tbl on cp.ID = tbl.iID_QDDauTu_ChiPhiID
	INNER JOIN NH_DA_QDDauTu_NguonVon as nguonvon on nguonvon.ID = cp.iID_QDDauTu_NguonVonID
	WHERE nguonvon.iID_QDDauTuID = @iIdQdDauTuId
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_qddautu_index]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_nh_qddautu_index]
	@YearOfWork int,
	@iLoai int
AS BEGIN
	WITH SoLieuDieuChinh AS (
		SELECT 
			ct.ID,
			ct.iID_ParentId
		FROM  NH_DA_QDDauTu ct 
		WHERE ct.iID_ParentId IS NOT NULL 
		UNION ALL 
		SELECT 
			ct.ID,
			ct.iID_ParentId
		FROM NH_DA_QDDauTu ct 
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
			quyetDinhTuNguonVon.iID_QDDauTuID AS iID_QDDauTuID, 
			SUM(quyetDinhTuNguonVon.fGiaTriNgoaiTeKhac) AS fGiaTriNgoaiTeKhac,
			SUM(quyetDinhTuNguonVon.fGiaTriUSD) AS fGiaTriUSD,
			SUM(quyetDinhTuNguonVon.fGiaTriVND) AS fGiaTriVND,
			SUM(quyetDinhTuNguonVon.fGiaTriEUR) AS fGiaTriEUR
		FROM NH_DA_QDDauTu_NguonVon quyetDinhTuNguonVon
		GROUP BY 
			quyetDinhTuNguonVon.iID_QDDauTuID
	)
	SELECT
		qdDauTu.ID AS Id,
		qdDauTu.iID_KHTT_NhiemVuChiID AS IIdKhttNhiemVuChiId,
		qdDauTu.iID_DuAnID AS IIdDuAnId,
		qdDauTu.iID_DonViQuanLyID AS IIdDonViQuanLyId,
		qdDauTu.iID_MaDonViQuanLy AS IIdMaDonViQuanLy,
		qdDauTu.iID_ChuTruongDauTuID AS IIdChuTruongDauTuId,
		qdDauTu.sSoQuyetDinh AS SSoQuyetDinh,
		qdDauTu.dNgayQuyetDinh AS DNgayQuyetDinh,
		qdDauTu.sMota AS SMota,
		qdDauTu.iID_ChuDauTuID AS IIdChuDauTuId,
		qdDauTu.iID_MaChuDauTu AS IIdMaChuDauTu,
		qdDauTu.sKhoiCong AS SKhoiCong,
		qdDauTu.sKetThuc AS SKetThuc,
		qdDauTu.sDiaDiem AS SDiaDiem,
		thongTinNguonVon.fGiaTriNgoaiTeKhac AS FGiaTriNgoaiTeKhac,
		thongTinNguonVon.fGiaTriUSD AS FGiaTriUsd,
		thongTinNguonVon.fGiaTriVND AS FGiaTriVnd,
		thongTinNguonVon.fGiaTriEUR AS FGiaTriEur,
		qdDauTu.dNgayTao AS DNgayTao,
		qdDauTu.sNguoiTao AS sNguoiTao,
		qdDauTu.dNgaySua AS DNgaySua,
		qdDauTu.sNguoiSua AS SNguoiSua,
		qdDauTu.dNgayXoa AS DNgayXoa,
		qdDauTu.sNguoiXoa AS SNguoiXoa,
		qdDauTu.bIsActive AS BIsActive,
		qdDauTu.bIsGoc AS BIsGoc,
		qdDauTu.bIsKhoa AS BIsKhoa,
		qdDauTu.bIsXoa AS BIsXoa,
		qdDauTu.iID_TiGiaID AS IIdTiGiaId,
		qdDauTu.sMaNgoaiTeKhac AS SMaNgoaiTeKhac,
		qdDauTu.iID_ParentID AS IIdParentId,
		CONCAT(donVi.iID_MaDonVi, ' - ', donVi.sTenDonVi) AS STenDonVi,
		CONCAT(duAn.sMaDuAn, ' - ', duAn.sTenDuAn) AS STenDuAn,
		ISNULL(soLieuDieuChinh.iSoLanDieuChinh, 0) AS ILanDieuChinh,
		(SELECT COUNT(Id) FROM Attachment WHERE ModuleType = 405 AND ObjectId = qdDauTu.ID) AS TotalFiles,
		qdDauTuParent.sSoQuyetDinh AS SDieuChinhTu
	FROM NH_DA_QDDauTu qdDauTu
	LEFT JOIN NH_DA_QDDauTu qdDauTuParent
		ON qdDauTu.iID_ParentID = qdDauTuParent.ID
	LEFT JOIN ThongTinNguonVon thongTinNguonVon
		ON qdDauTu.ID = thongTinNguonVon.iID_QDDauTuID
	INNER JOIN donVi 
		ON donVi.iID_DonVi = qdDauTu.iID_DonViQuanLyID
	INNER JOIN NH_DA_DuAn duAn 
		ON duAn.ID = qdDauTu.iID_DuAnID
	LEFT JOIN SoLanDieuChinh soLieuDieuChinh
		ON soLieuDieuChinh.ID = qdDauTu.ID
	WHERE qdDauTu.iLoai = @iLoai
	ORDER BY qdDauTu.dNgayQuyetDinh DESC, qdDauTu.sSoQuyetDinh DESC
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_qddautu_nguonvon]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_qddautu_nguonvon]
@iIdQdDauTuId uniqueidentifier
AS
BEGIN
	SELECT tbl.iID_NguonVonID as IIdNguonVonID, nv.sTen as STenNguonVon, NULL as IIdGoiThauID,
		ISNULL(tbl.FGiaTriNgoaiTeKhac, 0) as FGiaTriNgoaiTeKhac, ISNULL(tbl.FGiaTriUSD, 0) FGiaTriUSD, ISNULL(tbl.FGiaTriVND, 0) FGiaTriVND, ISNULL(tbl.FGiaTriEUR, 0) FGiaTriEUR,
		CAST(0 as float) as FGiaTriPheDuyetNgoaiTeKhac,
		CAST(0 as float) as FGiaTriPheDuyetUSD,
		CAST(0 as float) as FGiaTriPheDuyetVND,
		CAST(0 as float) as FGiaTriPheDuyetEUR
	FROM NH_DA_QDDauTu_NguonVon as tbl
	INNER JOIN NguonNganSach as nv on tbl.iID_NguonVonID = nv.iID_MaNguonNganSach
	WHERE tbl.iID_QDDauTuID = @iIdQdDauTuId
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_qt_taisan_thongketaisan]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_qt_taisan_thongketaisan]
AS
BEGIN 
	SET NOCOUNT ON;
	SELECT DISTINCT iID_MaDonViID, iID_LoaiTaiSanID, iLoaiTaiSan, SUM(fSoLuong) AS fSoLuong
	INTO #temp1
	FROM Nh_qt_taisan
	GROUP BY iID_MaDonViID, iID_LoaiTaiSanID, iLoaiTaiSan;

	SELECT DISTINCT iID_MaDonViID, iID_LoaiTaiSanID, iTrangThai, SUM(fSoLuong) AS fSoLuong
	INTO #temp2
	FROM Nh_qt_taisan
	GROUP BY iID_MaDonViID, iID_LoaiTaiSanID, iTrangThai;

	SELECT DISTINCT iID_MaDonViID, iID_LoaiTaiSanID, iTinhTrangSuDung, SUM(fSoLuong) AS fSoLuong
	INTO #temp3
	FROM Nh_qt_taisan
	GROUP BY iID_MaDonViID, iID_LoaiTaiSanID, iTinhTrangSuDung;

	SELECT DISTINCT CONCAT(dv.iID_MaDonVi, ' - ', dv.STenDonVi) AS STenDonVi, lts.sMaLoaiTaiSan AS SMaTaiSan, lts.sTenLoaiTaiSan AS STenTaiSan,
		ts.iID_MaDonViID AS IIdMaDonViId, ts.iID_LoaiTaiSanID AS IIdLoaiTaiSan,
		tb1.fTaiSan1 AS FTaiSan1, tb1.fTaiSan2 AS FTaiSan2,
		tb2.fTrangThai1 AS FTrangThai1, tb2.fTrangThai2 AS FTrangThai2, tb2.fTrangThai3 AS FTrangThai3,
		tb3.fTinhTrangSuDung1 AS FTinhTrangSuDung1, tb3.fTinhTrangSuDung2 AS FTinhTrangSuDung2, tb3.fTinhTrangSuDung3 AS FTinhTrangSuDung3
	FROM Nh_qt_taisan ts
	LEFT JOIN DonVi dv on ts.iID_MaDonViID = dv.iID_DonVi
	LEFT JOIN NH_DM_LoaiTaiSan lts on ts.iID_LoaiTaiSanID = lts.ID
	INNER JOIN (
		SELECT iID_MaDonViID,iID_LoaiTaiSanID, [1] AS fTaiSan1, [2] AS fTaiSan2 FROM #temp1
		pivot(SUM(fSoLuong) FOR iLoaiTaiSan in ([1], [2])) AS pv1
	) AS tb1 ON ts.iID_MaDonViID = tb1.iID_MaDonViID AND ts.iID_LoaiTaiSanID = tb1.iID_LoaiTaiSanID
	INNER JOIN (
		SELECT iID_MaDonViID,iID_LoaiTaiSanID, [1] AS fTrangThai1, [2] AS fTrangThai2, [3] AS fTrangThai3 FROM #temp2
		pivot(SUM(fSoLuong) FOR iTrangThai in ([1], [2], [3])) AS pv2
	) AS tb2 ON ts.iID_MaDonViID = tb2.iID_MaDonViID AND ts.iID_LoaiTaiSanID = tb2.iID_LoaiTaiSanID
	INNER JOIN (
		SELECT iID_MaDonViID,iID_LoaiTaiSanID, [1] AS fTinhTrangSuDung1, [2] AS fTinhTrangSuDung2, [3] AS fTinhTrangSuDung3 FROM #temp3
		pivot(SUM(fSoLuong) FOR iTinhTrangSuDung in ([1], [2], [3])) AS pv3
	) AS tb3 ON ts.iID_MaDonViID = tb3.iID_MaDonViID AND ts.iID_LoaiTaiSanID = tb3.iID_LoaiTaiSanID
	WHERE ts.iID_ChungTuTaiSanID IS NOT NULL
	ORDER BY STenDonVi

	DROP TABLE #temp1;
	DROP TABLE #temp2;
	DROP TABLE #temp3;
END;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_qt_thong_tri_quyet_toan_index]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_qt_thong_tri_quyet_toan_index]
AS
BEGIN
	SELECT
		TTQT.ID AS ID,
		TTQT.sSoThongTri,
		TTQT.dNgayLap,
		TTQT.iID_KHTT_NhiemVuChiID,
		TTQT.iID_DonViID,
		TTQT.iID_MaDonVi,
		TTQT.iNamThongTri,
		TTQT.iLoaiThongTri,
		TTQT.iLoaiNoiDungChi,
		TTQT.fThongTri_USD,
		TTQT.fThongTri_VND,
		DM_NVC.sTenNhiemVuChi,
		CONCAT(DV.iID_MaDonVi, IIF(DV.sTenDonVi IS NULL OR DV.sTenDonVi = '', '', CONCAT(' - ', DV.sTenDonVi))) AS sTenDonVi,
		IIF(TTQT.iLoaiThongTri = 2, N'Thông tri giảm quyết toán', N'Thông tri quyết toán') AS sLoaiThongTri,
		IIF(TTQT.iLoaiNoiDungChi = 2, N'Chi bằng nội tệ', N'Chi bằng ngoại tệ') AS sLoaiNoiDungChi
	FROM NH_QT_ThongTriQuyetToan TTQT
	LEFT JOIN NH_KHTongThe_NhiemVuChi NVC ON TTQT.iID_KHTT_NhiemVuChiID = NVC.ID
	LEFT JOIN NH_DM_NhiemVuChi DM_NVC ON NVC.iID_NhiemVuChiID = DM_NVC.ID
	LEFT JOIN DonVi DV ON TTQT.iID_MaDonVi = DV.iID_MaDonVi AND TTQT.iNamThongTri = DV.iNamLamViec
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_quanli_giao_duan_index]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_quanli_giao_duan_index] 
AS
BEGIN
	WITH SoLieuDieuChinh AS 
	 (
		SELECT 
			pbvcp.Id , pbvcp.iID_ParentId
		FROM 
			VDT_KHV_PhanBoVon_ChiPhi pbvcp 
		WHERE 
			pbvcp.iID_ParentId is not null

		UNION ALL

		SELECT 
			pbvcp.Id , pbvcp.iID_ParentId
		FROM 
			VDT_KHV_PhanBoVon_ChiPhi pbvcp JOIN SoLieuDieuChinh ctpr ON pbvcp.iID_ParentId = ctpr.Id 
		WHERE pbvcp.iID_ParentId is not null
	  ),SoLanDieuChinh AS (
		   SELECT
			sdc.Id,sdc.iID_ParentId,  COUNT(sdc.Id) AS iSoLanDieuChinh
		  FROM 
			SoLieuDieuChinh sdc
		  GROUP BY sdc.iID_ParentId,sdc.Id
	  )
	select pbvcp.Id as Id, 
		pbvcp.sSoQuyetDinh as SSoQuyetDinh,
		pbvcp.dNgayQuyetDinh as DNgayQuyetDinh,
		pbvcp.iID_DuAnID as IIdDuAnId,
		pbvcp.iNamKeHoach as INamKeHoach,
		pbvcp.iID_LoaiNguonVonID as IIdLoaiNguonVonId,
		pbvcp.iID_DonViID as IIdDonViId,
		pbvcp.iID_MaDonVi as IIdMaDonVi,
		pbvcp.sLoaiDieuChinh as SLoaiDieuChinh,
		pbvcp.iID_ParentId as IIdParentId,
		pbvcp.bActive as BActive,
		pbvcp.bIsGoc as BIsGoc,
		pbvcp.fGiaTriDuocDuyet as FGiaTriDuocDuyet,
		pbvcp.iLoai as ILoai,
		pbvcp.iID_PhanBoGoc_ChiPhiID as IIdPhanBoGocChiPhiId,
		isnull(tbl.iSoLanDieuChinh,0) AS ILanDieuChinh ,
		pbvcp.bKhoa as BKhoa,
		dv.sTenDonVi as STenDonVi,
		nns.sTen as STenNguonVon,
		pbvcp.sUserCreate,
		pbvcp.dDateCreate,
		pbvcp.sUserUpdate,
		pbvcp.dDateUpdate,
		pbvcp.sUserDelete,
		pbvcp.dDateDelete,
		case
			when tbl.iID_ParentId is null then ''
			else (select pbvcp.sSoQuyetDinh from VDT_KHV_PhanBoVon_ChiPhi pbvcp where pbvcp.Id = tbl.iID_ParentId)
		end DieuChinhTu
	from VDT_KHV_PhanBoVon_ChiPhi pbvcp
	left join DonVi dv on pbvcp.iID_DonViID = dv.iID_DonVi  
	left join NguonNganSach nns on pbvcp.iID_LoaiNguonVonID = nns.iID_MaNguonNganSach 
	left join SoLanDieuChinh tbl ON tbl.Id = pbvcp.Id
END;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_quyettoan_niendo_detail_fetch_data]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TungNH
-- Create date: 17/05/2022
-- Description:	Lấy dữ liệu quyết toán chi tiết
-- =============================================
CREATE PROCEDURE [dbo].[sp_nh_quyettoan_niendo_detail_fetch_data]
	@QuyetToanNienDoId NVARCHAR(2000),
	@HopDongId NVARCHAR(2000)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @YearReport int;

	SELECT @YearReport = iNamKeHoach FROM NH_QT_QuyetToanNienDo WHERE ID = @QuyetToanNienDoId;

	WITH SoLieuKPDuocDuyetCacNamTruoc AS (
		SELECT
			hopDong.ID AS iID_HopDongID,
			SUM(chiTiet.fDeNghiQTNamNay_USD) AS fQTKinhPhiDuyetCacNamTruoc_USD,
			SUM(chiTiet.fDeNghiQTNamNay_VND) AS fQTKinhPhiDuyetCacNamTruoc_VND
		FROM NH_QT_QuyetToanNienDo_ChiTiet chiTiet
		INNER JOIN NH_QT_QuyetToanNienDo qtNienDo
			ON chiTiet.iID_QuyetToanNienDoID = qtNienDo.ID
		INNER JOIN NH_DA_HopDong hopDong
			ON chiTiet.iID_HopDongID = hopDong.ID
		INNER JOIN NH_KHTongThe_NhiemVuChi khtkNhiemVuChi
			ON hopDong.iID_KHTongThe_NhiemVuChiID = khtkNhiemVuChi.ID
		INNER JOIN NH_KHTongThe khth
			ON khtkNhiemVuChi.iID_KHTongTheID = khth.ID
		WHERE
			khth.iGiaiDoanTu <= qtNienDo.iNamKeHoach
			AND qtNienDo.iNamKeHoach <= khth.iGiaiDoanDen
			AND qtNienDo.iNamKeHoach < @YearReport
		GROUP BY hopDong.ID
	), SoLieuDeNghiThanhToan AS (
		SELECT
			iID_HopDongID,
			SUM (
				CASE
					WHEN iCoQuanThanhToan = 1 AND iNamNganSach = 3 AND (iLoaiDeNghi = 2 OR iLoaiDeNghi = 3) THEN fDeNghiCapKyNay_USD
					WHEN iCoQuanThanhToan = 2 AND iNamNganSach = 3 AND iLoaiDeNghi = 1 THEN fDeNghiCapKyNay_USD
					ELSE 0
				END
			) AS fQTKinhPhiDuocCap_NamTruocChuyenSang_USD,
			SUM (
				CASE
					WHEN iCoQuanThanhToan = 1 AND iNamNganSach = 3 AND (iLoaiDeNghi = 2 OR iLoaiDeNghi = 3) THEN fDeNghiCapKyNay_VND
					WHEN iCoQuanThanhToan = 2 AND iNamNganSach = 3 AND iLoaiDeNghi = 1 THEN fDeNghiCapKyNay_VND
					ELSE 0
				END
			) AS fQTKinhPhiDuocCap_NamTruocChuyenSang_VND,
			SUM (
				CASE
					WHEN iCoQuanThanhToan = 1 AND iNamNganSach = 2 AND (iLoaiDeNghi = 2 OR iLoaiDeNghi = 3) THEN fDeNghiCapKyNay_USD
					WHEN iCoQuanThanhToan = 2 AND iNamNganSach = 2 AND iLoaiDeNghi = 1 THEN fDeNghiCapKyNay_USD
					ELSE 0
				END
			) AS fQTKinhPhiDuocCap_NamNay_USD,
			SUM (
				CASE
					WHEN iCoQuanThanhToan = 1 AND iNamNganSach = 2 AND (iLoaiDeNghi = 2 OR iLoaiDeNghi = 3) THEN fDeNghiCapKyNay_VND
					WHEN iCoQuanThanhToan = 2 AND iNamNganSach = 2 AND iLoaiDeNghi = 1 THEN fDeNghiCapKyNay_VND
					ELSE 0
				END
			) AS fQTKinhPhiDuocCap_NamNay_VND
		FROM NH_TT_ThanhToan thanhToan
		INNER JOIN NH_TT_ThanhToan_ChiTiet thanhToanChiTiet
			ON thanhToan.ID = thanhToanChiTiet.iID_DeNghiThanhToanID
		GROUP BY iID_HopDongID
	)

    SELECT
		hopDong.Id AS IIdHopDongId,
		hopDong.fGiaTriUSD AS FHopDongUsd,
		hopDong.fGiaTriVND AS FHopDongVnd,
		soLieuCacNamTruoc.fQTKinhPhiDuyetCacNamTruoc_USD AS FQtKinhPhiDuyetCacNamTruocUsd,
		soLieuCacNamTruoc.fQTKinhPhiDuyetCacNamTruoc_VND AS FQtKinhPhiDuyetCacNamTruocVnd,
		soLieuThanhToan.fQTKinhPhiDuocCap_NamTruocChuyenSang_USD AS FQtKinhPhiDuocCapNamTruocChuyenSangUsd,
		soLieuThanhToan.fQTKinhPhiDuocCap_NamTruocChuyenSang_VND AS FQtKinhPhiDuocCapNamTruocChuyenSangVnd,
		soLieuThanhToan.fQTKinhPhiDuocCap_NamNay_USD AS FQtKinhPhiDuocCapNamNayUsd,
		soLieuThanhToan.fQTKinhPhiDuocCap_NamNay_VND AS FQtKinhPhiDuocCapNamNayVnd
	FROM NH_DA_HopDong hopDong
	LEFT JOIN SoLieuKPDuocDuyetCacNamTruoc soLieuCacNamTruoc
		ON hopDong.Id = soLieuCacNamTruoc.iID_HopDongID
	LEFT JOIN SoLieuDeNghiThanhToan soLieuThanhToan
		ON hopDong.Id = soLieuThanhToan.iID_HopDongID
	WHERE hopDong.Id = @HopDongId
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_quyettoan_niendo_index]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TungNH
-- Create date: 07/05/2022
-- Description:	Lấy danh sách hiển thị đê nghị quyết toán niên độ

-- Last update: 04/10/2022
-- Description: Join lại bảng đơn vị (join theo id + mã thay vì năm + mã)
-- =============================================
CREATE PROCEDURE [dbo].[sp_nh_quyettoan_niendo_index]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
		qtNienDo.ID					AS Id,
		qtNienDo.iID_ParentID		AS IIdParentId,
		qtNienDo.iID_GocID			AS IIdGocId,
		qtNienDo.sSoDeNghi			AS SSoDeNghi,
		qtNienDo.dNgayDeNghi		AS DNgayDeNghi,
		qtNienDo.iNamKeHoach		AS INamKeHoach,
		donVi.iID_DonVi		AS IIdDonViId,
		qtNienDo.iID_MaDonVi		AS IIdMaDonVi,
		qtNienDo.iID_NguonVonID		AS IIdNguonVonId,
		qtNienDo.iLoaiThanhToan		AS ILoaiThanhToan,
		qtNienDo.iLoaiQuyetToan		AS ILoaiQuyetToan,
		qtNienDo.iCoQuanThanhToan	AS ICoQuanThanhToan,
		qtNienDo.iID_TiGiaID		AS IIdTiGiaId,
		qtNienDo.sMaNgoaiTeKhac		AS SMaNgoaiTeKhac,
		qtNienDo.sMoTa				AS SMoTa,
		qtNienDo.dNgayTao			AS DNgayTao,
		qtNienDo.sNguoiTao			AS SNguoiTao,
		qtNienDo.dNgaySua			AS DNgaySua,
		qtNienDo.sNguoiSua			AS SNguoiSua,
		qtNienDo.dNgayXoa			AS DNgayXoa,
		qtNienDo.sNguoiXoa			AS SNguoiXoa,
		qtNienDo.bIsActive			AS BIsActive,
		qtNienDo.bIsGoc				AS BIsGoc,
		qtNienDo.bIsKhoa			AS BIsKhoa,
		qtNienDo.iLanDieuChinh		AS ILanDieuChinh,
		qtNienDo.bIsXoa				AS BIsXoa,
		donVi.sTenDonVi				AS STenDonVi,
		nguonNganSach.sTen			AS STenNguonVon,
		qtNienDo.iID_TongHopID 		AS iID_TongHopID,
		qtNienDo.sTongHopChildID 	AS sTongHopChildID,
		CASE
			WHEN ILoaiThanhToan = 1 THEN N'Cấp kinh phí'
			WHEN ILoaiThanhToan = 2 THEN N'Tạm ứng'
			WHEN ILoaiThanhToan = 3 THEN N'Thanh toán'
			ELSE ''
		END							AS SLoaiThanhToan,
		CASE
			WHEN ILoaiQuyetToan = 1 THEN N'Thanh toán theo dự án'
			WHEN ILoaiQuyetToan = 2 THEN N'Thanh toán theo hợp đồng'
			ELSE ''
		END							AS SLoaiQuyetToan,
		CASE
			WHEN ICoQuanThanhToan = 1 THEN N'Kho bạc'
			WHEN ICoQuanThanhToan = 2 THEN N'Cơ quan tài chính Bộ Quốc phòng'
			ELSE ''
		END							AS SCoQuanThanhToan
	FROM NH_QT_QuyetToanNienDo qtNienDo
	LEFT JOIN DonVi donVi
		ON qtNienDo.iID_MaDonVi = donVi.iID_MaDonVi AND donVi.iID_DonVi = qtNienDo.iID_DonViID
	LEFT JOIN NguonNganSach nguonNganSach
		ON qtNienDo.iID_NguonVonID = nguonNganSach.iID_MaNguonNganSach
	WHERE qtNienDo.sTongHopChildID IS NULL
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_quyettoan_niendo_tonghop_index]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: LinhND
-- Create date: 16/09/2022
-- Description:	Lấy danh sách hiển thị đê nghị quyết toán niên độ tổng hợp

-- Last update: 04/10/2022
-- Description: Join lại bảng đơn vị theo mã + id thay vì mã + năm làm việc.
-- =============================================

CREATE PROCEDURE [dbo].[sp_nh_quyettoan_niendo_tonghop_index]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
		qtNienDo.ID					AS Id,
		qtNienDo.iID_ParentID		AS IIdParentId,
		qtNienDo.iID_GocID			AS IIdGocId,
		qtNienDo.sSoDeNghi			AS SSoDeNghi,
		qtNienDo.dNgayDeNghi		AS DNgayDeNghi,
		qtNienDo.iNamKeHoach		AS INamKeHoach,
		qtNienDo.iID_DonViID		AS IIdDonViId,
		qtNienDo.iID_MaDonVi		AS IIdMaDonVi,
		qtNienDo.iID_NguonVonID		AS IIdNguonVonId,
		qtNienDo.iLoaiThanhToan		AS ILoaiThanhToan,
		qtNienDo.iCoQuanThanhToan	AS ICoQuanThanhToan,
		qtNienDo.iLoaiQuyetToan		AS ILoaiQuyetToan,
		qtNienDo.iID_TiGiaID		AS IIdTiGiaId,
		qtNienDo.sMaNgoaiTeKhac		AS SMaNgoaiTeKhac,
		qtNienDo.sMoTa				AS SMoTa,
		qtNienDo.dNgayTao			AS DNgayTao,
		qtNienDo.sNguoiTao			AS SNguoiTao,
		qtNienDo.dNgaySua			AS DNgaySua,
		qtNienDo.sNguoiSua			AS SNguoiSua,
		qtNienDo.dNgayXoa			AS DNgayXoa,
		qtNienDo.sNguoiXoa			AS SNguoiXoa,
		qtNienDo.bIsActive			AS BIsActive,
		qtNienDo.bIsGoc				AS BIsGoc,
		qtNienDo.bIsKhoa			AS BIsKhoa,
		qtNienDo.iLanDieuChinh		AS ILanDieuChinh,
		qtNienDo.bIsXoa				AS BIsXoa,
		donVi.sTenDonVi				AS STenDonVi,
		nguonNganSach.sTen			AS STenNguonVon,
		qtNienDo.iID_TongHopID 		AS iID_TongHopID,
		qtNienDo.sTongHopChildID 	AS sTongHopChildID,
		IIF(qtNienDo.sTongHopChildID IS NOT NULL, CAST(1 AS BIT), CAST(0 AS BIT)) AS HasChildren,
		CAST(0 AS BIT) 				AS IsShowChildren,
		CASE
			WHEN ILoaiThanhToan = 1 THEN N'Cấp kinh phí'
			WHEN ILoaiThanhToan = 2 THEN N'Tạm ứng'
			WHEN ILoaiThanhToan = 3 THEN N'Thanh toán'
			ELSE ''
		END							AS SLoaiThanhToan,
		CASE
			WHEN ILoaiThanhToan = 1 THEN N'Thanh toán theo dự án'
			WHEN ILoaiThanhToan = 2 THEN N'Thanh toán theo hợp đồng'
			ELSE ''
		END							AS SLoaiQuyetToan,
		CASE
			WHEN ICoQuanThanhToan = 1 THEN N'Kho bạc'
			WHEN ICoQuanThanhToan = 2 THEN N'Cơ quan tài chính Bộ Quốc phòng'
			ELSE ''
		END							AS SCoQuanThanhToan
	FROM NH_QT_QuyetToanNienDo qtNienDo
	LEFT JOIN DonVi donVi ON qtNienDo.iID_MaDonVi = donVi.iID_MaDonVi AND donVi.iID_DonVi = qtNienDo.iID_DonViID
	LEFT JOIN NguonNganSach nguonNganSach ON qtNienDo.iID_NguonVonID = nguonNganSach.iID_MaNguonNganSach
	WHERE qtNienDo.sTongHopChildID IS NOT NULL OR qtNienDo.iID_TongHopID IS NOT NULL
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_report_thongtri_capphat]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_report_thongtri_capphat]
	@idThongTri uniqueidentifier
AS
BEGIN

	SELECT  NS_MucLucNganSach.sM,
		NS_MucLucNganSach.sTM,
		NS_MucLucNganSach.sTTM,
		NS_MucLucNganSach.sTNG,
		NS_MucLucNganSach.sMoTa,
		NH_TT_ThanhToan_ChiTiet.fPheDuyetCapKyNay_USD AS USD,
		NH_TT_ThanhToan_ChiTiet.fPheDuyetCapKyNay_VND AS VND,
		NH_TT_ThanhToan_ChiTiet.fPheDuyetCapKyNay_EUR AS EUR,
		NH_TT_ThanhToan_ChiTiet.fPheDuyetCapKyNay_NgoaiTeKhac AS NgoaiTeKhac
	FROM NH_TT_ThanhToan_ChiTiet INNER JOIN NS_MucLucNganSach
	ON NH_TT_ThanhToan_ChiTiet.iID_MucLucNganSachID = NS_MucLucNganSach.iID
		INNER JOIN NH_TT_ThanhToan
	ON NH_TT_ThanhToan_ChiTiet.iID_DeNghiThanhToanID = NH_TT_ThanhToan.ID
		INNER JOIN NH_TT_ThongTriCapPhat_ChiTiet 
	ON NH_TT_ThanhToan.ID = NH_TT_ThongTriCapPhat_ChiTiet.iID_PheDuyetThanhToanID
		WHERE NH_TT_ThongTriCapPhat_ChiTiet.iID_ThongTriCapPhatID = @idThongTri

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_rpt_quyettoan_niendo_nam]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TungNH
-- Create date: 09/05/2022
-- Description:	Báo cáo quyết toán niên độ
-- =============================================
CREATE PROCEDURE [dbo].[sp_nh_rpt_quyettoan_niendo_nam]
	@Id NVARCHAR(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @YearReport int;
	SELECT @YearReport = iNamKeHoach FROM NH_QT_QuyetToanNienDo WHERE ID = @Id;

	WITH SoLieuDeNghiThanhToan AS (
		SELECT
			iID_HopDongID,
			SUM (
				CASE
					WHEN iCoQuanThanhToan = 1 AND iNamNganSach = 3 AND (iLoaiDeNghi = 2 OR iLoaiDeNghi = 3) THEN fDeNghiCapKyNay_USD
					WHEN iCoQuanThanhToan = 2 AND iNamNganSach = 3 AND iLoaiDeNghi = 1 THEN fDeNghiCapKyNay_USD
					ELSE 0
				END
			) AS fQTKinhPhiDuocCap_NamTruocChuyenSang_USD,
			SUM (
				CASE
					WHEN iCoQuanThanhToan = 1 AND iNamNganSach = 3 AND (iLoaiDeNghi = 2 OR iLoaiDeNghi = 3) THEN fDeNghiCapKyNay_VND
					WHEN iCoQuanThanhToan = 2 AND iNamNganSach = 3 AND iLoaiDeNghi = 1 THEN fDeNghiCapKyNay_VND
					ELSE 0
				END
			) AS fQTKinhPhiDuocCap_NamTruocChuyenSang_VND,
			SUM (
				CASE
					WHEN iCoQuanThanhToan = 1 AND iNamNganSach = 2 AND (iLoaiDeNghi = 2 OR iLoaiDeNghi = 3) THEN fDeNghiCapKyNay_USD
					WHEN iCoQuanThanhToan = 2 AND iNamNganSach = 2 AND iLoaiDeNghi = 1 THEN fDeNghiCapKyNay_USD
					ELSE 0
				END
			) AS fQTKinhPhiDuocCap_NamNay_USD,
			SUM (
				CASE
					WHEN iCoQuanThanhToan = 1 AND iNamNganSach = 2 AND (iLoaiDeNghi = 2 OR iLoaiDeNghi = 3) THEN fDeNghiCapKyNay_VND
					WHEN iCoQuanThanhToan = 2 AND iNamNganSach = 2 AND iLoaiDeNghi = 1 THEN fDeNghiCapKyNay_VND
					ELSE 0
				END
			) AS fQTKinhPhiDuocCap_NamNay_VND
		FROM NH_TT_ThanhToan thanhToan
		INNER JOIN NH_TT_ThanhToan_ChiTiet thanhToanChiTiet
			ON thanhToan.ID = thanhToanChiTiet.iID_DeNghiThanhToanID
		GROUP BY iID_HopDongID
	), SoLieuKPDuocDuyetCacNamTruoc AS (
		SELECT
			hopDong.ID AS iID_HopDongID,
			SUM(chiTiet.fDeNghiQTNamNay_USD) AS fQTKinhPhiDuyetCacNamTruoc_USD,
			SUM(chiTiet.fDeNghiQTNamNay_VND) AS fQTKinhPhiDuyetCacNamTruoc_VND
		FROM NH_QT_QuyetToanNienDo_ChiTiet chiTiet
		INNER JOIN NH_QT_QuyetToanNienDo qtNienDo
			ON chiTiet.iID_QuyetToanNienDoID = qtNienDo.ID
		INNER JOIN NH_DA_HopDong hopDong
			ON chiTiet.iID_HopDongID = hopDong.ID
		INNER JOIN NH_KHTongThe_NhiemVuChi khtkNhiemVuChi
			ON hopDong.iID_KHTongThe_NhiemVuChiID = khtkNhiemVuChi.ID
		INNER JOIN NH_KHTongThe khth
			ON khtkNhiemVuChi.iID_KHTongTheID = khth.ID
		WHERE
			khth.iGiaiDoanTu <= qtNienDo.iNamKeHoach
			AND qtNienDo.iNamKeHoach <= khth.iGiaiDoanDen
			AND qtNienDo.iNamKeHoach < @YearReport
		GROUP BY hopDong.ID
	), SoLieuChiTietNamTruoc AS (
		SELECT
			qtNienDo.iNamKeHoach AS iNamKeHoach,
			chiTiet.iID_HopDongID AS iID_HopDongID,
			SUM(chiTiet.fQTKinhPhiDuocCap_NamNay_USD) AS fQTKinhPhiDuocCap_NamNay_USD,
			SUM(chiTiet.fQTKinhPhiDuocCap_NamNay_VND) AS fQTKinhPhiDuocCap_NamNay_VND
		FROM NH_QT_QuyetToanNienDo_ChiTiet chiTiet
		INNER JOIN NH_QT_QuyetToanNienDo qtNienDo
			ON qtNienDo.ID = chiTiet.iID_QuyetToanNienDoID
		GROUP BY qtNienDo.iNamKeHoach, chiTiet.iID_HopDongID
	)

    SELECT
		chiTiet.ID AS Id,
		hopDong.iLoai AS ILoai,
		mlns.sM AS M,
		mlns.sTM AS TM,
		dmNhiemVuChi.ID AS IIdNhiemVuChiId,
		dmNhiemVuChi.sTenNhiemVuChi AS sTenNhiemVuChi,
		duAn.ID AS IIdDuAnId,
		duAn.sTenDuAn AS STenDuAn,
		hopDong.sTenHopDong AS NoiDung,
		hopDong.fGiaTriUSD AS FHopDongUsd, -- 1
		hopDong.fGiaTriVND AS FHopDongVnd, -- 2
		NULL AS FKeHoachTtcpUsd, -- 3
		NULL AS FKeHoachBqpUsd, -- 4
		soLieuCacNamTruoc.fQTKinhPhiDuyetCacNamTruoc_USD AS FQtKinhPhiDuyetCacNamTruocUsd, -- 5
		soLieuCacNamTruoc.fQTKinhPhiDuyetCacNamTruoc_VND AS FQtKinhPhiDuyetCacNamTruocVnd, -- 6
		(deNghiThanhToan.fQTKinhPhiDuocCap_NamTruocChuyenSang_USD + deNghiThanhToan.fQTKinhPhiDuocCap_NamNay_USD) AS FQtKinhPhiDuocCapTongSoUsd, -- 7=9+11
		(deNghiThanhToan.fQTKinhPhiDuocCap_NamTruocChuyenSang_VND + deNghiThanhToan.fQTKinhPhiDuocCap_NamNay_VND) AS FQtKinhPhiDuocCapTongSoVnd, -- 8=10+12
		deNghiThanhToan.fQTKinhPhiDuocCap_NamTruocChuyenSang_USD AS FQtKinhPhiDuocCapNamTruocChuyenSangUsd, -- 9
		deNghiThanhToan.fQTKinhPhiDuocCap_NamTruocChuyenSang_VND AS FQtKinhPhiDuocCapNamTruocChuyenSangVnd, -- 10
		deNghiThanhToan.fQTKinhPhiDuocCap_NamNay_USD AS FQtKinhPhiDuocCapNamNayUsd, -- 11
		deNghiThanhToan.fQTKinhPhiDuocCap_NamNay_VND AS FQtKinhPhiDuocCapNamNayVnd, -- 12
		ISNULL(chiTiet.fDeNghiQTNamNay_USD, 0) AS FDeNghiQtNamNayUsd, -- 13
		ISNULL(chiTiet.fDeNghiQTNamNay_VND, 0) AS FDeNghiQtNamNayVnd, -- 14
		ISNULL(chiTiet.fDeNghiChuyenNamSau_USD, 0) AS FDeNghiChuyenNamSauUsd, -- 15
		ISNULL(chiTiet.fDeNghiChuyenNamSau_VND, 0) AS FDeNghiChuyenNamSauVnd, -- 16
		(deNghiThanhToan.fQTKinhPhiDuocCap_NamTruocChuyenSang_USD + deNghiThanhToan.fQTKinhPhiDuocCap_NamNay_USD - ISNULL(chiTiet.fDeNghiQTNamNay_USD, 0) - ISNULL(chiTiet.fDeNghiChuyenNamSau_USD, 0)) AS FThuaThieuKinhPhiTrongNamUsd, -- 17=7-13-15
		(deNghiThanhToan.fQTKinhPhiDuocCap_NamTruocChuyenSang_VND + deNghiThanhToan.fQTKinhPhiDuocCap_NamNay_VND - ISNULL(chiTiet.fDeNghiQTNamNay_VND, 0) - ISNULL(chiTiet.fDeNghiChuyenNamSau_VND, 0)) AS FThuaThieuKinhPhiTrongNamVnd, -- 18=8-14-16
		ISNULL(chiTiet.fThuaNopNSNN_USD, 0) AS FThuaNopNsnnUsd, -- 19
		ISNULL(chiTiet.fThuaNopNSNN_VND, 0) AS FThuaNopNsnnVnd, -- 20
		ISNULL(chiTiet.fLuyKeKinhPhiDuocCap_USD, 0) + chiTietNamTruoc.fQTKinhPhiDuocCap_NamNay_USD AS FLuyKeKinhPhiDuocCapUsd, -- 21
		ISNULL(chiTiet.fLuyKeKinhPhiDuocCap_VND, 0) + chiTietNamTruoc.fQTKinhPhiDuocCap_NamNay_VND AS FLuyKeKinhPhiDuocCapVnd, -- 22
		ISNULL(chiTiet.fKeHoachChuaGiaiNgan_USD, 0) AS FKeHoachChuaGiaiNganUsd -- 23=4-7
	FROM NH_QT_QuyetToanNienDo_ChiTiet chiTiet
	INNER JOIN NH_QT_QuyetToanNienDo qtNienDo
		ON qtNienDo.ID = chiTiet.iID_QuyetToanNienDoID
	LEFT JOIN SoLieuChiTietNamTruoc chiTietNamTruoc
		ON chiTiet.iID_HopDongID = chiTietNamTruoc.iID_HopDongID AND qtNienDo.iNamKeHoach - 1 = chiTietNamTruoc.iNamKeHoach
	LEFT JOIN NS_MucLucNganSach mlns
		ON chiTiet.iID_MLNS_ID = mlns.iID_MLNS AND chiTiet.iID_MucLucNganSachID = mlns.iID
	INNER JOIN NH_DA_HopDong hopDong
		ON chiTiet.iID_HopDongID = hopDong.ID
	INNER JOIN NH_KHTongThe_NhiemVuChi khtkNhiemVuChi
		ON hopDong.iID_KHTongThe_NhiemVuChiID = khtkNhiemVuChi.ID
	INNER JOIN NH_DM_NhiemVuChi dmNhiemVuChi
		ON khtkNhiemVuChi.iID_NhiemVuChiID = dmNhiemVuChi.ID
	LEFT JOIN NH_DA_DuAn duAn
		ON hopDong.iID_DuAnID = duAn.ID
	LEFT JOIN SoLieuDeNghiThanhToan deNghiThanhToan
		ON chiTiet.iID_HopDongID = deNghiThanhToan.iID_HopDongID
	LEFT JOIN SoLieuKPDuocDuyetCacNamTruoc soLieuCacNamTruoc
		ON hopDong.ID = soLieuCacNamTruoc.iID_HopDongID
	WHERE chiTiet.iID_QuyetToanNienDoID = @Id
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_rpt_quyettoan_niendo_quy]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TungNH
-- Create date: 17/05/2022
-- Description:	Báo cáo quyết toán niên độ - Quý
-- =============================================
CREATE PROCEDURE [dbo].[sp_nh_rpt_quyettoan_niendo_quy]
	@Id NVARCHAR(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @YearReport int;
	SELECT @YearReport = iNamKeHoach FROM NH_QT_QuyetToanNienDo WHERE ID = @Id;

	WITH KinhPhiGiaiNganCacNamTruoc AS (
		SELECT
			iID_HopDongID,
			SUM (
				CASE
					WHEN iLoaiDeNghi = 2 THEN fDeNghiCapKyNay_USD - fThuHoiTamUng_BangSo
					WHEN iLoaiDeNghi = 3 THEN fDeNghiCapKyNay_USD - fThuHoiTamUng_BangSo
					ELSE 0
				END
			) AS fQTKinhPhiGiaiNgan_CacNamTruoc_USD,
			SUM (
				CASE
					WHEN iLoaiDeNghi = 2 THEN fDeNghiCapKyNay_USD - fThuHoiTamUng_BangSo
					WHEN iLoaiDeNghi = 3 THEN fDeNghiCapKyNay_USD - fThuHoiTamUng_BangSo
					ELSE 0
				END
			) AS fQTKinhPhiGiaiNgan_CacNamTruoc_VND
		FROM NH_TT_ThanhToan thanhToan
		INNER JOIN NH_TT_ThanhToan_ChiTiet thanhToanChiTiet
			ON thanhToan.ID = thanhToanChiTiet.iID_DeNghiThanhToanID
		INNER JOIN NH_DA_HopDong hopDong
			ON thanhToan.iID_HopDongID = hopDong.ID
		INNER JOIN NH_KHTongThe_NhiemVuChi khtkNhiemVuChi
			ON hopDong.iID_KHTongThe_NhiemVuChiID = khtkNhiemVuChi.ID
		INNER JOIN NH_KHTongThe khth
			ON khtkNhiemVuChi.iID_KHTongTheID = khth.ID
		WHERE
			thanhToan.iNamKeHoach < @YearReport
			AND khth.iGiaiDoanTu <= thanhToan.iNamKeHoach
			AND thanhToan.iNamKeHoach <= khth.iGiaiDoanDen
		GROUP BY iID_HopDongID
	), KinhPhiThanhToanDenHetQuyNay AS (
		SELECT
			iID_HopDongID,
			SUM (
				CASE
					WHEN iLoaiDeNghi = 2 THEN fDeNghiCapKyNay_USD - fThuHoiTamUng_BangSo
					WHEN iLoaiDeNghi = 3 THEN fDeNghiCapKyNay_USD - fThuHoiTamUng_BangSo
					ELSE 0
				END
			) AS fQTKinhPhiGiaiNgan_DenKyNay_USD,
			SUM (
				CASE
					WHEN iLoaiDeNghi = 2 THEN fDeNghiCapKyNay_USD - fThuHoiTamUng_BangSo
					WHEN iLoaiDeNghi = 3 THEN fDeNghiCapKyNay_USD - fThuHoiTamUng_BangSo
					ELSE 0
				END
			) AS fQTKinhPhiGiaiNgan_DenKyNay_VND,
			SUM (
				CASE
					WHEN iCoQuanThanhToan = 1 AND (iLoaiDeNghi = 2 OR iLoaiDeNghi = 3) THEN fDeNghiCapKyNay_USD
					WHEN iCoQuanThanhToan = 2 AND iLoaiDeNghi = 1 THEN fDeNghiCapKyNay_USD
					ELSE 0
				END
			) AS fQTKinhPhiDuocCap_DenKyNay_USD,
			SUM (
				CASE
					WHEN iCoQuanThanhToan = 1 AND (iLoaiDeNghi = 2 OR iLoaiDeNghi = 3) THEN fDeNghiCapKyNay_VND
					WHEN iCoQuanThanhToan = 2 AND iLoaiDeNghi = 1 THEN fDeNghiCapKyNay_VND
					ELSE 0
				END
			) AS fQTKinhPhiDuocCap_DenKyNay_VND
		FROM NH_TT_ThanhToan thanhToan
		INNER JOIN NH_TT_ThanhToan_ChiTiet thanhToanChiTiet
			ON thanhToan.ID = thanhToanChiTiet.iID_DeNghiThanhToanID
		WHERE
			iNamKeHoach = @YearReport
			--AND thanhToan.dNgayPheDuyet < Hết quý này
		GROUP BY iID_HopDongID
	), SoLieuChiTietNamTruoc AS (
		SELECT
			qtNienDo.iNamKeHoach AS iNamKeHoach,
			chiTiet.iID_HopDongID AS iID_HopDongID,
			SUM(chiTiet.fQTKinhPhiDuocCap_NamNay_USD) AS fQTKinhPhiDuocCap_NamNay_USD,
			SUM(chiTiet.fQTKinhPhiDuocCap_NamNay_VND) AS fQTKinhPhiDuocCap_NamNay_VND,
			SUM(chiTiet.fLuyKeKinhPhiDuocCap_USD) AS fLuyKeKinhPhiDuocCap_USD,
			SUM(chiTiet.fLuyKeKinhPhiDuocCap_VND) AS fLuyKeKinhPhiDuocCap_VND
		FROM NH_QT_QuyetToanNienDo_ChiTiet chiTiet
		INNER JOIN NH_QT_QuyetToanNienDo qtNienDo
			ON qtNienDo.ID = chiTiet.iID_QuyetToanNienDoID
		GROUP BY qtNienDo.iNamKeHoach, chiTiet.iID_HopDongID
	)

    SELECT
		chiTiet.ID AS Id,
		hopDong.iLoai AS ILoai,
		mlns.sM AS M,
		mlns.sTM AS TM,
		dmNhiemVuChi.ID AS IIdNhiemVuChiId,
		dmNhiemVuChi.sTenNhiemVuChi AS sTenNhiemVuChi,
		duAn.ID AS IIdDuAnId,
		duAn.sTenDuAn AS STenDuAn,
		hopDong.sTenHopDong AS NoiDung,
		hopDong.fGiaTriUSD AS FHopDongUsd, -- 1
		hopDong.fGiaTriVND AS FHopDongVnd, -- 2
		0 AS FKeHoachTtcpTongSoUsd, -- 3
		0 AS FKeHoachTtcpGianDoanUsd, -- 4
		0 AS FKeHoachBqpTongSoUsd, -- 5
		0 AS FKeHoachBqpGiaiDoanUsd, -- 6
		ISNULL(chiTietNamTruoc.fLuyKeKinhPhiDuocCap_USD, 0) + ISNULL(deNghiThanhToan.fQTKinhPhiDuocCap_DenKyNay_USD, 0) AS FKinhPhiDuocCapTongSoUsd, -- 7=9+11
		ISNULL(chiTietNamTruoc.fLuyKeKinhPhiDuocCap_VND, 0) + ISNULL(deNghiThanhToan.fQTKinhPhiDuocCap_DenKyNay_VND, 0) AS FKinhPhiDuocCapTongSoVnd, -- 8=10+12
		ISNULL(chiTietNamTruoc.fLuyKeKinhPhiDuocCap_USD, 0) AS FKinhPhiDuocCapCacNamTruocUsd, -- 9
		ISNULL(chiTietNamTruoc.fLuyKeKinhPhiDuocCap_VND, 0) AS FKinhPhiDuocCapCacNamTruocVnd, -- 10
		deNghiThanhToan.fQTKinhPhiDuocCap_DenKyNay_USD AS FKinhPhiDuocCapDenQuyNayUsd, -- 11
		deNghiThanhToan.fQTKinhPhiDuocCap_DenKyNay_VND AS FKinhPhiDuocCapDenQuyNayVnd, -- 12
		(deNghiThanhToanCacNamTruoc.fQTKinhPhiGiaiNgan_CacNamTruoc_USD + deNghiThanhToan.fQTKinhPhiGiaiNgan_DenKyNay_USD) AS FKinhPhiGiaiNganTongSoUsd, -- 13=15+17
		(deNghiThanhToanCacNamTruoc.fQTKinhPhiGiaiNgan_CacNamTruoc_VND + deNghiThanhToan.fQTKinhPhiGiaiNgan_DenKyNay_VND) AS FKinhPhiGiaiNganTongSoVnd, -- 14=16+18
		deNghiThanhToanCacNamTruoc.fQTKinhPhiGiaiNgan_CacNamTruoc_USD AS FKinhPhiGiaiNganCacNamTruocUsd, -- 15
		deNghiThanhToanCacNamTruoc.fQTKinhPhiGiaiNgan_CacNamTruoc_VND AS FKinhPhiGiaiNganCacNamTruocVnd, -- 16
		deNghiThanhToan.fQTKinhPhiGiaiNgan_DenKyNay_USD AS FKinhPhiGiaiNganDenQuyNayUsd, -- 17
		deNghiThanhToan.fQTKinhPhiGiaiNgan_DenKyNay_VND AS FKinhPhiGiaiNganDenQuyNayVnd, -- 18
		(ISNULL(chiTietNamTruoc.fLuyKeKinhPhiDuocCap_USD, 0) + ISNULL(deNghiThanhToan.fQTKinhPhiDuocCap_DenKyNay_USD, 0)) - (ISNULL(deNghiThanhToanCacNamTruoc.fQTKinhPhiGiaiNgan_CacNamTruoc_USD, 0) + ISNULL(deNghiThanhToan.fQTKinhPhiGiaiNgan_DenKyNay_USD, 0)) AS FKinhPhiDuocCapChuaChiDenQuyNayUsd, -- 19=7-13
		(ISNULL(chiTietNamTruoc.fLuyKeKinhPhiDuocCap_VND, 0) + ISNULL(deNghiThanhToan.fQTKinhPhiDuocCap_DenKyNay_VND, 0)) - (ISNULL(deNghiThanhToanCacNamTruoc.fQTKinhPhiGiaiNgan_CacNamTruoc_VND, 0) + ISNULL(deNghiThanhToan.fQTKinhPhiGiaiNgan_DenKyNay_VND, 0)) AS FKinhPhiDuocCapChuaChiDenQuyNayVnd, -- 20=8-14
		0 AS FKeHoachChuaGiaiNgan -- 21=5-7
	FROM NH_QT_QuyetToanNienDo_ChiTiet chiTiet
	INNER JOIN NH_QT_QuyetToanNienDo qtNienDo
		ON qtNienDo.ID = chiTiet.iID_QuyetToanNienDoID
	LEFT JOIN NS_MucLucNganSach mlns
		ON chiTiet.iID_MLNS_ID = mlns.iID_MLNS AND chiTiet.iID_MucLucNganSachID = mlns.iID
	INNER JOIN NH_DA_HopDong hopDong
		ON chiTiet.iID_HopDongID = hopDong.ID
	INNER JOIN NH_KHTongThe_NhiemVuChi khtkNhiemVuChi
		ON hopDong.iID_KHTongThe_NhiemVuChiID = khtkNhiemVuChi.ID
	INNER JOIN NH_DM_NhiemVuChi dmNhiemVuChi
		ON khtkNhiemVuChi.iID_NhiemVuChiID = dmNhiemVuChi.ID
	LEFT JOIN NH_DA_DuAn duAn
		ON hopDong.iID_DuAnID = duAn.ID
	LEFT JOIN SoLieuChiTietNamTruoc chiTietNamTruoc
		ON chiTiet.iID_HopDongID = chiTietNamTruoc.iID_HopDongID AND qtNienDo.iNamKeHoach - 1 = chiTietNamTruoc.iNamKeHoach
	LEFT JOIN KinhPhiThanhToanDenHetQuyNay deNghiThanhToan
		ON chiTiet.iID_HopDongID = deNghiThanhToan.iID_HopDongID
	LEFT JOIN KinhPhiGiaiNganCacNamTruoc deNghiThanhToanCacNamTruoc
		ON chiTiet.iID_HopDongID = deNghiThanhToanCacNamTruoc.iID_HopDongID
	WHERE chiTiet.iID_QuyetToanNienDoID = @Id
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtin_goithau_index]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_nh_thongtin_goithau_index]

as begin
	select 
		goiThau.iID_GoiThauID as Id,
		goiThau.iID_GoiThauGocID as IIdGoiThauGocId,
		goiThau.iID_ParentID as IIdParentId,
		goiThau.iID_NhaThauID as IIdNhaThauId,
		goiThau.iID_CacQuyetDinhID as IIdCacQuyetDinhId,
		goiThau.iID_ParentAdjustID as IIdParentAdjustId,
		goiThau.iId_KHLCNhaThau as IIdKhlcnhaThau,
		goiThau.iID_TiGiaUSD_NgoaiTeKhacID as IIdTiGiaUSDNgoaiTeKhacId,
		goiThau.iID_TiGiaUSD_VNDID as IIdTiGiaUSDVNDId,
		goiThau.iID_TiGiaUSD_EURID as IIdTiGiaUSDEURId,
		goiThau.iID_HinhThucChonNhaThauID as IIdHinhThucChonNhaThauId,
		goiThau.iID_PhuongThucDauThauID as IIdPhuongThucDauThauId,
		goiThau.iID_LoaiHopDongID as IIdLoaiHopDongId,
		goiThau.iID_DuAnID as IIdDuAnId,
		goiThau.sSoQuyetDinh as SSoQuyetDinh,
		goiThau.dNgayQuyetDinh as DNgayQuyetDinh,
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
		goiThau.bActive as BActive,
		goiThau.iLanDieuChinh as ILanDieuChinh,
		goiThau.bIsKhoa as BIsKhoa,
		goiThau.dNgayTao as DNgayTao,
		goiThau.sNguoiTao as SNguoiTao,
		goiThau.sNguoiSua as SNguoiSua,
		goiThau.dNgaySua as DNgaySua,
		goiThau.dNgayXoa as DNgayXoa,
		goiThau.sNguoiXoa as SNguoiXoa,
		goiThau.iID_TiGiaID as IIdTiGiaId,
		goiThau.sMaNgoaiTeKhac as SMaNgoaiTeKhac,
		goiThau.bIsXoa as BIsXoa,
		NH_DM_HinhThucChonNhaThau.sTenHinhThucChonNhaThau as STenHinhThucChonNhaThau,
		NH_DM_PhuongThucChonNhaThau.sTenPhuongThucChonNhaThau as STenPhuongThucChonNhaThau,
		( SELECT COUNT ( Id ) FROM Attachment WHERE ModuleType = 405 AND ObjectId = goiThau.iID_GoiThauID ) AS TotalFiles,
		CASE
		WHEN goiThau.iID_ParentAdjustId IS NULL THEN
		'' ELSE ( SELECT TOP 1 hdpr.sMaGoiThau FROM NH_DA_GoiThau hdpr WHERE hdpr.iID_GoiThauID = goiThau.iID_ParentAdjustId ) 
		END DieuChinhTu ,
		tigia.sTenTiGia as STenTiGia,
		loaihopdong.sTenLoaiHopDong as STenLoaiHopDong
	from NH_DA_GoiThau goiThau
	left join NH_DM_HinhThucChonNhaThau 
		on NH_DM_HinhThucChonNhaThau.ID = goiThau.iID_HinhThucChonNhaThauID
	left join NH_DM_PhuongThucChonNhaThau
		on NH_DM_PhuongThucChonNhaThau.ID = goiThau.iID_PhuongThucDauThauID
	left join NH_DM_TiGia tigia
		on goiThau.iID_TiGiaID = tigia.ID
	left join NH_DM_LoaiHopDong loaihopdong
		on goiThau.iID_LoaiHopDongID = loaihopdong.iID_LoaiHopDongID;
end
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtin_hopdong_goithau_index]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_thongtin_hopdong_goithau_index]
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
	hd.iID_KeHoachDatHangID AS IIdKeHoachDatHangId,
	hd.iID_TiGiaID AS IIdTiGiaId,
	hd.iID_DuAnID AS IIdDuAnId,
	hd.iLoai AS ILoai,
	hd.iThuocMenu AS IThuocMenu,
	hd.iThoiGianThucHien AS IThoiGianThucHien,
	hd.fGiaTriHopDongUSD AS FGiaTriHopDongUSD,
	hd.fGiaTriHopDongVND AS FGiaTriHopDongVND,
	hd.fGiaTriHopDongEUR AS FGiaTriHopDongEUR,
	hd.fGiaTriHopDongNgoaiTeKhac AS FGiaTriHopDongNgoaiTeKhac,
	hd.fTiGiaNhap AS FTiGiaNhap,
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
	(CASE
		WHEN da.sTenDuAn IS NULL THEN ''
		WHEN da.sMaDuAn IS NULL THEN da.sTenDuAn
		ELSE CONCAT(da.sMaDuAn, ' - ', da.sTenDuAn)
		END) AS STenDuAn,
	CONCAT(donVi.iID_MaDonVi, ' - ', donVi.sTenDonVi) AS STenDonVi,
	hd.iID_DonViQuanLyID AS IIdDonViQuanLyId,
	hd.iID_MaDonViQuanLy AS IIdMaDonViQuanLy,
	lhd.sMaLoaiHopDong AS SMaLoaiHopDong,
	nt.sMaNhaThau AS SMaNhaThauThucHien,
	hd.sHinhThucHopDong AS SHinhThucHopDong,
	da.sMaDuAn AS SMaDuAn,
	CASE WHEN hd.iID_ParentAdjustId IS NULL THEN '' ELSE ( SELECT TOP 1 hdpr.sSoHopDong FROM NH_DA_HopDong hdpr WHERE hdpr.Id = hd.iID_ParentAdjustId) END DieuChinhTu
FROM NH_DA_HopDong hd
LEFT JOIN NH_DM_LoaiHopDong lhd on hd.iID_LoaiHopDongID = lhd.iID_LoaiHopDongID
LEFT JOIN NH_DA_DuAn da on hd.iID_DuAnID = da.ID
LEFT JOIN DonVi donVi ON hd.iID_DonViQuanLyID = donVi.iID_DonVi AND hd.iID_MaDonViQuanLy = donVi.iID_MaDonVi
LEFT JOIN NH_DM_NhaThau nt ON hd.iID_NhaThauThucHienID = nt.Id
LEFT JOIN (SELECT  n.ID, n.iID_KHTongTheID, d.sTenNhiemVuChi AS STenChuongTrinh, n.iID_NhiemVuChiID
	 FROM NH_KHTongThe_NhiemVuChi AS n 
	 INNER JOIN NH_DM_NhiemVuChi AS d 
	 ON n.iID_NhiemVuChiID = d.ID
) AS nvChi ON hd.iID_KHTongThe_NhiemVuChiID = nvChi.ID
LEFT JOIN (SELECT iID_HopDongID,
	SUM(fGiaTRiHopDong_USD) AS fGiaTRiHopDong_USD,
	SUM(fGiaTRiHopDong_VND) AS fGiaTRiHopDong_VND,
	SUM(fGiaTRiHopDong_EUR) AS fGiaTRiHopDong_EUR,
	SUM(fGiaTriHopDong_NgoaiTeKhac) AS fGiaTriHopDong_NgoaiTeKhac
	FROM NH_DA_HopDong_GoiThau_NhaThau 
	WHERE NH_DA_HopDong_GoiThau_NhaThau.isCheck=1
	GROUP BY iID_HopDongID
) gtnt ON hd.ID = gtnt.iID_HopDongID
WHERE @iThuocMenu IS NULL OR hd.iThuocMenu = @iThuocMenu
ORDER BY hd.dNgayTao DESC
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtin_hopdong_index]    Script Date: 11/3/2023 11:47:41 AM ******/
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
	hd.iID_KeHoachDatHangID AS IIdKeHoachDatHangId,
	hd.iID_TiGiaID AS IIdTiGiaId,
	hd.iID_DuAnID AS IIdDuAnId,
	hd.iLoai AS ILoai,
	hd.iThuocMenu AS IThuocMenu,
	hd.iThoiGianThucHien AS IThoiGianThucHien,
	hd.fGiaTriHopDongUSD AS FGiaTriHopDongUSD,
	hd.fGiaTriHopDongVND AS FGiaTriHopDongVND,
	hd.fGiaTriHopDongEUR AS FGiaTriHopDongEUR,
	hd.fGiaTriHopDongNgoaiTeKhac AS FGiaTriHopDongNgoaiTeKhac,
	hd.fTiGiaNhap AS FTiGiaNhap,
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
	khdh.sSoQuyetDinh AS SMaKeHoachDatHang,
	lhd.sMaLoaiHopDong AS SMaLoaiHopDong,
	nt.sMaNhaThau AS SMaNhaThauThucHien,
	hd.sHinhThucHopDong AS SHinhThucHopDong,
	da.sMaDuAn AS SMaDuAn,
	CASE WHEN hd.iID_ParentAdjustId IS NULL THEN '' ELSE ( SELECT TOP 1 hdpr.sSoHopDong FROM NH_DA_HopDong hdpr WHERE hdpr.Id = hd.iID_ParentAdjustId) END DieuChinhTu
FROM NH_DA_HopDong hd
LEFT JOIN NH_DM_LoaiHopDong lhd ON hd.iID_LoaiHopDongID = lhd.iID_LoaiHopDongID
LEFT JOIN NH_DA_DuAn da ON hd.iID_DuAnID = da.ID
LEFT JOIN DonVi donVi ON hd.iID_DonViQuanLyID = donVi.iID_DonVi AND hd.iID_MaDonViQuanLy = donVi.iID_MaDonVi
LEFT JOIN NH_MSTN_KeHoachDatHang khdh ON hd.iID_KeHoachDatHangID = khdh.ID
LEFT JOIN NH_DM_NhaThau nt ON hd.iID_NhaThauThucHienID = nt.Id
LEFT JOIN (SELECT n.ID, n.iID_KHTongTheID, d.sTenNhiemVuChi AS STenChuongTrinh, n.iID_NhiemVuChiID
	FROM NH_KHTongThe_NhiemVuChi AS n 
	INNER JOIN NH_DM_NhiemVuChi AS d 
	ON n.iID_NhiemVuChiID = d.ID
) AS nvChi ON hd.iID_KHTongThe_NhiemVuChiID = nvChi.ID
LEFT JOIN (SELECT iID_HopDongID, SUM(fGiaTRiHopDong_USD) AS fGiaTRiHopDong_USD,
	SUM(fGiaTRiHopDong_VND) AS fGiaTRiHopDong_VND,
	SUM(fGiaTRiHopDong_EUR) AS fGiaTRiHopDong_EUR,
	SUM(fGiaTriHopDong_NgoaiTeKhac) AS fGiaTriHopDong_NgoaiTeKhac
	FROM NH_DA_HopDong_GoiThau_NhaThau 
	GROUP BY iID_HopDongID
) AS gtnt ON hd.ID = gtnt.iID_HopDongID
WHERE @iThuocMenu IS NULL OR hd.iThuocMenu = @iThuocMenu
ORDER BY hd.dNgayTao DESC
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtin_hopdong_ngoaithuong_index]    Script Date: 11/3/2023 11:47:41 AM ******/
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
	hd.fTiGiaNhap as FTiGiaNhap,
	lhd.sTenLoaiHopDong AS SLoaiHopDong,
	hd.iLanDieuChinh AS ILanDieuChinh,
	nvChi.iID_KHTongTheID AS IIdKhTongTheId,
	nvChi.STenChuongTrinh AS STenChuongTrinh,
	(CASE
		WHEN da.sTenDuAn IS NULL THEN ''
		WHEN da.sMaDuAn IS NULL THEN da.sTenDuAn
		ELSE CONCAT(da.sMaDuAn, ' - ', da.sTenDuAn)
	END) AS STenDuAn,
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtinduan_index]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_thongtinduan_index]
	@iLoai int
AS BEGIN
	SELECT 
		duAn.ID AS Id,
		duAn.iID_KHTT_NhiemVuChiID AS IIdKhttNhiemVuChiId,
		duAn.sMaDuAn AS SMaDuAn,
		CONCAT(duAn.sMaDuAn, ' - ',duAn.sTenDuAn ) AS STenDuAn,
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
		duAn.fVND AS FVnd,
		duAn.fEUR AS FEur,
		duAn.fNgoaiTeKhac AS FNgoaiTeKhac,
		duAn.dNgayTao AS DNgayTao,
		duAn.sNguoiTao AS SNguoiTao,
		duAn.dNgaySua AS DNgaySua,
		duAn.sNguoiSua AS SNguoiSua,
		duAn.dNgayXoa AS DNgayXoa,
		duAn.sNguoiXoa AS SNguoiXoa,
		duAn.iID_ChuTruongDauTuID AS IIdChuTruongDauTuId,
		duAn.iID_TiGiaID AS IIdTiGiaID,
		duAn.sMaNgoaiTeKhac AS SMaNgoaiTeKhac,
		CONCAT(DonVi.iID_MaDonVi, ' - ', DonVi.sTenDonVi) AS STenDonVi,
		pheDuyet.sTen AS STenPheDuyet,
		DM_ChuDauTu.sTenDonVi AS STenChuDauTu,
		( SELECT COUNT ( Id ) FROM Attachment WHERE ModuleType = 409 AND ObjectId = duAn.ID ) AS TotalFiles,
		nvChi.ID as IIdKHTTNhiemVuChiId,
		nvChi.iID_NhiemVuChiID as iIDNhiemVuChiID,
		nvChi.iID_KHTongTheID as IIdKHTongTheID,
		nvChi.STenChuongTrinh as STenChuongTrinh
	FROM NH_DA_DuAn duAn
	LEFT JOIN DonVi 
		ON DonVi.iID_DonVi = duAn.iID_DonViQuanLyID
	LEFT JOIN DM_ChuDauTu 
		ON DM_ChuDauTu.iID_DonVi = duAn.iID_ChuDauTuID
	LEFT JOIN NH_DM_PhanCapPheDuyet AS pheDuyet
		on pheDuyet.ID = duAn.iID_CapPheDuyetID
	LEFT JOIN (SELECT  n.ID, n.iID_KHTongTheID, d.sTenNhiemVuChi AS STenChuongTrinh, n.iID_NhiemVuChiID
	 FROM NH_KHTongThe_NhiemVuChi AS n 
	 INNER JOIN NH_DM_NhiemVuChi AS d 
	 ON n.iID_NhiemVuChiID = d.ID
	) AS nvChi ON duAn.iID_KHTT_NhiemVuChiID = nvChi.ID
	WHERE duAn.iLoai = @iLoai
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtinnguonvon_byidgoithau]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_nh_thongtinnguonvon_byidgoithau]
	@idGoiThau uniqueidentifier
as begin
	select 
	NguonVon.iID_GoiThau_NguonVonID as Id,
	NguonVon.iID_GoiThauID as IIdGoiThauId,
	NguonNganSach.sTen as STenNguonVon,
	NguonVon.fTienGoiThau_USD as FTienGoiThauUsd,
	NguonVon.fTienGoiThau_VND as FTienGoiThauVnd,
	NguonVon.fTienGoiThau_EUR as FTienGoiThauEur,
	NguonVon.fTienGoiThau_NgoaiTeKhac as FTienGoiThauNgoaiTeKhac
	from NH_DA_GoiThau_NguonVon NguonVon
	left join NguonNganSach
		on NguonVon.iID_NguonVonID = NguonNganSach.iID_MaNguonNganSach
	where NguonVon.iID_GoiThauID = @idGoiThau
end
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtinnhathauhopdong_by_goithauid]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_nh_thongtinnhathauhopdong_by_goithauid]
 @idGoiThau uniqueidentifier
as begin
	Select 
	HDGTNhaThau.iID_GoiThauID as IIdGoiThauId,
	HDGTNhaThau.iID_HopDongID as IIdHopDongId,
	HopDong.sSoHopDong as SSoHopDong,
	HopDong.dNgayHopDong as DNgayHopDong,
	NhaThau.sTenNhaThau as STenNhaThau,
	LoaiHopDong.sTenLoaiHopDong as STenLoaiHopDong,
	HDGTNhaThau.fGiaTriUSD as FGiaTriUSD,
	HDGTNhaThau.fGiaTriVND as FGiaTriVND,
	HDGTNhaThau.fGiaTriEUR as FGiaTriEUR,
	HDGTNhaThau.fGiaTriEUR as FGiaTriNgoaiTeKhac
	from NH_DA_HopDong_GoiThau_NhaThau HDGTNhaThau
	inner join NH_DA_GoiThau GoiThau
		on HDGTNhaThau.iID_GoiThauID = GoiThau.iID_GoiThauID
	inner join NH_DA_HopDong HopDong
		on HDGTNhaThau.iID_HopDongID = HopDong.Id
	left join NH_DM_NhaThau NhaThau
		on HDGTNhaThau.iID_NhaThauID = NhaThau.Id
	left join NH_DM_LoaiHopDong LoaiHopDong
		on HopDong.iID_LoaiHopDongID = LoaiHopDong.iID_LoaiHopDongID
	where HDGTNhaThau.iID_GoiThauID = @idGoiThau
end
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtinthanhtoan_index]    Script Date: 11/3/2023 11:47:41 AM ******/
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtri_capphat_index]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_thongtri_capphat_index]
AS
BEGIN
	SELECT 
		thongtri.ID AS Id,
		thongtri.iID_MaDonViID AS IIdMaDonViId,
		thongtri.iID_DonViID AS IIdDonViId,
		thongtri.iID_NguonVonID AS IIdNguonVonId,
		thongtri.sMaThongTri AS SMaThongTri,
		thongtri.dNgayLapThongTri AS DNgayLapThongTri,
		thongtri.iNamThucHien AS INamThucHien,
		thongtri.iID_DonViTienTeID AS IIdDonViTienTeId,
		thongtri.dNgayGhiSo AS DNgayGhiSo,
		thongtri.sTK1 AS STk1,
		thongtri.sSoCT1 AS SSoCt1,
		thongtri.sTK2 AS STk2,
		thongtri.sSoCT2 AS SSoCt2,
		thongtri.iID_TiGiaUSD_VNDID AS IIdTiGiaUsdVndid,
		thongtri.iID_TiGiaUSD_NgoaiTeKhacID AS IIdTiGiaUsdNgoaiTeKhacId,
		thongtri.fTongGiaTriNgoaiTeKhac AS FTongGiaTriNgoaiTeKhac,
		thongtri.fTongGiaTriUSD AS FTongGiaTriUsd,
		thongtri.fTongGiaTriVND AS FTongGiaTriVnd,
		thongtri.sTongGiaTri_BangChu AS STongGiaTriBangChu,
		thongtri.sNguoiTao AS SNguoiTao,
		thongtri.dNgayTao AS DNgayTao,
		thongtri.sNguoiSua AS SNguoiSua,
		thongtri.dNgaySua AS DNgaySua,
		thongtri.sNguoiXoa AS SNguoiXoa,
		thongtri.dNgayXoa AS DNgayXoa,
		thongtri.bIsActive AS BIsActive,
		thongtri.bIsGoc AS BIsGoc,
		thongtri.bIsKhoa AS BIsKhoa,
		thongtri.iLanDieuChinh AS ILanDieuChinh,
		thongtri.iID_TiGiaID AS IIdTiGiaId,
		thongtri.bIsXoa AS BIsXoa,
		thongtri.sMaNgoaiTeKhac AS SMaNgoaiTeKhac,
		CONCAT(DonVi.iID_MaDonVi, ' - ', DonVi.sTenDonVi) AS STenDonVi,
		NguonNganSach.sTen AS STenNguonVon,
		tiente.sMaTienTe AS STenTienTe
	FROM NH_TT_ThongTriCapPhat thongtri
	LEFT JOIN DonVi ON thongtri.iID_DonViID = DonVi.iID_DonVi
	LEFT JOIN NguonNganSach ON thongtri.iID_NguonVonID = NguonNganSach.iID_MaNguonNganSach
	LEFT JOIN NH_DM_LoaiTienTe tiente ON thongtri.iID_DonViTienTeID = tiente.ID
	ORDER BY DNgayTao DESC
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtricapphat_dspheduyetthanhtoan]    Script Date: 11/3/2023 11:47:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_thongtricapphat_dspheduyetthanhtoan]
AS
BEGIN
	SELECT 
		thanhtoan.ID AS IIdPheDuyetThanhToanId,
		chitiet.ID AS Id,
		chitiet.iID_ThongTriCapPhatID AS IIdThongTriCapPhatId,
		chitiet.sMaOrder AS SMaOrder,
		chitiet.iTrangThai AS ITrangThai,
		CASE 
			WHEN thanhtoan.iTrangThai = 2  THEN thanhtoan.sSoDeNghi 
		END AS SSoDeNghi,
		nhiemvuchi.sTenNhiemVuChi AS STenNhiemVuChi,
		hopdong.sTenHopDong AS STenHopDong,
		CASE
			WHEN thanhtoan.iLoaiDeNghi = 1 THEN N'Cấp kinh phí'
			WHEN thanhtoan.iLoaiDeNghi = 2 THEN N'Tạm ứng kinh phí'
			WHEN thanhtoan.iLoaiDeNghi = 3 THEN N'Thanh toán theo khối lượng'
			WHEN thanhtoan.iLoaiDeNghi = 4 THEN N'Tạm ứng theo chế độ'
		END AS SLoaiDeNghi,
		SUM(TTchitiet.fPheDuyetCapKyNay_USD) AS FPheDuyetUsd,
		SUM(TTchitiet.fPheDuyetCapKyNay_VND) AS FPheDuyetVnd,
		SUM(TTchitiet.fPheDuyetCapKyNay_EUR) AS FPheDuyetEur,
		SUM(TTchitiet.fPheDuyetCapKyNay_NgoaiTeKhac) AS FPheDuyetNgoaiTeKhac,
		thanhtoan.iNamKeHoach AS iNamKeHoach,
		thanhtoan.iID_DonVi AS iID_DonVi,
		thanhtoan.iID_NguonVonID AS iID_NguonVonID
	FROM NH_TT_ThanhToan thanhtoan 
	LEFT JOIN NH_TT_ThongTriCapPhat_ChiTiet chitiet ON thanhtoan.ID = chitiet.iID_PheDuyetThanhToanID
	LEFT JOIN NH_DM_NhiemVuChi nhiemvuchi ON thanhtoan.iID_NhiemVuChiID = nhiemvuchi.ID
	LEFT JOIN NH_DA_HopDong hopdong ON thanhtoan.iID_HopDongID = hopdong.Id
	LEFT JOIN NH_TT_ThanhToan_ChiTiet TTchitiet ON thanhtoan.ID = TTchitiet.iID_DeNghiThanhToanID
	WHERE thanhtoan.iTrangThai = 2
	GROUP BY thanhtoan.ID, chitiet.ID, chitiet.iID_ThongTriCapPhatID, chitiet.sMaOrder, chitiet.iTrangThai, thanhtoan.iLoaiDeNghi, thanhtoan.iNamKeHoach, thanhtoan.iID_DonVi, thanhtoan.iID_NguonVonID,
	nhiemvuchi.sTenNhiemVuChi, hopdong.sTenHopDong, thanhtoan.iTrangThai, thanhtoan.sSoDeNghi
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thuchien_ngansach_index]    Script Date: 11/3/2023 11:47:41 AM ******/
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
		KHTT.iGiaiDoanDen_TTCP as iGiaiDoanDen , KHTT.iGiaiDoanTu_TTCP as iGiaiDoanTu,
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thuchien_ngansach_report]    Script Date: 11/3/2023 11:47:41 AM ******/
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
;
GO
