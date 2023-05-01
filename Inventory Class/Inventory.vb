Public Class Inventory
    Public Property InvNumber As String
    Public Property Description As String

    Public Property Cost As Decimal

    Public Property Retail As Decimal

    Public Property OnHand As Integer


    Public Sub New(invNumber As String, description As String, cost As Decimal, retail As Decimal, onHand As Integer)
        Me.InvNumber = invNumber
        Me.Description = description
        Me.Cost = cost
        Me.Retail = retail
        Me.OnHand = onHand
    End Sub
End Class
