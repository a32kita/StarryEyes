﻿using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using Livet.EventListeners;

namespace StarryEyes.Views.Behaviors
{
    public class TimelineTriggerBehavior : Behavior<ScrollViewer>
    {
        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        private bool? _isScrollOnTop;
        public bool IsScrollOnTop
        {
            get { return (_isScrollOnTop = (bool)GetValue(IsScrollOnTopProperty)).Value; }
            set
            {
                if (_isScrollOnTop != value)
                {
                    _isScrollOnTop = value;
                    SetValue(IsScrollOnTopProperty, value);
                }
            }
        }

        public static readonly DependencyProperty IsScrollOnTopProperty =
            DependencyProperty.Register("IsScrollOnTop", typeof(bool), typeof(TimelineTriggerBehavior), new PropertyMetadata(false));

        private bool? _isScrollOnBottom;
        public bool IsScrollOnBottom
        {
            get { return (_isScrollOnBottom = (bool)GetValue(IsScrollOnBottomProperty)).Value; }
            set
            {
                if (_isScrollOnBottom != value)
                {
                    _isScrollOnBottom = value;
                    SetValue(IsScrollOnBottomProperty, value);
                }
            }
        }

        public static readonly DependencyProperty IsScrollOnBottomProperty =
            DependencyProperty.Register("IsScrollOnBottom", typeof(bool), typeof(TimelineTriggerBehavior), new PropertyMetadata(false));

        private bool? _isMouseOver;
        public bool IsMouseOver
        {
            get { return (_isMouseOver = (bool)GetValue(IsMouseOverProperty)).Value; }
            set
            {
                if (_isMouseOver != value)
                {
                    _isMouseOver = value;
                    SetValue(IsMouseOverProperty, value);
                }
            }
        }

        public static readonly DependencyProperty IsMouseOverProperty =
            DependencyProperty.Register("IsMouseOver", typeof(bool), typeof(TimelineTriggerBehavior), new PropertyMetadata(false));

        protected override void OnAttached()
        {
            base.OnAttached();
            this._disposables.Add(
                new EventListener<MouseEventHandler>(
                    h => this.AssociatedObject.MouseEnter += h,
                    h => this.AssociatedObject.MouseEnter -= h,
                    (_, __) => this.IsMouseOver = this.AssociatedObject.IsMouseOver));
            this._disposables.Add(
                new EventListener<MouseEventHandler>(
                    h => this.AssociatedObject.MouseLeave += h,
                    h => this.AssociatedObject.MouseLeave -= h,
                    (_, __) => this.IsMouseOver = this.AssociatedObject.IsMouseOver));
            this._disposables.Add(
                new EventListener<ScrollChangedEventHandler>(
                    h => this.AssociatedObject.ScrollChanged += h,
                    h => this.AssociatedObject.ScrollChanged -= h,
                    (_, __) =>
                    {
                        bool top = this.AssociatedObject.VerticalOffset < 1;
                        bool bottom = this.AssociatedObject.VerticalOffset > this.AssociatedObject.ScrollableHeight - 1;
                        IsScrollOnTop = top;
                        IsScrollOnBottom = bottom;
                    }));
        }

        protected override void OnDetaching()
        {
            this._disposables.Dispose();
            base.OnDetaching();
        }

    }
}
