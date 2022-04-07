Imports System

Public Class Data
    Public Property dollarValue As Integer
    Public Property centValue As Integer
End Class
Public Class NumberToWords
    Private userInputNum As String

    Public Sub AcceptNumber()
        userInputNum = Console.ReadLine()
    End Sub

    Public Function GetWord(userInputNum As String) As String

        Dim beforePoint As String
        Dim afterPoint As String
        Dim pointPosition As Integer = 10000
        Dim dollarValue As Integer
        Dim centValue As Integer
        Dim dollarUnit As String = "dollars"
        Dim centUnit As String = "cents"
        Dim aValue As Double
        If Double.TryParse(userInputNum, aValue) Then
            For a = 0 To userInputNum.Length - 1
                If (userInputNum(a) <> "." And pointPosition > a) Then
                    beforePoint += userInputNum(a)
                ElseIf (userInputNum(a) = ".") Then
                    pointPosition = a
                ElseIf (a > pointPosition) Then
                    afterPoint += userInputNum(a)
                End If
            Next

            Try
                dollarValue = Convert.ToInt32(beforePoint)
                centValue = Convert.ToInt32(afterPoint)
                If Integer.TryParse(afterPoint, centValue) Then
                    If afterPoint.Length = 1 Then
                        centValue *= 10
                    ElseIf afterPoint.Length > 2 Then
                        Dim lastNum As String
                        lastNum = afterPoint.Substring(2).ToString()
                        afterPoint = afterPoint.Substring(0, 2)
                        centValue = Convert.ToInt32(afterPoint)
                        If (Convert.ToInt32(lastNum) >= 5) Then
                            centValue += 1
                        End If


                    End If
                    If (centValue = 0 Or centValue = 100) Then
                        dollarValue += 1
                        centValue = 0
                        centUnit = "cent"
                    End If
                End If


                beforePoint = ConvertNumberToWord(dollarValue)
                afterPoint = ConvertNumberToWord(centValue)

                If dollarValue = 0 Then
                    GetWord = String.Format("{0} {1}.", afterPoint, centUnit)
                Else
                    If centValue = 0 Then
                        GetWord = String.Format("{0} {1}.", beforePoint, dollarUnit)
                    Else
                        GetWord = String.Format("{0} {1} and {2} {3}.", beforePoint, dollarUnit, afterPoint, centUnit)
                    End If

                End If
            Catch ex As Exception
                GetWord = "Cannot covert using ToInt32"

            End Try


        Else
            GetWord = "Input is invalid"
        End If
    End Function

    Public Function ConvertNumberToWord(a As Integer)
        Dim word As String = ""
        If (a < 10 And a >= 0) Then
            word += GetUnit(a)

        ElseIf (a >= 10 And a < 20) Then
            word += GetTwoDigits(a)

        ElseIf (a >= 20 And a < 100) Then
            word += GetTenMultiply(a \ 10)

            If (a Mod 10 <> 0) Then
                word += " " + ConvertNumberToWord(a Mod 10)
            End If

        ElseIf (a >= 100 And a < 1000) Then
            word += GetUnit(a \ 100) + " "
            word += GetTenPow(2)

            If (a Mod 100 <> 0) Then
                word += " and " + ConvertNumberToWord(a Mod 100)
            End If

        ElseIf (a >= 1000 And a < 1000000) Then
            word += ConvertNumberToWord(a \ 1000)
            word += " " + GetTenPow(3)

            If (a Mod 1000 <> 0) Then
                word += ", " + ConvertNumberToWord(a Mod 1000)
            End If

        ElseIf (a >= 1000000 And a < 1000000000) Then
            word += ConvertNumberToWord(a \ 1000000)
            word += " " + GetTenPow(6)

            If (a Mod 1000000 <> 0) Then
                word += ", " + ConvertNumberToWord(a Mod 1000000)
            End If

        Else
            word = "It is too big.!"

        End If

        ConvertNumberToWord = word
    End Function

    Public Function GetUnit(a As Integer)
        Dim unit() As String = {"Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine"}
        GetUnit = unit(a)
    End Function

    Public Function GetTwoDigits(a As Integer)
        Dim unit() As String = {"Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"}
        GetTwoDigits = unit(a Mod 10)
    End Function

    Public Function GetTenMultiply(a As Integer)
        Dim unit() As String = {"Twenty", "Thirty", "Fourty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"}
        GetTenMultiply = unit(a - 2)
    End Function

    Public Function GetTenPow(a As Integer)
        Dim power() As String = {"Hundred", "Thousand", "", "", "Million"}
        GetTenPow = power(a - 2)
    End Function

    Public Sub Dipsplay()
        ' Take input from keyboard
        ' Dim s As String = userInputNum
        ' Console.WriteLine("Number: {0}", s)
        ' Console.WriteLine("Word: {0}", GetWord(s))

        ' Take input from TestInputs.txt
        Dim FILE_NAME As String = "TestInputs.txt"
        If System.IO.File.Exists(FILE_NAME) = True Then
            Dim objReader As New System.IO.StreamReader(FILE_NAME)
            Do While objReader.Peek() <> -1
                userInputNum = objReader.ReadLine()
                Console.WriteLine("Value: ${0}", userInputNum)
                Console.WriteLine("Word: {0}", GetWord(userInputNum))
            Loop
        Else
            Console.WriteLine("File Does Not Exist")
        End If
    End Sub

    Shared Sub Main()
        Dim r As New NumberToWords()

        ' take input from keyboard
        'r.AcceptNumber()

        r.Dipsplay()
        Console.ReadLine()
    End Sub

End Class