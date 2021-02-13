using Proyecto1_Compi2.Abstracto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class Funcion : Instruccion
    {
        public String id;
        public LinkedList<Expresion> paramFuncion;
        public LinkedList<Instruccion> listInstrucciones;
        public LinkedList<Instruccion> listVarLocales;
        public Funcion(String id, LinkedList<Expresion> paramFuncion, LinkedList<Instruccion> listInstrucciones, LinkedList<Instruccion> listVarLocales)
        {
            this.id = id;
            this.paramFuncion = paramFuncion;
            this.listInstrucciones = listInstrucciones;
            this.listVarLocales = listVarLocales;
        }
        public override Retornar ejectuar(Entornos.Entorno ent)
        {
            return new Retornar();
        }
    }
}
