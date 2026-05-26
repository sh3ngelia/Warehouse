create procedure udp_GetLoginHistory
    @LoginHistoryId int
    as
begin
    set nocount on;

begin try

if @LoginHistoryId is null
            raiserror('LoginHistoryId cannot be null.', 16, 1);

select
    LoginHistoryId,
    UserId,
    LoginAt
from dbo.LoginHistory
where LoginHistoryId = @LoginHistoryId;

end try
begin catch
throw;
end catch
end