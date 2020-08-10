
SET QUOTED_IDENTIFIER OFF;
GO

IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

INSERT INTO [dbo].[EmailMessages] VALUES 
(9,N'ตั้งค่ารหัสผ่านใหม่',N'Reset your password',
N'&lt;p&gt;กรุณาคลิก {urllink0} เพื่อตั้ง รหัสผ่านใหม่&lt;/p&gt;

&lt;p&gt;หากไม่สามารถคลิก URL ข้างต้นได้ ให้สำเนา (Copy) URL ดังกล่าวแล้วนำไปวาง (Paste) ใน ช่อง Address ของ Web Browser&lt;/p&gt;

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;p&gt;-------------------------------------------------------------------------------&lt;/p&gt;

&lt;p&gt;สถาบันวิจัยและพัฒนาอัญมณีและเครื่องประดับแห่งชาติ (องค์การมหาชน)&lt;/p&gt;',
N'&lt;p&gt;Please click {urllink0} for resetting your password.&lt;/p&gt;

&lt;p&gt;If you can&amp;#39;t open the site, please copy the URL and paste it into a Web Browser&amp;#39;s address bar.&lt;/p&gt;

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;p&gt;-------------------------------------------------------------------------------&lt;/p&gt;

&lt;p&gt;The Gem and Jewelry Institute of Thailand (Public Organization)&lt;/p&gt;',null);