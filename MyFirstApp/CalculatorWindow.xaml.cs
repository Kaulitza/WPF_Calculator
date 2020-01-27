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
using System.Windows.Shapes;

namespace MyFirstApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class CalculatorWindow : Window
    {
        public CalculatorWindow()
        {
            InitializeComponent();
        }

        private void ButtonOperator_Click(object sender, RoutedEventArgs e)
        {
            Button OperationButton = sender as Button;
            string Operation = OperationButton.Name;

            if (double.TryParse(Operand2.Text, out double Operand2Num) && double.TryParse(Operand1.Text, out double Operand1Num))
            {
                switch (Operation)
                {
                    case "ButtonAdd":
                        Answer.Text = Convert.ToString(Operand1Num + Operand2Num);
                        break;
                    case "ButtonSubtract":
                        Answer.Text = Convert.ToString(Operand1Num - Operand2Num);
                        break;
                    case "ButtonDivide":
                        if (Operand2Num == 0)
                        {
                            DivideByZeroError DivByZeroErrorWin = new DivideByZeroError();
                            DivByZeroErrorWin.Show();
                            Operand2.Focus();
                        }
                        else
                        {
                            Answer.Text = Convert.ToString(Operand1Num / Operand2Num);
                        }
                        break;
                    case "ButtonMultiply":
                        Answer.Text = Convert.ToString(Operand1Num * Operand2Num);
                        break;
                    default:
                        Answer.Text = "???";
                        break;
                }
            }
            else
            {
                ValidationError ValidationErrorWin = new ValidationError();
                ValidationErrorWin.Show();
            }
        }

        private void ButtonHelp_Click(object sender, RoutedEventArgs e)
        {
            HelpWindow helpWindow = new HelpWindow();
            helpWindow.Show();
        }

        private void Operand_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!double.TryParse(Operand1.Text, out double Number))
            {
                TextBlock_UserFeedback.Text = "Operands must be numbers";
            }
            else
            {
                TextBlock_UserFeedback.Text = "";
            }
        }

        private void Button_ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_ClearButton_Click(object sender, RoutedEventArgs e)
        {
            Operand1.Clear();
            Operand2.Clear();
            Answer.Clear();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton ButtonClicked = sender as RadioButton;
            string Button = ButtonClicked.Name;

            if (double.TryParse(DegreesRadiansBox.Text, out double DegreesRadiansNumber))
            {
                switch (Button)
                {
                    case "DegreesButton":
                        RadiansButton.IsChecked = false;
                        if (ComboBoxRoundOrExact.SelectedItem.ToString() == "System.Windows.Controls.ComboBoxItem: Rounded to Decimal")
                        {
                            DegreesRadiansBox.Text = Convert.ToString(DegreesRadiansNumber * 180 / Math.PI);
                            TextBlock_PiDisplay.Text = "";
                        }
                        else if (ComboBoxRoundOrExact.SelectedItem.ToString() == "System.Windows.Controls.ComboBoxItem: Exact, using Pi")
                        {
                            DegreesRadiansBox.Text = Convert.ToString(DegreesRadiansNumber * 180);
                            TextBlock_PiDisplay.Text = "";
                        }
                        else
                        {
                            DegreesRadiansBox.Text = ComboBoxRoundOrExact.SelectedItem.ToString();
                        }
                        break;
                    case "RadiansButton":
                        DegreesButton.IsChecked = false;
                        if (ComboBoxRoundOrExact.SelectedItem.ToString() == "System.Windows.Controls.ComboBoxItem: Rounded to Decimal")
                        {
                            DegreesRadiansBox.Text = Convert.ToString(DegreesRadiansNumber * Math.PI / 180);
                            TextBlock_PiDisplay.Text = "";
                        }
                        else if (ComboBoxRoundOrExact.SelectedItem.ToString() == "System.Windows.Controls.ComboBoxItem: Exact, using Pi")
                        {
                            DegreesRadiansBox.Text = Convert.ToString(DegreesRadiansNumber / 180);
                            TextBlock_PiDisplay.Text = "𝜋";
                        }
                        else
                        {
                            DegreesRadiansBox.Text = ComboBoxRoundOrExact.SelectedItem.ToString();
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void ComboBoxRoundOrExact_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((!(DegreesRadiansBox == null)) && (double.TryParse(DegreesRadiansBox.Text, out double DegreesRadiansNumber)))
            {
                if (RadiansButton.IsChecked == true)
                {
                    if (ComboBoxRoundOrExact.SelectedItem.ToString() == "System.Windows.Controls.ComboBoxItem: Rounded to Decimal")
                    {
                        DegreesRadiansBox.Text = Convert.ToString(DegreesRadiansNumber * Math.PI);
                        TextBlock_PiDisplay.Text = "";
                    }
                    else if (ComboBoxRoundOrExact.SelectedItem.ToString() == "System.Windows.Controls.ComboBoxItem: Exact, using Pi")
                    {
                        DegreesRadiansBox.Text = Convert.ToString(DegreesRadiansNumber / Math.PI);
                        TextBlock_PiDisplay.Text = "𝜋";
                    }
                    else
                    {
                        DegreesRadiansBox.Text = ComboBoxRoundOrExact.SelectedItem.ToString();
                    }
                }
            }
            else
            {
                if ((!(DegreesRadiansBox == null)))
                {
                    ValidationError ValidationErrorWin = new ValidationError();
                    ValidationErrorWin.Show();
                }
            }
        }
    }
}
