using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CuentaVirtual.Authorization;
using CuentaVirtual.Helpers;
using CuentaVirtual.Models.Users;
using CuentaVirtual.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

/*
 * El controlador de usuarios .NET define y maneja todas las rutas/puntos finales
 * (endpoints) para la API que se relaciona con los usuarios. 
 * Esto incluye autenticacion, registro y operaciones estandares CRUD. 
 * Dentro de cada ruta, el controlador llama al servicio del usuario para realizar
 * la accion requerida, esto permite que el controlador se mantenga "delegado" y
 * completamente separado de la base de datos/codigo de persistencia.
 * 
 * Las acciones del controlador se protegen con JWT usando el atributo [Authorize],
 * con la excepción de los methods Autenticar y Registrar que permiten el 
 * acceso público anulando el atributo [Authorize] en el controlador con los 
 * atributos [AllowAnonymous] en cada method de acción. 
 * Este enfoque fue elegido para que cualquier method agregado al controlador
 * sea seguro de forma predeterminada a menos que se haga público explícitamente.
 */
namespace CuentaVirtual.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UsersController(
            IUserService userService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register(RegisterRequest model)
        {
            _userService.Register(model);
            return Ok(new { message = "Registro exitoso" });
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userService.GetById(id);
            return Ok(user);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateRequest model)
        {
            _userService.Update(id, model);
            return Ok(new { message = "Usuario actualizado correctamente" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return Ok(new { message = "Usuario eliminado correctamente" });
        }

        [HttpPut("monto/{id}")]
        public IActionResult InsertMoney(int id, InsertMoneyRequest model)
        {
            _userService.InsertMoney(id, model);
            return Ok(new { message = "Modificacion de Monto correcta" });
        }

        [HttpPut("img/{id}")]
        public IActionResult InsertImage(int id, ImageRequest model)
        {
            _userService.InsertImage(id, model);
            return Ok(new { message = "Se inserto la imagen correctamente" });
        }

        [HttpGet("monto/pesos/{id}")]
        public IActionResult GetMoneyPesos(int id)
        {
            var user = _userService.GetById(id);
            return Ok(user.CapitalPesos);
        }

        [HttpGet("monto/dolares/{id}")]
        public IActionResult GetMoneyDolares(int id)
        {
            var user = _userService.GetById(id);
            return Ok(user.CapitalDolares);
        }

        [HttpGet("monto/criptomonedas/{id}")]
        public IActionResult GetMoneyCriptomonedas(int id)
        {
            var user = _userService.GetById(id);
            return Ok(user.CapitalCriptomonedas);
        }
    }
}
