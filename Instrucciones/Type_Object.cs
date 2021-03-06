using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class Type_Object : Abstracto.Instruccion
    {
        public String nombreType;
        public LinkedList<Instruccion> listaVariables;
        public Type_Object(String nombre, LinkedList<Instruccion> variables) {
            this.nombreType = nombre.ToLower();
            this.listaVariables = variables;
        }
        public Retornar Ejecutar(Entorno ent, string Ambito)
        {
            //Guardar Type en tabla de Simbolos
            ent.insertType(nombreType,this);
            return new Retornar();
        }
    }
}
