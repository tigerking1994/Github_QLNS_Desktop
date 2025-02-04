/****** Object:  StoredProcedure [dbo].[sp_bh_export_tonghopcaptamung_kcbbhyt]    Script Date: 8/24/2023 8:27:52 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_export_tonghopcaptamung_kcbbhyt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_export_tonghopcaptamung_kcbbhyt]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_export_kehoachcaptamung_kcbbhyt]    Script Date: 8/24/2023 8:27:52 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_export_kehoachcaptamung_kcbbhyt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_export_kehoachcaptamung_kcbbhyt]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dt_danhsach_cptubhyt_chitiet]    Script Date: 8/24/2023 8:27:52 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_dt_danhsach_cptubhyt_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_dt_danhsach_cptubhyt_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_cptubhyt_update_total]    Script Date: 8/24/2023 8:27:52 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_cptubhyt_update_total]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_cptubhyt_update_total]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_cptubhyt_create_data_summary]    Script Date: 8/24/2023 8:27:52 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_cptubhyt_create_data_summary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_cptubhyt_create_data_summary]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_cptubhyt_create_data_summary]    Script Date: 8/24/2023 8:27:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_cptubhyt_create_data_summary]
@IdChungTu nvarchar(MAX),
@NguoiTao nvarchar(500),
@YearOfWork int,
@IdChungTuSummary nvarchar(MAX)
AS
BEGIN
SET NOCOUNT ON;

INSERT INTO BH_CP_CapTamUng_KCB_BHYT_ChiTiet(iID_BH_CP_CapTamUng_KCB_BHYT_ChiTiet,iID_BH_CP_CapTamUng_KCB_BHYT, iID_MLNS, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sMoTa,
fQTQuyTruoc, fTamUngQuyNay, fLuyKeCapDenCuoiQuy, dNgayTao, dNgaySua, sNguoiTao, sNguoiSua ,iID_CoSoYTe, iID_MaCoSoYTe)
SELECT 
	   NEWID(),
	   @IdChungTuSummary,
       iID_MLNS,
       sXauNoiMa,
       sLNS,
       sL,
       sK,
       sM,
       sTM,
       sTTM,
       sNG,
       sTNG,
       sMoTa,
	   SUM(fQTQuyTruoc),
	   SUM(fTamUngQuyNay),
	   SUM(fLuyKeCapDenCuoiQuy),
	   GETDATE(),
	   GETDATE(),
	   @NguoiTao,
	   '',
	   iID_CoSoYTe,
	   iID_MaCoSoYTe
FROM BH_CP_CapTamUng_KCB_BHYT_ChiTiet
WHERE  iID_BH_CP_CapTamUng_KCB_BHYT IN
    (SELECT *
     FROM f_split(@IdChungTu))
group by iID_MLNS, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM,sNG, sTNG, sMoTa,iID_CoSoYTe, iID_MaCoSoYTe

UPDATE BH_CP_CapTamUng_KCB_BHYT SET bIsTongHop = 1 WHERE iID_BH_CP_CapTamUng_KCB_BHYT IN (SELECT * FROM f_split(@IdChungTu));
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_cptubhyt_update_total]    Script Date: 8/24/2023 8:27:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_bh_cptubhyt_update_total] 
	@VoucherId nvarchar(255),
	@UserModify nvarchar(100)
AS
BEGIN
	declare @TongfQTQuyTruoc float;
	declare @TongfTamUngQuyNay float;
	select @TongfQTQuyTruoc = SUM(fQTQuyTruoc) ,@TongfTamUngQuyNay= SUM(fTamUngQuyNay) FROM BH_CP_CapTamUng_KCB_BHYT_ChiTiet where iID_BH_CP_CapTamUng_KCB_BHYT = @VoucherId;
	update BH_CP_CapTamUng_KCB_BHYT set fQTQuyTruoc = @TongfQTQuyTruoc, fTamUngQuyNay = @TongfTamUngQuyNay, dNgaySua = GETDATE(), sNguoiSua = @UserModify  where iID_BH_CP_CapTamUng_KCB_BHYT = @VoucherId; 
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dt_danhsach_cptubhyt_chitiet]    Script Date: 8/24/2023 8:27:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_dt_danhsach_cptubhyt_chitiet]
	@ChungTuId NVARCHAR(255),
	@LNS NVARCHAR(MAX),
	@IdCsYTe NVARCHAR(MAX),
	@NamLamViec int,
	@UserName NVARCHAR(100)
As
begin
	--- Lấy danh sách MLNS
	select 
		CAST(NULL AS VARCHAR(50)) as iID_BH_CP_CapTamUng_KCB_BHYT_ChiTiet,
		BH_DM_MucLucNganSach.iID_MLNS as iID_MLNS,
		BH_DM_MucLucNganSach.iID_MLNS_Cha,
		BH_DM_MucLucNganSach.sLNS,
		BH_DM_MucLucNganSach.sL,
		BH_DM_MucLucNganSach.sK,
		BH_DM_MucLucNganSach.sM,
		BH_DM_MucLucNganSach.sTM,
		BH_DM_MucLucNganSach.sTTM,
		BH_DM_MucLucNganSach.sNG,
		BH_DM_MucLucNganSach.sTNG,
		BH_DM_MucLucNganSach.sXauNoiMa,
		BH_DM_MucLucNganSach.sMoTa as sMoTa,
		BH_DM_MucLucNganSach.bHangCha as bHangCha,
		'' as sGhiChu,
		0 as fQTQuyTruoc,
		0 as fTamUngQuyNay,
		0 as fLuyKeCapDenCuoiQuy,
		'' as iID_MaCoSoYTe,
		'' as sTenCoSoYTe
		into tblMucLucNganSach
		from BH_DM_MucLucNganSach 
		where sLNS  IN (SELECT * FROM f_split(@LNS))   and sLNS LIKE '904%'
		--and iNamLamViec = @NamLamViec


	---Hiển thị mục lục ngân sách con theo đơn vị cơ sở y tế được chọn
	select  
		CAST(NULL AS VARCHAR(50)) as iID_BH_CP_CapTamUng_KCB_BHYT_ChiTiet,
		tblMucLucNganSach.iID_MLNS as iID_MLNS,
		tblMucLucNganSach.iID_MLNS_Cha,
		tblMucLucNganSach.sLNS,
		tblMucLucNganSach.sL,
		tblMucLucNganSach.sK,
		tblMucLucNganSach.sM,
		tblMucLucNganSach.sTM,
		tblMucLucNganSach.sTTM,
		tblMucLucNganSach.sNG,
		tblMucLucNganSach.sTNG,
		tblMucLucNganSach.sXauNoiMa,
		tblMucLucNganSach.sMoTa as sMoTa,
		tblMucLucNganSach.bHangCha as bHangCha,
		'' as sGhiChu,
		0 as fQTQuyTruoc,
		0 as fTamUngQuyNay,
		0 as fLuyKeCapDenCuoiQuy,
		DM_CoSoYTe.iID_MaCoSoYTe, 
		concat(DM_CoSoYTe.iID_MaCoSoYTe, '-', DM_CoSoYTe.sTenCoSoYTe) as sTenCoSoYTe
		into #temp
		from tblMucLucNganSach cross join DM_CoSoYTe 
		where tblMucLucNganSach.bHangCha = 0 and DM_CoSoYTe.iID_MaCoSoYTe In (SELECT * FROM f_split(@IdCsYTe))

	---Map với bảng BH_CP_CapTamUng_KCB_BHYT_ChiTiet để lấy thông tin chi tiết

	select 
		ct.iID_BH_CP_CapTamUng_KCB_BHYT_ChiTiet as iID_BH_CP_CapTamUng_KCB_BHYT_ChiTiet,
		#temp.iID_MLNS,
		#temp.iID_MLNS_Cha,
		#temp.sLNS,
		#temp.sL,
		#temp.sK,
		#temp.sM,
		#temp.sTM,
		#temp.sTTM,
		#temp.sNG,
		#temp.sTNG,
		#temp.sXauNoiMa,
		#temp.sMoTa,
		0 as bHangCha,
		ct.sGhiChu as sGhiChu,
		ct.fQTQuyTruoc, 
		ct.fTamUngQuyNay,
		ct.fLuyKeCapDenCuoiQuy,
		#temp.iID_MaCoSoYTe,
		#temp.sTenCoSoYTe
		into tblCapPhatChiTiet
		from #temp
		left join (select * from BH_CP_CapTamUng_KCB_BHYT_ChiTiet where iID_BH_CP_CapTamUng_KCB_BHYT =  @ChungTuId)as ct
		on #temp.iID_MLNS = ct.iID_MLNS and #temp.iID_MaCoSoYTe = ct.iID_MaCoSoYTe


	--Kết quả trả về 

	select * from tblMucLucNganSach
	where bHangCha = 1
	union all
	select * from tblCapPhatChiTiet
	order by sXauNoiMa, iID_MaCoSoYTe

	---drop table
	drop table tblMucLucNganSach
	drop table #temp
	drop table tblCapPhatChiTiet
	
end
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_export_kehoachcaptamung_kcbbhyt]    Script Date: 8/24/2023 8:27:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_export_kehoachcaptamung_kcbbhyt]
@IdCsYTe NVARCHAR(MAX),
@ILoai int,
@IQuy int,
@NamLamViec int,
@UserName NVARCHAR(100),
@Donvitinh int
As
begin
	select 
			row_number() over (order by cptu_ct.iID_MaCoSoYTe) as sTT,
			sum(cptu_ct.fQTQuyTruoc)/@Donvitinh as fQTQuyTruoc, 
			sum(cptu_ct.fTamUngQuyNay)/@Donvitinh as fTamUngQuyNay, 
			sum(cptu_ct.fLuyKeCapDenCuoiQuy)/@Donvitinh as fLuyKeCapDenCuoiQuy, 
			cptu_ct.iID_MaCoSoYTe as iID_MaCoSoYTe,
			csyt.sTenCoSoYTe as sTenCoSoYTe
		from BH_CP_CapTamUng_KCB_BHYT_ChiTiet as cptu_ct
		inner join BH_CP_CapTamUng_KCB_BHYT as cptu on cptu_ct.iID_BH_CP_CapTamUng_KCB_BHYT = cptu.iID_BH_CP_CapTamUng_KCB_BHYT
		inner join DM_CoSoYTe as csyt on csyt.iID_MaCoSoYTe = cptu_ct.iID_MaCoSoYTe
		where cptu_ct.iID_MaCoSoYTe In (SELECT * FROM f_split(@IdCsYTe))
			and cptu.bIsTongHop <> 1 and cptu.sDSSoChungTuTongHop is null
			and cptu.iQuy = @IQuy
			--and ((@ILoai = 0 and cptu_ct.sLNS like '') or (@ILoai = 1 and cptu_ct.sLNS like ''))
		group by cptu_ct.iID_MaCoSoYTe, csyt.sTenCoSoYTe
		
end
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_export_tonghopcaptamung_kcbbhyt]    Script Date: 8/24/2023 8:27:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_export_tonghopcaptamung_kcbbhyt]
@IdCsYTe NVARCHAR(MAX),
@ILoai int,
@IQuy int,
@NamLamViec int,
@UserName NVARCHAR(100),
@Donvitinh int
As
begin
	select 
			row_number() over (order by cptu_ct.iID_MaCoSoYTe) as sTT,
			sum(cptu_ct.fQTQuyTruoc)/@Donvitinh as fQTQuyTruoc, 
			sum(cptu_ct.fTamUngQuyNay)/@Donvitinh as fTamUngQuyNay, 
			sum(cptu_ct.fLuyKeCapDenCuoiQuy)/@Donvitinh as fLuyKeCapDenCuoiQuy, 
			cptu_ct.iID_MaCoSoYTe as iID_MaCoSoYTe,
			csyt.sTenCoSoYTe as sTenCoSoYTe
		from BH_CP_CapTamUng_KCB_BHYT_ChiTiet as cptu_ct
		inner join BH_CP_CapTamUng_KCB_BHYT as cptu on cptu_ct.iID_BH_CP_CapTamUng_KCB_BHYT = cptu.iID_BH_CP_CapTamUng_KCB_BHYT
		inner join DM_CoSoYTe as csyt on csyt.iID_MaCoSoYTe = cptu_ct.iID_MaCoSoYTe
		where cptu_ct.iID_MaCoSoYTe In (SELECT * FROM f_split(@IdCsYTe))
			and  cptu.sDSSoChungTuTongHop is not null
			and cptu.iQuy = @IQuy
			--and ((@ILoai = 3 and cptu_ct.sLNS like '') or (@ILoai = 4 and cptu_ct.sLNS like ''))
		group by cptu_ct.iID_MaCoSoYTe, csyt.sTenCoSoYTe
		
end
GO
