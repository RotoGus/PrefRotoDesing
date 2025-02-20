# PrefRotoDesign
Código usado para representar en PrefCAD los gráficos de manillas y bisagras suministradas por Roto Frank, S.A. 

## Instalación

- Sobre la PrefUserCSharp del cliente (.NET), en el modulo ModelModule.cs, aplicar la llamada a la librería, previamente agregada como referencia: 
```csharp
   using PrefRotoDesing;
```  
- En "OnDrawHandle" y "OnDrawHinges" instanciar las clase DrawRotoHandle y DrawRotoHinge respectivamente:
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

## Contacto

Para comunicarse con el equipo de Ingenieria de datos de Roto Frank, S.A. puede hacerlo mediante el correo electrónico <gustavo.martinez@roto-frank.com>
