create procedure udp_DeleteCategory
    @CategoryId int
as
begin
    set nocount on;

    begin try
        if @CategoryId is null or @CategoryId <= 0
            raiserror('CategoryId must be a positive integer.', 16, 1);

        begin tran;

        update Categories
        set IsDeleted = 1,
            UpdateDate = getdate()
        where CategoryId = @CategoryId
          and IsDeleted = 0;

        if @@rowcount = 0
            raiserror('Category not found.', 16, 1);

        commit;
        return 0;
    end try
    begin catch
        if @@trancount > 0 rollback;
        throw;
    end catch
end