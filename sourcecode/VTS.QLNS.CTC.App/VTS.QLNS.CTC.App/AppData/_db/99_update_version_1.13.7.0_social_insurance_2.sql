/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_intheolyke_KPQL_KCB_Khac_donvi]    Script Date: 12/21/2023 10:32:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_pbdtc_intheolyke_KPQL_KCB_Khac_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_pbdtc_intheolyke_KPQL_KCB_Khac_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_intheolyke_donvi]    Script Date: 12/21/2023 10:32:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_pbdtc_intheolyke_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_pbdtc_intheolyke_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_dtc_phanbo_getfor_inluyke_donvi]    Script Date: 12/21/2023 10:32:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dtc_phanbo_getfor_inluyke_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dtc_phanbo_getfor_inluyke_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_dtc_phanbo_getfor_dotnhan_donvi]    Script Date: 12/21/2023 10:32:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dtc_phanbo_getfor_dotnhan_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dtc_phanbo_getfor_dotnhan_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_dtc_phanbo_get_donvi]    Script Date: 12/21/2023 10:32:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dtc_phanbo_get_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dtc_phanbo_get_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet_clone]    Script Date: 12/21/2023 10:32:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_dt_danhsach_pbdtc_chitiet_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet_clone]    Script Date: 12/21/2023 10:32:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet_clone]
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
	1 as Type,
	'' as iID_MaDonVi,
	'' as sTenDonVi,
	'' as sSoQuyetDinh,
	BH_DM_MucLucNganSach.bHangCha as bHangCha,
	0 as IsRemainRow
	into #tblMucLucNganSach
	from BH_DM_MucLucNganSach 
	where sLNS  IN (SELECT * FROM f_split(@LNS)) and iNamLamViec = @NamLamViec 

	---Lấy danh sách đơn vị được phân bổ
	select * 
	into  #tblDonVi
	from DonVi where iNamLamViec = @NamLamViec and iID_MaDonVi  IN (SELECT * FROM f_split(@IdDonVi)) ;

	--lấy danh sách dự toán nhận phân bổ
	select *
	into #tblChungTuNhanPhanBo
	from BH_DTC_Nhan_PhanBo_Map
	where iID_BHDTC_PhanBo = @ChungTuId

	
	---Lấy danh sách chi tiết dự toán toán nhận phân bổ
	select 
			nhanpb_chitiet.ID as iIDNhan_ChiTiet,
			nhanpb_chitiet.iID_DTC_DuToanChiTrenGiao as iID_DTC_DuToanChiTrenGiao,
			'' as iID_MaDonVi,
			nhanpb_chitiet.iID_MucLucNganSach,
			nhanpb_chitiet.fTongTien as fDuToan,
			nhanpb.sSoQuyetDinh
	into #tblChiTietDuToanNhan
	from BH_DTC_DuToanChiTrenGiao_ChiTiet as nhanpb_chitiet
	inner join BH_DTC_DuToanChiTrenGiao as nhanpb on nhanpb.ID = nhanpb_chitiet.iID_DTC_DuToanChiTrenGiao
	where iID_DTC_DuToanChiTrenGiao in (select iID_BHDTC_NhanPhanBo from #tblChungTuNhanPhanBo)

	

	---Lấy  danh sách chi tiết nhận phân bổ có thông tin mục lục ngân sách
	select 
		#tblChiTietDuToanNhan.iID_DTC_DuToanChiTrenGiao,
		#tblChiTietDuToanNhan.iID_MucLucNganSach as iID_MLNS,
		#tblMucLucNganSach.iID_MLNS_Cha,
		#tblMucLucNganSach.sLNS,
		#tblMucLucNganSach.sL,
		#tblMucLucNganSach.sK, 
		#tblMucLucNganSach.sM,
		#tblMucLucNganSach.sTM,
		#tblMucLucNganSach.sTTM,
		#tblMucLucNganSach.sNG,
		#tblMucLucNganSach.sTNG,
		#tblMucLucNganSach.sXauNoiMa,
		#tblMucLucNganSach.sNoiDung,
		#tblChiTietDuToanNhan.sSoQuyetDinh,
		#tblChiTietDuToanNhan.fDuToan as fDuToan ,
		3 as Type
	into #tbl_tblChiTietDuToanNhan_MucLuc
	from #tblMucLucNganSach
	inner join #tblChiTietDuToanNhan on #tblMucLucNganSach.iID_MLNS = #tblChiTietDuToanNhan.iID_MucLucNganSach

	


	---Hiển thị danh sách chi tiết nhận phân bổ theo đơn vị được chọn phân bổ
	select  #tbl_tblChiTietDuToanNhan_MucLuc.*,#tblDonVi.iID_MaDonVi, concat(#tblDonVi.iID_MaDonVi, '-', #tblDonVi.sTenDonVi) as sTenDonVi, 0 as bHangCha
	into #temp
	from #tbl_tblChiTietDuToanNhan_MucLuc cross join #tblDonVi 


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
		#temp.fDuToan as fTienTuChiTruocDieuChinh,
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
	chitiet_chuaphanbo.fTongTien as fTienTuChi,
	chitiet_chuaphanbo.fTongTien as fTienTuChiTruocDieuChinh,
	2 as Type,
	'' as iID_MaDonVi,
	'' as sTenDonVi,
	npb.sSoQuyetDinh as sSoQuyetDinh,
	1 as bHangCha,
	1 as IsRemainRow
	into #tblSoChuaPhanBo
	from #tblMucLucNganSach as muluc_ngansach
	inner join 
	(
		select (ISNULL(ct_npb.fTongTien,0) - ISNULL(ct_pb_t.fTongTien,0)) as fTongTien, ct_npb.iID_MucLucNganSach, ct_npb.iID_DTC_DuToanChiTrenGiao 
		from BH_DTC_DuToanChiTrenGiao_ChiTiet as ct_npb
		left join
			(
				select sum(fTongTien) as fTongTien  , iID_MucLucNganSach, iID_DTC_DuToanChiTrenGiao
				from BH_DTC_PhanBoDuToanChi_ChiTiet as ct_pb
				where iID_DTC_DuToanChiTrenGiao in (select iID_BHDTC_NhanPhanBo from #tblChungTuNhanPhanBo)
				group by  iID_MucLucNganSach, iID_DTC_DuToanChiTrenGiao
			)as ct_pb_t  

		on ct_pb_t.iID_MucLucNganSach = ct_npb.iID_MucLucNganSach and ct_npb.iID_DTC_DuToanChiTrenGiao = ct_pb_t.iID_DTC_DuToanChiTrenGiao) as chitiet_chuaphanbo
		on chitiet_chuaphanbo.iID_MucLucNganSach = muluc_ngansach.iID_MLNS 
	inner join BH_DTC_DuToanChiTrenGiao as npb on npb.ID = chitiet_chuaphanbo.iID_DTC_DuToanChiTrenGiao
	where npb.ID in ( select iID_BHDTC_NhanPhanBo from #tblChungTuNhanPhanBo)


	---- Lấy mục lục ngân sách có dupliate các mục lục ngân sách con
	select 
	#tblSoChuaPhanBo.iID_DTC_DuToanChiTrenGiao as iID_DTC_DuToanChiTrenGiao,
	CAST(NULL AS VARCHAR(50)) as iID_DTC_PhanBoDuToanChiTiet,
	#tblMucLucNganSach.iID_MLNS as iID_MLNS,
	#tblMucLucNganSach.iID_MLNS_Cha,
	#tblMucLucNganSach.sLNS,
	#tblMucLucNganSach.sL,
	#tblMucLucNganSach.sK,
	#tblMucLucNganSach.sM,
	#tblMucLucNganSach.sTM,
	#tblMucLucNganSach.sTTM,
	#tblMucLucNganSach.sNG,
	#tblMucLucNganSach.sTNG,
	#tblMucLucNganSach.sXauNoiMa,
	#tblMucLucNganSach.sNoiDung as sNoiDung,
	#tblSoChuaPhanBo.fTienTuChi as fTienTuChi,
	#tblSoChuaPhanBo.fTienTuChiTruocDieuChinh as fTienTuChiTruocDieuChinh,
	case when #tblSoChuaPhanBo.Type = 2 then 2 else #tblMucLucNganSach.Type end Type,
	'' as iID_MaDonVi,
	'' as sTenDonVi,
	#tblSoChuaPhanBo.sSoQuyetDinh as sSoQuyetDinh,
	case when #tblSoChuaPhanBo.Type = 2 then 1 else #tblMucLucNganSach.bHangCha end bHangCha,
	0 as IsRemainRow
	into #tblMucLucNganSach_duplicate
	from #tblMucLucNganSach
	left join #tblSoChuaPhanBo on #tblMucLucNganSach.iID_MLNS = #tblSoChuaPhanBo.iID_MLNS
	order by sXauNoiMa
	
	--select * from tblMucLucNganSach_duplicate
	--select * from tblSoChuaPhanBo
	--select * from #temp1

	--Hiển thị kết quả trả về
	select * from
	(
		SELECT * from #tblMucLucNganSach_duplicate
		UNION ALL
		SELECT * from #tblSoChuaPhanBo
		UNION ALL
		SELECT * from #temp1
	) as test
	order by test.sXauNoiMa, test.sSoQuyetDinh, test.iID_MaDonVi,test.Type,test.IsRemainRow


drop table #tblMucLucNganSach;
drop table #tblDonVi;
drop table #tblChungTuNhanPhanBo;
drop table #tblChiTietDuToanNhan;
drop table #tbl_tblChiTietDuToanNhan_MucLuc;
drop table #temp;
drop table #temp1;
drop table #tblSoChuaPhanBo;
drop table #tblMucLucNganSach_duplicate;

end
GO
/****** Object:  StoredProcedure [dbo].[sp_dtc_phanbo_get_donvi]    Script Date: 12/21/2023 10:32:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dtc_phanbo_get_donvi]
@NamLamViec int,
@IDLoaiChi uniqueidentifier,
@MaLoaichi nvarchar(50)
AS BEGIN
SET NOCOUNT ON;

SELECT 
	donvi.iID_DonVi AS Id,
	donvi.iID_MaDonVi as IIDMaDonVi,
	donvi.sTenDonVi as TenDonVi,
	donvi.sKyHieu as KyHieu,
	donvi.sMoTa as MoTa,
	donvi.iLoai as Loai,
	donvi.iNamLamViec as NamLamViec,
	donvi.iTrangThai as iTrangThai,
	donvi.dNgayTao as DNgayTao,
	donvi.sNguoiTao as SNguoiTao,
	donvi.dNgaySua as DNgaySua,
	donvi.dNgaySua as SNguoiSua,
	donvi.*

FROM BH_DTC_PhanBoDuToanChi chungtu
LEFT JOIN DonVi donvi ON donvi.iID_MaDonVi in (SELECT * FROM splitstring(chungtu.sID_MaDonVi))
WHERE chungtu.iNamChungTu = @NamLamViec
  AND chungtu.sID_MaDonVi <> ''
  AND chungtu.sID_MaDonVi IS NOT NULL
  AND donvi.iNamLamViec = @NamLamViec
  --AND chungtu.iID_LoaiDanhMucChi=@IDLoaiChi
  AND chungtu.sMaLoaiChi=@MaLoaichi
  --AND chungtu.iLoaiTongHop=@LoaiChungTu
ORDER BY cast(chungtu.dNgayChungTu AS date) END
;
;



GO
/****** Object:  StoredProcedure [dbo].[sp_dtc_phanbo_getfor_dotnhan_donvi]    Script Date: 12/21/2023 10:32:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[sp_dtc_phanbo_getfor_dotnhan_donvi]
@NamLamViec int,
@IDLoaiChi uniqueidentifier,
@MaLoaichi nvarchar(50),
@MaDonVi nvarchar(50)
AS BEGIN
SET NOCOUNT ON;

SELECT 
	donvi.iID_DonVi AS Id,
	donvi.iID_MaDonVi as IIDMaDonVi,
	donvi.sTenDonVi as TenDonVi,
	donvi.sKyHieu as KyHieu,
	donvi.sMoTa as MoTa,
	donvi.iLoai as Loai,
	donvi.iNamLamViec as NamLamViec,
	donvi.iTrangThai as iTrangThai,
	donvi.dNgayTao as DNgayTao,
	donvi.sNguoiTao as SNguoiTao,
	donvi.dNgaySua as DNgaySua,
	donvi.dNgaySua as SNguoiSua,
	donvi.*

FROM BH_DTC_PhanBoDuToanChi chungtu
LEFT JOIN DonVi donvi ON donvi.iID_MaDonVi in (SELECT * FROM splitstring(@MaDonVi))
WHERE chungtu.iNamChungTu = @NamLamViec
  AND chungtu.sID_MaDonVi <> ''
  AND chungtu.sID_MaDonVi IS NOT NULL
  AND donvi.iNamLamViec = @NamLamViec
  --AND chungtu.iID_LoaiDanhMucChi=@IDLoaiChi
  AND chungtu.sMaLoaiChi=@MaLoaichi
  --AND chungtu.iLoaiTongHop=@LoaiChungTu
ORDER BY cast(chungtu.dNgayChungTu AS date) END
;
;



GO
/****** Object:  StoredProcedure [dbo].[sp_dtc_phanbo_getfor_inluyke_donvi]    Script Date: 12/21/2023 10:32:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_dtc_phanbo_getfor_inluyke_donvi]
@NamLamViec int,
@IDLoaiChi uniqueidentifier,
@MaLoaichi nvarchar(50),
@SoQuyetDinh nvarchar(50),
@SNgayQuyetDinh nvarchar(50)
AS BEGIN
SET NOCOUNT ON;

SELECT 
	donvi.iID_DonVi AS Id,
	donvi.iID_MaDonVi as IIDMaDonVi,
	donvi.sTenDonVi as TenDonVi,
	donvi.sKyHieu as KyHieu,
	donvi.sMoTa as MoTa,
	donvi.iLoai as Loai,
	donvi.iNamLamViec as NamLamViec,
	donvi.iTrangThai as iTrangThai,
	donvi.dNgayTao as DNgayTao,
	donvi.sNguoiTao as SNguoiTao,
	donvi.dNgaySua as DNgaySua,
	donvi.dNgaySua as SNguoiSua,
	donvi.*

FROM BH_DTC_PhanBoDuToanChi chungtu
LEFT JOIN DonVi donvi ON donvi.iID_MaDonVi in (SELECT * FROM splitstring(chungtu.sID_MaDonVi))
WHERE chungtu.iNamChungTu = @NamLamViec
  AND chungtu.sID_MaDonVi <> ''
  AND chungtu.sID_MaDonVi IS NOT NULL
  AND donvi.iNamLamViec = @NamLamViec
  AND chungtu.sSoQuyetDinh=@SoQuyetDinh
  AND CAST(chungtu.dNgayQuyetDinh AS date) <= @SNgayQuyetDinh
  --AND cast(chungtu.dNgayQuyetDinh AS date) <= @SNgayQuyetDinh)
  --AND chungtu.iID_LoaiDanhMucChi=@IDLoaiChi
  AND chungtu.sMaLoaiChi=@MaLoaichi
  --AND chungtu.iLoaiTongHop=@LoaiChungTu
ORDER BY cast(chungtu.dNgayChungTu AS date) END
;
;



GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_intheolyke_donvi]    Script Date: 12/21/2023 10:32:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_rpt_dt_pbdtc_intheolyke_donvi]
	@INamLamViec int,
	@IdMaDonVi nvarchar(MAX),
	@IDLoaiChi nvarchar(MAX),
	@SNgayQuyetDinh nvarchar(MAX),
	@SoQuyetdinh nvarchar(MAX),
	@Donvitinh int
AS
BEGIN

select ctct.* into #tempall from BH_DTC_PhanBoDuToanChi ct
right join BH_DTC_PhanBoDuToanChi_ChiTiet ctct on ct.ID=ctct.iID_DTC_PhanBoDuToanChi
where ct.iNamChungTu=2023
and ct.iID_LoaiDanhMucChi=@IDLoaiChi
and cast(ct.dNgayQuyetDinh as date) <= @SNgayQuyetDinh
and ct.sSoQuyetDinh=@SoQuyetdinh
and ctct.iID_MaDonVi =@IdMaDonVi

select 
	 CAST(ROW_NUMBER() OVER (ORDER BY dv.sTenDonVi) AS VARCHAR(6)) STT
	, dv.sTenDonVi
	, dv.iID_MaDonVi
	, 0 fTienTroCapOmDau
	, 0 fTienTroCapThaiSan
	, 0 fTienTroCapTaiNan
	, 0 fTienTroCapHuuTri
	, 0 fTienTroCapPhucVien
	, 0 fTienTroCapXuatNgu
	, 0 fTienTroCapThoiViec
	, 0 fTienTroCapTuTuat
	, 1 IsHangCha
	, 0 RowNumber
	into #temp1
from 
#tempall ctct
left join DonVi dv on ctct.iID_MaDonVi= dv.iID_MaDonVi
where ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi))
and
dv.iNamLamViec=@INamLamViec
and
ctct.iNamLamViec=@INamLamViec
group by  dv.iID_MaDonVi, dv.sTenDonVi ,ctct.sXauNoiMa

select
	null as STT
	, N'Khối dự toán' as sTenDonVi
	, ctct.iID_MaDonVi
	,  (CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0001%' THEN Sum(ctct.fTongTien)/ @Donvitinh  ELSE 0 END ) fTienTroCapOmDau
	,  (CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0002%' THEN Sum(ctct.fTongTien)/ @Donvitinh  ELSE 0 END )fTienTroCapThaiSan
	,  (CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0003%' THEN Sum(ctct.fTongTien)/ @Donvitinh   ELSE 0 END )fTienTroCapTaiNan
	,  (CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0004%' THEN Sum(ctct.fTongTien)/ @Donvitinh   ELSE 0 END )fTienTroCapHuuTri
	,  (CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0005%' THEN Sum(ctct.fTongTien)/ @Donvitinh   ELSE 0 END )fTienTroCapPhucVien
	,  (CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0006%' THEN Sum(ctct.fTongTien)/ @Donvitinh   ELSE 0 END )fTienTroCapXuatNgu
	,  (CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0007%' THEN Sum(ctct.fTongTien)/ @Donvitinh   ELSE 0 END )fTienTroCapThoiViec
	,  (CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0008%' THEN Sum(ctct.fTongTien)/ @Donvitinh   ELSE 0 END )fTienTroCapTuTuat
	, 0 IsHangCha
	, 1 RowNumber
	into #temp2
from #tempall ctct
where ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi)) and ctct.iNamLamViec=@INamLamViec
group by ctct.iID_DonVi, ctct.iID_MaDonVi ,ctct.sXauNoiMa

select
	null as STT
	, N'Khối hạch toán' as sTenDonVi
	, ctct.iID_MaDonVi
	,  (CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0001%' THEN Sum(ctct.fTongTien)/ @Donvitinh   ELSE 0 END )fTienTroCapOmDau
	,  (CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0002%' THEN Sum(ctct.fTongTien)/ @Donvitinh   ELSE 0 END )fTienTroCapThaiSan
	,  (CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0003%' THEN Sum(ctct.fTongTien)/ @Donvitinh   ELSE 0 END )fTienTroCapTaiNan
	,  (CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0004%' THEN Sum(ctct.fTongTien)/ @Donvitinh   ELSE 0 END )fTienTroCapHuuTri
	,  (CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0005%' THEN Sum(ctct.fTongTien)/ @Donvitinh   ELSE 0 END )fTienTroCapPhucVien
	,  (CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0006%' THEN Sum(ctct.fTongTien)/ @Donvitinh   ELSE 0 END )fTienTroCapXuatNgu
	,  (CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0007%' THEN Sum(ctct.fTongTien)/ @Donvitinh   ELSE 0 END )fTienTroCapThoiViec
	,  (CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0008%' THEN Sum(ctct.fTongTien)/ @Donvitinh   ELSE 0 END )fTienTroCapTuTuat
	, 0 IsHangCha
	, 2 RowNumber
	into #temp3
from #tempall ctct
where ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi)) and ctct.iNamLamViec=@INamLamViec
group by ctct.iID_DonVi, ctct.iID_MaDonVi ,ctct.sXauNoiMa

SELECT  NEWID() as Id,* INTO #tempRESULT
from
(
	SELECT * FROM #temp1
	UNION ALL 
	SELECT * FROM #temp2
	UNION ALL 
	SELECT * FROM #temp3
) tempRESULT;

With #tree(id,sTenDonVi,iID_MaDonVi, STT, IsHangCha,fTienTroCapOmDau,fTienTroCapThaiSan,fTienTroCapTaiNan,fTienTroCapHuuTri,fTienTroCapPhucVien,fTienTroCapXuatNgu, fTienTroCapThoiViec, fTienTroCapTuTuat , RowNumber,position)
as
(
	SELECT  id,sTenDonVi,iID_MaDonVi, STT,IsHangCha,fTienTroCapOmDau,fTienTroCapThaiSan,fTienTroCapTaiNan,fTienTroCapHuuTri,fTienTroCapPhucVien,fTienTroCapXuatNgu, fTienTroCapThoiViec, fTienTroCapTuTuat,RowNumber,
						CAST(ROW_NUMBER() OVER(ORDER BY STT) AS NVARCHAR(MAX)) AS position
	FROM #tempRESULT
	WHERE  IsHangCha = 1 
	UNION ALL 
	SELECT chil.Id, chil.sTenDonVi, chil.iID_MaDonVi,   chil.STT, chil.IsHangCha,chil.fTienTroCapOmDau,chil.fTienTroCapThaiSan,chil.fTienTroCapTaiNan,chil.fTienTroCapHuuTri,chil.fTienTroCapPhucVien,chil.fTienTroCapXuatNgu, chil.fTienTroCapThoiViec, chil.fTienTroCapTuTuat,chil.RowNumber,
	CONCAT(pr.position,'.',CAST(ROW_NUMBER() OVER(ORDER BY chil.iID_MaDonVi) AS NVARCHAR(MAX))) AS position
	FROM #tempRESULT chil
	INNER JOIN #tree pr ON pr.iID_MaDonVi = chil.iID_MaDonVi AND  pr.IsHangCha = 1
	WHERE chil.STT IS NULL AND chil.IsHangCha = 0 

	
)
select *, 
		cast('/' + replace(position, '.', '/') + '/' as hierarchyid) AS sort

from #tree order by sort;

DROP TABLE #tempall
DROP TABLE #temp1
DROP TABLE #temp2
DROP TABLE #temp3
DROP TABLE #tempRESULT
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_intheolyke_KPQL_KCB_Khac_donvi]    Script Date: 12/21/2023 10:32:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[sp_rpt_dt_pbdtc_intheolyke_KPQL_KCB_Khac_donvi]
	@INamLamViec int,
	@IdMaDonVi nvarchar(MAX),
	@IDLoaiChi nvarchar(MAX),
	@MaLoaiChi nvarchar(MAX),
	@SNgayQuyetDinh nvarchar(MAX),
	@SoQuyetdinh nvarchar(MAX),
	@Donvitinh int
AS
BEGIN

select ctct.* into #tempall from BH_DTC_PhanBoDuToanChi ct
right join BH_DTC_PhanBoDuToanChi_ChiTiet ctct on ct.ID=ctct.iID_DTC_PhanBoDuToanChi
where ct.iNamChungTu=2023
and ct.iID_LoaiDanhMucChi=@IDLoaiChi
and ct.sMaLoaiChi=@MaLoaiChi
and cast(ct.dNgayQuyetDinh as date) <= @SNgayQuyetDinh
and ct.sSoQuyetDinh=@SoQuyetdinh
and ctct.iID_MaDonVi =@IdMaDonVi

select 
	 CAST(ROW_NUMBER() OVER (ORDER BY dv.sTenDonVi) AS VARCHAR(6)) STT
	, dv.sTenDonVi
	, dv.iID_MaDonVi
	, SUM(ctct.fTongTien) as fTongTienDuToan
	, 1 IsHangCha
	, 0 RowNumber
	into #temp1
from 
#tempall ctct
left join DonVi dv on ctct.iID_MaDonVi= dv.iID_MaDonVi
where ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi))
and
dv.iNamLamViec=@INamLamViec
and
ctct.iNamLamViec=@INamLamViec
group by  dv.iID_MaDonVi, dv.sTenDonVi ,ctct.sXauNoiMa



DROP TABLE #tempall
DROP TABLE #temp1
END
;
GO
