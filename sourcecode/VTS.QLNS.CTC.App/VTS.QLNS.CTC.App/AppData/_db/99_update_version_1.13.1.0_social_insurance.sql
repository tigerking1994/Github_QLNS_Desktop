/****** Object:  StoredProcedure [dbo].[sp_bh_export_phan_bo_du_toan_thumua_BHYT_chi_tiet]    Script Date: 8/16/2023 2:55:06 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_export_phan_bo_du_toan_thumua_BHYT_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_export_phan_bo_du_toan_thumua_BHYT_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dutoanthumua_dotnhan_phanbo_find_all]    Script Date: 8/16/2023 2:55:06 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_dutoanthumua_dotnhan_phanbo_find_all]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_dutoanthumua_dotnhan_phanbo_find_all]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dttm_danhsach_pbdttm_chitiet_dieuchinh]    Script Date: 8/16/2023 2:55:06 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_dttm_danhsach_pbdttm_chitiet_dieuchinh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_dttm_danhsach_pbdttm_chitiet_dieuchinh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dttm_danhsach_pbdttm_chitiet]    Script Date: 8/16/2023 2:55:06 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_dttm_danhsach_pbdttm_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_dttm_danhsach_pbdttm_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dttm_danhsach_dotnhan]    Script Date: 8/16/2023 2:55:06 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_dttm_danhsach_dotnhan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_dttm_danhsach_dotnhan]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet]    Script Date: 8/16/2023 2:55:06 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_dt_danhsach_pbdtc_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dt_danhsach_dotnhan]    Script Date: 8/16/2023 2:55:06 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_dt_danhsach_dotnhan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_dt_danhsach_dotnhan]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dt_danhsach_dotnhan]    Script Date: 8/16/2023 2:55:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_dt_danhsach_dotnhan]
	@IdPhanBo nvarchar(100)
AS
BEGIN
	select 
	pb.*
	from BH_DTC_PhanBoDuToanChi as pb
	inner join (select * from BH_DTC_Nhan_PhanBo_Map where iID_BHDTC_PhanBo = @IdPhanBo) as pb_map
	on pb_map.iID_BHDTC_NhanPhanBo = pb.ID
END
/****** Object:  StoredProcedure [dbo].[sp_dt_danhsach_chungtu_chitiet_luyke]    Script Date: 15/12/2021 6:35:25 PM ******/
SET ANSI_NULLS ON
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet]    Script Date: 8/16/2023 2:55:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet]
	@ChungTuId NVARCHAR(255),
	@LNS NVARCHAR(MAX),
	@IdDonVi NVARCHAR(MAX),
	@NamLamViec int,
	@UserName NVARCHAR(100)
