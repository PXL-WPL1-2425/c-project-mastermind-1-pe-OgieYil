using System.Reflection.Emit;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Mastermind_PE_Oguzhan_Yilmaz_1PROA
{
  
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string[] generatedCode; // De willekeurig gegenereerde code

        DispatcherTimer timer;
        DateTime clicked;
        TimeSpan elapsedTime;

        public MainWindow()
        {
            InitializeComponent();
            GenerateRandomCode();
            OpvullenComboBoxes();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;

        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
  
            elapsedTime = DateTime.Now - clicked;
            TimerTextBox.Text = $"{elapsedTime.Seconds}:{elapsedTime.Milliseconds.ToString().PadLeft(3, '0')}";

        }

        private void GenerateRandomCode()
        {
            Random random = new Random();
            string[] Colors = { "Rood", "Geel", "Oranje", "Wit", "Groen", "Blauw" };
            generatedCode = Enumerable.Range(0, 4).Select(_ => Colors[random.Next(Colors.Length)]).ToArray();
            this.Title = $"MasterMind ({string.Join(",", generatedCode)})"; // Toon de code in de titel voor debugging
            
            
        }

        
        private void OpvullenComboBoxes()
        {
            string[] Colors = { "Rood", "Geel", "Oranje", "Wit", "Groen", "Blauw" };



            ComboBox1.ItemsSource = Colors;
            ComboBox2.ItemsSource = Colors;
            ComboBox3.ItemsSource = Colors;
            ComboBox4.ItemsSource = Colors;
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Label1.Background = GetBrushFromColorName(ComboBox1.SelectedItem as string ?? "default");
            Label2.Background = GetBrushFromColorName(ComboBox2.SelectedItem as string ?? "default");
            Label3.Background = GetBrushFromColorName(ComboBox3.SelectedItem as string ?? "default");
            Label4.Background = GetBrushFromColorName(ComboBox4.SelectedItem as string ?? "default");

        }

        // Helper method to convert color names to SolidColorBrush
        private SolidColorBrush GetBrushFromColorName(string colorName)
        {
            switch (colorName)
            {
                case "Rood": return Brushes.Red;
                case "Geel": return Brushes.Yellow;
                case "Oranje": return Brushes.Orange;
                case "Wit": return Brushes.White;
                case "Groen": return Brushes.Green;
                case "Blauw": return Brushes.Blue;
                default: return Brushes.Transparent; // Default fallback
            }
        }

       
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            //mijn bedoeling met dit is het te laten checken of de code well juist is.
                string[] userCode = {
                    ComboBox1.SelectedItem != null ? ComboBox1.SelectedItem.ToString() : "default",
                    ComboBox2.SelectedItem != null ? ComboBox2.SelectedItem.ToString() : "default",
                    ComboBox3.SelectedItem != null ? ComboBox3.SelectedItem.ToString() : "default",
                    ComboBox4.SelectedItem != null ? ComboBox4.SelectedItem.ToString() : "default"
        };

            // Controleer elke invoer en stel de randkleur in
            CheckColor(Label1, userCode[0], 0);
            CheckColor(Label2, userCode[1], 1);
            CheckColor(Label3, userCode[2], 2);
            CheckColor(Label4, userCode[3], 3);

            //timer voor het oefening
            double timeDelta = Math.Abs(elapsedTime.TotalSeconds - 10);

            if (timeDelta > 0.2)
            {


                TimerTextBox.Background = Brushes.Red;
                timer.Stop();

            }
            else
            {

                TimerTextBox.Background = Brushes.Wheat;


            }

        }

   
    private void CheckColor(System.Windows.Controls.Label label, string selectedColor, int position)
        {
            if (selectedColor == generatedCode[position])
            {
                label.BorderBrush = new SolidColorBrush(Colors.DarkRed);
                label.BorderThickness = new Thickness(3);
            }
            else if (generatedCode.Contains(selectedColor))
            {
                label.BorderBrush = new SolidColorBrush(Colors.Wheat);
                label.BorderThickness = new Thickness(3);
            }
            else
            {
                label.BorderBrush = Brushes.Transparent;
                label.BorderThickness = new Thickness(0);
            }
        }

    }

}
