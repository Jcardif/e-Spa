IF NOT EXISTS(SELECT * FROM sys.views WHERE object_id=OBJECT_ID(N'[dbo].[SalonServiceView]'))
EXEC sp_executesql @statement =N'
CREATE VIEW SalonServiceView
AS 
SELECT s.Id, s.Name AS SalonName, ss.Price, ss.Discount, se.Name AS ServiceName,se.ImageUrl, se.Description
FROM
Salon s
JOIN SalonService ss ON ss.Salon_Id=s.Id
JOIN Service se ON se.Id=ss.Salon_Id'