using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Expresiones
{
    class AccesoObject : Abstracto.Expresion
    {
        Expresion izquierdo;
        Expresion derecho;
        public AccesoObject(Expresion izquierdo,Expresion derecho) {
            this.izquierdo = izquierdo;
            this.derecho = derecho;
        }
        public override Simbolo.EnumTipoDato getTipo()
        {
            throw new NotImplementedException();
        }

        public override Expresion obtenerValor(Entorno ent)
        {         
            Expresion izqu = izquierdo.obtenerValor(ent);

            Instrucciones.Type_Object temp = new Instrucciones.Type_Object("",null);
            if (izqu.valor.GetType() == temp.GetType()) {
                temp = (Instrucciones.Type_Object)izqu.valor;
                Expresion valorDerecho = derecho.obtenerValor(temp.entObjeto);
                if (valorDerecho.valor.GetType() == temp.GetType())
                {
                    return valorDerecho;
                }
                else { 
                    return valorDerecho;
                }
            }
            return null;
        }
    }
}
