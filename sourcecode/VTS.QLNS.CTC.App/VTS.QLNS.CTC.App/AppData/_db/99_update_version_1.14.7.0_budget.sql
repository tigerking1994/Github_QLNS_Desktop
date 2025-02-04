IF EXISTS (SELECT * FROM [dbo].[DM_ChuKy] WHERE Id_Type = 'rptNS_DuToan_DauNam_ChiThuongXuyen_MHHV')
UPDATE [dbo].[DM_ChuKy] SET TieuDe1_MoTa = N'BÁO CÁO TỔNG HỢP DỰ TOÁN NGÂN SÁCH NHÀ NƯỚC CHI THƯỜNG XUYÊN CHO QUỐC PHÒNG NĂM 2024', TieuDe2_MoTa = N'(NỘI DUNG CHI MUA HÀNG TẬP TRUNG ĐỂ CẤP HIỆN VẬT CHO ĐƠN VỊ)'
WHERE Id_Type = 'rptNS_DuToan_DauNam_ChiThuongXuyen_MHHV'
ELSE
INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) 
VALUES (NEWID(), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rptNS_DuToan_DauNam_ChiThuongXuyen_MHHV', NULL, N'rptNS_DuToan_DauNam_ChiThuongXuyen_MHHV', NULL, NULL, NULL, NULL, NULL, N'DU_TOAN_DAU_NAM', NULL, N'Dự toán ngân sách đặc thù của ngành', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', N'BÁO CÁO TỔNG HỢP DỰ TOÁN NGÂN SÁCH NHÀ NƯỚC CHI THƯỜNG XUYÊN CHO QUỐC PHÒNG NĂM 2024', NULL, N'(NỘI DUNG CHI MUA HÀNG TẬP TRUNG ĐỂ CẤP HIỆN VẬT CHO ĐƠN VỊ)', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

IF EXISTS (SELECT * FROM [dbo].[DM_ChuKy] WHERE Id_Type = 'rptNS_DuToan_DauNam_ChiThuongXuyen_DacThu')
UPDATE [dbo].[DM_ChuKy] SET TieuDe1_MoTa = N'BÁO CÁO TỔNG HỢP DỰ TOÁN NGÂN SÁCH NHÀ NƯỚC CHI THƯỜNG XUYÊN CHO QUỐC PHÒNG NĂM 2024', TieuDe2_MoTa = N'(NỘI DUNG CHI ĐẶC THÙ NGÀNH NGHIỆP VỤ PHÂN CẤP)'
WHERE Id_Type = 'rptNS_DuToan_DauNam_ChiThuongXuyen_DacThu'
ELSE
INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) 
VALUES (NEWID(), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rptNS_DuToan_DauNam_ChiThuongXuyen_DacThu', NULL, N'rptNS_DuToan_DauNam_ChiThuongXuyen_DacThu', NULL, NULL, NULL, NULL, NULL, N'DU_TOAN_DAU_NAM', NULL, N'Dự toán ngân sách đặc thù của ngành', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', N'BÁO CÁO TỔNG HỢP DỰ TOÁN NGÂN SÁCH NHÀ NƯỚC CHI THƯỜNG XUYÊN CHO QUỐC PHÒNG NĂM 2024', NULL, N'(NỘI DUNG CHI ĐẶC THÙ NGÀNH NGHIỆP VỤ PHÂN CẤP)', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

IF EXISTS (SELECT * FROM [dbo].[DM_ChuKy] WHERE Id_Type = 'rptNS_DuToan_DauNam')
UPDATE [dbo].[DM_ChuKy] SET TieuDe1_MoTa = N'BÁO CÁO TỔNG HỢP DỰ TOÁN NGÂN SÁCH NHÀ NƯỚC CHI THƯỜNG XUYÊN CHO QUỐC PHÒNG NĂM 2024', TieuDe2_MoTa = N'(KHÔNG BAO GỒM NỘI DUNG CHI ĐẦU TƯ XÂY DỰNG CƠ BẢN)'
WHERE Id_Type = 'rptNS_DuToan_DauNam'
ELSE
INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) 
VALUES (NEWID(), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rptNS_DuToan_DauNam', NULL, N'rptNS_DuToan_DauNam', NULL, NULL, NULL, NULL, NULL, N'DU_TOAN_DAU_NAM', NULL, N'Tổng hợp dự toán - Ước thực hiện', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', N'BÁO CÁO TỔNG HỢP DỰ TOÁN NGÂN SÁCH NHÀ NƯỚC CHI THƯỜNG XUYÊN CHO QUỐC PHÒNG NĂM 2024', NULL, N'(KHÔNG BAO GỒM NỘI DUNG CHI ĐẦU TƯ XÂY DỰNG CƠ BẢN)', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

IF EXISTS (SELECT * FROM [dbo].[DM_ChuKy] WHERE Id_Type = 'rptNS_SNC_ChiTiet_NSSD')
UPDATE [dbo].[DM_ChuKy] SET TieuDe1_MoTa = N'NHU CẦU NGÂN SÁCH NHÀ NƯỚC CHI THƯỜNG XUYÊN CHO QUỐC PHÒNG NĂM 2024', TieuDe2_MoTa = N'(Kèm theo Công văn số: ......./............... ngày ...../.../2024) của .................)'
WHERE Id_Type = 'rptNS_SNC_ChiTiet_NSSD'
ELSE
INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) 
VALUES (NEWID(), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rptNS_SNC_ChiTiet_NSSD', NULL, N'rptNS_SNC_ChiTiet_NSSD', NULL, NULL, NULL, NULL, NULL, N'SO_NHU_CAU', NULL, N'Báo cáo chi tiết số nhu cầu', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', NULL, NULL, N'NHU CẦU NGÂN SÁCH NHÀ NƯỚC CHI THƯỜNG XUYÊN CHO QUỐC PHÒNG NĂM 2024', NULL, N'(Kèm theo Công văn số: ......./............... ngày ...../.../2024) của .................)', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

IF EXISTS (SELECT * FROM [dbo].[DM_ChuKy] WHERE Id_Type = 'rptNS_SNC_ChiTiet_NSBD_DT')
UPDATE [dbo].[DM_ChuKy] SET TieuDe1_MoTa = N'NHU CẦU NGÂN SÁCH NHÀ NƯỚC CHI THƯỜNG XUYÊN CHO QUỐC PHÒNG NĂM 2024', TieuDe2_MoTa = N'(NỘI DUNG CHI ĐẶC THÙ NGÀNH NGHIỆP VỤ PHÂN CẤP)'
WHERE Id_Type = 'rptNS_SNC_ChiTiet_NSBD_DT'
ELSE
INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) 
VALUES (NEWID(), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rptNS_SNC_ChiTiet_NSBD_DT', NULL, N'rptNS_SNC_ChiTiet_NSBD_DT', NULL, NULL, NULL, NULL, NULL, N'SO_NHU_CAU', NULL, N'Báo cáo chi tiết số nhu cầu', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', NULL, NULL, N'NHU CẦU NGÂN SÁCH NHÀ NƯỚC CHI THƯỜNG XUYÊN CHO QUỐC PHÒNG NĂM 2024', NULL, N'(NỘI DUNG CHI ĐẶC THÙ NGÀNH NGHIỆP VỤ PHÂN CẤP)', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

IF EXISTS (SELECT * FROM [dbo].[DM_ChuKy] WHERE Id_Type = 'rptNS_SNC_ChiTiet_NSBD_MHHV')
UPDATE [dbo].[DM_ChuKy] SET TieuDe1_MoTa = N'NHU CẦU NGÂN SÁCH NHÀ NƯỚC CHI THƯỜNG XUYÊN CHO QUỐC PHÒNG NĂM 2024', TieuDe2_MoTa = N'(MUA HÀNG TẬP TRUNG ĐỂ CẤP HIỆN VẬT CHO ĐƠN VỊ)'
WHERE Id_Type = 'rptNS_SNC_ChiTiet_NSBD_MHHV'
ELSE
INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) 
VALUES (NEWID(), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rptNS_SNC_ChiTiet_NSBD_MHHV', NULL, N'rptNS_SNC_ChiTiet_NSBD_MHHV', NULL, NULL, NULL, NULL, NULL, N'SO_NHU_CAU', NULL, N'Báo cáo chi tiết số nhu cầu', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', NULL, NULL, N'NHU CẦU NGÂN SÁCH NHÀ NƯỚC CHI THƯỜNG XUYÊN CHO QUỐC PHÒNG NĂM 2024', NULL, N'(MUA HÀNG TẬP TRUNG ĐỂ CẤP HIỆN VẬT CHO ĐƠN VỊ)', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

IF EXISTS (SELECT * FROM [dbo].[DM_ChuKy] WHERE Id_Type = 'rptNS_SNC_TongHop_NSSD')
UPDATE [dbo].[DM_ChuKy] SET TieuDe1_MoTa = N'NHU CẦU NGÂN SÁCH NHÀ NƯỚC CHI THƯỜNG XUYÊN CHO QUỐC PHÒNG NĂM 2024', TieuDe2_MoTa = N'(Kèm theo Công văn số: ......./............... ngày ...../.../2024) của .................)'
WHERE Id_Type = 'rptNS_SNC_TongHop_NSSD'
ELSE
INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) 
VALUES (NEWID(), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rptNS_SNC_TongHop_NSSD', NULL, N'rptNS_SNC_TongHop_NSSD', NULL, NULL, NULL, NULL, NULL, N'SO_NHU_CAU', NULL, N'Báo cáo số nhu cầu tổng hợp', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', NULL, NULL, N'NHU CẦU NGÂN SÁCH NHÀ NƯỚC CHI THƯỜNG XUYÊN CHO QUỐC PHÒNG NĂM 2024', NULL, N'(Kèm theo Công văn số: ......./............... ngày ...../.../2024) của .................)', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

IF EXISTS (SELECT * FROM [dbo].[DM_ChuKy] WHERE Id_Type = 'rptNS_SNC_TongHop_NSBD_DT')
UPDATE [dbo].[DM_ChuKy] SET TieuDe1_MoTa = N'NHU CẦU NGÂN SÁCH NHÀ NƯỚC CHI THƯỜNG XUYÊN CHO QUỐC PHÒNG NĂM 2024', TieuDe2_MoTa = N'(NỘI DUNG CHI ĐẶC THÙ NGÀNH NGHIỆP VỤ PHÂN CẤP)'
WHERE Id_Type = 'rptNS_SNC_TongHop_NSBD_DT'
ELSE
INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) 
VALUES (NEWID(), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rptNS_SNC_TongHop_NSBD_DT', NULL, N'rptNS_SNC_TongHop_NSBD_DT', NULL, NULL, NULL, NULL, NULL, N'SO_NHU_CAU', NULL, N'Báo cáo số nhu cầu tổng hợp', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', NULL, NULL, N'NHU CẦU NGÂN SÁCH NHÀ NƯỚC CHI THƯỜNG XUYÊN CHO QUỐC PHÒNG NĂM 2024', NULL, N'(NỘI DUNG CHI ĐẶC THÙ NGÀNH NGHIỆP VỤ PHÂN CẤP)', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

IF EXISTS (SELECT * FROM [dbo].[DM_ChuKy] WHERE Id_Type = 'rptNS_SNC_TongHop_NSBD_MHHV')
UPDATE [dbo].[DM_ChuKy] SET TieuDe1_MoTa = N'NHU CẦU NGÂN SÁCH NHÀ NƯỚC CHI THƯỜNG XUYÊN CHO QUỐC PHÒNG NĂM 2024', TieuDe2_MoTa = N'(MUA HÀNG TẬP TRUNG ĐỂ CẤP HIỆN VẬT CHO ĐƠN VỊ)'
WHERE Id_Type = 'rptNS_SNC_TongHop_NSBD_MHHV'
ELSE
INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) 
VALUES (NEWID(), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rptNS_SNC_TongHop_NSBD_MHHV', NULL, N'rptNS_SNC_TongHop_NSBD_MHHV', NULL, NULL, NULL, NULL, NULL, N'SO_NHU_CAU', NULL, N'Báo cáo số nhu cầu tổng hợp', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', NULL, NULL, N'NHU CẦU NGÂN SÁCH NHÀ NƯỚC CHI THƯỜNG XUYÊN CHO QUỐC PHÒNG NĂM 2024', NULL, N'(MUA HÀNG TẬP TRUNG ĐỂ CẤP HIỆN VẬT CHO ĐƠN VỊ)', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

IF EXISTS (SELECT * FROM [dbo].[DM_ChuKy] WHERE Id_Type = 'rptNS_QuyetToan_ThuongXuyen_TongHop')
UPDATE [dbo].[DM_ChuKy] SET TieuDe1_MoTa = N'BÁO CÁO THỰC HIỆN DỰ TOÁN CHI TIỀN LƯƠNG, PHỤ CẤP, TIỀN ĂN VÀ CÁC KHOẢN ĐÓNG GÓP THEO LƯƠNG', TieuDe2_MoTa = N'Loại 010 - Khoản... (1)', TieuDe3_MoTa = NULL
WHERE Id_Type = 'rptNS_QuyetToan_ThuongXuyen_TongHop'
ELSE
INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) 
VALUES (NEWID(), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rptNS_QuyetToan_ThuongXuyen_TongHop', NULL, N'rptNS_QuyetToan_ThuongXuyen_TongHop', NULL, NULL, NULL, NULL, NULL, N'QUYET_TOAN', NULL, N'Tổng hợp quý - Quyết toán thường xuyên', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', N'BÁO CÁO THỰC HIỆN DỰ TOÁN CHI TIỀN LƯƠNG, PHỤ CẤP, TIỀN ĂN VÀ CÁC KHOẢN ĐÓNG GÓP THEO LƯƠNG', NULL, N'Loại 010 - Khoản ... (1)', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

IF EXISTS (SELECT * FROM [dbo].[DM_ChuKy] WHERE Id_Type = 'rptNS_QuyetToan_QuocPhong_TongHop')
UPDATE [dbo].[DM_ChuKy] SET TieuDe1_MoTa = N'BÁO CÁO THỰC HIỆN DỰ TOÁN NGÂN SÁCH NHÀ NƯỚC CHI THƯỜNG XUYÊN', TieuDe2_MoTa = NULL
WHERE Id_Type = 'rptNS_QuyetToan_QuocPhong_TongHop'
ELSE
INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) 
VALUES (NEWID(), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rptNS_QuyetToan_QuocPhong_TongHop', NULL, N'rptNS_QuyetToan_QuocPhong_TongHop', NULL, NULL, NULL, NULL, NULL, N'QUYET_TOAN', NULL, N'Tổng hợp quý - Quyết toán nghiệp vụ, quốc phòng khác', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', N'BÁO CÁO THỰC HIỆN DỰ TOÁN NGÂN SÁCH NHÀ NƯỚC CHI THƯỜNG XUYÊN', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

IF EXISTS (SELECT * FROM [dbo].[DM_ChuKy] WHERE Id_Type = 'rptNS_QuyetToan_NhaNuoc_TongHop')
UPDATE [dbo].[DM_ChuKy] SET TieuDe1_MoTa = N'BÁO CÁO THỰC HIỆN DỰ TOÁN NGÂN SÁCH NHÀ NƯỚC CHI THƯỜNG XUYÊN', TieuDe2_MoTa = NULL
WHERE Id_Type = 'rptNS_QuyetToan_NhaNuoc_TongHop'
ELSE
INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) 
VALUES (NEWID(), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rptNS_QuyetToan_NhaNuoc_TongHop', NULL, N'rptNS_QuyetToan_NhaNuoc_TongHop', NULL, NULL, NULL, NULL, NULL, N'QUYET_TOAN', NULL, N'Tổng hợp quý - Quyết toán nhà nước', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', N'BÁO CÁO THỰC HIỆN DỰ TOÁN NGÂN SÁCH NHÀ NƯỚC CHI THƯỜNG XUYÊN', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

IF EXISTS (SELECT * FROM [dbo].[DM_ChuKy] WHERE Id_Type = 'rptNS_QuyetToan_NgoaiHoi_TongHop')
UPDATE [dbo].[DM_ChuKy] SET TieuDe1_MoTa = N'BÁO CÁO THỰC HIỆN DỰ TOÁN NGÂN SÁCH NHÀ NƯỚC CHI THƯỜNG XUYÊN', TieuDe2_MoTa = NULL
WHERE Id_Type = 'rptNS_QuyetToan_NgoaiHoi_TongHop'
ELSE
INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) 
VALUES (NEWID(), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rptNS_QuyetToan_NgoaiHoi_TongHop', NULL, N'rptNS_QuyetToan_NgoaiHoi_TongHop', NULL, NULL, NULL, NULL, NULL, N'QUYET_TOAN', NULL, N'Tổng hợp quý - Quyết toán ngoại hối', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', N'BÁO CÁO THỰC HIỆN DỰ TOÁN NGÂN SÁCH NHÀ NƯỚC CHI THƯỜNG XUYÊN', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

IF EXISTS (SELECT * FROM [dbo].[DM_ChuKy] WHERE Id_Type = 'rptNS_QuyetToan_KinhPhiKhac_TongHop')
UPDATE [dbo].[DM_ChuKy] SET TieuDe1_MoTa = N'BÁO CÁO THỰC HIỆN DỰ TOÁN NGÂN SÁCH NHÀ NƯỚC CHI THƯỜNG XUYÊN', TieuDe2_MoTa = NULL
WHERE Id_Type = 'rptNS_QuyetToan_KinhPhiKhac_TongHop'
ELSE
INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) 
VALUES (NEWID(), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rptNS_QuyetToan_KinhPhiKhac_TongHop', NULL, N'rptNS_QuyetToan_KinhPhiKhac_TongHop', NULL, NULL, NULL, NULL, NULL, N'QUYET_TOAN', NULL, N'Tổng hợp quý - Quyết toán kinh phí khác', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', N'BÁO CÁO THỰC HIỆN DỰ TOÁN NGÂN SÁCH NHÀ NƯỚC CHI THƯỜNG XUYÊN', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

IF EXISTS (SELECT * FROM [dbo].[DM_ChuKy] WHERE Id_Type = 'rptNS_QuyetToan_TatCa_TongHop')
UPDATE [dbo].[DM_ChuKy] SET TieuDe1_MoTa = N'BÁO CÁO THỰC HIỆN DỰ TOÁN NGÂN SÁCH NHÀ NƯỚC CHI THƯỜNG XUYÊN', TieuDe2_MoTa = NULL
WHERE Id_Type = 'rptNS_QuyetToan_TatCa_TongHop'
ELSE
INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) 
VALUES (NEWID(), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rptNS_QuyetToan_TatCa_TongHop', NULL, N'rptNS_QuyetToan_TatCa_TongHop', NULL, NULL, NULL, NULL, NULL, N'QUYET_TOAN', NULL, N'Tổng hợp quý - Quyết toán tổng hợp', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', N'BÁO CÁO THỰC HIỆN DỰ TOÁN NGÂN SÁCH NHÀ NƯỚC CHI THƯỜNG XUYÊN', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_so_nhu_cau_phu_luc_5]    Script Date: 7/29/2024 1:48:57 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_tong_hop_so_nhu_cau_phu_luc_5]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_tong_hop_so_nhu_cau_phu_luc_5]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_so_nhu_cau_phu_luc_4]    Script Date: 7/29/2024 1:48:57 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_tong_hop_so_nhu_cau_phu_luc_4]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_tong_hop_so_nhu_cau_phu_luc_4]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_so_nhu_cau_phu_luc_3]    Script Date: 7/29/2024 1:48:57 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_tong_hop_so_nhu_cau_phu_luc_3]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_tong_hop_so_nhu_cau_phu_luc_3]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_chi_tiet_so_nhu_cau_phu_luc_5]    Script Date: 7/29/2024 1:48:57 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_chi_tiet_so_nhu_cau_phu_luc_5]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_chi_tiet_so_nhu_cau_phu_luc_5]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_chi_tiet_so_nhu_cau_phu_luc_4]    Script Date: 7/29/2024 1:48:57 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_chi_tiet_so_nhu_cau_phu_luc_4]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_chi_tiet_so_nhu_cau_phu_luc_4]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_chi_tiet_so_nhu_cau_phu_luc_3]    Script Date: 7/29/2024 1:48:57 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_chi_tiet_so_nhu_cau_phu_luc_3]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_chi_tiet_so_nhu_cau_phu_luc_3]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_chung_tu_chi_tiet]    Script Date: 7/30/2024 2:37:54 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_chung_tu_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_chung_tu_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_chi_tiet_so_nhu_cau_phu_luc_3]    Script Date: 7/29/2024 1:48:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_skt_chi_tiet_so_nhu_cau_phu_luc_3]
	@LoaiChungTu int,
	@IdDonVi nvarchar(max),
	@NamLamViec int,
	@NamNganSach int,
	@MaNguonNganSach int,
	@dvt int,
	@loaiNNS int
