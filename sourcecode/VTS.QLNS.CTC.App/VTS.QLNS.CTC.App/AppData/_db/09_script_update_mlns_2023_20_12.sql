update 
NS_MucLucNganSach
set iID_MLNS_Cha = (select iID_MLNS from NS_MucLucNganSach where sXauNoiMa ='101' and iNamLamViec =2023)
where iNamLamViec = 2023
and sXauNoiMa in ('1010100','1010200','1010300','1010400')


update 
NS_MucLucNganSach
set iID_MLNS_Cha = (select iID_MLNS from NS_MucLucNganSach where sXauNoiMa ='102' and iNamLamViec =2023)
where iNamLamViec = 2023
and sXauNoiMa in ('1020600')