create procedure udp_UpdateStorageStatus
    @StorageStatusId int,
    @Name varchar(15)
as
begin
    set nocount on;

    begin try
        if @StorageStatusId is null or @StorageStatusId <= 0
            raiserror('StorageStatusId must be a positive integer.', 16, 1);

        if @Name is null or ltrim(rtrim(@Name)) = ''
            raiserror('Storage status name cannot be empty.', 16, 1);

        -- Check Name uniqueness (excluding current record)
        if exists (select 1 from StorageStatuses where Name = @Name and StorageStatusId != @StorageStatusId and IsDeleted = 0)
        begin
            raiserror('Storage status with this Name already exists.', 16, 1);
            return 1;
        end

        begin tran;

        update StorageStatuses
        set [Name] = ltrim(rtrim(@Name)),
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