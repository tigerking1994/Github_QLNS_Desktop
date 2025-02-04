
-- Xóa dữ liệu mục lục hiện tại đã có trong master data 2025
delete cu
from ns_skt_mucluc cu
join ns_skt_mucluc_hd4554 moi on 
(cu.sKyHieu = moi.sKyHieu or cu.skyhieu = moi.skyhieucu or cu.iID = moi.iID) 
and cu.iNamLamViec = moi.iNamLamViec
where cu.iNamLamViec = 2025
go

-- Xóa dữ liệu mục lục 2025 hiện tại đã có trong master data 2024
delete cu
from ns_skt_mucluc cu
join ns_skt_mucluc_2024 moi on 
(cu.sKyHieu = moi.sKyHieu or cu.skyhieu = moi.skyhieucu) 
and cu.iNamLamViec = 2025

go

-- Thêm dữ liệu master data
INSERT INTO [dbo].[NS_SKT_MucLuc]
           ([iID]
           ,[bHangCha]
           ,[dNgaySua]
           ,[dNgayTao]
           ,[dNguoiSua]
           ,[dNguoiTao]
           ,[iID_MLSKT]
           ,[iID_MLSKTCha]
           ,[iNamLamViec]
           ,[iTrangThai]
           ,[KyHieuCha]
           ,[Log]
           ,[Muc]
           ,[sKyHieu]
           ,[sLoaiNhap]
           ,[sM]
           ,[sMoTa]
           ,[sNG_Cha]
           ,[sNG]
           ,[sSTT]
           ,[sSTTBC]
           ,[Tag]
           ,[sKyHieuCu]
           ,[SL]
           ,[SK])
SELECT [iID]
      ,[bHangCha]
      ,[dNgaySua]
      ,[dNgayTao]
      ,[dNguoiSua]
      ,[dNguoiTao]
      ,[iID_MLSKT]
      ,[iID_MLSKTCha]
	  ,[iNamLamViec]
      ,[iTrangThai]
      ,[KyHieuCha]
      ,[Log]
      ,[Muc]
      ,[sKyHieu]
      ,[sLoaiNhap]
      ,[sM]
      ,[sMoTa]
      ,[sNG_Cha]
      ,[sNG]
      ,[sSTT]
      ,[sSTTBC]
      ,[Tag]
      ,[sKyHieuCu]
	  ,[sL]
      ,[sK]
  FROM [dbo].[NS_SKT_MucLuc_HD4554]
  where sKyHieu not in (select sKyHieu from [dbo].NS_SKT_MucLuc where iNamLamViec = 2025);
go


-- Cập nhật lại id cha con
update con
set con.iID_MLSKTCha = cha.iID_MLSKT
from ns_skt_mucluc con
join ns_skt_mucluc cha 
on cha.sKyHieu = con.KyHieuCha 
and cha.inamlamviec = con.inamlamviec 
and cha.iID_MLSKT <> con.iID_MLSKTCha
where cha.inamlamviec = 2025
go

-- Cập nhật lại số ký hiệu cũ
update sktcu
set sktcu.sKyHieuCu = sktcu.sKyHieu
from ns_skt_mucluc sktcu
where sktcu.inamlamviec = 2025
and isnull(sktcu.sKyHieuCu, '') = ''
go

-- Cập nhật lại dữ liệu chứng từ chi tiết
update ctct
set ctct.sKyHieu = skt.sKyHieu, ctct.sMoTa = skt.sMoTa, ctct.iID_MLSKT = skt.iID_MLSKT
from NS_SKT_ChungTuChiTiet ctct
join NS_SKT_MucLuc skt 
on (ctct.sKyHieu = skt.sKyHieuCu or ctct.sKyHieu = skt.sKyHieu)
and skt.iNamLamViec = ctct.iNamLamViec
where ctct.iNamLamViec = 2025
go

-- Cập nhật lại dữ liệu chứng từ chi tiết căn cứ
update ctct
set ctct.sKyHieu = skt.sKyHieu, ctct.iID_MLSKT = skt.iID_MLSKT
from NS_SKT_ChungTuChiTiet_CanCu ctct
join NS_SKT_MucLuc skt 
on (ctct.sKyHieu = skt.sKyHieuCu or ctct.sKyHieu = skt.sKyHieu)
and skt.iNamLamViec = ctct.iNamLamViec
where ctct.iNamLamViec = 2025
go

update NS_SKT_MucLuc
set iID_MLSKT = '55A496DE-4AB1-4830-9AE0-E45299A13048'
where iID = '66A27F13-48C5-43A4-B3DB-8A5FB3E5278E' and iNamLamViec = 2025
go

update NS_SKT_MucLuc_HD4554
set iID_MLSKT = '55A496DE-4AB1-4830-9AE0-E45299A13048'
where iID = '66A27F13-48C5-43A4-B3DB-8A5FB3E5278E' and iNamLamViec = 2025
go


WITH CTE AS(
   SELECT [iiID_CTSoKiemTra], [iid_CanCu], [sKyHieu],
       RN = ROW_NUMBER()OVER(PARTITION BY iiID_CTSoKiemTra, iid_CanCu, sKyHieu, iNamLamViec ORDER BY iiID_CTSoKiemTra, iid_CanCu, sKyHieu, iNamLamViec)
   FROM NS_SKT_ChungTuChiTiet_CanCu
   WHERE iNamLamViec in (2023, 2024, 2025)
)

DELETE FROM CTE WHERE RN > 1
GO

-- Xóa bản ghi trùng nhau
WITH CTE AS(
SELECT
	RN = ROW_NUMBER()OVER(PARTITION BY sSKT_KyHieu, sNS_XauNoiMa, iNamLamViec ORDER BY sSKT_KyHieu, sNS_XauNoiMa, iNamLamViec)
FROM NS_MLSKT_MLNS
WHERE iNamLamViec in (2023, 2024, 2025)
)

DELETE FROM CTE WHERE RN > 1
GO


