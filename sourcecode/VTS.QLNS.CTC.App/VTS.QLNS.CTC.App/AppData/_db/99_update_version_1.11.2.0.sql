INSERT [TL_DM_PhuCap] ([Ma_PhuCap], [Ten_PhuCap], [Parent],  [Is_Formula], [Chon], [Tinh_BHXH], [Dinh_Dang], [Xau_Noi_Ma], [HuongPC_SN], [IThang_ToiDa], [iLoai], [bGiaTri], [bHuongPc_Sn], [iDinhDang], [bSaoChep]) 
VALUES ( 'PCTQ_HS', N'Hệ số phụ cấp thủ quỹ', 'PCDACTHU_HS',0, 1, 1, 0, N'PCDACTHU_HS-PCTQ_HS', NULL, NULL, NULL, 1, 1, 1, 0),
		('PCBCV_HS','Hệ số phụ cấp báo cáo viên','PCDACTHU_HS',0, 1, 1, 0, N'PCDACTHU_HS-PCBCV_HS',NULL, NULL, NULL, 1, 1, 1, 0),
		('PCBCV_TT','Phụ cấp báo cáo viên','PCDACTHU_TT',1, 1, 1, 0, N'PCDACTHU_TT-PCBCV_TT', NULL, NULL, 2, 1, 1, 0, 0),
		('PCTQ_HS_CU','Hệ số phụ cấp thủ quỹ cũ','TRUYLINH',1, 1, 1, 0, N'TRUYLINH-PCTQ_HS_CU', NULL, NULL, NULL, 1, 1, 0, 0)
											
INSERT [TL_DM_Cach_TinhLuong_Chuan] ([Ma_CachTL], [Ma_Cot], [Ten_Cot], [CongThuc], [Thang], [Nam]) 
VALUES ('CACH0', 'PCTQ_TT', N'Phụ cấp thủ quỹ', N'PCTQ_HS*LCS', 6, 2022),
		('CACH0', 'PCBCV_TT', N'Phụ cấp báo cáo viên', N'PCBCV_HS*LCS', 6, 2022)


Update [TL_DM_Cach_TinhLuong_Chuan]
set CongThuc = 'PC10+PCTQ_TT+PCKHAC2_TT+PCKHAC3_TT+PCDT_TT+PCTHANHTRA_TT+PCNU_TT+PCTEMTHU_TT+PCDTQUANSU_TT+PCDTN_TT+PCCU_TT+PCHOIPHUNU_TT+PCCONGDOAN_TT+PCANQP_TT+PCGS_TT+PCTS_TT+PCPGS_TT+PCBCV_TT'
where Ma_Cot = 'PCKHAC_SUM'


Update [TL_DM_Cach_TinhLuong_Chuan]
set CongThuc = 'LHT_TT+PCTN_TT+PCTNVK_TT+PCCV_TT+PCCOV_TT+PCTRA_SUM+PCDACTHU_SUM+PCKHAC_SUM+HSBL_TT+PCKV_TT+PCBVBG_TT'
where Ma_Cot = 'LUONGTHANG_SUM'

Update [TL_DM_Cach_TinhLuong_TruyLinh]
set CongThuc = '(PCTQ_HS-PCTQ_HS_CU)*LCS'
where Ma_Cot = 'PCTQ_TT'

INSERT [TL_DM_MapPC_Detail] ([old_value],[id_phuCap], [ma_phuCap], [ten_phuCap], [giatri]) 
VALUES ('TQ', (select top 1 [TL_DM_PhuCap].Id from [TL_DM_PhuCap] where [TL_DM_PhuCap].Ma_PhuCap = 'PCTQ_HS'), 'PCTQ_HS', N'Hệ số phụ cấp thủ quỹ', CAST(0.1000 AS Numeric(17, 4))),
		('BC',(select top 1 [TL_DM_PhuCap].Id from [TL_DM_PhuCap] where [TL_DM_PhuCap].Ma_PhuCap = 'PCBCV_HS'), 'PCBCV_HS', N'Hệ số phụ cấp báo cáo viên', CAST(0.2000 AS Numeric(17, 4))),
		('BP',(select top 1 [TL_DM_PhuCap].Id from [TL_DM_PhuCap] where [TL_DM_PhuCap].Ma_PhuCap = 'PCBVBG_HS'), 'PCBVBG_HS', N'Hệ số phụ cấp bảo vệ biên giới', CAST(0.1500 AS Numeric(17, 4))),
		('PN',(select top 1 [TL_DM_PhuCap].Id from [TL_DM_PhuCap] where [TL_DM_PhuCap].Ma_PhuCap = 'PCHOIPHUNU_HS'), 'PCHOIPHUNU_HS', N'Hệ số phụ cấp hội phụ nữ', CAST(0.1500 AS Numeric(17, 4))),
		('DV', (select top 1 [TL_DM_PhuCap].Id from [TL_DM_PhuCap] where [TL_DM_PhuCap].Ma_PhuCap = 'PCCONGDOAN_HS'), 'PCCONGDOAN_HS', N'Hệ số phụ cấp công đoàn', CAST(0.1500 AS Numeric(17, 4)))


INSERT [TL_Map_Column_Config] ([Old_Column], [New_Column], [Is_Map_PhuCap], [Is_Map_Value], [Use_PhuCap_Value], [Map_Expression])
VALUES ( N'KV', N'PCKV_HS', 0, 1, 0, N'(KV*1.0)/100.0')
