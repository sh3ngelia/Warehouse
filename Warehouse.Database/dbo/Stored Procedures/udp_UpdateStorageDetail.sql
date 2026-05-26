CREATE PROCEDURE udp_UpdateStorageDetail
    @StorageDetailId int,
    @ContractDetailId int,
    @ProductId int,
    @Quantity int
AS
BEGIN
    SET NOCOUNT ON;

    IF @StorageDetailId IS NULL OR @StorageDetailId <= 0
    BEGIN
        RAISERROR('StorageDetailId must be a positive integer.', 16, 1);
        RETURN 1;
    END

    IF @Quantity IS NULL OR @Quantity < 0
    BEGIN
        RAISERROR('Quantity must be greater than or equal to 0.', 16, 1);
        RETURN 1;
    END

    IF NOT EXISTS (SELECT 1 FROM ContractDetails WHERE ContractDetailId = @ContractDetailId)
    BEGIN
        RAISERROR('Referenced ContractDetail does not exist.', 16, 1);
        RETURN 1;
    END

    IF NOT EXISTS (SELECT 1 FROM Products WHERE ProductId = @ProductId AND IsDeleted = 0)
    BEGIN
        RAISERROR('Referenced Product not found or is deleted.', 16, 1);
        RETURN 1;
    END

    BEGIN TRY
        BEGIN TRAN;

        UPDATE StorageDetails
        SET ContractDetailId = @ContractDetailId,
            ProductId = @ProductId,
            Quantity = @Quantity
        WHERE StorageDetailId = @StorageDetailId;

        IF @@ROWCOUNT = 0
        BEGIN
            RAISERROR('StorageDetail record not found.', 16, 1);
        END

        COMMIT TRAN;
        RETURN 0;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRAN;
        THROW;
    END CATCH
END