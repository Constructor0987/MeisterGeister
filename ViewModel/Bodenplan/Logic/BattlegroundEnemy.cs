using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Linq;
using System.Text;
using MeisterGeister.Model;

namespace MeisterGeister.ViewModel.Bodenplan.Logic
{
    class BattlegroundEnemy:BattlegroundCreature
    {
        private Dictionary<string, Image> _gegnerNameHasPortrait;

        public BattlegroundEnemy(Gegner enemy)
        {
            Enemy = enemy;
            EnemyPictureUrl = LoadEnemyPortrait(enemy.Bild);
        }

        private string _enemyPictureUrl;
        public string EnemyPictureUrl
        {
            get { return _enemyPictureUrl; }
            set
            {
                _enemyPictureUrl = value;
                OnChanged("EnemyPictureUrl");
            }
        }

        private double _enemyWidth = 100;
        public double EnemyWidth
        {
            get { return _enemyWidth; }
            set
            {
                _enemyWidth = value;
                OnChanged("EnemyWidth");
            }
        }

        private double _enemyHeight = 100;
        public double EnemyHeight
        {
            get { return _enemyHeight; }
            set
            {
                _enemyHeight = value;
                OnChanged("EnemyHeight");
            }
        }
        

        private Gegner _enemy;
        public Gegner Enemy
        {
            get { return _enemy; }
            set
            {
                _enemy = value;
                OnChanged("Enemy");
            }
        }

        public Image _enemyImage;
        public Image EnemyImage
        {
            get { return _enemyImage; }
            set
            {
                _enemyImage = value;
                OnChanged("EnemyImage");
            }
        } 

        private String LoadEnemyPortrait(string portraitFilename)
        {
            try
            {
                return @portraitFilename.Replace("/DSA MeisterGeister;component", string.Empty);
            }
            catch
            {
                return ICON_DIR + "fragezeichen.png";
            }
        }

        //private Image LoadEnemyPortrait(string portraitFilename)
        //{
        //    try
        //    {
        //        return LoadImage(new Uri(@portraitFilename.Replace("/DSA MeisterGeister;component", string.Empty), UriKind.RelativeOrAbsolute));
        //    }
        //    catch
        //    {
        //        return LoadImage(new Uri(ICON_DIR + "fragezeichen.png", UriKind.Relative));
        //    }
        //}

    }
}
