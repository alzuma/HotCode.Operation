using System.Threading.Tasks;
using HotCode.Operation.Services.Models;

namespace HotCode.Operation.Services
{
    public interface IOperationRepository
    {
        Task SetAsync(string id, string userId, string name, OperationState state,
             string code = null, string reason = null);

        Task<OperationModel> GetAsync(string id);
    }
}