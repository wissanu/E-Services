
SET QUOTED_IDENTIFIER OFF;
GO

IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

INSERT INTO [dbo].[EmailMessages] VALUES 
(9,N'��駤�����ʼ�ҹ����',N'Reset your password',
N'&lt;p&gt;��سҤ�ԡ {urllink0} ���͵�� ���ʼ�ҹ����&lt;/p&gt;

&lt;p&gt;�ҡ�������ö��ԡ URL ��ҧ���� ������� (Copy) URL �ѧ��������ǹ���ҧ (Paste) � ��ͧ Address �ͧ Web Browser&lt;/p&gt;

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;p&gt;-------------------------------------------------------------------------------&lt;/p&gt;

&lt;p&gt;ʶҺѹ�Ԩ����оѲ���ѭ����������ͧ��дѺ��觪ҵ� (ͧ������Ҫ�)&lt;/p&gt;',
N'&lt;p&gt;Please click {urllink0} for resetting your password.&lt;/p&gt;

&lt;p&gt;If you can&amp;#39;t open the site, please copy the URL and paste it into a Web Browser&amp;#39;s address bar.&lt;/p&gt;

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;p&gt;-------------------------------------------------------------------------------&lt;/p&gt;

&lt;p&gt;The Gem and Jewelry Institute of Thailand (Public Organization)&lt;/p&gt;',null);