using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Entornos
{
    class Simbolo
    {
        public EnumTipoDato tipo;
        public String id;
        public Object valor;
        public string ambito;
        public string referencia_const;
        public Simbolo(EnumTipoDato tipo, Object valor, String id, String ambito,String referencia_const)
        {
            this.id = id;
            this.tipo = tipo;
            this.valor = valor;
            this.ambito = ambito;
            this.referencia_const = referencia_const;
        }
        public Object getValor()
        {
            return this.valor;
        }
        public EnumTipoDato getTipo()
        {
            return this.tipo;
        }
        public enum EnumTipoDato
        {
            CHAR,
            STRING,
            INT,
            ARREGLO,
            DOUBLE,
            BOOLEAN,
            NULL,
            ERROR,
            OBJETO,
            FUNCION,
            CONST
        }
    }
}
