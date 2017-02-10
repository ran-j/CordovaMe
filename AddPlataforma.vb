Imports System.IO
Imports System.Threading

Public Class AddPlataforma

    Private t1 As Thread
    Dim local As String = Gobala.caminho
    Dim aplataa As String
    Dim p As New wait
    Dim retorno As String


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Not Directory.Exists(TextBox1.Text) Then
            MsgBox("Essa pasta não existe")
        ElseIf TextBox1.Text.Length < 3 Then
            MsgBox("Selecione o caminho do projeto")
        ElseIf (ComboBox1.SelectedIndex = -1) Then
            MsgBox("Selecione a plataforma para adicionar")
        Else
            If ComboBox1.SelectedItem.Equals("Android") Then
                addplatabforma("android")
            ElseIf ComboBox1.SelectedItem.Equals("IOS") Then
                addplatabforma("ios")
            ElseIf ComboBox1.SelectedItem.Equals("Windows Phone") Then
                addplatabforma("windows")
            End If
        End If
    End Sub


    Sub addplatabforma(ByVal plataforma As String)
        Try
            If Directory.Exists(local + "\platforms\" + plataforma) Then
                MsgBox("Essa plataforma já está no projeto")
            Else

                aplataa = plataforma

                t1 = New Thread(AddressOf Me.addplata)
                t1.Start()

                p.ShowDialog()

                TextBox1.Clear()
                ComboBox1.SelectedIndex = -1

                Me.Dispose()


            End If
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Principal.FolderBrowserDialog1.ShowDialog()
        local = Principal.FolderBrowserDialog1.SelectedPath

        If local.Length > 3 Then

            If Directory.Exists(local + "\www") Or File.Exists(local + "\config.xml") Or File.Exists(local + "\index.html") Then
                Gobala.caminho = local
                TextBox1.Text = local
            Else
                MsgBox("Essa pasta não contem um projeto Cordova")
            End If

        End If

    End Sub

    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox1.SelectedIndex = 0
        On Error Resume Next
        If (local.Length > 3) Then
            TextBox1.Text = local
        Else
            TextBox1.Clear()
        End If
    End Sub

    Sub addplata()
        Dim saida As Integer = 2
        Dim SW As New StreamWriter(Path.GetTempPath() + "\criar.bat")
        SW.WriteLine("@echo off &echo.&echo Aguarde..& mode 60,20 & cd " + local + "& cordova platform add " + aplataa + " --save>>outp.cord")
        SW.Close()
        SW.Dispose()

        Dim proc As New Process()

        proc.StartInfo.FileName = Path.GetTempPath() + "\criar.bat"
        proc.Start()
        proc.WaitForExit()

        For Each linha As String In File.ReadAllLines(local + "\outp.cord")

            If linha.Contains("BUILD SUCCESSFUL") Then
                saida = 1
            End If

        Next

        If saida = 1 Then
            MsgBox("Plataforma Adicionada")
        Else
            MsgBox("Erro ao adicionar a plataforma")
        End If


        Gobala.fehca = 1

        My.Computer.FileSystem.DeleteFile(Path.GetTempPath() + "\criar.bat")
        My.Computer.FileSystem.DeleteFile(local + "\outp.cord")


    End Sub


End Class