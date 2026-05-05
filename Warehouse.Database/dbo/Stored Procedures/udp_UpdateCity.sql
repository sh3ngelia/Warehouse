create procedure udp_UpdateCity
    @CityId int,
    @RegionId int,
    @Name varchar(20)
as
begin
    set nocount on;

    begin try
    
        -- Check Name uniqueness within the same region (excluding current record)
        if exists (select 1 from Cities where Name = @Name and RegionId = @RegionId and CityId != @CityId and IsDeleted = 0)
        begin
            raiserror('City with this Name already exists in this Region.', 16, 1);
            return 1;
        end
    
        if @CityId is null or @CityId <= 0
            raiserror('CityId must be a positive integer.', 16, 1);

        if @RegionId is null or @RegionId <= 0
            raiserror('RegionId must be a positive integer.', 16, 1);

        if @Name is null or ltrim(rtrim(@Name)) = ''
            raiserror('City name cannot be empty.', 16, 1);

        if not exists (select 1 from Regions where RegionId = @RegionId and IsDeleted = 0)
            raiserror('Region does not exist.', 16, 1);

        begin tran;

        update Cities
        set RegionId = @RegionId,
            [Name] = ltrim(rtrim(@Name)),
            UpdateDate = getdate()
        where CityId = @CityId
          and IsDeleted = 0;

        if @@rowcount = 0
            raiserror('City not found.', 16, 1);

        commit;
        return 0;
    end try
    begin catch
        if @@trancount > 0 rollback;
        throw;
    end catch
end