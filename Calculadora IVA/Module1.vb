
Module Module1

    Private ruta As String = IO.Path.GetDirectoryName(Diagnostics.Process.GetCurrentProcess().MainModule.FileName)
    Private ivafile As String
    Dim iva As Integer

    Sub Main()
        Console.Title = "Calculadora de IVA"
        ' Comprobar si existe un archivo para guardar el IVA, y si es correcto.
        If System.IO.File.Exists(ruta & "\iva.txt") Then
            ivafile = System.IO.File.ReadAllText(ruta & "\iva.txt")
            Do Until iva > 0
                If ivafile.LongCount > 2 Then
                    ' Error porque el IVA no puede tener más de 2 cifras.
                    Console.WriteLine("El IVA almacenado es inválido. ¿Con qué cantidad de IVA desea trabajar?")
                    PedirIVA()
                Else
                    Try
                        iva = ivafile
                    Catch ex As Exception
                        ' Error porque el archivo contiene carácteres no numéricos.
                        Console.WriteLine("El IVA almacenado es inválido. ¿Con qué cantidad de IVA desea trabajar?")
                        PedirIVA()
                    End Try
                End If
            Loop
        Else
            Console.WriteLine("¿Con qué cantidad de IVA desea trabajar?")
            PedirIVA()
        End If
        Console.WriteLine("El IVA es de " & ivafile & "%")


        Console.WriteLine("¿Qué desea hacer? Calcular precios | Cambiar IVA | Salir")
        Select Case Console.ReadLine
            Case Is = "Calcular precio"
                Trabajo()
            Case Is = "Cambiar IVA"
                Console.WriteLine("¿Qué IVA desea establecer?")
                PedirIVA()
                Main()
            Case Is = "Salir"
                Environment.Exit(0)

        End Select
        Main()


    End Sub

    Sub Trabajo()
        Console.WriteLine("¿A qué precio desea calcularle el IVA?")
        ' Comenzar a hacer operaciones
        Dim precio As Integer = 0
        Dim ivafinal As Integer

        Do Until precio > 0
            Dim respuestaprecio As String = Console.ReadLine
            If respuestaprecio.Equals("Salir") Then
                Main()

            End If
            Try
                precio = respuestaprecio
                ivafinal = precio / 100 * iva
                Console.WriteLine("El IVA de " & precio & " es " & ivafinal & ". Sumando un precio total de " & precio + ivafinal & ".")

            Catch ex As Exception

                Console.WriteLine("No ha introducido un número válido")
            End Try
        Loop
        Trabajo()

    End Sub

    Sub PedirIVA()
        ivafile = Console.ReadLine()
        If ivafile.LongCount > 2 Then
            Console.WriteLine("¿Estamos locos? ¿Un IVA tan grande? dime el correcto.")
            PedirIVA()
        Else
            Try
                System.IO.File.WriteAllText(ruta & "\iva.txt", ivafile)
            Catch ex As Exception
                Console.WriteLine(ex.Message)
            End Try
            Try
                iva = ivafile
            Catch ex As Exception
                Console.WriteLine("Escríbemelo en números")
                PedirIVA()
            End Try
        End If

    End Sub

End Module
