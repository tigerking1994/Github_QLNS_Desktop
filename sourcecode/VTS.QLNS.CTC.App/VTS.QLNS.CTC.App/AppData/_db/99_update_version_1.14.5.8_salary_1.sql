/****** Object:  StoredProcedure [dbo].[sp_tl_chungtu_chitiet_kehoach_nq104]    Script Date: 6/5/2024 6:06:40 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_chungtu_chitiet_kehoach_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_chungtu_chitiet_kehoach_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_chungtu_chitiet_kehoach_nq104]    Script Date: 6/5/2024 6:06:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_chungtu_chitiet_kehoach_nq104]
	@maDonVi varchar(50), 
	@thang int, 
	@nam int, 
	@pcMlnsNam int
AS
BEGIN
	WITH PhuCapChiTiet2Nam AS (
			SELECT 
				ctct.XauNoiMa as XauNoiMa, 
				ctct.TongCong as TongNamTruoc
			FROM TL_QT_ChungTu_NQ104 ct
			JOIN TL_QT_ChungTuChiTiet_KeHoach ctct ON ct.ID = ctct.Id_ChungTu
			AND ct.sTongHop IS NULL
			WHERE ct.Nam = @pcMlnsNam - 1
			  AND Ma_DonVi = @maDonVi
			  AND ct.Thang = @thang
			GROUP BY ctct.XauNoiMa, ctct.TongCong
	),
	PhuCapChiTiet AS (
		SELECT
			TL_PhuCap_MLNS_NQ104.Ma_PhuCap,
			TL_PhuCap_MLNS_NQ104.XauNoiMa,
			TL_PhuCap_MLNS_NQ104.MoTa,
			Luong_CapBac.Ma_Don_Vi,
			Luong_CapBac.THANG,
			Luong_CapBac.NAM,
			SUM(Luong_CapBac.Gia_Tri) AS Tong
		FROM TL_PhuCap_MLNS_NQ104
		LEFT JOIN (
			SELECT
				bangluong.ma_phu_cap,
				bangluong.Ma_Don_Vi,
				bangluong.THANG,
				bangluong.NAM,
				capbac.Parent,
				SUM(bangluong.Gia_Tri) AS Gia_Tri,
				COUNT(bangluong.ma_phu_cap) AS SoNguoi
			FROM TL_BangLuong_KeHoach_Bridge_NQ104 bangluong
			JOIN TL_DM_CapBac_NQ104 capbac ON bangluong.Ma_CB = capbac.Ma_Cb
			GROUP BY bangluong.ma_phu_cap, bangluong.Ma_Don_Vi, bangluong.THANG, bangluong.NAM, capbac.Parent
		) AS Luong_CapBac ON TL_PhuCap_MLNS_NQ104.Ma_Cb = Luong_CapBac.Parent AND TL_PhuCap_MLNS_NQ104.Ma_PhuCap = Luong_CapBac.ma_phu_cap
		WHERE Luong_CapBac.Ma_Don_Vi = @maDonVi
			AND Luong_CapBac.THANG = @thang
			AND Luong_CapBac.NAM = @nam
			AND TL_PhuCap_MLNS_NQ104.Nam = @pcMlnsNam
			AND Luong_CapBac.Gia_Tri > 0
		GROUP BY TL_PhuCap_MLNS_NQ104.Ma_PhuCap, TL_PhuCap_MLNS_NQ104.XauNoiMa, TL_PhuCap_MLNS_NQ104.MoTa, Luong_CapBac.Ma_Don_Vi, Luong_CapBac.THANG, Luong_CapBac.NAM
	)
	SELECT
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
		@nam AS NamLamViec,
		bHangCha AS BHangCha,
		sChiTietToi AS ChiTietToi,
		SUM(TongNamTruoc) as TongNamTruoc,
		SUM(Tong) AS TongCong,
		--SoNguoi AS SoNguoi,
		SUM(Tong) AS DieuChinh,
		Ma_PhuCap AS MaPhuCap,
		@thang AS Thang
		--Ma_cb AS Ngach
	FROM NS_MucLucNganSach mlns
	LEFT JOIN PhuCapChiTiet phuCap ON mlns.sXauNoiMa = phuCap.XauNoiMa
	LEFT JOIN PhuCapChiTiet2Nam phuCap2Nam ON mlns.sXauNoiMa = phuCap2Nam.XauNoiMa
	WHERE
		sLNS IN ('1', '101', '1010000')
		AND iNamLamViec = @pcMlnsNam
	GROUP BY iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, NAM, sChiTietToi, bHangCha, Ma_PhuCap, Thang, phuCap2Nam.TongNamTruoc
	ORDER BY sXauNoiMa
END
;
;
;
GO


DELETE FROM [dbo].[TL_DM_CapBac_KeHoach_NQ104]
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'0ccf02f7-286c-4273-ac1c-4bd8a7b23cf5', NULL, N'01', N'02', NULL, NULL, NULL, NULL, N'4', NULL, NULL, NULL, N'Binh nhì', N'Binh nhất', NULL, NULL, NULL, N'01', N'02', NULL, NULL)
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'50a91a64-877e-43c7-af32-b4878935fd4e', NULL, N'02', N'03', NULL, NULL, NULL, NULL, N'4', NULL, NULL, NULL, N'Binh nhất', N'Hạ sỹ', NULL, NULL, NULL, N'02', N'03', NULL, NULL)
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'd8b99360-4c42-422f-8d2e-11e36a037fa0', NULL, N'03', N'04', NULL, NULL, NULL, NULL, N'4', NULL, NULL, NULL, N'Hạ sỹ', N'Trung sỹ', NULL, NULL, NULL, N'03', N'04', NULL, NULL)
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'f32a1121-38e0-4d51-ac70-d16c534deef4', NULL, N'04', N'05', NULL, NULL, NULL, NULL, N'4', NULL, NULL, NULL, N'Trung sỹ', N'Thượng sỹ', NULL, NULL, NULL, N'04', N'05', NULL, NULL)
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'07cd0727-e3e0-49e1-9b52-f48180d344fe', NULL, N'05', N'05', NULL, NULL, NULL, NULL, N'4', NULL, NULL, NULL, N'Thượng sỹ', N'Thượng sỹ', NULL, NULL, NULL, N'05', N'05', NULL, NULL)
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'10b8f949-1e92-4eee-af32-ad15ebc36d9b', NULL, N'111', N'112', NULL, NULL, NULL, NULL, N'1', NULL, NULL, NULL, N'Thiếu úy', N'Trung úy', 18, 46, 46, N'111', N'112', NULL, NULL)
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'a2571fc4-9189-4d28-ab2f-531d14764a89', NULL, N'112', N'113', NULL, NULL, NULL, NULL, N'1', NULL, NULL, NULL, N'Trung úy', N'Thượng úy', 36, 46, 46, N'112', N'113', NULL, NULL)
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'd39af9a2-3de7-458a-841b-d637f9dab6ad', NULL, N'113', N'114', NULL, NULL, NULL, NULL, N'1', NULL, NULL, NULL, N'Thượng úy', N'Đại úy', 36, 46, 46, N'113', N'114', NULL, NULL)
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'04a0b568-e8ea-40ee-b98a-1f8eee335914', NULL, N'114', N'121', NULL, NULL, NULL, NULL, N'1', NULL, NULL, NULL, N'Đại úy', N'Thiếu tá', 48, 46, 46, N'114', N'121', NULL, NULL)
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'b432225d-54c9-4b15-a4f7-185d75afae9f', NULL, N'1141', N'1142', NULL, NULL, NULL, NULL, N'1', NULL, NULL, NULL, N'Đại úy (Lần 1)', N'Đại úy (Lần 2)', 48, 46, 46, N'1141', N'1142', NULL, NULL)
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'0309f783-04af-4a25-ae3b-9abf759ddbbe', NULL, N'1142', N'1143', NULL, NULL, NULL, NULL, N'1', NULL, NULL, NULL, N'Đại úy (Lần 2)', N'Đại úy (Lần 3)', 48, 46, 46, N'1142', N'1143', NULL, NULL)
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'23d93824-13b5-4114-90df-7e836da4378c', NULL, N'1143', N'1144', NULL, NULL, NULL, NULL, N'1', NULL, NULL, NULL, N'Đại úy (Lần 3)', N'Đại úy (Lần 4)', 48, 46, 46, N'1143', N'1144', NULL, NULL)
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'2683beb0-fbe8-476d-913e-09925056ce59', NULL, N'1144', N'121', NULL, NULL, NULL, NULL, N'1', NULL, NULL, NULL, N'Đại úy (Lần 4)', N'Thiếu tá', 48, 46, 46, N'1144', N'121', NULL, NULL)
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'C8A9CA1B-A2CB-43C4-AE92-151B93B8E490', NULL, N'121', N'122', NULL, NULL, NULL, NULL, N'1', NULL, NULL, NULL, N'Thiếu tá', N'Trung tá', 48, 48, 48, N'121', N'122', NULL, NULL)
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'a0f5ca7d-e6ee-4126-b768-5d5113e2d6ea', NULL, N'1211', N'1212', NULL, NULL, NULL, NULL, N'1', NULL, NULL, NULL, N'Thiếu tá (Lấn 1)', N'Thiếu tá (Lấn 2)', 48, 48, 48, N'1211', N'1212', NULL, NULL)
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'b64de6a5-9cba-468c-8a22-e7e4cbdd2814', NULL, N'1212', N'1213', NULL, NULL, NULL, NULL, N'1', NULL, NULL, NULL, N'Thiếu tá (Lấn 2)', N'Thiếu tá (Lấn 3)', 48, 48, 48, N'1212', N'1213', NULL, NULL)
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'68baa0be-8f51-4295-8f98-1fea48e77555', NULL, N'1213', N'1214', NULL, NULL, NULL, NULL, N'1', NULL, NULL, NULL, N'Thiếu tá (Lấn 3)', N'Thiếu tá (Lấn 4)', 48, 48, 48, N'1213', N'1214', NULL, NULL)
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'48b5f6b8-3974-48c9-85ce-cdbb241e7dcf', NULL, N'1214', N'122', NULL, NULL, NULL, NULL, N'1', NULL, NULL, NULL, N'Thiếu tá (Lấn 4)', N'Trung tá', 48, 48, 48, N'1214', N'122', NULL, NULL)
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'0eccac61-bac0-4e76-96aa-f077d1f56f71', NULL, N'122', N'123', NULL, NULL, NULL, NULL, N'1', NULL, NULL, NULL, N'Trung tá', N'Thượng tá', 48, 51, 51, N'122', N'123', NULL, NULL)
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'e4250358-6571-4e4f-9a39-7796df0ec9ff', NULL, N'1221', N'1222', NULL, NULL, NULL, NULL, N'1', NULL, NULL, NULL, N'Trung tá (Lần 1)', N'Trung tá (Lần 2)', 48, 51, 51, N'1221', N'1222', NULL, NULL)
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'fe0d2b37-dd8c-4c2f-9c77-ca39ddff9f87', NULL, N'1222', N'1223', NULL, NULL, NULL, NULL, N'1', NULL, NULL, NULL, N'Trung tá (Lần 2)', N'Trung tá (Lần 3)', 48, 51, 51, N'1222', N'1223', NULL, NULL)
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'6bbb12ae-d9fe-495d-be92-aa7ee551a67c', NULL, N'1223', N'1224', NULL, NULL, NULL, NULL, N'1', NULL, NULL, NULL, N'Trung tá (Lần 3)', N'Trung tá (Lần 4)', 48, 51, 51, N'1223', N'1224', NULL, NULL)
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'1d39fa7c-106d-4853-b8bd-9baeb60e1a53', NULL, N'1224', N'123', NULL, NULL, NULL, NULL, N'1', NULL, NULL, NULL, N'Trung tá (Lần 4)', N'Thượng tá', 48, 51, 51, N'1224', N'123', NULL, NULL)
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'd4952971-e5a9-4ca1-bd94-60718fb251da', NULL, N'123', N'124', NULL, NULL, NULL, NULL, N'1', NULL, NULL, NULL, N'Thượng tá', N'Đại tá', 48, 54, 54, N'123', N'124', NULL, NULL)
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'2e723f33-7120-4f1b-8d0b-84b570c0e39e', NULL, N'1231', N'1232', NULL, NULL, NULL, NULL, N'1', NULL, NULL, NULL, N'Thượng tá (Lần 1)', N'Thượng tá (Lần 2)', 48, 54, 54, N'1231', N'1232', NULL, NULL)
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'5939ebbd-54ad-4a81-8da0-a80fb43d1a14', NULL, N'1232', N'1233', NULL, NULL, NULL, NULL, N'1', NULL, NULL, NULL, N'Thượng tá (Lần 2)', N'Thượng tá (Lần 3)', 48, 54, 54, N'1232', N'1233', NULL, NULL)
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'9bbf2ace-92b4-47dd-8f6a-78fd20810ab5', NULL, N'1233', N'1234', NULL, NULL, NULL, NULL, N'1', NULL, NULL, NULL, N'Thượng tá (Lần 3)', N'Thượng tá (Lần 4)', 48, 54, 54, N'1233', N'1234', NULL, NULL)
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'3efddb9c-16d1-4ce7-9516-7b67ea1f5426', NULL, N'1234', N'124', NULL, NULL, NULL, NULL, N'1', NULL, NULL, NULL, N'Thượng tá (Lần 4)', N'Đại tá', 48, 54, 54, N'1234', N'124', NULL, NULL)
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'202d5200-0e02-4722-938d-47b81147bfdb', NULL, N'124', N'131', NULL, NULL, NULL, NULL, N'1', NULL, NULL, NULL, N'Đại tá', N'Thiếu tướng', 48, 54, 54, N'124', N'131', NULL, NULL)
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'e8b3201d-de6f-409d-9005-67d563134011', NULL, N'1241', N'1242', NULL, NULL, NULL, NULL, N'1', NULL, NULL, NULL, N'Đại tá (Lần 1)', N'Đại tá (Lần 2)', 48, 54, 54, N'1241', N'1242', NULL, NULL)
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'b3d45c93-ace4-4f3c-b795-141261ccc719', NULL, N'1242', N'1243', NULL, NULL, NULL, NULL, N'1', NULL, NULL, NULL, N'Đại tá (Lần 2)', N'Đại tá (Lần 3)', 48, 54, 54, N'1242', N'1243', NULL, NULL)
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'0c8d5503-2bb9-4754-9f48-95a349de4038', NULL, N'1243', N'1244', NULL, NULL, NULL, NULL, N'1', NULL, NULL, NULL, N'Đại tá (Lần 3)', N'Đại tá (Lần 4)', 48, 54, 54, N'1243', N'1244', NULL, NULL)
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'89b1aadc-ca0e-4ee7-b0f1-ac01ad6d3546', NULL, N'1244', N'131', NULL, NULL, NULL, NULL, N'1', NULL, NULL, NULL, N'Đại tá (Lần 4)', N'Thiếu tướng', 48, 54, 54, N'1244', N'131', NULL, NULL)
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'b90c0c1d-37f1-4fb3-a41c-cc74b309a282', NULL, N'131', N'132', NULL, NULL, NULL, NULL, N'1', NULL, NULL, NULL, N'Thiếu tướng', N'Trung tướng', 48, 60, 60, N'131', N'132', NULL, NULL)
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'ca507900-f5b0-4fb2-b817-5adf61ee27b4', NULL, N'1311', N'1312', NULL, NULL, NULL, NULL, N'1', NULL, NULL, NULL, N'Thiếu tướng (Lần 1)', N'Thiếu tướng (Lần 2)', 48, 60, 60, N'1311', N'1312', NULL, NULL)
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'39ac3faf-6351-463a-8994-1b5a4adc4d10', NULL, N'1312', N'132', NULL, NULL, NULL, NULL, N'1', NULL, NULL, NULL, N'Thiếu tướng (Lần 2)', N'Trung tướng', 48, 60, 60, N'1312', N'132', NULL, NULL)
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'25bf7ca9-ae34-4080-bba0-85e7057389af', NULL, N'132', N'133', NULL, NULL, NULL, NULL, N'1', NULL, NULL, NULL, N'Trung tướng', N'Thượng tướng', 48, 60, 60, N'132', N'133', NULL, NULL)
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'd1d1e29c-9f8b-41a3-a578-d8c029c033f6', NULL, N'1321', N'1322', NULL, NULL, NULL, NULL, N'1', NULL, NULL, NULL, N'Trung tướng (Lần 1)', N'Trung tướng (Lần 2)', 48, 60, 60, N'1321', N'1322', NULL, NULL)
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'af1951d8-cd66-4aef-bce8-15532e933f15', NULL, N'1322', N'133', NULL, NULL, NULL, NULL, N'1', NULL, NULL, NULL, N'Trung tướng (Lần 2)', N'Thượng tướng', 48, 60, 60, N'1322', N'133', NULL, NULL)
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'6a713406-073a-45c5-9543-f6eef201148b', NULL, N'133', N'134', NULL, NULL, NULL, NULL, N'1', NULL, NULL, NULL, N'Thượng tướng', N'Đại tướng', 48, 60, 60, N'133', N'134', NULL, NULL)
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'84c8e7d3-9466-4c08-a730-639fca4ecf62', NULL, N'1331', N'1332', NULL, NULL, NULL, NULL, N'1', NULL, NULL, NULL, N'Thượng tướng (Lần 1)', N'Thượng tướng (Lần 2)', 48, 60, 60, N'1331', N'1332', NULL, NULL)
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'61b660e4-0b05-4ea5-ad8a-80e238ade36d', NULL, N'1332', N'134', NULL, NULL, NULL, NULL, N'1', NULL, NULL, NULL, N'Thượng tướng (Lần 2)', N'Đại tướng', 48, 60, 60, N'1332', N'134', NULL, NULL)
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'bf952ba7-be50-4708-bf50-8da899544f7f', NULL, N'134', N'1341', NULL, NULL, NULL, NULL, N'1', NULL, NULL, NULL, N'Đại tướng', N'Đại tướng (Lần 1)', 48, 60, 60, N'134', N'1341', NULL, NULL)
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'1f773e81-ff71-4195-bef2-6cff01a898c6', NULL, N'1341', N'1342', NULL, NULL, NULL, NULL, N'1', NULL, NULL, NULL, N'Đại tướng (Lần 1)', N'Đại tướng (Lần 2)', 48, 60, 60, N'1341', N'1342', NULL, NULL)
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'd1a5f6e5-22d0-4d99-be6c-06023db816f8', NULL, N'1342', N'1342', NULL, NULL, NULL, NULL, N'1', NULL, NULL, NULL, N'Đại tướng (Lần 2)', N'Đại tướng (Lần 2)', 48, 60, 60, N'1342', N'1342', NULL, NULL)
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'd719953d-0b4c-4bd6-b6a1-b17ae4d2218b', NULL, N'211', N'212', NULL, NULL, N'L1', N'N1', N'2', NULL, NULL, NULL, N'Thiếu úy', N'Trung úy', 36, 46, 46, N'B01', N'B02', NULL, N'L1-N1')
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'ef4e4a87-7e54-4847-96fc-180b999c1fa0', NULL, N'213', N'214', NULL, NULL, N'L1', N'N1', N'2', NULL, NULL, NULL, N'Thượng úy', N'Đại úy', 36, 46, 46, N'B03', N'B04', NULL, N'L1-N1')
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'94611bc3-4091-4209-88c3-675c704239d6', NULL, N'212', N'213', NULL, NULL, N'L1', N'N1', N'2', NULL, NULL, NULL, N'Trung úy', N'Thượng úy', 36, 46, 46, N'B02', N'B03', NULL, N'L1-N1')
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'36f7cdaf-3565-4f6a-941a-fc6655028673', NULL, N'214', N'214', NULL, NULL, N'L1', N'N1', N'2', NULL, NULL, NULL, N'Đại úy', N'Đại úy', 36, 46, 46, N'B04', N'B05', NULL, N'L1-N1')
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'7b6d68f1-5fb8-43bd-8b0e-f70b6ca183d3', NULL, N'214', N'221', NULL, NULL, N'L1', N'N1', N'2', NULL, NULL, NULL, N'Đại úy', N'Thiếu tá', 36, 46, 46, N'B05', N'B06', NULL, N'L1-N1')
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'f07a47f9-a6b1-4d13-bcc0-06f6376b9acc', NULL, N'221', N'221', NULL, NULL, N'L1', N'N1', N'2', NULL, NULL, NULL, N'Thiếu tá', N'Thiếu tá', 36, 46, 46, N'B06', N'B07', NULL, N'L1-N1')
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'7d1e45a6-b686-4028-bc4f-a112c4b7997c', NULL, N'221', N'222', NULL, NULL, N'L1', N'N1', N'2', NULL, NULL, NULL, N'Thiếu tá', N'Trung tá', 36, 46, 46, N'B07', N'B08', NULL, N'L1-N1')
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'6658398a-7062-4a9c-915a-36ddea49adac', NULL, N'222', N'222', NULL, NULL, N'L1', N'N1', N'2', NULL, NULL, NULL, N'Trung tá', N'Trung tá', 36, 46, 46, N'B08', N'B09', NULL, N'L1-N1')
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'7ba64cc9-a3c1-4d45-a11d-171e4177bea8', NULL, N'222', N'222', NULL, NULL, N'L1', N'N1', N'2', NULL, NULL, NULL, N'Trung tá', N'Trung tá', 36, 46, 46, N'B09', N'B10', NULL, N'L1-N1')
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'df37e10a-906b-4da8-a900-01dd0ab91dbc', NULL, N'222', N'223', NULL, NULL, N'L1', N'N1', N'2', NULL, NULL, NULL, N'Trung tá', N'Thượng tá', 36, 46, 46, N'B10', N'B11', NULL, N'L1-N1')
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'f1e7ae2d-324d-44fb-bd94-b23651756689', NULL, N'223', N'223', NULL, NULL, N'L1', N'N1', N'2', NULL, NULL, NULL, N'Thượng tá', N'Thượng tá', 36, 46, 46, N'B12', N'B12', NULL, N'L1-N1')
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'7d48aee8-824a-41fc-8af4-990ef6ddc915', NULL, N'223', N'223', NULL, NULL, N'L1', N'N1', N'2', NULL, NULL, NULL, N'Thượng tá', N'Thượng tá', 36, 46, 46, N'B11', N'B12', NULL, N'L1-N1')
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'e4fd7eca-9485-4279-a797-57406f58e60f', NULL, N'211', N'212', NULL, NULL, N'L1', N'N2', N'2', NULL, NULL, NULL, N'Thiếu úy', N'Trung úy', 36, 46, 46, N'B01', N'B02', NULL, N'L1-N2')
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'694c85d2-8d1e-44e0-a073-5237581a8507', NULL, N'212', N'212', NULL, NULL, N'L1', N'N2', N'2', NULL, NULL, NULL, N'Trung úy', N'Trung úy', 36, 46, 46, N'B02', N'B03', NULL, N'L1-N2')
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'b1e4a39b-85e6-40b3-be2b-b8aa72420f1a', NULL, N'212', N'213', NULL, NULL, N'L1', N'N2', N'2', NULL, NULL, NULL, N'Trung úy', N'Thượng úy', 36, 46, 46, N'B03', N'B04', NULL, N'L1-N2')
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'a9388a64-3eb7-4d59-87b5-1345092a7db4', NULL, N'213', N'214', NULL, NULL, N'L1', N'N2', N'2', NULL, NULL, NULL, N'Thượng úy', N'Đại úy', 36, 46, 46, N'B04', N'B05', NULL, N'L1-N2')
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'1794ba57-9252-41aa-9001-585c85a52793', NULL, N'214', N'221', NULL, NULL, N'L1', N'N2', N'2', NULL, NULL, NULL, N'Đại úy', N'Thiếu tá', 36, 46, 46, N'B05', N'B06', NULL, N'L1-N2')
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'36730429-cc45-4478-a0b8-3e66e1dd3ec5', NULL, N'221', N'221', NULL, NULL, N'L1', N'N2', N'2', NULL, NULL, NULL, N'Thiếu tá', N'Thiếu tá', 36, 46, 46, N'B06', N'B07', NULL, N'L1-N2')
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'b88bc8f7-506b-4cdc-a145-16e94c28ba84', NULL, N'221', N'222', NULL, NULL, N'L1', N'N2', N'2', NULL, NULL, NULL, N'Thiếu tá', N'Trung tá', 36, 46, 46, N'B07', N'B08', NULL, N'L1-N2')
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'0888e4fe-1e4b-4e93-ac6b-f7b806d51d6a', NULL, N'222', N'222', NULL, NULL, N'L1', N'N2', N'2', NULL, NULL, NULL, N'Trung tá', N'Trung tá', 36, 46, 46, N'B08', N'B09', NULL, N'L1-N2')
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'04ba21e5-47bc-49c6-b967-f6bb7870d651', NULL, N'222', N'223', NULL, NULL, N'L1', N'N2', N'2', NULL, NULL, NULL, N'Trung tá', N'Thượng tá', 36, 46, 46, N'B09', N'B10', NULL, N'L1-N2')
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'19d78956-4e65-4bd7-9e1d-998009df2110', NULL, N'223', N'223', NULL, NULL, N'L1', N'N2', N'2', NULL, NULL, NULL, N'Thượng tá', N'Thượng tá', 36, 46, 46, N'B10', N'B11', NULL, N'L1-N2')
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'f88b65b7-0173-40c3-9515-6b76f531ad0b', NULL, N'223', N'223', NULL, NULL, N'L1', N'N2', N'2', NULL, NULL, NULL, N'Thượng tá', N'Thượng tá', 36, 46, 46, N'B11', N'B12', NULL, N'L1-N2')
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'a36ff870-ba8c-406d-8586-032425c8f2c5', NULL, N'223', N'223', NULL, NULL, N'L1', N'N2', N'2', NULL, NULL, NULL, N'Thượng tá', N'Thượng tá', 36, 46, 46, N'B12', N'B12', NULL, N'L1-N2')
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'8559abe6-8f1f-40c4-9dae-512229487fad', NULL, N'211', N'211', NULL, NULL, N'L2', N'N1', N'2', NULL, NULL, NULL, N'Thiêu úy', N'Thiếu úy', 36, 46, 46, N'B01', N'B01', NULL, N'L2-N1')
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'85b44787-268d-4f65-bb7e-e37c57f5a0a5', NULL, N'211', N'212', NULL, NULL, N'L2', N'N1', N'2', NULL, NULL, NULL, N'Thiêu úy', N'Trung úy', 36, 46, 46, N'B02', N'B03', NULL, N'L2-N1')
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'b171051a-cab8-4c4d-b438-6469df177217', NULL, N'212', N'212', NULL, NULL, N'L2', N'N1', N'2', NULL, NULL, NULL, N'Trung úy', N'Trung úy', 36, 46, 46, N'B03', N'B04', NULL, N'L2-N1')
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'4115ffd5-bf9e-4939-aa35-928f31099807', NULL, N'212', N'213', NULL, NULL, N'L2', N'N1', N'2', NULL, NULL, NULL, N'Trung úy', N'Thượng úy', 36, 46, 46, N'B03', N'B05', NULL, N'L2-N1')
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'5dec82a5-bfe7-4be2-9780-6684785d4cf0', NULL, N'213', N'214', NULL, NULL, N'L2', N'N1', N'2', NULL, NULL, NULL, N'Thượng úy', N'Đại úy', 36, 46, 46, N'B05', N'B06', NULL, N'L2-N1')
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'2ade880e-c958-4bcf-8d9f-8206ceeeafe2', NULL, N'214', N'221', NULL, NULL, N'L2', N'N1', N'2', NULL, NULL, NULL, N'Đại úy', N'Thiếu tá', 36, 46, 46, N'B06', N'B07', NULL, N'L2-N1')
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'577aa39d-1b2b-4840-8e66-aa50ca182175', NULL, N'221', N'221', NULL, NULL, N'L2', N'N1', N'2', NULL, NULL, NULL, N'Thiếu tá', N'Thiếu tá', 36, 46, 46, N'B07', N'B08', NULL, N'L2-N1')
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'1a71b9ae-305f-425c-9310-3db4a0f07e16', NULL, N'221', N'221', NULL, NULL, N'L2', N'N1', N'2', NULL, NULL, NULL, N'Thiếu tá', N'Thiếu tá', 36, 46, 46, N'B08', N'B09', NULL, N'L2-N1')
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'333fd1d8-61a3-4462-8aec-5bfb45cdfab5', NULL, N'221', N'222', NULL, NULL, N'L2', N'N1', N'2', NULL, NULL, NULL, N'Thiếu tá', N'Trung tá', 36, 46, 46, N'B09', N'B10', NULL, N'L2-N1')
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'3bb0f29a-a7f6-41b5-a8cf-99f384ac6b6d', NULL, N'222', N'222', NULL, NULL, N'L2', N'N1', N'2', NULL, NULL, NULL, N'Trung tá', N'Trung tá', 36, 46, 46, N'B10', N'B10', NULL, N'L2-N1')
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'e62ccc7a-16c9-4cee-b5f0-706811009f7f', NULL, N'211', N'211', NULL, NULL, N'L3', N'N1', N'2', NULL, NULL, NULL, N'Thiếu úy', N'Thiếu úy', 36, 46, 46, N'B01', N'B02', NULL, N'L3-N1')
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'6bf43c95-f571-40c5-8e3e-488ccb6c0701', NULL, N'211', N'211', NULL, NULL, N'L3', N'N1', N'2', NULL, NULL, NULL, N'Thiếu úy', N'Thiếu úy', 36, 46, 46, N'B02', N'B03', NULL, N'L3-N1')
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'73f39957-c186-42ac-9057-801b29d0068f', NULL, N'211', N'212', NULL, NULL, N'L3', N'N1', N'2', NULL, NULL, NULL, N'Thiếu úy', N'Trung úy', 36, 46, 46, N'B03', N'B04', NULL, N'L3-N1')
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'43acf559-d5ca-4ae0-ae2d-13e69be4e224', NULL, N'212', N'212', NULL, NULL, N'L3', N'N1', N'2', NULL, NULL, NULL, N'Trung úy', N'Trung úy', 36, 46, 46, N'B04', N'B05', NULL, N'L3-N1')
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'7b9ef342-b674-4faa-a216-63c8b0d6170d', NULL, N'212', N'213', NULL, NULL, N'L3', N'N1', N'2', NULL, NULL, NULL, N'Trung úy', N'Thượng úy', 36, 46, 46, N'B05', N'B06', NULL, N'L3-N1')
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'b178dcde-9f4d-4f19-b155-1ccba9b0276e', NULL, N'213', N'213', NULL, NULL, N'L3', N'N1', N'2', NULL, NULL, NULL, N'Thượng úy', N'Thượng úy', 36, 46, 46, N'B06', N'B07', NULL, N'L3-N1')
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'd403ba8d-579f-410c-a245-f3bc00433729', NULL, N'213', N'214', NULL, NULL, N'L3', N'N1', N'2', NULL, NULL, NULL, N'Thượng úy', N'Đại úy', 36, 46, 46, N'B07', N'B08', NULL, N'L3-N1')
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'037a27a1-c04b-4b80-ba70-89d29780eb7a', NULL, N'214', N'214', NULL, NULL, N'L3', N'N1', N'2', NULL, NULL, NULL, N'Đại úy', N'Đại úy', 36, 46, 46, N'B08', N'B09', NULL, N'L3-N1')
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'3af312b0-aa6a-44cf-bf34-4e958b3abcdc', NULL, N'214', N'221', NULL, NULL, N'L3', N'N1', N'2', NULL, NULL, NULL, N'Đại úy', N'Thiếu úy', 36, 46, 46, N'B09', N'B10', NULL, N'L3-N1')
GO
INSERT [dbo].[TL_DM_CapBac_KeHoach_NQ104] ([ID], [HsVk], [Ma_Cb], [Ma_Cb_KeHoach], [MoTa], [iNamLamViec], [Loai], [Nhom], [Parent], [PCRQ_TT], [Readonly], [Splits], [Ten_Cb], [Ten_Cb_KeHoach], [Thoi_Han_Tang], [Tuoi_Huu_Nam], [Tuoi_Huu_Nu], [Ma_BacLuong], [Ma_BacLuong_KeHoach], [Ma_BacLuong_Tran], [LoaiNhom]) VALUES (N'31ffcc38-cf9a-4a7a-ae0e-c4a6a9167416', NULL, N'221', N'221', NULL, NULL, N'L3', N'N1', N'2', NULL, NULL, NULL, N'Thiếu tá', N'Thiếu tá', 36, 46, 46, N'B10', N'B10', NULL, N'L3-N1')
GO
