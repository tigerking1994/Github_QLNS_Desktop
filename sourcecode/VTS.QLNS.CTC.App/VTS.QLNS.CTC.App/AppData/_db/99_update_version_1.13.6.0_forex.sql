/****** Object:  Table [dbo].[[DM_ChuDauTu]]    Script Date: 11/27/2023 18:14:17 PM ******/
IF EXISTS (SELECT * FROM [dbo].[DM_ChuDauTu] where iID_DonVi = 'c382d1a6-8bdb-487a-89b9-0ab7b060d94a')
BEGIN 
	DELETE [dbo].[DM_ChuDauTu] where iID_DonVi = 'c382d1a6-8bdb-487a-89b9-0ab7b060d94a';
END
GO
IF EXISTS (SELECT * FROM [dbo].[DM_ChuDauTu] where iID_DonVi = 'a35d8af7-a12b-4961-b99d-391ff7253a58')
BEGIN 
	DELETE [dbo].[DM_ChuDauTu] where iID_DonVi = 'a35d8af7-a12b-4961-b99d-391ff7253a58';
END
GO
IF EXISTS (SELECT * FROM [dbo].[DM_ChuDauTu] where iID_DonVi = 'ee7d016a-3856-472d-97d4-52d58785574d')
BEGIN 
	DELETE [dbo].[DM_ChuDauTu] where iID_DonVi = 'ee7d016a-3856-472d-97d4-52d58785574d';
END
GO
IF EXISTS (SELECT * FROM [dbo].[DM_ChuDauTu] where iID_DonVi = '9404412b-05c0-4ad9-b732-cc60fb67a238')
BEGIN 
	DELETE [dbo].[DM_ChuDauTu] where iID_DonVi = '9404412b-05c0-4ad9-b732-cc60fb67a238';
END
GO
BEGIN
	DECLARE @ConstraintName nvarchar(200)
	SELECT @ConstraintName = Name FROM SYS.DEFAULT_CONSTRAINTS
	WHERE PARENT_OBJECT_ID = OBJECT_ID('DM_ChuDauTu')
	AND PARENT_COLUMN_ID = (SELECT column_id FROM sys.columns
	                        WHERE NAME = N'iID_DonVi'
	                        AND object_id = OBJECT_ID(N'DM_ChuDauTu'))
	IF @ConstraintName IS NOT NULL
	EXEC('ALTER TABLE DM_ChuDauTu DROP CONSTRAINT ' + @ConstraintName)
	ALTER TABLE DM_ChuDauTu ADD CONSTRAINT  DF__DM_ChuDau__iID_D__28112023 DEFAULT 'NewID()' FOR iID_DonVi;
END