AS
BEGIN
DECLARE @sModule nvarchar(MAX) = 'BUDGET_DEMANDCHECK_DEMAND',
        @idDuToan nvarchar(MAX) = 'BUDGET_ESTIMATE',
        @idSoKiemTra nvarchar(MAX) = 'BUDGET_DEMANDCHECK_CHECK' 
DECLARE @tblDuToan TABLE (iID_MLSKT uniqueidentifier, KyHieu nvarchar(200),TuChi float) 
DECLARE @tblSkt TABLE (iID_MLSKT uniqueidentifier,sKyHieu nvarchar(200) , TuChi float) 
DECLARE @countCanCuDuToan int = 0;

DECLARE @countCanCuSkt int = 0;

DECLARE @countSktChiTiet int = 0;


INSERT INTO @tblDuToan (iID_MLSKT, KyHieu, TuChi)
SELECT cc.iID_MLSKT,
       cc.sKyHieu,
       sum(isnull(cc.fTuChi, 0))
FROM
  (SELECT *
   FROM NS_SKT_ChungTuChiTiet_CanCu
   WHERE iNamLamViec = @NamLamViec
     AND iID_CanCu IN
       (SELECT iID_CauHinh_CanCu
        FROM NS_CauHinh_CanCu
        WHERE sModule = @sModule
          AND iID_MaChucNang = @idDuToan
          AND inamLamViec = @NamLamViec
          AND iNamCancu = @NamLamViec - 1)
     AND iiID_CTSoKiemTra in
       (SELECT iID_CTSoKiemTra
        FROM NS_SKT_ChungTu
        WHERE iID_MaDonVi = @IdDonVi
          AND iNamLamViec = @NamLamViec
          AND iNamNganSach = @NamNganSach
          AND iID_MaNguonNganSach = @MaNguonNganSach
          AND iLoaiChungTu = @LoaiChungTu
		  AND (@loaiNNS = 0 OR iLoaiNguonNganSach = @loaiNNS)
          AND iLoai = 0) ) cc
