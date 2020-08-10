
SET QUOTED_IDENTIFIER OFF;
GO

IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- Add column
INSERT INTO [dbo].[EmailMessages] VALUES 
(10,N'แจ้งการสั่งซื้อสินค้า',N'',
N'&lt;p&gt;{fullname} ได้ทำการสั้งซื้อสินค้า&lt;/p&gt;

&lt;p&gt;{eComPaymentList}&lt;/p&gt;

&lt;p&gt;{paymentTotalPrice}&lt;/p&gt;

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;p&gt;-------------------------------------------------------------------------------&lt;/p&gt;

&lt;p&gt;สถาบันวิจัยและพัฒนาอัญมณีและเครื่องประดับแห่งชาติ (องค์การมหาชน)&lt;/p&gt;',
N'',N'paweenruk@hotmail.com'),
(11,N'แจ้งการยืนยันการชำระสินค้า',N'',
N'&lt;p&gt;{fullname} ได้ทำการยืนยันการสั่งซื้อสินค้า&lt;/p&gt;

&lt;p&gt;{eComPaymentList}&lt;/p&gt;

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;p&gt;-------------------------------------------------------------------------------&lt;/p&gt;

&lt;p&gt;สถาบันวิจัยและพัฒนาอัญมณีและเครื่องประดับแห่งชาติ (องค์การมหาชน)&lt;/p&gt;',
N'',N'paweenruk@hotmail.com'),
(12,N'แจ้งการชำระค่าสินค้า',N'',
N'&lt;table align=&quot;&quot; border=&quot;0&quot; cellpadding=&quot;0&quot; cellspacing=&quot;0&quot; style=&quot;width:100%&quot;&gt;
  &lt;tbody&gt;
    &lt;tr&gt;
      &lt;td&gt;&lt;img alt=&quot;&quot; src=&quot;http://eservice.git.or.th/Images/GIT_Headder_New.png&quot; style=&quot;float:right&quot; /&gt;&lt;/td&gt;
    &lt;/tr&gt;
  &lt;/tbody&gt;
&lt;/table&gt;

&lt;hr /&gt;
&lt;p&gt;&lt;strong&gt;&lt;big&gt;{fullname}&lt;/big&gt;&lt;/strong&gt; ได้ชำระค่าสั่งซื้อสินค้าเรียบร้อยแล้ว&lt;/p&gt;

&lt;p&gt;หมายเลขการสั่งซื้อ &lt;big&gt;&lt;strong&gt;{order_id}&lt;/strong&gt;&lt;/big&gt;&lt;/p&gt;

&lt;p&gt;เวลาการสั่งซื้อ&lt;big&gt;&lt;strong&gt; {order_datetime}&lt;/strong&gt;&lt;/big&gt;&lt;/p&gt;

&lt;p&gt;{paymentList}&lt;/p&gt;

&lt;table align=&quot;right&quot; border=&quot;0&quot; cellpadding=&quot;0&quot; cellspacing=&quot;0&quot; style=&quot;width:100%&quot;&gt;
  &lt;tbody&gt;
    &lt;tr&gt;
      &lt;td&gt;
      &lt;table align=&quot;right&quot; border=&quot;0&quot; cellpadding=&quot;0&quot; cellspacing=&quot;0&quot;&gt;
        &lt;tbody&gt;
          &lt;tr&gt;
            &lt;td&gt;ค่าจัดส่ง&amp;nbsp;&lt;big&gt;&lt;strong&gt;{paymentDeliverPrice}&lt;/strong&gt;&lt;/big&gt;&lt;/td&gt;
          &lt;/tr&gt;
        &lt;/tbody&gt;
      &lt;/table&gt;
      &lt;/td&gt;
    &lt;/tr&gt;
    &lt;tr&gt;
      &lt;td&gt;
      &lt;table align=&quot;right&quot; border=&quot;0&quot; cellpadding=&quot;0&quot; cellspacing=&quot;0&quot;&gt;
        &lt;tbody&gt;
          &lt;tr&gt;
            &lt;td&gt;ราคาทั้งหมด&amp;nbsp;&lt;big&gt;&lt;strong&gt;{paymentSumPrice}&lt;/strong&gt;&lt;/big&gt;&lt;/td&gt;
          &lt;/tr&gt;
        &lt;/tbody&gt;
      &lt;/table&gt;
      &lt;/td&gt;
    &lt;/tr&gt;
    &lt;tr&gt;
      &lt;td&gt;
      &lt;table align=&quot;right&quot; border=&quot;0&quot; cellpadding=&quot;0&quot; cellspacing=&quot;0&quot;&gt;
        &lt;tbody&gt;
          &lt;tr&gt;
            &lt;td&gt;ภาษีมูลค่าเพิ่ม&amp;nbsp;&lt;big&gt;&lt;strong&gt;{paymentVatPrice}&lt;/strong&gt;&lt;/big&gt;&lt;/td&gt;
          &lt;/tr&gt;
        &lt;/tbody&gt;
      &lt;/table&gt;
      &lt;/td&gt;
    &lt;/tr&gt;
    &lt;tr&gt;
      &lt;td&gt;
      &lt;table align=&quot;right&quot; border=&quot;0&quot; cellpadding=&quot;0&quot; cellspacing=&quot;0&quot;&gt;
        &lt;tbody&gt;
          &lt;tr&gt;
            &lt;td&gt;{order_promotiom_list}&lt;/td&gt;
          &lt;/tr&gt;
        &lt;/tbody&gt;
      &lt;/table&gt;
      &lt;/td&gt;
    &lt;/tr&gt;
    &lt;tr&gt;
      &lt;td&gt;
      &lt;table align=&quot;right&quot; border=&quot;0&quot; cellpadding=&quot;0&quot; cellspacing=&quot;0&quot;&gt;
        &lt;tbody&gt;
          &lt;tr&gt;
            &lt;td&gt;ราคาที่ต้องชำระ &lt;big&gt;&lt;strong&gt;{paymentTotalPrice}&lt;/strong&gt;&lt;/big&gt;&lt;/td&gt;
          &lt;/tr&gt;
        &lt;/tbody&gt;
      &lt;/table&gt;
      &lt;/td&gt;
    &lt;/tr&gt;
  &lt;/tbody&gt;
&lt;/table&gt;

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;hr /&gt;
&lt;p&gt;สถาบันวิจัยและพัฒนาอัญมณีและเครื่องประดับแห่งชาติ (องค์การมหาชน)&lt;/p&gt;',
N'',N'paweenruk@hotmail.com');