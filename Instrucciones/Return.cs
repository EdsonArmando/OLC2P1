using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Analizadores;
using Proyecto1_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class Return : Abstracto.Instruccion
    {
        Expresion valorReturn;
        public Return(Expresion valor)
        {
            this.valorReturn = valor;
        }
        public Retornar Ejecutar(Entorno ent, string Ambito, Sintactico AST)
        {
            Retornar retornar = new Retornar();
            retornar.isReturn = true;
            retornar.valor = valorReturn.obtenerValor(ent);
            return retornar;
        }
    }
}
