﻿using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class Break : Abstracto.Instruccion
    {
        public Retornar Ejecutar(Entorno ent, string Ambito)
        {
            Retornar ret = new Retornar();
            ret.isBreak = true;
            return ret;
        }
    }
}
