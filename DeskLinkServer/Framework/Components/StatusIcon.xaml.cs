using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using DeskLinkServer.Logic.Helpers.StringEnum;

namespace DeskLinkServer.Framework.Components
{
    public partial class StatusIcon : UserControl
    {
        private static readonly DependencyProperty StatusProperty = DependencyProperty.Register(
            "Status",
            typeof(Status),
            typeof(StatusIcon),
            new PropertyMetadata(Status.Success, new PropertyChangedCallback(OnStatusChanged))
        );

        private static void OnStatusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as StatusIcon).StatusChanged();
        }

        public Status Status
        {
            get
            {
                return (Status)GetValue(StatusProperty);
            }
            set
            {
                SetValue(StatusProperty, value);
            }
        }

        private void StatusChanged()
        {
            string icon = StatusEnum.GetIconName(Status);
            if (icon != null)
            {
                IconImage.Source = (DrawingImage)IconDictionary[icon];
                if (StatusEnum.GetAnimated(Status))
                    AnimationBoard.Begin();
                else
                    AnimationBoard.Stop();
            }
        }

        private readonly Storyboard AnimationBoard;
        private readonly ResourceDictionary IconDictionary;

        public StatusIcon()
        {
            InitializeComponent();
            AnimationBoard = new Storyboard();
            IconDictionary = new ResourceDictionary()
            {
                Source = new Uri("/DeskLinkServer;component/Resources/Icons.xaml", UriKind.RelativeOrAbsolute)
            };
            RenderTransform = new RotateTransform(0);
            RenderTransformOrigin = new Point(0.5, 0.5);
            DoubleAnimation rotationAnimation = new DoubleAnimation()
            {
                Duration = TimeSpan.FromSeconds(1.2),
                RepeatBehavior = RepeatBehavior.Forever,
                EasingFunction = new CubicEase(),
                AccelerationRatio = 0.5,
                DecelerationRatio = 0.1,
                From = 0,
                To = 540
            };
            DoubleAnimation opacityAnimation = new DoubleAnimation()
            {
                From = 0.1,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.6),
                RepeatBehavior = RepeatBehavior.Forever,
                AutoReverse = true
            };
            Storyboard.SetTarget(rotationAnimation, this);
            Storyboard.SetTargetProperty(rotationAnimation, new PropertyPath("(UIElement.RenderTransform).(RotateTransform.Angle)"));
            Storyboard.SetTarget(opacityAnimation, this);
            Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath("Opacity"));
            AnimationBoard.Children.Add(rotationAnimation);
            AnimationBoard.Children.Add(opacityAnimation);
            StatusChanged();
        }
    }
}
