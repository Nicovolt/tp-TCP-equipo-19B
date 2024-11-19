using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Enums
    {
        public class EnumPresupuestoEstado
        {
            public const byte
                Creado = 1,
                Pagado = 2,
                Vencido = 3,
                Cancelado = 4,
                Armado = 5,
                Embalado = 6,
                Despachado = 7,
                Entregado = 8;
        }

        public class EnumPresupuestoFormaPago
        {
            public const byte
                Efectivo = 1,
                TransferenciaBancaria = 2,
                Deposito = 3,
                MercadoPago = 4;
        }
    }
}
