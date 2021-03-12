using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Analizadores;
using Proyecto1_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class Return : Instruccion
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
        public StringBuilder TraducirInstr(Entorno ent, StringBuilder str, string Ambito)
        {
            StringBuilder temp = new StringBuilder();
            str.Append("exit(" + valorReturn.Traducir(ent, temp) + ");");
            return str;
        }
    }
}
