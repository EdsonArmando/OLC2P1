﻿using Proyecto1_Compi2.Analizadores;
using System;
using System.Collections.Generic;
using System.Text;
using static Proyecto1_Compi2.Entornos.Simbolo;

namespace Proyecto1_Compi2.Abstracto
{
    abstract class Expresion
    {
        public Object valor;
        public int fila, columna;
        public EnumTipoDato tipo;
        abstract public Expresion obtenerValor(Entornos.Entorno ent);
        public abstract EnumTipoDato getTipo();
    }
}
