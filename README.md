![screenshot](logo-my-portal.png "Roto Frank SPN")
# PrefRotoDesign

## Descripción
Código C# usado para representar en PrefCAD los gráficos de manillas y bisagras suministradas por Roto Frank, S.A. 

## Instalación

- Compila el proyecto PrefRotoDesing.dll
- Sobre la PrefUserCSharp del cliente (.NET), en el modulo ModelModule.cs, aplica la llamada a la librería, previamente agregada como referencia: 
```csharp
   using PrefRotoDesing;
```  
- En **OnDrawHandle** y **OnDrawHinges** instancia las clase ***DrawRotoHandle*** y ***DrawRotoHinge*** respectivamente:
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
- y compila en la versión de PrefSuite vigente en el cliente.
- Los gráficos de manillas y bisagras deben de estar previamente aplicados como símbolos de usuario en PrefWise.

## Simbolos de Usuario usados en código
### HANDLES

- <ins>Handles para VENTANA:</ins>
  
  Controlados por la OPCION: ***RO_MANILLA VENTANA***
  
| Símbolo                       | Descripción                            |
|-------------------------------|----------------------------------------|
| `Option_HANDLE_Rotoline`      | Manilla Rotoline y Rotoline secustik   |
| `Option_HANDLE_Rotoline Llave`| Manilla Rotoline llave                 |
| `Option_HANDLE_Rotoswing`     | Manilla Rotoswing y Rotoswing secustik |
| `<PENDIENTE>`                 | Manilla Rotoswing llave                |
| `Option_HANDLE_Rotosamba`     | Manilla Rotosamba y Rotosamba secustik |
| `<PENDIENTE>`                 | Manilla Rotosamba llave                |
  
- <ins>Handles para BALCONERA:</ins>
  
  Controlados por la OPCION: ***RO_MANILLA BALCONERA*** y ***RO_BOMBILLO***
  
| Símbolo                  | Descripción                                      |
|--------------------------|--------------------------------------------------|
| `Option_HANDLE_MB`       | Manilla+Manilla y Manilla+Manilla plana (con bombillo)    |
| `Option_HANDLE_Rotoline` | Solo manilla interior (sin bombillo)             |
| `Option_HANDLE_MB_S`     | Solo manilla interior (para exterior bombillo)   |

- <ins>Handles para PUERTA:</ins>
  
  Controlados por la OPCION: ***RO_MANILLA PUERTA***

| Símbolo                  | Descripción                                      |
|--------------------------|--------------------------------------------------|
| `Option_HANDLE_MP_DR`    | Juego Manilla dos caras (Derecha)                |
| `Option_HANDLE_MP_IZ`    | Juego Manilla dos caras (Izquierda)              |
| `Option_HANDLE_MP_T_DR`  | Manilla + Tirador (Derecha)                      |
| `Option_HANDLE_MP_T_IZ`  | Manilla + Tirador (Izquierda)                    |
| `Option_HANDLE_MP_P`     | Manilla + Placa ciega (placa exterior)           |

- <ins>Handles para CORREDERA:</ins>
  
  Controlados por la OPCION: ***RO_COR_MANILLA INTERIOR***, ***RO_COR_MANILLA EXTERIOR***, y ***RO_BOMBILLO***

| Símbolo                     | Descripción  (M.Interior)                        |
|-----------------------------|--------------------------------------------------|
| `<PENDIENTE>` 	      | Cor. Tirador corto                               |
| `<PENDIENTE>`               | Cor. Tirador largo                               |
| `Option_HANDLE_MC_TB_DR`    | Cor. Tirador bombillo (Derecha)                  |
| `Option_HANDLE_MC_TB_IZ`    | Cor. Tirador bombillo (Izquierda)                |
| `Option_HANDLE_Rotoline`    | Cor. Rotoline y Cor. Rotoline secustik (sin bombillo) |
| `Option_HANDLE_RotoM+M`     | Cor. Rotoline y Cor. Rotoline secustik (con bombillo) |
| `<PENDIENTE>`               | Cor. Rotoline llave (sin bombillo)               |
| `<PENDIENTE>`               | Cor. Rotoswing y Cor. Rotoswing secustik (sin bombillo) |
| `<PENDIENTE>`               | Cor. Rotoswing y Cor. Rotoswing secustik (con bombillo) |
| `<PENDIENTE>`               | Cor. Rotoswing llave (sin bombillo)              |
| `Option_HANDLE_Uñero`       | Cor. Manilla uñero (sin bombillo)                |
| `<PENDIENTE>`               | Cor. Manilla uñero (con bombillo)                |

