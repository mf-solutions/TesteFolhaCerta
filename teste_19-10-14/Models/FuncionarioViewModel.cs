using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace teste_19_10_14.Models
{
    public class FuncionarioViewModel
    {
        public string TipoAcao { get; set; }
        public int Id { get; set; }
        public string NOME { get; set; }
        public Nullable<decimal> ID_CARGO { get; set; }
        public string DESC_CARGO { get; set; }
        public System.DateTime DATA_ENTRADA { get; set; }
        public Nullable<decimal> SALARIO { get; set; }

    }
}