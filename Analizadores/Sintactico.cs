using Irony.Parsing;
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
            if (raiz == null)
            {
                for (int i = 0; i < arbol.ParserMessages.Count(); i++)
                {
                    Form1.salidaConsola.AppendText(arbol.ParserMessages.ElementAt(i).Level.ToString() + "\n");
                }
                return;
            }
        }
    }
}
