using GreenPipes.Filters;
using MassTransit;
using System.Threading.Tasks;

namespace SharedLib.Filters
{
	public static class LatestFilter<T> where T : class
	{
		static ILatestFilter<ConsumeContext<T>> _latestFilter;

		/// <summary>
		/// Получает последний контекст, прошедший полностью через канал (иными словами последнее успешно обработанное сообщение и его контекст).
		/// </summary>
		public static Task<ConsumeContext<T>> Context => _latestFilter.Latest;

		public static void SetLatest(ILatestFilter<ConsumeContext<T>> latestFilter) => _latestFilter = latestFilter;
	}
}
