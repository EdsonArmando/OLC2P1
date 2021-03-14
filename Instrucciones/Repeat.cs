using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Analizadores;
using Proyecto1_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class Repeat : Instruccion
    {
        Expresion condicion;
        LinkedList<Instruccion> listaIntr;
        public Repeat(Expresion condicion, LinkedList<Instruccion> listaIntr)
        {
            this.condicion = condicion;
            this.listaIntr = listaIntr;
        }
        public Retornar Ejecutar(Entorno ent, string Ambito, Sintactico AST)
        {
            bool cond;
            bool seguiWhile = false;
            do {
                foreach (Instruccion ins in listaIntr)
                {
                    Retornar contenido = ins.Ejecutar(ent, Ambito, AST);
                    if (contenido.isBreak)
                    {
                        seguiWhile = true;
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
                cond = (Boolean)condicion.obtenerValor(ent).valor;
            } while (!(Boolean)condicion.obtenerValor(ent).valor && !seguiWhile);            
                return new Retornar();
        }

        public StringBuilder Traducir(Entorno ent)
        {
            throw new NotImplementedException();
        }

        public StringBuilder TraducirInstr(Entorno ent, StringBuilder str, string Ambito)
        {
            StringBuilder temp = new StringBuilder();
            str.Append("repeat ");
            temp.Clear();
            foreach (Instruccion ins in listaIntr)
            {
                temp.Append("\n\t");
                temp = ins.TraducirInstr(ent, temp, Ambito);
            }
            str.Append(temp.ToString() + "\n\runtil " + condicion.Traducir(ent,temp.Clear()) + ";" );
            return str;
        }
    }
}
