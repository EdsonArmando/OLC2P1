﻿using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Analizadores;
using Proyecto1_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class Continue : Abstracto.Instruccion
    {
        public Retornar Ejecutar(Entorno ent, string Ambito, Sintactico AST)
        {
            Retornar ret = new Retornar();
            ret.isContinue = true;
            return ret;
        }
    }
}
