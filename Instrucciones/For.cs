﻿using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Entornos;
using Proyecto1_Compi2.Expresiones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class For : Abstracto.Instruccion
    {
        String id;
        Expresion valorInicio;
        Expresion valorFin;
        LinkedList<Instruccion> listaInstrucciones;
        public For(String id, Expresion valorInicio, Expresion valorFin, LinkedList<Instruccion> listaInstr) {
            this.id = id;
            this.valorInicio = valorInicio;
            this.valorFin = valorFin;
            listaInstrucciones = listaInstr;
        }
        public Retornar Ejecutar(Entorno ent, string Ambito)
        {
            bool seguirFor = true;
            Simbolo temp = ent.obtener(id,ent); 
            Expresion valorNuevo = valorInicio.obtenerValor(ent);
            Simbolo sim = new Simbolo(temp.tipo, valorNuevo.valor,id,temp.ambito);
            ent.setVariable(id,sim,ent);
            Arimetica condicion = new Arimetica(new Arimetica(id,Arimetica.Tipo_operacion.IDENTIFICADOR),valorFin,Arimetica.Tipo_operacion.MENOR_QUE);
            Aumento aumento = new Aumento(id);
            while ((Boolean)condicion.obtenerValor(ent).valor  && seguirFor)
            {
                foreach (Instruccion ins in listaInstrucciones)
                {
                    Retornar contenido = ins.Ejecutar(ent,Ambito);
                    if (contenido.isBreak)
                    {
                        seguirFor = false;
                        break;
                    }
                    if (contenido.isContinue)
                    {
                        break;
                    }
                }
                aumento.Ejecutar(ent,Ambito);
            }
            return new Retornar();
        }
    }
}
