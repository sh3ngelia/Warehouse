create procedure udp_GetContracts
    @ContractId int 
as
begin
    set nocount on;

    select * from Contracts
    where ContractId = @ContractId;
 
end