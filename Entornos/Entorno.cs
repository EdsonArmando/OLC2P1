using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Entornos
{
    class Entorno
    {
        public Hashtable tablaSimbolos;
        public LinkedList<Abstracto.Instruccion> listFunciones;
        Entorno anterior;
        public Entorno(Entorno entornoAnterior) : base()
        {
            this.tablaSimbolos = new Hashtable();
            this.listFunciones = new LinkedList<Abstracto.Instruccion>();
            this.anterior = entornoAnterior;
            // llamada del constructor de la clase padre
        }
        public bool existeVariable(String id)
        {
            return this.tablaSimbolos.ContainsKey(id);
        }
        //Obtener la variable del entorno
        public Simbolo obtener(string id, Entorno entorno)
        {
            Simbolo sim = null;
            if (entorno.tablaSimbolos.ContainsKey(id))
            {
                sim = (Simbolo)entorno.tablaSimbolos[id];
                return sim;
            }
            else if (entorno.anterior != null)
            {
                sim = obtener(id,entorno.anterior);
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
            if (this.tablaSimbolos.ContainsKey(nombre))
            {
                ent.tablaSimbolos.Remove(nombre);
                ent.tablaSimbolos.Add(nombre, valor);
                return;
            }
            else if (ent.anterior != null)
            {
                setVariable(nombre,valor,ent.anterior);
                return;
            }
        }
        public void Insertar(string nombre, Simbolo valor)
        {
            if (this.tablaSimbolos.ContainsKey(nombre))
            {
                Form1.salidaConsola.AppendText("La variable '" + nombre + "' YA existe");

                return;
            }
            this.tablaSimbolos.Add(nombre, valor);

        }
    }
}
