Imports System.IO

Public Class Form1

    Private inventoryList As New List(Of Inventory)
    Private salesTaxRate As Decimal = 0.06
    Private nudQuantity As Object

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim selectedItem As Inventory = GetSelectedInventoryItem()
        If selectedItem Is Nothing Then
            MessageBox.Show("Please select an item from the list.")
            Return
        End If

        Dim quantity As Integer = CInt(nudQuantity.Value)
        If quantity = 0 Then
            MessageBox.Show("Please enter a quantity greater than zero.")
            Return
        End If

        If quantity > selectedItem.OnHand Then
            MessageBox.Show("The quantity entered is greater than the number of units on hand.")
            Return
        End If

        Dim subTotal As Decimal = selectedItem.Retail * quantity
        Dim salesTax As Decimal = subTotal * salesTaxRate
        Dim total As Decimal = subTotal + salesTax

        selectedItem.OnHand -= quantity
        UpdateInventoryListBox()

        txtSubTotal.Text = subTotal.ToString("C")
        txtSalesTax.Text = salesTax.ToString("C")
        txtTotal.Text = total.ToString("C")
    End Sub

    Private Function GetSelectedInventoryItem() As Inventory
        Throw New NotImplementedException()
    End Function

    Private Sub inventoryList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles inventoryList1.SelectedIndexChanged
        Dim selectedItem As Inventory = GetSelectedInventoryItem()
        If selectedItem IsNot Nothing Then
            txtDescription.Text = selectedItem.Description
            txtRetailPrice.Text = selectedItem.Retail.ToString("C")
            txtOnHand.Text = selectedItem.OnHand.ToString()
            NumericUpDown1.Maximum = selectedItem.OnHand
        Else
            txtDescription.Text = 0
            txtRetailPrice.Text = 0
            txtOnHand.Text = 0
            NumericUpDown1.Maximum = 0
        End If
    End Sub
    Private Sub LoadInventoryFromFile(filename As String)
        Try
            Using reader As New StreamReader(filename)
                While Not reader.EndOfStream
                    Dim line As String = reader.ReadLine()
                    Dim fields As String() = line.Split(",")
                    Dim invNumber As String = fields(0)
                    Dim description As String = fields(1)
                    Dim cost As Decimal = Decimal.Parse(fields(2))
                    Dim retail As Decimal = Decimal.Parse(fields(3))
                    Dim onHand As Integer = Integer.Parse(fields(4))
                    Dim newItem As New Inventory(invNumber, description, cost, retail, onHand)
                    inventoryList1.Items.Add(newItem)
                End While
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error")
        End Try
    End Sub
    Private Sub UpdateInventoryListBox()
        inventoryList1.Items.Clear()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadInventoryFromFile("inventory.txt")
        UpdateInventoryListBox()
    End Sub
End Class
