Imports System.Net.Mail
Imports System.Threading
Imports SeleniumTestProject.SeleniumTestProject
Public Module Scheduler

    Public Sub SchedulerMain()
        'While True
        Try
            Dim tests As New Tests()
            tests.Setup()

            'tests.TestQuickStartButton()
            'tests.TestRegistrationButton()
            'tests.TestSignInButton()
            'tests.TestSandboxButton()
            'tests.TestAnalyticsChartsMapsButton()
            'tests.TestProjectManagerButton()
            'tests.TestTablesClassesLink()
            'tests.TestDashboardLink()
            'tests.TestScheduledReportsLink()
            'tests.TestScheduledDownloadsLink()
            'tests.TestScheduledImportsLink()
            'tests.TestFriendlyNamesLink()
            'tests.TestHelpLink()
            'tests.TestReportProblemLink()
            'tests.TestCreateNewReportLink()
            'tests.TestImportDataLink()
            'tests.TestLogoffLink()
            'tests.TestListOfTables()
            'tests.TestListOfJoins()
            'tests.TestComparisonLink()
            'tests.TestAboutUsLink()
            'tests.TestOUReportsTestingSiteLink()
            'tests.TestOUReportsServicesLink()
            'tests.TestOUReportsSoftwareLink()
            'tests.TestOUReportsProjectManagerFreeLink()
            'tests.TestIndividualLink()
            'tests.TestCompanyLink()
            'tests.TestSalesAgentLink()
            'tests.TestCovidDashboardLink()
            'tests.TestPublicDataLink()
            'tests.TestContactUsLink()

            'Console.WriteLine("12 test cases for the week of July 1st")
            'tests.TestDataFieldsLink()
            'tests.TestJoinTablesLink()
            'tests.TestFiltersLink()
            'tests.TestSortingLink()
            'tests.TestColumnOrderExpressionsLink()
            'tests.TestGroupsAndTotalsLink()
            'tests.TestCombineColumnValuesLink()
            'tests.TestAdvancedReportDesignerLink()
            'tests.TestMapDefinitionLink()
            'tests.TestReportInfoLink()
            'tests.TestParametersLink()
            'tests.TestUsersLink()


            'Console.WriteLine("10 Test cases for the week of July 8th")
            'tests.TestBBListEditLogOffLink()
            '    tests.TestBBListEditListOfReportsLink()
            '    tests.TestBBListEditReportDefinitionLink()
            '    tests.TestBBListEditReportParametersLink()
            '    tests.TestBBListEditShareReportUsersLink()
                'tests.TestBBListEditReportDataQueryLink()
                'tests.TestBBListEditDataFieldsLink()
                'tests.TestBBListEditJoinsLink()
            '    tests.TestBBListEditFiltersLink()
                'tests.TestBBListEditSortingLink()


            ''6 Test cases for the week of July 15th
                'tests.TestBBListEditReportFormatDefinitionLink()
            'tests.TestBBListEditAdvancedReportDesignerLink()
                'tests.TestBBListEditColumnsExpressionsLink()
                'tests.TestBBListEditGroupsTotalLink()
                'tests.TestBBListEditCombineValuesLink()
                'tests.TestBBListEditMapDefinitionLink()

            ''4 Test cases for the week of July 22nd
            'tests.TestBBListEditExploreReportDataLink()
            'tests.TestBBListEditShowGenericReportLink()
            'tests.TestBBListEditShowReportChartsLink()
            'tests.TestBBListEditShowReportLink()

                ''5 Test cases for the week of July 29th
                'tests.TestBBListEditShowAnalyticsLink()
            '    tests.TestBBListEditSeeDataOverallStatisticsLink()
            '    tests.TestBBListEditSeeGroupsStatisticsLink()
            '    tests.TestBBListEditSeeFieldsCorrelationLink()
                'tests.TestBBListEditMatrixBalancingLink()

                ''2 Test cases for the week of August 30th
                'tests.TestQuickStartRegistration()
                'tests.TestQuickStartSignIn()

                '4 test cases for the week of november 19th
                tests.TestBBListMapDropdowns()
                tests.TestBBListAnalyticsDropdowns()
                tests.TestBBListDataDropdowns()
                tests.TestBBListChartsDropdowns()

                'WEEK OF SEPTEMBER 4TH THERE WERE NO TEST CASES, HOWEVER, WE RESTRUCTURED THE CODE SO THAT THE HELPER
                'METHODS ARE AT THE TOP AND ALL TEST CASES ARE STRUCUTED THE SAME AND OUTPUT TO THE CONSOLE AND A FILE
                'ON THE C DRIVE IF THEY PASS OR FAIL

                'WEEK OF SEPTEMBER 11TH WE ADDED TO THE TEST CASE TEST QUICK START SIGN IN

                'WEEK OF SEPTEMBER 18TH WE ALSO ADDED TO THE TEST CASE TEST QUICK START SIGN IN

                'WEEK OF SEPTEMBER 25TH WE ALSO ADDED TO THE TEST CASE TEST QUICK START SIGN IN



                'WEEK OF OCTOBER 2ND TEST CASES ARE ADDED TO BBLIST EDIT REPORT DEFINITION AND SHARE REPORT

                'TEST CASES FOR THE WEEK OF OCTOBER 9TH ARE ADDED TO BBLIST EDIT REPORT DEFINITIONS AND SHARE REPORT

                'TEST CASES FOR THE WEEK OF OCTOBER 15TH ARE ADDED TO BBLIST REPPORT DATA QUERY LINK, EDIT DATA FIELDS LINK, EDIT JOINS LINK
                'AND EDIT SORTING LINK




                'TEST CASES FOR THE WEEK OF OCTOBER 23RD ARE ADDED TO BBLIST REPORT FORMAT DEFINITION, EDIT COLUMNS EXPRESSIONS, EDIT GROUPS TOTAL,
                'EDIT COMBINE VALUES, AND EDIT MAP DEFINITION LINKS

                'TEST CASES FOR THE WEEK OF OCTOBER 30TH ARE ADDED TO BBLIST SHOW ANALYTICS, AND THE FIRST 3 DROP DOWNS IN THE MATRIX BALANCING
                'DROPDOWNS

                'TEST CASES FOR THE WEEK OF NOVEMBER 6TH ARE ADDED TO THE MATRIX BALANCING TEST CASE. WE COMPLETED DROWDOWN TESTING FOR 2B, 2C, 3A

                'TEST CASES FOR THE WEEK OF NOVEMBER 13TH ARE ADDED TO THE MATRIX BALANCING TEST CASE. WE COMPLETED TESTING FOR THE DROPDOWNS
                'OF 3B, 3C, 4A, 4B, ADND 4C

                'TEST CASES FOR THE WEEK OF NOVEMBER 19TH ARE ADDED TO THE BOTTOM OF THE CODE BECAUSE WE CREATED NEW TEST CASES FOR EACH OF WHAT WE WERE TASKED WITH
                'BBLIST MAP, ANALYTICS, DATA, AND CHARTS WERE ALL CHECKED FOR DROPDOWNS

            tests.TearDown()

            ' Log successful execution
            Console.WriteLine("All tests executed successfully.")
        Catch ex As Exception
            Console.WriteLine("An error occurred: " & ex.Message)
            'SendFailureEmail("Test Execution Failed", "An error occurred during the test execution: " & ex.Message)
        End Try

        ' Wait for 30 seconds before running the tests again
        'Thread.Sleep(TimeSpan.FromSeconds(15))
        'End While
    End Sub

    'Public Sub SendFailureEmail(subject As String, body As String)
    '    Try
    '        Dim mail As New MailMessage()
    '        Dim smtpServer As New SmtpClient("smtp.live.com")

    '        mail.From = New MailAddress("your - email")
    '        mail.To.Add("email - you - want - to - send - too")
    '        mail.Subject = subject
    '        mail.Body = body

    '        smtpServer.Port = 587
    '        smtpServer.Credentials = New System.Net.NetworkCredential("your - email", "your - password")
    '        smtpServer.EnableSsl = True

    '        smtpServer.Send(mail)
    '        Console.WriteLine("Failure email sent successfully.")
    '    Catch ex As Exception
    '        Console.WriteLine("Error sending failure email: " & ex.Message)
    '    End Try
    'End Sub
End Module
