namespace Products.API.Bus;

public interface IBusService
{
    Task Publish<T>(T message, CancellationToken cancellationToken);
}
