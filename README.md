![screenshot](logo-my-portal.png "Roto Frank SPN")
# PrefRotoDesign

## Descripción
Código C# usado para representar en PrefCAD los gráficos de manillas y bisagras suministradas por Roto Frank, S.A. 

## Instalación

- Clona y compila localmente el proyecto PrefRotoDesing.dll
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

> [!TIP]
> Los gráficos a usar y enumerados a continuación están incluidos en el archivo ROTO_VIEW.zip

## Simbolos de Usuario usados en código

### HANDLES

- <ins>Handles para VENTANA:</ins>
  
  Controlados por la OPCION: ***RO_MANILLA VENTANA***
  
| Símbolo                       | Descripción                            | Ancho  | Alto  |
|-------------------------------|----------------------------------------|--------|-------|
| `Option_HANDLE_Rotoline`      | Manilla Rotoline y Rotoline secustik   | 100    | 200   |
| `Option_HANDLE_Rotoline Llave`| Manilla Rotoline llave                 | 100    | 200   |
| `Option_HANDLE_Rotoswing`     | Manilla Rotoswing y Rotoswing secustik | 100    | 200   |
| `<PENDIENTE>`                 | Manilla Rotoswing llave                |        |       |
| `Option_HANDLE_Rotosamba`     | Manilla Rotosamba y Rotosamba secustik | 100    | 200   |
| `<PENDIENTE>`                 | Manilla Rotosamba llave                |        |       |
  
- <ins>Handles para BALCONERA:</ins>
  
  Controlados por la OPCION: ***RO_MANILLA BALCONERA*** y ***RO_BOMBILLO***
  
| Símbolo                  | Descripción                                            | Ancho  | Alto  |
|--------------------------|--------------------------------------------------------|--------|-------|
| `Option_HANDLE_MB`       | Manilla+Manilla y Manilla+Manilla plana (con bombillo) | 200    | 300   |
 | `Option_HANDLE_Rotoline` | Solo manilla interior (sin bombillo)                  | 100    | 200   |
| `Option_HANDLE_MB_S`     | Solo manilla interior (para exterior bombillo)         | 200    | 300   |

- <ins>Handles para PUERTA:</ins>
  
  Controlados por la OPCION: ***RO_MANILLA PUERTA***

| Símbolo                  | Descripción                                      | Ancho  | Alto  |
|--------------------------|--------------------------------------------------|--------|-------|
| `Option_HANDLE_MP_DR`    | Juego Manilla dos caras (Derecha)                | 550    | 1500  |
| `Option_HANDLE_MP_IZ`    | Juego Manilla dos caras (Izquierda)              | 550    | 1500  |
| `Option_HANDLE_MP_T_DR`  | Manilla + Tirador (Derecha)                      | 550    | 1500  |
| `Option_HANDLE_MP_T_IZ`  | Manilla + Tirador (Izquierda)                    | 550    | 1500  |
| `Option_HANDLE_MP_P`     | Manilla + Placa ciega (placa exterior)           | 550    | 1500  |

- <ins>Handles para CORREDERA:</ins>
  
  Controlados por la OPCION: ***RO_COR_MANILLA INTERIOR***, ***RO_COR_MANILLA EXTERIOR***, y ***RO_BOMBILLO***

| Símbolo                     | Descripción  (M.Interior)                             | Ancho  | Alto  |
|-----------------------------|-------------------------------------------------------|--------|-------| 
| `<PENDIENTE>` 	      | Cor. Tirador corto                                    |        |       |
| `<PENDIENTE>`               | Cor. Tirador largo                                    |        |       |
| `Option_HANDLE_MC_TB_DR`    | Cor. Tirador bombillo (Derecha)                       | 100    | 280   |
| `Option_HANDLE_MC_TB_IZ`    | Cor. Tirador bombillo (Izquierda)                     | 100    | 280   |
| `Option_HANDLE_Rotoline`    | Cor. Rotoline y Cor. Rotoline secustik (sin bombillo) | 100    | 200   |
| `Option_HANDLE_RotoM+M`     | Cor. Rotoline y Cor. Rotoline secustik (con bombillo) | 200    | 300   |
| `<PENDIENTE>`               | Cor. Rotoline llave (sin bombillo)                    |        |       |
| `<PENDIENTE>`               | Cor. Rotoswing y Cor. Rotoswing secustik (sin bombillo) |      |       |
| `<PENDIENTE>`               | Cor. Rotoswing y Cor. Rotoswing secustik (con bombillo) |      |       |
| `<PENDIENTE>`               | Cor. Rotoswing llave (sin bombillo)                   |        |       |
| `Option_HANDLE_Uñero`       | Cor. Manilla uñero (sin bombillo)                     | 100    | 200   |
| `<PENDIENTE>`               | Cor. Manilla uñero (con bombillo)                     |        |       |
 
| Símbolo                     | Descripción  (M.Exterior)                        | Ancho  | Alto  |
|-----------------------------|--------------------------------------------------|--------|-------| 
| `<PENDIENTE>` 	      | Cor. Tirador ext. uñero                          |        |       |
| `<PENDIENTE>`               | Cor. Tirador ext. corto                          |        |       |
| `<PENDIENTE>`               | Cor. Tirador ext. largo                          |        |       |
| `Option_HANDLE_MC_TB_DR`    | Cor. Tirador ext. bombillo (Derecha)             | 100    | 280   |
| `Option_HANDLE_MC_TB_IZ`    | Cor. Tirador ext. bombillo (Izquierda)           | 100    | 280   |

