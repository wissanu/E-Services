
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/06/2015 00:50:16
-- Generated from EDMX file: C:\Users\PonG\Documents\Project\Git-Eservice\Git_system\Models\DataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;

GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO
-- BackupDatabase

--INSERT INTO [dbo].[Memberships] VALUES (null,N'ปวีณรักษ์',N'คชรินทร์',N'paweenruk',N'kotcharin',N'swu',null,N'53',N'rayong',N'21000',N'Thailand',N'paweenruk@gmail.com',null,null,N'644A3BD31A1DB351BF3D9E3C62E3406AB3B94DF9F7D7122BCE60B4143270D4A582C4BC12DC447ED1F5122669DE29943E8CB09C4D0CA8FB27A4903605AE5E4752','02/10/15 6:56 PM',null,1,null),(null,N'ปวีณรักษ์',N'คชรินทร์',N'paweenruk',N'kotcharin',N'swu',null,N'53',N'rayong',N'21000',N'Thailand',N'paweenruk2@gmail.com',null,null,N'BACDFBE56FBADD0B76FC9EF994779DDBA5FE6AA1891F62D2B96E5A69FD2A4D1D5F9CE31E1910C608EF70DAE289BA9D17FD0330D16F080A99651EFCC2E9AA4D49','02/10/15 7:01 PM',null,1,N'rfed');

INSERT INTO [dbo].[Products] VALUES 
(N'หลักสูตรกิจกรรมทองคำ และโลหะมีค่า',N'nameeng',null,null,null,null,null,1,1,1000,40,3000,100,3000,1000,1,1,1,1);

INSERT INTO [dbo].[Courses] VALUES (N'รุ่นที่ 1 การทดสอบการเพิ่มคอร์ส',N'nameeng','06/05/15 9:00 AM','06/06/15 7:00 PM',1,5,1);