As
begin
	---Lấy danh sách mục lục ngân sách theo điều kiện @LNS
	select 
	NEWID()  as iID_DTC_DuToanChiTrenGiao,
	CAST(NULL AS VARCHAR(50)) as iID_DTC_PhanBoDuToanChiTiet,
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
	BH_DM_MucLucNganSach.sMoTa as sNoiDung,
	0 as fTienTuChi,
	0 as fTienTuChiTruocDieuChinh,
	0 as fTienHienVat,
	0 as fTienHienVatTruocDieuChinh,
	1 as Type,
	'' as iID_MaDonVi,
	'' as sTenDonVi,
	'' as sSoQuyetDinh,
	BH_DM_MucLucNganSach.bHangCha as bHangCha,
	0 as IsRemainRow
	into tblMucLucNganSach
	from BH_DM_MucLucNganSach 
	where sLNS  IN (SELECT * FROM f_split(@LNS)) and iNamLamViec = @NamLamViec  and sLNS LIKE '901%'

	---Lấy danh sách đơn vị được phân bổ
	select * 
	into  tblDonVi
	from DonVi where iNamLamViec = 2023 and iID_MaDonVi  IN (SELECT * FROM f_split(@IdDonVi)) ;

	--lấy danh sách dự toán nhận phân bổ
	select *
	into tblChungTuNhanPhanBo
	from BH_DTC_Nhan_PhanBo_Map
	where iID_BHDTC_PhanBo = @ChungTuId

	

	---Lấy danh sách chi tiết dự toán toán nhận phân bổ
	select 
			nhanpb_chitiet.ID as iIDNhan_ChiTiet,
			nhanpb_chitiet.iID_DTC_DuToanChiTrenGiao as iID_DTC_DuToanChiTrenGiao,
			'' as iID_MaDonVi,
			nhanpb_chitiet.iID_MucLucNganSach,
			nhanpb_chitiet.fTienTuChi as fTuChi,
			nhanpb_chitiet.fTienHienVat as fHienVat,
			nhanpb.sSoQuyetDinh
	into tblChiTietDuToanNhan
	from BH_DTC_DuToanChiTrenGiao_ChiTiet as nhanpb_chitiet
	inner join BH_DTC_DuToanChiTrenGiao as nhanpb on nhanpb.ID = nhanpb_chitiet.iID_DTC_DuToanChiTrenGiao
	where iID_DTC_DuToanChiTrenGiao in (select iID_BHDTC_NhanPhanBo from tblChungTuNhanPhanBo)

	

	---Lấy  danh sách chi tiết nhận phân bổ có thông tin mục lục ngân sách
	select 
		tblChiTietDuToanNhan.iID_DTC_DuToanChiTrenGiao,
		tblChiTietDuToanNhan.iID_MucLucNganSach as iID_MLNS,
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
		tblMucLucNganSach.sNoiDung,
		tblChiTietDuToanNhan.sSoQuyetDinh,
		tblChiTietDuToanNhan.fTuChi as fTienTuChi ,
		tblChiTietDuToanNhan.fHienVat as fTienHienVat,
		3 as Type
	into tbl_tblChiTietDuToanNhan_MucLuc
	from tblMucLucNganSach
	inner join tblChiTietDuToanNhan on tblMucLucNganSach.iID_MLNS = tblChiTietDuToanNhan.iID_MucLucNganSach

	


	---Hiển thị danh sách chi tiết nhận phân bổ theo đơn vị được chọn phân bổ
	select  tbl_tblChiTietDuToanNhan_MucLuc.*,tblDonVi.iID_MaDonVi, concat(tblDonVi.iID_MaDonVi, '-', tblDonVi.sTenDonVi) as sTenDonVi, 0 as bHangCha
	into #temp
	from tbl_tblChiTietDuToanNhan_MucLuc cross join tblDonVi 


	---Map với bảng BH_DTC_PhanBoDuToanChi_ChiTiet để lấy thông tin fTuChi đã được phân bổ
	select 
		#temp.iID_DTC_DuToanChiTrenGiao, 
		chitiet_phanbo.ID as iID_DTC_PhanBoDuToanChiTiet,
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
		#temp.sNoiDung as sNoiDung,
		chitiet_phanbo.fTienTuChi as fTienTuChi,
		#temp.fTienTuChi as fTienTuChiTruocDieuChinh,
		chitiet_phanbo.fTienHienVat as fTienHienVat,
		#temp.fTienHienVat as fTienHienVatTruocDieuChinh,
		3 as Type,
		#temp.iID_MaDonVi,
		#temp.sTenDonVi,
		#temp.sSoQuyetDinh,
		0 as bHangCha,
		0 as IsRemainRow
	into #temp1
	from #temp
	left join 
		(
			select * 
			from BH_DTC_PhanBoDuToanChi_ChiTiet 
			where iID_DTC_PhanBoDuToanChi = @ChungTuId
		) as chitiet_phanbo
		on chitiet_phanbo.iID_DTC_DuToanChiTrenGiao = #temp.iID_DTC_DuToanChiTrenGiao and chitiet_phanbo.iID_MaDonVi = #temp.iID_MaDonVi and chitiet_phanbo.iID_MucLucNganSach = #temp.iID_MLNS



	-----Lấy danh sách số chưa phân bổ
	select 
	npb.ID as iID_DTC_DuToanChiTrenGiao,
	CAST(NULL AS VARCHAR(50)) as iID_DTC_PhanBoDuToanChiTiet,
	muluc_ngansach.iID_MLNS as iID_MLNS,
	muluc_ngansach.iID_MLNS_Cha,
	muluc_ngansach.sLNS,
	muluc_ngansach.sL,
	muluc_ngansach.sK,
	muluc_ngansach.sM,
	muluc_ngansach.sTM,
	muluc_ngansach.sTTM,
	muluc_ngansach.sNG,
	muluc_ngansach.sTNG,
	muluc_ngansach.sXauNoiMa,
	N'Số Chưa Phân Bổ' as sNoiDung,
	chitiet_chuaphanbo.fTuChi as fTienTuChi,
	chitiet_chuaphanbo.fTuChi as fTienTuChiTruocDieuChinh,
	chitiet_chuaphanbo.fHienVat as fTienHienVat,
	chitiet_chuaphanbo.fHienVat as fTienHienVatTruocDieuChinh,
	2 as Type,
	'' as iID_MaDonVi,
	'' as sTenDonVi,
	npb.sSoQuyetDinh as sSoQuyetDinh,
	1 as bHangCha,
	1 as IsRemainRow
	into tblSoChuaPhanBo
	from tblMucLucNganSach as muluc_ngansach
	inner join 
	(
		select (ISNULL(ct_npb.fTienTuChi,0) - ISNULL(ct_pb_t.fTuChi,0)) as fTuChi , (ISNULL(ct_npb.fTienHienVat,0) - ISNULL(ct_pb_t.fHienVat,0)) as fHienVat, ct_npb.iID_MucLucNganSach, ct_npb.iID_DTC_DuToanChiTrenGiao 
		from BH_DTC_DuToanChiTrenGiao_ChiTiet as ct_npb
		left join
			(
				select sum(fTienTuChi) as fTuChi , sum(fTienHienVat) as fHienVat , iID_MucLucNganSach, iID_DTC_DuToanChiTrenGiao
				from BH_DTC_PhanBoDuToanChi_ChiTiet as ct_pb
				where iID_DTC_DuToanChiTrenGiao in (select iID_BHDTC_NhanPhanBo from tblChungTuNhanPhanBo)
				group by  iID_MucLucNganSach, iID_DTC_DuToanChiTrenGiao
			)as ct_pb_t  

		on ct_pb_t.iID_MucLucNganSach = ct_npb.iID_MucLucNganSach and ct_npb.iID_DTC_DuToanChiTrenGiao = ct_pb_t.iID_DTC_DuToanChiTrenGiao) as chitiet_chuaphanbo
		on chitiet_chuaphanbo.iID_MucLucNganSach = muluc_ngansach.iID_MLNS 
	inner join BH_DTC_DuToanChiTrenGiao as npb on npb.ID = chitiet_chuaphanbo.iID_DTC_DuToanChiTrenGiao
	where npb.ID in ( select iID_BHDTC_NhanPhanBo from tblChungTuNhanPhanBo)


	---- Lấy mục lục ngân sách có dupliate các mục lục ngân sách con
	select 
	tblSoChuaPhanBo.iID_DTC_DuToanChiTrenGiao as iID_DTC_DuToanChiTrenGiao,
	CAST(NULL AS VARCHAR(50)) as iID_DTC_PhanBoDuToanChiTiet,
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
	tblMucLucNganSach.sNoiDung as sNoiDung,
	tblSoChuaPhanBo.fTienTuChi as fTienTuChi,
	tblSoChuaPhanBo.fTienTuChiTruocDieuChinh as fTienTuChiTruocDieuChinh,
	tblSoChuaPhanBo.fTienHienVat as fTienHienVat,
	tblSoChuaPhanBo.fTienHienVatTruocDieuChinh as fTienHienVatTruocDieuChinh,
	case when tblSoChuaPhanBo.Type = 2 then 2 else tblMucLucNganSach.Type end Type,
	'' as iID_MaDonVi,
	'' as sTenDonVi,
	tblSoChuaPhanBo.sSoQuyetDinh as sSoQuyetDinh,
	case when tblSoChuaPhanBo.Type = 2 then 1 else tblMucLucNganSach.bHangCha end bHangCha,
	0 as IsRemainRow
	into tblMucLucNganSach_duplicate
	from tblMucLucNganSach
	left join tblSoChuaPhanBo on tblMucLucNganSach.iID_MLNS = tblSoChuaPhanBo.iID_MLNS
	order by sXauNoiMa
	
	--select * from tblMucLucNganSach_duplicate
	--select * from tblSoChuaPhanBo
	--select * from #temp1

	--Hiển thị kết quả trả về
	select * from
	(
		SELECT * from tblMucLucNganSach_duplicate
		UNION ALL
		SELECT * from tblSoChuaPhanBo
		UNION ALL
		SELECT * from #temp1
	) as test
	order by test.sXauNoiMa, test.sSoQuyetDinh, test.iID_MaDonVi,test.Type,test.IsRemainRow


