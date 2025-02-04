update NS_MucLucNganSach
set sMoTa = N'KP nghiệp vụ - Khối BVTC'
where iNamLamViec = 2023
and sXauNoiMa ='10209'

update NS_MucLucNganSach
set sMoTa = N'KP nghiệp vụ - Khối BVTC (Bộ hỗ trợ tự chủ)'
where iNamLamViec = 2023
and sXauNoiMa ='1020901'


update 
NS_MucLucNganSach
set iID_MLNS_Cha = (select iID_MLNS from NS_MucLucNganSach where sXauNoiMa ='10209' and iNamLamViec =2023)
where iNamLamViec = 2023
and sXauNoiMa in ('1020901','1020902','1020903')