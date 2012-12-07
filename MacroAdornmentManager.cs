using Microsoft.VisualStudio.Text.Editor;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace VSTextMacros
{
    // Manages the visual cue that is shown when recording a macro
    public class MacroAdornmentManager
    {
        // A reference to the text view
        private readonly IWpfTextView view;

        // The adorner layer, where the visual is added
        private readonly IAdornmentLayer layer;

        // The visual element shown when recording
        private UIElement visual;

        // Constructor
        private MacroAdornmentManager(IWpfTextView view)
        {
            this.view = view;
            this.layer = view.GetAdornmentLayer("CommentAdornmentLayer");
            
            // Reposition the visual when the editor is resized
            this.view.LayoutChanged += (s, e) =>
            {
                PositionVisual();
            };
        }

        // Shows the visual
        public void ShowVisual()
        {
            if (visual == null)
            {
                // Create the visual
                this.visual = CreateVisual();

                // Position it
                PositionVisual();

                // Add it to the adornment layer
                this.layer.AddAdornment(
                    AdornmentPositioningBehavior.ViewportRelative,
                    null,
                    "MacroRecording",
                    this.visual,
                    null);
            }
        }

        // Hides the visual
        public void HideVisual()
        {
            this.layer.RemoveAdornmentsByTag("MacroRecording");
            this.visual = null;
        }

        // Repositions the visual in the top-right corner of the view
        private void PositionVisual()
        {
            if (visual != null)
            {
                Canvas.SetLeft(this.visual, view.ViewportRight - 150);
                Canvas.SetTop(this.visual, view.ViewportTop + 10);
            }
        }

        // Creates the visual element
        private UIElement CreateVisual()
        {
            var ellipse = new Ellipse
            {
                Width = 15,
                Height = 15,
                Fill = Brushes.DarkRed,
                Margin = new Thickness(8),
                Opacity = 0
            };

            var animation = new DoubleAnimationUsingKeyFrames
            {
                Duration = new Duration(TimeSpan.FromSeconds(1)),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever,
            };

            animation.KeyFrames.Add(new DiscreteDoubleKeyFrame(0, KeyTime.FromPercent(0.00)));
            animation.KeyFrames.Add(new DiscreteDoubleKeyFrame(1, KeyTime.FromPercent(0.25)));
            animation.KeyFrames.Add(new DiscreteDoubleKeyFrame(1, KeyTime.FromPercent(0.50)));
            animation.KeyFrames.Add(new DiscreteDoubleKeyFrame(0, KeyTime.FromPercent(0.75)));

            Storyboard.SetTarget(animation, ellipse);
            Storyboard.SetTargetProperty(animation, new PropertyPath(Ellipse.OpacityProperty));

            var storyboard = new Storyboard();
            storyboard.Children.Add(animation);
            storyboard.Begin(ellipse);

            var text = new TextBlock
            {
                Text = "Recording Macro",
                Foreground = Brushes.DarkSlateGray,
                Margin = new Thickness(4, 0, 8, 2),
                VerticalAlignment = VerticalAlignment.Center
            };

            var panel = new StackPanel
            {
                Orientation = Orientation.Horizontal
            };

            panel.Children.Add(ellipse);
            panel.Children.Add(text);

            return new Border
            {
                Background = Brushes.WhiteSmoke,
                BorderThickness = new Thickness(2),
                BorderBrush = Brushes.DarkSlateGray,
                CornerRadius = new CornerRadius(3),
                Child = panel
            };
        }

        // Instanciates this class, and attaches the instance to the view
        public static MacroAdornmentManager Create(IWpfTextView view)
        {
            return view.Properties.GetOrCreateSingletonProperty<MacroAdornmentManager>(() => new MacroAdornmentManager(view));
        }
    }
}
