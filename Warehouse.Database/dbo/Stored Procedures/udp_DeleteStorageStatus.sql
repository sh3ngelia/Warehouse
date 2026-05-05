create procedure udp_DeleteStorageStatus
    @StorageStatusId int
as
begin
    set nocount on;

    begin try
        if @StorageStatusId is null or @StorageStatusId <= 0
            raiserror('StorageStatusId must be a positive integer.', 16, 1);

        begin tran;

        update StorageStatuses
        set IsDeleted = 1,
            UpdateDate = getdate()
        where StorageStatusId = @StorageStatusId
          and IsDeleted = 0;

        if @@rowcount = 0
            raiserror('Storage status not found.', 16, 1);

        commit;
        return 0;
    end try
    begin catch
        if @@trancount > 0 rollback;
        throw;
    end catch
end