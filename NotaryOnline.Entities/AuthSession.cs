using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using SharedLib.DataAccess;
using System;

namespace NotaryOnline.Entities
{
	/// <summary>
	/// Данные для подтверждения номера тел или почты при регистрации
	/// </summary>
	[Alias("AuthSession")]
	public class AuthSession : IEntity<Guid>
	{
		/// <summary>
		/// Id
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Код регистрации
		/// </summary>
		[StringLength(6)]
		public string Code { get; set; }

		/// <summary>
		/// Дата запроса на регистрацию
		/// </summary>
		[Default(OrmLiteVariables.SystemUtc)]
		public DateTime CreationDate { get; set; }

		/// <summary>
		/// Id пользователя
		/// </summary>
		[ForeignKey(typeof(User))]
		public Guid UserId { get; set; }

		/// <summary>
		/// Пользователь
		/// </summary>
		[Reference]
		public User User { get; set; }
	}
}
