program basico;
const  V : boolean = true;
const  F : boolean = false;
const  datos : integer = 3;
var val1 : integer = 0;
var val2 : integer = 0;
var val3 : integer = 0;
var resp : integer = 0;
var a : integer = 0;
var b : integer = 0;
procedure ImprimirValores();
begin
    writelnln('-----------------------');
    writeln('El valor de V es: ', V);
    writeln('El valor de F es: ', F);
    writeln('El valor de datos es: ', datos);
    writeln('El valor de val1 es: ', val1);
    writeln('El valor de val2 es: ', val2);
    writeln('El valor de val3 es: ',val3);
    writeln('El valor de resp es: ',resp);
    writeln('El valor de a es: ',a);
    writeln('El valor de b es: ',b);
    writeln('-----------------------');
end;
function SumarNumeros(num1 : integer; num2 : integer):integer;
var
	resp : integer;
begin
    resp := num1 + num2;
    writeln('El resultado de tu suma es: ', resp);
    exit(resp);
end;
procedure IniciarValores();
begin
    writeln('----Dentro de Iniciar Valores----');
    
    val1 := 7 - (5 + 10 * (2 + 4 * (5 + 2 * 3)) - 8 * 3 * 3) + 50 * (6 * 2);
    val2 := (2 * 2 * 2 * 2) - 9 * (8 - 6 * (3 * 3 - 6 * 5 - 7 * (9 + 7 * 7 * 7) + 10) - 5) + 8 * (6 - 5 * (2 * 3));
    val3 := val1 * ((2 + val2 * 3) + 1 - ((2 * 2 * 2) - 2) * 2) - 2;

    a := val1 + val2 - val3 + SumarNumeros(5, val1);
    b := SumarNumeros(5, a) - val1 * 2;

    resp := val1 + val2 + SumarNumeros(val3, resp);

    ImprimirValores();

    writeln('-----------------------');
end;
function decisiones() : boolean;
var
    valorVerdadero : integer = 100;
begin
    writeln('----Dentro de Decisiones----');
    if((valorVerdadero = (50 + 50 + (val1 - val1))) and not not not not not not not not not false) then
    begin
        writeln('En este lugar deberia de entrar :)');
        valorVerdadero := 50;
    end;
    else if (F or (valorVerdadero > 50)) and ((resp <> 100) and not not not not not V) then
    begin
        writeln('Aca no deberia de entrar :ccc');
        valorVerdadero := 70;
    end;
    else
    begin
        writeln('Aca no deberia de entrar :cccc');
    end;

    case valorVerdadero of
        70:
        begin
            writeln('No deberia entrar :P');
        end;
        50:
        begin
            writeln('Entro!? Que bueno :D');
            writeln('-----------------------');
            Exit(true);
            writeln('No deberia imprimir esto o:');
        end;
        100:
        begin
            writeln('No deberia entrar :P');
        end;
        else
        begin
            writeln('No deberia entrar :P');
        end;
    writeln('-----------------------');
    Exit(false and true);
end;
procedure CiclosYControl();
var
    i : integer = 0;
begin
    writeln('----Dentro de Ciclos y Control----');

    writeln('While');
    while i < datos do
    begin
        writeln('El valor de i: ');
        writeln(i);
        i := i + 1;
        continue;
        writeln('Esto no deberia imprimir dentro de while');
    end;

    writeln('For Do');
    for i := 0 to 10 do
    begin
        if i = 8 then
        begin
            break;
        end;

        writeln('El valor de i: ');
        writeln(i);
    end;

    writeln('Repeat');
    i := 6;
    repeat
        writeln('El valor de i: ');
        writeln(i);
        i := i - 2;
    until (i = 0);

    writeln('-----------------------');
end;
procedure Inicio();
begin
    writeln('----------------------');
    writeln('----ARCHIVO BASICO----');
    writeln('----------------------');

    ImprimirValores();

    IniciarValores();

    writeln('Dentro de Inicio');
    writeln(SumarNumeros(5, 5));

    if(decisiones()) then
    begin
        writeln('Esto deberia de imprimirse...');
    end;
    else
    begin
        writeln('No deberia entrar aca :D');
    end;

    CiclosYControl();

    writeln('----------------------------------------');
    writeln('----ESPEREMOS QUE HAYA FUNCIONADO :D----');
    writeln('----------------------------------------');
end;
Inicio();