create procedure udp_GetUsers
    @EmployeeId int
as
begin
    set nocount on;

select *
from Users
where EmployeeId = @EmployeeId
  and IsDeleted = 0;
end