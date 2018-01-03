using System;
using System.Collections.Generic;

namespace cswm.HotKeys
{
    public class HotKeyDefinition
    {
        public int Id { get; }
        public KeyCombo Keys { get; }
        public Action Action { get; }

        public HotKeyDefinition(int id, KeyCombo keys, Action action)
        {
            Id = id;
            Keys = keys;
            Action = action;
        }

        public override string ToString()
        {
            return $"{{ Id = {Id}, keys = {Keys}, action = {Action} }}";
        }

        public override bool Equals(object value)
        {
            return value is HotKeyDefinition type &&
                   EqualityComparer<int>.Default.Equals(type.Id, Id) &&
                   EqualityComparer<KeyCombo>.Default.Equals(type.Keys, Keys) &&
                   EqualityComparer<Action>.Default.Equals(type.Action, Action);
        }

        public override int GetHashCode()
        {
            int num = 0x7a2f0b42;
            num = (-1521134295 * num) + EqualityComparer<int>.Default.GetHashCode(Id);
            num = (-1521134295 * num) + EqualityComparer<KeyCombo>.Default.GetHashCode(Keys);
            return (-1521134295 * num) + EqualityComparer<Action>.Default.GetHashCode(Action);
        }
    }
}