using AutoMapper;
using Google.Apis.Auth;
using IMS.Auth.BLL.Authentication;
using IMS.Auth.Domain.Consts;
using IMS.Auth.Domain.Entities;
using IMS.Auth.IBLL.Services;
using IMS.Auth.IDAL.Repositories;
using IMS.Auth.Models.Dto.Incoming;
using IMS.Auth.Models.Dto.Outgoing;
using IMS.Auth.Models.Exceptions;
using IMS.Shared.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace IMS.Auth.BLL.Services;

public class GoogleAuthService : IGoogleAuthService
{
    private readonly ILogger<GoogleAuthService> _logger;
    private readonly IUserRepository _userRepository;
    private readonly IProviderRepository _providerRepository;
    private readonly GoogleAuthenticationConfiguration _googleAuthenticationConfiguration;
    private readonly IMapper _mapper;
    private readonly IJwtService _jwtService;

    public GoogleAuthService(
        IUserRepository userRepository,
        ILogger<GoogleAuthService> logger,
        IMapper mapper,
        IProviderRepository providerRepository, 
        GoogleAuthenticationConfiguration googleAuthenticationConfiguration, 
        IJwtService jwtService)
    {
        _logger = logger;
        _mapper = mapper;
        _providerRepository = providerRepository;
        _userRepository = userRepository;
        _googleAuthenticationConfiguration = googleAuthenticationConfiguration;
        _jwtService = jwtService;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="authenticateWithGoogleProviderDto"></param>
    /// <returns></returns>
    public async Task<UserInfoDto> SignIn(AuthenticateWithGoogleProviderDto authenticateWithGoogleProviderDto)
    {
        var requestTime = DateTime.UtcNow;
        var payload = await ValidateGooglePayload(authenticateWithGoogleProviderDto.Token);
        
        var user = await GetUserFromProvider(payload, authenticateWithGoogleProviderDto);
        var userInfoDto = _mapper.Map<UserInfoDto>(user);
        userInfoDto.Token = _jwtService.GenerateJwtToken(user);

        _logger.LogInformation("User successfully logged in with email {Email}",userInfoDto.Email);
        return userInfoDto;
    }
    
    public async Task<bool> AddGoogleProvider(AuthenticateWithGoogleProviderDto authenticateWithGoogleProviderDto, string userIdString)
    {
        var requestTime = DateTime.UtcNow;

        if (userIdString is null) throw new InvalidUserGuidStringException();
        if (!Guid.TryParse(userIdString, out var userId)) throw new UserGuidStringEmptyException();
        
        await ValidateGooglePayload(authenticateWithGoogleProviderDto.Token);

        var user = await _userRepository.GetByIdAsync(userId);

        if (user is null) throw new UserNotFoundException();

        if (user.Providers is null)
        {
            user.Providers = new [] {CreateNewProvider(authenticateWithGoogleProviderDto.UserId)};
        }
        else
        {
            var oldProvider = user.Providers.FirstOrDefault(x => x.AuthKey == authenticateWithGoogleProviderDto.UserId);

            if (oldProvider is not null)
            {
                throw new Exception();
            }

            user.Providers = user.Providers.Append(CreateNewProvider(authenticateWithGoogleProviderDto.UserId));
        }
        
        await _userRepository.Save();

        return true;
    }

    private async Task<User> GetUserFromProvider(GoogleJsonWebSignature.Payload payload, AuthenticateWithGoogleProviderDto authenticateWithGoogleProviderDto)
    {
        var provider = await _providerRepository.GetByTokenWithUserAsync(
            Providers.Google, authenticateWithGoogleProviderDto.Token);
        if (provider is null)
        {
            return await CheckIfEmailTaken(payload, authenticateWithGoogleProviderDto.UserId);
        }
        return CheckIfActiveUser(provider);
    }

    private async Task<GoogleJsonWebSignature.Payload> ValidateGooglePayload(string idToken)
    {
        var googleSettings = new GoogleJsonWebSignature.ValidationSettings
        {
            Audience = new List<string>() { _googleAuthenticationConfiguration.ClientId },
        };

        var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, googleSettings);
        
        if (payload is null)
            throw new IncorrectGoogleTokenException();

        return payload;
    }

    private async Task<User> CheckIfEmailTaken(GoogleJsonWebSignature.Payload payload, string providerKey)
    {
        var tempUser = await _userRepository.GetByEmailAsync(payload.Email);
        if (tempUser is not null)
            throw new UserWithEmailAlreadyExistsException(payload.Email);
        return await CreateNewUserFromPayload(payload, providerKey);
    }

    private static User CheckIfActiveUser(Provider provider)
    {
        if (provider.User.ConfirmedAccount is not true)
            throw new UserNotFoundException(provider.User.Email);
        return provider.User;
    }
    
    private async Task<User> CreateNewUserFromPayload(GoogleJsonWebSignature.Payload payload, string providerKey)
    {
        var activationCode = Guid.NewGuid().ToString();

        var newProvider = CreateNewProvider(providerKey);
        
        var user = new User
        {
            Email = payload.Email,
            Nickname = $"{payload.GivenName} {payload.FamilyName}",
            ConfirmedAccount = false,
            ActivationToken = activationCode,
            Role = Roles.User ,
            Providers = new List<Provider> {newProvider}
        };

        await _userRepository.Insert(user);
        await _userRepository.Save();
        
        return user;
    }

    private static Provider CreateNewProvider(string providerKey)
    {
        return new Provider
        {
            AuthKey = providerKey,
            Name = Providers.Google,
        };
    }
}