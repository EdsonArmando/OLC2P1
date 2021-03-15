using Proyecto1_Compi2.Abstracto;
using Proyecto1_Compi2.Analizadores;
using Proyecto1_Compi2.Entornos;
using Proyecto1_Compi2.Expresiones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Instrucciones
{
    class Asignacion : Instruccion
    {
        String id;
        Expresion valor;
        private Expresion posX;
        private Expresion posY;
        private Expresion posZ;

        public Asignacion(String id, Expresion posX, Expresion posy, Expresion posZ, Expresion val)
        {
            this.id = id;
            this.posX = posX;
            this.posY = posy;
            this.posZ = posZ;
            this.valor = val;
        }
        public Asignacion(String id, Expresion valor) {
            this.id = id;
            this.valor = valor;
        }
        public Retornar Ejecutar(Entorno ent, string Ambito, Sintactico AST)
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
                if (resultado != null && posX == null)
                {
                    if (sim.referencia_const.ToLower() == "const")
                    {
                        Form1.salidaConsola.AppendText("Una variable CONST no se puede modificar !!!");                       
                    }
                    else {
                        if (resultado.tipo == Simbolo.EnumTipoDato.ARRAY) 
                        {
                            Literal temp = (Literal)resultado;
                            ent.setVariable(id, new Simbolo(temp.tipo, temp.valor, temp.id, temp.ambito, temp.referencia_const, temp.posicion_X, temp.posicion_Y, temp.posicion_Z, temp.tipoItem),ent); // Guardo la variable
                        }
                        else {
                            ent.setVariable(id, new Simbolo(sim.tipo, resultado.valor, id, sim.ambito, sim.referencia_const), ent); // Guardo la variable
                            ent.recorrer(ent);
                        }                        
                    }                    
                }
                //Arreglo de una dimension
                else if (posX != null && posY == null && posZ == null)
                {
                    Expresion[] temp = (Expresion[])sim.valor;
                    int[] x = sim.posicion_X;
                    int calcularPos = int.Parse(posX.obtenerValor(ent).valor.ToString()) - x[0];
                    if (resultado.tipo == Simbolo.EnumTipoDato.OBJETO_TYPE)
                    {
                        Type_Object val = (Type_Object)resultado.valor;                        
                        Type_Object ultima = (Type_Object)val.Clone();                                                    
                        Entorno entornoTemp = new Entorno(null);
                        foreach (String key in ultima.entObjeto.tablaSimbolos.Keys) {
                            entornoTemp.tablaSimbolos.Add(key,ultima.entObjeto.tablaSimbolos[key]);
                        }
                        ultima.entObjeto = entornoTemp;                       
                        Literal n = new Literal(resultado.tipo, ultima.Clone());
                        temp[calcularPos] = n;
                        sim.valor = temp;
                        ent.setVariable(id, sim, ent);
                    }
                    else {
                        temp[calcularPos] = resultado;
                        sim.valor = temp;
                        ent.setVariable(id, sim, ent);
                    }                    
                }
                //Arreglo de dos dimensiones
                else if (posX != null && posY != null && posZ == null)
                {
                    Expresion[] temp = (Expresion[])sim.valor;
                    int[] x = sim.posicion_X;
                    int[] y = sim.posicion_Y;
                    int calcularPosX = int.Parse(posX.obtenerValor(ent).valor.ToString()) - x[0];
                    int calcularPosY = int.Parse(posY.obtenerValor(ent).valor.ToString()) - y[0];
                    int posicionTotal = calcularPosX * ((y[1]- y[0])+1) + calcularPosY;
                    if (resultado.tipo == Simbolo.EnumTipoDato.OBJETO_TYPE)
                    {
                        Type_Object val = (Type_Object)resultado.valor;
                        Type_Object ultima = (Type_Object)val.Clone();
                        Entorno entornoTemp = new Entorno(null);
                        foreach (String key in ultima.entObjeto.tablaSimbolos.Keys)
                        {
                            entornoTemp.tablaSimbolos.Add(key, ultima.entObjeto.tablaSimbolos[key]);
                        }
                        ultima.entObjeto = entornoTemp;
                        Literal n = new Literal(resultado.tipo, ultima.Clone());
                        temp[posicionTotal] = n;
                        sim.valor = temp;
                        ent.setVariable(id, sim, ent);
                    }
                    else
                    {
                        temp[posicionTotal] = resultado;
                        sim.valor = temp;
                        ent.setVariable(id, sim, ent);
                    }

                }
                //Arreglo de 3 dimensiones
                else if (posX != null && posY != null && posZ != null)
                {
                    Expresion[] temp = (Expresion[])sim.valor;
                    int[] x = sim.posicion_X;
                    int[] y = sim.posicion_Y;
                    int[] z = sim.posicion_Z;
                    int calcularPosX = int.Parse(posX.obtenerValor(ent).valor.ToString()) - x[0];
                    int calcularPosY = int.Parse(posY.obtenerValor(ent).valor.ToString()) - y[0];
                    int calcularPosZ = int.Parse(posZ.obtenerValor(ent).valor.ToString()) - z[0];
                    int posicionTotal = calcularPosZ + ((z[1] - z[0])+1)*(calcularPosY + ((y[1] - y[0])+1)*calcularPosX);
                    if (resultado.tipo == Simbolo.EnumTipoDato.OBJETO_TYPE)
                    {
                        Type_Object val = (Type_Object)resultado.valor;
                        Type_Object ultima = (Type_Object)val.Clone();
                        Entorno entornoTemp = new Entorno(null);
                        foreach (String key in ultima.entObjeto.tablaSimbolos.Keys)
                        {
                            entornoTemp.tablaSimbolos.Add(key, ultima.entObjeto.tablaSimbolos[key]);
                        }
                        ultima.entObjeto = entornoTemp;
                        Literal n = new Literal(resultado.tipo, ultima.Clone());
                        temp[posicionTotal] = n;
                        sim.valor = temp;
                        ent.setVariable(id, sim, ent);
                    }
                    else
                    {
                        temp[posicionTotal] = resultado;
                        sim.valor = temp;
                        ent.setVariable(id, sim, ent);
                    }
                }
            }
            return new Retornar();
        }

        public StringBuilder TraducirInstr(Entorno ent, StringBuilder str, string Ambito)
        {
            StringBuilder expr = new StringBuilder();
            if (posX == null && posY == null && posZ == null)
            {
                str.Append(id + " := " + valor.Traducir(ent, expr) + ";");
                return str;
            }
            if (posX != null && posY == null && posZ == null)
            {
                str.Append(id + " [" + posX.Traducir(ent,expr) + "]" + ":= " + valor.Traducir(ent, expr.Clear()) + ";");
                return str;
            }
            //Arreglo de dos dimensiones
            else if (posX != null && posY != null && posZ == null)
            {
                str.Append(id + " [" + posX.Traducir(ent, expr) + "," + posY.Traducir(ent, expr.Clear()) + "]" + ":= " + valor.Traducir(ent, expr.Clear()) + ";");
                return str;

            }
            //Arreglo de 3 dimensiones
            else if (posX != null && posY != null && posZ != null)
            {
                str.Append(id + " [" + posX.Traducir(ent, expr) + "," + posY.Traducir(ent, expr.Clear()) + "," + posZ.Traducir(ent, expr.Clear()) + "]" + ":= " + valor.Traducir(ent, expr.Clear()) + ";");
                return str;

            }
            return str;
        }
    }
}
