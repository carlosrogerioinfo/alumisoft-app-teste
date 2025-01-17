using Alumisoft.Pagamento.Domain.Http.Request;
using Alumisoft.Pagamento.Domain.Http.Response;
using Alumisoft.Pagamento.Service;
using Esterdigi.Core.Db.Domain.Model;
using Esterdigi.Core.Lib.Controller;
using Esterdigi.Core.Lib.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Alumisoft.Pagamento.Api.Controllers
{
    public class PagamentoClienteController : BaseController
    {
        private readonly PagamentoClienteApplication _service;

        public PagamentoClienteController(PagamentoClienteApplication service)
        {
            _service = service;
        }


        /// <summary>
        /// Retorna todos os pagamentos pode filtrar pelo cliente id retorna paginação
        /// </summary>
        /// <response code="200">Registro que foi retornado com sucesso.</response>
        /// <response code="412">Ocorreu uma falha de pre-condição ou um algum erro interno.</response>
        [HttpGet, Route("api/pagamentos/get")]
        [ProducesResponseType(typeof(PagedResponse<PagamentoClienteResponse, PagedResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status412PreconditionFailed)]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter paginationFilter, [FromQuery] PagamentoClienteFilter filter)
        {
            var data = await _service.GetAllByFilter(paginationFilter, filter);

            if (_service.Notifications.Any()) return BadRequest(BaseErrorResponse.Create(_service.Notifications));

            data.Success = !_service.Notifications.Any();
            return Ok(data);
        }

        /// <summary>
        /// Insere um registro da tabela pagamento cliente
        /// </summary>
        /// <response code="200">Registro que foi inserido com sucesso.</response>
        /// <response code="412">Ocorreu uma falha de pre-condição ou um algum erro interno.</response>
        [HttpPost, Route("api/pagamentos")]
        [ProducesResponseType(typeof(BaseResponse<PagamentoClienteResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status412PreconditionFailed)]
        public async Task<IActionResult> Post([FromBody] PagamentoClienteRegisterRequest request)
        {
            var data = await _service.Handle(request);
            return await Response(data, _service.Notifications);
        }

        /// <summary>
        /// Altera um registro da tabela pagamento cliente
        /// </summary>
        /// <response code="200">Registro que foi alterado com sucesso.</response>
        /// <response code="412">Ocorreu uma falha de pre-condição ou um algum erro interno.</response>
        [HttpPut, Route("api/pagamentos/{id}/status"), AllowAnonymous]
        [ProducesResponseType(typeof(BaseResponse<PagamentoClienteResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status412PreconditionFailed)]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] PagamentoClienteUpdateStatusRequest request)
        {
            var data = await _service.AlterarStatus(request, id);
            return await Response(data, _service.Notifications);
        }


    }
}