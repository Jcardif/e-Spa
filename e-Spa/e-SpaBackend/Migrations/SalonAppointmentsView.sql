IF NOT EXISTS(SELECT * FROM sys.views WHERE object_id=OBJECT_ID(N'[dbo].[SalonAppointments]'))
EXEC dbo.sp_executesql @statement =N'
CREATE VIEW [dbo].[SalonAppointments]
AS
SELECT  s.ID, s.Name AS SalonName, ap.Date, ap.Venue, ss.Price, ss.Discount, se.Name AS serviceName, c.FirstName, c.LastName, c.PhoneNo, c.Email
FROM
Salon s
JOIN Appointment ap ON  ap.Salon_Id=s.Id
JOIN SalonService ss ON ap.SalonService_Id=ss.Id
JOIN Service se ON se.Id=ss.Service_Id
JOIN Client c ON c.Id=ap.Client_Id'
