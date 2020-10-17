using System;
using Xamarin.Forms;

namespace Guadalupe.Conexao.App.Behaviors
{
    sealed class EntryValidatorBehavior : Behavior<Entry>
    {
        public static readonly BindableProperty IsTouchedProperty = BindableProperty.Create("IsTouched", typeof(bool), typeof(EntryValidatorBehavior), false, BindingMode.TwoWay);

        public bool IsTouched
        {
            get => (bool)GetValue(IsTouchedProperty);
            set {
                SetValue(IsTouchedProperty, value);
            }
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);

            if(bindable.BindingContext != null)
                BindingContext = bindable.BindingContext;

            bindable.BindingContextChanged += Bindable_BindingContextChanged;
            bindable.Unfocused += Bindable_Unfocused;
        }
        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);

            bindable.BindingContextChanged -= Bindable_BindingContextChanged;
            bindable.Focused -= Bindable_Unfocused;
        }
        private void Bindable_Unfocused(object sender, FocusEventArgs e)
        {
            IsTouched = true;
        }
        private void Bindable_BindingContextChanged(object sender, EventArgs e)
        {
            base.OnBindingContextChanged();

            if (!(sender is BindableObject bindable))
                return;

            BindingContext = bindable.BindingContext;
        }


    }
}
