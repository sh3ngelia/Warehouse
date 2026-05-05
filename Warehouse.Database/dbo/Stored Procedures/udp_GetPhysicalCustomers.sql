create procedure udp_GetPhysicalCustomers
	@PhysicalCustomerID int
as
begin
	set nocount on;

	select * from PhysicalCustomers as p

	join Customers as c on p.CustomerId = c.CustomerId
	where c.CustomerId = @PhysicalCustomerID and c.IsDeleted = 0;

end;
