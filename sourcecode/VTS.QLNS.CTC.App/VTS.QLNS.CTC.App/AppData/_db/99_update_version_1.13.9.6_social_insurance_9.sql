/****** Object:  StoredProcedure [dbo].[sp_bh_getDataQuyetToanGiaiThich]    Script Date: 2/1/2024 10:23:05 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_getDataQuyetToanGiaiThich]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_getDataQuyetToanGiaiThich]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_getDataQuyetToanGiaiThich]    Script Date: 2/1/2024 10:23:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<DungNV>
-- Create date: <01/02/2024>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_bh_getDataQuyetToanGiaiThich] 
	-- Add the parameters for the stored procedure here
	@IdChungTu uniqueidentifier, 
	@XauNoiMa nvarchar(100),
	@NamLamViec int
AS
BEGIN
	declare @IType int = (SELECT TOP(1) iDonViTinh FROM BH_DM_MucLucNganSach WHERE iNamLamViec = @NamLamViec AND sXauNoiMa = @XauNoiMa);--1:Ngay, 2:Thang, 3: nguoi
	DECLARE @DonViTinh int ;
	IF(@IType = 3) -- tinh so nguoi
		BEGIN
			select 
		               COUNT(CASE WHEN sMaCapBac LIKE '1%' then 1 ELSE NULL END) as ISoSQDeNghi,
		               SUM(CASE WHEN sMaCapBac LIKE '1%' then fSoTien ELSE 0 END) as FTienSQDeNghi,
		               COUNT(CASE WHEN sMaCapBac LIKE '2%' then 1 ELSE NULL END) as ISoQNCNDeNghi,
		               SUM(CASE WHEN sMaCapBac LIKE '2%' then fSoTien ELSE 0 END) as FTienQNCNDeNghi,
		               COUNT(CASE WHEN sMaCapBac LIKE '3.1%' OR sMaCapBac LIKE '3.2%' OR sMaCapBac LIKE '3.3%'then 1 ELSE    NULL     END)    as       ISoCNVCQPDeNghi,
		               SUM(CASE WHEN sMaCapBac LIKE '3.1%' OR sMaCapBac LIKE '3.2%' OR sMaCapBac LIKE '3.3%' then fSoTien    ELSE 0   END)   as     FTienCNVCQPDeNghi,		
		               COUNT(CASE WHEN sMaCapBac LIKE '43%' then 1 ELSE NULL END) as ISoHSQBSDeNghi,
		               SUM(CASE WHEN sMaCapBac LIKE '43%' then fSoTien ELSE 0 END) as FTienHSQBSDeNghi,		
		               COUNT(CASE WHEN sMaCapBac LIKE '0%' OR sMaCapBac = '4' then 1 ELSE NULL END) as ISoLDHDDeNghi,
		               SUM(CASE WHEN sMaCapBac LIKE '0%' OR sMaCapBac = '4' then fSoTien ELSE 0 END) as FTienLDHDDeNghi,
		               gttc.sXauNoiMa as SXauNoiMa
	               FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gttc
				   LEFT JOIN BH_DM_MucLucNganSach mucluc ON gttc.sXauNoiMa = mucluc.sXauNoiMa and  mucluc.iNamLamViec= @NamLamViec
	               WHERE 
	               		 gttc.iID_QTC_Quy_ChungTu=@IdChungTu
	               		 and gttc.sXauNoiMa = @XauNoiMa
	               GROUP BY gttc.sXauNoiMa;
		END
	ELSE
		BEGIN
		
			select 
		               CASE
							WHEN @IType = 2 THEN SUM(CASE WHEN sMaCapBac LIKE '1%' then iSoNgayHuong ELSE 0 END)/30
							ELSE SUM(CASE WHEN sMaCapBac LIKE '1%' then iSoNgayHuong ELSE 0 END)
					   END
					   AS ISoSQDeNghi,
		               SUM(CASE WHEN sMaCapBac LIKE '1%' then fSoTien ELSE 0 END) as FTienSQDeNghi,
		               CASE
							WHEN @IType = 2 THEN SUM(CASE WHEN sMaCapBac LIKE '2%' then iSoNgayHuong ELSE 0 END)/30
							ELSE SUM(CASE WHEN sMaCapBac LIKE '2%' then iSoNgayHuong ELSE 0 END)
					   END
					   AS ISoQNCNDeNghi,
		               SUM(CASE WHEN sMaCapBac LIKE '2%' then fSoTien ELSE 0 END) as FTienQNCNDeNghi,
		               CASE
							WHEN @IType = 2 THEN SUM(CASE WHEN sMaCapBac LIKE '3.1%' OR sMaCapBac LIKE '3.2%' OR sMaCapBac LIKE '3.3%'then iSoNgayHuong ELSE 0 END)/30
							ELSE SUM(CASE WHEN sMaCapBac LIKE '3.1%' OR sMaCapBac LIKE '3.2%' OR sMaCapBac LIKE '3.3%'then iSoNgayHuong ELSE 0 END)
					   END
					   AS ISoCNVCQPDeNghi,
		               SUM(CASE WHEN sMaCapBac LIKE '3.1%' OR sMaCapBac LIKE '3.2%' OR sMaCapBac LIKE '3.3%' then fSoTien    ELSE 0   END)   as     FTienCNVCQPDeNghi,	
		               CASE
							WHEN @IType = 2 THEN SUM(CASE WHEN sMaCapBac LIKE '43%' then iSoNgayHuong ELSE 0 END)/30
							ELSE SUM(CASE WHEN sMaCapBac LIKE '43%' then iSoNgayHuong ELSE 0 END)
					   END
					   AS ISoHSQBSDeNghi,					   
		               SUM(CASE WHEN sMaCapBac LIKE '43%' then fSoTien ELSE 0 END) as FTienHSQBSDeNghi,		
		               CASE
							WHEN @IType = 2 THEN SUM(CASE WHEN sMaCapBac LIKE '0%' OR sMaCapBac = '4' then iSoNgayHuong ELSE 0 END)/30
							ELSE SUM(CASE WHEN sMaCapBac LIKE '0%' OR sMaCapBac = '4' then iSoNgayHuong ELSE 0 END)
					   END
					   AS ISoLDHDDeNghi,
		               SUM(CASE WHEN sMaCapBac LIKE '0%' OR sMaCapBac = '4' then fSoTien ELSE 0 END) as FTienLDHDDeNghi,
		               gttc.sXauNoiMa as SXauNoiMa
	               FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gttc
					LEFT JOIN BH_DM_MucLucNganSach mucluc ON gttc.sXauNoiMa = mucluc.sXauNoiMa and  mucluc.iNamLamViec = @NamLamViec
	               WHERE 
	               		 gttc.iID_QTC_Quy_ChungTu=@IdChungTu
	               		 and gttc.sXauNoiMa = @XauNoiMa
	               GROUP BY gttc.sXauNoiMa;		
				   END

END
GO
