-- Categories

create procedure udp_InsertCategory
    @CategoryName varchar(20),
    @CategoryID int output
as
begin
    set nocount on;

    begin try

        -- Check CategoryName uniqueness
        if exists (select 1 from Categories where CategoryName = @CategoryName and IsDeleted = 0)
        begin
            raiserror('Category with this Name already exists.', 16, 1);
            return 1;
        end
        begin tran;

        insert into Categories(CategoryName)
        values (ltrim(rtrim(@CategoryName)));
        set @CategoryID = scope_identity();

        commit;
        return 0;
    end try
    begin catch
        if @@trancount > 0 rollback;
        throw;
    end catch
end