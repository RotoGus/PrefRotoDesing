using System;
using Interop.PrefCAD;

namespace PrefRotoDesing
{
    public class DrawRotoHinge
    {
        public void RotoHinge(Interop.PrefCAD.Modelo model, Interop.PrefCAD.Hueco hueco, Interop.PrefCAD.ModelImage imagen)
        {
            //Determina la apertura desde RotoTools, RotoOpening
            string apertura = RotoTools.RotoOpening(hueco);
            bool vista = model.OuterViewMode; // true si vista EXTERIOR esta activado

            Rectangulo Frame = hueco.Elemento.Barra.Eje.Marco;
            Vertice PointHandle = new Vertice();

            double distanciaV = 55;
            string designo = hueco.Elemento.Opciones.Item("RO_NX_TIPO HERRAJE");

            //1. VENTANA Y BALCONERA
            if (apertura.Equals("V_DR") || apertura.Equals("V_IZ") || apertura.Equals("B_DR_I") || apertura.Equals("B_IZ_I"))
            {
                string biS = "";
                string biI = "";

                if (apertura.Equals("V_DR") || apertura.Equals("B_DR_I"))
                {
                    PointHandle.x = Frame.right;
                    biS = "Option_HINGE_Superior_DR";
                    biI = "Option_HINGE_Inferior_DR";
                }
                else if (apertura.Equals("V_IZ") || apertura.Equals("B_IZ_I"))
                {
                    PointHandle.x = Frame.left;
                    biS = "Option_HINGE_Superior_IZ";
                    biI = "Option_HINGE_Inferior_IZ";
                }

                if (vista == false & designo=="Normal")
                {
                    //'Bisagra Superior
                    PointHandle.y = Frame.top - distanciaV;
                    imagen.DrawSymbol(biS, PointHandle);

                    //'Bisagra Inferior
                    PointHandle.y = Frame.bottom + distanciaV;
                    imagen.DrawSymbol(biI, PointHandle);
                }
            }
            //2. BALCONERA EXT.
            if (apertura.Equals("B_DR_E") || apertura.Equals("B_IZ_E"))
            {
                string biE = "Option_HINGE_PB10";

                if (apertura.Equals("B_IZ_E"))
                {
                    PointHandle.x = Frame.left ;
                }
                if (apertura.Equals("B_DR_E"))
                {
                    PointHandle.x = Frame.right;
                }
                if (!vista == false & designo=="Normal")
                {
                    // Bisagra superior
                    PointHandle.y = Frame.top - distanciaV;
                    imagen.DrawSymbol(biE, PointHandle);

                    // B. Inferior
                    PointHandle.y = Frame.bottom + distanciaV;
                    imagen.DrawSymbol(biE, PointHandle);

                    //B. Media
                    double mitad = Frame.height / 2;

                    PointHandle.y = Frame.bottom + mitad;
                    imagen.DrawSymbol(biE, PointHandle);

                }
            }
            //3. SECUNDARIA
            if (apertura.Equals("S_DR_I") || apertura.Equals("S_IZ_I") || apertura.Equals("S_DR_E") || apertura.Equals("S_IZ_E"))
            {
                string tipobisagra = hueco.Elemento.Opciones.Item("RO_PU_BISAGRA");
                string bisagra = "";
                string bInf = "";
                string cuerpo;
                string vte;
                string apert = "";
                double distancia = 200;
                bool aperturaE = false;

                if (apertura.Equals("S_DR_I") || apertura.Equals("S_DR_E"))
                {
                    apert = "DCHA"; 
                    PointHandle.x = Frame.right;
                    if (apertura.Equals("S_DR_I"))
                    {
                        aperturaE = false;
                    }
                    else
                    {
                        aperturaE = true;
                    }
                }
                if (apertura.Equals("S_IZ_I") || apertura.Equals("S_IZ_E"))
                {
                    apert = "IZDA";
                    PointHandle.x = Frame.left;
                    if (apertura.Equals("S_IZ_I"))
                    {
                        aperturaE = false;
                    }
                    else
                    {
                        aperturaE = true;
                    }
                }
                if (tipobisagra == "PS23" || tipobisagra == "150P")
                {
                    bisagra = "Option_HINGE_" + tipobisagra + "_" + apert;
                }
                else if (tipobisagra == "SOLID B")
                {
                    cuerpo = hueco.Elemento.Opciones.Item("RO_PU_SOLID_B_TIPO");
                    vte = cuerpo.Substring(0, 2);
                    bisagra = "Option_HINGE_" + tipobisagra + "_" + vte;
                }
                else if (tipobisagra == "PB10")
                {
                    bisagra = "Option_HINGE_" + tipobisagra;
                }
                else if (tipobisagra == "SCR" & aperturaE == false & designo == "Normal")
                {
                    distancia = 55;

                    if (apert== "DCHA")
                    {
                        bisagra = "Option_HINGE_Superior_DR";
                        bInf = "Option_HINGE_Inferior_DR";
                    }
                   else
                    {
                        bisagra = "Option_HINGE_Superior_IZ";
                        bInf = "Option_HINGE_Inferior_IZ";
                    }
                }

                // Bisagra Superior
                PointHandle.y = Frame.top - distancia;
                if (RotoTools.MuestraBisagra(aperturaE, vista))
                {
                    imagen.DrawSymbol(bisagra, PointHandle);
                }
                // Bisagra Inferior
                PointHandle.y = Frame.bottom + distancia;
                if (RotoTools.MuestraBisagra(aperturaE, vista))
                {
                    if (tipobisagra == "SCR")
                    {
                        imagen.DrawSymbol(bInf, PointHandle);
                    }
                    else
                    {
                        imagen.DrawSymbol(bisagra, PointHandle);
                    }
                }
                // Bisagras INTERMEDIAS
                if (hueco.Elemento.Opciones.Item("RO_PU_NUM_BISAGRAS").Equals("3 Bisagras") || (hueco.Elemento.Opciones.Item("RO_PU_NUM_BISAGRAS").Equals("4 Bisagras LC")))
                {
                    double mitad = Frame.height / 2;

                    //'Bisagra Central
                    PointHandle.y = Frame.bottom + mitad;
                    if (RotoTools.MuestraBisagra(aperturaE, vista))
                    {
                        imagen.DrawSymbol(bisagra, PointHandle);
                    }
                }
                if (hueco.Elemento.Opciones.Item("RO_PU_NUM_BISAGRAS").Equals("4 Bisagras LC"))
                {
                    double distanciaC = 410;

                    //'4º Bisagra Longitud Constante
                    PointHandle.y = Frame.top - distanciaC;
                    if (RotoTools.MuestraBisagra(aperturaE, vista))
                    {
                        imagen.DrawSymbol(bisagra, PointHandle);
                    }
                }

                if (hueco.Elemento.Opciones.Item("RO_PU_NUM_BISAGRAS").Equals("4 Bisagras LP"))
                {
                    double tercio = Frame.height / 3;

                    //'3ª Bisagra Longitud Promedio
                    PointHandle.y = Frame.bottom + tercio;
                    if (RotoTools.MuestraBisagra(aperturaE, vista))
                    {
                        imagen.DrawSymbol(bisagra, PointHandle);
                    }

                    //'4º Bisagra Longitud Promedio
                    PointHandle.y = Frame.top - tercio;
                    if (RotoTools.MuestraBisagra(aperturaE, vista))
                    {
                        imagen.DrawSymbol(bisagra, PointHandle);
                    }
                }
            }
            //4. ABATIBLE
            if (apertura.Equals("AB"))
            {
                PointHandle.x = Frame.bottom + 50;
                string bA = "Option_HINGE_Abatible";
                distanciaV = 100;
                double tercia = hueco.FFH / 3;


                if (vista == false)
                {
                    //'Bisagra Iz
                    PointHandle.x = Frame.left + distanciaV;
                    imagen.DrawSymbol(bA, PointHandle);

                    //'Bisagra Dr
                    PointHandle.x = Frame.right - distanciaV;
                    imagen.DrawSymbol(bA, PointHandle);

                    //'Bisagra Central
                    if ((hueco.FFH >= 801) && (hueco.FFH < 1600))
                    {
                        double media = hueco.FFH / 2; // Ojo que FFH esta aplicando la dimension de ancho
                        PointHandle.x = Frame.left + media + 20;
                        imagen.DrawSymbol(bA, PointHandle);
                    }
                    if ((hueco.FFH >= 1601) && (hueco.FFH < 2400))
                    {
                        PointHandle.x = Frame.left + tercia;
                        imagen.DrawSymbol(bA, PointHandle);

                        PointHandle.x = Frame.right - tercia;
                        imagen.DrawSymbol(bA, PointHandle);
                    }
                }
            }
            //5. PUERTA
            if (apertura.Equals("P_DR_I") || apertura.Equals("P_DR_E") || apertura.Equals("P_IZ_I") || apertura.Equals("P_IZ_E"))
            {
                string tipobisagra = "";
                string hardwareSupplier = hueco.Elemento.Opciones.Item("HardwareSupplier");

                if (hardwareSupplier == "ROTO NX")
                {
                    tipobisagra = hueco.Elemento.Opciones.Item("RO_PU_BISAGRA");
                }
                else if (hardwareSupplier == "ROTO NX ALU")
                {
                    tipobisagra = hueco.Elemento.Opciones.Item("RO_PU_AL_BISAGRA");
                }

                string bisagra;
                string cuerpo;
                string vte;
                string apert="";
                double distancia = 200;
                bool aperturaE = false;

                if (apertura.Equals("P_DR_I") || apertura.Equals("P_DR_E"))
                {
                    apert = "DCHA"; //ajustar los textos finales s/ option_hinge
                    PointHandle.x = Frame.right;
                    if (apertura.Equals("P_DR_I"))
                    {
                        aperturaE = false;
                    }
                    else
                    {
                        aperturaE = true;
                    }
                }
                if (apertura.Equals("P_IZ_I") || apertura.Equals("P_IZ_E"))
                {
                    apert = "IZDA";
                    PointHandle.x = Frame.left;
                    if (apertura.Equals("P_IZ_I"))
                    {
                        aperturaE = false;
                    }
                    else
                    {
                        aperturaE = true;
                    }
                }

                if (tipobisagra == "PS27" || tipobisagra == "PS23" || tipobisagra == "150P" || tipobisagra == "ATB80" || tipobisagra == "ATB120")
                {
                    bisagra = "Option_HINGE_" + tipobisagra + "_" + apert;
                }
                else if (tipobisagra == "SOLID B")
                {
                    cuerpo = hueco.Elemento.Opciones.Item("RO_PU_SOLID_B_TIPO");
                    vte = cuerpo.Substring(0, 2);
                    bisagra = "Option_HINGE_" + tipobisagra + "_" + vte;
                }
                else
                {
                    bisagra = "Option_HINGE_" + tipobisagra;
                }
                // Bisagra Superior
                PointHandle.y = Frame.top - distancia;
                if (RotoTools.MuestraBisagra (aperturaE, vista))
                {
                    imagen.DrawSymbol(bisagra, PointHandle);
                }
                // Bisagra Inferior
                PointHandle.y = Frame.bottom + distancia;
                if (RotoTools.MuestraBisagra(aperturaE, vista))
                {
                    imagen.DrawSymbol(bisagra, PointHandle);
                }
                // Bisagras INTERMEDIAS
                if (hueco.Elemento.Opciones.Item("RO_PU_NUM_BISAGRAS").Equals("3 Bisagras") || (hueco.Elemento.Opciones.Item("RO_PU_NUM_BISAGRAS").Equals("4 Bisagras LC")))
                {
                    double mitad = Frame.height / 2;

                    //'Bisagra Central
                    PointHandle.y = Frame.bottom + mitad;
                    if (RotoTools.MuestraBisagra(aperturaE, vista))
                    {
                        imagen.DrawSymbol(bisagra, PointHandle);
                    }
                }

                if (hueco.Elemento.Opciones.Item("RO_PU_NUM_BISAGRAS").Equals("4 Bisagras LC"))
                {
                    double distanciaC = 410;

                    //'4º Bisagra Longitud Constante
                    PointHandle.y = Frame.top - distanciaC;
                    if (RotoTools.MuestraBisagra(aperturaE, vista))
                    {
                        imagen.DrawSymbol(bisagra, PointHandle);
                    }
                }

                if (hueco.Elemento.Opciones.Item("RO_PU_NUM_BISAGRAS").Equals("4 Bisagras LP"))
                {
                    double tercio = Frame.height / 3;

                    //'3ª Bisagra Longitud Promedio
                    PointHandle.y = Frame.bottom + tercio;
                    if (RotoTools.MuestraBisagra(aperturaE, vista))
                    {
                        imagen.DrawSymbol(bisagra, PointHandle);
                    }

                    //'4º Bisagra Longitud Promedio
                    PointHandle.y = Frame.top - tercio;
                    if (RotoTools.MuestraBisagra(aperturaE, vista))
                    {
                        imagen.DrawSymbol(bisagra, PointHandle);
                    }
                }
            }
        }
    }
    public class DrawRotoHandle
    {
        public void RotoHandle(Interop.PrefCAD.Modelo model, Interop.PrefCAD.Hueco hueco, Interop.PrefCAD.ModelImage imagen)
        {
            //Determina la apertura desde RotoTools.RotoOpening
            string apertura = RotoTools.RotoOpening(hueco);
            bool vista = model.OuterViewMode;
            double am = hueco.AlturaManeta;

            Rectangulo rectManeta = new Rectangulo();
            Vertice PointHandle = new Vertice();
            Rectangulo frame = hueco.Elemento.Barra.Eje.Marco;

            string manilla = "";
            bool bombillo = false;
            double rot = 0;

            //1. VENTANA
            if (apertura.Equals("V_DR") || apertura.Equals("V_IZ") || apertura.Equals("AB"))
            {
                string valorManilla = hueco.Elemento.Opciones.Item("RO_MANILLA VENTANA");

                if (valorManilla == "Rotoline" || valorManilla == "Rotoline secustik")
                {
                    manilla = "Option_HANDLE_Rotoline";
                }
                else if (valorManilla == "Rotoline llave" || valorManilla == "Rotoline llave apertura logica")
                {
                    manilla = "Option_HANDLE_Rotoline llave";
                }
                else if (valorManilla == "Rotoswing" || valorManilla == "Rotoswing secustik")
                {
                    manilla = "Option_HANDLE_Rotoswing";
                }
                //Falta: Rotoswing llave
                else if (valorManilla == "Rotosamba" || valorManilla == "Rotosamba secustik")
                {
                    manilla = "Option_HANDLE_Rotosamba";
                }
                //Falta: Rotosamba llave
                if (vista == true)
                {
                    manilla = "";
                }
                // -------
                if (apertura.Equals("V_DR"))
                {
                    rectManeta.right = frame.left + 30;
                    rectManeta.top = frame.bottom + am;

                    //'Manilla derecha:
                    PointHandle.x = rectManeta.right;
                    PointHandle.y = rectManeta.top;
                }
                else if (apertura.Equals("V_IZ"))
                {
                    rectManeta.left = frame.right - 30;
                    rectManeta.top = frame.bottom + am;

                    //'Manilla izquierda:
                    PointHandle.x = rectManeta.left;
                    PointHandle.y = rectManeta.top;
                }
                else if (apertura.Equals("AB"))
                {
                    rectManeta.top = frame.top;
                    rot = 90;
                    Rectangulo Frame = hueco.Elemento.Barra.Eje.Marco;

                    //'Manilla superior:
                    double media = hueco.FFH / 2; // Ojo que FFH esta aplicando la dimension de ancho
                    PointHandle.x = Frame.left + media + 20;
                    PointHandle.y = rectManeta.top - 30;

                }
            }
            //2. BALCONERA
            else if (apertura.Equals("B_DR_I") || apertura.Equals("B_IZ_I") || apertura.Equals("B_DR_E") || apertura.Equals("B_IZ_E"))
            {
                // dibuja manilla balconera (segun valor opcion manilla balconera)
                string valorManilla = hueco.Elemento.Opciones.Item("RO_MANILLA BALCONERA");
                string valorBombillo = hueco.Elemento.Opciones.Item("RO_BOMBILLO");

                if (valorBombillo != "b_sin bombillo")
                {
                    bombillo = true;
                }

                if (valorManilla == "Manilla + Manilla" || valorManilla == "Manilla + Manilla plana" || valorManilla == "Solo manilla interior")
                {
                    if (bombillo == true)
                    {
                        manilla = "Option_HANDLE_MB";
                    }
                    else
                    {
                        manilla = "Option_HANDLE_Rotoline";
                    }

                    if (vista == true & valorManilla == "Solo manilla interior")
                    {
                        if (bombillo == true)
                        {
                            manilla = "Option_HANDLE_MB_S";
                        }
                        else
                        {
                            manilla = "";
                        }
                          
                    }
                }

                if (apertura.Equals("B_DR_I") || apertura.Equals("B_DR_E"))
                {
                    rectManeta.right = frame.left + 50;
                    rectManeta.top = frame.bottom + am;

                    //'Manilla derecha:
                    PointHandle.x = rectManeta.right;
                    PointHandle.y = rectManeta.top;
                }
                else if (apertura.Equals("B_IZ_I") || apertura.Equals("B_IZ_E"))
                {
                    rectManeta.left = frame.right - 50;
                    rectManeta.top = frame.bottom + am;

                    //'Manilla izquierda:
                    PointHandle.x = rectManeta.left;
                    PointHandle.y = rectManeta.top;
                }
            }



            //3. PUERTA Y SECUNDARIA
            else if (apertura.Equals("P_DR_I") || apertura.Equals("P_IZ_I") || apertura.Equals("P_DR_E") || apertura.Equals("P_IZ_E") ||
                apertura.Equals("S_DR_I") || apertura.Equals("S_IZ_I") || apertura.Equals("S_DR_E") || apertura.Equals("S_IZ_E"))
            {
                string valorManilla = hueco.Elemento.Opciones.Item("RO_MANILLA PUERTA");
                if (valorManilla == "Juego Manilla dos caras" || valorManilla == "Manilla + Manilla plana p.")
                {
                    if (apertura.Equals("P_DR_I") || apertura.Equals("P_DR_E") || apertura.Equals("S_DR_I") || apertura.Equals("S_DR_E"))
                    {
                        manilla = "Option_HANDLE_MP_IZ";
                    }
                    else if (apertura.Equals("P_IZ_I") || apertura.Equals("P_IZ_E") || apertura.Equals("S_IZ_I") || apertura.Equals("S_IZ_E"))
                    {
                        manilla = "Option_HANDLE_MP_DR";
                    }
                }
                if (valorManilla == "Manilla + Tirador")
                {
                    if (apertura.Equals("P_DR_I") || apertura.Equals("P_DR_E") || apertura.Equals("S_DR_I") || apertura.Equals("S_DR_E"))
                    {
                        if (vista == false)
                        {
                            manilla = "Option_HANDLE_MP_IZ";
                        }
                        else
                        {
                            manilla = "Option_HANDLE_MP_T_IZ";
                        }
                    }
                    else if (apertura.Equals("P_IZ_I") || apertura.Equals("P_IZ_E") || apertura.Equals("S_IZ_I") || apertura.Equals("S_IZ_E"))
                    {
                        if (vista == false)
                        {
                            manilla = "Option_HANDLE_MP_DR";
                        }
                        else
                        {
                            manilla = "Option_HANDLE_MP_T_DR";
                        }
                    }
                }
                if (valorManilla == "Manilla + Placa ciega")
                {
                    if (apertura.Equals("P_DR_I") || apertura.Equals("P_DR_E") || apertura.Equals("S_DR_I") || apertura.Equals("S_DR_E"))
                    {
                        if (vista == false)
                        {
                            manilla = "Option_HANDLE_MP_IZ";
                        }
                        else
                        {
                            manilla = "Option_HANDLE_MP_P";
                        }
                    }
                    else if (apertura.Equals("P_IZ_I") || apertura.Equals("P_IZ_E") || apertura.Equals("S_IZ_I") || apertura.Equals("S_IZ_E"))
                    {
                        if (vista == false)
                        {
                            manilla = "Option_HANDLE_MP_DR";
                        }
                        else
                        {
                            manilla = "Option_HANDLE_MP_P";
                        }
                    }
                }
                if (apertura.Equals("P_DR_I") || apertura.Equals("P_DR_E") || apertura.Equals("S_DR_I") || apertura.Equals("S_DR_E"))
                {
                    rectManeta.right = frame.left + 30;
                    rectManeta.top = frame.bottom + am;

                    //'Manilla derecha:
                    PointHandle.x = rectManeta.right;
                    PointHandle.y = rectManeta.top;
                }
                else if (apertura.Equals("P_IZ_I") || apertura.Equals("P_IZ_E") || apertura.Equals("S_IZ_I") || apertura.Equals("S_IZ_E"))
                {
                    rectManeta.left = frame.right - 30;
                    rectManeta.top = frame.bottom + am;

                    //'Manilla izquierda:
                    PointHandle.x = rectManeta.left;
                    PointHandle.y = rectManeta.top;
                }
            }
            //4. CORREDERA
            else if (apertura.Equals("C_DR") || apertura.Equals("C_IZ"))
            {
                // dibuja manilla corredera (segun valor opcion manilla ventana)
                string valorManillaInt = hueco.Elemento.Opciones.Item("RO_COR_MANILLA INTERIOR");
                string valorManillaExt = hueco.Elemento.Opciones.Item("RO_COR_MANILLA EXTERIOR");
                string valorBombillo = hueco.Elemento.Opciones.Item("RO_BOMBILLO");

                if (valorBombillo != "b_sin bombillo")
                {
                    bombillo = true;
                }
                if (valorManillaInt == "Cor. Rotoline" || valorManillaInt == "Cor. Rotoline secustik")
                {
                    if (bombillo == true)
                    {
                        manilla = "Option_HANDLE_RotoM+M";
                    }
                    else
                    {
                        manilla = "Option_HANDLE_Rotoline";
                    }
                }
                if (valorManillaInt == "Cor. Manilla uñero")
                {
                    if (bombillo == true)
                    {
                        manilla = "Option_HANDLE_RotoM+M";
                    }
                    else
                    {
                        manilla = "Option_HANDLE_Uñero";
                    }
                }
                if (valorManillaInt == "Cor. Tirador bombillo")
                {
                    if (apertura.Equals("C_IZ"))
                    {
                        manilla = "Option_HANDLE_MC_TB_DR";
                    }
                    else
                    {
                        manilla = "Option_HANDLE_MC_TB_IZ";
                    }
                }
                if (vista == true & valorManillaExt == "Cor. Manilla ext. Ninguna")
                {
                    manilla = "";
                }

                if (apertura.Equals("C_DR"))
                {
                    rectManeta.right = frame.left + 50;
                    rectManeta.top = frame.bottom + am;

                    //'Manilla derecha:
                    PointHandle.x = rectManeta.right;
                    PointHandle.y = rectManeta.top;
                }
                else if (apertura.Equals("C_IZ"))
                {
                    rectManeta.left = frame.right - 50;
                    rectManeta.top = frame.bottom + am;

                    //'Manilla izquierda:
                    PointHandle.x = rectManeta.left;
                    PointHandle.y = rectManeta.top;
                }
            }
            //5. PARALELA
            else if (apertura.Equals("OP_DR") || apertura.Equals("OP_IZ"))
            {
                string valorManilla = hueco.Elemento.Opciones.Item("RO_ALV_CREMONA");
                string selectManilla = hueco.Elemento.Opciones.Item("RO_ALV_MANILLA");

                if (valorManilla == "Cremona simple")
                {
                    manilla = "Option_HANDLE_MO";
                    if (vista == true & selectManilla != "Manilla dos caras")
                    {
                        manilla = "";
                    }
                }
                else
                {
                    manilla = "Option_HANDLE_MO_B";
                }
                if (apertura.Equals("OP_DR"))
                {
                    rectManeta.right = frame.left + 50;
                    rectManeta.top = frame.bottom + am;

                    //'Manilla derecha:
                    PointHandle.x = rectManeta.right;
                    PointHandle.y = rectManeta.top;
                }
                else if (apertura.Equals("OP_IZ"))
                {
                    rectManeta.left = frame.right - 50;
                    rectManeta.top = frame.bottom + am;

                    //'Manilla izquierda:
                    PointHandle.x = rectManeta.left;
                    PointHandle.y = rectManeta.top;
                }
            }
            //6. ELEVADORA
            else if (apertura.Equals("E_DR") || apertura.Equals("E_IZ"))
            {
                string valorManilla = hueco.Elemento.Opciones.Item("RO_ELV_MANILLA");

                if (valorManilla == "Manilla solo interior")
                {
                    manilla = "Option_HANDLE_ME";
                    if (vista == true)
                    {
                        manilla = "";
                    }
                }
                if (valorManilla == "Manilla 2 caras")
                {
                    manilla = "Option_HANDLE_ME";
                }
                if (valorManilla == "Manilla+Uñero")
                {
                    if (vista == false)
                    {
                        manilla = "Option_HANDLE_ME";
                    }
                    else
                    {
                        manilla = "Option_HANDLE_ME_U";
                    }
                }
                if (valorManilla == "Manilla cilindro+Uñero")
                {
                    if (vista == false)
                    {
                        manilla = "Option_HANDLE_ME_B";
                    }
                    else
                    {
                        manilla = "Option_HANDLE_ME_U";
                    }
                }
                if (valorManilla == "Manilla cilindro 2 caras")
                {
                    manilla = "Option_HANDLE_ME_B";
                }
                if (apertura.Equals("E_DR"))
                {
                    rectManeta.right = frame.left + 30;
                    rectManeta.top = frame.bottom + am;

                    //'Manilla derecha:
                    PointHandle.x = rectManeta.right;
                    PointHandle.y = rectManeta.top;
                }
                else if (apertura.Equals("E_IZ"))
                {
                    rectManeta.left = frame.right - 30;
                    rectManeta.top = frame.bottom + am;

                    //'Manilla izquierda:
                    PointHandle.x = rectManeta.left;
                    PointHandle.y = rectManeta.top;
                }
            }

            imagen.DrawSymbol(manilla, PointHandle, rot);

        }
    }
    public static class RotoTools
    {
        public static string RotoOpening(Interop.PrefCAD.Hueco hueco)
        {
            if (!hueco.Apertura.ToString().Equals("-1"))
            {
                string apert = "";
                string tipo = hueco.Elemento.Opciones.Item("RO_NX_EASY MIX");
                string sec = hueco.Elemento.Opciones.Item("RO_SEC_TIPO BALCONERA");

                //1. VENTANA, BALCONERA Y PUERTA SECUNDARIA
                if ((hueco.Apertura - TipoApertura.taActiva - TipoApertura.taPracticableDerecha - TipoApertura.taOscilobatienteInferior == 0) ||
                    (hueco.Apertura - TipoApertura.taPracticableDerecha - TipoApertura.taOscilobatienteInferior == 0) ||
                    (hueco.Apertura - TipoApertura.taActiva - TipoApertura.taPracticableDerecha == 0) ||
                    (hueco.Apertura - TipoApertura.taPracticableDerecha == 0) ||
                    (hueco.Apertura - TipoApertura.taActiva - TipoApertura.taPracticableDerecha - TipoApertura.taOscilobatienteInferior - TipoApertura.taCotaVariable == 0) ||
                    (hueco.Apertura - TipoApertura.taPracticableDerecha - TipoApertura.taOscilobatienteInferior - TipoApertura.taCotaVariable == 0) ||
                    (hueco.Apertura - TipoApertura.taActiva - TipoApertura.taPracticableDerecha - TipoApertura.taCotaVariable == 0) ||
                    (hueco.Apertura - TipoApertura.taPracticableDerecha - TipoApertura.taCotaVariable == 0))
                {
                    apert = "V_DR";
                    if (tipo == "Easy Mix_Si")
                    {
                        apert = "B_DR_I";
                        if (sec != "Balconera")
                        {
                            apert = "S_DR_I";
                        }
                    }
                }
                if ((hueco.Apertura - TipoApertura.taActiva - TipoApertura.taPracticableIzquierda - TipoApertura.taOscilobatienteInferior == 0) ||
                    (hueco.Apertura - TipoApertura.taPracticableIzquierda - TipoApertura.taOscilobatienteInferior == 0) ||
                    (hueco.Apertura - TipoApertura.taActiva - TipoApertura.taPracticableIzquierda == 0) ||
                    (hueco.Apertura - TipoApertura.taPracticableIzquierda == 0) ||
                    (hueco.Apertura - TipoApertura.taActiva - TipoApertura.taPracticableIzquierda - TipoApertura.taOscilobatienteInferior - TipoApertura.taCotaVariable == 0) ||
                    (hueco.Apertura - TipoApertura.taPracticableIzquierda - TipoApertura.taOscilobatienteInferior - TipoApertura.taCotaVariable == 0) ||
                    (hueco.Apertura - TipoApertura.taActiva - TipoApertura.taPracticableIzquierda - TipoApertura.taCotaVariable) == 0 ||
                    (hueco.Apertura - TipoApertura.taPracticableIzquierda == 0 - TipoApertura.taCotaVariable))
                    {
                        apert = "V_IZ";
                        if (tipo == "Easy Mix_Si")
                        {
                            apert = "B_IZ_I";
                            if (sec != "Balconera")
                            {
                                apert = "S_IZ_I";
                            }
                        }
                    }
                if ((hueco.Apertura - TipoApertura.taActiva - TipoApertura.taPracticableDerecha - TipoApertura.taExterior == 0) ||
                        (hueco.Apertura - TipoApertura.taPracticableDerecha - TipoApertura.taExterior == 0) ||
                        (hueco.Apertura - TipoApertura.taActiva - TipoApertura.taPracticableDerecha - TipoApertura.taExterior == 0) ||
                        (hueco.Apertura - TipoApertura.taPracticableDerecha - TipoApertura.taExterior == 0))
                    {
                        if (tipo == "Easy Mix_Si")
                        {
                            apert = "B_IZ_E";
                            if (sec != "Balconera")
                            {
                                apert = "S_IZ_E";
                            }
                        }
                    }
                if ((hueco.Apertura - TipoApertura.taActiva - TipoApertura.taPracticableIzquierda - TipoApertura.taExterior == 0) ||
                        (hueco.Apertura - TipoApertura.taPracticableIzquierda - TipoApertura.taExterior == 0) ||
                        (hueco.Apertura - TipoApertura.taActiva - TipoApertura.taPracticableIzquierda - TipoApertura.taExterior == 0) ||
                        (hueco.Apertura - TipoApertura.taPracticableIzquierda - TipoApertura.taExterior == 0))
                    {
                        if (tipo == "Easy Mix_Si")
                        {
                            apert = "B_DR_E";
                            if (sec != "Balconera")
                            {
                                apert = "S_DR_E";
                            }
                        }
                    }
                //2. ABATIBLE
                if ((hueco.Apertura - TipoApertura.taActiva - TipoApertura.taOscilobatienteInferior - TipoApertura.taCotaVariable == 0) ||
                    (hueco.Apertura - TipoApertura.taOscilobatienteInferior == 0))
                    {
                        apert = "AB";
                    }
                //3. PUERTA
                if ((hueco.Apertura - TipoApertura.taActiva - TipoApertura.taPracticableDerecha - TipoApertura.taPuerta == 0) ||
                    (hueco.Apertura - TipoApertura.taPracticableDerecha - TipoApertura.taPuerta == 0) ||
                    (hueco.Apertura - TipoApertura.taActiva - TipoApertura.taPracticableDerecha - TipoApertura.taPuerta - TipoApertura.taCotaVariable == 0) ||
                    (hueco.Apertura - TipoApertura.taPracticableDerecha - TipoApertura.taPuerta - TipoApertura.taCotaVariable == 0))
                    {
                        apert = "P_DR_I";
                    }
                if ((hueco.Apertura - TipoApertura.taActiva - TipoApertura.taPracticableIzquierda - TipoApertura.taPuerta - TipoApertura.taExterior == 0) ||
                    (hueco.Apertura - TipoApertura.taPracticableIzquierda - TipoApertura.taPuerta - TipoApertura.taExterior == 0) ||
                    (hueco.Apertura - TipoApertura.taActiva - TipoApertura.taPracticableIzquierda - TipoApertura.taPuerta - TipoApertura.taExterior - TipoApertura.taCotaVariable == 0) ||
                    (hueco.Apertura - TipoApertura.taPracticableIzquierda - TipoApertura.taPuerta - TipoApertura.taExterior - TipoApertura.taCotaVariable == 0))
                    {
                        apert = "P_DR_E";
                    }
                if ((hueco.Apertura - TipoApertura.taActiva - TipoApertura.taPracticableIzquierda - TipoApertura.taPuerta == 0) ||
                    (hueco.Apertura - TipoApertura.taPracticableIzquierda - TipoApertura.taPuerta == 0) ||
                    (hueco.Apertura - TipoApertura.taActiva - TipoApertura.taPracticableIzquierda - TipoApertura.taPuerta - TipoApertura.taCotaVariable == 0) ||
                    (hueco.Apertura - TipoApertura.taPracticableIzquierda - TipoApertura.taPuerta - TipoApertura.taCotaVariable == 0))
                    {
                        apert = "P_IZ_I";
                    }
                if ((hueco.Apertura - TipoApertura.taActiva - TipoApertura.taPracticableDerecha - TipoApertura.taPuerta - TipoApertura.taExterior == 0) ||
                    (hueco.Apertura - TipoApertura.taPracticableDerecha - TipoApertura.taPuerta - TipoApertura.taExterior == 0) ||
                    (hueco.Apertura - TipoApertura.taActiva - TipoApertura.taPracticableIzquierda - TipoApertura.taPuerta - TipoApertura.taExterior - TipoApertura.taCotaVariable == 0) ||
                    (hueco.Apertura - TipoApertura.taPracticableIzquierda - TipoApertura.taPuerta - TipoApertura.taExterior - TipoApertura.taCotaVariable == 0))
                    {
                        apert = "P_IZ_E";
                    }
                //4. CORREDERA
                // No encuentro mejor metodo para determinar aperturas cuando hay corredera involucrada: Revisar tras consulta.
                int taE = Convert.ToInt32(hueco.Apertura);
                if (taE == 51200)
                {
                    apert = "C_DR";
                }
                if (taE == 50176)
                {
                    apert = "C_IZ";
                }
                //5. PARALELA
                if (taE == 51216)
                {
                    apert = "OP_DR";
                }
                if (taE == 50192)
                {
                    apert = "OP_IZ";
                }
                //6. ELEVADORA
                if (taE == 25600)
                {
                    apert = "E_IZ";
                }
                if (taE == 26624)
                {
                    apert = "E_DR";
                }
                //
                return apert;
            }
            else
                return "";
        }

        public static bool MuestraBisagra(bool apertura, bool vista)
        {
            if (apertura && vista)
                return true;
            if (apertura && !vista)
                return false;
            if (!apertura && vista)
                return false;
            return !apertura && !vista; // Devuelve true si ambas son false
        }

    }
}
