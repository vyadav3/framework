﻿using Microsoft.Xaml.Behaviors;
using PraiseHim.Rejoice.WpfWindowToolkit.Extensions;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PraiseHim.Rejoice.WpfWindowToolkit.Behaviors
{
    /// <summary>
    /// An action that makes the specific ScrollViewer scrolls to the target element vertically.
    /// </summary>
    public class ScrollToTargetAction : TriggerAction<FrameworkElement>
    {
        /// <summary>
        /// ScrollViewerProperty
        /// </summary>
        public static readonly DependencyProperty ScrollViewerProperty =
            DependencyProperty.Register("ScrollViewer", typeof(ScrollViewer), typeof(ScrollToTargetAction), new PropertyMetadata(null));

        /// <summary>
        /// TargetElementProperty
        /// </summary>
        public static readonly DependencyProperty TargetElementProperty =
                    DependencyProperty.Register("TargetElement", typeof(FrameworkElement), typeof(ScrollToTargetAction), new PropertyMetadata(null));

        /// <summary>
        /// The target ScrollViewer
        /// </summary>
        public ScrollViewer ScrollViewer
        {
            get { return (ScrollViewer)GetValue(ScrollViewerProperty); }
            set { SetValue(ScrollViewerProperty, value); }
        }

        /// <summary>
        /// The target element
        /// </summary>
        public FrameworkElement TargetElement
        {
            get { return (FrameworkElement)GetValue(TargetElementProperty); }
            set { SetValue(TargetElementProperty, value); }
        }

        /// <summary>
        /// Overrides Invoke method
        /// </summary>
        /// <param name="parameter"></param>
        protected override void Invoke(object parameter)
        {
            if (TargetElement == null || ScrollViewer == null)
            {
                throw new ArgumentNullException($"{ScrollViewer} or {TargetElement} cannot be null");
            }

            // check if the target element is contained in the target ScrollViewer
            var container = TargetElement.GetParent<ScrollViewer>();
            if (container == null || container != ScrollViewer)
            {
                throw new InvalidOperationException("The TargetElement is not in the target ScrollViewer");
            }

            // get the current vertical offset
            var currentScrollPosition = ScrollViewer.VerticalOffset;
            var point = new Point(0, currentScrollPosition);

            var targetPosition = TargetElement.TransformToVisual(ScrollViewer).Transform(point);
            ScrollViewer.ScrollToVerticalOffset(targetPosition.Y);
        }
    }
}