using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Kairos.UI.Styles.Controls.Textfield.AttachedPropertys
{
    public class TextBoxValidator
    {
        public static string GetRegexValidator(DependencyObject obj)
        {
            return (string)obj.GetValue(ValidationRegExProp);
        }

        public static void SetRegexValidator(DependencyObject obj, string value)
        {
            obj.SetValue(ValidationRegExProp, value);
        }

        // Using a DependencyProperty as the backing store for RegexValidator
        public static readonly DependencyProperty ValidationRegExProp =
            DependencyProperty.RegisterAttached("RegexValidator", typeof(string), typeof(TextBoxValidator), new PropertyMetadata(null, (obj, args) => 
            {
                if (obj is TextBox textbox)
                {
                    if (args?.NewValue != null)
                    {
                        if(args.OldValue == null)
                        {
                            textbox.TextChanged += Textbox_TextChanged;
                        }
                    }
                    else
                    {
                        var ecn = TextBoxValidator.GetErrorControlName(textbox);

                        if (ecn != null)
                        {
                            var uiElementForErrorDisplay = (UIElement)textbox.FindName(ecn);
                            if (uiElementForErrorDisplay != null)
                                uiElementForErrorDisplay.Visibility = Visibility.Collapsed;
                            
                        }

                        textbox.TextChanged -= Textbox_TextChanged;

                    }
                }
                else
                {
                    return;
                }
            }
            ));

        private static void Textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
                ValidateTextbox(textBox);
        }

        public static string GetErrorControlName(DependencyObject obj)
        {
            return (string)obj.GetValue(ErrorControllerProp);
        }

        public static void SetErrorControlName(DependencyObject obj, string value)
        {
            obj.SetValue(ErrorControllerProp, value);
        }

        // Using a DependencyProperty as the backing store for ErrorController
        public static readonly DependencyProperty ErrorControllerProp =
            DependencyProperty.RegisterAttached("ErrorControlName", typeof(string), typeof(TextBoxValidator), new PropertyMetadata(""));

        private static void ValidateTextbox(TextBox textbox)
        {
            bool isValid = true;
            var regularExpression = TextBoxValidator.GetRegexValidator(textbox);
            var errorControl = TextBoxValidator.GetErrorControlName(textbox);

            if (errorControl == null)
                return;

            var uiElement = (UIElement)textbox.FindName(errorControl);
            if (uiElement == null)
                return;

            if (string.IsNullOrEmpty(regularExpression) == false && textbox.Text != null)
            {
                var re = new Regex(regularExpression);
                isValid = re.IsMatch(textbox.Text);
            }

            uiElement.Visibility = isValid ? 
                Visibility.Collapsed : 
                Visibility.Visible;
            
        }
    }
}
