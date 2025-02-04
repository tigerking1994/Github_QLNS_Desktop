update TL_DM_Cach_TinhLuong_TruyLinh set Ma_Cot = 'PCTQ_TT' WHERE Ma_CachTL='CACH5' AND Ma_Cot = 'TTL_PCTQ'  
update TL_DM_PhuCap set Ten_PhuCap=N'Hệ số phụ cấp thủ quỹ cũ', iDinhDang = 1 where Ma_PhuCap = 'PCTQ_HS_CU'  
update TL_DM_PhuCap set iDinhDang = 2 where Ma_PhuCap = 'TTL_PCTQ'