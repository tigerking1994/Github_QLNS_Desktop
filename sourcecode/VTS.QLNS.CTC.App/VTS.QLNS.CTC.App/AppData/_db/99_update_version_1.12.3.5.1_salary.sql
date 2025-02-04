IF NOT EXISTS (SELECT * FROM TL_DM_PhuCap WHERE Ma_PhuCap='BHXHDVCS_HS')
INSERT INTO TL_DM_PhuCap(bGiaTri, bHuongPc_Sn, bSaoChep, Chon, Dinh_Dang, Gia_Tri, iDinhDang, Is_Formula, Is_Readonly, Ma_PhuCap, Parent, Ten_PhuCap, Tinh_BHXH, Xau_Noi_Ma) VALUES
(1, 1, 0, 1, 0, 0.175, 1, 0, 0, 'BHXHDVCS_HS', 'BHDV', N'Bảo hiểm xã hội đơn vị đóng cho chiến sĩ (hệ số)', 1, 'BHDV-BHXHDVCS_HS')
GO

IF NOT EXISTS (SELECT * FROM TL_DM_PhuCap WHERE Ma_PhuCap='BHYTDVCS_TT')
INSERT INTO TL_DM_PhuCap(bGiaTri, bHuongPc_Sn, bSaoChep, Chon, Dinh_Dang, Gia_Tri, iDinhDang, Is_Formula, Is_Readonly, Ma_PhuCap, Parent, Ten_PhuCap, Tinh_BHXH, Xau_Noi_Ma) VALUES
(1, 1, 0, 1, 0, 0.000, NULL, 1, 0, 'BHYTDVCS_TT', 'BHDV', N'Bảo hiểm y tế đơn vị đóng cho chiến sĩ (thành tiền)', 1, 'BHDV-BHYTDVCS_TT')
GO

IF NOT EXISTS (SELECT * FROM TL_DM_PhuCap WHERE Ma_PhuCap='BHXHDVCS_TT')
INSERT INTO TL_DM_PhuCap(bGiaTri, bHuongPc_Sn, bSaoChep, Chon, Dinh_Dang, Gia_Tri, iDinhDang, Is_Formula, Is_Readonly, Ma_PhuCap, Parent, Ten_PhuCap, Tinh_BHXH, Xau_Noi_Ma) VALUES
(1, 1, 0, 1, 0, 0.000, NULL, 1, 0, 'BHXHDVCS_TT', 'BHDV', N'Bảo hiểm xã hội đơn vị đóng cho chiến sĩ (thành tiền)', 1, 'BHDV-BHXHDVCS_TT')
GO

IF NOT EXISTS (SELECT * FROM TL_DM_PhuCap WHERE Ma_PhuCap='BHYTDVCS_HS')
INSERT INTO TL_DM_PhuCap(bGiaTri, bHuongPc_Sn, bSaoChep, Chon, Dinh_Dang, Gia_Tri, iDinhDang, Is_Formula, Is_Readonly, Ma_PhuCap, Parent, Ten_PhuCap, Tinh_BHXH, Xau_Noi_Ma) VALUES
(1, 1, 0, 1, 0, 0.030, 1, 0, 0, 'BHYTDVCS_HS', 'BHDV', N'Bảo hiểm y tế đơn vị đóng cho chiến sĩ (hệ số)', 1, 'BHDV-BHYTDVCS_HS')
GO

IF NOT EXISTS (SELECT * FROM TL_DM_Cach_TinhLuong_Chuan WHERE Ma_Cot='BHXHDVCS_TT')
INSERT INTO TL_DM_Cach_TinhLuong_Chuan(CongThuc, Ma_CachTL, Ma_Cot, Nam, Ten_Cot, Thang) VALUES
('BHXHDVCS_HS*LCS', 'CACH0', 'BHXHDVCS_TT', 2022, N'Bảo hiểm xã hội đơn vị đóng cho chiến sĩ (thành tiền)', 11)
GO

IF NOT EXISTS (SELECT * FROM TL_DM_Cach_TinhLuong_Chuan WHERE Ma_Cot='BHYTDVCS_TT')
INSERT INTO TL_DM_Cach_TinhLuong_Chuan(CongThuc, Ma_CachTL, Ma_Cot, Nam, Ten_Cot, Thang) VALUES
('BHYTDVCS_HS*LCS', 'CACH0', 'BHYTDVCS_TT', 2022, N'Bảo hiểm y tế đơn vị đóng cho chiến sĩ (thành tiền)', 11)
GO