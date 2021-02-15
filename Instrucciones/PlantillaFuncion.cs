using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    abstract class PlantillaFuncion : Instruccion
    {
        public Retornar Ejecutar(Entorno ent, String ambito)
        {
            throw new NotImplementedException();
        }
    }
}
