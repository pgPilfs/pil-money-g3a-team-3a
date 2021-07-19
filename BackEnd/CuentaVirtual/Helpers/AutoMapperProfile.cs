using System;
using AutoMapper;
using CuentaVirtual.Entities;
using CuentaVirtual.Models.Users;

namespace CuentaVirtual.Helpers
{
    /* El perfil de automapper contiene la configuracion de mapeo utilizada por
     * la aplicacion, AutoMapper es un paquete disponible en Nuget que permite
     * el mapeo automatico de un tipo de clase a otro.
     * En este ejemplo, lo estamos usando para mapear entidades de usuario y 
     * algunos tipos de modelos diferentes: 
     * AuthenticateRequest
     * RegisterRequest
     * UpdateRequest
     */
    public class AutoMapperProfile : Profile 
    {
        public AutoMapperProfile()
        {
            // User -> AuthenticateResponse
            CreateMap<User, AuthenticateResponse>();

            // RegisterRequest -> User
            CreateMap<RegisterRequest, User>();

            // UpdateRequest -> User
            CreateMap<UpdateRequest, User>()
                .ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
                    {
                        // Ignora las propiedades de string null y vacio
                        if (prop == null) return false;
                        if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                        return true;
                    }
                ));

            // InsertMoneyRequest -> User
            //CreateMap<InsertMoneyRequest, User>();
        }
    }
}
