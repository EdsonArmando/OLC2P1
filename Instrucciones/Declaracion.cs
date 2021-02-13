using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class Declaracion : Abstracto.Instruccion
    {
        string nombreVariable;
        Abstracto.Expresion valor;
        Entornos.Simbolo.EnumTipoDato tipoVariable;
        LinkedList<Declaracion> lista;
        Instruccion llamadaFuncion;
        public Declaracion(Simbolo.EnumTipoDato tipo, string nombre, Abstracto.Expresion expresion)
        {
            this.tipoVariable = tipo;
            this.nombreVariable = nombre;
            this.valor = expresion;
        }
        public override Retornar ejectuar(Entornos.Entorno ent)
        {
            throw new NotImplementedException();
        }
    }
}
