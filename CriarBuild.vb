Imports System.IO
Imports System.Threading

Public Class CriarBuild
    Dim olugar As String = Gobala.caminho
    Dim retorno As String
    Dim ola As String
    Dim p As New wait
    Private t1 As Thread


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Principal.FolderBrowserDialog1.ShowDialog()
        olugar = Principal.FolderBrowserDialog1.SelectedPath

        If olugar.Length > 3 Then
            If Directory.Exists(olugar + "\www") Or File.Exists(olugar + "\config.xml") Or File.Exists(olugar + "\index.html") Then
                My.Settings.lugarsalvo = olugar
                Gobala.caminho = olugar
                TextBox1.Text = olugar
            Else
                MsgBox("Essa pasta não contem um projeto Cordova")
            End If

        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text.Length < 3 Then
            MsgBox("Pasta inválida")
        ElseIf (ComboBox1.SelectedIndex = -1) Then
            MsgBox("Selecione a plataforma para adicionar")
        Else
            olugar = TextBox1.Text
            If Directory.Exists(olugar + "\www") Or File.Exists(olugar + "\config.xml") Or File.Exists(olugar + "\index.html") Then
                My.Settings.lugarsalvo = olugar
                If Directory.Exists(olugar + "\platforms\android") And File.Exists(olugar + "\platforms\platforms.json") Then
                    If ComboBox1.SelectedItem.Equals("Android") Then
                        criarbuild("android")
                    ElseIf ComboBox1.SelectedItem.Equals("IOS") Then
                        criarbuild("ios")
                    ElseIf ComboBox1.SelectedItem.Equals("Windows Phone") Then
                        criarbuild("windows")
                    End If
                Else
                    MsgBox("O projeto não possui plataforma")
                End If

            Else
                MsgBox("Essa pasta não contem um projeto Cordova")
            End If
        End If
    End Sub

    Sub criarbuild(ByVal plata As String)
        Try
            ola = plata

            t1 = New Thread(AddressOf Me.backgroud)
            t1.Start()

            p.ShowDialog()

            TextBox1.Clear()

            Me.Dispose()

        Catch ex As Exception
            MsgBox(ex.Message)

        End Try
    End Sub

    Sub abrepastadaapk(ByVal aplata As String)
        Dim abrir As String

        If aplata.Equals("android") Then
            abrir = olugar + "\platforms\android\build\outputs\apk"
            If Directory.Exists(abrir) And File.Exists(abrir + "\android-debug.apk") Then
                Process.Start("Explorer", abrir)
            Else
                MsgBox("Erro ao localizar a apk")
            End If

        End If

    End Sub

    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox1.SelectedIndex = 0
        On Error Resume Next
        If (olugar.Length > 3) Then
            TextBox1.Text = olugar
        Else
            TextBox1.Clear()
        End If
    End Sub

    Sub backgroud()
        Dim saida As Integer = 2
        Dim SW As New StreamWriter(Path.GetTempPath() + "\criar.bat")
        SW.WriteLine("@echo off &echo.&echo Aguarde..& mode 60,20 & cd " + olugar + "& cordova build " + ola + ">>outp.cord")
        SW.Close()
        SW.Dispose()

        Dim proc As New Process()

        proc.StartInfo.FileName = Path.GetTempPath() + "\criar.bat"
        proc.Start()
        proc.WaitForExit()

        For Each linha As String In File.ReadAllLines(olugar + "\outp.cord")

            If linha.Contains("BUILD SUCCESSFUL") Then
                saida = 1
            End If

        Next

        If saida = 1 Then
            MsgBox("Build Criada")
            abrepastadaapk(ola)
        Else
            MsgBox("Erro ao iniciar a Build")
        End If


        Gobala.fehca = 1

        My.Computer.FileSystem.DeleteFile(Path.GetTempPath() + "\criar.bat")
        My.Computer.FileSystem.DeleteFile(olugar + "\outp.cord")


    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If TextBox1.Text.Length < 3 Then
            MsgBox("Selecione o projeto")
        Else
            olugar = TextBox1.Text
            Dim abrir = olugar + "\platforms\android\build\outputs\apk"
            If Directory.Exists(abrir) And File.Exists(abrir + "\android-debug.apk") Then
                Process.Start("Explorer", abrir)
            Else
                If Not Directory.Exists(abrir + "\platforms\android") Then
                    MsgBox("O projeto não possui plataforma android")
                Else
                    MsgBox("Erro ao localizar a apk")
                End If
            End If
        End If
    End Sub
End Class