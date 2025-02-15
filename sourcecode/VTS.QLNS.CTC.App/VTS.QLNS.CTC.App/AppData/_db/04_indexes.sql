/****** Object:  Index [IDX_TL_BangLuong_Thang]    Script Date: 4/27/2022 4:54:07 PM ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE name='IDX_TL_BangLuong_Thang' AND object_id = OBJECT_ID('TL_BangLuong_Thang'))
DROP INDEX [IDX_TL_BangLuong_Thang] ON [dbo].[TL_DM_CanBo]
GO
CREATE NONCLUSTERED INDEX [IDX_TL_BangLuong_Thang]
ON [dbo].[TL_BangLuong_Thang] ([parent])
INCLUDE([Ma_CBo],[Gia_Tri],[Ma_PhuCap])
GO
/****** Object:  Index [IDX_TL_DS_CapNhap_BangLuong]    Script Date: 4/27/2022 4:54:44 PM ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE name='IDX_TL_DS_CapNhap_BangLuong' AND object_id = OBJECT_ID('TL_DS_CapNhap_BangLuong'))
DROP INDEX [IDX_TL_DS_CapNhap_BangLuong] ON [dbo].[TL_DM_CanBo]
GO
CREATE NONCLUSTERED INDEX [IDX_TL_DS_CapNhap_BangLuong]
ON [dbo].[TL_DS_CapNhap_BangLuong] ([Ma_CBo], [Thang], [Nam], [Ma_CachTL], [Status])
GO
/****** Object:  Indexes IDX_TL_DM_CanBo    Script Date: 6/5/2022 9:06:46 PM ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE name='IDX_TL_DM_CanBo' AND object_id = OBJECT_ID('TL_DM_CanBo'))
DROP INDEX [IDX_TL_DM_CanBo] ON [dbo].[TL_DM_CanBo]
GO
CREATE NONCLUSTERED INDEX [IDX_TL_DM_CanBo]
ON [dbo].[TL_DM_CanBo] ([Ma_CanBo],[Thang],[Nam],[Parent])
INCLUDE ([Ten_CanBo],[Ma_CB],[Ma_Hieu_CanBo],[BHTN],[PCCV],[Ngay_NN],[Ngay_XN],[Ngay_TN],[Thang_TNN])
GO
/****** Object:  Indexes IDX_TL_CanBo_PhuCap    Script Date: 6/5/2022 9:06:46 PM ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE name='IDX_TL_CanBo_PhuCap' AND object_id = OBJECT_ID('TL_CanBo_PhuCap'))
DROP INDEX [IDX_TL_CanBo_PhuCap] ON [dbo].[TL_CanBo_PhuCap]
GO
CREATE NONCLUSTERED INDEX [IDX_TL_CanBo_PhuCap]
ON [dbo].[TL_CanBo_PhuCap] ([MA_CBO],[MA_PHUCAP])
INCLUDE ([GIA_TRI],[HE_SO],[MA_KMCP],[CONG_THUC],[PHANTRAM_CT],[CHON],[HuongPC_SN],[DateStart],[DateEnd],[ISoThang_Huong],[bSaoChep])
GO