using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Analizadores;
using Proyecto1_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class Case : Instruccion
    {
        private Expresion condicion;
        private LinkedList<Instruccion> Case_Instr;
        private LinkedList<Instruccion> Else;
        private bool global_casIndividual;
        public Case(Expresion condicion, LinkedList<Instruccion> Case_Instr, LinkedList<Instruccion> Else, bool esIndividual) {
            this.condicion = condicion;
            this.Case_Instr = Case_Instr;
            this.Else = Else;
            this.global_casIndividual = esIndividual;
        }
        public Case(Expresion condicion, LinkedList<Instruccion> Case_Instr, bool esIndividual)
        {
            this.condicion = condicion;
            this.Case_Instr = Case_Instr;
            this.global_casIndividual = esIndividual;
        }
        public Retornar Ejecutar(Entorno ent, string Ambito, Sintactico AST)
        {
            bool Ejecutado = false;
            Expresion val = condicion.obtenerValor(ent);
            foreach (Case ins in Case_Instr) {
                Expresion exprtemp = ins.condicion.obtenerValor(ent); ;                
                if (exprtemp.valor.ToString() == val.valor.ToString()) {
                    Ejecutado = true;
                    LinkedList<Instruccion> instruccionesCase = ins.Case_Instr;
                    foreach (Instruccion inst in instruccionesCase) {
                        Retornar contenido = inst.Ejecutar(ent, Ambito, AST);
                        if (contenido.isBreak)
                        {
                            return contenido;
                        }
                        if (contenido.isContinue)
                        {
                            break;
                        }

                        if (contenido.isReturn)
                        {
                            return contenido;
                        }
                    }
                }
            }
            if (Ejecutado == false) {
                if (Else != null)
                {
                    foreach (Instruccion ins in Else)
                    {
                        Retornar ret = ins.Ejecutar(ent, Ambito, AST);
                        if (ret.isReturn)
                        {
                            return ret;
                        }
                    }
                }
            }
            return new Retornar(); ;
        }
        public StringBuilder TraducirInstr(Entorno ent, StringBuilder str, string Ambito)
        {
            StringBuilder temp = new StringBuilder();
            if (global_casIndividual == false)
            {
                str.Append("case " + condicion.Traducir(ent, temp) + " of");
            }
            else {
                str.Append("\n\t" + condicion.Traducir(ent, temp) + " : ");
            }
            temp.Clear();
            foreach (Instruccion ins in Case_Instr) {
                temp = ins.TraducirInstr(ent, temp, Ambito);
            }
            str.Append("\t" + temp.ToString());
            temp.Clear();
            if (Else != null) {
                str.Append("\t" + "else \n");
                foreach (Instruccion ins in Else)
                {
                    temp = ins.TraducirInstr(ent, temp, Ambito);
                    temp.Append("\n\t");
                }
                str.Append("\t" + temp.ToString());
                str.Append("end;");
            }                        
            return str;
        }
        public String traducirCaseIndividual(Instruccion ins) {
            return "";
        }

        public Retornar Ejecutar(Entorno ent, string ambito, Sintactico aST, Expresion condicion)
        {
            throw new NotImplementedException();
        }
    }
}
