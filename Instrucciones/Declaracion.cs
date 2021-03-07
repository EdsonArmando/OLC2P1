using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Analizadores;
using Proyecto1_Compi2.Entornos;
using Proyecto1_Compi2.Expresiones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class Declaracion : Instruccion
    {
        public Simbolo.EnumTipoDato tipoVariable;
        public LinkedList<Expresion> variables;
        public String nombreVariable;
        public Expresion expresion;
        public int fila, columna;
        public String esReferencia_const;
        public String nameArra;

        public Declaracion(Simbolo.EnumTipoDato tipo, String nombre, Expresion expresion, int fila, int columna)
        {
            this.tipoVariable = tipo;
            this.nombreVariable = nombre;
            this.expresion = expresion;
            this.fila = fila;
            this.columna = columna;
        }
        public Declaracion(Simbolo.EnumTipoDato tipo, LinkedList<Expresion> valores, Expresion expresion, int fila, int columna, String esreferencia)
        {
            this.tipoVariable = tipo;
            this.expresion = expresion;
            this.fila = fila;
            this.columna = columna;
            this.variables = valores;
            this.esReferencia_const = esreferencia;
        }
        public Declaracion(Simbolo.EnumTipoDato tipo, String nombre, Expresion expresion, int fila, int columna,String esReferencia_const)
        {
            this.tipoVariable = tipo;
            this.nombreVariable = nombre;
            this.expresion = expresion;
            this.fila = fila;
            this.columna = columna;
            this.esReferencia_const = esReferencia_const;
        }
        public Declaracion(Simbolo.EnumTipoDato tipo, LinkedList<Expresion> valores, Expresion expresion, int fila, int columna, String esReferencia_const,String nameArray)
        {
            this.tipoVariable = tipo;
            this.variables = valores;
            this.expresion = expresion;
            this.fila = fila;
            this.columna = columna;
            this.esReferencia_const = esReferencia_const;
            this.nameArra = nameArray;
        }
        public Declaracion(Simbolo.EnumTipoDato tipo, String nombre, Expresion expresion, int fila, int columna, String esReferencia_const, String nameArray)
        {
            this.tipoVariable = tipo;
            this.nombreVariable = nombre;
            this.expresion = expresion;
            this.fila = fila;
            this.columna = columna;
            this.esReferencia_const = esReferencia_const;
            this.nameArra = nameArray;
        }
        public Retornar Ejecutar(Entorno ent,String ambito, Sintactico AST)
        {

            //Resuelvo la expresión que le quiero asignar a la variable
            if (expresion != null && variables == null)
            {
                Expresion resultado = expresion.obtenerValor(ent);
                if (resultado.tipo == Simbolo.EnumTipoDato.ARRAY)
                {
                    Literal temp = (Literal)resultado;
                    ent.Insertar(this.nombreVariable, new Simbolo(temp.tipo, temp.valor, temp.id, temp.ambito,temp.referencia_const, temp.posicion_X, temp.posicion_Y, temp.posicion_Z, temp.tipoItem)); // Guardo la variable
                }
                else {
                    ent.Insertar(this.nombreVariable, new Simbolo(this.tipoVariable, resultado.valor, nombreVariable, ambito, esReferencia_const)); // Guardo la variable
                }                
            } else if (variables != null) {
                
                foreach (Id expr in variables) {
                    if (expresion == null)
                    {
                        if (nameArra != null)
                        {
                            if (Singleton.getInstance().existType(nameArra))
                            {
                                //Instancia_Type temp = new Instancia_Type(expr.id.ToString().ToLower(), nameArra);
                                //temp.Ejecutar(ent, ambito,AST);
                                Type_Object temp2 = Singleton.getInstance().getType(nameArra);
                                ent.Insertar(expr.id.ToString().ToLower(), new Simbolo(Simbolo.EnumTipoDato.OBJETO_TYPE, temp2, nameArra, ambito, ""));
                            }
                            else
                            {
                                Simbolo sim = ent.obtener(nameArra, ent);
                                ent.Insertar(expr.id.ToString(), sim); // Guardo la variable
                            }
                        }
                        else {
                            ent.Insertar(expr.id.ToString(), new Simbolo(this.tipoVariable, "", expr.id.ToString(), ambito, esReferencia_const)); // Guardo la variable 
                        }
                        
                    }
                    else {
                        Expresion resultado = expresion.obtenerValor(ent);
                        if (resultado.tipo == Simbolo.EnumTipoDato.ARRAY)
                        {
                            Literal temp = (Literal)resultado;
                            ent.Insertar(expr.id.ToString(), new Simbolo(temp.tipo, temp.valor, temp.id, temp.ambito, temp.referencia_const, temp.posicion_X, temp.posicion_Y, temp.posicion_Z, temp.tipoItem)); // Guardo la variable
                        }
                        else
                        {
                            ent.Insertar(expr.id.ToString(), new Simbolo(this.tipoVariable, resultado.valor, expr.id.ToString(), ambito, esReferencia_const)); // Guardo la variable                         
                        }
                    }
                      
                }
            }
            else
            {
                if (nameArra != null)
                {
                    //Verifico si es un type o un ARRAY
                    if (Singleton.getInstance().existType(nameArra))
                    {
                        Type_Object temp = Singleton.getInstance().getType(nameArra);
                        ent.Insertar(nombreVariable, new Simbolo(Simbolo.EnumTipoDato.OBJETO_TYPE, temp, nameArra, ambito, ""));
                        //Creo la Instancia del Type
                        //Instancia_Type temp = new Instancia_Type(nombreVariable,nameArra);
                        //temp.Ejecutar(ent,ambito, AST);
                    }
                    else {
                        Simbolo sim = ent.obtener(nameArra, ent);
                        ent.Insertar(this.nombreVariable, sim); // Guardo la variable
                    }                    
                }
                else {
                    ent.Insertar(this.nombreVariable, new Simbolo(tipoVariable, null, nombreVariable, ambito, esReferencia_const)); // Guardo la variable
                }                
            }
             return new Retornar();
        }
        public void setExpresion(Expresion expr) {
            this.expresion = expr;
        }
    }
}
