INSERT INTO [dbo].[DM_ChuKy]
([Id]
,[bDanhSach]
,[iTrangThai]
,[Id_Code]
,[Id_Type]
,[sLoai]
,[Ten]
,[TieuDe1]
,[TieuDe1_MoTa]
,[TieuDe2_MoTa]
)
values (newid(), 1,1,'rptNS_QuyetToanNam_TongHop','rptNS_QuyetToanNam_TongHop','QUYET_TOAN_NAM',N'Tổng hợp quyết toán năm HD 6789',
'1',N'Báo cáo quyết toán chi ngân sách nhà nước và nguồn kinh phí khác',N'Năm')

INSERT INTO [dbo].[DM_ChuKy]
([Id]
,[bDanhSach]
,[iTrangThai]
,[Id_Code]
,[Id_Type]
,[sLoai]
,[Ten]
,[TieuDe1]
,[TieuDe1_MoTa]
,[TieuDe2_MoTa]
)
values (newid(), 1,1,'rptNS_QuyetToanNam','rptNS_QuyetToanNam','QUYET_TOAN_NAM',N'Tổng hợp quyết toán năm HD 8568',
'1',N'Báo cáo quyết toán kinh phí năm' ,N'(Ngân sách BĐ, sử dụng, ngân sách nhà nước)')

INSERT INTO [dbo].[DM_ChuKy]
([Id]
,[bDanhSach]
,[iTrangThai]
,[Id_Code]
,[Id_Type]
,[sLoai]
,[Ten]
,[TieuDe1]
,[TieuDe1_MoTa]
,[TieuDe2_MoTa]
)
values (newid(), 1,1,'rptNS_QuyetToanNam_XetDuyetQuyetToanNam','rptNS_QuyetToanNam_XetDuyetQuyetToanNam','QUYET_TOAN_NAM',N'Tổng hợp xét duyệt quyết toán năm HD 8063',
'1',N'Báo cáo quyết toán kinh phí năm' ,N'(Ngân sách BĐ, sử dụng, ngân sách nhà nước)')

-- xóa lns 1020000
delete NS_MucLucNganSach
where sXauNoiMa not in (select mt.sXauNoiMa from NS_MucLucNganSach_Khoi as mt)
and iNamLamViec = 2023
and sLNS in ('1020000')
and sXauNoiMa not in
(
select CONCAT(sLNS, '-', sL,'-',sK,'-',sM, '-', sTM, '-', sTTM,'-', sNG) from NS_BK_ChungTuChiTiet
where iNamLamViec = 2023
and sNG !=''
union
select CONCAT(sLNS, '-', sL,'-',sK,'-',sM, '-', sTM, '-', sTTM,'-', sNG) from NS_CP_ChungTuChiTiet
where iNamLamViec = 2023
and sNG !=''
union
select CONCAT(sLNS, '-', sL,'-',sK,'-',sM, '-', sTM, '-', sTTM,'-', sNG) from NS_DC_ChungTuChiTiet
where iNamLamViec = 2023
and sNG !=''
union
select CONCAT(sLNS, '-', sL,'-',sK,'-',sM, '-', sTM, '-', sTTM,'-', sNG) from NS_DT_ChungTuChiTiet
where iNamLamViec = 2023
and sNG !=''
union
select CONCAT(sLNS, '-', sL,'-',sK,'-',sM, '-', sTM, '-', sTTM,'-', sNG) from NS_DTDauNam_ChungTuChiTiet
where iNamLamViec = 2023
and sNG !=''
union
select CONCAT(sLNS, '-', sL,'-',sK,'-',sM, '-', sTM, '-', sTTM,'-', sNG) from NS_DTDauNam_ChungTuChiTiet_CanCu
where iNamLamViec = 2023
and sNG !=''
union

select CONCAT(sLNS, '-', sL,'-',sK,'-',sM, '-', sTM, '-', sTTM,'-', sNG) from NS_Nganh_ChungTuChiTiet
where iNamLamViec = 2023
and sNG !=''
union
select CONCAT(sLNS, '-', sL,'-',sK,'-',sM, '-', sTM, '-', sTTM,'-', sNG) from NS_QT_ChungTuChiTiet
where iNamLamViec = 2023
and sNG !=''

)
and sNG !=''