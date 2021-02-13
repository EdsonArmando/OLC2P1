using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1_Compi2.Analizadores
{
    class Gramatica : Grammar
    {
        public Gramatica() : base(caseSensitive: false)
        {
            #region ER
            CommentTerminal comentarioLinea = new CommentTerminal("comentarioLinea", "//", "\n", "\r\n"); //si viene una nueva linea se termina de reconocer el comentario.
            CommentTerminal comentarioBloque = new CommentTerminal("comentarioBloque", "(*", "*)");
            CommentTerminal comentarioBloque2 = new CommentTerminal("comentarioBloque", "{", "}");
            var NUMERO = new NumberLiteral("Numero");
            StringLiteral tCadena = new StringLiteral("cadena", "\"");
            StringLiteral tCadena2 = new StringLiteral("cadena2", "\'");
            var tDecimal = new RegexBasedTerminal("Decimal", "[0-9]+'.'[0-9]+");
            IdentifierTerminal tId = new IdentifierTerminal("ID");
            #endregion
            NonGrammarTerminals.Add(comentarioLinea);
            NonGrammarTerminals.Add(comentarioBloque);
            NonGrammarTerminals.Add(comentarioBloque2);
            #region Terminales
            var REVALUAR = ToTerm("evaluar");
            var TFUNCTION = ToTerm("function");
            var Twriteln = ToTerm("writeln");
            var TBEGIN = ToTerm("begin");
            var TVAR = ToTerm("var");
            var TEND = ToTerm("end");
            var PTCOMA = ToTerm(";");
            var PDOSPUNTOS = ToTerm(":");
            var PARIZQ = ToTerm("(");
            var PARDER = ToTerm(")");
            var CORIZQ = ToTerm("[");
            var CORDER = ToTerm("]");
            var COMA = ToTerm(",");
            var MAS = ToTerm("+");
            var MENOS = ToTerm("-");
            var IGUAL = ToTerm("=");
            var POR = ToTerm("*");
            var DIVIDIDO = ToTerm("/");

            RegisterOperators(1, MAS, MENOS);
            RegisterOperators(2, POR, DIVIDIDO);

            #endregion

            #region No Terminales
            NonTerminal ini = new NonTerminal("ini");
            NonTerminal instruccion = new NonTerminal("instruccion");
            NonTerminal instruccion2 = new NonTerminal("instruccion2");
            NonTerminal listInstr = new NonTerminal("listInstr");
            NonTerminal listExpr = new NonTerminal("listExpr");
            NonTerminal listInstr2 = new NonTerminal("listInstr2");
            NonTerminal listParam = new NonTerminal("listParam");
            NonTerminal listFuncion = new NonTerminal("listFuncion");
            NonTerminal expresion = new NonTerminal("expresion");
            NonTerminal DECLARACION = new NonTerminal("declaracion");
            NonTerminal FUNCION = new NonTerminal("funcion");
            NonTerminal LLAMADAFUNCION = new NonTerminal("llamadaFuncion");
            #endregion

            #region Gramatica
            ini.Rule = listInstr;

            listInstr.Rule = MakePlusRule(listInstr, instruccion)
                             | Empty;

            instruccion.Rule = DECLARACION + PTCOMA
                                | FUNCION
            ;
            listFuncion.Rule = MakePlusRule(listFuncion, FUNCION);
            listInstr2.Rule = MakePlusRule(listInstr2, instruccion2)
                              | Empty;
            listParam.Rule = MakePlusRule(listParam, PTCOMA, DECLARACION)
                 | Empty;
            listParam.ErrorRule = SyntaxError + ";";
            listExpr.Rule = MakePlusRule(listExpr, COMA, expresion)
                         | Empty;
            listExpr.ErrorRule = SyntaxError + ",";
            instruccion2.Rule = Twriteln + PARIZQ + expresion + PARDER + PTCOMA
                                | Twriteln + PARIZQ + expresion + PARDER
                                | tId + PDOSPUNTOS + IGUAL + listExpr + PTCOMA
                                | LLAMADAFUNCION + PTCOMA
            ;
            listFuncion.Rule = MakePlusRule(listFuncion, FUNCION);
            FUNCION.Rule = TFUNCTION + tId + PARIZQ + listParam + PARDER + PDOSPUNTOS + tId + PTCOMA
                                + listInstr
                                + TBEGIN + listInstr2 + TEND + PTCOMA
                        | TFUNCTION + tId + PDOSPUNTOS + tId + PTCOMA
                            + listInstr
                            + TBEGIN + listInstr2 + TEND + PTCOMA
                ;
            LLAMADAFUNCION.Rule = tId + PARIZQ + listExpr + PARDER
                ;
            instruccion.ErrorRule = SyntaxError + ";";
            instruccion2.ErrorRule = SyntaxError + ";";
            DECLARACION.Rule = TVAR + tId + PDOSPUNTOS + tId
                                | tId + PDOSPUNTOS 
                                | TVAR + tId + PDOSPUNTOS + tId + IGUAL + expresion
                ;

            expresion.Rule = MENOS + expresion
                | expresion + MAS + expresion
                | expresion + MENOS + expresion
                | expresion + POR + expresion
                | expresion + DIVIDIDO + expresion
                | NUMERO
                | tCadena2
                | tId
                | tCadena
                | LLAMADAFUNCION
                | PARIZQ + expresion + PARDER;

            #endregion

            #region Preferencias
            this.Root = ini;
            #endregion
        }
    }
}
