create procedure udp_GetCities
    @CityId int
as
begin
    set nocount on;

    select * from Cities
    where CityId = @CityId
      and IsDeleted = 0;

end