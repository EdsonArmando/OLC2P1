using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class If : Abstracto.Instruccion
    {
        Abstracto.Expresion condicion;
        LinkedList<Instruccion> listaInstrucciones;
        LinkedList<Instruccion> listaInsElse;
        Instruccion subIf;
        int fila, columna;
        public If(Abstracto.Expresion expresionBoolena,LinkedList<Instruccion> listaInst,LinkedList<Instruccion> ElseSt,int fila,int columna) {
            this.condicion = expresionBoolena;
            this.listaInstrucciones = listaInst;
            this.listaInsElse = ElseSt;
            this.fila = fila;
            this.columna = columna;
        }
        public If(Abstracto.Expresion expresionBoolena, LinkedList<Instruccion> listaInst, Instruccion subIf, int fila, int columna,bool EssubIf)
        {
            this.condicion = expresionBoolena;
            this.listaInstrucciones = listaInst;
            this.subIf = subIf;
            this.fila = fila;
            this.columna = columna;
        }
        public Retornar Ejecutar(Entorno ent, string Ambito)
        {
            bool condicionBooleana = (bool)condicion.obtenerValor(ent).valor;
            if (condicionBooleana)
            {
                foreach (Instruccion ins in listaInstrucciones)
                {
                    Retornar retorn = ins.Ejecutar(ent, Ambito);
                    if (retorn.isReturn == true)
                    {
                        return retorn;
                    }
                    if (retorn.isBreak == true)
                    {
                        return retorn;
                    }
                    if (retorn.isContinue == true)
                    {
                        return retorn;
                    }
                }
                if (subIf != null)
                {
                    return subIf.Ejecutar(ent, Ambito);
                }
            }
            else {
                if (subIf != null)
                {
                    Retornar ret = subIf.Ejecutar(ent, Ambito);
                    return ret;
                }
                else {
                    if (listaInsElse != null) {
                        foreach (Instruccion ins in listaInsElse)
                        {
                            Retornar retorn = ins.Ejecutar(ent, Ambito);
                            if (retorn.isReturn == true)
                            {
                                return retorn;
                            }
                            if (retorn.isBreak == true)
                            {
                                return retorn;
                            }
                            if (retorn.isContinue == true)
                            {
                                return retorn;
                            }
                        }
                    }
                }
            }
            return new Retornar();
        }
    }
}
