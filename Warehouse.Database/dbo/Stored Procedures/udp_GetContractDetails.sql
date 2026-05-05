create procedure udp_GetContractDetails
	@ContractDetailId int

as
begin
	set nocount on;

	select * from ContractDetails
	where ContractDetailId = @ContractDetailId;

end