using ConsignadoPrivado.Common;
using Microsoft.AspNetCore.Mvc;

namespace ConsignadoPrivado.Identity.WebApi.Common;

[ApiController]
public class BaseController : ControllerBase
{
    protected IActionResult OkResponse<T>(T data) =>
        Ok(new ApiResponseWithData<T> { Success = true, Message = "Operação realizada com sucesso", Data = data });

    protected IActionResult CreatedResponse<T>(T data) =>
        Created(string.Empty, new ApiResponseWithData<T> { Success = true, Message = "Recurso criado com sucesso", Data = data });

    protected IActionResult BadRequestResponse(string message) =>
        BadRequest(new ApiResponse { Success = false, Message = message });

    protected IActionResult NotFoundResponse(string message) =>
        NotFound(new ApiResponse { Success = false, Message = message });
}
