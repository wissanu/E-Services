
SET QUOTED_IDENTIFIER OFF;
GO

IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- Add column
INSERT INTO [dbo].[EmailMessages] VALUES 
(10,N'�駡����觫����Թ���',N'',
N'&lt;p&gt;{fullname} ��ӡ����駫����Թ���&lt;/p&gt;

&lt;p&gt;{eComPaymentList}&lt;/p&gt;

&lt;p&gt;{paymentTotalPrice}&lt;/p&gt;

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;p&gt;-------------------------------------------------------------------------------&lt;/p&gt;

&lt;p&gt;ʶҺѹ�Ԩ����оѲ���ѭ����������ͧ��дѺ��觪ҵ� (ͧ������Ҫ�)&lt;/p&gt;',
N'',N'paweenruk@hotmail.com'),
(11,N'�駡���׹�ѹ��ê����Թ���',N'',
N'&lt;p&gt;{fullname} ��ӡ���׹�ѹ�����觫����Թ���&lt;/p&gt;

&lt;p&gt;{eComPaymentList}&lt;/p&gt;

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;p&gt;-------------------------------------------------------------------------------&lt;/p&gt;

&lt;p&gt;ʶҺѹ�Ԩ����оѲ���ѭ����������ͧ��дѺ��觪ҵ� (ͧ������Ҫ�)&lt;/p&gt;',
N'',N'paweenruk@hotmail.com'),
(12,N'�駡�ê��Ф���Թ���',N'',
N'&lt;table align=&quot;&quot; border=&quot;0&quot; cellpadding=&quot;0&quot; cellspacing=&quot;0&quot; style=&quot;width:100%&quot;&gt;
  &lt;tbody&gt;
    &lt;tr&gt;
      &lt;td&gt;&lt;img alt=&quot;&quot; src=&quot;http://eservice.git.or.th/Images/GIT_Headder_New.png&quot; style=&quot;float:right&quot; /&gt;&lt;/td&gt;
    &lt;/tr&gt;
  &lt;/tbody&gt;
&lt;/table&gt;

&lt;hr /&gt;
&lt;p&gt;&lt;strong&gt;&lt;big&gt;{fullname}&lt;/big&gt;&lt;/strong&gt; ����Ф����觫����Թ������º��������&lt;/p&gt;

&lt;p&gt;�����Ţ�����觫��� &lt;big&gt;&lt;strong&gt;{order_id}&lt;/strong&gt;&lt;/big&gt;&lt;/p&gt;

&lt;p&gt;���ҡ����觫���&lt;big&gt;&lt;strong&gt; {order_datetime}&lt;/strong&gt;&lt;/big&gt;&lt;/p&gt;

&lt;p&gt;{paymentList}&lt;/p&gt;

&lt;table align=&quot;right&quot; border=&quot;0&quot; cellpadding=&quot;0&quot; cellspacing=&quot;0&quot; style=&quot;width:100%&quot;&gt;
  &lt;tbody&gt;
    &lt;tr&gt;
      &lt;td&gt;
      &lt;table align=&quot;right&quot; border=&quot;0&quot; cellpadding=&quot;0&quot; cellspacing=&quot;0&quot;&gt;
        &lt;tbody&gt;
          &lt;tr&gt;
            &lt;td&gt;��ҨѴ��&amp;nbsp;&lt;big&gt;&lt;strong&gt;{paymentDeliverPrice}&lt;/strong&gt;&lt;/big&gt;&lt;/td&gt;
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
            &lt;td&gt;�Ҥҷ�����&amp;nbsp;&lt;big&gt;&lt;strong&gt;{paymentSumPrice}&lt;/strong&gt;&lt;/big&gt;&lt;/td&gt;
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
            &lt;td&gt;������Ť������&amp;nbsp;&lt;big&gt;&lt;strong&gt;{paymentVatPrice}&lt;/strong&gt;&lt;/big&gt;&lt;/td&gt;
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
            &lt;td&gt;�Ҥҷ���ͧ���� &lt;big&gt;&lt;strong&gt;{paymentTotalPrice}&lt;/strong&gt;&lt;/big&gt;&lt;/td&gt;
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
&lt;p&gt;ʶҺѹ�Ԩ����оѲ���ѭ����������ͧ��дѺ��觪ҵ� (ͧ������Ҫ�)&lt;/p&gt;',
N'',N'paweenruk@hotmail.com');