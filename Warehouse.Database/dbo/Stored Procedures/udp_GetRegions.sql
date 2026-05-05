create procedure udp_GetRegions
    @RegionId int 
as
begin
    set nocount on;

    select * from Regions
    where RegionId = @RegionId
       and IsDeleted = 0;

end