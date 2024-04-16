using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Bissness.Validators
{
    internal class ValidatorsPersona
    {
        public string ValidarNombre(string Pn, string Sn, string Pa, string Sa,int? sueldo)
        {
            if (Pn == null || Pn.Length > 50)
                return "Primer Nombre vacío o excede los 50 caracteres";

            if (Sn != null && Sn.Length > 50)
                return "Segundo Nombre excede los 50 caracteres";

            if (Pa == null || Pa.Length > 50)
                return "Primer Apellido vacío o excede los 50 caracteres";

            if (Sa != null && Sa.Length > 50)
                return "Segundo Apellido excede los 50 caracteres";

            if (Pn == null)
                return "Primer Nombre vacio";
            
            if (Sn == null)
                return "Segundo Nombre vacio";


            if (Pa == null)
                return "Primer Apellido vacio";


            if (Sa == null)
                return "Segundo apellido vacio";


            if (Pn == null)
                return "Primer Nombre vacio";

            bool containNumPn = ValidStringNumber(Pn);
            if (containNumPn)
                return "El primer nombre contiene numeros";

            bool containNumSn = ValidStringNumber(Sn);
            if (containNumSn)
                return "El segundo nombre contiene numeros";

            bool containNumPa = ValidStringNumber(Pa);
            if (containNumPa)
                return "El primer apellido contiene numeros";

            bool containNumSa = ValidStringNumber(Sa);
            if (containNumSa)
                return "El segundo apellido contiene numeros";

            if (sueldo == 0)
                return "El sueldo debe ser diferente de 0";


            return "ok";

        }

        public bool ValidStringNumber(string var)
        {
            foreach (char caracter in var)
            {
                if (char.IsDigit(caracter))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
