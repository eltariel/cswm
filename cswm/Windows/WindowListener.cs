using System;
using System.Windows.Automation;
using NLog;

namespace cswm.Windows
{
    public class WindowListener
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        public void RegisterAutomationEvents()
        {
            var re = AutomationElement.RootElement;
            foreach (AutomationElement c in re.FindAll(TreeScope.Children, Condition.TrueCondition))
            {
                Console.WriteLine(c.Current.Name);
            }

            Automation.AddAutomationEventHandler(WindowPattern.WindowOpenedEvent, re, TreeScope.Subtree, WindowOpenedEventHandler);
            Automation.AddAutomationFocusChangedEventHandler(FocusChangedEventHandler);
        }

        public void FocusChangedEventHandler(object sender, AutomationFocusChangedEventArgs args)
        {
            try
            {
                var elem = (AutomationElement)sender;
                log.Trace($"focus: {elem.Current.Name}");
            }
            catch (Exception ex)
            {
                log.Debug(ex);
            }
        }

        private void WindowOpenedEventHandler(object sender, AutomationEventArgs automationEventArgs)
        {
            try
            {
                var elem = (AutomationElement)sender;
                if (elem.Current.ClassName == "Notepad")
                {
                    WindowManipulation.RemoveBorder(new IntPtr(elem.Current.NativeWindowHandle));

                    var p = elem.GetCurrentPattern(WindowPattern.Pattern) as WindowPattern;
                    p?.SetWindowVisualState(WindowVisualState.Maximized);

                    foreach (AutomationElement c in elem.FindAll(TreeScope.Subtree, Condition.TrueCondition))
                    {
                        log.Debug($"{c.Current.AutomationId} {c.Current.ClassName} {c.Current.Name}");
                    }
                }

                log.Debug($"Window opened: {elem.Current.AutomationId} {elem.Current.ClassName} {elem.Current.Name}");
            }
            catch (Exception ex)
            {
                log.Debug(ex);
            }
        }

    }
}