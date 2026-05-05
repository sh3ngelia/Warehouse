-- Cities

create procedure udp_InsertCity
    @Name varchar(20),
    @RegionId int,
    @CityId int output
    as
begin
    set nocount on;

begin try
if exists (select 1 from Cities where Name = @Name and IsDeleted = 0)
begin
            raiserror('City with this Name already exists in this Region.', 16, 1);
return 1;
end

        if @Name is null or ltrim(rtrim(@Name)) = ''
            raiserror('City name cannot be empty.', 16, 1);

        if @RegionId is null
            raiserror('RegionId cannot be null.', 16, 1);

begin tran;

insert into Cities(RegionId, [Name])
values (@RegionId, ltrim(rtrim(@Name)));

set @CityId = scope_identity();

commit;
return 0;
end try
begin catch
if @@trancount > 0 rollback;
        throw;
end catch
end