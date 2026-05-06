create procedure udp_GetCategories
	@CategoryId int 
as
begin
	set nocount on;
	select * from Categories where CategoryId = @CategoryId
	and IsDeleted = 0;
end
