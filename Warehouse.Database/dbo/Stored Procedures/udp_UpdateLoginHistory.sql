create procedure udp_UpdateLoginHistory
    @LoginHistoryId int,
    @UserId int,
    @LogoutAt datetime
as
begin
    set nocount on;

begin try
        if @LoginHistoryId is null or @LoginHistoryId <= 0
            raiserror('LoginHistoryId must be a positive integer.', 16, 1);

        if @UserId is null or @UserId <= 0
            raiserror('UserId must be a positive integer.', 16, 1);

        if @LogoutAt is null
            raiserror('LogoutDate cannot be null.', 16, 1);

        if not exists (select 1 from Users where EmployeeId = @UserId and IsDeleted = 0)
            raiserror('User not found.', 16, 1);

begin tran;

update LoginHistory
set LogoutAt = @LogoutAt
where LoginHistoryId = @LoginHistoryId
  and UserId = @UserId;

if @@rowcount = 0
            raiserror('Login history record not found.', 16, 1);

commit;
return 0;
end try
begin catch
if @@trancount > 0 rollback;
        throw;
end catch
end
