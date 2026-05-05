create procedure udp_UserLogin
    @Username   varchar(50),
    @Password   varchar(30),
    @IsValid    bit output
as
begin
    set nocount on;

    if @Username is null or ltrim(rtrim(@Username)) = ''
        raiserror('Username cannot be empty.', 16, 1);

    if @Password is null or datalength(@Password) = 0
        raiserror('PasswordHash cannot be empty.', 16, 1);

    -- Check Username uniqueness (excluding current record)
    if exists (select 1 from Users where Username = @Username and IsDeleted = 0)
    begin
        raiserror('User with this Username already exists.', 16, 1);
        return 1;
    end

    declare @PasswordHash varbinary(256);
    set @PasswordHash = HASHBYTES('SHA2_256', @Password);

    if exists (
        select 1
        from Users
        where IsDeleted = 0
          and Username = @Username
          and [Password] = @PasswordHash
    )
    begin
        set @IsValid = 1;
    end
    else
    begin
        set @IsValid = 0;
    end

    return 0;
end