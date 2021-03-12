using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Analizadores;
using Proyecto1_Compi2.Entornos;
using Proyecto1_Compi2.Expresiones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class For : Instruccion
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
        public Retornar Ejecutar(Entorno ent, string Ambito, Sintactico AST)
        {
            bool seguirFor = true;
            Simbolo temp = ent.obtener(id,ent); 
            Expresion valorNuevo = valorInicio.obtenerValor(ent);
            Simbolo sim = new Simbolo(temp.tipo, valorNuevo.valor,id,temp.ambito,temp.referencia_const);
            ent.setVariable(id,sim,ent);
            Arimetica condicion = new Arimetica(new Arimetica(id,Arimetica.Tipo_operacion.IDENTIFICADOR),valorFin,Arimetica.Tipo_operacion.MENOR_QUE);
            Aumento aumento = new Aumento(id);
            while ((Boolean)condicion.obtenerValor(ent).valor  && seguirFor)
            {
                foreach (Instruccion ins in listaInstrucciones)
                {
                    Retornar contenido = ins.Ejecutar(ent,Ambito,AST);
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
                aumento.Ejecutar(ent,Ambito,AST);
            }
            return new Retornar();
        }

        public StringBuilder TraducirInstr(Entorno ent, StringBuilder str, string Ambito)
        {
            StringBuilder temp = new StringBuilder();
            str.Append("for " + id + ":= " + valorInicio.Traducir(ent,temp) +  " to " + valorFin.Traducir(ent,temp.Clear()) +" do \n\tbegin" );
            temp.Clear();
            foreach (Instruccion ins in listaInstrucciones) {
                temp = ins.TraducirInstr(ent,temp,Ambito);
                temp.Append("\n");
            }
            str.Append("\t" + temp.ToString() + "\nend;");
            str.Append("\n");
            return str;
        }
    }
}
