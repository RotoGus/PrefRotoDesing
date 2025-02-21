# PrefRotoDesign
![screenshot](Roto_G3.jpg "Roto Frank SPN")

## Descripción
Código c# usado para representar en PrefCAD los gráficos de manillas y bisagras suministradas por Roto Frank, S.A. 

## Instalación

- Compila el proyecto PrefRotoDesing.dll
- Sobre la PrefUserCSharp del cliente (.NET), en el modulo ModelModule.cs, aplicar la llamada a la librería, previamente agregada como referencia: 
```csharp
   using PrefRotoDesing;
```  
- En **OnDrawHandle** y **OnDrawHinges** instanciar las clase ***DrawRotoHandle*** y ***DrawRotoHinge*** respectivamente:
```csharp
public void OnDrawHandle(Interop.PrefCAD.Modelo model, Interop.PrefCAD.Hueco hueco, Interop.PrefCAD.ModelImage imagen, out bool drawn)
{
    drawn = false;
    string hardwareSupplier = hueco.Elemento.Opciones.Item("HardwareSupplier");
    if (hardwareSupplier == "ROTO NX" || hardwareSupplier == "ROTO NX ALU")
    {
        DrawRotoHandle drHa = new DrawRotoHandle();
        drHa.RotoHandle(model, hueco, imagen);
    }
    drawn = true;
}
public void OnDrawHinges(Interop.PrefCAD.Modelo model, Interop.PrefCAD.Hueco hueco, Interop.PrefCAD.ModelImage imagen, out bool drawn)
{
    drawn = false;
    string hardwareSupplier = hueco.Elemento.Opciones.Item("HardwareSupplier");
    if (hardwareSupplier == "ROTO NX" || hardwareSupplier == "ROTO NX ALU")
    {
	DrawRotoHinge drHi = new DrawRotoHinge();
	drHi.RotoHinge(model, hueco, imagen);
    }
    drawn = true;
}
```  
- y compilar en la versión de PrefSuite vigente.
- Los gráficos de manillas y bisagras deben de estar previamente aplicados como símbolos de usuario en PrefWise.

## Simbolos de Usuario usados en código

- Handles para VENTANA:
  
  Controlados por la OPCION: ***RO_MANILLA VENTANA***
  
| Símbolo                       | Descripción                            |
|-------------------------------|----------------------------------------|
| `Option_HANDLE_Rotoline`      | Manilla Rotoline y Rotoline secustik   |
| `Option_HANDLE_Rotoline Llave`| Manilla Rotoline llave                 |
| `Option_HANDLE_Swing`         | Manilla Rotoswing y Rotoswing secustik |
| `<PENDIENTE>`                 | Manilla Rotoswing llave                |
| `Option_HANDLE_Rotosamba`     | Manilla Rotosamba y Rotosamba secustik |
| `<PENDIENTE>`                 | Manilla Rotosamba llave                |
  
- Handles para BALCONERA:
  
  Controlados por la OPCION: ***RO_MANILLA BALCONERA***
  
| Símbolo                  | Descripción                                      |
|--------------------------|--------------------------------------------------|
| `Option_HANDLE_MB`       | Manilla+Manilla y Manilla+Manilla plana (con bombillo)    |
| `Option_HANDLE_Rotoline` | Solo manilla interior (sin bombillo)             |
| `Option_HANDLE_MB_S`     | Solo manilla interior (para exterior bombillo)   |

- Handles para PUERTA:
  
  Controlados por la OPCION: ***RO_MANILLA PUERTA***

| Símbolo                  | Descripción                                      |
|--------------------------|--------------------------------------------------|
| `Option_HANDLE_MP_DR`    | Juego Manilla dos caras (Derecha)                |
| `Option_HANDLE_MP_IZ`    | Juego MAnilla dos caras (Izquierda)              |
| `Option_HANDLE_MP_T_DR`  | Manilla + Tirador (Derecha)                      |
| `Option_HANDLE_MP_T_IZ`  | Manilla + Tirador (Izquierda)                    |
| `Option_HANDLE_MP_P`     | Manilla + Placa ciega (placa exterior)           |





## Contribuciones

Las contribuciones son lo que hacen que la comunidad de código abierto sea un lugar increíble para aprender, inspirar y crear. Cualquier contribución que hagas será **agradecida**.

## Contacto

Para comunicarse con el equipo de Ingenieria de datos de Roto Frank, S.A. puede hacerlo mediante el correo electrónico <gustavo.martinez@roto-frank.com>
