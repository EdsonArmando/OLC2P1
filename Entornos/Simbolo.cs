﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Entornos
{
    class Simbolo
    {
        public EnumTipoDato tipo;
        public String id;
        public Object valor;
        public Simbolo(EnumTipoDato tipo, Object valor, String id)
        {
            this.id = id;
            this.tipo = tipo;
            this.valor = valor;
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
            FUNCION
        }
    }
}
