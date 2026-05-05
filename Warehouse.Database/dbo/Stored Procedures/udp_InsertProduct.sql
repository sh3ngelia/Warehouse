-- Products

create procedure udp_InsertProduct
    @CategoryId int,
    @Name varchar(100),
    @SKU char(20),
    @Description varchar(max),
    @ProductId int output
as
begin
    set nocount on;

    begin try
        if @CategoryId is null or @CategoryId <= 0
            raiserror('CategoryId must be a positive integer.', 16, 1);

        if @Name is null or ltrim(rtrim(@Name)) = ''
            raiserror('Product name cannot be empty.', 16, 1);

        if @SKU is null or ltrim(rtrim(@SKU)) = ''
            raiserror('SKU cannot be empty.', 16, 1);

        if not exists (select 1 from Categories where CategoryId = @CategoryId and IsDeleted = 0)
            raiserror('Category not found.', 16, 1);

        if exists (select 1 from Products where SKU = @SKU and IsDeleted = 0)
            raiserror('SKU already exists.', 16, 1);

        begin tran;

        insert into Products(CategoryId, [Name], SKU, [Description])
        values (@CategoryId, ltrim(rtrim(@Name)), @SKU, @Description);
        set @ProductId = scope_identity();

        commit;
        return 0;
    end try
    begin catch
        if @@trancount > 0 rollback;
        throw;
    end catch
end