using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Analizadores;
using Proyecto1_Compi2.Entornos;
using Proyecto1_Compi2.Expresiones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class AsignacionTypeObjcet : Abstracto.Instruccion
    {
        public LinkedList<String> listId;
        public Expresion valor;
        public AsignacionTypeObjcet(LinkedList<String> listId, Expresion valor) {
            this.listId = listId;
            this.valor = valor;
        }
        public Retornar Ejecutar(Entorno ent, string Ambito,Sintactico AST)
        {
            Expresion resultado = valor.obtenerValor(ent);
            Type_Object temp = new Type_Object("",null);
            Simbolo sim=null;
            foreach (String id in listId) {
                if (temp.nombreType == "")
                {
                    sim = ent.obtener(id, ent);
                }
                else {
                    sim = ent.obtener(id,temp.entObjeto);
                }
                if (sim == null && temp != null) {
                    sim = temp.entObjeto.obtener(id,temp.entObjeto);
                }
                if (sim != null && sim.valor.GetType() == temp.GetType())
                {
                    temp = (Type_Object)sim.valor;
                    if (temp.entObjeto.tablaSimbolos.Count == 0) {
                        temp.Ejecutar(null,"",AST);
                    }
                }
                else {
                    if (resultado.tipo == Simbolo.EnumTipoDato.ARRAY)
                    {
                        Literal temp2 = (Literal)resultado;
                        temp.entObjeto.setVariable(id, new Simbolo(temp2.tipo, temp2.valor, temp2.id, temp2.ambito, temp2.referencia_const, temp2.posicion_X, temp2.posicion_Y, temp2.posicion_Z, temp2.tipoItem),temp.entObjeto); // Guardo la variable
                    }
                    else {
                        temp.entObjeto.setVariable(id, new Simbolo(resultado.tipo, resultado.valor, id, Ambito, ""), temp.entObjeto);
                    }                    
                }
            }
            return new Retornar();
        }
    }
}
