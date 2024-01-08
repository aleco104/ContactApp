using ContactApp.Enums;
using ContactApp.Interfaces;

namespace ContactApp.Models.Responses;

public class ServiceResult : IServiceResult
{
    public ServiceStatus Status { get; set; }
    public object Result { get; set; } = null!;
}
