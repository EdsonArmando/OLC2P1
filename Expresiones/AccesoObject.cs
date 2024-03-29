﻿using Proyecto1_Compi2.Abstracto;
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
                if (temp.entObjeto.tablaSimbolos.Count == 0)
                {
                    temp.Ejecutar(null, "", null);
                }               
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

        public override StringBuilder Traducir(Entorno ent, StringBuilder str)
        {
            StringBuilder izq = new StringBuilder();
            StringBuilder dere = new StringBuilder();
            str.Append(izquierdo.Traducir(ent,izq) + "." + derecho.Traducir(ent,dere));
            return str;
        }
    }
}
