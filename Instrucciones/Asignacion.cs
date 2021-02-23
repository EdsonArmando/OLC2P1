using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class Asignacion : Abstracto.Instruccion
    {
        String id;
        Expresion valor;
        public Asignacion(String id, Expresion valor) {
            this.id = id;
            this.valor = valor;
        }
        public Retornar Ejecutar(Entorno ent, string Ambito)
        {
            /* 
             * 
             * Si se trata de un Return
             */
            if (id.ToLower() == Ambito.ToLower())
            {
                Retornar retornar = new Retornar();
                retornar.isReturn = true;
                retornar.valor = valor.obtenerValor(ent);
                return retornar;
            }
            else {
                Expresion resultado = valor.obtenerValor(ent);
                Simbolo sim = ent.obtener(id, ent);
                if (resultado != null)
                {
                    if (sim.referencia_const.ToLower() == "const")
                    {
                        Form1.salidaConsola.AppendText("Una variable CONST no se puede modificar !!!");                       
                    }
                    else {
                        ent.setVariable(id, new Simbolo(sim.tipo, resultado.valor, id, sim.ambito, sim.referencia_const), ent); // Guardo la variable
                    }                    
                }
            }
            return new Retornar();
        }
    }
}
