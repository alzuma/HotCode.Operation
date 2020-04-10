using System.Threading.Tasks;
using HotCode.Operation.Services;
using HotCode.Operation.Services.Models;
using HotCode.System;
using HotCode.System.Messaging.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotCode.Operation.Controllers
{
    [ApiVersion("1.0")]
    public class OperationsController : BaseController
    {
        private readonly IOperationRepository _operationRepository;

        public OperationsController(IBusPublisher busPublisher, IOperationRepository operationRepository) : base(
            busPublisher)
        {
            _operationRepository = operationRepository;
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<OperationModel>> Get(string id)
            => Single(await _operationRepository.GetAsync(id));
    }
}