-- Procedures

create procedure udp_InsertPermission
    @Name           varchar(30),
    @PermissionKey  smallint,
    @Description    varchar(max),
    @PermissionId   int output
as
begin
    set nocount on; 

    if @Name is null or ltrim(rtrim(@Name)) = ''
            raiserror('Role name cannot be empty.', 16, 1); 

    if @PermissionKey is null or ltrim(rtrim(@PermissionKey)) = ''
            raiserror('Role name cannot be empty.', 16, 1); 
    
    insert into Permissions(Name, PermissionKey, Description)
    values(@Name, @PermissionKey, @Description);
    set @PermissionId = scope_identity();

    return 0;
end