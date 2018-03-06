Imports System.IO
Imports System.Security.Cryptography

Public Class Form1

    Function hash_generator(ByVal hash_type As String, ByVal file_name As String)

        Dim hash

        If hash_type.ToLower = "md5" Then
            hash = MD5.Create
        ElseIf hash_type.ToLower = "sha1" Then
            hash = SHA1.Create()
        ElseIf hash_type.ToLower = "sha256" Then
            hash = SHA256.Create()
        Else
            MsgBox("Unknown type of hash : " & hash_type, MsgBoxStyle.Critical)
            Return False
        End If

        Dim hashValue() As Byte
        Dim fileStream As FileStream = File.OpenRead(file_name)
        fileStream.Position = 0
        hashValue = hash.ComputeHash(fileStream)
        Dim hash_hex = PrintByteArray(hashValue)
        fileStream.Close()
        Return hash_hex

    End Function

    Public Function PrintByteArray(ByVal array() As Byte)

        Dim hex_value As String = ""
        Dim i As Integer
        For i = 0 To array.Length - 1
            hex_value += array(i).ToString("X2")
        Next i
        Return hex_value.ToLower
    End Function

    Function md5_hash(ByVal file_name As String)
        Return hash_generator("md5", file_name)
    End Function

    Function sha_1(ByVal file_name As String)
        Return hash_generator("sha1", file_name)
    End Function

    Function sha_256(ByVal file_name As String)
        Return hash_generator("sha256", file_name)
    End Function

    Public ActiveTextBox As TextBox

    Private Sub TextBox_Enter(sender As Object, e As EventArgs) Handles TextBox2.Enter, TextBox3.Enter, TextBox4.Enter
        ActiveTextBox = CType(sender, TextBox)
    End Sub

    Private Sub TextBox_MouseDown() Handles TextBox2.MouseDown, TextBox3.MouseDown, TextBox4.MouseDown

        ActiveTextBox.SelectAll()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim openfile As New OpenFileDialog()
        If (openfile.ShowDialog = DialogResult.OK) Then
            TextBox1.Text = openfile.FileName
            Dim path As String = TextBox1.Text
            TextBox2.Text = md5_hash(path)
            TextBox3.Text = sha_1(path)
            TextBox4.Text = sha_256(path)
        End If
        Timer1.Start()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Select Case TextBox5.Text
            Case TextBox2.Text
                TextBox3.BackColor = Color.White
                TextBox4.BackColor = Color.White
                TextBox2.BackColor = Color.Green
                TextBox5.BackColor = Color.Green
            Case TextBox3.Text
                TextBox2.BackColor = Color.White
                TextBox4.BackColor = Color.White
                TextBox3.BackColor = Color.Green
                TextBox5.BackColor = Color.Green
            Case TextBox4.Text
                TextBox2.BackColor = Color.White
                TextBox3.BackColor = Color.White
                TextBox4.BackColor = Color.Green
                TextBox5.BackColor = Color.Green
            Case ""
                TextBox2.BackColor = Color.White
                TextBox3.BackColor = Color.White
                TextBox4.BackColor = Color.White
                TextBox5.BackColor = Color.White
            Case Else
                TextBox2.BackColor = Color.White
                TextBox3.BackColor = Color.White
                TextBox4.BackColor = Color.White
                TextBox5.BackColor = Color.Red
        End Select
    End Sub
End Class
