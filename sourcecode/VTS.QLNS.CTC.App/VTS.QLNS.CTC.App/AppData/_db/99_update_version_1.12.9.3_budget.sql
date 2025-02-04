IF NOT EXISTS (SELECT * FROM DANHMUC WHERE sTen = N'2 - Ngân sách nhà nước' AND sType = 'NS_NguonNganSach')
INSERT INTO [dbo].[DanhMuc]
           ([iID_DanhMuc]
           ,[iID_MaDanhMuc]
           ,[iTrangThai]
           ,[sTen]
           ,[sType]
           )
     VALUES
          (newid(), 2, 1, N'2 - Ngân sách nhà nước', 'NS_NguonNganSach')