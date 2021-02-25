using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Entornos;
using Proyecto1_Compi2.Expresiones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class Declaracion : Instruccion
    {
        Simbolo.EnumTipoDato tipoVariable;
        LinkedList<Expresion> variables;
        String nombreVariable;
        Expresion expresion;
        int fila, columna;
        String esReferencia_const;

        public Declaracion(Simbolo.EnumTipoDato tipo, String nombre, Expresion expresion, int fila, int columna)
        {
            this.tipoVariable = tipo;
            this.nombreVariable = nombre;
            this.expresion = expresion;
            this.fila = fila;
            this.columna = columna;
        }
        public Declaracion(Simbolo.EnumTipoDato tipo, LinkedList<Expresion> valores, Expresion expresion, int fila, int columna, String esreferencia)
        {
            this.tipoVariable = tipo;
            this.expresion = expresion;
            this.fila = fila;
            this.columna = columna;
            this.variables = valores;
            this.esReferencia_const = esreferencia;
        }
        public Declaracion(Simbolo.EnumTipoDato tipo, String nombre, Expresion expresion, int fila, int columna,String esReferencia_const)
        {
            this.tipoVariable = tipo;
            this.nombreVariable = nombre;
            this.expresion = expresion;
            this.fila = fila;
            this.columna = columna;
            this.esReferencia_const = esReferencia_const;
        }
        public Retornar Ejecutar(Entorno ent,String ambito)
        {
            //Resuelvo la expresión que le quiero asignar a la variable
            if (expresion != null && variables == null)
            {
                Expresion resultado = expresion.obtenerValor(ent);
                ent.Insertar(this.nombreVariable, new Simbolo(this.tipoVariable, resultado.valor, nombreVariable, ambito, esReferencia_const)); // Guardo la variable
            } else if (variables != null) {
                Expresion resultado = expresion.obtenerValor(ent);
                foreach (Id expr in variables) {
                    ent.Insertar(expr.id.ToString(), new Simbolo(this.tipoVariable, resultado.valor, nombreVariable, ambito, esReferencia_const)); // Guardo la variable        
                }
            }
            else
            {
                ent.Insertar(this.nombreVariable, new Simbolo(Simbolo.EnumTipoDato.NULL, null, nombreVariable, ambito, esReferencia_const)); // Guardo la variable
            }
             return new Retornar();
        }
        public void setExpresion(Expresion expr) {
            this.expresion = expr;
        }
    }
}
