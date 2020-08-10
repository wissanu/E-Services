INSERT INTO [dbo].[TranslateMaps] VALUES 
(N'รวมค่าจัดส่งแล้ว'), (N'ยังไม่รวมค่าจัดส่ง'), (N'ไม่มีค่าจัดส่ง');
GO

INSERT INTO [dbo].[Translates] VALUES 
(4, N'th', N'รวมค่าจัดส่งแล้ว'),
(5, N'th', N'ยังไม่รวมค่าจัดส่ง'),
(6, N'th', N'ไม่มีค่าจัดส่ง');
GO

INSERT INTO [dbo].[DeliverTypes] VALUES 
(4), (5), (6);
GO