| Símbolo                     | Descripción  (M.Exterior)                        |
|-----------------------------|--------------------------------------------------|
| `<PENDIENTE>` 	      | Cor. Tirador ext. uñero                          |
| `<PENDIENTE>`               | Cor. Tirador ext. corto                          |
| `<PENDIENTE>`               | Cor. Tirador ext. largo                          |
| `Option_HANDLE_MC_TB_DR`    | Cor. Tirador ext. bombillo (Derecha)             |
| `Option_HANDLE_MC_TB_IZ`    | Cor. Tirador ext. bombillo (Izquierda)           |

- <ins>Handles para OSCILOPARALELA:</ins>
  
  Controlados por la OPCION: ***RO_ALV_MANILLA*** y ***RO_ALV_CREMONA*** 

| Símbolo                     | Descripción                                      |
|-----------------------------|--------------------------------------------------|
| `Option_HANDLE_MO` 	      | Cremona simple                                   |
| `Option_HANDLE_MO_B`        | Cremona con bombillo (Manilla interior)          |

- <ins>Handles para ELEVADORA:</ins>
  
  Controlados por la OPCION: ***RO_ELV_MANILLA***  

| Símbolo                     | Descripción                                      |
|-----------------------------|--------------------------------------------------|
| `Option_HANDLE_ME` 	      | Manilla+Uñero y Manilla solo interior (interior) |
| `Option_HANDLE_ME_B`        | Manilla cilindro+Uñero y Manilla cilindro 2 caras (interior) |
| `Option_HANDLE_ME_U`        | Manilla+Uñero y Manilla cilindro+Uñero (exterior) |
| `Option_HANDLE_ME_B`        | Manilla cilindro 2 caras (exterior)              |
| `Option_HANDLE_ME`          | Manilla 2 caras (interior y exterior)            |

### HINCHES

- <ins>Hinges para VENTANA / BALCONERA / ABATIBLE:</ins>

| Símbolo                     | Descripción                              |
|-----------------------------|------------------------------------------|
| `Option_HINGE_Superior_DR`  | Bisagra superior derecha                 |
| `Option_HINGE_Superior_IZ`  | Bisagra superior izquierda               |
| `Option_HINGE_Inferior_DR`  | Pernio inferior derecho                  |
| `Option_HINGE_Inferior_IZ`  | Pernio inferior izquierdo                |
| `Option_HINGE_PB10`         | Bisagra al exterior                      |
| `Option_HINGE_Abatible`     | Bisagra abatible                         |

- <ins>Hinges para PUERTA (PVC):</ins>

  Controlados por la OPCION: ***RO_PU_BISAGRA***

| Símbolo                     | Descripción                              |
|-----------------------------|------------------------------------------|
| `Option_HINGE_PS27_DCHA`    | PS27 (derecha)                           |
| `Option_HINGE_PS27_IZDA`    | PS27 (izquierda)                         |
| `Option_HINGE_PS23_DCHA`    | PS23 (derecha)                           |
| `Option_HINGE_PS23_IZDA`    | PS23 (izquierda)                         |
| `Option_HINGE_PB10`         | PB10 (derecha e izquierda)               |
| `Option_HINGE_150P_DCHA`    | 150P (derecha)                           |
| `Option_HINGE_150P_IZDA`    | 150P (izquierda)                         |

  Controlados por la OPCION: ***RO_PU_BISAGRA = 'SOLID B'*** y ***RO_PU_SOLID_B_TIPO***

| Símbolo                     | Descripción                     |
|-----------------------------|---------------------------------|
| `Option_HINGE_SOLID B_2C`   | 2C/80Kg y 2C/120Kg              |
| `Option_HINGE_SOLID B_3C`   | 3C/120Kg y 3C/160Kg             |

- <ins>Hinges para PUERTA (ALUMINIO):</ins>

  Controlados por la OPCION: ***RO_PU_AL_BISAGRA***

| Símbolo                     | Descripción                              |
|-----------------------------|------------------------------------------|
| `Option_HINGE_ATB80_DCHA`   | ATB80 (derecha)                         |
| `Option_HINGE_ATB80_IZDA`   | ATB80 (izquierda)                       |




## Contribuciones

Las contribuciones son lo que hacen que la comunidad de código abierto sea un lugar increíble para aprender, inspirar y crear. Cualquier contribución que hagas será **bienvenida**.

## Contacto

Para comunicarte con el equipo de Ingeniería de datos de Roto Frank, S.A. puedes hacerlo mediante el correo electrónico <gustavo.martinez@roto-frank.com>
