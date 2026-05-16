namespace ConsignadoPrivado.Identity.WebApi.Common;

public class ApiResponseWithDataList<T> : ApiResponse
{
    public required List<T> DataList { get; set; }
}
