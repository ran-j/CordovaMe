Imports System.IO

Public Class DeletarProjeto
    Dim apagar As String = Gobala.caminho
    Dim p As New wait

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Principal.FolderBrowserDialog1.ShowDialog()
        apagar = Principal.FolderBrowserDialog1.SelectedPath

        If Not apagar.Length < 3 Then
            TextBox1.Text = apagar
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If TextBox1.Text.Length > 3 Then
            Dim choice As DialogResult = MessageBox.Show("Deseja realmente excluir esse projeto", "Excluir ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

            If choice = Windows.Forms.DialogResult.Yes Then
                If Directory.Exists(apagar + "\www") Or File.Exists(apagar + "\config.xml") Or File.Exists(apagar + "\index.html") Then

                    p.Show()
                    apagarapasta()
                Else
                    Dim choice2 As DialogResult = MessageBox.Show("Não e um projeto Cordova, deseja excluir mesmo assim ?", "Excluir ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    If choice2 = Windows.Forms.DialogResult.Yes Then
                        apagarapasta()
                    End If
                End If
            End If
        End If

    End Sub

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        On Error Resume Next
        If (apagar.Length > 3) Then
            TextBox1.Text = apagar
        Else
            TextBox1.Text = ""
        End If
    End Sub

    Sub apagarapasta()
        Try
            Dim objDirectory As New System.IO.DirectoryInfo(apagar)
            objDirectory.Delete(True)
            MsgBox("Apagdo")

            Gobala.caminho = ""
            TextBox1.Text = ""

            If (My.Settings.lugarsalvo.Equals(apagar)) Then
                My.Settings.lugarsalvo = 0
                Gobala.caminho = ""
            End If

        Catch ExIO As Exception
            MsgBox(ExIO.Message)
        End Try
        p.Close()
    End Sub
End Class