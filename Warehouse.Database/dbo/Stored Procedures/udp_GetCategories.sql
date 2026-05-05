create procedure udp_GetCategories
	@CategoryID int 
as
begin
	set nocount on;
	select * from Categories where CategoryID = @CategoryID
	and IsDeleted = 0;
end
