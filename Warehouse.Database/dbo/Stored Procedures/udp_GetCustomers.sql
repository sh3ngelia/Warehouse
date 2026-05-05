create procedure udp_GetCustomers
    @CustomerId int 
as
begin
    set nocount on;

    select *
    from Customers
    where CustomerId = @CustomerId
       and IsDeleted = 0;
    
end