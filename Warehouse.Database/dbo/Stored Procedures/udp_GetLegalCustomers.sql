create procedure udp_GetLegalCustomers
	@LegalCustomerID int
as
begin
	set nocount on;

	select * from LegalCustomers as l

	join Customers as c on l.CustomerId = c.CustomerId
	where c.CustomerId = @LegalCustomerID and c.IsDeleted = 0;

end;
