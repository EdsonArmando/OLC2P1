using Proyecto1_Compi2.Instrucciones;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Entornos
{
    class Entorno
    {
        public Hashtable tablaSimbolos;
        public Entorno anterior;
        public Entorno(Entorno entornoAnterior) : base()
        {
            this.tablaSimbolos = new Hashtable();
            this.anterior = entornoAnterior;
            // llamada del constructor de la clase padre
        }
        //Recorrer Tabla
        public void recorrer(Entorno ent) {
            Simbolo sim;
            foreach (String id in ent.tablaSimbolos.Keys) {
                sim = (Simbolo)ent.tablaSimbolos[id.ToLower()];
            }
        }
        //Insertar Types
        public bool existeVariable(String id)
        {
            return this.tablaSimbolos.ContainsKey(id.ToLower());
        }
        //Obtener la variable del entorno
        public Simbolo obtener(string id, Entorno entorno)
        {
            Simbolo sim = null;
            if (entorno.tablaSimbolos.ContainsKey(id.ToLower()))
            {
                sim = (Simbolo)entorno.tablaSimbolos[id.ToLower()];
                return sim;
            }
            else if (entorno.anterior != null)
            {
                sim = obtener(id.ToLower(),entorno.anterior);
                return sim;
            }
            else {
                Form1.salidaConsola.AppendText("La variable '" + id + "' NO existe");
                return null;
            }
            
        }
        public void printVal() {
            foreach (Simbolo sim in this.tablaSimbolos) {
                foreach (String key in tablaSimbolos.Keys)
                {
                    var value = tablaSimbolos[key];
                }
            }
        }
        public void setVariable(string nombre, Simbolo valor,Entorno ent) {
            if (ent.tablaSimbolos.ContainsKey(nombre.ToLower()))
            {
                ent.tablaSimbolos.Remove(nombre);
                ent.tablaSimbolos.Add(nombre, valor);
                return;
            }
            else if (ent.anterior != null)
            {
                setVariable(nombre.ToLower(),valor,ent.anterior);
                return;
            }
        }
        public void Insertar(string nombre, Simbolo valor)
        {
            if (this.tablaSimbolos.ContainsKey(nombre.ToLower()))
            {
                Form1.salidaConsola.AppendText("La variable '" + nombre + "' YA existe");

                return;
            }
            this.tablaSimbolos.Add(nombre.ToLower(), valor);

        }
    }
}