drop table tblMucLucNganSach;
drop table tblDonVi;
drop table tblChungTuNhanPhanBo;
drop table tblChiTietDuToanNhan;
drop table tbl_tblChiTietDuToanNhan_MucLuc;
drop table #temp;
drop table #temp1;
drop table tblSoChuaPhanBo;
drop table tblMucLucNganSach_duplicate;

end
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dttm_danhsach_dotnhan]    Script Date: 8/16/2023 2:55:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[sp_bh_dttm_danhsach_dotnhan]
	@IdPhanBo nvarchar(100)
AS
BEGIN
	select 
	pb.*
	from BH_DTTM_BHYT_ThanNhan_PhanBo as pb
	inner join (select * from BH_DTTM_BHYT_Nhan_PhanBo_Map where iID_DTTM_BHYT_PhanBo = @IdPhanBo) as pb_map
	on pb_map.iID_DTTM_BHYT_NhanPhanBo = pb.iID_DTTM_BHYT_ThanNhan_PhanBo
END
/****** Object:  StoredProcedure [dbo].[sp_dt_danhsach_chungtu_chitiet_luyke]    Script Date: 15/12/2021 6:35:25 PM ******/
SET ANSI_NULLS ON
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dttm_danhsach_pbdttm_chitiet]    Script Date: 8/16/2023 2:55:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_bh_dttm_danhsach_pbdttm_chitiet]
	@ChungTuId NVARCHAR(255),
	@LNS NVARCHAR(MAX),
	@IdDonVi NVARCHAR(MAX),
	@NamLamViec int,
	@UserName NVARCHAR(100)
