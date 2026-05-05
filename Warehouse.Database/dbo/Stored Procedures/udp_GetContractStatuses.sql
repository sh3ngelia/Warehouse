create procedure udp_GetContractStatuses
	@ContractStatusId int
as
begin

	set nocount on;
	select * from ContractStatuses
	where ContractStatusId = @ContractStatusId
	and IsDeleted=0	;

end;