using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Abstracto
{
    interface Instruccion
    {
        Retornar Ejecutar(Entornos.Entorno ent, String Ambito);
    }
}
