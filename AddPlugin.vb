Imports System.IO
Imports System.Threading

Public Class AddPlugin
    Dim olugar As String = Gobala.caminho

    Private t1 As Thread

    Dim aplataa As String
    Dim ooplugin As String
    Dim retorno As String

    Dim p As New wait
    Private fechar As Integer = 0


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Principal.FolderBrowserDialog1.ShowDialog()
        olugar = Principal.FolderBrowserDialog1.SelectedPath

        If olugar.Length > 3 Then
            If Directory.Exists(olugar + "\www") Or File.Exists(olugar + "\config.xml") Or File.Exists(olugar + "\index.html") Then
                Gobala.caminho = olugar
                TextBox1.Text = olugar
            Else
                MsgBox("Essa pasta não contem um projeto Cordova")
            End If

        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text.Length < 3 Then
            olugar = TextBox1.Text
            My.Settings.lugarsalvo = TextBox1.Text
        End If

        If olugar.Length < 3 Then
            MsgBox("Selecione o caminho do projeto")
        ElseIf (ComboBox1.SelectedIndex = -1) Then
            MsgBox("Selecione o plugin para adicionar")
        Else
            If ComboBox1.SelectedItem.Equals("Camera") Then
                addplugin("camera")
            ElseIf ComboBox1.SelectedItem.Equals("Diagnostic") Then
                addplugin("cordova.plugins.diagnostic")
            ElseIf ComboBox1.SelectedItem.Equals("Geolocation") Then
                addplugin("cordova-plugin-geolocation")
            ElseIf ComboBox1.SelectedItem.Equals("Dialogs") Then
                addplugin("cordova-plugin-dialogs")
            ElseIf ComboBox1.SelectedItem.Equals("Bluetooth Serial") Then
                addplugin("cordova-plugin-bluetooth-serial")
            ElseIf ComboBox1.SelectedItem.Equals("Background Mode") Then
                addplugin("cordova-plugin-background-mode")
            ElseIf ComboBox1.SelectedItem.Equals("Network Information") Then
                addplugin("cordova-plugin-network-information")

            End If
        End If
    End Sub

    Sub addplugin(ByVal oplugin As String)
        Try

            ooplugin = oplugin

            t1 = New Thread(AddressOf Me.addpluginembackground)
            t1.Start()

            p.ShowDialog()
            TextBox1.Clear()
            ComboBox1.SelectedIndex = -1

            Me.Dispose()

        Catch ex As Exception
            MsgBox(ex.Message)

        End Try
    End Sub

    Private Sub AddPlugin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        On Error Resume Next
        If (olugar.Length > 3) Then
            TextBox1.Text = olugar
        Else
            TextBox1.Clear()
        End If
    End Sub

    Sub addpluginembackground()

        Dim saida As Integer = 2
        Dim SW As New StreamWriter(Path.GetTempPath() + "\criar.bat")
        SW.WriteLine("@echo off &echo.&echo Aguarde..& mode 60,20 & cd " + olugar + "& cordova plugin add " + ooplugin)
        SW.Close()
        SW.Dispose()

        Dim proc As New Process()

        proc.StartInfo.FileName = Path.GetTempPath() + "\criar.bat"
        proc.Start()
        proc.WaitForExit()

        Gobala.fehca = 1

        My.Computer.FileSystem.DeleteFile(Path.GetTempPath() + "\criar.bat")
    End Sub

End Class