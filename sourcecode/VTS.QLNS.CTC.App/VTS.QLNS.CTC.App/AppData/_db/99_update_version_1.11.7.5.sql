update TL_DM_PhuCap set Ma_PhuCap ='TTL_PCTQ', Ten_PhuCap=N'Số tháng truy lĩnh PC thủ quỹ' where Ma_PhuCap = 'PCTQ_TT_TL' 
update TL_DM_PhuCap set Chon = 1 where Ma_PhuCap in ('TTL_LHT', 'TTL_PCCOV', 'TTL_PCCU', 'TTL_PCCV', 'TTL_PCTHUHUT', 'TTL_PCTQ') 
update TL_DM_PhuCap set Chon = 0 where Ma_PhuCap in ('TTL', 'PCTQ_TT_CU') 
update TL_DM_PhuCap set Ten_PhuCap = N'Hệ số phụ cấp báo cáo viên' where Ma_PhuCap = 'PCBCV_HS' 
update TL_DM_PhuCap set Ten_PhuCap = N'Phụ cấp báo cáo viên' where Ma_PhuCap = 'PCBCV_TT' 
 
update TL_DM_Cach_TinhLuong_TruyLinh set Ma_Cot = 'TTL_PCTQ', CongThuc = '(PCTQ_HS-PCTQ_HS_CU)*LCS*TTL_PCTQ' WHERE Ma_CachTL='CACH5' AND Ma_Cot = 'PCTQ_TT_TL' 
update TL_DM_Cach_TinhLuong_TruyLinh set CongThuc = 'PCCU_TT+PCTHUHUT_TT+PCTQ_TT' WHERE Ma_Cot = 'TRUYLINHKHAC_SUM'