-- Delete Data from All Tables in Correct Order (starting from child tables)
--DELETE FROM [dbo].[TicketPurchases];
--DELETE FROM [dbo].[TicketReservations];
--DELETE FROM [dbo].[TicketTypes];
--DELETE FROM [dbo].[Events];
--DELETE FROM [dbo].[Venues];
--DELETE FROM [dbo].[Customers];

-- Insert Venues (Parent Table for Events)
INSERT INTO [dbo].[Venues] (Name, Location, Capacity) VALUES
('Stadium A', '123 Main St, New York, USA', 5000),
('Stadium B', '456 Oak Ave, Madrid, Spain', 4000),
('Stadium C', '789 Pine Blvd, Tokyo, Japan', 3500),
('Theater X', '101 Maple Rd, Paris, France', 800),
('Theater Y', '202 Birch Ln, Berlin, Germany', 1200),
('Park Z', '303 Cedar Dr, Sydney, Australia', 2000),
('Arena W', '404 Elm St, São Paulo, Brazil', 6000),
('Grand Hall', '505 Elm St, London, UK', 1500),
('Sports Complex', '606 Willow St, Toronto, Canada', 7000),
('Concert Hall', '707 Rose Ave, Cape Town, South Africa', 1800);

-- Insert Events (Dependent on Venues)
INSERT INTO [dbo].[Events] (Name, Date, Description, VenueId) VALUES
('Rock Concert', '2025-05-01', 'A large rock concert with popular bands.', 61),
('Pop Concert', '2025-06-01', 'A pop concert featuring famous artists.', 62),
('Comedy Show', '2025-07-01', 'A night of stand-up comedy.', 63),
('Music Festival', '2025-08-01', 'A weekend music festival with various genres.', 64),
('Theater Play', '2025-09-01', 'A dramatic play by a local theater troupe.', 65),
('Sports Event', '2025-10-01', 'A national sports event featuring football matches.', 66),
('Jazz Night', '2025-11-01', 'A cozy night with live jazz music.', 67),
('Art Exhibition', '2025-12-01', 'An art exhibition featuring contemporary artists.', 68),
('Charity Gala', '2025-12-15', 'A charity gala to support community projects.', 69),
('Festival of Lights', '2025-12-20', 'An outdoor festival with lights and food stalls.', 70);

-- Insert TicketTypes (Dependent on Events)
INSERT INTO [dbo].[TicketTypes] (EventId, Type, Price, Capacity) VALUES
(26, 'VIP', 200.00, 500),
(26, 'General Admission', 50.00, 1000),
(28, 'VIP', 150.00, 300),
(28, 'General Admission', 30.00, 800),
(30, 'Front Row', 70.00, 100),
(30, 'Standard', 25.00, 500),
(32, 'Weekend Pass', 120.00, 1000),
(32, 'Single Day', 50.00, 2000),
(35, 'Regular', 40.00, 300),
(35, 'Premium', 60.00, 100);

-- Insert Customers (Independent Table)
INSERT INTO [dbo].[Customers] (Name, Email, CreatedAt) VALUES
('John Doe', 'johndoe@example.com', GETDATE()),
('Maria Garcia', 'mariagarcia@example.com', GETDATE()),
('Alice Johnson', 'alicej@example.com', GETDATE()),
('Hiroshi Tanaka', 'hiroshi.tanaka@example.com', GETDATE()),
('Liu Wei', 'liu.wei@example.com', GETDATE()),
('Ayesha Khan', 'ayesha.khan@example.com', GETDATE()),
('David Green', 'davidgreen@example.com', GETDATE()),
('Sophie Dubois', 'sophie.dubois@example.com', GETDATE()),
('Carlos Mendes', 'carlos.mendes@example.com', GETDATE()),
('Siti Aisyah', 'sitiaisyah@example.com', GETDATE());

-- Insert TicketReservations (Dependent on Customers and TicketTypes)
INSERT INTO [dbo].[TicketReservations] (CustomerId, TicketTypeId, Quantity, ExpirationTime, IsPurchased) VALUES
(61, 25, 2, '2025-05-01', 0),
(61, 26, 4, '2025-06-01', 0),
(62, 27, 3, '2025-07-01', 0),
(62, 28, 1, '2025-08-01', 0),
(63, 29, 6, '2025-09-01', 0),
(63, 30, 5, '2025-10-01', 0),
(64, 31, 3, '2025-11-01', 0),
(64, 32, 2, '2025-12-01', 0),
(70, 33, 4, '2025-12-15', 0),
(70, 34, 1, '2025-12-20', 0);

-- Insert TicketPurchases (Dependent on TicketReservations)
INSERT INTO [dbo].[TicketPurchases] (ReservationId, PurchaseDate, PaymentMethod) VALUES
(16, GETDATE(), 'Credit Card'),
(17, GETDATE(), 'PayPal'),
(18, GETDATE(), 'Debit Card'),
(19, GETDATE(), 'Cash'),
(20, GETDATE(), 'Credit Card'),
(21, GETDATE(), 'Bank Transfer'),
(22, GETDATE(), 'Credit Card'),
(23, GETDATE(), 'PayPal'),
(24, GETDATE(), 'Debit Card'),
(25, GETDATE(), 'Cash');


