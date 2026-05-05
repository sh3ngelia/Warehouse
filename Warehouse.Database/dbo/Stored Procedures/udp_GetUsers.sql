create procedure udp_GetUsers
    @UserId int
as
begin
    set nocount on;

    select *
    from Users
    where EmployeeId = @UserId
       and IsDeleted = 0;
   
end