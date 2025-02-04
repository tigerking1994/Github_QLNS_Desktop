/****** Object:  StoredProcedure [dbo].[sp_bh_update_total_tham_dinh_quyet_toan]    Script Date: 16/01/2024 8:23:36 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_update_total_tham_dinh_quyet_toan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_update_total_tham_dinh_quyet_toan]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_create_summary_tham_dinh_quyet_toan]    Script Date: 16/01/2024 8:23:36 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_create_summary_tham_dinh_quyet_toan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_create_summary_tham_dinh_quyet_toan]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_create_summary_tham_dinh_quyet_toan]    Script Date: 16/01/2024 8:23:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_create_summary_tham_dinh_quyet_toan]
@IdChungTu nvarchar(MAX),
@NguoiTao nvarchar(500),
@YearOfWork int,
@IdChungTuSummary nvarchar(MAX)
AS
BEGIN
SET NOCOUNT ON;

INSERT INTO BH_ThamDinhQuyetToan_ChungTuChiTiet(
	   [iID_BH_TDQT_ChungTuChiTiet]
      ,[fCNVLDHD]
      ,[fQuanNhan]
      ,[fSoBaoCao]
      ,[fSoThamDinh]
      ,[iID_BH_TDQT_ChungTu]
      ,[iID_MaDonVi]
      ,[iMa]
      ,[iNamLamViec]
      ,[sGhiChu]
	)
SELECT 
	   NEWID(),
	   SUM(ISNULL([fCNVLDHD], 0)),
	   SUM(ISNULL([fQuanNhan], 0)),
	   SUM(ISNULL([fSoBaoCao], 0)),
	   SUM(ISNULL([fSoThamDinh], 0)),
	   @IdChungTuSummary,
       '',
       [iMa],
       @YearOfWork,
       ''

FROM BH_ThamDinhQuyetToan_ChungTuChiTiet
WHERE  [iID_BH_TDQT_ChungTu] IN
    (SELECT *
     FROM f_split(@IdChungTu))
GROUP BY [iMa]

UPDATE BH_ThamDinhQuyetToan_ChungTu SET bDaTongHop=1  WHERE [iID_BH_TDQT_ChungTu] IN (SELECT * FROM f_split(@IdChungTu));
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_update_total_tham_dinh_quyet_toan]    Script Date: 16/01/2024 8:23:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_update_total_tham_dinh_quyet_toan] 
	@VoucherId nvarchar(255),
	@UserModify nvarchar(100)
AS
BEGIN
	DECLARE @FCNVLDHD FLOAT;
	DECLARE @FQuanNhan FLOAT;
	DECLARE @FSoBaoCao FLOAT;
	DECLARE @FSoThamDinh FLOAT;
	

	SELECT 
		@FCNVLDHD = SUM(fCNVLDHD),
		@FQuanNhan= SUM(fQuanNhan),
		@FSoBaoCao = SUM(fSoBaoCao),
		@FSoThamDinh = SUM(fSoThamDinh)
	FROM BH_ThamDinhQuyetToan_ChungTuChiTiet where iID_BH_TDQT_ChungTu = @VoucherId;

	UPDATE BH_ThamDinhQuyetToan_ChungTu 
	SET fCNVLDHD = ISNULL(@FCNVLDHD, 0), 
		fQuanNhan = ISNULL(@FQuanNhan, 0), 
		fSoBaoCao = ISNULL(@FSoBaoCao, 0),
		fSoThamDinh = ISNULL(@FSoThamDinh, 0),
		dNgaySua = GETDATE(), 
		sNguoiSua = @UserModify  
	WHERE iID_BH_TDQT_ChungTu = @VoucherId; 
END
;
;
;

GO
