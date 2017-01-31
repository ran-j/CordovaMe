Imports System.IO

Public Class Principal
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button7.Click
        CriarProjeto.ShowDialog()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        DeletarProjeto.ShowDialog()
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        AddPlataforma.ShowDialog()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        CriarBuild.ShowDialog()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Emular.ShowDialog()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        AddPlugin.ShowDialog()
    End Sub

    Private Sub Form1_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            If File.Exists(Path.GetTempPath() + "\criar.bat") Then
                My.Computer.FileSystem.DeleteFile(Path.GetTempPath() + "\criar.bat")
            End If

            For Each f As Form In My.Application.OpenForms
                f.Close()
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim SW As New StreamWriter(Path.GetTempPath() + "\criar.bat")
            SW.WriteLine("teste")
            SW.Close()
            SW.Dispose()
            My.Computer.FileSystem.DeleteFile(Path.GetTempPath() + "\criar.bat")
        Catch ex As Exception
            MsgBox("Erro, tente iniciar como adiministrador")
        End Try

        ToolTip1.SetToolTip(Me.Button1, "Adicionar uma plataforma a um projeto cordova")
        ToolTip1.SetToolTip(Me.Button5, "Adicionar um plugin a um projeto cordova")
        ToolTip1.SetToolTip(Me.Button7, "Criar um novo projeto Cordova")
        ToolTip1.SetToolTip(Me.Button3, "Criar uma build do projeto")
        ToolTip1.SetToolTip(Me.Button6, "Deletar um projeto")
        ToolTip1.SetToolTip(Me.Button4, "Emular um projeto")

        Label1.Text = System.String.Format(Label1.Text, My.Application.Info.Version.Major, My.Application.Info.Version.Minor)

        If Not (My.Settings.lugarsalvo.Equals("0")) Then
            Gobala.caminho = My.Settings.lugarsalvo
        End If

    End Sub
End Class
