using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;

namespace MeisterGeister.View.Arena
{
    /// <summary>
    /// Interaction logic for CreateNewArenaWindow.xaml
    /// </summary>
    partial class CreateNewArenaWindow : Window {

        private StackPanel _mainPanel;
        private TextBox _arenaWidthTextBox;
        private Label _arenaWidthLabel;
        private TextBox _arenaHeightTextBox;
        private Label _arenaHeightLabel;
        private Label _attentionLabel;
        private Button _okButton;

        private ArenaControlPanel _arenaControlPanel;

        public CreateNewArenaWindow(ArenaControlPanel arenaControlPanel) {
            _arenaControlPanel = arenaControlPanel;
            ResizeMode = System.Windows.ResizeMode.NoResize;
            Width = 240;
            Height = 220;

            createElements();
            addElements();
        }

        private void createElements() {
            _mainPanel = new StackPanel();
            _mainPanel.Height = Height;
            _mainPanel.Width = Width;

            _arenaWidthTextBox = new TextBox();
            _arenaWidthTextBox.Width = 40;
            _arenaWidthTextBox.Margin = new Thickness(0, 10, 0, 0);
            _arenaWidthTextBox.Text = "20";

            _arenaWidthLabel = new Label();
            _arenaWidthLabel.Content = "Breite";
            _arenaWidthLabel.HorizontalAlignment = HorizontalAlignment.Center;
            _arenaWidthLabel.Margin = new Thickness(0, 0, 0, 0);

            _arenaHeightTextBox = new TextBox();
            _arenaHeightTextBox.Width = 40;
            _arenaHeightTextBox.Margin = new Thickness(0, 5, 0, 0);
            _arenaHeightTextBox.Text = "20";

            _arenaHeightLabel = new Label();
            _arenaHeightLabel.Content = "Höhe";
            _arenaHeightLabel.HorizontalAlignment = HorizontalAlignment.Center;
            _arenaHeightLabel.Margin = new Thickness(0, 0, 0, 0);

            _attentionLabel = new Label();
            _attentionLabel.Foreground = new SolidColorBrush(Colors.Red);
            _attentionLabel.Content = "Achtung! Aktuelle Arena wird gelöscht!";
            _attentionLabel.HorizontalAlignment = HorizontalAlignment.Center;
            _attentionLabel.Margin = new Thickness(0, 10, 0, 0);

            _okButton = new Button();
            _okButton.Width = 100;
            _okButton.Content = "Arena erstellen";
            _okButton.Click += onOkButtonClick;

                        
        }

        private void addElements() {
            _mainPanel.Children.Add(_arenaWidthTextBox);
            _mainPanel.Children.Add(_arenaWidthLabel);
            _mainPanel.Children.Add(_arenaHeightTextBox);
            _mainPanel.Children.Add(_arenaHeightLabel);
            _mainPanel.Children.Add(_attentionLabel);
            _mainPanel.Children.Add(_okButton);

            AddChild(_mainPanel);            
        }

        private void onOkButtonClick(object sender, EventArgs e) {
            int arenaWidth = -1;
            int arenaHeight = -1;
            try {
                arenaWidth = (int)Convert.ToDouble(_arenaWidthTextBox.Text);
            } catch {
                MessageBox.Show("Breite ist keine gültige Zahl!", "Fehlerhafte Eingabe", MessageBoxButton.OK);
            }

            try {
                arenaHeight = (int)Convert.ToDouble(_arenaHeightTextBox.Text);
            }
            catch {
                MessageBox.Show("Höhe ist keine gültige Zahl!", "Fehlerhafte Eingabe", MessageBoxButton.OK);
            }

            if (arenaWidth > 0 && arenaHeight > 0) {
                Close();
                _arenaControlPanel.CreateNewArena(arenaWidth, arenaHeight);
            }
        }
    }
}