- <ins>Handles para OSCILOPARALELA:</ins>
  
  Controlados por la OPCION: ***RO_ALV_MANILLA*** y ***RO_ALV_CREMONA*** 

| Símbolo                     | Descripción                                 | Ancho  | Alto  |
|-----------------------------|---------------------------------------------|--------|-------| 
| `Option_HANDLE_MO` 	      | Cremona simple                              | 100    | 280   |
| `Option_HANDLE_MO_B`        | Cremona con bombillo (Manilla interior)     | 100    | 500   |

- <ins>Handles para ELEVADORA:</ins>
  
  Controlados por la OPCION: ***RO_ELV_MANILLA***  

| Símbolo                     | Descripción                                                  | Ancho  | Alto  |
|-----------------------------|--------------------------------------------------------------|--------|-------| 
| `Option_HANDLE_ME` 	      | Manilla+Uñero y Manilla solo interior (interior)             | 100    | 350   |
| `Option_HANDLE_ME_B`        | Manilla cilindro+Uñero y Manilla cilindro 2 caras (interior) | 100    | 350   |
| `Option_HANDLE_ME_U`        | Manilla+Uñero y Manilla cilindro+Uñero (exterior)            | 100    | 150   |
| `Option_HANDLE_ME_B`        | Manilla cilindro 2 caras (exterior)                          | 100    | 350   |
| `Option_HANDLE_ME`          | Manilla 2 caras (interior y exterior)                        | 100    | 350   |

### HINCHES

- <ins>Hinges para VENTANA / BALCONERA / ABATIBLE:</ins>

| Símbolo                     | Descripción                        | Ancho  | Alto  |
|-----------------------------|------------------------------------|--------|-------|
| `Option_HINGE_Superior_DR`  | Bisagra superior derecha           | 350    | 200   |
| `Option_HINGE_Superior_IZ`  | Bisagra superior izquierda         | 350    | 200   |
| `Option_HINGE_Inferior_DR`  | Pernio inferior derecho            | 350    | 200   |
| `Option_HINGE_Inferior_IZ`  | Pernio inferior izquierdo          | 350    | 200   |
| `Option_HINGE_PB10`         | Bisagra al exterior                | 350    | 200   |
| `Option_HINGE_Abatible`     | Bisagra abatible                   | 350    | 200   |

- <ins>Hinges para PUERTA (PVC):</ins>

  Controlados por la OPCION: ***RO_PU_BISAGRA***

| Símbolo                     | Descripción                       | Ancho  | Alto  |
|-----------------------------|-----------------------------------|--------|-------|
| `Option_HINGE_PS27_DCHA`    | PS27 (derecha)                    | 350    | 200   |
| `Option_HINGE_PS27_IZDA`    | PS27 (izquierda)                  | 350    | 200   |
| `Option_HINGE_PS23_DCHA`    | PS23 (derecha)                    | 350    | 200   |
| `Option_HINGE_PS23_IZDA`    | PS23 (izquierda)                  | 350    | 200   |
| `Option_HINGE_PB10`         | PB10 (derecha e izquierda)        | 350    | 200   |
| `Option_HINGE_150P_DCHA`    | 150P (derecha)                    | 350    | 200   |
| `Option_HINGE_150P_IZDA`    | 150P (izquierda)                  | 350    | 200   |

  Controlados por la OPCION: ***RO_PU_BISAGRA = 'SOLID B'*** y ***RO_PU_SOLID_B_TIPO***

| Símbolo                     | Descripción                       | Ancho  | Alto  |
|-----------------------------|-----------------------------------|--------|-------|
| `Option_HINGE_SOLID B_2C`   | 2C/80Kg y 2C/120Kg                | 350    | 200   |
| `Option_HINGE_SOLID B_3C`   | 3C/120Kg y 3C/160Kg               | 350    | 200   |

- <ins>Hinges para PUERTA (ALUMINIO):</ins>

  Controlados por la OPCION: ***RO_PU_AL_BISAGRA***

| Símbolo                     | Descripción                       | Ancho  | Alto  |
|-----------------------------|-----------------------------------|--------|-------|
| `Option_HINGE_ATB80_DCHA`   | ATB80 (derecha)                   | 350    | 200   |
| `Option_HINGE_ATB80_IZDA`   | ATB80 (izquierda)                 | 350    | 200   |
| `Option_HINGE_ATB120_DCHA`  | ATB120 (derecha)                  | 350    | 200   |
| `Option_HINGE_ATB120_IZDA`  | ATB120 (izquierda)                | 350    | 200   |


> [!IMPORTANT]
> Las imágenes suministradas, aunque algunas estén en formato .png, son compatibles para la carga en PrefWise.


## Contribuciones

Las contribuciones son lo que hacen que la comunidad de código abierto sea un lugar increíble para aprender, inspirar y crear. Cualquier contribución que hagas será **bienvenida**.

## Contacto

Para comunicarte con el equipo de Ingeniería de datos de Roto Frank, S.A. puedes hacerlo mediante el correo electrónico <gustavo.martinez@roto-frank.com>
