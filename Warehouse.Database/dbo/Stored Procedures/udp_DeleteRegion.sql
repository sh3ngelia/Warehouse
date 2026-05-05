create procedure udp_DeleteRegion
    @RegionId int
as
begin
    set nocount on;

    begin try
        if @RegionId is null or @RegionId <= 0
            raiserror('RegionId must be a positive integer.', 16, 1);

        begin tran;

        update Regions
        set IsDeleted = 1,
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