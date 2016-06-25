namespace SelectorAttachedPropertiesHasActivatableSupport.Utils
{
    public interface IActivatable
    {
        void Activate();

        /// <summary> return false to cancel deactivation </summary>
        bool Deactivate();
    }
}