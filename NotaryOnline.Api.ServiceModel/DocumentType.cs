using ServiceStack.DataAnnotations;

namespace NotaryOnline.Api.ServiceModel
{
	/// <summary>
	/// Тип документа
	/// </summary>
	public enum DocumentType
	{
		/// <summary>
		/// Готово
		/// </summary>
		[Description("Не определен")]
		Unknown = 0,

		/// <summary>
		/// Новый
		/// </summary>
		[Description("Пасспорт")]
		Passport = 1,
	}
}
