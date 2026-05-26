create procedure udp_InsertLoginHistory
    @UserId int,
    @LoginHistoryId int output
    as
begin
    set nocount on;

begin try

        if @UserId is null
            raiserror('UserId cannot be null.', 16, 1);

        if not exists (
            select 1
            from dbo.Users
            where EmployeeId = @UserId
        )
            raiserror('User does not exist.', 16, 1);

begin tran;

insert into dbo.LoginHistory (UserId)
values (@UserId);

set @LoginHistoryId = scope_identity();

commit;
end try
begin catch
if @@trancount > 0 rollback;
        throw;
end catch
end