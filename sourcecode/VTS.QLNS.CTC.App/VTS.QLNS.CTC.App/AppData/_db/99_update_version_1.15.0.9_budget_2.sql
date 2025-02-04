delete map
from NS_MLSKT_MLNS map
join NS_SKT_MucLuc mucluc on map.sSKT_KyHieu = mucluc.sKyHieuCu and map.iNamLamViec = mucluc.iNamLamViec
where map.iNamLamViec = 2025 and mucluc.sKyHieu <> mucluc.sKyHieuCu