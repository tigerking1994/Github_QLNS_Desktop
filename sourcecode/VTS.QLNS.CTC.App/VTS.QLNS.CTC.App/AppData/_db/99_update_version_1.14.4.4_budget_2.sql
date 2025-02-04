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
SELECT newid()
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

-- update lai sl, sk, stt, ssttbc
update ml
set ml.sl = moi.sl, ml.sk = moi.sk, ml.sstt = moi.sstt, ml.ssttbc = moi.ssttbc, ml.sm = moi.sm, ml.smota = moi.smota, ml.[iID_MLSKT] = moi.[iID_MLSKT], ml.[iID_MLSKTCha] = moi.[iID_MLSKTCha]
from ns_skt_mucluc ml 
join ns_skt_mucluc_hd4554 moi on
ml.sKyHieu = moi.sKyHieu 
where ml.sKyHieu in (select sKyHieu from ns_skt_mucluc_hd4554) and ml.iNamLamViec = 2025;
go