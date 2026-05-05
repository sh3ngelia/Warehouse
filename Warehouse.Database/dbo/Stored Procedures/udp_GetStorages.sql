create procedure udp_GetStorages
    @StorageId int
as
begin
    set nocount on;
    
    select * from Storages
    where StorageId = @StorageId
      and IsDeleted = 0;
   
end