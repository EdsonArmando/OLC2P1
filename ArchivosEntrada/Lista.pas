program suma;
var mat: array[1..3,1..3] of integer;
var sumaFila: array[1..3] of integer;
var sumaCol: array[1..3] of integer;
var i, j: integer;
for i:=1 to 3 do
  Begin
    sumaFila[i]:= 0;
    sumaCol[i]:= 0;
  End;
{Leemos los datos para la matriz de 3x3}
 for i:=1 to 3 do
  Begin
    for j:=1 to 3 do
     Begin
       mat[i,j] := 10;
     End;
  End;
   {Calculamos la suma de cada fila y cada columna con un solo ciclo for}
 for i:=1 to 3 do
 begin
  for j:=1 to 3 do
   Begin
     sumaFila[i]:= sumaFila[i] + mat[i,j];
     sumaCol[i]:= sumaCol[i] + mat[j,i];
   End;
  end;
{Mostramos el vector que contiene la suma de las filas}
 Writeln('VECTOR SUMA DE FILAS: ');
 for i:=1 to 3 do
  Begin
    Writeln(sumaFila[i]);
  End;
   {Mostramos el vector que contiene la suma de las columnas}
 Writeln('VECTOR SUMA DE COLUMNAS: ');
 for j:=1 to 3 do
  Begin
    Writeln(sumaCol[j]);
  End;