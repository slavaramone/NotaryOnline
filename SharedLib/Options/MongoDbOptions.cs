namespace SharedLib.Options
{
	/// <summary>
	/// Опции MongoDb
	/// </summary>
	public class MongoDbOptions
	{
		public const string MongoDb = "MongoDb";

		/// <summary>
		/// Адрес сервера Монго
		/// </summary>
		public string Host { get; set; }

		/// <summary>
		/// Порт сервера Монго
		/// </summary>
		public int Port { get; set; }

		/// <summary>
		/// Пользователь Монго
		/// </summary>
		public string User { get; set; }

		/// <summary>
		/// Пароль Монго
		/// </summary>
		public string Password { get; set; }

		/// <summary>
		/// Имя базы данных
		/// </summary>
		public string DatabaseName { get; set; }

		/// <summary>
		/// Имя коллекции
		/// </summary>
		public string CollectionName { get; set; }

		/// <summary>
		/// Строка подключения
		/// </summary>
		public string ConnectionString { get; set; }
	}
}
