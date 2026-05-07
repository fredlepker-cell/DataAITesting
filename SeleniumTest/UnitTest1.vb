Imports System.Drawing
Imports System.IO
Imports System.Linq.Expressions
Imports NUnit.Framework
Imports OpenQA.Selenium
Imports OpenQA.Selenium.Chrome
Imports OpenQA.Selenium.Support.UI


Namespace SeleniumVB

    Public Class Tests
        Dim driver As IWebDriver
        <SetUp>
        Public Sub Setup()
            Dim path As String = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName
            driver = New ChromeDriver(path & "\bin\debug\net8.0\")
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10)
            'driver = New ChromeDriver(path & "\Drivers\")

        End Sub

        <Test>
        Public Sub VerifyHomePage()
            driver.Manage.Window.Maximize()
            driver.Navigate().GoToUrl("http://localhost/ourtestusers/index1.aspx")
            Dim btnQuickStart As IWebElement = driver.FindElement(By.LinkText("Quick Start"))

            Assert.IsTrue(btnQuickStart.Displayed)
        End Sub

        <Test>
        Public Sub VerifyQuickStart()
            Try
                driver.Manage.Window.Maximize()
                driver.Navigate().GoToUrl("http://localhost/ourtestusers/index1.aspx")

                Dim wait As WebDriverWait = New WebDriverWait(driver, TimeSpan.FromSeconds(10))
                Dim btnQuickStart As IWebElement = driver.FindElement(By.LinkText("Quick Start"))
                Dim btnStart As IWebElement
                Dim txtEmail As IWebElement
                Dim errMessage As String = String.Empty
                Dim email As String = String.Empty
                Dim lastUsedEmail As String = String.Empty
                Static registrationCount As Integer = 1
                Dim currentDate As String = Today.ToString("MMddyyyy")


                If btnQuickStart.Displayed Then

                    btnQuickStart.Click()
                    wait.Until(Function(d) d.Url.Contains("QuickStart.aspx"))

                    ' Verify the new page URL
                    Assert.IsTrue(driver.Url.Contains("QuickStart.aspx"), "VerifyQuickStart failed.")
                    Do
                        email = "test" & currentDate & "-" & registrationCount.ToString() & "@test.com"
                        registrationCount += 1

                        txtEmail = driver.FindElement(By.Id("txtEmail"))
                        txtEmail.Clear()
                        txtEmail.SendKeys(email)

                        btnStart = driver.FindElement(By.Id("btStart"))
                        btnStart.Click()

                        Try
                            wait.Until(Function(d) d.Url.Contains("/ourtestusers/DataImport.aspx"))
                            Assert.IsTrue(driver.Url.Contains("/ourtestusers/DataImport.aspx"), "Registration failed or didn't redirect properly.")
                            lastUsedEmail = email
                            Exit Do ' Exit the loop if registration is successful
                        Catch ex As WebDriverTimeoutException
                            ' If the "email already used" message appears, increment and try again
                            Try
                                errMessage = driver.FindElement(By.XPath("//*[contains(text(), 'The email is already used')]")).Text
                                Console.WriteLine("Email already in use, trying with: " & email)
                            Catch ex2 As NoSuchElementException
                                ' If no error message is found, exit the loop
                                Exit Do
                            End Try
                        End Try
                    Loop While Not String.IsNullOrEmpty(errMessage)

                End If
                ' Validate that the registration was successful by checking the resulting page URL or content
                Assert.IsTrue(driver.Url.Contains("/ourtestusers/DataImport.aspx"), "Registration failed or didn't redirect properly.")

                Console.WriteLine("VerifyQuickStart passed using email " & email)

            Catch ex As StaleElementReferenceException
                Return
            Catch ex As Exception
                Console.WriteLine("VerifyQuickStart failed: " & ex.Message)
            End Try


        End Sub
        <TearDown>
        Public Sub TearDown()
            If driver IsNot Nothing Then
                driver.Quit()
            End If
        End Sub
    End Class

End Namespace