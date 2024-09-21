namespace WebApplication1.Services;

public class AdjustInventoryService : ICommandService<AdjustInventory>
{
    public void ExecuteCommand(AdjustInventory command)
    {
        var productId = command.ProductId;
    }
}

public class AuditingCommandServiceDecorator<TCommand>
(
    ICommandService<TCommand> decorated
) : ICommandService<TCommand>
{
    public void ExecuteCommand(TCommand command)
    {
        decorated.ExecuteCommand(command);
    }
}