GROUP BY cc.iID_MLSKT,cc.sKyHieu;

INSERT INTO @tblSkt (iID_MLSKT, sKyHieu, TuChi)
SELECT cc.iID_MLSKT,cc.sKyHieu,
       sum(isnull(cc.fTuChi, 0))
FROM
  (SELECT *
   FROM NS_SKT_ChungTuChiTiet_CanCu
   WHERE iNamLamViec = @NamLamViec
     AND iID_CanCu IN
       (SELECT iID_CauHinh_CanCu
        FROM NS_CauHinh_CanCu
        WHERE sModule = @sModule
          AND iID_MaChucNang = @idSoKiemTra
          AND inamLamViec = @NamLamViec
          AND iNamCancu = @NamLamViec - 1)
     AND iiID_CTSoKiemTra in
       (SELECT iID_CTSoKiemTra
        FROM NS_SKT_ChungTu
        WHERE iID_MaDonVi = @IdDonVi
          AND iNamLamViec = @NamLamViec
          AND iNamNganSach = @NamNganSach
          AND iID_MaNguonNganSach = @MaNguonNganSach
          AND iLoaiChungTu = @LoaiChungTu
		  AND (@loaiNNS = 0 OR iLoaiNguonNganSach = @loaiNNS)
          AND iLoai = 0) ) cc
GROUP BY cc.iID_MLSKT,cc.sKyHieu;


SELECT ctct.iID_MLSKT,
       ctct.sKyHieu,
       ctct.fTuChi,
       ctct.fHuyDongTonKho,
       ctct.fTonKhoDenNgay,
       ctct.fMuaHangCapHienVat,
       ctct.fPhanCap,
       ctct.sGhiChu INTO #SoNhuCauTongHop
FROM NS_SKT_ChungTuChiTiet ctct
JOIN NS_SKT_ChungTu ct ON ct.iID_CTSoKiemTra = ctct.iID_CTSoKiemTra
WHERE ct.iNamLamViec = @NamLamViec
  AND ct.iNamNganSach = @NamNganSach
  AND ct.iID_MaNguonNganSach = @MaNguonNganSach
  AND ct.iLoaiChungTu = @LoaiChungTu
  AND ct.iloai = 0 --AND ct.bKhoa = 1
  AND (@loaiNNS = 0 OR ct.iLoaiNguonNganSach = @loaiNNS)
  AND ct.iID_MaDonVi in
    (SELECT *
     FROM f_split(@IdDonVi));

SELECT iID_MLSKT,
       sKyHieu,
       sum(isnull(fTuChi,0)) fTuChi,
       sum(isnull(fHuyDongTonKho,0)) fHuyDongTonKho,
       sum(isnull(fTonKhoDenNgay,0)) fTonKhoDenNgay,
       sum(isnull(fMuaHangCapHienVat,0)) fMuaHangCapHienVat,
       sum(isnull(fPhanCap,0)) fPhanCap,
       STUFF((
			SELECT ', ' + ctct.sGhiChu
			FROM #SoNhuCauTongHop ctct
			WHERE (ctct.sKyHieu = sncTongHop.sKyHieu AND ctct.sGhiChu <> '') 
			--WHERE (ctct.iID_MLSKT = sncTongHop.iID_MLSKT AND ctct.sGhiChu <> '') 
			FOR XML PATH(''),TYPE).value('(./text())[1]','NVARCHAR(MAX)')
		  ,1,2,'') AS sGhiChu
	   
	   INTO #SoNhuCauTongHopGroup
	   FROM #SoNhuCauTongHop sncTongHop
GROUP BY iID_MLSKT, sKyHieu;

SELECT ml.sSTT STT,
	   ml.sSTTBC SSTTBC,
       ml.bHangCha,
       ml.iID_MLSKT,
       ml.iID_MLSKTCha,
       ml.sL,
       ml.sK,
       ml.sM,
       ml.sNG,
       ml.sKyHieu,
       ml.sMoTa,
       round(isnull(skt.TuChi, 0) / @dvt, 0) AS SoKiemTraNamTruoc,
       round(isnull(dt.TuChi, 0) / @dvt, 0) AS DuToanDauNam,
       round(isnull(snc.fTonKhoDenNgay, 0) / @dvt, 0) AS TonKhoDenNgay,
       round(isnull(snc.fHuyDongTonKho, 0) / @dvt, 0) AS HuyDongTonKho,
       round(isnull(snc.fTuChi, 0) / @dvt, 0) AS TuChi,
	   snc.sGhiChu
FROM NS_SKT_MucLuc ml

