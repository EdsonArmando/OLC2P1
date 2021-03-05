using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class Type_Object : Abstracto.Instruccion
    {
        private String nombreType;
        private LinkedList<Instruccion> listaVariables;
        public Type_Object(String nombre, LinkedList<Instruccion> variables) {
            this.nombreType = nombre;
            this.listaVariables = variables;
        }
        public Retornar Ejecutar(Entorno ent, string Ambito)
        {

            throw new NotImplementedException();
        }
    }
}
