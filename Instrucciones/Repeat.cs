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
            bool seguirWhile = true;
            while (!(Boolean)condicion.obtenerValor(ent).valor && seguirWhile)
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

        public StringBuilder Traducir(Entorno ent)
        {
            throw new NotImplementedException();
        }

        public StringBuilder TraducirInstr(Entorno ent, StringBuilder str, string Ambito)
        {
            throw new NotImplementedException();
        }
    }
}
