Public Class wait

    Private Sub Timer1_Tick_1(sender As Object, e As EventArgs) Handles Timer1.Tick
        If (Gobala.fehca = 1) Then
            Me.Dispose()
            Gobala.fehca = 0
        End If
    End Sub

    Private Sub wait_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If e.CloseReason = CloseReason.UserClosing Then
            Application.Restart()
        End If

    End Sub
End Class