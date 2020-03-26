using System.Collections.ObjectModel;

public class QuestionnaireViewModel
{
    public int Id { get; set; }
    public string Category {get; set;}
    public string Version {get; set;}
    public Collection<ChildFormlySchema> Schema { get; set; }
}

public class ChildFormlySchema
{
    public int Id { get; set; }
    public string Key { get; set; }
    public string Type { get; set; }
    public Collection<TemplateOptionsFormlyModel> TemplateOptions { get; set; }
    public Validation Validation { get; set; }
    public string Version { get; set; }
    public string HideExpression { get; set; }
    public Collection<FieldGroup> FieldGroup { get; set; }
}

public class FieldGroup
{
    public int Id { get; set; }
    public string Key { get; set; }
    public string Type { get; set; }
    public Collection<DefaultValue> DefaultValue { get; set; }
    public TemplateOptionsFormlyModel TemplateOptions { get; set; }
    public FieldArray FieldArray { get; set; }
}

public class FormlyOption
{
    public int Id { get; set; }
    public int Value { get; set; }
    public string Label { get; set; }
}

public class TemplateOptionsFormlyModel
{
    public int Id { get; set; }
    public string Label { get; set; }
    public string Type { get; set; }
    public string Language { get; set; }
    public string Version { get; set; }
    public string Placeholder { get; set; }
    public string Description { get; set; }
    public bool Required { get; set; }
    public Collection<FormlyOption> Options { get; set; }
    public bool? multiple { get; set; }
    public string selectAllOption { get; set; }
}

public class Messages
{
    public int Id { get; set; }
    public string Required { get; set; }
}

public class Validation
{
    public int Id { get; set; }
    public Messages Messages { get; set; }
}

public class DefaultValue
{
    public int Id { get; set; }
}


public class ValidationFormly
{
    public int Id { get; set; }
    public Messages Messages { get; set; }
}

public class FieldGroup2
{
    public int Id { get; set; }
    public string Type { get; set; }
    public string Key { get; set; }
    public TemplateOptionsFormlyModel TemplateOptions { get; set; }
    public ValidationFormly Validation { get; set; }
}

public class FieldArray
{
    public int Id { get; set; }
    public Collection<FieldGroup2> FieldGroup { get; set; }
}