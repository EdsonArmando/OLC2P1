using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class Declaracion : Instruccion
    {
        Simbolo.EnumTipoDato tipoVariable;
        String nombreVariable;
        Expresion expresion;
        int fila, columna;

        public Declaracion(Simbolo.EnumTipoDato tipo, String nombre, Expresion expresion, int fila, int columna)
        {
            this.tipoVariable = tipo;
            this.nombreVariable = nombre;
            this.expresion = expresion;
            this.fila = fila;
            this.columna = columna;
        }
        public Retornar Ejecutar(Entorno ent,String ambito)
        {
            Expresion resultado = expresion.obtenerValor(ent); //Resuelvo la expresión que le quiero asignar a la variable
            if (resultado != null)
            {
                ent.Insertar(this.nombreVariable, new Simbolo(this.tipoVariable, resultado.valor, nombreVariable,ambito)); // Guardo la variable
            }
            return new Retornar();
        }
    }
}
