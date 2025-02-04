UPDATE NS_SKT_MucLuc
set sL = (select top(1) sL from NS_SKT_MucLuc_HD4554 where NS_SKT_MucLuc.sKyHieu = NS_SKT_MucLuc_HD4554.sKyHieu),
sK = (select top(1) sK from NS_SKT_MucLuc_HD4554 where NS_SKT_MucLuc.sKyHieu = NS_SKT_MucLuc_HD4554.sKyHieu),
sM = (select top(1) sM from NS_SKT_MucLuc_HD4554 where NS_SKT_MucLuc.sKyHieu = NS_SKT_MucLuc_HD4554.sKyHieu)
where iNamLamViec = 2024

UPDATE NS_SKT_MucLuc
set sL = (select top(1) sL from NS_SKT_MucLuc_HD4554 where NS_SKT_MucLuc.sKyHieu = NS_SKT_MucLuc_HD4554.sKyHieu),
sK = (select top(1) sK from NS_SKT_MucLuc_HD4554 where NS_SKT_MucLuc.sKyHieu = NS_SKT_MucLuc_HD4554.sKyHieu),
sM = (select top(1) sM from NS_SKT_MucLuc_HD4554 where NS_SKT_MucLuc.sKyHieu = NS_SKT_MucLuc_HD4554.sKyHieu)
where iNamLamViec = 2025