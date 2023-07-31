﻿using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;

namespace PraiseHim.Rejoice.WpfWindowToolkit.Behaviors
{
    /// <summary>
    /// A trigger that fires when scrolling to a specific element in a ScrollViewer.
    /// </summary>
    [TypeConstraint(typeof(ScrollViewer))]
    public class ScrollingTrigger : TriggerBase<ScrollViewer>
    {
        /// <summary>
        /// TargetElementProperty
        /// </summary>
        public static readonly DependencyProperty TargetElementProperty =
            DependencyProperty.Register("TargetElement", typeof(FrameworkElement), typeof(ScrollingTrigger), new PropertyMetadata(null));

        /// <summary>
        /// The target element to scroll to
        /// </summary>
        public FrameworkElement TargetElement
        {
            get { return (FrameworkElement)GetValue(TargetElementProperty); }
            set { SetValue(TargetElementProperty, value); }
        }

        /// <summary>
        /// OnAttached
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();

            if (AssociatedObject != null)
            {
                AssociatedObject.ScrollChanged -= AssociatedObject_ScrollChanged;
            }

            AssociatedObject.ScrollChanged += AssociatedObject_ScrollChanged;
        }

        /// <summary>
        /// OnDetaching
        /// </summary>
        protected override void OnDetaching()
        {
            if (AssociatedObject != null)
            {
                AssociatedObject.ScrollChanged -= AssociatedObject_ScrollChanged;
            }

            base.OnDetaching();
        }

        private void AssociatedObject_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            // get the current vertical offset of the ScrollViewer
            var currentScrollPosition = AssociatedObject.VerticalOffset;
            var point = new Point(0, currentScrollPosition);

            var targetBound = TargetElement.TransformToVisual(AssociatedObject).TransformBounds(new Rect(0, currentScrollPosition,
                TargetElement.ActualWidth, TargetElement.ActualHeight));

            if (currentScrollPosition >= targetBound.Y && currentScrollPosition <= targetBound.Y + targetBound.Height)
            {
                InvokeActions(null);
            }
        }
    }
}