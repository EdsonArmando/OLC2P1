using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Analizadores;
using Proyecto1_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class Decremento : Instruccion
    {
        String id;
        public Decremento(String id)
        {
            this.id = id;
        }
        public Retornar Ejecutar(Entorno ent, string Ambito, Sintactico AST)
        {
            Simbolo variable = ent.obtener(id.ToLower(), ent);
            int valAument = int.Parse(variable.valor.ToString());
            valAument = valAument - 1;
            Simbolo sim = new Simbolo(variable.tipo, (object)valAument, id.ToLower() ,variable.ambito, variable.referencia_const);
            ent.setVariable(id.ToLower(), sim, ent);
            return new Retornar();
        }
        public StringBuilder TraducirInstr(Entorno ent, StringBuilder str, string Ambito)
        {
            throw new NotImplementedException();
        }
    }
}