As
begin
	---Lấy danh sách mục lục ngân sách theo điều kiện @LNS
	select 
	NEWID()  as IID_DTTM_BHYT_ThanNhan,
	CAST(NULL AS VARCHAR(50)) as iID_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet,
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
	BH_DM_MucLucNganSach.sMoTa as sNoiDung,
	0 as fDuToan,
	0 as fDuToanTruocDieuChinh,
	1 as Type,
	'' as iID_MaDonVi,
	'' as sTenDonVi,
	'' as sSoQuyetDinh,
	BH_DM_MucLucNganSach.bHangCha as bHangCha,
	0 as IsRemainRow
	into tblMucLucNganSach
	from BH_DM_MucLucNganSach 
	where sLNS  IN (SELECT * FROM f_split(@LNS)) and iNamLamViec = @NamLamViec  and sLNS LIKE '903%'

	---Lấy danh sách đơn vị được phân bổ
	select * 
	into  tblDonVi
	from DonVi where iNamLamViec = 2023 and iID_MaDonVi  IN (SELECT * FROM f_split(@IdDonVi)) ;

	--lấy danh sách dự toán nhận phân bổ
	select *
	into tblChungTuNhanPhanBo
	from BH_DTTM_BHYT_Nhan_PhanBo_Map
	where iID_DTTM_BHYT_PhanBo = @ChungTuId

	

	---Lấy danh sách chi tiết dự toán toán nhận phân bổ
	select 
			nhanpb_chitiet.iID_DTTM_BHYT_ThanNhan_ChiTiet as iID_DTTM_BHYT_ThanNhan_ChiTiet,
			nhanpb_chitiet.iID_DTTM_BHYT_ThanNhan as iID_DTTM_BHYT_ThanNhan,
			'' as iID_MaDonVi,
			nhanpb_chitiet.iID_MLNS,
			nhanpb_chitiet.fDuToan as fDuToan,
			nhanpb.sSoQuyetDinh
	into tblChiTietDuToanNhan
	from BH_DTTM_BHYT_ThanNhan_ChiTiet as nhanpb_chitiet
	inner join BH_DTTM_BHYT_ThanNhan as nhanpb on nhanpb.iID_DTTM_BHYT_ThanNhan = nhanpb_chitiet.iID_DTTM_BHYT_ThanNhan
	where nhanpb.iID_DTTM_BHYT_ThanNhan in (select iID_DTTM_BHYT_NhanPhanBo from tblChungTuNhanPhanBo)

	
	

	---Lấy  danh sách chi tiết nhận phân bổ có thông tin mục lục ngân sách
	select 
		tblChiTietDuToanNhan.iID_DTTM_BHYT_ThanNhan,
		tblChiTietDuToanNhan.iID_MLNS as iID_MLNS,
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
		tblMucLucNganSach.sNoiDung,
		tblChiTietDuToanNhan.sSoQuyetDinh,
		tblChiTietDuToanNhan.fDuToan,
		3 as Type
	into tbl_tblChiTietDuToanNhan_MucLuc
	from tblMucLucNganSach
	inner join tblChiTietDuToanNhan on tblMucLucNganSach.iID_MLNS = tblChiTietDuToanNhan.iID_MLNS

	


	---Hiển thị danh sách chi tiết nhận phân bổ theo đơn vị được chọn phân bổ
	select  tbl_tblChiTietDuToanNhan_MucLuc.*,tblDonVi.iID_MaDonVi, concat(tblDonVi.iID_MaDonVi, '-', tblDonVi.sTenDonVi) as sTenDonVi, 0 as bHangCha
	into #temp
	from tbl_tblChiTietDuToanNhan_MucLuc cross join tblDonVi 

	

	---Map với bảng BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet để lấy thông tin fDuToan đã được phân bổ
	select 
		#temp.iID_DTTM_BHYT_ThanNhan, 
		chitiet_phanbo.iID_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet as iID_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet,
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
		#temp.sNoiDung as sNoiDung,
		chitiet_phanbo.fDuToan as fDuToan,
		#temp.fDuToan as fDuToanTruocDieuChinh,
		3 as Type,
		#temp.iID_MaDonVi,
		#temp.sTenDonVi,
		#temp.sSoQuyetDinh,
		0 as bHangCha,
		0 as IsRemainRow
	into #temp1
	from #temp
	left join 
		(
			select * 
			from BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet 
			where iID_DTTM_BHYT_ThanNhan_PhanBo = @ChungTuId
		) as chitiet_phanbo
		on chitiet_phanbo.iID_DTTM_BHYT_ThanNhan = #temp.iID_DTTM_BHYT_ThanNhan and chitiet_phanbo.iID_MaDonVi = #temp.iID_MaDonVi and chitiet_phanbo.iID_MLNS = #temp.iID_MLNS


		
	-----Lấy danh sách số chưa phân bổ
	select 
	npb.iID_DTTM_BHYT_ThanNhan as iID_DTTM_BHYT_ThanNhan,
	CAST(NULL AS VARCHAR(50)) as iID_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet,
	muluc_ngansach.iID_MLNS as iID_MLNS,
	muluc_ngansach.iID_MLNS_Cha,
	muluc_ngansach.sLNS,
	muluc_ngansach.sL,
	muluc_ngansach.sK,
	muluc_ngansach.sM,
	muluc_ngansach.sTM,
	muluc_ngansach.sTTM,
	muluc_ngansach.sNG,
	muluc_ngansach.sTNG,
	muluc_ngansach.sXauNoiMa,
	N'Số Chưa Phân Bổ' as sNoiDung,
	chitiet_chuaphanbo.fDuToan as fDuToan,
	chitiet_chuaphanbo.fDuToan as fDuToanTruocDieuChinh,
	2 as Type,
	'' as iID_MaDonVi,
	'' as sTenDonVi,
	npb.sSoQuyetDinh as sSoQuyetDinh,
	1 as bHangCha,
	1 as IsRemainRow
	into tblSoChuaPhanBo
	from tblMucLucNganSach as muluc_ngansach
	inner join 
	(
		select (ISNULL(ct_npb.fDuToan,0) - ISNULL(ct_pb_t.fDuToan,0)) as fDuToan, ct_npb.iID_MLNS, ct_npb.iID_DTTM_BHYT_ThanNhan 
		from BH_DTTM_BHYT_ThanNhan_ChiTiet as ct_npb
		left join
			(
				select sum(fDuToan) as fDuToan , iID_MLNS, iID_DTTM_BHYT_ThanNhan
				from BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet as ct_pb
				where iID_DTTM_BHYT_ThanNhan in (select iID_DTTM_BHYT_NhanPhanBo from tblChungTuNhanPhanBo)
				group by  iID_MLNS, iID_DTTM_BHYT_ThanNhan
			)as ct_pb_t  

		on ct_pb_t.iID_MLNS = ct_npb.iID_MLNS and  ct_npb.iID_DTTM_BHYT_ThanNhan = ct_pb_t.iID_DTTM_BHYT_ThanNhan) as chitiet_chuaphanbo
		on chitiet_chuaphanbo.iID_MLNS = muluc_ngansach.iID_MLNS 
	inner join BH_DTTM_BHYT_ThanNhan as npb on npb.iID_DTTM_BHYT_ThanNhan = chitiet_chuaphanbo.iID_DTTM_BHYT_ThanNhan
	where npb.iID_DTTM_BHYT_ThanNhan in ( select iID_DTTM_BHYT_NhanPhanBo from tblChungTuNhanPhanBo)

	---- Lấy mục lục ngân sách có dupliate các mục lục ngân sách con
	select 
	tblSoChuaPhanBo.iID_DTTM_BHYT_ThanNhan as IID_DTTM_BHYT_ThanNhan,
	CAST(NULL AS VARCHAR(50)) as iID_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet,
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
	tblMucLucNganSach.sNoiDung as sNoiDung,
	tblSoChuaPhanBo.fDuToan as fDuToan,
	tblSoChuaPhanBo.fDuToanTruocDieuChinh as fDuToanTruocDieuChinh,
	case when tblSoChuaPhanBo.Type = 2 then 2 else tblMucLucNganSach.Type end Type,
	'' as iID_MaDonVi,
	'' as sTenDonVi,
	tblSoChuaPhanBo.sSoQuyetDinh as sSoQuyetDinh,
	case when tblSoChuaPhanBo.Type = 2 then 1 else tblMucLucNganSach.bHangCha end bHangCha,
	0 as IsRemainRow
	into tblMucLucNganSach_duplicate
	from tblMucLucNganSach
	left join tblSoChuaPhanBo on tblMucLucNganSach.iID_MLNS = tblSoChuaPhanBo.iID_MLNS
	order by sXauNoiMa
	

	--Hiển thị kết quả trả về
	select * from
	(
		SELECT * from tblMucLucNganSach_duplicate
		UNION ALL
		SELECT * from tblSoChuaPhanBo
		UNION ALL
		SELECT * from #temp1
	) as test
	order by test.sXauNoiMa, test.sSoQuyetDinh, test.iID_MaDonVi,test.Type,test.IsRemainRow


