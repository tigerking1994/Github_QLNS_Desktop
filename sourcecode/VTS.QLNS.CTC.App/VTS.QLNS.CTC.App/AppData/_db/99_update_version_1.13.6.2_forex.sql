-- DM_ChuDauTu
BEGIN
	DECLARE @ConstraintName nvarchar(200)
	SELECT @ConstraintName = Name FROM SYS.DEFAULT_CONSTRAINTS
	WHERE PARENT_OBJECT_ID = OBJECT_ID('[DM_ChuDauTu]')
	AND PARENT_COLUMN_ID = (SELECT column_id FROM sys.columns
	                        WHERE NAME = N'iID_DonVi'
	                        AND object_id = OBJECT_ID(N'[DM_ChuDauTu]'))
	IF @ConstraintName IS NOT NULL
	EXEC('ALTER TABLE [DM_ChuDauTu] DROP CONSTRAINT ' + @ConstraintName)
ALTER TABLE [dbo].[DM_ChuDauTu] ADD  DEFAULT (newid()) FOR [iID_DonVi]
END

-- [NH_DM_TiGia_ChiTiet]
BEGIN
	DECLARE @ConstraintNameTgct nvarchar(200)
	SELECT @ConstraintNameTgct = Name FROM SYS.DEFAULT_CONSTRAINTS
	WHERE PARENT_OBJECT_ID = OBJECT_ID('NH_DM_TiGia_ChiTiet')
	AND PARENT_COLUMN_ID = (SELECT column_id FROM sys.columns
	                        WHERE NAME = N'ID'
	                        AND object_id = OBJECT_ID(N'NH_DM_TiGia_ChiTiet'))
	IF @ConstraintNameTgct IS NOT NULL
	EXEC('ALTER TABLE NH_DM_TiGia_ChiTiet DROP CONSTRAINT ' + @ConstraintNameTgct)
	ALTER TABLE [dbo].[NH_DM_TiGia_ChiTiet] ADD  DEFAULT (newid()) FOR [ID]
END

-- [[NH_DM_TiGia]]
BEGIN
	DECLARE @ConstraintNameTg nvarchar(200)
	SELECT @ConstraintNameTg = Name FROM SYS.DEFAULT_CONSTRAINTS
	WHERE PARENT_OBJECT_ID = OBJECT_ID('NH_DM_TiGia')
	AND PARENT_COLUMN_ID = (SELECT column_id FROM sys.columns
	                        WHERE NAME = N'ID'
	                        AND object_id = OBJECT_ID(N'NH_DM_TiGia'))
	IF @ConstraintNameTg IS NOT NULL
	EXEC('ALTER TABLE NH_DM_TiGia DROP CONSTRAINT ' + @ConstraintNameTg)
	ALTER TABLE [dbo].[NH_DM_TiGia] ADD  DEFAULT (newid()) FOR [ID]
END
-- [[[NH_DM_PhuongThucChonNhaThau]]]
BEGIN
	DECLARE @ConstraintNamePt nvarchar(200)
	SELECT @ConstraintNamePt = Name FROM SYS.DEFAULT_CONSTRAINTS
	WHERE PARENT_OBJECT_ID = OBJECT_ID('NH_DM_PhuongThucChonNhaThau')
	AND PARENT_COLUMN_ID = (SELECT column_id FROM sys.columns
	                        WHERE NAME = N'ID'
	                        AND object_id = OBJECT_ID(N'NH_DM_PhuongThucChonNhaThau'))
	IF @ConstraintNamePt IS NOT NULL
	EXEC('ALTER TABLE [dbo].[NH_DM_PhuongThucChonNhaThau] DROP CONSTRAINT ' + @ConstraintNamePt)
ALTER TABLE [dbo].[NH_DM_PhuongThucChonNhaThau] ADD  DEFAULT (newid()) FOR [ID]
END
-- [[[NH_DM_PhanCapPheDuyet]]]
BEGIN
	DECLARE @ConstraintNamepcpd nvarchar(200)
	SELECT @ConstraintNamepcpd = Name FROM SYS.DEFAULT_CONSTRAINTS
	WHERE PARENT_OBJECT_ID = OBJECT_ID('NH_DM_PhanCapPheDuyet')
	AND PARENT_COLUMN_ID = (SELECT column_id FROM sys.columns
	                        WHERE NAME = N'ID'
	                        AND object_id = OBJECT_ID(N'NH_DM_PhanCapPheDuyet'))
	IF @ConstraintNamepcpd IS NOT NULL
	EXEC('ALTER TABLE NH_DM_PhanCapPheDuyet DROP CONSTRAINT ' + @ConstraintNamepcpd)
	ALTER TABLE NH_DM_PhanCapPheDuyet ADD DEFAULT newid() FOR ID
