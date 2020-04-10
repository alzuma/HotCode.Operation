using System;
using System.Threading.Tasks;
using HotCode.Operation.Services.Models;
using HotCode.System;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;

namespace HotCode.Operation.Services.Defaults
{
    [ServiceLocator.Service(ServiceLifetime.Scoped)]
    public class OperationRepository : IOperationRepository
    {
        private readonly IDistributedCache _cache;

        public OperationRepository(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task SetAsync(string id, string userId, string name, OperationState state, string code = null, string reason = null)
        {
            var newState = state.ToString().ToLowerInvariant();
            var operation = await GetAsync(id);
            operation ??= new OperationModel();
            operation.Id = id;
            operation.UserId = userId;
            operation.Name = name;
            operation.State = newState;
            operation.Code = code ?? string.Empty;
            operation.Reason = reason ?? string.Empty;

            await _cache.SetStringAsync(id, operation.ToJson(),
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                    SlidingExpiration = TimeSpan.FromMinutes(1)
                });
        }

        public async Task<OperationModel> GetAsync(string id)
        {
            var operation = await _cache.GetStringAsync(id);
            return string.IsNullOrWhiteSpace(operation)
                ? null
                : operation.FromJson<OperationModel>();
        }
    }
}