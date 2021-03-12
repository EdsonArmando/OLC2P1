using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Analizadores;
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
        public Retornar Ejecutar(Entorno ent,String ambito, Sintactico AST)
        {
            PlantillaFuncion f = (PlantillaFuncion)Singleton.getInstance().getFuncion(this.id.ToLower());
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
            PlantillaFuncion f = (PlantillaFuncion)Singleton.getInstance().getFuncion(this.id.ToLower());
            if (f != null)
            {
                f.setParametros(parametros);
                Expresion o = f.obtenerValor(ent);
                return o;
            }
            return null;
        }

        public override StringBuilder Traducir(Entorno ent, StringBuilder strin)
        {
            StringBuilder val = new StringBuilder();
            foreach (Abstracto.Expresion exp in parametros)
            {
                val = exp.Traducir(ent, val);
                val.Append(",");
            }
            if (parametros.Count > 0) {
                val.Remove(val.Length - 1, 1);
            }            
            strin.Append(id+ "(" + val.ToString() + ")");
            return strin;
        }

        public StringBuilder TraducirInstr(Entorno ent, StringBuilder str, string Ambito)
        {
            StringBuilder temp = new StringBuilder();
            if (parametros != null && parametros.Count != 0) {
                foreach (Abstracto.Expresion exp in parametros)
                {
                    temp = exp.Traducir(ent, temp);
                    temp.Append(",");
                }
                temp.Remove(temp.Length - 1, 1);
            }            
            str.Append( id+"(" + temp.ToString() + ");");
            return str;
        }
    }
}
