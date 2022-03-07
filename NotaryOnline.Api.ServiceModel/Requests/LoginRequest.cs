using ServiceStack;

namespace NotaryOnline.Api.ServiceModel
{
	/// <summary>
	/// Запрос аутентификация пользователя
	/// </summary>
	[Route("/auth/user")]
    public class LoginRequest : IReturn<LoginResponse>
    {
        /// <summary>
        /// Эл.почта
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }
    }
}
