using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Analizadores;
using Proyecto1_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class GraficarTS : Instruccion
    {
        public Retornar Ejecutar(Entorno ent, string Ambito, Sintactico AST)
        {
            ent.Graficar(ent);
            return new Retornar();
        }

        public StringBuilder TraducirInstr(Entorno ent, StringBuilder str, string Ambito)
        {
            throw new NotImplementedException();
        }
    }
}
