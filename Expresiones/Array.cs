using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Expresiones
{
    class Array : Abstracto.Instruccion
    {
        private String Nombre_id;
        private Simbolo.EnumTipoDato Tipo;
        private Expresion[,] valor;
         
        public Array(String id, Simbolo.EnumTipoDato tipo, Expresion[,] tamanio) {
            this.Nombre_id = id;
            this.Tipo = tipo;
            this.valor = tamanio;
        }

        public Retornar Ejecutar(Entorno ent, string Ambito)
        {
            /*
             * Insertar arreglo en 
             * tabla de Simbolos
             * 
             */
            //Significa que es de una dimension
            if (valor[0, 0] != null && valor[1, 0] == null)
            {
                Expresion PosiInicial = valor[0, 0].obtenerValor(ent);
                Expresion PosFinal = valor[0, 1].obtenerValor(ent);
                int tamanioVector = (int.Parse(PosFinal.valor.ToString()) - int.Parse(PosiInicial.valor.ToString())) + 1;
                Expresion[] temp = new Expresion[tamanioVector];
                int[] posiciones = new int[2];
                posiciones[0] = int.Parse(PosiInicial.valor.ToString());
                posiciones[1] = int.Parse(PosFinal.valor.ToString());
                Simbolo array = new Simbolo(Simbolo.EnumTipoDato.ARRAY,temp,Nombre_id,Ambito,"",posiciones,null,null,Tipo);
                ent.Insertar(Nombre_id,array);
            }
            //Significa que es de dos dimension
            else if (valor[0, 0] != null && valor[1, 0] != null && valor[2, 0] == null)
            {
                Expresion PosiInicialX = valor[0, 0].obtenerValor(ent);
                Expresion PosFinalX = valor[0, 1].obtenerValor(ent);
                Expresion PosiInicialY = valor[1, 0].obtenerValor(ent);
                Expresion PosFinalY = valor[1, 1].obtenerValor(ent);
                int tamanioVector = ((int.Parse(PosFinalX.valor.ToString()) - int.Parse(PosiInicialX.valor.ToString())) + 1) * ((int.Parse(PosFinalY.valor.ToString()) - int.Parse(PosiInicialY.valor.ToString())) + 1);
                Expresion[] temp = new Expresion[tamanioVector];
                int[] posicionesX = new int[2];
                posicionesX[0] = int.Parse(PosiInicialX.valor.ToString());
                posicionesX[1] = int.Parse(PosFinalX.valor.ToString());
                int[] posicionesY = new int[2];
                posicionesY[0] = int.Parse(PosiInicialY.valor.ToString());
                posicionesY[1] = int.Parse(PosFinalY.valor.ToString());
                Simbolo array = new Simbolo(Simbolo.EnumTipoDato.ARRAY, temp, Nombre_id, Ambito, "", posicionesX, posicionesY, null,Tipo);
                ent.Insertar(Nombre_id, array);
            }
            //Significa que es de Tres dimension
            else if (valor[0, 0] != null && valor[1, 0] != null && valor[2, 0] != null)
            {
                Expresion PosiInicialX = valor[0, 0].obtenerValor(ent);
                Expresion PosFinalX = valor[0, 1].obtenerValor(ent);
                Expresion PosiInicialY = valor[1, 0].obtenerValor(ent);
                Expresion PosFinalY = valor[1, 1].obtenerValor(ent);
                Expresion PosiInicialZ = valor[2, 0].obtenerValor(ent);
                Expresion PosFinalZ = valor[2, 1].obtenerValor(ent);
                int tamanioVector = ((int.Parse(PosFinalX.valor.ToString()) - int.Parse(PosiInicialX.valor.ToString())) + 1) * ((int.Parse(PosFinalY.valor.ToString()) - int.Parse(PosiInicialY.valor.ToString())) + 1) * ((int.Parse(PosFinalZ.valor.ToString()) - int.Parse(PosiInicialZ.valor.ToString())) + 1);
                Expresion[] temp = new Expresion[tamanioVector];
                int[] posicionesX = new int[2];
                posicionesX[0] = int.Parse(PosiInicialX.valor.ToString());
                posicionesX[1] = int.Parse(PosFinalX.valor.ToString());
                int[] posicionesY = new int[2];
                posicionesY[0] = int.Parse(PosiInicialY.valor.ToString());
                posicionesY[1] = int.Parse(PosFinalY.valor.ToString());
                int[] posicionesZ = new int[2];
                posicionesZ[0] = int.Parse(PosiInicialZ.valor.ToString());
                posicionesZ[1] = int.Parse(PosFinalZ.valor.ToString());
                Simbolo array = new Simbolo(Simbolo.EnumTipoDato.ARRAY, temp, Nombre_id, Ambito, "", posicionesX, posicionesY, posicionesZ,Tipo);
                ent.Insertar(Nombre_id, array);
            }
            else {
                Form1.salidaConsola.AppendText("Ocurrio un error al guardar el array !!! \n");
            }
            return new Retornar();
        }

        public static implicit operator Array(Expresion v)
        {
            throw new NotImplementedException();
        }
    }
}
