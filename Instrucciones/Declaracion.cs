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
        public String tipoDinamico;

        public Declaracion(Simbolo.EnumTipoDato tipo, String nombre, Expresion expresion, int fila, int columna,String tip)
        {
            this.tipoVariable = tipo;
            this.nombreVariable = nombre;
            this.expresion = expresion;
            this.tipoDinamico = tip;
            this.fila = fila;
            this.columna = columna;
        }
        public Declaracion(Simbolo.EnumTipoDato tipo, LinkedList<Expresion> valores, Expresion expresion, int fila, int columna, String esreferencia,String tip)
        {
            this.tipoVariable = tipo;
            this.tipoDinamico = tip;
            this.expresion = expresion;
            this.fila = fila;
            this.columna = columna;
            this.variables = valores;
            this.esReferencia_const = esreferencia;
        }
        public Declaracion(Simbolo.EnumTipoDato tipo, String nombre, Expresion expresion, int fila, int columna,String esReferencia_const, String tipoDi)
        {
            this.tipoVariable = tipo;
            this.tipoDinamico = tipoDi;
            this.nombreVariable = nombre;
            this.expresion = expresion;
            this.fila = fila;
            this.columna = columna;
            this.esReferencia_const = esReferencia_const;
        }
        public Declaracion(Simbolo.EnumTipoDato tipo, LinkedList<Expresion> valores, Expresion expresion, int fila, int columna, String esReferencia_const,String nameArray, String tipoDi)
        {
            this.tipoVariable = tipo;
            this.variables = valores;
            this.expresion = expresion;
            this.fila = fila;
            this.tipoDinamico = tipoDi;
            this.columna = columna;
            this.esReferencia_const = esReferencia_const;
            this.nameArra = nameArray;
        }
        public Declaracion(Simbolo.EnumTipoDato tipo, String nombre, Expresion expresion, int fila, int columna, String esReferencia_const, String nameArray,String tipoDi)
        {
            this.tipoVariable = tipo;
            this.nombreVariable = nombre;
            this.expresion = expresion;
            this.fila = fila;
            this.columna = columna;
            this.tipoDinamico = tipoDi;
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
                                temp2.Ejecutar(ent,ambito,AST);
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

        public StringBuilder TraducirInstr(Entorno ent, StringBuilder str, string Ambito)
        {
            if (esReferencia_const.ToLower() == "const") {
                StringBuilder uno = new StringBuilder();
                str.Append("const " + nombreVariable + " =" + expresion.Traducir(ent,uno) + ";");
                str.Append("\n");
                return str;
            }
            if (expresion == null)
            {
                if (variables == null)
                {
                    if (nameArra == null || nameArra == "")
                    {
                        str.Append("var " + nombreVariable + " :" + tipoDinamico.ToUpper() + " ;");
                        str.Append("\n");
                    }
                    else
                    {
                        str.Append("var " + nombreVariable + " :" + nameArra.ToUpper() + " ;");
                        str.Append("\n");
                    }
                }
                else
                {
                    StringBuilder temp = new StringBuilder();

                    str.Append("Var ");
                    foreach (Id expr in variables)
                    {
                        temp = expr.Traducir(ent, temp);
                        temp.Append(",");
                    }
                    temp.Remove(temp.Length - 1, 1);
                    if (nameArra == null || nameArra == "")
                    {
                        str.Append(temp.ToString() + " : " + tipoDinamico.ToUpper() + ";");          
                    }
                    else
                    {
                        str.Append(temp.ToString() + " : " + nameArra.ToUpper() + ";");
                    }
                    temp.Clear();
                }
            }
            else {
                if (variables == null)
                {
                    StringBuilder result = new StringBuilder();
                    if (nameArra == null || nameArra == "")
                    {
                        str.Append("var " + nombreVariable + " :" + tipoDinamico.ToUpper() + " =" + expresion.Traducir(ent, result) + " ;");
                        str.Append("\n");
                    }
                    else
                    {
                        str.Append("var " + nombreVariable + " :" + nameArra.ToUpper() + " =" + expresion.Traducir(ent, result) + " ;");
                        str.Append("\n");
                    }
                }
                else
                {
                    StringBuilder temp = new StringBuilder();
                    StringBuilder result = new StringBuilder();
                    str.Append("Var ");
                    foreach (Id expr in variables)
                    {
                        temp = expr.Traducir(ent, temp);
                        temp.Append(",");
                    }
                    temp.Remove(temp.Length - 1, 1);
                    if (nameArra == null || nameArra == "")
                    {
                        
                        str.Append(temp.ToString() + " : " + tipoDinamico.ToUpper() + " =" + expresion.Traducir(ent,result) + ";");  
                    }
                    else
                    {
                        str.Append(temp.ToString() + " : " + nameArra.ToUpper() + " =" + expresion.Traducir(ent, result.Clear()) + ";");
                    }
                    temp.Clear();
                }
            }            
            return str.Append("") ;
        }
    }
}
