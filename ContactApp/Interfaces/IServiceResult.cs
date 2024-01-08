using ContactApp.Enums;

namespace ContactApp.Interfaces;

public interface IServiceResult
{
    object Result { get; set; }
    ServiceStatus Status { get; set; }
}