LEFT JOIN  #SoNhuCauTongHopGroup snc ON snc.sKyHieu = ml.sKyHieu
LEFT JOIN @tblSkt skt ON skt.sKyHieu = ml.sKyHieu
LEFT JOIN @tblDuToan dt ON dt.KyHieu = ml.sKyHieu

WHERE ml.iNamLamViec = @NamLamViec
  AND (skt.TuChi <> 0
       OR dt.TuChi <> 0
       OR snc.fTonKhoDenNgay <> 0
       OR snc.fHuyDongTonKho <> 0
       OR snc.fTuChi <> 0
       OR ISNULL(snc.sGhiChu, '') != '');

DROP TABLE #SoNhuCauTongHopGroup;
DROP TABLE #SoNhuCauTongHop;


END
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_chi_tiet_so_nhu_cau_phu_luc_4]    Script Date: 7/29/2024 1:48:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_skt_chi_tiet_so_nhu_cau_phu_luc_4]
	@LoaiChungTu int,
	@IdDonVi varchar(max),
	@NamLamViec int,
	@NamNganSach int,
	@MaNguonNganSach int,
	@dvt int,
	@loaiNNS int
AS
BEGIN
declare @sModule nvarchar(max) = 'BUDGET_DEMANDCHECK_DEMAND',
	@idDuToan nvarchar(max) = 'BUDGET_ESTIMATE',
	@idSoKiemTra nvarchar(max) = 'BUDGET_DEMANDCHECK_CHECK'
declare @tblSkt table (iID_MLSKT uniqueidentifier,sKyHieu nvarchar(200), TuChi float, MuaHangHienVat float, DacThu float)
declare @countCanCuSkt int ;
declare @countSktChiTiet int ; 

--SELECT @countSktChiTiet = count(*)
--FROM NS_SKT_ChungTuChiTiet
--WHERE iID_MaDonVi = @IdDonVi
--  AND iNamLamViec = @NamLamViec
--  AND iNamNganSach = @NamNganSach
--  AND iID_MaNguonNganSach = @MaNguonNganSach
--  AND iLoaiChungTu = @LoaiChungTu
--  AND iLoai = 0;

--SELECT @countCanCuSkt = count(*)
--FROM NS_SKT_ChungTuChiTiet_CanCu
--WHERE iNamLamViec = @NamLamViec
--  AND iID_CanCu IN
--    (SELECT iID_CauHinh_CanCu
--     FROM NS_CauHinh_CanCu
--     WHERE sModule = @sModule
--       AND iID_MaChucNang = @idSoKiemTra
--       AND inamLamViec = @NamLamViec
--       AND iNamCancu = @NamLamViec - 1)
--  AND iiID_CTSoKiemTra in
--    (SELECT iID_CTSoKiemTra
--     FROM NS_SKT_ChungTu
--     WHERE iID_MaDonVi = @IdDonVi
--       AND iNamLamViec = @NamLamViec
--       AND iNamNganSach = @NamNganSach
--       AND iID_MaNguonNganSach = @MaNguonNganSach
--       AND iLoaiChungTu = @LoaiChungTu
--       AND iLoai = 0);

--IF (@countCanCuSkt = 0 and @countSktChiTiet = 0)
--INSERT INTO @tblSkt (iID_MLSKT, sKyHieu,TuChi, MuaHangHienVat, DacThu)
--SELECT ctct.iID_MLSKT,
--       ctct.sKyHieu,
--       ctct.fTuChi,
--       ctct.fMuaHangCapHienVat,
--       ctct.fPhanCap
--FROM NS_SKT_ChungTuChiTiet ctct
--JOIN NS_SKT_ChungTu ct ON ct.iID_CTSoKiemTra = ctct.iID_CTSoKiemTra
--WHERE ct.iNamLamViec = @NamLamViec - 1
--  AND ct.iNamNganSach = @NamNganSach
--  AND ct.iID_MaNguonNganSach = @MaNguonNganSach
--  AND ct.iLoaiChungTu = @LoaiChungTu
--  AND ct.iloai = 3
--  --AND ct.iID_MaDonVi = @IdDonVi
--  AND (ctct.iLoai = 3 and exists(select 1 from DonVi where iID_MaDonVi = @IdDonVi and iLoai = 0) or ctct.iLoai = 4)
--  AND ctct.iID_MaDonVi = @IdDonVi
--ELSE
INSERT INTO @tblSkt (iID_MLSKT,sKyHieu, TuChi, MuaHangHienVat, DacThu)
SELECT cc.iID_MLSKT, sKyHieu,sum(isnull(cc.fTuChi,0)), sum(isnull(cc.fMuaHangCapHienVat,0)), sum(isnull(cc.fPhanCap,0))
FROM 
	(SELECT *
FROM NS_SKT_ChungTuChiTiet_CanCu
WHERE iNamLamViec = @NamLamViec
  AND iID_CanCu IN
    (SELECT iID_CauHinh_CanCu
     FROM NS_CauHinh_CanCu
     WHERE sModule = @sModule
       AND iID_MaChucNang = @idSoKiemTra
       AND inamLamViec = @NamLamViec
	   AND iNamCancu = @NamLamViec - 1)
  AND iiID_CTSoKiemTra in
    (SELECT iID_CTSoKiemTra
     FROM NS_SKT_ChungTu
     WHERE iID_MaDonVi = @IdDonVi
       AND iNamLamViec = @NamLamViec
       AND iNamNganSach = @NamNganSach
       AND iID_MaNguonNganSach = @MaNguonNganSach
       AND iLoaiChungTu = @LoaiChungTu
	   AND (@loaiNNS = 0 OR iLoaiNguonNganSach = @loaiNNS)
       AND iLoai = 0)
	) cc
GROUP BY cc.iID_MLSKT, sKyHieu;


SELECT ctct.iID_MLSKT,
       ctct.sKyHieu,
       ctct.fTuChi,
       ctct.fHuyDongTonKho,
       ctct.fTonKhoDenNgay,
       ctct.fMuaHangCapHienVat,
       ctct.fKhungNganSachDuocDuyet ,
       ctct.fSoNganhPhanCap,
       ctct.fPhanCap as fDacThu,
       ctct.sGhiChu INTO #SoNhuCauTongHop
FROM NS_SKT_ChungTuChiTiet ctct
JOIN NS_SKT_ChungTu ct ON ct.iID_CTSoKiemTra = ctct.iID_CTSoKiemTra
WHERE ct.iNamLamViec = @NamLamViec
  AND ct.iNamNganSach = @NamNganSach
  AND ct.iID_MaNguonNganSach = @MaNguonNganSach
  AND ct.iLoaiChungTu = @LoaiChungTu
  AND ct.iloai = 0
  AND (@loaiNNS = 0 OR ct.iLoaiNguonNganSach = @loaiNNS)
  --AND ct.bKhoa = 1
  AND ct.iID_MaDonVi = @IdDonVi;


SELECT ml.sSTT STT,
	   ml.sSTTBC SSTTBC,
       ml.bHangCha,
	   ml.iID_MLSKT,
	   ml.iID_MLSKTCha,
	   ml.sL,
	   ml.sK,
	   ml.sM,
	   ml.sNG,
	   ml.sKyHieu,
	   ml.sMoTa,
       round(isnull(skt.DacThu,0) / @dvt, 0) AS SoKiemTraDacThuNamTruoc ,
       round(isnull(snc.fDacThu,0) / @dvt, 0) AS DacThu,
       round(isnull(snc.fSoNganhPhanCap,0) / @dvt, 0) AS SoNganhPhanCap,
       round(isnull(snc.fKhungNganSachDuocDuyet,0) / @dvt, 0) AS KhungNganSachDuocDuyet,
	   snc.sGhiChu
FROM NS_SKT_MucLuc ml
LEFT JOIN #SoNhuCauTongHop snc on snc.sKyHieu = ml.sKyHieu 
LEFT JOIN @tblSkt skt on skt.sKyHieu = ml.sKyHieu
WHERE ml.iNamLamViec = @NamLamViec
and (skt.DacThu <> 0 or snc.fDacThu <> 0 or (snc.sGhiChu is not null and snc.sGhiChu != ''));

DROP TABLE #SoNhuCauTongHop;
END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_chi_tiet_so_nhu_cau_phu_luc_5]    Script Date: 7/29/2024 1:48:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_skt_chi_tiet_so_nhu_cau_phu_luc_5]
	@LoaiChungTu int,
	@IdDonVi varchar(max),
	@NamLamViec int,
	@NamNganSach int,
	@MaNguonNganSach int,
	@dvt int,
	@loaiNNS int
AS
BEGIN
declare @sModule nvarchar(max) = 'BUDGET_DEMANDCHECK_DEMAND',
	@idDuToan nvarchar(max) = 'BUDGET_ESTIMATE',
	@idSoKiemTra nvarchar(max) = 'BUDGET_DEMANDCHECK_CHECK'
