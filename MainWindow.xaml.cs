using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace TextGen
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

        private void GenerateTextButton_Click(object sender, RoutedEventArgs e)
        {
            string inputText = textInput.Text;
            string variablesInput = variableInput.Text;
            string variables2Input = variable2Input.Text;

            if (!string.IsNullOrEmpty(inputText) && !string.IsNullOrEmpty(variablesInput))
            {
                // Split variables
                string[] variables = variablesInput.Split(',');

                string result = string.Empty;

                if (!string.IsNullOrEmpty(variables2Input))
                {
                    string[] variables2 = variables2Input.Split(',');

                    string aux = string.Empty;

                    // create Tuple of both variables that replace
                    foreach (var variable in variables.Zip(variables2, Tuple.Create))
                    {
                        aux = inputText.Replace("{variable}", variable.Item1);
                        result += aux.Replace("{variable2}", variable.Item2) + "\n";
                    }
                }
                else
                {
                    foreach (var variable in variables)
                    {
                        result += inputText.Replace("{variable}", variable) + "\n";
                    }
                }

                // Replace variable in the text and display the result
                resultText.Text = result;
            }
            else
            {
                resultText.Text = "Please enter text and variable.";
            }
        }


        // informs user that his text was copied to clipboard
        private void CopyTextButton_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(resultText.Text);

            textCopy.Visibility = Visibility.Visible;

            // after 2 seconds, hide text
            Task.Delay(1000).ContinueWith(_ =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    textCopy.Visibility = Visibility.Collapsed;
                });
            });
        }


        private void AddVariableCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            // cleans variable input
            variable2Input.Text = string.Empty;
        }

        private void AddVariableCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
