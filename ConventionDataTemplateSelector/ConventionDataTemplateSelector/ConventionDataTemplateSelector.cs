using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ConventionDataTemplateSelectorDemo
{

    public class ConventionDataTemplateSelector : DataTemplateSelector
    {
        // cache the templates until the model goes away
        private readonly ConditionalWeakTable<object, DataTemplate> _templatesCache = new ConditionalWeakTable<object, DataTemplate>();

        const string Model = "Model";
        const string View = "View";

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            // Don't use the selector in design mode in Visual Studio
            if (DesignerProperties.GetIsInDesignMode(container))
                return base.SelectTemplate(item, container);

            if (item != null)
            {
                lock (_templatesCache)
                {
                    DataTemplate template;
                    if (_templatesCache.TryGetValue(item, out template))
                        return template;

                    var templateType = GetTemplateTypeFor(item, container);
                    template = new DataTemplate
                    {
                        VisualTree = new FrameworkElementFactory
                        {
                            Type = templateType
                        }
                    };

                    _templatesCache.Add(item, template);
                    return template;
                }
            }

            return base.SelectTemplate(null, container);
        }

        public virtual Type GetTemplateTypeFor(object item, DependencyObject container)
        {
            var type = item.GetType();
            try
            {
                if (!type.Name.EndsWith(Model, StringComparison.Ordinal))
                {
                    throw new TypeLoadException($"type {type} does not conform to the conventions or a viewmodel," +
                                                $" the type's name should end with '{Model}'");
                }

                var viewTypeName = type.FullName;
                viewTypeName = viewTypeName.Substring(0, viewTypeName.Length - Model.Length) + View;

                try
                {
                    // Load the view type from the same assembly as the model type.
                    var templateTypeFor = type.Assembly.GetType(viewTypeName, true);
                    return templateTypeFor;
                }
                catch (Exception e)
                {
                    throw new TypeLoadException($"{GetType().Name}: Error loading view type {viewTypeName} for model {type}: {e.Message}", e);
                }
            }
            catch (TypeLoadException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new TypeLoadException($"{GetType().Name}: Error loading view for model {type}: {e.Message}", e);
            }
        }
    }

}
