Imports Microsoft.VisualBasic

Public Class Class1
    Sub Main()
        Dim x = 5

        For Each y In x
            If (y > 5) Then
                Exit For
            End If
        Next

        If x = 5 Then
            Exit Sub

        ElseIf x = 6 Then
            Exit Sub
        ElseIf x = 7 Then
            Exit Sub
        End If

        Select Case x
            Case 1
                Exit Sub
            Case 5
                Exit Sub
            Case Else
                Exit Sub
        End Select
    End Sub
End Class
