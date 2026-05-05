create procedure udp_UpdateRegion
    @RegionId int,
    @Name varchar(20)
as
begin
    set nocount on;

    -- Check Name uniqueness (excluding current record)
    if exists (select 1 from Regions where Name = @Name and RegionId != @RegionId and IsDeleted = 0)
    begin
        raiserror('Region with this Name already exists.', 16, 1);
        return 1;
    end

    begin try
        if @RegionId is null or @RegionId <= 0
            raiserror('RegionId must be a positive integer.', 16, 1);

        if @Name is null or ltrim(rtrim(@Name)) = ''
            raiserror('Region name cannot be empty.', 16, 1);

        begin tran;

        update Regions
        set [Name] = ltrim(rtrim(@Name)),
            UpdateDate = getdate()
        where RegionId = @RegionId
          and IsDeleted = 0;

        if @@rowcount = 0
            raiserror('Region not found.', 16, 1);

        commit;
        return 0;
    end try
    begin catch
        if @@trancount > 0 rollback;
        throw;
    end catch
end