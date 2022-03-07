using AutoMapper;
using Microsoft.Extensions.Logging;

namespace SharedLib.Templates
{
	public abstract class ConsumerTemplate<T>
	{
		protected readonly ILogger<T> _logger;
		protected readonly IMapper _mapper;

		public ConsumerTemplate(ILogger<T> logger, IMapper mapper)
		{
			_logger = logger;
			_mapper = mapper;
		}
	}
}
