SELECT sXauNoiMa INTO #temp
FROM
(
SELECT sXauNoiMa
, DupRank = ROW_NUMBER() OVER (
              PARTITION BY iid_mlns
              ORDER BY (SELECT iid_mlns)
            )
FROM NS_MucLucNganSach where iNamLamViec = 2024
) AS T
WHERE DupRank > 1 

update NS_MucLucNganSach
set log = null

update cha
set cha.log = newid()
from NS_MucLucNganSach cha
left join NS_MucLucNganSach con on cha.iID_MLNS = con.iID_MLNS_Cha
and con.sXauNoiMa like '%' + cha.sXauNoiMa + '%'
and con.iNamLamViec = cha.iNamLamViec
and cha.iNamLamViec = 2024
where cha.sXauNoiMa IN (SELECT * FROM #temp)

update con
set con.iID_MLNS_Cha = cha.log
from NS_MucLucNganSach cha
left join NS_MucLucNganSach con on cha.iID_MLNS = con.iID_MLNS_Cha
and con.sXauNoiMa like '%' + cha.sXauNoiMa + '%'
and con.iNamLamViec = cha.iNamLamViec
and cha.iNamLamViec = 2024
where cha.sXauNoiMa IN (SELECT * FROM #temp)

update NS_MucLucNganSach
set iID_MLNS = log
where isnull(log, '') <> ''

update NS_MucLucNganSach
set log = null

drop table #temp