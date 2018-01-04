using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace cswm.HotKeys
{
    public class KeyCombo : IEquatable<KeyCombo>
    {
        public KeyCombo(KeyModifiers modifiers, Keys key)
        {
            Modifiers = modifiers;
            Key = key;
        }

        public KeyModifiers Modifiers { get; }

        public System.Windows.Forms.Keys Key { get; }

        public override bool Equals(object obj)
        {
            return Equals(obj as KeyCombo);
        }

        public bool Equals(KeyCombo other)
        {
            return other != null &&
                   Modifiers == other.Modifiers &&
                   Key == other.Key;
        }

        public static KeyCombo Parse(string shortcut)
        {
            var parts = shortcut.Split('+').Select(p => p.Trim()).Reverse().ToList();
            var key = (Keys)Enum.Parse(typeof(Keys), parts.First());
            var mods = (KeyModifiers)Enum.Parse(typeof(KeyModifiers), string.Join(",", parts.Skip(1)));
            return new KeyCombo(mods, key);
        }

        public override int GetHashCode()
        {
            var hashCode = 628607405;
            hashCode = hashCode * -1521134295 + Modifiers.GetHashCode();
            hashCode = hashCode * -1521134295 + Key.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return $"{Modifiers.ToString().Replace(", ", " + ")} + {Key}";
        }

        public static bool operator ==(KeyCombo combo1, KeyCombo combo2)
        {
            return EqualityComparer<KeyCombo>.Default.Equals(combo1, combo2);
        }

        public static bool operator !=(KeyCombo combo1, KeyCombo combo2)
        {
            return !(combo1 == combo2);
        }
    }
}