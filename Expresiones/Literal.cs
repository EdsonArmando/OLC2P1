using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Expresiones
{
    class Literal : Abstracto.Expresion
    {
        public Literal(Simbolo.EnumTipoDato tipo, Object valor)
        {
            this.tipo = tipo;
            this.valor = valor;
        }
        public override Simbolo.EnumTipoDato getTipo()
        {
            throw new NotImplementedException();
        }

        public override Expresion obtenerValor(Entornos.Entorno ent)
        {
            return this;
        }
    }
}
