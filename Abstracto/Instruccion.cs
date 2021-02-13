using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Abstracto
{
    abstract class Instruccion
    {
        abstract public Retornar ejectuar(Entornos.Entorno ent);
    }
}
