
update ct
set ct.sXauNoiMa = dm.sXauNoiMa
from BH_KHC_KinhPhiQuanLy_ChiTiet ct
join BH_DM_MucLucNganSach dm 
on ct.sM = dm.sM
and ct.sTM = dm.sTM
and ct.iNamLamViec = dm.iNamLamViec
and ct.sXauNoiMa LIKE '9010003%'
and dm.sXauNoiMa LIKE '9010003%'
and len(ct.sXauNoiMa) = len(dm.sXauNoiMa)
and ct.sXauNoiMa <> dm.sXauNoiMa

update ct
set ct.sXauNoiMa = dm.sXauNoiMa
from BH_DTC_DieuChinhDuToanChi_ChiTiet ct
join BH_DM_MucLucNganSach dm 
on ct.sM = dm.sM
and ct.sTM = dm.sTM
and ct.iNamLamViec = dm.iNamLamViec
and ct.sXauNoiMa LIKE '9010003%'
and dm.sXauNoiMa LIKE '9010003%'
and len(ct.sXauNoiMa) = len(dm.sXauNoiMa)
and ct.sXauNoiMa <> dm.sXauNoiMa

update ct
set ct.sXauNoiMa = dm.sXauNoiMa
from BH_QTC_Quy_KinhPhiQuanLy_ChiTiet ct
join BH_DM_MucLucNganSach dm 
on ct.sM = dm.sM
and ct.sTM = dm.sTM
and ct.iNamLamViec = dm.iNamLamViec
and ct.sXauNoiMa LIKE '9010003%'
and dm.sXauNoiMa LIKE '9010003%'
and len(ct.sXauNoiMa) = len(dm.sXauNoiMa)
and ct.sXauNoiMa <> dm.sXauNoiMa

update ct
set ct.sXauNoiMa = dm.sXauNoiMa
from BH_QTC_Nam_KinhPhiQuanLy_ChiTiet ct
join BH_DM_MucLucNganSach dm 
on ct.sM = dm.sM
and ct.sTM = dm.sTM
and ct.iNamLamViec = dm.iNamLamViec
and ct.sXauNoiMa LIKE '9010003%'
and dm.sXauNoiMa LIKE '9010003%'
and len(ct.sXauNoiMa) = len(dm.sXauNoiMa)
and ct.sXauNoiMa <> dm.sXauNoiMa



