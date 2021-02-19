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
        private String text = "digraph lista{ \n rankdir=TB;node[shape = box, style = filled, color = white];\n";
        public GraficarAST(ParseTreeNode raiz) {
            this.raiz = raiz;
        }
        public void Graficar() {
            text += "nodo" + raiz.ChildNodes.ElementAt(0).GetHashCode() + "[label= \"LISTA \"fillcolor=\"LightBlue\", style =\"filled\", shape=\"box\"]; \n";
            text += "nodo" + raiz.ChildNodes.ElementAt(0).GetHashCode() + "->" + "nodo12345" + raiz.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).GetHashCode()+ 0 + 1 + "\n";
            for (int i=0;i<raiz.ChildNodes.ElementAt(0).ChildNodes.Count; i++) {
                ParseTreeNode instruccion = raiz.ChildNodes.ElementAt(0).ChildNodes.ElementAt(i);
                try
                {
                    text += "nodo12345" + instruccion.GetHashCode() + i + 1 + "->" + "nodo12345" + raiz.ChildNodes.ElementAt(0).ChildNodes.ElementAt(i + 1).GetHashCode() + (i + 1) + 1 + "\n";
                }
                catch (System.ArgumentOutOfRangeException e)
                {
                    //text += "nodo" + instruccion.GetHashCode() + i + 1 + "->" + "nodo" + raiz.ChildNodes.ElementAt(0).ChildNodes.ElementAt(i).GetHashCode() + "\n";
                }
                text += "nodo12345" + instruccion.GetHashCode() + i + 1 + "->" + "nodo" + instruccion.GetHashCode() + "\n";

                
                text += "nodo12345" + instruccion.GetHashCode()+ i + 1 + "[label= \"LISTAINSTRUCCIONES\"fillcolor=\"LightBlue\", style =\"filled\", shape=\"box\"];\n ";
                text += "nodo" + instruccion.GetHashCode() + "[label= " +"\""+ instruccion.Term.Name.ToUpper()+ "\"" + "fillcolor=\"LightBlue\", style =\"filled\", shape=\"box\"];\n ";               
                for (int j=0; j<instruccion.ChildNodes.Count;j++) { 
                    text += "nodo" + instruccion.GetHashCode() +" -> nodo" + instruccion.ChildNodes.ElementAt(j).GetHashCode() +"\n";
                    text += "nodo" + instruccion.ChildNodes.ElementAt(j).GetHashCode() + "[label= " + "\"" + instruccion.ChildNodes.ElementAt(j).Term.Name.ToUpper() + "\"" + "fillcolor=\"LightBlue\", style =\"filled\", shape=\"box\"];\n ";
                    ParseTreeNode funcion = instruccion.ChildNodes.ElementAt(j);
                    for (int k=0;k<funcion.ChildNodes.Count; k++) {
                        text += "nodo" + instruccion.ChildNodes.ElementAt(j).GetHashCode()  + " -> nodo" + funcion.ChildNodes.ElementAt(k).GetHashCode() + "\n";
                        text += "nodo" + funcion.ChildNodes.ElementAt(k).GetHashCode() + "[label= " + "\"" + funcion.ChildNodes.ElementAt(k).Term.Name.ToUpper() + "\"" + "fillcolor=\"LightBlue\", style =\"filled\", shape=\"box\"];\n ";
                        ParseTreeNode hojas = funcion.ChildNodes.ElementAt(k);
                        for (int l = 0; l < hojas.ChildNodes.Count; l++)
                        {
                            text += "nodo" + funcion.ChildNodes.ElementAt(j).GetHashCode() + " -> nodo" + hojas.ChildNodes.ElementAt(l).GetHashCode() + "\n";
                            text += "nodo" + hojas.ChildNodes.ElementAt(l).GetHashCode() + "[label= " + "\"" + hojas.ChildNodes.ElementAt(l).Term.Name.ToUpper() + "\"" + "fillcolor=\"LightBlue\", style =\"filled\", shape=\"box\"];\n ";

                        }
                    }
                }
            }
            StreamWriter archivo = new StreamWriter("C:\\compiladores2\\AST.txt");
            archivo.Write(text);
            archivo.Write("\n}");
            archivo.Close();
        }
    }
}
