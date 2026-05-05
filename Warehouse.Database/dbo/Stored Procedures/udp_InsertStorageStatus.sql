-- StorageStatuses

create procedure udp_InsertStorageStatus
    @Name varchar(15),
    @StorageStatusId int output
as
begin
    set nocount on;

    begin try
        if @Name is null or ltrim(rtrim(@Name)) = ''
            raiserror('Storage status name cannot be empty.', 16, 1);

        -- Check Name uniqueness
        if exists (select 1 from StorageStatuses where Name = @Name and IsDeleted = 0)
        begin
            raiserror('Storage status with this Name already exists.', 16, 1);
            return 1;
        end

        begin tran;

        insert into StorageStatuses([Name])
        values (ltrim(rtrim(@Name)));
        set @StorageStatusId = scope_identity();

        commit;
        return 0;
    end try
    begin catch
        if @@trancount > 0 rollback;
        throw;
    end catch
end