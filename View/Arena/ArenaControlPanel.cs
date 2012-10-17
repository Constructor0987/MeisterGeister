using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Diagnostics;
using System.Data;
using System.Windows.Media.Imaging;
// Eigene Usings
using MeisterGeister.Logic.General;
using VM = MeisterGeister.ViewModel;
using MeisterGeister.ViewModel.Kampf.Logic;

namespace MeisterGeister.View.Arena
{
    public class ArenaControlPanel : StackPanel {

        private static int MARGIN = 5;
        private static double MOUSE_WHEEL_ZOOM_PERCENTAGE = 0.05;

        private ArenaWindow _arenaWindow;
        private ArenaViewer _arenaViewer;

        private Slider _zoomSlider;
        private ComboBox _heroAdder;
        private ComboBox _enemyAdder;
        private Button _newArenaButton;

        public ArenaControlPanel(double width, double height, ArenaViewer arenaViewer, ArenaWindow arenaWindow) {
            Width = width;
            Height = height;
            _arenaViewer = arenaViewer;
            _arenaViewer.CreatureRemoved += OnCreatureRemoved;

            _arenaWindow = arenaWindow;

            Margin = new Thickness(MARGIN);

            HorizontalAlignment = System.Windows.HorizontalAlignment.Center;

            createElements();
            addElements();
        }

        private void createElements() {
            _zoomSlider = new Slider();
            _zoomSlider.Minimum = (double)ArenaViewer.MIN_PIXELS_PER_METER / (double)_arenaViewer.PixelsPerMeter;
            _zoomSlider.Maximum = (double)ArenaViewer.MAX_PIXELS_PER_METER / (double)_arenaViewer.PixelsPerMeter;
            _zoomSlider.Height = 25;
            _zoomSlider.Value = 1;
            _zoomSlider.Width = Width - 2 * MARGIN;
            _zoomSlider.Margin = new Thickness(0, 20, 0, 0);
            _zoomSlider.ValueChanged += onZoomSliderChanged;

            _heroAdder = new ComboBox();
            _heroAdder.Height = 25;
            _heroAdder.Width = Width - 2 * MARGIN;
            _heroAdder.FontSize = 12;
            _heroAdder.VerticalContentAlignment = VerticalAlignment.Center;
            _heroAdder.HorizontalContentAlignment = HorizontalAlignment.Center;
            _heroAdder.SelectionChanged += onHeroSelectionChanged;
            _heroAdder.Margin = new Thickness(0, 20, 0, 0);

            addHeroes();

            _enemyAdder = new ComboBox();
            _enemyAdder.Height = 25;
            _enemyAdder.Width = Width - 2 * MARGIN;
            _enemyAdder.FontSize = 12;
            _enemyAdder.VerticalContentAlignment = VerticalAlignment.Center;
            _enemyAdder.HorizontalContentAlignment = HorizontalAlignment.Center;
            _enemyAdder.SelectionChanged += onEnemySelectionChanged;
            _enemyAdder.Margin = new Thickness(0, 20, 0, 0);

            addEnemies();

            _newArenaButton = new Button();
            _newArenaButton.Height = 25;
            _newArenaButton.Width = Width - 2 * MARGIN;
            _newArenaButton.FontSize = 12;
            _newArenaButton.Margin = new Thickness(0, 40, 0, 0);
            _newArenaButton.Content = "Neue Arena";
            _newArenaButton.Click += onNewArenaClick;
            
        }

        private void addEnemies() {
            _enemyAdder.Items.Add("Gegner hinzufügen");
            _enemyAdder.SelectedIndex = 0;

            foreach (var item in Global.ContextHeld.Liste<Model.GegnerBase>().OrderBy(h => h.Name).ToList())
            {
                _enemyAdder.Items.Add(new CreatureNameIdPair(item.Name, item.GegnerBaseGUID));
                _arenaViewer.AddGegnerPortrait(item.Name, item.Bild);
            } 

        }



        private void onNewArenaClick(object sender, EventArgs e) {
            Debug.WriteLine("click!");

            CreateNewArenaWindow cnaw = new CreateNewArenaWindow(this);
            cnaw.Owner = _arenaWindow;
            cnaw.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            cnaw.Show();
            
        }

        private void addHeroes() {
            
            _heroAdder.Items.Add("Held hinzufügen");
            _heroAdder.SelectedIndex = 0;

            foreach (var item in Global.ContextHeld.HeldenGruppeListe)
	        {
                _heroAdder.Items.Add(new CreatureNameIdPair(item.Name, item.HeldGUID));
                _arenaViewer.AddHeldPortrait(item.HeldGUID, item.BildLink);
            }            
        }
        

        

