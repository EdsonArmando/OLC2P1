using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Expresiones
{
    class Arimetica : Abstracto.Expresion
    {
        private Expresion operadorDer;
        private Expresion operadorIzq;
        private Expresion objeto;
        private Tipo_operacion tipo;
        public Arimetica(Expresion operadorIzq, Expresion operadorDer, Tipo_operacion tipo)
        {
            this.tipo = tipo;
            this.operadorIzq = operadorIzq;
            this.operadorDer = operadorDer;
        }
        public Arimetica(Arimetica operadorIzq, Tipo_operacion tipo)
        {
            this.tipo = tipo;
            this.operadorIzq = operadorIzq;
        }
        public Arimetica(String a, Tipo_operacion tipo)
        {
            this.valor = a;
            this.tipo = tipo;
        }
        public Arimetica(Expresion a, Tipo_operacion tipo)
        {
            this.objeto = a;
            this.tipo = tipo;
        }
        public Arimetica(Double a)
        {
            this.valor = a;
            this.tipo = Tipo_operacion.NUMERO;
        }
        public override Simbolo.EnumTipoDato getTipo()
        {
            throw new NotImplementedException();
        }

        public override Expresion obtenerValor(Entorno ent)
        {
            if (tipo == Tipo_operacion.DIVISION)
            {
                return new Literal(Simbolo.EnumTipoDato.DOUBLE, (Double)operadorIzq.obtenerValor(ent).valor / (Double)operadorDer.obtenerValor(ent).valor);
            }
            else if (tipo == Tipo_operacion.MULTIPLICACION)
            {
                Double izquierda = (Double)operadorIzq.obtenerValor(ent).valor;
                Double derecha = (Double)operadorDer.obtenerValor(ent).valor;
                return new Literal(Simbolo.EnumTipoDato.DOUBLE, izquierda * derecha);
            }
            else if (tipo == Tipo_operacion.CADENA)
            {
                return new Literal(Simbolo.EnumTipoDato.STRING, valor.ToString());
            }
            else if (tipo == Tipo_operacion.RESTA)
            {
                return new Literal(Simbolo.EnumTipoDato.DOUBLE, (Double)operadorIzq.obtenerValor(ent).valor - (Double)operadorDer.obtenerValor(ent).valor);
            }
            else if (tipo == Tipo_operacion.POTENCIA)
            {
                Double izquierda = (Double)operadorIzq.obtenerValor(ent).valor;
                Double derecha = (Double)operadorDer.obtenerValor(ent).valor;
                return new Literal(Simbolo.EnumTipoDato.DOUBLE, Math.Pow(izquierda, derecha));
            }
            else if (tipo == Tipo_operacion.SUMA)
            {
                Expresion izquierda = operadorIzq.obtenerValor(ent);
                Expresion derecha = operadorDer.obtenerValor(ent);
                if (izquierda.tipo == Simbolo.EnumTipoDato.DOUBLE && derecha.tipo == Simbolo.EnumTipoDato.DOUBLE || izquierda.tipo == Simbolo.EnumTipoDato.INT && derecha.tipo == Simbolo.EnumTipoDato.INT || izquierda.tipo == Simbolo.EnumTipoDato.DOUBLE && derecha.tipo == Simbolo.EnumTipoDato.INT || izquierda.tipo == Simbolo.EnumTipoDato.INT && derecha.tipo == Simbolo.EnumTipoDato.DOUBLE)
                {
                    return new Literal(Simbolo.EnumTipoDato.DOUBLE, (double)izquierda.valor + (double)derecha.valor);
                }
                else {
                    
                    return new Literal(Simbolo.EnumTipoDato.STRING, izquierda.valor.ToString() + derecha.valor.ToString());
                }
            }
            else if (tipo == Tipo_operacion.NEGATIVO)
            {
                return new Literal(Simbolo.EnumTipoDato.DOUBLE, (Double)operadorIzq.obtenerValor(ent).valor * -1);
            }
            else if (tipo == Tipo_operacion.NUMERO)
            {
                return new Literal(Simbolo.EnumTipoDato.DOUBLE, Double.Parse(valor.ToString()));
            }
            else if (tipo == Tipo_operacion.IDENTIFICADOR)
            {
                Simbolo sim = ent.obtener(valor.ToString(),ent);
                return new Literal(sim.tipo, Double.Parse(sim.valor.ToString()));
            }
            return null;
        }
        public enum Tipo_operacion
        {
            SUMA,
            RESTA,
            MULTIPLICACION,
            DIVISION,
            NEGATIVO,
            NUMERO,
            LETRA,
            IDENTIFICADOR,
            CADENA,
            POTENCIA,
            CONCATENACION
        }
    }
}
