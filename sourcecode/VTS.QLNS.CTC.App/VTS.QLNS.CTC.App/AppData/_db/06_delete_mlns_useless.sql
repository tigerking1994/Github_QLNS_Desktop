-- tao bang backup mlns
SELECT * INTO NS_MucLucNganSach_Back202212
FROM NS_MucLucNganSach
where iNamLamViec = 2023;


--- delete mlns khong su dung
delete  NS_MucLucNganSach 
where sXauNoiMa  not in (select mt.sXauNoiMa from NS_MucLucNganSach_Khoi as mt)
and iNamLamViec = 2023
and sLNS in ('1020100', '1040100','1040200','1040300','1020901')
and sXauNoiMa not in 
(
   select  CONCAT(sLNS, '-', sL,'-',sK,'-',sM, '-', sTM, '-', sTTM,'-', sNG)  from  NS_BK_ChungTuChiTiet
	where iNamLamViec = 2023
	and sNG !=''
	union 
	select CONCAT(sLNS, '-', sL,'-',sK,'-',sM, '-', sTM, '-', sTTM,'-', sNG) from NS_CP_ChungTuChiTiet
	where iNamLamViec = 2023
	and sNG !=''
	union
	select CONCAT(sLNS, '-', sL,'-',sK,'-',sM, '-', sTM, '-', sTTM,'-', sNG)  from NS_DC_ChungTuChiTiet
	where iNamLamViec = 2023
	and sNG !=''
	union 
	select CONCAT(sLNS, '-', sL,'-',sK,'-',sM, '-', sTM, '-', sTTM,'-', sNG) from NS_DT_ChungTuChiTiet
	where iNamLamViec = 2023
	and sNG !=''
	union 
	select CONCAT(sLNS, '-', sL,'-',sK,'-',sM, '-', sTM, '-', sTTM,'-', sNG) from NS_DTDauNam_ChungTuChiTiet
	where iNamLamViec = 2023
	and sNG !=''
	union 
	select CONCAT(sLNS, '-', sL,'-',sK,'-',sM, '-', sTM, '-', sTTM,'-', sNG) from NS_DTDauNam_ChungTuChiTiet_CanCu
	where iNamLamViec = 2023
	and sNG !=''
	union 

	select CONCAT(sLNS, '-', sL,'-',sK,'-',sM, '-', sTM, '-', sTTM,'-', sNG) from NS_Nganh_ChungTuChiTiet
	where iNamLamViec = 2023
	and sNG !=''
	union 
	select CONCAT(sLNS, '-', sL,'-',sK,'-',sM, '-', sTM, '-', sTTM,'-', sNG) from NS_QT_ChungTuChiTiet
	where iNamLamViec = 2023
	and sNG !=''

)
and sNG !=''

--- insert mlns khoi bvtc , dn
INSERT INTO [dbo].[NS_MucLucNganSach]
           ([bDuPhong]
           ,[bHangMua]
           ,[bHangNhap]
           ,[bHienVat]
           ,[bNgay]
           ,[bPhanCap]
           ,[bSoNguoi]
           ,[bTonKho]
           ,[bTuChi]
		   ,[sLNS]
           ,[sK]
           ,[sL]
		   ,[sM]
		   ,[sTM]
		   ,[sTTM]
		   ,[sNG]
		   ,[sTNG]
           ,[sTNG1]
           ,[sTNG2]
           ,[sTNG3]
           ,[sXauNoiMa]
           ,[sMoTa]
           ,[iID_MLNS]
           ,[iNamLamViec]
		   ,[bHangCha]
           ,[bHangChaDuToan]
           ,[bHangChaQuyetToan]
           ,[sCPChiTietToi]
           ,[sDuToanChiTietToi]
           ,[sQuyetToanChiTietToi]
           ,[iLock]
           ,iTrangThai
           ,[sNguoiSua]
           ,[sNguoiTao]
		   ,[dNgaySua]
           ,[dNgayTao]
          )
select      0 
           ,0
           ,0
           ,1
           ,0
           ,0
           ,0
           ,0
           ,1
		   ,[sLNS]
           ,[sK]
           ,[sL]
		   ,[sM]
		   ,[sTM]
		   ,[sTTM]
		   ,[sNG]
		   ,''
           ,''
           ,''
           ,''
           ,[sXauNoiMa]
           ,[sMoTa]
           ,newid()
           ,2023
		   ,[bHangCha]
           ,[bHangChaDuToan]
           ,[bHangChaQuyetToan]
           ,[sCPChiTietToi]
           ,[sDuToanChiTietToi]
           ,[sQuyetToanChiTietToi]
           ,0
           ,1
           ,'maittp'
           ,'maittp'
		   ,'2022-12-12'
           ,'2022-12-12'
		from NS_MucLucNganSach_Khoi
		where sLNS in ('1020902', '1020903','1020600','1010100','1010200','1010300','1010400')
--- update mlns cha con 

	update mlns
	set iID_MLNS_Cha = tmp.iID_MLNS
	from NS_MucLucNganSach mlns
	inner join NS_MucLucNganSach_Khoi mlns_khoi
	on mlns.sXauNoiMa = mlns_khoi.sXauNoiMa
	inner join NS_MucLucNganSach tmp
	on tmp.sXauNoiMa = mlns_khoi.sXauNoiMaCha AND tmp.iNamLamViec = 2023
	and mlns.iNamLamViec = 2023
	and mlns.sLNS in ('1020902', '1020903','1020600','1010100','1010200','1010300','1010400')





