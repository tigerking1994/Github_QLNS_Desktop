update ns_skt_mucluc 
set skyhieucu = skyhieu
where inamlamviec > 2024
go

update DM_ChuKy
set TieuDe1_MoTa = N'Báo cáo điều chỉnh dự toán chi ngân sách năm 2023',
TieuDe2_MoTa = N'(Kèm theo báo cáo số...../..... ngày ..../...../2022 của....)',
TieuDe3_MoTa = N''
where id_code = 'rptNS_DuToan_DieuChinh_TONGHOP2'
go

UPDATE a
SET a.iID_MLNS_Cha = b.iID_MLNS
FROM ns_muclucngansach a
INNER JOIN ns_muclucngansach b
	ON (a.sXauNoiMa LIKE CONCAT(b.sXauNoiMa, '-', '%')
	AND ((LEN(a.sXauNoiMa) - LEN(b.sxaunoima) = 3)
	OR (LEN(a.sXauNoiMa) - LEN(b.sxaunoima) = 4)
	OR (LEN(a.sXauNoiMa) - LEN(b.sxaunoima) = 5)))
	OR (a.sXauNoiMa LIKE CONCAT(b.sXauNoiMa, '%')
	AND ((LEN(a.sXauNoiMa) = 3
	AND LEN(b.sxaunoima) = 1)
	OR (LEN(a.sXauNoiMa) = 7
	AND LEN(b.sxaunoima) = 3)))
	AND a.iNamLamViec = b.iNamLamViec
WHERE a.iID_MLNS_Cha IS NULL
go

