update TL_DM_PhuCap set iDinhDang = 0 where Ma_PhuCap in ('TRICHLUONG_TIEN', 'TRICHQUYKHAC_TT', 'TRICHQUYPCTT_TT')
update TL_DM_PhuCap set iLoai = null where Ma_PhuCap = 'LHT_TT'

update TL_DM_Cach_TinhLuong_Chuan set CongThuc = 'TA_THANG' where Ma_Cot = 'TA_TONG'
update TL_DM_Cach_TinhLuong_Chuan 
set CongThuc = 'PC10+PCTQ_TT+PCKHAC2_TT+PCKHAC3_TT+PCDT_TT+PCTHANHTRA_TT+PCNU_TT+PCTEMTHU_TT+PCDTQUANSU_TT+PCDTN_TT+PCCU_TT+PCHOIPHUNU_TT+PCCONGDOAN_TT+PCANQP_TT+PCGS_TT+PCTS_TT+PCPGS_TT+PCBCV_TT+TA_TT'
where Ma_Cot = 'PCKHAC_SUM'