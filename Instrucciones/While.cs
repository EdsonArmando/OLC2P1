using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Analizadores;
using Proyecto1_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class While : Instruccion
    {
        Expresion condicion;
        LinkedList<Instruccion> listaIntr;
        public While(Expresion condicion, LinkedList<Instruccion> listaIntr)
        {
            this.condicion = condicion;
            this.listaIntr = listaIntr;
        }
        public Retornar Ejecutar(Entorno ent, string Ambito, Sintactico AST)
        {
            bool seguirWhile = true;
            while ((Boolean)condicion.obtenerValor(ent).valor && seguirWhile)
            {
                foreach (Instruccion ins in listaIntr)
                {
                    Retornar contenido = ins.Ejecutar(ent,Ambito, AST);
                    if (contenido.isBreak)
                    {
                        seguirWhile = false;
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
            return new Retornar();
        }

        public StringBuilder TraducirInstr(Entorno ent, StringBuilder str, string Ambito)
        {
            StringBuilder temp = new StringBuilder();
            str.Append("\nwhile(" + condicion.Traducir(ent,temp) + ") do \n\t begin");
            temp.Clear();
            foreach (Instruccion ins in listaIntr)
            {
                temp.Append("\n\t");
                temp = ins.TraducirInstr(ent, temp, Ambito);
            }
            str.Append(temp.ToString() + "\rend;");
            return str;
        }         
    }
}
