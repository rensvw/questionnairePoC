using System.Collections.ObjectModel;
using System.Collections.Generic;


public class QuestionnaireViewModel
{
    public int Id { get; set; }
    public string Category {get; set;}
    public string Version {get; set;}
    public Collection<QuestionFormly> Questions { get; set; }
}


public class FormlyOption
{
    public int Id { get; set; }
    public int Value { get; set; }
    public string Label { get; set; }
}

public class TemplateFormly
{
    public int Id { get; set; }
    public string Type { get; set; }
    public bool Required { get; set; }
    public Collection<TranslationTemplate> Translations { get; set; }
    public bool Multiple { get; set; }
    
}

public class TranslationTemplate
{
    public int Id { get; set; }

    public string Label { get; set; }
    public string Placeholder { get; set; }
    public string Language { get; set; }
    public string Description { get; set; }
    public Collection<FormlyOption> MultipleChoiceOptions { get; set; }
    public FormlyValidation Validation { get; set; }
    public string SelectAllOption { get; set; }

}

public class QuestionFormly
{
    public int Id { get; set; }
    public string Key {get; set;}
    public string Type { get; set; }
    public string Version { get; set; }
    public TemplateFormly TemplateOptions {get; set;}
    public string HideExpression {get; set;}
    
    public Dictionary<string,string> ExpressionProperties {get; set;}
}

public class Messages
{
    public int Id { get; set; }
    public string Required { get; set; }
}

public class FormlyValidation
{
    public int Id { get; set; }
    public Dictionary<string,string> Messages { get; set; }
}