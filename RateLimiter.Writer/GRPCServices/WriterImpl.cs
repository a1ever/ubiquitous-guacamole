using Grpc.Core;

namespace RateLimiter.Writer.GRPCServices;

public class WriterImpl : Writer.WriterBase
{
    private readonly IWriterDomainService _writerDomainService;
    
    public WriterImpl(IWriterDomainService writerDomainService)
    {
        _writerDomainService = writerDomainService;
    }

    public override async Task<LimitDataReply> Create(CreateLimitRequest request, ServerCallContext context)
    {
        Limit limitToCreate = new Limit
        {
            Route = request.Route,
            Rpm = request.Rpm
        };

        User result = await _writerDomainService.CreateLimitAsync(limitToCreate);

        return new LimitDataReply { Route = result.Route, Rpm = result.Rpm };
    }

    public override async Task<LimitDataReply> Get(GetLimitRequest request, ServerCallContext context)
    {
        User result = await _writerDomainService.GetLimitAsync(request.Route);

        return new LimitDataReply { Route = result.Route, Rpm = result.Rpm };
    }

    public override async Task<SuccessReply> Update(UpdateLimitRequest request, ServerCallContext context)
    {
        Limit limitToUpdate = new Limit
        {
            Route = request.Route,
            Rpm = request.Rpm
        };
        
        bool result = await _writerDomainService.UpdateLimitAsync(limitToUpdate);

        return new SuccessReply { Response = result };
    }

    public override async Task<SuccessReply> Delete(DeleteLimitRequest request, ServerCallContext context)
    {
        bool result = await _writerDomainService.DeleteLimitAsync(request.Route);

        return new SuccessReply { Response = result };
    }
}