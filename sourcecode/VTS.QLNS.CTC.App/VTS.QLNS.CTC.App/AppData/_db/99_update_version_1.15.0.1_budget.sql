/****** Object:  Table [dbo].[NS_MLSKT_DinhMuc]    Script Date: 10/25/2024 8:26:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NS_MLSKT_DinhMuc]') AND type in (N'U'))
DROP TABLE [dbo].[NS_MLSKT_DinhMuc]
GO
/****** Object:  Table [dbo].[NS_MLSKT_DinhMuc]    Script Date: 10/25/2024 8:26:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NS_MLSKT_DinhMuc](
	[iMaMucTieuMuc] [int] NULL,
	[iMaNganh] [int] NULL,
	[sMaMucLuc] [nvarchar](255) NULL,
	[iSTT] [int] NULL,
	[sNoiDung] [nvarchar](255) NULL
) ON [PRIMARY]
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6253, 0, N'1-2-1-02-03-1-00', 0, N'PLTT: Tàu xe phép')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6299, 0, N'1-2-1-02-07-1-00', 0, N'TTM 90: Phúc lợi khác')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6650, 0, N'1-2-1-02-09-1-00', 0, N'Hội nghị')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6700, 0, N'1-2-1-02-10-1-00', 0, N'Công tác phí')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7761, 0, N'1-2-1-02-20-1-00', 0, N'Chỉ huy phí (tiếp khách)')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7000, 20, N'1-2-2-01-01-1-01', 0, N'Chi phí nghiệp vụ CM ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6949, 21, N'1-2-2-01-02-1-01', 0, N'Sữa chữa, duy tu tài sản chuyên dùng phục vụ công tác CM ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6950, 21, N'1-2-2-01-02-1-02', 0, N'MS tài sản phục vụ công tác CM')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7000, 21, N'1-2-2-01-02-1-03', 0, N'Chi phí nghiệp vụ ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7750, 21, N'1-2-2-01-02-1-04', 0, N'Chi huấn luyện chiến dịch')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7750, 21, N'1-2-2-01-02-1-07', 0, N'Chi diễn tập các cấp - Tác chiến')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6949, 22, N'1-2-2-01-03-1-01', 0, N'Sửa chữa, duy tu tài sản chuyên dùng phục vụ công tác CM ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6950, 22, N'1-2-2-01-03-1-02', 0, N'MS tài sản phục vụ công tác CM')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7012, 22, N'1-2-2-01-03-1-03', 0, N'Chi phí nghiệp vụ ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7750, 22, N'1-2-2-01-03-1-04', 0, N'Chi HL chiến đấu và TDTT')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7000, 23, N'1-2-2-01-04-1-04', 0, N'Chi phí nghiệp vụ ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6900, 24, N'1-2-2-01-05-1-01', 0, N'Sửa chữa, duy tu tài sản chuyên dùng phục vụ công tác CM ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6950, 24, N'1-2-2-01-05-1-02', 0, N'MS tài sản phục vụ công tác CM')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7000, 24, N'1-2-2-01-05-1-03', 0, N'Chi phí nghiệp vụ ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7750, 24, N'1-2-2-01-05-1-05', 0, N'Chi huấn luyện ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6950, 25, N'1-2-2-01-06-1-01', 0, N'MS tài sản phục vụ công tác CM')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7000, 25, N'1-2-2-01-06-1-02', 0, N'Chi phí nghiệp vụ ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7750, 25, N'1-2-2-01-06-1-03', 0, N'Chi huấn luyện ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6949, 26, N'1-2-2-01-07-1-01', 0, N'Sữa chữa, duy tu tài sản chuyên dùng phục vụ công tác CM ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6950, 26, N'1-2-2-01-07-1-02', 0, N'MS tài sản phục vụ công tác CM')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7000, 26, N'1-2-2-01-07-1-03', 0, N'Chi phí nghiệp vụ ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7750, 26, N'1-2-2-01-07-1-04', 0, N'Chi huấn luyện ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7012, 27, N'1-2-2-01-08-1-03', 0, N'Chi phí nghiệp vụ ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7750, 27, N'1-2-2-01-08-1-04', 0, N'Chi huấn luyện ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6949, 28, N'1-2-2-01-09-1-01', 0, N'Sửa chữa, duy tu tài sản chuyên dùng phục vụ công tác CM ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7012, 28, N'1-2-2-01-09-1-03', 0, N'Chi phí nghiệp vụ ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7750, 28, N'1-2-2-01-09-1-04', 0, N'Chi huấn luyện ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6949, 29, N'1-2-2-01-10-1-01', 0, N'Sửa chữa, duy tu tài sản chuyên dùng phục vụ công tác CM ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6950, 29, N'1-2-2-01-10-1-02', 0, N'MS tài sản phục vụ công tác CM')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7000, 29, N'1-2-2-01-10-1-03', 0, N'Chi phí nghiệp vụ ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7750, 29, N'1-2-2-01-10-1-04', 0, N'Chi huấn luyện ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6949, 41, N'1-2-2-01-11-1-01', 0, N'Sửa chữa, duy tu tài sản chuyên dùng phục vụ công tác CM ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6950, 41, N'1-2-2-01-11-1-02', 0, N'MS tài sản phục vụ công tác CM')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7000, 41, N'1-2-2-01-11-1-03', 0, N'Chi phí nghiệp vụ ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7750, 41, N'1-2-2-01-11-1-04', 0, N'Chi huấn luyện ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6949, 42, N'1-2-2-01-12-1-01', 0, N'Sửa chữa, duy tu tài sản chuyên dùng phục vụ công tác CM ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6950, 42, N'1-2-2-01-12-1-02', 0, N'MS tài sản phục vụ công tác CM')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7000, 42, N'1-2-2-01-12-1-03', 0, N'Chi phí nghiệp vụ ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7750, 42, N'1-2-2-01-12-1-04', 0, N'Chi huấn luyện ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6949, 44, N'1-2-2-01-13-1-01', 0, N'Sửa chữa, duy tu tài sản chuyên dùng phục vụ công tác CM ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6950, 44, N'1-2-2-01-13-1-02', 0, N'MS tài sản phục vụ công tác CM')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7000, 44, N'1-2-2-01-13-1-03', 0, N'Chi phí nghiệp vụ ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7750, 44, N'1-2-2-01-13-1-04', 0, N'Chi huấn luyện ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7000, 67, N'1-2-2-01-14-1-02', 0, N'Chi phí nghiệp vụ ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7750, 67, N'1-2-2-01-14-1-03', 0, N'Chi huấn luyện ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6949, 70, N'1-2-2-01-15-1-02', 0, N'Sửa chữa, duy tu tài sản chuyên dùng phục vụ công tác CM ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7000, 70, N'1-2-2-01-15-1-03', 0, N'Chi phí nghiệp vụ ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6900, 71, N'1-2-2-01-16-1-01', 0, N'Sửa chữa, duy tu tài sản chuyên dùng phục vụ công tác CM ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6950, 71, N'1-2-2-01-16-1-02', 0, N'MS tài sản phục vụ công tác CM')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7000, 71, N'1-2-2-01-16-1-03', 0, N'Chi phí nghiệp vụ ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7012, 72, N'1-2-2-01-17-1-04', 0, N'Chi phí nghiệp vụ ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6950, 73, N'1-2-2-01-18-1-02', 0, N'MS tài sản phục vụ công tác CM')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7012, 73, N'1-2-2-01-18-1-03', 0, N'Chi nghiệp vụ ngành lịch sử')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7750, 73, N'1-2-2-01-18-1-05', 0, N'Chi huấn luyện ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7000, 74, N'1-2-2-01-19-1-03', 0, N'Chi phí nghiệp vụ ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6949, 76, N'1-2-2-01-21-1-01', 0, N'Sửa chữa, duy tu tài sản chuyên dùng phục vụ công tác CM ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6950, 76, N'1-2-2-01-21-1-02', 0, N'MS tài sản phục vụ công tác CM')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7000, 76, N'1-2-2-01-21-1-03', 0, N'Chi phí nghiệp vụ ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7750, 76, N'1-2-2-01-21-1-04', 0, N'Chi huấn luyện ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7012, 76, N'1-2-2-01-21-1-07', 0, N'Thanh toán phí trả lương qua tài khoản ngân hàng')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6949, 77, N'1-2-2-01-22-1-01', 0, N'Sửa chữa, duy tu tài sản chuyên dùng phục vụ công tác CM ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7000, 77, N'1-2-2-01-22-1-02', 0, N'Chi phí nghiệp vụ ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7750, 77, N'1-2-2-01-22-1-03', 0, N'Chi huấn luyện ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6950, 78, N'1-2-2-01-23-1-02', 0, N'MS tài sản phục vụ công tác CM')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7000, 78, N'1-2-2-01-23-1-03', 0, N'Chi phí nghiệp vụ ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7750, 78, N'1-2-2-01-23-1-04', 0, N'Chi huấn luyện ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7000, 81, N'1-2-2-01-24-1-02', 0, N'Chi phí nghiệp vụ ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6949, 30, N'1-2-2-02-26-1-01', 0, N'Sửa chữa, duy tu tài sản chuyên dùng phục vụ công tác CM ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6950, 30, N'1-2-2-02-26-1-02', 0, N'MS tài sản phục vụ công tác CM')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7000, 30, N'1-2-2-02-26-1-03', 0, N'Chi phí nghiệp vụ ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7750, 30, N'1-2-2-02-26-1-04', 0, N'Chi huấn luyện ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6900, 31, N'1-2-2-02-27-1-01', 0, N'Sửa chữa, duy tu tài sản chuyên dùng phục vụ công tác CM ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6950, 31, N'1-2-2-02-27-1-02', 0, N'MS tài sản phục vụ công tác CM')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7000, 31, N'1-2-2-02-27-1-03', 0, N'Chi phí nghiệp vụ ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6600, 32, N'1-2-2-02-28-1-07', 0, N'Hoạt động văn hóa, VN, CLB')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6600, 32, N'1-2-2-02-28-1-08', 0, N'Hoạt động truyền thống, bảo tàng')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6900, 32, N'1-2-2-02-28-1-09', 0, N'Sửa chữa, duy tu tài sản chuyên dùng phục vụ công tác CM ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6950, 32, N'1-2-2-02-28-1-10', 0, N'MS tài sản phục vụ công tác CM')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7000, 32, N'1-2-2-02-28-1-11', 0, N'Chi phí nghiệp vụ ngành tuyên huấn')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6949, 33, N'1-2-2-02-29-1-01', 0, N'Sửa chữa, duy tu tài sản chuyên dùng phục vụ công tác CM ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6950, 33, N'1-2-2-02-29-1-02', 0, N'MS tài sản phục vụ công tác CM')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7000, 33, N'1-2-2-02-29-1-03', 0, N'Chi phí nghiệp vụ ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6949, 35, N'1-2-2-02-30-1-01', 0, N'Sửa chữa, duy tu tài sản chuyên dùng phục vụ công tác CM ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6950, 35, N'1-2-2-02-30-1-02', 0, N'MS tài sản phục vụ công tác CM')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7000, 35, N'1-2-2-02-30-1-03', 0, N'Chi phí nghiệp vụ ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6949, 36, N'1-2-2-02-31-1-02', 0, N'Sửa chữa, duy tu tài sản chuyên dùng phục vụ công tác CM ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6950, 36, N'1-2-2-02-31-1-03', 0, N'MS tài sản phục vụ công tác CM')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7000, 36, N'1-2-2-02-31-1-04', 0, N'Chi phí nghiệp vụ ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6949, 37, N'1-2-2-02-32-1-02', 0, N'Sửa chữa, duy tu tài sản chuyên dùng phục vụ công tác CM ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6950, 37, N'1-2-2-02-32-1-03', 0, N'MS tài sản phục vụ CM ngành cán bộ')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7000, 37, N'1-2-2-02-32-1-05', 0, N'Chi phí nghiệp vụ ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6949, 38, N'1-2-2-02-33-1-01', 0, N'Sửa chữa, duy tu tài sản chuyên dùng phục vụ công tác CM ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6950, 38, N'1-2-2-02-33-1-02', 0, N'MS tài sản phục vụ công tác CM')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7000, 38, N'1-2-2-02-33-1-03', 0, N'Chi phí nghiệp vụ ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6949, 39, N'1-2-2-02-34-1-01', 0, N'Sửa chữa, duy tu tài sản chuyên dùng phục vụ công tác CM ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6950, 39, N'1-2-2-02-34-1-02', 0, N'MS tài sản phục vụ công tác CM')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7000, 39, N'1-2-2-02-34-1-03', 0, N'Chi phí nghiệp vụ ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6949, 45, N'1-2-2-02-35-1-01', 0, N'Sửa chữa, duy tu tài sản chuyên dùng phục vụ công tác CM ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6950, 45, N'1-2-2-02-35-1-02', 0, N'MS tài sản phục vụ công tác CM')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7000, 45, N'1-2-2-02-35-1-03', 0, N'Chi phí nghiệp vụ ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6300, 46, N'1-2-2-02-36-1-01', 0, N'Các khoản đóng góp: KPCĐ')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6949, 47, N'1-2-2-02-37-1-02', 0, N'Sửa chữa, duy tu tài sản chuyên dùng phục vụ công tác CM ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6950, 47, N'1-2-2-02-37-1-03', 0, N'MS tài sản phục vụ công tác CM')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7000, 47, N'1-2-2-02-37-1-04', 0, N'Chi phí nghiệp vụ ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6949, 40, N'1-2-2-02-38-1-01', 0, N'Sửa chữa, duy tu tài sản chuyên dùng phục vụ công tác CM ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6950, 40, N'1-2-2-02-38-1-02', 0, N'MS tài sản phục vụ công tác CM')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7000, 43, N'1-2-2-02-39-1-03', 0, N'Chi phí nghiệp vụ ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7000, 50, N'1-2-2-03-38-1-02', 0, N'Chi phí nghiệp vụ ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7750, 50, N'1-2-2-03-38-1-04', 0, N'Chi huấn luyện ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6949, 51, N'1-2-2-03-39-1-02', 0, N'Sửa chữa, duy tu tài sản chuyên dùng phục vụ công tác CM ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6950, 51, N'1-2-2-03-39-1-03', 0, N'MS trang bị quân nhu')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7000, 51, N'1-2-2-03-39-1-04', 0, N'Chi mua hàng hóa doanh cụ quân nhu')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7000, 51, N'1-2-2-03-39-1-05', 0, N'Chi mua hàng dụng cụ cấp dưỡng')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7000, 51, N'1-2-2-03-39-1-09', 0, N'Chi phí nghiệp vụ ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7750, 51, N'1-2-2-03-39-1-10', 0, N'Chi huấn luyện ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6900, 53, N'1-2-2-03-40-1-01', 0, N'Sửa chữa, duy tu tài sản chuyên dùng phục vụ công tác CM ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6950, 53, N'1-2-2-03-40-1-02', 0, N'MS tài sản phục vụ công tác CM')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7012, 53, N'1-2-2-03-40-1-07', 0, N'Chi phí nghiệp vụ ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7799, 53, N'1-2-2-03-40-1-08', 0, N'TTM 50: Chi huấn luyện ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6949, 54, N'1-2-2-03-41-1-02', 0, N'Sửa chữa, duy tu tài sản chuyên dùng phục vụ công tác CM ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7000, 54, N'1-2-2-03-41-1-04', 0, N'Chi phí nghiệp vụ ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7750, 54, N'1-2-2-03-41-1-05', 0, N'Chi huấn luyện ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6949, 55, N'1-2-2-03-42-1-02', 0, N'Sửa chữa, duy tu tài sản chuyên dùng phục vụ công tác CM ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6950, 55, N'1-2-2-03-42-1-03', 0, N'MS tài sản phục vụ công tác CM')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7000, 55, N'1-2-2-03-42-1-04', 0, N'Chi phí nghiệp vụ ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7750, 55, N'1-2-2-03-42-1-05', 0, N'Chi huấn luyện ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6907, 56, N'1-2-2-03-43-1-03', 0, N'BQSC doanh trại')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6950, 56, N'1-2-2-03-43-1-06', 0, N'MS tài sản phục vụ công tác CM')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7000, 56, N'1-2-2-03-43-1-09', 0, N'Chi phí nghiệp vụ ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7750, 56, N'1-2-2-03-43-1-10', 0, N'Chi huấn luyện')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6907, 56, N'1-2-2-03-43-1-13', 0, N'TTM 20: Bảo trì thang máy')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6905, 60, N'1-2-2-04-45-1-01', 0, N'Bảo đảm kỹ thuật (tại XN,đơn vị)')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7001, 60, N'1-2-2-04-45-1-04', 0, N'TTM 40: Chi phí nghiệp vụ TMKT')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7799, 60, N'1-2-2-04-45-1-06', 0, N'TTM 40: Huấn luyện kỹ thuật - TMKT')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6905, 61, N'1-2-2-04-46-1-01', 0, N'Bảo đảm kỹ thuật (tại XN, đơn vị)')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6905, 62, N'1-2-2-04-47-1-01', 0, N'Bảo đảm kỹ thuật (tại XN, đơn vị)')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6905, 64, N'1-2-2-04-48-1-01', 0, N'Bảo đảm kỹ thuật (tại XN, đơn vị)')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7001, 64, N'1-2-2-04-48-1-03', 0, N'TTM 40: Chi phí nghiệp vụ TMKT')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6905, 65, N'1-2-2-04-49-1-01', 0, N'Bảo đảm kỹ thuật (tại XN, đơn vị)')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7001, 65, N'1-2-2-04-49-1-03', 0, N'TTM 40: Chi phí nghiệp vụ TMKT')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7012, 65, N'1-2-2-04-49-1-05', 0, N'Nghiệp vụ ngành Xe máy')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7004, 66, N'1-2-2-04-50-1-04', 0, N'Chi an toàn bảo hộ lao động')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6905, 27, N'1-2-2-04-51-1-01', 0, N'Bảo quản kỹ thuật cơ yếu')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6905, 1, N'1-2-2-04-54-1-01', 0, N'Bảo quản kỹ thuật PKKQ')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6905, 4, N'1-2-2-04-56-1-01', 0, N'Bảo quản kỹ thuật Thông tin')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6905, 5, N'1-2-2-04-57-1-01', 0, N'Bảo quản kỹ thuật Công binh')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7799, 5, N'1-2-2-04-57-1-04', 0, N'TTM 40: Huấn luyện kỹ thuật - Công binh')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6905, 7, N'1-2-2-04-59-1-01', 0, N'Bảo quản kỹ thuật Đặc công')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6905, 9, N'1-2-2-04-61-1-01', 0, N'Bảo quản kỹ thuật Hóa học')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6905, 67, N'1-2-2-04-65-1-01', 0, N'Bảo quản kỹ thuật Đo lường')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6905, 69, N'1-2-2-04-66-1-01', 0, N'Bảo quản kỹ thuật Tác chiến không gian mạng')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7012, 80, N'1-2-2-05-67-1-04', 0, N'Chi phí nghiệp vụ ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7012, 1, N'1-2-2-07-70-1-01', 0, N'Nghiệp vụ PKKQ')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7012, 4, N'1-2-2-07-72-1-02', 0, N'Nghiệp vụ Thông tin')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7000, 5, N'1-2-2-07-73-1-01', 0, N'Nghiệp vụ Công binh')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7000, 6, N'1-2-2-07-74-1-01', 0, N'Nghiệp vụ Pháo binh')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7000, 7, N'1-2-2-07-75-1-01', 0, N'Nghiệp vụ Đặc công')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7000, 8, N'1-2-2-07-76-1-01', 0, N'Nghiệp vụ Tăng Thiết giáp')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7000, 9, N'1-2-2-07-77-1-01', 0, N'Nghiệp vụ Hóa học')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7000, 10, N'1-2-2-07-78-1-01', 0, N'Nghiệp vụ Biên phòng chung')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (6999, 69, N'1-2-2-08-69-1-03', 0, N'TTM 30: MS tài sản phục vụ công tác CM')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7000, 69, N'1-2-2-08-69-1-04', 0, N'Chi phí nghiệp vụ ngành')
GO
INSERT [dbo].[NS_MLSKT_DinhMuc] ([iMaMucTieuMuc], [iMaNganh], [sMaMucLuc], [iSTT], [sNoiDung]) VALUES (7799, 69, N'1-2-2-08-69-1-08', 0, N'TTM 50: Chi huấn luyện ngành')
GO

update mucluc
set mucluc.bCoDinhMuc = 1
from NS_SKT_MucLuc mucluc
join NS_MLSKT_DinhMuc dinhmuc 
on mucluc.skyhieu = dinhmuc.sMaMucLuc
go