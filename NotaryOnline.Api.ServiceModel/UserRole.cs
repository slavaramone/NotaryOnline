using System.ComponentModel;

namespace NotaryOnline.Api.ServiceModel
{
	/// <summary>
	/// Роли пользователей
	/// </summary>
	public enum UserRole
	{
		/// <summary>
		/// Гражданин
		/// </summary>
		[Description("Помощник нотариуса")]
		NotaryAssistant = 1,

		/// <summary>
		/// Переводчик
		/// </summary>
		[Description("Переводчик")]
		Translator,

		/// <summary>
		/// Переводчик
		/// </summary>
		[Description("Админ")]
		Admin,

		/// <summary>
		/// Переводчик
		/// </summary>
		[Description("Клиент")]
		Customer
	}
}