declare @tblDuToan table (iID_MLSKT uniqueidentifier, KyHieu nvarchar(200),  MuaHangHienVat float, DacThu float)
declare @tblSkt table (iID_MLSKT uniqueidentifier,KyHieu nvarchar(200), TuChi float, MuaHangHienVat float, DacThu float)
declare @countCanCuDuToan int;
declare @countCanCuSkt int ;
declare @countSktChiTiet int ; 
--count chi tiet
--SELECT @countSktChiTiet = count(*) 
--	FROM NS_SKT_ChungTuChiTiet
--	WHERE 
--		iID_MaDonVi = @IdDonVi 
--		AND iNamLamViec = @NamLamViec
--		AND iNamNganSach = @NamNganSach
--		AND iID_MaNguonNganSach = @MaNguonNganSach
--		AND iLoaiChungTu = @LoaiChungTu
--		AND iLoai = 0;

---- count can cu du toan
--SELECT @countCanCuDuToan = count(*) 
--	FROM NS_SKT_ChungTuChiTiet_CanCu 
--	WHERE 
--		iNamLamViec = @NamLamViec
--		AND iID_CanCu IN (SELECT iID_CauHinh_CanCu 
--						FROM NS_CauHinh_CanCu 
--						WHERE sModule = @sModule 
--							AND iID_MaChucNang = @idDuToan 
--							AND inamLamViec = @NamLamViec
--							AND iNamCancu = @NamLamViec - 1)
--		AND iiID_CTSoKiemTra in 
--		(SELECT iID_CTSoKiemTra
--			FROM NS_SKT_ChungTu
--		WHERE 
--		iID_MaDonVi = @IdDonVi 
--		AND iNamLamViec = @NamLamViec
--		AND iNamNganSach = @NamNganSach
--		AND iID_MaNguonNganSach = @MaNguonNganSach
--		AND iLoaiChungTu = @LoaiChungTu
--		AND iLoai = 0);

---- count can cu so kiem tra
--SELECT @countCanCuSkt = count(*) 
--	FROM NS_SKT_ChungTuChiTiet_CanCu 
--	WHERE 
--		iNamLamViec = @NamLamViec
--		AND iID_CanCu IN (SELECT iID_CauHinh_CanCu 
--						FROM NS_CauHinh_CanCu 
--						WHERE sModule = @sModule 
--							AND iID_MaChucNang = @idSoKiemTra 
--							AND inamLamViec = @NamLamViec
--							AND iNamCancu = @NamLamViec - 1)
--		AND iiID_CTSoKiemTra in 
--		(SELECT iID_CTSoKiemTra
--			FROM NS_SKT_ChungTu
--		WHERE 
--		iID_MaDonVi = @IdDonVi 
--		AND iNamLamViec = @NamLamViec
--		AND iNamNganSach = @NamNganSach
--		AND iID_MaNguonNganSach = @MaNguonNganSach
--		AND iLoaiChungTu = @LoaiChungTu
--		AND iLoai = 0);


--IF (@countSktChiTiet = 0 AND @countCanCuDuToan = 0 AND @countCanCuSkt = 0)
--INSERT INTO @tblDuToan (iID_MLSKT, KyHieu,  MuaHangHienVat, DacThu)
--SELECT 
--       null,
--       mp.sSKT_KyHieu KyHieu,
--       Sum(HangNhap) + Sum(HangMua) + Sum(TuChi) MuaHangHienVat,
--       Sum(DacThu) DacThu
--FROM NS_MLSKT_MLNS mp
--JOIN
--  (SELECT SXauNoiMa,
--          sum(fTuChi) TuChi,
--          sum(fHangNhap) HangNhap,
--          sum(fHangMua) HangMua,
--          sum(fPhanCap) PhanCap,
--          sum(fTuChi) MuaHangHienVat,
--          sum(fTuChi) DacThu
--   FROM NS_DT_ChungTuChiTiet
--   WHERE iID_DTChungTu in
--       (SELECT iID_DTChungTu
--        FROM NS_DT_ChungTu
--        WHERE iNamLamViec = @NamLamViec - 1
--          AND iNamNganSach = @NamNganSach
--          AND iID_MaNguonNganSach = @MaNguonNganSach
--		  AND bKhoa = 1
--          AND iLoaiChungTu = @LoaiChungTu
--          AND (iloai = 0 OR iLoai = 1)
--          AND iLoaiDuToan = 1 )
--   AND iID_MaDonVi = @IdDonVi
--   GROUP BY SXauNoiMa) dtct ON mp.sNS_XauNoiMa = dtct.SXauNoiMa
--WHERE mp.iNamLamViec = @NamLamViec
--GROUP BY mp.sSKT_KyHieu
--ELSE
INSERT INTO @tblDuToan (iID_MLSKT, KyHieu, MuaHangHienVat, DacThu)
SELECT cc.iID_MLSKT, sKyHieu,  sum(isnull(cc.fMuaHangCapHienVat,0)), sum(isnull(cc.fPhanCap,0))
FROM 
	(SELECT * 
	FROM NS_SKT_ChungTuChiTiet_CanCu 
	WHERE 
		iNamLamViec = @NamLamViec
		AND iID_CanCu IN (SELECT iID_CauHinh_CanCu 
						FROM NS_CauHinh_CanCu 
						WHERE sModule = @sModule 
							AND iID_MaChucNang = @idDuToan 
							AND inamLamViec = @NamLamViec
							AND iNamCancu = @NamLamViec - 1)
			   		AND iiID_CTSoKiemTra in 
		(SELECT iID_CTSoKiemTra
			FROM NS_SKT_ChungTu
		WHERE 
		iID_MaDonVi = @IdDonVi 
		AND iNamLamViec = @NamLamViec
		AND iNamNganSach = @NamNganSach
		AND (@loaiNNS = 0 OR iLoaiNguonNganSach = @loaiNNS)
		AND iID_MaNguonNganSach = @MaNguonNganSach
		AND iLoaiChungTu = @LoaiChungTu
		AND iLoai = 0)
	) cc
GROUP BY cc.iID_MLSKT, sKyHieu;


--IF (@countSktChiTiet = 0 AND @countCanCuDuToan = 0 AND @countCanCuSkt = 0)
--INSERT INTO @tblSkt (iID_MLSKT, KyHieu, TuChi, MuaHangHienVat, DacThu)
--SELECT ctct.iID_MLSKT,sKyHieu,
--       ctct.fTuChi,
--       ctct.fMuaHangCapHienVat,
--       ctct.fPhanCap
--FROM NS_SKT_ChungTuChiTiet ctct
--JOIN NS_SKT_ChungTu ct ON ct.iID_CTSoKiemTra = ctct.iID_CTSoKiemTra
--WHERE ct.iNamLamViec = @NamLamViec - 1
--  AND ct.iNamNganSach = @NamNganSach
--  AND ct.iID_MaNguonNganSach = @MaNguonNganSach
--  AND ct.iLoaiChungTu = @LoaiChungTu
--  AND ct.iloai = 3
--  --AND ct.iID_MaDonVi = @IdDonVi
--  AND (ctct.iLoai = 3 and exists(select 1 from DonVi where iID_MaDonVi = @IdDonVi and iLoai = 0) or ctct.iLoai = 4)
--  AND ctct.iID_MaDonVi = @IdDonVi
--ELSE
INSERT INTO @tblSkt (iID_MLSKT,KyHieu, TuChi, MuaHangHienVat, DacThu)
SELECT cc.iID_MLSKT, sKyHieu, sum(isnull(cc.fTuChi,0)), sum(isnull(cc.fMuaHangCapHienVat,0)), sum(isnull(cc.fPhanCap,0))
FROM 
	(SELECT * 
	FROM NS_SKT_ChungTuChiTiet_CanCu 
	WHERE 
		iNamLamViec = @NamLamViec
		AND iID_CanCu IN (SELECT iID_CauHinh_CanCu 
						FROM NS_CauHinh_CanCu 
						WHERE sModule = @sModule 
							AND iID_MaChucNang = @idSoKiemTra 
							AND inamLamViec = @NamLamViec
							AND iNamCancu = @NamLamViec - 1)
		AND iiID_CTSoKiemTra in 
		(SELECT iID_CTSoKiemTra
			FROM NS_SKT_ChungTu
		WHERE 
		iID_MaDonVi = @IdDonVi 
		AND iNamLamViec = @NamLamViec
		AND iNamNganSach = @NamNganSach
		AND (@loaiNNS = 0 OR iLoaiNguonNganSach = @loaiNNS)
		AND iID_MaNguonNganSach = @MaNguonNganSach
		AND iLoaiChungTu = @LoaiChungTu
		AND iLoai = 0)
	) cc
GROUP BY cc.iID_MLSKT, sKyHieu;

SELECT ctct.iID_MLSKT,
       ctct.sKyHieu,
       ctct.fTuChi,
       ctct.fHuyDongTonKho,
       ctct.fTonKhoDenNgay,
       ctct.fMuaHangCapHienVat,
       ctct.fPhanCap,
       ctct.sGhiChu INTO #SoNhuCauTongHop
FROM NS_SKT_ChungTuChiTiet ctct
JOIN NS_SKT_ChungTu ct ON ct.iID_CTSoKiemTra = ctct.iID_CTSoKiemTra
WHERE ct.iNamLamViec = @NamLamViec
  AND ct.iNamNganSach = @NamNganSach
  AND ct.iID_MaNguonNganSach = @MaNguonNganSach
  AND ct.iLoaiChungTu = @LoaiChungTu
  AND ct.iloai = 0
  AND (@loaiNNS = 0 OR ct.iLoaiNguonNganSach = @loaiNNS)
  --AND ct.bKhoa = 1
  AND ct.iID_MaDonVi = @IdDonVi;