drop table tblMucLucNganSach;
drop table tblDonVi;
drop table tblChungTuNhanPhanBo;
drop table tblChiTietDuToanNhan;
drop table tbl_tblChiTietDuToanNhan_MucLuc;
drop table #temp;
drop table #temp1;
drop table tblSoChuaPhanBo;
drop table tblMucLucNganSach_duplicate;

end
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dttm_danhsach_pbdttm_chitiet_dieuchinh]    Script Date: 8/16/2023 2:55:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_bh_dttm_danhsach_pbdttm_chitiet_dieuchinh]
	@ChungTuId NVARCHAR(255),
	@LNS NVARCHAR(MAX),
	@NamLamViec int,
	@UserName NVARCHAR(100)
AS
begin
	---L?y danh sách m?c l?c ngân sách
	select 
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
	BH_DM_MucLucNganSach.sMoTa as sNoiDung,
	NULL as IID_DTTM_BHYT_ThanNhan,
	NULL as iID_DTTM_BHYT_ThanNhan_ChiTiet,
	NULL as iID_MaDonVi,
	NULL as sTenDonVi,
	NULL as sSoQuyetDinh,
	0 as fDuToanTruocDieuChinh,
	0 as fDuToan,
	0 as fDuToanSauDieuChinh,
	bHangCha,
	0 as Type
	into tblMucLucNganSach
	from BH_DM_MucLucNganSach 
	where sLNS  IN (SELECT * FROM f_split(@LNS)) and iNamLamViec = @NamLamViec and sLNS LIKE '903%'

	---L?y danh sách chung tu bi dieu chinh
	select * 
	into tblChungTuBiDieuChinh
	from BH_DTTM_BHYT_Nhan_PhanBo_Map
	where iID_DTTM_BHYT_PhanBo = @ChungTuId

	---L?y danh sách chi ti?t ch?ng t? bi dieu chinh
	select 
	dc_ct.iID_MLNS, 
	dc_ct.iID_MaDonVi,
	dc_ct.fDuToan as fDuToanTruocDieuChinh,
	dc_ct.iID_DTTM_BHYT_ThanNhan as iID_DTTM_BHYT_ThanNhan,
	dc_ct.iID_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet,
	dc.sSoQuyetDinh
	into tblChungTuBiDieuChinh_Ct 
	from BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet as dc_ct  
	inner join BH_DTTM_BHYT_ThanNhan_PhanBo as dc on dc_ct.iID_DTTM_BHYT_ThanNhan_PhanBo = dc.iID_DTTM_BHYT_ThanNhan_PhanBo
	where dc_ct.iID_DTTM_BHYT_ThanNhan_PhanBo in ( select iID_DTTM_BHYT_NhanPhanBo from tblChungTuBiDieuChinh)

	

	---Map voi bang BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet de lay thong tin fDuToan cua chung tu dieu chinh
	select 
	npb_ct.iID_MLNS,
	npb_ct.iID_MaDonVi,
	npb_ct.sSoQuyetDinh,
	npb_ct.fDuToanTruocDieuChinh as fDuToanTruocDieuChinh,
	npb_ct.iID_DTTM_BHYT_ThanNhan as iID_DTTM_BHYT_ThanNhan,
	pb_ct.iID_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet as iID_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet,
	pb_ct.fDuToan,
	0 as bHangCha,
	2 Type
	into tblThongTinChungTu_BiDieuChinh
	from tblChungTuBiDieuChinh_Ct as npb_ct
	left join ( select * from BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet where iID_DTTM_BHYT_ThanNhan_PhanBo =  @ChungTuId) as  pb_ct on npb_ct.iID_MLNS = pb_ct.iID_MLNS and npb_ct.iID_MaDonVi = pb_ct.iID_MaDonVi
	and npb_ct.iID_DTTM_BHYT_ThanNhan = pb_ct.iID_DTTM_BHYT_ThanNhan

	---Thong tin chi tiết chứng từ điều chỉnh thêm mới
	select 
	pb_ct.iID_MLNS,
	pb_ct.iID_MaDonVi,
	pb.sSoQuyetDinh,
	0 as fDuToanTruocDieuChinh,
	pb_ct.iID_DTTM_BHYT_ThanNhan as iID_DTTM_BHYT_ThanNhan,
	pb_ct.iID_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet,
	pb_ct.fDuToan,
	0 as bHangCha,
	2 Type
	into tblThongTinChungTu_DieuChinhThemMoi
	from BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet as pb_ct
	inner join BH_DTTM_BHYT_ThanNhan_PhanBo as pb on pb_ct.iID_DTTM_BHYT_ThanNhan_PhanBo = pb.iID_DTTM_BHYT_ThanNhan_PhanBo
	where pb_ct.iID_DTTM_BHYT_ThanNhan_PhanBo = @ChungTuId and pb_ct.iID_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet not in  (select iID_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet from  tblThongTinChungTu_BiDieuChinh where iID_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet is not null)

	

	---Thông tin chi tiết chúng từ
	select 
	*
	into tblThongTinChungTu
	from
		(
			select * from tblThongTinChungTu_BiDieuChinh
			UNION ALL
			select * from tblThongTinChungTu_DieuChinhThemMoi

		) as tbl


	--select * from tblThongTinChungTu
	--select * from tblMucLucNganSach

	---Hi?n th? k?t qu? tr? v? có m?c l?c ngân sách

	select 
	tblMucLucNganSach.iID_MLNS,
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
	tblMucLucNganSach.sNoiDung,
	tblThongTinChungTu.iID_DTTM_BHYT_ThanNhan,
	tblThongTinChungTu.iID_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet,
	tblThongTinChungTu.iID_MaDonVi,
	CONCAT(DonVi.iID_MaDonVi, '-', DonVi.sTenDonVi) as sTenDonVi,
	tblThongTinChungTu.sSoQuyetDinh,
	tblThongTinChungTu.fDuToanTruocDieuChinh,
	tblThongTinChungTu.fDuToan,
	ISNULL(tblThongTinChungTu.fDuToanTruocDieuChinh,0) + ISNULL(tblThongTinChungTu.fDuToan,0) as fDuToanSauDieuChinh,
	case when tblThongTinChungTu.Type = 2 then tblThongTinChungTu.bHangCha else tblMucLucNganSach.bHangCha end as bHangCha
	into tblThongTinChungTu_MLNS
	from tblMucLucNganSach
	left join tblThongTinChungTu on tblMucLucNganSach.iID_MLNS = tblThongTinChungTu.iID_MLNS
	left join DonVi on tblThongTinChungTu.iID_MaDonVi = DonVi.iID_MaDonVi and DonVi.InamLamViec = @NamLamViec

	select * from tblThongTinChungTu_MLNS 
	order  by sXauNoiMa, sSoQuyetDinh 

	drop table tblMucLucNganSach;
	drop table tblChungTuBiDieuChinh;
	drop table tblChungTuBiDieuChinh_Ct;
	drop table tblThongTinChungTu_BiDieuChinh
	drop table tblThongTinChungTu_DieuChinhThemMoi;
	drop table tblThongTinChungTu;
	drop table tblThongTinChungTu_MLNS;


