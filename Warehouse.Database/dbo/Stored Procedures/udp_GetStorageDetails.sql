create procedure udp_GetStorageDetails
	@StorageDetailId int
as
begin
	set nocount on;
	select * from StorageDetails
	where StorageDetailId = @StorageDetailId;
	
end;