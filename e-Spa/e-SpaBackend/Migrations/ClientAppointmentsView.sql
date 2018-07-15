IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id=OBJECT_ID(N'[dbo].[ClientAppointments]'))
EXEC dbo.sp_executesql @statement =N'
CREATE VIEW [dbo].[ClientAppointments]
AS
SELECT c.ID,c.FirstName, c.LastName,ap.Date, ap.Venue, ss.Price,ss.Discount,s.Name AS SalonName, se.Name AS ServiceName, se.ImageUrl, sm.PhoneNumber, sm.Email, sm.FirstName AS SMFirstName, sm.LastName AS SMLastName
FROM [dbo].Client   c
JOIN [dbo].Appointment ap ON ap.Client_Id=c.Id
JOIN [dbo].SalonService ss ON ss.Id=ap.SalonService_Id
JOIN [dbo].Salon s ON s.Id=ss.Salon_Id
JOIN [dbo].Service se ON se.Id=ss.Service_Id
JOIN [dbo].SalonManager sm ON s.SalonManager_Id=sm.Id' 
