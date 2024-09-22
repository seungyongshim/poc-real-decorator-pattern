using MediatR;
using System.Net.NetworkInformation;

public class Ping : IRequest<string> { }

public class PingHandler : IRequestHandler<Ping, string>
{
    public Task<string> Handle(Ping request, CancellationToken cancellationToken)
    {

        throw new Exception("Ping failed");
        return Task.FromResult("Pong");
    }
}
