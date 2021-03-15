using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Analizadores;
using Proyecto1_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class If : Instruccion
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
        public Retornar Ejecutar(Entorno ent, string Ambito, Sintactico AST)
        {
            bool condicionBooleana = Boolean.Parse(condicion.obtenerValor(ent).valor.ToString());
            if (condicionBooleana)
            {
                foreach (Instruccion ins in listaInstrucciones)
                {
                    Retornar retorn = ins.Ejecutar(ent, Ambito,AST);
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
            else {
                if (subIf != null)
                {
                    Retornar ret = subIf.Ejecutar(ent, Ambito, AST);
                    return ret;
                }
                else {
                    if (listaInsElse != null) {
                        foreach (Instruccion ins in listaInsElse)
                        {
                            Retornar retorn = ins.Ejecutar(ent, Ambito,AST);
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

        public StringBuilder TraducirInstr(Entorno ent, StringBuilder str, string Ambito)
        {
            StringBuilder temp = new StringBuilder();
            str.Append("if (" + condicion.Traducir(ent,temp) + " )then begin \n");
            temp.Clear();
            foreach (Instruccion ins in listaInstrucciones) {
                temp.Append("\t");
                temp = ins.TraducirInstr(ent,temp,Ambito);
            }
            str.Append(temp.ToString() + "\rend;");
            temp.Clear();
            if (subIf != null) {
                str.Append("\n");
                str.Append("else " + subIf.TraducirInstr(ent,temp.Clear(),Ambito));
            }
            if (listaInsElse!=null) {
                foreach (Instruccion ins in listaInsElse)
                {
                    temp = ins.TraducirInstr(ent, temp, Ambito);
                }
                str.Append("\nelse begin\n\t" + temp.ToString() + "\r end;");
            }
            return str;
        }
    }
}
