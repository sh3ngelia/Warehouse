create procedure udp_DeleteStorageDetail
    @StorageDetailId int
as
begin
    set nocount on;

    begin try
        if @StorageDetailId is null or @StorageDetailId <= 0
            raiserror('StorageDetailId must be a positive integer.', 16, 1);

        if not exists (select 1 from StorageDetails where StorageDetailId = @StorageDetailId)
            raiserror('Storage detail not found.', 16, 1);

        begin tran;

        delete from StorageDetails
        where StorageDetailId = @StorageDetailId;

        commit;
        return 0;
    end try
    begin catch
        if @@trancount > 0 rollback;
        throw;
    end catch
end