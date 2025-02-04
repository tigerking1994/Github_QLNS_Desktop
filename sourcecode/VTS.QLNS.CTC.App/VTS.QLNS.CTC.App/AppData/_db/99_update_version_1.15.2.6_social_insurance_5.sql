/****** Object:  StoredProcedure [dbo].[sp_bh_create_summary_tham_dinh_quyet_toan]    Script Date: 12/24/2024 11:52:23 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_create_summary_tham_dinh_quyet_toan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_create_summary_tham_dinh_quyet_toan]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_create_summary_tham_dinh_quyet_toan]    Script Date: 12/24/2024 11:52:23 AM ******/
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

DECLARE @MaDonVi nvarchar(100);
select @MaDonVi= iID_MaDonVi from BH_ThamDinhQuyetToan_ChungTu where iID_BH_TDQT_ChungTu = @IdChungTuSummary;

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
       @MaDonVi,
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
;
;
GO
