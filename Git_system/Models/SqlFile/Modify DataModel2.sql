
UPDATE [dbo].[Memberships]
SET [ExperienceInGem]= 0;

UPDATE [dbo].[Products]
SET [isShortTerm]= 1 , [isLongTerm]= 1;

INSERT INTO [dbo].[EmailMessages] VALUES 
(5,N'แจ้งการสมัครสมาชิก',N'',
N'&lt;p&gt;{fullname} ได้ทำการสมัครสมาชิก &lt;/p&gt;

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;p&gt;-------------------------------------------------------------------------------&lt;/p&gt;

&lt;p&gt;สถาบันวิจัยและพัฒนาอัญมณีและเครื่องประดับแห่งชาติ (องค์การมหาชน)&lt;/p&gt;',
N''),
(6,N'แจ้งการสั่งซื้อสินค้า',N'',
N'&lt;p&gt;{fullname} ได้ทำการสั้งซื้อสินค้า &lt;/p&gt;

{paymentList}

&lt;p&gt;{paymentTotalPrice}&lt;/p&gt;

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;p&gt;-------------------------------------------------------------------------------&lt;/p&gt;

&lt;p&gt;สถาบันวิจัยและพัฒนาอัญมณีและเครื่องประดับแห่งชาติ (องค์การมหาชน)&lt;/p&gt;',
N''),
(7,N'แจ้งการสมัครสมาชิกสถาบัน',N'',
N'&lt;p&gt;{fullname} ได้ทำการสมัครสมาชิกสถาบัน &lt;/p&gt;

{paymentList}

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;p&gt;-------------------------------------------------------------------------------&lt;/p&gt;

&lt;p&gt;สถาบันวิจัยและพัฒนาอัญมณีและเครื่องประดับแห่งชาติ (องค์การมหาชน)&lt;/p&gt;',
N''),
(8,N'แจ้งการยืนยันการชำระสินค้า',N'',
N'&lt;p&gt;{fullname} ได้ทำการยืนยันการสั่งซื้อสินค้า &lt;/p&gt;

{paymentList}

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;p&gt;-------------------------------------------------------------------------------&lt;/p&gt;

&lt;p&gt;สถาบันวิจัยและพัฒนาอัญมณีและเครื่องประดับแห่งชาติ (องค์การมหาชน)&lt;/p&gt;',
N'');

UPDATE [dbo].[EmailMessages]
SET [EmailAlert] = 'paweenruk@hotmail.com'
Where [Id] > 4;

INSERT INTO [dbo].[ProductNews] VALUES 
(N'ประกาศสั้น', N'ประกาศสั้นอังกฤษ'),
(N'ประกาศยาว', N'ประกาศยายอังกฤษ');


--Update default var on pay
UPDATE [dbo].[Pays]
SET [Fullname] = (Select ([Firstname] + ' ' + [Lastname]) as 'Fullname' From [dbo].[Memberships] where [Id] = [dbo].[Pays].[MembershipId]),
    [Address] = 'unknown';

INSERT INTO [dbo].[Benefits] VALUES 
(N'สิทธิประโยชน์บุคคลธรรมดา', N'สิทธิประโยชน์บุคคลธรรมดาอังกฤษ'),
(N'สิทธิประโยชน์นิติบุคคล', N'สิทธิประโยชน์นิติบุคคลอังกฤษ');


--Update pays currency
UPDATE
    [dbo].[Pays]
SET
    [dbo].[Pays].[Currency] = 
    case Memberships.MembershipRegisterTypeId   when 1 then 'THB'
                                                when 2 then 'USD'
                                                when 3 then 'THB'
                                                when 4 then 'USD'
                                                when 5 then 'THB'
                                                when 6 then 'USD'
                                                        else ''
    end
FROM
    [dbo].[Pays]
INNER JOIN
    [dbo].[Memberships]
ON
    [dbo].[Pays].MembershipId = [dbo].[Memberships].Id

--Update confirmpayments currency
UPDATE
    [dbo].[ConfirmPayments]
SET
    [dbo].[ConfirmPayments].[Currency] = 
    case [dbo].[Memberships].MembershipRegisterTypeId   when 1 then 'THB'
                                                        when 2 then 'USD'
                                                        when 3 then 'THB'
                                                        when 4 then 'USD'
                                                        when 5 then 'THB'
                                                        when 6 then 'USD'
                                                                else ''
    end
FROM
    [dbo].[ConfirmPayments]
INNER JOIN
    [dbo].[Pays]
ON
    [dbo].[ConfirmPayments].PayId = [dbo].[Pays].id
INNER JOIN
    [dbo].[Memberships]
ON
    [dbo].[Pays].MembershipId = [dbo].[Memberships].Id
