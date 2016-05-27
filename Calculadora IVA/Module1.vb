
Module Module1

    Private rutaArchivoIVA As String = IO.Path.GetDirectoryName(Diagnostics.Process.GetCurrentProcess().MainModule.FileName) & IO.Path.DirectorySeparatorChar & "iva.txt"
    Private contenidoArchivoIVA As String
    Dim valorIVA As Double

    Sub Main()
        Console.Title = "Calculadora de IVA"
        Do
            Console.Clear()
            CargarIVA()

            Console.WriteLine("¿Qué desea hacer? 1. Calcular precio | 2. Cambiar IVA | 3. Salir")
            Select Case Console.ReadLine
                Case Is = "Calcular precio", "1"
                    Trabajo()

                Case Is = "Cambiar IVA", "2"
                    PedirIVA()

                Case Is = "Salir", "3"
                    Environment.Exit(0)

            End Select
        Loop


    End Sub

    Private Sub CargarIVA()
        ' Comprobar si existe un archivo para guardar el IVA, y si es correcto.
        If System.IO.File.Exists(rutaArchivoIVA) Then
            contenidoArchivoIVA = System.IO.File.ReadAllText(rutaArchivoIVA)
            Do Until valorIVA > 0
                If contenidoArchivoIVA.LongCount > 2 Then
                    ' Error porque el IVA no puede tener más de 2 cifras.
                    Console.WriteLine("El IVA almacenado es inválido. ¿Con qué cantidad de IVA desea trabajar?")
                    PedirIVA()
                Else
                    Try
                        valorIVA = contenidoArchivoIVA
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
        Console.WriteLine("El IVA es de " & contenidoArchivoIVA & "%")

    End Sub

    Sub PedirIVA()
        Console.Clear()

        Do
            Console.WriteLine("¿Qué IVA desea establecer?")

            Try
                valorIVA = Console.ReadLine()
                System.IO.File.WriteAllText(rutaArchivoIVA, valorIVA)
                Return

            Catch ex As Exception
                Console.WriteLine(ex.Message)
            End Try


        Loop


    End Sub

    Sub Trabajo()
        Console.Clear()

        Do
            Console.WriteLine("¿A qué precio desea calcularle el IVA? escriba Salir para volver al menú.")

            Dim precio As Double = 0
            Dim ivafinal As Double

            Do Until precio > 0
                Dim respuestaprecio As String = Console.ReadLine
                If respuestaprecio.Equals("Salir") Then
                    Main()

                End If
                Try
                    precio = respuestaprecio
                    ivafinal = precio / 100 * valorIVA
                    Console.WriteLine("El IVA de " & precio & " es " & ivafinal & ". Sumando un precio total de " & precio + ivafinal & ".")

                Catch ex As Exception

                    Console.WriteLine("No ha introducido un número válido")
                End Try
            Loop
        Loop

    End Sub

End Module
