create procedure udp_GetLoginHistories
	@LoginHistoryId int
as
begin
	set nocount on;

	select * from LoginHistory
	where LoginHistoryId = @LoginHistoryId;

end;