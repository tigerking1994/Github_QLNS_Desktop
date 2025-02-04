IF EXISTS (SELECT 1
               FROM   INFORMATION_SCHEMA.COLUMNS
               WHERE  TABLE_NAME = 'BH_QTC_Nam_KinhPhiQuanLy'
                      AND COLUMN_NAME = 'iNamChungTu'
                      AND TABLE_SCHEMA='DBO')
  BEGIN
    ALTER TABLE BH_QTC_Nam_KinhPhiQuanLy
    DROP COLUMN iNamChungTu
	ALTER TABLE BH_QTC_Nam_KinhPhiQuanLy
	ADD iNamLamViec int
    ALTER TABLE BH_QTC_Nam_KinhPhiQuanLy
	ADD bDaTongHop bit not null default 0 
  END
  GO

IF EXISTS (SELECT 1
               FROM   INFORMATION_SCHEMA.COLUMNS
               WHERE  TABLE_NAME = 'BH_QTC_Nam_KPK'
                      AND COLUMN_NAME = 'iNamChungTu'
                      AND TABLE_SCHEMA='DBO')
  BEGIN
    ALTER TABLE BH_QTC_Nam_KPK
    DROP COLUMN iNamChungTu
	ALTER TABLE BH_QTC_Nam_KPK
	ADD iNamLamViec int
    ALTER TABLE BH_QTC_Nam_KPK
	ADD bDaTongHop bit not null default 0 
  END
  GO

IF EXISTS (SELECT 1
               FROM   INFORMATION_SCHEMA.COLUMNS
               WHERE  TABLE_NAME = 'BH_QTC_Nam_CheDoBHXH'
                      AND COLUMN_NAME = 'iNamChungTu'
                      AND TABLE_SCHEMA='DBO')
  BEGIN
    ALTER TABLE BH_QTC_Nam_CheDoBHXH
    DROP COLUMN iNamChungTu
	ALTER TABLE BH_QTC_Nam_CheDoBHXH
	ADD iNamLamViec int
  END
  GO

IF EXISTS (SELECT 1
               FROM   INFORMATION_SCHEMA.COLUMNS
               WHERE  TABLE_NAME = 'BH_QTC_Nam_KCB_QuanYDonVi'
                      AND COLUMN_NAME = 'iNamChungTu'
                      AND TABLE_SCHEMA='DBO')
  BEGIN
    ALTER TABLE BH_QTC_Nam_KCB_QuanYDonVi
    DROP COLUMN iNamChungTu
	ALTER TABLE BH_QTC_Nam_KCB_QuanYDonVi
	ADD iNamLamViec int
  END
  GO
  