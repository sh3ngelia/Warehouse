create procedure udp_ClearTestData
    as
begin
    set nocount on;

    delete from dbo.StorageDetails;
    delete from dbo.ContractDetails;
    delete from dbo.Contracts;

    delete from dbo.LoginHistory;

    delete from dbo.Storages;

    delete from dbo.PhysicalCustomers;
    delete from dbo.LegalCustomers;
    delete from dbo.Customers;

    delete from dbo.Users;
    delete from dbo.Employees;

    delete from dbo.Products;
    delete from dbo.Categories;

    delete from dbo.Permissions;
    delete from dbo.Roles;

    delete from dbo.StorageStatuses;
    delete from dbo.ContractStatuses;

    delete from dbo.Cities;
    delete from dbo.Regions;

    dbcc checkident ('dbo.StorageDetails', reseed, 0);
    dbcc checkident ('dbo.ContractDetails', reseed, 0);
    dbcc checkident ('dbo.Contracts', reseed, 0);

    dbcc checkident ('dbo.LoginHistory', reseed, 0);

    dbcc checkident ('dbo.Storages', reseed, 0);

    dbcc checkident ('dbo.Customers', reseed, 0);
    dbcc checkident ('dbo.Employees', reseed, 0);

    dbcc checkident ('dbo.Products', reseed, 0);
    dbcc checkident ('dbo.Categories', reseed, 0);

    dbcc checkident ('dbo.Permissions', reseed, 0);
    dbcc checkident ('dbo.Roles', reseed, 0);

    dbcc checkident ('dbo.StorageStatuses', reseed, 0);
    dbcc checkident ('dbo.ContractStatuses', reseed, 0);

    dbcc checkident ('dbo.Cities', reseed, 0);
    dbcc checkident ('dbo.Regions', reseed, 0);
end;