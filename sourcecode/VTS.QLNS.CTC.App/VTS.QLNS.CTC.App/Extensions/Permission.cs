using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Windows;
using VTS.QLNS.CTC.App.Service;

namespace VTS.QLNS.CTC.App.Extensions
{
    public class Permission
    {
        #region Identifier
        public static readonly DependencyProperty IdentifierProperty = DependencyProperty.RegisterAttached(
            "Identifier",
            typeof(string),
            typeof(Permission),
            new PropertyMetadata(Identifier_Callback));

        private static void Identifier_Callback(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            var uiElement = (UIElement)source;
            string identifier = GetIdentifier(uiElement);
            bool hasPermission = CheckPermission(identifier);
            RecalculateControlIsEnabled(uiElement, hasPermission);
        }

        public static void SetIdentifier(UIElement element, string value)
        {
            element.SetValue(IdentifierProperty, value);
        }

        public static string GetIdentifier(UIElement element)
        {
            return (string)element.GetValue(IdentifierProperty);
        }
        #endregion

        #region HasAuthority
        public static readonly DependencyProperty HasAuthorityProperty = DependencyProperty.RegisterAttached(
            "HasAuthority",
            typeof(string),
            typeof(Permission),
            new PropertyMetadata(HasAuthority_Callback));

        private static void HasAuthority_Callback(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            // Get sessionService from depedency injection store
            IServiceProvider serviceProvider = ((App)Application.Current).ServiceProvider;
            ISessionService sessionService = serviceProvider.GetRequiredService<ISessionService>();
            // Defaul false
            bool hasPermission = false;
            var uiElement = (UIElement)source;
            if (sessionService.Current != null)
            {
                // Get authorities from authority store
                string[] authoritiesStore = sessionService.Current.Authorities.ToArray();
                // Set visibility with has permisson
                var authorities = GetHasAuthority(uiElement).Split(',');
                foreach (var authority in authorities)
                {
                    hasPermission = authoritiesStore.Contains(authority.Trim());
                    if (hasPermission)
                        break;
                }
            }
            RecalculateControlVisibility(uiElement, hasPermission);
        }

        public static void SetHasAuthority(UIElement element, string value)
        {
            element.SetValue(HasAuthorityProperty, value);
        }

        public static string GetHasAuthority(UIElement element)
        {
            return (string)element.GetValue(HasAuthorityProperty);
        }
        #endregion

        #region Method
        private static void RecalculateControlVisibility(UIElement control, bool hasPermission)
        {
            if (hasPermission)
                control.Visibility = Visibility.Visible;
            else
                control.Visibility = Visibility.Collapsed;
        }

        private static void RecalculateControlIsEnabled(UIElement control, bool hasPermission)
        {
            control.IsEnabled = hasPermission;
        }

        public static bool CheckPermission(string funcCode)
        {
            try
            {
                // Get sessionService from depedency injection store
                IServiceProvider serviceProvider = ((App)Application.Current).ServiceProvider;
                ISessionService sessionService = serviceProvider.GetRequiredService<ISessionService>();
                if (sessionService != null && sessionService.Current != null)
                {
                    if (string.IsNullOrEmpty(funcCode))
                        return true;

                    var funcAuthorities = sessionService.Current.FuncAuthorities;
                    var authorities = sessionService.Current.Authorities;
                    if (funcAuthorities != null && funcAuthorities.Count > 0
                        && funcAuthorities.ContainsKey(funcCode))
                    {
                        // TODO: Check permission
                        var funcAuthoritiesByCode = funcAuthorities[funcCode];
                        return funcAuthoritiesByCode.Intersect(authorities).Any();
                    }
                    return false;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }
        #endregion
    }
}
