/****** Object:  StoredProcedure [dbo].[sp_vdt_hopdong_find_goithau_by_du_an]    Script Date: 20/12/2023 3:05:53 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_hopdong_find_goithau_by_du_an]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_hopdong_find_goithau_by_du_an]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_hopdong_dieuchinh_find_hangmuc_by_chiphi_hopdonggoc]    Script Date: 20/12/2023 3:05:53 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_hopdong_dieuchinh_find_hangmuc_by_chiphi_hopdonggoc]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_hopdong_dieuchinh_find_hangmuc_by_chiphi_hopdonggoc]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_hopdong_dieuchinh_find_hangmuc_by_chiphi_hopdongdieuchinh]    Script Date: 20/12/2023 3:05:53 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_hopdong_dieuchinh_find_hangmuc_by_chiphi_hopdongdieuchinh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_hopdong_dieuchinh_find_hangmuc_by_chiphi_hopdongdieuchinh]
GO

/****** Object:  StoredProcedure [dbo].[sp_vdt_hopdong_dieuchinh_find_hangmuc_by_chiphi_hopdonggoc]    Script Date: 20/12/2023 3:05:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_vdt_hopdong_dieuchinh_find_hangmuc_by_chiphi_hopdonggoc] 
	-- Add the parameters for the stored procedure here
	@chiphiID uniqueidentifier,
	@idHopDongGoiThauNhaThau uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select t1.*, ISNULL(t3.sTenHangMuc,t5.sTenHangMuc) as sTenHangMuc, ISNULL(t3.maOrder, t5.maOrder) as maOrder, ISNULL(t3.iID_ParentID, t5.iID_ParentID) as iID_ParentID, t4.fTienGoiThau, t1.iID_HopDonggoiThauNhaThauID as IdHopDongGoiThauNhaThau,
        t1.fGiatri as fGiatriSuDung, ISNULL(t1.fGiaTriTruocDC, t1.fGiatri) as giatritruocdc
        from VDT_DA_HopDong_goiThau_HangMuc t1
        left join VDT_DA_HopDong_goiThau_NhaThau t2 on t1.iID_HopDongGoiThauNhaThauID = t2.id
        left join VDT_DA_DuToan_DM_HangMuc t3 on t3.id = t1.iID_HangMucID
		left join VDT_DA_QDDauTu_DM_HangMuc t5 on t5.Id = t1.iID_HangMucID
        left join VDT_DA_goiThau_HangMuc t4 on t4.iID_goiThauID = t2.iID_GoiThauID and t4.iID_ChiPhiID = t1.iID_ChiPhiID and t4.iID_HangMucID = t1.iID_HangMucID
        where t1.iID_ChiPhiID = @chiphiID and iID_HopDonggoiThauNhaThauID = @idHopDongGoiThauNhaThau
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_hopdong_find_goithau_by_du_an]    Script Date: 20/12/2023 3:05:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_vdt_hopdong_find_goithau_by_du_an] 
	-- Add the parameters for the stored procedure here
	@DuAnId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT t1.*, ISNULL(t2.fgiatri, 0) as FGiaTriDaSD, (t1.fTienTrungThau - ISNULL(t2.fgiatri, 0)) as fGiaTriConLai, t1.iID_NhaThauID as NhaThauId FROM VDT_DA_GOITHAU t1
    left join
    (select s1.iid_goithauid, SUM(s1.fgiatri) as fgiatri from VDT_DA_HopDong_GoiThau_NhaThau s1
	JOIN vdt_da_tt_hopdong s2 on s1.iid_hopdongid = s2.id
	where s2.bactive = 1
	group by s1.iid_goithauid) t2
    on t1.iID_GoiThauID = t2.iID_GoiThauID
    where t1.fTienTrungThau > 0 and(t2.fgiatri is null or t1.fTienTrungThau > t2.fgiatri) and t1.iID_DuAnID = @DuAnId and t1.bactive = 1
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_hopdong_dieuchinh_find_hangmuc_by_chiphi_hopdongdieuchinh]    Script Date: 20/12/2023 5:26:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_vdt_hopdong_dieuchinh_find_hangmuc_by_chiphi_hopdongdieuchinh] 
	-- Add the parameters for the stored procedure here
	@chiphiID uniqueidentifier,
	@idHopDongGoiThauNhaThau uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select t1.*, 
	CASE
		WHEN (hd.bActive = 1 and hd.bIsGoc = 0) THEN t6.sTenHangMuc
		ELSE ISNULL(t3.sTenHangMuc,t5.sTenHangMuc)		
	END as sTenHangMuc,
	CASE
		WHEN (hd.bActive = 1 and hd.bIsGoc = 0) THEN t6.maOrder
		ELSE ISNULL(t3.maOrder, t5.maOrder)		
	END as maOrder,
	CASE
		WHEN (hd.bActive = 1 and hd.bIsGoc = 0) THEN t6.iID_ParentID
		ELSE ISNULL(t3.iID_ParentID, t5.iID_ParentID)		
	END as iID_ParentID,
	ISNULL(t1.fGiaTriDuocDuyet, t4.fTienGoiThau) as fTienGoiThau, t1.iID_HopDonggoiThauNhaThauID as IdHopDongGoiThauNhaThau,
        t1.fGiatri as fGiatriSuDung, ISNULL(t1.fGiaTriTruocDC, t1.fGiatri) as giatritruocdc
        from VDT_DA_HopDong_goiThau_HangMuc t1
        left join VDT_DA_HopDong_goiThau_NhaThau t2 on t1.iID_HopDongGoiThauNhaThauID = t2.id
		left join VDT_DA_TT_HopDong hd on hd.Id = t2.iID_HopDongID
        left join VDT_DA_DuToan_DM_HangMuc t3 on t3.id = t1.iID_HangMucID
		left join VDT_DA_QDDauTu_DM_HangMuc t5 on t5.Id = t1.iID_HangMucID		
		left join VDT_DA_HopDong_DM_HangMuc t6 on t6.Id = t1.iID_HangMucID
        left join VDT_DA_goiThau_HangMuc t4 on t4.iID_goiThauID = t2.iID_GoiThauID and t4.iID_ChiPhiID = t1.iID_ChiPhiID and t4.iID_HangMucID = t1.iID_HangMucID
        where t1.iID_ChiPhiID = @chiphiID and t1.iID_HopDonggoiThauNhaThauID = @idHopDongGoiThauNhaThau
END
;
;
;
GO
