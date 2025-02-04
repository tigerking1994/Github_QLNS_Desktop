IF EXISTS (SELECT 1
               FROM   INFORMATION_SCHEMA.COLUMNS
               WHERE  TABLE_NAME = 'BH_CP_CapTamUng_KCB_BHYT_ChiTiet'
                      AND COLUMN_NAME = 'sXauNoiMa'
                      AND TABLE_SCHEMA='DBO')
  BEGIN
      ALTER TABLE BH_CP_CapTamUng_KCB_BHYT_ChiTiet
        DROP COLUMN sXauNoiMa
  END
GO
IF EXISTS (SELECT 1
               FROM   INFORMATION_SCHEMA.COLUMNS
               WHERE  TABLE_NAME = 'BH_CP_CapTamUng_KCB_BHYT_ChiTiet'
                      AND COLUMN_NAME = 'sL'
                      AND TABLE_SCHEMA='DBO')
  BEGIN
      ALTER TABLE BH_CP_CapTamUng_KCB_BHYT_ChiTiet
        DROP COLUMN sL
  END
GO
IF EXISTS (SELECT 1
               FROM   INFORMATION_SCHEMA.COLUMNS
               WHERE  TABLE_NAME = 'BH_CP_CapTamUng_KCB_BHYT_ChiTiet'
                      AND COLUMN_NAME = 'sK'
                      AND TABLE_SCHEMA='DBO')
  BEGIN
      ALTER TABLE BH_CP_CapTamUng_KCB_BHYT_ChiTiet
        DROP COLUMN sK
  END
GO
IF EXISTS (SELECT 1
               FROM   INFORMATION_SCHEMA.COLUMNS
               WHERE  TABLE_NAME = 'BH_CP_CapTamUng_KCB_BHYT_ChiTiet'
                      AND COLUMN_NAME = 'sM'
                      AND TABLE_SCHEMA='DBO')
  BEGIN
      ALTER TABLE BH_CP_CapTamUng_KCB_BHYT_ChiTiet
        DROP COLUMN sM
  END
GO
IF EXISTS (SELECT 1
               FROM   INFORMATION_SCHEMA.COLUMNS
               WHERE  TABLE_NAME = 'BH_CP_CapTamUng_KCB_BHYT_ChiTiet'
                      AND COLUMN_NAME = 'sTM'
                      AND TABLE_SCHEMA='DBO')
  BEGIN
      ALTER TABLE BH_CP_CapTamUng_KCB_BHYT_ChiTiet
        DROP COLUMN sTM
  END
GO
IF EXISTS (SELECT 1
               FROM   INFORMATION_SCHEMA.COLUMNS
               WHERE  TABLE_NAME = 'BH_CP_CapTamUng_KCB_BHYT_ChiTiet'
                      AND COLUMN_NAME = 'sTTM'
                      AND TABLE_SCHEMA='DBO')
  BEGIN
      ALTER TABLE BH_CP_CapTamUng_KCB_BHYT_ChiTiet
        DROP COLUMN sTTM
  END
GO
IF EXISTS (SELECT 1
               FROM   INFORMATION_SCHEMA.COLUMNS
               WHERE  TABLE_NAME = 'BH_CP_CapTamUng_KCB_BHYT_ChiTiet'
                      AND COLUMN_NAME = 'sNG'
                      AND TABLE_SCHEMA='DBO')
  BEGIN
      ALTER TABLE BH_CP_CapTamUng_KCB_BHYT_ChiTiet
        DROP COLUMN sNG
  END
GO
IF EXISTS (SELECT 1
               FROM   INFORMATION_SCHEMA.COLUMNS
               WHERE  TABLE_NAME = 'BH_CP_CapTamUng_KCB_BHYT_ChiTiet'
                      AND COLUMN_NAME = 'sTNG'
                      AND TABLE_SCHEMA='DBO')
  BEGIN
      ALTER TABLE BH_CP_CapTamUng_KCB_BHYT_ChiTiet
        DROP COLUMN sTNG
  END
GO
IF EXISTS (SELECT 1
               FROM   INFORMATION_SCHEMA.COLUMNS
               WHERE  TABLE_NAME = 'BH_CP_CapTamUng_KCB_BHYT_ChiTiet'
                      AND COLUMN_NAME = 'dNgayTao'
                      AND TABLE_SCHEMA='DBO')
  BEGIN
      ALTER TABLE BH_CP_CapTamUng_KCB_BHYT_ChiTiet
        DROP COLUMN dNgayTao
  END
GO
IF EXISTS (SELECT 1
               FROM   INFORMATION_SCHEMA.COLUMNS
               WHERE  TABLE_NAME = 'BH_CP_CapTamUng_KCB_BHYT_ChiTiet'
                      AND COLUMN_NAME = 'dNgaySua'
                      AND TABLE_SCHEMA='DBO')
  BEGIN
      ALTER TABLE BH_CP_CapTamUng_KCB_BHYT_ChiTiet
        DROP COLUMN dNgaySua
  END
GO
IF EXISTS (SELECT 1
               FROM   INFORMATION_SCHEMA.COLUMNS
               WHERE  TABLE_NAME = 'BH_CP_CapTamUng_KCB_BHYT_ChiTiet'
                      AND COLUMN_NAME = 'sNguoiTao'
                      AND TABLE_SCHEMA='DBO')
  BEGIN
      ALTER TABLE BH_CP_CapTamUng_KCB_BHYT_ChiTiet
        DROP COLUMN sNguoiTao
  END
GO
IF EXISTS (SELECT 1
               FROM   INFORMATION_SCHEMA.COLUMNS
               WHERE  TABLE_NAME = 'BH_CP_CapTamUng_KCB_BHYT_ChiTiet'
                      AND COLUMN_NAME = 'sNguoiSua'
                      AND TABLE_SCHEMA='DBO')
  BEGIN
      ALTER TABLE BH_CP_CapTamUng_KCB_BHYT_ChiTiet
        DROP COLUMN sNguoiSua
  END
GO