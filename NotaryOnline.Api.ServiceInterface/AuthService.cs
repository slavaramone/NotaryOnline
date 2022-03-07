using AutoMapper;
using NotaryOnline.Api.ServiceModel;
using NotaryOnline.DataAccess.Interfaces;
using NotaryOnline.Entities;
using ServiceStack;
using SharedLib;
using SharedLib.Exceptions;
using SharedLib.Options;
using SharedLib.Security;
using SharedLib.Utils;
using System;
using System.Threading.Tasks;

namespace NotaryOnline.Api.ServiceInterface
{
	/// <summary>
	/// API сервис авторизации
	/// </summary>
	public class AuthService : Service
	{
		private readonly IUserRepository _userRepo;
		private readonly IMapper _mapper;
		private readonly IAuthSessionRepository _sessionRepo;
		private readonly AuthOptions _authOptions;
		private readonly IJwtAuthFactory _jwtAuthFactory;

		public AuthService(Microsoft.Extensions.Options.IOptions<AuthOptions> authOptions, IUserRepository userRepo, IMapper mapper, IAuthSessionRepository sessionRepo, IJwtAuthFactory jwtAuthFactory)
		{
			_userRepo = userRepo ?? throw new ArgumentNullException();
			_mapper = mapper ?? throw new ArgumentNullException();
			_sessionRepo = sessionRepo ?? throw new ArgumentNullException();
			_jwtAuthFactory = jwtAuthFactory ?? throw new ArgumentNullException();
			_authOptions = authOptions.Value ?? throw new ArgumentNullException();
		}

		/// <summary>
		/// Запрос данных пользователя по токену и паролю
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public async Task<object> Post(LoginRequest request)
		{
			var user = await _userRepo.Get(request.Email, request.Password);
			if (user is null)
			{
				throw new AuthenticationException();
			}

			var response = _mapper.Map<LoginResponse>(user);
			return response;
		}

		/// <summary>
		/// Запрос токена авторизации пользователя
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public async Task<object> Post(CodeRequest request)
		{
			var user = await _userRepo.Get(request.Phone);
			if (user is null)
			{
				user = _mapper.Map<User>(request);
				await _userRepo.Add(user);
			}

			var code = CodeGenerator.GetRandomNumberString(Constants.RegistrationCodeLength);
			var session = new AuthSession
			{
				Id = Guid.NewGuid(),
				Code = code,
				UserId = user.Id
			};
			Guid sessionId = await _sessionRepo.Add(session);

			var response = new CodeResponse
			{
				SessionId = sessionId
			};
			return response;
		}

		/// <summary>
		/// Запрос токена авторизации пользователя
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public async Task<object> Post(TokenRequest request)
		{
			var session = await _sessionRepo.Get(request.SessionId, request.Code);
			if (session == null)
			{
				throw new NotFoundException(request.SessionId.ToString());
			}

			var user = await _userRepo.Get(session.UserId);

			if (user is null)
			{
				throw new AuthenticationException();
			}

			string token = _jwtAuthFactory.Create(_authOptions.Key, user.Id, user.Name, user.Role.ToString());

			var response = new TokenResponse
			{
				UserId = user.Id,
				UserRole = user.Role,
				Token = token,
				ExpirationDateTime = DateTime.Now + _authOptions.Lifetime.Value
			};
			return response;
		}
	}
}
