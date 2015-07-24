using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DgSuche
{
    public class NotifyChangedBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Checks if a property already matches a desired value.  Sets the property and
        /// notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="storage">Reference to a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        /// <param name="propertyName">Name of the property used to notify listeners.  This
        /// value is optional and can be provided automatically when invoked from compilers that
        /// support CallerMemberName.</param>
        /// <returns>True if the value was changed, false if the existing value matched the
        /// desired value.</returns>
        protected bool Set<T>(ref T storage, T value, [CallerMemberName] String propertyName = null)
        {
            if (object.Equals(storage, value)) return false;

            storage = value;
            this.OnChanged(propertyName);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Notifies listeners that a property value has changed.
        /// </summary>
        /// <param name="propertyName">Name of the property used to notify listeners.  This
        /// value is optional and can be provided automatically when invoked from compilers
        /// that support <see cref="CallerMemberNameAttribute"/>.</param>
        protected void OnChanged([CallerMemberName] string propertyName = null)
        {
            this.VerifyPropertyName(propertyName);
            var eventHandler = this.PropertyChanged;
            if (eventHandler != null)
            {
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Gibt zurück, ob eine Exception gewurfen werden soll, oder Debug.Fail() 
        /// genutz wird, falls ein nicht vorhandener Property-Name als String an
        /// OnChanged bzw. VerifyPropertyName übergeben wird.
        /// Standartrückgabewert ist false. Unterklassen können für Unit-Tests diesen
        /// Getter überschreiben, um true zurückzugeben.
        /// <returns>true, falls eine Exeption geworfen werden soll.</returns>
        /// </summary>
        protected virtual bool ThrowOnInvalidPropertyName { get; private set; }

        /// <summary>
        /// Verifiziert als String übergebene Properties
        /// </summary>
        /// <param name="propertyString">String/Name des zu verifizierenden Property</param>
        [Conditional("DEBUG_PROPERTIES")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyString)
        {
            if (TypeDescriptor.GetProperties(this)[propertyString] == null)
            {
                string msg = "Invalid property name: " + propertyString;
                if (this.ThrowOnInvalidPropertyName)
                    throw new Exception(msg);
                else
                    Debug.Fail(msg);
            }
        }
    }
}
