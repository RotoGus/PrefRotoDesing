# PrefRotoDesign
Codigo usado para representar en PrefCAD los graficos de manillas y bisagras suministradas por Roto Frank, S.A. 

## Instalación

- Sobre la PrefUserCSharp del cliente (.NET), en el modulo ModelModule.cs, aplicar la llamada a la libreria: 
```bash[c#]
   using PrefRotoDesing;
```  
- En "onDrawHandle" y "OnDrawHinges" instanciar las clase DrawRotoHandle y DrawRotoHinge respectivamente:
```bash[c#]
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
