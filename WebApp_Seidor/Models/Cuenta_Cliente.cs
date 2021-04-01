using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApp_Seidor.Models
{
    public class Cuenta_Cliente
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Debe ingresar el nro de DNI")]
        public string DNI { get; set; }
        public string Nombres { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Nacimiento")]
        public System.DateTime FecNacimiento { get; set; }
        public Nullable<decimal> Saldo { get; set; }
        public Nullable<int> Puntos { get; set; }
    }
}