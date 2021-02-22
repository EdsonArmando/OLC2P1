using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class Repeat : Abstracto.Instruccion
    {
        Expresion condicion;
        LinkedList<Instruccion> listaIntr;
        public Repeat(Expresion condicion, LinkedList<Instruccion> listaIntr)
        {
            this.condicion = condicion;
            this.listaIntr = listaIntr;
        }
        public Retornar Ejecutar(Entorno ent, string Ambito)
        {
            bool seguirWhile = true;
            while (!(Boolean)condicion.obtenerValor(ent).valor && seguirWhile)
            {
                foreach (Instruccion ins in listaIntr)
                {
                    Retornar contenido = ins.Ejecutar(ent,Ambito);
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
    }
}