END
-- [NH_DM_NhaThau]
BEGIN
	DECLARE @ConstraintNameNt nvarchar(200)
	SELECT @ConstraintNameNt = Name FROM SYS.DEFAULT_CONSTRAINTS
	WHERE PARENT_OBJECT_ID = OBJECT_ID('NH_DM_NhaThau')
	AND PARENT_COLUMN_ID = (SELECT column_id FROM sys.columns
	                        WHERE NAME = N'Id'
	                        AND object_id = OBJECT_ID(N'NH_DM_NhaThau'))
	IF @ConstraintNameNt IS NOT NULL
	EXEC('ALTER TABLE NH_DM_NhaThau DROP CONSTRAINT ' + @ConstraintNameNt)
	ALTER TABLE NH_DM_NhaThau ADD DEFAULT newid() FOR Id
END

-- [NH_DM_LoaiTienTe]
BEGIN
	DECLARE @ConstraintNameltt nvarchar(200)
	SELECT @ConstraintNameltt = Name FROM SYS.DEFAULT_CONSTRAINTS
	WHERE PARENT_OBJECT_ID = OBJECT_ID('NH_DM_LoaiTienTe')
	AND PARENT_COLUMN_ID = (SELECT column_id FROM sys.columns
	                        WHERE NAME = N'ID'
	                        AND object_id = OBJECT_ID(N'NH_DM_LoaiTienTe'))
	IF @ConstraintNameltt IS NOT NULL
	EXEC('ALTER TABLE NH_DM_LoaiTienTe DROP CONSTRAINT ' + @ConstraintNameltt)
	ALTER TABLE NH_DM_LoaiTienTe ADD DEFAULT newid() FOR ID
END

-- [NH_DM_LoaiTaiSan]
BEGIN
	DECLARE @ConstraintNamelts nvarchar(200)
	SELECT @ConstraintNamelts = Name FROM SYS.DEFAULT_CONSTRAINTS
	WHERE PARENT_OBJECT_ID = OBJECT_ID('NH_DM_LoaiTaiSan')
	AND PARENT_COLUMN_ID = (SELECT column_id FROM sys.columns
	                        WHERE NAME = N'ID'
	                        AND object_id = OBJECT_ID(N'NH_DM_LoaiTaiSan'))
	IF @ConstraintNamelts IS NOT NULL
	EXEC('ALTER TABLE NH_DM_LoaiTaiSan DROP CONSTRAINT ' + @ConstraintNamelts)
	ALTER TABLE NH_DM_LoaiTaiSan ADD DEFAULT newid() FOR ID
END
-- [NH_DM_LoaiHopDong]
BEGIN
	DECLARE @ConstraintNamelhd nvarchar(200)
	SELECT @ConstraintNamelhd = Name FROM SYS.DEFAULT_CONSTRAINTS
	WHERE PARENT_OBJECT_ID = OBJECT_ID('NH_DM_LoaiHopDong')
	AND PARENT_COLUMN_ID = (SELECT column_id FROM sys.columns
	                        WHERE NAME = N'iID_LoaiHopDongID'
	                        AND object_id = OBJECT_ID(N'NH_DM_LoaiHopDong'))
	IF @ConstraintNamelhd IS NOT NULL
	EXEC('ALTER TABLE NH_DM_LoaiHopDong DROP CONSTRAINT ' + @ConstraintNamelhd)
	ALTER TABLE NH_DM_LoaiHopDong ADD DEFAULT newid() FOR iID_LoaiHopDongID
END
-- [NH_DM_LoaiCongTrinh]
BEGIN
	DECLARE @ConstraintNamelct nvarchar(200)
	SELECT @ConstraintNamelct = Name FROM SYS.DEFAULT_CONSTRAINTS
	WHERE PARENT_OBJECT_ID = OBJECT_ID('NH_DM_LoaiCongTrinh')
	AND PARENT_COLUMN_ID = (SELECT column_id FROM sys.columns
	                        WHERE NAME = N'ID'
	                        AND object_id = OBJECT_ID(N'NH_DM_LoaiCongTrinh'))
	IF @ConstraintNamelct IS NOT NULL
	EXEC('ALTER TABLE NH_DM_LoaiCongTrinh DROP CONSTRAINT ' + @ConstraintNamelct)
	ALTER TABLE NH_DM_LoaiCongTrinh ADD DEFAULT newid() FOR ID
END
-- [NH_DM_HinhThucChonNhaThau]
BEGIN
	DECLARE @ConstraintNamehtcnt nvarchar(200)
	SELECT @ConstraintNamehtcnt = Name FROM SYS.DEFAULT_CONSTRAINTS
	WHERE PARENT_OBJECT_ID = OBJECT_ID('NH_DM_HinhThucChonNhaThau')
	AND PARENT_COLUMN_ID = (SELECT column_id FROM sys.columns
	                        WHERE NAME = N'ID'
	                        AND object_id = OBJECT_ID(N'NH_DM_HinhThucChonNhaThau'))
	IF @ConstraintNamehtcnt IS NOT NULL
	EXEC('ALTER TABLE NH_DM_HinhThucChonNhaThau DROP CONSTRAINT ' + @ConstraintNamehtcnt)
	ALTER TABLE NH_DM_HinhThucChonNhaThau ADD DEFAULT newid() FOR ID
END


