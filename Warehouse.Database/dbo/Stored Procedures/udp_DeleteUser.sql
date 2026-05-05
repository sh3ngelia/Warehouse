create procedure udp_DeleteUser
    @EmployeeId int
as
begin
    set nocount on;

    begin try
        if @EmployeeId is null or @EmployeeId <= 0
            raiserror('EmployeeId must be a positive integer.', 16, 1);

        begin tran;

        update Users
        set IsDeleted = 1,
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