IF NOT EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[NH_TH_TongHop]') 
         AND name = 'iCoQuanThanhToan'
)
ALTER TABLE [dbo].[NH_TH_TongHop]
ADD [iCoQuanThanhToan] [int] NULL;

IF NOT EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[NH_NhuCauChiQuy]') 
         AND name = 'iID_KHTongTheID'
)
ALTER TABLE [dbo].[NH_NhuCauChiQuy]
ADD [iID_KHTongTheID] [uniqueidentifier] NULL;