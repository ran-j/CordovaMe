Imports System.IO
Imports System.Threading
Public Class CriarProjeto
    Dim caminho As String = Gobala.caminho
    Dim retorno As String
    Private t1 As Thread
    Dim p As New wait

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox2.Text.Length < 3 Then
            caminho = TextBox2.Text
        End If

        If TextBox1.Text.Contains(" ") Then
            MsgBox("O nome não pode conter espaço")
            TextBox1.Text = TextBox1.Text.Replace(" ", "-")

        ElseIf TextBox1.Text.Length < 1 Then
            MsgBox("Digite o nome do projeto")

        ElseIf caminho.Length < 3 Then
            MsgBox("Caminho inválido")
            Button1.PerformClick()

        ElseIf Directory.Exists(caminho + "\" + TextBox1.Text) Then
            MsgBox("Esse projeto já existe")
            caminho = caminho + "\" + TextBox1.Text
            abrirpasta()
        Else

            Try

                t1 = New Thread(AddressOf Me.criarp)
                t1.Start()
                p.ShowDialog()

                TextBox1.Clear()
                TextBox2.Clear()
                Me.Dispose()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Principal.FolderBrowserDialog1.ShowDialog()
        caminho = Principal.FolderBrowserDialog1.SelectedPath

        If caminho.Length > 3 Then
            Gobala.caminho = caminho + "\" + TextBox1.Text
            TextBox2.Text = caminho
        End If

    End Sub

    Sub abrirpasta()
        Try
            Process.Start("Explorer", caminho + "\" + TextBox1.Text)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        On Error Resume Next
        If (caminho.Length > 3) Then
            TextBox2.Text = caminho
        Else
            TextBox2.Clear()
        End If
    End Sub

    Sub criarp()
        Dim saida As Integer = 2
        Dim SW As New StreamWriter(Path.GetTempPath() + "\criar.bat")
        SW.WriteLine("@echo off &echo.&echo Aguarde..& mode 60,20 & cd " + caminho + "& cordova create " + TextBox1.Text + ">>outp.cord")
        SW.Close()
        SW.Dispose()

        Dim proc As New Process()

        proc.StartInfo.FileName = Path.GetTempPath() + "\criar.bat"
        proc.Start()
        proc.WaitForExit()

        For Each linha As String In File.ReadAllLines(caminho + "\outp.cord")

            If linha.Contains("Creating a new cordova project.") Then
                saida = 1
            Else
                saida = 2
            End If

        Next

        If saida = 1 Then
            MsgBox("Projeto criado")
            abrirpasta()
        Else
            MsgBox("Erro ao criar o projeto")
        End If


        Gobala.fehca = 1

        My.Computer.FileSystem.DeleteFile(Path.GetTempPath() + "\criar.bat")
        My.Computer.FileSystem.DeleteFile(caminho + "\outp.cord")

    End Sub

    Private Sub TextBox1_Leave(sender As Object, e As EventArgs) Handles TextBox1.Leave
        TextBox1.Text = TextBox1.Text.Replace(" ", "-")
    End Sub
End Class