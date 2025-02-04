/****** Object:  StoredProcedure [dbo].[sp_bh_dctc_get_data_quyet_toan]    Script Date: 12/23/2024 4:11:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_dctc_get_data_quyet_toan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_dctc_get_data_quyet_toan]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dctc_get_data_quyet_toan]    Script Date: 12/23/2024 4:11:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_dctc_get_data_quyet_toan] 
	@NamLamViec INT,
	@MaDonVi NVARCHAR(max),
	@Quy INT,
	@SLNS NVARCHAR(max)
AS
BEGIN

			select 
			ctct.iIDMaDonVi as IIDMaDonVi,
			ctct.sXauNoiMa,
			sum	(isnull(ctct.fTienSQ_DeNghi,0) + isnull(ctct.fTienQNCN_DeNghi,0) + isnull(ctct.fTienCNVCQP_DeNghi,0) + isnull(ctct.fTienHSQBS_DeNghi,0) + isnull(ctct.fTienLDHD_DeNghi,0)) as FTienThucHien06ThangDauNam
			from BH_QTC_Quy_CheDoBHXH_ChiTiet ctct
			left join BH_QTC_Quy_CheDoBHXH ct on ctct.iID_QTC_Quy_CheDoBHXH=ct.ID_QTC_Quy_CheDoBHXH
			where ct.iNamChungTu=2024
			and ct.bIsKhoa=1
			and ct.iID_MaDonVi in (select * from f_split(@MaDonVi))
			group by ctct.sXauNoiMa,ctct.iIDMaDonVi

			union all

			select  
			ctct.iIDMaDonVi as IIDMaDonVi,
			ctct.sXauNoiMa,
			sum	(isnull(ctct.FTienDeNghiQuyetToanQuyNay,0)) as FTienThucHien06ThangDauNam
			from BH_QTC_Quy_KinhPhiQuanLy_ChiTiet ctct
			left join BH_QTC_Quy_KinhPhiQuanLy ct on ctct.iID_QTC_Quy_KinhPhiQuanLy=ct.ID_QTC_Quy_KinhPhiQuanLy
			where ct.iNamChungTu=2024
			and ct.bIsKhoa=1
			and ct.iID_MaDonVi in (select * from f_split(@MaDonVi))
			group by ctct.sXauNoiMa,ctct.iIDMaDonVi

			union all
			select 
			ctct.iID_MaDonVi as IIDMaDonVi,
			ctct.sXauNoiMa,
			sum	(isnull(ctct.FTienDeNghiQuyetToanQuyNay,0)) as FTienThucHien06ThangDauNam
			from BH_QTC_Quy_KCB_ChiTiet ctct
			left join BH_QTC_Quy_KCB ct on ctct.iID_QTC_Quy_KCB=ct.ID_QTC_Quy_KCB
			where ct.iNamChungTu=2024
			and ct.bIsKhoa=1
			and ct.iID_MaDonVi in (select * from f_split(@MaDonVi))
			group by ctct.sXauNoiMa,ctct.iID_MaDonVi

			union all

			select 
			ctct.iIDMaDonVi as IIDMaDonVi,
			ctct.sXauNoiMa,
			sum	(isnull(ctct.FTienDeNghiQuyetToanQuyNay,0)) as FTienThucHien06ThangDauNam
			from BH_QTC_Quy_KPK_ChiTiet ctct
			left join BH_QTC_Quy_KPK ct on ctct.iID_QTC_Quy_KPK=ct.ID_QTC_Quy_KPK
			where ct.iNamChungTu=2024
			and ct.bIsKhoa=1
			and ct.sDSLNS=@SLNS
			and ct.iID_MaDonVi in (select * from f_split(@MaDonVi))
			group by ctct.sXauNoiMa,ctct.iIDMaDonVi 


END
;
GO

DELETE FROM [dbo].[DM_ChuKy]
where Id_Type='rpt_ThuNop_QuocPhong_Thang'

INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) VALUES (NEWID(), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rpt_ThuNop_QuocPhong_Thang', NULL, N'rpt_ThuNop_QuocPhong_Thang', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', N'BÁO CÁO CÁC KHOẢN THU NỘP BỘ QUỐC PHÒNG', NULL, N'Kèm theo Công văn số...../CTC-KHNS ngày...tháng....năm........ của Cục Tài chính', NULL, NULL, NULL, NULL, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL)
GO

DELETE FROM [dbo].[DM_ChuKy]
where Id_Type='rpt_ThuNop_QuocPhong_Quy'

INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) VALUES (NEWID(), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rpt_ThuNop_QuocPhong_Quy', NULL, N'rpt_ThuNop_QuocPhong_Quy', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', N'BÁO CÁO CÁC KHOẢN THU NỘP BỘ QUỐC PHÒNG', NULL, N'Kèm theo Công văn số...../CTC-KHNS ngày...tháng....năm 2024 của Cục Tài chính', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

DELETE FROM [dbo].[DM_ChuKy]
where Id_Type='rpt_ThuNop_QuocPhong_Nam'

INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) VALUES (NEWID(), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rpt_ThuNop_QuocPhong_Nam', NULL, N'rpt_ThuNop_QuocPhong_Nam', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', N'BÁO CÁO CÁC KHOẢN THU NỘP BỘ QUỐC PHÒNG NĂM', NULL, N'Kèm theo Công văn số...../CTC-KHNS ngày...tháng....năm........ của Cục Tài chính', NULL, NULL, NULL, NULL, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL)
GO

DELETE FROM [dbo].[DM_ChuKy]
where Id_Type='rpt_ThuNop_NganSach_NhaNuoc_Thang'

INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) VALUES (NEWID(), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rpt_ThuNop_NganSach_NhaNuoc_Thang', NULL, N'rpt_ThuNop_NganSach_NhaNuoc_Thang', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', N'BÁO CÁO CÁC KHOẢN THU NỘP NGÂN SÁCH NHÀ NƯỚC', NULL, N'Kèm theo Công văn số...../CTC-KHNS ngày...tháng....năm........ của Cục Tài chính', NULL, NULL, NULL, NULL, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL)
GO

DELETE FROM [dbo].[DM_ChuKy]
where Id_Type='rpt_ThuNop_NganSach_NhaNuoc_Quy'

INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) VALUES (NEWID(), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rpt_ThuNop_NganSach_NhaNuoc_Quy', NULL, N'rpt_ThuNop_NganSach_NhaNuoc_Quy', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', N'BÁO CÁO CÁC KHOẢN THU NỘP NGÂN SÁCH NHÀ NƯỚC', NULL, N'Kèm theo Công văn số...../CTC-KHNS ngày...tháng....năm........ của Cục Tài chính', NULL, NULL, NULL, NULL, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL)
GO


DELETE FROM [dbo].[DM_ChuKy]
where Id_Type='rpt_ThuNop_NganSach_NhaNuoc_Nam'
INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) VALUES (NEWID(), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rpt_ThuNop_NganSach_NhaNuoc_Nam', NULL, N'rpt_ThuNop_NganSach_NhaNuoc_Nam', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', N'BÁO CÁO CÁC KHOẢN THU NỘP NGÂN SÁCH NHÀ NƯỚC NĂM', NULL, N'Kèm theo Công văn số...../CTC-KHNS ngày...tháng....năm........ của Cục Tài chính', NULL, NULL, NULL, NULL, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL)
GO
