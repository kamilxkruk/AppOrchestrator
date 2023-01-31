using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.IO;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Execution;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using System.Diagnostics;

namespace AppOrchestrator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private Process apiProcess = null;

        private void Api_ToggleClick(object sender, RoutedEventArgs e)
        {
            const string apiProjectName = "WebAPI";

            var orchestratorDir = AppContext.BaseDirectory;
            var pathLength = orchestratorDir.Count(c => c == '\\')-4;
            var lastBackslashPosition = orchestratorDir.TakeWhile(c => (pathLength -= (c == '\\' ? 1 : 0)) > 0).Count();
            var rootPath = orchestratorDir.Substring(0, lastBackslashPosition);
            
            var apiDir = Path.Combine(rootPath, apiProjectName);
            
            if(apiProcess == null )
            {
                apiProcess = new Process
                {
                    StartInfo =
                    {
                        FileName = "dotnet",
                        Arguments = $"run --launch-profile https {apiProjectName}.csproj",
                        UseShellExecute = false,
                        WorkingDirectory = apiDir,
                        CreateNoWindow = true
                    }
                };
                apiProcess.Start();
            }
            else
            {
                apiProcess.Kill();
                apiProcess.WaitForExit();
                apiProcess.Dispose();
            }
        }
        private void Wpf_ToggleClick(object sender, RoutedEventArgs e)
        {

        }
        private void WebUI_ToggleClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
