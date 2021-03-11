using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Analizadores;
using Proyecto1_Compi2.Entornos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class Instancia_Type : Instruccion
    {
        public String nombreObjeto;
        public String nombreType;
        public Entorno entObjeto;
        public Instancia_Type(String nombre, String nombreType)
        {
            this.nombreObjeto = nombre;
            this.nombreType = nombreType;
            this.entObjeto = new Entorno(null);
        }
        public Retornar Ejecutar(Entorno ent, string Ambito, Sintactico AST)
        {
            //entObjeto.anterior = ent;
            Type_Object typeObj = Singleton.getInstance().getType(nombreType);
            //this.entObjeto = typeObj.entObj;
            if (typeObj == null)
            {
                Form1.salidaConsola.AppendText("El type no existe");
                return null;
            }
            else
            {
                ent.Insertar(nombreObjeto, new Simbolo(Simbolo.EnumTipoDato.OBJETO_TYPE, this, nombreObjeto, Ambito, ""));
            }            
            return new Retornar();
        }
        public StringBuilder TraducirInstr(Entorno ent, StringBuilder str, string Ambito)
        {
            throw new NotImplementedException();
        }
    }
}
