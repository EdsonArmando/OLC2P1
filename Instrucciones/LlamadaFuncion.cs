using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class LlamadaFuncion : Expresion, Instruccion
    {
        String id;
        LinkedList<Expresion> parametros;
        public LlamadaFuncion(String id, LinkedList<Expresion> parametros, int fila, int columna)
        {
            this.id = id;
            this.parametros = parametros;
            this.fila = fila;
            this.columna = columna;
        }
        public Retornar Ejecutar(Entorno ent,String ambito)
        {
            Procedure f = (Procedure)Singleton.getInstance().getFuncion(this.id.ToLower());
            if (f != null)
            {
                f.setParametros(parametros);
                f.obtenerValor(ent);
            }
            return new Retornar();
        }

        public override Simbolo.EnumTipoDato getTipo()
        {
            throw new NotImplementedException();
        }

        public override Expresion obtenerValor(Entorno ent)
        {
            Procedure f = (Procedure)Singleton.getInstance().getFuncion(this.id);
            if (f != null)
            {
                f.setParametros(parametros);
                Expresion o = f.obtenerValor(ent);
                return o;
            }
            return null;
        }
    }
}
