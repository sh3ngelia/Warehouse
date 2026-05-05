-- Users

create procedure udp_UpdateUser
    @EmployeeId int,
    @NewUsername varchar(40)
as
begin
    set nocount on;

    begin try
        if @EmployeeId is null or @EmployeeId <= 0
            raiserror('EmployeeId must be a positive integer.', 16, 1);

        if @NewUsername is null or ltrim(rtrim(@NewUsername)) = ''
            raiserror('Username cannot be empty.', 16, 1);

        if not exists (select 1 from Users where EmployeeId = @EmployeeId and IsDeleted = 0)
            raiserror('User not found.', 16, 1);

        -- Check Username uniqueness (excluding current record)
        if exists (select 1 from Users where Username = @NewUsername and EmployeeId != @EmployeeId and IsDeleted = 0)
        begin
            raiserror('User with this Username already exists.', 16, 1);
            return 1;
        end

        begin tran;

        update Users
        set Username = ltrim(rtrim(@EmployeeId)),
            UpdateDate = getdate()
        where EmployeeId = @EmployeeId
          and IsDeleted = 0;

        if @@rowcount = 0
            raiserror('User not found.', 16, 1);

        commit;
        return 0;
    end try
    begin catch
        if @@trancount > 0 rollback;
        throw;
    end catch
end