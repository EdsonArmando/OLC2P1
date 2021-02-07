using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Analizadores
{
    class Gramatica : Grammar
    {
        public Gramatica()
        {
            #region ER
            CommentTerminal comentarioLinea = new CommentTerminal("comentarioLinea", "//", "\n", "\r\n"); //si viene una nueva linea se termina de reconocer el comentario.
            CommentTerminal comentarioBloque = new CommentTerminal("comentarioBloque", "(*", "*)");
            CommentTerminal comentarioBloque2 = new CommentTerminal("comentarioBloque", "{", "}");
            var NUMERO = new NumberLiteral("Numero");
            #endregion
            NonGrammarTerminals.Add(comentarioLinea);
            NonGrammarTerminals.Add(comentarioBloque);
            NonGrammarTerminals.Add(comentarioBloque2);
            #region Terminales
            var REVALUAR = ToTerm("Evaluar");
            var PTCOMA = ToTerm(";");
            var PARIZQ = ToTerm("(");
            var PARDER = ToTerm(")");
            var CORIZQ = ToTerm("[");
            var CORDER = ToTerm("]");
            var MAS = ToTerm("+");
            var MENOS = ToTerm("-");
            var POR = ToTerm("*");
            var DIVIDIDO = ToTerm("/");

            RegisterOperators(1, MAS, MENOS);
            RegisterOperators(2, POR, DIVIDIDO);

            #endregion

            #region No Terminales
            NonTerminal ini = new NonTerminal("ini");
            NonTerminal instruccion = new NonTerminal("instruccion");
            NonTerminal listInstr = new NonTerminal("listInstr");
            NonTerminal expresion = new NonTerminal("expresion");
            #endregion

            #region Gramatica
            ini.Rule = listInstr;

            listInstr.Rule = MakeStarRule(listInstr, instruccion);

            instruccion.Rule = REVALUAR + CORIZQ + expresion + CORDER + PTCOMA
            ;
            instruccion.ErrorRule = SyntaxError + ";";

            expresion.Rule = MENOS + expresion
                | expresion + MAS + expresion
                | expresion + MENOS + expresion
                | expresion + POR + expresion
                | expresion + DIVIDIDO + expresion
                | NUMERO
                | PARIZQ + expresion + PARDER;

            #endregion

            #region Preferencias
            this.Root = ini;
            #endregion
        }
    }
}
