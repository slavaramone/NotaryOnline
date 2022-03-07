using System.ComponentModel;

namespace NotaryOnline.Api.ServiceModel
{
	/// <summary>
	/// Статус заказа
	/// </summary>
	public enum OrderStatus
	{
		/// <summary>
		/// Новый
		/// </summary>
		[Description("Новый")]
		New = 1,

		/// <summary>
		/// Перевод
		/// </summary>
		[Description("Перевод")]
		Translation,

		/// <summary>
		/// Заверение
		/// </summary>
		[Description("Заверение")]
		Assurance,

		/// <summary>
		/// Требует уточнения
		/// </summary>
		[Description("Требует уточнения")]
		NeedsClarification,

		/// <summary>
		/// Готово
		/// </summary>
		[Description("Готово")]
		Completed
	}
}
