create procedure udp_UserLogin
    @Username   varchar(50),
    @Password   varchar(30)
as
begin
    set nocount on;

    if @Username is null or ltrim(rtrim(@Username)) = ''
        raiserror('Username cannot be empty.', 16, 1);

    if @Password is null or datalength(@Password) = 0
        raiserror('PasswordHash cannot be empty.', 16, 1);

    select u.EmployeeId, u.Username
    from Users u
    where u.Username = @Username
      and u.Password = HASHBYTES('SHA2_256', @Password)
      and u.IsDeleted = 0;

    return 0;
end