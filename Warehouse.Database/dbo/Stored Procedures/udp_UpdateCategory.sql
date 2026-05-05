create procedure udp_UpdateCategory
    @CategoryId int,
    @CategoryName varchar(20)
as
begin
    set nocount on;

    begin try
        if @CategoryId is null or @CategoryId <= 0
            raiserror('CategoryId must be a positive integer.', 16, 1);
        if @CategoryName is null or ltrim(rtrim(@CategoryName)) = ''
            raiserror('Category name cannot be empty.', 16, 1);
        if not exists (select 1 from Categories where CategoryId = @CategoryId and IsDeleted = 0)
        begin
            raiserror('Category with the given ID does not exist.', 16, 1);
            return 1;
        end

        -- Check CategoryName uniqueness (excluding current record)
        if exists (select 1 from Categories where CategoryName = @CategoryName and CategoryId != @CategoryId and IsDeleted = 0)
        begin
            raiserror('Category with this Name already exists.', 16, 1);
            return 1;
        end

        begin tran;

        update Categories
        set CategoryName = ltrim(rtrim(@CategoryName)),
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