        private void addElements() {
            Children.Add(_zoomSlider);
            Children.Add(_heroAdder);
            Children.Add(_enemyAdder);
            Children.Add(_newArenaButton);
        }

        private void onZoomSliderChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {

            Point center = _arenaViewer.CurrentlyDisplayedCenter;

            _arenaViewer.ZoomFactor = ((Slider)sender).Value;
            _arenaViewer.DrawArena();


            _arenaViewer.CurrentlyDisplayedCenter = center;
            

        }

        private void onHeroSelectionChanged(object sender, EventArgs e) {
            if (_heroAdder.SelectedIndex != 0) {

                CreatureNameIdPair pair = (CreatureNameIdPair)_heroAdder.SelectedItem;

                if (pair != null && !_arenaViewer.Arena.ContainsHeldWidthId(pair.Id)) {
                    Model.Held held = Global.ContextHeld.LoadHeldByGUID(pair.Id);
                    _arenaViewer.Arena.AddHeld(held, new Point(_arenaViewer.Arena.Width / 2, _arenaViewer.Arena.Height / 2));
                    _arenaViewer.DrawArena();
                    _heroAdder.SelectedIndex = 0;
                    _heroAdder.Items.Remove(pair);
                }
            }
        }

        private void onEnemySelectionChanged(object sender, EventArgs e) {
            if (_enemyAdder.SelectedIndex != 0) {

                CreatureNameIdPair pair = (CreatureNameIdPair)_enemyAdder.SelectedItem;

                if (pair != null) {
                    Model.Gegner gegner = new Model.Gegner() { Name = pair.Name };
                    _arenaViewer.Arena.AddGegner(gegner, new Point(_arenaViewer.Arena.Width / 2, _arenaViewer.Arena.Height / 2));
                    _arenaViewer.DrawArena();
                    _enemyAdder.SelectedIndex = 0;
                }
            }
        }

        public void CreateNewArena(int width, int height) {
            VM.Arena.Arena arena = new VM.Arena.Arena(width, height);
            _arenaViewer.Arena = arena;
            _arenaViewer.DrawArena();

            _heroAdder.Items.Clear();
            addHeroes();
        }

        internal void OnMouseWheelZoom(int delta) {
            double sliderValue = _zoomSlider.Value;

            if (delta > 0)
                sliderValue = sliderValue + _zoomSlider.Maximum * MOUSE_WHEEL_ZOOM_PERCENTAGE;
            else
                sliderValue = sliderValue - _zoomSlider.Maximum * MOUSE_WHEEL_ZOOM_PERCENTAGE;

            _zoomSlider.Value = sliderValue;
        }

        public ArenaWindow ArenaMainWindow {
            get { return _arenaWindow; }
        }

        private void OnCreatureRemoved(object sender, ArenaViewer.CreatureEventArgs args) {

            if (args.CC.Creature is Model.Held) {
                Model.Held held = (Model.Held)args.CC.Creature;
                _heroAdder.Items.Add(new CreatureNameIdPair(held.Name, held.HeldGUID));              
            }
        }

        public void OnArenaPopulated(MeisterGeister.ViewModel.Kampf.Logic.Kampf kampf)
        {

            List<Guid> heldIds = new List<Guid>();
            
            foreach (KämpferInfo kämpferInfo in kampf.Kämpfer) {
                if (kämpferInfo.Kämpfer is Model.Held)
                {
                    heldIds.Add(((Model.Held)kämpferInfo.Kämpfer).HeldGUID);        
                }
            }

            List<CreatureNameIdPair> pairsToRemove = new List<CreatureNameIdPair>();

            foreach (Object o in _heroAdder.Items) {
                if (o is CreatureNameIdPair && heldIds.Contains(((CreatureNameIdPair)o).Id)){
                    pairsToRemove.Add((CreatureNameIdPair)o);
                }
            }

            foreach (CreatureNameIdPair pair in pairsToRemove) {
                _heroAdder.Items.Remove(pair);
            }
        }


        class CreatureNameIdPair {
            private String _name;
            private Guid _id;

            public CreatureNameIdPair(String heldName, Guid heldId) {
                _name = heldName;
                _id = heldId;
            }

            public String Name {
                get { return _name; }
            }

            public Guid Id {
                get { return _id; }
            }

            public override string ToString() {
                return _name;
            }
        }

       
    }
}
