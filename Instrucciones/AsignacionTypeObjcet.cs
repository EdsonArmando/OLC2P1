using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Analizadores;
using Proyecto1_Compi2.Entornos;
using Proyecto1_Compi2.Expresiones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class AsignacionTypeObjcet : Instruccion
    {
        public LinkedList<String> listId;
        public Expresion valor;
        public AsignacionTypeObjcet(LinkedList<String> listId, Expresion valor) {
            this.listId = listId;
            this.valor = valor;
        }
        public Retornar Ejecutar(Entorno ent, string Ambito, Sintactico AST)
        {
            LinkedList<String> tempId = new LinkedList<String>();
            foreach (String ST in listId)
            {
                tempId.AddLast(ST);
            }
            //Obtengo el primer entorno  y Resulevo la variable
            Simbolo sim = ent.obtener(tempId.ElementAt(0), ent);            
            Expresion resultado = valor.obtenerValor(ent);                      
            Type_Object temp = (Type_Object)sim.valor;
            tempId.RemoveFirst();
            if (temp.entObjeto.tablaSimbolos.Count == 0) {
                temp.Ejecutar(null,Ambito,null);
            }            
            setExpresion(tempId, temp.entObjeto, resultado, Ambito);
            return new Retornar();
        }


        public void setExpresion(LinkedList<String> accesos, Entorno ent, Expresion res, String Ambito)
        {            
            Simbolo sim = ent.obtener(accesos.ElementAt(0), ent);         
            if (sim.tipo == Simbolo.EnumTipoDato.OBJETO_TYPE && accesos.Count !=1)
            {
                Type_Object temp = (Type_Object)sim.valor;
                accesos.RemoveFirst();
                if (temp.entObjeto.tablaSimbolos.Count == 0)
                {
                    temp.Ejecutar(null, Ambito, null);
                }
                setExpresion(accesos, temp.entObjeto,res,Ambito);
            }
            else {
                if (res.tipo == Simbolo.EnumTipoDato.ARRAY)
                {
                    Literal temp2 = (Literal)res;                    
                    ent.setVariable(accesos.ElementAt(0).ToLower(), new Simbolo(temp2.tipo, temp2.valor, temp2.id, temp2.ambito, temp2.referencia_const, temp2.posicion_X, temp2.posicion_Y, temp2.posicion_Z, temp2.tipoItem), ent); // Guardo la variable
                }
                else
                {
                    ent.setVariable(accesos.ElementAt(0).ToLower(), new Simbolo(res.tipo, res.valor, accesos.ElementAt(0).ToLower(), Ambito, ""), ent);
                }
            }
        }
        public StringBuilder TraducirInstr(Entorno ent, StringBuilder str, string Ambito)
        {
            throw new NotImplementedException();
        }
    }
}
