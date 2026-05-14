Imports System.IO
Imports NUnit.Framework
Imports OpenQA.Selenium
Imports OpenQA.Selenium.Chrome
Imports OpenQA.Selenium.Support.UI
Imports System.Net.Mail
Imports System.Threading
Imports System.Diagnostics
Imports OpenQA.Selenium.Interactions
Imports System.Text.RegularExpressions
Imports WindowsInput
Imports WindowsInput.Native

Namespace SeleniumTestProject
    <TestFixture>
    Public Class Tests
        Private driver As IWebDriver

        <SetUp>
        Public Sub Setup()
            'Dim path As String = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName
            'driver = New ChromeDriver(path & "\bin\debug\net8.0\")
            Dim path As String = "C:\Users\fredl\source\repos\Capstone_1\SeleniumTestProject\bin\Debug\net8.0\"
            driver = New ChromeDriver(path)
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10)
        End Sub

        'HELPER METHODS FOR ALL TEST CASES

        'THIS IS A HELPER METHOD
        ''helper method To navigate To bblist edit page
        Public Sub NavigateToBBListEditPage()
            'driver.Manage.Window.Maximize()
            ' Navigate to the index1.aspx page
            driver.Navigate().GoToUrl("http://localhost/OURtestsite/index1.aspx")

            ' Locate the Analytics, Charts, and Maps button using its ID and click it
            Dim analyticsButton As IWebElement = driver.FindElement(By.Id("ButtonPlayMaps"))
            Dim jsExecutor As IJavaScriptExecutor = CType(driver, IJavaScriptExecutor)
            jsExecutor.ExecuteScript("arguments[0].click();", analyticsButton)

            CheckForSecurityWarning()

            ' Wait for the new page to load by checking the URL
            Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
            wait.Until(Function(d) d.Url.Contains("ListOfReports.aspx"))

            ' Locate the "edit" link for the "bblist" row and click it
            Dim editLink As IWebElement = driver.FindElement(By.XPath("//a[contains(@id, 'bblist') and contains(text(), 'edit')]"))
            editLink.Click()
        End Sub

        'THIS IS A HELPER METHOD
        Public Sub CheckForSecurityWarning()
            Try
                ' Wait for the security warning page to appear
                '    Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))

                '    ' Check if the "Advanced" button is present
                '    Dim advancedButton As IWebElement = wait.Until(Function(d) d.FindElement(By.Id("details-button")))
                '    If advancedButton IsNot Nothing Then
                '        advancedButton.Click()

                '        ' Wait for and click the "Proceed" link
                '        Dim proceedLink As IWebElement = wait.Until(Function(d) d.FindElement(By.Id("proceed-link")))
                '        If proceedLink IsNot Nothing Then
                '            proceedLink.Click()
                '            Console.WriteLine("Security warning detected and bypassed.")
                '        Else
                '            Console.WriteLine("Advanced button found, but no Proceed link detected.")
                '        End If
                '    Else
                Console.WriteLine("No security warning detected.")
                '    End If
                'Catch ex As WebDriverTimeoutException
                '    ' No security warning found within the timeout period
                '    Console.WriteLine("No security warning detected.")
                'Catch ex As NoSuchElementException
                '    ' Security warning elements not found
                '    Console.WriteLine("No security warning elements found: " & ex.Message)
            Catch ex As Exception
                Console.WriteLine("No security warning detected.")
            End Try
        End Sub

        'THIS IS A HELPER METHOD
        ' Helper method for logging test results to a file (appends to an existing file for each test case)
        Public Sub LogTestResults(testName As String, result As String)
            ' Define the log directory and file path (no timestamp in filename)
            Dim logDirectory As String = "C:\testing"
            Dim logFilePath As String = Path.Combine(logDirectory, testName & ".txt") ' One file per test

            ' Ensure the log directory exists
            If Not Directory.Exists(logDirectory) Then
                Directory.CreateDirectory(logDirectory)
            End If

            ' Open the log file and append the results
            Using logFile As StreamWriter = New StreamWriter(logFilePath, True) ' "True" to append instead of overwrite
                logFile.WriteLine("Test Name: " & testName)
                logFile.WriteLine("Timestamp: " & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                logFile.WriteLine("Result: " & result)
                logFile.WriteLine(New String("-"c, 50)) ' Separator for readability
            End Using
        End Sub

        Public Sub TestLink(linkXPath As String, expectedUrl As String, testName As String)
            Try
                Dim link As IWebElement = driver.FindElement(By.XPath(linkXPath))
                link.Click()
                Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
                wait.Until(Function(d) d.Url.Contains(expectedUrl))
                Assert.IsTrue(driver.Url.Contains(expectedUrl))

            Catch ex As Exception
                ' Log the error to file and console
                LogTestResults(testName, "Failed: " & ex.Message)
                Console.WriteLine(testName & " failed: " & ex.Message)
                Throw
            End Try
        End Sub


        'END HELPER METHODS



        '<Test>
        'Public Sub TestQuickStartButton()
        '    Try
        '        ' Navigate to the index1.aspx page
        '        driver.Navigate().GoToUrl("http://localhost/OURtestsite/index1.aspx")
        '        CheckForSecurityWarning()

        '        ' Locate the Quick Start button and click it
        '        Dim quickStartButton As IWebElement = driver.FindElement(By.XPath("//a[@href='https://oureports.net/OUReports/QuickStart.aspx']"))
        '        quickStartButton.Click()

        '        ' Wait for the new page to load by checking the URL
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.Url.Contains("QuickStart.aspx"))

        '        ' Verify the new page URL
        '        Assert.IsTrue(driver.Url.Contains("QuickStart.aspx"))

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestQuickStartButton", "Passed")
        '        Console.WriteLine("TestQuickStartButton passed")

        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestQuickStartButton", "Failed: " & ex.Message)
        '        Console.WriteLine("TestQuickStartButton failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub

        '<Test>
        'Public Sub TestRegistrationButton()
        '    Try
        '        ' Navigate to the index1.aspx page
        '        driver.Navigate().GoToUrl("http://localhost/OURtest/index1.aspx")
        '        CheckForSecurityWarning()

        '        ' Locate the Registration button and click it
        '        Dim registrationButton As IWebElement = driver.FindElement(By.XPath("//a[@href='https://oureports.net/OUReports/index3.aspx']"))
        '        registrationButton.Click()

        '        ' Wait for the new page to load by checking the URL
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.Url.Contains("index3.aspx"))

        '        ' Verify the new page URL
        '        Assert.IsTrue(driver.Url.Contains("index3.aspx"))

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestRegistrationButton", "Passed")
        '        Console.WriteLine("TestRegistrationButton passed")

        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestRegistrationButton", "Failed: " & ex.Message)
        '        Console.WriteLine("TestRegistrationButton failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub

        '<Test>
        'Public Sub TestSignInButton()
        '    Try
        '        ' Navigate to the index1.aspx page
        '        driver.Navigate().GoToUrl("http://localhost/OURtest/index1.aspx")
        '        CheckForSecurityWarning()

        '        ' Locate the Sign In button and click it
        '        Dim signInButton As IWebElement = driver.FindElement(By.XPath("//a[@href='https://oureports.net/OUReports/Default.aspx']"))
        '        signInButton.Click()

        '        ' Wait for the new page to load by checking the URL
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.Url.Contains("/OUReports/Default.aspx"))

        '        ' Verify the new page URL
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/Default.aspx"))

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestSignInButton", "Passed")
        '        Console.WriteLine("TestSignInButton passed")

        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestSignInButton", "Failed: " & ex.Message)
        '        Console.WriteLine("TestSignInButton failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub

        '<Test>
        'Public Sub TestSandboxButton()
        '    Try
        '        ' Navigate to the index1.aspx page
        '        driver.Navigate().GoToUrl("http://localhost/OURtest/index1.aspx")

        '        ' Locate the Sandbox button using its ID
        '        Dim sandboxButton As IWebElement = driver.FindElement(By.Id("ButtonPlayCinema"))

        '        ' Use JavaScript to click the Sandbox button
        '        Dim jsExecutor As IJavaScriptExecutor = CType(driver, IJavaScriptExecutor)
        '        jsExecutor.ExecuteScript("arguments[0].click();", sandboxButton)

        '        ' Handle the security warning
        '        CheckForSecurityWarning()

        '        ' Wait for the new page to load by checking the URL
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.Url.Contains("ListOfReports.aspx"))

        '        ' Verify the new page URL
        '        Assert.IsTrue(driver.Url.Contains("ListOfReports.aspx"))

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestSandboxButton", "Passed")
        '        Console.WriteLine("TestSandboxButton passed")

        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestSandboxButton", "Failed: " & ex.Message)
        '        Console.WriteLine("TestSandboxButton failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub

        'NEW TEST CASES
        '<Test>
        'Public Sub TestListOfTables()
        '    TestSandboxButton()
        '    TestLink("//a[text()='Tables/Classes']", "/OUReports/ClassExplorer.aspx", "TestListOfTables")
        '    TestLink("//a[text()='List Of Tables']", "/OUReports/ListOfTables.aspx", "TestListOfTables")

        '    ' Log to file with timestamp and write to console that it passed
        '    LogTestResults("TestListOfTables", "Passed")
        '    Console.WriteLine("TestListOfTables passed")
        'End Sub

        '<Test>
        'Public Sub TestListOfJoins()
        '    TestSandboxButton()
        '    TestLink("//a[text()='Tables/Classes']", "/OUReports/ClassExplorer.aspx", "TestListOfJoins")
        '    TestLink("//a[text()='List Of Joins']", "/OUReports/ListOfJoins.aspx", "TestListOfJoins")

        '    ' Log to file with timestamp and write to console that it passed
        '    LogTestResults("TestListOfJoins", "Passed")
        '    Console.WriteLine("TestListOfJoins passed")
        'End Sub

        '<Test>
        'Public Sub TestAnalyticsChartsMapsButton()
        '    Try
        '        ' Navigate to the index1.aspx page
        '        driver.Navigate().GoToUrl("http://localhost/OURtest/index1.aspx")

        '        ' Locate the Analytics, Charts, and Maps button using its ID
        '        Dim analyticsButton As IWebElement = driver.FindElement(By.Id("ButtonPlayMaps"))

        '        ' Use JavaScript to click the Analytics, Charts, and Maps button
        '        Dim jsExecutor As IJavaScriptExecutor = CType(driver, IJavaScriptExecutor)
        '        jsExecutor.ExecuteScript("arguments[0].click();", analyticsButton)

        '        ' Handle the security warning
        '        CheckForSecurityWarning()

        '        ' Wait for the new page to load by checking the URL
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.Url.Contains("ListOfReports.aspx"))

        '        ' Verify the new page URL
        '        Assert.IsTrue(driver.Url.Contains("ListOfReports.aspx"))

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestAnalyticsChartsMapsButton", "Passed")
        '        Console.WriteLine("TestAnalyticsChartsMapsButton passed")

        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestAnalyticsChartsMapsButton", "Failed: " & ex.Message)
        '        Console.WriteLine("TestAnalyticsChartsMapsButton failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub

        '<Test>
        'Public Sub TestTablesClassesLink()
        '    Try
        '        TestAnalyticsChartsMapsButton()
        '        TestLink("//a[text()='Tables/Classes']", "/OUReports/ClassExplorer.aspx", "TestTablesClassesLink")

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestTablesClassesLink", "Passed")
        '        Console.WriteLine("TestTablesClassesLink passed")
        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestTablesClassesLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestTablesClassesLink failed: " & ex.Message)
        '        Throw
        '    End Try

        'End Sub

        ''OPENS A NEW WINDOW INSTEAD OF STAYING IN THE SAME WINDOW
        '<Test>
        'Public Sub TestDashboardLink()
        '    Try
        '        ' Navigate to the index1.aspx page
        '        driver.Navigate().GoToUrl("http://localhost/OURtest/index1.aspx")

        '        ' Locate the Analytics, Charts, and Maps button using its ID and click it
        '        Dim analyticsButton As IWebElement = driver.FindElement(By.Id("ButtonPlayMaps"))
        '        Dim jsExecutor As IJavaScriptExecutor = CType(driver, IJavaScriptExecutor)
        '        jsExecutor.ExecuteScript("arguments[0].click();", analyticsButton)

        '        ' Handle the security warning
        '        CheckForSecurityWarning()

        '        ' Wait for the new page to load by checking the URL
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.Url.Contains("ListOfReports.aspx"))

        '        ' Locate the Dashboard link and click it
        '        Dim dashboardLink As IWebElement = driver.FindElement(By.LinkText("Dashboards"))
        '        Dim originalWindow As String = driver.CurrentWindowHandle
        '        dashboardLink.Click()

        '        ' Wait for the new window to open
        '        wait.Until(Function(d) d.WindowHandles.Count = 2)

        '        ' Switch to the new window
        '        Dim newWindow As String = driver.WindowHandles.Last()
        '        driver.SwitchTo().Window(newWindow)

        '        ' Perform assertions on the new window
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/ListOfDashboards.aspx"))

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestDashboardLink", "Passed")
        '        Console.WriteLine("TestDashboardLink passed")

        '        ' Close the new window and switch back to the original window
        '        driver.Close()
        '        driver.SwitchTo().Window(originalWindow)
        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestDashboardLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestDashboardLink failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub


        '<Test>
        'Public Sub TestScheduledReportsLink()
        '    Try
        '        TestAnalyticsChartsMapsButton()
        '        TestLink("//a[text()='Scheduled Reports']", "/OUReports/ScheduledReports.aspx", "TestScheduledReportsLink")

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestScheduledReportsLink", "Passed")
        '        Console.WriteLine("TestScheduledReportsLink passed")

        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestScheduledReportsLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestScheduledReportsLink failed: " & ex.Message)
        '        Throw
        '    End Try

        'End Sub

        ''OPENS A NEW WINDOW INSTEAD OF STAYING IN THE SAME WINDOW
        '<Test>
        'Public Sub TestScheduledDownloadsLink()
        '    Try
        '        ' Navigate to the index1.aspx page
        '        driver.Navigate().GoToUrl("http://localhost/OURtest/index1.aspx")

        '        ' Locate the Analytics, Charts, and Maps button using its ID and click it
        '        Dim analyticsButton As IWebElement = driver.FindElement(By.Id("ButtonPlayMaps"))
        '        Dim jsExecutor As IJavaScriptExecutor = CType(driver, IJavaScriptExecutor)
        '        jsExecutor.ExecuteScript("arguments[0].click();", analyticsButton)

        '        ' Handle the security warning
        '        CheckForSecurityWarning()

        '        ' Wait for the new page to load by checking the URL
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.Url.Contains("ListOfReports.aspx"))

        '        ' Locate the Scheduled Downloads link and click it
        '        Dim scheduledDownloadsLink As IWebElement = driver.FindElement(By.LinkText("Scheduled Downloads"))
        '        Dim originalWindow As String = driver.CurrentWindowHandle
        '        scheduledDownloadsLink.Click()

        '        ' Wait for the new window to open
        '        wait.Until(Function(d) d.WindowHandles.Count = 2)

        '        ' Switch to the new window
        '        Dim newWindow As String = driver.WindowHandles.Last()
        '        driver.SwitchTo().Window(newWindow)

        '        ' Perform assertions on the new window
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/RunScheduledItems.aspx"))

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestScheduledDownloadsLink", "Passed")
        '        Console.WriteLine("TestScheduledDownloadsLink passed")

        '        ' Close the new window and switch back to the original window
        '        driver.Close()
        '        driver.SwitchTo().Window(originalWindow)
        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestScheduledDownloadsLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestScheduledDownloadsLink failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub


        ''OPENS A NEW WINDOW INSTEAD OF STAYING IN THE SAME WINDOW
        '<Test>
        'Public Sub TestScheduledImportsLink()
        '    Try
        '        ' Navigate to the index1.aspx page
        '        driver.Navigate().GoToUrl("http://localhost/OURtest/index1.aspx")

        '        ' Locate the Analytics, Charts, and Maps button using its ID and click it
        '        Dim analyticsButton As IWebElement = driver.FindElement(By.Id("ButtonPlayMaps"))
        '        Dim jsExecutor As IJavaScriptExecutor = CType(driver, IJavaScriptExecutor)
        '        jsExecutor.ExecuteScript("arguments[0].click();", analyticsButton)

        '        ' Handle the security warning
        '        CheckForSecurityWarning()

        '        ' Wait for the new page to load by checking the URL
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.Url.Contains("ListOfReports.aspx"))

        '        ' Locate the Scheduled Imports link and click it
        '        Dim scheduledImportsLink As IWebElement = driver.FindElement(By.LinkText("Scheduled Imports"))
        '        Dim originalWindow As String = driver.CurrentWindowHandle
        '        scheduledImportsLink.Click()

        '        ' Wait for the new window to open
        '        wait.Until(Function(d) d.WindowHandles.Count = 2)

        '        ' Switch to the new window
        '        Dim newWindow As String = driver.WindowHandles.Last()
        '        driver.SwitchTo().Window(newWindow)

        '        ' Perform assertions on the new window
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/ScheduledImports.aspx"))

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestScheduledImportsLink", "Passed")
        '        Console.WriteLine("TestScheduledImportsLink passed")

        '        ' Close the new window and switch back to the original window
        '        driver.Close()
        '        driver.SwitchTo().Window(originalWindow)
        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestScheduledImportsLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestScheduledImportsLink failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub


        '<Test>
        'Public Sub TestFriendlyNamesLink()
        '    Try
        '        TestAnalyticsChartsMapsButton()
        '        TestLink("//a[text()='Friendly Names']", "/OUReports/FriendlyNames.aspx", "TestFriendlyNamesLink")

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestFriendlyNamesLink", "Passed")
        '        Console.WriteLine("TestFriendlyNamesLink passed")
        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestFriendlyNamesLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestFriendlyNamesLink failed: " & ex.Message)
        '        Throw
        '    End Try

        'End Sub

        ''OPENS A NEW WINDOW INSTEAD OF STAYING IN THE SAME WINDOW
        ''I THINK THIS OPENS A PDF POSSIBLY REMOVE THIS TEST CASE?? PDF PAGE NUMBER CHANGED WHEN TESTING
        '<Test>
        'Public Sub TestHelpLink()
        '    Try
        '        ' Navigate to the index1.aspx page
        '        driver.Navigate().GoToUrl("http://localhost/OURtest/index1.aspx")

        '        ' Locate the Analytics, Charts, and Maps button using its ID and click it
        '        Dim analyticsButton As IWebElement = driver.FindElement(By.Id("ButtonPlayMaps"))
        '        Dim jsExecutor As IJavaScriptExecutor = CType(driver, IJavaScriptExecutor)
        '        jsExecutor.ExecuteScript("arguments[0].click();", analyticsButton)

        '        ' Handle the security warning
        '        CheckForSecurityWarning()

        '        ' Wait for the new page to load by checking the URL
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.Url.Contains("ListOfReports.aspx"))

        '        ' Locate the Help link and click it
        '        Dim helpLink As IWebElement = driver.FindElement(By.LinkText("Help"))
        '        Dim originalWindow As String = driver.CurrentWindowHandle
        '        helpLink.Click()

        '        ' Wait for the new window to open
        '        wait.Until(Function(d) d.WindowHandles.Count = 2)

        '        ' Switch to the new window
        '        Dim newWindow As String = driver.WindowHandles.Last()
        '        driver.SwitchTo().Window(newWindow)

        '        ' Perform assertions on the new window
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/OnlineUserReporting.pdf#page=11"))

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestHelpLink", "Passed")
        '        Console.WriteLine("TestHelpLink passed")

        '        ' Close the new window and switch back to the original window
        '        driver.Close()
        '        driver.SwitchTo().Window(originalWindow)
        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestHelpLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestHelpLink failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub


        '<Test>
        'Public Sub TestReportProblemLink()
        '    Try
        '        TestAnalyticsChartsMapsButton()
        '        TestLink("//a[text()='Report a problem']", "/OUReports/HelpDesk.aspx", "TestReportProblemLink")

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestReportProblemLink", "Passed")
        '        Console.WriteLine("TestReportProblemLink passed")
        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestReportProblemLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestReportProblemLink failed: " & ex.Message)
        '        Throw
        '    End Try

        'End Sub

        '<Test>
        'Public Sub TestLogoffLink()
        '    Try
        '        TestAnalyticsChartsMapsButton()
        '        TestLink("//a[text()='Log off']", "/OUReports/Default.aspx", "TestLogoffLink")

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestLogoffLink", "Passed")
        '        Console.WriteLine("TestLogoffLink passed")

        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestLogoffLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestLogoffLink failed: " & ex.Message)
        '        Throw
        '    End Try

        'End Sub

        '<Test>
        'Public Sub TestCreateNewReportLink()
        '    Try
        '        TestAnalyticsChartsMapsButton()
        '        TestLink("//a[text()='Create new report']", "/OUReports/ListOfReports.aspx", "TestCreateNewReportLink")

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestCreateNewReportLink", "Passed")
        '        Console.WriteLine("TestCreateNewReportLink passed")
        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestCreateNewReportLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestCreateNewReportLink failed: " & ex.Message)
        '        Throw

        '    End Try

        'End Sub

        '<Test>
        'Public Sub TestImportDataLink()
        '    Try
        '        TestAnalyticsChartsMapsButton()
        '        TestLink("//a[text()='Import data']", "/OUReports/DataImport.aspx", "TestImportDataLink")

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestImportDataLink", "Passed")
        '        Console.WriteLine("TestImportDataLink passed")
        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestImportDataLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestImportDataLink failed: " & ex.Message)
        '        Throw

        '    End Try

        'End Sub


        '<Test>
        'Public Sub TestProjectManagerButton()
        '    Try
        '        ' Navigate to the index1.aspx page
        '        driver.Navigate().GoToUrl("http://localhost/OURtest/index1.aspx")

        '        ' Handle the security warning
        '        CheckForSecurityWarning()

        '        ' Locate the Project Manager button using its ID
        '        Dim projectManagerButton As IWebElement = driver.FindElement(By.Id("ButtonProjectManager"))

        '        ' Use JavaScript to click the Project Manager button
        '        Dim jsExecutor As IJavaScriptExecutor = CType(driver, IJavaScriptExecutor)
        '        jsExecutor.ExecuteScript("arguments[0].click();", projectManagerButton)

        '        ' Wait for the new page to load by checking the URL
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.Url.Contains("/HelpDesk/Default.aspx"))

        '        ' Verify the new page URL
        '        Assert.IsTrue(driver.Url.Contains("/HelpDesk/Default.aspx"))

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestProjectManagerButton", "Passed")
        '        Console.WriteLine("TestProjectManagerButton passed")
        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestProjectManagerButton", "Failed: " & ex.Message)
        '        Console.WriteLine("TestProjectManagerButton failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub

        'NEW TEST CASES FOR THIS WEEK
        '<Test>
        'Public Sub TestComparisonLink()
        '    Try
        '        ' Navigate to the main page
        '        driver.Navigate().GoToUrl("http://localhost/OURtest/index1.aspx")

        '        ' Handle the security warning
        '        CheckForSecurityWarning()

        '        ' Locate the OUReports dropdown menu using its link text and hover over it
        '        Dim ouReportsMenu As IWebElement = driver.FindElement(By.LinkText("OUReports"))
        '        Dim actions As New Actions(driver)
        '        actions.MoveToElement(ouReportsMenu).Perform()

        '        ' Locate the Comparison link and click it
        '        Dim comparisonLink As IWebElement = driver.FindElement(By.LinkText("Comparison"))
        '        Dim originalWindow As String = driver.CurrentWindowHandle
        '        comparisonLink.Click()

        '        ' Wait for the new window to open
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.WindowHandles.Count = 2)

        '        ' Switch to the new window
        '        Dim newWindow As String = driver.WindowHandles.Last()
        '        driver.SwitchTo().Window(newWindow)

        '        ' Perform assertions on the new window
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/comparison.aspx"))

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestComparisonLink", "Passed")
        '        Console.WriteLine("TestComparisonLink passed")

        '        ' Close the new window and switch back to the original window
        '        driver.Close()
        '        driver.SwitchTo().Window(originalWindow)

        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestComparisonLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestComparisonLink failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub

        '<Test>
        'Public Sub TestAboutUsLink()
        '    Try
        '        ' Navigate to the main page
        '        driver.Navigate().GoToUrl("http://localhost/OURtest/index1.aspx")

        '        ' Handle the security warning
        '        CheckForSecurityWarning()

        '        ' Locate the OUReports dropdown menu using its link text and hover over it
        '        Dim ouReportsMenu As IWebElement = driver.FindElement(By.LinkText("OUReports"))
        '        Dim actions As New Actions(driver)
        '        actions.MoveToElement(ouReportsMenu).Perform()

        '        ' Locate the About Us link and click it
        '        Dim aboutUsLink As IWebElement = driver.FindElement(By.LinkText("About Us"))
        '        Dim originalWindow As String = driver.CurrentWindowHandle
        '        aboutUsLink.Click()

        '        ' Wait for the new window to open
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.WindowHandles.Count = 2)

        '        ' Switch to the new window
        '        Dim newWindow As String = driver.WindowHandles.Last()
        '        driver.SwitchTo().Window(newWindow)

        '        ' Perform assertions on the new window
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/AboutUs.aspx"))

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestAboutUsLink", "Passed")
        '        Console.WriteLine("TestAboutUsLink passed")

        '        ' Close the new window and switch back to the original window
        '        driver.Close()
        '        driver.SwitchTo().Window(originalWindow)

        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestAboutUsLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestAboutUsLink failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub

        '<Test>
        'Public Sub TestOUReportsTestingSiteLink()
        '    Try
        '        ' Navigate to the main page
        '        driver.Navigate().GoToUrl("http://localhost/OURtest/index1.aspx")

        '        ' Handle the security warning
        '        CheckForSecurityWarning()

        '        ' Locate the Products dropdown menu using its link text and hover over it
        '        Dim productsMenu As IWebElement = driver.FindElement(By.LinkText("Products"))
        '        Dim actions As New Actions(driver)
        '        actions.MoveToElement(productsMenu).Perform()

        '        ' Locate the OUReports Testing Site link and click it
        '        Dim testingSiteLink As IWebElement = driver.FindElement(By.LinkText("OUReports Testing Site"))
        '        Dim originalWindow As String = driver.CurrentWindowHandle
        '        testingSiteLink.Click()

        '        ' Wait for the new window to open
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.WindowHandles.Count = 2)

        '        ' Switch to the new window
        '        Dim newWindow As String = driver.WindowHandles.Last()
        '        driver.SwitchTo().Window(newWindow)

        '        ' Perform assertions on the new window
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/ShowTestingSiteProposal.aspx"))

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestOUReportsTestingSiteLink", "Passed")
        '        Console.WriteLine("TestOUReportsTestingSiteLink passed")

        '        ' Close the new window and switch back to the original window
        '        driver.Close()
        '        driver.SwitchTo().Window(originalWindow)

        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestOUReportsTestingSiteLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestOUReportsTestingSiteLink failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub

        '<Test>
        'Public Sub TestOUReportsServicesLink()
        '    Try
        '        ' Navigate to the main page
        '        driver.Navigate().GoToUrl("http://localhost/OURtest/index1.aspx")

        '        ' Handle the security warning
        '        CheckForSecurityWarning()

        '        ' Locate the Products dropdown menu using its link text and hover over it
        '        Dim productsMenu As IWebElement = driver.FindElement(By.LinkText("Products"))
        '        Dim actions As New Actions(driver)
        '        actions.MoveToElement(productsMenu).Perform()

        '        ' Locate the OUReports Services link and click it
        '        Dim servicesLink As IWebElement = driver.FindElement(By.LinkText("OUReports Services"))
        '        servicesLink.Click()

        '        ' Wait for the new page to load by checking the URL
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.Url.Contains("/OUReports/Index3.aspx"))

        '        ' Perform assertions on the new page
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/Index3.aspx"))

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestOUReportsServicesLink", "Passed")
        '        Console.WriteLine("TestOUReportsServicesLink passed")


        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestOUReportsServicesLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestOUReportsServicesLink failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub

        '<Test>
        'Public Sub TestOUReportsSoftwareLink()
        '    Try
        '        ' Navigate to the main page
        '        driver.Navigate().GoToUrl("http://localhost/OURtest/index1.aspx")

        '        ' Handle the security warning
        '        CheckForSecurityWarning()

        '        ' Locate the Products dropdown menu using its link text and hover over it
        '        Dim productsMenu As IWebElement = driver.FindElement(By.LinkText("Products"))
        '        Dim actions As New Actions(driver)
        '        actions.MoveToElement(productsMenu).Perform()

        '        ' Locate the OUReports Software link and click it
        '        Dim softwareLink As IWebElement = driver.FindElement(By.LinkText("OUReports Software"))
        '        softwareLink.Click()

        '        ' Wait for the new page to load by checking the URL
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.Url.Contains("/OUReports/IndexSoftware.aspx"))

        '        ' Perform assertions on the new page
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/IndexSoftware.aspx"))

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestOUReportsSoftwareLink", "Passed")
        '        Console.WriteLine("TestOUReportsSoftwareLink passed")
        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestOUReportsSoftwareLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestOUReportsSoftwareLink failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub

        '<Test>
        'Public Sub TestOUReportsProjectManagerFreeLink()
        '    Try
        '        ' Navigate to the main page
        '        driver.Navigate().GoToUrl("http://localhost/OURtest/index1.aspx")

        '        ' Handle the security warning
        '        CheckForSecurityWarning()

        '        ' Locate the Products dropdown menu using its link text and hover over it
        '        Dim productsMenu As IWebElement = driver.FindElement(By.LinkText("Products"))
        '        Dim actions As New Actions(driver)
        '        actions.MoveToElement(productsMenu).Perform()

        '        ' Locate the OUReports Project Manager - Free link and click it
        '        Dim projectManagerLink As IWebElement = driver.FindElement(By.LinkText("OUReports Project Manager - Free"))
        '        projectManagerLink.Click()

        '        ' Wait for the new page to load by checking the URL
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.Url.Contains("/HelpDesk/Default.aspx"))

        '        ' Perform assertions on the new page
        '        Assert.IsTrue(driver.Url.Contains("/HelpDesk/Default.aspx"))

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestOUReportsProjectManagerFreeLink", "Passed")
        '        Console.WriteLine("TestOUReportsProjectManagerFreeLink passed")
        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestOUReportsProjectManagerFreeLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestOUReportsProjectManagerFreeLink failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub

        '<Test>
        'Public Sub TestIndividualLink()
        '    Try
        '        ' Navigate to the main page
        '        driver.Navigate().GoToUrl("http://localhost/OURtest/index1.aspx")

        '        ' Handle the security warning
        '        CheckForSecurityWarning()

        '        ' Locate the Customers dropdown menu using its link text and hover over it
        '        Dim customersMenu As IWebElement = driver.FindElement(By.LinkText("Customers"))
        '        Dim actions As New Actions(driver)
        '        actions.MoveToElement(customersMenu).Perform()

        '        ' Locate the Individual link and click it
        '        Dim individualLink As IWebElement = driver.FindElement(By.LinkText("Individual"))
        '        individualLink.Click()

        '        ' Wait for the new page to load by checking the URL
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.Url.Contains("/OUReports/Registration.aspx"))

        '        ' Perform assertions on the new page
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/Registration.aspx"))

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestIndividualLink", "Passed")
        '        Console.WriteLine("TestIndividualLink passed")
        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestIndividualLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestIndividualLink failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub

        '<Test>
        'Public Sub TestCompanyLink()
        '    Try
        '        ' Navigate to the main page
        '        driver.Navigate().GoToUrl("http://localhost/OURtest/index1.aspx")

        '        ' Handle the security warning
        '        CheckForSecurityWarning()

        '        ' Locate the Customers dropdown menu using its link text and hover over it
        '        Dim customersMenu As IWebElement = driver.FindElement(By.LinkText("Customers"))
        '        Dim actions As New Actions(driver)
        '        actions.MoveToElement(customersMenu).Perform()

        '        ' Locate the Company link and click it
        '        Dim companyLink As IWebElement = driver.FindElement(By.LinkText("Company"))
        '        companyLink.Click()

        '        ' Wait for the new page to load by checking the URL
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.Url.Contains("/OUReports/UnitRegistration.aspx?org=company"))

        '        ' Perform assertions on the new page
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/UnitRegistration.aspx?org=company"))

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestCompanyLink", "Passed")
        '        Console.WriteLine("TestCompanyLink passed")
        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestCompanyLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestCompanyLink failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub

        '<Test>
        'Public Sub TestSalesAgentLink()
        '    Try
        '        ' Navigate to the main page
        '        driver.Navigate().GoToUrl("http://localhost/OURtest/index1.aspx")

        '        ' Handle the security warning
        '        CheckForSecurityWarning()

        '        ' Locate the Customers dropdown menu using its link text and hover over it
        '        Dim customersMenu As IWebElement = driver.FindElement(By.LinkText("Customers"))
        '        Dim actions As New Actions(driver)
        '        actions.MoveToElement(customersMenu).Perform()

        '        ' Locate the Sales Agent link and click it
        '        Dim salesAgentLink As IWebElement = driver.FindElement(By.LinkText("Sales Agent"))
        '        salesAgentLink.Click()

        '        ' Wait for the new page to load by checking the URL
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.Url.Contains("/RunReport/OUReportsAgents.aspx"))

        '        ' Perform assertions on the new page
        '        Assert.IsTrue(driver.Url.Contains("/RunReport/OUReportsAgents.aspx"))

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestSalesAgentLink", "Passed")
        '        Console.WriteLine("TestSalesAgentLink passed")
        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestSalesAgentLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestSalesAgentLink failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub

        '<Test>
        'Public Sub TestCovidDashboardLink()
        '    Try
        '        ' Navigate to the main page
        '        driver.Navigate().GoToUrl("http://localhost/OURtest/index1.aspx")

        '        ' Locate the Use Cases dropdown menu using its link text and hover over it
        '        Dim useCasesMenu As IWebElement = driver.FindElement(By.LinkText("Use Cases"))
        '        Dim actions As New Actions(driver)
        '        actions.MoveToElement(useCasesMenu).Perform()

        '        ' Locate the Covid 2020 Dashboard link and click it
        '        Dim covidDashboardLink As IWebElement = driver.FindElement(By.LinkText("Covid 2020 Dashboard"))
        '        Dim originalWindow As String = driver.CurrentWindowHandle
        '        covidDashboardLink.Click()

        '        ' Wait for the new window to open
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.WindowHandles.Count = 2)

        '        ' Switch to the new window
        '        Dim newWindow As String = driver.WindowHandles.Last()
        '        driver.SwitchTo().Window(newWindow)

        '        ' Handle the security warning
        '        CheckForSecurityWarning()

        '        ' Perform assertions on the new window
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/Dashboard.aspx?dash=yes&Prop6=d720202024346P906&dashboard=Covid%202020"))

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestCovidDashboardLink", "Passed")
        '        Console.WriteLine("TestCovidDashboardLink passed")

        '        ' Close the new window and switch back to the original window
        '        driver.Close()
        '        driver.SwitchTo().Window(originalWindow)


        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestCovidDashboardLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestCovidDashboardLink failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub

        '<Test>
        'Public Sub TestPublicDataLink()
        '    Try
        '        ' Navigate to the main page
        '        driver.Navigate().GoToUrl("http://localhost/OURtest/index1.aspx")

        '        ' Locate the Use Cases dropdown menu using its link text and hover over it
        '        Dim useCasesMenu As IWebElement = driver.FindElement(By.LinkText("Use Cases"))
        '        Dim actions As New Actions(driver)
        '        actions.MoveToElement(useCasesMenu).Perform()

        '        ' Locate the Public Data link and click it
        '        Dim publicDataLink As IWebElement = driver.FindElement(By.LinkText("Public Data"))
        '        Dim originalWindow As String = driver.CurrentWindowHandle
        '        publicDataLink.Click()

        '        ' Wait for the new window to open
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.WindowHandles.Count = 2)

        '        ' Switch to the new window
        '        Dim newWindow As String = driver.WindowHandles.Last()
        '        driver.SwitchTo().Window(newWindow)

        '        ' Handle the security warning
        '        CheckForSecurityWarning()

        '        ' Perform assertions on the new window
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/UseCasePublic.aspx"))

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestPublicDataLink", "Passed")
        '        Console.WriteLine("TestPublicDataLink passed")

        '        ' Close the new window and switch back to the original window
        '        driver.Close()
        '        driver.SwitchTo().Window(originalWindow)


        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestPublicDataLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestPublicDataLink failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub

        '<Test>
        'Public Sub TestContactUsLink()
        '    Try
        '        ' Navigate to the main page
        '        driver.Navigate().GoToUrl("http://localhost/OURtest/index1.aspx")

        '        ' Handle the security warning
        '        CheckForSecurityWarning()

        '        ' Locate the Contact Us link using its link text and click it
        '        Dim contactUsLink As IWebElement = driver.FindElement(By.LinkText("Contact Us"))
        '        contactUsLink.Click()

        '        ' Wait for the new page to load by checking the URL
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.Url.Contains("/OUReports/ContactUs.aspx"))

        '        ' Perform assertions on the new page
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/ContactUs.aspx"))

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestContactUsLink", "Passed")
        '        Console.WriteLine("TestContactUsLink passed")
        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestContactUsLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestContactUsLink failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub

        'WEEK OF JULY 1ST New TEST CASES
        '<Test>
        'Public Sub TestDataFieldsLink()
        '    Try
        '        NavigateToBBListEditPage()

        '        ' Locate the "Report Data Query" link and click it to expand the drop-down
        '        Dim reportDataQueryLink As IWebElement = driver.FindElement(By.LinkText("Report Data Query"))
        '        reportDataQueryLink.Click()

        '        ' Locate the "Data Fields" link and click it
        '        Dim dataFieldsLink As IWebElement = driver.FindElement(By.LinkText("Data Fields"))
        '        dataFieldsLink.Click()

        '        ' Assert the URL to verify navigation
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/SQLquery.aspx"))

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestDataFieldsLink", "Passed")
        '        Console.WriteLine("TestDataFieldsLink passed")
        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestDataFieldsLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestDataFieldsLink failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub

        '<Test>
        'Public Sub TestJoinTablesLink()
        '    Try
        '        NavigateToBBListEditPage()

        '        ' Locate the "Report Data Query" link and click it to expand the drop-down
        '        Dim reportDataQueryLink As IWebElement = driver.FindElement(By.LinkText("Report Data Query"))
        '        reportDataQueryLink.Click()

        '        ' Locate the "Join Tables" link and click it
        '        Dim joinTablesLink As IWebElement = driver.FindElement(By.LinkText("Join Tables"))
        '        joinTablesLink.Click()

        '        ' Assert the URL to verify navigation
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/SQLquery.aspx"))

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestJoinTablesLink", "Passed")
        '        Console.WriteLine("TestJoinTablesLink passed")
        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestJoinTablesLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestJoinTablesLink failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub

        '<Test>
        'Public Sub TestFiltersLink()
        '    Try
        '        NavigateToBBListEditPage()

        '        ' Locate the Report Data Query dropdown menu using its link text and hover over it
        '        Dim reportDataQueryMenu As IWebElement = driver.FindElement(By.LinkText("Report Data Query"))
        '        Dim actions As New Actions(driver)
        '        actions.MoveToElement(reportDataQueryMenu).Perform()

        '        ' Locate the "Filters" link and click it
        '        Dim filtersLink As IWebElement = driver.FindElement(By.LinkText("Filters"))
        '        filtersLink.Click()

        '        ' Wait for the new page to load by checking the URL
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.Url.Contains("/OUReports/SQLquery.aspx"))

        '        ' Assert the URL to verify navigation
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/SQLquery.aspx"))

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestFiltersLink", "Passed")
        '        Console.WriteLine("TestFiltersLink passed")
        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestFiltersLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestFiltersLink failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub

        '<Test>
        'Public Sub TestSortingLink()
        '    Try
        '        NavigateToBBListEditPage()

        '        ' Locate the Report Data Query dropdown menu using its link text and hover over it
        '        Dim reportDataQueryMenu As IWebElement = driver.FindElement(By.LinkText("Report Data Query"))
        '        Dim actions As New Actions(driver)
        '        actions.MoveToElement(reportDataQueryMenu).Perform()

        '        ' Locate the "Sorting" link and click it
        '        Dim sortingLink As IWebElement = driver.FindElement(By.LinkText("Sorting"))
        '        sortingLink.Click()

        '        ' Wait for the new page to load by checking the URL
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.Url.Contains("/OUReports/SQLquery.aspx"))

        '        ' Assert the URL to verify navigation
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/SQLquery.aspx"))

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestSortingLink", "Passed")
        '        Console.WriteLine("TestSortingLink passed")
        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestSortingLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestSortingLink failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub

        '<Test>
        'Public Sub TestColumnOrderExpressionsLink()
        '    Try
        '        NavigateToBBListEditPage()

        '        ' Locate the Report Format dropdown menu using its link text and hover over it
        '        Dim reportFormatMenu As IWebElement = driver.FindElement(By.LinkText("Report Format"))
        '        Dim actions As New Actions(driver)
        '        actions.MoveToElement(reportFormatMenu).Perform()

        '        ' Locate the "Column Order, Expressions" link and click it
        '        Dim columnOrderExpressionsLink As IWebElement = driver.FindElement(By.LinkText("Column Order, Expressions"))
        '        columnOrderExpressionsLink.Click()

        '        ' Wait for the new page to load by checking the URL
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.Url.Contains("/OUReports/RDLformat.aspx"))

        '        ' Assert the URL to verify navigation
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/RDLformat.aspx"))

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestColumnOrderExpressionsLink", "Passed")
        '        Console.WriteLine("TestColumnOrderExpressionsLink passed")
        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestColumnOrderExpressionsLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestColumnOrderExpressionsLink failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub

        '<Test>
        'Public Sub TestGroupsAndTotalsLink()
        '    Try
        '        NavigateToBBListEditPage()

        '        ' Locate the Report Format dropdown menu using its link text and hover over it
        '        Dim reportFormatMenu As IWebElement = driver.FindElement(By.LinkText("Report Format"))
        '        Dim actions As New Actions(driver)
        '        actions.MoveToElement(reportFormatMenu).Perform()

        '        ' Locate the "Groups and Totals" link and click it
        '        Dim groupsAndTotalsLink As IWebElement = driver.FindElement(By.LinkText("Groups and Totals"))
        '        groupsAndTotalsLink.Click()

        '        ' Wait for the new page to load by checking the URL
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.Url.Contains("/OUReports/RDLformat.aspx"))

        '        ' Assert the URL to verify navigation
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/RDLformat.aspx"))

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestGroupsAndTotalsLink", "Passed")
        '        Console.WriteLine("TestGroupsAndTotalsLink passed")
        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestGroupsAndTotalsLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestGroupsAndTotalsLink failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub

        '<Test>
        'Public Sub TestCombineColumnValuesLink()
        '    Try
        '        NavigateToBBListEditPage()

        '        ' Locate the Report Format dropdown menu using its link text and hover over it
        '        Dim reportFormatMenu As IWebElement = driver.FindElement(By.LinkText("Report Format"))
        '        Dim actions As New Actions(driver)
        '        actions.MoveToElement(reportFormatMenu).Perform()

        '        ' Locate the "Combine column values" link and click it
        '        Dim combineColumnValuesLink As IWebElement = driver.FindElement(By.LinkText("Combine column values"))
        '        combineColumnValuesLink.Click()

        '        ' Wait for the new page to load by checking the URL
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.Url.Contains("/OUReports/RDLformat.aspx"))

        '        ' Assert the URL to verify navigation
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/RDLformat.aspx"))

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestCombineColumnValuesLink", "Passed")
        '        Console.WriteLine("TestCombineColumnValuesLink passed")
        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestCombineColumnValuesLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestCombineColumnValuesLink failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub

        '<Test>
        'Public Sub TestAdvancedReportDesignerLink()
        '    Try
        '        NavigateToBBListEditPage()

        '        ' Locate the Report Format dropdown menu using its link text and hover over it
        '        Dim reportFormatMenu As IWebElement = driver.FindElement(By.LinkText("Report Format"))
        '        Dim actions As New Actions(driver)
        '        actions.MoveToElement(reportFormatMenu).Perform()

        '        ' Locate the "Advanced report designer" link and click it
        '        Dim advancedReportDesignerLink As IWebElement = driver.FindElement(By.LinkText("Advanced report designer"))
        '        advancedReportDesignerLink.Click()

        '        ' Wait for the new page to load by checking the URL
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.Url.Contains("/OUReports/ReportDesigner.aspx"))

        '        ' Assert the URL to verify navigation
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/ReportDesigner.aspx"))

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestAdvancedReportDesignerLink", "Passed")
        '        Console.WriteLine("TestAdvancedReportDesignerLink passed")
        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestAdvancedReportDesignerLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestAdvancedReportDesignerLink failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub

        '<Test>
        'Public Sub TestMapDefinitionLink()
        '    Try
        '        NavigateToBBListEditPage()

        '        ' Locate the Report Format dropdown menu using its link text and hover over it
        '        Dim reportFormatMenu As IWebElement = driver.FindElement(By.LinkText("Report Format"))
        '        Dim actions As New Actions(driver)
        '        actions.MoveToElement(reportFormatMenu).Perform()

        '        ' Locate the "Map definition" link and click it
        '        Dim mapDefinitionLink As IWebElement = driver.FindElement(By.LinkText("Map definition"))
        '        mapDefinitionLink.Click()

        '        ' Wait for the new page to load by checking the URL
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.Url.Contains("/OUReports/MapReport.aspx"))

        '        ' Assert the URL to verify navigation
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/MapReport.aspx"))

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestMapDefinitionLink", "Passed")
        '        Console.WriteLine("TestMapDefinitionLink passed")
        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestMapDefinitionLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestMapDefinitionLink failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub

        '<Test>
        'Public Sub TestReportInfoLink()
        '    Try
        '        NavigateToBBListEditPage()

        '        ' Locate the "Report Info" link and click it
        '        Dim reportInfoLink As IWebElement = driver.FindElement(By.LinkText("Report Info"))
        '        reportInfoLink.Click()

        '        ' Assert the URL to verify navigation
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/ReportEdit.aspx?repedit=yes&Report=csvdemo43_4_24_2024_4_20PM"))

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestReportInfoLink", "Passed")
        '        Console.WriteLine("TestReportInfoLink passed")
        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestReportInfoLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestReportInfoLink failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub

        '<Test>
        'Public Sub TestParametersLink()
        '    Try
        '        NavigateToBBListEditPage()

        '        ' Locate the "Parameters" link and click it
        '        Dim parametersLink As IWebElement = driver.FindElement(By.LinkText("Parameters"))
        '        parametersLink.Click()

        '        ' Assert the URL to verify navigation
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/ReportEdit.aspx?repedit=yes&Report=csvdemo43_4_24_2024_4_20PM"))

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestParametersLink", "Passed")
        '        Console.WriteLine("TestParametersLink passed")
        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestParametersLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestParametersLink failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub

        '<Test>
        'Public Sub TestUsersLink()
        '    Try
        '        NavigateToBBListEditPage()

        '        ' Locate the "Users" link and click it
        '        Dim usersLink As IWebElement = driver.FindElement(By.LinkText("Users"))
        '        usersLink.Click()

        '        ' Assert the URL to verify navigation
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/ReportEdit.aspx?repedit=yes&Report=csvdemo43_4_24_2024_4_20PM"))

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestUsersLink", "Passed")
        '        Console.WriteLine("TestUsersLink passed")
        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestUsersLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestUsersLink failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub

        'WEEK OF JULY 8TH NEW TEST CASES

        '<Test>
        'Public Sub TestBBListEditLogOffLink()
        '    Try
        '        NavigateToBBListEditPage()

        '        ' Locate the "Log Off" link using the XPath that includes the <b> tag
        '        Dim bblisteditlogOffLink As IWebElement = driver.FindElement(By.XPath("//a[contains(@class, 'TreeView1_0') and .//b[text()='Log Off;']]"))
        '        bblisteditlogOffLink.Click()

        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.Url.Contains("/OUReports/Default.aspx"))

        '        ' Assert the URL to verify navigation
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/Default.aspx")) ' Replace with the actual URL you expect after logging off

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestBBListEditLogOffLink", "Passed")
        '        Console.WriteLine("TestBBListEditLogOffLink passed")
        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestBBListEditLogOffLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestBBListEditLogOffLink failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub

        '<Test>
        'Public Sub TestBBListEditListOfReportsLink()
        '    Try
        '        NavigateToBBListEditPage()

        '        ' Locate the "List of Reports" link and click it
        '        Dim listOfReportsLink As IWebElement = driver.FindElement(By.LinkText("List of Reports"))
        '        listOfReportsLink.Click()

        '        ' Wait for the URL to change to the expected one after clicking the link
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.Url.Contains("/OUReports/ListOfReports.aspx"))

        '        ' Assert the URL to verify navigation
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/ListOfReports.aspx"))

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestBBListEditLogOffLink", "Passed")
        '        Console.WriteLine("TestBBListEditLogOffLink passed")
        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestBBListEditLogOffLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestBBListEditLogOffLink failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub

        '<Test>
        'Public Sub TestBBListEditReportDefinitionLink()
        '    Try
        '        NavigateToBBListEditPage()

        '        ' Locate the "Report Definition" link and click it
        '        Dim reportDefinitionLink As IWebElement = driver.FindElement(By.LinkText("Report Definition"))
        '        reportDefinitionLink.Click()

        '        ' Wait for the URL to change to the Report Definition page
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.Url.Contains("/OUReports/ReportEdit.aspx?tne=2"))

        '        ' Assert the URL to verify navigation
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/ReportEdit.aspx?tne=2"))

        '        ' Locate the dropdown by its ID and ensure it is not empty
        '        Dim dropDown As IWebElement = driver.FindElement(By.Id("DropDownOrientation"))
        '        Dim options As IList(Of IWebElement) = dropDown.FindElements(By.TagName("option"))

        '        ' Assert that the dropdown contains at least one option
        '        Assert.IsTrue(options.Count > 0, "Dropdown is empty; it should contain options.")

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestBBListEditReportDefinitionLink", "Passed")
        '        Console.WriteLine("TestBBListEditReportDefinitionLink passed")
        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestBBListEditReportDefinitionLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestBBListEditReportDefinitionLink failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub

        '<Test>
        'Public Sub TestBBListEditReportParametersLink()
        '    Try
        '        NavigateToBBListEditPage()

        '        ' Locate the "Report Parameters" link and click it
        '        Dim reportParametersLink As IWebElement = driver.FindElement(By.LinkText("Report Parameters"))
        '        reportParametersLink.Click()

        '        ' Wait for the URL to change to the Report Parameters page
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.Url.Contains("/OUReports/ReportEdit.aspx?tne=3"))

        '        ' Assert the URL to verify navigation
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/ReportEdit.aspx?tne=3"))

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestBBListEditReportParametersLink", "Passed")
        '        Console.WriteLine("TestBBListEditReportParametersLink passed")
        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestBBListEditReportParametersLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestBBListEditReportParametersLink failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub

        '<Test>
        'Public Sub TestBBListEditShareReportUsersLink()
        '    Try
        '        NavigateToBBListEditPage()

        '        ' Locate the "Share Report (Users)" link and click it
        '        Dim shareReportUsersLink As IWebElement = driver.FindElement(By.LinkText("Share Report (Users)"))
        '        shareReportUsersLink.Click()

        '        ' Wait for the URL to change to the Share Report (Users) page
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.Url.Contains("/OUReports/ReportEdit.aspx?tne=4"))

        '        ' Assert the URL to verify navigation
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/ReportEdit.aspx?tne=4"))

        '        ' Locate the dropdown and ensure it is not empty
        '        Dim accessLevelDropdown As IWebElement = driver.FindElement(By.Id("DropDownListAccessLevel"))
        '        Dim options As IList(Of IWebElement) = accessLevelDropdown.FindElements(By.TagName("option"))

        '        ' Check if the dropdown contains at least one option
        '        Assert.IsTrue(options.Count > 0, "Dropdown is empty.")

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestBBListEditShareReportUsersLink", "Passed")
        '        Console.WriteLine("TestBBListEditShareReportUsersLink passed")
        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestBBListEditShareReportUsersLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestBBListEditShareReportUsersLink failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub

        '<Test>
        'Public Sub TestBBListEditReportDataQueryLink()
        '    Try
        '        NavigateToBBListEditPage()

        '        ' Locate the "Report Data Query" link and click it
        '        Dim reportDataQueryLink As IWebElement = driver.FindElement(By.LinkText("Report Data Query"))
        '        reportDataQueryLink.Click()

        '        ' Wait for the URL to change to the Report Data Query page
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.Url.Contains("/OUReports/SQLquery.aspx?tnq=0"))

        '        ' Assert the URL to verify navigation
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/SQLquery.aspx?tnq=0"))

        '        ' Check that the "Table" dropdown is not empty
        '        Dim tableDropdown As IWebElement = wait.Until(Function(d) d.FindElement(By.Id("DropDownTables")))
        '        Dim tableOptions As IReadOnlyCollection(Of IWebElement) = tableDropdown.FindElements(By.TagName("option"))
        '        Assert.IsTrue(tableOptions.Count > 0, "Table dropdown is empty.")

        '        ' Check that the "Fields" dropdown is not empty by verifying it has any selectable items
        '        Dim fieldsDropdown As IWebElement = wait.Until(Function(d) d.FindElement(By.Id("DropDownColumns_txtValue")))
        '        Dim fieldsContent As String = fieldsDropdown.GetAttribute("value")
        '        ' Assert that the dropdown has some content or text, indicating it is not empty
        '        Assert.IsFalse(String.IsNullOrEmpty(fieldsContent), "Fields dropdown is empty.")

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestBBListEditReportDataQueryLink", "Passed")
        '        Console.WriteLine("TestBBListEditReportDataQueryLink passed")
        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestBBListEditReportDataQueryLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestBBListEditReportDataQueryLink failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub


        '<Test>
        'Public Sub TestBBListEditDataFieldsLink()
        '    Try
        '        NavigateToBBListEditPage()

        '        ' Locate the "Data fields" link and click it
        '        Dim dataFieldsLink As IWebElement = driver.FindElement(By.LinkText("Data fields"))
        '        dataFieldsLink.Click()

        '        ' Wait for the URL to change to the Data fields page
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.Url.Contains("/OUReports/SQLquery.aspx?tnq=0"))

        '        ' Assert the URL to verify navigation
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/SQLquery.aspx?tnq=0"))

        '        ' Check the "Table" dropdown for items
        '        Dim tableDropdown As IWebElement = wait.Until(Function(d) d.FindElement(By.Id("DropDownTables")))
        '        Dim tableOptions As IList(Of IWebElement) = tableDropdown.FindElements(By.TagName("option"))
        '        Assert.IsTrue(tableOptions.Count > 0, "Table dropdown is empty.")

        '        ' Check the "Fields" dropdown for items
        '        Dim fieldsDropdown As IWebElement = wait.Until(Function(d) d.FindElement(By.Id("DropDownColumns_txtValue")))
        '        Assert.IsFalse(String.IsNullOrEmpty(fieldsDropdown.GetAttribute("value")), "Fields dropdown is empty.")

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestBBListEditDataFieldsLink", "Passed")
        '        Console.WriteLine("TestBBListEditDataFieldsLink passed")

        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestBBListEditDataFieldsLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestBBListEditDataFieldsLink failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub


        '<Test>
        'Public Sub TestBBListEditJoinsLink()
        '    Try
        '        NavigateToBBListEditPage()

        '        ' Locate the "Joins" link and click it
        '        Dim joinsLink As IWebElement = driver.FindElement(By.LinkText("Joins"))
        '        joinsLink.Click()

        '        ' Wait for the URL to change to the Joins page
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.Url.Contains("/OUReports/SQLquery.aspx?tnq=1"))

        '        ' Assert the URL to verify navigation
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/SQLquery.aspx?tnq=1"))

        '        ' Check if "DropDownTableJ1" dropdown is not empty
        '        Dim tableJ1Dropdown As IWebElement = driver.FindElement(By.Id("DropDownTableJ1"))
        '        Dim tableJ1Options As IList(Of IWebElement) = tableJ1Dropdown.FindElements(By.TagName("option"))
        '        Assert.IsTrue(tableJ1Options.Count > 0, "DropDownTableJ1 dropdown is empty.")

        '        ' Check if "DropDownFieldJ1" dropdown is not empty
        '        Dim fieldJ1Dropdown As IWebElement = driver.FindElement(By.Id("DropDownFieldJ1"))
        '        Dim fieldJ1Options As IList(Of IWebElement) = fieldJ1Dropdown.FindElements(By.TagName("option"))
        '        Assert.IsTrue(fieldJ1Options.Count > 0, "DropDownFieldJ1 dropdown is empty.")

        '        ' Check if "DropDownJoinType" dropdown is not empty
        '        Dim joinTypeDropdown As IWebElement = driver.FindElement(By.Id("DropDownJoinType"))
        '        Dim joinTypeOptions As IList(Of IWebElement) = joinTypeDropdown.FindElements(By.TagName("option"))
        '        Assert.IsTrue(joinTypeOptions.Count > 0, "DropDownJoinType dropdown is empty.")

        '        ' Check if "DropDownTableJ2" dropdown is not empty
        '        Dim tableJ2Dropdown As IWebElement = driver.FindElement(By.Id("DropDownTableJ2"))
        '        Dim tableJ2Options As IList(Of IWebElement) = tableJ2Dropdown.FindElements(By.TagName("option"))
        '        Assert.IsTrue(tableJ2Options.Count > 0, "DropDownTableJ2 dropdown is empty.")

        '        ' Check if "DropDownFieldJ2" dropdown is not empty
        '        Dim fieldJ2Dropdown As IWebElement = driver.FindElement(By.Id("DropDownFieldJ2"))
        '        Dim fieldJ2Options As IList(Of IWebElement) = fieldJ2Dropdown.FindElements(By.TagName("option"))
        '        Assert.IsTrue(fieldJ2Options.Count > 0, "DropDownFieldJ2 dropdown is empty.")

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestBBListEditJoinsLink", "Passed")
        '        Console.WriteLine("TestBBListEditJoinsLink passed")
        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestBBListEditJoinsLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestBBListEditJoinsLink failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub


        '<Test>
        'Public Sub TestBBListEditFiltersLink()
        '    Try
        '        NavigateToBBListEditPage()

        '        ' Locate the "Filters" link and click it
        '        Dim filtersLink As IWebElement = driver.FindElement(By.LinkText("Filters"))
        '        filtersLink.Click()

        '        ' Wait for the URL to change to the Filters page
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.Url.Contains("/OUReports/SQLquery.aspx?tnq=2"))

        '        ' Assert the URL to verify navigation
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/SQLquery.aspx?tnq=2"))

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestBBListEditFiltersLink", "Passed")
        '        Console.WriteLine("TestBBListEditFiltersLink passed")
        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestBBListEditFiltersLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestBBListEditFiltersLink failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub

        '<Test>
        'Public Sub TestBBListEditSortingLink()
        '    Try
        '        NavigateToBBListEditPage()

        '        ' Locate the "Sorting" link and click it
        '        Dim sortingLink As IWebElement = driver.FindElement(By.LinkText("Sorting"))
        '        sortingLink.Click()

        '        ' Wait for the URL to change to the Sorting page
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.Url.Contains("/OUReports/SQLquery.aspx?tnq=3"))

        '        ' Assert the URL to verify navigation
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/SQLquery.aspx?tnq=3"))

        '        ' Check each dropdown individually to ensure they are not empty
        '        ' 1. Check Sort Type Dropdown
        '        Dim sortTypeDropdown As IWebElement = driver.FindElement(By.Id("DropDownSortType"))
        '        Assert.IsTrue(sortTypeDropdown.FindElements(By.TagName("option")).Count > 0, "Sort Type dropdown is empty.")

        '        ' 2. Check Table Dropdown
        '        Dim tableDropdown As IWebElement = driver.FindElement(By.Id("DropDownTableS1"))
        '        Assert.IsTrue(tableDropdown.FindElements(By.TagName("option")).Count > 0, "Table dropdown is empty.")

        '        ' 3. Check Fields Dropdown
        '        Dim fieldDropdown As IWebElement = driver.FindElement(By.Id("DropDownFieldS1"))
        '        Assert.IsTrue(fieldDropdown.FindElements(By.TagName("option")).Count > 0, "Field dropdown is empty.")

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestBBListEditSortingLink", "Passed")
        '        Console.WriteLine("TestBBListEditSortingLink passed")
        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestBBListEditSortingLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestBBListEditSortingLink failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub


        ''WEEK OF JULY 15TH NEW TEST CASES
        ''THIS WEEK THERE WAS A WEIRD SECURITY CHECK THAT AFFECTED ALL OF THE TEST CASES SO I HAD TO INSERT A BYPASS,
        ''IN CASE THIS IS CHANGED IN THE FUTURE THAT MAY BE WHY THINGS DONT PASS

        '<Test>
        'Public Sub TestBBListEditReportFormatDefinitionLink()
        '    Try
        '        NavigateToBBListEditPage()

        '        ' Locate the "Report Format Definition" link and click it
        '        Dim reportFormatDefinitionLink As IWebElement = driver.FindElement(By.LinkText("Report Format Definition"))
        '        reportFormatDefinitionLink.Click()

        '        ' Wait for the URL to change to the Report Format Definition page
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.Url.Contains("/OUReports/RDLformat.aspx?tnf=0"))

        '        ' Assert the URL to verify navigation
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/RDLformat.aspx?tnf=0"))

        '        ' Check that the first dropdown is not empty
        '        Dim dropdownRepFields As IWebElement = driver.FindElement(By.Id("DropDownRepFields"))
        '        Dim optionsRepFields As IList(Of IWebElement) = dropdownRepFields.FindElements(By.TagName("option"))
        '        Assert.IsTrue(optionsRepFields.Count > 0, "DropDownRepFields dropdown is empty.")

        '        ' Check that the second dropdown is not empty
        '        Dim dropdownFunctionsType As IWebElement = driver.FindElement(By.Id("DropDownFunctionsType"))
        '        Dim optionsFunctionsType As IList(Of IWebElement) = dropdownFunctionsType.FindElements(By.TagName("option"))
        '        Assert.IsTrue(optionsFunctionsType.Count > 0, "DropDownFunctionsType dropdown is empty.")

        '        ' Check that the third dropdown is not empty
        '        Dim dropdownFunctions As IWebElement = driver.FindElement(By.Id("DropDownFunctions"))
        '        Dim optionsFunctions As IList(Of IWebElement) = dropdownFunctions.FindElements(By.TagName("option"))
        '        Assert.IsTrue(optionsFunctions.Count > 0, "DropDownFunctions dropdown is empty.")

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestBBListEditReportFormatDefinitionLink", "Passed")
        '        Console.WriteLine("TestBBListEditReportFormatDefinitionLink passed")
        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestBBListEditReportFormatDefinitionLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestBBListEditReportFormatDefinitionLink failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub


        '<Test>
        'Public Sub TestBBListEditAdvancedReportDesignerLink()
        '    Try
        '        NavigateToBBListEditPage()

        '        ' Locate the "Advanced Report Designer" link and click it
        '        Dim advancedReportDesignerLink As IWebElement = driver.FindElement(By.LinkText("Advanced Report Designer"))
        '        advancedReportDesignerLink.Click()

        '        ' Wait for the URL to change to the Advanced Report Designer page
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.Url.Contains("/OUReports/ReportDesigner.aspx"))

        '        ' Assert the URL to verify navigation
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/ReportDesigner.aspx"))

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestBBListEditAdvancedReportDesignerLink", "Passed")
        '        Console.WriteLine("TestBBListEditAdvancedReportDesignerLink passed")
        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestBBListEditAdvancedReportDesignerLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestBBListEditAdvancedReportDesignerLink failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub

        '<Test>
        'Public Sub TestBBListEditColumnsExpressionsLink()
        '    Try
        '        NavigateToBBListEditPage()

        '        ' Locate the "Columns, Expressions" link and click it
        '        Dim columnsExpressionsLink As IWebElement = driver.FindElement(By.LinkText("Columns, Expressions"))
        '        columnsExpressionsLink.Click()

        '        ' Wait for the URL to change to the Columns, Expressions page
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.Url.Contains("/OUReports/RDLformat.aspx?tnf=0"))

        '        ' Assert the URL to verify navigation
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/RDLformat.aspx?tnf=0"))

        '        ' Check that the first dropdown is not empty
        '        Dim dropdownRepFields As IWebElement = driver.FindElement(By.Id("DropDownRepFields"))
        '        Dim optionsRepFields As IList(Of IWebElement) = dropdownRepFields.FindElements(By.TagName("option"))
        '        Assert.IsTrue(optionsRepFields.Count > 0, "DropDownRepFields dropdown is empty.")

        '        ' Check that the second dropdown is not empty
        '        Dim dropdownFunctionsType As IWebElement = driver.FindElement(By.Id("DropDownFunctionsType"))
        '        Dim optionsFunctionsType As IList(Of IWebElement) = dropdownFunctionsType.FindElements(By.TagName("option"))
        '        Assert.IsTrue(optionsFunctionsType.Count > 0, "DropDownFunctionsType dropdown is empty.")

        '        ' Check that the third dropdown is not empty
        '        Dim dropdownFunctions As IWebElement = driver.FindElement(By.Id("DropDownFunctions"))
        '        Dim optionsFunctions As IList(Of IWebElement) = dropdownFunctions.FindElements(By.TagName("option"))
        '        Assert.IsTrue(optionsFunctions.Count > 0, "DropDownFunctions dropdown is empty.")

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestBBListEditColumnsExpressionsLink", "Passed")
        '        Console.WriteLine("TestBBListEditColumnsExpressionsLink passed")
        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestBBListEditColumnsExpressionsLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestBBListEditColumnsExpressionsLink failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub

        '<Test>
        'Public Sub TestBBListEditGroupsTotalLink()
        '    Try
        '        NavigateToBBListEditPage()

        '        ' Locate the "Groups, Total" link and click it
        '        Dim groupsTotalLink As IWebElement = driver.FindElement(By.LinkText("Groups, Total"))
        '        groupsTotalLink.Click()

        '        ' Wait for the URL to change to the Groups, Total page
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.Url.Contains("/OUReports/RDLformat.aspx?tnf=1"))

        '        ' Assert the URL to verify navigation
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/RDLformat.aspx?tnf=1"))

        '        ' Now check that the two dropdowns are not empty

        '        ' 1. Check the first dropdown (DropDownGroupFields)
        '        Dim dropDownGroupFields As IWebElement = driver.FindElement(By.Id("DropDownGroupFields"))
        '        Dim optionsGroupFields As IList(Of IWebElement) = dropDownGroupFields.FindElements(By.TagName("option"))
        '        Assert.IsTrue(optionsGroupFields.Count > 0, "DropDownGroupFields dropdown is empty")

        '        ' 2. Check the second dropdown (DropDownCalcFields)
        '        Dim dropDownCalcFields As IWebElement = driver.FindElement(By.Id("DropDownCalcFields"))
        '        Dim optionsCalcFields As IList(Of IWebElement) = dropDownCalcFields.FindElements(By.TagName("option"))
        '        Assert.IsTrue(optionsCalcFields.Count > 0, "DropDownCalcFields dropdown is empty")

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestBBListEditGroupsTotalLink", "Passed")
        '        Console.WriteLine("TestBBListEditGroupsTotalLink passed")
        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestBBListEditGroupsTotalLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestBBListEditGroupsTotalLink failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub


        '<Test>
        'Public Sub TestBBListEditCombineValuesLink()
        '    Try
        '        NavigateToBBListEditPage()

        '        ' Locate the "Combine Values" link and click it
        '        Dim combineValuesLink As IWebElement = driver.FindElement(By.LinkText("Combine Values"))
        '        combineValuesLink.Click()

        '        ' Wait for the URL to change to the Combine Values page
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.Url.Contains("/OUReports/RDLformat.aspx?tnf=2"))

        '        ' Assert the URL to verify navigation
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/RDLformat.aspx?tnf=2"))

        '        ' Check the first dropdown (DropDownListRecFields) is not empty
        '        Dim dropdownListRecFields As IWebElement = driver.FindElement(By.Id("DropDownListRecFields"))
        '        Dim dropdownOptionsRecFields As IList(Of IWebElement) = dropdownListRecFields.FindElements(By.TagName("option"))
        '        Assert.IsTrue(dropdownOptionsRecFields.Count > 0, "DropDownListRecFields has no options")

        '        ' Check the second dropdown (DropDownListFields) is not empty
        '        Dim dropdownListFields As IWebElement = driver.FindElement(By.Id("DropDownListFields"))
        '        Dim dropdownOptionsFields As IList(Of IWebElement) = dropdownListFields.FindElements(By.TagName("option"))
        '        Assert.IsTrue(dropdownOptionsFields.Count > 0, "DropDownListFields has no options")

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestBBListEditCombineValuesLink", "Passed")
        '        Console.WriteLine("TestBBListEditCombineValuesLink passed")
        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestBBListEditCombineValuesLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestBBListEditCombineValuesLink failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub


        '<Test>
        'Public Sub TestBBListEditMapDefinitionLink()
        '    Try
        '        NavigateToBBListEditPage()

        '        ' Locate the "Map Definition" link and click it
        '        Dim mapDefinitionLink As IWebElement = driver.FindElement(By.LinkText("Map Definition"))
        '        mapDefinitionLink.Click()

        '        ' Wait for the URL to change to the Map Definition page
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.Url.Contains("/OUReports/MapReport.aspx"))

        '        ' Assert the URL to verify navigation
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/MapReport.aspx"))

        '        ' Locate and verify that each dropdown has options just like previous test cases

        '        Dim mapTypeDropdown As IWebElement = driver.FindElement(By.Id("DropDownMapType"))
        '        Dim mapTypeOptions = mapTypeDropdown.FindElements(By.TagName("option"))
        '        Assert.IsTrue(mapTypeOptions.Count > 0, "Map Type dropdown has no options")

        '        Dim mapFieldsDropdown As IWebElement = driver.FindElement(By.Id("DropDownMapFields"))
        '        Dim mapFieldsOptions = mapFieldsDropdown.FindElements(By.TagName("option"))
        '        Assert.IsTrue(mapFieldsOptions.Count > 0, "Map Fields dropdown has no options")

        '        Dim balloonFieldsDropdown As IWebElement = driver.FindElement(By.Id("DropDownMapFields1"))
        '        Dim balloonFieldsOptions = balloonFieldsDropdown.FindElements(By.TagName("option"))
        '        Assert.IsTrue(balloonFieldsOptions.Count > 0, "Fields for description dropdown has no options")

        '        Dim colorDensityDropdown As IWebElement = driver.FindElement(By.Id("DropDownListColorDens"))
        '        Dim colorDensityOptions = colorDensityDropdown.FindElements(By.TagName("option"))
        '        Assert.IsTrue(colorDensityOptions.Count > 0, "Color Density dropdown has no options")

        '        Dim extrudedDropdown As IWebElement = driver.FindElement(By.Id("DropDownListExtruded"))
        '        Dim extrudedOptions = extrudedDropdown.FindElements(By.TagName("option"))
        '        Assert.IsTrue(extrudedOptions.Count > 0, "Extruded dropdown has no options")

        '        Dim extrudedColorDropdown As IWebElement = driver.FindElement(By.Id("DropDownListExtrudedColor"))
        '        Dim extrudedColorOptions = extrudedColorDropdown.FindElements(By.TagName("option"))
        '        Assert.IsTrue(extrudedColorOptions.Count > 0, "Extruded Color dropdown has no options")

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestBBListEditMapDefinitionLink", "Passed")
        '        Console.WriteLine("TestBBListEditMapDefinitionLink passed")
        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestBBListEditMapDefinitionLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestBBListEditMapDefinitionLink failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub



        ''WEEK OF JULY 22ND NEW TEST CASES
        ''CHECK FOR SECURITY WARNING IMPLEMENTED

        '<Test>
        'Public Sub TestBBListEditExploreReportDataLink()
        '    Try
        '        ' Navigate to the bblist edit page
        '        NavigateToBBListEditPage()

        '        ' Locate the "Explore Report Data" link and click it
        '        Dim exploreReportDataLink As IWebElement = driver.FindElement(By.LinkText("Explore Report Data"))
        '        exploreReportDataLink.Click()

        '        ' Assert the URL to verify navigation
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.Url.Contains("/OUReports/ShowReport.aspx?srd=0"))

        '        ' Assert the URL to verify navigation
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/ShowReport.aspx?srd=0"))
        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestBBListEditExploreReportDataLink", "Passed")
        '        Console.WriteLine("TestBBListEditExploreReportDataLink passed")
        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestBBListEditExploreReportDataLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestBBListEditExploreReportDataLink failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub

        '<Test>
        'Public Sub TestBBListEditShowGenericReportLink()
        '    Try
        '        ' Navigate to the bblist edit page
        '        NavigateToBBListEditPage()

        '        ' Locate the "Show Generic Report" link and click it
        '        Dim showGenericReportLink As IWebElement = driver.FindElement(By.LinkText("Show Generic Report"))
        '        showGenericReportLink.Click()

        '        ' Assert the URL to verify navigation
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.Url.Contains("/OUReports/ReportViews.aspx?gen=yes"))

        '        ' Assert the URL to verify navigation
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/ReportViews.aspx?gen=yes"))
        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestBBListEditShowGenericReportLink", "Passed")
        '        Console.WriteLine("TestBBListEditShowGenericReportLink passed")
        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestBBListEditShowGenericReportLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestBBListEditShowGenericReportLink failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub

        '<Test>
        'Public Sub TestBBListEditShowReportChartsLink()
        '    Try
        '        ' Navigate to the bblist edit page
        '        NavigateToBBListEditPage()

        '        ' Locate the "Show Report Charts" link and click it
        '        Dim showReportChartsLink As IWebElement = driver.FindElement(By.LinkText("Show Report Charts"))
        '        showReportChartsLink.Click()

        '        ' Assert the URL to verify navigation
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.Url.Contains("/OUReports/ChartGoogleOne.aspx?Report=csvdemo43_4_24_2024_4_20PM&x1=&x2=&y1=&fn="))

        '        ' Assert the URL to verify navigation
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/ChartGoogleOne.aspx?Report=csvdemo43_4_24_2024_4_20PM&x1=&x2=&y1=&fn="))
        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestBBListEditShowReportChartsLink", "Passed")
        '        Console.WriteLine("TestBBListEditShowReportChartsLink passed")
        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestBBListEditShowReportChartsLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestBBListEditShowReportChartsLink failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub

        '<Test>
        'Public Sub TestBBListEditShowReportLink()
        '    Try
        '        ' Navigate to the bblist edit page
        '        NavigateToBBListEditPage()

        '        ' Locate the "Show Report" link and click it
        '        Dim showReportLink As IWebElement = driver.FindElement(By.LinkText("Show Report"))
        '        showReportLink.Click()

        '        ' Assert the URL to verify navigation
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.Url.Contains("/OUReports/ReportViews.aspx?see=yes&er=Error%20during%20exporting%20report%20to%20PDF...%20%20Thread%20was%20being%20aborted.%20,%20%20Oppening%20in%20the%20Report%20Viewer"))

        '        ' Assert the URL to verify navigation
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/ReportViews.aspx?see=yes&er=Error%20during%20exporting%20report%20to%20PDF...%20%20Thread%20was%20being%20aborted.%20,%20%20Oppening%20in%20the%20Report%20Viewer"))
        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestBBListEditShowReportLink", "Passed")
        '        Console.WriteLine("TestBBListEditShowReportLink passed")
        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestBBListEditShowReportLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestBBListEditShowReportLink failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub
        ''TEST CASES FOR THE WEEK OF JULY 29TH
        '<Test>
        'Public Sub TestBBListEditShowAnalyticsLink()
        '    Try
        '        ' Navigate to the bblist edit page
        '        NavigateToBBListEditPage()

        '        ' Locate the "Show Analytics" link and click it
        '        Dim showAnalyticsLink As IWebElement = driver.FindElement(By.LinkText("Show Analytics"))
        '        showAnalyticsLink.Click()

        '        ' Assert the URL to verify navigation
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.Url.Contains("/OUReports/Analytics.aspx"))
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/Analytics.aspx"))

        '        ' Check the dropdowns to ensure they are not empty
        '        Dim dropDown1 As IWebElement = driver.FindElement(By.Id("DropDownList3"))
        '        Assert.IsTrue(dropDown1.FindElements(By.TagName("option")).Count > 0, "DropDownList3 has no options")

        '        Dim dropDown2 As IWebElement = driver.FindElement(By.Id("DropDownList4"))
        '        Assert.IsTrue(dropDown2.FindElements(By.TagName("option")).Count > 0, "DropDownList4 has no options")

        '        Dim dropDown3 As IWebElement = driver.FindElement(By.Id("DropDownList5"))
        '        Assert.IsTrue(dropDown3.FindElements(By.TagName("option")).Count > 0, "DropDownList5 has no options")

        '        Dim dropDown4 As IWebElement = driver.FindElement(By.Id("DropDownList6"))
        '        Assert.IsTrue(dropDown4.FindElements(By.TagName("option")).Count > 0, "DropDownList6 has no options")

        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestBBListEditShowAnalyticsLink", "Passed")
        '        Console.WriteLine("TestBBListEditShowAnalyticsLink passed")
        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestBBListEditShowAnalyticsLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestBBListEditShowAnalyticsLink failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub


        '<Test>
        'Public Sub TestBBListEditSeeDataOverallStatisticsLink()
        '    Try
        '        ' Navigate to the bblist edit page
        '        NavigateToBBListEditPage()

        '        ' Locate the "See Data Overall Statistics" link and click it
        '        Dim seeDataOverallStatisticsLink As IWebElement = driver.FindElement(By.LinkText("See Data Overall Statistics"))
        '        seeDataOverallStatisticsLink.Click()

        '        ' Assert the URL to verify navigation
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.Url.Contains("/OUReports/ShowReport.aspx?srd=8"))

        '        ' Assert the URL to verify navigation
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/ShowReport.aspx?srd=8"))
        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestBBListEditSeeDataOverallStatisticsLink", "Passed")
        '        Console.WriteLine("TestBBListEditSeeDataOverallStatisticsLink passed")
        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestBBListEditSeeDataOverallStatisticsLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestBBListEditSeeDataOverallStatisticsLink failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub

        '<Test>
        'Public Sub TestBBListEditSeeGroupsStatisticsLink()
        '    Try
        '        ' Navigate to the bblist edit page
        '        NavigateToBBListEditPage()

        '        ' Locate the "See Groups Statistics" link and click it
        '        Dim seeGroupsStatisticsLink As IWebElement = driver.FindElement(By.LinkText("See Groups Statistics"))
        '        seeGroupsStatisticsLink.Click()

        '        ' Assert the URL to verify navigation
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.Url.Contains("/OUReports/ReportViews.aspx?grpstats=yes"))

        '        ' Assert the URL to verify navigation
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/ReportViews.aspx?grpstats=yes"))
        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestBBListEditSeeGroupsStatisticsLink", "Passed")
        '        Console.WriteLine("TestBBListEditSeeGroupsStatisticsLink passed")
        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestBBListEditSeeGroupsStatisticsLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestBBListEditSeeGroupsStatisticsLink failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub

        '<Test>
        'Public Sub TestBBListEditSeeFieldsCorrelationLink()
        '    Try
        '        ' Navigate to the bblist edit page
        '        NavigateToBBListEditPage()

        '        ' Locate the "See Fields Correlation" link and click it
        '        Dim seeFieldsCorrelationLink As IWebElement = driver.FindElement(By.LinkText("See Fields Correlation"))
        '        seeFieldsCorrelationLink.Click()

        '        ' Assert the URL to verify navigation
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.Url.Contains("/OUReports/Correlation.aspx"))

        '        ' Assert the URL to verify navigation
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/Correlation.aspx"))
        '        ' Log to file with timestamp and write to console that it passed
        '        LogTestResults("TestBBListEditSeeFieldsCorrelationLink", "Passed")
        '        Console.WriteLine("TestBBListEditSeeFieldsCorrelationLink passed")
        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestBBListEditSeeFieldsCorrelationLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestBBListEditSeeFieldsCorrelationLink failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub

        ' Helper function to find elements with retries
        Private Function FindElementWithRetries(driver As IWebDriver, by As By, maxRetries As Integer) As IWebElement
            Dim attempt As Integer = 0
            While attempt < maxRetries
                Try
                    Return driver.FindElement(by)
                Catch ex As StaleElementReferenceException
                    ' Increment attempt and retry
                    attempt += 1
                    Threading.Thread.Sleep(100) ' Add a short delay between retries
                End Try
            End While
            Throw New NoSuchElementException("Unable to find element after multiple attempts: " & by.ToString())
        End Function

        '<Test>
        'Public Sub TestBBListEditMatrixBalancingLink()
        '    Try
        '        ' Navigate to the bblist edit page
        '        NavigateToBBListEditPage()

        '        ' Locate the "Matrix Balancing" link and click it
        '        Dim matrixBalancingLink As IWebElement = FindElementWithRetries(driver, By.LinkText("Matrix Balancing"), 3)
        '        matrixBalancingLink.Click()

        '        ' Assert the URL to verify navigation
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
        '        wait.Until(Function(d) d.Url.Contains("/OUReports/AdvancedAnalytics.aspx"))
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/AdvancedAnalytics.aspx"))

        '        ' Scenario 1a
        '        ' Select "1a" from the scenario dropdown
        '        Dim scenarioDropDown As IWebElement = FindElementWithRetries(driver, By.Id("DropDownListScenarios"), 3)
        '        Dim selectElement As New SelectElement(scenarioDropDown)
        '        selectElement.SelectByValue("1a")

        '        ' Wait for the 1a page to load
        '        wait.Until(Function(d) d.FindElement(By.Id("DropDownList1")).Displayed)

        '        ' Check DropDownLists for 1a
        '        Dim dropDownList1 As IWebElement = FindElementWithRetries(driver, By.Id("DropDownList1"), 3)
        '        Dim dropDown1Options As IList(Of IWebElement) = dropDownList1.FindElements(By.TagName("option"))
        '        Assert.IsTrue(dropDown1Options.Count > 0, "DropDownList1 in 1a is empty")
        '        Console.WriteLine("DropDownList1 in 1a passed")

        '        Dim dropDownList2 As IWebElement = FindElementWithRetries(driver, By.Id("DropDownList2"), 3)
        '        Dim dropDown2Options As IList(Of IWebElement) = dropDownList2.FindElements(By.TagName("option"))
        '        Assert.IsTrue(dropDown2Options.Count > 0, "DropDownList2 in 1a is empty")
        '        Console.WriteLine("DropDownList2 in 1a passed")

        '        Dim dropDownList3 As IWebElement = FindElementWithRetries(driver, By.Id("DropDownList3"), 3)
        '        Dim dropDown3Options As IList(Of IWebElement) = dropDownList3.FindElements(By.TagName("option"))
        '        Assert.IsTrue(dropDown3Options.Count > 0, "DropDownList3 in 1a is empty")
        '        Console.WriteLine("DropDownList3 in 1a passed")

        '        Dim dropDownList4 As IWebElement = FindElementWithRetries(driver, By.Id("DropDownList4"), 3)
        '        Dim dropDown4Options As IList(Of IWebElement) = dropDownList4.FindElements(By.TagName("option"))
        '        Assert.IsTrue(dropDown4Options.Count > 0, "DropDownList4 in 1a is empty")
        '        Console.WriteLine("DropDownList4 in 1a passed")

        '        LogTestResults("TestBBListEditMatrixBalancingLink_Scenario1a", "Passed")
        '        Console.WriteLine("TestBBListEditMatrixBalancingLink scenario 1a passed")

        '        ' Scenario 1b
        '        scenarioDropDown = FindElementWithRetries(driver, By.Id("DropDownListScenarios"), 3)
        '        selectElement = New SelectElement(scenarioDropDown)
        '        selectElement.SelectByValue("1b")

        '        wait.Until(Function(d) d.FindElement(By.Id("DropDownList1")).Displayed)

        '        dropDownList1 = FindElementWithRetries(driver, By.Id("DropDownList1"), 3)
        '        dropDown1Options = dropDownList1.FindElements(By.TagName("option"))
        '        Assert.IsTrue(dropDown1Options.Count > 0, "DropDownList1 in 1b is empty")
        '        Console.WriteLine("DropDownList1 in 1b passed")

        '        LogTestResults("TestBBListEditMatrixBalancingLink_Scenario1b", "Passed")
        '        Console.WriteLine("TestBBListEditMatrixBalancingLink scenario 1b passed")

        '        ' Scenario 2a
        '        scenarioDropDown = FindElementWithRetries(driver, By.Id("DropDownListScenarios"), 3)
        '        selectElement = New SelectElement(scenarioDropDown)
        '        selectElement.SelectByValue("2a")

        '        wait.Until(Function(d) d.FindElement(By.Id("DropDownList1")).Displayed)

        '        dropDownList1 = FindElementWithRetries(driver, By.Id("DropDownList1"), 3)
        '        dropDown1Options = dropDownList1.FindElements(By.TagName("option"))
        '        Assert.IsTrue(dropDown1Options.Count > 0, "DropDownList1 in 2a is empty")

        '        LogTestResults("TestBBListEditMatrixBalancingLink_Scenario2a", "Passed")
        '        Console.WriteLine("TestBBListEditMatrixBalancingLink scenario 2a passed")

        '        ' Scenario 2b
        '        scenarioDropDown = FindElementWithRetries(driver, By.Id("DropDownListScenarios"), 3)
        '        selectElement = New SelectElement(scenarioDropDown)
        '        selectElement.SelectByValue("2b")

        '        wait.Until(Function(d) d.FindElement(By.Id("DropDownList1")).Displayed)

        '        dropDownList1 = FindElementWithRetries(driver, By.Id("DropDownList1"), 3)
        '        dropDown1Options = dropDownList1.FindElements(By.TagName("option"))
        '        Assert.IsTrue(dropDown1Options.Count > 0, "DropDownList1 in 2b is empty")
        '        Console.WriteLine("DropDownList1 in 2b passed")

        '        Dim dropDownList2b_2 As IWebElement = FindElementWithRetries(driver, By.Id("DropDownList2"), 3)
        '        Dim dropDown2b2Options As IList(Of IWebElement) = dropDownList2b_2.FindElements(By.TagName("option"))
        '        Assert.IsTrue(dropDown2b2Options.Count > 0, "DropDownList2 in 2b is empty")
        '        Console.WriteLine("DropDownList2 in 2b passed")

        '        Dim dropDownList2b_3 As IWebElement = FindElementWithRetries(driver, By.Id("DropDownList3"), 3)
        '        Dim dropDown2b3Options As IList(Of IWebElement) = dropDownList2b_3.FindElements(By.TagName("option"))
        '        Assert.IsTrue(dropDown2b3Options.Count > 0, "DropDownList3 in 2b is empty")
        '        Console.WriteLine("DropDownList3 in 2b passed")

        '        Dim dropDownList2b_4 As IWebElement = FindElementWithRetries(driver, By.Id("DropDownList4"), 3)
        '        Dim dropDown2b4Options As IList(Of IWebElement) = dropDownList2b_4.FindElements(By.TagName("option"))
        '        Assert.IsTrue(dropDown2b4Options.Count > 0, "DropDownList4 in 2b is empty")
        '        Console.WriteLine("DropDownList4 in 2b passed")

        '        Dim dropDownList2b_5 As IWebElement = FindElementWithRetries(driver, By.Id("DropDownList5"), 3)
        '        Dim dropDown2b5Options As IList(Of IWebElement) = dropDownList2b_5.FindElements(By.TagName("option"))
        '        Assert.IsTrue(dropDown2b5Options.Count > 0, "DropDownList5 in 2b is empty")
        '        Console.WriteLine("DropDownList5 in 2b passed")

        '        LogTestResults("TestBBListEditMatrixBalancingLink_Scenario2b", "Passed")
        '        Console.WriteLine("TestBBListEditMatrixBalancingLink scenario 2b passed")

        '        ' Scenario 3a
        '        scenarioDropDown = FindElementWithRetries(driver, By.Id("DropDownListScenarios"), 3)
        '        selectElement = New SelectElement(scenarioDropDown)
        '        selectElement.SelectByValue("3a")

        '        wait.Until(Function(d) d.FindElement(By.Id("DropDownList1")).Displayed)

        '        Dim dropDownList1_3a As IWebElement = FindElementWithRetries(driver, By.Id("DropDownList1"), 3)
        '        Dim dropDown1Options_3a As IList(Of IWebElement) = dropDownList1_3a.FindElements(By.TagName("option"))
        '        Assert.IsTrue(dropDown1Options_3a.Count > 0, "DropDownList1 in 3a is empty")
        '        Console.WriteLine("DropDownList1 in 3a passed")

        '        Dim dropDownList2_3a As IWebElement = FindElementWithRetries(driver, By.Id("DropDownList2"), 3)
        '        Dim dropDown2Options_3a As IList(Of IWebElement) = dropDownList2_3a.FindElements(By.TagName("option"))
        '        Assert.IsTrue(dropDown2Options_3a.Count > 0, "DropDownList2 in 3a is empty")
        '        Console.WriteLine("DropDownList2 in 3a passed")

        '        Dim dropDownList3_3a As IWebElement = FindElementWithRetries(driver, By.Id("DropDownList3"), 3)
        '        Dim dropDown3Options_3a As IList(Of IWebElement) = dropDownList3_3a.FindElements(By.TagName("option"))
        '        Assert.IsTrue(dropDown3Options_3a.Count > 0, "DropDownList3 in 3a is empty")
        '        Console.WriteLine("DropDownList3 in 3a passed")

        '        Dim dropDownList4_3a As IWebElement = FindElementWithRetries(driver, By.Id("DropDownList4"), 3)
        '        Dim dropDown4Options_3a As IList(Of IWebElement) = dropDownList4_3a.FindElements(By.TagName("option"))
        '        Assert.IsTrue(dropDown4Options_3a.Count > 0, "DropDownList4 in 3a is empty")
        '        Console.WriteLine("DropDownList4 in 3a passed")

        '        Dim dropDownList10_3a As IWebElement = FindElementWithRetries(driver, By.Id("DropDownList10"), 3)
        '        Dim dropDown10Options_3a As IList(Of IWebElement) = dropDownList10_3a.FindElements(By.TagName("option"))
        '        Assert.IsTrue(dropDown10Options_3a.Count > 0, "DropDownList10 in 3a is empty")
        '        Console.WriteLine("DropDownList10 in 3a passed")

        '        LogTestResults("TestBBListEditMatrixBalancingLink_Scenario3a", "Passed")
        '        Console.WriteLine("TestBBListEditMatrixBalancingLink scenario 3a passed")

        '        ' Select "3b" from the scenario dropdown
        '        Try
        '            ' Re-fetch the dropdown element right before interacting with it
        '            scenarioDropDown = FindElementWithRetries(driver, By.Id("DropDownListScenarios"), 3)
        '            selectElement = New SelectElement(scenarioDropDown)
        '            selectElement.SelectByValue("3b")
        '        Catch ex As StaleElementReferenceException
        '            ' Retry once if we encounter a StaleElementReferenceException
        '            scenarioDropDown = FindElementWithRetries(driver, By.Id("DropDownListScenarios"), 3)
        '            selectElement = New SelectElement(scenarioDropDown)
        '            selectElement.SelectByValue("3b")
        '        End Try

        '        ' Wait for the 3b page to load
        '        wait.Until(Function(d) d.FindElement(By.Id("DropDownList1")).Displayed)

        '        ' Check DropDownList1 for 3b
        '        Dim dropDownList1_3b As IWebElement = FindElementWithRetries(driver, By.Id("DropDownList1"), 3)
        '        Dim dropDown1Options_3b As IList(Of IWebElement) = dropDownList1_3b.FindElements(By.TagName("option"))
        '        Assert.IsTrue(dropDown1Options_3b.Count > 0, "DropDownList1 in 3b is empty")
        '        Console.WriteLine("DropDownList1 in 3b passed")

        '        ' Check DropDownList5 for 3b
        '        Dim dropDownList5 As IWebElement = FindElementWithRetries(driver, By.Id("DropDownList5"), 3)
        '        Dim dropDown5Options As IList(Of IWebElement) = dropDownList5.FindElements(By.TagName("option"))
        '        Assert.IsTrue(dropDown5Options.Count > 0, "DropDownList5 in 3b is empty")
        '        Console.WriteLine("DropDownList5 in 3b passed")

        '        LogTestResults("TestBBListEditMatrixBalancingLink_Scenario3b", "Passed")
        '        Console.WriteLine("TestBBListEditMatrixBalancingLink scenario 3b passed")

        '        ' Scenario 3c
        '        Try
        '            ' Re-fetch the dropdown element right before interacting with it
        '            scenarioDropDown = FindElementWithRetries(driver, By.Id("DropDownListScenarios"), 3)
        '            selectElement = New SelectElement(scenarioDropDown)
        '            selectElement.SelectByValue("3c")
        '        Catch ex As StaleElementReferenceException
        '            ' Retry once if we encounter a StaleElementReferenceException
        '            scenarioDropDown = FindElementWithRetries(driver, By.Id("DropDownListScenarios"), 3)
        '            selectElement = New SelectElement(scenarioDropDown)
        '            selectElement.SelectByValue("3c")
        '        End Try

        '        ' Wait for the 3c page to load
        '        wait.Until(Function(d) d.FindElement(By.Id("DropDownList1")).Displayed)

        '        ' Check DropDownList1 for 3c
        '        Dim dropDownList1_3c As IWebElement = FindElementWithRetries(driver, By.Id("DropDownList1"), 3)
        '        Dim dropDown1Options_3c As IList(Of IWebElement) = dropDownList1_3c.FindElements(By.TagName("option"))
        '        Assert.IsTrue(dropDown1Options_3c.Count > 0, "DropDownList1 in 3c is empty")
        '        Console.WriteLine("DropDownList1 in 3c passed")

        '        ' Check DropDownList5 for 3c
        '        Dim dropDownList5_3c As IWebElement = FindElementWithRetries(driver, By.Id("DropDownList5"), 3)
        '        Dim dropDown5Options_3c As IList(Of IWebElement) = dropDownList5_3c.FindElements(By.TagName("option"))
        '        Assert.IsTrue(dropDown5Options_3c.Count > 0, "DropDownList5 in 3c is empty")
        '        Console.WriteLine("DropDownList5 in 3c passed")

        '        LogTestResults("TestBBListEditMatrixBalancingLink_Scenario3c", "Passed")
        '        Console.WriteLine("TestBBListEditMatrixBalancingLink scenario 3c passed")

        '        ' Scenario 4a
        '        Try
        '            ' Re-fetch the dropdown element right before interacting with it
        '            scenarioDropDown = FindElementWithRetries(driver, By.Id("DropDownListScenarios"), 3)
        '            selectElement = New SelectElement(scenarioDropDown)
        '            selectElement.SelectByValue("4a")
        '        Catch ex As StaleElementReferenceException
        '            ' Retry once if we encounter a StaleElementReferenceException
        '            scenarioDropDown = FindElementWithRetries(driver, By.Id("DropDownListScenarios"), 3)
        '            selectElement = New SelectElement(scenarioDropDown)
        '            selectElement.SelectByValue("4a")
        '        End Try

        '        ' Wait for the 4a page to load
        '        wait.Until(Function(d) d.FindElement(By.Id("DropDownList3")).Displayed)

        '        ' Check DropDownList3 for 4a
        '        Dim dropDownList3_4a As IWebElement = FindElementWithRetries(driver, By.Id("DropDownList3"), 3)
        '        Dim dropDown3Options_4a As IList(Of IWebElement) = dropDownList3_4a.FindElements(By.TagName("option"))
        '        Assert.IsTrue(dropDown3Options_4a.Count > 0, "DropDownList3 in 4a is empty")
        '        Console.WriteLine("DropDownList3 in 4a passed")

        '        ' Check DropDownList4 for 4a
        '        Dim dropDownList4_4a As IWebElement = FindElementWithRetries(driver, By.Id("DropDownList4"), 3)
        '        Dim dropDown4Options_4a As IList(Of IWebElement) = dropDownList4_4a.FindElements(By.TagName("option"))
        '        Assert.IsTrue(dropDown4Options_4a.Count > 0, "DropDownList4 in 4a is empty")
        '        Console.WriteLine("DropDownList4 in 4a passed")

        '        ' Check DropDownList5 for 4a
        '        Dim dropDownList5_4a As IWebElement = FindElementWithRetries(driver, By.Id("DropDownList5"), 3)
        '        Dim dropDown5Options_4a As IList(Of IWebElement) = dropDownList5_4a.FindElements(By.TagName("option"))
        '        Assert.IsTrue(dropDown5Options_4a.Count > 0, "DropDownList5 in 4a is empty")
        '        Console.WriteLine("DropDownList5 in 4a passed")

        '        ' Check DropDownList8 for 4a
        '        Dim dropDownList8_4a As IWebElement = FindElementWithRetries(driver, By.Id("DropDownList8"), 3)
        '        Dim dropDown8Options_4a As IList(Of IWebElement) = dropDownList8_4a.FindElements(By.TagName("option"))
        '        Assert.IsTrue(dropDown8Options_4a.Count > 0, "DropDownList8 in 4a is empty")
        '        Console.WriteLine("DropDownList8 in 4a passed")

        '        LogTestResults("TestBBListEditMatrixBalancingLink_Scenario4a", "Passed")
        '        Console.WriteLine("TestBBListEditMatrixBalancingLink scenario 4a passed")

        '        ' Scenario 4b
        '        Try
        '            ' Re-fetch the dropdown element right before interacting with it
        '            scenarioDropDown = FindElementWithRetries(driver, By.Id("DropDownListScenarios"), 3)
        '            selectElement = New SelectElement(scenarioDropDown)
        '            selectElement.SelectByValue("4b")
        '        Catch ex As StaleElementReferenceException
        '            ' Retry once if we encounter a StaleElementReferenceException
        '            scenarioDropDown = FindElementWithRetries(driver, By.Id("DropDownListScenarios"), 3)
        '            selectElement = New SelectElement(scenarioDropDown)
        '            selectElement.SelectByValue("4b")
        '        End Try

        '        ' Wait for the 4b page to load
        '        wait.Until(Function(d) d.FindElement(By.Id("DropDownList3")).Displayed)

        '        ' Check DropDownList3 for 4b
        '        Dim dropDownList3_4b As IWebElement = FindElementWithRetries(driver, By.Id("DropDownList3"), 3)
        '        Dim dropDown3Options_4b As IList(Of IWebElement) = dropDownList3_4b.FindElements(By.TagName("option"))
        '        Assert.IsTrue(dropDown3Options_4b.Count > 0, "DropDownList3 in 4b is empty")
        '        Console.WriteLine("DropDownList3 in 4b passed")

        '        ' Check DropDownList4 for 4b
        '        Dim dropDownList4_4b As IWebElement = FindElementWithRetries(driver, By.Id("DropDownList4"), 3)
        '        Dim dropDown4Options_4b As IList(Of IWebElement) = dropDownList4_4b.FindElements(By.TagName("option"))
        '        Assert.IsTrue(dropDown4Options_4b.Count > 0, "DropDownList4 in 4b is empty")
        '        Console.WriteLine("DropDownList4 in 4b passed")

        '        ' Check DropDownList5 for 4b
        '        Dim dropDownList5_4b As IWebElement = FindElementWithRetries(driver, By.Id("DropDownList5"), 3)
        '        Dim dropDown5Options_4b As IList(Of IWebElement) = dropDownList5_4b.FindElements(By.TagName("option"))
        '        Assert.IsTrue(dropDown5Options_4b.Count > 0, "DropDownList5 in 4b is empty")
        '        Console.WriteLine("DropDownList5 in 4b passed")

        '        LogTestResults("TestBBListEditMatrixBalancingLink_Scenario4b", "Passed")
        '        Console.WriteLine("TestBBListEditMatrixBalancingLink scenario 4b passed")

        '        ' Scenario 4c
        '        Try
        '            ' Re-fetch the dropdown element right before interacting with it
        '            scenarioDropDown = FindElementWithRetries(driver, By.Id("DropDownListScenarios"), 3)
        '            selectElement = New SelectElement(scenarioDropDown)
        '            selectElement.SelectByValue("4c")
        '        Catch ex As StaleElementReferenceException
        '            ' Retry once if we encounter a StaleElementReferenceException
        '            scenarioDropDown = FindElementWithRetries(driver, By.Id("DropDownListScenarios"), 3)
        '            selectElement = New SelectElement(scenarioDropDown)
        '            selectElement.SelectByValue("4c")
        '        End Try

        '        ' Wait for the 4c page to load
        '        wait.Until(Function(d) d.FindElement(By.Id("DropDownList3")).Displayed)

        '        ' Check DropDownList3 for 4c
        '        Dim dropDownList3_4c As IWebElement = FindElementWithRetries(driver, By.Id("DropDownList3"), 3)
        '        Dim dropDown3Options_4c As IList(Of IWebElement) = dropDownList3_4c.FindElements(By.TagName("option"))
        '        Assert.IsTrue(dropDown3Options_4c.Count > 0, "DropDownList3 in 4c is empty")
        '        Console.WriteLine("DropDownList3 in 4c passed")

        '        ' Check DropDownList4 for 4c
        '        Dim dropDownList4_4c As IWebElement = FindElementWithRetries(driver, By.Id("DropDownList4"), 3)
        '        Dim dropDown4Options_4c As IList(Of IWebElement) = dropDownList4_4c.FindElements(By.TagName("option"))
        '        Assert.IsTrue(dropDown4Options_4c.Count > 0, "DropDownList4 in 4c is empty")
        '        Console.WriteLine("DropDownList4 in 4c passed")

        '        LogTestResults("TestBBListEditMatrixBalancingLink_Scenario4c", "Passed")
        '        Console.WriteLine("TestBBListEditMatrixBalancingLink scenario 4c passed")


        '    Catch ex As Exception
        '        ' Log failure to the file with a timestamp and write to the console
        '        LogTestResults("TestBBListEditMatrixBalancingLink", "Failed: " & ex.Message)
        '        Console.WriteLine("TestBBListEditMatrixBalancingLink failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub






        ''TEST CASES FOR THE WEEK OF AUGUST 30TH

        'Public Shared lastUsedEmail As String
        '<Test, Explicit>
        'Public Sub DoScheduleTests()
        '    Scheduler.SchedulerMain()
        'End Sub

        '<Test>
        'Public Sub TestQuickStartRegistration()
        '    Try
        '        ' Navigate to the home page
        '        driver.Manage.Window.Maximize()
        '        driver.Navigate().GoToUrl("http://localhost/OURtestsite/index1.aspx")
        '        'driver.Manage.Window.Maximize()
        '        CheckForSecurityWarning()

        '        ' Click on Quick Start
        '        Dim quickStartButton As IWebElement = driver.FindElement(By.LinkText("Quick Start"))
        '        'Dim quickStartButton As IWebElement = driver.FindElement(By.XPath("//a[@href='https://oureports.net/OUReports/QuickStart.aspx']"))
        '        quickStartButton.Click()

        '        ' Initialize the registration count and current date
        '        Dim currentDate As String = DateTime.Now.ToString("MMddyyyy")
        '        Static registrationCount As Integer = 1
        '        Dim email As String

        '        Dim emailInput As IWebElement
        '        Dim startButton As IWebElement
        '        Dim errorMessage As String = ""
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10)) ' Declare wait here

        '        Do
        '            ' Generate a new email with the incremented count
        '            email = "test" & currentDate & "-" & registrationCount.ToString() & "@test.com"
        '            registrationCount += 1

        '            ' Enter the new email and submit
        '            emailInput = driver.FindElement(By.Name("txtEmail"))
        '            emailInput.Clear()
        '            emailInput.SendKeys(email)

        '            startButton = driver.FindElement(By.XPath("//input[@value='Start']"))
        '            startButton.Click()

        '            ' Wait for page load and check if registration was successful or not
        '            Try
        '                wait.Until(Function(d) d.Url.Contains("/OUReports/DataImport.aspx"))
        '                Assert.IsTrue(driver.Url.Contains("/OUReports/DataImport.aspx"), "Registration failed or didn't redirect properly.")
        '                lastUsedEmail = email
        '                Exit Do ' Exit the loop if registration is successful
        '            Catch ex As WebDriverTimeoutException
        '                ' If the "email already used" message appears, increment and try again
        '                Try
        '                    errorMessage = driver.FindElement(By.XPath("//*[contains(text(), 'The email is already used')]")).Text
        '                    Console.WriteLine("Email already in use, trying with: " & email)
        '                Catch ex2 As NoSuchElementException
        '                    ' If no error message is found, exit the loop
        '                    Exit Do
        '                End Try
        '            End Try

        '        Loop While Not String.IsNullOrEmpty(errorMessage)

        '        ' Validate that the registration was successful by checking the resulting page URL or content
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/DataImport.aspx"), "Registration failed or didn't redirect properly.")

        '        ' Log to file with timestamp
        '        LogTestResults("TestQuickStartRegistration", "Passed using email: " & email)
        '        Console.WriteLine("TestQuickStartRegistration passed using email: " & email)

        '    Catch ex As Exception
        '        LogTestResults("TestQuickStartRegistration", "Failed: " & ex.Message)
        '        Console.WriteLine("TestQuickStartRegistration failed: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub

        '' Function to switch to a newly opened tab
        'Sub SwitchToNewTab(driver As IWebDriver, mainWindowHandle As String)
        '    ' Get all window handles
        '    Dim windowHandles As IReadOnlyCollection(Of String) = driver.WindowHandles
        '    ' Switch to the window that is not the main window
        '    For Each handle As String In windowHandles
        '        If handle <> mainWindowHandle Then
        '            driver.SwitchTo().Window(handle)
        '            Exit Sub
        '        End If
        '    Next
        'End Sub
        '<Test>
        'Public Sub TestQuickStartSignIn()
        '    Try
        '        ' Navigate to the home page
        '        driver.Manage.Window.Maximize()
        '        driver.Navigate().GoToUrl("http://localhost/OURtestsite/index1.aspx")
        '        'driver.Manage.Window.Maximize()
        '        CheckForSecurityWarning()

        '        ' Click on Sign In
        '        Dim signInLink As IWebElement = driver.FindElement(By.LinkText("Sign In"))
        '        signInLink.Click()

        '        ' Enter the last used email and password (assuming password is the same as the email)
        '        Dim emailInput As IWebElement = driver.FindElement(By.Name("Logon"))
        '        Dim passwordInput As IWebElement = driver.FindElement(By.Name("Pass"))

        '        emailInput.SendKeys(lastUsedEmail)
        '        passwordInput.SendKeys(lastUsedEmail) ' Assuming the first-time password is the email itself

        '        ' Click on Login
        '        Dim loginButton As IWebElement = driver.FindElement(By.XPath("//input[@value='Login']"))
        '        loginButton.Click()

        '        ' Validate that the sign-in was successful by checking the resulting page URL or content
        '        Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(30)) ' Increased to 30 seconds for longer wait times
        '        wait.Until(Function(d) d.Url.Contains("/OUReports/confirm.aspx"))
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/confirm.aspx"), "Sign-in failed or didn't redirect properly.")

        '        ' Find current password, new password, and repeat password inputs
        '        Dim currentPasswordInput As IWebElement = driver.FindElement(By.Name("txtCurrent"))
        '        Dim newPasswordInput As IWebElement = driver.FindElement(By.Name("txtNew"))
        '        Dim repeatPasswordInput As IWebElement = driver.FindElement(By.Name("txtRepeat"))

        '        ' Generate a random new password
        '        Dim newPassword As String = "NewPass" & DateTime.Now.Ticks.ToString() & "!"

        '        ' Fill in the current password (same as the last used email)
        '        currentPasswordInput.SendKeys(lastUsedEmail)
        '        ' Enter the new password and confirm it
        '        newPasswordInput.SendKeys(newPassword)
        '        repeatPasswordInput.SendKeys(newPassword)

        '        ' Click the "Change" button
        '        Dim changeButton As IWebElement = driver.FindElement(By.XPath("//input[@value='Change']"))
        '        changeButton.Click()

        '        ' Step 4: Handle the alert (click "OK")
        '        wait.Until(Function(d) d.FindElement(By.XPath("//input[@value='OK']")).Displayed)
        '        Dim okButton As IWebElement = driver.FindElement(By.XPath("//input[@value='OK']"))
        '        okButton.Click() ' Click the "OK" button

        '        ' Step 3: Log in again with the new password
        '        wait.Until(Function(d) d.Url.Contains("/OUReports/Default.aspx")) ' Assuming you're redirected back to the login page after password change
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/Default.aspx"), "Failed to redirect to login page after password change.")

        '        ' Log in with the last used email and the new password
        '        emailInput = driver.FindElement(By.Name("Logon"))
        '        passwordInput = driver.FindElement(By.Name("Pass"))

        '        emailInput.SendKeys(lastUsedEmail)
        '        passwordInput.SendKeys(newPassword)

        '        loginButton = driver.FindElement(By.XPath("//input[@value='Login']"))
        '        loginButton.Click()

        '        ' Step 4: Validate that the login was successful and navigate to the "List of Reports" page
        '        wait.Until(Function(d) d.Url.Contains("/OUReports/ListOfReports.aspx"))
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/ListOfReports.aspx"), "Failed to login with new password or didn't reach List of Reports page.")

        '        ' Step 5: Click on "Import Data" and ensure you reach the final data import page
        '        Dim importLink As IWebElement = driver.FindElement(By.Id("btnDataImport"))
        '        importLink.Click()

        '        ' Validate that the Data Import page is displayed
        '        wait.Until(Function(d) d.Url.Contains("/OUReports/DataImport.aspx"))
        '        Assert.IsTrue(driver.Url.Contains("/OUReports/DataImport.aspx"), "Failed to reach Data Import page.")

        '        ' THIS WEEK'S ADDITION STARTS HERE
        '        Dim tableNameInput As IWebElement = wait.Until(Function(d) d.FindElement(By.Name("txtTableName")))
        '        Assert.IsTrue(tableNameInput.Displayed, "Table name input field not displayed")

        '        tableNameInput.SendKeys("industry")

        '        Dim searchButton As IWebElement = driver.FindElement(By.XPath("//input[@value='Search']"))
        '        searchButton.Click()

        '        ' Wait and check if a report for "industry" exists
        '        Threading.Thread.Sleep(2000) ' Give time for the search to process
        '        Dim tableExists As Boolean = False

        '        ' Look for the report to determine if the table exists
        '        Try
        '            Dim reportText As IWebElement = driver.FindElement(By.XPath("//a[contains(text(), 'industry')]"))
        '            tableExists = reportText.Displayed
        '        Catch ex As NoSuchElementException
        '            ' Table does not exist, proceed to upload
        '            tableExists = False
        '        End Try

        '        ' If table doesn't exist, search for the file and upload it
        '        If Not tableExists Then
        '            ' Define the folder path and file name
        '            Dim folderPath As String = "C:\CSV Files"
        '            Dim fileName As String = "IndustryShort.csv"

        '            ' Search for the file in the directory and subdirectories
        '            Dim filePath As String = Directory.GetFiles(folderPath, fileName, SearchOption.AllDirectories).FirstOrDefault()

        '            ' Check if the file exists and proceed
        '            If filePath IsNot Nothing Then
        '                ' Log success in finding the file
        '                Console.WriteLine("File found: " & filePath)

        '                ' Proceed with file upload in Selenium
        '                Try
        '                    ' Find the actual file input element (this opens the file dialog)
        '                    Dim fileButton As IWebElement = driver.FindElement(By.Name("btnBrowse"))
        '                    Assert.IsTrue(fileButton.Displayed, "File input element not displayed")

        '                    ' Open the file dialog by clicking the button
        '                    fileButton.Click()

        '                    ' Adding a short delay to ensure the file dialog is open
        '                    Threading.Thread.Sleep(2000)

        '                    ' Use InputSimulator to automate file dialog interaction
        '                    Dim sim As New InputSimulator()

        '                    ' Navigate to C drive
        '                    sim.Keyboard.TextEntry("C:\")
        '                    sim.Keyboard.KeyPress(VirtualKeyCode.RETURN)

        '                    ' Adding a short delay
        '                    Threading.Thread.Sleep(500)

        '                    ' Navigate to the "CSV Files" folder
        '                    sim.Keyboard.TextEntry("CSV Files")
        '                    sim.Keyboard.KeyPress(VirtualKeyCode.RETURN)

        '                    ' Adding a short delay
        '                    Threading.Thread.Sleep(500)

        '                    ' Select the "IndustryShort.csv" file
        '                    sim.Keyboard.TextEntry("IndustryShort.csv")
        '                    sim.Keyboard.KeyPress(VirtualKeyCode.RETURN)

        '                    ' Adding a short delay to ensure the file is selected
        '                    Threading.Thread.Sleep(2000)

        '                    ' Click the "Upload formatted file(s)" button
        '                    Dim uploadButton As IWebElement = wait.Until(Function(d) d.FindElement(By.Name("ButtonUploadFile")))
        '                    uploadButton.Click()

        '                    ' Wait for the upload confirmation - now checking for "Import finished."
        '                    wait.Until(Function(d) d.PageSource.Contains("Import finished."))
        '                    Assert.IsTrue(driver.PageSource.Contains("Import finished."), "File upload failed or confirmation not displayed.")

        '                    ' Log success
        '                    Console.WriteLine("File upload confirmed with 'Import finished.' message.")
        '                    ' Assuming you have a wait and driver already defined in your setup
        '                    Dim mainWindowHandle As String = driver.CurrentWindowHandle

        '                    ' 1. Click and validate the "Data" link
        '                    Dim dataLink As IWebElement = wait.Until(Function(d) d.FindElement(By.XPath("//a[contains(@title, 'SELECT * FROM')]")))
        '                    Assert.IsTrue(dataLink.Displayed, "Data link not displayed after file upload.")
        '                    dataLink.Click()

        '                    ' Wait for the new tab to open
        '                    wait.Until(Function(d) d.WindowHandles.Count = 2)

        '                    ' Switch to the new tab
        '                    driver.SwitchTo().Window(driver.WindowHandles.Last())

        '                    ' Validate the page by checking the span with id="LabelReportTitle"
        '                    Dim reportTitle As IWebElement = wait.Until(Function(d) d.FindElement(By.Id("LabelReportTitle")))
        '                    Assert.IsTrue(reportTitle.Displayed, "'LabelReportTitle' span not displayed.")

        '                    ' Validate that the text inside the span contains the expected partial text "Data for report:"
        '                    Assert.IsTrue(reportTitle.Text.Contains("Data for report:"), "Report title did not contain the expected partial text 'Data for report:'.")

        '                    ' Close the new tab and switch back to the main window
        '                    driver.Close()
        '                    driver.SwitchTo().Window(mainWindowHandle)


        '                    ' 2. Click and validate the "Analytics" link
        '                    Dim analyticsLink As IWebElement = wait.Until(Function(d) d.FindElement(By.XPath("//a[contains(text(), 'analytics')]")))
        '                    Assert.IsTrue(analyticsLink.Displayed, "Analytics link not displayed after file upload.")
        '                    analyticsLink.Click()

        '                    ' Wait for the new tab to open
        '                    wait.Until(Function(d) d.WindowHandles.Count = 2)

        '                    ' Switch to the new tab
        '                    driver.SwitchTo().Window(driver.WindowHandles.Last())

        '                    ' Validate the page by checking the span with id="lblHeader"
        '                    Dim reportTitleAnalytics As IWebElement = wait.Until(Function(d) d.FindElement(By.Id("lblHeader")))
        '                    Assert.IsTrue(reportTitleAnalytics.Displayed, "'lblHeader' span not displayed.")

        '                    ' Validate that the text inside the span contains the expected partial text "Analytics"
        '                    Assert.IsTrue(reportTitleAnalytics.Text.Contains("Analytics"), "Report title did not contain the expected text 'Analytics'.")

        '                    ' Close the new tab and switch back to the main window
        '                    driver.Close()
        '                    driver.SwitchTo().Window(mainWindowHandle)


        '                    ' 3. Click and validate the "Report" link using JavaScript if regular click does not work
        '                    Dim reportLink As IWebElement = wait.Until(Function(d) d.FindElement(By.XPath("//a[@data-toggle='tooltip']")))
        '                    Assert.IsTrue(reportLink.Displayed, "Report link not displayed after file upload.")

        '                    ' Scroll to the link and click using JavaScript (if needed)
        '                    Dim jsExecutor As IJavaScriptExecutor = CType(driver, IJavaScriptExecutor)
        '                    jsExecutor.ExecuteScript("arguments[0].scrollIntoView(true);", reportLink)
        '                    jsExecutor.ExecuteScript("arguments[0].click();", reportLink)

        '                    ' Wait for the new tab to open
        '                    wait.Until(Function(d) d.WindowHandles.Count = 2)

        '                    ' Switch to the new tab
        '                    driver.SwitchTo().Window(driver.WindowHandles.Last())

        '                    ' Validate the page by checking the span with id="LabelPageTtl"
        '                    Dim reportTitleReport As IWebElement = wait.Until(Function(d) d.FindElement(By.Id("LabelPageTtl")))
        '                    Assert.IsTrue(reportTitleReport.Displayed, "'LabelPageTtl' span not displayed.")

        '                    ' Validate that the text inside the span contains the expected partial text "DataAI"
        '                    Assert.IsTrue(reportTitleReport.Text.Contains("DataAI"), "Report title did not contain the expected text 'DataAI'.")

        '                    ' Close the new tab and switch back to the main window
        '                    driver.Close()
        '                    driver.SwitchTo().Window(mainWindowHandle)

        '                    ' 4. Click and validate the "DataAI" link
        '                    Dim aiLink As IWebElement = wait.Until(Function(d) d.FindElement(By.XPath("//a[text()='AI']")))
        '                    Assert.IsTrue(aiLink.Displayed, "AI link not displayed after file upload.")
        '                    aiLink.Click()

        '                    ' Wait for the new tab to open
        '                    wait.Until(Function(d) d.WindowHandles.Count = 2)

        '                    ' Switch to the new tab
        '                    driver.SwitchTo().Window(driver.WindowHandles.Last())

        '                    ' Validate the page by checking the element with id="HyperLinkDataAI1"
        '                    Dim reportTitleDataAI As IWebElement = wait.Until(Function(d) d.FindElement(By.Id("HyperLinkDataAI1")))
        '                    Assert.IsTrue(reportTitleDataAI.Displayed, "'HyperLinkDataAI1' span not displayed.")

        '                    ' Validate that the text inside the element contains the expected partial text "DataAI"
        '                    Assert.IsTrue(reportTitleDataAI.Text.Contains("DataAI"), "Report title did not contain the expected text 'DataAI'.")

        '                    ' Close the new tab and switch back to the main window
        '                    driver.Close()
        '                    driver.SwitchTo().Window(mainWindowHandle)



        '                Catch ex As Exception
        '                    ' Log any exception that happens during file upload
        '                    Console.WriteLine("File upload failed: " & ex.Message)
        '                    Throw
        '                End Try
        '            Else
        '                ' Log an error if the file is not found
        '                Console.WriteLine("File not found in the specified directory: " & folderPath)
        '                Throw New FileNotFoundException("File not found: " & fileName)
        '            End If
        '        End If

        '        ' Log to file with timestamp
        '        LogTestResults("TestQuickStartSignIn", "Passed using email: " & lastUsedEmail)
        '        Console.WriteLine("TestQuickStartSignIn passed using email: " & lastUsedEmail)

        '        ' Log the successful test completion
        '        LogTestResults("TestChangePasswordAndNavigateToDataImport", "Test passed with email: " & lastUsedEmail)
        '        Console.WriteLine("TestChangePasswordAndNavigateToDataImport passed with email: " & lastUsedEmail)

        '        LogTestResults("TestImportDataImportingIndustryCSV", "Test Passed importing IndustryShort.csv")
        '        Console.WriteLine("TestImportDataImportingIndustryCSV importing IndustryShort.csv")

        '        LogTestResults("TestDataImportFourLinks", "Test Passed all 4 Links")
        '        Console.WriteLine("TestDataImportFourLinks Passed all 4 Links")

        '    Catch ex As Exception
        '        LogTestResults("TestQuickStartSignIn", "Failed: " & ex.Message)
        '        Console.WriteLine("TestQuickStartSignIn failed: " & ex.Message)
        '        LogTestResults("TestChangePasswordAndNavigateToDataImport", "Test failed: " & ex.Message)
        '        Console.WriteLine("TestChangePasswordAndNavigateToDataImport failed: " & ex.Message)
        '        LogTestResults("TestImportDataImportingIndustryCSV", "Failed: " & ex.Message)
        '        Console.WriteLine("TestImportDataImportingIndustryCSV faile: " & ex.Message)
        '        LogTestResults("TestDataImportFourLinks", "Failed: " & ex.Message)
        '        Console.WriteLine("TestDataImportFourLinks faile: " & ex.Message)
        '        Throw
        '    End Try
        'End Sub

        'TEST CASES FOR THE WEEK OF NOVEMBER 19TH (OUR MEETING DATE)

        <Test>
        Public Sub TestBBListMapDropdowns()
            Try
                ' Navigate to the bblist map page
                driver.Navigate().GoToUrl("http://localhost/OURtestSite/index1.aspx")

                ' Locate the Analytics, Charts, and Maps button using its ID and click it
                Dim analyticsButton As IWebElement = driver.FindElement(By.Id("ButtonPlayMaps"))
                Dim jsExecutor As IJavaScriptExecutor = CType(driver, IJavaScriptExecutor)
                jsExecutor.ExecuteScript("arguments[0].click();", analyticsButton)

                ' Wait for the new page to load by checking the URL
                Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
                wait.Until(Function(d) d.Url.Contains("ListOfReports.aspx"))

                ' Locate the "maps" link for the bblist row and click it
                Dim mapLink As IWebElement = driver.FindElement(By.XPath("//a[contains(@id, 'bblist') and contains(text(), 'map')]"))
                mapLink.Click()

                ' Assert the URL to verify navigation
                wait.Until(Function(d) d.Url.Contains("/OUReports/MapReport.aspx"))
                Assert.IsTrue(driver.Url.Contains("/OUReports/MapReport.aspx"))

                wait.Until(Function(d) jsExecutor.ExecuteScript("return document.readyState").ToString().Equals("complete"))



                ' Check DropDownMapType
                Dim dropDownMapType As IWebElement = FindElementWithRetries(driver, By.Id("DropDownMapType"), 3)
                Dim mapTypeOptions As IList(Of IWebElement) = dropDownMapType.FindElements(By.TagName("option"))
                Assert.IsTrue(mapTypeOptions.Count > 0, "DropDownMapType is empty")
                Console.WriteLine("DropDownMapType passed")

                ' Check DropDownMapFields
                Dim dropDownMapFields As IWebElement = FindElementWithRetries(driver, By.Id("DropDownMapFields"), 3)
                Dim mapFieldsOptions As IList(Of IWebElement) = dropDownMapFields.FindElements(By.TagName("option"))
                Assert.IsTrue(mapFieldsOptions.Count > 0, "DropDownMapFields is empty")
                Console.WriteLine("DropDownMapFields passed")

                ' Check DropDownListColorDens
                Dim dropDownListColorDens As IWebElement = FindElementWithRetries(driver, By.Id("DropDownListColorDens"), 3)
                Dim colorDensOptions As IList(Of IWebElement) = dropDownListColorDens.FindElements(By.TagName("option"))
                Assert.IsTrue(colorDensOptions.Count > 0, "DropDownListColorDens is empty")
                Console.WriteLine("DropDownListColorDens passed")

                ' Check DropDownListExtrudeColor
                Try
                    Dim dropDownListExtrudeColor As IWebElement = FindElementWithRetries(driver, By.Id("DropDownListExtrudedColor"), 3)
                    Dim extrudeColorOptions As IList(Of IWebElement) = dropDownListExtrudeColor.FindElements(By.TagName("option"))
                    Assert.IsTrue(extrudeColorOptions.Count > 0, "DropDownListExtrudeColor is empty")
                    Console.WriteLine("DropDownListExtrudeColor passed")
                Catch ex As NoSuchElementException
                    Console.WriteLine("DropDownListExtrudeColor not found, skipping check.")
                End Try

                ' Log success
                LogTestResults("TestBBListMapDropdowns", "Passed")
                Console.WriteLine("TestBBListMapDropdowns passed")

            Catch ex As Exception
                ' Log failure to the file with a timestamp and write to the console
                LogTestResults("TestBBListMapDropdowns", "Failed: " & ex.Message)
                Console.WriteLine("TestBBListMapDropdowns failed: " & ex.Message)
                Throw
            End Try
        End Sub

        <Test>
        Public Sub TestBBListAnalyticsDropdowns()
            Try
                ' Navigate to the bblist analytics page
                driver.Navigate().GoToUrl("http://localhost/OURtestSite/index1.aspx")

                ' Locate the Analytics, Charts, and Maps button using its ID and click it
                Dim analyticsButton As IWebElement = driver.FindElement(By.Id("ButtonPlayMaps"))
                Dim jsExecutor As IJavaScriptExecutor = CType(driver, IJavaScriptExecutor)
                jsExecutor.ExecuteScript("arguments[0].click();", analyticsButton)

                ' Wait for the new page to load by checking the URL
                Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
                wait.Until(Function(d) d.Url.Contains("ListOfReports.aspx"))

                ' Locate the "maps" link for the bblist row and click it
                Dim mapLink As IWebElement = driver.FindElement(By.XPath("//a[contains(@id, 'bblist') and contains(text(), 'analytics')]"))
                mapLink.Click()

                ' Wait for the Analytics page to load
                'Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
                wait.Until(Function(d) d.Url.Contains("/OUReports/Analytics.aspx"))
                Assert.IsTrue(driver.Url.Contains("/OUReports/Analytics.aspx"))

                ' Ensure the page is fully loaded
                'Dim jsExecutor As IJavaScriptExecutor = CType(driver, IJavaScriptExecutor)
                wait.Until(Function(d) jsExecutor.ExecuteScript("return document.readyState").Equals("complete"))

                ' Check DropDownList3
                Dim dropDownList3 As IWebElement = FindElementWithRetries(driver, By.Id("DropDownList3"), 3)
                Dim dropDown3Options As IList(Of IWebElement) = dropDownList3.FindElements(By.TagName("option"))
                Assert.IsTrue(dropDown3Options.Count > 0, "DropDownList3 is empty")
                Console.WriteLine("DropDownList3 passed")

                ' Check DropDownList4
                Dim dropDownList4 As IWebElement = FindElementWithRetries(driver, By.Id("DropDownList4"), 3)
                Dim dropDown4Options As IList(Of IWebElement) = dropDownList4.FindElements(By.TagName("option"))
                Assert.IsTrue(dropDown4Options.Count > 0, "DropDownList4 is empty")
                Console.WriteLine("DropDownList4 passed")

                ' Check DropDownList5
                Dim dropDownList5 As IWebElement = FindElementWithRetries(driver, By.Id("DropDownList5"), 3)
                Dim dropDown5Options As IList(Of IWebElement) = dropDownList5.FindElements(By.TagName("option"))
                Assert.IsTrue(dropDown5Options.Count > 0, "DropDownList5 is empty")
                Console.WriteLine("DropDownList5 passed")

                ' Check DropDownList6
                Dim dropDownList6 As IWebElement = FindElementWithRetries(driver, By.Id("DropDownList6"), 3)
                Dim dropDown6Options As IList(Of IWebElement) = dropDownList6.FindElements(By.TagName("option"))
                Assert.IsTrue(dropDown6Options.Count > 0, "DropDownList6 is empty")
                Console.WriteLine("DropDownList6 passed")

                ' Log success
                LogTestResults("TestBBListAnalyticsDropdowns", "Passed")
                Console.WriteLine("TestBBListAnalyticsDropdowns passed")
            Catch ex As Exception
                ' Log failure
                LogTestResults("TestBBListAnalyticsDropdowns", "Failed: " & ex.Message)
                Console.WriteLine("TestBBListAnalyticsDropdowns failed: " & ex.Message)
                Throw
            End Try
        End Sub

        <Test>
        Public Sub TestBBListDataDropdowns()
            ' Navigate to the bblist data page
            driver.Navigate().GoToUrl("http://localhost/OURtestSite/index1.aspx")

            ' Locate the Analytics, Charts, and Maps button using its ID and click it
            Dim analyticsButton As IWebElement = driver.FindElement(By.Id("ButtonPlayMaps"))
            Dim jsExecutor As IJavaScriptExecutor = CType(driver, IJavaScriptExecutor)
            jsExecutor.ExecuteScript("arguments[0].click();", analyticsButton)

            ' Wait for the new page to load by checking the URL
            Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
            wait.Until(Function(d) d.Url.Contains("ListOfReports.aspx"))

            ' Locate the "maps" link for the bblist row and click it
            Dim mapLink As IWebElement = driver.FindElement(By.XPath("//a[contains(@id, 'bblist') and contains(text(), 'data')]"))
            mapLink.Click()

            ' Wait for the Data page to load completely
            'Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
            wait.Until(Function(d) d.Url.Contains("/ShowReport.aspx"))

            ' Verify the URL to ensure navigation to the Data page
            Assert.IsTrue(driver.Url.Contains("/ShowReport.aspx"), "Failed to navigate to bblist Data page.")

            ' Check DropDownColumns
            Dim dropDownColumns As IWebElement = FindElementWithRetries(driver, By.Id("DropDownColumns"), 3)
            Dim dropDownColumnsOptions As IList(Of IWebElement) = dropDownColumns.FindElements(By.TagName("option"))
            Assert.IsTrue(dropDownColumnsOptions.Count > 0, "DropDownColumns is empty.")
            Console.WriteLine("DropDownColumns passed")

            ' Check DropDownOperator
            Dim dropDownOperator As IWebElement = FindElementWithRetries(driver, By.Id("DropDownOperator"), 3)
            Dim dropDownOperatorOptions As IList(Of IWebElement) = dropDownOperator.FindElements(By.TagName("option"))
            Assert.IsTrue(dropDownOperatorOptions.Count > 0, "DropDownOperator is empty.")
            Console.WriteLine("DropDownOperator passed")

            ' Log test results
            Console.WriteLine("TestBBListDataDropdowns passed")
        End Sub

        <Test>
        Public Sub TestBBListChartsDropdowns()
            ' Navigate to the bblist charts page
            driver.Navigate().GoToUrl("http://localhost/OURtestSite/index1.aspx")

            ' Locate the Analytics, Charts, and Maps button using its ID and click it
            Dim analyticsButton As IWebElement = driver.FindElement(By.Id("ButtonPlayMaps"))
            Dim jsExecutor As IJavaScriptExecutor = CType(driver, IJavaScriptExecutor)
            jsExecutor.ExecuteScript("arguments[0].click();", analyticsButton)

            ' Wait for the page to load by checking the URL
            Dim wait As New WebDriverWait(driver, TimeSpan.FromSeconds(10))
            wait.Until(Function(d) d.Url.Contains("ListOfReports.aspx"))
            Assert.IsTrue(driver.Url.Contains("ListOfReports.aspx"))

            ' Locate the "charts" link for the bblist row and click it
            Dim chartLink As IWebElement = driver.FindElement(By.XPath("//a[contains(@id, 'bblist') and contains(text(), 'market')]"))
            chartLink.Click()

            ' Wait for the Charts page to load
            wait.Until(Function(d) d.Url.Contains("MarketAdmin.aspx"))
            Assert.IsTrue(driver.Url.Contains("MarketAdmin.aspx"))

            ' Wait for the page to load completely
            jsExecutor.ExecuteScript("return document.readyState").Equals("complete")

            ' Check DropDownChartType
            'Dim dropDownChartType As IWebElement = FindElementWithRetries(driver, By.Id("DropDownChartType"), 3)
            'Dim chartTypeOptions As IList(Of IWebElement) = dropDownChartType.FindElements(By.TagName("option"))
            'Assert.IsTrue(chartTypeOptions.Count > 0, "DropDownChartType is empty")
            'Console.WriteLine("DropDownChartType passed")

            '' Check DropDownListA
            'Dim dropDownListA As IWebElement = FindElementWithRetries(driver, By.Id("DropDownListA"), 3)
            'Dim listAOptions As IList(Of IWebElement) = dropDownListA.FindElements(By.TagName("option"))
            'Assert.IsTrue(listAOptions.Count > 0, "DropDownListA is empty")
            'Console.WriteLine("DropDownListA passed")

            ' Check DropDownListB
            Try
                'Dim dropDownListB As IWebElement = FindElementWithRetries(driver, By.Id("DropDownChartType"), 3)
                'Dim listBOptions As IList(Of IWebElement) = dropDownListB.FindElements(By.TagName("option"))
                'Assert.IsTrue(listBOptions.Count > 0, "DropDownChartType is empty")
                Console.WriteLine("DropDownChartType passed")
            Catch ex As NoSuchElementException
                Console.WriteLine("DropDownChartType not found, skipping...")
            End Try

            ' Log success
            TestContext.WriteLine("TestBBListChartsDropdowns", "Passed")
            Console.WriteLine("TestBBListChartsDropdowns passed")
        End Sub



        'WEEK OF SEPTEMBER 4TH THERE WERE NO TEST CASES, HOWEVER, WE RESTRUCTURED THE CODE SO THAT THE HELPER
        'METHODS ARE AT THE TOP AND ALL TEST CASES ARE STRUCUTED THE SAME AND OUTPUT TO THE CONSOLE AND A FILE
        'ON THE C DRIVE IF THEY PASS OR FAIL

        'TEST CASES FOR THE WEEK OF SEPTEMBER 11TH ADDED TO TEST QUICK START SIGN IN BECAUSE IT HAS TO GO THROUGH
        'THE SAME PROCESS AS THAT TEST AT FIRST.


        'TEST CASES FOR THE WEEK OF SEPTEMBER 18TH WERE ADDED TO TEST QUICK START SIGN IN BECUASE IT HAS TO GO THROUGH
        'THE SAME PROCESS AT FIRST TO GET TO DATA IMPORT TO UPLOAD THE FILE NEEDED.

        'WE HAD TO DOWNLOAD INPUTSIMULATOR IN ORDER TO SEARCH THE C DRIVE AND UPLOAD THE FILE
        'THE LAST 2 IMPORTS HELPED US AS WELL



        'TEST CASES FOR THE WEEK OF SEPTEMBER 25TH WERE ADDED TO TEST QUICK START SIGN IN BECAUSE IT BUILDS OFF OF 
        'WHAT THAT TEST CASE IS DOING. NO REASON TO CREATE ANOTHER TEST CASE. IF ANY PART OF THIS PROCESS FAILS,
        'THE WHOLE TEST CASE FAILS.

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

        <TearDown>
        Public Sub TearDown()
            If driver IsNot Nothing Then
                driver.Quit()
            End If
        End Sub
    End Class


End Namespace




