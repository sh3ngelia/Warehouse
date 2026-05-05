create procedure udp_DeleteStorage
    @StorageId int
as
begin
    set nocount on;

    begin try
        if @StorageId is null or @StorageId <= 0
            raiserror('StorageId must be a positive integer.', 16, 1);

        begin tran;

        update Storages
        set IsDeleted = 1,
            UpdateDate = getdate()
        where StorageId = @StorageId
          and IsDeleted = 0;

        if @@rowcount = 0
            raiserror('Storage not found.', 16, 1);

        commit;
        return 0;
    end try
    begin catch
        if @@trancount > 0 rollback;
        throw;
    end catch
end