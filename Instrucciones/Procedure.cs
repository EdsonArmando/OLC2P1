using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Entornos;
using Proyecto1_Compi2.Expresiones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class Procedure : Expresion,Instruccion
    {
        public String id;
        public int fila, columna;
        public LinkedList<Abstracto.Expresion> param_Actuales;
        public LinkedList<Abstracto.Expresion> param_Formales;
        public LinkedList<Instruccion> listInstrucciones;
        public LinkedList<Instruccion> listVarLocales;
        public Procedure(String id, LinkedList<Abstracto.Expresion> param_Formales, LinkedList<Instruccion> listInstrucciones, LinkedList<Instruccion> listVarLocales,int fila, int columna)
        {
            this.id = id;
            this.param_Formales = param_Formales;
            this.listInstrucciones = listInstrucciones;
            this.listVarLocales = listVarLocales;
            this.fila = fila;
            this.columna = columna;
        }
        public Retornar Ejecutar(Entorno ent, String ambito)
        {
            Procedure fun = this;
            Singleton.getInstance().putFuncion(fun, id.ToLower());
            return new Retornar();
        }

        public override Simbolo.EnumTipoDato getTipo()
        {
            throw new NotImplementedException();
        }

        public override Expresion obtenerValor(Entorno ent)
        {
            Entorno tablaLocal = new Entorno(ent);
            if (listVarLocales != null) {
                foreach (Instruccion decla in listVarLocales) {
                    decla.Ejecutar(tablaLocal,this.id);
                }
            }
            if (param_Actuales != null && param_Formales != null && param_Actuales.Count == param_Formales.Count  )
            {
                for (int i = 0; i < param_Formales.Count; i++)
                {
                    Arimetica op = (Arimetica)param_Formales.ElementAt(i);
                    Expresion val = param_Actuales.ElementAt(i).obtenerValor(ent);
                    Declaracion declaracion = new Declaracion(val.tipo,op.valor.ToString(), val,this.fila,this.columna);
                    declaracion.Ejecutar(tablaLocal,this.id);
                }
            }
            foreach (Instruccion instr in listInstrucciones)
            {
                Retornar ret = instr.Ejecutar(tablaLocal,this.id);
                if (ret.isReturn)
                {
                    return ret.valor;
                }
            }
            return null;
        }

        public void setParametros(LinkedList<Expresion> parametros)
        {
            this.param_Actuales = parametros;
        }
    }
}
