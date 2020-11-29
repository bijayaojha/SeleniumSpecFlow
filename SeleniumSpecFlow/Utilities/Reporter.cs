using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using TechTalk.SpecFlow;

namespace SeleniumSpecFlow.Utilities
{
    public static class Reporter
    {
        private const string ReportTitle = "Test Report";
        private const string ReportName = "SpecFlow Tests";
        private const string KlovURL = "http://localhost";
        private const string MongoURL = "localhost";
        private const int mongoPort = 27017;
        private static readonly string configFileName = $"{AppDomain.CurrentDomain.BaseDirectory}..\\..\\..\\Config\\extentReportConfig.xml";
        private static readonly string reportDir = $"{AppDomain.CurrentDomain.BaseDirectory}..\\..\\..\\Report";
       
        private static ExtentHtmlReporter htmlReporter;
        private static ExtentKlovReporter klov;
        public static AventStack.ExtentReports.ExtentReports report;
        public static ExtentTest feature;
        public static ExtentTest scenario;
        public static ExtentTest step;
      
        
        public static void SetupExtentReports()
        {
            
        InitHtmlReporter(new ExtentHtmlReporter($"{reportDir}\\index.html"));
           
            InitKlovReporter(new ExtentKlovReporter());
            InitExtentReport(new AventStack.ExtentReports.ExtentReports());
            CleanReportDir(new DirectoryInfo(reportDir));
        }

        private static void CleanReportDir(DirectoryInfo directoryInfo)
        {
            foreach (FileInfo file in directoryInfo.GetFiles()) file.Delete();
        }

        private static void InitHtmlReporter(ExtentHtmlReporter extentHtmlReporter)
        {
            htmlReporter = extentHtmlReporter;
            htmlReporter.LoadConfig(configFileName);
        }
        private static void InitKlovReporter(ExtentKlovReporter extentKlovReporter)
        {
            klov = extentKlovReporter;
            klov.InitMongoDbConnection(MongoURL, mongoPort);
            klov.InitKlovServerConnection(KlovURL);
            klov.ProjectName = ReportTitle;
            klov.ReportName = ReportName;
        }

        private static void InitExtentReport(AventStack.ExtentReports.ExtentReports extentReports)
        {
            report = extentReports;
            report.AttachReporter(htmlReporter);
            report.AttachReporter(klov);
            report.AddSystemInfo("OS", System.Environment.OSVersion.ToString());
            //report.AddSystemInfo("Browser", $"{DriverProvider.GetDriver()?.Capabilities["browserName"]} {DriverProvider.GetDriver()?.Capabilities["browserVersion"]}");
            report.AnalysisStrategy = AnalysisStrategy.BDD;
        }

        public static string CaptureScreen()
        {
            Screenshot screenshot = ((ITakesScreenshot)Hooks.Driver).GetScreenshot();
            string title = ScenarioStepContext.Current.StepInfo.Text.Replace(" ", "");
            string Runname = $"{title}_{DateTime.Now:yyyy-MM-dd-HH_mm_ss}";
            string screenshotfilename = $"{AppDomain.CurrentDomain.BaseDirectory}..\\..\\..\\Report\\{Runname}.jpg";
            screenshot.SaveAsFile(screenshotfilename);
            return screenshotfilename;
        }
    }
}
