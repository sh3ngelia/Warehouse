create procedure udp_GetEmployees
    @EmployeeId int
as
begin
    set nocount on;

    select *
    from Employees
    where EmployeeId = @EmployeeId
       and IsDeleted = 0;
   
end