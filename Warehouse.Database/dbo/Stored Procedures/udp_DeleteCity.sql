create procedure udp_DeleteCity
    @CityId int
as
begin
    set nocount on;

    begin try
        if @CityId is null or @CityId <= 0
            raiserror('CityId must be a positive integer.', 16, 1);

        begin tran;

        update Cities
        set IsDeleted = 1,
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