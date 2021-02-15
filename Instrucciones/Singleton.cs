using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class Singleton
    {
        private static Singleton singleton;
        public Hashtable funciones;
        private Singleton()
        {
            funciones = new Hashtable();
            /*Meter todos mis metodos propios del lenguaje*/
        }
        public static Singleton getInstance()
        {
            if (singleton == null)
            {
                singleton = new Singleton();
            }
            return singleton;
        }
        public void limpiarEntorno()
        {
            funciones.Clear();
        }
        public bool putFuncion(Abstracto.Instruccion funcion, String id)
        {
            if (funciones.ContainsKey(id.ToLower()))
            {
                Form1.salidaConsola.AppendText("La funcion ya existe\n");
                return false;
            }
            funciones.Add(id, funcion);
            return true;
        }
        public Abstracto.Instruccion getFuncion(String id)
        {
            if (funciones.ContainsKey(id.ToLower()))
            {
                Abstracto.Instruccion temp;
                temp = (Abstracto.Instruccion)funciones[id];
                return temp;
            }
            Form1.salidaConsola.AppendText("No se encontro la Funcion \n");
            return null;
        }
        public Hashtable getTabla()
        {
            return this.funciones;
        }
    }
}
