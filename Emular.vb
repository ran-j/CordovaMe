Imports System.IO
Imports System.Threading

Public Class Emular
    Dim oluigar As String = Gobala.caminho
    Dim p As New wait
    Private t1 As Thread

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Principal.FolderBrowserDialog1.ShowDialog()
        oluigar = Principal.FolderBrowserDialog1.SelectedPath

        If oluigar.Length > 3 Then

            If Directory.Exists(oluigar + "\www") Or File.Exists(oluigar + "\config.xml") Or File.Exists(oluigar + "\index.html") Then
                Gobala.caminho = oluigar
                TextBox1.Text = oluigar
            Else
                MsgBox("Essa pasta não contem um projeto Cordova")
            End If

        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        oluigar = TextBox1.Text

        If Directory.Exists(oluigar + "\platforms\android") And File.Exists(oluigar + "\platforms\platforms.json") Then
            If oluigar.Length > 3 Then
                Try
                    My.Settings.lugarsalvo = oluigar
                    t1 = New Thread(AddressOf Me.emular)
                    t1.Start()

                    p.ShowDialog()

                    TextBox1.Clear()
                    Me.Dispose()

                Catch ex As Exception
                    MsgBox(ex.Message)

                End Try

            ElseIf Not Directory.Exists(oluigar + "\www") Or Not File.Exists(oluigar + "\config.xml") Or Not File.Exists(oluigar + "\index.html") Then
                MsgBox("Essa pasta não contem um projeto Cordova")
            Else
                MsgBox("Caminho Invalido")
            End If
        Else
            MsgBox("O projeto está sem plataforma")
        End If
    End Sub

    Private Sub Form6_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        On Error Resume Next
        If (oluigar.Length > 3) Then
            TextBox1.Text = oluigar
        Else
            TextBox1.Clear()
        End If
    End Sub

    Sub emular()
        Dim saida As Integer = 2
        Dim SW As New StreamWriter(Path.GetTempPath() + "\criar.bat")
        SW.WriteLine("@echo off &echo.&echo Aguarde..& mode 60,20 & cd " + oluigar + "& cordova run")
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