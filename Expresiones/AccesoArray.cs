using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Expresiones
{
    class AccesoArray : Abstracto.Expresion
    {
        private String Nombre_id;
        private Expresion[] valor = new Expresion[2];

        public AccesoArray(String id, Expresion[] tamanio)
        {
            this.Nombre_id = id;
            this.valor = tamanio;
        }

        public override Simbolo.EnumTipoDato getTipo()
        {
            throw new NotImplementedException();
        }

        public override Expresion obtenerValor(Entorno ent)
        {
            Simbolo sim = ent.obtener(Nombre_id, ent);
            if (sim == null)
            {
                return new Literal(Simbolo.EnumTipoDato.ERROR, null);
            }
            //Acceso a un array de una dimension
            if (valor[0] != null && valor[1] == null && valor[2] == null) {
                int calcularPos = 0;
                Expresion[] temp = (Expresion[])sim.valor;
                int[] x = sim.posicion_X;
                Double val = Double.Parse(this.valor[0].obtenerValor(ent).valor.ToString()) - x[0];
                bool isInt = val % 1 == 0;
                if (isInt)
                {
                    calcularPos = int.Parse(this.valor[0].obtenerValor(ent).valor.ToString()) - x[0];
                }
                else {
                    calcularPos = (int)Math.Ceiling(val);
                }
                
                Expresion valor = temp[calcularPos];                
                if (valor == null) {
                    return new Literal(Simbolo.EnumTipoDato.NULL,"");
                }
                return valor;
            }
            //Acceso a un array de dos dimensiones
            else if (valor[0] != null && valor[1] != null && valor[2] == null)
            {
                Expresion[] temp = (Expresion[])sim.valor;
                int[] x = sim.posicion_X;
                int[] y = sim.posicion_Y;
                int calcularPosX = int.Parse(this.valor[0].obtenerValor(ent).valor.ToString()) - x[0];
                int calcularPosY = int.Parse(this.valor[1].obtenerValor(ent).valor.ToString()) - y[0];
                int posicionTotal = calcularPosX * ((y[1] - y[0]) + 1) + calcularPosY;
                Expresion valor = temp[posicionTotal];                
                if (valor == null)
                {
                    return new Literal(Simbolo.EnumTipoDato.NULL, "");
                }
                return valor;
            }
            //Acceso a un array de tres dimensiones
            else if (valor[0] != null && valor[1] != null && valor[2] != null)
            {
                Expresion[] temp = (Expresion[])sim.valor;
                int[] x = sim.posicion_X;
                int[] y = sim.posicion_Y;
                int[] z = sim.posicion_Z;
                int calcularPosX = int.Parse(this.valor[0].obtenerValor(ent).valor.ToString()) - x[0];
                int calcularPosY = int.Parse(this.valor[1].obtenerValor(ent).valor.ToString()) - y[0];
                int calcularPosZ = int.Parse(this.valor[2].obtenerValor(ent).valor.ToString()) - z[0];
                int posicionTotal = calcularPosZ + ((z[1] - z[0]) + 1) * (calcularPosY + ((y[1] - y[0]) + 1) * calcularPosX);
                Expresion valor = temp[posicionTotal];
                if (valor == null)
                {
                    return new Literal(Simbolo.EnumTipoDato.NULL, "");
                }
                return valor;
            }
            return null;
        }

        public override StringBuilder Traducir(Entorno ent, StringBuilder strin)
        {
            StringBuilder tm = new StringBuilder();
            if (valor[0] != null && valor[1] == null && valor[2] == null)
            {
                strin.Append(Nombre_id + "[" + valor[0].Traducir(ent, tm).ToString() + "]");
                
            }
            //Acceso a un array de dos dimensiones
            else if (valor[0] != null && valor[1] != null && valor[2] == null)
            {
                strin.Append(Nombre_id + "[" + valor[0].Traducir(ent, tm).ToString() + " , " + valor[1].Traducir(ent, tm.Clear()).ToString() + "]");
            }
            //Acceso a un array de tres dimensiones
            else if (valor[0] != null && valor[1] != null && valor[2] != null)
            {
                strin.Append(Nombre_id + "[" + valor[0].Traducir(ent, tm).ToString() + " , " + valor[1].Traducir(ent, tm.Clear()).ToString() + " , " + valor[2].Traducir(ent, tm.Clear()).ToString() + "]");
            }
            return strin;
        }
    }
}
