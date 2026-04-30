
namespace BarberCode.Application.Interfaces;

public interface IAppEvent {}

public interface IEventHandler<T> where T : IAppEvent 
{
	Task HandlerAsync(T appEvent);
}
