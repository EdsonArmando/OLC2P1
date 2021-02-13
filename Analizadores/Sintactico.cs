using Irony.Parsing;
using Proyecto1_Compi2.Instrucciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Proyecto1_Compi2.Analizadores
{
    class Sintactico
    {
        public void analizar(String entrada)
        {
            Console.WriteLine(entrada);
            Gramatica gramatica = new Gramatica();
            LanguageData lenguaje = new LanguageData(gramatica);
            Parser parser = new Parser(lenguaje);
            ParseTree arbol = parser.Parse(entrada);
            ParseTreeNode raiz = arbol.Root;
            if (raiz == null || arbol.ParserMessages.Count()>0)
            {
                for (int i = 0; i < arbol.ParserMessages.Count(); i++)
                {
                    Form1.salidaConsola.AppendText(arbol.ParserMessages.ElementAt(i).Level.ToString() + "\n");
                }
                return;
            }
            else
            {
                Form1.salidaConsola.AppendText("Se analizo correctamente\n");
                LinkedList<Abstracto.Instruccion> AST = instrucciones(raiz.ChildNodes.ElementAt(0));
            }
        }
        //Recorrer Raiz
        private LinkedList<Abstracto.Instruccion> instrucciones(ParseTreeNode actual)
        {
            LinkedList<Abstracto.Instruccion> instrucciones = new LinkedList<Abstracto.Instruccion>();
            for (int i = 0; i < actual.ChildNodes.Count; i++)
            {
                Abstracto.Instruccion nuevo = instruccion(actual.ChildNodes.ElementAt(i),"",null, instrucciones);
            }
            return instrucciones;
        }
        private LinkedList<Abstracto.Expresion> listaExpresiones(ParseTreeNode actual)
        {

            return null;
        }
        private LinkedList<Abstracto.Instruccion> listInstr2(ParseTreeNode actual)
        {

            return null;
        }
        /*
       Funcion para logra desanidar las funciones
        */
        private Abstracto.Instruccion instruccion(ParseTreeNode actual,String padre,ParseTreeNode ListaParametrosPadre, LinkedList<Abstracto.Instruccion> instrucciones)
        {
            string tokenOperador = actual.ChildNodes.ElementAt(0).ToString().Split(' ')[0];
            switch (tokenOperador.ToLower())
            {
                /*
                    Desanidamiento de Funciones
                 */
                case "funcion":
                    ParseTreeNode funcion = actual.ChildNodes.ElementAt(0);
                    String funcionHija;
                    Funcion temp;
                    if (padre != "")
                    {
                        funcionHija = padre + "_" + funcion.ChildNodes.ElementAt(1).ToString().Split(' ')[0];
                        Form1.salidaConsola.AppendText(funcionHija  +"\n");
                    }
                    else {
                        funcionHija = funcion.ChildNodes.ElementAt(1).ToString().Split(' ')[0];
                        Form1.salidaConsola.AppendText(funcionHija+"\n");
                    }

                    /*
                        Funcion tiene 13 nodos en el nodo 8 se encuentran la funciones anidadas
                     */
                    if (funcion.ChildNodes.Count == 13)
                    {
                        for (int i = 0; i < funcion.ChildNodes.ElementAt(8).ChildNodes.Count; i++)
                        {
                            Abstracto.Instruccion nuevo = instruccion(funcion.ChildNodes.ElementAt(8).ChildNodes.ElementAt(i), funcionHija, funcion.ChildNodes.ElementAt(3),instrucciones);
                        }
                        temp = new Funcion(funcionHija, listaExpresiones(funcion.ChildNodes.ElementAt(3)), listInstr2(funcion.ChildNodes.ElementAt(10)), listInstr2(funcion.ChildNodes.ElementAt(8)));
                        instrucciones.AddLast(temp);
                        return temp;
                    }
                    else {
                        for (int i = 0; i < funcion.ChildNodes.ElementAt(5).ChildNodes.Count; i++)
                        {
                            Abstracto.Instruccion nuevo = instruccion(funcion.ChildNodes.ElementAt(5).ChildNodes.ElementAt(i), funcionHija, ListaParametrosPadre,instrucciones);
                        }
                        temp = new Funcion(funcionHija, null, listInstr2(funcion.ChildNodes.ElementAt(7)), listInstr2(funcion.ChildNodes.ElementAt(5)));
                        instrucciones.AddLast(temp);
                        return temp;
                    }
                default:
                    return null;
            }
        }
    }
}
