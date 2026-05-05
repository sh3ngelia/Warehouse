create procedure udp_UpdateStorage
    @StorageId int,
    @Status int,
    @CityId int,
    @Name varchar(20),
    @Description nvarchar(max),
    @Capacity float,
    @Address nvarchar(250),
    @Price money
as
begin
    set nocount on;

    -- Check Name uniqueness (excluding current record)
    if exists (select 1 from Storages where Name = @Name and StorageId != @StorageId and IsDeleted = 0)
    begin
        raiserror('Storage with this Name already exists.', 16, 1);
        return 1;
    end

    -- Check Address uniqueness (excluding current record)
    if exists (select 1 from Storages where Address = @Address and StorageId != @StorageId and IsDeleted = 0)
    begin
        raiserror('Storage with this Address already exists.', 16, 1);
        return 1;
    end

    begin try
        if @StorageId is null or @StorageId <= 0
            raiserror('StorageId must be a positive integer.', 16, 1);

        if @Status is null or @Status <= 0
            raiserror('Status must be a positive integer.', 16, 1);

        if @CityId is null or @CityId <= 0
            raiserror('CityId must be a positive integer.', 16, 1);

        if @Name is null or ltrim(rtrim(@Name)) = ''
            raiserror('Storage name cannot be empty.', 16, 1);

        if @Capacity is null or @Capacity < 0
            raiserror('Capacity must be >= 0.', 16, 1);

        if @Price is null or @Price < 0
            raiserror('Price must be >= 0.', 16, 1);

        if not exists (select 1 from StorageStatuses where StorageStatusId = @Status and IsDeleted = 0)
            raiserror('Storage status not found.', 16, 1);

        if not exists (select 1 from Cities where CityId = @CityId and IsDeleted = 0)
            raiserror('City not found.', 16, 1);

        begin tran;

        update Storages
        set [Status] = @Status,
            CityId = @CityId,
            [Name] = ltrim(rtrim(@Name)),
            [Description] = @Description,
            Capacity = @Capacity,
            [Address] = @Address,
            Price = @Price,
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