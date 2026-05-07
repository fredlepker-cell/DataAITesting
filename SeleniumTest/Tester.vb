Imports SeleniumVB.SeleniumVB

Public Module Tester
    Public Sub Main()
        Dim test As New Tests
        Dim bError As Boolean = False

        Try
            test.Setup()
            Console.WriteLine("Testing Home Page")
            test.VerifyHomePage()
            Console.WriteLine("Testing Quick Start")
            test.VerifyQuickStart()

            test.TearDown()

        Catch ex As Exception
            bError = True
            Console.WriteLine("An error occurred: " & ex.Message)
        End Try
        If Not bError Then _
            Console.WriteLine("All tests executed successfully.")


    End Sub
End Module
