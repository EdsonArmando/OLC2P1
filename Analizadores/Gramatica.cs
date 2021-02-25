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
            var twhile = ToTerm("while");
            var TFUNCTION = ToTerm("function");
            var Twriteln = ToTerm("writeln");
            var tRepeat = ToTerm("repeat");
            var tUntil = ToTerm("until");
            var tConst = ToTerm("const");
            var tIf = ToTerm("if");
            var tCase = ToTerm("case");
            var tOf = ToTerm("of");
            var then = ToTerm("then");
            var tElse = ToTerm("else");
            var tExit = ToTerm("exit");
            var tBreak = ToTerm("break");
            var tContinue = ToTerm("continue");
            var tFor = ToTerm("for");
            var tTo = ToTerm("to");
            var tDo = ToTerm("do");
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
            var tAnd = ToTerm("and");
            var tOr = ToTerm("or");
            var tDifQ = ToTerm("not");
            var tDobleIgual = ToTerm("==");
            var tMayorQ = ToTerm(">");
            var tmayorIgual = ToTerm(">=");
            var tmenorIgual = ToTerm("<=");
            var tMenorQ = ToTerm("<");


            RegisterOperators(1, MAS, MENOS);
            RegisterOperators(2, POR, DIVIDIDO, POW);
            RegisterOperators(3, tMayorQ, tMenorQ, tmenorIgual, tmayorIgual);
            RegisterOperators(4, tOr, tAnd, tDifQ);

            #endregion

            #region No Terminales
            NonTerminal ini = new NonTerminal("ini");
            NonTerminal instruccion = new NonTerminal("instruccion");
            NonTerminal instruccion2 = new NonTerminal("instruccion2");
            NonTerminal returnFuncion = new NonTerminal("returnFuncion_asignacion");
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
            NonTerminal CASE = new NonTerminal("case");
            NonTerminal FOR = new NonTerminal("for");
            NonTerminal WHILE = new NonTerminal("while");
            NonTerminal ELSEST = new NonTerminal("else");
            NonTerminal INSTRCASE = new NonTerminal("instrcase");
            NonTerminal REPEAT = new NonTerminal("repeat");
            NonTerminal ASIGNACION = new NonTerminal("asignacion");
            NonTerminal LLAMADAFUNCION = new NonTerminal("llamadaFuncion");
            #endregion

            #region Gramatica
            ini.Rule = listInstr;

            listInstr.Rule = MakePlusRule(listInstr, instruccion)
                             | Empty;
            //Instrucciones que acepta el lenguaje
            instruccion.Rule = DECLARACION + PTCOMA
                                | FUNCION
                                | returnFuncion
                                | PROCEDURE
                                | LLAMADAFUNCION + PTCOMA
                                | Twriteln + PARIZQ + listExpr + PARDER + PTCOMA
            ;
            PROCEDURE.Rule = tProcedure + tId + PTCOMA + listInstr + TBEGIN + listInstr2 + TEND + PTCOMA
                            | tProcedure + tId + PARIZQ + listParam + PARDER + PTCOMA + listInstr + TBEGIN + listInstr2 + TEND + PTCOMA
                        ;
            FUNCION.Rule = TFUNCTION + tId + PARIZQ + listParam + PARDER + PDOSPUNTOS + tId + PTCOMA
                                + listInstr //Variable Locales
                                + TBEGIN + listInstr2  + TEND + PTCOMA
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
            returnFuncion.Rule = tExit + PARIZQ + expresion + PARDER + PTCOMA
                                | tId + PDOSPUNTOS + IGUAL + expresion + PTCOMA 
                                ;
            IF.Rule = tIf + expresion + then  + TBEGIN + listInstr2 + TEND + PTCOMA +ELSEST
                ;
            ELSEST.Rule = tElse + TBEGIN + listInstr2 + TEND + PTCOMA
                        | tElse + IF
                        | Empty
                      ;
            REPEAT.Rule = tRepeat + listInstr2 + tUntil + expresion + PTCOMA;
            FOR.Rule = tFor + tId + PDOSPUNTOS + IGUAL + expresion + tTo + expresion + tDo + TBEGIN + listInstr2 + TEND + PTCOMA;
            WHILE.Rule = twhile + expresion + tDo + TBEGIN + listInstr2 + TEND + PTCOMA;
            ASIGNACION.Rule = tId + PDOSPUNTOS + IGUAL + expresion ;
            INSTRCASE.Rule = expresion + PDOSPUNTOS +listInstr;
            CASE.Rule = tCase + expresion + tOf + listInstr2+ TEND + PTCOMA
                      | tCase + expresion + tOf + listInstr2 + tElse + listInstr2 + TEND + PTCOMA
                        ;
            instruccion2.Rule = //Twriteln + PARIZQ + expresion + PARDER + PTCOMA
                                 Twriteln + PARIZQ + listExpr + PARDER + PTCOMA
                                | returnFuncion
                                | LLAMADAFUNCION + PTCOMA
                                | IF
                                | FOR
                                | INSTRCASE
                                | WHILE
                                | CASE
                                | REPEAT
                                | ASIGNACION + PTCOMA
                                | tBreak + PTCOMA
                                | tContinue + PTCOMA
            ;
            listFuncion.Rule = MakePlusRule(listFuncion, FUNCION);
            
            LLAMADAFUNCION.Rule = tId + PARIZQ + listExpr + PARDER
                ;
            instruccion.ErrorRule = SyntaxError + ";";
            instruccion2.ErrorRule = SyntaxError + ";";
            DECLARACION.Rule = TVAR + tId + PDOSPUNTOS + tId
                                | TVAR + tId + PDOSPUNTOS + tId + IGUAL + expresion
                                | TVAR + listExpr + PDOSPUNTOS + tId + IGUAL + expresion
                                | tConst + tId +  IGUAL + expresion
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
                | expresion + IGUAL + expresion
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
