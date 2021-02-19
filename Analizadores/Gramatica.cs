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
            var tIf = ToTerm("if");
            var then = ToTerm("then");
            var tElse = ToTerm("else");
            var tExit = ToTerm("exit");
            var tProcedure = ToTerm("procedure");
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
            var POW = ToTerm("^");
            var DIVIDIDO = ToTerm("/");
            var tAnd = ToTerm("&&");
            var tOr = ToTerm("||");
            var tDifQ = ToTerm("!=");
            var tDobleIgual = ToTerm("==");
            var tMayorQ = ToTerm(">");
            var tmayorIgual = ToTerm(">=");
            var tmenorIgual = ToTerm("<=");
            var tMenorQ = ToTerm("<");


            RegisterOperators(1, MAS, MENOS);
            RegisterOperators(2, POR, DIVIDIDO, POW);
            RegisterOperators(3, tMayorQ, tMenorQ,tmenorIgual,tmayorIgual);
            RegisterOperators(4, tOr, tAnd, tDifQ);

            #endregion

            #region No Terminales
            NonTerminal ini = new NonTerminal("ini");
            NonTerminal instruccion = new NonTerminal("instruccion");
            NonTerminal instruccion2 = new NonTerminal("instruccion2");
            NonTerminal returnFuncion = new NonTerminal("returnFuncion");
            NonTerminal listInstr = new NonTerminal("listInstr");
            NonTerminal listExpr = new NonTerminal("listExpr");
            NonTerminal listInstr2 = new NonTerminal("listInstr2");
            NonTerminal listParam = new NonTerminal("listParam");
            NonTerminal listFuncion = new NonTerminal("listFuncion");
            NonTerminal expresion = new NonTerminal("expresion");
            NonTerminal DECLARACION = new NonTerminal("declaracion");
            NonTerminal FUNCION = new NonTerminal("funcion");
            NonTerminal PROCEDURE = new NonTerminal("procedure");
            NonTerminal IF = new NonTerminal("if");
            NonTerminal ELSEST = new NonTerminal("else");
            NonTerminal LLAMADAFUNCION = new NonTerminal("llamadaFuncion");
            #endregion

            #region Gramatica
            ini.Rule = listInstr;

            listInstr.Rule = MakePlusRule(listInstr, instruccion)
                             | Empty;
            //Instrucciones que acepta el lenguaje
            instruccion.Rule = DECLARACION + PTCOMA
                                | FUNCION
                                | PROCEDURE
                                | LLAMADAFUNCION + PTCOMA
                                | Twriteln + PARIZQ + listExpr + PARDER + PTCOMA
            ;
            PROCEDURE.Rule = tProcedure + tId + PTCOMA + listInstr + TBEGIN + listInstr2 + TEND + PTCOMA
                            | tProcedure + tId + PARIZQ + listParam + PARDER + PTCOMA + listInstr + TBEGIN + listInstr2 + TEND + PTCOMA
                        ;
            FUNCION.Rule = TFUNCTION + tId + PARIZQ + listParam + PARDER + PDOSPUNTOS + tId + PTCOMA
                                + listInstr
                                + TBEGIN + listInstr2 + TEND + PTCOMA
                        | TFUNCTION + tId + PDOSPUNTOS + tId + PTCOMA
                            + listInstr
                            + TBEGIN + listInstr2 + TEND + PTCOMA
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
            //Instrucciones dentro de las funciones y Procedimientos
            returnFuncion.Rule = tExit + PARIZQ + tId + PDOSPUNTOS + IGUAL + expresion + PARDER + PTCOMA
                                | tId + PDOSPUNTOS + IGUAL + expresion + PTCOMA ;
            IF.Rule = tIf + expresion + then  + TBEGIN + listInstr2 + TEND + PTCOMA +ELSEST
                ;
            ELSEST.Rule = tElse + TBEGIN + listInstr2 + TEND + PTCOMA
                        | tElse + IF
                        | Empty
                      ;
            instruccion2.Rule = //Twriteln + PARIZQ + expresion + PARDER + PTCOMA
                                 Twriteln + PARIZQ + listExpr + PARDER + PTCOMA
                                | returnFuncion
                                | LLAMADAFUNCION + PTCOMA
                                | IF
            ;
            listFuncion.Rule = MakePlusRule(listFuncion, FUNCION);
            
            LLAMADAFUNCION.Rule = tId + PARIZQ + listExpr + PARDER
                ;
            instruccion.ErrorRule = SyntaxError + ";";
            instruccion2.ErrorRule = SyntaxError + ";";
            DECLARACION.Rule = TVAR + tId + PDOSPUNTOS + tId
                                | TVAR + tId + PDOSPUNTOS + tId + IGUAL + expresion
                                | tId + PDOSPUNTOS + tId 
                ;
            // Expresiones (Devuleven un valor)
            expresion.Rule = MENOS + expresion
                | expresion + MAS + expresion
                | expresion + MENOS + expresion
                | expresion + POR + expresion
                | expresion + DIVIDIDO + expresion
                | expresion + tMenorQ + expresion
                | expresion + tMayorQ + expresion
                | expresion + tmenorIgual+ expresion
                | expresion + tmayorIgual + expresion
                | expresion + tDobleIgual + expresion
                | expresion + tOr + expresion
                | expresion + tAnd + expresion
                | expresion + tDifQ + expresion

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
