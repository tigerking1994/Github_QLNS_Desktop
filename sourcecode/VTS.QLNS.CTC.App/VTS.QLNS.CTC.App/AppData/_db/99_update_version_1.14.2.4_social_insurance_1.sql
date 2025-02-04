/****** Object:  StoredProcedure [dbo].[sp_bh_dttm_danhsach_pbdttm_chitiet]    Script Date: 3/29/2024 5:01:38 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_dttm_danhsach_pbdttm_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_dttm_danhsach_pbdttm_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dttm_danhsach_pbdttm_chitiet]    Script Date: 3/29/2024 5:01:38 PM ******/
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

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblMucLucNganSachPBDTTM]') AND type in (N'U')) drop table tblMucLucNganSachPBDTTM;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblDonViPBDTTM]') AND type in (N'U')) drop table tblDonViPBDTTM;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblChungTuNhanPhanBoPBDTTM]') AND type in (N'U')) drop table tblChungTuNhanPhanBoPBDTTM;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblChiTietDuToanNhanPBDTTM]') AND type in (N'U')) drop table tblChiTietDuToanNhanPBDTTM;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_MucLucPBDTTM]') AND type in (N'U')) drop table tbl_MucLucPBDTTM;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempPBDTTM]') AND type in (N'U')) drop table tempPBDTTM;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp1PBDTTM]') AND type in (N'U')) drop table temp1PBDTTM;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblSoChuaPhanBoPBDTTM]') AND type in (N'U')) drop table tblSoChuaPhanBoPBDTTM;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblMLNS_duplicatePBDTTM]') AND type in (N'U')) drop table tblMLNS_duplicatePBDTTM;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpNhanDTTM]') AND type in (N'U')) drop table tmpNhanDTTM;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpDaPhanBoDTTM]') AND type in (N'U')) drop table tmpDaPhanBoDTTM;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblResultThuMua]') AND type in (N'U')) drop table tblResultThuMua;

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
	into tblMucLucNganSachPBDTTM
	from BH_DM_MucLucNganSach 
	where sLNS  IN (SELECT * FROM f_split(@LNS)) and iNamLamViec = @NamLamViec  and sLNS LIKE '903%'

	---Lấy danh sách đơn vị được phân bổ
	select * 
	into tblDonViPBDTTM
	from DonVi where iNamLamViec = @NamLamViec and iID_MaDonVi  IN (SELECT * FROM f_split(@IdDonVi)) ;

	--lấy danh sách dự toán nhận phân bổ
	select *
	into tblChungTuNhanPhanBoPBDTTM
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
	into tblChiTietDuToanNhanPBDTTM
	from BH_DTTM_BHYT_ThanNhan_ChiTiet as nhanpb_chitiet
	inner join BH_DTTM_BHYT_ThanNhan as nhanpb on nhanpb.iID_DTTM_BHYT_ThanNhan = nhanpb_chitiet.iID_DTTM_BHYT_ThanNhan
	where nhanpb.iID_DTTM_BHYT_ThanNhan in (select iID_DTTM_BHYT_NhanPhanBo from tblChungTuNhanPhanBoPBDTTM)

	
	

	---Lấy  danh sách chi tiết nhận phân bổ có thông tin mục lục ngân sách
	select 
		tblChiTietDuToanNhanPBDTTM.iID_DTTM_BHYT_ThanNhan,
		tblChiTietDuToanNhanPBDTTM.iID_MLNS as iID_MLNS,
		tblMucLucNganSachPBDTTM.iID_MLNS_Cha,
		tblMucLucNganSachPBDTTM.sLNS,
		tblMucLucNganSachPBDTTM.sL,
		tblMucLucNganSachPBDTTM.sK, 
		tblMucLucNganSachPBDTTM.sM,
		tblMucLucNganSachPBDTTM.sTM,
		tblMucLucNganSachPBDTTM.sTTM,
		tblMucLucNganSachPBDTTM.sNG,
		tblMucLucNganSachPBDTTM.sTNG,
		tblMucLucNganSachPBDTTM.sXauNoiMa,
		tblMucLucNganSachPBDTTM.sNoiDung,
		tblChiTietDuToanNhanPBDTTM.sSoQuyetDinh,
		tblChiTietDuToanNhanPBDTTM.fDuToan,
		3 as Type
	into tbl_MucLucPBDTTM
	from tblMucLucNganSachPBDTTM
	inner join tblChiTietDuToanNhanPBDTTM on tblMucLucNganSachPBDTTM.iID_MLNS = tblChiTietDuToanNhanPBDTTM.iID_MLNS

	


	---Hiển thị danh sách chi tiết nhận phân bổ theo đơn vị được chọn phân bổ
	select  tbl_MucLucPBDTTM.*,tblDonViPBDTTM.iID_MaDonVi, concat(tblDonViPBDTTM.iID_MaDonVi, '-', tblDonViPBDTTM.sTenDonVi) as sTenDonVi, 0 as bHangCha
	into tempPBDTTM
	from tbl_MucLucPBDTTM cross join tblDonViPBDTTM 

	

	---Map với bảng BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet để lấy thông tin fDuToan đã được phân bổ
	select 
		tempPBDTTM.iID_DTTM_BHYT_ThanNhan, 
		chitiet_phanbo.iID_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet as iID_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet,
		tempPBDTTM.iID_MLNS,
		tempPBDTTM.iID_MLNS_Cha,
		tempPBDTTM.sLNS,
		tempPBDTTM.sL,
		tempPBDTTM.sK,
		tempPBDTTM.sM,
		tempPBDTTM.sTM,
		tempPBDTTM.sTTM,
		tempPBDTTM.sNG,
		tempPBDTTM.sTNG,
		tempPBDTTM.sXauNoiMa,
		tempPBDTTM.sNoiDung as sNoiDung,
		chitiet_phanbo.fDuToan as fDuToan,
		tempPBDTTM.fDuToan as fDuToanTruocDieuChinh,
		3 as Type,
		tempPBDTTM.iID_MaDonVi,
		tempPBDTTM.sTenDonVi,
		tempPBDTTM.sSoQuyetDinh,
		0 as bHangCha,
		0 as IsRemainRow
	into temp1PBDTTM
	from tempPBDTTM
	left join 
		(
			select * 
			from BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet 
			where iID_DTTM_BHYT_ThanNhan_PhanBo = @ChungTuId
		) as chitiet_phanbo
		on chitiet_phanbo.iID_DTTM_BHYT_ThanNhan = tempPBDTTM.iID_DTTM_BHYT_ThanNhan and chitiet_phanbo.iID_MaDonVi = tempPBDTTM.iID_MaDonVi and chitiet_phanbo.iID_MLNS = tempPBDTTM.iID_MLNS
		
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
	N'Số chưa phân bổ' as sNoiDung,
	chitiet_chuaphanbo.fDuToan as fDuToan,
	chitiet_chuaphanbo.fDuToan as fDuToanTruocDieuChinh,
	2 as Type,
	'' as iID_MaDonVi,
	'' as sTenDonVi,
	npb.sSoQuyetDinh as sSoQuyetDinh,
	1 as bHangCha,
	1 as IsRemainRow
	into tblSoChuaPhanBoPBDTTM
	from tblMucLucNganSachPBDTTM as muluc_ngansach
	inner join 
	(
		select (ISNULL(ct_npb.fDuToan,0) - ISNULL(ct_pb_t.fDuToan,0)) as fDuToan, ct_npb.iID_MLNS, ct_npb.iID_DTTM_BHYT_ThanNhan 
		from BH_DTTM_BHYT_ThanNhan_ChiTiet as ct_npb
		left join
			(
				select sum(fDuToan) as fDuToan , iID_MLNS, iID_DTTM_BHYT_ThanNhan
				from BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet as ct_pb
				where iID_DTTM_BHYT_ThanNhan in (select iID_DTTM_BHYT_NhanPhanBo from tblChungTuNhanPhanBoPBDTTM)
				group by  iID_MLNS, iID_DTTM_BHYT_ThanNhan
			)as ct_pb_t  

		on ct_pb_t.iID_MLNS = ct_npb.iID_MLNS and  ct_npb.iID_DTTM_BHYT_ThanNhan = ct_pb_t.iID_DTTM_BHYT_ThanNhan) as chitiet_chuaphanbo
		on chitiet_chuaphanbo.iID_MLNS = muluc_ngansach.iID_MLNS 
	inner join BH_DTTM_BHYT_ThanNhan as npb on npb.iID_DTTM_BHYT_ThanNhan = chitiet_chuaphanbo.iID_DTTM_BHYT_ThanNhan
	where npb.iID_DTTM_BHYT_ThanNhan in ( select iID_DTTM_BHYT_NhanPhanBo from tblChungTuNhanPhanBoPBDTTM)

	---- Lấy mục lục ngân sách có dupliate các mục lục ngân sách con
	select distinct
	tblSoChuaPhanBoPBDTTM.iID_DTTM_BHYT_ThanNhan as IID_DTTM_BHYT_ThanNhan,
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
	tblSoChuaPhanBoPBDTTM.fDuToan as fDuToan,
	tblSoChuaPhanBoPBDTTM.fDuToanTruocDieuChinh as fDuToanTruocDieuChinh,
	case when tblSoChuaPhanBoPBDTTM.Type = 2 then 2 else tblMucLucNganSach.Type end Type,
	'' as iID_MaDonVi,
	'' as sTenDonVi,
	tblSoChuaPhanBoPBDTTM.sSoQuyetDinh as sSoQuyetDinh,
	case when tblSoChuaPhanBoPBDTTM.Type = 2 then 1 else tblMucLucNganSach.bHangCha end bHangCha,
	0 as IsRemainRow
	into tblMLNS_duplicatePBDTTM
	from tblMucLucNganSachPBDTTM tblMucLucNganSach
	left join tblSoChuaPhanBoPBDTTM on tblMucLucNganSach.iID_MLNS = tblSoChuaPhanBoPBDTTM.iID_MLNS
	order by sXauNoiMa
	
	-----------
	-- Dữ liệu nhận phân bổ
	declare @IdDotNhan nvarchar(500) = (select sDS_DotNhan from BH_DTTM_BHYT_ThanNhan_PhanBo where iID_DTTM_BHYT_ThanNhan_PhanBo = @ChungTuId);

	select ct.iID_MLNS, ct.iID_DTTM_BHYT_ThanNhan, ct.fDuToan INTO tmpNhanDTTM from BH_DTTM_BHYT_ThanNhan_ChiTiet ct
	JOIN BH_DTTM_BHYT_ThanNhan dt on dt.iID_DTTM_BHYT_ThanNhan = ct.iID_DTTM_BHYT_ThanNhan
	where dt.iID_DTTM_BHYT_ThanNhan IN (select * from splitstring( @IdDotNhan))
	-----------
	-- Dữ liệu đã phân bổ
	declare @dNgayQuyetDinhThuMua Datetime = (select dNgayQuyetDinh from BH_DTTM_BHYT_ThanNhan_PhanBo where iID_DTTM_BHYT_ThanNhan_PhanBo = @ChungTuId);

	select iID_MLNS, iID_DTTM_BHYT_ThanNhan, (0 - SUM(ISNULL(ctct.fDuToan, 0))) fDuToan
	INTO tmpDaPhanBoDTTM from BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct
	JOIN BH_DTTM_BHYT_ThanNhan_PhanBo ct ON ctct.iID_DTTM_BHYT_ThanNhan_PhanBo = ct.iID_DTTM_BHYT_ThanNhan_PhanBo
	where 
	ct.iNamLamViec = @NamLamViec
	AND ct.dNgayQuyetDinh < @dNgayQuyetDinhThuMua
	group by iID_MLNS, iID_DTTM_BHYT_ThanNhan;
	----------------
	select * into tblResultThuMua from
	(
		SELECT * from tblMLNS_duplicatePBDTTM
		UNION ALL
		SELECT * from tblSoChuaPhanBoPBDTTM
		UNION ALL
		SELECT * from temp1PBDTTM
	) as test
	--order by test.sXauNoiMa, test.sSoQuyetDinh, test.iID_MaDonVi,test.Type,test.IsRemainRow

	SELECT
	rs.IID_DTTM_BHYT_ThanNhan,
	rs.iID_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet,
	rs.iID_MLNS,
	rs.iID_MLNS_Cha,
	rs.sLNS,
	rs.sL,
	rs.sK,
	rs.sM,
	rs.sTM,
	rs.sTTM,
	rs.sNG,
	rs.sTNG,
	rs.sXauNoiMa,
	rs.sNoiDung,
	rs.Type,
	rs.iID_MaDonVi,
	rs.sTenDonVi,
	rs.sSoQuyetDinh as sSoQuyetDinh,
	rs.IsRemainRow,
	rs.bHangCha,
	CASE WHEN rs.IsRemainRow = 1 THEN ISNULL(dt.fDuToan, 0) + (ISNULL(dpb.fDuToan, 0))
		WHEN rs.IsRemainRow = 0 AND rs.Type = 2 THEN ISNULL(dt.fDuToan,0) + (ISNULL(dpb.fDuToan, 0))
		ELSE rs.fDuToan
	END as fDuToan,
	CASE WHEN rs.IsRemainRow = 1 THEN ISNULL(dt.fDuToan,0) + (ISNULL(dpb.fDuToan, 0))
		WHEN rs.IsRemainRow = 0 AND rs.Type = 2 THEN ISNULL(dt.fDuToan,0) + (ISNULL(dpb.fDuToan, 0))
		ELSE rs.fDuToan
	END as fDuToanTruocDieuChinh
	FROM tblResultThuMua rs
	LEFT JOIN tmpNhanDTTM dt ON rs.iID_MLNS = dt.iID_MLNS and rs.IID_DTTM_BHYT_ThanNhan = dt.iID_DTTM_BHYT_ThanNhan
	LEFT JOIN tmpDaPhanBoDTTM dpb ON dpb.iID_MLNS = rs.iID_MLNS AND dpb.iID_DTTM_BHYT_ThanNhan = rs.IID_DTTM_BHYT_ThanNhan
	order by rs.sXauNoiMa, rs.sSoQuyetDinh, rs.iID_MaDonVi,rs.Type, rs.IsRemainRow


	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblMucLucNganSachPBDTTM]') AND type in (N'U')) drop table tblMucLucNganSachPBDTTM;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblDonViPBDTTM]') AND type in (N'U')) drop table tblDonViPBDTTM;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblChungTuNhanPhanBoPBDTTM]') AND type in (N'U')) drop table tblChungTuNhanPhanBoPBDTTM;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblChiTietDuToanNhanPBDTTM]') AND type in (N'U')) drop table tblChiTietDuToanNhanPBDTTM;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_MucLucPBDTTM]') AND type in (N'U')) drop table tbl_MucLucPBDTTM;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempPBDTTM]') AND type in (N'U')) drop table tempPBDTTM;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp1PBDTTM]') AND type in (N'U')) drop table temp1PBDTTM;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblSoChuaPhanBoPBDTTM]') AND type in (N'U')) drop table tblSoChuaPhanBoPBDTTM;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblMLNS_duplicatePBDTTM]') AND type in (N'U')) drop table tblMLNS_duplicatePBDTTM;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpNhanDTTM]') AND type in (N'U')) drop table tmpNhanDTTM;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpDaPhanBoDTTM]') AND type in (N'U')) drop table tmpDaPhanBoDTTM;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblResultThuMua]') AND type in (N'U')) drop table tblResultThuMua;

end
;
;
GO
