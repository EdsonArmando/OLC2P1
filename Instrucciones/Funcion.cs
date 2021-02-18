﻿using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Entornos;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class Funcion : PlantillaFuncion, Instruccion
    {
        public String id;
        public LinkedList<Instruccion> param_Formales;
        public LinkedList<Abstracto.Expresion> param_Actuales;
        public LinkedList<Instruccion> listInstrucciones;
        public LinkedList<Instruccion> listVarLocales;
        public Funcion(String id, LinkedList<Instruccion> param_Formales, LinkedList<Instruccion> listInstrucciones, LinkedList<Instruccion> listVarLocales)
        {
            this.id = id;
            this.param_Formales = param_Formales;
            this.listInstrucciones = listInstrucciones;
            this.listVarLocales = listVarLocales;
        }

        public Retornar Ejecutar(Entorno ent, String ambito)
        {
            Funcion fun = this;
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
            if (listVarLocales != null)
            {
                foreach (Instruccion decla in listVarLocales)
                {
                    decla.Ejecutar(tablaLocal, this.id);
                }
            }
            if (param_Actuales != null && param_Formales != null)
            {
                if (param_Actuales.Count == param_Formales.Count)
                {
                    for (int i = 0; i < param_Formales.Count; i++)
                    {
                        Declaracion temporal = (Declaracion)param_Formales.ElementAt(i);
                        temporal.setExpresion(param_Actuales.ElementAt(i).obtenerValor(ent));
                        temporal.Ejecutar(tablaLocal, this.id);
                    }
                }
                else
                {
                    Form1.salidaConsola.AppendText("El numero de parametros no coincide!!!\n");
                    return null;
                }
            }
            foreach (Instruccion instr in listInstrucciones)
            {
                Retornar ret = instr.Ejecutar(tablaLocal, this.id);
                if (ret.isReturn)
                {
                    return ret.valor;
                }
            }
            return null;
        }

        public override void setParametros(LinkedList<Expresion> parametros)
        {
            this.param_Actuales = parametros;
        }
    }
}
