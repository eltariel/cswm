using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using NLog;

namespace cswm.HotKeys
{
    public class HotKeyManager : IDisposable, IMessageFilter
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        private readonly IntPtr hwnd;
        private int nextHotKeyId = 0;

        private readonly Dictionary<int, HotKeyDefinition> hotKeys = new Dictionary<int, HotKeyDefinition>();

        public HotKeyManager(IntPtr hwnd)
        {
            this.hwnd = hwnd;

            log.Trace("Init");
        }

        public bool Register(KeyCombo keys, Action action)
        {
            lock (hotKeys)
            {
                var def = new HotKeyDefinition(GetNextHotKeyId(), keys, action);

                log.Info($"Registering new key combo {def.Keys} with Id {def.Id}");

                if (hotKeys.Values.Any(d => d.Keys == def.Keys))
                {
                    log.Warn($"Key combo already registered, attempting to unregister.");
                    if (!Unregister(def.Keys))
                    {
                        return false;
                    }
                }

                var registered = Win32.RegisterHotKey(hwnd, def.Id, (int)keys.Modifiers, (int)keys.Key);
                if (registered)
                {
                    hotKeys[def.Id] = def;
                }
                else
                {
                    log.Warn($"Failed to register key combo {def.Keys}");
                }
            
                return registered;
            }
        }

        public bool Unregister(KeyCombo keys)
        {
            lock (hotKeys)
            {
                var def = hotKeys.Values.FirstOrDefault(d => d.Keys == keys);
                if (def == null)
                {
                    return false;
                }

                return Unregister(def.Id);
            }
        }

        private bool Unregister(int id)
        {
            if (!Win32.UnregisterHotKey(hwnd, id))
            {
                return false;
            }

            hotKeys.Remove(id);
            return true;
        }

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == Win32.WM_HOTKEY)
            {
                var id = m.WParam.ToInt32();

                // ReSharper disable once InconsistentlySynchronizedField
                if (hotKeys.TryGetValue(id, out var def))
                {
                    log.Trace($"{def}");
                    def.Action();
                    return true;
                }
            }

            return false;
        }

        private int GetNextHotKeyId()
        {
            return nextHotKeyId++; // NOTE: this should not go over 0xBFFF 
        }

        #region IDisposable

        private void ReleaseUnmanagedResources()
        {
            lock (hotKeys)
            {
                foreach (var id in hotKeys.Keys.ToList())
                {
                    Unregister(id);
                }
            }
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        ~HotKeyManager()
        {
            ReleaseUnmanagedResources();
        }

        #endregion

        private class Win32
        {
            [DllImport("user32.dll")]
            public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

            [DllImport("user32.dll")]
            public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

            public const int WM_HOTKEY = 0x0312;
        }
    }
}