SELECT ml.sSTT STT,
	   ml.sSTTBC SSTTBC,
       ml.bHangCha,
	   ml.iID_MLSKT,
	   ml.iID_MLSKTCha,
	   ml.sL,
	   ml.sK,
	   ml.sM,
	   ml.sNG,
	   ml.sKyHieu,
	   ml.sMoTa,
       round(isnull(skt.MuaHangHienVat,0) / @dvt, 0) AS SoKiemTraMHHVNamTruoc ,
       round(isnull(dt.MuaHangHienVat,0) / @dvt, 0) AS DuToanDauNam ,
       round(isnull(snc.fHuyDongTonKho,0) / @dvt, 0) AS HuyDongTonKho,
       round(isnull(snc.fTonKhoDenNgay,0) / @dvt, 0) AS TonKhoDenNgay,
       round(isnull(snc.fMuaHangCapHienVat,0) / @dvt, 0) AS MuaHangCapHienVat,
	   snc.sGhiChu
FROM NS_SKT_MucLuc ml
LEFT JOIN #SoNhuCauTongHop snc on snc.sKyHieu = ml.sKyHieu
LEFT JOIN @tblSkt skt on skt.KyHieu = ml.sKyHieu
LEFT JOIN @tblDuToan dt on  dt.KyHieu = ml.sKyHieu
WHERE ml.iNamLamViec = @NamLamViec
and (skt.MuaHangHienVat <> 0 or dt.MuaHangHienVat <> 0 or snc.fHuyDongTonKho <> 0 or snc.fTonKhoDenNgay <> 0 or snc.fMuaHangCapHienVat <> 0 or (snc.sGhiChu is not null and snc.sGhiChu != ''));

DROP TABLE #SoNhuCauTongHop;
END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_so_nhu_cau_phu_luc_3]    Script Date: 7/29/2024 1:48:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_skt_tong_hop_so_nhu_cau_phu_luc_3]
	@LoaiChungTu int,
	@IdDonVi nvarchar(max),
	@NamLamViec int,
	@NamNganSach int,
	@MaNguonNganSach int,
	@MaBQuanLy varchar(max),
	@dvt int,
	@loaiNNS int
AS
BEGIN
declare @countDonVi int;
SELECT @countDonVi = count(*) FROM f_split(@IdDonVi);
declare @sModule nvarchar(max) = 'BUDGET_DEMANDCHECK_DEMAND',
	@idDuToan nvarchar(max) = 'BUDGET_ESTIMATE',
	@idSoKiemTra nvarchar(max) = 'BUDGET_DEMANDCHECK_CHECK'
declare @tblDuToan table (iID_MLSKT uniqueidentifier, KyHieu nvarchar(200), TuChi float)
declare @tblSkt table (iID_MLSKT uniqueidentifier, KyHieu nvarchar(200), TuChi float)
declare @countCanCuDuToan int;
declare @countCanCuSkt int ;
declare @countSktChiTiet int ; 


INSERT INTO @tblDuToan (iID_MLSKT, KyHieu, TuChi)
SELECT cc.iID_MLSKT, sKyHieu, sum(isnull(cc.fTuChi,0))
FROM 
	(SELECT * 
	FROM NS_SKT_ChungTuChiTiet_CanCu 
	WHERE 
		iNamLamViec = @NamLamViec
		AND iID_CanCu IN (SELECT iID_CauHinh_CanCu 
						FROM NS_CauHinh_CanCu 
						WHERE sModule = @sModule 
							AND iID_MaChucNang = @idDuToan 
							AND inamLamViec = @NamLamViec
							AND iNamCancu = @NamLamViec - 1)
			AND iiID_CTSoKiemTra in 
		(SELECT iID_CTSoKiemTra
			FROM NS_SKT_ChungTu
		WHERE 
		iID_MaDonVi in (select * from f_split(@IdDonVi))
		AND iNamLamViec = @NamLamViec
		AND iNamNganSach = @NamNganSach
		AND iID_MaNguonNganSach = @MaNguonNganSach
		AND iLoaiChungTu = @LoaiChungTu
		AND (@loaiNNS = 0 OR iLoaiNguonNganSach = @loaiNNS)
		AND iLoai = 0)
	) cc
GROUP BY cc.iID_MLSKT,sKyHieu;

INSERT INTO @tblSkt (iID_MLSKT,KyHieu, TuChi)
SELECT cc.iID_MLSKT, cc.sKyHieu, sum(isnull(cc.fTuChi,0))
FROM 
	(SELECT * 
	FROM NS_SKT_ChungTuChiTiet_CanCu 
	WHERE 
		iNamLamViec = @NamLamViec
		AND iID_CanCu IN (SELECT iID_CauHinh_CanCu 
						FROM NS_CauHinh_CanCu 
						WHERE sModule = @sModule 
							AND iID_MaChucNang = @idSoKiemTra 
							AND inamLamViec = @NamLamViec
							AND iNamCancu = @NamLamViec - 1)
	   	AND iiID_CTSoKiemTra in 
		(SELECT iID_CTSoKiemTra
			FROM NS_SKT_ChungTu
		WHERE 
		iID_MaDonVi in (select * from f_split(@IdDonVi))
		AND iNamLamViec = @NamLamViec
		AND iNamNganSach = @NamNganSach
		AND iID_MaNguonNganSach = @MaNguonNganSach
		AND iLoaiChungTu = @LoaiChungTu
		AND (@loaiNNS = 0 OR iLoaiNguonNganSach = @loaiNNS)
		AND iLoai = 0)
	) cc
GROUP BY cc.iID_MLSKT,cc.sKyHieu;


SELECT ctct.iID_MLSKT,
       ctct.sKyHieu,
       isnull(ctct.fTuChi,0) fTuChi,
       isnull(ctct.fHuyDongTonKho,0) fHuyDongTonKho,
       isnull(ctct.fTonKhoDenNgay,0) fTonKhoDenNgay,
       isnull(ctct.fMuaHangCapHienVat,0) fMuaHangCapHienVat,
       isnull(ctct.fPhanCap,0) fPhanCap,
	   ctct.sGhiChu
       --case when @countDonVi = 1 then ctct.sGhiChu else '' end as sGhiChu
	   INTO #sncChiTiet
FROM NS_SKT_ChungTuChiTiet ctct
JOIN NS_SKT_ChungTu ct ON ct.iID_CTSoKiemTra = ctct.iID_CTSoKiemTra
WHERE ct.iNamLamViec = @NamLamViec
  AND ct.iNamNganSach = @NamNganSach
  AND ct.iID_MaNguonNganSach = @MaNguonNganSach
  AND ct.iLoaiChungTu = @LoaiChungTu
  AND ct.iloai = 0
  AND (@loaiNNS = 0 OR ct.iLoaiNguonNganSach = @loaiNNS)
  --AND ct.bKhoa = 1
  AND ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVi))


select iID_MLSKT,
       sKyHieu,
       sum(isnull(fTuChi,0)) fTuChi,
       sum(isnull(fHuyDongTonKho,0)) fHuyDongTonKho,
       sum(isnull(fTonKhoDenNgay,0)) fTonKhoDenNgay,
       sum(isnull(fMuaHangCapHienVat,0)) fMuaHangCapHienVat,
       sum(isnull(fPhanCap,0)) fPhanCap,
       STUFF((
			SELECT ', ' + ctct.sGhiChu
			FROM #sncChiTiet ctct
			WHERE (ctct.sKyHieu = sncChiTiet.sKyHieu AND ctct.sGhiChu <> '') 
			FOR XML PATH(''),TYPE).value('(./text())[1]','NVARCHAR(MAX)')
		  ,1,2,'') AS sGhiChu
	   
	   INTO #SoNhuCauTongHop 
	   from #sncChiTiet sncChiTiet
GROUP BY iID_MLSKT, sKyHieu;


SELECT map.sSKT_KyHieu INTO #KyHieuSktBQuanLy
FROM NS_MLSKT_MLNS MAP
JOIN NS_MucLucNganSach mlns ON mlns.sXauNoiMa = map.sNS_XauNoiMa
AND map.iNamLamViec = mlns.iNamLamViec
WHERE map.iNamLamViec = @NamLamViec
AND sLNS in (select sLNS from NS_MucLucNganSach where iNamLamViec = @NamLamViec and iID_MaBQuanLy = @MaBQuanLy);

SELECT ml.sSTT STT,
	   ml.sSTTBC SSTTBC,
       ml.bHangCha,
	   ml.iID_MLSKT,
	   ml.iID_MLSKTCha,
	   ml.sL,
	   ml.sK,
	   ml.sM,
	   ml.sNG,
	   ml.sKyHieu,
	   ml.sMoTa,
       round(isnull(skt.TuChi,0) / @dvt, 0) AS SoKiemTraNamTruoc ,
       round(isnull(dt.TuChi,0) / @dvt, 0) AS DuToanDauNam ,
       round(isnull(snc.fTonKhoDenNgay,0) / @dvt, 0) AS TonKhoDenNgay,
       round(isnull(snc.fHuyDongTonKho,0) / @dvt, 0) AS HuyDongTonKho,
       round(isnull(snc.fTuChi,0) / @dvt, 0) AS TuChi,
	   snc.sGhiChu
