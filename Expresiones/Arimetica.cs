using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Analizadores;
using Proyecto1_Compi2.Entornos;
using Proyecto1_Compi2.Instrucciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Expresiones
{
    class Arimetica : Abstracto.Expresion
    {
        private Expresion operadorDer;
        private Expresion operadorIzq;
        private Tipo_operacion tipo;
        public Arimetica(Expresion operadorIzq, Expresion operadorDer, Tipo_operacion tipo)
        {
            this.tipo = tipo;
            this.operadorIzq = operadorIzq;
            this.operadorDer = operadorDer;
        }
        public Arimetica(Expresion operadorIzq, Tipo_operacion tipo)
        {
            this.tipo = tipo;
            this.operadorIzq = operadorIzq;
        }
        public Arimetica(String a, Tipo_operacion tipo)
        {
            this.valor = a;
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
                Expresion izquierda = operadorIzq.obtenerValor(ent);
                Expresion derecha = operadorDer.obtenerValor(ent);
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
            else if (tipo == Tipo_operacion.TRUE )
            {
                return new Literal(Simbolo.EnumTipoDato.BOOLEAN, "true");
            }
            else if (tipo == Tipo_operacion.FALSE)
            {
                return new Literal(Simbolo.EnumTipoDato.BOOLEAN, "false");
            }
            else if (tipo == Tipo_operacion.IDENTIFICADOR)
            {
                Simbolo sim = ent.obtener(valor.ToString().ToLower(), ent);
                if (Singleton.getInstance().existType(valor.ToString())) {
                    return new Literal(sim.tipo, Singleton.getInstance().getType(valor.ToString()));
                }
                if (sim == null) {
                    Form1.salidaConsola.AppendText("No se encontro la variable\n");
                    return null;
                } else if (sim.tipo == Simbolo.EnumTipoDato.CHAR) {
                    return new Literal(sim.tipo, sim.valor);
                } else if (sim.tipo == Simbolo.EnumTipoDato.OBJETO_TYPE) {
                    return new Literal(sim.tipo, sim.valor);
                }
                else if (sim.tipo == Simbolo.EnumTipoDato.TYPE)
                {
                    return new Literal(sim.tipo, sim.valor);
                }
                else if (sim.tipo == Simbolo.EnumTipoDato.BOOLEAN)
                {
                    return new Literal(sim.tipo, bool.Parse(sim.valor.ToString()));
                }
                else if (sim.tipo == Simbolo.EnumTipoDato.STRING)
                {
                    return new Literal(sim.tipo, sim.valor);
                }
                else if (sim.tipo == Simbolo.EnumTipoDato.ARRAY)
                {
                    return new Literal(sim.tipo, sim.valor, sim.id, sim.ambito, sim.referencia_const, sim.posicion_X, sim.posicion_Y, sim.posicion_Z, sim.tipoItem);
                }
                if (sim.valor != null && sim.valor.ToString() != "")
                {
                    return new Literal(sim.tipo, Double.Parse(sim.valor.ToString()));
                }
                else {
                    return new Literal(sim.tipo, Double.Parse("0"));
                }              
            }
            /*
             * 
             *      OPERACIONES LOGICAS
             * 
             */
            else if (tipo == Tipo_operacion.MAYOR_QUE)
            {

                return new Literal(Simbolo.EnumTipoDato.DOUBLE, (Double)operadorIzq.obtenerValor(ent).valor > (Double)operadorDer.obtenerValor(ent).valor);
            }
            else if (tipo == Tipo_operacion.DIFERENCIACION)
            {
                Double uno = (Double)operadorIzq.obtenerValor(ent).valor;
                Double dos = (Double)operadorDer.obtenerValor(ent).valor;
                return new Literal(Simbolo.EnumTipoDato.DOUBLE, (Double)operadorIzq.obtenerValor(ent).valor != (Double)operadorDer.obtenerValor(ent).valor);
            }
            else if (tipo == Tipo_operacion.AND)
            {
                object uno = operadorIzq.obtenerValor(ent).valor.ToString();
                object dos = operadorDer.obtenerValor(ent).valor.ToString();
                return new Literal(Simbolo.EnumTipoDato.BOOLEAN, bool.Parse(operadorIzq.obtenerValor(ent).valor.ToString()) && bool.Parse(operadorDer.obtenerValor(ent).valor.ToString()));
            }
            else if (tipo == Tipo_operacion.OR)
            {
                return new Literal(Simbolo.EnumTipoDato.BOOLEAN, bool.Parse(operadorIzq.obtenerValor(ent).valor.ToString()) || bool.Parse(operadorDer.obtenerValor(ent).valor.ToString()));
            }
            else if (tipo == Tipo_operacion.XOR)
            {
                return new Literal(Simbolo.EnumTipoDato.BOOLEAN, bool.Parse(operadorIzq.obtenerValor(ent).valor.ToString()) ^ bool.Parse(operadorDer.obtenerValor(ent).valor.ToString()));
            }
            else if (tipo == Tipo_operacion.DIFERENTE)
            {
                bool izquierda = bool.Parse(operadorIzq.obtenerValor(ent).valor.ToString());
                return new Literal(Simbolo.EnumTipoDato.BOOLEAN, !izquierda);
            }
            else if (tipo == Tipo_operacion.IGUAL_QUE)
            {
                return new Literal(Simbolo.EnumTipoDato.DOUBLE, (Double)operadorIzq.obtenerValor(ent).valor == (Double)operadorDer.obtenerValor(ent).valor);
            }
            else if (tipo == Tipo_operacion.MOD)
            {
                return new Literal(Simbolo.EnumTipoDato.DOUBLE, (Double)operadorIzq.obtenerValor(ent).valor % (Double)operadorDer.obtenerValor(ent).valor);
            }
            else if (tipo == Tipo_operacion.MENOR_QUE)
            {
                Expresion izquierda = operadorIzq.obtenerValor(ent);
                Expresion derecha = operadorDer.obtenerValor(ent);
                return new Literal(Simbolo.EnumTipoDato.DOUBLE, (Double)operadorIzq.obtenerValor(ent).valor < (Double)operadorDer.obtenerValor(ent).valor);
            }
            else if (tipo == Tipo_operacion.MENOR_IGUAL_QUE)
            {
                return new Literal(Simbolo.EnumTipoDato.DOUBLE, (Double)operadorIzq.obtenerValor(ent).valor <= (Double)operadorDer.obtenerValor(ent).valor);
            }
            else if (tipo == Tipo_operacion.MAYOR_IGUAL_QUE)
            {
                return new Literal(Simbolo.EnumTipoDato.DOUBLE, (Double)operadorIzq.obtenerValor(ent).valor >= (Double)operadorDer.obtenerValor(ent).valor);
            }
            return null;
        }

        public override StringBuilder Traducir(Entorno ent, StringBuilder str)
        {
            StringBuilder temp = new StringBuilder();
            StringBuilder tempDere = new StringBuilder();
            if (tipo == Tipo_operacion.DIVISION)
            {
                return str.Append(operadorIzq.Traducir(ent, temp).ToString() + " / " + operadorDer.Traducir(ent, tempDere).ToString());
            }
            else if (tipo == Tipo_operacion.MULTIPLICACION)
            {
                return str.Append(operadorIzq.Traducir(ent, temp).ToString() + " * " + operadorDer.Traducir(ent, tempDere).ToString());
            }
            else if (tipo == Tipo_operacion.CADENA)
            {
                return str.Append("'" + valor.ToString()+"'");
            }
            else if (tipo == Tipo_operacion.RESTA)
            {
                return str.Append(operadorIzq.Traducir(ent, temp).ToString() + " - " + operadorDer.Traducir(ent, tempDere).ToString());
            }
            else if (tipo == Tipo_operacion.POTENCIA)
            {
                return str.Append(operadorIzq.Traducir(ent, temp).ToString() + " ^ " + operadorDer.Traducir(ent, tempDere).ToString());
            }
            else if (tipo == Tipo_operacion.SUMA)
            {
                return str.Append(operadorIzq.Traducir(ent, temp).ToString() + " + " + operadorDer.Traducir(ent, tempDere).ToString());
            }
            else if (tipo == Tipo_operacion.NEGATIVO)
            {
                return str.Append("-" + operadorIzq.Traducir(ent, temp).ToString());
            }
            else if (tipo == Tipo_operacion.NUMERO)
            {
                return str.Append(valor.ToString());
            }
            else if (tipo == Tipo_operacion.TRUE)
            {
                return str.Append("true");
            }
            else if (tipo == Tipo_operacion.FALSE)
            {
                return str.Append("false");
            }
            else if (tipo == Tipo_operacion.IDENTIFICADOR)
            {
                return str.Append(valor.ToString());
            }
            /*
             * 
             *      OPERACIONES LOGICAS
             * 
             */
            else if (tipo == Tipo_operacion.MAYOR_QUE)
            {

                return str.Append(operadorIzq.Traducir(ent, temp).ToString() + " > " + operadorDer.Traducir(ent, tempDere).ToString());
            }
            else if (tipo == Tipo_operacion.DIFERENCIACION)
            {
                return str.Append(operadorIzq.Traducir(ent, temp).ToString() + " <> " + operadorDer.Traducir(ent, tempDere).ToString());
            }
            else if (tipo == Tipo_operacion.AND)
            {
                return str.Append(operadorIzq.Traducir(ent, temp).ToString() + " and " + operadorDer.Traducir(ent, tempDere).ToString());
            }
            else if (tipo == Tipo_operacion.OR)
            {
                return str.Append(operadorIzq.Traducir(ent, temp).ToString() + " or " + operadorDer.Traducir(ent, tempDere).ToString());
            }
            else if (tipo == Tipo_operacion.XOR)
            {
                return str.Append(operadorIzq.Traducir(ent, temp).ToString() + " > " + operadorDer.Traducir(ent, tempDere).ToString());
            }
            else if (tipo == Tipo_operacion.DIFERENTE)
            {
                return str.Append(operadorIzq.Traducir(ent, temp).ToString());
            }
            else if (tipo == Tipo_operacion.IGUAL_QUE)
            {
                return str.Append(operadorIzq.Traducir(ent, temp).ToString() + " = " + operadorDer.Traducir(ent, tempDere).ToString());
            }
            else if (tipo == Tipo_operacion.MENOR_QUE)
            {
                return str.Append(operadorIzq.Traducir(ent, temp).ToString() + " < " + operadorDer.Traducir(ent, tempDere).ToString());
            }
            else if (tipo == Tipo_operacion.MOD)
            {
                return str.Append(operadorIzq.Traducir(ent, temp).ToString() + " % " + operadorDer.Traducir(ent, tempDere).ToString());
            }
            else if (tipo == Tipo_operacion.MENOR_IGUAL_QUE)
            {
                return str.Append(operadorIzq.Traducir(ent, temp).ToString() + " <= " + operadorDer.Traducir(ent, tempDere).ToString());
            }
            else if (tipo == Tipo_operacion.MAYOR_IGUAL_QUE)
            {
                return str.Append(operadorIzq.Traducir(ent, temp).ToString() + " >= " + operadorDer.Traducir(ent, tempDere).ToString());
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
            CONCATENACION,
            MAYOR_QUE,
            MENOR_QUE,
            IGUAL_QUE,
            AND,
            MOD,
            OR,
            TRUE,
            FALSE,
            XOR,
            DIFERENTE,
            MENOR_IGUAL_QUE,
            MAYOR_IGUAL_QUE,
            DIFERENCIACION
        }
    }
}
