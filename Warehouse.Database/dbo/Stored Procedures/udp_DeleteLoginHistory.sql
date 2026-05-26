create procedure udp_DeleteLoginHistory
    @LoginHistoryId int
    as
begin
    set nocount on;

begin try

if @LoginHistoryId is null
            raiserror('LoginHistoryId cannot be null.', 16, 1);

        if not exists (
            select 1
            from dbo.LoginHistory
            where LoginHistoryId = @LoginHistoryId
        )
            raiserror('LoginHistory record not found.', 16, 1);

delete from dbo.LoginHistory
where LoginHistoryId = @LoginHistoryId;

end try
begin catch
throw;
end catch
end