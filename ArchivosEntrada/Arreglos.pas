program arreglos;
type 
  Reporte = object
  var rechazos,dat: integer;
  var vino ,revision , botella  : string;
end;
var i, j, k: integer;
var vinos : array[0..4] of String;
vinos[0] := 'Blanco';vinos[1] := 'Tinto';vinos[2] := 'Jerez';vinos[3] := 'Oporto';vinos[4] := 'Rosado';
var botellas : array[0..3] of String;
botellas[0] := 'Magnum';botellas[1] := 'DMagnum';botellas[2] := 'Estandar';botellas[3] := 'Imperial';
var revisiones : array[0..1] of String;
revisiones[0] := 'Oxigeno';revisiones[1] := 'Envases';
var encabezado : array[0..3] of String;
encabezado[0] := 'Revision';encabezado[1] := 'Tipo Vino';encabezado[2] := 'Botella'; encabezado[3] := 'Total' ;

var reportes : array[0..1,0..4,0..3] of Reporte;
function Insertar(var arreglo:Reporte): Reporte;
var valStruct : Reporte;
begin
  for i:=0 to 2 do
	Begin
		for j:=0 to 5 do
		Begin
			for k:=0 to 4 do
			Begin			
				valStruct.vino := vinos[j];
				valStruct.revision := revisiones[i];
				valStruct.botella:= botellas[k];
				valStruct.rechazos :=  ((i)*5*4) + ((j)*4) + (k + 1);
				arreglo[i,j,k] := valStruct;
				//writeln('arreglo','[' + i + ']','[' + j + ']','[' + k + ']');
			End;
		End;		
	End;
	exit(arreglo);
end;
procedure ImprimirStruct(var repo:Reporte);
begin
  writeln(repo.revision, '        ',repo.vino ,  '         ', repo.botella, '         ', repo.rechazos );
end;
procedure Imprimir(var arreglo:Reporte);
begin
  for i:=0 to 2 do
	Begin
		for j:=0 to 5 do
		Begin
			for k:=0 to 4 do
			Begin			
				ImprimirStruct(arreglo[i,j,k]);				
			End;
		End;		
	End;
end;
procedure ImprimirEncabezado();
begin
  writeln(encabezado[0] + '        ' , encabezado[1] + '         ' ,encabezado[2] + '         '  , encabezado[3]);
end;
reportes := Insertar(reportes);
ImprimirEncabezado();
Imprimir(reportes);