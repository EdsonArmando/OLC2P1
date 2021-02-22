using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Proyecto1_Compi2.Reportes
{
    class GraficarAST
    {
        private ParseTreeNode raiz;
        private int noNodo = 0;
        private String text = "digraph lista{ \n rankdir=TB;node[shape = box, style = filled, color = white];\n";
        public GraficarAST(ParseTreeNode raiz) {
            this.raiz = raiz;
        }
        public ParseTreeNode recorrerRaiz(ParseTreeNode raiz)
        {
            text += "<nodo" + raiz.GetHashCode() + "> [label= " + "\"" + raiz.Term.Name.ToUpper() + "\"" + "fillcolor=\"LightBlue\", style =\"filled\", shape=\"box\"];\n ";
            if (raiz.Token != null && raiz.Token.Text.ToString().ToLower() != raiz.Term.Name.ToLower())
            {
                text += "<nodo" + raiz.Token.GetHashCode() + "> [label= " + "\"" + raiz.Token.Text.ToString().ToUpper() + "\"" + "fillcolor=\"LightBlue\", style =\"filled\", shape=\"box\"];\n ";
                text += "<nodo" + raiz.GetHashCode() + "> -> <" + "nodo" + raiz.Token.GetHashCode() + ">\n";
            }
            if (raiz.ChildNodes.Count > 0) {
                for (int i=0;i<raiz.ChildNodes.Count;i++) {     
                    text += "<nodo" + raiz.GetHashCode() + "> -> <" + "nodo" + raiz.ChildNodes.ElementAt(i).GetHashCode() + ">\n";
                    recorrerRaiz(raiz.ChildNodes.ElementAt(i));
                }
            }
            return null;
        }
        public void generarArchivo() {
            StreamWriter archivo = new StreamWriter("C:\\compiladores2\\AST.txt");
            archivo.Write(text);
            archivo.Write("\n}");
            archivo.Close();
        }
    }
}
