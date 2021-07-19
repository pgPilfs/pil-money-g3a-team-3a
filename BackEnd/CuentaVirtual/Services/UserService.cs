using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CuentaVirtual.Authorization;
using CuentaVirtual.Entities;
using CuentaVirtual.Helpers;
using CuentaVirtual.Models.Users;
using BCryptNet = BCrypt.Net.BCrypt;

namespace CuentaVirtual.Services
{
    /* El servicio de usuario es responsable de toda la interaccion con la base
     * de datos y la logica relacionada con la autenticacion, el registro y la 
     * gestion de usuarios (Operaciones CRUD).
     * 
     * La parte superior del archivo contiene una interfaz que define el 
     * servicio de usuario, justo debajo está la clase de servicio de usuario 
     * concreta que implementa la interfaz. 
     * BCrypt se utiliza para hacer hash y verificar contraseñas.
     */

    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        User GetById(int id);
        void Register(RegisterRequest model);
        void Update(int id, UpdateRequest model);
        void Delete(int id);
        void InsertMoney(int id, InsertMoneyRequest model);
        void InsertImage(int id, ImageRequest model);
    }
    public class UserService : IUserService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public UserService(
            DataContext context,
            IJwtUtils jwtUtils,
            IMapper mapper)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }

        //Valicacion de login
        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _context.Users.SingleOrDefault(x => x.Username == model.Username);

            // Valida
            if (user == null || !BCryptNet.Verify(model.Password, user.PasswordHash))
                throw new AppException("Usuario o contraseña incorrecta");

            // Autenticacion exitosa
            var response = _mapper.Map<AuthenticateResponse>(user);
            response.JwtToken = _jwtUtils.GenerateToken(user);
            return response;
        }

        //Obtiene all usuarios
        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        //Obtiene usuario con un ID
        public User GetById(int id)
        {
            return getUser(id);
        }

        //Registro de usuario
        public void Register(RegisterRequest model)
        {
            // Valida
            if (_context.Users.Any(x => x.Username == model.Username))
                throw new AppException("El usuario '" + model.Username + "' ya existe");

            // Mapeo de modelo a nuevo objeto user
            var user = _mapper.Map<User>(model);

            // Hash de password
            user.PasswordHash = BCryptNet.HashPassword(model.Password);

            // Guarda usuario
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        //Actualizacion de usuario
        public void Update(int id, UpdateRequest model)
        {
            var user = getUser(id);

            // Valida parametros
            if (model.Username != user.Username && _context.Users.Any(x => x.Username == model.Username))
                throw new AppException("El usuario '" + model.Username + "' ya existe");

            // Hash de contraseña si fue ingresada
            if (!string.IsNullOrEmpty(model.Password))
                user.PasswordHash = BCryptNet.HashPassword(model.Password);

            // Copia el modelo al usuario y lo guarda
            _mapper.Map(model, user);
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        //Eliminacion de usuario
        public void Delete(int id)
        {
            var user = getUser(id);
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        //Incremento de monto en cuenta de un usuario
        public void InsertMoney(int id, InsertMoneyRequest model)
        {
            //Obtengo el ID del usuario a modificar el monto
            var user = _context.Users.SingleOrDefault(x => x.Id == id);

            user.CapitalPesos = user.CapitalPesos + model.Monto;

            _context.Users.Update(user);
            _context.SaveChanges();
        }

        //Inserta imagen para el usuario
        public void InsertImage(int id, ImageRequest model) 
        {
            //Obtengo el ID del usuario a modificar el monto
            var user = _context.Users.SingleOrDefault(x => x.Id == id);

            user.ImgDoc1 = model.Img1;
            user.ImgDoc2 = model.Img2;

            _context.Users.Update(user);
            _context.SaveChanges();
        }

        //Methods de ayuda
        private User getUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) throw new KeyNotFoundException("Usuario no encontrado");
            return user;
        }
    }
}
