using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Expresiones
{
    class Id : Abstracto.Expresion
    {
        public string id;
        public Id(string id)
        {
            this.id = id;
        }
        public override Simbolo.EnumTipoDato getTipo()
        {
            return this.tipo;
        }

        public override Expresion obtenerValor(Entornos.Entorno ent)
        {
            Simbolo simbolo = ent.obtener(id,ent);
            if (simbolo != null)
            {
                return new Literal(simbolo.getTipo(), simbolo.getValor());
            }
            return new Literal(Simbolo.EnumTipoDato.ERROR, "@Error@");
        }
    }
}
