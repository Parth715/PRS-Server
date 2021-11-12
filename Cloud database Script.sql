insert users
(username, password, firstname, lastname, phone, email, IsReviewer, IsAdmin) Values
('Max123', 'Max123', 'Max', 'Patel', '5135082611', 'maub@gmail.com', 1, 1),
('DP1126', 'DPatel03', 'Dharuv', 'Patel', '5131239876', 'DharuvP@gmail.com', 0, 0)

insert Vendors
(code, name, address, city, state, zip, phone, email)values
('MICRO', 'Micro Center', '123 Main street', 'Milford', 'OH', '45150', null, null),
('AAPL', 'Apple', '987 Main Street', 'Loveland', 'OH', '45140', null, null),
('DEPOT', 'Home Depot', '456 Main street', 'Kenwood', 'OH', '21236', null, null)

insert Requests
(Description, Justification, RejectionReason, DeliveryMode, Status, Total, UserId) Values
('Milwaukee driver', 'Need to finish up the deck', null, 'Pick up', 'REVIEW', 199.99, (select Id from Users where firstname = 'Parth')),
('Solid State Drives', 'Building a computer', null, 'Deliver', 'REVIEW', 2039.00, (select Id from Users where firstname = 'Max')),
('IPhones', 'Buying phones for the company', null, 'Deliver', 'Approve', 2199.98, (select Id from Users where firstname = 'Dharuv')),
('Dewault kit', 'Need to finish up the roof', null, 'Pick up', 'REVIEW', 3353.00, (select Id from Users where firstname = 'Parth')),
('Another Milwaukee driver', 'needed more to finish the job on time', null, 'Deliver', 'REVIEW', 399.98, (select Id from Users where firstname = 'Dharuv'))

insert Products
(PartNbr, Name, Price, Unit, Photopath, VendorId) Values
('123', 'Motherboard', 199.99, 'Single', null, (select Id from Vendors where code = 'MICRO')),
('124', 'RAM 16 GB', 129.99, 'Single', null, (select Id from Vendors where code = 'MICRO')),
('125', '1 TB SSD', 174.99, 'Single', null, (select Id from Vendors where code = 'MICRO')),
('987', 'IPhone 13', 1099.99, 'Single', null, (select Id from Vendors where code = 'AAPL')),
('945', 'Apple Watch series 5', 499.99, 'Single', null, (select Id from Vendors where code = 'AAPL')),
('764', 'Dewault Cordless kit', 479.00, 'Kit', null, (select Id from Vendors where code = 'DEPOT')),
('489', 'Milwaukee Impact Driver', 199.99, 'Single', null, (select Id from Vendors where code = 'DEPOT'))

insert Requestlines
(RequestId, ProductId, Quantity) Values
(2, (select Id from products where partnbr = 489), 1 ),
(3, (select Id from products where partnbr = 125), 4),
(4, (select Id from products where partnbr = 987), 2),
(5, (select Id from products where partnbr = 764), 7),
(6, (select Id from products where partnbr = 489), 1)