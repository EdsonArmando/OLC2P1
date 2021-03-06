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
        public LinkedList<Abstracto.Instruccion> listFunciones;
        public LinkedList<Abstracto.Instruccion> List_types;
        public Entorno anterior;
        public Entorno(Entorno entornoAnterior) : base()
        {
            this.tablaSimbolos = new Hashtable();
            this.List_types = new LinkedList<Abstracto.Instruccion>();
            this.anterior = entornoAnterior;
            // llamada del constructor de la clase padre
        }
        //Insertar Types
        public void insertType(String nombre, Type_Object typ)
        {
            this.List_types.AddLast(typ);
        }
        public Type_Object obtenerType(String nombre, Entorno ent)
        {
            Type_Object temp = null;
            foreach (Type_Object item in ent.List_types)
            {
                if (item.nombreType.ToLower().Equals(nombre.ToLower()))
                {
                    temp = item;
                    return temp;
                }              
            }
            if (ent.anterior != null) {
                temp = obtenerType(nombre,ent.anterior);
                return temp;
            }
            return null;
        }
        //Existe Type
        public bool existType(String nombre, Entorno ent) {
            bool valor = false;
            foreach (Type_Object item in ent.List_types)
            {
                if (item.nombreType.ToLower().Equals(nombre.ToLower()))
                {
                    valor = true;
                    return valor;
                }

            }
            if (ent.anterior != null) {
                valor = existType(nombre,ent.anterior);
                return valor;
            }
            return false;
        }
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
