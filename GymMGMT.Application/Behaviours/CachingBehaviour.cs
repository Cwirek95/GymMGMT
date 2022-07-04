using GymMGMT.Application.Caching;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Serilog;

namespace GymMGMT.Application.Behaviours
{
    public class CachingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
            where TRequest : IRequest<TResponse>, ICacheable
    {
        private readonly IMemoryCache _cache;
        private readonly ILogger _logger;

        public CachingBehaviour(IMemoryCache cache, ILogger logger)
        {
            _cache = cache;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var requestName = request.GetType();
            _logger.Information("{Request} is configured for caching.", requestName);

            TResponse response;
            if (_cache.TryGetValue(request.CacheKey, out response))
            {
                _logger.Information("Returning cached value for {Request}.", requestName);

                return response;
            }

            _logger.Information("{Request} Cache Key: {Key} is not inside the cache, executing request.", requestName, request.CacheKey);
            response = await next();
            _cache.Set(request.CacheKey, response);

            return response;
        }
    }
}