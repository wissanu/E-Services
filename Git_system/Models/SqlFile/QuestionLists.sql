-- raramuridesign.com
-- 2012-02-06
-- use this to import a list of countries into your database
------------------------------------------------------------

SET QUOTED_IDENTIFIER OFF;

GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

INSERT INTO [dbo].[QuestionLists] VALUES (N'ชื่อโรงเรียนของคุณ ?', N'What is the name of your school?'),
(N'ชื่อทีมโปรดของคุณ ?', N'What is your favorite team?'),
(N'ชื่อกลางของแม่คุณ ?', N'What is your mother''s maiden name?'),
(N'ชื่อสัตว์เลี้ยงของคุณ ?', N'What is the name of your pet?'),
(N'ชื่อฮีโร่ในวัยเด็กของคุณ ?', N'Who was your childhood hero?'),
(N'ชื่อเมืองที่คุณเกิด ?', N'What city were you born in?'),
(N'ชื่อเล่นของคุณในสมัยเด็กคืออะไร ?', N'What was your childhood nickname ?'),
(N'นามสกุลของเจ้านายคุณคืออะไร ?', N'What was the last name of your first boss ?'),
(N'ชื่อของแฟนที่คุณ เคยมีจูบครั้งที่สอง มีชื่อว่าอะไร ?', N'What was the name of the boy/girl you had your second kiss with?'),
(N'ชื่อของสัตว์เลี้ยงตัวที่สองที่คุณเคยเลี้ยง ชื่อว่าอะไร ?', N'What was the name of your second pet?'),
(N'คุณครูที่คุณชื่นชอบ นามสกุลอะไร ?', N'What was the last name of your favorite teacher?'),
(N'เมื่อสมัยตอนคุณยังเด็ก คุณฝันว่าคุณอยากจะเป็นอะไรเมื่อโตขึ้น ?', N'When you ware young, what did you want to be when you grew up?'),
(N'ถ้าหากคุณสามารถที่จะเปลี่ยนชื่อของตัวเองได้  คุณอยากจะเปลี่ยนเป็นชื่ออะไร ?', N'If you could change your name, what would it be?');
