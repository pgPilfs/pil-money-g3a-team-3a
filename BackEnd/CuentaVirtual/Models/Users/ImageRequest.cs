using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CuentaVirtual.Models.Users
{
    public class ImageRequest
    {
        [Required]
        public byte[] Img1 { get; set; }
        [Required]
        public byte[] Img2 { get; set; }
        public ImageRequest(){}
    }
}
