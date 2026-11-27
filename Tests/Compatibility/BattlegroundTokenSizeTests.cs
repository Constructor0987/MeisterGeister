#if NET10_COMPAT_TESTS
using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using MeisterGeister.Model;
using Xunit;

namespace MeisterGeister.Compatibility.Tests
{
    public class BattlegroundTokenSizeTests
    {
        [Theory]
        [InlineData(2, 3, 1.0, 1.0, 160, 240, 160, 240)]
        [InlineData(3, 2, 0.5, 1.5, 120, 80, 180, 120)]
        [InlineData(null, null, 1.0, 1.0, 80, 80, 80, 80)]
        [InlineData(0, -1, 1.0, 1.0, 80, 80, 80, 80)]
        public void EnemySizeUsesTemplateDimensionsAndExistingScale(int? sizeX, int? sizeY,
            double objectSize, double oversize, double width, double height, double pictureWidth, double pictureHeight)
        {
            RunInSta(() =>
            {
                var template = new GegnerBase { TokenSizeX = sizeX, TokenSizeY = sizeY };
                var enemy = new Gegner(template) { TokenOversize = oversize, ObjectSize = objectSize };

                Assert.Equal(width, enemy.CreatureWidth);
                Assert.Equal(height, enemy.CreatureHeight);
                Assert.Equal(pictureWidth, enemy.CreatureWidthPic);
                Assert.Equal(pictureHeight, enemy.CreatureHeightPic);
                Assert.Equal(enemy.CreatureX - (pictureWidth - width) / 2, enemy.CreatureXPic);
                Assert.Equal(enemy.CreatureY - (pictureHeight - height) / 2, enemy.CreatureYPic);
                Assert.Equal(height, enemy.MarginCreatureATPAaktionen.Top);
            });
        }

        [Fact]
        public void ChangingTemplateResizesAllObservingEnemies()
        {
            RunInSta(() =>
            {
                var template = new GegnerBase { TokenSizeX = 1, TokenSizeY = 1 };
                var first = new Gegner(template);
                var second = new Gegner(template) { ObjectSize = 2 };
                first.ObserveTokenSize();
                first.ObserveTokenSize();
                second.ObserveTokenSize();
                int positionNotifications = 0;
                first.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(first.CreatureXPic))
                        positionNotifications++;
                };

                template.TokenSizeX = 3;
                template.TokenSizeY = 2;

                Assert.Equal(240, first.CreatureWidth);
                Assert.Equal(160, first.CreatureHeight);
                Assert.Equal(480, second.CreatureWidth);
                Assert.Equal(320, second.CreatureHeight);
                Assert.Equal(2, positionNotifications);
                template.Name = "Unverwandte Änderung";
                Assert.Equal(2, positionNotifications);
            });
        }

        [Fact]
        public void HeroKeepsExistingScalingBehavior()
        {
            RunInSta(() =>
            {
                var hero = new Held { TokenOversize = 1.5, ObjectSize = 2 };

                Assert.Equal(160, hero.CreatureWidth);
                Assert.Equal(160, hero.CreatureHeight);
                Assert.Equal(240, hero.CreatureWidthPic);
                Assert.Equal(240, hero.CreatureHeightPic);
            });
        }

        private static void RunInSta(Action action)
        {
            Exception failure = null;
            var thread = new Thread(() =>
            {
                try { action(); }
                catch (Exception ex) { failure = ex; }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
            if (failure != null)
                ExceptionDispatchInfo.Capture(failure).Throw();
        }
    }
}
#endif
