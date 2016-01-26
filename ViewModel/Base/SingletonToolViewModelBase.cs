using MeisterGeister.Logic.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Base
{
    public class SingletonToolViewModelBase<T> : ToolViewModelBase where T : SingletonToolViewModelBase<T>, new()
    {
        #region Konstruktoren

        /// <summary>
        /// ViewModel mit Callbacks.
        /// </summary>
        protected SingletonToolViewModelBase() : base()
        {
            SetFromViewHelper();
        }
        private static readonly object padlock = new object();
        private static T instance = null;
        public static T Instance
        {
            get {
                if(instance == null)
                    lock(padlock)
                        if(instance == null)
                            instance = new T();
                return instance;
            }
        }

        #endregion
    }
}