end
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dutoanthumua_dotnhan_phanbo_find_all]    Script Date: 8/16/2023 2:55:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_dutoanthumua_dotnhan_phanbo_find_all]
@YearOfWork int ,
@Date DateTime,
@LoaiDuToanNhan int
AS
BEGIN
	DECLARE @DieuChinh int = 4;
	--Lấy danh sách dự toán nhận phân bổ
		SELECT iID_DTTM_BHYT_ThanNhan as iID_DTTM_BHYT_ThanNhan,
			   sSoChungTu,
			   sDSLNS,
			   iID_MaDonVi,
			   dNgayChungTu,
			   sSoQuyetDinh,
			   dNgayQuyetDinh,
			   fDuToan AS fSoPhanBo
		INTO  tblNhanPhanBo
		FROM BH_DTTM_BHYT_ThanNhan 
		WHERE iNamChungTu = @YearOfWork 
			AND iLoaiDuToan = @LoaiDuToanNhan
			AND (
				(dNgayQuyetDinh IS NOT NULL AND CAST(dNgayQuyetDinh AS DATE) <= CAST(@Date AS DATE)) 
				OR 
				(dNgayQuyetDinh IS NULL AND dNgayChungTu IS NOT NULL AND CAST(dNgayChungTu AS DATE) <= CAST(@Date AS DATE)))



		--Lấy danh sách dự toán đã được phân bổ
		SELECT ISNULL(sum(pb_ct.fDuToan),0) fDaPhanBo, pb_ct.iID_DTTM_BHYT_ThanNhan
		INTO tblChungTuNhanPhanBoMap
		FROM  BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet as pb_ct 
		WHERE pb_ct.iID_DTTM_BHYT_ThanNhan in (select iID_DTTM_BHYT_ThanNhan from  tblNhanPhanBo)
		GROUP BY pb_ct.iID_DTTM_BHYT_ThanNhan


		-----Lay danh sach du toan nhan phan bo, so phan bo, chua phan bo
		SELECT  npb.iID_DTTM_BHYT_ThanNhan as iID_DTTM_BHYT_ThanNhan,
			    npb.sSoChungTu, 
				npb.sDSLNS,
				npb.iID_MaDonVi,
				npb.dNgayChungTu, 
				npb.sSoQuyetDinh, 
				npb.dNgayQuyetDinh, 
				npb.fSoPhanBo, 
				npbm.fDaPhanBo,
				ISNULL(npb.fSoPhanBo,0) - ISNULL(npbm.fDaPhanBo,0) AS fSoChuaPhanBo
		FROM tblNhanPhanBo AS npb
		left join tblChungTuNhanPhanBoMap AS npbm ON npb.iID_DTTM_BHYT_ThanNhan = npbm.iID_DTTM_BHYT_ThanNhan

	   DROP TABLE tblNhanPhanBo;	
       DROP TABLE tblChungTuNhanPhanBoMap;