FROM NS_SKT_MucLuc ml
LEFT JOIN #SoNhuCauTongHop snc on snc.sKyHieu = ml.sKyHieu
LEFT JOIN @tblSkt skt on skt.KyHieu = ml.sKyHieu
LEFT JOIN @tblDuToan dt on dt.KyHieu = ml.sKyHieu
WHERE ml.iNamLamViec = @NamLamViec
AND (@MaBQuanLy = '0' OR ml.sKyHieu in (SELECT * FROM #KyHieuSktBQuanLy))
AND (skt.TuChi <> 0 OR dt.TuChi <> 0 OR snc.fTonKhoDenNgay <> 0 OR snc.fHuyDongTonKho <> 0 OR snc.fTuChi <> 0 OR (snc.sGhiChu is not null and snc.sGhiChu != ''));

DROP TABLE #SoNhuCauTongHop;
DROP TABLE #KyHieuSktBQuanLy;
DROP TABLE #sncChiTiet;

END
;
;
;
;
;
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_so_nhu_cau_phu_luc_4]    Script Date: 7/29/2024 1:48:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_skt_tong_hop_so_nhu_cau_phu_luc_4]
	@LoaiChungTu int,
	@IdDonVi nvarchar(max),
	@NamLamViec int,
	@NamNganSach int,
	@MaNguonNganSach int,
	@MaBQuanLy varchar(max),
	@dvt int,
	@loaiNNS int
AS
BEGIN
declare @countDonVi int;
SELECT @countDonVi = count(*) FROM f_split(@IdDonVi);
declare @sModule nvarchar(max) = 'BUDGET_DEMANDCHECK_DEMAND',
	@idDuToan nvarchar(max) = 'BUDGET_ESTIMATE',
	@idSoKiemTra nvarchar(max) = 'BUDGET_DEMANDCHECK_CHECK'
declare @tblSkt table (iID_MLSKT uniqueidentifier, KyHieu nvarchar(200), MuaHangHienVat float, DacThu float)
declare @countCanCuSkt int ;
declare @countSktChiTiet int ; 

INSERT INTO @tblSkt (iID_MLSKT, KyHieu, MuaHangHienVat, DacThu)
SELECT cc.iID_MLSKT, sKyHieu, sum(isnull(cc.fMuaHangCapHienVat,0)), sum(isnull(cc.fPhanCap,0))
FROM 
	(SELECT * 
	FROM NS_SKT_ChungTuChiTiet_CanCu 
	WHERE 
		iNamLamViec = @NamLamViec
		AND iID_CanCu IN (SELECT iID_CauHinh_CanCu 
						FROM NS_CauHinh_CanCu 
						WHERE sModule = @sModule 
							AND iID_MaChucNang = @idSoKiemTra 
							AND inamLamViec = @NamLamViec
							AND iNamCancu = @NamLamViec - 1)
		AND iiID_CTSoKiemTra in 
		(SELECT iID_CTSoKiemTra
			FROM NS_SKT_ChungTu
		WHERE 
		iID_MaDonVi in  (SELECT * FROM f_split(@IdDonVi))
		AND iNamLamViec = @NamLamViec
		AND iNamNganSach = @NamNganSach
		AND iID_MaNguonNganSach = @MaNguonNganSach
		AND iLoaiChungTu = @LoaiChungTu
		AND (@loaiNNS = 0 OR iLoaiNguonNganSach = @loaiNNS)
		AND iLoai = 0)
	) cc
GROUP BY cc.iID_MLSKT,sKyHieu;


select iID_MLSKT,
       sKyHieu,
       sum(isnull(fTuChi,0)) fTuChi,
       sum(isnull(fHuyDongTonKho,0)) fHuyDongTonKho,
       sum(isnull(fTonKhoDenNgay,0)) fTonKhoDenNgay,
       sum(isnull(fMuaHangCapHienVat,0)) fMuaHangCapHienVat,
       sum(isnull(fKhungNganSachDuocDuyet,0)) fKhungNganSachDuocDuyet,
       sum(isnull(fSoNganhPhanCap,0)) fSoNganhPhanCap,
       sum(isnull(fDacThu,0)) fDacThu,
       sGhiChu INTO #SoNhuCauTongHop 
	   from 
(SELECT ctct.iID_MLSKT,
       ctct.sKyHieu,
       isnull(ctct.fTuChi,0) fTuChi,
       isnull(ctct.fHuyDongTonKho,0) fHuyDongTonKho,
       isnull(ctct.fTonKhoDenNgay,0) fTonKhoDenNgay,
       isnull(ctct.fMuaHangCapHienVat,0) fMuaHangCapHienVat,
       isnull(ctct.fPhanCap,0) fDacThu,
       isnull(ctct.fKhungNganSachDuocDuyet,0) fKhungNganSachDuocDuyet,
       isnull(ctct.fSoNganhPhanCap,0) fSoNganhPhanCap,
       case when @countDonVi = 1 then ctct.sGhiChu else '' end as sGhiChu
FROM NS_SKT_ChungTuChiTiet ctct
JOIN NS_SKT_ChungTu ct ON ct.iID_CTSoKiemTra = ctct.iID_CTSoKiemTra
WHERE ct.iNamLamViec = @NamLamViec
  AND ct.iNamNganSach = @NamNganSach
  AND ct.iID_MaNguonNganSach = @MaNguonNganSach
  AND ct.iLoaiChungTu = @LoaiChungTu
  AND (@loaiNNS = 0 OR ct.iLoaiNguonNganSach = @loaiNNS)
  AND ct.iloai = 0
  --AND ct.bKhoa = 1
  AND ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVi))
  ) as sncChiTiet
GROUP BY iID_MLSKT, sKyHieu, sGhiChu;

  SELECT map.sSKT_KyHieu INTO #KyHieuSktBQuanLy
FROM NS_MLSKT_MLNS MAP
JOIN NS_MucLucNganSach mlns ON mlns.sXauNoiMa = map.sNS_XauNoiMa
AND map.iNamLamViec = mlns.iNamLamViec
WHERE map.iNamLamViec = @NamLamViec
AND mlns.iNamLamViec = @NamLamViec and mlns.iID_MaBQuanLy = @MaBQuanLy;

SELECT ml.sSTT STT,
	   ml.sSTTBC SSTTBC,
       ml.bHangCha,
	   ml.iID_MLSKT,
	   ml.iID_MLSKTCha,
	   ml.sL,
	   ml.sK,
	   ml.sM,
	   ml.sNG,
	   ml.sKyHieu,
	   ml.sMoTa,
       round(isnull(skt.DacThu,0) / @dvt, 0) AS SoKiemTraDacThuNamTruoc ,
       round(isnull(snc.fDacThu,0) / @dvt, 0) AS DacThu,
       round(isnull(snc.fSoNganhPhanCap,0) / @dvt, 0) AS SoNganhPhanCap,
       round(isnull(snc.fKhungNganSachDuocDuyet,0) / @dvt, 0) AS KhungNganSachDuocDuyet,
	   snc.sGhiChu
FROM NS_SKT_MucLuc ml
LEFT JOIN #SoNhuCauTongHop snc on snc.sKyHieu = ml.sKyHieu 
LEFT JOIN @tblSkt skt on skt.KyHieu = ml.sKyHieu 
WHERE ml.iNamLamViec = @NamLamViec
AND (@MaBQuanLy = '0' OR ml.sKyHieu in (SELECT * FROM #KyHieuSktBQuanLy))
and (skt.DacThu <> 0 or snc.fDacThu <> 0 or (snc.sGhiChu is not null and snc.sGhiChu != ''));

DROP TABLE #SoNhuCauTongHop;
DROP TABLE #KyHieuSktBQuanLy;
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
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_so_nhu_cau_phu_luc_5]    Script Date: 7/29/2024 1:48:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_skt_tong_hop_so_nhu_cau_phu_luc_5]
	@LoaiChungTu int,
	@IdDonVi nvarchar(max),
	@NamLamViec int,
	@NamNganSach int,
	@MaNguoiNganSach int,
	@MaBQuanLy varchar(max),
	@dvt int,
	@loaiNNS int
AS
BEGIN
declare @sModule nvarchar(max) = 'BUDGET_DEMANDCHECK_DEMAND',
	@idDuToan nvarchar(max) = 'BUDGET_ESTIMATE',
	@idSoKiemTra nvarchar(max) = 'BUDGET_DEMANDCHECK_CHECK'
declare @tblDuToan table (iID_MLSKT uniqueidentifier, KyHieu nvarchar(200), TuChi float, MuaHangHienVat float, DacThu float)
declare @tblSkt table (iID_MLSKT uniqueidentifier, KyHieu nvarchar(200), TuChi float, MuaHangHienVat float, DacThu float)
declare @countCanCuDuToan int;
declare @countCanCuSkt int ;
declare @countSktChiTiet int ; 
declare @countDonVi int;
SELECT @countDonVi = count(*) FROM f_split(@IdDonVi);


