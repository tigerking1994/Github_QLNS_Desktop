/****** Object:  StoredProcedure [dbo].[sp_nh_hangmuc_bygoithauid]    Script Date: 11/17/2023 2:36:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_hangmuc_bygoithauid]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_hangmuc_bygoithauid]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_hangmuc_bygoithauid]    Script Date: 11/17/2023 2:36:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROC [dbo].[sp_nh_hangmuc_bygoithauid]
	@idGoiThau uniqueidentifier
	
AS BEGIN
	SELECT
		HangMuc.iID_GoiThau_HangMucID as Id,
		HangMuc.isCheck as IsCheck,
		HangMuc.iID_GoiThau_ChiPhiID as IIdGoiThauChiPhiId,
		HangMuc.iIDGoiThauCheck as IIDGoiThauCheck,
		HangMuc.iID_QDDauTu_HangMucID as IIdQDDauTuHangMucId,
		HangMuc.iID_DuToan_HangMucID as IIdDuToanChiPhiId,
		HangMuc.fTienGoiThau_USD as FTienGoiThauUsd,
		HangMuc.fTienGoiThau_VND as FTienGoiThauVnd,
		HangMuc.fTienGoiThau_EUR as FTienGoiThauEur,
		HangMuc.fTienGoiThau_NgoaiTeKhac as FTienGoiThauNgoaiTeKhac,
		QDDT.sTenHangMuc as STenHangMucQDDT,
		DuToan.sTenHangMuc as STenHangMucDT,
		HangMuc.iID_ParentID as IIdParentId,
		DuToanChiPhi.sTenChiPhi as STenChiPhiDT,
		HangMuc.sMaHangMuc as SMaHangMuc,
		HangMuc.sMaOrder as SMaOrder
	FROM NH_DA_GoiThau_HangMuc HangMuc
	LEFT JOIN NH_DA_QDDauTu_HangMuc QDDT
		ON HangMuc.iID_QDDauTu_HangMucID = QDDT.ID
	LEFT JOIN NH_DA_DuToan_HangMuc DuToan
		ON HangMuc.iID_DuToan_HangMucID = DuToan.ID
	LEFT JOIN NH_DA_GoiThau_ChiPhi ChiPhi
	    ON HangMuc.iID_GoiThau_ChiPhiID = ChiPhi.ID
	LEFT JOIN NH_DA_QDDauTu_ChiPhi QDDTChiPhi
		ON ChiPhi.iID_QDDauTu_ChiPhiID = QDDTChiPhi.ID
	LEFT JOIN NH_DA_DuToan_ChiPhi DuToanChiPhi
		ON ChiPhi.iID_DuToan_ChiPhiID = DuToanChiPhi.ID
	WHERE 
		1=1
		AND ChiPhi.iID_GoiThauID = @idGoiThau
	ORDER BY DuToanChiPhi.sTenChiPhi,
	         case 
				when DuToan.iID_ParentID is null
				then DuToan.ID 
				else    (
						select  ID 
						from    NH_DA_DuToan_HangMuc parent 
						where   parent.ID = DuToan.iID_ParentID
						) 
				end
			,case when DuToan.iID_ParentID is null then 1 end desc
			,DuToan.ID

END
;
;
;
GO
