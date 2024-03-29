﻿using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Analizadores;
using Proyecto1_Compi2.Entornos;
using Proyecto1_Compi2.Expresiones;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class Procedure : PlantillaFuncion, Instruccion
    {
        public String id;
        public int fila, columna;
        public LinkedList<Abstracto.Expresion> param_Actuales;
        public LinkedList<Abstracto.Instruccion> param_Formales;
        public LinkedList<Instruccion> listInstrucciones;
        public LinkedList<Instruccion> listVarLocales;
        public String nombreOriginal;

        public Procedure(String id, LinkedList<Abstracto.Instruccion> param_Formales, LinkedList<Instruccion> listInstrucciones, LinkedList<Instruccion> listVarLocales,int fila, int columna,String nombreOriginal)
        {
            this.id = id;
            this.param_Formales = param_Formales;
            this.listInstrucciones = listInstrucciones;
            this.listVarLocales = listVarLocales;
            this.fila = fila;
            this.columna = columna;
            this.nombreOriginal = nombreOriginal;
        }
        public Retornar Ejecutar(Entorno ent, String ambito, Sintactico AST)
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
                    decla.Ejecutar(tablaLocal,this.id,null);
                }
            }
            if (param_Actuales != null && param_Formales != null)
            {
                if (param_Actuales.Count == param_Formales.Count) {
                    for (int i = 0; i < param_Formales.Count; i++)
                    {
                        Declaracion temporal = (Declaracion)param_Formales.ElementAt(i);
                        Expresion resultado = param_Actuales.ElementAt(i).obtenerValor(ent);
                        if (resultado == null) {
                            return new Literal(Simbolo.EnumTipoDato.ERROR,"");
                        }
                        if (resultado.tipo == Simbolo.EnumTipoDato.ARRAY)
                        {
                            Id arrayTemp = new Id("");
                            Expresion result = param_Actuales.ElementAt(i).obtenerValor(ent);
                            if (param_Actuales.ElementAt(i).GetType() == arrayTemp.GetType())
                            {
                                arrayTemp = (Id)param_Actuales.ElementAt(i);
                                Simbolo sim = ent.obtener(arrayTemp.id, ent);
                                tablaLocal.Insertar(temporal.nombreVariable, sim);
                            }
                            else if (result.tipo == Simbolo.EnumTipoDato.ARRAY)
                            {
                                Literal tem = (Literal)result;
                                tablaLocal.Insertar(temporal.nombreVariable, new Simbolo(tem.tipo, tem.valor, tem.id, tem.ambito, tem.referencia_const, tem.posicion_X, tem.posicion_Y, tem.posicion_Z, tem.tipoItem));
                            }                            
                        }
                        else if (resultado.tipo == Simbolo.EnumTipoDato.OBJETO_TYPE)
                        {
                            tablaLocal.Insertar(temporal.nombreVariable, new Simbolo(resultado.tipo, resultado.valor, temporal.nombreVariable, id, temporal.esReferencia_const));
                        }
                        else
                        {
                            temporal.setExpresion(resultado);
                            temporal.Ejecutar(tablaLocal, this.id,null);
                        }
                    }
                }
                else {
                    Form1.salidaConsola.AppendText("El numero de parametros no coincide!!!\n");
                    return null;
                }              
            }
            foreach (Instruccion instr in listInstrucciones)
            {
                Retornar ret = instr.Ejecutar(tablaLocal,this.id, null);
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

        public override StringBuilder Traducir(Entorno ent, StringBuilder strin)
        {
            throw new NotImplementedException();
        }

        public StringBuilder TraducirInstr(Entorno ent, StringBuilder str, string Ambito)
        {
            LlamadaFuncion llamada = new LlamadaFuncion(null,null,0,0);
            StringBuilder temp = new StringBuilder();
            str.Append("procedure " + id + "(");
            if (param_Formales != null && param_Formales.Count != 0 ) {
                foreach (Instruccion ins in param_Formales)
                {
                    temp = ins.TraducirInstr(ent, temp, Ambito);
                }
                temp.Remove(temp.Length - 2, 1);
            }            
            str.Append(temp + ");\n");
            temp.Clear();
            foreach (Instruccion ins in listVarLocales)
            {
                temp.Append("\n\t");
                temp = ins.TraducirInstr(ent, temp, Ambito);
            }
            str.Append(temp.ToString() + "\nbegin\n");
            temp.Clear();
            foreach (Instruccion ins in listInstrucciones)
            {
                if (ins.GetType() == llamada.GetType())
                {
                    llamada = (LlamadaFuncion)ins;
                    llamada.setNombre(id + "_" + llamada.id);
                    temp.Append("\n\t");
                    temp = llamada.TraducirInstr(ent, temp, Ambito);
                }
                else {
                    temp.Append("\n\t");
                    temp = ins.TraducirInstr(ent, temp, Ambito);
                }                                        
            }
            str.Append(temp.ToString() + "\nend;");
            return str;
        }
    }
}
