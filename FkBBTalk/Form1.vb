Public Class Form1
    Public Declare Function WriteProcessMemory Lib "kernel32" (ByVal hProcess As Integer, ByVal lpBaseAddress As Integer, ByVal lpBuffer As Byte(), ByVal nSize As Integer, ByRef lpNumberOfBytesWritten As Integer) As Integer
    Public Declare Function GetProcAddress Lib "kernel32" (ByVal hModule As Integer, ByVal lpProcName As String) As Integer
    Private Declare Function GetModuleHandle Lib "Kernel32" Alias "GetModuleHandleA" (ByVal lpModuleName As String) As Integer

    Sub mFunction()
        On Error Resume Next
        While True
            If (Process.GetProcessesByName("BBTalk").Length = 0) Then
                Label1.Text = "尚未發現該死的聊聊."
            Else
                If ((Process.GetProcessesByName("lol").Length <> 0)) And ((Process.GetProcessesByName("LolClient").Length <> 0)) Then

                    Label1.Text = "開始處理基巴聊聊吧 OwO"
                    Process.GetProcessesByName("BBTalk")(0).Kill()
                    Dim TargetHandle As IntPtr = Process.GetProcessesByName("ggdllhost")(0).Handle
                    Dim AdrOfBP As IntPtr = GetProcAddress(GetModuleHandle("ntdll"), "DbgBreakPoint")
                    Dim AdrOfRB As IntPtr = GetProcAddress(GetModuleHandle("ntdll"), "DbgUiRemoteBreakin")
                    Dim AdrOfUB As IntPtr = GetProcAddress(GetModuleHandle("ntdll"), "DbgUserBreakPoint")
                    WriteProcessMemory(TargetHandle, AdrOfBP, {&HCC}, 1, 0)
                    WriteProcessMemory(TargetHandle, AdrOfRB, {&H6A, &H8, &H68, &HE8, &H7}, 5, 0)
                    WriteProcessMemory(TargetHandle, AdrOfUB, {&HCC, &H90, &HC3, &H90, &HCC}, 5, 0)
                    Label1.Text = "強姦聊聊完畢 OwO"

                Else
                    Label1.Text = "快開啟LOL阿你..."
                End If
            End If
            Threading.Thread.Sleep(3000)
        End While
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = True
        Visible = False
        If e.CloseReason = CloseReason.WindowsShutDown Then
            End
        ElseIf (Visible = False) Then
            NotifyIcon1.BalloonTipTitle = "縮小"
            NotifyIcon1.BalloonTipText = "已於工具列隱藏"
            NotifyIcon1.ShowBalloonTip(500)
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Form.CheckForIllegalCrossThreadCalls = False
        Dim Thread As New Threading.Thread(AddressOf mFunction)
        Thread.Start()
    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        Visible = Not Visible
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        End
    End Sub
End Class
