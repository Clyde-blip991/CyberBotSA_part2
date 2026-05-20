using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CyberBotSA_part2
{
    public partial class MainWindow : Window
    {
        private ResponseEngine responseEngine = new ResponseEngine();
        private MemoryManager memory = new MemoryManager();
        private bool nameEntered = false;
        private string userName = "";

        public MainWindow()
        {
            InitializeComponent();
            AudioPlayer.PlayGreeting();
            AddBotMessage("Welcome to CyberBot SA! 🛡");
            AddBotMessage("I'm your Cybersecurity Awareness Assistant.");
            AddBotMessage("Please enter your name to get started.");
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            ProcessInput();
        }

        private void UserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                ProcessInput();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ChatPanel.Children.Clear();
            AddBotMessage("Chat cleared. How can I help you, " + userName + "?");
        }

        private void ProcessInput()
        {
            string input = UserInput.Text.Trim();

            if (string.IsNullOrWhiteSpace(input))
                return;

            AddUserMessage(input);
            UserInput.Clear();

            if (!nameEntered)
            {
                userName = input;
                nameEntered = true;
                memory.Store("name", userName);
                UserNameLabel.Text = "👤 " + userName;
                AddBotMessage($"Hello, {userName}! Great to have you here. 😊");
                AddBotMessage("I'm here to help you stay safe online.");
                AddBotMessage("You can ask me about password safety, phishing, privacy, scams, malware, or Wi-Fi safety.");
                return;
            }

            string response = responseEngine.GetResponse(input, userName, memory);

            // Update favourite topic label
            if (memory.Has("favouriteTopic"))
            {
                FavTopicLabel.Text = "⭐ Interested in: " + memory.Retrieve("favouriteTopic");
            }

            AddBotMessage(response);
        }

        private void AddUserMessage(string message)
        {
            Border bubble = new Border
            {
                Background = new SolidColorBrush(Color.FromRgb(15, 52, 96)),
                CornerRadius = new CornerRadius(10),
                Margin = new Thickness(100, 5, 5, 5),
                Padding = new Thickness(10)
            };

            TextBlock text = new TextBlock
            {
                Text = "👤 " + message,
                Foreground = Brushes.White,
                FontSize = 13,
                TextWrapping = TextWrapping.Wrap
            };

            bubble.Child = text;
            ChatPanel.Children.Add(bubble);
            ChatScrollViewer.ScrollToBottom();
        }

        private void AddBotMessage(string message)
        {
            Border bubble = new Border
            {
                Background = new SolidColorBrush(Color.FromRgb(22, 33, 62)),
                CornerRadius = new CornerRadius(10),
                Margin = new Thickness(5, 5, 100, 5),
                Padding = new Thickness(10),
                BorderBrush = new SolidColorBrush(Color.FromRgb(0, 212, 255)),
                BorderThickness = new Thickness(1)
            };

            TextBlock text = new TextBlock
            {
                Text = "🛡 CyberBot: " + message,
                Foreground = new SolidColorBrush(Color.FromRgb(0, 212, 255)),
                FontSize = 13,
                TextWrapping = TextWrapping.Wrap
            };

            bubble.Child = text;
            ChatPanel.Children.Add(bubble);
            ChatScrollViewer.ScrollToBottom();
        }
    }
}