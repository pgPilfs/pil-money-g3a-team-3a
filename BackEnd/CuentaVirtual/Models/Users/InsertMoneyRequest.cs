using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CuentaVirtual.Models.Users
{
    public class InsertMoneyRequest
    {
        [Required]
        public float Monto { get; set; }

        public InsertMoneyRequest()
        {
        }
    }
}
