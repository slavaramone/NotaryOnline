namespace SharedLib.Filters
{
	/// <summary>
	/// Общий фильтр для постраничного вывода списков
	/// </summary>
	public class PaginationRequestFilter
	{
        /// <summary>
        /// Кол-во на странице
        /// </summary>
        public int? Take { get; set; }

        /// <summary>
        /// Кол-во пропускаемых записей
        /// </summary>
        public int? Skip { get; set; }
    }
}
