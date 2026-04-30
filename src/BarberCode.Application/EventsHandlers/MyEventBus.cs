using BarberCode.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BarberCode.Application.EventsHandlers;

public interface IEventBus 
{
	Task PublishAsync<T>(T ev) where T : IAppEvent;
}
public class MyEventBus : IEventBus
{
	private readonly IServiceProvider _serviceProvider;

	public MyEventBus(IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider;
	}

	public async Task PublishAsync<T>(T ev) where T : IAppEvent
	{
		var handlers = _serviceProvider.GetServices<IEventHandler<T>>();
		foreach (var handler in handlers)
			handler.HandlerAsync(ev);
	}
}
