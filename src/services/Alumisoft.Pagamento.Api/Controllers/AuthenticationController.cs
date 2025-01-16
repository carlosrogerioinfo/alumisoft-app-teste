using AutoMapper;
using Esterdigi.Core.Authorization.Services;
using Esterdigi.Core.Authorization.Settings;
using Esterdigi.Core.Lib.Constants;
using Esterdigi.Core.Lib.Controller;
using Esterdigi.Core.Lib.Response;
using Alumisoft.Pagamento.Domain.Http.Request;
using Alumisoft.Pagamento.Domain.Http.Response;
using Alumisoft.Pagamento.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Alumisoft.Pagamento.Api.Controllers
{
    [Authorize(Roles = Constants.Admin)]
    public class AuthenticationController : BaseController
    {
        private readonly ClienteApplication _service;
        private readonly IJwtService _jwtService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwtSettings;

        public AuthenticationController(ClienteApplication service, IJwtService jwtService, IConfiguration configuration, IMapper mapper)
            : base()
        {
            _service = service;
            _mapper = mapper;
            _jwtService = jwtService;
            _configuration = configuration;

            _jwtSettings = _configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();
        }

        /// <summary>
        /// Retorna as informações do(a) cliente(a) pelo email e senha.
        /// </summary>
        /// <response code="200">As informações do(a) cliente(a) que retornou com sucesso.</response>
        /// <response code="412">Ocorreu uma falha de pre-condição ou um algum erro interno.</response>
        [HttpPost, Route("login"), AllowAnonymous]
        [ProducesResponseType(typeof(BaseResponse<AuthenticationResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status412PreconditionFailed)]
        public async Task<IActionResult> Post([FromBody] AuthenticationRequest request)
        {
            var data = await _service.Handle(request);

            if (data is not null)
            {
                return await Response(new
                {
                    token = _jwtService.GenerateTokenClient(data, _jwtSettings),

                }, _service.Notifications);
            }

            return await Response(data, _service.Notifications);
        }

    }
}