INSERT INTO @tblDuToan (iID_MLSKT, KyHieu, TuChi, MuaHangHienVat, DacThu)
SELECT cc.iID_MLSKT, sKyHieu, sum(isnull(cc.fTuChi,0)), sum(isnull(cc.fMuaHangCapHienVat,0)), sum(isnull(cc.fPhanCap,0))
FROM 
	(SELECT * 
	FROM NS_SKT_ChungTuChiTiet_CanCu 
	WHERE 
		iNamLamViec = @NamLamViec
		AND iID_CanCu IN (SELECT iID_CauHinh_CanCu 
						FROM NS_CauHinh_CanCu 
						WHERE sModule = @sModule 
							AND iID_MaChucNang = @idDuToan 
							AND inamLamViec = @NamLamViec
							AND iNamCancu = @NamLamViec - 1)
		AND iiID_CTSoKiemTra in 
		(SELECT iID_CTSoKiemTra
			FROM NS_SKT_ChungTu
		WHERE 
		iID_MaDonVi in (select * from f_split(@IdDonVi)) 
		AND iNamLamViec = @NamLamViec
		AND iNamNganSach = @NamNganSach
		AND iID_MaNguonNganSach = @MaNguoiNganSach
		AND iLoaiChungTu = @LoaiChungTu
		AND (@loaiNNS = 0 OR iLoaiNguonNganSach = @loaiNNS)
		AND iLoai = 0)
	) cc
GROUP BY cc.iID_MLSKT, sKyHieu;


INSERT INTO @tblSkt (iID_MLSKT,KyHieu, TuChi, MuaHangHienVat, DacThu)
SELECT cc.iID_MLSKT, cc.sKyHieu, sum(isnull(cc.fTuChi,0)), sum(isnull(cc.fMuaHangCapHienVat,0)), sum(isnull(cc.fPhanCap,0))
FROM 
	(SELECT * 
	FROM NS_SKT_ChungTuChiTiet_CanCu 
	WHERE 
		iNamLamViec = @NamLamViec
		AND iID_CanCu IN (SELECT iID_CauHinh_CanCu 
						FROM NS_CauHinh_CanCu 
						WHERE sModule = @sModule 
							AND iID_MaChucNang = @idSoKiemTra 
							AND inamLamViec = @NamLamViec
							AND iNamCancu = @NamLamViec - 1)
		AND iiID_CTSoKiemTra in 
		(SELECT iID_CTSoKiemTra
			FROM NS_SKT_ChungTu
		WHERE 
		iID_MaDonVi in (select * from f_split(@IdDonVi)) 
		AND iNamLamViec = @NamLamViec
		AND iNamNganSach = @NamNganSach
		AND iID_MaNguonNganSach = @MaNguoiNganSach
		AND iLoaiChungTu = @LoaiChungTu
		AND (@loaiNNS = 0 OR iLoaiNguonNganSach = @loaiNNS)
		AND iLoai = 0)
	) cc
GROUP BY cc.iID_MLSKT, cc.sKyHieu;

select iID_MLSKT,
       sKyHieu,
       sum(isnull(fTuChi,0)) fTuChi,
       sum(isnull(fHuyDongTonKho,0)) fHuyDongTonKho,
       sum(isnull(fTonKhoDenNgay,0)) fTonKhoDenNgay,
       sum(isnull(fMuaHangCapHienVat,0)) fMuaHangCapHienVat,
       sum(isnull(fPhanCap,0)) fPhanCap,
       sGhiChu INTO #SoNhuCauTongHop 
	   from 
(SELECT ctct.iID_MLSKT,
       ctct.sKyHieu,
       isnull(ctct.fTuChi,0) fTuChi,
       isnull(ctct.fHuyDongTonKho,0) fHuyDongTonKho,
       isnull(ctct.fTonKhoDenNgay,0) fTonKhoDenNgay,
       isnull(ctct.fMuaHangCapHienVat,0) fMuaHangCapHienVat,
       isnull(ctct.fPhanCap,0) fPhanCap,
       case when @countDonVi = 1 then ctct.sGhiChu else '' end as sGhiChu
FROM NS_SKT_ChungTuChiTiet ctct
JOIN NS_SKT_ChungTu ct ON ct.iID_CTSoKiemTra = ctct.iID_CTSoKiemTra
WHERE ct.iNamLamViec = @NamLamViec
  AND ct.iNamNganSach = @NamNganSach
  AND ct.iID_MaNguonNganSach = @MaNguoiNganSach
  AND ct.iLoaiChungTu = @LoaiChungTu
  AND ct.iloai = 0
  AND (@loaiNNS = 0 OR ct.iLoaiNguonNganSach = @loaiNNS)
  --AND ct.bKhoa = 1
  AND ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVi))
  ) as sncChiTiet
GROUP BY iID_MLSKT, sKyHieu, sGhiChu;

  SELECT map.sSKT_KyHieu INTO #KyHieuSktBQuanLy
FROM NS_MLSKT_MLNS MAP
JOIN NS_MucLucNganSach mlns ON mlns.sXauNoiMa = map.sNS_XauNoiMa
AND map.iNamLamViec = mlns.iNamLamViec
WHERE map.iNamLamViec = @NamLamViec
AND mlns.iNamLamViec = @NamLamViec and mlns.iID_MaBQuanLy = @MaBQuanLy;

SELECT ml.sSTT STT,
	   ml.sSTTBC SSTTBC,
       ml.bHangCha,
	   ml.iID_MLSKT,
	   ml.iID_MLSKTCha,
	   ml.sL,
	   ml.sK,
	   ml.sM,
	   ml.sNG,
	   ml.sKyHieu,
	   ml.sMoTa,
       round(isnull(skt.MuaHangHienVat,0) / @dvt, 0) AS SoKiemTraMHHVNamTruoc ,
       round(isnull(dt.MuaHangHienVat,0) / @dvt, 0) AS DuToanDauNam ,
       round(isnull(snc.fTonKhoDenNgay,0) / @dvt, 0) AS TonKhoDenNgay,
       round(isnull(snc.fHuyDongTonKho,0) / @dvt, 0) AS HuyDongTonKho,
       round(isnull(snc.fMuaHangCapHienVat,0) / @dvt, 0) AS MuaHangCapHienVat,
	   snc.sGhiChu
FROM NS_SKT_MucLuc ml
LEFT JOIN #SoNhuCauTongHop snc on snc.sKyHieu = ml.sKyHieu
LEFT JOIN @tblSkt skt on skt.KyHieu = ml.sKyHieu
LEFT JOIN @tblDuToan dt on  dt.KyHieu = ml.sKyHieu
WHERE ml.iNamLamViec = @NamLamViec
AND (@MaBQuanLy = '0' OR ml.sKyHieu in (SELECT * FROM #KyHieuSktBQuanLy))
and (skt.MuaHangHienVat <> 0 or dt.MuaHangHienVat <> 0 or snc.fHuyDongTonKho <> 0 or snc.fTonKhoDenNgay <> 0 or snc.fMuaHangCapHienVat <> 0 or (snc.sGhiChu is not null and snc.sGhiChu != ''));

DROP TABLE #SoNhuCauTongHop;
DROP TABLE #KyHieuSktBQuanLy;
END
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_chung_tu_chi_tiet]    Script Date: 7/30/2024 2:37:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_skt_chung_tu_chi_tiet]
	@Loai int,
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@LoaiNguonNganSach int,
	@LoaiChungTu int,
	@IdDonVi nvarchar(100),
	@BKhoa int
AS
BEGIN
SELECT 
		ct.iID_CTSoKiemTraChiTiet,
		ct.iID_CTSoKiemTra,
		ct.iID_MaDonVi,
		ct.iID_MLSKT,
		ct.sMoTa,
		ct.fHuyDongTonKho,
		ct.fTonKhoDenNgay,
		ct.fThongBaoDonVi,
		ct.fTuChi,
		ct.fTuChiDeNghi,
		ct.sGhiChu,
		ct.iLoai,
		ct.iNamLamViec,
		ct.iNamNganSach,
		ct.iID_MaNguonNganSach,
		ct.dNgayTao,
		ct.sNguoiTao,
		ct.dNgaySua,
		ct.sNguoiSua,
		ct.fHienVat,
		ct.fPhanCap,
		ct.fMuaHangCapHienVat,
		ct.fKhungNganSachDuocDuyet,
		ct.fSoNganhPhanCap,
		ct.iLoaiChungTu,
		ct.sKyHieu,
		ct.sTenDonVi
	FROM
		(
			SELECT 
				ctct.*
			FROM 
				NS_SKT_ChungTu ct
			JOIN 
				NS_SKT_ChungTuChiTiet ctct ON ct.iID_CTSoKiemTra = ctct.iID_CTSoKiemTra 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON ctct.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE 
				ctct.iNamLamViec = @NamLamViec
				AND ctct.iNamNganSach = @NamNganSach
				AND ctct.iID_MaNguonNganSach = @NguonNganSach
				AND ctct.iLoai = @Loai 
				AND ctct.iLoaiChungTu = @LoaiChungTu
				AND ct.iLoaiNguonNganSach = @LoaiNguonNganSach
				AND (@IdDonVi = '-1' OR ctct.iID_MaDonVi = @IdDonVi)
				AND (@BKhoa = 3 OR ct.bKhoa = @BKhoa) 
		) ct;

END
;
;
;
GO