END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_export_phan_bo_du_toan_thumua_BHYT_chi_tiet]    Script Date: 8/16/2023 2:55:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[sp_bh_export_phan_bo_du_toan_thumua_BHYT_chi_tiet]
	@ChungTuId nvarchar(255),
	@LNS nvarchar(max),
	@NamLamViec int
As
Begin

select 
ngan_sach.iID_MLNS,
ngan_sach.iID_MLNS_Cha,
ngan_sach.sLNS,
ngan_sach.sL,
ngan_sach.sK,
ngan_sach.sM,
ngan_sach.sTM,
ngan_sach.sTTM,
ngan_sach.sNG,
ngan_sach.sTNG,
ngan_sach.sXauNoiMa,
ngan_sach.sMoTa as sNoiDung,
ngan_sach.bHangCha,
pb_ct.fDuToan,
pb_ct.IID_MaDonVi
from
BH_DM_MucLucNganSach as ngan_sach
left join
(
	select sum(fDuToan) as fDuToan, IID_MaDonVi, iID_MLNS
	from BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet 
	where iID_DTTM_BHYT_ThanNhan_PhanBo = @ChungTuId
	group by iID_MLNS, IID_MaDonVi) as pb_ct on pb_ct.iID_MLNS = ngan_sach.iID_MLNS
where ngan_sach.iNamLamViec  = @NamLamViec and ngan_sach.sLNS in (select * from f_split(@LNS))
order by sXauNoiMa
End
GO
