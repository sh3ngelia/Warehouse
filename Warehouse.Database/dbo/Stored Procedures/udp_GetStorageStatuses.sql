create procedure udp_GetStorageStatuses
	@StorageStatusId int
as
begin
	set nocount on;
	select * from StorageStatuses
	where StorageStatusId = @StorageStatusId
	and IsDeleted = 0;

end;