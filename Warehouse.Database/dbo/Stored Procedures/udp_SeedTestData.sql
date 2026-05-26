create procedure udp_SeedTestData
as
begin
    set nocount on;

    insert into dbo.Regions (Name) values
        ('Georgia'),
        ('Greece'),
        ('USA'),
        ('France');

    insert into dbo.Cities (Name, RegionId) values
        ('Tbilisi', 1),
        ('Kutaisi', 1),
        ('Batumi', 1),
        ('Athens', 2),
        ('Thessaloniki', 2),
        ('Patras', 2),
        ('New York', 3),
        ('Los Angeles', 3),
        ('Chicago', 3),
        ('Paris', 4),
        ('Lyon', 4),
        ('Marseille', 4);

    insert into dbo.Categories (CategoryName) values
        ('Electronics'),
        ('Furniture'),
        ('Food'),
        ('Clothes'),
        ('Tools');

    insert into dbo.ContractStatuses (Name) values
        ('Pending'),
        ('Active'),
        ('Completed'),
        ('Cancelled'),
        ('Suspended');

    insert into dbo.StorageStatuses (Name) values
        ('Available'),
        ('Occupied'),
        ('Repair'),
        ('Reserved'),
        ('Closed');

    insert into dbo.Roles (Name) values
        ('Admin'),
        ('Manager'),
        ('Employee'),
        ('Operator'),
        ('Viewer');

    insert into dbo.Permissions (Name, PermissionKey, Description) values
        ('Create Contract', 1, 'Allows creating contracts'),
        ('Edit Contract', 2, 'Allows editing contracts'),
        ('Delete Contract', 3, 'Allows deleting contracts'),
        ('View Reports', 4, 'Allows viewing reports'),
        ('Manage Users', 5, 'Allows managing users');

    insert into dbo.Products (CategoryId, Name, SKU, Description) values
        (1, 'Laptop',      'SKU-LAP-000000000001', 'Business laptop'),
        (1, 'Monitor',     'SKU-MON-000000000002', '24 inch monitor'),
        (2, 'Office Desk', 'SKU-DSK-000000000003', 'Wooden desk'),
        (3, 'Coffee Pack', 'SKU-COF-000000000004', 'Premium coffee'),
        (4, 'Jacket',      'SKU-JAC-000000000005', 'Winter jacket');

-- Step 1: Create Employees first
    insert into dbo.Employees (PersonalId, FirstName, LastName, Phone, Email) values
        ('12345678901', 'Giorgi', 'Beridze',    '555000000001', 'giorgi@test.com'),
        ('12345678902', 'Nino',   'Kapanadze',  '555000000002', 'nino@test.com'),
        ('12345678903', 'Luka',   'Gelashvili', '555000000003', 'luka@test.com'),
        ('12345678904', 'Ana',    'Maisuradze', '555000000004', 'ana@test.com'),
        ('12345678905', 'Dato',   'Kiknadze',   '555000000005', 'dato@test.com');

-- Step 2: Create Users referencing Employees
    insert into dbo.Users (EmployeeId, Username, Password) values
        (1, 'admin',     HASHBYTES('SHA2_256', 'Kx9#mP2q')),
        (2, 'manager',   HASHBYTES('SHA2_256', 'Tz5$nL8w')),
        (3, 'employee1', HASHBYTES('SHA2_256', 'Qr3@jN6v')),
        (4, 'employee2', HASHBYTES('SHA2_256', 'Wy7!hM4b')),
        (5, 'viewer',    HASHBYTES('SHA2_256', 'Xp1%gK9c'));

    insert into dbo.Customers (CustomerType, Phone, Email) values
        (0, '599111111111', 'phys1@test.com'),
        (0, '599111111112', 'phys2@test.com'),
        (1, '599111111113', 'legal1@test.com'),
        (1, '599111111114', 'legal2@test.com'),
        (0, '599111111115', 'phys3@test.com');

    insert into dbo.PhysicalCustomers (CustomerId, FirstName, LastName, PersonalId) values
        (1, 'Giga',   'Tsertsvadze', '11111111111'),
        (2, 'Maka',   'Chikovani',   '22222222222'),
        (5, 'Irakli', 'Japaridze',   '33333333333');

    insert into dbo.LegalCustomers (CustomerId, Name, Address) values
        (3, 'TechCorp',     'Tbilisi, Georgia'),
        (4, 'LogisticsPro', 'Athens, Greece');

    insert into dbo.Storages (Status, CityId, Name, Description, Capacity, Address, Price) values
        (1, 1, 'Tbilisi Central',  N'Large warehouse in Tbilisi', 500, N'Tbilisi, Saburtalo 10',   120),
        (2, 2, 'Kutaisi West',     N'Medium warehouse in Kutaisi', 300, N'Kutaisi, Rustaveli 5',   90),
        (1, 3, 'Batumi Port',      N'Storage near port',           450, N'Batumi, Port Street 12', 110),
        (3, 4, 'Athens Main',      N'Warehouse in Athens',         600, N'Athens, Central Ave 22', 150),
        (1, 7, 'New York East',    N'Urban storage in NY',         700, N'New York, Madison 45',   200);

    insert into dbo.Contracts (CustomerId, EmployeeId, ContractStatus) values
        (1, 1, 2),
        (2, 2, 1),
        (3, 3, 2),
        (4, 4, 3),
        (5, 5, 1);

    insert into dbo.ContractDetails (ContractId, StorageId, Price) values
        (1, 1, 500),
        (2, 2, 350),
        (3, 3, 450),
        (4, 4, 800),
        (5, 5, 650);

    insert into dbo.StorageDetails (ContractDetailId, ProductId, Quantity) values
        (1, 1, 10),
        (2, 2, 15),
        (3, 3, 20),
        (4, 4, 50),
        (5, 5, 12);
end;