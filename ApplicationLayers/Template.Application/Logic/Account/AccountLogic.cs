using AutoMapper;
using Template.Application.Auth;
using Template.Application.Handlers.Account;
using Template.Application.Models.Account;
using Template.Application.Models.Usuarios;
using Template.Domain.AppSettings;
using Template.Domain.Core;
using Template.Domain.Entities;
using Template.Domain.Exceptions;

namespace Template.Application.Logic.Account
{
    public class AccountLogic
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AccountLogic(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        /////////////////////////////////////////

        public async Task<UsuarioModel?> GetUsuarioById(int id)
        {
            try
            {
                Usuario? db = await _unitOfWork.Usuarios.GetByIdAsync(id);
                UsuarioModel? result = _mapper.Map<UsuarioModel?>(db);
                return await Task.FromResult(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TokenInfo?> LoginUsuario(string userNameOrEmail, string password, JWT? jwt)
        {
            try
            {
                bool acceso = await VerificarAcceso(userNameOrEmail, password);
                if (acceso)
                {
                    Usuario? usuario = await _unitOfWork.Usuarios.GetFirstOrDefaultAsync(u => ((u.UserName != null && u.UserName.Equals(userNameOrEmail))
                                                                   || (u.Email != null && u.Email.Equals(userNameOrEmail))));

                    int? usuarioId = usuario?.UsuarioId;

                    // Obtengo roles del usuario
                    List<string>? roles = new List<string>() { "Administrador" };

                    // Obtengo las preferencias
                    Preferencias? preferencias = new Preferencias() { UsaTemaOscuro = true, MaximoMensajes = 13 };

                    // Creo Token nuevo
                    return JwtUtils.GenerarToken(usuarioId, usuario?.UserName, usuario?.Email, roles, preferencias, jwt);
                }
                else
                {
                    throw new UnAuthorizedException("Usuario o password incorrectos.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> RegistrarUsuario(RegisterUsuario request)
        {
            try
            {
                // convertimos a hash la password antes de guardar en la base de datos
                PasswordGenerator passwordGenerator = new PasswordGenerator();
                request.Password = passwordGenerator.EncriptarPassword(request.Password);
                Usuario db = _mapper.Map<Usuario>(request);
                return await _unitOfWork.Usuarios.AddAsync(db);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> VerificarAcceso(string userNameOrEmail, string password)
        {
            try
            {
                // convertir a hash la password antes de comparar en la base de datos
                PasswordGenerator passwordGenerator = new PasswordGenerator();
                password = passwordGenerator.EncriptarPassword(password);

                bool result = false;
                Usuario? db = await _unitOfWork.Usuarios.GetFirstOrDefaultAsync(u => (((u.Email != null && u.Email.Equals(userNameOrEmail))
                                                            || (u.UserName != null && u.UserName.Equals(userNameOrEmail))) && u.Password.Equals(password)));
                result = (db != null);
                return await Task.FromResult(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> ExisteUserName(string? userName)
        {
            try
            {
                bool result = true;
                if (userName != null)
                {
                    Usuario? db = await _unitOfWork.Usuarios.GetFirstOrDefaultAsync(u => u.UserName != null && u.UserName.Equals(userName));
                    result = (db != null);
                }
                return await Task.FromResult(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> ExisteEmail(string? email)
        {
            try
            {
                bool result = true;
                if (email != null)
                {
                    Usuario? db = await _unitOfWork.Usuarios.GetFirstOrDefaultAsync(u => u.Email != null && u.Email.Equals(email));
                    result = (db != null);
                }
                return await Task.FromResult(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /////////////////////////////////////////
    }
}
