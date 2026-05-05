-- Regions

create procedure udp_InsertRegion
    @Name varchar(20),
    @RegionId int output
as
begin
    set nocount on;

    begin try

        -- Check Name uniqueness
        if exists (select 1 from Regions where Name = @Name and IsDeleted = 0)
        begin
            raiserror('Region with this Name already exists.', 16, 1);
            return 1;
        end

        if @Name is null or ltrim(rtrim(@Name)) = ''
            raiserror('Region name cannot be empty.', 16, 1);

        begin tran;

        insert into Regions([Name])
        values (ltrim(rtrim(@Name)));
        set @RegionId = scope_identity();

        commit;
        return 0;
    end try
    begin catch
        if @@trancount > 0 rollback;
        throw;
    end catch
end