USE [SALES]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[V_SalesRecords]
AS

SELECT ManageSalesStaffs.MyLoginID As RptManagerID,B.StaffName As RptManagerName,
SalesTrans.LoginStaffID,A.StaffName,FORMAT(SalesDate, 'MM/yyyy')As MthYr,
Convert(varchar,SalesDate,103) As SalesDt ,SalesItem,CAST(Qty AS int)As Qty,
UnitPrice, cast(round(UnitPrice*Qty,2) as numeric(15,2)) As TotalPrice
from SalesTrans
join UserMaster A on A.LoginStaffID=SalesTrans.LoginStaffID
join ManageSalesStaffs on ManageSalesStaffs.MyStaffID= SalesTrans.LoginStaffID
join UserMaster B on B.LoginStaffID=ManageSalesStaffs.MyLoginID

GO

