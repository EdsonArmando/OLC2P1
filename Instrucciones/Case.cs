using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class Case : Abstracto.Instruccion
    {
        private Expresion condicion;
        private LinkedList<Instruccion> Case_Instr;
        private LinkedList<Instruccion> Else;
        public Case(Expresion condicion, LinkedList<Instruccion> Case_Instr, LinkedList<Instruccion> Else) {
            this.condicion = condicion;
            this.Case_Instr = Case_Instr;
            this.Else = Else;
        }
        public Case(Expresion condicion, LinkedList<Instruccion> Case_Instr)
        {
            this.condicion = condicion;
            this.Case_Instr = Case_Instr;
        }
        public Retornar Ejecutar(Entorno ent, string Ambito)
        {
            Expresion val = condicion.obtenerValor(ent);
            foreach (Case ins in Case_Instr) {
                Expresion exprtemp = ins.condicion.obtenerValor(ent);
                if (exprtemp.valor.ToString() == val.valor.ToString()) {
                    LinkedList<Instruccion> tempList = ins.Case_Instr;
                    Retornar ret = tempList.ElementAt(0).Ejecutar(ent,Ambito);
                    return ret;
                }
            }
            foreach (Instruccion ins in Else) {
                Retornar ret = ins.Ejecutar(ent,Ambito);
                if (ret.isReturn) {
                    return ret;
                }
            }
            return new Retornar();
        }
    }
}
