create procedure udp_GetProducts
    @ProductId int 
as
begin
    set nocount on;

    select * from Products
    where ProductId = @ProductId
       and IsDeleted = 0;

end