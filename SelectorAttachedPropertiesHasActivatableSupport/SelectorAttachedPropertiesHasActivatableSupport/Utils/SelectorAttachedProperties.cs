using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace SelectorAttachedPropertiesHasActivatableSupport.Utils
{
    /// <summary>This Attached Property sets Selector.IsSynchronizedWithCurrentItem to true.</summary>
    /// <remarks>
    /// Also, weird behavior can happen if dialog boxes are shown in the Activate or Deactivate implementations
    /// </remarks>
    public static class SelectorAttachedProperties
    {
        private static readonly Type _ownerType = typeof(SelectorAttachedProperties);

        private static readonly ConditionalWeakTable<Selector, ActivationHandler> _handlers = new ConditionalWeakTable<Selector, ActivationHandler>();


        public static readonly DependencyProperty HasActivatableSupportProperty =
            DependencyProperty.RegisterAttached("HasActivatableSupport", typeof(bool), _ownerType,
                new PropertyMetadata(false, HasActivatableSupportChanged));

        public static bool GetHasActivatableSupport(DependencyObject obj)
        {
            return (bool)obj.GetValue(HasActivatableSupportProperty);
        }

        /// <summary>This attached property sets Selector.IsSynchronizedWithCurrentItem to true.</summary>
        public static void SetHasActivatableSupport(DependencyObject obj, bool value)
        {
            obj.SetValue(HasActivatableSupportProperty, value);
        }

        private static void HasActivatableSupportChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var selector = d as Selector;
            if (selector == null || !(e.OldValue is bool && e.NewValue is bool) || e.OldValue == e.NewValue)
                return;

            var enableActivatableSupport = (bool)e.NewValue;
            var handler = _handlers.GetOrCreateValue(selector);

            TypeDescriptor.GetProperties(selector)["ItemsSource"].RemoveValueChanged(selector, handler.OnItemsSourceChanged);
            selector.SelectionChanged -= handler.OnSelectionChanged;

            if (enableActivatableSupport)
            {
                selector.IsSynchronizedWithCurrentItem = true;
                TypeDescriptor.GetProperties(selector)["ItemsSource"].AddValueChanged(selector, handler.OnItemsSourceChanged);
                selector.SelectionChanged += handler.OnSelectionChanged;
            }
        }

        private class ActivationHandler
        {
            ICollectionView _collectionView;

            public void OnItemsSourceChanged(object sender, EventArgs e)
            {
                var selector = sender as Selector;
                if (selector != null)
                {
                    var itemsSource = selector.ItemsSource;
                    _collectionView = itemsSource as ICollectionView ?? CollectionViewSource.GetDefaultView(itemsSource);
                    _collectionView.CurrentChanging += OnCurrentChanging;
                    _collectionView.CurrentChanged += OnCurrentChanged;
                }
            }

            public void OnSelectionChanged(object sender, SelectionChangedEventArgs args)
            {
                var collectionView = _collectionView;
                if (collectionView == null) return;

                var selector = sender as Selector;
                if (selector == null) return;

                if (selector.IsSynchronizedWithCurrentItem == true && selector.SelectedItem != collectionView.CurrentItem)
                {
                    selector.IsSynchronizedWithCurrentItem = false;
                    selector.SelectedItem = collectionView.CurrentItem;
                    selector.IsSynchronizedWithCurrentItem = true;
                }
            }

            private void OnCurrentChanging(object sender, CurrentChangingEventArgs e)
            {
                var activatable = (sender as ICollectionView)?.CurrentItem as IActivatable;
                if (activatable?.Deactivate() == false)
                {
                    if (e.IsCancelable)
                        e.Cancel = true;
                }
            }

            private void OnCurrentChanged(object sender, EventArgs e)
            {
                var activatable = (sender as ICollectionView)?.CurrentItem as IActivatable;
                activatable?.Activate();
            }
        }
    }
}