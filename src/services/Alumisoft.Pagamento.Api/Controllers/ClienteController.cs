using Alumisoft.Pagamento.Domain.Http.Request;
using Alumisoft.Pagamento.Domain.Http.Response;
using Alumisoft.Pagamento.Service;
using Esterdigi.Core.Db.Domain.Model;
using Esterdigi.Core.Lib.Controller;
using Esterdigi.Core.Lib.Helpers;
using Esterdigi.Core.Lib.Response;
using Microsoft.AspNetCore.Mvc;

namespace Alumisoft.Pagamento.Api.Controllers
{
    public class ClienteController : BaseController
    {
        private readonly ClienteApplication _service;

        public ClienteController(ClienteApplication service)
        {
            _service = service;
        }


        /// <summary>
        /// Retorna todos os clientes cadastrados paginação e filtros
        /// </summary>
        /// <response code="200">Registro que foi retornado com sucesso.</response>
        /// <response code="412">Ocorreu uma falha de pre-condição ou um algum erro interno.</response>
        [HttpGet, Route("api/clientes/get")]
        [ProducesResponseType(typeof(PagedResponse<ClienteResponse, PagedResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status412PreconditionFailed)]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter paginationFilter, [FromQuery] ClienteFilter filter)
        {
            var data = await _service.GetAllByFilter(paginationFilter, filter);

            if (_service.Notifications.Any()) return BadRequest(BaseErrorResponse.Create(_service.Notifications));

            data.Success = !_service.Notifications.Any();
            return Ok(data);
        }

        /// <summary>
        /// Insere um registro da tabela cliente
        /// </summary>
        /// <response code="200">Registro que foi inserido com sucesso.</response>
        /// <response code="412">Ocorreu uma falha de pre-condição ou um algum erro interno.</response>
        [HttpPost, Route("api/clientes/add")]
        [ProducesResponseType(typeof(BaseResponse<ClienteResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status412PreconditionFailed)]
        public async Task<IActionResult> Post([FromBody] ClienteRegisterRequest request)
        {
            var data = await _service.Handle(request);
            return await Response(data, _service.Notifications);
        }

        /// <summary>
        /// Altera um registro da tabela cliente
        /// </summary>
        /// <response code="200">Registro que foi alterado com sucesso.</response>
        /// <response code="412">Ocorreu uma falha de pre-condição ou um algum erro interno.</response>
        [HttpPut, Route("api/clientes/update")]
        [ProducesResponseType(typeof(BaseResponse<ClienteResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status412PreconditionFailed)]
        public async Task<IActionResult> Put([FromBody] ClienteUpdateRequest request)
        {
            var data = await _service.Handle(request);
            return await Response(data, _service.Notifications);
        }

        /// <summary>
        /// Deleta um registro da tabela cliente (Exclusão lógica, apenas inativa o cliente)
        /// </summary>
        /// <response code="200">Registro que foi deletado com sucesso.</response>
        /// <response code="412">Ocorreu uma falha de pre-condição ou um algum erro interno.</response>
        [HttpDelete, Route("api/clientes/delete")]
        [ProducesResponseType(typeof(BaseResponse<ClienteResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status412PreconditionFailed)]
        public async Task<IActionResult> Delete()
        {
            var data = await _service.Delete(Helper.GetIdFromToken(Request.Headers["Authorization"]));
            return await Response(data, _service.Notifications);
        }


    }
}