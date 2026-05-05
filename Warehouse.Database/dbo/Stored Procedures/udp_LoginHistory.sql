create procedure udp_LoginHistory
    @Username   varchar(50),
    @Password   varbinary(256)
as
begin
    set nocount on;

    declare @UserId int;

    select @UserId = EmployeeId
    from Users
    where IsDeleted = 0
      and Username = @Username
      and [Password] = @Password;

    if @UserId is not null
    begin
        insert into LoginHistory (UserId)
        values (@UserId);
    end

    return 0;
end